

CREATE VIEW [Inventory].[ViewReportKardex]
AS
SELECT	k.Id as KardexId, 
		k.WarehouseId, w.Code as WarehouseCode, w.Name as WarehouseName, w.Code + ' - ' + w.Name as WarehouseDescription, 
		k.ProductId, p.Code as ProductCode, p.Name as ProductName, p.Code + ' - ' + p.Name as ProductDescription, 
		p.ProductControl, p.CodeCUM, p.HealthRegistration, 
		k.BatchSerialId, bs.BatchCode as BatchCode, bs.ExpirationDate, 
		k.DocumentDate, k.CreationDate, k.MovementType, k.Quantity, 
		k.Value, k.PreviousCost, k.AverageCost, k.PreviousAverageCost, 
		k.PreviousAmountProduct, k.PreviousAmountWarehouse, k.PreviousAmountBatch,
		k.EntityId, k.EntityCode, case k.EntityName when 'RemissionEntrance' then case RED.SourceCode when null then 'Traslado Interno' else 'RemissionEntrance' end else k.EntityName end as EntityName, k.ImportedEntityId,k.ImportedEntityCode,k.ImportedEntityName,
		k.AffectInventory, 

		case k.EntityName
			when 'PharmaceuticalDispensing' then (pa.IPCODPACI + ' - ' + pa.IPNOMCOMP)
			when 'PharmaceuticalDispensingDevolution' then (pddpa.IPCODPACI + ' - ' + pddpa.IPNOMCOMP)
			when 'TransferOrder' then iif(tpto.Id is null, (sp.Identification + ' - ' + sp.Fullname), (tpto.Nit + ' - ' + tpto.Name))
			when 'EntranceVoucher' then (spev.Code + ' - ' + spev.Name)
			when 'RemissionEntrance' then (RED.SupplierCode + ' - ' + RED.SupplierName)
			WHEN 'BasicBilling' THEN (bb.Nit + ' - ' + bb.Name)
			WHEN 'BasicBillingDevolution' THEN (bb.Nit + ' - ' + bb.Name)
			else ''
		end as NamePatient,

		IIF(k.MovementType = 1, k.Quantity, 0) as QuantityEntrance, IIF(k.MovementType = 2, k.Quantity, 0) as QuantityExit, 
		(k.PreviousAverageCost * k.PreviousAmountProduct) as PreviousCostTotalCalculated,
		(k.AverageCost * (IIF(k.AffectInventory = 1, IIF(k.MovementType = 1, k.PreviousAmountProduct + k.Quantity, k.PreviousAmountProduct - k.Quantity), k.PreviousAmountProduct))) as CostTotalCalculated, 
		(IIF(k.AffectInventory = 1, IIF(k.MovementType = 1, k.PreviousAmountProduct + k.Quantity, k.PreviousAmountProduct - k.Quantity), k.PreviousAmountProduct)) as PreviousAmountProductCalculated, 
		(IIF(k.AffectInventory = 1, IIF(k.MovementType = 1, k.PreviousAmountWarehouse + k.Quantity, k.PreviousAmountWarehouse - k.Quantity), k.PreviousAmountWarehouse)) as PreviousAmountWarehouseCalculated, 
		(IIF(k.AffectInventory = 1, IIF(k.MovementType = 1, k.PreviousAmountBatch + k.Quantity, k.PreviousAmountBatch - k.Quantity), k.PreviousAmountBatch)) as PreviousAmountBatchCalculated, 
		case 
			when k.EntityName = 'ConsignmentInventoryRemission' then 'Remision de Inventario en Consignación' 
			when k.EntityName = 'RemissionEntrance' then 'Remision de Entrada'
			when k.EntityName = 'RemissionOutput' then 'Remision de Salida'
			when k.EntityName = 'RemissionDevolution' then 'Devolucion de Remision'
			when k.EntityName = 'EntranceVoucher' then 'Comprobante de Entrada'
			when k.EntityName = 'PharmaceuticalDispensing' then CONCAT('Dispensación Farmacéutica',' - ','( ',iif(pd.EntityName <> 'SavePharmaceuticalDispensing','Dashboard','Manual' ),' ) ')
			when k.EntityName = 'EntranceVoucherDevolution' then 'Devolución de Comprobante de Entrada'
			when k.EntityName = 'InventoryAdjustment' then 'Ajuste de Inventario'
			when k.EntityName = 'LoanMerchandise' then 'Préstamo de Mercancía'
			when k.EntityName = 'LoanMerchandiseDevolution' then 'Devolución de Préstamo de Mercancía'
			when k.EntityName = 'PharmaceuticalDispensingDevolution' then CONCAT('Devolución de Dispensación Farmacéutica',' - ','( ',iif(pdd.EntityName is null,'Manual','Dashboard' ),' ) ')
			when k.EntityName = 'TransferOrder' then 'Orden de Traslado'
			when k.EntityName = 'TransferOrderDevolution' then 'Devolución de Orden de Traslado'
			when k.EntityName = 'DocumentInvoiceProductSales' then 'Documento Factura de venta de Producto'
			when k.EntityName = 'InventoryControl' then 'Control De Inventarios'
			WHEN k.EntityName = 'BasicBilling' THEN 'Facturación Basica'
			WHEN k.EntityName = 'BasicBillingDevolution' then 'Anulación Factura Basica'
		end + ' - ' + k.EntityCode as DocumentDescription
FROM 
(
		SELECT	CONCAT('Kardex', k.Id) Id, k.DocumentDate, k.ProductId, k.WarehouseId, k.BatchSerialId,
				k.MovementType, k.Quantity, k.PreviousAmountProduct, k.PreviousAmountWarehouse, k.PreviousAmountBatch,
				k.Value, k.PreviousCost, k.PreviousAverageCost, k.AverageCost,
				k.EntityId, k.EntityCode, k.EntityName, k.ImportedEntityId, k.ImportedEntityCode, k.ImportedEntityName,
				k.CreationUser, k.CreationDate, k.AffectInventory
		FROM Inventory.Kardex k
	UNION ALL
		SELECT	CONCAT('KardexControl', k.Id) Id, k.DocumentDate, k.ProductId, k.WarehouseId, k.BatchSerialId,
				k.MovementType, k.Quantity, k.PreviousAmountProduct, k.PreviousAmountWarehouse, k.PreviousAmountBatch,
				0 Value, 0 PreviousCost, 0 PreviousAverageCost, 0 AverageCost,
				k.EntityId, k.EntityCode, k.EntityName, 0 ImportedEntityId, '' ImportedEntityCode, '' ImportedEntityName,
				k.CreationUser, k.CreationDate, 1 AffectInventory
		FROM Inventory.KardexControl k
) k
JOIN Inventory.Warehouse w ON W.Id = K.WarehouseId
JOIN Inventory.InventoryProduct p ON p.Id = k.ProductId
LEFT JOIN Inventory.BatchSerial bs ON Bs.Id = k.BatchSerialId
LEFT JOIN Inventory.PharmaceuticalDispensing pd on pd.Id = k.EntityId and k.EntityName = 'PharmaceuticalDispensing'
LEFT JOIN dbo.ADINGRESO i on i.NUMINGRES = pd.AdmissionNumber
LEFT JOIN dbo.INPACIENT pa  on pa.IPCODPACI = i.IPCODPACI
LEFT JOIN Inventory.PharmaceuticalDispensingDevolution pdd on pdd.Id = k.EntityId and k.EntityName = 'PharmaceuticalDispensingDevolution'
LEFT JOIN dbo.ADINGRESO pddi on pddi.NUMINGRES = pdd.AdmissionNumber
LEFT JOIN dbo.INPACIENT pddpa on pddpa.IPCODPACI = pddi.IPCODPACI
LEFT JOIN Inventory.TransferOrder tor on tor.Id = k.EntityId  And k.EntityName = 'TransferOrder'
LEFT JOIN Common.ThirdParty tpto  on tpto.Id = tor.ThirdPartyId 

LEFT JOIN (SELECT TOP 1 us.IdPerson, us.UserCode FROM [Security].[User] us ) us on us.UserCode = tor.ConfirmationUser
LEFT JOIN [Security].Person sp on us.IdPerson = sp.Id

LEFT JOIN Inventory.EntranceVoucher ev  on ev.Id = k.EntityId And k.EntityName = 'EntranceVoucher'
LEFT JOIN Common.Supplier spev on spev.Id = ev.SupplierId  
LEFT JOIN 
(
	SELECT RE.Code, RED.ProductId, MAX(RED.SourceCode) SourceCode, S.Code SupplierCode, S.Name SupplierName
	FROM Inventory.RemissionEntrance RE
	LEFT JOIN Inventory.RemissionEntranceDetail RED ON RE.Id = RED.RemissionEntranceId
	LEFT JOIN Common.Supplier S ON RE.SupplierId = S.Id
	GROUP BY RE.Code, RED.ProductId, S.Code, S.Name
) RED ON k.EntityCode = RED.Code AND p.Id = RED.ProductId AND k.EntityName = 'RemissionEntrance'
LEFT JOIN Billing.BasicBilling bba ON bba.Id = k.EntityId AND k.EntityName in ('BasicBilling','BasicBillingDevolution')
LEFT JOIN Common.Customer bb ON bb.Id = bba.CustomerId

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista del reporte de kardex de inventario que consolida todos los movimientos de entrada y salida de productos (medicamentos e insumos) combinando el kardex principal y el kardex de control en un único resultado unificado. Muestra para cada movimiento: el producto con su código, nombre, código CUM y registro sanitario; la bodega o almacén; el lote con su fecha de vencimiento; el tipo de movimiento (entrada o salida); cantidades, valores, costo promedio anterior y actual; y los saldos calculados por producto, bodega y lote. Identifica la entidad de negocio que originó cada movimiento, traduciendo los nombres técnicos internos a descripciones legibles en español como ''Dispensación Farmacéutica'', ''Comprobante de Entrada'', ''Orden de Traslado'', ''Ajuste de Inventario'', ''Facturación Básica'', entre otros. Cuando el movimiento está asociado a un paciente (dispensación farmacéutica o su devolución), muestra la cédula y nombre completo del paciente; cuando corresponde a un proveedor o tercero (remisión de entrada, comprobante de entrada, orden de traslado, facturación básica), muestra el NIT y nombre del tercero. Es la fuente principal para reportes de trazabilidad de inventario, análisis de movimientos por bodega y producto, y auditoría de stock de medicamentos e insumos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportKardex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportKardex';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte de kardex que consolida movimientos de inventario y de control, traduciendo entidades origen a descripciones legibles, calculando saldos y costos posteriores al movimiento, e identificando paciente, proveedor o tercero asociado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en Inventory.Kardex y/o Inventory.KardexControl con WarehouseId y ProductId válidos referenciando Inventory.Warehouse e Inventory.InventoryProduct.; Para enriquecer con datos de paciente, los movimientos de dispensación deben tener AdmissionNumber válido en dbo.ADINGRESO con paciente en dbo.INPACIENT.; Para órdenes de traslado se espera un ThirdPartyId o un ConfirmationUser que exista en Security.User y Security.Person.; Para remisiones de entrada y comprobantes de entrada debe existir el proveedor en Common.Supplier; para facturación básica el cliente debe existir en Common.Customer.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Unifica en un solo flujo los movimientos de Inventory.Kardex e Inventory.KardexControl, prefijando el Id con ''Kardex'' o ''KardexControl'' para evitar colisiones.; Para registros provenientes de KardexControl se fuerza Value, PreviousCost, PreviousAverageCost y AverageCost a 0 y AffectInventory a 1.; Los registros de KardexControl no exponen entidad importada (ImportedEntityId=0, códigos vacíos).; Solo se requieren Warehouse y Product (INNER JOIN); BatchSerial y demás entidades relacionadas son opcionales (LEFT JOIN).; Las uniones a entidades de origen (PharmaceuticalDispensing, TransferOrder, EntranceVoucher, etc.) se condicionan al valor de EntityName, garantizando que cada movimiento solo se enriquezca con la entidad que lo originó.; Los saldos calculados (producto, bodega, lote) solo se modifican cuando el movimiento afecta inventario; en caso contrario se preservan los saldos previos.; El costo total calculado se obtiene como AverageCost por la cantidad resultante después de aplicar el movimiento.; DocumentDescription traduce los nombres técnicos de EntityName a etiquetas legibles en español y las concatena con el EntityCode.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Kardex; Bodega/Almacén; Producto/Medicamento; Lote y fecha de vencimiento; Movimiento de entrada/salida; Costo promedio; Saldo por producto, bodega y lote; Dispensación farmacéutica; Devolución de dispensación; Comprobante de entrada; Remisión de entrada/salida/devolución; Orden de traslado; Ajuste de inventario; Préstamo de mercancía; Facturación básica; Inventario en consignación; Paciente; Proveedor; Tercero; Cliente; Control de inventarios', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si k.EntityName = ''RemissionEntrance'' y RED.SourceCode IS NULL → EntityName se muestra como ''Traslado Interno'' else Se conserva ''RemissionEntrance'' como EntityName; si k.EntityName = ''PharmaceuticalDispensing'' → NamePatient = identificación + nombre del paciente obtenido vía AdmissionNumber → ADINGRESO → INPACIENT; descripción concatena ''Dashboard'' si pd.EntityName <> ''SavePharmaceuticalDispensing'', si no ''Manual''; si k.EntityName = ''PharmaceuticalDispensingDevolution'' → NamePatient = identificación + nombre del paciente vía devolución → ADINGRESO → INPACIENT; descripción agrega ''Manual'' si pdd.EntityName IS NULL, si no ''Dashboard''; si k.EntityName = ''TransferOrder'' → Si existe ThirdParty (tpto.Id no null) NamePatient = NIT + nombre del tercero; en caso contrario, identificación + nombre de la persona del usuario que confirmó la orden; si k.EntityName = ''EntranceVoucher'' → NamePatient = código + nombre del proveedor del comprobante de entrada; si k.EntityName = ''RemissionEntrance'' → NamePatient = código + nombre del proveedor de la remisión de entrada; si k.EntityName IN (''BasicBilling'',''BasicBillingDevolution'') → NamePatient = NIT + nombre del cliente de la facturación básica; si k.MovementType = 1 → QuantityEntrance = Quantity y QuantityExit = 0 (movimiento de entrada) else QuantityEntrance = 0 y QuantityExit = Quantity (movimiento de salida); si k.AffectInventory = 1 → Saldos calculados ajustan PreviousAmount sumando o restando Quantity según MovementType (1 suma, distinto resta) else Saldos calculados conservan los PreviousAmount sin modificar', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Kardex; Inventory.KardexControl; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.BatchSerial; Inventory.PharmaceuticalDispensing; dbo.ADINGRESO; dbo.INPACIENT; Inventory.PharmaceuticalDispensingDevolution; Inventory.TransferOrder; Common.ThirdParty; Security.User; Security.Person; Inventory.EntranceVoucher; Common.Supplier; Inventory.RemissionEntrance; Inventory.RemissionEntranceDetail; Billing.BasicBilling; Common.Customer', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardex';
GO
