

CREATE VIEW [Maintenance].[ViewPartAccesoryConsumables]
as
(

	--Select p.Id, Code, [Name], 1 As TypeDetail, 'Parte' As TypeName, pp.PhysicalAssetId As PhysicalAssetId
	--From FixedAsset.FixedAssetPartsAccesoriesConsumables p With(Nolock)
	--inner join [FixedAsset].[FixedAssetPhysicalAssetParts] pp on pp.PartAccesoriesConsumiblesId = p.Id

	--Union All

	Select Concat(Id, 2) As Id, Id As DocumentId, Code, [Name], 2 As TypeDetail, 'ACCESORIOS' As TypeName, 0 As PhysicalAssetId
	From Maintenance.Accessory With(Nolock)

	Union All

	Select Concat(Id, 3) As Id,  Id As DocumentId, Code, [Name], 3 As TypeDetail, 'CONSUMIBLES' As TypeName, 0 As PhysicalAssetId
	From Maintenance.Consumable With(Nolock)
)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista unificada que consolida en una sola consulta los accesorios y consumibles del módulo de mantenimiento, asignando a cada registro un tipo diferenciador (ACCESORIOS o CONSUMIBLES) para distinguir su naturaleza. Combina mediante UNION ALL el catálogo de accesorios (Maintenance.Accessory) y el catálogo de consumibles (Maintenance.Consumable), exponiendo para cada ítem un identificador compuesto único, el código, el nombre y el tipo de elemento. Sirve como fuente centralizada para formularios o reportes que requieren seleccionar o listar partes, accesorios e insumos asociados a activos físicos o órdenes de mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewPartAccesoryConsumables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewPartAccesoryConsumables';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica los catálogos de accesorios y consumibles de mantenimiento en un listado homogéneo, diferenciando cada origen mediante un tipo y un identificador compuesto.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewPartAccesoryConsumables';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador expuesto (Id) se construye concatenando el Id original con el dígito del tipo (2 para accesorio, 3 para consumible), garantizando unicidad entre orígenes.; PhysicalAssetId siempre se devuelve como 0 ya que estos catálogos no están ligados a un activo físico específico.; El bloque para partes vinculadas a activos físicos (FixedAssetPartsAccesoriesConsumables/FixedAssetPhysicalAssetParts) está comentado y no forma parte del resultado actual.; Las lecturas se realizan con NOLOCK, permitiendo lecturas sucias sobre los catálogos de accesorios y consumibles.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewPartAccesoryConsumables';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Accesorios de mantenimiento; Consumibles de mantenimiento; Catálogo unificado de partes/accesorios/consumibles', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewPartAccesoryConsumables';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Maintenance.Accessory: Cada accesorio se expone con TypeDetail=2 y TypeName=''ACCESORIOS'', con Id resultante = Concat(Id,2).; [RETURN_RESULT] Maintenance.Consumable: Cada consumible se expone con TypeDetail=3 y TypeName=''CONSUMIBLES'', con Id resultante = Concat(Id,3).', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewPartAccesoryConsumables';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Maintenance.Accessory; Maintenance.Consumable', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewPartAccesoryConsumables';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewPartAccesoryConsumables';
GO
