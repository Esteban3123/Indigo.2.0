
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_ListarTriage]
(
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT 'Numero Reporte: ' + TRIANUMER + ' - ' + RTRIM(CAST(TRIAGECLA AS CHAR)) + ' - ' + CASE CAST(TRIAGECLA AS CHAR) WHEN '1' THEN 'EMERGENCIA' WHEN '2' THEN 'URGENCIA MEDICA' WHEN '3' THEN 'URGENCIA DIFERIDA' WHEN '4' THEN 'NO URGENTE' END AS 'NIVEL CALIFICACION TRIAGE'

FROM ADTRIAGEU WITH(NOLOCK)
WHERE NUMINGRES=@NumeroIngreso 

END