




CREATE VIEW [Contract].[ViewListContractsSearchLookUp]
AS

select c.Id, c.Code, cd.ContractName, cd.ContractNumber, c.ContractObject, c.Status, c.Code + ' - ' + cd.ContractName CodeContractName, 
ha.Id HealthAdministratorId, ha.Code + ' - ' + ha.Name HealthAdministratorCodeName, case c.Status when 1 then 'Activo' when 2 then 'Suspendido' when 3 then 'Terminado' end StatusName
from Contract.Contract c
inner join Contract.HealthAdministrator ha on ha.Id = c.HealthAdministratorId
inner join Contract.ContractDetail cd on cd.ContractId = c.Id
where cd.ValidRecord = 1