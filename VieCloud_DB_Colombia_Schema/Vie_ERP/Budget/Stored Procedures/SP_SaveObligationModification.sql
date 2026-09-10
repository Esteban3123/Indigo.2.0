-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-11
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una modificación de la obligacion
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveObligationModification]
    @ObligationModificationXml AS XML,
	@ObligationModificationDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@AuxiliaryResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Budget].[SP_SaveObligationModification_Output] 
		@ObligationModificationXml, 
		@ObligationModificationDetailForDeleteXml, 
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite crear, actualizar o confirmar una modificación sobre una obligación presupuestaria. Recibe los datos de la modificación y el detalle de ítems a eliminar en formato XML, junto con el código del usuario que ejecuta la operación. Delega el procesamiento real al procedimiento interno SP_SaveObligationModification_Output y retorna el resultado de la operación, incluyendo código de resultado, mensaje, identificador y código de la modificación generada o actualizada. Es el punto de entrada del módulo de presupuesto (Budget) para gestionar cambios sobre obligaciones existentes.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveObligationModification';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveObligationModification';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'"Wrapper que delega en el procedimiento _Output el guardado/actualización/confirmación de una modificación de obligación y devuelve el resultado (código, mensaje, auxiliar, Id y Code) como conjunto de filas.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere un XML con la cabecera/detalle de la modificación de obligación y un XML con los detalles a eliminar.; Debe proveerse el código de usuario que realiza la operación.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La lógica de persistencia se delega íntegramente al procedimiento _Output; este wrapper solo expone los OUTPUT como result set.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Modificación de obligación presupuestal', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras invocar SP_SaveObligationModification_Output se retorna un SELECT con CodeResult, MessageResult, AuxiliaryResult, Id y Code obtenidos como OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveObligationModification_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligationModification';
-- GO
