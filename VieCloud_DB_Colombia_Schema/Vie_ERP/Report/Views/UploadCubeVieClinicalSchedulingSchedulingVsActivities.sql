

CREATE view [Report].[UploadCubeVieClinicalSchedulingSchedulingVsActivities] as 
--CREATE PROCEDURE [Scheduling].[SP_AGENDA_VS_ACTIVIDADES]
--DECLARE	@FechaInicio DATE='2024-06-01';
--DECLARE	@FechaFin DATE ='2024-06-30';
--AS

SELECT DISTINCT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
	CA.NOMCENATE AS 'CENTRO ATENCION',--[CentroAtencion],
	RTRIM(B.DESESPECI) AS 'ESPECIALIDAD',--[Especialidad],
	RTRIM(E.NOMMEDICO) AS 'NOMBRE MEDICO',--[NombreMedico],
	C.FECHORAIN AS 'FECHA INICIAL',--[FechaInicial],
	C.FECHORAFI as 'FECHA FINAL',--[FechaFinal],
	ACT.CODACTMED AS 'CODIGO ACTIVIDAD',--[CodActividad],
	Act.DESACTMED AS 'DESCRIPCION ACTIVIDAD',--[DescripcionActividad],
	act.DURAACTIV AS 'DURACION ACTIVIDAD',--[DuracionActividad]
    CAST(C.FECHORAIN AS DATE) [FECHA BUSQUEDA],
    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM DBO.AGAGEMEDC AS C 
INNER JOIN .DBO.AGAGEMEDD AS D ON C.CODAUTONU =D.CODAUTONU 
INNER JOIN .INPROFSAL AS E WITH (NOLOCK) ON C.CODPROSAL = E.CODPROSAL
INNER JOIN .INESPECIA AS B WITH (NOLOCK) ON C.CODESPECI = B.CODESPECI
INNER JOIN .ADCENATEN AS CA WITH (NOLOCK) ON C.CODCENATE = CA.CODCENATE
INNER JOIN .DBO.AGACTIMED AS ACT ON ACT.CODACTMED =D.CODACTMED 
WHERE  CAST(C.FECHORAIN AS DATE)>='2022-01-01'
--CAST(C.FECHORAIN AS DATE)  BETWEEN @FechaInicio AND @FechaFin
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que cruza agendas médicas con actividades clínicas programadas, consolidando información de especialidad, profesional de salud y centro de atención desde registros con fecha de inicio a partir del 2022-01-01. Aplana datos de cabecera y detalle de agenda junto con catálogos de actividades médicas, especialidades y centros, para alimentar un cubo analítico que compara agendamiento versus actividades realizadas.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSchedulingVsActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSchedulingVsActivities';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, para alimentación de cubo OLAP, el cruce entre la programación de agenda médica y las actividades médicas asociadas, con datos de centro, especialidad, profesional, horarios y duración desde 2022-01-01.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSchedulingVsActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas AGAGEMEDC, AGAGEMEDD, INPROFSAL, INESPECIA, ADCENATEN y AGACTIMED deben existir y ser accesibles en la base de datos actual.; El servidor debe reconocer la zona horaria ''Pakistan Standard Time'' para el cálculo de ULT_ACTUAL.; Cada registro de agenda (AGAGEMEDC) debe tener correspondencia en INPROFSAL, INESPECIA y ADCENATEN; de lo contrario se excluye por los INNER JOIN.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSchedulingVsActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se filtran únicamente agendas cuya fecha inicial (FECHORAIN) sea igual o posterior al 2022-01-01.; Se devuelven filas DISTINCT, evitando duplicados en la combinación de centro, especialidad, médico, horarios y actividad.; El identificador de compañía corresponde al nombre de la base de datos actual (DB_NAME()) truncado a 9 caracteres.; La marca de última actualización (ULT_ACTUAL) se calcula con la hora actual convertida a la zona horaria ''Pakistan Standard Time''.; Las relaciones entre cabecera de agenda (AGAGEMEDC) y detalle (AGAGEMEDD) se hacen por CODAUTONU; cada actividad se enriquece con su descripción y duración desde AGACTIMED.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSchedulingVsActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de atención; Especialidad médica; Profesional de salud / Médico; Agenda médica; Actividad médica; Duración de actividad', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSchedulingVsActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalSchedulingSchedulingVsActivities: Devuelve un conjunto DISTINCT con centro de atención, especialidad, médico, fecha/hora inicial y final de agenda, código y descripción de la actividad y su duración, filtrando CAST(C.FECHORAIN AS DATE) >= ''2022-01-01''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSchedulingVsActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.AGAGEMEDC; DBO.AGAGEMEDD; DBO.INPROFSAL; DBO.INESPECIA; DBO.ADCENATEN; DBO.AGACTIMED', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSchedulingVsActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSchedulingVsActivities';
GO
