
--CREATE PROCEDURE [EHR].[SP_MORBILIDAD]

CREATE view [Report].[UploadCubeVieClinicalMedicalMorbidity] AS

--DECLARE	@FECINI Datetime='2024-06-01';
--DECLARE	@FECFIN Datetime='2024-06-30';

	WITH CTE_GENERAL
	AS
	(
		SELECT 
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
				HIS.IPCODPACI AS 'NRO IDENTIFICACION',--[NroIdentificacion], 
			RTRIM(PAC.IPNOMCOMP) AS 'NOMBRE PACIENTE',--[NomPaciente], 
			PAC.IPFECNACI AS 'FECHA NACIMIENTO',--[FecNacimiento],
			FLOOR((CAST(CONVERT(VARCHAR(8), HIS.FECHISPAC , 112) AS INT)-CAST(CONVERT(VARCHAR(8), PAC.IPFECNACI, 112) AS INT)) / 10000) AS 'EDAD',--[Edad],
			CASE PAC.IPSEXOPAC 
				WHEN 1 THEN 'MASCULINO' 
				WHEN 2 THEN 'FEMENINO' END AS 'SEXO',--[Sexo],
			DEP.nomdepart AS 'DEPARTAMENTO',--[Departamento],
			MUN.MUNNOMBRE AS 'MUNICIPIO',--[Municipio],
			CEN.NOMCENATE AS 'CENTRO ATENCION',--[CenAtencion],  
			CASE HIS.GENCONEXT 
				WHEN 0 THEN 'HOSPITALARIA' 
				WHEN 1 THEN 'CONSULTA EXTERNA' END AS 'TIPO UNIDAD',--[TipoUnidad],
			FUN.UFUDESCRI AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
			EAPB.Name AS 'NOMBRE EPS',--[NomEPS],
			HIS.NUMINGRES AS 'NRO INGRESO',--[NroIngreso], 
			CAST(HIS.FECHISPAC AS DATE) AS 'FECHA ATENCION',-- [FecAtencion],
			PRO.NOMMEDICO AS 'NOMBRE MEDICO',--[NomMedico],
		ESP.DESESPECI AS 'ESPECIALIDAD TRATANTE'--[EspeTratante]--, HIS.CODDIAGNO AS 'CIE10', DIA.NOMDIAGNO AS 'NOMBRE DIAGNOSTICO'
		FROM .HCHISPACA AS HIS
		INNER JOIN INPACIENT AS PAC WITH(NOLOCK) ON PAC.IPCODPACI=HIS.IPCODPACI
		INNER JOIN ADINGRESO AS ING WITH(NOLOCK) ON  ING.NUMINGRES =HIS.NUMINGRES AND ING.IPCODPACI=HIS.IPCODPACI
		INNER JOIN ADCENATEN AS CEN WITH(NOLOCK) ON CEN.CODCENATE=HIS.CODCENATE
		INNER JOIN INUNIFUNC AS FUN WITH(NOLOCK) ON FUN.UFUCODIGO=HIS.UFUCODIGO
		INNER JOIN INPROFSAL AS PRO WITH(NOLOCK) ON PRO.CODPROSAL=HIS.CODPROSAL
		INNER JOIN INESPECIA AS ESP WITH(NOLOCK) ON ESP.CODESPECI=HIS.CODESPTRA
		INNER JOIN INDIAGNOS AS DIA WITH(NOLOCK) ON DIA.CODDIAGNO=HIS.CODDIAGNO
		INNER JOIN Contract.HealthAdministrator AS EAPB WITH(NOLOCK) ON EAPB.Id=ING.GENCONENTITY
		INNER JOIN INUBICACI AS UB WITH(NOLOCK) ON UB.AUUBICACI = PAC.AUUBICACI
		INNER JOIN INMUNICIP AS MUN WITH(NOLOCK) ON MUN.DEPMUNCOD = UB.DEPMUNCOD
		INNER JOIN INDEPARTA AS DEP WITH(NOLOCK) ON DEP.depcodigo=MUN.DEPCODIGO
		WHERE (HIS.TIPHISPAC = 'I') AND CAST(HIS.FECHISPAC AS DATE)>='2024-01-01'
		--AND CAST(HIS.FECHISPAC AS DATE) BETWEEN @FecIni AND @FecFin

	)

	,CTE_CODIGO
	AS
	(
		SELECT * FROM 
		(
			SELECT 
				IPCODPACI,
				NUMINGRES, 
				NUMEFOLIO, 
				CODDIAGNO, 
				CASE 
					WHEN CODDIAPRI = 1 THEN 'CIE10 DIAGNOSTICO PRINCIPAL'
					WHEN CODDIAPRI <> 1 AND (ROW_NUMBER() OVER(PARTITION BY IPCODPACI,NUMINGRES, NUMEFOLIO ORDER BY IPCODPACI,NUMINGRES, NUMEFOLIO DESC)) = 2 THEN 'CIE10 DIAGNOSTICO RELACIONADO 1' 
					WHEN CODDIAPRI <> 1 AND (ROW_NUMBER() OVER(PARTITION BY IPCODPACI,NUMINGRES, NUMEFOLIO ORDER BY IPCODPACI,NUMINGRES, NUMEFOLIO DESC)) = 3 THEN 'CIE10 DIAGNOSTICO RELACIONADO 2' END AS TIPO_DIAGNOSTICO
			FROM .INDIAGNOP
		)P
		PIVOT
		(MAX(CODDIAGNO) 
		FOR  TIPO_DIAGNOSTICO IN ([CIE10 DIAGNOSTICO PRINCIPAL],[CIE10 DIAGNOSTICO RELACIONADO 1],[CIE10 DIAGNOSTICO RELACIONADO 2])
		)PVT2 
	)

	SELECT DISTINCT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		GE.*,
		CO.[CIE10 DIAGNOSTICO PRINCIPAL] AS 'CODIGO DIAGNOSTICO PRINCIPAL',--[CodDxPrincipal], 
		RTRIM(dxp.nomdiagno) AS 'NOMBRE DIAGNOSTICO PRINCIPAL',--[NomDxPrincipal],
		CO.[CIE10 DIAGNOSTICO RELACIONADO 1] AS 'CODIGO DIAGNOSTICO DOS',--[CodDxDos], 
		RTRIM(dx1.nomdiagno) AS 'NOMBRE DIAGNOSTICO DOS',--[NomDxDos],
		CO.[CIE10 DIAGNOSTICO RELACIONADO 2] AS 'CODIGO DIAGNOSTICO TRES',--[CodDxTres], 
		TRIM(dx2.nomdiagno) AS 'NOMBRE DIAGNOSTICO TRES',--[NomDxTres]
		CAST(GE.[FECHA ATENCION] AS DATE) as 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM  CTE_GENERAL GE
	INNER JOIN CTE_CODIGO CO ON ge.[NRO IDENTIFICACION] = CO.IPCODPACI AND ge.[NRO INGRESO] = CO.NUMINGRES 
	INNER JOIN dbo.indiagnos AS dxp ON co.[CIE10 DIAGNOSTICO PRINCIPAL] = dxp.coddiagno 
	LEFT JOIN dbo.indiagnos AS dx1 ON co.[CIE10 DIAGNOSTICO RELACIONADO 1] = dx1.coddiagno 
	LEFT JOIN dbo.indiagnos AS dx2 ON co.[CIE10 DIAGNOSTICO RELACIONADO 2] = dx2.coddiagno

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la morbilidad de atenciones hospitalarias desde 2024-01-01, exponiendo datos demográficos del paciente, ingreso, profesional tratante y hasta tres diagnósticos CIE10 (principal y dos relacionados) para alimentar un cubo de reportes.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMorbidity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener registro en INPACIENT con ubicación válida (AUUBICACI) y ésta a su vez con municipio y departamento existentes.; El ingreso debe existir en ADINGRESO y tener asociada una entidad pagadora (GENCONENTITY) presente en Contract.HealthAdministrator.; La historia clínica debe tener centro de atención, unidad funcional, profesional, especialidad tratante y diagnóstico válidos en sus catálogos respectivos.; Debe existir al menos un registro en INDIAGNOP cuyo CODDIAPRI=1 (diagnóstico principal) para el ingreso/folio.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMorbidity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La edad se calcula en años cumplidos a la fecha de la atención usando aritmética sobre fechas en formato YYYYMMDD.; Solo se exponen máximo tres diagnósticos por atención: principal y dos relacionados (los demás se ignoran).; La fecha de última actualización se entrega convertida a la zona horaria ''Pakistan Standard Time''.; El identificador de la compañía proviene del nombre de la base de datos actual truncado a 9 caracteres.; Las atenciones sin departamento/municipio/EPS asociadas no aparecen (joins INNER).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMorbidity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Morbilidad; Paciente; Tipo de identificación; Ingreso hospitalario; Centro de atención; Unidad funcional (hospitalaria/consulta externa); EPS / Administradora de salud; Profesional tratante; Especialidad; Diagnóstico CIE10 principal y relacionados; Ubicación geográfica (departamento/municipio)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMorbidity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Solo retorna atenciones con TIPHISPAC=''I'' y FECHISPAC >= ''2024-01-01''.; [RETURN_RESULT] resultset: Excluye atenciones sin diagnóstico principal en INDIAGNOP, debido al INNER JOIN con dxp sobre [CIE10 DIAGNOSTICO PRINCIPAL].', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMorbidity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pac.iptipodoc entre 1 y 15 → Mapea el código numérico al código alfabético del tipo de identificación (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI). else NULL; si PAC.IPSEXOPAC = 1 / = 2 → Clasifica el sexo como ''MASCULINO'' o ''FEMENINO'' respectivamente. else NULL; si HIS.GENCONEXT = 0 / = 1 → Marca el tipo de unidad como ''HOSPITALARIA'' o ''CONSULTA EXTERNA''. else NULL; si INDIAGNOP.CODDIAPRI = 1 → El diagnóstico se etiqueta como ''CIE10 DIAGNOSTICO PRINCIPAL''. else Si CODDIAPRI<>1 y es la 2ª/3ª fila por paciente-ingreso-folio, se etiqueta como ''CIE10 DIAGNOSTICO RELACIONADO 1'' o ''2'' (resto se descarta).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMorbidity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOS; Contract.HealthAdministrator; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.INDIAGNOP; dbo.indiagnos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMorbidity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMorbidity';
GO
