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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función auxiliar que convierte el tipo de redondeo de moneda (RoundingType) al parámetro de precisión que usa la función SQL ROUND. Recibe un código numérico del 1 al 6 que representa distintos niveles de redondeo monetario: desde dos decimales (centavos) hasta miles, pasando por decenas y centenas. Se usa en cálculos de valores financieros, tarifas, facturas y cobros para garantizar que los montos se redondeen según la configuración de precisión monetaria definida en el sistema.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'GetRoundPrecision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'GetRoundPrecision';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce el código de tipo de redondeo de moneda al parámetro numérico de precisión esperado por la función ROUND de SQL.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundPrecision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor retornado siempre es un entero entre -3 y 2.; Ante un tipo de redondeo desconocido, nunca falla: aplica el comportamiento por defecto de dos decimales.; Mapeo determinista 1:1 entre el código de redondeo y el parámetro de precisión de ROUND.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundPrecision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Moneda (Currency); Tipo de redondeo (RoundingType); Precisión de redondeo', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundPrecision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve 2 cuando el tipo es 1 (redondeo a 0.01).; [RETURN_RESULT] N/A: Devuelve 1 cuando el tipo es 2 (redondeo a 0.1).; [RETURN_RESULT] N/A: Devuelve 0 cuando el tipo es 3 (redondeo a unidades).; [RETURN_RESULT] N/A: Devuelve -1 cuando el tipo es 4 (redondeo a decenas).; [RETURN_RESULT] N/A: Devuelve -2 cuando el tipo es 5 (redondeo a centenas).; [RETURN_RESULT] N/A: Devuelve -3 cuando el tipo es 6 (redondeo a miles).; [RETURN_RESULT] N/A: Cuando el tipo no coincide con ningún valor definido (1-6), retorna 2 como precisión por defecto (dos decimales).', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundPrecision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de redondeo = 1 → Precisión 2 decimales; si Tipo de redondeo = 2 → Precisión 1 decimal; si Tipo de redondeo = 3 → Precisión 0 (unidades); si Tipo de redondeo = 4 → Precisión -1 (decenas); si Tipo de redondeo = 5 → Precisión -2 (centenas); si Tipo de redondeo = 6 → Precisión -3 (miles) else Precisión por defecto = 2 decimales', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundPrecision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundPrecision';
GO
