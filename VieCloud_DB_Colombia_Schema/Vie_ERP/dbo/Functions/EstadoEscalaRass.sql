

-- =============================================
-- Author:		Juan Montealegre
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- ==========================================
CREATE FUNCTION [dbo].[EstadoEscalaRass]
(
	-- Add the parameters for the function here
	@NumeroIngreso char(10),
	@CodigoPaciente varchar(25)
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Resu char

	-- Add the T-SQL statements to compute the return value here
	SELECT 
				@Resu = ESCARASS
	FROM 
		ADINGRESO
	WHERE 
		NUMINGRES = @NumeroIngreso AND IPCODPACI =@CodigoPaciente 

	-- Return the result of the function
	RETURN @Resu

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que consulta y devuelve el estado de la Escala RASS (Richmond Agitation-Sedation Scale) registrado para un paciente durante un episodio de ingreso hospitalario específico. Recibe como parámetros el número de ingreso y el código o cédula del paciente, y busca en la tabla de ingresos (ADINGRESO) el valor correspondiente al campo de escala RASS. Se utiliza para conocer el nivel de sedación o agitación evaluado al paciente en su admisión, dato relevante en contextos de cuidados intensivos, urgencias u hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'EstadoEscalaRass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'EstadoEscalaRass';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el estado de la escala RASS (sedación-agitación) registrado para un ingreso de un paciente específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoEscalaRass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro de ingreso que coincida con el número de ingreso y el código de paciente proporcionados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoEscalaRass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor retornado proviene siempre del campo ESCARASS del ingreso identificado por la combinación número de ingreso + código de paciente; Si no existe ingreso coincidente, el resultado queda sin asignar (NULL convertido al tipo de retorno)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoEscalaRass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso hospitalario; Paciente; Escala RASS (Richmond Agitation-Sedation Scale)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoEscalaRass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ADINGRESO: Cuando NUMINGRES y IPCODPACI coinciden con los parámetros, retorna el valor de ESCARASS asociado al ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoEscalaRass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoEscalaRass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoEscalaRass';
GO
