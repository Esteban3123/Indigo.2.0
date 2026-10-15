-- =============================================
-- Author:		Juan F. Tamayo
-- Create date: 2014-12-02
-- Description:	Lista los servicios de un fólio
-- =============================================
CREATE PROCEDURE [Billing].[SP_ListRevenueControlById]
	-- Add the parameters for the stored procedure here
	@Invoiced Bit, @RevenueControlDetailId Int
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @AuxTable AS Table(
		Row Bigint,
		Id Int,
		RevenueControlDetailId Int,
		FolioOrder Tinyint,
		FolioType Tinyint,
		ContractEntityCodeName Varchar(123),
		HealthAdministratorCodeName Varchar(123),
		ThirdPartyNitName Varchar(118),
		CareGroupCodeName Varchar(123),
		TotalFolio Numeric(18, 0),
		TotalSalesPrice1 Numeric(18, 0),
		DistributionType Tinyint,
		ThirdPartySalesPrice Numeric(18, 0),
		ThirdPartyPercentage Numeric(5, 2),
		ResponsibleRecoveryFee Tinyint,
		SubTotalPatientSalesPrice Numeric(18, 0),
		PatientPercentage Numeric(5, 2),
		PatientDiscountPercentage Numeric(5, 2),
		PatientDiscount Numeric(18, 0),
		TotalPatientSalesPrice Numeric(18, 0),
		IPSServiceCodeName Varchar(123),
		ProductCodeName Varchar(123),
		ServiceBillingGroupCodeName Varchar(123),
		ProductBillingGroupCodeName Varchar(123),
		InvoicedQuantity Int,
		SupplyQuantity Int,
		DevolutionQuantity Int,
		RateManualSalePrice Numeric(18, 0),
		RecoveryFeeType Tinyint,
		CostValue Numeric(18, 2),
		ServiceDate DateTime,
		AuthorizationNumber Varchar(20),
		PerformsFunctionalUnitCodeName Varchar(73),
		PerformsHealthProfessionalCode Char(20),
		PerformsProfessionalSpecialty Char(3),
		IPSServiceGroupCodeName Varchar(123),
		CostCenterCodeName Varchar(223),
		SubTotalSalesPrice Numeric(18, 0),
		ThirdPartyDiscount Numeric(18, 0),
		ThirdPartyDiscountPercentage Numeric(5, 2),
		TotalSalesPrice2 Numeric(18, 0)
	)

	DECLARE @Sql AS NVarchar(MAX)

	IF @Invoiced = 1 
	BEGIN
		SET @Sql = 'SELECT ROW_NUMBER() OVER (ORDER BY SODD.Id ASC) AS Row, SODD.Id, RCD.Id AS RevenueControlDetailId, RCD.FolioOrder, RCD.FolioType, CONCAT(CONCAT(CE.Code, '' - ''), CE.Name) AS ContractEntityCodeName, CONCAT(CONCAT(HA.Code, '' - ''), HA.Name) AS HealthAdministratorCodeName,
				CONCAT(CONCAT(TP.Nit, '' - ''), TP.Name) AS ThirdPartyNitName, CONCAT(CONCAT(CG.Code, '' - ''), CG.Name) AS CareGroupCodeName, RCD.TotalFolio, SODD.TotalSalesPrice AS TotalSalesPrice1, SODD.DistributionType, SODD.ThirdPartySalesPrice, SODD.ThirdPartyPercentage,
				SODD.ResponsibleRecoveryFee, SODD.SubTotalPatientSalesPrice, SODD.PatientPercentage, SODD.PatientDiscountPercentage, SODD.PatientDiscount, SODD.TotalPatientSalesPrice, CONCAT(CONCAT(IPSS.Code, '' - ''), IPSS.Name) AS IPSServiceCodeName,
				CONCAT(CONCAT(IP.Code, '' - ''), IP.Name) AS ProductCodeName, CONCAT(CONCAT(BG1.Code, '' - ''), BG1.Name) AS ServiceBillingGroupCodeName, CONCAT(CONCAT(BG2.Code, '' - ''), BG2.Name) AS ProductBillingGroupCodeName, SOD.InvoicedQuantity,
				SOD.SupplyQuantity, SOD.DevolutionQuantity, SOD.RateManualSalePrice, SOD.RecoveryFeeType, SOD.CostValue, SOD.ServiceDate, SOD.AuthorizationNumber, CONCAT(CONCAT(FU.Code, '' - ''), FU.Name) AS PerformsFunctionalUnitCodeName,
				SOD.PerformsHealthProfessionalCode, SOD.PerformsProfessionalSpecialty, CONCAT(CONCAT(IPSSG.Code, '' - ''), IPSSG.Name) AS IPSServiceGroupCodeName, CONCAT(CONCAT(CC.Code, '' - ''), CC.Name) AS CostCenterCodeName, SOD.SubTotalSalesPrice, SOD.ThirdPartyDiscount, SOD.ThirdPartyDiscountPercentage, SOD.TotalSalesPrice AS TotalSalesPrice2
				FROM Billing.RevenueControlDetail RCD
				INNER JOIN Common.ThirdParty TP ON RCD.ThirdPartyId = TP.Id
				INNER JOIN Contract.CareGroup CG ON RCD.CareGroupId = CG.Id
				INNER JOIN Billing.ServiceOrderDetailDistribution SODD ON RCD.Id = SODD.RevenueControlDetailId
				INNER JOIN Billing.ServiceOrderDetail SOD ON SODD.ServiceOrderDetailId = SOD.Id
				INNER JOIN Payroll.FunctionalUnit FU ON SOD.PerformsFunctionalUnitId = FU.Id
				INNER JOIN Contract.IPSServiceGroup IPSSG ON SOD.IPSServiceGroupId = IPSSG.Id
				INNER JOIN Payroll.CostCenter CC ON SOD.CostCenterId = CC.Id
				LEFT JOIN Contract.ContractEntity CE ON RCD.ContractEntityId = CE.Id
				LEFT JOIN Contract.HealthAdministrator HA ON RCD.HealthAdministratorId = HA.Id
				LEFT JOIN Inventory.InventoryProduct IP ON SOD.ProductId = IP.Id
				LEFT JOIN Contract.IPSService IPSS ON SOD.IPSServiceId = IPSS.Id
				LEFT JOIN Contract.CUPSEntity CUPSE ON SOD.CUPSEntityId = CUPSE.Id
				LEFT JOIN Billing.BillingGroup BG1 ON CUPSE.BillingGroupId = BG1.Id
				LEFT JOIN Billing.BillingGroup BG2 ON IP.BillingGroupId = BG2.Id
				WHERE RCD.Id = @RevenueControlDetailId'
	END
	ELSE
	BEGIN
		SET @Sql = 'SELECT ROW_NUMBER() OVER (ORDER BY SODD.Id ASC) AS Row, SODD.Id, RCD.Id AS RevenueControlDetailId, RCD.FolioOrder, RCD.FolioType, CONCAT(CONCAT(CE.Code, '' - ''), CE.Name) AS ContractEntityCodeName, CONCAT(CONCAT(HA.Code, '' - ''), HA.Name) AS HealthAdministratorCodeName,
				CONCAT(CONCAT(TP.Nit, '' - ''), TP.Name) AS ThirdPartyNitName, CONCAT(CONCAT(CG.Code, '' - ''), CG.Name) AS CareGroupCodeName, RCD.TotalFolio, SODD.TotalSalesPrice AS TotalSalesPrice1, SODD.DistributionType, SODD.ThirdPartySalesPrice, SODD.ThirdPartyPercentage,
				SODD.ResponsibleRecoveryFee, SODD.SubTotalPatientSalesPrice, SODD.PatientPercentage, SODD.PatientDiscountPercentage, SODD.PatientDiscount, SODD.TotalPatientSalesPrice, CONCAT(CONCAT(IPSS.Code, '' - ''), IPSS.Name) AS IPSServiceCodeName,
				CONCAT(CONCAT(IP.Code, '' - ''), IP.Name) AS ProductCodeName, CONCAT(CONCAT(BG1.Code, '' - ''), BG1.Name) AS ServiceBillingGroupCodeName, CONCAT(CONCAT(BG2.Code, '' - ''), BG2.Name) AS ProductBillingGroupCodeName, SOD.InvoicedQuantity,
				SOD.SupplyQuantity, SOD.DevolutionQuantity, SOD.RateManualSalePrice, SOD.RecoveryFeeType, SOD.CostValue, SOD.ServiceDate, SOD.AuthorizationNumber, CONCAT(CONCAT(FU.Code, '' - ''), FU.Name) AS PerformsFunctionalUnitCodeName,
				SOD.PerformsHealthProfessionalCode, SOD.PerformsProfessionalSpecialty, CONCAT(CONCAT(IPSSG.Code, '' - ''), IPSSG.Name) AS IPSServiceGroupCodeName, CONCAT(CONCAT(CC.Code, '' - ''), CC.Name) AS CostCenterCodeName, SOD.SubTotalSalesPrice, SOD.ThirdPartyDiscount, SOD.ThirdPartyDiscountPercentage, SOD.TotalSalesPrice AS TotalSalesPrice2
				FROM Billing.RevenueControlDetail RCD
				INNER JOIN Common.ThirdParty TP ON RCD.ThirdPartyId = TP.Id
				INNER JOIN Contract.CareGroup CG ON RCD.CareGroupId = CG.Id
				INNER JOIN Billing.ServiceOrderDetailDistribution SODD ON RCD.Id = SODD.RevenueControlDetailId
				INNER JOIN Billing.ServiceOrderDetail SOD ON SODD.ServiceOrderDetailId = SOD.Id
				INNER JOIN Payroll.FunctionalUnit FU ON SOD.PerformsFunctionalUnitId = FU.Id
				INNER JOIN Contract.IPSServiceGroup IPSSG ON SOD.IPSServiceGroupId = IPSSG.Id
				INNER JOIN Payroll.CostCenter CC ON SOD.CostCenterId = CC.Id
				LEFT JOIN Contract.ContractEntity CE ON RCD.ContractEntityId = CE.Id
				LEFT JOIN Contract.HealthAdministrator HA ON RCD.HealthAdministratorId = HA.Id
				LEFT JOIN Inventory.InventoryProduct IP ON SOD.ProductId = IP.Id
				LEFT JOIN Contract.IPSService IPSS ON SOD.IPSServiceId = IPSS.Id
				LEFT JOIN Contract.CUPSEntity CUPSE ON SOD.CUPSEntityId = CUPSE.Id
				LEFT JOIN Billing.BillingGroup BG1 ON CUPSE.BillingGroupId = BG1.Id
				LEFT JOIN Billing.BillingGroup BG2 ON IP.BillingGroupId = BG2.Id
				WHERE RCD.Id = @RevenueControlDetailId'
	END

	INSERT INTO @AuxTable EXECUTE sp_executesql @Sql, N'@RevenueControlDetailId Int', @RevenueControlDetailId

	SELECT Row, Id, RevenueControlDetailId, FolioOrder, FolioType, ContractEntityCodeName, HealthAdministratorCodeName, ThirdPartyNitName, CareGroupCodeName, TotalFolio, TotalSalesPrice1, DistributionType, ThirdPartySalesPrice, ThirdPartyPercentage, ResponsibleRecoveryFee, SubTotalPatientSalesPrice, PatientPercentage, PatientDiscountPercentage, PatientDiscount, TotalPatientSalesPrice, IPSServiceCodeName, ProductCodeName, ServiceBillingGroupCodeName, ProductBillingGroupCodeName, InvoicedQuantity, SupplyQuantity, DevolutionQuantity, RateManualSalePrice, RecoveryFeeType, CostValue, ServiceDate, AuthorizationNumber, PerformsFunctionalUnitCodeName, PerformsHealthProfessionalCode, PerformsProfessionalSpecialty, IPSServiceGroupCodeName, CostCenterCodeName, SubTotalSalesPrice, ThirdPartyDiscount, ThirdPartyDiscountPercentage, TotalSalesPrice2 FROM @AuxTable
END