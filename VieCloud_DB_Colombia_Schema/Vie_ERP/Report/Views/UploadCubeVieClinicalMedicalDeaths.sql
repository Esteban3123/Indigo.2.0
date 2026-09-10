
	CREATE view [Report].[UploadCubeVieClinicalMedicalDeaths] AS

	SELECT DISTINCT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		CASE p.iptipodoc 
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
		p.ipcodpaci AS 'NRO IDENTIFICACION',--[NroIdentificacion],
		RTRIM(p.ipnomcomp) AS 'NOMBRE',--[Nombre],
		p.ipfecnaci AS 'FECHA NACIMIENTO',--[FechaNacimiento],
		CASE p.ipsexo WHEN 'M' THEN 'FEMENINO' WHEN 'H' THEN 'MASCULINIO' END AS 'SEXO',--[Sexo],
		mun.munnombre AS 'MINICIPIO',--[Municipio],
		dep.nomdepart AS 'DEPARTAMENTO',--[Departamento],
		UPPER(p.ipdirecci) AS 'DIRECCION RESIDENCIA',--[DireccionResidencia],
		p.iptelmovi AS 'TELEFONO PRINCIPAL',--[TelefonoPrincipal],
		p.iptelefon AS 'TELEFONO ALTERNATIVO',--[TelefonoAlternativo],
		ha.code AS 'CODIGO EPS',--[CodigoEPS],
		ha.name 'NOMBRE EPS',--[NombreEPS],
		CASE ha.entitytype
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
			WHEN 12 THEN 'Otros' END AS 'REGIMEN EPS',--[RegimenEPS],
		cg.code AS 'CODIGO GRUPO ATENCION',--[CodigoGrupoAtencion],
		cg.name 'NOMBRE GRUPO ATENCION',--[NombreGrupoAtencion],
		CASE cg.liquidationtype
			WHEN 1 THEN 'Pago por Servicios'
			WHEN 2 THEN 'Capitacion'
			WHEN 3 THEN 'Factura Global'
			WHEN 4 THEN 'Capitacion Global'
			WHEN 5 THEN 'Pago Global Prospectivo - PGP' END AS 'TIPO LIQUIDACION',--[TipoLiquidacion],
		ing.numingres AS 'INGRESO',--[Ingreso],
		ing.ifechaing AS 'FECHA INGRESO',--[FechaIngreso],
		ca.nomcenate AS 'CENTRO ATENCION',--[CentroAtencion],
		ufu.ufudescri AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
		RTRIM(med.codprosal) + ' - ' +  RTRIM(med.nommedico) AS 'MEDICO REGISTRA DEFUNCION',--[MedicoRegistraDefuncion],
		em.desespeci AS 'ESPECIALIDAD MEDICO',--[EspecialidadMedico],
		egr.fecmuepac AS 'FECHA DEFUNCION',--[FechaDefuncion],
		egr.numcerdef AS 'NRO CERTIFICADO DEFUNCION',--[NroCertificadoDefuncion],
		CAST(egr.fecmuepac AS DATE) [FECHA BUSQUEDA],
        CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM dbo.hcregegre AS egr 
	INNER JOIN dbo.inpacient AS p ON egr.ipcodpaci = p.ipcodpaci 
	LEFT JOIN dbo.inubicaci AS ubi WITH (NOLOCK) ON p.auubicaci = ubi.auubicaci 
	LEFT JOIN dbo.inmunicip AS mun WITH (NOLOCK) ON ubi.depmuncod = mun.depmuncod
	LEFT JOIN dbo.indeparta AS dep WITH (NOLOCK) ON mun.depcodigo = dep.depcodigo
	INNER JOIN dbo.adingreso AS ing WITH (NOLOCK) ON egr.numingres = ing.numingres
	INNER JOIN contract.healthadministrator AS ha WITH (NOLOCK) ON ing.genconentity = ha.id
	INNER JOIN contract.caregroup AS cg WITH (NOLOCK) ON ing.gencaregroup = cg.id
	INNER JOIN dbo.adcenaten AS ca WITH (NOLOCK) ON ing.codcenate = ca.codcenate 
	INNER JOIN dbo.inunifunc AS ufu ON ing.ufucodigo = ufu.ufucodigo
	INNER JOIN dbo.inprofsal AS med WITH (NOLOCK) ON egr.codprosal = med.codprosal 
	INNER JOIN dbo.inespecia AS em WITH (NOLOCK) ON med.codespec1 = em.codespeci 
	WHERE egr.estpacegr = 3 
	--AND CAST(egr.fecmuepac AS DATE) BETWEEN @inidate AND @enddate
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista orientada a reporting/cubos de datos que consolida los registros de defunción hospitalaria, filtrando egresos con estado de paciente igual a 3 (fallecido). Aplana en una sola fila por paciente fallecido su información demográfica (identificación, sexo, fecha de nacimiento, municipio, departamento), datos del ingreso (centro de atención, unidad funcional, EPS con régimen y tipo de liquidación), y datos del deceso (fecha de defunción, número de certificado y médico que registra la defunción con su especialidad). Incluye marca de última actualización en zona horaria de Pakistán, lo que sugiere consumo por un proceso ETL externo o cubo OLAP.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalDeaths';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalDeaths';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, para alimentar un cubo analítico, el listado de pacientes fallecidos con sus datos demográficos, ingreso, EPS, grupo de atención, centro/unidad funcional y médico que registró la defunción.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalDeaths';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El egreso debe tener estado de paciente egresado igual a 3 (fallecido) — egr.estpacegr = 3; El paciente debe existir en inpacient (INNER JOIN por ipcodpaci); El ingreso asociado al egreso debe existir en adingreso (INNER JOIN por numingres); La entidad administradora de salud (genconentity) y el grupo de atención (gencaregroup) del ingreso deben estar registrados en contract.healthadministrator y contract.caregroup; El médico que registra el egreso debe existir en inprofsal y tener especialidad principal (codespec1) válida en inespecia', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalDeaths';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan egresos cuyo estado de paciente al egreso es 3 (fallecidos); ID_COMPANY se deriva de DB_NAME() truncado a VARCHAR(9), identificando la base/empresa origen; ULT_ACTUAL se calcula con GETDATE() convertido a la zona horaria ''Pakistan Standard Time''; FECHA BUSQUEDA es el CAST a DATE de fecmuepac (fecha de muerte del paciente); El código de mapeo de sexo invierte la convención habitual: ''M'' se etiqueta como FEMENINO y ''H'' como MASCULINIO; El médico que registra la defunción se presenta como ''codprosal - nommedico'' usando su especialidad principal (codespec1); La dirección de residencia se normaliza a mayúsculas (UPPER) y el nombre se entrega sin espacios finales (RTRIM)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalDeaths';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Defunción / Fallecimiento de paciente; Certificado de defunción; Egreso clínico; Paciente; Ingreso hospitalario; EPS / Entidad administradora de salud; Régimen de afiliación (Contributivo, Subsidiado, ARL, Medicina Prepagada, Fosyga, etc.); Grupo de atención y tipo de liquidación (Pago por Servicios, Capitación, PGP); Centro de atención y unidad funcional; Profesional de la salud y especialidad médica; Tipo de identificación (CC, CE, TI, RC, etc.); Ubicación geográfica (municipio/departamento)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalDeaths';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalMedicalDeaths: Devuelve filas DISTINCT solo cuando egr.estpacegr = 3 (egresos por defunción), enriquecidas con datos del paciente, ingreso, EPS, grupo de atención y médico', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalDeaths';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si p.iptipodoc IN (1..15) → Mapea el código numérico de tipo de documento a su sigla (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI) else NULL; si p.ipsexo = ''M'' / ''H'' → Traduce ''M'' a FEMENINO y ''H'' a MASCULINIO (sic) else NULL; si ha.entitytype IN (1..12) → Clasifica el régimen de la EPS (EPS Contributivo, Subsidiado, ET Vinculados, ARL, Medicina Prepagada, IPS Privada/Pública, Régimen Especial, Accidentes de tránsito, Fosyga, Otros) else NULL; si cg.liquidationtype IN (1..5) → Determina el tipo de liquidación del grupo de atención (Pago por Servicios, Capitación, Factura Global, Capitación Global, PGP) else NULL', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalDeaths';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.hcregegre; dbo.inpacient; dbo.inubicaci; dbo.inmunicip; dbo.indeparta; dbo.adingreso; contract.healthadministrator; contract.caregroup; dbo.adcenaten; dbo.inunifunc; dbo.inprofsal; dbo.inespecia', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalDeaths';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalDeaths';
GO
