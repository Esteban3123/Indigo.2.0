
CREATE FUNCTION [dbo].[AG_CitaExtra] (@CExtra as int)
RETURNS varchar (10)
AS
BEGIN

declare @grupo varchar(10)
SET @grupo=
     CASE WHEN @CExtra=0 THEN 'Normal' 
          WHEN @CExtra=1 THEN 'Cita Extra' 
	 END 
RETURN @grupo
END