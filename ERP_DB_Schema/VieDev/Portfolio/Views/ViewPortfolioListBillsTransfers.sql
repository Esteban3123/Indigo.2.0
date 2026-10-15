



CREATE VIEW [Portfolio].[ViewPortfolioListBillsTransfers]
--Vista para el DataSource de facturas en el formulario FrmPortfolioTranfers
AS
	WITH OfficialCurrency_CTE AS (	SELECT TOP 1 cs.OfficialCurrencyId,c1.Abbreviation 
								FROM GeneralLedger.CompanySettings cs WITH(NOLOCK)
								JOIN Common.Currency c1 WITH(NOLOCK) ON cs.OfficialCurrencyId= c1.Id)
		SELECT	d.*
		FROM (SELECT	ara.Id,
				ar.InvoiceNumber,
				th.Id as ThirdPartyId,
				ar.InvoiceId,
				CONCAT(th.Nit,' - ',th.Name) as NitName,
				CONCAT(ma.Number,' - ', ma.Name) as NumberName,		
				CASE ara.MainAccountId
					WHEN ar.AccountWithoutRadicateId THEN '1-Sin Radicar'
					WHEN ar.AccountRadicateId THEN '3-Radicada Entidad'
					WHEN ar.AccountObjectionRemediedId THEN '4-Glosada sin Conciliar'
					WHEN ar.AccountConciliationId THEN '12-Glosada Conciliada'
					WHEN ar.AccountHardCollectionId THEN '15-Cuenta de Dificil Recaudo'
					WHEN ar.AccountLegalCollectionId THEN '16-Cobro Juridico'
					ELSE 'N/A'
				END PortfolioStatusName,
				CASE ara.MainAccountId
					WHEN ar.AccountWithoutRadicateId THEN 1
					WHEN ar.AccountRadicateId THEN 3
					WHEN ar.AccountObjectionRemediedId THEN 4
					WHEN ar.AccountConciliationId THEN 12
					WHEN ar.AccountHardCollectionId THEN 15
					WHEN ar.AccountLegalCollectionId THEN 16
					ELSE 0
				END SpecificPortfolioStatus,
				ar.PortfolioStatus,
				ar.Status,
				ar.AccountReceivableType,
				ara.AccountReceivableId,
				ara.Value,	
				ara.Balance,
				ISNULL(c.Id,(SELECT OfficialCurrencyId FROM OfficialCurrency_CTE)) CurrencyId,
				ISNULL(C.Abbreviation,(SELECT Abbreviation FROM OfficialCurrency_CTE)) CurrencyAbbreviation,
				CONCAT(trim(i.PatientCode),' - ',pa.IPNOMCOMP) Patient
				FROM Portfolio.AccountReceivableAccounting ara WITH(NOLOCK)
				JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) ON ara.AccountReceivableId = ar.Id
				JOIN Common.ThirdParty th  WITH(NOLOCK) ON ar.ThirdPartyId = th.Id
				LEFT JOIN Billing.Invoice i WITH(NOLOCK) ON ar.InvoiceId = i.Id
				LEFT JOIN INPACIENT pa WITH(NOLOCK) ON i.PatientCode = pa.IPCODPACI
				LEFT JOIN Common.Currency c WITH(NOLOCK) ON c.Id =ar.CurrencyId
				LEFT JOIN Glosas.GlosaPortfolioGlosada gfg WITH(NOLOCK) ON ar.InvoiceNumber = gfg.InvoiceNumber
				LEFT JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ara.MainAccountId = ma.Id			
				WHERE  ara.Balance > 0  
				) as  d