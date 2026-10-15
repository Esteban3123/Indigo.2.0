
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-03-16
-- Description:	Procedimiento que se encarga de Reversar la distribución de la Factura de Monto Fijo
-- =============================================
CREATE PROCEDURE [Billing].[SP_ReverseInvoiceEntityCapitatedDistribution]
    @InvoiceEntityCapitatedDistributionId AS INT,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

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
		IsClosedYear TINYINT DEFAULT(0)
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
			@LegalBookId AS INT,
			@JournalVoucherId AS INT,
			--------------------------------------
			@JournalVoucherXML as XML,
			@CodeMessage Int,
			@Message Varchar(Max),
			@IdJournalVoucherResult Int

	--tabla temporal para almacenar el resultado del movimiento contable
	DECLARE @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	BEGIN TRY

		SELECT 
			@JournalVoucherTypeId = sb.ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId
		FROM Billing.InvoiceEntityCapitatedDistribution iecd
		LEFT JOIN Billing.SettingsBilling sb ON iecd.OperatingUnitId = sb.IdOperatingUnit
		WHERE iecd.Id = @InvoiceEntityCapitatedDistributionId

		SELECT @LegalBookId = lb.Id
		FROM GeneralLedger.LegalBook lb
		WHERE lb.OfficialBook = 1

		SELECT @JournalVoucherId = jv.Id
		FROM GeneralLedger.JournalVouchers jv
		WHERE jv.EntityName = 'InvoiceEntityCapitatedDistribution' 
			AND jv.EntityId = @InvoiceEntityCapitatedDistributionId 
			AND jv.LegalBookId = @LegalBookId

		/*************************************VALIDACIONES************************************/

		-- Valido que el registro se encuentre en estado confirmado
		IF EXISTS (SELECT 1 FROM Billing.InvoiceEntityCapitatedDistribution iec WHERE iec.Id = @InvoiceEntityCapitatedDistributionId AND iec.Status <> 2)
		BEGIN
			SELECT 999 as CodeMessage, 'El registro se encuentra en estado: ' + IIF(iec.Status = 1, 'Registrado', IIF(iec.Status = 3, 'Anulado', 'N/A')) as Message, '' AS Consecutive
			FROM Billing.InvoiceEntityCapitatedDistribution iec
			WHERE iec.Id = @InvoiceEntityCapitatedDistributionId
			RETURN
		END

		IF @JournalVoucherId IS NULL
		BEGIN
			SELECT 999 as CodeMessage, 'No se encontró ningun documento contable a reversar' as Message, '' AS Consecutive
			RETURN
		END

		IF @JournalVoucherTypeId IS NULL
		BEGIN
			SELECT 999 as CodeMessage, 'Debe parametrizar el Tipo de Comprobante de Distribución de Ingresos de Monto Fijo para la unidad Operativa' as Message, '' AS Consecutive
			RETURN
		END

		/*************************************** PROCESO ************************************/

		--Inserto la cabecera del comprobante contable
		INSERT INTO @JournalVourcherTmp
		(
			IdJournalVoucher, 
			VoucherDate, 
			Status, 
			Detail, 
			EntityCode, 
			EntityId, 
			EntityName
		)
		SELECT
			@JournalVoucherTypeId, 
			[Common].[GETDATE](), 
			2, 
			'Reversión: ' + jv.Detail, 
			jv.EntityCode, 
			jv.EntityId, 
			'InvoiceEntityCapitatedDistribution'
		FROM GeneralLedger.JournalVouchers jv
		WHERE jv.Id = @JournalVoucherId

		-- Reversamos el ingreso de la factura monto fijo
		INSERT INTO @JournalVourcherDetailTmp
		(
			IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail
		)
		SELECT
			jvd.IdMainAccount,
			jvd.IdThirdParty,
			jvd.IdCostCenter,
			jvd.CreditValue,
			jvd.DebitValue,
			'Reversión: ' + jvd.Detail
		FROM GeneralLedger.JournalVoucherDetails jvd
		WHERE jvd.IdAccounting = @JournalVoucherId

		--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
		SELECT @JournalVoucherXML = CONVERT(xml, 
			(
				SELECT * FROM @JournalVourcherTmp JournalVoucher 
				JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
				For xml AUTO,TYPE, ELEMENTS
			)
		)

		/************************************************************************************************************************************/

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
			SELECT 999 as CodeMessage, @Message as Message, '' AS Consecutive
			RETURN
		END

		--Se obtiene el consecutivo que generó el comprobante contable
		DECLARE @Consecutive as VARCHAR(max) = isnull( (select cast( Consecutive as varchar(30))  from GeneralLedger.JournalVouchers where id = @IdJournalVoucherResult),0)

		/************************************************************************************************************************************/

		--Actualizo el estado de la factura
		UPDATE iecd
			SET iecd.Status = 4,
				iecd.AnnulmentUser = @CodeUser,
				iecd.AnnulmentDate = [Common].[GETDATE]()
		FROM Billing.InvoiceEntityCapitatedDistribution iecd
		WHERE iecd.Id = @InvoiceEntityCapitatedDistributionId

		SELECT 0 AS CodeMessage, 'Se anuló correctamente' AS Message, @Consecutive AS Consecutive
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Consecutive
	END CATCH
END