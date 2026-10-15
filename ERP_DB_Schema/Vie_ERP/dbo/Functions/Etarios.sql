CREATE FUNCTION [dbo].[Etarios] (@Fnacimiento as datetime, @Fatencion as datetime)
RETURNS varchar (18)
AS
BEGIN

declare @grupo varchar(18)
SET @grupo=
     CASE WHEN (datediff(YY,@Fnacimiento, @Fatencion))<=1 THEN 'Menores de 1 año' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>1  AND (datediff(YY, @Fnacimiento, @Fatencion))<= 4)  THEN 'Entre 1 y 4 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 5 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 14) THEN 'Entre 5 y 14 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 15 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 44) THEN 'Entre 15 y 44 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 45 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 59) THEN 'Entre 45 y 59 años' 
          WHEN (datediff(YY, @Fnacimiento, @Fatencion))>= 60 THEN '60 y más años' 
     END 
RETURN @grupo
END