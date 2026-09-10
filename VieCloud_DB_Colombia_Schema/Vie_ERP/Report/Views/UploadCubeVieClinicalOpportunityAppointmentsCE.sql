

CREATE view [Report].[UploadCubeVieClinicalOpportunityAppointmentsCE] as 

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
		RTRIM(D.IPPRINOMB) AS 'PRIMER NOMBREO',--[PrimerNombre], 
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
		CASE WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.HealthEntityCode ) ELSE RTRIM(ENT.CODENTIDA) END AS 'CODIGO ENTIDAD',--[CodEntidad],
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

		RTRIM(C.CODIPSSEC) AS 'CODIGO IPS',--[CodIPS], 
		RTRIM(C.NOMCENATE) AS 'CENTRO ATENCION',--[CentroAtencion],
		RTRIM(UNI.UFUDESCRI) AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],

		'CITA MEDICA' AS 'TIPO AGENDAMIENTO',--[TipoAgendamiento],
		RTRIM(F.DESACTMED) AS 'ACTIVIDAD',--[Actividad],
		F.DURAACTIV + ' ' + 'Minutos' 'DURACION ACTIVIDAD',--[DuracionActividad],
		CASE WHEN A.CODTIPSOL = '0' THEN 'PRESENCIAL' WHEN A.CODTIPSOL = '1' THEN 'TELEFONICA' END AS 'FORMA SOLICITUD',--[FormaSolicitud],
		IIF (
			GC.NUMINGRES IS NULL, CASE WHEN A.CODTIPCIT = '0' THEN 'PRIMERA VEZ' WHEN A.CODTIPCIT = '1' THEN 'CONTROL' WHEN A.CODTIPCIT = '2' THEN 'POS OPERATORIO' ELSE 'N/A' END 
			,CASE WHEN ING1.TIPCITMED = '1' THEN 'PRIMERA VEZ' WHEN ING1.TIPCITMED = '2' THEN 'CONTROL' 
			ELSE CASE WHEN A.CODTIPCIT = '0' THEN 'PRIMERA VEZ' WHEN A.CODTIPCIT = '1' THEN 'CONTROL' WHEN A.CODTIPCIT = '2' THEN 'POS OPERATORIO' ELSE 'N/A' END END ) AS 'TIPO CITA',--[TipoCita],
		CASE A.MODALIDAD WHEN 0 THEN 'PRESENCIAL' WHEN 1 THEN 'TELECONSULTA' ELSE 'N/A' END AS 'MODALIDAD',--[Modalidad],
		CONVERT(varchar,CAST(A.FECITADES AS date),23) AS 'FECHA DESEADA',--[FechaDeseada] , 
		CONVERT(varchar,CAST(A.FECHAOFERTADA AS date),23) AS 'MEJOR FECHA DISPONIBLE',--[MejorFechaDisponible],
		CONVERT(varchar,CAST(FECHORAIN AS date),23) AS 'FECHA ASIGNACION',--[FechaAsignacion],
		CONVERT(varchar,CAST(FECHORAIN AS datetime),20) AS 'FECHA HORA ASIGNACION',--[FechaHoraAsignacion],
		CONVERT(varchar,CAST(FECHORAFI AS date),23) AS 'FECHA FINAL ASIGNACION',--[FechaFinalAsignacion],
		CONVERT(varchar,CAST(FECHORAFI AS datetime),20) AS 'FECHA HORA FINAL ASIGNACION',--[FechaHoraFinalAsignacion],
		CONVERT(varchar,CAST(A.FECREGSIS AS date),23) AS 'FECHA SOLICITUD',--[FechaSolicitud],
		CONVERT(varchar,CAST(A.FECHCANCELA AS datetime),20) AS 'FECHA CANCELACION',--[FechaCancelacion],
		CONVERT(varchar,CAST(A.FECREGSIS AS datetime),23) AS 'FECHA REGISTRO DB',--[FechaRegistroDB],
		DATEDIFF(DAY,A.FECREGSIS,FECHORAIN) AS 'FECHA ASIGNACION VS FECHA SOLICITUD',--[FechaAsignacionVSFechaSolicitud],
		DATEDIFF(DAY,A.FECITADES,FECHORAIN) AS 'FECHA ASIGNACION VS FECHA DESEADA',--[FechaAsignacionVSFechaDeseada],
		DATEDIFF(DAY,A.FECREGSIS,A.FECHAOFERTADA) AS 'DISPONIBILIDAD',--[Disponibilidad],
		CASE WHEN A.CITAEXTRA = 1 THEN 'Si' ELSE 'No' END AS 'CITA EXTRA',--[CitaExtra],
		--REPS.[CodigoReps] AS [CodREPSEspecialidad]
		B.CODESPECI AS 'CODIGO ESPECIALIDAD',--[CodEspecialidad],
		RTRIM(B.DESESPECI) AS 'ESPECIALIDAD',--[Especialidad],
		RTRIM(E.NOMMEDICO) AS 'MEDICO',--[Medico],
		ISNULL(RTRIM(M.CODSERIPS),ISNULL(RTRIM(CE.Code),ISNULL(RTRIM(O.CODSERIPS),RTRIM(IPS3.CODSERIPS)))) AS 'CODIGO CUPS',--[CodigoCUPS],
		ISNULL(RTRIM(M.DESSERIPS),ISNULL(RTRIM(CD.Name),ISNULL(RTRIM(IPS2.DESSERIPS),RTRIM(IPS3.DESSERIPS)))) AS 'DESCRIPCION',--[Descripcion],
		RTRIM(G.NOMUSUARI) AS 'USUARIO REGISTRO',--[UsuarioRegistro],
		'ASIGNADA' AS 'ESTADO INICIAL',--[EstadoInicial],
		CASE 
			WHEN A.CODESTCIT = '0' THEN 'ASIGNADA' 
			WHEN A.CODESTCIT = '1' THEN 'CUMPLIDA' 
			WHEN A.CODESTCIT = '2' THEN 'INCUMPLIDA'
			WHEN A.CODESTCIT = '3' THEN 'PREASIGNADA' 
			WHEN A.CODESTCIT = '4' THEN 'CANCELADA'  
			WHEN A.CODESTCIT = '5' THEN 'CANCELADA' END AS 'ESTADO ACTUAL',--[EstadoActual], 
		ISNULL(A.CODCAUCAN, '00') 'CODIGO CANCELACION',--[CodCancelacion],
		ISNULL(RTRIM(CAN.DESCAUCAN),NULL) 'DESCRIPCION CANCELACION',--[DescripcionCancelacion],
		ISNULL(GC.NUMINGRES, NULL) 'NRO INGREO ATENCION',--[NroIngresoAtencion],
		1 AS 'CANTIDAD',--[Cantidad],
		--CONVERT(varchar,CAST(FECHORAIN AS DATE),23) AS 'FECHA BUSQUEDA',--[FechaBusqueda],
		RTRIM(GA.CODUSUARI) + ' ' + GA.NOMUSUARI AS 'USUARIO ASIGNO',--[UsuarioAsigno],
		RTRIM(conf.usuariocreacion) + ' ' + GUC.NOMUSUARI AS 'USUARIO CONFIRMO',--[UsuarioConfirmo]
		CAST(A.FECHORAIN AS DATE) AS 'FECHA BUSQUEDA',
	    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM AGASICITA A with (nolock)
	INNER JOIN ADCENATEN AS C with (nolock) ON A.CODCENATE = C.CODCENATE
	INNER JOIN AGCONSULT AS AGC with (nolock) ON AGC.CODIGOCON =A.CODIGOCON AND AGC.CODCENATE =C.CODCENATE
	INNER JOIN INPROFSAL AS E with (nolock) ON A.CODPROSAL = E.CODPROSAL
	INNER JOIN INESPECIA AS B with (nolock) ON A.CODESPECI = B.CODESPECI
	INNER JOIN AGACTIMED AS F with (nolock) ON A.CODACTMED = F.CODACTMED
	INNER JOIN INPACIENT AS D with (nolock) ON A.IPCODPACI = D.IPCODPACI
	INNER JOIN INUBICACI AS UBI with (nolock) ON D.AUUBICACI =UBI.AUUBICACI
	INNER JOIN INMUNICIP as MUN with (nolock) ON UBI.DEPMUNCOD =MUN.DEPMUNCOD
	INNER JOIN INDEPARTA AS DEP with (nolock) ON DEP.depcodigo =MUN.DEPCODIGO
	INNER JOIN INENTIDAD AS ENT with (nolock) ON D.CODENTIDA = ENT.CODENTIDA
	INNER JOIN Contract.HealthAdministrator AS HEA with (nolock) ON A.GENCONENTITY = HEA.Id
	INNER JOIN SEGusuaru AS G with (nolock) ON A.CODUSUASI = G.CODUSUARI
	LEFT JOIN INCUPSIPS AS M with (nolock) ON A.CODSERIPS = M.CODSERIPS
	LEFT JOIN Contract.CareGroup AS CG WITH (NOLOCK) ON CG.Id =D.GENCAREGROUP
	LEFT JOIN Contract.CUPSEntityContractDescriptions AS CECD WITH (NOLOCK) ON CECD.ID=A.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.CUPSEntity AS CE WITH (NOLOCK) ON CE.Id =CECD.CUPSEntityId
	LEFT JOIN Contract.ContractDescriptions AS CD WITH (NOLOCK) ON CD.Id =CECD.ContractDescriptionId
	LEFT JOIN INUNIFUNC AS UNI with (nolock) ON UNI.UFUCODIGO =AGC.UFUCODIGO
	LEFT JOIN AGCAUCANC AS CAN with (nolock) ON A.CODCAUCAN=CAN.CODCAUCAN
	LEFT JOIN (
		SELECT CS1.* FROM ADCONCOEX  CS1 INNER JOIN 
		(
			SELECT MAX(CS.CODCONCEC) CODCONCEC,CS.IPCODPACI, CS.IPFECHCIT FROM ADCONCOEX AS CS  with (nolock) 
			WHERE CS.CONESTADO <>'2' /*AND IPCODPACI LIKE '24822605' AND NUMCONCIT='201983'*/
			GROUP BY CS.IPCODPACI, CS.IPFECHCIT
		) CS2 ON CS1.IPCODPACI=CS2.IPCODPACI AND CS1.IPFECHCIT=CS2.IPFECHCIT AND CS1.CODCONCEC=CS2.CODCONCEC
	) AS GC 
	ON GC.NUMCONCIT = A.CODAUTONU 
	LEFT JOIN RIASCUPS AS O with (nolock) ON A.IDRIASCUPS = O.ID
	LEFT JOIN RIAS AS Q with (nolock) ON O.IDRIAS = Q.ID
	LEFT JOIN INCUPSIPS AS IPS2 with (nolock) ON O.CODSERIPS =IPS2.CODSERIPS
	LEFT JOIN INCUPSIPS AS IPS3 with (nolock) ON F.CODSERIPS =IPS3.CODSERIPS
	LEFT JOIN HCHISPACA AS HIS WITH (NOLOCK) ON GC.NUMINGRES =HIS.NUMINGRES AND HIS.GENCONEXT=1 AND HIS.TIPHISPAC ='I'
	LEFT JOIN HCURGING1 AS ING1 WITH (NOLOCK) ON HIS.NUMINGRES=ING1.NUMINGRES AND HIS.NUMEFOLIO=ING1.NUMEFOLIO
	--LEFT JOIN INDIGOREP.dbo.TablaEspecialidadesReps AS REPS ON B.CODESPECI=REPS.CodigoEspecialidad
	LEFT JOIN SEGusuaru AS GA with (nolock) ON A.CODUSUASI = GA.CODUSUARI
	--LEFT JOIN indigosec.security.person AS pera ON a.codusuasi = pera.identification 
	LEFT JOIN agasicitaconfir AS conf ON a.codautonu = conf.idagasicita
	LEFT JOIN SEGusuaru AS GUC with (nolock) ON conf.usuariocreacion = GUC.CODUSUARI
	--LEFT JOIN indigosec.security.person AS perc ON conf.usuariocreacion = perc.identification 
	WHERE  A.TIPSOLICITU = 1 and year(A.FECHORAIN)>=2023
	--AND CAST(A.FECHORAIN AS DATE) BETWEEN @FechaInicio AND @FechaFin
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista orientada a reporting/cubo OLAP que aplana información de citas médicas ambulatorias (tipo solicitud = 1) asignadas desde 2023, consolidando datos demográficos del paciente, entidad pagadora con régimen, centro de atención, especialidad, médico, códigos CUPS, fechas clave (solicitud, asignación, deseada, ofertada, cancelación) y métricas de oportunidad calculadas como diferencias en días. Incluye tipo de cita, modalidad, estado actual y usuarios que asignaron o confirmaron la cita, destinada a alimentar un cubo de oportunidad de acceso en consulta externa.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOpportunityAppointmentsCE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOpportunityAppointmentsCE';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida y aplana las citas médicas de consulta externa asignadas desde 2023 para alimentar un cubo de oportunidad clínica, incluyendo datos demográficos del paciente, entidad/régimen, profesional, CUPS, tiempos de oportunidad y estado de la cita.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOpportunityAppointmentsCE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cita debe tener TIPSOLICITU = 1 (citas de consulta externa).; La fecha de inicio de la cita (FECHORAIN) debe ser de año 2023 o posterior.; Deben existir registros relacionados en centro de atención, agenda, profesional, especialidad, actividad médica, paciente, ubicación geográfica, entidad, administradora de salud (Contract.HealthAdministrator) y usuario de seguridad (todas son INNER JOIN).; El paciente debe tener una HealthAdministrator válida (GENCONENTITY referenciable) por ser INNER JOIN.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOpportunityAppointmentsCE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan citas de consulta externa (TIPSOLICITU=1) con FECHORAIN desde 2023.; Estado inicial siempre se reporta como ''ASIGNADA'' y Cantidad=1 por fila.; Tipo de agendamiento siempre ''CITA MEDICA''.; Las consultas externas con CONESTADO=''2'' se excluyen al determinar el ingreso de atención (NRO INGRESO ATENCION).; El campo ULT_ACTUAL refleja la fecha/hora actual convertida a zona horaria ''Pakistan Standard Time''.; ID_COMPANY se obtiene del nombre de la base de datos (DB_NAME) truncado a VARCHAR(9).; Solo considera ingresos hospitalarios con HIS.GENCONEXT=1 y HIS.TIPHISPAC=''I'' al cruzar con HCHISPACA.; Códigos CUPS y descripciones se resuelven por orden de prioridad (CUPS de cita → CUPS de contrato/entidad → CUPS de RIAS → CUPS de actividad médica).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOpportunityAppointmentsCE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalOpportunityAppointmentsCE: Devuelve un set DISTINCT de citas externas (TIPSOLICITU=1, año(FECHORAIN)>=2023) con métricas de oportunidad y datos demográficos para carga del cubo BI.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOpportunityAppointmentsCE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si iptipodoc del paciente (1..15) → Mapea a códigos de tipo de identificación: 1=CC, 2=CE, 3=TI, 4=RC, 5=PA, 6=AS, 7=MS, 8=NU, 9=CN, 10=CD, 11=SC, 12=PE, 13=PT, 14=DE, 15=SI.; si IPSEXOPAC = ''1'' o ''2'' → 1=HOMBRE/H, 2=MUJER/M; otro valor queda NULL.; si HEA.Id IS NOT NULL → Usa código y nombre de Contract.HealthAdministrator como entidad. else Usa CODENTIDA/NOMENTIDA de INENTIDAD.; si HEA.EntityType (1..12) → Clasifica el régimen: 1=EPS Contributivo, 2=EPS Subsidiado, 3=ET Vinculados Municipios, 4=ET Vinculados Departamentos, 5=ARL, 6=Medicina Prepagada, 7=IPS Privada, 8=IPS Pública, 9=Régimen Especial, 10=Accidentes de tránsito, 11=Fosyga, 12=Otros.; si A.CODTIPSOL = ''0'' o ''1'' → Forma de solicitud: 0=PRESENCIAL, 1=TELEFONICA.; si GC.NUMINGRES IS NULL (no hay ingreso de atención asociado) → Tipo de cita derivado de A.CODTIPCIT (0=PRIMERA VEZ, 1=CONTROL, 2=POS OPERATORIO, otro=N/A). else Si ING1.TIPCITMED=''1'' → PRIMERA VEZ; si ''2'' → CONTROL; en otro caso usa A.CODTIPCIT con el mismo mapeo.; si A.MODALIDAD = 0 o 1 → 0=PRESENCIAL, 1=TELECONSULTA, otro=N/A.; si A.CITAEXTRA = 1 → Marca ''Si'' en CITA EXTRA, en otro caso ''No''.; si A.CODESTCIT (0..5) → Estado actual: 0=ASIGNADA, 1=CUMPLIDA, 2=INCUMPLIDA, 3=PREASIGNADA, 4=CANCELADA, 5=CANCELADA.; si Resolución de CODIGO CUPS / DESCRIPCION → Prioridad: INCUPSIPS por A.CODSERIPS, luego Contract.CUPSEntity vía CUPSEntityContractDescriptions, luego RIASCUPS (O.CODSERIPS), por último IPS3 (CODSERIPS de la actividad médica F).; si Subconsulta de consultas externas (ADCONCOEX) → Selecciona el MAX(CODCONCEC) por (IPCODPACI, IPFECHCIT) descartando CONESTADO=''2'' (cancelado/anulado), y la enlaza a la cita por NUMCONCIT = A.CODAUTONU para obtener el ingreso/atención.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOpportunityAppointmentsCE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOpportunityAppointmentsCE';
GO
