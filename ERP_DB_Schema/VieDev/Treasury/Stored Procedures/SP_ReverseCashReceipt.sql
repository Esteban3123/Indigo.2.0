-- =============================================
-- Author:		Carlos Cordoba
-- Create date: 09-08-2016
-- Description:	Reversa un recibo de caja
-- =============================================
CREATE PROCEDURE [Treasury].[SP_ReverseCashReceipt]
	@CashReceiptId as int,
	@User VARCHAR(20),
	@TreasuryNoteCode as Varchar(30)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @OperatingUnitId INT,
			@TreasuryNoteId INT,
			@TreasuryNoteDate DATETIME,
			----Currency------
			@CurrencyNoteId INT,
			@CurrencyOriginId INT,
			------Collectype---------
			@CollectType TINYINT,
			------------------------------
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	--tabla temporal para almacenar el resultado del movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	BEGIN TRY
		
		SELECT
			@OperatingUnitId = tn.OperatingUnitId,
			@TreasuryNoteId = tn.Id,
			@TreasuryNoteDate = tn.NoteDate,
			@CurrencyNoteId = tn.CurrencyId
		FROM Treasury.TreasuryNote tn WITH(NOLOCK)
		WHERE tn.Code = @TreasuryNoteCode AND tn.CashReceiptId = @CashReceiptId
		-----------------------------------------------------------------------------
		-----------------------------------------------------------------------------
		select	@CollectType = CollectType,
				@CurrencyOriginId = COALESCE(eba.CurrencyId,crt.CurrencyId)
		from Treasury.CashReceipts cr WITH(NOLOCK)
		LEFT JOIN Treasury.EntityBankAccounts eba WITH(NOLOCK) ON cr.IdBankAccount = eba.Id
		LEFT JOIN Treasury.CashRegisters crt WITH(NOLOCK) ON cr.IdCashRegister = crt.Id
		where cr.Id = @CashReceiptId 

		--Tipo de recaudo: 1 = Caja, 2 = Bancos
		if @CollectType = 1 BEGIN

			--variable que obtiene el valor del recibo de caja sin contar el valor del metodo de pago por redencion de puntos
			DECLARE @ValueCashReceiptWithoutPoints AS DECIMAL(18,2)

			---se obtiene el valor sin sumar las redenciones de puntos
			SELECT @ValueCashReceiptWithoutPoints = SUM(pm.Value)
			from Treasury.CashReceipts cr 
			JOIN Treasury.PaymentMethods pm ON pm.IdCashReceipt = cr.Id
			where cr.id= @CashReceiptId  AND pm.PaymentMethodTypes  <> 5

			--Caja
			--se inserta un registro en los saldos de tesoreria
			INSERT INTO [Treasury].[TreasuryBalance]
			([DocumentNumber],[DocumentDate],[DocumentType],[Nature],[CashRegisterId],[EntityBankAccountId],[PreviousBalance],[ValueMovement],[CreationDate])
			select @TreasuryNoteCode,@TreasuryNoteDate,4,2,c.Id,null,c.CurrentBalance,@ValueCashReceiptWithoutPoints ,[Common].[GETDATE]() 
			from Treasury.CashReceipts cr WITH(NOLOCK)
			inner join Treasury.CashRegisters c WITH(NOLOCK) on cr.IdCashRegister = c.Id
			where cr.id = @CashReceiptId 
			
			--se actualiza el saldo de la caja
			update Treasury.CashRegisters set CurrentBalance -= @ValueCashReceiptWithoutPoints
			from Treasury.CashReceipts cr 
			inner join Treasury.CashRegisters c on cr.IdCashRegister = c.id 
			where cr.id= @CashReceiptId 
		end
		else begin
			--Cuenta Bancaria
			--se inserta un registro en los saldos de tesoreria
			INSERT INTO [Treasury].[TreasuryBalance]
			([DocumentNumber],[DocumentDate],[DocumentType],[Nature],[CashRegisterId],[EntityBankAccountId],[PreviousBalance],[ValueMovement],[CreationDate])
			select @TreasuryNoteCode,@TreasuryNoteDate,4,2,null,eba.id,eba.CurrentBalance,cr.Value ,[Common].[GETDATE]()   from Treasury.CashReceipts cr 
			inner join Treasury.EntityBankAccounts  eba on cr.IdBankAccount  = eba.Id
			where cr.id = @CashReceiptId 

			--se actualiza el saldo de la cuenta bancaria
			update Treasury.EntityBankAccounts set CurrentBalance -= cr.Value 
			from Treasury.CashReceipts cr 
			inner join Treasury.EntityBankAccounts eba on cr.IdBankAccount = eba.id 
			where cr.id= @CashReceiptId 
		END

		if(select COUNT(*) from Portfolio.PortfolioAdvance where CashReceiptId = @CashReceiptId )>0 begin			
			
			if (select count(*) from Portfolio.PortfolioAdvance where Value<>Balance and CashReceiptId = @CashReceiptId )>0 begin
				SELECT '999' AS CodeMessage,'Los saldos de  los anticipos han cambiado' Message,0 as IdJournalVoucher,CAST(3 AS TINYINT) AS [Status];
				return
			end

			--actualizo el valor credito y el saldo
			update Portfolio.PortfolioAdvance set CreditValue = Balance  , Balance = 0
			from Portfolio.PortfolioAdvance where CashReceiptId = @CashReceiptId 
		END

		IF @CurrencyOriginId <> @CurrencyNoteId
		BEGIN
			SELECT '999' AS CodeMessage,'La moneda de la cuenta de origen no coincide con la moneda actual de la transacción' Message,0 as IdJournalVoucher,CAST(3 AS TINYINT) AS [Status];
			return
		END
	
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
		END

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
				[ConfirmationDate] [datetime] NULL,
				[CurrencyId] [int])

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
			select 0,0,0,@LegalBookId,st.JournalVoucherTypeTreasuryNotes,tn.NoteDate,0,2,'Reversión de Recibo de Caja con nota '+tn.Code ,tn.Code ,tn.Id,'TreasuryNote',0,@User,[Common].[GETDATE](),@User,[Common].[GETDATE](),@User,[Common].[GETDATE](),tn.CurrencyId
			from Treasury .TreasuryNote tn with(NOLOCK) -----
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
			declare @costCenterPayment INT
			DECLARE @paymentMethodType INT
			DECLARE payment_cursor CURSOR		

			FOR select ValueInCurrencyHeader,PaymentMethodTypes  from Treasury.PaymentMethods where IdCashReceipt = @CashReceiptId 
			
			OPEN payment_cursor;
                FETCH NEXT FROM payment_cursor INTO @valuePaymetCursos,@paymentMethodType
                WHILE @@FETCH_STATUS = 0 BEGIN				

				if(select CollectType from Treasury.CashReceipts where id = @CashReceiptId) = 1 begin		
					
					---si el metodo de pago es de redencion de puntos
					IF @paymentMethodType = 5 
					BEGIN
						select 
								@thirdPartyPayment = arp.CustomerId ,
								@mainAccountPayment = c.MainAccountReceivableId,
								@costCenterPayment = case when ma.HandlesCostCenter =1 then cr.IdCostCenter  end  
						from Treasury.CashReceipts cr
						inner join Treasury.CashRegisters cg on cr.IdCashRegister = cg.Id 
						JOIN Treasury.PaymentMethods pm ON pm.IdCashReceipt = cr.Id
						join Treasury.AgreementsRedemptionPoints arp on pm.IdAgreementsRedemptionPoints = arp.Id
						join Common.Customer c on arp.CustomerId = c.Id
						join GeneralLedger.MainAccounts ma on c.MainAccountReceivableId = ma.Id
						where cr.Id = @CashReceiptId 

						--cambiamos el estado de la cuenta por cobrar creada
						update Portfolio.AccountReceivable 
						set Status = 3 
							,Balance -= @valuePaymetCursos
						FROM Portfolio.AccountReceivable ar
						JOIN Treasury.AccountReceivableAgreementsRedemptionPoints ararp ON ar.Id = ararp.AccountReceivableId
						JOIN Treasury.CashReceipts cr ON ararp.CashReceiptsId = @CashReceiptId
						WHERE cr.Id= @CashReceiptId

					END
					ELSE
					BEGIN
						select @thirdPartyPayment = cg.ThirdPartyId ,
								@mainAccountPayment = cg.IdMainAccount,
								@costCenterPayment = case when ma.HandlesCostCenter =1 then cr.IdCostCenter  end  
						from Treasury.CashReceipts cr
						inner join Treasury.CashRegisters cg on cr.IdCashRegister = cg.Id 
						inner join GeneralLedger.MainAccounts ma on cg.IdMainAccount = ma.Id 
						where cr.Id = @CashReceiptId 
					
						select @thirdPartyPayment = case when st.GetThirdPartyCashRegister =0 then cr.IdThirdParty   else @thirdPartyPayment end 
						from Treasury.CashReceipts cr
						inner join Treasury.SettingsTreasury st on cr.OperatingUnitId = st.IdOperatingUnit 					
						where cr.Id = @CashReceiptId 
					END
		
				end
				else begin
				
					select @thirdPartyPayment = ba.ThirdPartyId , @mainAccountPayment = ba.IdMainAccount,@costCenterPayment = case when ma.HandlesCostCenter =1 then cr.IdCostCenter  else null end  
					from Treasury.CashReceipts cr
					inner join Treasury.EntityBankAccounts  ba on cr.IdBankAccount = ba.Id
					inner join GeneralLedger.MainAccounts ma on ba.IdMainAccount = ma.Id 
					where cr.id = @CashReceiptId 

					select @thirdPartyPayment = case when st.GetThirdPartyBank  =0 then cr.IdThirdParty  else @thirdPartyPayment end 
					from Treasury.CashReceipts cr
					inner join Treasury.SettingsTreasury st on cr.OperatingUnitId = st.IdOperatingUnit 
					where cr.Id = @CashReceiptId
				end

				insert into @JournalVoucherDetails select 0,0,@mainAccountPayment,@thirdPartyPayment ,case when  @costCenterPayment > 0 then @costCenterPayment else null end ,0,@valuePaymetCursos,null,null,null,null,null

			FETCH NEXT FROM payment_cursor INTO @valuePaymetCursos,@paymentMethodType
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

		update cr  
			set [Status] = 4 , 
				ReversedUser = @User,
				ReversedDate = tn.NoteDate,
				ModificationDate = [Common].[GETDATE](),
				ModificationUser = @User 
		FROM Treasury.CashReceipts cr
		JOIN Treasury.TreasuryNote tn ON cr.Id = tn.CashReceiptId
		WHERE cr.Id = @CashReceiptId

		/******************************** MODIFICACION RECAUDO PRESUPUESTAL *******************************/
		
		EXEC [Treasury].[SP_ReverseCollectionByCashReceiptId_Output] @OperatingUnitId, @TreasuryNoteId, @User, @Code_Output OUT, @Message_Output OUT
		
		IF @Code_Output <> 0
		BEGIN
			SELECT '999' AS CodeMessage, ISNULL(@Message_Output, 'No se pudo generar la modificación del recaudo presupuestal') AS Message, 0 as IdJournalVoucher, CAST(3 AS TINYINT) AS [Status]
			RETURN
		END

		if @JournalVoucherId > 0 
		BEGIN	
			SELECT 
				'0' as CodeMessage, 
				IIF(@Message_Output = '', '', @Message_Output + CHAR(13) + CHAR(10)) + 'Comprobante Contable '+jvt.Code + ' - ' + jvt.Name +', con consecutivo '+ cast(jv.Consecutive as varchar(30))  as Message,
				@JournalVoucherId as IdJournalVoucher,
				CAST(1 AS TINYINT) AS [Status]
			FROM GeneralLedger.JournalVouchers jv
				INNER JOIN GeneralLedger.JournalVoucherTypes jvt on jv.IdJournalVoucher = jvt.id
			WHERE jv.id = @JournalVoucherId
			RETURN
		END

		SELECT 
			'0' as CodeMessage, 
			jv.MessageResult as Message,
			@JournalVoucherId as IdJournalVoucher,
			CAST(1 AS TINYINT) AS [Status]
		FROM @resultJournalVoucher jv
		RETURN

	END TRY
	BEGIN CATCH
		SELECT '999' AS CodeMessage,ERROR_MESSAGE()+', Linea: '+CAST(ERROR_LINE() AS VARCHAR(20)) Message,0 as IdJournalVoucher,CAST(3 AS TINYINT) AS [Status];
		return
	END CATCH
	
--SELECT '999' AS CodeMessage,'' as [Message],0 as IdJournalVoucher,CAST(3 AS TINYINT) AS [Status];
	
END