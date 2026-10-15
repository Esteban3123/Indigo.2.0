-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,19-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila228]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON; 

	Select
		Rtrim(D16) As 'D16', Rtrim(D15) As 'D15', Rtrim(D13) As 'D13', Rtrim(D12) As 'D12',
		Rtrim(D11) As 'D11', Rtrim(D21) As 'D21', Rtrim(D22) As 'D22', Rtrim(D23) As 'D23',
		Rtrim(D25) As 'D25', Rtrim(D26) As 'D26', Rtrim(D36) As 'D36', Rtrim(D46) As 'D46',
		CASE CLASIFCLIN WHEN '1' THEN 'X' END AS 'Normal', CASE CLASIFCLIN WHEN '2' THEN 'X' END AS 'Dudoso', CASE CLASIFCLIN WHEN '3' THEN 'X' END AS 'Muy Leve',
		CASE CLASIFCLIN WHEN '4' THEN 'X' END AS 'Leve', CASE CLASIFCLIN WHEN '5' THEN 'X' END AS 'Moderada', CASE CLASIFCLIN WHEN '6' THEN 'X' END AS 'Severa',
		CASE PRESECARI WHEN '1' THEN 'X' END AS 'PRESECARI Si',CASE PRESECARI WHEN '0' THEN 'X' END AS 'PRESECARI No',
		CASE TIPCARIE WHEN '1' THEN 'X' END AS 'TIPCARIE Si',CASE TIPCARIE WHEN '0' THEN 'X' END AS 'TIPCARIE No',
		CASE FUENTECONS WHEN '1' THEN 'X' END AS 'Acueducto', CASE FUENTECONS WHEN '2' THEN 'X' END AS 'Pozo', CASE FUENTECONS WHEN '3' THEN 'X' END AS 'Quebrada',
		CASE FUENTECONS WHEN '4' THEN 'X' END AS 'Agua Embotellada', CASE FUENTECONS WHEN '5' THEN 'X' END AS 'Otro',
		CASE INGESTCREM WHEN '1' THEN 'X' END AS 'INGESTCREM Si',CASE INGESTCREM WHEN '0' THEN 'X' END AS 'INGESTCREM No',
		CASE INGESTENJUA WHEN '1' THEN 'X' END AS 'INGESTENJUA Si',CASE INGESTENJUA WHEN '0' THEN 'X' END AS 'INGESTENJUA No',
		CASE APLITOP WHEN '1' THEN 'X' END AS 'APLITOP Si',CASE APLITOP WHEN '0' THEN 'X' END AS 'APLITOP No',
		CASE PERSORECLAC WHEN '1' THEN 'X' END AS 'SI', CASE PERSORECLAC WHEN '2' THEN 'X' END AS 'NO', CASE PERSORECLAC WHEN '3' THEN 'X' END AS 'DESCONOCIDO', 
		VERSION AS 'VERSION', 
		JSON AS 'JSON' 

	From 
		HCFICHA228 
	
	Where 
		IDFICHANOTIFICACION  = @IdFicha


END