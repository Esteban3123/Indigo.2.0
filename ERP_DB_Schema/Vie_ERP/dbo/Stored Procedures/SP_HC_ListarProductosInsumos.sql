
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarProductosInsumos]
@Almacen char(4),
@OidAlmacen int,
@VersionERP int,
@CentroAtencion char(20)
AS
BEGIN

	SET NOCOUNT ON;
	
	declare @AlmacenAux varchar(4) = RTRIM(@Almacen)
	if @AlmacenAux IS NOT NULL AND LEN(@AlmacenAux) = 0 SET @AlmacenAux = NULL 

	IF @VersionERP=1
		if @Almacen = '' begin 
				SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(A.DESPRODUC) AS Descripcion,SUM(CAST(C.IFICANTID AS INT)) AS Disponibles,
				RTRIM(A.CODPRODUC)+' - '+RTRIM(A.DESPRODUC) AS CodigoDescripcion,A.JUSINMEDI AS ExigeJustificacion 
				FROM IHLISTPRO A 
				INNER JOIN IHRINDDGH B ON A.CODPRODUC=B.CODPRODUC and a.PROESTADO=1 
				INNER JOIN dbo.INFISICO C ON B.IPRCODIGO=C.IPRCODIGO 
				WHERE  A.TIPPRODUC in ('2','3') 
				GROUP BY RTRIM(A.CODPRODUC) , RTRIM(A.DESPRODUC),A.JUSINMEDI

		end else begin

				SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(A.DESPRODUC) AS Descripcion,SUM(CAST(C.IFICANTID AS INT)) AS Disponibles,
				RTRIM(A.CODPRODUC)+' - '+RTRIM(A.DESPRODUC) AS CodigoDescripcion,A.JUSINMEDI AS ExigeJustificacion 
				FROM IHLISTPRO A 
				INNER JOIN IHRINDDGH B ON A.CODPRODUC=B.CODPRODUC and a.PROESTADO=1 
				INNER JOIN dbo.INFISICO C ON B.IPRCODIGO=C.IPRCODIGO 
				WHERE  A.TIPPRODUC in ('2','3') and c.IALCODIGO=@Almacen
				GROUP BY RTRIM(A.CODPRODUC) , RTRIM(A.DESPRODUC),A.JUSINMEDI
		end
	ELSE IF @VersionERP=2
		SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(A.DESPRODUC) AS Descripcion,SUM(CAST(C.IFICANTID AS INT)) AS Disponibles,
		RTRIM(A.CODPRODUC)+' - '+RTRIM(A.DESPRODUC) AS CodigoDescripcion,A.JUSINMEDI AS ExigeJustificacion 
		FROM IHLISTPRO A 
		INNER JOIN IHRINDDGH B ON A.CODPRODUC=B.CODPRODUC 
		INNER JOIN dbo.INNPRODUC P ON P.IPRCODIGO=B.IPRCODIGO 
		LEFT OUTER JOIN dbo.INNFISICO C ON P.OID=C.INNPRODUC 
		LEFT OUTER JOIN dbo.INNALMACE D ON C.INNALMACE=D.OID 
		WHERe a.PROESTADO=1  AND (A.TIPPRODUC = '2' OR  A.ESPDILPRO ='1')
		GROUP BY RTRIM(A.CODPRODUC) , RTRIM(A.DESPRODUC),A.JUSINMEDI
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

		SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.JUSINMEDI AS ExigeJustificacion
		from 
		dbo.IHLISTPRO D With(Nolock) 
		INNER JOIN Inventory.ATC atc  With(Nolock)
		ON RTRIM(D.CODPRODUC) = atc.Code 
		LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
		LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
		AND phy.WarehouseId IN (SELECT Id from @Almacenes)
		LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
		where (D.TIPPRODUC = '3' OR D.ESPDILPRO = 1)  AND D.PROESTADO = 1
 		GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.JUSINMEDI  
		union
		--carga los insumos
		select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles, D.JUSINMEDI AS ExigeJustificacion
		From dbo.IHLISTPRO D With(Nolock) 
		INNER JOIN Inventory.InventorySupplie s
		ON RTRIM(D.CODPRODUC) = s.Code 
		LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
		LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
		AND phy.WarehouseId IN (SELECT Id from @Almacenes)
		LEFT OUTER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id
		where D.TIPPRODUC = '2'  AND D.PROESTADO=1
		GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.JUSINMEDI
END