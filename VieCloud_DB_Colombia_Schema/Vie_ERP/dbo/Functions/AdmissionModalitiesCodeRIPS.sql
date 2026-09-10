CREATE FUNCTION [dbo].[AdmissionModalitiesCodeRIPS] (@Id as int)
RETURNS varchar(100)
AS
BEGIN
    
	declare @CodeRIPS varchar(100)

	SET @CodeRIPS = (SELECT CodeRIPS FROM Admissions.AdmissionModalities WHERE Id = @Id)
	/*
	Función que me va a retornar el código RIPS de la modalidad de atención.
	*/

    RETURN @CodeRIPS
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que recibe el identificador interno de una modalidad de admisión (urgencias, hospitalización, consulta externa, etc.) y devuelve su código RIPS equivalente. Consulta el catálogo de modalidades de admisión para obtener el código requerido en los reportes RIPS que se envían a los entes reguladores del sistema de salud. Se utiliza para traducir el código interno del sistema al código oficial exigido en la facturación y reporte de prestación de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'AdmissionModalitiesCodeRIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'AdmissionModalitiesCodeRIPS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtener el código RIPS correspondiente a una modalidad de atención registrada en el catálogo de modalidades de admisión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AdmissionModalitiesCodeRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en el catálogo de modalidades de admisión cuyo identificador corresponda al solicitado para obtener un código RIPS no nulo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AdmissionModalitiesCodeRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelve el código RIPS asociado a la modalidad de admisión cuyo identificador coincida exactamente con el valor recibido.; Si no existe coincidencia en el catálogo, el resultado es NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AdmissionModalitiesCodeRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Modalidad de atención; Código RIPS; Admisión', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AdmissionModalitiesCodeRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Admissions.AdmissionModalities: Retorna el CodeRIPS del registro de AdmissionModalities cuyo Id coincida con el parámetro; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AdmissionModalitiesCodeRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Admissions.AdmissionModalities', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AdmissionModalitiesCodeRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AdmissionModalitiesCodeRIPS';
GO
