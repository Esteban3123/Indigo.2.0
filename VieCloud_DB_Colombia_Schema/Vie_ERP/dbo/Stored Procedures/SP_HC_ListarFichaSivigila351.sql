-- Stored Procedure

-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,13-12-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila351]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 

	--Select 	*	From HCFICHA351 where IDFICHANOTIFICACION  = 13

	Select
	CASE COMPLESERVI WHEN '1' THEN 'X' END AS 'Baja',CASE COMPLESERVI WHEN '2' THEN 'X' END AS 'Media', CASE COMPLESERVI WHEN '3' THEN 'X' END AS 'Alta',
	CASE SERVIADMIN WHEN '1' THEN 'X' END AS 'ProgramAmbula',CASE SERVIADMIN WHEN '2' THEN 'X' END AS 'Urgencias', CASE SERVIADMIN WHEN '3' THEN 'X' END AS 'ProgramHospital',
	CASE PROCEDMEDI WHEN '1' THEN 'X' END AS 'Cesárea',CASE PROCEDMEDI WHEN '2' THEN 'X' END AS 'Herniorrafia', CASE PROCEDMEDI WHEN '3' THEN 'X' END AS 'Parto',
	CASE PROCEDMEDI WHEN '4' THEN 'X' END AS 'Revascularizacion',CASE PROCEDMEDI WHEN '5' THEN 'X' END AS 'Colecistectomia',
	CASE TIPOPARTO  WHEN '1' THEN 'X' END AS 'TIPOPARTO si',CASE TIPOPARTO WHEN '0' THEN 'X' END AS 'TIPOPARTO no',
	CASE PACIREQUI  WHEN '1' THEN 'X' END AS 'PACIREQUI si',CASE PACIREQUI WHEN '0' THEN 'X' END AS 'PACIREQUI no',
	CASE PROFILAXIS  WHEN '1' THEN 'X' END AS 'PROFILAXIS si',CASE PROFILAXIS WHEN '0' THEN 'X' END AS 'PROFILAXIS no',
	CASE LAPAROSCOPIA  WHEN '1' THEN 'X' END AS 'LAPAROSCOPIA si',CASE LAPAROSCOPIA WHEN '0' THEN 'X' END AS 'LAPAROSCOPIA no',
	cast(TIEMPODURA as time) As 'TIEMPODURA',  cast(TIEMPORUPT as time) As 'TIEMPORUPT', cast(DURAPROCE as time) As 'DURAPROCE',
	convert(varchar(10),FECHAPROCE,103) As 'FECHAPROCE', convert(varchar(10),FECHATOMA1,103) As 'FECHATOMA1', convert(varchar(10),FECHATOMA2,103) As 'FECHATOMA2', 
	convert(varchar(10),FECHATOMA3,103) As 'FECHATOMA3', 
	Rtrim(CODNOM) As 'CODNOM', Rtrim(CUAL) As 'CUAL', Rtrim(MICROORGA1) As 'MICROORGA1', Rtrim(MICROORGA2) As 'MICROORGA2', Rtrim(MICROORGA3) As 'MICROORGA3',
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
	WHEN '5' THEN '90' WHEN '6' THEN 'D4' END AS 'CODPRUEBA3'

	From HCFICHA351 where IDFICHANOTIFICACION  = @IdFicha

END

--select * from hcfichanotificacion where id = '8'
--select * from HCFICHA351
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera y formatea los datos de una ficha SIVIGILA tipo 351 para impresión o visualización del formulario oficial de notificación de Infecciones del Sitio Quirúrgico (ISQ), que son infecciones asociadas a la atención en salud (IAAS). Dado el identificador interno de la ficha, consulta la tabla HCFICHA351 y transforma todos los campos codificados en valores legibles: convierte indicadores numéricos en marcas ''X'' para los formularios (complejidad del servicio, vía de ingreso, tipo de procedimiento quirúrgico, factores de riesgo del paciente como diabetes, obesidad, desnutrición, anemia, preeclampsia e inmunosupresión, clasificación ASA del riesgo anestésico, tipo de herida, profilaxis antibiótica y microorganismos aislados en muestras de laboratorio). Se usa para imprimir o consultar la ficha epidemiológica completa de ISQ que debe reportarse al sistema de vigilancia en salud pública SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila351';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila351';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea los datos de una ficha de notificación Sivigila (infecciones del sitio quirúrgico) traduciendo códigos internos a marcas ''X'' y a códigos estándar Sivigila para impresión/presentación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila351';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA351 cuyo IDFICHANOTIFICACION coincida con el identificador recibido; Los códigos de los campos categóricos (COMPLESERVI, SERVIADMIN, PROCEDMEDI, CLASIFASA, TIPOHERIDA, TIEMPOADMIN, CODMUESTRA*, CODPRUEBA*) deben pertenecer a los dominios definidos; valores fuera de rango producen NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila351';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las columnas booleanas se devuelven como ''X'' o NULL (nunca como 0/1) para presentación en formato ficha; Los campos de tiempo (TIEMPODURA, TIEMPORUPT, DURAPROCE) se devuelven con formato TIME; Las fechas (FECHAPROCE, FECHATOMA1/2/3) se devuelven en formato dd/mm/yyyy (estilo 103); Los códigos internos de muestra y prueba se traducen siempre a la codificación oficial Sivigila antes de devolverse; Los campos de texto libre (CODNOM, CUAL, MICROORGA1/2/3) se devuelven sin espacios a la derecha (RTRIM); Filtra exclusivamente por IDFICHANOTIFICACION; el parámetro @NumFicha no se utiliza', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila351';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Infección del sitio quirúrgico (ISO); Clasificación ASA; Tipo de herida quirúrgica (limpia, limpia-contaminada, contaminada, sucia); Profilaxis antibiótica (antes/durante/después); Procedimientos quirúrgicos (cesárea, parto, herniorrafia, revascularización, colecistectomía); Factores de riesgo (diabetes, inmunosupresión, obesidad, desnutrición, preeclampsia, anemia); Microorganismos aislados; Muestras y pruebas de laboratorio (codificación Sivigila); Complejidad del servicio; Tipo de atención (ambulatoria, urgencias, hospitalaria)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila351';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA351: Cuando IDFICHANOTIFICACION = @IdFicha, retorna una fila con campos transformados: marcas ''X'' para selecciones categóricas/booleanas, fechas en formato 103, tiempos como TIME y códigos Sivigila traducidos para muestra y prueba', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila351';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si COMPLESERVI ∈ {''1'',''2'',''3''} → Marca con ''X'' la complejidad del servicio: 1=Baja, 2=Media, 3=Alta; si SERVIADMIN ∈ {''1'',''2'',''3''} → Marca tipo de servicio donde se administró: 1=Programado Ambulatorio, 2=Urgencias, 3=Programado Hospitalario; si PROCEDMEDI ∈ {''1''..''5''} → Marca el procedimiento médico: 1=Cesárea, 2=Herniorrafia, 3=Parto, 4=Revascularización, 5=Colecistectomía; si CLASIFASA ∈ {''1''..''5''} → Marca la clasificación ASA del paciente (ASA 1 a ASA 5); si TIPOHERIDA ∈ {''1''..''4''} → Marca tipo de herida: 1=Limpia, 2=Limpia-Contaminada, 3=Herida Contaminada, 4=Herida Sucia; si TIEMPOADMIN ∈ {''1''..''4''} → Marca el momento de administración de profilaxis: 1=Antes, 2=Durante, 3=Después, 4=Ninguna; si CODMUESTRA1/2/3 ∈ {''1'',''2'',''3'',''4''} → Traduce el código interno de muestra al código estándar Sivigila: 1→1, 2→4, 3→11, 4→32; si CODPRUEBA1/2/3 ∈ {''1''..''6''} → Traduce el código interno de prueba al código estándar Sivigila: 1→55, 2→92, 3→G3, 4→86, 5→90, 6→D4; si Campos booleanos (TIPOPARTO, PACIREQUI, PROFILAXIS, LAPAROSCOPIA) = ''1'' o ''0'' → Genera columnas separadas ''si''/''no'' marcadas con ''X'' según el valor; si Factores de riesgo (DIABETES, INMUNOSUPRE, OBESIDAD, DESNUTRICION, PREECLAMPSIA, ANEMIA) = ''1'' → Marca con ''X'' el factor de riesgo presente; si Tipo de infección (SUPERPRIMA, SUPERSECUN, PROFUPRIMA, PROFUSECUN, ORGAESPAC) = ''1'' → Marca con ''X'' la categoría de infección del sitio quirúrgico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila351';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA351', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila351';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila351';
-- GO
