-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,08-05-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila560]
(
  @IdFicha as Int
  )

AS
BEGIN
  SET NOCOUNT ON;
    
			Select 
				Rtrim(GESTACION) As 'Gestaciones',Rtrim(PARTOSVAG) As 'Partos Vaginales',Rtrim(CESAREAS) As 'Cesareas',Rtrim(ABORTOS) As 'Abortos',Rtrim(VIVOS) As 'Vivos',Rtrim(MUERTOS) As 'Vivos', 
				Rtrim(NUMCPN) As 'Numero C_P_N',Rtrim(SEMANACPN) As 'Semana Inicio C_P_N',	 
				CASE TIPOPARTO  WHEN '1' THEN 'X' END AS 'Vaginal',CASE TIPOPARTO  WHEN '2' THEN 'X' END AS 'Cesárea',CASE TIPOPARTO  WHEN '3' THEN 'X' END AS 'Instrumentado',CASE TIPOPARTO  WHEN '4' THEN 'X' END AS 'Ignorado',CASE TIPOPARTO  WHEN '5' THEN 'X' END AS 'No nació', 
				FECHAPARTO As 'Fecha Parto', 
				CASE SITIODEFUN  WHEN '1' THEN 'X' END AS 'Domicilio Defuncion',CASE SITIODEFUN  WHEN '2' THEN 'X' END AS 'Otr Sitio Defusion',CASE SITIODEFUN  WHEN '3' THEN 'X' END AS 'Baja complejidad',CASE SITIODEFUN  WHEN '4' THEN 'X' END AS 'Mediana complejidad',CASE SITIODEFUN  WHEN '5' THEN 'X' END AS 'Alta complejidad',CASE SITIODEFUN  WHEN '6' THEN 'X' END AS 'UCI',CASE SITIODEFUN  WHEN '7' THEN 'X' END AS 'Traslado interinstituciona',CASE SITIODEFUN  WHEN '8' THEN 'X' END AS 'Traslado domicilio IPS', 
				CASE PARTOATENDI  WHEN '1' THEN 'X' END AS 'Parto Médico general',CASE PARTOATENDI  WHEN '2' THEN 'X' END AS 'Parto Médico obstetra',CASE PARTOATENDI  WHEN '3' THEN 'X' END AS 'Parto Enfermera',CASE PARTOATENDI  WHEN '4' THEN 'X' END AS ' Parto Auxiliar enfermería',CASE PARTOATENDI  WHEN '5' THEN 'X' END AS 'Parto Promotor',CASE PARTOATENDI  WHEN '6' THEN 'X' END AS 'parto Partera',CASE PARTOATENDI  WHEN '7' THEN 'X' END AS 'Parto Otro',CASE PARTOATENDI  WHEN '8' THEN 'X' END AS 'Ella misma', 
				CASE SITIOPARTO  WHEN '1' THEN 'X' END AS 'Institucional',CASE SITIOPARTO  WHEN '2' THEN 'X' END AS 'Domicilio Parto',CASE SITIOPARTO  WHEN '3' THEN 'X' END AS 'Otro Sitio Parto', 
				CASE NIVELATEN2  WHEN '1' THEN 'X' END AS 'Nivel Baja complejidad 2 ',CASE NIVELATEN2  WHEN '2' THEN 'X' END AS 'Nivel Mediana complejidad 2 ',CASE NIVELATEN2  WHEN '3' THEN 'X' END AS 'Nivel Alta complejidad 2', 
				CASE MOMENTOMUER WHEN '1' THEN 'X' END AS 'Anteparto',CASE MOMENTOMUER WHEN '2' THEN 'X' END AS 'Intraparto',CASE MOMENTOMUER WHEN '3' THEN 'X' END AS 'Prealta en postparto',CASE MOMENTOMUER WHEN '4' THEN 'X' END AS 'Postalta en postparto',CASE MOMENTOMUER WHEN '5' THEN 'X' END AS 'Reingreso en postparto',CASE MOMENTOMUER WHEN '6' THEN 'X' END AS 'No aplica', 
				Rtrim(EDADGESTACIO) As 'Edad Gestacional', 
				Rtrim(EDADNEONATAL) As 'Edad Neonatal Muerte', 
				Rtrim(PESONACER) As 'Peso al nacer', 
				Rtrim(TALLANACER) As 'Talla al Nacer', 
				CASE SEXO  WHEN '1' THEN 'X' END AS 'Masculino',CASE SEXO  WHEN '2' THEN 'X' END AS 'Femenino',CASE SEXO  WHEN '3' THEN 'X' END AS 'Indeterminado', 
				CASE CAUSAMUERTE  WHEN '1' THEN 'X' END AS 'Historia clínica',CASE CAUSAMUERTE  WHEN '2' THEN 'X' END AS 'Autopsia verbal',CASE CAUSAMUERTE  WHEN '3' THEN 'X' END AS 'Necropsia', 
				'' As 'Causa Muerte Agrupadas', 
				'' As 'Causa Neoatales', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON' 
		
			From 
				HCFICHA560 C 

		   Where 
				IDFICHANOTIFICACION = @IdFicha 

END