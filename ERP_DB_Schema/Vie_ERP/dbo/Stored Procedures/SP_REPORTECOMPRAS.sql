CREATE proc SP_REPORTECOMPRAS(
@fechainicio varchar(10),
@fechafin varchar(10),
@idproveedor int
)
as
begin
	set dateformat dmy;
	select
	CONVERT(char(10),c.FechaRegistro,103)[FechaRegistro],c.TipoDocumento,c.NumeroDocumento,c.MontoTotal,
	u.NombreCompleto[UsuarioRegistro],
	pr.Documento[DocumentoProveedor],pr.RazonSocial,
	p.Codigo[CodigoProducto],p.Nombre[NombreProducto],ca.Descripcion[Categoria],dc.PrecioCompra,dc.PrecioVenta,dc.Cantidad,dc.MontoTotal[SubTotal]

	from AP_compra c
	inner join AP_usuario u on u.IdUsuario = c.IdUsuario
	inner join AP_proveedor pr on pr.IdProveedor = c.IdProveedor
	inner join AP_detalle_compra dc on dc.IdCompra = c.IdCompra
	inner join AP_producto p on p.IdProducto = dc.IdProducto
	inner join AP_categoria ca on ca.IdCategoria = p.IdCategoria
	where CONVERT(date,c.FechaRegistro) between @fechainicio and @fechafin
	and pr.IdProveedor = iif(@idproveedor=0,pr.IdProveedor,@idproveedor)
end