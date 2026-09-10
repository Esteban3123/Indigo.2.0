-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-04
-- Description:	Calcular el valor a ajustar
-- =============================================
CREATE FUNCTION [Common].[CalculateAdjustedValue]
(
	@Difference DECIMAL(20,4),	
	@ValueIfDifferenceNegative DECIMAL(20,4),
	@ValueIfDifferencePositive DECIMAL(20,4)
)
RETURNS DECIMAL(20,4)
AS
BEGIN
	DECLARE @AdjustedValue AS DECIMAL(20,4)

	SET @AdjustedValue = IIF
	(
		@Difference = 0, 0,
		IIF
		(
			@Difference > 0,
			IIF -- Positiva
			(
				@ValueIfDifferencePositive = 0,
				0,
				IIF
				(
					@Difference > @ValueIfDifferencePositive, 
					@ValueIfDifferencePositive, 
					@Difference
				)
			),
			IIF -- Negativa
			(
				@ValueIfDifferencePositive < 0, 
				IIF
				(
					@ValueIfDifferencePositive = 0,
					0,
					IIF
					(
						@Difference > @ValueIfDifferencePositive, 							
						@Difference,
						@ValueIfDifferencePositive
					)
				), 
				IIF
				(
					@ValueIfDifferenceNegative = 0,
					0,
					IIF
					(	
						@ValueIfDifferenceNegative < 0, 
						IIF
						(
							@Difference > @ValueIfDifferenceNegative, 							
							@Difference,
							@ValueIfDifferenceNegative
						) * -1, 
						@Difference
					)
				)
			)
		)
	)

	RETURN @AdjustedValue
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que calcula el valor de ajuste financiero a aplicar según la diferencia entre lo esperado y lo liquidado. Recibe tres parámetros: la diferencia (positiva o negativa) entre dos valores monetarios, el tope máximo permitido cuando la diferencia es positiva, y el tope máximo cuando la diferencia es negativa. Aplica reglas de negocio para determinar cuánto se puede ajustar sin exceder los límites definidos en cada sentido, retornando cero si no hay diferencia o si los topes son cero. Se usa típicamente en procesos de glosas, conciliaciones o ajustes de facturación donde se necesita calcular el monto real a corregir respetando límites por exceso o por defecto.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'CalculateAdjustedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'CalculateAdjustedValue';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula un valor de ajuste acotado a partir de una diferencia, escogiendo el tope positivo o negativo aplicable y limitando el resultado al menor valor absoluto entre la diferencia y el límite correspondiente.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CalculateAdjustedValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La diferencia y los valores de tope deben venir como DECIMAL(20,4); no se valida nulidad explícita.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CalculateAdjustedValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Si la diferencia es 0, el ajuste siempre es 0.; Si el tope aplicable es 0, el ajuste es 0.; El ajuste nunca excede en magnitud al tope correspondiente cuando el tope tiene el mismo signo esperado.; Cuando se aplica el tope negativo con signo negativo, el resultado se devuelve como valor positivo (cambio de signo).', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CalculateAdjustedValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'ajuste de valor; diferencia', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CalculateAdjustedValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando la diferencia es 0 retorna 0.; [RETURN_RESULT] N/A: Cuando la diferencia es positiva y el tope positivo es 0 retorna 0; en otro caso retorna el menor entre la diferencia y el tope positivo.; [RETURN_RESULT] N/A: Cuando la diferencia es negativa y el tope positivo es negativo, retorna el mayor (más cercano a cero) entre la diferencia y dicho tope.; [RETURN_RESULT] N/A: Cuando la diferencia es negativa y el tope negativo es 0 retorna 0.; [RETURN_RESULT] N/A: Cuando la diferencia es negativa y el tope negativo es menor que 0, retorna el mayor entre la diferencia y el tope negativo, multiplicado por -1 (lo invierte de signo).; [RETURN_RESULT] N/A: Cuando la diferencia es negativa y el tope negativo es positivo, retorna la diferencia tal cual.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CalculateAdjustedValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Diferencia = 0 → Retorna 0 else Evalúa el signo de la diferencia; si Diferencia > 0 → Aplica tope positivo: 0 si tope=0, sino min(diferencia, tope positivo) else Rama negativa según valor del tope positivo y negativo; si Diferencia < 0 y tope positivo < 0 → Devuelve el mayor entre diferencia y tope positivo (acotando a la magnitud del tope) else Usa el tope negativo; si Diferencia < 0 y tope negativo < 0 → Acota la diferencia al tope negativo y la invierte de signo (resultado positivo) else Si tope negativo = 0 retorna 0; si tope negativo > 0 retorna la diferencia sin alterar', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CalculateAdjustedValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CalculateAdjustedValue';
GO
