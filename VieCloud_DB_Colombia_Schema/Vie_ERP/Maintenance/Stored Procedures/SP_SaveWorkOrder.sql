-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-05-17
-- Description:	Procedimiento que se encarga de guardar, actualizar, anular, confirmar una orden de trabajo
-- =============================================
CREATE PROCEDURE [Maintenance].[SP_SaveWorkOrder]
    @WorkOrderXml AS XML,
	@UserCode AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Maintenance].[SP_SaveWorkOrder_Output]
		@WorkOrderXml, 
		@UserCode, 
		--------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT, 
		--------------------------------------------
		@Id OUTPUT, 
		@Code OUTPUT

	SELECT	@CodeResult AS CodeResult, 
			@MessageResult AS MessageResult, 
			@Id as Id, 
			@Code as Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento principal para gestionar órdenes de trabajo de mantenimiento: permite crear, actualizar, anular y confirmar una orden de trabajo. Recibe los datos de la orden en formato XML y el código del usuario que realiza la operación. Delega la lógica de negocio y persistencia al procedimiento interno SP_SaveWorkOrder_Output, del cual obtiene el resultado de la operación (código y mensaje de estado), el identificador interno y el código asignado a la orden. Devuelve al llamador el resultado de la transacción para que el sistema pueda informar al usuario si la orden fue procesada exitosamente o si ocurrió algún error.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'PROCEDURE', @level1name = N'SP_SaveWorkOrder';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'PROCEDURE', @level1name = N'SP_SaveWorkOrder';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega en SP_SaveWorkOrder_Output la persistencia (guardar/actualizar/anular/confirmar) de una orden de trabajo y expone como result set el resultado y el identificador/código generado.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proporcionar un XML con la información de la orden de trabajo y un código de usuario responsable de la operación.; El procedimiento Maintenance.SP_SaveWorkOrder_Output debe existir y aceptar la firma esperada (XML, usuario y cuatro OUTPUT).', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La operación sobre la orden de trabajo (guardar/actualizar/anular/confirmar) se delega íntegramente al SP_SaveWorkOrder_Output; este wrapper no aplica lógica adicional.; Siempre devuelve un único result set con las columnas CodeResult, MessageResult, Id y Code provenientes de los parámetros OUTPUT del procedimiento interno.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de trabajo; Mantenimiento', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras invocar SP_SaveWorkOrder_Output, retorna un SELECT con CodeResult, MessageResult, Id y Code para que el cliente conozca el éxito de la operación y la orden afectada.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Maintenance.SP_SaveWorkOrder_Output', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrder';
-- GO
