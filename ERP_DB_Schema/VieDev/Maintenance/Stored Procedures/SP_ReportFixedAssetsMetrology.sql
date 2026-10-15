-- =============================================
-- Author:		Giovanny Plazas Lozano
-- Create date: 2021-02-11
-- Description:	Procedimiento para el INFORME CERTIFICADOS DE CALIBRACION EQUIPOS BIOMEDICOS
-- =============================================
CREATE PROCEDURE [Maintenance].[SP_ReportFixedAssetsMetrology]
		@xmlCriterias AS XML,
		@xmlFilters AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ReportType INT

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 
			@ReportType = t.x.value('ReportType[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		IF @ReportType = 1
		BEGIN
			EXEC [Maintenance].[SP_ReportFixedAssetsMetrologySummary] @xmlCriterias, @xmlFilters
		END
		ELSE IF @ReportType = 2
		BEGIN
			EXEC [Maintenance].[SP_ReportFixedAssetsMetrologyDetail] @xmlCriterias, @xmlFilters
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH
END