
CREATE VIEW [dbo].[ATENCIONES POR DIAGNOSTICO]
AS
SELECT        TOP (100) PERCENT dbo.HCHISPACA.CODDIAGNO AS Expr1, dbo.HCHISPACA.IPCODPACI, dbo.HCHISPACA.NUMINGRES, dbo.INPACIENT.IPNOMCOMP, 
                         dbo.INPACIENT.NUMCARPET, dbo.HCHISPACA.FECHISPAC, dbo.INPACIENT.IPDIRECCI, dbo.INPACIENT.IPTELEFON, dbo.HCHISPACA.NUMEFOLIO
FROM            dbo.HCHISPACA INNER JOIN
                         dbo.INPACIENT ON dbo.HCHISPACA.IPCODPACI = dbo.INPACIENT.IPCODPACI
WHERE        (dbo.HCHISPACA.CODDIAGNO >= 'X200' AND dbo.HCHISPACA.CODDIAGNO <= 'X299')
ORDER BY dbo.HCHISPACA.FECHISPAC
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de consulta clínica que cruza historias clínicas con el maestro de pacientes para listar atenciones cuyos diagnósticos se encuentran en el rango de códigos X200–X299, correspondiente a exposición a factores externos según clasificación CIE. Filtra registros de la tabla de folios clínicos y retorna datos de identificación, contacto y ubicación del paciente, junto con la fecha de atención y número de folio, ordenados cronológicamente. Sirve como apoyo a reporting epidemiológico o vigilancia de eventos por causas externas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCIONES POR DIAGNOSTICO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCIONES POR DIAGNOSTICO';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista atenciones clínicas de pacientes cuyo diagnóstico se encuentra dentro del rango CIE-10 X200–X299, junto con datos demográficos básicos del paciente, ordenadas por fecha de la historia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCIONES POR DIAGNOSTICO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada registro de historia clínica debe tener un paciente asociado en el maestro de pacientes (INNER JOIN por código de paciente).; El campo de diagnóstico debe estar codificado de forma comparable lexicográficamente al rango ''X200''..''X299'' (códigos CIE-10).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCIONES POR DIAGNOSTICO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen atenciones cuyo diagnóstico cae en el rango CIE-10 X20–X29 (contacto traumático con animales/insectos, según CIE-10).; Se excluyen atenciones sin paciente registrado en el maestro (INNER JOIN).; Los resultados se entregan ordenados ascendentemente por la fecha de la historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCIONES POR DIAGNOSTICO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; historia clínica; diagnóstico (CIE-10); atención; folio; ingreso hospitalario; carpeta del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCIONES POR DIAGNOSTICO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultado de la vista: Cuando el código de diagnóstico está entre ''X200'' y ''X299'' y existe paciente relacionado, se retorna la atención con folio, fecha, ingreso y datos del paciente (nombre, carpeta, dirección, teléfono).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCIONES POR DIAGNOSTICO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODDIAGNO >= ''X200'' AND CODDIAGNO <= ''X299'' → La atención se incluye en el resultado. else La atención se excluye del resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCIONES POR DIAGNOSTICO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCIONES POR DIAGNOSTICO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCIONES POR DIAGNOSTICO';
GO
