

CREATE VIEW [Payments].[PaymentNotesDebitCredit]
AS
	SELECT	P.Id,P.Code,P.NoteDate,
			Case	P.Nature 
					When 1 Then 'Debito' 
					When 2 Then 'Credito' 
			End As Nature,P.Status,
			Case	P.IndicatesBillAdvance
					When 0 Then 'Factura' 
					When 1 Then 'Anticipo' 
					Else 'Otro' 
			End As IndicatesBillAdvance,
			S.Name,S.IdThirdParty,T.Nit,
			CASE	P.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado'
					WHEN 3 THEN 'Anulado'
					ELSE 'Otro' 
			END AS StatusName, 
			--Logica para sacar el valor de la nota de cuentas por pagar.
			Case	when P.Nature = 1 AND P.IndicatesBillAdvance = 0 Then IsNull(D.ValueDebit,0)+IsNull(AD.ValueBilling,0)
					When P.Nature = 1 AND P.IndicatesBillAdvance = 1 Then IsNull(D.ValueDebit,0) + IsNull(AD.ValueAdvance ,0)
					When P.Nature = 2 AND P.IndicatesBillAdvance = 0 Then IsNull(D.ValueCredit,0) + IsNull(AD.ValueBilling ,0)
					When P.Nature = 2 AND P.IndicatesBillAdvance = 1 Then IsNull(D.ValueCredit,0) + IsNull(AD.ValueAdvance ,0)
			Else 0
			End As Value,
			P.CurrencyId,
			c.Abbreviation as CurrencyAbbreviation
	FROM Payments.PaymentNotes AS P WITH (nolock)
	INNER JOIN Common.Supplier As S WITH (nolock) On P.idSupplier = S.Id
	INNER JOIN Common.ThirdParty As T WITH (nolock) On S.IdThirdParty = T.Id
	INNER JOIN 
	(
		Select D.IdPaymentsNote, Sum(Case when D.Nature = 1 Then D.Value Else 0 End) As ValueDebit, Sum(Case when D.Nature = 2 Then D.Value Else 0 End) As ValueCredit
		FROM Payments.PaymentsNoteDetails As D WITH (nolock) 
		Group by D.IdPaymentsNote
	) D On D.IdPaymentsNote = P.Id 
	INNER JOIN 
	(
		Select AD.PaymentNoteId, Sum(AD.AdjusmentValue) As ValueAdvance, Sum(AD.AdjustmentValueShare) As ValueBilling
		FROM Payments.PaymentNotesAccountPayableAdvance As AD WITH (nolock) 
		Group by AD.PaymentNoteid
	) AD On AD.PaymentNoteid = P.Id
	INNER JOIN Common.Currency c WITH(nolock) on P.CurrencyId= c.Id
UNION ALL
	SELECT	P.Id,P.Code,
			P.NoteDate,
			Case	P.Nature
					When 1 Then 'Debito'
					When 2 Then 'Credito' 
			End As Nature,
			P.Status,
			'Reversion CxP' As IndicatesBillAdvance,
			S.Name,S.IdThirdParty,
			T.Nit,
			CASE P.Status 
				WHEN 1 THEN 'Registrado'
				WHEN 2 THEN 'Confirmado'
				WHEN 3 THEN 'Anulado'
				ELSE 'Otro'
			END AS StatusName, 
			--Logica para sacar el valor de la nota de cuentas por pagar.
			ap.InvoiceValue As Value,
			P.CurrencyId,
			c.Abbreviation as CurrencyAbbreviation
	FROM Payments.PaymentNotes AS P WITH (nolock)
	INNER JOIN Common.Supplier As S WITH (nolock) On P.idSupplier = S.Id
	INNER JOIN Common.ThirdParty As T WITH (nolock) On S.IdThirdParty = T.Id
	INNER JOIN Payments.AccountPayable ap WITH (NOLOCK) ON p.IdAccountPayable = ap.Id
	INNER JOIN Common.Currency c WITH(nolock) on P.CurrencyId= c.Id