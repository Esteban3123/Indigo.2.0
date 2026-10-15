

CREATE view [Report].[UploadCubeVieClinicalSurgeriesPerformed] AS

	WITH hcanestesia AS 
	(
		SELECT 
			ane.idanestes,
			ane.numingres,
			hcr.codcirugi,
			ane.horinianes,
			ane.horfinanes 
		FROM dbo.hcreganes AS ane 
		INNER JOIN dbo.hcregicir AS hcr ON ane.idanestes = hcr.idregianes
		--WHERE CAST(ane.horinianes AS DATE) BETWEEN DATEADD(DD, -1, @DateStart) AND DATEADD(DD, 1, @DateEnd)
	)

	SELECT 
        CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		MUN2.MUNNOMBRE 'CIUDAD',
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
			WHEN 15 THEN 'SI' END AS 'TIPO IDENTIFICACION',-- [TipoIdentificacion], 
		
		D.IPCODPACI AS 'NRO IDENTIFICACION',--[NroIdentificacion], 
		RTRIM(D.IPNOMCOMP) AS 'NOMBRE',--[Nombre], 
		CAST(D.IPFECNACI AS DATE) AS 'FECHA NACIMIENTO',--[FechaNacimiento],
		DATEDIFF(YEAR, D.IPFECNACI, GETDATE()) AS 'EDAD',--[Edad],
		CASE D.IPSEXOPAC WHEN '1' THEN 'HOMBRE' WHEN '2' THEN 'MUJER' END AS 'SEXO',--[Sexo],
		DEP.nomdepart AS 'DEPARTAMENTO',--[Departamento], 
		MUN.MUNNOMBRE AS 'MUNICIPIO',--[Municipio],
		UPPER(D.IPDIRECCI) AS 'DIRECCION',--[Direccion], 
		D.IPTELMOVI AS 'TEL PRINCIPAL',--[TelPrincipal], 
		D.IPTELEFON AS 'TEL ALTERNATIVO',--TelAlternativo],
		CASE WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.HealthEntityCode ) ELSE RTRIM(ENT.CODENTIDA) END AS 'CODIGO EPS',--[CodEPS],
		CASE WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.Name) ELSE RTRIM(ENT.NOMENTIDA) END AS 'EPS',--[EPS],
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
			WHEN 99 THEN 'Particulares'
			WHEN 12 THEN 'Otros' else 'Otros' END AS 'REGIMEN',-- [Regimen],
		RTRIM(CG.Name) AS 'GRUPO ATENCION',--[GrpAtencion], 
		RTRIM(C.NOMCENATE) AS 'CENTRO ATENCION',--[CentroAtencion],
		RTRIM(UF1.UFUDESCRI) AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
		--REPS.[CodigoReps] AS [CodRepsEspecialidad],
		B.CODESPECI AS 'CODIGO ESPECIALIDAD',--[CodEspecialidad],
		RTRIM(B.DESESPECI) AS 'ESPECIALIDAD',--[Especialidad],
		RTRIM(E.NOMMEDICO) AS 'MEDICO',--[Medico],
		QX.NUMINGRES 'NRO INGRESO QUIRURGICO',--[NroIngresoQuirurgico],
		RTRIM(REA.CODSERIPS) AS 'CODIGO CUPS',--[CodCUPS],	
		RTRIM(M.DESSERIPS) AS 'DESCRIPCION CUPS',--[CUPS],
		CASE REA.QXPRINCIP WHEN 1 THEN 'SI' ELSE 'NO' END AS 'CIRUGIA PRINCIPAL',--[CirugiaPrincipal],
		qx.salacirug AS 'NRO SALA',--[NroSala],
		CAST(QX.FECHORINI AS DATE) AS 'FECHA INICIO CIRUGIA',--[FechaInicioCirugia], 
		CONVERT(varchar,QX.FECHORINI,20) AS 'FECHA HORA INICIO CIRUGIA',-- [FechaHoraInicioCirugia], 
		CAST(FECHORFIN AS DATE) AS 'FECHA FIN CIRUGIA',--[FechaFinCirugia],
		CONVERT(varchar,QX.FECHORFIN,20) AS 'FECHA HORA FIN CIRUGIA',--[FechaHoraFinCirugia],
		QX.CODDIAPRE AS 'CIE10 PRE',-- [CIE10Pre], 
		RTRIM(DIAI .NOMDIAGNO) AS 'DIAGNOSTICO PRE OPERATORIO',--[DiagnosticoPreoperatorio], 
		QX.CODDIAPOS 'CIE10 POS',--[CIE10Pos], 
		RTRIM(DIAE.NOMDIAGNO) AS 'DIAGNOSTICO POS OPERATORIO',--[DiagnosticoPosoperatorio],
		(SELECT TOP 1 CAST([23] AS VARCHAR(10)) FROM dbo.hconcopreg WHERE [6] = qx.ipcodpaci AND [17] = qx.coddiapre) AS 'FECHA PATOLOGIA',--[FechaPatologia],
		ane.horinianes AS 'FECHA INICIO ANESTESIA',--[FechaIncioAnestesia],
		ane.horfinanes AS 'FECHA FIN ANESTESIA',--[FechaFinAnestesia]
		CAST(QX.FECHORINI AS DATE) AS 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM DBO.HCQXINFOR AS QX WITH(nolock) 
	JOIN ADCENATEN AS C with (nolock) ON QX.CODCENATE = C.CODCENATE 
	INNER JOIN dbo.INMUNICIP AS MUN2 ON C.DEPMUNCOD=MUN2.DEPMUNCOD
	JOIN INPACIENT AS D with (nolock) ON QX.IPCODPACI = D.IPCODPACI
	JOIN INUBICACI AS UBI with (nolock) ON D.AUUBICACI =UBI.AUUBICACI
	JOIN INMUNICIP as MUN with (nolock) ON UBI.DEPMUNCOD =MUN.DEPMUNCOD 
	JOIN INDEPARTA AS DEP  with (nolock) ON DEP.depcodigo =MUN.DEPCODIGO 
	JOIN INUNIFUNC AS UF1 with (nolock) 	ON QX.UFUCODIGO = UF1.UFUCODIGO 
	JOIN INENTIDAD AS ENT with (nolock) ON D.CODENTIDA = ENT.CODENTIDA	
	JOIN Contract.HealthAdministrator AS HEA with (nolock) ON D.GENCONENTITY = HEA.Id 
	JOIN Contract .CareGroup AS CG ON CG.Id =D.GENCAREGROUP 
	JOIN INPROFSAL AS E with (nolock) ON QX.CODPROSAL = E.CODPROSAL
	INNER JOIN dbo.hchispaca AS hc ON qx.numingres = hc.numingres AND qx.numefolio = hc.numefolio AND estafolio = 1
	JOIN INESPECIA AS B with (nolock) ON hc.codesptra = b.codespeci
	JOIN INDIAGNOS AS DIAI with (nolock) ON QX.CODDIAPRE =DIAI.CODDIAGNO 
	JOIN INDIAGNOS AS DIAE with (nolock) ON QX.CODDIAPOS = DIAE.CODDIAGNO 
	JOIN HCQXREALI AS REA with (nolock) ON REA.NUMINGRES =QX.NUMINGRES  AND QX.NUMEFOLIO =REA.NUMEFOLIO 
	JOIN INCUPSIPS AS M with (nolock) ON REA.CODSERIPS = M.CODSERIPS
	--LEFT JOIN INDIGOREP.dbo.TablaEspecialidadesReps AS REPS ON B.CODESPECI=REPS.CodigoEspecialidad
	LEFT JOIN hcanestesia AS ane ON qx.numingres = ane.numingres AND qx.codserips = ane.codcirugi
	--WHERE CAST(QX.FECHORINI  AS date )  BETWEEN @DateStart AND @DateEnd