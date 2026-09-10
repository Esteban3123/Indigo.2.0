
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos e insumos disponibles para preparar mezclas (nutrición parenteral, quimioterapia u otras preparaciones magistrales), filtrando por centro de atención y bodegas asociadas. Consulta el catálogo maestro de productos (IHLISTPRO) junto con la clasificación ATC, el inventario físico por bodega y las formas de administración, para mostrar solo productos activos con tipo de formulación apta para mezclas (tipos 1 a 4). Por cada medicamento retorna el código, descripción, cantidad disponible en bodega, unidades de peso y volumen, abreviatura para mezclas, tiempo de estabilidad, si requiere estabilidad, y si pertenece al PBS/POS (No POS, Condicionado o Sí). Es usado en el módulo de Historia Clínica para que el profesional de salud seleccione los componentes al prescribir o preparar una mezcla farmacéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosParaMezclas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosParaMezclas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devolver el catálogo de productos/medicamentos aptos para preparar mezclas, con su disponibilidad agregada en los almacenes del centro de atención indicado y atributos clínicos/regulatorios (POS, PBS, UNIRS, estabilidad, DCI).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de almacén/centro de atención debe estar disponible para filtrar bodegas; Deben existir registros en Inventory.ATC asociados al producto con FormulationType apto para mezclas (1..4); El producto debe estar activo y con tipo y forma farmacéutica permitidos para mezclas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan productos activos (PROESTADO = 1); Solo productos de tipo ''1'' o ''3'' (TIPPRODUC); Solo productos cuya forma farmacéutica esté entre ''1'',''2'',''3'',''4'' (TIPFORMED); Solo se cruzan productos cuya clasificación ATC tenga FormulationType en (1,2,3,4), es decir aptos para mezclas; Solo se consideran productos de inventario en estado activo (InventoryProduct.Status = 1); Las cantidades disponibles se suman únicamente sobre los almacenes filtrados (centro de atención + almacenes sin centro); Siempre se incluyen los almacenes sin centro de atención asignado, exista o no centro de atención de entrada; Cantidades nulas se tratan como 0 al sumar disponibles', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto/Medicamento; Mezclas (preparaciones magistrales); Clasificación ATC; Inventario físico por bodega; Centro de atención; Forma farmacéutica/Forma de administración; POS / No POS / PBS; Condicionado; UNIRS; Estabilidad; Denominación Común Internacional (DCI); Grupo farmacológico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve productos activos (PROESTADO=1) con TIPPRODUC IN (''1'',''3'') y TIPFORMED IN (''1'',''2'',''3'',''4''), unidos a ATC con FormulationType IN (1,2,3,4), agregando la cantidad física disponible solo de los almacenes del centro de atención y de los almacenes sin centro de atención; [RETURN_RESULT] RESULTSET: Cuando ATC.Conditioned=1 → PBS=''Condicionado''; cuando NOPOSPROD=1 → PBS=''No''; en caso contrario PBS=''Si''; [RETURN_RESULT] RESULTSET: Cuando ATC.UNIRS=1 → columna UNIRS=''Si''; en caso contrario ''No''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Centro de atención recibido no es nulo y tiene longitud > 0 → Se incluyen en el conjunto de almacenes los Warehouse cuyo CodeCenterAttention coincide con el centro indicado else Solo se consideran almacenes sin centro de atención asignado; si ATC.Conditioned = 1 → Se etiqueta el producto como ''Condicionado'' en la columna PBS; si IHLISTPRO.NOPOSPROD = 1 (y no condicionado) → PBS se reporta como ''No'' else PBS se reporta como ''Si''; si ATC.UNIRS = 1 → UNIRS se devuelve como ''Si'' else UNIRS se devuelve como ''No''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Warehouse; dbo.IHLISTPRO; Inventory.ATC; Inventory.InventoryProduct; Inventory.PhysicalInventory; dbo.IHFORMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaMezclas';
-- GO
