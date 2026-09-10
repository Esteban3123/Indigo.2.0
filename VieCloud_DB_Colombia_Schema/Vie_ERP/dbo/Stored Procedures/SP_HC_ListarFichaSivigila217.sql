-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,26-10-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila217]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 
	Select
		CASE FIEBRE  WHEN '1' THEN 'X' END AS 'FIEBRE', CASE ARTRALGIA  WHEN '1' THEN 'X' END AS 'ARTRALGIA',CASE CEFALEA  WHEN '1' THEN 'X' END AS 'CEFALEA',
		CASE RASH  WHEN '1' THEN 'X' END AS 'RASH',CASE VOMITO  WHEN '1' THEN 'X' END AS 'VOMITO',
		CASE PIEL  WHEN '1' THEN 'X' END AS 'PIEL',CASE HIGADO  WHEN '1' THEN 'X' END AS 'HIGADO', CASE BAZO  WHEN '1' THEN 'X' END AS 'BAZO',
		CASE PULMON  WHEN '1' THEN 'X' END AS 'PULMON',CASE CEREBRO  WHEN '1' THEN 'X' END AS 'CEREBRO', CASE MIOCARDIO  WHEN '1' THEN 'X' END AS 'MIOCARDIO',
		CASE MEDULA  WHEN '1' THEN 'X' END AS 'MEDULA', CASE RINON  WHEN '1' THEN 'X' END AS 'RINON', 
		VERSION AS 'VERSION', 
		JSON AS 'JSON' 
	
	From 
		HCFICHA217 
	Where 
		IDFICHANOTIFICACION  = @IdFicha

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera los síntomas clínicos y órganos afectados registrados en la ficha de notificación epidemiológica SIVIGILA (formulario 217), utilizada para reportar enfermedades transmitidas por vectores como dengue, zika o chikungunya. Recibe el identificador interno de la ficha (@IdFicha) y el número de ficha (@NumFicha), y consulta la tabla HCFICHA217 para devolver, en formato marcado (''X'' o nulo), si el paciente presentó síntomas como fiebre, artralgia, cefalea, rash, vómito, y si hubo compromiso de órganos como piel, hígado, bazo, pulmón, cerebro, miocardio, médula o riñón. También expone la versión del formulario y el contenido JSON asociado al registro, permitiendo la impresión o visualización de la ficha epidemiológica oficial del caso notificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila217';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila217';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los datos clínicos y signos/síntomas registrados en la ficha de notificación 217 del Sivigila, formateando indicadores positivos como ''X''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila217';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA217 cuyo IDFICHANOTIFICACION coincida con el identificador recibido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila217';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera presencia del síntoma/afectación cuando el valor es exactamente ''1''; El parámetro NumFicha se recibe pero no participa en el filtro ni en la salida', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila217';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Síntomas clínicos (fiebre, artralgia, cefalea, rash, vómito); Órganos afectados (piel, hígado, bazo, pulmón, cerebro, miocardio, médula, riñón); Versión de ficha; JSON de ficha', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila217';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA217: Filtra por IDFICHANOTIFICACION = @IdFicha y retorna síntomas/órganos afectados; cada campo con valor ''1'' se traduce a ''X'', en otro caso queda NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila217';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Cada campo clínico (FIEBRE, ARTRALGIA, CEFALEA, RASH, VOMITO, PIEL, HIGADO, BAZO, PULMON, CEREBRO, MIOCARDIO, MEDULA, RINON) = ''1'' → Se muestra ''X'' en la columna correspondiente else Se devuelve NULL en la columna correspondiente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila217';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA217', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila217';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila217';
-- GO
