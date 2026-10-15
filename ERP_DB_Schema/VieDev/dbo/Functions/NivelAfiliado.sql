CREATE FUNCTION [dbo].[NivelAfiliado]
(
@CodigoNivelAfiliado as int
)
RETURNS nvarchar (60)
AS
BEGIN
 
    DECLARE  @CodigoNivel nvarchar (60)
   
    SELECT @CodigoNivel = CASE @CodigoNivel WHEN 01 THEN 'RANGO A 2020' WHEN 02 THEN 'RANGO B 2020' WHEN 03 THEN 'RANGO C 2020' WHEN 04 THEN 'OTRO' WHEN 08 THEN 'RANGO A 2011' END 

    RETURN @CodigoNivel
END