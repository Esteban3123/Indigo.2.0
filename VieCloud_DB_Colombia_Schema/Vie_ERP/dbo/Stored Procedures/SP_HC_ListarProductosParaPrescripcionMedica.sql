CREATE PROCEDURE [dbo].[SP_HC_ListarProductosParaPrescripcionMedica]
(
@Almacen char(4),
@Denominacion char(20),
@VersionERP int,
@CentroAtencion char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

	
	
	--fox
IF @VersionERP=1

				SELECT  max(B.IPRCODIGO),RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,NOPOSPROD AS 'NO POS'
			,SUM(CAST(COALESCE(NULLIF(D.IFICANTID,0),0) AS INT)) AS Disponibles,TIPFORMED,CODGRUFAR,A.CODJUMEES As JustificacionMedicamentosEspeciales
			,Antibiotic = CAST(0 AS BIT), ResistenciaBacteriana = CAST(0 AS BIT), ObservationRB = '', Conditioned = CAST(0 AS BIT), UNIRS = 'No'
			, PBS = CASE WHEN NOPOSPROD = 1 THEN 'No' ELSE 'Si' END
		FROM dbo.IHLISTPRO A 
		inner join dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
	inner join dbo.INPRODUC C ON B.IPRCODIGO=C.IPRCODIGO 
	left outer join dbo.INFISICO D on D.IPRCODIGO=C.IPRCODIGO 
	inner join dbo.INALMACE E ON D.IALCODIGO=E.IALCODIGO
					WHERE CODDCIMED=@Denominacion AND TIPPRODUC IN ('1','3') AND PROESTADO = 1 AND ESPDILPRO='0' AND E.IALCODIGO=@Almacen
		GROUP BY A.CODPRODUC,DESPRODUC,NOPOSPROD,A.TIPPRODUC,TIPFORMED,CODGRUFAR,A.CODJUMEES
       
       --net
ELSE IF @VersionERP=2
	
	
				SELECT MAX( B.IPRCODIGO) AS IPRCODIGO ,RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,NOPOSPROD AS 'NO POS'
			,SUM(CAST(COALESCE(NULLIF(D.IFICANTID,0),0) AS INT)) AS Disponibles,TIPFORMED,CODGRUFAR,A.CODJUMEES As JustificacionMedicamentosEspeciales
			,Antibiotic = CAST(0 AS BIT), ResistenciaBacteriana = CAST(0 AS BIT), ObservationRB = '', Conditioned = CAST(0 AS BIT), UNIRS = 'No'
			, PBS = CASE WHEN NOPOSPROD = 1 THEN 'No' ELSE 'Si' END
		FROM dbo.IHLISTPRO A 
		inner join dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
	inner join dbo.INNPRODUC C ON B.IPRCODIGO=C.IPRCODIGO 
	left outer join dbo.INNFISICO D on D.INNPRODUC=C.OID
				WHERE (c.OID in (select  INNPRODUC from dbo.INNFISICO a inner join
				dbo.INNALMACE b on a.INNALMACE=b.OID /*where b.IALCODIGO=@Almacen*/) or  c.OID not in (select  INNPRODUC from dbo.INNFISICO)) and
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

		SELECT MAX(B.CodeCUM)as CodeCUM,sum(isnull(phy.Quantity,0)) as Disponibles, RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Medicamento, D.NOPOSPROD AS 'NO POS',D.TIPFORMED,D.CODGRUFAR,D.CODJUMEES As JustificacionMedicamentosEspeciales
		, C.Antibiotic, ResistenciaBacteriana = CAST(CASE WHEN BRM.Id IS NULL THEN 0 ELSE 1 END AS BIT), ObservationRB = ISNULL(BRM.Observation,''), C.Conditioned, UNIRS = CASE WHEN C.UNIRS = 1 THEN 'Si' ELSE 'No' END
		, PBS = CASE WHEN C.Conditioned = 1 THEN 'Condicionado'
		   WHEN	D.NOPOSPROD = 1 THEN 'No'
		   ELSE 'Si'
		   END
		,D.PROCONTRO,
		B.DairyComponent AS 'ComponenteLacteo',
		B.DairyComponentType AS 'TipoComponenteLacteo', CONCAT(RTRIM(D.CODPRODUC) , '-', RTRIM(D.DESPRODUC)) AS 'DescripcionProducto'
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
 		GROUP BY RTRIM(D.CODPRODUC), RTRIM(D.DESPRODUC), D.NOPOSPROD, D.TIPFORMED, D.CODGRUFAR, D.CODJUMEES
		, C.Antibiotic, CAST(CASE WHEN BRM.Id IS NULL THEN 0 ELSE 1 END AS BIT), ISNULL(BRM.Observation,''), C.Conditioned, C.UNIRS, D.PROCONTRO, B.DairyComponent, B.DairyComponentType 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos e insumos disponibles para prescripción médica, filtrando por denominación común internacional (DCI) y almacén o centro de atención. Consulta el catálogo maestro de productos farmacéuticos (IHLISTPRO) cruzado con el inventario físico para mostrar cantidades disponibles, forma farmacéutica, grupo farmacológico, si el medicamento está en el PBS (antes POS), si requiere justificación de medicamento especial (CODJUMEES), si es antibiótico, si tiene resistencia bacteriana activa y si contiene componente lácteo. Soporta tres versiones del ERP (versión legada FoxPro, versión .NET intermedia y versión actual con módulo Inventory), adaptando las consultas al modelo de datos correspondiente según el parámetro @VersionERP. Es usado por el módulo de historia clínica cuando el profesional de salud va a generar una receta o prescripción médica y necesita buscar y seleccionar los productos disponibles en farmacia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosParaPrescripcionMedica';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosParaPrescripcionMedica';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos/medicamentos disponibles para prescripción médica filtrados por denominación (DCI) y almacén/centro de atención, adaptando la consulta según la versión del ERP (Fox, .Net o actual).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe indicar la denominación del medicamento (CODDCIMED) para filtrar el catálogo; Para versiones ERP 1 y 2 debe existir relación entre IHLISTPRO, IHRINDDGH y el catálogo de productos correspondiente (INPRODUC o INNPRODUC); Para la versión actual (ELSE), el producto debe existir en Inventory.ATC con DiluentProduct=0; Solo se consideran productos con PROESTADO=1 (activos), TIPPRODUC en (''1'',''3'') y ESPDILPRO=''0'' (no son diluyentes/especiales)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan productos activos (PROESTADO=1); Solo se incluyen tipos de producto ''1'' y ''3''; Se excluyen productos marcados como diluyentes o especiales (ESPDILPRO=''0''); En el modelo Inventory se excluyen productos ATC marcados como diluyentes (DiluentProduct=0); La disponibilidad se calcula sumando cantidades físicas, tratando NULL/0 como 0; PBS=''Condicionado'' tiene prioridad sobre la marca POS/No POS en la versión actual; La resistencia bacteriana solo se considera vigente si la fecha actual está dentro del rango de validez y el estado es 1 o 2; Siempre se incluyen los almacenes sin centro de atención asignado en el conjunto de búsqueda', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Prescripción médica; Medicamento; Denominación Común Internacional (DCI); Plan de Beneficios en Salud (PBS/POS); Antibiótico; Resistencia bacteriana; Medicamento condicionado; Justificación de medicamentos especiales; UNIRS; Componente lácteo; Almacén/bodega; Centro de atención; Clasificación ATC; Inventario físico/disponibilidad; Código CUM', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Almacenes: Cuando @CentroAtencion no es nulo y tiene longitud > 0, se insertan los Id de Inventory.Warehouse cuyo CodeCenterAttention coincide con el centro indicado; [INSERT] @Almacenes: Siempre se insertan los Id de Inventory.Warehouse cuyo CodeCenterAttention es NULL (almacenes sin centro asignado); [RETURN_RESULT] resultset: Cuando @VersionERP=1 retorna productos del ERP Fox con disponibilidad sumada desde INFISICO restringida al @Almacen recibido; [RETURN_RESULT] resultset: Cuando @VersionERP=2 retorna productos del ERP .Net incluyendo aquellos con stock en INNFISICO o sin registro físico (c.OID not in INNFISICO); [RETURN_RESULT] resultset: Cuando @VersionERP no es 1 ni 2, retorna productos del modelo Inventory con disponibilidad sumada solo de los almacenes del centro de atención y los almacenes sin centro asignado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @VersionERP = 1 → Ejecuta consulta para ERP Fox usando INPRODUC/INFISICO/INALMACE filtrando por @Almacen; marca PBS=''No'' si NOPOSPROD=1 y ''Si'' en caso contrario; campos clínicos (Antibiotic, ResistenciaBacteriana, Conditioned) se entregan en cero/falso por defecto; si @VersionERP = 2 → Ejecuta consulta para ERP .Net usando INNPRODUC/INNFISICO/INNALMACE; incluye productos sin existencias físicas registradas; el filtro por almacén está comentado/desactivado; si @VersionERP distinto de 1 y 2 → Ejecuta consulta sobre el modelo Inventory (ATC, InventoryProduct, PhysicalInventory, Warehouse) usando los almacenes del @CentroAtencion más los sin centro, calculando antibióticos, resistencia bacteriana vigente y componente lácteo; si @CentroAtencion no nulo y longitud > 0 → Agrega a @Almacenes los almacenes asociados a ese centro de atención else Solo se agregan los almacenes con CodeCenterAttention NULL; si C.Conditioned = 1 → PBS se reporta como ''Condicionado'' else Si NOPOSPROD=1 → ''No''; en otro caso ''Si''; si Existe registro vigente en BacterialResistanceMedication (StateBRM IN (1,2) y fecha actual entre StartDate y EndDate) → ResistenciaBacteriana = 1 y se entrega la observación BRM.Observation else ResistenciaBacteriana = 0 y observación vacía; si C.UNIRS = 1 → UNIRS se reporta como ''Si'' else UNIRS se reporta como ''No''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHLISTPRO; dbo.IHRINDDGH; dbo.INPRODUC; dbo.INFISICO; dbo.INALMACE; dbo.INNPRODUC; dbo.INNFISICO; dbo.INNALMACE; Inventory.Warehouse; Inventory.ATC; Inventory.InventoryProduct; Inventory.PhysicalInventory; Inventory.BacterialResistanceMedication', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica';
-- GO
