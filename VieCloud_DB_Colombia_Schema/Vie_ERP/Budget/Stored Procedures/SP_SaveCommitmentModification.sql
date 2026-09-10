-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-11
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una modificación de compromiso
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveCommitmentModification]
    @CommitmentModificationXml AS XML,
	@CommitmentModificationDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@AuxiliaryResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Budget].[SP_SaveCommitmentModification_Output] 
		@CommitmentModificationXml, 
		@CommitmentModificationDetailForDeleteXml, 
		@CodeUser, 
		--------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT, 
		@AuxiliaryResult OUTPUT,
		--------------------------------------------
		@Id OUTPUT, 
		@Code OUTPUT

	SELECT 
		@CodeResult AS CodeResult, 
		@MessageResult AS MessageResult, 
		@AuxiliaryResult AS AuxiliaryResult, 
		@Id as Id, 
		@Code as Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda, actualiza o confirma una modificación de compromiso presupuestal. Recibe los datos de la modificación y sus detalles a eliminar en formato XML, junto con el código del usuario que realiza la operación. Delega el procesamiento real al procedimiento interno SP_SaveCommitmentModification_Output y retorna el resultado de la operación (código, mensaje, identificador y código del registro afectado). Es el punto de entrada principal para gestionar cambios sobre compromisos del módulo de presupuesto (Budget).', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCommitmentModification';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCommitmentModification';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega el guardado/actualización/confirmación de una modificación de compromiso presupuestal y expone los resultados (código, mensaje, auxiliar, Id y Code) como un único result set.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitmentModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere XML con la cabecera/datos de la modificación de compromiso; Se requiere XML con los detalles a eliminar (puede ser vacío); Se requiere el código de usuario que ejecuta la operación', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitmentModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda la lógica de persistencia/validación se delega al SP _Output; este procedimiento solo transforma parámetros OUTPUT en columnas de un result set; SET NOCOUNT ON: no emite conteos intermedios de filas afectadas', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitmentModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Modificación de compromiso; Presupuesto', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitmentModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Tras ejecutar SP_SaveCommitmentModification_Output, retorna SELECT con CodeResult, MessageResult, AuxiliaryResult, Id y Code', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitmentModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveCommitmentModification_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitmentModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitmentModification';
-- GO
