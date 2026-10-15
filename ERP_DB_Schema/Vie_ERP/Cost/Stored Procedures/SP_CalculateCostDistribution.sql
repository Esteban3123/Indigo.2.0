-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date:	2019-09-20
-- Description:	Calcula la distribución a partir de un Elemento de Distribución
-- =============================================
CREATE PROCEDURE [Cost].[SP_CalculateCostDistribution]
	@CostGeneralExpenseId INT,
	@Year INT,
	@Month INT,
	@Value NUMERIC(20,2)
AS
BEGIN
	SET NOCOUNT ON;

	/****************************************************** VARIABLES ****************************************************/

	DECLARE @CostDistributionBaseRows INT = 1,
			@CostDistributionBaseId INT = 0,
			@DistributionType TINYINT,
			@Area BIT,
			@OfficialHours BIT,
			@SupplyValue BIT,
			@WorkmanshipValue BIT,
			@AssetValue BIT,
			@Sales BIT,
			--------------------------------------------
			@TotalValue DECIMAL(18,2),
			--------------------------------------------
			@DetailRows INT = 1,
			@DetailId INT = 0,
			@OutstandingValue DECIMAL(18,2) = 0,
			@AdjustedValue DECIMAL(18,2),
			@OutstandingPercentage DECIMAL(18,4) = 0,			
			@AdjustedPercentage DECIMAL(18,4)

	DECLARE @Details TABLE 
	(
		Id INT IDENTITY(1,1),
		ProductionCenterId INT, 
		ProductionCenterCodeName VARCHAR(500), 
		MainAccountId INT, 
		MainAccountNumberName VARCHAR(500), 
		CostCenterId INT, 
		CostCenterCodeName VARCHAR(500), 
		Percentage DECIMAL(18,4) DEFAULT(0),
		Value DECIMAL(18,2)
	)

	/************************************************** CARGUE DETALLES **************************************************/

	WHILE @CostDistributionBaseRows > 0
	BEGIN
		SELECT TOP 1 
			@CostDistributionBaseId = cdsb.Id,
			@DistributionType = cdsb.DistributionType,
			@Area = cdsb.Area,
			@OfficialHours = cdsb.OfficialHours,
			@SupplyValue = cdsb.SupplyValue,
			@WorkmanshipValue = cdsb.WorkmanshipValue,
			@AssetValue = cdsb.AssetValue,
			@Sales = cdsb.Sales
		FROM Cost.CostDistributionBase cdsb
		WHERE cdsb.GeneralExpenseId = @CostGeneralExpenseId
			AND cdsb.Id > @CostDistributionBaseId
		ORDER BY cdsb.Id

		SET @CostDistributionBaseRows = @@ROWCOUNT
		IF @CostDistributionBaseRows = 0 
		BEGIN
			BREAK
		END

		IF @DistributionType = 2
		BEGIN
			UPDATE d
				SET d.Value = d.Value + (cdsbd.Quantity * @Value)
			FROM Cost.CostDistributionBaseDetail cdsbd
			JOIN @Details d ON cdsbd.ProductionCenterId = d.ProductionCenterId AND cdsbd.MainAccountId = d.MainAccountId AND ISNULL(cdsbd.CostCenterId, 0) = ISNULL(d.CostCenterId, 0)
			WHERE cdsbd.DistributionBaseId = @CostDistributionBaseId

			--SELECT 
			--	SUM(cdsbd.Quantity * @Value)
			--	FROM Cost.CostDistributionBaseDetail cdsbd
			--	JOIN Cost.CostProductionCenter cpc ON cdsbd.ProductionCenterId = cpc.Id
			--	JOIN GeneralLedger.MainAccounts ma ON cdsbd.MainAccountId = ma.Id
			--	LEFT JOIN Payroll.CostCenter cc ON cdsbd.CostCenterId = cc.Id
			--	LEFT JOIN @Details d ON cdsbd.ProductionCenterId = d.ProductionCenterId AND cdsbd.MainAccountId = d.MainAccountId AND ISNULL(cdsbd.CostCenterId, 0) = ISNULL(d.CostCenterId, 0)
			--	WHERE cdsbd.DistributionBaseId = @CostDistributionBaseId AND d.ProductionCenterId IS NULL


			INSERT INTO @Details (ProductionCenterId, ProductionCenterCodeName, MainAccountId, MainAccountNumberName, CostCenterId, CostCenterCodeName, Value)
				SELECT cpc.Id, CONCAT(cpc.Code, ' - ', cpc.Name), ma.Id, CONCAT(ma.Number, ' - ', ma.Name), cc.Id, CONCAT(cc.Code, ' - ', cc.Name), SUM(cdsbd.Quantity * @Value)
				FROM Cost.CostDistributionBaseDetail cdsbd
				JOIN Cost.CostProductionCenter cpc ON cdsbd.ProductionCenterId = cpc.Id
				JOIN GeneralLedger.MainAccounts ma ON cdsbd.MainAccountId = ma.Id
				LEFT JOIN Payroll.CostCenter cc ON cdsbd.CostCenterId = cc.Id
				LEFT JOIN @Details d ON cdsbd.ProductionCenterId = d.ProductionCenterId AND cdsbd.MainAccountId = d.MainAccountId AND ISNULL(cdsbd.CostCenterId, 0) = ISNULL(d.CostCenterId, 0)
				WHERE cdsbd.DistributionBaseId = @CostDistributionBaseId AND d.ProductionCenterId IS NULL
				GROUP BY cpc.Id, cpc.Code, cpc.Name, ma.Id, ma.Number, ma.Name, cc.Id, cc.Code, cc.Name
		END
		ELSE IF @DistributionType = 3
		BEGIN
			IF @Area = 1
			BEGIN
				UPDATE d
				SET d.Value = d.Value + cpc.Area
				FROM Cost.CostProductionCenter cpc
				JOIN Cost.CostDistributionBaseDetail cdsbd ON cpc.Id = cdsbd.ProductionCenterId
				JOIN @Details d ON cdsbd.ProductionCenterId = d.ProductionCenterId AND cdsbd.MainAccountId = d.MainAccountId AND ISNULL(cdsbd.CostCenterId, 0) = ISNULL(d.CostCenterId, 0)
				WHERE cdsbd.DistributionBaseId = @CostDistributionBaseId

				INSERT INTO @Details (ProductionCenterId, ProductionCenterCodeName, MainAccountId, MainAccountNumberName, CostCenterId, CostCenterCodeName, Value)
					SELECT cpc.Id, CONCAT(cpc.Code, ' - ', cpc.Name), ma.Id, CONCAT(ma.Number, ' - ', ma.Name), cc.Id, CONCAT(cc.Code, ' - ', cc.Name), SUM(cpc.Area)
					FROM Cost.CostDistributionBaseDetail cdsbd
					JOIN Cost.CostProductionCenter cpc ON cdsbd.ProductionCenterId = cpc.Id
					JOIN GeneralLedger.MainAccounts ma ON cdsbd.MainAccountId = ma.Id
					LEFT JOIN Payroll.CostCenter cc ON cdsbd.CostCenterId = cc.Id
					LEFT JOIN @Details d ON cdsbd.ProductionCenterId = d.ProductionCenterId AND cdsbd.MainAccountId = d.MainAccountId AND ISNULL(cdsbd.CostCenterId, 0) = ISNULL(d.CostCenterId, 0)
					WHERE cdsbd.DistributionBaseId = @CostDistributionBaseId AND d.ProductionCenterId IS NULL
					GROUP BY cpc.Id, cpc.Code, cpc.Name, ma.Id, ma.Number, ma.Name, cc.Id, cc.Code, cc.Name
			END

			IF @OfficialHours = 1
			BEGIN
				UPDATE d
				SET d.Value = d.Value + cdmd.HoursQuantity
				FROM Cost.CostDistributionBaseDetail cdsbd
				JOIN @Details d ON cdsbd.ProductionCenterId = d.ProductionCenterId AND cdsbd.MainAccountId = d.MainAccountId AND ISNULL(cdsbd.CostCenterId, 0) = ISNULL(d.CostCenterId, 0)
				JOIN Cost.CostDistributionManpowerDetail cdmd ON cdsbd.ProductionCenterId = cdmd.ProductionCenterId
				JOIN Cost.CostDistributionManpower cdm ON cdmd.DistributionManpowerId = cdm.Id
				WHERE cdsbd.DistributionBaseId = @CostDistributionBaseId
					AND cdm.Status = 1 AND cdm.Year = @Year AND cdm.Month = @Month

				INSERT INTO @Details (ProductionCenterId, ProductionCenterCodeName, MainAccountId, MainAccountNumberName, CostCenterId, CostCenterCodeName, Value)
					SELECT cpc.Id, CONCAT(cpc.Code, ' - ', cpc.Name), ma.Id, CONCAT(ma.Number, ' - ', ma.Name), cc.Id, CONCAT(cc.Code, ' - ', cc.Name), SUM(cdmd.HoursQuantity)
					FROM Cost.CostDistributionBaseDetail cdsbd
					JOIN Cost.CostProductionCenter cpc ON cdsbd.ProductionCenterId = cpc.Id
					JOIN GeneralLedger.MainAccounts ma ON cdsbd.MainAccountId = ma.Id					
					JOIN Cost.CostDistributionManpowerDetail cdmd ON cdsbd.ProductionCenterId = cdmd.ProductionCenterId
					JOIN Cost.CostDistributionManpower cdm ON cdmd.DistributionManpowerId = cdm.Id
					LEFT JOIN Payroll.CostCenter cc ON cdsbd.CostCenterId = cc.Id
					LEFT JOIN @Details d ON cdsbd.ProductionCenterId = d.ProductionCenterId AND cdsbd.MainAccountId = d.MainAccountId AND ISNULL(cdsbd.CostCenterId, 0) = ISNULL(d.CostCenterId, 0)
					WHERE cdsbd.DistributionBaseId = @CostDistributionBaseId AND d.ProductionCenterId IS NULL
						AND cdm.Status = 1 AND cdm.Year = @Year AND cdm.Month = @Month
					GROUP BY cpc.Id, cpc.Code, cpc.Name, ma.Id, ma.Number, ma.Name, cc.Id, cc.Code, cc.Name
			END

			IF @SupplyValue = 1 OR @WorkmanshipValue = 1 OR @AssetValue = 1 OR @Sales = 1
			BEGIN
				UPDATE d
				SET d.Value = d.Value + ((glb.DebitValue - glb.CreditValue) * IIF(mac.Nature = 1, 1, -1))
				FROM Cost.CostDistributionBaseDetail cdsbd
				JOIN @Details d ON cdsbd.ProductionCenterId = d.ProductionCenterId AND cdsbd.MainAccountId = d.MainAccountId AND ISNULL(cdsbd.CostCenterId, 0) = ISNULL(d.CostCenterId, 0)
				JOIN Cost.CostProductionCenterHomologation cpch ON cdsbd.ProductionCenterId = cpch.ProductionCenterId
				JOIN Cost.CostProductionCenterCostCenter cpccc ON cpch.ProductionCenterId = cpccc.ProductionCenterId
				JOIN GeneralLedger.GeneralLedgerBalance glb ON glb.Year = @Year AND glb.[Month] = @Month 
					AND cpch.AccountOriginId = glb.IdMainAccount AND cpccc.CostCenterId = glb.IdCostCenter
				JOIN GeneralLedger.MainAccounts ma ON glb.IdMainAccount = ma.Id
				JOIN GeneralLedger.MainAccountClasses mac ON ma.IdAccountClass = mac.Id
				WHERE cdsbd.DistributionBaseId = @CostDistributionBaseId
					AND 
					(
						(@SupplyValue = 1 AND cpch.HomologationType = 2)
						OR
						(@WorkmanshipValue = 1 AND cpch.HomologationType = 1)
						OR
						(@AssetValue = 1 AND cpch.HomologationType = 5)
						OR
						(@Sales = 1 AND cpch.HomologationType = 6)
					)

				INSERT INTO @Details (ProductionCenterId, ProductionCenterCodeName, MainAccountId, MainAccountNumberName, CostCenterId, CostCenterCodeName, Value)
					SELECT cpc.Id, CONCAT(cpc.Code, ' - ', cpc.Name), ma.Id, CONCAT(ma.Number, ' - ', ma.Name), cc.Id, CONCAT(cc.Code, ' - ', cc.Name), SUM((glb.DebitValue - glb.CreditValue) * IIF(mac.Nature = 1, 1, -1))
					FROM Cost.CostProductionCenter cpc
					JOIN Cost.CostDistributionBaseDetail cdsbd ON cpc.Id = cdsbd.ProductionCenterId
					JOIN Cost.CostProductionCenterHomologation cpch ON cdsbd.ProductionCenterId = cpch.ProductionCenterId
					JOIN Cost.CostProductionCenterCostCenter cpccc ON cpch.ProductionCenterId = cpccc.ProductionCenterId
					JOIN GeneralLedger.GeneralLedgerBalance glb ON glb.Year = @Year AND glb.[Month] = @Month 
						AND cpch.AccountOriginId = glb.IdMainAccount AND cpccc.CostCenterId = glb.IdCostCenter
					JOIN GeneralLedger.MainAccounts ma ON glb.IdMainAccount = ma.Id
					JOIN GeneralLedger.MainAccountClasses mac ON ma.IdAccountClass = mac.Id
					LEFT JOIN Payroll.CostCenter cc ON cdsbd.CostCenterId = cc.Id
					LEFT JOIN @Details d ON cdsbd.ProductionCenterId = d.ProductionCenterId AND cdsbd.MainAccountId = d.MainAccountId AND ISNULL(cdsbd.CostCenterId, 0) = ISNULL(d.CostCenterId, 0)
					WHERE cdsbd.DistributionBaseId = @CostDistributionBaseId AND d.ProductionCenterId IS NULL
						AND 
						(
							(@SupplyValue = 1 AND cpch.HomologationType = 2)
							OR
							(@WorkmanshipValue = 1 AND cpch.HomologationType = 1)
							OR
							(@AssetValue = 1 AND cpch.HomologationType = 5)
							OR
							(@Sales = 1 AND cpch.HomologationType = 6)
						)
					GROUP BY cpc.Id, cpc.Code, cpc.Name, ma.Id, ma.Number, ma.Name, cc.Id, cc.Code, cc.Name
			END
		END
	END

	/**************************************************** CALCULO VALOR **************************************************/

	SELECT @TotalValue = SUM(Value) FROM @Details

	--select
	--	(Value / CAST(@TotalValue AS DECIMAL(18,4)) * 100) porcentaje,
	--	(Value / @TotalValue * @Value) value
	--FROM @Details d

	UPDATE d 
	SET d.Percentage = IIF(@TotalValue = 0,0,(Value / CAST(@TotalValue AS DECIMAL(18,4)) * 100)),
		d.Value = IIF(@TotalValue =0,0, (Value / @TotalValue * @Value))
	FROM @Details d

	SELECT	@OutstandingPercentage = 100 - SUM(d.Percentage),
			@OutstandingValue = @Value - SUM(d.Value)
	FROM @Details d

	/************************************************* AJUSTE DIFERENCIAS ************************************************/
			--SELECT sum(Percentage), sum(Value) FROM @Details

	IF @OutstandingPercentage <> 0 OR @OutstandingValue <> 0
	BEGIN
		SELECT @DetailRows = 1,
			   @DetailId = 0

		WHILE @DetailRows > 0
		BEGIN
			SELECT TOP 1 
				@DetailId = Id,
				@AdjustedPercentage = Common.CalculateAdjustedValue(@OutstandingPercentage, Percentage, Percentage),
				@AdjustedValue = Common.CalculateAdjustedValue(@OutstandingValue, Value, Value)
			FROM @Details
			WHERE Id > @DetailId
			ORDER BY Id

			SET @DetailRows = @@ROWCOUNT
			IF @DetailRows = 0 
			BEGIN
				BREAK
			END

			UPDATE @Details 
				SET Percentage = Percentage + @AdjustedPercentage,
					Value = Value + @AdjustedValue
			WHERE Id = @DetailId

			SELECT @OutstandingPercentage = @OutstandingPercentage - @AdjustedPercentage,
				   @OutstandingValue = @OutstandingValue - @AdjustedValue

			IF @OutstandingPercentage = 0 AND @OutstandingValue = 0
			BEGIN
				BREAK
			END
		END
	END

	/***************************************************** RESULTADO *****************************************************/

    SELECT *
	FROM @Details
END