CREATE FUNCTION [dbo].[CompleteProfessionalDocumentType] (@IdDocumento as int)
RETURNS varchar (100)
AS
BEGIN

declare @TipoDocumento varchar(100)
	
 SET @TipoDocumento  = (SELECT top 1 NOMBRE  FROM ADTIPOIDENTIFICA WHERE ID = @IdDocumento)

RETURN @TipoDocumento

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que recibe el identificador numérico de un tipo de documento y devuelve su nombre descriptivo completo (por ejemplo, ''Cédula de Ciudadanía'', ''Pasaporte'', ''Tarjeta de Identidad''). Consulta el catálogo de tipos de identificación ADTIPOIDENTIFICA para traducir un código interno al texto legible del tipo de documento. Se usa principalmente para mostrar el tipo de documento del profesional de salud en reportes, interfaces y documentos clínicos donde se necesita el nombre completo en lugar del código numérico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'CompleteProfessionalDocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'CompleteProfessionalDocumentType';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el nombre descriptivo del tipo de documento de identificación a partir de su identificador en el catálogo de tipos de identificación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CompleteProfessionalDocumentType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el catálogo de tipos de identificación con el ID solicitado para obtener un nombre; en caso contrario el resultado será NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CompleteProfessionalDocumentType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Devuelve a lo sumo un nombre de tipo de identificación (TOP 1).; Si no existe el identificador en el catálogo, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CompleteProfessionalDocumentType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo de identificación; Documento profesional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CompleteProfessionalDocumentType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADTIPOIDENTIFICA: Cuando se encuentra un registro en ADTIPOIDENTIFICA con ID igual al parámetro, retorna su NOMBRE (TOP 1); si no existe coincidencia, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CompleteProfessionalDocumentType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADTIPOIDENTIFICA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CompleteProfessionalDocumentType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CompleteProfessionalDocumentType';
GO
