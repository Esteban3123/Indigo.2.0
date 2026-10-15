-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-15
-- Description:	Procedimiento que se encarga de reversar el cruce de anticipo vs cxc
-- Update : Giovanny Plazas
-- Modification Date: 2024-06-24
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ReversePortfolioTransferXML_Output]
	@XmlParameters XML,
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@Id_Output INT,
			-----------------------------------------------------
			@PortfolioNoteId INT,
			@CodeUser VARCHAR(20),
			------------------------------------------------------
			@CompanyType TINYINT,
			@EntityName VARCHAR(100)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY

		
        SELECT	@PortfolioNoteId = t.x.value('Id[1]', 'INT'),
				@CodeUser = t.x.value('CodeUser[1]', 'VARCHAR(20)'),
				@CompanyType =  t.x.value('CompanyType[1]', 'TINYINT'),
				@EntityName =  t.x.value('EntityName[1]', 'VARCHAR(100)')
        FROM @XmlParameters.nodes('/PortfolioNote') t(x);

		SELECT @SubXml = CONVERT
		(
			XML, 
			(
				SELECT 
					PortfolioTransfer.*
				FROM 
				(
					SELECT 
						pn.PortfolioTransferId Id,
						pt.Code,
						pn.OperatingUnitId,
						pn.NoteDate DocumentDate,
						4 Status,
						pn.Id PortfolioNoteId,
						IIF(@EntityName='Invoice',@EntityName,'') EntityName,
						pa.CurrencyId
					FROM Portfolio.PortfolioNote pn
					LEFT JOIN Portfolio.PortfolioTransfer pt WITH(NOLOCK) ON pn.PortfolioTransferId = pt.Id
					LEFT JOIN Portfolio.PortfolioAdvance pa WITH(NOLOCK) on pt.PortfolioAdvanceId=pa.Id
					WHERE pn.Id = @PortfolioNoteId
				) PortfolioTransfer
				For xml AUTO,TYPE, ELEMENTS
			)
		)

		EXEC Portfolio.SP_SavePortfolioTransfer_Output @SubXml, @CodeUser, @CompanyType, @Code_Output OUT, @Message_Output OUT, NULL, NULL

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