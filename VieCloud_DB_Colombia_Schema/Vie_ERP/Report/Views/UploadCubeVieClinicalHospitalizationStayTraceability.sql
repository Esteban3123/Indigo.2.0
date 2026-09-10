

--CREATE PROCEDURE [Hospitalization].[SP_TRAZABILIDAD_ESTANCIAS]
--	@IDENTIFICA VARCHAR(20)
--AS

CREATE view [Report].[UploadCubeVieClinicalHospitalizationStayTraceability] AS

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
		/*
		CASE PAC.IPTIPODOC WHEN '1' THEN 'CEDULA DE CIUDADANIA' WHEN '2' THEN 'CEDULA DE EXTRANJERIA' WHEN '3' THEN 'TARJETA DE IDENTIDAD' WHEN '4' THEN 'REGISTRO CIVIL' WHEN '5' THEN 'PASAPORTE'
		WHEN '6' THEN 'ADULTO SIN IDENTIFICACION' WHEN '7' THEN 'MENOR SIN IDENTIFICACION' WHEN '8' THEN 'NUMERO UNICO DE IDENTIFICACIÒN' WHEN '9' THEN 'CERTIFICADO NACIDO VIVO' WHEN '10' THEN 'CARNET DIPLOMATICO'
		WHEN '11' THEN 'SALVOCONDUCTO' WHEN '12' THEN 'PERMISO ESPECIAL DE PERMANENCIA' END AS 'DESCRIPCION IDENTIFICACION',
		*/
		EST.IPCODPACI AS 'NRO IDENTIFICACION',--[NroIdentificacion],
		/*
		PAC.IPPRINOMB 'PRIMER NOMBRE',
		PAC.IPSEGNOMB 'SEGUNDO NOMBRE',
		PAC.IPPRIAPEL 'PRIMER APELLIDO',
		PAC.IPSEGAPEL 'SEGUNDO APELLIDO',
		*/
		PAC.IPNOMCOMP AS 'NOMBRE',--[Nombre]
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
		--CAM.NUMCAMHOS 'CAMA',
		RTRIM(CAM.DESCCAMAS) AS 'CAMA',--[Cama],
		CAST(EST.FECINIEST AS date ) AS 'FECHA INICIO ESTANCIA',--[FechaInicioEstancia],
		CAST(EST.FECFINEST AS date ) AS 'FECHA FIN ESTANCIA',--[FechaFinEstancia],
		CASE 
			WHEN EST.FECFINEST = '1900-01-01 00:00:00.000' THEN DATEDIFF(DAY,EST.FECINIEST, GETDATE()) 
			ELSE DATEDIFF(DAY,EST.FECINIEST, EST.FECFINEST) END AS 'DIA ESTANCIA',--[DiasEstancia],
		PRO.NOMMEDICO AS 'PROFESIONAL',--[Profesional],
		ESP.DESESPECI AS 'ESPECIALIDAD',--[Especialidad],
		ING.CODDIAING AS 'CIE10 INGRESO',--[CIE10 Ingreso],
		DIA.NOMDIAGNO AS 'DIAGNOSTICO INGRESO',--[DiagnosticoIngreso],
		ING.CODDIAEGR AS 'CIE10 EGRESO',--[CIE10 Egreso],
		DIAE.NOMDIAGNO AS 'DIAGNOSTICO EGRESO',--[DiagnosticoEgreso],
		cast(ing.ifechaing as date) as 'FECHA BUSQUEDA',
		CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM CHREGESTA AS EST WITH (NOLOCK)
	INNER JOIN INPACIENT AS PAC  WITH (NOLOCK) ON PAC.IPCODPACI =EST.IPCODPACI
	INNER JOIN ADINGRESO AS ING  WITH (NOLOCK) ON ING.IPCODPACI =EST.IPCODPACI AND ING.NUMINGRES =EST.NUMINGRES
	INNER JOIN CHCAMASHO CAM ON EST.CODICAMAS=CAM.CODICAMAS
	INNER JOIN ADCENATEN AS CEN WITH (NOLOCK) ON CAM.CODCENATE =CEN.CODCENATE
	INNER JOIN INUNIFUNC UNI WITH (NOLOCK) ON CAM.UFUCODIGO=UNI.UFUCODIGO
	INNER JOIN Contract .HealthAdministrator HEA WITH (NOLOCK) ON ING.GENCONENTITY =HEA.ID
	INNER JOIN Contract .CareGroup AS CGR WITH (NOLOCK) ON ING.GENCAREGROUP =CGR.Id 
	LEFT JOIN INPROFSAL PRO WITH (NOLOCK) ON EST.CODPROSAL =PRO.CODPROSAL 
	LEFT JOIN INESPECIA AS ESP WITH (NOLOCK) ON ESP.CODESPECI =EST.CODESPECI 
	LEFT JOIN INDIAGNOS AS DIA ON ING.CODDIAING =DIA.CODDIAGNO
	LEFT JOIN INDIAGNOS AS DIAE ON ING.CODDIAEGR  =DIAE.CODDIAGNO
	--WHERE est.ipcodpaci = @identifica
	--ORDER BY EST.IPCODPACI ,EST.FECINIEST

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la trazabilidad de estancias hospitalarias por paciente, consolidando datos demográficos, administrativos, clínicos y de ubicación para alimentar un cubo de reporting.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalizationStayTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en CHREGESTA con paciente (IPCODPACI), ingreso (NUMINGRES) y cama (CODICAMAS) válidos.; Las camas deben tener centro de atención (CODCENATE) y unidad funcional (UFUCODIGO) asociados.; El ingreso debe tener entidad administradora (GENCONENTITY) y grupo de atención (GENCAREGROUP) registrados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalizationStayTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La edad se calcula en años completos comparando yyyymmdd de fecha inicio de estancia contra fecha de nacimiento (FLOOR de la diferencia / 10000).; Una fecha fin de estancia igual a ''1900-01-01'' se interpreta como estancia aún abierta.; El identificador de compañía se obtiene del nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres.; La marca de última actualización se construye en zona horaria ''Pakistan Standard Time''.; Solo se incluyen estancias con paciente, ingreso, cama, centro de atención, unidad funcional, entidad y grupo de atención existentes (INNER JOIN); profesional, especialidad y diagnósticos son opcionales (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalizationStayTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de identificación; Ingreso hospitalario; Estancia hospitalaria; Cama; Centro de atención; Unidad funcional; Entidad administradora de salud; Grupo de atención; Profesional de salud; Especialidad médica; Diagnóstico CIE-10 de ingreso y egreso; Días de estancia; UCI; Urgencias; Hospitalización', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalizationStayTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalHospitalizationStayTraceability: Devuelve filas DISTINCT por estancia con datos del paciente, ingreso, cama, UF, profesional, diagnósticos de ingreso/egreso y días de estancia.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalizationStayTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pac.iptipodoc según valor 1..15 → Mapea a códigos de tipo de identificación (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI).; si UNI.UFUTIPUNI según valor 1..31 → Clasifica la unidad funcional en categorías de negocio (URGENCIAS, HOSPITALIZACION, UCI, UNIDAD RENAL, CIRUGIA, etc.); valores 5..11 se agrupan todos como ''UCI''.; si EST.FECFINEST = ''1900-01-01 00:00:00.000'' → Calcula días de estancia desde FECINIEST hasta GETDATE() (estancia abierta). else Calcula días de estancia entre FECINIEST y FECFINEST.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalizationStayTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGESTA; dbo.INPACIENT; dbo.ADINGRESO; dbo.CHCAMASHO; dbo.ADCENATEN; dbo.INUNIFUNC; Contract.HealthAdministrator; Contract.CareGroup; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalizationStayTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalizationStayTraceability';
GO
