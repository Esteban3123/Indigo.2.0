CREATE FUNCTION [FixedAsset].[fnCalculateDepreciationValueCost] (@DepreciateValue numeric(20,4), @DepreciateDays int, @DepreciateDaysCost int)
RETURNS numeric(20,4)
AS
BEGIN
	--Variable que retorna el valor depreciado de la tabla fixedAssetDepreciationDetailCost
	declare @DepreciateValueCost numeric(20,4)

	set @DepreciateValueCost = (@DepreciateValue * @DepreciateDaysCost) / @DepreciateDays

	RETURN ROUND(@DepreciateValueCost, 2)
END