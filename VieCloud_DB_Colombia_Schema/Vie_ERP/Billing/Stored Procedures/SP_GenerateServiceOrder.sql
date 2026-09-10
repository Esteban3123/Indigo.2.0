-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 23-01-2015
-- Description:	Store para crear ordenes de servicio
-- =============================================
CREATE PROCEDURE [Billing].[SP_GenerateServiceOrder]
	@ServiceOrderXml as xml,
	@User varchar(20)
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @CodeResult VARCHAR(3),
			@MessageResult VARCHAR(MAX),
			@StatusResult TINYINT,
			@Id INT

	EXEC Billing.SP_GenerateServiceOrder_Output	@ServiceOrderXml, 
													@User, 
													--Salidas
													@CodeResult OUTPUT, 
													@MessageResult OUTPUT, 
													@StatusResult OUTPUT,	
													@Id OUTPUT

	select @CodeResult CodeMessage, @MessageResult [Message], @Id ServiceOrderId, @StatusResult [Status]
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera una orden de servicio en el módulo de facturación a partir de un documento XML con los datos de la orden y el usuario que la registra. Actúa como punto de entrada principal delegando la lógica de creación al procedimiento interno SP_GenerateServiceOrder_Output, del cual recibe el resultado de la operación: un código de respuesta, un mensaje descriptivo, el identificador único de la orden creada y el estado del proceso. Retorna estos valores al sistema llamador para confirmar si la orden fue generada exitosamente o si ocurrió algún error. Se utiliza típicamente desde la capa de aplicación cuando se necesita registrar una nueva orden de servicio vinculada a la facturación del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateServiceOrder';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateServiceOrder';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega la creación de una orden de servicio a un procedimiento interno y devuelve como result set el código, mensaje, identificador generado y estado de la operación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateServiceOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de la orden de servicio debe respetar el contrato esperado por Billing.SP_GenerateServiceOrder_Output.; El usuario debe estar identificado para registrarse como autor de la operación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateServiceOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda la lógica de creación se delega al procedimiento Billing.SP_GenerateServiceOrder_Output; este wrapper no realiza validaciones ni escrituras propias.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateServiceOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'orden de servicio; facturación', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateServiceOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras invocar SP_GenerateServiceOrder_Output, devuelve un result set con CodeMessage, Message, ServiceOrderId y Status proveniente de los OUTPUT del proc interno.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateServiceOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_GenerateServiceOrder_Output', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateServiceOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateServiceOrder';
-- GO
