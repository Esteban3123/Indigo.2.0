-- ===============================================================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-10
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar un compromiso
-- ===============================================================================================================================
CREATE PROCEDURE [Budget].[SP_SaveCommitment]
	@CommitmentXml AS XML,
	@CommitmentDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Budget].[SP_SaveCommitment_Output] 
		@CommitmentXml, 
		@CommitmentDetailForDeleteXml,
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que guarda, actualiza o confirma un compromiso presupuestal en el módulo de Budget. Recibe la información del compromiso y sus detalles a eliminar en formato XML, junto con el código del usuario que realiza la operación. Delega la lógica principal al procedimiento interno SP_SaveCommitment_Output y retorna el resultado de la operación (código, mensaje, identificador y código del compromiso generado o modificado). Es el punto de entrada para la gestión de compromisos presupuestales desde la capa de aplicación.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCommitment';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCommitment';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega en Budget.SP_SaveCommitment_Output la creación, actualización o confirmación de un compromiso presupuestal y devuelve el resultado como result set (CodeResult, MessageResult, Id, Code).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML del compromiso y el XML con los detalles a eliminar deben tener la estructura esperada por Budget.SP_SaveCommitment_Output.; El usuario indicado debe existir en el sistema para que el proc subyacente acepte la operación.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La persistencia/actualización/confirmación del compromiso se delega íntegramente al procedimiento Budget.SP_SaveCommitment_Output; este wrapper no aplica lógica adicional.; Siempre devuelve un único result set con las columnas CodeResult, MessageResult, Id y Code provenientes de los OUTPUT del proc invocado.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'compromiso presupuestal', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras invocar Budget.SP_SaveCommitment_Output, retorna mediante SELECT las variables @CodeResult, @MessageResult, @Id y @Code como un único result set.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveCommitment_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitment';
-- GO
