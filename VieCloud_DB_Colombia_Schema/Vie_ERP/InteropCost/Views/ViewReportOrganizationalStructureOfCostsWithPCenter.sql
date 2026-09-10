

CREATE VIEW [InteropCost].[ViewReportOrganizationalStructureOfCostsWithPCenter]
AS

--select ROW_NUMBER() OVER(ORDER BY id ASC) as id, Code,[Name],ParentId,[Status],CenterType from (
select Distinct id, Code,[Name],ParentId,[Status] from (
select os.id,os.Code,os.[Name],os.ParentId,os.[Status] from InteropCost.ProductionCenter as pc
inner join InteropCost.OrganizationalStructureOfCosts as os on os.Id = pc.OrganizationalStructureOfCostId
where  pc.CenterType = 1
union ALL
--Nombres padres Obtenidos de los ParentId de los Hijos
select id,Code,[Name],ParentId,[Status] from InteropCost.OrganizationalStructureOfCosts where id in (select distinct ParentId from InteropCost.ProductionCenter as pc
inner join InteropCost.OrganizationalStructureOfCosts as os on os.Id = pc.OrganizationalStructureOfCostId
where  pc.CenterType = 1)
union ALL
--Nombres padres Obtenidos de los padres de los Hijos
select id,Code,[Name],ParentId,[Status] from InteropCost.OrganizationalStructureOfCosts where id in (select distinct ParentId from InteropCost.OrganizationalStructureOfCosts where id in (select distinct ParentId from InteropCost.ProductionCenter as pc
inner join InteropCost.OrganizationalStructureOfCosts as os on os.Id = pc.OrganizationalStructureOfCostId
where  pc.CenterType = 1))
union ALL
--Para Agregar el Parent Principal
select id,Code,[Name],ParentId,[Status] from InteropCost.OrganizationalStructureOfCosts where id in(select ParentId from InteropCost.OrganizationalStructureOfCosts where id in (select distinct ParentId from InteropCost.OrganizationalStructureOfCosts where id in (select distinct ParentId from InteropCost.ProductionCenter as pc
inner join InteropCost.OrganizationalStructureOfCosts as os on os.Id = pc.OrganizationalStructureOfCostId
where  pc.CenterType = 1)))
) as Datos
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la jerarquía completa de la estructura organizacional de costos asociada a centros de producción activos (tipo 1), incluyendo hasta cuatro niveles hacia arriba en el árbol padre-hijo. Integra los centros de producción con su nodo directo en la estructura de costos y remonta recursivamente los nodos padres hasta el nivel raíz, garantizando que el árbol quede completo para reportería. Se usa en informes financieros y de distribución de costos para mostrar la estructura organizacional completa de centros de costo, áreas y departamentos vinculados a centros productivos de la institución.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'VIEW', @level1name = N'ViewReportOrganizationalStructureOfCostsWithPCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'VIEW', @level1name = N'ViewReportOrganizationalStructureOfCostsWithPCenter';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la estructura organizacional de costos restringida a las ramas jerárquicas que contienen centros de producción (CenterType=1), incluyendo sus nodos hoja y hasta tres niveles de ancestros para reportes.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportOrganizationalStructureOfCostsWithPCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir relación entre InteropCost.ProductionCenter.OrganizationalStructureOfCostId e InteropCost.OrganizationalStructureOfCosts.Id.; Deben existir registros en ProductionCenter con CenterType = 1 para que la vista retorne datos.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportOrganizationalStructureOfCostsWithPCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen ramas jerárquicas asociadas a centros de producción cuyo CenterType = 1.; El conjunto resultante se aplana hasta tres niveles ascendentes de padres más el nodo hoja vinculado al ProductionCenter, garantizando incluir el padre principal de la jerarquía.; El uso de SELECT DISTINCT sobre la unión asegura que cada nodo de la estructura aparezca una sola vez aunque sea alcanzable por múltiples ramas.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportOrganizationalStructureOfCostsWithPCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Estructura organizacional de costos; Jerarquía padre-hijo de centros de costo; Tipo de centro', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportOrganizationalStructureOfCostsWithPCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] InteropCost.OrganizationalStructureOfCosts: Devuelve nodos de OrganizationalStructureOfCosts vinculados directamente a ProductionCenter con CenterType=1 (rama hoja).; [RETURN_RESULT] InteropCost.OrganizationalStructureOfCosts: Agrega los padres directos (ParentId) de los nodos hoja anteriores.; [RETURN_RESULT] InteropCost.OrganizationalStructureOfCosts: Agrega los abuelos (padres de los padres) de los nodos hoja, ampliando la jerarquía hacia arriba.; [RETURN_RESULT] InteropCost.OrganizationalStructureOfCosts: Agrega un nivel adicional de ancestros para asegurar la inclusión del ''Parent Principal'' de la jerarquía.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportOrganizationalStructureOfCostsWithPCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.ProductionCenter; InteropCost.OrganizationalStructureOfCosts', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportOrganizationalStructureOfCostsWithPCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportOrganizationalStructureOfCostsWithPCenter';
GO
