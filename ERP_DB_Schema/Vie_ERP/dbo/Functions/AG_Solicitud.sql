
CREATE FUNCTION [dbo].[AG_Solicitud] (@Solicitud as int)
RETURNS varchar (12)
AS
BEGIN

declare @grupo varchar(12)
SET @grupo=
     CASE WHEN @Solicitud=0 THEN 'Presencial' 
          WHEN @Solicitud=1 THEN 'Telefónica' 
	 END 
RETURN @grupo
END