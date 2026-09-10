-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-27
-- Description:	Procedimiento para el reporte de estimacion de costos tipo primario resumido
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReportEstimateCostsPrimarySummary]
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
		DirectCostDistribution DECIMAL(20,4),
		AutoCostDistribution DECIMAL(20,4),
		ManPowerDistributionDirect DECIMAL(20,4), 
		ManPowerDistributionInDirect DECIMAL(20,4),
		FixedAssetDistribution DECIMAL(20,4),
		DispensingDistribution DECIMAL(20,4),
		TransferDistribution DECIMAL(20,4),
		InitialDistribution DECIMAL(20,4),
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

		IF @TypeReport = 1
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
					cen.DirectCostDistribution,
					cen.AutoCostDistribution,
					cen.ManPowerDistributionDirect, 
					cen.ManPowerDistributionInDirect,
					cen.FixedAssetDistribution,
					cen.DispensingDistribution,
					cen.TransferDistribution,
					cen.InitialDistribution,
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
		SUM(r.DirectCostDistribution) DirectCostDistribution,
		SUM(r.AutoCostDistribution) AutoCostDistribution,
		SUM(r.ManPowerDistributionDirect) ManPowerDistributionDirect,
		SUM(r.ManPowerDistributionInDirect) ManPowerDistributionInDirect,
		SUM(r.FixedAssetDistribution) FixedAssetDistribution,
		SUM(r.DispensingDistribution) DispensingDistribution,
		SUM(r.TransferDistribution) TransferDistribution,
		SUM(r.InitialDistribution) InitialDistribution,
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte resumido de estimación de costos primarios por centro de producción para un rango de meses y años. Consulta la estimación nativa de costos (costos directos, mano de obra, activos fijos, dispensación, transferencias y distribución inicial) cruzando con la estructura organizacional de costos, las categorías y los centros de producción. Permite filtrar por tipo de centro, categorías y centros específicos, y agrupa los resultados según un nivel jerárquico de la estructura organizacional de costos definido en los criterios. Se usa en el módulo de contabilidad de costos para analizar y comparar los costos distribuidos entre centros de producción en un período determinado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportEstimateCostsPrimarySummary';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportEstimateCostsPrimarySummary';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte resumido de estimación de costos primarios agrupable por centro de producción, categoría o estructura organizacional, sumarizando distribuciones de costos en un rango de año/mes.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsPrimarySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los XML de criterios y filtros deben tener estructura /Data con los nodos esperados (YearStart, MonthStart, YearEnd, MonthEnd, TypeReport, GroupBy, Level, CenterType, Categories, ProductionCenters); CenterType debe ser una lista no vacía de enteros separados por coma; Categories y ProductionCenters pueden ser cadena vacía (sin filtro) o lista de enteros separados por coma; Si GroupBy=3 se requiere un Level válido para recorrer la jerarquía organizacional', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsPrimarySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan registros cuyo (Year,Month) esté dentro del rango [YearStart/MonthStart, YearEnd/MonthEnd]; Solo se incluyen centros de producción cuyo CenterType esté en la lista provista (INNER JOIN obligatorio con @Table_CenterType); Los filtros por categoría y centro de producción solo aplican si su flag respectivo está activo; Cualquier error en el flujo principal se transforma en una fila informativa con código ''999'' en lugar de propagar la excepción; Las métricas devueltas son siempre sumas agregadas por la combinación de padre, código/nombre según GroupBy y CenterType', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsPrimarySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estimación de costos; Centro de producción/costo; Categoría de centro de producción; Estructura organizacional de costos; Distribución de costo directo; Distribución de mano de obra directa e indirecta; Distribución de activo fijo; Distribución de dispensación; Distribución de transferencia; Distribución inicial; Ventas totales; Tipo de centro (Operativo/Administrativo/Logístico)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsPrimarySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Table_Result: Cuando TypeReport=1, inserta filas combinando CostEstimationNative con centros de producción filtrados por CenterType, dentro del rango (Year,Month) entre inicio y fin, y aplicando filtros opcionales de Categorías y Centros de Producción; [UPDATE] @Table_Result: Cuando GroupBy=3, sube iterativamente la jerarquía actualizando ParentId/Code/Name/Level hasta alcanzar el Level objetivo, agrupando registros al nivel jerárquico solicitado; [UPDATE] @Table_Result: Cuando GroupBy=3, al finalizar la subida jerárquica, completa el código y nombre del padre (ParentCode/ParentName) consultando la estructura organizacional; [INSERT] @Table_Result: En caso de error capturado por CATCH, inserta una fila con código ''999'' y el mensaje y línea del error en los campos descriptivos; [RETURN_RESULT] RESULT_SET: Devuelve el resultado agregado (SUM de distribuciones y ventas) agrupado por padre, código/nombre según GroupBy y CenterType, traduciendo CenterType a ''Operativo''/''Administrativo''/''Logístico''/''N/A''', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsPrimarySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Categories <> '''' → Activa FilterByCategories=1 y carga la lista de categorías a filtrar else No se aplica filtro por categorías; si @ProductionCenters <> '''' → Activa FilterByProductionCenters=1 y carga la lista de centros de producción a filtrar else No se aplica filtro por centros de producción; si @TypeReport = 1 → Ejecuta la carga de datos desde CostEstimationNative hacia la tabla resultado else No se cargan datos (la tabla resultado queda vacía); si @GroupBy = 3 → Limita los registros a OrganizationalStructureLevel >= @Level y luego escala la jerarquía organizacional hasta el nivel objetivo else No se realiza recorrido jerárquico; si @GroupBy IN (1,2,3) en SELECT final → Selecciona Code/Name según ProductionCenter (1), Categoría (2) o Estructura Organizacional (3) para el agrupamiento de salida; si CenterType en SELECT final → Traduce 1→Operativo, 2→Administrativo, 3→Logístico, otro→N/A', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsPrimarySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsPrimarySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostEstimationNative; Cost.CostProductionCenter; Cost.CostOrganizationalStructureOfCosts; Cost.CostProductionCenterCategory', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsPrimarySummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsPrimarySummary';
-- GO
