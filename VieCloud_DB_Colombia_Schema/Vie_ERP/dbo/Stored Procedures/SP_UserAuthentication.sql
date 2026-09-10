CREATE procedure [dbo].[SP_UserAuthentication]
@codeUser char(60),
@passUser char(50)
as
begin
select NOMUSUARI, PASSUSUAR from segusuaru where CODUSUARI = @codeUser and PASSUSUAR = @passUser
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de autenticación de usuarios del sistema Indigo Vie Cloud. Recibe el código de usuario y su contraseña como parámetros, y consulta la tabla de seguridad de usuarios (SEGusuaru) para verificar si las credenciales ingresadas son válidas. Devuelve el nombre del usuario y la contraseña asociada cuando existe una coincidencia, permitiendo así validar el acceso al sistema. Se utiliza en el proceso de inicio de sesión para controlar quién puede operar en la plataforma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_UserAuthentication';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_UserAuthentication';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Verifica las credenciales de un usuario devolviendo su nombre y contraseña cuando coinciden el código y la clave suministrados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_UserAuthentication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la tabla de usuarios con las columnas de código y contraseña; Se requieren código de usuario y contraseña como parámetros de entrada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_UserAuthentication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna registro si coinciden exactamente código y contraseña del usuario; La contraseña se compara en texto plano contra la columna almacenada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_UserAuthentication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Usuario; Autenticación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_UserAuthentication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.segusuaru: Cuando el código de usuario y la contraseña coinciden con un registro en segusuaru, se devuelve el nombre y la contraseña del usuario; en caso contrario el resultado es vacío', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_UserAuthentication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.segusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_UserAuthentication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_UserAuthentication';
-- GO
