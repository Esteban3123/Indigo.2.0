

CREATE VIEW [dbo].[RELACION EVOLUCIONES]
AS
SELECT        TOP (100) PERCENT dbo.HCHISPACA.IPCODPACI AS DOCUMENTO, dbo.INPACIENT.IPNOMCOMP AS [NOMBRE PACIENTE], 
                         dbo.HCHISPACA.NUMINGRES AS INGRESO, 
                         CASE WHEN IDETIPHIS = 'HCURGEVO1' THEN 'HISTORIA CLINICA DE EVOLUCION' WHEN IDETIPHIS = 'HCURGING1' THEN 'HISTORIA CLINICA DE INGRESO' WHEN IDETIPHIS
                          = 'HCNOTEVO1' THEN 'NOTAS RAPIDAS DE EVOLUCION' END AS [TIPO DE HISTORIA], dbo.HCHISPACA.FECHISPAC AS [FEC HISTORIA], 
                         dbo.HCHISPACA.CODPROSAL AS [COD PROF.], dbo.INPROFSAL.NOMMEDICO AS [NOM PROF], dbo.HCHISPACA.NUMEFOLIO AS FOLIO, 
                         dbo.INUNIFUNC.UFUDESCRI AS [UNIDAD FUNCIONAL]
FROM            dbo.HCHISPACA INNER JOIN
                         dbo.INPACIENT ON dbo.HCHISPACA.IPCODPACI = dbo.INPACIENT.IPCODPACI INNER JOIN
                         dbo.INPROFSAL ON dbo.HCHISPACA.CODPROSAL = dbo.INPROFSAL.CODPROSAL INNER JOIN
                         dbo.INUNIFUNC ON dbo.HCHISPACA.UFUCODIGO = dbo.INUNIFUNC.UFUCODIGO
WHERE        (dbo.HCHISPACA.FECHISPAC BETWEEN '01/03/2013 00:00:00' AND '31/03/2013 23:59:59')
ORDER BY [FEC HISTORIA] DESC
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de evoluciones y notas clínicas registradas en la historia clínica, combinando datos del paciente (cédula y nombre completo), el número de ingreso, el tipo de documento clínico (historia de evolución, historia de ingreso o notas rápidas de evolución), la fecha del folio, el profesional de la salud que lo generó y la unidad funcional donde se atendió. Integra las tablas de historias clínicas (HCHISPACA), el maestro de pacientes (INPACIENT), el maestro de profesionales (INPROFSAL) y el catálogo de unidades funcionales (INUNIFUNC). Sirve como reporte de auditoría y seguimiento clínico para revisar qué registros de evolución médica fueron generados por ingreso, profesional y servicio. Nota: actualmente tiene un filtro fijo de fechas (marzo 2013) que limita su uso dinámico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'RELACION EVOLUCIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'RELACION EVOLUCIONES';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las historias clínicas (ingreso, evolución y notas rápidas) registradas durante marzo de 2013, junto con datos del paciente, profesional tratante y unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RELACION EVOLUCIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen historias clínicas en HCHISPACA con fechas dentro del rango marzo 2013.; Cada historia debe tener paciente, profesional y unidad funcional referenciados existentes (INNER JOIN obligatorio).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RELACION EVOLUCIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen historias del periodo fijo de marzo 2013 (rango hardcodeado).; Se excluyen historias sin paciente, profesional o unidad funcional asociados por uso de INNER JOIN.; Únicamente se traducen tres tipos de historia; otros tipos aparecen sin descripción.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RELACION EVOLUCIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Historia clínica de ingreso; Historia clínica de evolución; Notas rápidas de evolución; Profesional de la salud; Unidad funcional; Folio clínico; Ingreso hospitalario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RELACION EVOLUCIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCHISPACA: Devuelve historias clínicas cuya FECHISPAC esté entre 01/03/2013 00:00:00 y 31/03/2013 23:59:59, ordenadas descendentemente por fecha.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RELACION EVOLUCIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IDETIPHIS = ''HCURGEVO1'' → Se etiqueta como ''HISTORIA CLINICA DE EVOLUCION''.; si IDETIPHIS = ''HCURGING1'' → Se etiqueta como ''HISTORIA CLINICA DE INGRESO''.; si IDETIPHIS = ''HCNOTEVO1'' → Se etiqueta como ''NOTAS RAPIDAS DE EVOLUCION''. else Tipo de historia queda en NULL (no se traduce).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RELACION EVOLUCIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.INPROFSAL; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RELACION EVOLUCIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RELACION EVOLUCIONES';
GO
