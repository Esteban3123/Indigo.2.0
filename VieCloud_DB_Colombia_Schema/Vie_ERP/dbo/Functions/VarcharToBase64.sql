
-- =============================================
-- Author:		Juan F. Tamayo Puertas
-- Create date: 2017-01-25
-- Description:	Convierte una cadena de caracteres
--				a su correspondiente en base 64
-- =============================================
create FUNCTION [dbo].[VarcharToBase64] 
(
	@text varchar(max)
)
RETURNS varchar(max)
AS
BEGIN
	
	DECLARE @encoded varchar(max), @source varbinary(max)

	set @source = convert(varbinary(max), @text)

	set @encoded = cast('' as xml).value('xs:base64Binary(sql:variable("@source"))', 'varchar(max)')

	RETURN @encoded
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convierte una cadena de texto (varchar) a su representación en Base64. Se usa para codificar información sensible o datos de negocio antes de ser transmitidos o almacenados en formatos que requieren encoding seguro, como integraciones externas, tokens o exportaciones de datos clínicos y administrativos. Recibe cualquier texto como parámetro y devuelve la cadena codificada en Base64 usando conversión binaria interna de SQL Server.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'VarcharToBase64';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'VarcharToBase64';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Convierte una cadena de texto a su representación codificada en Base64.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'VarcharToBase64';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La cadena de entrada se convierte primero a varbinary(max) antes de codificarse, garantizando una representación binaria intermedia.; La codificación Base64 se obtiene mediante el método XML xs:base64Binary, garantizando un formato Base64 estándar.; El resultado es siempre varchar(max) con la representación Base64 de la entrada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'VarcharToBase64';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'VarcharToBase64';
GO
