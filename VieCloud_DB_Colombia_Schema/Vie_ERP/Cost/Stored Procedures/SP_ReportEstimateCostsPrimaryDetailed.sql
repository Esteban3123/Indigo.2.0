-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-27
-- Description:	Procedimiento para el reporte de estimacion de costos tipo primario detallado
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReportEstimateCostsPrimaryDetailed]
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
			@DetailType VARCHAR(MAX),
			-------------
			@FilterByCategories BIT = 0,
			@FilterByProductionCenters BIT = 0

	DECLARE @Table_CenterType AS TABLE(CenterType INT)
	DECLARE @Table_Categories AS TABLE(Id INT)
	DECLARE @Table_ProductionCenters AS TABLE(Id INT)
	DECLARE @Table_DetailType AS TABLE(DetailType INT)

	DECLARE @Table_Result AS TABLE
	(
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
		AccountId INT,
		AccountNumber VARCHAR(50),
		AccountName VARCHAR(200),
		ThirdPartyId INT,
		ThirdPartyNit VARCHAR(20),
		ThirdPartyName VARCHAR(500),
		HomologationType TINYINT,
		Value DECIMAL(20,4)
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
				@Level = t.x.value('Level[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		--Se obtienen los datos de los filtros
		SELECT	@CenterType = t.x.value('CenterType[1]','varchar(max)'),
				@Categories = t.x.value('Categories[1]','varchar(max)'),
				@ProductionCenters = t.x.value('ProductionCenters[1]','varchar(max)')			,
				@DetailType = t.x.value('DetailType[1]','varchar(max)')
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

		INSERT INTO @Table_DetailType
			SELECT CAST(Data AS INT) Data 
			FROM dbo.Split(@DetailType, ',')

		/********************************** OBTENCION DE DATOS **********************************/

		IF @TypeReport = 2
		BEGIN
			INSERT INTO @Table_Result
				SELECT	cosc.ParentId OrganizationalStructureParentId, NULL, NULL,
						cosc.Code OrganizationalStructureCode, 
						cosc.Name OrganizationalStructureName,
						cosc.Level OrganizationalStructureLevel,
						cpcc.Code CategoryCode,
						cpcc.Name CategoryName,
						cpc.Id ProductionCenterId,
						cpc.Code ProductionCenterCode,
						cpc.Name ProductionCenterName,
						cpc.CenterType,
						ma.Id AccountId,
						ma.Number AccountNumber,
						ma.Name AccountName,
						tp.Id ThirdPartyId,
						tp.Nit ThirdPartyNit,
						tp.Name ThirdPartyName,
						cpch.HomologationType,
						SUM(IIF(mac.Nature = 1, (glb.DebitValue - glb.CreditValue), (glb.CreditValue - glb.DebitValue))) Value
				FROM Cost.CostProductionCenter cpc WITH (NOLOCK)
				JOIN @Table_CenterType ct ON cpc.CenterType = ct.CenterType
				JOIN Cost.CostOrganizationalStructureOfCosts cosc WITH (NOLOCK) ON cpc.OrganizationalStructureOfCostId = cosc.Id
				JOIN Cost.CostProductionCenterHomologation cpch WITH (NOLOCK) ON cpc.Id = cpch.ProductionCenterId
				JOIN @Table_DetailType dt ON cpch.HomologationType = dt.DetailType
				JOIN Cost.CostProductionCenterCostCenter cpccc ON cpch.ProductionCenterId = cpccc.ProductionCenterId
				JOIN GeneralLedger.GeneralLedgerBalance glb WITH (NOLOCK) ON cpch.AccountOriginId = glb.IdMainAccount AND cpccc.CostCenterId = glb.IdCostCenter				
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON glb.IdMainAccount = ma.Id
				JOIN GeneralLedger.MainAccountClasses mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id
				LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON glb.IdThirdParty = tp.Id
				LEFT JOIN Cost.CostProductionCenterCategory cpcc WITH (NOLOCK) ON cpc.CategoryId = cpcc.Id
				LEFT JOIN @Table_Categories c ON cpcc.Id = c.Id
				LEFT JOIN @Table_ProductionCenters pc ON cpc.Id = pc.Id
				WHERE (
						(glb.Year > @YearStart OR (glb.Year = @YearStart AND glb.Month >= @MonthStart))
						AND
						(glb.Year < @YearEnd OR (glb.Year = @YearEnd AND glb.Month <= @MonthEnd))
					)
					AND (@FilterByCategories = 0 OR c.Id IS NOT NULL)
					AND (@FilterByProductionCenters = 0 OR pc.Id IS NOT NULL)
					AND (@GroupBy <> 3 OR cosc.Level >= @Level)
				GROUP BY	cosc.ParentId, cosc.Code, cosc.Name, cosc.Level,
							cpcc.Code, cpcc.Name, 
							cpc.Id, cpc.Code, cpc.Name, cpc.CenterType,
							ma.Id, ma.Number, ma.Name,
							tp.Id, tp.Nit, tp.Name,
							cpch.HomologationType

			/****************************************** ASIGNACIÓN ESTRUCTURA ORGANIZACIONAL ******************************************/

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
		r.AccountNumber, r.AccountName,
		r.ThirdPartyNit, r.ThirdPartyName,
		CASE r.HomologationType
			WHEN 4 THEN 1
			WHEN 1 THEN 2
			WHEN 5 THEN 3
			WHEN 2 THEN 4
			WHEN 3 THEN 5
			WHEN 6 THEN 6
			ELSE 0
		END HomologationType,
		CASE r.HomologationType
			WHEN 1 THEN 'Mano de Obra'
			WHEN 2 THEN 'Dispensación'
			WHEN 3 THEN 'Consumo'
			WHEN 4 THEN 'Gastos Generales'
			WHEN 5 THEN 'Activos Fijos'
			WHEN 6 THEN 'Ventas'
			ELSE 'N/A'
		END HomologationTypeName,
		SUM(r.Value) Value
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
		r.CenterType,
		r.AccountNumber, r.AccountName,
		r.ThirdPartyNit, r.ThirdPartyName,
		r.HomologationType
	HAVING SUM(r.Value) <> 0
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte detallado de estimación de costos primarios por centro de producción, desglosando los valores contables (débitos y créditos) registrados en el libro mayor para un rango de períodos (año/mes de inicio y año/mes de fin). Cruza la estructura organizacional de costos, los centros de producción, sus cuentas contables homologadas (tipos de detalle/homologación) y los centros de costo asociados, permitiendo filtrar por tipo de centro, categorías de centros de producción y centros de producción específicos. Incorpora información de terceros (proveedores, aseguradoras u otras entidades externas) vinculados a los movimientos contables, y soporta diferentes modos de agrupación y niveles jerárquicos de la estructura organizacional. Se utiliza en el módulo de costos para analizar la distribución primaria de gastos por unidad productiva, cuenta contable y tercero, facilitando el control y seguimiento del costo hospitalario o empresarial.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportEstimateCostsPrimaryDetailed';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportEstimateCostsPrimaryDetailed';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte detallado de estimación de costos primarios, agrupando saldos contables por centro de producción, categoría o estructura organizacional según criterios y filtros recibidos por XML.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsPrimaryDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los XML de criterios y filtros deben tener el nodo /Data con los elementos esperados (YearStart, MonthStart, YearEnd, MonthEnd, TypeReport, GroupBy, Level / CenterType, Categories, ProductionCenters, DetailType).; CenterType y DetailType deben venir como listas separadas por comas y convertibles a INT.; El rango (YearStart,MonthStart) debe ser anterior o igual a (YearEnd,MonthEnd) para retornar datos.; Debe existir homologación de centros de producción y mapeo a cuentas contables en CostProductionCenterHomologation.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsPrimaryDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El reporte solo retorna información cuando TypeReport=2.; Los valores siempre se calculan respetando la naturaleza contable (débito/crédito) de la clase de cuenta.; Solo se incluyen centros de producción cuyo CenterType esté en la lista filtrada.; Solo se incluyen homologaciones cuyo HomologationType esté en la lista DetailType.; Las filas con suma neta cero se omiten del resultado final.; Los errores nunca interrumpen la ejecución: se capturan y materializan como una fila marcadora con código ''999''.; Cuando se agrupa por estructura organizacional, las filas se consolidan al nivel jerárquico solicitado (@Level) o al máximo nivel disponible.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsPrimaryDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado tabular (RETURN_RESULT): Solo procesa y retorna datos cuando @TypeReport = 2; en otros valores retorna conjunto vacío.; [RETURN_RESULT] Resultado tabular (RETURN_RESULT): El valor contable se calcula como (Debit-Credit) si la naturaleza de la clase de cuenta es 1, en caso contrario (Credit-Debit).; [RETURN_RESULT] Resultado tabular (RETURN_RESULT): Se filtran saldos por rango año/mes: (Year>YearStart OR (Year=YearStart AND Month>=MonthStart)) AND (Year<YearEnd OR (Year=YearEnd AND Month<=MonthEnd)).; [RETURN_RESULT] Resultado tabular (RETURN_RESULT): Si Categories no está vacío, solo se incluyen registros cuya categoría esté en la lista; igual lógica para ProductionCenters.; [RETURN_RESULT] Resultado tabular (RETURN_RESULT): Cuando GroupBy=3 se filtran filas con cosc.Level >= @Level y se asciende iterativamente la jerarquía organizacional hasta alcanzar el nivel solicitado, reasignando ParentId/Code/Name/Level.; [RETURN_RESULT] Resultado tabular (RETURN_RESULT): El campo Code/Name del resultado final se elige según GroupBy: 1=ProductionCenter, 2=Category, 3=OrganizationalStructure.; [RETURN_RESULT] Resultado tabular (RETURN_RESULT): CenterType se traduce a nombre: 1=''Operativo'', 2=''Administrativo'', 3=''Logístico'', otro=''N/A''.; [RETURN_RESULT] Resultado tabular (RETURN_RESULT): HomologationType se reordena: 4→1, 1→2, 5→3, 2→4, 3→5, 6→6 y se etiqueta como ''Mano de Obra'',''Dispensación'',''Consumo'',''Gastos Generales'',''Activos Fijos'',''Ventas''.; [RETURN_RESULT] Resultado tabular (RETURN_RESULT): Se descartan filas cuya suma de Value sea 0 (HAVING SUM(r.Value) <> 0).; [INSERT] @Table_Result: Ante cualquier error en TRY, se inserta una fila marcadora con códigos ''999'' y el ERROR_MESSAGE+línea como nombres en ProductionCenter, Category y OrganizationalStructure.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsPrimaryDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Categories <> '''' → Activa @FilterByCategories=1 y carga la lista para restringir resultados a esas categorías. else No se aplica filtro por categoría.; si @ProductionCenters <> '''' → Activa @FilterByProductionCenters=1 y carga la lista para restringir resultados a esos centros de producción. else No se aplica filtro por centros de producción.; si @TypeReport = 2 → Ejecuta la consulta principal de costos primarios detallados. else No se ejecuta la obtención de datos; se devuelve resultado vacío.; si @GroupBy = 3 → Aplica filtro cosc.Level >= @Level y ejecuta el ciclo de ascenso jerárquico para consolidar a nivel deseado de la estructura organizacional. else No se hace el ascenso jerárquico ni filtrado por nivel.; si mac.Nature = 1 → El valor se calcula como Débito - Crédito. else El valor se calcula como Crédito - Débito.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsPrimaryDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsPrimaryDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostProductionCenter; Cost.CostOrganizationalStructureOfCosts; Cost.CostProductionCenterHomologation; Cost.CostProductionCenterCostCenter; GeneralLedger.GeneralLedgerBalance; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; Common.ThirdParty; Cost.CostProductionCenterCategory', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsPrimaryDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsPrimaryDetailed';
-- GO
