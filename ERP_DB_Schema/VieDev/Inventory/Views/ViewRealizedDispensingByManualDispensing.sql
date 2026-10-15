



CREATE VIEW [Inventory].[ViewRealizedDispensingByManualDispensing]
AS

select ROW_NUMBER() OVER (ORDER BY mfpd.Id) AS Id,
mfpd.MedicalFormulaId, 
mfpd.PharmaceuticalDispensingId, 
mfpd.PharmaceuticalDispensingCode, 
pd.AdmissionNumber, 
i.Id InvoiceId, 
i.InvoiceNumber
from Inventory.MedicalFormulaPharmaceuticalDispensing mfpd WITH(NOLOCK)
inner join Inventory.PharmaceuticalDispensing pd WITH(NOLOCK) on pd.Id = mfpd.PharmaceuticalDispensingId
inner join Billing.Invoice i WITH(NOLOCK) on i.AdmissionNumber = pd.AdmissionNumber