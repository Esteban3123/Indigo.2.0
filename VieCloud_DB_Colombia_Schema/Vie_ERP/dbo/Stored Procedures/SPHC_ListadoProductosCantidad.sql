
CREATE PROCEDURE [dbo].[SPHC_ListadoProductosCantidad]
(
@Paciente varchar(25),
@Ingreso char(10),
@CentroAtencion char(10),
@UnidadFuncional char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(A.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,CASE WHEN B.CANACTPRO IS NULL THEN 0 ELSE B.CANACTPRO END AS Fisico
FROM dbo.IHLISTPRO A 
LEFT OUTER JOIN dbo.HCFISIPRO B ON A.CODPRODUC=B.CODPRODUC AND IPCODPACI=@Paciente AND 
NUMINGRES=@Ingreso AND CODCENATE=@CentroAtencion AND UFUCODIGO=@UnidadFuncional
WHERE A.TIPPRODUC IN ('2','3') AND PROESTADO='1'
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el listado de productos farmacéuticos y dispositivos médicos activos (tipo 2 y 3) disponibles en el catálogo, junto con la cantidad física real registrada para un paciente, ingreso, centro de atención y unidad funcional específicos. Cruza el catálogo maestro de productos (IHLISTPRO) con el inventario físico de productos por paciente e ingreso (HCFISIPRO) para mostrar, por cada producto, su código, descripción y stock actual en el piso o servicio. Se utiliza para apoyar la dispensación y control de medicamentos e insumos durante la hospitalización o atención del paciente, permitiendo saber qué productos están disponibles y en qué cantidad física existen para ese ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListadoProductosCantidad';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListadoProductosCantidad';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el catálogo de productos activos de ciertos tipos junto con la cantidad física registrada para un paciente en un ingreso, centro de atención y unidad funcional específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosCantidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere identificar al paciente, ingreso, centro de atención y unidad funcional para cruzar con la cantidad física registrada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosCantidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan productos con TIPPRODUC en (''2'',''3'') y PROESTADO=''1'' (productos activos de tipos específicos).; Cuando no existe registro de cantidad física asociada al paciente/ingreso/centro/unidad, la cantidad reportada es 0 en lugar de NULL.; El listado siempre incluye todos los productos del catálogo que cumplan el filtro, exista o no movimiento físico para el paciente (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosCantidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto; Paciente; Ingreso hospitalario; Centro de atención; Unidad funcional; Cantidad física de producto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosCantidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.IHLISTPRO: Cuando TIPPRODUC IN (''2'',''3'') AND PROESTADO=''1'', se retorna el producto con su código, descripción y la cantidad física (0 si no existe coincidencia en HCFISIPRO para el paciente/ingreso/centro/unidad).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosCantidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHLISTPRO; dbo.HCFISIPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosCantidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosCantidad';
-- GO
