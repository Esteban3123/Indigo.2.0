-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-30
-- Description:	Procedimiento para el reporte de listado de documentos del presupuesto de ingresos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentIncome]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @TypeDocument INT

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 
			@TypeDocument = t.x.value('TypeDocument[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		IF @TypeDocument = 1
		BEGIN
			EXEC [Budget].[SP_ReportListDocumentIncomeRecognition] @xmlCriterias
		END
		ELSE IF @TypeDocument = 2
		BEGIN
			EXEC [Budget].[SP_ReportListDocumentIncomeRecognitionModification] @xmlCriterias
		END
		ELSE IF @TypeDocument = 3
		BEGIN
			EXEC [Budget].[SP_ReportListDocumentIncomeCollection] @xmlCriterias
		END
		ELSE IF @TypeDocument = 4
		BEGIN
			EXEC [Budget].[SP_ReportListDocumentIncomeCollectionModification] @xmlCriterias
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH
END