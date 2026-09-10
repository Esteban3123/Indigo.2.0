

CREATE VIEW [Report].[ViewMedicamentosAplicados] as

WITH DISPENSACION AS
(
SELECT AdmissionNumber 'INGRESO',
      C.ConfirmationDate 'FECHA ENTREGA',ATC.Code 'CODIGO MEDICAMENTO',P.Code 'CUM',D.Quantity 'CANTIDAD ENTREGADA',D.EntityId 'IDFORMULA'
FROM Inventory.PharmaceuticalDispensing AS C WITH (NOLOCK)
JOIN Inventory.PharmaceuticalDispensingDetail AS D WITH (NOLOCK) ON C.Id =D.PharmaceuticalDispensingId 
JOIN Inventory.InventoryProduct AS P WITH (NOLOCK) ON  D.ProductId =P.ID 
JOIN Inventory.ATC AS ATC  WITH (NOLOCK) ON P.ATCId =ATC.Id  
WHERE C.Status =2
), 

CTE_CONSULTAS_ORDENAMIENTO  AS
(
SELECT
  CEN.NOMCENATE 'CENTRO ATENCION',
  UNI.UFUDESCRI 'UNIDAD FUNCIONAL',
  C.CODCONCEC 'FORMULA',
  MED.NOMMEDICO 'PROFESIONAL',
  HA.Name 'ENTIDAD',
  C.IPCODPACI 'IDENTIFICACION',
  PAC.IPNOMCOMP 'NOMBRE PACIENTE',
  C.NUMINGRES 'INGRESO',
  D.CODPRODUC 'CODIGO MEDICAMENTO',
  PRO.DESPRODUC 'MEDICAMENTO' , 
  PRE.DESFORMED 'PRESENTACION',
  PG.Name 'GRUPO FARMACOLOGICO' ,C.FECHAORDE 'FECHA ORDEN',
  HOM.FECAPLMED AS 'FECHA DE APLICACION',
  CANPEDPRO 'CANTIDAD SOLICITADA',
  DIS.[FECHA ENTREGA],
  ISNULL(DIS.[CANTIDAD ENTREGADA],0) [CANTIDAD ENTREGADA],
  CAST(ISNULL(DIS.[FECHA ENTREGA], C.FECHAORDE) AS DATE) AS 'FECHA BUSQUEDA' 
FROM HCFARMEPC AS C WITH (NOLOCK)
JOIN HCFARMEPD AS D WITH (NOLOCK) ON C.CODCONCEC =D.CODCONCEC AND C.NUMINGRES =D.NUMINGRES 
JOIN IHLISTPRO AS PRO WITH (NOLOCK) ON D.CODPRODUC =PRO.CODPRODUC AND PRO.TIPPRODUC ='1'
JOIN INPROFSAL AS MED WITH (NOLOCK) ON D.CODPROSAL =MED.CODPROSAL 
JOIN ADCENATEN AS CEN WITH (NOLOCK) ON C.CODCENATE =CEN.CODCENATE 
JOIN INUNIFUNC AS UNI WITH (NOLOCK) ON C.UFUCODIGO =UNI.UFUCODIGO 
JOIN INPACIENT AS PAC WITH (NOLOCK) ON C.IPCODPACI =PAC.IPCODPACI
JOIN ADINGRESO AS ING WITH (NOLOCK) ON C.NUMINGRES =ING.NUMINGRES 
JOIN Contract .HealthAdministrator AS HA WITH (NOLOCK) ON ING.GENCONENTITY =HA.Id  
JOIN Inventory .ATC AS ATC  WITH (NOLOCK) ON D.CODPRODUC =ATC.Code 
JOIN Inventory .ATCEntity AS ATCE WITH (NOLOCK) ON ATC.ATCEntityId =ATCE.Id 
JOIN Inventory .PharmacologicalGroup AS PG WITH (NOLOCK) ON PG.Id =ATCE.IdPharmacologicalGroup 
LEFT JOIN IHFORMEDI AS PRE WITH (NOLOCK) ON PRO.CODFORMED =PRE.CODFORMED 
LEFT JOIN DISPENSACION AS DIS WITH (NOLOCK) ON D.ID =DIS.IDFORMULA AND D.NUMINGRES =DIS.INGRESO LEFT JOIN
HCHOJAMED AS HOM ON HOM.IPCODPACI=D.IPCODPACI AND  HOM.NUMINGRES=ING.NUMINGRES AND HOM.CODPRODUC=DIS.[CODIGO MEDICAMENTO]
WHERE ORDESTADO <>'3'  
)

SELECT
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
 *,
 YEAR([FECHA BUSQUEDA]) AS 'AÑO FECHA BUSQUEDA', 
 MONTH([FECHA BUSQUEDA]) AS 'MES AÑO FECHA BUSQUEDA',
 CONCAT(FORMAT(MONTH([FECHA BUSQUEDA]), '00') ,' - ', 
	   CASE MONTH([FECHA BUSQUEDA]) 
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
 DAY([FECHA BUSQUEDA]) AS 'DIA FECHA BUSQUEDA',
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM CTE_CONSULTAS_ORDENAMIENTO
WHERE
  CAST([FECHA BUSQUEDA] AS DATE) BETWEEN CAST(DATEADD(m,-90,GETDATE()) AS DATE) AND CAST(GETDATE() AS DATE)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a consumo analítico/BI que consolida, para los últimos 90 días, las órdenes de medicamentos formulados en hospitalización junto con su dispensación efectiva por farmacia. Cruza las fórmulas médicas (órdenes y detalle de productos) con la dispensación confirmada, clasificación ATC, grupo farmacológico, datos del paciente, profesional ordenador, entidad aseguradora, centro y unidad funcional. Incluye columnas de partición temporal (año, mes, día) y marca de última actualización para facilitar el consumo desde herramientas de reporting.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicamentosAplicados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicamentosAplicados';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida los medicamentos ordenados a pacientes ingresados con su dispensación farmacéutica y datos de aplicación clínica, restringido a los últimos 90 meses según la fecha de búsqueda.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicamentosAplicados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los productos de IHLISTPRO deben tener TIPPRODUC=''1'' (medicamento) para ser incluidos.; Las órdenes de HCFARMEPC deben tener ORDESTADO distinto de ''3'' (no anuladas).; Solo se consideran dispensaciones con Status=2 (confirmadas) en Inventory.PharmaceuticalDispensing.; El producto ordenado debe existir en Inventory.ATC vinculado por código y tener grupo farmacológico asociado vía ATCEntity.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicamentosAplicados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo incluye órdenes farmacéuticas activas (ORDESTADO <> ''3'').; Solo considera dispensaciones farmacéuticas con Status=2.; Solo productos clasificados como medicamento (TIPPRODUC=''1'').; El rango temporal está acotado a los últimos 90 meses respecto a GETDATE().; ID_COMPANY se obtiene del nombre de la base de datos actual truncado a 9 caracteres.; ULT_ACTUAL se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; Si no hay dispensación asociada, la fecha de búsqueda se basa en la fecha de la orden, garantizando que toda orden activa aparezca.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicamentosAplicados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Fórmula médica; Medicamento; Clasificación ATC; Grupo farmacológico; Centro de atención; Unidad funcional; Paciente; Ingreso hospitalario; Profesional de la salud; Entidad administradora de salud; Aplicación de medicamento (hoja de medicación); Presentación del medicamento; Cantidad solicitada vs entregada', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicamentosAplicados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewMedicamentosAplicados: Devuelve filas de medicamentos ordenados (HCFARMEPC/HCFARMEPD) cruzadas con su dispensación confirmada (Status=2) y registro de aplicación en HCHOJAMED, con cantidad entregada=0 si no hay dispensación, filtrando por FECHA BUSQUEDA en los últimos 90 meses.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicamentosAplicados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DIS.[FECHA ENTREGA] IS NULL en el LEFT JOIN con DISPENSACION → FECHA BUSQUEDA toma el valor de C.FECHAORDE (fecha de la orden) else FECHA BUSQUEDA toma el valor de DIS.[FECHA ENTREGA] (fecha de dispensación confirmada); si DIS.[CANTIDAD ENTREGADA] IS NULL → Se reporta CANTIDAD ENTREGADA = 0 else Se reporta la cantidad efectivamente dispensada; si MONTH([FECHA BUSQUEDA]) entre 1 y 12 → Se traduce el mes a su nombre en español (ENERO..DICIEMBRE) concatenado con el número de mes formateado a dos dígitos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicamentosAplicados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Inventory.InventoryProduct; Inventory.ATC; Inventory.ATCEntity; Inventory.PharmacologicalGroup; HCFARMEPC; HCFARMEPD; IHLISTPRO; INPROFSAL; ADCENATEN; INUNIFUNC; INPACIENT; ADINGRESO; Contract.HealthAdministrator; IHFORMEDI; HCHOJAMED', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicamentosAplicados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicamentosAplicados';
GO
