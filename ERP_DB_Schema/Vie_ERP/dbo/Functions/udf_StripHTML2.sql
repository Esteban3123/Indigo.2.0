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