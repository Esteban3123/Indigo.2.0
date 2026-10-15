-- =============================================
-- Author:		Juan David Capera Núñez
-- Create date: 2023-09-07
-- Description:	Calcula el digito de verificación
-- =============================================
CREATE FUNCTION [Common].[fnCalculateVerificationCode](@nit AS VARCHAR(15))
RETURNS VARCHAR(1)
BEGIN
	DECLARE @verificationCode AS INT = 0
	------------------------------------
	DECLARE @nits AS CHAR(15)
	DECLARE @i AS INT = 15,
			@mul AS INT = 0,
			@residue AS INT = 0

  if ISNUMERIC(@nits) = 0 begin
	return 0
  end

	SET @nits = RIGHT('000000000000000' + @nit, 15)

	WHILE @i >= 1 
	BEGIN
		--homologo del valor según algoritmo mod 11
		SET @mul =
		(
			SELECT CASE @i
				WHEN 15 THEN 3
				WHEN 14 THEN 7
				WHEN 13 THEN 13
				WHEN 12 THEN 17
				WHEN 11 THEN 19
				WHEN 10 THEN 23
				WHEN 9 THEN 29
				WHEN 8 THEN 37
				WHEN 7 THEN 41
				WHEN 6 THEN 43
				WHEN 5 THEN 47
				WHEN 4 THEN 53
				WHEN 3 THEN 59
				WHEN 2 THEN 67
				ELSE 71
			END 
		)

		SET @residue = @residue + (CAST(SUBSTRING(@nits, @i, 1) AS INT) * @mul)
		--Decremento posición
		SET @i -= 1 
	END
	SET @residue = @residue % 11

	IF @residue IN (0,1)
	BEGIN
		SET @verificationCode = @residue
	END
	ELSE 
	BEGIN
		SET @verificationCode = 11 - @residue
	END	

	RETURN CAST(@verificationCode AS VARCHAR(1))
END