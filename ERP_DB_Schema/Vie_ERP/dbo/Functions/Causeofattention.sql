CREATE FUNCTION [dbo].[Causeofattention] (@Code as int)
RETURNS varchar (300)
AS
BEGIN


declare @Causeofattention varchar(300)
	
 SET @Causeofattention  = (SELECT Name FROM Causesofattention WHERE Code = @Code)
/*
Función que me va a listar la causa de atención.
*/

RETURN @Causeofattention

END