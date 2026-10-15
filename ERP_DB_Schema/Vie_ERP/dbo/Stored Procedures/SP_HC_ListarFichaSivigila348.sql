-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,08-05-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila348]
(
  @IdFicha as Int
  )

AS
BEGIN
  SET NOCOUNT ON;

			Select 
				 CASE TRABAJADORSALUD  WHEN '1' THEN 'X' END AS 'Trabajador Salid',CASE DETERIOROCLI  WHEN '1' THEN 'X' END AS 'Prsenta Deterioro',CASE CASOBROTE  WHEN '1' THEN 'X' END AS 'Caso asociado',
				 CASE VIAJO14DIASPRE WHEN '1' THEN 'X' END AS 'Viajo',
				 CASE VIAJETERNAL  WHEN '1' THEN 'X' END AS 'Viaje Nacional Si',CASE VIAJETERNAL  WHEN '0' THEN 'X' END AS 'Viaje Nacional No', 
				 Rtrim(E.depcodigo) + '-' + Rtrim(E.nomdepart) +' ; '+ Rtrim(P.MUNCODIGO) +'-'+ Rtrim(P.MUNNOMBRE)  as 'Lugar Viaje Nacional',
				 CASE VIAJETERINTER  WHEN '1' THEN 'X' END AS 'Viaje Internacional Si',CASE VIAJETERINTER  WHEN '0' THEN 'X' END AS 'Viaje Internacional No', 
				 CASE VACUINFLUENZA  WHEN '1' THEN 'X' END AS 'Vacuna Influ Si',
				 CASE VACUINFLUENZA  WHEN '2' THEN 'X' END AS 'Vacuna Influ No',
				 CASE VACUINFLUENZA  WHEN '3' THEN 'X' END AS 'Vacuna Desconocido',
				 Rtrim(DOSIS) As 'Dosis',				 
				 CASE ANTEASMA  WHEN '1' THEN 'X' END AS 'Asma',
				 CASE ANTEEPOC  WHEN '1' THEN 'X' END AS 'EPOC',
				 CASE ANTEDIABETES  WHEN '1' THEN 'X' END AS 'Diabetes',
				 CASE ANTEVIH  WHEN '1' THEN 'X' END AS 'VIH',
				 CASE ANTECARDIACA  WHEN '1' THEN 'X' END AS 'Cardiaca',
				 CASE ANTECANCER   WHEN '1' THEN 'X' END AS 'Cancer',
				 CASE ANTEMALNUTRI  WHEN '1' THEN 'X' END AS 'Malnutrición',
				 CASE ANTEOBESIDAD  WHEN '1' THEN 'X' END AS 'Obesidad',
				 CASE ANTERENAL WHEN '1' THEN 'X' END AS 'Insuficiencia renal',
				 CASE ANTEMEDICA WHEN '1' THEN 'X' END AS 'Toma medicamentos',
				 CASE ANTEFUMADOR  WHEN '1' THEN 'X' END AS 'Fumador',
				 CASE ANTEOTRO  WHEN '1' THEN 'X' END AS 'Otro',				 
				 Rtrim(OTROSANTE) As 'Otro Antecedentes',
				 CASE ANTIBIOTICOS  WHEN '1' THEN 'X' END AS 'Uso Antibiotico Si',CASE ANTIBIOTICOS  WHEN '0' THEN 'X' END AS 'Uso Antibiotico No',
				 CASE ANTIVIRALES  WHEN '1' THEN 'X' END AS 'Uso Antivirales Si',CASE ANTIVIRALES  WHEN '0' THEN 'X' END AS 'Uso Antivirales No',
				 CONVERT(varchar(10),FECHANTIVIR,103) as 'Fecha Inicio Antivirales',
				 CASE SERVHOSPITA  WHEN '1' THEN 'X' END AS 'Hospitalización general',CASE SERVHOSPITA  WHEN '2' THEN 'X' END AS 'UCINTERMEDIOS', CASE SERVHOSPITA  WHEN '3' THEN 'X' END AS 'UCINTENSIVOS',
				 CONVERT(varchar(10),FECHAINGREUCI,103) as 'Fecha Ingrso UCI',
				 CASE COMDERRAMEPLE   WHEN '1' THEN 'X' END AS 'Derrame pleural',
				 CASE COMDERRAMEPER   WHEN '1' THEN 'X' END AS 'Derrame pericárdico',
				 CASE COMMIOCARD   WHEN '1' THEN 'X' END AS 'Miocarditis',
				 CASE COMSEPTICE   WHEN '1' THEN 'X' END AS 'Septicemia',
				 CASE COMRESPIRA   WHEN '1' THEN 'X' END AS 'Falla respiratoria',
				 CASE COMPOTRA  WHEN '1' THEN 'X' END AS 'Otr Complicacion',
				 CONVERT(varchar(10),LABFECHATOMA1 ,103) as 'Fecha Toma 1',
				 CONVERT(varchar(10),LABFECHARECEP1 ,103) as 'Fecha Recepcion 1',
				 Rtrim(LABMUESTRA1) As 'Muestra 1',Rtrim(LABPRUEBA1) As 'Prueba 1',Rtrim(LABAGENTE1) As 'Agente 1',Rtrim(LABRESULTADO1) As 'Resultado 1',CONVERT(varchar(10),LABFECHARECEP11 ,103) as 'Fecha Recepcion 11', Rtrim(LABVALOREGIS1) As 'Valor Registrado 1',
				 CONVERT(varchar(10),LABFECHATOMA2 ,103) as 'Fecha Toma 2',
				 CONVERT(varchar(10),LABFECHARECEP2 ,103) as 'Fecha Recepcion 2',
				 Rtrim(LABMUESTRA2) As 'Muestra 2',Rtrim(LABPRUEBA2) As 'Prueba 2',Rtrim(LABAGENTE2) As 'Agente 2',Rtrim(LABRESULTADO2) As 'Resultado 2',CONVERT(varchar(10),LABFECHARECEP22 ,103) as 'Fecha Recepcion 22', Rtrim(LABVALOREGIS2) As 'Valor Registrado 2',
				 C.VERSION AS 'VERSION', 
				 C.JSON AS 'JSON', 
				 CASE TRY_CAST( JSON_VALUE(C.JSON,'$.CONTACTO_ANIMAL') AS BIT) WHEN 1 THEN 'X' END AS 'Contacto_Animal', 
				 CASE TRY_CAST( JSON_VALUE(C.JSON,'$.CONTACTO_PERSONA') AS BIT) WHEN 1 THEN 'X' END AS 'Contacto_Persona', 
				 CASE TRY_CAST( JSON_VALUE(C.JSON,'$.TIENE_TOS') AS BIT) WHEN 1 THEN 'X' END AS 'Tiene_Tos',
				 CASE TRY_CAST( JSON_VALUE(C.JSON,'$.TIENE_FIEBRE') AS BIT) WHEN 1 THEN 'X' END AS 'Tiene_Fiebre', 
				 Rtrim(JSON_VALUE(C.JSON,'$.PAIS_CODIGO')) + ' - ' + Rtrim(JSON_VALUE(C.JSON,'$.PAIS_TEXTO'))  as 'Lugar Viaje Internacional',
				 CASE TRY_CAST( JSON_VALUE(C.JSON,'$.ANTE_TROMBOCITOPENIA') AS BIT) WHEN 1 THEN 'X' END AS 'Trombocitopenia', 
				 CASE WHEN JSON_VALUE(C.JSON,'$.CONTEO_PLAQUETAS') IS NOT NULL THEN JSON_VALUE(C.JSON,'$.CONTEO_PLAQUETAS') END AS 'Conteo_Plaquetas',
				 CASE JSON_VALUE(C.JSON,'$.RADIOGRAFIA_HALLAZGO') WHEN 1 THEN 'X' END AS 'Infiltrado alveolar', CASE JSON_VALUE(C.JSON,'$.RADIOGRAFIA_HALLAZGO') WHEN 2 THEN 'X' END AS 'Infiltrados intersticiales',CASE JSON_VALUE(C.JSON,'$.RADIOGRAFIA_HALLAZGO')  WHEN 3 THEN 'X' END AS 'Hallazgo Ninguno',
				 CASE RTRIM( JSON_VALUE(JSON,'$.VACUNACOVID') ) WHEN 0 THEN 'X' END AS 'CovidSi',
				CASE RTRIM( JSON_VALUE(JSON,'$.VACUNACOVID') ) WHEN 1 THEN 'X' END AS 'CovidNo',
				CASE RTRIM( JSON_VALUE(JSON,'$.VACUNACOVID') ) WHEN 2 THEN 'X' END AS 'CovidDesco',
				RTRIM( JSON_VALUE(JSON,'$.NUMDOSIS') ) As 'NUMDOSIS',
				format(CONVERT(date,RTRIM(JSON_VALUE(JSON,'$.FECDOSIS'))),'dd/MM/yyyy') As 'FECDOSIS',
				RTRIM( JSON_VALUE(JSON,'$.NOMBREVACUNA') ) As 'NOMBREVACUNA',
				RTRIM(JSON_VALUE(C.JSON, '$.CODIGOPAISNAL')) As 'CODIGONAL',
				RTRIM(JSON_VALUE(C.JSON, '$.PAIS_CODIGO')) As 'CODIGOPAIS',
				CASE TRY_CAST(JSON_VALUE(C.JSON,'$.COVID19') AS BIT) WHEN 1 THEN 'X' END AS 'COVID19',
				CASE TRY_CAST(JSON_VALUE(C.JSON,'$.HTA') AS BIT) WHEN 1 THEN 'X' END AS 'HTA'
			From 
				HCFICHA348 C
   				Left join dbo.INUbicaci U on C.LUGARVIAJENAL = U.AUUBICACI
				Left Join dbo.INMUNICIP P on U.DEPMUNCOD = P.DEPMUNCOD
				Left Join dbo.INDEPARTA E on P.DEPCODIGO = E.DEPCODIGO 
			 Where 
				IDFICHANOTIFICACION  = @IdFicha 
				--AND ISJSON(C.JSON) > 0
		END