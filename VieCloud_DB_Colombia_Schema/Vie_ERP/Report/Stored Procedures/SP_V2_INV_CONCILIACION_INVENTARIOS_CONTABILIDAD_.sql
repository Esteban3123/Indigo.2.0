

CREATE PROCEDURE [Report].[SP_V2_INV_CONCILIACION_INVENTARIOS_CONTABILIDAD_]

----************************************ EMPRESA: ODO  SCRIPT TOTAL MOVIMIENTOS *********************************---

@AÑO AS INT,
@MES AS INT
AS

---****************************************************************************************************************************************-----
---************************************************DISPENSACIONES FARMACEUTICAS*************************************************************----
---*****************************************************************************************************************************************----

WITH CTE_KARDEX_DISPENSACIONES
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate
 FROM Inventory .Kardex K
 WHERE K.EntityName ='PharmaceuticalDispensing' AND YEAR(k.DocumentDate)=@AÑO AND MONTH(k.DocumentDate)=@MES AND K.DocumentDate < DATEADD(MINUTE, -10, COMMON.GETDATE())-- AND K.EntityCode='0001868764'
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
    END AS 'TIPO ALMACEN',
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.AverageCost) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.AverageCost) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_DISPENSACIONES AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='PharmaceuticalDispensing' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType
),

CTE_CONTABILIDAD_DISPENSACIONES
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'DISPENSACION FARMACEUTICA' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',
  SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_DISPENSACIONES AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='PharmaceuticalDispensing'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID
),

---****************************************************************************************************************************************-----
---************************************************DEVOLUCION DISPENSACIONES FARMACEUTICAS*************************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_DEVOLUCION_DISPENSACIONES
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate
 FROM Inventory .Kardex K
 WHERE K.EntityName ='PharmaceuticalDispensingDevolution' AND YEAR(k.DocumentDate)=@AÑO AND MONTH(k.DocumentDate)=@MES AND K.DocumentDate < DATEADD(MINUTE, -10, COMMON.GETDATE()) -- AND K.EntityCode='PMA0000001923'
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
    END AS 'TIPO ALMACEN',
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_DEVOLUCION_DISPENSACIONES AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='PharmaceuticalDispensingDevolution' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType
),

CTE_CONTABILIDAD_DEVOLUCION_DISPENSACIONES
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'DEVOLUCION DISPENSACION FARMACEUTICA' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',
  SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_DEVOLUCION_DISPENSACIONES AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='PharmaceuticalDispensingDevolution'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID
),

---****************************************************************************************************************************************-----
---************************************************AJUSTES DE INVENTARIOS ******************************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_AJUSTES
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate
 FROM Inventory .Kardex K
 WHERE K.EntityName ='InventoryAdjustment' AND YEAR(k.DocumentDate)=@AÑO AND MONTH(k.DocumentDate)=@MES AND K.DocumentDate < DATEADD(MINUTE, -10, COMMON.GETDATE())-- AND K.EntityCode='0001868764'
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
    END AS 'TIPO ALMACEN',
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.AverageCost) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.AverageCost) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_AJUSTES AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='InventoryAdjustment' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType
),

CTE_CONTABILIDAD_AJUSTES
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'AJUSTE DE INVENTARIOS' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',
  SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_AJUSTES AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='InventoryAdjustment'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID
),

---****************************************************************************************************************************************-----
---************************************************* PRESTAMOS DE MERCANCIAS ***************************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_PRESTAMOS
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate
 FROM Inventory .Kardex K
 WHERE K.EntityName ='LoanMerchandise' AND YEAR(k.DocumentDate)=@AÑO AND MONTH(k.DocumentDate)=@MES AND K.DocumentDate < DATEADD(MINUTE, -10, COMMON.GETDATE()) -- AND K.EntityCode='0001868764'
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
    END AS 'TIPO ALMACEN',
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.AverageCost) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.AverageCost) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_PRESTAMOS AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='LoanMerchandise' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType
),

CTE_CONTABILIDAD_PRESTAMOS
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'PRESTAMOS DE MERCANCIAS' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',
  SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_PRESTAMOS AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='LoanMerchandise'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID
),

---****************************************************************************************************************************************-----
---************************************************DEVOLUCION DE PRESTAMOS DE MERCANCIAS****************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_DEVOLUCION_PRESTAMOS
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,k.DocumentDate
 FROM Inventory .Kardex K
 WHERE K.EntityName ='LoanMerchandiseDevolution' AND YEAR(k.DocumentDate)=@AÑO AND MONTH(k.DocumentDate)=@MES AND K.DocumentDate < DATEADD(MINUTE, -10, COMMON.GETDATE()) -- AND K.EntityCode='PMA0000001923'
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
    END AS 'TIPO ALMACEN',
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_DEVOLUCION_PRESTAMOS AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='LoanMerchandiseDevolution' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType
),

CTE_CONTABILIDAD_DEVOLUCION_PRESTAMOS
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO', 'DEVOLUCION PRESTAMOS DE MERCANCIAS' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',
  SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_DEVOLUCION_PRESTAMOS AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='LoanMerchandiseDevolution'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID
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
 WHERE K.EntityName ='TransferOrder' AND TFO.Status<> 3 AND YEAR(k.DocumentDate)=@AÑO AND MONTH(k.DocumentDate)=@MES AND K.DocumentDate < DATEADD(MINUTE, -10, COMMON.GETDATE()) -- AND K.EntityCode='0001868764'
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
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.[TIPO ORDEN],KOT.[ESTADO]
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_ORDENES_TRASLADO AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='TransferOrder' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.[TIPO ORDEN],KOT.[ESTADO]
),

CTE_CONTABILIDAD_ORDENES_TRASLADO
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'ORDENES DE TRASLADO' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',
  SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_ORDENES_TRASLADO AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='TransferOrder'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID
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
 WHERE K.EntityName ='TransferOrderDevolution'AND TOD.Status<>3 AND YEAR(k.DocumentDate)=@AÑO AND MONTH(k.DocumentDate)=@MES AND K.DocumentDate < DATEADD(MINUTE, -10, COMMON.GETDATE())-- AND K.EntityCode='PMA0000001923'
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
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.[TIPO ORDEN],KOT.[ESTADO]
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='TransferOrderDevolution' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.[TIPO ORDEN],KOT.[ESTADO]
),

CTE_CONTABILIDAD_DEVOLUCION_ORDENES_TRASLADO
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO', 'DEVOLUCION ORDENES DE TRASLADO' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',
  SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='TransferOrderDevolution'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID
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
 WHERE K.EntityName ='EntranceVoucher' AND EV.Status<> 3 AND YEAR(k.DocumentDate)=@AÑO AND MONTH(k.DocumentDate)=@MES AND K.DocumentDate < DATEADD(MINUTE, -10, COMMON.GETDATE()) --AND K.EntityCode='ACBA0000004270'
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
 K.EntityId,MA.ID IdCuenta,KOT.[TIPO ORDEN],KOT.[ESTADO],KOT.[CODIGO CXP] 'CODIGO INTERNO'
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_COMPROBANTES_ENTRADA AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='EntranceVoucher' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.[TIPO ORDEN],KOT.[ESTADO],KOT.[CODIGO CXP]
),

CTE_CONTABILIDAD_COMPROBANTES_ENTRADA
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  KOT.EntityCode 'CODIGO DOCUMENTO',  'COMPROBANTES DE ENTRADA' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',
  SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', 
  JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.[CODIGO CXP] 'CODIGO INTERNO'
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_COMPROBANTES_ENTRADA AS KOT ON KOT.ID_CXP=JV.EntityId AND KOT.[CODIGO CXP]=JV.EntityCode AND JV.EntityName='AccountPayable'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.EntityCode,KOT.[CODIGO CXP]
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
 WHERE K.EntityName ='EntranceVoucherDevolution'AND EVD.Status<>3 AND YEAR(k.DocumentDate)=@AÑO AND MONTH(k.DocumentDate)=@MES AND K.DocumentDate < DATEADD(MINUTE, -10, COMMON.GETDATE())-- AND K.EntityCode='ACBA0000004270'
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
 KOT.[ESTADO],KOT.[CODIGO NOTA] 'CODIGO INTERNO'
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA AS KOT ON KOT.ID_DEVO=K.EntityId AND KOT.[CODIGO DEV]=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='EntranceVoucherDevolution' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.[CODIGO NOTA],KOT.[ESTADO]
),

CTE_CONTABILIDAD_DEVOLUCION_COMPROBANTES_ENTRADA
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  KOT.EntityCode 'CODIGO DOCUMENTO', 'DEVOLUCION ORDENES DE TRASLADO' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',
  SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', KOT.[CODIGO NOTA] 'CODIGO INTERNO',
  JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA AS KOT ON KOT.ID_NOTA=JV.EntityId AND KOT.[CODIGO NOTA]=JV.EntityCode AND JV.EntityName='PaymentNotes'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.EntityCode,KOT.[CODIGO NOTA]
),

---****************************************************************************************************************************************-----
---************************************************ REMISIONES DE ENTRADA *************************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_REMISIONES_ENTRADA
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate
 FROM Inventory .Kardex K
 WHERE K.EntityName ='RemissionEntrance' AND YEAR(k.DocumentDate)=@AÑO AND MONTH(k.DocumentDate)=@MES AND K.DocumentDate < DATEADD(MINUTE, -10, COMMON.GETDATE())-- AND K.EntityCode='MSGT0000000146'
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
    END AS 'TIPO ALMACEN',
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.AverageCost) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_REMISIONES_ENTRADA AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='RemissionEntrance' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType
),

CTE_CONTABILIDAD_REMISIONES_ENTRADA
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'REMISION DE ENTRADA' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',
  SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_REMISIONES_ENTRADA AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='RemissionEntrance'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID
),

---****************************************************************************************************************************************-----
---************************************************DEVOLUCION REMISION DE ENTRADA *************************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_DEVOLUCION_REMISION_ENTRADA
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate
 FROM Inventory .Kardex K
 INNER JOIN Inventory.RemissionDevolution AS RV ON RV.Id=k.EntityId and RV.Code=K.EntityCode
 WHERE K.EntityName ='RemissionDevolution' AND RV.DevolutionType=1 AND YEAR(k.DocumentDate)=@AÑO AND MONTH(k.DocumentDate)=@MES AND K.DocumentDate < DATEADD(MINUTE, -10, COMMON.GETDATE())  --AND K.EntityCode='MSGT0000000146'
),

CTE_DETALLE_KARDEX_DEVOLUCION_REMISION_ENTRADA
AS
(
 SELECT 'DEVOLUCION REMISION DE ENTRADA' 'TRANSACCION',year(K.DocumentDate) 'AÑO',month(K.DocumentDate) 'MES',DAY(K.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE K.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
        WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO' WHEN ControlStore = 1 THEN 'CONTROL' WHEN WareHouseType = 1 THEN 'REMANENTE'
        ELSE 'NINGUNO'END AS 'TIPO ALMACEN',
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_DEVOLUCION_REMISION_ENTRADA AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='RemissionDevolution' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType
),

CTE_CONTABILIDAD_DEVOLUCION_REMISIONES_ENTRADA
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'DEVOLUCION REMISION DE ENTRADA' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',
  SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_DEVOLUCION_REMISION_ENTRADA AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='RemissionEntranceDevolution'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID
),

---****************************************************************************************************************************************-----
---*****************************************REMISIONES DE INVENTARIO EN CONSIGNACION*********************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_REMISIONES_CONSIGNACION
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate
 FROM Inventory .Kardex K
 WHERE K.EntityName ='ConsignmentInventoryRemission' AND YEAR(k.DocumentDate)=@AÑO AND MONTH(k.DocumentDate)=@MES AND K.DocumentDate < DATEADD(MINUTE, -10, COMMON.GETDATE())  --AND K.EntityCode='ALCE0000000249'
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
    END AS 'TIPO ALMACEN',
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.AverageCost) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_REMISIONES_CONSIGNACION AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='ConsignmentInventoryRemission' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType
),

CTE_CONTABILIDAD_REMISIONES_CONSIGNACION
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'REMISION DE INVENTARIO CONSIGNACION' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',
  SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_REMISIONES_CONSIGNACION AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='ConsignmentInventoryRemission'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID
),

---****************************************************************************************************************************************-----
---************************************************DEVOLUCION REMISION DE INVENTARIO CONSIGNACION*********************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate
 FROM Inventory .Kardex K
 INNER JOIN Inventory.RemissionDevolution AS RV ON RV.Id=k.EntityId and RV.Code=K.EntityCode
 WHERE K.EntityName ='RemissionDevolution' AND RV.DevolutionType=3 AND YEAR(k.DocumentDate)=@AÑO AND MONTH(k.DocumentDate)=@MES AND K.DocumentDate < DATEADD(MINUTE, -10, COMMON.GETDATE()) -- AND K.EntityCode='PMCT0000000016'
),

CTE_DETALLE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION
AS
(
 SELECT 'DEVOLUCION REMISION DE ENTRADA' 'TRANSACCION',year(K.DocumentDate) 'AÑO',month(K.DocumentDate) 'MES',DAY(K.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE K.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
        WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO' WHEN ControlStore = 1 THEN 'CONTROL' WHEN WareHouseType = 1 THEN 'REMANENTE'
        ELSE 'NINGUNO'END AS 'TIPO ALMACEN',
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(20,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta
FROM Inventory.Kardex K
 INNER JOIN CTE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='RemissionDevolution' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType
),

CTE_CONTABILIDAD_DEVOLUCION_REMISIONES_CONSIGNACION
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'DEVOLUCION REMISION DE ENTRADA' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',
  SUM(CAST(DebitValue AS NUMERIC (20,2))) 'VALOR DEBITO',SUM(CAST(CreditValue AS NUMERIC (20,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='ConsignmentInventoryRemissionDevolution'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA  ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID
)

------------------------------******************DISPENSACIONES Y DEVOLUCION DE DISPENSACIONES ***********************------------------------------
SELECT 'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],'Dispensación Farmacéutica' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_DETALLE_KARDEX_DISPENSACIONES
UNION ALL
SELECT 'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Dispensación Farmacéutica' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_CONTABILIDAD_DISPENSACIONES
UNION ALL
SELECT 'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,'Devolución Dispensación' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_DETALLE_KARDEX_DEVOLUCION_DISPENSACIONES
UNION ALL
SELECT 'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Devolución Dispensación' [TIPO ORDEN],'CONFIRMADO'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_CONTABILIDAD_DEVOLUCION_DISPENSACIONES
------------------------------**********************************************************************************************************------------------------------
UNION ALL
------------------------------************************ AJUSTES DE INVENTARIOS **********************************************************------------------------------
SELECT 'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],'Ajuste' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_DETALLE_KARDEX_AJUSTES
UNION ALL
SELECT 'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Ajuste' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_CONTABILIDAD_AJUSTES
------------------------------**********************************************************************************************************------------------------------
UNION ALL
------------------------------*********************************** PRESTAMOS *********************************************------------------------------
SELECT 'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],'Prestamos' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_DETALLE_KARDEX_PRESTAMOS
UNION ALL
SELECT 'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Prestamos' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_CONTABILIDAD_PRESTAMOS
UNION ALL
SELECT 'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,'Devolución de Prestamos' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_DETALLE_KARDEX_DEVOLUCION_PRESTAMOS
UNION ALL
SELECT 'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Devolución de Prestamos' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_CONTABILIDAD_DEVOLUCION_PRESTAMOS
------------------------------**********************************************************************************************************------------------------------
UNION ALL
------------------------------************************** ORDENES DE TRASALDOS *****************************************************------------------------------
SELECT 'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],[TIPO ORDEN],[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_DETALLE_KARDEX_ORDENES_TRASLADO
UNION ALL
SELECT 'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_CONTABILIDAD_ORDENES_TRASLADO
UNION ALL
SELECT 'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,[TIPO ORDEN],[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_DETALLE_KARDEX_DEVOLUCION_ORDENES_TRASLADO
UNION ALL
SELECT 'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_CONTABILIDAD_DEVOLUCION_ORDENES_TRASLADO
------------------------------************************** ORDENES DE TRASALDOS *****************************************************------------------------------
UNION ALL
------------------------------************************** COMPROBANTES DE ENTRADA *****************************************************------------------------------
SELECT 'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],[TIPO ORDEN],[ESTADO],[CODIGO INTERNO]
FROM CTE_DETALLE_KARDEX_COMPROBANTES_ENTRADA
UNION ALL
SELECT 'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'' [TIPO ORDEN],''[ESTADO],[CODIGO INTERNO]
FROM CTE_CONTABILIDAD_COMPROBANTES_ENTRADA
UNION ALL
SELECT 'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,[TIPO ORDEN],[ESTADO],[CODIGO INTERNO]
FROM CTE_DETALLE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA
UNION ALL
SELECT 'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'' [TIPO ORDEN],'' [ESTADO],[CODIGO INTERNO]
FROM CTE_CONTABILIDAD_DEVOLUCION_COMPROBANTES_ENTRADA
------------------------------***************************************************************************************************------------------------------
UNION ALL
------------------------------**************************** REMISION DE ENTRADA ******************************************------------------------------
SELECT 'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],'Remisión de Entrada' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_DETALLE_KARDEX_REMISIONES_ENTRADA
UNION ALL
SELECT 'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Remisión de Entrada' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_CONTABILIDAD_REMISIONES_ENTRADA
UNION ALL
SELECT 'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,'Devolución Remisión de Entrada' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_DETALLE_KARDEX_DEVOLUCION_REMISION_ENTRADA
UNION ALL
SELECT 'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Devolución Remisión de Entrada' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_CONTABILIDAD_DEVOLUCION_REMISIONES_ENTRADA
------------------------------***************************************************************************************************------------------------------
UNION ALL
------------------------------**************************** REMISION DE INVENTARIOS CONSIGNACION ***********************************------------------------------
SELECT 'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],'Remisión Inventario Consignación' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_DETALLE_KARDEX_REMISIONES_CONSIGNACION
UNION ALL
SELECT 'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Remisión Inventario Consignación' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_CONTABILIDAD_REMISIONES_CONSIGNACION
UNION ALL
SELECT 'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,'Devolución Remisión Inventario Consignación' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_DETALLE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION
UNION ALL
SELECT 'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Devolución Remisión Inventario Consignación' [TIPO ORDEN],'Confirmado'[ESTADO],[CODIGO DOCUMENTO] 'CODIGO INTERNO'
FROM CTE_CONTABILIDAD_DEVOLUCION_REMISIONES_CONSIGNACION
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento de reporte que concilia los movimientos de inventario registrados en el kardex (dispensaciones farmacéuticas, devoluciones de dispensación y ajustes de inventario) contra sus respectivos comprobantes contables del libro mayor, filtrando por año y mes. Para cada tipo de transacción cruza entradas/salidas valoradas por costo promedio con los débitos/créditos contabilizados, desagregando por cuenta contable (PUC), grupo de producto y almacén, permitiendo detectar diferencias entre el módulo de inventario y contabilidad.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_INV_CONCILIACION_INVENTARIOS_CONTABILIDAD_';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_INV_CONCILIACION_INVENTARIOS_CONTABILIDAD_';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de conciliación mensual entre los movimientos de inventario (kardex) y sus contrapartidas contables (comprobantes de diario) para múltiples tipos de transacciones de inventario, separados por módulo INVENTARIOS vs CONTABILIDAD.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_INV_CONCILIACION_INVENTARIOS_CONTABILIDAD_';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@AÑO y @MES deben corresponder al periodo a conciliar (filtran YEAR/MONTH de Kardex.DocumentDate); Los movimientos del kardex se incluyen sólo si DocumentDate es anterior a (GETDATE - 10 minutos), garantizando un margen para que la contabilización se haya completado; Cada ProductGroup debe tener configurado InventoryAccountPayableConceptId con su IdAccount en MainAccounts para poder mapear la cuenta contable; Los comprobantes contables se consideran sólo si JournalVouchers.LegalBookId = 1 (libro legal principal)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_INV_CONCILIACION_INVENTARIOS_CONTABILIDAD_';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve un único conjunto de resultados unificado vía UNION ALL con columnas MODULO (''INVENTARIOS'' o ''CONTABILIDAD''), TRANSACCION, AÑO, MES, DIA, CÓDIGO/GRUPO PRODUCTO, CUENTA, almacén, tipo de transacción, totales (entradas/salidas o débito/crédito), tipo orden, estado y código interno.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_INV_CONCILIACION_INVENTARIOS_CONTABILIDAD_';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Kardex.EntityName = ''PharmaceuticalDispensing'' → Procesa como DISPENSACION FARMACEUTICA y empareja con JournalVouchers de igual EntityName (valoriza con AverageCost); si Kardex.EntityName = ''PharmaceuticalDispensingDevolution'' → Procesa como DEVOLUCION DISPENSACION FARMACEUTICA (valoriza con Value en lugar de AverageCost); si Kardex.EntityName = ''InventoryAdjustment'' → Procesa como AJUSTE DE INVENTARIOS valorizado con AverageCost; si Kardex.EntityName = ''LoanMerchandise'' / ''LoanMerchandiseDevolution'' → Procesa préstamos de mercancía y sus devoluciones (préstamos con AverageCost; devolución con Value); si Kardex.EntityName = ''TransferOrder'' AND TransferOrder.Status <> 3 (no anulado) → Incluye órdenes de traslado mostrando OrderType (Traslado/Consumo/Traslado en Tránsito) y Status (Registrado/Entregado/Anulado/En Tránsito) else Si Status = 3 (Anulado) la orden se excluye; si Kardex.EntityName = ''TransferOrderDevolution'' AND TOD.Status <> 3 → Incluye devoluciones de órdenes de traslado no anuladas; si Kardex.EntityName = ''EntranceVoucher'' AND EV.Status <> 3 → Procesa comprobantes de entrada confirmados, enlazando con AccountPayable por EntityId/EntityCode y mostrando EntranceSource (Ninguna/OC/Contrato/Remisión Entrada/Remisión Consignación); la conciliación contable se hace contra JV.EntityName=''AccountPayable'' usando ID_CXP/CODIGO CXP; si Kardex.EntityName = ''EntranceVoucherDevolution'' AND EVD.Status <> 3 → Procesa devoluciones de comprobantes de entrada y empareja contabilidad contra JV.EntityName=''PaymentNotes'' usando ID_NOTA/CODIGO NOTA derivado de PaymentNotes ligado a la devolución; si Kardex.EntityName = ''RemissionEntrance'' → Procesa remisiones de entrada (entradas con Value, salidas con AverageCost); si Kardex.EntityName = ''RemissionDevolution'' AND RemissionDevolution.DevolutionType = 1 → Trata el movimiento como devolución de remisión de entrada y se concilia contra JV.EntityName=''RemissionEntranceDevolution''; si Kardex.EntityName = ''ConsignmentInventoryRemission'' → Procesa remisiones de inventario en consignación; si Kardex.EntityName = ''RemissionDevolution'' AND RemissionDevolution.DevolutionType = 3 → Trata el movimiento como devolución de remisión de consignación y se concilia contra JV.EntityName=''ConsignmentInventoryRemissionDevolution''; si Kardex.MovementType = 1 → Se clasifica como ENTRADA y la sumatoria Quantity*Costo se asigna a TOTAL ENTRADAS else MovementType = 2 → SALIDA, sumatoria asignada a TOTAL SALIDAS; si Banderas del almacén (VirtualStore/WarehouseConsignment/CustodyStore/TransitStore/ControlStore/WareHouseType = 1) → Determinan el TIPO ALMACEN: VIRTUAL / CONSIGNACION / CUSTODIA / TRANSITO / CONTROL / REMANENTE else Si ninguna bandera está activa → ''NINGUNO''; si Kardex.AffectInventory = 1 → Se reporta ''AFECTA INVENTARIO'' = ''SI'' else ''NO''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_INV_CONCILIACION_INVENTARIOS_CONTABILIDAD_';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_INV_CONCILIACION_INVENTARIOS_CONTABILIDAD_';
-- GO
