
CREATE FUNCTION [dbo].[JornadaMapaQuirurgico] (@HoraTabla AS NVARCHAR(5)
,@HoraInicialManana AS NVARCHAR(5),@HoraFinalManana AS NVARCHAR(5)
,@HoraInicialTarde AS NVARCHAR(5),@HoraFinalTarde AS NVARCHAR(5)
,@HoraInicialNoche AS NVARCHAR(5),@HoraFinalNoche AS NVARCHAR(5))
RETURNS nvarchar (25)
AS
BEGIN

declare @Jornada nvarchar(25)

IF @HoraTabla BETWEEN @HoraInicialManana AND @HoraFinalManana
	SET @Jornada ='1. Mañana'
ELSE IF @HoraTabla BETWEEN @HoraInicialTarde AND @HoraFinalTarde 
	SET @Jornada ='2. Tarde'
ELSE IF @HoraTabla BETWEEN @HoraInicialNoche AND @HoraFinalNoche
	SET @Jornada ='3. Noche'
ELSE
	 SET @Jornada ='Jornada no estipulada'
   
RETURN @Jornada

END