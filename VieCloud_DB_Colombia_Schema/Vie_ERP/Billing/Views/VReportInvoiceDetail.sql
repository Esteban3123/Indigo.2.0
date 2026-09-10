

CREATE VIEW [Billing].[VReportInvoiceDetail]
AS
	SELECT	CONCAT(id.Id, sods.Id) Id,
			id.InvoiceId,  
			sod.Id serviceOrderDetailId, 
			------------  INFORMACION DEL SERVICIO ------------
			bg.Code + ' - ' + bg.Name BillingGroup, 
			ips.Code,
			ce.Code CUPSCode, 
			ce.RIPSCode,
			NULL CodeAlternative, 
			NULL CodeAlternativeTwo, 
			NULL CodeCUM,
			NULL IUM,
			ips.Name, 
			ce.Description CUPSName, 
			ce.RIPSDescription RIPSName, 
			cd.Code as ContractDescriptionCode,
			cd.Name ContractDescriptionName,
			------------- INFORMACION DEL DETALLE -------------
			sod.ServiceDate, sod.AuthorizationNumber,
			sod.RecordType,
			sod.Presentation, id.DistributionType, '94' MeasuryUnit,
			IIF(sods.Id IS NULL, id.InvoicedQuantity, IIF(sodsf.Id IS NULL, 0, id.InvoicedQuantity)) InvoicedQuantity, 
			--------------*CAMPOS FACT(CO)*------------
			Id.TotalSalesPrice,
			IIF(sods.Id IS NULL, id.SubTotalPatientSalesPrice, IIF(sodsf.Id IS NULL, 0, id.SubTotalPatientSalesPrice))SubTotalPatientSalesPrice,
			IIF(sods.Id IS NULL, id.ThirdPartySalesPrice, IIF(sodsf.Id IS NULL, 0, id.ThirdPartySalesPrice))ThirdPartySalesPrice,
			Common.CurrencyConverterByModule(sod.GrossValue, cs.OfficialCurrencyId,ISNULL(i.CurrencyId,cs.OfficialCurrencyId),NULL,'Invoice', i.InvoiceDate) AS GrossValue,
			Id.ThirdPartyDiscount,
			-----------CAMPOS CUENTA MADRE-----------
			id.NetWorth,
			ROUND(id.NetWorth/id.InvoicedQuantity,2) NetUnitValue,
			(id.NetWorth + id.GrandTotalDiscount) GrossSubValue,
			ROUND((id.NetWorth + id.GrandTotalDiscount)/id.InvoicedQuantity,2) GrossUnitValue,
			IIF(rcd.IsMasterAccount =0	AND id.GrandTotalTaxes = 0,
			Common.CurrencyConverterByModule((sod.TaxValue * sod.InvoicedQuantity), cs.OfficialCurrencyId,ISNULL(i.CurrencyId,cs.OfficialCurrencyId),NULL,'Invoice', i.InvoiceDate),
			id.GrandTotalTaxes) AS IvaTotalValue,
			iif(gli.Id is not null , CONCAT(gli.Percentage, '%'), '') AS IvaPercentage,
			id.GrandTotalSalesPrice,
			id.GrandTotalDiscount,
			----------- DETALE QUIRURGICO -----------
			sods.Id surgicalId, ipss.Code CodeSurgical, ipss.Name NameSurgical, 
			sods.InvoicedQuantity QuantitySurgical, sods.TotalSalesPrice TotalSalesPriceSurgical,
			mp.CodeMipres,
			mp.IdMipres
	FROM Billing.Invoice i WITH (NOLOCK) 
	JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON i.Id = id.InvoiceId
	JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sod.Id = id.ServiceOrderDetailId 
	JOIN Contract.CUPSEntity ce WITH (NOLOCK) ON ce.Id = sod.CUPSEntityId 	
	JOIN Contract.IPSService ips WITH (NOLOCK) ON sod.IPSServiceId = ips.Id 
	JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = IIF(ISNULL(sod.ApplyRIAS, 0) = 1, ISNULL(ce.RIASBillingGroupId, ce.BillingGroupId), ce.BillingGroupId) 
	LEFT JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id=i.RevenueControlDetailId
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH (NOLOCK) ON sod.CUPSEntityContractDescriptionId = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd WITH (NOLOCK) ON cecd.ContractDescriptionId = cd.Id
	LEFT JOIN Billing.ServiceOrderDetailSurgical sods WITH (NOLOCK) ON sods.ServiceOrderDetailId = sod.Id AND sods.TotalSalesPrice > 0 AND sods.OnlyMedicalFees = 0 
	LEFT JOIN Contract.IPSService ipss WITH (NOLOCK) ON sods.IPSServiceId = ipss.Id	
	LEFT JOIN GeneralLedger.GeneralLedgerIVA gli ON gli.Id = ISNULL(id.TaxId,sod.IvaId) 
	LEFT JOIN 
	(
		SELECT sods.ServiceOrderDetailId, MIN (sods.Id) Id
		FROM Billing.ServiceOrderDetailSurgical sods WITH (NOLOCK) 
		WHERE sods.TotalSalesPrice > 0 AND sods.OnlyMedicalFees = 0 
		GROUP BY sods.ServiceOrderDetailId
	) sodsf ON sods.Id = sodsf.Id
	outer APPLY(select string_agg(mp.Code,',') CodeMipres, string_agg(mp.IdMipres,',') IdMipres from Billing.MipresCode mp where mp.ServiceOrderDetailId=sod.Id )  mp
	JOIN GeneralLedger.CompanySettings cs WITH(NOLOCK) on 1=1
	WHERE sod.IsDelete = 0 AND sod.SettlementType != 3 AND (i.ThirdPartySalesValue = 0 OR sod.GrandTotalSalesPrice > 0)
UNION ALL
	SELECT	CONCAT(id.Id,psd.Id) Id,
			id.InvoiceId,  
			iif(psd.Id is null, sod.Id,psd.Id)  serviceOrderDetailId, 
			------------  INFORMACION DEL SERVICIO ------------
			bg.Code + ' - ' + bg.Name BillingGroup, 
			iif(ips.id is not null,ips.Code,ip.Code) Code,
			cups.Code CUPSCode, 
			cups.RIPSCode,
			ip.CodeAlternative, 
			ip.CodeAlternativeTwo, 
			COALESCE(NULLIF(LTRIM(RTRIM(ip.IUM)), ''), NULLIF(LTRIM(RTRIM(ip.CodeCUM)), ''), ip.Code) CodeCUM,
			ip.IUM,
			iif(ips.id is not null,ips.Name,ip.Name) Name, 
			cups.Description CUPSName, 
			cups.RIPSDescription RIPSName, 
			cd.Code as ContractDescriptionCode,
			cd.Name ContractDescriptionName,
			------------- INFORMACION DEL DETALLE -------------
			sod.ServiceDate, sod.AuthorizationNumber,
			IIF(psd.CUPSEntityId IS NULL,sod.RecordType,1) RecordType,
			sod.Presentation,
			id.DistributionType, '94' MeasuryUnit,
			sod.InvoicedQuantity InvoicedQuantity, 
			---------------VARIACION POR TIPO DE MONEDA------------
			IIF(psd.Id IS NULL,	id.TotalSalesPrice,
								Common.CurrencyConverterWithDate((	psd.Price-((psd.Price*psd.DiscountPercentage)/100)),
																	cs.OfficialCurrencyId,isnull(i.CurrencyId,cs.OfficialCurrencyId),i.InvoiceDate)) TotalSalesPrice,
			iif(psd.Id IS NULL,id.SubTotalPatientSalesPrice,(((psd.Price-((psd.Price*psd.DiscountPercentage)/100))*PatientPercentage)/100)*sod.InvoicedQuantity) AS SubTotalPatientSalesPrice,
			iif(psd.Id is null,id.ThirdPartySalesPrice,(sod.InvoicedQuantity*(psd.Price-((psd.Price*psd.DiscountPercentage)/100)))) AS ThirdPartySalesPrice,
			Common.CurrencyConverterWithDate(sod.GrossValue, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId), i.InvoiceDate) AS GrossValue,
			IIF(psd.Id IS NULL,	id.ThirdPartyDiscount,
								Common.CurrencyConverterWithDate(((	psd.DiscountPercentage*psd.Price)/100),
																	cs.OfficialCurrencyId,ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.InvoiceDate)) AS ThirdPartyDiscount,

			-----------CAMPOS CUENTA MADRE-----------
			id.NetWorth AS NetWorth,
			ROUND(id.NetWorth/id.InvoicedQuantity,2) NetUnitValue,
			(id.NetWorth + id.GrandTotalDiscount) GrossSubValue,
			ROUND((id.NetWorth + id.GrandTotalDiscount)/id.InvoicedQuantity,2) GrossUnitValue,
			IIF(rcd.IsMasterAccount=0 AND id.GrandTotalTaxes = 0,
				Common.CurrencyConverterWithDate((sod.TaxValue * sod.InvoicedQuantity), cs.OfficialCurrencyId,ISNULL(i.CurrencyId,cs.OfficialCurrencyId), i.InvoiceDate),
				id.GrandTotalTaxes) AS IvaTotalValue,
			iif(gli.Id is not null , CONCAT(gli.Percentage, '%'), '') AS IvaPercentage,
			id.GrandTotalSalesPrice,
			id.GrandTotalDiscount,
			----------- DETALLE QUIRURGICO -----------
			NULL surgicalId, NULL CodeSurgical, NULL NameSurgical, 
			NULL QuantitySurgical, NULL TotalSalesPriceSurgical,
			mp.CodeMipres,
			mp.IdMipres
	FROM Billing.Invoice i WITH (NOLOCK) 
	JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON i.Id = id.InvoiceId
	JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sod.Id = id.ServiceOrderDetailId 
	JOIN Inventory.InventoryProduct Ip WITH (NOLOCK) ON Ip.Id = sod.ProductId 
	LEFT JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) on rcd.Id=i.RevenueControlDetailId
	LEFT JOIN Billing.ProductServiceDetail psd WITH(NOLOCK) ON sod.Id = psd.ServiceOrderDetailId
	LEFT JOIN Inventory.InventoryProduct ips WITH(NOLOCK) ON psd.ProductId=ips.Id
	LEFT JOIN Contract.CUPSEntity cups WITH(NOLOCK) ON psd.CUPSEntityId=cups.Id
	LEFT JOIN Contract.ContractDescriptions cd WITH(NOLOCK) ON psd.ContractDescriptionsId = cd.Id
	LEFT JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = IIF(psd.CUPSEntityId IS NULL, Ip.BillingGroupId, cups.BillingGroupId)
	LEFT JOIN GeneralLedger.GeneralLedgerIVA gli ON gli.Id = ISNULL(id.TaxId,sod.IvaId) 
	outer APPLY(select string_agg(mp.Code,',') CodeMipres, string_agg(mp.IdMipres,',') IdMipres from Billing.MipresCode mp where mp.ServiceOrderDetailId=sod.Id )  mp
	JOIN GeneralLedger.CompanySettings cs WITH(NOLOCK) on 1=1
	WHERE sod.IsDelete = 0 AND sod.SettlementType != 3 AND (i.ThirdPartySalesValue = 0 OR sod.GrandTotalSalesPrice > 0)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte detallado de líneas de facturación que consolida, para cada factura emitida, la información completa de cada servicio o procedimiento cobrado: grupo de facturación, código del servicio propio de la IPS, código CUPS y descripción RIPS, descripción del contrato, fecha de atención, número de autorización, cantidades facturadas, valores de venta brutos y netos, distribución entre tercero pagador (EPS, aseguradora) y paciente, descuentos, impuestos (IVA), detalle quirúrgico (cuando aplica) y códigos MIPRES de prescripción. Integra datos de la factura (Billing.Invoice), el detalle de facturación (Billing.InvoiceDetail), el detalle de la orden de servicio (Billing.ServiceOrderDetail), el catálogo CUPS (Contract.CUPSEntity), los servicios propios de la IPS (Contract.IPSService), los grupos de facturación (Billing.BillingGroup), las descripciones de contrato (Contract.ContractDescriptions) y el detalle quirúrgico (Billing.ServiceOrderDetailSurgical), realizando conversión de moneda cuando la factura está en divisa distinta a la moneda oficial. Se usa para reportería de facturación, conciliación de cuentas, generación de RIPS, análisis de ingresos por servicio y auditoría de glosas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VReportInvoiceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VReportInvoiceDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida el detalle de líneas de factura (servicios CUPS/IPS y productos/medicamentos) con información del servicio, detalle quirúrgico, IVA, conversión de moneda y códigos MIPRES asociados.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El detalle de la orden de servicio no debe estar marcado como eliminado (sod.IsDelete = 0).; El tipo de liquidación del detalle no debe ser 3 (sod.SettlementType != 3).; La factura debe tener ThirdPartySalesValue = 0 o el detalle debe tener GrandTotalSalesPrice > 0.; Para evitar división por cero, id.InvoicedQuantity debe ser distinto de 0 (usado en NetUnitValue y GrossUnitValue).; Debe existir configuración en GeneralLedger.CompanySettings para resolver la moneda oficial.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen detalles de orden de servicio NO eliminados y con SettlementType distinto de 3.; Solo se reportan líneas cuando la factura no tiene venta a terceros (ThirdPartySalesValue=0) o cuando el detalle tiene valor total de venta positivo.; Solo se consideran detalles quirúrgicos con TotalSalesPrice > 0 y OnlyMedicalFees = 0.; Para evitar duplicación de valores monetarios cuando una orden tiene múltiples detalles quirúrgicos, sólo el de menor Id (sodsf) conserva los importes; los demás se reportan en 0.; La unidad de medida reportada siempre es ''94'' (constante).; NetUnitValue y GrossUnitValue se redondean a 2 decimales.; GrossSubValue siempre equivale a NetWorth + GrandTotalDiscount.; Los valores GrossValue, IvaTotalValue y, cuando aplica, TotalSalesPrice/ThirdPartyDiscount/SubTotalPatientSalesPrice se convierten desde la moneda oficial de la compañía a la moneda de la factura usando la fecha de la factura.; Los códigos MIPRES se concatenan con coma como agregación por orden de servicio.; El segundo SELECT no entrega información quirúrgica (campos quirúrgicos siempre NULL).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Detalle de factura; Orden de servicio; CUPS; RIPS; IPS; Grupo de facturación; RIAS (Rutas Integrales de Atención en Salud); Cuenta madre / Master Account; IVA; Detalle quirúrgico; MIPRES; CUM (medicamentos); Conversión de moneda; Descuento de tercero; Porcentaje paciente / tercero; Autorización', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.VReportInvoiceDetail: Devuelve la unión de dos conjuntos: (1) líneas de factura asociadas a servicios CUPS/IPS con su detalle quirúrgico, y (2) líneas asociadas a productos de inventario con su detalle de producto-servicio (psd).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si sod.ApplyRIAS = 1 (con ISNULL=0) → Usa ce.RIASBillingGroupId como grupo de facturación; si es NULL cae a ce.BillingGroupId else Usa ce.BillingGroupId; si sods.Id IS NULL (no existe detalle quirúrgico) en el primer SELECT → InvoicedQuantity, SubTotalPatientSalesPrice y ThirdPartySalesPrice se toman directamente de InvoiceDetail else Si sodsf.Id IS NULL (no es la fila quirúrgica mínima por orden) se devuelven 0 para esos campos; en caso contrario se devuelven los valores de InvoiceDetail (evita duplicar valores cuando hay múltiples quirúrgicos); si rcd.IsMasterAccount = 0 AND id.GrandTotalTaxes = 0 → IvaTotalValue se calcula como sod.TaxValue * sod.InvoicedQuantity convertido a la moneda de la factura else IvaTotalValue = id.GrandTotalTaxes; si psd.CUPSEntityId IS NULL (segundo SELECT) → RecordType = sod.RecordType y BillingGroup se toma de Ip.BillingGroupId (producto de inventario) else RecordType se fuerza a 1 y BillingGroup se toma de cups.BillingGroupId; si psd.Id IS NULL en segundo SELECT → TotalSalesPrice, SubTotalPatientSalesPrice, ThirdPartySalesPrice y ThirdPartyDiscount se toman de InvoiceDetail else Se recalculan a partir de psd.Price, psd.DiscountPercentage y PatientPercentage, con conversión de moneda por fecha de factura; si ip.CodeCUM es vacío o NULL (tras LTRIM/RTRIM) → CodeCUM se reemplaza por ip.Code else Se conserva ip.CodeCUM; si gli.Id IS NOT NULL → IvaPercentage = CONCAT(gli.Percentage, ''%'') else IvaPercentage = '''' (cadena vacía); si ips.Id IS NOT NULL en segundo SELECT → Code y Name se toman del IPSService asociado al producto del psd else Code y Name se toman del producto de inventario (ip)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.CurrencyConverterByModule; Common.CurrencyConverterWithDate', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Contract.CUPSEntity; Contract.IPSService; Billing.BillingGroup; Billing.RevenueControlDetail; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Billing.ServiceOrderDetailSurgical; GeneralLedger.GeneralLedgerIVA; Billing.MipresCode; GeneralLedger.CompanySettings; Inventory.InventoryProduct; Billing.ProductServiceDetail', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetail';
GO
