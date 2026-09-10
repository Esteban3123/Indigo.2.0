
CREATE PROCEDURE [dbo].[SP_HC_ListarProductosPacienteEnfermeriaUnidadQX]
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
						from 
						dbo.IHLISTPRO D With(Nolock)
						INNER JOIN Inventory.ATC atc  With(Nolock) ON RTRIM(D.CODPRODUC) = atc.Code 
						LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
						WHERE (D.TIPPRODUC IN ('1', '3') OR D.ESPDILPRO = 1) AND D.PROESTADO = 1 AND D.CONSUMPTION = 0 
 						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
						union
						--carga los insumos
						select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
						From dbo.IHLISTPRO D With(Nolock) 
						INNER JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = s.Code 
						LEFT OUTER JOIN  Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						LEFT OUTER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id
						WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1 AND D.CONSUMPTION = 0 
						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  

				END ELSE BEGIN
						--carga los medicamento como insumos
						SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
						from 
						dbo.IHLISTPRO D With(Nolock)
						INNER JOIN Inventory.ATC atc  With(Nolock) ON RTRIM(D.CODPRODUC) = atc.Code 
						LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
						WHERE (D.TIPPRODUC IN ('1', '3') OR D.ESPDILPRO = 1) AND D.PROESTADO = 1 AND D.CONSUMPTION = 0 
 						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
						union
						--carga los insumos
						select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
						From dbo.IHLISTPRO D With(Nolock) 
						INNER JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = s.Code 
						LEFT OUTER JOIN  Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						LEFT OUTER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id
						WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1 AND phy.Quantity > 0 AND D.CONSUMPTION = 0 
						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
				END

		END ELSE BEGIN

				  IF @ParametroMostrarInsumosCantidadCero = 1 BEGIN 

						--carga los medicamento como insumos
						SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
						from 
						dbo.IHLISTPRO D With(Nolock)
						INNER JOIN Inventory.ATC atc  With(Nolock) ON RTRIM(D.CODPRODUC) = atc.Code 
						LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						INNER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id  AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
						WHERE (D.TIPPRODUC IN ('1', '3') OR D.ESPDILPRO = 1) AND D.PROESTADO = 1 AND D.CONSUMPTION = 0 
 						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
						union
						--carga los insumos
						select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
						From dbo.IHLISTPRO D With(Nolock) 
						INNER JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = s.Code 
						LEFT OUTER JOIN  Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						INNER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
						WHERE D.TIPPRODUC = '2'  AND D.PROESTADO=1 AND D.CONSUMPTION = 0 
						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  

				END ELSE BEGIN

						--carga los medicamento como insumos
						SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
						from 
						dbo.IHLISTPRO D With(Nolock)
						INNER JOIN Inventory.ATC atc  With(Nolock) ON RTRIM(D.CODPRODUC) = atc.Code 
						LEFT OUTER JOIN Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.ATCId = atc.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						INNER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id  AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
						WHERE (D.TIPPRODUC IN ('1', '3') OR D.ESPDILPRO = 1) AND D.PROESTADO = 1 AND D.CONSUMPTION = 0
 						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  
						union
						--carga los insumos
						select RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Descripcion,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,SUM(isnull(phy.Quantity,0))AS Disponibles,D.TIPPRODUC, '' As 'DESADMINI', '' AS 'Cantidad Prescrita'
						From dbo.IHLISTPRO D With(Nolock) 
						INNER JOIN Inventory.InventorySupplie s WITH(NOLOCK) ON RTRIM(D.CODPRODUC) = s.Code 
						LEFT OUTER JOIN  Inventory.InventoryProduct invpro  With(Nolock) on invpro.Status = 1 AND invpro.SupplieId = s.Id
						LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = invpro.Id
						AND phy.WarehouseId IN (SELECT Id from @Almacenes)
						INNER JOIN Inventory.Warehouse E  With(Nolock) ON phy.WarehouseId = E.Id AND E.Code IN (select CODBODEGA from dbo.HCPARBODEGAS where CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional)
						WHERE D.TIPPRODUC = '2' AND D.PROESTADO=1 AND phy.Quantity > 0 AND D.CONSUMPTION = 0 
						GROUP BY RTRIM(D.CODPRODUC) , RTRIM(D.DESPRODUC),D.TIPPRODUC  

				END

		END
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los medicamentos, insumos y dispositivos médicos disponibles para un paciente hospitalizado en la Unidad Quirúrgica, combinando tres fuentes: el inventario físico del almacén indicado, las prescripciones activas del paciente (por número de ingreso y cédula) y las mezclas o preparaciones magistrales (líquidos, concentraciones) asociadas a su ingreso. Soporta dos versiones del ERP (v1 usa inventario INFISICO clásico, v2 usa el nuevo modelo INNFISICO/INNPRODUC), filtrando por almacén, tipo de producto (medicamentos, insumos, diluyentes) y estado activo del producto. Su propósito es alimentar la interfaz de enfermería en quirófano para que el personal pueda seleccionar y registrar los productos a administrar durante la atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosPacienteEnfermeriaUnidadQX';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosPacienteEnfermeriaUnidadQX';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos (medicamentos e insumos) disponibles para la atención de enfermería en unidad quirúrgica de un paciente, calculando existencias por almacén/bodega según la versión del ERP y parámetros de configuración de la unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaUnidadQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de versión de ERP determina la rama lógica (1 = ERP legado, 2 = ERP intermedio, otro valor = ERP nuevo basado en esquema Inventory); Para la rama del ERP nuevo se requieren CentroAtencion y UnidadFuncional para resolver parámetros y bodegas configuradas; Los códigos de paciente e ingreso deben corresponder a prescripciones/mezclas existentes para incluir productos prescritos o de mezcla; El almacén se normaliza con RTRIM y se trata como NULL si queda vacío', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaUnidadQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran productos activos (PROESTADO=1 o ''True''); Los medicamentos corresponden a TIPPRODUC ''1'' o ''3'' y los insumos a TIPPRODUC ''2''; Los productos diluyentes/especiales (ESPDILPRO=1) se tratan como medicamentos en la rama del ERP nuevo; Los productos marcados como CONSUMPTION=0 son los únicos elegibles en la rama del ERP nuevo; Las prescripciones consideradas para mostrar productos del paciente están en estados (''1'',''6'',''7'') para HCPRESCRA y (''1'',''5'') para liquidaciones de mezcla; La cantidad disponible se reporta como 0 cuando no existe registro físico (ISNULL/COALESCE); El procedimiento siempre devuelve un único resultset, nunca modifica datos persistentes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaUnidadQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Si VersionERP=1: devuelve unión de productos del catálogo con stock en INFISICO para el almacén (tipo 2 o 3, estado 1), más medicamentos prescritos en HCPRESCRA con PREESTADO IN (''1'',''6'',''7'') del paciente/ingreso (tipos 1 o 3), más productos asociados a conceptos de mezcla (HCINFCONC/HCINFLIQD ligados a HCINFLIQA con PREESTADO IN (''1'',''5'')); [RETURN_RESULT] RESULTSET: Si VersionERP=2: devuelve catálogo con stock desde INNFISICO/INNALMACE (tipo 2 o 3, o producto con ESPDILPRO=''1'' en almacén indicado y PROESTADO=''True''), unido con medicamentos del catálogo (tipos 1 o 3, PROESTADO=1, ESPDILPRO=''0'') filtrados a productos del almacén o sin inventario físico, y unido con productos de HCINFCONC/HCINFLIQD ligados a HCINFLIQA del paciente/ingreso con PREESTADO IN (''1'',''5''); [INSERT] @Almacenes: En la rama ELSE: si CentroAtencion no es nulo/vacío inserta los Warehouse cuyo CodeCenterAttention = CentroAtencion; siempre inserta además los Warehouse con CodeCenterAttention IS NULL; [RETURN_RESULT] RESULTSET: En la rama ELSE con DEFINIRBODEGAS=0 y MOSTRARINSUCERO=1: devuelve medicamentos (TIPPRODUC IN (''1'',''3'') o ESPDILPRO=1, PROESTADO=1, CONSUMPTION=0) e insumos (TIPPRODUC=''2'', PROESTADO=1, CONSUMPTION=0) con cantidad sumada (incluso cero) sobre PhysicalInventory de los almacenes en @Almacenes; [RETURN_RESULT] RESULTSET: En la rama ELSE con DEFINIRBODEGAS=0 y MOSTRARINSUCERO=0: igual al caso anterior pero los insumos (TIPPRODUC=''2'') solo se incluyen si phy.Quantity>0; [RETURN_RESULT] RESULTSET: En la rama ELSE con DEFINIRBODEGAS=1 y MOSTRARINSUCERO=1: devuelve medicamentos e insumos restringiendo Warehouse.Code a los CODBODEGA configurados en HCPARBODEGAS para el centro de atención y unidad funcional, mostrando insumos aún con cantidad cero; [RETURN_RESULT] RESULTSET: En la rama ELSE con DEFINIRBODEGAS=1 y MOSTRARINSUCERO=0: igual al caso anterior pero los insumos (TIPPRODUC=''2'') solo se devuelven cuando phy.Quantity>0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaUnidadQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @VersionERP = 1 → Consulta inventario y prescripciones contra el ERP legado (INFISICO, IHRINDDGH, HCPRESCRA, HCINFCONC, HCINFLIQA, HCINFLIQD) else Evalúa siguientes ramas; si @VersionERP = 2 → Consulta inventario contra el ERP intermedio (INNPRODUC, INNFISICO, INNALMACE) e incluye productos de mezclas del paciente else Pasa a rama del ERP nuevo basado en esquema Inventory; si @CentroAtencion no nulo y no vacío (rama ELSE) → Carga en @Almacenes los Warehouse cuyo CodeCenterAttention coincide con el centro de atención else Solo se cargan los Warehouse con CodeCenterAttention NULL; si Parámetro DEFINIRBODEGAS de HCUNITHIS = 0 → Usa todos los almacenes resueltos en @Almacenes para calcular existencias else Restringe Warehouse a los códigos definidos en HCPARBODEGAS para el centro/unidad; si Parámetro MOSTRARINSUCERO de HCUNITHIS = 1 → Incluye insumos aunque su cantidad disponible sea cero else Excluye insumos (TIPPRODUC=''2'') con phy.Quantity no positiva', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaUnidadQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPacienteEnfermeriaUnidadQX';
-- GO
