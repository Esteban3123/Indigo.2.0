
create procedure SP_REGISTRARCOMPRA(
@IdUsuario int,
@IdProveedor int,
@TipoDocumento varchar(500),
@NumeroDocumento Varchar(500),
@MontoTotal decimal(18,2),
@DetalleCompra [EAP_detalle_compra] READONLY,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	begin try
		declare @idcompra int = 0
		set @Resultado = 1
		set @Mensaje = ''

		begin transaction registro
			insert into AP_compra(IdUsuario,IdProveedor,TipoDocumento,NumeroDocumento,MontoTotal)
			values (@IdUsuario,@IdProveedor,@TipoDocumento,@NumeroDocumento,@MontoTotal)

			set @idcompra = SCOPE_IDENTITY()

			insert into AP_detalle_compra(IdCompra,IdProducto,PrecioCompra,PrecioVenta,Cantidad,MontoTotal)
			select @idcompra,IdProducto,PrecioCompra,PrecioVenta,Cantidad,MontoTotal from @DetalleCompra

			update p set p.Stock = p.Stock + dc.Cantidad,
			p.PrecioCompra = dc.PrecioCompra,
			p.PrecioVenta = dc.PrecioVenta
			from AP_producto p
			inner join @DetalleCompra dc on dc.IdProducto = p.IdProducto

		commit transaction registro

	end try
	begin catch
		
		set @Resultado = 0
		set @Mensaje = ERROR_MESSAGE()
		rollback transaction registro

	end catch
end