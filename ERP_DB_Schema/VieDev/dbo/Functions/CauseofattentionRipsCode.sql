CREATE FUNCTION [dbo].[CauseofattentionRipsCode] (@Code as int)
RETURNS varchar (3)
AS
BEGIN


declare @CauseofattentionRipsCode varchar(3)
	
 SET @CauseofattentionRipsCode  = (SELECT RIPSCode FROM Causesofattention WHERE Code = @Code)
/*
Función que me va a retornar el código RIPS.
*/

RETURN @CauseofattentionRipsCode

END