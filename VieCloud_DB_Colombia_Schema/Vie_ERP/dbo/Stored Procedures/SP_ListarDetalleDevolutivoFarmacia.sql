/**** 
SP para listar el detalle para la creación del JSON para las devoluciones para UNIHEALTH
****/
CREATE PROCEDURE [dbo].[SP_ListarDetalleDevolutivoFarmacia]
(
  @Consecutivo as Int
)
WITH RECOMPILE
AS
BEGIN
	SET NOCOUNT ON;

			SELECT 
				A.Id AS 'Id', RTRIM(A.CODPRODUC) AS 'COD PRODUCTO',RTRIM(F.Name) AS 'PRODUCTO',
				A.CANDEVOLV AS 'CANTIDAD', A.DevolutionObservations AS 'OBSERVACION ITEM'
			FROM HCDEVMEDD A
				INNER JOIN Inventory.InventoryProduct F ON A.CODPRODUC = F.Code				
			WHERE A.CODCONCEC = @Consecutivo 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que lista el detalle de los ítems incluidos en una devolución de farmacia, dado un número de consecutivo de devolución. Para cada ítem devuelto recupera el código y nombre del producto (medicamento, insumo o dispositivo médico) junto con la cantidad devuelta y las observaciones del ítem, cruzando los registros de devolución de la tabla HCDEVMEDD con el catálogo de productos del inventario. Su propósito es generar el detalle necesario para construir el JSON que se envía a UNIHEALTH como parte del proceso de integración de devoluciones de farmacia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarDetalleDevolutivoFarmacia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarDetalleDevolutivoFarmacia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el detalle de ítems (productos, cantidades y observaciones) asociados a una devolución de farmacia, enriqueciendo el código de producto con su nombre desde el catálogo de inventario, para construir un JSON de devoluciones.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleDevolutivoFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el consecutivo de devolución en HCDEVMEDD.; Cada CODPRODUC del detalle debe existir en Inventory.InventoryProduct (INNER JOIN); de lo contrario el ítem no aparece.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleDevolutivoFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan ítems de devolución cuyo producto exista en el catálogo de inventario (INNER JOIN).; El resultado se restringe a un único consecutivo de devolución.; Los códigos y nombres de producto se devuelven sin espacios en blanco a la derecha (RTRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleDevolutivoFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución de medicamentos en farmacia; Detalle de devolución; Producto de inventario; Cantidad devuelta; Observación de devolución', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleDevolutivoFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCDEVMEDD: Cuando CODCONCEC = consecutivo recibido, retorna Id, código y nombre de producto, cantidad devuelta y observación del ítem.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleDevolutivoFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCDEVMEDD; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleDevolutivoFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleDevolutivoFarmacia';
-- GO
