

CREATE VIEW [Report].[ViewEstadisticoIngresos] AS
---------********* SCRIPS PARA MOSTRAR EL ESTADISTICO DE INGRESO INDEPENDIENTE DE SU ESTADO SE UTILIZA UN FILTRO DE FECHA INICIAL Y FINAL*********---------
--DECLARE	@FechaInicio Datetime ='2022-04-30';
--DECLARE	@FechaFin Datetime ='2022-05-05';

WITH CTE_INGRESOS_ABIERTOS
AS
(
SELECT 
ING.NUMINGRES 'INGRESO' ,ING.IPCODPACI 'IDENTIFICACION',
CAST(ING.IFECHAING AS DATE) 'FECHA INGRESO' ,CASE ING.TIPOINGRE WHEN 1 THEN 'AMBULATORIO' ELSE 'HOSPITALARIO' END 'TIPO INGRESO',
ING.IAUTORIZA ,ING.UFUINGMED ,ING.UFUEGRMED ,ING.UFUINGHOS ,
ING.UFUEGRHOS ,ING.CODPROING ,ING.CODPROEGR ,ING.CODESPTRA ,
ING.UFUAACTMED ,ING.UFUAACTHOS ,ING.CODCAMACT ,ING.CODDIAING ,ING.CODDIAEGR ,ING.UFUACTPAC ,ING.CODICAMHO ,ING.FECHOSPIT ,ING.GENCONENTITY ,ING.GENCAREGROUP ,
ING.FECHEGRESO 'FECHA EGRESO CAMA',
CASE ING.IESTADOIN WHEN '' THEN 'ABIERTO' 
				   WHEN 'F' THEN 'FACTURADO'
				   WHEN 'A' THEN 'ANULADO' 
				   WHEN 'C' THEN 'CERRADO' 
				   WHEN 'P' THEN 'FACTURADO PARCIAL' ELSE 'DESCONOCIDO' END AS 'ESTADO INGRESO' ,

CASE ING.ICAUSAING WHEN '1'  THEN 'Heridos en combate' 
                   WHEN '2'  THEN 'Enfermedad profesional' 
                   WHEN '3'  THEN 'Enfermedad general adulto' 
                   WHEN '4'  THEN 'Enfermedad general pediatria' 
                   WHEN '5'  THEN 'Odontología' 
                   WHEN '6'  THEN 'Accidente de transito' 
                   WHEN '7'  THEN 'Catastrofe/Fisalud' 
                   WHEN '8'  THEN 'Quemados' 
                   WHEN '9'  THEN 'Maternidad' 
                   WHEN '10' THEN 'Accidente Laboral' 
                   WHEN '11' THEN 'Cirugia Programada' END AS 'CAUSA DE INGRESO',
ING.UFUCODIGO, CEN.NOMCENATE 'CENTRO ATENCION',
MUN.MUNNOMBRE 'UBICACION',
ING.OBSERAREM 'OBSERVACIONES',
CASE ING.ITIPORIES WHEN '1' THEN 'Enfermedad General y Maternidad'
                   WHEN '2' THEN 'Accidente de Transito'
				   WHEN '3' THEN 'Catastrofe' END AS 'TIPO RIESGO',
ING.FECREGCRE 'FECHA CREACIÓN',
ING.CODUSUCRE 'CODIGO USUARIO CREA',
USU.NOMUSUARI 'USUARIO CREO',
ING.FECREGMOD 'FECHA MODIFICACIÓN',
ING.CODUSUMOD 'CODUSUARIOMODIFICO',
USUM.NOMUSUARI 'USUARIO ANULO'
FROM 
DBO.ADINGRESO ING WITH (NOLOCK) INNER JOIN 
DBO.ADCENATEN AS CEN ON CEN.CODCENATE =ING.CODCENATE LEFT JOIN 
DBO.SEGusuaru AS USU ON USU.CODUSUARI =ING.CODUSUCRE LEFT JOIN 
DBO.SEGusuaru AS USUM ON USUM.CODUSUARI =ING.CODUSUANU LEFT JOIN 
dbo.INMUNICIP AS MUN ON ING.MUNCODIGOACC  =MUN.MUNCODIGO

--WHERE  CAST(ING.IFECHAING AS DATE) BETWEEN  @FechaInicio AND @FechaFin
 ),

 CTE_CARGOS_INGRESOS
AS
(
SELECT 
AdmissionNumber 'INGRESO',SUM(RCD.TotalFolio) 'TOTAL FOLIO'  
FROM BILLING.REVENUECONTROL RC WITH (NOLOCK) INNER JOIN 
CTE_INGRESOS_ABIERTOS AS ING WITH (NOLOCK) ON RC.AdmissionNumber =ING.INGRESO INNER JOIN 
Billing.RevenueControlDetail AS RCD WITH (NOLOCK) ON RC.Id =RCD.RevenueControlId AND RCD.Status NOT IN (2,4) GROUP BY AdmissionNumber
),

CTE_ALTA_MEDICA
AS
(
SELECT EGR.NUMINGRES 'INGRESO',EGR.IPCODPACI 'IDENTIFICACION' ,EGR.FECALTPAC 'FECHA ALTA MEDICA' 
FROM DBO.HCREGEGRE EGR
INNER JOIN CTE_INGRESOS_ABIERTOS AS ING WITH (NOLOCK) ON EGR.NUMINGRES =ING.INGRESO 
),
CTE_UBICACION AS
(
select 
UBI.AUUBICACI, DEP.DEPMUNCOD AS CODIGOMUNI,DEP.MUNNOMBRE AS NOMMUNI,DEPA.depcodigo AS CODDEP,DEPA.nomdepart AS NOMDEP
from dbo.INUBICACI UBI INNER JOIN
dbo.INMUNICIP DEP ON UBI.DEPMUNCOD=DEP.DEPMUNCOD INNER JOIN 
dbo.INDEPARTA DEPA ON DEP.DEPCODIGO=DEPA.depcodigo
)

SELECT 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
ING.[CENTRO ATENCION] ,ING.INGRESO,
ING.[FECHA INGRESO],ING.[ESTADO INGRESO] 'ESTADO INGRESO',ING.[TIPO INGRESO],
CASE PAC.IPTIPOPAC 
  WHEN 1 THEN 'CONTRIBUTIVO' 
  WHEN 2 THEN 'SUBSIDIADO' 
  WHEN 3 THEN 'VINCULADO' 
  WHEN 4 THEN 'PARTICULAR' 
  WHEN 5 THEN 'OTRO' 
  WHEN 6 THEN 'DESPLAZADO REG. CONTRIBUTIVO' 	 
  WHEN 7 THEN 'DESPLAZADO REG. SUBSIDIADO' 
  ELSE 'DESPLAZADO NO ASEGURADO'
 END AS 'RÉGIMEN',CASE PAC.IPTIPODOC WHEN '1' THEN 'CEDULA DE CIUDADANIA' WHEN '2' THEN 'CEDULA DE EXTRANJERIA' WHEN '3' THEN 'TARJETA DE IDENTIDAD' WHEN '4' THEN 'REGISTRO CIVIL' 
	 WHEN '5' THEN 'PASAPORTE' WHEN '6' THEN 'ADULTO SIN IDENTIFICACION' WHEN '7' THEN 'MENOR SIN IDENTIFICACION' WHEN '8' THEN 'NUMERO UNICO DE IDENTIFICACION' 
	 WHEN '9' THEN 'CERTIFICADO NACIDO VIVO' WHEN '10' THEN 'CARNET DIPLOMATICO'WHEN '11' THEN 'SALVOCONDUCTO' WHEN '12' THEN 'PERMISO ESPECIAL DE PERMANENCIA' 
	 WHEN '13' THEN 'PERMISO TEMPORAL DE PERMANENCIA' WHEN '14' THEN 'DOCUMENTO EXTRANJERO' WHEN '15' THEN 'SIN IDENTIFICACION' END AS 'TIPO IDENTIFICACION',
ING.IDENTIFICACION ,
PAC.IPEXPEDIC 'LUGAREXPEDICION',
rtrim(PAC.IPNOMCOMP) 'PACIENTE',
CAST(PAC.IPFECNACI AS DATE) 'FECHA NACIMIENTO' ,FLOOR((CAST(CONVERT(VARCHAR(8), ING.[FECHA INGRESO]  , 112) AS INT) - CAST(CONVERT(VARCHAR(8), PAC.IPFECNACI, 112) AS INT)) / 10000) AS 'EDAD',
CASE PAC.IPSEXOPAC WHEN 1 THEN 'MASCULINO' ELSE 'FEMENINO' END AS 'SEXO',
rtrim(PAC.IPDIRECCI) AS DIRECCION, PAC.IPTELEFON AS TELEFONO, PAC.IPTELMOVI AS MOVIL,
UBI.CODIGOMUNI AS [CODIGO MUNICIPIO],
UBI.NOMMUNI AS [MUNICIPIO],
UBI.CODDEP AS [CODIGO DEPARTAMENTO],
UBI.NOMDEP AS [DEPARTAMENTO],
UNI.UFUCODIGO 'COD U FUNCIONAL',
rtrim(UNI.UFUDESCRI) 'UNIDAD FUNCIONAL',
ING.[OBSERVACIONES],
ING.UFUAACTMED 'UNFUNCIONAL ACTUAL',
ALT.[FECHA ALTA MEDICA],
ING.[FECHA EGRESO CAMA],
CAM.DESCCAMAS 'CAMA',
CASE WHEN CAM.DESCCAMAS IS NULL THEN 'NO' ELSE 'SI' END CON_CAMA_ACTIVA,
EA.Code 'CODIGO ENTIDAD' ,EA.Name 'ENTIDAD',
GA.Code 'CODIGO GRUPO ATENCION' ,GA.Name AS 'GRUPO DE ATENCION',
CASE PAC.NIVECODIGO WHEN '01' THEN 'RANGO A'
					WHEN '02' THEN 'RANGO B'
					WHEN '03' THEN 'RANGO C'
					WHEN '04' THEN 'OTROS' END AS RANGOS,
ISNULL(VAL.[TOTAL FOLIO],'0') [TOTAL FOLIO],
ING.[FECHA CREACIÓN],
ING.[USUARIO CREO] ,
ING.[FECHA MODIFICACIÓN],
ING.CODUSUARIOMODIFICO,
ING.[USUARIO ANULO] ,
--CIE10 .CODDIAGNO 'CIE10', 
--CIE10.NOMDIAGNO 'DIAGNOSTICO',
ING. CODDIAING + ' ' +  CIE10.NOMDIAGNO 'CIE INGRESO',
ING.CODDIAEGR + ' ' + CIE10.NOMDIAGNO 'CIE EGRESO',
ING.[CAUSA DE INGRESO],
ING.[TIPO RIESGO],
1 as 'CANTIDAD',
CAST([FECHA INGRESO] AS date) AS 'FECHA BUSQUEDA',
YEAR([FECHA INGRESO]) AS 'AÑO FECHA BUSQUEDA',
MONTH([FECHA INGRESO]) AS 'MES AÑO FECHA BUSQUEDA',
CASE MONTH([FECHA INGRESO]) 
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
FORMAT(DAY([FECHA INGRESO]), '00') AS 'DIA FECHA BUSQUEDA',
CONCAT(FORMAT(MONTH([FECHA INGRESO]), '00') ,' - ', 
	   CASE MONTH([FECHA INGRESO]) 
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
FROM CTE_INGRESOS_ABIERTOS AS ING WITH (NOLOCK)
INNER JOIN CONTRACT.CAREGROUP AS GA WITH (NOLOCK) ON GA.ID = ING.GENCAREGROUP
INNER JOIN CONTRACT.HEALTHADMINISTRATOR AS EA WITH (NOLOCK) ON EA.ID = ING.GENCONENTITY
INNER JOIN DBO.INPACIENT AS PAC WITH (NOLOCK) ON PAC.IPCODPACI = ING.IDENTIFICACION 
INNER JOIN CTE_UBICACION AS UBI ON PAC.AUUBICACI=UBI.AUUBICACI
INNER JOIN DBO.INUNIFUNC AS UNI WITH (NOLOCK) ON UNI.UFUCODIGO = ISNULL(ING.UFUACTPAC ,ING.UFUCODIGO) 
LEFT JOIN CTE_ALTA_MEDICA AS ALT WITH (NOLOCK) ON ALT.INGRESO  =ING.INGRESO 
LEFT JOIN DBO.CHCAMASHO AS CAM ON CAM.CODICAMAS  =ING.CODCAMACT 
LEFT JOIN CTE_CARGOS_INGRESOS AS VAL ON VAL.INGRESO =ING.INGRESO 
LEFT JOIN DBO.INDIAGNOS AS CIE10 WITH (NOLOCK) ON CIE10.CODDIAGNO = ISNULL(ING.CODDIAEGR,ING.CODDIAING)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que aplana en una sola fila todos los datos estadísticos de cada ingreso hospitalario o ambulatorio: información del paciente (régimen, tipo documento, edad calculada, sexo, ubicación geográfica hasta departamento), estado del ingreso (abierto/cerrado/facturado/anulado), causa y tipo de riesgo, unidad funcional activa, fecha de alta médica, cama asignada, entidad y grupo de atención contratante, total facturado en folios vigentes, y dimensiones de fecha desagregadas (año, mes, día, etiqueta de mes) para facilitar análisis en herramientas de BI o reportes estadísticos de ingresos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewEstadisticoIngresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewEstadisticoIngresos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista estadística que consolida los ingresos (ambulatorios y hospitalarios) con datos del paciente, ubicación geográfica, entidad responsable, cama, diagnósticos, alta médica y total facturado del folio, para fines de reporte.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewEstadisticoIngresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe tener un centro de atención válido en ADCENATEN (INNER JOIN); El ingreso debe tener un grupo de atención (GENCAREGROUP) registrado en CONTRACT.CAREGROUP; El ingreso debe tener una entidad administradora de salud (GENCONENTITY) registrada en CONTRACT.HEALTHADMINISTRATOR; El paciente (IPCODPACI) debe existir en INPACIENT; La ubicación del paciente (AUUBICACI) debe existir en INUBICACI con municipio y departamento válidos; La unidad funcional resuelta (UFUACTPAC o UFUCODIGO) debe existir en INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewEstadisticoIngresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La edad se calcula por diferencia de años calendario entre la fecha de ingreso y la fecha de nacimiento usando aritmética sobre formato YYYYMMDD (FLOOR((aaaammdd_ing - aaaammdd_nac)/10000)); El TOTAL FOLIO suma únicamente detalles cuyo Status no esté en (2,4); si el ingreso no tiene cargos válidos, se reporta ''0'' (ISNULL); Cada ingreso emite siempre CANTIDAD = 1 (apto para conteos agregados); El identificador de empresa se toma dinámicamente desde DB_NAME() truncado a 9 caracteres; La marca de última actualización (ULT_ACTUAL) se calcula con la zona horaria ''Pakistan Standard Time''; El sexo se reporta como MASCULINO solo si IPSEXOPAC = 1; cualquier otro valor se reporta como FEMENINO; Solo se incluyen ingresos cuyo grupo de atención y entidad administradora estén registrados (INNER JOIN obligatorio)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewEstadisticoIngresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewEstadisticoIngresos: Devuelve un registro por cada ingreso de ADINGRESO, independientemente de su estado (abierto, facturado, anulado, cerrado, parcial), enriquecido con datos demográficos, geográficos, contractuales, clínicos y financieros.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewEstadisticoIngresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ING.TIPOINGRE = 1 → Clasifica el ingreso como ''AMBULATORIO'' else Clasifica el ingreso como ''HOSPITALARIO''; si ING.IESTADOIN ∈ {'''',''F'',''A'',''C'',''P''} → Traduce a etiqueta legible: ''''→ABIERTO, ''F''→FACTURADO, ''A''→ANULADO, ''C''→CERRADO, ''P''→FACTURADO PARCIAL else Se reporta como ''DESCONOCIDO''; si ING.ICAUSAING ∈ {''1''..''11''} → Traduce el código numérico a la causa de ingreso (Heridos en combate, Enfermedad profesional/general, Odontología, Accidente de tránsito, Catástrofe/Fisalud, Quemados, Maternidad, Accidente Laboral, Cirugía Programada); si ING.ITIPORIES ∈ {''1'',''2'',''3''} → Mapea tipo de riesgo: 1=Enfermedad General y Maternidad, 2=Accidente de Tránsito, 3=Catástrofe; si PAC.IPTIPOPAC ∈ {1..7} → Mapea régimen de afiliación (Contributivo, Subsidiado, Vinculado, Particular, Otro, Desplazados contributivo/subsidiado) else Asigna ''DESPLAZADO NO ASEGURADO''; si PAC.NIVECODIGO ∈ {''01'',''02'',''03'',''04''} → Asigna RANGO A/B/C/OTROS según nivel del paciente; si CAM.DESCCAMAS IS NULL → CON_CAMA_ACTIVA = ''NO'' else CON_CAMA_ACTIVA = ''SI''; si RCD.Status NOT IN (2,4) → Incluye el detalle de RevenueControlDetail en la suma del TOTAL FOLIO (excluye estados 2 y 4, presumiblemente anulados/no válidos); si ING.UFUACTPAC IS NULL → Usa ING.UFUCODIGO como unidad funcional para el join con INUNIFUNC else Usa ING.UFUACTPAC; si ING.CODDIAEGR IS NULL → Usa CODDIAING como diagnóstico para resolver descripción CIE10 else Usa CODDIAEGR', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewEstadisticoIngresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.ADINGRESO; DBO.ADCENATEN; DBO.SEGusuaru; dbo.INMUNICIP; BILLING.REVENUECONTROL; Billing.RevenueControlDetail; DBO.HCREGEGRE; dbo.INUBICACI; dbo.INDEPARTA; CONTRACT.CAREGROUP; CONTRACT.HEALTHADMINISTRATOR; DBO.INPACIENT; DBO.INUNIFUNC; DBO.CHCAMASHO; DBO.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewEstadisticoIngresos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewEstadisticoIngresos';
GO
