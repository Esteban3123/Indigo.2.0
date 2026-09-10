
/*******************************************************************************************************************
Nombre: [Report].[ViewMedicamentosDevoluciones]
Tipo:Vista
Observacion:Informe de devoluciones de indumos y medicamentos a farmacia.
Profesional: 
Fecha:
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 2
Persona que modifico: Nilsson Galindo Lopez
Fecha:14-06-2023
Ovservaciones: Se agrega el campo de gupo farmacologico a los medicamentos, esto solicitado por medio del ticket 1043
--------------------------------------
Version 3
Persona que modifico:
Fecha:
***********************************************************************************************************************************/

CREATE VIEW [Report].[ViewMedicamentosDevoluciones] as

WITH CTE_MEDICAMENTO as (
SELECT 
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
PD.Code DOCUMENTO,CAST(PD.DocumentDate AS date) FECHA_DOCUMENTO,PD.ConfirmationDate FECHA_CONFIRMACION,CASE PD.Status WHEN 1 THEN 'REGISTRADO' WHEN 2 THEN 'CONFIRMADO'
WHEN 3 THEN 'ANULADO' END ESTADO,PD.Code DOCUMENTO_DISPENSACION,IIF(pd.AffectInventory = 0, 'NO', 'SI') AFECTA_INVENTARIOS ,PD.EntityName ORIGEN,'Resta' OPERACION,
CEN.NOMCENATE AS CENTRO_ATENCION,FU.Name AS [UNIDAD FUNCIONAL],PD.AdmissionNumber INGRESO,ING.IFECHAING as [FECHA INGRESO],PAC.IPCODPACI AS IDENTIFICACION,PAC.IPNOMCOMP AS NOMBRE_PACIENTE,
TP2.Nit NIT,HA.Name AS ENTIDAD,CG.Code + ' - ' + CG.Name AS GRUPO_ATENCION,CASE PT.Class WHEN 2 THEN 'MEDICAMENTO' WHEN 3 THEN 'INSUMO' ELSE 'OTRO' END TIPO_PRODUCTO ,
ISNULL(ATC.Code,ISS.Code ) AS CODIGO_PADRE,ISNULL(ATC.Name,ISS.SupplieName ) AS NOMBRE_PADRE,IP.Code AS CODIGO_PRODUCTO,IP.CodeCUM AS CUM,IP.CodeAlternative [CODIGO ALT1] ,IP.CodeAlternativeTwo [CODIGO ALT2],
IP.Name AS NOMBRE_PRODUCTO,
/*IN V2*/FAR.[Name] AS [GRUPO FARMACOLOGICO],/*FN V2*/
ISNULL(ATC.Weight,ATC.Volume ) DOSIS,
ISNULL(IMU.Name ,IMU2.Name ) [UNIDAD]  ,PF.Name [PRESENTACION] ,
CASE ATC.POSProduct WHEN 1 THEN 'SI' ELSE 'NO' END PRODUCTO_POS ,case when ATC.Conditioned=1 then 'SI' ELSE 'NO' END CONDICIONADO ,
CASE WHEN atc.UNIRS=1 THEN 'SI' ELSE 'NO' END UNIRS ,
WH.Code + '-' + WH.Name AS ALMACEN ,PDDBS.Quantity [CANTIDAD] ,G.Quantity   CANTIDAD_DEVUELTA,PDD.AverageCost [COSTO PROMEDIO],
cast(PDD.TotalSalesPrice as numeric ) PRECIO_UNITARIO,(PDDBS.Quantity * PDD.TotalSalesPrice)  PRECIO_TOTAL ,
CAST(pdd.ServiceDate AS date) AS FECHA_SOLICITUD,
BS .BatchCode AS LOTE ,BS.ExpirationDate AS FECHA_VENCIMIENTO, IP.HealthRegistration AS REGISTRO_SANITARIO,IP.ExpirationDate AS FECHA_VENCIMIENTO_REGISTRO,
--TP.Nit +'-'+TP.Name 
'' AS PROFESIONAL,
isnull(SOD.AuthorizationNumber,ING.IAUTORIZA)  AS AUTORIZACION,
--PER.Identification +'-'+PER.Fullname 
'' USUARIO_REGISTRO,
ISNULL(ING.CODDIAEGR + ' - ' + DX2.NOMDIAGNO , ING.CODDIAING + ' - ' + DX.NOMDIAGNO ) [DIAGNOSTICO],'DISPENSACIONES' [TIPO DOCUMENTO],
 CAST(PD.DocumentDate  AS DATE) AS 'FECHA BUSQUEDA', 
 YEAR(PD.DocumentDate ) AS 'AÑO FECHA BUSQUEDA', 
 MONTH(PD.DocumentDate ) AS 'MES AÑO FECHA BUSQUEDA',
 CASE MONTH(PD.DocumentDate )WHEN 1THEN 'ENERO'
                         WHEN 2
                         THEN 'FEBRERO'
                         WHEN 3
                         THEN 'MARZO'
                         WHEN 4
                         THEN 'ABRIL'
                         WHEN 5
                         THEN 'MAYO'
                         WHEN 6
                         THEN 'JUNIO'
                         WHEN 7
                         THEN 'JULIO'
                         WHEN 8
                         THEN 'AGOSTO'
                         WHEN 9
                         THEN 'SEPTIEMBRE'
                         WHEN 10
                         THEN 'OCTUBRE'
                         WHEN 11
                         THEN 'NOVIEMBRE'
                         WHEN 12
                         THEN 'DICIEMBRE'
                     END AS 'MES NOMBRE FECHA BUSQUEDA', 
 DAY(PD.DocumentDate ) AS 'DIA FECHA BUSQUEDA'
from Inventory .PharmaceuticalDispensing PD JOIN
dbo.ADINGRESO AS ING WITH (NOLOCK) ON ING.NUMINGRES =PD.AdmissionNumber JOIN
dbo.INPACIENT AS PAC WITH (NOLOCK) ON PAC.IPCODPACI =ING.IPCODPACI  JOIN
dbo.ADCENATEN AS CEN WITH (NOLOCK) ON CEN.CODCENATE =ING.CODCENATE JOIN
Inventory .PharmaceuticalDispensingDetail as PDD WITH (NOLOCK) ON PDD.PharmaceuticalDispensingId =PD.Id JOIN
Inventory .InventoryProduct AS IP WITH (NOLOCK) ON IP.Id =PDD.ProductId JOIN
Inventory .ProductType AS PT WITH (NOLOCK) ON PT.ID =IP.ProductTypeId  JOIN
Inventory .Warehouse AS WH WITH (NOLOCK) ON WH.Id =PDD.WarehouseId JOIN
Payroll .FunctionalUnit AS FU WITH (NOLOCK) ON FU.Id =PDD.FunctionalUnitId JOIN
Billing .ServiceOrder as SO WITH (NOLOCK) ON PD.Id =SO.EntityId and PD.AdmissionNumber =SO.AdmissionNumber AND SO.Status =1 JOIN
Billing .ServiceOrderDetail AS SOD WITH (NOLOCK) ON SO.Id =SOD.ServiceOrderId AND SOD.ProductId =PDD.ProductId AND SOD.IsDelete =0 and SOD.SupplyQuantity  =PDD.Quantity  JOIN
--INDIGOSEC .Security .[User] AS US ON US.UserCode =PD.CreationUser JOIN
--INDIGOSEC .Security .Person AS PER ON PER.ID=US.IdPerson  JOIN
Common .ThirdParty AS TP WITH (NOLOCK) ON TP.Id =PDD.OrderedHealthProfessionalThirdPartyId LEFT JOIN
Inventory .ATC AS ATC WITH (NOLOCK) ON ATC.ID =IP.ATCId LEFT JOIN
Inventory .InventoryMeasurementUnit AS IMU WITH (NOLOCK) ON ATC.WeightMeasureUnit =IMU.Id  LEFT JOIN
Inventory .InventoryMeasurementUnit AS IMU2 WITH (NOLOCK) ON ATC.VolumeMeasureUnit  =IMU2.Id LEFT JOIN
Inventory .PharmaceuticalForm PF WITH (NOLOCK) ON ATC.PharmaceuticalFormId =PF.Id  LEFT JOIN
Inventory .InventorySupplie AS ISS WITH (NOLOCK) ON ISS.ID =IP.SupplieId LEFT JOIN
Contract .CareGroup AS CG WITH (NOLOCK) ON CG.Id =PDD.CareGroupId LEFT JOIN
Contract .HealthAdministrator AS HA WITH (NOLOCK) ON HA.Id =PDD.HealthAdministratorId LEFT JOIN 
Common .ThirdParty AS TP2 WITH (NOLOCK) ON TP2.Id =HA.ThirdPartyId  LEFT JOIN
Inventory.PharmaceuticalDispensingDetailBatchSerial AS PDDBS WITH (NOLOCK) ON PDD.ID= PDDBS.PharmaceuticalDispensingDetailId LEFT JOIN 
Inventory.PhysicalInventory PI WITH (NOLOCK) ON PDDBS.PhysicalInventoryId = PI.Id LEFT JOIN
Inventory .BatchSerial AS BS WITH (NOLOCK) ON BS.Id =PI.BatchSerialId LEFT JOIN
dbo.INDIAGNOS AS DX WITH (NOLOCK) ON ING.CODDIAING =DX.CODDIAGNO LEFT JOIN
dbo.INDIAGNOS AS DX2 WITH (NOLOCK) ON ING.CODDIAEGR  =DX2.CODDIAGNO LEFT JOIN
/*IN V2*/Inventory.PharmacologicalGroup FAR ON ATC.PharmacologicalGroupId=FAR.Id LEFT JOIN /*FN V2*/
(SELECT  PharmaceuticalDispensingDetailBatchSerialId, SUM(Quantity) Quantity 
FROM
Inventory.PharmaceuticalDispensingDevolutionDetail GROUP BY PharmaceuticalDispensingDetailBatchSerialId) AS G ON G.PharmaceuticalDispensingDetailBatchSerialId =  PDDBS.Id 
group by PD.Code,PD.DocumentDate,PD.ConfirmationDate, PD.Status ,pd.AffectInventory,PD.EntityName,CEN.NOMCENATE,FU.Name,PD.AdmissionNumber,ING.IFECHAING ,PAC.IPCODPACI,PAC.IPNOMCOMP,
TP2.Nit,HA.Name,CG.Code,CG.Name,PT.Class,ISNULL(ATC.Code,ISS.Code ),ISNULL(ATC.Name,ISS.SupplieName ),IP.Code,IP.CodeCUM,IP.CodeAlternative,IP.CodeAlternativeTwo,
IP.Name,ATC.POSProduct,ATC.Conditioned,atc.UNIRS,WH.Code + '-' + WH.Name,PDDBS.Quantity,G.Quantity,PDD.AverageCost,cast(PDD.TotalSalesPrice as numeric ),PDDBS.Quantity * PDD.TotalSalesPrice,CAST(pdd.ServiceDate AS date),FU.Name,
BS .BatchCode,BS.ExpirationDate, IP.HealthRegistration,IP.ExpirationDate,TP.Nit +'-'+TP.Name,isnull(SOD.AuthorizationNumber,ING.IAUTORIZA),ING.IAUTORIZA,
--PER.Identification +'-'+PER.Fullname,
SOD.InvoicedQuantity ,SOD.SupplyQuantity,
ISNULL(ING.CODDIAEGR + ' - ' + DX2.NOMDIAGNO , ING.CODDIAING + ' - ' + DX.NOMDIAGNO ),ISNULL(ATC.Weight,ATC.Volume ) ,ISNULL(IMU.Name ,IMU2.Name ),PF.Name,FAR.Name
UNION ALL
SELECT	
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
pddev.Code DOCUMENTO_DEVOLUCION,pddev.DocumentDate FECHA_DEVOLUCION,pddev.ConfirmationDate FECHA_CONFIRMACION,CASE pddev.Status WHEN 1 THEN 'REGISTRADO' WHEN 2 THEN 'CONFIRMADO' WHEN 3 THEN 'ANULADO' END ESTADO,
pd.Code DOCUMENTO_DISPENSACION,'SI' AFECTA_INVENTARIOS,pddev.EntityName ORIGEN,'Suma' OPERACION,CEN.NOMCENATE CENTRO_ATENCION,FU.Name AS [UNIDAD FUNCIONAL],pddev.AdmissionNumber INGRESO,ING.IFECHAING as [FECHA INGRESO],PAC.IPCODPACI AS IDENTIFICACION,PAC.IPNOMCOMP AS NOMBRE_PACIENTE,
TP2.Nit NIT,HA.Name AS ENTIDAD,CG.Name AS GRUPO_ATENCION,CASE PT.Class WHEN 2 THEN 'MEDICAMENTO' WHEN 3 THEN 'INSUMO' ELSE 'OTRO' END TIPO_PRODUCTO ,
ISNULL(ATC.Code,ISS.Code ) AS CODIGO_PADRE,ISNULL(ATC.Name,ISS.SupplieName ) AS NOMBRE_PADRE,IP.Code AS CODIGO_PRODUCTO,IP.CodeCUM AS CUM,IP.CodeAlternative [CODIGO ALT1] ,IP.CodeAlternativeTwo [CODIGO ALT2],
IP.Name AS NOMBRE_PRODUCTO,
/*IN V2*/FAR.[Name] AS [GRUPO FARMACOLOGICO],/*FN V2*/
ISNULL(ATC.Weight,ATC.Volume ) DOSIS,
ISNULL(IMU.Name ,IMU2.Name ) [UNIDAD]  ,PF.Name [PRESENTACION],
CASE ATC.POSProduct WHEN 1 THEN 'SI' ELSE 'NO' END PRODUCTO_POS ,case when ATC.Conditioned=1 then 'SI' ELSE 'NO' END CONDICIONADO ,
CASE WHEN atc.UNIRS=1 THEN 'SI' ELSE 'NO' END UNIRS,CONCAT(w.Code, ' - ', w.Name) ALMACEN,pdd.Quantity CANTIDAD,pddevd.Quantity CANTIDAD_DEVUELTA,pdd.AverageCost [COSTO PROMEDIO] ,
cast(PDD.TotalSalesPrice as numeric ) PRECIO_UNITARIO,pddevd.Quantity * cast(PDD.TotalSalesPrice as numeric )  PRECIO_TOTAL,CAST(pdd.ServiceDate AS date) AS FECHA_SOLICITUD,
bs.BatchCode [LOTE],bs.ExpirationDate [FECHA VENCIMIENTO],
IP.HealthRegistration AS REGISTRO_SANITARIO,IP.ExpirationDate AS FECHA_VENCIMIENTO_REGISTRO,TP.Nit +'-'+TP.Name AS PROFESIONAL,
PDD.AuthorizationNumber AS AUTORIZACION,
--PER.Identification +'-'+PER.Fullname
'' USUARIO_REGISTRO,
ISNULL(ING.CODDIAEGR + ' - ' + DX2.NOMDIAGNO , ING.CODDIAING + ' - ' + DX.NOMDIAGNO ) [DIAGNOSTICO],
'DEVOLUCIONES' [TIPO DOCUMENTO],
 CAST(pddev.DocumentDate  AS DATE) AS 'FECHA BUSQUEDA', 
 YEAR(pddev.DocumentDate ) AS 'AÑO FECHA BUSQUEDA', 
 MONTH(pddev.DocumentDate ) AS 'MES AÑO FECHA BUSQUEDA',
 CASE MONTH(pddev.DocumentDate )WHEN 1THEN 'ENERO'
                         WHEN 2
                         THEN 'FEBRERO'
                         WHEN 3
                         THEN 'MARZO'
                         WHEN 4
                         THEN 'ABRIL'
                         WHEN 5
                         THEN 'MAYO'
                         WHEN 6
                         THEN 'JUNIO'
                         WHEN 7
                         THEN 'JULIO'
                         WHEN 8
                         THEN 'AGOSTO'
                         WHEN 9
                         THEN 'SEPTIEMBRE'
                         WHEN 10
                         THEN 'OCTUBRE'
                         WHEN 11
                         THEN 'NOVIEMBRE'
                         WHEN 12
                         THEN 'DICIEMBRE'
                     END AS 'MES NOMBRE FECHA BUSQUEDA', 
 DAY(pddev.DocumentDate ) AS 'DIA FECHA BUSQUEDA'
FROM Inventory.PharmaceuticalDispensingDevolution pddev WITH (NOLOCK) 
JOIN dbo.ADINGRESO AS ING WITH (NOLOCK) ON ING.NUMINGRES =pddev.AdmissionNumber 
JOIN dbo.INPACIENT AS PAC WITH (NOLOCK) ON PAC.IPCODPACI =ING.IPCODPACI  
JOIN dbo.ADCENATEN AS CEN WITH (NOLOCK) ON CEN.CODCENATE =ING.CODCENATE
JOIN Inventory.PharmaceuticalDispensingDevolutionDetail pddevd WITH (NOLOCK) ON pddev.Id = pddevd.PharmaceuticalDispensingDevolutionId
JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs WITH (NOLOCK) ON pddevd.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK) ON pddbs.PharmaceuticalDispensingDetailId = pdd.Id
JOIN Inventory.PharmaceuticalDispensing pd WITH (NOLOCK) ON pdd.PharmaceuticalDispensingId = pd.Id
JOIN Inventory.Warehouse w WITH (NOLOCK) ON pdd.WarehouseId = w.Id
JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON pdd.ProductId = ip.Id
JOIN Inventory .ProductType AS PT WITH (NOLOCK) ON PT.ID =IP.ProductTypeId 
--JOIN INDIGOSEC .Security .[User] AS US ON US.UserCode =pddev.CreationUser 
--JOIN INDIGOSEC .Security .Person AS PER ON PER.ID=US.IdPerson  
JOIN Payroll .FunctionalUnit AS FU WITH (NOLOCK) ON FU.Id =PDD.FunctionalUnitId
JOIN Common .ThirdParty AS TP WITH (NOLOCK) ON TP.Id =PDD.OrderedHealthProfessionalThirdPartyId 
LEFT JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON pddbs.PhysicalInventoryId = phy.Id
LEFT JOIN Inventory .ATC AS ATC WITH (NOLOCK) ON ATC.ID =IP.ATCId 
LEFT JOIN Inventory .InventoryMeasurementUnit AS IMU WITH (NOLOCK) ON ATC.WeightMeasureUnit =IMU.Id  
LEFT JOIN Inventory .InventoryMeasurementUnit AS IMU2 WITH (NOLOCK) ON ATC.VolumeMeasureUnit  =IMU2.Id
LEFT JOIN Inventory .PharmaceuticalForm PF WITH (NOLOCK) ON ATC.PharmaceuticalFormId =PF.Id 
LEFT JOIN Inventory .InventorySupplie AS ISS WITH (NOLOCK) ON ISS.ID =IP.SupplieId
LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON phy.BatchSerialId = bs.Id
LEFT JOIN Contract .CareGroup AS CG WITH (NOLOCK) ON CG.Id =ING.GENCAREGROUP   --=PDD.CareGroupId 
LEFT JOIN Contract .HealthAdministrator AS HA WITH (NOLOCK) ON HA.Id =ING.GENCONENTITY  --=PDD.HealthAdministratorId 
LEFT JOIN Common .ThirdParty AS TP2 WITH (NOLOCK) ON TP2.Id =HA.ThirdPartyId 
LEFT JOIN dbo.INDIAGNOS AS DX WITH (NOLOCK) ON ING.CODDIAING =DX.CODDIAGNO 
LEFT JOIN dbo.INDIAGNOS AS DX2 WITH (NOLOCK) ON ING.CODDIAEGR  =DX2.CODDIAGNO /*IN V2*/ LEFT JOIN
Inventory.PharmacologicalGroup FAR ON ATC.PharmacologicalGroupId=FAR.Id /*FN V2*/
--WHERE CAST(pddev.DocumentDate AS DATE)  BETWEEN @FECINI AND @FECFIN 
)

SELECT 
 *,
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM
 CTE_MEDICAMENTO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a consumo en herramientas de análisis e informes de devoluciones de medicamentos e insumos a farmacia. Combina mediante UNION ALL dos conjuntos de datos: las dispensaciones originales (operación "Resta") y sus devoluciones asociadas (operación "Suma"), aplanando información de paciente, ingreso hospitalario, producto (con clasificación ATC, grupo farmacológico, POS, UNIRS), lote, almacén, costos, precios y diagnóstico de egreso/ingreso. Permite auditar cantidades dispensadas vs. devueltas por centro de atención, unidad funcional, entidad pagadora y periodo calendario.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicamentosDevoluciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicamentosDevoluciones';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida en un único reporte las dispensaciones farmacéuticas y sus devoluciones de medicamentos e insumos, con datos de paciente, ingreso, almacén, lote, costos, autorización y diagnóstico para análisis de devoluciones a farmacia.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicamentosDevoluciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen documentos de dispensación en Inventory.PharmaceuticalDispensing con su detalle, lotes/seriales y orden de servicio asociada (Billing.ServiceOrder con Status=1).; Cada dispensación incluida debe tener una orden de servicio con SOD.IsDelete=0 y SOD.SupplyQuantity igual a PDD.Quantity.; El ingreso (ADINGRESO) y paciente (INPACIENT) referenciados deben existir para el AdmissionNumber de la dispensación o devolución.; Los productos deben estar registrados en Inventory.InventoryProduct con su tipo de producto (ProductType).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicamentosDevoluciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El bloque de dispensaciones solo incluye registros donde la orden de servicio asociada está activa (SO.Status=1) y el detalle no está borrado (SOD.IsDelete=0) y la cantidad suministrada coincide con la dispensada (SOD.SupplyQuantity = PDD.Quantity).; Las devoluciones siempre se reportan con AFECTA_INVENTARIOS=''SI'' y OPERACION=''Suma''; las dispensaciones siempre con OPERACION=''Resta''.; La cantidad devuelta del bloque de dispensaciones se obtiene agregando (SUM) Inventory.PharmaceuticalDispensingDevolutionDetail.Quantity por PharmaceuticalDispensingDetailBatchSerialId.; ID_COMPANY siempre se fija con DB_NAME() truncado a 9 caracteres.; USUARIO_REGISTRO se devuelve siempre vacío (la unión con seguridad/persona está comentada).; PROFESIONAL en el bloque de dispensaciones se devuelve siempre vacío; en devoluciones se concatena TP.Nit-TP.Name.; PRECIO_TOTAL en dispensaciones = PDDBS.Quantity * PDD.TotalSalesPrice; en devoluciones = pddevd.Quantity * PDD.TotalSalesPrice.; ULT_ACTUAL se calcula con la zona horaria ''Pakistan Standard Time'' (no con la zona local del servidor).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicamentosDevoluciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewMedicamentosDevoluciones: Retorna dos bloques unidos con UNION ALL: (1) líneas de dispensación marcadas con OPERACION=''Resta'' y TIPO DOCUMENTO=''DISPENSACIONES'', (2) líneas de devolución marcadas con OPERACION=''Suma'' y TIPO DOCUMENTO=''DEVOLUCIONES''; agrega columna ULT_ACTUAL con GETDATE() convertido a ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicamentosDevoluciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PD.Status / pddev.Status = 1, 2 o 3 → Se traduce a ESTADO ''REGISTRADO'', ''CONFIRMADO'' o ''ANULADO'' respectivamente.; si PD.AffectInventory = 0 → AFECTA_INVENTARIOS=''NO'' else AFECTA_INVENTARIOS=''SI'' (en bloque de devoluciones siempre se fija ''SI''); si PT.Class = 2 / 3 / otro → TIPO_PRODUCTO=''MEDICAMENTO'' / ''INSUMO'' / ''OTRO''.; si ATC.POSProduct = 1 → PRODUCTO_POS=''SI'' else PRODUCTO_POS=''NO''; si ATC.Conditioned = 1 → CONDICIONADO=''SI'' else CONDICIONADO=''NO''; si ATC.UNIRS = 1 → UNIRS=''SI'' else UNIRS=''NO''; si IP.ATCId tiene match en ATC → CODIGO_PADRE/NOMBRE_PADRE se toman de ATC; en otro caso se toman del insumo (InventorySupplie).; si ATC.Weight existe → DOSIS = ATC.Weight con unidad ATC.WeightMeasureUnit else DOSIS = ATC.Volume con unidad ATC.VolumeMeasureUnit; si ING.CODDIAEGR (diagnóstico de egreso) existe → DIAGNOSTICO se arma con el diagnóstico de egreso else DIAGNOSTICO se arma con el diagnóstico de ingreso (ING.CODDIAING); si Bloque dispensación: SOD.AuthorizationNumber existe → AUTORIZACION = SOD.AuthorizationNumber else AUTORIZACION = ING.IAUTORIZA; si Bloque devolución → AUTORIZACION = PDD.AuthorizationNumber y entidad/grupo de atención se toman del ingreso (ING.GENCONENTITY, ING.GENCAREGROUP) en lugar de PDD.; si MONTH(DocumentDate) entre 1 y 12 → Se traduce a nombre del mes en español (ENERO..DICIEMBRE) en columna ''MES NOMBRE FECHA BUSQUEDA''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicamentosDevoluciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicamentosDevoluciones';
GO
