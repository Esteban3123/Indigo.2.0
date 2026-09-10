


CREATE PROC SP_EDITARPRODUCTO(
@IdProducto int,
@Codigo varchar(20),
@Nombre varchar(30),
@Descripcion varchar(30),
@IdCategoria int,
/*
@Stock int,
@PrecioCompra decimal(10,2),
@PrecioVenta decimal(10,2),
*/
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin

	set @Resultado = 1


	 if not exists(select * from AP_producto where Codigo = @Codigo and IdProducto!= @IdProducto)

		update AP_producto set
		Codigo = @Codigo,
		Nombre = @Nombre,
		Descripcion = @Descripcion,
		IdCategoria = @IdCategoria,
		/*
		Stock = @Stock,
		PrecioCompra = @PrecioCompra,
		PrecioVenta = @PrecioVenta,
		*/
		Estado = @Estado
		where IdProducto = @IdProducto

	 else
	 begin
		set @Resultado = 0
		set @Mensaje = 'Ya existe el producto con el mismo codigo'
	 end
end

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Actualiza los datos descriptivos y el estado de un producto existente, validando que su código no colisione con el de otro producto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el producto identificado para que el UPDATE tenga efecto; El código nuevo no debe estar usado por otro producto distinto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El código del producto debe ser único entre productos distintos; Resultado inicia en 1 y solo se degrada a 0 si hay conflicto de código; No se modifican Stock, PrecioCompra ni PrecioVenta (campos comentados)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto; Categoría; Código de producto; Estado del producto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.AP_producto: Cuando no existe otro producto con el mismo Codigo y distinto IdProducto, se actualizan Codigo, Nombre, Descripcion, IdCategoria y Estado del producto indicado; [RETURN_RESULT] dbo.AP_producto: Cuando ya existe otro producto con el mismo Codigo, se retorna Resultado=0 y Mensaje ''Ya existe el producto con el mismo codigo'' sin modificar datos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe otro producto con el mismo Codigo y distinto IdProducto → Actualiza el producto y mantiene Resultado=1 else Resultado=0 y Mensaje ''Ya existe el producto con el mismo codigo''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AP_producto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARPRODUCTO';
-- GO
