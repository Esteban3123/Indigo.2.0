
CREATE PROC SP_REGISTRARPRODUCTO(
@Codigo varchar(20),
@Nombre varchar(30),
@Descripcion varchar(30),
@IdCategoria int,
@Estado bit,
/*
@Stock int,
@PrecioCompra decimal(10,2),
@PrecioVenta decimal(10,2),
*/
@Resultado int output,
@Mensaje varchar(500) output
)
as
begin

	set @Resultado = 0
	 if not exists(select * from AP_producto where Codigo = @Codigo)
	 begin

		insert into AP_producto(Codigo,Nombre,Descripcion,IdCategoria,/*Stock,PrecioCompra,PrecioVenta,*/Estado)
		values (@Codigo,@Nombre,@Descripcion,@IdCategoria,/*@Stock,@PrecioCompra,@PrecioVenta,*/@Estado)

		set @Resultado = SCOPE_IDENTITY()


	 end
	 else
		set @Mensaje = 'Ya existe el producto con el mismo codigo'

end