

CREATE VIEW [Cost].[ViewCostReportOrganizationalStructureOfCostsWithPCenter]
AS

----select ROW_NUMBER() OVER(ORDER BY id ASC) as id, Code,[Name],ParentId,[Status],CenterType from (
--select Distinct id, Code,[Name],ParentId,[Status] from (
--select os.id,os.Code,os.[Name],os.ParentId,os.[Status] from Cost.CostProductionCenter as pc
--inner join Cost.CostOrganizationalStructureOfCosts as os on os.Id = pc.OrganizationalStructureOfCostId
--where  pc.CenterType = 1
--union ALL
----Nombres padres Obtenidos de los ParentId de los Hijos
--select id,Code,[Name],ParentId,[Status] from InteropCost.OrganizationalStructureOfCosts where id in (select distinct ParentId from Cost.CostProductionCenter as pc
--inner join Cost.CostOrganizationalStructureOfCosts as os on os.Id = pc.OrganizationalStructureOfCostId
--where  pc.CenterType = 1)
--union ALL
----Nombres padres Obtenidos de los padres de los Hijos
--select id,Code,[Name],ParentId,[Status] from InteropCost.OrganizationalStructureOfCosts where id in (select distinct ParentId from Cost.CostOrganizationalStructureOfCosts where id in (select distinct ParentId from Cost.CostProductionCenter as pc
--inner join Cost.CostOrganizationalStructureOfCosts as os on os.Id = pc.OrganizationalStructureOfCostId
--where  pc.CenterType = 1))
--union ALL
----Para Agregar el Parent Principal
--select id,Code,[Name],ParentId,[Status] from Cost.CostOrganizationalStructureOfCosts where id in(select ParentId from Cost.CostOrganizationalStructureOfCosts where id in (select distinct ParentId from Cost.CostOrganizationalStructureOfCosts where id in (select distinct ParentId from Cost.CostProductionCenter as pc
--inner join Cost.CostOrganizationalStructureOfCosts as os on os.Id = pc.OrganizationalStructureOfCostId
--where  pc.CenterType = 1)))
--) as Datos

select Id,Code,Name,ParentId,Status from Cost.CostOrganizationalStructureOfCosts
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que expone la estructura organizacional de costos completa, incluyendo todos los niveles jerárquicos (nodos hijos y padres) de la organización de costos. Está diseñada para reportes de costos que requieren visualizar la jerarquía de centros de costo, mostrando el identificador, código, nombre, nodo padre y estado de cada elemento. Aunque el código comentado filtraba únicamente los elementos relacionados con centros de producción (CenterType = 1) y reconstruía su árbol jerárquico completo hacia arriba, la versión activa retorna toda la estructura desde la tabla CostOrganizationalStructureOfCosts sin filtro. Se utiliza como fuente base para reportes de estructura de costos organizacional con centros de producción en el módulo de costos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewCostReportOrganizationalStructureOfCostsWithPCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewCostReportOrganizationalStructureOfCostsWithPCenter';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la estructura organizacional de costos (jerarquía de centros de costo) para reportes de costos vinculados a centros de producción.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportOrganizationalStructureOfCostsWithPCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La vista expone únicamente datos de la estructura organizacional de costos, sin filtrar por estado ni por tipo de centro.; Mantiene la relación jerárquica padre-hijo a través del identificador del padre.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportOrganizationalStructureOfCostsWithPCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estructura organizacional de costos; Centros de costo; Jerarquía de centros; Reporte de costos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportOrganizationalStructureOfCostsWithPCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Cost.CostOrganizationalStructureOfCosts: Devuelve todos los registros de la estructura organizacional de costos con sus atributos de identificación, código, nombre, padre y estado.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportOrganizationalStructureOfCostsWithPCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostOrganizationalStructureOfCosts', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportOrganizationalStructureOfCostsWithPCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportOrganizationalStructureOfCostsWithPCenter';
GO
