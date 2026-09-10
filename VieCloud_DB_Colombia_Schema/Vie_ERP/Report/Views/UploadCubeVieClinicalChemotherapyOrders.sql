

CREATE view [Report].[UploadCubeVieClinicalChemotherapyOrders] AS

--ALTER PROCEDURE [EHR].[SP_QUIMIOTERAPIA]
--DECLARE	@FECINI Datetime ='2024-01-01';
--DECLARE	@FECFIN Datetime ='2024-04-30';

	SELECT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		CASE P.IPTIPODOC 	
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
		
		ORD.IPCODPACI AS 'NRO IDENTIFICACION',-- [NroIdentificacion],
		RTRIM(P.IPNOMCOMP) AS 'NOMBRE PACIENTE',--[NombrePaciente], 
		CAST(P.IPFECNACI AS DATE) AS 'FECHA NACIMIENTO',--[FechaNacimiento], 
		DATEDIFF(YEAR, P.IPFECNACI, ING.IFECHAING) AS 'EDAD',--[Edad],  
		P.IPTELEFON AS 'TELEFONO PRINCIPAL',--[TelPrincipal],
		P.IPTELMOVI AS 'TELEFONO ALTERNATIVO',--[TelAlternativo],
		--DP.depcodigo AS 'Cod. Departamento',
		dp.nomdepart AS 'DEPARTAMENTO',--[Departamento],
		--MU.MUNCODIGO AS 'Cod. Municipio',
		MU.MUNNOMBRE AS 'MINICIPIO',--[Municipio],
		UPPER(p.ipdirecci) AS 'DIRECCION',--[Direccion],
		H.Name AS 'ENTIDAD',--[Entidad],
		CG.Name AS 'GRUPO ATENCION',-- [GrupoAtencion], 

		ORD.NUMINGRES AS 'NRO DE INGRESO',--[NroIngreso],
		CEN.NOMCENATE AS 'CENTRO DE ATENCION',--[CentroAtencion],
		FUN.UFUDESCRI AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
		CASE 
			WHEN ORD.CODCENATE = '13031'THEN 'ARMENIA' 
			when ORD.CODCENATE = '13034' THEN 'ARMENIA'
			when ORD.CODCENATE = '13032' THEN 'ARMENIA'
			when ORD.CODCENATE = '11011' THEN 'PEREIRA'
			when ORD.CODCENATE = '11012' THEN 'PEREIRA'
			when ORD.CODCENATE = '12021' THEN 'MANIZALES'
			when ORD.CODCENATE = '14041' THEN 'CARTAGO'
			WHEN ORD.CODCENATE = '12022' THEN 'MANIZALES'
			WHEN ORD.CODCENATE = '12024' THEN 'MANIZALES'
			WHEN ORD.CODCENATE = '13033' THEN 'ARMENIA'
			WHEN ORD.CODCENATE = '12025' THEN 'LA DORADA' END AS 'CIUDAD PROCEDIMIENTO',-- [CiudadProcedimiento],
		S.Code AS 'CODIGO ESQUEMA',--[CodEsquema],
		S.Description AS 'ESQUEMA',--[Esquema],
		CASE s.typescheme
			WHEN 1 THEN 'Quimioterapia' 
			WHEN 2 THEN 'Enfermedades Huérfanas'
			WHEN 3 THEN 'Otros' END AS 'TIPO ESQUEMA',--[TipoEsquema],
		RTRIM(cup.description) AS 'SERVICIO ADMINISTRACION',--[ServicioAdministracion],
		CASE ORD.ESTADO 
			WHEN 1 THEN  'Orden Solicitada' 
			WHEN 2 THEN 'Esquema iniciado' 
			WHEN 3 THEN 'Esquema finalizado completo'
			WHEN 4 THEN 'Suspendido - Finalización prematura' 
			WHEN 5 THEN 'Anulado - Cuando no se ha iniciado tratamiento' END 'ESTADO',-- [Estado],
		ORD.CODDIAGNO 'CODIGO DIAGNOSTICO',--[CodDiagnostico],
		DIA.NOMDIAGNO AS 'DIAGNOSTICO',--[Diagnostico],
		CASE dxP.tipdiagno 
			WHEN 'I' THEN 'Impresion Diagnostica' 
			WHEN 'C' THEN 'Confirmado Nuevo' 
			WHEN 'R' THEN 'Confirmado Repetido' END AS 'TIPO DIAGNOSTICO',--[TipoDiagnostico],
		CONVERT(VARCHAR(10), cac.[23], 103) AS 'FECHA PATOLOGIA',--[FechaPatologia], 
		CAST(ORD.CICLOS AS CHAR) 'NRO CICLOS ESQUEMA',--[NroCiclosEsquema],
		CAST(DET.CICLO AS CHAR) 'CICLO ACTUAL',--[CicloActual],
		CAST(DET.DIA AS CHAR) 'DIA',--[Dia],
		M.NOMMEDICO AS 'PROFESIONAL ORDENO',--[ProfesionalOrdeno], 
		med.nommedico AS 'PROFEIONAL ORDENO CICLO',--[ProfesionalOrdenoCiclo], 
		CAST(ORD.FECHAREGISTRO AS DATE) 'FECHA ORDEN',--[FechaOrden],
		CAST(CIC.FECHAREGISTRO AS DATE) 'FECHA ORDEN CICLO',--[FechaOrdenCiclo], 
		CASE DET.ESTADODIA 
			WHEN 1 THEN 'Dia Sin Cumplir' 
			WHEN 2 THEN 'Dia cumplido' 
			WHEN 3 THEN 'Antes de Indigo' END 'ESTADO DIA',--[EstadoDia],
		ISNULL(CAST(CIT.FECHORAIN AS DATE),'1900-01-01') AS 'FECHA APLICACION',--[FechaAplicacion],
		ING.IAUTORIZA AS 'NRO AUTORIZACION',--,
		PRO.CODSERIPS  'CUPS',--[CUPS], 
		CE.Description as 'DESCRIPCION',--[Descripcion],
		[CICLO ESQUEMA] = concat('Ciclo ', ORD.CICLOACTUAL, ' de ' , ORD.CICLOS),
		sal.descripsal AS 'SALA',--[Sala]
		CAST(ISNULL(CIT.FECHORAIN,'1900-01-01')  AS DATE) 'FECHA BUSQUEDA',
		YEAR(ORD.FECHAREGISTRO) AS 'AÑO CARGUE',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL

	FROM EHR.HCORDQUIMIO ORD
	JOIN EHR.HCORDCICLOSD DET ON DET.IDHCORDQUIMIO =ORD.ID 
	JOIN EHR.HCORDCICLOS CIC ON DET.IDHCORDCICLOS=CIC.ID
	JOIN EHR.Schemes S ON S.Id = ORD.SchemesId
	LEFT JOIN contract.cupsentity AS cup ON s.serviceips = cup.code
	JOIN DBO.INPACIENT P ON P.IPCODPACI = ORD.IPCODPACI
	JOIN dbo.ADINGRESO AS ING ON ORD.NUMINGRES  =ING.NUMINGRES
	JOIN DBO.ADCENATEN CEN ON CEN.CODCENATE = ORD.CODCENATE
	JOIN dbo.INUNIFUNC AS FUN ON FUN.UFUCODIGO =ORD.UFUCODIGO
	JOIN Contract.HealthAdministrator  H with (nolock) on H.id = ING.GENCONENTITY
	JOIN Contract .CareGroup AS CG ON CG.Id =ING.GENCAREGROUP 
	JOIN DBO.INPROFSAL M ON M.CODPROSAL = ORD.CODPROSAL
	JOIN dbo.inprofsal AS med ON cic.codprosal = med.codprosal
	JOIN DBO.INUBICACI UB ON UB.AUUBICACI = P.AUUBICACI
	JOIN DBO.INMUNICIP MU ON MU.DEPMUNCOD = UB.DEPMUNCOD
	JOIN DBO.INDEPARTA DP ON DP.depcodigo = MU.DEPCODIGO
	LEFT JOIN dbo.INDIAGNOS AS DIA ON DIA.CODDIAGNO =ORD.CODDIAGNO
	LEFT JOIN DBO.AGASICITA CIT ON CIT.IDHCORDCICLOSD = DET.ID
	LEFT JOIN dbo.HCORDPRON AS PRO ON PRO.AUTO =CIC.IDHCORDPRON 
	LEFT JOIN Contract .CUPSEntity  AS CE ON CE.Code  =PRO.CODSERIPS 
	LEFT JOIN dbo.agensalac AS sal ON cit.idsala = sal.codconcec
	LEFT JOIN dbo.indiagnop AS dxp WITH (NOLOCK) ON ord.numingres = dxp.numingres AND ord.coddiagno = dxp.coddiagno
	LEFT JOIN dbo.hconcopreg AS cac ON ord.ipcodpaci = cac.[6] AND ord.coddiagno = cac.[17] 
	WHERE ORD.ESTADO in (1, 2, 3, 4) and CODESTCIT = 1 --AND CAST(ISNULL(CIT.FECHORAIN,'1900-01-01')  AS DATE) BETWEEN @FECINI AND @FECFIN 
	--ORDER BY P.IPCODPACI,S.Code
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista diseñada para alimentar un cubo de análisis (UploadCube) con información consolidada de órdenes de quimioterapia activas (estados 1 a 4). Integra datos demográficos del paciente, entidad aseguradora, centro y unidad funcional, esquema terapéutico, ciclo y día de tratamiento, profesional prescriptor, diagnóstico CIE-10, código CUPS del procedimiento y fecha de aplicación de la cita. Aplica filtro sobre citas con estado 1 y excluye órdenes anuladas sin tratamiento iniciado (estado 5), consolidando múltiples sedes geográficas mediante mapeo de códigos de centro de atención.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalChemotherapyOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalChemotherapyOrders';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida información de órdenes de quimioterapia (esquema, ciclos, días, paciente, diagnóstico, entidad, centro y cita de aplicación) para alimentar un cubo analítico de procedimientos oncológicos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalChemotherapyOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden debe existir en EHR.HCORDQUIMIO con detalle de ciclo (HCORDCICLOSD) y cabecera de ciclo (HCORDCICLOS); El paciente, ingreso, centro de atención, unidad funcional, entidad administradora, grupo de atención, profesional ordenador y ubicación geográfica deben existir (JOINs internos); La cita asociada (AGASICITA) debe tener CODESTCIT = 1 para incluirse; El estado de la orden (ORD.ESTADO) debe estar en {1,2,3,4}; se excluye estado 5 (Anulado)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalChemotherapyOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes con estado 1-4 (excluye anuladas); Solo citas con CODESTCIT = 1 generan filas con fecha de aplicación efectiva; La edad se calcula con DATEDIFF(YEAR) entre fecha de nacimiento del paciente y fecha de ingreso (ING.IFECHAING); El campo ULT_ACTUAL siempre se sella con GETDATE() convertido a ''Pakistan Standard Time''; El año de cargue se deriva del año de FECHAREGISTRO de la orden; El identificador de compañía corresponde a DB_NAME() truncado a 9 caracteres; La etiqueta CICLO ESQUEMA siempre tiene formato ''Ciclo X de Y'' (ciclo actual sobre total)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalChemotherapyOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Quimioterapia; Esquema de tratamiento oncológico; Ciclo de quimioterapia; Día de administración; Orden médica; Paciente; Ingreso/Admisión; Diagnóstico CIE-10; Tipo de diagnóstico (Impresión/Confirmado); Centro de atención; Unidad funcional; Entidad administradora de salud (EPS); Grupo de atención; Autorización; CUPS; Cita / Sala de aplicación; Enfermedades Huérfanas; Patología oncológica', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalChemotherapyOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalChemotherapyOrders: Devuelve una fila por combinación orden-ciclo-día con cita en estado 1, mapeando códigos a etiquetas legibles (tipo de documento, estado, tipo de esquema, tipo de diagnóstico, estado del día, ciudad por código de centro).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalChemotherapyOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si P.IPTIPODOC entre 1 y 15 → Mapea a sigla de tipo de identificación (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI); si ORD.CODCENATE → Asigna ciudad de procedimiento: 13031/13032/13033/13034 → ARMENIA; 11011/11012 → PEREIRA; 12021/12022/12024 → MANIZALES; 14041 → CARTAGO; 12025 → LA DORADA; si S.typescheme → 1=Quimioterapia, 2=Enfermedades Huérfanas, 3=Otros; si ORD.ESTADO → 1=Orden Solicitada, 2=Esquema iniciado, 3=Esquema finalizado completo, 4=Suspendido - Finalización prematura, 5=Anulado (excluido por WHERE); si dxP.tipdiagno → I=Impresión Diagnóstica, C=Confirmado Nuevo, R=Confirmado Repetido; si DET.ESTADODIA → 1=Día Sin Cumplir, 2=Día cumplido, 3=Antes de Indigo; si CIT.FECHORAIN IS NULL → Se reporta fecha de aplicación/búsqueda como ''1900-01-01''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalChemotherapyOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCORDQUIMIO; EHR.HCORDCICLOSD; EHR.HCORDCICLOS; EHR.Schemes; contract.CUPSEntity; dbo.INPACIENT; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INUNIFUNC; Contract.HealthAdministrator; Contract.CareGroup; dbo.INPROFSAL; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.INDIAGNOS; dbo.AGASICITA; dbo.HCORDPRON; dbo.agensalac; dbo.indiagnop; dbo.hconcopreg', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalChemotherapyOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalChemotherapyOrders';
GO
