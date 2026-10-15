
CREATE proc SP_REGISTRARCLIENTE(
@Documento varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)
as
begin

	set @Resultado = 0
	declare @IDPERSONA INT
	IF NOT EXISTS (SELECT * FROM AP_cliente where Documento = @Documento)
	 begin

		insert into AP_cliente(Documento,NombreCompleto,Correo,Telefono,Estado)
		values (@Documento,@NombreCompleto,@Correo,@Telefono,@Estado)

		set @Resultado = SCOPE_IDENTITY()

	 end
	 else
		set @Mensaje = 'El numero de documento ya existe'

end