CREATE FUNCTION [dbo].[udf_StripHTML3] (@HTMLText VARCHAR(MAX))
RETURNS VARCHAR(MAX) AS
BEGIN
    DECLARE @Contador INT
    DECLARE @LongCadena INT
    DECLARE @EnTag INT
    DECLARE @Res VARCHAR(MAX)
    DECLARE @TeHTML INT

   DECLARE @Start INT  
DECLARE @End INT  
DECLARE @Length INT  
SET @Start = CHARINDEX('<BODY>',@HTMLText)  
SET @End = CHARINDEX('</BODY>',@HTMLText,CHARINDEX('<',@HTMLText))  
SET @Length = (@End - @Start) + 1  
SET  @HTMLText = SUBSTRING ( @HTMLText ,@Start , @End )
return @HTMLText

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función utilitaria que extrae el contenido del cuerpo de un texto HTML, identificando y devolviendo únicamente el fragmento comprendido entre las etiquetas <BODY> y </BODY>. Se utiliza para limpiar o aislar el contenido relevante de textos clínicos, notas médicas o documentos del EHR que llegan en formato HTML. Permite procesar campos de texto libre con marcado HTML para obtener solo el texto del cuerpo, facilitando la lectura, impresión o almacenamiento de información clínica sin etiquetas de encabezado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'udf_StripHTML3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'udf_StripHTML3';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Extrae el fragmento de texto comprendido entre las etiquetas <BODY> y </BODY> dentro de una cadena HTML.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'udf_StripHTML3';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cadena de entrada debe contener la etiqueta ''<BODY>'' y ''</BODY>'' para que el resultado sea significativo; en caso contrario CHARINDEX devuelve 0 y SUBSTRING puede retornar cadena vacía o comportamiento inesperado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'udf_StripHTML3';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre arranca en la posición donde aparece ''<BODY>'' dentro del texto original.; No realiza un verdadero stripping de HTML pese a su nombre; sólo delimita por la etiqueta BODY.; No modifica datos persistentes; es función escalar pura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'udf_StripHTML3';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Texto HTML', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'udf_StripHTML3';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Devuelve SUBSTRING(@HTMLText, posición de ''<BODY>'', posición de ''</BODY>'') como valor escalar VARCHAR(MAX).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'udf_StripHTML3';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'udf_StripHTML3';
GO
