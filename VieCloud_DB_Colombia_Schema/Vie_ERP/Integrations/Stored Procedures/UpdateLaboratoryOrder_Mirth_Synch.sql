-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [Integrations].[UpdateLaboratoryOrder_Mirth_Synch]
(
    @OrderId as int,
	@TypeOrder as varchar(20)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    if @TypeOrder = 'Ambulatoria' begin
		update dbo.AMBORDLAB set SYNCMIRTH = 1 where AUTO = @OrderId 
		print '1'
	end else if @TypeOrder = 'Hospitalaria' begin
		update dbo.HCORDLABO set SYNCMIRTH = 1 where AUTO = @OrderId 
		print '2'
	end

END
GO
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de integración que marca una orden de laboratorio como sincronizada con el sistema Mirth Connect. Recibe el identificador de la orden y su tipo (Ambulatoria u Hospitalaria) y actualiza el indicador de sincronización SYNCMIRTH en la tabla correspondiente: AMBORDLAB para órdenes ambulatorias o HCORDLABO para órdenes hospitalarias registradas en la historia clínica. Su propósito es garantizar que cada orden de laboratorio quede registrada como procesada por el motor de integración HL7/Mirth, evitando reprocesos o envíos duplicados hacia sistemas externos de laboratorio clínico.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'PROCEDURE', @level1name = N'UpdateLaboratoryOrder_Mirth_Synch';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'PROCEDURE', @level1name = N'UpdateLaboratoryOrder_Mirth_Synch';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Marca una orden de laboratorio (ambulatoria u hospitalaria) como sincronizada con el motor de integración Mirth.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'UpdateLaboratoryOrder_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El identificador de orden debe existir en la tabla correspondiente al tipo de orden; El tipo de orden debe ser ''Ambulatoria'' o ''Hospitalaria'' para que se ejecute alguna acción', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'UpdateLaboratoryOrder_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se actualizan órdenes de laboratorio identificadas por AUTO; Si el tipo de orden no es ''Ambulatoria'' ni ''Hospitalaria'', no se realiza ninguna actualización; El valor de sincronización siempre se fija en 1 (no se desmarca)', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'UpdateLaboratoryOrder_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de laboratorio ambulatoria; Orden de laboratorio hospitalaria; Sincronización con Mirth (motor de integración)', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'UpdateLaboratoryOrder_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.AMBORDLAB: Cuando el tipo de orden = ''Ambulatoria'', marca SYNCMIRTH = 1 para la orden cuyo AUTO coincide con el identificador recibido; [UPDATE] dbo.HCORDLABO: Cuando el tipo de orden = ''Hospitalaria'', marca SYNCMIRTH = 1 para la orden cuyo AUTO coincide con el identificador recibido', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'UpdateLaboratoryOrder_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de orden = ''Ambulatoria'' → Actualiza SYNCMIRTH=1 en dbo.AMBORDLAB else Si es ''Hospitalaria'', actualiza SYNCMIRTH=1 en dbo.HCORDLABO; cualquier otro valor no realiza acción', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'UpdateLaboratoryOrder_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'UpdateLaboratoryOrder_Mirth_Synch';
-- GO
