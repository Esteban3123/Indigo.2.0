

/*******************************************************************************************************************
Nombre: [Report].[ViewDailyBedCensus]
Tipo:Procedimiento Vista
Observacion:Informe del censo diario de camas
Profesional: Nilsson Miguel Galindo Lopez
Fecha:29-09-2023
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 1
Persona que modifico:
Fecha:
Ovservaciones:
-----------------------------------------------------------------------------------------------------------------------------------------
Version 2
Persona que modifico:
Fecha:
Observacion:
***********************************************************************************************************************************/
CREATE VIEW [Report].[ViewDailyBedCensus] AS

WITH
CTE_ESPECIALIDAD_FINAL AS
(
SELECT
EST.NUMINGRES,
RTRIM(PRO.CODPROSAL)+' - '+PRO.NOMMEDICO AS MEDICO,
ESP.CODESPECI+' - '+ESP.DESESPECI AS ESPECIALIDAD
FROM 
dbo.CHREGESTA EST INNER JOIN
dbo.HCHISPACA HC ON EST.NUMINGRES=HC.NUMINGRES AND EST.REGESTADO = 1 AND HC.NUMEFOLIO=(SELECT MAX(H.NUMEFOLIO) FROM dbo.HCHISPACA H WHERE EST.NUMINGRES=H.NUMINGRES) INNER JOIN
dbo.INPROFSAL PRO ON HC.CODPROSAL = PRO.CODPROSAL INNER JOIN
dbo.INESPECIA ESP ON PRO.CODESPEC1 = ESP.CODESPECI 
),

CTE_REFERENCIA AS
(
SELECT
REF.NUMINGRES,
REF.FECSOLICIT
FROM
dbo.CHREGESTA EST INNER JOIN
DBO.HCREFCONP REF ON EST.NUMINGRES=REF.NUMINGRES AND EST.REGESTADO = 1 AND REF.AUTO=(SELECT MAX(RE.AUTO) FROM DBO.HCREFCONP RE WHERE EST.NUMINGRES=RE.NUMINGRES)
),

CTE_PAD AS
(
SELECT
CON.NUMINGRES,
CON.FECHAREGISTRO
FROM
dbo.CHREGESTA EST INNER JOIN
dbo.PADCONTROL CON ON EST.NUMINGRES=CON.NUMINGRES AND EST.REGESTADO = 1 AND CON.ID=(SELECT MAX(C.ID) FROM dbo.PADCONTROL C WHERE EST.NUMINGRES=C.NUMINGRES)
),

CTE_ALTA_MEDICA AS
(
SELECT 
EGR.NUMINGRES
FROM 
dbo.CHREGESTA EST INNER JOIN
dbo.HCREGEGRE EGR ON EST.NUMINGRES=EGR.NUMINGRES AND EST.REGESTADO = 1 AND EGR.NUMEFOLIO=(SELECT MAX(EG.NUMEFOLIO) FROM dbo.HCREGEGRE EG WHERE EST.NUMINGRES=EG.NUMINGRES)
),
CTE_SERVICIOS_PENDIENTES AS
(
SELECT
RCD.id, 
RCD.Status,
RC.AdmissionNumber AS INGRESO
FROM BILLING.REVENUECONTROLDETAIL AS RCD
INNER JOIN BILLING.REVENUECONTROL AS RC ON RCD.REVENUECONTROLID = RC.ID
INNER JOIN DBO.ADINGRESO AS ING ON ING.NUMINGRES = RC.ADMISSIONNUMBER
INNER JOIN dbo.CHREGESTA AS CAM ON CAM.NUMINGRES =ING.NUMINGRES AND CAM.REGESTADO = 1
),

CTE_VALOR_ESTANCIA
AS
(
 SELECT 
 RCD.INGRESO,
 SUM(ISNULL(DQ.TOTALSALESPRICE,SOD.GrandTotalSalesPrice)) 'DET_ORD_VALOR_TOTAL'
 FROM CTE_SERVICIOS_PENDIENTES  AS RCD
 INNER JOIN BILLING.SERVICEORDERDETAILDISTRIBUTION AS SODD ON RCD.ID = SODD.REVENUECONTROLDETAILID AND RCD.STATUS IN ('1', '3')
 INNER JOIN BILLING.SERVICEORDERDETAIL AS SOD ON SODD.SERVICEORDERDETAILID = SOD.ID
 LEFT JOIN Billing.ServiceOrderDetailSurgical AS DQ ON DQ.ServiceOrderDetailId = SOD.Id AND DQ.OnlyMedicalFees = '0'
 WHERE SOD.IsDelete ='0'
 GROUP BY RCD.INGRESO
 )

SELECT
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
CEN.NOMCENATE AS [CENTRO DE ATENCIÓN],
RTRIM(UF.UFUCODIGO)+' - '+UF.UFUDESCRI AS [UNIDAD FUNCIONAL],
CAM.CODICAMAS AS [CODIGO CAMA],
CAM.NUMCAMHOS AS [#CAMA],
CAM.DESCCAMAS AS [DESCRIPCIÓN CAMA],
CASE CAM.CODCLAHAB WHEN '1' THEN 'SALA OBSERVACION' 
				   WHEN '2' THEN 'SALA PROCEDIMIENTO' 
				   WHEN '3' THEN 'SALA RECUPERACION' 
				   WHEN '4' THEN 'HABITACION 1 CAMA' 
				   WHEN '5' THEN 'HABITACION 2 CAMAS'  
				   WHEN '6' THEN 'HABITACION 3 CAMAS'  
				   WHEN '7' THEN 'HABITACION 4 CAMAS'  
				   WHEN '8' THEN 'SUITE' 
				   WHEN '9' THEN 'HAB.ESPECIAL' 
				   WHEN '10' THEN 'UCI'  
				   WHEN '11' THEN 'OTRO' END AS [CLASE DE HABITACION], 
CASE CAM.CODCLACAM WHEN '1' THEN UPPER('ObservacionUrgencias') 
				   WHEN '2' THEN UPPER('Recuperacion Post Qx')  
				   WHEN '3' THEN 'HOSPITALARIA' END AS [CLASE DE CAMA],
IIF(EST.NUMINGRES IS NULL,'DISPONIBLE','OCUPADA') AS [ESTADO CAMA],
DOC.NOMBRE AS [TIPO DE IDENTIFICACIÓN],
EST.IPCODPACI AS IDENTIFICACION,
PAC.IPNOMCOMP AS [NONBRE PACIENTE],PAC.IPPRINOMB 'PRIMER NOMBRE', PAC.IPSEGNOMB 'SEGUNDO NOMBRE',PAC.IPPRIAPEL 'PRIMER APELLIDO', PAC.IPSEGAPEL 'SEGUNDO APELLIDO',
CASE PAC.IPSEXOPAC WHEN 1 THEN 'MASCULINO' 
				   WHEN 2 THEN 'FEMENINO' ELSE NULL END AS SEXO,
CAST(PAC.IPFECNACI AS DATE) AS [FECHA NACIMIENTO],
FLOOR((CAST(CONVERT(VARCHAR(8), EST.FECINIEST  , 112) AS INT) - CAST(CONVERT(VARCHAR(8), PAC.IPFECNACI, 112) AS INT)) / 10000) AS EDAD,
PAC.IPDIRECCI AS DIRECCION, 
PAC.IPTELEFON AS TELEFONO, 
PAC.IPTELMOVI AS MOVIL,
EST.NUMINGRES AS INGRESO,
EST.FECINIEST AS [FECHA INICIO ESTANCIA],
DATEDIFF(DAY,EST.FECINIEST,GETDATE()) [DIAS TRANSCURRIDOS],
TIP.DESTIPEST AS [TIPO DE ESTANCIA],
HA.Name AS ENTIDAD,
CASE HA.ENTITYTYPE WHEN 1 THEN 'EPS CONTRIBUTIVO' 
				   WHEN 2 THEN 'EPS SUBSIDIADO' 
				   WHEN 3 THEN 'ET VINCULADO MUNICIPIO' 
				   WHEN 4 THEN 'ET VINCULADOS DAPARTAMENTO'
				   WHEN 5 THEN 'ARL RIESGO LABORALES' 
				   WHEN 6 THEN 'MP MEDICINA PREPAGADA' 
				   WHEN 7 THEN 'IPS PRIVADA' 
				   WHEN 8 THEN 'IPS PUBLICA' 
				   WHEN 9 THEN 'REGIMEN ESPECIAL' 
				   WHEN 10 THEN 'ACCIDENTE DE TRANSITO'
				   WHEN 11 THEN 'FOSYGA' 
				   WHEN 12 THEN 'OTROS' END AS [REGIMEN],
CASE PAC.IPTIPOAFI WHEN 0 THEN 'No Aplica' 
				   WHEN 1 THEN 'Cotizante'
				   WHEN 2 THEN 'Beneficiario'
				   WHEN 3 THEN 'Adicional'
				   WHEN 4 THEN 'Jub/Retirado'
				   WHEN 5 THEN 'Pensionado' END AS [TIPO AFILIADO],
NV.NIVDESCRI AS [CATEGORIA],
RTRIM(NOS.CODDIAGNO) + ' - ' + RTRIM(NOS.NOMDIAGNO) AS [DIAGNOSTICO PRINCIPAL],
ESP.MEDICO AS [ULTIMO PROFESIONAL],
ESP.ESPECIALIDAD AS [ULTIMA ESPECIALIDAD],
ING.IAUTORIZA AS [NUMERO AUTORIZACIÓN], 
ING.IOBSERVAC AS [OBSERVACIONES],
IIF(REF.NUMINGRES IS NULL,IIF(EST.NUMINGRES IS NULL,NULL,'NO'),'SI') AS REFERENCIA,
REF.FECSOLICIT AS [SOLICITUD REFERENCIA],
IIF(PAD.NUMINGRES IS NULL,IIF(EST.NUMINGRES IS NULL,NULL,'NO'),'SI') AS PAD,
PAD.FECHAREGISTRO AS [FECHA PAD],
IIF(AL.NUMINGRES IS NULL,IIF(EST.NUMINGRES IS NULL,NULL,'1 - Pacientes en la Unidad'),'2 - Pacientes Con Salida') AS [ESTADO INGRESO],
ING.IFECHAING AS [FECHA INGRESO ADMISION],
IIF(EST.NUMINGRES IS NULL,0,ISNULL(VAL.DET_ORD_VALOR_TOTAL,0)) AS [VALOR ESTANCIA],
1 AS CANTIDAD,
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
dbo.CHCAMASHO CAM INNER JOIN
dbo.INUNIFUNC UF ON CAM.UFUCODIGO = UF.UFUCODIGO INNER JOIN
dbo.ADCENATEN CEN ON CAM.CODCENATE = CEN.CODCENATE LEFT JOIN
dbo.CHREGESTA EST ON CAM.CODICAMAS = EST.CODICAMAS AND EST.REGESTADO = 1 LEFT JOIN
dbo.CHTIPESTA TIP ON EST.CODTIPEST = TIP.CODTIPEST LEFT JOIN
dbo.INPACIENT PAC ON EST.IPCODPACI = PAC.IPCODPACI LEFT JOIN
dbo.ADTIPOIDENTIFICA DOC ON PAC.IPTIPODOC=DOC.CODIGO LEFT JOIN
dbo.ADINGRESO ING ON EST.NUMINGRES=ING.NUMINGRES LEFT JOIN
CONTRACT.HEALTHADMINISTRATOR HA ON ING.GENCONENTITY=HA.ID LEFT JOIN
INDIAGNOP DIA ON ING.NUMINGRES=DIA.NUMINGRES AND DIA.CODDIAPRI=1 LEFT JOIN
dbo.INDIAGNOS NOS ON DIA.CODDIAGNO=NOS.CODDIAGNO LEFT JOIN
dbo.ADNIVELES NV ON PAC.NIVCODIGO=NV.NIVCODIGO LEFT JOIN
CTE_ESPECIALIDAD_FINAL ESP ON EST.NUMINGRES=ESP.NUMINGRES LEFT JOIN
CTE_REFERENCIA REF ON EST.NUMINGRES=REF.NUMINGRES LEFT JOIN
CTE_PAD PAD ON EST.NUMINGRES=PAD.NUMINGRES LEFT JOIN
CTE_ALTA_MEDICA AL ON EST.NUMINGRES=AL.NUMINGRES LEFT JOIN
CTE_VALOR_ESTANCIA VAL ON EST.NUMINGRES=VAL.INGRESO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte para el censo diario de camas hospitalarias, orientada a consumo en tableros o informes de ocupación. Consolida el estado de cada cama (disponible/ocupada), datos demográficos y de afiliación del paciente hospitalizado, diagnóstico principal CIE-10, último profesional y especialidad tratante, días de estancia, valor económico acumulado de la estancia, y flags de referencia, PAD y alta médica. Permite identificar en tiempo real la ocupación por unidad funcional y centro de atención.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewDailyBedCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewDailyBedCensus';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de censo diario hospitalario que lista todas las camas con su estado (disponible/ocupada), datos del paciente, ingreso, profesional tratante, diagnóstico, entidad responsable, referencia, PAD, alta médica y valor acumulado de la estancia.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewDailyBedCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las camas deben existir en dbo.CHCAMASHO con su unidad funcional (INUNIFUNC) y centro de atención (ADCENATEN) válidos.; Para considerar la cama como ocupada, debe existir un registro en CHREGESTA con REGESTADO = 1 asociado a la cama.; Para obtener última especialidad/profesional, debe existir un folio en HCHISPACA con NUMEFOLIO máximo por ingreso.; Para diagnóstico principal, INDIAGNOP debe tener CODDIAPRI=1.; La base de datos donde se ejecuta debe corresponder a la compañía a reportar (DB_NAME()).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewDailyBedCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran estancias activas (CHREGESTA.REGESTADO = 1) para determinar ocupación de la cama.; Para los datos clínicos del ingreso siempre se toma el último folio: MAX(NUMEFOLIO) en HCHISPACA y HCREGEGRE, MAX(AUTO) en HCREFCONP, MAX(ID) en PADCONTROL.; El diagnóstico mostrado siempre es el principal (INDIAGNOP.CODDIAPRI=1).; El régimen del paciente se deriva del HEALTHADMINISTRATOR del ingreso, no del paciente.; El valor de estancia solo agrega servicios facturables vigentes (no eliminados) cuyo control de ingresos esté en estados 1 o 3.; ID_COMPANY siempre corresponde al nombre de la base de datos actual truncado a 9 caracteres.; Cada cama del maestro CHCAMASHO produce exactamente una fila (LEFT JOIN garantiza presencia aunque no haya paciente).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewDailyBedCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewDailyBedCensus: Devuelve una fila por cama hospitalaria de CHCAMASHO, con estado ''OCUPADA'' si existe estancia activa (CHREGESTA.REGESTADO=1) y ''DISPONIBLE'' en caso contrario.; [RETURN_RESULT] Report.ViewDailyBedCensus: Calcula EDAD como diferencia entera de años entre EST.FECINIEST y PAC.IPFECNACI usando formato YYYYMMDD: FLOOR((fecha_estancia - fecha_nacimiento)/10000).; [RETURN_RESULT] Report.ViewDailyBedCensus: Calcula DIAS TRANSCURRIDOS como DATEDIFF(DAY, EST.FECINIEST, GETDATE()) para camas ocupadas.; [RETURN_RESULT] Report.ViewDailyBedCensus: VALOR ESTANCIA suma TOTALSALESPRICE de ServiceOrderDetailSurgical (cuando OnlyMedicalFees=''0'') o, en su defecto, GrandTotalSalesPrice de SERVICEORDERDETAIL, solo para detalles con IsDelete=''0'' y RevenueControlDetail.Status IN (''1'',''3''); si la cama está libre, retorna 0.; [RETURN_RESULT] Report.ViewDailyBedCensus: Marca ESTADO INGRESO como ''2 - Pacientes Con Salida'' si existe registro de egreso en HCREGEGRE para el ingreso, ''1 - Pacientes en la Unidad'' si está ocupado sin egreso, y NULL si la cama está libre.; [RETURN_RESULT] Report.ViewDailyBedCensus: Marca REFERENCIA=''SI'' si existe registro en HCREFCONP para el ingreso, ''NO'' si el ingreso existe sin referencia, y NULL si la cama está libre. Igual lógica para PAD usando PADCONTROL.; [RETURN_RESULT] Report.ViewDailyBedCensus: ULT_ACTUAL se entrega convertido a la zona horaria ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewDailyBedCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EST.NUMINGRES IS NULL (no hay estancia activa para la cama) → ESTADO CAMA=''DISPONIBLE'', VALOR ESTANCIA=0, REFERENCIA/PAD/ESTADO INGRESO=NULL else ESTADO CAMA=''OCUPADA'' y se calculan banderas de referencia, PAD, alta médica y valor de estancia; si Existe registro en HCREGEGRE para el ingreso (CTE_ALTA_MEDICA) → ESTADO INGRESO=''2 - Pacientes Con Salida'' else ''1 - Pacientes en la Unidad'' si está ocupado; si CAM.CODCLAHAB con valores ''1''..''11'' → Mapea a etiquetas de clase de habitación (SALA OBSERVACION, SUITE, UCI, etc.); si CAM.CODCLACAM con valores ''1'',''2'',''3'' → Mapea a CLASE DE CAMA (ObservacionUrgencias, Recuperacion Post Qx, HOSPITALARIA); si HA.ENTITYTYPE entre 1 y 12 → Traduce el tipo de entidad responsable a régimen (EPS contributivo, subsidiado, ARL, FOSYGA, etc.); si PAC.IPTIPOAFI entre 0 y 5 → Clasifica TIPO AFILIADO (Cotizante, Beneficiario, Adicional, Jub/Retirado, Pensionado); si RCD.STATUS IN (''1'',''3'') y SOD.IsDelete=''0'' → El detalle de orden de servicio se incluye en el cálculo de valor de estancia else Se excluye del valor; si DQ.OnlyMedicalFees=''0'' (servicio quirúrgico no es solo honorarios médicos) → Se usa DQ.TOTALSALESPRICE como valor; si es NULL, se usa SOD.GrandTotalSalesPrice', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewDailyBedCensus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewDailyBedCensus';
GO
