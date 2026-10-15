



CREATE PROC SP_ELIMINARPRODUCTO(
@IdProducto int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin

	set @Respuesta = 0
	set @Mensaje = ''
	declare @pasoreglas bit = 1

	IF EXISTS (SELECT * FROM AP_detalle_compra dc
	INNER JOIN AP_producto p on p.IdProducto = dc.IdProducto
	WHERE p.IdProducto = @IdProducto
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque el producto se encuentra relacionado a una COMPRA\n'
	END

	IF EXISTS (SELECT * FROM AP_detalle_venta dv
	INNER JOIN AP_producto p on p.IdProducto = dv.IdProducto
	WHERE p.IdProducto = @IdProducto
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque el producto se encuentra relacionado a una VENTA\n'
	END


	if (@pasoreglas = 1)
	begin
		delete from AP_producto where IdProducto = @IdProducto
		set @Respuesta = 1
	end

end