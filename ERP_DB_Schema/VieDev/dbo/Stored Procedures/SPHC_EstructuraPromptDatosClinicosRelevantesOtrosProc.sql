
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