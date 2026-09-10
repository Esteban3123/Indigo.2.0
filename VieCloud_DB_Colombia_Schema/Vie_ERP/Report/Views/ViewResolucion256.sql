

/*******************************************************************************************************************
Nombre: [Report].[ViewResolucion256]
Tipo:Vista
Observacion:Resolución 256
Profesional:Nilsson Miguel Galindo Lopez
Fecha:
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 1
Persona que modifico: Amira Gil Meneses
Fecha: 03-08-2023
Observaciones: Se adiciona condicional para citas cumplidas en el tipo de archivo 2 Oportunidad en Citas.
--------------------------------------------------------------------------------------
Version 2
Persona que modifico:Nilsson Miguel Galindo Lopez
Fecha:24-11-2023
Ovservaciones:Se agrega la tabla ADTIPOIDENTIFICA y se deja el campo de la sigla segun la resolución.
--------------------------------------------------------------------------------------
Version 3
Persona que modifico:Nilsson Miguel Galindo Lopez
Fecha:15-01-2024
Ovservaciones:En el tipo 5 se agrega la tabla dbo.HCURGING1 para sacar la fecha de la atención en consulta y se condiciona que solo sean unidades de urgencias
			  En el tipo 4 se agrega condición de CUPS y se valida si es san jose para que tome la fecha de radicacion
			  
_______________________________________________________________________________________________________________________

**********************************************************************************************************************/

CREATE View [Report].[ViewResolucion256] AS

WITH 

CTE_PACIENTE AS
(
SELECT
PAC.IPCODPACI,
/*IN V2 CASE PAC.IPTIPODOC WHEN '1'  THEN 'CEDULA DE CIUDADANIA'
				 WHEN '2'  THEN 'CEDULA DE EXTRANJERIA'
				 WHEN '3'  THEN 'TARJETA DE IDENTIDAD'
				 WHEN '4'  THEN 'REGISTRO CIVIL' 
				 WHEN '5'  THEN 'PASAPORTE'
				 WHEN '6'  THEN 'ADULTO SIN IDENTIFICACION'
				 WHEN '7'  THEN 'MENOR SIN IDENTIFICACION'
				 WHEN '8'  THEN 'NUMERO UNICO DE IDENTIFICACIÒN'
				 WHEN '9'  THEN 'CERTIFICADO NACIDO VIVO'
				 WHEN '10' THEN 'CARNET DIPLOMATICO'
				 WHEN '11' THEN 'SALVOCONDUCTO'
				 WHEN '12' THEN 'PERMISO ESPECIAL DE PERMANENCIA' END AS [2], FN V2*/
DOC.SIGLA AS [2],
PAC.IPCODPACI AS [3],
CAST(PAC.IPFECNACI AS DATE) AS [4],
CASE PAC.IPSEXOPAC WHEN 1 THEN 'H'
				   WHEN 2 THEN 'F' ELSE 'I' END AS [5],
PAC.IPPRIAPEL AS [6],
PAC.IPSEGAPEL AS [7],
PAC.IPPRINOMB AS [8],
PAC.IPSEGNOMB AS [9],
ENT.HealthEntityCode AS [10],
UBI.DEPMUNCOD AS [11]
FROM
DBO.INPACIENT PAC INNER JOIN
Contract.HealthAdministrator ENT ON PAC.CODENTIDA=ENT.Code INNER JOIN
--DBO.ADTIPOIDENTIFICA TID ON PAC.IPTIPODOC=TID.CODIGO LEFT JOIN
dbo.INUBICACI UBI ON PAC.AUUBICACI=UBI.AUUBICACI
INNER JOIN dbo.ADTIPOIDENTIFICA DOC ON PAC.IPTIPODOC=DOC.CODIGO
),

CTE_INFORME_QX AS
(
SELECT NUMINGRES FROM DBO.HCQXINFOR GROUP BY NUMINGRES
),
CTE_REPROGRAMACION AS
(
SELECT 
QX1.NUMINGRES,QX1.CODSERIPS
FROM dbo.AGEPROGQX QX1 INNER JOIN 
dbo.AGEPROGQX QX2 ON QX1.IPCODPACI=QX2.IPCODPACI AND QX1.CODESTPQX=6 AND QX2.CODESTPQX!=6 AND QX1.CODSERIPS=QX2.CODSERIPS
GROUP BY QX1.NUMINGRES,QX1.CODSERIPS
)

SELECT Distinct
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
'2' AS [0],
ROW_NUMBER ( )   
OVER (order by AGS.FECHORAIN DESC) [1],
PAC.[2],
PAC.[3],
PAC.[4],
PAC.[5],
PAC.[6],
PAC.[7],
PAC.[8],
PAC.[9],
PAC.[10],
CASE ACT.CODSERIPS WHEN '890201' THEN '1'
				   WHEN '890203' THEN '2'
				   WHEN '890266' THEN '3' 
				   WHEN '890283' THEN '4'
				   WHEN '890235' THEN '7' ELSE
	CASE WHEN ACT.DESACTMED='CONSULTA DE PRIMERA VEZ POR ESPECIALISTA EN GINECOLOGA' AND ACT.CODSERIPS='890250' THEN '5'
		 WHEN ACT.DESACTMED='CONSULTA DE PRIMERA VEZ POR ESPECIALISTA EN OBSTETRICIA' AND ACT.CODSERIPS='890250' THEN '6'
		 WHEN ACT.DESACTMED='CONSULTA DE PRIMERA VEZ POR ESPECIALISTA EN GINECOLOGA Y OBSTETRICIA 15MIN' AND ACT.CODSERIPS='890250' THEN '5' ELSE
	CASE WHEN AGS.CODSERIPS BETWEEN '881112' AND '882841' THEN '8' END END END AS [11],
CONVERT(VARCHAR(10),CAST(AGS.FECREGSIS as date),23) as [12],
'1'as [13],
CONVERT(VARCHAR(10),CAST(AGS.FECHAOFERTADA as date),23) as [14],
CONVERT(VARCHAR(10),CAST(AGS.FECITADES as date),23) as [15],
'' AS [16],
'' AS [17],
1 CANTIDAD,
CAST(AGS.FECREGSIS as date) [FECHA BUSQUEDA],
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM
DBO.AGASICITA AGS INNER JOIN 
dbo.AGACTIMED ACT ON AGS.CODACTMED = ACT.CODACTMED AND ACT.ACTIVICON!='1' 
												   INNER JOIN
CTE_PACIENTE PAC ON AGS.IPCODPACI=PAC.IPCODPACI
WHERE 
((AGS.CODSERIPS BETWEEN '881112' AND '882841') OR 
(ACT.CODSERIPS IN ('890201','890203','890266','890283','890235','890250') AND AGS.CODTIPCIT=0) AND AGS.CODESTCIT=1)

UNION ALL

SELECT
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
'4' AS [0],
ROW_NUMBER ( )   
OVER (order by AGX.FECHORAIN DESC) [1],
PAC.[2],
PAC.[3],
PAC.[4],
PAC.[5],
PAC.[6],
PAC.[7],
PAC.[8],
PAC.[9],
PAC.[10],
CONVERT(VARCHAR(5),PAC.[11]) AS [11],
CONVERT(VARCHAR(6),AGX.CODSERIPS) AS [12],
IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO040',CONVERT(VARCHAR(10),CAST(RAD.FECHARADIC as date),23)
											 ,CONVERT(VARCHAR(10),CAST(AGX.FECREGSIS as date),23)) AS [13],
CONVERT(VARCHAR(10),CAST(AGX.FECHORAIN as date),23) AS [14],
IIF(AGX.CODESTPQX=6,'2',CASE WHEN INF.NUMINGRES IS NULL THEN '1' ELSE '2' END) AS [15],
IIF(INF.NUMINGRES IS NULL,'',CASE WHEN CAX.DESCAUCAN LIKE '%PACIENTE%' THEN '2'
	 WHEN CAX.DESCAUCAN LIKE '%MANTENIMIENTO%' THEN '1'
	 WHEN CAX.DESCAUCAN LIKE '%ERROR%' THEN '1'
	 WHEN CAX.DESCAUCAN LIKE '%SALA%' THEN '1'
	 WHEN CAX.DESCAUCAN IS NULL THEN '1' END) AS [16],
IIF (INF.NUMINGRES IS NULL,2, CASE WHEN RE.NUMINGRES IS NULL THEN 2 ELSE 1 END) AS [17],
1 CANTIDAD,
CAST(AGX.FECREGSIS as date) [FECHA BUSQUEDA],
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
dbo.AGEPROGQX AGX 
INNER JOIN CTE_PACIENTE PAC ON AGX.IPCODPACI=PAC.IPCODPACI AND AGX.CODESTPQX!='0'
LEFT JOIN dbo.AGCACANQX CAX ON CAX.CODCACANQ=AGX.CODCAUCAN LEFT JOIN
CTE_INFORME_QX INF ON AGX.NUMINGRES=INF.NUMINGRES LEFT JOIN
CTE_REPROGRAMACION RE ON AGX.NUMINGRES=RE.NUMINGRES AND AGX.CODSERIPS=RE.CODSERIPS
LEFT JOIN dbo.ADRADICACIONQX RAD ON AGX.IDRADICACIONQX=RAD.ID
WHERE AGX.CODSERIPS BETWEEN '010101' AND '869700'

UNION ALL

select 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
'6' AS [0],
ROW_NUMBER ( )   
OVER (order by A.CODCONCEC DESC) [1],
PAC.[2],
PAC.[3],
PAC.[4],
PAC.[5],
PAC.[6],
PAC.[7],
PAC.[8],
PAC.[9],
PAC.[10],
CONVERT(VARCHAR(10),CAST(A.TRIAFECHA AS DATE),23) AS [11],
CONVERT(VARCHAR(5),CAST(A.TRIAFECHA AS TIME)) AS [12],
CONVERT(VARCHAR(10),CAST(URG.FECHINIHI AS DATE),23) AS [13],
CONVERT(VARCHAR(5),CAST(URG.FECHINIHI AS TIME)) AS [14],
NULL AS [15],
NULL AS [16],
NULL AS [17],
1 CANTIDAD,
CAST(A.TRIAFECHA as date) [FECHA BUSQUEDA],
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
dbo.ADTRIAGEU A 
INNER JOIN CTE_PACIENTE PAC ON A.IPCODPACI=PAC.IPCODPACI AND A.TRIAGECLA=2 
LEFT JOIN dbo.HCURGING1 URG ON A.NUMINGRES=URG.NUMINGRES AND URG.NUMEFOLIO=(SELECT MIN(CAST(FOL.NUMEFOLIO AS INT)) FROM dbo.HCURGING1 FOL WHERE A.NUMINGRES=FOL.NUMINGRES)
LEFT JOIN dbo.INUNIFUNC FUN ON URG.UFUCODIGO=FUN.UFUCODIGO
WHERE FUN.UFUDESCRI LIKE '%URGENCIAS%' AND A.ID=(SELECT MAX(B.ID) FROM dbo.ADTRIAGEU B WHERE A.TRIANUMER=B.TRIANUMER)

UNION ALL

SELECT
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
'3' AS [0],
1 AS [1],
'NI' AS [2],
CA.CODIPSSEC AS [3],
NULL AS [4],
NULL AS [5],
NULL AS [6],
NULL AS [7],
NULL AS [8],
NULL AS [9],
NULL AS [10],
NULL AS [11],
NULL AS [12],
NULL AS [13],
NULL AS [14],
NULL AS [15],
NULL AS [16],
NULL AS [17],
1 CANTIDAD,
CAST(GETDATE() as date)  AS [FECHA BUSQUEDA],
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL

FROM 
dbo.ADCENATEN CA

Union all

Select 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
'5' AS [0],
1 AS [1],
'NI' AS [2],
CA.CODIPSSEC AS [3],
NULL AS [4],
NULL AS [5],
NULL AS [6],
NULL AS [7],
NULL AS [8],
NULL AS [9],
NULL AS [10],
NULL AS [11],
NULL AS [12],
NULL AS [13],
NULL AS [14],
NULL AS [15],
NULL AS [16],
NULL AS [17],
1 CANTIDAD,
CAST(GETDATE() as date)  AS [FECHA BUSQUEDA],
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM  dbo.ADCENATEN CA
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte que consolida los distintos tipos de archivo exigidos por la Resolución 256 (indicadores de calidad en salud de Colombia): oportunidad en citas ambulatorias (tipo 2), programación quirúrgica (tipo 4), triage de urgencias (tipo 6) y datos de centros de atención (tipos 3 y 5). Para cada registro incluye datos demográficos del paciente, sigla del tipo de documento, entidad administradora, fechas de solicitud y asignación, códigos CUPS y estado de cumplimiento o cancelación.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion256';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion256';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista los archivos planos (tipos 2,3,4,5,6) requeridos por la Resolución 256 de MinSalud, integrando oportunidad en citas, oportunidad en cirugía, triage de urgencias y centros de atención.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion256';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'INPACIENT debe tener relación válida con HealthAdministrator (CODENTIDA), INUBICACI (AUUBICACI) y ADTIPOIDENTIFICA (IPTIPODOC) — los INNER JOIN obligan su existencia; El nombre de base de datos debe poder representarse en VARCHAR(9) (se usa para identificar la sede, p.ej. ''INDIGO040''); Para cirugías reprogramadas debe existir al menos un registro con CODESTPQX=6 y otro con CODESTPQX!=6 con mismo paciente y servicio; Para triage de urgencias debe existir HCURGING1 con NUMEFOLIO numérico convertible a INT', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion256';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila se etiqueta con un tipo de archivo fijo: ''2'' citas, ''3'' centro de atención, ''4'' cirugías, ''5'' centro de atención, ''6'' triage urgencias; El campo ID_COMPANY siempre proviene de DB_NAME() truncado a 9 caracteres; La fecha de última actualización siempre se calcula con GETDATE() convertido a ''Pakistan Standard Time''; En el archivo tipo 2 solo se incluyen citas con CODESTCIT=1 (estado activo) y CODTIPCIT=0 cuando son consultas, o CODSERIPS entre ''881112'' y ''882841'' para procedimientos; En el archivo tipo 4 se excluyen cirugías con CODESTPQX=''0'' (no programadas); En el archivo tipo 4 solo se incluyen CODSERIPS entre ''010101'' y ''869700'' (procedimientos quirúrgicos); En el archivo tipo 6 solo se considera el último triage por TRIANUMER (MAX(ID)) y la primera nota de urgencias (MIN NUMEFOLIO); Los archivos tipo 3 y 5 emiten una fila por cada centro de atención con valores ''NI'' y campos demográficos en NULL; La sigla del tipo de documento se toma de ADTIPOIDENTIFICA en lugar del literal hardcodeado (versión 2); Solo se cuentan triages con clasificación 2', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion256';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Resolución 256 (indicadores de calidad MinSalud Colombia); Oportunidad en citas de consulta especializada; Oportunidad en cirugía programada; Triage de urgencias; Reprogramación quirúrgica; Causa de cancelación quirúrgica; Tipo de documento de identificación; Centros de atención (IPS); Radicación de cirugía', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion256';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ACT.CODSERIPS está en lista fija (''890201'',''890203'',''890266'',''890283'',''890235'') → Asigna tipo de servicio 1,2,3,4 o 7 respectivamente en el campo [11] del archivo tipo 2; si ACT.DESACTMED corresponde a consulta de primera vez por especialista en ginecología/obstetricia y CODSERIPS=''890250'' → Asigna ''5'' (ginecología o gineco-obstetricia) o ''6'' (obstetricia) en el campo [11]; si AGS.CODSERIPS BETWEEN ''881112'' AND ''882841'' → Clasifica como tipo de servicio ''8'' (procedimientos) en el archivo tipo 2; si Archivo tipo 4: CAST(DB_NAME())=''INDIGO040'' (sede San José) → Usa RAD.FECHARADIC como fecha de solicitud [13] else Usa AGX.FECREGSIS como fecha de solicitud [13]; si AGX.CODESTPQX=6 (cirugía cancelada) → Marca estado [15]=''2'' (no realizada) else Si existe informe quirúrgico (INF.NUMINGRES) [15]=''2'' sino ''1''; si Causa de cancelación CAX.DESCAUCAN contiene ''PACIENTE'' → Motivo de no realización [16]=''2'' (atribuible al paciente) else Si contiene ''MANTENIMIENTO'',''ERROR'' o ''SALA'', o es NULL, asigna ''1'' (atribuible a la institución); si Existe informe quirúrgico y existe reprogramación (RE.NUMINGRES not null) → [17]=1 (reprogramada) else [17]=2; si Archivo tipo 6: A.TRIAGECLA=2 y unidad funcional descripción LIKE ''%URGENCIAS%'' → Incluye el triage como evento de oportunidad en urgencias usando la primera nota HCURGING1 (MIN NUMEFOLIO); si PAC.IPSEXOPAC=1 / =2 / otro → Mapea sexo a ''H'' / ''F'' / ''I''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion256';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.INPACIENT; Contract.HealthAdministrator; dbo.INUBICACI; dbo.ADTIPOIDENTIFICA; DBO.HCQXINFOR; dbo.AGEPROGQX; DBO.AGASICITA; dbo.AGACTIMED; dbo.AGCACANQX; dbo.ADRADICACIONQX; dbo.ADTRIAGEU; dbo.HCURGING1; dbo.INUNIFUNC; dbo.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion256';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion256';
GO
