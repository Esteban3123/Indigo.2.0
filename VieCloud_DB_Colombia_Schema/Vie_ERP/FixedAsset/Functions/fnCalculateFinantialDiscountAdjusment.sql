CREATE FUNCTION [FixedAsset].[fnCalculateFinantialDiscountAdjusment] 
(
	@FinantialDiscountDepreciated NUMERIC(18,2),
	@FinantialDiscountDepreciation NUMERIC(18,2),
	@FinantialDiscount NUMERIC(18,2),
	@DepreciatedDays INT,
	@DaysPendingDepreciate INT
)
RETURNS NUMERIC(18,2)
AS
BEGIN
	DECLARE @Adjustment NUMERIC(18,2),
			@FinantialDiscountCalculated NUMERIC(18,2)

	SET @FinantialDiscountCalculated = IIF(@FinantialDiscountDepreciated + @FinantialDiscountDepreciation >= @FinantialDiscount, 0, @FinantialDiscount) 

	SET @Adjustment = @FinantialDiscountCalculated * @DepreciatedDays / (@DepreciatedDays + @DaysPendingDepreciate) 

	RETURN @Adjustment
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el ajuste del descuento financiero aplicable a un activo fijo durante el proceso de depreciación. Determina si el descuento financiero acumulado (ya depreciado más el del período actual) ya alcanzó o superó el total del descuento financiero original; si es así, el ajuste es cero para evitar doble cómputo. En caso contrario, distribuye el descuento financiero restante de forma proporcional según los días ya depreciados respecto al total de días de vida útil (días depreciados más días pendientes por depreciar). Esta función se utiliza en el módulo de activos fijos para garantizar que el descuento financiero se amortice correctamente a lo largo del ciclo de vida del activo sin exceder el monto pactado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'FUNCTION', @level1name = N'fnCalculateFinantialDiscountAdjusment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'FUNCTION', @level1name = N'fnCalculateFinantialDiscountAdjusment';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el ajuste proporcional del descuento financiero a aplicar en la depreciación de un activo fijo, evitando que el descuento acumulado exceda el monto pactado.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateFinantialDiscountAdjusment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La suma de días depreciados y días pendientes por depreciar debe ser distinta de cero para evitar división por cero; Los montos de descuento financiero deben estar expresados en la misma unidad/moneda y escala', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateFinantialDiscountAdjusment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El ajuste se prorratea proporcionalmente a la fracción de días ya depreciados sobre el total de vida útil (días depreciados + días pendientes); Si la suma de descuento depreciado y depreciación corriente alcanza o supera el descuento total pactado, el ajuste resultante es 0; El resultado se devuelve con precisión NUMERIC(18,2)', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateFinantialDiscountAdjusment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'activo fijo; depreciación; descuento financiero; ajuste de depreciación; días depreciados; días pendientes por depreciar', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateFinantialDiscountAdjusment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Descuento financiero ya depreciado más la depreciación actual del descuento es mayor o igual al descuento financiero total → Se considera el descuento como 0 para el cálculo del ajuste (evita exceder el monto pactado) else Se toma el descuento financiero completo como base para prorratear el ajuste', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateFinantialDiscountAdjusment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateFinantialDiscountAdjusment';
GO
