-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 01/06/2020
-- Description:	Devuelve el color con que se pinta
-- =============================================
CREATE FUNCTION [Authorization].[fnGetColor]
(
	@RequestTime INT,			-- Tiempo parametrizado para el proceso
	@RequestElapsedTime INT	-- Tiempo Trasncurrido
)
RETURNS INT
AS
BEGIN
	--Color que se retorna: 1-Verde, 2-Amarillo, 3-Rojo
	declare @color int

	--Si la diferencia es mayor al numero enviado es porque ya se desfaso y se envia rojo
	if @RequestElapsedTime > @RequestTime
	begin
		set @color = 3
	end
	else begin --Si no esta pasado
		--Se divide en dos el numero para saber si la diferencia es mayor a la mitad del numero
		set @RequestTime = @RequestTime / 2

		--Si la diferencia es mayor a la mitad del numero se envia amarillo
		if @RequestElapsedTime > @RequestTime
		begin
			set @color = 2
		end
		else begin --Si es menor a la mitad se envia verde
			set @color = 1
		end
	end

	return @color
END