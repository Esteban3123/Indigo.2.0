-- =============================================
-- Author:		Andrés Steven Rojas
-- Create date: 2026-01-13
-- Description:	Convierte el RoundingType de Currency al parámetro de precisión para SQL ROUND
--              RoundingType values:
--              1 = 0.01 (dos decimales)
--              2 = 0.1 (un decimal)
--              3 = 1 (sin decimales)
--              4 = 10 (a la decena)
--              5 = 100 (a la centena)
--              6 = 1000 (a la milésima)
-- =============================================
CREATE FUNCTION [Common].[GetRoundPrecision]
(
	@RoundingType TINYINT
)
RETURNS INT
AS
BEGIN
	RETURN CASE @RoundingType
		WHEN 1 THEN 2   -- Dos decimales (0.01)
		WHEN 2 THEN 1   -- Un decimal (0.1)
		WHEN 3 THEN 0   -- Sin decimales (1)
		WHEN 4 THEN -1  -- Decenas (10)
		WHEN 5 THEN -2  -- Centenas (100)
		WHEN 6 THEN -3  -- Miles (1000)
		ELSE 2          -- Default: dos decimales
	END
END
