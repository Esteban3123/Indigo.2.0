-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-26
-- Description:	Reversa un comprobante de egreso
-- =============================================
CREATE PROCEDURE [Treasury].[SP_ReverseVoucherTransaction]
	@TreasuryNoteId INT,	
	@CodeUser VARCHAR(20)	
AS
BEGIN
	SET NOCOUNT ON;

	/********************************************* VARIABLES ********************************************/
	
	DECLARE @TreasuryNoteCode VARCHAR(20),
			@TreasuryNoteDate DATETIME,
			@VoucherTransactionId INT,
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
			@VoucherClass = vt.VoucherClass
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