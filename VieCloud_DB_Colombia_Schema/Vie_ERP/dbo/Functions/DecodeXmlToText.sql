
-- =============================================
-- Author:		Juan F. Tamayo
-- Create date: 2017-02-10
-- Description:	Convierte los caracteres prohibidos
-- en el formato Xml a su correspondiente en texto normal
-- =============================================
create FUNCTION [dbo].[DecodeXmlToText] 
(
	@text varchar(max)
)
RETURNS varchar(max)
AS
BEGIN
	return replace(replace(replace(replace(replace(@text, '&quot;', '"'), '&apos;', ''''), '&amp;', '&'), '&lt;', '<'), '&gt;', '>')
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de utilidad que convierte texto con codificación XML especial a texto legible normal, reemplazando las entidades XML escapadas (&quot;, &apos;, &amp;, &lt;, &gt;) por sus caracteres equivalentes (comillas, apóstrofos, ampersand, mayor y menor que). Se usa para decodificar contenido almacenado o transmitido en formato XML antes de mostrarlo en reportes, documentos clínicos o cualquier salida de texto del sistema. Es una función de soporte transversal que puede aplicarse a datos de historia clínica, notas médicas, diagnósticos o cualquier campo de texto libre que haya pasado por serialización XML.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DecodeXmlToText';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DecodeXmlToText';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Decodifica entidades XML estándar (&quot;, &apos;, &amp;, &lt;, &gt;) devolviendo su carácter de texto equivalente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DecodeXmlToText';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Reemplaza &quot; por comilla doble; Reemplaza &apos; por apóstrofo simple; Reemplaza &amp; por &; Reemplaza &lt; por <; Reemplaza &gt; por >; No modifica caracteres distintos a las cinco entidades XML soportadas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DecodeXmlToText';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DecodeXmlToText';
GO
