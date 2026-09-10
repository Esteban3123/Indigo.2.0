-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-11
-- Description:	Procedimiento el cual se encarga de guardar la modificación de un Reconocimiento
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveRecognitionModification]
	@RecognitionModificationXml AS XML,
	@RecognitionModificationDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Budget].[SP_SaveRecognitionModification_Output] @RecognitionModificationXml, @RecognitionModificationDetailForDeleteXml, @CodeUser, @CodeResult OUTPUT, @MessageResult OUTPUT, @Id OUTPUT, @Code OUTPUT

	SELECT 
		@CodeResult AS CodeResult, 
		@MessageResult AS MessageResult, 
		@Id as Id, 
		@Code as Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la modificación de un reconocimiento presupuestal, actualizando o eliminando los detalles indicados según los datos enviados en formato XML. Recibe el encabezado de la modificación, los detalles a eliminar y el usuario que realiza el cambio, delegando la lógica principal al procedimiento SP_SaveRecognitionModification_Output. Retorna un código de resultado, un mensaje de estado, el identificador y el código del registro afectado, permitiendo al sistema confirmar si la operación fue exitosa.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveRecognitionModification';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveRecognitionModification';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega el guardado de la modificación de un Reconocimiento al procedimiento interno y devuelve el resultado como conjunto de filas.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRecognitionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se deben suministrar los XML de la modificación del Reconocimiento y de los detalles a eliminar, junto con el código de usuario que ejecuta la acción.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRecognitionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La persistencia real (INSERT/UPDATE/DELETE) no la realiza este SP; se delega íntegramente en SP_SaveRecognitionModification_Output.; El resultado siempre se expone como un único resultset con columnas CodeResult, MessageResult, Id y Code.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRecognitionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reconocimiento (Budget Recognition); Modificación de Reconocimiento; Detalle de Reconocimiento', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRecognitionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Tras invocar SP_SaveRecognitionModification_Output, retorna un SELECT con CodeResult, MessageResult, Id y Code obtenidos de los parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRecognitionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveRecognitionModification_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRecognitionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRecognitionModification';
-- GO
