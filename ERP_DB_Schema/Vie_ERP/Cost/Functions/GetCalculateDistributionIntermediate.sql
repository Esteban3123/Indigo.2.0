-- =============================================
-- Author:		Diego A. Roldan Lozano
-- Create date: 2020-05-14
-- Description:	Calcula la distribución intermedia de un centro de producción
-- =============================================
CREATE FUNCTION [Cost].[GetCalculateDistributionIntermediate]
(
	@Year INT,
	@Month INT,
	@CostIntermediateDistributionId INT,
	@ValueToDistribute DECIMAL(18, 2),
	@PercentageToDistribute DECIMAL(5, 2)
)
RETURNS @CalculateIntermediateDistribution TABLE 
(
	[Id] [int] IDENTITY(1,1),
	[ProductionCenterId] [int] NOT NULL,
	[ProductionCenterCodeName] VARCHAR(200) NOT NULL, 
	[Percentage] [numeric](5, 2) DEFAULT(0),
	[Value] [decimal](18, 2) DEFAULT(0)
)
AS
BEGIN
	
	/****************************************************** VARIABLES ****************************************************/

	DECLARE @DistributionMonth INT = @Month,
			@CostIntermediateDistributionBaseRows INT = 1,
			@CostIntermediateDistributionBaseId INT = 0,
			--------------------------------------------
			@DistributionType TINYINT,
			@SalesValue BIT,
			@QuantitiesProduced BIT,
			--------------------------------------------
			@OutstandingValue DECIMAL(18, 2) = 0,
			@OutstandingPercentage DECIMAL(5,2) = 0,
			@TotalValue DECIMAL(18, 2)

	/************************************************ CARGUE DETALLES ************************************************/

	DISTRIBUTION_POINT:

	WHILE @CostIntermediateDistributionBaseRows > 0
	BEGIN
		SELECT TOP 1 
			@CostIntermediateDistributionBaseId = cdsb.Id,
			@DistributionType = cdsb.DistributionType,
			@SalesValue = cdsb.Sales,
			@QuantitiesProduced = cdsb.QuantitiesProduced
		FROM Cost.CostIntermediateDistributionBase cdsb
		WHERE cdsb.IntermediateDistributionId = @CostIntermediateDistributionId
			AND cdsb.Id > @CostIntermediateDistributionBaseId
		ORDER BY cdsb.Id

		SET @CostIntermediateDistributionBaseRows = @@ROWCOUNT
		IF @CostIntermediateDistributionBaseRows = 0 
		BEGIN
			BREAK
		END

		---------------------------------------------------------------------------------------------------------------

		--IF @DistributionType = 1
		--BEGIN
		--	INSERT INTO @CalculateIntermediateDistribution (ProductionCenterId, ProductionCenterCodeName, Value)
		--		SELECT cpc.Id, CONCAT(cpc.Code, ' - ', cpc.Name), SUM(ISNULL(cddsdr.Value, cddsd.Value))
		--		FROM Cost.CostDirectDistributionSecondary cdds
		--		JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
		--		LEFT JOIN Cost.CostDirectDistributionSecondaryDetailRedistribution cddsdr ON cddsd.Id = cddsdr.DirectDistributionSecondaryDetailId
		--		LEFT JOIN Cost.CostProductionCenter cpc ON ISNULL(cddsdr.ProductionCenterId, cddsd.ProductionCenterId) = cpc.Id
		--		LEFT JOIN @CalculateIntermediateDistribution d ON cpc.Id = d.ProductionCenterId				
		--		WHERE cdds.DistributionSecondaryId = @CostIntermediateDistributionId AND cdds.Year = @Year AND cdds.Month = @DistributionMonth AND cdds.Status = 2
		--			AND d.ProductionCenterId IS NULL
		--		GROUP BY cpc.Id, cpc.Code, cpc.Name
		--END
		--ELSE 
		IF @DistributionType = 2
		BEGIN
			UPDATE d
				SET d.Value = d.Value + cdsbd.Quantity
			FROM Cost.CostIntermediateDistributionBaseDetail cdsbd
			JOIN @CalculateIntermediateDistribution d ON cdsbd.ProductionCenterId = d.ProductionCenterId
			WHERE cdsbd.IntermediateDistributionBaseId = @CostIntermediateDistributionBaseId

			INSERT INTO @CalculateIntermediateDistribution (ProductionCenterId, ProductionCenterCodeName, Value)
				SELECT cpc.Id, CONCAT(cpc.Code, ' - ', cpc.Name), SUM(cdsbd.Quantity)
				FROM Cost.CostProductionCenter cpc
				JOIN Cost.CostIntermediateDistributionBaseDetail cdsbd ON cpc.Id = cdsbd.ProductionCenterId
				LEFT JOIN @CalculateIntermediateDistribution d ON cdsbd.ProductionCenterId = d.ProductionCenterId
				WHERE cdsbd.IntermediateDistributionBaseId = @CostIntermediateDistributionBaseId AND d.ProductionCenterId IS NULL
				GROUP BY cpc.Id, cpc.Code, cpc.Name
		END
		ELSE IF @DistributionType = 3
		BEGIN
			IF @SalesValue = 1
			BEGIN
				UPDATE d
				SET d.Value = d.Value + ((glb.DebitValue - glb.CreditValue) * IIF(mac.Nature = 1, 1, -1))
				FROM Cost.CostIntermediateDistributionBaseDetail cdsbd
				JOIN @CalculateIntermediateDistribution d ON cdsbd.ProductionCenterId = d.ProductionCenterId
				JOIN Cost.CostProductionCenterHomologation cpch ON cdsbd.ProductionCenterId = cpch.ProductionCenterId
				JOIN Cost.CostProductionCenterCostCenter cpccc ON cpch.ProductionCenterId = cpccc.ProductionCenterId
				JOIN GeneralLedger.GeneralLedgerBalance glb ON glb.Year = @Year AND glb.[Month] = @DistributionMonth 
					AND cpch.AccountOriginId = glb.IdMainAccount AND cpccc.CostCenterId = glb.IdCostCenter
				JOIN GeneralLedger.MainAccounts ma ON glb.IdMainAccount = ma.Id
				JOIN GeneralLedger.MainAccountClasses mac ON ma.IdAccountClass = mac.Id
				WHERE cdsbd.IntermediateDistributionBaseId = @CostIntermediateDistributionBaseId
					AND 
					(
						(@SalesValue = 1 AND cpch.HomologationType = 6 and cpch.AllowSecondaryDistribution = 1)
					)

				INSERT INTO @CalculateIntermediateDistribution (ProductionCenterId, ProductionCenterCodeName, Value)
					SELECT cpc.Id, CONCAT(cpc.Code, ' - ', cpc.Name), SUM((glb.DebitValue - glb.CreditValue) * IIF(mac.Nature = 1, 1, -1))
					FROM Cost.CostProductionCenter cpc
					JOIN Cost.CostIntermediateDistributionBaseDetail cdsbd ON cpc.Id = cdsbd.ProductionCenterId
					JOIN Cost.CostProductionCenterHomologation cpch ON cdsbd.ProductionCenterId = cpch.ProductionCenterId
					JOIN Cost.CostProductionCenterCostCenter cpccc ON cpch.ProductionCenterId = cpccc.ProductionCenterId
					JOIN GeneralLedger.GeneralLedgerBalance glb ON glb.Year = @Year AND glb.[Month] = @DistributionMonth 
						AND cpch.AccountOriginId = glb.IdMainAccount AND cpccc.CostCenterId = glb.IdCostCenter
					JOIN GeneralLedger.MainAccounts ma ON glb.IdMainAccount = ma.Id
					JOIN GeneralLedger.MainAccountClasses mac ON ma.IdAccountClass = mac.Id
					LEFT JOIN @CalculateIntermediateDistribution d ON cdsbd.ProductionCenterId = d.ProductionCenterId
					WHERE cdsbd.IntermediateDistributionBaseId = @CostIntermediateDistributionBaseId AND d.ProductionCenterId IS NULL
						AND 
						(
							(@SalesValue = 1 AND cpch.HomologationType = 6 and cpch.AllowSecondaryDistribution = 1)
						)
					GROUP BY cpc.Id, cpc.Code, cpc.Name
			END
			-- Pendiente para QuantitiesProduced
			--IF @CousinValue = 1
			--BEGIN
			--	UPDATE d
			--	SET d.Value = d.Value + cen.InitialDistribution
			--	FROM Cost.CostDistributionSecondaryBaseDetail cdsbd
			--	JOIN @CalculateIntermediateDistribution d ON cdsbd.ProductionCenterId = d.ProductionCenterId
			--	JOIN Cost.CostEstimationNative cen ON cdsbd.ProductionCenterId = cen.ProductionCenterId
			--		AND cen.Year = @Year AND cen.Month = @DistributionMonth
			--	WHERE cdsbd.DistributionSecondaryBaseId = @CostIntermediateDistributionBaseId

			--	INSERT INTO @CalculateIntermediateDistribution (ProductionCenterId, ProductionCenterCodeName, Value)
			--		SELECT cpc.Id, CONCAT(cpc.Code, ' - ', cpc.Name), SUM(cen.InitialDistribution)
			--		FROM Cost.CostProductionCenter cpc
			--		JOIN Cost.CostDistributionSecondaryBaseDetail cdsbd ON cpc.Id = cdsbd.ProductionCenterId
			--		JOIN Cost.CostEstimationNative cen ON cdsbd.ProductionCenterId = cen.ProductionCenterId
			--			AND cen.Year = @Year AND cen.Month = @DistributionMonth
			--		LEFT JOIN @CalculateIntermediateDistribution d ON cdsbd.ProductionCenterId = d.ProductionCenterId
			--		WHERE cdsbd.DistributionSecondaryBaseId = @CostIntermediateDistributionBaseId AND d.ProductionCenterId IS NULL
			--		GROUP BY cpc.Id, cpc.Code, cpc.Name
			--END
		END
	END

	IF @DistributionMonth > 0 AND NOT EXISTS(SELECT 1 FROM @CalculateIntermediateDistribution)
	BEGIN
		SET @DistributionMonth = @DistributionMonth - 1
		SET @CostIntermediateDistributionBaseRows = 1
		SET @CostIntermediateDistributionBaseId = 0
		
		GOTO DISTRIBUTION_POINT											 
	END

	/**************************************************** ASIGNACIONES ***************************************************/

	IF ISNULL(@ValueToDistribute, 0) = 0
	BEGIN
		SELECT 
			@ValueToDistribute = cen.InitialDistribution,
			@PercentageToDistribute = 100
		FROM Cost.CostIntermediateDistribution cds
		JOIN Cost.CostEstimationNative cen ON cds.ProductionCenterId = cen.ProductionCenterId AND cen.Year = @Year AND cen.Month = @Month
		WHERE cds.Id = @CostIntermediateDistributionId
	END

	SELECT	@OutstandingValue = @ValueToDistribute,
			@OutstandingPercentage = @PercentageToDistribute,
			@TotalValue = SUM(Value)
	FROM @CalculateIntermediateDistribution

	/**************************************************** CALCULO VALOR **************************************************/

	UPDATE @CalculateIntermediateDistribution
		SET Value = @ValueToDistribute * (Value / @TotalValue),
			[Percentage] = @PercentageToDistribute * (Value / @TotalValue)

	/************************************************* AJUSTE DIFERENCIAS ************************************************/

	UPDATE r
		SET r.Value = r.Value + rt.Diff
	FROM @CalculateIntermediateDistribution r
	JOIN
	(
		SELECT @ValueToDistribute - SUM(Value) Diff, MIN(Id) Id
		FROM @CalculateIntermediateDistribution
	) rt ON r.Id = rt.Id
	WHERE rt.Diff <> 0

	UPDATE r
		SET r.Percentage = r.Percentage + rt.Diff
	FROM @CalculateIntermediateDistribution r
	JOIN
	(
		SELECT @PercentageToDistribute - SUM(Percentage) Diff, MIN(Id) Id
		FROM @CalculateIntermediateDistribution
	) rt ON r.Id = rt.Id
	WHERE rt.Diff <> 0

	-------------------------------------------------------------------------------------------------------------------

	RETURN
END