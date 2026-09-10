-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-01-18
-- Description:	Procedimiento que se encarga de confirmar las Notas del Módulo de Cuentas por Pagar
-- =============================================
CREATE PROCEDURE [Payments].[SP_ConfirmPayableNote]
    @PaymentNotesXml AS XML,
	@CodeUser AS VARCHAR(20),
	@IsMasiveConfirm AS BIT
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/

	--Tabla con las Notas del Módulo de Cuentas por Pagar a confirmar
	DECLARE @TablePaymentNote TABLE
	(
		PaymentNoteId INT NOT NULL,
		Code VARCHAR(20) NOT NULL,
		JournalVoucherTypeId INT,
		OriginEntityName VARCHAR(250),
		Status TINYINT DEFAULT(0) --0 No procesado, 1 Erronea, 2 Confirmada
	)

	--Tabla con las contabilizaciones no homologables del proceso de Notas del Módulo de Cuentas por Pagar
	DECLARE @TablePaymentNoteDetailConceptNotHomologated TABLE
	(
		Id INT IDENTITY(1,1),
		PaymentNoteId INT NOT NULL,
		LegalBookId INT NOT NULL,
		IdMainAccount INT NOT NULL,
		IdThirdParty INT,
		IdCostCenter INT,
		DebitValue DECIMAL(20,4) NOT NULL,
		CreditValue DECIMAL(20,4) NOT NULL,
		Detail VARCHAR(MAX),
		IdRetention INT,
		RetentionRate DECIMAL(5,3),
		BaseValue DECIMAL(18,2),
		BillingValue DECIMAL(18,2)
	)

	--Tabla Respuesta a Retornar
	DECLARE @TableResult TABLE
	(
		CodeMessage INT,
		Message VARCHAR(MAX),
		Consecutive VARCHAR(MAX),
		Id INT
	)

	DECLARE @CodeMessageResultObligationModification INT,
			@MessageResultObligationModification VARCHAR(MAX)

	DECLARE @accountPayable AS VARCHAR(20) = 'AccountPayable',
			@paymentNotes AS VARCHAR(20) = 'PaymentNotes'

	DECLARE @responseRevaluation table (Code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)
	DECLARE @Message_DiffAdjustment AS VARCHAR(MAX)

	/******************************** VARIABLES CONTABLES *******************************/

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
	DECLARE @JournalVoucherXML as XML,
			@CodeMessage Int,
			@Message Varchar(Max),
			@IdJournalVoucherResult Int,
			@IndicatesBillAdvance tinyint,
			@OfficialCurrency INT,
			@TaxRegistration tinyint

	--tabla temporal para almacenar el resultado del movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	/*************************************** PROCESO ************************************/

	BEGIN TRY

		/* se consulta la Moneda Oficial*/
		SELECT @OfficialCurrency = cs.OfficialCurrencyId
		FROM GeneralLedger.CompanySettings cs
		---------------------------------
		--Se obtienen los datos de las Notas del Módulo de Cuentas por Pagar a confirmar
		INSERT INTO @TablePaymentNote 
			(PaymentNoteId, Code, JournalVoucherTypeId, OriginEntityName)
			SELECT 
				t.x.value('Id[1]','int'),
				t.x.value('Code[1]','varchar(20)'),
				t.x.value('JournalVoucherTypeId[1]','int'),
				t.x.value('OriginEntityName[1]','varchar(250)')
			FROM @PaymentNotesXml.nodes('/PaymentNotes') t(x)

		--Se obtienen los datos de las contabilizaciones a libros no homologables para las Notas del Módulo de Cuentas por Pagar a confirmar
		INSERT INTO @TablePaymentNoteDetailConceptNotHomologated 
			(PaymentNoteId, LegalBookId, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue)
			SELECT 
				t.x.value('PaymentNoteId[1]','int'),
				t.x.value('LegalBookId[1]','int'),
				t.x.value('IdMainAccount[1]','int'),
				t.x.value('IdThirdParty[1]','int'),
				t.x.value('IdCostCenter[1]','int'),
				t.x.value('DebitValue[1]','decimal(20,4)'),
				t.x.value('CreditValue[1]','decimal(20,4)'),
				t.x.value('Detail[1]','varchar(max)'),
				t.x.value('IdRetention[1]','int'),
				t.x.value('RetentionRate[1]','decimal(5,3)'),
				t.x.value('BaseValue[1]','decimal(18,2)'),
				t.x.value('BillingValue[1]','decimal(18,2)')
			FROM @PaymentNotesXml.nodes('/PaymentNotes/ListPaymentNoteDetailConceptNotHomologated/PaymentNoteDetailConceptNotHomologated') t(x)

			SELECT @IndicatesBillAdvance = pn.IndicatesBillAdvance
			From @TablePaymentNote tp 
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON tp.PaymentNoteId = pn.Id

		/************************************* VALIDACIONES ************************************/

		IF NOT EXISTS ( SELECT 1 FROM @TablePaymentNote )
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'No se envió ninguna nota de cuentas por pagar a confirmar' AS Message, '' AS Consecutive
				
			SELECT CodeMessage, Message, Consecutive FROM @TableResult
			RETURN
		END

		IF @IsMasiveConfirm = 1 AND EXISTS ( SELECT 1 FROM @TablePaymentNoteDetailConceptNotHomologated )
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'No se puede confirmar Notas del Módulo de Cuentas por Pagar con libros no homologables desde una confirmación masiva' AS Message, '' AS Consecutive
				
			SELECT CodeMessage, Message, Consecutive FROM @TableResult
			RETURN
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TablePaymentNote t
			LEFT JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
			WHERE pn.Id IS NULL
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
				SELECT 999 AS CodeMessage, 
					'No existe la Nota del Módulo de Cuentas por Pagar ' + t.Code AS Message, 
					'' AS Consecutive, t.PaymentNoteId
				FROM @TablePaymentNote t
				LEFT JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
				WHERE pn.Id IS NULL
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TablePaymentNote t
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
			LEFT JOIN Payments.SettingPayments sp WITH(NOLOCK) ON pn.IdOperatingUnit = sp.IdOperatingUnit
			WHERE sp.Id IS NULL
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
				SELECT DISTINCT 999 AS CodeMessage, 
					'No existen parámetros de pago en la unidad operativa asociada a la Nota ' + pn.Code + ' del Módulo de Cuentas por Pagar ' + t.Code AS Message, 
					'' AS Consecutive, t.PaymentNoteId
				FROM @TablePaymentNote t
				JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
				LEFT JOIN Payments.SettingPayments sp WITH(NOLOCK) ON pn.IdOperatingUnit = sp.IdOperatingUnit
				WHERE sp.Id IS NULL
		
			SELECT CodeMessage, Message, Consecutive FROM @TableResult
			RETURN
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TablePaymentNote t
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
			WHERE pn.Status <> 1
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
				SELECT DISTINCT 999 AS CodeMessage, 
					'La Nota del Módulo de Cuenta por Pagar ' + t.Code + ' se encuentra en estado ' + 
						CASE pn.Status 
							WHEN 2 THEN 'Confirmado'
							WHEN 3 THEN 'Anulado'
							ELSE 'N/A'
						END
					AS Message, 
					'' AS Consecutive, t.PaymentNoteId
				FROM @TablePaymentNote t
				JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
				WHERE pn.Status <> 1
		END

		IF EXISTS 
		( 
			SELECT 1 
			FROM @TablePaymentNote t
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
			JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId
			WHERE (pn.IndicatesBillAdvance = 0 AND pnapa.AdvancePaymentId IS NOT NULL) OR (pn.IndicatesBillAdvance = 1 AND pnapa.AccountPayableId IS NOT NULL)
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
				SELECT DISTINCT 999 AS CodeMessage, 
					'La Nota del Módulo de Cuentas por Pagar ' + t.Code + ' es del tipo ' + IIF(pn.IndicatesBillAdvance = 0, 'Facturas', 'Anticipos') + ' pero contiene ' + IIF(pn.IndicatesBillAdvance = 0, 'Anticipos', 'Facturas') AS Message, 
					'' AS Consecutive, pn.Id
				FROM @TablePaymentNote t
				JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
				JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId
				WHERE (pn.IndicatesBillAdvance = 0 AND pnapa.AdvancePaymentId IS NOT NULL) OR (pn.IndicatesBillAdvance = 1 AND pnapa.AccountPayableId IS NOT NULL)
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TablePaymentNote t
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
			JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId
			WHERE pn.IndicatesBillAdvance = 0
			GROUP BY pn.Id, pnapa.AccountPayableId, pnapa.AdjusmentValue
			HAVING pnapa.AdjusmentValue <> SUM(pnapa.AdjustmentValueShare)
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
				SELECT 999 AS CodeMessage, 
					'El valor a ajustar (' + CAST(pnapa.AdjusmentValue AS VARCHAR(20)) + ') de la factura ' + ap.BillNumber + ' de la Nota del Módulo de Cuentas por Pagar ' + pn.Code + ' no coincide con el ajuste de de las cuotas (' + CAST(SUM(pnapa.AdjustmentValueShare) AS VARCHAR(20)) + ')' AS Message, 
					'' AS Consecutive, pn.Id
				FROM @TablePaymentNote t
				JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
				JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON pnapa.AccountPayableId = ap.Id
				WHERE pn.IndicatesBillAdvance = 0
				GROUP BY pn.Id, pn.Code, pnapa.AccountPayableId, pnapa.AdjusmentValue, ap.BillNumber
				HAVING pnapa.AdjusmentValue <> SUM(pnapa.AdjustmentValueShare)
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TablePaymentNote t
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
			JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId
			JOIN Payments.AdvancePayments ap WITH(NOLOCK) ON pnapa.AdvancePaymentId = ap.Id
			WHERE pn.IndicatesBillAdvance = 1
				AND pn.Nature = 2 AND pnapa.AdjusmentValue > ap.Balance
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
				SELECT 999 AS CodeMessage, 
					CONCAT('El valor a ajustar (', FORMAT(pnapa.AdjusmentValue, 'C2', 'es-CO'), ') de la Nota del Módulo de Cuentas por Pagar ', pn.Code, ' supera el saldo del anticipo ', ap.Code, ' (', FORMAT(ap.Balance, 'C2', 'es-CO'), ')') AS Message, 
					'' AS Consecutive, pn.Id
				FROM @TablePaymentNote t
				JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
				JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId
				JOIN Payments.AdvancePayments ap WITH(NOLOCK) ON pnapa.AdvancePaymentId = ap.Id
				WHERE pn.IndicatesBillAdvance = 1
					AND pn.Nature = 2 AND pnapa.AdjusmentValue > ap.Balance
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TablePaymentNote t
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
			LEFT JOIN 
			(
				SELECT pnapa.PaymentNoteId, SUM(IIF(pnapa.AccountPayableId IS NULL, pnapa.AdjusmentValue, pnapa.AdjustmentValueShare)) AdjustmentValue
				FROM Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK)
				GROUP BY pnapa.PaymentNoteId
			) pnapa ON pn.Id = pnapa.PaymentNoteId
			LEFT JOIN 
			(
				SELECT pnd.IdPaymentsNote, SUM((pnd.Value + ISNULL(pnd.IVAValue, 0)) * IIF(pnd.Nature = 1, 1, -1)) AdjustmentValue
				FROM Payments.PaymentsNoteDetails pnd WITH(NOLOCK)
				GROUP BY pnd.IdPaymentsNote
			) pnd ON pn.Id = pnd.IdPaymentsNote
			WHERE (ISNULL(pnapa.AdjustmentValue, 0) * IIF(pn.Nature = 1, 1, -1) + ISNULL(pnd.AdjustmentValue, 0) <> 0)
		)
		BEGIN
			DECLARE @creditValue AS DECIMAL(18,2),
					@debitValue AS DECIMAL(18,2)

			SELECT @creditValue = pnapa.AdjustmentValue
			FROM @TablePaymentNote t
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
			LEFT JOIN 
			(
				SELECT pnapa.PaymentNoteId, SUM(IIF(pnapa.AccountPayableId IS NULL, pnapa.AdjusmentValue, pnapa.AdjustmentValueShare)) AdjustmentValue
				FROM Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK)
				GROUP BY pnapa.PaymentNoteId
			) pnapa ON pn.Id = pnapa.PaymentNoteId
			
			SELECT @debitValue = pnd.AdjustmentValue
			FROM @TablePaymentNote t
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
			LEFT JOIN 
			(
				SELECT pnd.IdPaymentsNote, SUM((pnd.Value + ISNULL(pnd.IVAValue, 0))) AdjustmentValue
				FROM Payments.PaymentsNoteDetails pnd WITH(NOLOCK)
				GROUP BY pnd.IdPaymentsNote
			) pnd ON pn.Id = pnd.IdPaymentsNote

			INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
				SELECT 999 AS CodeMessage, 
					'La Nota del Módulo de Cuentas por Pagar ' + pn.Code + ' se encuentra desbalanceada credito: ' + CAST(@creditValue AS VARCHAR(20)) + ' débito: ' + CAST(@debitValue AS VARCHAR(20)) AS Message, 
					'' AS Consecutive, pn.Id
				FROM @TablePaymentNote t
				JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
				LEFT JOIN 
				(
					SELECT pnapa.PaymentNoteId, SUM(IIF(pnapa.AccountPayableId IS NULL, pnapa.AdjusmentValue, pnapa.AdjustmentValueShare)) AdjustmentValue
					FROM Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK)
					GROUP BY pnapa.PaymentNoteId
				) pnapa ON pn.Id = pnapa.PaymentNoteId
				LEFT JOIN 
				(
					SELECT pnd.IdPaymentsNote, SUM(pnd.Value * IIF(pnd.Nature = 1, 1, -1)) AdjustmentValue
					FROM Payments.PaymentsNoteDetails pnd WITH(NOLOCK)
					GROUP BY pnd.IdPaymentsNote
				) pnd ON pn.Id = pnd.IdPaymentsNote
				WHERE (ISNULL(pnapa.AdjustmentValue, 0) * IIF(pn.Nature = 1, 1, -1) + ISNULL(pnd.AdjustmentValue, 0) <> 0)
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TablePaymentNoteDetailConceptNotHomologated tnh
			GROUP BY tnh.PaymentNoteId
			HAVING SUM(ISNULL(tnh.DebitValue, 0)) <> SUM(ISNULL(tnh.CreditValue, 0))
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
				SELECT 999 AS CodeMessage, 
					'El detalle no Homologable de La Nota del Módulo de Cuentas por Pagar ' + t.Code + ' se encuentra desbalanceado' AS Message, 
					'' AS Consecutive, t.PaymentNoteId
				FROM @TablePaymentNote t
				JOIN @TablePaymentNoteDetailConceptNotHomologated tnh ON t.PaymentNoteId = tnh.PaymentNoteId
				GROUP BY t.PaymentNoteId, t.Code
				HAVING SUM(ISNULL(tnh.DebitValue, 0)) <> SUM(ISNULL(tnh.CreditValue, 0))
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TablePaymentNote t
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
			LEFT JOIN GeneralLedger.ClosedMonth cm WITH(NOLOCK) ON YEAR(pn.NoteDate) = cm.Year AND MONTH(pn.NoteDate) = cm.Month
			WHERE cm.Id IS NULL OR ISNULL(cm.Status, 0) = 0
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
				SELECT DISTINCT 999 AS CodeMessage, 
					'La fecha del documento (' + CAST(pn.NoteDate AS VARCHAR(20)) + ') asociado a la Nota del Módulo de Cuentas por Pagar ' + pn.Code + ' no se encuentra en un periodo abierto de contabilidad' AS Message, 
					'' AS Consecutive, pn.Id
				FROM @TablePaymentNote t
				JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
				LEFT JOIN GeneralLedger.ClosedMonth cm WITH(NOLOCK) ON YEAR(pn.NoteDate) = cm.Year AND MONTH(pn.NoteDate) = cm.Month
				WHERE cm.Id IS NULL OR ISNULL(cm.Status, 0) = 0
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TablePaymentNote t
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
			JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId
			JOIN Payments.AccountPayable ap WITH(NOLOCK) ON pnapa.AccountPayableId = ap.Id
			WHERE pn.Nature = 1
			GROUP BY ap.Id, ap.Balance
			HAVING SUM(pnapa.AdjustmentValueShare) > ap.Balance
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
				SELECT ap.CodeMessage, ap.Message, ap.Consecutive, t.PaymentNoteId
				FROM @TablePaymentNote t
				JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON t.PaymentNoteId = pnapa.PaymentNoteId
				JOIN 
				(
					SELECT 999 AS CodeMessage, 
						'El saldo (' + CAST(ap.Balance AS VARCHAR(20)) + ') de la factura ' + ap.BillNumber + ' asociada a la Nota del Módulo de Cuentas por Pagar ' + MAX(pn.Code) + ' no es suficiente para el valor de ajuste (' + CAST(SUM(pnapa.AdjustmentValueShare) AS VARCHAR(20)) + ')' AS Message, 
						'' AS Consecutive, ap.Id
					FROM @TablePaymentNote t
					JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
					JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId
					JOIN Payments.AccountPayable ap WITH(NOLOCK) ON pnapa.AccountPayableId = ap.Id
					WHERE pn.Nature = 1
					GROUP BY ap.Id, ap.BillNumber, ap.Balance
					HAVING SUM(pnapa.AdjustmentValueShare) > ap.Balance
				) ap ON pnapa.AccountPayableId = ap.Id
		END

		IF EXISTS (
			SELECT 1
			FROM @TablePaymentNote t
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
			JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId
			JOIN Payments.AccountPayableShares aps WITH(NOLOCK) ON pnapa.AccountPayableId = aps.IdAccountPayable AND pnapa.AccountPayableShareId = aps.Id
			WHERE pn.Nature = 1
			GROUP BY pn.Id
			HAVING COUNT(CASE WHEN aps.Balance = 0 OR pnapa.AdjustmentValueShare > aps.Balance THEN 1 END) = COUNT(*)
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
			SELECT DISTINCT 
				999 AS CodeMessage,
				'El saldo de todas las cuotas de la factura ' + ap.BillNumber + 
				' asociada a la Nota del Módulo de Cuentas por Pagar ' + pn.Code + 
				' no es suficiente para los valores de ajuste.' AS Message,
				'' AS Consecutive,
				pn.Id
			FROM @TablePaymentNote t
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
			JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId
			JOIN Payments.AccountPayable ap WITH(NOLOCK) ON pnapa.AccountPayableId = ap.Id
			JOIN Payments.AccountPayableShares aps WITH(NOLOCK) ON pnapa.AccountPayableId = aps.IdAccountPayable AND pnapa.AccountPayableShareId = aps.Id
			WHERE pn.Nature = 1
			GROUP BY pn.Id, ap.BillNumber, pn.Code
			HAVING COUNT(CASE WHEN aps.Balance = 0 OR pnapa.AdjustmentValueShare > aps.Balance THEN 1 END) = COUNT(*)
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TablePaymentNote t
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
			JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId
			JOIN Payments.AdvancePayments ap WITH(NOLOCK) ON pnapa.AdvancePaymentId = ap.Id
			WHERE pn.Nature = 1 AND (ap.Balance = 0 OR pnapa.AdjusmentValue > ap.Balance) AND pn.IndicatesBillAdvance <> 1
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
				SELECT DISTINCT 999 AS CodeMessage, 
					'El saldo (' + CAST(ap.Balance AS VARCHAR(20)) + ') del anticipo ' + ap.Code + ' asociada a la Nota del Módulo de Cuentas por Pagar ' + pn.Code + ' no es suficiente para el valor de ajuste (' + CAST(pnapa.AdjusmentValue AS VARCHAR(20)) + ')' AS Message, 
					'' AS Consecutive, pn.Id
				FROM @TablePaymentNote t
				JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
				JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId
				JOIN Payments.AdvancePayments ap WITH(NOLOCK) ON pnapa.AdvancePaymentId = ap.Id
				WHERE pn.Nature = 1 AND (ap.Balance = 0 OR pnapa.AdjusmentValue > ap.Balance) AND pn.IndicatesBillAdvance <> 1
		END

		-- Valida que el ajuste con el saldo no sea superior al valor inicial para Anticipos
		IF EXISTS ( 
			SELECT 1 
			FROM @TablePaymentNote t
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
			JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId
			JOIN Payments.AdvancePayments ap WITH(NOLOCK) ON pnapa.AdvancePaymentId = ap.Id
			WHERE pn.Nature = 1 AND ((pnapa.AdjusmentValue + ap.Balance) > ap.Value) AND pn.IndicatesBillAdvance = 1
		)
		BEGIN
			   

			INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
				SELECT DISTINCT 999 AS CodeMessage, 
					'El saldo (' + CAST(ap.Balance AS VARCHAR(20)) + ') del anticipo ' + ap.Code + ' asociada a la Nota del Módulo de Cuentas por Pagar ' + pn.Code + ' sumado al valor de ajuste (' + CAST(pnapa.AdjusmentValue AS VARCHAR(20)) + ') es superior al Valor Inicial (' + CAST(ap.Value AS VARCHAR(20)) + ')' AS Message, 
					'' AS Consecutive, pn.Id
				FROM @TablePaymentNote t
				JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON t.PaymentNoteId = pn.Id
				JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId
				JOIN Payments.AdvancePayments ap WITH(NOLOCK) ON pnapa.AdvancePaymentId = ap.Id
				WHERE pn.Nature = 1 AND ((pnapa.AdjusmentValue + ap.Balance) > ap.Value) AND pn.IndicatesBillAdvance = 1
		END

		-- Eliminamos aquellas facturas que no pasaron el proceso de validación
		DELETE tap
		FROM @TablePaymentNote tap
		JOIN @TableResult tr ON tap.PaymentNoteId = tr.Id

		/**********************************************************************************************************************/

		IF EXISTS ( SELECT 1 FROM @TablePaymentNote )
		BEGIN
			UPDATE tap
				SET tap.JournalVoucherTypeId = IIF(pn.Nature = 1, sp.IdJournalVoucherDebitNotes, sp.IdJournalVoucherCreditNotes)
			FROM @TablePaymentNote tap
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON tap.PaymentNoteId = pn.Id
			JOIN Payments.SettingPayments sp WITH(NOLOCK) ON pn.IdOperatingUnit = sp.IdOperatingUnit
			WHERE tap.JournalVoucherTypeId IS NULL

			DECLARE @rows INT = 1,
					@Id INT = 0,
					@Code VARCHAR(20)

			WHILE @rows > 0
			BEGIN
				SELECT TOP 1 
					@Id = tap.PaymentNoteId,
					@Code = tap.Code
				FROM @TablePaymentNote tap
				WHERE tap.PaymentNoteId > @Id
				ORDER BY tap.PaymentNoteId

				-- Verifico que haya encontrado un resultado
				SET @rows = @@RowCount
				IF @rows = 0
				BEGIN
					BREAK
				END

		/***************************** CREACIÓN DOCUMENTO CONTABLE *****************************/
			
			IF @IndicatesBillAdvance <> 2 -- se Verifica que la nota no sea de tipo reversion, y ejecute el procedimiento almacenado correspondiente
			BEGIN
				SET @JournalVoucherXML = NULL
				DELETE FROM @JournalVourcherTmp
				DELETE FROM @JournalVourcherDetailTmp

				INSERT INTO @JournalVourcherTmp
					( LegalBookId, IdJournalVoucher, VoucherDate, Status, Detail, EntityCode, EntityId, EntityName, OriginEntityName,CurrencyId )
					SELECT TOP 1
						NULL LegalBookId, 
						tap.JournalVoucherTypeId IdJournalVoucher, 
						pn.NoteDate VoucherDate, 
						2 Status, 
						dbo.CleanSpecialChars(pn.Comment) Detail,
						@Code EntityCode,
						pn.Id EntityId,
						@paymentNotes EntityName,
						tap.OriginEntityName OriginEntityName,
						pn.CurrencyId
					FROM @TablePaymentNote tap
					JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON tap.PaymentNoteId = pn.Id
					WHERE pn.Id = @Id

			INSERT INTO @JournalVourcherDetailTmp
				( IdMainAccount, IdThirdParty, IdCostCenter, Detail, DebitValue, CreditValue, IdRetention, RetentionRate, BaseValue, BillingValue )
				SELECT
					ap.IdAccount AS IdMainAccount,
					CASE WHEN ma.HandlesThirdParty = 1 THEN ap.IdThirdParty ELSE NULL END AS IdThirdParty,
					CASE WHEN ma.HandlesCostCenter = 1 THEN ap.IdCostCenter ELSE NULL END AS IdCostCenter,
					dbo.CleanSpecialChars(ap.Coments) AS Detail,
					IIF(pn.Nature = 1, ISNULL(pnapa.AdjustmentValueShare, pnapa.AdjusmentValue), 0) AS DebitValue,
					IIF(pn.Nature = 1, 0, ISNULL(pnapa.AdjustmentValueShare, pnapa.AdjusmentValue)) AS CreditValue,
					NULL AS IdRetention,
					NULL AS RetentionRate,
					NULL AS BaseValue,
					NULL AS BillingValue
				FROM @TablePaymentNote tap
				JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON tap.PaymentNoteId = pn.Id
				JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON pnapa.AccountPayableId = ap.Id
				JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = ap.IdAccount
				WHERE pn.Id = @Id

				UNION ALL

				SELECT
					ap.IdAccount AS IdMainAccount,
					CASE WHEN ma.HandlesThirdParty = 1 THEN ap.IdThirdParty ELSE NULL END AS IdThirdParty,
					CASE WHEN ma.HandlesCostCenter = 1 THEN ap.IdCostCenter ELSE NULL END AS IdCostCenter,
					dbo.CleanSpecialChars(ap.Comments) AS Detail,
					IIF(pn.Nature = 1, ISNULL(pnapa.AdjustmentValueShare, pnapa.AdjusmentValue), 0) AS DebitValue,
					IIF(pn.Nature = 1, 0, ISNULL(pnapa.AdjustmentValueShare, pnapa.AdjusmentValue)) AS CreditValue,
					NULL AS IdRetention,
					NULL AS RetentionRate,
					NULL AS BaseValue,
					NULL AS BillingValue
				FROM @TablePaymentNote tap
				JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON tap.PaymentNoteId = pn.Id
				JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId
				JOIN Payments.AdvancePayments ap WITH(NOLOCK) ON pnapa.AdvancePaymentId = ap.Id
				JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = ap.IdAccount
				WHERE pn.Id = @Id
					
				UNION ALL

				SELECT
					pnd.IdAccount AS IdMainAccount,
					CASE WHEN ma.HandlesThirdParty = 1 THEN pnd.IdThirdParty ELSE NULL END AS IdThirdParty,
					CASE WHEN ma.HandlesCostCenter = 1 THEN pnd.IdCostCenter ELSE NULL END AS IdCostCenter,
					dbo.CleanSpecialChars(pnd.Comments) AS Detail,
					IIF(pnd.Nature = 1, isnull( pnd.TotalConceptValue,pnd.Value), 0) AS DebitValue,
					IIF(pnd.Nature = 1, 0, isnull( pnd.TotalConceptValue,pnd.Value)) AS CreditValue,
					pnd.IdRetentionConcept AS IdRetention,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.Percentage) AS RetentionRate,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.BaseValue) AS BaseValue,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.BillingValue) AS BillingValue
				FROM @TablePaymentNote tap
				JOIN Payments.PaymentsNoteDetails pnd WITH(NOLOCK) ON tap.PaymentNoteId = pnd.IdPaymentsNote
				JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = pnd.IdAccount
				WHERE tap.PaymentNoteId = @Id and(pnd.TaxRegistration = 1 ) AND (pnd.IVAValue > 0 AND pnd.IdRetentionConcept IS NULL)

				UNION ALL

				SELECT
					pnd.IdAccount AS IdMainAccount,
					CASE WHEN ma.HandlesThirdParty = 1 THEN pnd.IdThirdParty ELSE NULL END AS IdThirdParty,
					CASE WHEN ma.HandlesCostCenter = 1 THEN pnd.IdCostCenter ELSE NULL END AS IdCostCenter,
					dbo.CleanSpecialChars(pnd.Comments) AS Detail,
					IIF(pnd.Nature = 1, ISNULL(pnd.TotalConceptValue, pnd.Value), 0) AS DebitValue,
					IIF(pnd.Nature = 1, 0, ISNULL(pnd.TotalConceptValue, pnd.Value)) AS CreditValue,
					pnd.IdRetentionConcept AS IdRetention,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.Percentage) AS RetentionRate,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.BaseValue) AS BaseValue,
					CASE 
						WHEN pnd.IdRetentionConcept IS NULL THEN NULL
						WHEN COALESCE(pnd.TotalConceptValue, pnd.Value, 0) > 0
							 AND COALESCE(pnd.BillingValue, 0) = 0
							THEN pnd.BaseValue
						ELSE pnd.BillingValue
					END AS BillingValue
				FROM @TablePaymentNote tap
				JOIN Payments.PaymentsNoteDetails pnd WITH(NOLOCK) ON tap.PaymentNoteId = pnd.IdPaymentsNote
				JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = pnd.IdAccount
				WHERE tap.PaymentNoteId = @Id AND ( pnd.DiscountableIVA IS NULL OR COALESCE(pnd.IVAValue,0) = 0 OR pnd.IdRetentionConcept IS NOT NULL)

				UNION ALL

				/**********************IVA DESCONTABLE ****************************/

				SELECT
					pnd.IdAccount AS IdMainAccount,
					CASE WHEN ma.HandlesThirdParty = 1 THEN pnd.IdThirdParty ELSE NULL END AS IdThirdParty,
					CASE WHEN ma.HandlesCostCenter = 1 THEN pnd.IdCostCenter ELSE NULL END AS IdCostCenter,
					dbo.CleanSpecialChars(pnd.Comments) AS Detail,
					IIF(pnd.Nature = 1, pnd.Value, 0) AS DebitValue,
					IIF(pnd.Nature = 1, 0, pnd.Value) AS CreditValue,
					pnd.IdRetentionConcept AS IdRetention,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.Percentage) AS RetentionRate,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.BaseValue) AS BaseValue,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.BillingValue) AS BillingValue
				FROM @TablePaymentNote tap
				JOIN Payments.PaymentsNoteDetails pnd WITH(NOLOCK) ON tap.PaymentNoteId = pnd.IdPaymentsNote
				JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = pnd.IdAccount
				WHERE tap.PaymentNoteId = @Id and pnd.TaxRegistration = 2 AND (pnd.IVAValue > 0 AND pnd.IdRetentionConcept IS NULL)

				UNION ALL

				SELECT
					gli.IdAccountPurchaseService AS IdMainAccount,
					CASE WHEN ma.HandlesThirdParty = 1 THEN pnd.IdThirdParty ELSE NULL END AS IdThirdParty,
					CASE WHEN ma.HandlesCostCenter = 1 THEN pnd.IdCostCenter ELSE NULL END AS IdCostCenter,
					dbo.CleanSpecialChars(pnd.Comments) AS Detail,
					IIF(pnd.Nature = 1, pnd.IVAValue, 0) AS DebitValue,
					IIF(pnd.Nature = 1, 0, pnd.IVAValue) AS CreditValue,
					pnd.IdRetentionConcept AS IdRetention,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.Percentage) AS RetentionRate,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.BaseValue) AS BaseValue,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.BillingValue) AS BillingValue
				FROM @TablePaymentNote tap
				JOIN Payments.PaymentsNoteDetails pnd WITH(NOLOCK) ON tap.PaymentNoteId = pnd.IdPaymentsNote
				INNER JOIN GeneralLedger.GeneralLedgerIVA gli ON gli.Id = pnd.IdGeneralLedgerIVA
				JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = gli.IdAccountPurchaseService
				WHERE tap.PaymentNoteId = @Id and pnd.TaxRegistration= 2 AND (pnd.IVAValue > 0 AND pnd.IdRetentionConcept IS NULL)

				/*******************************************************************************/

				UNION ALL
				
				/**********************IVA AL COSTO (CONTROL FISCAL) ****************************/

				SELECT
					gli.IdAccountCreditControlFiscal AS IdMainAccount,
					CASE WHEN ma.HandlesThirdParty = 1 THEN pnd.IdThirdParty ELSE NULL END AS IdThirdParty,
					CASE WHEN ma.HandlesCostCenter = 1 THEN pnd.IdCostCenter ELSE NULL END AS IdCostCenter,
					dbo.CleanSpecialChars(pnd.Comments) AS Detail,
					pnd.IVAValue AS DebitValue,
					 0 AS CreditValue,
					pnd.IdRetentionConcept AS IdRetention,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.Percentage) AS RetentionRate,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.BaseValue) AS BaseValue,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.BillingValue) AS BillingValue
				FROM @TablePaymentNote tap
				JOIN Payments.PaymentsNoteDetails pnd WITH(NOLOCK) ON tap.PaymentNoteId = pnd.IdPaymentsNote
				INNER JOIN GeneralLedger.GeneralLedgerIVA gli ON gli.Id = pnd.IdGeneralLedgerIVA
				JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = gli.IdAccountCreditControlFiscal
				WHERE tap.PaymentNoteId = @Id  AND pnd.TaxRegistration = 1 AND (pnd.IVAValue > 0 AND pnd.IdRetentionConcept IS NULL)

				UNION ALL

				SELECT
					gli.IdAccountDebitControlFiscal AS IdMainAccount,
					CASE WHEN ma.HandlesThirdParty = 1 THEN pnd.IdThirdParty ELSE NULL END AS IdThirdParty,
					CASE WHEN ma.HandlesCostCenter = 1 THEN pnd.IdCostCenter ELSE NULL END AS IdCostCenter,
					dbo.CleanSpecialChars(pnd.Comments) AS Detail,
					0 AS DebitValue,
					pnd.IVAValue AS CreditValue,
					pnd.IdRetentionConcept AS IdRetention,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.Percentage) AS RetentionRate,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.BaseValue) AS BaseValue,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.BillingValue) AS BillingValue
				FROM @TablePaymentNote tap
				JOIN Payments.PaymentsNoteDetails pnd WITH(NOLOCK) ON tap.PaymentNoteId = pnd.IdPaymentsNote
				INNER JOIN GeneralLedger.GeneralLedgerIVA gli ON gli.Id = pnd.IdGeneralLedgerIVA
				JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = gli.IdAccountDebitControlFiscal
				WHERE tap.PaymentNoteId = @Id  AND pnd.TaxRegistration = 1 AND (pnd.IVAValue > 0 AND pnd.IdRetentionConcept IS NULL)

				/*******************************************************************************/

				UNION ALL 

				/************************* IVA AL COSTO ****************************/
				SELECT
					pnd.IdAccount AS IdMainAccount,
					CASE WHEN ma.HandlesThirdParty = 1 THEN pnd.IdThirdParty ELSE NULL END AS IdThirdParty,
					CASE WHEN ma.HandlesCostCenter = 1 THEN pnd.IdCostCenter ELSE NULL END AS IdCostCenter,
					dbo.CleanSpecialChars(pnd.Comments) AS Detail,
					IIF(pnd.Nature = 1, pnd.IVAValue, 0) AS DebitValue,
					IIF(pnd.Nature = 1, 0, pnd.IVAValue) AS CreditValue,
					pnd.IdRetentionConcept AS IdRetention,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.Percentage) AS RetentionRate,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.BaseValue) AS BaseValue,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.BillingValue) AS BillingValue
				FROM @TablePaymentNote tap
				JOIN Payments.PaymentsNoteDetails pnd WITH(NOLOCK) ON tap.PaymentNoteId = pnd.IdPaymentsNote
				JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = pnd.IdAccount
				WHERE tap.PaymentNoteId = @Id AND pnd.TaxRegistration = 4 AND (pnd.IVAValue > 0 AND pnd.IdRetentionConcept IS NULL)

				UNION ALL

				SELECT
					pnd.IdAccount AS IdMainAccount,
					CASE WHEN ma.HandlesThirdParty = 1 THEN pnd.IdThirdParty ELSE NULL END AS IdThirdParty,
					CASE WHEN ma.HandlesCostCenter = 1 THEN pnd.IdCostCenter ELSE NULL END AS IdCostCenter,
					dbo.CleanSpecialChars(pnd.Comments) AS Detail,
					IIF(pnd.Nature = 1, pnd.BaseValue, 0) AS DebitValue,
					IIF(pnd.Nature = 1, 0, pnd.BaseValue) AS CreditValue,
					pnd.IdRetentionConcept AS IdRetention,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.Percentage) AS RetentionRate,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.BaseValue) AS BaseValue,
					IIF(pnd.IdRetentionConcept IS NULL, NULL, pnd.BillingValue) AS BillingValue
				FROM @TablePaymentNote tap
				JOIN Payments.PaymentsNoteDetails pnd WITH(NOLOCK) ON tap.PaymentNoteId = pnd.IdPaymentsNote
				JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = pnd.IdAccount
				WHERE tap.PaymentNoteId = @Id AND pnd.TaxRegistration = 4 AND (pnd.IVAValue > 0 AND pnd.IdRetentionConcept IS NULL)

					/*******************************************************************/

					DECLARE @xmltmp xml = (SELECT * FROM @JournalVourcherDetailTmp
					 FOR XML AUTO)
					PRINT CONVERT(NVARCHAR(MAX), @xmltmp)
				--Eliminamos cuentas en 0
				DELETE jv FROM @JournalVourcherDetailTmp jv WHERE jv.DebitValue = 0 AND jv.CreditValue = 0

				--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
				SELECT @JournalVoucherXML = CONVERT(xml, 
					(
						SELECT * FROM @JournalVourcherTmp JournalVoucher 
						JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
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

				--Se valida que no hayan errores en el guardado del comprobante contable
				IF @CodeMessage = '999' 
				BEGIN
					INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
						SELECT 999 AS CodeMessage, 'No se confirmó la Nota del Módulo de Cuentas por Pagar ' + pn.Code + ' por ' + @Message AS Message, '' AS Consecutive, pn.Id
						FROM Payments.PaymentNotes pn WITH(NOLOCK)
						WHERE pn.Id = @Id

						UPDATE tap 
							SET tap.Status = 1
						FROM @TablePaymentNote tap
						WHERE tap.PaymentNoteId = @Id
				END
				ELSE
				BEGIN
					INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
						SELECT 0 AS CodeMessage, 'Se confirmó correctamente la Nota del Módulo de Cuentas por Pagar ' + @Code AS Message, '' AS Consecutive, @Id AS Id

					INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
						SELECT
							0 AS CodeMessage,
							'Se generó el comprobante contable de tipo ' + jvt.Code + ' - ' + jvt.Name AS Message,
							'' AS Consecutive,
							@Id AS Id
						FROM @TablePaymentNote tap
						JOIN GeneralLedger.JournalVoucherTypes jvt ON tap.JournalVoucherTypeId = jvt.Id
						WHERE tap.PaymentNoteId = @Id

						UPDATE tap
							SET tap.Status = 2
						FROM @TablePaymentNote tap
						WHERE tap.PaymentNoteId = @Id
				END
			END
			ELSE
			BEGIN
			--ejecuta el Sp que se encarga de hacer la reversion
				EXEC Payments.SP_AccountPayableReversal  @PaymentNotesXml,@CodeUser, @CodeMessage Output, @Message Output, @IdJournalVoucherResult Output
				
				IF @CodeMessage = '999' 
				BEGIN
					INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
						SELECT 999 AS CodeMessage, 'No se confirmó la Nota del Módulo de Cuentas por Pagar ' + pn.Code + ' por ' + @Message AS Message, '' AS Consecutive, pn.Id
						FROM Payments.PaymentNotes pn WITH(NOLOCK)
						WHERE pn.Id = @Id

						UPDATE tap 
							SET tap.Status = 1
						FROM @TablePaymentNote tap
						WHERE tap.PaymentNoteId = @Id
				END
				ELSE
				BEGIN
					INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
							SELECT 0 AS CodeMessage, 'Se confirmó correctamente la Nota del Módulo de Cuentas por Pagar ' + @Code AS Message, '' AS Consecutive, @Id AS Id

						INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
							SELECT
								0 AS CodeMessage,
								'Se generó el comprobante contable de tipo ' + jvt.Code + ' - ' + jvt.Name AS Message,
								'' AS Consecutive,
								@Id AS Id
							FROM @TablePaymentNote tap
							JOIN GeneralLedger.JournalVoucherTypes jvt ON tap.JournalVoucherTypeId = jvt.Id
							WHERE tap.PaymentNoteId = @Id

							UPDATE tap 
								SET tap.Status = 2
							FROM @TablePaymentNote tap
							WHERE tap.PaymentNoteId = @Id
				END

			END
				DECLARE @LegalBookId INT = 0,
					@LegalBookRows INT = 1

				WHILE @LegalBookRows > 0
				BEGIN
					SELECT TOP 1 @LegalBookId = tapdcnh.LegalBookId
					FROM @TablePaymentNoteDetailConceptNotHomologated tapdcnh
					WHERE tapdcnh.PaymentNoteId = @Id 
						AND tapdcnh.LegalBookId > @LegalBookId
					ORDER BY tapdcnh.LegalBookId

					-- Verifico que haya encontrado un resultado
					SET @LegalBookRows = @@RowCount

					IF EXISTS (SELECT 1 FROM @TablePaymentNote tap WHERE tap.PaymentNoteId = @Id AND tap.Status = 1)
					BEGIN
						SET @LegalBookRows = 0
					END

					IF @LegalBookRows = 0
					BEGIN
						BREAK
					END

					UPDATE jv
						SET jv.LegalBookId = @LegalBookId
					FROM @JournalVourcherTmp jv

					SET @JournalVoucherXML = NULL
					DELETE FROM @JournalVourcherDetailTmp

				INSERT INTO @JournalVourcherDetailTmp
					( IdMainAccount, IdThirdParty, IdCostCenter, Detail, DebitValue, CreditValue, IdRetention, RetentionRate, BaseValue, BillingValue )
					SELECT
						tapdcnh.IdMainAccount,
						CASE WHEN ma.HandlesThirdParty = 1 THEN tapdcnh.IdThirdParty ELSE NULL END,
						CASE WHEN ma.HandlesCostCenter = 1 THEN tapdcnh.IdCostCenter ELSE NULL END,
						tapdcnh.Detail,
						tapdcnh.DebitValue,
						tapdcnh.CreditValue,
						tapdcnh.IdRetention,
						tapdcnh.RetentionRate,
						tapdcnh.BaseValue,
						tapdcnh.BillingValue
					FROM @TablePaymentNoteDetailConceptNotHomologated tapdcnh
					JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = tapdcnh.IdMainAccount
					WHERE tapdcnh.PaymentNoteId = @Id AND tapdcnh.LegalBookId = @LegalBookId

					--Eliminamos cuentas en 0
					DELETE jv FROM @JournalVourcherDetailTmp jv WHERE jv.DebitValue = 0 AND jv.CreditValue = 0

					--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
					SELECT @JournalVoucherXML = CONVERT(xml, 
						(
							SELECT * FROM @JournalVourcherTmp JournalVoucher 
							JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
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

					--Se valida que no hayan errores en el guardado del comprobante contable
					IF @CodeMessage = '999' 
					BEGIN
						INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
							SELECT 999 AS CodeMessage, lb.Code + ' - ' + lb.Name +  ': No se confirmó la cuenta por pagar ' + tap.Code + ' por ' + @Message AS Message, '' AS Consecutive, tap.PaymentNoteId
							FROM @TablePaymentNote tap
							JOIN GeneralLedger.LegalBook lb WITH(NOLOCK) ON lb.Id = @LegalBookId
							WHERE tap.PaymentNoteId = @Id

						UPDATE tap 
							SET tap.Status = 1
						FROM @TablePaymentNote tap
						WHERE tap.PaymentNoteId = @Id
					END
					ELSE
					BEGIN
						INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
							SELECT
								0 AS CodeMessage,
								lb.Code + ' - ' + lb.Name +  ': Se generó el comprobante contable de tipo ' + jvt.Code + ' - ' + jvt.Name AS Message,
								'' AS Consecutive,
								@Id AS Id
							FROM @TablePaymentNote tap
							JOIN GeneralLedger.LegalBook lb ON lb.Id = @LegalBookId
							JOIN GeneralLedger.JournalVoucherTypes jvt ON tap.JournalVoucherTypeId = jvt.Id
							WHERE tap.PaymentNoteId = @Id

						UPDATE tap 
							SET tap.Status = 2
						FROM @TablePaymentNote tap
						WHERE tap.PaymentNoteId = @Id
					END
				END

				/******************************** INTERFAZ PRESUPUESTO *********************************/
				IF EXISTS 
				(
					SELECT 1 
					FROM Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK)
					JOIN Payments.PaymentNoteAccountPayableBudget pnapb WITH(NOLOCK) ON pnapa.Id = pnapb.PaymentNotesAccountPayableAdvanceId 
					WHERE pnapa.PaymentNoteId = @Id
				)
				BEGIN
					EXEC Budget.SP_GenerateObligationModification @Id, @Code, @paymentNotes, @CodeUser, @CodeMessageResultObligationModification OUTPUT, @MessageResultObligationModification OUTPUT

					IF @CodeMessageResultObligationModification <> 0
					BEGIN
						UPDATE tap 
							SET tap.Status = 1
						FROM @TablePaymentNote tap
						WHERE tap.PaymentNoteId = @Id
					END

					INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
						SELECT @CodeMessageResultObligationModification AS CodeMessage, @MessageResultObligationModification AS Message, '' AS Consecutive, @Id AS Id
				END
			END

			/***************************** Creación del documento electrónico *****************************/
			IF EXISTS
			(
				SELECT 1 
				FROM @TablePaymentNote tap
				JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON tap.PaymentNoteId = pn.Id 
				JOIN Payments.PaymentNotesAccountPayableAdvance pnapa on pnapa.PaymentNoteId = tap.PaymentNoteId
				JOIN GeneralLedger.GeneralLedgerSettings gls WITH(NOLOCK) ON gls.IdOperatingUnit = pn.IdOperatingUnit
				WHERE gls.HandlesSupportDocument = 1 
					AND pnapa.ConceptAdjustmentId IS NOT NULL
			)
			BEGIN
				DECLARE @IdForm AS VARCHAR(5) = '2819',
						@SupportSequenseId INT,
						@SupportSequenseDetailId INT,
						@SupportPrefix VARCHAR(20),
						@CodeAdjusmentNote BIGINT,
						------------------------
						@ElectronicSupportDocumentId INT

				DECLARE @ElectronicSupportDocumentAdjustmentNoteOutbox AS TABLE
				(
					Id INT NOT NULL,
					EntityId INT NOT NULL
				)

				SELECT @SupportSequenseId = Id
				FROM Billing.BillingSequence
				WHERE IdForm = @IdForm AND IsManual = 0

				IF @SupportSequenseId IS NULL
				BEGIN
					INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
					SELECT 999 CodeMessage, 'No existe una secuencia numérica automática para las notas de ajuste a documento soporte' AS Message, '' As Consecutive, 0 As Id

					UPDATE @TablePaymentNote SET Status = 1
				END

				SELECT @SupportSequenseDetailId = bsd.Id,
					   @SupportPrefix = REPLACE(s.Pattern, '#', '0')	
				FROM Billing.BillingSequenceDetail bsd 
				JOIN Common.Sequense s ON bsd.IdSequense = s.Id
				WHERE bsd.IdSequenseBillingC = @SupportSequenseId

				IF @SupportSequenseDetailId IS NULL
				BEGIN
					INSERT @TableResult (CodeMessage, Message, Consecutive, Id)
					SELECT 999 CodeMessage, 'No existe un detalle de secuencia numérica para las notas de ajuste a documento soporte' AS Message, '' As Consecutive, 0 As Id 

					UPDATE @TablePaymentNote SET Status = 1
				END

				IF @IndicatesBillAdvance = 0 --Factura de CxP
				BEGIN

					DECLARE @accountPayableWithDS AS TABLE 
					(
						PaymentNotesAccountPayableAdvanceId INT,
						AccountPayableId INT
					)
					--Variables para recorrer el cursor
					DECLARE @PaymentNotesAccountPayableAdvanceId INT,
							@AccountPayableId INT

					INSERT INTO @accountPayableWithDS
						SELECT 
							pnapa.Id PaymentNotesAccountPayableAdvanceId,
							pnapa.AccountPayableId AccountPayableId
						FROM @TablePaymentNote tap
						JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON pn.Id = tap.PaymentNoteId
						JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON tap.PaymentNoteId = pnapa.PaymentNoteId
						WHERE tap.Status = 2
							AND pnapa.ConceptAdjustmentId IS NOT NULL
						ORDER BY pnapa.Id
					
					/* CURSOR */
					DECLARE DATA_CURSOR CURSOR FOR
					SELECT PaymentNotesAccountPayableAdvanceId, AccountPayableId FROM @accountPayableWithDS

					OPEN DATA_CURSOR
					FETCH NEXT FROM DATA_CURSOR INTO @PaymentNotesAccountPayableAdvanceId, @AccountPayableId

						WHILE @@fetch_status = 0
						BEGIN
							--Seteo la variable para obtener el nuevo valor
							SET @CodeAdjusmentNote = NULL

							UPDATE bsd
							SET @CodeAdjusmentNote = bsd.Next += 1
							FROM Billing.BillingSequenceDetail bsd 
							WHERE bsd.Id = @SupportSequenseDetailId

							SET @CodeAdjusmentNote -= 1

							INSERT INTO [Billing].[ElectronicSupportDocumentAdjustmentNote]
								([Code], [DocumentDate], [NoteType], [Nature], [ElectronicSupportDocumentId], [Description],
								[SubTotalValue], [TaxValue], [TotalValue], [Status], [OperativeUnitId],
								[CreationUser], [CreationDate], [ConfirmationUser], [ConfirmationDate], [EntityId], [EntityName])
							OUTPUT inserted.Id, inserted.EntityId
							INTO @ElectronicSupportDocumentAdjustmentNoteOutbox (Id, EntityId)
							SELECT 
								CONCAT(@SupportPrefix,@CodeAdjusmentNote), pn.NoteDate, pnapa.ConceptAdjustmentId, pn.Nature, esd.Id ElectronicSupportDocumentId, CONCAT('Nota de ajuste aplicada al documento soporte ', esd.DocumentNumber) AS Description,
								pnapa.AdjusmentValue, 0 TaxValue, pnapa.AdjusmentValue, 2 [Status], pn.IdOperatingUnit,
								@CodeUser, Common.GETDATE(), @CodeUser, Common.GETDATE(), pn.Id EntityId, @paymentNotes EntityName
							FROM Billing.ElectronicSupportDocument esd WITH(NOLOCK)
							JOIN Payments.AccountPayable ap WITH(NOLOCK) ON ap.Id = esd.EntityId AND @accountPayable = esd.EntityName
							JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON ap.Id = pnapa.AccountPayableId
							JOIN Payments.PaymentNotes pn  WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId
							JOIN @TablePaymentNote tap ON pn.Id = tap.PaymentNoteId
							WHERE pnapa.Id = @PaymentNotesAccountPayableAdvanceId

							--Paso a la siguiente posición
							FETCH NEXT FROM DATA_CURSOR INTO @PaymentNotesAccountPayableAdvanceId, @AccountPayableId
						END
					CLOSE DATA_CURSOR;
					DEALLOCATE DATA_CURSOR; --Finalizo cursor

				END
				ELSE IF @IndicatesBillAdvance = 2 --Reversión total de CxP
				BEGIN

					SELECT @ElectronicSupportDocumentId = esd.Id
					FROM @TablePaymentNote tap
					JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON tap.PaymentNoteId = pn.Id
					JOIN Payments.AccountPayable ap WITH(NOLOCK) ON pn.IdAccountPayable = ap.Id 
					JOIN Billing.ElectronicSupportDocument esd WITH(NOLOCK) ON esd.EntityId = ap.Id AND @accountPayable = esd.EntityName

					IF @ElectronicSupportDocumentId IS NOT NULL
					BEGIN
						UPDATE bsd
						SET @CodeAdjusmentNote = bsd.Next += 1
						FROM Billing.BillingSequenceDetail bsd 
						WHERE bsd.Id = @SupportSequenseDetailId

						SET @CodeAdjusmentNote -= 1

						INSERT INTO [Billing].[ElectronicSupportDocumentAdjustmentNote]
							([Code], [DocumentDate], [NoteType], [Nature], [ElectronicSupportDocumentId], [Description],
							[SubTotalValue], [TaxValue], [TotalValue], [Status], [OperativeUnitId],
							[CreationUser], [CreationDate], [ConfirmationUser], [ConfirmationDate], [EntityId], [EntityName])
						OUTPUT inserted.Id, inserted.EntityId
						INTO @ElectronicSupportDocumentAdjustmentNoteOutbox (Id, EntityId)
						SELECT 
							CONCAT(@SupportPrefix, @CodeAdjusmentNote), pn.NoteDate, 2 AS NoteType, pn.Nature, @ElectronicSupportDocumentId, CONCAT('Reversión del documento soporte ', esd.DocumentNumber) AS Description,
							esd.SubTotalValue, esd.TaxValue, esd.TotalValue, 2 [Status], esd.OperativeUnitId,
							@CodeUser, Common.GETDATE() CreationDate, @CodeUser, Common.GETDATE() ConfirmationDate, pn.Id AS EntityId, @paymentNotes AS EntityName
						FROM @TablePaymentNote tap
						JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON tap.PaymentNoteId = pn.Id
						JOIN Billing.ElectronicSupportDocument esd  WITH(NOLOCK) ON esd.Id = @ElectronicSupportDocumentId
						WHERE tap.Status = 2
					END	
				END

				INSERT INTO Billing.OutboxEvent (
					EventType, AggregateType, AggregateId, PayloadJson, OccurredAtUtc
				)
				SELECT
					N'Billing.SupportDocumentAdjustmentNoteValidationPending.v1',
					N'BillingRecord',
					CAST(esdan.EntityId AS NVARCHAR(255)),
					N'{"Id":' + CAST(esdan.Id AS NVARCHAR(20)) + N'}',
					[Common].[GETDATE]()
				FROM @ElectronicSupportDocumentAdjustmentNoteOutbox esdan
			END --Fin validación si maneja documento soporte
				
			/************************************** PROCESO FINAL ****************************************************/
				
			/************************* AJUSTE DIFERENCIAL NOTA CXP **************/
				IF EXISTS(SELECT  1 FROM @TablePaymentNote tap WHERE  tap.Status = 2 and @IndicatesBillAdvance not in (1,3) ) BEGIN
					DELETE from @responseRevaluation
					
					declare @ListAccountPayable TABLE (	Id INT  NOT NULL,
														ValuePaid NUMERIC(20,2) NOT NULL,
														EntityName VARCHAR(250),
														EntityId INT)

					declare @ListAccountPayableXml as XML,
							@XmlOutput as XML
								
					INSERT INTO @ListAccountPayable
					SELECT	ap.Id,ap.AdjustmentValue,ap.EntityName,ap.PaymentNoteId
					FROM (
							SELECT	ap.Id,
									sum(pnapa.AdjustmentValueShare) AdjustmentValue,
									'PaymentNotes' EntityName,
									pn.Id PaymentNoteId
							from @TablePaymentNote tap
							JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON tap.PaymentNoteId = pn.Id
							JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId		
							JOIN Payments.AccountPayableShares aps WITH(NOLOCK) ON pnapa.AccountPayableId = aps.IdAccountPayable AND pnapa.AccountPayableShareId = aps.Id
							JOIN Payments.AccountPayable ap WITH(NOLOCK) ON pnapa.AccountPayableId = ap.Id
							WHERE tap.Status = 2 and @IndicatesBillAdvance =0
							GROUP by ap.Id,pn.Id

							UNION ALL

							SELECT	ap.Id,
									ap.Value AdjustmentValue,
									'PaymentNotes' EntityName,
									pn.Id PaymentNoteId
							from @TablePaymentNote tap
							JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON tap.PaymentNoteId = pn.Id
							JOIN Payments.AccountPayable ap WITH(NOLOCK) ON pn.IdAccountPayable = ap.Id
							WHERE tap.Status = 2 and @IndicatesBillAdvance =2
							) ap
					

					SELECT @ListAccountPayableXml = CONVERT(xml, 
													(
														SELECT * FROM @ListAccountPayable AccountPayable 
														For xml AUTO,TYPE, ELEMENTS
													))

					EXEC [Payments].[SP_AccountPayableRevaluation]
						@ListAccountPayableXml,
						@CodeUser,@XmlOutput OUTPUT
						
					INSERT @responseRevaluation
					SELECT
					t.x.value('Code[1]', 'Varchar(20)')  Code,
					t.x.value('MessageOutput[1]', 'varchar(max)')  MessageOutput,
					t.x.value('JournalVoucherId[1]', 'INT')  JournalVoucherId
					from @XmlOutput.nodes('/TableResult') t(x);
					
					IF EXISTS(SELECT 1 FROM @responseRevaluation)
					BEGIN
						SET @Message_DiffAdjustment = (select CONCAT('Ajuste Diferencial CxP: ', STRING_AGG(MessageResult, ', '))
														from @responseRevaluation)

						IF EXISTS(select 1 from @responseRevaluation WHERE Code = '999')
						BEGIN	
						
							SELECT 999 AS CodeMessage,
									CONCAT(@Message,' - ', @Message_DiffAdjustment) AS Message,
									'' Consecutive,
									tap.PaymentNoteId Id
								from @TablePaymentNote tap
								WHERE tap.Status = 2
							RETURN
						END 						
					END								
				END
					
				/***************************************************************************************************************************/
				
				/***************************** FACTURAS *****************************/

					-- Se asigna el valor de PreviousBalance que es el valor previo, antes de hacer la modificacion
					UPDATE pnapa
						SET pnapa.PreviousBalance = aps.Balance
					FROM @TablePaymentNote tap
					JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON tap.PaymentNoteId = pnapa.PaymentNoteId
					JOIN Payments.AccountPayableShares aps WITH(NOLOCK) ON pnapa.AccountPayableId = aps.IdAccountPayable AND pnapa.AccountPayableShareId = aps.Id
					WHERE tap.Status = 2

					-- Se actualizan los valores de la cuota
					UPDATE aps
						SET aps.DebitValue = aps.DebitValue + IIF(pn.Nature = 1, pnapa.AdjustmentValueShare, 0),
							aps.CreditValue = aps.CreditValue + IIF(pn.Nature = 1, 0, pnapa.AdjustmentValueShare),
							aps.Balance = aps.Balance + (pnapa.AdjustmentValueShare * IIF(pn.Nature = 1, -1, 1))
					FROM @TablePaymentNote tap
					JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON tap.PaymentNoteId = pn.Id
					JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId		
					JOIN Payments.AccountPayableShares aps WITH(NOLOCK) ON pnapa.AccountPayableId = aps.IdAccountPayable AND pnapa.AccountPayableShareId = aps.Id
					JOIN Payments.AccountPayable ap WITH(NOLOCK) ON pnapa.AccountPayableId = ap.Id
					JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ap.IdAccount = ma.Id
					WHERE tap.Status = 2

					-- Se asigna el valor de Balance que es el valor despues de hacer la modificacion
					UPDATE pnapa
						SET pnapa.Balance = aps.Balance
					FROM @TablePaymentNote tap
					JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON tap.PaymentNoteId = pnapa.PaymentNoteId
					JOIN Payments.AccountPayableShares aps WITH(NOLOCK) ON pnapa.AccountPayableId = aps.IdAccountPayable AND pnapa.AccountPayableShareId = aps.Id
					WHERE tap.Status = 2

					-- Se actualiza el balance de la cuenta por pagar
					UPDATE ap
						SET ap.Balance = aps.Balance
					FROM @TablePaymentNote tap
					JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON tap.PaymentNoteId = pnapa.PaymentNoteId
					JOIN Payments.AccountPayable ap WITH(NOLOCK) ON pnapa.AccountPayableId = ap.Id
					JOIN
					(
						SELECT aps.IdAccountPayable, SUM(aps.Balance) Balance
						FROM Payments.AccountPayableShares aps WITH(NOLOCK)
						GROUP BY aps.IdAccountPayable
					) aps ON ap.Id = aps.IdAccountPayable
					WHERE tap.Status = 2
				/*********************************************************************/				
			/*********************** AJUSTE DIFERENCIAL ANTICPOS ********************************************************/
				IF EXISTS(SELECT  1 FROM @TablePaymentNote tap WHERE  tap.Status = 2 and @IndicatesBillAdvance =1 ) BEGIN
					DELETE from @responseRevaluation
					DECLARE @ListAdvancePayments TABLE (Id INT  NOT NULL,
														ValueAdjustment NUMERIC(20,2) NOT NULL,
														EntityName VARCHAR(250),
														EntityId INT,
														DocumentDate DATE)
					DECLARE @ListAdvancePaymentsXml as XML,
							@XmlOutputAdvance as XML

					INSERT INTO @ListAdvancePayments (Id,ValueAdjustment,EntityName,EntityId, DocumentDate)
					SELECT	ap.Id,
							pnapa.AdjusmentValue,
							'PaymentNotes' AS EntityName,
							pn.Id AS EntityId,
							Cast(pn.NoteDate as DATE) AS NoteDate
					FROM @TablePaymentNote tap
					JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON tap.PaymentNoteId = pn.Id
					JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId		
					JOIN Payments.AdvancePayments ap WITH(NOLOCK) ON pnapa.AdvancePaymentId = ap.Id
					WHERE tap.Status = 2

					SELECT @ListAdvancePaymentsXml = CONVERT(xml, 
																(
																	SELECT * FROM @ListAdvancePayments AS AdvancePaymentsRevaluation 
																	For xml AUTO,TYPE, ELEMENTS
																))
					EXEC [Payments].[SP_AdvancePaymentsRevaluation_Output]
							@ListAdvancePaymentsXml,
							@CodeUser,@XmlOutputAdvance OUTPUT
						
					INSERT @responseRevaluation
					SELECT
					t.x.value('Code[1]', 'Varchar(20)')  Code,
					t.x.value('MessageOutput[1]', 'varchar(max)')  MessageOutput,
					t.x.value('JournalVoucherId[1]', 'INT')  JournalVoucherId
					from @XmlOutputAdvance.nodes('/TableResult') t(x);
					
					 IF EXISTS(SELECT 1 FROM @responseRevaluation) 
					 BEGIN
						select @Message_DiffAdjustment =  CONCAT('Ajuste diferencial anticipo : ',STRING_AGG(MessageResult, ', '))
						from @responseRevaluation

						if EXISTS(select 1 from @responseRevaluation WHERE Code = '999')
						Begin							
							SELECT 999 AS CodeMessage,
								CONCAT(@Message,' - ', @Message_DiffAdjustment) AS Message,
								'' Consecutive,
								tap.PaymentNoteId Id
							from @TablePaymentNote tap
							WHERE tap.Status = 2

							RETURN
						End
					 END	
				END
			/*********************** ************************ ******************************/
				
			/***************************** ANTICIPOS *****************************/

			-- Se asigna el valor de PreviousBalance que es el valor previo, antes de hacer la modificacion
			UPDATE pnapa
				SET pnapa.PreviousBalance = ap.Balance
			FROM @TablePaymentNote tap
			JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON tap.PaymentNoteId = pnapa.PaymentNoteId
			JOIN Payments.AdvancePayments ap WITH(NOLOCK) ON pnapa.AdvancePaymentId = ap.Id
			WHERE tap.Status = 2

			-- Se actualizan los valores del anticipo
			UPDATE ap
				SET ap.DebitValue = ISNULL(ap.DebitValue, 0) + IIF(pn.Nature = 1, pnapa.AdjusmentValue, 0),
					ap.CreditValue = ISNULL(ap.CreditValue, 0) + IIF(pn.Nature = 1, 0, pnapa.AdjusmentValue),
					ap.Balance = ap.Balance + (pnapa.AdjusmentValue * IIF(pn.Nature = 1, 1, -1))
			FROM @TablePaymentNote tap
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON tap.PaymentNoteId = pn.Id
			JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON pn.Id = pnapa.PaymentNoteId		
			JOIN Payments.AdvancePayments ap WITH(NOLOCK) ON pnapa.AdvancePaymentId = ap.Id
			WHERE tap.Status = 2

			-- Se asigna el valor de Balance que es el valor despues de hacer la modificacion
			UPDATE pnapa
				SET pnapa.Balance = ap.Balance
			FROM @TablePaymentNote tap
			JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH(NOLOCK) ON tap.PaymentNoteId = pnapa.PaymentNoteId
			JOIN Payments.AdvancePayments ap WITH(NOLOCK) ON pnapa.AdvancePaymentId = ap.Id
			WHERE tap.Status = 2

			UPDATE pn
				SET pn.ModificationUser = @CodeUser,
					pn.ModificationDate = [Common].[GETDATE](),
					pn.ConfirmationUser = @CodeUser,
					pn.ConfirmationDate = [Common].[GETDATE](),
					pn.Status = 2
			FROM @TablePaymentNote tap
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON tap.PaymentNoteId = pn.Id
			WHERE tap.Status = 2

			DELETE pc 
			FROM @TablePaymentNote tap
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) ON tap.PaymentNoteId = pn.Id
			JOIN Payments.PaymentsControl pc WITH(NOLOCK) ON pc.DocumentType = 2 AND pc.DocumentNumber = pn.Code
			WHERE pn.Status = 2
		END
	END TRY
	BEGIN CATCH
		INSERT @TableResult (CodeMessage, Message, Consecutive)
			SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Consecutive
	END CATCH

	IF COALESCE(@Message_DiffAdjustment,'') <> '' BEGIN
		INSERT INTO @TableResult (CodeMessage,Message,Consecutive,Id)
		VALUES(0,@Message_DiffAdjustment,'',0 )
	END

	SELECT CodeMessage, Message, Consecutive 
	FROM @TableResult
	ORDER BY Id
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma notas de cuentas por pagar (notas débito y crédito) del módulo de Pagos, recibiendo un XML con las notas a procesar y generando los comprobantes contables correspondientes en los libros legales. Valida que las notas existan y sean confirmables, procesa las contabilizaciones homologadas y no homologadas contra los libros contables, y registra el movimiento en el libro mayor (GeneralLedger). Soporta tanto confirmación individual como confirmación masiva, bloqueando en este último caso notas con asientos en libros no homologables. Retorna códigos de resultado y consecutivos de comprobante para cada nota procesada.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmPayableNote';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmPayableNote';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma notas del módulo de Cuentas por Pagar (notas débito/crédito, ajustes y reversiones), validando reglas de negocio, generando los comprobantes contables, documentos soporte electrónicos, ajustes presupuestales y diferenciales, y actualizando saldos de facturas y anticipos.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmPayableNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de notas de pago debe contener al menos un nodo /PaymentNotes con Id, Code, JournalVoucherTypeId y OriginEntityName.; Si la confirmación es masiva (@IsMasiveConfirm=1) no se permiten contabilizaciones a libros no homologables.; Cada PaymentNoteId enviado debe existir en Payments.PaymentNotes.; La unidad operativa de la nota debe tener parámetros configurados en Payments.SettingPayments.; La nota debe estar en estado 1 (no confirmada ni anulada) para poder confirmarse.; El tipo de la nota (IndicatesBillAdvance) debe ser coherente con el contenido: tipo Facturas no debe traer anticipos y tipo Anticipos no debe traer facturas.; La fecha (NoteDate) de la nota debe estar en un período abierto en GeneralLedger.ClosedMonth (existe registro y Status=0).; La sumatoria de AdjustmentValueShare por factura debe igualar el AdjusmentValue total cuando IndicatesBillAdvance=0.; El valor de ajuste no puede superar el saldo del anticipo (Nature=2, IndicatesBillAdvance=1) ni de la factura/cuotas (Nature=1).; Para anticipos con IndicatesBillAdvance=1 y Nature=1, AdjusmentValue + Balance no puede exceder el Value inicial del anticipo.; Los detalles no homologables deben estar balanceados (suma de débitos = suma de créditos por nota).; La nota completa debe estar balanceada entre el detalle (PaymentsNoteDetails) y la aplicación (PaymentNotesAccountPayableAdvance) considerando la naturaleza.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmPayableNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @TableResult: Devuelve mensajes con CodeMessage=999 cuando falla cualquiera de las validaciones (XML vacío, nota inexistente, sin parámetros de pago, estado distinto de 1, descuadre, saldo insuficiente, período cerrado, etc.).; [UPDATE] @TablePaymentNote: Cuando JournalVoucherTypeId es NULL, asigna IdJournalVoucherDebitNotes (Nature=1) o IdJournalVoucherCreditNotes (otro caso) desde Payments.SettingPayments.; [INSERT] GeneralLedger.JournalVouchers: Cuando IndicatesBillAdvance<>2 (no reversión), arma un comprobante contable a partir de las cuentas de AccountPayable/AdvancePayments y los detalles (incluyendo IVA descontable, IVA al costo y control fiscal según TaxRegistration 1/2/4) y lo persiste vía GeneralLedger.SP_CreateAndValidateJournalVoucherMovement.; [INSERT] GeneralLedger.JournalVouchers: Por cada LegalBookId de los detalles no homologables se genera un comprobante contable adicional con esos movimientos.; [UPDATE] @TablePaymentNote: Tras intentar generar el comprobante: si SP_CreateAndValidateJournalVoucherMovement retorna code=''999'' marca Status=1 (errónea); en caso contrario Status=2 (confirmada).; [INSERT] Billing.ElectronicSupportDocumentAdjustmentNote: Cuando GeneralLedgerSettings.HandlesSupportDocument=1 y existe ConceptAdjustmentId, y IndicatesBillAdvance=0 (factura CxP), inserta una nota de ajuste por cada PaymentNotesAccountPayableAdvance con Status=2, usando consecutivo de Billing.BillingSequenceDetail.; [INSERT] Billing.ElectronicSupportDocumentAdjustmentNote: Cuando IndicatesBillAdvance=2 (reversión total CxP) y existe ElectronicSupportDocument asociado, inserta una nota de ajuste tipo 2 (Reversión) con los valores del documento soporte original.; [UPDATE] Billing.BillingSequenceDetail: Por cada nota de ajuste a documento soporte generada, incrementa en 1 el campo Next de la secuencia y usa el valor previo como código.; [UPDATE] Payments.PaymentNotesAccountPayableAdvance: Para notas con Status=2: graba PreviousBalance con el saldo previo de la cuota/anticipo y luego Balance con el saldo posterior al ajuste.; [UPDATE] Payments.AccountPayableShares: Para notas confirmadas (Status=2) actualiza DebitValue/CreditValue según Nature y recalcula Balance = Balance + AdjustmentValueShare * (Nature=1?-1:1).; [UPDATE] Payments.AccountPayable: Para notas confirmadas, actualiza Balance de la cuenta por pagar con la suma de Balance de sus AccountPayableShares.; [UPDATE] Payments.AdvancePayments: Para notas confirmadas actualiza DebitValue/CreditValue según Nature y Balance = Balance + AdjusmentValue * (Nature=1?1:-1).; [UPDATE] Payments.PaymentNotes: Para notas con Status=2 marca ModificationUser/ConfirmationUser=@CodeUser, fechas con Common.GETDATE() y Status=2 (Confirmada).; [DELETE] Payments.PaymentsControl: Elimina los registros de control donde DocumentType=2 y DocumentNumber=Code de las notas que quedaron Status=2.; [RETURN_RESULT] @TableResult: Inserta mensajes de éxito (CodeMessage=0) por cada nota confirmada y por cada comprobante contable generado, e incluye mensajes de Ajuste Diferencial CxP/Anticipos cuando aplica.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmPayableNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmPayableNote';
-- GO
