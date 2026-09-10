

/*******************************************************************************************************************
Nombre: [Report].[ViewResolution7080]
Tipo:Vista
Observacion:Resolucion 7080 del 2013 
Profesional: Amira Esperanza Gil Meneses
Fecha:
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 1
Persona que modifico:
Fecha:
Ovservaciones:
--------------------------------------
Version 2
Persona que modifico:
Fecha:
***********************************************************************************************************************************/

--	DROP VIEW [Report].[ViewResolution7080]

CREATE VIEW [Report].[ViewResolution0780] AS

WITH DISPENSACION
AS
(
SELECT AdmissionNumber 'INGRESO',
      C.ConfirmationDate 'FECHA ENTREGA',
	  ATC.Code 'CODIGO MEDICAMENTO',
	  P.Code 'CUM',
	  D.Quantity 'CANTIDAD ENTREGADA',
	  D.EntityId 'IDFORMULA',
	  D.SalePrice 'Valor unitario', 
	  D.GrandTotalSalesPrice 'Total'

FROM Inventory .PharmaceuticalDispensing AS C
JOIN Inventory .PharmaceuticalDispensingDetail AS D ON C.Id =D.PharmaceuticalDispensingId 
JOIN Inventory .InventoryProduct AS P ON  D.ProductId =P.ID 
JOIN Inventory .ATC AS ATC ON P.ATCId =ATC.Id  
WHERE C.Status =2 --Estado (registrado = 1,confirmado = 2,anulado = 3)--
)
,
CTE_DIAS_TRATAMIENTO AS 
(
select 
A.NUMINGRES,
A.CODPRODUC,
DATEDIFF(DAY,A.FECINITRA,B.FECAPLMED) AS [DIAS TRATAMIENTO]
from 
dbo.HCHOJAMED A INNER JOIN
dbo.HCHOJAMED B ON A.CONSECUTI=B.CONSECUTI AND A.CONSECUTI=(SELECT MIN(CON.CONSECUTI) FROM dbo.HCHOJAMED CON WHERE A.NUMINGRES=CON.NUMINGRES AND A.CODPRODUC=CON.CODPRODUC)
                                           AND B.CONSECUTI=(SELECT MAX(CON.CONSECUTI) FROM dbo.HCHOJAMED CON WHERE B.NUMINGRES=CON.NUMINGRES AND B.CODPRODUC=CON.CODPRODUC AND CON.FECAPLMED IS NOT NULL)                                           
)

SELECT
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
TP.Nit As [Nit de la IPS],
CEN.CODIPSSEC As [Código habilitación],
CEN.NOMCENATE 'CENTRO ATENCION',
 HA.Code 'COD_ENTIDAD',
 HA.Name 'ENTIDAD',
 CASE PAC.IPTIPODOC WHEN '1'  THEN 'CC'
				 WHEN '2'  THEN 'CE'
				 WHEN '3'  THEN 'TI'
				 WHEN '4'  THEN 'RC' 
				 WHEN '5'  THEN 'PA'
				 WHEN '6'  THEN 'AS'
				 WHEN '7'  THEN 'MS'
				 WHEN '8'  THEN 'NU'
				 WHEN '9'  THEN 'CN'
				 WHEN '10' THEN 'CD'
				 WHEN '11' THEN 'SA'
				 WHEN '12' THEN 'PE' 
				 WHEN '13' THEN 'PT'
				 WHEN '14' THEN 'DE'
				 WHEN '15' THEN 'SI'
				 END AS [TIPO IDENTIFICACION],
C.IPCODPACI 'IDENTIFICACION',
PAC.IPPRIAPEL 'PRIMER APELLIDO' , 
PAC.IPSEGAPEL 'SEGUNDO APELLIDO',
PAC.IPPRINOMB 'PRIMER NOMBRE',
PAC.IPSEGNOMB 'SEGUNDO NOMBRE',
DIS.[CUM],
PRO.DESPRODUC 'MEDICAMENTO' , 
TRA.[DIAS TRATAMIENTO],
DIS.[FECHA ENTREGA],
CASE D.NOPOSPROD WHEN 'False' THEN 'SI' ELSE 'NO' END AS PBS,--Indica si el medicamento es  POS--
1 'CANTIDAD',
CAST(DIS.[FECHA ENTREGA] AS DATE) AS 'FECHA BUSQUEDA', 
YEAR(DIS.[FECHA ENTREGA]) AS 'AÑO FECHA BUSQUEDA', 
MONTH(DIS.[FECHA ENTREGA]) AS 'MES AÑO FECHA BUSQUEDA',
CASE MONTH(DIS.[FECHA ENTREGA])WHEN 1THEN 'ENERO'
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
DAY(DIS.[FECHA ENTREGA]) AS 'DIA FECHA BUSQUEDA',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM HCFARMEPC AS C JOIN
HCFARMEPD AS D ON C.CODCONCEC=D.CODCONCEC AND C.ORDESTADO <>'3' JOIN
IHLISTPRO AS PRO ON D.CODPRODUC=PRO.CODPRODUC AND PRO.TIPPRODUC ='1' JOIN 
INPROFSAL AS MED ON D.CODPROSAL =MED.CODPROSAL JOIN 
ADCENATEN AS CEN ON C.CODCENATE =CEN.CODCENATE 
JOIN INUNIFUNC AS UNI ON C.UFUCODIGO =UNI.UFUCODIGO 
JOIN INPACIENT AS PAC ON C.IPCODPACI =PAC.IPCODPACI
JOIN ADINGRESO AS ING ON C.NUMINGRES =ING.NUMINGRES 
JOIN Contract.HealthAdministrator AS HA ON ING.GENCONENTITY =HA.Id 
JOIN Common.ThirdParty AS TP ON HA.ThirdPartyId =TP.Id
JOIN Inventory.ATC AS ATC ON D.CODPRODUC =ATC.Code 
JOIN Inventory.ATCEntity AS ATCE ON ATC.ATCEntityId =ATCE.Id 
JOIN Inventory.PharmacologicalGroup AS PG ON PG.Id =ATCE.IdPharmacologicalGroup 
LEFT JOIN IHFORMEDI AS PRE ON PRO.CODFORMED =PRE.CODFORMED 
LEFT JOIN DISPENSACION AS DIS ON D.ID =DIS.IDFORMULA AND D.NUMINGRES =DIS.INGRESO
LEFT JOIN CTE_DIAS_TRATAMIENTO AS TRA ON D.NUMINGRES=TRA.NUMINGRES AND D.CODPRODUC=TRA.CODPRODUC
WHERE D.NOPOSPROD = 0
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte para la Resolución 7080 de 2013 que consolida la dispensación de medicamentos NO-PBS (no incluidos en el Plan de Beneficios en Salud) entregados a pacientes hospitalizados. Cruza datos de fórmulas médicas, dispensación confirmada en farmacia, clasificación ATC, entidad pagadora (EPS/ARS), identificación del paciente y días de tratamiento calculados desde la hoja de medicación. Está orientada a reportes regulatorios con desagregación por fecha, mes y año de entrega, incluyendo marca de última actualización.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolution0780';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolution0780';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte regulatorio Resolución 0780/7080 de 2013 con la entrega de medicamentos NO POS dispensados a pacientes, incluyendo datos de identificación, entidad responsable, días de tratamiento y fecha de entrega.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolution0780';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las dispensaciones farmacéuticas deben estar en estado confirmado (PharmaceuticalDispensing.Status = 2) para considerarse entregadas.; Las prescripciones de farmacia (HCFARMEPC) no deben estar anuladas (ORDESTADO <> ''3'').; Solo se incluyen productos de tipo medicamento (IHLISTPRO.TIPPRODUC = ''1'').; El producto debe estar marcado como NO POS (HCFARMEPD.NOPOSPROD = 0) para entrar al reporte.; Cada prescripción debe tener producto, profesional, centro de atención, unidad funcional, paciente, ingreso y entidad de salud asociados (joins INNER).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolution0780';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan medicamentos clasificados como NO POS (NOPOSPROD = 0 en el detalle de prescripción).; Los días de tratamiento se calculan como DATEDIFF(DAY, primera FECINITRA, última FECAPLMED) por combinación ingreso+producto, tomando el menor y mayor consecutivo de la hoja de medicación con FECAPLMED no nula.; La cantidad reportada por fila siempre es 1 (constante literal), independiente de la cantidad realmente dispensada.; La fecha de entrega proviene exclusivamente de dispensaciones confirmadas (Status=2); si no hay match, queda NULL por LEFT JOIN.; El timestamp de actualización se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; El ID_COMPANY corresponde al nombre de la base de datos truncado a 9 caracteres.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolution0780';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Resolución 0780/7080 de 2013; Medicamento NO POS / PBS; Dispensación farmacéutica; Hoja de medicación; Días de tratamiento; Código CUM; Clasificación ATC; Grupo farmacológico; Tipo de identificación del paciente; EPS / Administradora de salud; Centro de atención e IPS (código de habilitación); Ingreso hospitalario', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolution0780';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewResolution0780: Devuelve una fila por detalle de prescripción de medicamento NO POS, con CUM, días de tratamiento, fecha de entrega y desglose por año/mes/día, además de identificación del paciente y la EPS/aseguradora.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolution0780';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PAC.IPTIPODOC entre ''1'' y ''15'' → Mapea el código numérico al tipo de identificación textual (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SA, PE, PT, DE, SI). else Devuelve NULL en TIPO IDENTIFICACION.; si D.NOPOSPROD = ''False'' → Marca PBS = ''SI'' (medicamento POS). else Marca PBS = ''NO'' (no POS).; si MONTH(DIS.[FECHA ENTREGA]) entre 1 y 12 → Traduce el número de mes a su nombre en español (ENERO..DICIEMBRE).; si PharmaceuticalDispensing.Status = 2 → Solo se incluyen dispensaciones confirmadas en el CTE DISPENSACION. else Las dispensaciones registradas (1) o anuladas (3) se excluyen.; si HCFARMEPC.ORDESTADO <> ''3'' → Incluye la prescripción en el reporte. else Excluye prescripciones anuladas.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolution0780';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Inventory.InventoryProduct; Inventory.ATC; Inventory.ATCEntity; Inventory.PharmacologicalGroup; dbo.HCHOJAMED; dbo.HCFARMEPC; dbo.HCFARMEPD; dbo.IHLISTPRO; dbo.INPROFSAL; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPACIENT; dbo.ADINGRESO; Contract.HealthAdministrator; Common.ThirdParty; dbo.IHFORMEDI', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolution0780';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewResolution0780';
GO
