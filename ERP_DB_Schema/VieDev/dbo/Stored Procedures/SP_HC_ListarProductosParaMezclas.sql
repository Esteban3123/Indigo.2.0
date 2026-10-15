
CREATE PROCEDURE [dbo].[SP_HC_ListarProductosParaMezclas]
(
@Almacen CHAR(4),
@CodigoDenominacion CHAR(20),
@VersionERP INT,
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

	SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Medicamento,D.NOPOSPROD AS 'NO POS',sum(ISNULL(phy.Quantity,0)) AS Disponibles,D.TIPFORMED,D.CODGRUFAR,D.CODUNIPES AS UnidadPeso,D.CODUNIVOL AS UnidadVolumen,
		RTRIM(D.ABRPROMEZ) AS AbreviaturaProducto,D.VOLTOTMED AS TotalVolumen,
		D.VOLTOTMED,D.PESTOTMED,D.TIEESTMED,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion
		, C.Conditioned, UNIRS = CASE WHEN C.UNIRS = 1 THEN 'Si' ELSE 'No' END
			, PBS = CASE WHEN C.Conditioned = 1 THEN 'Condicionado'
				WHEN D.NOPOSPROD = 1 THEN 'No'
				ELSE 'Si'
				END,F.RequireStability,
				 RTRIM(D.CODDCIMED) AS 'Codigo DCI',
				 RTRIM(D.ABRPROMEZ) AS 'Descripcion DCI'
				 
	from 
	dbo.IHLISTPRO D With(Nolock)
	INNER JOIN Inventory.ATC C  With(Nolock) ON RTRIM(D.CODPRODUC) = C.Code and C.FormulationType IN(1,2,3,4)
	LEFT OUTER JOIN Inventory.InventoryProduct B  With(Nolock) on B.Status = 1 AND B.ATCId = C.Id
	LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = B.Id
	AND phy.WarehouseId IN (SELECT Id from @Almacenes)
	LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
	LEFT OUTER JOIN dbo.IHFORMEDI F on F.CODFORMED = D.CODFORMED 
	WHERE /*D.CODDCIMED = @CodigoDenominacion AND*/ 
	D.TIPPRODUC IN ('1', '3') AND D.TIPFORMED IN ('1','2','3','4') AND D.PROESTADO = 1
 	GROUP BY RTRIM(D.CODPRODUC), RTRIM(D.DESPRODUC),D.NOPOSPROD,D.TIPFORMED,D.CODGRUFAR,D.CODUNIPES,D.CODUNIVOL,
		RTRIM(D.ABRPROMEZ), D.VOLTOTMED,D.PESTOTMED,D.TIEESTMED,C.Conditioned,C.UNIRS,F.RequireStability ,CODDCIMED
		
end