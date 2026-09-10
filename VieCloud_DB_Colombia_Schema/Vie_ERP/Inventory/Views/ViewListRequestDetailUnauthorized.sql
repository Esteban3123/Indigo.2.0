

CREATE VIEW [Inventory].[ViewListRequestDetailUnauthorized]
AS
	SELECT	 irdo.Id
			,ir.OperatingUnitId
			,ir.RequestType
			,irdo.Status
			,IIf(RequestType = 1, 'Unidad Funcional', 'Almacen') RequestTypeName
			,ir.ConfirmationDate
			,ir.Code
			,p.Fullname UserConfirmation
			,IIF(ip.Id IS NULL,CONCAT(isu.Code,' - ',isu.SupplieName), CONCAT(ip.Code, ' - ', ip.Name)) ItemDescription
			,CONCAT(fu.Code, ' - ', fu.Name) FunctionUnitCodeName
			,CONCAT(w.Code, ' - ', w.Name) WarehouseCodeName
			,IIF(ir.MovementType = 1, 'Consumo',IIF(ir.MovementType = 2, 'Traslado', '')) MovementTypeName
			,irdo.OriginalQuantity
			,irdo.OutstandingQuantity
			,irdo.Quantity
			,(irdo.OriginalQuantity - irdo.Quantity) QuantityRejected
	FROM Inventory.InventoryRequest ir
	JOIN Inventory.InventoryRequestDetailOther irdo ON irdo.InventoryRequestId = ir.Id
	LEFT JOIN Inventory.InventoryProduct ip ON ip.Id = irdo.InventoryProductId
	LEFT JOIN Inventory.InventorySupplie isu on isu.Id = irdo.SupplieId 
	LEFT JOIN Payroll.FunctionalUnit fu ON fu.Id = ir.TargetFunctionalUnitId
	LEFT JOIN Inventory.Warehouse w ON w.Id = ir.TargetWarehouseId
	LEFT JOIN Security.[User] u ON ir.ConfirmationUser = u.UserCode
	LEFT JOIN Security.Person p ON p.Id = u.IdPerson
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el detalle de ítems no autorizados (pendientes o rechazados) en las solicitudes de inventario, combinando tanto medicamentos como insumos o dispositivos médicos. Para cada línea de solicitud muestra la descripción del producto, las cantidades originales solicitadas, despachadas, pendientes y rechazadas, junto con la unidad funcional o bodega destino, el tipo de movimiento (consumo o traslado), la fecha de confirmación y el nombre completo del usuario que confirmó la solicitud. Sirve para el seguimiento y control de solicitudes de inventario que tienen ítems sin despachar o no autorizados, permitiendo identificar quiénes solicitaron, qué se pidió y cuánto quedó pendiente de atención.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewListRequestDetailUnauthorized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewListRequestDetailUnauthorized';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los detalles de solicitudes de inventario no autorizadas, enriquecidos con descripción del ítem (producto o insumo), unidad funcional, bodega, usuario que confirma y cantidades originales/pendientes/rechazadas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailUnauthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en Inventory.InventoryRequest con su detalle en Inventory.InventoryRequestDetailOther vinculados por InventoryRequestId.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailUnauthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La cantidad rechazada se calcula siempre como OriginalQuantity - Quantity.; Un detalle de solicitud referencia un producto o un insumo, no ambos: si InventoryProductId es nulo se usa SupplieId.; Solo se incluyen detalles que pertenezcan a una InventoryRequest existente (JOIN obligatorio con la cabecera).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailUnauthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de inventario; Detalle de solicitud no autorizada; Tipo de solicitud (Unidad Funcional/Almacén); Tipo de movimiento (Consumo/Traslado); Producto de inventario; Insumo; Unidad funcional destino; Bodega/almacén destino; Usuario de confirmación; Cantidad original/pendiente/rechazada', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailUnauthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.InventoryRequestDetailOther: Devuelve cada detalle (irdo) unido a su solicitud (ir) con datos calculados de tipo de solicitud, tipo de movimiento, descripción del ítem y cantidad rechazada.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailUnauthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RequestType = 1 → Etiqueta la solicitud como ''Unidad Funcional'' else Etiqueta la solicitud como ''Almacen''; si ip.Id IS NULL (no hay InventoryProduct asociado al detalle) → Describe el ítem usando InventorySupplie (Code - SupplieName) else Describe el ítem usando InventoryProduct (Code - Name); si ir.MovementType = 1 → Tipo de movimiento es ''Consumo'' else Si MovementType = 2 → ''Traslado''; en otro caso cadena vacía', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailUnauthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryRequest; Inventory.InventoryRequestDetailOther; Inventory.InventoryProduct; Inventory.InventorySupplie; Payroll.FunctionalUnit; Inventory.Warehouse; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailUnauthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailUnauthorized';
GO
