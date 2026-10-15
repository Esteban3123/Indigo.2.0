









CREATE VIEW [Authorization].[ViewListProductsSusceptibles]
AS

	select CONCAT(pro.Id, '-', pcc.CareCenterCode) Row, 
	pro.Id InventoryProductId, pro.Code InventoryProductCode, pro.Name InventoryProductName, pro.Code + ' - ' + pro.Name InventoryProductCodeName, pcc.CareCenterCode
	from [Authorization].AuthorizationPortfolioInventoryProduct pip
	inner join [Authorization].AuthorizationPortfolioCareCenter pcc on pcc.AuthorizationPortfolioId = pip.AuthorizationPortfolioId
	inner join [Authorization].ConfigurationServicesAmbulatory csa on csa.AuthorizationPortfolioInventoryProductId = pip.Id
	inner join [Authorization].AuthorizationPortfolio p on p.Id = pip.AuthorizationPortfolioId
	inner join Inventory.InventoryProduct pro on pro.Id = pip.InventoryProductId
	where p.Status = 1