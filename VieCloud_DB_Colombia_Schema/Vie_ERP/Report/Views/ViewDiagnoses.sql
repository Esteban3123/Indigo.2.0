
/*******************************************************************************************************************
Nombre: [Report].[ViewDiagnoses]
Tipo:Vista
Observacion:Diagnosticos por ingreso
Profesional:Nilsson Miguel Galindo Lopez
Fecha:31-01-2024
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 1
Persona que modifico:
Fecha:
Observaciones:
--------------------------------------------------------------------------------------
Version 2
Persona que modifico:
Fecha:
Ovservaciones:
_______________________________________________________________________________________________________________________

**********************************************************************************************************************/
CREATE VIEW [Report].[ViewDiagnoses]
AS

WITH
CTE_DX_PRINCIPAL AS
(
SELECT
0 AS NUMERO,
CODCENATE,
NUMEFOLIO,
UFUCODIGO,
NUMINGRES,
IPCODPACI,
CODDIAGNO,
DIAINGEGR,
TIPDIAGNO,
CLADIAGNO,
OBSDIAGNO,
FECDIAGNO,
1 AS [DX PRINCIPAL]
FROM
dbo.INDIAGNOP WHERE CODDIAPRI=1
),
CTE_DX_RELACIONADOS AS
(
SELECT
ROW_NUMBER ( )   
OVER (PARTITION BY NUMINGRES ORDER BY FECDIAGNO DESC) AS NUMERO,
CODCENATE,
NUMEFOLIO,
UFUCODIGO,
NUMINGRES,
IPCODPACI,
CODDIAGNO,
DIAINGEGR,
TIPDIAGNO,
CLADIAGNO,
OBSDIAGNO,
FECDIAGNO,
0 AS [DX PRINCIPAL]
FROM
dbo.INDIAGNOP WHERE CODDIAPRI!=1
),
CTE_DX AS
(
SELECT * FROM CTE_DX_PRINCIPAL
UNION ALL
SELECT * FROM CTE_DX_RELACIONADOS
)

SELECT 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
CEN.NOMCENATE AS [CENTRO DE ATENCION],
HEA.HealthEntityCode AS [CODIGO EPS/ENTIDAD TERRITORIAL],
HEA.CODE AS [CODIGO ENTIDAD], 
RTRIM(HEA.NAME) AS ENTIDAD, 
RTRIM(CGR.NAME) AS [GRUPO DE ATENCION],
CASE PAC.IPTIPOPAC WHEN 1 THEN 'CONTRIBUTIVO' 
				   WHEN 2 THEN 'SUBSIDIADO' 
				   WHEN 3 THEN 'VINCULADO' 
				   WHEN 4 THEN 'PARTICULAR' 
				   WHEN 5 THEN 'OTRO' 
				   WHEN 6 THEN 'DESPLAZADO REG. CONTRIBUTIVO' 	 
				   WHEN 7 THEN 'DESPLAZADO REG. SUBSIDIADO' ELSE 'DESPLAZADO NO ASEGURADO' END AS RÉGIMEN,
TIPD.SIGLA AS [TIPO IDENTIFICACIÓN],
PAC.IPCODPACI AS [IDENDTIFICACIÓN],
RTRIM(PAC.IPNOMCOMP) AS [NOMBRE COMPLETO PACIENTE], 
CAST(PAC.IPFECNACI AS DATE) AS [FECHA NACIMIENTO], 
FLOOR((CAST(CONVERT(VARCHAR(8),ING.IFECHAING,112) AS INT) - CAST(CONVERT(VARCHAR(8), PAC.IPFECNACI, 112) AS INT)) / 10000) AS EDAD,
CASE WHEN PAC.IPSEXOPAC = '1' THEN 'M' ELSE 'F' END AS SEXO,
PAC.IPDIRECCI AS DIRECCION,
PAC.IPTELEFON AS [TELEFONO FIJO],
PAC.IPTELMOVI AS [TELEFONO MOVIL],
CASE PAC.CODGRUPOE WHEN '000' THEN 'OTRO'
				   WHEN '001' THEN 'INDIGENAS'
				   WHEN '002' THEN 'AFROCOLOMBIANOS NEGROS MULATOS O AFRODESCENDIENTES'
				   WHEN '003' THEN 'RAIZALES SAN ANDRES Y PROVIDENCIA'
				   WHEN '004' THEN 'PUEBLO ROM GITANOS'
				   WHEN '999' THEN 'NO SABE  NO INFORMA  NO APLICA' ELSE 'NO REGISTRA' END AS [ETNIA PACIENTE],
FUN.UFUDESCRI AS [UNIDAD ASIGNACIÓN DX],
DX.NUMINGRES AS INGRESO,
DX.NUMEFOLIO AS FOLIO,
DX.CODDIAGNO AS [CODIGO DIAGNOSTICO],
DIA.NOMDIAGNO AS DIAGNOSTICO,
CASE DX.[DX PRINCIPAL] WHEN 1 THEN 'SI'
					   WHEN 0 THEN 'NO' END AS [DX PRINCIPAL],
CASE DX.DIAINGEGR WHEN 'I' THEN 'Ingreso'
				  WHEN 'E' THEN 'Egreso'
				  WHEN 'A' THEN 'Ambos' END AS [DX EGRESO O INGRESO],
CASE DX.TIPDIAGNO WHEN 'I' THEN 'Impresion Diagnostica'
				  WHEN 'C' THEN 'Confirmado Nuevo'
				  WHEN 'R' THEN 'Confirmado Repetido' END AS [TIPO DE DX],
CASE DX.CLADIAGNO WHEN 'PR' THEN 'Pre-operatorio'
				  WHEN 'PO' THEN 'Pos-operatorio'
				  WHEN 'PP' THEN 'Pre y Pos-Operatorio'
				  WHEN 'HI' THEN 'Hispatologico'
				  WHEN 'NA' THEN 'No Aplica' END AS [CLASE DE DX],
DX.OBSDIAGNO AS OBSERVACIÓN,
DX.FECDIAGNO AS [FECHA DX],
 1 as 'CANTIDAD',
 CAST(DX.FECDIAGNO AS date) AS 'FECHA BUSQUEDA',
 YEAR(DX.FECDIAGNO) AS 'AÑO BUSQUEDA',
 MONTH(DX.FECDIAGNO) AS 'MES BUSQUEDA',
 CONCAT(FORMAT(MONTH(DX.FECDIAGNO), '00') ,' - ', 
	   CASE MONTH(DX.FECDIAGNO) 
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
FROM 
CTE_DX AS DX
INNER JOIN dbo.ADINGRESO AS ING ON DX.NUMINGRES=ING.NUMINGRES
INNER JOIN dbo.INPACIENT PAC ON ING.IPCODPACI=PAC.IPCODPACI
INNER JOIN dbo.ADTIPOIDENTIFICA TIPD ON PAC.IPTIPODOC=TIPD.CODIGO
INNER JOIN CONTRACT.HEALTHADMINISTRATOR HEA ON ING.GENCONENTITY = HEA.ID
INNER JOIN CONTRACT.CAREGROUP AS CGR ON ING.GENCAREGROUP = CGR.ID
INNER JOIN ADCENATEN AS CEN ON DX.CODCENATE=CEN.CODCENATE
INNER JOIN dbo.INUNIFUNC FUN ON DX.UFUCODIGO=FUN.UFUCODIGO
LEFT JOIN dbo.INDIAGNOS DIA ON DX.CODDIAGNO=DIA.CODDIAGNO
--WHERE ING.NUMINGRES='3E0C520EF5'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a análisis de diagnósticos clínicos por ingreso hospitalario. Combina diagnósticos principales (CODDIAPRI=1) y relacionados desde INDIAGNOP, enriquecidos con datos demográficos del paciente, tipo de identificación, régimen de afiliación, entidad aseguradora, grupo de atención, unidad funcional y centro de atención. Expone clasificaciones decodificadas del diagnóstico (tipo, clase, momento ingreso/egreso), edad calculada al ingreso, etnia y campos de fecha desagregados por año y mes para facilitar filtros temporales en herramientas de Business Intelligence.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewDiagnoses';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewDiagnoses';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los diagnósticos por ingreso (principal y relacionados) enriquecidos con datos del paciente, entidad responsable de pago, grupo de atención, unidad funcional y descripciones del catálogo CIE-10 para fines de reportería.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewDiagnoses';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada ingreso (NUMINGRES) debe existir en ADINGRESO y referenciar un paciente válido en INPACIENT, una entidad en CONTRACT.HEALTHADMINISTRATOR y un grupo de atención en CONTRACT.CAREGROUP (INNER JOIN).; El paciente debe tener tipo de documento registrado en ADTIPOIDENTIFICA.; El diagnóstico debe estar asociado a un centro de atención (ADCENATEN) y a una unidad funcional (INUNIFUNC) válidos.; INDIAGNOP debe distinguir el diagnóstico principal mediante CODDIAPRI=1.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewDiagnoses';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La edad se calcula como diferencia de años entre la fecha de ingreso (ING.IFECHAING) y la fecha de nacimiento (PAC.IPFECNACI) usando aritmética sobre formato YYYYMMDD, garantizando años cumplidos.; Cada fila incorpora ID_COMPANY = nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres.; La marca temporal ULT_ACTUAL se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; Sólo los diagnósticos con CODDIAPRI=1 reciben la marca de diagnóstico principal; el resto se ordena por fecha de diagnóstico descendente dentro de cada ingreso.; El catálogo de diagnósticos (INDIAGNOS) se une por LEFT JOIN, por lo que un código no catalogado no excluye la fila pero deja DIAGNOSTICO en NULL.; Cada fila aporta CANTIDAD=1 para conteos directos en reportes.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewDiagnoses';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Diagnóstico principal; Diagnósticos relacionados; Ingreso/admisión del paciente; Régimen de afiliación (contributivo, subsidiado, vinculado, particular, desplazado); Etnia del paciente; Tipo de identificación; Centro de atención; Unidad funcional; Entidad responsable de pago (EPS/Entidad Territorial); Grupo de atención; Clasificación de diagnóstico (ingreso/egreso, impresión/confirmado, pre/pos-operatorio); CIE-10', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewDiagnoses';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewDiagnoses: Devuelve una fila por cada diagnóstico registrado en INDIAGNOP, marcando ''DX PRINCIPAL''=''SI'' cuando CODDIAPRI=1 y ''NO'' en caso contrario.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewDiagnoses';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si INDIAGNOP.CODDIAPRI = 1 → Se clasifica como diagnóstico principal (NUMERO=0, [DX PRINCIPAL]=1). else Se clasifica como diagnóstico relacionado y se numera con ROW_NUMBER particionado por NUMINGRES ordenado por FECDIAGNO DESC ([DX PRINCIPAL]=0).; si PAC.IPTIPOPAC entre 1 y 7 → Se traduce a régimen: 1=CONTRIBUTIVO, 2=SUBSIDIADO, 3=VINCULADO, 4=PARTICULAR, 5=OTRO, 6=DESPLAZADO REG. CONTRIBUTIVO, 7=DESPLAZADO REG. SUBSIDIADO. else Cualquier otro valor se clasifica como ''DESPLAZADO NO ASEGURADO''.; si PAC.CODGRUPOE en (''000'',''001'',''002'',''003'',''004'',''999'') → Mapea a etnia: 000=OTRO, 001=INDIGENAS, 002=AFROCOLOMBIANOS NEGROS MULATOS O AFRODESCENDIENTES, 003=RAIZALES SAN ANDRES Y PROVIDENCIA, 004=PUEBLO ROM GITANOS, 999=NO SABE NO INFORMA NO APLICA. else Otros valores se reportan como ''NO REGISTRA''.; si DX.DIAINGEGR → Traduce I=Ingreso, E=Egreso, A=Ambos.; si DX.TIPDIAGNO → Traduce I=Impresion Diagnostica, C=Confirmado Nuevo, R=Confirmado Repetido.; si DX.CLADIAGNO → Traduce PR=Pre-operatorio, PO=Pos-operatorio, PP=Pre y Pos-Operatorio, HI=Hispatologico, NA=No Aplica.; si PAC.IPSEXOPAC = ''1'' → SEXO=''M''. else SEXO=''F''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewDiagnoses';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INDIAGNOP; dbo.ADINGRESO; dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; CONTRACT.HEALTHADMINISTRATOR; CONTRACT.CAREGROUP; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewDiagnoses';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewDiagnoses';
GO
