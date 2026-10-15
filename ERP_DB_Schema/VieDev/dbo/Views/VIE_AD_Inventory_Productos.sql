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