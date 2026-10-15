

-- =============================================
-- Author:		Juan Bermudez
-- Create date: 24/06/2016
-- Description:	Procedimiento para el reporte de libro mayor y balance
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportInventoryAndBalance]

	@mesInicial int,
	@mesFinal int ,
	@ano int,
	@accountingZero as bit,
	@bookId as integer,
	@accountLevel as integer,
	@allowThirdParty as bit,
	@initialAccount as int,
	@finalAccount as int
AS
BEGIN
	

	print @initialAccount
	print @finalAccount
	-- Creamos la tabla temporal que devolveremos con los datos
	declare @tableExecution table(id int identity(1,1) primary key,
								  mainAccountId int,
								  mainAccountCode varchar(20),
								  mainAccountName varchar(max),
								  nature varchar(10), 
								  thirdPartyId int, 
								  thirdPartyNit varchar(20), 
								  thirdPartyName varchar(max), 
								  valueDebitInitial decimal(20,4), 
								  valueCreditInitial decimal(20,4),
								  previousBalance decimal(20,4), 
								  valueDebitMovement decimal(20,4), 
								  valueCreditMovement decimal(20,4), 
								  newBalance decimal(20,4),
								  mainAccountClassType int, 
								  mainAccountLevel int, 
								  allowsMovement bit,
								  classCode varchar(20))
	 --declare @CurrencyAbbreviation as varchar(5)

	 --select 
		--@CurrencyAbbreviation=c.Abbreviation
	 --from GeneralLedger.LegalBook lb
	 --join Common.Currency as c on c.Id = lb.OfficialCurrencyId
	 --where lb.Id = @bookId

	if @allowThirdParty = 1 begin
		
		INSERT INTO @tableExecution (mainAccountId,mainAccountCode,mainAccountName,nature, thirdPartyId, thirdPartyNit, thirdPartyName, valueDebitInitial, valueCreditInitial,previousBalance, valueDebitMovement, valueCreditMovement, newBalance,mainAccountClassType, mainAccountLevel,allowsMovement, classCode)
		SELECT 
			ISNULL (Inicial.IdMainAccount,Movimiento.IdMainAccount) as IdCuentaContable, 
			ISNULL (Inicial.Number,Movimiento.Number)  as CuentaContable,
			ISNULL(Inicial.Name,Movimiento.Name) as CuentaContableName, 
			ISNULL(Inicial.Naturaleza, Movimiento.Naturaleza)  as Naturaleza,
			ISNULL (Inicial.IdThirdParty,Movimiento.IdThirdParty) as idTercero ,
			tp.Nit as Nit , 
			tp.Name as NombreTercero,   
			isnull(Inicial.Debit,0) as DebitoIncial, 
			isnull(Inicial.Credit,0) as CreditoInicial,
			isnull(Inicial.Saldo,0) as SaldoInicial,
			isnull(Movimiento.Debit,0) as DebitoMovimiento, 
			isnull(Movimiento.Credit,0) as CreditoMovimiento,
			isnull(Inicial.Saldo,0) + isnull(Movimiento.Saldo,0) as Saldo, 
			ISNULL(Inicial.mainAccountType, Movimiento.mainAccountType) as mainAccountType,
			ISNULL(Inicial.[Level], Movimiento.[Level]) as IdAccountLevel,
			ISNULL(Inicial.AllowsMovement, Movimiento.AllowsMovement) as AllowsMovement,
			ISNULL(Inicial.codeClass,Movimiento.codeClass) as codeClass	
		FROM 
		(select 
			ma.Id as IdMainAccount,
			ma.Number,ma.Name,
			glb.IdThirdParty,
			SUM(glb.DebitValue) AS Debit, 
			SUM(glb.CreditValue) as Credit,  
			case ma.nature 
				when 1 then  SUM(glb.DebitValue) - SUM(glb.CreditValue) 
				else    SUM(glb.CreditValue) -SUM(glb.DebitValue)
			end as Saldo, 
			case ma.nature 
				when 1 then 'Debito' 
				else 'Credito' 
			end as Naturaleza,
			mac.[Type] as mainAccountType,
			mal.[Level], ma.AllowsMovement,
			mac.Code as codeClass
		from 
		(select
			IdMainAccount, 
			IdThirdParty,
			DebitValue, 
			CreditValue
		from GeneralLedger.GeneralLedgerBalance
		where ([Year] = (@ano - 1) and [Month] = 14) or ([Year] = @ano and [Month] < @mesInicial)) as glb 
		right outer join GeneralLedger.MainAccounts as ma on  ma.id = glb.IdMainAccount 
		inner join GeneralLedger.MainAccountClasses mac on mac.Id = ma.IdAccountClass 
		inner join GeneralLedger.MainAccountLevels mal on mal.Id = ma.IdAccountLevel 
		where ma.LegalBookId = @bookId
		group by ma.Id, ma.Number, ma.Name, glb.IdThirdParty, ma.Nature, mac.[Type], mal.[Level], ma.AllowsMovement, mac.Code) as Inicial
		FULL JOIN 
		(select 
			ma.Id as IdMainAccount,
			ma.Number,
			ma.Name, 
			glb.IdThirdParty,
			SUM(glb.DebitValue) AS Debit,
			SUM(glb.CreditValue) as Credit,  
			case ma.nature 
				when 1 then  SUM(glb.DebitValue) - SUM(glb.CreditValue) 
				else    SUM(glb.CreditValue) -SUM(glb.DebitValue) 
			end as Saldo, 
			case ma.nature
				when 1 then 'Debito' 
				else 'Credito' 
			end as Naturaleza, 
			mac.[Type] as mainAccountType,
			mal.[Level], 
			ma.AllowsMovement,
			mac.Code as codeClass 
		from 
		(select
			IdMainAccount,  
			IdThirdParty, 
			DebitValue, 
			CreditValue 
		from GeneralLedger.GeneralLedgerBalance 
		where ([Year] = @ano and [Month] >= @mesInicial and [Month] <= @mesFinal )) as glb 
		right outer join GeneralLedger.MainAccounts as ma   on  ma.id = glb.IdMainAccount 
		inner join GeneralLedger.MainAccountClasses mac on mac.Id = ma.IdAccountClass 
		inner join GeneralLedger.MainAccountLevels mal on mal.Id = ma.IdAccountLevel 
		where ma.LegalBookId = @bookId
		group by ma.Id, ma.Number, ma.Name, glb.IdThirdParty, ma.Nature, mac.[Type], mal.[Level], ma.AllowsMovement, mac.Code) as Movimiento
		ON Inicial.IdMainAccount = Movimiento.IdMainAccount and ISNULL(Inicial.IdThirdParty,0) = ISNULL(Movimiento.IdThirdParty,0)
		LEFT JOIN Common.ThirdParty as tp on tp.Id = ISNULL (Inicial.IdThirdParty,Movimiento.IdThirdParty)
		order by CuentaContable
		
	end
	else begin
		INSERT INTO @tableExecution (mainAccountId,mainAccountCode,mainAccountName,nature, thirdPartyId, thirdPartyNit, thirdPartyName, valueDebitInitial, valueCreditInitial,previousBalance, valueDebitMovement, valueCreditMovement, newBalance,mainAccountClassType, mainAccountLevel,allowsMovement, classCode)
		SELECT 
			ISNULL (Inicial.IdMainAccount,Movimiento.IdMainAccount) as IdCuentaContable, 
			ISNULL (Inicial.Number,Movimiento.Number)  as CuentaContable,
			ISNULL(Inicial.Name,Movimiento.Name) as CuentaContableName, 
			ISNULL(Inicial.Naturaleza, Movimiento.Naturaleza)  as Naturaleza,
			0 as idTercero ,'' as Nit , '' as NombreTercero,   
			isnull(Inicial.Debit,0) as DebitoIncial,
			isnull(Inicial.Credit,0) as CreditoInicial,
			isnull(Inicial.Saldo,0) as SaldoInicial, 
			isnull(Movimiento.Debit,0) as DebitoMovimiento, 
			isnull(Movimiento.Credit,0) as CreditoMovimiento,
			isnull(Inicial.Saldo,0) + isnull(Movimiento.Saldo,0) as Saldo, 
			ISNULL(Inicial.mainAccountType, Movimiento.mainAccountType) as mainAccountType,
			ISNULL(Inicial.[Level], Movimiento.[Level]) as IdAccountLevel, 
			ISNULL(Inicial.AllowsMovement, Movimiento.AllowsMovement) as AllowsMovement,
			ISNULL(Inicial.codeClass,Movimiento.codeClass) as codeClass	
		FROM 
		(select
			ma.Id as IdMainAccount, 
			ma.Number,ma.Name,
			SUM(glb.DebitValue) AS Debit,
			SUM(glb.CreditValue) as Credit,  
			case ma.nature
				when 1 then  SUM(glb.DebitValue) - SUM(glb.CreditValue) 
				else    SUM(glb.CreditValue) -SUM(glb.DebitValue)
			end as Saldo, 
			case ma.nature
				when 1 then 'Debito' 
				else 'Credito' 
			end as Naturaleza,
			mac.[Type] as mainAccountType, 
			mal.[Level], ma.AllowsMovement, 
			mac.Code as codeClass
		from 
		(select
			IdMainAccount, 
			DebitValue, 
			CreditValue 
		from GeneralLedger.GeneralLedgerBalance
		where ([Year] = (@ano - 1) and [Month] = 14) or ([Year] = @ano and [Month] < @mesInicial)) as glb
		right outer join GeneralLedger.MainAccounts as ma on  ma.id = glb.IdMainAccount 
		inner join GeneralLedger.MainAccountClasses mac on mac.Id = ma.IdAccountClass
		inner join GeneralLedger.MainAccountLevels mal on mal.Id = ma.IdAccountLevel 
		where ma.LegalBookId = @bookId
		group by ma.Id, ma.Number, ma.Name, ma.Nature, mac.[Type], mal.[Level], ma.AllowsMovement, mac.Code) as Inicial
		FULL JOIN 
		(select
			ma.Id as IdMainAccount,
			ma.Number, 
			ma.Name,
			SUM(glb.DebitValue) AS Debit, 
			SUM(glb.CreditValue) as Credit,  
			case ma.nature
				when 1 then  SUM(glb.DebitValue) - SUM(glb.CreditValue)
				else    SUM(glb.CreditValue) -SUM(glb.DebitValue)
			end as Saldo, 
			case ma.nature
				when 1 then 'Debito'
				else 'Credito' 
			end as Naturaleza, 
			mac.[Type] as mainAccountType,
			mal.[Level], ma.AllowsMovement,
			mac.Code as codeClass 
		from 
		(select 
			IdMainAccount,
			DebitValue, 
			CreditValue
		from GeneralLedger.GeneralLedgerBalance 
		where ([Year] = @ano and [Month] >= @mesInicial and [Month] <= @mesFinal )) as glb 
		right outer join GeneralLedger.MainAccounts as ma   on  ma.id = glb.IdMainAccount 
		inner join GeneralLedger.MainAccountClasses mac on mac.Id = ma.IdAccountClass
		inner join GeneralLedger.MainAccountLevels mal on mal.Id = ma.IdAccountLevel 
		where ma.LegalBookId = @bookId 
		group by ma.Id, ma.Number, ma.Name, ma.Nature, mac.[Type], mal.[Level], ma.AllowsMovement, mac.Code) as Movimiento
		ON Inicial.IdMainAccount = Movimiento.IdMainAccount
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
					
					declare @debitInitialMayor as decimal(20,2)
					declare @creditInitialMayor as decimal(20,2)
					declare @previousBalanceMayor as decimal(20,2)
					declare @debitMovementMayor as decimal(20,2)
					declare @creditMovementMayor as decimal(20,2)
					declare @newBalanceMayor as decimal(20,2)
					
					select @debitInitialMayor = sum(ISNULL(te.valueDebitInitial,0))
					,@creditInitialMayor = sum(ISNULL(te.valueCreditInitial,0))
					,@previousBalanceMayor = sum(case te.nature when @natureAccount then ISNULL(te.previousBalance,0) else (-1 * ISNULL(te.previousBalance,0)) end)
					,@debitMovementMayor =sum(ISNULL(te.valueDebitMovement,0))
					,@creditMovementMayor = sum(ISNULL(te.valueCreditMovement,0))
					,@newBalanceMayor = sum(case te.nature when @natureAccount then ISNULL(te.newBalance,0) else (-1 * ISNULL(te.newBalance,0)) end) 
					from @TableExecution as te where te.mainAccountCode like @mainAccountCode + '%' and allowsMovement = 1

					
					Update @TableExecution set
					valueDebitInitial = ISNULL(@debitInitialMayor,0),
					valueCreditInitial = ISNULL(@creditInitialMayor,0),
					previousBalance = ISNULL(@previousBalanceMayor,0),
					valueDebitMovement = ISNULL(@debitMovementMayor,0),
					valueCreditMovement = ISNULL(@creditMovementMayor,0),
					newBalance = ISNULL(@newBalanceMayor,0)
					where Id = @IdAccount

					FETCH NEXT FROM detail_cursor
					INTO @IdAccount, @mainAccountCode, @natureAccount
					End
			close detail_cursor
			deallocate detail_cursor

	if @accountingZero = 0
	BEGIN
	DELETE FROM @tableExecution Where valueDebitInitial = 0 And valueCreditInitial = 0 And previousBalance = 0 And valueDebitMovement = 0 And valueCreditMovement = 0 And newBalance = 0 
	END
	--Select * from @tableExecution Where mainAccountLevel = @accountLevel and mainAccountCode between cast(@initialAccount as varchar(50)) and cast(@finalAccount as varchar(50)) order by mainAccountCode
	--update @tableExecution set currencyAbrreviation = @CurrencyAbbreviation
	Select * from @tableExecution Where mainAccountLevel = @accountLevel order by mainAccountCode
		
END