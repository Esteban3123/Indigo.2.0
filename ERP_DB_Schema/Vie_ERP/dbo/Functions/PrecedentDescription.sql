


CREATE FUNCTION [dbo].[PrecedentDescription] (@CodigoAntecedente as int)
RETURNS nvarchar (50)
AS
BEGIN

declare @DescripcionAntecedente nvarchar(50)

SELECT @DescripcionAntecedente = CASE @CodigoAntecedente WHEN 1 THEN 'Médicos' WHEN 2 THEN 'Quirúrgico' WHEN 3 THEN 'Anestésico' WHEN 4 THEN 'Transfusionales' WHEN 5 THEN 'Inmunológicos' WHEN 6 THEN 'Alérgicos' WHEN 7 THEN 'Traumáticos' WHEN 8 THEN 'Psicológicos' WHEN 9 THEN 'Farmacológicos'  WHEN 10 THEN 'Familiares' WHEN 11 THEN 'Ginecológicos / obstétricos' WHEN 12 THEN 'Urología sexual' WHEN 13 THEN 'Perinatales' WHEN 14 THEN 'Tóxicos' WHEN 15 THEN 'Hábitos de vida' WHEN 16 THEN 'Esquema de vacunación' WHEN 17 THEN 'Escolares' WHEN 18 THEN 'Laborales' WHEN 19 THEN 'Nutricionales' WHEN 20 THEN 'Odontológicos' WHEN 21 THEN 'Socioeconómicos' WHEN 22 THEN 'Otros' WHEN 23 THEN 'Oftalmológicos' END


RETURN @DescripcionAntecedente

END