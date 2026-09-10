create procedure SP_REGISTRARVENTA(
@IdUsuario int,
@TipoDocumento varchar(500),
@NumeroDocumento Varchar(500),
@DocumentoCliente Varchar(500),
@NombreCliente varchar(500),
@MontoPago decimal(18,2),
@MontoCambio decimal(18,2),
@MontoTotal decimal(18,2),
@DetalleVenta [EAP_detalle_venta] READONLY,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	begin try
		declare @idventa int = 0
		set @Resultado = 1
		set @Mensaje = ''

		begin transaction registro

		insert into AP_venta(IdUsuario,TipoDocumento,NumeroDocumento,DocumentoCliente,NombreCliente,MontoPago,MontoCambio,MontoTotal)
		values (@IdUsuario,@TipoDocumento,@NumeroDocumento,@DocumentoCliente,@NombreCliente,@MontoPago,@MontoCambio,@MontoTotal)

		set @idventa = SCOPE_IDENTITY()

		insert into AP_detalle_venta(IdVenta,IdProducto,PrecioVenta,Cantidad,subTotal)
		select @idventa,IdProducto,PrecioVenta,Cantidad,SubTotal from @DetalleVenta

		commit transaction registro

	end try
	begin catch
		
		set @Resultado = 0
		set @Mensaje = ERROR_MESSAGE()
		rollback transaction registro

	end catch
end

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra de forma transaccional una venta y su detalle de productos, devolviendo el resultado de la operación y un mensaje de error si falla.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARVENTA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario indicado debe existir para asociar la venta; Debe proveerse una tabla tipo EAP_detalle_venta con los productos vendidos; Los montos de pago, cambio y total deben estar calculados previamente por el cliente que invoca', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARVENTA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La cabecera de venta y su detalle se persisten atómicamente: o ambos quedan registrados o ninguno (transacción única con rollback en error); Todos los renglones del detalle quedan vinculados al mismo IdVenta generado por la inserción de la cabecera; Ante error nunca se propaga la excepción al llamador; siempre se responde mediante los parámetros de salida', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARVENTA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Venta; Detalle de venta; Cliente; Tipo y número de documento; Monto de pago; Monto de cambio; Monto total; Producto; Precio de venta; Cantidad; Subtotal; Usuario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARVENTA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.AP_venta: Siempre inserta una cabecera de venta con los datos del usuario, documento, cliente y montos dentro de la transacción ''registro''; [INSERT] dbo.AP_detalle_venta: Por cada fila de la tabla de detalle recibida se inserta un renglón asociado al IdVenta recién generado por SCOPE_IDENTITY(); [RETURN_RESULT] dbo.AP_venta: Si la transacción es exitosa retorna Resultado=1 y Mensaje vacío; si ocurre error retorna Resultado=0 y Mensaje=ERROR_MESSAGE() haciendo rollback', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARVENTA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Ocurre cualquier excepción durante los INSERT (bloque CATCH) → Se realiza rollback de la transacción ''registro'', se asigna Resultado=0 y Mensaje con el texto del error else Se confirma la transacción y se devuelve Resultado=1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARVENTA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.EAP_detalle_venta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARVENTA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARVENTA';
-- GO
