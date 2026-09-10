CREATE PROCEDURE [dbo].[SP_AuthenticationUser]
@NameUser char(60),
@PassUser char(50) AS
SELECT * FROM SEGusuaru WHERE NOMUSUARI = @NameUser AND PASSUSUAR = @PassUser
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de autenticación de usuarios del sistema Indigo Vie Cloud. Recibe el nombre de usuario y la contraseña, los valida contra el registro de usuarios de seguridad (SEGusuaru) y retorna los datos del usuario si las credenciales coinciden. Se utiliza para verificar el acceso al sistema, comprobando que el nombre de usuario y la contraseña ingresados correspondan a un usuario registrado y habilitado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AuthenticationUser';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AuthenticationUser';
-- GO
