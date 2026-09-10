
CREATE VIEW [Maintenance].[ViewMaintenanceWithOutProgramming]
as
(
	select
		erg.Id as Id,		
		erg.FixedAssetPhysicalAssetId as PhysetAssetId,
		lo.Id as LocationId,
		pai.Id as ItemId,
		pait.InventoryTypeId as InventoryTypeId,
		pai.ItemTypeId as ItemTypeId,
		pa.Plate,
		pai.Code as ArticleCode, 
		pai.[Description] as ArticleName, 
		concat(tm.Code, ' - ', tm.[Name]) as TrademarkCodeName,
		pa.Serie, 
		pa.Model, 
		lo.Code as LocationCode, 
		lo.[Name] as LocationName,
		concat(lo.code, ' - ', lo.[Name]) as LocationCodeName		
	from FixedAsset.FixedAssetPhysicalAsset pa with(nolock)
	inner join FixedAsset.FixedAssetItem pai with(nolock) on pai.Id = pa.ItemId
	inner join FixedAsset.FixedAssetItemType pait with(nolock) on pai.[ItemTypeId] = pait.Id	
	inner join FixedAsset.FixedAssetLocation lo with(nolock) on pa.LocationId = lo.Id
	inner join Maintenance.EquipmentRegistration er with(nolock) on er.FixedAssetPhysicalAssetId = pa.Id
	inner join FixedAsset.FixedAssetTrademark tm with(nolock) on pa.TrademarkId = tm.Id
	inner join Maintenance.EquipmentRegistration erg with(nolock) on erg.FixedAssetPhysicalAssetId = pa.Id
)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los equipos registrados en el módulo de mantenimiento que aún no tienen programación de mantenimiento asignada. Combina información del activo físico (placa, serie, modelo), el artículo o ítem de inventario (código y descripción del bien), la marca del equipo y la ubicación física donde se encuentra. Sirve para identificar qué equipos están pendientes de ser incluidos en un plan o cronograma de mantenimiento preventivo o correctivo, permitiendo gestionar las brechas en la programación de mantenimiento de activos fijos.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewMaintenanceWithOutProgramming';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewMaintenanceWithOutProgramming';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los equipos registrados en mantenimiento junto con sus datos de activo fijo (placa, serie, modelo, marca, ubicación e ítem) para identificar aquellos disponibles sin programación de mantenimiento asociada.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceWithOutProgramming';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El activo físico debe tener un ítem en FixedAssetItem y un tipo de ítem en FixedAssetItemType.; El activo físico debe tener una ubicación válida en FixedAssetLocation y una marca válida en FixedAssetTrademark.; El activo físico debe estar registrado como equipo en Maintenance.EquipmentRegistration.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceWithOutProgramming';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen activos físicos que tienen al menos un EquipmentRegistration asociado (INNER JOIN con Maintenance.EquipmentRegistration).; Se excluyen activos sin ítem, sin tipo de ítem, sin ubicación o sin marca (todos los joins son INNER).; Las consultas se realizan con WITH(NOLOCK), permitiendo lecturas sucias sobre las tablas fuente.; El campo TrademarkCodeName se construye como ''Code - Name'' de la marca; LocationCodeName se construye como ''code - Name'' de la ubicación.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceWithOutProgramming';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'activo fijo físico; registro de equipo; ítem de activo fijo; tipo de inventario; ubicación de activo; marca; placa; serie; modelo; mantenimiento sin programación', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceWithOutProgramming';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Maintenance.ViewMaintenanceWithOutProgramming: Devuelve una fila por registro de equipo (EquipmentRegistration) con los datos consolidados del activo físico, su ítem, tipo de inventario, marca y ubicación.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceWithOutProgramming';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemType; FixedAsset.FixedAssetLocation; Maintenance.EquipmentRegistration; FixedAsset.FixedAssetTrademark', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceWithOutProgramming';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceWithOutProgramming';
GO
