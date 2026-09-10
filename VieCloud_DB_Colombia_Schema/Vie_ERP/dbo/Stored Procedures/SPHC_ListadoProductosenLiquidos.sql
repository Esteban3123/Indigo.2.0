

CREATE PROCEDURE [dbo].[SPHC_ListadoProductosenLiquidos]
AS
BEGIN
	SET NOCOUNT ON;

SELECT RTRIM(CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,NOPOSPROD AS 'NO POS',COALESCE(NULLIF(CODDCIMED, CAST('' AS CHAR)),CAST('' AS CHAR)) AS CODDCIMED
FROM dbo.IHLISTPRO
WHERE ESPDILPRO=1 AND TIPPRODUC IN ('1','3') AND PROESTADO = 1
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos y productos farmacéuticos que se pueden usar como soluciones líquidas o diluyentes, filtrando del catálogo maestro de productos (IHLISTPRO) solo aquellos activos, de tipo medicamento o insumo (tipos 1 y 3), y marcados como aptos para dilución. Devuelve el código del producto, el nombre del medicamento, si pertenece al plan de beneficios POS, y el código del medicamento institucional (CODDCIMED). Se usa para apoyar la preparación de mezclas y líquidos endovenosos en el proceso de farmacia clínica y administración de medicamentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListadoProductosenLiquidos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListadoProductosenLiquidos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtener el listado de medicamentos/productos activos clasificados como líquidos, con su clasificación POS y código DCI, para consulta operativa.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosenLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El catálogo de productos debe contener registros con la marca de presentación líquida y los tipos de producto requeridos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosenLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan productos activos (estado = 1).; Solo se incluyen productos marcados como dilución/presentación líquida (ESPDILPRO=1).; Solo se consideran tipos de producto ''1'' y ''3''.; El código DCI nunca se retorna NULL: si está vacío o nulo se entrega cadena vacía.; Se eliminan espacios finales del código y descripción del producto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosenLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento; Producto POS/No POS; Código DCI; Productos en presentación líquida', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosenLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.IHLISTPRO: Cuando ESPDILPRO=1 AND TIPPRODUC IN (''1'',''3'') AND PROESTADO=1 → se retorna el conjunto de productos (Código, Medicamento, NO POS, CODDCIMED).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosenLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosenLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosenLiquidos';
-- GO
