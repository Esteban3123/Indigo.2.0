-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-02-04
-- Description:	Procedimiento para el reporte comparativo de consumo
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReportComparativeTransferDistribution]
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
				WHERE cpch.HomologationType = 3
					AND (@FilterByCategories = 0 OR c.Id IS NOT NULL)
					AND (@FilterByProductionCenters = 0 OR pc.Id IS NOT NULL)
					AND (@GroupBy <> 3 OR cosc.Level >= @Level)
				GROUP BY	cosc.ParentId, cosc.Code, cosc.Name, cosc.Level,
							cpcc.Code, cpcc.Name, 
							cpc.Id, cpc.Code, cpc.Name, cpc.CenterType,
							glb.AccountId, glb.AccountNumber, glb.AccountName,
							tp.Id, tp.Nit, tp.Name,
							cpch.HomologationType, glb.isInitialRange

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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera un reporte comparativo de distribución y transferencia de costos entre centros de producción para dos rangos de períodos contables (inicial y final), permitiendo analizar la evolución del gasto entre períodos. Consulta los saldos del libro mayor (GeneralLedger) por cuenta contable y centro de costo, los cruza con la estructura organizacional de costos y la homologación de centros de producción, y calcula los valores distribuidos y no distribuidos según el tipo de centro (administrativo, auxiliar, final) y los filtros aplicados (categorías, centros de producción, nivel jerárquico). Su propósito es apoyar el control de gestión y la contabilidad de costos hospitalarios, mostrando cuánto costo fue transferido o distribuido en cada período y cómo varía entre rangos de tiempo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportComparativeTransferDistribution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportComparativeTransferDistribution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte comparativo de consumo contable entre dos rangos de períodos (inicial vs final) agregando saldos del libro mayor por centro de producción, categoría o estructura organizacional, según filtros y agrupación seleccionados.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeTransferDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@xmlCriterias debe contener nodo /Data con los rangos de año/mes inicial y final; @xmlFilters debe contener nodo /Data con TypeReport, GroupBy, Level, CenterType, Categories y ProductionCenters; CenterType debe ser una lista CSV no vacía de enteros válidos; Existencia de homologaciones de centros de producción con HomologationType = 3 para que retornen datos; Las cuentas en MainAccounts deben tener una clase con Nature definida (1 = débito) para calcular el valor', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeTransferDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran homologaciones con HomologationType = 3; El valor contable se calcula respetando la naturaleza de la cuenta (débito vs crédito); Los rangos de periodo se evalúan por combinación (Year, Month) inclusiva en ambos extremos; Las filas resultantes con valores cero en ambos rangos se excluyen del reporte (HAVING); El emparejamiento entre rango inicial y final se realiza por ProductionCenterId, AccountId y ThirdPartyId (con FULL JOIN para conservar ambos lados); Si ocurre un error, el procedimiento no propaga la excepción sino que devuelve una fila marcadora con código ''999''', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeTransferDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Centro de costo; Estructura organizacional de costos; Homologación de cuentas; Libro mayor / saldos contables; Naturaleza de cuenta (débito/crédito); Tercero (NIT); Categoría de centro de producción; Tipo de centro (Operativo, Administrativo, Logístico); Comparativo de consumo entre periodos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeTransferDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RETURN_RESULT: Devuelve un único result set comparativo con InitialRangeValue y FinalRangeValue agrupado según @GroupBy (1=ProductionCenter, 2=Category, 3=OrganizationalStructure), filtrando filas donde ambos sumatorios sean 0; [INSERT] @Table_Result: Cuando @TypeReport = 3, inserta los saldos contables agregados de ambos rangos uniendo CostProductionCenter con homologaciones HomologationType=3 y GeneralLedgerBalance; [UPDATE] @Table_Result: Cuando @GroupBy = 3, sube iterativamente la jerarquía de CostOrganizationalStructureOfCosts hasta alcanzar @Level, reasignando ParentId/Code/Name/Level por cada nivel encontrado; [INSERT] @Table_Result: En CATCH, inserta una fila con código ''999'' y el mensaje y línea del error en los campos de ProductionCenter, Category y OrganizationalStructure', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeTransferDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Categories <> '''' → Activa @FilterByCategories=1 y carga el listado de categorías a filtrar else No se aplica filtro por categorías; si @ProductionCenters <> '''' → Activa @FilterByProductionCenters=1 y carga el listado de centros de producción a filtrar else No se aplica filtro por centros de producción; si @TypeReport = 3 → Ejecuta la obtención de datos desde GeneralLedgerBalance vía homologación de centros de producción else No se cargan datos en @Table_Result (sólo retornará vacío o error); si @GroupBy = 3 → Aplica filtro cosc.Level >= @Level y ejecuta el ciclo de ascenso jerárquico para asignar la estructura organizacional al nivel deseado else No se aplica restricción de nivel ni reasignación jerárquica; si mac.Nature = 1 (cuenta de naturaleza débito) → Value = DebitValue - CreditValue else Value = CreditValue - DebitValue; si CenterType en (1,2,3) → Etiqueta el centro como ''Operativo'', ''Administrativo'' o ''Logístico'' respectivamente else Etiqueta como ''N/A''', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeTransferDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeTransferDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostProductionCenter; Cost.CostOrganizationalStructureOfCosts; Cost.CostProductionCenterHomologation; Cost.CostProductionCenterCostCenter; Cost.CostProductionCenterCategory; GeneralLedger.GeneralLedgerBalance; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeTransferDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeTransferDistribution';
-- GO
