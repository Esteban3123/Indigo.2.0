









CREATE VIEW [Billing].[ViewListInvoiceAndPAtient]
AS

select I.Id, I.InvoiceNumber, I.InvoiceDate, I.AdmissionNumber, RTRIM(INP.IPNOMCOMP) AS PatientName, 
I.PatientCode, IC.Code + ' - ' + IC.Name AS InvoiceCategory
from Billing.Invoice AS  I with (nolock)
LEFT OUTER JOIN Billing.InvoiceCategories as IC  with (nolock) ON I.InvoiceCategoryId = IC.Id
left JOIN dbo.INPACIENT AS INP  with (nolock) ON I.PatientCode = INP.IPCODPACI