-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-07-08
-- Description:	Reversion de la estimacion de costos
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReverseEstimateCostNative]
	@Year INT,
	@Month INT,
	@UserCode VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY
		/********************************************  DISTRIBUCION FINAL ********************************************/
		
		DELETE cmce
		FROM [Cost].[ClosedMonth] cm 
		JOIN [Cost].[ClosedMonthCUPSEntity] cmce ON cm.Id = cmce.ClosedMonthId
		WHERE cm.Year = @Year AND cm.Month = @Month

		DELETE cmca
		FROM [Cost].[ClosedMonth] cm 
		JOIN [Cost].[ClosedMonthCostActivity] cmca ON cm.Id = cmca.ClosedMonthId
		WHERE cm.Year = @Year AND cm.Month = @Month

		DELETE cmi
		FROM [Cost].[ClosedMonth] cm 
		JOIN [Cost].[ClosedMonthInventory] cmi ON cm.Id = cmi.ClosedMonthId
		WHERE cm.Year = @Year AND cm.Month = @Month

		DELETE cmp
		FROM [Cost].[ClosedMonth] cm 
		JOIN [Cost].[ClosedMonthPayroll] cmp ON cm.Id = cmp.ClosedMonthId
		WHERE cm.Year = @Year AND cm.Month = @Month

		DELETE cmfa
		FROM [Cost].[ClosedMonth] cm 
		JOIN [Cost].[ClosedMonthFixedAsset] cmfa ON cm.Id = cmfa.ClosedMonthId
		WHERE cm.Year = @Year AND cm.Month = @Month

		DELETE cmcebcc
		FROM [Cost].[ClosedMonth] cm 
		JOIN [Cost].[ClosedMonthCUPSEntityByCostCenter] cmcebcc ON cm.Id = cmcebcc.ClosedMonthId
		WHERE cm.Year = @Year AND cm.Month = @Month

		DELETE cm
		FROM [Cost].[ClosedMonth] cm 
		WHERE cm.Year = @Year AND cm.Month = @Month

		/****************************************** DISTRIBUCION SECUNDARIA ******************************************/

		UPDATE cen
			SET cen.SecondaryDirectCostDistribution = 0,
				cen.SecondaryAutoCostDistribution = 0,
				cen.SecondaryManPowerDistributionDirect = 0,
				cen.SecondaryManPowerDistributionInDirect = 0,
				cen.SecondaryFixedAssetDistribution = 0,
				cen.SecondaryDispensingDistribution = 0,
				cen.SecondaryTransferDistribution = 0,
				cen.SecondaryDistribution = 0
		FROM Cost.CostEstimationNative cen
		WHERE cen.Year = @Year AND cen.Month = @Month

		UPDATE cdds
			SET cdds.Status = 1
		FROM Cost.CostDirectDistributionSecondary cdds
		JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
		LEFT JOIN Cost.CostDirectDistributionSecondaryDetailRedistribution cddsdr ON cddsd.Id = cddsdr.DirectDistributionSecondaryDetailId
		WHERE cdds.Year = @Year AND cdds.Month = @Month AND cdds.Status = 2

		/****************************************** DISTRIBUCION PRIMARIA ******************************************/

		DELETE cdmi 
		FROM [Cost].[CostDistributionManpowerInitial] cdmi
		WHERE cdmi.Year = @Year AND cdmi.Month = @Month

		DELETE cdfai
		FROM [Cost].[CostDistributionFixedAssetInitial] cdfai
		WHERE cdfai.Year = @Year AND cdfai.Month = @Month

		UPDATE cen
			SET cen.DirectCostDistribution = 0,
				cen.AutoCostDistribution = 0,
				cen.ManPowerDistributionDirect = 0,
				cen.ManPowerDistributionInDirect = 0,
				cen.FixedAssetDistribution = 0,
				cen.DispensingDistribution = 0,
				cen.TransferDistribution = 0,
				cen.InitialDistribution = 0,
				cen.CostAccountingAdjustment = 0,
				cen.ManPowerAccountingAdjustment = 0,
				cen.FixedAssetAccountingAdjustment = 0,
				cen.DispensingAccountingAdjustment = 0,
				cen.TransferAccountingAdjustment = 0,
				cen.TotalSales = 0,
				cen.ModificationDate = Common.GETDATE(),
				cen.ModificationUser = @UserCode
		FROM Cost.CostEstimationNative cen
		WHERE cen.Year = @Year AND cen.Month = @Month

		SELECT	0 AS CodeMessage, 'Reversión realizada con éxito' AS Message
	END TRY
	BEGIN CATCH
		SELECT	999 AS CodeMessage, CONCAT('Error realizando la reversión: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE()) AS Message
	END CATCH
END