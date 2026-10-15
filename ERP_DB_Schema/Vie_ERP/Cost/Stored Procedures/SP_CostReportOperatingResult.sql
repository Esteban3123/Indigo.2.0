-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 04/11/2016
-- Description:	Store para el reporte de resultado de operaciones
-- =============================================
-- =============================================
-- Author:		Carlos Jhefersson Muñoz Ramirez
-- Create Modified: 10/04/2017
-- Description:	Stored para el reporte de Structure Organizacional Mayorizado
-- =============================================
CREATE PROCEDURE [Cost].[SP_CostReportOperatingResult]
	@InitialMonth INT,
	@EndMonth INT,
	@Year INT,
	@CodePCenterIni VARCHAR(50),
    @CodePCenterFin VARCHAR(50),
    @StructureOfCostId INT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Table_Result AS TABLE
	(
		Id VARCHAR(MAX),
		OrganizationalStructureParentId INT,
		OrganizationalStructureId INT,
		OrganizationalStructureCode VARCHAR(20), 
		OrganizationalStructureName VARCHAR(500),
		OrganizationalStructureLevel TINYINT,
		TotalDistribution DECIMAL(20,4) DEFAULT(0),
		TotalSales DECIMAL(20,4) DEFAULT(0),
		Filter BIT DEFAULT(0)
	)

	BEGIN TRY

		SET @CodePCenterIni = IIF(@CodePCenterIni = '', NULL, @CodePCenterIni)
		SET @CodePCenterFin = IIF(@CodePCenterFin = '', NULL, @CodePCenterFin)
		SET @StructureOfCostId = IIF(@StructureOfCostId = 0, NULL, @StructureOfCostId)

		/********************************* OBTENCION DE LA ESTRUCTURA ORGANIZACIONAL *********************************/

		DECLARE @LevelRows INT = 1,
				@CurrentLevel INT = 0

		WHILE @LevelRows > 0
		BEGIN
			SELECT TOP 1
				@CurrentLevel = Level
			FROM Cost.CostOrganizationalStructureOfCosts
			WHERE Level > @CurrentLevel
			ORDER BY Level

			SET @LevelRows = @@ROWCOUNT
			IF @LevelRows = 0
			BEGIN
				BREAK
			END

			INSERT INTO @Table_Result
			(
				Id, OrganizationalStructureParentId, OrganizationalStructureId, OrganizationalStructureCode, OrganizationalStructureName, OrganizationalStructureLevel
			)
			SELECT CONCAT(tr.Id, '*IND*', cosc.Code), cosc.ParentId, cosc.Id, cosc.Code, cosc.Name, cosc.Level
			FROM Cost.CostOrganizationalStructureOfCosts cosc
			LEFT JOIN @Table_Result tr ON cosc.ParentId = tr.OrganizationalStructureId
			WHERE Level = @CurrentLevel
		END

		/****************************  OBTENCION DE VALORES POR ESTRUCTURA ORGANIZACIONAL ****************************/

		UPDATE tr 
			SET tr.TotalDistribution = cen.TotalDistribution,
				tr.TotalSales = cen.TotalSales,
				tr.Filter = cen.Filter
		FROM @Table_Result tr
		JOIN
		(
			SELECT	cosc.Id OrganizationalStructureId,
					SUM(cen.SecondaryDistribution) AS TotalDistribution,
					SUM(cen.TotalSales) TotalSales,
					MAX
					(	
						IIF(cpc.Code BETWEEN ISNULL(@CodePCenterIni, '0') AND ISNULL(@CodePCenterFin, 'ZZZZZZZZZZZZZZZZZZZ'), IIF(ISNULL(@StructureOfCostId, cosc.Id) = cosc.Id, 1, 0), 0)
					) Filter
			FROM Cost.CostEstimationNative cen WITH (NOLOCK)
			JOIN Cost.CostProductionCenter cpc WITH (NOLOCK) ON cen.ProductionCenterId = cpc.Id
			JOIN Cost.CostOrganizationalStructureOfCosts cosc WITH (NOLOCK) ON cpc.OrganizationalStructureOfCostId = cosc.Id
			WHERE (cen.Year = @Year AND (cen.Month >= @InitialMonth OR cen.Month <= @EndMonth))
			GROUP BY cosc.Id
		) cen ON tr.OrganizationalStructureId = cen.OrganizationalStructureId

		UPDATE @Table_Result SET Filter = 1 WHERE OrganizationalStructureId = @StructureOfCostId

		/***********************************************  MAYORIZACION ***********************************************/

		DECLARE @LevelMax INT

		SELECT	@LevelRows = 1,
				@CurrentLevel = 0,
				@LevelMax = MAX(OrganizationalStructureLevel) FROM @Table_Result

		WHILE @LevelRows > 0
		BEGIN
			SELECT TOP 1
				@CurrentLevel = OrganizationalStructureLevel
			FROM @Table_Result
			WHERE @LevelMax > OrganizationalStructureLevel
			ORDER BY OrganizationalStructureLevel DESC

			SET @LevelRows = @@ROWCOUNT
			IF @LevelRows = 0 OR @CurrentLevel = @LevelMax
			BEGIN
				BREAK
			END

			UPDATE tr
				SET tr.TotalDistribution += gtr.TotalDistribution,
					tr.TotalSales += gtr.TotalSales
			FROM @Table_Result tr
			JOIN
			(
				SELECT	OrganizationalStructureParentId,
						SUM(TotalDistribution) TotalDistribution,
						SUM(TotalSales) TotalSales
				FROM @Table_Result
				GROUP BY OrganizationalStructureParentId
			) gtr ON tr.OrganizationalStructureId = gtr.OrganizationalStructureParentId
			WHERE tr.OrganizationalStructureLevel = @CurrentLevel

			SET @LevelMax = @CurrentLevel
		END

		/**************************************************  FILTRO **************************************************/

		IF @StructureOfCostId IS NOT NULL OR (@CodePCenterIni IS NOT NULL AND @CodePCenterFin IS NOT NULL)
		BEGIN
			DECLARE @StructureOfCostRows INT = 1,
					@ParentId INT

			WHILE @StructureOfCostRows > 0
			BEGIN
				SELECT TOP 1
					@ParentId = trp.OrganizationalStructureId
				FROM @Table_Result tr
				JOIN @Table_Result trp ON tr.OrganizationalStructureParentId = trp.OrganizationalStructureId
				WHERE tr.Filter = 1 AND trp.Filter = 0

				SET @StructureOfCostRows = @@ROWCOUNT
				IF @StructureOfCostRows = 0
				BEGIN
					BREAK
				END

				UPDATE trp
					SET trp.Filter = 1
				FROM @Table_Result tr
				JOIN @Table_Result trp ON tr.OrganizationalStructureParentId = trp.OrganizationalStructureId
				WHERE tr.Filter = 1 AND trp.Filter = 0
			END

			DELETE @Table_Result WHERE Filter = 0
		END

	END TRY
	BEGIN CATCH	
		INSERT INTO @Table_Result 
		(
			OrganizationalStructureCode, OrganizationalStructureName
		)
		SELECT	'999', ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	/*************************************************** RESULTADO ***************************************************/

	SELECT	OrganizationalStructureParentId ParentId,
			OrganizationalStructureId Id,
			OrganizationalStructureCode,
			OrganizationalStructureName,
			OrganizationalStructureLevel Level,
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
	ORDER BY tr.Id
END