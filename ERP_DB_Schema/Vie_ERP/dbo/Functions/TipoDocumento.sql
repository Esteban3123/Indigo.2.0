

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