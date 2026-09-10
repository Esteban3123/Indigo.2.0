	CREATE PROCEDURE [Budget].[ExecutionByCategoryThird]

	@DateStart AS DATE,
	@DateEnd AS DATE,
	@CategoryStart AS VARCHAR(20),
	@CategoryEnd AS VARCHAR(20)

	AS
	BEGIN
	declare @TableExecution table(
	BudgetId int,
	CategoryId int, 
	CategoryCode varchar(20),
	CategoryName varchar(max),
	BudgetInitial decimal(18,0),
	BudgetTransferCredit decimal(18,0),
	BudgetTransferDebit decimal(18,0),
	BudgetModificationCredit decimal(18,0),
	BudgetModificationDebit decimal(18,0),
	[AvailabilityCode] varchar(20),
	AvailabilityTyp varchar(50),
	AvailabilityModificationCode varchar(20),
	CommitmentCode varchar(20),
	CommitmentTyp varchar(50),
	ObligationCode varchar(20),
	ObligationTyp varchar(50)
	)
	
	insert into @TableExecution 
	(BudgetId,CategoryId,CategoryCode,CategoryName,BudgetInitial)
	 Select b.Id, c.Id, c.Code, c.Name, b.InitialValue From Budget.Budget b inner join Budget.Category c on c.Id = b.CategoryId
	 inner join Budget.FinancialSource fs on fs.Id = c.FinancialSourceId inner join Budget.RevenueType rt on rt.Id = b.RevenueTypeId 
	 inner join Budget.BudgetHeader bh on bh.Id = b.BudgetHeaderId /*where bh.BudgetaryValidityId = @ValidityId 
	 And fs.Code >= ISNULL(@FinancialSourceCodeStart,'0') AND fs.Code <= ISNULL(@FinancialSourceCodeEnd, '99999999999999999999') 
	 And c.Code >= ISNULL(@CodeCategoryStart,'0') And c.Code <= ISNULL(@CodeCategoryEnd,'99999999999999999999') And c.ItemType = 1*/

 	 -- insertamos en la tabla temporal los datos del traslado de presupuesto 
	 update @TableExecution set BudgetTransferCredit = CreditValue
	 ,BudgetTransferDebit = DebitValue
	 from (select btd.BudgetId ,SUM(IIF(btd.Nature = 2, btd.Value, 0)) CreditValue, SUM(IIF(btd.Nature = 1, btd.Value, 0)) DebitValue
	 from Budget.BudgetTransferDetail btd 
	 inner join Budget.BudgetTransfer bt on bt.Id = btd.TransferId
	 /*where cast(bt.DocumentDate as date) BETWEEN @InitialDate And @EndDate*/ group by btd.BudgetId) as data
	 inner join @TableExecution te on data.BudgetId= te.BudgetId 
	

		 -- insertamos en la tabla temporal los datos de la modificacion del presupuesto
	 update @TableExecution set BudgetModificationCredit = CreditValue
	 ,BudgetModificationDebit = DebitValue
	 from (select bmd.BudgetId ,SUM(IIF(bmd.Nature = 2, bmd.Value, 0)) CreditValue, SUM(IIF(bmd.Nature = 1, bmd.Value, 0)) DebitValue
	 from Budget.BudgetModificationDetail bmd 
	 inner join Budget.BudgetModification bm on bm.Id = bmd.ModificationId
	 /*where cast(bm.DocumentDate as date) BETWEEN @InitialDate And @EndDate*/ group by bmd.BudgetId) as data
	 inner join @TableExecution te on data.BudgetId = te.BudgetId

	   -- insertamos en la tabla temporal los datos de Disponibilidades
	 update @TableExecution set [AvailabilityCode] = Code,
	 AvailabilityTyp = AvailabilityType
	 from (select ad.BudgetId, a.Code, a.AvailabilityType
	 from Budget.AvailabilityDetail AS ad 
	 INNER JOIN Budget.[Availability] AS a ON a.Id = ad.AvailabilityId
	 /*where cast(bm.DocumentDate as date) BETWEEN @InitialDate And @EndDate*/ group by ad.BudgetId, a.Code, a.AvailabilityType) as data
	 inner join @TableExecution te on data.BudgetId = te.BudgetId

	   -- insertamos en la tabla temporal los datos  Modificaciones de disponibilidades
	 update @TableExecution set AvailabilityModificationCode = Code
	 from (select ad.BudgetId, am.Code
	 from Budget.AvailabilityModificationDetail AS amd
	 INNER JOIN Budget.AvailabilityDetail AS ad ON ad.Id = amd.AvailabilityDetailId
	 INNER JOIN Budget.AvailabilityModification AS am ON am.Id = amd.AvailabilityModificationId
	 /*where cast(bm.DocumentDate as date) BETWEEN @InitialDate And @EndDate*/ group by ad.BudgetId, am.Code) as data
	 inner join @TableExecution te on data.BudgetId = te.BudgetId

	    -- insertamos en la tabla temporal los datos de Compromisos
	 update @TableExecution set CommitmentCode = Code,
	 CommitmentTyp = CommitmentType
	 from (select ad.BudgetId, c.Code, c.CommitmentType
	 from Budget.CommitmentDetail AS cd
	 INNER JOIN Budget.AvailabilityDetail AS ad ON ad.Id = cd.AvailabilityDetailId
	 INNER JOIN Budget.Commitment AS c ON c.Id = cd.CommitmentId
	 /*where cast(bm.DocumentDate as date) BETWEEN @InitialDate And @EndDate*/ group by ad.BudgetId, c.Code, c.CommitmentType) as data
	 inner join @TableExecution te on data.BudgetId = te.BudgetId

	 
	    -- insertamos en la tabla temporal los datos de Obligaciones
	 update @TableExecution set ObligationCode = Code,
	 ObligationTyp = ObligationType
	 from (select ad.BudgetId, o.Code, o.ObligationType
	 from Budget.ObligationDetail AS od
	 INNER JOIN Budget.Obligation AS o ON o.Id = od.ObligationId
	 LEFT JOIN Budget.CommitmentDetail AS cd ON cd.Id = od.CommitmentDetailId
	 LEFT JOIN Budget.AvailabilityDetail AS ad ON ad.Id = cd.AvailabilityDetailId
	 LEFT JOIN Budget.Commitment AS c ON c.Id = cd.CommitmentId
	 /*where cast(bm.DocumentDate as date) BETWEEN @InitialDate And @EndDate*/ group by ad.BudgetId, o.Code, o.ObligationType) as data
	 inner join @TableExecution te on data.BudgetId = te.BudgetId

  Select Iif([BudgetInitial] is null, 0 ,[BudgetInitial] ) + Iif([BudgetTransferCredit] is null, 0 ,[BudgetTransferCredit]) - Iif([BudgetTransferDebit] is null, 0 ,[BudgetTransferDebit]) + Iif([BudgetModificationCredit] is null, 0 ,[BudgetModificationCredit]) - Iif([BudgetModificationDebit] is null, 0 ,[BudgetModificationDebit]) AS 'definitive', iif(AvailabilityTyp = 1, 'Ninguno',iif(AvailabilityTyp = 2,'Disponibilidad',iif(AvailabilityTyp = 3,'Vigencia Factura', NULL))) AS 'TypeAvailability', iif(ObligationTyp = 1, 'Obligación', iif(ObligationTyp = 2,'CuentaXPagar', NULL)) AS 'ObligationType', iif(CommitmentTyp = 1, 'Compromiso',iif(CommitmentTyp = 2, 'Reserva', NULL)) AS 'CommitmentType', * From @TableExecution --WHERE CategoryCode = '1010101' ORDER BY CategoryCode

  END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el informe de ejecución presupuestal por categoría o rubro de tercer nivel, consolidando para cada partida presupuestal su valor inicial, los traslados de recursos (créditos y débitos por transferencias y modificaciones presupuestales), las disponibilidades presupuestales, las modificaciones de disponibilidad, los compromisos adquiridos y las obligaciones generadas. Recibe un rango de fechas y un rango de códigos de categoría como filtros, y calcula el presupuesto definitivo de cada rubro sumando el valor inicial más los créditos menos los débitos de traslados y modificaciones. Es utilizado en la gestión y seguimiento presupuestal para conocer el estado de ejecución del gasto por categoría, mostrando en lenguaje de negocio el tipo de disponibilidad (ninguno, disponibilidad, vigencia factura), el tipo de compromiso (compromiso, reserva) y el tipo de obligación (obligación, cuenta por pagar).', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'ExecutionByCategoryThird';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'ExecutionByCategoryThird';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye un reporte consolidado de ejecución presupuestal por categoría, integrando valores iniciales, traslados, modificaciones, disponibilidades, compromisos y obligaciones con su valor definitivo calculado.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'ExecutionByCategoryThird';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir presupuestos en Budget.Budget con su Category, FinancialSource, RevenueType y BudgetHeader relacionados.; Los parámetros de fecha y rango de categoría se reciben pero actualmente no se aplican como filtros (las cláusulas WHERE están comentadas).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'ExecutionByCategoryThird';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor ''definitive'' siempre se calcula con créditos sumando y débitos restando, considerando NULL como 0.; La carga base solo incluye presupuestos con Category, FinancialSource, RevenueType y BudgetHeader existentes (INNER JOIN).; Los enriquecimientos posteriores (traslados, modificaciones, disponibilidades, compromisos, obligaciones) solo afectan filas con BudgetId coincidente; sin coincidencia los campos quedan NULL.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'ExecutionByCategoryThird';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Presupuesto; Categoría presupuestal; Traslado presupuestal; Modificación presupuestal; Disponibilidad presupuestal (CDP); Modificación de disponibilidad; Compromiso; Reserva; Obligación; Cuenta por pagar; Vigencia factura; Naturaleza débito/crédito; Valor definitivo de presupuesto', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'ExecutionByCategoryThird';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT_SET: Devuelve por cada presupuesto el ''definitive'' = BudgetInitial + BudgetTransferCredit - BudgetTransferDebit + BudgetModificationCredit - BudgetModificationDebit (NULLs tratados como 0).; [RETURN_RESULT] RESULT_SET: Traduce AvailabilityTyp: 1→''Ninguno'', 2→''Disponibilidad'', 3→''Vigencia Factura'', otro→NULL.; [RETURN_RESULT] RESULT_SET: Traduce ObligationTyp: 1→''Obligación'', 2→''CuentaXPagar'', otro→NULL.; [RETURN_RESULT] RESULT_SET: Traduce CommitmentTyp: 1→''Compromiso'', 2→''Reserva'', otro→NULL.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'ExecutionByCategoryThird';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si BudgetTransferDetail.Nature = 2 → Suma el valor como CreditValue (BudgetTransferCredit) else Si Nature = 1, suma como DebitValue (BudgetTransferDebit); si BudgetModificationDetail.Nature = 2 → Suma el valor como CreditValue (BudgetModificationCredit) else Si Nature = 1, suma como DebitValue (BudgetModificationDebit)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'ExecutionByCategoryThird';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Budget; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Budget.BudgetHeader; Budget.BudgetTransferDetail; Budget.BudgetTransfer; Budget.BudgetModificationDetail; Budget.BudgetModification; Budget.AvailabilityDetail; Budget.Availability; Budget.AvailabilityModificationDetail; Budget.AvailabilityModification; Budget.CommitmentDetail; Budget.Commitment; Budget.ObligationDetail; Budget.Obligation', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'ExecutionByCategoryThird';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'ExecutionByCategoryThird';
-- GO
