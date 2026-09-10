-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-24
-- Description:	Procedimiento para el reporte comparativo de ventas
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReportComparativeSales]
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
		AccountId INT,
		AccountNumber VARCHAR(50),
		AccountName VARCHAR(200),
		ThirdPartyId INT,
		ThirdPartyNit VARCHAR(20),
		ThirdPartyName VARCHAR(500),
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
		AccountId INT,
		AccountNumber VARCHAR(50),
		AccountName VARCHAR(200),
		ThirdPartyId INT,
		ThirdPartyNit VARCHAR(20),
		ThirdPartyName VARCHAR(500),
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

		IF @TypeReport = 6
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
					glb.AccountId,
					glb.AccountNumber,
					glb.AccountName,
					tp.Id ThirdPartyId,
					tp.Nit ThirdPartyNit,
					tp.Name ThirdPartyName,
					SUM(glb.Value) Value,
					glb.isInitialRange,
					0 IsDistribuited
				FROM Cost.CostProductionCenter cpc WITH (NOLOCK)
				JOIN @Table_CenterType ct ON cpc.CenterType = ct.CenterType
				JOIN Cost.CostOrganizationalStructureOfCosts cosc WITH (NOLOCK) ON cpc.OrganizationalStructureOfCostId = cosc.Id
				JOIN Cost.CostProductionCenterHomologation cpch WITH (NOLOCK) ON cpc.Id = cpch.ProductionCenterId
				JOIN Cost.CostProductionCenterCostCenter cpccc ON cpch.ProductionCenterId = cpccc.ProductionCenterId
				JOIN
				(
					SELECT	ma.Id AccountId, 
							ma.Number AccountNumber,
							ma.Name AccountName,
							glb.IdThirdParty, 
							glb.IdCostCenter,
							1 isInitialRange,
							SUM(IIF(mac.Nature = 1, (glb.DebitValue - glb.CreditValue), (glb.CreditValue - glb.DebitValue))) Value
					FROM GeneralLedger.GeneralLedgerBalance glb WITH (NOLOCK)
					JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON glb.IdMainAccount = ma.Id
					JOIN GeneralLedger.MainAccountClasses mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id
					WHERE 
					(
						(glb.Year > @InitialRangeYearStart OR (glb.Year = @InitialRangeYearStart AND glb.Month >= @InitialRangeMonthStart))
						AND
						(glb.Year < @InitialRangeYearEnd OR (glb.Year = @InitialRangeYearEnd AND glb.Month <= @InitialRangeMonthEnd))
					)
					GROUP BY ma.Id, ma.Number, ma.Name, glb.IdThirdParty, glb.IdCostCenter
				UNION ALL
					SELECT	ma.Id AccountId, 
							ma.Number AccountNumber,
							ma.Name AccountName,
							glb.IdThirdParty, 
							glb.IdCostCenter,
							0 isInitialRange,
							SUM(IIF(mac.Nature = 1, (glb.DebitValue - glb.CreditValue), (glb.CreditValue - glb.DebitValue))) Value
					FROM GeneralLedger.GeneralLedgerBalance glb WITH (NOLOCK)
					JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON glb.IdMainAccount = ma.Id
					JOIN GeneralLedger.MainAccountClasses mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id
					WHERE 
					(
						(glb.Year > @FinalRangeYearStart OR (glb.Year = @FinalRangeYearStart AND glb.Month >= @FinalRangeMonthStart))
						AND
						(glb.Year < @FinalRangeYearEnd OR (glb.Year = @FinalRangeYearEnd AND glb.Month <= @FinalRangeMonthEnd))
					)
					GROUP BY ma.Id, ma.Number, ma.Name, glb.IdThirdParty, glb.IdCostCenter
				) glb ON cpch.AccountOriginId = glb.AccountId AND cpccc.CostCenterId = glb.IdCostCenter
				LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON glb.IdThirdParty = tp.Id
				LEFT JOIN Cost.CostProductionCenterCategory cpcc WITH (NOLOCK) ON cpc.CategoryId = cpcc.Id
				LEFT JOIN @Table_Categories c ON cpcc.Id = c.Id
				LEFT JOIN @Table_ProductionCenters pc ON cpc.Id = pc.Id
				WHERE cpch.HomologationType = 6
					AND (@FilterByCategories = 0 OR c.Id IS NOT NULL)
					AND (@FilterByProductionCenters = 0 OR pc.Id IS NOT NULL)
					AND (@GroupBy <> 3 OR cosc.Level >= @Level)
				GROUP BY	cosc.ParentId, cosc.Code, cosc.Name, cosc.Level,
							cpcc.Code, cpcc.Name, 
							cpc.Id, cpc.Code, cpc.Name, cpc.CenterType,
							glb.AccountId, glb.AccountNumber, glb.AccountName,
							tp.Id, tp.Nit, tp.Name,
							cpch.HomologationType, glb.isInitialRange
				OPTION (RECOMPILE)
			
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
		ISNULL(ri.AccountNumber, rf.AccountNumber) AccountNumber, ISNULL(ri.AccountName, rf.AccountName) AccountName,
		ISNULL(ri.ThirdPartyNit, rf.ThirdPartyNit) ThirdPartyNit, ISNULL(ri.ThirdPartyName, rf.ThirdPartyName) ThirdPartyName,
		SUM(ISNULL(ri.Value, 0)) InitialRangeValue,
		SUM(ISNULL(rf.Value, 0)) FinalRangeValue
	FROM 
	(
		SELECT	OrganizationalStructureParentCode, OrganizationalStructureParentName,
				OrganizationalStructureCode, OrganizationalStructureName,					
				CategoryCode, CategoryName,
				ProductionCenterId, ProductionCenterCode, ProductionCenterName, CenterType,
				AccountId, AccountNumber, AccountName,
				ThirdPartyId, ThirdPartyNit, ThirdPartyName,
				SUM(Value) Value			
		FROM @Table_Result
		WHERE IsInitialRange = 1
		GROUP BY	OrganizationalStructureParentCode, OrganizationalStructureParentName,
					OrganizationalStructureCode, OrganizationalStructureName,					
					CategoryCode, CategoryName,
					ProductionCenterId, ProductionCenterCode, ProductionCenterName, CenterType,
					AccountId, AccountNumber, AccountName,
					ThirdPartyId, ThirdPartyNit, ThirdPartyName
	) ri
	FULL JOIN 
	(
		SELECT	OrganizationalStructureParentCode, OrganizationalStructureParentName,
				OrganizationalStructureCode, OrganizationalStructureName,					
				CategoryCode, CategoryName,
				ProductionCenterId, ProductionCenterCode, ProductionCenterName, CenterType,
				AccountId, AccountNumber, AccountName,
				ThirdPartyId, ThirdPartyNit, ThirdPartyName,
				SUM(Value) Value			
		FROM @Table_Result
		WHERE IsInitialRange = 0
		GROUP BY	OrganizationalStructureParentCode, OrganizationalStructureParentName,
					OrganizationalStructureCode, OrganizationalStructureName,					
					CategoryCode, CategoryName,
					ProductionCenterId, ProductionCenterCode, ProductionCenterName, CenterType,
					AccountId, AccountNumber, AccountName,
					ThirdPartyId, ThirdPartyNit, ThirdPartyName
	) rf 
		ON ri.ProductionCenterId = rf.ProductionCenterId 
			AND ISNULL(ri.AccountId, 0) = ISNULL(rf.AccountId, 0)
			AND ISNULL(ri.ThirdPartyId, 0) = ISNULL(rf.ThirdPartyId, 0)			
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
		ISNULL(ri.AccountNumber, rf.AccountNumber), ISNULL(ri.AccountName, rf.AccountName),
		ISNULL(ri.ThirdPartyNit, rf.ThirdPartyNit), ISNULL(ri.ThirdPartyName, rf.ThirdPartyName)
	HAVING SUM(ISNULL(ri.Value, 0)) <> 0 OR SUM(ISNULL(rf.Value, 0)) <> 0
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte comparativo de ventas por centros de producción, permitiendo contrastar los valores contables (ingresos/ventas) entre dos rangos de tiempo (período inicial y período final). Cruza los saldos del libro mayor (GeneralLedger) con la estructura organizacional de costos, los centros de producción y su homologación contable, para calcular y comparar el comportamiento de las ventas según el tipo de centro, categoría o centro de producción seleccionado. Soporta múltiples niveles de agrupación y tipos de reporte, siendo utilizado en el módulo de costos para el análisis gerencial de variaciones de ingresos entre períodos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportComparativeSales';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportComparativeSales';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte comparativo de ventas entre dos rangos de períodos (inicial y final) por centro de producción, categoría o estructura organizacional, agrupando saldos contables homologados.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeSales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@xmlCriterias debe contener los rangos de año/mes inicial y final (InitialRangeYearStart/MonthStart/YearEnd/MonthEnd y FinalRangeYearStart/MonthStart/YearEnd/MonthEnd).; @xmlFilters debe contener TypeReport, GroupBy, Level, CenterType, Categories y ProductionCenters.; CenterType es obligatorio (se inserta siempre); Categories y ProductionCenters son opcionales (cadena vacía desactiva el filtro).; Deben existir homologaciones en Cost.CostProductionCenterHomologation con HomologationType = 6 para obtener datos.; El reporte sólo se ejecuta cuando @TypeReport = 6.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeSales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se consideran homologaciones con HomologationType = 6.; El valor contable se calcula respetando la naturaleza de la cuenta (débito o crédito).; Si @FilterByCategories=0 o @FilterByProductionCenters=0, no se restringe por esos filtros.; El resultado final excluye filas donde tanto el valor del rango inicial como el final son cero.; Los rangos de período se evalúan por (Year, Month) inclusive en ambos extremos.; Errores en tiempo de ejecución no propagan excepción: se devuelven como una fila con código ''999''.; El comparativo siempre presenta dos columnas: InitialRangeValue y FinalRangeValue, emparejadas por ProductionCenterId, AccountId y ThirdPartyId.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeSales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Centro de costo; Estructura organizacional de costos; Homologación contable; Categoría de centro de producción; Saldo del libro mayor (GeneralLedger); Plan de cuentas (cuenta contable y naturaleza); Tercero (NIT); Tipo de centro (Operativo/Administrativo/Logístico); Comparativo de ventas entre rangos de períodos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeSales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Table_Result: Cuando @TypeReport = 6, inserta los saldos sumarizados de GeneralLedger.GeneralLedgerBalance para los rangos inicial (IsInitialRange=1) y final (IsInitialRange=0) cruzados con centros de producción homologados (HomologationType=6) y filtrados por CenterType, Categorías y Centros de producción si aplican.; [UPDATE] @Table_Result: Cuando @GroupBy = 3, recorre iterativamente la jerarquía de Cost.CostOrganizationalStructureOfCosts subiendo de nivel hasta @Level, actualizando ParentId/Code/Name/Level para consolidar al nivel solicitado.; [UPDATE] @Table_Result: Cuando @GroupBy = 3, al finalizar el ascenso jerárquico, asigna OrganizationalStructureParentCode/Name a partir del padre en Cost.CostOrganizationalStructureOfCosts.; [INSERT] @Table_Result: En caso de error (CATCH), inserta un registro con códigos ''999'' y el mensaje y línea de error en los campos descriptivos.; [RETURN_RESULT] RESULTSET: Devuelve un resultset con FULL JOIN entre los valores del rango inicial y final agrupados según @GroupBy (1=ProductionCenter, 2=Category, 3=OrganizationalStructure), excluyendo filas donde ambos valores son cero.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeSales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Categories <> '''' → Activa @FilterByCategories=1 y carga la lista de categorías a filtrar. else No se aplica filtro por categorías.; si @ProductionCenters <> '''' → Activa @FilterByProductionCenters=1 y carga la lista de centros de producción a filtrar. else No se aplica filtro por centros de producción.; si @TypeReport = 6 → Ejecuta la carga principal de saldos homologados con HomologationType=6. else No se cargan datos en @Table_Result.; si @GroupBy = 3 → Aplica restricción cosc.Level >= @Level y ejecuta el bucle de ascenso jerárquico hasta consolidar al nivel @Level. else No se realiza consolidación jerárquica.; si Naturaleza de la cuenta (mac.Nature) = 1 → Calcula Value como (DebitValue - CreditValue). else Calcula Value como (CreditValue - DebitValue).; si @GroupBy IN (1,2,3) → Selecciona Code/Name del resultado según ProductionCenter (1), Category (2) u OrganizationalStructure (3).; si CenterType IN (1,2,3) → Etiqueta como ''Operativo'' (1), ''Administrativo'' (2) o ''Logístico'' (3). else Etiqueta como ''N/A''.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeSales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeSales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostProductionCenter; Cost.CostOrganizationalStructureOfCosts; Cost.CostProductionCenterHomologation; Cost.CostProductionCenterCostCenter; Cost.CostProductionCenterCategory; GeneralLedger.GeneralLedgerBalance; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeSales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeSales';
-- GO
