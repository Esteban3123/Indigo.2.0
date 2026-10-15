
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