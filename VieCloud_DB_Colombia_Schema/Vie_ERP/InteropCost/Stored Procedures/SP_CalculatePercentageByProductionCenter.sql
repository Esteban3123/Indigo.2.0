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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el porcentaje de participación de cada centro de producción dentro de una base de distribución de costos, para un mes y año determinados. Soporta cuatro métodos de cálculo: por horas trabajadas de mano de obra (usando las tablas de distribución de nómina), por valor de suministros, por valor de mano de obra contable, o por valor de activos (estos tres últimos consultando los saldos contables del libro mayor según el contenedor de la empresa configurada). El resultado es el porcentaje y el valor neto que le corresponde a cada centro de producción, insumo clave para el prorrateo y la imputación de gastos generales en el proceso de costeo por centros de producción.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_CalculatePercentageByProductionCenter';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_CalculatePercentageByProductionCenter';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el porcentaje de participación de cada centro de producción dentro de una base de distribución para un año/mes, según el tipo de cálculo seleccionado (horas, suministros, mano de obra, activos o ventas).', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculatePercentageByProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una DistributionBase activa identificada por el id recibido con detalle (DistributionBaseDetail) que liste los centros de producción a prorratear.; Para tipo 1 (horas): deben existir registros en DistributionManpower con Status=1 para el año y mes indicados, y detalle en DistributionManpowerDetail para los centros de producción de la base.; Para tipos 2,3,4 y 5: el contenedor (base de datos) recibido debe tener la tabla CTNSAL{año} con columnas CTNCUENTA, CTNCENCOS, CSCMES, CSCDEBITO, CSCCREDITO.; Los centros de producción referenciados deben tener Status=1.; Debe existir homologación en ProductionCenterHomologation con HomologationType correspondiente (2 suministros, 1 mano de obra, 5 activos, 6 ventas) que asocie cuentas contables (AccountOriginId) con centros de producción.; Los centros de costo deben estar asociados a los centros de producción vía ProductionCenterCostCenter.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculatePercentageByProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El nombre de la tabla contable consultada se construye dinámicamente concatenando el contenedor recibido con ''dbo.CTNSAL'' y el año (CTNSAL{@pYear}).; Solo se consideran centros de producción con Status=1.; El valor contable usado es siempre ABS(COALESCE(SUM(CSCDEBITO)-SUM(CSCCREDITO),0)), garantizando un monto no negativo aunque la cuenta sea de naturaleza crédito.; Para horas, solo se incluyen distribuciones de mano de obra con Status=1.; El porcentaje (Value) se expresa sobre 100 dividiendo el valor del centro entre la suma total del conjunto.; El centro de producción debe estar incluido en el detalle de la base de distribución (DistributionBaseDetail) para participar en el cálculo.; El filtro por mes en las consultas contables usa CTNSAL.CSCMES = @pMonth.; Las ramas no son mutuamente excluyentes mediante ELSE: si @pTypeCalc no coincide con ningún caso, sólo se devuelve la fila final con ceros.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculatePercentageByProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'centro de producción; centro de costo; base de distribución de costos; gasto general; homologación contable; distribución de mano de obra; horas trabajadas; suministros; valor del activo; valor de venta; saldo contable (débito-crédito); prorrateo de costos', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculatePercentageByProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Cuando @pTypeCalc=1, retorna ProductionCenterId, porcentaje (HoursQuantity*100/TotalHorasTrabajadas) y NetoValue=total de horas, sumando horas de DistributionManpowerDetail para registros con dm.Status=1, año y mes dados.; [RETURN_RESULT] Resultset: Cuando @pTypeCalc=2, retorna por centro de producción el valor absoluto del saldo (SUM(CSCDEBITO)-SUM(CSCCREDITO)) en CTNSAL{año} para el mes dado, filtrando por HomologationType=2 (suministros), y su porcentaje sobre la suma total.; [RETURN_RESULT] Resultset: Cuando @pTypeCalc=3, igual que el caso 2 pero filtrando ProductionCenterHomologation.HomologationType=1 (mano de obra).; [RETURN_RESULT] Resultset: Cuando @pTypeCalc=4, igual que el caso 2 pero filtrando ProductionCenterHomologation.HomologationType=5 (valor del activo).; [RETURN_RESULT] Resultset: Cuando @pTypeCalc=5, igual que el caso 2 pero filtrando ProductionCenterHomologation.HomologationType=6 (valor de venta).; [RETURN_RESULT] Resultset: Siempre, al final del procedimiento se emite además un resultset adicional con una fila (ProductionCenterId=0, Value=0, NetoValue=0) independientemente del tipo de cálculo.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculatePercentageByProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @pTypeCalc = 1 → Calcula distribución por horas trabajadas usando InteropCost.DistributionManpower y DistributionManpowerDetail filtrados por Status=1, año y mes.; si @pTypeCalc = 2 → Construye SQL dinámico contra {contenedor}.dbo.CTNSAL{año} filtrando HomologationType=2 (suministros) y calcula porcentaje por centro de producción.; si @pTypeCalc = 3 → Construye SQL dinámico contra CTNSAL{año} filtrando HomologationType=1 (mano de obra) y calcula porcentaje por centro de producción.; si @pTypeCalc = 4 → Construye SQL dinámico contra CTNSAL{año} filtrando HomologationType=5 (valor del activo) y calcula porcentaje por centro de producción.; si @pTypeCalc = 5 → Construye SQL dinámico contra CTNSAL{año} filtrando HomologationType=6 (valor de venta) y calcula porcentaje por centro de producción.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculatePercentageByProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.DistributionManpower; InteropCost.DistributionManpowerDetail; InteropCost.DistributionBaseDetail; InteropCost.DistributionBase; InteropCost.GeneralExpense; InteropCost.ProductionCenter; InteropCost.ProductionCenterHomologation; InteropCost.ProductionCenterCostCenter', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculatePercentageByProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculatePercentageByProductionCenter';
-- GO
