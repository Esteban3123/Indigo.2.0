
CREATE FUNCTION [dbo].[FormatTextAsRTF]
(
    @InputText NVARCHAR(MAX)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @RTFText NVARCHAR(MAX)

    -- Verificar si el texto es NULL o un string vacío
    IF @InputText IS NULL OR @InputText = ''
    BEGIN
        -- Convertir "No dato" a RTF
        SET @RTFText = 
            '{\rtf1\ansi\ansicpg1252\uc1\deff0' +
            '{\fonttbl{\f0\fswiss\fcharset0 Arial;}}' + -- Fuente Arial
            '{\colortbl;\red0\green0\blue0;}' + -- Color negro
            '\viewkind4\uc1\pard\cf1\f0\fs17 ' + -- Tamaño 8.25 pt (17 half-points)
            'No dato\par}'
    END
    ELSE IF LEFT(@InputText, 5) = '{\rtf'
    BEGIN
        -- Si el texto ya es RTF, no realizar ninguna conversión
        SET @RTFText = @InputText
    END
    ELSE
    BEGIN
        -- Convertir texto plano a RTF
        SET @RTFText = 
            '{\rtf1\ansi\ansicpg1252\uc1\deff0' +
            '{\fonttbl{\f0\fswiss\fcharset0 Arial;}}' + -- Fuente Arial
            '{\colortbl;\red0\green0\blue0;}' + -- Color negro
            '\viewkind4\uc1\pard\cf1\f0\fs17 ' + -- Tamaño 8.25 pt (17 half-points)
            REPLACE(REPLACE(REPLACE(@InputText, '\', '\\'), '{', '\{'), '}', '\}') + -- Escapar caracteres
            '\par}'
    END

    RETURN @RTFText
END