CREATE PROC SP_REGISTRARUSUARIO(
@Documento varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(100),
@Clave varchar(100),
@IdRol int,
@Estado bit,
@IdUsuarioResultado int output,
@Mensaje varchar(500) output
)
as
begin

	set @IdUsuarioResultado = 0
	set @Mensaje = ''

	 if not exists(select *from AP_usuario where Documento = @Documento)
	 begin

		insert into AP_usuario (Documento,NombreCompleto,Correo,Clave,IdRol,Estado)
		values (@Documento,@NombreCompleto,@Correo,@Clave,@IdRol,@Estado)

		set @IdUsuarioResultado = SCOPE_IDENTITY()
		set @Mensaje = ''

	 end
	 else
		set @Mensaje = 'No se puede repetir el documento para mas de un usuario'

end

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra un nuevo usuario en el sistema validando que el documento de identidad no esté duplicado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El documento no debe existir previamente en AP_usuario para permitir el alta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El documento de usuario es único: nunca se inserta un usuario con un Documento ya existente; Si la inserción no ocurre, el ID resultante permanece en 0; Si la inserción es exitosa, el mensaje de salida queda vacío', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'usuario; documento de identidad; rol; credenciales (clave); estado de usuario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.AP_usuario: Cuando no existe un registro con el mismo Documento, se inserta el nuevo usuario y se devuelve su ID generado vía SCOPE_IDENTITY(); [RETURN_RESULT] @Mensaje: Cuando ya existe un usuario con el mismo Documento, se retorna el mensaje ''No se puede repetir el documento para mas de un usuario'' y el ID resultante queda en 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe registro en AP_usuario con el mismo Documento → Inserta el nuevo usuario y asigna el ID generado al output else No inserta y devuelve mensaje de documento duplicado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AP_usuario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARUSUARIO';
-- GO
