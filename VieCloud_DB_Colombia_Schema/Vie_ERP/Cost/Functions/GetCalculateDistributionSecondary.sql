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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de costeo que calcula la distribución secundaria de costos para un centro de producción en un período (año y mes) determinado. Recibe un monto y un porcentaje a distribuir, y devuelve una tabla con los centros de producción receptores, su nombre codificado y el valor o porcentaje que le corresponde a cada uno. Para construir los resultados, consulta las bases de distribución secundaria (CostDistributionSecondaryBase) según el tipo de criterio configurado: distribución por valores históricos de distribuciones directas previas (tipo 1), por cantidades manuales definidas por centro (tipo 2), o por indicadores operativos del centro de producción como área física, horas oficiales de mano de obra, valor de insumos, mano de obra, activos, facturación o costos primos (tipo 3). Es el núcleo del proceso de costeo indirecto secundario, permitiendo repartir costos administrativos o de apoyo entre las áreas productivas de la institución de salud.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'FUNCTION', @level1name = N'GetCalculateDistributionSecondary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'FUNCTION', @level1name = N'GetCalculateDistributionSecondary';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la distribución secundaria de un costo entre centros de producción, asignando valor y porcentaje proporcionalmente según las bases configuradas (área, horas oficiales, insumos, mano de obra, activos, facturación, estimaciones nativas o distribución directa).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetCalculateDistributionSecondary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en Cost.CostDistributionSecondaryBase asociado al CostDistributionSecondaryId recibido.; Para DistributionType=1 deben existir registros en Cost.CostDirectDistributionSecondary con Status=2 para el año/mes.; Para DistributionType=3 con OfficialHours, debe existir Cost.CostDistributionManpower con Status=1 para el año/mes.; Si @ValueToDistribute es NULL o 0, debe existir un registro en Cost.CostEstimationNative para el ProductionCenter del CostDistributionSecondary en el año/mes.; @TotalValue (suma de Value de la tabla resultante) no debe ser cero al momento del cálculo proporcional.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetCalculateDistributionSecondary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La suma de Value de la tabla resultante siempre cuadra con @ValueToDistribute (ajuste de diferencia en la fila de menor Id).; La suma de Percentage siempre cuadra con @PercentageToDistribute.; Cada ProductionCenter aparece una sola vez en la tabla resultado (los INSERT filtran por d.ProductionCenterId IS NULL y los UPDATE acumulan sobre el existente).; La distribución solo considera los centros de producción presentes en la base configurada (CostDistributionSecondaryBaseDetail) o en distribuciones directas con Status=2.; Solo se incluyen movimientos contables de mano de obra cuando CostDistributionManpower.Status = 1.; El signo del saldo contable se determina por la naturaleza de la clase de cuenta (Nature=1 directo, otros invertido).; Si no hay datos en el mes solicitado, retrocede mes a mes hasta encontrar información o llegar a mes 0.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetCalculateDistributionSecondary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @CalculateDistributionSecondary: Cuando DistributionType=1, inserta los centros de producción con la suma del Value de CostDirectDistributionSecondaryDetail (priorizando el valor de redistribución si existe) para registros con Status=2 del año/mes.; [INSERT] @CalculateDistributionSecondary: Cuando DistributionType=2, inserta o acumula la cantidad (Quantity) de CostDistributionSecondaryBaseDetail por centro de producción.; [UPDATE] @CalculateDistributionSecondary: Cuando DistributionType=3 y Area=1, suma el Area del CostProductionCenter al Value del centro correspondiente.; [UPDATE] @CalculateDistributionSecondary: Cuando DistributionType=3 y OfficialHours=1, suma HoursQuantity de CostDistributionManpowerDetail (con CostDistributionManpower Status=1, año y mes solicitados) al Value.; [UPDATE] @CalculateDistributionSecondary: Cuando DistributionType=3 y SupplyValue/WorkmanshipValue/AssetValue/InvoiceValue=1, suma (DebitValue-CreditValue) ajustado por naturaleza de la cuenta (1=positivo, distinto=negativo) filtrando HomologationType=2 (insumos), 1 (mano de obra), 5 (activos) o 6 con AllowSecondaryDistribution=1 (facturación) según corresponda.; [UPDATE] @CalculateDistributionSecondary: Cuando DistributionType=3 y CousinValue=1, suma InitialDistribution de CostEstimationNative del año/mes al Value.; [UPDATE] @CalculateDistributionSecondary: Tras consolidar bases, recalcula Value = ValueToDistribute * (Value/TotalValue) y Percentage = PercentageToDistribute * (Value/TotalValue).; [UPDATE] @CalculateDistributionSecondary: Si la suma final de Value difiere de ValueToDistribute, ajusta la diferencia en la fila de menor Id; igual ajuste se aplica al Percentage frente a PercentageToDistribute.; [RETURN_RESULT] @CalculateDistributionSecondary: Devuelve la tabla con ProductionCenterId, código-nombre concatenados, Percentage y Value distribuidos.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetCalculateDistributionSecondary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DistributionType = 1 → Carga base desde la distribución secundaria directa (CostDirectDistributionSecondary con Status=2) usando el valor de redistribución si existe, sino el valor del detalle.; si DistributionType = 2 → Carga base desde las cantidades configuradas en CostDistributionSecondaryBaseDetail.; si DistributionType = 3 → Compone la base sumando los componentes habilitados: Area, OfficialHours, SupplyValue, WorkmanshipValue, AssetValue, InvoiceValue y CousinValue.; si Dentro de DistributionType=3, HomologationType según flag activo → SupplyValue→type 2; WorkmanshipValue→type 1; AssetValue→type 5; InvoiceValue→type 6 con AllowSecondaryDistribution=1.; si MainAccountClass.Nature = 1 → El saldo (Debit-Credit) se suma con signo positivo. else Se invierte el signo (multiplicado por -1).; si Tras procesar todas las bases, la tabla resultado está vacía y @DistributionMonth > 0 → Decrementa el mes en 1 y reinicia el ciclo de carga (GOTO DISTRIBUTION_POINT) para buscar bases del mes anterior.; si ISNULL(@ValueToDistribute,0) = 0 → Toma como valor a distribuir el InitialDistribution de CostEstimationNative del centro asociado al CostDistributionSecondary y fija el porcentaje a 100.; si Diferencia entre suma calculada y valor/porcentaje objetivo distinta de 0 → Ajusta el residuo en la fila de menor Id para garantizar cuadre exacto.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetCalculateDistributionSecondary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetCalculateDistributionSecondary';
GO
