

--CREATE PROCEDURE [EHR].[SP_ORDENES_LABORATORIOS]
--	@OperatingUnitCode VARCHAR(20),
CREATE view [Report].[UploadCubeVieClinicalMedicalLaboratoryOrders] AS

--DECLARE	@DateStart DATE='2024-06-01';
--DECLARE	@DateEnd DATE='2024-06-02';

	---***LABORATORIOS EXTERNO*****---
	SELECT DISTINCT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
		CASE PAC.IPTIPODOC 	
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
		HIS.ipcodpaci AS 'NRO IDENTIFICACION',--[NroIdentificacion], 
		RTRIM(PAC.ipnomcomp) AS 'NOMBRE PACIENTE',--[NombrePaciente], 
		RTRIM(HA.Name) 'ENTIDAD',--[Entidad], 
		RTRIM(grp.code) + ' - ' + grp.name 'GRUPO ATENCION',--[GrpAtencion], 
		LAB.codcenate AS 'CODIGO CENTRO ATENCION',--[CodCentroAtencion], 
		CEN.nomcenate AS 'CENTRO ATENCION',--[CentroAtencion],
		LAB.ufucodigo AS 'CODIGO UNIDAD FUNCIONAL',--[CodUnidadFuncional],
		UNI.ufudescri AS 'UNIDAD FUNCIONAL',-- [UnidadFuncional],
		HIS.numingres AS 'NRO INGRESO',--[NroIngreso],
		CASE WHEN ING.tipoingre = '1' THEN 'AMBULATORIO' ELSE 'HOSPITALARIO' END AS 'TIPO INGRESO',--[TipoIngreso], 
		HIS.numefolio AS 'NRO FOLIO',--[NroFolio], 
		LAB.codprosal AS 'IDENTIFICACION MEDICO',--[IdentificacionMedico], 
		PRO.nommedico AS 'MEDICO',-- [Medico],
		HIS.codesptra AS 'CODIGO ESPECIALIDAD',--[CodEspecialidad],
		ESP.desespeci AS 'ESPECIALIDAD',--[Especialidad],
		HIS.fechispac AS 'FECHA HISTORIA',--[FechaHistoria], 
		HIS.coddiagno AS 'CODIGO DIAGNOSTICO',--[CodDiagnostico],
		DIA.nomdiagno AS 'DIAGNISTICO',--[Diagnostico], 
		CASE URG.tipcitmed WHEN 1 THEN 'PRIMERA VEZ' WHEN 2 THEN 'CONTROL' ELSE 'N/A' END AS 'TIPO CITA',--[TipoCita], 
		LAB.codserips AS 'CODIGO SERVICIO',--[CodServicio],
		Isnull(CD.name, IPS.desserips) AS 'DESCRIPCION',--[Descripcion], 
		LAB.canserips AS 'CANTIDAD',--[Cantidad], 
		'LABORATORIO' AS 'TIPO SERVICIO',--[TipoServicio], 
		ISNULL(CD.id,0) AS 'CUPS',--[CUPS],
		CD.name AS 'DESCRIPCION CUPS',--[DescripcionCUPS], 
		IGRU.DESGRUIPS AS 'GRUPO CUPS',--[GrupoCUPS],
		GRU.dessubips AS 'SUBGRUPO CUPS',--[SubgrupoCUPS]
		cast(HIS.fechispac as date) as 'FECHA BUSQUEDA',
		CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	/*INTO INDIGODWH.PBI.[STG_SERVICIOS_SOLICITADOS_TOTAL]*/ 
	FROM   dbo.hcurging1 AS URG INNER JOIN 
	dbo.hchispaca AS HIS ON URG.idetiphis = HIS.idetiphis AND URG.ipcodpaci = HIS.ipcodpaci AND URG.numingres = HIS.numingres 
	AND HIS.numefolio = URG.numefolio INNER JOIN 
	dbo.inpacient AS PAC ON HIS.ipcodpaci = PAC.ipcodpaci INNER JOIN 
	dbo.adingreso AS ING ON HIS.numingres = ING.numingres INNER JOIN 
	Contract .HealthAdministrator HA  WITH (NOLOCK) ON HA.ID =ING.GENCONENTITY INNER JOIN
	dbo.indiagnos AS DIA ON HIS.coddiagno = DIA.coddiagno INNER JOIN 
	dbo.hcordlabo AS LAB ON HIS.ipcodpaci = LAB.ipcodpaci AND HIS.numingres = LAB.numingres AND LAB.manextpro = 1 AND HIS.numefolio = LAB.numefolio INNER JOIN 
	dbo.incupsips AS IPS ON LAB.codserips = IPS.codserips INNER JOIN 
	dbo.incupssub AS GRU ON IPS.codgrusub = GRU.codgrusub INNER JOIN 
	dbo.INCUPSGRU as IGRU ON IGRU.CODGRUIPS =IPS.CODGRUIPS INNER JOIN
	dbo.adcenaten AS CEN ON LAB.codcenate = CEN.codcenate INNER JOIN 
	dbo.inunifunc AS UNI ON LAB.ufucodigo = UNI.ufucodigo INNER JOIN 
	dbo.inprofsal AS PRO ON LAB.codprosal = PRO.codprosal INNER JOIN 
	dbo.inespecia AS ESP ON HIS.codesptra = ESP.codespeci LEFT JOIN 
	contract.cupsentitycontractdescriptions AS CECD ON LAB.iddescripcionrelacionada = CECD.id LEFT OUTER JOIN 
	contract.contractdescriptions AS CD ON CECD.contractdescriptionid = CD.id 
	INNER JOIN contract.caregroup AS grp ON ing.gencaregroup = grp.id
	WHERE  ( HIS.genconext = 1 ) AND ( HIS.tiphispac = 'I' ) AND ( LAB.estserips <> '6' ) and cast(HIS.fechispac as date)>='2024-01-01'
	--cast(HIS.fechispac as date) between @DateStart and @DateEnd
	UNION
	---***LABORATORIOS HOSPITALARIOS*****---
	SELECT DISTINCT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
		CASE PAC.IPTIPODOC 	
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
			WHEN 15 THEN 'SI' END AS [TipoIdentificacion], 
		HIS.ipcodpaci AS IDENTIFICACION, 
		PAC.ipnomcomp AS NOMBRE_PACIENTE, 
		HA.Name ENTIDAD, RTRIM(grp.code) + ' - ' + grp.name [GRUPO DE ATENCION], 
		LAB.codcenate AS CODIGO_CENTRO_ATENCIÓN, 
		CEN.nomcenate AS CENTRO_ATENCION,
		LAB.ufucodigo AS CODIGO_UNIDAD_FUNCIONAL,
		UNI.ufudescri AS UNIDAD_FUNCIONAL,
		HIS.numingres AS INGRESO,
		CASE WHEN ING.tipoingre = '1' THEN 'AMBULATORIO' ELSE 'HOSPITALARIO' END AS TIPO_INGRESO, 
		HIS.numefolio AS NUMERO_FOLIO, 
		LAB.codprosal AS DOCUMENTO_PROFESIONAL, 
		PRO.nommedico AS PROFESIONAL,
		HIS.codesptra AS CODIGO_ESPECIALIDAD,
		ESP.desespeci AS ESPECIALIDAD,
		HIS.fechispac AS FECHA_HISTORIA, 
		HIS.coddiagno AS CODIGO_DIAGNOSTICO,
		DIA.nomdiagno AS DIAGNOSTICO, 
		'VALORACION HOSPITALARIA'  AS TIPO_CITA, 
		LAB.codserips AS CODIGO_PROCEDIMIENTO,
		Isnull(CD.name, IPS.desserips) AS PROCEDIMIENTO, 
		LAB.canserips AS CANTIDAD, 
		'LABORATORIO' AS TIPO_SERVICIO, 
		ISNULL(CD.id,0) AS CODIGO_DESCRIPCION_CUPS,
		CD.name AS DESCRIPCION_CUPS, 
		IGRU.DESGRUIPS AS GRUPO_CUPS,
		GRU.dessubips AS SUBGRUPO_CUPS,
		cast(HIS.fechispac as date) as 'FECHA BUSQUEDA',
		CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM  dbo.hchispaca AS HIS  INNER JOIN 
	dbo.inpacient AS PAC ON HIS.ipcodpaci = PAC.ipcodpaci INNER JOIN 
	dbo.adingreso AS ING ON HIS.numingres = ING.numingres INNER JOIN 
	Contract .HealthAdministrator HA  WITH (NOLOCK) ON HA.ID =ING.GENCONENTITY INNER JOIN
	dbo.indiagnos AS DIA ON HIS.coddiagno = DIA.coddiagno INNER JOIN 
	dbo.hcordlabo AS LAB ON HIS.ipcodpaci = LAB.ipcodpaci AND HIS.numingres = LAB.numingres AND LAB.manextpro = 0 AND HIS.numefolio = LAB.numefolio INNER JOIN 
	dbo.incupsips AS IPS ON LAB.codserips = IPS.codserips INNER JOIN 
	dbo.incupssub AS GRU ON IPS.codgrusub = GRU.codgrusub INNER JOIN 
	dbo.INCUPSGRU as IGRU ON IGRU.CODGRUIPS =IPS.CODGRUIPS INNER JOIN 
	dbo.adcenaten AS CEN ON LAB.codcenate = CEN.codcenate INNER JOIN 
	dbo.inunifunc AS UNI ON LAB.ufucodigo = UNI.ufucodigo INNER JOIN 
	dbo.inprofsal AS PRO ON LAB.codprosal = PRO.codprosal INNER JOIN 
	dbo.inespecia AS ESP ON HIS.codesptra = ESP.codespeci LEFT JOIN 
	contract.cupsentitycontractdescriptions AS CECD ON LAB.iddescripcionrelacionada = CECD.id LEFT OUTER JOIN 
	contract.contractdescriptions AS CD ON CECD.contractdescriptionid = CD.id 
	INNER JOIN contract.caregroup AS grp ON ing.gencaregroup = grp.id
	WHERE  ( HIS.genconext = 0 ) AND ( LAB.estserips <> '6' ) and cast(HIS.fechispac as date)>='2024-01-01'
	--cast(HIS.fechispac as date) between @DateStart and @DateEnd