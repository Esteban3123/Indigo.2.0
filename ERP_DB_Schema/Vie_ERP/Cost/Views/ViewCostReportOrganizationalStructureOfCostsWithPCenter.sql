


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