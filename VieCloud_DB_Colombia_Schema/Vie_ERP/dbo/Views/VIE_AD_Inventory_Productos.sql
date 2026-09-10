CREATE VIEW [dbo].[VIE_AD_Inventory_Productos]
AS
SELECT pr.Id, pr.Code AS Còdigo, pr.Name AS Nombre, tpr.Name AS [Tipo producto], cuenta.Number AS Cuenta, cuenta.Name AS CuentaIngreso, catc.Code AS [Código ATC], catc.Name AS [Nombre ATC], pr.CodeCUM AS [Código CUM], 
                  pr.CodeAlternative AS [Código alterno], pr.CodeAlternativeTwo AS [Código alterno 2], pr.Description AS Descripción, gp.Name AS Grupo, sg.Code AS [Código Subgrupo], sg.Name AS [Nombre subgrupo], ue.Name AS [Unidad de empaque], 
                  f.Name AS Fabricante, pr.Presentation, pr.ExpirationDate AS [Fecha vencimiento], gf.Name AS [Grupo facturación], CASE pr.ProductControl WHEN '0' THEN 'No' WHEN '1' THEN 'Control' END AS [Producto Control], 
                  CASE pr.ProductWithPriceControl WHEN '0' THEN 'No' WHEN '1' THEN 'Si' END AS [Maneja control precios], CASE pr.POSProduct WHEN '0' THEN 'No' WHEN '1' THEN 'Si' END AS POS, 
                  CASE pr.ControlOrderQuantity WHEN '0' THEN 'No' WHEN '1' THEN 'Si' END AS [Control cantidad X orden], pr.ProductOrderAmount AS [Cantidad producto x Orden], pr.LastPurchase AS [Ultima compra], pr.LastSale AS [Ultima Venta], 
                  CASE pr.ProductOrigin WHEN '1' THEN 'Nacional' WHEN '2' THEN 'Importado' END AS [Origen producto], pr.MinimumStock AS [Stock minimo], pr.MaximumStock AS [stock máximo], pr.ProductCost AS [Costo promedio], 
                  pr.FinalProductCost AS [Ultimo costo], pr.SellingPrice AS [Precio Venta], CASE pr.AllPOSPathologies WHEN '1' THEN 'Si' WHEN '0' THEN 'No' END AS [Aplica todas patologías], 
                  CASE pr.Status WHEN '0' THEN 'Inactivo' WHEN '1' THEN 'Activo' END AS Estado, sp.Fullname AS Usuario, pr.CreationDate AS [Fecha creación], IVA.Name AS IVA, pr.HealthRegistration AS RegistroSanitario
FROM Inventory.InventoryProduct AS pr WITH (nolock) 
JOIN Inventory.ProductType AS tpr WITH (nolock) ON tpr.Id = pr.ProductTypeId 
JOIN Inventory.ProductGroup AS gp WITH (nolock) ON gp.Id = pr.ProductGroupId 
JOIN Inventory.ProductSubGroup AS sg WITH (nolock) ON sg.Id = pr.ProductSubGroupId 
JOIN GeneralLedger.MainAccounts AS cuenta WITH (nolock) ON cuenta.Id = gp.IncomeAccountId 
JOIN Inventory.PackagingUnit AS ue WITH (nolock) ON ue.Id = pr.PackagingUnitId 
JOIN Inventory.Manufacturer AS f WITH (nolock) ON f.Id = pr.ManufacturerId 
LEFT JOIN Billing.BillingGroup AS gf WITH (nolock) ON gf.Id = pr.BillingGroupId 
LEFT JOIN Security.[User] AS s ON s.UserCode = pr.CreationUser 
LEFT JOIN Security.Person AS sp ON sp.Id = s.IdPerson 
LEFT JOIN GeneralLedger.GeneralLedgerIVA AS IVA ON IVA.Id = pr.IVAId 
LEFT JOIN Inventory.ATC AS catc WITH (nolock) ON catc.Id = pr.ATCId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo completo de productos del inventario hospitalario: medicamentos, insumos y dispositivos médicos. Integra información de clasificación (tipo, grupo, subgrupo, categoría ATC), datos de identificación del producto (código interno, código CUM, código alterno, registro sanitario), condiciones comerciales (precio de venta, costo promedio, último costo, última compra, última venta), parámetros de control de stock (stock mínimo, máximo, control de cantidad por orden), y atributos de configuración como origen nacional o importado, manejo de IVA, grupo de facturación, control de precios y aplicabilidad en punto de venta (POS). Compone el resultado cruzando el catálogo maestro de productos con sus tablas de referencia de tipo, grupo, subgrupo, unidad de empaque, fabricante o laboratorio, cuenta contable de ingresos, grupo de facturación y usuario creador. Sirve como fuente principal para reportes y consultas de inventario, compras, facturación y gestión farmacéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VIE_AD_Inventory_Productos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VIE_AD_Inventory_Productos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone un listado consolidado y legible del catálogo maestro de productos de inventario, enriquecido con su clasificación, fabricante, cuenta contable de ingreso, parámetros comerciales, costos, IVA y datos de auditoría.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Inventory_Productos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada producto debe tener asociados un tipo, grupo, subgrupo, unidad de empaque y fabricante existentes; El grupo de producto debe tener una cuenta de ingreso configurada en el plan de cuentas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Inventory_Productos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen productos que tengan tipo, grupo, subgrupo, cuenta de ingreso del grupo, unidad de empaque y fabricante asignados (joins internos obligatorios); El grupo de facturación, usuario creador, persona asociada al usuario, IVA y clasificación ATC son opcionales y no excluyen al producto si están vacíos; Los códigos numéricos de banderas (ProductControl, POSProduct, ControlOrderQuantity, etc.) se traducen siempre a etiquetas legibles (''Si''/''No'', ''Activo''/''Inactivo'', ''Nacional''/''Importado''); La cuenta de ingreso reportada proviene del grupo del producto, no del producto directamente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Inventory_Productos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto de inventario; Medicamento; Código ATC; Código CUM; Registro sanitario; Fabricante / laboratorio; Unidad de empaque; Grupo y subgrupo de producto; Cuenta contable de ingreso; Grupo de facturación; IVA; Producto POS; Control de precios; Patologías POS; Stock mínimo y máximo; Costo promedio y último costo; Precio de venta; Origen del producto (Nacional/Importado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Inventory_Productos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.InventoryProduct: Devuelve un registro por cada producto que cumpla los joins internos con tipo, grupo, subgrupo, cuenta de ingreso, unidad de empaque y fabricante; los demás atributos (grupo de facturación, usuario, IVA, ATC) se devuelven en NULL si no existen', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Inventory_Productos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ProductControl = ''0'' / ''1'' → Se etiqueta como ''No'' o ''Control'' respectivamente; si ProductWithPriceControl = ''0'' / ''1'' → Se etiqueta como ''No'' o ''Si'' indicando si maneja control de precios; si POSProduct = ''0'' / ''1'' → Se etiqueta como ''No'' o ''Si'' indicando si es producto POS; si ControlOrderQuantity = ''0'' / ''1'' → Se etiqueta como ''No'' o ''Si'' indicando si controla cantidad por orden; si ProductOrigin = ''1'' / ''2'' → Se traduce a ''Nacional'' o ''Importado''; si AllPOSPathologies = ''1'' / ''0'' → Se etiqueta como ''Si'' o ''No'' indicando si aplica a todas las patologías; si Status = ''0'' / ''1'' → Se traduce a ''Inactivo'' o ''Activo''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Inventory_Productos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; Inventory.ProductType; Inventory.ProductGroup; Inventory.ProductSubGroup; GeneralLedger.MainAccounts; Inventory.PackagingUnit; Inventory.Manufacturer; Billing.BillingGroup; Security.User; Security.Person; GeneralLedger.GeneralLedgerIVA; Inventory.ATC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Inventory_Productos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Inventory_Productos';
GO
