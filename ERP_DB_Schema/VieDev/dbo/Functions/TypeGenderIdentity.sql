
CREATE FUNCTION [dbo].[TypeGenderIdentity] (@IdTipoIdentidadGenero AS int)
RETURNS varchar(50)
AS
BEGIN

DECLARE @NombreIdentidadGenero varchar(50) 	
 SET @NombreIdentidadGenero  = (SELECT TOP 1 Name FROM Admissions.GenderTypes WHERE Id = @IdTipoIdentidadGenero)	
RETURN @NombreIdentidadGenero

END