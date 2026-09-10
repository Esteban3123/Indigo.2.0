CREATE VIEW [Portfolio].[ViewRadicateInvoiceDetailSearch]
AS
select
	rd.id as Row,
	rd.InvoiceNumber,
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
		ELSE '' END AS [RIPSStatusName]
from Portfolio.RadicateInvoiceC rc
join Common.Customer cus on rc.CustomerId = cus.Id
join Portfolio.RadicateInvoiceD rd on rc.Id = rd.RadicateInvoiceCId
join Portfolio.AccountReceivable ar on rd.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType in (1,2)
left join Billing.ElectronicsProperties iep ON ar.InvoiceId = iep.EntityId and iep.EntityName ='Invoice'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista angosta usada exclusivamente por el buscador de consecutivos de radicación (FrmInvoiceRadicate). Es una versión reducida de Portfolio.ViewRadicateInvoiceDetail: solo trae las columnas necesarias para el grid de búsqueda (número de factura, consecutivo, entidad, fecha, estado del radicado y estado de validación RIPS), evitando los JOINs a Billing.Invoice, Billing.InvoiceCategories, Billing.ElectronicsRIPS y Common.Currency que no aportan a esta pantalla.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewRadicateInvoiceDetailSearch';
GO
