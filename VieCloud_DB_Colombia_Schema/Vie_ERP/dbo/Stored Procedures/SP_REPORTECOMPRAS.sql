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

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte detallado de compras realizadas en un rango de fechas, opcionalmente filtrado por proveedor, incluyendo datos del documento, usuario, proveedor y productos adquiridos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REPORTECOMPRAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas de inicio y fin deben venir en formato dd/mm/yyyy (set dateformat dmy); Las compras deben tener usuario, proveedor, detalle, producto y categoría existentes (joins INNER); Para filtrar por un proveedor específico se debe enviar un IdProveedor distinto de 0; 0 indica ''todos los proveedores''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REPORTECOMPRAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen compras que tengan al menos un detalle con producto y categoría asociados (por uso de INNER JOIN); El rango de fechas se evalúa sobre la parte fecha de FechaRegistro, ignorando la hora; El monto SubTotal por línea se toma del detalle de compra, no se recalcula', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REPORTECOMPRAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Compra; Detalle de compra; Proveedor; Producto; Categoría de producto; Usuario que registra; Tipo y número de documento; Precio de compra; Precio de venta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REPORTECOMPRAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] AP_compra: Devuelve filas de compras cuyo FechaRegistro (convertido a date) esté entre @fechainicio y @fechafin, junto con su detalle, producto, categoría, proveedor y usuario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REPORTECOMPRAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @idproveedor = 0 → No se filtra por proveedor (se compara IdProveedor consigo mismo, devolviendo todos) else Se filtran únicamente las compras del proveedor indicado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REPORTECOMPRAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AP_compra; dbo.AP_usuario; dbo.AP_proveedor; dbo.AP_detalle_compra; dbo.AP_producto; dbo.AP_categoria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REPORTECOMPRAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REPORTECOMPRAS';
-- GO
