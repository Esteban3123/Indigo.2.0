-- =============================================
-- Author:		
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GeneratePartialPayments]
	@PaymentsCXml XML,
	@CodeUser VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @PartialPaymentCId INT,
			@Consecutive DECIMAL,
			@ProcessDate DATETIME,
			@CustomerId INT, 
			@DocumentDate DATETIME, 
			@State CHAR(1), 
			@Comments VARCHAR(250),
			@EntityId INT,
			@EntityCode VARCHAR(20),
			@EntityName VARCHAR(250),
			-------------------------------------------------------------------
			@errors VARCHAR(MAX),
			-------------------------------------------------------------------
			@PartialPaymentsDetailRows INT, 
			@PartialPaymentsDetailId INT,
			@InvoiceNumber VARCHAR(50),
			@PaymentValue DECIMAL(18,0),
			@Value DECIMAL(18,0),
			@GlosaMovementGlosaRows INT, 
			@GlosaMovementGlosaId INT

	DECLARE @PartialPaymentsD TABLE
	(
		PortfolioGlosaId INT, 
		InvoiceNumber VARCHAR(50),
		InvoiceDate DATETIME,
		RadicatedNumber VARCHAR(50),
		RadicatedDate DATETIME,
		PatientCode VARCHAR(15),
		PatientName VARCHAR(200),
		ContractCode VARCHAR(15),
		ValuePendingConciliation DECIMAL(18,0),
		ValuePayments DECIMAL(18,0)
	)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY
		--Se obtienen los datos de la cabecera
   		SELECT	@CustomerId = t.x.value('CustomerId[1]','int'),
				@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
				@ProcessDate = Common.[GETDATE](),
				@State = t.x.value('State[1]','char(1)'),
				@Comments = t.x.value('Comments[1]','varchar(250)'),
				@EntityId = t.x.value('EntityId[1]','int'),
				@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
				@EntityName = t.x.value('EntityName[1]','varchar(250)')
		FROM @PaymentsCXml.nodes('/PartialPaymentsC') t(x)

   		INSERT INTO @PartialPaymentsD
			SELECT	t.x.value('PortfolioGlosaId[1]','int') as Id,
					t.x.value('InvoiceNumber[1]','varchar(50)') as InvoiceNumber,
					t.x.value('InvoiceDate[1]','datetime') as InvoiceDate,
					t.x.value('RadicatedNumber[1]','varchar(50)') as RadicatedNumber,
					t.x.value('RadicatedDate[1]','datetime') as RadicatedDate,
					t.x.value('PatientCode[1]','varchar(15)') as PatientCode,
					t.x.value('PatientName[1]','varchar(200)') as PatientName,
					t.x.value('ContractCode[1]','varchar(15)') as ContractCode,
					t.x.value('ValuePendingConciliation[1]','decimal(18,0)') as ValuePendingConciliation,
					t.x.value('ValuePayments[1]','decimal(18,0)') as ValuePayments
			FROM @PaymentsCXml.nodes('/PartialPaymentsC/PartialPaymentsD') t(x)

		/***********************************************  VALIDACIONES ***********************************************/

		UPDATE Common.Consecutive 
			SET @Consecutive = NumberConsecutive = NumberConsecutive + 1 
		WHERE Code = '10'

		IF @Consecutive IS NULL
		BEGIN
			SELECT 999 AS CodeMessage, 'No existen consecutivos para la generación del pago parcial' AS Message
			RETURN
		END

		IF EXISTS (SELECT 1 FROM @PartialPaymentsD WHERE RadicatedNumber IS NULL)
		BEGIN
			SELECT @errors = STUFF((
				SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - La factura ', td.InvoiceNumber, ' no esta radicada')
				FROM @PartialPaymentsD td 
				WHERE td.RadicatedNumber IS NULL
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeMessage, @errors AS Message
			RETURN
		END

		IF EXISTS (
			SELECT 1 
			FROM 
			(
				SELECT InvoiceNumber
				FROM @PartialPaymentsD
				GROUP BY InvoiceNumber
			) ppd
			JOIN [Glosas].[GlosaPortfolioGlosada] gpg ON ppd.InvoiceNumber = gpg.InvoiceNumber
			WHERE gpg.BalanceGlosa <= 0
		) BEGIN
			SELECT @errors = STUFF((
				SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - La factura ', ppd.InvoiceNumber, ' no tiene saldo en acumulados de glosas')
				FROM 
				(
					SELECT InvoiceNumber
					FROM @PartialPaymentsD
					GROUP BY InvoiceNumber
				) ppd
				JOIN [Glosas].[GlosaPortfolioGlosada] gpg ON ppd.InvoiceNumber = gpg.InvoiceNumber
				WHERE gpg.BalanceGlosa <= 0
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeMessage, @errors AS Message
			RETURN
		END

		/************************************************** PROCESO **************************************************/
		
		INSERT INTO [Glosas].[PartialPaymentsC]
		(
			[RadicatedConsecutive],[CustomerId],[DocumentDate],[State],[Comment],[EntityId],[EntityCode],[EntityName],[ConfirmDateSystem],[ConfirmUser],[CreationUser],[CreationDate]
		)
		SELECT	@Consecutive,@CustomerId,@DocumentDate,@State,@Comments,@EntityId,@EntityCode,@EntityName,@ProcessDate,@CodeUser,@CodeUser,@ProcessDate

		SET @PartialPaymentCId = SCOPE_IDENTITY()

		---------------------------------------------------------------------------------------------------------------

		INSERT INTO [Glosas].[PartialPaymentsD]
		(
			[PartialPaymentsCId],[PortfolioGlosaId],[InvoiceNumber],[InvoiceDate],[RadicatedNumber],[RadicatedDate],[PatientCode],[PatientName],[ContractCode],[ValuePendingConciliation],[ValuePayments],[State]
		)
		SELECT @PartialPaymentCId,D.PortfolioGlosaId,D.InvoiceNumber,D.InvoiceDate,D.RadicatedNumber,D.RadicatedDate,D.PatientCode,D.PatientName,ISNULL(D.ContractCode,''),D.ValuePendingConciliation,D.ValuePayments,2 
		FROM @PartialPaymentsD as D

		---------------------------------------------------------------------------------------------------------------

		SET @PartialPaymentsDetailRows = 1
		SET @PartialPaymentsDetailId = 0

		WHILE @PartialPaymentsDetailRows > 0
		BEGIN
			SELECT TOP 1 
				@PartialPaymentsDetailId = ppd.Id,
				@InvoiceNumber = ppd.InvoiceNumber,
				@PaymentValue = ppd.ValuePayments,
				---------------------------------------------------------------
				@GlosaMovementGlosaRows = 1,
				@GlosaMovementGlosaId = 0
			FROM Glosas.PartialPaymentsD ppd 
			WHERE ppd.PartialPaymentsCId = @PartialPaymentCId 
				AND ppd.Id > @PartialPaymentsDetailId
			ORDER BY ppd.Id

			SET @PartialPaymentsDetailRows = @@ROWCOUNT
			IF @PartialPaymentsDetailRows = 0 
				BREAK

			-----------------------------------------------------------------------------------------------------------

			WHILE @GlosaMovementGlosaRows > 0 AND @PaymentValue > 0
			BEGIN
				SELECT TOP 1 
					@GlosaMovementGlosaId = gmg.Id,
					@Value = IIF(@PaymentValue > gmg.ValuePendingConciliation, gmg.ValuePendingConciliation, @PaymentValue)
				FROM Glosas.GlosaMovementGlosa gmg 
				WHERE gmg.InvoiceNumber = @InvoiceNumber
					AND gmg.MainGlosa = 1 AND gmg.ValuePendingConciliation > 0
					AND gmg.Id > @GlosaMovementGlosaId 
				ORDER BY gmg.Id

				SET @GlosaMovementGlosaRows = @@ROWCOUNT
				IF @GlosaMovementGlosaRows = 0
					BREAK

				-----------------------------------------------------------------------------------------------------------

				UPDATE Glosas.GlosaMovementGlosa 
					SET TempState = State, 
						State = 7, 
						ValuePayments = ISNULL(ValuePayments,0) + @Value, 
						ValuePendingConciliation = ValuePendingConciliation - @Value 
				WHERE Id = @GlosaMovementGlosaId

				INSERT INTO [Glosas].[PartialPaymentsMovement]
				(
					[PartialPaymentsDId],[GlosaMovementGlosaId],[ValueEAPB],[CreationUser],[CreationDate]
				)
				SELECT	@PartialPaymentsDetailId,@GlosaMovementGlosaId,@Value,@CodeUser,@ProcessDate

				SET @PaymentValue = @PaymentValue - @Value
			END

			---------------------------------------------------------------------------------------------------------------

			UPDATE gpg
				SET gpg.TempState = gpg.State, 
					gpg.State = 14, 
					gpg.ValuePayments = ISNULL(gmg.ValuePayments, 0), 
					gpg.BalanceGlosa = ISNULL(gmg.BalanceGlosa, 0)  
			FROM Glosas.GlosaPortfolioGlosada gpg
			LEFT JOIN
			(
				SELECT gmg.InvoiceNumber, SUM(ISNULL(gmg.ValuePayments, 0)) ValuePayments, SUM(ISNULL(gmg.ValuePendingConciliation, 0)) BalanceGlosa
				FROM Glosas.GlosaMovementGlosa gmg 
				WHERE gmg.InvoiceNumber = @InvoiceNumber AND MainGlosa = 1
				GROUP BY gmg.InvoiceNumber
			) gmg ON gpg.InvoiceNumber = gmg.InvoiceNumber
			WHERE gpg.InvoiceNumber = @Invoicenumber
		END

		---------------------------------------------------------------------------------------------------------------

		SELECT '0' AS CodeMessage, CONCAT('Se genero el pago parcial ', @Consecutive) AS Message
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, CONCAT('Error generando pago parcial: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE()) AS Message
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera y registra pagos parciales sobre glosas de cartera radicadas ante aseguradoras (EPS/EAPB). Recibe un XML con la cabecera del pago (entidad pagadora, fecha, estado, comentarios) y el detalle de facturas a abonar, valida que cada factura esté radicada y tenga saldo de glosa pendiente en la cartera glosada, asigna un número consecutivo del sistema (código 10) y persiste tanto la cabecera del pago en PartialPaymentsC como cada línea de factura en PartialPaymentsD. Es el punto de entrada para registrar abonos parciales que las entidades pagadoras realizan sobre deudas de glosa, afectando el balance acumulado de glosas por factura.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePartialPayments';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePartialPayments';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un pago parcial sobre facturas glosadas, distribuyendo el valor pagado entre los movimientos de glosa principales pendientes de conciliación y actualizando saldos de cartera glosada.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePartialPayments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un consecutivo activo con Code=''10'' en Common.Consecutive; si no existe, retorna error 999.; Todas las facturas del detalle XML deben tener RadicatedNumber (estar radicadas).; Las facturas del detalle deben tener saldo positivo (BalanceGlosa > 0) en GlosaPortfolioGlosada.; El XML de entrada debe contener una cabecera /PartialPaymentsC y al menos un nodo /PartialPaymentsD.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePartialPayments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El consecutivo del pago parcial siempre proviene de Common.Consecutive Code=''10'' incrementado atómicamente.; Solo se afectan movimientos de glosa marcados como MainGlosa=1 y con ValuePendingConciliation>0.; Antes de cambiar State en GlosaMovementGlosa y GlosaPortfolioGlosada, se preserva el estado anterior en TempState.; GlosaMovementGlosa pagados quedan en State=7; GlosaPortfolioGlosada procesado queda en State=14; PartialPaymentsD se inserta con State=2.; El total aplicado a una factura nunca excede el ValuePayments declarado ni la suma de ValuePendingConciliation de sus movimientos principales.; BalanceGlosa y ValuePayments de GlosaPortfolioGlosada se reconcilian con la suma de los movimientos principales de la factura tras el pago.; Cualquier error en el TRY produce un único result-set con CodeMessage=999.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePartialPayments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Pago parcial; Glosa; Factura radicada; Cartera glosada; Conciliación; Saldo de glosa; EAPB; Paciente; Contrato; Consecutivo', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePartialPayments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Common.Consecutive: Incrementa NumberConsecutive en 1 donde Code=''10'' y captura el nuevo valor como consecutivo del pago parcial.; [INSERT] Glosas.PartialPaymentsC: Tras validaciones exitosas, inserta la cabecera del pago parcial con el consecutivo obtenido, datos de la entidad y usuario/fecha de creación y confirmación.; [INSERT] Glosas.PartialPaymentsD: Inserta cada detalle del XML asociado al PartialPaymentsCId recién creado, asignando State=2 y ContractCode='''' cuando viene NULL.; [UPDATE] Glosas.GlosaMovementGlosa: Por cada movimiento con MainGlosa=1 y ValuePendingConciliation>0 de la factura, mueve State a TempState, fija State=7, suma @Value a ValuePayments y resta @Value a ValuePendingConciliation, hasta agotar el ValuePayments del detalle.; [INSERT] Glosas.PartialPaymentsMovement: Por cada aplicación de pago a un movimiento de glosa, inserta un registro con el detalle, el GlosaMovementGlosaId afectado y el valor aplicado (ValueEAPB).; [UPDATE] Glosas.GlosaPortfolioGlosada: Tras procesar la factura, mueve State a TempState, fija State=14 y recalcula ValuePayments y BalanceGlosa con la suma de los movimientos MainGlosa=1 de esa factura.; [RETURN_RESULT] @PartialPaymentsD: Si RadicatedNumber es NULL en alguna factura, retorna CodeMessage=999 con lista concatenada ''La factura X no esta radicada''.; [RETURN_RESULT] Glosas.GlosaPortfolioGlosada: Si alguna factura tiene BalanceGlosa<=0, retorna CodeMessage=999 con lista ''La factura X no tiene saldo en acumulados de glosas''.; [RETURN_RESULT] Glosas.PartialPaymentsC: En éxito retorna CodeMessage=''0'' y mensaje ''Se genero el pago parcial <consecutivo>''; ante excepción retorna CodeMessage=999 con ERROR_MESSAGE y línea.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePartialPayments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Consecutive IS NULL tras intentar incrementar Common.Consecutive Code=''10'' → Retorna error 999 ''No existen consecutivos para la generación del pago parcial'' y termina.; si Existe al menos un detalle con RadicatedNumber NULL → Retorna error 999 listando facturas no radicadas y termina.; si Existe alguna factura del detalle cuyo GlosaPortfolioGlosada.BalanceGlosa <= 0 → Retorna error 999 listando facturas sin saldo y termina.; si Por cada detalle, mientras existan movimientos MainGlosa=1 con ValuePendingConciliation>0 y @PaymentValue>0 → Aplica @Value = MIN(PaymentValue, ValuePendingConciliation), actualiza el movimiento, registra PartialPaymentsMovement y reduce el saldo a pagar.; si @PaymentValue > ValuePendingConciliation del movimiento → Se aplica solo ValuePendingConciliation (consume el movimiento completo) else Se aplica el @PaymentValue restante (consume parcialmente el movimiento)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePartialPayments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePartialPayments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.Consecutive; Glosas.GlosaPortfolioGlosada; Glosas.PartialPaymentsD; Glosas.GlosaMovementGlosa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePartialPayments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePartialPayments';
-- GO
