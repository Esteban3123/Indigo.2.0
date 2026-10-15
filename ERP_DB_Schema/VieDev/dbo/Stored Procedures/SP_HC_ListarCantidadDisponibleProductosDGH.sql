  
  
CREATE PROCEDURE [dbo].[SP_HC_ListarCantidadDisponibleProductosDGH]  
(  
@Almacen CHAR(4),  
@Producto CHAR(20),  
@VersionERP INT  
)  
AS  
BEGIN  
 SET NOCOUNT ON;  
   
IF @VersionERP=1  
  
 select sum(IFICANTID) AS Disponibles   
 from dbo.infisico   
 where iprcodigo in (select iprcodigo from dbo.ihrinddgh where codproduc = @Producto) and ialcodigo = @Almacen  
        
ELSE  
   
 select SUM(IFICANTID) AS Disponibles   
 from dbo.INNFISICO A  
 INNER JOIN dbo.INNPRODUC B ON A.INNPRODUC = B.OID  
 INNER JOIN dbo.INNALMACE C ON A.INNALMACE = C.OID  
 where iprcodigo in (select iprcodigo from dbo.ihrinddgh where codproduc = @Producto) and ialcodigo = @Almacen  
        
   
END