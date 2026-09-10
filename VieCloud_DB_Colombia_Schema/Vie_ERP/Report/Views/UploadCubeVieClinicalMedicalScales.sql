
--CREATE PROCEDURE [dbo].[ODO_Escalas]
--	-- Add the parameters for the stored procedure here

--DECLARE	@ini_date DATE='2024-01-01';
--DECLARE	@end_date DATE='2024-06-30';
--DECLARE	@type VARCHAR(30);


	--IF @type = 'TISS28'
	--	SET @type = 23
	--ELSE IF @type = 'Braden'
	--	SET @type = 24
	--ELSE IF @type = 'Downton'
	--	SET @type = 47
CREATE view [Report].[UploadCubeVieClinicalMedicalScales] AS

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
			WHEN 15 THEN 'SI' END AS 'TIPO IDENTIFICACION',--[TipoIdentificacion],
		pac.ipcodpaci AS 'NRO IDENTIFICACION',--[NroIdentificacion],
		pac.ipnomcomp AS 'NOMBRE',--[Nombre],
		pac.ipfecnaci AS 'FECHA NACIMIENTO',--[FechaNacimiento],
		CASE pac.ipsexo WHEN 'M' THEN 'FEMENINO' WHEN 'H' THEN 'MASCULINIO' END AS 'SEXO',--[Sexo],
		mun.munnombre AS 'MUNICIPIO',--[Municipio],
		dep.nomdepart AS 'DEPARTAMENTO',--[Departamento],
		UPPER(RTRIM(pac.ipdirecci)) AS 'DIRECCION RESIDENCIA',--[DireccionResidencia],
		pac.iptelmovi AS 'TELEFONO PRINCIPAL',--[TelefonoPrincipal],
		pac.iptelefon AS 'TELEFONO ALTERNATIVO',--[TelefonoAlternativo],
		entc.code AS 'CODIGO EPS',--[CodEPS],
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
		grpc.code AS 'CODIGO GRUPO ATENCION',--[CodGrupoAtencion],
		grpc.name 'NOMBRE GRUPO ATENCION',--[NombreGrupoAtencion],
		CASE grpc.liquidationtype
			WHEN 1 THEN 'Pago por Servicios'
			WHEN 2 THEN 'Capitacion'
			WHEN 3 THEN 'Factura Global'
			WHEN 4 THEN 'Capitacion Global'
			WHEN 5 THEN 'Pago Global Prospectivo - PGP' END AS 'TIPO LIQUIDACION',--[TipoLiquidacion],
		ca.nomcenate AS 'CENTRO ATENCION',--[CentroAtencion],
		uf.ufudescri AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
		esc.fecharegistro AS 'FECHA REGISTRO',--[FechaRegistro],
		CASE tipoescala 
			WHEN 23 THEN 'Escala TISS28'
			WHEN 24 THEN 'Escala Braden' 
			WHEN 47 THEN 'Escala Downton' END AS 'TIPO ESCALA',--[TipoEscala],
		CASE  
			-- TISS-28
			WHEN tipoescala = 23 AND esc.resultado < 10 THEN 'Grado: I - TISS Puntaje: ' + CAST(esc.resultado AS VARCHAR) +  ' - Clasificación: Observación'
			WHEN tipoescala = 23 AND esc.resultado >= 10 AND esc.resultado < 20 THEN 'Grado: II - TISS Puntaje: ' + CAST(esc.resultado AS VARCHAR) +  ' - Clasificación: Vigilancia activa'
			WHEN tipoescala = 23 AND esc.resultado >= 20 AND esc.resultado < 40 THEN 'Grado: III - TISS Puntaje: ' + CAST(esc.resultado AS VARCHAR) +  ' - Clasificación: Vigilancia intensiva'
			WHEN tipoescala = 23 AND esc.resultado >= 40 THEN 'Grado: IV - TISS Puntaje: ' + CAST(esc.resultado AS VARCHAR) +  ' - Clasificación: Terapéutica Intensiva'
			--	Braden
			WHEN tipoescala = 24 AND esc.resultado <= 12 THEN CAST(esc.resultado AS VARCHAR) +  ' - RIESGO ALTO: Puntuación <= 12'
			WHEN tipoescala = 24 AND esc.resultado >= 13 AND esc.resultado <= 14 THEN CAST(esc.resultado AS VARCHAR) +  ' - RIESGO MODERADO: Puntuación 13-14'
			WHEN tipoescala = 24 AND esc.resultado >= 15 AND esc.resultado <= 16 AND DATEDIFF(YEAR, pac.ipfecnaci, esc.fecharegistro) < 75 THEN CAST(esc.resultado AS VARCHAR) +  ' - RIESGO BAJO: Puntuación 15-16 (Si < 75 Años)'
			WHEN tipoescala = 24 AND esc.resultado >= 15 AND esc.resultado <= 18 AND DATEDIFF(YEAR, pac.ipfecnaci, esc.fecharegistro) < 75 THEN CAST(esc.resultado AS VARCHAR) +  ' - RIESGO BAJO: Puntuación 15-18 (Si >= 75 Años)'
			WHEN tipoescala = 24 AND esc.resultado >= 19 THEN CAST(esc.resultado AS VARCHAR) +  ' - SIN RIESGO'
			-- Donwton 
			WHEN esc.tipoescala = 47 AND esc.resultado < 3 THEN CAST(esc.resultado AS VARCHAR) +  ' - El paciente no presenta riesgo de caída'  
			WHEN esc.tipoescala = 47 AND esc.resultado > 2 THEN CAST(esc.resultado AS VARCHAR) +  ' - ALERTA - El paciente presenta riesgo de caída' END AS 'RESULTADO',--[Resultado],
		med.nommedico AS 'PROFESIONAL REALIZA',--[ProfesionalRealiza],
		esp.desespeci AS 'ESPECIALIDAD',--[Especialidad],
		ing.numingres AS 'NRO INGRESO',--[NroIngreso],
		ing.ifechaing AS 'FECHA INGRESO',--[FechaIngreso],
		dx.coddiagno AS 'COD DIAGNOSTICO PRINCIPAL',--[CodDiagnosticoPrincipal],
		dx.nomdiagno AS 'NOMBRE DIAGNOSTICO PRINCIPAL',--[NombreDiagnosticoPrincipal],
		CASE dxi.tipdiagno 
			WHEN 'I' THEN 'Impresion Diagnostica' 
			WHEN 'C' THEN 'Confirmado Nuevo' 
			WHEN 'R' THEN 'Confirmado Repetido' END AS 'TIPO DIAGNOSTICO',--[TipoDiagnostico],
		dxi.t1 + dxi.n1 + dxi.m1 AS TNM,
		CAST(esc.fecharegistro AS DATE) [FECHA BUSQUEDA],
        CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM dbo.hcescalas AS esc 
	INNER JOIN dbo.inpacient AS pac WITH (NOLOCK) ON esc.ipcodpaci = pac.ipcodpaci 
	LEFT JOIN dbo.inubicaci AS ubi WITH (NOLOCK) ON pac.auubicaci = ubi.auubicaci 
	LEFT JOIN dbo.inmunicip AS mun WITH (NOLOCK) ON ubi.depmuncod = mun.depmuncod
	LEFT JOIN dbo.indeparta AS dep WITH (NOLOCK) ON mun.depcodigo = dep.depcodigo
	INNER JOIN dbo.adingreso AS ing WITH (NOLOCK) ON esc.numingres = ing.numingres
	INNER JOIN contract.healthadministrator AS entc WITH (NOLOCK) ON ing.genconentity = entc.id
	INNER JOIN contract.caregroup AS grpc WITH (NOLOCK) ON ing.gencaregroup = grpc.id
	INNER JOIN dbo.adcenaten AS ca WITH (NOLOCK) ON esc.codcenate = ca.codcenate 
	INNER JOIN dbo.inunifunc AS uf WITH (NOLOCK) ON esc.ufucodigo = uf.ufucodigo
	INNER JOIN dbo.inprofsal AS med WITH (NOLOCK) ON esc.codprosal = med.codprosal 
	INNER JOIN dbo.inespecia AS esp WITH (NOLOCK) ON med.codespec1 = esp.codespeci 
	LEFT JOIN dbo.indiagnop AS dxi WITH (NOLOCK) ON esc.numingres = dxi.numingres AND dxi.coddiapri = 1 
	LEFT JOIN dbo.indiagnos AS dx WITH (NOLOCK) ON dxi.coddiagno = dx.coddiagno
	WHERE tipoescala  IN(23,24,47) AND CAST(esc.fecharegistro AS DATE)>='2024-01-01'
	--CAST(esc.fecharegistro AS DATE) BETWEEN @ini_date AND @end_date

	--SELECT * FROM  dbo.hcescalas

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida los registros de escalas clínicas (TISS28, Braden, Downton) aplicadas a pacientes desde 2024-01-01, enriqueciendo con datos demográficos, administrativos, de ingreso, profesional tratante y diagnóstico principal, para alimentar un cubo de reportería.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalScales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en dbo.hcescalas con tipoescala en (23, 24, 47); Los pacientes (inpacient), ingresos (adingreso), entidad de salud (healthadministrator), grupo de atención (caregroup), centro de atención (adcenaten), unidad funcional (inunifunc), profesional (inprofsal) y especialidad (inespecia) deben existir para que la fila aparezca (joins INNER); La fecha de registro de la escala debe ser >= 2024-01-01', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalScales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone escalas de tipo TISS28 (23), Braden (24) y Downton (47); Solo expone registros con fecharegistro a partir del 2024-01-01; Toma únicamente el diagnóstico marcado como principal (coddiapri = 1); ID_COMPANY se trunca a 9 caracteres del nombre de la base de datos; ULT_ACTUAL se calcula con la zona horaria ''Pakistan Standard Time''; El sexo ''M'' se reporta como FEMENINO y ''H'' como MASCULINIO (mapeo invertido respecto al estándar habitual)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalScales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Escala clínica TISS28; Escala Braden (riesgo de úlceras por presión); Escala Downton (riesgo de caída); EPS / Régimen de afiliación; Grupo de atención y tipo de liquidación (Capitación, PGP, etc.); Centro de atención; Unidad funcional; Ingreso hospitalario; Diagnóstico principal (CIE) y clasificación TNM; Profesional de salud y especialidad; Tipo de identificación colombiano', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalScales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalMedicalScales: Devuelve filas DISTINCT de escalas clínicas filtradas por tipoescala IN (23,24,47) y fecharegistro >= 2024-01-01, con la fecha de última actualización calculada como GETDATE() convertido a ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalScales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si tipoescala = 23 (TISS28) y resultado < 10 → Clasifica como Grado I - Observación; si tipoescala = 23 y resultado entre 10 y 19 → Clasifica como Grado II - Vigilancia activa; si tipoescala = 23 y resultado entre 20 y 39 → Clasifica como Grado III - Vigilancia intensiva; si tipoescala = 23 y resultado >= 40 → Clasifica como Grado IV - Terapéutica Intensiva; si tipoescala = 24 (Braden) y resultado <= 12 → Clasifica como RIESGO ALTO; si tipoescala = 24 y resultado entre 13 y 14 → Clasifica como RIESGO MODERADO; si tipoescala = 24, resultado entre 15 y 16 y edad < 75 años → Clasifica como RIESGO BAJO (Puntuación 15-16, < 75 Años); si tipoescala = 24, resultado entre 15 y 18 y edad < 75 años → Clasifica como RIESGO BAJO (Puntuación 15-18, >= 75 Años) — nota: la condición codificada usa < 75 pero la etiqueta dice >= 75; si tipoescala = 24 y resultado >= 19 → Clasifica como SIN RIESGO; si tipoescala = 47 (Downton) y resultado < 3 → El paciente no presenta riesgo de caída; si tipoescala = 47 y resultado > 2 → ALERTA - El paciente presenta riesgo de caída; si iptipodoc del paciente (1..15) → Mapea a códigos de tipo de identificación (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI); si ipsexo = ''M'' o ''H'' → Etiqueta como FEMENINO o MASCULINIO respectivamente; si entitytype de la entidad (1..12) → Mapea a régimen (EPS Contributivo, Subsidiado, ET Vinculados, ARL, Medicina Prepagada, IPS, Régimen Especial, Accidentes de tránsito, Fosyga, Otros); si liquidationtype del grupo de atención (1..5) → Mapea a tipo de liquidación (Pago por Servicios, Capitación, Factura Global, Capitación Global, PGP); si tipdiagno = ''I'' / ''C'' / ''R'' → Mapea a Impresión Diagnóstica / Confirmado Nuevo / Confirmado Repetido', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalScales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.hcescalas; dbo.inpacient; dbo.inubicaci; dbo.inmunicip; dbo.indeparta; dbo.adingreso; contract.healthadministrator; contract.caregroup; dbo.adcenaten; dbo.inunifunc; dbo.inprofsal; dbo.inespecia; dbo.indiagnop; dbo.indiagnos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalScales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalScales';
GO
