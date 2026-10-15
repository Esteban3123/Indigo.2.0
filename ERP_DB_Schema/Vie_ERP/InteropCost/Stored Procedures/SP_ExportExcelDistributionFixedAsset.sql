-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 08/11/2016
-- Description:	SP para exportar los datos de la distribucion de activos fijos
-- =============================================
CREATE PROCEDURE [InteropCost].[SP_ExportExcelDistributionFixedAsset]
	@Container varchar(20),
	@Year int,
	@Month int
AS
BEGIN
	
	declare @Sql nvarchar(max) = '
	select act.AACCODACT + '' - '' + pro.APRNOMBRE as Activo, cal.ACADEPMEN as Valor, cen.CCCODIGO + '' - '' + cen.CCNOMBRE as CentroCosto, pc.Code + '' - '' + pc.Name as CentroProduccion
	from '+ @Container +'.dbo.AFNCALDEP cal
	inner join '+ @Container +'.dbo.AFNACTIVO act on act.OID = cal.AFNACTIVO
	inner join '+ @Container +'.dbo.AFNPRODUC pro on pro.OID = act.AFNPRODUC
	inner join '+ @Container +'.dbo.CTNCENCOS cen on cen.OID = cal.CTNCENCOS
	inner join '+ @Container +'.dbo.AFNDEPRECI dep on dep.OID = cal.AFNDEPRECI
	left join InteropCost.ProductionCenterCostCenter pcc on pcc.[CostCenterId] = cal.CTNCENCOS
	left join InteropCost.ProductionCenter pc on pc.Id = pcc.[ProductionCenterId]
	where cal.ACADEPMEN > 0 and year(dep.ACAFECCHCI) = '+ cast(@Year as varchar(20)) +' and month(dep.ACAFECCHCI) = ' + cast(@Month as varchar(20))

	exec sp_sqlexec @Sql

	--select act.AACCODACT + ' - ' + pro.APRNOMBRE as Activo, cal.ACADEPMEN as Valor, cen.CCCODIGO + ' - ' + cen.CCNOMBRE as CentroCosto, pc.Code + ' - ' + pc.Name as CentroProduccion
	--from DGEMPRES03.dbo.AFNCALDEP cal
	--inner join DGEMPRES03.dbo.AFNACTIVO act on act.OID = cal.AFNACTIVO
	--inner join DGEMPRES03.dbo.AFNPRODUC pro on pro.OID = act.AFNPRODUC
	--inner join DGEMPRES03.dbo.CTNCENCOS cen on cen.OID = cal.CTNCENCOS
	--inner join DGEMPRES03.dbo.AFNDEPRECI dep on dep.OID = cal.AFNDEPRECI
	--left join InteropCost.ProductionCenterCostCenter pcc on pcc.[CostCenterId] = cal.CTNCENCOS
	--left join InteropCost.ProductionCenter pc on pc.Id = pcc.[ProductionCenterId]

END