

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
	LEFT JOIN Billing.RevenueControlDetail rcd ON rcd.Id = i.RevenueControlDetailId
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte consolidado de facturas de venta emitidas por la institución. Integra facturas facturadas y anuladas (tipos de documento 1, 2, 3, 4 y 5) con sus terceros pagadores (EPS, aseguradoras, empresas), moneda de cobro, valores subtotal, descuentos, valor del paciente, impuestos y total liquidado. Combina la información de encabezado de factura, detalle de control de ingresos, radicación de cartera ante entidades pagadoras y el código único de validación electrónica (CUV), permitiendo reportería de listado de facturas para seguimiento de cobro, cartera y facturación electrónica. Cada fila representa una factura o nota (facturada o anulada) con su estado, número de ingreso, código del paciente, tercero responsable y radicado de cartera asociado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VReportListInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VReportListInvoice';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada para reporte de listado de facturación que unifica facturas activas, anuladas, notas crédito, facturación básica, ventas de productos y cuentas maestras en un único conjunto comparable.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportListInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en GeneralLedger.CompanySettings con OfficialCurrencyId definido (TOP 1) para resolver la moneda por defecto.; Para el bloque DocumentType=6, la factura básica debe tener una dirección (Common.Address) y ciudad (Common.City) asociadas (JOIN obligatorio).; Para el bloque DocumentType=7, el registro Inventory.DocumentInvoiceProductSales debe tener un ThirdPartyId válido en Common.ThirdParty (JOIN obligatorio).; Para el bloque de cuentas maestras (DocumentType=8 con IsMasterAccount=3), debe existir el ingreso en ADINGRESO con NUMINGRES correspondiente al AdmissionNumber del control de ingresos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportListInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.VReportListInvoice: Bloque 1 (Facturado): retorna facturas con DocumentType IN (1,2,3,5) AND Status=1, etiqueta StatusName=''Facturado'', Id prefijado con ''1'' y TotalValue ajustado por TaxDevolutionValue cuando rcd.IsMasterAccount<>0.; [RETURN_RESULT] Billing.VReportListInvoice: Bloque 2 (Anulado): retorna facturas con DocumentType IN (1,2,3,5) AND Status=2, usa AnnulmentDate como InvoiceDate, etiqueta StatusName=''Anulado'', Id prefijado con ''2'' y fuerza IsMasterAccount=1.; [RETURN_RESULT] Billing.VReportListInvoice: Bloque 3 (Notas): retorna documentos con DocumentType=4 (notas crédito/débito), con Subtotal y TotalValue calculados según rcd.IsMasterAccount<>0 y StatusName derivado del Status (1=''Facturado'', 2=''Anulado'').; [RETURN_RESULT] Billing.VReportListInvoice: Bloque 4 (Facturación básica): retorna documentos con DocumentType=6 tomando valores (Code, Description, Value, ValueDiscount, ValueIVA, TotalValue) desde Billing.BasicBilling, con CityName resuelto vía Address→City y PatientValue=0.; [RETURN_RESULT] Billing.VReportListInvoice: Bloque 5 (Venta de productos): retorna documentos con DocumentType=7 tomando valores desde Inventory.DocumentInvoiceProductSales, con SucursalId desde Payroll.BranchOffice, CityName de la sucursal y PatientValue=0.; [RETURN_RESULT] Billing.VReportListInvoice: Bloque 6 (Cuenta maestra sin factura): cuando rcd.IsMasterAccount=3 y ADINGRESO.IESTADOIN=''F'', retorna DocumentType=8 con la fecha COALESCE(InvoiceDate de factura hija vía Cte_TRM, rcd.CreationDate) y todos los valores monetarios en NULL.; [RETURN_RESULT] Billing.VReportListInvoice: Bloque 6 (Cuenta maestra con factura hija): cuando i.DocumentType IN (1,2,3,5) AND i.Status=1 AND rcd.IsMasterAccount IN (2,4), retorna DocumentType=8 con los valores de la factura hija y StatusName=''Facturado''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportListInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si i.DocumentType IN (1,2,3,5) AND i.Status=1 → Se incluye la factura como ''Facturado'' con InvoiceDate real y aplicando ajuste de TaxDevolutionValue si la cuenta es maestra (IsMasterAccount<>0).; si i.DocumentType IN (1,2,3,5) AND i.Status=2 → Se incluye la factura como ''Anulado'', usando AnnulmentDate en lugar de InvoiceDate y forzando IsMasterAccount=1.; si i.DocumentType=4 → Se incluye como nota con Subtotal y TotalValue calculados condicionalmente: si rcd.IsMasterAccount<>0 se resta ValueTax y TaxDevolutionValue; en caso contrario se usan los valores brutos.; si i.DocumentType=6 → Se obtiene el detalle desde Billing.BasicBilling y la ciudad desde Common.Address→Common.City; la moneda usa COALESCE(bb.CurrencyId, i.CurrencyId, OfficialCurrencyId).; si i.DocumentType=7 → Se obtiene el detalle desde Inventory.DocumentInvoiceProductSales y la sucursal/ciudad desde Payroll.BranchOffice.; si rcd.IsMasterAccount=3 AND ADINGRESO.IESTADOIN=''F'' → Se publica una fila tipo cuenta maestra (DocumentType=8) sin valores monetarios, representando el agregado del ingreso facturado.; si rcd.IsMasterAccount IN (2,4) AND factura hija con Status=1 → Se publica como DocumentType=8 reutilizando los valores de la factura hija ligada al detalle maestro.; si i.CurrencyId IS NULL → Se reemplaza por OfficialCurrencyId proveniente de GeneralLedger.CompanySettings (ISNULL/COALESCE).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportListInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportListInvoice';
GO
