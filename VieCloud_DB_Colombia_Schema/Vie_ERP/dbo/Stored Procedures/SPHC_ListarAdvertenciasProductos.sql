CREATE PROCEDURE [dbo].[SPHC_ListarAdvertenciasProductos]
(
@CodigoProducto varchar(20)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT DCI.typewarning AS 'TIPO', STUFF((SELECT ' ' + RTRIM(RL.Name) + ': ' + RTRIM(HRD.Observation) + char(10) FROM Inventory.HighRiskDrugs HRD INNER JOIN Inventory.InventoryRiskLevel RL ON HRD.InventoryRiskLevelId = RL.Id 
WHERE HRD.DCIId = DCI.Id FOR XML PATH('')), 1, 1, '') As 'ADVERTENCIA'
FROM dbo.IHLISTPRO PRO
INNER JOIN Inventory.DCI DCI ON PRO.CODDCIMED = DCI.Code
WHERE PRO.CODPRODUC = @CodigoProducto
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y devuelve las advertencias de seguridad asociadas a un producto farmacéutico o medicamento, dado su código de producto. A partir del código ingresado, identifica el principio activo (DCI) del producto en el catálogo maestro de medicamentos, y luego recupera las alertas de alto riesgo registradas para ese principio activo, agrupando por tipo de advertencia y concatenando el nivel de riesgo con su observación de seguridad. Se utiliza en la gestión clínica y farmacéutica para alertar al personal de salud sobre medicamentos de alto riesgo que requieren controles especiales antes de su dispensación o administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarAdvertenciasProductos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarAdvertenciasProductos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el tipo de advertencia y el listado consolidado de observaciones de alto riesgo asociadas al DCI de un producto farmacéutico determinado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarAdvertenciasProductos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El producto debe existir en dbo.IHLISTPRO con el código indicado.; El producto debe tener un código de DCI (CODDCIMED) que coincida con un registro en Inventory.DCI para retornar resultados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarAdvertenciasProductos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La advertencia se construye únicamente con los registros de alto riesgo asociados al DCI del producto consultado.; Cada advertencia concatena el nombre del nivel de riesgo y la observación, separados por salto de línea.; Solo se devuelven advertencias para productos cuyo código de DCI coincide con el catálogo Inventory.DCI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarAdvertenciasProductos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto farmacéutico; Denominación Común Internacional (DCI); Medicamento de alto riesgo; Nivel de riesgo de inventario; Advertencia de medicamento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarAdvertenciasProductos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando el producto coincide en IHLISTPRO y su CODDCIMED existe en Inventory.DCI, retorna el typewarning del DCI y la concatenación (vía FOR XML PATH) de ''Nivel: Observación'' por cada registro de Inventory.HighRiskDrugs ligado a ese DCI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarAdvertenciasProductos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHLISTPRO; Inventory.DCI; Inventory.HighRiskDrugs; Inventory.InventoryRiskLevel', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarAdvertenciasProductos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarAdvertenciasProductos';
-- GO
