


CREATE VIEW [Taxes].[VReportGenerateInvoice]
AS
select ti.InvoiceNumber,
tid.Id,
tp.id as TaId,
Getdate() as DeadLine,
concat(su.UserCode,sp.Fullname) as CreationUser,
ct.Nit,
ct.Name as Tercero,  
tp.Code,
concat('(415)7709998000506(8020)',ti.InvoiceNumber,'(3900)',REPLACE(STR(ti.TaxValue, 10), SPACE(1), '0'),'(96)',convert(varchar,GETDATE(),112)) as BarCode,
SUBSTRING(tp.Code,1,2) as TypeCode,
SUBSTRING(tp.Code,3,4) as Section,
SUBSTRING(tp.Code,5,8) as Block,
concat(tp.LandArea,' Mts2') as LandArea,
Iif(tp.LandArea=0,'0',(tp.LandArea / 10000)) as Hectare,
concat(tp.BuiltArea,' Mts2') as BuildArea,
tp.Addres,
tid.Value,
ti.Validity,
ti.Appraisal,
ti.TaxValue,
0 as Interest,
0 as Total,
tp.Id as IdProperty,
tlc.Code as CodeLiquidationConcept,
concat(tlc.Code,' ',tlc.Name,' ',tid.PercentageConcept,' %') as LiquidationConcept,
0 as PreviousValidity,
concat(ISNULL((select top 1 cast(PercentageDiscount as float) as PercentageDiscount from Portfolio.AccountReceivablePromptPayment WITH (NOLOCK) where AccountReceivableId = par.id and DeadLine >= cast(getdate() as date) order by DeadLine asc),'0'),' %')as PercentageDiscount,
ISNULL((select top 1 (ti.TaxValue * (PercentageDiscount / 100)) as PercentageDiscount from Portfolio.AccountReceivablePromptPayment WITH (NOLOCK) where AccountReceivableId = par.id and DeadLine >= cast(getdate() as date) order by DeadLine asc),0)as VrDscto,
ISNULL((select top 1 (ti.TaxValue) as PercentageDiscount from Portfolio.AccountReceivablePromptPayment WITH (NOLOCK) where AccountReceivableId = par.id and DeadLine >= cast(getdate() as date) order by DeadLine asc),0)as VrBaseDscto
from Taxes.TaxesInvoiceDetail as tid WITH (NOLOCK)
inner join taxes.TaxesLiquidationConcept as tlc WITH (NOLOCK) on tlc.id = tid.LiquidationConceptId
inner join taxes.TaxesInvoice as ti WITH (NOLOCK) on ti.Id = tid.TaxesInvoiceId
inner join taxes.TaxesProperty as tp WITH (NOLOCK) on tp.Id = ti.TaxesPropertyId
inner join Common.ThirdParty as ct WITH (NOLOCK) on ct.id = ti.ThirdPartyId
inner join [Security].[User] as su on su. UserCode = ti.CreationUser
inner join [Security].Person as sp on sp.id = su.IdPerson
inner join Portfolio.AccountReceivable as par WITH (NOLOCK) on par.InvoiceNumber = ti.InvoiceNumber
where par.Balance > 0