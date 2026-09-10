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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que lista los servicios y productos incluidos en un folio de control de ingresos (revenue control), dado un identificador de detalle de folio. Según si el folio ya está facturado o no, consulta las distribuciones de órdenes de servicio junto con información de contratos, entidades, administradoras de salud, terceros, grupos de atención, unidades funcionales, centros de costo y grupos de facturación, consolidando valores como precio de venta, porcentaje y valor a cargo del tercero pagador, subtotal y total a cargo del paciente, descuentos, cuota moderadora, cantidades facturadas, suministradas y devueltas, así como el precio manual de tarifa. Se usa en el módulo de facturación para revisar el detalle económico y de servicios de un folio antes o después de su facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ListRevenueControlById';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ListRevenueControlById';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el detalle de servicios distribuidos asociados a un folio (RevenueControlDetail), enriquecido con datos de tercero, contratante, administradora, grupo de atención, producto/servicio, unidad funcional y centro de costo.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ListRevenueControlById';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El RevenueControlDetailId debe existir en Billing.RevenueControlDetail y tener distribuciones en Billing.ServiceOrderDetailDistribution para retornar filas.; Los detalles asociados deben tener tercero (ThirdParty), grupo de atención (CareGroup), unidad funcional, grupo de servicio IPS y centro de costo válidos (joins INNER).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ListRevenueControlById';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre filtra por RCD.Id = @RevenueControlDetailId.; Siempre concatena Code/Nit con '' - '' y Name para construir los campos *CodeName/*NitName.; Las relaciones con ContractEntity, HealthAdministrator, InventoryProduct, IPSService, CUPSEntity y BillingGroup son opcionales (LEFT JOIN); las demás son obligatorias (INNER JOIN).; El grupo de facturación del servicio (BG1) proviene de CUPSEntity.BillingGroupId y el del producto (BG2) de InventoryProduct.BillingGroupId.; Aunque @Invoiced bifurca el flujo, ambas ramas generan el mismo SQL, por lo que el parámetro no afecta el resultado actualmente.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ListRevenueControlById';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'folio de facturación; control de ingresos (RevenueControl); distribución de venta tercero/paciente; copago/cuota moderadora (PatientDiscount, ResponsibleRecoveryFee); número de autorización; servicio IPS / CUPS; grupo de facturación; unidad funcional; centro de costo; administradora de salud (EPS); entidad contratante', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ListRevenueControlById';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] result set: Retorna las filas de ServiceOrderDetailDistribution y ServiceOrderDetail filtradas por RCD.Id = @RevenueControlDetailId, numeradas con ROW_NUMBER() ordenado por SODD.Id ASC.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ListRevenueControlById';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Invoiced = 1 → Construye la consulta dinámica para servicios facturados. else Construye una consulta dinámica idéntica para servicios no facturados (no hay diferencia funcional entre ambas ramas en el SQL actual).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ListRevenueControlById';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ListRevenueControlById';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControlDetail; Common.ThirdParty; Contract.CareGroup; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; Payroll.FunctionalUnit; Contract.IPSServiceGroup; Payroll.CostCenter; Contract.ContractEntity; Contract.HealthAdministrator; Inventory.InventoryProduct; Contract.IPSService; Contract.CUPSEntity; Billing.BillingGroup', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ListRevenueControlById';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ListRevenueControlById';
-- GO
