
CREATE FUNCTION [Admissions].[AdmissionModality] (@Id as int)
RETURNS varchar (300)
AS
BEGIN

declare @AdmissionModality varchar(300)
	
 SET @AdmissionModality  = (SELECT Name FROM Admissions.AdmissionModalities WHERE Id = @Id)
/*
Función que me va a listar la modalidad de atención.
*/

RETURN @AdmissionModality

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que, dado un código numérico de modalidad de admisión, devuelve el nombre descriptivo de esa modalidad (por ejemplo: urgencias, hospitalización, consulta externa). Consulta el catálogo de modalidades de admisión para traducir el identificador interno a un texto legible. Se usa para mostrar en pantalla o en reportes el tipo de atención o vía de ingreso del paciente, evitando trabajar con códigos crípticos.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'FUNCTION', @level1name = N'AdmissionModality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'FUNCTION', @level1name = N'AdmissionModality';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resuelve el nombre descriptivo de la modalidad de admisión a partir de su identificador, consultando el catálogo correspondiente.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'FUNCTION', @level1name=N'AdmissionModality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El Id recibido debe corresponder a un registro existente en Admissions.AdmissionModalities para obtener un nombre; en caso contrario el resultado será NULL.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'FUNCTION', @level1name=N'AdmissionModality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Devuelve exactamente el Name correspondiente al Id solicitado en Admissions.AdmissionModalities; si el Id no existe, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'FUNCTION', @level1name=N'AdmissionModality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'modalidad de admisión', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'FUNCTION', @level1name=N'AdmissionModality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Admissions.AdmissionModalities: SELECT Name FROM Admissions.AdmissionModalities WHERE Id = @Id → retorna el nombre de la modalidad de admisión asociada al Id.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'FUNCTION', @level1name=N'AdmissionModality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Admissions.AdmissionModalities', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'FUNCTION', @level1name=N'AdmissionModality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'FUNCTION', @level1name=N'AdmissionModality';
GO
