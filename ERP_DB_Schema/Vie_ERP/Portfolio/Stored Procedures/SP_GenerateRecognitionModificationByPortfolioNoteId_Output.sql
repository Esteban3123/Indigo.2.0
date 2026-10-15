-- =====================================================================================
-- Author: Miguel Angel Fonseca Castro
-- Create date: 2019-10-16
-- Description:	Procedimiento que se encarga de generar el recaudo a partir de un cruce de anticipos
-- =====================================================================================
CREATE PROCEDURE [Portfolio].[SP_GenerateRecognitionModificationByPortfolioNoteId_Output]
	@PortfolioNoteId INT,
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @OperatingUnitId INT,
			@PortfolioNoteCode VARCHAR(20),
			@PortfolioNoteDate DATETIME,
			@Nature TINYINT,
			------------------------------
			@BudgetaryValidityId INT,
			@RecognitionRows INT,
			@RecognitionId INT,
			@AdjusmentValue DECIMAL(18,2),
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY

		SELECT	@OperatingUnitId = pn.OperatingUnitId,
				@PortfolioNoteCode = pn.Code,
				@PortfolioNoteDate = pn.NoteDate,
				@Nature = IIF(pn.Nature = 1, 2, 1)
		FROM Portfolio.PortfolioNote pn
		WHERE pn.Id = @PortfolioNoteId
		
		/*********************************  MODIFICACION RECONOCIMIENTO PRESUPUESTAL *********************************/
		
		SET @RecognitionRows = 1
		SET @RecognitionId = 0

		WHILE @RecognitionRows > 0
		BEGIN
			SELECT TOP 1
				@RecognitionId = r.Id,
				@BudgetaryValidityId = r.BudgetaryValidityId,
				@AdjusmentValue =  SUM(pnara.AdjusmentValue - ISNULL(i.ValueTax, 0))
			FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara
			JOIN Portfolio.AccountReceivable ar ON pnara.AccountReceivableId = ar.Id
			JOIN Budget.Recognition r ON ar.RecognitionId = r.Id
			JOIN Budget.BudgetaryValidity bv ON r.BudgetaryValidityId = bv.Id AND YEAR(ar.AccountReceivableDate) = bv.Year AND bv.Status IN (1, 2)
			LEFT JOIN Billing.Invoice i WITH (NOLOCK) ON i.InvoiceNumber = ar.InvoiceNumber
			WHERE pnara.PortfolioNoteId = @PortfolioNoteId
				AND r.Id > @RecognitionId
			GROUP BY r.Id, r.BudgetaryValidityId
			ORDER BY r.Id

			SET @RecognitionRows = @@ROWCOUNT
			IF @RecognitionRows = 0 
			BEGIN
				BREAK
			END

			SELECT @SubXml = CONVERT
			(
				XML, 
				(
					SELECT 
						RecognitionModification.*,
						RecognitionModificationDetail.*
					FROM 
					(
						SELECT 
							0 Id,						
							'' Code,
							@OperatingUnitId OperatingUnitId,
							@BudgetaryValidityId BudgetaryValidityId,
							@PortfolioNoteDate DocumentDate,
							@RecognitionId RecognitionId,
							@PortfolioNoteCode Document,
							'Modificación generada desde la Nota de Cartera No. ' + @PortfolioNoteCode Observations,
							2 Status,
							@PortfolioNoteId EntityId,
							@PortfolioNoteCode EntityCode,
							'PortfolioNote' EntityName
					) RecognitionModification
					JOIN
					( 
						SELECT
							0 RecognitionModificationId,
							rd.Id RecognitionDetailId,
							rd.CategoryId,
							rd.RevenueTypeId,
							@Nature Nature,
							@AdjusmentValue Value
						FROM Budget.RecognitionDetail rd
						WHERE rd.RecognitionId = @RecognitionId
					) RecognitionModificationDetail ON RecognitionModification.Id = RecognitionModificationDetail.RecognitionModificationId
					FOR XML AUTO,TYPE, ELEMENTS
				)
			)

			EXEC [Budget].[SP_SaveRecognitionModification_Output] @SubXml, '', @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL, NULL

			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = ISNULL(@Message_Output, 'No se pudo generar la modificación del reconocimiento presupuestal')
				RETURN
			END

			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		/************************************************* RESULTADO *************************************************/

		SELECT	@CodeResult = 0,
				@MessageResult = ISNULL(@Message, '')
	END TRY
	BEGIN CATCH	
		SELECT	@CodeResult = 999,
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50))
	END CATCH
END