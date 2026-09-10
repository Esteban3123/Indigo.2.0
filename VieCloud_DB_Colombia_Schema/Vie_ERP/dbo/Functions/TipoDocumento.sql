

CREATE FUNCTION [dbo].[TipoDocumento] (@CodigoDocumento as int)
RETURNS varchar (2)
AS
BEGIN

declare @TipoDocumento varchar(2)
	
 SET @TipoDocumento  = (SELECT top 1 SIGLA FROM ADTIPOIDENTIFICA WHERE CODIGO = @CodigoDocumento)
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que recibe el código numérico interno de un tipo de documento de identificación y devuelve su sigla abreviada de dos letras (por ejemplo: CC para Cédula de Ciudadanía, CE para Cédula de Extranjería, TI para Tarjeta de Identidad, PA para Pasaporte, entre otros). Consulta el catálogo de tipos de identificación ADTIPOIDENTIFICA para obtener la sigla correspondiente al código recibido. Se utiliza en reportes, generación de RIPS y procesos que requieren mostrar el tipo de documento del paciente en formato estándar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TipoDocumento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TipoDocumento';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de tipo de identificación a su sigla estándar (ej. CC, CE, TI) consultando el catálogo de tipos de identificación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoDocumento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código recibido debe existir en el catálogo de tipos de identificación; de lo contrario el resultado será NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoDocumento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La sigla retornada está limitada a 2 caracteres; Se retorna a lo sumo una sigla (TOP 1) aunque existan múltiples coincidencias; Si no hay coincidencia en el catálogo, el resultado es NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoDocumento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo de documento de identificación; Catálogo de identificaciones', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoDocumento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultado escalar): Devuelve la SIGLA del primer registro de ADTIPOIDENTIFICA cuyo CODIGO coincide con el código recibido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoDocumento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADTIPOIDENTIFICA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoDocumento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoDocumento';
GO
