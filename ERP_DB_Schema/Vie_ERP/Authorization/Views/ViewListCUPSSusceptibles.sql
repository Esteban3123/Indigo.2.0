CREATE VIEW [Authorization].[ViewListCUPSSusceptibles]
AS

	select CONCAT(pcc.Id, csa.Id, ISNULL(csae.Id, 0)) Id, 
	ce.Id CUPSEntityId, ce.Code CUPSEntityCode, ce.Description CUPSEntityName, ce.Code + ' - ' + ce.Description CUPSEntityCodeName, 
	pcc.CareCenterCode,
	ISNULL(csae.CareGroupId, 0) CareGroupId, ISNULL(csae.SusceptibleAuthorization, 1) SusceptibleAuthorization, 
	pce.ContractDescriptionId, cd.Code + ' - ' + cd.Name ContractDescriptionCodeName
	from [Authorization].AuthorizationPortfolioCUPSEntity pce
	inner join [Authorization].AuthorizationPortfolioCareCenter pcc on pcc.AuthorizationPortfolioId = pce.AuthorizationPortfolioId
	inner join [Authorization].ConfigurationServicesAmbulatory csa on csa.AuthorizationPortfolioCUPSEntityId = pce.Id
	inner join [Authorization].AuthorizationPortfolio p on p.Id = pce.AuthorizationPortfolioId
	inner join Contract.CUPSEntity ce on ce.Id = pce.CUPSEntityId
	left join [Authorization].ConfigurationServicesAmbulatoryExceptions csae on csae.ConfigurationServicesAmbulatoryId = csa.Id
	left join Contract.ContractDescriptions cd on cd.Id = pce.ContractDescriptionId
	where p.Status = 1