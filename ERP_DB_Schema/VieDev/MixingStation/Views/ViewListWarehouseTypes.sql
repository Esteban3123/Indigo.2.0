

CREATE view [MixingStation].[ViewListWarehouseTypes] 
as 
	SELECT c.id Id,
		   c.IdMixingStation,
		   C.WarehouseType,
		   w.Code, 
		   w.Name,
		   CONCAT(W.Code,' - ', w.Name) As CodeName,
		   w.Id as Id_Warehouse,
		   Case  C.WarehouseType
				when 1 then 'Materia Prima Stock'
				when 2 then 'Almacén'
				when 3 then 'En Proceso'
				when 4 then 'Inventario de Control'
				else 'Sin asignar'
		   end WarehouseTypeName
	FROM MixingStation.CMWarehouse C WITH(NOLOCK)
	INNER JOIN Inventory.Warehouse w WITH(NOLOCK) ON c.IdWarehouse = w.id
	INNER JOIN MixingStation.CMConfiguration s WITH(NOLOCK) ON s.id = c.IdMixingStation
	WHERE C.StateWH = 1