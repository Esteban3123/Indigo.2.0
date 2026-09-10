



--CREATE PROCEDURE [EHR].[SP_BRAQUITERAPIA_APLICACIONES]
--DECLARE	@FechaInicio Datetime ='2024-04-01';
--DECLARE	@FechaFin Datetime ='2024-04-30';
--AS

CREATE view [Report].[UploadCubeVieClinicalMedicalRadiotherapyApplications] AS

	SELECT * FROM 
	(
		SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
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
				WHEN 15 THEN 'SI' END AS 'TIPO IDENTIFICACION',-- [TipoIdentificacion],
			ORD.IPCODPACI 'NRO IDENTIFICACION',--[NroIdentificacion], 
			RTRIM(P.IPNOMCOMP) 'NOMBRE PACIENTE',--[Nombre], 
			P.IPTELEFON AS 'TELEFONO PRINCIPAL',--[TelPrincipal],
			P.IPTELMOVI AS 'TELEFONO SECUNDARIO',--[TelSecundario],
			dp.nomdepart as 'DEPARTAMENTO',--[Departamento],
			MU.MUNNOMBRE AS 'MUNICIPIO',--[Municipio], 
			HE.HealthEntityCode AS 'CODIGO EPS',--[CodEPS],
			HE.Name AS 'EPS',--[EPS], 
			cgro.name AS 'GRUPO ATENCION',--[GrpAtencion], 
			PRON.NUMINGRES AS 'NRO INGRESO',--[NroIngreso],
			ORD.CODDIAGNO AS 'CIE 10',--[CodDiagnostico], 
			DORD.NOMDIAGNO AS 'DIAGNOSTICO',--[Diagnostico],
			(SELECT CAST(MAX([23]) AS DATE) FROM dbo.hconcopreg WHERE [6] = ord.ipcodpaci AND [17] = ord.coddiagno) AS 'FECHA PATOLOGIA',-- [FechaPatologia], 
			CUPS.CODGOCUPS AS 'CODIGO CUPS',--[CodCUPS], 
			RTRIM(ORD.CODSERIPS) + '-' + CUPS.DESCODCUPS AS 'DESCRIPCION CUPS',--[Servicio], 
			CAST(ORD.FECHAORDEN AS DATE) AS 'FECHA ORDEN',--[FechaOrden], 
			ING.IAUTORIZA AS 'NRO AUTORIZACION',--[NroAutorizacin],
			CASE ORD.ESTADO 
				WHEN 1 THEN 'Solicitado'
				WHEN 5 THEN 'Finalizado'
				WHEN 6 THEN 'Orden Anulada' 
				WHEN 7 THEN 'Tratamiento Completado'
				WHEN 8 THEN 'Braquiterapia Iniciada' END 'ESTADO ORDEN',-- [EstadoOrden],
			PORD.NOMMEDICO AS 'MEDICO ORDENA',-- [MedicoOrdena], 
			EPORD.DESESPECI AS 'ESPECIALIDAD ORDENA',--[EspecialidadOrdena],
			RTRIM(ORD.CODCENATE) + '-' + CAORD.NOMCENATE 'CENTRO ATENCION ORDEN',--[CenAtencionOrden],
			CASE 
				WHEN ORD.CODCENATE = '13031' THEN 'ARMENIA' 	 
				when ORD.CODCENATE = '13034' THEN 'ARMENIA'	 
				when ORD.CODCENATE = '13032' THEN 'ARMENIA'	 
				when ORD.CODCENATE = '11011' THEN 'PEREIRA'
				when ORD.CODCENATE = '11012' THEN 'PEREIRA'	 
				when ORD.CODCENATE = '12021' THEN 'MANIZALES'	 
				when ORD.CODCENATE = '14041' THEN 'CARTAGO'	 
				WHEN ORD.CODCENATE = '12022' THEN 'MANIZALES'END AS 'CIUDAD ORDEN',--[CiudadOrden],
			ROW_NUMBER() OVER(PARTITION BY P.IPCODPACI+ING.NUMINGRES  ORDER BY E.FECHAREGISTRO ASC) AS 'NUMERO SESION',--[NumeroSesion], 
			ISNULL(CAST(E.TOTALDOSIS AS CHAR), 'No Agendada') 'TOTAL DOSIS',--[TotalDosis], 
			CASE 
				WHEN E.TIPOTRATA = '1' THEN 'Intracavitaria'	 
				WHEN E.TIPOTRATA = '2' THEN 'Intraluminal'	 
				WHEN E.TIPOTRATA = '3' THEN 'Intersticial'	 
				WHEN E.TIPOTRATA = '4' THEN 'Superficial' END AS 'TIPO TRATAMIENTO',--[TipoTratamiento],
			ISNULL(CAST(E.FECHAREGISTRO AS date), '1900-01-01') AS 'FECHA REGISTRO DOSIS',--[FechaRegistroDosis],
			CASE 
				WHEN E.DOSISPRESCRITA = '1' THEN 'Isocentro'	 
				WHEN E.DOSISPRESCRITA = '2' THEN 'Piel'	 
				WHEN E.DOSISPRESCRITA = '3' THEN 'Isocentro y Piel'	 
				WHEN E.DOSISPRESCRITA = '4' THEN 'Puntos A'
				WHEN E.DOSISPRESCRITA = '5' THEN 'Mucosa Vaginal'	 
				WHEN E.DOSISPRESCRITA = '6' THEN 'Mucosa'	 
				WHEN E.DOSISPRESCRITA = '7' THEN '10 mm'	 
				WHEN E.DOSISPRESCRITA = '8' THEN '8 mm' END 'DOSIS PRESCRITA',--[DodisPrescrita],
			CASE 
				when E.CODCENATEDOSISBRA = '13034' THEN 'BRAQUITERAPIA CLINICA ARTURO LOPEZ CARDONA'	 
				when E.CODCENATEDOSISBRA = '13031' THEN 'BRAQUITERAPIA CENTENARIO'
				when E.CODCENATEDOSISBRA = '13032' THEN 'BRAQUITERAPIA SAN JUAN DE DIOS'	
				when E.CODCENATEDOSISBRA = '11011' THEN 'BRAQUITERAPIA MARAYA'
				when E.CODCENATEDOSISBRA = '11012' THEN 'BRAQUITERAPIA CIRCUNVALAR'	 
				when E.CODCENATEDOSISBRA = '12021' THEN 'BRAQUITERAPIA SAN MARCEL'
				when E.CODCENATEDOSISBRA = '14041' THEN 'BRAQUITERAPIA CARTAGO'	 
				WHEN E.CODCENATEDOSISBRA = '12022' THEN 'BRAQUITERAPIA HOSPITALIZACION INFANTIL' END AS 'CENTRO ATENCION APLICACION',--[CenAtencoinAplicacion],
			CASE 
				WHEN E.CODCENATEDOSISBRA = '13031'THEN 'ARMENIA' 	 
				when E.CODCENATEDOSISBRA = '13034' THEN 'ARMENIA'	 
				when E.CODCENATEDOSISBRA = '13032' THEN 'ARMENIA'
				when E.CODCENATEDOSISBRA = '11011' THEN 'PEREIRA'	 
				when E.CODCENATEDOSISBRA = '11012' THEN 'PEREIRA'	 
				when E.CODCENATEDOSISBRA = '12021' THEN 'MANIZALES'
				when E.CODCENATEDOSISBRA = '14041' THEN 'CARTAGO'	 
				WHEN E.CODCENATEDOSISBRA = '12022' THEN 'MANIZALES'END AS 'CIUDAD APLICACION',--[CiudadAlicacion],
			PRO.NOMMEDICO  AS 'USUARIO REGISTRO',--[UsuarioRegistro],
			'1' AS 'CANTIDAD',-- [Cantidad],
			ISNULL(CAST(E.FECHAREGISTRO AS date), '1900-01-01') 'FECHA BUSQUEDA',
		   CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
		FROM DBO.HCRADORDEN ORD
		JOIN dbo.HCORDPRON AS PRON ON PRON.AUTO =ORD.IDHCORDPRON 
		LEFT JOIN DBO.HCRADDOSISBRAQUI E ON E.IDHCRADORDEN = ORD.ID
		LEFT JOIN dbo.AGASICITA AS AGA ON E.IDCITA=AGA.CODAUTONU 
		LEFT JOIN dbo.ADINGRESO AS ING ON PRON.NUMINGRES =ING.NUMINGRES
		LEFT JOIN Contract.HealthAdministrator AS HE ON ING.GENCONENTITY =HE.Id 
		LEFT JOIN contract.caregroup AS cgro ON ing.gencaregroup = cgro.id
		JOIN DBO.INPACIENT P ON P.IPCODPACI = ORD.IPCODPACI
		JOIN DBO.INPROFSAL PORD ON PORD.CODPROSAL = ORD.CODPROSAL
		JOIN DBO.INESPECIA EPORD ON EPORD.CODESPECI = ORD.CODESPECI
		JOIN DBO.INDIAGNOS DORD ON DORD.CODDIAGNO = ORD.CODDIAGNO
		JOIN DBO.INCUPSIPS CUPS ON CUPS.CODSERIPS = ORD.CODSERIPS
		JOIN DBO.ADCENATEN CAORD ON CAORD.CODCENATE = ORD.CODCENATE
		JOIN DBO.INUBICACI UB ON UB.AUUBICACI = P.AUUBICACI
		JOIN DBO.INMUNICIP MU ON MU.DEPMUNCOD = UB.DEPMUNCOD
		JOIN DBO.INDEPARTA DP ON DP.depcodigo = MU.DEPCODIGO
		LEFT JOIN DBO.INPROFSAL PRO ON PRO.CODPROSAL = E.USUARIOREGISTRO
		where CUPS.CODSERIPS IN ('922602','922603','922604','922605','922606','922607','922608','922609','922610','922611','922612','922613','922614','922615','922616')

	) BRAQ
	--WHERE CAST (BRAQ.[FECHA REGISTRO DOSIS]  AS DATE)  BETWEEN @FechaInicio AND @FechaFin
	--order by BRAQ.[NRO IDENTIFICACION], BRAQ.[FECHA REGISTRO DOSIS] 
 
--GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una vista la información de aplicaciones de braquiterapia (órdenes, sesiones, dosis, paciente, EPS y centro de atención) para alimentar un cubo analítico de radioterapia.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalRadiotherapyApplications';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El servicio CUPS de la orden debe pertenecer al rango de braquiterapia (''922602''..''922616''); Deben existir datos maestros relacionados (paciente, profesional, especialidad, diagnóstico, CUPS, centro de atención, ubicación, municipio, departamento) para que la fila aparezca; La orden debe estar asociada a una orden de pronóstico (HCORDPRON) vía IDHCORDPRON', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalRadiotherapyApplications';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen servicios CUPS de braquiterapia (922602–922616); La numeración de sesión se calcula por paciente+ingreso ordenando por fecha de registro ascendente; La cantidad reportada por fila siempre es 1; La fecha de patología se toma como el máximo registro en hconcopreg para el paciente y diagnóstico de la orden; La marca de última actualización (ULT_ACTUAL) se calcula con la hora actual convertida a ''Pakistan Standard Time''; ID_COMPANY se trunca al nombre de la base de datos a 9 caracteres', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalRadiotherapyApplications';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de identificación; EPS / Administradora de salud; Grupo de atención; Ingreso; Diagnóstico CIE-10; Servicio CUPS; Orden médica; Autorización; Estado de la orden; Médico ordenante; Especialidad; Centro de atención; Ciudad; Sesión de braquiterapia; Dosis prescrita; Tipo de tratamiento (Intracavitaria/Intraluminal/Intersticial/Superficial); Fecha de patología; Radioterapia / Braquiterapia', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalRadiotherapyApplications';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalMedicalRadiotherapyApplications: Devuelve una fila por orden/sesión de braquiterapia cuyo CUPS está entre ''922602'' y ''922616'', enriquecida con datos del paciente, EPS, diagnóstico y centro de atención', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalRadiotherapyApplications';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC entre 1 y 15 → Mapea el código numérico de tipo de documento a su sigla (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI); si ORD.ESTADO en (1,5,6,7,8) → Traduce el estado de la orden a: Solicitado, Finalizado, Orden Anulada, Tratamiento Completado o Braquiterapia Iniciada; si E.TIPOTRATA en (''1'',''2'',''3'',''4'') → Clasifica el tipo de tratamiento como Intracavitaria, Intraluminal, Intersticial o Superficial; si E.DOSISPRESCRITA en (''1''..''8'') → Traduce la dosis prescrita a Isocentro, Piel, Isocentro y Piel, Puntos A, Mucosa Vaginal, Mucosa, 10 mm u 8 mm; si ORD.CODCENATE coincide con un código de centro conocido → Asigna ciudad de la orden (ARMENIA, PEREIRA, MANIZALES o CARTAGO) según el centro; si E.CODCENATEDOSISBRA coincide con un código de centro conocido → Asigna nombre del centro de aplicación de braquiterapia y su ciudad else NULL; si E.TOTALDOSIS es NULL → Reporta ''No Agendada'' como total de dosis; si E.FECHAREGISTRO es NULL → Asume fecha ''1900-01-01'' como fecha de registro de dosis y fecha de búsqueda', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalRadiotherapyApplications';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCRADORDEN; dbo.HCORDPRON; dbo.HCRADDOSISBRAQUI; dbo.AGASICITA; dbo.ADINGRESO; Contract.HealthAdministrator; contract.caregroup; dbo.INPACIENT; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOS; dbo.INCUPSIPS; dbo.ADCENATEN; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.hconcopreg', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalRadiotherapyApplications';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalRadiotherapyApplications';
GO
