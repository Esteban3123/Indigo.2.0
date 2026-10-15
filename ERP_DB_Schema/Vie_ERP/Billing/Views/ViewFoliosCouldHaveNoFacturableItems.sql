

CREATE view [Billing].[ViewFoliosCouldHaveNoFacturableItems]
as (
select distinct rcd.Id, rcd.CareGroupId
from Billing.RevenueControlDetail rcd
inner join Billing.RevenueControl rc on rc.Id = rcd.RevenueControlId
inner join [Contract].CareGroup cg on cg.Id = rcd.CareGroupId
inner join [Contract].CareGroupBillingItemsRestriction cgr on cg.Id = cgr.CareGroupId
inner join Contract.BillingItemsRestriction r on r.Id = cgr.BillingItemsRestrictionId and r.[Status] = 1
where rcd.Status in (1,3)
)