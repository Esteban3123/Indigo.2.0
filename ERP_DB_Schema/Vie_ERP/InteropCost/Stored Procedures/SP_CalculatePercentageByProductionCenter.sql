-- =============================================
-- Author:		Juan F. Tamayo
-- Create date:	2016-10-05
-- Description:	Obtiene la cantidad de valores por centro de producción dependiendo del tipo de busqueda
-- =============================================
CREATE PROCEDURE [InteropCost].[SP_CalculatePercentageByProductionCenter]
	@pContainerNameDGEmpres varchar(50),
	@pDistributionBaseId int,
	@pTypeCalc int,
	@pYear int,
	@pMonth int
AS
BEGIN
	SET NOCOUNT ON;

    if @pTypeCalc = 1 begin --Por horas
	   declare @TotalHoursWorked int = (select sum(dmd.HoursQuantity)
	   from InteropCost.DistributionManpower dm
	   inner join InteropCost.DistributionManpowerDetail dmd on dmd.DistributionManpowerId = dm.Id
	   inner join InteropCost.DistributionBaseDetail sd on sd.ProductionCenterId = dmd.ProductionCenterId
	   where dm.Status = 1 And dm.Year = @pYear And dm.Month = @pMonth and sd.DistributionBaseId = @pDistributionBaseId)

	   select dmd.ProductionCenterId, cast((dmd.HoursQuantity * 100) / @TotalHoursWorked as decimal(18,2)) as Value, cast(@TotalHoursWorked as decimal(18,2)) as NetoValue
	   from InteropCost.DistributionManpower dm
	   inner join InteropCost.DistributionManpowerDetail dmd on dmd.DistributionManpowerId = dm.Id
	   inner join InteropCost.DistributionBaseDetail sd on sd.ProductionCenterId = dmd.ProductionCenterId
	   where dm.Status = 1 And dm.Year = @pYear And dm.Month = @pMonth and sd.DistributionBaseId = @pDistributionBaseId
    end
    if @pTypeCalc = 2 begin --Por suministro
	   declare @tmpMainAccountValuesSupply table (ProductionCenterId int , Value decimal(18,2))
	   delete from @tmpMainAccountValuesSupply
	   declare @SelectSupply nvarchar(max) = N'select pc.Id, ABS(COALEsCE(SUM([CSCDEBITO]) - sum([CSCCREDITO]),0))
	   from ' + @pContainerNameDGEmpres + '.dbo.CTNSAL' + cast(@pYear as varchar(4)) + ' as ctn 
	   inner join InteropCost.ProductionCenterHomologation as pch on pch.AccountOriginId = CTNCUENTA
	   inner join InteropCost.ProductionCenter as pc on pch.ProductionCenterId = pc.Id 
	   inner join (select distinct DistributionBaseId, ProductionCenterId from InteropCost.DistributionBaseDetail) as dbd on dbd.ProductionCenterId = pc.Id
	   inner join InteropCost.DistributionBase as db on db.Id = dbd.DistributionBaseId 
	   inner join InteropCost.GeneralExpense as ge on ge.Id = db.GeneralExpenseId 
	   where db.Id = ' + cast(@pDistributionBaseId as varchar(20)) + ' and pc.Status = 1 and pch.HomologationType = 2  and CTNCENCOS in (select  pccc.CostCenterId from  InteropCost.GeneralExpense as ge 
						  inner join InteropCost.DistributionBase as db on ge.Id = db.GeneralExpenseId 
						  inner join (select distinct DistributionBaseId, ProductionCenterId from InteropCost.DistributionBaseDetail) as dbd on db.Id = dbd.DistributionBaseId 
						  inner join InteropCost.ProductionCenter as pc on dbd.ProductionCenterId = pc.Id 
						  inner join InteropCost.ProductionCenterCostCenter as pccc on dbd.ProductionCenterId = pccc.ProductionCenterId 
						  where db.Id = ' + cast(@pDistributionBaseId as varchar(20)) + ' and pc.Status = 1)
		  and ctn.CSCMES = ' + cast(@pMonth as varchar(2)) + '
	   group by pc.Id'
	   insert into @tmpMainAccountValuesSupply
	   exec sp_executesql @SelectSupply

	   select ProductionCenterId, (Value * 100 / (select SUM(Value) from @tmpMainAccountValuesSupply)) as Value, Value as NetoValue
	   from @tmpMainAccountValuesSupply
    end
    if @pTypeCalc = 3 begin --Por mano de obra
	  declare @tmpMainAccountValuesWorkMan table (ProductionCenterId int , Value decimal(18,2))
	   delete from @tmpMainAccountValuesWorkMan
	   declare @SelectWorkMan nvarchar(max) =  N'select pc.Id, ABS(COALEsCE(SUM([CSCDEBITO]) - sum([CSCCREDITO]),0))
	   from ' + @pContainerNameDGEmpres + '.dbo.CTNSAL' + cast(@pYear as varchar(4)) + ' as ctn 
	   inner join InteropCost.ProductionCenterHomologation as pch on pch.AccountOriginId = CTNCUENTA
	   inner join InteropCost.ProductionCenter as pc on pch.ProductionCenterId = pc.Id 
	   inner join (select distinct DistributionBaseId, ProductionCenterId from InteropCost.DistributionBaseDetail) as dbd on dbd.ProductionCenterId = pc.Id
	   inner join InteropCost.DistributionBase as db on db.Id = dbd.DistributionBaseId 
	   inner join InteropCost.GeneralExpense as ge on ge.Id = db.GeneralExpenseId 
	   where db.Id = ' + cast(@pDistributionBaseId as varchar(20)) + ' and pc.Status = 1 and pch.HomologationType = 1  and CTNCENCOS in (select  pccc.CostCenterId from  InteropCost.GeneralExpense as ge 
						  inner join InteropCost.DistributionBase as db on ge.Id = db.GeneralExpenseId 
						  inner join (select distinct DistributionBaseId, ProductionCenterId from InteropCost.DistributionBaseDetail) as dbd on db.Id = dbd.DistributionBaseId 
						  inner join InteropCost.ProductionCenter as pc on dbd.ProductionCenterId = pc.Id 
						  inner join InteropCost.ProductionCenterCostCenter as pccc on dbd.ProductionCenterId = pccc.ProductionCenterId 
						  where db.Id = ' + cast(@pDistributionBaseId as varchar(20)) + ' and pc.Status = 1)
		  and ctn.CSCMES = ' + cast(@pMonth as varchar(2)) + '
	   group by pc.Id'
	   insert into @tmpMainAccountValuesWorkMan
	   exec sp_executesql @SelectWorkMan

	   select ProductionCenterId, (Value * 100 / (select SUM(Value) from @tmpMainAccountValuesWorkMan)) as Value, Value as NetoValue
	   from @tmpMainAccountValuesWorkMan
    end
    if @pTypeCalc = 4 begin --Por valor del activo
	   declare @tmpMainAccountValuesAsset table (ProductionCenterId int , Value decimal(18,2))
	   delete from @tmpMainAccountValuesAsset
	   declare @SelectAsset nvarchar(max) =  N'select pc.Id, ABS(COALEsCE(SUM([CSCDEBITO]) - sum([CSCCREDITO]),0))
	   from ' + @pContainerNameDGEmpres + '.dbo.CTNSAL' + cast(@pYear as varchar(4)) + ' as ctn 
	   inner join InteropCost.ProductionCenterHomologation as pch on pch.AccountOriginId = CTNCUENTA
	   inner join InteropCost.ProductionCenter as pc on pch.ProductionCenterId = pc.Id 
	   inner join (select distinct DistributionBaseId, ProductionCenterId from InteropCost.DistributionBaseDetail) as dbd on dbd.ProductionCenterId = pc.Id
	   inner join InteropCost.DistributionBase as db on db.Id = dbd.DistributionBaseId 
	   inner join InteropCost.GeneralExpense as ge on ge.Id = db.GeneralExpenseId 
	   where db.Id = ' + cast(@pDistributionBaseId as varchar(20)) + ' and pc.Status = 1 and pch.HomologationType = 5  and CTNCENCOS in (select  pccc.CostCenterId from  InteropCost.GeneralExpense as ge 
						  inner join InteropCost.DistributionBase as db on ge.Id = db.GeneralExpenseId 
						  inner join (select distinct DistributionBaseId, ProductionCenterId from InteropCost.DistributionBaseDetail) as dbd on db.Id = dbd.DistributionBaseId 
						  inner join InteropCost.ProductionCenter as pc on dbd.ProductionCenterId = pc.Id 
						  inner join InteropCost.ProductionCenterCostCenter as pccc on dbd.ProductionCenterId = pccc.ProductionCenterId 
						  where db.Id = ' + cast(@pDistributionBaseId as varchar(20)) + ' and pc.Status = 1)
		  and ctn.CSCMES = ' + cast(@pMonth as varchar(2)) + '
	   group by pc.Id'
	   insert into @tmpMainAccountValuesAsset
	   exec sp_executesql @SelectAsset

	   select ProductionCenterId, (Value * 100 / (select SUM(Value) from @tmpMainAccountValuesAsset)) as Value, Value as NetoValue
	   from @tmpMainAccountValuesAsset 
    end
    if @pTypeCalc = 5 begin --Por valor de venta
	  declare @tmpMainAccountValuesInvoice table (ProductionCenterId int , Value decimal(18,2))
	   delete from @tmpMainAccountValuesInvoice
	   declare @SelectInvoice nvarchar(max) =  N'select pc.Id, ABS(COALEsCE(SUM([CSCDEBITO]) - sum([CSCCREDITO]),0))
	   from ' + @pContainerNameDGEmpres + '.dbo.CTNSAL' + cast(@pYear as varchar(4)) + ' as ctn 
	   inner join InteropCost.ProductionCenterHomologation as pch on pch.AccountOriginId = CTNCUENTA
	   inner join InteropCost.ProductionCenter as pc on pch.ProductionCenterId = pc.Id 
	   inner join (select distinct DistributionBaseId, ProductionCenterId from InteropCost.DistributionBaseDetail) as dbd on dbd.ProductionCenterId = pc.Id
	   inner join InteropCost.DistributionBase as db on db.Id = dbd.DistributionBaseId 
	   inner join InteropCost.GeneralExpense as ge on ge.Id = db.GeneralExpenseId 
	   where db.Id = ' + cast(@pDistributionBaseId as varchar(20)) + ' and pc.Status = 1 and pch.HomologationType = 6  and CTNCENCOS in (select  pccc.CostCenterId from  InteropCost.GeneralExpense as ge 
						  inner join InteropCost.DistributionBase as db on ge.Id = db.GeneralExpenseId 
						  inner join (select distinct DistributionBaseId, ProductionCenterId from InteropCost.DistributionBaseDetail) as dbd on db.Id = dbd.DistributionBaseId 
						  inner join InteropCost.ProductionCenter as pc on dbd.ProductionCenterId = pc.Id 
						  inner join InteropCost.ProductionCenterCostCenter as pccc on dbd.ProductionCenterId = pccc.ProductionCenterId 
						  where db.Id = ' + cast(@pDistributionBaseId as varchar(20)) + ' and pc.Status = 1)
		  and ctn.CSCMES = ' + cast(@pMonth as varchar(2)) + '
	   group by pc.Id'
	   insert into @tmpMainAccountValuesInvoice
	   exec sp_executesql @SelectInvoice

	   select ProductionCenterId, (Value * 100 / (select SUM(Value) from @tmpMainAccountValuesInvoice)) as Value, Value as NetoValue
	   from @tmpMainAccountValuesInvoice 
    end

    select cast(0 as int) as ProductionCenterId, cast(0 as decimal(18,2)) as Value, cast(0 as decimal(18,2)) as NetoValue
END