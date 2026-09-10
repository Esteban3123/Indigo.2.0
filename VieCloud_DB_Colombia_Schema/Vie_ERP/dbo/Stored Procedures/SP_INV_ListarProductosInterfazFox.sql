
CREATE PROCEDURE [dbo].[SP_INV_ListarProductosInterfazFox]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT IPRCODIGO AS 'Codigo',RTRIM(IPRDESCOR) AS 'Descripción Corta', RTRIM(IPRDESLAR) AS 'Descripción Larga', CASE WHEN IPRCLAPRO='P' THEN 'Producto' WHEN IPRCLAPRO='S' THEN 'Servicio' END AS 'Clase', CASE WHEN IPRTIPPRO='0' THEN 'Ninguno' WHEN IPRTIPPRO='1' THEN 'Suministro' WHEN IPRTIPPRO='2' THEN 'Medicamento' END AS 'Tipo', RTRIM(B.IUNNOMBRE) AS 'U. de Compra', RTRIM(B1.IUNNOMBRE) AS 'U. de Consumo', RTRIM(IPRCONCEN) AS 'Concentración'
FROM dbo.INPRODUC A
LEFT OUTER JOIN dbo.INUNIDAD B ON A.IUNCODIGO=B.IUNCODIGO 
LEFT OUTER JOIN dbo.INUNIDAD B1 ON B.IUNCODUND = B1.IUNCODIGO 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los productos y servicios del inventario para la interfaz de integración con Fox. Retorna el código, descripción corta y larga, la clase del ítem (Producto o Servicio), el tipo (Ninguno, Suministro o Medicamento), la unidad de compra, la unidad de consumo y la concentración. Consulta el catálogo maestro de productos (INPRODUC) cruzando con la tabla de unidades de medida (INUNIDAD) para resolver tanto la unidad de compra como la unidad de consumo. Se usa como fuente de sincronización del catálogo de ítems farmacéuticos y de suministros hacia el sistema externo Fox.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarProductosInterfazFox';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarProductosInterfazFox';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el catálogo de productos y servicios con sus descripciones, clase, tipo, unidades de compra/consumo y concentración para alimentar una interfaz tipo FoxPro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarProductosInterfazFox';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La unidad de consumo se obtiene navegando desde la unidad de compra a través de IUNCODUND (jerarquía de unidades).; Se aplica RTRIM a descripciones y nombres de unidad para eliminar espacios sobrantes.; Los productos sin unidad asociada igualmente aparecen en el listado (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarProductosInterfazFox';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto; Servicio; Suministro; Medicamento; Unidad de compra; Unidad de consumo; Concentración', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarProductosInterfazFox';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INPRODUC: Devuelve un resultset con todos los productos del maestro, traduciendo códigos de clase y tipo a etiquetas legibles y resolviendo las unidades vinculadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarProductosInterfazFox';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPRCLAPRO = ''P'' → Se etiqueta el ítem como ''Producto'' else Si IPRCLAPRO = ''S'' se etiqueta como ''Servicio''; otros valores quedan en NULL; si IPRTIPPRO = ''0'' → Se etiqueta como ''Ninguno'' else ''1'' → ''Suministro''; ''2'' → ''Medicamento''; otros valores quedan en NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarProductosInterfazFox';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPRODUC; dbo.INUNIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarProductosInterfazFox';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarProductosInterfazFox';
-- GO
