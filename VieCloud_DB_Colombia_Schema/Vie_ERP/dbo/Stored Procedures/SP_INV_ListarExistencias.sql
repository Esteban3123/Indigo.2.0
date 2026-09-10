
CREATE PROCEDURE [dbo].[SP_INV_ListarExistencias]
(
@OIDProducto varchar(20),
@VersionDGH int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
IF @VersionDGH=1
		SELECT B.IALCODIGO AS 'CODIGO ALMACEN', B.IALNOMBRE AS 'NOMBRE DEL ALMACEN', IFINUMLOT AS LOTE,IFIFECVEN AS 'FECHA DE VENCIMIENTO', IFICANTID AS 'CANTIDAD DISPONIBLE', IFICANCOM AS 'CANTIDAD COMPROMETIDA',IFICANTID + IFICANCOM AS EXISTENCIA
		FROM DBO.INFISICO A
		INNER JOIN DBO.INALMACE B ON A.IALCODIGO = B.IALCODIGO
		WHERE A.IPRCODIGO=@OIDProducto
ELSE
		SELECT B.IALCODIGO AS 'CODIGO ALMACEN', B.IALNOMBRE AS 'NOMBRE DEL ALMACEN', C.ILSCODIGO AS LOTE,C.ILSFECVEN AS 'FECHA DE VENCIMIENTO', IFICANTID AS 'CANTIDAD DISPONIBLE', IFICANCOMP AS 'CANTIDAD COMPROMETIDA',IFICANTID + IFICANCOMP AS EXISTENCIA
		FROM DBO.INNFISICO A
		INNER JOIN DBO.INNALMACE B ON A.INNALMACE = B.OID
		INNER JOIN DBO.INNLOTSER C ON A.INNLOTSER = C.OID
		WHERE A.INNPRODUC=@OIDProducto
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las existencias físicas de un producto de inventario en todos los almacenes donde se encuentra disponible, mostrando por cada almacén el lote, la fecha de vencimiento, la cantidad disponible y la cantidad comprometida (reservada). Soporta dos versiones del modelo de datos de inventario (DGH versión 1 y versión nueva), seleccionando las tablas correspondientes según el parámetro de versión. Se usa para conocer el stock actual de un insumo, medicamento o material por bodega o almacén, útil en gestión de farmacia, suministros y control de inventario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarExistencias';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarExistencias';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las existencias (cantidad disponible, comprometida y total) por almacén y lote/vencimiento de un producto, eligiendo el modelo de datos según la versión del esquema DGH.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarExistencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse el identificador del producto a consultar.; Debe indicarse la versión del esquema DGH (1 = modelo legado; otro valor = modelo nuevo).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarExistencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La existencia total siempre se calcula como cantidad disponible + cantidad comprometida.; El resultado siempre se restringe a un único producto.; Cada fila representa la existencia de un producto en un almacén y lote específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarExistencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Inventario; Almacén; Lote; Fecha de vencimiento; Cantidad disponible; Cantidad comprometida; Existencia; Producto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarExistencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] DBO.INFISICO: Cuando la versión DGH = 1, retorna existencias desde INFISICO unido a INALMACE filtrando por el código de producto.; [RETURN_RESULT] DBO.INNFISICO: Cuando la versión DGH ≠ 1, retorna existencias desde INNFISICO unido a INNALMACE e INNLOTSER filtrando por el OID del producto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarExistencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Versión DGH = 1 → Consulta el modelo legado (INFISICO/INALMACE) usando códigos de almacén y datos de lote embebidos en la tabla física. else Consulta el modelo nuevo (INNFISICO/INNALMACE/INNLOTSER) usando OIDs y normalizando el lote/vencimiento en INNLOTSER.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarExistencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.INFISICO; DBO.INALMACE; DBO.INNFISICO; DBO.INNALMACE; DBO.INNLOTSER', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarExistencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarExistencias';
-- GO
