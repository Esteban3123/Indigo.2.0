

CREATE VIEW [dbo].[Oportunidad Urgencias]
AS
SELECT        TOP (100) PERCENT dbo.ADTRIAGEU.IPCODPACI AS DOCUMENTO, dbo.INPACIENT.IPNOMCOMP AS NOMBRE, INUNIFUNC_1.UFUDESCRI AS [UNIDAD TRIAGE], 
                         dbo.ADCONTURG.IPFECLLEGA AS [FECHA ADMISIONES], dbo.ADTRIAGEU.TRIAFECHA AS [FECHA DE TRIAGE], dbo.HCURGING1.FECINIATE AS [FEC. ATENCION INI], 
                         dbo.ADTRIAGEU.NUMINGRES AS INGRESO, dbo.ADTRIAGEU.TRIAGECLA AS [CLASIFICACION TRIAGE], dbo.ADTRIAGEU.CODPROSAL AS [COD PROF TRIAGE], 
                         dbo.INUNIFUNC.UFUDESCRI AS [UNIDAD ATEN. INICI], INPROFSAL_1.NOMMEDICO AS [MED. TRIAGE], dbo.HCURGING1.CODPROSAL AS [PROF ATENCION INI.], 
                         dbo.INPROFSAL.NOMMEDICO AS [MED. ATENCION INI.]
FROM            dbo.ADCONTURG LEFT OUTER JOIN
                         dbo.INUNIFUNC AS INUNIFUNC_1 ON dbo.ADCONTURG.UFUCODIGO = INUNIFUNC_1.UFUCODIGO LEFT OUTER JOIN
                         dbo.ADTRIAGEU LEFT OUTER JOIN
                         dbo.INPROFSAL AS INPROFSAL_1 ON dbo.ADTRIAGEU.CODPROSAL = INPROFSAL_1.CODPROSAL LEFT OUTER JOIN
                         dbo.INPACIENT ON dbo.ADTRIAGEU.IPCODPACI = dbo.INPACIENT.IPCODPACI ON dbo.ADCONTURG.CODCONCEC = dbo.ADTRIAGEU.CODCONCEC LEFT OUTER JOIN
                         dbo.INPROFSAL INNER JOIN
                         dbo.HCURGING1 ON dbo.INPROFSAL.CODPROSAL = dbo.HCURGING1.CODPROSAL RIGHT OUTER JOIN
                         dbo.INUNIFUNC ON dbo.HCURGING1.UFUCODIGO = dbo.INUNIFUNC.UFUCODIGO ON dbo.ADTRIAGEU.NUMINGRES = dbo.HCURGING1.NUMINGRES
WHERE        (dbo.ADTRIAGEU.TRIAFECHA BETWEEN '01/05/2013 00:00:00' AND '31/05/2013 23:59:59')
ORDER BY [FECHA ADMISIONES] DESC
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de oportunidad de atención en urgencias que integra, para cada paciente, la cadena completa desde su llegada hasta el inicio de la consulta médica: fecha de llegada a admisiones, fecha y clasificación del triage, unidad donde fue triado, profesional que realizó el triage, y fecha e inicio de la atención médica junto con el profesional tratante. Cruza los registros de llamados y convocatorias (ADCONTURG), la valoración de triage (ADTRIAGEU), la historia clínica inicial de urgencias (HCURGING1), el maestro de pacientes (INPACIENT), el maestro de profesionales (INPROFSAL) y el catálogo de unidades funcionales (INUNIFUNC) para permitir medir tiempos de espera y oportunidad en el servicio de urgencias. Sirve para indicadores de calidad y auditoría de oportunidad en urgencias, mostrando cuánto tardó cada paciente en ser triado y atendido desde su llegada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Oportunidad Urgencias';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Oportunidad Urgencias';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolidar, para los triages de urgencias de mayo de 2013, la trazabilidad entre admisión, triage y atención inicial: paciente, unidades funcionales, profesionales y fechas clave.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Oportunidad Urgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros de triage de urgencias en el rango de fechas configurado (mayo de 2013).; Las tablas de catálogo (unidades funcionales, profesionales, pacientes) deben estar pobladas para resolver las descripciones y nombres asociados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Oportunidad Urgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se reportan únicamente eventos de triage cuya fecha esté dentro de mayo de 2013 (01/05/2013 al 31/05/2013).; Cada fila corresponde a un contacto de urgencias y, opcionalmente, su triage, paciente, profesional de triage, atención inicial de urgencias y unidades funcionales asociadas (relaciones por LEFT/RIGHT OUTER JOIN).; El cruce entre triage y atención inicial de urgencias se realiza por el número de ingreso.; Los resultados se presentan ordenados de forma descendente por la fecha de llegada/admisión a urgencias.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Oportunidad Urgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; urgencias; triage; clasificación de triage; ingreso; unidad funcional; profesional de la salud; atención inicial de urgencias; admisión', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Oportunidad Urgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando TRIAFECHA está entre 01/05/2013 y 31/05/2013, se retorna la fila combinada de admisión de urgencias, triage, paciente, profesional de triage, atención inicial y sus unidades funcionales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Oportunidad Urgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONTURG; dbo.INUNIFUNC; dbo.ADTRIAGEU; dbo.INPROFSAL; dbo.INPACIENT; dbo.HCURGING1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Oportunidad Urgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Oportunidad Urgencias';
GO
