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

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de ventas detallado por rango de fechas, incluyendo datos del documento, cliente, usuario, producto y categoría.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REPORTEVENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas de entrada deben respetar formato dmy (set dateformat dmy); Toda venta debe tener usuario, detalle, producto y categoría asociados (joins INNER)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REPORTEVENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El rango de fechas se evalúa sobre la parte date de FechaRegistro (sin hora); Solo se incluyen ventas con detalle existente y producto con categoría válida (INNER JOIN); Las fechas se interpretan como día/mes/año', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REPORTEVENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'venta; detalle de venta; producto; categoría; cliente; usuario de registro; documento de venta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REPORTEVENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas de ventas y su detalle cuando CONVERT(date, v.FechaRegistro) está entre @fechainicio y @fechafin', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REPORTEVENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AP_venta; dbo.AP_usuario; dbo.AP_detalle_venta; dbo.AP_producto; dbo.AP_categoria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REPORTEVENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REPORTEVENTAS';
-- GO
