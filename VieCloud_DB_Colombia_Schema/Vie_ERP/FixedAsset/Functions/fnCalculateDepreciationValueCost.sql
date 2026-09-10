CREATE FUNCTION [FixedAsset].[fnCalculateDepreciationValueCost] (@DepreciateValue numeric(20,4), @DepreciateDays int, @DepreciateDaysCost int)
RETURNS numeric(20,4)
AS
BEGIN
	--Variable que retorna el valor depreciado de la tabla fixedAssetDepreciationDetailCost
	declare @DepreciateValueCost numeric(20,4)

	set @DepreciateValueCost = (@DepreciateValue * @DepreciateDaysCost) / @DepreciateDays

	RETURN ROUND(@DepreciateValueCost, 2)
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el valor de depreciación proporcional de un activo fijo para un período específico de días. Recibe el valor total a depreciar, el total de días del período de depreciación y los días efectivos a costear, y devuelve el monto depreciado correspondiente a esos días mediante una regla de tres simple, redondeado a dos decimales. Se utiliza en el módulo de activos fijos para distribuir el costo de depreciación diaria en el detalle de costos de depreciación (FixedAssetDepreciationDetailCost), permitiendo calcular cuánto se deprecia un bien en un rango de días determinado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'FUNCTION', @level1name = N'fnCalculateDepreciationValueCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'FUNCTION', @level1name = N'fnCalculateDepreciationValueCost';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el valor de depreciación proporcional asignado a un componente de costo, distribuyendo el valor depreciado total según los días de costo respecto al total de días depreciados.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDepreciationValueCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El total de días depreciados debe ser distinto de cero para evitar división por cero.; Los valores de entrada (valor depreciado, días) deben estar definidos.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDepreciationValueCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre se redondea a 2 decimales.; El valor calculado es proporcional: (valor * días_costo) / días_totales.; Si días_costo = días_totales, el resultado es el valor depreciado original (redondeado).', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDepreciationValueCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Depreciación de activos fijos; Distribución de costo de depreciación; Días de depreciación', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDepreciationValueCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'FUNCTION', @level1name=N'fnCalculateDepreciationValueCost';
GO
