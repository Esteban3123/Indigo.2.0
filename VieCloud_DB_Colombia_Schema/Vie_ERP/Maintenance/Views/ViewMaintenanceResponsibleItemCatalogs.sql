CREATE VIEW [Maintenance].[ViewMaintenanceResponsibleItemCatalogs]
AS
SELECT	mr.Id,
		tp.Nit ThirdPartyNit,
		tp.Name ThirdPartyName,
		mr.ResponsibleRole,
		CONCAT(',',rcoa.ItemCatalogIds, ',') ItemCatalogIds
FROM Common.ThirdParty tp WITH (NOLOCK)
JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON tp.Id = mr.ThirdPartyId
JOIN 
(
	SELECT rcoa.ResponsibleId, STRING_AGG(rcoa.ItemCatalogId, ',') WITHIN GROUP (ORDER BY rcoa.ItemCatalogId) ItemCatalogIds
	FROM Maintenance.ResponsibleCatalogOfArticles rcoa WITH (NOLOCK) 
	GROUP BY rcoa.ResponsibleId
) rcoa ON mr.Id = rcoa.ResponsibleId
WHERE mr.Status = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra los responsables de mantenimiento activos junto con los catálogos de artículos que tienen asignados. Combina la información del tercero vinculado (NIT y nombre del proveedor, contratista o persona externa), el rol que cumple dentro del área de mantenimiento, y la lista consolidada de identificadores de artículos del catálogo a su cargo. Es útil para consultar rápidamente qué ítems o equipos están bajo la responsabilidad de cada encargado de mantenimiento activo en el sistema.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewMaintenanceResponsibleItemCatalogs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewMaintenanceResponsibleItemCatalogs';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los responsables de mantenimiento activos junto con el tercero asociado y la lista concatenada de catálogos de artículos a su cargo.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleItemCatalogs';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El responsable debe existir en Maintenance.MaintenanceResponsible y estar vinculado a un tercero en Common.ThirdParty.; El responsable debe tener al menos un registro en Maintenance.ResponsibleCatalogOfArticles (JOIN no LEFT).', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleItemCatalogs';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye responsables inactivos (Status distinto de 1).; Excluye responsables sin artículos asignados, debido al INNER JOIN con la subconsulta agrupada de ResponsibleCatalogOfArticles.; Los IDs de catálogo dentro de ItemCatalogIds están ordenados ascendentemente por ItemCatalogId (ORDER BY en STRING_AGG).; Usa NOLOCK en todas las tablas: pueden retornarse lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleItemCatalogs';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tercero; Responsable de mantenimiento; Rol del responsable; Catálogo de artículos; NIT', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleItemCatalogs';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Solo retorna responsables con mr.Status = 1 (activos).; [RETURN_RESULT] : ItemCatalogIds se devuelve como cadena delimitada por comas tanto al inicio como al final (CONCAT('','', STRING_AGG(...), '','')) para facilitar búsquedas de pertenencia tipo LIKE ''%,id,%''.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleItemCatalogs';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Maintenance.MaintenanceResponsible; Maintenance.ResponsibleCatalogOfArticles', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleItemCatalogs';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleItemCatalogs';
GO
