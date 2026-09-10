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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle completo de una ficha epidemiológica SIVIGILA código 348 (vigilancia de influenza y enfermedades respiratorias agudas graves) identificada por su número de ficha. Consolida datos clínicos y epidemiológicos del paciente: si es trabajador de la salud, si hubo deterioro clínico, si el caso está asociado a un brote, historial de viajes nacionales e internacionales en los últimos 14 días (resolviendo municipio y departamento mediante los catálogos INMUNICIP e INDEPARTA), vacunación contra influenza y COVID-19 (dosis, nombre de vacuna y fechas), antecedentes patológicos (asma, EPOC, diabetes, VIH, cardiopatía, cáncer, malnutrición, obesidad, insuficiencia renal, tabaquismo, entre otros), uso de antibióticos y antivirales, tipo de servicio de hospitalización (general, UCI intermedia, UCI intensiva), complicaciones (derrame pleural, pericárdico, miocarditis, septicemia, falla respiratoria), resultados de laboratorio de dos tomas de muestra, síntomas clave (tos, fiebre, contacto con animales o personas), hallazgos radiológicos y datos extendidos almacenados en formato JSON dentro de la ficha. Se usa para imprimir o consultar la ficha SIVIGILA 348 en el módulo de historia clínica y reporte epidemiológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila348';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila348';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea, para una ficha de notificación de Sivigila, los datos epidemiológicos, antecedentes, complicaciones, resultados de laboratorio y campos JSON asociados, marcando con ''X'' las opciones aplicables para impresión/visualización del formato 348.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila348';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA348 cuyo IDFICHANOTIFICACION coincida con el identificador recibido.; Para resolver el lugar de viaje nacional, LUGARVIAJENAL debe enlazar con INUbicaci, INMUNICIP e INDEPARTA; en caso contrario los campos de ubicación quedan vacíos por los LEFT JOIN.; Los campos JSON deben contener texto JSON válido para que JSON_VALUE/TRY_CAST devuelvan valores; de lo contrario se devuelven NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila348';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No modifica datos: es una consulta de solo lectura.; Las opciones de checklist se representan como ''X'' o NULL, no como valores booleanos.; Las fechas se formatean en estilo 103 (dd/mm/yyyy) o vía FORMAT ''dd/MM/yyyy''.; El lugar de viaje nacional siempre se concatena como ''codDep-nomDep ; codMun-nomMun'' a partir de los catálogos territoriales.; Solo se devuelve la ficha cuyo identificador de notificación coincida con el parámetro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila348';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Sivigila (vigilancia epidemiológica); Ficha de notificación 348; Trabajador de salud; Viaje nacional/internacional; Vacunación contra influenza; Vacunación COVID-19 (dosis, fecha, nombre de vacuna); Antecedentes clínicos (asma, EPOC, diabetes, VIH, cardiaca, cáncer, malnutrición, obesidad, insuficiencia renal, fumador); Uso de antibióticos y antivirales; Hospitalización general / UCI intermedios / UCI intensivos; Complicaciones (derrame pleural, derrame pericárdico, miocarditis, septicemia, falla respiratoria); Pruebas de laboratorio (muestra, prueba, agente, resultado); Trombocitopenia y conteo de plaquetas; Hallazgos radiográficos (infiltrado alveolar/intersticial); Contacto con animal/persona, tos, fiebre; COVID-19, HTA; Ubicación geográfica (departamento, municipio, país)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila348';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA348: Cuando IDFICHANOTIFICACION = parámetro de entrada, se retorna una fila con los datos de la ficha transformados a marcadores ''X'' según los códigos de cada columna y los valores extraídos del JSON.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila348';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TRABAJADORSALUD, DETERIOROCLI, CASOBROTE, VIAJO14DIASPRE, ANTEASMA, ANTEEPOC, ANTEDIABETES, ANTEVIH, ANTECARDIACA, ANTECANCER, ANTEMALNUTRI, ANTEOBESIDAD, ANTERENAL, ANTEMEDICA, ANTEFUMADOR, ANTEOTRO y complicaciones (COMDERRAMEPLE, COMDERRAMEPER, COMMIOCARD, COMSEPTICE, COMRESPIRA, COMPOTRA) = ''1'' → Se marca ''X'' en la columna correspondiente. else Se devuelve NULL (no se marca).; si VIAJETERNAL / VIAJETERINTER / ANTIBIOTICOS / ANTIVIRALES = ''1'' → Marca la opción ''Si'' con ''X''. else Si vale ''0'' marca la opción ''No'' con ''X''.; si VACUINFLUENZA = ''1'' / ''2'' / ''3'' → Marca respectivamente ''Vacuna Influ Si'', ''Vacuna Influ No'' o ''Vacuna Desconocido''.; si SERVHOSPITA = ''1'' / ''2'' / ''3'' → Marca respectivamente Hospitalización general, UCI Intermedios o UCI Intensivos.; si JSON.RADIOGRAFIA_HALLAZGO = 1 / 2 / 3 → Marca ''Infiltrado alveolar'', ''Infiltrados intersticiales'' o ''Hallazgo Ninguno'' respectivamente.; si JSON.VACUNACOVID = 0 / 1 / 2 → Marca ''CovidSi'', ''CovidNo'' o ''CovidDesco'' respectivamente.; si JSON.CONTEO_PLAQUETAS IS NOT NULL → Devuelve el valor del conteo de plaquetas; en caso contrario, NULL.; si TRY_CAST de los campos JSON CONTACTO_ANIMAL, CONTACTO_PERSONA, TIENE_TOS, TIENE_FIEBRE, ANTE_TROMBOCITOPENIA, COVID19, HTA a BIT = 1 → Marca con ''X'' la columna correspondiente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila348';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA348; dbo.INUbicaci; dbo.INMUNICIP; dbo.INDEPARTA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila348';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila348';
-- GO
