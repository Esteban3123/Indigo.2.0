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