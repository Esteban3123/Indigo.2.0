
CREATE VIEW [dbo].[MORBILIDAD CONS EXTERNA]
AS
SELECT     TOP (100) PERCENT dbo.INDIAGNOP.CODDIAGNO, dbo.INDIAGNOS.NOMDIAGNO, COUNT(*) AS Expr1
FROM         dbo.INDIAGNOP INNER JOIN
                      dbo.ADINGRESO ON dbo.INDIAGNOP.NUMINGRES = dbo.ADINGRESO.NUMINGRES INNER JOIN
                      dbo.INDIAGNOS ON dbo.INDIAGNOP.CODDIAGNO = dbo.INDIAGNOS.CODDIAGNO
WHERE     (dbo.ADINGRESO.TIPOINGRE = 1) AND (dbo.ADINGRESO.IFECHAING >= CONVERT(DATETIME, '2013-09-01 00:00:00', 102)) AND (dbo.INDIAGNOP.CODDIAPRI = 1) AND 
                      (dbo.ADINGRESO.IFECHAING <= CONVERT(DATETIME, '2013-09-30 00:00:00', 102))
GROUP BY dbo.INDIAGNOP.CODDIAGNO, dbo.INDIAGNOS.NOMDIAGNO
ORDER BY Expr1 DESC
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de morbilidad para consulta externa: consolida los diagnósticos principales (CIE-10) registrados en atenciones de consulta externa (tipo de ingreso 1), mostrando el código y nombre del diagnóstico junto con la cantidad de veces que fue registrado como diagnóstico primario. Cruza los diagnósticos por paciente (INDIAGNOP), los ingresos o admisiones (ADINGRESO) y el catálogo maestro de diagnósticos (INDIAGNOS). Originalmente fue construida para el período septiembre 2013, ordenando los diagnósticos de mayor a menor frecuencia; sirve como base para reportes de morbilidad, estadísticas epidemiológicas y análisis de patologías más frecuentes en consulta externa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'MORBILIDAD CONS EXTERNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'MORBILIDAD CONS EXTERNA';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los diagnósticos principales más frecuentes en atenciones de consulta externa durante septiembre de 2013, ordenados por número de ocurrencias para análisis de morbilidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'MORBILIDAD CONS EXTERNA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen ingresos con TIPOINGRE = 1 (consulta externa) en el rango de fechas; Los diagnósticos están vinculados a un ingreso vía NUMINGRES; El catálogo INDIAGNOS contiene los códigos CIE-10 referenciados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'MORBILIDAD CONS EXTERNA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo cuenta diagnósticos principales (CODDIAPRI=1), excluyendo secundarios; El periodo de análisis está fijo (hardcoded) a septiembre 2013; Solo considera atenciones de tipo consulta externa (TIPOINGRE=1); Cada fila representa un diagnóstico único con su frecuencia agregada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'MORBILIDAD CONS EXTERNA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'morbilidad; consulta externa; diagnóstico principal; CIE-10; ingreso/admisión; estadística epidemiológica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'MORBILIDAD CONS EXTERNA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve código de diagnóstico, nombre y conteo de ocurrencias agrupados, ordenados descendentemente por frecuencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'MORBILIDAD CONS EXTERNA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ADINGRESO.TIPOINGRE = 1 → Solo se incluyen ingresos clasificados como consulta externa; si INDIAGNOP.CODDIAPRI = 1 → Solo se considera el diagnóstico marcado como principal; si IFECHAING entre 2013-09-01 y 2013-09-30 → Solo se contabilizan atenciones del mes de septiembre 2013', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'MORBILIDAD CONS EXTERNA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INDIAGNOP; dbo.ADINGRESO; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'MORBILIDAD CONS EXTERNA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'MORBILIDAD CONS EXTERNA';
GO
