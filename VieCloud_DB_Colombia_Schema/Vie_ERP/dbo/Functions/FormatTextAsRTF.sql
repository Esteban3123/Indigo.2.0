
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convierte texto plano en formato RTF (Rich Text Format) para que pueda ser renderizado correctamente en campos de texto enriquecido del sistema clínico. Si el texto de entrada está vacío o es nulo, genera un RTF con el mensaje ''No dato''; si ya es RTF lo devuelve sin cambios; de lo contrario, escapa los caracteres especiales y envuelve el contenido en la estructura RTF con fuente Arial. Se utiliza principalmente para formatear notas clínicas, observaciones y textos de historia clínica que requieren presentación visual estructurada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'FormatTextAsRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'FormatTextAsRTF';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Convierte texto plano a formato RTF con fuente Arial y color negro, preservando contenido ya formateado en RTF y sustituyendo entradas vacías por la leyenda ''No dato''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FormatTextAsRTF';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre es una cadena RTF válida o el contenido RTF original.; Todo RTF generado usa codepage 1252, fuente Arial (f0/fswiss/fcharset0), color negro (red0 green0 blue0) y tamaño 17 half-points (8.25pt).; Los caracteres ''\'', ''{'' y ''}'' del texto plano siempre se escapan antes de incluirse en el RTF.; Nunca retorna NULL: las entradas nulas o vacías se sustituyen por ''No dato''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FormatTextAsRTF';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando el texto de entrada es NULL o cadena vacía, retorna un bloque RTF con el literal ''No dato''.; [RETURN_RESULT] N/A: Cuando el texto comienza con ''{\rtf'' (primeros 5 caracteres), retorna el texto tal cual sin transformación.; [RETURN_RESULT] N/A: Cuando el texto es plano, escapa los caracteres ''\'', ''{'' y ''}'' y lo envuelve en una estructura RTF con fuente Arial, color negro y tamaño 8.25pt (fs17).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FormatTextAsRTF';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @InputText IS NULL OR @InputText = '''' → Genera RTF con texto ''No dato'' else Evalúa si ya es RTF o requiere conversión; si LEFT(@InputText,5) = ''{\rtf'' → Devuelve el texto sin modificación else Escapa caracteres especiales y envuelve en estructura RTF', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FormatTextAsRTF';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FormatTextAsRTF';
GO
