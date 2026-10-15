
--CREATE PROCEDURE [dbo].[ODO_Hemocomponetes_Aplicados]
	-- Add the parameters for the stored procedure here
--DECLARE	@inidate DATE='2024-06-01';
--DECLARE	@endate DATE ='2024-06-30';

CREATE view [Report].[UploadCubeVieClinicalMedicalAppliedBloodComponents] AS

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
		RTRIM(p.ipnomcomp)AS 'NOMBRE',--[Nombre],
		p.ipfecnaci AS 'FECHA NACIMIENTO',--[FechaNacimiento],
		CASE p.ipsexo WHEN 'M' THEN 'FEMENINO' WHEN 'H' THEN 'MASCULINIO' END AS 'SEXO',--Sexo,
		mun.munnombre AS 'MUNICIPIO',--[Municipio],
		dep.nomdepart AS 'DEPARTAMENTO',--[Departamento],
		p.ipdirecci AS 'DIRECCION RESIDENCIA',--[DireccionResidencia],
		p.iptelmovi AS 'TELEFONO PRINCIPAL',--[TelefonoPrincipal],
		p.iptelefon AS 'TELEFONO ALTERNATIVO',--[TelefonoAlternativo],
		ha.code AS 'CODIGO EPS',--[CodEPS],
		ha.name 'EPS',--[EPS],
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
		cg.code AS 'CODIGO GRUPO ATENCION',--[CodGrupoAtencion],
		cg.name 'GRUPO ATENCION',--[GrupoAtencion],
		CASE cg.liquidationtype
			WHEN 1 THEN 'Pago por Servicios'
			WHEN 2 THEN 'Capitacion'
			WHEN 3 THEN 'Factura Global'
			WHEN 4 THEN 'Capitacion Global'
			WHEN 5 THEN 'Pago Global Prospectivo - PGP' END AS 'TIPO LIQUIDACION',--[TipoLiquidacion],

		ai.numingres AS 'NRO INGRESO',--[NroIngreso],
		ai.ifechaing AS 'FECHA INGRESO',--[FechaIngreso],
		ca.nomcenate AS 'CENTRO ATENCION',--[CentroAtencion],
		ord.fecordmed AS 'FECHA ORDEN',--[FechaOrden],
		'Aplicado' AS 'ESTADO',--[Estado],
		cs.codcomsam + ' - ' + cs.descomsam AS 'COMPONENTE SANGUINEO',--[ComponenteSanguineo],
		bol.numbolsa AS 'NRO BOLSA',--[NroBolsa],
		bol.sellocalidad AS 'SELLO CALIDAD',--[SelloCalidad],
		RTRIM(med.codprosal) + ' - ' +  RTRIM(med.nommedico) AS 'MEDICO APLICA',--[MedicoAplica],
		em.desespeci AS 'ESPECIALIDAD MEDICO',--[EspecialidadMedico],
		bol.fecaplicmed AS 'FECHA APLICACION MEDICO',--[FechaAplicacionMedico],
		RTRIM(enf.codprosal) + ' - ' +  RTRIM(enf.nommedico) AS 'ENFERMERA APLICA',--[EnfermeraAplica],
		ee.desespeci AS 'ESPECIALIDAD ENFERMERA',--[EspecialidadEnfermera],
		bol.fecaplicenf AS 'FECHA APLICACION ENFERMERIA',--[FechaAplicacionEnfermería],

		dx.coddiagno AS 'COD DIAGNOSTICO PRINCIPAL',--[CodDiagnosticoPrincipal],
		dx.nomdiagno AS 'NOMBRE DIAGNOSTICO PRINCIPAL',--[NombreDiagnosticoPrincipal],
		CASE dp.tipdiagno 
			WHEN 'I' THEN 'Impresion Diagnostica' 
			WHEN 'C' THEN 'Confirmado Nuevo' 
			WHEN 'R' THEN 'Confirmado Repetido' END AS 'TIPO DIAGNOSTICO',--[TipoDiagnostico],
		dp.t1 + dp.n1 + dp.m1 AS TNM,
		CAST(bol.fecaplicenf AS DATE) [FECHA BUSQUEDA],
        CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM dbo.hcorhembol AS bol 
	INNER JOIN dbo.hcorhemco AS ord ON bol.hcorhemcoid = ord.id 

	INNER JOIN dbo.inpacient AS p ON ord.ipcodpaci = p.ipcodpaci 
	LEFT JOIN dbo.inubicaci AS ubi WITH (NOLOCK) ON p.auubicaci = ubi.auubicaci 
	LEFT JOIN dbo.inmunicip AS mun WITH (NOLOCK) ON ubi.depmuncod = mun.depmuncod
	LEFT JOIN dbo.indeparta AS dep WITH (NOLOCK) ON mun.depcodigo = dep.depcodigo
	INNER JOIN dbo.adingreso AS ai WITH (NOLOCK) ON ord.numingres = ai.numingres
	INNER JOIN contract.healthadministrator AS ha WITH (NOLOCK) ON ai.genconentity = ha.id
	INNER JOIN contract.caregroup AS cg WITH (NOLOCK) ON ai.gencaregroup = cg.id

	INNER JOIN dbo.adcenaten AS ca WITH (NOLOCK) ON ord.codcenate = ca.codcenate 
	LEFT JOIN dbo.inprofsal AS med WITH (NOLOCK) ON bol.profaplica = med.codprosal 
	LEFT JOIN dbo.inespecia AS em WITH (NOLOCK) ON med.codespec1 = em.codespeci 
	LEFT JOIN dbo.inprofsal AS enf WITH (NOLOCK) ON bol.enfaplica = enf.codprosal 
	LEFT JOIN dbo.inespecia AS ee WITH (NOLOCK) ON enf.codespec1 = ee.codespeci
	INNER JOIN dbo.hccomsan AS cs WITH (NOLOCK) ON bol.comsamid = cs.id
	LEFT JOIN dbo.indiagnop AS dp WITH (NOLOCK) ON ord.numingres = dp.numingres AND dp.coddiapri = 1
	LEFT JOIN dbo.indiagnos AS dx WITH (NOLOCK) ON dp.coddiagno = dx.coddiagno 
	WHERE bol.estado = 7 AND CAST(bol.fecaplicenf AS DATE)>='2024-01-01'
	--CAST(bol.fecaplicenf AS DATE) BETWEEN @inidate AND @endate