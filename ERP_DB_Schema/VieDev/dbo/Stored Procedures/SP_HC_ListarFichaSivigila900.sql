
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila900]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
		
		Select
				
				CASE SOSPECHAEVENTO  WHEN '1' THEN 'X' END AS 'Síndrome_mano_pie_boca',
				CASE SOSPECHAEVENTO WHEN '2' THEN 'X' END AS 'Conjuntivitis',
				CASE SOSPECHAEVENTO WHEN '3' THEN 'X' END AS 'Accidente_escorpionico/Picadura',
				CASE SOSPECHAEVENTO WHEN '4' THEN 'X' END AS 'Brucelosis',
				CASE SOSPECHAEVENTO WHEN '5' THEN 'X' END AS 'Hepatitis',
				CASE SOSPECHAEVENTO WHEN '6' THEN 'X' END AS 'Otros',
				OTROSEVENTOS as 'Otros_eventos',
				VERSION AS 'VERSION', 
				JSON AS 'JSON' 
	
			From 
				HCFICHA900 

			Where 
				IDFICHANOTIFICACION  = @IdFicha 

END