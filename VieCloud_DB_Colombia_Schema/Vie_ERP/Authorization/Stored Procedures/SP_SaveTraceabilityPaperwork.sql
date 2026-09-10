-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 02/06/2020
-- Description:	Procedimiento que se encarga de guardar la trazabilidad de tramites
-- =============================================
CREATE PROCEDURE [Authorization].[SP_SaveTraceabilityPaperwork]
    @Xml AS xml
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@AnnexesConsecutives VARCHAR(20),
			@AnnexId INT			

	EXEC [Authorization].[SP_SaveTraceabilityPaperwork_Output] 
		@Xml,
		--------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT, 
		--------------------------------------------		
		@AnnexesConsecutives OUTPUT,
		@AnnexId OUTPUT

	SELECT	@CodeResult AS CodeResult, 
			@MessageResult AS MessageResult, 		
			@AnnexesConsecutives as AnnexesConsecutives,
			@AnnexId as AnnexId
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la trazabilidad de trámites de autorización médica a partir de un XML con la información del trámite. Actúa como punto de entrada que delega el procesamiento real al procedimiento interno SP_SaveTraceabilityPaperwork_Output, el cual ejecuta la lógica de persistencia y devuelve el resultado. Retorna un código de resultado, un mensaje de respuesta, los consecutivos de anexos generados y el identificador del anexo registrado. Se usa para registrar y hacer seguimiento del ciclo de vida de trámites de autorización de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_SaveTraceabilityPaperwork';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_SaveTraceabilityPaperwork';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega el guardado de la trazabilidad de trámites al procedimiento _Output y retorna como resultset el código/mensaje y los identificadores generados del anexo.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un XML de entrada con la información del trámite a registrar.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No ejecuta lógica de negocio propia: toda persistencia/validación recae en SP_SaveTraceabilityPaperwork_Output.; Siempre devuelve exactamente un resultset con cuatro columnas (CodeResult, MessageResult, AnnexesConsecutives, AnnexId).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'trazabilidad de trámites; anexos; consecutivos de anexos', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Tras invocar SP_SaveTraceabilityPaperwork_Output, retorna un SELECT con CodeResult, MessageResult, AnnexesConsecutives y AnnexId provenientes de los OUTPUT del proc interno.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Authorization.SP_SaveTraceabilityPaperwork_Output', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTraceabilityPaperwork';
-- GO
