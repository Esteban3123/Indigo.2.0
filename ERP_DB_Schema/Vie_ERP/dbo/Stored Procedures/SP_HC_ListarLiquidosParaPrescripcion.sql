CREATE PROCEDURE [dbo].[SP_HC_ListarLiquidosParaPrescripcion]
(
@CentroAtencion char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

		DECLARE @Almacenes as table
		(Id int)

		if @CentroAtencion is not null and len(rtrim(@CentroAtencion))> 0
		begin
			INSERT INTO @Almacenes
			select Id from Inventory.Warehouse where CodeCenterAttention = @CentroAtencion
		end

		INSERT INTO @Almacenes
		select Id from Inventory.Warehouse where CodeCenterAttention IS NULL
		
		SELECT 
			RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Medicamento, RTRIM(DCI.CODDCIMED) AS 'CodigoDCI', RTRIM(DCI.DESDCIMED) AS 'DescripcionDCI',
			D.NOPOSPROD AS 'NO POS',sum(ISNULL(phy.Quantity,0)) AS Disponibles,D.TIPFORMED,D.CODGRUFAR,D.CODUNIPES AS UnidadPeso,D.CODUNIVOL AS UnidadVolumen,
			RTRIM(D.ABRPROMEZ) AS AbreviaturaProducto,D.VOLTOTMED AS TotalVolumen,D.VOLTOTMED,D.PESTOTMED,D.TIEESTMED,CAST(0 AS BIT) AS FormatoNOPOS, D.TODASPATO, 
			C.Conditioned, UNIRS = CASE WHEN C.UNIRS = 1 THEN 'Si' ELSE 'No' END,F.CODFORMED, F.RequireStability, 
			PBS = CASE 
					WHEN C.Conditioned = 1 THEN 'Condicionado'
					WHEN D.NOPOSPROD = 1 THEN 'No'
					ELSE 'Si'
					END
		FROM dbo.IHLISTPRO D With(Nolock)
			INNER JOIN DBO.IHDCIMEDI DCI ON DCI.CODDCIMED = D.CODDCIMED 
			INNER JOIN Inventory.ATC C  With(Nolock) ON RTRIM(D.CODPRODUC) = C.Code and C.DiluentProduct = 1
			LEFT OUTER JOIN Inventory.InventoryProduct B  With(Nolock) on B.Status = 1 AND B.ATCId = C.Id
			LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = B.Id AND phy.WarehouseId IN (SELECT Id from @Almacenes)
			LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
			LEFT OUTER JOIN dbo.IHFORMEDI F on F.CODFORMED = D.CODFORMED 
		WHERE 
			D.TIPPRODUC IN ('1', '3') AND D.ESPDILPRO = 1 AND D.PROESTADO = 1
 		GROUP BY RTRIM(D.CODPRODUC), RTRIM(D.DESPRODUC),RTRIM(DCI.CODDCIMED), RTRIM(DCI.DESDCIMED),D.NOPOSPROD,D.TIPFORMED,D.CODGRUFAR,D.CODUNIPES,D.CODUNIVOL,RTRIM(D.ABRPROMEZ),D.VOLTOTMED,D.PESTOTMED,D.TIEESTMED,D.TODASPATO,C.Conditioned,C.UNIRS,F.CODFORMED,F.RequireStability 

END