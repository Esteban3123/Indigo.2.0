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

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código de tipo de redondeo de moneda al valor de precisión numérica que requiere la función ROUND de SQL Server.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundPrecision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor retornado siempre está en el rango [-3, 2].; Nunca retorna NULL: ante cualquier entrada no contemplada se garantiza precisión 2.; Existe correspondencia 1 a 1 entre los códigos válidos (1..6) y los niveles de precisión soportados por ROUND.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundPrecision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Redondeo de moneda; Precisión monetaria; Currency RoundingType', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundPrecision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando RoundingType=1 retorna 2; =2 retorna 1; =3 retorna 0; =4 retorna -1; =5 retorna -2; =6 retorna -3; cualquier otro valor (incluido NULL) retorna 2 por defecto.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundPrecision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RoundingType = 1 → Precisión 2 (dos decimales, 0.01); si RoundingType = 2 → Precisión 1 (un decimal, 0.1); si RoundingType = 3 → Precisión 0 (sin decimales, unidad); si RoundingType = 4 → Precisión -1 (decenas); si RoundingType = 5 → Precisión -2 (centenas); si RoundingType = 6 → Precisión -3 (miles); si RoundingType fuera del rango 1-6 → Precisión 2 por defecto (dos decimales)', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundPrecision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundPrecision';
GO
