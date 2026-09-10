

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los medicamentos e insumos disponibles para administración de enfermería a un paciente en un ingreso hospitalario específico, consultando tanto el inventario físico del almacén como las prescripciones activas, mezclas magistrales y líquidos parenterales ordenados al paciente. Combina el catálogo de productos farmacéuticos (medicamentos, diluyentes e insumos por tipo), el stock disponible en el almacén indicado, las prescripciones vigentes del paciente (por cédula y número de ingreso), y las concentraciones o preparaciones magistrales asociadas a su ingreso. Soporta dos versiones del ERP (v1 con inventario clásico INFISICO, v2 con inventario nuevo INNFISICO/INNPRODUC) y devuelve código, descripción y cantidad disponible de cada producto, permitiendo que enfermería seleccione y registre la administración de medicamentos desde la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosPacienteEnfermeriaTodosMedicamentos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosPacienteEnfermeriaTodosMedicamentos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos (medicamentos, insumos, mezclas y líquidos) disponibles para administración de enfermería a un paciente, consolidando existencias de inventario y prescripciones según la versión del ERP y parámetros configurados por centro/unidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaTodosMedicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de versión de ERP determina la rama de consulta (1, 2 o cualquier otro valor cae en la rama de inventario nuevo); Para la rama por defecto (no v1, no v2) se requiere que existan registros en HCUNITHIS para el centro de atención y unidad funcional con CODTIPHIS=''ENF'' para leer parámetros DEFINIRBODEGAS y MOSTRARINSUCERO; Si se proporciona CentroAtencion no vacío, se cargan bodegas de Inventory.Warehouse cuyo CodeCenterAttention coincida; siempre se agregan también las bodegas con CodeCenterAttention NULL; Para resultados con prescripciones del paciente, se requiere coincidencia de IPCODPACI y NUMINGRES con el ingreso del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaTodosMedicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven productos activos (PROESTADO=1 o ''1'' / ''True'' según versión); Los productos con CONSUMPTION=1 nunca se incluyen en la rama de inventario nuevo; Las prescripciones consideradas para HCPRESCRA siempre están en estado 1, 6 o 7; Las mezclas/líquidos considerados (HCINFLIQA) siempre están en estado 1 o 5; La disponibilidad nula se normaliza a 0 mediante ISNULL/COALESCE; Las prescripciones siempre se filtran por el paciente e ingreso recibidos; Las consultas sobre inventario nuevo siempre se ejecutan con WITH(NOLOCK); Siempre se agregan al conjunto de bodegas evaluadas las que tengan CodeCenterAttention NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaTodosMedicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Si VersionERP=1: devuelve productos con disponibilidad sumada de INFISICO filtrando por almacén, uniendo medicamentos/dispositivos activos (PROESTADO=''1'') tipo 2/3, prescripciones HCPRESCRA con PREESTADO IN (''1'',''6'',''7'') tipo 1/3, y mezclas/líquidos HCINFCONC/HCINFLIQD con HCINFLIQA.PREESTADO IN (''1'',''5''); [RETURN_RESULT] RESULTSET: Si VersionERP=2: devuelve productos usando INNPRODUC/INNFISICO/INNALMACE; incluye productos cuyo OID está en el almacén indicado o que no tienen registro en INNFISICO, con TIPPRODUC IN (''1'',''3''), PROESTADO=1 y ESPDILPRO=''0''; [INSERT] @Almacenes: Cuando @CentroAtencion no es nulo ni vacío, inserta los Id de Inventory.Warehouse cuyo CodeCenterAttention coincide; adicionalmente siempre inserta los Id cuyo CodeCenterAttention IS NULL; [RETURN_RESULT] RESULTSET: Cuando DEFINIRBODEGAS=0 y MOSTRARINSUCERO=1: devuelve medicamentos (TIPPRODUC 1/3 o ESPDILPRO=1) e insumos (TIPPRODUC=2) activos con PROESTADO=1 y CONSUMPTION=0 con cantidad incluso en cero, más prescripciones del paciente y mezclas/líquidos; [RETURN_RESULT] RESULTSET: Cuando DEFINIRBODEGAS=0 y MOSTRARINSUCERO=0: igual que el caso anterior pero filtrando phy.Quantity > 0 (oculta insumos con existencia cero); [RETURN_RESULT] RESULTSET: Cuando DEFINIRBODEGAS=1: restringe Inventory.Warehouse a las bodegas (E.Code) configuradas en HCPARBODEGAS para el CODCENATE y UFUCODIGO recibidos, combinándolo con la regla de MOSTRARINSUCERO para mostrar o no productos con cantidad cero; [RETURN_RESULT] RESULTSET: En todas las ramas se incluyen prescripciones de HCPRESCRA con PREESTADO IN (1,6,7) y mezclas/líquidos de HCINFLIQA con PREESTADO IN (1,5) del paciente e ingreso recibidos, descartando productos con CONSUMPTION=1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaTodosMedicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @VersionERP = 1 → Consulta sobre el modelo legado de inventario (INFISICO/IHRINDDGH) filtrado por @Almacen; si @VersionERP = 2 → Consulta sobre el modelo INN* (INNPRODUC/INNFISICO/INNALMACE) con regla de inclusión de productos sin registro físico; si @VersionERP distinto de 1 y 2 → Carga bodegas en variable de tabla y ramifica según parámetros DEFINIRBODEGAS y MOSTRARINSUCERO de HCUNITHIS para usar inventario nuevo (Inventory.*); si @CentroAtencion no nulo y con longitud > 0 → Inserta en @Almacenes las bodegas asociadas al centro de atención else No inserta bodegas filtradas por centro, solo las de centro NULL; si @ParametroDefinirBodegasCantidadInsumos = 0 → Usa todas las bodegas cargadas en @Almacenes sin restringir por HCPARBODEGAS else Restringe a bodegas cuyo Code esté en HCPARBODEGAS para el centro y unidad funcional; si @ParametroMostrarInsumosCantidadCero = 1 → Incluye productos aunque phy.Quantity sea 0 (devuelve disponibles en 0) else Filtra phy.Quantity > 0, excluyendo productos sin existencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaTodosMedicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaTodosMedicamentos';
-- GO
