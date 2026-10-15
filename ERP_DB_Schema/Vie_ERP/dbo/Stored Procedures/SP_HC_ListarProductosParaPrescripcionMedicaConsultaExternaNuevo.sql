CREATE PROCEDURE [dbo].[SP_HC_ListarProductosParaPrescripcionMedicaConsultaExternaNuevo]
(
@Almacen char(4),
@CentroAtencion char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

		DECLARE @Almacenes as table (Id int)

		IF @CentroAtencion is not null and len(rtrim(@CentroAtencion))> 0
		BEGIN
			INSERT INTO @Almacenes
			select Id from Inventory.Warehouse where CodeCenterAttention = @CentroAtencion
		END
		
		SELECT 
		MAX(B.CodeCUM)as CodeCUM,RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Medicamento, D.NOPOSPROD AS 'NO POS',sum(isnull(phy.Quantity,0)) as Disponibles,D.TIPFORMED,D.CODGRUFAR,D.CODJUMEES As JustificacionMedicamentosEspeciales, C.Antibiotic 
		, ResistenciaBacteriana = CAST(CASE WHEN BRM.Id IS NULL THEN 0 ELSE 1 END AS BIT) , ObservationRB = ISNULL(BRM.Observation,''), C.Conditioned, UNIRS = CASE WHEN C.UNIRS = 1 THEN 'Si' ELSE 'No' END
		, PBS = CASE WHEN C.Conditioned = 1 THEN 'Condicionado'
		   WHEN	D.NOPOSPROD = 1 THEN 'No'
		   ELSE 'Si'
		   END
		,D.PROCONTRO
		,RTRIM(D.CODDCIMED) AS 'Codigo DCI'
		,RTRIM(D.ABRPROMEZ) AS 'Descripcion DCI'
		,'1 - Medicamentos del inventario' as Tipo,
		B.DairyComponent AS 'ComponenteLacteo',
		B.DairyComponentType AS 'TipoComponenteLacteo'
		from 
		dbo.IHLISTPRO D With(Nolock)
		INNER JOIN Inventory.ATC C  With(Nolock) ON RTRIM(D.CODPRODUC) = C.Code and C.DiluentProduct = 0
		LEFT OUTER JOIN Inventory.InventoryProduct B  With(Nolock) on B.Status = 1 AND B.ATCId = C.Id
		LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = B.Id
		AND phy.WarehouseId IN (SELECT Id from @Almacenes)
		LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
		LEFT OUTER JOIN INVENTORY.BacterialResistanceMedication BRM
		ON BRM.AtcId = C.Id AND BRM.StateBRM IN (1,2) AND [Common].[GETDATE]() BETWEEN BRM.StartDate AND BRM.EndDate
		WHERE  D.TIPPRODUC IN ('1', '3') AND D.ESPDILPRO = 0 AND D.PROESTADO = 1
 		GROUP BY RTRIM(D.CODPRODUC), RTRIM(D.DESPRODUC), D.NOPOSPROD,D.TIPFORMED,D.CODGRUFAR,D.CODJUMEES, C.Antibiotic 
		, CAST(CASE WHEN BRM.Id IS NULL THEN 0 ELSE 1 END AS BIT) , ISNULL(BRM.Observation,''), C.Conditioned, C.UNIRS,D.PROCONTRO, D.CODDCIMED, D.ABRPROMEZ, B.DairyComponent, B.DairyComponentType 

		UNION ALL
		--cargamos medicamento adicionales creadas por los medicos EJ: Alcachofa 500 mg Natural Freshly	
		SELECT 
		  '' as CodeCUM, 
		  RTRIM(D.CODPRODUC) AS Codigo, 
		  RTRIM(D.DESPRODUC) AS Medicamento, 
		  D.NOPOSPROD AS 'NO POS', 
		  0 as Disponibles, 
		  D.TIPFORMED, 
		  D.CODGRUFAR, 
		  D.CODJUMEES As JustificacionMedicamentosEspeciales, 
		  CAST(0 AS BIT) as Antibiotic, 
		  ResistenciaBacteriana = CAST(0 AS BIT), 
		  ObservationRB = '', 
		  CAST(0 AS BIT) as Conditioned, 
		  UNIRS = 'No', 
		  PBS = 'No', 
		  D.PROCONTRO, 
		  '' AS 'Codigo DCI', 
		  '' AS 'Descripcion DCI', 
		  '2 - Medicamentos Adicionales' as Tipo,
		  0 AS 'ComponenteLacteo',
		  0 AS 'TipoComponenteLacteo'
		from 
		  dbo.IHLISTPRO D With(Nolock) 
		WHERE 
		  D.TIPPRODUC IN (1) 
		  AND D.ESPDILPRO = 0 
		  AND D.PROESTADO = 1
		  AND D.CODDCIMED IS NULL 
		  AND D.AddedAutomatic = 1 
		GROUP BY 
		  RTRIM(D.CODPRODUC),RTRIM(D.DESPRODUC), D.NOPOSPROD, D.TIPFORMED, D.CODGRUFAR, D.CODJUMEES, D.PROCONTRO, D.CODDCIMED,D.ABRPROMEZ    


END