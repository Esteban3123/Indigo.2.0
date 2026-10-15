CREATE PROCEDURE [Billing].[SP_ReversePortfolioByBasicBillingId_Output]
	@OperatingUnitId INT,
	@BasicBillingId INT,
	@AnnulmentDate DATETIME,
	@ReversalReasonDescription VARCHAR(300),
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CompanyType TINYINT,
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--SELECT	@CodeResult = 0,
	--			@MessageResult = ''
	--			return
	/*************************************************** VARIABLES ***************************************************/

	DECLARE @InvoiceId Int,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@Id_Output INT

	/*****************************************************************************************************************/

	BEGIN TRY

		/*************************************************************************************************************/
		SELECT TOP 1 @InvoiceId = InvoiceId
		FROM Billing.BasicBilling WHERE Id = @BasicBillingId

		DECLARE @AccountReceivableRows INT = 1,
				@AccountReceivableId INT = 2147483647,
				@AccountReceivableCode VARCHAR(20),
				@AccountReceivableType TINYINT,
				-------------------------------
				@PortfolioTransferRows INT,
				@PortfolioTransferId INT

		WHILE @AccountReceivableRows > 0
		BEGIN
			SELECT TOP 1
				@AccountReceivableId = ar.Id,
				@AccountReceivableCode = ar.Code,
				@AccountReceivableType = ar.AccountReceivableType,
				-----------------------------
				@PortfolioTransferRows = 1,
				@PortfolioTransferId = 0
			FROM Portfolio.AccountReceivable ar
			WHERE ar.InvoiceId = @InvoiceId
				AND ar.Id < @AccountReceivableId
			ORDER BY ar.Id DESC

			SET @AccountReceivableRows = @@ROWCOUNT
			IF @AccountReceivableRows = 0 
			BEGIN
				BREAK
			END
			
			/*********************************************************************************************************/

			SET @Message_Output = CONCAT('Se reversó la Cuenta por Cobrar ', CASE @AccountReceivableType
					WHEN 4 THEN 'tipo Pagaré: '
					WHEN 6 THEN 'al Paciente: '
				END,  @AccountReceivableCode)
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)

			/*********************************************************************************************************/

			WHILE @PortfolioTransferRows > 0
			BEGIN
					SELECT TOP 1
						@PortfolioTransferId = pt.Id
					FROM Portfolio.AccountReceivable ar
					JOIN Portfolio.PortfolioTransferDetail ptd ON ar.Id = ptd.AccountReceivableId
					JOIN Portfolio.PortfolioTransfer pt ON ptd.PortfolioTrasferId = pt.Id
					WHERE ar.Id = @AccountReceivableId AND pt.Status = 2
						AND pt.Id > @PortfolioTransferId
					ORDER BY pt.Id
					
					SET @PortfolioTransferRows = @@ROWCOUNT
					IF @PortfolioTransferRows = 0 
					BEGIN
						BREAK
					END

					/*************************************************************************************************/

					SELECT @SubXml = CONVERT
					(
						XML, 
						(
							SELECT 
								PortfolioNote.*
							FROM 
							(
								SELECT 
									0 Id,
									'' Code,
									@AnnulmentDate NoteDate,
									'Reversión del cruce de Anticipo vs CxC: ' + pt.Code + ' por: ' + @ReversalReasonDescription Observations,
									1 Nature,
									5 NoteType,
									pt.OperatingUnitId OperatingUnitId,
									2 Status,
									@AnnulmentDate CreationDate,
									pt.Id PortfolioTransferId,
									'Added' ChangeTracker,
									ad.CurrencyId
								FROM Portfolio.PortfolioTransfer pt WITH(NOLOCK)
								JOIN Portfolio.PortfolioAdvance ad WITH(NOLOCK) on ad.Id =pt.PortfolioAdvanceId
								WHERE pt.Id = @PortfolioTransferId
							) PortfolioNote
							For xml AUTO,TYPE, ELEMENTS
						)
					)

					EXEC Portfolio.SP_SavePortfolioNote_Output @SubXml, @CodeUser, @CompanyType, @Code_Output OUT, @Message_Output OUT, NULL, NULL

					IF @Code_Output <> 0
					BEGIN
						SELECT	@CodeResult = 999,
								@MessageResult = ISNULL(@Message_Output, 'No se pudo reversar los cruces de anticipo asociados a la factura')
						RETURN
					END

					SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
				END
		END
		
		/************************************************* RESULTADO *************************************************/

		SELECT	@CodeResult = 0,
				@MessageResult = ISNULL(@Message, '')
	END TRY
	BEGIN CATCH
		SELECT	@CodeResult = 999,
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20))
	END CATCH
END