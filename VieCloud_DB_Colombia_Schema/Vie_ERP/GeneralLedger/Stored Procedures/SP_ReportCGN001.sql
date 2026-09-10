
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte contable CGN 001 (Balance General o Estado de Saldos para entidades públicas) para un rango de fechas, rango de cuentas y libro contable específico. Consulta las cuentas contables del plan de cuentas (MainAccounts) y sus saldos acumulados (GeneralLedgerBalance) para calcular, por cada cuenta: el saldo anterior al período, los movimientos débito y crédito del período seleccionado, y el saldo nuevo resultante, distinguiendo además entre saldo corriente y no corriente según la disponibilidad de la cuenta. Maneja correctamente el cierre de año al tomar el saldo acumulado del mes 14 (mes de cierre) del año anterior como punto de partida, y permite filtrar cuentas con saldo cero. Es el procedimiento base para presentar el informe oficial CGN 001 ante la Contaduría General de la Nación.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportCGN001';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportCGN001';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte contable CGN-001 consolidando saldos anteriores, movimientos del período y saldos corriente/no corriente por cuenta del PUC, mayorizando hasta nivel de subcuenta.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCGN001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El libro contable (LegalBookId) debe existir y tener cuentas asociadas en GeneralLedger.MainAccounts.; Las cuentas deben tener Availability distinto de 0 para participar en el reporte.; Los saldos en GeneralLedgerBalance deben tener Month entre 1 y 14 (14 representa cierre de año).; El rango de cuentas (@accountStart, @accountEnd) actúa como filtro; si es null se asume rango completo (''0'' a ''99999999999999999999'').', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCGN001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan cuentas cuya Availability sea distinta de 0.; El reporte siempre se restringe a un único libro contable (@bookId).; El resultado final solo incluye cuentas con levelAccount <= 4.; Las cuentas padre (allowsMovement=0) reflejan la suma agregada de sus hijos detalle (allowsMovement=1) bajo el mismo prefijo de código.; La clasificación corriente/no corriente depende exclusivamente del campo Availability de la cuenta (1/3 = corriente, 2 = no corriente).; El cálculo de saldo respeta la naturaleza de la cuenta: débito (Nature=1) usa Débito-Crédito, crédito usa Crédito-Débito.; Month=14 en GeneralLedgerBalance se interpreta como saldo de cierre anual.; Los valores devueltos están redondeados a 0 decimales.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCGN001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Plan Único de Cuentas (PUC); Reporte CGN-001 (Contaduría General de la Nación); Libro contable legal; Saldo anterior / saldo final; Movimientos débito y crédito; Naturaleza de la cuenta (débito/crédito); Mayorización de cuentas; Activos/pasivos corrientes y no corrientes; Cierre contable anual (Mes 14); Niveles jerárquicos de cuenta', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCGN001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @tableExecution: Inserta movimientos del período: cuentas con LegalBookId=@bookId, Availability<>0, dentro del rango de fechas calculado a partir de Month/Year de GeneralLedgerBalance, y dentro del rango de cuentas.; [INSERT] @tableBalance: Si el mes anterior es 12, inserta solo los saldos de Month=14 del año anterior (cierre anual) como saldo previo.; [INSERT] @tableBalance: Si el mes anterior NO es 12, inserta saldos de Month=14 del año (@previousYear-1) como saldo de apertura.; [UPDATE] @tableBalance: Acumula sobre los saldos de apertura los movimientos de Month entre 1 y @previousMonth del @previousYear para cuentas ya presentes en @tableBalance.; [INSERT] @tableBalance: Inserta saldos de Month entre 1 y @previousMonth del @previousYear únicamente para cuentas que no estaban ya en @tableBalance (evita duplicar).; [UPDATE] @tableExecution: Suma a cada cuenta el saldo previo y recalcula mainAccountNewBalance: si Nature=1 (débito) saldo+débitos-créditos; en otro caso saldo+créditos-débitos.; [INSERT] @tableExecution: Inserta cuentas que tienen saldo previo en @tableBalance pero no tuvieron movimientos en el período (no estaban en @tableExecution).; [INSERT] @tableExecution: Inserta el resto del PUC del libro (cuentas sin saldo previo ni movimientos) con valores en cero para presentar la estructura completa.; [UPDATE] @tableExecution: Para cada cuenta con allowsMovement=0 (cuenta padre) totaliza los hijos cuyo mainAccountCode empieza con el código del padre y allowsMovement=1, invirtiendo el signo cuando la naturaleza del hijo difiere de la del padre.; [DELETE] @tableExecution: Si @accountingZero=0, elimina filas con todos los saldos y movimientos en cero, exceptuando cuentas de levelAccount=1.; [RETURN_RESULT] @tableExecution: Retorna las cuentas con levelAccount<=4 ordenadas por mainAccountCode, con valores redondeados a entero.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCGN001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @previousMonth = 12 (la fecha inicial está en enero) → Solo carga saldos de cierre Month=14 del @previousYear como saldo anterior. else Carga saldos de cierre Month=14 de (@previousYear-1) y acumula movimientos de los meses 1..@previousMonth del @previousYear.; si Availability = 3 o 1 en cuenta → El movimiento/saldo se considera ''corriente'' (balanceCurrent). else Si Availability=2 se clasifica como ''no corriente'' (balanceNotCurrent); si Availability=0 la cuenta se excluye.; si Nature = 1 (débito) → Saldo se calcula como Débito - Crédito. else Saldo se calcula como Crédito - Débito.; si allowsMovement = 0 (cuenta mayor/padre) → Se mayoriza sumando los hijos (allowsMovement=1) cuyo código comience con el código del padre, ajustando signo según naturaleza. else Se mantiene el valor calculado con sus propios movimientos y saldos.; si @accountingZero = 0 → Elimina del resultado las cuentas en cero (excepto levelAccount=1). else Conserva todas las cuentas aunque tengan valores en cero.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCGN001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.MainAccounts; GeneralLedger.GeneralLedgerBalance', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCGN001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCGN001';
-- GO
