

CREATE view [Billing].[InvoiceWithoutElectronicDocument] as (
SELECT Id, InvoiceNumber from Billing.Invoice
where Id not in (select EntityId from Billing.ElectronicDocument where EntityName = 'Invoice')
)