CREATE VIEW [Billing].[ViewListRecognitionDetail]
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
		COALESCE (SOD1.CostValue, 0) AS CostValue, COALESCE (SOD1.ServiceDate, [Common].[GETDATE]()) AS ServiceDate, COALESCE (DRD.AllowValueChange, 0) AS AllowValueChange, COALESCE (SOD1.AuthorizationNumber, '0') 
		AS AuthorizationNumber, CONCAT(CONCAT(FU.Code, ' - '), FU.Name) AS PerformsFunctionalUnitCodeName, COALESCE (SOD1.PerformsHealthProfessionalCode, '') AS PerformsHealthProfessionalCode, 
		COALESCE (SOD1.PerformsProfessionalSpecialty, - 1) AS PerformsProfessionalSpecialty, CONCAT(CONCAT(IPSSG.Code, ' - '), IPSSG.Name) AS IPSServiceGroupCodeName, CONCAT(CONCAT(CC.Code, ' - '), 
		CC.Name) AS CostCenterCodeName, COALESCE (SOD1.SubTotalSalesPrice, 0) AS SubTotalSalesPrice, COALESCE (SOD1.ThirdPartyDiscount, 0) AS ThirdPartyDiscount, COALESCE (SOD1.GrandTotalDiscount, 0) 
		AS GrandTotalDiscount, COALESCE (SOD1.ThirdPartyDiscountPercentage, 0) AS ThirdPartyDiscountPercentage, COALESCE (SOD1.TotalSalesPrice, 0) AS TotalSalesPrice, RCD.Status, COALESCE (SOD1.IsPackage, 0) 
		AS IsPackage, THP.Id AS ThirdPartyPatientId, CASE WHEN SOD1.RecordType = 1 THEN COALESCE (IPSS.Code, '') ELSE COALESCE (IP.Code, '') END AS ServiceCode, 
		CASE WHEN SOD1.RecordType = 1 THEN COALESCE (IPSS.Name, '') ELSE COALESCE (IP.Name, '') END AS ServiceName, CONCAT(CONCAT(COALESCE (SOD1.SODDId, - 1), ' - '), RCD.Id) AS IdKey, CONCAT(LTRIM(RTRIM(RC.AdmissionNumber)), ' - Paciente: ', LTRIM(RTRIM(RC.PatientCode)), ' - ', THP.Name) AS AdmissionNumberPatient
		, CG.OperativeUnitId--, ING.IFECHAING AS FechaIngreso
FROM    Billing.RevenueControlDetail RCD with (nolock) INNER JOIN
        Billing.RevenueControl RC with (nolock) ON RCD.RevenueControlId = RC.Id INNER JOIN
        Common.ThirdParty TP with (nolock) ON RCD.ThirdPartyId = TP.Id INNER JOIN
        Contract.CareGroup CG with (nolock) ON RCD.CareGroupId = CG.Id LEFT JOIN
        Billing.InvoiceCategories IC with (nolock) ON RCD.InvoiceCategoryId = IC.Id LEFT JOIN
		(
			SELECT 
			SOD.ServiceOrderId,SOD.PerformsFunctionalUnitId,SOD.BillingConceptId,SOD.CostCenterId,SOD.DefinitionRateDetailId,
			SOD.ProductId,SOD.IPSServiceId,SOD.CUPSEntityId,SOD.SurgeryNumber,SOD.Presentation,SOD.CodeAssociateService,
			SOD.Id AS SODID,SOD.SettlementType,SOD.RecordType,SOD.SupplyQuantity,SOD.DevolutionQuantity,SOD.RateManualSalePrice,
			SOD.CostValue,SOD.ServiceDate,SOD.AuthorizationNumber,SOD.PerformsHealthProfessionalCode,SOD.PerformsProfessionalSpecialty,
			SOD.SubTotalSalesPrice,SOD.ThirdPartyDiscount,SOD.ThirdPartyDiscountPercentage,SOD.TotalSalesPrice,SOD.IsPackage

			,SODD.RevenueControlDetailId,SODD.Id AS SODDID,SODD.GrandTotalSalesPrice - SODD.GrandTotalTaxes AS DGrandTotalSalesPrice,SODD.DistributionType,
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
		where RCD.TotalFolio > 0