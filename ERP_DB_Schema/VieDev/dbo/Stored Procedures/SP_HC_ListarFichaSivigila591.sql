-- Stored Procedure
-- =============================================
-- Autor:		Jean Carlos Roldan Lozano
-- Fecha Creacion: 22-11-2018
-- Descripcion:	Sp que me lista la Información de las Fichas del Sivigila, Ficha 591
-- Modifico:    Yezid Garcia Medina
-- Fecha Modificación : 30-12-2021
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila591]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;

			Select
				CASE SITIODEF WHEN '1' THEN 'X' END AS 'Hospital', CASE SITIODEF WHEN '2' THEN 'X' END AS 'Centro', CASE SITIODEF WHEN '3' THEN 'X' END AS 'CasaDom',
				CASE SITIODEF WHEN '4' THEN 'X' END AS 'LugTrab', CASE SITIODEF WHEN '5' THEN 'X' END AS 'ViaPub', CASE SITIODEF WHEN '6' THEN 'X' END AS 'OtroSit',
				CASE SITIODEF WHEN '7' THEN 'X' END AS 'SinInformacion', Rtrim(NOMAPE) As 'NOMAPE', 				
				CASE Rtrim(TIPOID) 
					WHEN '1' THEN 'RC - Registro Civil' WHEN '2' THEN 'TI - Tarjeta de Identidad' WHEN '3' THEN 'CC - Cédula de Ciudadanía' WHEN '4' THEN 'CE - Cédula de Extranjería' 
					WHEN '5' THEN 'PA - Pasaporte' WHEN '6' THEN 'MS - Menor Sin Identificación' WHEN '7' THEN 'AS - Adulto Sin Identificación' WHEN '8' THEN 'PE - Permiso Especial de Permanencia' 
					WHEN '9' THEN 'CN - Certificado de Nacido Vivo' WHEN '10' THEN 'CD - Carnet Diplomático' WHEN '11' THEN 'SC - Salvoconducto' WHEN '13' THEN 'DE - Documento Extranjero' 
					WHEN '14' THEN 'PT - Permiso Temporal de Permanencia'
				END AS 'TIPOID', 
				Rtrim(NUMIDEN) As 'NUMIDEN',
				Rtrim(EDAD) As 'EDAD', Rtrim(NUMHIJVIVOS) As 'NUMHIJVIVOS', Rtrim(NUMHIJMUERT) As 'NUMHIJMUERT', Rtrim(ULTIMOANIO) As 'ULTIMOANIO',
				CASE ESTADCONYU WHEN '1' THEN 'X' END AS 'Nocasamas', CASE ESTADCONYU WHEN '2' THEN 'X' END AS 'Nocasamenos', CASE ESTADCONYU WHEN '3' THEN 'X' END AS 'Divorciada',
				CASE ESTADCONYU WHEN '4' THEN 'X' END AS 'Viuda', CASE ESTADCONYU WHEN '5' THEN 'X' END AS 'Soltera', CASE ESTADCONYU WHEN '6' THEN 'X' END AS 'Casada',
				CASE ESTADCONYU WHEN '7' THEN 'X' END AS 'SinInfo',
				CASE NECROPSIA WHEN '1' THEN 'X' END AS 'NECROPSIA', CASE HISTCLINICA WHEN '1' THEN 'X' END AS 'HISTCLINICA', CASE PRUEBLAB WHEN '1' THEN 'X' END AS 'PRUEBLAB',
				CASE INTERROG WHEN '1' THEN 'X' END AS 'INTERROG', CASE ASISMED WHEN '1' THEN 'X' END AS 'ASISMED',
				Rtrim(CAUSAA) As 'CAUSAA', Rtrim(CAUSAB) As 'CAUSAB', Rtrim(CAUSAC) As 'CAUSAC',
				Rtrim(CAUSAD) As 'CAUSAD', Rtrim(OTROESTAD) As 'OTROESTAD', Rtrim(CAUSAPROB) As 'CAUSAPROB',
				CASE CLASIFIN WHEN '1' THEN 'X' END AS 'DNT', CASE CLASIFIN WHEN '2' THEN 'X' END AS 'IRA', CASE CLASIFIN WHEN '3' THEN 'X' END AS 'EDA', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON' 
	
			From
				HCFICHA591

			Where
				IDFICHANOTIFICACION  = @IdFicha
		
END