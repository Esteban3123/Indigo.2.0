
-- Stored Procedure
-- =============================================
-- Autor:		Yezid Garcia Medina
-- Fecha Creación: 08-Febrero-2022
-- Descripción:	Sp que me lista la Información de las Fichas del Sivigila, Ficha 607
-- Modificó:    ---
-- Fecha Modificación : ---
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila607]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 

			Select
				CASE CONTACASO  WHEN '1' THEN 'X' END AS 'CONTACASO Si',CASE CONTACASO WHEN '2' THEN 'X' END AS 'CONTACASO No', CASE CONTACASO WHEN '3' THEN 'X' END AS 'CONTACASO Desconocido',
				CASE DOSISAPLI  WHEN 1 THEN 'X' END AS 'Ninguna', 
				CASE DOSISAPLI  WHEN 2 THEN 'X' END AS 'Una', 
				CASE DOSISAPLI  WHEN 3 THEN 'X' END AS 'Dos',
				CASE DOSISAPLI  WHEN 4 THEN 'X' END AS 'Tres o Más', 
				CASE DOSISAPLI  WHEN 5 THEN 'X' END AS 'Primer_Refuerzo', 
				CASE DOSISAPLI  WHEN 6 THEN 'X' END AS 'Segundo_Refuerzo',
				CASE FIEBRE  WHEN '1' THEN 'X' END AS 'FIEBRE Si',CASE FIEBRE WHEN '2' THEN 'X' END AS 'FIEBRE No', CASE FIEBRE WHEN '3' THEN 'X' END AS 'FIEBRE Desconocido',
				CASE AMIGDALITIS  WHEN '1' THEN 'X' END AS 'AMIGDALITIS Si',CASE AMIGDALITIS WHEN '2' THEN 'X' END AS 'AMIGDALITIS No', CASE AMIGDALITIS WHEN '3' THEN 'X' END AS 'AMIGDALITIS Desconocido',
				CASE FARINGITIS  WHEN '1' THEN 'X' END AS 'FARINGITIS Si',CASE FARINGITIS WHEN '2' THEN 'X' END AS 'FARINGITIS No', CASE FARINGITIS WHEN '3' THEN 'X' END AS 'FARINGITIS Desconocido',
				CASE LARINGITIS  WHEN '1' THEN 'X' END AS 'LARINGITIS Si',CASE LARINGITIS WHEN '2' THEN 'X' END AS 'LARINGITIS No', CASE LARINGITIS WHEN '3' THEN 'X' END AS 'LARINGITIS Desconocido',
				CASE PRESEMEMB  WHEN '1' THEN 'X' END AS 'PRESEMEMB Si',CASE PRESEMEMB WHEN '2' THEN 'X' END AS 'PRESEMEMB No', CASE PRESEMEMB WHEN '3' THEN 'X' END AS 'PRESEMEMB Desconocido',
				CASE COMPLICA  WHEN '1' THEN 'X' END AS 'COMPLICA Si',CASE COMPLICA WHEN '2' THEN 'X' END AS 'COMPLICA No', CASE COMPLICA WHEN '3' THEN 'X' END AS 'COMPLICA Desconocido',
				CASE TIPOCOMPLI  WHEN '1' THEN 'X' END AS 'Neurologica',CASE TIPOCOMPLI WHEN '2' THEN 'X' END AS 'Renal', CASE TIPOCOMPLI WHEN '3' THEN 'X' END AS 'Cardiaca',
				CASE TIPOCOMPLI  WHEN '4' THEN 'X' END AS 'Traquetomia',CASE TIPOCOMPLI WHEN '5' THEN 'X' END AS 'Otro',
				convert(varchar(10),FECHATOMA,103) As 'FECHATOMA', convert(varchar(10),FECHARECE,103) As 'FECHARECE', Rtrim(MUESTRA) As 'MUESTRA',
				Rtrim(PRUEBA) As 'PRUEBA', Rtrim(AGENTE) As 'AGENTE', Rtrim(RESULTADO) As 'RESULTADO', 
				convert(varchar(10),FECHARESUL,103) As 'FECHARESUL', Rtrim(VALOR) As 'VALOR', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.CARNET_VACUNACION') AS BIT ) WHEN 1 THEN 'X' END AS 'Carnet_SI', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.CARNET_VACUNACION') AS BIT ) WHEN 0 THEN 'X' END AS 'Carnet_NO',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.TIPO_VACUNA') AS int ) WHEN 1 THEN 'X' END AS 'Tipo_DPT',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.TIPO_VACUNA') AS int ) WHEN 2 THEN 'X' END AS 'Tipo_Pentavalente',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.TIPO_VACUNA') AS int ) WHEN 3 THEN 'X' END AS 'Tipo_TD',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.TIPO_VACUNA') AS int ) WHEN 4 THEN 'X' END AS 'Tipo_Otra',
				RTRIM( JSON_VALUE(JSON,'$.OTRA_VACUNA') ) AS 'Otra_Vacuna',	
				FORMAT( TRY_CONVERT( date, JSON_VALUE(JSON,'$.FECHA_ULTIMA_DOSIS') ),'dd/MM/yyyy') As 'Fecha_Ultima_Dosis'        
			From 
				HCFICHA607
			Where 
				IDFICHANOTIFICACION  = @IdFicha 
				
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle completo de la ficha epidemiológica 607 del SIVIGILA correspondiente a casos sospechosos o confirmados de difteria y enfermedades similares. A partir del identificador interno de la ficha (@IdFicha), consulta la tabla HCFICHA607 y devuelve en formato legible los síntomas clínicos del paciente (fiebre, amigdalitis, faringitis, laringitis, presencia de membrana), las complicaciones presentadas (neurológica, renal, cardíaca, traqueotomía u otra), el historial de vacunación (dosis aplicadas, tipo de vacuna DPT/Pentavalente/TD, carnet de vacunación y fecha de última dosis) y los resultados de laboratorio (muestra, prueba, agente, resultado y fechas de toma y recepción). Se utiliza para imprimir o visualizar la ficha de notificación obligatoria ante las autoridades de salud pública en el marco del sistema de vigilancia epidemiológica nacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila607';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila607';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta y formatea para presentación los datos clínico-epidemiológicos de una ficha Sivigila 607 (difteria), incluyendo síntomas, complicaciones, muestras de laboratorio y antecedentes de vacunación almacenados en JSON.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila607';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro de ficha 607 cuyo identificador coincida con el parámetro de entrada; El campo JSON debe tener estructura válida con claves CARNET_VACUNACION, TIPO_VACUNA, OTRA_VACUNA y FECHA_ULTIMA_DOSIS para poder extraer los datos de vacunación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila607';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las fechas se exponen en formato dd/MM/yyyy (estilo 103 o FORMAT); Las opciones excluyentes (Si/No/Desconocido, tipo de dosis, tipo de complicación, tipo de vacuna) se representan con ''X'' solo en la columna correspondiente al valor almacenado; Los campos de texto se entregan sin espacios finales (RTRIM); Los valores del JSON se interpretan con TRY_CAST/TRY_CONVERT para tolerar contenido inválido sin fallar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila607';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila 607; Contacto con caso; Dosis aplicadas de vacuna; Síntomas (fiebre, amigdalitis, faringitis, laringitis); Presencia de membranas; Complicaciones (neurológica, renal, cardíaca, traqueotomía, otra); Muestra de laboratorio; Carnet de vacunación; Tipo de vacuna (DPT, Pentavalente, TD, Otra); Fecha última dosis', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila607';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA607: Cuando IDFICHANOTIFICACION coincide con el id solicitado, retorna una fila con los datos de la ficha 607 transformados para presentación (marcas ''X'' por opción, fechas en dd/MM/yyyy y campos del JSON de vacunación extraídos)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila607';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA607', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila607';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila607';
-- GO
