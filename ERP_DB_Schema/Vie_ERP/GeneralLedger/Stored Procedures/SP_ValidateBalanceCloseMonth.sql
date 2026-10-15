

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ValidateBalanceCloseMonth]
	@Period as varchar(2),
	@Year as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--- Estado del mensaje 1 - Correcto 2 - Advertencia 3 - Error 4 - Desbalanceado
	declare @TableResult table(CodeMessage varchar(10), Message varchar(max), [Status] tinyint,Consecutive bigint null,CodeNameJournalVoucherTye varchar(100) null, ConfirmationDate datetime null,Detail varchar(500) null,DebitValue numeric(18,2) null,CreditValue numeric(18,2) null)
    
	begin try
		declare @debitvalue as numeric(18,2)
		declare @creditValue as numeric(18,2)

		select @debitvalue = ROUND(sum(DebitValue),-1),
			   @creditValue = ROUND(sum(CreditValue),-1)
		from GeneralLedger.JournalVoucherDetails jvd 
		inner join GeneralLedger.JournalVouchers jv on jvd.IdAccounting = jv.id 
		inner join GeneralLedger.LegalBook lg on jv.legalBookId = lg.id
		where MONTH( jv.VoucherDate ) = @Period  and YEAR(jv.VoucherDate ) = @Year and lg.OfficialBook = 1 and jv.Status = 2

		
		if (@debitvalue <> @creditValue ) begin
			--si los detalles de los comprobantes estan desbalanceados
			insert into @TableResult (CodeMessage,Message,[Status],Consecutive,CodeNameJournalVoucherTye,ConfirmationDate,Detail,DebitValue,CreditValue)
			SELECT '999',
					'', 
					cast(4 as tinyint) as [Status], 
					jv.Consecutive, jvt.Code + ' - ' + jvt.Name as CodeNameJournalVoucherTye, 
					jv.ConfirmationDate,
					CAST(jv.Detail AS VARCHAR(500)), 
					ROUND(sum(DebitValue),-1) AS DebitValue, 
					ROUND(sum(CreditValue),-1) as CreditValue
			FROM GeneralLedger.JournalVouchers AS JV 
			INNER JOIN GeneralLedger.JournalVoucherDetails AS JVD ON JV.Id = JVD.IdAccounting 
			INNER JOIN GeneralLedger.JournalVoucherTypes jvt on jv.IdJournalVoucher = jvt.id 
			inner join GeneralLedger.LegalBook lg on jv.legalBookId = lg.id
			WHERE JV.ID IN (SELECT DISTINCT JV.ID FROM GeneralLedger.JournalVouchers AS JV INNER JOIN GeneralLedger.JournalVoucherDetails AS JVD ON JV.Id = JVD.IdAccounting 
			WHERE lg.OfficialBook = 1 and MONTH( jv.VoucherDate ) = @Period  and YEAR(jv.VoucherDate ) = @Year and jv.Status = 2)
			GROUP BY  jv.IdJournalVoucher, jv.Consecutive, jv.ConfirmationDate, jv.Detail,jvt.Code , jvt.Name

			select * from @TableResult 
			return 
		end
		--valido que los debitos y los creditos para el periodo seleccionado sean iguales
		select @debitvalue = ROUND(sum(DebitValue),-1),@creditValue = ROUND(sum(CreditValue),-1)
		from GeneralLedger.GeneralLedgerBalance 
		where [Month] =@Period  and [Year] =@Year

		if (@debitvalue <> @creditValue ) begin
			insert into @TableResult 
			select '999' as CodeMessage,'Los saldos para el periodo seleccionado estan desbalanceados' as Message, cast(4 as tinyint) as [Status]  ,null,null,null,null,null,null 
			select * from @TableResult 
			return		
		end

		--Proceso termino correctamente
		insert into @TableResult (CodeMessage,Message,[Status])		
		select '0' as  CodeMessage,'Proceso finalizado correctamente ' as Message ,cast(1 as tinyint) as [Status] 		
		select * from @TableResult 
		return 
	end try
	begin catch
	select '999' CodeMessage, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(5)) Message, cast(3 as tinyint) as [Status]
	end catch
    
	
END