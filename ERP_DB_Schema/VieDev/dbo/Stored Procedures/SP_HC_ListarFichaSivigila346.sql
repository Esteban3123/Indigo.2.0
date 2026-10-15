-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,21-03-2020,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila 346>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila346]
(
  @IdFicha as Int
  )

AS
BEGIN
  SET NOCOUNT ON;
 
			Select 
				CASE TRABADESALUD when 1 Then 'X' end as 'Trabajado Salud SI',CASE TRABADESALUD when 2 Then 'X' end as 'Trabajado Salud NO',
				CASE VIAJOAREASVIRUS when 1 Then 'X' end as 'Viajo SI',CASE VIAJOAREASVIRUS when 2 Then 'X' end as 'Viajo NO',
				CASE VIAJETERNAL  WHEN '1' THEN 'X' END AS 'Viaje Nacional Si',CASE VIAJETERNAL  WHEN '0' THEN 'X' END AS 'Viaje Nacional No',Rtrim(E.depcodigo) + '-' + Rtrim(E.nomdepart) +' ; '+ Rtrim(P.MUNCODIGO) +'-'+ Rtrim(P.MUNNOMBRE)  as 'Lugar Viaje Nacional',
				CASE VIAJETERINTER  WHEN '1' THEN 'X' END AS 'Viaje Internacional Si',CASE VIAJETERINTER  WHEN '0' THEN 'X' END AS 'Viaje Internacional No', LUGARVIAJEINTER  as 'Lugar Viaje Internacional',
				CASE CONTACTO14DIAS when 1 Then 'X' end as 'Contacto SI',CASE CONTACTO14DIAS when 2 Then 'X' end as 'Contacto NO',
				CASE SIN_TOS WHEN '1' Then 'X' end as 'Paciente Tos',
				CASE SIN_FIEBRE WHEN '1' Then 'X' end as 'Paciente Fiebre',
				CASE SIN_ODINOFAGIA WHEN '1' Then 'X' end as 'Paciente Odinofagia',
				CASE SIN_DIFICULTARESPIRA WHEN '1' Then 'X' end as 'Paciente Difi Respitaroia',
				CASE SIN_FATIGA WHEN '1' Then 'X' end as 'Paciente Fatiga',
				CASE C.SIN_RINORREA WHEN '1' Then 'X' end as 'Paciente Rinorrea', 
				CASE C.SIN_CONJUNTIVITIS WHEN '1' Then 'X' end as 'Paciente Conjuntivitis', 
				CASE C.SIN_CEFALEA WHEN '1' Then 'X' end as 'Paciente Cefalea', 
				CASE C.SIN_DIARREA WHEN '1' Then 'X' end as 'Paciente diarrea',
				CASE C.SIN_OLFATO WHEN '1' Then 'X' end as 'Paciente Olfato', 
				CASE C.OTROSINT WHEN '1' Then 'X' end as 'Paciente Otros',
				CASE VACUINFLUENZA  WHEN '1' THEN 'X' END AS 'Vacuna Influ Si',
				CASE VACUINFLUENZA  WHEN '2' THEN 'X' END AS 'Vacuna Influ No',CASE VACUINFLUENZA  WHEN '3' THEN 'X' END AS 'Vacuna Desconocido',
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
				CASE C.ANTEHIPER WHEN '1' THEN 'X' END AS 'Hipertenso',
				CASE ANTEFUMADOR  WHEN '1' THEN 'X' END AS 'Fumandor',
				CASE ANTTUBERCULOS  WHEN '1' THEN 'X' END AS 'Tuberculosis',
				CASE ANTEOTRO  WHEN '1' THEN 'X' END AS 'Otro',
				Rtrim(OTROSANTE) As 'Otro Antecedentes',
				CASE RADIOTORAX WHEN 1 THEN 'X' END AS 'Radiografia Si',CASE RADIOTORAX  WHEN 2 THEN 'X' END AS 'Radiografia No',CASE RADIOTORAX  WHEN 3 THEN 'X' END AS 'Infiltrados', CASE RADIOTORAX  WHEN 4 THEN 'X' END AS 'Radiografia Desconocido',
				CASE ANTIBIOTICOS  WHEN '1' THEN 'X' END AS 'Uso Antibiotico Si',CASE VACUINFLUENZA  WHEN '0' THEN 'X' END AS 'Uso Antibiotico No',
				CONVERT(varchar(10),LABFECHATOMA1 ,103) as 'Fecha Toma 1',
				CONVERT(varchar(10),LABFECHARECEP1 ,103) as 'Fecha Recepcion 1',
				Rtrim(LABMUESTRA1) As 'Muestra 1',Rtrim(LABPRUEBA1) As 'Prueba 1',Rtrim(LABAGENTE1) As 'Agente 1',Rtrim(LABRESULTADO1) As 'Resultado 1',CONVERT(varchar(10),LABFECHARECEP11 ,103) as 'Fecha Recepcion 11', Rtrim(LABVALOREGIS1) As 'Valor Registrado 1',
				CONVERT(varchar(10),LABFECHATOMA2 ,103) as 'Fecha Toma 2',
				CONVERT(varchar(10),LABFECHARECEP2 ,103) as 'Fecha Recepcion 2',
				Rtrim(LABMUESTRA2) As 'Muestra 2',Rtrim(LABPRUEBA2) As 'Prueba 2',Rtrim(LABAGENTE2) As 'Agente 2',Rtrim(LABRESULTADO2) As 'Resultado 2',CONVERT(varchar(10),LABFECHARECEP22 ,103) as 'Fecha Recepcion 22', Rtrim(LABVALOREGIS2) As 'Valor Registrado 2',
				C.CODDEPTOMUN As 'Codigo DptoMun', 
				RTRIM( JSON_VALUE(JSON,'$.CODIGOPAIS') ) As 'Codigo Pais Internacional',
				C.OTROSINTO As 'Otros Sintomas', CASE SERVHOS  WHEN 1 THEN 'X' END AS 'Hospitalización',CASE SERVHOS  WHEN 2 THEN 'X' END AS 'UCI', CONVERT(varchar(10),C.FECHINGUCI ,103) As 'Fecha UCI', 
				CASE C.COMPDEPLEURAL WHEN '1' THEN 'X' END AS 'PLEURAL',
				CASE C.COMPDEPERI WHEN '1' THEN 'X' END AS 'PERICARDIACO',
				CASE C.COMPMIOCARDITIS WHEN '1' THEN 'X' END AS 'MIOCARDITIS',
				CASE C.COMPSEPTICEMIA WHEN '1' THEN 'X' END AS 'SEPTICEMIA',
				CASE C.COMPRESPI WHEN '1' THEN 'X' END AS 'RESPIRATORIA',
				CASE C.COMPOTRO WHEN '1' THEN 'X' END AS 'OTRO',
				C.OTROCOMPLI As 'Otras Complicaciones',
				CASE RTRIM( JSON_VALUE(JSON,'$.VACUNACOVID') ) WHEN 0 THEN 'X' END AS 'Si',
				CASE RTRIM( JSON_VALUE(JSON,'$.VACUNACOVID') ) WHEN 1 THEN 'X' END AS 'No',
				CASE RTRIM( JSON_VALUE(JSON,'$.VACUNACOVID') ) WHEN 2 THEN 'X' END AS 'Desconocido',
				RTRIM( JSON_VALUE(JSON,'$.NUMDOSIS') ) As 'NumeroDosis',
				CONVERT(varchar(10),RTRIM(JSON_VALUE(JSON,'$.FECDOSIS')) ,103) As 'FechaDosis',
				RTRIM( JSON_VALUE(JSON,'$.NOMBREVACUNA') ) As 'NombreVacuna',
				C.VERSION AS 'VERSION', 
				C.JSON AS 'JSON' 

			From 
				HCFICHA346 C
   				Left join dbo.INUbicaci U on C.LUGARVIAJENAL = U.AUUBICACI
				Left Join dbo.INMUNICIP P on U.DEPMUNCOD = P.DEPMUNCOD
				Left Join dbo.INDEPARTA E on P.DEPCODIGO = E.DEPCODIGO 
				Left join dbo.INUbicaci M on C.LUGARVIAJEINTER = M.AUUBICACI
				Left Join dbo.INMUNICIP R on M.DEPMUNCOD = R.DEPMUNCOD
				Left Join dbo.INDEPARTA O on R.DEPCODIGO = O.DEPCODIGO
								
			 Where
				IDFICHANOTIFICACION  = @IdFicha 
				AND ISJSON(C.JSON) > 0
		
END