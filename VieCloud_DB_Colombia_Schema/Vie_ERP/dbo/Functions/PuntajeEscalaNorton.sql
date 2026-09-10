

-- =============================================
-- Author:		Juan Montealegre
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[PuntajeEscalaNorton]
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
	SELECT TOP 1 
				@Resu = ESTFISGEN + ESTMENTAL +
						MOVILIDAD + ACTIVIDAD + 
						INCTINENCI 
	FROM 
		HCESCNTON 
	WHERE 
		NUMINGRES = @NumeroIngreso AND IPCODPACI =@CodigoPaciente ORDER BY CODCONCEC DESC

	-- Return the result of the function
	RETURN @Resu

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el puntaje total de la Escala de Norton para un paciente en un ingreso hospitalario específico. Suma los cinco componentes evaluados: estado físico general, estado mental, movilidad, actividad e incontinencia, tomando la valoración más reciente registrada en la historia clínica. Se usa para determinar el riesgo de úlceras por presión (escaras) del paciente durante su hospitalización. Recibe como parámetros el número de ingreso y la cédula o código del paciente, y retorna un número entero que representa el puntaje total de riesgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'PuntajeEscalaNorton';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'PuntajeEscalaNorton';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el puntaje total de la Escala de Norton (riesgo de úlceras por presión) sumando sus cinco dimensiones a partir del registro más reciente del paciente en un ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaNorton';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro de la escala de Norton para la combinación de ingreso y paciente; de lo contrario el resultado será NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaNorton';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera el registro más reciente por consecutivo (ORDER BY CODCONCEC DESC, TOP 1).; El puntaje es la suma aritmética de las cinco dimensiones de la escala, sin ponderaciones.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaNorton';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Escala de Norton; Estado físico general; Estado mental; Movilidad; Actividad; Incontinencia; Paciente; Ingreso hospitalario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaNorton';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve la suma de ESTFISGEN + ESTMENTAL + MOVILIDAD + ACTIVIDAD + INCTINENCI del registro con mayor CODCONCEC para el ingreso y paciente dados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaNorton';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCESCNTON', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaNorton';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaNorton';
GO
