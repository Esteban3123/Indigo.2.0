
CREATE FUNCTION [dbo].[SexoR256] (@Sexo as int)
RETURNS varchar (9)
AS
BEGIN

declare @grupo varchar(9)
SET @grupo=
     CASE WHEN @Sexo=1 THEN 'H' 
          WHEN @Sexo=2 THEN 'M' 
	 END 
RETURN @grupo
END