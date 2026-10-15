-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-27
-- Description:	Procedimiento para el reporte de estimacion de costos por tipo
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReportEstimateCosts]
	@xmlCriterias AS XML,
	@xmlFilters AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @TypeReport INT

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 
			@TypeReport = t.x.value('TypeReport[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		IF @TypeReport = 1
		BEGIN
			EXEC [Cost].[SP_ReportEstimateCostsPrimarySummary] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 2
		BEGIN
			EXEC [Cost].[SP_ReportEstimateCostsPrimaryDetailed] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 3
		BEGIN
			EXEC [Cost].[SP_ReportEstimateCostsSecondarySummary] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 4
		BEGIN
			EXEC [Cost].[SP_ReportEstimateCostsSecondaryDetailed] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 5
		BEGIN
			EXEC [Cost].[SP_ReportEstimateCostsFinalSummary] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 6
		BEGIN
			EXEC [Cost].[SP_ReportEstimateCostsFinalDetailed] @xmlCriterias, @xmlFilters
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH
END