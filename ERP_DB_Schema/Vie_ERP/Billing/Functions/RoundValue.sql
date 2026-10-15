-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 03-10-2017
-- Description:	Devuelve la categoria del tipo de unidad
-- =============================================
CREATE Function [Billing].[RoundValue]
(
	@valueToRound Decimal(18, 2),
	@roundLevel Int
)
Returns Decimal(18, 2)
As
Begin
	Declare @RoundedValue Decimal(18, 2) = @valueToRound
	
	If @roundLevel = 1
		Set @RoundedValue = Round(@RoundedValue, 0)		
	Else If @roundLevel = 10
		Set @RoundedValue = Round(@RoundedValue, -1)		
	Else If @roundLevel = 100
		Set @RoundedValue = Round(@RoundedValue, -2)		
	Else If @roundLevel = 1000
		Set @RoundedValue = Round(@RoundedValue, -3)		
	Else
		Set @RoundedValue = Round(@RoundedValue, 2)

	Return @RoundedValue
End