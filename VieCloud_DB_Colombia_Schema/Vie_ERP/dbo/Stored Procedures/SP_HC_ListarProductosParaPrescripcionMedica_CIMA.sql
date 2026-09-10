CREATE PROCEDURE [dbo].[SP_HC_ListarProductosParaPrescripcionMedica_CIMA]
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

		SELECT MAX(B.CodeCUM)as CodeCUM,sum(isnull(phy.Quantity,0)) as Disponibles, RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Medicamento, D.NOPOSPROD AS 'NO POS',D.TIPFORMED,D.CODGRUFAR,D.CODJUMEES As JustificacionMedicamentosEspeciales
		, C.Antibiotic, ResistenciaBacteriana = CAST(CASE WHEN BRM.Id IS NULL THEN 0 ELSE 1 END AS BIT), ObservationRB = ISNULL(BRM.Observation,''), C.Conditioned, UNIRS = CASE WHEN C.UNIRS = 1 THEN 'Si' ELSE 'No' END
		, PBS = CASE WHEN C.Conditioned = 1 THEN 'Condicionado'
		   WHEN	D.NOPOSPROD = 1 THEN 'No'
		   ELSE 'Si'
		   END,
		   rtrim(ltrim(dci.CODDCIMED)) as CodigoDCI,
		   rtrim(ltrim(dci.DESDCIMED)) as DescripcionDCI,
		   RTRIM(D.CODPRODUC) + ' - '+ RTRIM(D.DESPRODUC) AS CodigoDescripcionMedicamento
		from 
		dbo.IHLISTPRO D With(Nolock)
		INNER JOIN Inventory.ATC C  With(Nolock) ON RTRIM(D.CODPRODUC) = C.Code and C.DiluentProduct = 0
		LEFT OUTER JOIN Inventory.InventoryProduct B  With(Nolock) on B.Status = 1 AND B.ATCId = C.Id
		LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = B.Id
		AND phy.WarehouseId IN (SELECT Id from @Almacenes)
		LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
		LEFT OUTER JOIN INVENTORY.BacterialResistanceMedication BRM
		ON BRM.AtcId = C.Id AND BRM.StateBRM IN (1,2) AND [Common].[GETDATE]() BETWEEN BRM.StartDate AND BRM.EndDate
		INNER JOIN dbo.IHDCIMEDI DCI on DCI.CODDCIMED = D.CODDCIMED 
		WHERE --D.CODDCIMED = 'DCI0002' AND 
		D.TIPPRODUC IN ('1', '3') AND D.ESPDILPRO = 0 AND D.PROESTADO = 1
 		GROUP BY RTRIM(D.CODPRODUC), RTRIM(D.DESPRODUC), D.NOPOSPROD, D.TIPFORMED, D.CODGRUFAR, D.CODJUMEES
		, C.Antibiotic, CAST(CASE WHEN BRM.Id IS NULL THEN 0 ELSE 1 END AS BIT), ISNULL(BRM.Observation,''), C.Conditioned, C.UNIRS, DCI.CODDCIMED, DCI.DESDCIMED  
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que lista medicamentos disponibles para prescripción médica, filtrando productos activos de tipo farmacéutico (no diluyentes) según almacén y centro de atención. Consolida stock físico disponible por bodega, datos POS/no-POS, clasificación DCI, indicador de antibiótico, resistencia bacteriana vigente y justificación de medicamentos especiales (CIMA), agrupando por producto para exponer cantidad total disponible al módulo de prescripción clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_CIMA';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos disponibles para prescripción médica con su clasificación ATC, disponibilidad por almacén del centro de atención, marcadores POS/PBS, antibiótico, resistencia bacteriana y DCI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir almacenes en Inventory.Warehouse vinculados al centro de atención o sin centro asignado (CodeCenterAttention IS NULL).; Los productos deben tener correspondencia en Inventory.ATC con DiluentProduct=0.; Los productos deben tener un DCI válido en dbo.IHDCIMEDI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre se incluyen los almacenes sin centro de atención en la disponibilidad calculada.; Se excluyen productos diluyentes (DiluentProduct=0 en ATC y ESPDILPRO=0 en IHLISTPRO).; Solo se consideran productos en estado activo (PROESTADO=1).; La vigencia de la resistencia bacteriana se valida contra la fecha del sistema obtenida vía Common.GETDATE().; Las cantidades nulas de inventario físico se tratan como 0 (ISNULL en Quantity).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Prescripción médica; Medicamento; Clasificación ATC; DCI (Denominación Común Internacional); Antibiótico; Resistencia bacteriana; POS / No POS; PBS (Plan de Beneficios en Salud) condicionado; UNIRS; Justificación de medicamentos especiales; Inventario físico por bodega; Centro de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve solo productos con TIPPRODUC IN (''1'',''3''), ESPDILPRO=0 y PROESTADO=1 (productos activos no diluyentes de tipos permitidos).; [RETURN_RESULT] RESULTSET: Las cantidades disponibles se suman únicamente sobre bodegas del centro de atención indicado y bodegas sin centro asignado.; [RETURN_RESULT] RESULTSET: El inventario considerado solo proviene de InventoryProduct con Status=1 (productos activos).; [RETURN_RESULT] RESULTSET: Marca ResistenciaBacteriana=1 cuando existe registro en BacterialResistanceMedication con StateBRM IN (1,2) y la fecha actual está entre StartDate y EndDate; de lo contrario 0.; [RETURN_RESULT] RESULTSET: PBS se calcula como: ''Condicionado'' si ATC.Conditioned=1; ''No'' si NOPOSPROD=1; ''Si'' en cualquier otro caso.; [RETURN_RESULT] RESULTSET: UNIRS se devuelve como ''Si'' cuando ATC.UNIRS=1 y ''No'' en caso contrario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CentroAtencion no es nulo y tiene longitud > 0 → Carga en la tabla temporal de almacenes los Warehouse cuyo CodeCenterAttention coincide con el centro else Solo se cargan los almacenes sin centro asignado; si ATC.Conditioned = 1 → PBS = ''Condicionado'' else Si NOPOSPROD=1 → ''No'', sino ''Si''; si BRM.Id IS NULL (no hay registro vigente de resistencia bacteriana) → ResistenciaBacteriana = 0 y ObservationRB vacío else ResistenciaBacteriana = 1 con la observación correspondiente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Warehouse; dbo.IHLISTPRO; Inventory.ATC; Inventory.InventoryProduct; Inventory.PhysicalInventory; Inventory.BacterialResistanceMedication; dbo.IHDCIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_CIMA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_CIMA';
-- GO
