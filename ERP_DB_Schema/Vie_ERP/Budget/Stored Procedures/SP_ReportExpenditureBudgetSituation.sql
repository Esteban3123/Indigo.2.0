

 CREATE PROCEDURE [Budget].[SP_ReportExpenditureBudgetSituation]
    @ValidityId as int,
	@CodeCategoryStart as varchar(20),
	@CodeCategoryEnd as varchar(20)

AS
BEGIN
	declare @yearValidity as integer = (Select [Year] from Budget.BudgetaryValidity where Id = @ValidityId)
	
	-- Creamos la tabla temporal que devolveremos con los datos
	declare @TableExecution table(BudgetId int,CategoryId int,CategoryCode varchar(40),CategoryName varchar(max),FinancialSourceCode varchar(20), FinancialSourceName varchar(max)
	, BudgetInitial decimal(18,0), RevenueTypeCode varchar(20), RevenueTypeName varchar(100)
	, BudgetTransferCredit decimal(18,0), BudgetTransferDebit decimal(18,0), BudgetModificationCredit decimal(18,0), BudgetModificationDebit decimal(18,0)
	, FinalBalance decimal(18,0), SuspensionValue decimal(18,0), AvailabilityValue decimal(18,0), AppropriationAvailableValue decimal(18,0))

	-- insertamos en la tabla temporal los datos del presupuesto inicial
	insert into @TableExecution 
	(BudgetId,CategoryId,CategoryCode,CategoryName, FinancialSourceCode, FinancialSourceName,BudgetInitial, RevenueTypeCode, RevenueTypeName
	, BudgetTransferCredit, BudgetTransferDebit, BudgetModificationCredit, BudgetModificationDebit, FinalBalance, SuspensionValue, AvailabilityValue, AppropriationAvailableValue)
	 Select b.Id, c.Id, c.Code, c.Name, fs.Code, fs.Name, b.InitialValue,rt.Code,rt.Name,0,0,0,0,0,0,0,0
	 From Budget.Budget b inner join Budget.Category c on c.Id = b.CategoryId
	 inner join Budget.FinancialSource fs on fs.Id = c.FinancialSourceId inner join Budget.RevenueType rt on rt.Id = b.RevenueTypeId 
	 inner join Budget.BudgetHeader bh on bh.Id = b.BudgetHeaderId where bh.BudgetaryValidityId = @ValidityId 
	 And c.Code >= ISNULL(@CodeCategoryStart,'0') And c.Code <= ISNULL(@CodeCategoryEnd,'99999999999999999999') And c.ItemType = 2

	 -- insertamos en la tabla temporal los datos del traslado de presupuesto 
	 update @TableExecution set BudgetTransferCredit = CreditValue
	 ,BudgetTransferDebit = DebitValue
	 from (select btd.BudgetId ,SUM(IIF(btd.Nature = 2, btd.Value, 0)) CreditValue, SUM(IIF(btd.Nature = 1, btd.Value, 0)) DebitValue
	 from Budget.BudgetTransferDetail btd 
	 inner join Budget.BudgetTransfer bt on bt.Id = btd.TransferId
	 where bt.BudgetaryValidityId = @ValidityId and bt.Status = 2 group by btd.BudgetId) as data
	 inner join @TableExecution te on data.BudgetId= te.BudgetId 

	 -- insertamos en la tabla temporal los datos de la modificacion del presupuesto
	 update @TableExecution set BudgetModificationCredit = CreditValue
	 ,BudgetModificationDebit = DebitValue
	 from (select bmd.BudgetId ,SUM(IIF(bmd.Nature = 2, bmd.Value, 0)) CreditValue, SUM(IIF(bmd.Nature = 1, bmd.Value, 0)) DebitValue
	 from Budget.BudgetModificationDetail bmd 
	 inner join Budget.BudgetModification bm on bm.Id = bmd.ModificationId
	 where bm.BudgetaryValidityId = @ValidityId and bm.Status = 2 group by bmd.BudgetId) as data
	 inner join @TableExecution te on data.BudgetId = te.BudgetId 

	 --- Insertamos las suspensiones
	update @TableExecution set SuspensionValue = isnull(Value,0)
	from (select sd.BudgetId, sum(sd.Balance) as Value
	from Budget.Suspension s
	inner join Budget.SuspensionDetail sd on sd.SuspensionId = s.Id
	where s.Status = 2 and s.BudgetaryValidityId = @ValidityId
	group by sd.BudgetId) as data
	inner join @TableExecution te on te.BudgetId = data.BudgetId

	 -- insertamos en la tabla temporal los datos de los movimientos de Disponibilidad en los meses anteriores al seleccionado------
	 update @TableExecution set AvailabilityValue = isnull(Value,0)
	 FROM (SELECT cd.BudgetId,(sum(cd.InitialValue)) Value
	 From Budget.AvailabilityDetail cd 
	 inner join Budget.[Availability] c on c.Id = cd.AvailabilityId
	 inner join @TableExecution te on te.BudgetId = cd.BudgetId
	 where c.BudgetaryValidityId = @ValidityId And c.Status = 2
	 group by cd.BudgetId) as data
	 inner join @TableExecution te on te.BudgetId = data.BudgetId
	 
	 -- insertamos en la tabla temporal los datos de las modificaciones de la disponibilidad
	 update @TableExecution set AvailabilityValue += isnull(Value,0)
	 from (SELECT cd.BudgetId,(ISNULL(SUM(IIF(cmd.Nature = 2, cmd.Value, 0)),0) - ISNULL(SUM(IIF(cmd.Nature = 1, cmd.Value, 0)),0)) Value
	 From Budget.AvailabilityModificationDetail cmd
	 inner join Budget.AvailabilityDetail cd on cd.Id = cmd.AvailabilityDetailId
	 inner join Budget.AvailabilityModification cm on cm.Id = cmd.AvailabilityModificationId
	 inner join @TableExecution te on te.BudgetId = cd.BudgetId
	 where cm.BudgetaryValidityId = @ValidityId And cm.Status = 2
	 group by cd.BudgetId) as data
	 inner join @TableExecution te on te.BudgetId = data.BudgetId
	 
	 ------Actualizo los datos 
	 update @TableExecution set FinalBalance = (BudgetInitial - BudgetTransferDebit - BudgetModificationDebit + BudgetTransferCredit + BudgetModificationCredit)
	 update @TableExecution set AppropriationAvailableValue = FinalBalance - SuspensionValue - AvailabilityValue

	 select * from @TableExecution

END