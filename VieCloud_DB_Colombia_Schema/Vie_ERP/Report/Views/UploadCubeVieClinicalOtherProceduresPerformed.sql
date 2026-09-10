

--CREATE PROCEDURE [EHR].[SP_PROCEDIMIENTOS_REALIZADOS]
--DECLARE	@FechaInicio Datetime='2024-05-01';
--DECLARE	@FechaFin Datetime ='2024-05-05';
--AS

CREATE view [Report].[UploadCubeVieClinicalOtherProceduresPerformed] AS

	WITH CTE_LISTADO_FINAL
	AS
	(
		SELECT  * FROM DBO.HCHISPACA AS HIS 
		WHERE YEAR(HIS.FECHISPAC)>=2023 AND 
		HIS.HCOTROSPROC =1 AND (HIS.GENCONEXT = 0)-- AND (HIS.TIPHISPAC = 'N')
	), salas AS 
	(
		SELECT ctrl.numingres, sala.codigsala + ' - ' + sala.descripsal AS sala, MAX(ctrl.ipfechaco) AS fecha
		FROM dbo.adconcoex ctrl
		INNER JOIN dbo.agasicita AS cit ON ctrl.numconcit = cit.codautonu AND cit.tipsolicitu = 2
		INNER JOIN dbo.agensalac AS sala ON cit.idsala = sala.codconcec 
		WHERE YEAR(ctrl.ipfechaco)>=2023
		GROUP BY ctrl.numingres, sala.codigsala, sala.descripsal
	)

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

			HIS.IPCODPACI AS 'IDENTIFICACION',--[NroIdentificacion], 
			PAC.IPNOMCOMP AS 'NOMBRE PACIENTE',--[NombrePaciente], 

			CASE PAC.IPSEXOPAC WHEN '1' THEN 'H' WHEN '2' THEN 'M' END AS 'CODIGO SEXO',-- [CodigoSexo],
			CASE PAC.IPSEXOPAC WHEN '1' THEN 'HOMBRE' WHEN '2' THEN 'MUJER' END AS 'SEXO',--[Sexo],
			CAST(PAC.IPFECNACI AS DATE) AS 'FECHA NACIMIENTO',--[FechaNacimiento], 
			DATEDIFF(YEAR, PAC.IPFECNACI, GETDATE()) AS 'EDAD',--[Edad], 
			PAC.IPTELEFON AS 'TELEFONO PRINCIPAL',--[TelefonoPrincipal],
			PAC.IPTELMOVI AS 'TELEFONO ALTERNTIVO',--[TelefonoAlternativo],
			DEP.depcodigo AS 'CODIGO DEPARTAMENTO',--[CodigoDepartamento],
			DEP.nomdepart AS 'DEPARTAMENTO',--[Departamento],
			MUN.MUNCODIGO 'CODIGO MUNICIPIO',--[CodigoMunicipio], 
			MUN.MUNNOMBRE AS 'MUNICIPIO',--[Municipio],
			UPPER(PAC.IPDIRECCI) AS 'DIRECCION',--[Direccion],
			TP.Nit AS 'NIT',--[NIT],
			HA.HealthEntityCode 'CODIGO ENTIDAD',--[CodigoEntidad],
			HA.Name AS 'ENTIDAD',--[Entidad],
			CASE HA.EntityType 
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
				WHEN 12 THEN 'Otros' end as 'REGIMEN',--[Regimen],
			CG.Code 'CODIGO GRUPO ATENCION',--[CodigoGrupoAtencion],
			CG.Name 'GRUPO ATENCION',--[GrupoAtencion],

			HIS.NUMINGRES AS 'NRO INGRESO',--[NroIngreso],
			ing.ifechaing AS 'FECHA INGRESO',--[FechaIngreso],
			CEN.NOMCENATE AS 'CENTRO ATENCION',--[CentroAtencion],
			HIS.UFUCODIGO AS 'CODIGO UNIDAD FUNCIONAL',--[CodigoUnidadFuncional], 
			UNI.UFUDESCRI AS 'UNIDAD FUNCIONAL',--[UnidadFuncional], 
			sala.sala AS 'SALA',--[Sala],
			HIS.CODPROSAL AS 'NRO IDENTIFICACION PROFESIONAL',--[NroIdentificacionProfesional], 
			PRO.NOMMEDICO AS 'PROFESIONAL',--[Profesional], 
			HIS.CODESPTRA AS 'CODIGO ESPECIALIDAD',--[CodigoEspecialidad],
			ESP.DESESPECI AS 'ESPECIALIDAD',--[Especialidad], 
			CAST(HIS.FECHISPAC AS DATE) AS 'FECHA HISTORIA',--[FechaHistoria], 
			HIS.CODDIAGNO AS 'CODIGO DIAGNOSTICO',--[CodigoDiagnostico], 
			DIA.NOMDIAGNO AS 'DIAGNOSTICO',--[Diagnostico],
			CUPS.CODSERIPS AS 'CUPS',--[CUPS],
			IPS.DESSERIPS AS 'DESCRIPCION CUPS',--[DescripcionCUPS]
			CAST(HIS.FECHISPAC AS date ) AS [FECHA BUSQUEDA],
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
		FROM 
		dbo.HCNOTEVO1 AS URG INNER JOIN
		DBO.HCHISPACA AS HIS WITH (NOLOCK) ON URG.IDETIPHIS = HIS.IDETIPHIS AND URG.IPCODPACI = HIS.IPCODPACI AND URG.NUMINGRES = HIS.NUMINGRES AND HIS.NUMEFOLIO = URG.NUMEFOLIO INNER JOIN
		CTE_LISTADO_FINAL AS FIN ON FIN.ID =HIS.ID INNER JOIN
		dbo.HCPLAOTRPROC C  WITH (NOLOCK) ON C.IDHCHISPACA=HIS.ID INNER JOIN
		dbo.ADINGRESO AS ING WITH (NOLOCK) ON ING.NUMINGRES =URG.NUMINGRES INNER JOIN
		Contract.CareGroup AS CG WITH (NOLOCK) ON ING.GENCAREGROUP =CG.Id INNER JOIN
		Contract.HealthAdministrator AS HA WITH (NOLOCK) ON ING.GENCONENTITY =HA.Id INNER JOIN
		Common .ThirdParty AS TP WITH (NOLOCK) ON HA.ThirdPartyId =TP.Id INNER JOIN
		dbo.INPACIENT AS PAC WITH (NOLOCK) ON HIS.IPCODPACI = PAC.IPCODPACI INNER JOIN
		dbo.ADCENATEN AS CEN WITH (NOLOCK) ON HIS.CODCENATE = CEN.CODCENATE INNER JOIN
		dbo.INUNIFUNC AS UNI WITH (NOLOCK) ON HIS.UFUCODIGO = UNI.UFUCODIGO INNER JOIN
		dbo.INPROFSAL AS PRO WITH (NOLOCK) ON HIS.CODPROSAL = PRO.CODPROSAL INNER JOIN
		dbo.INESPECIA AS ESP WITH (NOLOCK) ON HIS.CODESPTRA = ESP.CODESPECI INNER JOIN
		dbo.INDIAGNOS AS DIA WITH (NOLOCK) ON HIS.CODDIAGNO = DIA.CODDIAGNO INNER JOIN 
		DBO.HCPLAOTRPROCUPS AS CUPS WITH (NOLOCK) ON CUPS.IDHCPLAOTRPROC =C.ID INNER JOIN
		dbo.INCUPSIPS AS IPS  WITH (NOLOCK) ON IPS.CODSERIPS =CUPS.CODSERIPS LEFT JOIN 
		DBO.INUBICACI AS UBI WITH (NOLOCK) ON PAC.AUUBICACI =UBI.AUUBICACI LEFT JOIN 
		DBO.INMUNICIP AS MUN WITH (NOLOCK) ON UBI.DEPMUNCOD =MUN.DEPMUNCOD LEFT JOIN 
		DBO.INDEPARTA AS DEP WITH (NOLOCK) ON DEP.depcodigo =MUN.DEPCODIGO
		LEFT JOIN salas AS sala WITH (NOLOCK) ON urg.numingres = sala.numingres

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone un dataset consolidado de pacientes con otros procedimientos clínicos planeados/realizados (CUPS) desde 2023, enriquecido con datos demográficos, ingreso, profesional, especialidad, diagnóstico y sala, para carga al cubo de reportería.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOtherProceduresPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen historias clínicas en HCHISPACA con HCOTROSPROC=1 y GENCONEXT=0 a partir de 2023; Existe relación entre HCHISPACA y HCPLAOTRPROC (otros procedimientos) y sus CUPS asociados en HCPLAOTRPROCUPS; Existe nota de evolución (HCNOTEVO1) ligada por IDETIPHIS, IPCODPACI, NUMINGRES y NUMEFOLIO; El ingreso (ADINGRESO) tiene grupo de atención (CareGroup) y entidad administradora (HealthAdministrator) válidos; Maestros de paciente, centro, unidad funcional, profesional, especialidad, diagnóstico y CUPS deben existir', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOtherProceduresPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen historias con marca de otros procedimientos (HCOTROSPROC=1); Se excluyen historias con consulta externa (GENCONEXT<>0); Solo registros con FECHISPAC desde el año 2023 en adelante; La sala asignada corresponde a la última fecha de control (MAX ipfechaco) para citas con tipsolicitu=2; La edad se calcula en años entre fecha de nacimiento y la fecha actual; ID_COMPANY se obtiene del nombre de la base de datos actual truncado a 9 caracteres; ULT_ACTUAL se entrega convertido a zona horaria ''Pakistan Standard Time''; Las uniones a ubicación, municipio y departamento son opcionales (LEFT JOIN); el resto son obligatorias (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOtherProceduresPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de identificación; Sexo; Edad; Ubicación geográfica (departamento/municipio); Entidad administradora de salud (EPS/ARL/MP); Régimen de afiliación; Grupo de atención; Ingreso/admisión; Centro de atención; Unidad funcional; Sala; Profesional de la salud; Especialidad; Historia clínica; Diagnóstico; Otros procedimientos clínicos; CUPS; Nota de evolución', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOtherProceduresPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalOtherProceduresPerformed: Devuelve un registro distinto por combinación paciente/ingreso/historia/CUPS cuando YEAR(FECHISPAC)>=2023, HCOTROSPROC=1 y GENCONEXT=0', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOtherProceduresPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si iptipodoc del paciente (1..15) → Mapea a etiqueta de tipo de identificación (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI); si IPSEXOPAC = ''1'' o ''2'' → Clasifica sexo como HOMBRE (H) o MUJER (M) else NULL; si HealthAdministrator.EntityType (1..12) → Asigna régimen: EPS Contributivo/Subsidiado, ET Vinculados, ARL, Medicina Prepagada, IPS Privada/Pública, Régimen Especial, Accidentes de tránsito, Fosyga u Otros; si agasicita.tipsolicitu = 2 → Considera la cita como vinculada a sala para obtener la última fecha de control por ingreso', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOtherProceduresPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.HCNOTEVO1; dbo.HCPLAOTRPROC; dbo.HCPLAOTRPROCUPS; dbo.ADINGRESO; dbo.adconcoex; dbo.agasicita; dbo.agensalac; Contract.CareGroup; Contract.HealthAdministrator; Common.ThirdParty; dbo.INPACIENT; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOS; dbo.INCUPSIPS; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOtherProceduresPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalOtherProceduresPerformed';
GO
