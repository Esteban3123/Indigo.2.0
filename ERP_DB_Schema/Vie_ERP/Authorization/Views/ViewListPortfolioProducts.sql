






CREATE VIEW [Authorization].[ViewListPortfolioProducts]
AS

	select pip.Id, 
	pip.AuthorizationPortfolioId, p.Code AuthorizationCode, p.Name AuthorizationName, p.Code + ' - ' + p.Name AuthorizationCodeName,
	pip.AuthorizationGroupId, g.Code AuthorizationGroupCode, g.Name AuthorizationGroupName, g.Code + ' - ' + g.Name AuthorizationGroupCodeName,
	ip.Id InventoryProductId, ip.Code InventoryProductCode, ip.Name InventoryProductName, ip.Code + ' - ' + ip.Name InventoryProductCodeName,
	IIF(csa.Id is null, 0, 1) SelectOption,
	csa.Id ConfigurationServicesAmbulatoryId
	from [Authorization].AuthorizationPortfolioInventoryProduct pip
	inner join [Authorization].AuthorizationPortfolio p on p.Id = pip.AuthorizationPortfolioId
	inner join [Authorization].AuthorizationGroup g on g.Id = pip.AuthorizationGroupId
	inner join Inventory.InventoryProduct ip on ip.Id = pip.InventoryProductId
	left join [Authorization].ConfigurationServicesAmbulatory csa on csa.AuthorizationPortfolioInventoryProductId = pip.Id