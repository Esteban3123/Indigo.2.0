-- =============================================
-- Author:		Giovanny Plazas L
-- Create date: 29-04-2022
-- Description:	Devuelve un recibo de caja
-- =============================================
CREATE PROCEDURE [Treasury].[SP_DevolutionCashReceipt]
	--@CashReceiptId as int,
	--@CashRegisterId as int,
	--@User VARCHAR(20),
	--@TreasuryNoteCode as Varchar(30)
	@DevolutionCashReceiptXml AS XML
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @OperatingUnitId INT,
			@TreasuryNoteId INT,
			@CashReceiptId  int,
			@CashRegisterId  int,
			@User VARCHAR(20),
			@TreasuryNoteCode  Varchar(30),
			@NoteType  tinyint,
			@IdBankAccount  int,
			@TreasuryNoteDate DATETIME,
			------------------------------
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

			select
				@CashReceiptId= t.x.value('CashReceiptId[1]','int'),
				@CashRegisterId =t.x.value('CashRegisterId[1]','int'),
				@User=t.x.value('User[1]','Varchar(20)'),
				@TreasuryNoteCode = t.x.value('TreasuryNoteCode[1]','varchar(30)'),
				@NoteType =t.x.value('NoteType[1]','Tinyint'),
				@IdBankAccount =t.x.value('IdBankAccount[1]','Int')
			from @DevolutionCashReceiptXml.nodes('/DevolutionCashReceipt') t(x)

	--tabla temporal para almacenar el resultado del movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	BEGIN TRY
		SELECT
			@OperatingUnitId = tn.OperatingUnitId,
			@TreasuryNoteId = tn.Id,
			@TreasuryNoteDate =tn.NoteDate
		FROM Treasury.TreasuryNote tn
		WHERE tn.Code = @TreasuryNoteCode AND tn.CashReceiptId = @CashReceiptId


		if(select CollectType  from Treasury.CashReceipts where Id = @CashReceiptId ) = 1 begin
			--Caja
			--se inserta un registro en los saldos de tesoreria
			INSERT INTO [Treasury].[TreasuryBalance]
			([DocumentNumber],[DocumentDate],[DocumentType],[Nature],[CashRegisterId],[EntityBankAccountId],[PreviousBalance],[ValueMovement],[CreationDate])
			select	@TreasuryNoteCode,@TreasuryNoteDate,4,2,c.Id,null,c.CurrentBalance,cr.Value ,[Common].[GETDATE]()   
			from	Treasury.CashReceipts cr 
			inner join Treasury.CashRegisters c on c.Id = @CashRegisterId
			where cr.id = @CashReceiptId 

			--se actualiza el saldo de la caja
			update Treasury.CashRegisters 
					set CurrentBalance -=cr.Value 
			from Treasury.CashReceipts cr 
			inner join Treasury.CashRegisters c on  c.id = @CashRegisterId
			where cr.id= @CashReceiptId 
		end
		else begin
			--Cuenta Bancaria
			--se inserta un registro en los saldos de tesoreria
			INSERT INTO [Treasury].[TreasuryBalance]
			([DocumentNumber],[DocumentDate],[DocumentType],[Nature],[CashRegisterId],[EntityBankAccountId],[PreviousBalance],[ValueMovement],[CreationDate])
			select @TreasuryNoteCode,@TreasuryNoteDate,4,2,null,eba.id,eba.CurrentBalance,cr.Value ,[Common].[GETDATE]()   from Treasury.CashReceipts cr 
			inner join Treasury.EntityBankAccounts  eba on  eba.Id = @IdBankAccount
			where cr.id = @CashReceiptId 

			--se actualiza el saldo de la cuenta bancaria
			update Treasury.EntityBankAccounts set CurrentBalance -=cr.Value 
			from Treasury.CashReceipts cr 
			inner join Treasury.EntityBankAccounts  eba on  eba.id = @IdBankAccount
			where cr.id= @CashReceiptId 
		end
		--valido si el recibo de caja tiene anticipos
		if(select COUNT(*) from Portfolio.PortfolioAdvance where CashReceiptId = @CashReceiptId )>0 begin			
			if (select count(*) from Portfolio.PortfolioAdvance where Value<>Balance and CashReceiptId = @CashReceiptId )>0 begin
				SELECT '999' AS CodeMessage,'Los saldos de  los anticipos han cambiado' Message,0 as IdJournalVoucher,CAST(3 AS TINYINT) AS [Status];
				return
			end
			--actualizo el valor credito y el saldo
			update Portfolio.PortfolioAdvance set CreditValue = Balance  , Balance = 0
			from Portfolio.PortfolioAdvance where CashReceiptId = @CashReceiptId 
		end
		--si existen detalles con comportamiento 2- Cancelacion / Abonos Facturas CxC
		if(select COUNT(*) from Treasury.CashReceiptDetails where IdCashReceipt = @CashReceiptId and CashReceiptConceptAffectation = 2)>0 begin
			--actualizo accountReceivable
			update Portfolio.AccountReceivable  set Balance += crar.Value 
			from Treasury.CashReceiptDetails crd
			inner join CashReceiptAccountReceivable crar on crd.id = crar.CashReceiptDetailId 
			inner join Portfolio.AccountReceivable ar on crar.AccountReceivableId = ar.id 
			where crd.CashReceiptConceptAffectation = 2 and crd.IdCashReceipt = @CashReceiptId 
			--actualizo el accountReceivableAccounting
			update Portfolio.AccountReceivableAccounting   set Balance += crar.Value 
			from Treasury.CashReceiptDetails crd
			inner join CashReceiptAccountReceivable crar on crd.id = crar.CashReceiptDetailId 
			inner join Portfolio.AccountReceivable ar on crar.AccountReceivableId = ar.id
			inner join Portfolio.AccountReceivableAccounting ara on ar.id = ara.AccountReceivableId and crd.IdMainAccount = ara.MainAccountId  
			where crd.CashReceiptConceptAffectation = 2 and crd.IdCashReceipt = @CashReceiptId 
			--actulizo el accountReceivableShare
			update Portfolio.AccountReceivableShare  set Balance += crars.Value ,PaymentValue -=crars.Value
			from Treasury.CashReceiptDetails crd
			inner join Treasury.CashReceiptAccountReceivable crar on crd.id = crar.CashReceiptDetailId 
			inner join Treasury.CashReceiptAccountReceivableShare crars on crar.Id = crars.CashReceiptAccountReceivableId 
			inner join Portfolio.AccountReceivableShare ars on crars.AccountReceivableShareId = ars.Id
			where crd.CashReceiptConceptAffectation = 2 and crd.IdCashReceipt = @CashReceiptId 
		end
		
		--si existen detalles con comportamiento 3-  Reintegro de Anticipos a Proveedores
		if(select COUNT(*) from Treasury.CashReceiptDetails where IdCashReceipt = @CashReceiptId and CashReceiptConceptAffectation = 3)>0 begin
			update Payments.AdvancePayments set  Balance += crap.PaymentValue 
			from Treasury.CashReceiptDetails crd
			inner join Treasury.CashReceiptAdvancePayment crap on crd.Id= crap.CashReceiptDetailId 
			inner join Payments.AdvancePayments ap on crap.AdvancePaymentId = ap.Id 
			where crd.CashReceiptConceptAffectation = 3 and crd.IdCashReceipt = @CashReceiptId 
		end

		--si existen detalles con comportamiento 4-  Reintegro de cuentas por pagar
		if(select COUNT(*) from Treasury.CashReceiptDetails where IdCashReceipt = @CashReceiptId and CashReceiptConceptAffectation = 4)>0 begin
		print '4'
			declare @idAccountPayableCursor int,@refundValueCursor numeric(18,2),@idShareCursor int,@balanceShareCursor numeric(18,2)					

			DECLARE bill_cursor CURSOR
			FOR 
				select crdap.AccountPayableId ,crdap.RefundValue  from Treasury.CashReceiptDetails crd
				inner join Treasury.CashReceiptDetailAccountPayable crdap on crd.id=crdap.CashReceiptDetailId 
				where crd.CashReceiptConceptAffectation = 4 and crd.IdCashReceipt = @CashReceiptId 
			OPEN bill_cursor
			FETCH NEXT FROM bill_cursor INTO  @idAccountPayableCursor,@refundValueCursor
            WHILE @@FETCH_STATUS = 0 BEGIN
			

				DECLARE share_cursor CURSOR					
				FOR 
					select Id , Balance  from Payments.AccountPayableShares where IdAccountPayable = @idAccountPayableCursor and Balance >0
				OPEN share_cursor;
	
				FETCH NEXT FROM share_cursor INTO @idShareCursor,@balanceShareCursor
				WHILE @@FETCH_STATUS = 0 BEGIN	
				
					if (@refundValueCursor<=0)begin
						break
					end				
					if(@refundValueCursor > @balanceShareCursor )begin
						update Payments.AccountPayableShares set DebitValue += @balanceShareCursor ,Balance -= @balanceShareCursor where id = @idShareCursor 
						set @refundValueCursor -= @balanceShareCursor
					end
					else begin
						update Payments.AccountPayableShares set DebitValue += @refundValueCursor ,Balance -= @refundValueCursor where id = @idShareCursor 
						break
					end
				FETCH NEXT FROM share_cursor INTO @idShareCursor,@balanceShareCursor
				END
				CLOSE share_cursor;
				DEALLOCATE share_cursor;

			FETCH NEXT FROM bill_cursor INTO @idAccountPayableCursor,@refundValueCursor
            END;
            CLOSE bill_cursor;
            DEALLOCATE bill_cursor;

			--actualizo el saldo de la factura
			update Payments.AccountPayable set Balance = shares.sumBalance 
			from Treasury.CashReceiptDetails crd
			inner join Treasury.CashReceiptDetailAccountPayable cap on crd.id = cap.CashReceiptDetailId 
			inner join (select cap.AccountPayableId , SUM(aps.Balance) as sumBalance 
				from Treasury.CashReceiptDetails crd
				inner join Treasury.CashReceiptDetailAccountPayable cap on crd.id = cap.CashReceiptDetailId 
				inner join Payments.AccountPayable ap on cap.AccountPayableId = ap.Id 
				inner join Payments.AccountPayableShares aps on ap.Id = aps.IdAccountPayable where crd.IdCashReceipt = @CashReceiptId   group by cap.AccountPayableId) shares on cap.AccountPayableId = shares .AccountPayableId 
			inner join Payments.AccountPayable ac on cap.AccountPayableId = ac.Id 
			where crd.IdCashReceipt = @CashReceiptId 
		end

		--Generacion comprobante contable
		declare @JournalVouchers as table(
				[Id] [int] NOT NULL,
				[AccountingMovementId] [int] NOT NULL,
				[Consecutive] [bigint] NOT NULL,
				[LegalBookId] [int] NOT NULL ,
				[IdJournalVoucher] [int] NOT NULL,
				[VoucherDate] [datetime] NOT NULL,
				[Imported] [bit] NOT NULL,
				[Status] [tinyint] NOT NULL,
				[Detail] [varchar](500) NULL ,
				[EntityCode] [varchar](20) NULL,
				[EntityId] [int] NULL,
				[EntityName] [varchar](250) NULL,
				[IsClosedYear] [bit] NOT NULL,
				[CreationUser] [varchar](20) NOT NULL,
				[CreationDate] [datetime] NOT NULL,
				[ModificationUser] [varchar](20) NULL,
				[ModificationDate] [datetime] NULL,
				[ConfirmationUser] [varchar](20) NULL,
				[ConfirmationDate] [datetime] NULL)

			declare @JournalVoucherDetails as table(
				[Id] [int] NOT NULL,
				[IdAccounting] [int] NOT NULL,
				[IdMainAccount] [int] NOT NULL,
				[IdThirdParty] [int] NULL,
				[IdCostCenter] [int] NULL,
				[DebitValue] [decimal](18, 2) NOT NULL ,
				[CreditValue] [decimal](18, 2) NOT NULL ,
				[Detail] [varchar](max) NULL ,
				[IdRetention] [int] NULL,
				[RetentionRate] [decimal](5, 3) NULL,
				[BaseValue] [decimal](18, 0) NULL ,
				[BillingValue] [decimal](18, 0) NULL)


			--inserto la cabecera del comprobante
			--Obtengo el libro oficial
			declare @LegalBookId integer
			select @LegalBookId = id  from GeneralLedger.LegalBook where OfficialBook = 1

			insert into @JournalVouchers 
			select 0,0,0,@LegalBookId,st.JournalVoucherTypeTreasuryNotes,tn.NoteDate,0,2,'Reversión de Recibo de Caja con nota '+tn.Code ,tn.Code ,tn.Id,'TreasuryNote',0,@User,[Common].[GETDATE](),@User,[Common].[GETDATE](),@User,[Common].[GETDATE]()
			from Treasury .TreasuryNote tn
			inner join Treasury.SettingsTreasury st on tn.OperatingUnitId = st.IdOperatingUnit 
			where tn.Code = @TreasuryNoteCode AND tn.CashReceiptId = @CashReceiptId 


			--inserto los detalles del comprobante por los detalles del recibo de caja
			insert into @JournalVoucherDetails 
			select 0,0,crdTmp.IdMainAccount ,crdTmp.IdThirdParty,crdTmp.IdCostCenter,case when crdTmp.Nature  =  1 then 0 else crdTmp.Value end as DebitValue,case when crdTmp.Nature  =  1 then crdTmp.Value else 0 end as CreditValue,crdTmp.Detail ,crdTmp.IdRetentionConcept ,crdTmp.PercentageRetention,crdTmp.BaseValue ,crdTmp.BillingValue  
			from Treasury.CashReceiptDetails crdTmp where IdCashReceipt = @CashReceiptId 

			--inserto los detalles por los metodos de pago
			declare @valuePaymetCursos numeric(18,2)
			declare @mainAccountPayment int
			declare @thirdPartyPayment int
			declare @costCenterPayment int
			DECLARE payment_cursor CURSOR		

			FOR select Value  from Treasury.PaymentMethods where IdCashReceipt = @CashReceiptId 
			
			OPEN payment_cursor;
                FETCH NEXT FROM payment_cursor INTO @valuePaymetCursos
                WHILE @@FETCH_STATUS = 0 BEGIN

				
				
				if(select CollectType  from Treasury.CashReceipts where id = @CashReceiptId) = 1 begin		
					
					select @thirdPartyPayment = cg.ThirdPartyId , @mainAccountPayment = cg.IdMainAccount,@costCenterPayment = case when ma.HandlesCostCenter =1 then cr.IdCostCenter  end  
					from Treasury.CashReceipts cr
					inner join Treasury.CashRegisters cg on  cg.Id = @CashRegisterId
					inner join GeneralLedger.MainAccounts ma on cg.IdMainAccount = ma.Id 
					where cr.Id = @CashReceiptId 
					
					select @thirdPartyPayment = case when st.GetThirdPartyCashRegister =0 then cr.IdThirdParty   else @thirdPartyPayment end 
					from Treasury.CashReceipts cr
					inner join Treasury.SettingsTreasury st on cr.OperatingUnitId = st.IdOperatingUnit 					
					where cr.Id = @CashReceiptId 

					
				end
				else begin
				
					select @thirdPartyPayment = ba.ThirdPartyId , @mainAccountPayment = ba.IdMainAccount,@costCenterPayment = case when ma.HandlesCostCenter =1 then cr.IdCostCenter  else null end  
					from Treasury.CashReceipts cr
					inner join Treasury.EntityBankAccounts  ba on  ba.Id = @IdBankAccount
					inner join GeneralLedger.MainAccounts ma on ba.IdMainAccount = ma.Id 
					where cr.id = @CashReceiptId 

					select @thirdPartyPayment = case when st.GetThirdPartyBank  =0 then cr.IdThirdParty  else @thirdPartyPayment end 
					from Treasury.CashReceipts cr
					inner join Treasury.SettingsTreasury st on cr.OperatingUnitId = st.IdOperatingUnit 
					where cr.Id = @CashReceiptId
				end

				insert into @JournalVoucherDetails select 0,0,@mainAccountPayment,@thirdPartyPayment ,case when  @costCenterPayment > 0 then @costCenterPayment else null end ,0,@valuePaymetCursos,null,null,null,null,null

			FETCH NEXT FROM payment_cursor INTO @valuePaymetCursos
			END;
			CLOSE payment_cursor;
			DEALLOCATE payment_cursor;


			--genero el XML para guardar el comprobante 
			
			declare @JournalVoucherXML as XML
			select @JournalVoucherXML =  convert(xml, (select * from @JournalVouchers JournalVoucher inner join @JournalVoucherDetails JournalVoucherDetail on JournalVoucher.id = JournalVoucherDetail.IdAccounting   For xml AUTO,TYPE, ELEMENTS))
		
			insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,@User 
		
			if (select code  from @resultJournalVoucher) = '999' 
			Begin			
				PRINT CAST(@JournalVoucherXML AS VARCHAR(MAX))
				declare @errorJV varchar(max)
				select @errorJV = MessageResult  from @resultJournalVoucher 
				select '999' as CodeMessage,@errorJV as Message,0 as IdJournalVoucher,CAST(3 AS TINYINT) AS [Status]
				return 
			End  

			declare @JournalVoucherId as int			
			select @JournalVoucherId = IdJournalVoucher  from @resultJournalVoucher
			
		--fin comprobante contable
		
			--update cr  
			--	set [Status] = 4 , 
			--		ReversedUser = @User,
			--		ReversedDate = tn.NoteDate,
			--		ModificationDate = [Common].[GETDATE](),
			--		ModificationUser = @User 
			--FROM Treasury.CashReceipts cr
			--JOIN Treasury.TreasuryNote tn ON cr.Id = tn.CashReceiptId
			--WHERE cr.Id = @CashReceiptId

		/******************************** MODIFICACION RECAUDO PRESUPUESTAL *******************************/

		EXEC [Treasury].[SP_ReverseCollectionByCashReceiptId_Output] @OperatingUnitId, @TreasuryNoteId, @User, @Code_Output OUT, @Message_Output OUT

		IF @Code_Output <> 0
		BEGIN
			SELECT '999' AS CodeMessage, ISNULL(@Message_Output, 'No se pudo generar la modificación del recaudo presupuestal') AS Message, 0 as IdJournalVoucher, CAST(3 AS TINYINT) AS [Status]
			RETURN
		END

		select 
			'0' as CodeMessage, 
			IIF(@Message_Output = '', '', @Message_Output + CHAR(13) + CHAR(10)) + 'Comprobante Contable '+jvt.Code + ' - ' + jvt.Name +', con consecutivo '+ cast(jv.Consecutive as varchar(30))  as Message,
			@JournalVoucherId as IdJournalVoucher,
			CAST(1 AS TINYINT) AS [Status]
		from GeneralLedger.JournalVouchers jv
		inner join GeneralLedger.JournalVoucherTypes jvt on jv.IdJournalVoucher = jvt.id
		where jv.id = @JournalVoucherId
		return

	END TRY
	BEGIN CATCH
		SELECT '999' AS CodeMessage,ERROR_MESSAGE()+', Linea: '+CAST(ERROR_LINE() AS VARCHAR(20)) Message,0 as IdJournalVoucher,CAST(3 AS TINYINT) AS [Status];
		return
	END CATCH
	
--SELECT '999' AS CodeMessage,'' as [Message],0 as IdJournalVoucher,CAST(3 AS TINYINT) AS [Status];
	
END