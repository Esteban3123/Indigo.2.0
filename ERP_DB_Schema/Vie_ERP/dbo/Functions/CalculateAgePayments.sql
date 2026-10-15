

CREATE FUNCTION [dbo].[CalculateAgePayments] (@Fecha as datetime, @AccountPayableId as int)
RETURNS varchar(100)
AS
BEGIN

declare @Dias int
declare @Edad varchar(100)


SELECT @Dias = DATEDIFF(DAY, @Fecha, GETDATE()) + 1 


--set @edad = (SELECT Name 
--			FROM Payments.AgesPayments 
--			WHERE InitialRange <= @dias and EndRange >= @dias)

set @Edad = (SELECT DISTINCT AGP.Name
	FROM Payments.AccountPayable as AP
	inner join Payments.SettingPayments as SP on SP.IdOperatingUnit = AP.IdOperatingUnit
	inner join Payments.AgesPayments as AGP on AGP.SettingPaymentId = SP.Id
	WHERE InitialRange <= @Dias and EndRange >= @Dias and AP.Id = @AccountPayableId)



RETURN @Edad

END