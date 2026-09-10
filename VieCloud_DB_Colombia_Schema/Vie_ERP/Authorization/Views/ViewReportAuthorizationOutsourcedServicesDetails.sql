

CREATE VIEW [Authorization].[ViewReportAuthorizationOutsourcedServicesDetails]
AS
	SELECT	CONCAT('AuthorizationOutsourcedServicesServiceOrderDetail-',aossod.Id,'-',aossods.Id) Id,
			aos.Id AuthorizationOutsourcedServicesId,
			aossod.Id DetailId, 
			------------  INFORMACION DEL SERVICIO ------------
			bg.Code + ' - ' + bg.Name BillingGroup, 
			ISNULL(fu.Code + ' - ' + fu.Name, '') AS FunctionalUnitCodeName, 
			ips.Code,
			ce.Code CUPSCode, 
			ce.RIPSCode,
			NULL CodeAlternative, 
			NULL CodeAlternativeTwo, 
			NULL CodeCUM,
			SUBSTRING(ips.Name, 1,94) Name, 
			SUBSTRING(ce.Description, 1, 94) CUPSName, 
			ce.RIPSDescription RIPSName, 
			cd.Name ContractDescriptionName,
			------------- INFORMACION DEL DETALLE -------------
			aossod.ServiceDate, aossod.AuthorizationNumber, aossod.RecordType, aossod.Presentation,
			IIF(aossods.Id IS NULL, aossod.InvoicedQuantity, IIF(qsodsf.Id IS NULL, 0, aossod.InvoicedQuantity)) InvoicedQuantity, 
			aossod.TotalSalesPrice, aossod.ThirdPartyDiscount, 
			IIF(aossods.Id IS NULL, aossod.GrandTotalSalesPrice, IIF(qsodsf.Id IS NULL, 0, aossod.GrandTotalSalesPrice)) ThirdPartySalesPrice, 
			----------------- DETALE QUIRURGICO ---------------
			aossods.Id SurgicalId, ipss.Code CodeSurgical, SUBSTRING(ipss.Name, 1, 100) NameSurgical, 
			aossods.InvoicedQuantity QuantitySurgical, aossods.TotalSalesPrice TotalSalesPriceSurgical
	FROM [Authorization].AuthorizationOutsourcedServices aos WITH (NOLOCK)
	JOIN [Authorization].AuthorizationOutsourcedServicesServiceOrderDetail aossod WITH (NOLOCK) ON aossod.AuthorizationOutsourcedServicesId = aos.Id
	JOIN Contract.CUPSEntity ce WITH (NOLOCK) ON ce.Id = aossod.CUPSEntityId
	LEFT JOIN Contract.IPSService ips WITH (NOLOCK) ON aossod.IPSServiceId = ips.Id
	JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = IIF(ISNULL(aossod.ApplyRIAS, 0) = 1, ISNULL(ce.RIASBillingGroupId, ce.BillingGroupId), ce.BillingGroupId)
	LEFT JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON aossod.PerformsFunctionalUnitId = fu.Id
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH (NOLOCK) ON aossod.CUPSEntityContractDescriptionId = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd WITH (NOLOCK) ON cecd.ContractDescriptionId = cd.Id
	LEFT JOIN [Authorization].AuthorizationOutsourcedServicesServiceOrderDetailSurgical aossods WITH (NOLOCK) ON aossods.AuthorizationOutsourcedServicesServiceOrderDetailId = aossod.Id
	LEFT JOIN Contract.IPSService ipss WITH (NOLOCK) ON aossods.IPSServiceId = ipss.Id	
	LEFT JOIN 
	(
		SELECT aossods.QuotationServiceOrderDetailId, MIN(aossods.Id) Id
		FROM Billing.QuotationServiceOrderDetailSurgical aossods WITH (NOLOCK)
		GROUP BY aossods.QuotationServiceOrderDetailId
	) qsodsf ON aossods.Id = qsodsf.Id
UNION ALL
	SELECT	CONCAT('AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail-',aospdd.Id) Id,
			aos.Id AuthorizationOutsourcedServicesId,
			aospdd.Id DetailId, 
			------------  INFORMACION DEL SERVICIO ------------
			bg.Code + ' - ' + bg.Name BillingGroup, 
			ISNULL(fu.Code + ' - ' + fu.Name, '') AS FunctionalUnitCodeName, 
			ip.Code,
			NULL CUPSCode, 
			NULL RIPSCode,
			ip.CodeAlternative, 
			ip.CodeAlternativeTwo, 
			case LTRIM(RTRIM(isnull(ip.CodeCUM,''))) when '' then ip.Code else ip.CodeCUM end CodeCUM, 
			ip.Name, 
			NULL CUPSName, 
			NULL RIPSName, 
			NULL ContractDescriptionName,
			------------- INFORMACION DEL DETALLE -------------
			aospdd.ServiceDate, aospdd.AuthorizationNumber, 2 RecordType, NULL Presentation,
			aospdd.Quantity InvoicedQuantity, 
			aospdd.SalePrice TotalSalesPrice, aospdd.DiscountValue ThirdPartyDiscount, 
			aospdd.GrandTotalSalesPrice ThirdPartySalesPrice, 
			----------------- DETALE QUIRURGICO ---------------
			NULL SurgicalId, NULL CodeSurgical, NULL NameSurgical, 
			NULL QuantitySurgical, NULL TotalSalesPriceSurgical	
	FROM [Authorization].AuthorizationOutsourcedServices aos WITH (NOLOCK)
	JOIN [Authorization].AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail aospdd WITH (NOLOCK) ON aospdd.AuthorizationOutsourcedServicesId = aos.Id
	JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON ip.Id = aospdd.ProductId
	JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = ip.BillingGroupId
	LEFT JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON aospdd.FunctionalUnitId = fu.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte que consolida el detalle completo de los servicios tercerizados (outsourcing) autorizados, combinando dos tipos de ítems: servicios y procedimientos (con su código CUPS, descripción RIPS, grupo de facturación, unidad funcional ejecutante, descripción de contrato y detalle quirúrgico cuando aplica) y medicamentos o productos farmacéuticos dispensados (con sus códigos CUM, alternativo y de inventario). Une las autorizaciones de outsourcing con sus líneas de detalle de orden de servicio y sus líneas de dispensación farmacéutica, aplicando la lógica de grupo de facturación RIAS cuando corresponde. Se usa para reportería y auditoría de facturación de servicios tercerizados, permitiendo revisar cantidades facturadas, precios de venta, descuentos a terceros y el desglose quirúrgico por autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewReportAuthorizationOutsourcedServicesDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewReportAuthorizationOutsourcedServicesDetails';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único conjunto de filas el detalle de servicios y dispensaciones farmacéuticas asociados a autorizaciones de servicios tercerizados, para alimentar reportes con información del servicio, detalle facturado y componente quirúrgico.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewReportAuthorizationOutsourcedServicesDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada detalle de orden de servicio (aossod) debe tener un CUPSEntity válido (JOIN obligatorio) y una autorización tercerizada padre (aos).; Cada detalle de dispensación farmacéutica (aospdd) debe tener un InventoryProduct válido y un BillingGroup asociado al producto.; El BillingGroup del servicio se determina por ApplyRIAS: si está activo, se requiere RIASBillingGroupId o se usa BillingGroupId como fallback.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewReportAuthorizationOutsourcedServicesDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El Id de salida es siempre único y compuesto, prefijado según el origen (servicio u orden quirúrgica vs. dispensación farmacéutica) e incluye los Ids fuente.; Para dispensaciones farmacéuticas, los campos CUPSCode, RIPSCode, CUPSName, RIPSName, ContractDescriptionName, Presentation y todos los campos quirúrgicos siempre se devuelven en NULL.; Para servicios, CodeAlternative, CodeAlternativeTwo y CodeCUM siempre se devuelven en NULL.; Cuando existen múltiples filas en QuotationServiceOrderDetailSurgical para un mismo QuotationServiceOrderDetailId, sólo la de menor Id se considera ''válida'' para conservar valores facturados; las demás llevan cantidades y totales en cero.; Los nombres se truncan: Name a 94 caracteres, CUPSName a 94, NameSurgical a 100.; FunctionalUnitCodeName devuelve cadena vacía cuando no hay unidad funcional asignada.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewReportAuthorizationOutsourcedServicesDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autorización de servicios tercerizados (outsourcing); Procedimiento CUPS; RIPS; RIAS (Rutas Integrales de Atención en Salud); Grupo de facturación; Unidad funcional; Servicio IPS; Detalle quirúrgico; Dispensación farmacéutica; Código CUM de medicamento; Descripción de contrato; Cotización de orden de servicio', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewReportAuthorizationOutsourcedServicesDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Une (UNION ALL) dos conjuntos: (1) detalles de orden de servicio con posible desglose quirúrgico y (2) detalles de dispensación farmacéutica, cada uno con un Id sintético prefijado (''AuthorizationOutsourcedServicesServiceOrderDetail-...'' o ''AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail-...'').', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewReportAuthorizationOutsourcedServicesDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(aossod.ApplyRIAS,0) = 1 → El BillingGroup se toma de ce.RIASBillingGroupId (con fallback a ce.BillingGroupId si es NULL). else El BillingGroup se toma de ce.BillingGroupId.; si aossods.Id IS NULL (no hay detalle quirúrgico asociado al detalle) → Se reportan InvoicedQuantity y GrandTotalSalesPrice originales del detalle (aossod). else Si existe detalle quirúrgico pero qsodsf.Id IS NULL (no es el mínimo de la cotización quirúrgica), InvoicedQuantity y ThirdPartySalesPrice se fuerzan a 0; en caso contrario se conservan los valores originales.; si LTRIM(RTRIM(ISNULL(ip.CodeCUM,''''))) = '''' en dispensación farmacéutica → CodeCUM toma el valor de ip.Code. else CodeCUM toma el valor de ip.CodeCUM.; si Origen del registro → RecordType proviene de aossod.RecordType para servicios; se fija en 2 para dispensación farmacéutica.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewReportAuthorizationOutsourcedServicesDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.AuthorizationOutsourcedServices; Authorization.AuthorizationOutsourcedServicesServiceOrderDetail; Authorization.AuthorizationOutsourcedServicesServiceOrderDetailSurgical; Authorization.AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail; Contract.CUPSEntity; Contract.IPSService; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Billing.BillingGroup; Billing.QuotationServiceOrderDetailSurgical; Payroll.FunctionalUnit; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewReportAuthorizationOutsourcedServicesDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewReportAuthorizationOutsourcedServicesDetails';
GO
