CREATE view [InteropCost].[ViewProductionCenterCCLogistic]
	as
select distinct(pc.Id), pc.Code, pc.Name, pc.Status, pc.CenterType from [InteropCost].[LogisticsProductionCenterRecordDetail] as lpc inner join
[InteropCost].[ProductionCenter] as pc on lpc.ProductionCenterId = pc.Id