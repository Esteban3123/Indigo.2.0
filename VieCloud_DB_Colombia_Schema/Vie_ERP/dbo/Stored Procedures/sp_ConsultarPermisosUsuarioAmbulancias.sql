CREATE proc [dbo].[sp_ConsultarPermisosUsuarioAmbulancias]
@userCode char(20)
as
select * from SEGpermiu where codusuari = '999' and indidmenu = '255'
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los permisos de acceso al módulo de Ambulancias para un usuario del sistema. Obtiene desde la tabla de permisos (SEGpermiu) la configuración de acciones permitidas (guardar, consultar, actualizar, eliminar, imprimir, anular, entre otras) asociadas al menú con identificador 255, que corresponde al módulo de Ambulancias. Aunque recibe un código de usuario como parámetro, actualmente consulta de forma fija el usuario ''999'', por lo que devuelve los permisos configurados para ese perfil genérico. Se utiliza para controlar la seguridad y visibilidad del módulo de gestión de ambulancias dentro del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'sp_ConsultarPermisosUsuarioAmbulancias';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'sp_ConsultarPermisosUsuarioAmbulancias';
-- GO
