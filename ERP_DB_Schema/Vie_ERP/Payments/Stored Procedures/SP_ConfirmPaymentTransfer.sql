-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-04-09
-- Description:	Procedimiento que se encarga de confirmar los cruce de anticipo vs Cuentas por Pagar
-- =============================================
CREATE PROCEDURE [Payments].[SP_ConfirmPaymentTransfer] 
    @PaymentTransferId AS Integer,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/

	DECLARE @errors VARCHAR(MAX)

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
		CurrencyId Int
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
		Detail VARCHAR(500),
		IdRetention INT,
		RetentionRate DECIMAL(6,3),
		BaseValue DECIMAL(18,2),
		BillingValue DECIMAL(18,2)
	)

	--Variable para la creacion del comprobante contable
	DECLARE @JournalVoucherTypeId AS INT,
			--------------------------------------
			@JournalVoucherXML as XML,
			@CodeMessage Int,
			@Message Varchar(Max),
			@IdJournalVoucherResult Int

	DECLARE @responseRevaluation table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)
	DECLARE @Message_DiffAdjustment VARCHAR(1000)

	--tabla temporal para almacenar el resultado del movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	/*************************************** PROCESO ************************************/

	BEGIN TRY

		SELECT 
			@JournalVoucherTypeId = sp.IdJournalVoucherTranslation
		FROM Payments.PaymentTransfer pt
		LEFT JOIN Payments.SettingPayments sp ON pt.OperatingUnitId = sp.IdOperatingUnit
		WHERE pt.Id = @PaymentTransferId
		
		/************************************* VALIDACIONES ************************************/

		IF NOT EXISTS ( SELECT 1 FROM Payments.PaymentTransfer pt WHERE pt.Id = @PaymentTransferId )
		BEGIN
			SELECT 999 AS CodeMessage, 'No existe el registro' AS Message, '' AS Consecutive
			RETURN
		END

		IF EXISTS ( SELECT 1 FROM Payments.PaymentTransfer pt WHERE pt.Id = @PaymentTransferId AND pt.Status <> 1 )
		BEGIN
			SELECT DISTINCT 999 AS CodeMessage, 
				'El Cruce de Anticipo ' + pt.Code + ' se encuentra en estado ' + 
					CASE pt.Status 
						WHEN 2 THEN 'Confirmado'
						WHEN 3 THEN 'Anulado'
						ELSE 'N/A'
					END
				AS Message, 
				'' AS Consecutive
			FROM Payments.PaymentTransfer pt
			WHERE pt.Id = @PaymentTransferId AND pt.Status <> 1
			RETURN
		END

		IF NOT EXISTS ( 
			SELECT 1 
			FROM Payments.PaymentTransfer pt
			JOIN GeneralLedger.ClosedMonth cm ON YEAR(pt.DocumentDate) = cm.Year AND MONTH(pt.DocumentDate) = cm.Month AND cm.Status = 1
			WHERE pt.Id = @PaymentTransferId
		)
		BEGIN
			SELECT DISTINCT 999 AS CodeMessage, 
				'La fecha del documento (' + CAST(pt.DocumentDate AS VARCHAR(20)) + ') no se encuentra en un periodo abierto de contabilidad' AS Message, 
				'' AS Consecutive
			FROM Payments.PaymentTransfer pt
			WHERE pt.Id = @PaymentTransferId
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Payments.PaymentTransfer pt
			JOIN Payments.AdvancePayments ap ON pt.AdvancePaymentId = ap.Id
			WHERE pt.Id = @PaymentTransferId
				AND ap.Balance = 0
		)
		BEGIN
			SELECT 999 AS CodeMessage, 'El saldo del anticipo es igual a 0' AS Message, '' AS Consecutive
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Payments.PaymentTransfer pt
			JOIN Payments.AdvancePayments ap ON pt.AdvancePaymentId = ap.Id
			JOIN 
			(
				SELECT PaymentTransferId, SUM(Value) AS Value
				FROM
				(
					SELECT ptd.PaymentTransferId, IIF(ptd.ValueInCurrencyInvoice = 0, ptd.Value, ptd.ValueInCurrencyInvoice) AS Value
					FROM Payments.PaymentTransferDetail ptd 
					WHERE ptd.PaymentTransferId = @PaymentTransferId
					UNION ALL
					SELECT ptoc.PaymentTransferId, (ptoc.Value * IIF(ptoc.Nature = 1, 1, -1)) AS Value
					FROM Payments.PaymentTransferOtherConcept ptoc
					WHERE ptoc.PaymentTransferId = @PaymentTransferId
				) ptd
				GROUP BY ptd.PaymentTransferId
			) ptd ON pt.Id = ptd.PaymentTransferId
			WHERE pt.Id = @PaymentTransferId
				AND ptd.Value > ap.Balance
		)
		BEGIN
			SELECT 999 AS CodeMessage, 'El saldo del anticipo es menor al total del valor a cruzar' AS Message, '' AS Consecutive
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Payments.PaymentTransferDetail ptd
			JOIN Payments.AccountPayableShares aps ON ptd.AccountPayableShareId = aps.Id			
			WHERE ptd.PaymentTransferId = @PaymentTransferId
				AND ptd.AccountPayableId <> aps.IdAccountPayable
		)
		BEGIN
			SELECT 999 AS CodeMessage, 'Existen detalles donde la factura asociada a la cuota no coincide con la factura asociada al detalle' AS Message, '' AS Consecutive
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Payments.AccountPayable ap
			JOIN 
			(
				SELECT ptd.AccountPayableId, SUM(IIF(ptd.ValueInCurrencyInvoice = 0, ptd.Value, ptd.ValueInCurrencyInvoice)) Value
				FROM Payments.PaymentTransferDetail ptd 
				WHERE ptd.PaymentTransferId = @PaymentTransferId
				GROUP BY ptd.AccountPayableId
			) ptd ON ap.Id = ptd.AccountPayableId
			WHERE ptd.Value > ap.Balance
		)
		BEGIN
			SELECT @errors = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - El saldo de la factura ' + ap.BillNumber + ' es menor al valor a cruzar.'
					FROM Payments.AccountPayable ap
					JOIN 
					(
						SELECT ptd.AccountPayableId, SUM(IIF(ptd.ValueInCurrencyInvoice = 0, ptd.Value, ptd.ValueInCurrencyInvoice)) Value
						FROM Payments.PaymentTransferDetail ptd 
						WHERE ptd.PaymentTransferId = @PaymentTransferId
						GROUP BY ptd.AccountPayableId
					) ptd ON ap.Id = ptd.AccountPayableId
					WHERE ptd.Value > ap.Balance
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeMessage, CHAR(13) + CHAR(10) + @errors AS Message, '' AS Consecutive
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Payments.PaymentTransferDetail ptd
			JOIN Payments.AccountPayableShares aps ON ptd.AccountPayableShareId = aps.Id			
			WHERE ptd.PaymentTransferId = @PaymentTransferId
				AND IIF(ptd.ValueInCurrencyInvoice = 0, ptd.Value, ptd.ValueInCurrencyInvoice) > aps.Balance
		)
		BEGIN
			SELECT @errors = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - El saldo de la cuota No. ' + CAST(aps.Share AS VARCHAR(20)) + ' de la factura ' + ap.BillNumber + ' es menor al valor a cruzar.'
					FROM Payments.PaymentTransferDetail ptd
					JOIN Payments.AccountPayableShares aps ON ptd.AccountPayableShareId = aps.Id
					JOIN Payments.AccountPayable ap ON aps.IdAccountPayable = ap.Id
					WHERE ptd.PaymentTransferId = @PaymentTransferId
						AND IIF(ptd.ValueInCurrencyInvoice = 0, ptd.Value, ptd.ValueInCurrencyInvoice) > aps.Balance
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeMessage, CHAR(13) + CHAR(10) + @errors AS Message, '' AS Consecutive
			RETURN
		END

		IF @JournalVoucherTypeId IS NULL
		BEGIN
			SELECT 999 AS CodeMessage, 'Debe parametrizar el Tipo de Comprobante de Traslado para la unidad Operativa' AS Message, '' AS Consecutive
			RETURN
		END

		/*********************** AJUSTE DIFERENCIAL ANTICPOS ******************************/		
		DELETE FROM @responseRevaluation
		SET @Message_DiffAdjustment = ''
		declare @ListAdvancePayments TABLE (	Id INT  NOT NULL,
												ValueAdjustment NUMERIC(20,2) NOT NULL,
												EntityName VARCHAR(250),
												EntityId INT,
												DocumentDate DATE)

		declare @ListAdvancePaymentsXml as XML,
				@XmlOutput as XML
					
		INSERT INTO @ListAdvancePayments (Id,ValueAdjustment,EntityName,EntityId, DocumentDate)
		SELECT	pt.AdvancePaymentId,
				ABS(SUM(Value)) ValueAdjustment,
				'PaymentTransfer',
				pt.Id,
				pt.DocumentDate
		FROM Payments.PaymentTransfer pt with (nolock)
		JOIN
		(
			SELECT ptd.PaymentTransferId, ptd.Value
			FROM Payments.PaymentTransferDetail ptd 
			WHERE ptd.PaymentTransferId = @PaymentTransferId
			UNION ALL
			SELECT ptoc.PaymentTransferId, (ptoc.Value * IIF(ptoc.Nature = 1, 1, -1)) AS Value
			FROM Payments.PaymentTransferOtherConcept ptoc
			WHERE ptoc.PaymentTransferId = @PaymentTransferId

		) ptd ON pt.Id = ptd.PaymentTransferId
		WHERE pt.Id = @PaymentTransferId
		GROUP BY pt.Id,pt.AdvancePaymentId,pt.DocumentDate

		SELECT @ListAdvancePaymentsXml = CONVERT(xml, 
													(
														SELECT * FROM @ListAdvancePayments AS AdvancePaymentsRevaluation 
														For xml AUTO,TYPE, ELEMENTS
													))
									
		EXEC [Payments].[SP_AdvancePaymentsRevaluation_Output]
				@ListAdvancePaymentsXml,
				@CodeUser,@XmlOutput OUTPUT
						
		INSERT @responseRevaluation
		SELECT
		t.x.value('Code[1]', 'Varchar(20)')  Code,
		t.x.value('MessageOutput[1]', 'varchar(max)')  MessageOutput,
		t.x.value('JournalVoucherId[1]', 'INT')  JournalVoucherId
		from @XmlOutput.nodes('/TableResult') t(x);

		IF EXISTS(select 1 from @responseRevaluation) BEGIN
			set @Message_DiffAdjustment = (select CONCAT( 'Ajuste diferencial anticipo : ',STRING_AGG(MessageResult, ', '))   from @responseRevaluation)

			if EXISTS(select 1 from @responseRevaluation WHERE Code = '999')
			Begin	
							
				SELECT	999 as CodeMessage,
						CONCAT(@Message_DiffAdjustment, ' - ', 'Cruce de Anticipo vs CxP') as Message,
						'' AS Consecutive
				RETURN
			End 
		END
		/*********************** ************************ ******************************/

		/********************************** MODIFICACION ANTICIPO *********************************/

		UPDATE ap
			SET ap.Balance = ap.Balance - ptd.Value,
				ap.CreditValue = ap.CreditValue + ptd.Value
		FROM Payments.PaymentTransfer pt
		JOIN Payments.AdvancePayments ap ON pt.AdvancePaymentId = ap.Id
		JOIN 
		(
			SELECT PaymentTransferId, SUM(Value) AS Value
			FROM
			(
				SELECT ptd.PaymentTransferId, ptd.Value
				FROM Payments.PaymentTransferDetail ptd 
				WHERE ptd.PaymentTransferId = @PaymentTransferId
				UNION ALL
				SELECT ptoc.PaymentTransferId, (ptoc.Value * IIF(ptoc.Nature = 1, 1, -1)) AS Value
				FROM Payments.PaymentTransferOtherConcept ptoc
				WHERE ptoc.PaymentTransferId = @PaymentTransferId
			) ptd
			GROUP BY ptd.PaymentTransferId
		) ptd ON pt.Id = ptd.PaymentTransferId
		WHERE pt.Id = @PaymentTransferId

		/********************************** AJUSTE DIFERENCIAL CXP *************************************************/
		----- Existen cruces que solo se cargan por otros conceptos y no por facturas
		IF (SELECT COUNT(*) FROM Payments.PaymentTransfer PT 
			INNER JOIN Payments.PaymentTransferDetail PTD ON PTD.PaymentTransferId = PT.Id
			WHERE PT.ID = @PaymentTransferId) > 0
		BEGIN 

			DELETE FROM @responseRevaluation
			declare @ListAccountPayable TABLE (	Id INT  NOT NULL,
												ValuePaid NUMERIC(20,2) NOT NULL,
												EntityName VARCHAR(250),
												EntityId INT)

			declare @TableResultTRMAdjustment TABLE (MessageOutput varchar(max))
			declare @ListAccountPayableXml as XML,
					@XmlOutput_CxP as XML
		
			INSERT INTO @ListAccountPayable
			SELECT	 ap.Id
					,ptd.Value
					,'PaymentTransfer'
					,@PaymentTransferId
			FROM Payments.AccountPayable ap
			JOIN 
			(
				SELECT ptd.AccountPayableId, SUM(IIF(ptd.ValueInCurrencyInvoice = 0, ptd.Value, ptd.ValueInCurrencyInvoice)) Value
				FROM Payments.PaymentTransferDetail ptd 
				WHERE ptd.PaymentTransferId = @PaymentTransferId
				GROUP BY ptd.AccountPayableId
			) ptd ON ap.Id = ptd.AccountPayableId

			SELECT @ListAccountPayableXml = CONVERT(xml, 
													(
														SELECT * FROM @ListAccountPayable AccountPayable 
														For xml AUTO,TYPE, ELEMENTS
													))

			EXEC [Payments].[SP_AccountPayableRevaluation]
				@ListAccountPayableXml,
				@CodeUser,@XmlOutput_CxP OUTPUT
						
			INSERT @responseRevaluation
			SELECT
			t.x.value('Code[1]', 'Varchar(20)')  Code,
			t.x.value('MessageOutput[1]', 'varchar(max)')  MessageOutput,
			t.x.value('JournalVoucherId[1]', 'INT')  JournalVoucherId
			from @XmlOutput_CxP.nodes('/TableResult') t(x);

			IF EXISTS(SELECT 1 FROM @responseRevaluation) BEGIN

				SET @Message_DiffAdjustment = CONCAT(@Message_DiffAdjustment,' | ',(select STRING_AGG(MessageResult, ', ')  from @responseRevaluation))

				IF EXISTS(select 1 from @responseRevaluation WHERE Code = '999') BEGIN
				
					SELECT 999 AS CodeMessage,
							CONCAT( STRING_AGG(MessageResult, ', '),' - ', 'Cruce de Anticipo vs CxP') AS [Message],
							'' AS Consecutive
					from @responseRevaluation 
					WHERE Code = '999'
				
					RETURN
				END
			END
		END
		/*******************************************************************************************/
		/***************************** MODIFICACION CUENTAS POR PAGAR *****************************/

		UPDATE aps
			SET aps.Balance = aps.Balance - IIF(ptd.ValueInCurrencyInvoice = 0, ptd.Value, ptd.ValueInCurrencyInvoice),
				aps.ValueTransfers = aps.ValueTransfers + IIF(ptd.ValueInCurrencyInvoice = 0, ptd.Value, ptd.ValueInCurrencyInvoice)
		FROM Payments.PaymentTransferDetail ptd
		JOIN Payments.AccountPayableShares aps ON ptd.AccountPayableShareId = aps.Id			
		WHERE ptd.PaymentTransferId = @PaymentTransferId

		UPDATE ap
			SET ap.Balance = ap.Balance - ptd.Value
		FROM Payments.AccountPayable ap
		JOIN 
		(
			SELECT ptd.AccountPayableId, SUM(IIF(ptd.ValueInCurrencyInvoice = 0, ptd.Value, ptd.ValueInCurrencyInvoice)) Value
			FROM Payments.PaymentTransferDetail ptd 
			WHERE ptd.PaymentTransferId = @PaymentTransferId
			GROUP BY ptd.AccountPayableId
		) ptd ON ap.Id = ptd.AccountPayableId

		/****************************** CREACIÓN DOCUMENTO CONTABLE *******************************/

		INSERT INTO @JournalVourcherTmp
			( IdJournalVoucher, VoucherDate, Status, Detail, EntityCode, EntityId, EntityName,CurrencyId )
			SELECT TOP 1
				@JournalVoucherTypeId IdJournalVoucher, 
				pt.DocumentDate VoucherDate, 
				2 Status, 
				pt.Observations Detail,
				pt.Code EntityCode,
				pt.Id EntityId,
				'PaymentTransfer' EntityName,
				ap.CurrencyId
			FROM Payments.PaymentTransfer pt
			join Payments.AdvancePayments ap on pt.AdvancePaymentId = ap.Id
			WHERE pt.Id = @PaymentTransferId

		INSERT INTO @JournalVourcherDetailTmp
			( IdMainAccount, IdThirdParty, IdCostCenter, Detail, DebitValue, CreditValue )
			-------------------------------- FACTURAS --------------------------------
			SELECT
				ptd.MainAccountId AS IdMainAccount,
				ap.IdThirdParty AS IdThirdParty,
				ptd.CostCenterId AS IdCostCenter,
				NULL AS Detail,
				ptd.Value AS DebitValue,
				0 AS CreditValue
			FROM Payments.PaymentTransfer pt
			JOIN Payments.PaymentTransferDetail ptd ON pt.Id = ptd.PaymentTransferId
			JOIN Payments.AccountPayable ap ON ptd.AccountPayableId = ap.Id
			WHERE pt.Id = @PaymentTransferId
			--------------------------------------------------------------------------	
			UNION ALL
			----------------------------- OTROS CONCEPTOS ----------------------------
			SELECT
				ptoc.MainAccountId AS IdMainAccount,
				ptoc.ThirdPartyId AS IdThirdParty,
				ptoc.CostCenterId AS IdCostCenter,
				NULL AS Detail,
				IIF(ptoc.Nature = 1, ptoc.Value, 0) AS DebitValue,
				IIF(ptoc.Nature = 1, 0, ptoc.Value) AS CreditValue
			FROM Payments.PaymentTransfer pt
			JOIN Payments.PaymentTransferOtherConcept ptoc ON pt.Id = ptoc.PaymentTransferId
			WHERE pt.Id = @PaymentTransferId
			--------------------------------------------------------------------------	
			UNION ALL
			---------------------------------- TOTAL ---------------------------------
			SELECT
				pt.MainAccountId AS IdMainAccount,
				pt.ThirdPartyId AS IdThirdParty,
				pt.CostCenterId AS IdCostCenter,
				NULL AS Detail,
				IIF(ptd.Value > 0, 0, ABS(ptd.Value)) AS DebitValue,
				IIF(ptd.Value > 0, ptd.Value, 0) AS CreditValue
			FROM Payments.PaymentTransfer pt
			JOIN 
			(
				SELECT PaymentTransferId, SUM(Value) AS Value
				FROM
				(
					SELECT ptd.PaymentTransferId, ptd.Value
					FROM Payments.PaymentTransferDetail ptd 
					WHERE ptd.PaymentTransferId = @PaymentTransferId
					UNION ALL
					SELECT ptoc.PaymentTransferId, (ptoc.Value * IIF(ptoc.Nature = 1, 1, -1)) AS Value
					FROM Payments.PaymentTransferOtherConcept ptoc
					WHERE ptoc.PaymentTransferId = @PaymentTransferId
				) ptd
				GROUP BY ptd.PaymentTransferId
			) ptd ON pt.Id = ptd.PaymentTransferId
			WHERE pt.Id = @PaymentTransferId

		--Eliminamos cuentas en 0
		DELETE jv FROM @JournalVourcherDetailTmp jv WHERE ISNULL(jv.DebitValue, 0) = 0 AND ISNULL(jv.CreditValue, 0) = 0

		--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
		SELECT @JournalVoucherXML = CONVERT(xml, 
			(
				SELECT * FROM @JournalVourcherTmp JournalVoucher 
				JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
				For xml AUTO,TYPE, ELEMENTS
			)
		)

		/************************************************************************************************************************************/
		
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
			SELECT 999 as CodeMessage, @Message as Message, '' AS Consecutive
			RETURN
		END

		--Se obtiene el consecutivo que generó el comprobante contable
		DECLARE @Consecutive as VARCHAR(MAX) = isnull( (SELECT CAST( Consecutive AS VARCHAR(30)) FROM GeneralLedger.JournalVouchers WHERE id = @IdJournalVoucherResult),0)

		/************************************************************************************************************************************/

		DELETE pc 
		FROM Payments.PaymentTransfer pt
		JOIN Payments.PaymentsControl pc ON pc.DocumentType = 4 AND pc.DocumentNumber = pt.Code
		WHERE pt.Id = @PaymentTransferId

		UPDATE pt
			SET pt.ModificationUser = @CodeUser,
				pt.ModificationDate = [Common].[GETDATE](),
				pt.ConfirmationUser = @CodeUser,
				pt.ConfirmationDate = [Common].[GETDATE](),
				pt.Status = 2
		FROM Payments.PaymentTransfer pt
		WHERE pt.Id = @PaymentTransferId

		SELECT	0 AS CodeMessage, 
				CONCAT('Se confirmó correctamente', ' ',COALESCE(@Message_DiffAdjustment,'')) AS Message,
				@Consecutive AS Consecutive

	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Consecutive
	END CATCH
END