
CREATE  VIEW [Inventory].[ViewWarehouseUserRequestParam] AS
select 
	u.Id,
	w.Code as CodeWarehouse,
	u.UserCode  + ' - ' + p.Fullname  as CodeName
from Inventory.Warehouse  as w
inner join Inventory.WarehouseUserRequest as wur on wur.WarehouseId =  w.Id
inner join Security.[User] as u on u.Id = wur.UserId 
inner join Security.Person as p on p.Id = u.IdPerson
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de usuarios con acceso a bodegas de inventario, utilizada como parámetro de selección en formularios y reportes. Combina la información de la bodega asignada, el código de usuario y el nombre completo de la persona, cruzando las tablas de bodegas, solicitudes de acceso a bodega, usuarios y personas. Permite identificar qué usuarios tienen permiso sobre cada bodega y presentarlos en controles desplegables o filtros de búsqueda con el formato ''código de usuario - nombre completo''.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewWarehouseUserRequestParam';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewWarehouseUserRequestParam';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los usuarios autorizados para solicitar movimientos en cada bodega, mostrando el código de bodega y la identificación del usuario junto a su nombre completo.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewWarehouseUserRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una relación en WarehouseUserRequest entre el usuario y la bodega; El usuario debe tener una persona asociada (IdPerson) en Security.Person', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewWarehouseUserRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen usuarios con asignación explícita a una bodega vía WarehouseUserRequest (INNER JOIN); Solo se muestran usuarios que tengan persona asociada en Security.Person (INNER JOIN); El campo CodeName siempre tiene formato ''UserCode - Fullname''', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewWarehouseUserRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'bodega; usuario; solicitud de acceso a bodega; persona', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewWarehouseUserRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.ViewWarehouseUserRequestParam: Retorna una fila por cada vínculo usuario-bodega registrado en WarehouseUserRequest, concatenando UserCode y Fullname con separador '' - ''', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewWarehouseUserRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Warehouse; Inventory.WarehouseUserRequest; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewWarehouseUserRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewWarehouseUserRequestParam';
GO
