

CREATE VIEW [Report].[UploadCubeVieSCMInventoryVsAccounting] AS

----************************************ EMPRESA: ODO  SCRIPT TOTAL MOVIMIENTOS *********************************---
--DECLARE @AÑO AS INT=2024;
--DECLARE @MES AS INT=6;

---****************************************************************************************************************************************-----
---************************************************DISPENSACIONES FARMACEUTICAS*************************************************************----
---*****************************************************************************************************************************************----

WITH CTE_KARDEX_DISPENSACIONES
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K
 WHERE K.EntityName ='PharmaceuticalDispensing' AND k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(HOUR, -1, COMMON.GETDATE())
 ),

CTE_DETALLE_KARDEX_DISPENSACIONES
AS
(
 SELECT 'DISPENSACION FARMACEUTICA' 'TRANSACCION',year(K.DocumentDate) 'AÑO',month(K.DocumentDate) 'MES',DAY(K.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE K.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE 
        WHEN VirtualStore = 1 THEN 'VIRTUAL' 
        WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
        WHEN CustodyStore = 1 THEN 'CUSTODIA'
        WHEN TransitStore = 1 THEN 'TRANSITO'
        WHEN ControlStore = 1 THEN 'CONTROL'
        WHEN WareHouseType = 1 THEN 'REMANENTE'
        ELSE 'NINGUNO'
    END AS 'TIPO ALMACEN',KOT.[ESTADO],
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.AverageCost) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.AverageCost) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_DISPENSACIONES AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='PharmaceuticalDispensing' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
),

CTE_CONTABILIDAD_DISPENSACIONES
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'DISPENSACION FARMACEUTICA' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO'
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_DISPENSACIONES AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='PharmaceuticalDispensing'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate
),

---****************************************************************************************************************************************-----
---************************************************DEVOLUCION DISPENSACIONES FARMACEUTICAS*************************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_DEVOLUCION_DISPENSACIONES
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K
 WHERE K.EntityName ='PharmaceuticalDispensingDevolution' AND k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(HOUR, -1, COMMON.GETDATE())
),

CTE_DETALLE_KARDEX_DEVOLUCION_DISPENSACIONES
AS
(
 SELECT 'DEVOLUCION DISPENSACION FARMACEUTICA' 'TRANSACCION',year(K.DocumentDate) 'AÑO',month(K.DocumentDate) 'MES',DAY(K.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE K.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE 
        WHEN VirtualStore = 1 THEN 'VIRTUAL' 
        WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
        WHEN CustodyStore = 1 THEN 'CUSTODIA'
        WHEN TransitStore = 1 THEN 'TRANSITO'
        WHEN ControlStore = 1 THEN 'CONTROL'
        WHEN WareHouseType = 1 THEN 'REMANENTE'
        ELSE 'NINGUNO'
    END AS 'TIPO ALMACEN',KOT.[ESTADO],
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_DEVOLUCION_DISPENSACIONES AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='PharmaceuticalDispensingDevolution' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
),

CTE_CONTABILIDAD_DEVOLUCION_DISPENSACIONES
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'DEVOLUCION DISPENSACION FARMACEUTICA' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC (20,2)))'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO'
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_DEVOLUCION_DISPENSACIONES AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='PharmaceuticalDispensingDevolution'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate
),

---****************************************************************************************************************************************-----
---************************************************AJUSTES DE INVENTARIOS ******************************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_AJUSTES
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K
 WHERE K.EntityName ='InventoryAdjustment' AND k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(HOUR, -1, COMMON.GETDATE())-- AND K.EntityCode='0001868764'
),

CTE_DETALLE_KARDEX_AJUSTES
AS
(
 SELECT 'AJUSTE DE INVENTARIOS' 'TRANSACCION',year(K.DocumentDate) 'AÑO',month(K.DocumentDate) 'MES',DAY(K.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE K.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE 
        WHEN VirtualStore = 1 THEN 'VIRTUAL' 
        WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
        WHEN CustodyStore = 1 THEN 'CUSTODIA'
        WHEN TransitStore = 1 THEN 'TRANSITO'
        WHEN ControlStore = 1 THEN 'CONTROL'
        WHEN WareHouseType = 1 THEN 'REMANENTE'
        ELSE 'NINGUNO'
    END AS 'TIPO ALMACEN',KOT.[ESTADO],
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.AverageCost) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.AverageCost) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_AJUSTES AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='InventoryAdjustment' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
),

CTE_CONTABILIDAD_AJUSTES
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'AJUSTE DE INVENTARIOS' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO'
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_AJUSTES AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='InventoryAdjustment'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate
),

---****************************************************************************************************************************************-----
---************************************************* PRESTAMOS DE MERCANCIAS ***************************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_PRESTAMOS
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K
 WHERE K.EntityName ='LoanMerchandise' AND k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(HOUR, -1, COMMON.GETDATE()) -- AND K.EntityCode='0001868764'
),

CTE_DETALLE_KARDEX_PRESTAMOS
AS
(
 SELECT 'PRESTAMOS DE MERCANCIAS' 'TRANSACCION',year(K.DocumentDate) 'AÑO',month(K.DocumentDate) 'MES',DAY(K.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE K.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE 
        WHEN VirtualStore = 1 THEN 'VIRTUAL' 
        WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
        WHEN CustodyStore = 1 THEN 'CUSTODIA'
        WHEN TransitStore = 1 THEN 'TRANSITO'
        WHEN ControlStore = 1 THEN 'CONTROL'
        WHEN WareHouseType = 1 THEN 'REMANENTE'
        ELSE 'NINGUNO'
    END AS 'TIPO ALMACEN',KOT.[ESTADO],
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.AverageCost) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.AverageCost) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_PRESTAMOS AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='LoanMerchandise' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
),

CTE_CONTABILIDAD_PRESTAMOS
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'PRESTAMOS DE MERCANCIAS' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO'
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_PRESTAMOS AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='LoanMerchandise'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate
),

---****************************************************************************************************************************************-----
---************************************************DEVOLUCION DE PRESTAMOS DE MERCANCIAS****************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_DEVOLUCION_PRESTAMOS
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,k.DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K
 WHERE K.EntityName ='LoanMerchandiseDevolution' AND k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(HOUR, -1, COMMON.GETDATE()) -- AND K.EntityCode='PMA0000001923'
),

CTE_DETALLE_KARDEX_DEVOLUCION_PRESTAMOS
AS
(
 SELECT 'DEVOLUCION PRESTAMOS DE MERCANCIAS' 'TRANSACCION',year(K.DocumentDate) 'AÑO',month(K.DocumentDate) 'MES',DAY(K.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE K.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE 
        WHEN VirtualStore = 1 THEN 'VIRTUAL' 
        WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
        WHEN CustodyStore = 1 THEN 'CUSTODIA'
        WHEN TransitStore = 1 THEN 'TRANSITO'
        WHEN ControlStore = 1 THEN 'CONTROL'
        WHEN WareHouseType = 1 THEN 'REMANENTE'
        ELSE 'NINGUNO'
    END AS 'TIPO ALMACEN',KOT.[ESTADO],
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_DEVOLUCION_PRESTAMOS AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='LoanMerchandiseDevolution' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
),

CTE_CONTABILIDAD_DEVOLUCION_PRESTAMOS
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO', 'DEVOLUCION PRESTAMOS DE MERCANCIAS' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO'
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_DEVOLUCION_PRESTAMOS AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='LoanMerchandiseDevolution'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate
),

---****************************************************************************************************************************************-----
---*************************************************** ORDENES DE TRASLADO *****************************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_ORDENES_TRASLADO
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,CASE TFO.OrderType WHEN 1 THEN 'Traslado' WHEN 2 THEN 'Consumo' WHEN 3 THEN 'Traslado en Transito' END 'TIPO ORDEN',
CASE TFO.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Entregado' WHEN 3 THEN 'Anulado' WHEN 4 THEN 'En Transito' END 'ESTADO'
 FROM Inventory .Kardex K
 INNER JOIN Inventory .TransferOrder TFO ON TFO.ID=K.EntityId AND TFO.CODE=K.EntityCode
 WHERE K.EntityName ='TransferOrder' AND TFO.Status<> 3 AND k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(HOUR, -1, COMMON.GETDATE()) -- AND K.EntityCode='0001868764'
),

CTE_DETALLE_KARDEX_ORDENES_TRASLADO
AS
(
 SELECT 'ORDENES DE TRASLADO' 'TRANSACCION',year(K.DocumentDate) 'AÑO',month(K.DocumentDate) 'MES',DAY(K.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE K.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE 
        WHEN VirtualStore = 1 THEN 'VIRTUAL' 
        WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
        WHEN CustodyStore = 1 THEN 'CUSTODIA'
        WHEN TransitStore = 1 THEN 'TRANSITO'
        WHEN ControlStore = 1 THEN 'CONTROL'
        WHEN WareHouseType = 1 THEN 'REMANENTE'
        ELSE 'NINGUNO'
    END AS 'TIPO ALMACEN',
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.AverageCost) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.AverageCost) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.[TIPO ORDEN],KOT.[ESTADO],
 KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_ORDENES_TRASLADO AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='TransferOrder' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.[TIPO ORDEN],KOT.[ESTADO],KOT.DocumentDate
),

CTE_CONTABILIDAD_ORDENES_TRASLADO
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'ORDENES DE TRASLADO' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO'
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_ORDENES_TRASLADO AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='TransferOrder'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate
),

---****************************************************************************************************************************************-----
---************************************************DEVOLUCION DE ORDENES DE TRASLADO *******************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,CASE TFO.OrderType WHEN 1 THEN 'Traslado' WHEN 2 THEN 'Consumo' WHEN 3 THEN 'Traslado en Transito' END 'TIPO ORDEN',
 CASE TFO.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Entregado' WHEN 3 THEN 'Anulado' WHEN 4 THEN 'En Transito' END 'ESTADO'
 FROM Inventory .Kardex K
 INNER JOIN Inventory .TransferOrderDevolution AS TOD ON TOD.ID=K.EntityId AND TOD.CODE=EntityCode
INNER JOIN Inventory .TransferOrder TFO ON TFO.ID=TOD.TransferOrderId 
 WHERE K.EntityName ='TransferOrderDevolution'AND TOD.Status<>3 AND k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(HOUR, -1, COMMON.GETDATE())-- AND K.EntityCode='PMA0000001923'
),

CTE_DETALLE_KARDEX_DEVOLUCION_ORDENES_TRASLADO
AS
(
 SELECT 'DEVOLUCION ORDENES DE TRASLADO' 'TRANSACCION',year(K.DocumentDate) 'AÑO',month(K.DocumentDate) 'MES',DAY(K.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE K.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE 
        WHEN VirtualStore = 1 THEN 'VIRTUAL' 
        WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
        WHEN CustodyStore = 1 THEN 'CUSTODIA'
        WHEN TransitStore = 1 THEN 'TRANSITO'
        WHEN ControlStore = 1 THEN 'CONTROL'
        WHEN WareHouseType = 1 THEN 'REMANENTE'
        ELSE 'NINGUNO'
    END AS 'TIPO ALMACEN',
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.[TIPO ORDEN],KOT.[ESTADO],
 KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='TransferOrderDevolution' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.[TIPO ORDEN],KOT.[ESTADO],KOT.DocumentDate
),

CTE_CONTABILIDAD_DEVOLUCION_ORDENES_TRASLADO
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO', 'DEVOLUCION ORDENES DE TRASLADO' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO'
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='TransferOrderDevolution'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate
),

---********************************************************************************************************************************************************-----
---********************************************************** COMPROBANTES DE ENTRADA **********************************************************************----
---*********************************************************************************************************************************************************----

CTE_KARDEX_COMPROBANTES_ENTRADA
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode,CAST(k.DocumentDate AS DATE) DocumentDate, K.EntityName ,AP.Id ID_CXP,AP.Code [CODIGO CXP],CASE EVD.EntranceSource WHEN 1 THEN 'Ninguna'
      WHEN 2 THEN 'Orden de Compra' WHEN  3 THEN 'Contrato' WHEN 4 THEN 'Remisión de Entrada' WHEN  5 THEN 'Remisión en Consignación' END 'TIPO ORDEN',
	  'SI' 'VALOR',EV.ValueTax 'VALOR IVA','Confirmado' 'ESTADO'
 FROM Inventory .Kardex K
 INNER JOIN Payments.AccountPayable AS AP ON K.EntityId=AP.EntityId AND K.EntityCode=AP.EntityCode
 INNER JOIN Inventory.EntranceVoucher EV ON EV.ID=K.EntityId AND EV.CODE=K.EntityCode
 INNER JOIN Inventory.EntranceVoucherDetail AS EVD ON EV.ID=EVD.EntranceVoucherId
 WHERE K.EntityName ='EntranceVoucher' AND EV.Status<> 3 AND k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(HOUR, -1, COMMON.GETDATE()) --AND K.EntityCode='ACBA0000004270'
),

CTE_DETALLE_KARDEX_COMPROBANTES_ENTRADA
AS
(
 SELECT 'COMPROBANTES DE ENTRADA' 'TRANSACCION',year(K.DocumentDate) 'AÑO',month(K.DocumentDate) 'MES',DAY(K.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE K.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
 WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO'WHEN ControlStore = 1 THEN 'CONTROL' WHEN WareHouseType = 1 THEN 'REMANENTE'ELSE 'NINGUNO'
 END AS 'TIPO ALMACEN',
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',
 K.EntityId,MA.ID IdCuenta,KOT.[TIPO ORDEN],KOT.[ESTADO],KOT.[CODIGO CXP] 'CODIGO INTERNO',KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_COMPROBANTES_ENTRADA AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='EntranceVoucher' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.[TIPO ORDEN],KOT.[ESTADO],
 KOT.[CODIGO CXP],KOT.DocumentDate
),

CTE_CONTABILIDAD_COMPROBANTES_ENTRADA
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  KOT.EntityCode 'CODIGO DOCUMENTO',  'COMPROBANTES DE ENTRADA' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',
  SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', 
  JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.[CODIGO CXP] 'CODIGO INTERNO',KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO'
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_COMPROBANTES_ENTRADA AS KOT ON KOT.ID_CXP=JV.EntityId AND KOT.[CODIGO CXP]=JV.EntityCode AND JV.EntityName='AccountPayable'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.EntityCode,KOT.[CODIGO CXP],KOT.DocumentDate
),

---*********************************************************************************************************************************************************----
---****************************************************** DEVOLUCION COMPROBANTES DE ENTRADA ***************************************************************----
---*********************************************************************************************************************************************************----

CTE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode,CAST(k.DocumentDate AS DATE) DocumentDate, K.EntityName,PN.ID ID_NOTA,PN.CODE 'CODIGO NOTA',EVD.ID ID_DEVO,EVD.CODE 'CODIGO DEV', EVD.ValueTax 'VALOR IVA',
CASE EVD.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Confirmado' WHEN 3 THEN 'Anulado' END 'ESTADO'
 FROM Inventory .Kardex K
 INNER JOIN Inventory.EntranceVoucherDevolution EVD ON EVD.ID=K.EntityId AND EVD.CODE=EntityCode
 INNER JOIN Payments.PaymentNotes as PN ON PN.Entitycode=EVD.Code and PN.EntityId= EVD.Id
 WHERE K.EntityName ='EntranceVoucherDevolution'AND EVD.Status<>3 AND k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(HOUR, -1, COMMON.GETDATE())-- AND K.EntityCode='ACBA0000004270'
),

CTE_DETALLE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA
AS
(
 SELECT 'DEVOLUCION ORDENES DE TRASLADO' 'TRANSACCION',year(K.DocumentDate) 'AÑO',month(K.DocumentDate) 'MES',DAY(K.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE K.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
        WHEN CustodyStore = 1 THEN 'CUSTODIA'WHEN TransitStore = 1 THEN 'TRANSITO'WHEN ControlStore = 1 THEN 'CONTROL'WHEN WareHouseType = 1 THEN 'REMANENTE'
        ELSE 'NINGUNO'END AS 'TIPO ALMACEN',
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,'Devoluciones en Compras' [TIPO ORDEN],
 KOT.[ESTADO],KOT.[CODIGO NOTA] 'CODIGO INTERNO',KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA AS KOT ON KOT.ID_DEVO=K.EntityId AND KOT.[CODIGO DEV]=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='EntranceVoucherDevolution' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.[CODIGO NOTA],KOT.[ESTADO],KOT.DocumentDate
),

CTE_CONTABILIDAD_DEVOLUCION_COMPROBANTES_ENTRADA
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  KOT.EntityCode 'CODIGO DOCUMENTO', 'DEVOLUCION ORDENES DE TRASLADO' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',
  SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', KOT.[CODIGO NOTA] 'CODIGO INTERNO',
  JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO'
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA AS KOT ON KOT.ID_NOTA=JV.EntityId AND KOT.[CODIGO NOTA]=JV.EntityCode AND JV.EntityName='PaymentNotes'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.EntityCode,KOT.[CODIGO NOTA],KOT.DocumentDate
),

---****************************************************************************************************************************************-----
---************************************************ REMISIONES DE ENTRADA *************************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_REMISIONES_ENTRADA
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K
 WHERE K.EntityName ='RemissionEntrance' AND k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(HOUR, -1, COMMON.GETDATE())-- AND K.EntityCode='MSGT0000000146'
),

CTE_DETALLE_KARDEX_REMISIONES_ENTRADA
AS
(
 SELECT  'REMISION DE ENTRADA' 'TRANSACCION',year(K.DocumentDate) 'AÑO',month(K.DocumentDate) 'MES',DAY(K.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE K.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE 
        WHEN VirtualStore = 1 THEN 'VIRTUAL' 
        WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
        WHEN CustodyStore = 1 THEN 'CUSTODIA'
        WHEN TransitStore = 1 THEN 'TRANSITO'
        WHEN ControlStore = 1 THEN 'CONTROL'
        WHEN WareHouseType = 1 THEN 'REMANENTE'
        ELSE 'NINGUNO'
    END AS 'TIPO ALMACEN',KOT.[ESTADO],
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.AverageCost) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_REMISIONES_ENTRADA AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='RemissionEntrance' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
),

CTE_CONTABILIDAD_REMISIONES_ENTRADA
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'REMISION DE ENTRADA' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO'
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_REMISIONES_ENTRADA AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='RemissionEntrance'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate
),

---****************************************************************************************************************************************-----
---************************************************DEVOLUCION REMISION DE ENTRADA *************************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_DEVOLUCION_REMISION_ENTRADA
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K
 INNER JOIN Inventory.RemissionDevolution AS RV ON RV.Id=k.EntityId and RV.Code=K.EntityCode
 WHERE K.EntityName ='RemissionDevolution' AND RV.DevolutionType=1 AND k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(HOUR, -1, COMMON.GETDATE())  --AND K.EntityCode='MSGT0000000146'
),

CTE_DETALLE_KARDEX_DEVOLUCION_REMISION_ENTRADA
AS
(
 SELECT 'DEVOLUCION REMISION DE ENTRADA' 'TRANSACCION',year(K.DocumentDate) 'AÑO',month(K.DocumentDate) 'MES',DAY(K.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE K.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
        WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO' WHEN ControlStore = 1 THEN 'CONTROL' WHEN WareHouseType = 1 THEN 'REMANENTE'
        ELSE 'NINGUNO'END AS 'TIPO ALMACEN',KOT.[ESTADO],
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_DEVOLUCION_REMISION_ENTRADA AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='RemissionDevolution' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
),

CTE_CONTABILIDAD_DEVOLUCION_REMISIONES_ENTRADA
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'DEVOLUCION REMISION DE ENTRADA' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO'
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_DEVOLUCION_REMISION_ENTRADA AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='RemissionEntranceDevolution'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate
),

---****************************************************************************************************************************************-----
---*****************************************REMISIONES DE INVENTARIO EN CONSIGNACION*********************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_REMISIONES_CONSIGNACION
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K
 WHERE K.EntityName ='ConsignmentInventoryRemission' AND k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(HOUR, -1, COMMON.GETDATE())  --AND K.EntityCode='ALCE0000000249'
),

CTE_DETALLE_KARDEX_REMISIONES_CONSIGNACION
AS
(
 SELECT  'REMISION DE INVENTARIO CONSIGNACION' 'TRANSACCION',year(K.DocumentDate) 'AÑO',month(K.DocumentDate) 'MES',DAY(K.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE K.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE 
        WHEN VirtualStore = 1 THEN 'VIRTUAL' 
        WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
        WHEN CustodyStore = 1 THEN 'CUSTODIA'
        WHEN TransitStore = 1 THEN 'TRANSITO'
        WHEN ControlStore = 1 THEN 'CONTROL'
        WHEN WareHouseType = 1 THEN 'REMANENTE'
        ELSE 'NINGUNO'
    END AS 'TIPO ALMACEN',KOT.[ESTADO],
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.AverageCost) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_REMISIONES_CONSIGNACION AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='ConsignmentInventoryRemission' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
),

CTE_CONTABILIDAD_REMISIONES_CONSIGNACION
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'REMISION DE INVENTARIO CONSIGNACION' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO'
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_REMISIONES_CONSIGNACION AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='ConsignmentInventoryRemission'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate
),

---****************************************************************************************************************************************-----
---************************************************DEVOLUCION REMISION DE INVENTARIO CONSIGNACION*********************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K
 INNER JOIN Inventory.RemissionDevolution AS RV ON RV.Id=k.EntityId and RV.Code=K.EntityCode
 WHERE K.EntityName ='RemissionDevolution' AND RV.DevolutionType=3 AND k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(HOUR, -1, COMMON.GETDATE()) -- AND K.EntityCode='PMCT0000000016'
),

CTE_DETALLE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION
AS
(
 SELECT 'DEVOLUCION REMISION DE ENTRADA' 'TRANSACCION',year(K.DocumentDate) 'AÑO',month(K.DocumentDate) 'MES',DAY(K.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE K.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
        WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO' WHEN ControlStore = 1 THEN 'CONTROL' WHEN WareHouseType = 1 THEN 'REMANENTE'
        ELSE 'NINGUNO'END AS 'TIPO ALMACEN',KOT.[ESTADO],
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='RemissionDevolution' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
),

CTE_CONTABILIDAD_DEVOLUCION_REMISIONES_CONSIGNACION
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'DEVOLUCION REMISION DE ENTRADA' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO'
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='ConsignmentInventoryRemissionDevolution'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate
)

------------------------------******************DISPENSACIONES Y DEVOLUCION DE DISPENSACIONES ***********************------------------------------
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],'Dispensación Farmacéutica' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_DETALLE_KARDEX_DISPENSACIONES
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Dispensación Farmacéutica' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_CONTABILIDAD_DISPENSACIONES
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,'Devolución Dispensación' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_DETALLE_KARDEX_DEVOLUCION_DISPENSACIONES
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Devolución Dispensación' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_CONTABILIDAD_DEVOLUCION_DISPENSACIONES
------------------------------**********************************************************************************************************------------------------------
UNION ALL
------------------------------************************ AJUSTES DE INVENTARIOS **********************************************************------------------------------
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],'Ajuste' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_DETALLE_KARDEX_AJUSTES
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Ajuste' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_CONTABILIDAD_AJUSTES
------------------------------**********************************************************************************************************------------------------------
UNION ALL
------------------------------*********************************** PRESTAMOS *********************************************------------------------------
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],'Prestamos' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_DETALLE_KARDEX_PRESTAMOS
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Prestamos' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_CONTABILIDAD_PRESTAMOS
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,'Devolución de Prestamos' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_DETALLE_KARDEX_DEVOLUCION_PRESTAMOS
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Devolución de Prestamos' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_CONTABILIDAD_DEVOLUCION_PRESTAMOS
------------------------------**********************************************************************************************************------------------------------
UNION ALL
------------------------------************************** ORDENES DE TRASALDOS *****************************************************------------------------------
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],[TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_DETALLE_KARDEX_ORDENES_TRASLADO
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Ordene de Traslado' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_CONTABILIDAD_ORDENES_TRASLADO
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,[TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_DETALLE_KARDEX_DEVOLUCION_ORDENES_TRASLADO
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Ordene de Traslado' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_CONTABILIDAD_DEVOLUCION_ORDENES_TRASLADO
------------------------------************************************************** *****************************************************------------------------------
UNION ALL
------------------------------************************** COMPROBANTES DE ENTRADA *****************************************************------------------------------
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],[TIPO ORDEN],[ESTADO],
[CODIGO INTERNO],[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_DETALLE_KARDEX_COMPROBANTES_ENTRADA
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Comprobante Entrada' [TIPO ORDEN],[ESTADO],
[CODIGO INTERNO],[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_CONTABILIDAD_COMPROBANTES_ENTRADA
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,[TIPO ORDEN],[ESTADO],
[CODIGO INTERNO],[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_DETALLE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Comprobante Entrada' [TIPO ORDEN],[ESTADO],
[CODIGO INTERNO],[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_CONTABILIDAD_DEVOLUCION_COMPROBANTES_ENTRADA
------------------------------***************************************************************************************************------------------------------
UNION ALL
------------------------------**************************** REMISION DE ENTRADA ******************************************------------------------------
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],'Remisión de Entrada' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_DETALLE_KARDEX_REMISIONES_ENTRADA
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Remisión de Entrada' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_CONTABILIDAD_REMISIONES_ENTRADA
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,'Devolución Remisión de Entrada' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_DETALLE_KARDEX_DEVOLUCION_REMISION_ENTRADA
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Devolución Remisión de Entrada' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_CONTABILIDAD_DEVOLUCION_REMISIONES_ENTRADA
------------------------------***************************************************************************************************------------------------------
UNION ALL
------------------------------**************************** REMISION DE INVENTARIOS CONSIGNACION ***********************************------------------------------
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],'Remisión Inventario Consignación' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_DETALLE_KARDEX_REMISIONES_CONSIGNACION
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Remisión Inventario Consignación' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_CONTABILIDAD_REMISIONES_CONSIGNACION
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,'Devolución Remisión Inventario Consignación' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_DETALLE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Devolución Remisión Inventario Consignación' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM CTE_CONTABILIDAD_DEVOLUCION_REMISIONES_CONSIGNACION
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting destinada a alimentar un cubo analítico (OLAP) que confronta los movimientos de inventario (kardex) con los asientos contables generados para las mismas transacciones. Cubre dispensaciones farmacéuticas, devoluciones de dispensaciones y ajustes de inventario desde junio de 2024, cruzando cantidades valorizadas a costo promedio contra débitos/créditos registrados en el libro legal (LegalBookId=1). El resultado permite detectar diferencias entre el módulo de inventario (SCM) y contabilidad general, desglosado por almacén, tipo de almacén, grupo de producto y cuenta contable (PUC).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMInventoryVsAccounting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMInventoryVsAccounting';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola salida la comparación entre los movimientos de inventario (kardex) y sus contrapartidas contables (libro legal) por tipo de transacción de SCM, para alimentar un cubo de control inventario vs. contabilidad.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMInventoryVsAccounting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los movimientos de Inventory.Kardex deben tener DocumentDate entre ''2024-06-01 00:00:00'' y la hora actual menos 1 hora (COMMON.GETDATE()); Cada producto en Kardex debe tener ProductGroup con InventoryAccountPayableConceptId asociado a un AccountPayableConcept con IdAccount válido en GeneralLedger.MainAccounts (por los INNER JOIN); Los comprobantes contables considerados deben pertenecer al libro legal (JV.LegalBookId = 1); Para órdenes de traslado y devoluciones: el estado no puede ser 3 (Anulado) (TFO.Status<>3, TOD.Status<>3, EVD.Status<>3); Para devoluciones de remisión: RemissionDevolution.DevolutionType debe ser 1 (entrada) o 3 (consignación) según el bloque; Existencia de la función COMMON.GETDATE() y de la zona horaria ''Pakistan Standard Time''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMInventoryVsAccounting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un dataset unificado con columnas (ID_COMPANY, MODULO, TRANSACCION, AÑO, MES, DIA, CODIGO GRUPO, GRUPO PRODUCTO, CUENTA CONTABLE, NOMBRE ALMACEN, TIPO DE TRANSACCION, TIPO ALMACEN, TOTAL ENTRADAS/VALOR DEBITO, TOTAL SALIDAS/VALOR CREDITO, AFECTA INVENTARIO, CODIGO DOCUMENTO, TIPO ORDEN, ESTADO, CODIGO INTERNO, FECHA BUSQUEDA, ULT_ACTUA) usando UNION ALL entre kardex (MODULO=''INVENTARIOS'') y comprobantes contables (MODULO=''CONTABILIDAD'') para 11 tipos de transacciones SCM.; [RETURN_RESULT] resultset: ID_COMPANY se obtiene de DB_NAME() truncado a VARCHAR(9); ULT_ACTUA es la hora actual convertida a la zona ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMInventoryVsAccounting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Inventory.Kardex.MovementType = 1 → Se clasifica como ''ENTRADA'' y el monto se acumula en TOTAL ENTRADAS else Si MovementType = 2 se clasifica como ''SALIDA'' y el monto se acumula en TOTAL SALIDAS; si Para dispensaciones, ajustes, préstamos y órdenes de traslado: monto = Quantity * Kardex.AverageCost → Se valoriza el movimiento al costo promedio del kardex else Para devoluciones, comprobantes de entrada, devolución de comprobantes y remisiones: monto = Quantity * Kardex.Value; si Warehouse: VirtualStore=1 / WarehouseConsignment=1 / CustodyStore=1 / TransitStore=1 / ControlStore=1 / WareHouseType=1 → Se etiqueta TIPO ALMACEN como VIRTUAL / CONSIGNACION / CUSTODIA / TRANSITO / CONTROL / REMANENTE respectivamente else Si ninguno aplica, TIPO ALMACEN = ''NINGUNO''; si Kardex.AffectInventory = 1 → AFECTA INVENTARIO = ''SI'' else AFECTA INVENTARIO = ''NO''; si TransferOrder.OrderType IN (1,2,3) → TIPO ORDEN se etiqueta como ''Traslado'', ''Consumo'' o ''Traslado en Transito''; si TransferOrder/TransferOrderDevolution.Status (1,2,3,4) → ESTADO se etiqueta como ''Registrado'', ''Entregado'', ''Anulado'' o ''En Transito''; los registros con Status=3 son excluidos del resultado; si EntranceVoucherDetail.EntranceSource (1..5) → TIPO ORDEN para comprobantes de entrada se mapea a ''Ninguna'', ''Orden de Compra'', ''Contrato'', ''Remisión de Entrada'' o ''Remisión en Consignación''; si RemissionDevolution.DevolutionType = 1 → Se incluye en el bloque ''DEVOLUCION REMISION DE ENTRADA'' else Si DevolutionType = 3, se incluye en el bloque ''DEVOLUCION REMISION DE INVENTARIO CONSIGNACION''; si Cruce contable según EntityName del kardex → JournalVouchers se vincula con EntityName = ''PharmaceuticalDispensing'', ''PharmaceuticalDispensingDevolution'', ''InventoryAdjustment'', ''LoanMerchandise'', ''LoanMerchandiseDevolution'', ''TransferOrder'', ''TransferOrderDevolution'', ''AccountPayable'' (para EntranceVoucher), ''PaymentNotes'' (para devoluciones de comprobantes), ''RemissionEntrance'', ''RemissionEntranceDevolution'', ''ConsignmentInventoryRemission'' o ''ConsignmentInventoryRemissionDevolution'' según el bloque', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMInventoryVsAccounting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMInventoryVsAccounting';
GO
