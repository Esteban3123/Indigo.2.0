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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que gestiona el cargue de extractos bancarios: permite guardar, actualizar y confirmar los movimientos importados desde un archivo bancario enviado como XML. Recibe el contenido del extracto bancario y el código del usuario que realiza la operación, delegando la lógica principal al procedimiento SP_SaveUploadBankStatements_Output y retornando el resultado del proceso con un código de mensaje, descripción, identificador del cargue y un estado de respuesta. Es el punto de entrada del módulo de tesorería para la conciliación y registro de extractos bancarios.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveUploadBankStatements';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveUploadBankStatements';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega el guardado/actualización/confirmación de un cargue de extracto bancario y devuelve el resultado en un único result set.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveUploadBankStatements';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un XML con la información del cargue de extracto bancario; Se debe proveer el código de usuario que ejecuta la operación', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveUploadBankStatements';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El campo Status retornado siempre es 0 (TINYINT); El CodeMessage se retorna casteado a VARCHAR(20); El campo Code calculado por el SP interno no se expone en el result set (está comentado)', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveUploadBankStatements';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cargue de extracto bancario; Tesorería', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveUploadBankStatements';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un result set con CodeMessage, Message, UploadBankStatementsId y Status=0 tras invocar el SP de salida', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveUploadBankStatements';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Treasury.SP_SaveUploadBankStatements_Output', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveUploadBankStatements';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveUploadBankStatements';
-- GO
