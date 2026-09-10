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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de migración que sincroniza los consecutivos de radicación de glosas entre el sistema de origen Dinamia y el sistema destino Genesis. Cruza las facturas radicadas (cabecera y detalle de radicación de glosas) con el número de radicado del sistema Dinamia (CCRNUMRAD de CRMRACTS) y actualiza el consecutivo de radicación en la tabla maestra de radicación de glosas (RadicateInvoiceC). Garantiza que el número de radicado de glosa en Genesis coincida con el consecutivo asignado por Dinamia, evitando inconsistencias entre los dos sistemas durante procesos de migración o integración de glosas y facturas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_NumberConsecutivo';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_NumberConsecutivo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Sincroniza el consecutivo de radicación de facturas en el módulo de glosas, reemplazándolo por el consecutivo equivalente proveniente del sistema externo (Dinamia) para alinearlo con Génesis.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_NumberConsecutivo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir correspondencia entre facturas radicadas en Glosas y registros en el sistema externo mediante el número de factura (InvoiceNumber = CMRNUMFAC); Las tablas RadicateInvoiceC, RadicateInvoiceD y CRMRACTS deben estar accesibles y pobladas', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_NumberConsecutivo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se actualizan registros de RadicateInvoiceC cuyas facturas asociadas (vía RadicateInvoiceD) tengan correspondencia en CRMRACTS por número de factura; El nuevo consecutivo proviene exclusivamente del campo CCRNUMRAD del sistema externo; La actualización se hace emparejando por el valor previo de RadicatedConsecutive, por lo que todos los registros con ese mismo consecutivo serán homologados al nuevo valor', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_NumberConsecutivo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'radicación de facturas; consecutivo de radicación; glosas; homologación entre sistemas (Dinamia y Génesis)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_NumberConsecutivo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Glosas.RadicateInvoiceC: Para cada factura cuyo InvoiceNumber coincide con CMRNUMFAC en CRMRACTS, se reemplaza el RadicatedConsecutive actual por el valor CCRNUMRAD del sistema externo (Dinamia)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_NumberConsecutivo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.RadicateInvoiceC; Glosas.RadicateInvoiceD; dbo.CRMRACTS', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_NumberConsecutivo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_NumberConsecutivo';
-- GO
