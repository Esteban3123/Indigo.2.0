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