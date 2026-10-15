



CREATE PROC SP_EDITARCLIENTE(
@IdCliente int,
@Documento varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin

	set @Resultado = 1
	declare @IDPERSONA INT

	 if not exists(select * from AP_cliente where Documento = @Documento and IdCliente != @IdCliente)
	 begin

		update AP_cliente set
		Documento = @Documento,
		NombreCompleto = @NombreCompleto,
		Correo = @Correo,
		Telefono = @Telefono,
		Estado = @Estado
		where IdCliente = @IdCliente
	 end
	 else
	 begin
		set @Resultado = 0
		set @Mensaje = 'No se puede repetir el documento para mas de un usuario'
	end
end