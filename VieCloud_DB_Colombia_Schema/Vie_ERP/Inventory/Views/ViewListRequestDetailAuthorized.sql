

CREATE VIEW [Inventory].[ViewListRequestDetailAuthorized]

AS
		SELECT	ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) Id
				,k.InventoryRequestDetailOtherId
				,k.OperatingUnitId
				,k.RequestType
				,k.Status
				,k.RequestTypeName
				,k.ConfirmationDate
				,k.Code
				,k.UserConfirmation
				,k.ItemDescription
				,k.FunctionUnitCodeName
				,k.WarehouseCodeName
				,k.MovementTypeName
				,k.Quantity
				,k.QuantityDelivered
				,k.OutstandingQuantity

		FROM(	SELECT 
				c.InventoryRequestDetailOtherId
				,c.OperatingUnitId
				,c.RequestType
				,c.Status
				,c.RequestTypeName
				,c.ConfirmationDate
				,c.Code
				,c.UserConfirmation
				,c.ItemDescription
				,c.FunctionUnitCodeName
				,c.WarehouseCodeName
				,c.MovementTypeName
				,c.Quantity
				,c.QuantityDelivered
				,c.OutstandingQuantity

		FROM (	SELECT
				irdo.Id InventoryRequestDetailOtherId
				,ir.OperatingUnitId
				,ir.RequestType
				,irdo.Status
				,IIf(RequestType = 1, 'Unidad Funcional', 'Almacen') RequestTypeName
				,ir.ConfirmationDate
				,ir.Code
				,p.Fullname UserConfirmation
				,CONCAT(ISNULL(ip.Code, isu.Code), ' - ', ISNULL(ip.Name, isu.SupplieName)) ItemDescription
				,CONCAT(fu.Code, ' - ', fu.Name) FunctionUnitCodeName
				,CONCAT(w.Code, ' - ', w.Name) WarehouseCodeName
				,IIF(ir.MovementType = 1, 'Consumo',IIF(ir.MovementType = 2, 'Traslado', '')) MovementTypeName
				,irdo.Quantity
				,SUM(ISNULL(tod.Quantity, 0)) QuantityDelivered
				,(irdo.Quantity - ISNULL(tod.Quantity, 0)) OutstandingQuantity
		FROM Inventory.InventoryRequest ir 
		JOIN Inventory.InventoryRequestDetailOther irdo ON irdo.InventoryRequestId = ir.Id
		LEFT JOIN Inventory.InventoryProduct ip ON ip.Id = irdo.InventoryProductId
		LEFT JOIN Inventory.InventorySupplie isu ON isu.Id = irdo.SupplieId
		LEFT JOIN Inventory.TransferOrderDetail tod ON tod.InventoryRequestDetailOtherId = irdo.Id
		LEFT JOIN Payroll.FunctionalUnit fu ON fu.Id = ir.TargetFunctionalUnitId
		LEFT JOIN Inventory.Warehouse w ON w.Id = ir.TargetWarehouseId
		LEFT JOIN Security.[User] u ON ir.ConfirmationUser = u.UserCode
		LEFT JOIN Security.Person p ON p.Id = u.IdPerson
		WHERE irdo.Status = 2 AND irdo.OutstandingQuantity > 0 and irdo.Quantity > 0
		GROUP BY irdo.Id, ir.OperatingUnitId, ir.RequestType, irdo.Status, ir.ConfirmationDate, ir.Code, p.Fullname, ip.Code
		,ip.Name, fu.Code, fu.Name, w.Code, w.Name, ir.MovementType, irdo.Quantity, tod.Quantity, isu.Code, isu.SupplieName) c

		UNION ALL

		SELECT	 tod.InventoryRequestDetailOtherId
				,NULL OperatingUnitId
				,NULL RequestType
				,ot.Status
				,'' RequestTypeName
				,ot.ConfirmationDate
				,ot.Code
				,p.Fullname UserConfirmation
				,'' ItemDescription
				,'' FunctionUnitCodeName
				,CONCAT(w.Code, ' - ', w.Name) WarehouseCodeName
				,'' MovementTypeName
				,tod.Quantity
				,0 QuantityDelivered
				,0 OutstandingQuantity
		FROM Inventory.TransferOrderDetail tod
		JOIN Inventory.TransferOrder ot ON ot.Id = tod.TransferOrderId
		LEFT JOIN Inventory.InventoryRequestDetailOther irdo ON irdo.Id = tod.InventoryRequestDetailOtherId
		LEFT JOIN Inventory.Warehouse w ON w.Id = ot.SourceWarehouseId
		LEFT JOIN Security.[User] u ON ot.ConfirmationUser = u.UserCode
		LEFT JOIN Security.Person p ON p.Id = u.IdPerson
		WHERE ot.Status = 2 AND irdo.OutstandingQuantity > 0) k
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que lista el detalle de solicitudes de inventario autorizadas (aprobadas) que aún tienen cantidades pendientes de despacho. Combina solicitudes dirigidas a unidades funcionales o bodegas (almacenes) con el detalle de sus ítems —medicamentos del catálogo de productos o insumos/dispositivos médicos— mostrando cuánto se pidió, cuánto ya fue entregado mediante órdenes de transferencia y cuánto queda por despachar (cantidad pendiente u outstanding). Incluye además las órdenes de transferencia ya generadas para esas solicitudes, indicando la bodega origen del despacho y el usuario que confirmó la autorización. Sirve para el seguimiento y control de despachos pendientes en la gestión de almacenes y bodegas, permitiendo identificar solicitudes autorizadas con saldo abierto en los procesos de traslado o consumo de insumos y medicamentos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewListRequestDetailAuthorized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewListRequestDetailAuthorized';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los detalles de solicitudes de inventario autorizadas (con saldo pendiente) junto con sus órdenes de transferencia confirmadas asociadas, para seguimiento de despachos.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailAuthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen solicitudes de inventario (InventoryRequest) con sus detalles en InventoryRequestDetailOther.; Las órdenes de transferencia (TransferOrder) están vinculadas al detalle de solicitud mediante TransferOrderDetail.InventoryRequestDetailOtherId.; Los usuarios de confirmación están registrados en Security.User con persona asociada en Security.Person.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailAuthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran detalles de solicitud con Status = 2 (autorizado) y cantidad pendiente y solicitada mayor a cero.; Solo se consideran órdenes de transferencia con Status = 2 (confirmadas) cuyo detalle de solicitud aún tenga saldo pendiente.; El ítem se identifica indistintamente como producto (InventoryProduct) o insumo (InventorySupplie), pero nunca ambos en una misma fila.; OutstandingQuantity en la rama de solicitudes equivale a la cantidad solicitada menos la cantidad ya entregada en órdenes de transferencia.; Cada fila tiene un Id secuencial generado por ROW_NUMBER sin orden determinístico.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailAuthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de inventario; Detalle de solicitud autorizada; Orden de transferencia; Bodega/Almacén; Unidad funcional; Producto de inventario; Insumo médico; Movimiento de consumo; Movimiento de traslado; Cantidad entregada y saldo pendiente; Usuario de confirmación', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailAuthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve el conjunto unificado (UNION ALL) de detalles de solicitud autorizados con saldo pendiente y órdenes de transferencia confirmadas con saldo pendiente en el detalle origen.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailAuthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si irdo.Status = 2 AND irdo.OutstandingQuantity > 0 AND irdo.Quantity > 0 → Incluye el detalle de la solicitud de inventario como fila de tipo solicitud autorizada, calculando QuantityDelivered como suma de cantidades de TransferOrderDetail y OutstandingQuantity = Quantity - entregado.; si ot.Status = 2 AND irdo.OutstandingQuantity > 0 → Incluye la orden de transferencia confirmada como fila adicional con la bodega origen, sin tipo de solicitud ni descripción de ítem/unidad funcional.; si ir.RequestType = 1 → Etiqueta el tipo como ''Unidad Funcional'' else Etiqueta el tipo como ''Almacen''; si ir.MovementType = 1 → Etiqueta el movimiento como ''Consumo'' else Si MovementType = 2 etiqueta ''Traslado''; en otro caso cadena vacía.; si InventoryProduct existe (ip) → Usa código y nombre de InventoryProduct para describir el ítem else Usa código y SupplieName de InventorySupplie como descripción del ítem.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailAuthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryRequest; Inventory.InventoryRequestDetailOther; Inventory.InventoryProduct; Inventory.InventorySupplie; Inventory.TransferOrderDetail; Inventory.TransferOrder; Payroll.FunctionalUnit; Inventory.Warehouse; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailAuthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailAuthorized';
GO
