
CREATE FUNCTION [dbo].[EtariosR] (@Fnacimiento as datetime, @Fatencion as datetime)
RETURNS varchar (13)
AS
BEGIN

declare @grupo varchar(13)
SET @grupo=
     CASE WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 0  AND (datediff(YY, @Fnacimiento, @Fatencion))<= 4)  THEN '0 a 4 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 5  AND (datediff(YY, @Fnacimiento, @Fatencion))<= 9)  THEN '5 a 9 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 10 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 14) THEN '10 a 14 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 15 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 19) THEN '15 a 19 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 20 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 24) THEN '20 a 24 años'
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 25 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 29) THEN '25 a 29 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 30 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 34) THEN '30 a 34 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 35 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 39) THEN '35 a 39 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 40 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 44) THEN '40 a 44 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 45 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 49) THEN '45 a 49 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 50 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 54) THEN '50 a 54 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 55 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 59) THEN '55 a 59 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 60 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 64) THEN '60 a 64 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 65 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 69) THEN '65 a 69 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 70 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 74) THEN '70 a 74 años' 
          WHEN ((datediff(YY,@Fnacimiento, @Fatencion))>= 75 AND (datediff(YY, @Fnacimiento, @Fatencion))<= 79) THEN '75 a 79 años' 
          WHEN (datediff(YY, @Fnacimiento, @Fatencion))>= 80 THEN '80 y más años' 
     END 
RETURN @grupo
END