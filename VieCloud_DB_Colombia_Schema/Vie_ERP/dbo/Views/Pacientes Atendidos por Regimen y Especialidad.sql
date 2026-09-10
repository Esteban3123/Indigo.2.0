
CREATE VIEW [dbo].[Pacientes Atendidos por Regimen y Especialidad]
AS
SELECT     dbo.AGASICITA.CODAUTONU, dbo.INESPECIA.DESESPECI, dbo.AGASICITA.IPCODPACI, dbo.INPACIENT.IPNOMCOMP, dbo.INPACIENT.IPTIPOPAC, 
                      dbo.AGASICITA.FECHORAIN, dbo.AGASICITA.CODESTCIT
FROM         dbo.AGASICITA INNER JOIN
                      dbo.INESPECIA ON dbo.AGASICITA.CODESPECI = dbo.INESPECIA.CODESPECI INNER JOIN
                      dbo.INPACIENT ON dbo.AGASICITA.IPCODPACI = dbo.INPACIENT.IPCODPACI
WHERE     (dbo.AGASICITA.CODESTCIT = '1') AND (dbo.AGASICITA.FECHORAIN >= CONVERT(DATETIME, '2013-01-01 00:00:00', 102)) AND 
                      (dbo.AGASICITA.FECHORAIN <= CONVERT(DATETIME, '2013-06-30 00:00:00', 102))
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los pacientes atendidos mediante citas médicas confirmadas (estado 1) durante el primer semestre de 2013, cruzando información de agendamiento de citas (AGASICITA), el catálogo de especialidades médicas (INESPECIA) y el maestro de pacientes (INPACIENT). Muestra para cada cita atendida: el número de autorización, la especialidad médica en la que fue atendido el paciente, la cédula o documento de identidad del paciente, su nombre completo, el tipo o régimen del paciente (contributivo, subsidiado, particular, etc.), la fecha y hora de la cita, y el estado de la misma. Sirve para reportería y análisis de producción asistencial segmentada por régimen de afiliación y especialidad médica en un período específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Pacientes Atendidos por Regimen y Especialidad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Pacientes Atendidos por Regimen y Especialidad';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las citas atendidas en el primer semestre de 2013, mostrando paciente, especialidad y tipo de paciente (régimen) para análisis de atención por especialidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Pacientes Atendidos por Regimen y Especialidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las citas deben existir en el registro de agendamiento con estado y fecha válidos; El paciente y la especialidad referidos en la cita deben existir en sus catálogos maestros', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Pacientes Atendidos por Regimen y Especialidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan citas en estado ''1''; El rango de fechas está fijo (hardcodeado) al primer semestre de 2013; Se requiere correspondencia (INNER JOIN) entre cita, especialidad y paciente; citas sin paciente o especialidad válidos se excluyen', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Pacientes Atendidos por Regimen y Especialidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Especialidad médica; Paciente; Tipo de paciente (régimen); Estado de cita; Autorización', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Pacientes Atendidos por Regimen y Especialidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve únicamente citas con CODESTCIT=''1'' (estado atendida/efectiva) cuya FECHORAIN esté entre 2013-01-01 y 2013-06-30', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Pacientes Atendidos por Regimen y Especialidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODESTCIT = ''1'' y FECHORAIN entre 2013-01-01 y 2013-06-30 → se incluye la cita en el resultado else se excluye', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Pacientes Atendidos por Regimen y Especialidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.INESPECIA; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Pacientes Atendidos por Regimen y Especialidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Pacientes Atendidos por Regimen y Especialidad';
GO
