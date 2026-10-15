

CREATE FUNCTION [dbo].[DiferenciaColumna] (  @Cama as nvarchar(10), @Cuenta as nvarchar(10))
RETURNS datetime 
AS
BEGIN

declare @Dias datetime


select  @Dias= b.FECINIEST
    FROM .CHREGESTA AS A INNER JOIN
	  (SELECT CODICAMAS,NUMINGRES,FECINIEST,  ROW_NUMBER() OVER(PARTITION BY CODICAMAS ORDER BY FECREGSIS ASC)-1 AS Cuenta 
	       from .CHREGESTA) AS B ON  @Cama = a.CODICAMAS   
WHERE @Cuenta= b.cuenta 
RETURN @Dias

END