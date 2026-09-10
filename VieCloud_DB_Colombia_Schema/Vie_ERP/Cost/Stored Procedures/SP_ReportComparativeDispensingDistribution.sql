-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-02-04
-- Description:	Procedimiento para el reporte comparativo de dispensacion
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReportComparativeDispensingDistribution]
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

		IF @TypeReport = 2
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
				WHERE cpch.HomologationType = 2
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte comparativo de dispensación y distribución de costos entre dos períodos de tiempo (rango inicial y rango final), permitiendo analizar la evolución del gasto por centros de producción, categorías y estructura organizativa de costos. Cruza los saldos contables del libro mayor (débitos y créditos por cuenta, tercero y centro de costo) con la homologación y los centros de producción, filtrando por tipo de centro, categoría o centro de producción según los parámetros recibidos. Recibe criterios y filtros en formato XML, decodifica listas de valores usando la función Split, y produce un resultado detallado que incluye la estructura jerárquica de costos, el código y nombre de la cuenta contable, el tercero (NIT y nombre), el valor acumulado y si el movimiento corresponde a costos distribuidos o directos. Está orientado al módulo de costos hospitalarios para comparar la dispensación de recursos entre períodos y apoyar la toma de decisiones financieras y de gestión.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportComparativeDispensingDistribution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportComparativeDispensingDistribution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte comparativo de dispensación entre dos rangos de períodos contables (inicial vs final), agrupando saldos del libro mayor por centro de producción, categoría o estructura organizacional, con filtros opcionales.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeDispensingDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@xmlCriterias debe contener los nodos InitialRangeYearStart/MonthStart/YearEnd/MonthEnd y FinalRangeYearStart/MonthStart/YearEnd/MonthEnd dentro de /Data; @xmlFilters debe contener TypeReport, GroupBy, Level, CenterType, Categories y ProductionCenters dentro de /Data; @CenterType debe ser una lista de enteros separados por coma (no vacía) procesable por dbo.Split; Para que se generen datos, @TypeReport debe ser igual a 2; Las cuentas contables deben tener una clase asociada (MainAccountClasses) con Nature definida (1 = débito)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeDispensingDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan homologaciones cuyo HomologationType = 2; Los saldos se calculan respetando la naturaleza contable de la cuenta (débito vs crédito); Los rangos de fechas se evalúan por (Year, Month) usando comparaciones combinadas, garantizando inclusión de los meses extremos; Las filas cuyo total combinado (inicial + final) es cero no se devuelven; El CenterType numérico se traduce siempre a etiquetas: 1=''Operativo'', 2=''Administrativo'', 3=''Logístico'', otros=''N/A''; El JOIN final FULL OUTER se hace por ProductionCenterId, AccountId y ThirdPartyId (tratando NULL como 0) para alinear los dos rangos; Cualquier excepción se captura y se reporta como una fila con código ''999'' en lugar de propagar el error', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeDispensingDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción / centro de costo; Estructura organizacional de costos; Homologación de cuentas contables; Plan de cuentas (cuenta contable principal); Naturaleza contable (débito/crédito); Saldo del libro mayor por período; Tercero (NIT); Categoría de centro de producción; Tipo de centro: Operativo, Administrativo, Logístico; Reporte comparativo de dispensación entre rangos de períodos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeDispensingDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT_SET: Se devuelve un conjunto de resultados con los valores agregados de los rangos inicial y final (InitialRangeValue, FinalRangeValue) por agrupación elegida, excluyendo filas donde ambas sumas son cero (HAVING SUM(ri.Value)<>0 OR SUM(rf.Value)<>0); [RETURN_RESULT] RESULT_SET: Cuando ocurre un error en el TRY, el CATCH inserta una fila con código ''999'' y el mensaje/línea del error en los campos descriptivos, y esa fila se devuelve como parte del resultado', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeDispensingDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Categories <> '''' → Activa @FilterByCategories=1 y carga la lista de categorías; el resultado se filtra exigiendo que la categoría esté en la lista else No se aplica filtro por categorías; si @ProductionCenters <> '''' → Activa @FilterByProductionCenters=1 y carga la lista; el resultado se filtra exigiendo que el centro de producción esté en la lista else No se aplica filtro por centros de producción; si @TypeReport = 2 → Ejecuta la obtención y agregación de saldos contables comparando ambos rangos else No se cargan datos en @Table_Result (el reporte saldrá vacío salvo errores); si @GroupBy = 3 (agrupación por estructura organizacional) → Filtra por cosc.Level >= @Level y ejecuta el bucle de re-asignación ascendente de la jerarquía organizacional hasta alcanzar @Level else No se realiza el ajuste jerárquico; se agrupa por centro de producción (1) o categoría (2); si mac.Nature = 1 (cuenta de naturaleza débito) → Calcula Value como (DebitValue - CreditValue) else Calcula Value como (CreditValue - DebitValue); si cpch.HomologationType = 2 → Solo se consideran las homologaciones de tipo 2 al cruzar cuentas contables con centros de producción', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeDispensingDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeDispensingDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostProductionCenter; Cost.CostOrganizationalStructureOfCosts; Cost.CostProductionCenterHomologation; Cost.CostProductionCenterCostCenter; Cost.CostProductionCenterCategory; GeneralLedger.GeneralLedgerBalance; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeDispensingDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeDispensingDistribution';
-- GO
