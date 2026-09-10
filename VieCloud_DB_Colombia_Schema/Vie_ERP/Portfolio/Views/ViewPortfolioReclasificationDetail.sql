

CREATE VIEW [Portfolio].[ViewPortfolioReclasificationDetail]
AS
SELECT  pr.Id,
		pr.Code,
		ar.InvoiceNumber AS AccountReceivableCode,
		mas.Number + ' - ' + mas.Name AS AccoutSource,
		mat.Number + ' - ' + mat.Name AS TargetAccount, 
		pr.Value,
		cu.Abbreviation AS CurrencyAbbreviation
FROM Portfolio.PortfolioReclassification AS pr  with (nolock) 
INNER JOIN Portfolio.AccountReceivable AS ar  with (nolock) ON pr.AccountReceivableId = ar.Id
INNER JOIN GeneralLedger.MainAccounts AS mas with (nolock) ON pr.SourceAccountId = mas.Id 
INNER JOIN GeneralLedger.MainAccounts AS mat with (nolock) ON pr.TargetAccountId = mat.Id
INNER JOIN (SELECT TOP 1 cs.OfficialCurrencyId
			FROM GeneralLedger.CompanySettings cs WITH(NOLOCK)) cs on 1=1
INNER JOIN Common.Currency cu WITH(NOLOCK) on cu.Id = ISNULL(ar.CurrencyId,cs.OfficialCurrencyId)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de reclasificaciones de cartera: muestra cada movimiento contable que traslada el saldo de una cuenta por cobrar (factura o cuenta de cobro) desde una cuenta contable de origen hacia una cuenta contable destino, indicando el número de factura asociado, el código y nombre de ambas cuentas del plan contable, el valor reclasificado y la moneda correspondiente (tomando la moneda oficial de la empresa si el documento no tiene moneda propia). Integra las tablas de reclasificaciones de cartera, cuentas por cobrar, cuentas del libro mayor y catálogo de monedas para ofrecer una vista lista para reportes de auditoría contable, conciliación de saldos y seguimiento de reclasificaciones en cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewPortfolioReclasificationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewPortfolioReclasificationDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle legible de las reclasificaciones contables de cartera, mostrando la cuenta por cobrar asociada, las cuentas contables de origen y destino con su código y nombre, el valor reclasificado y la moneda aplicable.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioReclasificationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La reclasificación debe estar vinculada a una cuenta por cobrar existente (INNER JOIN sobre AccountReceivableId).; Las cuentas contables de origen (SourceAccountId) y destino (TargetAccountId) deben existir en GeneralLedger.MainAccounts.; Debe existir al menos un registro en GeneralLedger.CompanySettings con OfficialCurrencyId definido (se usa TOP 1).; La moneda resultante (de la cuenta por cobrar o la oficial) debe existir en Common.Currency.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioReclasificationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las cuentas contables de origen y destino se presentan siempre con el formato ''Number - Name''.; Toda fila retornada tiene una moneda resuelta (nunca nula) gracias al fallback con la moneda oficial de la empresa.; Solo se considera UN registro de CompanySettings (TOP 1) como fuente de la moneda oficial.; Se usan lecturas con NOLOCK en todas las tablas, permitiendo lecturas sucias sin bloqueos.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioReclasificationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reclasificación de cartera; Cuenta por cobrar; Cuenta contable origen; Cuenta contable destino; Plan de cuentas (PUC); Moneda oficial de la empresa; Divisa / abreviatura de moneda', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioReclasificationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.ViewPortfolioReclassificationDetail: Retorna una fila por cada reclasificación cuya cuenta por cobrar, cuentas contables origen/destino y moneda resuelta existan; si la cuenta por cobrar no tiene CurrencyId, se utiliza la moneda oficial de la empresa (ISNULL(ar.CurrencyId, cs.OfficialCurrencyId)).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioReclasificationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ar.CurrencyId IS NULL → Se usa cs.OfficialCurrencyId (moneda oficial configurada en CompanySettings) para resolver la moneda mostrada. else Se utiliza la moneda propia de la cuenta por cobrar (ar.CurrencyId).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioReclasificationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioReclassification; Portfolio.AccountReceivable; GeneralLedger.MainAccounts; GeneralLedger.CompanySettings; Common.Currency', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioReclasificationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioReclasificationDetail';
GO
