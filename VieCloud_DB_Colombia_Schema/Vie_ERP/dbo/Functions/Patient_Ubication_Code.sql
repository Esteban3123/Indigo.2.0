-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE FUNCTION [dbo].[Patient_Ubication_Code] (@IPCODPACI as varchar(25))
RETURNS varchar (300)
AS
BEGIN

declare @Ubication_code varchar(300)

   SET @Ubication_code = (SELECT RTRIM(B.AUUBICACI) 
				from Admissions.PatientAddress A 
				inner join INUBICACI B  WITH(NOLOCK) on A.IdUbication = B.ID 
				where ipcodpaci = @IPCODPACI and IsMain = 1)

    -- Return the result of the function
    RETURN @Ubication_code
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que, dado el código o cédula de un paciente, devuelve el código de la ubicación o zona geográfica principal donde reside. Consulta la dirección marcada como principal en el registro de domicilios del paciente (PatientAddress) y la cruza con la tabla maestra de ubicaciones (INUBICACI) para obtener el nombre o código estandarizado de esa localidad. Se usa para identificar la zona de residencia principal del paciente en procesos de agendamiento, admisión o reportería territorial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Patient_Ubication_Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Patient_Ubication_Code';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el código de ubicación geográfica asociado a la dirección principal de un paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Ubication_Code';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener una dirección registrada con IsMain = 1 vinculada a un registro válido en INUBICACI; en caso contrario el resultado es NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Ubication_Code';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera la dirección marcada como principal (IsMain = 1) del paciente.; El código de ubicación retornado proviene del catálogo INUBICACI vinculado por IdUbication.; Se aplica RTRIM al código de ubicación para eliminar espacios finales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Ubication_Code';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; dirección del paciente; ubicación geográfica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Ubication_Code';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Admissions.PatientAddress: Cuando existe una PatientAddress con IsMain=1 para el paciente, retorna RTRIM(AUUBICACI) del INUBICACI relacionado; si no existe, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Ubication_Code';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Admissions.PatientAddress; dbo.INUBICACI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Ubication_Code';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Ubication_Code';
GO
