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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula y distribuye un gasto general (costo indirecto) entre los centros de producción de la organización, aplicando el monto recibido según las bases de distribución configuradas para ese gasto. Soporta múltiples métodos de prorrateo: por valor fijo o cantidad (tipo 2), o por factores estadísticos como área física, horas hombre oficiales, valor de insumos, mano de obra, activos o ventas (tipo 3). Consulta las tablas CostDistributionBase para obtener las reglas de distribución y CostDistributionBaseDetail para los centros de costo participantes, acumula los valores parciales en una tabla temporal interna y al final calcula el porcentaje que le corresponde a cada centro y ajusta el último registro para que la suma cuadre exactamente con el valor total a distribuir, evitando diferencias de redondeo. Se usa en el módulo de costos para asignar gastos generales a unidades productivas en un período (año y mes) determinado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CalculateCostDistribution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CalculateCostDistribution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula y devuelve la distribución de un gasto general entre centros de producción/cuentas/centros de costo según las bases de distribución configuradas (cantidades, área, horas oficiales o saldos contables) prorrateando el valor y ajustando diferencias.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateCostDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en Cost.CostDistributionBase asociado al gasto general; si no, no se generan detalles.; Para distribución por horas oficiales (OfficialHours=1) deben existir distribuciones de mano de obra (Cost.CostDistributionManpower) con Status=1 para el año y mes indicados.; Para distribución por valores contables (SupplyValue/WorkmanshipValue/AssetValue/Sales) deben existir saldos en GeneralLedger.GeneralLedgerBalance para el año y mes indicados, y homologaciones en Cost.CostProductionCenterHomologation con el HomologationType correspondiente.; Las cuentas referenciadas deben tener clase contable (MainAccountClasses) con Nature definida para determinar el signo del saldo.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateCostDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La suma de Percentage de los detalles tiende a 100 y la suma de Value tiende a @Value tras el ajuste de diferencias.; El signo del valor contable se determina por la Nature de la clase de cuenta (1 → positivo; cualquier otro → negativo).; Solo se consideran distribuciones de mano de obra activas (Status=1) coincidentes con el año y mes indicados.; Solo se consideran saldos contables del mismo período (Year/Month) que los parámetros recibidos.; Cada combinación (ProductionCenter, MainAccount, CostCenter) aparece una sola vez en el resultado, acumulando valores cuando ya existe.; Si no existen bases de distribución para el gasto, el resultado es vacío.; Si TotalValue es 0, no se realiza prorrateo ni se generan ajustes.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateCostDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @Details (resultado): Devuelve el conjunto final de detalles con ProductionCenter, MainAccount, CostCenter, Percentage y Value distribuidos.; [INSERT] @Details: Cuando DistributionType=2, inserta filas con SUM(Quantity * @Value) por centro de producción/cuenta/centro de costo no existentes previamente en la tabla temporal.; [UPDATE] @Details: Cuando DistributionType=2 y la combinación ya existe en @Details, acumula Value += Quantity * @Value.; [INSERT] @Details: Cuando DistributionType=3 y Area=1, inserta filas con SUM(Area) del centro de producción para combinaciones nuevas.; [UPDATE] @Details: Cuando DistributionType=3 y Area=1, acumula Value += Area del centro de producción en filas existentes.; [INSERT/UPDATE] @Details: Cuando DistributionType=3 y OfficialHours=1, suma HoursQuantity de CostDistributionManpowerDetail filtrando por Status=1, Year=@Year, Month=@Month.; [INSERT/UPDATE] @Details: Cuando DistributionType=3 y SupplyValue/WorkmanshipValue/AssetValue/Sales=1, suma (DebitValue-CreditValue) ajustado por signo según Nature de la clase de cuenta (1 → +1, otro → -1), filtrando GeneralLedgerBalance por @Year/@Month y por HomologationType (Supply=2, Workmanship=1, Asset=5, Sales=6).; [UPDATE] @Details: Tras consolidar valores, recalcula Percentage = Value/TotalValue*100 y Value = Value/TotalValue*@Value; si TotalValue=0, ambos quedan en 0.; [UPDATE] @Details: Si quedan diferencias (OutstandingPercentage o OutstandingValue distintos de 0), itera fila por fila aplicando Common.CalculateAdjustedValue para ajustar Percentage y Value hasta agotar la diferencia.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateCostDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DistributionType = 2 → Distribuye en función de Quantity * @Value tomado de CostDistributionBaseDetail.; si DistributionType = 3 y Area = 1 → Distribuye según el Area del centro de producción.; si DistributionType = 3 y OfficialHours = 1 → Distribuye según HoursQuantity de la mano de obra activa (Status=1) del período.; si DistributionType = 3 y (SupplyValue=1 o WorkmanshipValue=1 o AssetValue=1 o Sales=1) → Distribuye según saldos contables (DebitValue-CreditValue) signados por Nature, filtrando por HomologationType: Supply=2, Workmanship=1, Asset=5, Sales=6.; si TotalValue = 0 → Asigna Percentage=0 y Value=0 a todos los detalles (evita división por cero).; si OutstandingPercentage <> 0 OR OutstandingValue <> 0 → Ejecuta ciclo de ajuste de diferencias distribuyendo el residuo entre las filas existentes.; si OutstandingPercentage = 0 AND OutstandingValue = 0 dentro del ciclo de ajuste → Termina el ciclo de ajuste anticipadamente.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateCostDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.CalculateAdjustedValue', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateCostDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostDistributionBase; Cost.CostDistributionBaseDetail; Cost.CostProductionCenter; GeneralLedger.MainAccounts; Payroll.CostCenter; Cost.CostDistributionManpowerDetail; Cost.CostDistributionManpower; Cost.CostProductionCenterHomologation; Cost.CostProductionCenterCostCenter; GeneralLedger.GeneralLedgerBalance; GeneralLedger.MainAccountClasses', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateCostDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateCostDistribution';
-- GO
