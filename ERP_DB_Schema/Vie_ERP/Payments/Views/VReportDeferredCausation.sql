


CREATE VIEW [Payments].[VReportDeferredCausation]
AS
select
dcs.Id as deferredCausationId, 
dcs.PaymentMonth  as paymentMonth,
dcs.PaymentYear as paymentYear, 
ap.Code as accountPayableCode, 
ap.BillNumber as accountPayableBillNumber, 
tp.Nit + ' - ' + tp.Name as supplierDescription,
case dc.PeriodsNumber
	when 1 then cast(dc.PeriodsNumber as varchar(2)) + ' Mes' 
	else cast(dc.PeriodsNumber as varchar(2)) + ' Meses' 
end as periodMonth,
dcs.Value as valuePeriod, 
ISNULL((select 
			Sum(dcsna.Value) 
			from Payments.DeferredCausationShare dcsna 
			where dcsna.DeferredCausationId = dcs.DeferredCausationId ),0)  - 
			ISNULL((select
			Sum(dcsa.Value) 
			from Payments.DeferredCausationShare dcsa
			where dcsa.DeferredCausationId = dcs.DeferredCausationId AND dcsa.Amortized=1
			And cast(('01/' + RIGHT('0'+cast(dcsa.PaymentMonth as varchar(2)),2) 
			+ '/'+ cast(dcsa.PaymentYear as varchar(4))) as date) <= cast(('01/' + 
			RIGHT('0'+cast(dcs.PaymentMonth as varchar(2)),2) + '/'+ cast(dcs.PaymentYear as varchar(4))) as date)),0)  as valueNotArmotize,
ISNULL((select
		Sum(dcsa.Value) 
		from Payments.DeferredCausationShare dcsa
		where dcsa.DeferredCausationId = dcs.DeferredCausationId AND dcsa.Amortized=1
		And cast(('01/' + RIGHT('0'+cast(dcsa.PaymentMonth as varchar(2)),2) 
		+ '/'+ cast(dcsa.PaymentYear as varchar(4))) as date) <= cast(('01/' + 
		RIGHT('0'+cast(dcs.PaymentMonth as varchar(2)),2) + '/'+ cast(dcs.PaymentYear as varchar(4))) as date)),0) as valueArmotize,
		c.Id as CurrencyId,
		c.Abbreviation as CurrencyAbbreviation,
		dcs.Amortized
from Payments.DeferredCausationShare dcs 
inner join Payments.DeferredCausation dc on dc.Id = dcs.DeferredCausationId
inner join Payments.AccountPayable ap on ap.Id = dc.IdAccountPayable inner join Common.Supplier s on s.Id = ap.IdSupplier
inner join Common.ThirdParty tp on tp.Id = s.IdThirdParty
left join Common.Currency c on c.Id = ap.CurrencyId