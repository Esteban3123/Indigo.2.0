-- Stored Procedure



-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,27-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila420]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;

Select
	CASE MUESTRA WHEN '1' THEN 'Sangre Total' WHEN '2' THEN 'Tejido' WHEN '3' THEN 'Linfa' END AS 'MUESTRA',
	CASE PRUEBA  WHEN '1' THEN 'Estudio Directo' WHEN '2' THEN 'Aspirado Bazo' WHEN '3' THEN 'Aspirado Medula' WHEN '4' THEN'Prueba Montenegro' WHEN '5' THEN 'Biopsia' END AS 'PRUEBA',
	CASE AGENTE  WHEN '1' THEN 'Leishmania' END AS 'AGENTE',
	CASE RESULTADO  WHEN '1' THEN 'Positivo' WHEN '2' THEN 'Negativo' WHEN '3' THEN 'Compatible' WHEN '4' THEN 'No Compatible' END AS 'RESULTADO',
	convert(varchar(10),FECHATOMA,103) As 'FECHATOMA', convert(varchar(10),FECHARECE,103) As 'FECHARECE', convert(varchar(10),FECHARESUL,103) As 'FECHARESUL',
	Rtrim(PESOACT) As 'PESOACT', Rtrim(OTROCUAL) As 'OTROCUAL', Rtrim(NUMCAPS) As 'NUMCAPS', Rtrim(DIASTRAT) As 'DIASTRAT', Rtrim(TOTACAPS) As 'TOTACAPS', Rtrim(VALOR) As 'VALOR',
	CASE RECITRAT  WHEN '1' THEN 'X' END AS 'RECITRAT si',CASE RECITRAT WHEN '0' THEN 'X' END AS 'RECITRAT no',
	CASE TRATLOCAL  WHEN '1' THEN 'X' END AS 'Crioterapia',CASE TRATLOCAL WHEN '0' THEN 'X' END AS 'Termoterapia',
	CASE MEDIFORM WHEN '1' THEN 'X' END AS 'N-Metil',CASE MEDIFORM WHEN '2' THEN 'X' END AS 'Estibogluco', CASE MEDIFORM WHEN '3' THEN 'X' END AS 'Isotianato',
	CASE MEDIFORM WHEN '4' THEN 'X' END AS 'Anfotericina',CASE MEDIFORM WHEN '5' THEN 'X' END AS 'Otro', CASE MEDIFORM WHEN '6' THEN 'X' END AS 'Miltefosina',
	CASE MEDIFORM WHEN '7' THEN 'X' END AS 'Pentamidina',CASE MEDIFORM WHEN '8' THEN 'X' END AS 'SinTratamiento',
	CASE CARA WHEN '1' THEN 'X' END AS 'CARA',CASE TRONCO WHEN '1' THEN 'X' END AS 'TRONCO', CASE MIEMSUP WHEN '1' THEN 'X' END AS 'MIEMSUP',
	CASE MIEMINF WHEN '1' THEN 'X' END AS 'MIEMINF',
	VERSION AS 'VERSION', JSON AS 'JSON'
	From HCFICHA420 where IDFICHANOTIFICACION  = @IdFicha

END

--select * from hcfichanotificacion where id = '8'
--select * from HCFICHA420