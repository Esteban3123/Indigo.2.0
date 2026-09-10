

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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a cubos analíticos que consolida información de cirugías realizadas por paciente, combinando datos demográficos, identificación, aseguradora/régimen, centro y unidad funcional, especialidad, médico tratante, procedimientos CUPS ejecutados, diagnósticos pre y posoperatorios CIE-10, tiempos de inicio/fin de cirugía y anestesia. Sirve como fuente aplanada para alimentar un cubo OLAP o herramienta de BI con registros quirúrgicos de la institución.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSurgeriesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSurgeriesPerformed';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida cirugías realizadas con datos demográficos del paciente, EPS/régimen, especialidad, médico, sala, tiempos quirúrgicos y de anestesia, diagnósticos pre/pos y CUPS, para alimentar un cubo de reportes.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSurgeriesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada cirugía (HCQXINFOR) debe tener un folio de historia clínica activo en hchispaca con estafolio=1 y mismo numingres/numefolio.; El paciente debe tener entidad en INENTIDAD y pagador en Contract.HealthAdministrator (JOIN exige ambos).; Debe existir al menos un procedimiento realizado en HCQXREALI con su CUPS en INCUPSIPS.; Los diagnósticos pre y pos (CODDIAPRE, CODDIAPOS) deben existir en INDIAGNOS.; El centro de atención y la ubicación del paciente deben tener municipio/departamento válidos en INMUNICIP/INDEPARTA.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSurgeriesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La edad se calcula con DATEDIFF(YEAR, IPFECNACI, GETDATE()) sin ajuste por mes/día.; La fecha de última actualización (ULT_ACTUAL) se entrega convertida a la zona horaria ''Pakistan Standard Time''.; ID_COMPANY se reporta como los primeros 9 caracteres de DB_NAME().; Solo se incluyen registros con folio de historia clínica activo (estafolio = 1).; La ''FECHA PATOLOGIA'' se obtiene como TOP 1 de hconcopreg filtrando por paciente y diagnóstico pre, sin orden definido.; La hospitalización/cirugía sólo aparece si el paciente tiene tanto entidad (INENTIDAD) como administradora de salud (HealthAdministrator) y grupo de atención (CareGroup) asignados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSurgeriesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cirugía / Procedimiento quirúrgico; Anestesia (inicio/fin); Diagnóstico pre y pos operatorio (CIE-10); CUPS; Especialidad médica; Médico tratante; EPS / Administradora de salud; Régimen (Contributivo, Subsidiado, ARL, Prepagada, etc.); Centro de atención y unidad funcional; Sala de cirugía; Folio de historia clínica; Patología; Grupo de atención', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSurgeriesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalSurgeriesPerformed: Devuelve una fila por cada par (cirugía, procedimiento CUPS realizado) cumpliendo los INNER JOIN; la anestesia se vincula opcionalmente por (numingres, codserips=codcirugi).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSurgeriesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si D.IPTIPODOC entre 1..15 → Mapea a etiquetas de tipo de identificación: 1=CC, 2=CE, 3=TI, 4=RC, 5=PA, 6=AS, 7=MS, 8=NU, 9=CN, 10=CD, 11=SC, 12=PE, 13=PT, 14=DE, 15=SI else NULL; si D.IPSEXOPAC = ''1'' o ''2'' → Traduce a ''HOMBRE'' o ''MUJER'' else NULL; si HEA.Id IS NOT NULL → Toma código y nombre de EPS desde Contract.HealthAdministrator (HealthEntityCode/Name) else Usa CODENTIDA/NOMENTIDA de INENTIDAD; si HEA.EntityType ∈ {1..12,99} → Clasifica el régimen (Contributivo, Subsidiado, ARL, Prepagada, IPS Privada/Pública, Régimen Especial, SOAT, Fosyga, Particulares, etc.) else ''Otros''; si REA.QXPRINCIP = 1 → Marca la cirugía como principal (''SI'') else ''NO''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSurgeriesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.hcreganes; dbo.hcregicir; dbo.HCQXINFOR; dbo.ADCENATEN; dbo.INMUNICIP; dbo.INPACIENT; dbo.INUBICACI; dbo.INDEPARTA; dbo.INUNIFUNC; dbo.INENTIDAD; Contract.HealthAdministrator; Contract.CareGroup; dbo.INPROFSAL; dbo.hchispaca; dbo.INESPECIA; dbo.INDIAGNOS; dbo.HCQXREALI; dbo.INCUPSIPS; dbo.hconcopreg', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSurgeriesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSurgeriesPerformed';
GO
