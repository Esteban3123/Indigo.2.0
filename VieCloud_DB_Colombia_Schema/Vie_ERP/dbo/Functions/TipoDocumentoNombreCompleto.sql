
CREATE FUNCTION [dbo].[TipoDocumentoNombreCompleto] (@CodigoDocumento as int)
RETURNS varchar (100)
AS
BEGIN

declare @TipoDocumento varchar(100)
	
 SET @TipoDocumento  = (SELECT top 1 NOMBRE  FROM ADTIPOIDENTIFICA WHERE CODIGO = @CodigoDocumento)
	/*
	CASE @CodigoDocumento 
		WHEN 1 THEN 'CC' 
		WHEN 2 THEN 'CE' 
		WHEN 3 THEN 'TI' 
		WHEN 4 THEN 'RC' 
		WHEN 5 THEN 'PA' 
		WHEN 6 THEN 'AS' 
		WHEN 7 THEN 'MS' 
		WHEN 8 THEN 'NU' 
		WHEN 9 THEN 'NV' 
		WHEN 10 THEN 'CD' 
		WHEN 11 THEN 'SC' 
		WHEN 12 THEN 'PE' 
		WHEN 13 THEN 'PT'
		WHEN 14 THEN 'DE'
		WHEN 15 THEN 'SI'
	
	*/

RETURN @TipoDocumento

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que recibe el código numérico interno de un tipo de documento de identificación y devuelve su nombre completo en texto (por ejemplo: ''Cédula de Ciudadanía'', ''Pasaporte'', ''Tarjeta de Identidad''). Consulta el catálogo de tipos de identificación ADTIPOIDENTIFICA para traducir el código al nombre legible. Se usa para mostrar el tipo de documento del paciente o cualquier persona en formularios, reportes y pantallas del sistema, evitando exponer solo el código numérico interno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TipoDocumentoNombreCompleto';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TipoDocumentoNombreCompleto';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el nombre descriptivo del tipo de documento de identificación a partir de su código.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoDocumentoNombreCompleto';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código recibido debe existir en la tabla de tipos de identificación; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoDocumentoNombreCompleto';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna a lo más un único valor (TOP 1).; El valor retornado está limitado a 100 caracteres.; No modifica datos; es una función de solo lectura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoDocumentoNombreCompleto';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo de documento de identificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoDocumentoNombreCompleto';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ADTIPOIDENTIFICA: Retorna el campo NOMBRE del primer registro cuyo CODIGO coincida con el código recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoDocumentoNombreCompleto';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADTIPOIDENTIFICA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoDocumentoNombreCompleto';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoDocumentoNombreCompleto';
GO
