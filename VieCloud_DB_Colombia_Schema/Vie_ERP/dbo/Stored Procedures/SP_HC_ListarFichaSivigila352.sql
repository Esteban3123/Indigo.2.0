-- Stored Procedure

-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,13-12-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila352]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 

	--Select 	*	From HCFICHA352 where IDFICHANOTIFICACION  = 13

	Select
	CASE COMPLESERVI WHEN '1' THEN 'X' END AS 'Baja',CASE COMPLESERVI WHEN '2' THEN 'X' END AS 'Media', CASE COMPLESERVI WHEN '3' THEN 'X' END AS 'Alta',
	CASE SERVIADMIN WHEN '1' THEN 'X' END AS 'ProgramAmbula',CASE SERVIADMIN WHEN '2' THEN 'X' END AS 'Urgencias', CASE SERVIADMIN WHEN '3' THEN 'X' END AS 'ProgramHospital',
	CASE PROCEDMEDI WHEN '1' THEN 'X' END AS 'Cesárea',CASE PROCEDMEDI WHEN '2' THEN 'X' END AS 'Herniorrafia', CASE PROCEDMEDI WHEN '3' THEN 'X' END AS 'Parto',
	CASE PROCEDMEDI WHEN '4' THEN 'X' END AS 'Revascularizacion',CASE PROCEDMEDI WHEN '5' THEN 'X' END AS 'Colecistectomia',
	CASE PROFILAXIS  WHEN '1' THEN 'X' END AS 'PROFILAXIS si',CASE PROFILAXIS WHEN '0' THEN 'X' END AS 'PROFILAXIS no',
	CASE LAPAROSCOPIA  WHEN '1' THEN 'X' END AS 'LAPAROSCOPIA si',CASE LAPAROSCOPIA WHEN '0' THEN 'X' END AS 'LAPAROSCOPIA no',
	DURAPROCE As 'DURAPROCE',
	convert(varchar(10),FECHATOMA1,103) As 'FECHATOMA1', convert(varchar(10),FECHATOMA2,103) As 'FECHATOMA2', 
	convert(varchar(10),FECHATOMA3,103) As 'FECHATOMA3', 
	Rtrim(CUAL) As 'CUAL', Rtrim(MICROORGA1) As 'MICROORGA1', Rtrim(MICROORGA2) As 'MICROORGA2', Rtrim(MICROORGA3) As 'MICROORGA3',
	CASE DIABETES WHEN '1' THEN 'X' END AS 'DIABETES',CASE INMUNOSUPRE WHEN '1' THEN 'X' END AS 'INMUNOSUPRE', CASE OBESIDAD WHEN '1' THEN 'X' END AS 'OBESIDAD',
	CASE DESNUTRICION WHEN '1' THEN 'X' END AS 'DESNUTRICION',CASE PREECLAMPSIA WHEN '1' THEN 'X' END AS 'PREECLAMPSIA', CASE ANEMIA WHEN '1' THEN 'X' END AS 'ANEMIA',
    CASE SUPERPRIMA WHEN '1' THEN 'X' END AS 'SUPERPRIMA',CASE SUPERSECUN WHEN '1' THEN 'X' END AS 'SUPERSECUN', CASE PROFUPRIMA WHEN '1' THEN 'X' END AS 'PROFUPRIMA',
	CASE PROFUSECUN WHEN '1' THEN 'X' END AS 'PROFUSECUN',CASE ORGAESPAC WHEN '1' THEN 'X' END AS 'ORGAESPAC',
	CASE CLASIFASA WHEN '1' THEN 'X' END AS 'ASA 1',CASE CLASIFASA WHEN '2' THEN 'X' END AS 'ASA 2', CASE CLASIFASA WHEN '3' THEN 'X' END AS 'ASA 3',
	CASE CLASIFASA WHEN '4' THEN 'X' END AS 'ASA 4',CASE CLASIFASA WHEN '5' THEN 'X' END AS 'ASA 5',
	CASE TIPOHERIDA WHEN '1' THEN 'X' END AS 'Limpia',CASE TIPOHERIDA WHEN '2' THEN 'X' END AS 'LimpiaContami', CASE TIPOHERIDA WHEN '3' THEN 'X' END AS 'HeridaContami',
	CASE TIPOHERIDA WHEN '4' THEN 'X' END AS 'HeridaSucia',
	CASE TIEMPOADMIN WHEN '1' THEN 'X' END AS 'Antes',CASE TIEMPOADMIN WHEN '2' THEN 'X' END AS 'Durante', CASE TIEMPOADMIN WHEN '3' THEN 'X' END AS 'Despues',
	CASE TIEMPOADMIN WHEN '4' THEN 'X' END AS 'Ninguna',
	CASE CODMUESTRA1 WHEN '1' THEN '1' WHEN '2' THEN '4' WHEN '3' THEN '11' WHEN '4' THEN '32' END AS 'CODMUESTRA1',
	CASE CODPRUEBA1  WHEN '1' THEN '55' WHEN '2' THEN '92' WHEN '3' THEN 'G3' WHEN '4' THEN'86' 
	WHEN '5' THEN '90' WHEN '6' THEN 'D4' END AS 'CODPRUEBA1',
	CASE CODMUESTRA2 WHEN '1' THEN '1' WHEN '2' THEN '4' WHEN '3' THEN '11' WHEN '4' THEN '32' END AS 'CODMUESTRA2',
	CASE CODPRUEBA2  WHEN '1' THEN '55' WHEN '2' THEN '92' WHEN '3' THEN 'G3' WHEN '4' THEN'86' 
	WHEN '5' THEN '90' WHEN '6' THEN 'D4' END AS 'CODPRUEBA2',
	CASE CODMUESTRA3 WHEN '1' THEN '1' WHEN '2' THEN '4' WHEN '3' THEN '11' WHEN '4' THEN '32' END AS 'CODMUESTRA3',
	CASE CODPRUEBA3  WHEN '1' THEN '55' WHEN '2' THEN '92' WHEN '3' THEN 'G3' WHEN '4' THEN'86' 
	WHEN '5' THEN '90' WHEN '6' THEN 'D4' END AS 'CODPRUEBA3',
	VERSION AS 'VERSION', JSON AS 'JSON',
	RTRIM( JSON_VALUE(JSON,'$.CODCUPS') ) As 'CODCUPS', CONVERT(varchar(10),RTRIM(JSON_VALUE(JSON,'$.FECHPROC')) ,103) As 'FECHPROC',
	CASE RTRIM( JSON_VALUE(JSON,'$.DETECCION') ) 
	WHEN 1 THEN 'Periodo postoperatorio de la admisión'
	WHEN 2 THEN 'Vigilancia posterior al alta' 
	WHEN 3 THEN 'Readmisión a la institución donde se realizó el procedimiento o la atención del parto' 
	WHEN 4 THEN 'Readmisión a una institución distinta de donde se realizó el procedimiento o la atención del parto' 
	END AS 'DETECCION',
	RTRIM( JSON_VALUE(JSON,'$.NOMBINSTITU') ) As 'NOMBINSTITU',	CASE RTRIM( JSON_VALUE(JSON,'$.TIPOPROC') ) WHEN 1 THEN 'X' END AS 'TIPOPROC1', CASE RTRIM( JSON_VALUE(JSON,'$.TIPOPROC') ) WHEN 2 THEN 'X' END AS 'TIPOPROC2',
	RTRIM( JSON_VALUE(JSON,'$.TIEMPARTO') ) As 'TIEMPARTO', RTRIM( JSON_VALUE(JSON,'$.TIEMRUTURA') ) As 'TIEMRUTURA',
	CASE RTRIM( JSON_VALUE(JSON,'$.DIABETES') ) WHEN 'True' THEN 'X' END AS 'DIABETESSI', CASE RTRIM( JSON_VALUE(JSON,'$.DIABETES') ) WHEN 'False' THEN 'X' END AS 'DIABETESNO',
	RTRIM( JSON_VALUE(JSON,'$.PESO') ) As 'PESO',RTRIM( JSON_VALUE(JSON,'$.TALLA') ) As 'TALLA',RTRIM( JSON_VALUE(JSON,'$.ORGANO') ) As 'ORGANO'

	From HCFICHA352 where IDFICHANOTIFICACION  = @IdFicha

END

--select * from hcfichanotificacion where id = '8'
--select * from HCFICHA352
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que recupera y formatea el detalle completo de una ficha de notificación SIVIGILA tipo 352 (Infección del Sitio Operatorio - ISO) a partir de su identificador. Consulta la tabla HCFICHA352 y transforma los campos codificados en valores legibles para impresión o visualización del formulario oficial, incluyendo: complejidad del servicio, vía de ingreso (urgencias, hospitalización, ambulatorio), tipo de procedimiento quirúrgico (cesárea, herniorrafia, colecistectomía, etc.), uso de profilaxis antibiótica, técnica laparoscópica, duración del procedimiento, factores de riesgo del paciente (diabetes, obesidad, desnutrición, anemia, inmunosupresión, preeclampsia), clasificación ASA del riesgo anestésico, tipo de herida quirúrgica, momento de administración de antibiótico, resultados microbiológicos de hasta tres muestras con su código de muestra y prueba, y datos adicionales almacenados en formato JSON como código CUPS, fecha del procedimiento, momento de detección de la infección, nombre de la institución y signos vitales del paciente. Se usa para generar o imprimir la ficha de vigilancia epidemiológica ISO requerida por el SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila352';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila352';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea los datos de una ficha de notificación SIVIGILA (evento 352, infecciones asociadas a procedimientos quirúrgicos/parto) para su presentación, traduciendo códigos a marcas o etiquetas legibles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila352';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA352 cuyo IDFICHANOTIFICACION coincida con el identificador recibido.; El campo JSON de la ficha debe contener una estructura válida con las claves esperadas (CODCUPS, FECHPROC, DETECCION, NOMBINSTITU, TIPOPROC, TIEMPARTO, TIEMRUTURA, DIABETES, PESO, TALLA, ORGANO).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila352';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La consulta siempre retorna a lo sumo una representación por ficha, filtrada por IDFICHANOTIFICACION.; Los valores codificados se exhiben como ''X'' o como descripción; cualquier código fuera del dominio enumerado se devuelve como NULL.; Las fechas (FECHATOMA1/2/3 y FECHPROC del JSON) se formatean siempre en estilo 103 (dd/mm/yyyy).; Los campos de texto se devuelven sin espacios finales (RTRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila352';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'SIVIGILA; Ficha de notificación epidemiológica; Infección de sitio quirúrgico; Profilaxis antibiótica; Clasificación ASA; Tipo de herida quirúrgica; Comorbilidades (diabetes, inmunosupresión, obesidad, desnutrición, preeclampsia, anemia); Procedimientos quirúrgicos (cesárea, herniorrafia, parto, revascularización, colecistectomía); Laparoscopia; Muestras y pruebas de laboratorio; Detección postoperatoria/readmisión; CUPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila352';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA352: Devuelve una fila por ficha que cumple IDFICHANOTIFICACION = @IdFicha, con valores codificados traducidos a ''X'' o a etiquetas descriptivas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila352';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si COMPLESERVI = ''1'' | ''2'' | ''3'' → Marca complejidad del servicio como Baja, Media o Alta respectivamente.; si SERVIADMIN = ''1'' | ''2'' | ''3'' → Clasifica el servicio de admisión como Programado Ambulatorio, Urgencias o Programado Hospitalario.; si PROCEDMEDI = ''1''..''5'' → Identifica el procedimiento médico: Cesárea, Herniorrafia, Parto, Revascularización o Colecistectomía.; si PROFILAXIS = ''1'' o ''0'' → Marca si se aplicó profilaxis (Sí/No).; si LAPAROSCOPIA = ''1'' o ''0'' → Marca si el procedimiento fue por laparoscopia (Sí/No).; si CLASIFASA = ''1''..''5'' → Clasifica al paciente en escala ASA 1 a ASA 5.; si TIPOHERIDA = ''1''..''4'' → Clasifica la herida como Limpia, Limpia-Contaminada, Contaminada o Sucia.; si TIEMPOADMIN = ''1''..''4'' → Indica el momento de administración de profilaxis: Antes, Durante, Después o Ninguna.; si CODMUESTRA1/2/3 = ''1''..''4'' → Traduce el código de muestra a los códigos institucionales 1, 4, 11 o 32.; si CODPRUEBA1/2/3 = ''1''..''6'' → Traduce el código de prueba a 55, 92, G3, 86, 90 o D4.; si JSON.DETECCION = 1..4 → Traduce el momento de detección a una de las cuatro descripciones (postoperatorio, posterior al alta, readmisión a la misma o a otra institución).; si JSON.TIPOPROC = 1 o 2 → Marca con ''X'' el tipo de procedimiento correspondiente.; si JSON.DIABETES = ''True'' o ''False'' → Marca con ''X'' la casilla de diabetes Sí o No según el valor booleano del JSON.; si Comorbilidades (DIABETES, INMUNOSUPRE, OBESIDAD, DESNUTRICION, PREECLAMPSIA, ANEMIA) = ''1'' → Marca con ''X'' la comorbilidad presente.; si Tipo de infección (SUPERPRIMA, SUPERSECUN, PROFUPRIMA, PROFUSECUN, ORGAESPAC) = ''1'' → Marca con ''X'' el tipo/sitio de infección quirúrgica (superficial primaria/secundaria, profunda primaria/secundaria, órgano-espacio).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila352';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA352', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila352';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila352';
-- GO
