
CREATE  VIEW [Inventory].[ViewWarehouseUserRequestParam] AS
select 
	u.Id,
	w.Code as CodeWarehouse,
	u.UserCode  + ' - ' + p.Fullname  as CodeName
from Inventory.Warehouse  as w
inner join Inventory.WarehouseUserRequest as wur on wur.WarehouseId =  w.Id
inner join Security.[User] as u on u.Id = wur.UserId 
inner join Security.Person as p on p.Id = u.IdPerson