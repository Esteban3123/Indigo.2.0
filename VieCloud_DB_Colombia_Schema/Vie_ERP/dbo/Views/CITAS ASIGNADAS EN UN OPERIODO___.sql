

CREATE VIEW [dbo].[CITAS ASIGNADAS EN UN OPERIODO___]
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista estática que consolida las citas médicas agendadas durante el año 2014 (del 1 de enero al 31 de diciembre), uniendo los datos de agendamiento con el nombre completo del paciente y la descripción de la especialidad médica correspondiente. Incluye atributos como fechas de inicio y fin, estado de la cita, tipo y si es una cita extra. Está ordenada cronológicamente y limitada a 100 registros, lo que sugiere uso puntual de reporte o consulta histórica de ese período.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CITAS ASIGNADAS EN UN OPERIODO___';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CITAS ASIGNADAS EN UN OPERIODO___';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las citas asignadas dentro del año 2014 enriquecidas con el nombre del paciente y la descripción de la especialidad, ordenadas cronológicamente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CITAS ASIGNADAS EN UN OPERIODO___';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las citas deben tener un paciente válido referenciado en el maestro de pacientes.; Las citas deben tener una especialidad válida referenciada en el catálogo de especialidades.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CITAS ASIGNADAS EN UN OPERIODO___';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen citas cuya fecha/hora de inicio cae dentro del año 2014 (entre 2014-01-01 y 2014-12-31).; Solo se incluyen citas con paciente existente en el maestro de pacientes y especialidad existente en el catálogo de especialidades (joins internos).; Los resultados se entregan ordenados cronológicamente por la fecha/hora de inicio de la cita.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CITAS ASIGNADAS EN UN OPERIODO___';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Paciente; Especialidad médica; Cita extra; Estado de cita; Tipo de cita', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CITAS ASIGNADAS EN UN OPERIODO___';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.AGASICITA: Devuelve únicamente las citas cuya FECHORAIN está entre 2014-01-01 y 2014-12-31, junto con datos del paciente y la especialidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CITAS ASIGNADAS EN UN OPERIODO___';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.INPACIENT; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CITAS ASIGNADAS EN UN OPERIODO___';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CITAS ASIGNADAS EN UN OPERIODO___';
GO
