

-- =============================================
-- Author:		Juan Montealegre
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- ==========================================
CREATE FUNCTION [dbo].[EstadoEscalaDownt]
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
				@Resu = ESCADOWNT 
	FROM 
		ADINGRESO
	WHERE 
		NUMINGRES = @NumeroIngreso AND IPCODPACI =@CodigoPaciente 

	-- Return the result of the function
	RETURN @Resu

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el valor de la Escala de Downton registrada en el ingreso de un paciente. Recibe el número de ingreso y el código o cédula del paciente, y devuelve el resultado numérico de dicha escala, que evalúa el riesgo de caídas del paciente hospitalizado. Busca directamente en la tabla de ingresos (ADINGRESO) usando ambos parámetros como identificadores únicos del episodio de atención. Se utiliza en procesos clínicos para conocer el estado de valoración de riesgo de caída asociado a un ingreso específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'EstadoEscalaDownt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'EstadoEscalaDownt';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el estado de la escala de Downton (riesgo de caídas) registrado para un ingreso específico de un paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoEscalaDownt';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en ADINGRESO con el número de ingreso y código de paciente indicados para obtener un valor; en caso contrario el resultado queda no inicializado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoEscalaDownt';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Identifica unívocamente el ingreso del paciente combinando número de ingreso y código de paciente.; El valor retornado proviene exclusivamente del campo ESCADOWNT del registro de ingreso correspondiente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoEscalaDownt';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso; Paciente; Escala de Downton', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoEscalaDownt';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Devuelve el valor de ESCADOWNT del ingreso filtrado por NUMINGRES y IPCODPACI, convertido a int (al estar declarado como char de 1 carácter, se trunca a un solo carácter).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoEscalaDownt';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoEscalaDownt';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoEscalaDownt';
GO
