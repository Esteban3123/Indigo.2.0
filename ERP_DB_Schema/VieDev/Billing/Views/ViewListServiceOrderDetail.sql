

CREATE VIEW [Billing].[ViewListServiceOrderDetail]
AS

select 
sod.Id,
CONCAT(ips.Code,' - ',ips.Name) as CodeNameIpsService,
CONCAT(c.Code, ' - ', c.Name) as CodeNameCareGroup,
sod.RecordType,
sod.Packaging,
sod.InvoicedQuantity,
so.AdmissionNumber,
so.Status
from Billing.ServiceOrderDetail sod
JOIN Billing.ServiceOrder so ON so.Id = sod.ServiceOrderId
JOIN Contract.IPSService ips on ips.Id = sod.IPSServiceId
JOIN Contract.CareGroup c ON c.Id = sod.CareGroupId