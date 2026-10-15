
-- =============================================
-- Author:		Juan F. Tamayo
-- Create date:	2016-10-05
-- Description:	Obtiene la cantidad de valores por centro de producción dependiendo del tipo de busqueda
-- =============================================
CREATE PROCEDURE [Cost].[SP_CalculatePercentageByCostProductionCenter]
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
	   from Cost.CostDistributionManpower dm
	   inner join Cost.CostDistributionManpowerDetail dmd on dmd.DistributionManpowerId = dm.Id
	   inner join Cost.CostDistributionBaseDetail sd on sd.ProductionCenterId = dmd.ProductionCenterId
	   where dm.Status = 1 And dm.Year = @pYear And dm.Month = @pMonth and sd.DistributionBaseId = @pDistributionBaseId)

	   select dmd.ProductionCenterId, cast((dmd.HoursQuantity * 100) / @TotalHoursWorked as decimal(18,2)) as Value, cast(@TotalHoursWorked as decimal(18,2)) as NetoValue
	   from Cost.CostDistributionManpower dm
	   inner join Cost.CostDistributionManpowerDetail dmd on dmd.DistributionManpowerId = dm.Id
	   inner join Cost.CostDistributionBaseDetail sd on sd.ProductionCenterId = dmd.ProductionCenterId
	   where dm.Status = 1 And dm.Year = @pYear And dm.Month = @pMonth and sd.DistributionBaseId = @pDistributionBaseId
    end
    if @pTypeCalc = 2 begin --Por suministro
	   declare @tmpMainAccountValuesSupply table (ProductionCenterId int , Value decimal(18,2))
	   delete from @tmpMainAccountValuesSupply

	   insert into @tmpMainAccountValuesSupply
	   select pc.Id, ABS(COALEsCE(SUM(ctn.DebitValue) - sum(ctn.CreditValue),0))
		from GeneralLedger.GeneralLedgerBalance as ctn 
		inner join Cost.CostProductionCenterHomologation as pch on pch.AccountOriginId = IdMainAccount
		inner join Cost.CostProductionCenter as pc on pch.ProductionCenterId = pc.Id 
		inner join (select distinct DistributionBaseId, ProductionCenterId from Cost.CostDistributionBaseDetail) as dbd on dbd.ProductionCenterId = pc.Id
		inner join Cost.CostDistributionBase as db on db.Id = dbd.DistributionBaseId 
		inner join Cost.CostGeneralExpense as ge on ge.Id = db.GeneralExpenseId 
		where db.Id = @pDistributionBaseId and pc.Status = 1 and pch.HomologationType = 2  and IdCostCenter in (select  pccc.CostCenterId from  Cost.CostGeneralExpense as ge 
		inner join Cost.CostDistributionBase as db on ge.Id = db.GeneralExpenseId 
		inner join (select distinct DistributionBaseId, ProductionCenterId from Cost.CostDistributionBaseDetail) as dbd on db.Id = dbd.DistributionBaseId 
		inner join Cost.CostProductionCenter as pc on dbd.ProductionCenterId = pc.Id 
		inner join Cost.CostProductionCenterCostCenter as pccc on dbd.ProductionCenterId = pccc.ProductionCenterId 
		where db.Id = @pDistributionBaseId and pc.Status = 1)
		and ctn.Month = @pMonth and ctn.Year = @pYear
		group by pc.Id

	   select ProductionCenterId, (Value * 100 / (select SUM(Value) from @tmpMainAccountValuesSupply)) as Value, Value as NetoValue
	   from @tmpMainAccountValuesSupply
    end
    if @pTypeCalc = 3 begin --Por mano de obra
	  declare @tmpMainAccountValuesWorkMan table (ProductionCenterId int , Value decimal(18,2))
	   delete from @tmpMainAccountValuesWorkMan

	   insert into @tmpMainAccountValuesWorkMan
	   select pc.Id, ABS(COALEsCE(SUM(ctn.DebitValue) - sum(ctn.CreditValue),0))
		from GeneralLedger.GeneralLedgerBalance as ctn 
		inner join Cost.CostProductionCenterHomologation as pch on pch.AccountOriginId = IdMainAccount
		inner join Cost.CostProductionCenter as pc on pch.ProductionCenterId = pc.Id 
		inner join (select distinct DistributionBaseId, ProductionCenterId from Cost.CostDistributionBaseDetail) as dbd on dbd.ProductionCenterId = pc.Id
		inner join Cost.CostDistributionBase as db on db.Id = dbd.DistributionBaseId 
		inner join Cost.CostGeneralExpense as ge on ge.Id = db.GeneralExpenseId 
		where db.Id = @pDistributionBaseId and pc.Status = 1 and pch.HomologationType = 1  and IdCostCenter in (select  pccc.CostCenterId from  Cost.CostGeneralExpense as ge 
		inner join Cost.CostDistributionBase as db on ge.Id = db.GeneralExpenseId 
		inner join (select distinct DistributionBaseId, ProductionCenterId from Cost.CostDistributionBaseDetail) as dbd on db.Id = dbd.DistributionBaseId 
		inner join Cost.CostProductionCenter as pc on dbd.ProductionCenterId = pc.Id 
		inner join Cost.CostProductionCenterCostCenter as pccc on dbd.ProductionCenterId = pccc.ProductionCenterId 
		where db.Id = @pDistributionBaseId and pc.Status = 1)
		and ctn.Month = @pMonth and ctn.Year = @pYear
		group by pc.Id

	   select ProductionCenterId, (Value * 100 / (select SUM(Value) from @tmpMainAccountValuesWorkMan)) as Value, Value as NetoValue
	   from @tmpMainAccountValuesWorkMan
    end
    if @pTypeCalc = 4 begin --Por valor del activo
	   declare @tmpMainAccountValuesAsset table (ProductionCenterId int , Value decimal(18,2))
	   delete from @tmpMainAccountValuesAsset
	   
	   insert into @tmpMainAccountValuesAsset
	   select pc.Id, ABS(COALEsCE(SUM(ctn.DebitValue) - sum(ctn.CreditValue),0))
		from GeneralLedger.GeneralLedgerBalance as ctn 
		inner join Cost.CostProductionCenterHomologation as pch on pch.AccountOriginId = IdMainAccount
		inner join Cost.CostProductionCenter as pc on pch.ProductionCenterId = pc.Id 
		inner join (select distinct DistributionBaseId, ProductionCenterId from Cost.CostDistributionBaseDetail) as dbd on dbd.ProductionCenterId = pc.Id
		inner join Cost.CostDistributionBase as db on db.Id = dbd.DistributionBaseId 
		inner join Cost.CostGeneralExpense as ge on ge.Id = db.GeneralExpenseId 
		where db.Id = @pDistributionBaseId and pc.Status = 1 and pch.HomologationType = 5  and IdCostCenter in (select  pccc.CostCenterId from  Cost.CostGeneralExpense as ge 
		inner join Cost.CostDistributionBase as db on ge.Id = db.GeneralExpenseId 
		inner join (select distinct DistributionBaseId, ProductionCenterId from Cost.CostDistributionBaseDetail) as dbd on db.Id = dbd.DistributionBaseId 
		inner join Cost.CostProductionCenter as pc on dbd.ProductionCenterId = pc.Id 
		inner join Cost.CostProductionCenterCostCenter as pccc on dbd.ProductionCenterId = pccc.ProductionCenterId 
		where db.Id = @pDistributionBaseId and pc.Status = 1)
		and ctn.Month = @pMonth and ctn.Year = @pYear
		group by pc.Id

	   select ProductionCenterId, (Value * 100 / (select SUM(Value) from @tmpMainAccountValuesAsset)) as Value, Value as NetoValue
	   from @tmpMainAccountValuesAsset 
    end
    if @pTypeCalc = 5 begin --Por valor de venta
	  declare @tmpMainAccountValuesInvoice table (ProductionCenterId int , Value decimal(18,2))
	   delete from @tmpMainAccountValuesInvoice
	   
	   insert into @tmpMainAccountValuesInvoice
	   select pc.Id, ABS(COALEsCE(SUM(ctn.DebitValue) - sum(ctn.CreditValue),0))
		from GeneralLedger.GeneralLedgerBalance as ctn 
		inner join Cost.CostProductionCenterHomologation as pch on pch.AccountOriginId = IdMainAccount
		inner join Cost.CostProductionCenter as pc on pch.ProductionCenterId = pc.Id 
		inner join (select distinct DistributionBaseId, ProductionCenterId from Cost.CostDistributionBaseDetail) as dbd on dbd.ProductionCenterId = pc.Id
		inner join Cost.CostDistributionBase as db on db.Id = dbd.DistributionBaseId 
		inner join Cost.CostGeneralExpense as ge on ge.Id = db.GeneralExpenseId 
		where db.Id = 0 and pc.Status = 1 and pch.HomologationType = 6  and IdCostCenter in (select  pccc.CostCenterId from  Cost.CostGeneralExpense as ge 
		inner join Cost.CostDistributionBase as db on ge.Id = db.GeneralExpenseId 
		inner join (select distinct DistributionBaseId, ProductionCenterId from Cost.CostDistributionBaseDetail) as dbd on db.Id = dbd.DistributionBaseId 
		inner join Cost.CostProductionCenter as pc on dbd.ProductionCenterId = pc.Id 
		inner join Cost.CostProductionCenterCostCenter as pccc on dbd.ProductionCenterId = pccc.ProductionCenterId 
		where db.Id = 0 and pc.Status = 1)
		and ctn.Month = 0 and ctn.Year = 0
		group by pc.Id

	   select ProductionCenterId, (Value * 100 / (select SUM(Value) from @tmpMainAccountValuesInvoice)) as Value, Value as NetoValue
	   from @tmpMainAccountValuesInvoice 
    end

    select cast(0 as int) as ProductionCenterId, cast(0 as decimal(18,2)) as Value, cast(0 as decimal(18,2)) as NetoValue
END