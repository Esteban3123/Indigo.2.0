-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,06-12-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila730]
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
	CASE RTRIM( JSON_VALUE(JSON,'$.FIBRINOGENO') ) WHEN 'True' THEN 'X' END AS 'FIBRINOGENOSI', CASE RTRIM( JSON_VALUE(JSON,'$.FIBRINOGENO') ) WHEN 'False' THEN 'X' END AS 'FIBRINOGENONO',
	CASE RTRIM( JSON_VALUE(JSON,'$.PROTEINAC') ) WHEN 'True' THEN 'X' END AS 'PROTEINACSI', CASE RTRIM( JSON_VALUE(JSON,'$.PROTEINAC') ) WHEN 'False' THEN 'X' END AS 'PROTEINACNO',
	CASE RTRIM( JSON_VALUE(JSON,'$.FERRITINA') ) WHEN 'True' THEN 'X' END AS 'FERRITINASI', CASE RTRIM( JSON_VALUE(JSON,'$.FERRITINA') ) WHEN 'False' THEN 'X' END AS 'FERRITINANO',
	CASE RTRIM( JSON_VALUE(JSON,'$.DIMEROD') ) WHEN 'True' THEN 'X' END AS 'DIMERODSI', CASE RTRIM( JSON_VALUE(JSON,'$.DIMEROD') ) WHEN 'False' THEN 'X' END AS 'DIMERODNO',
	CASE RTRIM( JSON_VALUE(JSON,'$.LINFOPENIA') ) WHEN 'True' THEN 'X' END AS 'LINFOPENIASI', CASE RTRIM( JSON_VALUE(JSON,'$.LINFOPENIA') ) WHEN 'False' THEN 'X' END AS 'LINFOPENIANO',
	RTRIM( JSON_VALUE(JSON,'$.VALOR1') ) As 'VALOR1', RTRIM( JSON_VALUE(JSON,'$.VALOR2') ) As 'VALOR2', RTRIM( JSON_VALUE(JSON,'$.VALOR3') ) As 'VALOR3',
	RTRIM( JSON_VALUE(JSON,'$.VALOR4') ) As 'VALOR4', RTRIM( JSON_VALUE(JSON,'$.VALOR5') ) As 'VALOR5', 
	RTRIM( JSON_VALUE(JSON,'$.CODPAIS_IMPORTADO') ) As 'CODPAIS_IMPORTADO' 

	From HCFICHA730 where IDFICHANOTIFICACION  = @IdFicha
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo de una ficha de notificación epidemiológica SIVIGILA código 730, correspondiente a casos sospechosos o confirmados de sarampión y rubéola. A partir del identificador de ficha (@IdFicha) y el número de ficha (@NumFicha), consulta la tabla HCFICHA730 y devuelve todos los campos clínicos y epidemiológicos requeridos por el sistema de vigilancia en salud pública: datos del padre o madre del paciente (nombre, ocupación, dirección de trabajo), antecedentes de vacunación contra sarampión y rubéola (número de dosis, tipo de vacuna, fecha de última dosis), síntomas presentes (fiebre, tos, coriza, conjuntivitis, adenopatía, artralgia, erupción cutánea con tipo y duración), condición de embarazo, historial de viajes y contactos con casos confirmados, fechas clave (inicio de fiebre, inicio de erupción, toma y recepción de muestras de laboratorio, resultados), mecanismo de detección del caso, fuente de información, lugar probable de infección y clasificación final del caso (incluido si es caso importado). Este procedimiento es usado por el módulo de historia clínica para imprimir o visualizar la ficha SIVIGILA 730 que debe diligenciarse y reportarse a las autoridades sanitarias en cumplimiento de la vigilancia epidemiológica obligatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila730';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila730';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea para presentación los datos de una ficha de notificación epidemiológica Sivigila 730 (sarampión/rubéola y variantes), traduciendo códigos a etiquetas legibles y extrayendo campos JSON adicionales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila730';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA730 con IDFICHANOTIFICACION igual al identificador recibido; La columna JSON debe contener un documento JSON válido para que JSON_VALUE retorne datos no nulos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila730';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las fechas se devuelven en formato dd/MM/yyyy (estilo 103) o formateadas como dd/MM/yyyy desde JSON; Los valores de texto se devuelven con RTRIM aplicado para eliminar espacios finales; Los códigos numéricos almacenados se exponen siempre como etiquetas descriptivas o como marca ''X'' en columnas mutuamente excluyentes; El segundo parámetro (NumFicha) no se usa en la consulta; el filtrado depende exclusivamente del identificador de ficha', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila730';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Sarampión; Rubéola; Vacunación (dosis y fechas); Erupción cutánea; Toma y resultado de muestras de laboratorio (IgM, IgG, PCR, Elisa, aislamiento viral, genotípicas); Tipos de muestra (orina, hisopado/aspirado nasofaríngeo, suero); Clasificación de caso (importado, relacionado con importado, autóctono, fuente desconocida); Descarte de caso (laboratorio negativo, reacción vacunal, dengue, parvovirus, herpes, reacción alérgica); Sospecha MISC (síndrome inflamatorio multisistémico) y nexo COVID; Marcadores de laboratorio COVID/MISC (fibrinógeno, proteína C, ferritina, dímero D, linfopenia); Embarazo; Seguimiento epidemiológico y visita domiciliaria; Contacto con caso; País de importación del caso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila730';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA730: Cuando IDFICHANOTIFICACION coincide con el parámetro recibido, devuelve una fila con campos de la ficha Sivigila 730 traducidos a etiquetas legibles y valores extraídos del JSON', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila730';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si VACUCONTSARA / VACURUBEOLA / TOS / CORIZA / CONJUNTIVI / ADENOPATIA / ARTRALGIA / EMBARAZADA / VIAJODURANT / TUVOCONTAC / HUBOVACU / HUBOMONITOR / SEHIZOSEGUI / FUENTE1 / FUENTE2 / TIPOVACU1 / TIPOVACU2 = ''1'' | ''2'' | ''3'' → Marca ''X'' en columna Si / no / desconocido respectivamente; si CASODETECT = ''1''..''8'' → Marca ''X'' en una de: Consulta, Laboratorio, BusqActivInstitu, BusqActivComuni, InvestiContact, Comunidad, Otros, Desconocido; si TIPOERUP = ''1''..''4'' → Marca ''X'' en Maculo / Vesicular / Otro / desconocido; si HUBOCONTAC / HUBOCASOCON = ''1''..''5'' → Marca ''X'' en sarampión / rubéola / ambos / ninguno / desconocido; si CASODESCART = ''1''..''7'' → Marca ''X'' en LaboNegat / ReaccioVacunal / Dengue / Parvovirus / Herpes / ReaccionAlergica / Otrodiagnostico; si CASOCONFIR = ''1''..''4'' → Marca ''X'' en Importado / RelacioImporta / FuenteDescono / Autoctono; si MUESTRA1..4 = ''1''..''4'' → Traduce a Orina / Hisopado Nasofaringeo / Aspirado Nasofaringeo / Suero; si PRUEBA1..4 = ''1''..''6'' → Traduce a IgM / IgG / PCR / Elisa / Aislamiento viral / Pruebas genotípicas; si AGENTE1..4 = ''1''..''7'' → Traduce a Sarampión / Rubéola / Dengue / Citomegalovirus / Herpes virus / Parvovirus / Chikungunya; si RESULTADO1..4 = ''1''..''6'' → Traduce a Positivo / Negativo / No procesado / Inadecuado / Dudoso / Valor Registrado; si JSON.SOSPECHAMISC / RTPCR / IGMIGG / NEXOCOVID / FIBRINOGENO / PROTEINAC / FERRITINA / DIMEROD / LINFOPENIA = ''True''/''False'' → Marca ''X'' en columna SI o NO correspondiente; si JSON.FUENTECONTAGIO = 1..5 → Traduce a Contacto en casa / Comunidad / Centro de salud / Otros / Desconocido; si JSON.SINTOMASINI = 1..4 → Traduce a Vomito / Diarrea / Edema de mucosas / Alteraciones del estado de conciencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila730';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA730', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila730';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila730';
-- GO
