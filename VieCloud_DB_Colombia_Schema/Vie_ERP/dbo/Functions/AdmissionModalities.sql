
CREATE FUNCTION [dbo].[AdmissionModalities](@Id as int)
RETURNS varchar(100)
AS
BEGIN
    
	declare @AdmissionModalities varchar(100)

	SET @AdmissionModalities = (SELECT Name FROM Admissions.AdmissionModalities WHERE Id = @Id)
	/*
	Función que me va a retornar el nombre de la modalidad de atención.
	*/

    RETURN @AdmissionModalities
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que recibe un identificador numérico de modalidad de admisión y retorna su nombre descriptivo en texto. Consulta el catálogo de modalidades de admisión (urgencias, hospitalización, consulta externa, entre otras) para convertir un código interno en una etiqueta legible. Se utiliza para mostrar el nombre de la modalidad de atención en reportes, consultas e interfaces de usuario sin necesidad de hacer un JOIN manual con la tabla de modalidades.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'AdmissionModalities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'AdmissionModalities';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el nombre de la modalidad de atención correspondiente a un identificador dado del catálogo de modalidades de admisión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AdmissionModalities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en el catálogo de modalidades de admisión cuyo identificador coincida con el valor recibido; en caso contrario el resultado será NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AdmissionModalities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo lee datos del catálogo, nunca modifica información.; El valor retornado siempre proviene de la columna Name del catálogo de modalidades de admisión, truncado a 100 caracteres.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AdmissionModalities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Modalidad de atención; Admisión', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AdmissionModalities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Cuando se encuentra una modalidad con Id igual al parámetro, retorna su Name; si no existe, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AdmissionModalities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Admissions.AdmissionModalities', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AdmissionModalities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AdmissionModalities';
GO
