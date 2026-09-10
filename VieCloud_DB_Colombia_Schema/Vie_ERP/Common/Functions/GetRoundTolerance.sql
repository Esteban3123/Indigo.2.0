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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que devuelve la tolerancia de redondeo monetario según el tipo de redondeo configurado para una moneda o divisa. Recibe un código de tipo de redondeo (1 al 6) y retorna el valor decimal aceptable como diferencia permitida entre dos cifras monetarias (por ejemplo, 0.01 para dos decimales, 1 para enteros, 10 para decenas, hasta 1000 para miles). Se utiliza en validaciones financieras y de facturación donde dos valores pueden diferir levemente por efecto del redondeo y esa diferencia debe considerarse aceptable. Es clave en procesos de conciliación, comparación de totales de facturas, glosas y liquidaciones donde se necesita determinar si una discrepancia es real o simplemente producto del redondeo de la moneda.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'GetRoundTolerance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'GetRoundTolerance';

El valor retornado siempre es positivo y corresponde a una potencia de 10 entre 0.01 y 1000.; Ante un RoundingType desconocido nunca falla: aplica el default 0.01.  |  Conceptos de dominio: Moneda (Currency); Tipo de redondeo (RoundingType); Tolerancia de redondeo  |  Side effects: [RETURN_RESULT] (retorno escalar): Cuando RoundingType=1 retorna 0.01; =2 retorna 0.1; =3 retorna 1; =4 retorna 10; =5 retorna 100; =6 retorna 1000; cualquier otro valor (incluido NULL) retorna 0.01 por defecto.  |  Decisiones: si RoundingType = 1 → Tolerancia 0.01 (dos decimales); si RoundingType = 2 → Tolerancia 0.1 (un decimal); si RoundingType = 3 → Tolerancia 1 (sin decimales); si RoundingType = 4 → Tolerancia 10 (decenas); si RoundingType = 5 → Tolerancia 100 (centenas); si RoundingType = 6 → Tolerancia 1000 (miles)', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundTolerance';

GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la tolerancia de redondeo asociada a un tipo de redondeo de moneda, para permitir comparaciones de valores con diferencias aceptables.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundTolerance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La tolerancia retornada siempre es un DECIMAL(18,2) no nulo.; Ante un tipo de redondeo desconocido o nulo, la tolerancia mínima por defecto es 0.01.; La tolerancia es coherente con la escala del RoundingType (a mayor escala de redondeo, mayor tolerancia).', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundTolerance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tolerancia de redondeo; Tipo de redondeo de moneda (RoundingType); Currency', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundTolerance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (retorno escalar): Mapea RoundingType a tolerancia: 1→0.01, 2→0.1, 3→1, 4→10, 5→100, 6→1000; cualquier otro valor (incluido NULL) retorna 0.01 por defecto.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundTolerance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @RoundingType = 1 → Retorna 0.01 (dos decimales); si @RoundingType = 2 → Retorna 0.1 (un decimal); si @RoundingType = 3 → Retorna 1 (sin decimales); si @RoundingType = 4 → Retorna 10 (decenas); si @RoundingType = 5 → Retorna 100 (centenas); si @RoundingType = 6 → Retorna 1000 (miles); si @RoundingType no coincide con valores 1-6 → Retorna 0.01 como tolerancia por defecto', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundTolerance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetRoundTolerance';
GO
