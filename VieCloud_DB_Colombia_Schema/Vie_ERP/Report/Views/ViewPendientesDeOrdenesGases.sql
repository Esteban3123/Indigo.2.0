

CREATE VIEW [Report].[ViewPendientesDeOrdenesGases]
as

WITH CTE_GASES
AS
(
SELECT 'GASES MEDICINALES' EntityName,iif(A.GENSERVICEORDER IS NULL,'SIN ORDEN','CON ORDEN')  'ORDENE DE SERVICIO' ,'HOSPITALARIO'  'AMBITO',E.DESVIAADM 'VIA',
E.CODSERIPS 'CODIGO CUPS',B.DESSERIPS 'PROCEDIMIENTO CUPS',A.NUMINGRES 'INGRESO',A.IPCODPACI 'IDENTIFICACION',RTRIM(i.IPNOMCOMP) 'PACIENTE' ,
A.FECHREGIS AS DIAS,A.LITRXMINUT,A.HORAINICIA,A.HORAFINAL,A.TOTHORAS,A.TOTLITADM,C.NOMMEDICO 'PROFESIONAL',D.UFUDESCRI 'UNIDAD FUNCIONAL',ING.IESTADOIN 'ESTADO INGRESO'

FROM dbo.HCCONOXIG AS A 
INNER JOIN dbo.INPROFSAL AS C ON A.CODPROSAL=C.CODPROSAL 
INNER JOIN dbo.INUNIFUNC AS D ON A.UFUCODIGO=D.UFUCODIGO 
INNER JOIN dbo.HCPARCONO E ON A.CODVIAADM=E.CODVIAADM
inner join dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
inner join  dbo.ADINGRESO AS ING  on ING.NUMINGRES = A.NUMINGRES  
INNER JOIN dbo.INCUPSIPS B with (nolock) ON E.CODSERIPS=B.CODSERIPS 
WHERE A.GENSERVICEORDER IS NULL
union all

SELECT 'GASES MEDICINALES' EntityName,iif(A.GENSERVICEORDER IS NULL,'SIN ORDEN','CON ORDEN')  'ORDENE DE SERVICIO' , 'HOSPITALARIO'  'AMBITO',E.DESVIAADM 'VIA',
E.CODSERIPS 'CODIGO CUPS',B.DESSERIPS 'PROCEDIMIENTO CUPS',
A.NUMINGRES 'INGRESO',A.IPCODPACI 'IDENTIFICACION',RTRIM(PAC.IPNOMCOMP) 'PACIENTE' ,
		
A.FECHREGIS AS DIAS,A.LITRXMINUT,A.HORAINICIA,A.HORAFINAL,A.TOTHORAS,A.TOTLITADM,C.NOMMEDICO 'PROFESIONAL',D.UFUDESCRI 'UNIDAD FUNCIONAL',ING.IESTADOIN 'ESTADO INGRESO'
FROM dbo.HCCONOXIG AS A 
INNER JOIN dbo.INPROFSAL AS C ON A.CODPROSAL=C.CODPROSAL 
INNER JOIN dbo.INUNIFUNC AS D ON A.UFUCODIGO=D.UFUCODIGO 
INNER JOIN dbo.HCPARCONO E ON A.CODVIAADM=E.CODVIAADM
inner join dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
inner join dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
inner join  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO 
INNER JOIN DBO.INPACIENT AS PAC with (nolock) ON PAC.IPCODPACI =A.IPCODPACI
INNER JOIN dbo.INCUPSIPS B with (nolock) ON E.CODSERIPS=B.CODSERIPS 
WHERE A.GENSERVICEORDER IS NULL
),

CTE_ALTA_MEDICA
AS
(
  SELECT EGR.NUMINGRES ,EGR.IPCODPACI ,MAX(FECALTPAC) 'FECHA ALTA' FROM DBO.HCREGEGRE EGR with (nolock) 
  INNER JOIN CTE_GASES AS PEN with (nolock) ON PEN.INGRESO =EGR.NUMINGRES 
  GROUP BY EGR.NUMINGRES ,EGR.IPCODPACI
),

CTE_ESTANCIA_MAYOR
AS
(
  SELECT C.NUMINGRES ,C.IPCODPACI,C.CODICAMAS  ,C.FECINIEST 'FECHA ESTANCIA',C.REGESTADO,C.CODTIPEST,C.ID    FROM dbo.CHREGESTA C with (nolock)
  INNER JOIN CTE_GASES AS PEN ON PEN.INGRESO =C.NUMINGRES 
  INNER JOIN(SELECT C.NUMINGRES ,C.IPCODPACI ,MAX(C.ID ) IDD FROM dbo.CHREGESTA C with (nolock) GROUP BY C.NUMINGRES ,C.IPCODPACI) AS G ON G.IDD =C.ID 
),

CTE_CAMAS
AS
(
     SELECT FUN.UFUDESCRI 'UNIDAD FUNCIONAL' ,C.IPCODPACI 'IDENTIFICACION' ,C.NUMINGRES 'INGRESO',A.NUMCAMHOS 'CAMA',G.DESTIPEST 'TIPO ESTANCIA'  ,C.ID  'ID_ESTANCIA', 
	 A.CODICAMAS 'ID_CAMAS', CEN.NOMCENATE 'CENTRO DE ATENCION',A.CODCLAHAB ,A.CODCLACAM 
	 FROM dbo.CHCAMASHO A with (nolock)
	 INNER JOIN DBO.INUNIFUNC AS FUN with (nolock) ON A.UFUCODIGO =FUN.UFUCODIGO 
	 INNER JOIN CTE_ESTANCIA_MAYOR C with (nolock) ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 1 
     INNER JOIN dbo.CHTIPESTA G with (nolock) ON G.CODTIPEST=C.CODTIPEST 
	 INNER JOIN DBO.ADCENATEN AS CEN with (nolock) ON CEN.CODCENATE =A.CODCENATE 
),

CTE_HISTORIAS_AMBULATORIAS
AS
(
  SELECT HIS.NUMINGRES ,HIS.IPCODPACI ,MAX(FECHISPAC) AS 'FECHA ALTA'    FROM  DBO.HCHISPACA HIS with (nolock)
  INNER JOIN CTE_GASES AS PEN with (nolock) ON PEN.INGRESO =HIS.NUMINGRES 
  WHERE HIS.GENCONEXT =1
  GROUP BY HIS.NUMINGRES ,HIS.IPCODPACI
)

SELECT
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 IMG.*,F.InvoiceNumber 'NRO FACTURA',
 ALT.[FECHA ALTA],
 1 as 'CANTIDAD',
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
 CTE_GASES IMG
 inner join dbo.ADINGRESO as ing with (nolock) ON img.INGRESO =ing.NUMINGRES 
 LEFT JOIN CTE_ALTA_MEDICA AS ALT with (nolock) ON ALT.NUMINGRES =IMG.INGRESO 
 LEFT JOIN Billing .Invoice AS F ON F.AdmissionNumber =IMG.INGRESO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a consolidar los registros de administración de gases medicinales hospitalarios (incluidos recién nacidos) que aún no tienen orden de servicio generada (`GENSERVICEORDER IS NULL`). Cruza sesiones de oxigenoterapia con datos del ingreso, cama actual, unidad funcional, profesional, código CUPS, fecha de alta médica y número de factura, permitiendo identificar prestaciones pendientes de facturar o formalizar en el ciclo de facturación hospitalaria.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesGases';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesGases';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las administraciones de gases medicinales hospitalarios que aún no tienen orden de servicio generada, enriquecidas con datos de paciente, profesional, unidad funcional, ingreso, alta médica y factura asociada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesGases';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en dbo.HCCONOXIG con GENSERVICEORDER IS NULL; Cada registro de gas debe tener vía de administración (HCPARCONO), profesional (INPROFSAL), unidad funcional (INUNIFUNC) y CUPS (INCUPSIPS) válidos para aparecer; Para la rama de recién nacidos, debe existir vínculo en HCINGRESORECNAC y HCRECINAC asociando el ingreso del hijo', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesGases';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone registros de gases medicinales sin orden de servicio (GENSERVICEORDER IS NULL); EntityName siempre es ''GASES MEDICINALES'' y AMBITO siempre ''HOSPITALARIO''; ID_COMPANY se obtiene del nombre de la base de datos vigente truncado a 9 caracteres; ULT_ACTUAL se calcula con la zona horaria ''Pakistan Standard Time''; La fecha de alta corresponde al MAX(FECALTPAC) por ingreso/paciente en HCREGEGRE; La estancia considerada es la de mayor ID por ingreso en CHREGESTA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesGases';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Gases medicinales; Oxigenoterapia; Orden de servicio; Ingreso hospitalario; Paciente; Recién nacido; Alta médica; Estancia hospitalaria; Cama hospitalaria; Unidad funcional; CUPS; Vía de administración; Factura; Profesional de salud', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesGases';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por cada administración de gas (HCCONOXIG) cuyo GENSERVICEORDER IS NULL, marcada como ''SIN ORDEN'' y ámbito ''HOSPITALARIO'', con CANTIDAD=1 y timestamp ULT_ACTUAL en zona horaria ''Pakistan Standard Time''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesGases';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.GENSERVICEORDER IS NULL → Etiqueta el registro como ''SIN ORDEN'' else ''CON ORDEN'' (no se incluye porque el WHERE filtra solo NULL); si UNION ALL entre pacientes regulares y recién nacidos vinculados (HCINGRESORECNAC/HCRECINAC) → Se concatenan ambas poblaciones de pacientes con gases sin orden de servicio; si CHREGESTA.REGESTADO = 1 en CTE_CAMAS → Solo considera la estancia activa para asociar la cama del paciente; si HCHISPACA.GENCONEXT = 1 en CTE_HISTORIAS_AMBULATORIAS → Solo agrupa historias clínicas marcadas como generadoras de consulta externa', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesGases';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCCONOXIG; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.HCPARCONO; dbo.INPACIENT; dbo.ADINGRESO; dbo.INCUPSIPS; dbo.HCINGRESORECNAC; dbo.HCRECINAC; dbo.HCREGEGRE; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.CHTIPESTA; dbo.ADCENATEN; dbo.HCHISPACA; Billing.Invoice', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesGases';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesGases';
GO
