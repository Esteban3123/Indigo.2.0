create proc SP_AP_ELIMINARPROVEEDOR(
@IdProveedor int,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	set @Resultado = 1
	if not exists (select *from AP_proveedor P
	INNER JOIN AP_compra c on p.IdProveedor = c.IdProveedor
	where p.IdProveedor = @IDProveedor)
	begin
		delete top(1) from AP_proveedor where IdProveedor = @IdProveedor
	end
	ELSE
	begin
		set @Resultado = 0
		set @Mensaje = 'El proveedor se encuentra relacionado a una compra'
	end

end