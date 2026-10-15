


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