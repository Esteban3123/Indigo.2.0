CREATE FUNCTION [dbo].[udf_StripHTML2] (@HTMLText VARCHAR(MAX))
RETURNS VARCHAR(MAX) AS
BEGIN
    DECLARE @Contador INT
    DECLARE @LongCadena INT
    DECLARE @EnTag INT
    DECLARE @Res VARCHAR(MAX)
    DECLARE @TeHTML INT

    SET @EnTag        = 0
    SET @LongCadena = Len(@HTMLText)
    SET @Res        = ''
    SET @TeHTML        = 0

    If @LongCadena = 0
        RETURN @HTMLText
    
    -- Reemplazos de Tags HTML
    SET @HTMLText = REPLACE(@HTMLText, '<br>', char(13) + char(10))
    SET @HTMLText = REPLACE(@HTMLText, '&nbsp;', ' ') 
    SET @HTMLText = REPLACE(@HTMLText, '&euro;', '€')
    SET @HTMLText = REPLACE(@HTMLText, '&ordf;', 'ª')
    SET @HTMLText = REPLACE(@HTMLText, '&ordm;', 'º')
    SET @HTMLText = REPLACE(@HTMLText, '&aacute;', 'á')
    SET @HTMLText = REPLACE(@HTMLText, '&eacute;', 'é')
    SET @HTMLText = REPLACE(@HTMLText, '&iacute;', 'í')
    SET @HTMLText = REPLACE(@HTMLText, '&oacute;', 'ó')
    SET @HTMLText = REPLACE(@HTMLText, '&uacute;', 'ú')
    SET @HTMLText = REPLACE(@HTMLText, '&agrave;', 'à')
    SET @HTMLText = REPLACE(@HTMLText, '&egrave;', 'è')
    SET @HTMLText = REPLACE(@HTMLText, '&igrave;', 'ì')
    SET @HTMLText = REPLACE(@HTMLText, '&ograve;', 'ò')
    SET @HTMLText = REPLACE(@HTMLText, '&ugrave;', 'ù')
    SET @HTMLText = REPLACE(@HTMLText, '&Aacute;', 'Á')
    SET @HTMLText = REPLACE(@HTMLText, '&Eacute;', 'É')
    SET @HTMLText = REPLACE(@HTMLText, '&Iacute;', 'Í')
    SET @HTMLText = REPLACE(@HTMLText, '&Oacute;', 'Ó')
    SET @HTMLText = REPLACE(@HTMLText, '&Uacute;', 'Ú')
    SET @HTMLText = REPLACE(@HTMLText, '&Agrave;', 'À')
    SET @HTMLText = REPLACE(@HTMLText, '&Egrave;', 'È')
    SET @HTMLText = REPLACE(@HTMLText, '&Igrave;', 'Ì')
    SET @HTMLText = REPLACE(@HTMLText, '&Ograve;', 'Ò')
    SET @HTMLText = REPLACE(@HTMLText, '&Ugrave;', 'Ù')
    SET @HTMLText = REPLACE(@HTMLText, '&#192;', 'À')
    SET @HTMLText = REPLACE(@HTMLText, '&#193;', 'Á')
    SET @HTMLText = REPLACE(@HTMLText, '&#199;', 'Ç')
    SET @HTMLText = REPLACE(@HTMLText, '&#200;', 'È')
    SET @HTMLText = REPLACE(@HTMLText, '&#201;', 'É')
    SET @HTMLText = REPLACE(@HTMLText, '&#204;', 'Ì')
    SET @HTMLText = REPLACE(@HTMLText, '&#205;', 'Í')
    SET @HTMLText = REPLACE(@HTMLText, '&#209;', 'Ñ')
    SET @HTMLText = REPLACE(@HTMLText, '&#210;', 'Ò')
    SET @HTMLText = REPLACE(@HTMLText, '&#211;', 'Ó')
    SET @HTMLText = REPLACE(@HTMLText, '&#217;', 'Ù')
    SET @HTMLText = REPLACE(@HTMLText, '&#218;', 'Ú')
    SET @HTMLText = REPLACE(@HTMLText, '&#221;', 'Ý')
    SET @HTMLText = REPLACE(@HTMLText, '&#224;', 'à')
    SET @HTMLText = REPLACE(@HTMLText, '&#225;', 'á')
    SET @HTMLText = REPLACE(@HTMLText, '&#231;', 'ç')
    SET @HTMLText = REPLACE(@HTMLText, '&#232;', 'è')
    SET @HTMLText = REPLACE(@HTMLText, '&#233;', 'é')
    SET @HTMLText = REPLACE(@HTMLText, '&#236;', 'ì')
    SET @HTMLText = REPLACE(@HTMLText, '&#237;', 'í')
    SET @HTMLText = REPLACE(@HTMLText, '&#241;', 'ñ')
    SET @HTMLText = REPLACE(@HTMLText, '&#242;', 'ò')
    SET @HTMLText = REPLACE(@HTMLText, '&#243;', 'ó')
    SET @HTMLText = REPLACE(@HTMLText, '&#249;', 'ù')
    SET @HTMLText = REPLACE(@HTMLText, '&#250;', 'ú')
    SET @HTMLText = REPLACE(@HTMLText, '&#253;', 'ý')
    -- Localización de <BODY y Eliminación de TAGS HTML
    SET @Contador = 1
    WHILE @Contador < @LongCadena
    BEGIN
        If Substring(@HTMLText, @Contador, 1) = '<'
            SET @EnTag = 1
        If @EnTag = 1 AND Substring(@HTMLText, @Contador, 1) = '>'
        BEGIN
            SET @EnTag = 0
            SET @Contador = @Contador + 1
            If Substring(@HTMLText, @Contador, 1) = '<'
                SET @EnTag = 1
        END
        If @Contador > 5
        BEGIN
            If @EnTag = 1 AND UPPER(Substring(@HTMLText, @Contador - 5, 5)) = '<BODY'
                SET @Res = ''
        END
        IF @EnTag = 0
            SET @Res = @Res + Substring(@HTMLText, @Contador, 1)
        ELSE
            SET @TeHTML = 1

        SET @Contador = @Contador + 1
    END
    If @TeHTML = 1
    BEGIN
        SET @EnTag = 0
        WHILE @EnTag = 0
        BEGIN
            If Len(@Res) >= 2
            BEGIN
                If Substring(@Res, 1, 2) = CHAR(13) + CHAR(10)
                    SET @Res = RTrim(LTRIM(Substring(@Res, 3, Len(@Res))))
                Else
                    SET @EnTag = 1
            END       
            Else
                SET @EnTag = 1
        END
        Return @Res
    END
    Else
        Return @HTMLText

    Return ''
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función utilitaria que limpia y convierte texto en formato HTML a texto plano legible. Reemplaza etiquetas HTML comunes (como saltos de línea <br>), entidades HTML especiales (tildes, eñes, caracteres especiales) por sus equivalentes de texto, y elimina todas las etiquetas HTML restantes. Se usa principalmente para mostrar o procesar notas clínicas, evoluciones médicas u observaciones de historia clínica que fueron ingresadas con formato HTML, convirtiéndolas a texto limpio para reportes, impresión o búsqueda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'udf_StripHTML2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'udf_StripHTML2';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Limpia una cadena HTML quitando etiquetas y convirtiendo entidades HTML comunes (acentos, símbolos) a texto plano legible.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'udf_StripHTML2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La entrada puede ser NULL o cadena vacía; si la longitud es 0 se retorna sin transformar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'udf_StripHTML2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las entidades HTML reconocidas (named y numéricas para acentos latinos, ñ, ç, €, ª, º, &nbsp;) se traducen a su carácter Unicode equivalente antes del stripping.; La etiqueta <br> se sustituye por salto de línea CRLF (CHAR(13)+CHAR(10)).; Todo contenido entre ''<'' y ''>'' se descarta del resultado final.; Si existe una sección anterior a <BODY>, se descarta totalmente del resultado.; Cuando hubo tags, el resultado final no comienza con CRLF ni con espacios en blanco.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'udf_StripHTML2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Si la longitud de entrada es 0, retorna la entrada tal cual sin procesar.; [RETURN_RESULT] N/A: Si se detectó al menos una etiqueta HTML (@TeHTML=1), retorna el texto sin tags y recortando saltos de línea iniciales (CHAR(13)+CHAR(10)) más LTRIM/RTRIM.; [RETURN_RESULT] N/A: Si no se detectó ninguna etiqueta HTML durante el recorrido, retorna el texto original (con las entidades ya reemplazadas no aplicadas porque se devuelve @HTMLText tras los REPLACE iniciales).; [RETURN_RESULT] N/A: Cuando se encuentra ''<BODY'' dentro de una etiqueta, el resultado acumulado se reinicia a vacío, descartando todo lo previo a <BODY.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'udf_StripHTML2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Longitud de la cadena de entrada = 0 → Retorna la cadena tal cual sin aplicar transformaciones else Aplica reemplazos de entidades y recorre carácter a carácter para eliminar tags; si Carácter actual = ''<'' → Marca estado ''dentro de tag'' (@EnTag=1) y deja de copiar caracteres al resultado; si Dentro de tag y carácter actual = ''>'' → Cierra el tag (@EnTag=0) y avanza; si el siguiente carácter es ''<'' reabre estado de tag; si Se detecta la subcadena ''<BODY'' (case-insensitive) dentro de una etiqueta → Reinicia el resultado acumulado a cadena vacía (descarta cabecera HTML previa); si Se detectó al menos una etiqueta HTML durante el recorrido (@TeHTML=1) → Elimina saltos de línea iniciales repetidos y aplica LTRIM/RTRIM antes de retornar else Retorna el texto original sin recorte adicional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'udf_StripHTML2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'udf_StripHTML2';
GO
