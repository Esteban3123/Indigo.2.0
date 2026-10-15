


CREATE VIEW [GeneralLedger].[VBalanceMainAccount]
as
select glb.[Year] as YearBalance, glb.[Month] as MonthBalance, sum(glb.DebitValue) as DebitValue, sum(glb.CreditValue) as CreditValue, ma.Number as NumberAccount, mac.Nature as Nature, ma.LegalBookId as LegalBookId, ma.Id as IdAccount  
from GeneralLedger.GeneralLedgerBalance glb with (nolock) 
inner join GeneralLedger.MainAccounts ma with (nolock)  on ma.Id = glb.IdMainAccount 
inner join GeneralLedger.MainAccountClasses mac with (nolock)  on mac.Id = ma.IdAccountClass
group by glb.Year,glb.Month,ma.Number,mac.Nature,ma.LegalBookId,ma.Id