
CREATE VIEW [dbo].[atendidos por especuialidad cons exte]
AS
SELECT     dbo.HCHISPACA.IPCODPACI, dbo.HCHISPACA.UFUCODIGO, dbo.HCHISPACA.FECHISPAC, dbo.HCHISPACA.CODDIAGNO, dbo.HCHISPACA.CODESPTRA, 
                      dbo.INPACIENT.IPNOMCOMP, dbo.INDIAGNOS.NOMDIAGNO, dbo.INPACIENT.NUMCARPET
FROM         dbo.HCHISPACA INNER JOIN
                      dbo.INPACIENT ON dbo.HCHISPACA.IPCODPACI = dbo.INPACIENT.IPCODPACI INNER JOIN
                      dbo.INDIAGNOS ON dbo.HCHISPACA.CODDIAGNO = dbo.INDIAGNOS.CODDIAGNO
WHERE     (dbo.HCHISPACA.UFUCODIGO = '006') AND (dbo.HCHISPACA.CODESPTRA = '620') AND (dbo.HCHISPACA.FECHISPAC BETWEEN CONVERT(DATETIME, 
                      '2013-01-01 00:00:00', 102) AND CONVERT(DATETIME, '2013-12-31 23:59:00', 102))
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de consulta externa filtrada para la unidad funcional ''006'' y especialidad ''620'', que consolida pacientes atendidos durante el año 2013. Combina historias clínicas con datos demográficos del paciente y la descripción del diagnóstico CIE-10, orientada a reporting de atenciones por especialidad en consulta externa para un período y servicio específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'atendidos por especuialidad cons exte';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'atendidos por especuialidad cons exte';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes atendidos en consulta externa por una especialidad específica durante el año 2013, mostrando datos del paciente y su diagnóstico clínico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'atendidos por especuialidad cons exte';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de historias clínicas en HCHISPACA con la unidad funcional ''006'' y especialidad tratante ''620''.; Los pacientes referenciados deben existir en INPACIENT (INNER JOIN).; Los diagnósticos referenciados deben existir en INDIAGNOS (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'atendidos por especuialidad cons exte';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen atenciones de la unidad funcional ''006'' (consulta externa).; Solo se incluyen atenciones de la especialidad tratante ''620''.; El rango de fechas está fijo (hardcodeado) al año 2013.; Excluye atenciones cuyo paciente o diagnóstico no exista en los catálogos (por ser INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'atendidos por especuialidad cons exte';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Historia clínica; Diagnóstico; Especialidad tratante; Unidad funcional; Consulta externa; Carpeta del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'atendidos por especuialidad cons exte';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve historias clínicas filtradas por UFUCODIGO=''006'', CODESPTRA=''620'' y FECHISPAC entre 2013-01-01 00:00:00 y 2013-12-31 23:59:00.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'atendidos por especuialidad cons exte';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'atendidos por especuialidad cons exte';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'atendidos por especuialidad cons exte';
GO
