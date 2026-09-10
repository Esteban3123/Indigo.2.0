
CREATE PROCEDURE [dbo].[SP_INV_ListarProveedores]
(
@OIDProducto varchar(20),
@VersionDGH int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF @VersionDGH=1

	SELECT IPRCODIGO AS CODIGO, RTRIM(B.GPRNOMBRE) AS NOMBRE, IPPVALPRO AS VALOR, IPPCODPRO AS 'PRODUCTO PROVEEDOR',IPRREGSAN AS 'REGISTRO SANITARIO'
	FROM DBO.INPROPRO A
	INNER JOIN DBO.GEPROVEE B ON A.GPRCODIGO= B.GPRCODIGO
	WHERE A.IPRCODIGO=@OIDProducto

ELSE

	SELECT INNPRODUC AS CODIGO, RTRIM(GPRNOMBRE) AS NOMBRE, IPPVALOR AS VALOR, IPPCODPRO AS 'PRODUCTO PROVEEDOR',IPPREGSAN AS 'REGISTRO SANITARIO'
	FROM DBO.INNPROPRO A
	INNER JOIN DBO.GENTERCERP B ON A.GENTERCERP=B.OID
	WHERE INNPRODUC=@OIDProducto

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los proveedores asociados a un producto de inventario, mostrando el código del proveedor, nombre, precio o valor del producto para ese proveedor, código interno del producto según el proveedor y registro sanitario. Recibe el identificador del producto y una versión de esquema (DGH) para determinar qué tablas consultar: la versión 1 usa las tablas antiguas de productos-proveedores (INPROPRO / GEPROVEE), mientras que las versiones posteriores usan el esquema nuevo (INNPROPRO / GENTERCERP). Se usa en la gestión de inventario y compras para conocer qué proveedores pueden suministrar un artículo y a qué precio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarProveedores';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarProveedores';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los proveedores asociados a un producto, devolviendo código, nombre, valor, producto-proveedor y registro sanitario, eligiendo el origen de datos según la versión del modelo (DGH).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarProveedores';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere el identificador del producto a consultar.; Se requiere indicar la versión del modelo DGH para decidir las tablas a consultar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarProveedores';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sin importar la versión, el resultado expone siempre el mismo contrato de columnas: CODIGO, NOMBRE, VALOR, PRODUCTO PROVEEDOR y REGISTRO SANITARIO.; El nombre del proveedor se devuelve siempre sin espacios finales (RTRIM).; Solo se retornan proveedores asociados al producto solicitado (filtro obligatorio por producto).; La relación producto-proveedor se obtiene mediante INNER JOIN, por lo que se excluyen proveedores sin maestro asociado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarProveedores';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto; Proveedor; Producto del proveedor; Registro sanitario; Tercero proveedor; Versión de modelo DGH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarProveedores';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Cuando la versión DGH es 1, devuelve proveedores desde INPROPRO unida con GEPROVEE filtrando por el producto.; [RETURN_RESULT] Resultset: Cuando la versión DGH no es 1, devuelve proveedores desde INNPROPRO unida con GENTERCERP filtrando por el producto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarProveedores';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Versión DGH = 1 → Consulta INPROPRO + GEPROVEE (modelo legado). else Consulta INNPROPRO + GENTERCERP (modelo nuevo basado en terceros con OID).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarProveedores';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.INPROPRO; DBO.GEPROVEE; DBO.INNPROPRO; DBO.GENTERCERP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarProveedores';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarProveedores';
-- GO
