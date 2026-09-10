

CREATE FUNCTION [dbo].[Medicos] (@Paciente as varchar(25), @Ingreso as varchar(10))
RETURNS varchar (20)
AS
BEGIN

declare @Medico varchar(20)
declare @prueba varchar(25)
SELECT @prueba=IPCODPACI FROM INPACIENT
set @Medico = 'A01'

RETURN @Medico

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que, dado un paciente (por cédula o código) y un número de ingreso, tiene como propósito consultar y retornar el código del médico asociado a ese paciente o ingreso. Actualmente la lógica está incompleta: consulta la tabla de pacientes (INPACIENT) pero retorna siempre el valor fijo ''A01'' sin procesar los parámetros recibidos. Toca las entidades de negocio de pacientes e ingresos, y está diseñada para ser usada como función auxiliar que resuelva qué médico (profesional de la salud) está vinculado a un determinado paciente o episodio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Medicos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Medicos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Función que retorna un código de médico fijo (''A01''), aparentando ser un stub o implementación pendiente sin lógica real de negocio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Medicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna el valor literal ''A01'' independientemente de los parámetros de entrada; La consulta a INPACIENT no influye en el resultado retornado (código muerto / placeholder)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Medicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Medicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Retorna siempre la cadena ''A01'' sin condicionar al paciente ni al ingreso recibidos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Medicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Medicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Medicos';
GO
