
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