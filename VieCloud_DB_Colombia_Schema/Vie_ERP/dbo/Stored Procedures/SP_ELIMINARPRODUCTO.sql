



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

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Elimina un producto del catálogo solo si no tiene relaciones con compras ni ventas; en caso contrario devuelve mensajes de error acumulados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ELIMINARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El producto debe existir en AP_producto para que el DELETE tenga efecto; El producto no debe estar referenciado en AP_detalle_compra; El producto no debe estar referenciado en AP_detalle_venta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ELIMINARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se elimina un producto que tenga detalles de compra asociados; Nunca se elimina un producto que tenga detalles de venta asociados; Los mensajes de error se acumulan permitiendo reportar múltiples motivos de bloqueo en una sola ejecución; Respuesta=1 solo se establece cuando efectivamente se ejecuta el DELETE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ELIMINARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto; Compra; Venta; Detalle de compra; Detalle de venta; Integridad referencial de catálogo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ELIMINARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] dbo.AP_producto: Si no existen registros en AP_detalle_compra ni AP_detalle_venta asociados al producto (pasoreglas=1), se elimina el producto y se retorna Respuesta=1; [RETURN_RESULT] OUTPUT: Si el producto está relacionado a una compra, se concatena mensaje ''No se puede eliminar porque el producto se encuentra relacionado a una COMPRA'' y Respuesta=0; [RETURN_RESULT] OUTPUT: Si el producto está relacionado a una venta, se concatena mensaje ''No se puede eliminar porque el producto se encuentra relacionado a una VENTA'' y Respuesta=0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ELIMINARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe registro en AP_detalle_compra para el producto → Marca pasoreglas=0, Respuesta=0 y agrega mensaje de relación con COMPRA else Continúa con la siguiente validación; si Existe registro en AP_detalle_venta para el producto → Marca pasoreglas=0, Respuesta=0 y agrega mensaje de relación con VENTA else Continúa con la siguiente validación; si pasoreglas = 1 (sin relaciones bloqueantes) → Ejecuta DELETE sobre AP_producto y asigna Respuesta=1 else No realiza eliminación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ELIMINARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AP_detalle_compra; dbo.AP_detalle_venta; dbo.AP_producto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ELIMINARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ELIMINARPRODUCTO';
-- GO
