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