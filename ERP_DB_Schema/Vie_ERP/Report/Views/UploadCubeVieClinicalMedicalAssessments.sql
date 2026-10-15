

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