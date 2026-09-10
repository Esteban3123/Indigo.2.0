
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el porcentaje de participación de cada centro de producción en la distribución de costos indirectos, según el tipo de base de cálculo seleccionado: horas trabajadas de mano de obra, valor de suministros, valor de mano de obra por contabilidad, o valor de activos fijos. Para cada tipo, totaliza el valor o cantidad del período indicado (año y mes) asociado a una base de distribución específica, y devuelve el porcentaje proporcional de cada centro de producción junto con el valor neto total. Es el motor central del prorrateo de costos indirectos entre unidades productivas, consumiendo los registros de distribución de mano de obra, los detalles de bases de distribución y los saldos contables homologados por centro de producción.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CalculatePercentageByCostProductionCenter';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CalculatePercentageByCostProductionCenter';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el porcentaje de participación de cada centro de producción sobre una base de distribución de costos, según el tipo de cálculo solicitado (horas, suministros, mano de obra, activos o ventas).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculatePercentageByCostProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una base de distribución vigente identificada por el parámetro de entrada; Para cálculo por horas, los registros de distribución de mano de obra deben tener Status=1 y coincidir con el año y mes solicitados; Para cálculos contables (suministros, mano de obra, activos, ventas) deben existir homologaciones de cuentas en CostProductionCenterHomologation con HomologationType correspondiente (2, 1, 5, 6); Los centros de producción involucrados deben estar activos (pc.Status=1); Debe existir saldos en GeneralLedgerBalance para el período (Month/Year) consultado', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculatePercentageByCostProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor neto contable se calcula siempre como valor absoluto de (SUM(Debe) - SUM(Haber)); Los porcentajes se calculan sobre el total acumulado del conjunto filtrado (Value*100/SUM(Value)); Solo se consideran centros de producción activos (Status=1); Los cálculos contables solo incluyen centros de costo asociados al centro de producción vía CostProductionCenterCostCenter; Siempre se emite una fila final con ceros como cierre del resultset; El cálculo por horas exige Status=1 en la cabecera de distribución de mano de obra', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculatePercentageByCostProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Centro de costo; Base de distribución de costos; Gasto general; Distribución de mano de obra; Horas trabajadas; Homologación de cuentas contables; Saldo contable (debe/haber); Suministros; Activos; Ventas; Período contable (mes/año)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculatePercentageByCostProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Cuando @pTypeCalc=1 → retorna por cada centro de producción el porcentaje (HoursQuantity*100/total horas trabajadas) y el total de horas como NetoValue, filtrando por Status=1, Year y Month; [RETURN_RESULT] RESULTSET: Cuando @pTypeCalc=2 → retorna porcentaje basado en saldo contable (|Debe-Haber|) de cuentas con HomologationType=2 (suministros) para el mes/año dados; [RETURN_RESULT] RESULTSET: Cuando @pTypeCalc=3 → retorna porcentaje basado en saldo contable (|Debe-Haber|) de cuentas con HomologationType=1 (mano de obra) para el mes/año dados; [RETURN_RESULT] RESULTSET: Cuando @pTypeCalc=4 → retorna porcentaje basado en saldo contable (|Debe-Haber|) de cuentas con HomologationType=5 (activos) para el mes/año dados; [RETURN_RESULT] RESULTSET: Cuando @pTypeCalc=5 → arma consulta con HomologationType=6 (ventas) pero los filtros están fijados a db.Id=0, ctn.Month=0 y ctn.Year=0, por lo que en la práctica no retorna datos contables válidos (posible bug); [RETURN_RESULT] RESULTSET: Independiente del tipo de cálculo, siempre se retorna adicionalmente una fila final fija con ProductionCenterId=0, Value=0 y NetoValue=0', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculatePercentageByCostProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @pTypeCalc = 1 → Calcula distribución porcentual basada en horas trabajadas (CostDistributionManpowerDetail.HoursQuantity); si @pTypeCalc = 2 → Calcula distribución porcentual basada en saldos contables de cuentas homologadas como suministros (HomologationType=2); si @pTypeCalc = 3 → Calcula distribución porcentual basada en saldos contables de cuentas homologadas como mano de obra (HomologationType=1); si @pTypeCalc = 4 → Calcula distribución porcentual basada en saldos contables de cuentas homologadas como activos (HomologationType=5); si @pTypeCalc = 5 → Calcula distribución porcentual basada en cuentas homologadas como ventas (HomologationType=6) pero con filtros hardcodeados a 0', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculatePercentageByCostProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostDistributionManpower; Cost.CostDistributionManpowerDetail; Cost.CostDistributionBaseDetail; Cost.CostDistributionBase; Cost.CostGeneralExpense; Cost.CostProductionCenter; Cost.CostProductionCenterHomologation; Cost.CostProductionCenterCostCenter; GeneralLedger.GeneralLedgerBalance', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculatePercentageByCostProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculatePercentageByCostProductionCenter';
-- GO
