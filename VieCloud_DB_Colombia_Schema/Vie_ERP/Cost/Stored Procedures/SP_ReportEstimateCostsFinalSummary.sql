-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-29
-- Description:	Procedimiento para el reporte de estimacion de costos tipo secundario resumido
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReportEstimateCostsFinalSummary]
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
		SecondaryDistribution DECIMAL(20,4),
		TotalDistribution DECIMAL(20,4),
		TotalSales DECIMAL(20,4),
		TotalSalesSecondary DECIMAL(20,4)
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
					(cen.SecondaryDirectCostDistribution + cen.SecondaryAutoCostDistribution + cen.SecondaryManPowerDistributionDirect + cen.SecondaryManPowerDistributionInDirect + cen.SecondaryFixedAssetDistribution + cen.SecondaryDispensingDistribution + cen.SecondaryTransferDistribution) AS SecondaryDistribution,
					cen.SecondaryDistribution AS TotalDistribution,
					cen.TotalSales,
					cen.TotalSalesSecondary
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
		SUM(r.SecondaryDistribution) SecondaryDistribution,
		SUM(r.TotalDistribution) TotalDistribution,
		SUM(r.TotalSales) TotalSales,
		SUM(r.TotalSalesSecondary) TotalSalesSecondary,
		IIF
		(
			SUM(r.TotalDistribution) = 0, 
			SUM(r.TotalSales), 
			(SUM(r.TotalSales) - SUM(r.TotalDistribution))
		) AbsoluteProfitabilityMargin,
		IIF
		(
			SUM(r.TotalDistribution) = 0, 
			IIF(SUM(r.TotalSales) = 0, 0, 1), 
			(SUM(r.TotalSales) - SUM(r.TotalDistribution)) / SUM(r.TotalDistribution)
		) ProfitabilityMargin
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento para generar el reporte resumido final de estimación de costos por centro de producción en un rango de meses y años. Consolida los valores de costos directos, mano de obra, activos fijos, dispensación, transferencias, distribución intermedia y secundaria tomados de la estimación nativa de costos (CostEstimationNative), cruzados con la estructura organizacional de costos, las categorías y los centros de producción. Permite filtrar por tipo de centro, categorías y centros de producción específicos, y agrupar los resultados por distintos niveles jerárquicos de la estructura organizacional. Se usa para reportería gerencial y contable de cierre de costos, mostrando el costo total distribuido y las ventas por centro en su etapa final confirmada.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportEstimateCostsFinalSummary';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportEstimateCostsFinalSummary';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte resumido de estimación de costos secundaria, agregando distribuciones de costos y ventas por centro de producción, categoría o estructura organizacional, calculando márgenes de rentabilidad.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsFinalSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los XML de criterios y filtros deben tener nodo /Data con los elementos esperados (YearStart, MonthStart, YearEnd, MonthEnd, TypeReport, GroupBy, Level / CenterType, Categories, ProductionCenters).; CenterType debe venir como lista CSV de enteros válidos para parsear con dbo.Split.; Debe existir información en Cost.CostEstimationNative dentro del rango año/mes solicitado.; Para agrupar por estructura organizacional (GroupBy=3) se requiere un Level válido en la jerarquía de Cost.CostOrganizationalStructureOfCosts.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsFinalSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El rango de periodos se evalúa por (Year, Month) combinados, permitiendo rangos multi-año.; SecondaryDistribution se calcula como la suma de los siete componentes secundarios (DirectCost, AutoCost, ManPowerDirect, ManPowerInDirect, FixedAsset, Dispensing, Transfer).; TotalDistribution toma el valor de SecondaryDistribution de la tabla nativa.; Siempre se filtra por los CenterType provistos (INNER JOIN obligatorio con @Table_CenterType).; Los filtros de categorías y centros de producción solo se aplican si la lista respectiva no es vacía.; Nunca se produce división por cero al calcular ProfitabilityMargin gracias al IIF de control.; Ante cualquier error capturado, el procedimiento no propaga la excepción sino que devuelve una fila marcadora con código ''999''.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsFinalSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estimación de costos; Distribución secundaria de costos; Centro de producción; Centro de costo; Categoría de centro de producción; Estructura organizacional de costos; Tipo de centro (Operativo/Administrativo/Logístico); Mano de obra directa e indirecta; Activos fijos; Dispensación; Transferencias; Margen de rentabilidad absoluto y relativo; Ventas totales y ventas secundarias', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsFinalSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT_SET: Devuelve filas agregadas (SUM) de distribuciones y ventas agrupadas según @GroupBy (1=ProductionCenter, 2=Category, 3=OrganizationalStructure) y CenterType, incluyendo márgenes de rentabilidad.; [RETURN_RESULT] RESULT_SET: Cuando SUM(TotalDistribution)=0, AbsoluteProfitabilityMargin se iguala a SUM(TotalSales) y ProfitabilityMargin retorna 0 si TotalSales=0 o 1 en caso contrario, evitando división por cero.; [RETURN_RESULT] RESULT_SET: Si ocurre una excepción, retorna una única fila con código ''999'' y el mensaje de error junto a la línea del error en lugar de los datos.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsFinalSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Categories <> '''' → Activa filtro por categorías (@FilterByCategories=1) y carga la lista en @Table_Categories. else No se filtra por categorías.; si @ProductionCenters <> '''' → Activa filtro por centros de producción (@FilterByProductionCenters=1) y carga la lista en @Table_ProductionCenters. else No se filtra por centros de producción.; si @TypeReport = 5 → Ejecuta la consulta principal que carga @Table_Result con las distribuciones de costos y ventas en el rango año/mes y aplica filtros de categorías, centros y nivel. else No carga datos; el resultado final estará vacío salvo error.; si @GroupBy = 3 → Restringe a registros con cosc.Level >= @Level y ejecuta un bucle que sube en la jerarquía organizacional actualizando código/nombre/nivel hasta alcanzar el nivel objetivo, finalmente asigna ParentCode/ParentName con el padre. else No se realiza el ascenso jerárquico.; si CASE @GroupBy en SELECT final (1/2/3) → Selecciona Code/Name desde ProductionCenter, Category u OrganizationalStructure respectivamente para agrupar la salida.; si CASE r.CenterType → Traduce CenterType 1=''Operativo'', 2=''Administrativo'', 3=''Logístico'', otros=''N/A''.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsFinalSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsFinalSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostEstimationNative; Cost.CostProductionCenter; Cost.CostOrganizationalStructureOfCosts; Cost.CostProductionCenterCategory', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsFinalSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsFinalSummary';
-- GO
