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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los líquidos diluyentes disponibles para prescripción médica en un centro de atención específico. Combina el catálogo de productos farmacéuticos (IHLISTPRO) con la clasificación ATC —filtrando solo los marcados como producto diluyente— y consulta el stock disponible en las bodegas asociadas al centro de atención indicado (incluyendo bodegas globales sin centro asignado). Para cada líquido retorna su código, nombre, DCI (Denominación Común Internacional), unidades disponibles en inventario físico, forma farmacéutica, volumen total, indicador de PBS/No PBS/Condicionado, si requiere estabilidad y si es NO POS, facilitando así la selección de diluyentes al momento de prescribir o formular medicamentos en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarLiquidosParaPrescripcion';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarLiquidosParaPrescripcion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos diluentes (líquidos) disponibles para prescripción médica, con su disponibilidad en almacenes del centro de atención y clasificación POS/condicionado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarLiquidosParaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir productos marcados como diluentes (Inventory.ATC.DiluentProduct = 1) y activos en el catálogo (PROESTADO = 1, ESPDILPRO = 1); Si se desea filtrar por centro de atención, el código debe corresponder a uno o más almacenes en Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarLiquidosParaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre se incluyen los almacenes sin centro de atención asignado, además de los del centro filtrado; Solo se consideran productos diluentes (ATC.DiluentProduct = 1) activos (PROESTADO = 1) y marcados como especiales para dilución (ESPDILPRO = 1); Solo se suman cantidades de InventoryProduct con Status = 1 (productos activos); El stock disponible se calcula como suma de PhysicalInventory.Quantity, devolviendo 0 cuando no hay existencias; La clasificación PBS prioriza ''Condicionado'' sobre la condición POS/No POS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarLiquidosParaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento diluente; Prescripción médica; Clasificación ATC; Producto POS / No POS; Producto condicionado (PBS); Inventario físico por bodega; Centro de atención; Forma farmacéutica; Estabilidad de medicamentos; DCI (Denominación Común Internacional); Mezcla / preparación magistral', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarLiquidosParaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve productos cuyo TIPPRODUC IN (''1'',''3''), ESPDILPRO = 1 y PROESTADO = 1, que sean diluentes (ATC.DiluentProduct = 1), con cantidades disponibles agregadas desde PhysicalInventory restringidas a los almacenes del centro y a almacenes sin centro asignado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarLiquidosParaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Se proporciona código de centro de atención no vacío → Incluye en el set de almacenes los Warehouse cuyo CodeCenterAttention coincide con el centro else Solo se incluyen los almacenes sin centro asignado (CodeCenterAttention IS NULL); si ATC.Conditioned = 1 → El indicador PBS se reporta como ''Condicionado''; si ATC.Conditioned = 0 y NOPOSPROD = 1 → El indicador PBS se reporta como ''No'' (no POS) else El indicador PBS se reporta como ''Si'' (POS); si ATC.UNIRS = 1 → Se reporta UNIRS = ''Si'' else Se reporta UNIRS = ''No''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarLiquidosParaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Warehouse; dbo.IHLISTPRO; dbo.IHDCIMEDI; Inventory.ATC; Inventory.InventoryProduct; Inventory.PhysicalInventory; dbo.IHFORMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarLiquidosParaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarLiquidosParaPrescripcion';
-- GO
