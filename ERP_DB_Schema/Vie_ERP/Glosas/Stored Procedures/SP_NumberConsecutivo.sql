-- =============================================  
-- Author:    
-- ALTER date:  
-- Description: Script de actualizacion de consecutivo Dinamia - a genesis  
-- =============================================  
CREATE PROCEDURE [Glosas].[SP_NumberConsecutivo]  
  
AS  
BEGIN  
  
   
 Create TABLE #tablaActualizacion(  
  Id integer identity(1,1),  
  RadicatedConsecutive  varchar(50),  
  InvoiceNumber  varchar(50),  
  CCRNUMRAD varchar(50),  
  CMRNUMFAC varchar(50)  
 )  
   
  
 INSERT INTO #tablaActualizacion  
 select c.RadicatedConsecutive, d.InvoiceNumber,dd.CCRNUMRAD,dd.CMRNUMFAC     from [Glosas].[RadicateInvoiceC] c inner join  
 [Glosas].[RadicateInvoiceD] d on c.id = d.RadicateInvoiceCId  inner join   
 [dbo].[CRMRACTS] dd on d.InvoiceNumber = dd.CMRNUMFAC   
  
 declare @count as integer = (select count(*) from #tablaActualizacion)  
  
 declare @contador as integer = 0  
  
 WHILE @contador <= @count BEGIN  
  
  set @contador = @contador + 1  
  
  declare @numeroRadicateDinamica as varchar (100), @RadicatedConsecutive as varchar(100)  
  select @numeroRadicateDinamica = CCRNUMRAD, @RadicatedConsecutive = RadicatedConsecutive from #tablaActualizacion where Id = @contador  
  
  update [Glosas].[RadicateInvoiceC] set   RadicatedConsecutive = @numeroRadicateDinamica where RadicatedConsecutive = @RadicatedConsecutive  
  
  
 END    
  
  
END