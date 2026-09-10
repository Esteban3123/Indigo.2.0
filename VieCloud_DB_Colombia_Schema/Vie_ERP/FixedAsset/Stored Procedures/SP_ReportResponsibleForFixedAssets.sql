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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de responsables de activos fijos de la organización, mostrando qué bienes (equipos, muebles u otros activos) tiene asignado cada tercero o persona responsable. Combina información del catálogo de ítems, la ubicación física del activo, la marca, el número de placa, serie, modelo, fecha de adquisición, valor histórico y estado del bien (activo, dado de baja, etc.). Acepta filtros configurables por responsable, catálogo de ítem, tipo de ítem, ubicación y activo físico específico, enviados como parámetros XML, permitiendo restringir el resultado según las necesidades del usuario. Se utiliza para control patrimonial, auditorías de activos y verificación de la custodia de bienes por empleado o tercero.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportResponsibleForFixedAssets';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportResponsibleForFixedAssets';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de activos fijos con su responsable, ubicación, ítem, marca, datos de adquisición y estado, aplicando filtros opcionales de responsables, catálogos, ítems, tipos, ubicaciones, activos físicos y tipos de adquisición.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResponsibleForFixedAssets';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los XML de criterios y filtros deben tener nodo raíz /Data con elementos esperados (Responsibles, ItemCatalogs, Items, ItemTypes, Locations, PhysicalAssets, AdquisitionType); Los valores de las listas separadas por coma deben ser convertibles a INT; Deben existir registros relacionados en tercero, persona, responsable, activo físico, ubicación, ítem, catálogo y marca para que el activo aparezca en el reporte', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResponsibleForFixedAssets';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan activos físicos cuya marca de salida está en cero (HasOutput = 0); Los criterios y filtros son opcionales: cuando un filtro no está activo, no restringe el resultado; El estado del activo (Status) se resuelve indistintamente desde el detalle de entrada o desde el saldo inicial del activo; El tipo de identificación se traduce mediante la función Common.GetIdentificationNameTypeByCode; Los errores no se propagan: se devuelven como conjunto de resultados con Code=''999''', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResponsibleForFixedAssets';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Responsable de activo fijo; Tercero; Ubicación de activo fijo; Catálogo de activo fijo; Marca de activo fijo; Placa y serie del activo; Tipo de adquisición; Valor histórico; Estado del activo; Saldo inicial de activo fijo; Tipo de identificación', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResponsibleForFixedAssets';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] FixedAsset.FixedAssetPhysicalAsset: Devuelve únicamente activos físicos con HasOutput = 0 (sin salida registrada), aplicando los filtros activos según las listas recibidas; [RETURN_RESULT] (error): Cuando ocurre una excepción, devuelve una fila con Code=''999'', Message=ERROR_MESSAGE() y Line=ERROR_LINE()', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResponsibleForFixedAssets';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Cadena de responsables no vacía en los criterios → Activa filtro por responsables y carga ids en tabla temporal; si Cadena de catálogos de ítem no vacía → Activa filtro por catálogos de ítem; si Cadena de ítems no vacía → Activa filtro por ítems; si Cadena de tipos de ítem no vacía → Activa filtro por tipos de ítem; si Cadena de ubicaciones no vacía → Activa filtro por ubicaciones; si Cadena de activos físicos no vacía → Activa filtro por activos físicos; si Error en ejecución (CATCH) → Retorna fila con código ''999'', mensaje y línea de error en lugar del resultado', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResponsibleForFixedAssets';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split; Common.GetIdentificationNameTypeByCode', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResponsibleForFixedAssets';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Common.Person; FixedAsset.FixedAssetResponsible; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetLocation; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemCatalog; FixedAsset.FixedAssetTrademark; FixedAsset.FixedAssetEntryItemDetail; FixedAsset.FixedAssetInitialBalanceItem; FixedAsset.FixedAssetStatusAsset', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResponsibleForFixedAssets';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResponsibleForFixedAssets';
-- GO
