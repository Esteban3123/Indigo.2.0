-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,16-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila215]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON; 
		
			Select
				Rtrim(NOMMAD) As 'NOMMAD', 
				CASE Rtrim(TIPID)  WHEN '1' THEN 'RC - Registro Civil' WHEN '2' THEN 'TI - Tarjeta de Identidad' WHEN '3' THEN 'CC - Cédula de Ciudadanía' WHEN '4' THEN 'CE - Cédula de Extranjería' WHEN '5' THEN 'PA - Pasaporte' WHEN '6' THEN 'MS - Menor Sin Identificación' WHEN '7' THEN 'AS - Adulto Sin Identificación' WHEN '8' THEN 'PE - Permiso Especial de Permanencia' WHEN '9' THEN 'CN - Certificado de Nacido Vivo' WHEN 10 THEN 'PT - Permiso Temporal de Permanencia' END AS 'TIPID', 				
				Rtrim(NUMIDENT) As 'NUMIDENT', Rtrim(EDAD) As 'EDAD',
				Rtrim(NUMEMB) As 'NUMEMB', Rtrim(NACVIVOS) As 'NACVIVOS', Rtrim(ABORTOS) As 'ABORTOS', Rtrim(MORTINAT) As 'MORTINAT',
				CASE DIAGNOS WHEN '1' THEN 'X' END AS 'DIAGNOS Si',CASE DIAGNOS WHEN '0' THEN 'X' END AS 'DIAGNOS No', Rtrim(EDADGESTDIA) As 'EDADGESTDIA',
				CASE PATCRON WHEN '1' THEN 'X' END AS 'PATCRON Si',CASE PATCRON WHEN '0' THEN 'X' END AS 'PATCRON No', Rtrim(CUALES) As 'CUALES',
				CASE EMBMULT WHEN '1' THEN 'X' END AS 'EMBMULT Si',CASE EMBMULT WHEN '0' THEN 'X' END AS 'EMBMULT No',
				CASE NATIVIVO WHEN '1' THEN 'X' END AS 'si', CASE NATIVIVO WHEN '2' THEN 'X' END AS 'no', CASE NATIVIVO WHEN '3' THEN 'X' END AS 'No ha nacido',
				Rtrim(EDADGESTNAC) As 'EDADGESTNAC', Rtrim(PESO) As 'PESO', Rtrim(PERIMCEF) As 'PERIMCEF',
				Rtrim(DESC1) As 'DESC1', Rtrim(DESC2) As 'DESC2', Rtrim(DESC3) As 'DESC3', Rtrim(DESC4) As 'DESC4',
				Rtrim(DESC5) As 'DESC5', Rtrim(DESC6) As 'DESC6', Rtrim(DESC7) As 'DESC7', Rtrim(DESC8) As 'DESC8',
				CASE STORCH WHEN '1' THEN 'X' END AS 'STORCH Si',CASE STORCH WHEN '0' THEN 'X' END AS 'STORCH No',
				CASE TSH1 WHEN '1' THEN 'X' END AS 'TSH1 Si',CASE TSH1 WHEN '0' THEN 'X' END AS 'TSH1 No',
				CASE TOTSUE1 WHEN '1' THEN 'X' END AS 'TOTSUE1 Si',CASE TOTSUE1 WHEN '0' THEN 'X' END AS 'TOTSUE1 No',
				CASE LIBSUE1 WHEN '1' THEN 'X' END AS 'LIBSUE1 Si',CASE LIBSUE1 WHEN '0' THEN 'X' END AS 'LIBSUE1 No',
				CASE TSH2 WHEN '1' THEN 'X' END AS 'TSH2 Si',CASE TSH2 WHEN '0' THEN 'X' END AS 'TSH2 No',
				CASE TOTSUE2 WHEN '1' THEN 'X' END AS 'TOTSUE2 Si',CASE TOTSUE2 WHEN '0' THEN 'X' END AS 'TOTSUE2 No',
				CASE LIBSUE2 WHEN '1' THEN 'X' END AS 'LIBSUE2 Si',CASE LIBSUE2 WHEN '0' THEN 'X' END AS 'LIBSUE2 No', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON' 

			From 
				HCFICHA215 

			Where 
				IDFICHANOTIFICACION  = @IdFicha 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que consulta y devuelve el contenido de la ficha de notificación SIVIGILA número 215, correspondiente al seguimiento perinatal y neonatal para vigilancia epidemiológica. Recupera datos de la madre (nombre, tipo y número de identificación, edad) y del embarazo (número de embarazos, nacidos vivos, abortos, mortinatos, edad gestacional, patologías crónicas, embarazo múltiple). También incluye resultados de tamizajes neonatales como STORCH, TSH (hipotiroidismo congénito) y pruebas de suero libre, así como datos antropométricos del recién nacido (peso, perímetro cefálico). Se usa para imprimir o visualizar la ficha oficial de reporte obligatorio ante las autoridades de salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila215';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila215';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta y retorna los datos de una ficha de notificación SIVIGILA 215 (mortalidad perinatal/neonatal) formateados para presentación, traduciendo códigos a etiquetas legibles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila215';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en la tabla de fichas 215 con el identificador de notificación recibido; El identificador de ficha debe ser numérico válido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila215';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna información de la ficha cuyo identificador coincide exactamente con el parámetro entregado; Los valores de texto se entregan sin espacios finales (RTRIM); Los códigos de tipo de identificación y banderas booleanas se traducen a etiquetas legibles antes de salir del procedimiento; No realiza modificaciones de datos (solo lectura)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila215';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha SIVIGILA 215; Notificación epidemiológica; Tipo de identificación del paciente; Embarazos / nacidos vivos / abortos / mortinatos; Edad gestacional; Patologías crónicas; Embarazo múltiple; Tamizaje neonatal (TSH, TORCH, T4 total/libre); Peso y perímetro cefálico del recién nacido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila215';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA215: Cuando IDFICHANOTIFICACION coincide con el parámetro de entrada, retorna un único conjunto de resultados con los campos de la ficha 215 transformados (RTRIM y traducción de códigos a ''X''/etiquetas)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila215';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPID en {''1''..''10''} → Se traduce el código a la etiqueta del tipo de documento (RC, TI, CC, CE, PA, MS, AS, PE, CN, PT); si Campos booleanos (DIAGNOS, PATCRON, EMBMULT, STORCH, TSH1/2, TOTSUE1/2, LIBSUE1/2) = ''1'' o ''0'' → Se marca con ''X'' la columna ''Si'' o ''No'' correspondiente para representación tipo formulario; si NATIVIVO = ''1'' / ''2'' / ''3'' → Se marca ''X'' en columna ''si'', ''no'' o ''No ha nacido'' respectivamente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila215';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA215', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila215';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila215';
-- GO
