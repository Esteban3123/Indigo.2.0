
CREATE PROCEDURE [dbo].[SP_HC_ListarProductosParaPrescripcionMedicaConsultaExterna] 

@Almacen char(4),
@Denominacion char(20),
@VersionERP int,
@CentroAtencion char(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
IF @VersionERP=1

				SELECT max( B.IPRCODIGO) as IPRCODIGO,RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,NOPOSPROD AS 'NO POS'
			,SUM(CAST(COALESCE(NULLIF(D.IFICANTID,0),0) AS INT)) AS Disponibles,TIPFORMED,CODGRUFAR,A.CODJUMEES As JustificacionMedicamentosEspeciales
			,Antibiotic = CAST(0 AS BIT), ResistenciaBacteriana = CAST(0 AS BIT), ObservationRB = '', Conditioned = CAST(0 AS BIT), UNIRS = 'No'
			, PBS = CASE WHEN NOPOSPROD = 1 THEN 'No' ELSE 'Si' END
		FROM dbo.IHLISTPRO A 
		inner join dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
	inner join dbo.INPRODUC C ON B.IPRCODIGO=C.IPRCODIGO 
	left outer join dbo.INFISICO D on D.IPRCODIGO=C.IPRCODIGO
				WHERE (c.IPRCODIGO in (select  IPRCODIGO from dbo.INFISICO a inner join
				dbo.INALMACE b on a.IALCODIGO=b.IALCODIGO where b.IALCODIGO=@Almacen) or  c.IPRCODIGO not in (select  IPRCODIGO from dbo.INFISICO)) and
				 CODDCIMED=@Denominacion AND TIPPRODUC IN ('1','3') AND PROESTADO = 1 AND ESPDILPRO='0'
		GROUP BY A.CODPRODUC,DESPRODUC,NOPOSPROD,A.TIPPRODUC,TIPFORMED,CODGRUFAR,A.CODJUMEES
       
       
ELSE  IF @VersionERP=2
	
	
				SELECT  MAX(B.IPRCODIGO) AS IPRCODIGO,RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,NOPOSPROD AS 'NO POS'
			,SUM(CAST(COALESCE(NULLIF(D.IFICANTID,0),0) AS INT)) AS Disponibles,TIPFORMED,CODGRUFAR,A.CODJUMEES As JustificacionMedicamentosEspeciales
			,Antibiotic = CAST(0 AS BIT), ResistenciaBacteriana = CAST(0 AS BIT), ObservationRB = '', Conditioned = CAST(0 AS BIT), UNIRS = 'No'
			, PBS = CASE WHEN NOPOSPROD = 1 THEN 'No' ELSE 'Si' END
		FROM dbo.IHLISTPRO A 
		inner join dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
	inner join dbo.INNPRODUC C ON B.IPRCODIGO=C.IPRCODIGO 
	left outer join dbo.INNFISICO D on D.INNPRODUC=C.OID
				WHERE (c.OID in (select  INNPRODUC from dbo.INNFISICO a inner join
				dbo.INNALMACE b on a.INNALMACE=b.OID where b.IALCODIGO=@Almacen) or  c.OID not in (select  INNPRODUC from dbo.INNFISICO)) and
				 CODDCIMED=@Denominacion AND TIPPRODUC IN ('1','3') AND PROESTADO = 1 AND ESPDILPRO='0'
		GROUP BY A.CODPRODUC,DESPRODUC,NOPOSPROD,A.TIPPRODUC,TIPFORMED,CODGRUFAR, A.CODJUMEES

ELSE

		DECLARE @Almacenes as table
		(Id int)

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
		,D.PROCONTRO ,
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
 		GROUP BY RTRIM(D.CODPRODUC), RTRIM(D.DESPRODUC), D.NOPOSPROD,D.TIPFORMED,D.CODGRUFAR,D.CODJUMEES, C.Antibiotic 
		, CAST(CASE WHEN BRM.Id IS NULL THEN 0 ELSE 1 END AS BIT) , ISNULL(BRM.Observation,''), C.Conditioned, C.UNIRS,D.PROCONTRO , B.DairyComponent, B.DairyComponentType
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos e insumos disponibles para prescripción médica en consulta externa, filtrando por denominación común (DCI) y almacén o centro de atención. Consulta el catálogo maestro de productos farmacéuticos (IHLISTPRO) y cruza con el inventario físico disponible para mostrar las cantidades en stock, la clasificación POS/No POS, si el medicamento requiere justificación de medicamentos especiales (No PBS), si es antibiótico, si tiene resistencia bacteriana activa y si tiene componente lácteo. Soporta tres versiones del ERP (legado v1, legado v2 y versión actual con esquema Inventory), permitiendo que el médico en consulta externa seleccione medicamentos disponibles realmente en el almacén correspondiente al centro de atención para generar una receta o prescripción médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExterna';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExterna';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos/medicamentos disponibles para prescripción médica en consulta externa, según una denominación común internacional (DCI), filtrando por almacén o centro de atención y adaptando la consulta a la versión del ERP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe indicar la denominación (DCI) del medicamento a buscar.; Para versiones ERP 1 y 2 se requiere el código de almacén.; Para la versión por defecto (no 1 ni 2), si se desea filtrar por centro de atención, debe enviarse no nulo y no vacío.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven productos con TIPPRODUC en (''1'',''3''), PROESTADO = 1 y ESPDILPRO = ''0'' (no diluyentes, activos).; En la versión nueva, solo se consideran productos ATC con DiluentProduct = 0.; En la versión nueva, los productos de inventario considerados deben tener Status = 1.; Las cantidades nulas o cero se tratan como 0 al sumar disponibilidades.; En versiones 1 y 2, los flags Antibiotic, ResistenciaBacteriana y Conditioned siempre se devuelven en falso y UNIRS siempre como ''No''.; En la versión nueva, solo se suman existencias de almacenes pertenecientes al centro de atención solicitado (no se mezclan con otros centros).; Productos sin existencias físicas registradas también pueden listarse (versiones 1 y 2 incluyen productos no presentes en INFISICO/INNFISICO).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Prescripción médica; Consulta externa; Medicamento; Denominación Común Internacional (DCI); Plan de Beneficios en Salud (PBS/POS); Justificación de medicamentos especiales; Antibiótico; Resistencia bacteriana; Medicamento condicionado; UNIRS; Componente lácteo; Almacén/Bodega de inventario; Centro de atención; Inventario físico; Clasificación ATC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Cuando @VersionERP=1, retorna productos desde IHLISTPRO/INPRODUC/INFISICO filtrando existencias del almacén indicado o productos sin existencias físicas, con flags de antibiótico/resistencia bacteriana en falso.; [RETURN_RESULT] RESULTSET: Cuando @VersionERP=2, retorna productos desde IHLISTPRO/INNPRODUC/INNFISICO usando OID y INNALMACE para filtrar por almacén; flags de antibiótico/resistencia en falso.; [RETURN_RESULT] RESULTSET: Cuando @VersionERP no es 1 ni 2, retorna productos desde IHLISTPRO con clasificación ATC (Inventory.ATC), sumando inventario físico solo de almacenes del centro de atención indicado, marcando antibiótico, resistencia bacteriana vigente, condicionado, UNIRS y componente lácteo.; [INSERT] @Almacenes: Cuando @CentroAtencion no es nulo ni vacío, se insertan los Id de Inventory.Warehouse cuyo CodeCenterAttention coincide con el centro indicado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @VersionERP = 1 → Consulta el catálogo legado INPRODUC/INFISICO/INALMACE para obtener productos y disponibilidad por almacén.; si @VersionERP = 2 → Consulta el catálogo legado alterno INNPRODUC/INNFISICO/INNALMACE (basado en OID) para obtener productos y disponibilidad.; si @VersionERP distinto de 1 y 2 → Consulta el modelo nuevo Inventory.ATC/InventoryProduct/PhysicalInventory restringiendo a almacenes del centro de atención y enriquece con datos de antibiótico, resistencia bacteriana y componente lácteo.; si @CentroAtencion no nulo y con longitud > 0 (rama por defecto) → Carga en @Almacenes los almacenes asociados a ese centro de atención; de lo contrario la tabla queda vacía y no se sumarán existencias.; si NOPOSPROD = 1 (versión 1 y 2) / D.NOPOSPROD = 1 (versión nueva) → El campo PBS se reporta como ''No'' (no incluido en plan de beneficios).; si C.Conditioned = 1 (versión nueva) → PBS se reporta como ''Condicionado'', prevaleciendo sobre la regla NOPOSPROD.; si Existe registro en BacterialResistanceMedication con StateBRM IN (1,2) y fecha actual entre StartDate y EndDate → Se marca ResistenciaBacteriana = 1 y se devuelve la observación correspondiente. else ResistenciaBacteriana = 0 y observación vacía.; si C.UNIRS = 1 → UNIRS se devuelve como ''Si'' else UNIRS se devuelve como ''No''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHLISTPRO; dbo.IHRINDDGH; dbo.INPRODUC; dbo.INFISICO; dbo.INALMACE; dbo.INNPRODUC; dbo.INNFISICO; dbo.INNALMACE; Inventory.Warehouse; Inventory.ATC; Inventory.InventoryProduct; Inventory.PhysicalInventory; Inventory.BacterialResistanceMedication', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedicaConsultaExterna';
-- GO
