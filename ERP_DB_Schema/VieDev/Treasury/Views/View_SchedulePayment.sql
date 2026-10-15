



CREATE VIEW [Treasury].[View_SchedulePayment]
AS
SELECT AccountPayableId as Id,* 
FROM (
	SELECT  
		s.Id as SupplierId, 
		s.IdThirdParty as ThirdId, 
		s.Name as SupplierName, 
		s.Code as SupplierCode, 
		tp.Nit as SupplierNit, 
		dl.Description as DescriptionLine, 
		dl.Id as DistributionLineId, 
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
		ap.Id as AccountPayableId, 
		aps.Id as AccountPayableShareId, 
		ap.Code AS AccountPayableCode,
		ap.Value As CXPValue,
		iif(ap.Value = (aps.Balance + isnull(pnd.AdjusmentValue,0)),isnull(ppd.RangeName,''''),'''') AS RangeNameDiscount,
		iif(ap.Value = (aps.Balance + isnull(pnd.AdjusmentValue,0)),cast(isnull(ppd.DiscountRate,0) as DECIMAL(5,2)),0) AS DiscountRate,
		iif(ap.Value = (aps.Balance + isnull(pnd.AdjusmentValue,0)),cast((iif(gdvap.BaseValue is null or gdvap.BaseValue =0,(ap.Value+(ap.InvoiceValue-ap.Value))+(isnull(pnd.AdjusmentValueToAffectBase,0)),gdvap.BaseValue+(isnull(pnd.AdjusmentValueToAffectBase,0)))* isnull(ppd.DiscountRate,0))/100 as decimal(18,2)),0) AS DiscountValue,
		ap.ServicePeriodDate as CxPRadicateDate,
		iif(gdvap.BaseValue is null or gdvap.BaseValue =0,((ap.Value+(ap.InvoiceValue-ap.Value))+(isnull(pnd.AdjusmentValueToAffectBase,0))),(gdvap.BaseValue+(isnull(pnd.AdjusmentValueToAffectBase,0)))) as BaseValue,
		gdvap.ValueTax,
		gdvap.RateValue,
		CAST(Common.GETDATE() AS DATETIME) AS PaymentDate,
		ISNULL(pnd.AdjusmentValue,0) ValueNote,
		ISNULL(AdjusmentValueToAffectBase,0) AdjusmentValueToAffectBase
	FROM Payments.AccountPayable AS ap WITH (NOLOCK)
	JOIN Common.Supplier AS s WITH (NOLOCK) ON ap.IdSupplier = s.Id
	OUTER APPLY (	SELECT  ppd.RangeName, ppd.DiscountRate
					from Common.PromptPaymentDiscount AS ppd  WITH (NOLOCK)
					where SupplierId =s.Id and (DATEDIFF(DAY,ap.ServicePeriodDate,CAST(Common.GETDATE() AS DATETIME))) BETWEEN ppd.InitialRank and ppd.EndRank
					) AS ppd
	OUTER APPLY (SELECT * from Payments.GetDetailValueAccountPayable(ap.EntityId,ap.EntityName,ap.BillNumber,ap.IdSupplier)) gdvap
	OUTER APPLY (SELECT	sum(iif(pn.AllowDiscountPromptPayment=1,pnapa.AdjusmentValue*iif(pn.Nature=1,1,-1),0)) AdjusmentValue,
						sum(iif(pn.AffectBaseToDiscount=1,pnapa.AdjusmentValue*iif(pn.Nature=1,-1,1),0)) AdjusmentValueToAffectBase
						FROM Payments.PaymentNotesAccountPayableAdvance pnapa 
						JOIN Payments.PaymentNotes pn on pnapa.PaymentNoteId=pn.Id
						where pnapa.AccountPayableId=ap.Id) pnd
	JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON tp.Id = s.IdThirdParty
	JOIN Common.SuppliersDistributionLines AS spd WITH (NOLOCK) ON ap.IdSuppliersDistributionLines = spd.Id
	JOIN Common.DistributionLines AS dl WITH (NOLOCK) ON  spd.IdDistributionLine = dl.Id
	JOIN Treasury.ExpenseConcepts AS ec WITH (NOLOCK) ON ec.Id = dl.ExpensesConceptId
	JOIN Common.SupplierType AS st WITH (NOLOCK) ON ap.SupplierTypeId = st.Id 
	JOIN Payments.AccountPayableShares AS aps ON aps.IdAccountPayable = ap.Id and aps.Balance > 0 
	JOIN Payments.SettingPayments as SP on SP.IdOperatingUnit = AP.IdOperatingUnit
	LEFT JOIN Payments.AgesPayments as AGP on AGP.SettingPaymentId = SP.Id and (DATEDIFF(DAY, APS.DateExpires, [Common].[GETDATE]()) + 1) BETWEEN AGP.InitialRange  and AGP.EndRange
	WHERE AP.Balance > 0 and AP.Status = 2) sc
	GROUP by	sc.SupplierId,sc.ThirdId,sc.SupplierName,sc.SupplierCode,sc.SupplierNit,sc.DescriptionLine,sc.DistributionLineId,sc.NatureExpenseConcept,
				sc.SupplierTypeId,sc.SupplierTypeName,sc.Invoice,sc.ExpirationDate,sc.AgePayment,sc.InvoiceBalance,sc.Share,sc.ShareExpirationDate,sc.BalanceShare,
				sc.AccountPayableId,sc.AccountPayableShareId,sc.AccountPayableCode,sc.CXPValue,sc.DiscountRate,sc.DiscountValue,sc.CxPRadicateDate,sc.BaseValue,
				sc.ValueTax,sc.RateValue,sc.PaymentDate,sc.ValueNote,sc.AdjusmentValueToAffectBase,sc.RangeNameDiscount