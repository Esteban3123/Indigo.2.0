
CREATE PROCEDURE [Maintenance].[SP_ReportMaintenanceRepairs]
		@xmlCriterias AS XML,
		@xmlFilters AS XML				
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY

	DECLARE 	
			
			-- CRITERIOS --									
			@DateStart Date,
			@DateEnd Date,		

			--FILTROS--			
			@State TINYINT,
			@PhysicalAssetId VARCHAR(MAX),
			@FixedAssetItemId VARCHAR(MAX),
			@MaintenanceResponsibleId VARCHAR(MAX),
			@FixedAssetItemCatalogId VARCHAR(MAX),
			@Quantity INT,
			---------------------------------------------------------------------------------------
			 
			@FilterByPhysicalAssetId BIT = 0,
			@FilterByFixedAssetItemId BIT = 0,
			@FilterByMaintenanceResponsibleId BIT = 0,
			@FilterByFixedAssetItemCatalogId BIT = 0,
			@FilterQuantity INT = 0,
			@FilterState TINYINT = 0
	
	DECLARE @Table_PhysicalAssetId AS TABLE(Id INT)
	DECLARE @Table_FixedAssetItemId AS TABLE(Id INT)
	DECLARE @Table_MaintenanceResponsibleId AS TABLE(Id INT)
	DECLARE @Table_FixedAssetItemCatalogId AS TABLE(Id INT)
	DECLARE @Table_State AS TABLE(Id INT)
	DECLARE @Table_Quantity AS TABLE(Id INT)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT	@DateStart = t.x.value('DateStart[1]','date'),
				@DateEnd = t.x.value('DateEnd[1]','date')
				FROM @xmlCriterias.nodes('/Data') t(x)

		--Se obtienen los datos de los filtros
		SELECT	@State = t.x.value('State[1]','int'),
				---------------------------------------------------------------------------------------
				@PhysicalAssetId = t.x.value('PhysicalAssetId [1]','varchar(max)'),
				@FixedAssetItemId = t.x.value('FixedAssetItemId[1]','varchar(max)'),
				@MaintenanceResponsibleId = t.x.value('MaintenanceResponsibleId[1]','varchar(max)'),
				@FixedAssetItemCatalogId = t.x.value('FixedAssetItemCatalogId[1]','varchar(max)'),
				@Quantity = t.x.value('Quantity[1]','int')
				FROM @xmlFilters.nodes('/Data') t(x)

		-------------------------------------------------------------------------------------------------
		IF ISNULL(@PhysicalAssetId, '') <> ''
		BEGIN
			SET @FilterByPhysicalAssetId = 1		
			INSERT INTO @Table_PhysicalAssetId
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@PhysicalAssetId, ',')
		END

		IF ISNULL(@FixedAssetItemId, '') <> ''
		BEGIN
			SET @FilterByFixedAssetItemId = 1

			INSERT INTO @Table_FixedAssetItemId
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@FixedAssetItemId, ',')
		END

		IF ISNULL(@MaintenanceResponsibleId, '') <> ''
		BEGIN
			SET @FilterByMaintenanceResponsibleId = 1

			INSERT INTO @Table_MaintenanceResponsibleId
				SELECT CAST(Data AS INT) Data 		
				FROM dbo.Split(@MaintenanceResponsibleId, ',')
		END

		IF ISNULL(@FixedAssetItemCatalogId, '') <> ''
		BEGIN
			SET @FilterByFixedAssetItemCatalogId = 1

			INSERT INTO @Table_FixedAssetItemCatalogId
				SELECT CAST(Data AS INT) Data 		
				FROM dbo.Split(@FixedAssetItemCatalogId, ',')
		END		

		IF ISNULL(@State, '') <> ''
		BEGIN
			SET @FilterState = 1

			INSERT INTO @Table_State
				SELECT CAST(Data AS INT) Data 		
				FROM dbo.Split(@State, ',')
		END	

		IF ISNULL(@Quantity, '') <> ''
		BEGIN
			SET @FilterQuantity = 1

			INSERT INTO @Table_Quantity
				SELECT CAST(Data AS INT) Data 		
				FROM dbo.Split(@Quantity, ',')
		END	

		/********************************** OBTENCION DE DATOS **********************************/	
	BEGIN		
			SELECT	
					fapa.Plate AS Plate,
					CONCAT(fait.Code,'-',fait.Description)AS ItemCodeName,
					fat.Name AS TrademarkName,
					fapa.Model AS Model,
					fapa.Serie AS Serie,					
					Sum(Iif(mp.Type = 3,1,0)) As Preventive,					
					Sum(Iif(mp.Type = 4,1,0)) As Corrective,					
					COUNT(Iif(mp.Type IN (3,4),1,0)) AS TotalMaintenance																			
			FROM [Maintenance].[MaintenanceProtocol] AS mp				
			JOIN Maintenance.WorkOrder wo ON mp.Id = wo.ProtocolId
			--LEFT JOIN Maintenance.MaintenancePlanProgramated mpp WITH (NOLOCK) ON  wo.MaintenanceProgrametedId = mpp.Id					
			LEFT JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON wo.PhysicalAssetId = fapa.Id
			LEFT JOIN FixedAsset.FixedAssetItem fait WITH (NOLOCK) ON fapa.ItemId = fait.Id
			JOIN FixedAsset.FixedAssetTrademark fat WITH (NOLOCK) ON fapa.TrademarkId = fat.Id		
			INNER JOIN FixedAsset.FixedAssetItemCatalog faic WITH (NOLOCK)  ON  fait.ItemCatalogId =faic.Id
			LEFT JOIN Maintenance.ResponsibleCatalogOfArticles rca WITH (NOLOCK) ON faic.Id =rca.ItemCatalogId
			LEFT JOIN Maintenance.MaintenanceResponsible kk WITH (NOLOCK) ON rca.ResponsibleId = kk.Id
			
			--*************************************************************************************
			LEFT JOIN @Table_PhysicalAssetId tpt ON fapa.Id = tpt.Id
			LEFT JOIN @Table_FixedAssetItemId tc ON fait.Id = tc.Id
			LEFT JOIN @Table_MaintenanceResponsibleId tdt ON KK.Id = tdt.Id
			LEFT JOIN @Table_FixedAssetItemCatalogId ts ON faic.Id = ts.Id
			LEFT JOIN @Table_State tss ON wo.State = tss.Id
			WHERE wo.ProgramDate BETWEEN Cast(@DateStart As DATETIME) AND Cast(@DateEnd As DATETIME)
				AND (@FilterByPhysicalAssetId = 0 OR tpt.Id IS NOT NULL)
				AND	(@FilterByFixedAssetItemId = 0 OR tc.Id IS NOT NULL) 
				AND	(@FilterByMaintenanceResponsibleId = 0 OR tdt.Id IS NOT NULL)
				AND	(@FilterByFixedAssetItemCatalogId = 0 OR ts.Id IS NOT NULL)				
				AND (@FilterState = 0 OR tss.Id IS NOT NULL)
				AND mp.Type IN (3, 4)									
			GROUP BY fapa.Plate,mp.Type,fait.Code,fait.Description,fapa.Model,fapa.Serie,wo.PhysicalAssetId,fat.Name
			HAVING
			(			
				(@Quantity = 0)
				OR
				(@Quantity < 10 AND COUNT(1) = @Quantity)
				OR
				(@Quantity >= 10 AND COUNT(1) >= @Quantity)
			)				
	END
	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END