
CREATE Function [Billing].[SetValueSalesPrice]
(
	@FlagTaxInclude As BIT,
	@SalesPrice As Decimal(20,2),
	@TaxPercent As Decimal(10,2)
)
Returns @SetValueSalesPrice Table
(
	StatusResult Bit,
	MessageResult Varchar(Max),
	
	GrossValue decimal(20,2),
	TaxValue decimal(20,2),
	SalesPrice decimal(20,2)
) 
As
Begin	
	set @SalesPrice= ISNULL(@SalesPrice,0)
	set @TaxPercent = ISNULL(@TaxPercent,0)

	declare @_grossValue decimal(20,2),
			@_taxValue decimal(20,2),
			@_salesPrice decimal(20,2)

	if @FlagTaxInclude=1 begin
		set @_salesPrice = @SalesPrice
		set @_grossValue =ROUND(@SalesPrice/((@TaxPercent/100) + 1),2)
		set @_taxValue =  iif(@TaxPercent=0,0,ROUND(@SalesPrice - (@SalesPrice/((@TaxPercent/100) + 1)),2))
	end
	else begin
		set @_grossValue = @SalesPrice
		set @_taxValue = ROUND(@SalesPrice * (@TaxPercent/100),2)
		set @_salesPrice = @SalesPrice + ROUND((@SalesPrice * (@TaxPercent/100)),2)
	end

	insert INTO @SetValueSalesPrice VALUES (1, 'Calculó existosamente', @_grossValue,@_taxValue,@_salesPrice)

	Return
End