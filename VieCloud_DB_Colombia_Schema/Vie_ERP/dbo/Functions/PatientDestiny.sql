

CREATE FUNCTION [dbo].[PatientDestiny] (@CodigoPaciente as varchar(25), @Ingreso as varchar(10), @FechaHistoria as Datetime )
RETURNS varchar (2)
AS
BEGIN

declare @PatientDestiny varchar(2)
	
 SET @PatientDestiny  = (select top 1 INDICAPAC from HCHISPACA where IPCODPACI = @CodigoPaciente and NUMINGRES = @Ingreso and FECHISPAC < @FechaHistoria and INDICAPAC <> 13 order by NUMEFOLIO desc)
/*
Función que me va a listar la causa de atención.
*/

RETURN @PatientDestiny

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que determina el destino o causa de atención de un paciente en un ingreso específico. Consulta la historia clínica (HCHISPACA) para obtener el indicador de destino del paciente (INDICAPAC) más reciente registrado antes de una fecha dada, excluyendo el indicador 13. Se utiliza para conocer, en un momento determinado de la atención, cuál era el último destino o causa de atención del paciente durante un ingreso u hospitalización, identificado por su código de paciente (cédula) y número de ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'PatientDestiny';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'PatientDestiny';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la causa/destino de atención más reciente de un paciente para un ingreso, anterior a una fecha dada, excluyendo el indicador 13.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PatientDestiny';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en HCHISPACA para el paciente e ingreso indicados con fecha anterior a la fecha de historia y con indicador distinto de 13; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PatientDestiny';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca retorna registros con indicador igual a 13.; Solo considera historias con fecha estrictamente anterior a la fecha de historia recibida.; Selecciona el folio más alto (último cronológicamente por numeración) del paciente e ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PatientDestiny';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso; Historia clínica; Causa de atención; Folio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PatientDestiny';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna el INDICAPAC del último folio (mayor NUMEFOLIO) cuya FECHISPAC sea menor a la fecha de historia y cuyo INDICAPAC sea distinto de 13.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PatientDestiny';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PatientDestiny';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PatientDestiny';
GO
