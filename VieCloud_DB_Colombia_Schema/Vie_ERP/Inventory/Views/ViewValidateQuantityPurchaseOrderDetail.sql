

CREATE VIEW [Inventory].[ViewValidateQuantityPurchaseOrderDetail]
AS

select row_number() over (order by tempEV.PurchaseOrderDetailId asc) Id, 
	   tempEV.PurchaseOrderId, 
	   tempEV.PurchaseOrderDetailId, 
	   sum(tempEV.QuantityRegister + tempRE.QuantityRegister) QuantityRegister,
	   sum(tempEV.QuantityConfirmed + tempRE.QuantityConfirmed) QuantityConfirmed
	   from 
	   (
	   		select pod.PurchaseOrderId, pod.Id PurchaseOrderDetailId, 
	   		isnull(evd.QuantityRegister, 0) QuantityRegister, 
	   		isnull(evd.QuantityConfirmed, 0) QuantityConfirmed
	   		from Inventory.PurchaseOrderDetail pod
	   		left join
	   	(
	   		select evd.PurchaseOrderDetailId, 
	   		sum(case when ev.Status <> 2 then evd.Quantity else 0 end) QuantityRegister, 
	   		sum(case when ev.Status = 2 then evd.Quantity else 0 end) QuantityConfirmed
	   		from Inventory.EntranceVoucherDetail evd 
	   		inner join Inventory.EntranceVoucher ev on evd.EntranceVoucherId = ev.Id	
	   		group by evd.PurchaseOrderDetailId
	   	) evd on pod.Id = evd.PurchaseOrderDetailId
	   ) tempEV
	   inner join 
	   (
	   		select pod.PurchaseOrderId, pod.Id PurchaseOrderDetailId, 
	   		isnull(red.QuantityRegister, 0) QuantityRegister, 
	   		isnull(red.QuantityConfirmed, 0) QuantityConfirmed
	   		from Inventory.PurchaseOrderDetail pod
	   		left join
	   	(
	   		select red.PurchaseOrderDetailId, 
	   		sum(case when re.Status <> 2 then red.Quantity else 0 end) QuantityRegister, 
	   		sum(case when re.Status = 2 then red.Quantity else 0 end) QuantityConfirmed
	   		from Inventory.RemissionEntranceDetail red 
	   		inner join Inventory.RemissionEntrance re on red.RemissionEntranceId = re.Id	
	   		group by red.PurchaseOrderDetailId
	   	) red on pod.Id = red.PurchaseOrderDetailId
	   ) tempRE on tempRE.PurchaseOrderId = tempEV.PurchaseOrderId and tempRE.PurchaseOrderDetailId = tempEV.PurchaseOrderDetailId
	   group by tempEV.PurchaseOrderId, tempEV.PurchaseOrderDetailId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida y valida las cantidades recibidas por ítem (detalle) de cada orden de compra de inventario, combinando los ingresos registrados a través de comprobantes de entrada (vales de bodega) y remisiones de entrada de proveedores. Para cada línea de orden de compra muestra la cantidad total registrada (pendiente de confirmación) y la cantidad confirmada (comprobantes en estado confirmado/aprobado), permitiendo controlar cuánto se ha recibido físicamente versus cuánto está oficialmente aceptado frente a lo ordenado. Es clave para la gestión de recepciones parciales, el seguimiento del cumplimiento de órdenes de compra y el cierre de pedidos de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewValidateQuantityPurchaseOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewValidateQuantityPurchaseOrderDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida por cada detalle de orden de compra las cantidades ingresadas vía comprobantes de entrada y remisiones de entrada, separando lo solo registrado de lo confirmado, para validar cantidades recibidas frente a la orden.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewValidateQuantityPurchaseOrderDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de detalles de orden de compra en Inventory.PurchaseOrderDetail.; Integridad referencial entre EntranceVoucherDetail y EntranceVoucher, y entre RemissionEntranceDetail y RemissionEntrance.; El valor 2 en Status representa el estado ''confirmado'' tanto para EntranceVoucher como para RemissionEntrance.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewValidateQuantityPurchaseOrderDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las cantidades expuestas combinan (suman) los aportes provenientes tanto de comprobantes de entrada (EntranceVoucher) como de remisiones de entrada (RemissionEntrance) para el mismo detalle de orden de compra.; Los detalles sin entradas asociadas se reportan con 0 (vía ISNULL) en lugar de excluirse.; El estado con valor 2 representa ''confirmado''; cualquier otro estado se trata como ''registrado'' (no confirmado).; Solo se incluyen detalles de orden de compra que existan en ambos lados (EV y RE) por el INNER JOIN entre los subconjuntos tempEV y tempRE.; Cada fila de salida corresponde a una combinación única de PurchaseOrderId + PurchaseOrderDetailId.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewValidateQuantityPurchaseOrderDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de compra; Detalle de orden de compra; Comprobante de entrada de inventario; Remisión de entrada de inventario; Cantidad registrada vs cantidad confirmada; Estado de comprobante (confirmado = 2)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewValidateQuantityPurchaseOrderDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve, por (PurchaseOrderId, PurchaseOrderDetailId), la suma de QuantityRegister (entradas con Status<>2) y QuantityConfirmed (entradas con Status=2), combinando EntranceVoucher y RemissionEntrance.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewValidateQuantityPurchaseOrderDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Status del EntranceVoucher distinto de 2 → La cantidad del detalle se acumula como QuantityRegister (registrada/pendiente) else Si Status = 2, la cantidad se acumula como QuantityConfirmed (confirmada); si Status del RemissionEntrance distinto de 2 → La cantidad del detalle se acumula como QuantityRegister else Si Status = 2, la cantidad se acumula como QuantityConfirmed', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewValidateQuantityPurchaseOrderDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PurchaseOrderDetail; Inventory.EntranceVoucherDetail; Inventory.EntranceVoucher; Inventory.RemissionEntranceDetail; Inventory.RemissionEntrance', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewValidateQuantityPurchaseOrderDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewValidateQuantityPurchaseOrderDetail';
GO
