
create FUNCTION [Taxes].[fnCalculateTaxPercentage] (@Appraisal as decimal(18,0), @SMLV as decimal(18,0))
RETURNS decimal(5,2)
AS
BEGIN

--Porcentaje impuesto
declare @TaxPercentage decimal(5,2)

--Se calcula la comparación
declare @Compare decimal(18,0) = ROUND((@Appraisal / @SMLV), 0)

--Se realizan las comparaciones para establecer el porcentaje del impuesto y el valor del impuesto
set @TaxPercentage = 0

--Avaluo mayor a 10 y hasta 50 salarios minimos
if @Compare > 10 and @Compare <= 50
Begin
	set @TaxPercentage = 4.2
End

--Avaluo mayores a 50 y hasta 90 salarios minimos
if @Compare > 50 and @Compare <= 90
Begin
	set @TaxPercentage = 5.2
End

--Avaluo mayores a 90 y hasta 130 salarios minimos
if @Compare > 90 and @Compare <= 130
Begin
	set @TaxPercentage = 6.2
End

--Avaluo mayores a 130 y hasta 170 salarios minimos
if @Compare > 130 and @Compare <= 170
Begin
	set @TaxPercentage = 7.5
End

--Avaluo mayores a 170 y hasta 250 salarios minimos
if @Compare > 170 and @Compare <= 250
Begin
	set @TaxPercentage = 8.5
End

--Avaluo mayores a 250 y hasta 350 salarios minimos
if @Compare > 250 and @Compare <= 350
Begin
	set @TaxPercentage = 9.5
End

--Avaluo mayores a 350 y hasta 450 salarios minimos
if @Compare > 350 and @Compare <= 450
Begin
	set @TaxPercentage = 10.5
End

--Avaluo mayores a 450
if @Compare > 450
Begin
	set @TaxPercentage = 12
End

Return @TaxPercentage

END