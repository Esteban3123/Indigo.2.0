

CREATE PROCEDURE [dbo].[SP_INV_ListarCodigosdeBarra]
(
@CodigoProducto varchar(20) 
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT FECHCREAC AS 'FECHA CREACION',CODBARRAS AS 'CODIGO DE BARRAS', IPRCODIGO AS 'CODIGO DE PRODUCTO' 
	FROM   IHCODBARR
	WHERE IPRCODIGO=@CodigoProducto

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los códigos de barras registrados para un producto o insumo de inventario/farmacia, dado su código interno. Consulta la tabla de códigos de barras (IHCODBARR) filtrando por el código del producto recibido como parámetro, y retorna el código de barras, el código del producto y la fecha en que se creó la asociación. Se utiliza para identificar y verificar los códigos de barras asignados a un ítem específico del inventario o farmacia, facilitando su lectura con escáneres o su trazabilidad logística.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarCodigosdeBarra';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarCodigosdeBarra';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los códigos de barras registrados para un producto específico, mostrando su fecha de creación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosdeBarra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proveerse el código del producto a consultar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosdeBarra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven códigos de barras asociados al producto especificado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosdeBarra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Código de barras; Producto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosdeBarra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.IHCODBARR: Retorna fecha de creación, código de barras y código de producto desde IHCODBARR filtrado por IPRCODIGO=@CodigoProducto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosdeBarra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHCODBARR', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosdeBarra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosdeBarra';
-- GO
