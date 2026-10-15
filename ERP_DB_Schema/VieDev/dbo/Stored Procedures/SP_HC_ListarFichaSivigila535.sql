-- Stored Procedure



-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,05-12-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila535]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 

	--Select 	*	From HCFICHA535 where IDFICHANOTIFICACION  = 13

	Select
	CASE USOANTIBIO  WHEN '1' THEN 'X' END AS 'USOANTIBIO si',CASE USOANTIBIO WHEN '0' THEN 'X' END AS 'USOANTIBIO no',
	CASE TIPOAGENTE WHEN '1' THEN 'X' END AS 'Haemophilus',CASE TIPOAGENTE WHEN '2' THEN 'X' END AS 'Neisseria', CASE TIPOAGENTE WHEN '3' THEN 'X' END AS 'Streptococcus',
	CASE TIPOAGENTE WHEN '4' THEN 'X' END AS 'OtrosAgentes',CASE TIPOAGENTE WHEN '5' THEN 'X' END AS 'AgentsinDet',
	CASE VACUANTIHIB WHEN '1' THEN 'X' END AS 'Si1',CASE VACUANTIHIB WHEN '2' THEN 'X' END AS 'No1', CASE VACUANTIHIB WHEN '3' THEN 'X' END AS 'Desconocido1',
	CASE VACUANTIMENINGO WHEN '1' THEN 'X' END AS 'Si2',CASE VACUANTIMENINGO WHEN '2' THEN 'X' END AS 'No2', CASE VACUANTIMENINGO WHEN '3' THEN 'X' END AS 'Desconocido2',
	CASE VACUANTINEUMO WHEN '1' THEN 'X' END AS 'Si3',CASE VACUANTINEUMO WHEN '2' THEN 'X' END AS 'No3', CASE VACUANTINEUMO WHEN '3' THEN 'X' END AS 'Desconocido3',
	CASE DOSIS1 WHEN '1' THEN 'X' END AS 'DOSIS1 1',CASE DOSIS1 WHEN '2' THEN 'X' END AS 'DOSIS1 2', CASE DOSIS1 WHEN '3' THEN 'X' END AS 'DOSIS1 3',
	CASE DOSIS2 WHEN '1' THEN 'X' END AS 'DOSIS2 1',CASE DOSIS2 WHEN '2' THEN 'X' END AS 'DOSIS2 2', CASE DOSIS2 WHEN '3' THEN 'X' END AS 'DOSIS2 3',
	CASE DOSIS3 WHEN '1' THEN 'X' END AS 'DOSIS3 1',CASE DOSIS3 WHEN '2' THEN 'X' END AS 'DOSIS3 2', CASE DOSIS3 WHEN '3' THEN 'X' END AS 'DOSIS3 3',
	convert(varchar(10),FECHAULTIDO1,103) As 'FECHAULTIDO1', convert(varchar(10),FECHAULTIDO2,103) As 'FECHAULTIDO2', convert(varchar(10),FECHAULTIDO3,103) As 'FECHAULTIDO3',
	convert(varchar(10),FECHAULTIDO4,103) As 'FECHAULTIDO4', convert(varchar(10),FECHATOMA,103) As 'FECHATOMA', convert(varchar(10),FECHARECE,103) As 'FECHARECE',
	convert(varchar(10),FECHARESUL,103) As 'FECHARESUL',
	CASE CLASIFCASO WHEN '1' THEN 'X' END AS 'Meningitis',CASE CLASIFCASO WHEN '2' THEN 'X' END AS 'MeningMeningo', CASE CLASIFCASO WHEN '3' THEN 'X' END AS 'MeningosinMeningi',
	CASE MUESTRA WHEN '1' THEN 'Sangre Total' WHEN '2' THEN 'LCR' END AS 'MUESTRA',
	CASE PRUEBA  WHEN '1' THEN 'Aislamiento' WHEN '2' THEN 'Cultivo' WHEN '3' THEN 'Coloración de gram' WHEN '4' THEN'Antigenemia' WHEN '5' THEN 'RT/PCR' END AS 'PRUEBA',
	CASE AGENTE  WHEN '1' THEN 'Otro' WHEN '2' THEN 'Haemophilus Influenzae' WHEN '3' THEN 'Neisseria Meningitidis' WHEN '4' THEN 'Streptococus pneumonie' 
	WHEN '6' THEN 'Staphylococcus Aereus' WHEN '7' THEN 'Listeria Monocytogenes' WHEN '8' THEN 'E. Coli' WHEN '9' THEN 'Enterobacter clocae' WHEN '10' THEN 'Staphylococcus Epidermis' WHEN '11' THEN 'Streptococcus beta hemiolítico'
	WHEN '12' THEN 'Streptococcus Agalactiae' WHEN '13' THEN 'Streptococcus Pyogenes' END AS 'AGENTE',
	Rtrim(VALOR) As 'VALOR',
	CASE RESULTADO  WHEN '1' THEN 'Positivo' WHEN '2' THEN 'Negativo' END AS 'RESULTADO'


	From HCFICHA535 where IDFICHANOTIFICACION  = @IdFicha


END


--select * from hcfichanotificacion where id = '8'
--select * from HCFICHA535