
--CREATE PROCEDURE [dbo].[ODO_Glucometrias]
	-- Add the parameters for the stored procedure here
--	@ini_date DATE, @end_date DATE

	CREATE view [Report].[UploadCubeVieClinicalMedicalGlucometries] AS

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
		RTRIM(pac.ipnomcomp) AS 'NOMBRE',--[Nombre],
		RTRIM(ca.codcenate) + ' - ' + RTRIM(ca.nomcenate) AS 'CENTRO ATENCION',--[CentroAtencion],
		RTRIM(uf.ufucodigo) + ' - ' + RTRIM(uf.ufudescri) AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
		exf.fecregite AS 'FECHA REGISTRO',--[FechaRegistro],
		exf.uciaduglu AS 'VALOR GLUCOMETRIA',--[ValorGlucometria],
		exf.codprosal AS 'NRO IDENTIFICACION PROFESIONAL',--[NroIdentificacionProfesional],
		esp.desespeci AS 'ESPECIALIDAD',--[Especialidad],
		med.nommedico AS 'NOMBRE PROFESIONAL',--[NombreProfesional],
		CAST(exf.fecregite AS DATE) [FECHA BUSQUEDA],
        CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM dbo.hcexfisic exf 
	INNER JOIN dbo.inprofsal AS med ON exf.codprosal = med.codprosal 
	INNER JOIN dbo.inespecia AS esp ON med.codespec1 = esp.codespeci 
	INNER JOIN dbo.adcenaten AS ca ON exf.codcenate = ca.codcenate 
	INNER JOIN dbo.inunifunc AS uf ON exf.ufucodigo = uf.ufucodigo  
	INNER JOIN dbo.inpacient AS pac ON exf.ipcodpaci = pac.ipcodpaci
	WHERE exf.idetiphis = 'ENFERMER1' AND exf.uciaduglu IS NOT NULL AND CAST(exf.fecregite AS DATE)>='2023-01-01'
	--CAST(exf.fecregite AS DATE) BETWEEN @ini_date AND @end_date 