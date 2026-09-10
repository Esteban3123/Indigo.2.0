-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-24
-- Description:	Procedimiento para el reporte comparativo de gastos generales
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReportComparativeCostDistribution]
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

		IF @TypeReport = 4
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
				WHERE cpch.HomologationType = 4					
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
						tr.AccountId,
						tr.AccountNumber,
						tr.AccountName,
						tr.ThirdPartyId,
						tr.ThirdPartyNit,
						tr.ThirdPartyName,
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
					tds.AccountId,
					tds.AccountNumber,
					tds.AccountName,
					tds.ThirdPartyId,
					tds.ThirdPartyNit,
					tds.ThirdPartyName,
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte comparativo de distribución de costos entre dos rangos de período (inicial y final), diseñado para analizar la evolución del gasto en centros de producción de la organización. Consolida saldos contables del libro mayor (débitos y créditos por cuenta y tercero) contra los centros de costo y centros de producción, aplicando filtros por tipo de centro, categorías y estructura organizativa de costos. Permite agrupar y comparar los gastos generales de un período frente a otro, identificando si los valores ya fueron distribuidos o están pendientes de distribución, con opción de desglose por nivel jerárquico, cuenta contable y tercero. Se usa en el módulo de costos para apoyar decisiones de reasignación presupuestal, auditoría de gastos y análisis comparativo de periodos en centros de producción (administrativos, asistenciales, etc.).', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportComparativeCostDistribution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportComparativeCostDistribution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte comparativo de gastos generales entre dos rangos período (inicial vs final) agrupado por centro de producción, categoría o estructura organizacional, basándose en saldos del libro mayor homologados a centros de costo.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeCostDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los XML @xmlCriterias y @xmlFilters deben contener los nodos esperados (rangos año/mes inicial y final, TypeReport, GroupBy, Level, CenterType, Categories, ProductionCenters); @CenterType debe contener al menos un identificador separado por comas para que dbo.Split lo cargue; Deben existir homologaciones con HomologationType = 4 en Cost.CostProductionCenterHomologation para obtener datos; Las cuentas contables deben tener una clase con Nature definida (1=débito) en GeneralLedger.MainAccountClasses', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeCostDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran homologaciones con HomologationType = 4; El valor contable se calcula respetando la naturaleza de la cuenta (débito vs crédito); Los rangos de período se evalúan inclusivos por (Año, Mes) tanto para rango inicial como final; Si @FilterByCategories=0 o @FilterByProductionCenters=0, no se aplica el filtro respectivo; Cuando @GroupBy=3, se promueven los niveles de estructura organizacional hasta alcanzar el @Level solicitado; El bloque de distribución secundaria está comentado y no se ejecuta (IsDistribuited siempre = 0); Las filas con suma de InitialRangeValue y FinalRangeValue iguales a 0 se excluyen del resultado final; Errores en tiempo de ejecución no propagan excepción: se reportan como fila con código ''999''', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeCostDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción / centro de costo; Estructura organizacional de costos; Homologación de cuentas contables; Saldos del libro mayor (PUC); Naturaleza de cuenta contable (débito/crédito); Tercero (NIT); Categoría de centro de producción; Tipo de centro: Operativo, Administrativo, Logístico; Comparativo de gastos generales entre dos períodos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeCostDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve filas comparativas con InitialRangeValue y FinalRangeValue, filtrando con HAVING para excluir filas donde ambos valores sumen 0; [RETURN_RESULT] RESULTSET: El código/nombre devuelto depende de @GroupBy: 1=ProductionCenter, 2=Category, 3=OrganizationalStructure; [RETURN_RESULT] RESULTSET: CenterTypeName se traduce: 1=''Operativo'', 2=''Administrativo'', 3=''Logístico'', otro=''N/A''; [RETURN_RESULT] RESULTSET: Si ocurre una excepción en TRY, se devuelve una fila con código ''999'' y el mensaje y línea del error en lugar de los datos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeCostDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Categories <> '''' → Activa @FilterByCategories=1 y carga la lista de categorías para filtrar resultados else No filtra por categorías; si @ProductionCenters <> '''' → Activa @FilterByProductionCenters=1 y carga la lista de centros de producción para filtrar else No filtra por centros de producción; si @TypeReport = 4 → Ejecuta la obtención de datos comparativa entre rango inicial y final desde GeneralLedgerBalance else No realiza extracción de datos (la tabla resultado queda vacía); si @GroupBy = 3 (estructura organizacional) → Aplica filtro cosc.Level >= @Level y ejecuta el ciclo de promoción de niveles para reasignar Parent al nivel solicitado else No reasigna estructura organizacional; si mac.Nature = 1 → Calcula Value como (DebitValue - CreditValue) else Calcula Value como (CreditValue - DebitValue)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeCostDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeCostDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostProductionCenter; Cost.CostOrganizationalStructureOfCosts; Cost.CostProductionCenterHomologation; Cost.CostProductionCenterCostCenter; Cost.CostProductionCenterCategory; GeneralLedger.GeneralLedgerBalance; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeCostDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeCostDistribution';
-- GO
