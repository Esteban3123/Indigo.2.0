

-- =============================================
-- Author:		Juan Montealegre
-- Create date: 16-12-2013
-- Description:	Funcion que retorna el puntaje de la escala
-- =============================================
CREATE FUNCTION [dbo].[PuntajeEscalaRass] 
(
	-- Add the parameters for the function here
	@NumeroIngreso char(10),
	@CodigoPaciente varchar(25)
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Resu int

	-- Add the T-SQL statements to compute the return value here
	SELECT TOP 1 @Resu =  PUNTAJESC  FROM HCESCRASS with(nolock) WHERE NUMINGRES = @NumeroIngreso AND IPCODPACI =@CodigoPaciente ORDER BY CODCONCEC DESC

	-- Return the result of the function
	RETURN @Resu

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que retorna el último puntaje registrado en la Escala RASS (Richmond Agitation-Sedation Scale) para un paciente durante un ingreso hospitalario específico. Recibe como parámetros el número de ingreso y la cédula o código del paciente, y consulta la tabla de evaluaciones clínicas HCESCRASS para obtener el puntaje más reciente según el consecutivo de concepto (CODCONCEC) en orden descendente. Se utiliza en contextos de monitoreo de sedación y agitación en pacientes hospitalizados, especialmente en unidades de cuidado intensivo o áreas críticas. Permite conocer el nivel de conciencia o sedación del paciente en su último registro valorado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'PuntajeEscalaRass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'PuntajeEscalaRass';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el puntaje más reciente de la escala RASS registrada para un ingreso y paciente determinados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaRass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en la tabla de escala RASS asociado al ingreso y paciente para obtener un puntaje; en caso contrario el resultado será NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaRass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Devuelve únicamente el puntaje del registro más reciente (mayor consecutivo) de la escala RASS para la combinación ingreso-paciente.; Si no existe registro para la combinación, retorna NULL.; Lectura sin bloqueo (NOLOCK), por lo que puede leer datos no confirmados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaRass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión hospitalaria; Escala RASS (Richmond Agitation-Sedation Scale); Puntaje de escala clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaRass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCESCRASS: Cuando existen registros en HCESCRASS con NUMINGRES y IPCODPACI dados, retorna el PUNTAJESC del registro con mayor CODCONCEC (último consecutivo).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaRass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCESCRASS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaRass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaRass';
GO
