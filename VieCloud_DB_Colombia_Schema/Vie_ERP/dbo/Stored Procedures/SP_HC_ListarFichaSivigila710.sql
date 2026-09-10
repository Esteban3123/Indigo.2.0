-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,07-12-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila710]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 
	Select
	Rtrim(NOMPADMAD) As 'NOMPADMAD', Rtrim(OCUPAPADMAD) As 'OCUPAPADMAD', Rtrim(DIRETRABA) As 'DIRETRABA', Rtrim(DOSIS1) As 'DOSIS1', Rtrim(DOSIS2) As 'DOSIS2',
	Rtrim(DIAGNOINIC) As 'DIAGNOINIC', Rtrim(DURAERUP) As 'DURAERUP', Rtrim(NUMSEMANAS) As 'NUMSEMANAS', Rtrim(LUGAPROBA) As 'LUGAPROBA', Rtrim(ADONDE) As 'ADONDE',
	Rtrim(VALORREGIS1) As 'VALORREGIS1', Rtrim(VALORREGIS2) As 'VALORREGIS2', Rtrim(VALORREGIS3) As 'VALORREGIS3', Rtrim(VALORREGIS4) As 'VALORREGIS4', Rtrim(CASOIMPOR) As 'CASOIMPOR',
	convert(varchar(10),ULTIDOSIS1,103) As 'ULTIDOSIS1', convert(varchar(10),ULTIDOSIS2,103) As 'ULTIDOSIS2', convert(varchar(10),VISIDOMI,103) As 'VISIDOMI',
	convert(varchar(10),INICIFIEBRE,103) As 'INICIFIEBRE', convert(varchar(10),INICIERUP,103) As 'INICIERUP', convert(varchar(10),FECHATOMA1,103) As 'FECHATOMA1',
	convert(varchar(10),FECHATOMA2,103) As 'FECHATOMA2', convert(varchar(10),FECHATOMA3,103) As 'FECHATOMA3', convert(varchar(10),FECHATOMA4,103) As 'FECHATOMA4',
	convert(varchar(10),FECHARECE1,103) As 'FECHARECE1', convert(varchar(10),FECHARECE2,103) As 'FECHARECE2', convert(varchar(10),FECHARECE3,103) As 'FECHARECE3',
	convert(varchar(10),FECHARECE4,103) As 'FECHARECE4', convert(varchar(10),FECHARESUL1,103) As 'FECHARESUL1', convert(varchar(10),FECHARESUL2,103) As 'FECHARESUL2',
	convert(varchar(10),FECHARESUL3,103) As 'FECHARESUL3', convert(varchar(10),FECHARESUL4,103) As 'FECHARESUL4',
	CASE VACUCONTSARA WHEN '1' THEN 'X' END AS 'VACUCONTSARA Si',CASE VACUCONTSARA WHEN '2' THEN 'X' END AS 'VACUCONTSARA no', CASE VACUCONTSARA WHEN '3' THEN 'X' END AS 'VACUCONTSARA descono',
	CASE VACURUBEOLA WHEN '1' THEN 'X' END AS 'VACURUBEOLA Si',CASE VACURUBEOLA WHEN '2' THEN 'X' END AS 'VACURUBEOLA no', CASE VACURUBEOLA WHEN '3' THEN 'X' END AS 'VACURUBEOLA descono',
	CASE TOS WHEN '1' THEN 'X' END AS 'TOS Si',CASE TOS WHEN '2' THEN 'X' END AS 'TOS no', CASE TOS WHEN '3' THEN 'X' END AS 'TOS descono',
	CASE CORIZA WHEN '1' THEN 'X' END AS 'CORIZA Si',CASE CORIZA WHEN '2' THEN 'X' END AS 'CORIZA no', CASE CORIZA WHEN '3' THEN 'X' END AS 'CORIZA descono',
	CASE CONJUNTIVI WHEN '1' THEN 'X' END AS 'CONJUNTIVI Si',CASE CONJUNTIVI WHEN '2' THEN 'X' END AS 'CONJUNTIVI no', CASE CONJUNTIVI WHEN '3' THEN 'X' END AS 'CONJUNTIVI descono',
	CASE ADENOPATIA WHEN '1' THEN 'X' END AS 'ADENOPATIA Si',CASE ADENOPATIA WHEN '2' THEN 'X' END AS 'ADENOPATIA no', CASE ADENOPATIA WHEN '3' THEN 'X' END AS 'ADENOPATIA descono',
	CASE ARTRALGIA WHEN '1' THEN 'X' END AS 'ARTRALGIA Si',CASE ARTRALGIA WHEN '2' THEN 'X' END AS 'ARTRALGIA no', CASE ARTRALGIA WHEN '3' THEN 'X' END AS 'ARTRALGIA descono',
	CASE EMBARAZADA WHEN '1' THEN 'X' END AS 'EMBARAZADA Si',CASE EMBARAZADA WHEN '2' THEN 'X' END AS 'EMBARAZADA no', CASE EMBARAZADA WHEN '3' THEN 'X' END AS 'EMBARAZADA descono',
	CASE VIAJODURANT WHEN '1' THEN 'X' END AS 'VIAJODURANT Si',CASE VIAJODURANT WHEN '2' THEN 'X' END AS 'VIAJODURANT no', CASE VIAJODURANT WHEN '3' THEN 'X' END AS 'VIAJODURANT descono',
	CASE TUVOCONTAC WHEN '1' THEN 'X' END AS 'TUVOCONTAC Si',CASE TUVOCONTAC WHEN '2' THEN 'X' END AS 'TUVOCONTAC no', CASE TUVOCONTAC WHEN '3' THEN 'X' END AS 'TUVOCONTAC descono',
	CASE HUBOVACU WHEN '1' THEN 'X' END AS 'HUBOVACU Si',CASE HUBOVACU WHEN '2' THEN 'X' END AS 'HUBOVACU no', CASE HUBOVACU WHEN '3' THEN 'X' END AS 'HUBOVACU descono',
	CASE HUBOMONITOR WHEN '1' THEN 'X' END AS 'HUBOMONITOR Si',CASE HUBOMONITOR WHEN '2' THEN 'X' END AS 'HUBOMONITOR no', CASE HUBOMONITOR WHEN '3' THEN 'X' END AS 'HUBOMONITOR descono',
	CASE SEHIZOSEGUI WHEN '1' THEN 'X' END AS 'SEHIZOSEGUI Si',CASE SEHIZOSEGUI WHEN '2' THEN 'X' END AS 'SEHIZOSEGUI no', CASE SEHIZOSEGUI WHEN '3' THEN 'X' END AS 'SEHIZOSEGUI descono',
	CASE CASODETECT WHEN '1' THEN 'X' END AS 'Consulta',CASE CASODETECT WHEN '2' THEN 'X' END AS 'Laboratorio', CASE CASODETECT WHEN '3' THEN 'X' END AS 'BusqActivInstitu',
	CASE CASODETECT WHEN '4' THEN 'X' END AS 'BusqActivComuni',CASE CASODETECT WHEN '5' THEN 'X' END AS 'InvestiContact', CASE CASODETECT WHEN '6' THEN 'X' END AS 'Comunidad',
	CASE CASODETECT WHEN '7' THEN 'X' END AS 'Otros',CASE CASODETECT WHEN '8' THEN 'X' END AS 'Desconocido',
	CASE FUENTE1 WHEN '1' THEN 'X' END AS 'FUENTE1 Si',CASE FUENTE1 WHEN '2' THEN 'X' END AS 'FUENTE1 no', CASE FUENTE1 WHEN '3' THEN 'X' END AS 'FUENTE1 descono',
	CASE FUENTE2 WHEN '1' THEN 'X' END AS 'FUENTE2 Si',CASE FUENTE2 WHEN '2' THEN 'X' END AS 'FUENTE2 no', CASE FUENTE2 WHEN '3' THEN 'X' END AS 'FUENTE2 descono',
	CASE TIPOVACU1 WHEN '1' THEN 'X' END AS 'TIPOVACU1 Si',CASE TIPOVACU1 WHEN '2' THEN 'X' END AS 'TIPOVACU1 no', CASE TIPOVACU1 WHEN '3' THEN 'X' END AS 'TIPOVACU1 descono',
	CASE TIPOVACU2 WHEN '1' THEN 'X' END AS 'TIPOVACU2 Si',CASE TIPOVACU2 WHEN '2' THEN 'X' END AS 'TIPOVACU2 no', CASE TIPOVACU2 WHEN '3' THEN 'X' END AS 'TIPOVACU2 descono',
	CASE TIPOERUP WHEN '1' THEN 'X' END AS 'Maculo',CASE TIPOERUP WHEN '2' THEN 'X' END AS 'Vesicular', CASE TIPOERUP WHEN '3' THEN 'X' END AS 'TIPOERUP Otro',
	CASE TIPOERUP WHEN '4' THEN 'X' END AS 'TIPOERUP desconocido',
	CASE HUBOCONTAC WHEN '1' THEN 'X' END AS 'HUBOCONTAC saramp',CASE HUBOCONTAC WHEN '2' THEN 'X' END AS 'HUBOCONTAC rubeola', CASE HUBOCONTAC WHEN '3' THEN 'X' END AS 'HUBOCONTAC ambos',
	CASE HUBOCONTAC WHEN '4' THEN 'X' END AS 'HUBOCONTAC ninguno',CASE HUBOCONTAC WHEN '5' THEN 'X' END AS 'HUBOCONTAC descono',
	CASE HUBOCASOCON WHEN '1' THEN 'X' END AS 'HUBOCASOCON saramp',CASE HUBOCASOCON WHEN '2' THEN 'X' END AS 'HUBOCASOCON rubeola', CASE HUBOCASOCON WHEN '3' THEN 'X' END AS 'HUBOCASOCON ambos',
	CASE HUBOCASOCON WHEN '4' THEN 'X' END AS 'HUBOCASOCON ninguno',CASE HUBOCASOCON WHEN '5' THEN 'X' END AS 'HUBOCASOCON descono',
	CASE CASODESCART WHEN '1' THEN 'X' END AS 'LaboNegat',CASE CASODESCART WHEN '2' THEN 'X' END AS 'ReaccioVacunal', CASE CASODESCART WHEN '3' THEN 'X' END AS 'Dengue',
	CASE CASODESCART WHEN '4' THEN 'X' END AS 'Parvovirus',CASE CASODESCART WHEN '5' THEN 'X' END AS 'Herpes', CASE CASODESCART WHEN '6' THEN 'X' END AS 'ReaccionAlergica',
	CASE CASODESCART WHEN '7' THEN 'X' END AS 'Otrodiagnostico',
	CASE CASOCONFIR WHEN '1' THEN 'X' END AS 'Importado',CASE CASOCONFIR WHEN '2' THEN 'X' END AS 'RelacioImporta', CASE CASOCONFIR WHEN '3' THEN 'X' END AS 'FuenteDescono',
	CASE CASOCONFIR WHEN '4' THEN 'X' END AS 'Autoctono',
	CASE MUESTRA1 WHEN '1' THEN 'Orina' WHEN '2' THEN 'Hisopado Nasofaringeo' WHEN '3' THEN 'Aspirado Nasofaringeo' WHEN '4' THEN 'Suero' END AS 'MUESTRA1',
	CASE MUESTRA2 WHEN '1' THEN 'Orina' WHEN '2' THEN 'Hisopado Nasofaringeo' WHEN '3' THEN 'Aspirado Nasofaringeo' WHEN '4' THEN 'Suero' END AS 'MUESTRA2',
	CASE MUESTRA3 WHEN '1' THEN 'Orina' WHEN '2' THEN 'Hisopado Nasofaringeo' WHEN '3' THEN 'Aspirado Nasofaringeo' WHEN '4' THEN 'Suero' END AS 'MUESTRA3',
	CASE MUESTRA4 WHEN '1' THEN 'Orina' WHEN '2' THEN 'Hisopado Nasofaringeo' WHEN '3' THEN 'Aspirado Nasofaringeo' WHEN '4' THEN 'Suero' END AS 'MUESTRA4',
	CASE PRUEBA1  WHEN '1' THEN 'IgM' WHEN '2' THEN 'IgG' WHEN '3' THEN 'PCR' WHEN '4' THEN'Elisa' WHEN '5' THEN 'Aislamiento viral'  WHEN '6' THEN 'Pruebas genotípicas' END AS 'PRUEBA1',
	CASE PRUEBA2  WHEN '1' THEN 'IgM' WHEN '2' THEN 'IgG' WHEN '3' THEN 'PCR' WHEN '4' THEN'Elisa' WHEN '5' THEN 'Aislamiento viral'  WHEN '6' THEN 'Pruebas genotípicas' END AS 'PRUEBA2',
	CASE PRUEBA3  WHEN '1' THEN 'IgM' WHEN '2' THEN 'IgG' WHEN '3' THEN 'PCR' WHEN '4' THEN'Elisa' WHEN '5' THEN 'Aislamiento viral'  WHEN '6' THEN 'Pruebas genotípicas' END AS 'PRUEBA3',
	CASE PRUEBA4  WHEN '1' THEN 'IgM' WHEN '2' THEN 'IgG' WHEN '3' THEN 'PCR' WHEN '4' THEN'Elisa' WHEN '5' THEN 'Aislamiento viral'  WHEN '6' THEN 'Pruebas genotípicas' END AS 'PRUEBA4',
	CASE AGENTE1  WHEN '1' THEN 'Sarampión' WHEN '2' THEN 'Rubéola' WHEN '3' THEN 'Dengue' WHEN '4' THEN 'Citomegalovirus' WHEN '5' THEN 'Herpes virus'
	WHEN '6' THEN 'Parvovirus' WHEN '7' THEN 'Chikungunya' END AS 'AGENTE1',
	CASE AGENTE2  WHEN '1' THEN 'Sarampión' WHEN '2' THEN 'Rubéola' WHEN '3' THEN 'Dengue' WHEN '4' THEN 'Citomegalovirus' WHEN '5' THEN 'Herpes virus'
	WHEN '6' THEN 'Parvovirus' WHEN '7' THEN 'Chikungunya' END AS 'AGENTE2',
	CASE AGENTE3  WHEN '1' THEN 'Sarampión' WHEN '2' THEN 'Rubéola' WHEN '3' THEN 'Dengue' WHEN '4' THEN 'Citomegalovirus' WHEN '5' THEN 'Herpes virus'
	WHEN '6' THEN 'Parvovirus' WHEN '7' THEN 'Chikungunya' END AS 'AGENTE3',
	CASE AGENTE4  WHEN '1' THEN 'Sarampión' WHEN '2' THEN 'Rubéola' WHEN '3' THEN 'Dengue' WHEN '4' THEN 'Citomegalovirus' WHEN '5' THEN 'Herpes virus'
	WHEN '6' THEN 'Parvovirus' WHEN '7' THEN 'Chikungunya' END AS 'AGENTE4',
	CASE RESULTADO1  WHEN '1' THEN 'Positivo' WHEN '2' THEN 'Negativo' WHEN '3' THEN 'No procesado' WHEN '4' THEN 'Inadecuado' WHEN '5' THEN 'Dudoso' WHEN '6' THEN 'Valor Registrado' END AS 'RESULTADO1',
	CASE RESULTADO2  WHEN '1' THEN 'Positivo' WHEN '2' THEN 'Negativo' WHEN '3' THEN 'No procesado' WHEN '4' THEN 'Inadecuado' WHEN '5' THEN 'Dudoso' WHEN '6' THEN 'Valor Registrado' END AS 'RESULTADO2',
	CASE RESULTADO3  WHEN '1' THEN 'Positivo' WHEN '2' THEN 'Negativo' WHEN '3' THEN 'No procesado' WHEN '4' THEN 'Inadecuado' WHEN '5' THEN 'Dudoso' WHEN '6' THEN 'Valor Registrado' END AS 'RESULTADO3',
	CASE RESULTADO4  WHEN '1' THEN 'Positivo' WHEN '2' THEN 'Negativo' WHEN '3' THEN 'No procesado' WHEN '4' THEN 'Inadecuado' WHEN '5' THEN 'Dudoso' WHEN '6' THEN 'Valor Registrado' END AS 'RESULTADO4',
	VERSION AS 'VERSION', JSON AS 'JSON',
	CASE RTRIM( JSON_VALUE(JSON,'$.SOSPECHAMISC') ) WHEN 'True' THEN 'X' END AS 'SOSPECHAMISCSI', CASE RTRIM( JSON_VALUE(JSON,'$.SOSPECHAMISC') ) WHEN 'False' THEN 'X' END AS 'SOSPECHAMISCNO',
	CASE RTRIM( JSON_VALUE(JSON,'$.FUENTECONTAGIO') ) 
	WHEN 1 THEN 'Contacto en casa'
	WHEN 2 THEN 'Comunidad' 
	WHEN 3 THEN 'Centro de salud' 
	WHEN 4 THEN 'Otros' 
	WHEN 5 THEN 'Desconocido' 
	END AS 'FUENTECONTAGIO',
	format(CONVERT(date,RTRIM(JSON_VALUE(JSON,'$.FECHASEGUI'))),'dd/MM/yyyy') As 'FECHASEGUI',
	CASE RTRIM( JSON_VALUE(JSON,'$.RTPCR') ) WHEN 'True' THEN 'X' END AS 'RTPCRSI', CASE RTRIM( JSON_VALUE(JSON,'$.RTPCR') ) WHEN 'False' THEN 'X' END AS 'RTPCRNO',
	CASE RTRIM( JSON_VALUE(JSON,'$.IGMIGG') ) WHEN 'True' THEN 'X' END AS 'IGMIGGSI', CASE RTRIM( JSON_VALUE(JSON,'$.IGMIGG') ) WHEN 'False' THEN 'X' END AS 'IGMIGGNO',
	CASE RTRIM( JSON_VALUE(JSON,'$.NEXOCOVID') ) WHEN 'True' THEN 'X' END AS 'NEXOCOVIDSI', CASE RTRIM( JSON_VALUE(JSON,'$.NEXOCOVID') ) WHEN 'False' THEN 'X' END AS 'NEXOCOVIDNO',
	CASE RTRIM( JSON_VALUE(JSON,'$.SINTOMASINI') ) 
	WHEN 1 THEN 'Vomito'
	WHEN 2 THEN 'Diarrea' 
	WHEN 3 THEN 'Edema de mucosas' 
	WHEN 4 THEN 'Alteraciones del estado de conciencia' 
	END AS 'SINTOMASINI',
	CASE RTRIM( JSON_VALUE(JSON,'$.FIBRINOGESI') ) WHEN 'True' THEN 'X' END AS 'FIBRINOGENOSI', CASE RTRIM( JSON_VALUE(JSON,'$.FIBRINOGENO') ) WHEN 'False' THEN 'X' END AS 'FIBRINOGENONO',
	CASE RTRIM( JSON_VALUE(JSON,'$.PROTEINAC') ) WHEN 'True' THEN 'X' END AS 'PROTEINACSI', CASE RTRIM( JSON_VALUE(JSON,'$.PROTEINAC') ) WHEN 'False' THEN 'X' END AS 'PROTEINACNO',
	CASE RTRIM( JSON_VALUE(JSON,'$.FERRITINA') ) WHEN 'True' THEN 'X' END AS 'FERRITINASI', CASE RTRIM( JSON_VALUE(JSON,'$.FERRITINA') ) WHEN 'False' THEN 'X' END AS 'FERRITINANO',
	CASE RTRIM( JSON_VALUE(JSON,'$.DIMEROD') ) WHEN 'True' THEN 'X' END AS 'DIMERODSI', CASE RTRIM( JSON_VALUE(JSON,'$.DIMEROD') ) WHEN 'False' THEN 'X' END AS 'DIMERODNO',
	CASE RTRIM( JSON_VALUE(JSON,'$.LINFOPENIA') ) WHEN 'True' THEN 'X' END AS 'LINFOPENIASI', CASE RTRIM( JSON_VALUE(JSON,'$.LINFOPENIA') ) WHEN 'False' THEN 'X' END AS 'LINFOPENIANO',
	RTRIM( JSON_VALUE(JSON,'$.VALOR1') ) As 'VALOR1', RTRIM( JSON_VALUE(JSON,'$.VALOR2') ) As 'VALOR2', RTRIM( JSON_VALUE(JSON,'$.VALOR3') ) As 'VALOR3',
	RTRIM( JSON_VALUE(JSON,'$.VALOR4') ) As 'VALOR4', RTRIM( JSON_VALUE(JSON,'$.VALOR5') ) As 'VALOR5', 
	RTRIM( JSON_VALUE(JSON,'$.CODPAIS_IMPORTADO') ) As 'CODPAIS_IMPORTADO' 

	From HCFICHA710 where IDFICHANOTIFICACION  = @IdFicha
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle completo de una ficha SIVIGILA de tipo 710 (vigilancia epidemiológica de sarampión y rubéola) a partir del identificador interno de la ficha y su número. Consulta la tabla HCFICHA710 para obtener datos clínicos y epidemiológicos del caso notificado, incluyendo: nombre y ocupación del padre o acudiente, dirección del trabajo, dosis de vacunas aplicadas (sarampión, rubéola), diagnóstico inicial, fechas de inicio de fiebre y erupción, duración de la erupción, número de semanas de gestación, lugar probable de infección, fechas de toma y recepción de muestras de laboratorio, fechas de resultados, síntomas presentes (tos, coriza, conjuntivitis, adenopatía, artralgia), estado de embarazo, antecedentes de viaje, contactos con casos, historial de vacunación, tipo de erupción, forma de detección del caso y fuentes de información. Existe para alimentar el reporte oficial de notificación SIVIGILA que las IPS deben entregar al sistema de vigilancia en salud pública de Colombia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila710';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila710';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea para impresión/visualización los datos de la ficha de notificación Sivigila 710 (sarampión/rubéola y variantes), traduciendo códigos a etiquetas y marcando casillas tipo ''X''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila710';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA710 con IDFICHANOTIFICACION igual al identificador suministrado; El campo JSON debe contener una estructura JSON válida para que JSON_VALUE retorne valores correctos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila710';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las fechas se devuelven en formato dd/MM/yyyy (estilo 103 o format dd/MM/yyyy); Los valores de texto se devuelven sin espacios finales (RTRIM); Las casillas de selección retornan ''X'' o NULL, nunca otro valor; El procedimiento solo lee datos; no modifica HCFICHA710 ni otras tablas; La consulta filtra exclusivamente por IDFICHANOTIFICACION (el parámetro NumFicha se recibe pero no se usa en el WHERE); Los códigos numéricos almacenados se mapean a etiquetas en español según catálogos fijos del Sivigila', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila710';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Sivigila; Ficha de notificación 710; Sarampión; Rubéola; Vacunación (dosis); Síntomas (tos, coriza, conjuntivitis, adenopatía, artralgia, erupción, fiebre); Embarazo; Contacto epidemiológico; Tipo de muestra (orina, hisopado/aspirado nasofaríngeo, suero); Pruebas de laboratorio (IgM, IgG, PCR, Elisa, aislamiento viral, genotípicas); Agentes etiológicos (Dengue, Citomegalovirus, Herpes, Parvovirus, Chikungunya); Clasificación de caso (importado, autóctono, descartado, confirmado); Síndrome inflamatorio multisistémico (MIS-C) asociado a COVID; Marcadores inflamatorios (fibrinógeno, proteína C, ferritina, dímero D, linfopenia); Seguimiento epidemiológico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila710';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA710: WHERE IDFICHANOTIFICACION = @IdFicha → devuelve un único conjunto de resultados con los datos de la ficha 710 transformados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila710';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si VACUCONTSARA / VACURUBEOLA / TOS / CORIZA / CONJUNTIVI / ADENOPATIA / ARTRALGIA / EMBARAZADA / VIAJODURANT / TUVOCONTAC / HUBOVACU / HUBOMONITOR / SEHIZOSEGUI / FUENTE1 / FUENTE2 / TIPOVACU1 / TIPOVACU2 = ''1''/''2''/''3'' → Marca con ''X'' la casilla Si / No / Desconocido respectivamente; si CASODETECT = ''1''..''8'' → Marca ''X'' en una de las casillas: Consulta, Laboratorio, BúsquedaActivaInstitucional, BúsquedaActivaComunitaria, InvestigaciónContactos, Comunidad, Otros, Desconocido; si TIPOERUP = ''1''..''4'' → Clasifica la erupción como Maculo, Vesicular, Otro o Desconocido; si HUBOCONTAC / HUBOCASOCON = ''1''..''5'' → Indica el tipo de contacto/caso conocido: sarampión, rubéola, ambos, ninguno o desconocido; si CASODESCART = ''1''..''7'' → Clasifica el caso descartado como LaboratorioNegativo, ReacciónVacunal, Dengue, Parvovirus, Herpes, ReacciónAlérgica u OtroDiagnóstico; si CASOCONFIR = ''1''..''4'' → Clasifica el caso confirmado como Importado, Relacionado con importado, Fuente desconocida o Autóctono; si MUESTRA1..4 = ''1''..''4'' → Traduce código a tipo de muestra: Orina, Hisopado Nasofaríngeo, Aspirado Nasofaríngeo o Suero; si PRUEBA1..4 = ''1''..''6'' → Traduce a tipo de prueba: IgM, IgG, PCR, Elisa, Aislamiento viral o Pruebas genotípicas; si AGENTE1..4 = ''1''..''7'' → Traduce a agente etiológico: Sarampión, Rubéola, Dengue, Citomegalovirus, Herpes virus, Parvovirus o Chikungunya; si RESULTADO1..4 = ''1''..''6'' → Traduce a resultado de laboratorio: Positivo, Negativo, No procesado, Inadecuado, Dudoso o Valor Registrado; si JSON.SOSPECHAMISC / RTPCR / IGMIGG / NEXOCOVID / FIBRINOGESI / PROTEINAC / FERRITINA / DIMEROD / LINFOPENIA = ''True''/''False'' → Marca con ''X'' la casilla SI o NO correspondiente; si JSON.FUENTECONTAGIO = 1..5 → Traduce a: Contacto en casa, Comunidad, Centro de salud, Otros o Desconocido; si JSON.SINTOMASINI = 1..4 → Traduce a: Vómito, Diarrea, Edema de mucosas o Alteraciones del estado de conciencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila710';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA710', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila710';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila710';
-- GO
