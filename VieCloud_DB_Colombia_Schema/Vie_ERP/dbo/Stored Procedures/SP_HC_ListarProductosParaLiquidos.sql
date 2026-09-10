

CREATE PROCEDURE [dbo].[SP_HC_ListarProductosParaLiquidos]
(
@Almacen CHAR(4),
@CodigoDenominacion CHAR(20),
@VersionERP INT,
@CentroAtencion char(20)
)
AS
BEGIN
	SET NOCOUNT ON;
	
IF @VersionERP=1

		SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,NOPOSPROD AS 'NO POS',SUM(CAST(C.IFICANTID AS INT)) AS Disponibles,TIPFORMED,CODGRUFAR,CODUNIPES AS UnidadPeso,CODUNIVOL AS UnidadVolumen,RTRIM(ABRPROMEZ) AS AbreviaturaProducto,VOLTOTMED AS TotalVolumen,VOLTOTMED,PESTOTMED,TIEESTMED,CAST(0 AS BIT) AS FormatoNOPOS 
		,CAST(0 AS BIT) TODASPATO, Conditioned = CAST(0 AS BIT), UNIRS = 'No', PBS = CASE WHEN NOPOSPROD = 1 THEN 'No' ELSE 'Si' END,CODFORMED
		FROM dbo.IHLISTPRO A 
		INNER JOIN dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
		INNER JOIN dbo.INFISICO C ON B.IPRCODIGO=C.IPRCODIGO
		WHERE C.IALCODIGO=@Almacen AND ESPDILPRO=1 AND TIPPRODUC IN ('1','3') AND CODDCIMED=@CodigoDenominacion AND PROESTADO = 1
		GROUP BY A.CODPRODUC,DESPRODUC,NOPOSPROD,TIPFORMED,CODGRUFAR,CODUNIPES ,CODUNIVOL ,ABRPROMEZ ,VOLTOTMED ,VOLTOTMED,PESTOTMED,TIEESTMED 
	
ELSE IF @VersionERP=2

		SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,NOPOSPROD AS 'NO POS',SUM(CAST(D.IFICANTID AS INT)) AS Disponibles,TIPFORMED,CODGRUFAR,CODUNIPES AS UnidadPeso,CODUNIVOL AS UnidadVolumen,RTRIM(ABRPROMEZ) AS AbreviaturaProducto,VOLTOTMED AS TotalVolumen,VOLTOTMED,PESTOTMED,TIEESTMED,CAST(0 AS BIT) AS FormatoNOPOS 
		,CAST(0 AS BIT) TODASPATO, Conditioned = CAST(0 AS BIT), UNIRS = 'No', PBS = CASE WHEN NOPOSPROD = 1 THEN 'No' ELSE 'Si' END,CODFORMED
		FROM dbo.IHLISTPRO A 
		INNER JOIN dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
		INNER JOIN dbo.INNPRODUC C ON B.IPRCODIGO=C.IPRCODIGO 
		INNER JOIN dbo.INNFISICO D ON D.INNPRODUC=C.OID 
		INNER JOIN dbo.INNALMACE E ON D.INNALMACE=E.OID 
		WHERE E.IALCODIGO=@Almacen AND ESPDILPRO=1 AND TIPPRODUC IN ('1','3') AND CODDCIMED=@CodigoDenominacion AND PROESTADO = 1
		GROUP BY A.CODPRODUC,DESPRODUC,NOPOSPROD,TIPFORMED,CODGRUFAR,CODUNIPES ,CODUNIVOL ,ABRPROMEZ ,VOLTOTMED ,VOLTOTMED,PESTOTMED,TIEESTMED 
	
ELSE

		/*SELECT RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Medicamento,D.NOPOSPROD AS 'NO POS',SUM(CAST(A.Quantity AS INT)) AS Disponibles,D.TIPFORMED,D.CODGRUFAR,D.CODUNIPES AS UnidadPeso,D.CODUNIVOL AS UnidadVolumen,
		RTRIM(D.ABRPROMEZ) AS AbreviaturaProducto,D.VOLTOTMED AS TotalVolumen,D.VOLTOTMED,D.PESTOTMED,D.TIEESTMED,CAST(0 AS BIT) AS FormatoNOPOS
		FROM Inventory.PhysicalInventory AS A 
		INNER JOIN Inventory.InventoryProduct AS B ON B.Id =  A.ProductId
		INNER JOIN Inventory.ATC AS C ON C.Id = B.ATCId
		INNER JOIN dbo.IHLISTPRO AS D ON D.CODPRODUC = C.Code
		INNER JOIN Inventory.Warehouse AS E ON A.WarehouseId = E.Id
		where E.Code = @Almacen AND D.ESPDILPRO = 1 AND D.TIPPRODUC IN ('1','3') AND  D.CODDCIMED = @CodigoDenominacion  AND D.PROESTADO = 1   
		GROUP BY D.CODPRODUC,D.DESPRODUC,D.NOPOSPROD,D.TIPFORMED,D.CODGRUFAR,D.CODUNIPES ,D.CODUNIVOL ,D.ABRPROMEZ ,D.VOLTOTMED ,D.VOLTOTMED,D.PESTOTMED,D.TIEESTMED */

		--SELECT Codigo,Medicamento,[NO POS],sum(ISNULL(Quantity,0)) AS Disponibles,TIPFORMED,CODGRUFAR,UnidadPeso,UnidadVolumen,AbreviaturaProducto,TotalVolumen,VOLTOTMED,PESTOTMED,TIEESTMED,FormatoNOPOS
		--FROM (
		--SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Medicamento,D.NOPOSPROD AS 'NO POS',D.TIPFORMED,D.CODGRUFAR,D.CODUNIPES AS UnidadPeso,D.CODUNIVOL AS UnidadVolumen,
		--		RTRIM(D.ABRPROMEZ) AS AbreviaturaProducto,D.VOLTOTMED AS TotalVolumen,D.VOLTOTMED,D.PESTOTMED,D.TIEESTMED,CAST(0 AS BIT) AS FormatoNOPOS
		--	FROM Inventory.InventoryProduct AS B 
		--		 INNER JOIN Inventory.ATC AS C ON C.Id = B.ATCId
		--		 INNER JOIN dbo.IHLISTPRO AS D ON D.CODPRODUC = C.Code
		--	WHERE D.CODDCIMED = @CodigoDenominacion AND D.ESPDILPRO = 1 AND D.TIPPRODUC IN ('1','3') AND D.PROESTADO = 1    
		--	GROUP BY D.CODPRODUC,D.DESPRODUC,D.NOPOSPROD,D.TIPFORMED,D.CODGRUFAR,D.CODUNIPES ,D.CODUNIVOL ,D.ABRPROMEZ ,D.VOLTOTMED ,D.VOLTOTMED,D.PESTOTMED,D.TIEESTMED 
		--) AS PRO
		--		LEFT JOIN (
		--		SELECT ATC.Code,Quantity FROM Inventory.PhysicalInventory phy
		--		INNER JOIN Inventory.Warehouse AS E ON phy.WarehouseId = E.Id
		--		INNER JOIN Inventory.InventoryProduct invpro ON invpro.Id = phy.ProductId
		--		INNER JOIN Inventory.ATC atc ON atc.Id = invpro.ATCId
		--		WHERE E.Code = @Almacen AND invpro.Status = 1  AND atc.DiluentProduct = 1) AS INV ON PRO.Codigo = INV.Code
		--		group by Codigo,Medicamento,[NO POS],TIPFORMED,CODGRUFAR,UnidadPeso,UnidadVolumen,AbreviaturaProducto,TotalVolumen,VOLTOTMED,PESTOTMED,TIEESTMED,FormatoNOPOS

		--DECLARE @CentroAtencion char(20)

		DECLARE @Almacenes as table
		(Id int)

		if @CentroAtencion is not null and len(rtrim(@CentroAtencion))> 0
		begin
			INSERT INTO @Almacenes
			select Id from Inventory.Warehouse where CodeCenterAttention = @CentroAtencion
		end

		INSERT INTO @Almacenes
		select Id from Inventory.Warehouse where CodeCenterAttention IS NULL

		SELECT RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Medicamento,D.NOPOSPROD AS 'NO POS',sum(ISNULL(phy.Quantity,0)) AS Disponibles,D.TIPFORMED,D.CODGRUFAR,D.CODUNIPES AS UnidadPeso,D.CODUNIVOL AS UnidadVolumen,
			RTRIM(D.ABRPROMEZ) AS AbreviaturaProducto,D.VOLTOTMED AS TotalVolumen,D.VOLTOTMED,D.PESTOTMED,D.TIEESTMED,CAST(0 AS BIT) AS FormatoNOPOS, D.TODASPATO
			, C.Conditioned, UNIRS = CASE WHEN C.UNIRS = 1 THEN 'Si' ELSE 'No' END,F.CODFORMED, F.RequireStability
			, PBS = CASE WHEN C.Conditioned = 1 THEN 'Condicionado'
				WHEN D.NOPOSPROD = 1 THEN 'No'
				ELSE 'Si'
				END
		from 
		dbo.IHLISTPRO D With(Nolock)
		INNER JOIN Inventory.ATC C  With(Nolock) ON RTRIM(D.CODPRODUC) = C.Code and C.DiluentProduct = 1
		LEFT OUTER JOIN Inventory.InventoryProduct B  With(Nolock) on B.Status = 1 AND B.ATCId = C.Id
		LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = B.Id
		AND phy.WarehouseId IN (SELECT Id from @Almacenes)
		LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
		LEFT OUTER JOIN dbo.IHFORMEDI F on F.CODFORMED = D.CODFORMED 
		WHERE D.CODDCIMED = @CodigoDenominacion AND 
		D.TIPPRODUC IN ('1', '3') AND D.ESPDILPRO = 1 AND D.PROESTADO = 1
 		GROUP BY RTRIM(D.CODPRODUC), RTRIM(D.DESPRODUC),D.NOPOSPROD,D.TIPFORMED,D.CODGRUFAR,D.CODUNIPES,D.CODUNIVOL,
			RTRIM(D.ABRPROMEZ),D.VOLTOTMED,D.PESTOTMED,D.TIEESTMED,D.TODASPATO,C.Conditioned,C.UNIRS,F.CODFORMED,F.RequireStability 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los productos farmacéuticos utilizados como líquidos diluyentes disponibles en un almacén específico, consultando el catálogo maestro de medicamentos (IHLISTPRO) y el inventario físico según la versión del ERP (v1, v2 o v3/Inventory). Filtra únicamente productos marcados como diluyentes (ESPDILPRO=1), activos, de tipo medicamento o insumo (TIPPRODUC 1 o 3), y que coincidan con una denominación común (código DCI/INN). Retorna información clave del medicamento como nombre, código, unidades de peso y volumen, volumen total, tiempo de estabilidad, abreviatura para mezclas, condición PBS/No PBS o condicionado, y las unidades disponibles en inventario; datos esenciales para la preparación de mezclas intravenosas y líquidos en farmacia hospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosParaLiquidos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosParaLiquidos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los productos diluentes/aptos para preparación de mezclas y líquidos correspondientes a una denominación común, con sus existencias por almacén y atributos farmacéuticos, soportando tres versiones del modelo de inventario (ERP).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse la denominación común del medicamento sobre la que se buscarán los productos diluentes.; Para las ramas de versión 1 y 2 debe existir un almacén válido sobre el cual consultar existencias.; El parámetro de versión de ERP determina contra qué modelo de datos se ejecuta la consulta.; El centro de atención solo se aplica como filtro adicional cuando viene informado y no vacío.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven productos activos (PROESTADO = 1).; Solo productos cuyo tipo es ''1'' o ''3'' (TIPPRODUC IN (''1'',''3'')).; Solo productos marcados como aptos para dilución/preparación de líquidos (ESPDILPRO = 1 en ramas 1 y 2; DiluentProduct = 1 en la rama nueva).; El filtro siempre se realiza por la denominación común recibida (CODDCIMED = @CodigoDenominacion).; En la rama nueva los almacenes considerados siempre incluyen los Warehouse sin centro de atención asignado (CodeCenterAttention IS NULL).; En la rama nueva solo se consideran productos de inventario con Status = 1.; Las cantidades disponibles se agregan con SUM y se tratan como entero; en la rama nueva los nulos se convierten en 0.; PBS prioriza la condición ''Condicionado'' sobre la marca No POS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto/medicamento; Diluente para mezclas; Denominación común (DCI); Almacén/bodega; Centro de atención; Inventario físico/disponibilidad; Clasificación POS / No POS / PBS; Forma farmacéutica; Grupo farmacológico; Unidad de peso y volumen; Tiempo de estabilidad; Producto condicionado; Unidosis (UNIRS); Patología (TODASPATO)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Cuando @VersionERP=1, devuelve productos diluentes activos (ESPDILPRO=1, PROESTADO=1, TIPPRODUC IN (''1'',''3'')) de la denominación dada con existencias sumadas desde INFISICO para el almacén indicado.; [RETURN_RESULT] RESULTSET: Cuando @VersionERP=2, devuelve los mismos productos pero tomando existencias desde INNFISICO/INNALMACE filtrando por IALCODIGO=@Almacen.; [RETURN_RESULT] RESULTSET: Cuando @VersionERP no es 1 ni 2, devuelve productos cuyo ATC tiene DiluentProduct=1, sumando Quantity desde Inventory.PhysicalInventory de las bodegas asociadas al centro de atención (y las bodegas sin centro de atención), incluyendo Conditioned, UNIRS, RequireStability y clasificación PBS.; [INSERT] @Almacenes: Si @CentroAtencion viene informado y no vacío, inserta los Warehouse.Id cuyo CodeCenterAttention=@CentroAtencion.; [INSERT] @Almacenes: Siempre (en la rama nueva) inserta también los Warehouse.Id cuyo CodeCenterAttention IS NULL para considerarlos como almacenes válidos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @VersionERP = 1 → Consulta existencias en el ERP legado usando IHLISTPRO + IHRINDDGH + INFISICO filtrando por almacén recibido.; si @VersionERP = 2 → Consulta existencias en el ERP intermedio usando IHLISTPRO + IHRINDDGH + INNPRODUC + INNFISICO + INNALMACE filtrando por almacén.; si @VersionERP distinto de 1 y 2 (ELSE) → Consulta existencias contra el módulo Inventory (ATC, InventoryProduct, PhysicalInventory, Warehouse) restringiendo bodegas según centro de atención y agregando información de estabilidad, condicionamiento y unidosis.; si @CentroAtencion no es nulo y tiene longitud > 0 (rama nueva) → Incluye en la lista de almacenes válidos los Warehouse cuyo CodeCenterAttention coincide con el centro de atención recibido.; si Siempre en rama nueva (después del IF de centro) → Incluye también los Warehouse cuyo CodeCenterAttention IS NULL como almacenes considerados.; si NOPOSPROD = 1 (versiones 1 y 2) → Marca PBS = ''No'' else PBS = ''Si''; si C.Conditioned = 1 (rama nueva) → PBS = ''Condicionado'' else Si NOPOSPROD = 1 → PBS=''No''; en otro caso PBS=''Si''; si C.UNIRS = 1 (rama nueva) → UNIRS = ''Si'' else UNIRS = ''No''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHLISTPRO; dbo.IHRINDDGH; dbo.INFISICO; dbo.INNPRODUC; dbo.INNFISICO; dbo.INNALMACE; Inventory.PhysicalInventory; Inventory.InventoryProduct; Inventory.ATC; Inventory.Warehouse; dbo.IHFORMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaLiquidos';
-- GO
