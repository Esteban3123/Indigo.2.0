
CREATE PROCEDURE [dbo].[SP_INV_ListarSubGrupos]
(
@VersionERP int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF @VersionERP=1
        
    	SELECT ISGCODIGO AS 'CODIGO DEL SUBGRUPO',ISGNOMBRE AS 'NOMBRE DEL SUBGRUPO',ISGMANVEN AS 'MANEJA VENCIMIENTO',RTRIM(ISGCODIGO) + ' - ' + RTRIM(ISGNOMBRE) AS 'SUBGRUPO'
		FROM DBO.INSUBGRU

ELSE
	
		SELECT OID,ISGCODIGO AS 'CODIGO DEL SUBGRUPO',ISGNOMBRE AS 'NOMBRE DEL SUBGRUPO',ISGMANVEN AS 'MANEJA VENCIMIENTO',RTRIM(ISGCODIGO) + ' - ' + RTRIM(ISGNOMBRE) AS 'SUBGRUPO'
		FROM DBO.INNSUBGRU
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los subgrupos de inventario registrados en el sistema, incluyendo el código, nombre y si el subgrupo maneja control de vencimiento de productos. Según la versión del ERP (@VersionERP), consulta la tabla de subgrupos correspondiente (INSUBGRU para versión 1 o INNSUBGRU para versiones posteriores). Se utiliza para poblar listas desplegables o catálogos de clasificación de artículos de inventario, permitiendo agrupar medicamentos, insumos y materiales bajo categorías de segundo nivel.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarSubGrupos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarSubGrupos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los subgrupos de inventario seleccionando el origen de datos según la versión del ERP (legacy o nueva).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarSubGrupos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe indicarse la versión del ERP para decidir el origen de datos (versión legacy vs. nueva)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarSubGrupos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El campo concatenado ''SUBGRUPO'' siempre se construye como código + '' - '' + nombre con espacios derechos recortados; Ambas ramas exponen el mismo conjunto de columnas de negocio (código, nombre, maneja vencimiento, etiqueta concatenada); solo difiere la inclusión de OID en la versión nueva', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarSubGrupos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Subgrupo de inventario; Manejo de vencimiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarSubGrupos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] DBO.INSUBGRU: Cuando VersionERP=1, retorna código, nombre, indicador de manejo de vencimiento y etiqueta concatenada del subgrupo desde INSUBGRU; [RETURN_RESULT] DBO.INNSUBGRU: Cuando VersionERP<>1, retorna OID, código, nombre, indicador de manejo de vencimiento y etiqueta concatenada del subgrupo desde INNSUBGRU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarSubGrupos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si VersionERP = 1 → Consulta los subgrupos desde la tabla legacy INSUBGRU sin incluir identificador OID else Consulta los subgrupos desde la tabla nueva INNSUBGRU incluyendo el identificador OID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarSubGrupos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.INSUBGRU; DBO.INNSUBGRU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarSubGrupos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarSubGrupos';
-- GO
