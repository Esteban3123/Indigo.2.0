




CREATE VIEW [Billing].[ViewListRevenueControl]
AS

WITH SERVICE_ORDER_CTE AS (
	SELECT SODD.Id AS ServiceOrderDetailDistributionId
		, SODD.RevenueControlDetailId
		, SODD.Quantity
		, sodd.ServiceOrderDetailId
		, SOD.ServiceOrderId
		, SO.Code as ServiceOrderCode
		, SOD.PerformsFunctionalUnitId
		, FU.Code as FunctionalUnitCode
		, FU.Name as FunctionalUnitName
		, cc.Code as CostCenterCode
		, cc.Name as CostCenterName
		, sod.AuthorizationNumber
		, sod.SurgeryNumber
		, sod.PerformsHealthProfessionalCode
		, sod.PerformsProfessionalSpecialty
		, CONCAT(ISNULL(BG1.Code, BG2.Code), ' - ', ISNULL(BG1.Name, BG2.Name)) AS ServiceBillingGroupCodeName
		, SOD.ProductId
		, case when sod.ProductId is null then '' else ip.Code end as ProductCode
		, case when sod.ProductId is null then '' else ip.Name end as ProductName
		, case when sod.IPSServiceId is not null then ipss.Code else isnull(ip.Code, '') end as ServiceCode
		, case when sod.IPSServiceId is not null then ipss.Name else isnull(ip.Name, '') end as ServiceName
		, case when sod.IPSServiceId is not null then ipss.Code else '' end as IpsServiceCode
		, case when sod.IPSServiceId is not null then ipss.Name else '' end as IpsServiceName
		, case when sod.IPSServiceId is not null then ipss.POS else null end as POSService
		, bi.Code as BillingConceptCode
		, bi.Name as BillingConceptName
		, case when sod.CUPSEntityId is not null then CUPSE.Code else null end as CupsCode
		, case when sod.CUPSEntityId is not null then CUPSE.Description else null end as CupsDescription
		, case when ccd.ContractDescriptionId is not null then cd.Code else null end as ContractDescriptionCode
		, case when ccd.ContractDescriptionId is not null then cd.Name else null end as ContractDescriptionName
		, bg2.Code as ProductBillingGroupCode
		, bg2.Name as ProductBillingGroupName
		, ip.POSProduct
		, AT.Code as AtcCode
		, sod.ServiceDate
		, sod.RecordType
		, sod.Presentation
		, sod.SettlementType
		, sodd.DistributionType
		, sod.SupplyQuantity
		, sod.DevolutionQuantity
		, sodd.ThirdPartyPercentage
		, sodd.ApplyRecoveryFee
		, sodd.RecoveryFeeType
		, sodd.PatientPercentage
		, sod.IsPackage
		, sod.CodeAssociateService
		, sod.ApplyRIAS
		, sod.RIASCupsId
		, sod.ProductLiquidationType
		, iif(GLI.Id is not null , CONCAT(GLI.Percentage, '%'), null) AS IvaPercentage
		, sodd.GrandTotalTaxes
		, sod.TaxValue
		, sod.GrossValue
		, case when sod.ProductId is not null then pt.Class else null end as ProductTypeClass
		, case when sod.DefinitionRateDetailId is not null then drd.AllowValueChange else null end as AllowValueChange
		, sodd.SubTotalPatientSalesPrice
		, sodd.ThirdPartySalesPrice
		, sod.TotalSalesPrice
		, sod.ThirdPartyDiscountPercentage
		, sodd.GrandTotalDiscount
		, sod.ThirdPartyDiscount
		, sodd.SubTotalSalesPrice
		, sodd.GrandTotalSalesPrice
		, sod.RateManualSalePrice
		, sod.CostValue
	FROM Billing.ServiceOrderDetailDistribution SODD
	JOIN Billing.ServiceOrderDetail SOD ON SODD.ServiceOrderDetailId = SOD.Id AND SOD.InvoicedQuantity > 0 AND SOD.IsDelete = 0
	JOIN Billing.ServiceOrder SO ON SOD.ServiceOrderId = SO.Id 
	JOIN Payroll.FunctionalUnit FU ON SOD.PerformsFunctionalUnitId = FU.Id 
	JOIN Payroll.CostCenter CC ON SOD.CostCenterId = CC.Id 
	LEFT JOIN Billing.BillingConcept BI ON SOD.BillingConceptId = BI.Id 
	LEFT JOIN Contract.DefinitionRateDetail DRD ON SOD.DefinitionRateDetailId = DRD.Id
	LEFT JOIN Contract.IPSService IPSS ON SOD.IPSServiceId = IPSS.Id 
	LEFT JOIN GeneralLedger.GeneralLedgerIVA GLI ON GLI.Id = sod.IVAId
	LEFT JOIN Contract.CUPSEntity CUPSE ON SOD.CUPSEntityId = CUPSE.Id 
	LEFT JOIN Contract.CUPSEntityContractDescriptions ccd ON SOD.CUPSEntityContractDescriptionId = ccd.Id
	LEFT JOIN Contract.ContractDescriptions cd ON ccd.ContractDescriptionId = cd.Id
	LEFT JOIN Billing.BillingGroup BG1  ON IIF(ISNULL(SOD.ApplyRIAS, 0) = 1, ISNULL(CUPSE.RIASBillingGroupId, CUPSE.BillingGroupId), IIF(ISNULL(ccd.Id, 0) > 0, ccd.BillingGroupId, CUPSE.BillingGroupId)) = BG1.Id 
	LEFT JOIN Inventory.InventoryProduct IP ON SOD.ProductId = IP.Id 
	LEFT JOIN Billing.BillingGroup BG2 ON IP.BillingGroupId = BG2.Id 
	LEFT JOIN [Inventory].[ProductType] pt with(nolock) on ip.ProductTypeId = pt.Id
	LEFT JOIN Inventory.ATC AT ON IP.ATCId = AT.Id 
), CTE_ApplyLogicThirdPartyBeneficiary AS (
		SELECT rcd.Id RevenueControlDetailId 
		FROM ADINGRESO a
		JOIN Contract.CareGroup cg ON cg.Id = a.GENCAREGROUP
		JOIN Billing.RevenueControl	 rc ON rc.AdmissionNumber = a.NUMINGRES
		JOIN Billing.RevenueControlDetail rcd ON rcd.RevenueControlId = rc.Id
		WHERE cg.LiquidationType NOT IN (2,5) AND cg.CareGroupType <> 3 AND rcd.IsMasterAccount IN (2,0))
SELECT
		CONCAT(I.Id, ' - ', ISNULL(SOCTE.ServiceOrderDetailDistributionId, - 1), ' - ', RCD.Id) AS IdKey, 		
		THP.Id AS ThirdPartyPatientId, 
		RC.PatientCode, 
		RC.AdmissionNumber, 
		CONCAT(LTRIM(RTRIM(RC.AdmissionNumber)), ' - Paciente: ', LTRIM(RTRIM(RC.PatientCode)), ' - ', THP.Name) AS AdmissionNumberPatient, 
		RCD.Id AS RevenueControlDetailId, 
		RCD.FolioOrder, 
		RCD.InvoiceCategoryId, 
		CASE WHEN RCD.InvoiceCategoryId IS NULL THEN ' - ' ELSE CONCAT(IC.Code, ' - ', IC.Name) END AS InvoiceCategoryCodeName,
		ISNULL(RCD.BillingAuthorizationId, - 1) AS BillingAuthorizationId, 
		TP.Id AS ThirdPartyId, 
		CONCAT(TP.Nit, ' - ', TP.Name) AS ThirdPartyNitName, 
		CG.Id AS CareGroupId, 
		CG.EntityType AS CaregroupEntityType, 
		CONCAT(CG.Code, ' - ', CG.Name) AS CareGroupCodeName, 
		CASE WHEN CG.ContractId IS NULL THEN ' - ' ELSE CONCAT(C.Code, ' - ', C.ContractName) END AS ContractCodeName,
		ISNULL(RCD.HealthAdministratorId, 0) AS HealthAdministratorId,
		CASE WHEN RCD.HealthAdministratorId IS NULL THEN ' - ' ELSE CONCAT(HA.Code, ' - ', HA.Name) END AS HealthAdministratorCodeName,
		CASE WHEN RCD.HealthAdministratorId IS NULL THEN NULL ELSE HA.ThirdPartyId END AS ThirdPartyHealthAdministrator,
		ISNULL(RCD.ContractEntityId, 0) AS ContractEntityId, 
		CASE WHEN RCD.ContractEntityId IS NULL THEN ' - ' ELSE CONCAT(CE.Code, ' - ', CE.Name) END AS ContractEntityCodeName,
		ISNULL(I.Id, - 1) AS InvoiceId, 
		ISNULL(I.InvoiceNumber, '') AS InvoiceNumber, 
		ISNULL(I.InvoiceDate, '') AS InvoiceDate, 
		ISNULL(I.InvoicedUser, '') AS InvoicedUser, 
		CG.CostCenterId CareGroupCostCenterId, 
		CG.LiquidationType,
		RCD.FolioType,
		RCD.Observation, 
		ROUND(Common.[CurrencyConverterByModule](rcd.TotalFolio,  cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.OperatingUnitId,'Invoice', i.InvoiceDate),2) AS TotalFolio, 
		RCD.ResponsibleRecoveryFee, 
		RCD.PatientDiscountPercentage, 
		RCD.PatientDiscount, 
		ISNULL(RCD.TotalPatientSalesPrice, 0) AS TotalPatientSalesPrice, 
		ISNULL(RCD.TotalPatientWithDiscount, 0) AS TotalPatientWithDiscount, 
		ISNULL(RCD.ValueVoucher, 0) AS VoucherValue, 
		RCD.Status,
		RCD.StatusFolioId,
		CONCAT(CSS.Code,'-',CSS.Name) AS StatusFolioName,
		RCD.IsMasterAccount As IsMasterAccount,
		SOCTE.ServiceOrderId AS ServiceOrderId, 
		SOCTE.ServiceOrderCode AS ServiceOrderCode, 
		ISNULL(SOCTE.ServiceOrderDetailDistributionId, - 1) AS Id, 
		SOCTE.ServiceOrderDetailId AS ServiceOrderDetailId, 
		CONCAT(SOCTE.FunctionalUnitCode, ' - ', SOCTE.FunctionalUnitName) AS PerformsFunctionalUnitCodeName, 		
		CONCAT(SOCTE.CostCenterCode, ' - ', SOCTE.CostCenterName) AS CostCenterCodeName, 
		ISNULL(SOCTE.AuthorizationNumber, '0') AS AuthorizationNumber, 
		SOCTE.SurgeryNumber,
		ISNULL(SOCTE.PerformsHealthProfessionalCode, '') AS PerformsHealthProfessionalCode, 
		ISNULL(SOCTE.PerformsProfessionalSpecialty, - 1) AS PerformsProfessionalSpecialty, 
		SOCTE.ServiceBillingGroupCodeName,
		SOCTE.ServiceCode AS ServiceCode, 
		SOCTE.ServiceName AS ServiceName, 
		CONCAT(SOCTE.BillingConceptCode, ' - ', SOCTE.BillingConceptName) AS IPSServiceGroupCodeName, 		
		SOCTE.IpsServiceCode AS IPSServiceCode, 
		SOCTE.IpsServiceName AS IPSServiceName, 
		SOCTE.POSService AS IsPOSService,
		CONCAT(SOCTE.CupsCode, ' - ', SOCTE.CupsDescription) AS CUPS, 
		CONCAT(SOCTE.ContractDescriptionCode, ' - ', SOCTE.ContractDescriptionName) AS ContractDescriptionCodeName, 
		CONCAT(SOCTE.ProductBillingGroupCode, ' - ', SOCTE.ProductBillingGroupName) AS ProductBillingGroupCodeName, 
		ISNULL(SOCTE.ProductId, 0) AS ProductId,
		SOCTE.ProductCode AS ProductCode, 		
		SOCTE.ProductName AS ProductName, 
		SOCTE.POSProduct AS IsPOSProduct, 
		SOCTE.AtcCode AS ProductATCCode, 
		CAST(ISNULL(POSP.IdParticion,0) AS BIT) AS HasPathologies,
		ISNULL(SOCTE.ServiceDate, Common.[GETDATE]()) AS ServiceDate, 
		ISNULL(SOCTE.RecordType, 1) AS RecordType, 
		SOCTE.Presentation, 
		SOCTE.SettlementType AS SettlementType, 
		ISNULL(SOCTE.DistributionType, 1) AS DistributionType, 
		Case ISNULL(SOCTE.DistributionType, 1) 
			When 1 Then 'Ninguno' 
			When 2 Then 'Distribución Normal' 
			When 3 Then 'Distribución por Corte de Cuentas' 
			When 4 Then 'Distribución por Unidad' 
			When 5 Then 'Ninguno NoPOS' 
			Else '' 
		End AS DistributionTypeName, 
		ISNULL(SOCTE.Quantity, 0) AS InvoicedQuantity, 
		ISNULL(SOCTE.SupplyQuantity, 0) AS SupplyQuantity, 
		ISNULL(SOCTE.DevolutionQuantity, 0) AS DevolutionQuantity, 
		ROUND(Common.[CurrencyConverterByModule](SOCTE.CostValue, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.OperatingUnitId,'Invoice',i.InvoiceDate),2) AS CostValue,
		ROUND(Common.[CurrencyConverterByModule](SOCTE.RateManualSalePrice, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.OperatingUnitId,'Invoice', i.InvoiceDate),2) AS RateManualSalePrice,
		ROUND(Common.[CurrencyConverterByModule](SOCTE.GrandTotalSalesPrice, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.OperatingUnitId,'Invoice', i.InvoiceDate),2) AS GrandTotalSalesPrice,
		ROUND(Common.[CurrencyConverterByModule](SOCTE.SubTotalSalesPrice, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.OperatingUnitId,'Invoice', i.InvoiceDate),2) AS SubTotalSalesPrice, 
		ROUND(Common.[CurrencyConverterByModule](SOCTE.ThirdPartyDiscount, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.OperatingUnitId,'Invoice', i.InvoiceDate),2) AS ThirdPartyDiscount, 
		ROUND(Common.[CurrencyConverterByModule](SOCTE.GrandTotalDiscount, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.OperatingUnitId,'Invoice', i.InvoiceDate),2) AS GrandTotalDiscount,
		ISNULL(SOCTE.ThirdPartyDiscountPercentage, 0) AS ThirdPartyDiscountPercentage, 
		ROUND(Common.[CurrencyConverterByModule](SOCTE.TotalSalesPrice, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId), i.OperatingUnitId,'Invoice',i.InvoiceDate),2) AS TotalSalesPrice,
		ROUND(Common.[CurrencyConverterByModule](SOCTE.ThirdPartySalesPrice, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId), i.OperatingUnitId,'Invoice',i.InvoiceDate),2) AS ThirdPartySalesPrice,
		ISNULL(SOCTE.ThirdPartyPercentage, 0) AS ThirdPartyPercentage, 
		IIF(SOCTE.SettlementType = 3, 'Si', 'No') AS SurchargeApply, 
		ISNULL(SOCTE.RecoveryFeeType, 0) AS RecoveryFeeType, 
		ISNULL(SOCTE.ApplyRecoveryFee, 1) AS ApplyRecoveryFee,  
		ROUND(Common.[CurrencyConverterByModule](SOCTE.SubTotalPatientSalesPrice, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.OperatingUnitId,'Invoice', i.InvoiceDate),2) AS SubTotalPatientSalesPrice,
		ISNULL(SOCTE.PatientPercentage, 0) / 100 AS PatientPercentage, 		
		ISNULL(SOCTE.IsPackage, 0) AS IsPackage, 
		SOCTE.CodeAssociateService AS CodeAssociateService,
		ISNULL(SOCTE.CodeAssociateService, '') AS GuidHomologation, 
		ISNULL(SOCTE.ApplyRIAS, 0) ApplyRIAS, 
		ISNULL(SOCTE.RIASCupsId, 0) RIASCupsId,
		ISNULL(SOCTE.AllowValueChange, 0) AS AllowValueChange,
		SUBSTRING((SELECT ',' + code  AS [text()] FROM Billing.MipresCode mc WHERE mc.ServiceOrderDetailId = SOCTE.ServiceOrderDetailId FOR XML PATH ('')), 2, 1000) AS Mipres,
		SUBSTRING((SELECT ',' + IdMipres  AS [text()] FROM Billing.MipresCode mc WHERE mc.ServiceOrderDetailId = SOCTE.ServiceOrderDetailId FOR XML PATH ('')), 2, 1000) AS IdMipres,
		Case When SOCTE.ProductTypeClass = 5 Then 1 Else 0 End As IsItemProduction,
		Case When SOCTE.ProductTypeClass = 5 then 99 else ISNULL(SOCTE.DistributionType, 1) end as IconType,
		ISNULL((SELECT top 1 1 from Billing.ProductServiceDetail psd WITH(NOLOCK) WHERE psd.ServiceOrderDetailId = SOCTE.ServiceOrderDetailId),0) FlagProductServiceDetail,
		SOCTE.ProductLiquidationType,
		CASE WHEN RCD.PatientQuotaResponsibleThirdPartyId IS NULL THEN ' - ' ELSE CONCAT(THPR.Nit, ' - ', THPR.Name) END AS ThirdPartyResponsibleQuotaNitName,
		RCD.PatientQuotaResponsibleThirdPartyId AS PatientQuotaResponsibleThirdPartyId,
		cg.MaximumTypeTop,isnull(cu.Abbreviation,'') CurrencyAbbreviation,
		ISNULL(cgm.[Value],0) AS MaximumTopValue,
		ISNULL(ai.ISOATVALO,0) AS InvoiceExternalValue,
		ISNULL(
			(
			SELECT SUM(A.TotalInvoice)
            FROM [Billing].[Invoice] A INNER JOIN dbo.ADINGRESO B On A.AdmissionNumber = B.NUMINGRES
            WHERE A.PatientCode = RC.PatientCode AND B.ITIPORIES = '2' AND CONVERT(date, B.FECACTRAN) = ai.FECACTRAN
			), 0) as InvoiceCurrentValue,
		SOCTE.IvaPercentage,
		ROUND(Common.[CurrencyConverterByModule](SOCTE.TaxValue, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId), i.OperatingUnitId,'Invoice',i.InvoiceDate),2) AS IvaTotalValue,
		ROUND(Common.[CurrencyConverterByModule](SOCTE.GrossValue, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.OperatingUnitId,'Invoice', i.InvoiceDate),2) AS GrossValue,
		SOCTE.GrandTotalTaxes,
		fnc.Id as FeeNotCollectedId,
		icy.BasicBillingId BasicbillingCopayId,
		IIF(Tmp.RevenueControlDetailId IS NOT NULL, 1, 0) ApplyLogicThirdPartyBeneficiary
FROM Billing.RevenueControl RC 
JOIN Billing.RevenueControlDetail RCD ON RC.Id = RCD.RevenueControlId
JOIN Common.ThirdParty TP ON RCD.ThirdPartyId = TP.Id 
JOIN Contract.CareGroup CG ON RCD.CareGroupId = CG.Id 
JOIN Common.ThirdParty THP ON RC.PatientCode = THP.Nit
LEFT JOIN [Contract].[Contract] C ON CG.ContractId = C.Id
LEFT JOIN Billing.InvoiceCategories IC ON RCD.InvoiceCategoryId = IC.Id 
LEFT JOIN Common.ThirdParty THPR  ON RCD.PatientQuotaResponsibleThirdPartyId = THPR.Id
LEFT JOIN Contract.HealthAdministrator HA ON RCD.HealthAdministratorId = HA.Id 
LEFT JOIN Contract.ContractEntity CE ON RCD.ContractEntityId = CE.Id 
LEFT JOIN Billing.RevenueControlDetailInvoice rcdi ON rcd.Id = rcdi.RevenueControlDetailId AND rcdi.[Status] = 1
LEFT JOIN Billing.Invoice I ON (RCD.Id = I.RevenueControlDetailId OR rcdi.InvoiceId = I.Id) AND I.Status = 1 
LEFT JOIN billing.InvoiceCopay icy ON icy.InvoiceId = I.Id
LEFT JOIN Billing.ConceptsCausesStatusFolio CSS WITH(NOLOCK) ON RCD.StatusFolioId = CSS.Id
LEFT JOIN SERVICE_ORDER_CTE SOCTE ON RCD.Id = SOCTE.RevenueControlDetailId AND SOCTE.Quantity > 0
LEFT JOIN dbo.ADINGRESO ai ON ai.NUMINGRES=rc.AdmissionNumber
LEFT JOIN [Contract].CareGroupMaximumTop cgm ON cgm.CareGroupId = cg.Id and YEAR(ai.FECACTRAN) = cgm.[Year]
LEFT JOIN 
(
	SELECT ROW_NUMBER() OVER (PARTITION BY PP.ProductId ORDER BY PP.Id) IdParticion, PP.ProductId 
	FROM Inventory.POSPathologies PP 
	WHERE PP.ProductId IS NOT NULL
) POSP ON POSP.IdParticion = 1 AND POSP.ProductId = SOCTE.ProductId
LEFT JOIN Common.Currency cu WITH(NOLOCK) on i.CurrencyId = cu.Id
LEFT JOIN Billing.FeeNotCollected fnc WITH(NOLOCK) on fnc.RevenueControlDetailId = RCD.Id
LEFT JOIN CTE_ApplyLogicThirdPartyBeneficiary Tmp ON Tmp.RevenueControlDetailId = rcd.Id
--------------------------------------------------------------------
OUTER APPLY GeneralLedger.CompanySettings cs