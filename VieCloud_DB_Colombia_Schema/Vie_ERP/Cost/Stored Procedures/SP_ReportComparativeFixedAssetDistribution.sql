-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-25
-- Description:	Procedimiento para el reporte comparativo de costos de activos fijos
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReportComparativeFixedAssetDistribution]
	@xmlCriterias AS XML,
	@xmlFilters AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE -- CRITERIOS --
			@InitialRangeYearStart INT,
			@InitialRangeMonthStart INT,
			@InitialRangeYearEnd INT,
			@InitialRangeMonthEnd INT,
			@FinalRangeYearStart INT,
			@FinalRangeMonthStart INT,
			@FinalRangeYearEnd INT,
			@FinalRangeMonthEnd INT,
			-- FILTROS --
			@TypeReport INT,
			@GroupBy INT,
			@Level INT,
			@CenterType VARCHAR(MAX),
			@Categories VARCHAR(MAX),
			@ProductionCenters VARCHAR(MAX),
			-------------
			@FilterByCategories BIT = 0,
			@FilterByProductionCenters BIT = 0

	DECLARE @Table_CenterType AS TABLE(CenterType INT)
	DECLARE @Table_Categories AS TABLE(Id INT)
	DECLARE @Table_ProductionCenters AS TABLE(Id INT)

	DECLARE @Table_Result AS TABLE
	(	
		Id INT IDENTITY(1,1),
		OrganizationalStructureParentId INT,
		OrganizationalStructureParentCode VARCHAR(20), 
		OrganizationalStructureParentName VARCHAR(500),
		OrganizationalStructureCode VARCHAR(20), 
		OrganizationalStructureName VARCHAR(200),
		OrganizationalStructureLevel INT,
		CategoryCode VARCHAR(20),
		CategoryName VARCHAR(200),
		ProductionCenterId INT,
		ProductionCenterCode VARCHAR(20),
		ProductionCenterName VARCHAR(200),
		CenterType TINYINT,
		----------------------------------------------
		PhysicalAssetId INT,
		PhysicalAssetPlate VARCHAR(50),	
		ItemCode VARCHAR(20),
		ItemDescription VARCHAR(500),
		----------------------------------------------
		Value DECIMAL(20,4),
		IsInitialRange BIT,
		IsDistribuited BIT
	)

	DECLARE @Table_DistributionSecondaryDetail AS TABLE
	(
		IsInitialRange BIT,
		SourceProductionCenterId INT,
		TargetProductionCenterId INT,
		Value DECIMAL(20,4),
		Total DECIMAL(20,4),
		CalculatedValue DECIMAL(20,4)
	)

	DECLARE @Table_DistributionSecondary AS TABLE
	(
		IsInitialRange BIT,
		ProductionCenterId INT,
		PhysicalAssetId INT,
		PhysicalAssetPlate VARCHAR(50),	
		ItemCode VARCHAR(20),
		ItemDescription VARCHAR(500),
		Value DECIMAL(20,4)
	)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 
			@InitialRangeYearStart = t.x.value('InitialRangeYearStart[1]','int'),
			@InitialRangeMonthStart = t.x.value('InitialRangeMonthStart[1]','int'),
			@InitialRangeYearEnd = t.x.value('InitialRangeYearEnd[1]','int'),
			@InitialRangeMonthEnd = t.x.value('InitialRangeMonthEnd[1]','int'),

			@FinalRangeYearStart = t.x.value('FinalRangeYearStart[1]','int'),
			@FinalRangeMonthStart = t.x.value('FinalRangeMonthStart[1]','int'),
			@FinalRangeYearEnd = t.x.value('FinalRangeYearEnd[1]','int'),
			@FinalRangeMonthEnd = t.x.value('FinalRangeMonthEnd[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		--Se obtienen los datos de los filtros
		SELECT 
			@TypeReport = t.x.value('TypeReport[1]','int'),
			@GroupBy = t.x.value('GroupBy[1]','int'),
			@Level = t.x.value('Level[1]','int'),
			@CenterType = t.x.value('CenterType[1]','varchar(max)'),
			@Categories = t.x.value('Categories[1]','varchar(max)'),
			@ProductionCenters = t.x.value('ProductionCenters[1]','varchar(max)')
		FROM @xmlFilters.nodes('/Data') t(x)

		--Se Cargan las listas enviadas

		INSERT INTO @Table_CenterType
			SELECT CAST(Data AS INT) Data 
			FROM dbo.Split(@CenterType, ',')

		IF @Categories <> ''
		BEGIN
			SET @FilterByCategories = 1

			INSERT INTO @Table_Categories
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@Categories, ',')
		END

		IF @ProductionCenters <> ''
		BEGIN
			SET @FilterByProductionCenters = 1

			INSERT INTO @Table_ProductionCenters
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@ProductionCenters, ',')
		END

		/********************************** OBTENCION DE DATOS **********************************/

		IF @TypeReport = 5
		BEGIN
			INSERT INTO @Table_Result
				SELECT
					cosc.ParentId OrganizationalStructureParentId, NULL, NULL,
					cosc.Code OrganizationalStructureCode, 
					cosc.Name OrganizationalStructureName,
					cosc.Level OrganizationalStructureLevel,
					cpcc.Code CategoryCode,
					cpcc.Name CategoryName,
					cpc.Id ProductionCenterId,
					cpc.Code ProductionCenterCode,
					cpc.Name ProductionCenterName,
					cpc.CenterType,
					fapa.Id PhysicalAssetId, 
					fapa.Plate PhysicalAssetPlate,
					fai.Code ItemCode, 
					fai.Description ItemDescription,
					SUM(cdfa.Value) Value,
					cdfa.isInitialRange,
					0 IsDistribuited
				FROM Cost.CostProductionCenter cpc WITH (NOLOCK)
				JOIN @Table_CenterType ct ON cpc.CenterType = ct.CenterType
				JOIN Cost.CostOrganizationalStructureOfCosts cosc WITH (NOLOCK) ON cpc.OrganizationalStructureOfCostId = cosc.Id
				JOIN
				(	
					SELECT	cdfa.FixedAssetPhysicalAssetId,
							cdfad.ProductionCenterId,
							1 isInitialRange,
							SUM(cdfad.DepreciationValue) Value
					FROM Cost.CostDistributionFixedAsset cdfa WITH (NOLOCK)
					JOIN Cost.CostDistributionFixedAssetDetail cdfad WITH (NOLOCK) ON cdfa.Id = cdfad.DistributionFixedAssetId
					WHERE cdfa.Status = 1 AND
					(
						(cdfa.Year > @InitialRangeYearStart OR (cdfa.Year = @InitialRangeYearStart AND cdfa.Month >= @InitialRangeMonthStart))
						AND
						(cdfa.Year < @InitialRangeYearEnd OR (cdfa.Year = @InitialRangeYearEnd AND cdfa.Month <= @InitialRangeMonthEnd))
					)
					GROUP BY cdfa.FixedAssetPhysicalAssetId, cdfad.ProductionCenterId
				UNION ALL
					SELECT	cdfa.FixedAssetPhysicalAssetId,
							cdfad.ProductionCenterId,
							0 isInitialRange,
							SUM(cdfad.DepreciationValue) Value
					FROM Cost.CostDistributionFixedAsset cdfa WITH (NOLOCK)
					JOIN Cost.CostDistributionFixedAssetDetail cdfad WITH (NOLOCK) ON cdfa.Id = cdfad.DistributionFixedAssetId
					WHERE cdfa.Status = 1 AND
					(
						(cdfa.Year > @FinalRangeYearStart OR (cdfa.Year = @FinalRangeYearStart AND cdfa.Month >= @FinalRangeMonthStart))
						AND
						(cdfa.Year < @FinalRangeYearEnd OR (cdfa.Year = @FinalRangeYearEnd AND cdfa.Month <= @FinalRangeMonthEnd))
					)
					GROUP BY cdfa.FixedAssetPhysicalAssetId, cdfad.ProductionCenterId
				) cdfa ON cpc.Id = cdfa.ProductionCenterId
				JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON cdfa.FixedAssetPhysicalAssetId = fapa.Id
				JOIN FixedAsset.FixedAssetItem fai WITH (NOLOCK) ON fapa.ItemId = fai.Id
				LEFT JOIN Cost.CostProductionCenterCategory cpcc WITH (NOLOCK) ON cpc.CategoryId = cpcc.Id
				LEFT JOIN @Table_Categories c ON cpcc.Id = c.Id
				LEFT JOIN @Table_ProductionCenters pc ON cpc.Id = pc.Id
				WHERE (@FilterByCategories = 0 OR c.Id IS NOT NULL)
					AND (@FilterByProductionCenters = 0 OR pc.Id IS NOT NULL)
					AND (@GroupBy <> 3 OR cosc.Level >= @Level)
				GROUP BY	cosc.ParentId, cosc.Code, cosc.Name, cosc.Level,
							cpcc.Code, cpcc.Name, 
							cpc.Id, cpc.Code, cpc.Name, cpc.CenterType,
							fapa.Id, fapa.Plate,
							fai.Code, fai.Description,
							cdfa.isInitialRange
				OPTION (RECOMPILE)

			/************************************************* DISTRIBUCIÓN SECUNDARIA *************************************************/
/*
			INSERT INTO @Table_DistributionSecondaryDetail
				SELECT 
					1 IsInitialRange, cds.ProductionCenterId, cddsd.ProductionCenterId, SUM(cddsd.Value), 0, 0
				FROM Cost.CostDistributionSecondary cds WITH (NOLOCK)
				JOIN Cost.CostDirectDistributionSecondary cdds WITH (NOLOCK) ON cds.Id = cdds.DistributionSecondaryId
				JOIN Cost.CostDirectDistributionSecondaryDetail cddsd WITH (NOLOCK) ON cdds.Id = cddsd.DirectDistributionSecondaryId
				WHERE cdds.Status = 2 AND
					(
						(cdds.Year > @InitialRangeYearStart OR (cdds.Year = @InitialRangeYearStart AND cdds.Month >= @InitialRangeMonthStart))
						AND
						(cdds.Year < @InitialRangeYearEnd OR (cdds.Year = @InitialRangeYearEnd AND cdds.Month <= @InitialRangeMonthEnd))
					)
				GROUP BY cds.ProductionCenterId, cddsd.ProductionCenterId
			UNION ALL
				SELECT 
					0 IsInitialRange, cds.ProductionCenterId, cddsd.ProductionCenterId, SUM(cddsd.Value), 0, 0
				FROM Cost.CostDistributionSecondary cds WITH (NOLOCK)
				JOIN Cost.CostDirectDistributionSecondary cdds WITH (NOLOCK) ON cds.Id = cdds.DistributionSecondaryId
				JOIN Cost.CostDirectDistributionSecondaryDetail cddsd WITH (NOLOCK) ON cdds.Id = cddsd.DirectDistributionSecondaryId
				WHERE cdds.Status = 2 AND
					(
						(cdds.Year > @FinalRangeYearStart OR (cdds.Year = @FinalRangeYearStart AND cdds.Month >= @FinalRangeMonthStart))
						AND
						(cdds.Year < @FinalRangeYearEnd OR (cdds.Year = @FinalRangeYearEnd AND cdds.Month <= @FinalRangeMonthEnd))
					)
				GROUP BY cds.ProductionCenterId, cddsd.ProductionCenterId

			UPDATE ds
				SET ds.Total = sds.Value
			FROM @Table_DistributionSecondaryDetail ds
			JOIN
			(
				SELECT IsInitialRange, SourceProductionCenterId, SUM(Value) Value
				FROM @Table_DistributionSecondaryDetail
				GROUP BY IsInitialRange, SourceProductionCenterId
			) sds ON ds.IsInitialRange = sds.IsInitialRange AND ds.SourceProductionCenterId = sds.SourceProductionCenterId

			-----------------------------------------------------------------------------------------------------------------------------

			DECLARE @ResultRows INT = 1,
					@ResultId INT = 0,					
					@SourceProductionCenterId INT = 0,
					@IsInitalRange BIT,
					@SourceValue DECIMAL(20,4),
					------------------------------------------------------					
					@Difference DECIMAL(20,4)

			WHILE @ResultRows > 0
			BEGIN
				SELECT TOP 1 
					@ResultId = tr.Id,
					@SourceProductionCenterId = tds.SourceProductionCenterId,
					@IsInitalRange = tr.IsInitialRange,
					@SourceValue = tr.Value
				FROM @Table_Result tr
				JOIN @Table_DistributionSecondaryDetail tds ON tr.ProductionCenterId = tds.SourceProductionCenterId
				WHERE tr.IsDistribuited = 0 
					AND tr.Id > @ResultId
				ORDER BY tr.Id

				SET @ResultRows = @@ROWCOUNT
				IF @ResultRows = 0 
					BREAK

				-------------------------------------------------------------------------------------------------------------------------

				UPDATE tds
					SET CalculatedValue = Value / Total * @SourceValue
				FROM @Table_DistributionSecondaryDetail tds
				WHERE tds.IsInitialRange = @IsInitalRange AND tds.SourceProductionCenterId = @SourceProductionCenterId

				SELECT @Difference = @SourceValue - SUM(tds.CalculatedValue)
				FROM @Table_DistributionSecondaryDetail tds
				WHERE tds.IsInitialRange = @IsInitalRange AND tds.SourceProductionCenterId = @SourceProductionCenterId

				-------------------------------------------------------------------------------------------------------------------------

				IF @Difference <> 0
				BEGIN
					UPDATE TOP (1) tds
						SET tds.CalculatedValue = tds.CalculatedValue + @Difference
					FROM @Table_DistributionSecondaryDetail tds
					WHERE tds.IsInitialRange = @IsInitalRange AND SourceProductionCenterId = @SourceProductionCenterId
				END

				-------------------------------------------------------------------------------------------------------------------------

				UPDATE tr
					SET tr.Value = tr.Value - tds.CalculatedValue
				FROM @Table_Result tr
				JOIN 
				(
					SELECT SourceProductionCenterId, SUM(CalculatedValue) CalculatedValue
					FROM @Table_DistributionSecondaryDetail
					WHERE IsInitialRange = @IsInitalRange AND SourceProductionCenterId = @SourceProductionCenterId
					GROUP BY SourceProductionCenterId
				) tds ON tr.ProductionCenterId = tds.SourceProductionCenterId
				WHERE tr.Id = @ResultId

				-------------------------------------------------------------------------------------------------------------------------

				INSERT @Table_DistributionSecondary
					SELECT
						@IsInitalRange IsInitialRange,
						tds.TargetProductionCenterId,
						tr.PhysicalAssetId,
						tr.PhysicalAssetPlate,
						tr.ItemCode,
						tr.ItemDescription,
						tds.CalculatedValue Value
					FROM @Table_Result tr
					JOIN @Table_DistributionSecondaryDetail tds ON tr.ProductionCenterId = tds.SourceProductionCenterId
					WHERE tr.Id = @ResultId AND tds.CalculatedValue <> 0
			END

			-----------------------------------------------------------------------------------------------------------------------------

			INSERT @Table_Result
				SELECT
					cosc.ParentId OrganizationalStructureParentId, NULL, NULL,
					cosc.Code OrganizationalStructureCode, 
					cosc.Name OrganizationalStructureName,
					cosc.Level OrganizationalStructureLevel,
					cpcc.Code CategoryCode,
					cpcc.Name CategoryName,
					cpc.Id ProductionCenterId,
					cpc.Code ProductionCenterCode,
					cpc.Name ProductionCenterName,
					cpc.CenterType,
					tds.PhysicalAssetId,
					tds.PhysicalAssetPlate,
					tds.ItemCode,
					tds.ItemDescription,
					tds.Value,
					tds.IsInitialRange,
					1 IsDistribuited
				FROM @Table_DistributionSecondary tds
				JOIN Cost.CostProductionCenter cpc WITH (NOLOCK) ON tds.ProductionCenterId = cpc.Id
				JOIN @Table_CenterType ct ON cpc.CenterType = ct.CenterType
				JOIN Cost.CostOrganizationalStructureOfCosts cosc WITH (NOLOCK) ON cpc.OrganizationalStructureOfCostId = cosc.Id
				LEFT JOIN Cost.CostProductionCenterCategory cpcc WITH (NOLOCK) ON cpc.CategoryId = cpcc.Id
				LEFT JOIN @Table_Categories c ON cpcc.Id = c.Id
				LEFT JOIN @Table_ProductionCenters pc ON cpc.Id = pc.Id
				WHERE (@FilterByCategories = 0 OR c.Id IS NOT NULL)
					AND (@FilterByProductionCenters = 0 OR pc.Id IS NOT NULL)
					AND (@GroupBy <> 3 OR cosc.Level >= @Level)
*/
			/******************************************  ASIGNACIÓN ESTRUCTURA ORGANIZACIONAL ******************************************/

			IF @GroupBy = 3
			BEGIN
				DECLARE @LevelRows INT = 1,
						@CurrentLevel INT,
						@LevelMax INT = @Level

				WHILE @LevelRows > 0
				BEGIN
					SELECT TOP 1
						@CurrentLevel = OrganizationalStructureLevel
					FROM @Table_Result
					WHERE OrganizationalStructureLevel > @Level
					ORDER BY OrganizationalStructureLevel DESC

					SET @LevelRows = @@ROWCOUNT
					IF @LevelRows = 0 OR @CurrentLevel = @LevelMax
					BEGIN
						BREAK
					END

					UPDATE r
						SET r.OrganizationalStructureParentId = cosc.ParentId,
							r.OrganizationalStructureCode = cosc.Code,
							r.OrganizationalStructureName = cosc.Name,
							r.OrganizationalStructureLevel = cosc.Level
					FROM @Table_Result r
					JOIN Cost.CostOrganizationalStructureOfCosts cosc WITH (NOLOCK) ON r.OrganizationalStructureParentId = cosc.Id
					WHERE r.OrganizationalStructureLevel = @CurrentLevel

					SET @LevelMax = @CurrentLevel
				END

				UPDATE r
					SET r.OrganizationalStructureParentCode = cosc.Code,
						r.OrganizationalStructureParentName = cosc.Name
				FROM @Table_Result r
				JOIN Cost.CostOrganizationalStructureOfCosts cosc WITH (NOLOCK) ON r.OrganizationalStructureParentId = cosc.Id
			END
		END
	END TRY
	BEGIN CATCH	
		INSERT INTO @Table_Result 
		(
			ProductionCenterCode, ProductionCenterName, 
			CategoryCode, CategoryName,
			OrganizationalStructureCode, OrganizationalStructureName
		)
		SELECT	'999', ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)),
				'999', ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)),
				'999', ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	SELECT 
		ISNULL(ri.OrganizationalStructureParentCode, rf.OrganizationalStructureParentCode) ParentCode, 
		ISNULL(ri.OrganizationalStructureParentName, rf.OrganizationalStructureParentName) ParentName,
		CASE @GroupBy
			WHEN 1 THEN ISNULL(ri.ProductionCenterCode, rf.ProductionCenterCode)
			WHEN 2 THEN ISNULL(ri.CategoryCode, rf.CategoryCode)
			WHEN 3 THEN ISNULL(ri.OrganizationalStructureCode, rf.OrganizationalStructureCode)
		END Code, 
		CASE @GroupBy
			WHEN 1 THEN ISNULL(ri.ProductionCenterName, rf.ProductionCenterName)
			WHEN 2 THEN ISNULL(ri.CategoryName, rf.CategoryName)
			WHEN 3 THEN ISNULL(ri.OrganizationalStructureName, rf.OrganizationalStructureName)
		END Name,
		ISNULL(ri.CenterType, rf.CenterType) CenterType,
		CASE ISNULL(ri.CenterType, rf.CenterType)
			WHEN 1 THEN 'Operativo'
			WHEN 2 THEN 'Administrativo'
			WHEN 3 THEN 'Logístico'
			ELSE 'N/A'
		END CenterTypeName,
		ISNULL(ri.PhysicalAssetPlate, rf.PhysicalAssetPlate) PhysicalAssetPlate,
		ISNULL(ri.ItemCode, rf.ItemCode) ItemCode, ISNULL(ri.ItemDescription, rf.ItemDescription) ItemDescription,
		SUM(ISNULL(ri.Value, 0)) InitialRangeValue,
		SUM(ISNULL(rf.Value, 0)) FinalRangeValue
	FROM 
	(
		SELECT	OrganizationalStructureParentCode, OrganizationalStructureParentName,
				OrganizationalStructureCode, OrganizationalStructureName,					
				CategoryCode, CategoryName,
				ProductionCenterId, ProductionCenterCode, ProductionCenterName, CenterType,
				PhysicalAssetId, PhysicalAssetPlate,
				ItemCode, ItemDescription,
				SUM(Value) Value			
		FROM @Table_Result
		WHERE IsInitialRange = 1
		GROUP BY	OrganizationalStructureParentCode, OrganizationalStructureParentName,
					OrganizationalStructureCode, OrganizationalStructureName,					
					CategoryCode, CategoryName,
					ProductionCenterId, ProductionCenterCode, ProductionCenterName, CenterType,
					PhysicalAssetId, PhysicalAssetPlate,
					ItemCode, ItemDescription
	) ri
	FULL JOIN 
	(
		SELECT	OrganizationalStructureParentCode, OrganizationalStructureParentName,
				OrganizationalStructureCode, OrganizationalStructureName,					
				CategoryCode, CategoryName,
				ProductionCenterId, ProductionCenterCode, ProductionCenterName, CenterType,
				PhysicalAssetId, PhysicalAssetPlate,
				ItemCode, ItemDescription,
				SUM(Value) Value			
		FROM @Table_Result
		WHERE IsInitialRange = 0
		GROUP BY	OrganizationalStructureParentCode, OrganizationalStructureParentName,
					OrganizationalStructureCode, OrganizationalStructureName,					
					CategoryCode, CategoryName,
					ProductionCenterId, ProductionCenterCode, ProductionCenterName, CenterType,
					PhysicalAssetId, PhysicalAssetPlate,
					ItemCode, ItemDescription
	) rf 
		ON ri.ProductionCenterId = rf.ProductionCenterId 
			AND ISNULL(ri.PhysicalAssetId, 0) = ISNULL(rf.PhysicalAssetId, 0)	
	GROUP BY
		ISNULL(ri.OrganizationalStructureParentCode, rf.OrganizationalStructureParentCode), ISNULL(ri.OrganizationalStructureParentName, rf.OrganizationalStructureParentName),
		CASE @GroupBy
			WHEN 1 THEN ISNULL(ri.ProductionCenterCode, rf.ProductionCenterCode)
			WHEN 2 THEN ISNULL(ri.CategoryCode, rf.CategoryCode)
			WHEN 3 THEN ISNULL(ri.OrganizationalStructureCode, rf.OrganizationalStructureCode)
		END, 
		CASE @GroupBy
			WHEN 1 THEN ISNULL(ri.ProductionCenterName, rf.ProductionCenterName)
			WHEN 2 THEN ISNULL(ri.CategoryName, rf.CategoryName)
			WHEN 3 THEN ISNULL(ri.OrganizationalStructureName, rf.OrganizationalStructureName)
		END,
		ISNULL(ri.CenterType, rf.CenterType),
		ISNULL(ri.PhysicalAssetPlate, rf.PhysicalAssetPlate),
		ISNULL(ri.ItemCode, rf.ItemCode), ISNULL(ri.ItemDescription, rf.ItemDescription)
	HAVING SUM(ISNULL(ri.Value, 0)) <> 0 OR SUM(ISNULL(rf.Value, 0)) <> 0
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte comparativo de distribución de costos de activos fijos entre dos períodos de tiempo (rango inicial y rango final), utilizado en el módulo de costos para analizar cómo varía la depreciación de activos fijos asignada a cada centro de producción de un período a otro. Combina información de centros de producción, su estructura organizacional de costos y categorías, con el detalle de distribución de depreciación por activo físico, permitiendo filtrar por tipo de centro, categoría y centros de producción específicos. Soporta diferentes tipos de reporte y niveles de agrupación, facilitando la toma de decisiones sobre la imputación de costos fijos en la organización sanitaria.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportComparativeFixedAssetDistribution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportComparativeFixedAssetDistribution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte comparativo de depreciación/costos de activos fijos distribuidos a centros de producción entre dos rangos de periodos (inicial vs final), agrupable por centro, categoría o estructura organizacional.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeFixedAssetDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@xmlCriterias debe contener los nodos de año/mes inicial y final de ambos rangos a comparar.; @xmlFilters debe traer TypeReport, GroupBy, Level y al menos CenterType (lista separada por comas).; Solo se procesan distribuciones con Status = 1 en Cost.CostDistributionFixedAsset.; El reporte solo se genera cuando @TypeReport = 5.; Si se envía lista de Categories o ProductionCenters, deben ser ids enteros separados por coma.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeFixedAssetDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran distribuciones de activos fijos con Status = 1.; El rango Año/Mes se evalúa con lógica compuesta: (Year>YS OR (Year=YS AND Month>=MS)) AND (Year<YE OR (Year=YE AND Month<=ME)).; Los centros de producción incluidos siempre están restringidos por la lista @Table_CenterType.; Si @FilterByCategories=0 no se filtra por categoría; si =1, solo se incluyen las categorías de la lista.; Si @FilterByProductionCenters=0 no se filtra por centro; si =1, solo se incluyen los centros de la lista.; El bloque de distribución secundaria está comentado: actualmente NO se realiza redistribución secundaria, todos los registros quedan con IsDistribuited=0.; El resultado final omite filas con valor cero en ambos rangos.; Los errores en tiempo de ejecución no propagan excepción; se devuelven como una fila marcada con código ''999''.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeFixedAssetDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo físico (placa); Depreciación de activos fijos; Centro de producción / centro de costo; Tipo de centro: Operativo, Administrativo, Logístico; Categoría de centro de producción; Estructura organizacional de costos (jerarquía por niveles); Distribución de costos de activos fijos; Comparativo entre rango inicial y rango final por periodo (año/mes)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeFixedAssetDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Table_Result: Cuando @TypeReport = 5, inserta los activos fijos distribuidos sumando DepreciationValue por rango inicial (IsInitialRange=1) y rango final (IsInitialRange=0), filtrando por Status=1 y por el rango Año/Mes correspondiente.; [UPDATE] @Table_Result: Cuando @GroupBy = 3, escala iterativamente la estructura organizacional hacia el padre hasta alcanzar el @Level configurado, actualizando ParentId/Code/Name/Level.; [INSERT] @Table_Result: En el bloque CATCH inserta una fila ficticia con código ''999'' y el mensaje/línea del error en los campos de centro, categoría y estructura organizacional.; [RETURN_RESULT] RESULT: Devuelve un FULL JOIN entre el agregado del rango inicial y el final por ProductionCenterId y PhysicalAssetId, descartando filas donde InitialRangeValue=0 y FinalRangeValue=0 (HAVING).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeFixedAssetDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Categories <> '''' → Activa @FilterByCategories=1 y carga la lista de categorías a filtrar. else No se filtra por categorías.; si @ProductionCenters <> '''' → Activa @FilterByProductionCenters=1 y carga los centros de producción permitidos. else No se filtra por centros de producción.; si @TypeReport = 5 → Ejecuta la carga de datos de distribución de activos fijos y, si aplica, el reagrupamiento por estructura organizacional. else No se cargan datos en @Table_Result (solo se devolverá el resultado vacío o el error).; si @GroupBy = 3 → Itera subiendo niveles de la estructura organizacional hasta @Level y agrupa el resultado por estructura organizacional. else Agrupa por ProductionCenter (1) o por Category (2) según @GroupBy en el SELECT final.; si CenterType IN (1,2,3) en proyección final → Etiqueta como ''Operativo'' (1), ''Administrativo'' (2) o ''Logístico'' (3). else Etiqueta como ''N/A''.; si @GroupBy <> 3 OR cosc.Level >= @Level → Incluye la fila en el resultado. else Excluye filas cuyo nivel de estructura sea menor a @Level cuando se agrupa por estructura.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeFixedAssetDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeFixedAssetDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostProductionCenter; Cost.CostOrganizationalStructureOfCosts; Cost.CostDistributionFixedAsset; Cost.CostDistributionFixedAssetDetail; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; Cost.CostProductionCenterCategory', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeFixedAssetDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeFixedAssetDistribution';
-- GO
