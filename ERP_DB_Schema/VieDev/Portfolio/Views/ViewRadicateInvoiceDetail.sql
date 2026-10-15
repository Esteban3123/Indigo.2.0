
CREATE VIEW [Portfolio].[ViewRadicateInvoiceDetail]
AS
select 
	rd.id as Row,
	ic.Code + ' - ' + ic.name as CategoryName,
	c.Id CurrencyId,
	c.Name CurrencyName,
	c.Abbreviation CurrencyAbbreviation,
	rd.* ,
	iep.CUV,
	rc.RadicatedConsecutive,
	CONCAT(cus.Nit,' - ',cus.Name) NitName,
	rc.DocumentDate,
	CASE rc.State
		WHEN 1 THEN 'Sin Confirmar'
		WHEN 2 THEN 'Confirmado'
		ELSE 'Anulado'
	END [HeaderState],
	CASE iep.StatusRIPS
		WHEN 1 THEN 'Sin validar'
		WHEN 2 THEN 'En proceso de validación'
		WHEN 3 THEN 'Validado'
		ELSE '' END AS  [RIPSStatusName],
	er.CosmoDBId as CosmoDBId

from Portfolio.RadicateInvoiceC rc  with (nolock) 
join Common.Customer cus WITH(NOLOCK) on rc.CustomerId = cus.Id
join Portfolio.RadicateInvoiceD rd  with (nolock) on rc.Id = rd.RadicateInvoiceCId 
join Portfolio.AccountReceivable ar with (nolock) on rd.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType in (1,2)
join Billing.InvoiceCategories ic with (nolock) on ar.InvoiceCategoryId = ic.id
left join Billing.Invoice i WITH (NOLOCK) on rd.InvoiceNumber = i.InvoiceNumber
left join Billing.ElectronicsProperties iep WITH(NOLOCK) ON i.Id = iep.EntityId and iep.EntityName ='Invoice'
left JOIN Billing.ElectronicsRIPS er on er.ElectronicsPropertiesId = iep.Id
left join Common.Currency c WITH (NOLOCK) on ISNULL(i.CurrencyId, ar.CurrencyId) = c.Id