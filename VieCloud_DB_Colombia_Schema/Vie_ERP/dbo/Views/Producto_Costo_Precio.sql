CREATE VIEW [dbo].[Producto_Costo_Precio]
AS
SELECT Inventory.InventoryProduct.Code AS Codigo_Producto, Inventory.InventoryProduct.Name AS Nombre, Inventory.InventoryProduct.ProductCost AS Costo_promedio, Inventory.InventoryProduct.FinalProductCost AS Ultimo_Costo, 
             Inventory.ProductRateDetail.SalesValue AS venta_Tarifa_Institucional
FROM   Inventory.InventoryProduct INNER JOIN
             Inventory.ProductRateDetail ON Inventory.InventoryProduct.Id = Inventory.ProductRateDetail.ProductId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el costo y precio de venta de cada producto del inventario (medicamentos, insumos y dispositivos médicos). Combina el catálogo maestro de productos con el detalle de tarifas institucionales para mostrar, en una sola consulta, el código del producto, su nombre, el costo promedio, el último costo registrado y el valor de venta según la tarifa institucional vigente. Sirve para comparar márgenes entre costo y precio de venta, apoyar decisiones de compra y facturación, y alimentar reportes de rentabilidad por producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Producto_Costo_Precio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Producto_Costo_Precio';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, por cada producto del inventario con tarifa asociada, su código, nombre, costo promedio, último costo y valor de venta de la tarifa institucional, para análisis de costo-precio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Producto_Costo_Precio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de productos en el catálogo de inventario con registros de tarifa asociados mediante ProductId.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Producto_Costo_Precio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen productos que tengan al menos un detalle de tarifa asociado (INNER JOIN con ProductRateDetail).; Un mismo producto puede aparecer múltiples veces si tiene varios registros de tarifa, ya que no se filtra por tarifa específica ni se agrega.; Se exponen costos (promedio y último) junto con el valor de venta, permitiendo comparativa costo vs precio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Producto_Costo_Precio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto de inventario; Costo promedio; Último costo; Tarifa institucional; Valor de venta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Producto_Costo_Precio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.InventoryProduct: Devuelve una fila por cada combinación producto-tarifa cuando InventoryProduct.Id = ProductRateDetail.ProductId, incluyendo costo promedio, último costo y valor de venta.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Producto_Costo_Precio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; Inventory.ProductRateDetail', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Producto_Costo_Precio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Producto_Costo_Precio';
GO
