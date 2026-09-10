-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-26
-- Description:	Reversa un comprobante de egreso
-- =============================================
create PROCEDURE [Treasury].[SP_ReverseVoucherTransaction]
	@TreasuryNoteId INT,	
	@CodeUser VARCHAR(20)	
AS
BEGIN
	SET NOCOUNT ON;

	/********************************************* VARIABLES ********************************************/
	
	DECLARE @TreasuryNoteCode VARCHAR(20),
			@TreasuryNoteDate DATETIME,
			@VoucherTransactionId INT,
			@VoucherTransactionCode VARCHAR(20), -- >>> FIX CIMA #52141
			@ExpenseType TINYINT,
			@VoucherClass INT,
			------------------------------
			@errors VARCHAR(MAX)

	/**************************************** VARIABLES CONTABLES ***************************************/

	--Se declara una tabla con los datos para la cabecera del comprobante contable 
	DECLARE @JournalVourcherTmp TABLE 
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
	DECLARE @JournalVourcherDetailTmp TABLE 
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
		BillingValue DECIMAL(18,2)
	)

	--Variable para obtener el xml
	DECLARE @JournalVoucherTypeId AS INT,
			--------------------------------------
			@JournalVoucherXML XML,
			@CodeMessage INT,
			@Message VARCHAR(MAX),
			@IdJournalVoucherResult INT,
			--------------------------------------
			@Consecutive VARCHAR(30)

	DECLARE @CodeMessageResultReimbursementResource INT,
			@MessageResultReimbursementResource VARCHAR(MAX)

	--tabla temporal para almacenar el resultado deL movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	/**************************************** ******************* ***************************************/

	BEGIN TRY

		SELECT 
			@TreasuryNoteCode = tn.Code,
			@TreasuryNoteDate = tn.NoteDate,
			@VoucherTransactionId = tn.VoucherTransactionId,			
			@JournalVoucherTypeId = st.JournalVoucherTypeTreasuryNotes,
			@ExpenseType = vt.ExpenseType,
			@VoucherClass = vt.VoucherClass,
			@VoucherTransactionCode = vt.Code -- >>> FIX CIMA #52141
		FROM Treasury.TreasuryNote tn		
		JOIN Treasury.SettingsTreasury st ON tn.OperatingUnitId = st.IdOperatingUnit
		JOIN Treasury.VoucherTransaction vt ON tn.VoucherTransactionId = vt.Id
		WHERE tn.Id = @TreasuryNoteId

		/***************************************** VALIDACIONES ****************************************/
		
		IF NOT EXISTS (SELECT 1 FROM Treasury.VoucherTransaction vt WHERE vt.Id = @VoucherTransactionId AND vt.Status = 2)
		BEGIN
			SELECT 999 AS CodeMessage, 'El registro no existe o no se encuentra confirmado' AS [Message], 0 AS IdJournalVoucher, '' AS Consecutive
			RETURN
		END

		IF EXISTS 
		(
			SELECT 1
			FROM Treasury.VoucherTransaction vt
			JOIN Treasury.VoucherTransactionDetails vtd ON vt.Id = vtd.IdVoucherTransaction
			JOIN Treasury.ExpenseConcepts ec ON vtd.IdExpenseConcept = ec.Id
			JOIN Payments.AdvancePayments ap ON vt.Code = ap.Code
			WHERE vt.Id = @VoucherTransactionId
				AND vt.VoucherClass = 1
				AND ec.Behavior = 3
				AND ap.Value <> ap.Balance
		)
		BEGIN
			SELECT 999 AS CodeMessage, 'El registro no se puede confirmar porque el anticipo con código ' + vt.Code + ' ya fue afectado por otro proceso' AS [Message], 0 AS IdJournalVoucher, '' AS Consecutive
			FROM Treasury.VoucherTransaction vt
			WHERE vt.Id = @VoucherTransactionId
			RETURN
		END

		--Si es un reembolso debo retornar error (actualmente no se permite realizar una reversion de reembolsos)
		IF @VoucherClass = 2
		BEGIN
			SELECT 999 AS CodeMessage, 'El registro no se puede confirmar porque el sistema no permite reversar un comprobante de egreso de reembolso' AS [Message], 0 AS IdJournalVoucher, '' AS Consecutive
			RETURN
		END

		--Si es un traslado debo validar saldos
		IF @VoucherClass = 3 /**************************** TRASLADOS  *****************************/
		BEGIN
			IF @ExpenseType = 1
			BEGIN
				IF EXISTS
				(
					SELECT 1
					FROM Treasury.EntityBankAccounts eba
					JOIN
					(
						SELECT vtd.IdEntityBankAccount, SUM(vtd.Value) Value
						FROM Treasury.VoucherTransactionDetails vtd
						WHERE vtd.IdVoucherTransaction = @VoucherTransactionId
						GROUP BY vtd.IdEntityBankAccount
					) vtd ON vtd.IdEntityBankAccount = eba.Id
					WHERE vtd.Value > eba.CurrentBalance
				)
				BEGIN
					SELECT @errors = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + eba.Code + ': Saldo Actual (' + CAST(eba.CurrentBalance AS VARCHAR(50)) + ') - Valor Comprobante de Egreso (' + CAST(vtd.Value AS VARCHAR(50)) + ')'
							FROM Treasury.EntityBankAccounts eba
							JOIN
							(
								SELECT vtd.IdEntityBankAccount, SUM(vtd.Value) Value
								FROM Treasury.VoucherTransactionDetails vtd
								WHERE vtd.IdVoucherTransaction = @VoucherTransactionId
								GROUP BY vtd.IdEntityBankAccount
							) vtd ON vtd.IdEntityBankAccount = eba.Id
							WHERE vtd.Value > eba.CurrentBalance
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT 999 AS CodeMessage, 
						   'El registro no se puede confirmar porque las siguientes entidades bancarias no tienen el saldo suficiente: ' + CHAR(13) + CHAR(10) + ISNULL(@errors, '') AS [Message], 
						   0 AS IdJournalVoucher, 
						   '' AS Consecutive
					RETURN
				END
			END
			ELSE IF @ExpenseType = 3
			BEGIN
				IF EXISTS
				(
					SELECT 1
					FROM Treasury.CashRegisters cr
					JOIN
					(
						SELECT vtd.CashRegisterId, SUM(vtd.Value) Value
						FROM Treasury.VoucherTransactionDetails vtd
						WHERE vtd.IdVoucherTransaction = @VoucherTransactionId
						GROUP BY vtd.CashRegisterId
					) vtd ON vtd.CashRegisterId = cr.Id
					WHERE vtd.Value > cr.CurrentBalance
				)
				BEGIN
					SELECT @errors = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cr.Code + ': Saldo Actual (' + CAST(cr.CurrentBalance AS VARCHAR(50)) + ') - Valor Comprobante de Egreso (' + CAST(vtd.Value AS VARCHAR(50)) + ')'
							FROM Treasury.CashRegisters cr
							JOIN
							(
								SELECT vtd.CashRegisterId, SUM(vtd.Value) Value
								FROM Treasury.VoucherTransactionDetails vtd
								WHERE vtd.IdVoucherTransaction = @VoucherTransactionId
								GROUP BY vtd.CashRegisterId
							) vtd ON vtd.CashRegisterId = cr.Id
							WHERE vtd.Value > cr.CurrentBalance
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT 999 AS CodeMessage, 
						   'El registro no se puede confirmar porque las siguientes cajas no tienen el saldo suficiente: ' + CHAR(13) + CHAR(10) + ISNULL(@errors, '') AS [Message], 
						   0 AS IdJournalVoucher, 
						   '' AS Consecutive
					RETURN
				END
			END
		END

		/************************************ PROCESO BALANCE Y SALDOS *********************************/

		IF @ExpenseType = 1
		BEGIN
			/***************************************** BALANCE *****************************************/
			INSERT INTO [Treasury].[TreasuryBalance]
			(
				[DocumentNumber],[DocumentDate],[DocumentType],[Nature],[CashRegisterId],[EntityBankAccountId],[PreviousBalance],[ValueMovement],[CreationDate]
			)
			SELECT 
				@TreasuryNoteCode, @TreasuryNoteDate, 4, 1, NULL, eba.Id, eba.CurrentBalance, 
				(
					vt.Value + IIF(vt.TaxByMil = 1, vt.TaxByMilValue, 0)
				), [Common].[GETDATE]()
			FROM Treasury.VoucherTransaction vt
			JOIN Treasury.EntityBankAccounts eba ON vt.IdEntityBankAccount = eba.Id
			WHERE vt.Id = @VoucherTransactionId

			/****************************************** BANCO ******************************************/
			UPDATE eba 
				SET eba.CurrentBalance = eba.CurrentBalance + (vt.Value + IIF(vt.TaxByMil = 1, vt.TaxByMilValue, 0)),
					eba.ModificationUser = @CodeUser,
					eba.ModificationDate = [Common].[GETDATE]()
			FROM Treasury.VoucherTransaction vt
			JOIN Treasury.EntityBankAccounts eba ON vt.IdEntityBankAccount = eba.Id
			WHERE vt.Id = @VoucherTransactionId
		END
		ELSE IF @ExpenseType IN (2, 3)
		BEGIN
			/***************************************** BALANCE *****************************************/
			INSERT INTO [Treasury].[TreasuryBalance]
			(
				[DocumentNumber],[DocumentDate],[DocumentType],[Nature],[CashRegisterId],[EntityBankAccountId],[PreviousBalance],[ValueMovement],[CreationDate]
			)
			SELECT 
				@TreasuryNoteCode, @TreasuryNoteDate, 4, 1, cr.Id, NULL, cr.CurrentBalance, 
				(
					vt.Value + IIF(vt.TaxByMil = 1, vt.TaxByMilValue, 0)
				), [Common].[GETDATE]()
			FROM Treasury.VoucherTransaction vt
			JOIN Treasury.CashRegisters cr ON vt.IdCashRegister = cr.Id
			WHERE vt.Id = @VoucherTransactionId

			/****************************************** CAJA *******************************************/
			UPDATE cr 
				SET cr.CurrentBalance = cr.CurrentBalance + (vt.Value + IIF(vt.TaxByMil = 1, vt.TaxByMilValue, 0)),
					cr.ModificationUser = @CodeUser,
					cr.ModificationDate = [Common].[GETDATE]()
			FROM Treasury.VoucherTransaction vt
			JOIN Treasury.CashRegisters cr ON vt.IdCashRegister = cr.Id
			WHERE vt.Id = @VoucherTransactionId
		END

		/*********************************** PROCESO POR COMPORTAMIENTO ********************************/

		IF @VoucherClass = 1 /********************************* PAGO  **********************************/
		BEGIN
			/**************************** CAJA MENOR - PAGO / ANTICIPO CxP  ****************************/
			UPDATE aps
				SET aps.Balance = aps.Balance + db.AdvancedValue,
					aps.PaymentValue = aps.PaymentValue - db.AdvancedValue
			FROM Treasury.VoucherTransaction vt
			JOIN Treasury.VoucherTransactionDetails vtd ON vt.Id = vtd.IdVoucherTransaction
			JOIN Treasury.ExpenseConcepts ec ON vtd.IdExpenseConcept = ec.Id
			JOIN Treasury.DischargeBill db ON vtd.Id = db.IdVoucherTransactionD
			JOIN Payments.AccountPayableShares aps ON db.IdAccountPayableShare = aps.Id
			WHERE vt.Id = @VoucherTransactionId
				AND ec.Behavior IN (2, 3)

			UPDATE ap
				SET ap.Balance = ap.Balance + db.AdvancedValue
			FROM Treasury.VoucherTransaction vt
			JOIN Treasury.VoucherTransactionDetails vtd ON vt.Id = vtd.IdVoucherTransaction
			JOIN Treasury.ExpenseConcepts ec ON vtd.IdExpenseConcept = ec.Id
			JOIN Treasury.DischargeBill db ON vtd.Id = db.IdVoucherTransactionD
			JOIN Payments.AccountPayableShares aps ON db.IdAccountPayableShare = aps.Id
			JOIN Payments.AccountPayable ap ON aps.IdAccountPayable = ap.Id
			WHERE vt.Id = @VoucherTransactionId
				AND ec.Behavior IN (2, 3)

			INSERT INTO Payments.MovementAccountPayables 
				(
					IdAccountPayable, IdAccountPayableShare, EntityId, EntityCode, EntityName, MovementDate, Value
				)
				SELECT
					aps.IdAccountPayable, aps.Id, @TreasuryNoteId, @TreasuryNoteCode, 'TreasuryNote', @TreasuryNoteDate, db.AdvancedValue
				FROM Treasury.VoucherTransaction vt
				JOIN Treasury.VoucherTransactionDetails vtd ON vt.Id = vtd.IdVoucherTransaction
				JOIN Treasury.ExpenseConcepts ec ON vtd.IdExpenseConcept = ec.Id
				JOIN Treasury.DischargeBill db ON vtd.Id = db.IdVoucherTransactionD
				JOIN Payments.AccountPayableShares aps ON db.IdAccountPayableShare = aps.Id
				WHERE vt.Id = @VoucherTransactionId
					AND ec.Behavior IN (2, 3)

			/*********************************** PAGO / ANTICIPO CxP  **********************************/
			UPDATE ap SET Balance = 0
			FROM Treasury.VoucherTransaction vt
			JOIN Treasury.VoucherTransactionDetails vtd ON vt.Id = vtd.IdVoucherTransaction
			JOIN Treasury.ExpenseConcepts ec ON vtd.IdExpenseConcept = ec.Id
			JOIN Payments.AdvancePayments ap ON vt.Code = ap.Code
			WHERE vt.Id = @VoucherTransactionId
				AND ec.Behavior = 3

			/********************************* DEVOLUTIVO ANTICIPO RC  *********************************/
			UPDATE pa
				SET pa.Balance = pa.Balance + vta.Value
			FROM Treasury.VoucherTransaction vt
			JOIN Treasury.VoucherTransactionDetails vtd ON vt.Id = vtd.IdVoucherTransaction
			JOIN Treasury.ExpenseConcepts ec ON vtd.IdExpenseConcept = ec.Id
			JOIN Treasury.VoucherTransactionAdvance vta ON vtd.Id = vta.IdVoucherTransactionD
			JOIN Portfolio.PortfolioAdvance pa ON vta.PortfolioAdvanceId = pa.Id
			WHERE vt.Id = @VoucherTransactionId
				AND ec.Behavior = 4
		END
		ELSE IF @VoucherClass = 2 /**************************** REEMBOLSOS  *****************************/
		BEGIN
			--Devolvemos error, actualmente no se ha desarrollado la logica para devolución de reembolsos
			SELECT 999 AS CodeMessage, 'El sistema no permite reversar un comprobante de egreso de reembolso' AS [Message], 0 AS IdJournalVoucher, '' AS Consecutive
			RETURN
		END
		ELSE IF @VoucherClass = 3 /**************************** TRASLADOS  *****************************/
		BEGIN
			IF @ExpenseType = 1 /********************* ENTRE CUENTAS BANCARIAS  ***********************/
			BEGIN
				/*************************************** BALANCE ***************************************/
				INSERT INTO [Treasury].[TreasuryBalance]
				(
					[DocumentNumber],[DocumentDate],[DocumentType],[Nature],[CashRegisterId],[EntityBankAccountId],[PreviousBalance],[ValueMovement],[CreationDate]
				)
				SELECT 
					@TreasuryNoteCode, @TreasuryNoteDate, 4, 2, NULL, eba.Id, eba.CurrentBalance, vtd.Value, [Common].[GETDATE]()
				FROM Treasury.VoucherTransaction vt
				JOIN Treasury.VoucherTransactionDetails vtd ON vt.Id = vtd.IdVoucherTransaction
				JOIN Treasury.EntityBankAccounts eba ON vtd.IdEntityBankAccount = eba.Id
				WHERE vt.Id = @VoucherTransactionId

				/**************************************** BANCO ****************************************/
				UPDATE eba 
					SET eba.CurrentBalance = eba.CurrentBalance - vtd.Value,
						eba.ModificationUser = @CodeUser,
						eba.ModificationDate = [Common].[GETDATE]()
				FROM Treasury.VoucherTransaction vt
				JOIN Treasury.VoucherTransactionDetails vtd ON vt.Id = vtd.IdVoucherTransaction
				JOIN Treasury.EntityBankAccounts eba ON vtd.IdEntityBankAccount = eba.Id
				WHERE vt.Id = @VoucherTransactionId
			END
			ELSE IF @ExpenseType = 3 /********************** ENTRE CAJAS MAYORES  *********************/
			BEGIN
				/*************************************** BALANCE ***************************************/
				INSERT INTO [Treasury].[TreasuryBalance]
				(
					[DocumentNumber],[DocumentDate],[DocumentType],[Nature],[CashRegisterId],[EntityBankAccountId],[PreviousBalance],[ValueMovement],[CreationDate]
				)
				SELECT 
					@TreasuryNoteCode, @TreasuryNoteDate, 4, 2, cr.Id, NULL, cr.CurrentBalance, vtd.Value, [Common].[GETDATE]()
				FROM Treasury.VoucherTransaction vt
				JOIN Treasury.VoucherTransactionDetails vtd ON vt.Id = vtd.IdVoucherTransaction
				JOIN Treasury.CashRegisters cr ON vtd.CashRegisterId = cr.Id
				WHERE vt.Id = @VoucherTransactionId

				/**************************************** CAJA *****************************************/
				UPDATE cr 
					SET cr.CurrentBalance = cr.CurrentBalance - vtd.Value,
						cr.ModificationUser = @CodeUser,
						cr.ModificationDate = [Common].[GETDATE]()
				FROM Treasury.VoucherTransaction vt
				JOIN Treasury.VoucherTransactionDetails vtd ON vt.Id = vtd.IdVoucherTransaction
				JOIN Treasury.CashRegisters cr ON vtd.CashRegisterId = cr.Id
				WHERE vt.Id = @VoucherTransactionId
			END
		END

		/**************************************** PROCESO CONTABLE *************************************/

		INSERT INTO @JournalVourcherTmp
		(
			IdJournalVoucher, 
			VoucherDate, 
			Status, 
			Detail, 
			EntityCode, 
			EntityId, 
			EntityName,
			CurrencyId
		)
		SELECT
			@JournalVoucherTypeId, 
			tn.NoteDate, 
			2, 
			'Reversión de Comprobante de Egreso con Nota ' +  tn.Code + '. ' + tn.Description, 
			tn.Code, 
			tn.Id, 
			'TreasuryNote',
			tn.CurrencyId
		FROM Treasury.TreasuryNote tn
		WHERE tn.Id = @TreasuryNoteId

		INSERT INTO @JournalVourcherDetailTmp
		(
			IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, 
			IdRetention, RetentionRate, BaseValue, BillingValue
		)
		SELECT
			ma.Id AS IdMainAccount,
			IIF
			(
				ma.HandlesThirdParty = 1, 
				CASE
					WHEN vt.VoucherClass = 1 THEN vtd.IdThirdParty --payment
					WHEN vt.VoucherClass = 2 THEN cr.ThirdPartyId --refund
					WHEN vt.ExpenseType = 1 THEN eba.ThirdPartyId --bank account
					ELSE cr.ThirdPartyId 
				END,
				NULL
			) AS IdThirdParty,
			IIF(ma.HandlesCostCenter = 1, vtd.IdCostCenter, NULL) AS IdCostCenter,
			IIF(vtd.Nature = 1, 0, IIF(vtd.discountableIVA = 0, vtd.TotalConcept, vtd.Value)) DebitValue,
			IIF(vtd.Nature = 1, IIF(vtd.discountableIVA = 0, vtd.TotalConcept, vtd.Value), 0) CreditValue,
			vtd.Observation,
			vtd.IdRetentionConcept,
			vtd.PercentRetention,
			vtd.BaseValue,
			vtd.BillingValue
		FROM Treasury.VoucherTransaction vt
		JOIN Treasury.VoucherTransactionDetails vtd ON vt.Id = vtd.IdVoucherTransaction
		JOIN GeneralLedger.MainAccounts ma ON vtd.IdMainAccount = ma.Id
		LEFT JOIN Treasury.EntityBankAccounts eba ON vtd.IdEntityBankAccount = eba.Id
		LEFT JOIN Treasury.CashRegisters cr ON vtd.CashRegisterId = cr.Id
		WHERE vt.Id = @VoucherTransactionId

		UNION ALL

		SELECT
			ma.Id AS IdMainAccount,
			IIF
			(
				ma.HandlesThirdParty = 1, 
				CASE vt.VoucherClass 
					WHEN 1 THEN --payment
						CASE vt.ExpenseType -- bank account
							WHEN 1 THEN
								CASE st.GetThirdPartyBank
									WHEN 1 THEN eba.ThirdPartyId
									ELSE vt.IdThirdParty
								END
							ELSE -- Cash Register
								CASE st.GetThirdPartyCashRegister
									WHEN 1 THEN cr.ThirdPartyId
									ELSE vt.IdThirdParty
								END
						END 
					ELSE 
						CASE vt.ExpenseType 
							WHEN 1 THEN eba.ThirdPartyId
							ELSE cr.ThirdPartyId
						END 
				END,
				NULL
			) AS IdThirdParty,
			IIF(ma.HandlesCostCenter = 1, vt.IdCostCenter, NULL) AS IdCostCenter,
			vt.Value DebitValue,
			0 CreditValue,
			'Comprobante de Egreso: ' + vt.Code + CASE vt.PaymentMethod
				WHEN 1 THEN ', Cheque # ' + CAST(vt.CheckNumber AS VARCHAR(50))
				WHEN 2 THEN ', Nota Debito # ' + vt.NoteNumber
				ELSE ISNULL(', ' + vt.Detail, '')
			END,
			NULL, 0,0,0
		FROM Treasury.VoucherTransaction vt
		JOIN GeneralLedger.MainAccounts ma ON vt.IdMainAccount = ma.Id
		JOIN Treasury.SettingsTreasury st ON vt.IdUnitOperative = st.IdOperatingUnit
		LEFT JOIN Treasury.EntityBankAccounts eba ON vt.IdEntityBankAccount = eba.Id
		LEFT JOIN Treasury.CashRegisters cr ON vt.IdCashRegister = cr.Id
		WHERE vt.Id = @VoucherTransactionId
			AND vt.Value <> 0

		UNION ALL

		SELECT
			ma.Id AS IdMainAccount,
			IIF
			(
				ma.HandlesThirdParty = 1, 
				IIF
				(
					ma.Id = eba.FMGCounterpartMainAccountId,
					eba.FMGCounterpartThirdPartyId,
					eba.FMGExpenseThirdPartyId
				),
				NULL
			) AS IdThirdParty,
			IIF
			(
				ma.HandlesCostCenter = 1, 
				IIF
				(
					ma.Id = eba.FMGCounterpartMainAccountId,
					eba.FMGCounterpartCostCenterId,
					eba.FMGExpenseCostCenterId
				),
				NULL
			) AS IdCostCenter,
			IIF
			(
				ma.Id = eba.FMGCounterpartMainAccountId,
				vt.TaxByMilValue,
				0
			) DebitValue,
			IIF
			(
				ma.Id = eba.FMGCounterpartMainAccountId,				
				0,
				vt.TaxByMilValue
			) CreditValue,
			vt.Detail,
			NULL, 0,0,0
		FROM Treasury.VoucherTransaction vt
		JOIN Treasury.EntityBankAccounts eba ON vt.IdEntityBankAccount = eba.Id
		JOIN GeneralLedger.MainAccounts ma ON eba.FMGCounterpartMainAccountId = ma.Id 
			OR eba.FMGExpenseMainAccountId = ma.Id
		WHERE vt.Id = @VoucherTransactionId
			AND vt.TaxByMil = 1
			AND ISNULL(vt.TaxByMilValue, 0) <> 0

		UNION ALL
----------------------------- IVA NO DESCONTABLE -------------------------------
			SELECT
				ma.Id AS IdMainAccount,
				vtd.IdThirdParty AS IdThirdParty,
				IIF(ma.HandlesCostCenter = 1, vtd.IdCostCenter, NULL) AS IdCostCenter,
				0 DebitValue,
				vtd.ValueIVA CreditValue,
				NULL Observation,
				NULL IdRetentionConcept,
				0 PercentRetention,
				0 BaseValue,
				0 BillingValue
			FROM Treasury.VoucherTransactionDetails vtd
			JOIN GeneralLedger.GeneralLedgerIVA iva ON vtd.IdGeneralLedgerIVA = iva.Id
			JOIN GeneralLedger.MainAccounts ma ON iva.IdAccountDebitControlFiscal = ma.Id
			WHERE vtd.IdVoucherTransaction = @VoucherTransactionId AND vtd.discountableIVA = 0

		UNION ALL

			SELECT
				ma.Id AS IdMainAccount,
				vtd.IdThirdParty AS IdThirdParty,
				IIF(ma.HandlesCostCenter = 1, vtd.IdCostCenter, NULL) AS IdCostCenter,
				vtd.ValueIVA DebitValue,
				0 CreditValue,
				NULL Observation,
				NULL IdRetentionConcept,
				0 PercentRetention,
				0 BaseValue,
				0 BillingValue
			FROM Treasury.VoucherTransactionDetails vtd
			JOIN GeneralLedger.GeneralLedgerIVA iva ON vtd.IdGeneralLedgerIVA = iva.Id
			JOIN GeneralLedger.MainAccounts ma ON iva.IdAccountCreditControlFiscal = ma.Id
			WHERE vtd.IdVoucherTransaction = @VoucherTransactionId AND vtd.discountableIVA = 0

		UNION ALL
----------------------------- IVA DESCONTABLE -------------------------------
			SELECT
				ma.Id AS IdMainAccount,
				vtd.IdThirdParty AS IdThirdParty,
				IIF(ma.HandlesCostCenter = 1, vtd.IdCostCenter, NULL) AS IdCostCenter,
				0 DebitValue,
				vtd.ValueIVA CreditValue,
				NULL Observation,
				NULL IdRetentionConcept,
				0 PercentRetention,
				0 BaseValue,
				0 BillingValue
			FROM Treasury.VoucherTransactionDetails vtd
			JOIN GeneralLedger.GeneralLedgerIVA iva ON vtd.IdGeneralLedgerIVA = iva.Id
			JOIN GeneralLedger.MainAccounts ma ON iva.IdAccountPurchaseService = ma.Id
			WHERE vtd.IdVoucherTransaction = @VoucherTransactionId AND vtd.discountableIVA = 1

		--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
		SELECT @JournalVoucherXML = CONVERT(xml, 
			(
				SELECT * FROM @JournalVourcherTmp JournalVoucher 
				JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
				For xml AUTO,TYPE, ELEMENTS
			)
		)

		--Se consume el sp que guarda el comprobante contable
		insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,@CodeUser 
			select 
				@CodeMessage = rjv.code, 
				@Message = rjv.MessageResult, 
				@IdJournalVoucherResult = rjv.IdJournalVoucher
			from @resultJournalVoucher rjv

		--Se valida que no hayan errores en el guardado del comprobante contable
		IF @CodeMessage = '999' 
		BEGIN
			SELECT 999 as CodeMessage, @Message as Message, 0 AS IdJournalVoucher, '' AS Consecutive
			RETURN
		END
		
		SELECT 
			@Message = 'Comprobante Contable ' + jvt.Code + ' - ' + jvt.Name + ' con Consecutivo ' + CAST(jv.Consecutive AS VARCHAR(30)),
			@Consecutive = CAST(jv.Consecutive AS VARCHAR(30))
		FROM GeneralLedger.JournalVouchers jv
		JOIN GeneralLedger.JournalVoucherTypes jvt ON jv.IdJournalVoucher = jvt.Id
		WHERE jv.Id = @IdJournalVoucherResult

		/*************************** REVERSIÓN AJUSTE POR DIFERENCIAL CAMBIARIO *************************/
		-- >>> FIX CIMA #52141: Se busca y se reversa el comprobante de Ajuste por Diferencial Cambiario
		-- generado originalmente por Payments.SP_AccountPayableRevaluation al confirmar este CE.

		DECLARE @ProfitLostJournalVoucherTypeId INT
		SELECT TOP 1 @ProfitLostJournalVoucherTypeId = ProfitLostJournalVoucherTypeId
		FROM GeneralLedger.CompanySettings

		IF @ProfitLostJournalVoucherTypeId IS NOT NULL
		BEGIN
			DECLARE @FxAdjustmentIds TABLE (Id INT)

			-- Se busca por el enlace correcto (EntityName/EntityId, una vez aplicado el fix en
			-- SP_AccountPayableRevaluation) y por texto como fallback para comprobantes históricos
			-- que quedaron grabados con EntityId = 0 / EntityName = 'JournalVouchers'.
			-- El patrón '( CODIGO )' es fijo en el Detail sin importar el texto de
			-- GeneralLedger.ViewEntityNameDescriptions, que puede variar entre ambientes.
			INSERT INTO @FxAdjustmentIds (Id)
			SELECT jv.Id
			FROM GeneralLedger.JournalVouchers jv
			WHERE jv.IdJournalVoucher = @ProfitLostJournalVoucherTypeId
				AND jv.Status = 2
				AND
				(
					(jv.EntityName = 'VoucherTransaction' AND jv.EntityId = @VoucherTransactionId)
					OR
					(
						ISNULL(jv.EntityId, 0) = 0
						AND jv.Detail LIKE '%( ' + @VoucherTransactionCode + ' )%'
					)
				)

			IF EXISTS (SELECT 1 FROM @FxAdjustmentIds)
			BEGIN
				DECLARE @FxJournalVourcherTmp TABLE 
				(
					Id INT DEFAULT(0), Consecutive BIGINT DEFAULT(0), LegalBookId INT, IdJournalVoucher INT,
					VoucherDate VARCHAR(30), Imported VARCHAR(5) DEFAULT('False'), Status TINYINT, Detail VARCHAR(MAX),
					EntityCode VARCHAR(20), EntityId INT, EntityName VARCHAR(250), IsClosedYear TINYINT DEFAULT(0), CurrencyId INT
				)
				DECLARE @FxJournalVourcherDetailTmp TABLE 
				(
					Id INT DEFAULT(0), IdAccounting INT DEFAULT(0), IdMainAccount INT, IdThirdParty INT, IdCostCenter INT,
					DebitValue DECIMAL(18,2), CreditValue DECIMAL(18,2), Detail VARCHAR(MAX),
					IdRetention INT, RetentionRate DECIMAL(6,3), BaseValue DECIMAL(18,2), BillingValue DECIMAL(18,2)
				)
				DECLARE @FxAdjustmentId INT

				DECLARE fx_cursor CURSOR LOCAL FAST_FORWARD FOR
					SELECT Id FROM @FxAdjustmentIds

				OPEN fx_cursor
				FETCH NEXT FROM fx_cursor INTO @FxAdjustmentId

				WHILE @@FETCH_STATUS = 0
				BEGIN
					DELETE FROM @FxJournalVourcherTmp
					DELETE FROM @FxJournalVourcherDetailTmp

					INSERT INTO @FxJournalVourcherTmp
					(LegalBookId, IdJournalVoucher, VoucherDate, Status, Detail, EntityCode, EntityId, EntityName, CurrencyId)
					SELECT
						jv.LegalBookId, jv.IdJournalVoucher, tn.NoteDate, 2,
						'Reversión de Ajuste por Diferencial Cambiario, Comp. Egreso ( ' + @VoucherTransactionCode + ' ), Nota ' + tn.Code,
						-- >>> FIX CIMA #53146: la moneda del comprobante de reversión debe ser la del LIBRO
						-- del ajuste original (jv.LegalBookId), no la del documento que se está reversando
						-- (tn.CurrencyId). Los valores que se copian abajo ya vienen convertidos al libro/moneda
						-- del ajuste original; si se declara aquí la moneda del CE/Nota (p.ej. USD) y el libro
						-- resulta ser uno homologado en otra moneda (p.ej. COP), SP_ProcessJournalVoucherMovement
						-- vuelve a convertir esos valores con la TRM, multiplicando el monto por segunda vez.
						tn.Code, tn.Id, 'TreasuryNote', lb.OfficialCurrencyId
					FROM GeneralLedger.JournalVouchers jv
					JOIN GeneralLedger.LegalBook lb ON lb.Id = jv.LegalBookId
					JOIN Treasury.TreasuryNote tn ON tn.Id = @TreasuryNoteId
					WHERE jv.Id = @FxAdjustmentId

					--Se invierten débitos y créditos del comprobante original de ajuste
					INSERT INTO @FxJournalVourcherDetailTmp (IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail)
					SELECT jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter, jvd.CreditValue, jvd.DebitValue,
						   'Reversión - ' + ISNULL(jvd.Detail, '')
					FROM GeneralLedger.JournalVoucherDetails jvd
					WHERE jvd.IdAccounting = @FxAdjustmentId

					SELECT @JournalVoucherXML = CONVERT(xml, 
						(SELECT * FROM @FxJournalVourcherTmp JournalVoucher 
						 JOIN @FxJournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
						 For xml AUTO,TYPE, ELEMENTS))

					DELETE FROM @resultJournalVoucher
					INSERT @resultJournalVoucher EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML, @CodeUser

					SELECT @CodeMessage = rjv.code, @Message = rjv.MessageResult FROM @resultJournalVoucher rjv

					IF @CodeMessage = '999'
					BEGIN
						CLOSE fx_cursor
						DEALLOCATE fx_cursor
						SELECT 999 as CodeMessage, 'Error al reversar el ajuste por diferencial cambiario: ' + @Message as Message, 0 AS IdJournalVoucher, '' AS Consecutive
						RETURN
					END

					FETCH NEXT FROM fx_cursor INTO @FxAdjustmentId
				END

				CLOSE fx_cursor
				DEALLOCATE fx_cursor
			END
		END

		/***************************************************************************************************/

		/************************************* INTERFAZ PRESUPUESTO *************************************/

		EXEC Budget.SP_GenerateReimbursementResource @TreasuryNoteId, @TreasuryNoteCode, 'TreasuryNote', @CodeUser, @CodeMessageResultReimbursementResource OUTPUT, @MessageResultReimbursementResource OUTPUT

		IF @CodeMessageResultReimbursementResource <> 0
		BEGIN
			SELECT 999 AS CodeMessage, 
				   @MessageResultReimbursementResource AS [Message], 
				   0 AS IdJournalVoucher, 
				   '' AS Consecutive
			RETURN
		END

		/***************************** ACTUALIZACION DEL COMPROBANTE DE EGRESO **************************/

		UPDATE vt
			SET vt.Status = 4,
				vt.ModificationUser = @CodeUser,
				vt.ModificationDate = [Common].[GETDATE](),
				vt.ReversedUser = @CodeUser,
				vt.ReversedDate = @TreasuryNoteDate
		FROM Treasury.VoucherTransaction vt
		WHERE vt.Id = @VoucherTransactionId

		/**************************** INSERTAR EN AJUSTE DE NOTA DOCUMENTO SOPORTE **********************/
		DECLARE @MessageNoteElectronicDocument VARCHAR(max) =''

		IF EXISTS( SELECT 1 FROM Treasury.VoucherTransaction vt where vt.Status=4 and vt.Id=@VoucherTransactionId and vt.HandlesDocumentSupport=1)
		BEGIN
				IF NOT EXISTS (
								SELECT 1
								FROM Billing.BillingSequence s WITH (NOLOCK)
								JOIN Billing.BillingSequenceDetail sd WITH (NOLOCK) ON s.Id = sd.IdSequenseBillingC
								JOIN Common.Sequense cs WITH (NOLOCK) on sd.IdSequense = cs.Id
								WHERE s.IdForm = '2819' AND s.IsManual = 0
									AND 
									(
										(s.Scope = 'O')
										OR
										(s.Scope = 'OU' AND sd.IdOperatingUnit IN( SELECT vt.IdUnitOperative
																					from VoucherTransaction vt 
																					WHERE vt.id=@VoucherTransactionId))
									)
								) BEGIN

										SET	@MessageNoteElectronicDocument ='No existe una secuencia automatica para Nota de Ajuste del Documento Soporte Electronico'
										SELECT 999 as CodeMessage, @MessageNoteElectronicDocument as Message, 0 AS IdJournalVoucher, '' AS Consecutive
										RETURN

										END
								ELSE BEGIN	
								--genero la secuencia para EL DOCUMENTO SOPORTE ELECTRONICO
											DECLARE @idSequenceDetail	int
											DECLARE @pattern			varchar(300)
											DECLARE @NextS				bigint
											DECLARE @Scope				varchar(5)
											DECLARE @IdSequence			int
											DECLARE @CodeDS				VARCHAR(20)
											DECLARE @IdOperatingUnit	int
											
							/********/
							select @IdSequence = Id, @Scope = Scope from Billing.BillingSequence With(Nolock) where IdForm = '2819'

									if @Scope = 'OU' begin
										select @pattern = cs.Pattern, @idSequenceDetail = psd.Id  
										from Billing.BillingSequenceDetail psd  With(Nolock)
										inner join Common.Sequense cs With(Nolock) on cs.Id = psd.IdSequense 
										where psd.IdSequenseBillingC = @IdSequence and IdOperatingUnit = @IdOperatingUnit
									end
									else begin
										select @pattern = cs.Pattern, @idSequenceDetail = psd.Id  
										from Billing.BillingSequenceDetail psd With(Nolock) 
										inner join Common.Sequense cs With(Nolock) on cs.Id = psd.IdSequense 
										where psd.IdSequenseBillingC = @IdSequence
									end
									/*se actualiza la tabla de secuencia con el numero de documento que se van a generar*/
									update Billing.BillingSequenceDetail set @NextS = [Next] += 1 where Id = @idSequenceDetail
									SELECT @CodeDS =dbo.GetSequence('',@pattern,(@NextS - 1))

							/**************/

							INSERT INTO Billing.ElectronicSupportDocumentAdjustmentNote(
									Code,
									DocumentDate,
									NoteType,
									Nature,
									ElectronicSupportDocumentId,
									Description,
									SubTotalValue,
									TaxValue,
									TotalValue,
									OperativeUnitId,
									Status,
									CreationUser,
									CreationDate,
									Retry,
									Year,
									EntityId,
									EntityName) 
								SELECT
									@CodeDS,
									vt.DocumentDate,
									1,
									1,
									elsd.Id,
									tn.Description,
									elsd.SubTotalValue,
									elsd.TaxValue,
									elsd.TotalValue,
									elsd.OperativeUnitId,
									2 Status,
									tn.CreationUser,
									tn.CreationDate,
									elsd.Retry,
									elsd.Year,
									tn.Id EntityId,
									'TreasuryNote' EntityName
								FROM Treasury.VoucherTransaction vt
								JOIN Billing.ElectronicSupportDocument elsd WITH(NOLOCK) on vt.id = elsd.EntityId and elsd.EntityName ='VoucherTransaction'
								JOIN Treasury.TreasuryNote tn WITH(NOLOCK) on tn.VoucherTransactionId = vt.Id -- Nuevo join
								WHERE vt.id =@VoucherTransactionId and vt.HandlesDocumentSupport = 1
				END								
		END
		/************************************************************************************************/

		SELECT 0 AS CodeMessage, 
				CONCAT(@Message,IIF(ISNULL(@MessageResultReimbursementResource, '') = '', '', CHAR(13) + CHAR(10)+ISNULL(@MessageResultReimbursementResource, '')),' ',@MessageNoteElectronicDocument) AS [Message],
			   @IdJournalVoucherResult AS IdJournalVoucher, 
			   @Consecutive AS Consecutive
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS [Message], 0 AS IdJournalVoucher, '' AS Consecutive
	END CATCH	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reversa (anula) un comprobante de egreso de tesorería previamente confirmado, generando la nota de tesorería correspondiente y el asiento contable inverso en el libro mayor. Antes de ejecutar la reversión, valida que el comprobante exista y esté confirmado, que el anticipo asociado no haya sido afectado por otro proceso, que no sea un reembolso (los reembolsos no son reversables) y, en caso de traslados, que las cuentas bancarias o cajas involucradas tengan saldo suficiente. Consume la nota de tesorería (TreasuryNote) para obtener el contexto del movimiento, la configuración de tesorería (SettingsTreasury) para determinar el tipo de comprobante contable a generar, los detalles del voucher (VoucherTransactionDetails y ExpenseConcepts) para verificar el comportamiento del gasto, y los anticipos (AdvancePayments) para controlar si el anticipo ya fue utilizado. El resultado es la creación del comprobante contable inverso y la actualización del estado del comprobante de egreso original.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseVoucherTransaction';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseVoucherTransaction';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reversa un comprobante de egreso (pago o traslado) restituyendo saldos en bancos/cajas, devolviendo balances a cuentas por pagar/anticipos, generando el comprobante contable de reversión y, si aplica, la nota de ajuste del documento soporte electrónico y la interfaz presupuestal.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseVoucherTransaction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La transacción de comprobante (VoucherTransaction) referenciada por la nota de tesorería debe existir y estar en estado confirmado (Status = 2).; Para anticipos de cuentas por pagar (Behavior=3, VoucherClass=1), el AdvancePayments asociado debe tener Value = Balance (no haber sido afectado por otro proceso).; El comprobante no debe ser de tipo reembolso (VoucherClass <> 2): la reversión de reembolsos no está permitida.; Para traslados (VoucherClass=3) con ExpenseType=1, las cuentas bancarias destino deben tener CurrentBalance suficiente para cubrir el valor a reversar.; Para traslados (VoucherClass=3) con ExpenseType=3, las cajas destino deben tener CurrentBalance suficiente para cubrir el valor a reversar.; Si HandlesDocumentSupport=1, debe existir una secuencia automática (BillingSequence con IdForm=''2819'' e IsManual=0) acorde al alcance (Organización o Unidad Operativa).', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseVoucherTransaction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Treasury.TreasuryBalance: Cuando ExpenseType=1 inserta movimiento de balance con Nature=1, EntityBankAccountId y ValueMovement = vt.Value + (TaxByMilValue si TaxByMil=1, sino 0); cuando ExpenseType IN (2,3) inserta con CashRegisterId; en traslados inserta adicionalmente con Nature=2 por cada detalle.; [UPDATE] Treasury.EntityBankAccounts: Si ExpenseType=1: incrementa CurrentBalance en (vt.Value + TaxByMilValue cuando TaxByMil=1) sobre la cuenta bancaria del comprobante. En traslado (VoucherClass=3, ExpenseType=1) decrementa CurrentBalance de las cuentas destino del detalle por vtd.Value.; [UPDATE] Treasury.CashRegisters: Si ExpenseType IN (2,3): incrementa CurrentBalance en (vt.Value + TaxByMilValue cuando TaxByMil=1). En traslado (VoucherClass=3, ExpenseType=3) decrementa CurrentBalance de las cajas destino del detalle por vtd.Value.; [UPDATE] Payments.AccountPayableShares: Cuando VoucherClass=1 y ExpenseConcepts.Behavior IN (2,3): aps.Balance += db.AdvancedValue y aps.PaymentValue -= db.AdvancedValue (revierte la imputación de pago a la cuota).; [UPDATE] Payments.AccountPayable: Cuando VoucherClass=1 y Behavior IN (2,3): ap.Balance += db.AdvancedValue (restituye saldo de la cuenta por pagar).; [INSERT] Payments.MovementAccountPayables: Por cada cuota afectada inserta un movimiento con EntityName=''TreasuryNote'', EntityId=@TreasuryNoteId, Value=db.AdvancedValue, dejando trazabilidad de la reversión sobre la cuenta por pagar.; [UPDATE] Payments.AdvancePayments: Cuando VoucherClass=1 y Behavior=3: ap.Balance = 0 sobre el anticipo cuyo Code coincide con vt.Code (anula el anticipo CxP que originó el pago).; [UPDATE] Portfolio.PortfolioAdvance: Cuando VoucherClass=1 y Behavior=4 (devolutivo de anticipo de cartera): pa.Balance += vta.Value, restituyendo el saldo del anticipo de cartera.; [INSERT] GeneralLedger.JournalVouchers: Construye XML con cabecera y detalle (incluyendo IVA descontable y no descontable, ajustes por TaxByMil cuando TaxByMil=1 y TaxByMilValue<>0) y llama a SP_CreateAndValidateJournalVoucherMovement para crear el comprobante contable de reversión con Status=2 y Detail=''Reversión de Comprobante de Egreso con Nota ...''.; [UPDATE] Treasury.VoucherTransaction: Tras éxito del proceso contable y presupuestal: Status=4 (reversado), ReversedUser=@CodeUser y ReversedDate=@TreasuryNoteDate.; [UPDATE] Billing.BillingSequenceDetail: Si HandlesDocumentSupport=1: incrementa [Next] en 1 sobre la secuencia con IdForm=''2819'' (filtrada por alcance OU/O) para asignar consecutivo a la nota de ajuste.; [INSERT] Billing.ElectronicSupportDocumentAdjustmentNote: Si VoucherTransaction.HandlesDocumentSupport=1, inserta la nota de ajuste con NoteType=1, Nature=1, Status=2, Code generado por la secuencia y datos heredados del ElectronicSupportDocument origen y de la TreasuryNote.; [RETURN_RESULT] RESULT: Devuelve CodeMessage=999 con mensaje de error en validaciones (no confirmado, anticipo afectado, reembolso no soportado, saldos insuficientes, error contable o presupuestal); en éxito devuelve CodeMessage=0, mensaje con código y consecutivo del comprobante contable, IdJournalVoucher y Consecutive.; [RAISERROR] RESULT: El bloque CATCH retorna CodeMessage=999 con ERROR_MESSAGE() y ERROR_LINE() ante cualquier excepción no controlada.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseVoucherTransaction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseVoucherTransaction';
-- GO
