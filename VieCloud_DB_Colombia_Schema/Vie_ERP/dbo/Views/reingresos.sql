
CREATE VIEW [dbo].[reingresos]
AS
SELECT        TOP (100) PERCENT IREINGRES AS Expr1, NUMINGRES AS Expr2, IFECHAING AS Expr3, NUMINGREI
FROM            dbo.ADINGRESO
WHERE        (IREINGRES = 1) AND (IFECHAING >= CONVERT(DATETIME, '2015-08-01 00:00:00', 102))
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Filtra la tabla de ingresos/admisiones para identificar episodios marcados como reingresos (`IREINGRES = 1`) ocurridos a partir del 1 de agosto de 2015. Expone el indicador de reingreso, el número de ingreso actual, la fecha de ingreso y el número del ingreso previo relacionado (`NUMINGREI`). Sirve como vista de consulta o reporte para analizar pacientes que fueron admitidos nuevamente tras un episodio anterior registrado en el sistema.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'reingresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'reingresos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las admisiones que corresponden a reingresos de pacientes ocurridos a partir del 1 de agosto de 2015, vinculándolas con el ingreso original.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'reingresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen admisiones marcadas como reingreso (IREINGRES = 1).; Solo se incluyen ingresos cuya fecha sea igual o posterior al 1 de agosto de 2015.; Cada fila expone, además del indicador y fecha del ingreso actual, el número del ingreso previo asociado (NUMINGREI).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'reingresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'reingreso; ingreso/admisión de paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'reingresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'reingresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'reingresos';
GO
