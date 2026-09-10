
CREATE view [Report].[UploadCubeVieClinicalHospitalOutings] as

	WITH ESTANCIA_INICIAL AS
	(
		SELECT ES.IPCODPACI ,ES.NUMINGRES,ES.FECINIEST  FROM CHREGESTA AS ES INNER JOIN
		(SELECT EST.IPCODPACI ,EST.NUMINGRES,MIN(ID) ID  
		FROM CHREGESTA AS EST GROUP BY EST.IPCODPACI ,EST.NUMINGRES) AS G ON G.ID =ES.ID 

	), estancia AS 
	(

		SELECT 
			est.ipcodpaci, 
			est.numingres, 
			est.feciniest,
			tip.destipest,
			ROW_NUMBER() OVER(PARTITION BY est.ipcodpaci, est.numingres ORDER BY est.feciniest DESC) AS numrow
		FROM dbo.chregesta AS est 
		LEFT JOIN dbo.chtipesta AS tip ON est.codtipest = tip.codtipest
		--WHERE est.feciniest >= DATEADD(DD, -180, @FECINI)

	), CTE_EGRESOS_HOSPITALARIOS AS
	(

		SELECT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
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
				WHEN 15 THEN 'SI' END AS 'TIPO DOCUMENTO',--[Tipo de Documento],
			/*
			CASE PAC.IPTIPODOC WHEN '1' THEN 'CEDULA DE CIUDADANIA' WHEN '2' THEN 'CEDULA DE EXTRANJERIA' WHEN '3' THEN 'TARJETA DE IDENTIDAD' WHEN '4' THEN 'REGISTRO CIVIL' WHEN '5' THEN 'PASAPORTE'
			WHEN '6' THEN 'ADULTO SIN IDENTIFICACION' WHEN '7' THEN 'MENOR SIN IDENTIFICACION' WHEN '8' THEN 'NUMERO UNICO DE IDENTIFICACIÒN' WHEN '9' THEN 'CERTIFICADO NACIDO VIVO' WHEN '10' THEN 'CARNET DIPLOMATICO'
			WHEN '11' THEN 'SALVOCONDUCTO' WHEN '12' THEN 'PERMISO ESPECIAL DE PERMANENCIA' END AS 'DESCRIPCION IDENTIFICACION',*/
			HC.IPCODPACI AS 'NRO IDENTIFICACION',--[NroIdentificacion],
			RTRIM(PAC.IPPRINOMB) AS 'PRIMER NOMBRE',--[PrimerNombre],
			RTRIM(PAC.IPSEGNOMB) As 'SEGUNDO NOMBRE',--[SegundoNombre],
			RTRIM(PAC.IPPRIAPEL) AS 'PRIMER APELLIDO',--[PrimerApellido],
			RTRIM(PAC.IPSEGAPEL) AS 'SEGUNDO APELLIDO',--[SegundoApellido],
			--RTRIM(PAC.IPNOMCOMP) 'NOMBRE COMPLETO PACIENTE',
			PAC.IPFECNACI AS 'FECHA NACIMIENTO',--[FechaNacimiento],
			FLOOR((CAST(CONVERT(VARCHAR(8), HC.FECALTPAC , 112) AS INT)-CAST(CONVERT(VARCHAR(8), PAC.IPFECNACI, 112) AS INT)) / 10000) AS 'EDAD',--[Edad],
			HEA.Code AS 'CODIGO ENTIDAD',--[CodEntidad],
			RTRIM(HEA.Name) AS 'ENTIDAD',--[Entidad],
			RTRIM(CGR.Name) AS 'GRUPO ATENCION',--[GrupoAtencion],

			CEN.NOMCENATE AS 'CENTRO ATENCION',--[CentroAtencion], 
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
				WHEN 31 THEN 'CONSULTA PRIORITARIA' END AS 'TIPO UNIDAD FUNCIONAL',--[TipoUnidadFuncional],
			RTRIM(UNI.UFUDESCRI) AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
			HC.NUMINGRES AS 'NRO INGRESO',--[NroIngreso],
			CAST(EI.FECINIEST   AS DATE) AS 'FECHA INICIO HOSPITALIZACION',--[FechaInicioHospitalizacion],
			CAST(HC.FECALTPAC AS date ) AS 'FECHA ALTA MEDICA',--[FechaAltaMedica],
			DATEDIFF(DAY,EI.FECINIEST ,HC.FECALTPAC) 'DIAS',--[Dias],
			est.destipest AS 'TIPO ESTANCIA',--[TipoEstancia],
			CASE HC.ESTPACEGR 
				WHEN 1 THEN 'MEJOR' 
				WHEN 2 THEN 'IGUAL Y PEOR'
				WHEN 3 THEN 'FALLECIDO' 
				WHEN 4 THEN 'REMITIDO'
				WHEN 5 THEN 'HOSPITALIZACION EN CASA' END AS 'ESTADO EGRESO',--[EstadoEgreso],
			PRO.NOMMEDICO AS 'MEDICO',--[Medico],
			ESP.DESESPECI AS 'ESPECIALIDAD',--[Especialidad],
			ING.CODDIAING AS 'CODIGO DIAGNOSTICO INGRESO',--[CodDiagnosticoIngreso],
			RTRIM(DIA.NOMDIAGNO) AS 'DIAGNOSTICO INGRESO',--[DiagnosticoIngreso],
			ING.CODDIAEGR AS 'CODIGO DIAGNOSTICO EGRESO',--[CodDiagnosticoEgreso],
			RTRIM(DIAE.NOMDIAGNO) AS 'DIAGNOSTICO EGRESO',--[DiagnosticoEgreso],
			CAST(HC.FECALTPAC AS DATE) [FECHA BUSQUEDA],
		    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
			--'1' AS 'CANTIDAD'
		FROM HCREGEGRE HC
		JOIN HCHISPACA HIS WITH (NOLOCK) ON HC.NUMINGRES =HIS.NUMINGRES AND HC.NUMEFOLIO =HIS.NUMEFOLIO 
		JOIN INPACIENT AS PAC  WITH (NOLOCK) ON PAC.IPCODPACI =HC.IPCODPACI
		JOIN ADINGRESO AS ING  WITH (NOLOCK) ON ING.IPCODPACI =HC.IPCODPACI AND ING.NUMINGRES =HC.NUMINGRES
		JOIN ADCENATEN AS CEN WITH (NOLOCK) ON HC.CODCENATE =CEN.CODCENATE
		JOIN INUNIFUNC UNI WITH (NOLOCK) ON HC.UFUCODIGO=UNI.UFUCODIGO
		JOIN Contract .HealthAdministrator HEA WITH (NOLOCK) ON ING.GENCONENTITY =HEA.ID
		JOIN Contract .CareGroup AS CGR WITH (NOLOCK) ON ING.GENCAREGROUP =CGR.Id 
		JOIN ESTANCIA_INICIAL EI WITH (NOLOCK) ON  HC.NUMINGRES =EI.NUMINGRES 
		LEFT JOIN INPROFSAL PRO WITH (NOLOCK) ON HIS.CODPROSAL =PRO.CODPROSAL 
		LEFT JOIN INESPECIA AS ESP WITH (NOLOCK) ON HIS.CODESPTRA = ESP.CODESPECI
		LEFT JOIN INDIAGNOS AS DIA ON ING.CODDIAING =DIA.CODDIAGNO
		LEFT JOIN INDIAGNOS AS DIAE ON ING.CODDIAEGR  =DIAE.CODDIAGNO
		LEFT JOIN estancia AS est ON hc.ipcodpaci = est.ipcodpaci AND hc.numingres = est.numingres AND est.numrow = 1
		--WHERE CAST(HC.FECALTPAC AS DATE) BETWEEN  @FECINI AND @FECFIN
		--AND HC.NUMINGRES ='120606'
	)

	SELECT * FROM CTE_EGRESOS_HOSPITALARIOS
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista orientada a reporting/cubo de datos que consolida los egresos hospitalarios de pacientes, aplanando información demográfica, administrativa y clínica de cada ingreso. Combina datos de identificación del paciente, entidad aseguradora, grupo de atención, centro y unidad funcional, fechas de inicio de hospitalización y alta médica, tipo de estancia, estado al egreso, médico tratante, especialidad y diagnósticos de ingreso/egreso. Está diseñada para alimentar un cubo analítico (BI/OLAP) con el historial de salidas hospitalarias por compañía.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalOutings';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalOutings';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información de egresos hospitalarios (datos del paciente, ingreso, estancia, diagnósticos y entidad responsable) para alimentar el cubo analítico de salidas clínicas hospitalarias.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalOutings';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada egreso en HCREGEGRE debe tener correspondencia en HCHISPACA por NUMINGRES y NUMEFOLIO.; El paciente debe existir en INPACIENT y el ingreso en ADINGRESO.; Debe existir centro de atención (ADCENATEN), unidad funcional (INUNIFUNC), entidad de salud (Contract.HealthAdministrator) y grupo de atención (Contract.CareGroup) asociados al ingreso.; Debe existir al menos un registro de estancia en CHREGESTA para el ingreso (usado por el CTE ESTANCIA_INICIAL via INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalOutings';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha de inicio de hospitalización corresponde SIEMPRE a la estancia con el mínimo ID dentro del ingreso (primera estancia registrada).; El tipo de estancia mostrado corresponde SIEMPRE a la estancia más reciente del ingreso (orden descendente por FECINIEST).; La edad se calcula como diferencia entera de años entre la fecha de alta del paciente (FECALTPAC) y su fecha de nacimiento.; ID_COMPANY se obtiene de DB_NAME() truncado a 9 caracteres, por lo que identifica la base de datos origen.; ULT_ACTUAL se calcula con la zona horaria ''Pakistan Standard Time'' (no horario local colombiano).; La fecha de búsqueda corresponde a la fecha de alta médica (FECALTPAC) casteada a DATE.; Los días de estancia se calculan como DATEDIFF en DAY entre la fecha de inicio de la primera estancia y la fecha de alta.; Solo se incluyen egresos con entidad y grupo de atención asociados (INNER JOIN a HealthAdministrator y CareGroup).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalOutings';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Egreso hospitalario; Paciente; Tipo de documento de identidad; Ingreso hospitalario; Estancia hospitalaria; Tipo de unidad funcional (Urgencias, Hospitalización, UCI, Unidad Renal, Oncológica, etc.); Estado de egreso (mejoría, fallecido, remitido, hospitalización en casa); Diagnóstico de ingreso y egreso; Entidad de salud (HealthAdministrator); Grupo de atención; Médico tratante y especialidad; Centro de atención', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalOutings';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalHospitalOutings: Devuelve un registro por cada egreso hospitalario (HCREGEGRE) con su estancia inicial (mínimo ID en CHREGESTA por paciente/ingreso) y su última estancia vigente (ROW_NUMBER ORDER BY FECINIEST DESC = 1).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalOutings';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pac.iptipodoc entre 1 y 15 → Mapea el código numérico a la sigla del tipo de documento colombiano (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI) else NULL; si uni.ufutipuni según valor numérico → Clasifica la unidad funcional en categorías como URGENCIAS, HOSPITALIZACION, UCI (5-11), UNIDAD RENAL, ONCOLOGICA, CIRUGIA, GINECO OBSTETRICIA, CONSULTA EXTERNA, etc. else NULL; si hc.estpacegr entre 1 y 5 → Traduce el estado de egreso a: MEJOR, IGUAL Y PEOR, FALLECIDO, REMITIDO u HOSPITALIZACION EN CASA else NULL', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalOutings';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGESTA; dbo.CHTIPESTA; dbo.HCREGEGRE; dbo.HCHISPACA; dbo.INPACIENT; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INUNIFUNC; Contract.HealthAdministrator; Contract.CareGroup; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalOutings';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalHospitalOutings';
GO
