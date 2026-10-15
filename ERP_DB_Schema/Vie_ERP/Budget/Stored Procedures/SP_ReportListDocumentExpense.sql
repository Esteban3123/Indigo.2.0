-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-06
-- Description:	Procedimiento para el reporte de listado de documentos del presupuesto de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentExpense]
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
			EXEC [Budget].[SP_ReportListDocumentExpenseAvailability] @xmlCriterias
		END
		ELSE IF @TypeDocument = 2
		BEGIN
			EXEC [Budget].[SP_ReportListDocumentExpenseAvailabilityModification] @xmlCriterias
		END
		ELSE IF @TypeDocument = 3
		BEGIN
			EXEC [Budget].[SP_ReportListDocumentExpenseCommitment] @xmlCriterias
		END
		ELSE IF @TypeDocument = 4
		BEGIN
			EXEC [Budget].[SP_ReportListDocumentExpenseCommitmentModification] @xmlCriterias
		END
		ELSE IF @TypeDocument = 5
		BEGIN
			EXEC [Budget].[SP_ReportListDocumentExpenseObligation] @xmlCriterias
		END
		ELSE IF @TypeDocument = 6
		BEGIN
			EXEC [Budget].[SP_ReportListDocumentExpenseObligationModification] @xmlCriterias
		END
		ELSE IF @TypeDocument = 7
		BEGIN
			EXEC [Budget].[SP_ReportListDocumentExpensePaymentOrder] @xmlCriterias
		END
		ELSE IF @TypeDocument = 8
		BEGIN
			EXEC [Budget].[SP_ReportListDocumentExpenseReimbursementResource] @xmlCriterias
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH
END