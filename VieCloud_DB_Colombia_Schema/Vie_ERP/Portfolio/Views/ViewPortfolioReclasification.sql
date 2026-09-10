
CREATE VIEW [Portfolio].[ViewPortfolioReclasification]
AS
SELECT	pr.Code, 
		SUM(pr.Value) Value, 
		pr.DocumentType, 
		pr.Status, 
		ar.InvoiceNumber,
		prt.Total,
		cu.Abbreviation CurrencyAbbreviation
FROM Portfolio.PortfolioReclassification AS pr 
JOIN Portfolio.AccountReceivable AS ar ON pr.AccountReceivableId = ar.Id
JOIN
(
	SELECT Code, SUM(VALUE) Total
	FROM Portfolio.PortfolioReclassification
	GROUP BY Code
) prt ON pr.Code = prt.Code
JOIN (SELECT top 1 cs.OfficialCurrencyId from GeneralLedger.CompanySettings cs WITH(NOLOCK)) cs on 1=1
JOIN Common.Currency cu WITH(NOLOCK) on cu.Id =  ISNULL(ar.CurrencyId,cs.OfficialCurrencyId)
GROUP BY  pr.Code, pr.Status, pr.DocumentType, ar.InvoiceNumber, prt.Total,cu.Abbreviation
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reclasificaciones de cartera que consolida los movimientos contables por los que una cuenta por cobrar es trasladada entre cuentas. Combina cada reclasificación con su factura o cuenta de cobro asociada, acumula el valor reclasificado por código de movimiento y calcula el total global de cada código sumando todos sus registros. Incluye la moneda del documento (tomada de la cuenta por cobrar o, en su defecto, la moneda oficial de la compañía) para mostrar el símbolo o abreviatura de la divisa junto a los importes. Se usa en reportería de cartera para consultar el estado, tipo de documento, número de factura, valor parcial y valor total de cada operación de reclasificación registrada en el sistema.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewPortfolioReclasification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewPortfolioReclasification';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las reclasificaciones de cartera agregadas por código, mostrando el valor por documento/estado junto con el total reclasificado del código, la factura asociada y la abreviatura de la moneda (de la cuenta por cobrar o, si es nula, la oficial de la compañía).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioReclasification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en GeneralLedger.CompanySettings con OfficialCurrencyId definido para que el JOIN devuelva filas (se toma TOP 1 sin ORDER BY).; La moneda resultante (CurrencyId de la cuenta por cobrar o, en su defecto, OfficialCurrencyId) debe existir en Common.Currency; de lo contrario la fila se excluye.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioReclasification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor mostrado por fila corresponde a la suma de Value de las reclasificaciones agrupadas por Code, Status, DocumentType, InvoiceNumber, Total y abreviatura de moneda.; El Total expuesto representa la suma global de Value de todas las reclasificaciones que comparten el mismo Code (independiente del Status/DocumentType).; Cuando la cuenta por cobrar no tiene CurrencyId definido, se asume la moneda oficial de la compañía (CompanySettings.OfficialCurrencyId) vía ISNULL.; Solo se incluyen reclasificaciones cuya AccountReceivableId existe en Portfolio.AccountReceivable (JOIN obliga existencia).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioReclasification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reclasificación de cartera; Cuenta por cobrar; Factura; Moneda; Moneda oficial de la compañía', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioReclasification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.PortfolioReclassification: Retorna por cada combinación (Code, Status, DocumentType, InvoiceNumber, Total, CurrencyAbbreviation) la suma de Value de las reclasificaciones.; [RETURN_RESULT] Portfolio.PortfolioReclassification: Calcula y expone una columna Total como SUM(Value) agrupado únicamente por Code, representando el total reclasificado del código.; [RETURN_RESULT] Common.Currency: Cuando ar.CurrencyId IS NULL, la moneda mostrada se resuelve usando CompanySettings.OfficialCurrencyId (ISNULL(ar.CurrencyId, cs.OfficialCurrencyId)).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioReclasification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioReclassification; Portfolio.AccountReceivable; GeneralLedger.CompanySettings; Common.Currency', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioReclasification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioReclasification';
GO
