-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-03-18
-- Description:	Obtener la secuencia numerica
-- =============================================
CREATE PROCEDURE [Common].[SP_CalculateFormule]
	@Formulate VARCHAR(MAX),
	@XmlVariables XML,
	---------------------------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	@Result DECIMAL(18, 2) OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @TheSQL NVARCHAR(MAX)

	DECLARE @Variables AS TABLE
	(
		Pattern VARCHAR(500),
		Value DECIMAL(18,2)
	)

	BEGIN TRY

		INSERT INTO @Variables
			SELECT	t.x.value('Pattern[1]','varchar(500)'),
					t.x.value('Value[1]','decimal(18,2)')
			FROM @XmlVariables.nodes('/Data') t(x)

		/************************************************* VARIABLES *************************************************/

		SELECT @Formulate = REPLACE(@Formulate, Pattern, Value)
		FROM @Variables

		SET @TheSQL = CONCAT('SELECT @Result = ', @Formulate)

		EXEC sp_executesql @TheSQL, N'@Result DECIMAL(18,2) OUTPUT', @Result = @Result OUTPUT

		IF @@ROWCOUNT > 1
		BEGIN
			SELECT @CodeResult = 999, 
					@MessageResult = CONCAT('La Formula: ''',@Formulate, ''' no es valida')
			RETURN
		END

		/************************************************* RESULTADO *************************************************/

		SELECT @CodeResult = 0,
			   @MessageResult = ''
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = CONCAT('SP_CalculateFormule: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento genérico que evalúa una fórmula matemática en tiempo de ejecución. Recibe una expresión aritmética con variables simbólicas (por ejemplo, tarifas, cantidades o indicadores clínicos) y un XML con los pares nombre-valor de cada variable; reemplaza los patrones en la fórmula por sus valores numéricos y ejecuta el cálculo dinámicamente mediante sp_executesql. Devuelve el resultado numérico con dos decimales junto con un código y mensaje de éxito o error, siendo útil para calcular valores parametrizados como tarifas de servicios, fórmulas de liquidación o indicadores configurables sin necesidad de lógica fija en el código.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_CalculateFormule';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_CalculateFormule';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Evalúa dinámicamente una fórmula matemática parametrizada, sustituyendo patrones por valores provistos en XML, y devuelve el resultado numérico junto con código y mensaje de estado.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateFormule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La fórmula debe ser una expresión evaluable por SQL tras la sustitución de patrones; El XML de variables debe tener la estructura /Data con nodos Pattern y Value; Cada patrón referenciado en la fórmula debe tener su correspondiente valor en el XML para que la expresión final sea válida', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateFormule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado numérico se entrega siempre con precisión DECIMAL(18,2); Ante cualquier error o fórmula inválida el código de retorno es 999; Cuando la ejecución es exitosa el código de retorno es 0 y el mensaje queda vacío; Los patrones presentes en la fórmula se sustituyen por su valor numérico antes de evaluarla', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateFormule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Fórmula parametrizada; Cálculo dinámico', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateFormule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @Result (parámetro de salida): Tras REPLACE de patrones por valores y EXEC sp_executesql sobre ''SELECT @Result = <Formulate>'', se retorna el valor calculado de la fórmula; [RETURN_RESULT] @CodeResult/@MessageResult (parámetros de salida): Si @@ROWCOUNT > 1 tras la ejecución dinámica → CodeResult=999 y mensaje ''La Formula: ... no es valida''; [RETURN_RESULT] @CodeResult/@MessageResult (parámetros de salida): En CATCH → CodeResult=999 y mensaje ''SP_CalculateFormule: <ERROR_MESSAGE> - Linea: <ERROR_LINE>''; [RETURN_RESULT] @CodeResult/@MessageResult (parámetros de salida): Si la fórmula se ejecuta sin error y no produce más de una fila → CodeResult=0 y mensaje vacío', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateFormule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @@ROWCOUNT > 1 tras ejecutar la fórmula dinámica → Marca código 999 con mensaje indicando que la fórmula no es válida y termina la ejecución else Asigna código 0 y mensaje vacío indicando éxito; si Se produce una excepción al evaluar la fórmula (BEGIN CATCH) → Devuelve código 999 con el mensaje de error y la línea donde falló', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateFormule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateFormule';
-- GO
