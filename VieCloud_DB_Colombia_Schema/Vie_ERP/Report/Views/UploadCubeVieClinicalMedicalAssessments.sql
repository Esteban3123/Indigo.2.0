

--
--CREATE PROCEDURE [EHR].[SP_VALORACIONES_MEDICAS]
--declare	@FechaInicio DATE='2024-04-01';
--declare	@FechaFin DATE='2024-04-30'; 
--AS

CREATE view [Report].[UploadCubeVieClinicalMedicalAssessments] AS

	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		CASE i.iptipodoc
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
			WHEN 15 THEN 'SI' END AS 'TIPO DE IDENTIFICACION',--[TipoIdentificacion],
		a.IPCODPACI AS 'NRO DE IDENTIFICACION',--[NroIdentificacion],
		RTRIM(i.IPNOMCOMP) as 'NOMBRE DEL PACIENTE',--[NombrePaciente],
		TRIM(gcare.code) + ' - ' + gcare.name AS 'GRUPO DE ATENCION',-- [GrupoAtencion], 
		TRIM(ent.code) + ' - ' + ent.name AS 'ENTIDAD',--[Entidad],
		RTRIM(E.NOMCENATE) AS 'CENTRO DE ATENCION',--[CentroAtencion],
		A.UFUCODIGO +' - ' + RTRIM(D.UFUDESCRI) AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
		A.NUMINGRES AS 'NRO INGRESO',--[NroIngreso],
		RTRIM(s.CODSERIPSINTRA) +' - ' + RTRIM(CUPS.DESSERIPS)  as 'CUPS',-- [CUPS],  
		'1' AS 'CANTIDAD',--[Cantidad], 
		A.NUMEFOLIO AS 'NRO FOLIO',--[NroFolio],
		B.CODPROSAL AS 'IDENTIFICACION MEDICO',--[IdentificacionMedico],
		B.NOMMEDICO AS 'NOMBRE MEDICO',-- [NombreMedico],
		RTRIM(C.DESESPECI) AS 'ESPECIALIDAD',--[Especialidad],
		FECHISPAC AS 'FECHA HISTORIA',--[FechaHistoria],
		TRIM(diag.coddiagno) + ' - ' + TRIM(diag.nomdiagno) AS 'DIAGNOSTICO',-- [Diagnostico],
		CASE dxp.tipdiagno 
			WHEN 'I' THEN 'Impresion Diagnostica' 
			WHEN 'C' THEN 'Confirmado Nuevo' 
			WHEN 'R' THEN 'Confirmado Repetido' END AS 'TIPO DIAGNOSTICO',-- [TipoDiagnostico]
			cast(A.FECHISPAC as date) 'FECHA BUSQUEDA',
			CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM .dbo.HCHISPACA A  
	INNER JOIN .dbo.INPROFSAL B ON A.CODPROSAL=B.CODPROSAL 
	INNER JOIN .dbo.INESPECIA C ON A.CODESPTRA = C.CODESPECI 
	INNER JOIN .dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
	INNER JOIN .dbo.ADCENATEN E ON A.CODCENATE=E.CODCENATE
	inner join .dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI
	inner join .dbo.ADINGRESO ing on A.NUMINGRES = ing.NUMINGRES
	left join .dbo.HCESPSERU s on s.CODESPECI = C.CODESPECI
	left join  .dbo.INCUPSIPS as CUPS ON CUPS.CODSERIPS =s.CODSERIPSINTRA
	LEFT JOIN .contract.caregroup AS gcare ON ing.gencaregroup = gcare.id 
	LEFT JOIN .contract.healthadministrator AS ent ON ing.genconentity = ent.id
	LEFT JOIN .dbo.indiagnos AS diag ON a.coddiagno = diag.coddiagno 
	LEFT JOIN .dbo.indiagnop AS dxp ON a.numingres = dxp.numingres AND a.coddiagno = dxp.coddiagno
	WHERE CAST(A.FECHISPAC AS date)>='2023-01-01' AND  
	a.ipcodpaci NOT IN('999999999', '1010112421', '7777775', '22446688', '1234567890', '00000000', '1010', '1212', '11111111', '000000000000000', '10101010', '11111', '00000')
	-- CAST(A.FECHISPAC AS date) BETWEEN @FechaInicio AND @FechaFin

	union all

	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		'RC' AS 'TIPO DE IDENTIFICACION',--[TipoIdentificacion],
		a.IPCODPACI AS 'NRO DE IDENTIFICACION',--[IDENTIFICACION],
		'Hijo '+ cast(RTRIM(rn.NUMHIJREG) as varchar(20)) as 'NOMBRE DEL PACIENTE',-- [PACIENTE],
		TRIM(gcare.code) + ' - ' + gcare.name AS 'GRUPO DE ATENCION',-- [GRUPO_ATENCION], 
		TRIM(ent.code) + ' - ' + ent.name AS 'ENTIDAD',--[EPS],
		RTRIM(E.NOMCENATE) AS 'CENTRO DE ATENCION',--[CENTRO ATENCION],
		A.UFUCODIGO +' - ' + RTRIM(D.UFUDESCRI) AS 'UNIDAD FUNCIONAL',--[UNIDAD FUNCIONAL],
		INGMH.NUMINGRES AS 'NRO INGRESO',--[INGRESO],
		RTRIM(s.CODSERIPSINTRA) +' - ' + RTRIM(CUPS.DESSERIPS) as 'CUPS',--CUPSManejo, 
		'1' AS 'CANTIDAD',--[Cantidad],
		A.NUMEFOLIO as 'NRO FOLIO',-- FOLIO,
		B.CODPROSAL AS 'IDENTIFICACION MEDICO',--[IDENTIFICACION PROFESIONAL],
		B.NOMMEDICO as 'NOMBRE MEDICO',--PROFESIONAL,
		RTRIM(C.DESESPECI) AS 'ESPECIALIDAD',-- ESPECIALIDAD,
		a.FECHISPAC AS 'FECHA HISTORIA',-- [FECHA HISTORIA],
		TRIM(diag.coddiagno) + ' - ' + TRIM(diag.nomdiagno) AS 'DIAGNOSTICO',
		CASE dxp.tipdiagno 
			WHEN 'I' THEN 'Impresion Diagnostica' 
			WHEN 'C' THEN 'Confirmado Nuevo' 
			WHEN 'R' THEN 'Confirmado Repetido' END AS 'TIPO DIAGNOSTICO',
			cast(A.FECHISPAC as date) 'FECHA BUSQUEDA',
			CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM .dbo.HCHISPACA A  
	INNER JOIN .dbo.INPROFSAL B ON A.CODPROSAL=B.CODPROSAL 
	INNER JOIN .dbo.INESPECIA C ON B.CODESPEC1=C.CODESPECI 
	INNER JOIN .dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
	INNER JOIN .dbo.ADCENATEN E ON A.CODCENATE=E.CODCENATE
	inner join .dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	inner join .dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	inner join .dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO
	left join .dbo.HCESPSERU s on s.CODESPECI = C.CODESPECI
	left join  .dbo.INCUPSIPS as CUPS ON CUPS.CODSERIPS =s.CODSERIPSINTRA
	LEFT JOIN .contract.caregroup AS gcare ON ing.gencaregroup = gcare.id 
	LEFT JOIN .contract.healthadministrator AS ent ON ing.genconentity = ent.id
	LEFT JOIN .dbo.indiagnos AS diag ON a.coddiagno = diag.coddiagno 
	LEFT JOIN .dbo.indiagnop AS dxp ON a.numingres = dxp.numingres AND a.coddiagno = dxp.coddiagno
	WHERE CAST(A.FECHISPAC AS date)>='2023-01-01' AND 
	a.ipcodpaci NOT IN('999999999', '1010112421', '7777775', '22446688', '1234567890', '00000000', '1010', '1212', '11111111', '000000000000000', '10101010', '11111', '00000')
	-- CAST(A.FECHISPAC AS date)  BETWEEN @FechaInicio AND @FechaFin	--AND

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida las valoraciones médicas (historias clínicas) de pacientes y de recién nacidos asociados a su madre, enriquecidas con datos de paciente, profesional, especialidad, unidad funcional, grupo de atención, entidad, CUPS y diagnóstico, para alimentar un cubo de reportería.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalAssessments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas base (HCHISPACA, INPROFSAL, INESPECIA, INUNIFUNC, ADCENATEN, INPACIENT, ADINGRESO, HCINGRESORECNAC, HCRECINAC) deben existir en la BD actual con sus claves de relación pobladas.; La zona horaria ''Pakistan Standard Time'' debe estar disponible en el servidor para el cálculo de ULT_ACTUAL.; Existe correspondencia entre especialidad y servicio (HCESPSERU) y catálogo CUPS (INCUPSIPS) para resolver el código CUPS; de lo contrario quedan en NULL por LEFT JOIN.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalAssessments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen historias clínicas con FECHISPAC desde 2023-01-01 en adelante.; Se excluyen sistemáticamente identificadores de paciente considerados de prueba/dummy (''999999999'',''1010112421'',''7777775'',''22446688'',''1234567890'',''00000000'',''1010'',''1212'',''11111111'',''000000000000000'',''10101010'',''11111'',''00000'').; La cantidad por valoración siempre es 1.; ID_COMPANY se trunca a 9 caracteres del nombre de la base de datos actual.; ULT_ACTUAL se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; En la rama de recién nacidos el tipo de identificación se fija en ''RC'' y el nombre se construye como ''Hijo '' + NUMHIJREG.; La especialidad en la rama del titular se toma de A.CODESPTRA, mientras que en la rama de recién nacidos se toma de la especialidad principal del profesional (B.CODESPEC1).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalAssessments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de identificación; Historia clínica / Valoración médica; Ingreso hospitalario; Recién nacido; Profesional de la salud / Médico; Especialidad médica; Unidad funcional; Centro de atención; Grupo de atención; Entidad / Administradora de salud (EPS); CUPS (servicio); Diagnóstico; Tipo de diagnóstico (Impresión, Confirmado Nuevo, Confirmado Repetido); Folio', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalAssessments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve registros de historias clínicas (HCHISPACA) cuando FECHISPAC >= ''2023-01-01'' y el documento del paciente no está en la lista de identificadores de prueba; cada fila incluye ID_COMPANY = nombre de la BD.; [RETURN_RESULT] resultset: Cuando el ingreso del paciente corresponde a un recién nacido (existe en HCINGRESORECNAC.NUMINGRESHIJO), genera fila adicional con tipo de identificación fijo ''RC'', nombre ''Hijo ''+NUMHIJREG y NRO INGRESO del hijo (INGMH.NUMINGRES).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalAssessments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si i.iptipodoc en 1..15 → Mapea el código numérico a una etiqueta de tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI) else NULL (tipo de identificación no clasificado); si dxp.tipdiagno IN (''I'',''C'',''R'') → Traduce a ''Impresion Diagnostica'', ''Confirmado Nuevo'' o ''Confirmado Repetido'' else NULL; si Registro de HCHISPACA tiene ingreso vinculado a HCINGRESORECNAC (recién nacido) → Se reporta en la segunda rama del UNION ALL como paciente ''Hijo N'' con tipo ''RC'' y especialidad tomada de B.CODESPEC1 else Solo aparece en la primera rama con datos del paciente titular', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalAssessments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPROFSAL; dbo.INESPECIA; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.INPACIENT; dbo.ADINGRESO; dbo.HCESPSERU; dbo.INCUPSIPS; contract.caregroup; contract.healthadministrator; dbo.indiagnos; dbo.indiagnop; dbo.HCINGRESORECNAC; dbo.HCRECINAC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalAssessments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalAssessments';
GO
