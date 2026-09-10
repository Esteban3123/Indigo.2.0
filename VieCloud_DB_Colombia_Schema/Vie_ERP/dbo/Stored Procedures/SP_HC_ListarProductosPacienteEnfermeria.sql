CREATE PROCEDURE [dbo].[SP_HC_ListarProductosPacienteEnfermeria]
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

	declare @AlmacenAux varchar(4) = RTRIM(@Almacen)
	if @AlmacenAux IS NOT NULL AND LEN(@AlmacenAux) = 0 SET @AlmacenAux = NULL 
	
	IF @VersionERP=1

       SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(A.CODPRODUC)+' - '+RTRIM(DESPRODUC)AS CodigoDescripcion,SUM(CAST(C.IFICANTID AS INT)) AS Disponibles,A.TIPPRODUC 
       FROM dbo.IHLISTPRO A With(Nolock)
       INNER JOIN  dbo.IHRINDDGH AS B With(Nolock) ON A.CODPRODUC=B.CODPRODUC
       INNER JOIN  dbo.INFISICO C With(Nolock) ON B.IPRCODIGO=C.IPRCODIGO 
       WHERE C.IALCODIGO=@Almacen AND A.TIPPRODUC IN ('2','3')  and a.PROESTADO='1'
       GROUP BY A.CODPRODUC,DESPRODUC,A.TIPPRODUC
       UNION 
       SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,SUM(CAST(D.IFICANTID AS INT)) AS Disponibles,B.TIPPRODUC 
       FROM dbo.HCPRESCRA A With(Nolock) 
       INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
       INNER JOIN  dbo.IHRINDDGH AS C With(Nolock) ON B.CODPRODUC=C.CODPRODUC
       INNER JOIN dbo.INFISICO D With(Nolock) ON C.IPRCODIGO=D.IPRCODIGO 
       WHERE D.IALCODIGO=@Almacen AND A.PREESTADO IN ('1','6','7') AND IPCODPACI=@Paciente AND NUMINGRES=@Ingreso  AND B.TIPPRODUC IN ('1','3')  and b.PROESTADO='1'
       GROUP BY B.CODPRODUC,DESPRODUC,B.TIPPRODUC
       UNION 
       SELECT RTRIM(C.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(C.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,SUM(CAST(D.IFICANTID AS INT)) AS Disponibles,B.TIPPRODUC 
       FROM dbo.HCINFCONC A With(Nolock) 
       INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
       INNER JOIN dbo.IHRINDDGH AS C With(Nolock) ON B.CODPRODUC=C.CODPRODUC
       INNER JOIN dbo.INFISICO D With(Nolock) ON C.IPRCODIGO=D.IPRCODIGO 
       INNER JOIN dbo.HCINFLIQA E With(Nolock) ON A.CODCONCEC=E.CODCONCEC 
       WHERE D.IALCODIGO=@Almacen AND E.IPCODPACI=@Paciente AND E.NUMINGRES=@Ingreso AND E.PREESTADO IN ('1','5')  and b.PROESTADO='1'
       GROUP BY C.CODPRODUC,DESPRODUC,B.TIPPRODUC
       UNION 
       SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,SUM(CAST(D.IFICANTID AS INT)) AS Disponibles,B.TIPPRODUC 
       FROM dbo.HCINFLIQD A With(Nolock) 
       INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
       INNER JOIN dbo.IHRINDDGH AS C With(Nolock) ON B.CODPRODUC=C.CODPRODUC
       INNER JOIN dbo.INFISICO D With(Nolock) ON C.IPRCODIGO=D.IPRCODIGO 
       INNER JOIN dbo.HCINFLIQA E With(Nolock) ON A.CODCONCEC=E.CODCONCEC 
       WHERE D.IALCODIGO=@Almacen AND E.IPCODPACI=@Paciente AND E.NUMINGRES=@Ingreso AND E.PREESTADO IN ('1','5') and b.PROESTADO='1'
       GROUP BY B.CODPRODUC,DESPRODUC,B.TIPPRODUC

ELSE IF @VersionERP=2
			/*aca la consulta*/

		SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(A.DESPRODUC) AS Descripcion,
		RTRIM(A.CODPRODUC)+' - '+RTRIM(A.DESPRODUC) AS CodigoDescripcion,CASE WHEN SUM(CAST(C.IFICANTID AS INT)) IS NULL THEN 0 ELSE  SUM(CAST(C.IFICANTID AS INT)) END  AS Disponibles,A.TIPPRODUC 
		FROM IHLISTPRO A With(Nolock) 
		INNER JOIN IHRINDDGH B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
		LEFT OUTER JOIN dbo.INNPRODUC P With(Nolock) ON P.IPRCODIGO=B.IPRCODIGO 
		LEFT OUTER JOIN dbo.INNFISICO C With(Nolock) ON P.OID=C.INNPRODUC 
		LEFT OUTER JOIN dbo.INNALMACE D With(Nolock) ON C.INNALMACE=D.OID 
		WHERE   A.TIPPRODUC in ('2','3') OR (D.IALCODIGO=@Almacen AND A.ESPDILPRO ='1') AND (PROESTADO='True') -- AND C.INNALMACE =(select oid from dbo.INNALMACE where IALCODIGO=@Almacen)
		GROUP BY RTRIM(A.CODPRODUC) , RTRIM(A.DESPRODUC),A.TIPPRODUC
       UNION 
       /*SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,CASE WHEN SUM(CAST(E.IFICANTID AS INT)) IS NULL THEN 0 ELSE  SUM(CAST(E.IFICANTID AS INT)) END  AS Disponibles,B.TIPPRODUC 
       FROM dbo.HCPRESCRA A 
       INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
       INNER JOIN  dbo.IHRINDDGH AS C ON B.CODPRODUC=C.CODPRODUC
	LEFT OUTER JOIN dbo.INNPRODUC AS D ON A.CODPRODUC=D.IPRCODIGO
       LEFT OUTER JOIN dbo.INNFISICO E ON D.OID=E.INNPRODUC AND E.INNALMACE=(select oid from dbo.INNALMACE where IALCODIGO=@Almacen)
       WHERE A.PREESTADO IN ('1','6','7') AND IPCODPACI=@Paciente AND NUMINGRES=@Ingreso  
       GROUP BY B.CODPRODUC,DESPRODUC,B.TIPPRODUC*/

	   /*como la cantidad se retorna en una subconsulta, no podemos agrupar allí por ello es que se realiza un select anidado.*/
	    select x.Codigo ,x.Descripcion, x.CodigoDescripcion ,sum(x.Disponibles), x.TIPPRODUC   from
		(
		SELECT distinct RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,
				(SELECT SUM(ISNULL(CONVERT(INT,IFICANTID),0)) FROM dbo.INNFISICO With(Nolock) WHERE INNPRODUC  = D.OID ) AS Disponibles,B.TIPPRODUC
	    FROM dbo.HCPRESCRA A With(Nolock) 
			INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
			INNER JOIN  dbo.IHRINDDGH AS C With(Nolock) ON B.CODPRODUC=C.CODPRODUC
			LEFT OUTER JOIN dbo.INNPRODUC AS D With(Nolock) ON D.IPRCODIGO = C.IPRCODIGO 
			LEFT OUTER JOIN dbo.INNFISICO E With(Nolock) ON D.OID=E.INNPRODUC 
		WHERE A.PREESTADO IN ('1','6','7') AND IPCODPACI=@Paciente AND NUMINGRES=@Ingreso     
		) as x
		GROUP BY x.Codigo ,x.Descripcion ,x.TIPPRODUC, x.CodigoDescripcion      
       UNION 
       SELECT RTRIM(C.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(C.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,CASE WHEN SUM(CAST(E.IFICANTID AS INT)) IS NULL THEN 0 ELSE  SUM(CAST(E.IFICANTID AS INT)) END  AS Disponibles,B.TIPPRODUC 
       FROM dbo.HCINFCONC A With(Nolock) 
       INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
       INNER JOIN dbo.IHRINDDGH AS C With(Nolock) ON B.CODPRODUC=C.CODPRODUC
	   LEFT OUTER JOIN dbo.INNPRODUC AS D With(Nolock) ON A.CODPRODUC=D.IPRCODIGO
     LEFT OUTER JOIN dbo.INNFISICO E With(Nolock) ON D.OID=E.INNPRODUC -- AND E.INNALMACE=(select oid from dbo.INNALMACE where IALCODIGO=@Almacen)
       INNER JOIN dbo.HCINFLIQA F With(Nolock) ON A.CODCONCEC=F.CODCONCEC 
       WHERE IFICANTID IS NOT NULL AND  F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND F.PREESTADO IN ('1','5')  
       GROUP BY C.CODPRODUC,DESPRODUC,B.TIPPRODUC
       UNION 
       SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,CASE WHEN SUM(CAST(E.IFICANTID AS INT)) IS NULL THEN 0 ELSE  SUM(CAST(E.IFICANTID AS INT)) END  AS Disponibles,B.TIPPRODUC 
       FROM dbo.HCINFLIQD A With(Nolock) 
       INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
       INNER JOIN dbo.IHRINDDGH AS C With(Nolock) ON B.CODPRODUC=C.CODPRODUC
	 LEFT OUTER JOIN dbo.INNPRODUC AS D With(Nolock) ON A.CODPRODUC=D.IPRCODIGO
         LEFT OUTER JOIN dbo.INNFISICO E With(Nolock) ON D.OID=E.INNPRODUC --AND E.INNALMACE=(select oid from dbo.INNALMACE where IALCODIGO=@Almacen)
       INNER JOIN dbo.HCINFLIQA F With(Nolock) ON A.CODCONCEC=F.CODCONCEC 
       WHERE IFICANTID IS NOT NULL AND  F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND F.PREESTADO IN ('1','5') 
       GROUP BY B.CODPRODUC,DESPRODUC,B.TIPPRODUC

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
						   UNION 
						   --carga los medicamento o medicamentos como insumo formulados al paciente
					  SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,sum(isnull(phy.Quantity,0)) AS Disponibles,B.TIPPRODUC, RTRIM(A.DESADMINI), isnull(EXT.CANTIDADREPOSICION,A.CANPEDPRO) AS 'Cantidad Prescrita' 
						   FROM dbo.HCPRESCRA A With(Nolock) 
							INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.IPCODPACI=@Paciente AND A.NUMINGRES=@Ingreso AND 
							A.PREESTADO IN (1,6,7)
							AND A.CODPRODUC = B.CODPRODUC AND B.TIPPRODUC = '1'  AND B.PROESTADO =1
							INNER JOIN Inventory.ATC atc  With(Nolock) ON atc.code = RTRIM(B.CODPRODUC)
							LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
							LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
							AND phy.WarehouseId IN (SELECT Id from @Almacenes)
							LEFT OUTER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id
							LEFT JOIN dbo.HCPRESCRAEXT EXT ON A.ID = EXT.IDHCPRESCRA
							WHERE (A.IDESQUEMAONC IS NULL OR A.IDESQUEMAONC = 0)  AND B.CONSUMPTION = 0 
							GROUP BY RTRIM(B.CODPRODUC) , RTRIM(B.DESPRODUC),B.TIPPRODUC, RTRIM(A.DESADMINI), EXT.CANTIDADREPOSICION ,A.CANPEDPRO 
						   UNION 
						   --Mezclas y Liquidos que tiene paciente
					 SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(B.DESPRODUC) AS CodigoDescripcion,sum(isnull(phy.Quantity,0)) AS Disponibles,B.TIPPRODUC, RTRIM(F.ADMMEZLIQ) As 'DESADMINI' , ISNULL(EXT.CANTIDADREPOSICION,SUM(A.CANPROCAL)) AS 'Cantidad Prescrita' 
						   FROM dbo.HCINFCONC A With(Nolock) 
						   INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
						   INNER JOIN dbo.HCINFLIQA F With(Nolock) ON A.CODCONCEC=F.CODCONCEC
						   INNER JOIN Inventory.ATC AS atc With(Nolock) ON A.CODPRODUC = atc.Code  
						   LEFT OUTER JOIN Inventory.InventoryProduct AS invpro With(Nolock) ON atc.Id = invpro.ATCId
						   LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on invpro.Id = phy.ProductId
						   AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						   LEFT OUTER JOIN  Inventory.Warehouse AS E With(Nolock) ON phy.WarehouseId = E.Id
						   LEFT JOIN dbo.HCINFCONCEXT EXT on A.CODCONCEC = EXT.IDHCINFCONC
						   WHERE  F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND 
						   F.PREESTADO IN (1,5)  AND B.CONSUMPTION = 0 
						   GROUP BY RTRIM(B.CODPRODUC) , RTRIM(B.DESPRODUC),B.TIPPRODUC, F.ADMMEZLIQ, EXT.CANTIDADREPOSICION, A.CANPROCAL  
						   UNION 
					 SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,sum(ISNULL(phy.Quantity,0 )) AS Disponibles,B.TIPPRODUC, RTRIM(A.ADMMEZLIQ) As 'DESADMINI', ISNULL(EXT.CANTIDADREPOSICION,SUM(A.CANPROCAL)) AS 'Cantidad Prescrita' 
						   FROM dbo.HCINFLIQD A With(Nolock) 
						   INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
						   INNER JOIN dbo.HCINFLIQA F With(Nolock) ON A.CODCONCEC=F.CODCONCEC 
						   INNER JOIN Inventory.ATC AS atc With(Nolock) ON A.CODPRODUC = atc.Code  
						   LEFT OUTER JOIN Inventory.InventoryProduct AS invpro With(Nolock) ON atc.Id = invpro.ATCId
						   LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on invpro.Id = phy.ProductId
						   AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						   LEFT OUTER JOIN  Inventory.Warehouse AS E With(Nolock) ON phy.WarehouseId = E.Id
						   LEFT JOIN dbo.HCINFLIQDEXT EXT on F.CODCONCEC = EXT.IDHCINFLIQD
						   WHERE F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND 
						   F.PREESTADO IN (1,5)   AND B.CONSUMPTION = 0 
						   GROUP BY RTRIM(B.CODPRODUC) , RTRIM(B.DESPRODUC),B.TIPPRODUC, RTRIM(A.ADMMEZLIQ), EXT.CANTIDADREPOSICION, A.CANPROCAL  

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
						   UNION 
						   --carga los medicamento o medicamentos como insumo formulados al paciente
					  SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,sum(isnull(phy.Quantity,0)) AS Disponibles,B.TIPPRODUC , RTRIM(A.DESADMINI), isnull(EXT.CANTIDADREPOSICION,A.CANPEDPRO)  AS 'Cantidad Prescrita'
						   FROM dbo.HCPRESCRA A With(Nolock) 
							INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.IPCODPACI=@Paciente AND A.NUMINGRES=@Ingreso AND 
							A.PREESTADO IN (1,6,7)
							AND A.CODPRODUC = B.CODPRODUC AND B.TIPPRODUC = '1'  AND B.PROESTADO =1
							INNER JOIN Inventory.ATC atc  With(Nolock) ON atc.code = RTRIM(B.CODPRODUC)
							LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
							LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
							AND phy.WarehouseId IN (SELECT Id from @Almacenes)
							LEFT OUTER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id
							LEFT JOIN dbo.HCPRESCRAEXT EXT ON A.ID = EXT.IDHCPRESCRA
							WHERE (A.IDESQUEMAONC IS NULL OR A.IDESQUEMAONC = 0)  AND B.CONSUMPTION = 0 
							GROUP BY RTRIM(B.CODPRODUC) , RTRIM(B.DESPRODUC),B.TIPPRODUC, RTRIM(A.DESADMINI), EXT.CANTIDADREPOSICION , A.CANPEDPRO  
						   UNION 
						   --Mezclas y Liquidos que tiene paciente
					 SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(B.DESPRODUC) AS CodigoDescripcion,sum(isnull(phy.Quantity,0)) AS Disponibles,B.TIPPRODUC , RTRIM(F.ADMMEZLIQ) As 'DESADMINI', ISNULL(EXT.CANTIDADREPOSICION,SUM(A.CANPROCAL)) AS 'Cantidad Prescrita'
						   FROM dbo.HCINFCONC A With(Nolock) 
						   INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
						   INNER JOIN dbo.HCINFLIQA F With(Nolock) ON A.CODCONCEC=F.CODCONCEC
						   INNER JOIN Inventory.ATC AS atc With(Nolock) ON A.CODPRODUC = atc.Code  
						   LEFT OUTER JOIN Inventory.InventoryProduct AS invpro With(Nolock) ON atc.Id = invpro.ATCId
						   LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on invpro.Id = phy.ProductId
						   AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						   LEFT OUTER JOIN  Inventory.Warehouse AS E With(Nolock) ON phy.WarehouseId = E.Id  
						   LEFT JOIN dbo.HCINFCONCEXT EXT on A.CODCONCEC = EXT.IDHCINFCONC
						   WHERE  F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND 
						   F.PREESTADO IN (1,5)   AND B.CONSUMPTION = 0 
						   GROUP BY RTRIM(B.CODPRODUC) , RTRIM(B.DESPRODUC),B.TIPPRODUC, F.ADMMEZLIQ, EXT.CANTIDADREPOSICION, A.CANPROCAL  
						   UNION 
					 SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,sum(ISNULL(phy.Quantity,0 )) AS Disponibles,B.TIPPRODUC, RTRIM(A.ADMMEZLIQ) As 'DESADMINI', ISNULL(EXT.CANTIDADREPOSICION,SUM(A.CANPROCAL)) AS 'Cantidad Prescrita' 
						   FROM dbo.HCINFLIQD A With(Nolock) 
						   INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
						   INNER JOIN dbo.HCINFLIQA F With(Nolock) ON A.CODCONCEC=F.CODCONCEC 
						   INNER JOIN Inventory.ATC AS atc With(Nolock) ON A.CODPRODUC = atc.Code  
						   LEFT OUTER JOIN Inventory.InventoryProduct AS invpro With(Nolock) ON atc.Id = invpro.ATCId
						   LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on invpro.Id = phy.ProductId
						   AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						   LEFT OUTER JOIN  Inventory.Warehouse AS E With(Nolock) ON phy.WarehouseId = E.Id
						   LEFT JOIN dbo.HCINFLIQDEXT EXT on F.CODCONCEC = EXT.IDHCINFLIQD
						   WHERE F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND 
						   F.PREESTADO IN (1,5)  AND B.CONSUMPTION = 0 
						   GROUP BY RTRIM(B.CODPRODUC) , RTRIM(B.DESPRODUC),B.TIPPRODUC, RTRIM(A.ADMMEZLIQ), EXT.CANTIDADREPOSICION, A.CANPROCAL   
				END

	   END ELSE BEGIN
						
				   IF  @ParametroMostrarInsumosCantidadCero = 1 BEGIN  	
						
						--carga los medicamento como dispositivos
						SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
								from 
								dbo.IHLISTPRO D With(Nolock) 
								INNER JOIN Inventory.ATC atc With(Nolock) ON RTRIM(D.CODPRODUC) = atc.Code 
								LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
								LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
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
								INNER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
								WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1 AND D.CONSUMPTION = 0 
								GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
							   UNION 
							   --carga los medicamento o medicamentos como insumo formulados al paciente
						  SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,sum(isnull(phy.Quantity,0)) AS Disponibles,B.TIPPRODUC, RTRIM(A.DESADMINI), ISNULL(EXT.CANTIDADREPOSICION,A.CANPEDPRO) AS 'Cantidad Prescrita' 
							   FROM dbo.HCPRESCRA A With(Nolock) 
								INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.IPCODPACI=@Paciente AND A.NUMINGRES=@Ingreso AND 
								A.PREESTADO IN (1,6,7)
								AND A.CODPRODUC = B.CODPRODUC AND B.TIPPRODUC = '1'  AND B.PROESTADO =1
								INNER JOIN Inventory.ATC atc  With(Nolock) ON atc.code = RTRIM(B.CODPRODUC)
								LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
								LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
								AND phy.WarehouseId IN (SELECT Id from @Almacenes)
								LEFT OUTER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id
								LEFT JOIN dbo.HCPRESCRAEXT EXT ON A.ID = EXT.IDHCPRESCRA
								WHERE (A.IDESQUEMAONC IS NULL OR A.IDESQUEMAONC = 0) AND B.CONSUMPTION = 0 
								GROUP BY RTRIM(B.CODPRODUC) , RTRIM(B.DESPRODUC),B.TIPPRODUC, RTRIM(A.DESADMINI), EXT.CANTIDADREPOSICION , A.CANPEDPRO  
							   UNION 
							   --Mezclas y Liquidos que tiene paciente
						 SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(B.DESPRODUC) AS CodigoDescripcion,sum(isnull(phy.Quantity,0)) AS Disponibles,B.TIPPRODUC, RTRIM(F.ADMMEZLIQ) As 'DESADMINI' , ISNULL(EXT.CANTIDADREPOSICION,SUM(A.CANPROCAL)) AS 'Cantidad Prescrita' 
							   FROM dbo.HCINFCONC A With(Nolock) 
							   INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
							   INNER JOIN dbo.HCINFLIQA F With(Nolock) ON A.CODCONCEC=F.CODCONCEC
							   INNER JOIN Inventory.ATC AS atc With(Nolock) ON A.CODPRODUC = atc.Code  
							   LEFT OUTER JOIN Inventory.InventoryProduct AS invpro With(Nolock) ON atc.Id = invpro.ATCId
							   LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on invpro.Id = phy.ProductId
							   AND phy.WarehouseId IN (SELECT Id from @Almacenes)
							   LEFT OUTER JOIN  Inventory.Warehouse AS E With(Nolock) ON phy.WarehouseId = E.Id
							   LEFT JOIN dbo.HCINFCONCEXT EXT on A.CODCONCEC = EXT.IDHCINFCONC
							   WHERE  F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND 
							   F.PREESTADO IN (1,5)  AND B.CONSUMPTION = 0 
							   GROUP BY RTRIM(B.CODPRODUC) , RTRIM(B.DESPRODUC),B.TIPPRODUC, F.ADMMEZLIQ, EXT.CANTIDADREPOSICION, A.CANPROCAL    
							   UNION 
						 SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,sum(ISNULL(phy.Quantity,0 )) AS Disponibles,B.TIPPRODUC, RTRIM(A.ADMMEZLIQ) As 'DESADMINI', ISNULL(EXT.CANTIDADREPOSICION,SUM(A.CANPROCAL)) AS 'Cantidad Prescrita' 
							   FROM dbo.HCINFLIQD A With(Nolock) 
							   INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
							   INNER JOIN dbo.HCINFLIQA F With(Nolock) ON A.CODCONCEC=F.CODCONCEC 
							   INNER JOIN Inventory.ATC AS atc With(Nolock) ON A.CODPRODUC = atc.Code  
							   LEFT OUTER JOIN Inventory.InventoryProduct AS invpro With(Nolock) ON atc.Id = invpro.ATCId
							   LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on invpro.Id = phy.ProductId
							   AND phy.WarehouseId IN (SELECT Id from @Almacenes)
							   LEFT OUTER JOIN  Inventory.Warehouse AS E With(Nolock) ON phy.WarehouseId = E.Id
							   LEFT JOIN dbo.HCINFLIQDEXT EXT on F.CODCONCEC = EXT.IDHCINFLIQD
							   WHERE F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND 
							   F.PREESTADO IN (1,5) AND B.CONSUMPTION = 0 
							   GROUP BY RTRIM(B.CODPRODUC) , RTRIM(B.DESPRODUC),B.TIPPRODUC, RTRIM(A.ADMMEZLIQ), EXT.CANTIDADREPOSICION, A.CANPROCAL   
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
							   UNION 
							   --carga los medicamento o medicamentos como insumo formulados al paciente
						  SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,sum(isnull(phy.Quantity,0)) AS Disponibles,B.TIPPRODUC, RTRIM(A.DESADMINI), isnull(EXT.CANTIDADREPOSICION, A.CANPEDPRO ) AS 'Cantidad Prescrita' 
							   FROM dbo.HCPRESCRA A With(Nolock) 
								INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.IPCODPACI=@Paciente AND A.NUMINGRES=@Ingreso AND 
								A.PREESTADO IN (1,6,7)
								AND A.CODPRODUC = B.CODPRODUC AND B.TIPPRODUC = '1'  AND B.PROESTADO =1
								INNER JOIN Inventory.ATC atc  With(Nolock) ON atc.code = RTRIM(B.CODPRODUC)
								LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
								LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
								AND phy.WarehouseId IN (SELECT Id from @Almacenes)
								LEFT OUTER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id
								LEFT JOIN dbo.HCPRESCRAEXT EXT ON A.ID = EXT.IDHCPRESCRA
								WHERE (A.IDESQUEMAONC IS NULL OR A.IDESQUEMAONC = 0)  AND B.CONSUMPTION = 0 
								GROUP BY RTRIM(B.CODPRODUC) , RTRIM(B.DESPRODUC),B.TIPPRODUC, RTRIM(A.DESADMINI), EXT.CANTIDADREPOSICION ,A.CANPEDPRO  
							   UNION 
							   --Mezclas y Liquidos que tiene paciente
						 SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(B.DESPRODUC) AS CodigoDescripcion,sum(isnull(phy.Quantity,0)) AS Disponibles,B.TIPPRODUC, RTRIM(F.ADMMEZLIQ) As 'DESADMINI' , ISNULL(EXT.CANTIDADREPOSICION,SUM(A.CANPROCAL)) AS 'Cantidad Prescrita' 
							   FROM dbo.HCINFCONC A With(Nolock) 
							   INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
							   INNER JOIN dbo.HCINFLIQA F With(Nolock) ON A.CODCONCEC=F.CODCONCEC
							   INNER JOIN Inventory.ATC AS atc With(Nolock) ON A.CODPRODUC = atc.Code  
							   LEFT OUTER JOIN Inventory.InventoryProduct AS invpro With(Nolock) ON atc.Id = invpro.ATCId
							   LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on invpro.Id = phy.ProductId
							   AND phy.WarehouseId IN (SELECT Id from @Almacenes)
							   LEFT OUTER JOIN  Inventory.Warehouse AS E With(Nolock) ON phy.WarehouseId = E.Id
							   LEFT JOIN dbo.HCINFCONCEXT EXT on A.CODCONCEC = EXT.IDHCINFCONC
							   WHERE  F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND 
							   F.PREESTADO IN (1,5)  AND B.CONSUMPTION = 0 
							   GROUP BY RTRIM(B.CODPRODUC) , RTRIM(B.DESPRODUC),B.TIPPRODUC, F.ADMMEZLIQ, EXT.CANTIDADREPOSICION, A.CANPROCAL     
							   UNION 
						 SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,sum(ISNULL(phy.Quantity,0 )) AS Disponibles,B.TIPPRODUC, RTRIM(A.ADMMEZLIQ) As 'DESADMINI', ISNULL(EXT.CANTIDADREPOSICION,SUM(A.CANPROCAL)) AS 'Cantidad Prescrita' 
							   FROM dbo.HCINFLIQD A With(Nolock) 
							   INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
							   INNER JOIN dbo.HCINFLIQA F With(Nolock) ON A.CODCONCEC=F.CODCONCEC 
							   INNER JOIN Inventory.ATC AS atc With(Nolock) ON A.CODPRODUC = atc.Code  
							   LEFT OUTER JOIN Inventory.InventoryProduct AS invpro With(Nolock) ON atc.Id = invpro.ATCId
							   LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on invpro.Id = phy.ProductId
							   AND phy.WarehouseId IN (SELECT Id from @Almacenes)
							   LEFT OUTER JOIN  Inventory.Warehouse AS E With(Nolock) ON phy.WarehouseId = E.Id
							   LEFT JOIN dbo.HCINFLIQDEXT EXT on F.CODCONCEC = EXT.IDHCINFLIQD
							   WHERE F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND 
							   F.PREESTADO IN (1,5)  AND B.CONSUMPTION = 0 
							   GROUP BY RTRIM(B.CODPRODUC) , RTRIM(B.DESPRODUC),B.TIPPRODUC, RTRIM(A.ADMMEZLIQ), EXT.CANTIDADREPOSICION, A.CANPROCAL   

				END 
		END

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los medicamentos, insumos y productos farmacéuticos disponibles para administrar a un paciente específico en enfermería, combinando tres fuentes: el inventario general del almacén, las prescripciones activas del paciente (órdenes médicas vigentes) y las mezclas o preparaciones magistrales (líquidos IV y concentraciones) asociadas al ingreso hospitalario. Para cada producto retorna su código, descripción y cantidad disponible en el almacén indicado, filtrando por cédula del paciente, número de ingreso y almacén de enfermería. Soporta dos versiones del motor de inventario del ERP (VersionERP 1 y 2), adaptando la consulta de existencias según la estructura de tablas correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosPacienteEnfermeria';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosPacienteEnfermeria';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos (medicamentos, insumos, dispositivos y mezclas/líquidos) disponibles para administración por enfermería a un paciente en un ingreso, mostrando existencias en bodegas según la versión del ERP y parámetros de configuración.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe indicar la versión del ERP para escoger el flujo (1, 2 u otro flujo basado en parámetros); Para los flujos por paciente se requiere identificar paciente e ingreso; Las prescripciones consideradas deben estar activas: HCPRESCRA con estado en (1,6,7) y HCINFLIQA con estado en (1,5); Solo se consideran productos vigentes: IHLISTPRO.PROESTADO = 1 (o ''True''); En el flujo nuevo (ELSE) debe existir configuración en HCUNITHIS para el centro de atención, unidad funcional y tipo HC = ''ENF'' para leer los parámetros DEFINIRBODEGAS y MOSTRARINSUCERO; Si se usan bodegas por centro/unidad, deben existir registros en HCPARBODEGAS para CODCENATE y UFUCODIGO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Almacén/Bodega; Centro de atención; Unidad funcional; Medicamentos; Insumos; Dispositivos médicos; Prescripción médica; Mezclas y líquidos de infusión; Inventario físico; Cantidad de reposición; Cantidad prescrita; Esquema oncológico; Enfermería', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Result set: Si @VersionERP=1: retorna unión de productos tipo 2 y 3 disponibles en el almacén indicado, más medicamentos prescritos al paciente/ingreso (HCPRESCRA con PREESTADO IN (1,6,7), tipo 1 y 3) y los productos de mezclas/líquidos del paciente (HCINFCONC/HCINFLIQD con HCINFLIQA.PREESTADO IN (1,5)), con cantidades sumadas desde INFISICO.; [RETURN_RESULT] Result set: Si @VersionERP=2: retorna los mismos conjuntos pero contra el inventario nuevo (INNPRODUC/INNFISICO/INNALMACE). Para tipo 2 y 3, o cuando ESPDILPRO=''1'' y el almacén coincide con @Almacen, devuelve las existencias agrupadas; si la cantidad es NULL retorna 0.; [INSERT] @Almacenes: En el flujo ELSE: si @CentroAtencion no es nulo y no vacío, inserta los Id de Inventory.Warehouse cuyo CodeCenterAttention = @CentroAtencion; además siempre inserta los Id de Warehouse cuyo CodeCenterAttention IS NULL.; [RETURN_RESULT] Result set: En el flujo ELSE con @ParametroDefinirBodegasCantidadInsumos=0 y @ParametroMostrarInsumosCantidadCero=1: retorna medicamentos (TIPPRODUC=''3'' o ESPDILPRO=1), insumos (TIPPRODUC=''2''), prescripciones tipo ''1'' y mezclas/líquidos del paciente, sumando phy.Quantity (NULL→0) sin filtrar por cantidad>0.; [RETURN_RESULT] Result set: En el flujo ELSE con @ParametroDefinirBodegasCantidadInsumos=0 y @ParametroMostrarInsumosCantidadCero=0: aplica los mismos conjuntos pero filtra phy.Quantity > 0 en los productos de catálogo (medicamentos e insumos), excluyendo los de existencia cero.; [RETURN_RESULT] Result set: En el flujo ELSE con @ParametroDefinirBodegasCantidadInsumos=1: restringe las bodegas (Inventory.Warehouse.Code) a las definidas en HCPARBODEGAS para CODCENATE=@CentroAtencion y UFUCODIGO=@UnidadFuncional, y según @ParametroMostrarInsumosCantidadCero filtra o no por phy.Quantity>0.; [RETURN_RESULT] Result set: Para prescripciones HCPRESCRA solo se incluyen las que NO pertenecen a esquema oncológico: (A.IDESQUEMAONC IS NULL OR A.IDESQUEMAONC = 0).; [RETURN_RESULT] Result set: La ''Cantidad Prescrita'' se toma de HCPRESCRAEXT.CANTIDADREPOSICION (o HCINFCONCEXT/HCINFLIQDEXT) y, si es nula, se usa CANPEDPRO o SUM(CANPROCAL) según la fuente.; [RETURN_RESULT] Result set: En todos los conjuntos del flujo ELSE se excluyen productos cuyo IHLISTPRO.CONSUMPTION = 1 (solo se incluye CONSUMPTION = 0).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHLISTPRO; dbo.IHRINDDGH; dbo.INFISICO; dbo.HCPRESCRA; dbo.HCINFCONC; dbo.HCINFLIQA; dbo.HCINFLIQD; dbo.INNPRODUC; dbo.INNFISICO; dbo.INNALMACE; Inventory.Warehouse; Inventory.ATC; Inventory.InventoryProduct; Inventory.PhysicalInventory; Inventory.InventorySupplie; dbo.HCPRESCRAEXT; dbo.HCINFCONCEXT; dbo.HCINFLIQDEXT; dbo.HCUNITHIS; dbo.HCPARBODEGAS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeria';
-- GO
