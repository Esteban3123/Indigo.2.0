-- Stored Procedure

-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,27-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila345]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;

	Select
		CASE PRESECARNE  WHEN '1' THEN 'X' END AS 'PRESECARNE si',CASE PRESECARNE WHEN '0' THEN 'X' END AS 'PRESECARNE no',
		CASE USOANTIBIO  WHEN '1' THEN 'X' END AS 'USOANTIBIO si',CASE USOANTIBIO WHEN '0' THEN 'X' END AS 'USOANTIBIO no',
		CASE USOANTIVIRAL  WHEN '1' THEN 'X' END AS 'USOANTIVIRAL si',CASE USOANTIVIRAL WHEN '0' THEN 'X' END AS 'USOANTIVIRAL no',
		CASE SERVIHOSPITAL  WHEN '1' THEN 'X' END AS 'SERVIHOSPITAL si',CASE SERVIHOSPITAL WHEN '0' THEN 'X' END AS 'SERVIHOSPITAL no',
		CASE NEUMOCOCO WHEN '1' THEN 'X' END AS 'NEUMOCOCO Si',CASE NEUMOCOCO WHEN '2' THEN 'X' END AS 'NEUMOCOCO No', CASE NEUMOCOCO WHEN '3' THEN 'X' END AS 'NEUMOCOCO desco',
		CASE INFLUENZAESTACIO WHEN '1' THEN 'X' END AS 'INFLUENZAESTACIO Si',CASE INFLUENZAESTACIO WHEN '2' THEN 'X' END AS 'INFLUENZAESTACIO No', CASE INFLUENZAESTACIO WHEN '3' THEN 'X' END AS 'INFLUENZAESTACIO desco',
		CASE TOMRADIOTORAX WHEN '1' THEN 'X' END AS 'TOMRADIOTORAX Si',CASE TOMRADIOTORAX WHEN '2' THEN 'X' END AS 'TOMRADIOTORAX No', CASE TOMRADIOTORAX WHEN '3' THEN 'X' END AS 'TOMRADIOTORAX desco',
		Rtrim(DOSIS1) As 'DOSIS1', Rtrim(DOSIS2) As 'DOSIS2', Rtrim(CUALESOTROS) As 'CUALESOTROS', 
		Rtrim(SEMAGEST) As 'SEMAGEST', Rtrim(OTROSCUALES) As 'OTROSCUALES', Rtrim(DIAGNOINICI) As 'DIAGNOINICI', 
		Rtrim(DIAGNOEGRESO) As 'DIAGNOEGRESO', Rtrim(VALORREGIS1) As 'VALORREGIS1', Rtrim(VALORREGIS2) As 'VALORREGIS2', 
		convert(varchar(10),FECHAULTDOS1,103) As 'FECHAULTDOS1', convert(varchar(10),FECHAULTDOS2,103) As 'FECHAULTDOS2', convert(varchar(10),FECHATOMA1,103) As 'FECHATOMA1',
		convert(varchar(10),FECHATOMA2,103) As 'FECHATOMA2', convert(varchar(10),FECHATOMA3,103) As 'FECHATOMA3', convert(varchar(10),FECHAINICIO1,103) As 'FECHAINICIO1',
		convert(varchar(10),FECHAINICIO2,103) As 'FECHAINICIO2', convert(varchar(10),FECHAINGRESO,103) As 'FECHAINGRESO', convert(varchar(10),FECHARECE1,103) As 'FECHARECE1',
		convert(varchar(10),FECHARECE2,103) As 'FECHARECE2', convert(varchar(10),FECHARECE3,103) As 'FECHARECE13', convert(varchar(10),FECHARECE4,103) As 'FECHARECE4',
		CASE HALLAZGOS WHEN '1' THEN 'X' END AS 'InfiltAlveo',CASE HALLAZGOS WHEN '2' THEN 'X' END AS 'Infilinters', CASE HALLAZGOS WHEN '3' THEN 'X' END AS 'Ninguno',
		CASE ASMA WHEN '1' THEN 'X' END AS 'ASMA',CASE OBESIDAD WHEN '1' THEN 'X' END AS 'OBESIDAD', CASE TOS WHEN '1' THEN 'X' END AS 'TOS',
		CASE EPOC WHEN '1' THEN 'X' END AS 'EPOC',CASE INSUFIRENAL WHEN '1' THEN 'X' END AS 'INSUFIRENAL', CASE FIEBRE WHEN '1' THEN 'X' END AS 'FIEBRE',
		CASE DIABETES WHEN '1' THEN 'X' END AS 'DIABETES',CASE TOMAMEDICA WHEN '1' THEN 'X' END AS 'TOMAMEDICA', CASE DOLORGARGANTA WHEN '1' THEN 'X' END AS 'DOLORGARGANTA',
		CASE VIH WHEN '1' THEN 'X' END AS 'VIH',CASE FUMADOR WHEN '1' THEN 'X' END AS 'FUMADOR', CASE RINORREA WHEN '1' THEN 'X' END AS 'RINORREA',
		CASE ENFECARDIACA WHEN '1' THEN 'X' END AS 'ENFECARDIACA',CASE OTROS WHEN '1' THEN 'X' END AS 'OTROS', CASE CONJUNTIVITIS WHEN '1' THEN 'X' END AS 'CONJUNTIVITIS',
		CASE CANCER WHEN '1' THEN 'X' END AS 'CANCER',CASE CEFALEA WHEN '1' THEN 'X' END AS 'CEFALEA', CASE MALNUTRICION WHEN '1' THEN 'X' END AS 'MALNUTRICION',
		CASE DIFIRESPIRA WHEN '1' THEN 'X' END AS 'DIFIRESPIRA',CASE DIARREA WHEN '1' THEN 'X' END AS 'DIARREA', CASE DERRAPLEURAL WHEN '1' THEN 'X' END AS 'DERRAPLEURAL',
		CASE DERRAPERICAR WHEN '1' THEN 'X' END AS 'DERRAPERICAR',CASE MIOCARDITIS WHEN '1' THEN 'X' END AS 'MIOCARDITIS', CASE SEPTICEMIA WHEN '1' THEN 'X' END AS 'SEPTICEMIA',
		CASE FALLARESPIR WHEN '1' THEN 'X' END AS 'FALLARESPIR',CASE OTRO WHEN '1' THEN 'X' END AS 'OTRO',

		CASE MUESTRA1 WHEN '1' THEN '1' WHEN '2' THEN '3' WHEN '3' THEN '4' WHEN '4' THEN '8' WHEN '5' THEN '11' WHEN '6' THEN '22' END AS 'MUESTRA1',
		CASE PRUEBA1  WHEN '1' THEN '4' WHEN '2' THEN 'E1' WHEN '3' THEN '30' WHEN '4' THEN'31' WHEN '5' THEN '46' WHEN '6' THEN '55'
		WHEN '7' THEN '76' WHEN '8' THEN '92' END AS 'PRUEBA1',
		CASE AGENTE1  WHEN '1' THEN '8' WHEN '2' THEN '16' WHEN '3' THEN '18' WHEN '4' THEN '22' WHEN '5' THEN '24' WHEN '6' THEN '40'
		WHEN '7' THEN '41' WHEN '8' THEN '42' WHEN '9' THEN '43' WHEN '10' THEN '44' WHEN '11' THEN '56' WHEN '12' THEN '59'
		WHEN '13' THEN '64' WHEN '14' THEN '76' WHEN '15' THEN '77' WHEN '16' THEN '78' WHEN '17' THEN '79' WHEN '18' THEN '84'
		WHEN '19' THEN '1Q' WHEN '20' THEN '1R' WHEN '21' THEN '1S' WHEN '22' THEN '1T' WHEN '23' THEN '1U' WHEN '24' THEN '1V'
		WHEN '25' THEN '1W' WHEN '26' THEN '2H'  END AS 'AGENTE1',
		CASE RESULTADO1  WHEN '1' THEN '1' WHEN '2' THEN '2' WHEN '3' THEN '3'
		WHEN '4' THEN '4' WHEN '5' THEN '6' WHEN '6' THEN '12' WHEN '7' THEN '13' END AS 'RESULTADO1',
		CASE MUESTRA2 WHEN '1' THEN '1' WHEN '2' THEN '3' WHEN '3' THEN '4' WHEN '4' THEN '8' WHEN '5' THEN '11' WHEN '6' THEN '22' END AS 'MUESTRA2',
		CASE PRUEBA2  WHEN '1' THEN '4' WHEN '2' THEN 'E1' WHEN '3' THEN '30' WHEN '4' THEN'31' WHEN '5' THEN '46' WHEN '6' THEN '55'
		WHEN '7' THEN '76' WHEN '8' THEN '92' END AS 'PRUEBA2',
		CASE AGENTE2  WHEN '1' THEN '8' WHEN '2' THEN '16' WHEN '3' THEN '18' WHEN '4' THEN '22' WHEN '5' THEN '24' WHEN '6' THEN '40'
		WHEN '7' THEN '41' WHEN '8' THEN '42' WHEN '9' THEN '43' WHEN '10' THEN '44' WHEN '11' THEN '56' WHEN '12' THEN '59'
		WHEN '13' THEN '64' WHEN '14' THEN '76' WHEN '15' THEN '77' WHEN '16' THEN '78' WHEN '17' THEN '79' WHEN '18' THEN '84'
		WHEN '19' THEN '1Q' WHEN '20' THEN '1R' WHEN '21' THEN '1S' WHEN '22' THEN '1T' WHEN '23' THEN '1U' WHEN '24' THEN '1V'
		WHEN '25' THEN '1W' WHEN '26' THEN '2H' END AS 'AGENTE2',
		CASE RESULTADO2  WHEN '1' THEN '1' WHEN '2' THEN '2' WHEN '3' THEN '3'
		WHEN '4' THEN '4' WHEN '5' THEN '6' WHEN '6' THEN '12' WHEN '7' THEN '13' END AS 'RESULTADO2',
		CASE RTRIM( JSON_VALUE(JSON,'$.COVID19') ) WHEN 1 THEN 'X' END AS 'CovidSi',
		CASE RTRIM( JSON_VALUE(JSON,'$.COVID19') ) WHEN 2 THEN 'X' END AS 'CovidNo',
		CASE RTRIM( JSON_VALUE(JSON,'$.COVID19') ) WHEN 3 THEN 'X' END AS 'CovidDesco',
		CASE TRY_CAST( JSON_VALUE(JSON,'$.Hipertension') AS BIT) WHEN 1 THEN 'X' END AS 'Hipertension',
		RTRIM( JSON_VALUE(JSON,'$.DOSIS3') ) As 'DOSIS3',
		format(CONVERT(date,RTRIM(JSON_VALUE(JSON,'$.FECHAULTDOS3'))),'dd/MM/yyyy') As 'FECHAULTDOS3',
		RTRIM( JSON_VALUE(JSON,'$.NOMVACU') ) As 'NOMVACU',
		VERSION AS 'VERSION', 
		JSON AS 'JSON' 
	From HCFICHA345 
	where IDFICHANOTIFICACION  = @IdFicha
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera y formatea todos los datos clínicos y epidemiológicos registrados en la ficha SIVIGILA número 345 para la notificación de casos de Infección Respiratoria Aguda Grave (IRAG). Recibe como parámetros el identificador interno de la ficha y el número de ficha SIVIGILA, y devuelve en formato listo para impresión o reporte los campos de: antecedentes de vacunación (neumococo, influenza estacional, dosis y fechas), comorbilidades del paciente (asma, diabetes, EPOC, VIH, obesidad, cáncer, insuficiencia renal, enfermedad cardíaca, entre otras), síntomas clínicos (tos, fiebre, rinorrea, dificultad respiratoria, diarrea, conjuntivitis, etc.), manejo hospitalario (uso de antibióticos, antivirales, servicio de hospitalización), hallazgos radiológicos de tórax, complicaciones (falla respiratoria, septicemia, derrame pleural, miocarditis), muestras de laboratorio con su tipo, prueba, agente identificado y resultado (hasta tres muestras), diagnóstico inicial y de egreso, semanas de gestación y fechas clave de ingreso, toma de muestras y recepción. Todos los valores codificados se traducen a marcas ''X'' o códigos estándar SIVIGILA para facilitar el diligenciamiento y envío del reporte obligatorio al sistema nacional de vigilancia epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila345';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila345';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea los datos de una ficha de notificación Sivigila (IRA/ESI/COVID) traduciendo códigos internos a los códigos oficiales del sistema de vigilancia epidemiológica para su presentación o reporte.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila345';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA345 cuyo IDFICHANOTIFICACION coincida con el identificador recibido; El campo JSON debe contener una estructura parseable con claves COVID19, Hipertension, DOSIS3, FECHAULTDOS3 y NOMVACU para extraer datos extendidos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila345';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las fechas se devuelven formateadas como dd/MM/yyyy (estilo 103); Los campos de texto se entregan sin espacios a la derecha (RTRIM); Solo los valores con mapeo explícito definido se traducen; cualquier otro retorna NULL; Los datos extendidos (DOSIS3, FECHAULTDOS3, NOMVACU, COVID19, Hipertension) se obtienen del campo JSON, no de columnas físicas; El procedimiento siempre opera sobre una sola ficha identificada por IDFICHANOTIFICACION', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila345';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Vigilancia epidemiológica; IRA (Infección Respiratoria Aguda); COVID-19; Vacunación (neumococo, influenza estacional, dosis); Comorbilidades (asma, EPOC, diabetes, VIH, cáncer, obesidad, hipertensión, insuficiencia renal, enfermedad cardiaca, malnutrición); Sintomatología (tos, fiebre, dolor de garganta, rinorrea, conjuntivitis, cefalea, dificultad respiratoria, diarrea); Complicaciones (derrame pleural, derrame pericárdico, miocarditis, septicemia, falla respiratoria); Hallazgos radiológicos (infiltrado alveolar, infiltrado intersticial); Muestras de laboratorio, pruebas diagnósticas, agentes etiológicos y resultados; Semanas de gestación; Diagnóstico de ingreso y egreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila345';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA345: Devuelve una fila con los datos de la ficha filtrada por IDFICHANOTIFICACION, transformando flags 1/0/2/3 en marcas ''X'' y mapeando códigos internos de muestra/prueba/agente/resultado a códigos oficiales Sivigila', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila345';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Campos binarios tipo PRESECARNE/USOANTIBIO/USOANTIVIRAL/SERVIHOSPITAL = ''1'' o ''0'' → Marca ''X'' en columna ''si'' o ''no'' respectivamente else NULL; si Campos triádicos NEUMOCOCO/INFLUENZAESTACIO/TOMRADIOTORAX = ''1'',''2'',''3'' → Marca ''X'' en columna Si, No o Desconocido else NULL; si HALLAZGOS = ''1'',''2'',''3'' → Marca como InfiltAlveo, Infilinters o Ninguno respectivamente; si MUESTRA1/MUESTRA2 con valores 1-6 → Traduce a códigos Sivigila 1,3,4,8,11,22; si PRUEBA1/PRUEBA2 con valores 1-8 → Traduce a códigos Sivigila 4,E1,30,31,46,55,76,92; si AGENTE1/AGENTE2 con valores 1-26 → Traduce a códigos Sivigila de agentes etiológicos (8,16,18,22,...,1Q-2H); si RESULTADO1/RESULTADO2 con valores 1-7 → Traduce a códigos Sivigila 1,2,3,4,6,12,13; si JSON.COVID19 = 1, 2 o 3 → Marca ''X'' en CovidSi, CovidNo o CovidDesco respectivamente; si TRY_CAST(JSON.Hipertension AS BIT) = 1 → Marca ''X'' en columna Hipertension', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila345';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA345', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila345';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila345';
-- GO
