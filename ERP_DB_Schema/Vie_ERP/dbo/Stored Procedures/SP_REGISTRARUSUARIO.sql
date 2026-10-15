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