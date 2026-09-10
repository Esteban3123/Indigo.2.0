
CREATE view [Report].[UploadCubeVieClinicalSchedulingAvailabilityScheduling] as 
 

--CREATE PROCEDURE [Scheduling].[SP_DISPONIBILIDAD_AGENDA]
--DECLARE	@FechaInicio DATE='2024-06-01';
--DECLARE	@FechaFin DATE ='2024-06-30';
--AS
WITH CTE_AGENDA
AS
(
SELECT DISTINCT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
C.NOMCENATE AS 'CENTRO ATENCION',--[CentroAtencion],
CON.DESCRICON AS 'CONSULTORIO',--[Consultorio],
B.DESESPECI AS 'ESPECIALIDAD',--[Especialidad],
RTRIM(E.NOMMEDICO) AS 'PROFESIONAL',--[Profesional],
CONVERT(varchar,MIN(FECHORAIN),23) 'FECHA INICIAL AGENDA',--[FechaInicialAgenda], 
CONVERT(varchar,MIN(FECHORAIN),20) 'FECHA HORA INICIAL AGENDA',--[FechaHoraInicialAgenda], 
CONVERT(varchar,MAX(FECHORAFI),23) 'FECHA FINAL AGENDA',--[FechaFinalAgenda],
CONVERT(varchar,MAX(FECHORAFI),20) 'FECHA HORA FINAL AGENDA',--[FechaHoraFinalAgenda],
SUM(DATEDIFF(MINUTE,A.FECHORAIN,A.FECHORAFI)) AS 'MINUTOS DIA',--[MinutosDia],
SUM(ISNULL(GB.HORAS,0)) 'HORAS BLOQUEO',--[HorasBloqueo],
SUM(ISNULL(G.CITAS,0)) 'NRO CITAS DIA',--[NroCitasDia],
SUM(ISNULL(GC.CITASCUMPLIDAS,0)) 'CITAS CUMPLIDAS',--[CitasCumplidas],
SUM(ISNULL(GI.CITASINCUMPLIDAS,0)) 'CITAS INCUMPLIDAS',--[CitasIncumplidas],
CAST(A.FECHORAIN AS DATE) [FECHA BUSQUEDA],
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM AGAGEMEDC AS A
JOIN INPROFSAL AS E WITH (NOLOCK) ON A.CODPROSAL = E.CODPROSAL
JOIN INESPECIA AS B WITH (NOLOCK) ON A.CODESPECI = B.CODESPECI
JOIN ADCENATEN AS C WITH (NOLOCK) ON A.CODCENATE = C.CODCENATE
JOIN AGCONSULT AS CON WITH (NOLOCK) ON A.CODIGOCON =CON.CODIGOCON AND C.CODCENATE =CON.CODCENATE
LEFT JOIN (SELECT IDAGENDA, SUM(DATEDIFF(HOUR,FECHAINIBLOQUEO,FECHAFINBLOQUEO)) 'HORAS' FROM AGBLOQUEOPARCIAL WHERE ESTADO =1 GROUP BY IDAGENDA) AS GB ON GB.IDAGENDA =A.CODAUTONU
LEFT JOIN (SELECT COUNT(IDAGENDA) AS CITAS, IDAGENDA FROM AGASICITA WITH (NOLOCK) WHERE TIPSOLICITU =1 AND CODESTCIT <>4 GROUP BY IDAGENDA ) AS G ON G.IDAGENDA =A.CODAUTONU
LEFT JOIN (SELECT COUNT(IDAGENDA) AS CITASASIGNADAS, IDAGENDA FROM AGASICITA WITH (NOLOCK) WHERE TIPSOLICITU =1 AND CODESTCIT =0 GROUP BY IDAGENDA ) AS GA ON GA.IDAGENDA =A.CODAUTONU
LEFT JOIN (SELECT COUNT(IDAGENDA) AS CITASCUMPLIDAS, IDAGENDA FROM AGASICITA WITH (NOLOCK) WHERE TIPSOLICITU =1 AND CODESTCIT =1 GROUP BY IDAGENDA ) AS GC ON GC.IDAGENDA =A.CODAUTONU
LEFT JOIN (SELECT COUNT(IDAGENDA) AS CITASINCUMPLIDAS, IDAGENDA FROM AGASICITA WITH (NOLOCK) WHERE TIPSOLICITU =1 AND CODESTCIT =2 GROUP BY IDAGENDA) AS GI ON GI.IDAGENDA =A.CODAUTONU
GROUP BY C.NOMCENATE, CON.DESCRICON, B.DESESPECI, E.NOMMEDICO, YEAR(FECHORAIN), MONTH(FECHORAIN), DAY(FECHORAIN),YEAR(FECHORAFI), MONTH(FECHORAFI), DAY(FECHORAFI), CAST(A.FECHORAIN AS DATE)
)
SELECT * FROM CTE_AGENDA as AGE where CAST([FECHA BUSQUEDA] AS DATE)>='2022-01-01'
--CAST([FechaBusqueda] AS DATE) BETWEEN @FechaInicio AND @FechaFin
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de aplanado para carga a un cubo analítico (OLAP/reporting) que consolida la disponibilidad diaria de agendas médicas desde 2022-01-01. Agrega por día, profesional, especialidad, centro de atención y consultorio los minutos totales de agenda, horas de bloqueos parciales activos, y conteos de citas totales, cumplidas (estado 1) e incumplidas (estado 2), excluyendo citas canceladas (estado 4). El timestamp de actualización se registra en zona horaria de Pakistán.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingAvailabilityScheduling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingAvailabilityScheduling';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la disponibilidad y uso de las agendas médicas (minutos programados, bloqueos, citas asignadas/cumplidas/incumplidas) por centro, consultorio, especialidad y profesional para alimentar el cubo de reporting.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingAvailabilityScheduling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las agendas en AGAGEMEDC deben tener relacionados profesional (INPROFSAL), especialidad (INESPECIA), centro de atención (ADCENATEN) y consultorio (AGCONSULT) válidos para entrar al resultado (JOIN INNER).; El servidor debe reconocer la zona horaria ''Pakistan Standard Time'' para calcular ULT_ACTUAL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingAvailabilityScheduling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ID_COMPANY se deriva de DB_NAME() truncado a 9 caracteres, identificando la base/empresa origen.; MINUTOS DIA es la suma de DATEDIFF(MINUTE, FECHORAIN, FECHORAFI) por agenda agrupada.; Los conteos de citas y bloqueos faltantes se tratan como 0 (ISNULL).; Solo se consideran solicitudes con TIPSOLICITU = 1 para los conteos de citas.; Las citas en estado 4 quedan excluidas del total de NRO CITAS DIA.; ULT_ACTUAL se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; La agrupación se realiza por centro, consultorio, especialidad, profesional y por día (Y/M/D) tanto de inicio como de fin de agenda.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingAvailabilityScheduling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Agenda médica; Disponibilidad de agenda; Centro de atención; Consultorio; Especialidad; Profesional de salud; Cita médica; Citas cumplidas; Citas incumplidas; Bloqueo parcial de agenda', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingAvailabilityScheduling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalSchedulingAvailabilityScheduling: Devuelve únicamente filas cuya FECHA BUSQUEDA (CAST(A.FECHORAIN AS DATE)) sea >= ''2022-01-01''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingAvailabilityScheduling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AGBLOQUEOPARCIAL.ESTADO = 1 → Se suman como HORAS BLOQUEO (DATEDIFF HOUR entre FECHAINIBLOQUEO y FECHAFINBLOQUEO) para la agenda.; si AGASICITA.TIPSOLICITU = 1 AND CODESTCIT <> 4 → La cita cuenta dentro de NRO CITAS DIA (excluye estado 4, presumiblemente canceladas).; si AGASICITA.TIPSOLICITU = 1 AND CODESTCIT = 0 → Se contabiliza como cita asignada (CITASASIGNADAS, no expuesta en SELECT final).; si AGASICITA.TIPSOLICITU = 1 AND CODESTCIT = 1 → Se contabiliza como CITAS CUMPLIDAS.; si AGASICITA.TIPSOLICITU = 1 AND CODESTCIT = 2 → Se contabiliza como CITAS INCUMPLIDAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingAvailabilityScheduling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGAGEMEDC; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADCENATEN; dbo.AGCONSULT; dbo.AGBLOQUEOPARCIAL; dbo.AGASICITA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingAvailabilityScheduling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingAvailabilityScheduling';
GO
