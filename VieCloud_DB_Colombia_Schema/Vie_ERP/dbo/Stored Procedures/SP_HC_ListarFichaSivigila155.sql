
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle completo de la ficha 155 del SIVIGILA para un caso de cáncer de mama o cuello uterino, identificada por su número de ficha de notificación. Consulta la tabla HCFICHA155 y devuelve los datos clínicos del paciente en formato legible: tipo de cáncer diagnosticado, fechas de procedimientos, toma de muestra y resultados de biopsias (exocérvix y endocérvix), grado histopatológico, presencia de adenocarcinoma, y los tipos de tratamiento recibidos (radioterapia, cirugía, quimioterapia, hormonoterapia, cuidados paliativos, inmunoterapia) junto con la fecha de inicio del tratamiento. Se utiliza para imprimir o consultar la ficha de notificación obligatoria de cáncer exigida por el sistema de vigilancia epidemiológica SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila155';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila155';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera los datos de una ficha de notificación SIVIGILA 155 (cáncer de mama y cuello uterino) transformando códigos numéricos en marcas ''X'' por categoría para su despliegue en formulario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila155';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA155 cuyo IDFICHANOTIFICACION coincida con el parámetro recibido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila155';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera una ficha por IDFICHANOTIFICACION (clave de búsqueda); Las fechas se entregan siempre en formato dd/mm/yyyy (estilo 103) como cadena; Los campos categóricos se exponen como columnas booleanas tipo ''X''/NULL, una por cada valor posible del código origen; No modifica datos: es una operación de solo lectura', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila155';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha SIVIGILA 155; Cáncer de mama; Cáncer de cuello uterino; Biopsia (ductal, lobulillar, exocérvix, endocérvix); Grado histopatológico (in-situ, infiltrante, invasor); Adenocarcinoma; LEIAG; Carcinoma escamocelular; Tratamiento oncológico (radioterapia, quirúrgico, quimioterapia, hormonoterapia, cuidados paliativos, inmunoterapia); Notificación epidemiológica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila155';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA155: Devuelve el resultset de la ficha filtrando por IDFICHANOTIFICACION = @IdFicha, mapeando códigos a ''X'' por categoría y formateando fechas a varchar(10) estilo 103 (dd/mm/yyyy)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila155';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOCANCER = ''1'' / ''2'' / ''3'' → Marca ''X'' en CA_Mama / CA_CuelloUterino / CA_Ambos respectivamente; si RESULBIOPSIA = ''1'' / ''2'' → Marca ''X'' en Carcioma_ductal / Carcioma_lobulillar; si GRADOHISPA1 = ''1'' / ''2'' / ''3'' → Marca ''X'' en In-situ / Infiltrante / No_indicado del primer grado histopatológico; si BIOEXOTRANSF = ''1'' / ''0'' → Marca ''X'' en BIOEXOCERVIX_SI / BIOEXOCERVIX_NO; si RESULBIOPSIAEXO = ''1'' / ''2'' / ''3'' → Marca ''X'' en LEIAG / Carcinoma escamocelular / Adenocarcinoma; si GRADOHISPA2 = ''1'' a ''5'' → Marca ''X'' en In-situ, Infiltrante, No_indicado, Invasor_Infiltrante_B2 o Invasor_Infiltrante_B3; si BIOPSIAENDOCER = ''1'' / ''0'' → Marca ''X'' en BIOPSIAENDOCER_SI / BIOPSIAENDOCER_NO; si ADENOCARCINOMA = ''1'' / ''2'' → Marca ''X'' en Positivo / Negativo; si GRADOHISPA3 = ''1'' / ''2'' / ''3'' → Marca ''X'' en In-situ / Infiltrante / No_indicado del tercer grado histopatológico; si TRATAINITUMOR = ''1'' / ''0'' → Marca ''X'' en TRATAINITUMOR_SI / TRATAINITUMOR_NO; si TRADIOTERAPIA / TQUIRURGICO / TQUIMIOTERAPIA / THORMONTERAPIA / TCUIDADOSPALI / TINMUNOTERAPIA = ''1'' → Marca ''X'' en la modalidad de tratamiento correspondiente (Radioterapia, Quirúrgico, Quimioterapia, Hormonoterapia, Cuidados Paliativos, Inmunoterapia)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila155';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA155', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila155';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila155';
-- GO
