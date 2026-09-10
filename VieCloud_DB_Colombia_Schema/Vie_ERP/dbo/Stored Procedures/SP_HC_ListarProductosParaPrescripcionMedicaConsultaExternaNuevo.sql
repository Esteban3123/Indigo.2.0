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
		  AND D.CODDCIMED = 'MED-ADICIONAL'
		  AND D.AddedAutomatic = 1 
		GROUP BY 
		  RTRIM(D.CODPRODUC),RTRIM(D.DESPRODUC), D.NOPOSPROD, D.TIPFORMED, D.CODGRUFAR, D.CODJUMEES, D.PROCONTRO, D.CODDCIMED,D.ABRPROMEZ    

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos e insumos disponibles para prescripción médica en consulta externa, filtrando por centro de atención o bodega específica. Combina el catálogo maestro de productos farmacéuticos (IHLISTPRO) con la clasificación ATC, el inventario físico real por bodega y la información de resistencia bacteriana vigente, mostrando para cada medicamento su disponibilidad en stock, código CUM, clasificación PBS/No POS/Condicionado, si es antibiótico, si tiene resistencia bacteriana activa, y si contiene componente lácteo. Incluye además, mediante un segundo bloque UNION ALL, los medicamentos adicionales creados manualmente por médicos (por ejemplo preparados magistrales o productos naturales) que no tienen código DCI asignado en el sistema. Existe para alimentar el selector de medicamentos en el módulo de prescripción médica de consulta externa, garantizando que el profesional de salud vea únicamente productos activos, no diluyentes, con su stock actualizado y alertas clínicas relevantes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExternaNuevo';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExternaNuevo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos/medicamentos disponibles para prescripción médica en consulta externa, combinando productos del inventario (con stock por almacenes del centro de atención) y medicamentos adicionales creados manualmente por médicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExternaNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Si se recibe centro de atención no nulo y no vacío, se obtienen los almacenes asociados a ese centro para calcular disponibilidad; Los productos deben estar activos (PROESTADO = 1) y no ser diluyentes (ESPDILPRO = 0); Para el primer bloque, el ATC asociado debe no ser diluyente (DiluentProduct = 0)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExternaNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La resistencia bacteriana solo se considera vigente cuando el registro está en estado 1 o 2 y la fecha actual está entre StartDate y EndDate; Los medicamentos adicionales siempre se reportan con disponibilidad 0, sin CUM, sin antibiótico, sin condicionamiento y PBS=''No''; Los productos diluyentes (ESPDILPRO=1 o DiluentProduct=1) nunca se incluyen; Productos inactivos (PROESTADO≠1) nunca se incluyen; La disponibilidad solo agrega stock de almacenes pertenecientes al centro de atención indicado; Los medicamentos adicionales se identifican por CODDCIMED=''MED-ADICIONAL'' y AddedAutomatic=1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExternaNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Prescripción médica; Consulta externa; Medicamentos POS/No POS; PBS (Plan de Beneficios en Salud); Antibióticos; Resistencia bacteriana; Medicamentos condicionados; Medicamentos UNIRS; Justificación de medicamentos especiales; Clasificación ATC; Componente lácteo; Medicamentos adicionales (creados por médicos); Inventario físico por bodega; Centro de atención; Código DCI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExternaNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Almacenes: Cuando el centro de atención es no nulo y de longitud > 0, se insertan los Ids de almacenes cuyo CodeCenterAttention coincide con el parámetro; [RETURN_RESULT] RESULTSET: Devuelve productos tipo ''1'' o ''3'', no diluyentes y activos, con disponibilidad sumada solo de los almacenes del centro de atención; unido con medicamentos adicionales (TIPPRODUC=1, CODDCIMED=''MED-ADICIONAL'', AddedAutomatic=1) con disponibilidad 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExternaNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CentroAtencion no nulo y len(rtrim) > 0 → Carga la tabla temporal de almacenes filtrando por CodeCenterAttention else La tabla de almacenes queda vacía y por tanto la disponibilidad calculada será 0 para todos los productos; si BRM.Id IS NULL → ResistenciaBacteriana = 0 else ResistenciaBacteriana = 1; si C.Conditioned = 1 → PBS = ''Condicionado'' else Si NOPOSPROD=1 entonces PBS=''No'', sino PBS=''Si''; si C.UNIRS = 1 → UNIRS = ''Si'' else UNIRS = ''No''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExternaNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExternaNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Warehouse; dbo.IHLISTPRO; Inventory.ATC; Inventory.InventoryProduct; Inventory.PhysicalInventory; Inventory.BacterialResistanceMedication', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExternaNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExternaNuevo';
-- GO
