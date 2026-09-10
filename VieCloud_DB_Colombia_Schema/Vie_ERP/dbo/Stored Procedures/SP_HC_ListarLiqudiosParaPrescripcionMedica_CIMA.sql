CREATE PROCEDURE [dbo].[SP_HC_ListarLiqudiosParaPrescripcionMedica_CIMA]
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

		if @CentroAtencion is not null and len(rtrim(@CentroAtencion))> 0
		begin
			INSERT INTO @Almacenes
			select Id from Inventory.Warehouse where CodeCenterAttention = @CentroAtencion
		end

		INSERT INTO @Almacenes
		select Id from Inventory.Warehouse where CodeCenterAttention IS NULL

		SELECT RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Medicamento,D.NOPOSPROD AS 'NO POS',sum(ISNULL(phy.Quantity,0)) AS Disponibles,D.TIPFORMED,D.CODGRUFAR,D.CODUNIPES AS UnidadPeso,D.CODUNIVOL AS UnidadVolumen,
			RTRIM(D.ABRPROMEZ) AS AbreviaturaProducto,D.VOLTOTMED AS TotalVolumen,D.VOLTOTMED,D.PESTOTMED,D.TIEESTMED,CAST(0 AS BIT) AS FormatoNOPOS, D.TODASPATO
			, C.Conditioned, UNIRS = CASE WHEN C.UNIRS = 1 THEN 'Si' ELSE 'No' END,CODFORMED
			, PBS = CASE WHEN C.Conditioned = 1 THEN 'Condicionado'
				WHEN D.NOPOSPROD = 1 THEN 'No'
				ELSE 'Si'
				END,
				rtrim(ltrim(dci.CODDCIMED)) as CodigoDCI,
				rtrim(ltrim(dci.DESDCIMED)) as DescripcionDCI,
				RTRIM(D.CODPRODUC) + ' - '+ RTRIM(D.DESPRODUC) AS CodigoDescripcionMedicamento
from 
	dbo.IHLISTPRO D With(Nolock)
	INNER JOIN Inventory.ATC C  With(Nolock) ON RTRIM(D.CODPRODUC) = C.Code and C.DiluentProduct = 1
	LEFT OUTER JOIN Inventory.InventoryProduct B  With(Nolock) on B.Status = 1 AND B.ATCId = C.Id
	LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = B.Id
	AND phy.WarehouseId IN (SELECT Id from @Almacenes)
	LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
	INNER JOIN dbo.IHDCIMEDI DCI on DCI.CODDCIMED = D.CODDCIMED 
	WHERE-- D.CODDCIMED = @Denominacion AND 
	D.TIPPRODUC IN ('1', '3') AND D.ESPDILPRO = 1 AND D.PROESTADO = 1
 		GROUP BY RTRIM(D.CODPRODUC), RTRIM(D.DESPRODUC),D.NOPOSPROD,D.TIPFORMED,D.CODGRUFAR,D.CODUNIPES,D.CODUNIVOL,
			RTRIM(D.ABRPROMEZ),D.VOLTOTMED,D.PESTOTMED,D.TIEESTMED,D.TODASPATO,C.Conditioned,C.UNIRS,CODFORMED,dci.CODDCIMED,dci.DESDCIMED
		
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que lista líquidos diluyentes disponibles para prescripción médica, filtrando productos marcados como diluyentes especiales (`DiluentProduct = 1`, `ESPDILPRO = 1`) y activos, de tipo farmacéutico 1 o 3. Consolida el stock físico disponible por bodegas asociadas al centro de atención indicado, enriqueciendo cada producto con su clasificación DCI, condición PBS/No POS/Condicionado, volumen, peso y abreviatura, orientado al módulo de prescripción médica integrado con CIMA.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarLiqudiosParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarLiqudiosParaPrescripcionMedica_CIMA';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos/productos clasificados como diluyentes disponibles para prescripción médica, con sus existencias agregadas por almacenes asociados al centro de atención y su clasificación POS/PBS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarLiqudiosParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir productos en IHLISTPRO con TIPPRODUC en (''1'',''3''), ESPDILPRO=1 y PROESTADO=1.; El producto debe estar registrado en Inventory.ATC con DiluentProduct=1.; Cada producto debe tener un código DCI válido vinculado a IHDCIMEDI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarLiqudiosParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran productos marcados como diluyentes (ESPDILPRO=1 y ATC.DiluentProduct=1).; Solo productos activos (PROESTADO=1) y de tipo ''1'' o ''3''.; Las existencias se agregan únicamente sobre productos de inventario con Status=1.; Siempre se incluyen los almacenes globales (CodeCenterAttention NULL) en el cálculo de disponibilidad.; FormatoNOPOS siempre se devuelve como 0 (BIT).; La clasificación PBS prioriza ''Condicionado'' sobre la marca NOPOS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarLiqudiosParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Prescripción médica; Medicamento diluyente; Clasificación DCI; Clasificación POS/NO POS; PBS (Plan de Beneficios en Salud) Condicionado; Centro de atención; Almacén/Bodega; Inventario físico disponible; Clasificación ATC; UNIRS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarLiqudiosParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Almacenes: Cuando @CentroAtencion no es nulo ni vacío, inserta los Id de Inventory.Warehouse cuyo CodeCenterAttention coincide con el centro recibido (lo hace dos veces de forma duplicada).; [INSERT] @Almacenes: Siempre inserta los Id de Inventory.Warehouse con CodeCenterAttention IS NULL (almacenes globales/sin centro), ejecutándose dos veces.; [RETURN_RESULT] RESULTSET: Retorna los productos diluyentes activos con sus existencias sumadas (Disponibles), información farmacológica, DCI y clasificación PBS calculada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarLiqudiosParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Conditioned = 1 en ATC → Marca PBS = ''Condicionado'' else Si NOPOSPROD=1 marca PBS=''No''; en otro caso PBS=''Si''; si UNIRS = 1 en ATC → Devuelve UNIRS=''Si'' else Devuelve UNIRS=''No''; si @CentroAtencion no nulo y con longitud > 0 → Incluye en el conjunto de almacenes los específicos del centro de atención además de los globales else Solo considera almacenes con CodeCenterAttention NULL (globales)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarLiqudiosParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Warehouse; dbo.IHLISTPRO; Inventory.ATC; Inventory.InventoryProduct; Inventory.PhysicalInventory; dbo.IHDCIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarLiqudiosParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarLiqudiosParaPrescripcionMedica_CIMA';
-- GO
