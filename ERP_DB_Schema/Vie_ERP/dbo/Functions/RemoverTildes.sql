create
 
FUNCTION [dbo].[RemoverTildes] (@Cadena VARCHAR (100))
RETURNS VARCHAR (100)
AS
 
BEGIN
 
 
--Reemplazamos las vocales acentuadas
    
RETURN
 
REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(@Cadena,'á','A'),'é','E'),'í','I'),'ó','O'),'ú','U')
 
END