

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

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola salida las notas clínicas de terapeutas/enfermería (dos orígenes diferentes) enriquecidas con datos demográficos del paciente, EPS, grupo de atención, ingreso y diagnóstico principal, para alimentar un cubo de reporte.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalTherapistsAndNursingNotes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las notas deben tener fecha de registro (fecregist) mayor o igual a 2024-01-01.; Cada nota debe estar asociada a un paciente, ingreso, centro de atención, unidad funcional, profesional de salud y especialidad existentes (INNER JOIN).; El ingreso debe tener entidad administradora de salud y grupo de atención válidos en contract.healthadministrator y contract.caregroup.; Existe la función COMMON.GETDATE() y la zona horaria ''Pakistan Standard Time'' está disponible en el servidor.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalTherapistsAndNursingNotes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo incluye notas con fecha de registro a partir del 1 de enero de 2024.; ID_COMPANY se trunca al nombre de la base de datos a 9 caracteres.; La columna ULT_ACTUAL siempre refleja la hora convertida a ''Pakistan Standard Time''.; TNM se construye concatenando t1+n1+m1 del diagnóstico principal.; Sólo se trae un diagnóstico por ingreso: el principal (coddiapri = 1).; Las dos ramas del UNION ALL comparten el mismo filtro temporal y la misma estructura de columnas.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalTherapistsAndNursingNotes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de identificación; Ingreso hospitalario; EPS / Entidad administradora; Régimen de afiliación; Grupo de atención; Modalidad de liquidación (PGP, capitación, etc.); Centro de atención; Unidad funcional; Profesional de salud; Especialidad; Notas clínicas de enfermería/terapia; Diagnóstico principal (CIE); Clasificación TNM; Impresión diagnóstica / confirmado nuevo / repetido', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalTherapistsAndNursingNotes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve UNION ALL de notas provenientes de dbo.hcctrnote y dbo.hcctrnott cuando CAST(fecregist AS DATE) >= ''2024-01-01''.; [RETURN_RESULT] resultset: Filtra diagnóstico principal usando indiagnop con coddiapri = 1; si no existe, las columnas de diagnóstico quedan en NULL (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalTherapistsAndNursingNotes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si iptipodoc del paciente (1..15) → Mapea a códigos de tipo de identificación: CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI.; si Sexo del paciente (primer bloque ipsexo=''M''/''H''; segundo bloque ipsexopac=2/1) → Traduce a ''FEMENINO'' o ''MASCULINIO'' (sic). Nota: en el primer bloque ''M'' se mapea a FEMENINO y ''H'' a MASCULINIO.; si entitytype de la entidad administradora (1..12) → Clasifica el régimen: EPS Contributivo, EPS Subsidiado, ET Vinculados Municipios/Departamentos, ARL, Medicina Prepagada, IPS Privada/Pública, Régimen Especial, SOAT, Fosyga u Otros.; si liquidationtype del grupo de atención (1..5) → Determina modalidad: Pago por Servicios, Capitación, Factura Global, Capitación Global, PGP.; si tipdiagno del diagnóstico (I/C/R) → Clasifica como Impresión Diagnóstica, Confirmado Nuevo o Confirmado Repetido.; si dxi.coddiapri = 1 → Sólo se considera el diagnóstico marcado como principal del ingreso.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalTherapistsAndNursingNotes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'COMMON.GETDATE', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalTherapistsAndNursingNotes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.hcctrnote; dbo.hcctrnott; dbo.inpacient; dbo.inubicaci; dbo.inmunicip; dbo.indeparta; dbo.adingreso; contract.healthadministrator; contract.caregroup; dbo.adcenaten; dbo.inunifunc; dbo.inprofsal; dbo.inespecia; dbo.indiagnop; dbo.indiagnos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalTherapistsAndNursingNotes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalTherapistsAndNursingNotes';
GO
