
-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2017-10-19
-- Refactoring: Cristian Camilo Bahamon
-- Refactoring date: 2024-07-02
-- Description:	Conciliación de los saldos del mes entre inventario y contabilidad
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ConciliationInventory]
	@MonthClosed AS INT,
	@YearClosed AS INT
AS
BEGIN	

	;WITH CTE_KARDEX_DISPENSACIONES
	AS
	(
		SELECT DISTINCT K.EntityId, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
		FROM Inventory .Kardex K WITH (NOLOCK)
		WHERE K.EntityName ='PharmaceuticalDispensing' 
		AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_DETALLE_KARDEX_DISPENSACIONES
	AS
	(
	 SELECT 
			year(K.DocumentDate) [Year],
			month(K.DocumentDate) [Month],
			k.EntityName, 
			'DISPENSACION FARMACEUTICA' [Transaction],
			ma.id MainAccountId,
			MA.Number MainAccountNumber,
			IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) DebitValue,
			IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) CreditValue,
			CASE 
				WHEN VirtualStore = 1 THEN 'VIRTUAL' 
				WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
				WHEN CustodyStore = 1 THEN 'CUSTODIA'
				WHEN TransitStore = 1 THEN 'TRANSITO'
				WHEN ControlStore = 1 THEN 'CONTROL'
				WHEN WareHouseType = 1 THEN 'REMANENTE'
				ELSE 'NINGUNO'
			END AS WareHouseTypeName,
			KOT.[ESTADO] Status
	FROM Inventory.Kardex K WITH (NOLOCK)
	 INNER JOIN CTE_KARDEX_DISPENSACIONES AS KOT ON KOT.EntityId=K.EntityId --AND KOT.EntityCode=K.EntityCode
	 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
	 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
	 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
	 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) on apc.Id = PG.InventoryAccountPayableConceptId 
	 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
	 WHERE K.EntityName ='PharmaceuticalDispensing' 
	 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,k.EntityName,
	 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
	),

	CTE_KARDEX_DEVOLUCION_DISPENSACIONES
	AS
	(
		SELECT DISTINCT K.EntityId, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
		FROM Inventory .Kardex K WITH (NOLOCK)
		WHERE K.EntityName ='PharmaceuticalDispensingDevolution' 
		AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_DETALLE_KARDEX_DEVOLUCION_DISPENSACIONES
	AS
	(
	 SELECT 
		year(K.DocumentDate) [Year],
		month(K.DocumentDate) [Month],
		k.EntityName, 
		'DEVOLUCION DISPENSACION FARMACEUTICA' [Transaction],
		ma.Id MainAccountId,
		MA.Number MainAccountNumber,
		IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) DebitValue, 
		IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) CreditValue,
		CASE 
			WHEN VirtualStore = 1 THEN 'VIRTUAL' 
			WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
			WHEN CustodyStore = 1 THEN 'CUSTODIA'
			WHEN TransitStore = 1 THEN 'TRANSITO'
			WHEN ControlStore = 1 THEN 'CONTROL'
			WHEN WareHouseType = 1 THEN 'REMANENTE'
			ELSE 'NINGUNO'
		END AS WareHouseTypeName,
		KOT.[ESTADO] Status
	FROM Inventory.Kardex K WITH (NOLOCK)
	 INNER JOIN CTE_KARDEX_DEVOLUCION_DISPENSACIONES AS KOT ON KOT.EntityId=K.EntityId --AND KOT.EntityCode=K.EntityCode
	 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
	 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
	 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
	 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
	 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
	 WHERE K.EntityName ='PharmaceuticalDispensingDevolution' 
	 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,k.EntityName,
	 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
	),

	CTE_KARDEX_AJUSTES
	AS
	(
		SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
		FROM Inventory .Kardex K WITH (NOLOCK)
		WHERE K.EntityName ='InventoryAdjustment' 
		AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_DETALLE_KARDEX_AJUSTES
	AS
	(
	 SELECT 
		year(K.DocumentDate) [Year],
		month(K.DocumentDate) [Month],
		k.EntityName, 
		'AJUSTE DE INVENTARIOS' [Transaction],
		ma.id MainAccountId,
		MA.Number MainAccountNumber,
		IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) DebitValue,
		IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) CreditValue,
		CASE 
			WHEN VirtualStore = 1 THEN 'VIRTUAL' 
			WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
			WHEN CustodyStore = 1 THEN 'CUSTODIA'
			WHEN TransitStore = 1 THEN 'TRANSITO'
			WHEN ControlStore = 1 THEN 'CONTROL'
			WHEN WareHouseType = 1 THEN 'REMANENTE'
			ELSE 'NINGUNO'
		END AS WareHouseTypeName,
		KOT.[ESTADO] Status
	FROM Inventory.Kardex K WITH (NOLOCK)
	 INNER JOIN CTE_KARDEX_AJUSTES AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
	 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
	 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
	 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK)  ON PG.Id=PRO.ProductGroupId
	 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
	 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
	 WHERE K.EntityName ='InventoryAdjustment' 
	 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,k.EntityName,
	 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
	),

	CTE_KARDEX_PRESTAMOS
	AS
	(
		SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
		FROM Inventory.Kardex K WITH (NOLOCK)
		WHERE K.EntityName ='LoanMerchandise' 
		AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_DETALLE_KARDEX_PRESTAMOS
	AS
	(
	 SELECT 
		year(K.DocumentDate) [Year],
		month(K.DocumentDate) [Month],
		k.EntityName, 
		'PRESTAMOS DE MERCANCIAS' [Transaction],
	    ma.id MainAccountId,
		MA.Number MainAccountNumber,
	    IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) DebitValue,
	    IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) CreditValue,
		CASE 
			WHEN VirtualStore = 1 THEN 'VIRTUAL' 
			WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
			WHEN CustodyStore = 1 THEN 'CUSTODIA'
			WHEN TransitStore = 1 THEN 'TRANSITO'
			WHEN ControlStore = 1 THEN 'CONTROL'
			WHEN WareHouseType = 1 THEN 'REMANENTE'
			ELSE 'NINGUNO'
		END AS WareHouseTypeName,
		KOT.[ESTADO] Status
	 FROM Inventory.Kardex K WITH (NOLOCK)
	 INNER JOIN CTE_KARDEX_PRESTAMOS AS KOT WITH (NOLOCK) ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
	 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
	 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
	 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
	 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
	 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
	 WHERE K.EntityName ='LoanMerchandise' 
	 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,k.EntityName,
	 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
	),

	CTE_KARDEX_DEVOLUCION_PRESTAMOS
	AS
	(
	SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,k.DocumentDate,'Confirmado' 'ESTADO'
		FROM Inventory .Kardex K WITH (NOLOCK)
		WHERE K.EntityName ='LoanMerchandiseDevolution' 
		AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_DETALLE_KARDEX_DEVOLUCION_PRESTAMOS
	AS
	(
	 SELECT 
		year(K.DocumentDate) [Year],
		month(K.DocumentDate) [Month],
		k.EntityName,
		'DEVOLUCION PRESTAMOS DE MERCANCIAS'  [Transaction],
		ma.id MainAccountId,
		MA.Number MainAccountNumber,
		IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) DebitValue,
		IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) CreditValue,
		CASE 
			WHEN VirtualStore = 1 THEN 'VIRTUAL' 
			WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
			WHEN CustodyStore = 1 THEN 'CUSTODIA'
			WHEN TransitStore = 1 THEN 'TRANSITO'
			WHEN ControlStore = 1 THEN 'CONTROL'
			WHEN WareHouseType = 1 THEN 'REMANENTE'
			ELSE 'NINGUNO'
		END AS WareHouseTypeName,
		KOT.[ESTADO] Status
	FROM Inventory.Kardex K WITH (NOLOCK)
	INNER JOIN CTE_KARDEX_DEVOLUCION_PRESTAMOS AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
	INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
	INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
	INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
	INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
	INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
	WHERE K.EntityName ='LoanMerchandiseDevolution' 
	 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,k.EntityName,
	 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
	),

	CTE_KARDEX_ORDENES_TRASLADO AS (
		SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,
		CASE TFO.OrderType WHEN 1 THEN 'Traslado' WHEN 2 THEN 'Consumo' WHEN 3 THEN 'Traslado en Transito' END 'TIPO ORDEN',
		CASE TFO.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Entregado' WHEN 3 THEN 'Anulado' WHEN 4 THEN 'En Transito' END 'ESTADO'
		FROM Inventory .Kardex K WITH (NOLOCK)
		INNER JOIN Inventory .TransferOrder TFO WITH (NOLOCK) ON TFO.ID=K.EntityId AND TFO.CODE=K.EntityCode
		WHERE K.EntityName ='TransferOrder' AND TFO.Status<> 3 AND TFO.OrderType<>3 
		AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed

		UNION ALL

		SELECT K.EntityId,K.EntityCode, K.EntityName,max(CAST(k.CreationDate AS DATE)) DocumentDate,'Traslado en Transito' AS 'TIPO ORDEN',
		CASE TFO.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Entregado' WHEN 3 THEN 'Anulado' WHEN 4 THEN 'En Transito' END 'ESTADO'
		FROM Inventory .Kardex K WITH (NOLOCK)
		INNER JOIN Inventory .TransferOrder TFO WITH (NOLOCK) ON TFO.ID=K.EntityId AND TFO.CODE=K.EntityCode
		WHERE K.EntityName ='TransferOrder' AND TFO.Status<> 3 AND TFO.OrderType=3 and k.MovementType=1 AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
		group by K.EntityId,K.EntityCode, K.EntityName,TFO.Status

	),

	CTE_DETALLE_KARDEX_ORDENES_TRASLADO
	AS
	(
	 SELECT 
		year(KOT.DocumentDate) [Year],
		month(KOT.DocumentDate) [Month],
		k.EntityName,
		'ORDENES DE TRASLADO' [Transaction],
		ma.id MainAccountId,
		MA.Number MainAccountNumber,
		IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) DebitValue,
		IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) CreditValue,
		CASE 
			WHEN VirtualStore = 1 THEN 'VIRTUAL' 
			WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
			WHEN CustodyStore = 1 THEN 'CUSTODIA'
			WHEN TransitStore = 1 THEN 'TRANSITO'
			WHEN ControlStore = 1 THEN 'CONTROL'
			WHEN WareHouseType = 1 THEN 'REMANENTE'
		ELSE 'NINGUNO'
		END AS WareHouseTypeName,
		kot.ESTADO Status,
		KOT.[TIPO ORDEN] OrderType
	FROM Inventory.Kardex K WITH (NOLOCK)
	 INNER JOIN CTE_KARDEX_ORDENES_TRASLADO AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
	 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
	 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
	 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
	 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
	 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
	 WHERE K.EntityName ='TransferOrder' 
	 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,k.EntityName,
	 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.[TIPO ORDEN],KOT.[ESTADO],KOT.DocumentDate
	),

	CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO
	AS
	(
		SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,
		CASE TFO.OrderType WHEN 1 THEN 'Traslado' WHEN 2 THEN 'Consumo' WHEN 3 THEN 'Traslado en Transito' END 'TIPO ORDEN',
		 CASE TFO.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Entregado' WHEN 3 THEN 'Anulado' WHEN 4 THEN 'En Transito' END 'ESTADO'
		 FROM Inventory .Kardex K WITH (NOLOCK)
		 INNER JOIN Inventory .TransferOrderDevolution AS TOD WITH (NOLOCK) ON TOD.ID=K.EntityId AND TOD.CODE=EntityCode
		INNER JOIN Inventory .TransferOrder TFO WITH (NOLOCK) ON TFO.ID=TOD.TransferOrderId 
		 WHERE K.EntityName ='TransferOrderDevolution'AND TOD.Status<>3 AND TFO.OrderType<>2 
		 AND YEAR(K.DocumentDate) = @YearClosed 
			AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO_CONSUMO
	AS
	(
	SELECT DISTINCT 
		K.EntityId,
		K.EntityCode,
		K.EntityName,
		CAST(k.DocumentDate AS DATE) DocumentDate,
		CASE TFO.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Entregado' WHEN 3 THEN 'Anulado' WHEN 4 THEN 'En Transito' END 'ESTADO',
		CASE TFO.OrderType WHEN 1 THEN 'Traslado' WHEN 2 THEN 'Consumo' WHEN 3 THEN 'Traslado en Transito' END 'TIPO ORDEN',
		TFO.ID 'ID ORDEN TRASLADO',
		TFO.CODE 'CODIGO ORDEN',
		K.MovementType,
		K.AffectInventory
		FROM Inventory.Kardex K WITH (NOLOCK)
		INNER JOIN Inventory .TransferOrderDevolution AS TOD WITH (NOLOCK) ON TOD.ID=K.EntityId AND TOD.CODE=EntityCode
		INNER JOIN Inventory .TransferOrder TFO WITH (NOLOCK) ON TFO.ID=TOD.TransferOrderId 
		WHERE K.EntityName ='TransferOrderDevolution'AND TOD.Status<>3 AND TFO.OrderType=2 AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
	),

		
	CTE_DETALLE_KARDEX_DEVOLUCION_ORDENES_TRASLADO
	AS
	(
		SELECT 
			year(K.DocumentDate) [Year],
			month(K.DocumentDate) [Month],
			k.EntityName,
			'DEVOLUCION ORDENES DE TRASLADO' [Transaction],
			ma.Id MainAccountId,
			MA.Number MainAccountNumber,
			IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) DebitValue,
			IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) CreditValue,
			CASE 
				WHEN VirtualStore = 1 THEN 'VIRTUAL' 
				WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
				WHEN CustodyStore = 1 THEN 'CUSTODIA'
				WHEN TransitStore = 1 THEN 'TRANSITO'
				WHEN ControlStore = 1 THEN 'CONTROL'
				WHEN WareHouseType = 1 THEN 'REMANENTE'
				ELSE 'NINGUNO'
			END AS WareHouseTypeName,
			KOT.[ESTADO] Status,
			KOT.[TIPO ORDEN] OrderType
	FROM Inventory.Kardex K WITH (NOLOCK)
		INNER JOIN CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
		INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
		INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK)  ON WH.Id =K.WarehouseId
		INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
		INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
		INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
		WHERE K.EntityName ='TransferOrderDevolution' 
		GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,k.EntityName,
		AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.[TIPO ORDEN],KOT.[ESTADO],KOT.DocumentDate
	),

	CTE_DETALLE_KARDEX_DEVOLUCION_ORDENES_TRASLADO_CONSUMO
	AS
	(
		SELECT  
			year(KOT.DocumentDate) [Year],
			month(KOT.DocumentDate) [Month],
			kot.EntityName,
			'DEVOLUCION ORDENES DE TRASLADO' [Transaction],
			ma.Id MainAccountId,
			MA.Number MainAccountNumber,
			IIF(KOT.MovementType=1,cast(SUM(todd.Quantity * tood.Value) as numeric(18,2)),0) DebitValue, 
			IIF(Kot.MovementType=2,cast(SUM(todd.Quantity * tood.Value) as numeric(18,2)),0) CreditValue,
			CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
				WHEN CustodyStore = 1 
				THEN 'CUSTODIA' WHEN TransitStore = 1 
				THEN 'TRANSITO' WHEN ControlStore = 1 
				THEN 'CONTROL' WHEN WareHouseType = 1 
				THEN 'REMANENTE'
					ELSE 'NINGUNO' 
			END AS WareHouseTypeName,
			KOT.[ESTADO] Status,
			KOT.[TIPO ORDEN] OrderType
	FROM Inventory.TransferOrderDevolution tod WITH (NOLOCK)
	INNER JOIN CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO_CONSUMO AS KOT ON tod.ID=KOT.EntityId AND tod.CODE=KOT.EntityCode
    INNER JOIN Inventory.TransferOrderDevolutionDetail todd WITH (NOLOCK) on todd.TransferOrderDevolutionId = tod.Id
	INNER JOIN Inventory.TransferOrderDetailBatchSerial todbs WITH (NOLOCK) on todbs.Id = todd.TransferOrderDetailBatchSerialId
	INNER JOIN Inventory.TransferOrderDetail tood WITH (NOLOCK) on tood.Id = todbs.TransferOrderDetailId
	INNER JOIN Inventory.TransferOrder too WITH (NOLOCK) on too.Id = tood.TransferOrderId
	INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =tood.ProductId 
	INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK)  ON WH.Id =too.SourceWarehouseId
	INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
	INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
	INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
	WHERE KOT.EntityName ='TransferOrderDevolution' 
		GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),PG.CODE,PG.NAME,MA.Number,KOT.MovementType,WH.CODE,WH.NAME,VirtualStore,ma.Id,kot.EntityName,
		WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.AffectInventory,KOT.EntityCode,KOT.EntityId,KOT.[TIPO ORDEN],KOT.[ESTADO],KOT.DocumentDate
	),

	CTE_KARDEX_COMPROBANTES_ENTRADA
	AS
	(
		SELECT DISTINCT K.EntityId,
		K.EntityCode,
		CAST(k.DocumentDate AS DATE) DocumentDate,
		K.EntityName ,AP.Id ID_CXP,AP.Code [CODIGO CXP],
		'SI' 'VALOR',EV.ValueTax 'VALOR IVA','Confirmado' 'ESTADO'
		FROM Inventory .Kardex K WITH (NOLOCK)
		INNER JOIN Payments.AccountPayable AS AP WITH (NOLOCK) ON K.EntityId=AP.EntityId AND K.EntityCode=AP.EntityCode
		INNER JOIN Inventory.EntranceVoucher EV WITH (NOLOCK) ON EV.ID=K.EntityId AND EV.CODE=K.EntityCode
		INNER JOIN Inventory.EntranceVoucherDetail AS EVD WITH (NOLOCK) ON EV.ID=EVD.EntranceVoucherId
		WHERE K.EntityName ='EntranceVoucher' AND EV.Status<> 3 AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_DETALLE_KARDEX_COMPROBANTES_ENTRADA
	AS
	(
		SELECT 
			year(KOT.DocumentDate) [Year],
			month(KOT.DocumentDate) [Month],
			kot.EntityName,
			'COMPROBANTES DE ENTRADA' [Transaction],
			ma.Id MainAccountId,
			MA.Number MainAccountNumber,
			CAST(SUM(EVD.TotalValue) AS NUMERIC(18,2)) DebitValue,
			0 CreditValue,
			CASE 
			WHEN VirtualStore = 1 THEN 'VIRTUAL' 
			WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION' WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO'WHEN ControlStore = 1 THEN 'CONTROL' 
			WHEN WareHouseType = 1 THEN 'REMANENTE'ELSE 'NINGUNO' END AS WareHouseTypeName,
			KOT.[ESTADO] Status,
			CASE EVD.EntranceSource WHEN 1 THEN 'Ninguna' WHEN 2 THEN 'Orden de Compra' WHEN  3 THEN 'Contrato' WHEN 4 THEN 'Remisión de Entrada' 
			WHEN  5 THEN 'Remisión en Consignación' END OrderType
		FROM Inventory.EntranceVoucher EV WITH (NOLOCK)
		INNER JOIN CTE_KARDEX_COMPROBANTES_ENTRADA KOT ON KOT.EntityId=EV.Id AND KOT.EntityCode=EV.Code
		INNER JOIN Inventory.EntranceVoucherDetail AS EVD WITH (NOLOCK) ON EV.ID=EVD.EntranceVoucherId
		INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =EVD.ProductId 
		INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =EV.WarehouseId
		INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
		INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
		INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
		GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),PG.CODE,PG.NAME,MA.Number,WH.CODE,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,
		TransitStore,ControlStore,WareHouseType,EVD.EntranceSource,KOT.EntityCode,KOT.EntityId,MA.ID,KOT.[ESTADO],KOT.[CODIGO CXP],KOT.DocumentDate,kot.EntityName
	),

	CTE_DETALLE_KARDEX_RECLASIFICACION_REMISIONES
	AS
	(
		SELECT 
			year(KOT.DocumentDate) [Year],
			month(KOT.DocumentDate) [Month],
			kot.EntityName,
			'RECLASIFICACION REMISIONES' [Transaction],
			ma.Id MainAccountId,
			MA.Number MainAccountNumber,
			0 DebitValue,
			CAST(SUM((evd.Quantity * RED.UnitValue) + RED.IvaValue) AS NUMERIC(18,2)) CreditValue,
			CASE 
			WHEN VirtualStore = 1 THEN 'VIRTUAL' 
			WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION' WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO'WHEN ControlStore = 1 THEN 'CONTROL' 
			WHEN WareHouseType = 1 THEN 'REMANENTE'ELSE 'NINGUNO' END AS WareHouseTypeName,
			KOT.[ESTADO] Status,
			CASE EVD.EntranceSource WHEN 1 THEN 'Ninguna' WHEN 2 THEN 'Orden de Compra' WHEN  3 THEN 'Contrato' WHEN 4 THEN 'Remisión de Entrada' 
			WHEN  5 THEN 'Remisión en Consignación' END OrderType
		FROM Inventory.EntranceVoucher EV WITH (NOLOCK)
		INNER JOIN CTE_KARDEX_COMPROBANTES_ENTRADA KOT ON KOT.EntityId=EV.Id AND KOT.EntityCode=EV.Code
		INNER JOIN Inventory.EntranceVoucherDetail AS EVD WITH (NOLOCK) ON EV.ID=EVD.EntranceVoucherId
		INNER JOIN Inventory.RemissionEntranceDetailBatchSerial AS REDBS WITH (NOLOCK) ON REDBS.Id=EVD.RemissionEntranceDetailBatchSerialId
		INNER JOIN Inventory.RemissionEntranceDetail as RED WITH (NOLOCK) ON RED.Id=REDBS.RemissionEntranceDetailId
		INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =RED.ProductId 
		INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =EV.WarehouseId
		INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
		INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
		INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
		WHERE EVD.EntranceSource=4
		GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),PG.CODE,PG.NAME,MA.Number,WH.CODE,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,
		TransitStore,ControlStore,WareHouseType,EVD.EntranceSource,KOT.EntityCode,KOT.EntityId,MA.ID,KOT.[ESTADO],KOT.[CODIGO CXP],KOT.DocumentDate,kot.EntityName
	),

	CTE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA
	AS
	(
	SELECT DISTINCT K.EntityId,K.EntityCode,CAST(k.DocumentDate AS DATE) DocumentDate, K.EntityName,PN.ID ID_NOTA,PN.CODE 'CODIGO NOTA',EVD.ID ID_DEVO,EVD.CODE 'CODIGO DEV', EVD.ValueTax 'VALOR IVA',
	CASE EVD.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Confirmado' WHEN 3 THEN 'Anulado' END 'ESTADO'
		FROM Inventory .Kardex K WITH (NOLOCK)
		INNER JOIN Inventory.EntranceVoucherDevolution EVD WITH (NOLOCK) ON EVD.ID=K.EntityId AND EVD.CODE=EntityCode
		INNER JOIN Payments.PaymentNotes as PN WITH (NOLOCK) ON PN.Entitycode=EVD.Code and PN.EntityId= EVD.Id
		WHERE K.EntityName ='EntranceVoucherDevolution'AND EVD.Status<>3 AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_DETALLE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA
	AS
	(
		SELECT 
			year(KOT.DocumentDate) [Year],
			month(KOT.DocumentDate) [Month],
			kot.EntityName,
			'DEVOLUCION DE COMPRAS' [Transaction],
			MA.ID MainAccountId,
			MA.Number MainAccountNumber,
			IIF(k.MovementType=1,CAST(SUM(k.Quantity*k.Value) AS NUMERIC(18,2)),0) DebitValue,
			IIF(k.MovementType=2,CAST(SUM(k.Quantity*k.Value) AS NUMERIC(18,2)),0) CreditValue,
			CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
				WHEN CustodyStore = 1 THEN 'CUSTODIA'WHEN TransitStore = 1 THEN 'TRANSITO'WHEN ControlStore = 1 THEN 'CONTROL'WHEN WareHouseType = 1 THEN 'REMANENTE'
				ELSE 'NINGUNO'END AS WareHouseTypeName,
			KOT.[ESTADO] Status,
			'Devoluciones en Compras' OrderType
		FROM Inventory.Kardex K WITH (NOLOCK)
		INNER JOIN CTE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA AS KOT ON KOT.ID_DEVO=K.EntityId AND KOT.[CODIGO DEV]=K.EntityCode
		INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
		INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
		INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
		INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
		INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
		WHERE K.EntityName ='EntranceVoucherDevolution' 
		GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,kot.EntityName,
		AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.[CODIGO NOTA],KOT.[ESTADO],KOT.DocumentDate
	),

	CTE_KARDEX_REMISIONES_ENTRADA
	AS
	(
	SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
		FROM Inventory .Kardex K WITH (NOLOCK)
		WHERE K.EntityName ='RemissionEntrance' AND YEAR(K.DocumentDate) = @YearClosed 
				AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_DETALLE_KARDEX_REMISIONES_ENTRADA
	AS
	(
		SELECT 
			year(K.DocumentDate) [Year],
			month(K.DocumentDate) [Month],
			KOT.EntityName,
			'REMISION DE ENTRADA' [Transaction],
			MA.ID MainAccountId,
			MA.Number MainAccountNumber,
			IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) DebitValue,
			IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) CreditValue,
			CASE 
				WHEN VirtualStore = 1 THEN 'VIRTUAL' 
				WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
				WHEN CustodyStore = 1 THEN 'CUSTODIA'
				WHEN TransitStore = 1 THEN 'TRANSITO'
				WHEN ControlStore = 1 THEN 'CONTROL'
				WHEN WareHouseType = 1 THEN 'REMANENTE'
				ELSE 'NINGUNO'
			END AS WareHouseTypeName,
			KOT.[ESTADO] Status
		FROM Inventory.Kardex K WITH (NOLOCK)
		INNER JOIN CTE_KARDEX_REMISIONES_ENTRADA AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
		INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
		INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
		INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
		INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
		INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
		WHERE K.EntityName ='RemissionEntrance' 
		GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,kot.EntityName,
		AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
	),

	CTE_KARDEX_DEVOLUCION_REMISION_ENTRADA
	AS
	(
	SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
		FROM Inventory .Kardex K WITH (NOLOCK)
		INNER JOIN Inventory.RemissionDevolution AS RV WITH (NOLOCK) ON RV.Id=k.EntityId and RV.Code=K.EntityCode
		WHERE K.EntityName ='RemissionDevolution' AND RV.DevolutionType=1 AND YEAR(K.DocumentDate) = @YearClosed 
				AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_DETALLE_KARDEX_DEVOLUCION_REMISION_ENTRADA
	AS
	(
		SELECT 
			year(K.DocumentDate) [Year],
			month(K.DocumentDate) [Month],
			kot.EntityName,
			'DEVOLUCION REMISION DE ENTRADA' [Transaction],
			ma.id MainAccountId,
			MA.Number MainAccountNumber,
			IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) DebitValue,
			IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) CreditValue,
			CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
				WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO' WHEN ControlStore = 1 THEN 'CONTROL' WHEN WareHouseType = 1 THEN 'REMANENTE'
				ELSE 'NINGUNO'END AS WareHouseTypeName,
		KOT.[ESTADO] Status
		FROM Inventory.Kardex K WITH (NOLOCK)
		INNER JOIN CTE_KARDEX_DEVOLUCION_REMISION_ENTRADA AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
		INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
		INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
		INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
		INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
		INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
		WHERE K.EntityName ='RemissionDevolution' 
		GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,kot.EntityName,
		AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
	),

	CTE_KARDEX_REMISIONES_CONSIGNACION
	AS
	(
	SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
		FROM Inventory .Kardex K WITH (NOLOCK)
		WHERE K.EntityName ='ConsignmentInventoryRemission' AND YEAR(K.DocumentDate) = @YearClosed 
				AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_DETALLE_KARDEX_REMISIONES_CONSIGNACION
	AS
	(
		SELECT  
			year(K.DocumentDate) [Year],
			month(K.DocumentDate) [Month],
			kot.EntityName,
			'REMISION DE INVENTARIO CONSIGNACION' [Transaction],
			ma.id MainAccountId,
			MA.Number MainAccountNumber,
			IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) DebitValue,
			IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.value) AS NUMERIC(18,2)),0) CreditValue,
			CASE 
				WHEN VirtualStore = 1 THEN 'VIRTUAL' 
				WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
				WHEN CustodyStore = 1 THEN 'CUSTODIA'
				WHEN TransitStore = 1 THEN 'TRANSITO'
				WHEN ControlStore = 1 THEN 'CONTROL'
				WHEN WareHouseType = 1 THEN 'REMANENTE'
				ELSE 'NINGUNO'
			END AS WareHouseTypeName,
			KOT.[ESTADO] Status
		FROM Inventory.Kardex K WITH (NOLOCK)
		INNER JOIN CTE_KARDEX_REMISIONES_CONSIGNACION AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
		INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
		INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
		INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
		INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
		INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
		WHERE K.EntityName ='ConsignmentInventoryRemission' 
		GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,kot.EntityName,
		AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
	),

	CTE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION
	AS
	(
		SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
		FROM Inventory .Kardex K WITH (NOLOCK)
		INNER JOIN Inventory.RemissionDevolution AS RV WITH (NOLOCK) ON RV.Id=k.EntityId and RV.Code=K.EntityCode
		WHERE K.EntityName ='RemissionDevolution' AND RV.DevolutionType=3
			AND YEAR(K.DocumentDate) = @YearClosed 
			AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_DETALLE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION
	AS
	(
		SELECT 
			year(K.DocumentDate) [Year],
			month(K.DocumentDate) [Month],
			kot.EntityName,
			'DEVOLUCION REMISION DE ENTRADA' [Transaction],
			ma.id MainAccountId,
			MA.Number MainAccountNumber,
			IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) DebitValue,
			IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) CreditValue,
			CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
			WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO' WHEN ControlStore = 1 THEN 'CONTROL' WHEN WareHouseType = 1 THEN 'REMANENTE'
			ELSE 'NINGUNO'END AS WareHouseTypeName,
			KOT.[ESTADO] Status
		FROM Inventory.Kardex K WITH (NOLOCK)
		INNER JOIN CTE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
		INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
		INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
		INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
		INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
		INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
		WHERE K.EntityName ='RemissionDevolution' 
		GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,kot.EntityName,
		AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO]
	),

	CTE_KARDEX_FACTURA_PRODUCTOS
	AS
	(
		SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
		FROM Inventory .Kardex K WITH (NOLOCK)
		WHERE K.EntityName ='BasicBilling'
			AND YEAR(K.DocumentDate) = @YearClosed 
			AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_DETALLE_KARDEX_FACTURA_PRODUCTOS
	AS
	(
		 SELECT 
			year(K.DocumentDate) [Year],
			month(K.DocumentDate) [Month],
			kot.EntityName,
			'FACTURA BASICA PRODUCTOS' [Transaction],
			ma.id MainAccountId,
			MA.Number MainAccountNumber,
			IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) DebitValue,
			IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) CreditValue,
			CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
			WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO' WHEN ControlStore = 1 THEN 'CONTROL' WHEN WareHouseType = 1 THEN 'REMANENTE'
			ELSE 'NINGUNO'END AS WareHouseTypeName,
			KOT.[ESTADO] Status
		 FROM Inventory.Kardex K WITH (NOLOCK)
		 INNER JOIN CTE_KARDEX_FACTURA_PRODUCTOS AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
		 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
		 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
		 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
		 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) on apc.Id = PG.InventoryAccountPayableConceptId 
		 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
		 WHERE K.EntityName ='BasicBilling' 
		 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
		 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO],kot.EntityName
	),

	CTE_KARDEX_DEVOLUCION_FACTURA_PRODUCTOS
	AS
	(
	 SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
	 FROM Inventory .Kardex K WITH (NOLOCK)
	 WHERE K.EntityName ='BasicBillingDevolution' 
		AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_DETALLE_KARDEX_DEVOLUCION_FACTURA_PRODUCTOS
	AS
	(
	 SELECT 
		year(K.DocumentDate) [Year],
		month(K.DocumentDate) [Month],
		kot.EntityName,
		'DEVOLUCION FACTURA BASICA PRODUCTOS' [Transaction],
		ma.id MainAccountId,
		MA.Number MainAccountNumber,
		IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) DebitValue,
		IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) CreditValue,
		CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
		WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO' WHEN ControlStore = 1 THEN 'CONTROL' WHEN WareHouseType = 1 THEN 'REMANENTE'
		ELSE 'NINGUNO'END AS WareHouseTypeName,
		KOT.[ESTADO] Status
	 FROM Inventory.Kardex K WITH (NOLOCK)
	 INNER JOIN CTE_KARDEX_DEVOLUCION_FACTURA_PRODUCTOS AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
	 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
	 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
	 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
	 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
	 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
	 WHERE K.EntityName ='BasicBillingDevolution' 
	 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
	 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO],kot.EntityName
	),

	CTE_KARDEX_REMISIONES_SALIDA
	AS
	(
	SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
	 FROM Inventory .Kardex K WITH (NOLOCK)
	 WHERE K.EntityName ='RemissionOutput' 
		AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_DETALLE_KARDEX_REMISIONES_SALIDA
	AS
	(
	 SELECT 
		year(K.DocumentDate) [Year],
		month(K.DocumentDate) [Month],
		kot.EntityName,
		'REMISION DE SALIDA' [Transaction],
		ma.id MainAccountId,
		MA.Number MainAccountNumber,
		IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) DebitValue,
		IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) CreditValue,
		CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
		WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO' WHEN ControlStore = 1 THEN 'CONTROL' WHEN WareHouseType = 1 THEN 'REMANENTE'
		ELSE 'NINGUNO'END AS WareHouseTypeName,
		KOT.[ESTADO] Status
	 FROM Inventory.Kardex K WITH (NOLOCK)
	 INNER JOIN CTE_KARDEX_REMISIONES_SALIDA AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
	 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
	 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
	 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
	 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) on apc.Id = PG.InventoryAccountPayableConceptId 
	 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
	 WHERE K.EntityName ='RemissionOutput' 
	 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
	 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO],kot.EntityName
	),

	CTE_KARDEX_DEVOLUCION_REMISION_SALIDA
	AS
	(
	SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
	 FROM Inventory .Kardex K WITH (NOLOCK)
	 INNER JOIN Inventory.RemissionDevolution AS RV WITH (NOLOCK) ON RV.Id=k.EntityId and RV.Code=K.EntityCode
	 WHERE K.EntityName ='RemissionDevolution' AND RV.DevolutionType=2 
		AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_DETALLE_KARDEX_DEVOLUCION_REMISION_SALIDA
	AS
	(
	 SELECT
		year(K.DocumentDate) [Year],
		month(K.DocumentDate) [Month],
		kot.EntityName,
		'DEVOLUCION REMISION DE SALIDA' [Transaction],
		ma.id MainAccountId,
		MA.Number MainAccountNumber,
		IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) DebitValue,
		IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) CreditValue,
		CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
		WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO' WHEN ControlStore = 1 THEN 'CONTROL' WHEN WareHouseType = 1 THEN 'REMANENTE'
		ELSE 'NINGUNO'END AS WareHouseTypeName,
		KOT.[ESTADO] Status
	 FROM Inventory.Kardex K WITH (NOLOCK)
	 INNER JOIN CTE_KARDEX_DEVOLUCION_REMISION_SALIDA AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
	 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
	 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
	 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
	 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
	 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
	 WHERE K.EntityName ='RemissionDevolution' 
	 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
	 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO],kot.EntityName
	),

	CTE_KARDEX_DOCUMENTO_FACTURA_PRODUCTOS
	AS
	(
		SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
		FROM Inventory .Kardex K WITH (NOLOCK)
		WHERE K.EntityName ='DocumentInvoiceProductSales' 
		AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_DETALLE_KARDEX_DOCUMENTO_FACTURA_PRODUCTOS
	AS
	(
	 SELECT 
		year(K.DocumentDate) [Year],
		month(K.DocumentDate) [Month],
		kot.EntityName,
		'FACTURA BASICA PRODUCTOS' [Transaction],
		ma.id MainAccountId,
		MA.Number MainAccountNumber,
		IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) DebitValue,
		IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) CreditValue,
		CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
		WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO' WHEN ControlStore = 1 THEN 'CONTROL' WHEN WareHouseType = 1 THEN 'REMANENTE'
		ELSE 'NINGUNO'END AS WareHouseTypeName,
		KOT.[ESTADO] Status
	 FROM Inventory.Kardex K WITH (NOLOCK)
	 INNER JOIN CTE_KARDEX_DOCUMENTO_FACTURA_PRODUCTOS AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
	 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
	 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
	 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
	 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) on apc.Id = PG.InventoryAccountPayableConceptId 
	 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
	 WHERE K.EntityName ='DocumentInvoiceProductSales' 
	 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
	 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO],kot.EntityName
	),

	CTE_KARDEX_DEVOLUCION_DOCUMENTO_FACTURA_PRODUCTOS
	AS
	(
	 SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
	 FROM Inventory .Kardex K WITH (NOLOCK)
	 WHERE K.EntityName ='DocumentInvoiceProductSalesDevolution' 
		AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_DETALLE_KARDEX_DEVOLUCION_DOCUMENTO_FACTURA_PRODUCTOS
	AS
	(
	 SELECT 
		year(K.DocumentDate) [Year],
		month(K.DocumentDate) [Month],
		kot.EntityName,
		'DEVOLUCION FACTURA BASICA PRODUCTOS' [Transaction],
		ma.id MainAccountId,
		MA.Number MainAccountNumber,
		IIF(K.MovementType=1,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) DebitValue,
		IIF(K.MovementType=2,CAST(SUM(K.Quantity*k.Value) AS NUMERIC(18,2)),0) CreditValue,
		CASE WHEN VirtualStore = 1 THEN 'VIRTUAL' WHEN WarehouseConsignment = 1 THEN 'CONSIGNACION'
		WHEN CustodyStore = 1 THEN 'CUSTODIA' WHEN TransitStore = 1 THEN 'TRANSITO' WHEN ControlStore = 1 THEN 'CONTROL' WHEN WareHouseType = 1 THEN 'REMANENTE'
		ELSE 'NINGUNO'END AS WareHouseTypeName,
		KOT.[ESTADO] Status
	 FROM Inventory.Kardex K WITH (NOLOCK)
	 INNER JOIN CTE_KARDEX_DEVOLUCION_DOCUMENTO_FACTURA_PRODUCTOS AS KOT ON KOT.EntityId=K.EntityId AND KOT.EntityCode=K.EntityCode
	 INNER JOIN Inventory.InventoryProduct AS PRO WITH (NOLOCK) ON PRO.Id =K.ProductId 
	 INNER JOIN Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =K.WarehouseId
	 INNER JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.Id=PRO.ProductGroupId
	 INNER JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON apc.Id = PG.InventoryAccountPayableConceptId 
	 INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=apc.IdAccount
	 WHERE K.EntityName ='DocumentInvoiceProductSalesDevolution' 
	 GROUP BY year(K.DocumentDate),month(K.DocumentDate),DAY(K.DocumentDate),K.EntityCode,K.MovementType,K.EntityId,PG.CODE,PG.NAME,MA.Number,K.MovementType,
	 AffectInventory,MA.ID,WH.CODE ,WH.NAME,VirtualStore,WarehouseConsignment,CustodyStore,TransitStore,ControlStore,WareHouseType,KOT.DocumentDate,KOT.[ESTADO],kot.EntityName
	),
		
CTE_DATOS_MOSTRAR
AS
(
------------------------------******************DISPENSACIONES Y DEVOLUCION DE DISPENSACIONES ***********************------------------------------

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Dispensación Farmacéutica' OrderType
FROM CTE_DETALLE_KARDEX_DISPENSACIONES

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Devolución Dispensación' OrderType
FROM CTE_DETALLE_KARDEX_DEVOLUCION_DISPENSACIONES

------------------------------**********************************************************************************************************------------------------------
UNION ALL
------------------------------************************ AJUSTES DE INVENTARIOS **********************************************************------------------------------
SELECT
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Ajuste' OrderType
FROM CTE_DETALLE_KARDEX_AJUSTES

------------------------------**********************************************************************************************************------------------------------
UNION ALL
------------------------------*********************************** PRESTAMOS *********************************************------------------------------
SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Prestamos' OrderType
FROM CTE_DETALLE_KARDEX_PRESTAMOS

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Devolución de Prestamos' OrderType
FROM CTE_DETALLE_KARDEX_DEVOLUCION_PRESTAMOS

------------------------------**********************************************************************************************************------------------------------
UNION ALL
------------------------------************************** ORDENES DE TRASALDOS *****************************************************------------------------------
SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	OrderType
FROM CTE_DETALLE_KARDEX_ORDENES_TRASLADO

UNION ALL
SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status, 
	OrderType
FROM CTE_DETALLE_KARDEX_DEVOLUCION_ORDENES_TRASLADO

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	OrderType
FROM CTE_DETALLE_KARDEX_DEVOLUCION_ORDENES_TRASLADO_CONSUMO

------------------------------************************************************** *****************************************************------------------------------
UNION ALL
------------------------------************************** COMPROBANTES DE ENTRADA *****************************************************------------------------------
SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	OrderType
FROM CTE_DETALLE_KARDEX_COMPROBANTES_ENTRADA

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	OrderType
FROM CTE_DETALLE_KARDEX_RECLASIFICACION_REMISIONES

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	OrderType
FROM CTE_DETALLE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA

UNION ALL

------------------------------**************************** REMISION DE ENTRADA ******************************************------------------------------
SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Remisión de Entrada' OrderType
FROM CTE_DETALLE_KARDEX_REMISIONES_ENTRADA
UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Devolución Remisión de Entrada' OrderType
FROM CTE_DETALLE_KARDEX_DEVOLUCION_REMISION_ENTRADA

------------------------------***************************************************************************************************------------------------------
UNION ALL
------------------------------**************************** REMISION DE INVENTARIOS CONSIGNACION ***********************************------------------------------
SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Remisión Inventario Consignación' OrderType
FROM CTE_DETALLE_KARDEX_REMISIONES_CONSIGNACION
UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Devolución Remisión Inventario Consignación' OrderType
FROM CTE_DETALLE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Factura Productos' OrderType
FROM CTE_DETALLE_KARDEX_FACTURA_PRODUCTOS

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Devolución Factura Productos' OrderType
FROM CTE_DETALLE_KARDEX_DEVOLUCION_FACTURA_PRODUCTOS

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Remisión de Salida' OrderType
FROM CTE_DETALLE_KARDEX_REMISIONES_SALIDA

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Devolución Remisión de Salida' OrderType
FROM CTE_DETALLE_KARDEX_DEVOLUCION_REMISION_SALIDA

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Factura Documento Productos' OrderType
FROM CTE_DETALLE_KARDEX_DOCUMENTO_FACTURA_PRODUCTOS

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Devolución Factura Documento Productos' OrderType
FROM CTE_DETALLE_KARDEX_DEVOLUCION_DOCUMENTO_FACTURA_PRODUCTOS

),

SourceData AS (

    SELECT 
		Year,
		Month,
		EntityName,
		[Transaction],
		MainAccountId,
		MainAccountNumber,
		SUM(DebitValue) TotalDebit,
		SUM(CreditValue) TotalCredit,
		'Inventory' Module
	FROM CTE_DATOS_MOSTRAR
    WHERE WareHouseTypeName='NINGUNO'
	AND Status<>'En Transito' AND OrderType<>'Remisión en Consignación' 
    AND OrderType<>'Remisión Inventario Consignación' 
	AND OrderType<>'Devolución Remisión Inventario Consignación'
	GROUP BY  Year,
				Month,
				EntityName,
				[Transaction],
				MainAccountId,
				MainAccountNumber
    )

SELECT * from SourceData

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de conciliación entre el inventario y la contabilidad para un mes y año específicos. Recorre el kardex de movimientos (entradas y salidas) agrupando las transacciones del período cerrado por tipo: dispensaciones farmacéuticas, devoluciones de dispensaciones, ajustes de inventario y otros movimientos; luego las cruza con el catálogo de productos, las bodegas, los grupos de productos y las cuentas contables principales para calcular los valores débito y crédito por cuenta mayor. Su propósito es detectar diferencias entre los saldos registrados en el módulo de inventario y los saldos contables, facilitando el cierre contable mensual y la auditoría de las cuentas de inventario (medicamentos, insumos y dispositivos médicos) por tipo de bodega (virtual, consignación, custodia, tránsito, control o remanente).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ConciliationInventory';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ConciliationInventory';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Kardex de inventario; Conciliación inventario vs contabilidad; Cuenta contable principal (PUC); Dispensación farmacéutica; Devolución de dispensación; Ajuste de inventario; Préstamo de mercancías; Devolución de préstamos; Orden de traslado (traslado, consumo, traslado en tránsito); Devolución de orden de traslado; Comprobante de entrada; Devolución de compras; Reclasificación de remisiones; Remisión de entrada / salida / consignación; Devolución de remisiones; Factura básica de productos; Documento factura de productos; Tipos de bodega: virtual, consignación, custodia, tránsito, control, remanente; Cierre contable mensual; Concepto de cuenta por pagar (AccountPayableConcepts); Movimiento débito / crédito', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConciliationInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si K.EntityName clasifica el movimiento del kardex (PharmaceuticalDispensing, PharmaceuticalDispensingDevolution, InventoryAdjustment, LoanMerchandise, LoanMerchandiseDevolution, TransferOrder, TransferOrderDevolution, EntranceVoucher, EntranceVoucherDevolution, RemissionEntrance, RemissionDevolution, ConsignmentInventoryRemission, BasicBilling, BasicBillingDevolution, RemissionOutput, DocumentInvoiceProductSales, DocumentInvoiceProductSalesDevolution) → Cada tipo se procesa en un CTE específico que le asigna una etiqueta de Transaction y un OrderType propio para la conciliación; si K.MovementType = 1 → El valor (Quantity*Value) se acumula como DebitValue y CreditValue queda en 0 else Si MovementType = 2 se acumula como CreditValue y DebitValue queda en 0 (en devolución de órdenes de traslado se invierte: MovementType=2 => Debit, MovementType=1 => Credit); si Bodega: VirtualStore=1 / WarehouseConsignment=1 / CustodyStore=1 / TransitStore=1 / ControlStore=1 / WareHouseType=1 → Se etiqueta WareHouseTypeName como VIRTUAL/CONSIGNACION/CUSTODIA/TRANSITO/CONTROL/REMANENTE respectivamente else Si no coincide ninguna, WareHouseTypeName=''NINGUNO''; si Para TransferOrder: TFO.OrderType <> 3 y TFO.Status <> 3 → Se incluye como traslado/consumo normal con su DocumentDate else Si OrderType=3 (Traslado en Tránsito) y MovementType=1, se toma por separado usando max(CreationDate) como DocumentDate; si Para TransferOrderDevolution: TFO.OrderType <> 2 (no consumo) y TOD.Status <> 3 → Se procesa por CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO con valores tomados del Kardex else Si TFO.OrderType = 2 (Consumo), se procesa por la rama de consumo, calculando débito/crédito desde TransferOrderDevolutionDetail.Quantity * TransferOrderDetail.Value; si RemissionDevolution.DevolutionType → DevolutionType=1 -> devolución remisión de entrada; DevolutionType=2 -> devolución remisión de salida; DevolutionType=3 -> devolución remisión de consignación; si EntranceVoucherDetail.EntranceSource = 4 (Remisión de Entrada) → Se genera un registro adicional ''RECLASIFICACION REMISIONES'' con CreditValue = SUM((evd.Quantity*RED.UnitValue)+RED.IvaValue), tomando los valores desde RemissionEntranceDetail; si EV.Status <> 3 (no anulado) para comprobantes de entrada y EVD.Status <> 3 para devolución de comprobantes → Se incluyen en la conciliación else Los comprobantes/devoluciones anulados se excluyen; si Filtro final en SourceData: WareHouseTypeName=''NINGUNO'' AND Status<>''En Transito'' AND OrderType NOT IN (''Remisión en Consignación'',''Remisión Inventario Consignación'',''Devolución Remisión Inventario Consignación'') → Solo se devuelven movimientos de bodegas regulares y se excluyen tránsito y consignación para la conciliación contable final', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConciliationInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Kardex; Inventory.InventoryProduct; Inventory.Warehouse; Inventory.ProductGroup; Payments.AccountPayableConcepts; GeneralLedger.MainAccounts; Inventory.TransferOrder; Inventory.TransferOrderDevolution; Inventory.TransferOrderDevolutionDetail; Inventory.TransferOrderDetailBatchSerial; Inventory.TransferOrderDetail; Payments.AccountPayable; Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail; Inventory.RemissionEntranceDetailBatchSerial; Inventory.RemissionEntranceDetail; Inventory.EntranceVoucherDevolution; Payments.PaymentNotes; Inventory.RemissionDevolution', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConciliationInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConciliationInventory';
-- GO
