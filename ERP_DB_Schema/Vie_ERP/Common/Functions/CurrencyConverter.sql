
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