


CREATE PROC SP_EDITARPRODUCTO(
@IdProducto int,
@Codigo varchar(20),
@Nombre varchar(30),
@Descripcion varchar(30),
@IdCategoria int,
/*
@Stock int,
@PrecioCompra decimal(10,2),
@PrecioVenta decimal(10,2),
*/
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin

	set @Resultado = 1


	 if not exists(select * from AP_producto where Codigo = @Codigo and IdProducto!= @IdProducto)

		update AP_producto set
		Codigo = @Codigo,
		Nombre = @Nombre,
		Descripcion = @Descripcion,
		IdCategoria = @IdCategoria,
		/*
		Stock = @Stock,
		PrecioCompra = @PrecioCompra,
		PrecioVenta = @PrecioVenta,
		*/
		Estado = @Estado
		where IdProducto = @IdProducto

	 else
	 begin
		set @Resultado = 0
		set @Mensaje = 'Ya existe el producto con el mismo codigo'
	 end
end