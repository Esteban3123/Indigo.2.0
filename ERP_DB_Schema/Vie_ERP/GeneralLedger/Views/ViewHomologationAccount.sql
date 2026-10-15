


CREATE VIEW [GeneralLedger].[ViewHomologationAccount]
as
(

select  h.Id,
m.Id as OfficalMainAccountId,
CONCAT(m.Number,' - ',m.Name) as NumberNameOfficialMainAccount,
h.MainAccountId, 
CONCAT(mh.Number,' - ',mh.Name) as NumberNameMainAccount,
b.Id as LegalBookId,
mh.LegalBookId as HomologationLegalBookId,
m.HandlesCostCenter as HandlesCostCenterOfficialMainAccount,
m.HandlesThirdParty as HandlesThirdPartyOfficialMainAccount,
h.CreationUser,
h.CreationDate,
h.ModificationUser,
h.ModificationDate
from GeneralLedger.LegalBook b with (nolock) 
inner join GeneralLedger.MainAccounts m with (nolock)  on m.LegalBookId = b.Id
left join GeneralLedger.HomologationAccount h  with (nolock) on h.OfficialMainAccountId = m.Id
left join GeneralLedger.MainAccounts mh  with (nolock) on mh.id = h.MainAccountId
where m.AllowsMovement = 1 and m.Status = 1

)