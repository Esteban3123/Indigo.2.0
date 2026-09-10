CREATE PROCEDURE [Integrations].[UpdateBloodComponentsOrder_Mirth_Synch]
(
    @OrderId as int
)
AS
BEGIN
    SET NOCOUNT ON
		update dbo.[HCORHEMCO] set SYNCMIRTH = 1 where ID = @OrderId 
		print '1'
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca una orden de hemoterapia (transfusión de sangre o hemoderivados) como sincronizada con el motor de integración Mirth Connect. Recibe el identificador interno de la orden y actualiza el indicador de sincronización en el registro correspondiente de la tabla de órdenes de hemoterapia. Se usa para llevar el control de qué órdenes de componentes sanguíneos ya fueron enviadas o procesadas por el canal de interoperabilidad, evitando reenvíos o reprocesos.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'PROCEDURE', @level1name = N'UpdateBloodComponentsOrder_Mirth_Synch';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'PROCEDURE', @level1name = N'UpdateBloodComponentsOrder_Mirth_Synch';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Marca una orden de hemocomponentes como sincronizada con el motor de integración Mirth.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'UpdateBloodComponentsOrder_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en dbo.HCORHEMCO cuyo ID coincida con el identificador recibido', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'UpdateBloodComponentsOrder_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo afecta una orden específica identificada por su ID; El valor de SYNCMIRTH siempre se fija en 1 (sincronizado), nunca se restablece a 0 desde este procedimiento', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'UpdateBloodComponentsOrder_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de hemocomponentes; Sincronización con Mirth (motor de integración)', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'UpdateBloodComponentsOrder_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.HCORHEMCO: Para el registro con ID igual al parámetro recibido, se establece SYNCMIRTH = 1 indicando que la orden fue sincronizada con Mirth', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'UpdateBloodComponentsOrder_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'UpdateBloodComponentsOrder_Mirth_Synch';
-- GO
