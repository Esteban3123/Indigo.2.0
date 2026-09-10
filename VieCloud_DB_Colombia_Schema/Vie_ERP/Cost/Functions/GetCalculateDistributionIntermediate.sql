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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la distribución intermedia de costos indirectos entre centros de producción para un mes y año determinados, prorrateando un valor y porcentaje dados según la base de distribución configurada. Soporta tres tipos de distribución: manual (tipo 2, usando cantidades fijas por centro), por ventas o ingresos facturados registrados en el libro mayor (tipo 3 con ventas), y por cantidades producidas o atenciones prestadas según tipo de servicio (tipo 3 con producción). Retorna una tabla con cada centro de producción, su código y nombre, el porcentaje que le corresponde y el valor distribuido, permitiendo al módulo de costos prorratear gastos indirectos entre unidades funcionales o centros de costo de la institución de salud.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'FUNCTION', @level1name = N'GetCalculateDistributionIntermediate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'FUNCTION', @level1name = N'GetCalculateDistributionIntermediate';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la distribución intermedia de costos de un centro de producción para un periodo (año/mes), repartiendo un valor o porcentaje entre los centros que conforman su base de distribución.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetCalculateDistributionIntermediate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos una base de distribución intermedia (Cost.CostIntermediateDistributionBase) asociada al identificador de distribución recibido.; Para distribución por ventas (DistributionType=3, Sales=1) debe haber homologaciones con HomologationType=6 y AllowSecondaryDistribution=1, y saldos en GeneralLedgerBalance para el año/mes.; Si no se entrega valor a distribuir, debe existir un registro en Cost.CostIntermediateDistribution con su correspondiente Cost.CostEstimationNative para el año/mes.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetCalculateDistributionIntermediate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La suma final de Value en el resultado es igual a ValueToDistribute (el ajuste por diferencia corrige redondeos sobre el registro de menor Id).; La suma final de Percentage es igual a PercentageToDistribute.; Cada centro de producción aparece una sola vez por base; al volver a procesar una base existente solo se acumula sobre el registro previo.; Si no se especifica valor a distribuir, se asume 100% del InitialDistribution estimado para el periodo.; Solo se consideran homologaciones de ventas con HomologationType=6 y AllowSecondaryDistribution=1 cuando el criterio es ventas.; Si el mes solicitado no aporta datos, se busca recursivamente en meses anteriores hasta encontrar información o llegar a 0.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetCalculateDistributionIntermediate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución intermedia de costos; Centro de producción; Centro de costo; Base de distribución; Homologación contable de centros de producción; Saldo contable (debe/haber); Naturaleza de cuenta contable; Distribución por cantidades producidas; Distribución por ventas; Estimación inicial de distribución (InitialDistribution); Prorrateo y ajuste de diferencias por redondeo', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetCalculateDistributionIntermediate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @CalculateIntermediateDistribution: Cuando DistributionType=2, inserta centros de producción aún no presentes en la tabla con la suma de Quantity de CostIntermediateDistributionBaseDetail.; [UPDATE] @CalculateIntermediateDistribution: Cuando DistributionType=2 y el centro ya existe, acumula Value sumando la Quantity del detalle de la base.; [INSERT] @CalculateIntermediateDistribution: Cuando DistributionType=3 y Sales=1, inserta centros nuevos con la suma de (DebitValue-CreditValue) ajustada por la naturaleza de la cuenta (1 positivo, otro negativo), filtrando homologaciones con HomologationType=6 y AllowSecondaryDistribution=1.; [UPDATE] @CalculateIntermediateDistribution: Cuando DistributionType=3 y Sales=1 y el centro ya existe, acumula Value con (DebitValue-CreditValue)*signo_naturaleza desde GeneralLedgerBalance del año/mes.; [UPDATE] @CalculateIntermediateDistribution: Una vez cargada la base, recalcula Value = ValueToDistribute*(Value/TotalValue) y Percentage = PercentageToDistribute*(Value/TotalValue) para prorratear proporcionalmente.; [UPDATE] @CalculateIntermediateDistribution: Si hay diferencia entre ValueToDistribute y la suma de Value tras el prorrateo, suma el residuo al registro con menor Id (ajuste de redondeo de valor).; [UPDATE] @CalculateIntermediateDistribution: Si hay diferencia entre PercentageToDistribute y la suma de Percentage, suma el residuo al registro con menor Id (ajuste de redondeo de porcentaje).; [RETURN_RESULT] @CalculateIntermediateDistribution: Devuelve la tabla con ProductionCenterId, código-nombre concatenado, porcentaje y valor distribuidos.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetCalculateDistributionIntermediate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DistributionType = 2 → Distribuye con base en Quantity del detalle de la base (cantidades producidas/unidades).; si DistributionType = 3 y Sales = 1 → Distribuye con base en saldos contables (DebitValue-CreditValue) ajustados por naturaleza de cuenta, usando homologaciones tipo 6 que permitan distribución secundaria.; si El mes de distribución > 0 y la tabla resultado quedó vacía tras procesar todas las bases → Decrementa el mes en 1 y reinicia el ciclo (GOTO DISTRIBUTION_POINT) buscando datos en el mes anterior.; si ValueToDistribute es NULL o 0 → Toma como valor a distribuir el InitialDistribution de Cost.CostEstimationNative del centro y asigna 100% como porcentaje. else Usa los valores y porcentajes recibidos como parámetros.; si Naturaleza de cuenta (mac.Nature) = 1 → Aplica signo positivo (+1) al saldo (DebitValue-CreditValue). else Aplica signo negativo (-1) al saldo.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetCalculateDistributionIntermediate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostIntermediateDistributionBase; Cost.CostIntermediateDistributionBaseDetail; Cost.CostProductionCenter; Cost.CostProductionCenterHomologation; Cost.CostProductionCenterCostCenter; Cost.CostIntermediateDistribution; Cost.CostEstimationNative; GeneralLedger.GeneralLedgerBalance; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetCalculateDistributionIntermediate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetCalculateDistributionIntermediate';
GO
