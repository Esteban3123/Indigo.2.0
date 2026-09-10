CREATE PROCEDURE [GeneralLedger].[SP_ReportCertificateRetICA]
	-- Add the parameters for the stored procedure here
    @DateStart as date,
	@DateEnd as date,
	@AccountStart as varchar(50),
	@AccountEnd as varchar(50),
	@NitStart as varchar(15),
	@NitEnd as varchar(15),
	@RetencionType as integer,
	@LegalBookId as integer
AS
BEGIN
	SELECT

		gr.Rate
	   ,ct.Nit
	   ,ct.Name Tercero
	   ,ma.Number
	   ,ma.Name Concept
	   ,SUM
		(
		CASE mc.[Nature]
			WHEN 1 THEN IIF(jvd.CreditValue > 0, jvd.BillingValue * -1, jvd.BillingValue)
			ELSE IIF(jvd.DebitValue > 0, jvd.BillingValue * -1, jvd.BillingValue)
		END
		) ValorFacturado
	   ,SUM
		(
		CASE mc.[Nature]
			WHEN 1 THEN IIF(jvd.CreditValue > 0, BaseValue * -1, BaseValue)
			ELSE IIF(jvd.DebitValue > 0, BaseValue * -1, BaseValue)
		END
		) BaseRetención
	   ,SUM
		(
		IIF(mc.[Nature] = 1, (jvd.DebitValue - jvd.CreditValue), (jvd.CreditValue - jvd.DebitValue))
		) ValorRetenido
	   ,'' LettersValue

	FROM GeneralLedger.JournalVoucherDetails jvd
	JOIN GeneralLedger.JournalVouchers jv ON jv.Id = jvd.IdAccounting
	LEFT JOIN GeneralLedger.RetentionConcepts AS gr ON gr.Id = jvd.IdRetention
	JOIN Common.ThirdParty AS ct ON ct.Id = jvd.IdThirdParty
	JOIN GeneralLedger.MainAccounts AS ma ON ma.Id = jvd.IdMainAccount
	JOIN GeneralLedger.MainAccountClasses AS mc ON mc.Id = ma.IdAccountClass
	WHERE CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND ma.Number >= ISNULL(@AccountStart, '0')
			AND ma.Number <= ISNULL(@AccountEnd, 'z')
			AND ct.Nit >= ISNULL(@NitStart, '0')
			AND ct.Nit <= ISNULL(@NitEnd, 'z')
			AND ma.RetencionType = @RetencionType
			AND jv.LegalBookId = @LegalBookId
			AND (jvd.RetentionRate IS NOT NULL OR gr.Rate IS NOT NULL)
			AND jv.IsClosedYear = 0
			AND jv.Status = 2
	GROUP BY gr.Rate
			,ct.Nit
			,ct.Name
			,ma.Number
			,ma.Name
	HAVING SUM(IIF(mc.[Nature] = 1, (jvd.DebitValue - jvd.CreditValue), (jvd.CreditValue - jvd.DebitValue))) > 0
	ORDER BY ct.Nit, ma.Number
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el certificado de retención de ICA (Impuesto de Industria y Comercio) consolidado por tercero y cuenta contable, dentro de un rango de fechas, cuentas contables y NITs parametrizable. Consulta los comprobantes contables y sus detalles para calcular tres valores clave por cada combinación de tercero y concepto: el valor facturado, la base sobre la que se aplicó la retención y el valor efectivamente retenido, ajustando el signo según la naturaleza débito/crédito de la cuenta. El resultado, filtrado por tipo de retención y libro legal, muestra únicamente registros donde se haya practicado retención (tasa de retención presente) y el valor retenido sea positivo, sirviendo como soporte tributario y de cumplimiento fiscal para expedir certificados de retención ICA a proveedores y terceros.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportCertificateRetICA';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportCertificateRetICA';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte/certificado de retenciones de ICA por tercero y cuenta contable, calculando valor facturado, base de retención y valor retenido en un rango de fechas, cuentas y NITs.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetICA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los comprobantes contables deben estar en estado 2 (contabilizados/aprobados); Los comprobantes no deben pertenecer a un año cerrado (IsClosedYear = 0); Las líneas deben tener tasa de retención asignada (RetentionRate o Rate del concepto de retención); La cuenta contable debe tener configurado el tipo de retención coincidente con el solicitado; Los comprobantes deben pertenecer al libro legal especificado', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetICA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se reportan movimientos de comprobantes en estado 2 y de años no cerrados; Sólo se incluyen líneas cuyas cuentas tengan el tipo de retención solicitado; El signo del valor facturado/base se invierte cuando el movimiento va contra la naturaleza de la cuenta (notas crédito/anulaciones); Sólo se muestran combinaciones con valor retenido neto positivo; Los rangos de cuenta y NIT nulos se reemplazan por ''0'' y ''z'' (sin filtro efectivo); El reporte se ordena por NIT del tercero y número de cuenta', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetICA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retención de ICA; Certificado de retención; Tercero; NIT; Plan de cuentas (PUC); Naturaleza contable (débito/crédito); Comprobante contable; Libro legal; Base de retención; Tasa de retención; Año contable cerrado', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetICA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve filas agrupadas por tasa, NIT, tercero y cuenta sólo cuando la suma neta del valor retenido es estrictamente mayor a cero (HAVING SUM(...) > 0)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetICA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si mc.Nature = 1 (cuenta de naturaleza débito) → ValorFacturado y BaseRetención se invierten cuando hay CreditValue > 0; ValorRetenido se calcula como DebitValue - CreditValue else Cuando la naturaleza es crédito, se invierten cuando DebitValue > 0; ValorRetenido se calcula como CreditValue - DebitValue; si jvd.RetentionRate IS NOT NULL OR gr.Rate IS NOT NULL → Se incluye la línea en el reporte (debe existir alguna tasa de retención aplicable)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetICA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVoucherDetails; GeneralLedger.JournalVouchers; GeneralLedger.RetentionConcepts; Common.ThirdParty; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetICA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetICA';
-- GO
