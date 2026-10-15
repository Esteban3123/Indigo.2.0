
CREATE FUNCTION [dbo].[AG_Tipo] (@Tipo as int)
RETURNS varchar (15)
AS
BEGIN

declare @grupo varchar(15)
SET @grupo=
     CASE WHEN @Tipo=0 THEN 'Primera Vez' 
          WHEN @Tipo=1 THEN 'Control' 
		  WHEN @Tipo=2 THEN 'Pos Operatorio'
	 END 
RETURN @grupo
END