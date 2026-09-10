

CREATE view [Report].[UploadCubeVieClinicalOpportunityQxAppointments] as 
	
	SELECT DISTINCT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		CASE d.iptipodoc
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
			D.IPCODPACI AS 'NRO IDENTIFICACION',--[NroIdentificacion], 
		RTRIM(D.IPPRIAPEL) AS 'PRIMER APELLIDO',--[PrimerApellido],
		RTRIM(D.IPSEGAPEL) AS 'SEGUNDO APELLIDO',--[SegundoApellido], 
		RTRIM(D.IPPRINOMB) AS 'PRIMER NOMBRE',--[PrimerNombre], 
		RTRIM(D.IPSEGNOMB) AS 'SEGUNDO NOMBRE',--[SegundoNombre],
		CAST(D.IPFECNACI AS DATE) AS 'FECHA NACIMIENTO',--[FechaNacimiento],
		DATEDIFF(YEAR, D.IPFECNACI, GETDATE()) AS 'EDAD',--[Edad],
		CASE D.IPSEXOPAC WHEN '1' THEN 'H' WHEN '2' THEN 'M' END AS 'CODIGO SEXO',--[CodSexo],
		CASE D.IPSEXOPAC WHEN '1' THEN 'HOMBRE' WHEN '2' THEN 'MUJER' END AS 'SEXO',--[Sexo],
		DEP.depcodigo AS 'CODIGO DEPARTAMENTO',--[CodDepartamento],
		DEP.nomdepart AS 'DEPARTAMENTO',--[Departamento], 
		MUN.MUNCODIGO 'CODIGO MUNICIPIO',--[CodMunicipio], 
		MUN.MUNNOMBRE AS 'MUNICIPIO',--[Municipio],
		UPPER(D.IPDIRECCI) AS 'DIRECCION',--[Direccion], 
		D.IPTELMOVI AS 'TELEFONO PRINCIPAL',--[TelefonoPrincipal], 
		D.IPTELEFON AS 'TELEFONO ALTERNATIVO',--[TelefonoAlternativo],
		CASE WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.HealthEntityCode ) ELSE RTRIM(ENT.CODENTIDA) END AS 'CODIGO ENTIDAD',--[CodigoEntidad],
		CASE WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.Name) ELSE RTRIM(ENT.NOMENTIDA) END AS 'ENTIDAD',--[Entidad],
		CASE HEA.EntityType
			WHEN 1 THEN 'EPS Contributivo'
			WHEN 2 THEN 'EPS Subsidiado'
			WHEN 3 THEN 'ET Vinculados Municipios'
			WHEN 4 THEN 'ET Vinculados Departamentos'
			WHEN 5 THEN 'ARL Riesgos Laborales'
			WHEN 6 THEN 'MP Medicina Prepagada'
			WHEN 7 THEN 'IPS Privada'
			WHEN 8 THEN 'IPS Publica'
			WHEN 9 THEN 'Regimen Especial'
			WHEN 10 THEN 'Accidentes de transito'
			WHEN 11 THEN 'Fosyga'
			WHEN 12 THEN 'Otros' END AS 'REGIMEN',--[Regimen],
		CG.Name AS 'GRUPO ATENCION',--[GrupoAtencion],
		RTRIM(C.CODIPSSEC) AS 'CODIGO IPS',--[CodigoIPS], 
		RTRIM(C.NOMCENATE) AS 'CENTRO ATENCION',--[CentroAtencion],
		RTRIM(UF1.UFUDESCRI) AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
		RTRIM(AGE.CODSERIPS) AS 'CUPS',--[CUPS],	
		RTRIM(M.DESSERIPS) AS 'DESCRIPCION',--[Descripcion],
		'PRIMERA VEZ' 'TIPO CITA',--[TipoCita],
		'CITA CIRUGIA' AS 'TIPO AGENDA',--[TipoAgenda],
		--REPS.[CodigoReps] AS [CodREPSEspecialidad],
		B.CODESPECI AS 'CODIGO ESPECIALIDAD',--[CodEspecialidad],
		RTRIM(B.DESESPECI) AS 'ESPECIALIDAD',--[Especialidad],
		RTRIM(E.NOMMEDICO) AS 'MEDICO',--[Medico],
		'PROCEDIMIENTOS QUIRURGICO ODO' AS 'ACTIVIDAD AGENDAMIENTO',--[ActividadAgendamiento],
		CAST(RTRIM(AGE.DURPROCQX) AS char ) + ' ' + 'MINUTOS' AS 'DURACION ACTIVIDAD',--[DuracionActividad],
		ISNULL(HOS.FECORDMED,AGE.FECREGSIS) AS 'FECHA SOLICITUD CIRUGIA',--[FechaSolicitudCirugia],
		AGE.FECREGSIS AS 'FECHA REGISTRO RADICACION',--[FechaRegistroRadicacion],
		AGE.FECHORAIN AS 'FECHA ASIGNACION CIRUGIA',--[FechaAsignacionCirugia],
		IIF(AGE.IDRADICACIONQX IS NULL,HOS.HORA_INFO_QX_INICIO,AMB.HORA_INFO_QX_AMB_INICIO)  'FECHA INFORME QUIRURGICO',--[FechaInformeQuirurgico],
		DATEDIFF(DAY,AGE.FECREGSIS,IIF(AGE.IDRADICACIONQX IS NULL,HOS.HORA_INFO_QX_INICIO,AMB.HORA_INFO_QX_AMB_INICIO)) AS 'DIAS DESDE AGENDAMIENTO',--[DiasDesdeAgendamiento],
		CASE  AGE.ORIGENQX 
			WHEN 1 THEN 'AMBULATORIA' 
			WHEN 2 THEN 'HOSPITALARIA' END 'TIPO SOLICITUD',--[TipoSolicitud],
		isnull(AGE.NUMINGRES,HOS.INGRESO ) 'NRO INGRESO',--[NroIngreso],
		SAL.CODIGSALA  'CODIGO CONSULTORIO SALA',--[CodConsultorioSala] ,
		SAL.DESCRIPSAL  'CONSULTORIO SALA',--[ConsultorioSala],
		CASE 
			WHEN AGE.CODESTPQX  = '0' THEN  IIF(IIF(AGE.IDRADICACIONQX IS NULL,HOS.HORA_INFO_QX_INICIO,AMB.HORA_INFO_QX_AMB_INICIO) IS NULL,'CIRUGIA PROGRAMADA','CIRUGIA REALIZADA')
			WHEN AGE.CODESTPQX  = '1' THEN 'PACIENTE ADMITIDO (CIRUGIA AMBULATORIA)'	
			WHEN AGE.CODESTPQX = '2' THEN 'PACIENTE EN SALA DE ESPERA'
			WHEN AGE.CODESTPQX = '3' THEN 'PACIENTE EN SALA QUIRURGICA' 
			WHEN AGE.CODESTPQX = '4' THEN 'PACIENTE EN RECUPERACION'		
			WHEN AGE.CODESTPQX = '5' THEN 'PACIENTE CON ALTA'
			WHEN AGE.CODESTPQX = '6' THEN 'CANCELADA' END AS 'ESTADO CIRUGIA',--[EstadoCirugia],
		RTRIM(AGE.OBSERVACION ) AS 'OBSERVACION CITA',--[ObservacionCita], 	
		RTRIM(G.NOMUSUARI) AS 'USUARIO REGISTRO CITA',--[UsuarioRegistroCita],
		RTRIM(H.NOMUSUARI) AS 'USUARIO CANCELO CITA',--[UsuarioCanceloCita],
		AGE.FECHACAN AS 'FECHA CANCELACION',--[FechaCancelacion],
		RTRIM(P.DESCAUCAN) AS 'CAUSA CANCELACION',--[CausaCancelacion] ,	
		RTRIM(AGE.OBSERCAN) AS 'OBSERVACION CANCELACION',--[ObservacionesCancelacion],
		CAST(AGE.FECHORAIN AS date ) AS 'FECHA BUSQUEDA',
	    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL

	FROM DBO.AGEPROGQX AS AGE INNER JOIN 
	ADCENATEN AS C with (nolock) ON AGE.CODCENATE = C.CODCENATE 
	JOIN INPACIENT AS D with (nolock) ON AGE.IPCODPACI = D.IPCODPACI
	JOIN INUBICACI AS UBI with (nolock) ON D.AUUBICACI =UBI.AUUBICACI
	JOIN AGENSALAC AS SAL with (nolock) ON AGE.AGENSALAC =SAL.CODCONCEC 
	JOIN INMUNICIP as MUN with (nolock) ON UBI.DEPMUNCOD =MUN.DEPMUNCOD 
	JOIN INDEPARTA AS DEP  with (nolock) ON DEP.depcodigo =MUN.DEPCODIGO 
	JOIN INUNIFUNC AS UF1 with (nolock) 	ON SAL.UFUCODIGO = UF1.UFUCODIGO 
	JOIN INENTIDAD AS ENT with (nolock) ON D.CODENTIDA = ENT.CODENTIDA	
	JOIN Contract.HealthAdministrator AS HEA with (nolock)	ON  D.GENCONENTITY = HEA.Id 
	JOIN Contract.CareGroup AS CG ON CG.Id =D.GENCAREGROUP 
	JOIN INESPECIA AS B with (nolock) ON AGE.CODESPECI = B.CODESPECI  
	JOIN INPROFSAL AS E with (nolock) ON AGE.CODPROSAL = E.CODPROSAL
	JOIN INCUPSIPS AS M with (nolock) ON AGE.CODSERIPS = M.CODSERIPS 
	JOIN SEGusuaru AS G with (nolock) ON AGE.CODUSUASI = G.CODUSUARI 
	LEFT JOIN SEGusuaru AS H with (nolock) ON AGE.CODUSUCAN  = H.CODUSUARI 
	LEFT JOIN AGCAUCANC AS P with (nolock)	ON AGE.CODCAUCAN = P.CODCAUCAN   
	LEFT JOIN SEGusuaru AS I with (nolock) ON AGE.CODUSUINA = I.CODUSUARI 
	--LEFT JOIN INDIGOREP.dbo.TablaEspecialidadesReps AS REPS ON B.CODESPECI=REPS.CodigoEspecialidad
	LEFT JOIN (
		SELECT AGE.IPCODPACI ,AGE.CODSERIPS,PQX.FECORDMED  ,AGE.FECHORAIN ,AGE.FECHORAFI ,AGE.AUTOHCORDPROQ , PQX.NUMINGRES 'INGRESO' ,
		REA.CODSERIPS 'CUPS_REALIZADO',INF.CODDIAPRE ,INF.CODDIAPOS,INF.FECHORINI 'HORA_INFO_QX_INICIO' ,INF.FECHORFIN 'HORA_INFO_QX_FIN'
		FROM DBO.AGEPROGQX AS AGE 
		JOIN DBO.HCORDPROQ AS PQX ON AGE.AUTOHCORDPROQ = PQX.AUTO 
		JOIN DBO.HCQXREALI AS REA ON PQX.NUMINGRES = REA.NUMINGRES AND PQX .CODSERIPS =REA.CODSERIPS 
		JOIN DBO.HCQXINFOR AS INF ON INF.NUMINGRES = PQX.NUMINGRES 		
					
		WHERE AGE.PRINCIPAL =1 AND AGE.AUTOHCORDPROQ IS NOT NULL --AND AGE.IPCODPACI ='6057524' 
		--AND CAST(AGE.FECHORAIN AS date )  BETWEEN @DateStart AND @DateEnd
		) AS HOS ON HOS.AUTOHCORDPROQ =AGE.AUTOHCORDPROQ 
		LEFT JOIN (SELECT AGE.IPCODPACI ,AGE.CODSERIPS ,AGE.FECHORAIN ,AGE.FECHORAFI ,AGE.NUMINGRES ,INF.CODSERIPS 'CUPS_REALIZADO',
		INF.CODDIAPRE ,INF.CODDIAPOS,INF.FECHORINI 'HORA_INFO_QX_AMB_INICIO' ,INF.FECHORFIN 'HORA_INFO_QX_AMB_FIN'
		FROM DBO.AGEPROGQX AS AGE
		JOIN DBO.HCQXINFOR AS INF ON AGE.NUMINGRES = INF.NUMINGRES AND AGE.CODSERIPS =INF.CODSERIPS 
		WHERE AGE.PRINCIPAL = 1 AND AGE.NUMINGRES  IS NOT NULL --AND AGE.IPCODPACI ='42026438'
		 --AND CAST(AGE.FECHORAIN AS date) BETWEEN @DateStart AND @DateEnd
		) AMB ON AMB.NUMINGRES =AGE.NUMINGRES AND AMB.CUPS_REALIZADO =AGE.CODSERIPS 
	Where AGE.PRINCIPAL = 1 --AND CAST(AGE.FECHORAIN AS date )  BETWEEN @DateStart AND @DateEnd 
	--AND AGE.CODESTPQX<>'6'
	--and AGE.IPCODPACI IN ('38450117') --('7246667','42026438','15377439')
	--AND IIF(AGE.IDRADICACIONQX IS NULL,HOS.HORA_INFO_QX_INICIO,AMB.HORA_INFO_QX_AMB_INICIO) IS NULL
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte destinada a alimentar un cubo analítico con información aplanada de citas/programaciones quirúrgicas. Consolida datos demográficos del paciente, entidad pagadora, régimen, centro de atención, sala quirúrgica, especialidad, médico y procedimiento CUPS. Incluye tiempos clave del flujo quirúrgico (solicitud, radicación, asignación, informe quirúrgico) diferenciando origen hospitalario o ambulatorio, días transcurridos y estado de la cirugía. También captura cancelaciones, usuarios de registro y la última actualización con zona horaria.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOpportunityQxAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOpportunityQxAppointments';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de carga al cubo BI que consolida las citas/agendamientos de cirugía (programadas, realizadas o canceladas) con datos demográficos del paciente, entidad pagadora, especialidad, profesional, sala y tiempos quirúrgicos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOpportunityQxAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cita debe ser principal (AGEPROGQX.PRINCIPAL = 1); El paciente debe tener entidad asociada en Contract.HealthAdministrator (INNER JOIN por D.GENCONENTITY = HEA.Id); El paciente debe tener un grupo de atención en Contract.CareGroup (INNER JOIN por D.GENCAREGROUP); Para enriquecer con datos de cirugía hospitalaria: AGE.AUTOHCORDPROQ no nulo y existencia de HCORDPROQ + HCQXREALI + HCQXINFOR con NUMINGRES y CODSERIPS coincidentes; Para enriquecer con datos de cirugía ambulatoria: AGE.NUMINGRES no nulo con HCQXINFOR coincidente en NUMINGRES y CODSERIPS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOpportunityQxAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo incluye agendamientos quirúrgicos marcados como principales (PRINCIPAL=1); TIPO CITA siempre se reporta como ''PRIMERA VEZ'' y TIPO AGENDA como ''CITA CIRUGIA''; ACTIVIDAD AGENDAMIENTO se fija como ''PROCEDIMIENTOS QUIRURGICO ODO''; ID_COMPANY corresponde al nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres; DURACION ACTIVIDAD siempre se expresa concatenando AGE.DURPROCQX con la cadena ''MINUTOS''; El cálculo de DIAS DESDE AGENDAMIENTO usa la fecha de informe quirúrgico (hospitalaria o ambulatoria según radicación) menos FECREGSIS; ULT_ACTUAL siempre se calcula con la zona horaria ''Pakistan Standard Time''; EDAD se calcula por diferencia de años entre IPFECNACI y la fecha actual', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOpportunityQxAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado de la vista: Devuelve un registro DISTINCT por cita quirúrgica principal (PRINCIPAL=1) con timestamp ULT_ACTUAL convertido a zona horaria ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOpportunityQxAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si D.IPTIPODOC entre 1 y 15 → Mapea a códigos de tipo de identificación: 1=CC, 2=CE, 3=TI, 4=RC, 5=PA, 6=AS, 7=MS, 8=NU, 9=CN, 10=CD, 11=SC, 12=PE, 13=PT, 14=DE, 15=SI; si D.IPSEXOPAC = ''1'' o ''2'' → Sexo: 1=HOMBRE/H, 2=MUJER/M; si HEA.Id IS NOT NULL → Toma código y nombre desde Contract.HealthAdministrator (HealthEntityCode, Name) else Usa ENT.CODENTIDA y ENT.NOMENTIDA de INENTIDAD; si HEA.EntityType (1..12) → Determina el régimen: 1=EPS Contributivo, 2=EPS Subsidiado, 3=ET Vinculados Municipios, 4=ET Vinculados Departamentos, 5=ARL, 6=Medicina Prepagada, 7=IPS Privada, 8=IPS Pública, 9=Régimen Especial, 10=Accidentes de tránsito, 11=Fosyga, 12=Otros; si AGE.ORIGENQX = 1 / 2 → Tipo solicitud: 1=AMBULATORIA, 2=HOSPITALARIA; si AGE.IDRADICACIONQX IS NULL → Toma fecha de informe quirúrgico desde el flujo HOSPITALARIO (HOS.HORA_INFO_QX_INICIO) else Toma fecha desde el flujo AMBULATORIO (AMB.HORA_INFO_QX_AMB_INICIO); si AGE.CODESTPQX = ''0'' y fecha de informe quirúrgico es NULL → Estado=''CIRUGIA PROGRAMADA'' else Si CODESTPQX=''0'' y hay informe → ''CIRUGIA REALIZADA''; si AGE.CODESTPQX entre ''1'' y ''6'' → Estados: 1=PACIENTE ADMITIDO (AMB), 2=EN SALA DE ESPERA, 3=EN SALA QUIRURGICA, 4=EN RECUPERACION, 5=CON ALTA, 6=CANCELADA; si HOS.FECORDMED no nulo → Usa FECORDMED como FECHA SOLICITUD CIRUGIA else Usa AGE.FECREGSIS; si AGE.NUMINGRES no nulo → Usa NUMINGRES de la cita else Usa HOS.INGRESO', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOpportunityQxAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.AGEPROGQX; DBO.ADCENATEN; DBO.INPACIENT; DBO.INUBICACI; DBO.AGENSALAC; DBO.INMUNICIP; DBO.INDEPARTA; DBO.INUNIFUNC; DBO.INENTIDAD; Contract.HealthAdministrator; Contract.CareGroup; DBO.INESPECIA; DBO.INPROFSAL; DBO.INCUPSIPS; DBO.SEGusuaru; DBO.AGCAUCANC; DBO.HCORDPROQ; DBO.HCQXREALI; DBO.HCQXINFOR', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOpportunityQxAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOpportunityQxAppointments';
GO
