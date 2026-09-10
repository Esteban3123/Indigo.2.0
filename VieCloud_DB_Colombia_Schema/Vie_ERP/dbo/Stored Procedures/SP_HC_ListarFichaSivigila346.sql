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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo de una ficha epidemiológica SIVIGILA 346 (enfermedades respiratorias de notificación obligatoria, como influenza o COVID-19) identificada por su ID de ficha. Combina los datos clínicos y epidemiológicos registrados en la ficha —antecedentes de exposición, síntomas respiratorios (tos, fiebre, dificultad respiratoria, odinofagia, etc.), comorbilidades (asma, EPOC, diabetes, VIH, hipertensión, obesidad, entre otras), resultados de laboratorio, estado de vacunación contra influenza y COVID-19, y complicaciones (septicemia, miocarditis, derrame pleural, etc.)— con los catálogos maestros de municipios y departamentos de Colombia (INMUNICIP, INDEPARTA, INUBICACI) para resolver en texto legible el lugar de viaje nacional e internacional del paciente. Se usa para imprimir o consultar la ficha de notificación epidemiológica obligatoria en el módulo de Historia Clínica, facilitando el reporte a las autoridades de salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila346';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila346';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea para impresión los datos clínicos y epidemiológicos de una ficha de notificación Sivigila 346 (vigilancia de COVID-19/IRA), incluyendo síntomas, antecedentes, viajes, laboratorios, complicaciones y vacunación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila346';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La ficha identificada debe existir en HCFICHA346.; El campo JSON de la ficha debe ser un JSON válido (ISJSON(JSON) > 0); de lo contrario no se retorna fila.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila346';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No retorna filas cuyo campo JSON no sea un JSON válido.; Las respuestas binarias se entregan como columnas separadas con marca ''X'' o NULL, no como valor original.; El lugar de viaje nacional se construye concatenando código y nombre de departamento con código y nombre de municipio separados por ''-'' y '' ; ''.; Los datos de vacunación COVID, número de dosis, fecha y nombre de vacuna se extraen del campo JSON mediante JSON_VALUE.; Las fechas se formatean en estilo 103 (dd/mm/yyyy).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila346';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha Sivigila 346; Vigilancia epidemiológica COVID-19/IRA; Antecedentes clínicos del paciente; Síntomas respiratorios; Viajes nacionales e internacionales; Contacto epidemiológico; Vacunación influenza; Vacunación COVID; Hospitalización y UCI; Complicaciones clínicas; Pruebas de laboratorio (muestra, prueba, agente, resultado); Radiografía de tórax', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila346';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA346: Cuando IDFICHANOTIFICACION coincide con el parámetro y el campo JSON es válido, devuelve un resultset con los datos de la ficha 346 transformados a marcas ''X'' según códigos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila346';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TRABADESALUD = 1 / = 2 → Marca ''Trabajado Salud SI'' / ''NO'' respectivamente; si VIAJOAREASVIRUS = 1 / = 2 → Marca ''Viajo SI'' / ''NO''; si VIAJETERNAL = ''1'' / ''0'' → Marca ''Viaje Nacional Si'' / ''No''; si VIAJETERINTER = ''1'' / ''0'' → Marca ''Viaje Internacional Si'' / ''No''; si CONTACTO14DIAS = 1 / = 2 → Marca ''Contacto SI'' / ''NO'' (contacto en últimos 14 días); si Cada SIN_* = ''1'' → Marca el síntoma correspondiente (tos, fiebre, odinofagia, dificultad respiratoria, fatiga, rinorrea, conjuntivitis, cefalea, diarrea, alteración olfato, otros); si VACUINFLUENZA = ''1'' / ''2'' / ''3'' → Marca vacuna influenza Si / No / Desconocido; si Cada ANTE* = ''1'' → Marca el antecedente clínico (asma, EPOC, diabetes, VIH, cardiaca, cáncer, malnutrición, obesidad, renal, medicamentos, hipertensión, fumador, tuberculosis, otro); si RADIOTORAX = 1 / 2 / 3 / 4 → Marca Radiografía Si / No / Infiltrados / Desconocido; si ANTIBIOTICOS = ''1'' → Marca ''Uso Antibiotico Si''; si VACUINFLUENZA = ''0'' → Marca ''Uso Antibiotico No'' (posible bug: usa VACUINFLUENZA en lugar de ANTIBIOTICOS); si SERVHOS = 1 / 2 → Marca ''Hospitalización'' / ''UCI''; si Cada COMP* = ''1'' → Marca complicación (pleural, pericárdico, miocarditis, septicemia, respiratoria, otro); si JSON_VALUE(JSON,''$.VACUNACOVID'') = 0 / 1 / 2 → Marca vacuna COVID Si / No / Desconocido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila346';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA346; dbo.INUbicaci; dbo.INMUNICIP; dbo.INDEPARTA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila346';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila346';
-- GO
