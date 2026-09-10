
/***********************************************************************************************
Modified By: Andres Ramirez
Date: 2020.01.22
Description: Consulta para sala con fecha y hora, y cambiar estado dentro de la tabla RISORDENES - Web 
************************************************************************************************/
CREATE PROCEDURE [dbo].[SPNEWRIS_AsignarSala] 
(
@Auto int,
@Estado int,
@IdSala int,
@FechaAgendo Datetimeoffset,
@FechaInicial Datetimeoffset,
@FechaFinal Datetimeoffset,
@Usuario varchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
UPDATE RISORDENES SET ESTADO = @Estado, IDSALA = @IdSala, FECAGENDO = @FechaAgendo, FECHORAIN = @FechaInicial, FECHORAFI = @FechaFinal, USUAGENDO = @Usuario
WHERE AUTO = @Auto

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Asigna o actualiza la sala de imágenes diagnósticas para una orden de radiología o ecografía previamente registrada. Actualiza en la tabla de órdenes de imágenes (RISORDENES) el estado de la orden, la sala asignada, la fecha de agendamiento, el horario de inicio y fin del estudio, y el usuario que realizó el agendamiento. Se utiliza desde la interfaz web del módulo RIS (Radiología e Imágenes) para confirmar y programar en qué sala y en qué franja horaria se ejecutará un examen de imagen diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPNEWRIS_AsignarSala';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPNEWRIS_AsignarSala';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Asigna sala y ventana horaria a una orden RIS, actualizando su estado, fecha de agendamiento y usuario que agenda.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_AsignarSala';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una orden RIS con AUTO igual al recibido para que la actualización tenga efecto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_AsignarSala';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La actualización siempre afecta una única orden identificada por su llave AUTO.; Al asignar sala se registra simultáneamente estado, sala, fecha de agendamiento, ventana horaria (inicio/fin) y usuario que agenda.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_AsignarSala';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden RIS; Sala; Agendamiento; Estado de orden', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_AsignarSala';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.RISORDENES: Cuando AUTO coincide con la orden indicada, se actualizan ESTADO, IDSALA, FECAGENDO, FECHORAIN, FECHORAFI y USUAGENDO en RISORDENES.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_AsignarSala';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_AsignarSala';
-- GO
