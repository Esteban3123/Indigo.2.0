
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

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los registros de glucometrías tomadas por enfermería desde 2023, junto con datos del paciente, profesional, especialidad, centro de atención y unidad funcional, para alimentar un cubo de reporte.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalGlucometries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El examen físico debe estar identificado con tipo de historia ''ENFERMER1''; El valor de glucometría no debe ser nulo; La fecha de registro debe ser igual o posterior a 2023-01-01; Deben existir relaciones válidas entre paciente, profesional, especialidad, centro de atención y unidad funcional', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalGlucometries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se reportan glucometrías registradas por personal de enfermería (idetiphis=''ENFERMER1''); Se excluyen registros con valor de glucometría nulo; Sólo se incluyen registros desde el 1 de enero de 2023 en adelante; El identificador de compañía corresponde al nombre de la base de datos truncado a 9 caracteres; La marca de última actualización se calcula con la hora actual convertida a zona horaria ''Pakistan Standard Time''; Los códigos de centro de atención y unidad funcional se presentan concatenados con su descripción en formato ''código - descripción''; Sólo aparecen pacientes, profesionales, especialidades, centros y unidades existentes (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalGlucometries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glucometría; Paciente; Tipo de identificación; Profesional de la salud; Especialidad médica; Centro de atención; Unidad funcional; Examen físico de enfermería; Historia clínica', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalGlucometries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalMedicalGlucometries: Devuelve filas distintas con la glucometría sólo cuando idetiphis=''ENFERMER1'', uciaduglu IS NOT NULL y fecregite >= ''2023-01-01''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalGlucometries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Mapeo de pac.iptipodoc según valor numérico (1..15) → Traduce el código numérico interno a la sigla del tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI) else Si no coincide con ningún valor del 1 al 15, el tipo de identificación queda en NULL', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalGlucometries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.hcexfisic; dbo.inprofsal; dbo.inespecia; dbo.adcenaten; dbo.inunifunc; dbo.inpacient', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalGlucometries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalGlucometries';
GO
