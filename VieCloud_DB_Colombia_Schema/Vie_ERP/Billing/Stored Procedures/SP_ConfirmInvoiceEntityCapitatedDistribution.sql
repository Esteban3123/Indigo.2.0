-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-03-16
-- Description:	Procedimiento que se encarga de confirmar la distribucion Factura Monto Fijo
-- =============================================
CREATE PROCEDURE [Billing].[SP_ConfirmInvoiceEntityCapitatedDistribution]
    @InvoiceEntityCapitatedDistributionId AS INT,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/
	
	DECLARE @errors VARCHAR(MAX),
			@InvoiceEntityCapitatedId INT

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
			@CapitationRevenueMainAccountId AS INT,
			@CapitationProfitMainAccountId INT,
			@CapitationLossMainAccountId INT,
			@AccountingForSurgical TINYINT,			
			@DistributeCapitationControls TINYINT,
			--------------------------------------
			@JournalVoucherXML as XML,
			@CodeMessage Int,
			@Message Varchar(Max),
			@IdJournalVoucherResult Int

	--tabla temporal para almacenar el resultado del movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	BEGIN TRY
		SELECT 
			@InvoiceEntityCapitatedId = iecd.InvoiceEntityCapitatedId,
			@JournalVoucherTypeId = sb.InvoiceEntityCapitatedDistributionJournalVoucherTypeId,
			@CapitationRevenueMainAccountId = sb.CapitationRevenueMainAccountId,
			@CapitationProfitMainAccountId = sb.CapitationProfitMainAccountId,
			@CapitationLossMainAccountId = sb.CapitationLossMainAccountId,
			@AccountingForSurgical = sb.AccountingForSurgical,
			@DistributeCapitationControls = sb.DistributeCapitationControls
		FROM Billing.InvoiceEntityCapitatedDistribution iecd
		LEFT JOIN Billing.SettingsBilling sb ON iecd.OperatingUnitId = sb.IdOperatingUnit
		WHERE iecd.Id = @InvoiceEntityCapitatedDistributionId

		/*************************************VALIDACIONES************************************/

		-- Valido que el registro se encuentre en estado registrado
		IF EXISTS (SELECT 1 FROM Billing.InvoiceEntityCapitatedDistribution iec WHERE iec.Id = @InvoiceEntityCapitatedDistributionId AND iec.Status <> 1)
		BEGIN
			SELECT 999 as CodeMessage, 'El registro se encuentra en estado: ' + IIF(iec.Status = 2, 'Confirmado', 'Anulado') as Message, '' AS Consecutive
			FROM Billing.InvoiceEntityCapitatedDistribution iec
			WHERE iec.Id = @InvoiceEntityCapitatedDistributionId
			RETURN
		END

		--- Valido que la factura monto fijo no haya sido distribuida
		IF EXISTS 
		( 
			SELECT 1 
			FROM [Billing].[InvoiceEntityCapitatedDistribution] iecd 
			WHERE iecd.Id <> @InvoiceEntityCapitatedDistributionId 
				AND iecd.InvoiceEntityCapitatedId = @InvoiceEntityCapitatedId 
				AND iecd.Status NOT IN (3, 4)
		)
		BEGIN
			SELECT @errors = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + iecd.Code
					FROM [Billing].[InvoiceEntityCapitatedDistribution] iecd 
					WHERE iecd.Id <> @InvoiceEntityCapitatedDistributionId 
						AND iecd.InvoiceEntityCapitatedId = @InvoiceEntityCapitatedId 
						AND iecd.Status NOT IN (3, 4)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeMessage, 'La factura Monto Fijo ya se encuentra en otra distribución ' + CHAR(13) + CHAR(10) + @errors AS Message, '' AS Consecutive
			RETURN
		END

		--- Valido que los controles de servicios no se encuentren ya distribuidas
		IF EXISTS 
		(
			SELECT 1 
			FROM [Billing].[InvoiceEntityCapitatedDistributionDetail] iecdd
			JOIN [Billing].[InvoiceEntityCapitatedDistributionDetail] iecdd2 ON iecdd.InvoiceId = iecdd2.InvoiceId
				AND iecdd.InvoiceEntityCapitatedDistributionId <> iecdd2.InvoiceEntityCapitatedDistributionId
			JOIN Billing.InvoiceEntityCapitatedDistribution iecd2 ON iecdd2.InvoiceEntityCapitatedDistributionId = iecd2.Id AND iecd2.Status = 2
			WHERE iecdd.InvoiceEntityCapitatedDistributionId = @InvoiceEntityCapitatedDistributionId
		)
		BEGIN
			SELECT @errors = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + i.InvoiceNumber + ' (Distribución: ' + iecd2.Code + ')'
					FROM [Billing].[InvoiceEntityCapitatedDistributionDetail] iecdd
					JOIN [Billing].[InvoiceEntityCapitatedDistributionDetail] iecdd2 ON iecdd.InvoiceId = iecdd2.InvoiceId
						AND iecdd.InvoiceEntityCapitatedDistributionId <> iecdd2.InvoiceEntityCapitatedDistributionId
					JOIN Billing.InvoiceEntityCapitatedDistribution iecd2 ON iecdd2.InvoiceEntityCapitatedDistributionId = iecd2.Id AND iecd2.Status = 2
					JOIN Billing.Invoice i ON iecdd.InvoiceId = i.Id
					WHERE iecdd.InvoiceEntityCapitatedDistributionId = @InvoiceEntityCapitatedDistributionId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeMessage, 'Los siguientes controles de servicios ya se encuentran en otra distribución ' + CHAR(13) + CHAR(10) + @errors AS Message, '' AS Consecutive
			RETURN
		END

		--- Valido que los controles de servicios no se encuentren anulados
		IF EXISTS 
		(
			SELECT 1 
			FROM [Billing].[InvoiceEntityCapitatedDistributionDetail] iecdd
			JOIN Billing.Invoice i ON iecdd.InvoiceId = i.Id
			WHERE iecdd.InvoiceEntityCapitatedDistributionId = @InvoiceEntityCapitatedDistributionId AND 
				i.Status <> 1
		)
		BEGIN
			SELECT @errors = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + i.InvoiceNumber + ' (Estado: ' + IIF(i.Status = 2, 'ANULADO', 'N/A') + ')'
					FROM [Billing].[InvoiceEntityCapitatedDistributionDetail] iecdd
					JOIN Billing.Invoice i ON iecdd.InvoiceId = i.Id
					WHERE iecdd.InvoiceEntityCapitatedDistributionId = @InvoiceEntityCapitatedDistributionId AND 
						i.Status <> 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeMessage, 'Los siguientes controles de servicios no se encuentran en estado facturado ' + CHAR(13) + CHAR(10) + @errors AS Message, '' AS Consecutive
			RETURN
		END

		IF NOT EXISTS (SELECT 1 FROM GeneralLedger.VieBot vb WHERE vb.Form = 'InvoiceEntityCapitatedDistribution' AND vb.Allow = 1)
		BEGIN
			SELECT 999 as CodeMessage, 'Debe parametrizar al menos un libro contable' as Message, '' AS Consecutive
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
			iecd.DocumentDate, 
			2, 
			'Distribución de Ingresos de la Factura No. ' +  i.InvoiceNumber + ' - Código ' + iec.Code + ' - Grupo de Atención: ' + CONCAT(cg.Code, ' - ', cg.Name), 
			iecd.Code, 
			iecd.Id, 
			'InvoiceEntityCapitatedDistribution'
		FROM Billing.InvoiceEntityCapitatedDistribution iecd
		JOIN Billing.InvoiceEntityCapitated iec ON iecd.InvoiceEntityCapitatedId = iec.Id
		JOIN Contract.CareGroup cg ON iec.CareGroupId = cg.Id
		LEFT JOIN Billing.Invoice i ON iec.InvoiceId = i.Id
		WHERE iecd.Id = @InvoiceEntityCapitatedDistributionId

		-- Reversamos el ingreso de la factura monto fijo
		INSERT INTO @JournalVourcherDetailTmp
		(
			IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail
		)
		SELECT
			ma.Id AS IdMainAccount,
			IIF(ma.HandlesThirdParty =1, ha.ThirdPartyId, NULL) AS IdThirdParty,
			IIF(ma.HandlesCostCenter = 1, cg.CostCenterId, NULL) AS IdCostCenter,
			iecd.InvoiceValue,
			0,
			'Reversión del ingreso de la Factura Monto Fijo'
		FROM Billing.InvoiceEntityCapitatedDistribution iecd
		JOIN Billing.InvoiceEntityCapitated iec ON iecd.InvoiceEntityCapitatedId = iec.Id
		JOIN Contract.CareGroup cg ON iec.CareGroupId = cg.Id
		JOIN Contract.Contract c ON cg.ContractId = c.Id
		JOIN Contract.HealthAdministrator ha ON c.HealthAdministratorId = ha.Id
		JOIN GeneralLedger.MainAccounts ma ON ma.Id = @CapitationRevenueMainAccountId		
		WHERE iecd.Id = @InvoiceEntityCapitatedDistributionId

		DECLARE @rows INT = 1,
				@InvoiceEntityCapitatedDistributionDetailId INT = 0,
				@RevenueControlDetailId AS INT

		WHILE @rows > 0
		BEGIN
			SELECT TOP 1
				@InvoiceEntityCapitatedDistributionDetailId = iecdd.Id,
				@RevenueControlDetailId = i.RevenueControlDetailId
			FROM Billing.InvoiceEntityCapitatedDistributionDetail iecdd
			JOIN Billing.Invoice i ON iecdd.InvoiceId = i.Id
			WHERE iecdd.InvoiceEntityCapitatedDistributionId = @InvoiceEntityCapitatedDistributionId
				AND iecdd.Id > @InvoiceEntityCapitatedDistributionDetailId
				AND i.RevenueControlDetailId IS NOT NULL
			ORDER BY iecdd.Id

			-- Verifico que haya encontrado un resultado
			SET @rows = @@RowCount
			IF @rows = 0
			BEGIN
				BREAK
			END

			-- Distribuimos los ingresos de cada una de las facturas asociadas
			INSERT INTO @JournalVourcherDetailTmp
			(
				IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail
			)
			SELECT 
				IIF(@DistributeCapitationControls = 1, @CapitationRevenueMainAccountId, IdMainAccount), 
				IdThirdParty, 
				IdCostCenter, 
				SUM(IIF(DebitValue > 0, DebitValue, IIF(CreditValue < 0, ABS(CreditValue), 0))) DebitValue, 
				SUM(IIF(CreditValue > 0, CreditValue, IIF(DebitValue < 0, ABS(DebitValue), 0))) CreditValue,
				'Distribución del control de Monto Fijo'
			FROM Billing.fnJournalVoucherIncomeInvoiceDetail(@RevenueControlDetailId, @AccountingForSurgical, 2, NULL)
			GROUP BY IIF(@DistributeCapitationControls = 1, @CapitationRevenueMainAccountId, IdMainAccount), 
				IdThirdParty, 
				IdCostCenter
		END

		-- Insertamos el valor de la utilidad o perdida, de acuerdo a lo que corresponda
		INSERT INTO @JournalVourcherDetailTmp
		(
			IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail
		)
		SELECT
			ma.Id AS IdMainAccount,
			IIF(ma.HandlesThirdParty = 1, ha.ThirdPartyId, NULL) AS IdThirdParty,
			IIF(ma.HandlesCostCenter = 1, cg.CostCenterId, NULL) AS IdCostCenter,
			IIF(iecd.TotalControlValue > iecd.InvoiceValue, iecd.TotalControlValue - iecd.InvoiceValue, 0),
			IIF(iecd.InvoiceValue > iecd.TotalControlValue, iecd.InvoiceValue - iecd.TotalControlValue, 0),
			IIF(iecd.InvoiceValue > iecd.TotalControlValue, 'Utilidad por distribucion Factura Monto Fijo', 'Perdida por distribucion Factura Monto Fijo')
		FROM Billing.InvoiceEntityCapitatedDistribution iecd
		JOIN Billing.InvoiceEntityCapitated iec ON iecd.InvoiceEntityCapitatedId = iec.Id
		JOIN Contract.CareGroup cg ON iec.CareGroupId = cg.Id
		JOIN Contract.Contract c ON cg.ContractId = c.Id
		JOIN Contract.HealthAdministrator ha ON c.HealthAdministratorId = ha.Id
		JOIN GeneralLedger.MainAccounts ma 
			ON (iecd.InvoiceValue > iecd.TotalControlValue AND ma.Id = @CapitationProfitMainAccountId)
				OR
				(iecd.InvoiceValue < iecd.TotalControlValue AND ma.Id = @CapitationLossMainAccountId)
		WHERE iecd.Id = @InvoiceEntityCapitatedDistributionId
			AND iecd.InvoiceValue <> iecd.TotalControlValue

		--Se eliminan los registros que traen valores en cero
		delete from @JournalVourcherDetailTmp where CreditValue = 0 and DebitValue = 0

		--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
		SELECT @JournalVoucherXML = CONVERT(xml, 
			(
				SELECT * FROM @JournalVourcherTmp JournalVoucher 
				JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
				For xml AUTO,TYPE, ELEMENTS
			)
		)

		/************************************************************************************************************************************/

		--Se consume el sp que guarda el mvimiento contable
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
			SET iecd.Status = 2,
				iecd.ConfirmationUser = @CodeUser,
				iecd.ConfirmationDate = [Common].[GETDATE]()
		FROM Billing.InvoiceEntityCapitatedDistribution iecd
		WHERE iecd.Id = @InvoiceEntityCapitatedDistributionId

		SELECT 0 AS CodeMessage, 'Se confirmó correctamente' AS Message, @Consecutive AS Consecutive
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Consecutive
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que confirma la distribución de una factura de monto fijo (capitación) entre unidades operativas. Valida que la distribución esté en estado registrado, que la factura capitada no esté ya confirmada en otra distribución, y que las facturas de controles de servicios incluidas no estén duplicadas ni anuladas. Una vez superadas las validaciones, genera el comprobante contable correspondiente usando la configuración contable definida en SettingsBilling (tipo de comprobante, cuentas de ingreso, utilidad y pérdida de capitación), registrando el movimiento en el libro contable. Es el paso final del proceso de distribución de capitación antes de que los valores queden asentados contablemente por unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmInvoiceEntityCapitatedDistribution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmInvoiceEntityCapitatedDistribution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma una distribución de factura de monto fijo (capitación), genera el comprobante contable que reversa el ingreso original y distribuye los ingresos por cada control de servicio, registrando además la utilidad o pérdida resultante.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La distribución debe existir y estar en estado 1 (Registrado); si está en 2 (Confirmado) o 3+ (Anulado) se rechaza.; Ninguna otra distribución de la misma InvoiceEntityCapitated puede estar activa (Status NOT IN (3,4)).; Los controles de servicios (InvoiceId del detalle) no pueden estar incluidos en otra distribución ya confirmada (Status=2).; Todas las facturas asociadas en el detalle deben estar en estado 1 (facturado); no anuladas.; Debe existir al menos un libro contable parametrizado en GeneralLedger.VieBot para Form=''InvoiceEntityCapitatedDistribution'' con Allow=1.; La unidad operativa debe tener parametrizado InvoiceEntityCapitatedDistributionJournalVoucherTypeId en Billing.SettingsBilling.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se confirma una distribución por InvoiceEntityCapitated a la vez (las demás deben estar anuladas, estados 3 o 4).; Un mismo control de servicio (InvoiceId) no puede pertenecer a dos distribuciones confirmadas simultáneamente.; La cuenta contable utiliza tercero (ThirdPartyId del HealthAdministrator del contrato) solo si MainAccount.HandlesThirdParty=1, y centro de costo (del CareGroup) solo si HandlesCostCenter=1.; Las líneas con DebitValue=0 y CreditValue=0 son eliminadas antes de generar el XML del comprobante.; La actualización de estado a Confirmado solo ocurre si el comprobante contable se generó sin errores.; Los errores en el TRY son capturados y devueltos como CodeMessage=999 con número de línea, sin propagar excepción.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura de monto fijo (capitación); Distribución de ingresos; Comprobante contable; Reversión de ingreso; Utilidad por distribución; Pérdida por distribución; Grupo de atención (CareGroup); Administradora de salud (EPS); Control de servicios; Libro contable legal; Tercero contable; Centro de costo', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Billing.InvoiceEntityCapitatedDistribution: Tras generar exitosamente el comprobante contable, marca Status=2 (Confirmado) y registra ConfirmationUser y ConfirmationDate=[Common].[GETDATE]() para el Id procesado.; [INSERT] GeneralLedger.JournalVouchers: Crea un comprobante contable vía GeneralLedger.SP_CreateAndValidateJournalVoucherMovement con detalle: (1) débito a la cuenta de ingreso por capitación por InvoiceValue (reversión); (2) por cada control de servicio del detalle, distribuye los movimientos de Billing.fnJournalVoucherIncomeInvoiceDetail, reemplazando la cuenta por @CapitationRevenueMainAccountId si DistributeCapitationControls=1; (3) registra utilidad (cuenta CapitationProfit) si InvoiceValue>TotalControlValue o pérdida (cuenta CapitationLoss) si InvoiceValue<TotalControlValue.; [RETURN_RESULT] resultset: Devuelve CodeMessage=0 con el consecutivo del comprobante en éxito, o CodeMessage=999 con el mensaje de error en cualquier validación fallida o excepción capturada.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Status de la distribución <> 1 → Retorna error 999 indicando si está Confirmado (2) o Anulado (otro) else Continúa con validaciones; si Existe otra distribución de la misma InvoiceEntityCapitatedId con Status NOT IN (3,4) → Retorna error 999 listando los códigos de distribuciones en conflicto; si Existe algún InvoiceId del detalle ya incluido en otra distribución con Status=2 → Retorna error 999 listando facturas y distribución conflictiva; si Alguna factura del detalle tiene Status<>1 → Retorna error 999 listando las facturas no facturadas/anuladas; si @DistributeCapitationControls = 1 → Al distribuir cada control, fuerza IdMainAccount=@CapitationRevenueMainAccountId else Usa la cuenta original devuelta por fnJournalVoucherIncomeInvoiceDetail; si iecd.InvoiceValue > iecd.TotalControlValue → Genera registro de Utilidad usando @CapitationProfitMainAccountId (crédito = diferencia) else Si InvoiceValue < TotalControlValue genera Pérdida con @CapitationLossMainAccountId (débito = diferencia); si son iguales no inserta línea; si @CodeMessage = 999 al ejecutar SP_CreateAndValidateJournalVoucherMovement → Retorna 999 con el mensaje del comprobante y aborta sin actualizar la distribución', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Billing.fnJournalVoucherIncomeInvoiceDetail; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.InvoiceEntityCapitatedDistribution; Billing.SettingsBilling; Billing.InvoiceEntityCapitatedDistributionDetail; Billing.Invoice; Billing.InvoiceEntityCapitated; Contract.CareGroup; Contract.Contract; Contract.HealthAdministrator; GeneralLedger.MainAccounts; GeneralLedger.VieBot; GeneralLedger.JournalVouchers; Billing.fnJournalVoucherIncomeInvoiceDetail', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmInvoiceEntityCapitatedDistribution';
-- GO
