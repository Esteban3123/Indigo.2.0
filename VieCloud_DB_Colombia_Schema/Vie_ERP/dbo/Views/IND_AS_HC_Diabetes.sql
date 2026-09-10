
CREATE VIEW [dbo].[IND_AS_HC_Diabetes]
AS
SELECT DISTINCT NUMINGRES AS Ingreso, IPCODPACI AS Identificacion, CODDIAGNO AS CIE10
FROM            dbo.INDIAGNOH
WHERE        (CODDIAGNO BETWEEN 'E100' AND 'E129') AND (FECDIAGNO >= '01/01/2016 00:00:00') OR
                         (CODDIAGNO BETWEEN 'E140' AND 'E149') OR
                         (CODDIAGNO = 'E232') OR
                         (CODDIAGNO = 'N251') OR
                         (CODDIAGNO BETWEEN 'O240' AND 'O244') OR
                         (CODDIAGNO = 'O249') OR
                         (CODDIAGNO = 'P702')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que identifica pacientes con diagnóstico de diabetes u otras condiciones relacionadas (diabetes mellitus tipos 1 y 2, diabetes gestacional, diabetes neonatal, diabetes insípida nefrógena, entre otras), filtrando registros de la historia clínica con códigos CIE-10 del rango E100-E129, E140-E149, E232, N251, O240-O244, O249 y P702 registrados desde enero de 2016. Consulta la tabla de diagnósticos clínicos (INDIAGNOH) y expone el número de ingreso, la cédula o identificación del paciente y el código de diagnóstico CIE-10 correspondiente. Sirve como insumo para indicadores asistenciales, reportes epidemiológicos y seguimiento de pacientes diabéticos dentro del sistema de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AS_HC_Diabetes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AS_HC_Diabetes';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los ingresos y pacientes con diagnóstico CIE-10 asociado a diabetes (incluida la gestacional y neonatal) para alimentar el indicador de atención en salud de diabetes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_Diabetes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla de diagnósticos debe contener los códigos CIE-10 normalizados sin separadores (p. ej. ''E100'', ''O244'').; El campo de fecha de diagnóstico debe estar poblado para los registros del rango E10–E12 a fin de aplicar el filtro temporal.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_Diabetes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen diagnósticos cuyo código CIE-10 corresponde a los grupos asociados a diabetes: E10–E12 (desde 2016-01-01), E14, E23.2, N25.1, O24.0–O24.4, O24.9 y P70.2.; Para los códigos E100–E129 se exige fecha de diagnóstico igual o posterior al 01/01/2016; los demás rangos no tienen restricción temporal.; El resultado se entrega sin duplicados (DISTINCT) por la combinación ingreso/identificación/CIE-10.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_Diabetes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Diagnóstico; CIE-10; Diabetes mellitus; Diabetes gestacional; Historia clínica; Ingreso del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_Diabetes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INDIAGNOH: Devuelve ingreso, identificación y código CIE-10 cuando el diagnóstico está entre E100–E129 con fecha ≥ 2016-01-01, o entre E140–E149, o es E232, N251, O240–O244, O249 o P702.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_Diabetes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INDIAGNOH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_Diabetes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_Diabetes';
GO
