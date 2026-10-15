

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