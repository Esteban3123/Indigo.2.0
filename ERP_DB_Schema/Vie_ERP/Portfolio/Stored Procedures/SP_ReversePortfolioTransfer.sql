-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-15
-- Description:	Procedimiento que se encarga de reversar el cruce de anticipo vs cxc
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ReversePortfolioTransfer]
	@PortfolioNoteId INT,
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CompanyType TINYINT,
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@Id_Output INT

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY
		SELECT @SubXml = CONVERT
		(
			XML, 
			(
				SELECT 
					PortfolioNote.*
				FROM 
				(
					SELECT 
						pn.Id,
						@CodeUser as CodeUser,
						@CompanyType as CompanyType,
						'' as EntityName
					FROM Portfolio.PortfolioNote pn
					WHERE pn.Id = @PortfolioNoteId
				) PortfolioNote
				For xml AUTO,TYPE, ELEMENTS
			)
		)

		EXEC Portfolio.SP_ReversePortfolioTransferXML_Output @SubXml, @Code_Output OUT, @Message_Output OUT

		IF @Code_Output <> 0
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = ISNULL(@Message_Output, 'No se pudo reversar los cruces de anticipo asociados a la factura')
			RETURN
		END

		SELECT	@CodeResult = 0,
				@MessageResult = ISNULL(@Message_Output, '')
	END TRY
	BEGIN CATCH
		SELECT	@CodeResult = 999,
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20))
	END CATCH
END