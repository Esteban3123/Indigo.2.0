
CREATE PROCEDURE [dbo].[SP_INV_ListarFabricantes]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT OID,IFRCODIGO AS 'CODIGO FABRICANTE',IFRNOMBRE AS 'NOMBRE FABRICANTE', RTRIM(IFRCODIGO)+' - '+RTRIM(IFRNOMBRE) AS 'FABRICANTE'
FROM DBO.INNFABRIC

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los fabricantes de insumos o medicamentos registrados en el sistema de inventario. Retorna el código del fabricante, su nombre y una descripción combinada código-nombre para uso en listas desplegables o filtros. Consulta directamente la tabla maestra de fabricantes (INNFABRIC) y se usa para poblar catálogos en módulos de inventario, compras o gestión de insumos médicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarFabricantes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarFabricantes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el catálogo de fabricantes disponibles, exponiendo código, nombre y una etiqueta concatenada para presentación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarFabricantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No filtra por estado: retorna todos los fabricantes existentes en la tabla.; La etiqueta ''FABRICANTE'' siempre se construye como RTRIM(código) + '' - '' + RTRIM(nombre).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarFabricantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Fabricante; Catálogo de inventario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarFabricantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] DBO.INNFABRIC: Devuelve todas las filas del catálogo de fabricantes con OID, código, nombre y la concatenación ''código - nombre'' como etiqueta.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarFabricantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.INNFABRIC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarFabricantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarFabricantes';
-- GO
