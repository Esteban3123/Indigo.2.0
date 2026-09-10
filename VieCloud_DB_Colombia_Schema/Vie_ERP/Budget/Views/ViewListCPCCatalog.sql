

-- =============================================
-- Author:      <HECTOR RODRIGUEZ RUBIANO>
-- Create Date: <17/01/2022>
-- Description: <Lista CPC activos sin hijos>
-- =============================================
CREATE VIEW [Budget].[ViewListCPCCatalog]
AS

SELECT CPCC.[Id], CPCC.[Code], CPCC.[Name] FROM [Budget].[CPCCatalog] CPCC WITH(NOLOCK)
WHERE CPCC.[Status] = 1
AND CPCC.ID NOT IN
(SELECT DISTINCT CPCCatalogOwnerId FROM [Budget].[CPCCatalog] WITH(NOLOCK) WHERE [Status] = 1
AND CPCCatalogOwnerId IS NOT NULL)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los códigos CPC (Clasificación de Procedimientos y Condiciones) activos que son nodos hoja, es decir, que no tienen registros hijos asociados en el catálogo presupuestal. Consulta el catálogo de CPC del módulo de Presupuesto filtrando solo los ítems con estado activo y excluyendo aquellos que son padres de otros CPC. Se usa para presentar al usuario únicamente los CPC seleccionables o asignables en procesos presupuestales, evitando seleccionar categorías intermedias o agrupadores.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewListCPCCatalog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewListCPCCatalog';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los elementos hoja activos del catálogo CPC, es decir, aquellos que no son referenciados como padre por ningún otro registro activo.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListCPCCatalog';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla Budget.CPCCatalog debe contener la columna Status y la relación jerárquica CPCCatalogOwnerId para identificar la relación padre-hijo.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListCPCCatalog';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen registros con Status = 1 (activos).; Se excluyen los nodos que actúan como padres, es decir, aquellos cuyo Id aparece como CPCCatalogOwnerId de algún otro registro activo (nodos hoja únicamente).; La condición de ''padre'' se evalúa solo contra registros activos (Status = 1) con CPCCatalogOwnerId no nulo.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListCPCCatalog';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Catálogo CPC; Clasificación Central de Productos; Jerarquía de catálogo (padre/hijo)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListCPCCatalog';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Budget.CPCCatalog: Devuelve Id, Code y Name de los CPC con Status = 1 cuyo Id no aparece en la lista de CPCCatalogOwnerId de registros activos (filtra nodos padre, conservando solo hojas activas).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListCPCCatalog';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.CPCCatalog', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListCPCCatalog';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListCPCCatalog';
GO
