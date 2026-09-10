
CREATE PROCEDURE [dbo].[SP_INV_ListarCodigosIVA]
(
@VersionERP int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
IF @VersionERP=1
	SELECT IVACODIGO AS 'CODIGO DEL IVA',IVANOMBRE AS 'NOMBRE DEL IVA',IVAPORCEN AS 'PORCENTAJE DE IVA', RTRIM(IVACODIGO)+' - '+ RTRIM(IVANOMBRE) AS 'IVA'
	FROM DBO.DGTABIVA
ELSE
	SELECT OID,IVACODIGO AS 'CODIGO DEL IVA',IVANOMBRE AS 'NOMBRE DEL IVA',IVAPORCEN AS 'PORCENTAJE DE IVA', RTRIM(IVACODIGO)+' - '+ RTRIM(IVANOMBRE) AS 'IVA'
	FROM DBO.GENTABIVA
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los códigos de IVA disponibles en el sistema, incluyendo su código identificador, nombre y porcentaje aplicable. Según la versión del ERP indicada, consulta la tabla de IVA correspondiente (DGTABIVA para versión 1 o GENTABIVA para versiones posteriores). Se utiliza para poblar catálogos o listas desplegables de tipos de IVA en procesos de facturación, inventario y configuración tributaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarCodigosIVA';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarCodigosIVA';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el catálogo de códigos de IVA, eligiendo la tabla origen según la versión del ERP indicada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe indicarse la versión de ERP para determinar qué tabla de IVA consultar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre incluye un campo ''IVA'' compuesto por código y nombre concatenados con guion, sin espacios sobrantes a la derecha.; La tabla consultada depende exclusivamente de la versión del ERP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'IVA; Catálogo de impuestos; Versión de ERP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] DBO.DGTABIVA: Cuando la versión de ERP es 1, retorna código, nombre, porcentaje y descripción concatenada del IVA desde DGTABIVA.; [RETURN_RESULT] DBO.GENTABIVA: Cuando la versión de ERP es distinta de 1, retorna OID además de código, nombre, porcentaje y descripción concatenada del IVA desde GENTABIVA.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Versión de ERP = 1 → Consulta el catálogo de IVA en DGTABIVA (sin OID). else Consulta el catálogo de IVA en GENTABIVA incluyendo OID.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.DGTABIVA; DBO.GENTABIVA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarCodigosIVA';
-- GO
