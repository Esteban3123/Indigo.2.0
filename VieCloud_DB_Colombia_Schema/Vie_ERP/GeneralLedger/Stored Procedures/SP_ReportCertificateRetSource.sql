-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2018-02-26
-- Description:	Generación reporte retención en la fuente
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportCertificateRetSource]
	-- Add the parameters for the stored procedure here
	@LegalBookId AS INTEGER,
	@Year AS INTEGER,
    @AccountStart AS VARCHAR(50),
	@AccountEnd AS VARCHAR(50),
	@NitStart AS VARCHAR(15),
	@NitEnd AS VARCHAR(15),
	@InitialDate DateTime,
	@FinalDate DateTime
AS
BEGIN
	SELECT 
		tp.Nit ThirdPartyNit, 
		tp.Name as ThirdPartyName,
		tp.Id ThirdPartyId,
		ma.Number MainAccountNumber,
		ma.Name as MainAccountName, 
		Year(@InitialDate) as Validity,
		ma.Id MainAccountId,
		ma.Name + IIF(ma.FreelancerCategory = 1, 
		'', 
		' ' + CAST(CAST(ISNULL(IIF(ma.FreelancerCategory = 1, 0, jvd.RetentionRate), 0) AS FLOAT) AS VARCHAR(20)) + '%') AS RetentionConceptName,
		ma.FreelancerCategory,
		ISNULL(t.AnnualIncome, SUM(jvd.BillingValue)) AnnualIncome,
		SUM(CASE mc.[Nature] 
				WHEN 1 THEN IIF(jvd.CreditValue > 0, BaseValue * - 1, BaseValue) 
				ELSE IIF(jvd.DebitValue > 0, BaseValue * - 1, BaseValue)
			END) as BaseValue,
		SUM(IIF(mc.[Nature] = 1,(jvd.DebitValue - jvd.CreditValue),(jvd.CreditValue - jvd.DebitValue))) as RetentionValue,
		'' as LettersValue
	FROM GeneralLedger.JournalVoucherDetails AS jvd
	INNER JOIN GeneralLedger.JournalVouchers AS jv ON jv.Id = jvd.IdAccounting
	INNER JOIN Common.ThirdParty AS tp ON tp.Id = jvd.IdThirdParty
	INNER JOIN GeneralLedger.MainAccounts AS ma ON ma.Id = jvd.IdMainAccount
	INNER JOIN GeneralLedger.MainAccountClasses AS mc ON mc.Id = ma.IdAccountClass
	LEFT JOIN GeneralLedger.RetentionConcepts AS rc ON rc.Id = jvd.IdRetention
	LEFT JOIN 
	(
		SELECT jvd.IdThirdParty,
			SUM(jvd.DebitValue - jvd.CreditValue) as AnnualIncome
		FROM GeneralLedger.JournalVouchers jv
		JOIN GeneralLedger.JournalVoucherDetails jvd ON jv.Id = jvd.IdAccounting
		JOIN GeneralLedger.MainAccounts ma ON ma.Id = jvd.IdMainAccount
		JOIN GeneralLedger.MainAccountClasses mc ON mc.Id = ma.IdAccountClass
		WHERE mc.Type = 2 AND mc.Nature = 1 
			AND jv.LegalBookId = @LegalBookId 
			AND jv.Status = 2 
			AND jv.IsClosedYear = 0
			AND (jv.VoucherDate BETWEEN @InitialDate AND @FinalDate)--YEAR(jv.VoucherDate) = @Year
		GROUP BY jvd.IdThirdParty
	) AS t ON tp.Id = t.IdThirdParty
	WHERE 
		jv.LegalBookId = @LegalBookId 
		AND jv.Status = 2 
		--AND jv.IsClosedYear = 0 
		AND (jvd.RetentionRate IS NOT NULL OR rc.Rate IS NOT NULL)
		AND (ma.RetencionType = 1 OR ma.RetencionType = 4) 
		AND (jv.VoucherDate BETWEEN @InitialDate AND @FinalDate)
		AND  ma.Number >= ISNULL(@AccountStart,'0') AND ma.Number <= ISNULL(@AccountEnd, 'z') 
		AND tp.Nit >= ISNULL(@NitStart,'0') AND tp.Nit <= ISNULL(@NitEnd, 'z')
	GROUP BY tp.Id, tp.Nit, tp.Name, ma.Id, ma.Number, ma.Name, ma.FreelancerCategory, IIF(ma.FreelancerCategory = 1, 0, jvd.RetentionRate), t.AnnualIncome
	HAVING SUM(IIF(mc.[Nature] = 1, (jvd.DebitValue - jvd.CreditValue), (jvd.CreditValue - jvd.DebitValue))) > 0
	ORDER BY tp.Nit, ma.FreelancerCategory DESC
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de certificado de retención en la fuente para un libro legal y rango de fechas determinados. Consolida los movimientos contables (débitos y créditos) de los comprobantes contables aprobados, agrupados por tercero (proveedor o contratista identificado por NIT) y cuenta contable de retención, calculando la base gravable, el valor retenido y los ingresos anuales acumulados del tercero. Permite filtrar por rango de cuentas contables, rango de NIT y período, y es utilizado para emitir certificados tributarios de retención en la fuente que la organización entrega a sus proveedores y contratistas.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportCertificateRetSource';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportCertificateRetSource';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de certificados de retención en la fuente, agrupando por tercero y cuenta contable los valores base, retenidos e ingresos anuales dentro de un rango de fechas, cuentas y NITs.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetSource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los comprobantes deben pertenecer al libro legal indicado (LegalBookId); Los comprobantes deben estar en estado 2 (contabilizado/aprobado); La fecha del comprobante debe estar entre @InitialDate y @FinalDate; El detalle debe tener tasa de retención (jvd.RetentionRate) o el concepto de retención debe tener tasa (rc.Rate); La cuenta principal debe tener RetencionType = 1 o 4; Para el cálculo de ingresos anuales (subconsulta), los comprobantes deben tener IsClosedYear = 0 y la clase de cuenta debe ser Type=2 y Nature=1', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetSource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan movimientos de comprobantes contabilizados (Status=2) del libro legal indicado; Solo se incluyen cuentas con tipo de retención 1 o 4; Solo se incluyen movimientos con tasa de retención definida (en el detalle o en el concepto); Los ingresos anuales se calculan únicamente sobre cuentas de Type=2 y Nature=1 no cerradas (IsClosedYear=0); Los rangos de cuenta y NIT aplican lexicográficamente; nulos se sustituyen por ''0'' (mínimo) y ''z'' (máximo); Solo se entregan resultados cuando el valor neto de retención es positivo; El año de vigencia del certificado se toma del año de @InitialDate', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetSource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retención en la fuente; Certificado tributario; Tercero; NIT; Plan de cuentas (cuenta principal); Comprobante contable (journal voucher); Naturaleza débito/crédito; Concepto de retención; Categoría de freelancer (independiente); Ingresos anuales; Base gravable; Libro legal contable; Vigencia fiscal', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetSource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve filas agrupadas por tercero y cuenta solo cuando SUM de retención (débito-crédito según naturaleza) > 0 (cláusula HAVING)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetSource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ma.FreelancerCategory = 1 → El nombre del concepto de retención se muestra sin porcentaje y los ingresos anuales/tasa se tratan como 0 else Se concatena al nombre la tasa de retención (RetentionRate) como porcentaje; si mc.Nature = 1 (cuenta de naturaleza débito) → BaseValue se invierte de signo si CreditValue>0; RetentionValue = DebitValue - CreditValue else BaseValue se invierte de signo si DebitValue>0; RetentionValue = CreditValue - DebitValue; si Existe AnnualIncome calculado en la subconsulta para el tercero → Se usa ese AnnualIncome else Se usa SUM(jvd.BillingValue) como ingreso anual', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetSource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVoucherDetails; GeneralLedger.JournalVouchers; Common.ThirdParty; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; GeneralLedger.RetentionConcepts', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetSource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetSource';
-- GO
