CREATE PROCEDURE [dbo].[SP_HC_ListarProductosParaPrescripcion]
(
@Extramural as bit,
@Almacen char(4),
@CentroAtencion char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Almacenes as table (Id int)

IF @Extramural=1 begin
		if @CentroAtencion is not null and len(rtrim(@CentroAtencion))> 0
		begin
			INSERT INTO @Almacenes
			select Id from Inventory.Warehouse where CodeCenterAttention = @CentroAtencion
		end

		---29-07-2021 se comenta para que NO sume cantidades del producto de otros centros de atenciòn diferentes al que esta haciendo la HC att: Juan patiño - andres cabrera - naudy - edwin - rafa
		--INSERT INTO @Almacenes 
		--select Id,CodeCenterAttention from Inventory.Warehouse where CodeCenterAttention IS NULL

		SELECT 
		MAX(B.CodeCUM)as CodeCUM,RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Medicamento, D.NOPOSPROD AS 'NO POS',sum(isnull(phy.Quantity,0)) as Disponibles,D.TIPFORMED,D.CODGRUFAR,D.CODJUMEES As JustificacionMedicamentosEspeciales, C.Antibiotic 
		, ResistenciaBacteriana = CAST(CASE WHEN BRM.Id IS NULL THEN 0 ELSE 1 END AS BIT) , ObservationRB = ISNULL(BRM.Observation,''), C.Conditioned, UNIRS = CASE WHEN C.UNIRS = 1 THEN 'Si' ELSE 'No' END
		, PBS = CASE WHEN C.Conditioned = 1 THEN 'Condicionado'
		   WHEN	D.NOPOSPROD = 1 THEN 'No'
		   ELSE 'Si'
		   END
		,D.PROCONTRO,
		RTRIM(D.CODDCIMED) AS 'Codigo DCI',
		RTRIM(D.ABRPROMEZ) AS 'Descripcion DCI',
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

       end
ELSE 
		if @CentroAtencion is not null and len(rtrim(@CentroAtencion))> 0
		begin
			INSERT INTO @Almacenes
			select Id from Inventory.Warehouse where CodeCenterAttention = @CentroAtencion
		end

		INSERT INTO @Almacenes
		select Id from Inventory.Warehouse where CodeCenterAttention IS NULL

		SELECT MAX(B.CodeCUM)as CodeCUM,sum(isnull(phy.Quantity,0)) as Disponibles, RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Medicamento, D.NOPOSPROD AS 'NO POS',D.TIPFORMED,D.CODGRUFAR,D.CODJUMEES As JustificacionMedicamentosEspeciales
		, C.Antibiotic, ResistenciaBacteriana = CAST(CASE WHEN BRM.Id IS NULL THEN 0 ELSE 1 END AS BIT), ObservationRB = ISNULL(BRM.Observation,''), C.Conditioned, UNIRS = CASE WHEN C.UNIRS = 1 THEN 'Si' ELSE 'No' END
		, PBS = CASE WHEN C.Conditioned = 1 THEN 'Condicionado'
		   WHEN	D.NOPOSPROD = 1 THEN 'No'
		   ELSE 'Si'
		   END
		,D.PROCONTRO,
		RTRIM(D.CODDCIMED) AS 'Codigo DCI',
		RTRIM(dci.DESDCIMED) AS 'Descripcion DCI',
		B.DairyComponent AS 'ComponenteLacteo',
		B.DairyComponentType AS 'TipoComponenteLacteo'
		from 
		dbo.IHLISTPRO D With(Nolock)
		INNER JOIN IHDCIMEDI dci ON dci.CODDCIMED = d.CODDCIMED
		INNER JOIN Inventory.ATC C  With(Nolock) ON RTRIM(D.CODPRODUC) = C.Code and C.DiluentProduct = 0
		LEFT OUTER JOIN Inventory.InventoryProduct B  With(Nolock) on B.Status = 1 AND B.ATCId = C.Id
		LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = B.Id
		AND phy.WarehouseId IN (SELECT Id from @Almacenes)
		LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
		LEFT OUTER JOIN INVENTORY.BacterialResistanceMedication BRM
		ON BRM.AtcId = C.Id AND BRM.StateBRM IN (1,2) AND [Common].[GETDATE]() BETWEEN BRM.StartDate AND BRM.EndDate
		WHERE  D.TIPPRODUC IN ('1', '3') AND D.ESPDILPRO = 0 AND D.PROESTADO = 1 AND D.AddedAutomatic = 0
 		GROUP BY RTRIM(D.CODPRODUC), RTRIM(D.DESPRODUC), D.NOPOSPROD, D.TIPFORMED, D.CODGRUFAR, D.CODJUMEES, C.Antibiotic, dci.DESDCIMED
		, CAST(CASE WHEN BRM.Id IS NULL THEN 0 ELSE 1 END AS BIT), ISNULL(BRM.Observation,''), C.Conditioned, C.UNIRS, D.PROCONTRO, D.CODDCIMED, D.ABRPROMEZ, B.DairyComponent, B.DairyComponentType 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos e insumos disponibles para prescripción médica en historia clínica, filtrando por centro de atención y tipo de atención (extramural o intramural). Consulta el catálogo de productos farmacéuticos (IHLISTPRO) cruzado con la clasificación ATC, el inventario físico por bodega y las bodegas asociadas al centro de atención, para mostrar la cantidad disponible en stock junto con atributos clave del medicamento: nombre, código CUM, código DCI, grupo farmacológico, si es antibiótico, si tiene resistencia bacteriana activa, si es PBS/No PBS/Condicionado, si requiere justificación de medicamentos especiales (UNIRS), y si contiene componente lácteo. Cuando la atención es extramural solo considera bodegas del centro de atención indicado; para atención intramural también suma bodegas sin centro de atención asignado (bodegas generales). Es utilizado por el módulo de prescripción médica para que el profesional de salud seleccione medicamentos con información de disponibilidad real en inventario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosParaPrescripcion';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosParaPrescripcion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos disponibles para prescripción médica con sus cantidades en inventario, ajustando las bodegas consideradas según si la atención es extramural o intramural y exponiendo marcas clínicas (PBS, antibiótico, resistencia bacteriana, DCI, etc.).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El indicador de extramural debe estar definido para escoger la rama de filtrado de bodegas; Para filtrar por centro de atención, el código debe venir informado y no vacío; Deben existir catálogos sincronizados entre IHLISTPRO y ATC por código de producto; En modo intramural, el producto debe tener correspondencia en el catálogo DCI (IHDCIMEDI) por INNER JOIN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan productos activos (PROESTADO = 1) cuyo tipo es ''1'' o ''3'' y que no son diluyentes (ESPDILPRO = 0); Se excluyen productos cuyo ATC esté marcado como diluyente (DiluentProduct = 0); En modo intramural se excluyen productos agregados automáticamente (AddedAutomatic = 0); Solo se consideran productos de inventario con Status = 1; La disponibilidad se calcula únicamente sobre bodegas seleccionadas por el modo de atención; cantidades nulas se tratan como 0; La marca de resistencia bacteriana solo aplica a registros vigentes por fecha y con estado 1 o 2; PBS es ''Condicionado'' si el ATC es condicionado, prevalece sobre el indicador NO POS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Prescripción médica; Medicamento; Atención extramural; Atención intramural; Centro de atención; Bodega/Almacén; Inventario físico (disponibilidad); Clasificación ATC; Antibiótico; Resistencia bacteriana; Justificación de medicamentos especiales; Plan de Beneficios en Salud (PBS/POS); Medicamento condicionado; UNIRS; Componente lácteo; Denominación Común Internacional (DCI); Medicamento de control', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Almacenes: Cuando se informa centro de atención, se cargan los Id de bodegas cuyo CodeCenterAttention coincide con el centro indicado; [INSERT] @Almacenes: En la rama intramural se agregan adicionalmente las bodegas con CodeCenterAttention NULL (bodegas generales); [RETURN_RESULT] dbo.IHLISTPRO: Devuelve catálogo de medicamentos prescribibles con disponibilidad sumada por las bodegas filtradas, indicadores de PBS, antibiótico, resistencia bacteriana, UNIRS, DCI y componente lácteo, según sea atención extramural o intramural', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Atención extramural con centro de atención informado → Restringe el cálculo de disponibilidad únicamente a las bodegas asociadas al centro de atención indicado (no incluye bodegas generales) else Para atención intramural, además de las bodegas del centro de atención (si se informa) también incluye las bodegas sin centro de atención asignado (bodegas generales) y excluye productos marcados como agregados automáticamente; si ATC.Conditioned = 1 → Marca PBS como ''Condicionado'' else Si el producto es NO POS marca PBS=''No''; en caso contrario PBS=''Si''; si Existe registro vigente en BacterialResistanceMedication para el ATC (StateBRM IN (1,2) y fecha actual entre StartDate y EndDate) → Marca el medicamento como con resistencia bacteriana y expone la observación asociada else ResistenciaBacteriana = 0 y observación vacía', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Warehouse; dbo.IHLISTPRO; Inventory.ATC; Inventory.InventoryProduct; Inventory.PhysicalInventory; Inventory.BacterialResistanceMedication; dbo.IHDCIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcion';
-- GO
