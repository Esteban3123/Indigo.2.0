
CREATE VIEW [dbo].[CITAS ASIGNADAS EN UN OPERIODO]
AS
SELECT     TOP (100) PERCENT dbo.AGASICITA.IPCODPACI, dbo.INPACIENT.IPNOMCOMP, dbo.AGASICITA.FECHORAIN, dbo.AGASICITA.FECHORAFI, dbo.AGASICITA.CITAEXTRA, 
                      dbo.AGASICITA.CODESTCIT, dbo.AGASICITA.CODTIPCIT, dbo.INESPECIA.DESESPECI
FROM         dbo.AGASICITA INNER JOIN
                      dbo.INPACIENT ON dbo.AGASICITA.IPCODPACI = dbo.INPACIENT.IPCODPACI INNER JOIN
                      dbo.INESPECIA ON dbo.AGASICITA.CODESPECI = dbo.INESPECIA.CODESPECI
WHERE     (dbo.AGASICITA.FECHORAIN >= CONVERT(DATETIME, '2014-01-01 00:00:00', 102)) AND (dbo.AGASICITA.FECHORAIN <= CONVERT(DATETIME, '2014-12-31 00:00:00', 
                      102))
ORDER BY dbo.AGASICITA.FECHORAIN
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las citas médicas asignadas durante un período específico (en este caso el año 2014), integrando datos del agendamiento de citas (AGASICITA), información del paciente (nombre completo y cédula desde INPACIENT) y la descripción de la especialidad médica (desde INESPECIA). Sirve para reportería y consulta operativa de citas agendadas, mostrando por cada cita: la identificación y nombre completo del paciente, fecha y hora de inicio y fin de la cita, si es cita extra, el estado de la cita, el tipo de cita y la especialidad médica correspondiente. Es útil para analizar la ocupación de agendas médicas, auditar citas asignadas en un rango de tiempo y hacer seguimiento al servicio de agendamiento por especialidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'CITAS ASIGNADAS EN UN OPERIODO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'CITAS ASIGNADAS EN UN OPERIODO';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las citas médicas asignadas durante el año 2014, enriquecidas con el nombre del paciente y la descripción de la especialidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CITAS ASIGNADAS EN UN OPERIODO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de pacientes relacionados en el maestro de pacientes (INNER JOIN por IPCODPACI); Existencia de la especialidad asociada a la cita en el catálogo de especialidades (INNER JOIN por CODESPECI)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CITAS ASIGNADAS EN UN OPERIODO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El rango temporal está fijo en el año 2014 (no parametrizable); Solo se incluyen citas con paciente y especialidad existentes (INNER JOIN), excluyendo huérfanos; Se exponen tipo de cita, estado de cita y marca de cita extra para clasificación posterior', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CITAS ASIGNADAS EN UN OPERIODO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Paciente; Especialidad médica; Cita extra; Estado de cita; Tipo de cita', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CITAS ASIGNADAS EN UN OPERIODO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve únicamente las citas cuya FECHORAIN está entre 2014-01-01 y 2014-12-31, ordenadas ascendentemente por fecha/hora de inicio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CITAS ASIGNADAS EN UN OPERIODO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.INPACIENT; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CITAS ASIGNADAS EN UN OPERIODO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CITAS ASIGNADAS EN UN OPERIODO';
GO
