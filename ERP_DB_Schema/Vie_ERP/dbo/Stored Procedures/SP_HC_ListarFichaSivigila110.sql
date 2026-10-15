-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,08-05-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila110]
(
  @IdFicha as Int,
  @NumFicha as Int
)

AS
BEGIN
  SET NOCOUNT ON;
  			
			Select
				 CASE RNTIPDOC  WHEN 1 THEN 'X' END AS 'Registro civil', CASE RNTIPDOC  WHEN 2 THEN 'X' END AS 'Menor sin identificación' , CASE RNTIPDOC  WHEN 3 THEN 'X' END AS 'Permiso Especial Permanencia', CASE RNTIPDOC  WHEN 4 THEN 'X' END AS 'Certificado Nacido Vivo'
				 ,RTRIM(RNIPCODPACI) as 'Identificacion'
				 ,convert(varchar(10),RNFECHNACI,103) as 'FechaNacimientos'
				 ,RNEDAD as 'Edad'
				 ,CASE RNSEXO  WHEN '1' THEN 'X' END AS 'Masculino',CASE RNSEXO  WHEN '2' THEN 'X' END AS 'Femenino' 
				 ,RNPESO as 'Peso'
				 ,RNTALLA as 'Talla'
				 ,GESTPARTO as 'GestacionParto'
				 ,CASE CLASIFPESO  WHEN '1' THEN 'X' END AS 'Bajopesoalnacer',CASE CLASIFPESO  WHEN '2' THEN 'X' END AS 'Muybajopesonacer'   
				 ,CASE SITIOPARTO  WHEN '1' THEN 'X' END AS 'Instituciónsalud',CASE SITIOPARTO  WHEN '2' THEN 'X' END AS 'Domicilio',CASE SITIOPARTO  WHEN '3' THEN 'X' END AS 'Otro',CASE SITIOPARTO  WHEN '4' THEN 'X' END AS 'Via Publica'
				 ,CASE MULTIPLIEMBARA  WHEN '1' THEN 'X' END AS 'Simple',CASE MULTIPLIEMBARA  WHEN '2' THEN 'X' END AS 'Doble',CASE MULTIPLIEMBARA  WHEN '3' THEN 'X' END AS 'Triple'
     			 ,EMBARAZOPREVIOS AS 'EmbarazosPrevios'
				 ,NUMHIJOS as 'NumerosHijos'
				 ,CASE NIVELEDUCATIVO  WHEN 1 THEN 'X' END AS 'NEducativo_Primaria', CASE NIVELEDUCATIVO  WHEN 2 THEN 'X' END AS 'NEducativo_Secundaria', CASE NIVELEDUCATIVO  WHEN 3 THEN 'X' END AS 'NEducativo_Tecnico', CASE NIVELEDUCATIVO  WHEN 4 THEN 'X' END AS 'NEducativo_Ninguno'
				 , VERSION AS 'VERSION' 
				 , JSON AS 'JSON'

			 From 
				 HCFICHA110 
		
			Where 
				 IDFICHANOTIFICACION  = @IdFicha
		
END