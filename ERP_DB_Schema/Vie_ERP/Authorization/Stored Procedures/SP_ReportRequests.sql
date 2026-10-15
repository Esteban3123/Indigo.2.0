-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-07-15
-- Description:	Procedimiento para el reporte de autorizaciones (solicitudes)
-- =============================================
CREATE PROCEDURE [Authorization].[SP_ReportRequests]
	@xmlCriterias AS XML
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
			EXEC [Authorization].[SP_ReportRequestSummary] @xmlCriterias
		END
		ELSE IF @TypeReport = 2
		BEGIN
			EXEC [Authorization].[SP_ReportRequestDetailed] @xmlCriterias
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, CONCAT('SP_ReportRequests: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE()) AS Message
	END CATCH
END