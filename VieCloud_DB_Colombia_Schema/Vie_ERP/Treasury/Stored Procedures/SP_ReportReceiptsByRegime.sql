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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de recibos de caja agrupados por régimen de afiliación del paciente, para un rango de fechas dado. Consulta los recibos de caja (tesorería) que corresponden a anticipos de pacientes (contributivo, subsidiado, vinculado, particular, desplazado, entre otros), cruzando el detalle del recibo con los anticipos de cartera y las facturas a las que fueron aplicados, para mostrar el valor recaudado y el valor cruzado contra factura. Se utiliza en tesorería y cartera para auditar y reportar los recaudos por régimen de salud dentro de un período específico, identificando cuánto del anticipo ya fue cruzado con facturas activas.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_ReportReceiptsByRegime';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_ReportReceiptsByRegime';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de recibos de caja confirmados aplicados a anticipos de cartera, agrupando el régimen del paciente según las facturas activas cruzadas dentro de un rango de fechas.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReceiptsByRegime';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener los nodos /Data/DateStart y /Data/DateEnd con fechas válidas.; Deben existir conceptos de recibo de caja configurados en Billing.SettingsBilling como PatientAdvanceCashReceiptConceptId o CapitedPatientAdvanceCashReceiptConceptId.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReceiptsByRegime';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran recibos de caja con Status = 2 (confirmados/aplicados).; Solo se consideran facturas con Status = 1 al calcular el valor cruzado y el tipo de paciente.; El régimen reportado corresponde al MIN(PatientType) de las facturas asociadas al anticipo de cartera.; El CrossingValue nunca es NULL: se reemplaza por 0 vía ISNULL cuando no hay facturas cruzadas.; Solo se reportan detalles de recibo que estén vinculados a un Portfolio.PortfolioAdvance (JOIN obligatorio).', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReceiptsByRegime';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'recibo de caja; régimen del paciente (contributivo, subsidiado, vinculado, particular, desplazado); anticipo de cartera; cruce de anticipo contra factura; concepto de anticipo de paciente; concepto de anticipo de paciente capitado', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReceiptsByRegime';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Devuelve código del recibo, etiqueta de régimen, valor del detalle y valor cruzado contra facturas, filtrando cr.Status=2 y CAST(cr.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd.; [RETURN_RESULT] ResultSet: En caso de excepción retorna una fila con CodeResult=''999'' y MessageResult con el mensaje y línea del error.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReceiptsByRegime';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ipa.PatientType según valor 1..8 → Traduce el código numérico a etiqueta de régimen: 1=Contributivo, 2=Subsidiado, 3=Vinculado, 4=Particular, 5=Otro, 6=Desplazado Reg. Contributivo, 7=Desplazado Reg. Subsidiado, 8=Desplazado No Asegurado. else Cadena vacía cuando PatientType no coincide con ningún valor mapeado.; si crd.IdCashReceiptConcept = sb.PatientAdvanceCashReceiptConceptId OR crd.IdCashReceiptConcept = sb.CapitedPatientAdvanceCashReceiptConceptId → Solo se consideran detalles de recibo cuyo concepto corresponda a anticipos de paciente o anticipos de paciente capitado.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReceiptsByRegime';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.CashReceipts; Treasury.CashReceiptDetails; Billing.SettingsBilling; Portfolio.PortfolioAdvance; Billing.Invoice; Billing.InvoicePortfolioAdvance', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReceiptsByRegime';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReceiptsByRegime';
-- GO
