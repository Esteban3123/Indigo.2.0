

CREATE VIEW [Billing].[ViewReportQuotationDetails]
AS
	SELECT	CONCAT('QuotationServiceOrderDetail-',qsod.Id,'-',qsods.Id) Id,
			q.Id QuotationId,
			qsod.Id DetailId, 
			------------  INFORMACION DEL SERVICIO ------------
			bg.Code + ' - ' + bg.Name BillingGroup, 
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
			qsod.ServiceDate, qsod.AuthorizationNumber, qsod.RecordType, qsod.Presentation,
			IIF(qsods.Id IS NULL, qsod.InvoicedQuantity, IIF(qsodsf.Id IS NULL, 0, qsod.InvoicedQuantity)) InvoicedQuantity, 
			qsod.TotalSalesPrice, qsod.ThirdPartyDiscount, 
			IIF(qsods.Id IS NULL, qsod.GrandTotalSalesPrice, IIF(qsodsf.Id IS NULL, 0, qsod.GrandTotalSalesPrice)) ThirdPartySalesPrice, 
			----------------- DETALE QUIRURGICO ---------------
			qsods.Id SurgicalId, ipss.Code CodeSurgical, SUBSTRING(ipss.Name, 1, 100) NameSurgical, 
			qsods.InvoicedQuantity QuantitySurgical, qsods.TotalSalesPrice TotalSalesPriceSurgical
	FROM Billing.Quotation q WITH (NOLOCK)
	JOIN Billing.QuotationServiceOrderDetail qsod WITH (NOLOCK) ON qsod.QuotationId = q.Id
	JOIN Contract.CUPSEntity ce WITH (NOLOCK) ON ce.Id = qsod.CUPSEntityId
	LEFT JOIN Contract.IPSService ips WITH (NOLOCK) ON qsod.IPSServiceId = ips.Id
	JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = IIF(ISNULL(qsod.ApplyRIAS, 0) = 1, ISNULL(ce.RIASBillingGroupId, ce.BillingGroupId), ce.BillingGroupId)
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH (NOLOCK) ON qsod.CUPSEntityContractDescriptionId = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd WITH (NOLOCK) ON cecd.ContractDescriptionId = cd.Id
	LEFT JOIN Billing.QuotationServiceOrderDetailSurgical qsods WITH (NOLOCK) ON qsods.QuotationServiceOrderDetailId = qsod.Id
	LEFT JOIN Contract.IPSService ipss WITH (NOLOCK) ON qsods.IPSServiceId = ipss.Id
	LEFT JOIN 
	(
		SELECT qsods.QuotationServiceOrderDetailId, MIN(qsods.Id) Id
		FROM Billing.QuotationServiceOrderDetailSurgical qsods WITH (NOLOCK)
		GROUP BY qsods.QuotationServiceOrderDetailId
	) qsodsf ON qsods.Id = qsodsf.Id
UNION ALL
	SELECT	CONCAT('QuotationPharmaceuticalDispensingDetail-',qpdd.Id) Id,
			q.Id QuotationId,
			qpdd.Id DetailId, 
			------------  INFORMACION DEL SERVICIO ------------
			bg.Code + ' - ' + bg.Name BillingGroup, 
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
			qpdd.ServiceDate, qpdd.AuthorizationNumber, 2 RecordType, NULL Presentation,
			qpdd.Quantity InvoicedQuantity, 
			qpdd.SalePrice TotalSalesPrice, qpdd.DiscountValue ThirdPartyDiscount, 
			qpdd.GrandTotalSalesPrice ThirdPartySalesPrice, 
			----------------- DETALE QUIRURGICO ---------------
			NULL SurgicalId, NULL CodeSurgical, NULL NameSurgical, 
			NULL QuantitySurgical, NULL TotalSalesPriceSurgical	
	FROM Billing.Quotation q WITH (NOLOCK)
	JOIN Billing.QuotationPharmaceuticalDispensingDetail qpdd WITH (NOLOCK) ON qpdd.QuotationId = q.Id
	JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON ip.Id = qpdd.ProductId
	LEFT JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = ip.BillingGroupId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte que consolida el detalle completo de los ítems incluidos en una cotización de facturación, integrando dos tipos de líneas: servicios de salud (procedimientos, exámenes y consultas con sus códigos CUPS, RIPS, grupo de facturación, descripción de contrato y eventualmente detalle quirúrgico) y medicamentos o productos farmacéuticos dispensados (con código CUM, precio de venta, descuentos y cantidades). Combina información de cotizaciones, órdenes de servicio, catálogos de servicios IPS y CUPS, grupos de facturación, contratos y el inventario de productos, permitiendo generar reportes detallados de lo cotizado por ítem para análisis de facturación, presupuesto y autorización. Es la fuente principal para consultas como ''¿qué servicios y medicamentos están incluidos en una cotización?'', incluyendo valores de venta, descuentos, cantidades facturadas y datos quirúrgicos asociados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewReportQuotationDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewReportQuotationDetails';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un solo conjunto los detalles facturables de una cotización: ítems de orden de servicio (con su detalle quirúrgico) e ítems de dispensación farmacéutica, normalizando códigos, nombres y grupos de facturación para reportes.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportQuotationDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las cotizaciones deben existir en Billing.Quotation con sus detalles de servicio (QuotationServiceOrderDetail) o de dispensación farmacéutica (QuotationPharmaceuticalDispensingDetail); Cada detalle de servicio debe tener un CUPSEntity asociado vía CUPSEntityId; Cada detalle farmacéutico debe tener un InventoryProduct asociado vía ProductId; El CUPSEntity debe tener configurado un BillingGroupId (y RIASBillingGroupId si ApplyRIAS=1) para resolver el grupo de facturación', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportQuotationDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador de la fila resultante se construye con prefijo ''QuotationServiceOrderDetail-'' o ''QuotationPharmaceuticalDispensingDetail-'' para distinguir el origen del detalle; Para servicios, RecordType proviene del detalle; para medicamentos siempre se asigna RecordType = 2; Cuando un detalle de servicio tiene múltiples líneas quirúrgicas asociadas, solo la línea quirúrgica con el menor Id conserva los valores de cantidad y total facturado del detalle padre; las demás los reportan en 0 para no duplicar; El nombre del servicio IPS se trunca a 94 caracteres y la descripción CUPS también a 94; el nombre quirúrgico a 100; Para medicamentos no aplican CUPS, RIPS, descripción de contrato ni detalle quirúrgico (se devuelven NULL); El BillingGroup se concatena como ''Code - Name''; Si el producto farmacéutico no tiene CodeCUM, se usa su Code como sustituto del CUM', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportQuotationDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cotización; Detalle de orden de servicio; Detalle quirúrgico; Dispensación farmacéutica; CUPS; RIPS; RIAS; Grupo de facturación; CUM (medicamentos); Descripción de contrato; Número de autorización; IPS Service', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportQuotationDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Une mediante UNION ALL los detalles de QuotationServiceOrderDetail (con LEFT JOIN a su detalle quirúrgico) y los detalles de QuotationPharmaceuticalDispensingDetail por cada Quotation', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportQuotationDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(qsod.ApplyRIAS,0) = 1 → Toma el grupo de facturación RIAS de CUPSEntity (RIASBillingGroupId) else Toma el grupo de facturación estándar de CUPSEntity (BillingGroupId); si Existe detalle quirúrgico (qsods.Id IS NOT NULL) y NO es el primer registro quirúrgico (qsodsf.Id IS NULL) → InvoicedQuantity y ThirdPartySalesPrice se reportan como 0 para evitar duplicar valores del detalle padre else Se reporta la cantidad y total de venta del detalle de orden original; si LTRIM(RTRIM(ISNULL(ip.CodeCUM,''''))) = '''' → CodeCUM se sustituye por el Code del producto else Se usa el CodeCUM real del producto', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportQuotationDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Quotation; Billing.QuotationServiceOrderDetail; Contract.CUPSEntity; Contract.IPSService; Billing.BillingGroup; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Billing.QuotationServiceOrderDetailSurgical; Billing.QuotationPharmaceuticalDispensingDetail; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportQuotationDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportQuotationDetails';
GO
