-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE FUNCTION [dbo].[Patient_Ubication] (@IPCODPACI as varchar(25))
RETURNS varchar (300)
AS
BEGIN

declare @Ubication varchar(300)

   SET @Ubication = (SELECT RTRIM(B.UBINOMBRE) 
				from Admissions.PatientAddress A 
				inner join INUBICACI B  WITH(NOLOCK) on A.IdUbication = B.ID 
				where ipcodpaci = @IPCODPACI and IsMain = 1)

    -- Return the result of the function
    RETURN @Ubication
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que, dado el código o cédula de un paciente, devuelve el nombre de su ubicación geográfica principal (municipio, barrio o localidad). Consulta la dirección marcada como principal en el registro de domicilios del paciente y la cruza con la tabla maestra de ubicaciones para obtener el nombre legible. Se usa para mostrar o reportar dónde reside el paciente sin necesidad de hacer joins manuales desde otros módulos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Patient_Ubication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Patient_Ubication';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtener el nombre de la ubicación geográfica asociada a la dirección principal de un paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Ubication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en Admissions.PatientAddress con al menos un registro marcado como principal para obtener un valor.; La dirección principal debe tener un IdUbication válido referenciado en INUBICACI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Ubication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelve la ubicación marcada como principal (IsMain = 1) del paciente.; El nombre de la ubicación se entrega sin espacios finales (RTRIM).; Si el paciente no tiene dirección principal o no existe, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Ubication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; dirección del paciente; ubicación geográfica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Ubication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Admissions.PatientAddress: Cuando existe una dirección con IsMain=1 para el paciente, retorna el nombre de la ubicación (UBINOMBRE) recortado; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Ubication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Admissions.PatientAddress; dbo.INUBICACI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Ubication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Ubication';
GO
