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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el dígito de verificación de un NIT (Número de Identificación Tributaria) utilizando el algoritmo módulo 11, estándar exigido por la DIAN en Colombia. Recibe el NIT sin dígito de verificación y retorna el dígito correspondiente (0 al 9). Se usa para validar o completar la identificación tributaria de empresas, prestadores, aseguradoras o cualquier tercero registrado en el sistema. Es útil en procesos de facturación electrónica, generación de RIPS y verificación de contratos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'fnCalculateVerificationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'fnCalculateVerificationCode';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el dígito de verificación de un NIT aplicando el algoritmo de ponderación por primos y módulo 11.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'fnCalculateVerificationCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El NIT recibido debe ser numérico una vez normalizado a 15 posiciones; en caso contrario la función retorna 0', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'fnCalculateVerificationCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El NIT se normaliza rellenando con ceros a la izquierda hasta 15 caracteres antes del cálculo; Cada posición del NIT se pondera con un primo fijo (3,7,13,17,19,23,29,37,41,43,47,53,59,67,71) según su posición; El resultado siempre es un único carácter numérico (0-9); Cuando el residuo módulo 11 es 0 o 1 el dígito coincide con el residuo; en otro caso es el complemento a 11', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'fnCalculateVerificationCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'NIT; Dígito de verificación', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'fnCalculateVerificationCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (retorno escalar): Si el NIT no es numérico retorna ''0''; en caso contrario retorna el dígito de verificación calculado como residuo mod 11 (si es 0/1) o 11-residuo (en otro caso)', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'fnCalculateVerificationCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNUMERIC sobre el NIT normalizado es 0 (no numérico) → Retorna 0 sin calcular dígito else Procede a calcular el dígito de verificación con algoritmo módulo 11; si Residuo del módulo 11 está en (0,1) → El dígito de verificación es el propio residuo else El dígito de verificación es 11 menos el residuo', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'fnCalculateVerificationCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'fnCalculateVerificationCode';
GO
