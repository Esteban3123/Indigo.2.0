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