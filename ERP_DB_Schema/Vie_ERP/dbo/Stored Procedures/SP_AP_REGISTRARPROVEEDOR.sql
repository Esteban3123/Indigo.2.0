
create proc SP_AP_REGISTRARPROVEEDOR(
@Documento varchar(50),
@RazonSocial varchar(500),
@Correo varchar(50),
@Telefono varchar(50),
@Estado int,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	set @Resultado = 0
	declare @IDPERSONA INT
	if not exists (select *from AP_proveedor where Documento = @Documento)
	begin
		insert into AP_proveedor(Documento,RazonSocial,Correo,Telefono,Estado)
		values (@Documento,@RazonSocial,@Correo,@Telefono,@Estado)

		set @Resultado = SCOPE_IDENTITY()
	end
	ELSE
		set @Mensaje = 'El numero de documento ya existe'
end