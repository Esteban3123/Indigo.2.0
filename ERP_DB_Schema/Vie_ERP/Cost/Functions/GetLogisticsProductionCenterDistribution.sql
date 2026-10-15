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