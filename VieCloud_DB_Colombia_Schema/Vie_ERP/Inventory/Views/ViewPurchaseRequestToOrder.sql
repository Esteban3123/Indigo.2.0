CREATE VIEW [Inventory].[ViewPurchaseRequestToOrder]
AS
SELECT	prd.Id, 
		pr.Id PurchaseRequestId, 
		pr.Code PurchaseRequestCode, 		
		prd.InventoryProductId, 
		ip.Code ProductCode,
		ip.Name ProductName, 
		prd.OutstandingQuantity,		
		pr.CreationDate
FROM Inventory.PurchaseRequest pr
JOIN Inventory.PurchaseRequestDetail prd ON pr.Id = prd.PurchaseRequestId
JOIN Inventory.InventoryProduct ip ON prd.InventoryProductId = ip.Id
WHERE pr.Status = 2 AND pr.RequestTypeId = 1
	AND prd.Status = 1 AND prd.OutstandingQuantity > 0
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los ítems de solicitudes de compra confirmadas (tipo solicitud estándar) que aún tienen cantidad pendiente de convertir en orden de compra. Combina las solicitudes de compra, su detalle y el catálogo de productos de inventario para exponer únicamente los renglones aprobados y con saldo pendiente mayor a cero. Sirve como fuente de trabajo para el proceso de generación de órdenes de compra, permitiendo identificar qué productos, en qué cantidades y de qué solicitud de origen se deben ordenar a proveedores.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewPurchaseRequestToOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewPurchaseRequestToOrder';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los ítems de solicitudes de compra aprobadas y pendientes que aún tienen cantidad por atender, listos para ser convertidos en órdenes de compra.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPurchaseRequestToOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las solicitudes deben existir en estado Status = 2 y con RequestTypeId = 1.; El detalle debe estar en Status = 1 con OutstandingQuantity > 0.; El producto referenciado debe existir en el catálogo de inventario.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPurchaseRequestToOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen solicitudes de compra cuyo Status = 2 (estado habilitado para convertirse en orden).; Solo se exponen solicitudes con RequestTypeId = 1 (tipo de solicitud específico, excluyendo otros tipos).; Solo se incluyen ítems de detalle con Status = 1 (activos/vigentes).; Solo se exponen ítems con cantidad pendiente (OutstandingQuantity) mayor que cero, garantizando que ya no aparezcan ítems totalmente atendidos.; Cada fila representa un ítem de solicitud aún convertible a orden de compra, enriquecido con datos del producto.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPurchaseRequestToOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de compra; Detalle de solicitud de compra; Producto de inventario; Cantidad pendiente; Orden de compra', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPurchaseRequestToOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.PurchaseRequestDetail: Devuelve ítems de solicitud de compra cuando pr.Status = 2 AND pr.RequestTypeId = 1 AND prd.Status = 1 AND prd.OutstandingQuantity > 0, junto con código y nombre del producto.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPurchaseRequestToOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PurchaseRequest; Inventory.PurchaseRequestDetail; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPurchaseRequestToOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPurchaseRequestToOrder';
GO
