
-- =============================================
-- Author:		Juan David Capera
-- Create date: 2023-04-13
-- Description:	vista para obtener la información necesaria para enviar al API de integración factura electrónica
-- =============================================

CREATE VIEW [Billing].[ViewElectronicDocumentMoreInformation]
AS
	WITH cte_LastElectronicDocument AS (
		SELECT
			ed.EntityId,
			MAX(ed.Id) AS LastEdId
		FROM Billing.ElectronicDocument ed WITH (NOLOCK)
		WHERE ed.EntityName = 'Invoice'
		GROUP BY ed.EntityId
	),
	cte_Header as	(	
		select	i.Id as EntityId,
				'Invoice' EntityName,
				i.InvoiceValue Value,
				i.TotalValue,
				i.Observation,
				i.TaxDevolutionValue,
				i.InvoiceNumber as InternalInvoice,
				i.CurrencyId,
				i.ThirdPartyId,
				i.CareGroupId,
				i.AdmissionNumber,
				led.LastEdId AS Id,
				ar.InvoiceId,
				ar.Term,
				ar.Id as  AccountReceivableId,
				ar.Balance,
				i.RevenueControlDetailId,
                i.InvoiceDate
		FROM Billing.Invoice i WITH (NOLOCK)
		JOIN cte_LastElectronicDocument led ON led.EntityId = i.Id
		JOIN Billing.ElectronicDocument ed WITH (NOLOCK) ON ed.Id = led.LastEdId
		JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON i.Id = ar.InvoiceId
	)
	, PaymentMethods AS (
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
			where rcd.FolioType = 3 and round(ar.Balance,-1) <> 0
		) payment
		GROUP BY payment.InvoiceId
	)
	, cte_AdditionalData AS (
		SELECT
				ed.Id,
				COALESCE(sod.Code, bb.Code, '') EconomicActivityCode,
				CASE
					WHEN ed.DocumentType = 91 THEN '03'
					WHEN ed.DocumentType = 92 THEN '02' 
					WHEN i.IsElectronicTicket = 0 THEN '01'
					WHEN i.IsElectronicTicket = 1 THEN '04'
					ELSE ''
				END DocumentTypeCode,
				ISNULL(ea.Code, '') ReceiverEconomicActivityCode
		FROM Billing.ElectronicDocument ed WITH (NOLOCK)
		LEFT JOIN Billing.Invoice i WITH (NOLOCK) ON i.Id = ed.EntityId AND 'Invoice' = ed.EntityName
		LEFT JOIN Common.EconomicActivity ea WITH (NOLOCK) ON i.EconomicActivityId = ea.Id
		LEFT JOIN
		(
			SELECT
				id.InvoiceId,
				MIN(ea.Code) AS Code
			FROM Billing.InvoiceDetail id WITH (NOLOCK)
			INNER JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK)	ON sod.Id = id.ServiceOrderDetailId
			INNER JOIN Common.EconomicActivity ea WITH (NOLOCK) ON ea.Id = sod.EconomicActivityId
			GROUP BY id.InvoiceId
		) AS sod ON sod.InvoiceId = i.Id
		LEFT JOIN
		(
			SELECT
				bb.InvoiceId,
				MIN(ea.Code) AS Code
			FROM Billing.BasicBilling bb WITH (NOLOCK)
			INNER JOIN Billing.BasicBillingDetail bbd WITH (NOLOCK)	ON bbd.BasicBillingId = bb.Id
			INNER JOIN Common.EconomicActivity ea WITH (NOLOCK) ON ea.Id = bbd.EconomicActivityId
			GROUP BY bb.InvoiceId
		) AS bb ON bb.InvoiceId = i.Id
	)
	, cte_BillingNoteTRM AS
	(
		SELECT 
			trm.BillingNoteId,
			trm.TRMValue,
			trm.TRMValueReverse
		FROM (
			SELECT 
				bnd.BillingNoteId,
				ISNULL(trm.ValueOfficialToCurrency,1) AS TRMValue,
				arer.ValueReverse AS TRMValueReverse,
				ROW_NUMBER() OVER (PARTITION BY bnd.BillingNoteId ORDER BY bnd.Id) AS RowNum
			FROM Billing.BillingNoteDetail bnd WITH (NOLOCK)
			JOIN Billing.Invoice i WITH (NOLOCK) ON bnd.InvoiceId = i.Id
			JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON i.Id = ar.InvoiceId
			JOIN Portfolio.AccountReceivableExchangeRate arer WITH (NOLOCK) ON ar.Id = arer.AccountReceivableId
			LEFT JOIN Common.TRM trm on trm.MeasurementDate = convert(date,I.InvoiceDate)
			--WHERE i.InvoiceDate < '2025-10-07'
		) trm
		WHERE trm.RowNum = 1
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
		CASE        
			WHEN ISNULL(cte.CurrencyId, cs.OfficialCurrencyId) = 2
			THEN COALESCE(trm.ValueOfficialToCurrency, 1)
			ELSE 1
		END AS TRMValue,
		ISNULL(arer.ValueReverse, 1) TRMValueReverse,
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
		RIGHT('00' + LEFT(LTRIM(RTRIM(distrito.UBICODIGO)), 2), 2) AS 'Distrito',
		RIGHT('00' + LEFT(LTRIM(RTRIM(canton.UBICODIGO)), 2), 2) AS 'Canton',
		LEFT(RTRIM(provincia.UBICODIGO), 1) 'Provincia', 
		'0' Pais,
		pa.Address ThirdPartyAddress,
		SUBSTRING(REPLACE((SELECT ',' + e.Email FROM Common.Email e WHERE e.Type = 2 AND e.IdPerson = tp.PersonId FOR XML PATH ('')), '&#x0D;', ''),2, 100) AS Emails,
		'0' AS Phone,
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
		trim(ing.NUMINGRES) Admission,
		ad.DocumentTypeCode,
		ad.EconomicActivityCode,
		ad.ReceiverEconomicActivityCode
	FROM cte_Header cte
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
	-----Medios de pago-----
	LEFT JOIN PaymentMethods pm ON cte.EntityId = pm.InvoiceId
	-----Additional Data ---
	LEFT JOIN cte_AdditionalData ad ON cte.Id = ad.Id
	LEFT JOIN Portfolio.AccountReceivableExchangeRate arer WITH (NOLOCK) ON cte.AccountReceivableId = arer.AccountReceivableId
    LEFT JOIN Common.TRM trm on trm.MeasurementDate = convert(date,cte.InvoiceDate)

	UNION ALL

	SELECT 
		ed.Id,
		bn.Id EntityId,
		'BillingNote' EntityName,
		ISNULL(pn.CurrencyId, i.CurrencyId) CurrencyDocumentId,
		ISNULL(cn.Abbreviation, ci.Abbreviation) CurrencyDocumentAbbreviation,
		cs.OfficialCurrencyId,
		CASE
            WHEN ISNULL(pn.CurrencyId, i.CurrencyId) = 2
            THEN trm.TRMValue
            ELSE 1
		END AS TRMValue,
		ISNULL(trm.TRMValueReverse, 1) TRMValueReverse,
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
		RIGHT('00' + LEFT(LTRIM(RTRIM(distrito.UBICODIGO)), 2), 2) AS 'Distrito',
		RIGHT('00' + LEFT(LTRIM(RTRIM(canton.UBICODIGO)), 2), 2) AS 'Canton',
		LEFT(RTRIM(provincia.UBICODIGO), 1) 'Provincia', 
		'0' AS 'Pais',
		pa.Address ThirdPartyAddress,
		SUBSTRING(REPLACE((SELECT ',' + e.Email FROM Common.Email e WHERE e.Type = 2 AND e.IdPerson = tp.PersonId FOR XML PATH ('')), '&#x0D;', ''),2, 100) AS Emails,
		'0' AS Phone,
		---- datos del pago --------
		0 PayValue,
		NULL PaymentMethodTypes,
		CAST(1 AS BIT) IsCounted,
		NULL ConditionSalesCode,
		isnull(i.TaxDevolutionValue,0) TaxDevolutionValue,
		null as InternalInvoice,
		null as RealPatientIdentification,
		null as RealPatientName,
		null Admission,
		ad.DocumentTypeCode,
		ad.EconomicActivityCode,
		ad.ReceiverEconomicActivityCode
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
	LEFT JOIN Common.EconomicActivity ea ON i.EconomicActivityId = ea.Id
	----Paciente----
	LEFT JOIN INPACIENT pac WITH (NOLOCK) ON pac.IPCODPACI = tp.Nit
	LEFT JOIN ADTIPOIDENTIFICA tipId on tipId.CODIGO = pac.IPTIPODOC
	LEFT JOIN Admissions.PatientAddress pa WITH (NOLOCK) ON pac.ID = pa.IdPatient
	LEFT JOIN INUBICACI distrito WITH (NOLOCK) ON distrito.ID = pa.IdUbication
	LEFT JOIN INUBICACI canton WITH (NOLOCK) ON distrito.UbicationId = canton.ID
	LEFT JOIN INUBICACI provincia WITH (NOLOCK) ON canton.UbicationId = provincia.ID
	-----Additional Data ---
	LEFT JOIN cte_AdditionalData ad ON ed.Id = ad.Id
	LEFT JOIN cte_BillingNoteTRM trm ON bn.Id = trm.BillingNoteId

GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de facturación electrónica que consolida toda la información necesaria para enviar una factura al API de integración con la DIAN. Combina el encabezado de la factura (número interno, valores cobrados, descuentos, observaciones y moneda), el último documento electrónico emitido asociado (CUFE, estado de envío), la cuenta por cobrar correspondiente (saldo pendiente, plazo), los datos del tercero pagador (NIT, nombre, tipo de identificación, dirección, correo electrónico, ubicación geográfica por provincia, cantón y distrito), los medios de pago aplicados (efectivo, transferencia, etc. obtenidos desde recibos de caja y traslados de cartera), el código de actividad económica del emisor y del receptor, el tipo de documento electrónico (factura, nota débito, nota crédito, tiquete electrónico) y los datos reales del paciente (cédula e identificación del paciente, número de ingreso u admisión). Es la fuente principal de consulta para el proceso de emisión y retransmisión de documentos electrónicos de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewElectronicDocumentMoreInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewElectronicDocumentMoreInformation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista los datos de facturas y notas de facturación con sus terceros, paciente, ubicación, medios de pago, monedas/TRM y actividad económica para alimentar la integración con el API de factura electrónica.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentMoreInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Para facturas, debe existir al menos un Billing.ElectronicDocument con EntityName=''Invoice'' asociado a la factura (la CTE toma el MAX(Id) por EntityId).; Para notas, Billing.ElectronicDocument debe tener EntityName=''BillingNote'' y existir el BillingNote correspondiente.; Debe existir GeneralLedger.CompanySettings (JOIN ON 1=1) y la moneda oficial referenciada en Common.Currency.; El tercero (ThirdParty) y su Person deben existir para resolver identificación y emails.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentMoreInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se toma el último ElectronicDocument por factura (MAX(Id) en cte_LastElectronicDocument).; EntityName siempre es ''Invoice'' o ''BillingNote'' según el bloque del UNION ALL.; Para notas, el bloque siempre fija PayValue=0, IsCounted=1 y no expone medios de pago.; Cuando la moneda del documento no es la id 2, TRMValue se fuerza a 1.; TRMValueReverse se sustituye por 1 cuando no hay registro en AccountReceivableExchangeRate.; Los códigos de Distrito/Cantón se normalizan a 2 dígitos con padding (''00''+...) y Provincia a 1 dígito; Pais siempre se devuelve como ''0'' y Phone como ''0''.; Solo se concatenan emails con Type=2 del tercero (Common.Email) en una lista separada por comas.; Para BillingNote, la TRM se toma del primer detalle (RowNum=1) de BillingNoteDetail por BillingNoteId.; EconomicActivityCode prioriza ServiceOrderDetail; si no, BasicBilling; en caso contrario cadena vacía (COALESCE(sod.Code, bb.Code, '''')).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentMoreInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Es una vista de solo lectura: no realiza INSERT/UPDATE/DELETE; devuelve el conjunto unido de facturas y notas de facturación enriquecido con datos para facturación electrónica.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentMoreInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ed.DocumentType = 91 → DocumentTypeCode = ''03'' (nota débito); si ed.DocumentType = 92 → DocumentTypeCode = ''02'' (nota crédito); si i.IsElectronicTicket = 0 → DocumentTypeCode = ''01'' (factura electrónica estándar) else Si IsElectronicTicket = 1 → ''04'' (boleta/ticket electrónico); en otro caso ''''; si ISNULL(cte.CurrencyId, cs.OfficialCurrencyId) = 2 → TRMValue = COALESCE(trm.ValueOfficialToCurrency, 1) según Common.TRM por la fecha de la factura else TRMValue = 1 (sin conversión cuando la moneda no es la id 2); si bb.Id IS NOT NULL (existe BasicBilling para la factura) → IsCounted = 1 si bb.SaleModality = 1, en caso contrario 0 else IsCounted = 1 si pm.Value > 0, en caso contrario 0; si PortfolioTransfer.Status = 2 AND CashReceipts.Status = 2 AND ROUND(ar.Balance,-1) = 0 → Se consideran los métodos de pago provenientes de anticipos de cartera aplicados a la factura; si CashReceipts.Status = 2 AND ROUND(ar.Balance,-1) = 0 → Se consideran los métodos de pago provenientes de recibos de caja aplicados directamente a la cuenta por cobrar; si RevenueControlDetail.FolioType = 3 AND ROUND(ar.Balance,-1) <> 0 → Se asigna PaymentMethodTypes = ''99'' (método ''particular'' por defecto cuando el folio es tipo particular y aún hay saldo); si Origen del registro: factura (cte_Header) → Se exponen Value, TotalValue, Term, datos de paciente real (ADINGRESO/INPACIENT), InternalInvoice, etc. else Si origen es BillingNote: Value=0, TotalValue=0, Term=0, IsCounted=1, PaymentMethodTypes=NULL, paciente y admisión NULL', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentMoreInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ElectronicDocument; Billing.Invoice; Portfolio.AccountReceivable; Portfolio.PortfolioTransferDetail; Portfolio.PortfolioTransfer; Portfolio.PortfolioAdvance; Treasury.CashReceipts; Treasury.PaymentMethods; Treasury.CashReceiptAccountReceivable; Treasury.CashReceiptDetails; Billing.RevenueControlDetail; Common.EconomicActivity; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.BasicBilling; Billing.BasicBillingDetail; Billing.BillingNoteDetail; Portfolio.AccountReceivableExchangeRate; Common.TRM; Common.Email; Common.ThirdParty; Common.Person; GeneralLedger.CompanySettings; Common.Currency; Billing.ElectronicsProperties; Billing.ConditionSales; Billing.BillingNote; Portfolio.PortfolioNote; dbo.ADTIPOIDENTIFICA; dbo.ADINGRESO (+3 adicionales)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentMoreInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentMoreInformation';
GO
