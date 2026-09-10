
-- =============================================
-- Author:		Juan Montealegre
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[PuntajeEscalaApache]
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el puntaje total de la escala de valoración de riesgo de caídas (Downton) para un paciente en un ingreso específico. Suma los factores de riesgo evaluados en la historia clínica: edad del paciente, caídas previas, uso de medicamentos (tranquilizantes, diuréticos, hipotensores, antiparkinsonianos, antidepresivos), alteraciones sensoriales (visión y audición), ictus en extremidades, estado mental, capacidad de deambulación (segura con ayuda, insegura con ayuda, imposible), patología y nutrición. Toma el registro más reciente de la tabla HCESCDOWN para el número de ingreso y la cédula del paciente indicados, y retorna el puntaje numérico total que determina el nivel de riesgo de caída del paciente hospitalizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'PuntajeEscalaApache';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'PuntajeEscalaApache';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el puntaje total de la escala Apache (riesgo de caídas/valoración clínica) sumando los componentes evaluados en el registro más reciente de un ingreso y paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaApache';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en HCESCDOWN para el ingreso y paciente indicados; en caso contrario el resultado será NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaApache';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera el registro más reciente según el campo AUTO (ORDER BY AUTO DESC, TOP 1).; El puntaje es la suma aritmética de 16 factores de evaluación; si cualquiera es NULL, el resultado será NULL.; El filtro siempre exige coincidencia exacta de número de ingreso y código de paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaApache';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Escala Apache; Paciente; Ingreso hospitalario; Valoración clínica (edad, caídas previas, tranquilizantes, diuréticos, hipotensores, antiparkinsonianos, antidepresivos, alteración visual/auditiva, ictus, estado mental, ayuda segura/insegura, patología, nutrición)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaApache';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCESCDOWN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaApache';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaApache';
GO
