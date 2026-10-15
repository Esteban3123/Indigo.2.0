
 /*=============================================
 Author:		Giovanny Plazas
 Create date: 2020-03-30
 Description:	Procedimiento que se encarga de hacer la confirmacion de la reversion de una cuenta por pagar en el formulario NOTA debito/credito
 =============================================*/
CREATE PROCEDURE [Payments].[SP_AccountPayableReversal]
    @PaymentNotesXml AS XML,
	@CodeUser as varchar(20),
	----------------------
	@CodeMessage as INT Output,
	@Message as Varchar(Max) Output,
	@IdJournalVoucherResult as Int Output
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/

	--Tabla con las Notas del Módulo de Cuentas por Pagar a confirmar
	DECLARE @TablePaymentNote TABLE
	(
		PaymentNoteId INT NOT NULL,
		Code VARCHAR(20) NOT NULL,
		JournalVoucherTypeId INT
	)

		--Tabla Respuesta a Retornar
	DECLARE @TableResult TABLE
	(
		CodeMessage INT ,
		Message VARCHAR(MAX),
		IdJournalVoucherResult INT
	)
		DECLARE @JournalVoucher TABLE 
	(
	    Id INT DEFAULT(0),
		Consecutive BIGINT DEFAULT(0),
		LegalBookId INT,
		IdJournalVoucher INT,
		VoucherDate VARCHAR(30),
		Imported VARCHAR(5) DEFAULT('False'),
		Status TINYINT,
		Detail VARCHAR(MAX),
		EntityCode VARCHAR(20),
		EntityId INT,
		EntityName VARCHAR(250),
		OriginEntityName VARCHAR(250),
		IsClosedYear TINYINT DEFAULT(0),
		CurrencyId INT
	)

	--Se declara una tabla temporal para los detalles del comprobante
	DECLARE @JournalVoucherDetail TABLE 
	(
	    Id INT DEFAULT(0),
		IdAccounting INT DEFAULT(0),
		IdMainAccount INT,
		IdThirdParty INT,
		IdCostCenter INT,
		DebitValue DECIMAL(18,2),
		CreditValue DECIMAL(18,2),
		Detail VARCHAR(MAX),
		IdRetention INT,
		RetentionRate DECIMAL(6,3),
		BaseValue DECIMAL(18,2),
		BillingValue DECIMAL(18,2),
		AccountPayableTempId INT
	)

	--Variable para obtener el xml
	DECLARE 
		 @JournalVoucherXML as xml,
		 @OfficialCurrencyId as INT,
		 @TaxRegistration tinyint

	--tabla temporal para almacenar el resultado del movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	BEGIN TRY

	--Moneda Oficial
		select @OfficialCurrencyId = cs.OfficialCurrencyId
		from GeneralLedger.CompanySettings cs
		print @TaxRegistration
--Se obtienen los datos de las Notas del Módulo de Cuentas por Pagar a confirmar
		INSERT INTO @TablePaymentNote 
			(PaymentNoteId, Code)
			SELECT 
				t.x.value('Id[1]','int'),
				t.x.value('Code[1]','varchar(20)')
			FROM @PaymentNotesXml.nodes('/PaymentNotes') t(x)
/*---------------------------------------------------------------------------*/
/* Primero validaremos que la cuenta por pagar tenga causación diferida */
		IF EXISTS (

			SELECT 1 FROM Payments.DeferredCausation dc WITH (NOLOCK)
			INNER JOIN Payments.AccountPayable ap WITH (NOLOCK) ON ap.Id = dc.IdAccountPayable
			INNER JOIN Payments.PaymentNotes pn WITH (NOLOCK) ON pn.IdAccountPayable = ap.Id
			INNER JOIN @TablePaymentNote tpn ON tpn.PaymentNoteId = pn.Id
			WHERE pn.Id = tpn.PaymentNoteId AND NOT EXISTS (
				
				SELECT 1 FROM Payments.DeferredCausationShare dcs WITH (NOLOCK)
				WHERE dcs.DeferredCausationId = dc.Id AND dcs.Amortized = 1

			)
		)	
			BEGIN
				UPDATE Payments.DeferredCausation 
				SET Status = 3
				WHERE IdAccountPayable IN (
					SELECT ap.Id FROM Payments.AccountPayable ap WITH (NOLOCK) 
					INNER JOIN Payments.PaymentNotes pn WITH (NOLOCK) ON pn.IdAccountPayable =  ap.Id
					INNER JOIN @TablePaymentNote tpn ON tpn.PaymentNoteId = pn.Id
					WHERE pn.Id = tpn.PaymentNoteId
				)
			END
/* se insertan los datos de la reversion para generar el comprobante */
			INSERT INTO @JournalVoucherDetail
					(	
					IdMainAccount,
					IdThirdParty,
					IdCostCenter,
					Detail, 
					DebitValue,
					CreditValue,
					IdRetention,
					RetentionRate,
					BaseValue,
					BillingValue,
					AccountPayableTempId
					)

				SELECT	ap.IdAccount AS IdMainAccount,
						ap.IdThirdParty AS IdThirdParty,
						ap.IdCostCenter AS IdCostCenter,
						NULL AS Detail,
						ap.Value as DebitValue,
						0 CreditValue,
						NULL AS IdRetention,
						NULL AS RetentionRate,
						NULL AS BaseValue,
						NULL AS BillingValue,
						ap.id as AccountPayableId
						
				from @TablePaymentNote tp
				 JOIN Payments.PaymentNotes pn with (NOLOCK) ON tp.PaymentNoteId =pn.Id
				LEFT join Payments.AccountPayable ap with (NOLOCK) ON pn.IdAccountPayable = ap.Id

				UNION ALL
				SELECT
						apd.IdAccount AS IdMainAccount,
						apd.IdThirdParty AS IdThirdParty,
						apd.IdCostCenter AS IdCostCenter,
						IIF(apd.IsDirectCost = 1, ISNULL(ap.Coments + CHAR(13) + CHAR(10), ''), '') + apd.Detail AS Detail,
						IIF(apd.Nature = 1, 0,apd.value) as DebitValue,
						IIF(apd.Nature = 1, apd.value, 0) as CreditValue,
						apd.IdRetentionConcept AS IdRetention,
						apd.Percentage AS RetentionRate,
						apd.BaseValue AS BaseValue,
						IIF(apd.BillingValue = 0, ap.InvoiceValue, apd.BillingValue) AS BillingValue,
						apd.IdAccountPayable as AccountPayableId
					FROM @TablePaymentNote tp
					left JOIN Payments.PaymentNotes pn with (NOLOCK) ON tp.PaymentNoteId = pn.Id
					left JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH (NOLOCK) ON pn.Id = pnapa.PaymentNoteId
					left JOIN Payments.AccountPayable ap with (NOLOCK) ON ISNULL( pnapa.AccountPayableId ,pn.IdAccountPayable) = ap.Id
					left JOIN Payments.AccountPayableDetailConcept apd  with (NOLOCK) ON ap.Id = apd.IdAccountPayable
					WHERE (apd.RateIva is null OR (ap.EntityName is not null AND ap.EntityName = 'EntranceVoucher'))

				---si tiene iva descontable(se hace registro del concepto y otra del iva-debito)

					UNION ALL
					SELECT
							apd.IdAccount AS IdMainAccount,
							apd.IdThirdParty AS IdThirdParty,
							apd.IdCostCenter AS IdCostCenter,
							IIF(apd.IsDirectCost = 1, ISNULL(ap.Coments + CHAR(13) + CHAR(10), ''), '') + apd.Detail AS Detail,
							IIF(apd.Nature = 1, 0, apd.BaseValue) AS DebitValue,
							IIF(apd.Nature = 1, apd.BaseValue, 0) AS CreditValue,
							apd.IdRetentionConcept AS IdRetention,
							apd.Percentage AS RetentionRate,
							apd.BaseValue AS BaseValue,
							IIF(apd.BillingValue = 0, ap.InvoiceValue, apd.BillingValue) AS BillingValue,
							apd.IdAccountPayable as AccountPayableId
					FROM @TablePaymentNote tp
					left JOIN Payments.PaymentNotes pn with (NOLOCK) ON tp.PaymentNoteId = pn.Id
					left JOIN Payments.AccountPayable ap with (NOLOCK) ON pn.IdAccountPayable = ap.Id
					left JOIN Payments.AccountPayableDetailConcept apd  with (NOLOCK) ON ap.Id = apd.IdAccountPayable
					WHERE apd.RateIva is not null and ap.TaxRegistration=2 and (ap.EntityName IS NULL OR ap.EntityName <> 'EntranceVoucher')

					UNION ALL
					SELECT
							gli.IdAccountPurchaseService AS IdMainAccount,
							apd.IdThirdParty AS IdThirdParty,
							NULL AS IdCostCenter,
							IIF(apd.IsDirectCost = 1, ISNULL(ap.Coments + CHAR(13) + CHAR(10), ''), '') + apd.Detail AS Detail,
							0 AS DebitValue,
							apd.IvaValue AS CreditValue,
							NULL AS IdRetention,
							NULL AS RetentionRate,
							apd.BaseValue AS BaseValue,
							NULL AS BillingValue,
							apd.IdAccountPayable as AccountPayableId

					from @TablePaymentNote tp
					left JOIN Payments.PaymentNotes pn with (NOLOCK) ON tp.PaymentNoteId = pn.Id
					left JOIN Payments.AccountPayable ap WITH(NOLOCK) ON pn.IdAccountPayable = ap.Id
					left JOIN Payments.AccountPayableDetailConcept apd WITH(NOLOCK) ON ap.Id = apd.IdAccountPayable
					left JOIN GeneralLedger.GeneralLedgerIVA gli on apd.RateIva = gli.Id
					where apd.RateIva is not null and ap.TaxRegistration=2 and (ap.EntityName IS NULL OR ap.EntityName <> 'EntranceVoucher')

				/**********************IVA AL COSTO (CONTROL FISCAL) ****************************/
				UNION ALL
				SELECT
					apd.IdAccount AS IdMainAccount,
					apd.IdThirdParty AS IdThirdParty,
					apd.IdCostCenter AS IdCostCenter,
					IIF(apd.IsDirectCost = 1, ISNULL(ap.Coments + CHAR(13) + CHAR(10), ''), '') + apd.Detail AS Detail,
					IIF(apd.Nature = 1, 0, apd.Value) AS DebitValue,
					IIF(apd.Nature = 1, apd.Value, 0) AS CreditValue,
					apd.IdRetentionConcept AS IdRetention,
					apd.Percentage AS RetentionRate,
					apd.TotalConcept AS BaseValue,
					IIF(apd.BillingValue = 0, ap.InvoiceValue, apd.BillingValue) AS BillingValue,
					apd.IdAccountPayable as AccountPayableId

				FROM @TablePaymentNote tp
				left JOIN Payments.PaymentNotes pn with (NOLOCK) ON tp.PaymentNoteId = pn.Id
				left JOIN Payments.AccountPayable ap WITH(NOLOCK) ON pn.IdAccountPayable = ap.Id
				left JOIN Payments.AccountPayableDetailConcept apd WITH(NOLOCK) ON ap.Id = apd.IdAccountPayable
				WHERE apd.RateIva is not null  and (ap.TaxRegistration=1)  and (ap.EntityName IS NULL OR ap.EntityName <> 'EntranceVoucher')
				
				UNION ALL
				SELECT
					gli.IdAccountDebitControlFiscal AS IdMainAccount,
					apd.IdThirdParty AS IdThirdParty,
					NULL AS IdCostCenter,
					IIF(apd.IsDirectCost = 1, ISNULL(ap.Coments + CHAR(13) + CHAR(10), ''), '') + apd.Detail AS Detail,
					0 AS DebitValue,
					apd.IvaValue AS CreditValue,
					NULL AS IdRetention,
					NULL AS RetentionRate,
					apd.BaseValue AS BaseValue,
					NULL AS BillingValue,
					apd.IdAccountPayable as AccountPayableId

				FROM @TablePaymentNote tp
				left JOIN Payments.PaymentNotes pn with (NOLOCK) ON tp.PaymentNoteId = pn.Id
				left JOIN Payments.AccountPayable ap WITH(NOLOCK) ON pn.IdAccountPayable = ap.Id
				left JOIN Payments.AccountPayableDetailConcept apd WITH(NOLOCK) ON ap.Id = apd.IdAccountPayable
				left JOIN GeneralLedger.GeneralLedgerIVA gli on apd.RateIva = gli.Id
				WHERE apd.RateIva is not null and (ap.TaxRegistration=1) and (ap.EntityName IS NULL OR ap.EntityName <> 'EntranceVoucher')
				
				UNION ALL
				SELECT
					gli.IdAccountCreditControlFiscal AS IdMainAccount,
					apd.IdThirdParty AS IdThirdParty,
					NULL AS IdCostCenter,
					IIF(apd.IsDirectCost = 1, ISNULL(ap.Coments + CHAR(13) + CHAR(10), ''), '') + apd.Detail AS Detail,
					apd.IvaValue AS DebitValue,
					0 AS CreditValue,
					NULL AS IdRetention,
					NULL AS RetentionRate,
					apd.BaseValue AS BaseValue,
					NULL AS BillingValue,
					apd.IdAccountPayable as AccountPayableId

				FROM @TablePaymentNote tp
				left JOIN Payments.PaymentNotes pn with (NOLOCK) ON tp.PaymentNoteId = pn.Id
				left JOIN Payments.AccountPayable ap WITH(NOLOCK) ON pn.IdAccountPayable = ap.Id
				left JOIN Payments.AccountPayableDetailConcept apd WITH(NOLOCK) ON ap.Id = apd.IdAccountPayable
				left JOIN GeneralLedger.GeneralLedgerIVA gli on apd.RateIva = gli.Id
				WHERE apd.RateIva is not null and (ap.TaxRegistration=1) and (ap.EntityName IS NULL OR ap.EntityName <> 'EntranceVoucher')

				/************************* IVA AL COSTO ****************************/
					UNION ALL
				SELECT
					apdc.IdAccount AS IdMainAccount,
					apdc.IdThirdParty AS IdThirdParty,
					apdc.IdCostCenter AS IdCostCenter,
					IIF(apdc.IsDirectCost = 1, ISNULL(ap.Coments + CHAR(13) + CHAR(10), ''), '') + apdc.Detail AS Detail,
					IIF(apdc.Nature = 1, 0, apdc.BaseValue) AS DebitValue,
					IIF(apdc.Nature = 1, apdc.BaseValue,0) AS CreditValue,
					apdc.IdRetentionConcept AS IdRetention,
					apdc.Percentage AS RetentionRate,
					apdc.BaseValue AS BaseValue,
					IIF(apdc.BillingValue = 0, ap.InvoiceValue, apdc.BillingValue) AS BillingValue,
					apdc.IdAccountPayable as AccountPayableId
				FROM @TablePaymentNote tap
				left JOIN Payments.PaymentNotes pn with (NOLOCK) ON tap.PaymentNoteId = pn.Id
				left JOIN Payments.AccountPayable ap WITH(NOLOCK) ON pn.IdAccountPayable = ap.Id
				left JOIN Payments.AccountPayableDetailConcept apdc WITH(NOLOCK) ON ap.Id = apdc.IdAccountPayable
				where apdc.RateIva is not null and (ap.TaxRegistration =4) and (ap.EntityName IS NULL OR ap.EntityName <> 'EntranceVoucher')

				UNION ALL
				SELECT
					apdc.IdAccount AS IdMainAccount,
					apdc.IdThirdParty AS IdThirdParty,
					apdc.IdCostCenter AS IdCostCenter,
					CONCAT('IVA ',gli.Percentage,' %',' - ',gli.Name) AS Detail,
					IIF(apdc.Nature = 1, 0, apdc.IvaValue) AS DebitValue,
					IIF(apdc.Nature = 1, apdc.IvaValue, 0) AS CreditValue,
					NULL AS IdRetention,
					NULL AS RetentionRate,
					apdc.BaseValue AS BaseValue,
					NULL AS BillingValue,
					apdc.IdAccountPayable as AccountPayableId
				FROM @TablePaymentNote tap
				left JOIN Payments.PaymentNotes pn with (NOLOCK) ON tap.PaymentNoteId = pn.Id
				left JOIN Payments.AccountPayable ap WITH(NOLOCK) ON pn.IdAccountPayable = ap.Id
				left JOIN Payments.AccountPayableDetailConcept apdc WITH(NOLOCK) ON ap.Id = apdc.IdAccountPayable
				left JOIN GeneralLedger.GeneralLedgerIVA gli on gli.Id = apdc.RateIva
				where apdc.RateIva is not null and (ap.TaxRegistration = 4) and (ap.EntityName IS NULL OR ap.EntityName <> 'EntranceVoucher')

				BEGIN
				/*------------------------------------------------------------*/
				/* Se actualiza el balanace de la cuenta por pagar de la tabla AccountPayable*/
				BEGIN
				UPDATE ap
				SET ap.Balance = (SELECT SUM(DebitValue) -SUM(CreditValue) as NewBalance
								from @JournalVoucherDetail)
				FROM @TablePaymentNote tp
				left JOIN Payments.PaymentNotes pn with (NOLOCK) ON tp.PaymentNoteId =pn.Id
				left join Payments.AccountPayable ap WITH (NOLOCK) ON pn.IdAccountPayable = ap.Id
				WHERE ap.Id = pn.IdAccountPayable
				END
			/*----------------------------------------------------------------------------------*/
			/* Se actualiza el balanace de la cuenta por pagar de la tabla AccountPayableShare*/
				BEGIN
				UPDATE aps
				SET aps.Balance = (SELECT SUM(DebitValue) -SUM(CreditValue) as NewBalance
								from @JournalVoucherDetail)
				FROM @TablePaymentNote tp
				left JOIN Payments.PaymentNotes pn with (NOLOCK) ON tp.PaymentNoteId =pn.Id
				left join Payments.AccountPayable ap WITH (NOLOCK) ON pn.IdAccountPayable = ap.Id
				LEFT JOIN Payments.AccountPayableShares aps WITH (NOLOCK) ON ap.Id = aps.IdAccountPayable
				WHERE ap.Id = pn.IdAccountPayable
				END

				/* se actualiza en la tabla @TablePaymentNote el Id del Journal Voucher*/
				BEGIN
					UPDATE tap
						SET tap.JournalVoucherTypeId = sp.IdJournalVoucherAccountPayable
					FROM @TablePaymentNote tap
					JOIN Payments.PaymentNotes pn with (NOLOCK) ON tap.PaymentNoteId = pn.Id
					JOIN Payments.AccountPayable ap with (NOLOCK) ON pn.IdAccountPayable = ap.Id
					JOIN Payments.SettingPayments sp with (NOLOCK) ON ap.IdOperatingUnit = sp.IdOperatingUnit
					WHERE tap.JournalVoucherTypeId IS NULL
					END
				END

				/* se insertan los datos de cabecera para adjuntar al XML para GENERAR el comprobante Contable*/
				BEGIN
				INSERT INTO @JournalVoucher
					( LegalBookId, IdJournalVoucher, VoucherDate, Status, Detail, EntityCode, EntityId, EntityName, CurrencyId)
					SELECT TOP 1
						NULL LegalBookId, 
						tap.JournalVoucherTypeId IdJournalVoucher, 
						pn.NoteDate VoucherDate, 
						2 Status, 
						dbo.CleanSpecialChars(pn.Comment) Detail,
						tap.Code EntityCode,
						pn.Id EntityId,
						'PaymentNotes' EntityName
						,ISNULL(pn.CurrencyId,@OfficialCurrencyId)
					FROM @TablePaymentNote tap
					JOIN Payments.PaymentNotes pn with (NOLOCK) ON tap.PaymentNoteId = pn.Id
				END

				BEGIN
				--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
				SELECT @JournalVoucherXML = CONVERT(xml, 
					(
						SELECT * FROM @JournalVoucher JournalVoucher
						join @JournalVoucherDetail JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
						For xml AUTO,TYPE, ELEMENTS
					)
				)
				SELECT @CodeMessage = NULL, @Message = NULL, @IdJournalVoucherResult = NULL

				--Se consume el sp que guarda el movimiento contable
				insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,@CodeUser 

				select 
				@CodeMessage = rjv.code, 
				@Message = rjv.MessageResult, 
				@IdJournalVoucherResult = rjv.IdJournalVoucher
				from @resultJournalVoucher rjv

				END

				/*Si la nota de reversión se hace a una CxP que provenga del formulario "Distribucion de Elementos del Costo"
				 se actualiza el estado de esta a 4 = reversado*/

				 declare @IdCostDistributionDirectCost as INT = 0

				select  @IdCostDistributionDirectCost =cddc.Id
				from @TablePaymentNote tpn
				join Payments.PaymentNotes pn WITH(NOLOCK) on tpn.PaymentNoteId = pn.Id
				join Payments.AccountPayable ap on pn.IdAccountPayable = ap.Id
				join Cost.CostDistributionDirectCost cddc on cddc.AccountPayableId = ap.Id
				where ap.CostDistributionDirectCostId = cddc.Id

				if @IdCostDistributionDirectCost <> 0
				begin 
					UPDATE Cost.CostDistributionDirectCost 
					SET Status = 4
					WHERE Id = @IdCostDistributionDirectCost
				END

				IF EXISTS (SELECT 1 FROM Cost.CostDistributionDirectCostLegalizedDocuments cddcld
							WHERE cddcld.DistributionDirectCostId = @IdCostDistributionDirectCost
				) BEGIN

                    EXEC Cost.SP_ReverseProvisionDocumentDistributionDirectCost @IdCostDistributionDirectCost, @CodeUser, 3
                END

				BEGIN
				INSERT  @TableResult (CodeMessage, Message,IdJournalVoucherResult)
				select @CodeMessage as CodeMessage,  @Message as Message,  @IdJournalVoucherResult as IdJournalVoucherResult
				RETURN
				END	
		END TRY
		BEGIN CATCH
		INSERT @TableResult (CodeMessage, Message)
			SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message
		END CATCH

			SELECT tr.CodeMessage , tr.Message,tr.IdJournalVoucherResult
			from @TableResult tr	
END