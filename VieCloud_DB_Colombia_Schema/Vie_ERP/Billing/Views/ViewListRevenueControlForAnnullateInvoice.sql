
CREATE VIEW [Billing].[ViewListRevenueControlForAnnullateInvoice]
AS
--ROW_NUMBER() OVER (ORDER BY BID.Id ASC) AS Row,
SELECT        
	COALESCE(BID.Id, -1) AS Id, BI.PatientCode,
	SO.Id AS ServiceOrderId,SOD.Presentation, 
	SOD.SurgeryNumber,
	SOD.Id AS ServiceOrderDetailId, 
	- 1 AS RevenueControlDetailId,
	BI.Observation, 
	BI.InvoiceCategoryId, 
	COALESCE (BI.InvoiceDate, '') AS InvoiceDate,
	COALESCE (BI.InvoicedUser, '') AS InvoicedUser, 
	CASE 
		WHEN BI.InvoiceCategoryId IS NULL THEN '' 
		ELSE CONCAT(IC.Code, ' - ', IC.Name)
	END  as InvoiceCategoryCodeName,
	SO.Code AS ServiceOrderCode, 
	BI.AdmissionNumber, 
	- 1 AS BillingAuthorizationId, 
	COALESCE (BI.Id, - 1) AS InvoiceId, 
	COALESCE (BI.InvoiceNumber, '') AS InvoiceNumber, 
	COALESCE (SOD.RecordType, 1) AS RecordType,
	P.POSProduct as IsPOSProduct,
	-1 AS FolioOrder,
	CG.CareGroupType AS FolioType, 
	-1 AS ContractEntityId, 
	'' AS ContractEntityCodeName,
	COALESCE (HA.Id, 0) AS HealthAdministratorId, 
	CONCAT(CONCAT(COALESCE (HA.Code, ''), ' - '), 
	COALESCE (HA.Name, '')) AS HealthAdministratorCodeName, 
	TP.Id AS ThirdPartyId, 
	CONCAT(CONCAT(TP.Nit, ' - '), TP.Name) AS ThirdPartyNitName, 
	CG.Id AS CareGroupId,
	CG.EntityType as CaregroupEntityType,
	CONCAT(CONCAT(CG.Code, ' - '), CG.Name) AS CareGroupCodeName, 
	BI.TotalInvoice as TotalFolio, 
	COALESCE (BID.GrandTotalSalesPrice, 0) AS GrandTotalSalesPrice, 
	1 AS DistributionType,
	COALESCE (BID.ThirdPartySalesPrice, 0) AS ThirdPartySalesPrice, 
	COALESCE (BID.ThirdPartyPercentage, 0) 
	AS ThirdPartyPercentage,
	BI.ResponsibleRecoveryFee, 
	COALESCE (BID.SubTotalPatientSalesPrice, 0) AS SubTotalPatientSalesPrice, 
	(COALESCE (BID.PatientPercentage, 0) / 100) AS PatientPercentage, 
	BI.PatientDiscountPercentage, 
	BI.PatientDiscount, 
	COALESCE (BI.TotalPatientSalesPrice, 0) as TotalPatientSalesPrice,
	COALESCE (BI.TotalPatientWithDiscount, 0) as TotalPatientWithDiscount,
	COALESCE (BI.ValueVoucher, 0) as VoucherValue,  
	COALESCE (IPSS.Code, '') AS IPSServiceCode, 
	COALESCE (IPSS.Name, '') AS IPSServiceName,
	CONCAT(CUPSE.Code, ' - ', CUPSE.Description) AS CUPS, 
	COALESCE (IP.Id, 0) AS ProductId,
	COALESCE (IP.Code, '') AS ProductCode,
	COALESCE (IP.Name, '') AS ProductName, 
	AT.Code as ProductATCCode, 
	IIF(SOD.RecordType = 1, 
	CONCAT(CONCAT(BG1.Code, ' - '), BG1.Name), CONCAT(CONCAT(BG3.Code, ' - '), BG3.Name)) AS ServiceBillingGroupCodeName , 
	CONCAT(CONCAT(BG2.Code, ' - '), BG2.Name) AS ProductBillingGroupCodeName,
	COALESCE (SOD.CodeAssociateService, '') AS GuidHomologation, 
	COALESCE (SOD.InvoicedQuantity, 0) AS InvoicedQuantity, 
	COALESCE (SOD.SupplyQuantity, 0) AS SupplyQuantity, 
	COALESCE (SOD.DevolutionQuantity, 0) AS DevolutionQuantity,
	COALESCE (SOD.RateManualSalePrice, 0) AS RateManualSalePrice, 
	(case SOD.SurchargeApply when 0 then 'No' when 1 then 'Si' end) AS SurchargeApply,
	COALESCE (BID.RecoveryFeeType, 0) AS RecoveryFeeType, 
	COALESCE (BID.ApplyRecoveryFee, 1) AS ApplyRecoveryFee, 
	COALESCE (SOD.CostValue, 0) AS CostValue, 
	COALESCE (SOD.ServiceDate, GETDATE()) AS ServiceDate, 
	COALESCE (DRD.AllowValueChange, 0) AS AllowValueChange, 
	COALESCE (SOD.AuthorizationNumber, '0') AS AuthorizationNumber, 
	CONCAT(CONCAT(FU.Code, ' - '), FU.Name) AS PerformsFunctionalUnitCodeName, 
	COALESCE (SOD.PerformsHealthProfessionalCode, '') AS PerformsHealthProfessionalCode, 
	COALESCE (SOD.PerformsProfessionalSpecialty, - 1) AS PerformsProfessionalSpecialty,
	CONCAT(CONCAT(IPSSG.Code, ' - '), IPSSG.Name) AS IPSServiceGroupCodeName,
	CONCAT(CONCAT(CC.Code, ' - '), CC.Name) AS CostCenterCodeName, 
	COALESCE (SOD.SubTotalSalesPrice, 0) AS SubTotalSalesPrice, 
	COALESCE (SOD.ThirdPartyDiscount, 0) AS ThirdPartyDiscount,
	COALESCE (BID.GrandTotalDiscount, 0) AS GrandTotalDiscount, 
	COALESCE (SOD.ThirdPartyDiscountPercentage, 0) AS ThirdPartyDiscountPercentage, 
	COALESCE (SOD.TotalSalesPrice, 0) AS TotalSalesPrice, BI.Status,
	COALESCE(SOD.IsPackage, 0) as IsPackage, THP.Id as ThirdPartyPatientId,
	bid.GrandTotalTaxes,
	CASE 
		WHEN SOD.RecordType = 1 THEN COALESCE (IPSS.Code, '') 
		ELSE COALESCE (IP.Code, '')
	END AS ServiceCode,
	CASE
		WHEN SOD.RecordType = 1 THEN COALESCE (IPSS.Name, '')
		ELSE COALESCE (IP.Name, '')
	END AS ServiceName
	
FROM Billing.Invoice BI with (nolock)
INNER JOIN Billing.InvoiceDetail BID with (nolock) ON BID.InvoiceId = BI.Id
INNER JOIN Common.ThirdParty TP with (nolock) ON BI.ThirdPartyId = TP.Id 
LEFT JOIN Billing.InvoiceCategories IC with (nolock) ON BI.InvoiceCategoryId = IC.Id 
LEFT JOIN Contract.CareGroup CG with (nolock) ON BI.CareGroupId = CG.Id 
LEFT JOIN Billing.ServiceOrderDetail SOD with (nolock) ON BID.ServiceOrderDetailId = SOD.Id 
LEFT JOIN Billing.ServiceOrder SO with (nolock) ON SOD.ServiceOrderId = SO.Id
LEFT JOIN Payroll.FunctionalUnit FU with (nolock) ON SOD.PerformsFunctionalUnitId = FU.Id
LEFT JOIN Billing.BillingConcept IPSSG with (nolock) ON SOD.BillingConceptId = IPSSG.Id 
LEFT JOIN Payroll.CostCenter CC with (nolock) ON SOD.CostCenterId = CC.Id
LEFT JOIN Contract.DefinitionRateDetail DRD with (nolock) on DRD.Id = SOD.DefinitionRateDetailId 
LEFT JOIN Contract.HealthAdministrator HA with (nolock) ON BI.HealthAdministratorId = HA.Id
LEFT JOIN Inventory.InventoryProduct IP  with (nolock) ON SOD.ProductId = IP.Id 
LEFT JOIN Contract.IPSService IPSS with (nolock) ON SOD.IPSServiceId = IPSS.Id 
LEFT JOIN Contract.CUPSEntity CUPSE with (nolock) ON SOD.CUPSEntityId = CUPSE.Id 
LEFT JOIN Billing.BillingGroup BG1 with (nolock) ON CUPSE.BillingGroupId = BG1.Id
LEFT JOIN Billing.BillingGroup BG2 with (nolock) ON IP.BillingGroupId = BG2.Id
LEFT JOIN Inventory.InventoryProduct P with (nolock) ON P.Id = SOD.ProductId
LEFT JOIN Billing.BillingGroup BG3 with (nolock) ON P.BillingGroupId = BG3.Id 
LEFT JOIN Inventory.ATC AT  with (nolock) ON P.ATCId = AT.Id 
LEFT JOIN Common.ThirdParty THP with (nolock) ON BI.PatientCode = THP.Nit
WHERE BI.Status = 2 AND BID.InvoicedQuantity > 0
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de líneas de facturación pertenecientes a facturas anuladas (estado 2), utilizada para el control de ingresos y la trazabilidad de la anulación de facturas. Integra encabezados de factura, detalle de ítems facturados, órdenes de servicio, terceros pagadores (EPS, aseguradoras), grupos de atención del contrato, categorías de factura, unidades funcionales, centros de costo, productos de inventario, servicios CUPS/IPS y conceptos de facturación para ofrecer una vista completa de cada cargo anulado. Expone valores de venta totales, distribución entre tercero pagador y paciente, descuentos, cuotas moderadoras, impuestos, cantidades facturadas y devueltas, así como datos del profesional ejecutante y del grupo de facturación. Sirve como fuente principal para reportes de auditoría de ingresos, conciliación contable y análisis de glosas o reversiones de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListRevenueControlForAnnullateInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListRevenueControlForAnnullateInvoice';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida el detalle de facturas en estado 2 con cantidad facturada positiva, uniendo información de paciente, tercero, administradora de salud, grupo de atención, orden de servicio, productos y servicios CUPS/IPS, para soportar la anulación o control de ingresos de facturas.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControlForAnnullateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe existir en Billing.Invoice con Status = 2; El detalle Billing.InvoiceDetail debe tener InvoicedQuantity > 0; Debe existir relación válida entre Invoice y ThirdParty (INNER JOIN sobre BI.ThirdPartyId = TP.Id)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControlForAnnullateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen facturas con Status = 2 (estado de factura considerado para anulación/control); Solo se exponen líneas de detalle con cantidad facturada mayor a cero; Se neutralizan valores nulos a defaults: -1 para identificadores, 0 para montos/cantidades, '''' para textos, GETDATE() para ServiceDate; PatientPercentage se devuelve dividido entre 100 (proporción decimal); Los campos RevenueControlDetailId, BillingAuthorizationId, FolioOrder y ContractEntityId se fijan como -1 (no aplican en este contexto de control de ingresos para anulación); DistributionType siempre se expone como 1; El paciente se vincula al maestro de terceros emparejando BI.PatientCode con THP.Nit; El tipo de ítem (servicio vs producto) se determina por SOD.RecordType: 1 = servicio IPS/CUPS, otro = producto de inventario', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControlForAnnullateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Anulación de factura; Control de ingresos (Revenue Control); Paciente; Tercero pagador; Administradora de salud (EPS); Grupo de atención (CareGroup); Orden de servicio; Categoría de factura; Producto de inventario / medicamento (clasificación ATC, POS); Servicio IPS / CUPS; Grupo de facturación; Centro de costo; Unidad funcional; Tarifa de contrato; Cuota de recuperación (RecoveryFee); Copago / descuento al paciente; Distribución tercero-paciente; Autorización; Cirugía (SurgeryNumber); Folio / Tipo de folio (CareGroupType)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControlForAnnullateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewListRevenueControlForAnnullateInvoice: Devuelve únicamente filas donde BI.Status = 2 AND BID.InvoicedQuantity > 0', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControlForAnnullateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si BI.InvoiceCategoryId IS NULL → InvoiceCategoryCodeName se devuelve como cadena vacía else Se concatena IC.Code + '' - '' + IC.Name; si SOD.RecordType = 1 (servicio) → ServiceBillingGroupCodeName se arma desde BG1 (CUPSE.BillingGroupId); ServiceCode/ServiceName provienen de IPSS (servicio IPS) else ServiceBillingGroupCodeName se arma desde BG3 (Producto.BillingGroupId); ServiceCode/ServiceName provienen de IP (producto de inventario); si SOD.SurchargeApply = 0 → SurchargeApply se expone como ''No'' else Si SOD.SurchargeApply = 1, se expone como ''Si''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControlForAnnullateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Common.ThirdParty; Billing.InvoiceCategories; Contract.CareGroup; Billing.ServiceOrderDetail; Billing.ServiceOrder; Payroll.FunctionalUnit; Billing.BillingConcept; Payroll.CostCenter; Contract.DefinitionRateDetail; Contract.HealthAdministrator; Inventory.InventoryProduct; Contract.IPSService; Contract.CUPSEntity; Billing.BillingGroup; Inventory.ATC', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControlForAnnullateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControlForAnnullateInvoice';
GO
