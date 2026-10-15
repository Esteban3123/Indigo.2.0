
CREATE PROCEDURE [dbo].[SPHC_ListadoProductosenMezclas]
AS
BEGIN
	SET NOCOUNT ON;

SELECT RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.ABRPROMEZ) AS 'Descripcion DCI', RTRIM(D.DESPRODUC) AS Medicamento, COALESCE(NULLIF(D.CODDCIMED, CAST('' AS CHAR)),CAST('' AS CHAR)) AS 'Codigo DCI', phy.Quantity AS Disponibles
from dbo.IHLISTPRO D With(Nolock)
	INNER JOIN Inventory.ATC C  With(Nolock) ON D.CODPRODUC = C.Code
	LEFT OUTER JOIN Inventory.InventoryProduct B  With(Nolock) on B.Status = 1 AND B.ATCId = C.Id
	LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = B.Id
WHERE TIPFORMED IN ('1','2','3') AND TIPPRODUC IN ('1','3') AND PROESTADO = 1 
end