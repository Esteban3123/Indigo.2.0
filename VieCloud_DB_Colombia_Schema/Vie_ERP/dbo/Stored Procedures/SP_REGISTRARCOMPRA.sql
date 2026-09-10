
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

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra de forma transaccional una compra a proveedor con su detalle y actualiza el stock y precios de los productos involucrados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARCOMPRA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El tipo tabla EAP_detalle_compra debe contener filas con IdProducto, PrecioCompra, PrecioVenta, Cantidad y MontoTotal; Los IdProducto del detalle deben existir en AP_producto para que el UPDATE tenga efecto; IdUsuario e IdProveedor deben ser válidos según las FKs de AP_compra', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARCOMPRA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cabecera, detalle y actualización de stock/precios se confirman atómicamente: o se persisten todos o ninguno; El IdCompra usado en el detalle siempre proviene del SCOPE_IDENTITY() de la cabecera recién insertada; El stock de un producto nunca disminuye en este flujo: solo se suma la cantidad comprada; Los precios de compra y venta del producto quedan alineados con los del último detalle de compra registrado para ese producto; Ante error, los parámetros de salida siempre quedan informados (Resultado=0, Mensaje con el error)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARCOMPRA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'compra; proveedor; detalle de compra; producto; stock; precio de compra; precio de venta; documento de compra', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARCOMPRA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.AP_compra: Siempre inserta una cabecera de compra con los datos de usuario, proveedor, tipo y número de documento y monto total dentro de la transacción ''registro''; [INSERT] dbo.AP_detalle_compra: Por cada fila del detalle recibido se inserta un renglón asociado al IdCompra recién generado vía SCOPE_IDENTITY(); [UPDATE] dbo.AP_producto: Para cada producto presente en el detalle, incrementa Stock en la Cantidad comprada y sobreescribe PrecioCompra y PrecioVenta con los valores del detalle; [RETURN_RESULT] output: Si todo finaliza, retorna @Resultado=1 y @Mensaje vacío; ante excepción, retorna @Resultado=0 y @Mensaje=ERROR_MESSAGE() tras rollback', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARCOMPRA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Ocurre cualquier error dentro del TRY (en los INSERT/UPDATE de la transacción) → Hace ROLLBACK de la transacción ''registro'' y devuelve Resultado=0 con el mensaje del error else Hace COMMIT de la transacción y devuelve Resultado=1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARCOMPRA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AP_producto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARCOMPRA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARCOMPRA';
-- GO
