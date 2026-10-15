











CREATE VIEW [Billing].[VReportListInvoice]
AS
	WITH cte_company as (SELECT TOP 1 OfficialCurrencyId from GeneralLedger.CompanySettings),
	Cte_TRM as (SELECT i.InvoiceDate, rcd.CreationDate, rcd.RevenueControlDetailMasterId 
				FROM Billing.RevenueControlDetail rcd
				JOIN Billing.Invoice i ON i.RevenueControlDetailId = rcd.Id
				WHERE IsMasterAccount = 2 AND RevenueControlDetailMasterId IS NOT NULL)

	SELECT	CONCAT(1,i.DocumentType, '-', i.Id, '-', ri.Id) Id,
			i.Id InvoiceId,
			i.DocumentType,
			NULL Code,
			i.InvoiceNumber,	
			i.InvoiceDate as InvoiceDate,
			i.Observation,	
			tp.Nit ThirdPartyNit,
			tp.Name ThirdPartyName,	
			NULL CityName,
			(i.ThirdPartySalesValue + i.ThirdPartyDiscountValue + i.TotalPatientWithDiscount)- i.ValueTax Subtotal,	
			i.ThirdPartyDiscountValue DiscountValue,
			i.TotalPatientWithDiscount PatientValue,
			i.ValueTax,
		   	iif(rcd.IsMasterAccount <>0,(i.ThirdPartySalesValue - i.TaxDevolutionValue),i.ThirdPartySalesValue) TotalValue,
			i.InvoicedUser,
			'Facturado' StatusName,
			---------- FILTROS ----------
			i.CareGroupId,
			i.HealthAdministratorId,
			i.InvoiceCategoryId,
			i.AdmissionNumber,
			i.PatientCode,	
			tp.Id ThirdPartyId,
			NULL SucursalId,
			ri.Id RadicateInvoiceId,
			i.Status,
			c.Id CurrencyId,
			c.Abbreviation,
			rcd.IsMasterAccount,
			epro.CUV,
			RevenueControlDetailMasterId,
			RevenueControlDetailId
	FROM Billing.Invoice i 
	JOIN Common.ThirdParty tp ON i.ThirdPartyId = tp.Id
	JOIN cte_company cte on 1=1
	JOIN Common.Currency c on c.Id = ISNULL(i.CurrencyId, cte.OfficialCurrencyId)
	LEFT JOIN Billing.RevenueControlDetail rcd ON rcd.Id = i.RevenueControlDetailId
	LEFT JOIN Portfolio.RadicateInvoiceD rid ON i.InvoiceNumber = rid.InvoiceNumber
	LEFT JOIN Portfolio.RadicateInvoiceC ri ON rid.RadicateInvoiceCId = ri.Id
	LEFT JOIN Billing.ElectronicsProperties epro ON epro.EntityId = i.Id
	WHERE i.DocumentType IN (1, 2, 3, 5) AND i.Status = 1

UNION ALL

SELECT	CONCAT(2,i.DocumentType, '-', i.Id, '-', ri.Id) Id,
			i.Id InvoiceId,
			i.DocumentType,
			NULL Code,
			i.InvoiceNumber,
			i.AnnulmentDate as InvoiceDate,
			i.Observation,	
			tp.Nit ThirdPartyNit,
			tp.Name ThirdPartyName,	
			NULL CityName,
			((i.ThirdPartySalesValue + i.ThirdPartyDiscountValue + i.TotalPatientWithDiscount)- i.ValueTax) Subtotal,	
			i.ThirdPartyDiscountValue DiscountValue,
			i.TotalPatientWithDiscount PatientValue,
			i.ValueTax,
		   	(i.ThirdPartySalesValue - i.TaxDevolutionValue) TotalValue,
			i.InvoicedUser,
			'Anulado' StatusName,
			---------- FILTROS ----------
			i.CareGroupId,
			i.HealthAdministratorId,
			i.InvoiceCategoryId,
			i.AdmissionNumber,
			i.PatientCode,	
			tp.Id ThirdPartyId,
			NULL SucursalId,
			ri.Id RadicateInvoiceId,
			i.Status,
			c.Id CurrencyId,
			c.Abbreviation,
			1 as IsMasterAccount,
			epro.CUV,
		
			rcd.RevenueControlDetailMasterId,
		i.RevenueControlDetailId
	FROM Billing.Invoice i 
	JOIN Common.ThirdParty tp ON i.ThirdPartyId = tp.Id
	JOIN cte_company cte on 1=1
	JOIN Common.Currency c on c.Id = ISNULL(i.CurrencyId, cte.OfficialCurrencyId)
	LEFT JOIN Portfolio.RadicateInvoiceD rid ON i.InvoiceNumber = rid.InvoiceNumber
	LEFT JOIN Portfolio.RadicateInvoiceC ri ON rid.RadicateInvoiceCId = ri.Id
	LEFT JOIN Billing.ElectronicsProperties epro ON epro.EntityId = i.Id
		LEFT JOIN Billing.RevenueControlDetail rcd ON rcd.Id = i.RevenueControlDetailId
	WHERE i.DocumentType IN (1, 2, 3, 5) and i.Status = 2

UNION ALL

	SELECT	CONCAT(i.DocumentType, '-', i.Id, '-', ri.Id) Id,
			i.Id InvoiceId,
			i.DocumentType,
			NULL Code,
			i.InvoiceNumber,	
			i.InvoiceDate as InvoiceDate,
			i.Observation,	
			tp.Nit ThirdPartyNit,
			tp.Name ThirdPartyName,	
			NULL CityName,
			iif(rcd.IsMasterAccount <>0,(((i.ThirdPartySalesValue + i.ThirdPartyDiscountValue + i.TotalPatientWithDiscount)- i.ValueTax)),(i.ThirdPartySalesValue + i.ThirdPartyDiscountValue + i.TotalPatientWithDiscount)) Subtotal,
			i.ThirdPartyDiscountValue DiscountValue,
			i.TotalPatientWithDiscount PatientValue,
			i.ValueTax,
			iif(rcd.IsMasterAccount <>0,(i.ThirdPartySalesValue - i.TaxDevolutionValue),i.ThirdPartySalesValue) TotalValue,
			i.InvoicedUser,		
			CASE i.Status 
				WHEN 1 THEN 'Facturado' 
				WHEN 2 THEN 'Anulado'
			END StatusName,
			---------- FILTROS ----------
			i.CareGroupId,
			i.HealthAdministratorId,
			i.InvoiceCategoryId,
			i.AdmissionNumber,
			i.PatientCode,
			tp.Id ThirdPartyId,
			NULL SucursalId,
			ri.Id RadicateInvoiceId,
			i.Status,
			c.Id CurrencyId,
			c.Abbreviation,
			rcd.IsMasterAccount,
			epro.CUV,
			RevenueControlDetailMasterId,
			RevenueControlDetailId
	FROM Billing.Invoice i 
	JOIN Common.ThirdParty tp ON i.ThirdPartyId = tp.Id
	JOIN cte_company cte on 1=1
	JOIN Common.Currency c on c.Id = ISNULL(i.CurrencyId, cte.OfficialCurrencyId)
	JOIN Billing.RevenueControlDetail rcd ON rcd.Id = i.RevenueControlDetailId
	LEFT JOIN Portfolio.RadicateInvoiceD rid ON i.InvoiceNumber = rid.InvoiceNumber
	LEFT JOIN Portfolio.RadicateInvoiceC ri ON rid.RadicateInvoiceCId = ri.Id
	LEFT JOIN Billing.ElectronicsProperties epro ON epro.EntityId = i.Id
	WHERE i.DocumentType = 4

UNION ALL

	SELECT	CONCAT(i.DocumentType, '-', i.Id) Id,
			i.Id InvoiceId,
			i.DocumentType,
			bb.Code,
			i.InvoiceNumber,	
			i.InvoiceDate as InvoiceDate,
			bb.Description Observation,
			tp.Nit ThirdPartyNit,
			tp.Name ThirdPartyName,
			c.Name CityName,
			bb.Value Subtotal,
			bb.ValueDiscount DiscountValue,
			0 PatientValue,
			bb.ValueIVA ValueTax,
			bb.TotalValue,
			i.InvoicedUser,
			CASE i.Status 
				WHEN 1 THEN 'Facturado' 
				WHEN 2 THEN 'Anulado'
			END StatusName,
			---------- FILTROS ----------
			NULL CareGroupId,
			NULL HealthAdministratorId,
			NULL InvoiceCategoryId,
			i.AdmissionNumber,
			i.PatientCode,
			tp.Id ThirdPartyId,
			NULL SucursalId,
			NULL RadicateInvoiceId,
			i.Status,
			cu.Id CurrencyId,
			cu.Abbreviation Abbreviation,
			rcd.IsMasterAccount,
			epro.CUV,
			RevenueControlDetailMasterId,
			RevenueControlDetailId
	FROM Billing.Invoice i 
	JOIN Billing.BasicBilling bb ON i.Id = bb.InvoiceId
	JOIN Common.ThirdParty tp ON i.ThirdPartyId = tp.Id
	JOIN Common.Address a ON bb.AddressId = a.Id
	JOIN Common.City c ON a.CityId = c.Id
    JOIN cte_company cte on 1=1
	JOIN Common.Currency cu on cu.Id = COALESCE( bb.CurrencyId,i.CurrencyId,cte.OfficialCurrencyId)
	LEFT JOIN Billing.RevenueControlDetail rcd ON rcd.Id = i.RevenueControlDetailId
	LEFT JOIN Billing.ElectronicsProperties epro ON epro.EntityId = i.Id
	WHERE i.DocumentType = 6

UNION ALL

	SELECT	CONCAT(i.DocumentType, '-', i.Id) Id,
			i.Id InvoiceId,
			i.DocumentType,
			dips.Code,
			i.InvoiceNumber,	
			i.InvoiceDate as InvoiceDate,
			dips.Description Observation,
			tp.Nit ThirdPartyNit,
			tp.Name ThirdPartyName,
			c.Name CityName,
			dips.Value Subtotal,
			dips.ValueDiscount DiscountValue,
			0 PatientValue,
			dips.ValueTax ValueTax,
			dips.TotalValue,
			i.InvoicedUser,
			CASE i.Status 
				WHEN 1 THEN 'Facturado' 
				WHEN 2 THEN 'Anulado'
			END StatusName,
			---------- FILTROS ----------
			NULL CareGroupId,
			NULL HealthAdministratorId,
			NULL InvoiceCategoryId,
			i.AdmissionNumber,
			i.PatientCode,
			tp.Id ThirdPartyId,
			bo.Id SucursalId,
			NULL RadicateInvoiceId,
			i.Status,
			cu.Id CurrencyId,
			cu.Abbreviation,
			rcd.IsMasterAccount,
			epro.CUV,
			RevenueControlDetailMasterId,
			RevenueControlDetailId
	FROM Billing.Invoice i 
	JOIN Inventory.DocumentInvoiceProductSales dips ON i.Id = dips.InvoiceId
	JOIN Common.ThirdParty tp ON dips.ThirdPartyId = tp.Id
	JOIN cte_company cte on 1=1
	JOIN Common.Currency cu on cu.Id = ISNULL(i.CurrencyId, cte.OfficialCurrencyId)
	LEFT JOIN Billing.RevenueControlDetail rcd ON rcd.Id = i.RevenueControlDetailId
	LEFT JOIN Payroll.BranchOffice bo ON dips.BranchOfficeId = bo.Id
	LEFT JOIN Common.City c ON bo.CityId = c.Id
	LEFT JOIN Billing.ElectronicsProperties epro ON epro.EntityId = i.Id
	WHERE i.DocumentType = 7

UNION ALL

	SELECT k.Id,
			k.InvoiceId,
			k.DocumentType,
			k.Code,
			k.InvoiceNumber,	
			k.InvoiceDate,
			k.Observation,
			k.ThirdPartyNit,
			k.ThirdPartyName,
			k.CityName,
			k.Subtotal,	
			k.DiscountValue,
			k.PatientValue,
			k.ValueTax,
			k.TotalValue,
			k.InvoicedUser,
			k.StatusName,
			k.CareGroupId,
			k.HealthAdministratorId,
			k.InvoiceCategoryId,
			k.AdmissionNumber,
			k.PatientCode,
			k.ThirdPartyId,
			k.SucursalId,
			k.RadicateInvoiceId,
			k.Status,
			k.CurrencyId,
			k.Abbreviation,
			k.IsMasterAccount,
			k.CUV,
			K.RevenueControlDetailMasterId,
			k.RevenueControlDetailId
	FROM
	(
	SELECT  CONCAT(8, '-', rcd.Id, '-') Id,
			rcd.Id InvoiceId,
			8 DocumentType,
			NULL Code,
			NULL InvoiceNumber,	
			COALESCE(cte2.InvoiceDate, rcd.CreationDate)  InvoiceDate,
			NULL Observation,
			tp.Nit ThirdPartyNit,
			tp.Name ThirdPartyName,
			NULL CityName,
			NULL Subtotal,	
			NULL DiscountValue,
			NULL PatientValue,
			NULL ValueTax,
			NULL TotalValue,
			NULL InvoicedUser,
			'Facturado' StatusName,
			---------- FILTROS ----------
			rcd.CareGroupId CareGroupId,
			NULL HealthAdministratorId,
			NULL InvoiceCategoryId,
			rc.AdmissionNumber,
			NULL PatientCode,
			tp.Id ThirdPartyId,
			NULL SucursalId,
			NULL RadicateInvoiceId,
			1 Status,
			NULL CurrencyId,
			NULL Abbreviation,
			rcd.IsMasterAccount IsMasterAccount,
			NULL CUV,
			RCD.RevenueControlDetailMasterId,
			rcd.Id as RevenueControlDetailId
	FROM Billing.RevenueControlDetail rcd
	JOIN Billing.RevenueControl rc ON rc.Id = rcd.RevenueControlId
	JOIN ADINGRESO ad ON ad.NUMINGRES = rc.AdmissionNumber
	JOIN Common.ThirdParty tp ON rcd.ThirdPartyId = tp.Id
	LEFT JOIN Cte_TRM cte2 ON cte2.RevenueControlDetailMasterId = rcd.Id
	WHERE rcd.IsMasterAccount = 3 AND ad.IESTADOIN = 'F'
	
	UNION ALL
	
	SELECT	CONCAT(8, '-', i.Id) Id,
			i.Id InvoiceId,
			8 DocumentType,
			NULL Code,
			i.InvoiceNumber,	
			COALESCE(i.InvoiceDate, rcd.CreationDate)  InvoiceDate,
			NULL Observation,
			tp.Nit ThirdPartyNit,
			tp.Name ThirdPartyName,
			NULL CityName,
			(i.ThirdPartySalesValue + i.ThirdPartyDiscountValue + i.TotalPatientWithDiscount)- i.ValueTax Subtotal,	
			i.ThirdPartyDiscountValue DiscountValue,
			i.TotalPatientWithDiscount PatientValue,
			i.ValueTax,
			iif(rcd.IsMasterAccount <>0,(i.ThirdPartySalesValue - i.TaxDevolutionValue),i.ThirdPartySalesValue) TotalValue,
			i.InvoicedUser,
			'Facturado' StatusName,
			---------- FILTROS ----------
			i.CareGroupId CareGroupId,
			i.HealthAdministratorId,
			i.InvoiceCategoryId,
			i.AdmissionNumber,
			i.PatientCode,
			tp.Id ThirdPartyId,
			NULL SucursalId,
			ri.Id RadicateInvoiceId,
			i.Status Status,
			c.Id CurrencyId,
			c.Abbreviation,
			rcd.IsMasterAccount IsMasterAccount,
			epro.CUV,
			RCD.RevenueControlDetailMasterId,
			rcd.Id
	FROM Billing.RevenueControlDetail rcd
	JOIN Billing.Invoice i ON i.RevenueControlDetailId = rcd.Id
	JOIN cte_company cte on 1=1
	JOIN Common.Currency c on c.Id = ISNULL(i.CurrencyId, cte.OfficialCurrencyId)
	JOIN Billing.RevenueControl rc ON rc.Id = rcd.RevenueControlId
	JOIN dbo.INPACIENT AS INP ON rc.PatientCode = INP.IPCODPACI 
	JOIN Common.ThirdParty tp ON i.ThirdPartyId = tp.Id
	LEFT JOIN Cte_TRM cte2 ON cte2.RevenueControlDetailMasterId = rcd.Id
	LEFT JOIN Portfolio.RadicateInvoiceD rid ON i.InvoiceNumber = rid.InvoiceNumber
	LEFT JOIN Portfolio.RadicateInvoiceC ri ON rid.RadicateInvoiceCId = ri.Id
	LEFT JOIN Billing.ElectronicsProperties epro ON epro.EntityId = i.Id
	WHERE i.DocumentType IN (1, 2, 3, 5) AND i.Status = 1 AND rcd.IsMasterAccount IN (2,4)) K
	WHERE K.DocumentType = 8