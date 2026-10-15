-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-24
-- Description:	Procedimiento para el reporte de la circular unica
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportSingleCircular]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Archive INT

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 
			@Archive = t.x.value('Archive[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		IF @Archive = 1
		BEGIN
			EXEC [GeneralLedger].[SP_ReportSingleCircularFT001] @xmlCriterias
		END
			ELSE IF @Archive = 3
		BEGIN
			EXEC [GeneralLedger].[SP_ReportSingleCircularFT003] @xmlCriterias
		END
		ELSE IF @Archive = 4
		BEGIN
			EXEC [GeneralLedger].[SP_ReportSingleCircularFT004] @xmlCriterias
		END
		ELSE IF @Archive = 6
		BEGIN
			EXEC [GeneralLedger].[SP_ReportSingleCircularFT006] @xmlCriterias
		END
		ELSE IF @Archive = 7
		BEGIN
			EXEC [GeneralLedger].[SP_ReportSingleCircularFT007] @xmlCriterias
		END
		ELSE IF @Archive = 8
		BEGIN
			EXEC [GeneralLedger].[SP_ReportSingleCircularFT008] @xmlCriterias
		END
		ELSE IF @Archive = 9
		BEGIN
			EXEC [GeneralLedger].[SP_ReportSingleCircularFT009] @xmlCriterias
		END
		ELSE IF @Archive = 10
		BEGIN
			EXEC [GeneralLedger].[SP_ReportSingleCircularFT010] @xmlCriterias
		END
		ELSE IF @Archive = 25
		BEGIN
			EXEC [GeneralLedger].[SP_ReportSingleCircularFT025] @xmlCriterias
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH
END