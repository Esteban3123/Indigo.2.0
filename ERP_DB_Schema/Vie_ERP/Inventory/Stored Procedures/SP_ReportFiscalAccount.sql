-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-03-04
-- Description:	Procedimiento para el reporte de cuenta fiscal
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ReportFiscalAccount]
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
			EXEC [Inventory].[SP_ReportFiscalAccountSummary] @xmlCriterias
		END
		ELSE IF @TypeReport = 2
		BEGIN
			EXEC [Inventory].[SP_ReportFiscalAccountDetailed] @xmlCriterias
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH
END