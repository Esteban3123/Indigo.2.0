
CREATE FUNCTION [dbo].[AG_Estado] (@Estado as int)
RETURNS varchar (15)
AS
BEGIN

declare @grupo varchar(15)
SET @grupo=
     CASE WHEN @Estado=0 THEN 'Asignada' 
          WHEN @Estado=1 THEN 'Cumplida' 
		  WHEN @Estado=2 THEN 'Incumplida'
		  WHEN @Estado=3 THEN 'Preasignada'
	 END 
RETURN @grupo
END