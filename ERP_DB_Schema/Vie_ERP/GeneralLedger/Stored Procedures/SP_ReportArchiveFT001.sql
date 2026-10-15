-- =============================================
-- Author:      Carlos jhefersson Muñoz
-- Create date: 03/08/2017
-- Description: Procedimiento para el reporte de ArchivoFT001
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportArchiveFT001]

    @mesFinal int ,
    @Year int,
    @bookId as int,
	@accountLevel as int,
	@accountStart as varchar(50),
	@accountEnd as varchar(50)
AS
BEGIN
    --Tabla donde se acumula los saldos de los meses anteriores y del mes 14 del año anterior
	declare @TableBalance table(Id int identity(1,1), IdAccount int, Number varchar(50), NameAccount varchar(100), balanceCurrent decimal(20,4), nature tinyint, allowsMovement bit, [Availability] tinyint, mainAccountLevel int)
    
	declare @TableBalanceMayor table(Id int identity(1,1), IdAccount int, Number varchar(50), NameAccount varchar(100), balanceCurrent decimal(20,4), nature tinyint, allowsMovement bit, [Availability] tinyint, mainAccountLevel int)

	INSERT INTO @TableBalance
		SELECT 
			ma.id,
			ma.Number, 
			ma.[Name],
			isnull(case ma.Nature when 1 then sum(gb.DebitValue) - sum(gb.CreditValue) when 2 then sum(gb.CreditValue) - sum(gb.DebitValue) end,0) as Balance, 
			ma.Nature,
			ma.AllowsMovement,
			case ma.AllowsMovement when 0 then 0 else ma.[Availability] end,
			mal.[Level]
		FROM GeneralLedger.GeneralLedgerBalance as gb
			inner join GeneralLedger.MainAccounts as ma on  ma.id = gb.IdMainAccount
			inner join GeneralLedger.MainAccountLevels mal on mal.Id = ma.IdAccountLevel
		WHERE ma.LegalBookId = @bookId and ((gb.[Month] = 14 and gb.[Year] = @Year - 1) or (gb.[Month] <= @mesFinal and gb.[Year] = @Year))
			and (gb.CreditValue <> 0 Or gb.DebitValue <> 0)
		GROUP BY ma.id, ma.Number, ma.[Name], ma.Nature, ma.AllowsMovement, ma.[Availability], mal.[Level]
		HAVING (SUM(gb.CreditValue) <> SUM(gb.DebitValue))

    --mayorizamos los saldos a nivel de subcuenta
    declare @IdAccount int
    declare @Number varchar(50)
	declare @NameAccount varchar(50)
    declare @natureAccount varchar(10)
	declare @Level int

    declare detail_cursor cursor for
		select ma.Id, ma.Number, ma.[Name], ma.Nature, mal.[level] 
		from GeneralLedger.MainAccounts as ma 
			inner join GeneralLedger.MainAccountLevels as mal on mal.id = ma.IdAccountLevel 
		where ma.allowsMovement = 0 and mal.Level < @accountLevel and ma.LegalBookId = @bookId

		open detail_cursor
			FETCH NEXT FROM detail_cursor
			INTO @IdAccount, @Number, @NameAccount, @natureAccount, @Level

			WHILE @@FETCH_STATUS = 0
			BEGIN
                  
				INSERT @TableBalanceMayor
					select @IdAccount, @Number, @NameAccount, balanceCurrent, @natureAccount, 0, [Availability], @Level from (
						select SUM(te.balanceCurrent) as balanceCurrent, [Availability]
						from @TableBalance as te where te.Number like @Number + '%' and allowsMovement = 1
						group by [Availability]
					) as Mayor
                      
				FETCH NEXT FROM detail_cursor
				INTO @IdAccount, @Number, @NameAccount, @natureAccount, @Level
			End
		close detail_cursor
    deallocate detail_cursor

	INSERT INTO @TableBalanceMayor
		SELECT IdAccount, Number, NameAccount, balanceCurrent, nature, allowsMovement, [Availability], mainAccountLevel FROM @TableBalance
    	
	SELECT Number,NameAccount,round(balanceCurrent,0),[Availability], mainAccountLevel 
	FROM @TableBalanceMayor
	WHERE mainAccountLevel <= case @accountLevel when 5 then (select max([Level]) from GeneralLedger.MainAccountLevels) else @accountLevel end
	ORDER BY Number, [Availability]
END