

CREATE VIEW [Contract].[ViewListCupsEntityWithDescriptions]
AS

select cecd.Id CUPSEntityContractDescriptionId, cd.Id ContractDescriptionId, ce.Id CUPSEntityId,
ce.Code CUPSEntityCode, ce.Description CUPSEntityName, ce.Code + ' - ' + ce.Description CUPSEntityCodeName,
cd.Code ContractDescriptionCode, cd.Name ContractDescriptionName, cd.Code + ' - ' + cd.Name ContractDescriptionCodeName
from Contract.CUPSEntity ce with(nolock)
inner join Contract.CUPSEntityContractDescriptions cecd with(nolock) on cecd.CUPSEntityId = ce.Id
inner join Contract.ContractDescriptions cd with(nolock) on cd.Id = cecd.ContractDescriptionId
where cecd.IsDelete = 0