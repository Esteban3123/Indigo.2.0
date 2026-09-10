
CREATE VIEW [Report].[ViewReporteFIAS19] as

WITH DISPENSACION AS
 (
  SELECT AdmissionNumber 'INGRESO',
         C.ConfirmationDate 'FECHA ENTREGA',
	     ATC.Code 'CODIGO MEDICAMENTO',
		 P.Code 'CUM',
		 D.Quantity 'CANTIDAD ENTREGADA',
		 D.EntityId 'IDFORMULA',
		 PF.Name 'FORMA FARMACEUTICA',
		 CASE WHEN ATC.POSProduct='0' THEN 'NO' ELSE 'SI' END [INCLUIDO EN PBS],
		 CASE WHEN ATC.Conditioned='0' THEN 'NO' ELSE 'SI' END [MEDICMANETO CONDICIONADO],
		 CASE WHEN ATC.UNIRS ='0' THEN 'NO' ELSE 'SI' END [MEDICMANETO UNIRS],
		 IIF (ATC.AllPOSPathologies = 'TRUE', 'SI', 'NO') [TODAS_LAS_PATOLOGIAS],
		 C.Status as ESTADO_DIS
  FROM 
   Inventory.PharmaceuticalDispensing AS C
   JOIN Inventory.PharmaceuticalDispensingDetail AS D ON C.Id =D.PharmaceuticalDispensingId 
   JOIN Inventory.InventoryProduct AS P ON  D.ProductId =P.ID 
   JOIN Inventory.ATC AS ATC  ON P.ATCId =ATC.Id  
   LEFT JOIN Inventory.PharmaceuticalForm PF ON ATC.PharmaceuticalFormId = PF.Id 
)

SELECT
  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
  CASE c.TIPOSOLICITUD 
       WHEN 1 THEN 'IntraHospitalario'
	   WHEN 3 THEN 'Mixto'
	   ELSE  'Extramural'	   
  END 'AMBITO DISPENSACION',
  C.CODCONCEC 'FORMULA',
  MED.NOMMEDICO 'PROFESIONAL',
  CEN.NOMCENATE 'CENTRO ATENCION',
  DEP.depcodigo AS 'COD. DEPARTAMENTO',
  DEP.nomdepart AS 'DEPARTAMENTO',
  MUN.DEPCODIGO AS 'COD. MUNICIPIO',
  MUN.MUNNOMBRE AS 'MUNICIPIO',
  UNI.UFUDESCRI 'UNIDAD FUNCIONAL',
  HA.Name 'ENTIDAD-EAPB',
  C.IPCODPACI 'IDENTIFICACION',
  CASE PAC.IPTIPODOC WHEN '1' THEN 'CEDULA DE CIUDADANIA'
					 WHEN '2' THEN 'CEDULA DE EXTRANJERIA'
					 WHEN '3' THEN 'TARJETA DE IDENTIDAD'
					 WHEN '4' THEN 'REGISTRO CIVIL'
					 WHEN '5' THEN 'PASAPORTE'
					 WHEN '6' THEN 'ADULTO SIN IDENTIFICACION'
					 WHEN '7' THEN 'MENOR SIN IDENTIFICACION'
					 WHEN '8' THEN 'NUMERO UNICO DE IDENTIFICACIÒN'
					 WHEN '9' THEN 'CERTIFICADO NACIDO VIVO'
					 WHEN '10' THEN 'CARNET DIPLOMATICO'
					 WHEN '11' THEN 'SALVOCONDUCTO'
					 WHEN '12' THEN 'PERMISO ESPECIAL DE PERMANENCIA' END AS [TIPO DOCUMENTO],
  PAC.IPNOMCOMP AS 'NOMBRE PACIENTE',
  C.NUMINGRES 'INGRESO',
  D.CODPRODUC 'CODIGO MEDICAMENTO',
  PRO.DESPRODUC 'MEDICAMENTO' , 
  PRE.DESFORMED 'PRESENTACION',
  PG.Name 'GRUPO FARMACOLOGICO' ,
  DIS.[FORMA FARMACEUTICA] 'FORMA FARMACEUTICA',
  C.FECHAORDE 'FECHA SOLICITUD',
  CANPEDPRO 'CANTIDAD SOLICITADA',
  DIS.[FECHA ENTREGA], 
  ISNULL(DIS.[CANTIDAD ENTREGADA] ,0) [CANTIDAD ENTREGADA],
  CASE 
   WHEN DIS.[CANTIDAD ENTREGADA] IS NULL THEN 'NO ENTREGADO'
   WHEN CANPEDPRO - DIS.[CANTIDAD ENTREGADA] = 0 THEN 'ENTREGA COMPLETA'
   ELSE 'ENTREGA INCOMPLETA' 
  END 'ENTREGA FORMULA',
  DATEDIFF(HOUR, C.FECHAORDE,DIS.[FECHA ENTREGA]) [DIFERENCIA EN HORAS],
  [INCLUIDO EN PBS],
  [MEDICMANETO CONDICIONADO],
  [MEDICMANETO UNIRS],
  [TODAS_LAS_PATOLOGIAS],
  CAST(C.FECHAORDE AS DATE) AS 'FECHA BUSQUEDA', 
  YEAR(C.FECHAORDE) AS 'AÑO FECHA BUSQUEDA', 
  MONTH(C.FECHAORDE) AS 'MES AÑO FECHA BUSQUEDA',
  CASE MONTH(C.FECHAORDE)
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
 DAY(C.FECHAORDE) AS 'DIA FECHA BUSQUEDA',
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
 HCFARMEPC AS C
 JOIN HCFARMEPD AS D ON C.CODCONCEC = D.CODCONCEC AND C.NUMINGRES =D.NUMINGRES 
 JOIN IHLISTPRO AS PRO ON D.CODPRODUC = PRO.CODPRODUC
 JOIN INPROFSAL AS MED ON D.CODPROSAL = MED.CODPROSAL 
 JOIN ADCENATEN AS CEN ON C.CODCENATE = CEN.CODCENATE
 JOIN INUNIFUNC AS UNI ON C.UFUCODIGO = UNI.UFUCODIGO 
 JOIN Payroll.FunctionalUnit AS FU ON FU.Code =UNI.UFUCODIGO 
 JOIN INPACIENT AS PAC ON C.IPCODPACI =PAC.IPCODPACI
 LEFT JOIN INUBICACI AS UB WITH(NOLOCK) ON UB.AUUBICACI = PAC.AUUBICACI
 LEFT JOIN INMUNICIP AS MUN WITH(NOLOCK) ON MUN.DEPMUNCOD = UB.DEPMUNCOD
 LEFT JOIN INDEPARTA AS DEP WITH(NOLOCK) ON DEP.depcodigo=MUN.DEPCODIGO
 JOIN ADINGRESO AS ING ON C.NUMINGRES =ING.NUMINGRES 
 JOIN Contract.HealthAdministrator AS HA ON ING.GENCONENTITY =HA.Id  
 JOIN Inventory.ATC AS ATC  ON D.CODPRODUC =ATC.Code 
 JOIN Inventory.ATCEntity AS ATCE ON ATC.ATCEntityId =ATCE.Id 
 JOIN Inventory.PharmacologicalGroup AS PG ON PG.Id =ATCE.IdPharmacologicalGroup 
 LEFT JOIN IHFORMEDI AS PRE ON PRO.CODFORMED =PRE.CODFORMED 
 LEFT JOIN DISPENSACION AS DIS ON D.ID = DIS.IDFORMULA AND D.NUMINGRES =DIS.INGRESO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada al reporte FIAS-19 de dispensación farmacéutica. Cruza fórmulas médicas solicitadas (cabecera y detalle) con los despachos confirmados en farmacia para determinar si cada ítem fue entregado completa, parcialmente o no entregado, calculando la diferencia en horas entre solicitud y entrega. Incluye clasificación ATC, CUM, forma farmacéutica, grupo farmacológico, pertenencia al PBS, condicionamiento y UNIRS, junto con datos demográficos del paciente, EAPB, municipio y unidad funcional para consumo analítico y regulatorio.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteFIAS19';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteFIAS19';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la trazabilidad de fórmulas médicas y su dispensación farmacéutica para el reporte FIAS19, cruzando solicitud, entrega, paciente, ubicación geográfica, EAPB y clasificación ATC del medicamento.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteFIAS19';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas legadas HCFARMEPC/HCFARMEPD deben tener correspondencia por CODCONCEC y NUMINGRES.; Cada producto dispensado debe existir en Inventory.ATC vía CODPRODUC=ATC.Code y tener ATCEntity con IdPharmacologicalGroup válido.; El ingreso (ADINGRESO) debe estar asociado a una HealthAdministrator (GENCONENTITY).; La unidad funcional UFUCODIGO debe existir tanto en INUNIFUNC como en Payroll.FunctionalUnit.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteFIAS19';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La cantidad entregada nunca se reporta como NULL en la salida: se aplica ISNULL(...,0).; La fecha de última actualización (ULT_ACTUAL) se calcula siempre con GETDATE() convertido a zona horaria ''Pakistan Standard Time''.; ID_COMPANY se trunca a los primeros 9 caracteres del nombre de la base de datos actual (DB_NAME()).; La diferencia entre solicitud y entrega se mide siempre en horas (DATEDIFF HOUR).; Solo se incluyen fórmulas cuyo medicamento (CODPRODUC) tenga clasificación ATC y grupo farmacológico (joins INNER con ATC, ATCEntity y PharmacologicalGroup).; Solo se incluyen ingresos con EAPB asociada (INNER JOIN con Contract.HealthAdministrator).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteFIAS19';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Fórmula médica; Medicamento POS/PBS; Medicamento condicionado; UNIRS; Clasificación ATC; Grupo farmacológico; Forma farmacéutica; Tipo de documento de identidad del paciente; EAPB / Administradora de salud; Unidad funcional; Centro de atención; Ámbito de dispensación (intrahospitalario/mixto/extramural); Ubicación geográfica del paciente (departamento/municipio); Ingreso hospitalario; Entrega completa vs incompleta', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteFIAS19';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewReporteFIAS19: Devuelve una fila por cada detalle de fórmula (HCFARMEPD) con su dispensación farmacéutica asociada, si existe (LEFT JOIN con CTE DISPENSACION).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteFIAS19';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.TIPOSOLICITUD = 1 → Clasifica AMBITO DISPENSACION como ''IntraHospitalario'' else Si =3 ''Mixto''; cualquier otro valor ''Extramural''; si DIS.[CANTIDAD ENTREGADA] IS NULL → Marca ENTREGA FORMULA como ''NO ENTREGADO'' else Si CANPEDPRO - CANTIDAD ENTREGADA = 0 → ''ENTREGA COMPLETA''; en otro caso ''ENTREGA INCOMPLETA''; si ATC.POSProduct=''0'' → INCLUIDO EN PBS = ''NO'' else ''SI''; si ATC.Conditioned=''0'' → MEDICAMENTO CONDICIONADO = ''NO'' else ''SI''; si ATC.UNIRS=''0'' → MEDICAMENTO UNIRS = ''NO'' else ''SI''; si ATC.AllPOSPathologies=''TRUE'' → TODAS_LAS_PATOLOGIAS = ''SI'' else ''NO''; si PAC.IPTIPODOC entre ''1''..''12'' → Mapea a etiqueta de tipo de documento (CC, CE, TI, RC, PA, ASI, MSI, NUI, CNV, CD, SC, PEP) else NULL', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteFIAS19';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Inventory.InventoryProduct; Inventory.ATC; Inventory.PharmaceuticalForm; Inventory.ATCEntity; Inventory.PharmacologicalGroup; Payroll.FunctionalUnit; Contract.HealthAdministrator; HCFARMEPC; HCFARMEPD; IHLISTPRO; INPROFSAL; ADCENATEN; INUNIFUNC; INPACIENT; INUBICACI; INMUNICIP; INDEPARTA; ADINGRESO; IHFORMEDI', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteFIAS19';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewReporteFIAS19';
GO
