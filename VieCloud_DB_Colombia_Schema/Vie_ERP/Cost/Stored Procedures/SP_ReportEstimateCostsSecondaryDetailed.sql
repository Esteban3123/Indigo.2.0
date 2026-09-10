-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-30
-- Description:	Procedimiento para el reporte de estimacion de costos tipo secundario detallado
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReportEstimateCostsSecondaryDetailed]
	@xmlCriterias AS XML,
	@xmlFilters AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE -- CRITERIOS --
			@YearStart INT,
			@MonthStart INT,
			@YearEnd INT,
			@MonthEnd INT,
			@TypeReport INT,
			@GroupBy INT,
			@Level INT,
			@SecondaryDetailType INT,
			-- FILTROS --
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
		SourceOrganizationalStructureParentId INT,
		SourceOrganizationalStructureParentCode VARCHAR(20), 
		SourceOrganizationalStructureParentName VARCHAR(500),
		SourceOrganizationalStructureId INT, 
		SourceOrganizationalStructureCode VARCHAR(20), 
		SourceOrganizationalStructureName VARCHAR(500),
		SourceOrganizationalStructureLevel INT,
		SourceCategoryId INT,
		SourceCategoryCode VARCHAR(20),
		SourceCategoryName VARCHAR(500),
		SourceProductionCenterId INT,
		SourceProductionCenterCode VARCHAR(20),
		SourceProductionCenterName VARCHAR(500),
		SourceCenterType TINYINT,
		TargetOrganizationalStructureParentId INT,
		TargetOrganizationalStructureParentCode VARCHAR(20), 
		TargetOrganizationalStructureParentName VARCHAR(500),
		TargetOrganizationalStructureId INT,
		TargetOrganizationalStructureCode VARCHAR(20), 
		TargetOrganizationalStructureName VARCHAR(500),
		TargetOrganizationalStructureLevel INT,
		TargetCategoryId INT,
		TargetCategoryCode VARCHAR(20),
		TargetCategoryName VARCHAR(500),
		TargetProductionCenterId VARCHAR(20),
		TargetProductionCenterCode VARCHAR(20),
		TargetProductionCenterName VARCHAR(500),
		TargetCenterType TINYINT,
		SourceInitialDistribution DECIMAL(20,4),
		Value DECIMAL(20,4)
	)

	DECLARE @Table_CostEstimation AS TABLE
	(
		Id INT,
		InitialDistribution DECIMAL(20,4)
	)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT	@YearStart = t.x.value('YearStart[1]','int'),
				@MonthStart = t.x.value('MonthStart[1]','int'),
				@YearEnd = t.x.value('YearEnd[1]','int'),
				@MonthEnd = t.x.value('MonthEnd[1]','int'),
				@TypeReport = t.x.value('TypeReport[1]','int'),
				@GroupBy = t.x.value('GroupBy[1]','int'),
				@Level = t.x.value('Level[1]','int'),
				@SecondaryDetailType = t.x.value('SecondaryDetailType[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		--Se obtienen los datos de los filtros
		SELECT	@CenterType = t.x.value('CenterType[1]','varchar(max)'),
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

		IF @TypeReport = 4
		BEGIN
			INSERT INTO @Table_Result
				SELECT	scosc.ParentId OrganizationalStructureParentId, NULL, NULL,
						scosc.Id SourceOrganizationalStructureId, 
						scosc.Code SourceOrganizationalStructureCode, 
						scosc.Name SourceOrganizationalStructureName,
						scosc.Level SourceOrganizationalStructureLevel,
						scpcc.Id SourceCategoryId,
						scpcc.Code SourceCategoryCode,
						scpcc.Name SourceCategoryName,
						scpc.Id SourceProductionCenterId,
						scpc.Code SourceProductionCenterCode,
						scpc.Name SourceProductionCenterName,
						scpc.CenterType SourceCenterType,
						tcosc.ParentId OrganizationalStructureParentId, NULL, NULL,
						tcosc.Id TargetOrganizationalStructureId, 
						tcosc.Code TargetOrganizationalStructureCode, 
						tcosc.Name TargetOrganizationalStructureName,
						tcosc.Level TargetOrganizationalStructureLevel,
						tcpcc.Id TargetCategoryId,
						tcpcc.Code TargetCategoryCode,
						tcpcc.Name TargetCategoryName,
						tcpc.Id TargetProductionCenterId,
						tcpc.Code TargetProductionCenterCode,
						tcpc.Name TargetProductionCenterName,
						tcpc.CenterType TargetCenterType,
						0 SourceInitialDistribution,
						ISNULL(cddsdr.Value, cddsd.Value) Value
				FROM Cost.CostProductionCenter scpc WITH (NOLOCK)
				JOIN @Table_CenterType sct ON scpc.CenterType = sct.CenterType
				JOIN Cost.CostOrganizationalStructureOfCosts scosc WITH (NOLOCK) ON scpc.OrganizationalStructureOfCostId = scosc.Id
				--------------------------------------------------------------------------------------------------------------------
				JOIN Cost.CostDistributionSecondary cds WITH (NOLOCK) ON scpc.Id = cds.ProductionCenterId
				JOIN Cost.CostDirectDistributionSecondary cdds WITH (NOLOCK) ON cds.Id = cdds.DistributionSecondaryId
				JOIN Cost.CostDirectDistributionSecondaryDetail cddsd WITH (NOLOCK) ON cdds.Id = cddsd.DirectDistributionSecondaryId
				LEFT JOIN Cost.CostDirectDistributionSecondaryDetailRedistribution cddsdr WITH (NOLOCK) ON cddsd.Id = cddsdr.DirectDistributionSecondaryDetailId
				--------------------------------------------------------------------------------------------------------------------
				LEFT JOIN Cost.CostProductionCenter tcpc WITH (NOLOCK) ON ISNULL(cddsdr.ProductionCenterId, cddsd.ProductionCenterId) = tcpc.Id
				LEFT JOIN @Table_CenterType tct ON tcpc.CenterType = tct.CenterType
				LEFT JOIN Cost.CostOrganizationalStructureOfCosts tcosc WITH (NOLOCK) ON tcpc.OrganizationalStructureOfCostId = tcosc.Id
				--------------------------------------------------------------------------------------------------------------------
				LEFT JOIN Cost.CostProductionCenterCategory scpcc WITH (NOLOCK) ON scpc.CategoryId = scpcc.Id
				LEFT JOIN Cost.CostProductionCenterCategory tcpcc WITH (NOLOCK) ON tcpc.CategoryId = tcpcc.Id
				LEFT JOIN @Table_Categories sc ON scpcc.Id = sc.Id
				LEFT JOIN @Table_Categories tc ON tcpcc.Id = tc.Id
				LEFT JOIN @Table_ProductionCenters spc ON scpc.Id = spc.Id
				LEFT JOIN @Table_ProductionCenters tpc ON tcpc.Id = tpc.Id
				WHERE cdds.Status = 2 AND
					(
						(cdds.Year > @YearStart OR (cdds.Year = @YearStart AND cdds.Month >= @MonthStart))
						AND
						(cdds.Year < @YearEnd OR (cdds.Year = @YearEnd AND cdds.Month <= @MonthEnd))
					)
					AND (@FilterByCategories = 0 OR (sc.Id IS NOT NULL OR tc.Id IS NOT NULL))
					AND (@FilterByProductionCenters = 0 OR (spc.Id IS NOT NULL OR tpc.Id IS NOT NULL))
					AND (@GroupBy <> 3 OR (scosc.Level >= @Level AND tcosc.Level >= @Level))

			/*************************************************************************************************************************/

			INSERT INTO @Table_CostEstimation
				SELECT tr.Id, SUM(cen.InitialDistribution) InitialDistribution
				FROM 
				(
					SELECT IIF(@SecondaryDetailType = 1, SourceProductionCenterId, TargetProductionCenterId) ProductionCenterId, MIN(tr.Id) Id
					FROM @Table_Result tr
					GROUP BY IIF(@SecondaryDetailType = 1, SourceProductionCenterId, TargetProductionCenterId)
				) tr
				JOIN Cost.CostEstimationNative cen WITH (NOLOCK) ON tr.ProductionCenterId = cen.ProductionCenterId
				WHERE
				(
					(cen.Year > @YearStart OR (cen.Year = @YearStart AND cen.Month >= @MonthStart))
					AND
					(cen.Year < @YearEnd OR (cen.Year = @YearEnd AND cen.Month <= @MonthStart))
				)
				GROUP BY tr.Id

			/*************************************************************************************************************************/

			UPDATE tr
				SET tr.SourceInitialDistribution += cen.InitialDistribution
			FROM @Table_Result tr
			JOIN @Table_CostEstimation cen ON tr.Id = cen.Id

			/*************************************************************************************************************************/

			IF @GroupBy = 3
			BEGIN
				DECLARE @LevelRows INT = 1,
						@CurrentLevel INT,
						@LevelMax INT = @Level

				WHILE @LevelRows > 0
				BEGIN
					SELECT TOP 1
						@CurrentLevel = SourceOrganizationalStructureLevel
					FROM @Table_Result
					WHERE SourceOrganizationalStructureLevel > @Level
					ORDER BY SourceOrganizationalStructureLevel DESC

					SET @LevelRows = @@ROWCOUNT
					IF @LevelRows = 0 OR @CurrentLevel = @LevelMax
					BEGIN
						BREAK
					END

					UPDATE r
						SET r.SourceOrganizationalStructureParentId = cosc.ParentId,
							r.SourceOrganizationalStructureId = cosc.Id,
							r.SourceOrganizationalStructureCode = cosc.Code,
							r.SourceOrganizationalStructureName = cosc.Name,
							r.SourceOrganizationalStructureLevel = cosc.Level
					FROM @Table_Result r
					JOIN Cost.CostOrganizationalStructureOfCosts cosc WITH (NOLOCK) ON r.SourceOrganizationalStructureParentId = cosc.Id
					WHERE r.SourceOrganizationalStructureLevel = @CurrentLevel

					SET @LevelMax = @CurrentLevel
				END

				/*************************************************************************************************************************/

				UPDATE r
					SET r.SourceOrganizationalStructureParentCode = cosc.Code,
						r.SourceOrganizationalStructureParentName = cosc.Name
				FROM @Table_Result r
				JOIN Cost.CostOrganizationalStructureOfCosts cosc WITH (NOLOCK) ON r.SourceOrganizationalStructureParentId = cosc.Id

				/*************************************************************************************************************************/

				SELECT	@LevelRows = 1,
						@LevelMax = @Level

				WHILE @LevelRows > 0
				BEGIN
					SELECT TOP 1
						@CurrentLevel = TargetOrganizationalStructureLevel
					FROM @Table_Result
					WHERE TargetOrganizationalStructureLevel > @Level
					ORDER BY TargetOrganizationalStructureLevel DESC

					SET @LevelRows = @@ROWCOUNT
					IF @LevelRows = 0 OR @CurrentLevel = @LevelMax
					BEGIN
						BREAK
					END

					UPDATE r
						SET r.TargetOrganizationalStructureParentId = cosc.ParentId,
							r.TargetOrganizationalStructureId = cosc.Id,
							r.TargetOrganizationalStructureCode = cosc.Code,
							r.TargetOrganizationalStructureName = cosc.Name,
							r.TargetOrganizationalStructureLevel = cosc.Level
					FROM @Table_Result r
					JOIN Cost.CostOrganizationalStructureOfCosts cosc WITH (NOLOCK) ON r.TargetOrganizationalStructureParentId = cosc.Id
					WHERE r.TargetOrganizationalStructureLevel = @CurrentLevel

					SET @LevelMax = @CurrentLevel
				END

				UPDATE r
					SET r.TargetOrganizationalStructureParentCode = cosc.Code,
						r.TargetOrganizationalStructureParentName = cosc.Name
				FROM @Table_Result r
				JOIN Cost.CostOrganizationalStructureOfCosts cosc WITH (NOLOCK) ON r.TargetOrganizationalStructureParentId = cosc.Id
			END
		END
	END TRY
	BEGIN CATCH	
		INSERT INTO @Table_Result 
		(
			SourceProductionCenterCode, SourceProductionCenterName, 
			SourceCategoryCode, SourceCategoryName,
			SourceOrganizationalStructureCode, SourceOrganizationalStructureName,
			TargetProductionCenterCode, TargetProductionCenterName, 
			TargetCategoryCode, TargetCategoryName,
			TargetOrganizationalStructureCode, TargetOrganizationalStructureName
		)
		SELECT	'999', ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)),
				'999', ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)),
				'999', ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)),
				'999', ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)),
				'999', ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)),
				'999', ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	/***************************************************************************************************************************/

	SELECT 
		CASE @GroupBy
			WHEN 3 THEN IIF(@SecondaryDetailType = 1, r.SourceOrganizationalStructureParentCode, r.TargetOrganizationalStructureParentCode)
		END ParentCode, 
		CASE @GroupBy
			WHEN 3 THEN IIF(@SecondaryDetailType = 1, r.SourceOrganizationalStructureParentName, r.TargetOrganizationalStructureParentName)
		END ParentName, 
		CASE @GroupBy
			WHEN 1 THEN IIF(@SecondaryDetailType = 1, r.SourceProductionCenterId, r.TargetProductionCenterId)
			WHEN 2 THEN IIF(@SecondaryDetailType = 1, r.SourceCategoryId, r.TargetCategoryId)
			WHEN 3 THEN IIF(@SecondaryDetailType = 1, r.SourceOrganizationalStructureId, r.TargetOrganizationalStructureId)
		END SourceId, 
		CASE @GroupBy
			WHEN 1 THEN IIF(@SecondaryDetailType = 1, r.SourceProductionCenterCode, r.TargetProductionCenterCode)
			WHEN 2 THEN IIF(@SecondaryDetailType = 1, r.SourceCategoryCode, r.TargetCategoryCode)
			WHEN 3 THEN IIF(@SecondaryDetailType = 1, r.SourceOrganizationalStructureCode, r.TargetOrganizationalStructureCode)
		END SourceCode, 
		CASE @GroupBy
			WHEN 1 THEN IIF(@SecondaryDetailType = 1, r.SourceProductionCenterName, r.TargetProductionCenterName)
			WHEN 2 THEN IIF(@SecondaryDetailType = 1, r.SourceCategoryName, r.TargetCategoryName)
			WHEN 3 THEN IIF(@SecondaryDetailType = 1, r.SourceOrganizationalStructureName, r.TargetOrganizationalStructureName)
		END SourceName,
		IIF(@SecondaryDetailType = 1, r.SourceCenterType, r.TargetCenterType) SourceCenterType,
		CASE IIF(@SecondaryDetailType = 1, r.SourceCenterType, r.TargetCenterType)
			WHEN 1 THEN 'Operativo'
			WHEN 2 THEN 'Administrativo'
			WHEN 3 THEN 'Logístico'
			ELSE 'N/A'
		END SourceCenterTypeName,
		CASE @GroupBy
			WHEN 1 THEN IIF(@SecondaryDetailType = 1, r.TargetProductionCenterCode, r.SourceProductionCenterCode)
			WHEN 2 THEN IIF(@SecondaryDetailType = 1, r.TargetCategoryCode, r.SourceCategoryCode)
			WHEN 3 THEN IIF(@SecondaryDetailType = 1, r.TargetOrganizationalStructureCode, r.SourceOrganizationalStructureCode)
		END TargetCode, 
		CASE @GroupBy
			WHEN 1 THEN IIF(@SecondaryDetailType = 1, r.TargetProductionCenterName, r.SourceProductionCenterName)
			WHEN 2 THEN IIF(@SecondaryDetailType = 1, r.TargetCategoryName, r.SourceCategoryName)
			WHEN 3 THEN IIF(@SecondaryDetailType = 1, r.TargetOrganizationalStructureName, r.SourceOrganizationalStructureName)
		END TargetName,
		IIF(@SecondaryDetailType = 1, r.TargetCenterType, r.SourceCenterType) TargetCenterType,
		CASE IIF(@SecondaryDetailType = 1, r.TargetCenterType, r.SourceCenterType)
			WHEN 1 THEN 'Operativo'
			WHEN 2 THEN 'Administrativo'
			WHEN 3 THEN 'Logístico'
			ELSE 'N/A'
		END TargetCenterTypeName,
		SUM(r.SourceInitialDistribution) SourceInitialDistribution,
		SUM(r.Value) Value
	FROM @Table_Result r
	GROUP BY
		CASE @GroupBy
			WHEN 3 THEN IIF(@SecondaryDetailType = 1, r.SourceOrganizationalStructureParentCode, r.TargetOrganizationalStructureParentCode)
		END, 
		CASE @GroupBy
			WHEN 3 THEN IIF(@SecondaryDetailType = 1, r.SourceOrganizationalStructureParentName, r.TargetOrganizationalStructureParentName)
		END, 
		CASE @GroupBy
			WHEN 1 THEN IIF(@SecondaryDetailType = 1, r.SourceProductionCenterId, r.TargetProductionCenterId)
			WHEN 2 THEN IIF(@SecondaryDetailType = 1, r.SourceCategoryId, r.TargetCategoryId)
			WHEN 3 THEN IIF(@SecondaryDetailType = 1, r.SourceOrganizationalStructureId, r.TargetOrganizationalStructureId)
		END,
		CASE @GroupBy
			WHEN 1 THEN IIF(@SecondaryDetailType = 1, r.SourceProductionCenterCode, r.TargetProductionCenterCode)
			WHEN 2 THEN IIF(@SecondaryDetailType = 1, r.SourceCategoryCode, r.TargetCategoryCode)
			WHEN 3 THEN IIF(@SecondaryDetailType = 1, r.SourceOrganizationalStructureCode, r.TargetOrganizationalStructureCode)
		END, 
		CASE @GroupBy
			WHEN 1 THEN IIF(@SecondaryDetailType = 1, r.SourceProductionCenterName, r.TargetProductionCenterName)
			WHEN 2 THEN IIF(@SecondaryDetailType = 1, r.SourceCategoryName, r.TargetCategoryName)
			WHEN 3 THEN IIF(@SecondaryDetailType = 1, r.SourceOrganizationalStructureName, r.TargetOrganizationalStructureName)
		END,
		IIF(@SecondaryDetailType = 1, r.SourceCenterType, r.TargetCenterType),
		CASE @GroupBy
			WHEN 1 THEN IIF(@SecondaryDetailType = 1, r.TargetProductionCenterCode, r.SourceProductionCenterCode)
			WHEN 2 THEN IIF(@SecondaryDetailType = 1, r.TargetCategoryCode, r.SourceCategoryCode)
			WHEN 3 THEN IIF(@SecondaryDetailType = 1, r.TargetOrganizationalStructureCode, r.SourceOrganizationalStructureCode)
		END, 
		CASE @GroupBy
			WHEN 1 THEN IIF(@SecondaryDetailType = 1, r.TargetProductionCenterName, r.SourceProductionCenterName)
			WHEN 2 THEN IIF(@SecondaryDetailType = 1, r.TargetCategoryName, r.SourceCategoryName)
			WHEN 3 THEN IIF(@SecondaryDetailType = 1, r.TargetOrganizationalStructureName, r.SourceOrganizationalStructureName)
		END,
		IIF(@SecondaryDetailType = 1, r.TargetCenterType, r.SourceCenterType)
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte detallado de estimación de costos de distribución secundaria para un rango de períodos (mes/año de inicio y fin), permitiendo analizar cómo se redistribuyen los costos entre centros de producción en la segunda etapa del proceso de costeo. Recibe criterios de consulta (tipo de reporte, nivel, agrupación, tipo de detalle secundario) y filtros opcionales (tipo de centro, categorías y centros de producción específicos) en formato XML, y cruza las tablas de bases de distribución secundaria, distribución directa secundaria y sus redistribuciones para calcular el valor asignado desde cada centro origen hacia cada centro destino. El resultado expone la estructura organizacional de costos, la categoría y el centro de producción tanto de la fuente como del destino, junto con el valor distribuido, siendo útil para auditoría del costeo, control gerencial y conciliación de la asignación de gastos entre unidades funcionales.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportEstimateCostsSecondaryDetailed';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportEstimateCostsSecondaryDetailed';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte detallado de estimación de costos de tipo distribución secundaria, mostrando el flujo de costos entre centros de producción origen y destino agrupado por centro, categoría o estructura organizacional.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsSecondaryDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener YearStart, MonthStart, YearEnd, MonthEnd, TypeReport, GroupBy, Level y SecondaryDetailType.; El XML de filtros debe contener al menos CenterType (lista CSV) y opcionalmente Categories y ProductionCenters.; Solo se procesan datos cuando TypeReport = 4.; Para que la distribución secundaria sea considerada, debe existir con Status = 2 (aprobada/cerrada).; El rango de período (Year/Month) debe estar correctamente delimitado entre inicio y fin.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsSecondaryDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se reportan distribuciones secundarias en estado 2 (aprobadas/cerradas).; El reporte únicamente produce datos para TypeReport = 4 (estimación secundaria detallada).; El valor reportado prioriza siempre la redistribución sobre el detalle directo cuando existe redistribución.; Cuando se agrupa por estructura organizacional, ninguna fila final tiene Level inferior al @Level solicitado (las inferiores son promovidas a su antecesor).; Los errores nunca interrumpen la ejecución: se capturan y devuelven como una fila marcada con ''999''.; Los filtros por categoría y centro de producción son inclusivos: basta que origen O destino cumplan para conservar la fila.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsSecondaryDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RETURN_RESULT: Cuando @TypeReport = 4 se devuelve el resultado agrupado según @GroupBy (1=Centro de Producción, 2=Categoría, 3=Estructura Organizacional) y orientado por @SecondaryDetailType (1=origen→destino, otro=destino→origen).; [RETURN_RESULT] RETURN_RESULT: Se filtran distribuciones donde cdds.Status = 2 y el período (Year, Month) cae en el rango [YearStart/MonthStart, YearEnd/MonthEnd].; [RETURN_RESULT] RETURN_RESULT: Cuando @FilterByCategories = 1 (lista de categorías no vacía) sólo se devuelven filas donde la categoría origen o destino esté en la lista.; [RETURN_RESULT] RETURN_RESULT: Cuando @FilterByProductionCenters = 1 sólo se devuelven filas donde el centro de producción origen o destino esté en la lista filtrada.; [RETURN_RESULT] RETURN_RESULT: Cuando @GroupBy = 3 se exige que tanto la estructura organizacional origen como la destino tengan Level >= @Level; las estructuras con nivel mayor se promueven recursivamente a su padre hasta alcanzar @Level.; [RETURN_RESULT] RETURN_RESULT: El Value de cada fila usa la redistribución (cddsdr.Value) cuando existe; si no, usa el detalle base (cddsd.Value) vía ISNULL.; [RETURN_RESULT] RETURN_RESULT: El centro destino se toma de cddsdr.ProductionCenterId si hay redistribución; si no, de cddsd.ProductionCenterId.; [RETURN_RESULT] RETURN_RESULT: SourceInitialDistribution se acumula sumando InitialDistribution de CostEstimationNative del mismo período para el centro de producción origen o destino, según @SecondaryDetailType.; [RETURN_RESULT] RETURN_RESULT: Si ocurre cualquier error en el TRY, se devuelve una única fila con valor ''999'' y el mensaje y línea del error en todos los campos descriptivos.; [RETURN_RESULT] RETURN_RESULT: El SourceCenterType y TargetCenterType se traducen a literal: 1=''Operativo'', 2=''Administrativo'', 3=''Logístico'', resto=''N/A''.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsSecondaryDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Categories <> '''' → Activa @FilterByCategories=1 y carga la lista de categorías a filtrar. else No se filtra por categorías.; si @ProductionCenters <> '''' → Activa @FilterByProductionCenters=1 y carga la lista de centros a filtrar. else No se filtra por centros de producción.; si @TypeReport = 4 → Ejecuta la carga principal de datos de distribución secundaria detallada. else No se obtienen datos (resultado vacío).; si @GroupBy = 3 → Aplica el filtro de Level mínimo y ejecuta el bucle de promoción jerárquica de estructuras organizacionales (origen y destino) hasta alcanzar el nivel solicitado, completando código y nombre del padre. else No realiza promoción jerárquica.; si @SecondaryDetailType = 1 → El reporte se orienta desde el centro origen (Source) hacia el destino (Target). else El reporte se invierte y se orienta desde el destino (Target) hacia el origen (Source).; si @GroupBy = 1 / 2 / 3 → Agrupa el resultado por Centro de Producción / Categoría / Estructura Organizacional respectivamente.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsSecondaryDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsSecondaryDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostProductionCenter; Cost.CostOrganizationalStructureOfCosts; Cost.CostDistributionSecondary; Cost.CostDirectDistributionSecondary; Cost.CostDirectDistributionSecondaryDetail; Cost.CostDirectDistributionSecondaryDetailRedistribution; Cost.CostProductionCenterCategory; Cost.CostEstimationNative', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsSecondaryDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsSecondaryDetailed';
-- GO
