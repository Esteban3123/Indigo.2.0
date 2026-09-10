

--CREATE PROCEDURE [Scheduling].[ReporteAgendamientoFinal]
--DECLARE	@FechaInicio DATE='2024-05-01';
--DECLARE	@FechaFin DATE ='2024-05-30';
--AS

CREATE view [Report].[UploadCubeVieClinicalFinalSchedulingReport] AS

	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
		CASE D.IPTIPODOC 	
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
			D.IPCODPACI AS 'NRO IDENTIFICACION',-- [NroIdentificacion],
		RTRIM(D.IPPRIAPEL) AS 'PRIMER APELLIDO',--[PrimerApellido],
		RTRIM(D.IPSEGAPEL) AS 'SEGUNDO APELLIDO',--[SegundoApellido], 
		RTRIM(D.IPPRINOMB) AS 'PRIMER NOMBRE',--[PrimerNombre] , 
		RTRIM(D.IPSEGNOMB) AS 'SEGUNDO NOMBRE',--[SegundoNombre],
		CAST(D.IPFECNACI AS DATE) AS 'FECHA NACIMIENTO',--[FechaNacimiento],
		DATEDIFF(YEAR, D.IPFECNACI, GETDATE()) AS 'EDAD',--[Edad],
		CASE D.IPSEXOPAC WHEN '1' THEN 'H' WHEN '2' THEN 'M' END AS 'CODIGO SEXO',--[CodigoSexo],
		CASE D.IPSEXOPAC WHEN '1' THEN 'HOMBRE' WHEN '2' THEN 'MUJER' END AS 'SEXO',--[Sexo],
		DEP.depcodigo AS 'CODIGO DEPARTAMENTO',--[CodDepartamento],
		DEP.nomdepart AS 'DEPARTAMENTO',--[Departamento],
		MUN.MUNCODIGO 'CODIGO MUNICIPIO',--[CodMunicipio], 
		MUN.MUNNOMBRE AS 'MUNICIPIO',--[Municipio],
		UPPER(D.IPDIRECCI) AS 'DIRECCION',--[Direccion], 
		D.IPTELMOVI AS 'TELEFONO PRINCIPAL',--[TelPrincipal], 
		D.IPTELEFON AS 'TELEFONO ALTERNATIVO',--[TelAlternativo],
		CASE 
			WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.HealthEntityCode ) 
			ELSE  RTRIM(ENT.CODENTIDA) END AS 'CODIGO ENTIDAD',--[CodEntidad],
		CASE 
			WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.Name) 
			ELSE RTRIM(ENT.NOMENTIDA) END AS 'ENTIDAD',--[Entidad],
		CG.Name AS 'GRUPO ATENCION',--[GrpAtencion],
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
			WHEN 12 THEN 'Otros' end as 'REGIMEN',--[Regimen],
		RTRIM(C.CODCENATE) AS 'CODIGO CENTRO ATENCION',--[CodCentroAtencion],
		RTRIM(C.NOMCENATE) AS 'CENTRO ATENCION',--[CentroAtencion],
		CASE 
			WHEN UF1.UFUCODIGO IS NOT NULL THEN RTRIM(UF1.UFUDESCRI) 
			WHEN UF2.UFUCODIGO IS NOT NULL THEN RTRIM(UF2.UFUDESCRI) 
			ELSE 'CONSULTA EXTERNA' END AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
		CS.NUMINGRES AS 'NRO INGRESO',--[NroIngreso], 
		ISNULL(RTRIM(E.CODPROSAL),'N/A') AS 'IDENTIFICACION MEDICO',--[IdentificacionMedico],
		ISNULL(RTRIM(E.NOMMEDICO),'N/A') AS 'MEDICO',--[Medico] ,
		ISNULL(isnull(RTRIM(ter.codespeci), RTRIM(B.CODESPECI)),'N/A') AS 'CODIGO ESPECIALIDAD',--[CodEspecialidad] ,
		ISNULL(isnull(RTRIM(ter.desespeci), RTRIM(B.DESESPECI)),'N/A') AS 'ESPECIALIDAD',--[Especialidad],
		CASE 
			WHEN A.TIPSOLICITU = 1 THEN 'Cita Medica' 
			WHEN A.TIPSOLICITU = 2 THEN 'Cita Apoyo Diagnostico'
			WHEN A.TIPSOLICITU = 3 THEN 'Cita Tratamiento Especiales' END AS 'TIPO SOLICITUD',--[TipoSolicitud],
		CASE 
			WHEN A.TIPTRATAMIENTO = 1 THEN 'Quimioterapia' 
			WHEN A.TIPTRATAMIENTO = 2 THEN 'RadioTerapia' 
			WHEN A.TIPTRATAMIENTO = 3 THEN 'Diálisis' 
			WHEN A.TIPTRATAMIENTO = 4 THEN 'Braquiterapia' 
			ELSE 'Consulta Externa' END AS 'TIPO ATENCION',--[TipoAtencion] ,
		A.FECITADES AS 'FECHA DESEADA CITA',--[FechaDeseadaCita], 
		A.FECHAOFERTADA AS 'FECHA OFERTADA',--[FechaOfertada],
		FECHORAIN AS 'FECHA CITA',--[FechaCita],
		A.FECREGSIS AS 'FECHA REGISTRO DB',--[FechaRegistroDB],
		'' 'FECHA REALIZACION CIRUGIA',--[FechaRealizacionCirugia],
		DATEDIFF(DAY,A.FECREGSIS,FECHORAIN) AS 'DIAS DESDE AGENDAMIENTO',--[DiasDesdeAgendamiento],
		DATEDIFF(DAY,A.FECREGSIS,A.FECHAOFERTADA) AS 'DIAS DESDE FECHA OFERTADA',--[DiasDesdeFechaOfertada],
		ISNULL(RTRIM(J.CODIGOCON),'N/A') AS 'CODIGO CONSULTORIO SALA',--[CodConsultorioSala],
		ISNULL(RTRIM(J.DESCRICON),'N/A') AS 'CONSULTORIO SALA',--[ConsultorioSala],
		ISNULL(RTRIM(F.CODACTMED),'N/A') AS 'CODIGO ACTIVIDAD AGENDAMIENTO',--[CodActividadAgendamiento],
		ISNULL(RTRIM(F.DESACTMED),'N/A') AS 'ACTIVIDAD AGENDAMIENTO',--[ActividadAgendamiento],
		CASE WHEN A.CODTIPSOL = '0' THEN 'Presencial' WHEN A.CODTIPSOL = '1' THEN 'Telefónica' END AS 'FORMA COLICITUD',--[FormaSolicitud],
		CASE WHEN A.CODTIPCIT = '0' THEN 'Primera Vez' WHEN A.CODTIPCIT = '1' THEN 'Control' WHEN A.CODTIPCIT = '2' THEN 'Pos Operatorio' ELSE 'N/A' END AS 'TIPO CITA',--[TipoCita],
		CASE 
			WHEN A.CODESTCIT = '0' THEN 'Asignada' 
			WHEN A.CODESTCIT = '1' THEN 'Cumplida' 
			WHEN A.CODESTCIT = '2' THEN 'Incumplida'
			WHEN A.CODESTCIT = '3' THEN 'PreAsignada' 
			WHEN A.CODESTCIT = '4' THEN 'Cancelada' END AS 'ESTADO CITA',--[EstadoCita] ,
		CASE A.MODALIDAD WHEN 0 THEN 'Presencial' WHEN 1 THEN 'Teleconsulta' ELSE 'N/A' END AS 'MODALIDAD',--[Modalidad],
		CASE WHEN A.CONFASIST = '1' THEN 'Confirmada' WHEN A.CONFASIST = '2' THEN 'Cancelada' WHEN A.CONFASIST = '3' THEN 'Sin Definir' END AS 'ASISTENCIA',--[Asistencia],
		CASE WHEN A.CITAEXTRA = 1 THEN 'Si' ELSE 'No' END AS 'CITA EXTRA',--[CitaExtra] , 
		RTRIM(A.OBSERVACI) AS 'OBSERVACIONES CITA',--[ObservacionCita] ,
		RTRIM(G.NOMUSUARI) AS 'USUARIO REGISTRO CITA',--[UsuarioRegistroCita] , 
		RTRIM(M.CODSERIPS) AS 'CODIGO CUPS',--[CodigoCUPS], 
		RTRIM(M.DESSERIPS) AS 'CUPS',--[CUPS],
		CONCAT(RTRIM(K.CODIGSALA), ' - ', RTRIM(K.DESCRIPSAL)) AS 'SALA',--[Sala],
		CONCAT(RTRIM(L.CODEQUIPO) , ' - ', RTRIM(L.DESCREQUI)) AS 'EQUIPO TRATAMIENTO',--[EquipoTratamiento],
		RTRIM(H.NOMUSUARI) AS 'USUARIO CANCELA',--[UsuarioCancela],
		A.FECHCANCELA AS 'FECHA CANCELACION',--[FechaCancelacion],
		RTRIM(P.DESCAUCAN) AS 'CAUSA CANCELACION',--[CausaCancelacion], 
		RTRIM(A.OBSCAUCAN) AS 'OBSERVACIONES CANCELACION',--[ObservacionCancelacion] ,
		RTRIM(A.CODCAUINA) AS 'CAUSA INANTENCION',--[CausaInatencion], 
		RTRIM(A.OBSCAUINA) AS 'OBSERVACION INATENCION',--[ObservacionInatencion] ,
		RTRIM(I.NOMUSUARI) AS 'USUARIO REGISTRA INATENCION',--[UsuarioRegistraInatencion],
		A.FECHAINA AS 'FECHA REGISTRO INATENCION',--[FechaRegistroInatencion], 
		Y.FECHAREGISTRO AS 'FECHA ORDEN QUIMIOTERAPIA',--[FechaOrdenQuimioterapia], 
		CASE WHEN Y.CICLOACTUAL IS NULL THEN NULL ELSE CONCAT('Ciclo ', Y.CICLOACTUAL, ' de ' , Y.CICLOS) END AS 'CICLO',--[Ciclo]
		CAST(A.FECHORAIN AS DATE) [FECHA BUSQUEDA],
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL

	FROM AGASICITA AS A with (nolock) INNER JOIN
	ADCENATEN AS C with (nolock) ON A.CODCENATE = C.CODCENATE INNER JOIN
	INPACIENT AS D with (nolock) ON A.IPCODPACI = D.IPCODPACI LEFT JOIN
	INUBICACI AS UBI with (nolock) ON D.AUUBICACI =UBI.AUUBICACI LEFT JOIN
	INMUNICIP as MUN with (nolock) ON UBI.DEPMUNCOD =MUN.DEPMUNCOD LEFT JOIN
	INDEPARTA AS DEP  with (nolock) ON DEP.depcodigo =MUN.DEPCODIGO LEFT JOIN
	ADCONCOEX AS CS ON CS.NUMCONCIT = A.CODAUTONU LEFT JOIN
	INESPECIA AS B with (nolock) ON A.CODESPECI = B.CODESPECI LEFT JOIN
	inespecia as ter with (nolock) ON A.CODESPECI = ter.codespeci left join
	INPROFSAL AS E with (nolock) ON A.CODPROSAL = E.CODPROSAL LEFT JOIN
	AGACTIMED AS F with (nolock) ON A.CODACTMED = F.CODACTMED INNER JOIN
	SEGusuaru AS G with (nolock) ON A.CODUSUASI = G.CODUSUARI LEFT JOIN
	SEGusuaru AS H with (nolock) ON A.CANCELUSU = H.CODUSUARI LEFT JOIN
	SEGusuaru AS I with (nolock) ON A.CODUSUINA = I.CODUSUARI LEFT JOIN
	AGCONSULT  AS J with (nolock)ON A.CODIGOCON = J.CODIGOCON AND A.CODCENATE = J.CODCENATE LEFT JOIN
	AGENSALAC AS K with (nolock) ON A.IDSALA = K.CODCONCEC AND A.CODCENATE = K.CODCENATE LEFT JOIN
	AGEQUIPTRA AS L with (nolock) ON A.IDEQUIPOTRA = L.ID LEFT JOIN
	INCUPSIPS AS M with (nolock) ON A.CODSERIPS = M.CODSERIPS LEFT JOIN
	INDIAGNOS AS N with (nolock) ON A.CODDIAGNO = N.CODDIAGNO LEFT JOIN
	RIASCUPS AS O with (nolock) ON A.IDRIASCUPS = O.ID LEFT JOIN
	RIAS AS Q with (nolock) ON O.IDRIAS = Q.ID LEFT JOIN
	AGCAUCANC AS P with (nolock) ON A.CODCAUCAN = P.CODCAUCAN LEFT JOIN
	INUNIFUNC AS UF1 with (nolock) ON J.UFUCODIGO = UF1.UFUCODIGO LEFT JOIN
	INUNIFUNC AS UF2 with (nolock) ON K.UFUCODIGO = UF2.UFUCODIGO  LEFT JOIN
	INENTIDAD AS ENT with (nolock) ON D.CODENTIDA = ENT.CODENTIDA LEFT JOIN
	Contract.HealthAdministrator AS HEA with (nolock) ON  A.GENCONENTITY = HEA.Id LEFT JOIN
	Contract .CareGroup AS CG ON CG.Id =D.GENCAREGROUP LEFT JOIN
	EHR.HCORDCICLOSD AS Z with (nolock) ON Z.ID = A.IDHCORDCICLOSD  LEFT JOIN
	EHR.HCORDQUIMIO AS Y with (nolock) ON Y.ID  = Z.IDHCORDQUIMIO  LEFT JOIN
	Report.Table_HOMO_GRUPO_ETARIO AS HGE WITH (NOLOCK) ON DATEDIFF(YEAR, D.IPFECNACI, A.FECHORAIN) = HGE.GRUPO_ETAREO_EDAD LEFT OUTER JOIN
	Report.Table_DIM_GRUPO_ETAREO AS DGE WITH (NOLOCK) ON HGE.ID_GRUPO_ETAREO = DGE.ID_GRUPO_ETAREO
	where  D.IPCODPACI NOT IN ('0002', '100200', '000000000000000', '0000000', '00000000', '00000123', '862000005', '00000') AND
	YEAR(A.FECHORAIN)>=2023
--	CAST(A.FECHORAIN AS DATE)  BETWEEN @FechaInicio AND @FechaFin AND 

	UNION

	SELECT   CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		CASE D.IPTIPODOC 	
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
			WHEN 15 THEN 'SI' END AS [TipoIdentificacion], 
		D.IPCODPACI AS [NRO IDENTIFICACION],
		RTRIM(D.IPPRIAPEL) AS [PRIMER APELLIDO],
		RTRIM(D.IPSEGAPEL) AS [SEGUNDO APELLIDO], 
		RTRIM(D.IPPRINOMB) AS [PRIMER NOMBRE] , 
		RTRIM(D.IPSEGNOMB) AS [SEGUNDO NOMBRE], 
		CAST(D.IPFECNACI AS DATE) AS [FECHA NACIMIENTO],
		DATEDIFF(YEAR, D.IPFECNACI, GETDATE()) AS [EDAD],
		CASE D.IPSEXOPAC WHEN '1' THEN 'H' WHEN '2' THEN 'M' END AS [CODIGO SEXO],
		CASE D.IPSEXOPAC WHEN '1' THEN 'HOMBRE' WHEN '2' THEN 'MUJER' END AS [SEXO],
		DEP.depcodigo AS [CODIGO DEPARTAMENTO],
		DEP.nomdepart AS [NOMBRE DEPARTAMENTO RESIDENCIA],
		MUN.MUNCODIGO [CODIGO MUNICIPIO RESIDENCIA], 
		MUN.MUNNOMBRE AS [NOMBRE MUNICIPIO RESIDENCIA],
		UPPER(D.IPDIRECCI) AS [DIRECCION], 
		D.IPTELMOVI AS CELULAR, 
		D.IPTELEFON AS [TELEFONO FIJO],
		CASE 
			WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.HealthEntityCode) 
			ELSE  RTRIM(ENT.CODENTIDA) END AS [CODIGO ENTIDAD],
		CASE 
			WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.Name)	
			ELSE RTRIM(ENT.NOMENTIDA)	END AS [ENTIDAD],
		CG.Name AS [GRUPO ATENCION],
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
			WHEN 12 THEN 'Otros' end as [REGIMEN],
		RTRIM(CEN.CODCENATE) AS 'CODIGO CENTRO ATENCION',
		RTRIM(CEN.NOMCENATE) AS 'CENTRO ATENCION',
		RTRIM(UF1.UFUDESCRI) 'UNIDAD FUNCIONAL',
		ISNULL(AGE.NUMINGRES, INF2.NUMINGRES ) AS [INGRESO], 
		RTRIM(E.CODPROSAL) AS [IDENTIFICACION PROFESIONAL],
		RTRIM(E.NOMMEDICO) AS [PROFESIONAL],
		isnull(RTRIM(ter.codespeci ),RTRIM(B.CODESPECI)) AS [CODIGO ESPECIALIDAD],
		isnull(RTRIM(ter.desespeci),RTRIM(B.DESESPECI)) AS [ESPECIALIDAD],
		'Cita Cirugia' [TIPO DE SOLICITUD],
		'Programacion Cirugia' [TIPO DE CITA],
		AGE.FECHORAIN AS [FECHA DESEADA DE CITA], 
		AGE.FECHORAIN AS [FECHA OFERTADA],
		FECHORAIN AS [FECHA DE CITA],
		AGE.FECREGSIS AS [FECHA DE REGISTRO BD],
		ISNULL(INF.FECHORINI,INF2.FECHORINI )[FECHA REALIZACION QX],
		DATEDIFF(DAY,AGE.FECREGSIS,FECHORAIN) AS [DIAS DESDE AGENDAMIENTO],
		DATEDIFF(DAY,AGE.FECREGSIS,AGE.FECHORAIN) AS [DIAS DESDE FECHA OFERTADA],
		SAL.CODIGSALA  [CODIGO CONSULTORIO] ,
		SAL.DESCRIPSAL  [CONSULTORIO/SALA],
		'349' AS [CODIGO ACTIVIDAD AGENDAMIENTO] ,
		'PROCEDIMIENTOS QUIRURGICO ODO' AS [ACTIVIDAD DE AGENDAMIENTO],
		'N/A' AS [FORMA DE SOLICITUD],
		'Primera Vez' [TIPO DE CITA], 
		CASE
			WHEN AGE.CODESTPQX  = '0' THEN 'Cirugia Programada'	
			WHEN AGE.CODESTPQX  = '1' THEN ' Paciente admitido (Cirugia Origen Ambulatoria)'	
			WHEN AGE.CODESTPQX = '2' THEN 'Paciente en sala de espera'
			WHEN AGE.CODESTPQX = '3' THEN 'Paciente en sala quirurgica' 
			WHEN AGE.CODESTPQX = '4' THEN 'Paciente en recuperación'		
			WHEN AGE.CODESTPQX = '5' THEN 'Paciente con alta'
			WHEN AGE.CODESTPQX = '6' THEN 'Cancelada' END AS [ESTADO DE CITA],
		'Presencial' AS [MODALIDAD],
		CASE
			WHEN AGE.CODESTPQX  = '0' THEN 'Sin Definir'	
			WHEN AGE.CODESTPQX  = '1' THEN 'Confirmada'	
			WHEN AGE.CODESTPQX = '2' THEN 'Confirmada'
			WHEN AGE.CODESTPQX = '3' THEN 'Confirmada' 
			WHEN AGE.CODESTPQX = '4' THEN 'Confirmada'		
			WHEN AGE.CODESTPQX = '5' THEN 'Confirmada'
			WHEN AGE.CODESTPQX = '6' THEN 'Cancelada' END AS [CONFIRMAR ASISTENCIA],
		'N/A' AS [CITA EXTRA] , 	
		RTRIM(AGE.OBSERVACION ) AS [OBSERVACION DE CITA] , 	
		RTRIM(G.NOMUSUARI) AS [USUARIO QUE REGISTRO LA CITA],
		RTRIM(AGE.CODSERIPS) AS [CODIGO CUPS],	
		RTRIM(M.DESSERIPS) AS [CUPS],
		SAL.DESCRIPSAL AS [SALA],
		'N/A' AS [EQUIPO DE TRATAMIENTO],	
		RTRIM(H.NOMUSUARI) AS [USUARIO QUE CANCELA] ,
		AGE.FECHACAN AS [FECHA DE CANCELACION],
		RTRIM(P.DESCAUCAN) AS [CAUSA DE CANCELACION] ,	
		RTRIM(AGE.OBSERCAN) AS [OBSERVACIONES CANCELACION] ,
		RTRIM(AGE.CODCAUINA) AS [CAUSA DE INATENCION],	
		RTRIM(AGE.OBSCAUINA) AS [OBSERVACIONES INATENCION] ,
		RTRIM(I.NOMUSUARI) AS [USUARIO REGISTRA INATENCION],
		AGE.FECHAINA AS [FECHA REGISTRO INATENCION], 
		NULL, 
		NULL,
		CAST(AGE.FECHORAIN AS DATE) [FECHA BUSQUEDA],
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM DBO.AGEPROGQX AS AGE INNER JOIN 
	ADCENATEN AS CEN with (nolock) ON AGE.CODCENATE = CEN.CODCENATE INNER JOIN
	AGENSALAC AS SAL with (nolock) ON AGE.AGENSALAC =SAL.CODCONCEC LEFT JOIN 
	INUNIFUNC AS UF1 with (nolock) 	ON SAL.UFUCODIGO = UF1.UFUCODIGO INNER JOIN 
	INPACIENT AS D with (nolock) ON AGE.IPCODPACI = D.IPCODPACI LEFT JOIN 
	INUBICACI AS UBI with (nolock) ON D.AUUBICACI =UBI.AUUBICACI LEFT JOIN
	INMUNICIP as MUN with (nolock) ON UBI.DEPMUNCOD =MUN.DEPMUNCOD LEFT JOIN
	INDEPARTA AS DEP  with (nolock) ON DEP.depcodigo =MUN.DEPCODIGO LEFT JOIN 
	INENTIDAD AS ENT with (nolock) ON D.CODENTIDA = ENT.CODENTIDA	LEFT JOIN 
	Contract.HealthAdministrator AS HEA with (nolock)	ON  D.GENCONENTITY = HEA.Id LEFT JOIN
	Contract .CareGroup AS CG ON CG.Id =D.GENCAREGROUP LEFT JOIN
	INESPECIA AS B with (nolock) ON AGE.CODESPECI = B.CODESPECI LEFT JOIN 
	inespecia as ter with (nolock) ON AGE.CODESPECI =ter.codespeci left join
	INPROFSAL AS E with (nolock) ON AGE.CODPROSAL = E.CODPROSAL INNER JOIN 
	SEGusuaru AS G with (nolock) ON AGE.CODUSUASI = G.CODUSUARI LEFT JOIN 
	INCUPSIPS AS M with (nolock) ON AGE.CODSERIPS = M.CODSERIPS LEFT JOIN 
	SEGusuaru AS H with (nolock) ON AGE.CODUSUCAN  = H.CODUSUARI LEFT JOIN 
	AGCAUCANC AS P with (nolock)	ON AGE.CODCAUCAN = P.CODCAUCAN  LEFT JOIN 
	SEGusuaru AS I with (nolock) ON AGE.CODUSUINA = I.CODUSUARI LEFT JOIN
	( SELECT INF.IPCODPACI ,INF.NUMINGRES ,INF.FECHORINI ,INF.CODSERIPS,INF.CODCENATE ,INF.CODDIAPRE ,INF.CODDIAPOS 
	FROM HCQXINFOR INF with (nolock)) AS INF ON AGE.NUMINGRES =INF.NUMINGRES LEFT JOIN
	( SELECT PQX.AUTO, PQX.IPCODPACI ,PQX.NUMINGRES,QX.FECHORINI   FROM dbo.HCORDPROQ AS PQX LEFT JOIN
	HCQXINFOR AS QX ON QX.NUMINGRES =PQX.NUMINGRES AND QX.CODSERIPS =PQX.CODSERIPS ) AS INF2 ON AGE.AUTOHCORDPROQ =INF2.AUTO
	WHERE AGE.PRINCIPAL =1
	and D.IPCODPACI NOT IN ('0002', '100200', '000000000000000', '0000000', '00000000', '00000123', '862000005', '00000')
	AND YEAR(AGE.FECHORAIN)>=2023
--	CAST(AGE.FECHORAIN AS DATE) BETWEEN @FechaInicio AND @FechaFin AND GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada para cubo BI que unifica el agendamiento clínico (citas médicas, apoyo diagnóstico, tratamientos especiales y programación de cirugías) desde 2023 con datos demográficos, administrativos y de cancelación/inatención.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalFinalSchedulingReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas maestras (centros, pacientes, entidades, especialidades, profesionales, salas, CUPS, causas) deben estar pobladas para enriquecer la información vía LEFT JOIN.; Existencia de tablas de homologación Report.Table_HOMO_GRUPO_ETARIO y Report.Table_DIM_GRUPO_ETAREO para clasificación etaria.; La zona horaria ''Pakistan Standard Time'' debe estar disponible en el servidor SQL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalFinalSchedulingReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye siempre pacientes con identificaciones consideradas inválidas o de prueba (''0002'',''100200'',''000000000000000'',''0000000'',''00000000'',''00000123'',''862000005'',''00000'').; Solo devuelve agendamientos cuyo año de FECHORAIN sea 2023 o superior.; En la rama de cirugías sólo se consideran procedimientos marcados como principales (PRINCIPAL = 1).; Las cirugías se clasifican siempre como ''Cita Cirugia'', ''Programacion Cirugia'', actividad ''349 - PROCEDIMIENTOS QUIRURGICO ODO'' y modalidad ''Presencial''.; La edad del paciente se calcula con DATEDIFF(YEAR, fecha nacimiento, GETDATE()) y para grupo etario contra la fecha de la cita.; La marca de tiempo de actualización (ULT_ACTUAL) usa la zona horaria ''Pakistan Standard Time''.; El ID_COMPANY se deriva del nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalFinalSchedulingReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalFinalSchedulingReport: Devuelve UNION de citas (AGASICITA) y programación de cirugías (AGEPROGQX) excluyendo identificaciones genéricas/dummy (''0002'',''100200'',''000000000000000'',''0000000'',''00000000'',''00000123'',''862000005'',''00000'') y filtrando YEAR(FECHORAIN) >= 2023.; [RETURN_RESULT] Report.UploadCubeVieClinicalFinalSchedulingReport: En el bloque de cirugías sólo se incluyen registros donde AGEPROGQX.PRINCIPAL = 1.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalFinalSchedulingReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC entre 1 y 15 → Mapea a códigos de tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI); si IPSEXOPAC = ''1'' o ''2'' → Clasifica como HOMBRE (H) o MUJER (M) else NULL; si HEA.Id IS NOT NULL → Usa código y nombre de Contract.HealthAdministrator como entidad else Usa CODENTIDA/NOMENTIDA de INENTIDAD; si HEA.EntityType entre 1 y 12 → Clasifica el régimen (EPS Contributivo, EPS Subsidiado, ET Vinculados, ARL, MP, IPS Privada/Pública, Régimen Especial, Accidentes de Tránsito, Fosyga, Otros); si UF1.UFUCODIGO o UF2.UFUCODIGO presentes (consultorio o sala) → Asigna unidad funcional desde consultorio (UF1) o sala (UF2) else Etiqueta como ''CONSULTA EXTERNA''; si A.TIPSOLICITU IN (1,2,3) → Clasifica como Cita Médica, Cita Apoyo Diagnóstico o Cita Tratamiento Especiales; si A.TIPTRATAMIENTO entre 1 y 4 → Clasifica como Quimioterapia, Radioterapia, Diálisis o Braquiterapia else ''Consulta Externa''; si A.CODTIPSOL = ''0'' o ''1'' → Forma de solicitud Presencial o Telefónica; si A.CODTIPCIT in (''0'',''1'',''2'') → Tipo de cita Primera Vez, Control o Pos Operatorio else ''N/A''; si A.CODESTCIT entre ''0'' y ''4'' → Estado de cita: Asignada, Cumplida, Incumplida, PreAsignada o Cancelada; si A.MODALIDAD = 0 o 1 → Modalidad Presencial o Teleconsulta else ''N/A''; si A.CONFASIST in (''1'',''2'',''3'') → Asistencia Confirmada, Cancelada o Sin Definir; si A.CITAEXTRA = 1 → Marca cita como extra (Si) else No; si Y.CICLOACTUAL IS NOT NULL → Construye etiqueta ''Ciclo X de Y'' para tratamientos de quimioterapia else NULL; si AGE.CODESTPQX entre ''0'' y ''6'' → Estado quirúrgico: Cirugía Programada, Paciente admitido, En sala de espera, En sala quirúrgica, En recuperación, Con alta o Cancelada; si AGE.CODESTPQX = ''0'' → Confirmar asistencia = ''Sin Definir'' else Si CODESTPQX entre ''1'' y ''5'' => ''Confirmada''; si ''6'' => ''Cancelada''; si ter.codespeci tiene valor → Usa especialidad terapéutica (ter) else Usa especialidad de B.CODESPECI; si ambos nulos, ''N/A''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalFinalSchedulingReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalFinalSchedulingReport';
GO
