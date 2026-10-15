CREATE FUNCTION [Payroll].[fnCalculateDays360] (@InitialDate Date, @EndDate Date)
RETURNS INT
AS
BEGIN


	declare @TotalDays integer
	declare @AuxiliarDays integer
 
	If Day(@EndDate) > 30 BEGIN
		SET @AuxiliarDays = 0
	END ELSE IF Day(@EndDate) = 28 And Month(@EndDate) = 2 BEGIN
		SET @AuxiliarDays = 2
	END ELSE IF Day(@EndDate) = 29 And Month(@EndDate) = 2 BEGIN
		SET @AuxiliarDays = 1
	END ELSE BEGIN
		SET @AuxiliarDays = 1
	END

	If Month(@EndDate) = 2 And Day(@EndDate) = 28 BEGIN
		SET @AuxiliarDays = 3
	END

	If Month(@EndDate) = 2 And Day(@EndDate) = 29 BEGIN
		SET @AuxiliarDays = 2
	END

	If Month(@InitialDate) = 2 And Day(@InitialDate) = 1 And Month(@EndDate) = 2 And Day(@EndDate) = 28 BEGIN
		SET @AuxiliarDays = 3
	END

	If Month(@InitialDate) = 2 And Day(@InitialDate) = 1 And Month(@EndDate) = 2 And Day(@EndDate) = 29 BEGIN
		SET @AuxiliarDays = 2
	END

	If Abs(Year(@InitialDate) - Year(@EndDate)) = 0 BEGIN
		SET @TotalDays = 30 - Day(@InitialDate) + (Abs(Month(@InitialDate) - Month(@EndDate)) - 1) * 30 + Day(@EndDate) + @AuxiliarDays
	END ELSE BEGIN
		SET @TotalDays = (Abs(Year(@EndDate) - Year(@InitialDate) - 1) * 360) + (360 - Month(@InitialDate) * 30) + ((Month(@EndDate) - 1) * 30 + Day(@EndDate)) + (30 - Day(@InitialDate)) + @AuxiliarDays
	END

	Return @TotalDays



END