
CREATE VIEW [Treasury].[VSchedulePayment]
AS
SELECT DISTINCT 
	s.Id as SupplierId, 
	s.IdThirdParty as ThirdId, 
	s.Name as SupplierName, 
	s.Code as SupplierCode, 
	tp.Nit as SupplierNit, 
	dl.Description as DescriptionLine, 
	dl.Id as DistributionLineId, 
	dl.ExpensesConceptId as ExpenseConceptIdDistributionLine, 
	dl.IdMainAccount as MainAccountIdDistributionLine,
	ec.Nature as NatureExpenseConcept, 
	st.Id as SupplierTypeId, 
	st.Name as SupplierTypeName, 
	ap.BillNumber as Invoice, 
	ap.ExpirationDate as ExpirationDate, 
	agp.Name as AgePayment, 
	ap.Balance as InvoiceBalance, 
	aps.Share as Share, 
	aps.DateExpires as ShareExpirationDate, 
	aps.Balance as BalanceShare, 
	0.0 as PayValue, 
	0 as PaymentConceptId, 
	ap.Id as AccountPayableId, 
	aps.Id as AccountPayableShareId, 
	0 as SchedulePaymentDetailId, 
	0.0 as PaymentPercent, 
	ap.Code AS AccountPayableCode
FROM Payments.AccountPayable AS ap WITH (NOLOCK)
JOIN Common.Supplier AS s WITH (NOLOCK) ON ap.IdSupplier = s.Id
JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON tp.Id = s.IdThirdParty
JOIN Common.SuppliersDistributionLines AS spd WITH (NOLOCK) ON ap.IdSuppliersDistributionLines = spd.Id
JOIN Common.DistributionLines AS dl WITH (NOLOCK) ON  spd.IdDistributionLine = dl.Id
JOIN Treasury.ExpenseConcepts AS ec WITH (NOLOCK) ON ec.Id = dl.ExpensesConceptId
JOIN Common.SupplierType AS st WITH (NOLOCK) ON ap.SupplierTypeId = st.Id 
JOIN Payments.AccountPayableShares AS aps ON aps.IdAccountPayable = ap.Id and aps.Balance > 0 
JOIN Payments.SettingPayments as SP on SP.IdOperatingUnit = AP.IdOperatingUnit
LEFT JOIN Payments.AgesPayments as AGP on AGP.SettingPaymentId = SP.Id and (DATEDIFF(DAY, APS.DateExpires, GETDATE()) + 1) BETWEEN AGP.InitialRange  and AGP.EndRange
WHERE ap.Status = 2