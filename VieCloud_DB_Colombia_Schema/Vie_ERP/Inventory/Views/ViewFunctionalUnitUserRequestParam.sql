
CREATE VIEW [Inventory].[ViewFunctionalUnitUserRequestParam] AS

select 
	
	u.Id,
	fu.Code as CodeFunctionalUnit,
	u.UserCode  + ' - ' + p.Fullname  as CodeName
from [Payroll].[FunctionalUnit] AS fu
INNER JOIN Payroll.FunctionalUnitUserAuthorizationRequest AS fusar ON fusar.FunctionalUnitId = fu.Id
inner join Security.[User] as u on u.Id = fusar.UserId 
inner join Security.Person as p on p.Id = u.IdPerson
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los usuarios que tienen solicitudes de autorización sobre unidades funcionales, combinando el código de la unidad funcional con el código de usuario y el nombre completo de la persona. Integra las tablas de unidades funcionales de nómina, solicitudes de autorización, usuarios y personas para ofrecer un parámetro de búsqueda o filtro en formularios y reportes. Se usa principalmente para poblar selectores o listas desplegables donde se necesita identificar qué usuario (con su nombre) tiene acceso o solicitud pendiente sobre una unidad funcional específica (área, servicio o departamento).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewFunctionalUnitUserRequestParam';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewFunctionalUnitUserRequestParam';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los usuarios autorizados (vía solicitud) sobre unidades funcionales, mostrando el código de la unidad y una etiqueta legible que combina código de usuario y nombre completo de la persona, para uso como parámetro de selección.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewFunctionalUnitUserRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros relacionados de unidad funcional, solicitud de autorización, usuario y persona para que aparezcan filas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewFunctionalUnitUserRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen usuarios que tengan al menos una solicitud de autorización registrada sobre una unidad funcional (INNER JOIN con FunctionalUnitUserAuthorizationRequest).; Cada usuario expuesto debe tener una persona asociada (INNER JOIN con Security.Person mediante IdPerson).; El nombre mostrado se compone como ''UserCode - Fullname'', concatenando código de usuario con el nombre completo de la persona.; Si un usuario tiene múltiples solicitudes sobre distintas unidades funcionales, aparecerá una fila por combinación usuario/unidad funcional.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewFunctionalUnitUserRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad funcional; Usuario; Persona; Solicitud de autorización de acceso a unidad funcional', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewFunctionalUnitUserRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.ViewFunctionalUnitUserRequestParam: Devuelve una fila por cada solicitud de autorización de usuario sobre una unidad funcional, incluyendo el código de la unidad y la concatenación ''UserCode - Fullname''.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewFunctionalUnitUserRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.FunctionalUnit; Payroll.FunctionalUnitUserAuthorizationRequest; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewFunctionalUnitUserRequestParam';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewFunctionalUnitUserRequestParam';
GO
