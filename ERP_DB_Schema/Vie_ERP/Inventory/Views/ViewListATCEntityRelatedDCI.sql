CREATE VIEW [Inventory].[ViewListATCEntityRelatedDCI]
AS

select ROW_NUMBER() OVER (ORDER BY dci.Id) Id, atc.Id ATCId, atc.Code, atc.Name, atc.Code + ' - ' + atc.Name CodeName, dci.Id DCIId
from Inventory.DCI dci
inner join Inventory.DCIATCEntity dciatc on dciatc.IdDCI = dci.Id
inner join Inventory.ATCEntity atc on atc.Id = dciatc.IdATCEntity
where dci.Status = 1 and atc.State = 1