CREATE proc SP_REPORTEVENTAS(
@fechainicio varchar(10),
@fechafin varchar(10)
)
as
begin
	set dateformat dmy;
	select
	CONVERT(char(10),v.FechaRegistro,103)[FechaRegistro],v.TipoDocumento,v.NumeroDocumento,v.MontoTotal,
	u.NombreCompleto[UsuarioRegistro],
	v.DocumentoCliente,v.NombreCliente,
	p.Codigo[CodigoProducto],p.Nombre[NombreProducto],ca.Descripcion[Categoria],dv.PrecioVenta,dv.Cantidad,dv.SubTotal

	from AP_venta v
	inner join AP_usuario u on u.IdUsuario = v.IdUsuario
	inner join AP_detalle_venta dv on dv.IdVenta = v.IdVenta
	inner join AP_producto p on p.IdProducto = dv.IdProducto
	inner join AP_categoria ca on ca.IdCategoria = p.IdCategoria
	where CONVERT(date,v.FechaRegistro) between @fechainicio and @fechafin

end