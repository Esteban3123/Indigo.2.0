CREATE VIEW [Billing].[ViewRelatedInvoices]
AS
SELECT	CONCAT(rc.AdmissionNumber, '-', i.Id) Id, 
		rc.AdmissionNumber,
		i.Id InvoiceId,
		i.InvoiceNumber,
		i.InvoiceDate,
		i.ThirdPartySalesValue
FROM Billing.RevenueControl rc
JOIN Billing.RevenueControlDetail rcd ON rc.Id = rcd.RevenueControlId
JOIN Billing.Invoice i ON rcd.Id = i.RevenueControlDetailId