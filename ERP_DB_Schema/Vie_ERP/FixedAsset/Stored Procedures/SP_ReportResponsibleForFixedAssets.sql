-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-11-13
-- Description:	Procedimiento para el reporte de edades de cartera
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_ReportResponsibleForFixedAssets]
	@xmlCriterias AS XML,
	@xmlFilters AS XML
AS
BEGIN
	SET NOCOUNT ON;
	SET DATEFORMAT DMY

	DECLARE -- CRITERIOS --
			@Responsibles VARCHAR(MAX),
			@ItemCatalogs VARCHAR(MAX),
			@Items VARCHAR(MAX),
			@ItemTypes VARCHAR(MAX),
			-- FILTROS --
			@Locations VARCHAR(MAX),
			@PhysicalAssets VARCHAR(MAX),
			@AdquisitionType VARCHAR(MAX),
			-------------
			@FilterByResponsibles BIT = 0,
			@FilterByItemCatalogs BIT = 0,
			@FilterByItems BIT = 0,
			@FilterByItemTypes BIT = 0,
			@FilterByLocations BIT = 0,
			@FilterByPhysicalAssets BIT = 0

	DECLARE @Table_Responsibles AS TABLE(Id INT)
	DECLARE @Table_ItemCatalogs AS TABLE(Id INT)
	DECLARE @Table_Items AS TABLE(Id INT)
	DECLARE @Table_ItemTypes AS TABLE(Id INT)
	DECLARE @Table_Locations AS TABLE(Id INT)
	DECLARE @Table_PhysicalAssets AS TABLE(Id INT)
	DECLARE @Table_AdquisitionType AS TABLE(AdquisitionType INT)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 
			@Responsibles = t.x.value('Responsibles[1]','varchar(max)'),
			@ItemCatalogs = t.x.value('ItemCatalogs[1]','varchar(max)'),
			@Items = t.x.value('Items[1]','varchar(max)'),
			@ItemTypes = t.x.value('ItemTypes[1]','varchar(max)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		--Se obtienen los datos de los filtros
		SELECT 
			@Locations = t.x.value('Locations[1]','varchar(max)'),
			@PhysicalAssets = t.x.value('PhysicalAssets[1]','varchar(max)'),
			@AdquisitionType = t.x.value('AdquisitionType[1]','varchar(max)')
		FROM @xmlFilters.nodes('/Data') t(x)

		/***********************************  TABLA TEMPORALES ***********************************/

		IF @Responsibles <> ''
		BEGIN
			SET @FilterByResponsibles = 1

			INSERT INTO @Table_Responsibles
				SELECT CAST(Data AS INT) ResponsibleId 			
				FROM dbo.Split(@Responsibles, ',')
		END

		IF @ItemCatalogs <> ''
		BEGIN
			SET @FilterByItemCatalogs = 1

			INSERT INTO @Table_ItemCatalogs
				SELECT CAST(Data AS INT) ItemCatalogId 
				FROM dbo.Split(@ItemCatalogs, ',')
		END

		IF @Items <> ''
		BEGIN
			SET @FilterByItems = 1

			INSERT INTO @Table_Items
				SELECT CAST(Data AS INT) ItemId 
				FROM dbo.Split(@Items, ',')
		END

		IF @ItemTypes <> ''
		BEGIN
			SET @FilterByItemTypes = 1

			INSERT INTO @Table_ItemTypes
				SELECT CAST(Data AS INT) ItemTypeId 
				FROM dbo.Split(@ItemTypes, ',')
		END

		IF @Locations <> ''
		BEGIN
			SET @FilterByLocations = 1

			INSERT INTO @Table_Locations
				SELECT CAST(Data AS INT) LocationId 
				FROM dbo.Split(@Locations, ',')
		END

		IF @PhysicalAssets <> ''
		BEGIN
			SET @FilterByPhysicalAssets = 1

			INSERT INTO @Table_PhysicalAssets
				SELECT CAST(Data AS INT) PhysicalAssetId 
				FROM dbo.Split(@PhysicalAssets, ',')
		END

		INSERT INTO @Table_AdquisitionType
			SELECT CAST(Data AS INT) AdquisitionType 
			FROM dbo.Split(@AdquisitionType, ',')

		/********************************** OBTENCION DE DATOS **********************************/

		SELECT	
				[Common].[GetIdentificationNameTypeByCode](p.IdentificationType) IdentificationType,
				tp.Nit ThirdPartyNit,
				tp.Name ThirdPartyName,
				fal.Code LocationCode,
				fal.Name LocationName,
				fai.Code ItemCode,
				fai.Description ItemDescription,
				fapa.Plate,
				fapa.Serie,
				fapa.Model,
				fat.Name TrademarkName,
				fapa.AdquisitionDate,
				fapa.HistoricalValue,
				faic.Description ItemCatalogDescription,
				fasa.Name Status
		FROM Common.ThirdParty tp WITH (NOLOCK)
		JOIN Common.Person p WITH(NOLOCK) ON p.Id = tp.PersonId
		JOIN FixedAsset.FixedAssetResponsible far WITH (NOLOCK) ON tp.Id = far.ThirdPartyId
		JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON far.Id = fapa.ResponsibleId
		JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fapa.LocationId = fal.Id
		JOIN FixedAsset.FixedAssetItem fai WITH (NOLOCK) ON fapa.ItemId = fai.Id
		JOIN FixedAsset.FixedAssetItemCatalog faic WITH(NOLOCK) ON faic.Id = fai.ItemCatalogId
		JOIN FixedAsset.FixedAssetTrademark fat WITH (NOLOCK) ON fapa.TrademarkId = fat.Id
		LEFT JOIN FixedAsset.FixedAssetEntryItemDetail faeid WITH(NOLOCK) ON faeid.Plate = fapa.Plate AND faeid.Serie = fapa.Serie
		LEFT JOIN FixedAsset.FixedAssetInitialBalanceItem faibi WITH(NOLOCK) ON faibi.Serie = fapa.Serie AND faibi.Plate = fapa.Plate AND faibi.Model = fapa.Model
		LEFT JOIN FixedAsset.FixedAssetStatusAsset fasa WITH(NOLOCK) ON (fasa.Id = faibi.StatusAssetId) OR (fasa.Id = faeid.StatusAssetId)
		LEFT JOIN @Table_AdquisitionType tat ON fapa.AdquisitionType = tat.AdquisitionType
		LEFT JOIN @Table_Responsibles tr ON far.Id = tr.Id
		LEFT JOIN @Table_ItemCatalogs tic ON fai.ItemCatalogId = tic.Id
		LEFT JOIN @Table_Items ti ON fai.Id = ti.Id
		LEFT JOIN @Table_ItemTypes tit ON fai.ItemTypeId = tit.Id
		LEFT JOIN @Table_Locations tl ON fal.Id = tl.Id
		LEFT JOIN @Table_PhysicalAssets tpa ON fapa.Id = tpa.Id
		WHERE (@FilterByResponsibles = 0 OR tr.Id IS NOT NULL)
			AND (@FilterByItemCatalogs = 0 OR tic.Id IS NOT NULL)
			AND (@FilterByItems = 0 OR ti.Id IS NOT NULL)
			AND (@FilterByItemTypes = 0 OR tit.Id IS NOT NULL)
			AND (@FilterByLocations = 0 OR tl.Id IS NOT NULL)
			AND (@FilterByPhysicalAssets = 0 OR tpa.Id IS NOT NULL)
			AND fapa.HasOutput = 0

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END