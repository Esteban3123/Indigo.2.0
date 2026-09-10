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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda, actualiza o confirma un documento de cuenta por cobrar en el módulo de cartera (Portfolio). Recibe la información del documento en formato XML junto con el código del usuario que realiza la operación, y delega el procesamiento real al procedimiento interno SP_SaveAccountReceivableDocument_Output, del cual obtiene el resultado de la operación (código de resultado, mensaje, identificador y código del documento generado). Retorna al llamador el estado de la transacción y los datos del documento afectado, sirviendo como punto de entrada estándar para la gestión de documentos de cobro a aseguradoras o pagadores.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAccountReceivableDocument';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAccountReceivableDocument';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega en el procedimiento _Output el guardado, actualización o confirmación de un documento de cuenta por cobrar y devuelve como result set el resultado, mensaje, Id y código generados.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAccountReceivableDocument';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe recibir el XML con la información del documento de cuenta por cobrar y el código del usuario que ejecuta la acción.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAccountReceivableDocument';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado de la operación (código, mensaje, identificador y código del documento) siempre se entrega como un único result set al cliente.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAccountReceivableDocument';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Documento de cuenta por cobrar; Cartera', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAccountReceivableDocument';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras invocar SP_SaveAccountReceivableDocument_Output se retorna un SELECT con CodeResult, MessageResult, Id y Code obtenidos como parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAccountReceivableDocument';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.SP_SaveAccountReceivableDocument_Output', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAccountReceivableDocument';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAccountReceivableDocument';
-- GO
