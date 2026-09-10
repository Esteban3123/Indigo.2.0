

--CREATE PROCEDURE [dbo].[ODO_Masa_Corporal_Pacientes_Quimioterapia]
	-- Add the parameters for the stored procedure here

CREATE view [Report].[UploadCubeVieClinicalMedicalBodyMassPatientsChemotherapy] AS

--DECLARE	@inidate DATE='2024-06-01';
--DECLARE	@endate DATE='2024-06-30';

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
		RTRIM(pac.ipnomcomp) AS 'NOMBRE',--Nombre,
		CASE pac.ipsexopac WHEN 1 THEN 'MASCULINO' WHEN 2 THEN 'FEMENINO' END AS 'SEXO',--Sexo,
		pac.ipfecnaci AS 'FECHA NACIMIENTO',--[FechaNacimiento],
		DATEDIFF(YEAR, pac.ipfecnaci, COMMON.GETDATE()) AS 'EDAD',--Edad,
		dep.nomdepart AS 'DEPARTAMENTO',--Departamento,
		ubi.ubinombre AS 'MUNICIPIO',--Municipio,
		UPPER(pac.ipdirecci) AS 'DIRECCION',--Direccion,
		pac.iptelefon AS 'TELEFONO PRINCIPAL',--[TelefonoPrincipal],
		pac.iptelmovi AS 'TELEFONO ALTERNO',--[TelefonoAlterno],
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
		ing.numingres AS 'NRO INGRESO',--[NroIngreso],
		ing.ifechaing AS 'FECHA INGRESO',--[FechaIngreso],
		ca.nomcenate AS 'CENTRO ATENCION',--[CentroAtencion],
		ufu.ufudescri AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
		RTRIM(med.codprosal) + ' - ' +  RTRIM(med.nommedico) AS 'MEDICO',--[Medico],
		esp.desespeci AS 'ESPECIALIDAD',--[Especialidad],
		sch.description AS 'ESQUEMA',--[Esquema],
		CASE ordq.estado 
			WHEN 1 THEN 'Orden solicitada'
			WHEN 2 THEN 'Esquema iniciado'
			WHEN 3 THEN 'Esquema finalizado completo'
			WHEN 4 THEN 'Suspendido'
			WHEN 5 THEN 'Anulado' END AS 'ESTADO ESQUEMA',--[EstadoEsquema],
		dx.coddiagno AS 'CODIGO DIAGNOSTICO PRINCIPAL',--[CodigoDiagnosticoPrincipal],
		dx.nomdiagno AS 'NOMBRE DIAGNOSTICO PRINCIPAL',--[NombreDiagnosticoPrincipal],
		CASE dxp.tipdiagno 
			WHEN 'I' THEN 'Impresion Diagnostica' 
			WHEN 'C' THEN 'Confirmado Nuevo' 
			WHEN 'R' THEN 'Confirmado Repetido' END AS 'TIPO DIAGNOSTICO',--[TipoDiagnostico],
		dxp.t1 + dxp.n1 + dxp.m1 AS 'TNM',--TNM,
		exf.fecregite AS 'FECHA REGISTRO',--[FechaRegistro],
		exf.tallapaci AS 'TALLA',--[Talla],
		(exf.pesopacie / 1000) AS 'PESO',--[Peso],
		CAST(exf.fecregite AS DATE) as 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM dbo.hcexfisic AS exf
	INNER JOIN ehr.hcordquimio AS ordq ON exf.numingres = ordq.numingres AND exf.numefolio = ordq.numefolio 
	INNER JOIN ehr.schemes AS sch ON ordq.schemesid = sch.id
	INNER JOIN dbo.inpacient AS pac ON exf.ipcodpaci = pac.ipcodpaci 
	LEFT JOIN dbo.inubicaci AS ubi WITH(NOLOCK) ON pac.auubicaci = ubi.auubicaci
	LEFT JOIN dbo.inmunicip AS mun WITH(NOLOCK) ON ubi.depmuncod = mun.depmuncod
	LEFT JOIN dbo.indeparta AS dep WITH(NOLOCK) ON mun.depcodigo = dep.depcodigo
	INNER JOIN dbo.adingreso AS ing ON exf.numingres = ing.numingres
	INNER JOIN contract.healthadministrator AS ha WITH (NOLOCK) ON ing.genconentity = ha.id
	INNER JOIN contract.caregroup AS cg WITH (NOLOCK) ON ing.gencaregroup = cg.id
	INNER JOIN dbo.adcenaten AS ca WITH (NOLOCK) ON ing.codcenate = ca.codcenate 
	INNER JOIN dbo.inunifunc AS ufu ON exf.ufucodigo = ufu.ufucodigo
	INNER JOIN dbo.inprofsal AS med WITH (NOLOCK) ON exf.codprosal = med.codprosal 
	INNER JOIN dbo.inespecia AS esp WITH (NOLOCK) ON med.codespec1 = esp.codespeci 
	LEFT JOIN dbo.indiagnop AS dxp WITH (NOLOCK) ON ing.numingres = dxp.numingres AND dxp.coddiapri = 1
	LEFT JOIN dbo.indiagnos AS dx WITH (NOLOCK) ON dxp.coddiagno = dx.coddiagno 
	WHERE CAST(exf.fecregite AS DATE)>='2024-01-01'
	--CAST(exf.fecregite AS DATE) BETWEEN @inidate AND @endate

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida información demográfica, administrativa y clínica (peso, talla, esquema de quimioterapia, diagnóstico principal y TNM) de pacientes con órdenes de quimioterapia para alimentar un cubo analítico.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalBodyMassPatientsChemotherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada examen físico debe estar asociado a una orden de quimioterapia (numingres + numefolio coincidentes en hcexfisic y hcordquimio).; El esquema referenciado en la orden de quimioterapia debe existir en ehr.schemes.; El paciente del examen físico debe existir en inpacient.; El ingreso debe existir en adingreso y tener entidad administradora (genconentity) y grupo de atención (gencaregroup) válidos en contract.healthadministrator y contract.caregroup.; El profesional (codprosal) debe existir en inprofsal y tener especialidad principal (codespec1) válida en inespecia.; La unidad funcional y el centro de atención deben existir en sus catálogos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalBodyMassPatientsChemotherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan pacientes con examen físico vinculado a una orden de quimioterapia (INNER JOIN con hcordquimio).; El registro nunca incluye examen físico anterior al 1 de enero de 2024.; El identificador de compañía (ID_COMPANY) corresponde al nombre de la BD actual truncado a 9 caracteres.; El TNM se construye concatenando dxp.t1 + dxp.n1 + dxp.m1 únicamente para el diagnóstico principal (coddiapri=1).; El médico se reporta como ''código - nombre'' (codprosal + nommedico).; La especialidad reportada es la primera especialidad (codespec1) del profesional.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalBodyMassPatientsChemotherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Quimioterapia; Esquema de tratamiento; Examen físico (peso/talla/masa corporal); Diagnóstico principal; Clasificación TNM (oncología); Ingreso asistencial; EPS / Régimen de afiliación; Grupo de atención y tipo de liquidación; Centro de atención y unidad funcional; Profesional de salud y especialidad', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalBodyMassPatientsChemotherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Se devuelven solo registros cuyo CAST(exf.fecregite AS DATE) >= ''2024-01-01''.; [RETURN_RESULT] resultset: Se aplica DISTINCT sobre todas las columnas para eliminar duplicados generados por joins.; [RETURN_RESULT] resultset: El peso del paciente se reporta dividido entre 1000 (exf.pesopacie/1000), asumiendo conversión de gramos a kilogramos.; [RETURN_RESULT] resultset: La edad se calcula como DATEDIFF(YEAR, ipfecnaci, fecha actual de COMMON.GETDATE()).; [RETURN_RESULT] resultset: El timestamp ULT_ACTUAL se obtiene convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; [RETURN_RESULT] resultset: El diagnóstico se incluye solo si es principal: indiagnop.coddiapri = 1 (LEFT JOIN, por lo que registros sin diagnóstico principal igualmente se reportan con NULL).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalBodyMassPatientsChemotherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pac.iptipodoc entre 1 y 15 → Se mapea a etiqueta de tipo de identificación (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI).; si pac.ipsexopac = 1 / = 2 → Se traduce a ''MASCULINO'' / ''FEMENINO''; otros valores quedan NULL.; si ha.entitytype entre 1 y 12 → Se traduce al régimen EPS correspondiente (Contributivo, Subsidiado, ET Vinculados, ARL, Medicina Prepagada, IPS Privada/Pública, Régimen Especial, Accidentes de tránsito, Fosyga, Otros).; si cg.liquidationtype entre 1 y 5 → Se traduce a tipo de liquidación (Pago por Servicios, Capitación, Factura Global, Capitación Global, PGP).; si ordq.estado entre 1 y 5 → Se traduce el estado del esquema (Solicitada, Iniciado, Finalizado completo, Suspendido, Anulado).; si dxp.tipdiagno IN (''I'',''C'',''R'') → Se traduce a ''Impresión Diagnóstica'', ''Confirmado Nuevo'' o ''Confirmado Repetido''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalBodyMassPatientsChemotherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.hcexfisic; ehr.hcordquimio; ehr.schemes; dbo.inpacient; dbo.inubicaci; dbo.inmunicip; dbo.indeparta; dbo.adingreso; contract.healthadministrator; contract.caregroup; dbo.adcenaten; dbo.inunifunc; dbo.inprofsal; dbo.inespecia; dbo.indiagnop; dbo.indiagnos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalBodyMassPatientsChemotherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalBodyMassPatientsChemotherapy';
GO
