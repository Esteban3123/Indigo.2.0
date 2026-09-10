-- Stored Procedure

CREATE PROCEDURE [dbo].[SPREP_HC_AgendamientoEspecialidades]
(
@CentroAtencion Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

SELECT CONSECUTI AS 'CONSECUTIVO', A.CODCENATE AS 'CODIGO CENTRO DE ATENCION', D.NOMCENATE AS 'CENTRO DE ATENCION',A.CODPROSAL AS 'CODIGO PROFESIONAL', 
B.NOMMEDICO AS 'NOMBRE PROFESIONAL',A.CODESPECI AS 'CODIGO ESPECIALIDAD', C.DESESPECI AS ESPECIALIDAD,FECHAINIC AS 'FECHA INICIAL',FECHAINIC AS 'HORA INICIAL', FECHAFINA AS 'HORA FINAL',
OBSERVACI AS OBSERVACIONES
FROM HCTURNESP A with(nolock)
INNER JOIN INPROFSAL B with(nolock) ON A.CODPROSAL=B.CODPROSAL 
INNER JOIN INESPECIA C with(nolock) ON A.CODESPECI=C.CODESPECI
INNER JOIN ADCENATEN D with(nolock) ON A.CODCENATE=D.CODCENATE
WHERE A.CODCENATE=@CentroAtencion

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de agendamiento de turnos por especialidad médica para un centro de atención específico. Consulta los turnos programados en la tabla HCTURNESP y los enriquece con el nombre del profesional de la salud (médico o especialista) desde INPROFSAL, la descripción de la especialidad médica desde INESPECIA y el nombre del centro de atención desde ADCENATEN. Devuelve el consecutivo del turno, datos del profesional, especialidad, fechas y horas de inicio y fin, y observaciones, filtrando por el código de sede o centro de atención recibido como parámetro. Se utiliza para generar reportes operativos de agenda de especialidades por sede.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_AgendamientoEspecialidades';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_AgendamientoEspecialidades';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los turnos/agendamientos de profesionales por especialidad asociados a un centro de atención, con datos descriptivos del profesional, especialidad y centro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AgendamientoEspecialidades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención recibido debe existir en ADCENATEN para que aparezcan resultados; Los turnos en HCTURNESP deben tener profesional registrado en INPROFSAL y especialidad registrada en INESPECIA (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AgendamientoEspecialidades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan turnos que tengan correspondencia válida en profesional, especialidad y centro (INNER JOIN excluye huérfanos); El resultado se restringe al centro de atención solicitado; Se usa NOLOCK en todas las tablas: lecturas sin bloqueo, pueden incluir datos no confirmados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AgendamientoEspecialidades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de atención; Profesional de salud; Especialidad médica; Turno/Agendamiento; Agenda por especialidad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AgendamientoEspecialidades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCTURNESP: Devuelve los turnos cuya CODCENATE coincide con el parámetro de centro de atención, enriquecidos con nombres del profesional, especialidad y centro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AgendamientoEspecialidades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCTURNESP; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AgendamientoEspecialidades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AgendamientoEspecialidades';
-- GO
