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
			JOIN [Glosas].[GlosaPortfolioGlosada] gpg WITH (NOLOCK) ON ppd.InvoiceNumber = gpg.InvoiceNumber
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
				JOIN [Glosas].[GlosaPortfolioGlosada] gpg WITH (NOLOCK) ON ppd.InvoiceNumber = gpg.InvoiceNumber
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