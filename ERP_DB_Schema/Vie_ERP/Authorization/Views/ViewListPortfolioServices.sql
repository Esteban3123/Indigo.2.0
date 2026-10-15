







CREATE VIEW [Authorization].[ViewListPortfolioServices]
AS

	select pce.Id, 
	pce.AuthorizationPortfolioId, p.Code AuthorizationCode, p.Name AuthorizationName, p.Code + ' - ' + p.Name AuthorizationCodeName,
	pce.AuthorizationGroupId, g.Code AuthorizationGroupCode, g.Name AuthorizationGroupName, g.Code + ' - ' + g.Name AuthorizationGroupCodeName,
	ce.Id CUPSEntityId, ce.Code CUPSEntityCode, ce.Description CUPSEntityName, ce.Code + ' - ' + ce.Description CUPSEntityCodeName,
	IIF(csa.Id is null, 0, 1) SelectOption,
	csa.Id ConfigurationServicesAmbulatoryId,
	cd.Id ContractDescriptionId, cd.Code + ' - ' + cd.Name ContractDescriptionCodeName
	from [Authorization].AuthorizationPortfolioCUPSEntity pce
	inner join [Authorization].AuthorizationPortfolio p on p.Id = pce.AuthorizationPortfolioId
	inner join [Authorization].AuthorizationGroup g on g.Id = pce.AuthorizationGroupId
	inner join Contract.CUPSEntity ce on ce.Id = pce.CUPSEntityId
	left join [Authorization].ConfigurationServicesAmbulatory csa on csa.AuthorizationPortfolioCUPSEntityId = pce.Id
	left join Contract.ContractDescriptions cd on cd.Id = pce.ContractDescriptionId