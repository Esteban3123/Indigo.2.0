
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reversa la distribución contable de una factura de monto fijo (capitación) previamente confirmada. Valida que el registro esté en estado confirmado y que exista un comprobante contable asociado, luego genera un comprobante de reversión invirtiendo los débitos y créditos originales mediante el motor contable. Actualiza el estado de la distribución a anulado en la tabla InvoiceEntityCapitatedDistribution y retorna el consecutivo del nuevo comprobante generado. Se usa cuando se necesita anular o reversar una distribución de ingresos de capitación ya contabilizada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseInvoiceEntityCapitatedDistribution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseInvoiceEntityCapitatedDistribution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reversa contablemente la distribución de una factura de monto fijo (capitación), generando un comprobante contable inverso y anulando el registro de distribución.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La distribución debe existir en Billing.InvoiceEntityCapitatedDistribution con Status = 2 (Confirmado); de lo contrario se rechaza indicando el estado actual.; Debe existir un comprobante contable previo en GeneralLedger.JournalVouchers asociado a EntityName=''InvoiceEntityCapitatedDistribution'' y al Id de la distribución sobre el libro oficial.; La unidad operativa de la distribución debe tener parametrizado ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId en Billing.SettingsBilling.; Debe existir un LegalBook con OfficialBook = 1.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reversan distribuciones en estado Confirmado (Status=2); el resultado de la reversión deja la distribución en Status=4 (Anulado).; El comprobante de reversión se construye intercambiando débitos y créditos del comprobante original, manteniendo cuenta, tercero y centro de costo.; Los descriptores del comprobante y de sus detalles de reversión se anteponen con el literal ''Reversión: ''.; La búsqueda del comprobante original se restringe al libro contable oficial (LegalBook.OfficialBook=1).; Cualquier error en el TRY se captura y se devuelve como CodeMessage=999 con ERROR_MESSAGE() y línea, sin propagar la excepción.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura de monto fijo (capitación); Distribución de ingresos por entidad capitada; Comprobante contable de reversión; Libro contable oficial; Anulación de documento; Tipo de comprobante por unidad operativa; Débito/Crédito contable', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] GeneralLedger.JournalVouchers: Vía EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement, se crea un comprobante contable de reversión con tipo = ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId, fecha = Common.GETDATE(), Status=2 y Detail prefijado con ''Reversión: ''.; [INSERT] GeneralLedger.JournalVoucherDetails: Por cada detalle del comprobante original (IdAccounting = @JournalVoucherId) se crea un detalle inverso intercambiando DebitValue↔CreditValue y prefijando el detalle con ''Reversión: ''.; [UPDATE] Billing.InvoiceEntityCapitatedDistribution: Tras crear exitosamente el comprobante de reversión, se marca la distribución como anulada: Status=4, AnnulmentUser=@CodeUser, AnnulmentDate=Common.GETDATE().; [RETURN_RESULT] RESULT: Devuelve (CodeMessage, Message, Consecutive): 999 con mensaje de error si el estado no es 2, no existe comprobante a reversar, falta parametrización, falla SP_CreateAndValidate o hay excepción; 0 con ''Se anuló correctamente'' y consecutivo del nuevo comprobante en éxito.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe distribución con Id=@InvoiceEntityCapitatedDistributionId y Status<>2 → Retorna CodeMessage=999 indicando estado actual (''Registrado'' si Status=1, ''Anulado'' si Status=3, ''N/A'' en otro caso) y termina.; si @JournalVoucherId IS NULL (no se halló comprobante previo en libro oficial) → Retorna CodeMessage=999 ''No se encontró ningun documento contable a reversar'' y termina.; si @JournalVoucherTypeId IS NULL (sin parametrización de tipo de comprobante de reversión en SettingsBilling) → Retorna CodeMessage=999 exigiendo parametrizar el Tipo de Comprobante de Distribución de Ingresos de Monto Fijo y termina.; si El SP de creación de comprobante devuelve code=999 → Retorna CodeMessage=999 con el mensaje recibido y no actualiza la distribución.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.InvoiceEntityCapitatedDistribution; Billing.SettingsBilling; GeneralLedger.LegalBook; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseInvoiceEntityCapitatedDistribution';
-- GO
