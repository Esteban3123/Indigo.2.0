


CREATE view [Billing].[ViewElectronicRipsInfoHeader]
as

SELECT 
	i.InvoiceNumber,
	tp.Nit
FROM Billing.Invoice i
join Common.ThirdParty tp on i.ThirdPartyId = tp.Id