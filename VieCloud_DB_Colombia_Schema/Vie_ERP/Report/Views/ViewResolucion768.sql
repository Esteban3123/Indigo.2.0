

    /*******************************************************************************************************************
Nombre: [Report].[ViewResolucion768]
Tipo:Vista
Observacion:Vista que trae resolución 768(estancias mayores a 6 horas de hospitalización.)
Profesional: Nilsson Miguel Galindo Lopez
Fecha:21-09-2022
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Vercion 2
Persona que modifico:
Fecha:
Ovservaciones:
--------------------------------------
Vercion 3
Persona que modifico:
Fecha:
***********************************************************************************************************************************/
CREATE VIEW [Report].[ViewResolucion768] AS

WITH

CTE_ESTANCIAS AS
(
--- select de estancias que no sean de observacion ni de uci ni de cx
SELECT
CEN.CODIPSSEC AS [CODIGO REPS],
CASE P.IPTIPODOC WHEN 1 THEN 'CC'
				 WHEN 2 THEN 'CE'
				 WHEN 3 THEN 'TI'
				 WHEN 4 THEN 'RC' 
				 WHEN 5 THEN 'PA'
				 WHEN 6 THEN 'AS' 
				 WHEN 7 THEN 'MS'
				 WHEN 8 THEN 'NU'
				 when 9 then 'CN'
				 when 10 then 'CD'
				 when 11 then 'SC'
				 when 12 then 'PE' 
				 WHEN 13 THEN 'PT' 
				 WHEN 14 THEN 'DE' END AS [TIPO DE IDENTIFICACION],
A.IPCODPACI AS [IDENTIFICACION],
P.IPPRIAPEL AS [PRIMER APELLIDO],
P.IPSEGAPEL AS [SEGUNDO APELLIDO],
P.IPPRINOMB AS [PRIMER NOMBRE],
P.IPSEGNOMB AS [SEGUNDO NOMBRE],
A.NUMINGRES AS [INGRESO],
CASE A.FECFINEST WHEN '1989-01-01 00:00:00.000' THEN DATEDIFF(HOUR,A.FECINIEST,A.FECFINEST) 
	 ELSE DATEDIFF(HOUR,A.FECINIEST,getdate())  END AS [HORAS DE ESTANCIA],
D.UFUDESCRI AS [UNIDAD FUNCIONAL],
ING.IFECHAING
FROM 
DBO.CHREGESTA A INNER JOIN 
dbo.CHCAMASHO B ON A.CODICAMAS=B.CODICAMAS INNER JOIN .
dbo.INUNIFUNC D ON B.UFUCODIGO=D.UFUCODIGO INNER JOIN 
dbo.INPACIENT P ON A.IPCODPACI=P.IPCODPACI INNER JOIN
dbo.ADINGRESO ING ON A.NUMINGRES=ING.NUMINGRES AND ING.FECHEGRESO IS NULL INNER JOIN
dbo.ADCENATEN CEN ON ING.CODCENATE=CEN.CODCENATE
WHERE 
(D.UFUDESCRI NOT LIKE '%UNIDAD DE CUIDADO INTENSIVO%' AND D.UFUDESCRI NOT LIKE '%CIRUGIA%' AND D.UFUDESCRI NOT LIKE '%GINECOLOGIA%' AND
D.UFUDESCRI NOT LIKE '%OBSERVACION%' AND D.UFUDESCRI NOT LIKE '%EXTERNA%')

UNION ALL
----select con unidad funcional de observacion igual o mayor a 6 horas-----------------------------------------------------------------
SELECT
CEN.CODIPSSEC AS [CODIGO REPS],
CASE P.IPTIPODOC WHEN 1 THEN 'CC'
				 WHEN 2 THEN 'CE'
				 WHEN 3 THEN 'TI'
				 WHEN 4 THEN 'RC' 
				 WHEN 5 THEN 'PA'
				 WHEN 6 THEN 'AS' 
				 WHEN 7 THEN 'MS'
				 WHEN 8 THEN 'NU'
				 when 9 then 'CN'
				 when 10 then 'CD'
				 when 11 then 'SC'
				 when 12 then 'PE' 
				 WHEN 13 THEN 'PT'
				 WHEN 14 THEN 'DE' END AS [TIPO DE IDENTIFICACION],
A.IPCODPACI AS [IDENTIFICACION],
P.IPPRIAPEL AS [PRIMER APELLIDO],
P.IPSEGAPEL AS [SEGUNDO APELLIDO],
P.IPPRINOMB AS [PRIMER NOMBRE],
P.IPSEGNOMB AS [SEGUNDO NOMBRE],
A.NUMINGRES AS [INGRESO],
CASE A.FECFINEST WHEN '1989-01-01 00:00:00.000' THEN DATEDIFF(HOUR,A.FECINIEST,A.FECFINEST) 
	 ELSE DATEDIFF(HOUR,A.FECINIEST,getdate())  END AS [HORAS DE ESTANCIA],
D.UFUDESCRI AS [UNIDAD FUNCIONAL],
ING.IFECHAING
FROM 
DBO.CHREGESTA A INNER JOIN 
dbo.CHCAMASHO B ON A.CODICAMAS=B.CODICAMAS INNER JOIN .
dbo.INUNIFUNC D ON B.UFUCODIGO=D.UFUCODIGO INNER JOIN 
dbo.INPACIENT P ON A.IPCODPACI=P.IPCODPACI INNER JOIN
dbo.ADINGRESO ING ON A.NUMINGRES=ING.NUMINGRES AND ING.FECHEGRESO IS NULL INNER JOIN
dbo.ADCENATEN CEN ON ING.CODCENATE=CEN.CODCENATE
WHERE D.UFUDESCRI LIKE '%OBSERVACION%'
)

SELECT
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
[CODIGO REPS],
[TIPO DE IDENTIFICACION],
IDENTIFICACION,
[PRIMER APELLIDO],
[SEGUNDO APELLIDO],
[PRIMER NOMBRE],
[SEGUNDO NOMBRE],
INGRESO,
SUM([HORAS DE ESTANCIA]) AS [HORAS DE ESTANCIA],
1 AS [CANTIDAD],
CAST(IFECHAING AS DATE) AS 'FECHA BUSQUEDA', 
YEAR(IFECHAING) AS 'AÑO FECHA BUSQUEDA', 
MONTH(IFECHAING) AS 'MES AÑO FECHA BUSQUEDA',
CASE MONTH(IFECHAING) WHEN 1  THEN 'ENERO'
					  WHEN 2  THEN 'FEBRERO'
					  WHEN 3  THEN 'MARZO'
					  WHEN 4  THEN 'ABRIL'
					  WHEN 5  THEN 'MAYO'
					  WHEN 6  THEN 'JUNIO'
					  WHEN 7  THEN 'JULIO'
					  WHEN 8  THEN 'AGOSTO' 
					  WHEN 9  THEN 'SEPTIEMBRE'
					  WHEN 10 THEN 'OCTUBRE'
					  WHEN 11 THEN 'NOVIEMBRE'   
					  WHEN 12 THEN 'DICIEMBRE'END AS 'MES NOMBRE FECHA BUSQUEDA', 
DAY(IFECHAING) AS 'DIA FECHA BUSQUEDA',
CONCAT(FORMAT(MONTH(IFECHAING), '00') ,' - ', 
CASE MONTH(IFECHAING) WHEN 1 THEN 'ENERO'
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
					  WHEN 12 THEN 'DICIEMBRE'END) MES_LABEL_BUSQUEDA,
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM CTE_ESTANCIAS
WHERE [HORAS DE ESTANCIA]>=6 --and IDENTIFICACION='1112627561'
GROUP BY [CODIGO REPS],[TIPO DE IDENTIFICACION],IDENTIFICACION,[PRIMER APELLIDO],[SEGUNDO APELLIDO],[PRIMER NOMBRE],
[SEGUNDO NOMBRE],INGRESO,IFECHAING
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte para la Resolución 768 que consolida estancias hospitalarias activas (sin egreso registrado) con duración igual o superior a 6 horas. Combina mediante UNION ALL las estancias de unidades generales de hospitalización con las de observación, excluyendo UCI, cirugía, ginecología y consulta externa. Agrega las horas de estancia por paciente e ingreso, enriqueciendo el resultado con desgloses temporales de la fecha de ingreso (año, mes, día) para consumo en herramientas de reporting o BI.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion768';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion768';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporte regulatorio Resolución 768: lista pacientes con estancias de hospitalización u observación iguales o mayores a 6 horas, excluyendo UCI, cirugía, ginecología y consulta externa, para ingresos aún no egresados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion768';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe estar activo: ING.FECHEGRESO IS NULL (paciente no egresado).; Debe existir relación válida entre cama (CHCAMASHO), unidad funcional (INUNIFUNC), paciente (INPACIENT), ingreso (ADINGRESO) y centro de atención (ADCENATEN).; Las horas de estancia acumuladas por paciente/ingreso deben ser >= 6.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion768';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan pacientes con ingreso activo (sin fecha de egreso).; El umbral regulatorio aplicado es estancia >= 6 horas, conforme Resolución 768.; Las unidades de UCI, cirugía, ginecología y consulta externa nunca aparecen en el reporte; observación sí se incluye explícitamente.; ID_COMPANY se deriva de DB_NAME() truncado a VARCHAR(9), identificando la base/empresa origen.; ULT_ACTUAL se entrega en zona horaria ''Pakistan Standard Time''.; La columna CANTIDAD siempre es 1 (conteo unitario por fila agrupada).; La fecha ''1989-01-01'' se usa como centinela para indicar estancia sin fecha de fin real.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion768';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Resolución 768; estancia hospitalaria; observación; unidad funcional; ingreso/admisión; cama hospitalaria; paciente; tipo de documento de identificación; código REPS; centro de atención', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion768';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewResolucion768: Devuelve una fila por combinación de paciente, ingreso y fecha de ingreso cuya suma de horas de estancia sea >= 6, agrupando por código REPS, identificación, nombres, ingreso y fecha de ingreso.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion768';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si UFUDESCRI NO contiene ''UNIDAD DE CUIDADO INTENSIVO'', ''CIRUGIA'', ''GINECOLOGIA'', ''OBSERVACION'' ni ''EXTERNA'' → Incluye la estancia como hospitalización general en el primer bloque del UNION ALL. else Si la unidad funcional contiene ''OBSERVACION'', se incluye por el segundo bloque del UNION ALL; otras (UCI, cirugía, ginecología, externa) quedan excluidas.; si A.FECFINEST = ''1989-01-01 00:00:00.000'' (fecha centinela de estancia sin cierre) → Calcula horas como DATEDIFF(HOUR, FECINIEST, FECFINEST) — equivale a 0/negativo por la fecha centinela. else Calcula horas como DATEDIFF(HOUR, FECINIEST, GETDATE()) — estancia abierta hasta el momento actual.; si IPTIPODOC ∈ {1..14} → Mapea código numérico a sigla de tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE).; si MONTH(IFECHAING) ∈ {1..12} → Traduce el número de mes a su nombre en español para las columnas de etiqueta.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion768';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGESTA; dbo.CHCAMASHO; dbo.INUNIFUNC; dbo.INPACIENT; dbo.ADINGRESO; dbo.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion768';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolucion768';
GO
