CREATE VIEW [Budget].[ViewListObligationOfFixedAssetEntry]
AS
select ROW_NUMBER() OVER (ORDER BY ev.Id) Id,
ev.Id FixedAssetEntryId,
ap.Id AccountPayableId,
od.Id ObligationDetailId, od.InitialValue, od.Balance, od.EntityId, od.EntityCode, od.EntityName,
o.Id ObligationId, o.Code ObligationCode, o.Document ObligationDocument,
c.Id CategoryId, c.Name CategoryName, c.Code + ' - ' + c.Name CategoryDescription,
rt.Id RevenueTypeId, rt.Code + ' - ' + rt.Name RevenueTypeDescription,
fs.Id FinancialSourceId, fs.Code + ' - ' + fs.Name FinancialSourceDescription,
od.CommitmentDetailId
from FixedAsset.FixedAssetEntry ev
inner join Payments.AccountPayable ap on ap.Id = ev.AccountPayableId
inner join Budget.ObligationDetail od on od.EntityId = ap.Id and od.EntityName = 'AccountPayable'
inner join Budget.Obligation o on o.Id = od.ObligationId
inner join Budget.Category c on c.Id = od.CategoryId
inner join Budget.RevenueType rt on rt.Id = od.RevenueTypeId
left join Budget.FinancialSource fs on fs.Id = c.FinancialSourceId