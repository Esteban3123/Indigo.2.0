-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-13
-- Description:	Genera la distribución de centros Logísticos
-- =============================================
CREATE FUNCTION [Cost].[GetLogisticsProductionCenterDistribution]
(
	@Year INT,
	@Month INT,
	@LogisticsProductionCenterId INT
)
RETURNS @LogisticsProductionCenterDistribution TABLE 
(
	[Id] [int] IDENTITY(1,1),
	-------------------------------------------------------
	[ProductionCenterId] [int] NOT NULL,
	[MeasurementUnitId] [int] NULL,
	[Quantity] [numeric](18, 2) NOT NULL,
	[Percentage] [numeric](5, 2) NOT NULL,
	[Value] [decimal](20, 4) NOT NULL
)
AS
BEGIN
	DECLARE @InitialDistribution [decimal](20, 4) = 0

	SELECT @InitialDistribution = cen.InitialDistribution
	FROM Cost.CostEstimationNative cen 
	WHERE @Year = cen.Year AND @Month = cen.Month AND @LogisticsProductionCenterId = cen.ProductionCenterId

	-------------------------------------------------------

	INSERT INTO @LogisticsProductionCenterDistribution
	(
		ProductionCenterId, MeasurementUnitId, Quantity, Percentage, Value
	)
	SELECT	lpcrd.ProductionCenterId, 
			lpcrd.InventoryMeasurementUnitId MeasurementUnitId,
			SUM(lpcrd.Count) Quantity,
			0,
			0
	FROM Cost.CostLogisticsProductionCenterRecord lpcr
	JOIN Cost.CostLogisticsProductionCenterRecordDetail lpcrd ON lpcr.Id = lpcrd.LogisticsProductionCenterRecordId
	WHERE lpcr.Status = 2 AND lpcrd.Import = 0
		AND YEAR(lpcr.RecordDate) = @Year AND MONTH(lpcr.RecordDate) = @Month AND lpcr.ProductionCenterId = @LogisticsProductionCenterId
	GROUP BY lpcrd.ProductionCenterId, lpcrd.InventoryMeasurementUnitId

	-------------------------------------------------------

	UPDATE r
		SET r.Percentage = ROUND(r.Quantity / rt.TotalQuantity * 100, 2)
	FROM @LogisticsProductionCenterDistribution r
	JOIN
	(
		SELECT SUM(Quantity) TotalQuantity
		FROM @LogisticsProductionCenterDistribution
	) rt ON @LogisticsProductionCenterId = @LogisticsProductionCenterId

	-------------------------------------------------------

	UPDATE r
		SET r.Percentage = r.Percentage + rt.Diff
	FROM @LogisticsProductionCenterDistribution r
	JOIN
	(
		SELECT 100 - SUM(Percentage) Diff, MIN(Id) Id
		FROM @LogisticsProductionCenterDistribution
	) rt ON r.Id = rt.Id
	WHERE rt.Diff <> 0

	-------------------------------------------------------

	IF ISNULL(@InitialDistribution, 0) <> 0
	BEGIN
		UPDATE r
			SET r.Value = ROUND(@InitialDistribution * r.Percentage / 100, 4)
		FROM @LogisticsProductionCenterDistribution r

		-------------------------------------------------------

		UPDATE r
			SET r.Value = r.Value + rt.Diff
		FROM @LogisticsProductionCenterDistribution r
		JOIN
		(
			SELECT @InitialDistribution - SUM(Value) Diff, MIN(Id) Id
			FROM @LogisticsProductionCenterDistribution
		) rt ON r.Id = rt.Id
		WHERE rt.Diff <> 0
	END

	-------------------------------------------------------

	RETURN
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de costos que calcula la distribución del costo logístico de un centro de producción logístico hacia los centros de producción receptores, para un mes y año específicos. Toma el valor de distribución inicial registrado en la estimación nativa de costos (CostEstimationNative) y lo distribuye proporcionalmente entre los centros de producción destino, usando como base las cantidades de insumos o productos recibidos confirmados (actas en estado confirmado, sin importación) registrados en los detalles logísticos (CostLogisticsProductionCenterRecordDetail). Para cada centro receptor calcula el porcentaje que le corresponde según su volumen relativo de consumo, ajusta los redondeos para que los porcentajes sumen exactamente 100%, y aplica ese porcentaje al monto a distribuir para obtener el valor monetario asignado. Se utiliza en el proceso de distribución secundaria o cierre de costos para repartir los gastos de los centros logísticos entre los centros productivos que consumen sus servicios.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'FUNCTION', @level1name = N'GetLogisticsProductionCenterDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'FUNCTION', @level1name = N'GetLogisticsProductionCenterDistribution';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la distribución porcentual y monetaria de un centro de producción logístico hacia los centros de producción destino, prorrateando el valor de la estimación nativa según las cantidades consumidas en el período.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetLogisticsProductionCenterDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en Cost.CostLogisticsProductionCenterRecord con Status=2 para el centro y período indicados; Los detalles considerados deben tener Import=0; Para que se calculen valores monetarios debe existir una fila en Cost.CostEstimationNative con InitialDistribution distinto de cero para el centro y período', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetLogisticsProductionCenterDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La suma de Percentage de las filas resultantes siempre es exactamente 100 (corregida por residuo de redondeo en la fila de menor Id); Cuando InitialDistribution no es nulo ni cero, la suma de Value es exactamente igual a InitialDistribution; Solo se consideran registros logísticos en estado 2 y con detalle no marcado como Import (Import=0); El filtro de período se hace por año y mes de RecordDate del registro logístico; La agrupación se realiza por ProductionCenterId e InventoryMeasurementUnitId del detalle; Si no existe estimación nativa para el período/centro, Value permanece en 0 para todas las filas', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetLogisticsProductionCenterDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción logístico; Distribución de costos; Estimación de costos (CostEstimation); Unidad de medida de inventario; Prorrateo por porcentaje; Período contable (año/mes)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetLogisticsProductionCenterDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @LogisticsProductionCenterDistribution: Se inserta una fila por (ProductionCenterId, InventoryMeasurementUnitId) sumando Count, cuando lpcr.Status=2, lpcrd.Import=0 y RecordDate corresponde al año/mes del centro logístico solicitado; [UPDATE] @LogisticsProductionCenterDistribution: Asigna Percentage = ROUND(Quantity / TotalQuantity * 100, 2) para cada fila; [UPDATE] @LogisticsProductionCenterDistribution: Cuando 100 - SUM(Percentage) <> 0, ajusta el Percentage de la fila con MIN(Id) sumándole esa diferencia; [UPDATE] @LogisticsProductionCenterDistribution: Cuando InitialDistribution <> 0, asigna Value = ROUND(InitialDistribution * Percentage / 100, 4); [UPDATE] @LogisticsProductionCenterDistribution: Cuando InitialDistribution <> 0 y InitialDistribution - SUM(Value) <> 0, ajusta el Value de la fila con MIN(Id) sumándole la diferencia; [RETURN_RESULT] @LogisticsProductionCenterDistribution: Retorna la tabla con la distribución (ProductionCenterId, MeasurementUnitId, Quantity, Percentage, Value)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetLogisticsProductionCenterDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@InitialDistribution, 0) <> 0 → Calcula Value de cada fila prorrateando InitialDistribution según Percentage y ajusta el residuo de redondeo en la fila de menor Id else Value queda en 0 para todas las filas; si 100 - SUM(Percentage) <> 0 (diferencia por redondeo) → Suma la diferencia al Percentage de la fila con menor Id para garantizar que el total sea exactamente 100; si @InitialDistribution - SUM(Value) <> 0 → Suma la diferencia al Value de la fila con menor Id para garantizar que la suma de Value sea igual a InitialDistribution', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetLogisticsProductionCenterDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostEstimationNative; Cost.CostLogisticsProductionCenterRecord; Cost.CostLogisticsProductionCenterRecordDetail', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetLogisticsProductionCenterDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'GetLogisticsProductionCenterDistribution';
GO
