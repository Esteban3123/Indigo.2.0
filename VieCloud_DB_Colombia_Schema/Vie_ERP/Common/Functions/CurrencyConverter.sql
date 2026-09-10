
-- =============================================
-- Author:		Giovanny Plazas L
-- Create date: 24/10/2022
-- Description:	Convierte de una moneda a otra (Retorna el Valor)
-- =============================================
CREATE Function [Common].[CurrencyConverter]
(
	@Value Decimal(21,5),
	@FromCurrencyId Int,
	@ToCurrencyConvertId int
)
Returns Decimal(21,5)
As
Begin 
	--Logica para el cambio de moneda se translada a la siguiente funcion
	RETURN  [Common].[CurrencyConverterWithDate](@Value, @FromCurrencyId, @ToCurrencyConvertId, Common.GETDATE() )
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convierte un monto económico de una moneda origen a una moneda destino, retornando el valor equivalente según la tasa de cambio vigente a la fecha actual. Recibe el valor a convertir, el identificador de la moneda de origen y el identificador de la moneda destino, y delega el cálculo real a la función [Common].[CurrencyConverterWithDate] usando la fecha del sistema como referencia temporal. Se utiliza en procesos de facturación, contratos y liquidaciones donde los valores monetarios deben expresarse en diferentes monedas (por ejemplo, pesos colombianos, dólares u otras divisas). Es un atajo conveniente cuando no se necesita especificar una fecha particular para la conversión.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'CurrencyConverter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'CurrencyConverter';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Convierte un valor monetario de una moneda origen a una moneda destino usando la tasa vigente a la fecha actual.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las monedas origen y destino deben existir y tener tasa de conversión definida para la fecha actual en la función subyacente.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La conversión siempre se realiza con la fecha actual del sistema (Common.GETDATE()), nunca con una fecha histórica.; No implementa lógica de conversión propia; centraliza el cálculo en Common.CurrencyConverterWithDate.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Conversión de moneda; Tasa de cambio', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (retorno escalar): Siempre delega el cálculo a Common.CurrencyConverterWithDate pasando la fecha actual obtenida con Common.GETDATE() y retorna el valor convertido.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.CurrencyConverterWithDate; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'CurrencyConverter';
GO
