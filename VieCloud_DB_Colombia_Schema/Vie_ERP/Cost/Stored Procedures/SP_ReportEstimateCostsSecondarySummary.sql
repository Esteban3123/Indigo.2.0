-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-29
-- Description:	Procedimiento para el reporte de estimacion de costos tipo secundario resumido
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReportEstimateCostsSecondarySummary]
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
		OrganizationalStructureParentId INT,
		OrganizationalStructureParentCode VARCHAR(20), 
		OrganizationalStructureParentName VARCHAR(500),
		OrganizationalStructureCode VARCHAR(20), 
		OrganizationalStructureName VARCHAR(500),
		OrganizationalStructureLevel INT,
		CategoryCode VARCHAR(20),
		CategoryName VARCHAR(500),
		ProductionCenterCode VARCHAR(20),
		ProductionCenterName VARCHAR(500),
		CenterType TINYINT,
		InitialDistribution DECIMAL(20,4),
		SecondaryDirectCostDistribution DECIMAL(20,4),
		SecondaryAutoCostDistribution DECIMAL(20,4),
		SecondaryManPowerDistributionDirect DECIMAL(20,4), 
		SecondaryManPowerDistributionInDirect DECIMAL(20,4),
		SecondaryFixedAssetDistribution DECIMAL(20,4),
		SecondaryDispensingDistribution DECIMAL(20,4),
		SecondaryTransferDistribution DECIMAL(20,4),
		SecondaryDistribution DECIMAL(20,4),
		TotalSales DECIMAL(20,4)
	)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 
			@YearStart = t.x.value('YearStart[1]','int'),
			@MonthStart = t.x.value('MonthStart[1]','int'),
			@YearEnd = t.x.value('YearEnd[1]','int'),
			@MonthEnd = t.x.value('MonthEnd[1]','int'),
			@TypeReport = t.x.value('TypeReport[1]','int'),
			@GroupBy = t.x.value('GroupBy[1]','int'),
			@Level = t.x.value('Level[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		--Se obtienen los datos de los filtros
		SELECT 
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

		IF @TypeReport = 3
		BEGIN
			INSERT INTO @Table_Result
				SELECT
					cosc.ParentId OrganizationalStructureParentId, NULL, NULL,
					cosc.Code OrganizationalStructureCode, 
					cosc.Name OrganizationalStructureName,
					cosc.Level OrganizationalStructureLevel,
					cpcc.Code CategoryCode,
					cpcc.Name CategoryName,
					cpc.Code ProductionCenterCode,
					cpc.Name ProductionCenterName,
					cpc.CenterType,
					cen.InitialDistribution,
					cen.SecondaryDirectCostDistribution,
					cen.SecondaryAutoCostDistribution,
					cen.SecondaryManPowerDistributionDirect, 
					cen.SecondaryManPowerDistributionInDirect,
					cen.SecondaryFixedAssetDistribution,
					cen.SecondaryDispensingDistribution,
					cen.SecondaryTransferDistribution,
					cen.SecondaryDistribution,
					cen.TotalSales
				FROM Cost.CostEstimationNative cen WITH (NOLOCK)
				JOIN Cost.CostProductionCenter cpc WITH (NOLOCK) ON cen.ProductionCenterId = cpc.Id
				JOIN @Table_CenterType ct ON cpc.CenterType = ct.CenterType
				JOIN Cost.CostOrganizationalStructureOfCosts cosc WITH (NOLOCK) ON cpc.OrganizationalStructureOfCostId = cosc.Id
				LEFT JOIN Cost.CostProductionCenterCategory cpcc WITH (NOLOCK) ON cpc.CategoryId = cpcc.Id
				LEFT JOIN @Table_Categories c ON cpcc.Id = c.Id
				LEFT JOIN @Table_ProductionCenters pc ON cpc.Id = pc.Id
				WHERE (
						(cen.Year > @YearStart OR (cen.Year = @YearStart AND cen.Month >= @MonthStart))
						AND
						(cen.Year < @YearEnd OR (cen.Year = @YearEnd AND cen.Month <= @MonthEnd))
					)
					AND (@FilterByCategories = 0 OR c.Id IS NOT NULL)
					AND (@FilterByProductionCenters = 0 OR pc.Id IS NOT NULL)
					AND (@GroupBy <> 3 OR cosc.Level >= @Level)
				OPTION (RECOMPILE)

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
		r.OrganizationalStructureParentCode ParentCode, 
		r.OrganizationalStructureParentName ParentName,
		CASE @GroupBy
			WHEN 1 THEN r.ProductionCenterCode
			WHEN 2 THEN r.CategoryCode
			WHEN 3 THEN r.OrganizationalStructureCode
		END Code, 
		CASE @GroupBy
			WHEN 1 THEN r.ProductionCenterName
			WHEN 2 THEN r.CategoryName
			WHEN 3 THEN r.OrganizationalStructureName
		END Name,
		r.CenterType,
		CASE r.CenterType
			WHEN 1 THEN 'Operativo'
			WHEN 2 THEN 'Administrativo'
			WHEN 3 THEN 'Logístico'
			ELSE 'N/A'
		END CenterTypeName,
		SUM(r.InitialDistribution) InitialDistribution,
		SUM(r.SecondaryDirectCostDistribution) SecondaryDirectCostDistribution,
		SUM(r.SecondaryAutoCostDistribution) SecondaryAutoCostDistribution,
		SUM(r.SecondaryManPowerDistributionDirect) SecondaryManPowerDistributionDirect,
		SUM(r.SecondaryManPowerDistributionInDirect) SecondaryManPowerDistributionInDirect,
		SUM(r.SecondaryFixedAssetDistribution) SecondaryFixedAssetDistribution,
		SUM(r.SecondaryDispensingDistribution) SecondaryDispensingDistribution,
		SUM(r.SecondaryTransferDistribution) SecondaryTransferDistribution,
		SUM(r.SecondaryDistribution) SecondaryDistribution,
		SUM(r.TotalSales) TotalSales
	FROM @Table_Result r
	GROUP BY
		r.OrganizationalStructureParentCode, r.OrganizationalStructureParentName,
		CASE @GroupBy
			WHEN 1 THEN r.ProductionCenterCode
			WHEN 2 THEN r.CategoryCode
			WHEN 3 THEN r.OrganizationalStructureCode
		END, 
		CASE @GroupBy
			WHEN 1 THEN r.ProductionCenterName
			WHEN 2 THEN r.CategoryName
			WHEN 3 THEN r.OrganizationalStructureName
		END,
		r.CenterType
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte resumido de estimación de costos en su etapa secundaria, es decir, luego de que los costos han sido redistribuidos desde los centros de producción auxiliares hacia los centros productivos finales. Recibe criterios de período (año/mes inicio y fin), tipo de reporte, nivel de agrupación y filtros por tipo de centro, categoría y centro de producción en formato XML. Consulta la tabla de estimación nativa de costos (CostEstimationNative) cruzándola con la estructura organizacional de costos, las categorías y los centros de producción, para consolidar en un resultado resumido los valores de distribución secundaria: costos directos, mano de obra directa e indirecta, activos fijos, dispensación, transferencias, autocostos y totales de ventas. Soporta agrupación por niveles jerárquicos de la estructura organizacional de costos, permitiendo comparar y analizar el costo secundario por área, categoría o centro de producción para la toma de decisiones de gestión de costos hospitalarios.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportEstimateCostsSecondarySummary';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportEstimateCostsSecondarySummary';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte resumido de estimación de costos secundarios por rango año/mes, agrupado por centro de producción, categoría o estructura organizacional, con escalamiento jerárquico al nivel solicitado.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsSecondarySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los XML de criterios y filtros deben respetar la estructura /Data con los nodos esperados (YearStart, MonthStart, YearEnd, MonthEnd, TypeReport, GroupBy, Level, CenterType, Categories, ProductionCenters).; CenterType debe contener al menos un valor entero separado por comas, ya que actúa como filtro obligatorio (INNER JOIN).; Las cadenas Categories y ProductionCenters, si vienen no vacías, deben ser enteros separados por coma.; La función dbo.Split debe existir y devolver columna ''Data'' convertible a INT.; Debe existir información en Cost.CostEstimationNative para el rango (Year/Month) consultado.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsSecondarySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El filtro por CenterType siempre se aplica (INNER JOIN), incluso si la lista está vacía (en cuyo caso no devolvería filas).; Los filtros por categorías y centros de producción solo se aplican cuando la cadena correspondiente no es vacía.; El rango de fechas se evalúa por (Year, Month) combinados, permitiendo rangos multianuales.; Cuando ocurre un error, el procedimiento nunca falla hacia el cliente: siempre retorna un resultset (con fila de error ''999'' si aplica).; Los importes se agregan siempre con SUM en el resultado final.; El escalamiento jerárquico solo ocurre cuando se agrupa por estructura organizacional (@GroupBy=3).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsSecondarySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estimación de costos; Distribución secundaria de costos; Centro de producción / centro de costo; Categoría de centro de producción; Estructura organizacional de costos; Tipo de centro (Operativo, Administrativo, Logístico); Mano de obra directa e indirecta; Activo fijo; Dispensación; Transferencia; Ventas totales', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsSecondarySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] TABLE @Table_Result: Cuando @TypeReport = 3, inserta filas de costos estimados uniendo CostEstimationNative con CostProductionCenter, CostOrganizationalStructureOfCosts y CostProductionCenterCategory, dentro del rango año/mes y aplicando los filtros opcionales por categorías, centros de producción y nivel organizacional.; [UPDATE] TABLE @Table_Result: Cuando @GroupBy = 3, mientras existan filas con OrganizationalStructureLevel mayor a @Level, sube iterativamente la estructura al padre (ParentId, Code, Name, Level) hasta alcanzar el nivel solicitado.; [UPDATE] TABLE @Table_Result: Cuando @GroupBy = 3, al finalizar el escalamiento, asigna OrganizationalStructureParentCode/Name a partir del padre actual en CostOrganizationalStructureOfCosts.; [INSERT] TABLE @Table_Result: Si ocurre cualquier error en el TRY, inserta una fila con códigos ''999'' y ERROR_MESSAGE()+linea como nombres de centro/categoría/estructura.; [RETURN_RESULT] RESULTSET: Devuelve el resultado agregando con SUM las métricas de distribución y ventas, agrupado por padre y por código/nombre dependiendo de @GroupBy (1=ProductionCenter, 2=Category, 3=OrganizationalStructure), incluyendo CenterTypeName traducido.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsSecondarySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Categories <> '''' → Activa @FilterByCategories=1 y carga los IDs de categorías para filtrar.; si @ProductionCenters <> '''' → Activa @FilterByProductionCenters=1 y carga los IDs de centros de producción para filtrar.; si @TypeReport = 3 → Ejecuta la consulta principal de estimación de costos secundarios. else No se carga información en el resultado (se devuelve vacío).; si @GroupBy = 3 → Aplica filtro cosc.Level >= @Level y ejecuta el proceso iterativo de escalamiento jerárquico hasta @Level.; si CenterType IN (1,2,3) en proyección → Traduce a ''Operativo'', ''Administrativo'' o ''Logístico''; cualquier otro valor se traduce como ''N/A''.; si @GroupBy = 1 / 2 / 3 en SELECT final → Define Code/Name como ProductionCenter, Category u OrganizationalStructure respectivamente.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsSecondarySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsSecondarySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostEstimationNative; Cost.CostProductionCenter; Cost.CostOrganizationalStructureOfCosts; Cost.CostProductionCenterCategory', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsSecondarySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsSecondarySummary';
-- GO
