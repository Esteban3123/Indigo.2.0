
CREATE FUNCTION [dbo].[TipoPaciente] (@CodigoTipoPaciente as int)
RETURNS nvarchar (60)
AS
BEGIN

declare @TipoPaciente nvarchar (60)

SELECT @TipoPaciente = CASE @CodigoTipoPaciente WHEN 1 THEN 'CONTRIBUTIVO' WHEN 2 THEN 'SUBSIDIADO' WHEN 3 THEN 'No afiliado' WHEN 4 THEN 'PARTICULAR' WHEN 5 THEN 'OTRO' WHEN 6 THEN 'DESPLAZADO REG. CONTRIBUTIVO' WHEN 7 THEN 'DESPLAZADO REG. SUBSIDIADO' WHEN 8 THEN 'DESPLAZADO NO ASEGURADO'
WHEN 9 THEN 'Especial o Excepción' WHEN 10 THEN 'Personas privadas de la libertad a cargo del Fondo Nacional de Salud' WHEN 11 THEN 'Tomador/ Amparado ARL' WHEN 12 THEN 'Tomador/ Amparado SOAT' WHEN 13 THEN 'Tomador/ Amparado Planes voluntarios de salud' END

RETURN @TipoPaciente

END