
-- =============================================
-- Author:		Juan Montealegre
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[PuntajeEscalaVas]
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
				@Resu = EDADPACIE + CAIDPREVI +
						TRANQUILI + DIURETICO +
						HIPOTENSO + ANTIPARKI + 
						ANTIDEPRE + ALTERAVIS + 
						ALTERAUDI + ICTUEXTRE + 
						ESTADOMEN + SEGURAYUD + 
						INSEGAYUD + IMPOSIBLE + 
						PATOLOGIA + NUTRICION
	FROM 
		HCESCDOWN 
	WHERE 
		NUMINGRES = @NumeroIngreso AND IPCODPACI =@CodigoPaciente ORDER BY AUTO DESC

	-- Return the result of the function
	RETURN @Resu

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el puntaje total de la Escala de Valoración de Riesgo de Caídas (escala VAS/Downton) para un paciente en un ingreso específico. Suma los valores de cada factor de riesgo evaluado: edad, caídas previas, uso de medicamentos (tranquilizantes, diuréticos, hipotensores, antiparkinsonianos, antidepresivos), alteraciones sensoriales (visión y audición), ictus o secuelas en extremidades, estado mental, tipo de deambulación (segura con ayuda, insegura con ayuda, imposible), patología de base y estado nutricional. Consulta la tabla HCESCDOWN y retorna el resultado del registro más reciente para el número de ingreso y código de paciente (cédula) indicados. Se usa para determinar el nivel de riesgo de caídas del paciente durante su hospitalización o atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'PuntajeEscalaVas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'PuntajeEscalaVas';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el puntaje total de la escala VAS de un paciente sumando los 16 factores de riesgo registrados en la valoración más reciente para un ingreso dado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaVas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en la tabla de escala asociado al ingreso y código de paciente para obtener resultado; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaVas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El puntaje devuelto corresponde siempre al registro más reciente del paciente/ingreso (mayor AUTO).; El puntaje total es la suma aritmética de 16 factores de riesgo predefinidos.; Si no existe registro para el paciente/ingreso, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaVas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Escala VAS (riesgo de caídas); Paciente; Ingreso hospitalario; Edad; Caídas previas; Medicación (tranquilizantes, diuréticos, hipotensores, antiparkinsonianos, antidepresivos); Alteraciones sensoriales (visual, auditiva); Estado mental; Patología; Nutrición', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaVas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCESCDOWN: Selecciona el registro más reciente (ORDER BY AUTO DESC, TOP 1) filtrando por número de ingreso y código de paciente, y retorna la suma de los 16 campos de la escala como entero.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaVas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCESCDOWN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaVas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaVas';
GO
