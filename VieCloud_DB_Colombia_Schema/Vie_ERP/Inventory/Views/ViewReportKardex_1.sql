

CREATE VIEW [Inventory].[ViewReportKardex_1]
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
			else ''
		end as NamePatient,

		--IIF
		--(
		--	k.EntityName = 'PharmaceuticalDispensing',
		--	(pa.IPCODPACI + ' - ' + pa.IPNOMCOMP), 
		--	IIF
		--	(
		--		k.EntityName = 'PharmaceuticalDispensingDevolution', 
		--		(pddpa.IPCODPACI + ' - ' + pddpa.IPNOMCOMP),
		--		IIF
		--		(
		--			k.EntityName = 'TransferOrder', 
		--			(tpto.Nit + ' - ' + tpto.Name),
		--			IIF
		--			(
		--				k.EntityName = 'EntranceVoucher', 
		--				(spev.Code + ' - ' + spev.Name),
		--				''
		--			)
		--		)
		--	)
		--) as NamePatient,
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
			when k.EntityName = 'PharmaceuticalDispensing' then 'Dispensación Farmacéutica'
			when k.EntityName = 'EntranceVoucherDevolution' then 'Devolución de Comprobante de Entrada'
			when k.EntityName = 'InventoryAdjustment' then 'Ajuste de Inventario'
			when k.EntityName = 'LoanMerchandise' then 'Préstamo de Mercancía'
			when k.EntityName = 'LoanMerchandiseDevolution' then 'Devolución de Préstamo de Mercancía'
			when k.EntityName = 'PharmaceuticalDispensingDevolution' then 'Devolución de Dispensación Farmacéutica'
			when k.EntityName = 'TransferOrder' then 'Orden de Traslado'
			when k.EntityName = 'TransferOrderDevolution' then 'Devolución de Orden de Traslado'
			when k.EntityName = 'DocumentInvoiceProductSales' then 'Documento Factura de venta de Producto'
			when k.EntityName = 'InventoryControl' then 'Control De Inventarios'
		end + ' - ' + k.EntityCode as DocumentDescription
FROM 
(
		SELECT	CONCAT('Kardex', k.Id) Id, k.DocumentDate, k.ProductId, k.WarehouseId, k.BatchSerialId,
				k.MovementType, k.Quantity, k.PreviousAmountProduct, k.PreviousAmountWarehouse, k.PreviousAmountBatch,
				k.Value, k.PreviousCost, k.PreviousAverageCost, k.AverageCost,
				k.EntityId, k.EntityCode, k.EntityName, k.ImportedEntityId, k.ImportedEntityCode, k.ImportedEntityName,
				k.CreationUser, k.CreationDate, k.AffectInventory
		FROM Inventory.Kardex k WITH (NOLOCK)
	UNION ALL
		SELECT	CONCAT('KardexControl', k.Id) Id, k.DocumentDate, k.ProductId, k.WarehouseId, k.BatchSerialId,
				k.MovementType, k.Quantity, k.PreviousAmountProduct, k.PreviousAmountWarehouse, k.PreviousAmountBatch,
				0 Value, 0 PreviousCost, 0 PreviousAverageCost, 0 AverageCost,
				k.EntityId, k.EntityCode, k.EntityName, 0 ImportedEntityId, '' ImportedEntityCode, '' ImportedEntityName,
				k.CreationUser, k.CreationDate, 1 AffectInventory
		FROM Inventory.KardexControl k WITH (NOLOCK)
) k
JOIN Inventory.Warehouse w  WITH (NOLOCK)ON W.Id = K.WarehouseId
JOIN Inventory.InventoryProduct p WITH (NOLOCK) ON p.Id = k.ProductId
LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON Bs.Id = k.BatchSerialId
LEFT JOIN Inventory.PharmaceuticalDispensing pd WITH (NOLOCK) on pd.Id = k.EntityId and k.EntityName = 'PharmaceuticalDispensing'
LEFT JOIN dbo.ADINGRESO i  WITH (NOLOCK)on i.NUMINGRES = pd.AdmissionNumber
LEFT JOIN dbo.INPACIENT pa  WITH (NOLOCK) on pa.IPCODPACI = i.IPCODPACI
LEFT JOIN Inventory.PharmaceuticalDispensingDevolution pdd WITH (NOLOCK) on pdd.Id = k.EntityId and k.EntityName = 'PharmaceuticalDispensingDevolution'
LEFT JOIN dbo.ADINGRESO pddi  WITH (NOLOCK)on pddi.NUMINGRES = pdd.AdmissionNumber
LEFT JOIN dbo.INPACIENT pddpa  WITH (NOLOCK)on pddpa.IPCODPACI = pddi.IPCODPACI
LEFT JOIN Inventory.TransferOrder tor  WITH (NOLOCK)on tor.Id = k.EntityId  And k.EntityName = 'TransferOrder'
LEFT JOIN Common.ThirdParty tpto  WITH (NOLOCK) on tpto.Id = tor.ThirdPartyId 

LEFT JOIN [Security].[User] us on tor.ConfirmationUser = us.UserCode
LEFT JOIN [Security].Person sp on us.IdPerson = sp.Id

LEFT JOIN Inventory.EntranceVoucher ev  WITH (NOLOCK) on ev.Id = k.EntityId And k.EntityName = 'EntranceVoucher'
LEFT JOIN Common.Supplier spev WITH (NOLOCK) on spev.Id = ev.SupplierId  
LEFT JOIN 
(
	SELECT RE.Code, RED.ProductId, MAX(RED.SourceCode) SourceCode
	FROM Inventory.RemissionEntrance RE WITH (NOLOCK)
	LEFT JOIN Inventory.RemissionEntranceDetail RED WITH (NOLOCK) ON RE.Id = RED.RemissionEntranceId
	GROUP BY RE.Code, RED.ProductId
) RED ON k.EntityCode = RED.Code AND p.Id = RED.ProductId AND k.EntityName = 'RemissionEntrance'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte tipo kardex que consolida movimientos de inventario desde las tablas `Kardex` y `KardexControl`, aplanando información de bodegas, productos, lotes y tipos de documento para consumo en reportes. Calcula cantidades y saldos de entradas/salidas, costos promedio y totales anteriores derivados. Resuelve el tercero o entidad relacionada según el tipo de movimiento: paciente para dispensaciones farmacéuticas y devoluciones, proveedor para comprobantes de entrada, tercero o usuario para órdenes de traslado. Traduce al español los tipos de documento de inventario y clasifica remisiones de entrada como traslado interno o externo según existencia de código fuente.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardex_1';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardex_1';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada para reporte de Kardex que unifica movimientos de Inventory.Kardex y Inventory.KardexControl, enriquece con datos de bodega, producto, lote, paciente/tercero/proveedor según el tipo de documento y calcula saldos y costos antes/después del movimiento.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardex_1';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en Inventory.Kardex y/o Inventory.KardexControl con WarehouseId y ProductId válidos; Los movimientos deben tener MovementType definido (1=entrada, 2=salida); Para resolver paciente: las dispensaciones farmacéuticas deben tener AdmissionNumber asociado a dbo.ADINGRESO y a dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardex_1';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El Id del registro se prefija con ''Kardex'' para movimientos de Inventory.Kardex y con ''KardexControl'' para movimientos de Inventory.KardexControl, garantizando unicidad entre ambas fuentes; Los movimientos provenientes de KardexControl siempre se exponen con AffectInventory=1 y costos/valor en cero; QuantityEntrance y QuantityExit son mutuamente excluyentes (una de las dos siempre es 0); Los saldos calculados solo se modifican cuando AffectInventory=1; en caso contrario reflejan el saldo previo sin cambios; PreviousCostTotalCalculated = PreviousAverageCost * PreviousAmountProduct; Las descripciones combinadas (WarehouseDescription, ProductDescription) siempre tienen formato ''Code - Name''; Para RemissionEntrance, si no se encuentra SourceCode en el detalle se interpreta como traslado interno', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardex_1';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Kardex; Movimiento de inventario (entrada/salida); Bodega; Producto / Medicamento; Lote y fecha de vencimiento; Costo promedio; Paciente; Admisión / Ingreso; Dispensación farmacéutica; Devolución de dispensación; Orden de traslado; Comprobante de entrada; Remisión de entrada / salida / devolución; Inventario en consignación; Ajuste de inventario; Préstamo de mercancía; Factura de venta de producto; Control de inventarios; Tercero / Proveedor; Registro sanitario / CUM', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardex_1';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve la unión de Inventory.Kardex (con valores y costos reales) y de Inventory.KardexControl (con Value, PreviousCost, PreviousAverageCost, AverageCost forzados a 0 y AffectInventory forzado a 1, ImportedEntity vacío)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardex_1';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si k.EntityName = ''RemissionEntrance'' y RED.SourceCode IS NULL → EntityName se reporta como ''Traslado Interno'' else Se conserva ''RemissionEntrance'' (o el EntityName original para los demás casos); si k.EntityName = ''PharmaceuticalDispensing'' → NamePatient se construye con IPCODPACI + '' - '' + IPNOMCOMP del paciente vinculado vía PharmaceuticalDispensing → ADINGRESO → INPACIENT; si k.EntityName = ''PharmaceuticalDispensingDevolution'' → NamePatient se obtiene del paciente asociado a la devolución vía AdmissionNumber → INPACIENT; si k.EntityName = ''TransferOrder'' y existe ThirdParty (tpto.Id no nulo) → NamePatient se forma con Nit + '' - '' + Name del tercero else Si no hay tercero, se usa Identification + '' - '' + Fullname del usuario de confirmación (Security.Person); si k.EntityName = ''EntranceVoucher'' → NamePatient se construye con Code + '' - '' + Name del proveedor (Common.Supplier); si k.MovementType = 1 → QuantityEntrance = Quantity y QuantityExit = 0 else QuantityEntrance = 0 y QuantityExit = Quantity (cuando MovementType = 2); si k.AffectInventory = 1 y k.MovementType = 1 → Saldos calculados = PreviousAmount(Product/Warehouse/Batch) + Quantity (suma al saldo) else Si AffectInventory = 1 y MovementType = 2 se resta Quantity; si AffectInventory <> 1 se conserva el PreviousAmount sin alterar; si EntityName conocido (ConsignmentInventoryRemission, RemissionEntrance, RemissionOutput, RemissionDevolution, EntranceVoucher, PharmaceuticalDispensing, EntranceVoucherDevolution, InventoryAdjustment, LoanMerchandise, LoanMerchandiseDevolution, PharmaceuticalDispensingDevolution, TransferOrder, TransferOrderDevolution, DocumentInvoiceProductSales, InventoryControl) → DocumentDescription se traduce a etiqueta en español concatenada con EntityCode else Si EntityName no está mapeado, la parte descriptiva queda NULL y DocumentDescription resulta NULL', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardex_1';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Kardex; Inventory.KardexControl; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.BatchSerial; Inventory.PharmaceuticalDispensing; dbo.ADINGRESO; dbo.INPACIENT; Inventory.PharmaceuticalDispensingDevolution; Inventory.TransferOrder; Common.ThirdParty; Security.User; Security.Person; Inventory.EntranceVoucher; Common.Supplier; Inventory.RemissionEntrance; Inventory.RemissionEntranceDetail', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardex_1';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardex_1';
GO
