create proc SP_AP_EDITARPROVEEDOR(
@IdProveedor int,
@Documento varchar(50),
@RazonSocial varchar(500),
@Correo varchar(50),
@Telefono varchar(50),
@Estado int,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	set @Resultado = 1
	declare @IDPERSONA INT
	if not exists (select *from AP_proveedor where Documento = @Documento and IdProveedor != @IDProveedor)
	begin
		update AP_proveedor set
		Documento = @Documento,
		RazonSocial = @RazonSocial,
		Correo = @Correo,
		Telefono = @Telefono,
		Estado = @Estado
		where IdProveedor = @IdProveedor
	end
	ELSE
	begin
		set @Resultado = 0
		set @Mensaje = 'El numero de documento ya existe'
	end

end