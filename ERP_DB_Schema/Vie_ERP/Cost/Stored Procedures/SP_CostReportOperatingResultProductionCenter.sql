-- =============================================
-- Author:		Carlos Jhefersson Muñoz Ramirez
-- Create date: 06/05/2017
-- Description:	Store para el reporte de resultado de operaciones por centro de produccion
-- =============================================
CREATE PROCEDURE [Cost].[SP_CostReportOperatingResultProductionCenter]
	@InitialMonth int,
	@EndMonth int,
	@Year int,
	@CodePCenterIni varchar(50),
    @CodePCenterFin varchar(50),
	@OrderBy int
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Table_Result AS TABLE
	(
		ProductionCenterCode VARCHAR(20), 
		ProductionCenterName VARCHAR(500),
		TotalDistribution DECIMAL(20,4) DEFAULT(0),
		TotalSales DECIMAL(20,4) DEFAULT(0)
	)

	BEGIN TRY

		SET @CodePCenterIni = IIF(@CodePCenterIni = '', NULL, @CodePCenterIni)
		SET @CodePCenterFin = IIF(@CodePCenterFin = '', NULL, @CodePCenterFin)

		/**********************************  OBTENCION DE LA ESTIMACIONES DE COSTOS **********************************/

		INSERT INTO @Table_Result
		(
			ProductionCenterCode, ProductionCenterName, TotalDistribution, TotalSales
		)
		SELECT cpc.Code,cpc.Name, SUM(cen.SecondaryDistribution) AS TotalDistribution, SUM(cen.TotalSales) TotalSales
		FROM Cost.CostEstimationNative cen WITH (NOLOCK)
		JOIN Cost.CostProductionCenter cpc WITH (NOLOCK) ON cen.ProductionCenterId = cpc.Id
		WHERE (cen.Year = @Year AND (cen.Month >= @InitialMonth OR cen.Month <= @EndMonth))
			AND cpc.Code BETWEEN ISNULL(@CodePCenterIni, '0') AND ISNULL(@CodePCenterFin, 'ZZZZZZZZZZZZZZZZZZZ')
		GROUP BY cpc.Code, cpc.Name

	END TRY
	BEGIN CATCH	
		INSERT INTO @Table_Result 
		(
			ProductionCenterCode, ProductionCenterName
		)
		SELECT	'999', ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	/*************************************************** RESULTADO ***************************************************/

	SELECT	CASE @OrderBy
				WHEN 1 THEN tr.ProductionCenter
				WHEN 2 THEN CAST(tr.TotalCost AS VARCHAR(500))
				WHEN 3 THEN CAST(tr.BillingValue AS VARCHAR(500))
				WHEN 4 THEN CAST(tr.Diference AS VARCHAR(500))
				WHEN 5 THEN CAST(tr.Margin AS VARCHAR(500))
				WHEN 6 THEN CAST(tr.Utility AS VARCHAR(500))
			END OrderBy,
			*
	FROM
	(
		SELECT	CONCAT(tr.ProductionCenterCode, ' - ', tr.ProductionCenterName) ProductionCenter,
				TotalDistribution TotalCost,
				TotalSales BillingValue,
				TotalSales - TotalDistribution Diference,
				IIF
				(
					TotalDistribution = 0, 
					IIF(TotalSales = 0, 0, 1), 
					(TotalSales - TotalDistribution) / TotalDistribution
				) Margin,
				IIF
				(
					TotalSales = 0, 
					0, 
					(TotalSales - TotalDistribution) / TotalSales
				) Utility
		FROM @Table_Result tr
		WHERE TotalDistribution <> 0 OR TotalSales <> 0
	) tr
	ORDER BY 1
END