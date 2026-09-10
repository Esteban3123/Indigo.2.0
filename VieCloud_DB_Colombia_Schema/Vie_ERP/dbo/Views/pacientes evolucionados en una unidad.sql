
CREATE VIEW [dbo].[pacientes evolucionados en una unidad]
AS
SELECT     TOP (100) PERCENT dbo.HCHISPACA.FECHISPAC AS Expr1, dbo.HCHISPACA.UFUCODIGO, dbo.HCHISPACA.NUMEFOLIO, dbo.INPACIENT.IPCODPACI, 
                      dbo.INPACIENT.IPNOMCOMP, dbo.HCHISPACA.CODPROSAL, dbo.INPROFSAL.NOMMEDICO
FROM         dbo.HCHISPACA INNER JOIN
                      dbo.INPACIENT ON dbo.HCHISPACA.IPCODPACI = dbo.INPACIENT.IPCODPACI INNER JOIN
                      dbo.INPROFSAL ON dbo.HCHISPACA.CODPROSAL = dbo.INPROFSAL.CODPROSAL
WHERE     (dbo.HCHISPACA.FECHISPAC BETWEEN CONVERT(DATETIME, '2015-01-13 00:00:00', 102) AND CONVERT(DATETIME, '2015-01-13 23:59:59', 102)) AND 
                      (dbo.HCHISPACA.UFUCODIGO = '018')
ORDER BY Expr1
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de consulta ad hoc que lista los pacientes que tuvieron notas o folios clínicos registrados en la unidad funcional ''018'' durante el 13 de enero de 2015, mostrando fecha, número de folio, datos identificativos del paciente y el profesional de salud que generó cada registro. Sirve como reporte puntual de actividad clínica diaria por unidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'pacientes evolucionados en una unidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'pacientes evolucionados en una unidad';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes con notas clínicas (evoluciones) registradas en una unidad funcional específica durante un día determinado, mostrando folio, paciente y profesional tratante.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'pacientes evolucionados en una unidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las historias clínicas deben tener paciente asociado existente en el maestro de pacientes; Las historias clínicas deben tener profesional de salud asociado existente en el maestro de profesionales', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'pacientes evolucionados en una unidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Filtra exclusivamente la unidad funcional ''018''; Restringe el resultado a evoluciones del día 13 de enero de 2015; Solo expone evoluciones con paciente y profesional válidos (INNER JOIN obliga existencia en ambos maestros); Resultados ordenados ascendentemente por fecha de evolución', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'pacientes evolucionados en una unidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Historia clínica; Evolución clínica; Folio clínico; Unidad funcional; Profesional de la salud / Médico tratante', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'pacientes evolucionados en una unidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCHISPACA: Devuelve solo registros cuya fecha de evolución (FECHISPAC) esté entre 2015-01-13 00:00:00 y 2015-01-13 23:59:59 y cuya unidad funcional (UFUCODIGO) sea ''018''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'pacientes evolucionados en una unidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'pacientes evolucionados en una unidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'pacientes evolucionados en una unidad';
GO
