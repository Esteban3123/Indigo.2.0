-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,27-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila430]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON; 
  			
			SELECT
				CASE MUESTRA WHEN '1' THEN 'Sangre Total' WHEN '2' THEN 'Tejido' WHEN '3' THEN 'Linfa' END AS 'MUESTRA',
				CASE PRUEBA  WHEN '1' THEN 'Estudio Directo' WHEN '2' THEN 'Titulo IFI' WHEN '3' THEN 'Aspirado Bazo' WHEN '4' THEN 'Aspirado Medula' WHEN '5' THEN'Prueba Montenegro' END AS 'PRUEBA',
				CASE AGENTE  WHEN '1' THEN 'Leishmania' END AS 'AGENTE',
				CASE RESULTADO  WHEN '1' THEN 'Positivo' WHEN '2' THEN 'Negativo' WHEN '3' THEN 'Compatible' WHEN '4' THEN 'No Compatible' END AS 'RESULTADO',
				CASE MUCOSA WHEN 1 THEN 'X' END AS 'Nasal',CASE MUCOSA WHEN 2 THEN 'X' END AS 'Cavidad Oral', CASE MUCOSA WHEN 3 THEN 'X' END AS 'Labios',
				CASE MUCOSA WHEN 4 THEN 'X' END AS 'Faringe',CASE MUCOSA WHEN 5 THEN 'X' END AS 'Laringe', CASE MUCOSA WHEN 6 THEN 'X' END AS 'Parpados',
				CASE MUCOSA WHEN 7 THEN 'X' END AS 'Genitales',
				convert(varchar(10),FECHATOMA,103) As 'FECHATOMA', convert(varchar(10),FECHARECE,103) As 'FECHARECE', convert(varchar(10),FECHARESUL,103) As 'FECHARESUL',
				Rtrim(PESOACT) As 'PESOACT', Rtrim(OTROCUAL) As 'OTROCUAL', Rtrim(NUMCAPS) As 'NUMCAPS', Rtrim(DIASTRAT) As 'DIASTRAT', Rtrim(TOTACAPS) As 'TOTACAPS', Rtrim(VALOR) As 'VALOR',
				CASE RECITRAT  WHEN '1' THEN 'X' END AS 'RECITRAT si',CASE RECITRAT WHEN '0' THEN 'X' END AS 'RECITRAT no',
				CASE TRATLOCAL  WHEN '1' THEN 'X' END AS 'Crioterapia',CASE TRATLOCAL WHEN '0' THEN 'X' END AS 'Termoterapia',
				CASE MEDIFORM WHEN 1 THEN 'X' END AS 'N-Metil',CASE MEDIFORM WHEN 2 THEN 'X' END AS 'Estibogluco', CASE MEDIFORM WHEN 3 THEN 'X' END AS 'Isotianato',
				CASE MEDIFORM WHEN 4 THEN 'X' END AS 'Anfotericina',CASE MEDIFORM WHEN 5 THEN 'X' END AS 'Otro', CASE MEDIFORM WHEN 6 THEN 'X' END AS 'Miltefosina',
				CASE MEDIFORM WHEN 7 THEN 'X' END AS 'Pentamidina',CASE MEDIFORM WHEN 8 THEN 'X' END AS 'SinTratamiento',
				CASE RINORREA WHEN '1' THEN 'X' END AS 'RINORREA',CASE EPISTAXIS WHEN '1' THEN 'X' END AS 'EPISTAXIS', CASE OBSTRNASAL WHEN '1' THEN 'X' END AS 'OBSTRNASAL',
				CASE DISFONIA WHEN '1' THEN 'X' END AS 'DISFONIA', CASE DISFAGIA WHEN '1' THEN 'X' END AS 'DISFAGIA', CASE HIPEREMIA WHEN '1' THEN 'X' END AS 'HIPEREMIA',
				CASE ULCERACION WHEN '1' THEN 'X' END AS 'ULCERACION',CASE PERFORACION WHEN '1' THEN 'X' END AS 'PERFORACION', CASE DESTRUCCION WHEN '1' THEN 'X' END AS 'DESTRUCCION',
				VERSION AS 'VERSION', 
				JSON AS 'JSON'

			FROM 
				HCFICHA430 
	
			WHERE
				IDFICHANOTIFICACION  = @IdFicha

END