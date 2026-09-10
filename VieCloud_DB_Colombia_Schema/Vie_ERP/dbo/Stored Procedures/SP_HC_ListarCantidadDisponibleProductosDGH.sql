  
  
CREATE PROCEDURE [dbo].[SP_HC_ListarCantidadDisponibleProductosDGH]  
(  
@Almacen CHAR(4),  
@Producto CHAR(20),  
@VersionERP INT  
)  
AS  
BEGIN  
 SET NOCOUNT ON;  
   
IF @VersionERP=1  
  
 select sum(IFICANTID) AS Disponibles   
 from dbo.infisico   
 where iprcodigo in (select iprcodigo from dbo.ihrinddgh where codproduc = @Producto) and ialcodigo = @Almacen  
        
ELSE  
   
 select SUM(IFICANTID) AS Disponibles   
 from dbo.INNFISICO A  
 INNER JOIN dbo.INNPRODUC B ON A.INNPRODUC = B.OID  
 INNER JOIN dbo.INNALMACE C ON A.INNALMACE = C.OID  
 where iprcodigo in (select iprcodigo from dbo.ihrinddgh where codproduc = @Producto) and ialcodigo = @Almacen  
        
   
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta la cantidad disponible (stock) de un producto farmacéutico o de bodega en un almacén específico, considerando la versión del ERP instalada. Recibe como parámetros el código del almacén, el código del producto y la versión del sistema, y devuelve la suma de unidades físicas disponibles. Utiliza la tabla de indicadores DGH (IHRINDDGH) para relacionar el código de producto con el código de ítem interno, y luego consulta el inventario físico (infisico o INNFISICO según la versión) para obtener la cantidad. Se usa típicamente en historia clínica o dispensación para verificar existencias antes de entregar o prescribir un medicamento o insumo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarCantidadDisponibleProductosDGH';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarCantidadDisponibleProductosDGH';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtener la cantidad disponible (stock físico) de un producto en un almacén determinado, soportando dos modelos de datos de inventario según la versión del ERP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCantidadDisponibleProductosDGH';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir mapeo del producto en la tabla de equivalencias DGH→ERP para obtener códigos internos de producto.; El almacén indicado debe existir en el modelo de inventario correspondiente a la versión del ERP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCantidadDisponibleProductosDGH';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El producto se traduce siempre desde el código DGH (codproduc) a uno o varios códigos internos del ERP (iprcodigo) vía la tabla de mapeo ihrinddgh.; El cálculo de disponibles es siempre una sumatoria de existencias físicas (IFICANTID) restringida al almacén indicado.; Existen dos modelos de inventario soportados según versión del ERP: legado (infisico) y nuevo (INNFISICO con relaciones por OID).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCantidadDisponibleProductosDGH';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto; Almacén; Stock disponible; Inventario físico; Mapeo de productos DGH a ERP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCantidadDisponibleProductosDGH';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.infisico: Cuando VersionERP=1 retorna la suma de IFICANTID desde infisico para el almacén y los iprcodigo asociados al producto en ihrinddgh.; [RETURN_RESULT] dbo.INNFISICO: Cuando VersionERP<>1 retorna la suma de IFICANTID desde INNFISICO uniendo con INNPRODUC e INNALMACE por OID, filtrando por almacén y los iprcodigo asociados en ihrinddgh.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCantidadDisponibleProductosDGH';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si VersionERP = 1 → Consulta cantidad disponible sumando IFICANTID desde dbo.infisico filtrando por almacén y por los códigos internos de producto (iprcodigo) mapeados en ihrinddgh. else Consulta cantidad disponible sumando IFICANTID desde dbo.INNFISICO, uniendo con INNPRODUC e INNALMACE por OID, aplicando el mismo filtro por almacén y mapeo en ihrinddgh.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCantidadDisponibleProductosDGH';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.infisico; dbo.ihrinddgh; dbo.INNFISICO; dbo.INNPRODUC; dbo.INNALMACE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCantidadDisponibleProductosDGH';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCantidadDisponibleProductosDGH';
-- GO
