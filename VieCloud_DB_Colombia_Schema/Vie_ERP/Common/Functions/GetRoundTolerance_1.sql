-- =============================================
-- Author:		Andrés Steven Rojas
-- Create date: 2026-01-13
-- Description:	Devuelve la tolerancia de redondeo basada en el RoundingType de Currency
--              Útil para validaciones donde se comparan valores que pueden tener 
--              diferencias de redondeo aceptables
--              RoundingType values:
--              1 = 0.01 (dos decimales) -> tolerancia 0.01
--              2 = 0.1 (un decimal) -> tolerancia 0.1
--              3 = 1 (sin decimales) -> tolerancia 1
--              4 = 10 (a la decena) -> tolerancia 10
--              5 = 100 (a la centena) -> tolerancia 100
--              6 = 1000 (a la milésima) -> tolerancia 1000
-- =============================================
CREATE FUNCTION [Common].[GetRoundTolerance]
(
	@RoundingType TINYINT
)
RETURNS DECIMAL(18,2)
AS
BEGIN
	RETURN CASE @RoundingType
		WHEN 1 THEN 0.01   -- Dos decimales
		WHEN 2 THEN 0.1    -- Un decimal
		WHEN 3 THEN 1      -- Sin decimales
		WHEN 4 THEN 10     -- Decenas
		WHEN 5 THEN 100    -- Centenas
		WHEN 6 THEN 1000   -- Miles
		ELSE 0.01          -- Default: dos decimales
	END
END