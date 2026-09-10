

CREATE FUNCTION [dbo].[SplitString_QA] ( @stringToSplit VARCHAR(MAX), @delimiter CHAR(1) )
RETURNS
@returnList TABLE ([Value] [nvarchar] (1000))
AS
BEGIN
    DECLARE @xml XML
    SET @xml = CAST(('<X>' + REPLACE(@stringToSplit, @delimiter, '</X><X>') + '</X>') AS XML)

    INSERT INTO @returnList ([Value])
    SELECT N.value('.', 'NVARCHAR(1000)') AS [Value]
    FROM @xml.nodes('X') AS T(N)
    
    RETURN
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función de utilidad que divide una cadena de texto en múltiples filas usando un delimitador de un carácter. Convierte la cadena en XML para extraer cada segmento como registro individual en una tabla retornada. Es una función auxiliar genérica de tipo *string splitter*, comúnmente usada en consultas que procesan listas de valores separados por comas u otros delimitadores.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SplitString_QA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SplitString_QA';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Divide una cadena de texto en múltiples filas utilizando un delimitador de un carácter, devolviendo cada segmento como una fila independiente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SplitString_QA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cadena de entrada no debe contener caracteres que rompan el parseo XML (ej. ''<'', ''>'', ''&'') sin escape; El delimitador debe ser exactamente un carácter', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SplitString_QA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada segmento devuelto está limitado a 1000 caracteres (NVARCHAR(1000)); El número de filas resultantes es igual al número de delimitadores en la cadena más uno; La transformación se realiza vía conversión a XML usando nodos <X>', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SplitString_QA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @returnList: Por cada nodo <X> generado al reemplazar el delimitador en la cadena, se inserta una fila con el valor del segmento truncado/convertido a NVARCHAR(1000)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SplitString_QA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SplitString_QA';
GO
