-- =============================================
-- Author:		Johan Sebastian Cuellar Esquivel
-- Create date: 2021-04-13
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar un cargue de extracto bancario
-- =============================================
CREATE PROCEDURE [Treasury].[SP_SaveUploadBankStatements]
    @UploadBankStatementsXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeMessage INT,
			@Message VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Treasury].[SP_SaveUploadBankStatements_Output] @UploadBankStatementsXml, @CodeUser, @CodeMessage OUTPUT, @Message OUTPUT, @Id OUTPUT, @Code OUTPUT

	SELECT 
		CAST(@CodeMessage AS VARCHAR(20)) AS CodeMessage, 
		@Message AS Message, 
		@Id as UploadBankStatementsId, 
		--@Code as Code
		CAST(0 AS TINYINT) Status
END