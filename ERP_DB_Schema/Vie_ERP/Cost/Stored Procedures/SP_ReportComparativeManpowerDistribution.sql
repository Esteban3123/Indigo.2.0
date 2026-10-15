-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-25
-- Description:	Procedimiento para el reporte comparativo de costos de mano de obra
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReportComparativeManpowerDistribution]
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
		ManpowerType TINYINT,
		ThirdPartyId INT,
		ThirdPartyNit VARCHAR(50),	
		ThirdPartyName VARCHAR(500),	
		PositionId INT,
		PositionCode VARCHAR(20),
		PositionName VARCHAR(500),
		----------------------------------------------
		AccruedValue DECIMAL(20,4),
		EmployerContributionValue DECIMAL(20,4),
		ParafiscalValue DECIMAL(20,4),
		ProvisionValue DECIMAL(20,4),
		IsInitialRange BIT,
		IsDistribuited BIT
	)

	DECLARE @Table_DistributionSecondaryDetail AS TABLE
	(
		IsInitialRange BIT,
		SourceProductionCenterId INT,
		TargetProductionCenterId INT,
		----------------------------------------------
		AccruedValue DECIMAL(20,4),
		EmployerContributionValue DECIMAL(20,4),
		ParafiscalValue DECIMAL(20,4),
		ProvisionValue DECIMAL(20,4),
		----------------------------------------------
		TotalAccrued DECIMAL(20,4),
		TotalEmployerContribution DECIMAL(20,4),
		TotalParafiscal DECIMAL(20,4),
		TotalProvision DECIMAL(20,4),
		CalculatedValue DECIMAL(20,4)
	)

	DECLARE @Table_DistributionSecondary AS TABLE
	(
		IsInitialRange BIT,
		ProductionCenterId INT,
		ManpowerType TINYINT,
		ThirdPartyId INT,
		ThirdPartyNit VARCHAR(50),	
		ThirdPartyName VARCHAR(500),	
		PositionId INT,
		PositionCode VARCHAR(20),
		PositionName VARCHAR(500),
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
					cpc.Id ProductionCenterId,
					cpc.Code ProductionCenterCode,
					cpc.Name ProductionCenterName,
					cpc.CenterType,
					cdm.ManpowerType,
					tp.Id ThirdPartyId, 
					tp.Nit ThirdPartyNit,
					tp.Name ThirdPartyName,
					p.Id PositionId,
					p.Code PositionCode, 
					p.Name PositionName,
					SUM(cdm.TotalAccrued) AccruedValue,
					SUM(cdm.TotalEmployerContribution) EmployerContributionValue,
					SUM(cdm.TotalParafiscal) ParafiscalValue,
					SUM(cdm.TotalProvision) ProvisionValue,
					cdm.isInitialRange,
					0 IsDistribuited
				FROM Cost.CostProductionCenter cpc WITH (NOLOCK)
				JOIN @Table_CenterType ct ON cpc.CenterType = ct.CenterType
				JOIN Cost.CostOrganizationalStructureOfCosts cosc WITH (NOLOCK) ON cpc.OrganizationalStructureOfCostId = cosc.Id
				JOIN
				(	
					SELECT	cdm.ManpowerType, cdm.ThirdPartyId, cdm.PositionId, cdmd.ProductionCenterId, 1 isInitialRange,
							SUM(cdmd.TotalAccrued) TotalAccrued, 
							SUM(cdmd.TotalEmployerContribution) TotalEmployerContribution,
							SUM(cdmd.TotalParafiscal) TotalParafiscal,
							SUM(cdmd.TotalProvision) TotalProvision
					FROM Cost.CostDistributionManpower cdm WITH (NOLOCK)
					JOIN Cost.CostDistributionManpowerDetail cdmd WITH (NOLOCK) ON cdm.Id = cdmd.DistributionManpowerId
					WHERE cdm.Status = 1 AND
					(
						(cdm.Year > @InitialRangeYearStart OR (cdm.Year = @InitialRangeYearStart AND cdm.Month >= @InitialRangeMonthStart))
						AND
						(cdm.Year < @InitialRangeYearEnd OR (cdm.Year = @InitialRangeYearEnd AND cdm.Month <= @InitialRangeMonthEnd))
					)
					GROUP BY cdm.ManpowerType, cdm.ThirdPartyId, cdm.PositionId, cdmd.ProductionCenterId
				UNION ALL
					SELECT	cdm.ManpowerType, cdm.ThirdPartyId, cdm.PositionId, cdmd.ProductionCenterId, 0 isInitialRange,
							SUM(cdmd.TotalAccrued) TotalAccrued, 
							SUM(cdmd.TotalEmployerContribution) TotalEmployerContribution,
							SUM(cdmd.TotalParafiscal) TotalParafiscal,
							SUM(cdmd.TotalProvision) TotalProvision
					FROM Cost.CostDistributionManpower cdm WITH (NOLOCK)
					JOIN Cost.CostDistributionManpowerDetail cdmd WITH (NOLOCK) ON cdm.Id = cdmd.DistributionManpowerId
					WHERE cdm.Status = 1 AND
					(
						(cdm.Year > @FinalRangeYearStart OR (cdm.Year = @FinalRangeYearStart AND cdm.Month >= @FinalRangeMonthStart))
						AND
						(cdm.Year < @FinalRangeYearEnd OR (cdm.Year = @FinalRangeYearEnd AND cdm.Month <= @FinalRangeMonthEnd))
					)
					GROUP BY cdm.ManpowerType, cdm.ThirdPartyId, cdm.PositionId, cdmd.ProductionCenterId
				) cdm ON cpc.Id = cdm.ProductionCenterId
				LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON cdm.ThirdPartyId = tp.Id
				LEFT JOIN Payroll.Position p WITH (NOLOCK) ON cdm.PositionId = p.Id
				LEFT JOIN Cost.CostProductionCenterCategory cpcc WITH (NOLOCK) ON cpc.CategoryId = cpcc.Id
				LEFT JOIN @Table_Categories c ON cpcc.Id = c.Id
				LEFT JOIN @Table_ProductionCenters pc ON cpc.Id = pc.Id
				WHERE (@FilterByCategories = 0 OR c.Id IS NOT NULL)
					AND (@FilterByProductionCenters = 0 OR pc.Id IS NOT NULL)
					AND (@GroupBy <> 3 OR cosc.Level >= @Level)
				GROUP BY	cosc.ParentId, cosc.Code, cosc.Name, cosc.Level,
							cpcc.Code, cpcc.Name, 
							cpc.Id, cpc.Code, cpc.Name, cpc.CenterType,
							cdm.ManpowerType, tp.Id, tp.Nit, tp.Name,
							p.Id, p.Code, p.Name,
							cdm.isInitialRange
				OPTION (RECOMPILE)

			/************************************************* DISTRIBUCIÓN SECUNDARIA *************************************************/

			-- PENDIENTE --

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
		ISNULL(ri.ManpowerType, rf.ManpowerType) ManpowerType,
		CASE ISNULL(ri.ManpowerType, rf.ManpowerType)
			WHEN 1 THEN 'Empleado'
			WHEN 2 THEN 'Contratista'
			ELSE 'N/A'
		END ManpowerTypeName,
		ISNULL(ri.ThirdPartyNit, rf.ThirdPartyNit) ThirdPartyNit, ISNULL(ri.ThirdPartyName, rf.ThirdPartyName) ThirdPartyName,
		ISNULL(ri.PositionCode, rf.PositionCode) PositionCode, ISNULL(ri.PositionName, rf.PositionName) PositionName,

		SUM(ISNULL(ri.AccruedValue, 0)) InitialRangeAccruedValue,
		SUM(ISNULL(ri.EmployerContributionValue, 0)) InitialRangeEmployerContributionValue,
		SUM(ISNULL(ri.ParafiscalValue, 0)) InitialRangeParafiscalValue,
		SUM(ISNULL(ri.ProvisionValue, 0)) InitialRangeProvisionValue,

		SUM(ISNULL(rf.AccruedValue, 0)) FinalRangeAccruedValue,
		SUM(ISNULL(rf.EmployerContributionValue, 0)) FinalRangeEmployerContributionValue,
		SUM(ISNULL(rf.ParafiscalValue, 0)) FinalRangeParafiscalValue,
		SUM(ISNULL(rf.ProvisionValue, 0)) FinalRangeProvisionValue
	FROM 
	(
		SELECT	OrganizationalStructureParentCode, OrganizationalStructureParentName,
				OrganizationalStructureCode, OrganizationalStructureName,					
				CategoryCode, CategoryName,
				ProductionCenterId, ProductionCenterCode, ProductionCenterName, CenterType,
				ManpowerType, ThirdPartyId, ThirdPartyNit, ThirdPartyName,
				PositionCode, PositionName,
				SUM(AccruedValue) AccruedValue,
				SUM(EmployerContributionValue) EmployerContributionValue,
				SUM(ParafiscalValue) ParafiscalValue,
				SUM(ProvisionValue) ProvisionValue
		FROM @Table_Result
		WHERE IsInitialRange = 1
		GROUP BY	OrganizationalStructureParentCode, OrganizationalStructureParentName,
					OrganizationalStructureCode, OrganizationalStructureName,					
					CategoryCode, CategoryName,
					ProductionCenterId, ProductionCenterCode, ProductionCenterName, CenterType,
					ManpowerType, ThirdPartyId, ThirdPartyNit, ThirdPartyName,
					PositionCode, PositionName
	) ri
	FULL JOIN 
	(
		SELECT	OrganizationalStructureParentCode, OrganizationalStructureParentName,
				OrganizationalStructureCode, OrganizationalStructureName,					
				CategoryCode, CategoryName,
				ProductionCenterId, ProductionCenterCode, ProductionCenterName, CenterType,
				ManpowerType, ThirdPartyId, ThirdPartyNit, ThirdPartyName,
				PositionCode, PositionName,
				SUM(AccruedValue) AccruedValue,
				SUM(EmployerContributionValue) EmployerContributionValue,
				SUM(ParafiscalValue) ParafiscalValue,
				SUM(ProvisionValue) ProvisionValue
		FROM @Table_Result
		WHERE IsInitialRange = 0
		GROUP BY	OrganizationalStructureParentCode, OrganizationalStructureParentName,
					OrganizationalStructureCode, OrganizationalStructureName,					
					CategoryCode, CategoryName,
					ProductionCenterId, ProductionCenterCode, ProductionCenterName, CenterType,
					ManpowerType, ThirdPartyId, ThirdPartyNit, ThirdPartyName,
					PositionCode, PositionName
	) rf 
		ON ri.ProductionCenterId = rf.ProductionCenterId 
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
		ISNULL(ri.ManpowerType, rf.ManpowerType), ISNULL(ri.ThirdPartyNit, rf.ThirdPartyNit), ISNULL(ri.ThirdPartyName, rf.ThirdPartyName),
		ISNULL(ri.PositionCode, rf.PositionCode), ISNULL(ri.PositionName, rf.PositionName)
	HAVING SUM(ISNULL(ri.AccruedValue, 0)) <> 0 OR SUM(ISNULL(rf.AccruedValue, 0)) <> 0
		OR SUM(ISNULL(ri.EmployerContributionValue, 0)) <> 0 OR SUM(ISNULL(rf.EmployerContributionValue, 0)) <> 0
		OR SUM(ISNULL(ri.ParafiscalValue, 0)) <> 0 OR SUM(ISNULL(rf.ParafiscalValue, 0)) <> 0
		OR SUM(ISNULL(ri.ProvisionValue, 0)) <> 0 OR SUM(ISNULL(rf.ProvisionValue, 0)) <> 0
END