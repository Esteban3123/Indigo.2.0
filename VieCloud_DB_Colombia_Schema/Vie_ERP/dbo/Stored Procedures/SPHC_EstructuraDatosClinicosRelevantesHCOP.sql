
CREATE PROCEDURE [dbo].[SPHC_EstructuraDatosClinicosRelevantesHCOP]
(
@Paciente Varchar(25),
@Ingreso Varchar(20),
@Folio Varchar(10)
)
AS
BEGIN
    SET NOCOUNT ON

			BEGIN
			
				CREATE TABLE #tmpDatosRelevantesHCOP(FechaHistoria DateTime, Historias VARCHAR(MAX))
		
				INSERT INTO #tmpDatosRelevantesHCOP
				EXEC [dbo].[SPHC_GenerarDatosClinicosRelevantesHCOP] @Paciente, @Ingreso, @Folio

				declare @HistoriasPcte As VARCHAR(MAX) = (SELECT STRING_AGG(Historias, CHAR(13)+CHAR(13)) As 'HistoriasClinicas' FROM #tmpDatosRelevantesHCOP)

				SELECT  @HistoriasPcte AS 'Parametro 1 HCOP'

				DROP TABLE #tmpDatosRelevantesHCOP
			END

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estructura y consolida los datos clínicos relevantes de la historia clínica de hospitalización (HCOP) de un paciente. Recibe como parámetros el código o cédula del paciente, el número de ingreso y el folio de la historia clínica, y delega la generación del contenido clínico al procedimiento SPHC_GenerarDatosClinicosRelevantesHCOP. Los registros obtenidos se concatenan en un único bloque de texto (separados por saltos de línea) y se devuelven como un solo campo llamado ''Parametro 1 HCOP'', listo para ser consumido por formularios clínicos, impresión de historia clínica o integración con otros módulos del EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_EstructuraDatosClinicosRelevantesHCOP';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_EstructuraDatosClinicosRelevantesHCOP';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'"Consolida en un único texto las historias clínicas relevantes de un paciente/ingreso/folio para presentarlas como parámetro de salida en una HCOP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraDatosClinicosRelevantesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el procedimiento dependiente que genera los datos clínicos relevantes y devolver un resultset con FechaHistoria y Historias.; Los identificadores de paciente, ingreso y folio deben ser válidos para que el procedimiento dependiente retorne información.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraDatosClinicosRelevantesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las historias clínicas se concatenan separadas por doble salto de línea (CHAR(13)+CHAR(13)) en un único valor de salida.; La tabla temporal usada para acumular las historias se descarta al finalizar la ejecución.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraDatosClinicosRelevantesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso; Historia clínica; Datos clínicos relevantes; HCOP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraDatosClinicosRelevantesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Tras invocar el procedimiento generador y agregar las historias con STRING_AGG separadas por doble CR, se retorna un único resultset con la cadena consolidada bajo el alias ''Parametro 1 HCOP''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraDatosClinicosRelevantesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SPHC_GenerarDatosClinicosRelevantesHCOP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraDatosClinicosRelevantesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraDatosClinicosRelevantesHCOP';
-- GO
