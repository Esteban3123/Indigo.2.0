
CREATE view [Report].[VIEW_RADIOTERAPIA_APLICACIONES] as

select * FROM
(
 select 
  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
  CEN.NOMCENATE 'CENTRO DE ATENCION',HEA.Code 'CODIGO ENTIDAD' ,RTRIM(HEA.Name) 'ENTIDAD',RTRIM(CGR.Name) 'GRUPO DE ATENCION',ORD.IPCODPACI 'IDENTIFICACION',RTRIM(PAC.IPNOMCOMP) 'PACIENTE', 
  ESQ.NUMINGRES 'INGRESO',CAST(ING.IFECHAING AS DATE) 'FECHA INGRESO' ,ORD.CODDIAGNO 'CIE10',DIA.NOMDIAGNO 'DIAGNOSTICO',ORD.CODSERIPS 'CUPS',B.DESSERIPS 'DESCRIPCION SERVICIO' ,
  CAST(ORD.FECHAORDEN AS DATE) 'FECHA DE ORDEN' ,
  CASE ORD.ESTADO WHEN 1 THEN 'SOLICITADO' WHEN 2 THEN 'SIMULACION PROGRAMADA' WHEN 3 THEN 'PLANEACION PROGRAMADA' WHEN 4 THEN 'PLANEACION CONFIRMADA' WHEN 5 THEN 'FINALIZADO'
  WHEN 6 THEN 'ANULADO' WHEN 7 THEN 'COMPLETADO' WHEN 8 THEN 'BRAQUITERAPIA INICIADA' END AS 'ESTADO',
  CAST(FECHASIMULA AS DATE) 'FECHA SIMULACION' ,CAST(ORD.FECHAPLANEA AS DATE) 'FECHA PLANEACION', 
  ESQ.DOSTOTAL 'DOSIS TOTAL' ,ESQ.DOSISPORSESION 'DOSIS POR SESION',ESQ.NUMSESION 'NRO DE SESIONES' ,CAST(ESQ.FECHACREA AS DATE) 'FECHA ESQUEMA',
  ROW_NUMBER() OVER(PARTITION BY ORD.IPCODPACI+ESQ.NUMINGRES  ORDER BY DOS.FECHAREGISTRO ASC) AS 'NUMERO DE SESION',CAST(DOS.FECHAREGISTRO AS DATE) 'FECHA APLICACION',
  prof.nommedico AS [PROFESIONAL ORDENO],
  espe.codespeci + ' - ' + espe.desespeci AS ESPECIALIDAD,
  USU.NOMUSUARI  'USUARIO APLICO', DOS.DOSISTUMOR1 ,DOS.DOSISTUMOR2,DOS.DOSISTUMOR3,DOS.DOSISTUMOR4,DOS.DOSISTUMOR5,DOS.DOSISTUMOR6,DOS.TOTALDOSIS ,DOS.OBSERVACION,
  1 as 'CANTIDAD',
  CAST(DOS.FECHAREGISTRO AS date) AS 'FECHA BUSQUEDA',
  YEAR(DOS.FECHAREGISTRO) AS 'AÑO BUSQUEDA',
  MONTH(DOS.FECHAREGISTRO) AS 'MES BUSQUEDA',
  CONCAT(FORMAT(MONTH(DOS.FECHAREGISTRO), '00') ,' - ', 
	   CASE MONTH(DOS.FECHAREGISTRO) 
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
  INNER JOIN dbo.inprofsal AS prof  ON ORD.codprosal = prof.codprosal
  INNER JOIN dbo.inespecia AS espe  ON prof.codespec1 = espe.codespeci
  INNER JOIN dbo.HCORDPRON AS PRON  ON PRON.AUTO =ORD.IDHCORDPRON
  INNER JOIN dbo.INPACIENT AS PAC  ON PAC.IPCODPACI =ORD.IPCODPACI 
  INNER JOIN dbo.ADCENATEN AS CEN  ON ORD.CODCENATE =CEN.CODCENATE
  INNER JOIN dbo.INCUPSIPS B  ON ORD.CODSERIPS=B.CODSERIPS
  INNER JOIN DBO.INDIAGNOS AS DIA WITH(NOLOCK) ON DIA.CODDIAGNO =ORD.CODDIAGNO 
  LEFT JOIN DBO.HCRADESQUEMAS ESQ  ON ESQ.IDHCRADORDEN = ORD.ID
  LEFT JOIN dbo.ADINGRESO AS ING  ON ESQ.NUMINGRES =ING.NUMINGRES 
  LEFT JOIN DBO.HCRADDOSIS DOS  ON DOS.IDHCRADESQUEMAS = ESQ.ID
  LEFT JOIN Contract .HealthAdministrator HEA  ON ING.GENCONENTITY =HEA.ID
  LEFT JOIN Contract .CareGroup AS CGR  ON ING.GENCAREGROUP =CGR.Id
  LEFT JOIN SEGusuaru AS USU  ON USU.CODUSUARI =DOS.USUARIOREGISTRO 
  WHERE (B.DESSERIPS LIKE '%TELETER%' OR B.DESSERIPS LIKE  '%RADIOCIRUGIA%' )
) RADIO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista orientada a reporting que consolida las aplicaciones de radioterapia (teleterapia y radiocirugía) por paciente y sesión. Integra órdenes médicas, esquemas de tratamiento, dosis registradas por sesión, diagnóstico CIE-10, entidad aseguradora, centro de atención, profesional ordenante y usuario que aplicó la dosis. Incluye campos de fecha y mes en español para facilitar filtros temporales en herramientas de BI o informes de gestión oncológica.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_RADIOTERAPIA_APLICACIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_RADIOTERAPIA_APLICACIONES';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida las aplicaciones de radioterapia (teleterapia y radiocirugía) por paciente, esquema, sesión y dosis, enriquecida con datos de ingreso, entidad, profesional, especialidad y centro de atención.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_RADIOTERAPIA_APLICACIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes de radioterapia (HCRADORDEN) deben estar vinculadas a un profesional (inprofsal), especialidad (inespecia), pronóstico (HCORDPRON), paciente (INPACIENT), centro de atención (ADCENATEN), CUPS (INCUPSIPS) y diagnóstico (INDIAGNOS) válidos para aparecer en la vista (INNER JOIN).; El servicio CUPS asociado a la orden debe tener descripción que contenga ''TELETER'' o ''RADIOCIRUGIA''.; Los esquemas (HCRADESQUEMAS), ingresos (ADINGRESO), dosis (HCRADDOSIS), entidad de salud (HealthAdministrator), grupo de atención (CareGroup) y usuario (SEGusuaru) son opcionales (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_RADIOTERAPIA_APLICACIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El alcance del reporte está limitado a servicios de teleterapia y radiocirugía (no incluye braquiterapia explícitamente en el filtro, aunque exista el estado 8).; ID_COMPANY siempre corresponde al nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres.; El número de sesión se reinicia por combinación paciente+ingreso, ordenado cronológicamente por fecha de registro de la dosis.; La cantidad por fila siempre es 1 (constante).; ULT_ACTUAL siempre se calcula a la zona horaria ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_RADIOTERAPIA_APLICACIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radioterapia; Teleterapia; Radiocirugía; Braquiterapia; Esquema de radioterapia; Dosis (total, por sesión, por tumor); Sesión de aplicación; Simulación; Planeación; Orden médica; Diagnóstico CIE-10; CUPS; Paciente; Ingreso; Centro de atención; Entidad de salud (HealthAdministrator); Grupo de atención (CareGroup); Profesional ordenante; Especialidad médica', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_RADIOTERAPIA_APLICACIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.VIEW_RADIOTERAPIA_APLICACIONES: Devuelve únicamente registros cuyo CUPS asociado tenga descripción con ''TELETER'' o ''RADIOCIRUGIA'' (WHERE B.DESSERIPS LIKE ''%TELETER%'' OR B.DESSERIPS LIKE ''%RADIOCIRUGIA%'').; [RETURN_RESULT] Report.VIEW_RADIOTERAPIA_APLICACIONES: Calcula el número de sesión por paciente+ingreso (ROW_NUMBER OVER PARTITION BY ORD.IPCODPACI+ESQ.NUMINGRES ORDER BY DOS.FECHAREGISTRO ASC).; [RETURN_RESULT] Report.VIEW_RADIOTERAPIA_APLICACIONES: Expone ULT_ACTUAL como GETDATE() convertido a zona horaria ''Pakistan Standard Time''.; [RETURN_RESULT] Report.VIEW_RADIOTERAPIA_APLICACIONES: Cada fila representa una aplicación de dosis con CANTIDAD fija = 1 para sumarización en reportes.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_RADIOTERAPIA_APLICACIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ORD.ESTADO ∈ {1..8} → Traduce el código numérico a etiqueta: 1=SOLICITADO, 2=SIMULACION PROGRAMADA, 3=PLANEACION PROGRAMADA, 4=PLANEACION CONFIRMADA, 5=FINALIZADO, 6=ANULADO, 7=COMPLETADO, 8=BRAQUITERAPIA INICIADA.; si MONTH(DOS.FECHAREGISTRO) ∈ {1..12} → Traduce el mes numérico a su nombre en español (ENERO..DICIEMBRE) concatenado con el número formateado a dos dígitos.; si B.DESSERIPS LIKE ''%TELETER%'' OR B.DESSERIPS LIKE ''%RADIOCIRUGIA%'' → Incluye la fila en el resultado. else Excluye la fila.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_RADIOTERAPIA_APLICACIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.HCRADORDEN; dbo.inprofsal; dbo.inespecia; dbo.HCORDPRON; dbo.INPACIENT; dbo.ADCENATEN; dbo.INCUPSIPS; DBO.INDIAGNOS; DBO.HCRADESQUEMAS; dbo.ADINGRESO; DBO.HCRADDOSIS; Contract.HealthAdministrator; Contract.CareGroup; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_RADIOTERAPIA_APLICACIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_RADIOTERAPIA_APLICACIONES';
GO
