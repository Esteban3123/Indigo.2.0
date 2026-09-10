
CREATE PROCEDURE [dbo].[SP_HC_ListarProductosParaPrescripcionMedicaParametrosHistoria]
(
@Denominacion char(20),
@VersionERP int,
@CentroAtencion char(20)
)
AS
BEGIN
	SET NOCOUNT ON;
	      
   --net
 IF @VersionERP=2
	
	
				SELECT MAX( B.IPRCODIGO) AS IPRCODIGO ,RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,NOPOSPROD AS 'NO POS'
			,SUM(CAST(COALESCE(NULLIF(D.IFICANTID,0),0) AS INT)) AS Disponibles,TIPFORMED,CODGRUFAR,A.CODJUMEES As JustificacionMedicamentosEspeciales
			,Antibiotic = CAST(0 AS BIT), ResistenciaBacteriana = CAST(0 AS BIT), ObservationRB = '', Conditioned = CAST(0 AS BIT), UNIRS = 'No'
			, PBS = CASE WHEN NOPOSPROD = 1 THEN 'No' ELSE 'Si' END
		FROM dbo.IHLISTPRO A 
		inner join dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
	inner join dbo.INNPRODUC C ON B.IPRCODIGO=C.IPRCODIGO 
	left outer join dbo.INNFISICO D on D.INNPRODUC=C.OID
				WHERE (c.OID in (select  INNPRODUC from dbo.INNFISICO a inner join
				dbo.INNALMACE b on a.INNALMACE=b.OID) or  c.OID not in (select  INNPRODUC from dbo.INNFISICO)) and
				 CODDCIMED=@Denominacion AND TIPPRODUC IN ('1','3') AND PROESTADO = 1 AND ESPDILPRO='0'
		GROUP BY A.CODPRODUC,DESPRODUC,NOPOSPROD,A.TIPPRODUC,TIPFORMED,CODGRUFAR,A.CODJUMEES
       
ELSE

	DECLARE @Almacenes as table
	(Id int)

	if @CentroAtencion is not null and len(rtrim(@CentroAtencion))> 0
	begin
		INSERT INTO @Almacenes
		select Id from Inventory.Warehouse where CodeCenterAttention = @CentroAtencion
	end

	INSERT INTO @Almacenes
	select Id from Inventory.Warehouse where CodeCenterAttention IS NULL

	SELECT MAX(B.CodeCUM)as CodeCUM,RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Medicamento, D.NOPOSPROD AS 'NO POS',sum(isnull(phy.Quantity,0)) as Disponibles,D.TIPFORMED,D.CODGRUFAR,D.CODJUMEES As JustificacionMedicamentosEspeciales, C.Antibiotic
	, ResistenciaBacteriana = CAST(CASE WHEN BRM.Id IS NULL THEN 0 ELSE 1 END AS BIT), ObservationRB = ISNULL(BRM.Observation,''), C.Conditioned, UNIRS = CASE WHEN C.UNIRS = 1 THEN 'Si' ELSE 'No' END
	, PBS = CASE WHEN C.Conditioned = 1 THEN 'Condicionado'
		   WHEN	D.NOPOSPROD = 1 THEN 'No'
		   ELSE 'Si'
		   END
	,D.PROCONTRO, CONCAT(RTRIM(D.CODPRODUC) , '-', RTRIM(D.DESPRODUC)) AS 'DescripcionProducto' 
	from 
	dbo.IHLISTPRO D With(Nolock)
	INNER JOIN Inventory.ATC C  With(Nolock) ON RTRIM(D.CODPRODUC) = C.Code and C.DiluentProduct = 0
	LEFT OUTER JOIN Inventory.InventoryProduct B  With(Nolock) on B.Status = 1 AND B.ATCId = C.Id
	LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = B.Id
	AND phy.WarehouseId IN (SELECT Id from @Almacenes)
	LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
	LEFT OUTER JOIN INVENTORY.BacterialResistanceMedication BRM
	ON BRM.AtcId = C.Id AND BRM.StateBRM IN (1,2) AND [Common].[GETDATE]() BETWEEN BRM.StartDate AND BRM.EndDate
	WHERE D.CODDCIMED = @Denominacion AND 
	D.TIPPRODUC IN ('1', '3') AND D.ESPDILPRO = 0 AND D.PROESTADO = 1
 	GROUP BY RTRIM(D.CODPRODUC), RTRIM(D.DESPRODUC), D.NOPOSPROD,D.TIPFORMED,D.CODGRUFAR,D.CODJUMEES, C.Antibiotic
	,CAST(CASE WHEN BRM.Id IS NULL THEN 0 ELSE 1 END AS BIT), ISNULL(BRM.Observation,''), C.Conditioned, C.UNIRS, D.PROCONTRO 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos e insumos disponibles para prescripción médica en la historia clínica, filtrando por denominación común (DCI) y centro de atención. Consulta el catálogo maestro de productos farmacéuticos (IHLISTPRO) cruzado con el catálogo ATC, y calcula el stock disponible en las bodegas del centro de atención indicado. Según la versión del ERP, usa la lógica del modelo clásico (IHRINDDGH/INNFISICO) o el modelo nuevo de inventario (Inventory.ATC, Inventory.InventoryProduct, Inventory.PhysicalInventory). Devuelve información clave para la receta médica: código del medicamento, nombre, si es POS o no POS, justificación de medicamentos especiales, si es antibiótico, si tiene restricción por resistencia bacteriana y las unidades disponibles en bodega.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosParaPrescripcionMedicaParametrosHistoria';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosParaPrescripcionMedicaParametrosHistoria';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos disponibles para prescripción médica filtrados por denominación (DCI), mostrando existencias por almacén, clasificación POS/PBS, antibiótico, resistencia bacteriana y justificación de medicamentos especiales, con dos variantes según la versión del ERP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaParametrosHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe indicar la denominación común (DCI) del medicamento a buscar.; La versión del ERP determina la fuente de datos: 2 usa el modelo legado (INN*), distinto de 2 usa el modelo de Inventory.; Para la rama no-legada, si se indica centro de atención debe existir al menos un almacén asociado en Inventory.Warehouse.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaParametrosHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan productos con PROESTADO = 1 (productos activos).; Solo se incluyen tipos de producto ''1'' y ''3''.; Se excluyen productos marcados como diluyentes (ESPDILPRO = ''0'' / DiluentProduct = 0).; Las existencias nulas se tratan como cero al sumar disponibilidad.; En la rama Inventory, la disponibilidad se restringe a almacenes del centro de atención y/o sin centro asignado.; La resistencia bacteriana solo aplica si está vigente (fecha actual entre StartDate y EndDate) y en estados 1 o 2.; El producto debe coincidir exactamente con la denominación común (CODDCIMED = @Denominacion).; En la rama Inventory se considera únicamente InventoryProduct con Status = 1.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaParametrosHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Prescripción médica; Medicamento; Denominación común internacional (DCI); Producto POS / PBS; Antibiótico; Resistencia bacteriana; Medicamento condicionado; Justificación de medicamentos especiales; Inventario físico por bodega; Centro de atención; Clasificación ATC; Producto diluyente; UNIRS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaParametrosHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Cuando @VersionERP=2: retorna productos tipo ''1'' o ''3'', con PROESTADO=1 y ESPDILPRO=''0'', cuya denominación coincide con @Denominacion, sumando existencias físicas; campos clínicos (Antibiotic, ResistenciaBacteriana, Conditioned) se devuelven en valores por defecto (0/No).; [RETURN_RESULT] RESULTSET: Cuando @VersionERP<>2: retorna productos tipo ''1'' o ''3'' con PROESTADO=1 y ESPDILPRO=0 cruzando con Inventory.ATC (DiluentProduct=0), sumando existencias físicas solo de almacenes filtrados, e informando antibiótico, resistencia bacteriana vigente y condición PBS.; [INSERT] @Almacenes: Si @CentroAtencion no es nulo y no está vacío, inserta los Id de Inventory.Warehouse cuyo CodeCenterAttention coincide con @CentroAtencion.; [INSERT] @Almacenes: Siempre inserta los Id de Inventory.Warehouse cuyo CodeCenterAttention es NULL (almacenes generales/sin centro).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaParametrosHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @VersionERP = 2 → Consulta el modelo legado (IHLISTPRO + IHRINDDGH + INNPRODUC + INNFISICO + INNALMACE) y retorna campos clínicos por defecto. else Consulta el modelo Inventory (ATC, InventoryProduct, PhysicalInventory, Warehouse, BacterialResistanceMedication) filtrando existencias por almacenes calculados.; si @CentroAtencion no nulo y con longitud > 0 → Agrega a la tabla de almacenes los Warehouse del centro de atención indicado. else Solo se incluirán los almacenes con CodeCenterAttention NULL.; si NOPOSPROD = 1 (rama legada) → PBS = ''No''. else PBS = ''Si''.; si C.Conditioned = 1 (rama Inventory) → PBS = ''Condicionado''. else Si NOPOSPROD=1 → PBS=''No''; en caso contrario PBS=''Si''.; si Existe registro en BacterialResistanceMedication con StateBRM IN (1,2) y fecha actual entre StartDate y EndDate → ResistenciaBacteriana = 1 y se devuelve la observación asociada. else ResistenciaBacteriana = 0 y observación vacía.; si C.UNIRS = 1 (rama Inventory) → UNIRS = ''Si''. else UNIRS = ''No''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaParametrosHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHLISTPRO; dbo.IHRINDDGH; dbo.INNPRODUC; dbo.INNFISICO; dbo.INNALMACE; Inventory.Warehouse; Inventory.ATC; Inventory.InventoryProduct; Inventory.PhysicalInventory; INVENTORY.BacterialResistanceMedication', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaParametrosHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaParametrosHistoria';
-- GO
