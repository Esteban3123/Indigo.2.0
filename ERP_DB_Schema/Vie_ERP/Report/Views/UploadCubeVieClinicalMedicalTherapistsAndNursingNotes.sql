

--CREATE PROCEDURE [dbo].[ODO_Notas_Enfermeria]
	
--DECLARE	@ini_date DATE='2024-06-01'; 
--DECLARE @end_date DATE='2024-06-30';

CREATE view [Report].[UploadCubeVieClinicalMedicalTherapistsAndNursingNotes] AS

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
	pac.ipcodpaci AS 'NRO IDENTIFICACION',--[NroIdentificacion],
	RTRIM(pac.ipnomcomp) AS 'NOMBRE',--[Nombre],
	pac.ipfecnaci AS 'FECHA NACIMIENTO',--[FechaNacimiento],
	CASE pac.ipsexo WHEN 'M' THEN 'FEMENINO' WHEN 'H' THEN 'MASCULINIO' END AS 'SEXO',--Sexo,
	mun.munnombre AS 'MUNICIPIO',--[Municipio],
	dep.nomdepart AS 'DEPARTAMENTO',--[Departamento],
	pac.ipdirecci AS 'DIRECCION RESIDENCIA',--[DireccionResidencia],
	pac.iptelmovi AS 'TELEFONO PRINCIPAL',--[TelefonoPrincipal],
	pac.iptelefon AS 'TELEFONO ALTERNATIVO',--[TelefonoAlternativo],
	entc.code AS 'CODIGO EPS',--[CodigoEPS],
	entc.name 'NOMBRE EPS', --[NombreEPS],
	CASE entc.entitytype
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
	grpc.code AS 'CODIGO GRUPO ATENCION',--[CodigoGrupoAtencion],
	grpc.name 'NOMBRE GRUPO ATENCION',--[NombreGrupoAtencion],
	CASE grpc.liquidationtype
		WHEN 1 THEN 'Pago por Servicios'
		WHEN 2 THEN 'Capitacion'
		WHEN 3 THEN 'Factura Global'
		WHEN 4 THEN 'Capitacion Global'
		WHEN 5 THEN 'Pago Global Prospectivo - PGP' END AS 'TIPO LIQUIDACION',--[TipoLiquidacion],

	ca.nomcenate AS 'CENTRO ATENCION',--[CentroAtencion],
	uf.ufudescri AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],

	nott.fecregist AS 'FECHA NOTA',--[FechaNota],
	med.nommedico AS 'PROFESIONAL REALIZA',--[ProfesionalRealiza],
	esp.desespeci AS 'ESPECIALIDAD',--[Especialidad],
	ing.numingres AS 'NRO INGRESO',--[NroIngreso],
	ing.ifechaing AS 'FECHA INGRESO',--[FechaIngreso],
	dx.coddiagno AS 'CODIGO DIAGNOSTICO PRINCIPAL',--[CodigoDiagnosticoPrincipal],
	dx.nomdiagno AS 'NOMBRE DIAGNOSTICO PRINCIPAL',--[NombreDiagnosticoPrincipal],
	CASE dxi.tipdiagno 
		WHEN 'I' THEN 'Impresion Diagnostica' 
		WHEN 'C' THEN 'Confirmado Nuevo' 
		WHEN 'R' THEN 'Confirmado Repetido' END AS 'TIPO DIAGNOSTICO',--[TipoDiagnostico],
	    dxi.t1 + dxi.n1 + dxi.m1 AS TNM ,
		CAST(nott.fecregist AS DATE) as 'FECHA BUSQUEDA',
		CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL

FROM dbo.hcctrnote AS nott
INNER JOIN dbo.inpacient AS pac WITH (NOLOCK) ON nott.ipcodpaci = pac.ipcodpaci 
LEFT JOIN dbo.inubicaci AS ubi WITH (NOLOCK) ON pac.auubicaci = ubi.auubicaci 
LEFT JOIN dbo.inmunicip AS mun WITH (NOLOCK) ON ubi.depmuncod = mun.depmuncod
LEFT JOIN dbo.indeparta AS dep WITH (NOLOCK) ON mun.depcodigo = dep.depcodigo
INNER JOIN dbo.adingreso AS ing WITH (NOLOCK) ON nott.numingres = ing.numingres
INNER JOIN contract.healthadministrator AS entc WITH (NOLOCK) ON ing.genconentity = entc.id
INNER JOIN contract.caregroup AS grpc WITH (NOLOCK) ON ing.gencaregroup = grpc.id
INNER JOIN dbo.adcenaten AS ca WITH (NOLOCK) ON nott.codcenate = ca.codcenate 
INNER JOIN dbo.inunifunc AS uf WITH (NOLOCK) ON nott.ufucodigo = uf.ufucodigo
INNER JOIN dbo.inprofsal AS med WITH (NOLOCK) ON nott.codprosal = med.codprosal 
INNER JOIN dbo.inespecia AS esp WITH (NOLOCK) ON med.codespec1 = esp.codespeci 
LEFT JOIN dbo.indiagnop AS dxi WITH (NOLOCK) ON nott.numingres = dxi.numingres AND dxi.coddiapri = 1 
LEFT JOIN dbo.indiagnos AS dx WITH (NOLOCK) ON dxi.coddiagno = dx.coddiagno
WHERE CAST(nott.fecregist AS DATE)>='2024-01-01'
--CAST(nott.fecregist AS DATE) BETWEEN @ini_date AND @end_date

UNION ALL 

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
		WHEN 15 THEN 'SI' END AS [TipoDocumento],
	RTRIM(pac.ipcodpaci) AS [NroDocumento],
	RTRIM(pac.ipnomcomp) AS [Nombre],
	pac.ipfecnaci AS [FecNacimiento],
	CASE pac.ipsexopac WHEN 2 THEN 'FEMENINO' WHEN 1 THEN 'MASCULINIO' END AS Sexo,
	mun.munnombre AS [Municipio],
	dep.nomdepart AS [Departamento],
	UPPER(RTRIM(pac.ipdirecci)) AS [DireccionResidencia],
	pac.iptelmovi AS [TelPrincipal],
	pac.iptelefon AS [TelSecundario],
	entc.code AS [CodEPS],
	entc.name [NomEPS],
	CASE entc.entitytype
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
		WHEN 12 THEN 'Otros' END AS [RegimenEPS],
	grpc.code AS [CodGrupoAtencion],
	grpc.name [NomGrupoAtencion],
	CASE grpc.liquidationtype
		WHEN 1 THEN 'Pago por Servicios'
		WHEN 2 THEN 'Capitacion'
		WHEN 3 THEN 'Factura Global'
		WHEN 4 THEN 'Capitacion Global'
		WHEN 5 THEN 'Pago Global Prospectivo - PGP' END AS [TipoLiquidacion],

	ca.nomcenate AS [CentroAtencion],
	uf.ufudescri AS [UnidadFuncional],
	nott.fecregist AS [FecNota],
	med.nommedico AS [ProfesionalRealiza],
	esp.desespeci AS Especialidad,

	ing.numingres AS [NroIngreso],
	ing.ifechaing AS [FecIngreso],
	

	dx.coddiagno AS [CodDiagnosticoPrincipal],
	dx.nomdiagno AS [NomDiagnsticoPrincipal],
	CASE dxi.tipdiagno 
		WHEN 'I' THEN 'Impresion Diagnostica' 
		WHEN 'C' THEN 'Confirmado Nuevo' 
		WHEN 'R' THEN 'Confirmado Repetido' END AS [TipoDiagnostico],
	dxi.t1 + dxi.n1 + dxi.m1 AS TNM,
	CAST(nott.fecregist AS DATE) as 'FECHA BUSQUEDA',
	CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM dbo.hcctrnott AS nott
INNER JOIN dbo.inpacient AS pac WITH (NOLOCK) ON nott.ipcodpaci = pac.ipcodpaci 
LEFT JOIN dbo.inubicaci AS ubi WITH (NOLOCK) ON pac.auubicaci = ubi.auubicaci 
LEFT JOIN dbo.inmunicip AS mun WITH (NOLOCK) ON ubi.depmuncod = mun.depmuncod
LEFT JOIN dbo.indeparta AS dep WITH (NOLOCK) ON mun.depcodigo = dep.depcodigo
INNER JOIN dbo.adingreso AS ing WITH (NOLOCK) ON nott.numingres = ing.numingres
INNER JOIN contract.healthadministrator AS entc WITH (NOLOCK) ON ing.genconentity = entc.id
INNER JOIN contract.caregroup AS grpc WITH (NOLOCK) ON ing.gencaregroup = grpc.id
INNER JOIN dbo.adcenaten AS ca WITH (NOLOCK) ON nott.codcenate = ca.codcenate 
INNER JOIN dbo.inunifunc AS uf WITH (NOLOCK) ON nott.ufucodigo = uf.ufucodigo
INNER JOIN dbo.inprofsal AS med WITH (NOLOCK) ON nott.codprosal = med.codprosal 
INNER JOIN dbo.inespecia AS esp WITH (NOLOCK) ON med.codespec1 = esp.codespeci 
LEFT JOIN dbo.indiagnop AS dxi WITH (NOLOCK) ON nott.numingres = dxi.numingres AND dxi.coddiapri = 1 
LEFT JOIN dbo.indiagnos AS dx WITH (NOLOCK) ON dxi.coddiagno = dx.coddiagno
WHERE CAST(nott.fecregist AS DATE)>='2024-01-01'
--CAST(nott.fecregist AS DATE) BETWEEN @ini_date AND @end_date