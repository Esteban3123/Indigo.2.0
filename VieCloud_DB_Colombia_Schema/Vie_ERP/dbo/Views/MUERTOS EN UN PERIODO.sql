
CREATE VIEW [dbo].[MUERTOS EN UN PERIODO]
AS
SELECT        TOP (100) PERCENT dbo.HCHISPACA.FECHISPAC AS [FECHA ATENCION], dbo.HCHISPACA.CODDIAGNO AS DIAGNOSTICO, dbo.INDIAGNOS.NOMDIAGNO, 
                         dbo.HCHISPACA.INDICAPAC AS Expr3, dbo.HCHISPACA.IPCODPACI, dbo.INPACIENT.IPNOMCOMP, dbo.HCHISPACA.NUMEFOLIO, dbo.HCHISPACA.UFUCODIGO, 
                         dbo.INUNIFUNC.UFUDESCRI
FROM            dbo.HCHISPACA INNER JOIN
                         dbo.INPACIENT ON dbo.HCHISPACA.IPCODPACI = dbo.INPACIENT.IPCODPACI INNER JOIN
                         dbo.INDIAGNOS ON dbo.HCHISPACA.CODDIAGNO = dbo.INDIAGNOS.CODDIAGNO INNER JOIN
                         dbo.INUNIFUNC ON dbo.HCHISPACA.UFUCODIGO = dbo.INUNIFUNC.UFUCODIGO
WHERE        (dbo.HCHISPACA.FECHISPAC >= CONVERT(DATETIME, '2015-04-01 00:00:00', 102) AND dbo.HCHISPACA.FECHISPAC <= CONVERT(DATETIME, '2015-06-30 00:00:00', 
                         102)) AND (dbo.HCHISPACA.INDICAPAC = '11')
ORDER BY [FECHA ATENCION]
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Listado de pacientes fallecidos (indicador de condición ''11'' = muerte) atendidos entre abril y junio de 2015, obtenido cruzando las notas clínicas (historia clínica) con el catálogo de diagnósticos CIE-10, los datos del paciente y las unidades funcionales donde ocurrió la atención. Muestra la fecha de la atención, el diagnóstico registrado al momento del fallecimiento, el nombre completo del paciente, la cédula, el folio de la historia clínica y el servicio o sala donde falleció. Sirve como reporte de mortalidad institucional para auditoría clínica, estadísticas vitales y análisis epidemiológico de muertes hospitalarias en el período indicado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'MUERTOS EN UN PERIODO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'MUERTOS EN UN PERIODO';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las atenciones clínicas de pacientes fallecidos (indicador ''11'') ocurridas en el segundo trimestre de 2015, mostrando diagnóstico, paciente y unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'MUERTOS EN UN PERIODO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los códigos de paciente, diagnóstico y unidad funcional registrados en la historia clínica deben existir en sus respectivos catálogos maestros para que la atención aparezca.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'MUERTOS EN UN PERIODO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna atenciones cuyo indicador de atención sea ''11'' (interpretado como fallecidos).; Solo retorna atenciones cuya fecha esté entre 2015-04-01 y 2015-06-30 (rango fijo embebido en la vista).; Cada fila debe tener paciente, diagnóstico y unidad funcional existentes en sus catálogos maestros (INNER JOIN).; Resultados ordenados ascendentemente por fecha de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'MUERTOS EN UN PERIODO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; diagnóstico; atención clínica; unidad funcional; historia clínica; fallecimiento (indicador de atención ''11'')', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'MUERTOS EN UN PERIODO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve registros de historia clínica donde INDICAPAC = ''11'' y FECHISPAC entre 2015-04-01 y 2015-06-30, enriquecidos con datos del paciente, diagnóstico y unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'MUERTOS EN UN PERIODO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.INDIAGNOS; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'MUERTOS EN UN PERIODO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'MUERTOS EN UN PERIODO';
GO
