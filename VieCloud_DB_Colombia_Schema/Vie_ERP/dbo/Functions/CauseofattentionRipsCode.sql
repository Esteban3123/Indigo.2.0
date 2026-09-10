CREATE FUNCTION [dbo].[CauseofattentionRipsCode] (@Code as int)
RETURNS varchar (3)
AS
BEGIN

declare @CauseofattentionRipsCode varchar(3)
	
 SET @CauseofattentionRipsCode  = (SELECT RIPSCode FROM Causesofattention WHERE Code = @Code)
/*
Función que me va a retornar el código RIPS.
*/

RETURN @CauseofattentionRipsCode

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que, dado un código interno de causa de atención (motivo de consulta o ingreso como urgencia, hospitalización o consulta externa), retorna el código RIPS de 3 caracteres correspondiente, consultando el catálogo de causas de atención. Se utiliza para obtener el código requerido en la generación de reportes RIPS al sistema de información en salud (SISPRO/Ministerio de Salud). Facilita la conversión entre el código interno del sistema y el código estándar exigido en los archivos de reporte obligatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'CauseofattentionRipsCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'CauseofattentionRipsCode';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el código RIPS asociado a una causa de atención registrada en el catálogo, para reportes regulatorios.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CauseofattentionRipsCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Causesofattention cuyo Code coincida con el parámetro; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CauseofattentionRipsCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor retornado siempre se trunca/limita a 3 caracteres por el tipo de retorno varchar(3).; Solo se consulta una causa de atención por su Code (clave única esperada).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CauseofattentionRipsCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Causa de atención; Código RIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CauseofattentionRipsCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Causesofattention: Retorna RIPSCode (varchar(3)) de Causesofattention donde Code = parámetro recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CauseofattentionRipsCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.Causesofattention', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CauseofattentionRipsCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CauseofattentionRipsCode';
GO
