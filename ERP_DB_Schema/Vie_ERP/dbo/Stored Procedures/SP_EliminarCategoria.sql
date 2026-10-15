



create proc SP_EliminarCategoria(
@IdCategoria int,
@Resultado int output,
@Mensaje varchar(500) output
)
as
begin
	set @Resultado = 1
	if not exists (
	select *from AP_categoria c 
	inner join AP_producto p on p.IdCategoria = c.IdCategoria
	where c.IdCategoria = @IdCategoria
	)
	begin
		delete top(1) from AP_categoria where IdCategoria = @IdCategoria
	end
	ELSE
	begin
		set @Resultado = 0
		set @Mensaje = 'La categoria se encuentra relacionada a un producto'
	end
end