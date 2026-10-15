
CREATE FUNCTION [dbo].[EstadoFolio] (@NUMINGRES as nvarchar(20),@UFUCODIGO as nvarchar(20))
RETURNS nvarchar (60)
AS
BEGIN

declare @INDICAPAC nvarchar(60); 
declare @NUMEFOLIO nvarchar(60); 


select @INDICAPAC=CASE INDICAPAC
WHEN 1 THEN 'Trasladar a Urgencias'
WHEN 2 THEN 'Trasladar a Observacion Urgencias'
WHEN 3 THEN 'Trasladar a Hospitalizacion'
WHEN 4 THEN 'Trasladar a  UCI Adulto'
WHEN 5 THEN 'Trasladar a UCI Pediatrica'
WHEN 6 THEN 'Trasladar a UCI Neonatal'
WHEN 7 THEN 'Trasladar a Consulta Externa'
WHEN 8 THEN 'Trasladar a  Cirugia'
WHEN 9 THEN 'Hospitalizacion en Casa'
WHEN 10 THEN 'Referencia'
WHEN 11 THEN 'Morgue'
WHEN 12 THEN 'Salida'
WHEN 13 THEN 'Continua en la Unidad'
WHEN 15 THEN 'Retiro Voluntario'
WHEN 16 THEN 'Fuga' END , @NUMEFOLIO=NUMEFOLIO
from .HCHISPACA AS A INNER JOIN
(SELECT UFUCODIGO, NUMINGRES,max(fechispac) AS FECHA
FROM .HCHISPACA
where INDICAPAC in ('3','9') and NUMINGRES =@NUMINGRES and UFUCODIGO= @UFUCODIGO
group by UFUCODIGO,NUMINGRES, UFUCODIGO) as B ON B.NUMINGRES = A.NUMINGRES AND B.UFUCODIGO = A.UFUCODIGO AND B.FECHA = A.FECHISPAC


RETURN @INDICAPAC +''+ @NUMEFOLIO

END