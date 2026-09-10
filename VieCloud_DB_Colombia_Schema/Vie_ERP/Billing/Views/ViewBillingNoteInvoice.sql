CREATE VIEW [Billing].[ViewBillingNoteInvoice]
AS
SELECT bbd.Id Id,
bnd.BillingNoteId,
i.InvoiceNumber,
bnd.DocumentDate,
(isnull(ip.Code, isnull(bc.Code, fai.Code)) + ' - ' + (isnull(ip.Name, isnull(bc.Name, fai.Description)))) CodeName,
bbd.Quantity InvoicedQuantity,
bbd.Price As UnitValue,
bbd.Price As BaseValue,
IIF(bbd.PercentageIVA > 0, bbd.Value * bbd.PercentageIVA / 100, 0) As TaxValue,
bbd.PercentageIVA As TaxPercentage,
(IIF(bbd.PercentageIVA > 0, bbd.Value * bbd.PercentageIVA / 100, 0) + bbd.Price) As TotalValue,
i.InvoiceDate,
i.InvoiceExpirationDate,
CONCAT(ma.Number,' - ',ma.Name) AccountNumberName,
((bbd.Value) + (IIF(bbd.PercentageIVA > 0, bbd.Value * bbd.PercentageIVA / 100, 0))) BillingValue,
((bbd.Value) + (IIF(bbd.PercentageIVA > 0, bbd.Value * bbd.PercentageIVA / 100, 0))) AdjusmentValue,
i.CUFE as CUFE
from Billing.BillingNote bn
INNER JOIN Billing.BillingNoteDetail bnd on bnd.BillingNoteId = bn.id
INNER JOIN Portfolio.PortfolioNote pn on pn.Id = bn.EntityId and bn.EntityName = 'PortfolioNote'
INNER JOIN Billing.Invoice i on i.id = bnd.InvoiceId
INNER JOIN Billing.BasicBilling BB ON BB.InvoiceId = bnd.InvoiceId
INNER JOIN Billing.BasicBillingDetail bbd on bbd.BasicBillingId = bb.id
INNER JOIN Portfolio.AccountReceivable ar  on i.Id =ar.InvoiceId AND ar.ThirdPartyId =i.ThirdPartyId
LEFT JOIN Inventory.InventoryProduct ip on ip.Id = bbd.ProductId
LEFT JOIN Billing.BillingConcept bc on bc.Id = bbd.BillingConceptId
LEFT JOIN FixedAsset.FixedAssetPhysicalAsset fapa on fapa.Id = bbd.PhysicalAssetId
LEFT JOIN FixedAsset.FixedAssetItem fai on fai.Id = fapa.ItemId
LEFT JOIN GeneralLedger.GeneralLedgerIVA IVA on IVA.Id IN (ip.IVAId, bc.IVAId, fai.IVAId)
LEFT JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara  
			ON pnara.AccountReceivableId =ar.Id 
			AND pnara.PortfolioNoteId =  bn.EntityId 
			AND bn.EntityName = 'PortfolioNote'
LEFT JOIN GeneralLedger.MainAccounts ma  on ma.Id = pnara.MainAccountId
WHERE pn.EntityName = 'ReverseBasicBilling' AND (ip.IVAId IS NOT NULL OR bc.IVAId IS NOT NULL OR fai.IVAId IS NOT NULL)

UNION ALL


SELECT	(bnd.Id) + ROW_NUMBER() OVER (
			PARTITION BY bnd.Id 
			ORDER BY bndt.TaxPercentage
		) AS Id,
		bnd.BillingNoteId,
		bnd.InvoiceNumber,
		bnd.DocumentDate,
		bnd.InvoiceNumber As CodeName,
		1 InvoicedQuantity,
		IIF(bndt.BaseValue IS NOT NULL, bndt.BaseValue, bnd.AdjusmentValue) As UnitValue,
		IIF(bndt.BaseValue IS NOT NULL, bndt.BaseValue, bnd.AdjusmentValue) As BaseValue,
		ISNULL(bndt.TaxValue, 0) TaxValue,
		bndt.TaxPercentage,
		IIF(bndt.BaseValue IS NOT NULL,bndt.BaseValue + bndt.TaxValue,bnd.AdjusmentValue) TotalValue,
		i.InvoiceDate,
		i.InvoiceExpirationDate,
		CONCAT(ma.Number,' - ',ma.Name) AccountNumberName,
		IIF(bndt.BaseValue IS NOT NULL,bndt.BaseValue + bndt.TaxValue,bnd.BillingValue) BillingValue,
		IIF(bndt.BaseValue IS NOT NULL,bndt.BaseValue + bndt.TaxValue,bnd.AdjusmentValue) AdjusmentValue,
		i.CUFE As CUFE
FROM Billing.BillingNoteDetail bnd 
INNER JOIN Billing.BillingNote bn  on bnd.BillingNoteId = bn.Id
INNER JOIN Billing.Invoice i  ON bnd.InvoiceId = i.Id
INNER JOIN Portfolio.AccountReceivable ar  on i.Id =ar.InvoiceId AND ar.ThirdPartyId =i.ThirdPartyId
LEFT JOIN Portfolio.PortfolioNote pn on pn.Id = bn.EntityId and bn.EntityName = 'PortfolioNote' and pn.EntityName not in('ReverseBasicBilling')
LEFT JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara  
			ON pnara.AccountReceivableId =ar.Id 
			AND pnara.PortfolioNoteId =  bn.EntityId 
			AND bn.EntityName = 'PortfolioNote'
LEFT JOIN GeneralLedger.MainAccounts ma  on ma.Id = pnara.MainAccountId
LEFT JOIN
(
	SELECT BillingNoteDetailId, SUM(TaxValue) TaxValue, TaxPercentage, sum(BaseValue) BaseValue
	FROM Billing.BillingNoteDetailTax 
	GROUP BY BillingNoteDetailId, TaxPercentage
) bndt ON bnd.Id = bndt.BillingNoteDetailId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de notas de facturación (crédito o débito) junto con la factura original afectada, los valores ajustados, los impuestos aplicados y la cuenta contable asociada. Cruza el detalle de la nota con la factura de cobro, la cuenta por cobrar en cartera y, cuando aplica, el anticipo de cartera vinculado a una nota de portafolio. Incluye fechas clave de la factura (emisión y vencimiento), el valor facturado, el valor de ajuste y el total de impuestos por línea. Se usa en reportería de ajustes de facturación electrónica, conciliación de cartera y seguimiento de notas débito/crédito emitidas a pagadores como EPS, aseguradoras o pacientes.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewBillingNoteInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewBillingNoteInvoice';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los detalles de notas de facturación junto con su factura, valores ajustados, impuestos totalizados, fechas de la factura y la cuenta contable del anticipo de cartera asociado, cuando aplica.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingNoteInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada detalle de nota debe estar asociado a una nota de facturación existente y a una factura existente (JOIN obligatorio con BillingNote e Invoice).; Debe existir una cuenta por cobrar (AccountReceivable) para la factura cuyo tercero coincida con el tercero de la factura.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingNoteInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se vinculan anticipos de cartera (PortfolioNoteAccountReceivableAdvance) cuando la nota de facturación tiene EntityName = ''PortfolioNote'' y EntityId coincide con el PortfolioNoteId.; El emparejamiento de cuentas por cobrar exige que la factura y la cuenta por cobrar correspondan al mismo tercero (ar.ThirdPartyId = i.ThirdPartyId).; El valor de impuestos (TaxValue) se totaliza por detalle de nota sumando todos los registros de BillingNoteDetailTax y se devuelve 0 cuando no existen impuestos asociados.; La cuenta contable se expone con formato ''Número - Nombre'' concatenando MainAccounts.Number y MainAccounts.Name.; Los detalles de nota se devuelven aunque no exista anticipo asociado ni cuenta contable (LEFT JOIN sobre PortfolioNoteAccountReceivableAdvance y MainAccounts).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingNoteInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nota de facturación (crédito/débito); Factura; Cuenta por cobrar; Anticipo de cartera; Cuenta contable (PUC); Impuestos de detalle de nota', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingNoteInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.BillingNoteDetail: Devuelve un renglón por cada detalle de nota de facturación con su factura asociada y, opcionalmente, la cuenta contable del anticipo cuando la nota está vinculada a una PortfolioNote (bn.EntityName = ''PortfolioNote'').', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingNoteInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.BillingNoteDetail; Billing.BillingNote; Billing.Invoice; Portfolio.AccountReceivable; Portfolio.PortfolioNoteAccountReceivableAdvance; GeneralLedger.MainAccounts; Billing.BillingNoteDetailTax', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingNoteInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingNoteInvoice';
GO
