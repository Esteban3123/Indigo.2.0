
-- =============================================
-- Author:		Juan Bermudez
-- Create date: 18/03/2016
-- Description:	Procedimiento para el reporte de CGN 001 
-- =============================================

CREATE PROCEDURE [GeneralLedger].[SP_ReportCGN001]
	@dateStart as date,
	@dateEnd as date,
	@accountStart as varchar(20),
	@accountEnd as varchar(20),
	@accountingZero as bit,
	@bookId as integer
AS
BEGIN

	-- Creamos la tabla temporal que devolveremos con los datos
	declare @tableExecution table
	(
		mainAccountId int,
		mainAccountCode varchar(20),
		mainAccountName varchar(max), 
		mainAccountPreviousBalance decimal(20,4), 
		debitValue decimal(20,4), 
		creditValue decimal(20,4), 
		mainAccountNewBalance decimal(20,4), 
		balanceCurrent decimal(20,4), 
		balanceNotCurrent decimal(20,4), 
		natureMainAccount int, 
		allowsMovement bit, 
		levelAccount int
	)

	--- creamos la tabla temporal para los saldos anteriores
	declare @tableBalance table
	(
		mainAccountBalanceId int, 
		previousBalanceAccount decimal(20,4),
		balanceCurrent decimal(20,4),
		balanceNotCurrent decimal(20,4),
		NatureAccount int, 
		allowsMovement bit, 
		levelAccount int
	)

	--- sacamos el mes y año anterior a la fecha inicial para sacar el saldo anterior
	declare @previousMonth as int
	declare @previousYear as int
	set @PreviousMonth = Month(DATEADD(MONTH, -1, @dateStart))
	set @previousYear = YEAR(DATEADD(MONTH, -1, @dateStart))
	
	--- insertamos los movimientos
	INSERT INTO @TableExecution 
	(
		mainAccountId,mainAccountCode,mainAccountName,natureMainAccount,allowsMovement,levelAccount,
		mainAccountPreviousBalance,debitValue,creditValue,mainAccountNewBalance,
		balanceCurrent,
		balanceNotCurrent
	)
	Select	ma.Id,ma.Number,ma.Name,ma.Nature, ma.AllowsMovement, ma.IdAccountLevel,
			0, SUM(ISNULL(glb.DebitValue,0)), SUM(ISNULL(glb.CreditValue,0)), 0, 
			SUM(IIF(ma.[Availability] = 3,iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0)),IIF(ma.[Availability] = 1,iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0)),0))),
			SUM(IIF(ma.[Availability] = 2, iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0)),0))			
	from GeneralLedger.MainAccounts ma 
	left join GeneralLedger.GeneralLedgerBalance glb on ma.Id = glb.IdMainAccount 
	Where ma.LegalBookId = @bookId		
		AND ma.[Availability] <> 0
		AND (
			[glb].Id IS NULL
			OR (
				[glb].[Month] < 13
				AND dateadd(mm, ([glb].[Year] - 1900) * 12 + [glb].[Month] - 1 , 1 - 1) >= @dateStart  
				AND dateadd(mm, ([glb].[Year] - 1900) * 12 + [glb].[Month] - 1 , 1 - 1) <= @dateEnd
			)
		)
		AND ma.Number >= ISNULL(@accountStart, '0') And ma.Number <= ISNULL(@accountEnd, '99999999999999999999') 
	GROUP BY ma.Id,ma.Number,ma.Name,ma.Nature,ma.AllowsMovement, ma.IdAccountLevel
	
	-------------------------------------------------------------------------------------------------------------------------
	
	if @previousMonth = 12
	BEGIN
		--- si el mes anterior es 12 consultamos solo el saldo del mes 14 que seria igual al saldo del año anterior

		--- insertamos los saldos del mes 14 del año anterior en la tabla temporal de saldos
		INSERT INTO @tableBalance 
		(
			mainAccountBalanceId, NatureAccount, allowsMovement, levelAccount,
			previousBalanceAccount,
			balanceCurrent,
			balanceNotCurrent
		)
		Select	ma.Id, ma.Nature, ma.AllowsMovement, ma.IdAccountLevel,
				SUM(iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0))),
				SUM(IIF(ma.[Availability] = 1,iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0)),0)),
				SUM(IIF(ma.[Availability] = 3,iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0)),IIF(ma.[Availability] = 2,iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0)),0)))				
		from GeneralLedger.GeneralLedgerBalance glb 
		join GeneralLedger.MainAccounts ma on ma.Id = glb.IdMainAccount 
		Where ma.LegalBookId = @bookId 			
			AND ma.[Availability] <> 0
			AND glb.[Month] = 14 AND glb.[Year] = @previousYear 
			AND ma.Number >= ISNULL(@accountStart, '0') And ma.Number <= ISNULL(@accountEnd, '99999999999999999999')
		group by ma.Id,ma.Nature,ma.AllowsMovement, ma.IdAccountLevel
	END	
	ELSE
	BEGIN
		--- si el mes es diferente a 12 insertamos los saldos del mes 14 del año anterior y los del mes 1 al mes anterior de la fecha inicial seleccionada 

		--- insertamos los saldos del mes 14 del año anterior en la tabla temporal
		INSERT INTO @tableBalance 
		(
			mainAccountBalanceId, NatureAccount, allowsMovement, levelAccount,
			previousBalanceAccount,
			balanceCurrent,
			balanceNotCurrent
		)
		Select	ma.Id, ma.Nature, ma.AllowsMovement, ma.IdAccountLevel,
				SUM(iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0))),
				SUM(IIF(ma.[Availability] = 1,iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0)),0)),
				SUM(IIF(ma.[Availability] = 3,iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0)),IIF(ma.[Availability] = 2,iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0)),0)))
		from GeneralLedger.GeneralLedgerBalance glb 
		join GeneralLedger.MainAccounts ma on ma.Id = glb.IdMainAccount
		Where ma.LegalBookId = @bookId 
			AND ma.[Availability] <> 0
			AND glb.[Month] = 14 And glb.[Year] = @previousYear -1
			AND ma.Number >= ISNULL(@accountStart, '0') And ma.Number <= ISNULL(@accountEnd, '99999999999999999999')
		group by ma.Id, ma.Nature, ma.AllowsMovement, ma.IdAccountLevel

		--- actualizamos en la tabla temporal los saldos del mes 1 al mes anterior a la fecha inicial seleccionada por el usuario 
		update @tableBalance 
			set previousBalanceAccount += previousBalance,
				balanceCurrent += prevBalanceCurrent,
				balanceNotCurrent += prevBalanceNotCurrent
		from
		(
			Select	ma.Id as mainAccountId, 
					SUM(iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0))) as previousBalance,
					SUM(IIF(ma.[Availability] = 3,iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0)),IIF(ma.[Availability] = 1,iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0)),0))) as prevBalanceCurrent,
					SUM(IIF(ma.[Availability] = 2,iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0)),0)) as prevBalanceNotCurrent
			from GeneralLedger.GeneralLedgerBalance glb 
			join GeneralLedger.MainAccounts ma on ma.Id = glb.IdMainAccount
			Where ma.LegalBookId = @bookId 
				And ma.[Availability] <> 0
				AND glb.[Month] >= 1 And glb.[Month] <= @PreviousMonth And glb.[Year] = @previousYear 
				And ma.Number >= ISNULL(@accountStart, '0') And ma.Number <= ISNULL(@accountEnd, '99999999999999999999')
			group by ma.Id
		) as data
		inner join @tableBalance tb on tb.mainAccountBalanceId = data.mainAccountId 

		--- insertamos en la tabla temporal los saldos del mes 1 al mes anterior a la fecha inicial seleccionada por el usuario
		INSERT INTO @tableBalance 
		(
			mainAccountBalanceId, NatureAccount, allowsMovement, levelAccount,
			previousBalanceAccount,
			balanceCurrent,
			balanceNotCurrent
		)
		Select	ma.Id, ma.Nature , ma.AllowsMovement, ma.IdAccountLevel,
				SUM(iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0))),
				SUM(IIF(ma.[Availability] = 3,iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0)),IIF(ma.[Availability] = 1,iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0)),0))),
				SUM(IIF(ma.[Availability] = 2,iif(ma.Nature = 1,ISNULL(glb.DebitValue,0) - ISNULL(glb.CreditValue,0),ISNULL(glb.CreditValue,0) - ISNULL(glb.DebitValue,0)),0))
		from GeneralLedger.GeneralLedgerBalance glb 
		join GeneralLedger.MainAccounts ma on ma.Id = glb.IdMainAccount
		left join @tableBalance tbl ON tbl.mainAccountBalanceId = ma.Id 
		Where ma.LegalBookId = @bookId 
			AND ma.[Availability] <> 0 
			AND glb.[Month] >= 1 And glb.[Month] <= @PreviousMonth And glb.[Year] = @previousYear 
			AND ma.Number >= ISNULL(@accountStart, '0') And ma.Number <= ISNULL(@accountEnd, '99999999999999999999')
			AND tbl.mainAccountBalanceId is null 
		group by ma.Id, ma.Nature, ma.AllowsMovement, ma.IdAccountLevel
	END

	--- actualizamos el campo saldo inicial y final de la tabla 
	update @tableExecution 
		set mainAccountPreviousBalance += ISNULL(previousBalance,0),
			mainAccountNewBalance += IIF(natureMainAccount = 1, ISNULL(previousBalance,0) + ISNULL(debitValue,0) - ISNULL(creditValue,0), ISNULL(previousBalance,0) + ISNULL(creditValue,0) - ISNULL(debitValue,0)),
			balanceCurrent += ISNULL(currentBalance,0),
			balanceNotCurrent += ISNULL(currentNotBalance,0) 	
	from @tableExecution te 
	left join 
	(
		Select	tb.mainAccountBalanceId, 
				ISNULL(tb.previousBalanceAccount,0) as previousBalance, 
				ISNULL(tb.balanceCurrent,0) as currentBalance, 
				ISNULL(tb.balanceNotCurrent,0) as currentNotBalance
		from @tableBalance tb
	) as data on te.mainAccountId = data.mainAccountBalanceId
	
	--- insertamos los datos que estan en la tabla de saldos pero no tuvieron movimientos en el rango de fechas seleccionado
	INSERT INTO @tableExecution 
	(
		mainAccountId, mainAccountCode,mainAccountName, natureMainAccount, allowsMovement, levelAccount,
		mainAccountPreviousBalance,debitValue,creditValue,mainAccountNewBalance,
		balanceCurrent,
		balanceNotCurrent
	)
	select 	ma.Id,ma.Number,ma.Name, ma.Nature, ma.AllowsMovement, ma.IdAccountLevel,
			SUM(ISNULL(tb.previousBalanceAccount,0)),0,0,SUM(ISNULL(tb.previousBalanceAccount,0)), 
			SUM(ISNULL(tb.balanceCurrent,0)),
			SUM(ISNULL(tb.balanceNotCurrent,0))
	from @tableBalance tb inner join GeneralLedger.MainAccounts ma on ma.Id = tb.mainAccountBalanceId
	left join @tableExecution te ON te.mainAccountId = tb.mainAccountBalanceId
	where te.mainAccountId is null 
	group by ma.Id,ma.Number,ma.Name, ma.Nature, ma.AllowsMovement, ma.IdAccountLevel

	--- insertamos todo el puc
	INSERT INTO @TableExecution 
	(
		mainAccountId,mainAccountCode,mainAccountName,natureMainAccount, allowsMovement, levelAccount,
		mainAccountPreviousBalance,debitValue,creditValue,mainAccountNewBalance,balanceCurrent,balanceNotCurrent
	)
	Select ma.Id,ma.Number,ma.Name,ma.Nature,ma.AllowsMovement, ma.IdAccountLevel,
		0,0,0,0,0,0 
	from GeneralLedger.MainAccounts ma 
	left join @tableExecution te on te.mainAccountId = ma.Id
	where ma.LegalBookId = @bookId AND te.mainAccountId is null


	--- mayorizamos los saldos a nivel de subcuenta
	declare @IdAccount int
	declare @mainAccountCode varchar(50)
	declare @natureAccount varchar(10)
	declare detail_cursor cursor for select mainAccountId,mainAccountCode,natureMainAccount from @TableExecution where allowsMovement = 0

	open detail_cursor
		FETCH NEXT FROM detail_cursor INTO @IdAccount, @mainAccountCode, @natureAccount

	WHILE @@FETCH_STATUS = 0
	BEGIN
		declare @previousBalanceMayor as decimal(20,4)
		declare @debitMovementMayor as decimal(20,4)
		declare @creditMovementMayor as decimal(20,4)
		declare @newBalanceMayor as decimal(20,4)
		declare @balanceCurrentMayor as decimal(20,4)
		declare @balanceNotCurrentMayor as decimal(20,4)

		select	@previousBalanceMayor = sum(case te.natureMainAccount when @natureAccount then ISNULL(te.mainAccountPreviousBalance,0) else (-1 * ISNULL(te.mainAccountPreviousBalance,0)) end),
				@debitMovementMayor = sum(ISNULL(te.debitValue,0)),
				@creditMovementMayor = sum(ISNULL(te.creditValue,0)),
				@newBalanceMayor = sum(case te.natureMainAccount when @natureAccount then ISNULL(te.mainAccountNewBalance,0) else (-1 * ISNULL(te.mainAccountNewBalance,0)) end),
				@balanceCurrentMayor = sum(ISNULL(te.balanceCurrent,0)),
				@balanceNotCurrentMayor = sum(ISNULL(te.balanceNotCurrent,0))
		from @TableExecution as te 
		where te.mainAccountCode like @mainAccountCode + '%' and allowsMovement = 1

		Update @TableExecution 
			set mainAccountPreviousBalance = ISNULL(@previousBalanceMayor,0),
				debitValue = ISNULL(@debitMovementMayor,0),
				creditValue = ISNULL(@creditMovementMayor,0),
				mainAccountNewBalance = ISNULL(@newBalanceMayor,0),
				balanceCurrent = ISNULL(@balanceCurrentMayor,0),
				balanceNotCurrent = ISNULL(@balanceNotCurrentMayor,0)
		where mainAccountId = @IdAccount					

		FETCH NEXT FROM detail_cursor INTO @IdAccount, @mainAccountCode, @natureAccount
	End

	close detail_cursor
	deallocate detail_cursor

	if @accountingZero = 0
	BEGIN
		DELETE FROM @tableExecution Where mainAccountPreviousBalance = 0 And debitValue = 0 And creditValue = 0 And mainAccountNewBalance = 0 And balanceCurrent = 0 And balanceNotCurrent = 0 And levelAccount <> 1
	END

	select	mainAccountId,
			mainAccountCode,
			mainAccountName, 
			ROUND(mainAccountPreviousBalance, 0) mainAccountPreviousBalance, 
			ROUND(debitValue, 0) debitValue, 
			ROUND(creditValue, 0) creditValue, 
			ROUND(mainAccountNewBalance, 0) mainAccountNewBalance, 
			ROUND(balanceCurrent, 0) balanceCurrent, 
			ROUND(balanceNotCurrent, 0) balanceNotCurrent, 
			natureMainAccount, 
			allowsMovement, 
			levelAccount
	from @tableExecution
	where levelAccount <= 4
	ORder by mainAccountCode
END