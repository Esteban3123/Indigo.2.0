-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-07-16
-- Description:	Procedimiento que se encarga de validar los detalles de la orden de servicio
-- =============================================
CREATE PROCEDURE [Billing].[SP_ValidateServiceOrderDetail]
	@ServiceOrderDetailXml XML,
	@UserCode VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX)

	EXEC [Billing].[SP_ValidateServiceOrderDetail_Output]
		@ServiceOrderDetailXml,
		@UserCode,
		--------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT

	SELECT	@CodeResult AS CodeResult, 
			@MessageResult AS MessageResult
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que valida los detalles de una orden de servicio en el módulo de facturación. Recibe la información del detalle de la orden en formato XML y el código del usuario que ejecuta la acción, luego delega la lógica de validación al procedimiento interno SP_ValidateServiceOrderDetail_Output, del cual obtiene un código y un mensaje de resultado. Devuelve al llamador si la orden de servicio es válida o no, junto con el mensaje descriptivo del resultado, siendo el punto de entrada principal para verificar la integridad de los ítems de una orden antes de procesarlos en facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateServiceOrderDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateServiceOrderDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega la validación de los detalles de una orden de servicio a un procedimiento interno y expone su código y mensaje de resultado como result set.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateServiceOrderDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el procedimiento Billing.SP_ValidateServiceOrderDetail_Output que reciba el XML del detalle y el código de usuario y devuelva los OUTPUT de código y mensaje.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateServiceOrderDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna un único result set con las columnas CodeResult y MessageResult provenientes del procedimiento delegado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateServiceOrderDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'orden de servicio; detalle de orden de servicio', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateServiceOrderDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras invocar SP_ValidateServiceOrderDetail_Output, hace SELECT de @CodeResult y @MessageResult devolviéndolos como columnas CodeResult y MessageResult.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateServiceOrderDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_ValidateServiceOrderDetail_Output', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateServiceOrderDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateServiceOrderDetail';
-- GO
