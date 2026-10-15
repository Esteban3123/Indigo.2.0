CREATE VIEW [dbo].[Producto_Costo_Precio]
AS
SELECT Inventory.InventoryProduct.Code AS Codigo_Producto, Inventory.InventoryProduct.Name AS Nombre, Inventory.InventoryProduct.ProductCost AS Costo_promedio, Inventory.InventoryProduct.FinalProductCost AS Ultimo_Costo, 
             Inventory.ProductRateDetail.SalesValue AS venta_Tarifa_Institucional
FROM   Inventory.InventoryProduct INNER JOIN
             Inventory.ProductRateDetail ON Inventory.InventoryProduct.Id = Inventory.ProductRateDetail.ProductId