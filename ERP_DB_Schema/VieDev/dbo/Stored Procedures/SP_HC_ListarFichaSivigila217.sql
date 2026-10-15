-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,26-10-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila217]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 
	Select
		CASE FIEBRE  WHEN '1' THEN 'X' END AS 'FIEBRE', CASE ARTRALGIA  WHEN '1' THEN 'X' END AS 'ARTRALGIA',CASE CEFALEA  WHEN '1' THEN 'X' END AS 'CEFALEA',
		CASE RASH  WHEN '1' THEN 'X' END AS 'RASH',CASE VOMITO  WHEN '1' THEN 'X' END AS 'VOMITO',
		CASE PIEL  WHEN '1' THEN 'X' END AS 'PIEL',CASE HIGADO  WHEN '1' THEN 'X' END AS 'HIGADO', CASE BAZO  WHEN '1' THEN 'X' END AS 'BAZO',
		CASE PULMON  WHEN '1' THEN 'X' END AS 'PULMON',CASE CEREBRO  WHEN '1' THEN 'X' END AS 'CEREBRO', CASE MIOCARDIO  WHEN '1' THEN 'X' END AS 'MIOCARDIO',
		CASE MEDULA  WHEN '1' THEN 'X' END AS 'MEDULA', CASE RINON  WHEN '1' THEN 'X' END AS 'RINON', 
		VERSION AS 'VERSION', 
		JSON AS 'JSON' 
	
	From 
		HCFICHA217 
	Where 
		IDFICHANOTIFICACION  = @IdFicha


END