

--CREATE PROCEDURE [dbo].[ODO_Incapacidades_Expedidas]
	
--DECLARE	@ini_date DATE='2024-06-01';
--DECLARE @end_date DATE='2024-06-30';

CREATE view [Report].[UploadCubeVieClinicalMedicalIssuedMedicalDisabilities] AS

SELECT DISTINCT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
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
		WHEN 15 THEN 'SI' END AS 'TIPO IDENTIFICACIO',--[TipoIdentificacion],
	pac.ipcodpaci AS 'NRO IDENTIFICACION',--[NroIdentificacion],
	pac.ipnomcomp AS 'NOMBRE',--[Nombre],
	pac.ipfecnaci AS 'FECHA NACIMIENTO',--[FechaNacimiento],
	mun.munnombre AS 'MUNICIPIO',--[Municipio],
	dep.nomdepart AS 'DEPARTAMENTO',--[Departamento],
	entc.code AS 'CODIGO EPS',--[CodigoEPS],
	entc.name 'NOMBRE EPS',--[NombreEPS],
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
	med.nommedico AS 'MEDICO EXPIDE',--[MedicoExpide],
	esp.desespeci AS 'ESPECIALIDAD',--[Especialidad],
	incp.fecregist AS 'FECHA EXPEDICION',--[FechaExpedicion],
	incp.feciniinc AS 'FECHA INICIO INCAPACIDAD',--[FechaInicioIncapacidad],
	incp.fecfininc AS 'FECHA FIN INCAPACIDAD',--[FechaFinIncapacidad],
	DATEDIFF(DAY, incp.feciniinc, incp.fecfininc) + 1 AS 'NRO DIAS',--[NroDias],
    CASE incp.esprorrog WHEN 0 THEN 'No' WHEN 1 THEN 'Si' END AS 'ES PRORROGA',--[EsProrroga],
	ing.numingres AS 'NRO INGRESO',--[NroIngreso],
	ing.ifechaing AS 'FECHA INGRESO',--[FechaIngreso],
	dx.coddiagno AS 'CODIGO DIAGNOSTICO',--[CodigoDiagnostico],
	dx.nomdiagno AS 'NOMBRE DIAGNOSTICO',--[NombreDiagnostico],
	CAST(incp.fecregist AS DATE) [FECHA BUSQUEDA],
    CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM dbo.hcincapac incp
INNER JOIN dbo.inpacient AS pac WITH (NOLOCK) ON incp.ipcodpaci = pac.ipcodpaci
LEFT JOIN dbo.inubicaci AS ubi WITH (NOLOCK) ON pac.auubicaci = ubi.auubicaci 
LEFT JOIN dbo.inmunicip AS mun WITH (NOLOCK) ON ubi.depmuncod = mun.depmuncod
LEFT JOIN dbo.indeparta AS dep WITH (NOLOCK) ON mun.depcodigo = dep.depcodigo
INNER JOIN dbo.adingreso AS ing WITH (NOLOCK) ON incp.numingres = ing.numingres
INNER JOIN contract.healthadministrator AS entc WITH (NOLOCK) ON ing.genconentity = entc.id
INNER JOIN contract.caregroup AS grpc WITH (NOLOCK) ON ing.gencaregroup = grpc.id
INNER JOIN dbo.adcenaten AS ca WITH (NOLOCK) ON incp.codcenate = ca.codcenate 
INNER JOIN dbo.inunifunc AS uf WITH (NOLOCK) ON incp.ufucodigo = uf.ufucodigo 
INNER JOIN dbo.inprofsal AS med WITH (NOLOCK) ON incp.codprosal = med.codprosal 
INNER JOIN dbo.inespecia AS esp WITH (NOLOCK) ON med.codespec1 = esp.codespeci 
LEFT JOIN dbo.indiagnos AS dx WITH (NOLOCK) ON incp.coddiagno = dx.coddiagno
WHERE CAST(incp.fecregist AS DATE)>='2023-01-01'
--CAST(incp.fecregist AS DATE) BETWEEN @ini_date AND @end_date

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida las incapacidades médicas expedidas desde 2023-01-01, enriquecidas con datos del paciente, ubicación, EPS, grupo de atención, médico, especialidad y diagnóstico, para alimentar un cubo de análisis clínico.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalIssuedMedicalDisabilities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente de la incapacidad debe existir en el maestro de pacientes; La incapacidad debe estar asociada a un ingreso existente; El ingreso debe tener entidad administradora de salud y grupo de atención válidos en contract; Deben existir centro de atención, unidad funcional, médico y especialidad asociados a la incapacidad; La fecha de registro de la incapacidad debe ser igual o posterior al 2023-01-01', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalIssuedMedicalDisabilities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El número de días de incapacidad se calcula como DATEDIFF(DAY, feciniinc, fecfininc) + 1, incluyendo ambos extremos; El ID_COMPANY corresponde al nombre de la base de datos en ejecución, truncado a 9 caracteres; La fecha de última actualización se entrega convertida a la zona horaria ''Pakistan Standard Time''; Sólo se incluyen incapacidades registradas desde 2023-01-01 en adelante; El paciente, ingreso, EPS, grupo de atención, centro, unidad funcional, médico y especialidad son obligatorios (INNER JOIN); ubicación, municipio, departamento y diagnóstico son opcionales (LEFT JOIN); Se eliminan duplicados mediante DISTINCT', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalIssuedMedicalDisabilities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Incapacidad médica; Prórroga de incapacidad; Paciente; Ingreso; EPS / Entidad administradora de salud; Régimen de salud (Contributivo, Subsidiado, ARL, Medicina Prepagada, Fosyga); Grupo de atención; Tipo de liquidación (Capitación, PGP, Pago por Servicios); Centro de atención; Unidad funcional; Médico tratante; Especialidad médica; Diagnóstico (CIE); Tipo de identificación', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalIssuedMedicalDisabilities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalMedicalIssuedMedicalDisabilities: Cuando CAST(incp.fecregist AS DATE) >= ''2023-01-01'' y existen las relaciones obligatorias (paciente, ingreso, EPS, grupo atención, centro, unidad funcional, médico, especialidad), se retorna una fila DISTINCT por incapacidad con los datos enriquecidos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalIssuedMedicalDisabilities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pac.iptipodoc entre 1 y 15 → Mapea el tipo de documento a códigos estándar (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI) else NULL; si entc.entitytype entre 1 y 12 → Clasifica el régimen de la entidad (EPS Contributivo/Subsidiado, ET Vinculados, ARL, Medicina Prepagada, IPS Privada/Pública, Régimen Especial, Accidentes de tránsito, Fosyga, Otros) else NULL; si grpc.liquidationtype entre 1 y 5 → Clasifica el tipo de liquidación (Pago por Servicios, Capitación, Factura Global, Capitación Global, PGP) else NULL; si incp.esprorrog = 1 → Marca la incapacidad como prórroga (''Si'') else Marca como ''No'' cuando es 0', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalIssuedMedicalDisabilities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.hcincapac; dbo.inpacient; dbo.inubicaci; dbo.inmunicip; dbo.indeparta; dbo.adingreso; contract.healthadministrator; contract.caregroup; dbo.adcenaten; dbo.inunifunc; dbo.inprofsal; dbo.inespecia; dbo.indiagnos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalIssuedMedicalDisabilities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalIssuedMedicalDisabilities';
GO
