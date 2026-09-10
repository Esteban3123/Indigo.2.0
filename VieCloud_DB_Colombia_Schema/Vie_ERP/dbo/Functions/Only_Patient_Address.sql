

CREATE FUNCTION [dbo].[Only_Patient_Address] (@IPCODPACI as varchar(25))
RETURNS varchar (300)
AS
BEGIN

declare @Address varchar(300)
	
 SET @Address  = (select Address
					from Admissions.PatientAddress A WITH(NOLOCK) 
					where A.IPCODPACI = @IPCODPACI and IsMain = 1
				  )

/*
Función que me va a listar la direccion del paciente.
*/

RETURN @Address

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que retorna la dirección principal registrada para un paciente, dado su código o cédula. Consulta la tabla de direcciones del paciente (Admissions.PatientAddress) y devuelve únicamente la dirección marcada como principal (IsMain = 1). Se usa para obtener el domicilio o dirección de contacto del paciente en procesos de admisión, facturación o notificaciones. Recibe como parámetro la cédula o identificación del paciente (IPCODPACI) y devuelve la dirección como texto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Only_Patient_Address';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Only_Patient_Address';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtener la dirección principal registrada de un paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Only_Patient_Address';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Admissions.PatientAddress asociado al paciente con IsMain = 1 para obtener un valor no nulo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Only_Patient_Address';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna la dirección marcada como principal (IsMain = 1).; Si el paciente no tiene una dirección principal registrada, retorna NULL.; Se asume que existe a lo sumo una dirección principal por paciente; si hubiera más de una, la asignación escalar fallaría.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Only_Patient_Address';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; dirección del paciente; dirección principal', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Only_Patient_Address';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Admissions.PatientAddress: Cuando existe una dirección del paciente con IsMain = 1, retorna el valor de Address; si no, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Only_Patient_Address';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Admissions.PatientAddress', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Only_Patient_Address';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Only_Patient_Address';
GO
