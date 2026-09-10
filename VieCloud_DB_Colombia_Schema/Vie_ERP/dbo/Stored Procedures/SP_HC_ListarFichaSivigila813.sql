-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,09-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila813]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON; 
		
			Select
				CASE CONDICION WHEN '1' THEN 'X' END AS 'Sensible', CASE CONDICION WHEN '0' THEN 'X' END AS 'Resistente',
				CASE TIPOTUBER WHEN '1' THEN 'X' END AS 'Pulmonar', CASE TIPOTUBER WHEN '0' THEN 'X' END AS 'ExtraPulmonar',
				CASE LOCALTUBER WHEN '1' THEN 'X' END AS 'Pleural', CASE LOCALTUBER WHEN '2' THEN 'X' END AS 'Meningea', CASE LOCALTUBER WHEN '3' THEN 'X' END AS 'Peritoneal',
				CASE LOCALTUBER WHEN '4' THEN 'X' END AS 'Ganglional', CASE LOCALTUBER WHEN '5' THEN 'X' END AS 'Renal', CASE LOCALTUBER WHEN '6' THEN 'X' END AS 'Intestinal',
				CASE LOCALTUBER WHEN '7' THEN 'X' END AS 'OsteoArticular', CASE LOCALTUBER WHEN '8' THEN 'X' END AS 'Genitourinaria', CASE LOCALTUBER WHEN '9' THEN 'X' END AS 'Pericardica',
				CASE LOCALTUBER WHEN '10' THEN 'X' END AS 'Cutanea', CASE LOCALTUBER WHEN '11' THEN 'X' END AS 'Otro',
				CASE SEGUANTECE WHEN '1' THEN 'X' END AS 'Nuevo', CASE SEGUANTECE WHEN '0' THEN 'X' END AS 'Previamente Tratado',
				CASE PREVTRAT WHEN '1' THEN 'X' END AS 'TrasRecai', CASE PREVTRAT WHEN '2' THEN 'X' END AS 'TrasFrac', CASE PREVTRAT WHEN '3' THEN 'X' END AS 'PaciRecu',
				CASE PREVTRAT WHEN '4' THEN 'X' END AS 'OtrosPaci', CASE PREVTRAT WHEN '5' THEN 'X' END AS 'Trat1rali', CASE PREVTRAT WHEN '6' THEN 'X' END AS 'trat2dali',
				CASE PACTRASAL WHEN '1' THEN 'X' END AS 'PACTRASAL si', CASE PACTRASAL WHEN '0' THEN 'X' END AS 'PACTRASAL no',
				Rtrim(OCUPACION) As 'OCUPACION', Rtrim(PESOACT) As 'PESOACT', Rtrim(TALLAACT) As 'TALLAACT', Rtrim(IMC) As 'IMC',
				CASE PACCUENDIA WHEN '1' THEN 'X' END AS 'PACCUENDIA si', CASE PACCUENDIA WHEN '0' THEN 'X' END AS 'PACCUENDIA no',
				CASE BACILOS WHEN '1' THEN 'X' END AS 'BACILOS si', CASE BACILOS WHEN '0' THEN 'X' END AS 'BACILOS no',
				CASE RESULBACI WHEN '1' THEN 'X' END AS 'RESULBACI si', CASE RESULBACI WHEN '0' THEN 'X' END AS 'RESULBACI no',
				CASE CULTIVO WHEN '1' THEN 'X' END AS 'CULTIVO si', CASE CULTIVO WHEN '0' THEN 'X' END AS 'CULTIVO no',
				CASE RESUCULT WHEN '1' THEN 'X' END AS 'RESUCULT si', CASE RESUCULT WHEN '2' THEN 'X' END AS 'RESUCULT no', CASE RESUCULT WHEN '3' THEN 'X' END AS 'RESUCULT enproc',
				CASE PRUEMOL WHEN '1' THEN 'X' END AS 'PRUEMOL si', CASE PRUEMOL WHEN '0' THEN 'X' END AS 'PRUEMOL no',
				CASE RESULPRUEMOL WHEN '1' THEN 'X' END AS 'RESULPRUEMOL si', CASE RESULPRUEMOL WHEN '0' THEN 'X' END AS 'RESULPRUEMOL no',
				CASE NOMESPIDENT WHEN '1' THEN 'X' END AS 'mycoTuberculosis', CASE NOMESPIDENT WHEN '2' THEN 'X' END AS 'mycobovis', CASE NOMESPIDENT WHEN '3' THEN 'X' END AS 'mycoafrica',
				CASE NOMESPIDENT WHEN '4' THEN 'X' END AS 'mycomicroti', CASE NOMESPIDENT WHEN '5' THEN 'X' END AS 'mycocanet',
				CASE HISPATO WHEN '1' THEN 'X' END AS 'HISPATO si', CASE HISPATO WHEN '0' THEN 'X' END AS 'HISPATO no',
				CASE RESUHISPAT WHEN '1' THEN 'X' END AS 'RESUHISPAT si', CASE RESUHISPAT WHEN '0' THEN 'X' END AS 'RESUHISPAT no',
				CASE RESPRUSENS WHEN '1' THEN 'X' END AS 'RESPRUSENS si', CASE RESPRUSENS WHEN '0' THEN 'X' END AS 'RESPRUSENS no',
				CASE CUADCLI WHEN '1' THEN 'X' END AS 'CUADCLI si', CASE CUADCLI WHEN '0' THEN 'X' END AS 'CUADCLI no',
				CASE NEXOEPI WHEN '1' THEN 'X' END AS 'NEXOEPI si', CASE NEXOEPI WHEN '0' THEN 'X' END AS 'NEXOEPI no',
				CASE RADIOL WHEN '1' THEN 'X' END AS 'RADIOL si', CASE RADIOL WHEN '0' THEN 'X' END AS 'RADIOL no',
				CASE ADA WHEN '1' THEN 'X' END AS 'ADA si', CASE ADA WHEN '0' THEN 'X' END AS 'ADA no',
				CASE TUBERCULINA WHEN '1' THEN 'X' END AS 'TUBERCULINA si', CASE TUBERCULINA WHEN '0' THEN 'X' END AS 'TUBERCULINA no',
				CASE COOMOR WHEN '1' THEN 'X' END AS 'Diabetes', CASE COOMOR WHEN '2' THEN 'X' END AS 'Silicosis', CASE COOMOR WHEN '3' THEN 'X' END AS 'EnfRenal',
				CASE COOMOR WHEN '4' THEN 'X' END AS 'EPOC', CASE COOMOR WHEN '5' THEN 'X' END AS 'EnfHepat', CASE COOMOR WHEN '6' THEN 'X' END AS 'Cancer',
				CASE COOMOR WHEN '7' THEN 'X' END AS 'Artritis', CASE COOMOR WHEN '8' THEN 'X' END AS 'Desnutricion',
				convert(varchar(10),FECHACONFIR,103) As 'FECHACONFIR',
				CASE MONORES WHEN '1' THEN 'X' END AS 'MONORES', CASE MDR WHEN '1' THEN 'X' END AS 'MDR', CASE POLIRES WHEN '1' THEN 'X' END AS 'POLIRES',
				CASE XDR WHEN '1' THEN 'X' END AS 'XDR', CASE RESRIFAMP WHEN '1' THEN 'X' END AS 'RESRIFAMP', CASE RESPREXDR WHEN '1' THEN 'X' END AS 'RESPREXDR',
				CASE ESTREP1 WHEN '1' THEN 'X' END AS 'ESTREP1-1', CASE ESTREP1 WHEN '2' THEN 'X' END AS 'ESTREP1-2', CASE ESTREP1 WHEN '3' THEN 'X' END AS 'ESTREP1-3',
				CASE ISONIA1 WHEN '1' THEN 'X' END AS 'ISONIA1-1', CASE ISONIA1 WHEN '2' THEN 'X' END AS 'ISONIA1-2', CASE ISONIA1 WHEN '3' THEN 'X' END AS 'ISONIA1-3',
				CASE ETAMBU1 WHEN '1' THEN 'X' END AS 'ETAMBU1-1', CASE ETAMBU1 WHEN '2' THEN 'X' END AS 'ETAMBU1-2', CASE ETAMBU1 WHEN '3' THEN 'X' END AS 'ETAMBU1-3',
				CASE PIRAZI1 WHEN '1' THEN 'X' END AS 'PIRAZI1-1', CASE PIRAZI1 WHEN '2' THEN 'X' END AS 'PIRAZI1-2', CASE PIRAZI1 WHEN '3' THEN 'X' END AS 'PIRAZI1-3',
				CASE ISONIA2 WHEN '1' THEN 'X' END AS 'ISONIA2-2', CASE RIFAMP1 WHEN '1' THEN 'X' END AS 'RIFAMP1-2', CASE ESTREP2 WHEN '1' THEN 'X' END AS 'ESTREP2-2',
				CASE ISONIA3 WHEN '1' THEN 'X' END AS 'ISONIA3-2', CASE ETAMBU2 WHEN '1' THEN 'X' END AS 'ETAMBU2-2', CASE PIRAZI2 WHEN '1' THEN 'X' END AS 'PIRAZI2-2',
				CASE QUINDO1 WHEN '1' THEN 'X' END AS 'QUINDO1-2', CASE INYECT1 WHEN '1' THEN 'X' END AS 'INYECT1-2', CASE RIFAMP2 WHEN '1' THEN 'X' END AS 'RIFAMP2-2',
				CASE QUINDO2 WHEN '1' THEN 'X' END AS 'QUINDO2-1', CASE QUINDO2 WHEN '2' THEN 'X' END AS 'QUINDO2-2',
				CASE INYECT2 WHEN '1' THEN 'X' END AS 'INYECT2-1', CASE INYECT2 WHEN '2' THEN 'X' END AS 'INYECT2-2', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON' ,				 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.ENPROCESOCLASIFICA') AS BIT) WHEN 1 THEN 'X' END AS 'EnProcesoClasificacion',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.CLASIFICACION_ESTADO_PRUEBA_VIH') AS INT) WHEN 1 THEN 'X' END AS 'Persona_tb_vih',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.CLASIFICACION_ESTADO_PRUEBA_VIH') AS INT) WHEN 2 THEN 'X' END AS 'Persona_tb_sin_vih',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.CLASIFICACION_ESTADO_PRUEBA_VIH') AS INT) WHEN 3 THEN 'X' END AS 'Persona_tb_vih_desc',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.RESULTADO_PRUEBA_SENSIBILIDAD_FARMACOS') AS INT) WHEN 1 THEN 'X' END AS 'Positivo',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.RESULTADO_PRUEBA_SENSIBILIDAD_FARMACOS') AS INT) WHEN 2 THEN 'X' END AS 'Negativo',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.RESULTADO_PRUEBA_SENSIBILIDAD_FARMACOS') AS INT) WHEN 3 THEN 'X' END AS 'No_se_realizo',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.COMORBILIDAD_DIABETES') AS BIT) WHEN 1 THEN 'X' END AS 'Comor_diabetes',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.COMORBILIDAD_SILICOSIS') AS BIT) WHEN 1 THEN 'X' END AS 'Comor_silicosis',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.COMORBILIDAD_ENFERMEDAD_RENAL') AS BIT) WHEN 1 THEN 'X' END AS 'Comor_renal',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.COMORBILIDAD_EPOC') AS BIT) WHEN 1 THEN 'X' END AS 'Comor_epoc',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.COMORBILIDAD_ENFERMEDAD_HEPATICA') AS BIT) WHEN 1 THEN 'X' END AS 'Comor_hepatica',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.COMORBILIDAD_CANCER') AS BIT) WHEN 1 THEN 'X' END AS 'Comor_cancer',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.COMORBILIDAD_ARTRITIS_REUMATOIDE') AS BIT) WHEN 1 THEN 'X' END AS 'Comor_reumatoide',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.COMORBILIDAD_DESNUTRICION') AS BIT) WHEN 1 THEN 'X' END AS 'Comor_desnutricion',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.XDR_H_ISONIAZIDA') AS BIT) WHEN 1 THEN 'X' END AS 'Xdr_h_isoniazida',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.XDR_R_RIFAMPICINA') AS BIT) WHEN 1 THEN 'X' END AS 'Xdr_r_rifampicina',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.XDR_Lfx_LEVOFLOXACINA') AS BIT) WHEN 1 THEN 'X' END AS 'Xdr_lfx_levofloxacina',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.XDR_Mfx_MOXIFLOXACIANA') AS BIT) WHEN 1 THEN 'X' END AS 'Xdr_mfx_moxifloxaciana',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.XDR_Bdq_BEDAQUILINA') AS BIT) WHEN 1 THEN 'X' END AS 'Xdr_bdq_bedaquilina',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.XDR_Ldz_LINEZOLID') AS BIT) WHEN 1 THEN 'X' END AS 'Xdr_ldz_linezolid',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.PREXDR_H_ISONIAZIDA') AS BIT) WHEN 1 THEN 'X' END AS 'Prexdr_h_isoniazida',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.PREXDR_R_RIFAMPICINA') AS BIT) WHEN 1 THEN 'X' END AS 'Prexdr_r_rifampicina',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.PREXDR_Lfx_LEVOFLOXACINA') AS BIT) WHEN 1 THEN 'X' END AS 'Prexdr_lfx_levofloxacina',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.PREXDR_Mfx_MOXIFLOXACIANA') AS BIT) WHEN 1 THEN 'X' END AS 'Prexdr_mfx_moxifloxaciana',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.PREXDR_Bdq_BEDAQUILINA') AS BIT) WHEN 1 THEN 'X' END AS 'Prexdr_bdq_bedaquilina',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.PREXDR_Ldz_LINEZOLID') AS BIT) WHEN 1 THEN 'X' END AS 'Prexdr_ldz_linezolid',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.RESISTENCIAOTROMEDICAMENTO') AS BIT) WHEN 1 THEN 'X' END AS 'Resis_otro_medicam',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.OTRO_MEDICAMENTOS_CLOFAZIMINA') AS INT) WHEN 1 THEN 'X' END AS 'Otro_medica_clofazi1',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.OTRO_MEDICAMENTOS_CLOFAZIMINA') AS INT) WHEN 2 THEN 'X' END AS 'Otro_medica_clofazi2',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.OTRO_MEDICAMENTOS_CLOFAZIMINA') AS INT) WHEN 3 THEN 'X' END AS 'Otro_medica_clofazi3',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.OTRO_MEDICAMENTOS_DELAMANID') AS INT) WHEN 1 THEN 'X' END AS 'Otro_medica_delam1',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.OTRO_MEDICAMENTOS_DELAMANID') AS INT) WHEN 2 THEN 'X' END AS 'Otro_medica_delam2',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.OTRO_MEDICAMENTOS_DELAMANID') AS INT) WHEN 3 THEN 'X' END AS 'Otro_medica_delam3'
			FROM 
				HCFICHA813 
	
			WHERE 
				IDFICHANOTIFICACION  = @IdFicha
				AND ISJSON(JSON) > 0

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera y formatea el detalle completo de la ficha SIVIGILA número 813 de tuberculosis (TB) correspondiente a un paciente, a partir de su identificador interno y número de ficha. Traduce todos los campos codificados de la tabla HCFICHA813 a etiquetas legibles de negocio: tipo y localización de la tuberculosis (pulmonar, extrapulmonar y sus subtipos), condición de sensibilidad o resistencia, antecedentes de tratamiento previo, resultados de pruebas diagnósticas (baciloscopia, cultivo, prueba molecular, histopatología, tuberculina, ADA, radiología), comorbilidades (diabetes, silicosis, cáncer, desnutrición, entre otras), perfil de resistencia a medicamentos antituberculosos (monoresistencia, MDR, XDR, resistencia a isoniacida, rifampicina, etambutol, pirazinamida, quinolonas, inyectables, etc.) y fecha de confirmación del diagnóstico. Este procedimiento existe para alimentar la impresión o visualización de la ficha epidemiológica oficial de notificación de TB exigida por el SIVIGILA, marcando con ''X'' cada casilla según la clasificación clínica y epidemiológica del caso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila813';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila813';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta y proyecta los datos clínicos y epidemiológicos de una ficha Sivigila 813 (tuberculosis), traduciendo códigos numéricos y atributos de un campo JSON a marcas legibles para impresión/visualización del formato.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila813';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en la tabla de fichas cuyo identificador de notificación coincida con el parámetro recibido.; El campo JSON del registro debe contener un documento JSON válido para que la fila sea retornada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila813';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan fichas cuyo campo JSON es un JSON válido (ISJSON(JSON) > 0).; Cada indicador clínico se transforma a una marca ''X'' cuando el valor codificado coincide con el código esperado; en caso contrario el campo queda en NULL.; Las comorbilidades, esquemas de resistencia y medicamentos se exponen tanto desde columnas tradicionales como desde el JSON, permitiendo coexistencia de versiones de la ficha.; La fecha de confirmación se devuelve siempre formateada como dd/mm/yyyy (estilo 103).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila813';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Tuberculosis (sensible/resistente, pulmonar/extrapulmonar); Localización de tuberculosis (pleural, meníngea, peritoneal, ganglionar, renal, intestinal, osteoarticular, genitourinaria, pericárdica, cutánea); Antecedentes de tratamiento (recaída, fracaso, recuperado); Pruebas diagnósticas (baciloscopia, cultivo, prueba molecular, histopatología, ADA, tuberculina, radiología); Especies de Mycobacterium (tuberculosis, bovis, africanum, microti, canetti); Comorbilidades (diabetes, silicosis, enfermedad renal, EPOC, enfermedad hepática, cáncer, artritis reumatoide, desnutrición); Resistencia a fármacos (MONORES, MDR, POLIRES, XDR, pre-XDR, resistencia a rifampicina); Medicamentos antituberculosos (isoniazida, rifampicina, etambutol, pirazinamida, estreptomicina, levofloxacina, moxifloxacina, bedaquilina, linezolid, clofazimina, delamanid); Coinfección TB/VIH; Datos antropométricos (peso, talla, IMC); Ocupación del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila813';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA813: Cuando IDFICHANOTIFICACION coincide con el parámetro de entrada y el campo JSON es un JSON válido, retorna una fila con los indicadores de la ficha Sivigila 813 transformados a marcas ''X'' según los códigos almacenados y a partir de propiedades del JSON.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila813';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA813', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila813';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila813';
-- GO
