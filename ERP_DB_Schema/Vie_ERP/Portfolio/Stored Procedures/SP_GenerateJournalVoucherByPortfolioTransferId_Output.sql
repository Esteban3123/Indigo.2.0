-- =====================================================================================
-- Author: Miguel Angel Fonseca Castro
-- Create date: 2019-10-09
-- Description:	Procedimiento que se encarga de generar el reconocimiento a partir de una cuenta por cobrar
-- =====================================================================================
CREATE PROCEDURE [Portfolio].[SP_GenerateJournalVoucherByPortfolioTransferId_Output]
	@PortfolioTransferId INT,
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Code_Output INT,
			@Message_Output VARCHAR(MAX)

	BEGIN TRY
	/******************************** Se ejecuta el OVERLOAD del SP *******************************/

					EXEC [Portfolio].[SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad] @PortfolioTransferId, @CodeUser,'', @Code_Output OUT, @Message_Output OUT

	/************************************************* RESULTADO *************************************************/
					
					SELECT	@CodeResult = @Code_Output,
							@MessageResult =ISNULL(@Message_Output, '')
					RETURN	
	END TRY
	BEGIN CATCH	
		SELECT	@CodeResult = 999,
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50))
	END CATCH
END