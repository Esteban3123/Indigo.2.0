CREATE FUNCTION [Billing].[DuplicateRows]
(
	@Id int,
	@Value int
)  
RETURNS @RtnValue table 
(
	Id int
) 
AS  
BEGIN 
	Declare @Cnt int
	Set @Cnt = 0

	While @Cnt < @Value
	Begin
		Insert Into @RtnValue (Id) values (@Id)
		Set @Cnt = @Cnt + 1
	End
	
	Return
END