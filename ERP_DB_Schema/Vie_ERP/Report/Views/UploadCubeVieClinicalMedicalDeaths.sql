
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