

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de situación presupuestal de gastos para una vigencia presupuestal determinada. Consolida, por rubro presupuestal (categoría) y fuente de financiación, el presupuesto inicial de gastos junto con los traslados (créditos y débitos), modificaciones presupuestales, suspensiones, y disponibilidades presupuestales aprobadas, calculando el saldo final y el saldo de apropiación disponible. Se utiliza para la toma de decisiones financieras y el control de ejecución presupuestal de egresos, permitiendo filtrar por rango de códigos de categoría y por vigencia presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportExpenditureBudgetSituation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportExpenditureBudgetSituation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de la situación de ejecución del presupuesto de gastos para una vigencia, consolidando valor inicial, traslados, modificaciones, suspensiones y disponibilidades por rubro presupuestal.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenditureBudgetSituation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La vigencia presupuestal indicada debe existir en Budget.BudgetaryValidity.; Los rangos de código de categoría son opcionales: si son nulos se asume ''0'' como mínimo y ''99999999999999999999'' como máximo.; Solo se consideran categorías cuyo ItemType = 2 (rubros de gasto).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenditureBudgetSituation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran traslados, modificaciones, suspensiones, disponibilidades y modificaciones de disponibilidad con Status = 2 (estado aprobado/oficial).; El reporte se restringe siempre a categorías de gasto (ItemType = 2).; Todos los movimientos se filtran por la misma BudgetaryValidityId recibida.; Nature=1 representa movimiento de débito y Nature=2 representa movimiento de crédito en todos los detalles de movimiento.; El valor de apropiación disponible nunca incluye recursos suspendidos ni recursos ya comprometidos en disponibilidades.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenditureBudgetSituation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Vigencia presupuestal; Presupuesto de gastos; Categoría/rubro presupuestal; Fuente de financiamiento; Tipo de ingreso/renta; Traslado presupuestal; Modificación presupuestal; Suspensión presupuestal; Disponibilidad presupuestal (CDP); Modificación de disponibilidad; Saldo final de apropiación; Apropiación disponible', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenditureBudgetSituation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableExecution: Se cargan los presupuestos cuya BudgetHeader pertenece a la vigencia recibida, con Category.ItemType=2 y Code dentro del rango [@CodeCategoryStart,@CodeCategoryEnd], inicializando contadores en 0.; [UPDATE] @TableExecution: Por cada BudgetId, BudgetTransferCredit acumula SUM(Value) de BudgetTransferDetail con Nature=2 y BudgetTransferDebit con Nature=1, considerando solo BudgetTransfer.Status=2 de la vigencia.; [UPDATE] @TableExecution: Por cada BudgetId, BudgetModificationCredit acumula SUM(Value) de BudgetModificationDetail con Nature=2 y BudgetModificationDebit con Nature=1, considerando solo BudgetModification.Status=2 de la vigencia.; [UPDATE] @TableExecution: SuspensionValue se actualiza con SUM(SuspensionDetail.Balance) por BudgetId, solo de Suspension.Status=2 de la vigencia.; [UPDATE] @TableExecution: AvailabilityValue se inicializa con SUM(AvailabilityDetail.InitialValue) por BudgetId, considerando solo Availability.Status=2 de la vigencia.; [UPDATE] @TableExecution: AvailabilityValue se incrementa con (SUM(IIF(Nature=2,Value,0)) - SUM(IIF(Nature=1,Value,0))) de AvailabilityModificationDetail, considerando solo AvailabilityModification.Status=2 de la vigencia.; [UPDATE] @TableExecution: FinalBalance = BudgetInitial - BudgetTransferDebit - BudgetModificationDebit + BudgetTransferCredit + BudgetModificationCredit.; [UPDATE] @TableExecution: AppropriationAvailableValue = FinalBalance - SuspensionValue - AvailabilityValue.; [RETURN_RESULT] @TableExecution: Devuelve todas las columnas de la tabla temporal con la situación presupuestal consolidada por rubro.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenditureBudgetSituation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si BudgetTransferDetail.Nature = 2 → Se suma como crédito en BudgetTransferCredit else Si Nature = 1 se suma como débito en BudgetTransferDebit; si BudgetModificationDetail.Nature = 2 → Se suma como crédito en BudgetModificationCredit else Si Nature = 1 se suma como débito en BudgetModificationDebit; si AvailabilityModificationDetail.Nature = 2 → Suma al AvailabilityValue (incremento de disponibilidad) else Si Nature = 1 resta al AvailabilityValue (reducción); si @CodeCategoryStart o @CodeCategoryEnd son NULL → Se sustituyen por ''0'' y ''99999999999999999999'' respectivamente para no acotar el rango', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenditureBudgetSituation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.BudgetaryValidity; Budget.Budget; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Budget.BudgetHeader; Budget.BudgetTransferDetail; Budget.BudgetTransfer; Budget.BudgetModificationDetail; Budget.BudgetModification; Budget.Suspension; Budget.SuspensionDetail; Budget.AvailabilityDetail; Budget.Availability; Budget.AvailabilityModificationDetail; Budget.AvailabilityModification', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenditureBudgetSituation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenditureBudgetSituation';
-- GO
