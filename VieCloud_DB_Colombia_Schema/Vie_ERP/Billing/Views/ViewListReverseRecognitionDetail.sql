
CREATE VIEW [Billing].[ViewListReverseRecognitionDetail]
AS

SELECT	COALESCE (SOD1.SODDId, - 1) AS Id, RC.PatientCode, RCD.Observation, RCD.InvoiceCategoryId, CASE WHEN RCD.InvoiceCategoryId IS NULL THEN '' ELSE CONCAT(IC.Code, ' - ', IC.Name) 
		END AS InvoiceCategoryCodeName, SO.Id AS ServiceOrderId, SOD1.SurgeryNumber, SOD1.Presentation, SOD1.CodeAssociateService AS CodeAssociateService, SO.Code AS ServiceOrderCode, 
		SOD1.SODId AS ServiceOrderDetailId, SOD1.SettlementType AS SettlementType, RCD.Id AS RevenueControlDetailId, RC.AdmissionNumber, COALESCE (RCD.BillingAuthorizationId, - 1) AS BillingAuthorizationId, 
		COALESCE (BI.Id, - 1) AS InvoiceId, COALESCE (BI.InvoiceNumber, '') AS InvoiceNumber, COALESCE (BI.InvoiceDate, '') AS InvoiceDate, COALESCE (BI.InvoicedUser, '') AS InvoicedUser, COALESCE (SOD1.RecordType, 1) AS RecordType, P.POSProduct AS IsPOSProduct, RCD.FolioOrder, RCD.FolioType, COALESCE (CE.Id, 0) 
		AS ContractEntityId, CONCAT(CONCAT(COALESCE (CE.Code, ''), ' - '), COALESCE (CE.Name, '')) AS ContractEntityCodeName, COALESCE (HA.Id, 0) AS HealthAdministratorId, CONCAT(CONCAT(COALESCE (HA.Code, 
		''), ' - '), COALESCE (HA.Name, '')) AS HealthAdministratorCodeName, HA.ThirdPartyId AS ThirdPartyHealthAdministrator, TP.Id AS ThirdPartyId, CONCAT(CONCAT(TP.Nit, ' - '), TP.Name) AS ThirdPartyNitName, CG.Id AS CareGroupId, 
		CG.EntityType AS CaregroupEntityType, CONCAT(CONCAT(CG.Code, ' - '), CG.Name) AS CareGroupCodeName, RCD.TotalFolio, COALESCE (SOD1.DGrandTotalSalesPrice, 0) AS GrandTotalSalesPrice, 
		COALESCE (SOD1.DistributionType, 1) AS DistributionType, COALESCE (SOD1.ThirdPartySalesPrice, 0) AS ThirdPartySalesPrice, COALESCE (SOD1.ThirdPartyPercentage, 0) AS ThirdPartyPercentage, 
		RCD.ResponsibleRecoveryFee, COALESCE (SOD1.SubTotalPatientSalesPrice, 0) AS SubTotalPatientSalesPrice, (COALESCE (SOD1.PatientPercentage, 0) / 100) AS PatientPercentage, 
		RCD.PatientDiscountPercentage, RCD.PatientDiscount, COALESCE (RCD.TotalPatientSalesPrice, 0) AS TotalPatientSalesPrice, COALESCE (RCD.TotalPatientWithDiscount, 0) AS TotalPatientWithDiscount, COALESCE (RCD.ValueVoucher, 0) as VoucherValue, 
		COALESCE (IPSS.Code, '') AS IPSServiceCode, CONCAT(CUPSE.Code, ' - ', CUPSE.Description) AS CUPS, COALESCE (IPSS.Name, '') AS IPSServiceName, COALESCE (IP.Id, 0) AS ProductId,COALESCE (IP.Code, '') AS ProductCode, AT.Code as ProductATCCode, 
		COALESCE (IP.Name, '') AS ProductName, IIF(SOD1.RecordType = 1, CONCAT(CONCAT(BG1.Code, ' - '), BG1.Name), CONCAT(CONCAT(BG3.Code, ' - '), BG3.Name)) AS ServiceBillingGroupCodeName, 
		CONCAT(CONCAT(BG2.Code, ' - '), BG2.Name) AS ProductBillingGroupCodeName, COALESCE (SOD1.CodeAssociateService, '') AS GuidHomologation, COALESCE (SOD1.Quantity, 0) AS InvoicedQuantity, 
		COALESCE (SOD1.SupplyQuantity, 0) AS SupplyQuantity, COALESCE (SOD1.DevolutionQuantity, 0) AS DevolutionQuantity, COALESCE (SOD1.RateManualSalePrice, 0) AS RateManualSalePrice, 
		(CASE SOD1.SettlementType WHEN 3 THEN 'Si' ELSE 'No' END) AS SurchargeApply, COALESCE (SOD1.RecoveryFeeType, 0) AS RecoveryFeeType, COALESCE (SOD1.ApplyRecoveryFee, 1) AS ApplyRecoveryFee, 
		COALESCE (SOD1.CostValue, 0) AS CostValue, COALESCE (SOD1.ServiceDate, GETDATE()) AS ServiceDate, COALESCE (DRD.AllowValueChange, 0) AS AllowValueChange, COALESCE (SOD1.AuthorizationNumber, '0') 
		AS AuthorizationNumber, CONCAT(CONCAT(FU.Code, ' - '), FU.Name) AS PerformsFunctionalUnitCodeName, COALESCE (SOD1.PerformsHealthProfessionalCode, '') AS PerformsHealthProfessionalCode, 
		COALESCE (SOD1.PerformsProfessionalSpecialty, - 1) AS PerformsProfessionalSpecialty, CONCAT(CONCAT(IPSSG.Code, ' - '), IPSSG.Name) AS IPSServiceGroupCodeName, CONCAT(CONCAT(CC.Code, ' - '), 
		CC.Name) AS CostCenterCodeName, COALESCE (SOD1.SubTotalSalesPrice, 0) AS SubTotalSalesPrice, COALESCE (SOD1.ThirdPartyDiscount, 0) AS ThirdPartyDiscount, COALESCE (SOD1.GrandTotalDiscount, 0) 
		AS GrandTotalDiscount, COALESCE (SOD1.ThirdPartyDiscountPercentage, 0) AS ThirdPartyDiscountPercentage, COALESCE (SOD1.TotalSalesPrice, 0) AS TotalSalesPrice, RCD.Status, COALESCE (SOD1.IsPackage, 0) 
		AS IsPackage, THP.Id AS ThirdPartyPatientId, CASE WHEN SOD1.RecordType = 1 THEN COALESCE (IPSS.Code, '') ELSE COALESCE (IP.Code, '') END AS ServiceCode, 
		CASE WHEN SOD1.RecordType = 1 THEN COALESCE (IPSS.Name, '') ELSE COALESCE (IP.Name, '') END AS ServiceName, CONCAT(CONCAT(COALESCE (SOD1.SODDId, - 1), ' - '), RCD.Id) AS IdKey, CONCAT(LTRIM(RTRIM(RC.AdmissionNumber)), ' - Paciente: ', LTRIM(RTRIM(RC.PatientCode)), ' - ', THP.Name) AS AdmissionNumberPatient
		, CG.OperativeUnitId, rrc.Id as RevenueRecognitionId, rrc.[State] as RecognitionState
FROM    Billing.RevenueControlDetail RCD with (nolock) INNER JOIN
        Billing.RevenueControl RC with (nolock) ON RCD.RevenueControlId = RC.Id INNER JOIN
        Common.ThirdParty TP with (nolock) ON RCD.ThirdPartyId = TP.Id INNER JOIN
        Contract.CareGroup CG with (nolock) ON RCD.CareGroupId = CG.Id INNER JOIN
		Billing.RevenueRecognitionDetail rrd with(nolock) on rrd.RevenueControlDetailId = RCD.Id INNER JOIN
		Billing.RevenueRecognition rrc with(nolock) on rrc.Id = rrd.RevenueRecognitionId LEFT JOIN
        Billing.InvoiceCategories IC with (nolock) ON RCD.InvoiceCategoryId = IC.Id LEFT JOIN
		(
			SELECT 
			SOD.ServiceOrderId,SOD.PerformsFunctionalUnitId,SOD.BillingConceptId,SOD.CostCenterId,SOD.DefinitionRateDetailId,
			SOD.ProductId,SOD.IPSServiceId,SOD.CUPSEntityId,SOD.SurgeryNumber,SOD.Presentation,SOD.CodeAssociateService,
			SOD.Id AS SODID,SOD.SettlementType,SOD.RecordType,SOD.SupplyQuantity,SOD.DevolutionQuantity,SOD.RateManualSalePrice,
			SOD.CostValue,SOD.ServiceDate,SOD.AuthorizationNumber,SOD.PerformsHealthProfessionalCode,SOD.PerformsProfessionalSpecialty,
			SOD.SubTotalSalesPrice,SOD.ThirdPartyDiscount,SOD.ThirdPartyDiscountPercentage,SOD.TotalSalesPrice,SOD.IsPackage

			,SODD.RevenueControlDetailId,SODD.Id AS SODDID,SODD.GrandTotalSalesPrice AS DGrandTotalSalesPrice,SODD.DistributionType,
			SODD.ThirdPartySalesPrice,SODD.ThirdPartyPercentage,SODD.SubTotalPatientSalesPrice,SODD.PatientPercentage,SODD.Quantity,
			SODD.RecoveryFeeType,SODD.ApplyRecoveryFee,SODD.GrandTotalDiscount

			FROM Billing.ServiceOrderDetailDistribution SODD
			INNER JOIN Billing.ServiceOrderDetail SOD ON SODD.ServiceOrderDetailId = SOD.Id AND SOD.InvoicedQuantity > 0 AND SOD.IsDelete = 0
			WHERE SODD.Quantity > 0
		) AS SOD1 ON SOD1.RevenueControlDetailId = RCD.Id LEFT JOIN
		Billing.ServiceOrder SO  with (nolock) ON SOD1.ServiceOrderId = SO.Id LEFT JOIN
        Payroll.FunctionalUnit FU with (nolock) ON SOD1.PerformsFunctionalUnitId = FU.Id LEFT JOIN
        Billing.BillingConcept IPSSG with (nolock) ON SOD1.BillingConceptId = IPSSG.Id LEFT JOIN
        Payroll.CostCenter CC  with (nolock) ON SOD1.CostCenterId = CC.Id LEFT JOIN
        Contract.DefinitionRateDetail DRD  with (nolock) ON DRD.Id = SOD1.DefinitionRateDetailId LEFT JOIN
        Billing.Invoice BI with (nolock) ON RCD.Id = BI.RevenueControlDetailId AND BI.Status = 1 LEFT JOIN
        Contract.ContractEntity CE with (nolock) ON RCD.ContractEntityId = CE.Id LEFT JOIN
        Contract.HealthAdministrator HA with (nolock) ON RCD.HealthAdministratorId = HA.Id LEFT JOIN
        Inventory.InventoryProduct IP with (nolock) ON SOD1.ProductId = IP.Id LEFT JOIN
        Contract.IPSService IPSS with (nolock) ON SOD1.IPSServiceId = IPSS.Id LEFT JOIN
        Contract.CUPSEntity CUPSE with (nolock) ON SOD1.CUPSEntityId = CUPSE.Id LEFT JOIN
        Billing.BillingGroup BG1  with (nolock) ON CUPSE.BillingGroupId = BG1.Id LEFT JOIN
        Billing.BillingGroup BG2 with (nolock) ON IP.BillingGroupId = BG2.Id LEFT JOIN
        Inventory.InventoryProduct P  with (nolock) ON P.Id = SOD1.ProductId LEFT JOIN
        Billing.BillingGroup BG3  with (nolock) ON P.BillingGroupId = BG3.Id LEFT JOIN
		Inventory.ATC AT  with (nolock) ON P.ATCId = AT.Id LEFT JOIN
        Common.ThirdParty THP  with (nolock) ON RC.PatientCode = THP.Nit
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de líneas de facturación asociadas a reconocimientos de ingresos que han sido o pueden ser revertidos. Integra el control de ingresos por admisión (topes de cuota moderadora, copago y recuperación), el detalle de distribución financiera de cada ítem de orden de servicio (valores del tercero pagador y del paciente, descuentos, porcentajes), y el comprobante de reconocimiento contable con su estado, permitiendo identificar qué servicios, procedimientos o productos facturados están vinculados a un reconocimiento contable reversado o pendiente de reversión. Sirve como fuente principal para los procesos de reversión de causación contable en facturación, reportes de glosas, auditoría de cobros a EPS/aseguradoras y trazabilidad del ingreso por admisión, grupo de atención y categoría de factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListReverseRecognitionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListReverseRecognitionDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola fila por distribución el detalle de los folios cuyo reconocimiento de ingresos puede revertirse, mostrando datos de admisión, paciente, contrato, servicio/producto facturado, valores y estado del reconocimiento.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListReverseRecognitionDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El detalle del folio (RevenueControlDetail) debe estar vinculado a un control de ingresos (RevenueControl), un tercero (ThirdParty) y un grupo de atención (CareGroup) — todos con INNER JOIN.; Debe existir un RevenueRecognitionDetail asociado al detalle del folio y, a su vez, un RevenueRecognition padre (INNER JOIN), es decir, el folio debe haber sido reconocido contablemente para aparecer.; Para mostrar datos de la orden de servicio, el ServiceOrderDetail debe tener InvoicedQuantity > 0 e IsDelete = 0, y la distribución (ServiceOrderDetailDistribution) debe tener Quantity > 0.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListReverseRecognitionDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen folios con reconocimiento de ingresos existente (gracias a los INNER JOIN sobre RevenueRecognitionDetail y RevenueRecognition).; Los pacientes se obtienen de Common.ThirdParty cruzando RC.PatientCode contra TP.Nit, asumiendo que el código del paciente equivale al NIT del tercero.; PatientPercentage se expone dividido entre 100 (proporción 0–1), no como porcentaje entero.; Los campos numéricos y de identificadores ausentes se normalizan con COALESCE a 0 o -1, y los textuales a cadena vacía, garantizando que la vista nunca devuelva NULLs en esos campos.; Solo se consideran ítems de la orden de servicio efectivamente facturados (InvoicedQuantity > 0) y no eliminados (IsDelete = 0), con distribución vigente (Quantity > 0).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListReverseRecognitionDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reversa de reconocimiento de ingresos; Folio de facturación; Control de ingresos por admisión; Cuota de recuperación / copago / cuota moderadora; Distribución tercero-paciente; Orden de servicio; Autorización de facturación; CUPS; Producto POS; Grupo de facturación; Administradora de salud / Entidad contratante; Unidad funcional y centro de costo; Tipo de liquidación y recargo; Categoría de factura; Clasificación ATC de medicamentos', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListReverseRecognitionDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewListReverseRecognitionDetail: Devuelve un registro por cada combinación de RevenueControlDetail + RevenueRecognitionDetail + distribución de orden de servicio que cumpla las condiciones de los joins.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListReverseRecognitionDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RCD.InvoiceCategoryId IS NULL → InvoiceCategoryCodeName se devuelve como cadena vacía else Se concatena Code + '' - '' + Name de InvoiceCategories; si SOD1.RecordType = 1 (servicio) → ServiceBillingGroupCodeName se toma del BillingGroup del CUPSEntity (BG1) y ServiceCode/ServiceName se toma de IPSService else ServiceBillingGroupCodeName se toma del BillingGroup del producto (BG3) y ServiceCode/ServiceName se toma de InventoryProduct; si SOD1.SettlementType = 3 → SurchargeApply = ''Si'' (aplica recargo) else SurchargeApply = ''No''; si BI.Status = 1 al unir Billing.Invoice → Solo se asocian facturas activas/vigentes al detalle; en caso contrario InvoiceId, InvoiceNumber, InvoiceDate y InvoicedUser quedan en valores por defecto (-1, '''', '''')', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListReverseRecognitionDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControlDetail; Billing.RevenueControl; Common.ThirdParty; Contract.CareGroup; Billing.RevenueRecognitionDetail; Billing.RevenueRecognition; Billing.InvoiceCategories; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; Billing.ServiceOrder; Payroll.FunctionalUnit; Billing.BillingConcept; Payroll.CostCenter; Contract.DefinitionRateDetail; Billing.Invoice; Contract.ContractEntity; Contract.HealthAdministrator; Inventory.InventoryProduct; Contract.IPSService; Contract.CUPSEntity; Billing.BillingGroup; Inventory.ATC', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListReverseRecognitionDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListReverseRecognitionDetail';
GO
