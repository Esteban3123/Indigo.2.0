

-- =============================================
-- Author:      Cristhian Salazar
-- Create Date: 2022-12-16
-- Description: Funcion para convertir un valor a un moneda especifica basado en el valor o valor reverso 
-- =============================================
CREATE FUNCTION [Portfolio].[fnConvertValueBasedOnExchangeRate]
(
    @value numeric(20,5),
	@valueRate numeric(20,5), -- Si convierto con esta variable multiplico
	@valueRateReverse numeric(20,5) -- Si convierto con esta variable divido
)
RETURNS numeric(18,2)
AS
BEGIN
	declare @valueConverted numeric(20,5)

	if (@valueRate > @valueRateReverse) begin
		set @valueConverted = @value * @valueRate
		return @valueConverted
	end

		set @valueConverted = @value / @valueRateReverse
	return @valueConverted

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convierte un valor monetario a una moneda específica aplicando una tasa de cambio. Recibe el monto original, la tasa directa y la tasa inversa: si la tasa directa es mayor que la inversa, multiplica el valor por la tasa directa; en caso contrario, divide el valor por la tasa inversa. Se usa en el módulo de Portafolio para transformar valores financieros entre monedas, por ejemplo al calcular costos o precios en moneda extranjera dentro de contratos o cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'FUNCTION', @level1name = N'fnConvertValueBasedOnExchangeRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'FUNCTION', @level1name = N'fnConvertValueBasedOnExchangeRate';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Convierte un valor monetario aplicando una tasa de cambio directa o inversa según cuál de las dos tasas suministradas sea mayor.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'fnConvertValueBasedOnExchangeRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se deben suministrar dos tasas de cambio numéricas (directa e inversa) comparables entre sí.; La tasa inversa no debe ser cero cuando sea la utilizada, para evitar división por cero.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'fnConvertValueBasedOnExchangeRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La selección entre multiplicar o dividir depende exclusivamente de cuál tasa es mayor.; El resultado se entrega con precisión numeric(18,2) aunque internamente se calcule con numeric(20,5).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'fnConvertValueBasedOnExchangeRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tasa de cambio; Conversión de moneda; Portafolio', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'fnConvertValueBasedOnExchangeRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Si @valueRate > @valueRateReverse retorna @value * @valueRate; en caso contrario retorna @value / @valueRateReverse.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'fnConvertValueBasedOnExchangeRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @valueRate > @valueRateReverse → Multiplica el valor por @valueRate (conversión directa). else Divide el valor entre @valueRateReverse (conversión inversa).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'fnConvertValueBasedOnExchangeRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'fnConvertValueBasedOnExchangeRate';
GO
