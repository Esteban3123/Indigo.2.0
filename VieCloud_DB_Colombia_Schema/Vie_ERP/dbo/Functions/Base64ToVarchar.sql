
-- =============================================
-- Author:		Juan F. Tamayo Puertas
-- Create date: 2017-01-25
-- Description:	Convierte una cadena de caracteres
--				en base 64 a su correspondiente
-- =============================================
create FUNCTION [dbo].[Base64ToVarchar] 
(
	@text varchar(max)
)
RETURNS varchar(max)
AS
BEGIN
	
	DECLARE @decoded varbinary(max)

	set @decoded = cast('' as xml).value('xs:base64Binary(sql:variable("@text"))', 'varbinary(max)')

	RETURN convert(varchar(max), @decoded)
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de utilidad que convierte una cadena de texto codificada en Base64 a su valor original legible en texto plano. Recibe como parámetro una cadena en formato Base64 y devuelve el texto decodificado. Se usa en el sistema para descifrar información que fue almacenada o transmitida en formato Base64, como datos de documentos, notas clínicas o cualquier contenido textual que haya sido codificado para transporte o almacenamiento seguro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Base64ToVarchar';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Base64ToVarchar';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Decodifica una cadena en Base64 y devuelve su contenido equivalente como texto varchar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Base64ToVarchar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cadena de entrada debe estar codificada en Base64 válido conforme a xs:base64Binary; de lo contrario, la conversión XML fallará.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Base64ToVarchar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La decodificación se realiza interpretando la cadena de entrada como Base64 estándar (xs:base64Binary) y devolviendo su representación textual en varchar(max).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Base64ToVarchar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Convierte el texto Base64 a varbinary mediante xs:base64Binary y luego lo retorna como varchar(max).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Base64ToVarchar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Base64ToVarchar';
GO
