CREATE Function [MixingStation].[CalculateQuantityTypeMSclass]
(
	@Quantity DECIMAL(18,2),
	@MSclass INT,
	@Weight NUMERIC(18,2)
)
Returns Decimal(18, 2)
As
Begin
	Declare @Result Decimal(18, 2) = 0
	---Calcula la cantidad de tipo de dosis unitaria de tipo reempaque o reenvase
	set @Result = IIF(@MSclass = 5 or @MSclass = 7, @Quantity/@Weight, @Quantity)
	Return @Result
End