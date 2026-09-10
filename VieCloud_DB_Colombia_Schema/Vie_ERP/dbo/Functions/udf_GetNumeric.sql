CREATE FUNCTION [dbo].[udf_GetNumeric](@strAlphaNumeric VARCHAR(256))
RETURNS VARCHAR(256)
AS
BEGIN
	DECLARE @intAlpha INT
	SET @intAlpha = PATINDEX('%[^0-9]%', @strAlphaNumeric)
	
	WHILE @intAlpha > 0
	BEGIN
		SET @strAlphaNumeric = STUFF(@strAlphaNumeric, @intAlpha, 1, '' )
		SET @intAlpha = PATINDEX('%[^0-9]%', @strAlphaNumeric )
	END
	
	RETURN ISNULL(@strAlphaNumeric,0)
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extrae únicamente los dígitos numéricos de una cadena de texto alfanumérica, eliminando letras, espacios, guiones y cualquier otro carácter no numérico. Se utiliza para limpiar y estandarizar valores como cédulas, números de ingreso, códigos o identificaciones que pueden llegar con caracteres no deseados desde formularios o integraciones externas. Recibe un texto alfanumérico y devuelve solo los números contenidos en él; si el resultado es nulo, retorna cero. Es una función utilitaria de saneamiento de datos, útil por ejemplo para normalizar la cédula del paciente, el número de factura o cualquier código de negocio antes de comparaciones o búsquedas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'udf_GetNumeric';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'udf_GetNumeric';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Función utilitaria que extrae únicamente los dígitos numéricos de una cadena alfanumérica, eliminando cualquier carácter no numérico para normalizar códigos antes de comparaciones o búsquedas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'udf_GetNumeric';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor retornado nunca es NULL (se sustituye por 0).; El valor retornado solo contiene dígitos 0-9 o es ''0''.; No modifica datos persistentes; es una función pura de transformación de cadena.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'udf_GetNumeric';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'cédula del paciente; número de factura; código de negocio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'udf_GetNumeric';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Mientras PATINDEX(''%[^0-9]%'', cadena) > 0, se elimina el carácter no numérico con STUFF; al finalizar retorna la cadena resultante.; [RETURN_RESULT] N/A: Si el resultado final es NULL, retorna ''0'' por aplicación de ISNULL(@strAlphaNumeric,0).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'udf_GetNumeric';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PATINDEX(''%[^0-9]%'', cadena) > 0 (existe carácter no numérico) → Itera eliminando uno a uno los caracteres no numéricos hasta que solo queden dígitos. else Retorna la cadena tal cual (ya es puramente numérica o vacía).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'udf_GetNumeric';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'udf_GetNumeric';
GO
