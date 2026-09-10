

CREATE VIEW [Report].[ViewPendientesDeOrdenesTerapias]
as

WITH CTE_TERAPIAS
AS
(
	SELECT	 'TERAPIAS' EntityName,iif(A.GENSERVICEORDER IS NULL,'SIN ORDEN','CON ORDEN')  'ORDENE DE SERVICIO' ,'HOSPITALARIO'  'AMBITO',
    A.CODSERIPS 'CODIGO CUPS',B.DESSERIPS 'PROCEDIMIENTO CUPS',A.NUMINGRES 'INGRESO',A.IPCODPACI 'IDENTIFICACION',RTRIM(i.IPNOMCOMP) 'PACIENTE' ,
	RTRIM(D.UFUDESCRI) AS 'UNIDAD FUNCIONAL',RTRIM(C.NOMMEDICO) AS 'PROFESIONAL',A.FECHISPAC 'FECHA REALIZACION'
	FROM dbo.HCPROCTER A 
	INNER JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	INNER JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	INNER JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
	inner join dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
	where A.GENSERVICEORDER IS NULL
UNION ALL
	SELECT	'TERAPIAS' EntityName,iif(A.GENSERVICEORDER IS NULL,'SIN ORDEN','CON ORDEN')  'ORDENE DE SERVICIO' ,'HOSPITALARIO'  'AMBITO',
    A.CODSERIPS 'CODIGO CUPS',B.DESSERIPS 'PROCEDIMIENTO CUPS',A.NUMINGRES 'INGRESO',A.IPCODPACI 'IDENTIFICACION',RTRIM(i.IPNOMCOMP) 'PACIENTE' ,
	RTRIM(D.UFUDESCRI) AS 'UNIDAD FUNCIONAL',RTRIM(C.NOMMEDICO) AS 'PREOFESIONAL',A.FECHISPAC 'FECHA REALIZACION'
	FROM dbo.HCPROCTER A 
	INNER JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	INNER JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	INNER JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
	INNER JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	INNER JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	INNER JOIN dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO 
	inner join dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI
	where A.GENSERVICEORDER IS NULL
),

CTE_ALTA_MEDICA
AS
(
  SELECT EGR.NUMINGRES ,EGR.IPCODPACI ,MAX(FECALTPAC) 'FECHA ALTA' FROM DBO.HCREGEGRE EGR with (nolock) 
  INNER JOIN CTE_TERAPIAS AS PEN with (nolock) ON PEN.INGRESO =EGR.NUMINGRES 
  GROUP BY EGR.NUMINGRES ,EGR.IPCODPACI
),

CTE_ESTANCIA_MAYOR
AS
(
  SELECT C.NUMINGRES ,C.IPCODPACI,C.CODICAMAS  ,C.FECINIEST 'FECHA ESTANCIA',C.REGESTADO,C.CODTIPEST,C.ID    FROM dbo.CHREGESTA C with (nolock)
  INNER JOIN CTE_TERAPIAS  AS PEN ON PEN.INGRESO =C.NUMINGRES 
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
  INNER JOIN CTE_TERAPIAS AS PEN with (nolock) ON PEN.INGRESO =HIS.NUMINGRES 
  WHERE HIS.GENCONEXT =1
  GROUP BY HIS.NUMINGRES ,HIS.IPCODPACI
)

SELECT 
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 IMG.*, ING.IESTADOIN 'ESTADO',F.InvoiceNumber 'NRO FACTURA',
 ALT.[FECHA ALTA],
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
 CTE_TERAPIAS IMG
 INNER JOIN DBO.ADINGRESO AS ING ON ING.NUMINGRES= IMG.INGRESO
 LEFT JOIN CTE_ALTA_MEDICA AS ALT with (nolock) ON ALT.NUMINGRES =IMG.INGRESO 
 LEFT JOIN Billing .Invoice AS F ON F.AdmissionNumber =IMG.INGRESO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte que consolida procedimientos de terapias (código CUPS) realizados en el ámbito hospitalario que aún no tienen orden de servicio generada (`GENSERVICEORDER IS NULL`), incluyendo casos de recién nacidos vinculados a la madre. Para cada registro expone el paciente, profesional, unidad funcional, cama actual, estado del ingreso, fecha de alta médica y número de factura, permitiendo identificar servicios ejecutados pendientes de formalización administrativa.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesTerapias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesTerapias';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los procedimientos de terapias hospitalarias que aún no tienen orden de servicio generada, enriquecidos con datos del ingreso, paciente, profesional, alta médica y factura asociada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesTerapias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El procedimiento de terapia debe existir en HCPROCTER con GENSERVICEORDER nulo (sin orden de servicio generada); Deben existir referencias válidas en INCUPSIPS (CUPS), INPROFSAL (profesional), INUNIFUNC (unidad funcional) e INPACIENT (paciente); Para la segunda rama del UNION, el ingreso debe estar vinculado como ingreso de hijo en HCINGRESORECNAC y tener registro de recién nacido en HCRECINAC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesTerapias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las filas devueltas corresponden a terapias sin orden de servicio (GENSERVICEORDER IS NULL); El ámbito reportado es siempre ''HOSPITALARIO'' y la entidad es ''TERAPIAS''; En CTE_ESTANCIA_MAYOR se selecciona siempre el último registro de estancia por ingreso/paciente (MAX(ID)); En CTE_ALTA_MEDICA se toma la fecha de alta más reciente (MAX(FECALTPAC)) por ingreso/paciente; La factura se asocia mediante LEFT JOIN, por lo que un ingreso sin factura sigue apareciendo', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesTerapias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Terapias; Orden de servicio; Procedimiento CUPS; Ingreso hospitalario; Paciente; Profesional de la salud; Unidad funcional; Alta médica; Estancia hospitalaria; Cama hospitalaria; Recién nacido; Factura; Historia clínica ambulatoria', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesTerapias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewPendientesDeOrdenesTerapias: Devuelve solo procedimientos de terapia con A.GENSERVICEORDER IS NULL, marcados como ''SIN ORDEN'' y ámbito ''HOSPITALARIO''; [RETURN_RESULT] Report.ViewPendientesDeOrdenesTerapias: Incluye ID_COMPANY = DB_NAME() truncado a 9 caracteres y ULT_ACTUAL como GETDATE() convertido a zona horaria ''Pakistan Standard Time''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesTerapias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.GENSERVICEORDER IS NULL → Marca la fila como ''SIN ORDEN'' else Marca como ''CON ORDEN'' (aunque el WHERE filtra solo NULL, por lo que en la práctica solo se devuelven ''SIN ORDEN''); si UNION ALL entre terapias generales y terapias asociadas a ingresos de recién nacido (vía HCINGRESORECNAC/HCRECINAC) → Permite reportar terapias pendientes tanto de pacientes regulares como de recién nacidos vinculados a la madre; si CHREGESTA con REGESTADO = 1 en CTE_CAMAS → Solo considera estancias activas para asociar la cama actual del paciente; si HCHISPACA con GENCONEXT = 1 en CTE_HISTORIAS_AMBULATORIAS → Solo toma historias ambulatorias marcadas como consulta externa generada', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesTerapias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPROCTER; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.INPACIENT; dbo.HCINGRESORECNAC; dbo.HCRECINAC; dbo.ADINGRESO; dbo.HCREGEGRE; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.CHTIPESTA; dbo.ADCENATEN; dbo.HCHISPACA; Billing.Invoice', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesTerapias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesTerapias';
GO
