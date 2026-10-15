-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_GeneratePortfolioReclasification]
	@OperatingUnitId INT,
	@IdRadicateInvoice INT,
	@CodeUser VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @CodeRadicate VARCHAR(200),
			@RadicateDate DATETIME,
			@Status CHAR(1),
			@ConfirmDate DATETIME,
			------------------------------
			@Message VARCHAR(MAX)	
	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY

		SELECT 
			@CodeRadicate = RadicatedConsecutive, 
			@RadicateDate = DocumentDate,
			@Status = State,
			@ConfirmDate = ISNULL(ConfirmDate, DocumentDate)
		FROM Portfolio.RadicateInvoiceC 
		WHERE Id = @IdRadicateInvoice
		
		/************************************************ VALIDACIONES ***********************************************/
				
		IF @Status <> '1'
		BEGIN
			SELECT	'999' CodeMessage, 
					'La radicación se encuentra en estado ' + CASE @Status  
																WHEN '2' THEN 'Confirmado'
																WHEN '4' THEN 'Anulado'
																ELSE 'N/A'
															END MessageResult
			RETURN
		END

		IF EXISTS 
		(
			SELECT 1 
			FROM Portfolio.RadicateInvoiceD rid
			LEFT JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber
			WHERE RadicateInvoiceCId = @IdRadicateInvoice AND ar.Id IS NULL
		)
		BEGIN				
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + rid.InvoiceNumber
						FROM Portfolio.RadicateInvoiceD rid
						LEFT JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber
						WHERE RadicateInvoiceCId = @IdRadicateInvoice AND ar.Id IS NULL
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT '999' CodeMessage, 'No se encontró cuenta por cobrar para las facturas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') MessageResult
			RETURN
		END

		IF EXISTS 
		(
			SELECT 1 
			FROM Portfolio.RadicateInvoiceD 
			WHERE RadicateInvoiceCId = @IdRadicateInvoice 
			GROUP BY InvoiceNumber 
			HAVING COUNT(*) > 1
		)
		BEGIN 
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + InvoiceNumber
						FROM Portfolio.RadicateInvoiceD 
						WHERE RadicateInvoiceCId = @IdRadicateInvoice 
						GROUP BY InvoiceNumber HAVING COUNT(*) > 1
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
			SELECT '999' CodeMessage, 'No se logro guardar el radicado debido a que las siguientes facturas estan repetidas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') MessageResult
			RETURN
		END

		IF EXISTS 
		( 
			SELECT 1
			FROM Portfolio.RadicateInvoiceD rid 
			JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
			LEFT JOIN Portfolio.AccountReceivableAccounting ara ON ar.Id = ara.AccountReceivableId AND ar.AccountWithoutRadicateId = ara.MainAccountId
			WHERE rid.RadicateInvoiceCId = @IdRadicateInvoice AND ara.Id IS NULL
		)
		BEGIN			
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + rid.InvoiceNumber
						FROM Portfolio.RadicateInvoiceD rid 
						JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
						LEFT JOIN Portfolio.AccountReceivableAccounting ara ON ar.Id = ara.AccountReceivableId AND ar.AccountWithoutRadicateId = ara.MainAccountId
						WHERE rid.RadicateInvoiceCId = @IdRadicateInvoice AND ara.Id IS NULL
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
			SELECT '999' CodeMessage, 'No se encontro saldo en la cuenta sin radicar para las facturas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') MessageResult
			RETURN
		END

		IF EXISTS 
		( 
			SELECT 1
			FROM Portfolio.RadicateInvoiceD rid 
			JOIN
			(
				SELECT rid.InvoiceNumber
				FROM Portfolio.RadicateInvoiceC ri 
				JOIN Portfolio.RadicateInvoiceD rid ON ri.Id = rid.RadicateInvoiceCId
				WHERE ri.Id <> @IdRadicateInvoice AND ri.State = '2'
			) d ON rid.InvoiceNumber = d.InvoiceNumber
			WHERE rid.RadicateInvoiceCId = @IdRadicateInvoice AND rid.Devolution = 0
		)
		BEGIN			
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + rid.InvoiceNumber
						FROM Portfolio.RadicateInvoiceD rid 
						JOIN
						(
							SELECT rid.InvoiceNumber
							FROM Portfolio.RadicateInvoiceC ri 
							JOIN Portfolio.RadicateInvoiceD rid ON ri.Id = rid.RadicateInvoiceCId
							WHERE ri.Id <> @IdRadicateInvoice AND ri.State = '2'
						) d ON rid.InvoiceNumber = d.InvoiceNumber
						WHERE rid.RadicateInvoiceCId = @IdRadicateInvoice AND rid.Devolution = 0
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
			SELECT '999' CodeMessage, 'Las siguientes facturas ya se encuentran radicadas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') MessageResult
			RETURN
		END

		/***************************************** RECLASIFICAR LAS FACTURAS *****************************************/

		-- Se actualiza el saldo en el detalle de la radicación por el saldo actual de la factura
		UPDATE rid 
			SET rid.BalanceInvoice = ar.Balance
		FROM Portfolio.RadicateInvoiceD rid 
		JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
		WHERE rid.RadicateInvoiceCId = @IdRadicateInvoice

		-- Se actualiza el movimiento por notas del detalle de la radicación
		UPDATE rid 
			SET rid.CreditNoteValue = ars.CreditValue,
				rid.DebitNoteValue = ars.DebitValue
		FROM Portfolio.RadicateInvoiceD rid 
		JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
		JOIN Portfolio.AccountReceivableShare ars on ar.Id = ars.AccountReceivableId
		WHERE rid.RadicateInvoiceCId = @IdRadicateInvoice

		SELECT @Message = STUFF((
					SELECT ', ' + rid.InvoiceNumber
					FROM Portfolio.RadicateInvoiceD rid
					JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
					WHERE RadicateInvoiceCId = @IdRadicateInvoice
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

		
		DECLARE @CurrencyId INT
		DECLARE @MessageTabla  TABLE (	code INT,
										MessageOut VARCHAR(max))
		DECLARE currency_Cursor CURSOR FOR
			SELECT	ar.CurrencyId
			FROM Portfolio.RadicateInvoiceD rid
			JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
			WHERE rid.RadicateInvoiceCId = @IdRadicateInvoice AND rid.BalanceInvoice <> 0
			GROUP by ar.CurrencyId
		
		OPEN currency_Cursor
		FETCH NEXT FROM currency_Cursor
		INTO @CurrencyId
		WHILE @@FETCH_STATUS = 0  
		BEGIN 
			
		------------------------------
		DECLARE	@SubXml XML = NULL,
				@Code_Output INT = NULL,
				@Message_Output VARCHAR(MAX) = NULL
			
			SELECT @SubXml = CONVERT
					(
						XML, 
						(
							SELECT *
							FROM 
							(
								SELECT	0 Id,
										@OperatingUnitId OperatingUnitId,
										'' Code,
										@ConfirmDate DocumentDate,
										1 DocumentType,
										'Radicacion de Cuentas No. ' + @CodeRadicate + ' Facturas: ' + @Message Detail,
										2 Status,
										ri.Id EntityId,
										ri.RadicatedConsecutive EntityCode,
										'RadicateInvoiceC' EntityName,
										@CurrencyId as CurrencyId
								FROM Portfolio.RadicateInvoiceC ri
								WHERE ri.Id = @IdRadicateInvoice
							) PortfolioReclassification
							JOIN
							(
								SELECT	0 PortfolioReclassificationId,
										0 Id,
										ar.Id AccountReceivableId,
										ar.AccountWithoutRadicateId SourceAccountId,
										ar.AccountRadicateId TargetAccountId,
										rid.BalanceInvoice Value
								FROM Portfolio.RadicateInvoiceD rid
								JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
								WHERE rid.RadicateInvoiceCId = @IdRadicateInvoice AND rid.BalanceInvoice <> 0 and ar.CurrencyId = @CurrencyId
							) PortfolioReclassificationDetail ON PortfolioReclassification.Id = PortfolioReclassificationDetail.PortfolioReclassificationId
							For xml AUTO,TYPE, ELEMENTS
						)
					)

			EXEC [Portfolio].[SP_SavePortfolioReclassification_Output] @SubXml, @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL

			IF @Code_Output <> 0
			BEGIN
				SELECT '999' CodeMessage, ISNULL(@Message_Output, 'No se pudo generar la Reclasificación de Documentos de Cartera') MessageResult
				RETURN
			END
			
			INSERT into @MessageTabla VALUES(@Code_Output,@Message_Output)

		NEXT_ROW:
		FETCH NEXT FROM currency_Cursor   
		INTO @CurrencyId
		END
		CLOSE currency_Cursor;  
		DEALLOCATE currency_Cursor;
		
		SET @Message = 'Se guardó y confirmó la radicación de cuentas con consecutivo ' + @CodeRadicate

		SELECT @Message =  @Message + CHAR(13) + CHAR(10) + ISNULL(STRING_AGG(MessageOut,';'), '')
		FROM @MessageTabla

		/**************************************** ACTUALIZACION DE REGISTROS *****************************************/
		
		UPDATE ar
			SET PortfolioStatus = 3
		FROM Portfolio.RadicateInvoiceD rid
		JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
		WHERE rid.RadicateInvoiceCId = @IdRadicateInvoice

		UPDATE rid 
			SET State = 2, 
				RadicatedDate = [Common].[GETDATE](), 
				RadicatedNumber = @CodeRadicate 
		FROM Portfolio.RadicateInvoiceD rid
		WHERE RadicateInvoiceCId = @IdRadicateInvoice

		DECLARE @UserId INT

		SELECT @UserId = u.Id
		FROM Security.[User] u 
		WHERE u.UserCode = @CodeUser

		UPDATE ri
			SET ri.State = '2',
				ri.ConfirmDateSystem = [Common].[GETDATE](),
				ri.ConfirmUser = @UserId,
				ri.ModificationUser = @CodeUser,
				ri.ConfirmDate = @ConfirmDate
		FROM Portfolio.RadicateInvoiceC ri
		WHERE ri.Id = @IdRadicateInvoice

		/************************************************* RESULTADO *************************************************/

		SELECT	'0' CodeMessage, 
				CHAR(13) + CHAR(10) + ISNULL(@Message, '') MessageResult

	END TRY
	BEGIN CATCH

		IF CURSOR_STATUS('global','currency_Cursor') >= -1  BEGIN
		  IF CURSOR_STATUS('global','currency_Cursor') > -1 BEGIN
			CLOSE currency_Cursor
		  END
		 DEALLOCATE currency_Cursor
		END

		SELECT '999' CodeMessage, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50)) MessageResult
	END CATCH
END