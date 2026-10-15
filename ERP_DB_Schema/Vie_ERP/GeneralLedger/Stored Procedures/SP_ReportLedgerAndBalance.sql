-- =============================================
-- Author:		Juan Bermudez
-- Create date: 24/06/2016
-- Description:	Procedimiento para el reporte de libro mayor y balance
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportLedgerAndBalance]
	@mes int,
	@ano int,
	@accountingZero as bit,
	@bookId as integer,
	@accountLevel as integer,
	@allowThirdParty as bit = 0
AS
BEGIN

	-- Creamos la tabla temporal que devolveremos con los datos
	declare @tableExecution table(id int identity(1,1) primary key,mainAccountId int,mainAccountCode varchar(20) ,mainAccountName varchar(max),nature varchar(10), thirdPartyId int, thirdPartyNit varchar(20), thirdPartyName varchar(max),costCenterId int, costCenterCode varchar(20),costCenterName varchar(max), valueDebitInitial decimal(20,4), valueCreditInitial decimal(20,4),previousBalance decimal(20,4), valueDebitMovement decimal(20,4), valueCreditMovement decimal(20,4), newBalance decimal(20,4), newBalanceDebit decimal(20,4), newBalanceCredit decimal(20,4),mainAccountClassType int, mainAccountLevel int, allowsMovement bit, classCode varchar(20) )
	if @allowThirdParty = 1 begin
		INSERT INTO @tableExecution (mainAccountId,mainAccountCode,mainAccountName,nature, thirdPartyId, thirdPartyNit, thirdPartyName,costCenterId, costCenterCode,costCenterName, valueDebitInitial, valueCreditInitial,previousBalance, valueDebitMovement, valueCreditMovement, newBalance, newBalanceDebit, newBalanceCredit,mainAccountClassType, mainAccountLevel,allowsMovement, classCode)
		SELECT 
			ISNULL (Inicial.IdMainAccount,Movimiento.IdMainAccount) as IdCuentaContable, ISNULL (Inicial.Number,Movimiento.Number)  as CuentaContable,ISNULL(Inicial.Name,Movimiento.Name) as CuentaContableName, ISNULL(Inicial.Naturaleza, Movimiento.Naturaleza)  as Naturaleza,
			ISNULL (Inicial.IdThirdParty,Movimiento.IdThirdParty) as idTercero ,tp.Nit as Nit , tp.Name as NombreTercero ,ISNULL (Inicial.IdCostCenter , Movimiento.IdCostCenter) as IdCC, cc.Code as CodigoCC , cc.Name as NombreCC,   
			isnull(Inicial.Debit,0) as DebitoIncial, isnull(Inicial.Credit,0) as CreditoInicial, isnull(Inicial.Saldo,0) as SaldoInicial, isnull(Movimiento.Debit,0) as DebitoMovimiento, isnull(Movimiento.Credit,0) as CreditoMovimiento,
			isnull(Inicial.Saldo,0) + isnull(Movimiento.Saldo,0) as Saldo, isnull(Inicial.Debit,0) + ISNULL(Movimiento.Debit,0) as saldoDebito, ISNULL(Inicial.Credit,0) + ISNULL(Movimiento.Credit, 0) as saldoCredito,
			ISNULL(Inicial.mainAccountType, Movimiento.mainAccountType) as mainAccountType, ISNULL(Inicial.[Level], Movimiento.[Level]) as IdAccountLevel, ISNULL(Inicial.AllowsMovement, Movimiento.AllowsMovement) as AllowsMovement,
			ISNULL(Inicial.codeClass,Movimiento.codeClass) as codeClass	
		FROM 
		(
			select 
				ma.Id as IdMainAccount, ma.Number,ma.Name, glb.IdThirdParty, glb.IdCostCenter, SUM(glb.DebitValue) AS Debit, SUM(glb.CreditValue) as Credit,  
				case ma.nature when 1 then  SUM(glb.DebitValue) - SUM(glb.CreditValue) else    SUM(glb.CreditValue) -SUM(glb.DebitValue) end as Saldo, 
				case ma.nature when 1 then 'Debito' else 'Credito' end as Naturaleza, mac.[Type] as mainAccountType, mal.[Level], ma.AllowsMovement, mac.Code as codeClass
			from 
			(
				select 
					IdMainAccount,  IdThirdParty, IdCostCenter, DebitValue, CreditValue 
				from GeneralLedger.GeneralLedgerBalance 
				where ([Year] = (@ano - 1) and [Month] = 14) or ([Year] = @ano and [Month] < @mes)
			) as glb 
			right outer join GeneralLedger.MainAccounts as ma on  ma.id = glb.IdMainAccount 
			inner join GeneralLedger.MainAccountClasses mac on mac.Id = ma.IdAccountClass 
			inner join GeneralLedger.MainAccountLevels mal on mal.Id = ma.IdAccountLevel 
			where ma.LegalBookId = @bookId
			group by ma.Id, ma.Number, ma.Name, glb.IdThirdParty, glb.IdCostCenter, ma.Nature, mac.[Type], mal.[Level], ma.AllowsMovement, mac.Code
		) as Inicial
		FULL JOIN 
		(
			select 
				ma.Id as IdMainAccount, ma.Number, ma.Name, glb.IdThirdParty, glb.IdCostCenter, SUM(glb.DebitValue) AS Debit, SUM(glb.CreditValue) as Credit,  
				case ma.nature when 1 then  SUM(glb.DebitValue) - SUM(glb.CreditValue) else    SUM(glb.CreditValue) -SUM(glb.DebitValue) end as Saldo, 
				case ma.nature when 1 then 'Debito' else 'Credito' end as Naturaleza, mac.[Type] as mainAccountType, mal.[Level], ma.AllowsMovement, mac.Code as codeClass 
			from 
			(
				select 
					IdMainAccount,  IdThirdParty, IdCostCenter, DebitValue, CreditValue 
				from GeneralLedger.GeneralLedgerBalance 
				where ([Year] = @ano and [Month] = @mes )
			) as glb 
			right outer join GeneralLedger.MainAccounts as ma on  ma.id = glb.IdMainAccount 
			inner join GeneralLedger.MainAccountClasses mac on mac.Id = ma.IdAccountClass 
			inner join GeneralLedger.MainAccountLevels mal on mal.Id = ma.IdAccountLevel 
			where ma.LegalBookId = @bookId 
			group by ma.Id, ma.Number, ma.Name, glb.IdThirdParty, glb.IdCostCenter, ma.Nature, mac.[Type], mal.[Level], ma.AllowsMovement, mac.Code
		) as Movimiento ON Inicial.IdMainAccount = Movimiento.IdMainAccount and ISNULL(Inicial.IdThirdParty,0) = ISNULL(Movimiento.IdThirdParty,0) and ISNULL(Inicial.IdCostCenter,0) = ISNULL(Movimiento.IdCostCenter,0)
		LEFT JOIN Common.ThirdParty as tp on tp.Id = ISNULL (Inicial.IdThirdParty,Movimiento.IdThirdParty)
		LEFT JOIN Payroll.CostCenter as cc on cc.Id = ISNULL (Inicial.IdCostCenter,Movimiento.IdCostCenter)
		order by CuentaContable
	end
	else begin
		INSERT INTO @tableExecution (mainAccountId,mainAccountCode,mainAccountName,nature, thirdPartyId, thirdPartyNit, thirdPartyName,costCenterId, costCenterCode,costCenterName, valueDebitInitial, valueCreditInitial,previousBalance, valueDebitMovement, valueCreditMovement, newBalance, newBalanceDebit, newBalanceCredit,mainAccountClassType, mainAccountLevel,allowsMovement, classCode)
		SELECT ISNULL (Inicial.IdMainAccount,Movimiento.IdMainAccount) as IdCuentaContable, ISNULL (Inicial.Number,Movimiento.Number)  as CuentaContable,ISNULL(Inicial.Name,Movimiento.Name) as CuentaContableName, ISNULL(Inicial.Naturaleza, Movimiento.Naturaleza)  as Naturaleza,
		null as idTercero ,'' as Nit , '' as NombreTercero ,null as IdCC, '' as CodigoCC , '' as NombreCC,   
		isnull(Inicial.Debit,0) as DebitoIncial, isnull(Inicial.Credit,0) as CreditoInicial, isnull(Inicial.Saldo,0) as SaldoInicial, isnull(Movimiento.Debit,0) as DebitoMovimiento, isnull(Movimiento.Credit,0) as CreditoMovimiento,
		isnull(Inicial.Saldo,0) + isnull(Movimiento.Saldo,0) as Saldo, isnull(Inicial.Debit,0) + ISNULL(Movimiento.Debit,0) as saldoDebito, ISNULL(Inicial.Credit,0) + ISNULL(Movimiento.Credit, 0) as saldoCredito,
		ISNULL(Inicial.mainAccountType, Movimiento.mainAccountType) as mainAccountType, ISNULL(Inicial.[Level], Movimiento.[Level]) as IdAccountLevel, ISNULL(Inicial.AllowsMovement, Movimiento.AllowsMovement) as AllowsMovement,
		ISNULL(Inicial.codeClass,Movimiento.codeClass) as codeClass	
		FROM 
		(select ma.Id as IdMainAccount, ma.Number,ma.Name, null as IdThirdParty, null as IdCostCenter, SUM(glb.DebitValue) AS Debit, SUM(glb.CreditValue) as Credit,  
		case ma.nature when 1 then  SUM(glb.DebitValue) - SUM(glb.CreditValue) else    SUM(glb.CreditValue) -SUM(glb.DebitValue) end as Saldo, 
		case ma.nature when 1 then 'Debito' else 'Credito' end as Naturaleza, mac.[Type] as mainAccountType, mal.[Level], ma.AllowsMovement, mac.Code as codeClass
		from 
		(select IdMainAccount,  IdThirdParty, IdCostCenter, DebitValue, CreditValue from GeneralLedger.GeneralLedgerBalance where ([Year] = (@ano - 1) and [Month] = 14) or ([Year] = @ano and [Month] < @mes)) as glb right outer join GeneralLedger.MainAccounts as ma on  ma.id = glb.IdMainAccount 
		inner join GeneralLedger.MainAccountClasses mac on mac.Id = ma.IdAccountClass inner join GeneralLedger.MainAccountLevels mal on mal.Id = ma.IdAccountLevel 
		where ma.LegalBookId = @bookId
		group by ma.Id, ma.Number, ma.Name, ma.Nature, mac.[Type], mal.[Level], ma.AllowsMovement, mac.Code) as Inicial
		FULL JOIN 
		(select ma.Id as IdMainAccount, ma.Number, ma.Name, null as IdThirdParty, null as IdCostCenter, SUM(glb.DebitValue) AS Debit, SUM(glb.CreditValue) as Credit,  
		case ma.nature when 1 then  SUM(glb.DebitValue) - SUM(glb.CreditValue) else    SUM(glb.CreditValue) -SUM(glb.DebitValue) end as Saldo, 
		case ma.nature when 1 then 'Debito' else 'Credito' end as Naturaleza, mac.[Type] as mainAccountType, mal.[Level], ma.AllowsMovement, mac.Code as codeClass 
		from 
		(select IdMainAccount,  IdThirdParty, IdCostCenter, DebitValue, CreditValue from GeneralLedger.GeneralLedgerBalance where ([Year] = @ano and [Month] = @mes )) as glb right outer join GeneralLedger.MainAccounts as ma   on  ma.id = glb.IdMainAccount 
		inner join GeneralLedger.MainAccountClasses mac on mac.Id = ma.IdAccountClass inner join GeneralLedger.MainAccountLevels mal on mal.Id = ma.IdAccountLevel 
		where ma.LegalBookId = @bookId 
		group by ma.Id, ma.Number, ma.Name, ma.Nature, mac.[Type], mal.[Level], ma.AllowsMovement, mac.Code) as Movimiento

		ON Inicial.IdMainAccount = Movimiento.IdMainAccount and ISNULL(Inicial.IdThirdParty,0) = ISNULL(Movimiento.IdThirdParty,0) and ISNULL(Inicial.IdCostCenter,0) = ISNULL(Movimiento.IdCostCenter,0)
		LEFT JOIN
		Common.ThirdParty as tp on tp.Id = ISNULL (Inicial.IdThirdParty,Movimiento.IdThirdParty)
		LEFT JOIN
		Payroll.CostCenter as cc on cc.Id = ISNULL (Inicial.IdCostCenter,Movimiento.IdCostCenter)
		order by CuentaContable
	end

	--- mayorizamos los saldos a nivel de subcuenta
	declare @IdAccount int
	declare @mainAccountCode varchar(50)
	declare @natureAccount varchar(10)
	declare detail_cursor cursor for
			select Id,mainAccountCode,nature from @TableExecution where allowsMovement = 0

			open detail_cursor
					FETCH NEXT FROM detail_cursor
					INTO @IdAccount, @mainAccountCode, @natureAccount

					WHILE @@FETCH_STATUS = 0
					BEGIN
					
					declare @debitInitialMayor as decimal(20,4)
					declare @creditInitialMayor as decimal(20,4)
					declare @previousBalanceMayor as decimal(20,4)
					declare @debitMovementMayor as decimal(20,4)
					declare @creditMovementMayor as decimal(20,4)
					declare @newBalanceMayor as decimal(20,4)
					declare @newBalanceDebitMayor as decimal(20,4)
					declare @newBalanceCreditMayor as decimal(20,4)
					
					select @debitInitialMayor = sum(ISNULL(te.valueDebitInitial,0))
					,@creditInitialMayor = sum(ISNULL(te.valueCreditInitial,0))
					,@previousBalanceMayor = sum(case te.nature when @natureAccount then ISNULL(te.previousBalance,0) else (-1 * ISNULL(te.previousBalance,0)) end)
					,@debitMovementMayor =sum(ISNULL(te.valueDebitMovement,0))
					,@creditMovementMayor = sum(ISNULL(te.valueCreditMovement,0))
					,@newBalanceMayor = sum(case te.nature when @natureAccount then ISNULL(te.newBalance,0) else (-1 * ISNULL(te.newBalance,0)) end) 
					,@newBalanceDebitMayor = sum(ISNULL(te.newBalanceDebit,0))
					,@newBalanceCreditMayor = sum(ISNULL(te.newBalanceCredit,0))
					from @TableExecution as te where te.mainAccountCode like @mainAccountCode + '%' and allowsMovement = 1

					
					Update @TableExecution set
					valueDebitInitial = ISNULL(@debitInitialMayor,0),
					valueCreditInitial = ISNULL(@creditInitialMayor,0),
					previousBalance = ISNULL(@previousBalanceMayor,0),
					valueDebitMovement = ISNULL(@debitMovementMayor,0),
					valueCreditMovement = ISNULL(@creditMovementMayor,0),
					newBalance = ISNULL(@newBalanceMayor,0),
					newBalanceDebit = ISNULL(@newBalanceDebitMayor,0),
					newBalanceCredit = ISNULL(@newBalanceCreditMayor,0)
					where Id = @IdAccount

					FETCH NEXT FROM detail_cursor
					INTO @IdAccount, @mainAccountCode, @natureAccount
					End
			close detail_cursor
			deallocate detail_cursor

	if @accountingZero = 0
	BEGIN
	DELETE FROM @tableExecution Where valueDebitInitial = 0 And valueCreditInitial = 0 And previousBalance = 0 And valueDebitMovement = 0 And valueCreditMovement = 0 And newBalance = 0 And newBalanceDebit = 0 And newBalanceCredit= 0
	END
	
	Select * from @tableExecution Where mainAccountLevel <= @accountLevel order by mainAccountCode

END