-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-04-23
-- Description:	Reversa una consignacion
-- =============================================
CREATE PROCEDURE [Treasury].[SP_ReverseConsignment]
	@TreasuryNoteId INT,	
	@CodeUser VARCHAR(20)	
AS
BEGIN
	SET NOCOUNT ON;

	/************************************* VARIABLES ************************************/
	
	DECLARE @TreasuryNoteCode VARCHAR(20),
			@ConsignmentId INT

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
		Detail VARCHAR(500),
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
			@Consecutive VARCHAR(30),
			--------Moneda de la nota----
			@TreasuryNoteCurrencyId INT

	--tabla temporal para almacenar el resultado deL movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	BEGIN TRY

		SELECT 
			@TreasuryNoteCode = tn.Code,
			@ConsignmentId = tn.ConsignmentId,
			@JournalVoucherTypeId = st.JournalVoucherTypeTreasuryNotes,
			@TreasuryNoteCurrencyId = tn.CurrencyId
		FROM Treasury.TreasuryNote tn
		LEFT JOIN Treasury.SettingsTreasury st ON tn.OperatingUnitId = st.IdOperatingUnit
		WHERE tn.Id = @TreasuryNoteId

		/*************************************VALIDACIONES************************************/
		
		IF NOT EXISTS 
		(
			SELECT *
			FROM Treasury.Consignment c
			WHERE c.Id = @ConsignmentId AND c.Status = 2
		)
		BEGIN
			SELECT 999 AS CodeMessage, 'El registro no existe o no se encuentra confirmado' AS [Message], 0 AS IdJournalVoucher, '' AS Consecutive
			RETURN
		END

		IF EXISTS 
		(
			SELECT 1
			FROM Treasury.Consignment c
			JOIN Treasury.ConsignmentDetail cd ON c.Id = cd.ConsignmentTransferId
			WHERE c.Id = @ConsignmentId
			GROUP BY c.Id, c.Value
			HAVING c.Value <> SUM(cd.Value)
		)
		BEGIN
			SELECT 999 AS CodeMessage, 'El valor consignado no corresponde con la suma de valores de las cajas' AS [Message], 0 AS IdJournalVoucher, '' AS Consecutive
			RETURN
		END

		IF EXISTS 
		(
			SELECT *
			FROM Treasury.Consignment c
			JOIN Treasury.EntityBankAccounts eba ON c.EntityBankAccountId = eba.Id
			WHERE c.Id = @ConsignmentId AND c.Value > eba.CurrentBalance
		)
		BEGIN
			SELECT 999 AS CodeMessage, 'El saldo de la cuenta bancaria ' + b.Name + ' # Cta. ' + eba.Number + ' es menor al valor consignado'  AS [Message], 0 AS IdJournalVoucher, '' AS Consecutive
			FROM Treasury.Consignment c
			JOIN Treasury.EntityBankAccounts eba ON c.EntityBankAccountId = eba.Id
			JOIN Payroll.Bank b ON eba.IdBank = b.Id
			WHERE c.Id = @ConsignmentId AND c.Value > eba.CurrentBalance
			RETURN
		END

		/*************************************** PROCESO BALANCE DE TESORERIA ************************************/

		INSERT INTO [Treasury].[TreasuryBalance]
		(
			[DocumentNumber],[DocumentDate],[DocumentType],[Nature],[CashRegisterId],[EntityBankAccountId],[PreviousBalance],[ValueMovement],[CreationDate]
		)
		SELECT @TreasuryNoteCode, [Common].[GETDATE](), 4, 2, NULL, eba.Id, eba.CurrentBalance, c.Value, [Common].[GETDATE]()
		FROM Treasury.Consignment c
		JOIN Treasury.EntityBankAccounts eba ON c.EntityBankAccountId = eba.Id
		WHERE c.Id = @ConsignmentId

		INSERT INTO [Treasury].[TreasuryBalance]
		(
			[DocumentNumber],[DocumentDate],[DocumentType],[Nature],[CashRegisterId],[EntityBankAccountId],[PreviousBalance],[ValueMovement],[CreationDate]
		)
		SELECT @TreasuryNoteCode, [Common].[GETDATE](), 4, 1, cr.Id, NULL, cr.CurrentBalance, cd.Value, [Common].[GETDATE]()
		FROM Treasury.Consignment c
		JOIN Treasury.ConsignmentDetail cd ON c.Id = cd.ConsignmentTransferId
		JOIN Treasury.CashRegisters cr ON cd.CashRegisterId = cr.Id
		WHERE c.Id = @ConsignmentId

		/*************************************** PROCESO SALDO DE BANCO Y CAJA ************************************/

		UPDATE eba 
			SET eba.CurrentBalance = eba.CurrentBalance - c.Value,
				eba.ModificationUser = @CodeUser,
				eba.ModificationDate = [Common].[GETDATE]()
		FROM Treasury.Consignment c
		JOIN Treasury.EntityBankAccounts eba ON c.EntityBankAccountId = eba.Id
		WHERE c.Id = @ConsignmentId
		
		UPDATE cr 
			SET cr.CurrentBalance = cr.CurrentBalance + cd.Value,
				cr.ModificationUser = @CodeUser,
				cr.ModificationDate = [Common].[GETDATE]()
		FROM Treasury.Consignment c
		JOIN Treasury.ConsignmentDetail cd ON c.Id = cd.ConsignmentTransferId
		JOIN Treasury.CashRegisters cr ON cd.CashRegisterId = cr.Id
		WHERE c.Id = @ConsignmentId

		/*************************************** PROCESO CONTABLE ************************************/

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
			'Reversión de Consignación con Nota ' +  tn.Code + '. ' + tn.Description, 
			tn.Code, 
			tn.Id, 
			'TreasuryNote'
			,@TreasuryNoteCurrencyId
		FROM Treasury.TreasuryNote tn
		WHERE tn.Id = @TreasuryNoteId

		INSERT INTO @JournalVourcherDetailTmp
		(
			IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail
		)
		SELECT
			ma.Id AS IdMainAccount,
			IIF(ma.HandlesThirdParty =1, eba.ThirdPartyId, NULL) AS IdThirdParty,
			IIF(ma.HandlesCostCenter = 1, c.CostCenterId, NULL) AS IdCostCenter,			
			0,
			c.Value,
			c.Description
		FROM Treasury.Consignment c
		JOIN Treasury.EntityBankAccounts eba ON c.EntityBankAccountId = eba.Id
		JOIN GeneralLedger.MainAccounts ma ON c.MainAccountId = ma.Id
		WHERE c.Id = @ConsignmentId

		UNION ALL

		SELECT
			cd.MainAccountId AS IdMainAccount,
			IIF(ma.HandlesThirdParty =1, cr.ThirdPartyId, NULL) AS IdThirdParty,
			IIF(ma.HandlesCostCenter = 1, cd.CostCenterId, NULL) AS IdCostCenter,
			cd.Value,
			0,
			c.Description
		FROM Treasury.Consignment c
		JOIN Treasury.ConsignmentDetail cd ON c.Id = cd.ConsignmentTransferId
		JOIN Treasury.CashRegisters cr ON cd.CashRegisterId = cr.Id
		JOIN GeneralLedger.MainAccounts ma ON cd.MainAccountId = ma.Id
		WHERE c.Id = @ConsignmentId

		/************************************************************************************************************************************/

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

		/*************************************** ACTUALIZACION DE LA CONSIGNACION ************************************/

		UPDATE c
			SET c.Status = 4,
				c.ModificationUser = @CodeUser,
				c.ModificationDate = [Common].[GETDATE](),
				c.ReversedUser = @CodeUser,
				c.ReversedDate = [Common].[GETDATE]()
		FROM Treasury.Consignment c
		WHERE c.Id = @ConsignmentId

		SELECT 0 AS CodeMessage, @Message AS [Message], @IdJournalVoucherResult AS IdJournalVoucher, @Consecutive AS Consecutive

	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS [Message], 0 AS IdJournalVoucher, '' AS Consecutive
	END CATCH
	
END