

CREATE PROCEDURE [dbo].[SP_HC_ListarProductosPacienteEnfermeriaTodosMedicamentos]
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
       FROM dbo.IHLISTPRO A 
       INNER JOIN  dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
       INNER JOIN  dbo.INFISICO C ON B.IPRCODIGO=C.IPRCODIGO 
       WHERE C.IALCODIGO=@Almacen AND A.TIPPRODUC IN ('2','3')  and a.PROESTADO='1'
       GROUP BY A.CODPRODUC,DESPRODUC,A.TIPPRODUC
       UNION 
       SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,SUM(CAST(D.IFICANTID AS INT)) AS Disponibles,B.TIPPRODUC 
       FROM dbo.HCPRESCRA A 
       INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
       INNER JOIN  dbo.IHRINDDGH AS C ON B.CODPRODUC=C.CODPRODUC
       INNER JOIN dbo.INFISICO D ON C.IPRCODIGO=D.IPRCODIGO 
       WHERE D.IALCODIGO=@Almacen AND A.PREESTADO IN ('1','6','7') AND IPCODPACI=@Paciente AND NUMINGRES=@Ingreso  AND B.TIPPRODUC IN ('1','3')  and b.PROESTADO='1'
       GROUP BY B.CODPRODUC,DESPRODUC,B.TIPPRODUC
       UNION 
       SELECT RTRIM(C.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(C.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,SUM(CAST(D.IFICANTID AS INT)) AS Disponibles,B.TIPPRODUC 
       FROM dbo.HCINFCONC A 
       INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
       INNER JOIN dbo.IHRINDDGH AS C ON B.CODPRODUC=C.CODPRODUC
       INNER JOIN dbo.INFISICO D ON C.IPRCODIGO=D.IPRCODIGO 
       INNER JOIN dbo.HCINFLIQA E ON A.CODCONCEC=E.CODCONCEC 
       WHERE D.IALCODIGO=@Almacen AND E.IPCODPACI=@Paciente AND E.NUMINGRES=@Ingreso AND E.PREESTADO IN ('1','5')  and b.PROESTADO='1'
       GROUP BY C.CODPRODUC,DESPRODUC,B.TIPPRODUC
       UNION 
       SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,SUM(CAST(D.IFICANTID AS INT)) AS Disponibles,B.TIPPRODUC 
       FROM dbo.HCINFLIQD A 
       INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
       INNER JOIN dbo.IHRINDDGH AS C ON B.CODPRODUC=C.CODPRODUC
       INNER JOIN dbo.INFISICO D ON C.IPRCODIGO=D.IPRCODIGO 
       INNER JOIN dbo.HCINFLIQA E ON A.CODCONCEC=E.CODCONCEC 
       WHERE D.IALCODIGO=@Almacen AND E.IPCODPACI=@Paciente AND E.NUMINGRES=@Ingreso AND E.PREESTADO IN ('1','5') and b.PROESTADO='1'
       GROUP BY B.CODPRODUC,DESPRODUC,B.TIPPRODUC


ELSE IF @VersionERP=2
			/*aca la consulta*/

		SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(A.DESPRODUC) AS Descripcion,
		RTRIM(A.CODPRODUC)+' - '+RTRIM(A.DESPRODUC) AS CodigoDescripcion,CASE WHEN SUM(CAST(C.IFICANTID AS INT)) IS NULL THEN 0 ELSE  SUM(CAST(C.IFICANTID AS INT)) END  AS Disponibles,A.TIPPRODUC 
		FROM IHLISTPRO A 
		INNER JOIN IHRINDDGH B ON A.CODPRODUC=B.CODPRODUC 
		LEFT OUTER JOIN dbo.INNPRODUC P ON P.IPRCODIGO=B.IPRCODIGO 
		LEFT OUTER JOIN dbo.INNFISICO C ON P.OID=C.INNPRODUC 
		LEFT OUTER JOIN dbo.INNALMACE D ON C.INNALMACE=D.OID 
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

	   select A.Codigo, A.Medicamento as Descripcion, A.Codigo + ' - ' + A.Medicamento AS CodigoDescripcion , A.Disponibles, A.TIPPRODUC from
	   (SELECT MAX( B.IPRCODIGO) AS IPRCODIGO ,RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,NOPOSPROD AS 'NO POS'
			,SUM(CAST(COALESCE(NULLIF(D.IFICANTID,0),0) AS INT)) AS Disponibles,A.TIPPRODUC,TIPFORMED,CODGRUFAR,A.CODJUMEES As JustificacionMedicamentosEspeciales
		FROM dbo.IHLISTPRO A 
		inner join dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
	inner join dbo.INNPRODUC C ON B.IPRCODIGO=C.IPRCODIGO 
	left outer join dbo.INNFISICO D on D.INNPRODUC=C.OID
				WHERE (c.OID in (select  INNPRODUC from dbo.INNFISICO a inner join
				dbo.INNALMACE b on a.INNALMACE=b.OID where b.IALCODIGO=@Almacen) or  c.OID not in (select  INNPRODUC from dbo.INNFISICO)) 
				 AND TIPPRODUC IN ('1','3') AND PROESTADO = 1 AND ESPDILPRO='0'
		GROUP BY A.CODPRODUC,DESPRODUC,NOPOSPROD,A.TIPPRODUC,TIPFORMED,CODGRUFAR,A.CODJUMEES) A
		    
       UNION 
       SELECT RTRIM(C.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(C.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,CASE WHEN SUM(CAST(E.IFICANTID AS INT)) IS NULL THEN 0 ELSE  SUM(CAST(E.IFICANTID AS INT)) END  AS Disponibles,B.TIPPRODUC 
       FROM dbo.HCINFCONC A 
       INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
       INNER JOIN dbo.IHRINDDGH AS C ON B.CODPRODUC=C.CODPRODUC
	   LEFT OUTER JOIN dbo.INNPRODUC AS D ON A.CODPRODUC=D.IPRCODIGO
     LEFT OUTER JOIN dbo.INNFISICO E ON D.OID=E.INNPRODUC -- AND E.INNALMACE=(select oid from dbo.INNALMACE where IALCODIGO=@Almacen)
       INNER JOIN dbo.HCINFLIQA F ON A.CODCONCEC=F.CODCONCEC 
       WHERE IFICANTID IS NOT NULL AND  F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND F.PREESTADO IN ('1','5')  
       GROUP BY C.CODPRODUC,DESPRODUC,B.TIPPRODUC
       UNION 
       SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,CASE WHEN SUM(CAST(E.IFICANTID AS INT)) IS NULL THEN 0 ELSE  SUM(CAST(E.IFICANTID AS INT)) END  AS Disponibles,B.TIPPRODUC 
       FROM dbo.HCINFLIQD A 
       INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
       INNER JOIN dbo.IHRINDDGH AS C ON B.CODPRODUC=C.CODPRODUC
	 LEFT OUTER JOIN dbo.INNPRODUC AS D ON A.CODPRODUC=D.IPRCODIGO
         LEFT OUTER JOIN dbo.INNFISICO E ON D.OID=E.INNPRODUC --AND E.INNALMACE=(select oid from dbo.INNALMACE where IALCODIGO=@Almacen)
       INNER JOIN dbo.HCINFLIQA F ON A.CODCONCEC=F.CODCONCEC 
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

	
					IF @ParametroMostrarInsumosCantidadCero = 1 BEGIN 
						--carga los medicamento como insumos
						SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
						FROM dbo.IHLISTPRO D With(Nolock) 
						INNER JOIN Inventory.ATC atc WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = atc.Code 
						LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
						WHERE (D.TIPPRODUC IN ('1','3') OR D.ESPDILPRO = 1) AND D.PROESTADO=1 AND D.CONSUMPTION = 0
 						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
						union
						--carga los insumos
						select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles, D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
						From 
						dbo.IHLISTPRO D With(Nolock) 
						INNER JOIN Inventory.InventorySupplie s With(Nolock) ON RTRIM(D.CODPRODUC) = s.Code 
						LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id
						WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1 AND D.CONSUMPTION = 0
						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
					   UNION 
					   --carga los medicamento o medicamentos como insumo formulados al paciente
					   SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,sum(isnull(phy.Quantity,0)) AS Disponibles,B.TIPPRODUC, RTRIM(A.DESADMINI), EXT.CANTIDADREPOSICION AS 'Cantidad Prescrita'
					   FROM 
					   dbo.HCPRESCRA A With(Nolock) 
						INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.IPCODPACI=@Paciente AND A.NUMINGRES=@Ingreso AND 
						A.PREESTADO IN (1,6,7) AND B.CONSUMPTION = 0
						AND A.CODPRODUC = B.CODPRODUC AND B.TIPPRODUC = '1'  AND B.PROESTADO = 1
						INNER JOIN Inventory.ATC atc With(Nolock) ON atc.code = RTRIM(B.CODPRODUC)
						LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id
						LEFT JOIN dbo.HCPRESCRAEXT EXT ON A.ID = EXT.IDHCPRESCRA
						GROUP BY RTRIM(B.CODPRODUC) , RTRIM(B.DESPRODUC),B.TIPPRODUC, RTRIM(A.DESADMINI), EXT.CANTIDADREPOSICION 
					   UNION 
					   --Mezclas y Liquidos que tiene paciente
					   SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(B.DESPRODUC) AS CodigoDescripcion,sum(isnull(phy.Quantity,0)) AS Disponibles,B.TIPPRODUC, RTRIM(F.ADMMEZLIQ) As 'DESADMINI' , ISNULL(EXT.CANTIDADREPOSICION,SUM(A.CANPROCAL)) AS 'Cantidad Prescrita' 
					   FROM dbo.HCINFCONC A With(Nolock)
					   INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
					   INNER JOIN dbo.HCINFLIQA F With(Nolock) ON A.CODCONCEC=F.CODCONCEC
					   INNER JOIN Inventory.ATC atc With(Nolock) ON A.CODPRODUC = atc.Code  
					   LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) ON atc.Id = invpro.ATCId
					   LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on invpro.Id = phy.ProductId
					   AND phy.WarehouseId IN (SELECT Id from @Almacenes)
					   LEFT OUTER JOIN  Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id
					   LEFT JOIN dbo.HCINFCONCEXT EXT on A.CODCONCEC = EXT.IDHCINFCONC
					   WHERE  F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND 
					   F.PREESTADO IN (1,5)  AND B.CONSUMPTION = 0
					   GROUP BY B.CODPRODUC,DESPRODUC,B.TIPPRODUC, F.ADMMEZLIQ, EXT.CANTIDADREPOSICION, A.CANPROCAL
					   UNION 
					   SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,sum(ISNULL(phy.Quantity,0 )) AS Disponibles,B.TIPPRODUC, RTRIM(A.ADMMEZLIQ) As 'DESADMINI', ISNULL(EXT.CANTIDADREPOSICION,SUM(A.CANPROCAL)) AS 'Cantidad Prescrita' 
					   FROM dbo.HCINFLIQD A With(Nolock)
					   INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
					   INNER JOIN dbo.HCINFLIQA F With(Nolock) ON A.CODCONCEC=F.CODCONCEC 
					   INNER JOIN Inventory.ATC atc With(Nolock) ON A.CODPRODUC = atc.Code  
					   LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) ON atc.Id = invpro.ATCId
					   LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on invpro.Id = phy.ProductId
					   AND phy.WarehouseId IN (SELECT Id from @Almacenes)
					   LEFT OUTER JOIN  Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id
					   LEFT JOIN dbo.HCINFLIQDEXT EXT on F.CODCONCEC = EXT.IDHCINFLIQD
					   WHERE F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND 
					   F.PREESTADO IN (1,5)  AND B.CONSUMPTION = 0
					   GROUP BY B.CODPRODUC,DESPRODUC,B.TIPPRODUC, RTRIM(A.ADMMEZLIQ), EXT.CANTIDADREPOSICION, A.CANPROCAL

				END ELSE BEGIN

					   	--carga los medicamento como insumos
						SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
						FROM dbo.IHLISTPRO D With(Nolock) 
						INNER JOIN Inventory.ATC atc WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = atc.Code 
						LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
						WHERE (D.TIPPRODUC IN ('1','3') OR D.ESPDILPRO = 1) AND D.PROESTADO=1 AND phy.Quantity > 0 AND D.CONSUMPTION = 0
 						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
						union
						--carga los insumos
						select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles, D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
						From 
						dbo.IHLISTPRO D With(Nolock) 
						INNER JOIN Inventory.InventorySupplie s With(Nolock) ON RTRIM(D.CODPRODUC) = s.Code 
						LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id
						WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1 AND phy.Quantity > 0  AND D.CONSUMPTION = 0
						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
					   UNION 
					   --carga los medicamento o medicamentos como insumo formulados al paciente
					   SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,sum(isnull(phy.Quantity,0)) AS Disponibles,B.TIPPRODUC, RTRIM(A.DESADMINI), EXT.CANTIDADREPOSICION AS 'Cantidad Prescrita' 
					   FROM 
					   dbo.HCPRESCRA A With(Nolock) 
						INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.IPCODPACI=@Paciente AND A.NUMINGRES=@Ingreso AND 
						A.PREESTADO IN (1,6,7) AND B.CONSUMPTION = 0
						AND A.CODPRODUC = B.CODPRODUC AND B.TIPPRODUC = '1'  AND B.PROESTADO = 1
						INNER JOIN Inventory.ATC atc With(Nolock) ON atc.code = RTRIM(B.CODPRODUC)
						LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id
						LEFT JOIN dbo.HCPRESCRAEXT EXT ON A.ID = EXT.IDHCPRESCRA
			            GROUP BY RTRIM(B.CODPRODUC) , RTRIM(B.DESPRODUC),B.TIPPRODUC, RTRIM(A.DESADMINI), EXT.CANTIDADREPOSICION  
					   UNION 
					   --Mezclas y Liquidos que tiene paciente
					   SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(B.DESPRODUC) AS CodigoDescripcion,sum(isnull(phy.Quantity,0)) AS Disponibles,B.TIPPRODUC, RTRIM(F.ADMMEZLIQ) As 'DESADMINI', ISNULL(EXT.CANTIDADREPOSICION,SUM(A.CANPROCAL)) AS 'Cantidad Prescrita' 
					   FROM dbo.HCINFCONC A With(Nolock)
					   INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
					   INNER JOIN dbo.HCINFLIQA F With(Nolock) ON A.CODCONCEC=F.CODCONCEC
					   INNER JOIN Inventory.ATC atc With(Nolock) ON A.CODPRODUC = atc.Code  
					   LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) ON atc.Id = invpro.ATCId
					   LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on invpro.Id = phy.ProductId
					   AND phy.WarehouseId IN (SELECT Id from @Almacenes)
					   LEFT OUTER JOIN  Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id
					   LEFT JOIN dbo.HCINFCONCEXT EXT on A.CODCONCEC = EXT.IDHCINFCONC
					   WHERE  F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND 
					   F.PREESTADO IN (1,5)  AND B.CONSUMPTION = 0
					   GROUP BY B.CODPRODUC,DESPRODUC,B.TIPPRODUC, F.ADMMEZLIQ, EXT.CANTIDADREPOSICION, A.CANPROCAL
					   UNION 
					   SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,sum(ISNULL(phy.Quantity,0 )) AS Disponibles,B.TIPPRODUC, RTRIM(A.ADMMEZLIQ) As 'DESADMINI', ISNULL(EXT.CANTIDADREPOSICION,SUM(A.CANPROCAL)) AS 'Cantidad Prescrita' 
					   FROM dbo.HCINFLIQD A With(Nolock)
					   INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
					   INNER JOIN dbo.HCINFLIQA F With(Nolock) ON A.CODCONCEC=F.CODCONCEC 
					   INNER JOIN Inventory.ATC atc With(Nolock) ON A.CODPRODUC = atc.Code  
					   LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) ON atc.Id = invpro.ATCId
					   LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on invpro.Id = phy.ProductId
					   AND phy.WarehouseId IN (SELECT Id from @Almacenes)
					   LEFT OUTER JOIN  Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id
					   LEFT JOIN dbo.HCINFLIQDEXT EXT on F.CODCONCEC = EXT.IDHCINFLIQD
					   WHERE F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND 
					   F.PREESTADO IN (1,5) AND B.CONSUMPTION = 0
					   GROUP BY B.CODPRODUC,DESPRODUC,B.TIPPRODUC, RTRIM(A.ADMMEZLIQ), EXT.CANTIDADREPOSICION, A.CANPROCAL
				   END 

	   END ELSE BEGIN
					--leemos las Bodegas configuardas en la tabla HCPARBODEGAS


					IF @ParametroMostrarInsumosCantidadCero = 1 BEGIN 
				
						--carga los medicamento como insumos
						SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
						FROM dbo.IHLISTPRO D With(Nolock) 
						INNER JOIN Inventory.ATC atc WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = atc.Code 
						LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes) 
						INNER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
						WHERE (D.TIPPRODUC IN ('1','3') OR D.ESPDILPRO = 1) AND D.PROESTADO=1 AND D.CONSUMPTION = 0
 						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
						union
						--carga los insumos
						select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles, D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
						From 
						dbo.IHLISTPRO D With(Nolock) 
						INNER JOIN Inventory.InventorySupplie s With(Nolock) ON RTRIM(D.CODPRODUC) = s.Code 
						LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						INNER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
						WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1 AND D.CONSUMPTION = 0
						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
					   UNION 
					   --carga los medicamento o medicamentos como insumo formulados al paciente
					   SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,sum(isnull(phy.Quantity,0)) AS Disponibles,B.TIPPRODUC, RTRIM(A.DESADMINI), EXT.CANTIDADREPOSICION AS 'Cantidad Prescrita' 
					   FROM 
					   dbo.HCPRESCRA A With(Nolock) 
						INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.IPCODPACI=@Paciente AND A.NUMINGRES=@Ingreso AND 
						A.PREESTADO IN (1,6,7) AND B.CONSUMPTION = 0
						AND A.CODPRODUC = B.CODPRODUC AND B.TIPPRODUC = '1'  AND B.PROESTADO = 1
						INNER JOIN Inventory.ATC atc With(Nolock) ON atc.code = RTRIM(B.CODPRODUC)
						LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id
						LEFT JOIN dbo.HCPRESCRAEXT EXT ON A.ID = EXT.IDHCPRESCRA
						GROUP BY RTRIM(B.CODPRODUC) , RTRIM(B.DESPRODUC),B.TIPPRODUC, RTRIM(A.DESADMINI), EXT.CANTIDADREPOSICION 
					   UNION 
					   --Mezclas y Liquidos que tiene paciente
					   SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(B.DESPRODUC) AS CodigoDescripcion,sum(isnull(phy.Quantity,0)) AS Disponibles,B.TIPPRODUC, RTRIM(F.ADMMEZLIQ) As 'DESADMINI' , ISNULL(EXT.CANTIDADREPOSICION,SUM(A.CANPROCAL)) AS 'Cantidad Prescrita' 
					   FROM dbo.HCINFCONC A With(Nolock)
					   INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
					   INNER JOIN dbo.HCINFLIQA F With(Nolock) ON A.CODCONCEC=F.CODCONCEC
					   INNER JOIN Inventory.ATC atc With(Nolock) ON A.CODPRODUC = atc.Code  
					   LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) ON atc.Id = invpro.ATCId
					   LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on invpro.Id = phy.ProductId
					   AND phy.WarehouseId IN (SELECT Id from @Almacenes)
					   LEFT OUTER JOIN  Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id
					   LEFT JOIN dbo.HCINFCONCEXT EXT on A.CODCONCEC = EXT.IDHCINFCONC
					   WHERE  F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND 
					   F.PREESTADO IN (1,5)  AND B.CONSUMPTION = 0
					   GROUP BY B.CODPRODUC,DESPRODUC,B.TIPPRODUC, F.ADMMEZLIQ, EXT.CANTIDADREPOSICION, A.CANPROCAL
					   UNION 
					   SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,sum(ISNULL(phy.Quantity,0 )) AS Disponibles,B.TIPPRODUC, RTRIM(A.ADMMEZLIQ) As 'DESADMINI', ISNULL(EXT.CANTIDADREPOSICION,SUM(A.CANPROCAL)) AS 'Cantidad Prescrita' 
					   FROM dbo.HCINFLIQD A With(Nolock)
					   INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
					   INNER JOIN dbo.HCINFLIQA F With(Nolock) ON A.CODCONCEC=F.CODCONCEC 
					   INNER JOIN Inventory.ATC atc With(Nolock) ON A.CODPRODUC = atc.Code  
					   LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) ON atc.Id = invpro.ATCId
					   LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on invpro.Id = phy.ProductId
					   AND phy.WarehouseId IN (SELECT Id from @Almacenes)
					   LEFT OUTER JOIN  Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id
					   LEFT JOIN dbo.HCINFLIQDEXT EXT on F.CODCONCEC = EXT.IDHCINFLIQD
					   WHERE F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND 
					   F.PREESTADO IN (1,5) AND B.CONSUMPTION = 0
					   GROUP BY B.CODPRODUC,DESPRODUC,B.TIPPRODUC, RTRIM(A.ADMMEZLIQ), EXT.CANTIDADREPOSICION, A.CANPROCAL

					  
				   END ELSE BEGIN
				    
					       --carga los medicamento como insumos
							SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
							FROM dbo.IHLISTPRO D With(Nolock) 
							INNER JOIN Inventory.ATC atc WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = atc.Code 
							LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
							LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on phy.ProductId = invpro.Id
							AND phy.WarehouseId IN (SELECT Id from @Almacenes) 
							INNER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
							WHERE (D.TIPPRODUC IN ('1','3') OR D.ESPDILPRO = 1) AND D.PROESTADO=1 AND phy.Quantity > 0 AND D.CONSUMPTION = 0
 							GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
							union
							--carga los insumos
							select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles, D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
							From 
							dbo.IHLISTPRO D With(Nolock) 
							INNER JOIN Inventory.InventorySupplie s With(Nolock) ON RTRIM(D.CODPRODUC) = s.Code 
							LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
							LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on phy.ProductId = invpro.Id
							AND phy.WarehouseId IN (SELECT Id from @Almacenes)
							INNER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
							WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1 AND phy.Quantity > 0  AND D.CONSUMPTION = 0
							GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
						   UNION 
						   --carga los medicamento o medicamentos como insumo formulados al paciente
						   SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,sum(isnull(phy.Quantity,0)) AS Disponibles,B.TIPPRODUC, RTRIM(A.DESADMINI), EXT.CANTIDADREPOSICION AS 'Cantidad Prescrita' 
						   FROM 
						   dbo.HCPRESCRA A With(Nolock) 
							INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.IPCODPACI=@Paciente AND A.NUMINGRES=@Ingreso AND 
							A.PREESTADO IN (1,6,7) AND B.CONSUMPTION = 0
							AND A.CODPRODUC = B.CODPRODUC AND B.TIPPRODUC = '1'  AND B.PROESTADO = 1
							INNER JOIN Inventory.ATC atc With(Nolock) ON atc.code = RTRIM(B.CODPRODUC)
							LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
							LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on phy.ProductId = invpro.Id
							AND phy.WarehouseId IN (SELECT Id from @Almacenes)
							LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id
							LEFT JOIN dbo.HCPRESCRAEXT EXT ON A.ID = EXT.IDHCPRESCRA
							GROUP BY RTRIM(B.CODPRODUC) , RTRIM(B.DESPRODUC),B.TIPPRODUC, RTRIM(A.DESADMINI), EXT.CANTIDADREPOSICION 
						   UNION 
						   --Mezclas y Liquidos que tiene paciente
						   SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(B.DESPRODUC) AS CodigoDescripcion,sum(isnull(phy.Quantity,0)) AS Disponibles,B.TIPPRODUC, RTRIM(F.ADMMEZLIQ) As 'DESADMINI' , ISNULL(EXT.CANTIDADREPOSICION,SUM(A.CANPROCAL)) AS 'Cantidad Prescrita' 
						   FROM dbo.HCINFCONC A With(Nolock)
						   INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
						   INNER JOIN dbo.HCINFLIQA F With(Nolock) ON A.CODCONCEC=F.CODCONCEC
						   INNER JOIN Inventory.ATC atc With(Nolock) ON A.CODPRODUC = atc.Code  
						   LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) ON atc.Id = invpro.ATCId
						   LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on invpro.Id = phy.ProductId
						   AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						   LEFT OUTER JOIN  Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id
						   LEFT JOIN dbo.HCINFCONCEXT EXT on A.CODCONCEC = EXT.IDHCINFCONC
						   WHERE  F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND 
						   F.PREESTADO IN (1,5)  AND B.CONSUMPTION = 0
						   GROUP BY B.CODPRODUC,DESPRODUC,B.TIPPRODUC, F.ADMMEZLIQ, EXT.CANTIDADREPOSICION, A.CANPROCAL
						   UNION 
						   SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,sum(ISNULL(phy.Quantity,0 )) AS Disponibles,B.TIPPRODUC, RTRIM(A.ADMMEZLIQ) As 'DESADMINI', ISNULL(EXT.CANTIDADREPOSICION,SUM(A.CANPROCAL)) AS 'Cantidad Prescrita' 
						   FROM dbo.HCINFLIQD A With(Nolock)
						   INNER JOIN dbo.IHLISTPRO B With(Nolock) ON A.CODPRODUC=B.CODPRODUC 
						   INNER JOIN dbo.HCINFLIQA F With(Nolock) ON A.CODCONCEC=F.CODCONCEC 
						   INNER JOIN Inventory.ATC atc With(Nolock) ON A.CODPRODUC = atc.Code  
						   LEFT OUTER JOIN Inventory.InventoryProduct invpro With(Nolock) ON atc.Id = invpro.ATCId
						   LEFT OUTER JOIN Inventory.PhysicalInventory phy With(Nolock) on invpro.Id = phy.ProductId
						   AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						   LEFT OUTER JOIN  Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id
						   LEFT JOIN dbo.HCINFLIQDEXT EXT on F.CODCONCEC = EXT.IDHCINFLIQD
						   WHERE F.IPCODPACI=@Paciente AND F.NUMINGRES=@Ingreso AND 
						   F.PREESTADO IN (1,5) AND B.CONSUMPTION = 0 
						   GROUP BY B.CODPRODUC,DESPRODUC,B.TIPPRODUC, RTRIM(A.ADMMEZLIQ), EXT.CANTIDADREPOSICION, A.CANPROCAL

				   END
	   END
END