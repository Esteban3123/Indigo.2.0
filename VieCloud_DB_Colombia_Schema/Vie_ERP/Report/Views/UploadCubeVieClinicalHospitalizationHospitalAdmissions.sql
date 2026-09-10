

--CREATE PROCEDURE [Hospitalization].[SP_INGRESOS_HOSPITALARIOS]
--DECLARE	@FECINI Datetime='2024-06-01';
--DECLARE	@FECFIN Datetime='2024-06-30';
--AS

CREATE view [Report].[UploadCubeVieClinicalHospitalizationHospitalAdmissions] AS

	WITH ESTANCIA_UNICA
	AS
	(

		SELECT EST.IPCODPACI ,EST.NUMINGRES ,MIN(ID) ID  
		FROM CHREGESTA AS EST GROUP BY EST.IPCODPACI ,EST.NUMINGRES

	)

	SELECT DISTINCT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		CASE pac.iptipodoc 
			WHEN 1 THEN 'CC'
			WHEN 2 THEN 'CE'
			WHEN 3 THEN 'TI'
			WHEN 4 THEN 'RC'
			WHEN 5 THEN 'PA'
			WHEN 6 THEN 'AS'
			WHEN 7 THEN 'MS'
			WHEN 8 THEN 'NU'
			WHEN 9 THEN 'CN'
			WHEN 10 THEN 'CD'
			WHEN 11 THEN 'SC' 
			WHEN 12 THEN 'PE' 
			WHEN 13 THEN 'PT'
			WHEN 14 THEN 'DE'
			WHEN 15 THEN 'SI' END AS 'TIPO IDENTIFICACION',--[TipoIdentificacion],
		EST.IPCODPACI AS 'NRO IDENTIFICACION',--[NroIdentificacion],
		PAC.IPNOMCOMP AS 'NOMBRE',--[Nombre],
		PAC.IPFECNACI AS 'FECHA NACIMIENTO',--[FechaNacimiento],
		FLOOR((CAST(CONVERT(VARCHAR(8), EST.FECINIEST, 112) AS INT)-CAST(CONVERT(VARCHAR(8), PAC.IPFECNACI, 112) AS INT)) / 10000) AS 'EDAD',--[Edad],
		HEA.Code AS 'CODIGO ENTIDAD',--[CodigoEntidad],
		HEA.Name AS 'ENTIDAD',--[Entidad],
		CGR.Name AS 'GRUPO ATENCION',--[GrupoAtencion],
		EST.NUMINGRES AS 'NRO INGRESO',--[NroIngreso],
		ing.ifechaing AS 'FECHA INGRESO',--[FechaIngreso],
		CEN.NOMCENATE AS 'CENTRO ATENCION',--[CentroAtencion], 
		UNI.UFUDESCRI AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
		CASE UNI.UFUTIPUNI 
			WHEN 1 THEN 'URGENCIAS' 
			WHEN 2 THEN 'HOSPITALIZACION' 
			WHEN 3 THEN 'APOYO DIAGNOSTICO' 
			WHEN 4 THEN 'APOYO TERAPEUTICO'
			WHEN 5 THEN 'UCI' 
			WHEN 6 THEN 'UCI' 
			WHEN 7 THEN 'UCI' 
			WHEN 8 THEN 'UCI' 
			WHEN 9 THEN 'UCI' 
			WHEN 10 THEN 'UCI' 
			WHEN 11 THEN 'UCI' 
			WHEN 12 THEN 'UNIDAD RENAL' 
			WHEN 13 THEN 'UNIDAD ONCOLOGICA' 
			WHEN 14 THEN 'UNIDAD MEDICINA NUCLEAR' 
			WHEN 15 THEN 'CONSULTA EXTERNA' 
			WHEN 16 THEN 'UNIDAD MENTAL' 
			WHEN 17 THEN 'UNIDAD DE QUEMADOS' 
			WHEN 18 THEN 'UNIDAD DE CUIDADOS PALATIVOS' 
			WHEN 19 THEN 'CIRUGIA' 
			WHEN 20 THEN 'LABORATORIOS' 
			WHEN 21 THEN 'CARDIOLOGIA NO INVASIVA' 
			WHEN 22 THEN 'CARDIOLOGIA INVASIVA' 
			WHEN 23 THEN 'GINECO OBSTETRICIA' 
			WHEN 24 THEN 'CONSULTA EXTERNA GINOCO OBSTETRICIA'
			WHEN 30 THEN 'OTRAS' 
			WHEN 31 THEN 'CONSULTA PRIORITARIA' END AS 'TIPO UF',--[TipoUF],
		RTRIM(CAM.DESCCAMAS) AS 'CAMA',--[Cama],
		CAST(EST.FECINIEST AS date ) AS 'FECHA INICIO ESTANCIA',--[FechaInicioEstancia],
		PRO.NOMMEDICO AS 'PROFESIONAL',--[Profesional],
		ESP.DESESPECI AS 'ESPECIALIDAD',--[Especialidad],
		ING.CODDIAING AS 'CIE10 INGRESO',--[CIE10 Ingreso],
		DIA.NOMDIAGNO AS 'DIAGNOSTICO INGRESO',--[DiagnosticoIngreso],
		ING.CODDIAEGR AS 'CIE10 EGRESO',--[CIE10 Egreso],
		DIAE.NOMDIAGNO AS 'DIAGNOSTICO EGRESO',--[DiagnosticoEgreso]
		CAST(EST.FECINIEST AS DATE) as 'FECHA BUSQUEDA',
		CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM CHREGESTA AS EST WITH (NOLOCK)
	JOIN ESTANCIA_UNICA AS EU WITH (NOLOCK) ON EU.ID=EST.ID
	JOIN INPACIENT AS PAC  WITH (NOLOCK) ON PAC.IPCODPACI =EST.IPCODPACI
	JOIN ADINGRESO AS ING  WITH (NOLOCK) ON ING.IPCODPACI =EST.IPCODPACI AND ING.NUMINGRES =EST.NUMINGRES
	JOIN CHCAMASHO CAM ON EST.CODICAMAS=CAM.CODICAMAS
	JOIN ADCENATEN AS CEN WITH (NOLOCK) ON CAM.CODCENATE =CEN.CODCENATE
	JOIN INUNIFUNC UNI WITH (NOLOCK) ON CAM.UFUCODIGO=UNI.UFUCODIGO
	JOIN Contract .HealthAdministrator HEA WITH (NOLOCK) ON ING.GENCONENTITY =HEA.ID
	JOIN Contract .CareGroup AS CGR WITH (NOLOCK) ON ING.GENCAREGROUP =CGR.Id 
	LEFT JOIN INPROFSAL PRO WITH (NOLOCK) ON EST.CODPROSAL =PRO.CODPROSAL 
	LEFT JOIN INESPECIA AS ESP WITH (NOLOCK) ON ESP.CODESPECI =EST.CODESPECI 
	LEFT JOIN INDIAGNOS AS DIA ON ING.CODDIAING =DIA.CODDIAGNO
	LEFT JOIN INDIAGNOS AS DIAE ON ING.CODDIAEGR  =DIAE.CODDIAGNO
	WHERE CAST(EST.FECINIEST AS DATE)>='2022-01-01'
	--CAST(EST.FECINIEST AS DATE) BETWEEN @FECINI AND @FECFIN

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los ingresos hospitalarios desde 2022-01-01 con datos demográficos del paciente, entidad responsable, ubicación (cama/unidad funcional/centro), profesional tratante y diagnósticos de ingreso/egreso, para alimentar un cubo analítico.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalizationHospitalAdmissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada estancia (CHREGESTA) debe tener paciente en INPACIENT y un ingreso correspondiente en ADINGRESO con la misma combinación paciente+número de ingreso; La cama de la estancia debe existir en CHCAMASHO y estar asociada a un centro de atención (ADCENATEN) y unidad funcional (INUNIFUNC); El ingreso debe tener entidad pagadora válida en Contract.HealthAdministrator y grupo de atención válido en Contract.CareGroup; Debe existir la función COMMON.GETDATE() y soporte de zona horaria ''Pakistan Standard Time''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalizationHospitalAdmissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Una sola estancia por paciente+ingreso (la de menor ID); Los tipos de UF 5 al 11 se agrupan bajo la categoría UCI; Profesional, especialidad y diagnósticos (ingreso/egreso) son opcionales (LEFT JOIN); el resto de relaciones son obligatorias (INNER JOIN); Sólo se reportan ingresos desde 2022-01-01 en adelante; Las fechas de inicio de estancia y de búsqueda se entregan a nivel DATE (sin hora)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalizationHospitalAdmissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de identificación; Edad; Ingreso hospitalario; Estancia; Entidad pagadora (Health Administrator); Grupo de atención; Centro de atención; Unidad funcional; Cama; UCI; Urgencias; Hospitalización; Profesional de la salud; Especialidad; Diagnóstico CIE10 de ingreso; Diagnóstico CIE10 de egreso', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalizationHospitalAdmissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalHospitalizationHospitalAdmissions: Devuelve sólo registros cuya fecha de inicio de estancia (EST.FECINIEST) sea >= ''2022-01-01''; [RETURN_RESULT] Report.UploadCubeVieClinicalHospitalizationHospitalAdmissions: Por cada combinación paciente+número de ingreso se selecciona una única estancia: la del menor ID (MIN(ID)) vía CTE ESTANCIA_UNICA; [RETURN_RESULT] Report.UploadCubeVieClinicalHospitalizationHospitalAdmissions: La edad se calcula como años completos entre IPFECNACI e FECINIEST usando formato YYYYMMDD y división por 10000; [RETURN_RESULT] Report.UploadCubeVieClinicalHospitalizationHospitalAdmissions: ULT_ACTUAL se fija a la fecha/hora actual convertida a zona horaria ''Pakistan Standard Time''; [RETURN_RESULT] Report.UploadCubeVieClinicalHospitalizationHospitalAdmissions: ID_COMPANY se obtiene del nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalizationHospitalAdmissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pac.iptipodoc en {1..15} → Mapea a códigos de tipo de identificación: 1=CC, 2=CE, 3=TI, 4=RC, 5=PA, 6=AS, 7=MS, 8=NU, 9=CN, 10=CD, 11=SC, 12=PE, 13=PT, 14=DE, 15=SI else NULL; si UNI.UFUTIPUNI según rango → Clasifica el tipo de unidad funcional: 1=URGENCIAS; 2=HOSPITALIZACION; 3=APOYO DIAGNOSTICO; 4=APOYO TERAPEUTICO; 5-11=UCI (todas se consolidan como UCI); 12=UNIDAD RENAL; 13=UNIDAD ONCOLOGICA; 14=MEDICINA NUCLEAR; 15=CONSULTA EXTERNA; 16=UNIDAD MENTAL; 17=QUEMADOS; 18=CUIDADOS PALIATIVOS; 19=CIRUGIA; 20=LABORATORIOS; 21=CARDIOLOGIA NO INVASIVA; 22=CARDIOLOGIA INVASIVA; 23=GINECO OBSTETRICIA; 24=CONSULTA EXTERNA GINECO; 30=OTRAS; 31=CONSULTA PRIORITARIA else NULL', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalizationHospitalAdmissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGESTA; dbo.INPACIENT; dbo.ADINGRESO; dbo.CHCAMASHO; dbo.ADCENATEN; dbo.INUNIFUNC; Contract.HealthAdministrator; Contract.CareGroup; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalizationHospitalAdmissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalizationHospitalAdmissions';
GO
