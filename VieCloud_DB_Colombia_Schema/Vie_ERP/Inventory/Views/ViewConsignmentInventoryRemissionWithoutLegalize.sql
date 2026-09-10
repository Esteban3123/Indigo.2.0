

CREATE VIEW [Inventory].[ViewConsignmentInventoryRemissionWithoutLegalize]
AS
with cte_ccld as (	select ccl.SupplierId,ccld.ProductId,ccld.CostNew,ccl.OperatingUnitId
					from Inventory.ConsignmentCostList as ccl with(nolock)
					inner join Inventory.ConsignmentCostListDetail as ccld with(nolock) on ccl.Id = ccld.ConsignmentCostListId
					inner join Common.Supplier as s on s.Id = ccl.SupplierId
					--Solo los proveedores que tengan marcado costo de listas y la lista este activa
					where  s.ConsignmentInventoryCosting = 1 and ccl.Status = 1 
					group by ccl.SupplierId,ccld.ProductId,ccld.CostNew,ccl.OperatingUnitId) 

	select
	cirdbs.Id,
	cirdbs.BatchSerialId,
	cirdbs.Quantity,
	cirdbs.UsedQuantity,
	cirdbs.LegalizedQuantity,
	cirdbs.UsedQuantity - cirdbs.LegalizedQuantity - ISNULL(ev.Quantity, 0) AS OutstandingQuantity,
	------------------------------
	cird.ProductId,
	coalesce(ccld.CostNew,cird.UnitValue,0) UnitValue	,	
	cird.IvaValue,
	cird.LastValue,
	------------------------------
	cir.Code,
	cir.RemissionDate,
	cir.SupplierId,
	cir.SupplierDistributionLineId,
	cir.WarehouseId,
	------------------------------
	CONCAT(pg.Code, ' - ', pg.Name) GroupCodeName,
	CONCAT(ip.Code, ' - ', ip.Name) CodeNameProduct,
	bs.BatchCode,
	c.Id AS CurrencyId,
	c.Abbreviation AS CurrencyAbbreviation
FROM Inventory.ConsignmentInventoryRemission as cir
JOIN Inventory.ConsignmentInventoryRemissionDetail cird ON cir.Id = cird.ConsignmentInventoryRemissionId
JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
------------------------------------------------------------------------------------------------------------------------------
JOIN Inventory.InventoryProduct ip ON cird.ProductId = ip.Id
JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
JOIN Common.Currency c WITH(NOLOCK) ON c.id = cir.CurrencyId
LEFT JOIN Inventory.BatchSerial bs ON cirdbs.BatchSerialId = bs.Id
------------------------------------------------------------------------------------------------------------------------------
LEFT JOIN 
(
	SELECT
		evd.ConsignmentInventoryRemissionDetailBatchSerialId,
		SUM(evd.Quantity) Quantity
	FROM Inventory.EntranceVoucher ev
	JOIN Inventory.EntranceVoucherDetail evd ON ev.Id = evd.EntranceVoucherId
	WHERE ev.Status IN (1)
		AND evd.ConsignmentInventoryRemissionDetailBatchSerialId IS NOT NULL
	GROUP BY evd.ConsignmentInventoryRemissionDetailBatchSerialId
) ev ON ev.ConsignmentInventoryRemissionDetailBatchSerialId = cirdbs.Id
LEFT join cte_ccld ccld on ccld.ProductId = cird.ProductId and ccld.SupplierId= cir.SupplierId  AND ccld.OperatingUnitId = cir.OperatingUnitId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de remisiones de inventario en consignación pendientes de legalización. Muestra, por cada lote o serial de producto consignado, las cantidades remisionadas, usadas, legalizadas y pendientes de legalizar (calculando la diferencia entre lo usado y lo ya legalizado o ingresado mediante comprobante de entrada). Integra la remisión con su detalle de productos y lotes, el catálogo de productos, el grupo de productos, el proveedor, la moneda y el costo unitario vigente de la lista de consignación activa del proveedor para la unidad operativa correspondiente; si no existe costo pactado en la lista, toma el valor unitario registrado en la remisión. Es útil para controlar el saldo de mercancía en consignación que aún no ha sido legalizada ante el proveedor, para conciliación de inventario, auditoría de consignaciones y procesos de compras o facturación pendiente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewConsignmentInventoryRemissionWithoutLegalize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewConsignmentInventoryRemissionWithoutLegalize';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los saldos de mercancía en consignación recibida por remisión que aún no ha sido legalizada ante el proveedor, calculando la cantidad pendiente y el costo unitario aplicable según lista de costos pactada o valor de la remisión.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewConsignmentInventoryRemissionWithoutLegalize';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir remisiones de consignación con detalle y lotes/seriales asociados; Para aplicar costo pactado, el proveedor debe tener ConsignmentInventoryCosting=1 y la lista de costos en Status=1; Solo se descuenta lo ingresado mediante EntranceVoucher con Status=1 y referencia al detalle de lote/serial de la remisión', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewConsignmentInventoryRemissionWithoutLegalize';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran listas de costos de consignación activas (Status=1) y proveedores con costeo de consignación habilitado; Las entradas (EntranceVoucher) solo afectan el pendiente cuando están en Status=1; El costo unitario nunca es NULL: cae a UnitValue de la remisión o a 0; El saldo pendiente se calcula a nivel de lote/serial de cada línea de remisión', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewConsignmentInventoryRemissionWithoutLegalize';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Inventario en consignación; Remisión de consignación; Legalización ante proveedor; Lista de costos de consignación; Lote/serial; Comprobante de entrada; Proveedor; Unidad operativa; Moneda; Grupo de productos', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewConsignmentInventoryRemissionWithoutLegalize';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve por cada lote/serial de remisión la cantidad pendiente como UsedQuantity - LegalizedQuantity - ISNULL(SUM(EntranceVoucherDetail.Quantity),0); [RETURN_RESULT] N/A: El UnitValue retornado es COALESCE(costo de lista vigente del proveedor para el producto y unidad operativa, UnitValue de la remisión, 0)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewConsignmentInventoryRemissionWithoutLegalize';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Supplier.ConsignmentInventoryCosting=1 AND ConsignmentCostList.Status=1 con coincidencia por ProductId, SupplierId y OperatingUnitId → Usa CostNew de la lista de costos de consignación como UnitValue else Usa UnitValue del detalle de la remisión, o 0 si es nulo; si EntranceVoucher.Status = 1 y EntranceVoucherDetail.ConsignmentInventoryRemissionDetailBatchSerialId IS NOT NULL → Suma esas cantidades y las descuenta del saldo pendiente (OutstandingQuantity) else No descuenta cantidades por entradas no aprobadas o no vinculadas al lote/serial', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewConsignmentInventoryRemissionWithoutLegalize';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ConsignmentCostList; Inventory.ConsignmentCostListDetail; Common.Supplier; Inventory.ConsignmentInventoryRemission; Inventory.ConsignmentInventoryRemissionDetail; Inventory.ConsignmentInventoryRemissionDetailBatchSerial; Inventory.InventoryProduct; Inventory.ProductGroup; Common.Currency; Inventory.BatchSerial; Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewConsignmentInventoryRemissionWithoutLegalize';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewConsignmentInventoryRemissionWithoutLegalize';
GO
