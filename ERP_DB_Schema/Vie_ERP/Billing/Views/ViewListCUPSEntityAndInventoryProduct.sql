CREATE VIEW [Billing].[ViewListCUPSEntityAndInventoryProduct]
AS

select ROW_NUMBER() OVER (ORDER BY temp.Id) Row, temp.Id, temp.Code, temp.Description, temp.Identification, temp.IdentificationDescription
from (
	select ce.Id, ce.Code, ce.Description, 0 Identification, 'CUPS' IdentificationDescription
	from Contract.CUPSEntity ce
	where ce.Status = 1
	union all
	select ip.Id, ip.Code, ip.Name Description, 1 Identification, 'Medicamento' IdentificationDescription
	from Inventory.InventoryProduct ip
	where ip.Status = 1
) temp