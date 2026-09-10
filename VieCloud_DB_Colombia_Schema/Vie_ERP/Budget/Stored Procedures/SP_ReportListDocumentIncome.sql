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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de listado de documentos del presupuesto de ingresos, actuando como enrutador según el tipo de documento solicitado. Recibe criterios de filtro en formato XML y, dependiendo del valor del tipo de documento, delega la ejecución a uno de cuatro subprocedimientos: reconocimiento de ingresos, modificación de reconocimiento, recaudo de ingresos o modificación de recaudo. Cubre los principales documentos del ciclo presupuestal de ingresos en la entidad, permitiendo consultar y listar cada etapa del proceso desde un único punto de entrada.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentIncome';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentIncome';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Despachador que, según el tipo de documento recibido por XML, ejecuta el procedimiento específico para generar el reporte de listado de documentos del presupuesto de ingresos.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncome';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener el nodo /Data con el elemento TypeDocument de tipo entero; TypeDocument debe tomar uno de los valores soportados (1, 2, 3 o 4) para que se ejecute alguna rama', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncome';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se invoca uno de los cuatro procedimientos especializados por ejecución (ramas mutuamente excluyentes con IF/ELSE IF); Si TypeDocument no está en {1,2,3,4} no se ejecuta ningún subproceso y no se retorna resultset salvo que ocurra error; Ante cualquier excepción se devuelve un único resultset estandarizado con Code=''999''', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncome';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Presupuesto de ingresos; Reconocimiento de ingresos; Modificación de reconocimiento de ingresos; Recaudo de ingresos; Modificación de recaudo de ingresos; Documentos presupuestales', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncome';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Cuando ocurre cualquier error capturado en el TRY/CATCH, retorna un resultset con Code=''999'' y Message conteniendo ERROR_MESSAGE() + '' - Linea: '' + ERROR_LINE()', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncome';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TypeDocument = 1 → Ejecuta Budget.SP_ReportListDocumentIncomeRecognition con el XML de criterios; si @TypeDocument = 2 → Ejecuta Budget.SP_ReportListDocumentIncomeRecognitionModification con el XML de criterios; si @TypeDocument = 3 → Ejecuta Budget.SP_ReportListDocumentIncomeCollection con el XML de criterios; si @TypeDocument = 4 → Ejecuta Budget.SP_ReportListDocumentIncomeCollectionModification con el XML de criterios', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncome';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_ReportListDocumentIncomeRecognition; Budget.SP_ReportListDocumentIncomeRecognitionModification; Budget.SP_ReportListDocumentIncomeCollection; Budget.SP_ReportListDocumentIncomeCollectionModification', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncome';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncome';
-- GO
