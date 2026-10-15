

CREATE FUNCTION [dbo].[DestinoFolio] (@Paciente as varchar(25))
RETURNS nvarchar (80)
AS
BEGIN

declare @DescipcionDestino nvarchar(80)

SELECT TOP 1 @DescipcionDestino= ' Destino: ' + CASE INDICAPAC WHEN '1' THEN 'Trasladar a Urgencias' WHEN '2'
THEN 'Trasladar a Observacion Urgencias' WHEN '3' THEN 'Trasladar a Hospitalizacion' WHEN '4' THEN 'Trasladar a  UCI Adulto'
WHEN '5' THEN 'Trasladar a UCI Pediatrica' WHEN '6' THEN 'Trasladar a UCI Neonatal' WHEN '7' THEN 'Trasladar a Consulta Externa'
WHEN '8' THEN 'Trasladar a  Cirugia' WHEN '9' THEN 'Hospitalizacion en Casa' WHEN '10' THEN 'Referencia' WHEN '11' THEN 'Morgue'
WHEN '12' THEN 'Salida' WHEN '13' THEN 'Continua en la Unidad' END  FROM HCHISPACA WHERE IPCODPACI = @Paciente
ORDER BY FECHISPAC DESC

RETURN @DescipcionDestino

END