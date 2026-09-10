

/*******************************************************************************************************************
Nombre: [Report].[ChildbirthCare]
Tipo:Vista
Observacion:Historia clinica de atención al parto.
Profesional: Nilsson Miguel Galindo Lopez
Fecha:15-08-2023
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 2
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha: 9-11-2023
Observaciones:Se agrga el campo de numero de cesareas. 
--------------------------------------
Version 3
Persona que modifico:
Observacion:
Fecha:
***********************************************************************************************************************************/

CREATE VIEW [Report].[ViewChildbirthCare] AS

WITH
CTE_MORTALIDAD AS
(
SELECT NUMINGRES FROM DBO.HCHISPACA WHERE INDICAPAC=11
),
CTE_DISPENSACION AS 
(
SELECT
DIS.AdmissionNumber,
CASE WHEN PRO.Name LIKE '%micronizado%' THEN 1
	 WHEN PRO.Name LIKE '%intrauterino%'THEN 2 
	 WHEN PRO.Name LIKE '%DIU%' THEN 3 
	 WHEN PRO.Name LIKE '%condones%' THEN 4
	 WHEN PRO.Name LIKE '%T DE COBRE%' THEN 5 
	 WHEN PRO.Name LIKE '%PRESERVATIVOS%' THEN 6 END AS TIPO,
PRO.Name
FROM 
Inventory.PharmaceuticalDispensing DIS INNER JOIN
Inventory.PharmaceuticalDispensingDetail DISD ON DIS.ID=DISD.PharmaceuticalDispensingId and STATUS=2 INNER JOIN
Inventory.InventoryProduct PRO ON DISD.ProductId=PRO.ID
WHERE PRO.Name LIKE '%micronizado%' OR PRO.Name LIKE '%intrauterino%' OR PRO.Name LIKE '%DIU%' OR PRO.Name LIKE '%condones%' OR 
PRO.Name LIKE '%T DE COBRE%' OR PRO.Name LIKE '%PRESERVATIVOS%'
),

CTE_INFORME_QX AS
(
SELECT NUMINGRES FROM dbo.HCQXINFOR WHERE CODSERIPS='740001'
),

CTE_ANTECEDENTES AS
(
SELECT
A.IPCODPACI,
A.NUMCESARE
FROM dbo.HCANTGINE A
WHERE A.NUMEFOLIO=(SELECT MAX(B.NUMEFOLIO) FROM dbo.HCANTGINE B WHERE A.IPCODPACI=B.IPCODPACI AND B.NUMCESARE!=0)
)

SELECT
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
CEN.NOMCENATE AS [CENTRO DE ATENCION],
CASE PAC.IPTIPODOC WHEN 1 THEN 'CC - CEDULA DE CIUDADANIA' 
				   WHEN 2 THEN 'CE - CEDULA DE EXTRANJERIA' 
				   WHEN 3 THEN 'TI - TARJETA DE IDENTIDAD' 
				   WHEN 4 THEN 'RC - REGISTRO CIVIL' 
				   WHEN 5 THEN 'PA - PASAPORTE' 
				   WHEN 6 THEN 'AS - ADULTO SIN IDENTIFICACION' 
				   WHEN 7 THEN 'MS - MENOR SIN IDENTIFICACION' 
				   WHEN 8 THEN 'NU - NUMERO UNICO DE IDENTIFICACIÒN' 
				   WHEN 9 THEN 'NV - CERTIFICADO NACIDO VIVO' 
				   WHEN 10 THEN 'CD - CARNET DIPLOMATICO' 
				   WHEN 11 THEN 'SC - SALVOCONDUCTO' 
				   WHEN 12 THEN 'PE - PERMISO ESPECIAL DE PERMANENCIA'
				   WHEN 13 THEN 'PT - PERMISO POR PROTECCIÓN TEMPORAL'
				   WHEN 14 THEN 'DE - DOCUMENTO EXTRANJERO'
				   WHEN 15 THEN 'SI - SIN IDENTIFICACION' ELSE 'OTRO'END [TIPO IDENTIFICACION],
PAC.IPCODPACI AS [# IDENTIFICACIÓN],
PAC.IPNOMCOMP AS [NOMBRE DEL PACIENTE],
PAC.IPPRINOMB AS [PRIMER NOMBRE],
PAC.IPSEGNOMB AS [SEGUNDO NOMBRE],
PAC.IPPRIAPEL AS [PRIMER APELLIDO],
PAC.IPSEGAPEL AS [SEGUNDO APELLIDO],
CAST(PAC.IPFECNACI AS DATE) AS [FECHA DE NACIMIENTO],
DATEDIFF(YEAR,PAC.IPFECNACI,ING.IFECHAING) AS EDAD,
GRO.Name AS [GRUPO DE ATENCIÓN],
HA.Code+' - '+HA.Name AS ENTIDAD,
ING.NUMINGRES AS INGRESO,
PAR.NUMEFOLIO AS FOLIO,
FUN.UFUDESCRI AS [UNIDAD FUNCIONAL],
PRO.NOMMEDICO AS MEDICO,
ESP.DESESPECI AS ESPECIALIDAD,
PAR.FECINIATE AS [FECHA INICIAL ATENCIÓN],
PAR.INITRAPAR AS [INICIO TRABAJO DE PARTO],
IIF(QX.NUMINGRES IS NULL,'NO','SI')AS [INFORME QX],
PAR.TERTRAPAR AS [TERMINA TRABAJO DE PARTO],
PAR.PRESENTAC AS PRESENTACION,
PAR.NUMEROFET AS [NUMERO DE FETOS],
PAR.TIERUPMEM AS [TIEMPO RUPTURA MEMBRANAS],
PAR.DESLIQAMN AS [LIQUIDO AMNIOTICO],
PAR.PREDESGAR AS DESGARRO,
DX.CODDIAGNO AS [CODIGO DX],
DX.NOMDIAGNO AS [DIAGNOSTICO PRINCIPAL],
IIF(FA.NUMINGRES IS NULL,'NO','SI') AS FALLECIDO,
DIS.Name AS [ANTICONCEPTIVO POST PARTO],
ISNULL(ANT.NUMCESARE,0) AS [NUMERO DE CESAREAS],
1 as 'CANTIDAD',
CAST(PAR.FECINIATE AS date) AS 'FECHA BUSQUEDA',
YEAR(PAR.FECINIATE) AS 'AÑO FECHA BUSQUEDA',
MONTH(PAR.FECINIATE) AS 'MES FECHA BUSQUEDA',
CASE MONTH(PAR.FECINIATE) WHEN 1 THEN 'ENERO'
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
						  WHEN 12 THEN 'DICIEMBRE' END AS 'MES NOMBRE FECHA BUSQUEDA', 
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
dbo.HCATINPAR PAR 
INNER JOIN dbo.ADINGRESO ING ON PAR.NUMINGRES=ING.NUMINGRES
INNER JOIN dbo.ADCENATEN CEN ON ING.CODCENATE=CEN.CODCENATE
INNER JOIN dbo.INPACIENT PAC ON PAR.IPCODPACI=PAC.IPCODPACI
INNER JOIN Contract.CareGroup GRO ON ING.GENCAREGROUP=GRO.Id
INNER JOIN Contract.HealthAdministrator HA ON ING.GENCONENTITY=HA.Id
INNER JOIN dbo.INUNIFUNC FUN ON PAR.UFUCODIGO=FUN.UFUCODIGO
INNER JOIN dbo.INPROFSAL PRO ON PAR.CODPROSAL=PRO.CODPROSAL
INNER JOIN dbo.INESPECIA ESP ON PRO.CODESPEC1=ESP.CODESPECI
INNER JOIN dbo.INDIAGNOP DIA ON PAR.NUMINGRES=DIA.NUMINGRES AND DIA.CODDIAPRI=1 AND DIA.DIAESTADO=1
INNER JOIN dbo.INDIAGNOS DX ON DIA.CODDIAGNO=DX.CODDIAGNO
LEFT JOIN CTE_MORTALIDAD FA ON PAR.NUMINGRES=FA.NUMINGRES
LEFT JOIN CTE_DISPENSACION DIS ON PAR.NUMINGRES=AdmissionNumber
LEFT JOIN CTE_INFORME_QX QX ON PAR.NUMINGRES=QX.NUMINGRES
LEFT JOIN CTE_ANTECEDENTES ANT ON PAR.IPCODPACI=ANT.IPCODPACI
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a consolidar la historia clínica de atención al parto por ingreso hospitalario. Integra datos demográficos del paciente, información del episodio de ingreso (entidad aseguradora, grupo de atención, unidad funcional, médico y especialidad), evolución clínica del parto (trabajo de parto, presentación, líquido amniótico, desgarros, número de fetos) y antecedentes ginecológicos (número de cesáreas previas). Complementa cada registro con indicadores derivados: si hubo informe quirúrgico con código CUPS 740001 (cesárea), si la paciente falleció durante el ingreso, y qué método anticonceptivo postparto fue dispensado (DIU, preservativos, T de cobre, entre otros). Está diseñada para consumo en herramientas de inteligencia de negocios o reportes institucionales de salud materna.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewChildbirthCare';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewChildbirthCare';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la historia clínica de atención al parto con datos del paciente, ingreso, profesional, diagnóstico principal, anticonceptivo dispensado, antecedentes de cesáreas y marcadores de mortalidad e informe quirúrgico, para fines de reporte.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewChildbirthCare';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe tener un diagnóstico principal activo (INDIAGNOP.CODDIAPRI=1 AND DIAESTADO=1).; El paciente debe estar registrado en INPACIENT y el ingreso en ADINGRESO con grupo de atención y administradora de salud asociados.; El profesional debe tener una especialidad principal (CODESPEC1) válida en INESPECIA.; Existe un registro de atención de parto en HCATINPAR para que la fila aparezca.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewChildbirthCare';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera dispensaciones con STATUS=2 en PharmaceuticalDispensingDetail.; Solo considera diagnósticos primarios activos (CODDIAPRI=1 AND DIAESTADO=1).; El informe QX se reconoce únicamente por el código de servicio IPS ''740001''.; La mortalidad se identifica por INDICAPAC=11 en HCHISPACA.; La marca temporal ULT_ACTUAL se calcula con la zona horaria ''Pakistan Standard Time''.; ID_COMPANY se construye a partir de DB_NAME() truncado a 9 caracteres.; EDAD se calcula como diferencia de años entre fecha de nacimiento y fecha de ingreso (DATEDIFF YEAR), sin ajustar por mes/día.; El número de cesáreas previas proviene del folio máximo (más reciente) con NUMCESARE distinto de cero; si no existe antecedente, se reporta 0.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewChildbirthCare';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Atención al parto; Trabajo de parto; Anticonceptivo post parto (DIU, T de cobre, condones, preservativos, micronizado, intrauterino); Antecedentes ginecológicos / cesáreas previas; Mortalidad del paciente; Informe quirúrgico; Diagnóstico principal CIE-10; Centro de atención / Unidad funcional; Grupo de atención y Administradora de salud (EPS); Tipo de documento de identificación; Especialidad médica / Profesional de la salud; Líquido amniótico, presentación fetal, ruptura de membranas, desgarro, número de fetos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewChildbirthCare';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewChildbirthCare: Devuelve una fila por registro de atención al parto (HCATINPAR) cruzado con su ingreso, paciente, profesional, diagnóstico principal y datos opcionales de mortalidad, dispensación de anticonceptivos, informe quirúrgico y antecedentes de cesáreas.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewChildbirthCare';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PRO.Name LIKE ''%micronizado%'' / ''%intrauterino%'' / ''%DIU%'' / ''%condones%'' / ''%T DE COBRE%'' / ''%PRESERVATIVOS%'' en CTE_DISPENSACION → Clasifica el producto dispensado como anticonceptivo post parto (TIPO 1..6) y lo asocia al ingreso vía AdmissionNumber. else Se excluye del CTE (no se considera anticonceptivo).; si Existe NUMINGRES en HCHISPACA con INDICAPAC=11 (CTE_MORTALIDAD) → Marca el campo FALLECIDO=''SI''. else FALLECIDO=''NO''.; si Existe NUMINGRES en HCQXINFOR con CODSERIPS=''740001'' (CTE_INFORME_QX) → Marca [INFORME QX]=''SI''. else [INFORME QX]=''NO''.; si Para CTE_ANTECEDENTES: se selecciona el folio máximo de HCANTGINE por paciente con NUMCESARE!=0 → Se trae el número de cesáreas previas del antecedente más reciente. else Si no hay antecedente con cesáreas, [NUMERO DE CESAREAS]=0 (ISNULL).; si PAC.IPTIPODOC IN (1..15) → Traduce el código numérico al texto del tipo de documento (CC, CE, TI, RC, PA, etc.). else Devuelve ''OTRO''.; si MONTH(PAR.FECINIATE) IN (1..12) → Traduce el número de mes a su nombre en español (''ENERO''..''DICIEMBRE'').', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewChildbirthCare';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Inventory.InventoryProduct; dbo.HCQXINFOR; dbo.HCANTGINE; dbo.HCATINPAR; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INPACIENT; Contract.CareGroup; Contract.HealthAdministrator; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOP; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewChildbirthCare';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewChildbirthCare';
GO
