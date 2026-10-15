CREATE FUNCTION [dbo].[CompleteProfessionalDocumentType] (@IdDocumento as int)
RETURNS varchar (100)
AS
BEGIN


declare @TipoDocumento varchar(100)
	
 SET @TipoDocumento  = (SELECT top 1 NOMBRE  FROM ADTIPOIDENTIFICA WHERE ID = @IdDocumento)

RETURN @TipoDocumento

END