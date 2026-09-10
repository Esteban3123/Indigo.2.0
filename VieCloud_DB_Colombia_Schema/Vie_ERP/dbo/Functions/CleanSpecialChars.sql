
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
create FUNCTION [dbo].[CleanSpecialChars]
(
	@Cadena varchar(400)
)
RETURNS VARCHAR(400)
AS
BEGIN
-- Declare the return variable here
	DECLARE @resultado varchar(400)

	-- Add the T-SQL statements to compute the return value here
	set @resultado = REPLACE(@Cadena,'Ñ','N');
    set @resultado = REPLACE(@resultado,'á','a');
    set @resultado = REPLACE(@resultado,'Á','A');
    set @resultado = REPLACE(@resultado,'é','e');
    set @resultado = REPLACE(@resultado,'É','E');
    set @resultado = REPLACE(@resultado,'í','i');
    set @resultado = REPLACE(@resultado,'Í','I');
    set @resultado = REPLACE(@resultado,'ó','o');
    set @resultado = REPLACE(@resultado,'Ó','O');
    set @resultado = REPLACE(@resultado,'ú','u');
    set @resultado = REPLACE(@resultado,'Ú','U');	

	DECLARE @IncorrectCharLoc SMALLINT
	SET @IncorrectCharLoc = PATINDEX('%[^0-9A-Za-z ]%', @resultado)
	WHILE @IncorrectCharLoc > 0	BEGIN
		SET @resultado = STUFF(@resultado, @IncorrectCharLoc, 1, '')
		SET @IncorrectCharLoc = PATINDEX('%[^0-9A-Za-z ]%', @resultado)
	END

	set @resultado = @resultado
	-- Return the result of the function
	
	RETURN UPPER(@resultado)
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de limpieza y normalización de texto que recibe una cadena de caracteres y devuelve la misma en mayúsculas, sin tildes, sin la letra Ñ y sin caracteres especiales. Reemplaza vocales acentuadas (á, é, í, ó, ú) y la Ñ por sus equivalentes sin tilde, luego elimina cualquier símbolo que no sea letra, número o espacio. Se utiliza para estandarizar nombres, direcciones u otros campos de texto en el sistema antes de almacenarlos o compararlos, garantizando consistencia en búsquedas y reportes de pacientes, contratos y demás entidades de negocio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'CleanSpecialChars';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'CleanSpecialChars';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Normaliza una cadena removiendo tildes, eñes y cualquier carácter no alfanumérico, devolviéndola en mayúsculas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CleanSpecialChars';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cadena de entrada no debe exceder 400 caracteres', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CleanSpecialChars';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado solo contiene dígitos 0-9, letras A-Z y espacios; Las vocales acentuadas se sustituyen por su equivalente sin tilde antes de filtrar; La Ñ se normaliza a N; El resultado final siempre se devuelve en mayúsculas; La longitud del resultado nunca excede 400 caracteres', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CleanSpecialChars';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Devuelve la cadena con vocales acentuadas y Ñ reemplazadas por su equivalente ASCII, sin caracteres distintos a [0-9A-Za-z ], convertida a mayúsculas mediante UPPER', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CleanSpecialChars';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PATINDEX detecta carácter fuera de [0-9A-Za-z ] → Elimina ese carácter con STUFF y vuelve a evaluar hasta no encontrar más else Termina el bucle de limpieza', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CleanSpecialChars';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CleanSpecialChars';
GO
