CREATE PROCEDURE [dbo].[SP_HC_ListarMezclasParaPrescripcionMedica_CIMA]
(
@Almacen char(4),
@Denominacion char(20),
@VersionERP int,
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
				END,
				rtrim(ltrim(dci.CODDCIMED)) as CodigoDCI,
				rtrim(ltrim(dci.DESDCIMED)) as DescripcionDCI,
				RTRIM(D.CODPRODUC) + ' - '+ RTRIM(D.DESPRODUC) AS CodigoDescripcionMedicamento
from 
	dbo.IHLISTPRO D With(Nolock)
	INNER JOIN Inventory.ATC C  With(Nolock) ON RTRIM(D.CODPRODUC) = C.Code and C.FormulationType IN(1,2,3,4)
	LEFT OUTER JOIN Inventory.InventoryProduct B  With(Nolock) on B.Status = 1 AND B.ATCId = C.Id
	LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = B.Id
	AND phy.WarehouseId IN (SELECT Id from @Almacenes)
	LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
	INNER JOIN dbo.IHDCIMEDI DCI on DCI.CODDCIMED = D.CODDCIMED 
	WHERE-- D.CODDCIMED = @Denominacion AND 
	D.TIPPRODUC IN ('1', '3') AND D.TIPFORMED IN ('1','2','3') AND D.PROESTADO = 1
 	GROUP BY RTRIM(D.CODPRODUC), RTRIM(D.DESPRODUC),D.NOPOSPROD,D.TIPFORMED,D.CODGRUFAR,D.CODUNIPES,D.CODUNIVOL,
		RTRIM(D.ABRPROMEZ), D.VOLTOTMED,D.PESTOTMED,D.TIEESTMED,C.Conditioned,C.UNIRS,dci.CODDCIMED,dci.DESDCIMED
		
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que lista mezclas intravenosas y preparaciones magistrales disponibles para prescripción médica, filtrando productos de tipo farmacéutico (tipo 1 y 3) con formulación 1, 2 o 3 y estado activo. Cruza el catálogo de productos con la clasificación ATC, el DCI (Denominación Común Internacional) y el stock físico real por bodega, considerando el centro de atención indicado para determinar los almacenes aplicables. Retorna disponibilidad en unidades, volumen/peso total, condición PBS/No PBS y datos técnicos de la mezcla.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMezclasParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMezclasParaPrescripcionMedica_CIMA';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos aptos para mezclas en prescripción médica, con su disponibilidad agregada por almacenes asociados al centro de atención y datos POS/PBS/DCI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMezclasParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los productos deben estar activos (PROESTADO = 1); El producto debe tener clasificación ATC con FormulationType en (1,2,3,4); El producto debe tener un DCI asociado en IHDCIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMezclasParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran productos cuyo InventoryProduct tenga Status=1; La cantidad disponible nunca es NULL: se usa ISNULL(Quantity,0) y se agrega con SUM; Solo se incluyen formulaciones aptas para mezcla (FormulationType 1-4 y TIPFORMED 1-3); Los almacenes globales (CodeCenterAttention NULL) siempre se consideran en el cálculo de disponibilidad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMezclasParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Prescripción médica; Mezclas de medicamentos; Medicamento POS / No POS; PBS (Plan de Beneficios en Salud) Condicionado; Clasificación ATC; DCI (Denominación Común Internacional); Centro de atención; Almacén/Bodega; Inventario físico disponible; Tipo de formulación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMezclasParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Almacenes: Si se recibe centro de atención no nulo y no vacío, inserta los almacenes cuyo CodeCenterAttention coincide; [INSERT] @Almacenes: Siempre inserta además los almacenes con CodeCenterAttention IS NULL (almacenes globales/sin centro); [RETURN_RESULT] (resultset): Devuelve solo productos con TIPPRODUC IN (''1'',''3''), TIPFORMED IN (''1'',''2'',''3'') y PROESTADO=1, agrupados por producto sumando inventario físico de los almacenes filtrados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMezclasParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CentroAtencion no nulo y con longitud > 0 → Incluye en el conjunto de almacenes los del centro de atención indicado, además de los globales else Solo se consideran los almacenes con CodeCenterAttention NULL; si C.Conditioned = 1 → PBS se reporta como ''Condicionado'' else Si NOPOSPROD=1 PBS=''No'', en otro caso PBS=''Si''; si C.UNIRS = 1 → UNIRS se devuelve como ''Si'' else UNIRS se devuelve como ''No''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMezclasParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Warehouse; dbo.IHLISTPRO; Inventory.ATC; Inventory.InventoryProduct; Inventory.PhysicalInventory; dbo.IHDCIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMezclasParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMezclasParaPrescripcionMedica_CIMA';
-- GO
