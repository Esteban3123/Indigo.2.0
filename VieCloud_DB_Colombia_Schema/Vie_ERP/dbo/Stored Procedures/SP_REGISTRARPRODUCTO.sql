
CREATE PROC SP_REGISTRARPRODUCTO(
@Codigo varchar(20),
@Nombre varchar(30),
@Descripcion varchar(30),
@IdCategoria int,
@Estado bit,
/*
@Stock int,
@PrecioCompra decimal(10,2),
@PrecioVenta decimal(10,2),
*/
@Resultado int output,
@Mensaje varchar(500) output
)
as
begin

	set @Resultado = 0
	 if not exists(select * from AP_producto where Codigo = @Codigo)
	 begin

		insert into AP_producto(Codigo,Nombre,Descripcion,IdCategoria,/*Stock,PrecioCompra,PrecioVenta,*/Estado)
		values (@Codigo,@Nombre,@Descripcion,@IdCategoria,/*@Stock,@PrecioCompra,@PrecioVenta,*/@Estado)

		set @Resultado = SCOPE_IDENTITY()


	 end
	 else
		set @Mensaje = 'Ya existe el producto con el mismo codigo'

end

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra un nuevo producto en el catálogo siempre que su código no esté ya registrado, devolviendo el identificador generado o un mensaje de duplicado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La categoría referenciada debe existir para respetar la integridad referencial con AP_producto.IdCategoria; El código del producto no debe existir previamente en AP_producto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El código del producto debe ser único: nunca se inserta un producto si ya existe otro con el mismo Codigo; El identificador resultante es 0 cuando no se realiza la inserción', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto; Categoría; Código de producto; Estado del producto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.AP_producto: Cuando NOT EXISTS un producto con el mismo Codigo, se inserta el nuevo producto y se asigna SCOPE_IDENTITY() al resultado; [RETURN_RESULT] dbo.AP_producto: Cuando ya existe un producto con el mismo Codigo, se retorna el mensaje ''Ya existe el producto con el mismo codigo'' y @Resultado permanece en 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe registro en AP_producto con el mismo Codigo → Inserta el producto y devuelve el ID generado en @Resultado else No inserta y retorna mensaje ''Ya existe el producto con el mismo codigo''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AP_producto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARPRODUCTO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARPRODUCTO';
-- GO
