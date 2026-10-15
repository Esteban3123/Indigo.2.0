
CREATE FUNCTION [dbo].[RangoAfiliacion](@CodigoNivelAfiliado as char(2))
RETURNS nvarchar (60)
AS
BEGIN
    DECLARE @CodigoNivel nvarchar (60)

    SELECT @CodigoNivel = CASE @CodigoNivelAfiliado WHEN '01' THEN 'RANGO A 2020' WHEN '02' THEN 'RANGO B 2020' 
	WHEN '03' THEN 'RANGO C 2020' WHEN '04' THEN 'OTRO' WHEN '08' THEN 'RANGO A 2011' ELSE 'NO APLICA' END 

    RETURN @CodigoNivel
END