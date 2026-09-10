CREATE proc [dbo].[sp_AddToken]
@user char(20),
@token char(100),
@expiration datetime
as
update segusuaru set token = @token, expiration = @expiration where codusuari = @user
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra o actualiza el token de sesión y su fecha de expiración para un usuario específico del sistema de seguridad. Recibe el código de usuario, el token generado y la fecha límite de validez, y los persiste en la tabla de usuarios (SEGusuaru). Se usa para gestionar la autenticación y el control de acceso en Indigo Vie Cloud, garantizando que cada sesión activa tenga un token vigente asociado al usuario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'sp_AddToken';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'sp_AddToken';
-- GO
