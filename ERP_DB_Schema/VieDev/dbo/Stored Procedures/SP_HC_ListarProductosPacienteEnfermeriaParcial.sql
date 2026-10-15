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