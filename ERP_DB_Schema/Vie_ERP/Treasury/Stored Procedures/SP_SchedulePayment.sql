-- =============================================
-- Author:		Diego Andrés Roldán Lozano
-- Create date: 15-08-2014
-- Description:	sp para consultar la programacion de pagos
-- =============================================
CREATE PROCEDURE [Treasury].[SP_SchedulePayment]	
	@SupplierTypeIdList varchar(MAX), --contiene los ids de supplierType en formato: 1,2,3,4,5,6
	@PaymentD  VARCHAR(50)
AS
BEGIN	
	SET NOCOUNT ON;

	IF @PaymentD IS NULL
		SET @PaymentD  =[Common].[GETDATE]();
		
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
	ap.Code AS AccountPayableCode,
	ap.Value As CXPValue,
	iif(gppcxp.BaseValue > 1,isnull(ppd.RangeName,''),'') AS RangeNameDiscount,
	iif(gppcxp.BaseValue > 1,cast(isnull(ppd.DiscountRate,0) as DECIMAL(5,2)),0) AS DiscountRate,
	iif(gppcxp.BaseValue > 1, cast(gppcxp.BaseValue * isnull(ppd.DiscountRate,0)/100 as decimal(18,2)),0) AS DiscountValue,
	ap.ServicePeriodDate as CxPRadicateDate,
	gppcxp.BaseValue as BaseValue,
	gppcxp.ValueTax,
	gppcxp.RateValue,
	CAST(@PaymentD AS DATETIME) AS PaymentDate,
	ISNULL(pnd.AdjusmentValue,0) ValueNote,
	ISNULL(pnd.AdjusmentValueToAffectBase,0) AdjusmentValueToAffectBase,
	isnull(c.Id, ccy.Id) as CurrencyId,
	isnull(c.Abbreviation, ccy.Abbreviation) as CurrencyAbbreviation
FROM Payments.AccountPayable AS ap WITH (NOLOCK)
JOIN Common.Supplier AS s WITH (NOLOCK) ON ap.IdSupplier = s.Id
OUTER APPLY (	SELECT  ppd.RangeName, ppd.DiscountRate
				from Common.PromptPaymentDiscount AS ppd  WITH (NOLOCK)
				where SupplierId =s.Id and (DATEDIFF(DAY,ap.ServicePeriodDate,CAST(@PaymentD AS DATETIME))) BETWEEN ppd.InitialRank and ppd.EndRank
				) AS ppd
OUTER APPLY (SELECT * from Payments.GetPromptPaymentCxP(ap.EntityId,ap.EntityName,ap.IdSupplier,ap.BillNumber,ap.Id)) gppcxp
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
LEFT JOIN Common.Currency c on c.Id = ap.CurrencyId
OUTER APPLY (
	SELECT TOP 1 cr.Id, cr.Abbreviation 
	FROM GeneralLedger.CompanySettings cs
	JOIN Common.Currency cr on cs.OfficialCurrencyId = cr.Id
) AS ccy
WHERE AP.Balance > 0 and AP.Status = 2 
and ST.Id in (IIF(@SupplierTypeIdList = '', (ST.Id), (SELECT Item from dbo.SplitStrings_Moden(@SupplierTypeIdList, ','))))
order by ap.BillNumber

END