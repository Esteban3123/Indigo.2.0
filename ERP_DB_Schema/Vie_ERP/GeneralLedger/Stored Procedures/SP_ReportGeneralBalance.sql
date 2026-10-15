-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-17
-- Description:	Procedimiento para el reporte de balance general comparativo
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportGeneralBalance]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ReportType TINYINT

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT	@ReportType = t.x.value('ReportType[1]','tinyint')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		IF @ReportType = 1 OR @ReportType = 2
		BEGIN
			EXEC [GeneralLedger].[SP_ReportGeneralBalanceMonthly] @xmlCriterias
		END
		ELSE IF @ReportType = 3
		BEGIN
			EXEC [GeneralLedger].[SP_ReportGeneralBalanceComparative] @xmlCriterias
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH
END