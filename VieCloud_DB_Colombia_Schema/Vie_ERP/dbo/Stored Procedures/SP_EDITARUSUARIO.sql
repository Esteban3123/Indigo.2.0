
CREATE PROC SP_EDITARUSUARIO(
@IdUsuario int,
@Documento varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(100),
@Clave varchar(100),
@IdRol int,
@Estado bit,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin

	set @Respuesta = 0
	set @Mensaje = ''

	 if not exists(select * from AP_usuario where Documento = @Documento and idusuario != @IdUsuario)
	 begin

		update AP_usuario set
		Documento = @Documento,
		NombreCompleto = @NombreCompleto,
		Correo = @Correo,
		Clave = @Clave,
		IdRol = @IdRol,
		Estado = @Estado
		where IdUsuario = @IdUsuario

		set @Respuesta = 1


	 end
	 else
		set @Mensaje = 'No se puede repetir el documento para mas de un usuario'

end

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Actualiza los datos de un usuario existente garantizando la unicidad del documento de identificación entre usuarios.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'No debe existir otro usuario distinto con el mismo documento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El documento de identificación es único por usuario; Respuesta inicia en 0 y solo cambia a 1 cuando el UPDATE se ejecuta; No se modifica el usuario si la validación de unicidad falla', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Usuario; Rol; Documento de identificación; Credenciales de acceso; Estado de usuario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.AP_usuario: Si no existe otro usuario con el mismo Documento (distinto IdUsuario), se actualizan Documento, NombreCompleto, Correo, Clave, IdRol y Estado del usuario indicado; [RETURN_RESULT] OUTPUT: Si la actualización procede, devuelve Respuesta=1; si el documento ya existe en otro usuario, devuelve Respuesta=0 y Mensaje=''No se puede repetir el documento para mas de un usuario''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe otro usuario con el mismo Documento y distinto IdUsuario → Ejecuta UPDATE sobre AP_usuario y marca Respuesta=1 else No actualiza y retorna mensaje de documento duplicado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AP_usuario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARUSUARIO';
-- GO
