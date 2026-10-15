


CREATE VIEW [Portfolio].[ViewAccountReceivableAccountingByPortfolioNote]
AS

WITH OfficialCurrency_CTE AS (	SELECT TOP 1 cs.OfficialCurrencyId,c1.Abbreviation 
								FROM GeneralLedger.CompanySettings cs WITH(NOLOCK)
								JOIN Common.Currency c1 WITH(NOLOCK) ON cs.OfficialCurrencyId= c1.Id)

SELECT	CONCAT(ar.Id, '-', ara.Id) Id,
		ar.Id AccountReceivableId, 
		ara.Id AccountReceivableAccountingId, 
		ar.InvoiceId,
		ar.InvoiceNumber, 
		ar.AccountReceivableDate,		
		ma.Id MainAccountId, 
		ma.Number MainAccountNumber, 
		ma.Name MainAccountName, 
		ma.Number + ' - ' + ma.Name MainAccountNumberName,
		ara.Value, 
		ara.Balance,
		t.Id ThirdPartyId, 
		t.Nit ThirdPartyNit, 
		t.Name ThirdPartyName, 
		t.Nit + ' - ' + t.Name ThirdPartyNitName,
		cc.Id CostCenterId, 
		cc.Code CostCenterCode, 
		cc.Name CostCenterName, 
		cc.Code + ' - ' + cc.Name CostCenterCodeName,
		ISNULL(i.DocumentType, 0) InvoiceDocumentType,
		ar.OpeningBalance,
		CASE ara.MainAccountId
				WHEN ar.AccountWithoutRadicateId THEN 1
				WHEN ar.AccountRadicateId THEN 3
				WHEN ar.AccountObjectionRemediedId THEN 4
				WHEN ar.AccountConciliationId THEN 12
				WHEN ar.AccountHardCollectionId THEN 15
				WHEN ar.AccountLegalCollectionId THEN 16
				ELSE 0
		END SpecificPortfolioStatus,
		CASE ara.MainAccountId
				WHEN ar.AccountWithoutRadicateId THEN '1-Sin Radicar'
				WHEN ar.AccountRadicateId THEN '3-Radicada Entidad'
				WHEN ar.AccountObjectionRemediedId THEN '4-Glosada sin Conciliar'
				WHEN ar.AccountConciliationId THEN '12-Glosada Conciliada'
				WHEN ar.AccountHardCollectionId THEN '15-Cuenta de Dificil Recaudo'
				WHEN ar.AccountLegalCollectionId THEN '16-Cobro Juridico'
				ELSE 'N/A'
		END PortfolioStatusName,
		ISNULL(ar.CurrencyId,(SELECT OfficialCurrencyId FROM OfficialCurrency_CTE)) CurrencyId,
		ISNULL(c.Abbreviation,(SELECT Abbreviation FROM OfficialCurrency_CTE)) CurrencyAbbreviation,
		ar.TRMValue
FROM Portfolio.AccountReceivable ar WITH(NOLOCK)
JOIN Common.ThirdParty t WITH(NOLOCK) on t.Id = ar.ThirdPartyId
JOIN Portfolio.AccountReceivableAccounting ara WITH(NOLOCK) on ara.AccountReceivableId = ar.Id
JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = ara.MainAccountId
LEFT JOIN Payroll.CostCenter cc WITH(NOLOCK) on cc.Id = ara.CostCenterId
LEFT JOIN Billing.Invoice i WITH(NOLOCK) on i.Id = ar.InvoiceId
LEFT JOIN Common.Currency c WITH(NOLOCK) ON c.Id = ar.CurrencyId
WHERE ar.NumberShares = 1 
	--AND ma.Id <> ISNULL(ar.AccountObjectionRemediedId, 0) 
	AND ar.Status = 2
	AND (ar.AccountRadicateId = ara.MainAccountId or ar.AccountWithoutRadicateId = ara.MainAccountId or ar.AccountConciliationId = ara.MainAccountId OR ar.AccountHardCollectionId = ara.MainAccountId OR ar.AccountObjectionRemediedId=ara.MainAccountId or ar.AccountLegalCollectionId = ara.MainAccountId)