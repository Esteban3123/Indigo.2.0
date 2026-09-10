-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-20
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una orden de traslado de inventario
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SaveTransferOrder]
    @TransferOrderXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@MessageResultAux VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC  [Inventory].[SP_SaveTransferOrder_Output]
		@TransferOrderXml, 
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite crear, actualizar o confirmar una orden de traslado de inventario entre ubicaciones o almacenes. Recibe los datos de la orden en formato XML junto con el código del usuario que realiza la operación, y delega el procesamiento principal al procedimiento interno SP_SaveTransferOrder_Output. Retorna un código de resultado, mensajes de éxito o error, el identificador interno generado y el código de la orden procesada, permitiendo que la capa de negocio conozca el resultado de la operación de traslado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveTransferOrder';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveTransferOrder';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega en SP_SaveTransferOrder_Output el guardar/actualizar/confirmar una orden de traslado de inventario y devuelve como resultset el código, mensajes y el Id/Code generados.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un XML con la información de la orden de traslado y el código de usuario que ejecuta la operación.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La lógica de persistencia/validación se delega íntegramente al SP_SaveTransferOrder_Output; este wrapper no aplica reglas propias.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'orden de traslado de inventario', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Tras invocar SP_SaveTransferOrder_Output, retorna un resultset con CodeResult, MessageResult, MessageResultAux, Id y Code obtenidos como parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_SaveTransferOrder_Output', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTransferOrder';
-- GO
