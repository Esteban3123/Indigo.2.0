

CREATE PROCEDURE [dbo].[SP_HC_ListarProductosEmergencia]
(
@Almacen CHAR(4),
@VersionERP INT,
@CentroAtencion char(20),
@UnidadFuncional char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

	declare @AlmacenAux varchar(4) = RTRIM(@Almacen)
	if @AlmacenAux IS NOT NULL AND LEN(@AlmacenAux) = 0 SET @AlmacenAux = NULL 	

IF @VersionERP=1

        SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Producto,SUM(CAST(COALESCE(NULLIF(C.IFICANTID,0),0) AS INT)) AS Disponibles,TIPPRODUC AS Tipo 
	    FROM dbo.IHLISTPRO A 
		INNER JOIN  dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC INNER JOIN
		dbo.INFISICO C ON B.IPRCODIGO=C.IPRCODIGO 
		WHERE C.IALCODIGO=@Almacen AND NOPOSPROD='0' AND PROESTADO = 1
		GROUP BY A.CODPRODUC,DESPRODUC,TIPPRODUC
		
ELSE IF @VersionERP=2

		SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Producto,SUM(CAST(COALESCE(NULLIF(D.IFICANTID,0),0) AS INT)) AS Disponibles,TIPPRODUC AS Tipo 
		FROM dbo.IHLISTPRO A 
		INNER JOIN  dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC 
		INNER JOIN dbo.INNPRODUC C ON B.IPRCODIGO=C.IPRCODIGO 
		INNER JOIN dbo.INNFISICO D ON D.INNPRODUC=C.OID 
		INNER JOIN dbo.INNALMACE E ON D.INNALMACE=E.OID 
		WHERE  E.IALCODIGO=@Almacen AND NOPOSPROD='0' AND PROESTADO = 1 
		GROUP BY A.CODPRODUC,DESPRODUC,TIPPRODUC

ELSE
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

		declare @ParametroDefinirBodegasCantidadInsumos as bit = 0 --"Definir Bodegas para mostrar cantidad de insumos" 
		declare @ParametroMostrarInsumosCantidadCero as bit  = 0 --"Mostrar insumos con cantidad en cero para la solicitud"
	
		select @ParametroDefinirBodegasCantidadInsumos = isnull(DEFINIRBODEGAS,0) , @ParametroMostrarInsumosCantidadCero = isnull(MOSTRARINSUCERO,0) from HCUNITHIS where CODCENATE = @CentroAtencion and UFUCODIGO = @UnidadFuncional and CODTIPHIS = 'ENF'

		IF @ParametroDefinirBodegasCantidadInsumos = 0 BEGIN

				
				IF  @ParametroMostrarInsumosCantidadCero = 1 BEGIN 
						--carga los medicamento como insumos
						SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Producto,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(ISNULL(phy.Quantity,0))AS Disponibles,D.TIPPRODUC AS Tipo 
						from 
						dbo.IHLISTPRO D With(Nolock)
						INNER JOIN Inventory.ATC atc  With(Nolock) ON RTRIM(D.CODPRODUC) = atc.Code 
						LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
						WHERE (D.TIPPRODUC IN ('1', '3') OR D.ESPDILPRO = 1) AND D.PROESTADO = 1
 						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
						union
						--carga los insumos
						select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Producto,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(ISNULL(phy.Quantity,0))AS Disponibles,D.TIPPRODUC AS Tipo 
						From dbo.IHLISTPRO D With(Nolock)
						INNER JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = s.Code 
						LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						LEFT OUTER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id
						WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1
						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  

				END  ELSE BEGIN
						--carga los medicamento como insumos
						SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Producto,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(ISNULL(phy.Quantity,0))AS Disponibles,D.TIPPRODUC AS Tipo 
						from 
						dbo.IHLISTPRO D With(Nolock)
						INNER JOIN Inventory.ATC atc  With(Nolock) ON RTRIM(D.CODPRODUC) = atc.Code 
						LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
						WHERE (D.TIPPRODUC IN ('1', '3') OR D.ESPDILPRO = 1) AND D.PROESTADO = 1 AND phy.Quantity > 0
 						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
						union
						--carga los insumos
						select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Producto,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(ISNULL(phy.Quantity,0))AS Disponibles,D.TIPPRODUC AS Tipo 
						From dbo.IHLISTPRO D With(Nolock)
						INNER JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = s.Code 
						LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						LEFT OUTER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id
						WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1 AND phy.Quantity > 0
						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  

				END
	

		END ELSE BEGIN
		

				IF  @ParametroMostrarInsumosCantidadCero = 1 BEGIN 

						--carga los medicamento como insumos
						SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Producto,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(ISNULL(phy.Quantity,0))AS Disponibles,D.TIPPRODUC AS Tipo 
						from 
						dbo.IHLISTPRO D With(Nolock)
						INNER JOIN Inventory.ATC atc  With(Nolock) ON RTRIM(D.CODPRODUC) = atc.Code 
						LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						INNER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
						WHERE (D.TIPPRODUC IN ('1', '3') OR D.ESPDILPRO = 1) AND D.PROESTADO = 1
 						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
						union
						--carga los insumos
						select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Producto,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(ISNULL(phy.Quantity,0))AS Disponibles,D.TIPPRODUC AS Tipo 
						From dbo.IHLISTPRO D With(Nolock)
						INNER JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = s.Code 
						LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						INNER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
						WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1 
						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC 

				END ELSE BEGIN

							--carga los medicamento como insumos
						SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Producto,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(ISNULL(phy.Quantity,0))AS Disponibles,D.TIPPRODUC AS Tipo 
						from 
						dbo.IHLISTPRO D With(Nolock)
						INNER JOIN Inventory.ATC atc  With(Nolock) ON RTRIM(D.CODPRODUC) = atc.Code 
						LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						INNER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
						WHERE (D.TIPPRODUC IN ('1', '3') OR D.ESPDILPRO = 1) AND D.PROESTADO = 1
 						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
						union
						--carga los insumos
						select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Producto,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(ISNULL(phy.Quantity,0))AS Disponibles,D.TIPPRODUC AS Tipo 
						From dbo.IHLISTPRO D With(Nolock)
						INNER JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = s.Code 
						LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						INNER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
						WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1 AND phy.Quantity > 0
						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC 

				END

		END
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos, insumos y dispositivos médicos disponibles en el botiquín o almacén de emergencia para una unidad funcional y centro de atención específicos, mostrando el código del producto, nombre, tipo (medicamento, insumo o diluyente) y cantidad disponible en inventario físico. Soporta tres versiones del motor de inventario (ERP v1, v2 y v3/Inventory), consultando el catálogo maestro de productos (IHLISTPRO) cruzado con el inventario físico de las bodegas asignadas al centro de atención (Inventory.Warehouse). En la versión 3, respeta la configuración clínica de la unidad funcional (HCUNITHIS) para determinar si se deben filtrar bodegas específicas y si se deben mostrar productos con cantidad en cero, lo que permite al personal de enfermería y urgencias seleccionar insumos y medicamentos disponibles al momento de registrar una solicitud de emergencia en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosEmergencia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosEmergencia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos (medicamentos e insumos) disponibles para una atención de emergencia, mostrando existencias por almacén/bodega según la versión del ERP y la parametrización del centro/unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosEmergencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Para versiones 1 y 2 del ERP debe proporcionarse un código de almacén válido.; Para la versión por defecto (no 1 ni 2) deben existir registros en HCUNITHIS con CODTIPHIS=''ENF'' para el centro de atención y unidad funcional dados, de lo contrario los parámetros de comportamiento quedarán como 0.; Si se usa parametrización por bodegas, deben existir registros en HCPARBODEGAS asociados al centro de atención y unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosEmergencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven productos activos (PROESTADO = 1).; En las versiones ERP 1 y 2 solo se devuelven productos no posológicos (NOPOSPROD = ''0'').; Las cantidades disponibles se calculan como SUM agrupado por código, descripción y tipo de producto.; En el esquema Inventory, los medicamentos se identifican por TIPPRODUC IN (''1'',''3'') o ESPDILPRO=1 (mapeados vía catálogo ATC) y los insumos por TIPPRODUC=''2'' (mapeados vía InventorySupplie).; Solo se consideran InventoryProduct con Status = 1.; Las bodegas con CodeCenterAttention NULL siempre forman parte del conjunto de bodegas evaluadas en el flujo Inventory.; Si NULLIF/COALESCE devuelven 0 o NULL en cantidades, se sustituyen por 0 para el cálculo (versiones 1 y 2).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosEmergencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Almacenes: Cuando el centro de atención no es nulo y no está vacío, se insertan los Id de Inventory.Warehouse cuyo CodeCenterAttention coincide con el centro de atención.; [INSERT] @Almacenes: Siempre se insertan adicionalmente los Id de Inventory.Warehouse cuyo CodeCenterAttention es NULL (bodegas sin centro asignado).; [RETURN_RESULT] RESULTSET: Cuando @VersionERP=1, retorna productos activos (PROESTADO=1) no posológicos (NOPOSPROD=''0'') con stock sumado desde INFISICO filtrado por el almacén indicado.; [RETURN_RESULT] RESULTSET: Cuando @VersionERP=2, retorna productos activos no posológicos con stock sumado desde INNFISICO uniendo INNPRODUC e INNALMACE filtrado por el almacén indicado.; [RETURN_RESULT] RESULTSET: Cuando @VersionERP no es 1 ni 2 y el parámetro DEFINIRBODEGAS=0 y MOSTRARINSUCERO=1: retorna unión de medicamentos (TIPPRODUC IN (''1'',''3'') o ESPDILPRO=1) e insumos (TIPPRODUC=''2''), activos, mostrando incluso los de cantidad cero en las bodegas de @Almacenes.; [RETURN_RESULT] RESULTSET: Cuando DEFINIRBODEGAS=0 y MOSTRARINSUCERO=0: retorna la misma unión pero filtrando phy.Quantity > 0 (excluyendo los de cantidad cero).; [RETURN_RESULT] RESULTSET: Cuando DEFINIRBODEGAS=1 y MOSTRARINSUCERO=1: retorna productos restringiendo las bodegas a las definidas en HCPARBODEGAS para el centro de atención y unidad funcional, mostrando insumos aunque tengan cantidad cero (en medicamentos no aplica filtro de cantidad).; [RETURN_RESULT] RESULTSET: Cuando DEFINIRBODEGAS=1 y MOSTRARINSUCERO=0: retorna productos restringidos a las bodegas configuradas en HCPARBODEGAS, filtrando insumos con phy.Quantity > 0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosEmergencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @VersionERP = 1 → Consulta el stock en el esquema legado (INFISICO) filtrando por el almacén.; si @VersionERP = 2 → Consulta el stock en el esquema intermedio (INNPRODUC/INNFISICO/INNALMACE) filtrando por el almacén.; si @VersionERP distinto de 1 y 2 → Usa el esquema Inventory (ATC, InventorySupplie, InventoryProduct, PhysicalInventory, Warehouse) y aplica reglas de parametrización por centro/unidad funcional.; si @CentroAtencion no nulo y no vacío → Agrega a @Almacenes las bodegas asociadas a ese centro de atención además de las globales (sin centro).; si DEFINIRBODEGAS = 0 (no se restringe a bodegas parametrizadas) → Considera todas las bodegas de @Almacenes (las del centro y las sin centro).; si DEFINIRBODEGAS = 1 → Restringe las bodegas a las configuradas en HCPARBODEGAS para el centro de atención y unidad funcional.; si MOSTRARINSUCERO = 1 → Incluye productos con cantidad cero o sin existencias.; si MOSTRARINSUCERO = 0 → Excluye productos con cantidad cero (filtro phy.Quantity > 0).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosEmergencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHLISTPRO; dbo.IHRINDDGH; dbo.INFISICO; dbo.INNPRODUC; dbo.INNFISICO; dbo.INNALMACE; Inventory.Warehouse; Inventory.ATC; Inventory.InventoryProduct; Inventory.PhysicalInventory; Inventory.InventorySupplie; dbo.HCUNITHIS; dbo.HCPARBODEGAS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosEmergencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosEmergencia';
-- GO
