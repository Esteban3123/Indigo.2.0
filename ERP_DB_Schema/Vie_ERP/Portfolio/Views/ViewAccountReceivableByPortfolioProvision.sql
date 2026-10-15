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