CREATE PROCEDURE [dbo].[SP_HC_ListarProductosPacienteEnfermeriaParcial]
(
@Almacen char(4),
@Ingreso char(20),
@Paciente varchar(25),
@VersionERP int,
@CentroAtencion char(20),
@UnidadFuncional char(20)
)
AS
BEGIN
	SET NOCOUNT ON;
	-- VIE-ERP VERSIÓN 3

		declare @AlmacenAux varchar(4) = RTRIM(@Almacen)
		if @AlmacenAux IS NOT NULL AND LEN(@AlmacenAux) = 0 SET @AlmacenAux = NULL 

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

				    --carga los medicamento como dispositivos
					SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
							from 
							dbo.IHLISTPRO D With(Nolock) 
							INNER JOIN Inventory.ATC atc With(Nolock) ON RTRIM(D.CODPRODUC) = atc.Code 
							LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
							LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
							AND phy.WarehouseId IN (SELECT Id from @Almacenes)
							LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
							WHERE (D.TIPPRODUC = '3' OR D.ESPDILPRO = 1) AND D.PROESTADO=1  AND D.CONSUMPTION = 0 
 							GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
							union
							--carga los insumos
					 select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles, D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
							From dbo.IHLISTPRO D With(Nolock)
							INNER JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = s.Code 
							LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
							LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
							AND phy.WarehouseId IN (SELECT Id from @Almacenes)
							LEFT OUTER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id
							WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1  AND D.CONSUMPTION = 0 
							GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
				END ELSE BEGIN

						--carga los medicamento como dispositivos
					SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
							from 
							dbo.IHLISTPRO D With(Nolock) 
							INNER JOIN Inventory.ATC atc With(Nolock) ON RTRIM(D.CODPRODUC) = atc.Code 
							LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
							LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
							AND phy.WarehouseId IN (SELECT Id from @Almacenes)
							LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
							WHERE (D.TIPPRODUC = '3' OR D.ESPDILPRO = 1) AND D.PROESTADO=1 AND phy.Quantity > 0  AND D.CONSUMPTION = 0 
 							GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
							union
							--carga los insumos
					 select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles, D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
							From dbo.IHLISTPRO D With(Nolock)
							INNER JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = s.Code 
							LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
							LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
							AND phy.WarehouseId IN (SELECT Id from @Almacenes)
							LEFT OUTER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id
							WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1  AND phy.Quantity > 0  AND D.CONSUMPTION = 0 
							GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC
				END

	   END ELSE BEGIN
						
				   IF  @ParametroMostrarInsumosCantidadCero = 1 BEGIN  	
						
						--carga los medicamento como dispositivos
						SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
								from 
								dbo.IHLISTPRO D With(Nolock) 
								INNER JOIN Inventory.ATC atc With(Nolock) ON RTRIM(D.CODPRODUC) = atc.Code 
								LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
								LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on phy.ProductId = invpro.Id
								AND phy.WarehouseId IN (SELECT Id from @Almacenes)
								INNER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
								WHERE (D.TIPPRODUC = '3' OR D.ESPDILPRO = 1) AND D.PROESTADO=1 AND D.CONSUMPTION = 0 
 								GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
								union
								--carga los insumos
						 select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles, D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
								From dbo.IHLISTPRO D With(Nolock)
								INNER JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = s.Code 
								LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
								LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
								AND phy.WarehouseId IN (SELECT Id from @Almacenes)
								INNER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
								WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1 AND D.CONSUMPTION = 0 
								GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
				END ELSE BEGIN
							--carga los medicamento como dispositivos
						SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
								from 
								dbo.IHLISTPRO D With(Nolock) 
								INNER JOIN Inventory.ATC atc With(Nolock) ON RTRIM(D.CODPRODUC) = atc.Code 
								LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
								LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
								AND phy.WarehouseId IN (SELECT Id from @Almacenes)
								INNER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
								WHERE (D.TIPPRODUC = '3' OR D.ESPDILPRO = 1) AND D.PROESTADO=1 AND phy.Quantity > 0 AND D.CONSUMPTION = 0 
 								GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
								union
								--carga los insumos
						 select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles, D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
								From dbo.IHLISTPRO D With(Nolock)
								INNER JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = s.Code 
								LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
								LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
								AND phy.WarehouseId IN (SELECT Id from @Almacenes)
								INNER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
								WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1 AND phy.Quantity > 0 AND D.CONSUMPTION = 0 
								GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC
				END 
		END

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos, dispositivos médicos e insumos disponibles en bodega que pueden ser solicitados por enfermería para un paciente, consultando el stock físico por centro de atención y unidad funcional. El comportamiento varía según dos parámetros configurados en la unidad funcional de enfermería (HCUNITHIS): si se deben usar bodegas específicas predefinidas y si se deben mostrar productos con cantidad en cero. Combina el catálogo maestro de productos (IHLISTPRO) con la clasificación ATC e insumos (Inventory.ATC, InventorySupplie), el inventario de productos activos (InventoryProduct) y las cantidades físicas disponibles por bodega (PhysicalInventory), filtrando las bodegas asociadas al centro de atención indicado (Inventory.Warehouse). Se usa en el módulo de enfermería para presentar el listado de insumos y medicamentos disponibles al momento de realizar una solicitud parcial de productos para el paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosPacienteEnfermeriaParcial';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosPacienteEnfermeriaParcial';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos (tipo 3 o especiales de dilución) e insumos (tipo 2) activos disponibles para enfermería, mostrando cantidades existentes según los almacenes y parámetros configurados para el centro de atención y unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaParcial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir configuración en HCUNITHIS para el centro de atención y unidad funcional con tipo de historia ''ENF'' para obtener los parámetros DEFINIRBODEGAS y MOSTRARINSUCERO; en caso contrario se asumen ambos en 0.; Si se filtra por bodegas específicas (DEFINIRBODEGAS=1) deben existir registros en HCPARBODEGAS para el centro de atención y unidad funcional indicados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaParcial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre se incluyen en el conjunto de almacenes los Warehouse con CodeCenterAttention NULL.; Solo se consideran productos con PROESTADO=1 y CONSUMPTION=0.; Solo se consideran InventoryProduct con Status=1.; La consulta nunca modifica datos: es de solo lectura (todas las tablas con WITH(NOLOCK)).; La clasificación de medicamentos vs insumos se hace exclusivamente por TIPPRODUC (''3'' o ESPDILPRO=1 = medicamento; ''2'' = insumo).; Los campos ''DESADMINI'' y ''Cantidad Prescrita'' siempre se devuelven vacíos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaParcial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamentos; Insumos / dispositivos médicos; Inventario físico por bodega; Centro de atención; Unidad funcional; Historia clínica de enfermería (CODTIPHIS=''ENF''); Clasificación ATC; Productos para dilución; Parametrización de bodegas por unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaParcial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Cuando DEFINIRBODEGAS=0 y MOSTRARINSUCERO=1: retorna todos los productos activos (PROESTADO=1, CONSUMPTION=0) sin filtro de cantidad, considerando bodegas del centro de atención más las bodegas sin centro asignado.; [RETURN_RESULT] RESULTSET: Cuando DEFINIRBODEGAS=0 y MOSTRARINSUCERO=0: retorna solo productos con phy.Quantity>0, PROESTADO=1 y CONSUMPTION=0.; [RETURN_RESULT] RESULTSET: Cuando DEFINIRBODEGAS=1 y MOSTRARINSUCERO=1: retorna productos activos restringiendo (INNER JOIN) a bodegas cuyo Code esté en HCPARBODEGAS para el centro de atención y unidad funcional, sin filtro de cantidad.; [RETURN_RESULT] RESULTSET: Cuando DEFINIRBODEGAS=1 y MOSTRARINSUCERO=0: retorna productos activos restringiendo a bodegas configuradas en HCPARBODEGAS y exigiendo phy.Quantity>0.; [RETURN_RESULT] RESULTSET: Los medicamentos se identifican con TIPPRODUC=''3'' o ESPDILPRO=1 y se cruzan contra Inventory.ATC; los insumos se identifican con TIPPRODUC=''2'' y se cruzan contra Inventory.InventorySupplie; ambos se unen con UNION.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaParcial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CentroAtencion no es nulo y tiene longitud > 0 → Agrega a la tabla de almacenes los Warehouse cuyo CodeCenterAttention coincida con el centro de atención else Solo se cargan en la tabla los almacenes con CodeCenterAttention NULL; si @ParametroDefinirBodegasCantidadInsumos = 0 → No restringe por bodegas configuradas en HCPARBODEGAS (LEFT OUTER JOIN sobre Warehouse) else Restringe (INNER JOIN) a las bodegas listadas en HCPARBODEGAS para el centro de atención y unidad funcional; si @ParametroMostrarInsumosCantidadCero = 1 → Incluye productos sin importar la cantidad física (incluye cantidad cero o sin existencias) else Filtra solo productos con phy.Quantity > 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaParcial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Warehouse; dbo.IHLISTPRO; Inventory.ATC; Inventory.InventoryProduct; Inventory.PhysicalInventory; Inventory.InventorySupplie; dbo.HCPARBODEGAS; dbo.HCUNITHIS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaParcial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaParcial';
-- GO
