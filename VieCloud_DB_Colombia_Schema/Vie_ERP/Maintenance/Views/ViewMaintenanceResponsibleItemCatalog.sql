CREATE VIEW [Maintenance].[ViewMaintenanceResponsibleItemCatalog]
AS
SELECT	CONCAT(mr.Id, ' - ', rcoa.Id) RowId,
		faic.Id,
		faic.Code, 
		faic.Description,
		mr.Id ResponsibleId
FROM FixedAsset.FixedAssetItemCatalog faic WITH (NOLOCK)
JOIN Maintenance.ResponsibleCatalogOfArticles rcoa WITH (NOLOCK) ON faic.Id = rcoa.ItemCatalogId
JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON rcoa.ResponsibleId = mr.Id
WHERE mr.Status = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de ítems o artículos de activos fijos asignados a cada responsable de mantenimiento activo. Combina el catálogo de artículos (con su código y descripción) con la tabla de asignaciones de responsables y filtra únicamente los responsables con estado activo. Sirve para consultar rápidamente qué ítems o equipos tiene a cargo cada persona o tercero encargado del mantenimiento, y se usa en reportes y pantallas de gestión de mantenimiento preventivo y correctivo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewMaintenanceResponsibleItemCatalog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewMaintenanceResponsibleItemCatalog';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los ítems del catálogo de activos fijos asignados a responsables de mantenimiento activos, permitiendo identificar qué artículos están bajo la custodia de cada responsable vigente.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleItemCatalog';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir relación en ResponsibleCatalogOfArticles entre el ítem del catálogo de activos fijos y un responsable de mantenimiento.; El responsable de mantenimiento debe tener Status = 1 (activo) para que el ítem aparezca.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleItemCatalog';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen ítems cuyo responsable de mantenimiento está activo (mr.Status = 1).; Cada fila se identifica unívocamente por la combinación responsable-ítem mediante CONCAT(mr.Id, '' - '', rcoa.Id) como RowId.; Un ítem aparece tantas veces como responsables activos lo tengan asignado a través de ResponsibleCatalogOfArticles.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleItemCatalog';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Responsable de mantenimiento; Catálogo de ítems de activo fijo; Asignación de artículos a responsables', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleItemCatalog';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Maintenance.ViewMaintenanceResponsibleItemCatalog: Devuelve un registro por cada par (ítem de catálogo, responsable) cuando mr.Status = 1; ítems sin responsable activo o sin asignación quedan excluidos por los JOIN internos.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleItemCatalog';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetItemCatalog; Maintenance.ResponsibleCatalogOfArticles; Maintenance.MaintenanceResponsible', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleItemCatalog';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleItemCatalog';
GO
