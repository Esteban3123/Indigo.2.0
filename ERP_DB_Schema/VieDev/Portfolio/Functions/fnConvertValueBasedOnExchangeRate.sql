



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