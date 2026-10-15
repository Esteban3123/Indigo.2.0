


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