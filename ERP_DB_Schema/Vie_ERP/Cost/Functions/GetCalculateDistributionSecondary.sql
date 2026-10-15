-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-14
-- Description:	Calcula la distribución secundaria de un centro de producción
-- =============================================
CREATE FUNCTION [Cost].[GetCalculateDistributionSecondary]
(
	@Year INT,
	@Month INT,
	@CostDistributionSecondaryId INT,
	@ValueToDistribute DECIMAL(18, 2),
	@PercentageToDistribute DECIMAL(5, 2)
)
RETURNS @CalculateDistributionSecondary TABLE 
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
			@CostDistributionSecondaryBaseRows INT = 1,
			@CostDistributionSecondaryBaseId INT = 0,
			--------------------------------------------
			@DistributionType TINYINT,
			@Area BIT,
			@OfficialHours BIT,
			@SupplyValue BIT,
			@WorkmanshipValue BIT,
			@AssetValue BIT,
			@InvoiceValue BIT,
			@CousinValue BIT,
			--------------------------------------------
			@OutstandingValue DECIMAL(18, 2) = 0,
			@OutstandingPercentage DECIMAL(5,2) = 0,
			@TotalValue DECIMAL(18, 2)

	/************************************************ CARGUE DETALLES ************************************************/

	DISTRIBUTION_POINT:

	WHILE @CostDistributionSecondaryBaseRows > 0
	BEGIN
		SELECT TOP 1 
			@CostDistributionSecondaryBaseId = cdsb.Id,
			@DistributionType = cdsb.DistributionType,
			@Area = cdsb.Area,
			@OfficialHours = cdsb.OfficialHours,
			@SupplyValue = cdsb.SupplyValue,
			@WorkmanshipValue = cdsb.WorkmanshipValue,
			@AssetValue = cdsb.AssetValue,
			@InvoiceValue = cdsb.InvoiceValue,
			@CousinValue = cdsb.CousinValue
		FROM Cost.CostDistributionSecondaryBase cdsb
		WHERE cdsb.DistributionSecondaryId = @CostDistributionSecondaryId
			AND cdsb.Id > @CostDistributionSecondaryBaseId
		ORDER BY cdsb.Id

		SET @CostDistributionSecondaryBaseRows = @@ROWCOUNT
		IF @CostDistributionSecondaryBaseRows = 0 
		BEGIN
			BREAK
		END

		---------------------------------------------------------------------------------------------------------------

		IF @DistributionType = 1
		BEGIN
			INSERT INTO @CalculateDistributionSecondary (ProductionCenterId, ProductionCenterCodeName, Value)
				SELECT cpc.Id, CONCAT(cpc.Code, ' - ', cpc.Name), SUM(ISNULL(cddsdr.Value, cddsd.Value))
				FROM Cost.CostDirectDistributionSecondary cdds
				JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
				LEFT JOIN Cost.CostDirectDistributionSecondaryDetailRedistribution cddsdr ON cddsd.Id = cddsdr.DirectDistributionSecondaryDetailId
				LEFT JOIN Cost.CostProductionCenter cpc ON ISNULL(cddsdr.ProductionCenterId, cddsd.ProductionCenterId) = cpc.Id
				LEFT JOIN @CalculateDistributionSecondary d ON cpc.Id = d.ProductionCenterId				
				WHERE cdds.DistributionSecondaryId = @CostDistributionSecondaryId AND cdds.Year = @Year AND cdds.Month = @DistributionMonth AND cdds.Status = 2
					AND d.ProductionCenterId IS NULL
				GROUP BY cpc.Id, cpc.Code, cpc.Name
		END
		ELSE IF @DistributionType = 2
		BEGIN
			UPDATE d
				SET d.Value = d.Value + cdsbd.Quantity
			FROM Cost.CostDistributionSecondaryBaseDetail cdsbd
			JOIN @CalculateDistributionSecondary d ON cdsbd.ProductionCenterId = d.ProductionCenterId
			WHERE cdsbd.DistributionSecondaryBaseId = @CostDistributionSecondaryBaseId

			INSERT INTO @CalculateDistributionSecondary (ProductionCenterId, ProductionCenterCodeName, Value)
				SELECT cpc.Id, CONCAT(cpc.Code, ' - ', cpc.Name), SUM(cdsbd.Quantity)
				FROM Cost.CostProductionCenter cpc
				JOIN Cost.CostDistributionSecondaryBaseDetail cdsbd ON cpc.Id = cdsbd.ProductionCenterId
				LEFT JOIN @CalculateDistributionSecondary d ON cdsbd.ProductionCenterId = d.ProductionCenterId
				WHERE cdsbd.DistributionSecondaryBaseId = @CostDistributionSecondaryBaseId AND d.ProductionCenterId IS NULL
				GROUP BY cpc.Id, cpc.Code, cpc.Name
		END
		ELSE IF @DistributionType = 3
		BEGIN
			IF @Area = 1
			BEGIN
				UPDATE d
				SET d.Value = d.Value + cpc.Area
				FROM Cost.CostProductionCenter cpc
				JOIN Cost.CostDistributionSecondaryBaseDetail cdsbd ON cpc.Id = cdsbd.ProductionCenterId
				JOIN @CalculateDistributionSecondary d ON cdsbd.ProductionCenterId = d.ProductionCenterId
				WHERE cdsbd.DistributionSecondaryBaseId = @CostDistributionSecondaryBaseId

				INSERT INTO @CalculateDistributionSecondary (ProductionCenterId, ProductionCenterCodeName, Value)
					SELECT cpc.Id, CONCAT(cpc.Code, ' - ', cpc.Name), SUM(cpc.Area)
					FROM Cost.CostProductionCenter cpc
					JOIN Cost.CostDistributionSecondaryBaseDetail cdsbd ON cpc.Id = cdsbd.ProductionCenterId
					LEFT JOIN @CalculateDistributionSecondary d ON cdsbd.ProductionCenterId = d.ProductionCenterId
					WHERE cdsbd.DistributionSecondaryBaseId = @CostDistributionSecondaryBaseId AND d.ProductionCenterId IS NULL
					GROUP BY cpc.Id, cpc.Code, cpc.Name
			END

			IF @OfficialHours = 1
			BEGIN
				UPDATE d
				SET d.Value = d.Value + cdmd.HoursQuantity
				FROM Cost.CostDistributionSecondaryBaseDetail cdsbd
				JOIN @CalculateDistributionSecondary d ON cdsbd.ProductionCenterId = d.ProductionCenterId
				JOIN Cost.CostDistributionManpowerDetail cdmd ON cdsbd.ProductionCenterId = cdmd.ProductionCenterId
				JOIN Cost.CostDistributionManpower cdm ON cdmd.DistributionManpowerId = cdm.Id
				WHERE cdsbd.DistributionSecondaryBaseId = @CostDistributionSecondaryBaseId
					AND cdm.Status = 1 AND cdm.Year = @Year AND cdm.Month = @DistributionMonth

				INSERT INTO @CalculateDistributionSecondary (ProductionCenterId, ProductionCenterCodeName, Value)
					SELECT cpc.Id, CONCAT(cpc.Code, ' - ', cpc.Name), SUM(cdmd.HoursQuantity)
					FROM Cost.CostProductionCenter cpc
					JOIN Cost.CostDistributionSecondaryBaseDetail cdsbd ON cpc.Id = cdsbd.ProductionCenterId
					JOIN Cost.CostDistributionManpowerDetail cdmd ON cdsbd.ProductionCenterId = cdmd.ProductionCenterId
					JOIN Cost.CostDistributionManpower cdm ON cdmd.DistributionManpowerId = cdm.Id
					LEFT JOIN @CalculateDistributionSecondary d ON cdsbd.ProductionCenterId = d.ProductionCenterId
					WHERE cdsbd.DistributionSecondaryBaseId = @CostDistributionSecondaryBaseId AND d.ProductionCenterId IS NULL
						AND cdm.Status = 1 AND cdm.Year = @Year AND cdm.Month = @DistributionMonth
					GROUP BY cpc.Id, cpc.Code, cpc.Name
			END

			IF @SupplyValue = 1 OR @WorkmanshipValue = 1 OR @AssetValue = 1 OR @InvoiceValue = 1
			BEGIN
				UPDATE d
				SET d.Value = d.Value + ((glb.DebitValue - glb.CreditValue) * IIF(mac.Nature = 1, 1, -1))
				FROM Cost.CostDistributionSecondaryBaseDetail cdsbd
				JOIN @CalculateDistributionSecondary d ON cdsbd.ProductionCenterId = d.ProductionCenterId
				JOIN Cost.CostProductionCenterHomologation cpch ON cdsbd.ProductionCenterId = cpch.ProductionCenterId
				JOIN Cost.CostProductionCenterCostCenter cpccc ON cpch.ProductionCenterId = cpccc.ProductionCenterId
				JOIN GeneralLedger.GeneralLedgerBalance glb ON glb.Year = @Year AND glb.[Month] = @DistributionMonth 
					AND cpch.AccountOriginId = glb.IdMainAccount AND cpccc.CostCenterId = glb.IdCostCenter
				JOIN GeneralLedger.MainAccounts ma ON glb.IdMainAccount = ma.Id
				JOIN GeneralLedger.MainAccountClasses mac ON ma.IdAccountClass = mac.Id
				WHERE cdsbd.DistributionSecondaryBaseId = @CostDistributionSecondaryBaseId
					AND 
					(
						(@SupplyValue = 1 AND cpch.HomologationType = 2)
						OR
						(@WorkmanshipValue = 1 AND cpch.HomologationType = 1)
						OR
						(@AssetValue = 1 AND cpch.HomologationType = 5)
						OR
						(@InvoiceValue = 1 AND cpch.HomologationType = 6 and cpch.AllowSecondaryDistribution = 1)
					)

				INSERT INTO @CalculateDistributionSecondary (ProductionCenterId, ProductionCenterCodeName, Value)
					SELECT cpc.Id, CONCAT(cpc.Code, ' - ', cpc.Name), SUM((glb.DebitValue - glb.CreditValue) * IIF(mac.Nature = 1, 1, -1))
					FROM Cost.CostProductionCenter cpc
					JOIN Cost.CostDistributionSecondaryBaseDetail cdsbd ON cpc.Id = cdsbd.ProductionCenterId
					JOIN Cost.CostProductionCenterHomologation cpch ON cdsbd.ProductionCenterId = cpch.ProductionCenterId
					JOIN Cost.CostProductionCenterCostCenter cpccc ON cpch.ProductionCenterId = cpccc.ProductionCenterId
					JOIN GeneralLedger.GeneralLedgerBalance glb ON glb.Year = @Year AND glb.[Month] = @DistributionMonth 
						AND cpch.AccountOriginId = glb.IdMainAccount AND cpccc.CostCenterId = glb.IdCostCenter
					JOIN GeneralLedger.MainAccounts ma ON glb.IdMainAccount = ma.Id
					JOIN GeneralLedger.MainAccountClasses mac ON ma.IdAccountClass = mac.Id
					LEFT JOIN @CalculateDistributionSecondary d ON cdsbd.ProductionCenterId = d.ProductionCenterId
					WHERE cdsbd.DistributionSecondaryBaseId = @CostDistributionSecondaryBaseId AND d.ProductionCenterId IS NULL
						AND 
						(
							(@SupplyValue = 1 AND cpch.HomologationType = 2)
							OR
							(@WorkmanshipValue = 1 AND cpch.HomologationType = 1)
							OR
							(@AssetValue = 1 AND cpch.HomologationType = 5)
							OR
							(@InvoiceValue = 1 AND cpch.HomologationType = 6 and cpch.AllowSecondaryDistribution = 1)
						)
					GROUP BY cpc.Id, cpc.Code, cpc.Name
			END

			IF @CousinValue = 1
			BEGIN
				UPDATE d
				SET d.Value = d.Value + cen.InitialDistribution
				FROM Cost.CostDistributionSecondaryBaseDetail cdsbd
				JOIN @CalculateDistributionSecondary d ON cdsbd.ProductionCenterId = d.ProductionCenterId
				JOIN Cost.CostEstimationNative cen ON cdsbd.ProductionCenterId = cen.ProductionCenterId
					AND cen.Year = @Year AND cen.Month = @DistributionMonth
				WHERE cdsbd.DistributionSecondaryBaseId = @CostDistributionSecondaryBaseId

				INSERT INTO @CalculateDistributionSecondary (ProductionCenterId, ProductionCenterCodeName, Value)
					SELECT cpc.Id, CONCAT(cpc.Code, ' - ', cpc.Name), SUM(cen.InitialDistribution)
					FROM Cost.CostProductionCenter cpc
					JOIN Cost.CostDistributionSecondaryBaseDetail cdsbd ON cpc.Id = cdsbd.ProductionCenterId
					JOIN Cost.CostEstimationNative cen ON cdsbd.ProductionCenterId = cen.ProductionCenterId
						AND cen.Year = @Year AND cen.Month = @DistributionMonth
					LEFT JOIN @CalculateDistributionSecondary d ON cdsbd.ProductionCenterId = d.ProductionCenterId
					WHERE cdsbd.DistributionSecondaryBaseId = @CostDistributionSecondaryBaseId AND d.ProductionCenterId IS NULL
					GROUP BY cpc.Id, cpc.Code, cpc.Name
			END
		END
	END

	IF @DistributionMonth > 0 AND NOT EXISTS(SELECT 1 FROM @CalculateDistributionSecondary)
	BEGIN
		SET @DistributionMonth = @DistributionMonth - 1
		SET @CostDistributionSecondaryBaseRows = 1
		SET @CostDistributionSecondaryBaseId = 0
		
		GOTO DISTRIBUTION_POINT											 
	END

	/**************************************************** ASIGNACIONES ***************************************************/

	IF ISNULL(@ValueToDistribute, 0) = 0
	BEGIN
		SELECT 
			@ValueToDistribute = cen.InitialDistribution,
			@PercentageToDistribute = 100
		FROM Cost.CostDistributionSecondary cds
		JOIN Cost.CostEstimationNative cen ON cds.ProductionCenterId = cen.ProductionCenterId AND cen.Year = @Year AND cen.Month = @Month
		WHERE cds.Id = @CostDistributionSecondaryId
	END

	SELECT	@OutstandingValue = @ValueToDistribute,
			@OutstandingPercentage = @PercentageToDistribute,
			@TotalValue = SUM(Value)
	FROM @CalculateDistributionSecondary

	/**************************************************** CALCULO VALOR **************************************************/

	UPDATE @CalculateDistributionSecondary
		SET Value = @ValueToDistribute * (Value / @TotalValue),
			[Percentage] = @PercentageToDistribute * (Value / @TotalValue)

	/************************************************* AJUSTE DIFERENCIAS ************************************************/

	UPDATE r
		SET r.Value = r.Value + rt.Diff
	FROM @CalculateDistributionSecondary r
	JOIN
	(
		SELECT @ValueToDistribute - SUM(Value) Diff, MIN(Id) Id
		FROM @CalculateDistributionSecondary
	) rt ON r.Id = rt.Id
	WHERE rt.Diff <> 0

	UPDATE r
		SET r.Percentage = r.Percentage + rt.Diff
	FROM @CalculateDistributionSecondary r
	JOIN
	(
		SELECT @PercentageToDistribute - SUM(Percentage) Diff, MIN(Id) Id
		FROM @CalculateDistributionSecondary
	) rt ON r.Id = rt.Id
	WHERE rt.Diff <> 0

	-------------------------------------------------------------------------------------------------------------------

	RETURN
END