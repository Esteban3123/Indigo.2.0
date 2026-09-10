

CREATE VIEW [Billing].[ViewListRevenueControl_cte]
AS
WITH revenue as (
	SELECT
	RCD.Id as RevenueControlDetailId,
	RC.AdmissionNumber,
	RCD.BillingAuthorizationId,
	TP.Id AS ThirdPartyId,
	CONCAT(TP.Nit, ' - ', TP.Name) AS ThirdPartyNitName,
	CG.Id AS CareGroupId,
	CG.EntityType AS CaregroupEntityType,
	CONCAT(CG.Code, ' - ', CG.Name) AS CareGroupCodeName,
	CG.CostCenterId CareGroupCostCenterId,
	RCD.FolioType,
	RCD.Observation, 
	RCD.TotalFolio, 
	RCD.ResponsibleRecoveryFee, 
	RCD.PatientDiscountPercentage, 
	RCD.PatientDiscount, 
	ISNULL(RCD.TotalPatientSalesPrice, 0) AS TotalPatientSalesPrice, 
	ISNULL(RCD.TotalPatientWithDiscount, 0) AS TotalPatientWithDiscount, 
	ISNULL(RCD.ValueVoucher, 0) AS VoucherValue, 
	RCD.Status,
	RCD.StatusFolioId,
	CG.LiquidationType,
	RCD.FolioOrder,
	CG.ContractId,
	RCD.InvoiceCategoryId,
	RC.PatientCode,
	RCD.HealthAdministratorId,
	RCD.ContractEntityId
	FROM Billing.RevenueControl RC WITH (NOLOCK) 
	JOIN Billing.RevenueControlDetail RCD WITH (NOLOCK) ON RC.Id = RCD.RevenueControlId
	JOIN Common.ThirdParty TP WITH (NOLOCK) ON RCD.ThirdPartyId = TP.Id 
	JOIN Contract.CareGroup CG WITH (NOLOCK) ON RCD.CareGroupId = CG.Id 
),
products as (
	SELECT
	isnull(IP.Id, 0) as Id,
	isnull(IP.Code, '') as Code,
	isnull(IP.Name, '') as Name,
	IP.POSProduct AS IsPOSProduct,
	AT.Code AS ATCCode,
	BG2.Name as BGroupName,
	BG2.Code as BGroupCode,
	CAST(ISNULL(POSP.IdParticion,0) AS BIT) AS HasPathologies
	FROM Inventory.InventoryProduct IP WITH (NOLOCK)
	LEFT JOIN Billing.BillingGroup BG2 WITH (NOLOCK) ON IP.BillingGroupId = BG2.Id 
	LEFT JOIN Inventory.ATC AT WITH (NOLOCK) ON IP.ATCId = AT.Id 
	LEFT JOIN 
	(
		SELECT ROW_NUMBER() OVER (PARTITION BY PP.ProductId ORDER BY PP.Id) IdParticion, PP.ProductId 
		FROM Inventory.POSPathologies PP WITH (NOLOCK) 
		WHERE PP.ProductId IS NOT NULL
	) POSP ON POSP.IdParticion = 1 AND POSP.ProductId = IP.Id
),
orderdetail as (
	SELECT
	SODD.Id,
	SO.Id AS ServiceOrderId,
	SO.Code AS ServiceOrderCode,
	SODD.RevenueControlDetailId,
	SODD.Quantity,
	SOD.Id AS ServiceOrderDetailId,
	SOD.IPSServiceId,
	SOD.CUPSEntityId,
	SOD.CUPSEntityContractDescriptionId,
	SOD.ProductId,
	SOD.SurgeryNumber,
	ISNULL(SOD.ServiceDate, Common.[GETDATE]()) AS ServiceDate, 
	ISNULL(SOD.RecordType, 1) AS RecordType, 
	SOD.Presentation, 
	SOD.SettlementType AS SettlementType, 
	ISNULL(SODD.DistributionType, 1) AS DistributionType, 
	ISNULL(SODD.Quantity, 0) AS InvoicedQuantity, 
	ISNULL(SOD.SupplyQuantity, 0) AS SupplyQuantity, 
	ISNULL(SOD.DevolutionQuantity, 0) AS DevolutionQuantity, 
	ISNULL(SOD.CostValue, 0) AS CostValue, 
	ISNULL(SOD.RateManualSalePrice, 0) AS RateManualSalePrice, 
	ISNULL(SODD.GrandTotalSalesPrice, 0) AS GrandTotalSalesPrice, 
	ISNULL(SOD.SubTotalSalesPrice, 0) AS SubTotalSalesPrice, 
	ISNULL(SOD.ThirdPartyDiscount, 0) AS ThirdPartyDiscount, 
	ISNULL(SODD.GrandTotalDiscount, 0) AS GrandTotalDiscount, 
	ISNULL(SOD.ThirdPartyDiscountPercentage, 0) AS ThirdPartyDiscountPercentage, 
	ISNULL(SOD.TotalSalesPrice, 0) AS TotalSalesPrice, 
	ISNULL(SODD.ThirdPartySalesPrice, 0) AS ThirdPartySalesPrice, 
	ISNULL(SODD.ThirdPartyPercentage, 0) AS ThirdPartyPercentage, 
	IIF(SOD.SettlementType = 3, 'Si', 'No') AS SurchargeApply, 
	ISNULL(SODD.RecoveryFeeType, 0) AS RecoveryFeeType, 
	ISNULL(SODD.ApplyRecoveryFee, 1) AS ApplyRecoveryFee, 
	ISNULL(SODD.SubTotalPatientSalesPrice, 0) AS SubTotalPatientSalesPrice, 
	ISNULL(SODD.PatientPercentage, 0) / 100 AS PatientPercentage, 		
	ISNULL(SOD.IsPackage, 0) AS IsPackage, 
	SOD.CodeAssociateService AS CodeAssociateService,
	ISNULL(SOD.CodeAssociateService, '') AS GuidHomologation, 
	ISNULL(SOD.ApplyRIAS, 0) ApplyRIAS, 
	ISNULL(SOD.RIASCupsId, 0) RIASCupsId,
	ISNULL(SOD.PerformsHealthProfessionalCode, '') AS PerformsHealthProfessionalCode, 
	ISNULL(SOD.PerformsProfessionalSpecialty, - 1) AS PerformsProfessionalSpecialty, 
	CONCAT(FU.Code, ' - ', FU.Name) AS PerformsFunctionalUnitCodeName,
	CONCAT(CC.Code, ' - ', CC.Name) AS CostCenterCodeName, 
	ISNULL(SOD.AuthorizationNumber, '0') AS AuthorizationNumber, 
	CONCAT(BI.Code, ' - ', BI.Name) AS IPSServiceGroupCodeName,
	ISNULL(DRD.AllowValueChange, 0) AS AllowValueChange,
	SUBSTRING((SELECT ','+code AS [text()] FROM Billing.MipresCode mc WHERE mc.ServiceOrderDetailId = sod.Id FOR XML PATH ('')), 2, 1000) AS Mipres,
	SUBSTRING((SELECT ',' + IdMipres  AS [text()] FROM Billing.MipresCode mc WHERE mc.ServiceOrderDetailId = sod.Id FOR XML PATH ('')), 2, 1000) AS IdMipres
	FROM Billing.ServiceOrderDetailDistribution SODD WITH (NOLOCK)
	INNER JOIN Billing.ServiceOrderDetail SOD WITH (NOLOCK) ON SODD.ServiceOrderDetailId = SOD.Id AND SOD.InvoicedQuantity > 0 AND SOD.IsDelete = 0
	INNER JOIN Billing.ServiceOrder SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id 
	INNER JOIN Payroll.FunctionalUnit FU WITH (NOLOCK) ON SOD.PerformsFunctionalUnitId = FU.Id 
	LEFT JOIN Payroll.CostCenter CC WITH (NOLOCK) ON SOD.CostCenterId = CC.Id 
	LEFT JOIN Billing.BillingConcept BI WITH (NOLOCK) ON SOD.BillingConceptId = BI.Id 
	LEFT JOIN Contract.DefinitionRateDetail DRD WITH (NOLOCK) ON SOD.DefinitionRateDetailId = DRD.Id
)
SELECT	
		CONCAT(ISNULL(od.Id, - 1), ' - ', r.RevenueControlDetailId) AS IdKey, 		
		-----------------------------------------------------------------------
		THP.Id AS ThirdPartyPatientId, 
		r.PatientCode, 
		r.AdmissionNumber, 
		CONCAT(LTRIM(RTRIM(r.AdmissionNumber)), ' - Paciente: ', LTRIM(RTRIM(r.PatientCode)), ' - ', THP.Name) AS AdmissionNumberPatient, 
		-----------------------------------------------------------------------
		r.RevenueControlDetailId, 
		r.FolioOrder, 
		r.InvoiceCategoryId, 
		CONCAT(IC.Code, ' - ', IC.Name) AS InvoiceCategoryCodeName, 
		ISNULL(r.BillingAuthorizationId, - 1) AS BillingAuthorizationId, 
		r.ThirdPartyId, 
		r.ThirdPartyNitName, 
		r.CareGroupId, 
		r.CaregroupEntityType, 
		r.CareGroupCodeName, 
		CONCAT(C.Code, ' - ', C.ContractName) AS ContractCodeName, 
		ISNULL(HA.Id, 0) AS HealthAdministratorId, 
		CONCAT(HA.Code, ' - ', HA.Name) AS HealthAdministratorCodeName, 
		HA.ThirdPartyId AS ThirdPartyHealthAdministrator, 
		ISNULL(CE.Id, 0) AS ContractEntityId, 
		CONCAT(CE.Code, ' - ', CE.Name) AS ContractEntityCodeName, 
		-----------------------------------------------------------------------
		ISNULL(I.Id, - 1) AS InvoiceId, 
		ISNULL(I.InvoiceNumber, '') AS InvoiceNumber, 
		ISNULL(I.InvoiceDate, '') AS InvoiceDate, 
		ISNULL(I.InvoicedUser, '') AS InvoicedUser, 
		-----------------------------------------------------------------------
		r.CareGroupCostCenterId, 
		r.LiquidationType,
		r.FolioType,
		r.Observation, 
		r.TotalFolio, 
		r.ResponsibleRecoveryFee, 
		r.PatientDiscountPercentage, 
		r.PatientDiscount, 
		r.TotalPatientSalesPrice, 
		r.TotalPatientWithDiscount, 
		r.VoucherValue, 
		r.Status,
		r.StatusFolioId,
		CONCAT(CSS.Code,'-',CSS.Name) AS StatusFolioName,
		-----------------------------------------------------------------------
		od.ServiceOrderId, 
		od.ServiceOrderCode, 
		-----------------------------------------------------------------------
		ISNULL(od.Id, - 1) AS Id, 
		od.ServiceOrderDetailId, 
		od.PerformsFunctionalUnitCodeName, 		
		od.CostCenterCodeName, 
		od.AuthorizationNumber, 
		od.SurgeryNumber,
		od.PerformsHealthProfessionalCode, 
		od.PerformsProfessionalSpecialty, 
		CONCAT(ISNULL(BG1.Code, p.BGroupCode), ' - ', ISNULL(BG1.Name, p.BGroupName)) AS ServiceBillingGroupCodeName, 
		COALESCE(IPSS.Code, p.Code, '') AS ServiceCode, 
		COALESCE(IPSS.Name, p.Name, '') AS ServiceName, 
		-----------------------------------------------------------------------		
		od.IPSServiceGroupCodeName,
		ISNULL(IPSS.Code, '') AS IPSServiceCode, 
		ISNULL(IPSS.Name, '') AS IPSServiceName, 
		CONCAT(CUPSE.Code, ' - ', CUPSE.Description) AS CUPS, 
		CONCAT(cd.Code, ' - ', cd.Name) AS ContractDescriptionCodeName, 
		-----------------------------------------------------------------------
		CONCAT(p.BGroupCode, ' - ', p.BGroupName) AS ProductBillingGroupCodeName, 
		p.Id AS ProductId,
		p.Code AS ProductCode, 		
		p.Name AS ProductName, 
		p.IsPOSProduct, 
		p.ATCCode AS ProductATCCode, 
		p.HasPathologies,
		-----------------------------------------------------------------------
		od.ServiceDate, 
		od.RecordType, 
		od.Presentation, 
		od.SettlementType, 
		od.DistributionType, 
		od.InvoicedQuantity, 
		od.SupplyQuantity, 
		od.DevolutionQuantity, 
		od.CostValue, 
		od.RateManualSalePrice, 
		od.GrandTotalSalesPrice, 
		od.SubTotalSalesPrice, 
		od.ThirdPartyDiscount, 
		od.GrandTotalDiscount, 
		od.ThirdPartyDiscountPercentage, 
		od.TotalSalesPrice, 
		od.ThirdPartySalesPrice, 
		od.ThirdPartyPercentage, 
		od.SurchargeApply, 
		od.RecoveryFeeType, 
		od.ApplyRecoveryFee, 
		od.SubTotalPatientSalesPrice, 
		od.PatientPercentage, 		
		od.IsPackage, 
		od.CodeAssociateService,
		od.GuidHomologation, 
		od.ApplyRIAS, 
		od.RIASCupsId,
		od.AllowValueChange,
		Mipres,
		IdMipres
FROM revenue r
LEFT JOIN Contract.Contract C WITH (NOLOCK) ON r.ContractId = C.Id
LEFT JOIN Billing.InvoiceCategories IC WITH (NOLOCK) ON r.InvoiceCategoryId = IC.Id 
LEFT JOIN Common.ThirdParty THP  WITH (NOLOCK) ON r.PatientCode = THP.Nit
LEFT JOIN Contract.HealthAdministrator HA WITH (NOLOCK) ON r.HealthAdministratorId = HA.Id 
LEFT JOIN Contract.ContractEntity CE WITH (NOLOCK) ON r.ContractEntityId = CE.Id 
LEFT JOIN Billing.RevenueControlDetailInvoice rcdi WITH (NOLOCK) ON r.RevenueControlDetailId = rcdi.RevenueControlDetailId AND rcdi.Status = 1
LEFT JOIN Billing.Invoice I WITH (NOLOCK) ON (r.RevenueControlDetailId = I.RevenueControlDetailId OR rcdi.InvoiceId = I.Id) AND I.Status = 1 
LEFT JOIN Billing.ConceptsCausesStatusFolio CSS WITH(NOLOCK) ON r.StatusFolioId = CSS.Id
-----------------------------------------------------------------------
LEFT JOIN orderdetail od WITH (NOLOCK) ON r.RevenueControlDetailId = od.RevenueControlDetailId AND od.Quantity > 0
-----------------------------------------------------------------------
LEFT JOIN Contract.IPSService IPSS WITH (NOLOCK) ON od.IPSServiceId = IPSS.Id 
LEFT JOIN Contract.CUPSEntity CUPSE WITH (NOLOCK) ON od.CUPSEntityId = CUPSE.Id 
LEFT JOIN Contract.CUPSEntityContractDescriptions ccd WITH (NOLOCK) ON od.CUPSEntityContractDescriptionId = ccd.Id
LEFT JOIN Contract.ContractDescriptions cd WITH (NOLOCK) ON ccd.ContractDescriptionId = cd.Id
LEFT JOIN Billing.BillingGroup BG1  WITH (NOLOCK) ON IIF(od.ApplyRIAS = 1, ISNULL(CUPSE.RIASBillingGroupId, CUPSE.BillingGroupId), IIF(ISNULL(ccd.Id, 0) > 0, ccd.BillingGroupId, CUPSE.BillingGroupId)) = BG1.Id 
-----------------------------------------------------------------------
LEFT JOIN products p ON od.ProductId = p.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de control de ingresos de facturación que consolida, para cada folio de cobro asociado a un ingreso (admisión) del paciente, el detalle completo de los servicios liquidados, los montos por tipo de responsable (tercero pagador, paciente), copagos, cuotas de recuperación, descuentos y valores de bono. Integra el encabezado del control de ingresos (RevenueControl) con su detalle por folio (RevenueControlDetail), cruzando datos del tercero responsable del pago (NIT y nombre de la aseguradora o entidad), el grupo de atención contractual (CareGroup con código, tipo de entidad y contrato), el detalle de distribución de órdenes de servicio con cantidades facturadas, precios de venta, descuentos y unidad funcional ejecutante, así como información del producto o servicio (medicamento, procedimiento, código ATC, grupo de facturación). Sirve como fuente principal para la pantalla y reportes de revisión y gestión de folios de facturación por admisión, permitiendo identificar qué se cobró, a quién, bajo qué contrato y cuánto corresponde al paciente versus al tercero pagador.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListRevenueControl_cte';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListRevenueControl_cte';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola consulta el detalle de control de ingresos (folios) por admisión, cruzándolo con sus órdenes de servicio, productos, contratos, terceros, facturas y estados de folio para listados/reportes de facturación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControl_cte';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existir registros relacionados en Billing.RevenueControl y Billing.RevenueControlDetail vinculados a un ThirdParty y a un CareGroup (joins INNER vía CTE ''revenue'').; Para que aparezca el detalle de orden, debe existir ServiceOrderDetail con InvoicedQuantity > 0 e IsDelete = 0 y la distribución (ServiceOrderDetailDistribution) con Quantity > 0.; El paciente se identifica cruzando RevenueControl.PatientCode contra Common.ThirdParty.Nit.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControl_cte';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran facturas activas (Invoice.Status = 1) y relaciones detalle-factura activas (RevenueControlDetailInvoice.Status = 1).; Solo se consideran ítems de orden no eliminados (ServiceOrderDetail.IsDelete = 0) y efectivamente facturados (InvoicedQuantity > 0, Distribution.Quantity > 0).; PatientPercentage se expone como fracción (porcentaje dividido entre 100).; El BillingGroup mostrado del servicio prioriza la regla RIAS/ContractDescription antes que el del producto: COALESCE entre BG1 (servicio) y el del producto (BG2).; Se utiliza WITH (NOLOCK) en todas las lecturas, por lo que la vista permite lecturas sucias.; Mipres e IdMipres se concatenan a partir de los códigos MIPRES asociados a cada ServiceOrderDetail mediante FOR XML PATH.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControl_cte';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Control de ingresos (Revenue Control); Folio de facturación; Admisión del paciente; Autorización de facturación; Cuota de recuperación (RecoveryFee); Copago / descuento al paciente; Grupo de atención (CareGroup); Tipo de liquidación; Categoría de factura; Administradora de salud (EPS/ARS); Entidad contratante; CUPS (Clasificación Única de Procedimientos en Salud); Servicio IPS; MIPRES (prescripción de tecnologías no financiadas por UPC); RIAS (Rutas Integrales de Atención en Salud); Producto POS / Patologías POS; Clasificación ATC de medicamentos; Bono/voucher; Unidad funcional y centro de costo; Distribución tercero-paciente (ThirdPartyPercentage / PatientPercentage); Cirugía (SurgeryNumber); Paquete de servicios (IsPackage)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControl_cte';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve una fila por cada combinación de detalle de folio (RevenueControlDetail) y, si existe, su distribución de orden de servicio; si no hay orden asociada, devuelve igualmente la fila de folio (LEFT JOIN orderdetail).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControl_cte';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si od.ApplyRIAS = 1 → El BillingGroup se toma de CUPSEntity.RIASBillingGroupId (o BillingGroupId si es null) else Si existe CUPSEntityContractDescription (ccd.Id > 0) usa ccd.BillingGroupId; en caso contrario usa CUPSEntity.BillingGroupId; si SOD.SettlementType = 3 → Se marca SurchargeApply = ''Si'' else SurchargeApply = ''No''; si Existe Billing.RevenueControlDetailInvoice con Status = 1 para el detalle, o Invoice.RevenueControlDetailId coincide directamente, y Invoice.Status = 1 → Se expone la información de la factura (InvoiceId, InvoiceNumber, InvoiceDate, InvoicedUser) else Se devuelven valores por defecto (-1 / cadena vacía) para los campos de factura; si Inventory.POSPathologies tiene al menos una fila para el ProductId (ROW_NUMBER = 1) → HasPathologies = 1 (producto con patologías POS asociadas) else HasPathologies = 0', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControl_cte';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControl; Billing.RevenueControlDetail; Common.ThirdParty; Contract.CareGroup; Inventory.InventoryProduct; Billing.BillingGroup; Inventory.ATC; Inventory.POSPathologies; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; Billing.ServiceOrder; Payroll.FunctionalUnit; Payroll.CostCenter; Billing.BillingConcept; Contract.DefinitionRateDetail; Billing.MipresCode; Contract.Contract; Billing.InvoiceCategories; Contract.HealthAdministrator; Contract.ContractEntity; Billing.RevenueControlDetailInvoice; Billing.Invoice; Billing.ConceptsCausesStatusFolio; Contract.IPSService; Contract.CUPSEntity; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControl_cte';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControl_cte';
GO
