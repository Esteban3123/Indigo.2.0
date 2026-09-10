-- Stored Procedure

-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,07-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila298]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 

	--Select 	*	From HCFICHA298 where IDFICHANOTIFICACION  = 13

	Select
	CASE VACU1 WHEN '1' THEN 'BCG' WHEN '2' THEN 'DPT' WHEN '3' THEN 'ANTIPOLIO ORAL' WHEN '4' THEN 'HB' WHEN '5' THEN 'HiB' WHEN '6' THEN 'PENTAVALENTE' WHEN '7' THEN 'TRIPLE VIRAL' 
	WHEN '8' THEN 'F.A' WHEN '9' THEN 'SR-' WHEN '10' THEN 'Td/TD' WHEN '11' THEN 'INFLUENZA' WHEN '12' THEN 'Tdap' WHEN '13' THEN 'ANTINEUMOCOCO' WHEN '14' THEN 'ANTIVARICELA' 
	WHEN '15' THEN 'ANTIROTAVIRICA' WHEN '16' THEN 'OTRA' WHEN '17' THEN 'HEPATITIS A' WHEN '18' THEN 'Anti VPH' WHEN '19' THEN 'ANTIMENINGOCOCO'
	WHEN '20' THEN 'ANTIRRABICA' WHEN '21' THEN 'ANTIPOLIO INYECTABLE' WHEN '22' THEN 'Hexavalente' WHEN '23' THEN 'AntiTyphi' WHEN '24' THEN 'COVID-19' END AS 'VACU1',
	CASE DOSIS1 WHEN '1' THEN 'PRIMERA' WHEN '2' THEN 'SEGUNDA' WHEN '3' THEN 'TERCERA' WHEN '4' THEN 'ADICIONAL RN' WHEN '5' THEN 'ÚNICA' 
	WHEN '6' THEN 'REFUERZO' END AS 'DOSIS1',
	CASE VIA1 WHEN '1' THEN 'ORAL' WHEN '2' THEN 'INTRADÉRMICA' WHEN '3' THEN 'SUBCUTÁNEA' WHEN '4' THEN 'INTRAMUSCULAR' END AS 'VIA1',
	CASE SITIO1 WHEN '1' THEN 'HOMBRO DER.' WHEN '2' THEN 'HOMBRO IZQ.' WHEN '3' THEN 'BRAZO DER.' WHEN '4' THEN 'BRAZO IZQ.' 
	WHEN '5' THEN 'GLÚTEO DER.' WHEN '6' THEN 'GLÚTEO IZQ.' WHEN '7' THEN 'MUSLO DER.' WHEN '8' THEN 'MUSLO IZQ.' WHEN '9' THEN 'ORAL' END AS 'SITIO1',
	convert(varchar(10),FECHA1,103) As 'FECHA1', Rtrim(LOTE1) As 'LOTE1', 

	CASE VACU2 WHEN '1' THEN 'BCG' WHEN '2' THEN 'DPT' WHEN '3' THEN 'ANTIPOLIO ORAL' WHEN '4' THEN 'HB' WHEN '5' THEN 'HiB' WHEN '6' THEN 'PENTAVALENTE' WHEN '7' THEN 'TRIPLE VIRAL' 
	WHEN '8' THEN 'F.A' WHEN '9' THEN 'SR-' WHEN '10' THEN 'Td/TD' WHEN '11' THEN 'INFLUENZA' WHEN '12' THEN 'Tdap' WHEN '13' THEN 'ANTINEUMOCOCO' WHEN '14' THEN 'ANTIVARICELA' 
	WHEN '15' THEN 'ANTIROTAVIRICA' WHEN '16' THEN 'OTRA' WHEN '17' THEN 'HEPATITIS A' WHEN '18' THEN 'Anti VPH' WHEN '19' THEN 'ANTIMENINGOCOCO'
	WHEN '20' THEN 'ANTIRRABICA' WHEN '21' THEN 'ANTIPOLIO INYECTABLE' WHEN '22' THEN 'Hexavalente' WHEN '23' THEN 'AntiTyphi' WHEN '24' THEN 'COVID-19' END AS 'VACU2',
	CASE DOSIS2 WHEN '1' THEN 'PRIMERA' WHEN '2' THEN 'SEGUNDA' WHEN '3' THEN 'TERCERA' WHEN '4' THEN 'ADICIONAL RN' WHEN '5' THEN 'ÚNICA' 
	WHEN '6' THEN 'REFUERZO' END AS 'DOSIS2',
	CASE VIA2 WHEN '1' THEN 'ORAL' WHEN '2' THEN 'INTRADÉRMICA' WHEN '3' THEN 'SUBCUTÁNEA' WHEN '4' THEN 'INTRAMUSCULAR' END AS 'VIA2',
	CASE SITIO2 WHEN '1' THEN 'HOMBRO DER.' WHEN '2' THEN 'HOMBRO IZQ.' WHEN '3' THEN 'BRAZO DER.' WHEN '4' THEN 'BRAZO IZQ.' 
	WHEN '5' THEN 'GLÚTEO DER.' WHEN '6' THEN 'GLÚTEO IZQ.' WHEN '7' THEN 'MUSLO DER.' WHEN '8' THEN 'MUSLO IZQ.' WHEN '9' THEN 'ORAL' END AS 'SITIO2',
	convert(varchar(10),FECHA2,103) As 'FECHA2', Rtrim(LOTE2) As 'LOTE2', 

	CASE VACU3 WHEN '1' THEN 'BCG' WHEN '2' THEN 'DPT' WHEN '3' THEN 'ANTIPOLIO ORAL' WHEN '4' THEN 'HB' WHEN '5' THEN 'HiB' WHEN '6' THEN 'PENTAVALENTE' WHEN '7' THEN 'TRIPLE VIRAL' 
	WHEN '8' THEN 'F.A' WHEN '9' THEN 'SR-' WHEN '10' THEN 'Td/TD' WHEN '11' THEN 'INFLUENZA' WHEN '12' THEN 'Tdap' WHEN '13' THEN 'ANTINEUMOCOCO' WHEN '14' THEN 'ANTIVARICELA' 
	WHEN '15' THEN 'ANTIROTAVIRICA' WHEN '16' THEN 'OTRA' WHEN '17' THEN 'HEPATITIS A' WHEN '18' THEN 'Anti VPH' WHEN '19' THEN 'ANTIMENINGOCOCO'
	WHEN '20' THEN 'ANTIRRABICA' WHEN '21' THEN 'ANTIPOLIO INYECTABLE' WHEN '22' THEN 'Hexavalente' WHEN '23' THEN 'AntiTyphi' WHEN '24' THEN 'COVID-19' END AS 'VACU3',
	CASE DOSIS3 WHEN '1' THEN 'PRIMERA' WHEN '2' THEN 'SEGUNDA' WHEN '3' THEN 'TERCERA' WHEN '4' THEN 'ADICIONAL RN' WHEN '5' THEN 'ÚNICA' 
	WHEN '6' THEN 'REFUERZO' END AS 'DOSIS3',
	CASE VIA3 WHEN '1' THEN 'ORAL' WHEN '2' THEN 'INTRADÉRMICA' WHEN '3' THEN 'SUBCUTÁNEA' WHEN '4' THEN 'INTRAMUSCULAR' END AS 'VIA3',
	CASE SITIO3 WHEN '1' THEN 'HOMBRO DER.' WHEN '2' THEN 'HOMBRO IZQ.' WHEN '3' THEN 'BRAZO DER.' WHEN '4' THEN 'BRAZO IZQ.' 
	WHEN '5' THEN 'GLÚTEO DER.' WHEN '6' THEN 'GLÚTEO IZQ.' WHEN '7' THEN 'MUSLO DER.' WHEN '8' THEN 'MUSLO IZQ.' WHEN '9' THEN 'ORAL' END AS 'SITIO3',
	convert(varchar(10),FECHA3,103) As 'FECHA3', Rtrim(LOTE3) As 'LOTE3', 

	CASE VACU4 WHEN '1' THEN 'BCG' WHEN '2' THEN 'DPT' WHEN '3' THEN 'ANTIPOLIO ORAL' WHEN '4' THEN 'HB' WHEN '5' THEN 'HiB' WHEN '6' THEN 'PENTAVALENTE' WHEN '7' THEN 'TRIPLE VIRAL' 
	WHEN '8' THEN 'F.A' WHEN '9' THEN 'SR-' WHEN '10' THEN 'Td/TD' WHEN '11' THEN 'INFLUENZA' WHEN '12' THEN 'Tdap' WHEN '13' THEN 'ANTINEUMOCOCO' WHEN '14' THEN 'ANTIVARICELA' 
	WHEN '15' THEN 'ANTIROTAVIRICA' WHEN '16' THEN 'OTRA' WHEN '17' THEN 'HEPATITIS A' WHEN '18' THEN 'Anti VPH' WHEN '19' THEN 'ANTIMENINGOCOCO'
	WHEN '20' THEN 'ANTIRRABICA' WHEN '21' THEN 'ANTIPOLIO INYECTABLE' WHEN '22' THEN 'Hexavalente' WHEN '23' THEN 'AntiTyphi' WHEN '24' THEN 'COVID-19' END AS 'VACU4',
	CASE DOSIS4 WHEN '1' THEN 'PRIMERA' WHEN '2' THEN 'SEGUNDA' WHEN '3' THEN 'TERCERA' WHEN '4' THEN 'ADICIONAL RN' WHEN '5' THEN 'ÚNICA' 
	WHEN '6' THEN 'REFUERZO' END AS 'DOSIS4',
	CASE VIA4 WHEN '1' THEN 'ORAL' WHEN '2' THEN 'INTRADÉRMICA' WHEN '3' THEN 'SUBCUTÁNEA' WHEN '4' THEN 'INTRAMUSCULAR' END AS 'VIA4',
	CASE SITIO4 WHEN '1' THEN 'HOMBRO DER.' WHEN '2' THEN 'HOMBRO IZQ.' WHEN '3' THEN 'BRAZO DER.' WHEN '4' THEN 'BRAZO IZQ.' 
	WHEN '5' THEN 'GLÚTEO DER.' WHEN '6' THEN 'GLÚTEO IZQ.' WHEN '7' THEN 'MUSLO DER.' WHEN '8' THEN 'MUSLO IZQ.' WHEN '9' THEN 'ORAL' END AS 'SITIO4',
	convert(varchar(10),FECHA4,103) As 'FECHA4', Rtrim(LOTE4) As 'LOTE4', 

	CASE ADENITIS  WHEN '1' THEN 'X' END AS 'ADENITIS',CASE ABSCESO WHEN '1' THEN 'X' END AS 'ABSCESO', CASE LINFADE WHEN '1' THEN 'X' END AS 'LINFADE',
    CASE FIEBRE  WHEN '1' THEN 'X' END AS 'FIEBRE',CASE CONVUFEBR WHEN '1' THEN 'X' END AS 'CONVUFEBR', CASE CONVUSINF WHEN '1' THEN 'X' END AS 'CONVUSINF',
	CASE EPIHIPOTO  WHEN '1' THEN 'X' END AS 'EPIHIPOTO',CASE PARESTESIA WHEN '1' THEN 'X' END AS 'PARESTESIA', CASE PARALISIS WHEN '1' THEN 'X' END AS 'PARALISIS',
    CASE ENCEFALO  WHEN '1' THEN 'X' END AS 'ENCEFALO',CASE MENINGI WHEN '1' THEN 'X' END AS 'MENINGI', CASE URTICARIA WHEN '1' THEN 'X' END AS 'URTICARIA',
	CASE ECZEMA  WHEN '1' THEN 'X' END AS 'ECZEMA',CASE CHOQUE WHEN '1' THEN 'X' END AS 'CHOQUE', CASE GUILLAIN WHEN '1' THEN 'X' END AS 'GUILLAIN',
    CASE CELULITIS  WHEN '1' THEN 'X' END AS 'CELULITIS',CASE LLANTO WHEN '1' THEN 'X' END AS 'LLANTO', CASE RUMOR WHEN '1' THEN 'X' END AS 'RUMOR',
	Rtrim(TIEMPOTRANS) As 'TIEMPOTRANS', CASE UNIMEDI  WHEN '1' THEN 'X' END AS 'Meses',CASE UNIMEDI WHEN '2' THEN 'X' END AS 'Dias',
	CASE UNIMEDI WHEN '3' THEN 'X' END AS 'Horas', CASE UNIMEDI  WHEN '4' THEN 'X' END AS 'Minutos',
	CASE ANTEPAT  WHEN '1' THEN 'X' END AS 'ANTEPAT si',CASE ANTEPAT WHEN '0' THEN 'X' END AS 'ANTEPAT no',  Rtrim(CUAL1) As 'CUAL1',
	CASE ANTEALERG  WHEN '1' THEN 'X' END AS 'ANTEALERG si',CASE ANTEALERG WHEN '0' THEN 'X' END AS 'ANTEALERG no',  Rtrim(CUAL2) As 'CUAL2',
	CASE ANTEPREV  WHEN '1' THEN 'X' END AS 'ANTEPREV si',CASE ANTEPREV WHEN '0' THEN 'X' END AS 'ANTEPREV no',  Rtrim(CUAL3) As 'CUAL3',
	CASE ESTADFINAL  WHEN '1' THEN 'X' END AS 'Recp sin sec',CASE ESTADFINAL WHEN '2' THEN 'X' END AS 'Recp con sec',
	CASE CLASCASO  WHEN '1' THEN 'X' END AS 'relavacun',CASE CLASCASO WHEN '2' THEN 'X' END AS 'relaprogra',
	CASE CLASCASO WHEN '3' THEN 'X' END AS 'coincidente', CASE CLASCASO  WHEN '4' THEN 'X' END AS 'no concluyente', CASE CLASCASO  WHEN '5' THEN 'X' END AS 'Pendiente',
	CASE CLASCASO  WHEN '6' THEN 'X' END AS 'CaDefectVacu', CASE CLASCASO  WHEN '7' THEN 'X' END AS 'CaAnsieVacu',
	VERSION AS 'VERSION', JSON AS 'JSON',
	CASE RTRIM( JSON_VALUE(JSON,'$.FATIGA') ) WHEN 'TRUE' THEN 'X' END AS 'Fatiga', CASE RTRIM( JSON_VALUE(JSON,'$.DOLORCABEZA') ) WHEN 'TRUE' THEN 'X' END AS 'DolorCabeza', 
	CASE RTRIM( JSON_VALUE(JSON,'$.MIALGIA') ) WHEN 'TRUE' THEN 'X' END AS 'Mialgia', CASE RTRIM( JSON_VALUE(JSON,'$.ARTRALGIA') ) WHEN 'TRUE' THEN 'X' END AS 'Artralgia',
	CASE RTRIM( JSON_VALUE(JSON,'$.NAUSEAS') ) WHEN 'TRUE' THEN 'X' END AS 'Nauseas', CASE RTRIM( JSON_VALUE(JSON,'$.OTROS') ) WHEN 'TRUE' THEN 'X' END AS 'Otros', RTRIM( JSON_VALUE(JSON,'$.CUALOTROS') ) As 'CualOtros'
	From HCFICHA298 where IDFICHANOTIFICACION  = @IdFicha

END

--select * from hcfichanotificacion where id = '8'
--select * from HCFICHA298
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera y presenta de forma legible el detalle completo de una ficha SIVIGILA 298 de Eventos Supuestamente Atribuibles a la Vacunación e Inmunización (ESAVI), identificada por su número o ID de ficha. Consulta la tabla HCFICHA298 y traduce los códigos numéricos internos a descripciones legibles en lenguaje humano: nombre de la vacuna (BCG, DPT, COVID-19, etc.), número de dosis (primera, segunda, refuerzo, etc.), vía de administración (oral, intramuscular, etc.), sitio de aplicación (hombro derecho, muslo izquierdo, etc.), fecha y lote del biológico para hasta 4 vacunas registradas en el evento. También expone las manifestaciones clínicas asociadas al ESAVI como adenitis, absceso, linfadenopatía y fiebre con convulsión febril. Se utiliza para visualizar y reportar la notificación obligatoria al sistema de vigilancia epidemiológica SIVIGILA ante una reacción adversa post-vacunación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila298';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila298';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista y traduce a etiquetas legibles la información de la ficha epidemiológica Sivigila 298 (ESAVI - eventos adversos posteriores a la vacunación) para una ficha de notificación específica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila298';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA298 cuyo IDFICHANOTIFICACION coincida con el identificador recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila298';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las fechas de aplicación (FECHA1..FECHA4) se devuelven en formato dd/mm/yyyy (estilo 103).; Códigos fuera del rango definido en cada CASE devuelven NULL en la columna correspondiente.; El parámetro @NumFicha se declara pero no se utiliza en el filtrado; el único criterio efectivo es IDFICHANOTIFICACION = @IdFicha.; Los campos de texto LOTE1..LOTE4, CUAL1..CUAL3, TIEMPOTRANS y CUALOTROS se devuelven sin espacios finales (RTRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila298';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Sivigila; Ficha de notificación 298; ESAVI (Evento adverso posterior a la vacunación); Vacunas del PAI; Dosis y vía de administración; Sitio anatómico de aplicación; Antecedentes patológicos/alérgicos/de eventos previos; Clasificación de caso ESAVI; Síntomas post-vacunación COVID-19 (campo JSON)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila298';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA298: Cuando IDFICHANOTIFICACION = @IdFicha, se retorna la ficha decodificando códigos numéricos a descripciones (vacuna, dosis, vía, sitio de aplicación, unidad de tiempo, estado final, clasificación del caso) y marcando con ''X'' los eventos adversos presentes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila298';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si VACU1..VACU4 entre ''1'' y ''24'' → Se traduce el código a nombre de vacuna (BCG, DPT, ANTIPOLIO ORAL, HB, HiB, PENTAVALENTE, TRIPLE VIRAL, F.A, SR-, Td/TD, INFLUENZA, Tdap, ANTINEUMOCOCO, ANTIVARICELA, ANTIROTAVIRICA, OTRA, HEPATITIS A, Anti VPH, ANTIMENINGOCOCO, ANTIRRABICA, ANTIPOLIO INYECTABLE, Hexavalente, AntiTyphi, COVID-19); si DOSIS1..DOSIS4 entre ''1'' y ''6'' → Se traduce a PRIMERA, SEGUNDA, TERCERA, ADICIONAL RN, ÚNICA o REFUERZO; si VIA1..VIA4 entre ''1'' y ''4'' → Se traduce a ORAL, INTRADÉRMICA, SUBCUTÁNEA o INTRAMUSCULAR; si SITIO1..SITIO4 entre ''1'' y ''9'' → Se traduce a ubicación anatómica (HOMBRO/BRAZO/GLÚTEO/MUSLO derecho o izquierdo, u ORAL); si UNIMEDI = 1/2/3/4 → Se marca columna Meses/Dias/Horas/Minutos respectivamente; si ANTEPAT, ANTEALERG, ANTEPREV = ''1'' o ''0'' → Se marca columna ''si'' o ''no'' según corresponda al antecedente patológico, alérgico o de evento previo; si ESTADFINAL = ''1'' o ''2'' → Se marca ''Recp sin sec'' (recuperado sin secuelas) o ''Recp con sec'' (recuperado con secuelas); si CLASCASO entre ''1'' y ''7'' → Se marca clasificación: relavacun, relaprogra, coincidente, no concluyente, Pendiente, CaDefectVacu o CaAnsieVacu; si Campos clínicos (ADENITIS, ABSCESO, LINFADE, FIEBRE, CONVUFEBR, etc.) = ''1'' → Se marca con ''X'' indicando presencia del evento adverso; si JSON_VALUE(JSON,''$.<sintoma>'') = ''TRUE'' → Se marca con ''X'' el síntoma extra (Fatiga, DolorCabeza, Mialgia, Artralgia, Nauseas, Otros) proveniente del campo JSON de la versión nueva de ficha', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila298';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA298', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila298';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila298';
-- GO
