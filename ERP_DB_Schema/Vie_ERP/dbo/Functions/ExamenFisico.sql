

CREATE FUNCTION [dbo].[ExamenFisico] (@Cedula varchar(25))
RETURNS datetime
AS
BEGIN

declare 
@fechamax as datetime

SELECT  top 1 @fechamax =max(FECREGITE)
FROM         HCEXFISIC
WHERE IPCODPACI = @Cedula 
group by FECREGITE


RETURN @fechamax

END