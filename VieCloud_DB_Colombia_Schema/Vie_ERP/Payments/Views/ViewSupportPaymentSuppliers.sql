

CREATE VIEW [Payments].[ViewSupportPaymentSuppliers] -- nombre de la vista en ingles 
AS

WITH cte_DetailConceptRetencionType as (SELECT	apc.IdAccountPayable,
												sum(apc.BaseValue) BaseValue,
												sum(apc.BillingValue) BillingValue,
												sum(apc.IvaValue) IvaValue,
												sum(apc.TotalConcept) TotalConcept,
												ma.RetencionType
										from Payments.AccountPayableDetailConcept apc WITH(NOLOCK)
										JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = apc.IdAccount
										group by apc.IdAccountPayable, ma.RetencionType),

	cte_DetailConcept as (	SELECT	apc.IdAccountPayable,
									sum(apc.BaseValue) BaseValue,
									sum(apc.BillingValue) BillingValue,
									sum(apc.IvaValue) IvaValue,
									sum(apc.TotalConcept) TotalConcept
							from cte_DetailConceptRetencionType apc
							group by apc.IdAccountPayable),
	
	cte_VoucherTransaction AS ( SELECT	db.IdAccountPayable,
										STRING_AGG(vt.Code,' , ') VoucherCode,
										STRING_AGG(vt.Id,' , ') VoucherTransactionId,
										STRING_AGG(vt.DocumentDate,',') DocumentDate,
										sum(ISNULL(vtd.TotalConcept,vtd.Value)) TotalConcept,
										STRING_AGG((CASE vt.ExpenseType
													WHEN 1 THEN 'Pago'
													WHEN 2 THEN 'Reembolso'
													WHEN 3 THEN 'Traslado'
													END),',') ExpenseType,
										STRING_AGG(NoteNumber,',') NoteNumber,
										STRING_AGG(sba.Number,',') SupplierBankAccount,
										vt.CurrencyId
								from Treasury.VoucherTransaction vt WITH(NOLOCK)
								Join Treasury.VoucherTransactionDetails vtd WITH(NOLOCK) on vt.Id=vtd.IdVoucherTransaction
								JOIN Treasury.DischargeBill db WITH(NOLOCK) on db.IdVoucherTransactionD = vtd.Id
								LEFT JOIN Common.SupplierBankAccount sba WITH(NOLOCK) on sba.Id= vtd.SupplierBankAccountId
								group by db.IdAccountPayable, vt.CurrencyId),
	
	cte_Notes as (SELECT	papa.AccountPayableId,
							sum(IIF(pn.Nature = 1,papa.AdjusmentValue,0)) DebitValue,
							sum(IIF(pn.Nature = 1,0,papa.AdjusmentValue)) CreditValue,
							0 PromptPaymentDiscount,
							pn.CurrencyId
					FROM Payments.PaymentNotes pn WITH(NOLOCK)
					JOIN Payments.PaymentsNoteDetails pnd WITH(NOLOCK) on pn.Id = pnd.IdPaymentsNote
					join Payments.PaymentNotesAccountPayableAdvance papa WITH(NOLOCK) on pn.Id=papa.PaymentNoteId
					GROUP by papa.AccountPayableId, pn.CurrencyId)

SELECT	ap.Id,
		ap.BillDate,
		ap.IdSupplier,
		CONCAT(s.Code,' - ',s.Name) SupplierName,
		ap.BillNumber,
		ap.InvoiceValue,
		apc.BaseValue,
		ISNULL(apc.IvaValue,0) IvaValue,
		ISNULL(apc.TotalConcept,0) TotalConcept,
		ISNULL(retef.TotalConcept,0) ReteFuente,
		ISNULL(reteIC.TotalConcept,0) ReteICA,
		ISNULL(reteIV.TotalConcept,0) ReteIVA,
		ISNULL(reteO.TotalConcept,0) ReteOthers,
		ap.CurrencyId,
		c.Abbreviation CurrencyAbbreviation,
		cte_vt.VoucherCode,
		cte_vt.VoucherTransactionId,
		cte_vt.DocumentDate VoucherDate,
		cte_vt.TotalConcept VoucherTotalValue,
		cte_vt.ExpenseType VoucherExpenseType,
		cte_vt.NoteNumber VoucherReferencePayment,
		cte_vt.SupplierBankAccount,
		ISNULL(cte_vt.CurrencyId,c.Id) VoucherCurrencyId,
		cte_n.DebitValue DebitValueNote,
		cte_n.CreditValue CreditValueNote,
		cte_n.PromptPaymentDiscount,
		ISNULL(cte_n.CurrencyId,c.Id) NoteCurrencyId
from Payments.AccountPayable ap WITH(NOLOCK)
JOIN Common.Supplier s WITH(NOLOCK) on ap.IdSupplier = s.Id
JOIN cte_DetailConcept apc on apc.IdAccountPayable = ap.Id
JOIN Common.Currency c WITH(NOLOCK) on c.Id= ap.CurrencyId
LEFT JOIN cte_DetailConceptRetencionType  retef on ap.Id= retef.IdAccountPayable and retef.RetencionType =1
LEFT JOIN cte_DetailConceptRetencionType  reteIC on ap.Id= reteIC.IdAccountPayable and reteIC.RetencionType =3
LEFT JOIN cte_DetailConceptRetencionType  reteIV on ap.Id= reteIV.IdAccountPayable and reteIV.RetencionType =2
LEFT JOIN cte_DetailConceptRetencionType  reteO on ap.Id= reteO.IdAccountPayable and reteO.RetencionType =4
LEFT JOIN cte_VoucherTransaction cte_vt on ap.Id = cte_vt.IdAccountPayable 
LEFT JOIN cte_Notes cte_n on ap.Id = cte_n.AccountPayableId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de soporte de pagos a proveedores que consolida, por cada cuenta por pagar, los valores de la factura del proveedor junto con el desglose de retenciones (ReteFuente, ReteICA, ReteIVA y otras retenciones), IVA y valor base, cruzando los conceptos contables de cada obligación. Integra los comprobantes de egreso de tesorería asociados (código del comprobante, fecha, tipo de movimiento —pago, reembolso o traslado—, número de referencia y cuenta bancaria del proveedor utilizada para el pago), así como las notas débito y crédito aplicadas a cada cuenta por pagar. Sirve para reportería y auditoría del ciclo de pagos a proveedores: permite verificar cuánto se facturó, cuánto se retuvo, cuánto se pagó efectivamente y mediante qué comprobante o nota contable, incluyendo la moneda en que se realizó cada transacción.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewSupportPaymentSuppliers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewSupportPaymentSuppliers';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola fila por cuenta por pagar la información del proveedor, totales de conceptos, retenciones desagregadas (Fuente, ICA, IVA, Otras), comprobantes de pago asociados y notas de ajuste, para soportar reportes de pagos a proveedores.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewSupportPaymentSuppliers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada AccountPayable debe tener al menos un registro en AccountPayableDetailConcept (JOIN obligatorio vía cte_DetailConcept).; Cada AccountPayable debe tener un proveedor existente en Common.Supplier y una moneda válida en Common.Currency.; Las cuentas contables (MainAccounts) referenciadas en el detalle deben tener clasificación de RetencionType para discriminar tipos de retención.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewSupportPaymentSuppliers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre se retorna exactamente una fila por cada AccountPayable que tenga al menos un detalle de concepto.; Los valores monetarios de IVA, TotalConcept y retenciones nunca son NULL en la salida (se fuerza ISNULL a 0).; PromptPaymentDiscount siempre se reporta como 0 (no se calcula descuento por pronto pago).; Las retenciones se clasifican exclusivamente por RetencionType de la cuenta contable, no por otro criterio.; La moneda de comprobantes/notas, si no existen, hereda la moneda de la cuenta por pagar.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewSupportPaymentSuppliers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por pagar; Proveedor; Factura; Retención en la fuente; Retención de ICA; Retención de IVA; Otras retenciones; IVA; Comprobante de egreso; Pago; Reembolso; Traslado; Nota de pago (débito/crédito); Anticipo; Cuenta bancaria del proveedor; Moneda; Descuento por pronto pago', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewSupportPaymentSuppliers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewSupportPaymentSuppliers: Devuelve una fila por cuenta por pagar con totales (BaseValue, IvaValue, TotalConcept) y retenciones discriminadas: RetencionType=1 → ReteFuente, RetencionType=2 → ReteIVA, RetencionType=3 → ReteICA, RetencionType=4 → ReteOthers.; [RETURN_RESULT] ViewSupportPaymentSuppliers: Cuando existen comprobantes de tesorería ligados (vía DischargeBill→VoucherTransactionDetails→VoucherTransaction), agrega con STRING_AGG sus códigos, Ids, fechas, números de nota y cuentas bancarias del proveedor, traduciendo ExpenseType: 1=''Pago'', 2=''Reembolso'', 3=''Traslado''.; [RETURN_RESULT] ViewSupportPaymentSuppliers: Cuando existen notas de pago aplicadas, suma AdjusmentValue como DebitValue si Nature=1 y como CreditValue en caso contrario; PromptPaymentDiscount se entrega siempre en 0.; [RETURN_RESULT] ViewSupportPaymentSuppliers: VoucherCurrencyId y NoteCurrencyId se reemplazan por la moneda de la cuenta por pagar (c.Id) cuando no existen comprobantes o notas asociados (ISNULL).; [RETURN_RESULT] ViewSupportPaymentSuppliers: SupplierName se entrega concatenado como ''Code - Name'' del proveedor.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewSupportPaymentSuppliers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MainAccounts.RetencionType = 1 → El total del concepto se suma en ReteFuente.; si MainAccounts.RetencionType = 2 → El total del concepto se suma en ReteIVA.; si MainAccounts.RetencionType = 3 → El total del concepto se suma en ReteICA.; si MainAccounts.RetencionType = 4 → El total del concepto se suma en ReteOthers.; si VoucherTransaction.ExpenseType = 1 → Se etiqueta como ''Pago''.; si VoucherTransaction.ExpenseType = 2 → Se etiqueta como ''Reembolso''.; si VoucherTransaction.ExpenseType = 3 → Se etiqueta como ''Traslado''.; si PaymentNotes.Nature = 1 → AdjusmentValue se acumula como DebitValue. else AdjusmentValue se acumula como CreditValue.; si VoucherTransactionDetails.TotalConcept es NULL → Se utiliza VoucherTransactionDetails.Value como total del comprobante (ISNULL).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewSupportPaymentSuppliers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayableDetailConcept; GeneralLedger.MainAccounts; Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.DischargeBill; Common.SupplierBankAccount; Payments.PaymentNotes; Payments.PaymentsNoteDetails; Payments.PaymentNotesAccountPayableAdvance; Payments.AccountPayable; Common.Supplier; Common.Currency', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewSupportPaymentSuppliers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewSupportPaymentSuppliers';
GO
