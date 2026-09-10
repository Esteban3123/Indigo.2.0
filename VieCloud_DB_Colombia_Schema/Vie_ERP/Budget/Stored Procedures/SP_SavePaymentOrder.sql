-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-22
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una orden de pago
-- =============================================
CREATE PROCEDURE [Budget].[SP_SavePaymentOrder]
    @PaymentOrderXml AS XML,
	@PaymentOrderDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeMessage INT,
			@Message VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Budget].[SP_SavePaymentOrder_Output] @PaymentOrderXml, @PaymentOrderDetailForDeleteXml, @CodeUser, @CodeMessage OUTPUT, @Message OUTPUT, @Id OUTPUT, @Code OUTPUT

	SELECT 
		@CodeMessage AS CodeMessage, 
		@Message AS Message, 
		@Id as Id, 
		@Code as Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite crear, actualizar o confirmar una orden de pago en el módulo de presupuesto (Budget). Recibe la información de la orden de pago y sus detalles a eliminar en formato XML, junto con el código del usuario que ejecuta la operación. Delega el procesamiento real al procedimiento interno SP_SavePaymentOrder_Output y retorna el resultado de la operación con un código de mensaje, descripción, identificador y código de la orden procesada. Es el punto de entrada principal para la gestión de órdenes de pago en el sistema.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SavePaymentOrder';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SavePaymentOrder';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que ejecuta el guardado/actualización/confirmación de una orden de pago invocando al SP _Output y retorna como result set el código de mensaje, mensaje, Id y Code generados.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SavePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere proporcionar el XML con la cabecera/detalle de la orden de pago y el XML con los detalles a eliminar, además del usuario que ejecuta la operación.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SavePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Actúa como wrapper: delega toda la lógica de guardar/actualizar/confirmar la orden de pago al SP_SavePaymentOrder_Output y expone los OUTPUT como un único result set.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SavePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'orden de pago', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SavePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras llamar a SP_SavePaymentOrder_Output, devuelve un SELECT con CodeMessage, Message, Id y Code obtenidos como parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SavePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SavePaymentOrder_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SavePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SavePaymentOrder';
-- GO
