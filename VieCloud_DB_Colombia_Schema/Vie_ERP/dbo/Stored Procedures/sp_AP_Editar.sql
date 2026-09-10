create PROC sp_AP_Editar
@IdUsuario INT,
@Documento VARCHAR(50),
@NombreCompleto VARCHAR(100),
@Correo VARCHAR(50),
@Clave VARCHAR(50)
AS
BEGIN
	UPDATE AP_usuario
	SET
		Documento = @Documento,
		NombreCompleto = @NombreCompleto,
		Correo = @Correo,
		Clave = @Clave
	WHERE IdUsuario = @IdUsuario
END

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Actualiza los datos de identificación, contacto y credenciales de un usuario existente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_AP_Editar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un usuario con el identificador proporcionado para que la actualización tenga efecto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_AP_Editar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La actualización se restringe a un único usuario identificado por su clave primaria.; La clave del usuario se almacena en texto plano tal cual se recibe (sin hash ni transformación).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_AP_Editar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Usuario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_AP_Editar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.AP_usuario: Cuando el IdUsuario coincide, se sobrescriben Documento, NombreCompleto, Correo y Clave con los valores recibidos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_AP_Editar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_AP_Editar';
-- GO
