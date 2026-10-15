-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,29-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila200]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 
	Select
		CASE DESPLAZAMIENTO  WHEN '1' THEN 'X' END AS 'DESPLAZAMIENTO si',CASE DESPLAZAMIENTO WHEN '0' THEN 'X' END AS 'DESPLAZAMIENTO no',
		CASE HECHOCONT  WHEN '1' THEN 'X' END AS 'HECHOCONT si',CASE HECHOCONT WHEN '0' THEN 'X' END AS 'HECHOCONT no',
		CASE CASOASOC  WHEN '1' THEN 'X' END AS 'CASOASOC si',CASE CASOASOC WHEN '0' THEN 'X' END AS 'CASOASOC no',
		CASE CASOCAPT WHEN '1' THEN 'X' END AS 'UPGD',CASE CASOCAPT WHEN '2' THEN 'X' END AS 'BusqueInst', CASE CASOCAPT WHEN '3' THEN 'X' END AS 'VigIntes',
		CASE CASOCAPT WHEN '4' THEN 'X' END AS 'BusqueComun',
		CASE AGENTIDENT WHEN 1 THEN 'X' END AS 'VibrioChole', CASE AGENTIDENT WHEN 2 THEN 'X' END AS 'VibrioSPP', CASE AGENTIDENT WHEN 3 THEN 'X' END AS 'Otro',
		CASE AGENTIDENT WHEN 4 THEN 'X' END AS 'Pendiente', CASE AGENTIDENT WHEN 5 THEN 'X' END AS 'No Detectado', CASE AGENTIDENT WHEN 6 THEN 'X' END AS 'VibChoO1Toxi',
		CASE AGENTIDENT WHEN 7 THEN 'X' END AS 'VibChoO1noToxi', CASE AGENTIDENT WHEN 8 THEN 'X' END AS 'VibChonoO1noO139', CASE AGENTIDENT WHEN 9 THEN 'X' END AS 'VibChono139notoxi',
		CASE AGENTIDENT WHEN 10 THEN 'X' END AS 'VibChoO139',
		Rtrim(DEPART1) As 'DEPART1', Rtrim(DEPART2) As 'DEPART2', Rtrim(DEPART3) As 'DEPART3', Rtrim(DEPART4) As 'DEPART4', Rtrim(CIUDAD1) As 'CIUDAD1', Rtrim(CIUDAD2) As 'CIUDAD2',
		Rtrim(CIUDAD3) As 'CIUDAD3', Rtrim(CIUDAD4) As 'CIUDAD4', Rtrim(PAIS1) As 'PAIS1', Rtrim(PAIS2) As 'PAIS2', Rtrim(PAIS3) As 'PAIS3', Rtrim(PAIS4) As 'PAIS4',  Rtrim(CONQUIEN) As 'CONQUIEN',
		convert(varchar(10),FECHDESP1,103) As 'FECHDESP1', convert(varchar(10),FECHDESP2,103) As 'FECHDESP2',
		convert(varchar(10),FECHDESP3,103) As 'FECHDESP3', convert(varchar(10),FECHDESP4,103) As 'FECHDESP4',
		convert(varchar(10),FECHLLEG1,103) As 'FECHLLEG1', convert(varchar(10),FECHLLEG2,103) As 'FECHLLEG2',
		convert(varchar(10),FECHLLEG3,103) As 'FECHLLEG3', convert(varchar(10),FECHLLEG4,103) As 'FECHLLEG4',
		VERSION AS 'VERSION', 
		JSON AS 'JSON' 

	From 
		HCFICHA200 
	
	Where 
		IDFICHANOTIFICACION  = @IdFicha 
		
END