-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-03-19
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar un documento de cuenta por cobrar
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_SaveAccountReceivableDocument]
    @AccountReceivableDocumentXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC  [Portfolio].[SP_SaveAccountReceivableDocument_Output]
		@AccountReceivableDocumentXml, 
		@CodeUser, 
		--------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT, 
		--------------------------------------------
		@Id OUTPUT, 
		@Code OUTPUT

	SELECT 
		@CodeResult AS CodeResult, 
		@MessageResult AS MessageResult, 
		@Id as Id, 
		@Code as Code
END