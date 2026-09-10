

--CON CAMBIO ORDEN DE TRASLADO--

CREATE PROCEDURE [Report].[SP_V2_INV_CONCILIACION_INVENTARIOS_CONTABILIDAD_FINAL]

----************************************ EMPRESA: ODO  SCRIPT TOTAL MOVIMIENTOS *********************************---
@FECINI AS DATE,
@FECHFIN AS DATE

AS
--DECLARE @FECINI AS DATE='2024-06-01';
--DECLARE @FECHFIN AS DATE='2024-06-30';

---****************************************************************************************************************************************-----
---************************************************DISPENSACIONES FARMACEUTICAS*************************************************************----
---*****************************************************************************************************************************************----

WITH CTE_KARDEX_DISPENSACIONES
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K WITH (NOLOCK)
 WHERE K.EntityName ='PharmaceuticalDispensing' AND CAST(K.DocumentDate AS DATE) BETWEEN @FECINI AND @FECHFIN
 --k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(MINUTE, -6, COMMON.GETDATE())
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
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K WITH (NOLOCK)
 INNER JOIN CTE_KARDEX_DISPENSACIONES AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) on apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='PharmaceuticalDispensing' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
),

CTE_CONTABILIDAD_DISPENSACIONES
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'DISPENSACION FARMACEUTICA' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC(18,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC(18,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO',JV.ID 'ID COMPROBANTE'
  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_DISPENSACIONES AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='PharmaceuticalDispensing'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT WITH (NOLOCK) ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
),

---****************************************************************************************************************************************-----
---************************************************DEVOLUCION DISPENSACIONES FARMACEUTICAS*************************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_DEVOLUCION_DISPENSACIONES
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K WITH (NOLOCK)
 WHERE K.EntityName ='PharmaceuticalDispensingDevolution' AND CAST(K.DocumentDate AS DATE) BETWEEN @FECINI AND @FECHFIN
 --k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(MINUTE, -6, COMMON.GETDATE())
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
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K WITH (NOLOCK)
 INNER JOIN CTE_KARDEX_DEVOLUCION_DISPENSACIONES AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='PharmaceuticalDispensingDevolution' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
),

CTE_CONTABILIDAD_DEVOLUCION_DISPENSACIONES
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'DEVOLUCION DISPENSACION FARMACEUTICA' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC(18,2)))'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC(18,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO',JV.ID 'ID COMPROBANTE'
  FROM GeneralLedger.JournalVouchers JV
  INNER JOIN CTE_KARDEX_DEVOLUCION_DISPENSACIONES AS KOT WITH (NOLOCK) ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='PharmaceuticalDispensingDevolution'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT WITH (NOLOCK) ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
),

---****************************************************************************************************************************************-----
---************************************************AJUSTES DE INVENTARIOS ******************************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_AJUSTES
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K WITH (NOLOCK)
 WHERE K.EntityName ='InventoryAdjustment' AND CAST(K.DocumentDate AS DATE) BETWEEN @FECINI AND @FECHFIN
 --k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(MINUTE, -6, COMMON.GETDATE())-- AND K.EntityCode='0001868764'
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
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K WITH (NOLOCK)
 INNER JOIN CTE_KARDEX_AJUSTES AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK)  ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='InventoryAdjustment' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
),

CTE_CONTABILIDAD_AJUSTES
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'AJUSTE DE INVENTARIOS' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC(18,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC(18,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO',JV.ID 'ID COMPROBANTE'
  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_AJUSTES AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='InventoryAdjustment'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK)  ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT WITH (NOLOCK) ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
),

---****************************************************************************************************************************************-----
---************************************************* PRESTAMOS DE MERCANCIAS ***************************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_PRESTAMOS
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K WITH (NOLOCK)
 WHERE K.EntityName ='LoanMerchandise' AND CAST(K.DocumentDate AS DATE) BETWEEN @FECINI AND @FECHFIN
 --k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(MINUTE, -6, COMMON.GETDATE()) -- AND K.EntityCode='0001868764'
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
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K WITH (NOLOCK)
 INNER JOIN CTE_KARDEX_PRESTAMOS AS KOT WITH (NOLOCK) ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='LoanMerchandise' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
),

CTE_CONTABILIDAD_PRESTAMOS
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'PRESTAMOS DE MERCANCIAS' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC(18,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC(18,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO',JV.ID 'ID COMPROBANTE'
  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_PRESTAMOS AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='LoanMerchandise'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT WITH (NOLOCK) ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
),

---****************************************************************************************************************************************-----
---************************************************DEVOLUCION DE PRESTAMOS DE MERCANCIAS****************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_DEVOLUCION_PRESTAMOS
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,k.DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K WITH (NOLOCK)
 WHERE K.EntityName ='LoanMerchandiseDevolution' AND CAST(K.DocumentDate AS DATE) BETWEEN @FECINI AND @FECHFIN
 --k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(MINUTE, -6, COMMON.GETDATE()) -- AND K.EntityCode='PMA0000001923'
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
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K WITH (NOLOCK)
 INNER JOIN CTE_KARDEX_DEVOLUCION_PRESTAMOS AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='LoanMerchandiseDevolution' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
),

CTE_CONTABILIDAD_DEVOLUCION_PRESTAMOS
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO', 'DEVOLUCION PRESTAMOS DE MERCANCIAS' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC(18,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC(18,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO',JV.ID 'ID COMPROBANTE'
  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_DEVOLUCION_PRESTAMOS AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='LoanMerchandiseDevolution'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT WITH (NOLOCK) ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
),

---****************************************************************************************************************************************-----
---*************************************************** ORDENES DE TRASLADO *****************************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_ORDENES_TRASLADO
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,
CASE TFO.OrderType WHEN 1 THEN 'Traslado' WHEN 2 THEN 'Consumo' WHEN 3 THEN 'Traslado en Transito' END 'TIPO ORDEN',
CASE TFO.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Entregado' WHEN 3 THEN 'Anulado' WHEN 4 THEN 'En Transito' END 'ESTADO'
 FROM Inventory .Kardex K WITH (NOLOCK)
 INNER JOIN Inventory .TransferOrder TFO WITH (NOLOCK) ON TFO.ID=K.EntityId AND TFO.CODE=K.EntityCode
 WHERE K.EntityName ='TransferOrder' AND TFO.Status<> 3 AND TFO.OrderType<>3 AND CAST(K.DocumentDate AS DATE) BETWEEN @FECINI AND @FECHFIN
 --k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(MINUTE, -6, COMMON.GETDATE()) 
 --AND K.EntityCode='MAN00100770'

 UNION ALL

 SELECT K.EntityId,K.EntityCode, K.EntityName,max(CAST(k.CreationDate AS DATE)) DocumentDate,'Traslado en Transito' AS 'TIPO ORDEN',
CASE TFO.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Entregado' WHEN 3 THEN 'Anulado' WHEN 4 THEN 'En Transito' END 'ESTADO'
 FROM Inventory .Kardex K WITH (NOLOCK)
 INNER JOIN Inventory .TransferOrder TFO WITH (NOLOCK) ON TFO.ID=K.EntityId AND TFO.CODE=K.EntityCode
 WHERE K.EntityName ='TransferOrder' AND TFO.Status<> 3 AND TFO.OrderType=3 and k.MovementType=1 and CAST(k.CreationDate AS DATE) BETWEEN @FECINI AND @FECHFIN
 --k.CreationDate BETWEEN CAST(DATEFROMPARTS(@AÑO, @MES, 1) AS DATETIME) AND CAST(DATEADD(MINUTE, -6, COMMON.GETDATE()) AS DATETIME)
 --k.CreationDate between '2024-06-01 00:00:00'and  DATEADD(MINUTE, -6, COMMON.GETDATE()) 
 --AND K.EntityCode='MAN00100770'
 group by K.EntityId,K.EntityCode, K.EntityName,TFO.Status

),

CTE_DETALLE_KARDEX_ORDENES_TRASLADO
AS
(
 SELECT 'ORDENES DE TRASLADO' 'TRANSACCION',year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
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
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.[TIPO ORDEN],KOT.[ESTADO],
 KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K WITH (NOLOCK)
 INNER JOIN CTE_KARDEX_ORDENES_TRASLADO AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='TransferOrder' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.[TIPO ORDEN],KOT.[ESTADO],KOT.DocumentDate
),

CTE_CONTABILIDAD_ORDENES_TRASLADO
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'ORDENES DE TRASLADO' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC(18,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC(18,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO',JV.ID 'ID COMPROBANTE'
  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_ORDENES_TRASLADO AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='TransferOrder'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT WITH (NOLOCK) ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
),

---****************************************************************************************************************************************-----
---************************************************DEVOLUCION DE ORDENES DE TRASLADO *******************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,CASE TFO.OrderType WHEN 1 THEN 'Traslado' WHEN 2 THEN 'Consumo' WHEN 3 THEN 'Traslado en Transito' END 'TIPO ORDEN',
 CASE TFO.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Entregado' WHEN 3 THEN 'Anulado' WHEN 4 THEN 'En Transito' END 'ESTADO'
 FROM Inventory .Kardex K WITH (NOLOCK)
 INNER JOIN Inventory .TransferOrderDevolution AS TOD WITH (NOLOCK) ON TOD.ID=K.EntityId AND TOD.CODE=EntityCode
INNER JOIN Inventory .TransferOrder TFO WITH (NOLOCK) ON TFO.ID=TOD.TransferOrderId 
 WHERE K.EntityName ='TransferOrderDevolution'AND TOD.Status<>3 AND TFO.OrderType<>2 AND CAST(K.DocumentDate AS DATE) BETWEEN @FECINI AND @FECHFIN
 --k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(MINUTE, -6, COMMON.GETDATE())-- AND K.EntityCode='PMA0000001923'
),

CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO_CONSUMO
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,CASE TFO.OrderType WHEN 1 THEN 'Traslado' WHEN 2 THEN 'Consumo' WHEN 3 THEN 'Traslado en Transito' END 'TIPO ORDEN',
 CASE TFO.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Entregado' WHEN 3 THEN 'Anulado' WHEN 4 THEN 'En Transito' END 'ESTADO',TFO.ID 'ID ORDEN TRASLADO',
 TFO.CODE 'CODIGO ORDEN',K.MovementType,K.AffectInventory
 FROM Inventory.Kardex K WITH (NOLOCK)
 INNER JOIN Inventory .TransferOrderDevolution AS TOD WITH (NOLOCK) ON TOD.ID=K.EntityId AND TOD.CODE=EntityCode
INNER JOIN Inventory .TransferOrder TFO WITH (NOLOCK) ON TFO.ID=TOD.TransferOrderId 
 WHERE K.EntityName ='TransferOrderDevolution'AND TOD.Status<>3 AND TFO.OrderType=2 AND CAST(K.DocumentDate AS DATE) BETWEEN @FECINI AND @FECHFIN
 --k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(MINUTE, -6, COMMON.GETDATE())
-- AND K.EntityCode='ALCH0000000050'
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
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.[TIPO ORDEN],KOT.[ESTADO],
 KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K WITH (NOLOCK)
 INNER JOIN CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK)  ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='TransferOrderDevolution' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.[TIPO ORDEN],KOT.[ESTADO],KOT.DocumentDate
),

CTE_DETALLE_KARDEX_DEVOLUCION_ORDENES_TRASLADO_CONSUMO
AS
(
 SELECT  'DEVOLUCION ORDENES DE TRASLADO' 'TRANSACCION',year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE KOT.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
 WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO' WHEN ControlStore = 1 THEN 'CONTROL' WHEN WareHouseType = 1 THEN 'REMANENTE'
 ELSE 'NINGUNO' END AS 'TIPO ALMACEN',
 IIF(KOT.MovementType=1,cast(SUM(todd.Quantity * tood.Value) as numeric(18,2)),0) 'TOTAL ENTRADAS', 
 IIF(Kot.MovementType=2,cast(SUM(todd.Quantity * tood.Value) as numeric(18,2)),0) 'TOTAL SALIDAS',
 CASE KOT.AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',KOT.EntityCode 'CODIGO DOCUMENTO',KOT.EntityId,KOT.[TIPO ORDEN],KOT.[ESTADO],
 KOT.DocumentDate 'FECHA BUSQUEDA'
  FROM    Inventory.TransferOrderDevolution tod WITH (NOLOCK)
        INNER JOIN CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO_CONSUMO AS KOT ON tod.ID=KOT.EntityId AND tod.CODE=KOT.EntityCode
        join Inventory.TransferOrderDevolutionDetail todd WITH (NOLOCK) on todd.TransferOrderDevolutionId = tod.Id
        join Inventory.TransferOrderDetailBatchSerial todbs WITH (NOLOCK) on todbs.Id = todd.TransferOrderDetailBatchSerialId
        join Inventory.TransferOrderDetail tood WITH (NOLOCK) on tood.Id = todbs.TransferOrderDetailId
        join Inventory.TransferOrder too WITH (NOLOCK) on too.Id = tood.TransferOrderId
        INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =tood.ProductId 
        INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK)  ON WH.Id =too.SourceWarehouseId
        INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
        INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
        INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
		WHERE KOT.EntityName ='TransferOrderDevolution' 
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),PG.CODE,PG.NAME,MA.Number,KOT.MovementType,WH.CODE,WH.NAME,VirtualStore,
  WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.AffectInventory,KOT.EntityCode,KOT.EntityId,KOT.[TIPO ORDEN],KOT.[ESTADO],KOT.DocumentDate
),

CTE_CONTABILIDAD_DEVOLUCION_ORDENES_TRASLADO
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO', 'DEVOLUCION ORDENES DE TRASLADO' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC(18,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC(18,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO',JV.ID 'ID COMPROBANTE'
  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='TransferOrderDevolution'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT WITH (NOLOCK) ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
),

CTE_CONTABILIDAD_DEVOLUCION_ORDENES_TRASLADO_CONSUMO
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO', 'DEVOLUCION ORDENES DE TRASLADO' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC(18,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC(18,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO',JV.ID 'ID COMPROBANTE'
  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO_CONSUMO AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='TransferOrderDevolution'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT WITH (NOLOCK) ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
),

---********************************************************************************************************************************************************-----
---********************************************************** COMPROBANTES DE ENTRADA **********************************************************************----
---*********************************************************************************************************************************************************----

CTE_KARDEX_COMPROBANTES_ENTRADA
AS
(
 SELECT DISTINCT K.EntityId,K.EntityCode,CAST(k.DocumentDate AS DATE) DocumentDate, K.EntityName ,AP.Id ID_CXP,AP.Code [CODIGO CXP],
 'SI' 'VALOR',EV.ValueTax 'VALOR IVA','Confirmado' 'ESTADO'
 FROM Inventory .Kardex K WITH (NOLOCK)
 INNER JOIN Payments.AccountPayable AS AP WITH (NOLOCK) ON K.EntityId=AP.EntityId AND K.EntityCode=AP.EntityCode
 INNER JOIN Inventory.EntranceVoucher EV WITH (NOLOCK) ON EV.ID=K.EntityId AND EV.CODE=K.EntityCode
 INNER JOIN Inventory.EntranceVoucherDetail AS EVD WITH (NOLOCK) ON EV.ID=EVD.EntranceVoucherId
 WHERE K.EntityName ='EntranceVoucher' AND EV.Status<> 3 AND CAST(K.DocumentDate AS DATE) BETWEEN @FECINI AND @FECHFIN
 --k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(MINUTE, -6, COMMON.GETDATE()) --AND K.EntityCode='MSGT0000000377'
),

CTE_DETALLE_KARDEX_COMPROBANTES_ENTRADA
AS
(
  SELECT 'COMPROBANTES DE ENTRADA' 'TRANSACCION',year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
  MA.Number 'CUENTA CONTABLE','ENTRADA' 'TIPO DE TRANSACCION', WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' 
  WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION' WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO'WHEN ControlStore = 1 THEN 'CONTROL' 
  WHEN WareHouseType = 1 THEN 'REMANENTE'ELSE 'NINGUNO' END AS 'TIPO ALMACEN',
  CAST(SUM(EVD.TotalValue) AS NUMERIC(18,2)) 'TOTAL ENTRADAS', 0 'TOTAL SALIDAS',
  CASE EVD.EntranceSource WHEN 1 THEN 'Ninguna' WHEN 2 THEN 'Orden de Compra' WHEN  3 THEN 'Contrato' WHEN 4 THEN 'Remisión de Entrada' 
  WHEN  5 THEN 'Remisión en Consignación' END 'TIPO ORDEN','SI' 'AFECTA INVENTARIO',KOT.EntityCode 'CODIGO DOCUMENTO',
   KOT.EntityId,MA.ID IdCuenta,KOT.[ESTADO],KOT.[CODIGO CXP] 'CODIGO INTERNO',KOT.DocumentDate 'FECHA BUSQUEDA'
  FROM Inventory.EntranceVoucher EV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_COMPROBANTES_ENTRADA KOT ON KOT.EntityId=EV.Id AND KOT.EntityCode=EV.Code
  INNER JOIN Inventory.EntranceVoucherDetail AS EVD WITH (NOLOCK) ON EV.ID=EVD.EntranceVoucherId
  INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =EVD.ProductId 
  INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =EV.WarehouseId
  INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
  INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),PG.CODE,PG.NAME,MA.Number,WH.CODE,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,
  TransitStore,ControlStore,WareHouseType,EVD.EntranceSource,KOT.EntityCode,KOT.EntityId,MA.ID,KOT.[ESTADO],KOT.[CODIGO CXP],KOT.DocumentDate
),

CTE_DETALLE_KARDEX_RECLASIFICACION_REMISIONES
AS
(
  SELECT 'RECLASIFICACION REMISIONES' 'TRANSACCION',year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
  MA.Number 'CUENTA CONTABLE','ENTRADA' 'TIPO DE TRANSACCION', WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' 
  WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION' WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO'WHEN ControlStore = 1 THEN 'CONTROL' 
  WHEN WareHouseType = 1 THEN 'REMANENTE'ELSE 'NINGUNO' END AS 'TIPO ALMACEN',
  0 'TOTAL ENTRADAS', CAST(SUM((REDBS.Quantity*RED.UnitValue)+RED.IvaValue) AS NUMERIC(18,2)) 'TOTAL SALIDAS',
  CASE EVD.EntranceSource WHEN 1 THEN 'Ninguna' WHEN 2 THEN 'Orden de Compra' WHEN  3 THEN 'Contrato' WHEN 4 THEN 'Remisión de Entrada' 
  WHEN  5 THEN 'Remisión en Consignación' END 'TIPO ORDEN','SI' 'AFECTA INVENTARIO',KOT.EntityCode 'CODIGO DOCUMENTO',
   KOT.EntityId,MA.ID IdCuenta,KOT.[ESTADO],KOT.[CODIGO CXP] 'CODIGO INTERNO',KOT.DocumentDate 'FECHA BUSQUEDA'
  FROM Inventory.EntranceVoucher EV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_COMPROBANTES_ENTRADA KOT ON KOT.EntityId=EV.Id AND KOT.EntityCode=EV.Code
  INNER JOIN Inventory.EntranceVoucherDetail AS EVD WITH (NOLOCK) ON EV.ID=EVD.EntranceVoucherId
  --INNER JOIN Inventory.RemissionEntrance AS RE WITH (NOLOCK) ON RE.Code=EVD.SourceCode
  INNER JOIN Inventory.RemissionEntranceDetailBatchSerial AS REDBS WITH (NOLOCK) ON REDBS.Id=EVD.RemissionEntranceDetailBatchSerialId
  INNER JOIN Inventory.RemissionEntranceDetail as RED WITH (NOLOCK) ON RED.Id=REDBS.RemissionEntranceDetailId
  INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =RED.ProductId 
  INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =EV.WarehouseId
  INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
  INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
  WHERE EVD.EntranceSource=4
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),PG.CODE,PG.NAME,MA.Number,WH.CODE,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,
  TransitStore,ControlStore,WareHouseType,EVD.EntranceSource,KOT.EntityCode,KOT.EntityId,MA.ID,KOT.[ESTADO],KOT.[CODIGO CXP],KOT.DocumentDate
),

CTE_CONTABILIDAD_COMPROBANTES_ENTRADA
AS
(
  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  KOT.EntityCode 'CODIGO DOCUMENTO',  'COMPROBANTES DE ENTRADA' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',
  SUM(CAST(DebitValue AS NUMERIC(18,2))) 'VALOR DEBITO',SUM(CAST(CreditValue AS NUMERIC(18,2))) 'VALOR CREDITO', 
  JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.[CODIGO CXP] 'CODIGO INTERNO',KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO',JV.ID 'ID COMPROBANTE'
  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_COMPROBANTES_ENTRADA AS KOT ON KOT.ID_CXP=JV.EntityId AND KOT.[CODIGO CXP]=JV.EntityCode AND JV.EntityName='AccountPayable'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT WITH (NOLOCK) ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.EntityCode,KOT.[CODIGO CXP],KOT.DocumentDate,JV.ID
),

CTE_CONTABILIDAD_COMPROBANTES_RECLASIFICACION_REMISIONES
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  KOT.EntityCode 'CODIGO DOCUMENTO',  'RECLASIFICACION REMISIONES' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',
  SUM(CAST(DebitValue AS NUMERIC(18,2))) 'VALOR DEBITO',SUM(CAST(CreditValue AS NUMERIC(18,2))) 'VALOR CREDITO', 
  JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.[CODIGO CXP] 'CODIGO INTERNO',KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO',JV.ID 'ID COMPROBANTE'
  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_COMPROBANTES_ENTRADA AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='RemissionReclassification'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT WITH (NOLOCK) ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.EntityCode,KOT.[CODIGO CXP],KOT.DocumentDate,JV.ID
),

---*********************************************************************************************************************************************************----
---****************************************************** DEVOLUCION COMPROBANTES DE ENTRADA ***************************************************************----
---*********************************************************************************************************************************************************----

CTE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode,CAST(k.DocumentDate AS DATE) DocumentDate, K.EntityName,PN.ID ID_NOTA,PN.CODE 'CODIGO NOTA',EVD.ID ID_DEVO,EVD.CODE 'CODIGO DEV', EVD.ValueTax 'VALOR IVA',
CASE EVD.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Confirmado' WHEN 3 THEN 'Anulado' END 'ESTADO'
 FROM Inventory .Kardex K WITH (NOLOCK)
 INNER JOIN Inventory.EntranceVoucherDevolution EVD WITH (NOLOCK) ON EVD.ID=K.EntityId AND EVD.CODE=EntityCode
 INNER JOIN Payments.PaymentNotes as PN WITH (NOLOCK) ON PN.Entitycode=EVD.Code and PN.EntityId= EVD.Id
 WHERE K.EntityName ='EntranceVoucherDevolution'AND EVD.Status<>3 AND CAST(K.DocumentDate AS DATE) BETWEEN @FECINI AND @FECHFIN
 --k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(MINUTE, -6, COMMON.GETDATE())-- AND K.EntityCode='ACBA0000004270'
),

CTE_DETALLE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA
AS
(
 SELECT 'DEVOLUCION DE COMPRAS' 'TRANSACCION',year(K.DocumentDate) 'AÑO',month(K.DocumentDate) 'MES',DAY(K.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE K.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
        WHEN CustodyStore = 1 THEN 'CUSTODIA'WHEN TransitStore = 1 THEN 'TRANSITO'WHEN ControlStore = 1 THEN 'CONTROL'WHEN WareHouseType = 1 THEN 'REMANENTE'
        ELSE 'NINGUNO'END AS 'TIPO ALMACEN',
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,'Devoluciones en Compras' [TIPO ORDEN],
 KOT.[ESTADO],KOT.[CODIGO NOTA] 'CODIGO INTERNO',KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K WITH (NOLOCK)
 INNER JOIN CTE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA AS KOT ON KOT.ID_DEVO=K.EntityId AND KOT.[CODIGO DEV]=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='EntranceVoucherDevolution' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.[CODIGO NOTA],KOT.[ESTADO],KOT.DocumentDate
),

CTE_CONTABILIDAD_DEVOLUCION_COMPROBANTES_ENTRADA
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  KOT.EntityCode 'CODIGO DOCUMENTO', 'DEVOLUCION DE COMPRAS' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',
  SUM(CAST(DebitValue AS NUMERIC(18,2))) 'VALOR DEBITO',SUM(CAST(CreditValue AS NUMERIC(18,2))) 'VALOR CREDITO', KOT.[CODIGO NOTA] 'CODIGO INTERNO',
  JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO',JV.ID 'ID COMPROBANTE'
  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA AS KOT ON KOT.ID_NOTA=JV.EntityId AND KOT.[CODIGO NOTA]=JV.EntityCode AND JV.EntityName='PaymentNotes'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT WITH (NOLOCK) ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.EntityCode,KOT.[CODIGO NOTA],KOT.DocumentDate,JV.ID
),

---****************************************************************************************************************************************-----
---************************************************ REMISIONES DE ENTRADA *************************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_REMISIONES_ENTRADA
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K WITH (NOLOCK)
 WHERE K.EntityName ='RemissionEntrance' AND CAST(K.DocumentDate AS DATE) BETWEEN @FECINI AND @FECHFIN
 --k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(MINUTE, -6, COMMON.GETDATE())-- AND K.EntityCode='MSGT0000000146'
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
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K WITH (NOLOCK)
 INNER JOIN CTE_KARDEX_REMISIONES_ENTRADA AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='RemissionEntrance' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
),

CTE_CONTABILIDAD_REMISIONES_ENTRADA
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'REMISION DE ENTRADA' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC(18,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC(18,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO',JV.ID 'ID COMPROBANTE'
  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_REMISIONES_ENTRADA AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='RemissionEntrance'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK)  ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT WITH (NOLOCK) ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
),

---****************************************************************************************************************************************-----
---************************************************DEVOLUCION REMISION DE ENTRADA *************************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_DEVOLUCION_REMISION_ENTRADA
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K WITH (NOLOCK)
 INNER JOIN Inventory.RemissionDevolution AS RV WITH (NOLOCK) ON RV.Id=k.EntityId and RV.Code=K.EntityCode
 WHERE K.EntityName ='RemissionDevolution' AND RV.DevolutionType=1 AND CAST(K.DocumentDate AS DATE) BETWEEN @FECINI AND @FECHFIN
 --k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(MINUTE, -6, COMMON.GETDATE())  --AND K.EntityCode='MSGT0000000146'
),

CTE_DETALLE_KARDEX_DEVOLUCION_REMISION_ENTRADA
AS
(
 SELECT 'DEVOLUCION REMISION DE ENTRADA' 'TRANSACCION',year(K.DocumentDate) 'AÑO',month(K.DocumentDate) 'MES',DAY(K.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE K.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
        WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO' WHEN ControlStore = 1 THEN 'CONTROL' WHEN WareHouseType = 1 THEN 'REMANENTE'
        ELSE 'NINGUNO'END AS 'TIPO ALMACEN',KOT.[ESTADO],
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K WITH (NOLOCK)
 INNER JOIN CTE_KARDEX_DEVOLUCION_REMISION_ENTRADA AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='RemissionDevolution' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
),

CTE_CONTABILIDAD_DEVOLUCION_REMISIONES_ENTRADA
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'DEVOLUCION REMISION DE ENTRADA' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC(18,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC(18,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO',JV.ID 'ID COMPROBANTE'
  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_DEVOLUCION_REMISION_ENTRADA AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='RemissionEntranceDevolution'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT WITH (NOLOCK) ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
),

---****************************************************************************************************************************************-----
---*****************************************REMISIONES DE INVENTARIO EN CONSIGNACION*********************************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_REMISIONES_CONSIGNACION
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K WITH (NOLOCK)
 WHERE K.EntityName ='ConsignmentInventoryRemission' AND CAST(K.DocumentDate AS DATE) BETWEEN @FECINI AND @FECHFIN
 --k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(MINUTE, -6, COMMON.GETDATE())  --AND K.EntityCode='ALCE0000000249'
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
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K WITH (NOLOCK)
 INNER JOIN CTE_KARDEX_REMISIONES_CONSIGNACION AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='ConsignmentInventoryRemission' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
),

CTE_CONTABILIDAD_REMISIONES_CONSIGNACION
AS
(
  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'REMISION DE INVENTARIO CONSIGNACION' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC(18,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC(18,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO',JV.ID 'ID COMPROBANTE'
  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_REMISIONES_CONSIGNACION AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='ConsignmentInventoryRemission'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT WITH (NOLOCK) ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
),

---****************************************************************************************************************************************-----
---************************************************DEVOLUCION REMISION DE INVENTARIO CONSIGNACION*********************************************----
---*****************************************************************************************************************************************----

CTE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K WITH (NOLOCK)
 INNER JOIN Inventory.RemissionDevolution AS RV WITH (NOLOCK) ON RV.Id=k.EntityId and RV.Code=K.EntityCode
 WHERE K.EntityName ='RemissionDevolution' AND RV.DevolutionType=3 AND CAST(K.DocumentDate AS DATE) BETWEEN @FECINI AND @FECHFIN
 --k.DocumentDate between '2024-06-01 00:00:00'and  DATEADD(MINUTE, -6, COMMON.GETDATE()) -- AND K.EntityCode='PMCT0000000016'
),

CTE_DETALLE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION
AS
(
 SELECT 'DEVOLUCION REMISION DE ENTRADA' 'TRANSACCION',year(K.DocumentDate) 'AÑO',month(K.DocumentDate) 'MES',DAY(K.DocumentDate) 'DIA',PG.CODE 'CODIGO GRUPO',PG.NAME 'GRUPO PRODUCTO',
 MA.Number 'CUENTA CONTABLE',CASE K.MovementType WHEN 1 THEN 'ENTRADA' WHEN 2 THEN 'SALIDA' END 'TIPO DE TRANSACCION',
 WH.CODE 'CODIGO ALMACEN', WH.NAME 'NOMBRE ALMACEN',CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
        WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO' WHEN ControlStore = 1 THEN 'CONTROL' WHEN WareHouseType = 1 THEN 'REMANENTE'
        ELSE 'NINGUNO'END AS 'TIPO ALMACEN',KOT.[ESTADO],
 IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) 'TOTAL ENTRADAS', IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) 'TOTAL SALIDAS',
 CASE AffectInventory WHEN 1 THEN 'SI' ELSE 'NO' END 'AFECTA INVENTARIO',K.EntityCode 'CODIGO DOCUMENTO',K.EntityCode,K.EntityId,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA'
FROM Inventory.Kardex K WITH (NOLOCK)
 INNER JOIN CTE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
 WHERE K.EntityName ='RemissionDevolution' 
 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
),

CTE_CONTABILIDAD_DEVOLUCION_REMISIONES_CONSIGNACION
AS
(

  SELECT year(KOT.DocumentDate) 'AÑO',month(KOT.DocumentDate) 'MES',DAY(KOT.DocumentDate) 'DIA',JV.CONSECUTIVE 'COMPROBANTE CONTABLE',JVT.Code +' - '+ JVT.Name 'TIPO COMPROBANTE',
  JV.EntityCode 'CODIGO DOCUMENTO',  'DEVOLUCION REMISION DE ENTRADA' 'TRANSACCION',MA.NUMBER 'CUENTA CONTABILIDAD',SUM(CAST(DebitValue AS NUMERIC(18,2))) 'VALOR DEBITO',
  SUM(CAST(CreditValue AS NUMERIC(18,2))) 'VALOR CREDITO', JV.EntityId,JV.EntityCode,JV.EntityName,MA.ID IdCuenta,KOT.DocumentDate 'FECHA BUSQUEDA','Confirmado' 'ESTADO',JV.ID 'ID COMPROBANTE'
  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='ConsignmentInventoryRemissionDevolution'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
  INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT WITH (NOLOCK) ON JVT.ID= JV.IdJournalVoucher
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JVT.Code +' - '+ JVT.Name,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
),

CTE_DATOS_MOSTRAR
AS
(
------------------------------******************DISPENSACIONES Y DEVOLUCION DE DISPENSACIONES ***********************------------------------------

SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],'Dispensación Farmacéutica' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,000 'ID COMPROBANTE',000 [COMPROBANTE CONTABLE],'N/A' [TIPO COMPROBANTE]
FROM CTE_DETALLE_KARDEX_DISPENSACIONES
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Dispensación Farmacéutica' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,[ID COMPROBANTE],[COMPROBANTE CONTABLE],[TIPO COMPROBANTE]
FROM CTE_CONTABILIDAD_DISPENSACIONES
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,'Devolución Dispensación' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,000 [ID COMPROBANTE],000 [COMPROBANTE CONTABLE],'N/A' [TIPO COMPROBANTE]
FROM CTE_DETALLE_KARDEX_DEVOLUCION_DISPENSACIONES
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Devolución Dispensación' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,[ID COMPROBANTE],[COMPROBANTE CONTABLE],[TIPO COMPROBANTE]
FROM CTE_CONTABILIDAD_DEVOLUCION_DISPENSACIONES
------------------------------**********************************************************************************************************------------------------------
UNION ALL
------------------------------************************ AJUSTES DE INVENTARIOS **********************************************************------------------------------
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],'Ajuste' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,000 [ID COMPROBANTE],000 [COMPROBANTE CONTABLE],'N/A' [TIPO COMPROBANTE]
FROM CTE_DETALLE_KARDEX_AJUSTES
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Ajuste' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,[ID COMPROBANTE],[COMPROBANTE CONTABLE],[TIPO COMPROBANTE]
FROM CTE_CONTABILIDAD_AJUSTES
------------------------------**********************************************************************************************************------------------------------
UNION ALL
------------------------------*********************************** PRESTAMOS *********************************************------------------------------
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],'Prestamos' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,000 [ID COMPROBANTE],000 [COMPROBANTE CONTABLE],'N/A' [TIPO COMPROBANTE]
FROM CTE_DETALLE_KARDEX_PRESTAMOS
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Prestamos' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA, [ID COMPROBANTE],[COMPROBANTE CONTABLE],[TIPO COMPROBANTE]
FROM CTE_CONTABILIDAD_PRESTAMOS
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,'Devolución de Prestamos' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,000 [ID COMPROBANTE],000 [COMPROBANTE CONTABLE],'N/A' [TIPO COMPROBANTE]
FROM CTE_DETALLE_KARDEX_DEVOLUCION_PRESTAMOS
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Devolución de Prestamos' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,[ID COMPROBANTE],[COMPROBANTE CONTABLE],[TIPO COMPROBANTE]
FROM CTE_CONTABILIDAD_DEVOLUCION_PRESTAMOS
------------------------------**********************************************************************************************************------------------------------
UNION ALL
------------------------------************************** ORDENES DE TRASALDOS *****************************************************------------------------------
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],[TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,000 [ID COMPROBANTE],000 [COMPROBANTE CONTABLE],'N/A' [TIPO COMPROBANTE]
FROM CTE_DETALLE_KARDEX_ORDENES_TRASLADO
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Ordene de Traslado' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,[ID COMPROBANTE],[COMPROBANTE CONTABLE],[TIPO COMPROBANTE]
FROM CTE_CONTABILIDAD_ORDENES_TRASLADO
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,[TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,000 [ID COMPROBANTE],000 [COMPROBANTE CONTABLE],'N/A' [TIPO COMPROBANTE]
FROM CTE_DETALLE_KARDEX_DEVOLUCION_ORDENES_TRASLADO
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,[TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,000 [ID COMPROBANTE],000 [COMPROBANTE CONTABLE],'N/A' [TIPO COMPROBANTE]
FROM CTE_DETALLE_KARDEX_DEVOLUCION_ORDENES_TRASLADO_CONSUMO
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Ordene de Traslado' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,[ID COMPROBANTE],[COMPROBANTE CONTABLE],[TIPO COMPROBANTE]
FROM CTE_CONTABILIDAD_DEVOLUCION_ORDENES_TRASLADO
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Ordene de Traslado' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,[ID COMPROBANTE],[COMPROBANTE CONTABLE],[TIPO COMPROBANTE]
FROM CTE_CONTABILIDAD_DEVOLUCION_ORDENES_TRASLADO_CONSUMO
------------------------------************************************************** *****************************************************------------------------------
UNION ALL
------------------------------************************** COMPROBANTES DE ENTRADA *****************************************************------------------------------
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],[TIPO ORDEN],[ESTADO],
[CODIGO INTERNO],[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,000 [ID COMPROBANTE],000 [COMPROBANTE CONTABLE],'N/A' [TIPO COMPROBANTE]
FROM CTE_DETALLE_KARDEX_COMPROBANTES_ENTRADA
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],[TIPO ORDEN],[ESTADO],
[CODIGO INTERNO],[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,000 [ID COMPROBANTE],000 [COMPROBANTE CONTABLE],'N/A' [TIPO COMPROBANTE]
FROM CTE_DETALLE_KARDEX_RECLASIFICACION_REMISIONES
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Comprobante Entrada' [TIPO ORDEN],[ESTADO],
[CODIGO INTERNO],[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,[ID COMPROBANTE],[COMPROBANTE CONTABLE],[TIPO COMPROBANTE]
FROM CTE_CONTABILIDAD_COMPROBANTES_ENTRADA
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Reclasificación remisión' [TIPO ORDEN],[ESTADO],
[CODIGO INTERNO],[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,[ID COMPROBANTE],[COMPROBANTE CONTABLE],[TIPO COMPROBANTE]
FROM CTE_CONTABILIDAD_COMPROBANTES_RECLASIFICACION_REMISIONES
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,[TIPO ORDEN],[ESTADO],
[CODIGO INTERNO],[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,000 [ID COMPROBANTE],000 [COMPROBANTE CONTABLE],'N/A' [TIPO COMPROBANTE]
FROM CTE_DETALLE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Comprobante Entrada' [TIPO ORDEN],[ESTADO],
[CODIGO INTERNO],[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,[ID COMPROBANTE],[COMPROBANTE CONTABLE],[TIPO COMPROBANTE]
FROM CTE_CONTABILIDAD_DEVOLUCION_COMPROBANTES_ENTRADA
------------------------------***************************************************************************************************------------------------------
UNION ALL
------------------------------**************************** REMISION DE ENTRADA ******************************************------------------------------
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],'Remisión de Entrada' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,000 [ID COMPROBANTE],000 [COMPROBANTE CONTABLE],'N/A' [TIPO COMPROBANTE]
FROM CTE_DETALLE_KARDEX_REMISIONES_ENTRADA
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Remisión de Entrada' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,[ID COMPROBANTE],[COMPROBANTE CONTABLE],[TIPO COMPROBANTE]
FROM CTE_CONTABILIDAD_REMISIONES_ENTRADA
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,'Devolución Remisión de Entrada' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,000 [ID COMPROBANTE],000 [COMPROBANTE CONTABLE],'N/A' [TIPO COMPROBANTE]
FROM CTE_DETALLE_KARDEX_DEVOLUCION_REMISION_ENTRADA
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Devolución Remisión de Entrada' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,[ID COMPROBANTE],[COMPROBANTE CONTABLE],[TIPO COMPROBANTE]
FROM CTE_CONTABILIDAD_DEVOLUCION_REMISIONES_ENTRADA
------------------------------***************************************************************************************************------------------------------
UNION ALL
------------------------------**************************** REMISION DE INVENTARIOS CONSIGNACION ***********************************------------------------------
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO],'Remisión Inventario Consignación' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,000 [ID COMPROBANTE],000 [COMPROBANTE CONTABLE],'N/A' [TIPO COMPROBANTE]
FROM CTE_DETALLE_KARDEX_REMISIONES_CONSIGNACION
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Remisión Inventario Consignación' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA, [ID COMPROBANTE],[COMPROBANTE CONTABLE],[TIPO COMPROBANTE]
FROM CTE_CONTABILIDAD_REMISIONES_CONSIGNACION
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'INVENTARIOS' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],[CODIGO GRUPO],[GRUPO PRODUCTO],[CUENTA CONTABLE],[NOMBRE ALMACEN],[TIPO DE TRANSACCION],[TIPO ALMACEN],
[TOTAL ENTRADAS],[TOTAL SALIDAS] ,[AFECTA INVENTARIO],[CODIGO DOCUMENTO] ,'Devolución Remisión Inventario Consignación' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,000 [ID COMPROBANTE],000 [COMPROBANTE CONTABLE],'N/A' [TIPO COMPROBANTE]
FROM CTE_DETALLE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION
UNION ALL
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,'CONTABILIDAD' 'MODULO',[TRANSACCION],[AÑO],[MES],[DIA],'' [CODIGO GRUPO],'' [GRUPO PRODUCTO],[CUENTA CONTABILIDAD],'' [NOMBRE ALMACEN],
'' [TIPO DE TRANSACCION],'NINGUNO' [TIPO ALMACEN],[VALOR DEBITO],[VALOR CREDITO] ,'' [AFECTA INVENTARIO] ,[CODIGO DOCUMENTO],'Devolución Remisión Inventario Consignación' [TIPO ORDEN],[ESTADO],
[CODIGO DOCUMENTO] 'CODIGO INTERNO',[FECHA BUSQUEDA],CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA,[ID COMPROBANTE],[COMPROBANTE CONTABLE],[TIPO COMPROBANTE]
FROM CTE_CONTABILIDAD_DEVOLUCION_REMISIONES_CONSIGNACION
)

SELECT * FROM CTE_DATOS_MOSTRAR
--WHERE [CODIGO DOCUMENTO]='MAN00098691'
GO
GRANT EXECUTE
    ON OBJECT::[Report].[SP_V2_INV_CONCILIACION_INVENTARIOS_CONTABILIDAD_FINAL] TO [odopbi]
    AS [dbo];
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento de reporte que concilia, para un rango de fechas dado, los movimientos del kardex de inventario contra los comprobantes contables generados en el libro mayor, abarcando transacciones de dispensaciones farmacéuticas, devoluciones de dispensaciones, ajustes de inventario, órdenes de traslado y entradas de almacén. Por cada tipo de transacción compara entradas/salidas valoradas (cantidad × valor) por grupo de producto y cuenta contable PUC, frente a los débitos/créditos registrados en los vouchers contables, permitiendo detectar diferencias entre el módulo de inventario y contabilidad.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_INV_CONCILIACION_INVENTARIOS_CONTABILIDAD_FINAL';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_INV_CONCILIACION_INVENTARIOS_CONTABILIDAD_FINAL';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporte que concilia los movimientos de inventario (kardex) contra sus comprobantes contables del libro legal, agrupando por tipo de transacción (dispensaciones, ajustes, préstamos, traslados, comprobantes de entrada, remisiones y consignación) en un rango de fechas.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_INV_CONCILIACION_INVENTARIOS_CONTABILIDAD_FINAL';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los parámetros @FECINI y @FECHFIN deben definir el rango de fechas a conciliar (filtro sobre Kardex.DocumentDate y, para traslados en tránsito, sobre Kardex.CreationDate).; Cada ProductGroup debe tener configurado InventoryAccountPayableConceptId y este a su vez una cuenta contable (MainAccounts) para poder cruzar el kardex con la contabilidad.; Los comprobantes contables considerados deben pertenecer al libro legal (JournalVouchers.LegalBookId = 1).; Para comprobantes de entrada debe existir una cuenta por pagar (Payments.AccountPayable) asociada al mismo EntityId/EntityCode del kardex.; Para devoluciones de comprobantes de entrada debe existir una nota de pago (Payments.PaymentNotes) cuyo EntityId/EntityCode corresponde a la devolución (EntranceVoucherDevolution).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_INV_CONCILIACION_INVENTARIOS_CONTABILIDAD_FINAL';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se concilian comprobantes contables del libro legal (JournalVouchers.LegalBookId = 1).; Todos los registros del lado de inventario se etiquetan con MODULO=''INVENTARIOS'' y los del lado contable con MODULO=''CONTABILIDAD''.; El estado del documento se reporta siempre como ''Confirmado'' (excepto en órdenes de traslado y devoluciones de comprobantes donde se mapea desde Status: 1=Registrado, 2=Entregado/Confirmado, 3=Anulado, 4=En Tránsito).; Documentos anulados (Status=3) nunca aparecen en el reporte.; El cruce inventario↔contabilidad se hace por EntityId+EntityCode+EntityName, salvo en comprobantes de entrada (cruce vía AccountPayable: ID_CXP/CODIGO CXP con EntityName=''AccountPayable'') y devoluciones de entrada (cruce vía PaymentNotes con EntityName=''PaymentNotes'').; La cuenta contable del lado inventario se obtiene siempre vía ProductGroup → AccountPayableConcepts → MainAccounts.; Los valores monetarios se truncan a NUMERIC(18,2).; La fecha de actualización del reporte (ULT_ACTUA) se calcula con COMMON.GETDATE() convertido a ''Pakistan Standard Time''.; El ID de la compañía se reporta como los primeros 9 caracteres de DB_NAME().', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_INV_CONCILIACION_INVENTARIOS_CONTABILIDAD_FINAL';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT_SET: Devuelve un único conjunto de resultados (UNION ALL de todos los CTEs) con columnas estandarizadas (ID_COMPANY, MODULO ''INVENTARIOS''/''CONTABILIDAD'', TRANSACCION, AÑO/MES/DIA, cuenta, almacén, totales, estado, tipo orden, comprobante contable, etc.) para conciliar inventario vs contabilidad.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_INV_CONCILIACION_INVENTARIOS_CONTABILIDAD_FINAL';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Kardex.EntityName se evalúa por tipo de documento (''PharmaceuticalDispensing'',''PharmaceuticalDispensingDevolution'',''InventoryAdjustment'',''LoanMerchandise'',''LoanMerchandiseDevolution'',''TransferOrder'',''TransferOrderDevolution'',''EntranceVoucher'',''EntranceVoucherDevolution'',''RemissionEntrance'',''RemissionDevolution'',''ConsignmentInventoryRemission'') → Cada EntityName alimenta su propio bloque de CTE (kardex + contabilidad) y luego se unifica vía UNION ALL en CTE_DATOS_MOSTRAR.; si Kardex.MovementType = 1 → Se reporta como ''ENTRADA'' y el valor Quantity*Value se acumula en TOTAL ENTRADAS. else Si MovementType=2 se reporta como ''SALIDA'' y el valor se acumula en TOTAL SALIDAS.; si Clasificación del almacén: VirtualStore=1, WarehouseConsignment=1, CustodyStore=1, TransitStore=1, ControlStore=1 o WareHouseType=1 → Se etiqueta TIPO ALMACEN como VIRTUAL/CONSIGNACION/CUSTODIA/TRANSITO/CONTROL/REMANENTE respectivamente. else Se etiqueta como ''NINGUNO''.; si TransferOrder.OrderType y TransferOrder.Status para órdenes de traslado: se excluyen Status=3 (Anulado) y para ''TransferOrder'' se excluye OrderType=3 en el bloque normal → Solo entran al reporte traslados activos; las órdenes ''Traslado en Tránsito'' (OrderType=3) se incluyen vía UNION ALL adicional usando CreationDate y MovementType=1.; si TransferOrderDevolution: TFO.OrderType <> 2 (no consumo) → Se procesa por el flujo estándar de kardex (CTE_DETALLE_KARDEX_DEVOLUCION_ORDENES_TRASLADO). else Si OrderType=2 (Consumo) se procesa con un flujo alterno que toma cantidades y valores desde TransferOrderDevolutionDetail/TransferOrderDetailBatchSerial/TransferOrderDetail (CTE_..._CONSUMO).; si RemissionDevolution.DevolutionType = 1 → Se trata como devolución de Remisión de Entrada. else Si DevolutionType=3 se trata como devolución de Remisión de Inventario en Consignación.; si EntranceVoucherDetail.EntranceSource = 4 (Remisión de Entrada) → Se genera adicionalmente la fila de ''RECLASIFICACION REMISIONES'' calculando el valor como SUM((REDBS.Quantity*RED.UnitValue)+RED.IvaValue) como salida.; si Status de documentos: EntranceVoucher.Status<>3, EntranceVoucherDevolution.Status<>3, TransferOrder.Status<>3, TransferOrderDevolution.Status<>3 → Se excluyen los documentos anulados de la conciliación.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_INV_CONCILIACION_INVENTARIOS_CONTABILIDAD_FINAL';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_INV_CONCILIACION_INVENTARIOS_CONTABILIDAD_FINAL';
-- GO
