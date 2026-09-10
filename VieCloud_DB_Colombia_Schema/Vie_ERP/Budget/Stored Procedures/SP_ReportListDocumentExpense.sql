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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento orquestador para el reporte de listado de documentos del presupuesto de gastos. Recibe criterios de búsqueda en formato XML y, según el tipo de documento presupuestal indicado, delega la ejecución al subprocedimiento correspondiente: disponibilidad, modificación de disponibilidad, compromiso, modificación de compromiso, obligación, modificación de obligación, orden de pago o reembolso de recursos. Cubre todos los tipos de documentos del ciclo presupuestal de gastos (CDP, compromisos, obligaciones, órdenes de pago), permitiendo consultar el listado de documentos desde una sola entrada unificada según el tipo seleccionado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentExpense';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentExpense';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Despachador que, según el tipo de documento recibido por XML, delega la generación del reporte de listado de documentos del presupuesto de gastos al procedimiento especializado correspondiente.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener el nodo /Data/TypeDocument con un entero entre 1 y 8; Los procedimientos especializados de Budget para cada tipo de documento deben existir y aceptar el mismo XML de criterios', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se invoca un SP especializado por ejecución (las ramas son mutuamente excluyentes con ELSE IF); El mismo @xmlCriterias se propaga sin modificación al SP especializado; Ante cualquier error, la salida estandarizada es Code=''999'' más mensaje y línea, en lugar de propagar la excepción', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Presupuesto de gastos; Disponibilidad presupuestal; Modificación de disponibilidad; Compromiso; Modificación de compromiso; Obligación; Modificación de obligación; Orden de pago; Reintegro de recursos', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Si ocurre una excepción en TRY, se retorna un result set con Code=''999'' y Message = ERROR_MESSAGE() concatenado con el número de línea; [RETURN_RESULT] ResultSet: Cuando @TypeDocument está entre 1 y 8, se retorna el result set producido por el SP especializado invocado; si está fuera de ese rango, no se retorna ningún conjunto de datos', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TypeDocument = 1 → EXEC Budget.SP_ReportListDocumentExpenseAvailability (disponibilidad presupuestal); si @TypeDocument = 2 → EXEC Budget.SP_ReportListDocumentExpenseAvailabilityModification (modificación de disponibilidad); si @TypeDocument = 3 → EXEC Budget.SP_ReportListDocumentExpenseCommitment (compromiso); si @TypeDocument = 4 → EXEC Budget.SP_ReportListDocumentExpenseCommitmentModification (modificación de compromiso); si @TypeDocument = 5 → EXEC Budget.SP_ReportListDocumentExpenseObligation (obligación); si @TypeDocument = 6 → EXEC Budget.SP_ReportListDocumentExpenseObligationModification (modificación de obligación); si @TypeDocument = 7 → EXEC Budget.SP_ReportListDocumentExpensePaymentOrder (orden de pago); si @TypeDocument = 8 → EXEC Budget.SP_ReportListDocumentExpenseReimbursementResource (reintegro de recursos); si Cualquier error capturado en BEGIN CATCH → Retorna SELECT con Code ''999'' y mensaje de error con número de línea', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_ReportListDocumentExpenseAvailability; Budget.SP_ReportListDocumentExpenseAvailabilityModification; Budget.SP_ReportListDocumentExpenseCommitment; Budget.SP_ReportListDocumentExpenseCommitmentModification; Budget.SP_ReportListDocumentExpenseObligation; Budget.SP_ReportListDocumentExpenseObligationModification; Budget.SP_ReportListDocumentExpensePaymentOrder; Budget.SP_ReportListDocumentExpenseReimbursementResource', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpense';
-- GO
