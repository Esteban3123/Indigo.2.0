
CREATE VIEW [Report].[VIEW_BRAQUITERAPIA_APLICACIONES] AS

SELECT * FROM 
(

   SELECT 
	CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
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
		WHEN 15 THEN 'SI' END AS 'TIPO IDENTIFICACION',
	CASE P.IPTIPODOC 
		WHEN '1' THEN 'CEDULA DE CIUDADANIA'  
		WHEN '2' THEN 'CEDULA DE EXTRANJERIA'  
		WHEN '3' THEN 'TARJETA DE IDENTIDAD'  
		WHEN '4' THEN 'REGISTRO CIVIL'  
		WHEN '5' THEN 'PASAPORTE'
		WHEN '6' THEN 'ADULTO SIN IDENTIFICACION'  
		WHEN '7' THEN 'MENOR SIN IDENTIFICACION'  
		WHEN '8' THEN 'NUMERO UNICO DE IDENTIFICACIÒN'  
		WHEN '9' THEN 'CERTIFICADO NACIDO VIVO' 
		WHEN '10' THEN 'CARNET DIPLOMATICO' 
		WHEN '11' THEN 'SALVOCONDUCTO' 
		WHEN '12' THEN 'PERMISO ESPECIAL DE PERMANENCIA' 
		WHEN '13' THEN 'PERMISO TEMPORAL DE PERMANECIA' 
		WHEN '14' THEN 'DOCUMENTO EXTRANJERO' 
		WHEN '15' THEN 'SIN IDENTIFICACION' END AS 'NOMBRE IDENTIFICACION', 
		ORD.IPCODPACI 'IDENTIFICACION', P.IPNOMCOMP 'NOMBRE COMPLETO', P.IPTELEFON AS 'TELEFONO' ,P.IPTELMOVI AS 'CEULAR',
    DP.depcodigo AS 'COD. DEPARTAMENTO', dp.nomdepart as 'NOMBRE DEPARTAMENTO',MU.MUNCODIGO AS 'COD. MUNICIPIO',MU.MUNNOMBRE AS 'MUNICIPIO', CUPS.CODGOCUPS AS 'COD.CUPS', 
    RTRIM(ORD.CODSERIPS) + '-' + CUPS.DESCODCUPS 'SERVICIO', CAST(ORD.FECHAORDEN AS DATE)  'FECH. ORDEN', PRON.NUMINGRES AS 'INGRESO',HE.HealthEntityCode AS 'CODIGO EAPB' ,HE.Name AS 'EAPB PACIENTE', cgro.name AS 'GRUPO DE ATENCION',  ORD.CODDIAGNO AS 'CIE10', DORD.NOMDIAGNO AS 'DIAGNOSTICO',
		(SELECT CAST(MAX([23]) AS DATE) FROM dbo.hconcopreg WHERE [6] = ord.ipcodpaci AND [17] = ord.coddiagno) AS [FECHA PATOLOGIA], 
   ING.IAUTORIZA AS 'AUTORIZACION',
   CASE ORD.ESTADO WHEN 1 THEN 'Solicitado'WHEN 5 THEN 'Finalizado'WHEN 6 THEN 'Orden Anulada' WHEN 7 THEN 'Tratamiento Completado'
   WHEN 8 THEN 'Braquiterapia Iniciada' END 'ESTADO ORDEN',PORD.NOMMEDICO 'PROFESIONAL QUE ORDENA', EPORD.DESESPECI 'ESPECIALIDA QUE ORDENA',
   RTRIM(ORD.CODCENATE) + '-' + CAORD.NOMCENATE 'CENTRO DE ATENCION DE ORDEN',
   CASE WHEN ORD.CODCENATE = '13031'THEN 'ARMENIA' 	 when ORD.CODCENATE = '13034' THEN 'ARMENIA'	 when ORD.CODCENATE = '13032' THEN 'ARMENIA'	 when ORD.CODCENATE = '11011' THEN 'PEREIRA'
   	 when ORD.CODCENATE = '11012' THEN 'PEREIRA'	 when ORD.CODCENATE = '12021' THEN 'MANIZALES'	 when ORD.CODCENATE = '14041' THEN 'CARTAGO'	 WHEN ORD.CODCENATE = '12022' THEN 'MANIZALES'
   END AS 'CIUDAD DE ORDEN',
   ROW_NUMBER() OVER(PARTITION BY P.IPCODPACI+ING.NUMINGRES  ORDER BY E.FECHAREGISTRO ASC) AS 'NUMERO DE SESION', 
   ISNULL(CAST(E.TOTALDOSIS AS CHAR), 'No Agendada') 'TOTAL DOSIS', 
   CASE WHEN E.TIPOTRATA = '1' THEN 'Intracavitaria'	 WHEN E.TIPOTRATA = '2' THEN 'Intraluminal'	 WHEN E.TIPOTRATA = '3' THEN 'Intersticial'	 WHEN E.TIPOTRATA = '4' THEN 'Superficial'
   END 'TIPO DE TRATAMIENTO',
   ISNULL(CAST(E.FECHAREGISTRO AS date), '1900-01-01') 'FEC. REGISTRO DOSIS',
   CASE WHEN E.DOSISPRESCRITA = '1' THEN 'Isocentro'	 WHEN E.DOSISPRESCRITA = '2' THEN 'Piel'	 WHEN E.DOSISPRESCRITA = '3' THEN 'Isocentro y Piel'	 WHEN E.DOSISPRESCRITA = '4' THEN 'Puntos A'
   	 WHEN E.DOSISPRESCRITA = '5' THEN 'Mucosa Vaginal'	 WHEN E.DOSISPRESCRITA = '6' THEN 'Mucosa'	 WHEN E.DOSISPRESCRITA = '7' THEN '10 mm'	 WHEN E.DOSISPRESCRITA = '8' THEN '8 mm'
   	 END 'DOSIS PRESCRITA A',
   CASE when E.CODCENATEDOSISBRA = '13034' THEN 'BRAQUITERAPIA CLINICA ARTURO LOPEZ CARDONA'	 when E.CODCENATEDOSISBRA = '13031' THEN 'BRAQUITERAPIA CENTENARIO'
   	 when E.CODCENATEDOSISBRA = '13032' THEN 'BRAQUITERAPIA SAN JUAN DE DIOS'	 when E.CODCENATEDOSISBRA = '11011' THEN 'BRAQUITERAPIA MARAYA'
   	 when E.CODCENATEDOSISBRA = '11012' THEN 'BRAQUITERAPIA CIRCUNVALAR'	 when E.CODCENATEDOSISBRA = '12021' THEN 'BRAQUITERAPIA SAN MARCEL'
   	 when E.CODCENATEDOSISBRA = '14041' THEN 'BRAQUITERAPIA CARTAGO'	 WHEN E.CODCENATEDOSISBRA = '12022' THEN 'BRAQUITERAPIA HOSPITALIZACION INFANTIL'
   	 END AS 'CENTRO DE APLICACION DOSIS',
   	 CASE WHEN E.CODCENATEDOSISBRA = '13031'THEN 'ARMENIA' 	 when E.CODCENATEDOSISBRA = '13034' THEN 'ARMENIA'	 when E.CODCENATEDOSISBRA = '13032' THEN 'ARMENIA'
   	 when E.CODCENATEDOSISBRA = '11011' THEN 'PEREIRA'	 when E.CODCENATEDOSISBRA = '11012' THEN 'PEREIRA'	 when E.CODCENATEDOSISBRA = '12021' THEN 'MANIZALES'
   	 when E.CODCENATEDOSISBRA = '14041' THEN 'CARTAGO'	 WHEN E.CODCENATEDOSISBRA = '12022' THEN 'MANIZALES'END AS 'CIUDAD APLICACION DOSIS',PRO.NOMMEDICO  AS 'USUARIO REGISTRO',
   1 as 'CANTIDAD',
  CAST(E.FECHAREGISTRO AS date) AS 'FECHA BUSQUEDA',
  YEAR(E.FECHAREGISTRO) AS 'AÑO BUSQUEDA',
  MONTH(E.FECHAREGISTRO) AS 'MES BUSQUEDA',
  CONCAT(FORMAT(MONTH(E.FECHAREGISTRO), '00') ,' - ', 
	   CASE MONTH(E.FECHAREGISTRO) 
	    WHEN 1 THEN 'ENERO'
   	    WHEN 2 THEN 'FEBRERO'
	    WHEN 3 THEN 'MARZO'
	    WHEN 4 THEN 'ABRIL'
	    WHEN 5 THEN 'MAYO'
	    WHEN 6 THEN 'JUNIO'
	    WHEN 7 THEN 'JULIO'
	    WHEN 8 THEN 'AGOSTO'
	    WHEN 9 THEN 'SEPTIEMBRE'
	    WHEN 10 THEN 'OCTUBRE'
	    WHEN 11 THEN 'NOVIEMBRE'
	    WHEN 12 THEN 'DICIEMBRE' END) 'MES NOMBRE BUSQUEDA',
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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a consolidar el seguimiento de aplicaciones de braquiterapia por paciente. Cruza órdenes médicas de procedimientos CUPS específicos de braquiterapia con los registros de dosis aplicadas, datos demográficos del paciente, ingreso, aseguradora (EAPB), diagnóstico CIE-10, profesional que ordena, centro y ciudad de aplicación, tipo de tratamiento y dosis prescrita. Numera secuencialmente las sesiones por paciente-ingreso y expone campos de fecha para filtrado por año/mes, destinada a consumo en herramientas de reporting clínico-operativo.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_BRAQUITERAPIA_APLICACIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_BRAQUITERAPIA_APLICACIONES';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida las aplicaciones/sesiones de braquiterapia por paciente, exponiendo datos demográficos, orden médica, ingreso, EAPB, diagnóstico, dosis, centro/ciudad de aplicación y métricas de fecha para análisis.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_BRAQUITERAPIA_APLICACIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes de HCRADORDEN deben tener un código de servicio CUPS perteneciente al rango de braquiterapia (''922602''…''922616''); Toda orden debe tener relación válida (INNER JOIN) con HCORDPRON, INPACIENT, INPROFSAL, INESPECIA, INDIAGNOS, INCUPSIPS, ADCENATEN, INUBICACI, INMUNICIP e INDEPARTA; La ubicación del paciente (AUUBICACI) debe resolver a un municipio y departamento existentes', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_BRAQUITERAPIA_APLICACIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La numeración de sesión se calcula con ROW_NUMBER() OVER (PARTITION BY paciente+ingreso ORDER BY FECHAREGISTRO ASC), garantizando secuencia cronológica por episodio; La columna CANTIDAD siempre es 1 (cada fila cuenta como una aplicación); ULT_ACTUAL siempre se entrega convertido a zona horaria ''Pakistan Standard Time''; ID_COMPANY se deriva dinámicamente de DB_NAME() truncado a 9 caracteres; Solo se exponen procedimientos cuyo código CUPS pertenece al catálogo fijo de braquiterapia listado en el WHERE; FECHA PATOLOGIA se obtiene como el MAX([23]) en hconcopreg para el paciente y diagnóstico de la orden', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_BRAQUITERAPIA_APLICACIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Braquiterapia; Orden médica radioterápica; Sesión de dosis; Tipo de tratamiento (Intracavitaria/Intraluminal/Intersticial/Superficial); Dosis prescrita (Isocentro, Piel, Puntos A, Mucosa); Paciente; Ingreso/Episodio asistencial; EAPB / Administradora de salud; Grupo de atención (caregroup); Diagnóstico CIE-10; Procedimiento CUPS; Centro de atención; Profesional de salud y especialidad; Autorización; Tipo de identificación (CC, CE, TI, RC, PA, etc.); Patología (fecha de patología desde concepto registro)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_BRAQUITERAPIA_APLICACIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.VIEW_BRAQUITERAPIA_APLICACIONES: Devuelve una fila por sesión de dosis de braquiterapia (HCRADDOSISBRAQUI) asociada a una orden cuyo CUPS está en la lista de procedimientos de braquiterapia; si la orden no tiene dosis, igualmente se incluye con valores por defecto (''No Agendada'', fecha 1900-01-01) por el LEFT JOIN.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_BRAQUITERAPIA_APLICACIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CUPS.CODSERIPS IN (''922602''…''922616'') → Se incluye la orden como procedimiento de braquiterapia en la vista else La orden se excluye del resultado; si E.FECHAREGISTRO/E.TOTALDOSIS NULL (no hay dosis registrada) → Se reporta ''No Agendada'' como TOTAL DOSIS y ''1900-01-01'' como FEC. REGISTRO DOSIS else Se muestra el total de dosis y la fecha real de registro; si ORD.ESTADO ∈ {1,5,6,7,8} → Se traduce a ''Solicitado'',''Finalizado'',''Orden Anulada'',''Tratamiento Completado'',''Braquiterapia Iniciada'' respectivamente else Se devuelve NULL en ESTADO ORDEN; si ORD.CODCENATE / E.CODCENATEDOSISBRA en {13031,13032,13034,11011,11012,12021,12022,14041} → Se mapea a la ciudad (Armenia, Pereira, Manizales, Cartago) y al nombre del centro de braquiterapia correspondiente else NULL en ciudad y centro de aplicación; si E.TIPOTRATA ∈ {1..4} → Se traduce a ''Intracavitaria'',''Intraluminal'',''Intersticial'',''Superficial'' else NULL; si E.DOSISPRESCRITA ∈ {1..8} → Se traduce a etiqueta clínica (Isocentro, Piel, Puntos A, Mucosa Vaginal, Mucosa, 10 mm, 8 mm, etc.) else NULL', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_BRAQUITERAPIA_APLICACIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCRADORDEN; dbo.HCORDPRON; dbo.HCRADDOSISBRAQUI; dbo.AGASICITA; dbo.ADINGRESO; Contract.HealthAdministrator; contract.caregroup; dbo.INPACIENT; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOS; dbo.INCUPSIPS; dbo.ADCENATEN; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.hconcopreg', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_BRAQUITERAPIA_APLICACIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_BRAQUITERAPIA_APLICACIONES';
GO
