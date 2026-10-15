


CREATE FUNCTION [dbo].[TipDocR256] (@Tipdoc as int)
RETURNS varchar (20)
AS
BEGIN

declare @grupo varchar(20)
SET @grupo=
     CASE WHEN @Tipdoc=1 THEN 'CC' 
          WHEN @Tipdoc=2 THEN 'CE' 
		  WHEN @Tipdoc=3 THEN 'TI'
		  WHEN @Tipdoc=4 THEN 'RC'
		  WHEN @Tipdoc=5 THEN 'PA'
		  WHEN @Tipdoc=8 THEN 'NUIP'
	 END 
RETURN @grupo
END