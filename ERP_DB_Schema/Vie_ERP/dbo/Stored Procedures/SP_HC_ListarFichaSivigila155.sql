
-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,08-05-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila155]
(
  @IdFicha as Int
)

AS
BEGIN
  SET NOCOUNT ON;
  			

	Select 

		CASE TIPOCANCER  WHEN '1' THEN 'X' END AS 'CA_Mama',CASE TIPOCANCER  WHEN '2' THEN 'X' END AS 'CA_CuelloUterino' ,CASE TIPOCANCER  WHEN '3' THEN 'X' END AS 'CA_Ambos' 
		,convert(varchar(10),FECHAPROCEDI,103) as 'FechaProcedimiento'
		,convert(varchar(10),FECHARESULT1,103) as 'FechaResultado1'
		,CASE RESULBIOPSIA  WHEN '1' THEN 'X' END AS 'Carcioma_ductal',CASE RESULBIOPSIA  WHEN '2' THEN 'X' END AS 'Carcioma_lobulillar' 
		,CASE GRADOHISPA1  WHEN '1' THEN 'X' END AS 'GradoHispa1_In-situ',CASE GRADOHISPA1  WHEN '2' THEN 'X' END AS 'GradoHispa1_Infiltrante' ,CASE GRADOHISPA1  WHEN '3' THEN 'X' END AS 'GradoHispa1_No_indicado' 
		,convert(varchar(10),FECHATOMAMUES,103) as 'FechaTomaMuestra'
		,convert(varchar(10),FECHARESULT2,103) as 'FechaResultado2'
		,CASE BIOEXOTRANSF   WHEN '1' THEN 'X' END AS 'BIOEXOCERVIX_SI',CASE BIOEXOTRANSF  WHEN '0' THEN 'X' END AS 'BIOEXOCERVIX_NO' 
		,CASE RESULBIOPSIAEXO    WHEN '1' THEN 'X' END AS 'ResultadoBioExocervix_LEIAG',CASE RESULBIOPSIAEXO  WHEN '2' THEN 'X' END AS 'ResultadoBioExocervix_Carcinomaescamocelular',CASE RESULBIOPSIAEXO  WHEN '3' THEN 'X' END AS 'ResultadoBioExocervix_AdenocarcinomaM'
		,CASE GRADOHISPA2  WHEN '1' THEN 'X' END AS 'GradoHispa2_In-situ',CASE GRADOHISPA2  WHEN '2' THEN 'X' END AS 'GradoHispa2_Infiltrante' ,CASE GRADOHISPA2  WHEN '3' THEN 'X' END AS 'GradoHispa2_No_indicado' , CASE GRADOHISPA2  WHEN '4' THEN 'X' END AS 'GradoHispa2_Invasor_Infiltrante_B2',CASE GRADOHISPA2  WHEN '5' THEN 'X' END AS 'GradoHispa2_Invasor_Infiltrante_B3'
		,CASE BIOPSIAENDOCER    WHEN '1' THEN 'X' END AS 'BIOPSIAENDOCER_SI',CASE BIOPSIAENDOCER  WHEN '0' THEN 'X' END AS 'BIOPSIAENDOCER_NO' 
		,CASE ADENOCARCINOMA   WHEN '1' THEN 'X' END AS 'ADENOCARCINOMA_Positivo',CASE ADENOCARCINOMA  WHEN '2' THEN 'X' END AS 'ADENOCARCINOMA_Negativo' 
		,CASE GRADOHISPA3  WHEN '1' THEN 'X' END AS 'GradoHispa3_In-situ',CASE GRADOHISPA3  WHEN '2' THEN 'X' END AS 'GradoHispa3_Infiltrante' ,CASE GRADOHISPA3  WHEN '3' THEN 'X' END AS 'GradoHispa3_No_indicado' 
		,CASE TRATAINITUMOR     WHEN '1' THEN 'X' END AS 'TRATAINITUMOR_SI',CASE TRATAINITUMOR  WHEN '0' THEN 'X' END AS 'TRATAINITUMOR_NO' 
		,CASE TRADIOTERAPIA WHEN '1' THEN 'X' END AS 'Tratamiento_Radioterapia'
		,CASE TQUIRURGICO WHEN '1' THEN 'X' END AS 'Tratamiento_Quirurgico'
		,CASE TQUIMIOTERAPIA  WHEN '1' THEN 'X' END AS 'Tratamiento_Quimioterapia'
		,CASE THORMONTERAPIA   WHEN '1' THEN 'X' END AS 'Tratamiento_Hormonoterapia'
		,CASE TCUIDADOSPALI    WHEN '1' THEN 'X' END AS 'Tratamiento_CuidadoPaleativos'
		,CASE TINMUNOTERAPIA     WHEN '1' THEN 'X' END AS 'Tratamiento_InmunoTerapia'
		,convert(varchar(10),FECHAINITRATA,103) as 'FechaInicioTratamiento' 
		, VERSION AS 'VERSION' 
		, JSON AS 'JSON' 

	From 
		HCFICHA155 

	Where 
		IDFICHANOTIFICACION  = @IdFicha 
		
END