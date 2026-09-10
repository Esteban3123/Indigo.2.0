CREATE VIEW [Portfolio].[ViewAccountReceivableByPortfolioProvision]
AS
SELECT	ar.Id,
		ar.AccountReceivableType,
		ar.InvoiceNumber, 
		ar.AccountReceivableDate,
		ri.ConfirmDate AS RadicatedDate,
		IIF(ar.AccountReceivableType = 2 AND ar.PortfolioStatus > 2, ISNULL(ri.ConfirmDate, ar.AccountReceivableDate), ar.AccountReceivableDate) DocumentDate,
		tp.Nit ThirdPartyNit, 
		tp.Name ThirdPartyName, 
		pgr.RegimenName AS RegimenName,
		ar.Value,
		ar.Balance,
		gpg.Id GlosaPortfolioGlosadaId, 
		ISNULL(gpg.ValueGlosado, 0) ValueGlosado,
		ISNULL(gpg.BalanceGlosa, 0) BalanceGlosa,
		ar.DeteriorationBalance,
		ISNULL(pp.Expectative, 0) Expectative
FROM Common.ThirdParty tp WITH (NOLOCK)
JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON tp.Id = ar.ThirdPartyId
LEFT JOIN GeneralLedger.MainAccounts mar WITH (NOLOCK) ON mar.Id = ar.AccountWithoutRadicateId 
LEFT JOIN Portfolio.GetRegimes() pgr ON mar.Number = pgr.AccountNumber
LEFT JOIN
(
	SELECT rid.InvoiceNumber, MIN(ri.Id) Id, MIN(rid.RadicatedNumber) RadicatedNumber
	FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
	JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId
	WHERE ri.State = 2 AND rid.State = 2
	GROUP BY rid.InvoiceNumber
) ric ON ar.AccountReceivableType = 2 AND ar.InvoiceNumber = ric.InvoiceNumber
LEFT JOIN Portfolio.RadicateInvoiceC ri WITH (NOLOCK) ON ric.Id = ri.Id
LEFT JOIN Glosas.GlosaPortfolioGlosada gpg WITH(NOLOCK) ON ar.AccountReceivableType = 2 AND ar.InvoiceNumber = gpg.InvoiceNumber
LEFT JOIN
(
	SELECT ppd.AccountReceivableId, ppd.Value, ppd.Expectative
	FROM Portfolio.PortfolioProvision pp WITH(NOLOCK)
	JOIN Portfolio.PortfolioProvisionDetail ppd WITH(NOLOCK) ON pp.Id = ppd.PortfolioProvisionId
	JOIN
	(
		SELECT ppd.AccountReceivableId, MAX(pp.Id) Id
		FROM Portfolio.PortfolioProvision pp WITH(NOLOCK)
		JOIN Portfolio.PortfolioProvisionDetail ppd WITH(NOLOCK) ON pp.Id = ppd.PortfolioProvisionId
		WHERE pp.DocumentType = 2 AND pp.Status = 2
		GROUP BY ppd.AccountReceivableId
	) pm ON ppd.AccountReceivableId = pm.AccountReceivableId AND pp.Id = pm.Id
) pp ON ar.Id = pp.AccountReceivableId
WHERE ar.Status = 2 
	AND ar.Balance > 0 
	AND IIF(ar.AccountReceivableType = 2 AND ar.PortfolioStatus > 2, ISNULL(ri.ConfirmDate, ar.AccountReceivableDate), ar.AccountReceivableDate) IS NOT NULL
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de cuentas por cobrar activas con saldo pendiente, enriquecidas con su información de provisión contable más reciente. Consolida cada documento de cobro (factura o cuenta de cobro) con el tercero o aseguradora deudora, el régimen de afiliación, los valores glosados y el saldo en glosa provenientes del seguimiento de glosas, y la expectativa de recaudo calculada en la última provisión de cartera aprobada. Incluye la fecha de radicación confirmada ante la entidad pagadora cuando el documento es una factura ya radicada, o la fecha del documento en caso contrario. Sirve de base para reportes de aging, análisis de deterioro de cartera y cálculo de provisiones por deudas de difícil cobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewAccountReceivableByPortfolioProvision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewAccountReceivableByPortfolioProvision';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la cartera vigente con saldo pendiente, enriqueciéndola con datos del tercero, régimen contable, fecha de radicación, glosas asociadas y la última provisión vigente para análisis de provisión de cartera.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableByPortfolioProvision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las cuentas por cobrar deben tener Status = 2 (activas/vigentes); El saldo (Balance) debe ser mayor a 0; La fecha del documento (ConfirmDate de radicación o AccountReceivableDate) no puede ser nula; Para considerar radicación, los registros en RadicateInvoiceC y RadicateInvoiceD deben tener State = 2; Para considerar provisión vigente, PortfolioProvision debe tener DocumentType = 2 y Status = 2', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableByPortfolioProvision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen cuentas por cobrar activas (Status=2) con saldo positivo; ValueGlosado y BalanceGlosa siempre son numéricos: si no hay glosa asociada, se devuelven en 0; Expectative siempre es numérico: si no hay provisión vigente, se devuelve en 0; Las fechas de radicación solo aplican a documentos tipo factura (AccountReceivableType=2); Solo se consideran radicaciones y provisiones en estado confirmado/vigente (State=2 / Status=2); El régimen se deriva de la cuenta contable sin radicar (AccountWithoutRadicateId) cruzando con el plan de cuentas y la función GetRegimes', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableByPortfolioProvision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por cobrar; Factura; Tercero; Radicación de factura; Glosa de cartera; Provisión de cartera; Régimen; Saldo de cartera; Deterioro de cartera; Expectativa de recaudo; Plan de cuentas (PUC)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableByPortfolioProvision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.ViewAccountReceivableByPortfolioProvision: Devuelve solo cuentas por cobrar con Status=2 y Balance>0 cuya fecha de documento (radicación o emisión) no sea nula', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableByPortfolioProvision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ar.AccountReceivableType = 2 AND ar.PortfolioStatus > 2 → DocumentDate toma ISNULL(ri.ConfirmDate, ar.AccountReceivableDate), priorizando la fecha de confirmación de radicación else DocumentDate toma ar.AccountReceivableDate (fecha original del documento); si ar.AccountReceivableType = 2 (factura) → Se cruza con radicación (RadicateInvoiceC/D) y con glosas (GlosaPortfolioGlosada) por InvoiceNumber else No se aplica enlace de radicación ni de glosas; si Existen múltiples PortfolioProvision para una misma AccountReceivableId con DocumentType=2 y Status=2 → Se selecciona la de MAX(pp.Id), es decir, la provisión vigente más reciente; si Existen múltiples radicaciones para una misma factura → Se toma MIN(ri.Id) y MIN(rid.RadicatedNumber), es decir, la radicación más antigua válida', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableByPortfolioProvision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.GetRegimes', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableByPortfolioProvision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Portfolio.AccountReceivable; GeneralLedger.MainAccounts; Portfolio.GetRegimes; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Glosas.GlosaPortfolioGlosada; Portfolio.PortfolioProvision; Portfolio.PortfolioProvisionDetail', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableByPortfolioProvision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableByPortfolioProvision';
GO
