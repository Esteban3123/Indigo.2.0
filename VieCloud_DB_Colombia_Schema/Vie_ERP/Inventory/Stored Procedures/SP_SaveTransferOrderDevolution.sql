-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-26
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una devolución de orden de traslado de inventario
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SaveTransferOrderDevolution]
    @TransferOrderDevolutionXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@MessageResultAux VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC  [Inventory].[SP_SaveTransferOrderDevolution_Output]
		@TransferOrderDevolutionXml, 
		@CodeUser, 
		--------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT, 
		@MessageResultAux OUTPUT,
		--------------------------------------------
		@Id OUTPUT, 
		@Code OUTPUT

	SELECT 
		@CodeResult AS CodeResult, 
		@MessageResult AS MessageResult, 
		@MessageResultAux AS MessageResultAux,
		@Id as Id, 
		@Code as Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite crear, actualizar o confirmar una devolución de orden de traslado de inventario entre ubicaciones o bodegas. Recibe los datos de la devolución en formato XML junto con el código del usuario que realiza la operación, y delega el procesamiento principal al procedimiento interno SP_SaveTransferOrderDevolution_Output, el cual ejecuta la lógica de negocio y persistencia. Retorna un código de resultado, mensajes de éxito o error, y el identificador y código del registro de devolución generado o actualizado. Aplica sobre la entidad de devolución de traslado de inventario, siendo el punto de entrada para gestionar reversiones o retornos de mercancía entre áreas o centros de atención.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveTransferOrderDevolution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveTransferOrderDevolution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega en el procedimiento de salida la persistencia (guardar/actualizar/confirmar) de una devolución de orden de traslado de inventario y devuelve el resultado como result set.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrderDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere un XML con la información de la devolución de orden de traslado.; Se requiere el código de usuario que ejecuta la operación.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrderDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La lógica de validación y persistencia se delega íntegramente a SP_SaveTransferOrderDevolution_Output; este SP solo expone el resultado.; SET NOCOUNT ON suprime mensajes de conteo de filas para que el único result set sea el SELECT final.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrderDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución de orden de traslado de inventario; Inventario', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrderDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Tras invocar SP_SaveTransferOrderDevolution_Output, se retorna un SELECT con CodeResult, MessageResult, MessageResultAux, Id y Code.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrderDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_SaveTransferOrderDevolution_Output', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrderDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrderDevolution';
-- GO
