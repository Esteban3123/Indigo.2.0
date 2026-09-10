-- Guardar nuevo usuario
CREATE PROC sp_AP_Guardar
@Documento VARCHAR(50),
@NombreCompleto VARCHAR(100),
@Correo VARCHAR(50),
@Clave VARCHAR(50)
AS
BEGIN
	INSERT INTO AP_usuario (Documento, NombreCompleto, Correo, Clave, IdRol, Estado)
	VALUES (@Documento, @NombreCompleto, @Correo, @Clave, 2, 0)
END

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registrar un nuevo usuario en el sistema asignándole un rol fijo y un estado inicial predefinido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_AP_Guardar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se deben proveer documento, nombre completo, correo y clave del nuevo usuario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_AP_Guardar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todo usuario nuevo se crea siempre con IdRol=2; Todo usuario nuevo se crea con Estado=0 (inactivo/pendiente)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_AP_Guardar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'usuario; rol; estado de usuario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_AP_Guardar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.AP_usuario: Siempre inserta una fila nueva con IdRol=2 y Estado=0, usando los datos de identificación, contacto y credencial recibidos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_AP_Guardar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_AP_Guardar';
-- GO
