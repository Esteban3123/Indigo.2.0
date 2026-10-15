-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-11-12
-- Description:	Procedimiento para el reporte de listado de recibos de caja por regimen
-- =============================================
CREATE PROCEDURE [Treasury].[SP_ReportReceiptsByRegime]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE -- CRITERIOS --
			@DateStart DATE,
			@DateEnd DATE

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 
			@DateStart = t.x.value('DateStart[1]','date'),
			@DateEnd = t.x.value('DateEnd[1]','date')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		SELECT
			cr.Code,
			CASE ipa.PatientType
				WHEN 1 THEN 'Contributivo'  
				WHEN 2 THEN 'Subsidiado' 
				WHEN 3 THEN 'Vinculado'
				WHEN 4 THEN 'Particular' 
				WHEN 5 THEN 'Otro'
				WHEN 6 THEN 'Desplazado Reg. Contributivo'
				WHEN 7 THEN 'Desplazado Reg. Subsidiado'
				WHEN 8 THEN 'Desplazado No Asegurado'
				ELSE ''
			END Regime,
			crd.Value,
			ISNULL(ipa.Value, 0) CrossingValue
		FROM Treasury.CashReceipts cr
		JOIN Treasury.CashReceiptDetails crd ON cr.Id = crd.IdCashReceipt
		JOIN Billing.SettingsBilling sb ON crd.IdCashReceiptConcept = sb.PatientAdvanceCashReceiptConceptId OR crd.IdCashReceiptConcept = sb.CapitedPatientAdvanceCashReceiptConceptId
		JOIN Portfolio.PortfolioAdvance pa ON crd.Id = pa.CashReceiptDetailId
		LEFT JOIN 
		(
			SELECT 
				ipa.PortfolioAdvanceId, 
				MIN(i.PatientType) PatientType,
				SUM(ipa.Value) Value
			FROM Billing.Invoice i
			JOIN Billing.InvoicePortfolioAdvance ipa ON i.Id = ipa.InvoiceId
			WHERE i.Status = 1
			GROUP BY ipa.PortfolioAdvanceId
		) ipa ON pa.Id = ipa.PortfolioAdvanceId
		WHERE cr.Status = 2
			AND CAST(cr.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
		ORDER BY cr.Code
		OPTION (RECOMPILE)
		
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END