CREATE FUNCTION [Payroll].[fnCalculateSolidarityFundSubsistence] (@LegalMinimunSalary as decimal(18,0), @BasicSalary as decimal(18,0), @IBCPension decimal (18,0))
RETURNS decimal(18,0)
AS
BEGIN

--Porcentaje impuesto
declare @SolidarityFundSubsitence decimal(18,0)

SET @SolidarityFundSubsitence = 0

IF @IBCPension >= (@LegalMinimunSalary * 16) AND @IBCPension <= (@LegalMinimunSalary * 17) BEGIN
	SET @SolidarityFundSubsitence = @IBCPension * (0.012 - 0.005)
END

IF @IBCPension > (@LegalMinimunSalary * 17) AND @IBCPension <= (@LegalMinimunSalary * 18) BEGIN
	SET @SolidarityFundSubsitence = @IBCPension * (0.014 - 0.005)
END

IF @IBCPension > (@LegalMinimunSalary * 18) AND @IBCPension <= (@LegalMinimunSalary * 19) BEGIN
	SET @SolidarityFundSubsitence = @IBCPension * (0.016 - 0.005)
END

IF @IBCPension > (@LegalMinimunSalary * 19) AND @IBCPension <= (@LegalMinimunSalary * 20) BEGIN
	SET @SolidarityFundSubsitence = @IBCPension * (0.020 - 0.005)
END

IF @IBCPension > (@LegalMinimunSalary * 20) BEGIN
	SET @SolidarityFundSubsitence = @IBCPension * (0.01 - 0.005)
END

If @BasicSalary > (@LegalMinimunSalary * 25)  BEGIN
	SET @SolidarityFundSubsitence = (@LegalMinimunSalary * 25) * 0.015
END

Return @SolidarityFundSubsitence

END