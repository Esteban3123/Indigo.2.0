
CREATE PROCEDURE [dbo].[SPHC_EstructuraPromptDatosClinicosRelevantesOtrosProc]
(
@Paciente Varchar(25),
@Ingreso Varchar(20),
@Folio Varchar(10)
)
AS
BEGIN
    SET NOCOUNT ON

			BEGIN
			
				CREATE TABLE #tmpPromptDatosRelevantes(FechaHistoria DateTime, Historias VARCHAR(MAX))
		
				INSERT INTO #tmpPromptDatosRelevantes
				EXEC [dbo].[SPHC_GenerarPromptDatosClinicosRelevantes] @Paciente, @Ingreso, @Folio

				declare @HistoriasPcte As VARCHAR(MAX) = (SELECT STRING_AGG(Historias, CHAR(13)+CHAR(13)) As 'HistoriasClinicas' FROM #tmpPromptDatosRelevantes)

				SELECT  @HistoriasPcte AS 'Prompt parametro 1'

				DROP TABLE #tmpPromptDatosRelevantes
			END

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recopila y estructura los datos clínicos relevantes de un paciente para generar un prompt consolidado destinado a un motor de inteligencia artificial o sistema de asistencia clínica. Recibe como parámetros el código del paciente (cédula/identificación), el número de ingreso y el folio de la historia clínica. Internamente llama al procedimiento SPHC_GenerarPromptDatosClinicosRelevantes para obtener los registros de historias clínicas asociadas, los concatena en un único bloque de texto separado por saltos de línea y devuelve ese texto unificado como ''Prompt parámetro 1'', listo para ser consumido por un proceso de generación de lenguaje natural o resumen clínico automatizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_EstructuraPromptDatosClinicosRelevantesOtrosProc';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_EstructuraPromptDatosClinicosRelevantesOtrosProc';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida las historias clínicas relevantes de un paciente/ingreso/folio en un único texto concatenado, separadas por dobles saltos de línea, para usarse como prompt de IA.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraPromptDatosClinicosRelevantesOtrosProc';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el procedimiento dependiente que genera las historias clínicas relevantes; Los identificadores de paciente, ingreso y folio deben corresponder a registros con historias clínicas asociadas para obtener resultado no nulo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraPromptDatosClinicosRelevantesOtrosProc';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las historias se separan siempre por doble retorno de carro (CHAR(13)+CHAR(13)); La tabla temporal se elimina al finalizar la ejecución; Sólo se retorna una columna con el texto agregado, sin metadatos de fecha', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraPromptDatosClinicosRelevantesOtrosProc';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso; Folio; Historia clínica; Datos clínicos relevantes; Prompt', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraPromptDatosClinicosRelevantesOtrosProc';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una única fila/columna con la concatenación (STRING_AGG con separador CHAR(13)+CHAR(13)) de todas las historias clínicas obtenidas del proc dependiente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraPromptDatosClinicosRelevantesOtrosProc';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SPHC_GenerarPromptDatosClinicosRelevantes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraPromptDatosClinicosRelevantesOtrosProc';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraPromptDatosClinicosRelevantesOtrosProc';
-- GO
