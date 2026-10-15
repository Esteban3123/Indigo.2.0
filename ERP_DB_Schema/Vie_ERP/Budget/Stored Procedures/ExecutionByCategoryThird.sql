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