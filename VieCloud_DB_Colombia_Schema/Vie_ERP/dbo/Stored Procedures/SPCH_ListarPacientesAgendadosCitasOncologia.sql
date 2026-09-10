-- Stored Procedure

CREATE PROCEDURE [dbo].[SPCH_ListarPacientesAgendadosCitasOncologia]
(
@centroAtencion char(10),
@UnidadFuncional Char(10),
@Fecha as date,
@Usuario as varchar(20)
)
AS
BEGIN
SET NOCOUNT ON;

---Nota para el desarrollador: Guía el sp de SPCH_ListarPacientesAgendadosCitasRenal
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes agendados en citas del servicio de Oncología para una fecha, centro de atención y unidad funcional específicos. Permite consultar la agenda de oncología filtrando por sede, unidad funcional, fecha de la cita y usuario que realiza la consulta. Está basado en la estructura del procedimiento de citas renales (SPCH_ListarPacientesAgendadosCitasRenal), adaptado para el programa oncológico. Útil para la gestión de agendamiento, seguimiento de pacientes oncológicos y reportes de ocupación de agenda en oncología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarPacientesAgendadosCitasOncologia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarPacientesAgendadosCitasOncologia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procedimiento esqueleto destinado a listar pacientes agendados a citas de oncología en un centro de atención y unidad funcional para una fecha dada, actualmente sin lógica implementada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasOncologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El procedimiento no realiza operaciones ni retorna resultados; su cuerpo solo contiene una nota de desarrollo que indica seguir como guía otro SP de citas renales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasOncologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Pacientes agendados; Citas de oncología; Centro de atención; Unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasOncologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasOncologia';
-- GO
