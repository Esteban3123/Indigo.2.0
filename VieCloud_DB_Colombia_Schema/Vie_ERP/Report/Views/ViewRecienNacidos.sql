

    /*******************************************************************************************************************
Nombre: [Report].[ViewRecienNacidos]
Tipo:Vista
Observacion:Pacientes recien nacidos
Profesional: Nilsson Miguel Galindo Lopez
Fecha:25-08-2022
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Vercion 1
Persona que modifico: 
Fecha:
Ovservaciones: 
--------------------------------------
Vercion 2
Persona que modifico:
Fecha:
***********************************************************************************************************************************/

CREATE view [Report].[ViewRecienNacidos]
as

WITH 
CTE_INGRESOS_MADRES AS
(
SELECT ING.NUMINGRES,ING.IPCODPACI,ING.IFECHAING,REC.NUMINGRESHIJO
FROM 
ADINGRESO ING INNER JOIN 
dbo.HCINGRESORECNAC REC ON ING.NUMINGRES=REC.NUMINGRES
),

CTE_OXIGENO AS
(
SELECT 
ROW_NUMBER ( )   
OVER ( PARTITION BY IM.NUMINGRESHIJO  order by IM.NUMINGRESHIJO,FIS.FECREGITE) 'NUMERO', FIS.REGSO2PAC,IM.NUMINGRESHIJO,FIS.FECREGITE,fis.DESOJOPAC
FROM
CTE_INGRESOS_MADRES IM INNER JOIN
dbo.HCEXFISIC FIS ON IM.NUMINGRESHIJO=FIS.NUMINGRES
WHERE FIS.REGSO2PAC!=0
),
CTE_OXIGENO1 AS
(
SELECT * FROM CTE_OXIGENO WHERE NUMERO=1
),
CTE_OXIGENO2 AS
(
SELECT * FROM CTE_OXIGENO WHERE NUMERO=2
)

SELECT
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
PAC.IPNOMCOMP AS [NOMBRE MADRE],
PAC.IPCODPACI AS [IDENTIFICACION MADRE],
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
				   WHEN 12 THEN 'PE - PERMISO ESPECIAL DE PERMANENCIA' ELSE 'OTRO' END [TIPO IDENTIFICACION],
CAST(PAC.IPFECNACI AS DATE ) AS [FECHA_NACIMIENTO MADRE],
FLOOR((CAST(CONVERT(VARCHAR(8),IM.IFECHAING, 112) AS INT) - CAST(CONVERT(VARCHAR(8),PAC.IPFECNACI, 112) AS INT)) / 10000) AS [EDAD DE LA MADRE],
DEP.NOMDEPART AS DEPARTAMENTO, 
MUN.MUNNOMBRE AS MUNICIPIO,
UB.UBINOMBRE AS UBICACION,
PAC.IPTELEFON AS [TELEFONO],
PAC.IPTELMOVI AS [CELULAR],
EAPB.CODENTIDA AS [CODIGO EAPB],
EAPB.NOMENTIDA AS [NOMBRE EAPB],
CASE PAC.IPTIPOPAC WHEN 1 THEN 'Contributivo'
				   WHEN 2 THEN 'Subsidiado'
				   WHEN 3 THEN 'Vinculado'
				   WHEN 4 THEN 'Particular'
				   WHEN 5 THEN 'Otro' 
				   WHEN 6 THEN 'Desplazado Reg. Contributivo'
				   WHEN 7 THEN 'Desplazado Reg. Subsidiado'
				   WHEN 8 THEN 'Desplazado No Asegurado' END AS [REGIMEN],
CEN.NOMCENATE AS [CENTRO DE ATENCION],
IM.NUMINGRESHIJO AS [INGRESO DEL HIJO],
FUN.UFUDESCRI AS [UNIDAD FUNCIONAL],
RNC.NUMHIJREG AS [NUMERO DE HIJO],
RNC.FECHANACIM AS [FECHA DE NACIMIENTO],
CASE RNC.SEXRECNAC WHEN 1 THEN 'MASCULINO' ELSE 'FEMENINO' END AS SEXO,
RNC.VITANACIM AS [ESTADO NACIMIENTO],
RNC.RECPERCEF AS [PERIMETRO CEFALICO cm],
RNC.RECPERTOR AS [PERIMETRO TORAXICO cm],
RNC.RECPERABD AS [PERIMETRO ABDOMINAL cm],
RNC.TEMPERREC AS TEMPERATURA,
RNC.FRECARREC AS [FRECUENCIA CARDIACA],
RNC.FRERESREC AS [FRECUENCIA RESPIRATORIA],
OX.REGSO2PAC AS [SATURACION DE OXIGENO 1],
OX.FECREGITE AS [FECHA TOMA DE OXIGENO 1],
OXI.REGSO2PAC AS [SATURACION DE OXIGENO 2],
OXI.FECREGITE AS [FECHA TOMA DE OXIGENO 2],
RNC.APGAR1REC AS [APGAR 1 MINUTO],
RNC.APGAR5REC AS [APGAR 5 MINUTOS],
RNC.APGAR10RN AS [APGAR 10 MINUTOS],
RNC.ADAPRECNA AS [TIPO DE ADAPTACION],
RNC.TALLARECI AS [TALLA cm],
RNC.PESORECNA AS [PESO gr],
RNC.EDADGESNAC AS [EDAD GESTACIONAL],
'SIN DATO' AS [AGUDEZA OJO D],
'SIN DATO' AS [AGUDEZA OJO I],
1 as 'CANTIDAD',
CAST(RNC.FECHANACIM AS date) AS 'FECHA BUSQUEDA',
YEAR(RNC.FECHANACIM) AS 'AÑO FECHA BUSQUEDA',
MONTH(RNC.FECHANACIM) AS 'MES AÑO FECHA BUSQUEDA',
CASE MONTH(RNC.FECHANACIM) 
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
	WHEN 12 THEN 'DICIEMBRE'
  END AS 'MES NOMBRE FECHA BUSQUEDA', 
FORMAT(DAY(RNC.FECHANACIM), '00') AS 'DIA FECHA BUSQUEDA',
CONCAT(FORMAT(MONTH(RNC.FECHANACIM), '00') ,' - ', 
	   CASE MONTH(RNC.FECHANACIM) 
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
			WHEN 12 THEN 'DICIEMBRE'
		END) MES_LABEL_BUSQUEDA,
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
DBO.INPACIENT PAC INNER JOIN
CTE_INGRESOS_MADRES IM ON PAC.IPCODPACI=IM.IPCODPACI INNER JOIN
INUBICACI UB ON PAC.AUUBICACI=UB.AUUBICACI INNER JOIN
INMUNICIP MUN ON UB.DEPMUNCOD = MUN.DEPMUNCOD INNER JOIN
INDEPARTA DEP ON MUN.DEPCODIGO = DEP.DEPCODIGO AND PAC.AUUBICACI = UB.AUUBICACI INNER JOIN
INENTIDAD AS EAPB ON PAC.CODENTIDA=EAPB.CODENTIDA INNER JOIN
dbo.HCRECINAC RNC ON IM.NUMINGRESHIJO=RNC.NUMINGRESHIJO INNER JOIN
dbo.ADCENATEN CEN ON RNC.CODCENATE=CEN.CODCENATE INNER JOIN
dbo.INUNIFUNC FUN ON RNC.UFUCODIGO=FUN.UFUCODIGO LEFT JOIN
CTE_OXIGENO1 OX ON IM.NUMINGRESHIJO=OX.NUMINGRESHIJO LEFT JOIN
CTE_OXIGENO2 OXI ON IM.NUMINGRESHIJO=OXI.NUMINGRESHIJO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a consumo analítico (BI/reporting) que consolida en una sola fila por recién nacido los datos clínicos del nacimiento (APGAR, antropometría, signos vitales, saturación de oxígeno en dos tomas, edad gestacional) junto con la información demográfica y de aseguramiento de la madre. Vincula el ingreso de la madre con el ingreso del hijo mediante la tabla de relación neonatal, y enriquece el resultado con ubicación geográfica, régimen de salud, EAPB, centro de atención y unidad funcional. Incluye campos de fecha descompuestos (año, mes, día, etiquetas en español) para facilitar filtros y agrupaciones en herramientas de visualización.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewRecienNacidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewRecienNacidos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único reporte la información clínica y demográfica de los recién nacidos junto con los datos de su madre, ubicación, asegurador, centro de atención, signos vitales y mediciones de saturación de oxígeno.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewRecienNacidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el vínculo madre-hijo en HCINGRESORECNAC con el NUMINGRES de la madre en ADINGRESO.; El recién nacido debe tener registro clínico en HCRECINAC asociado a NUMINGRESHIJO.; El paciente (madre) debe tener ubicación válida en INUBICACI con municipio en INMUNICIP y departamento en INDEPARTA.; La madre debe estar afiliada a una entidad existente en INENTIDAD.; El recién nacido debe tener un centro de atención (ADCENATEN) y unidad funcional (INUNIFUNC) registrados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewRecienNacidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La edad de la madre se calcula como la diferencia de años entre la fecha de ingreso y la fecha de nacimiento, usando FLOOR((AAAAMMDD_ing - AAAAMMDD_nac)/10000).; Solo se consideran tomas de saturación de oxígeno con valor distinto de 0.; Por cada recién nacido se reportan máximo dos tomas de oxígeno (las primeras dos en orden cronológico por FECREGITE).; El campo CANTIDAD siempre es 1 (cada fila representa un recién nacido).; Los campos AGUDEZA OJO D y AGUDEZA OJO I siempre se devuelven como ''SIN DATO''.; ID_COMPANY corresponde al nombre de la base de datos truncado a 9 caracteres.; ULT_ACTUAL se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; Solo se incluyen madres cuyo ingreso esté efectivamente enlazado a un ingreso de hijo en HCINGRESORECNAC (INNER JOIN), garantizando vínculo madre-hijo.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewRecienNacidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Recién nacido; Madre / vínculo madre-hijo; Ingreso hospitalario; Tipo de documento; Régimen de afiliación (EAPB); Centro de atención; Unidad funcional; Signos vitales (frecuencia cardiaca, respiratoria, temperatura); Saturación de oxígeno; Puntuación APGAR (1, 5, 10 minutos); Antropometría neonatal (peso, talla, perímetros cefálico/torácico/abdominal); Edad gestacional; Adaptación neonatal; Estado de nacimiento (vitalidad)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewRecienNacidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewRecienNacidos: Devuelve un registro por recién nacido (HCRECINAC) cuya madre tenga ingreso vinculado vía HCINGRESORECNAC, con datos de la madre, datos del nacimiento y hasta dos mediciones de saturación de oxígeno (las dos primeras tomas con REGSO2PAC<>0).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewRecienNacidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PAC.IPTIPODOC entre 1..12 → Mapea código a etiqueta de tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, NV, CD, SC, PE) else Devuelve ''OTRO''; si PAC.IPTIPOPAC entre 1..8 → Mapea código a régimen de afiliación (Contributivo, Subsidiado, Vinculado, Particular, Otro, Desplazado Contributivo/Subsidiado/No Asegurado); si RNC.SEXRECNAC = 1 → Reporta sexo ''MASCULINO'' else Reporta sexo ''FEMENINO'' (cualquier otro valor); si FIS.REGSO2PAC != 0 → Considera la medición para el ranking de tomas de oxígeno por NUMINGRESHIJO; toma 1 = primera, toma 2 = segunda según FECREGITE ascendente else Excluye la medición del cálculo de saturación; si MONTH(RNC.FECHANACIM) entre 1..12 → Genera etiqueta de mes en español (ENERO..DICIEMBRE) y MES_LABEL_BUSQUEDA con formato ''NN - NOMBRE''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewRecienNacidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.HCINGRESORECNAC; dbo.HCEXFISIC; dbo.INPACIENT; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.INENTIDAD; dbo.HCRECINAC; dbo.ADCENATEN; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewRecienNacidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewRecienNacidos';
GO
