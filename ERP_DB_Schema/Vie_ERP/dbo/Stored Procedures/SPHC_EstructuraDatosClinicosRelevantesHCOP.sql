
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