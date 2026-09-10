CREATE PROCEDURE [dbo].[SP_HC_ListarInsumos]
(
@CentroAtencion char(20),
@UnidadFuncional char(20),
@ListarProducto bit
)
AS
BEGIN
	SET NOCOUNT ON;

		DECLARE @Almacenes as table(Id int) 

		if @CentroAtencion is not null and len(rtrim(@CentroAtencion))> 0
		begin
			INSERT INTO @Almacenes
			select Id from Inventory.Warehouse where CodeCenterAttention = @CentroAtencion
		end

		INSERT INTO @Almacenes
		select Id from Inventory.Warehouse where CodeCenterAttention IS NULL

		declare @TipoProductos as varchar(20)
		if @ListarProducto = 1 begin
			 set @TipoProductos = '1' + ',' + '3' --1 - medicamentos 3--medicamentos como insumos
		end else begin
			 set @TipoProductos = '2' --insumos
		end

		print @TipoProductos

		
		declare @ParametroDefinirBodegasCantidadInsumos as bit = 0 --"Definir Bodegas para mostrar cantidad de insumos" 
		declare @ParametroMostrarInsumosCantidadCero as bit  = 0 --"Mostrar insumos con cantidad en cero para la solicitud"
	
		
		select @ParametroDefinirBodegasCantidadInsumos = isnull(DEFINIRBODEGAS,0) , @ParametroMostrarInsumosCantidadCero = isnull(MOSTRARINSUCERO,0) from HCUNITHIS where CODCENATE = @CentroAtencion and UFUCODIGO = @UnidadFuncional and CODTIPHIS = 'ENF'
				
		IF @ParametroDefinirBodegasCantidadInsumos = 0 BEGIN

				 IF  @ParametroMostrarInsumosCantidadCero = 1 BEGIN  	

				    --carga los medicamento como dispositivos
					SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC
							from 
							dbo.IHLISTPRO D With(Nolock) 
							INNER JOIN Inventory.ATC atc With(Nolock) ON RTRIM(D.CODPRODUC) = atc.Code 
							LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
							LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
							AND phy.WarehouseId IN (SELECT Id from @Almacenes)
							LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
							WHERE (D.TIPPRODUC IN (@TipoProductos) OR D.ESPDILPRO = 1) AND D.PROESTADO=1
 							GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
							union
							--carga los insumos
					 select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles, D.TIPPRODUC
							From dbo.IHLISTPRO D With(Nolock)
							INNER JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = s.Code 
							LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
							LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
							AND phy.WarehouseId IN (SELECT Id from @Almacenes)
							LEFT OUTER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id
							WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1
							GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  

				END ELSE BEGIN

						--carga los medicamento como dispositivos
					SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC
							from 
							dbo.IHLISTPRO D With(Nolock) 
							INNER JOIN Inventory.ATC atc With(Nolock) ON RTRIM(D.CODPRODUC) = atc.Code 
							LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
							LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
							AND phy.WarehouseId IN (SELECT Id from @Almacenes)
							LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
							WHERE (D.TIPPRODUC IN (@TipoProductos)  OR D.ESPDILPRO = 1) AND D.PROESTADO=1 AND phy.Quantity > 0
 							GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
							union
							--carga los insumos
					 select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles, D.TIPPRODUC
							From dbo.IHLISTPRO D With(Nolock)
							INNER JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = s.Code 
							LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
							LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
							AND phy.WarehouseId IN (SELECT Id from @Almacenes)
							LEFT OUTER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id
							WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1  AND phy.Quantity > 0
							GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
		
				END

	   END ELSE BEGIN
						
				   IF  @ParametroMostrarInsumosCantidadCero = 1 BEGIN  	
						
						--carga los medicamento como dispositivos
						SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC
								from 
								dbo.IHLISTPRO D With(Nolock) 
								INNER JOIN Inventory.ATC atc With(Nolock) ON RTRIM(D.CODPRODUC) = atc.Code 
								LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
								LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
								AND phy.WarehouseId IN (SELECT Id from @Almacenes)
								INNER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
								WHERE (D.TIPPRODUC = '3' OR D.ESPDILPRO = 1) AND D.PROESTADO=1
 								GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
								union
								--carga los insumos
						 select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles, D.TIPPRODUC
								From dbo.IHLISTPRO D With(Nolock)
								INNER JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = s.Code 
								LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
								LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
								AND phy.WarehouseId IN (SELECT Id from @Almacenes)
								INNER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
								WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1
								GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  

				END ELSE BEGIN
							--carga los medicamento como dispositivos
						SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC
								from 
								dbo.IHLISTPRO D With(Nolock) 
								INNER JOIN Inventory.ATC atc With(Nolock) ON RTRIM(D.CODPRODUC) = atc.Code 
								LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
								LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
								AND phy.WarehouseId IN (SELECT Id from @Almacenes)
								INNER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
								WHERE (D.TIPPRODUC = '3' OR D.ESPDILPRO = 1) AND D.PROESTADO=1 AND phy.Quantity > 0
 								GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
								union
								--carga los insumos
						 select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles, D.TIPPRODUC
								From dbo.IHLISTPRO D With(Nolock)
								INNER JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = s.Code 
								LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
								LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
								AND phy.WarehouseId IN (SELECT Id from @Almacenes)
								INNER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
								WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1 AND phy.Quantity > 0 
								GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
							    
				END 
		END

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos e insumos disponibles en el inventario para una unidad funcional y centro de atención específicos, con su cantidad disponible en bodega. Consulta el catálogo de productos (medicamentos clasificados por ATC e insumos), cruza con el inventario físico de las bodegas correspondientes al centro de atención, y aplica dos parámetros de configuración clínica de la unidad (definición de bodegas específicas y visualización de productos con stock en cero). Se usa en la historia clínica de enfermería para que el profesional pueda seleccionar medicamentos o insumos al momento de registrar una solicitud o administración, filtrando por tipo de producto (medicamento, insumo o ambos) y respetando las reglas operativas configuradas por unidad funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarInsumos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarInsumos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista medicamentos e insumos disponibles con sus cantidades en bodega para una unidad funcional, aplicando parámetros de visibilidad de bodegas y de inclusión de ítems con stock cero.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInsumos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir configuración en HCUNITHIS para el centro de atención y unidad funcional con CODTIPHIS=''ENF'' (si no, los parámetros de comportamiento toman valor 0 por defecto vía ISNULL).; Los productos a listar deben tener PROESTADO=1 (activos) en IHLISTPRO.; Los productos de inventario deben estar con Status=1 en Inventory.InventoryProduct.; Cuando se restringe por bodegas, deben existir registros en HCPARBODEGAS para el centro de atención y unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInsumos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven productos activos (PROESTADO=1) e InventoryProduct con Status=1.; La unión combina dos fuentes de catálogo: medicamentos vía Inventory.ATC e insumos vía Inventory.InventorySupplie.; El alcance de bodegas siempre depende de la combinación CentroAtencion/UnidadFuncional y de los parámetros de configuración de HCUNITHIS.; La cantidad disponible nunca es NULL (se reemplaza por 0 con ISNULL).; Cuando se aplica el modo de bodegas restringidas, los medicamentos de TIPPRODUC=''1'' no se devuelven, solo ''3'' o ESPDILPRO=1.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInsumos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'medicamentos; insumos; dispositivos médicos; medicamentos como insumos; bodegas/almacenes de farmacia; centro de atención; unidad funcional; inventario físico; clasificación ATC; productos especiales de dilución; historia clínica de enfermería (CODTIPHIS=''ENF'')', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInsumos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT: Cuando @ListarProducto=1 se listan tipos de producto ''1'' (medicamentos) y ''3'' (medicamentos como insumos); cuando =0 se lista solo tipo ''2'' (insumos).; [RETURN_RESULT] RESULT: Cuando DEFINIRBODEGAS=0, se consideran almacenes asociados al CentroAtencion más los que tienen CodeCenterAttention NULL (almacenes generales).; [RETURN_RESULT] RESULT: Cuando DEFINIRBODEGAS=1, se restringen los almacenes a los códigos definidos en HCPARBODEGAS para el centro de atención y unidad funcional, y la rama de medicamentos filtra solo TIPPRODUC=''3'' (excluye ''1'').; [RETURN_RESULT] RESULT: Cuando MOSTRARINSUCERO=1 se incluyen productos con cantidad cero o sin inventario; cuando =0 solo se devuelven los que cumplen phy.Quantity > 0.; [RETURN_RESULT] RESULT: Productos marcados como ESPDILPRO=1 (especiales para dilución) siempre se incluyen en la rama de medicamentos/dispositivos, independiente del tipo de producto.; [RETURN_RESULT] RESULT: La cantidad disponible se calcula como SUM(ISNULL(phy.Quantity,0)) agrupado por código, descripción y tipo de producto, sumando existencias en los almacenes filtrados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInsumos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CentroAtencion no nulo y no vacío → Carga en la tabla de almacenes los Warehouse cuyo CodeCenterAttention coincide con el centro else Solo se cargan los almacenes con CodeCenterAttention NULL; si @ListarProducto = 1 → Lista tipos de producto ''1,3'' (medicamentos y medicamentos como insumos) else Lista tipo de producto ''2'' (insumos); si DEFINIRBODEGAS = 0 (parámetro ''Definir Bodegas para mostrar cantidad de insumos'') → Usa LEFT JOIN con Warehouse sin filtrar por HCPARBODEGAS y considera tipos según @TipoProductos else Usa INNER JOIN con Warehouse restringido a códigos de HCPARBODEGAS y limita medicamentos a TIPPRODUC=''3''; si MOSTRARINSUCERO = 1 (parámetro ''Mostrar insumos con cantidad en cero'') → Incluye todos los productos activos aunque no tengan stock else Filtra solo productos con phy.Quantity > 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInsumos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Warehouse; dbo.IHLISTPRO; Inventory.ATC; Inventory.InventoryProduct; Inventory.PhysicalInventory; Inventory.InventorySupplie; dbo.HCPARBODEGAS; dbo.HCUNITHIS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInsumos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInsumos';
-- GO
