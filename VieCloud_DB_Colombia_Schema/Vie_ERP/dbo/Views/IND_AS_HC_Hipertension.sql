
CREATE VIEW [dbo].[IND_AS_HC_Hipertension]
AS
SELECT DISTINCT NUMINGRES AS Ingreso, IPCODPACI AS Identificacion, CODDIAGNO AS CIE10
FROM            dbo.INDIAGNOH
WHERE        (CODDIAGNO BETWEEN 'I10' AND 'I159') AND (FECDIAGNO >= '01/01/2016 00:00:00')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de pacientes con diagnóstico de hipertensión arterial registrado en la historia clínica desde el 1 de enero de 2016. Filtra los diagnósticos de la tabla INDIAGNOH cuyo código CIE-10 se encuentre en el rango I10–I159, que corresponde al grupo de hipertensión según la clasificación internacional de enfermedades. Para cada registro expone el número de ingreso o atención, la cédula o identificación del paciente y el código diagnóstico CIE-10 confirmado. Sirve como base para indicadores asistenciales, reportes epidemiológicos y seguimiento de pacientes hipertensos en la institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AS_HC_Hipertension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AS_HC_Hipertension';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Identifica ingresos y pacientes con diagnósticos de hipertensión arterial (CIE-10 I10–I159) registrados desde el 01/01/2016, para indicadores asistenciales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_Hipertension';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de diagnósticos cargados en la historia clínica con código CIE-10 y fecha de diagnóstico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_Hipertension';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo incluye diagnósticos cuyo CIE-10 cae en el rango lexicográfico ''I10''..''I159'' (familia de hipertensión); Excluye diagnósticos previos al 01/01/2016; Resultados únicos por la combinación ingreso/identificación/CIE-10 (DISTINCT)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_Hipertension';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Hipertensión arterial; Diagnóstico CIE-10; Ingreso hospitalario; Identificación de paciente; Indicador asistencial', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_Hipertension';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INDIAGNOH: Cuando CODDIAGNO está entre ''I10'' y ''I159'' y FECDIAGNO >= ''2016-01-01'' → retorna ingreso, identificación del paciente y código CIE-10 (sin duplicados)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_Hipertension';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INDIAGNOH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_Hipertension';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_Hipertension';
GO
