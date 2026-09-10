CREATE VIEW [Inventory].[ViewReportPriceListRate]
AS
SELECT        prd.Id, prd.ProductRateId, prd.ProductId, prd.InitialDate, prd.EndDate, prd.SalesValue, prd.SalesValueWithSurcharge, p.Code AS CodeProduct, p.Name AS NameProduct, pu.Name AS NamePackagin, 
                         g.Code AS CodeGroup, g.Name AS NameGroup, sg.Code AS CodeSubgroup, sg.Name AS NameSubgroup, pr.Code AS CodeRate, pr.Code + ' - ' + pr.Name AS RateManual
FROM            Inventory.InventoryProduct AS p INNER JOIN
                         Inventory.ProductGroup AS g ON p.ProductGroupId = g.Id INNER JOIN
                         Inventory.ProductSubGroup AS sg ON sg.Id = p.ProductSubGroupId INNER JOIN
                         Inventory.PackagingUnit AS pu ON pu.Id = p.PackagingUnitId INNER JOIN
                         Inventory.ProductRateDetail AS prd ON prd.ProductId = p.Id INNER JOIN
                         Inventory.ProductRate AS pr ON pr.Id = prd.ProductRateId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de precios de productos por tarifa: consolida los valores de venta vigentes (con y sin recargo) de cada producto del inventario —medicamentos, insumos y dispositivos médicos— cruzados con su grupo, subgrupo, unidad de empaque y la tarifa a la que pertenecen. Integra el catálogo de productos, la clasificación por grupos y subgrupos, las presentaciones de empaque y el detalle de tarifas para generar un reporte completo de precios que puede usarse en consultas de lista de precios, cotizaciones y validación de tarifas contractuales. Es útil para áreas de farmacia, suministros y facturación que necesitan conocer el precio de venta de un artículo según la tarifa aplicable en un periodo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportPriceListRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportPriceListRate';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone un reporte consolidado de precios de productos por tarifa, combinando datos del producto, su grupo, subgrupo, unidad de empaque y la tarifa con sus vigencias y valores de venta.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPriceListRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El producto debe estar asociado a un grupo (ProductGroupId), subgrupo (ProductSubGroupId) y unidad de empaque (PackagingUnitId) válidos.; El producto debe tener al menos un detalle de tarifa (ProductRateDetail) asociado.; El detalle de tarifa debe referenciar una tarifa (ProductRate) existente.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPriceListRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen productos que tengan completos sus vínculos a grupo, subgrupo, unidad de empaque y al menos una tarifa con detalle (ningún LEFT JOIN).; El campo RateManual siempre se compone como ''Code - Name'' de la tarifa.; Cada fila refleja la vigencia (InitialDate/EndDate) y los valores de venta (con y sin recargo) del detalle de tarifa.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPriceListRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto de inventario; Grupo de producto; Subgrupo de producto; Unidad de empaque; Tarifa de producto; Detalle de tarifa; Valor de venta; Valor de venta con recargo; Vigencia de tarifa', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPriceListRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.ViewReportPriceListRate: Devuelve una fila por cada detalle de tarifa de producto, incluyendo solo productos con grupo, subgrupo, unidad de empaque y tarifa existentes (todos los JOIN son INNER).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPriceListRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; Inventory.ProductGroup; Inventory.ProductSubGroup; Inventory.PackagingUnit; Inventory.ProductRateDetail; Inventory.ProductRate', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPriceListRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPriceListRate';
GO
