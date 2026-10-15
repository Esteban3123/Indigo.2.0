CREATE VIEW [Payments].[ViewThirdPartyMonthlyIncome]
AS
	SELECT	NEWID() Id
			, ap.IdThirdParty ThirdPartyId
			, YEAR(ap.DocumentDate) [Year]
			, 1 [Type]
			, 0 ConceptType
			, 'Cuenta por pagar' [TypeName]
			, ap.Code
			, ap.BillNumber
			, ap.BillDate DocumentDate
			, apdcl.TotalIncome DebitValue
			, 0 CreditValue
			, apdcl.RetentionValue383
			, apdcl.ExemptIncome ExemptIncome
			, apdcl.MaxDeductionsAndRentExents MaxDeductionsAndRentExents
			, ap.Status
			, ap.CreationDate
	FROM Payments.AccountPayable ap
	JOIN Payments.AccountPayableDetailConcept apdc ON ap.Id = apdc.IdAccountPayable 
	JOIN Payments.AccountPayableDetailConceptLiquidation apdcl ON apdc.Id = apdcl.AccountPayableDetailConceptId
	GROUP BY  ap.IdThirdParty 
			, ap.DocumentDate
			, ap.Code
			, ap.BillNumber
			, ap.BillDate 
			, apdcl.TotalIncome 
			, apdcl.RetentionValue383
			, apdcl.ExemptIncome 
			, apdcl.MaxDeductionsAndRentExents 
			, ap.Status
			, ap.CreationDate

	UNION ALL

	SELECT	NEWID() Id
			, ap.IdThirdParty ThirdPartyId
			, YEAR(ap.DocumentDate) [Year]
			, 2 [Type]
			, apdcla.ConceptType
			, 'Ajuste' [TypeName]
			, ap.Code
			, ap.BillNumber
			, ap.BillDate DocumentDate
			, IIF(apdcla.Nature = 1, apdcla.TotalIncome, 0) DebitValue
			, IIF(apdcla.Nature = 1, 0, apdcla.TotalIncome) CreditValue
			, 0 RetentionValue383
			, IIF(apdcla.ConceptType = 1, apdcla.Value, 0)*IIF(apdcla.Nature = 1, 1, -1) ExemptIncome
			, IIF(apdcla.ConceptType = 1, 0, apdcla.Value)*IIF(apdcla.Nature = 1, 1, -1) MaxDeductionsAndRentExents
			, ap.Status
			, ap.CreationDate
	FROM Payments.AccountPayable ap
	JOIN Payments.AccountPayableDetailConcept apdc ON ap.Id = apdc.IdAccountPayable 
	JOIN Payments.AccountPayableDetailConceptLiquidation apdcl ON apdc.Id = apdcl.AccountPayableDetailConceptId
	JOIN Payments.AccountPayableDetailConceptLiquidationAdjusments apdcla ON apdcl.Id = apdcla.LiquidationId

	UNION ALL

	SELECT	NEWID() Id
			, ap.IdThirdParty ThirdPartyId
			, YEAR(ap.DocumentDate) [Year]
			, 3 [Type]
			, 0 ConceptType
			, 'Nota Reversión' [TypeName]
			, pn.Code
			, ap.BillNumber
			, pn.NoteDate DocumentDate
			, 0 DebitValue
			, apdcl.TotalIncome CreditValue
			, (apdcl.RetentionValue383 * -1) RetentionValue383
			, apdcl.ExemptIncome * -1 ExemptIncome
			, apdcl.MaxDeductionsAndRentExents * -1 MaxDeductionsAndRentExents
			, ap.Status
			, ap.CreationDate
	FROM Payments.PaymentNotes pn
	JOIN Payments.AccountPayable ap ON pn.IdAccountPayable = ap.Id
	JOIN Payments.AccountPayableDetailConcept apdc ON ap.Id = apdc.IdAccountPayable 
	JOIN Payments.AccountPayableDetailConceptLiquidation apdcl ON apdc.Id = apdcl.AccountPayableDetailConceptId
	WHERE pn.IndicatesBillAdvance = 2

	UNION ALL

	SELECT	NEWID() Id
			, ap.IdThirdParty ThirdPartyId
			, YEAR(ap.DocumentDate) [Year]
			, 2 [Type]
			, apdcla.ConceptType
			, 'Ajuste CXP Reversada' [TypeName]
			, pn.Code
			, ap.BillNumber
			, pn.NoteDate DocumentDate
			, IIF(apdcla.Nature = 1, 0, apdcla.TotalIncome) DebitValue
			, IIF(apdcla.Nature = 1, apdcla.TotalIncome, 0) CreditValue
			, 0 RetentionValue383
			, IIF(apdcla.ConceptType = 1, apdcla.Value, 0)*IIF(apdcla.Nature = 1, -1, 1) ExemptIncome
			, IIF(apdcla.ConceptType = 1, 0, apdcla.Value)*IIF(apdcla.Nature = 1, -1, 1) MaxDeductionsAndRentExents
			, ap.Status
			, ap.CreationDate
	FROM Payments.PaymentNotes pn
	JOIN Payments.AccountPayable ap ON pn.IdAccountPayable = ap.Id
	JOIN Payments.AccountPayableDetailConcept apdc ON ap.Id = apdc.IdAccountPayable 
	JOIN Payments.AccountPayableDetailConceptLiquidation apdcl ON apdc.Id = apdcl.AccountPayableDetailConceptId
	JOIN Payments.AccountPayableDetailConceptLiquidationAdjusments apdcla ON apdcl.Id = apdcla.LiquidationId
	WHERE pn.IndicatesBillAdvance = 2