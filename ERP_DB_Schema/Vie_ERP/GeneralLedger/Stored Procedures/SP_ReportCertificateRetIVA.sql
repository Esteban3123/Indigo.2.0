
CREATE PROCEDURE [GeneralLedger].[SP_ReportCertificateRetIVA]
	-- Add the parameters for the stored procedure here
    @DateStart as date,
	@DateEnd as date,
	@AccountStart as varchar(50),
	@AccountEnd as varchar(50),
	@NitStart as varchar(15),
	@NitEnd as varchar(15),
	@RetencionType as integer,
	@LegalBookId as integer
AS
BEGIN
select gr.Rate,ct.Nit,ct.Name as Tercero,ma.Number,ma.Name as Concept,SUM(jvd.BillingValue) as ValorFacturado
,SUM(case mc.[Nature] when 1 then iif(jvd.CreditValue > 0, BaseValue * - 1, BaseValue) else iif(jvd.DebitValue > 0, BaseValue * - 1, BaseValue) end) as BaseRetención,
SUM(IIF(mc.[Nature] = 1,(jvd.DebitValue - jvd.CreditValue),(jvd.CreditValue - jvd.DebitValue))) as ValorRetenido,
'' as LettersValue
from GeneralLedger.JournalVoucherDetails as jvd
inner join GeneralLedger.JournalVouchers as jv on jv.Id = jvd.IdAccounting
left join GeneralLedger.RetentionConcepts as gr on gr.Id =jvd.IdRetention
inner join Common.ThirdParty as ct on ct.Id = jvd.IdThirdParty
inner join GeneralLedger.MainAccounts as ma on ma.Id = jvd.IdMainAccount
inner join GeneralLedger.MainAccountClasses as mc on mc.Id = ma.IdAccountClass 
where cast(jv.VoucherDate as date) between @DateStart and @DateEnd and ma.Number  >= ISNULL(@AccountStart,'0') and ma.Number <= ISNULL(@AccountEnd, 'z') and ct.Nit >= ISNULL(@NitStart,'0') and ct.Nit <= ISNULL(@NitEnd, 'z') and ma.RetencionType = @RetencionType and jv.LegalBookId = @LegalBookId and jv.IsClosedYear = 0 and jv.Status = 2
group by gr.Rate,ct.Nit,ct.Name,ma.Number,ma.Name having SUM(IIF(mc.[Nature] = 1,(jvd.DebitValue - jvd.CreditValue),(jvd.CreditValue - jvd.DebitValue))) > 0
order by ct.Nit,ma.Number
END