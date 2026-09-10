

-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 22/03/2016
-- Description:	Procedimiento que se encarga de obtener los servicios no quirurgicos
-- =============================================
CREATE PROCEDURE [MedicalFees].[SP_ViewListNoSurgical] 
	@InvoiceId as int
AS
BEGIN

	Begin try
	
		
		select 0 as CodeMessage, 'Se anuló correctamente' as Message
		
	end try
	begin catch
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento diseñado para obtener el listado de servicios médicos no quirúrgicos asociados a una factura específica, identificada por su número de factura (InvoiceId). Toca la entidad de negocio de honorarios médicos (MedicalFees), permitiendo consultar los cargos o servicios que no corresponden a procedimientos quirúrgicos dentro de una cuenta o factura. Actualmente el cuerpo del procedimiento devuelve únicamente un mensaje de confirmación sin lógica de consulta implementada, lo que sugiere que está pendiente de desarrollo o fue simplificado.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'PROCEDURE', @level1name = N'SP_ViewListNoSurgical';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'PROCEDURE', @level1name = N'SP_ViewListNoSurgical';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve un mensaje fijo de éxito o, en caso de error, captura y retorna el mensaje de error; actualmente no implementa la consulta de servicios no quirúrgicos.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ViewListNoSurgical';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna exactamente un resultset con las columnas CodeMessage y Message.; Nunca modifica datos: no ejecuta INSERT/UPDATE/DELETE.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ViewListNoSurgical';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'servicios no quirúrgicos', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ViewListNoSurgical';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): En ejecución normal retorna CodeMessage=0 y Message=''Se anuló correctamente''.; [RETURN_RESULT] (resultset): En el bloque CATCH retorna CodeMessage=999 y Message=ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ViewListNoSurgical';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Ocurre una excepción dentro del TRY → Se ejecuta el CATCH retornando código 999 y el mensaje de error else Retorna código 0 con mensaje de éxito', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ViewListNoSurgical';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ViewListNoSurgical';
-- GO
