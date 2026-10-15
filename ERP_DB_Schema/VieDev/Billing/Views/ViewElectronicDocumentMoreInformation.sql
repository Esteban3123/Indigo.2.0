





-- =============================================
-- Author:		Juan David Capera
-- Create date: 2023-04-13
-- Description:	vista para obtener la información necesaria para enviar al API de integración factura electrónica
-- =============================================

CREATE VIEW [Billing].[ViewElectronicDocumentMoreInformation]
AS
	WITH cte_Header as	(	select	i.Id as EntityId,
								ed.EntityName,
								i.InvoiceValue Value,
								i.TotalValue,
								i.Observation,
								i.TaxDevolutionValue,
								i.InvoiceNumber as InternalInvoice,
								i.CurrencyId,
								i.ThirdPartyId,
								i.CareGroupId,
								i.AdmissionNumber,
								ed.id,
								ar.InvoiceId,
								ar.Term,
								ar.Id as  AccountReceivableId,
								ar.Balance,
								i.RevenueControlDetailId
						FROM Billing.Invoice i WITH (NOLOCK)
						JOIN Billing.ElectronicDocument ed WITH (NOLOCK) ON i.Id = ed.EntityId AND 'Invoice' = ed.EntityName
						JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON i.Id = ar.InvoiceId),


	 PaymentMethods AS
	(
		SELECT 
			payment.InvoiceId, 
			STRING_AGG( payment.PaymentMethodTypes,',') PaymentMethodTypes,
			0.0 value
		FROM (
			SELECT	ar.InvoiceId,
				CAST(mc.PaymentMethodTypes AS VARCHAR(2)) AS PaymentMethodTypes,
				0.0 Value
			FROM cte_Header ar 
			JOIN Portfolio.PortfolioTransferDetail ptd WITH(NOLOCK) ON ar.AccountReceivableId = ptd.AccountReceivableId
			JOIN Portfolio.PortfolioTransfer pt WITH(NOLOCK) ON ptd.PortfolioTrasferId = pt.Id
			JOIN Portfolio.PortfolioAdvance pa WITH(NOLOCK) ON pt.PortfolioAdvanceId = pa.Id
			JOIN Treasury.CashReceipts cr WITH(NOLOCK) ON pa.CashReceiptId = cr.Id
			LEFT JOIN  Treasury.PaymentMethods mc WITH(NOLOCK) ON mc.IdCashReceipt = cr.Id  
			WHERE pt.Status = 2 AND cr.Status = 2 and ROUND(ar.Balance, -1) = 0
			GROUP BY ar.InvoiceId,mc.PaymentMethodTypes

			UNION ALL

			SELECT	ar.InvoiceId,
					CAST(mc.PaymentMethodTypes AS VARCHAR(2)) AS PaymentMethodTypes,
					0.0 Value
			FROM cte_Header ar WITH(NOLOCK)
			JOIN Treasury.CashReceiptAccountReceivable crar WITH(NOLOCK) ON ar.AccountReceivableId = crar.AccountReceivableId
			JOIN Treasury.CashReceiptDetails crd WITH(NOLOCK) ON crar.CashReceiptDetailId = crd.Id
			JOIN Treasury.CashReceipts cr WITH(NOLOCK) ON crd.IdCashReceipt = cr.Id
			LEFT JOIN  Treasury.PaymentMethods mc WITH(NOLOCK) ON mc.IdCashReceipt = cr.Id
			WHERE cr.Status = 2 and ROUND(ar.Balance,-1) = 0
			GROUP BY ar.InvoiceId,mc.PaymentMethodTypes

			UNION ALL
			--cuando el folio es tipo particular
			SELECT	ar.InvoiceId,
					'99' AS PaymentMethodTypes,
					0.0 Value
			FROM cte_Header ar WITH(NOLOCK)
			join Billing.RevenueControlDetail rcd on rcd.id = ar.RevenueControlDetailId
			where rcd.FolioType = 3 and round(ar.Balance,-1) <> 0) payment
			GROUP BY payment.InvoiceId
	)  
	
	/*-------------------------- RECOLECCIÓN DE DATOS  --------------------------*/
	SELECT 
	    ---- datos de la factura ----------
		cte.Id,
		cte.EntityId,
		'Invoice' EntityName,
		ISNULL(cte.CurrencyId, cs.OfficialCurrencyId) CurrencyDocumentId,
		ISNULL(c.Abbreviation, co.Abbreviation) CurrencyAbbreviation,
		cs.OfficialCurrencyId,
		arer.Value TRMValue,
		arer.ValueReverse TRMValueReverse,
		cte.Value,
		cte.TotalValue,
		cte.Observation,
		cte.Term,
		---- datos del tercero -----------
		tp.Nit ThirdPartyNit,
		tp.Name ThirdPartyName,
		tpIden.SIGLA as ThirdPartyIdentification,
		tipId.SIGLA as PatientIdentification,
		ISNULL(tp.Class, 1) ThirdPartyClass,
		tp.ContributionType,
		distrito.UBICODIGO 'Distrito',
		canton.UBICODIGO 'Canton',
		provincia.UBICODIGO 'Provincia',
		pais.UBICODIGO 'Pais',
		pa.Address ThirdPartyAddress,
		SUBSTRING(REPLACE((SELECT ',' + e.Email FROM Common.Email e WHERE e.Type = 2 AND e.IdPerson = tp.PersonId FOR XML PATH ('')), '&#x0D;', ''),2, 100) AS Emails,
		(SELECT TOP 1  ph.Phone FROM Common.Phone ph WHERE ph.IdPerson = tp.PersonId) AS Phone,
		---- datos del pago --------
		ISNULL(pm.Value, 0) PayValue,
		pm.PaymentMethodTypes,
		CAST(IIF(bb.Id is not null,
				iif(bb.SaleModality = 1, 1, 0), 
				IIF(pm.Value > 0 , 1, 0)
			) AS BIT) IsCounted,
		css.Code ConditionSalesCode,
		cte.TaxDevolutionValue,
		cte.InternalInvoice,
		trim(realPac.IPCODPACI) as RealPatientIdentification,
		trim(realPac.IPNOMCOMP) as RealPatientName,
		trim(ing.NUMINGRES) Admission
	FROM cte_Header cte
	JOIN Portfolio.AccountReceivableExchangeRate arer WITH (NOLOCK) ON cte.AccountReceivableId = arer.AccountReceivableId
	JOIN Common.ThirdParty tp WITH (NOLOCK) ON tp.Id = cte.ThirdPartyId
	JOIN Common.Person p WITH (NOLOCK) ON p.Id = tp.PersonId
	JOIN GeneralLedger.CompanySettings cs WITH (NOLOCK) ON 1 = 1
	JOIN Common.Currency co WITH (NOLOCK) ON co.Id = cs.OfficialCurrencyId
	LEFT JOIN Billing.ElectronicsProperties ep ON ep.EntityId = cte.EntityId AND 'Invoice' = cte.EntityName
	LEFT JOIN Billing.ConditionSales css ON cs.Id = ep.ConditionSalesId
	LEFT JOIN dbo.ADTIPOIDENTIFICA tpIden on tpIden.ID = p.IdentificationTypeId
	LEFT JOIN Billing.BasicBilling bb on bb.InvoiceId = cte.EntityId
	LEFT JOIN Contract.CareGroup cg WITH (NOLOCK) ON cg.Id = cte.CareGroupId 
	LEFT JOIN Common.Currency c WITH (NOLOCK) ON c.Id = cte.CurrencyId
	----Paciente----
	left join ADINGRESO ing on ing.NUMINGRES = cte.AdmissionNumber
	LEFT JOIN INPACIENT realPac WITH (NOLOCK) ON realPac.IPCODPACI = ing.IPCODPACI
	LEFT JOIN INPACIENT pac WITH (NOLOCK) ON pac.IPCODPACI = tp.Nit
	LEFT JOIN ADTIPOIDENTIFICA tipId on tipId.CODIGO = pac.IPTIPODOC
	LEFT JOIN Admissions.PatientAddress pa WITH (NOLOCK) ON pac.ID = pa.IdPatient
	LEFT JOIN INUBICACI distrito WITH (NOLOCK) ON distrito.ID = pa.IdUbication
	LEFT JOIN INUBICACI canton WITH (NOLOCK) ON distrito.UbicationId = canton.ID
	LEFT JOIN INUBICACI provincia WITH (NOLOCK) ON canton.UbicationId = provincia.ID
	LEFT JOIN INUBICACI pais WITH (NOLOCK) ON provincia.UbicationId = pais.ID
	-----Medios de pago-----
	LEFT JOIN PaymentMethods pm ON cte.EntityId = pm.InvoiceId

	UNION ALL

	SELECT 
		ed.Id,
		bn.Id EntityId,
		'BillingNote' EntityName,
		ISNULL(pn.CurrencyId, i.CurrencyId) CurrencyDocumentId,
		ISNULL(cn.Abbreviation, ci.Abbreviation) CurrencyDocumentAbbreviation,
		cs.OfficialCurrencyId,
		1 TRMValue,
		1 TRMValueReverse,
		0 Value,
		0 TotalValue,
		bn.Observations Observation,
		0 Term,
		---- datos del tercero -----------
		tp.Nit ThirdPartyNit,
		tp.Name ThirdPartyName,
		tpIden.SIGLA as ThirdPartyIdentification,
		tipId.SIGLA as PatientIdentification,
		ISNULL(tp.Class, 1) ThirdPartyClass,
		tp.ContributionType,
		distrito.UBICODIGO 'Distrito',
		canton.UBICODIGO 'Canton',
	    provincia.UBICODIGO 'Provincia',
		pais.UBICODIGO 'Pais',
		pa.Address ThirdPartyAddress,
		SUBSTRING(REPLACE((SELECT ',' + e.Email FROM Common.Email e WHERE e.Type = 2 AND e.IdPerson = tp.PersonId FOR XML PATH ('')), '&#x0D;', ''),2, 100) AS Emails,
		(SELECT TOP 1  ph.Phone FROM Common.Phone ph WHERE ph.IdPerson = tp.PersonId) AS Phone,
		---- datos del pago --------
		0 PayValue,
		NULL PaymentMethodTypes,
		CAST(1 AS BIT) IsCounted,
		NULL ConditionSalesCode,
		isnull(i.TaxDevolutionValue,0) TaxDevolutionValue,
		null as InternalInvoice,
		null as RealPatientIdentification,
		null as RealPatientName,
		null Admission
	FROM Billing.ElectronicDocument ed WITH (NOLOCK)
	JOIN Billing.BillingNote bn WITH (NOLOCK) ON bn.Id = ed.EntityId AND 'BillingNote' = ed.EntityName
	JOIN Common.ThirdParty tp WITH (NOLOCK) ON tp.Id = ed.CustomerPartyId
	JOIN Common.Person p WITH (NOLOCK) ON p.Id = tp.PersonId
	JOIN GeneralLedger.CompanySettings cs WITH (NOLOCK) ON 1 = 1
	JOIN Common.Currency co WITH (NOLOCK) ON co.Id = cs.OfficialCurrencyId
	LEFT JOIN dbo.ADTIPOIDENTIFICA tpIden on tpIden.ID = p.IdentificationTypeId
	---
	LEFT JOIN Portfolio.PortfolioNote pn WITH (NOLOCK) ON pn.Id = bn.EntityId AND 'PortfolioNote' = bn.EntityName
	LEFT JOIN Common.Currency cn WITH (NOLOCK) ON cn.Id = pn.CurrencyId
	---
	LEFT JOIN Billing.Invoice i WITH (NOLOCK) ON i.Id = bn.EntityId AND 'Invoice' = bn.EntityName
	LEFT JOIN Common.Currency ci WITH (NOLOCK) ON ci.Id = i.CurrencyId
	----Paciente----
	LEFT JOIN INPACIENT pac WITH (NOLOCK) ON pac.IPCODPACI = tp.Nit
	LEFT JOIN ADTIPOIDENTIFICA tipId on tipId.CODIGO = pac.IPTIPODOC
	LEFT JOIN Admissions.PatientAddress pa WITH (NOLOCK) ON pac.ID = pa.IdPatient
	LEFT JOIN INUBICACI distrito WITH (NOLOCK) ON distrito.ID = pa.IdUbication
	LEFT JOIN INUBICACI canton WITH (NOLOCK) ON distrito.UbicationId = canton.ID
	LEFT JOIN INUBICACI provincia WITH (NOLOCK) ON canton.UbicationId = provincia.ID
	LEFT JOIN INUBICACI pais WITH (NOLOCK) ON provincia.UbicationId = pais.ID