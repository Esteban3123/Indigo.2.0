

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
		ar.PortfolioStatus AS InvoicePortfolioStatus,
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista contable de cuentas por cobrar del módulo de cartera, desglosada por movimiento contable (nota de portafolio). Combina cada cuenta por cobrar (factura o documento de cobro emitido a un tercero o aseguradora) con su línea contable asociada, mostrando la cuenta contable principal, el tercero responsable (NIT y nombre), el centro de costo y el saldo pendiente. Clasifica automáticamente cada línea según el estado específico de cartera: Sin Radicar, Radicada Entidad, Glosada sin Conciliar, Glosada Conciliada, Difícil Recaudo o Cobro Jurídico, filtrando únicamente cuentas activas (estado 2) con una sola cuota. Incluye la moneda de la cuenta o, en su defecto, la moneda oficial de la compañía con su tasa de cambio (TRM), y el tipo de documento de la factura asociada; sirve para reportería contable, seguimiento de cartera, gestión de glosas y análisis de recaudo por estado de cobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewAccountReceivableAccountingByPortfolioNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewAccountReceivableAccountingByPortfolioNote';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, por nota/asiento contable, las cuentas por cobrar activas de cartera clasificadas según su estado de cobro (sin radicar, radicada, glosada, conciliada, difícil recaudo o jurídico) con datos de tercero, centro de costo, factura y moneda.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableAccountingByPortfolioNote';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en GeneralLedger.CompanySettings con OfficialCurrencyId válido y referenciable en Common.Currency, para el fallback de moneda.; Cada AccountReceivable debe tener ThirdPartyId válido en Common.ThirdParty (JOIN interno).; Cada AccountReceivableAccounting debe estar enlazado a un AccountReceivable y a una MainAccount existente (JOIN interno).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableAccountingByPortfolioNote';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone cuentas por cobrar con una sola cuota (NumberShares = 1).; Solo incluye cuentas por cobrar con Status = 2 (estado activo/vigente para cartera).; Solo se muestran filas cuya cuenta contable contable (ara.MainAccountId) coincide con alguno de los seis estados de cartera definidos en la cuenta por cobrar (sin radicar, radicada, glosada sin conciliar, conciliada, difícil recaudo o cobro jurídico).; La moneda nunca queda nula: si la CxC no tiene CurrencyId, se sustituye por la moneda oficial parametrizada en GeneralLedger.CompanySettings.; El identificador de la fila combina AccountReceivableId y AccountReceivableAccountingId mediante CONCAT con guion, garantizando unicidad por par CxC-asiento.; InvoiceDocumentType nunca es NULL; se devuelve 0 cuando no hay factura asociada.; PortfolioStatusName se devuelve como ''N/A'' y SpecificPortfolioStatus como 0 únicamente si no hay coincidencia con los estados definidos (caso que el WHERE descarta).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableAccountingByPortfolioNote';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cartera; Cuenta por cobrar; Cuenta contable (PUC); Glosa; Conciliación de glosas; Radicación de factura; Cobro jurídico; Cuenta de difícil recaudo; Tercero; Centro de costo; Factura; Moneda oficial; TRM; Saldo / Valor inicial', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableAccountingByPortfolioNote';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.ViewAccountReceivableAccountingByPortfolioNote: Devuelve un conjunto de filas filtrado por ar.NumberShares = 1 AND ar.Status = 2 y donde ara.MainAccountId coincide con alguna de las cuentas de estado de cartera (radicada, sin radicar, conciliación, difícil recaudo, glosa remediada o cobro jurídico).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableAccountingByPortfolioNote';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ara.MainAccountId = ar.AccountWithoutRadicateId → Clasifica el registro como estado 1 - ''Sin Radicar''; si ara.MainAccountId = ar.AccountRadicateId → Clasifica como estado 3 - ''Radicada Entidad''; si ara.MainAccountId = ar.AccountObjectionRemediedId → Clasifica como estado 4 - ''Glosada sin Conciliar''; si ara.MainAccountId = ar.AccountConciliationId → Clasifica como estado 12 - ''Glosada Conciliada''; si ara.MainAccountId = ar.AccountHardCollectionId → Clasifica como estado 15 - ''Cuenta de Difícil Recaudo''; si ara.MainAccountId = ar.AccountLegalCollectionId → Clasifica como estado 16 - ''Cobro Jurídico''; si ar.CurrencyId IS NULL → Usa la moneda oficial de la empresa (CompanySettings.OfficialCurrencyId) y su abreviatura como divisa por defecto else Usa la moneda registrada en la cuenta por cobrar', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableAccountingByPortfolioNote';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Common.Currency; Portfolio.AccountReceivable; Common.ThirdParty; Portfolio.AccountReceivableAccounting; GeneralLedger.MainAccounts; Payroll.CostCenter; Billing.Invoice', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableAccountingByPortfolioNote';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewAccountReceivableAccountingByPortfolioNote';
GO
