CREATE VIEW [Portfolio].[ViewRadicateDetailInvoiceCapitated]
AS
SELECT        rd.RadicateInvoiceCId, i.Id, i.OperatingUnitId, i.DocumentType, i.InvoiceNumber, i.RevenueControlDetailId, i.AdmissionNumber, i.HealthAdministratorId, i.ThirdPartyId, i.PatientCode, i.CareGroupId, i.InvoiceDate, 
                         i.InvoiceExpirationDate, i.TotalInvoice, i.CapitationInitialDate, i.CapitationEndDate, i.CapitationlPatientsAmount, i.CapitationPatientValue, i.ThirdPartySalesValue, i.ThirdPartyDiscountValue, 
                         i.ResponsibleRecoveryFee, i.TotalPatientSalesPrice, i.PatientDiscount, i.PatientDiscountPercentage, i.TotalPatientWithDiscount, i.ValueVoucher, i.CashReceiptId, i.JournalVoucherId, i.PatientPaidValue, 
                         i.ThirdPartyAccountReceivableValue, i.PatientAccountReceivableValue, i.PatientAccountReceivableId, i.PatientType, i.PatientAffiliatedType, i.PatientPaidAbility, i.PatientSocialClass, i.CREETaxRetentionValue, 
                         i.CREETaxRetentionBaseValue, i.Observation, i.InvoiceCategoryId, i.Status, i.InvoicedUser, i.InvoicedDate, i.AnnulmentUser, i.AnnulmentDate, i.ReversalReasonId, i.DescriptionReversal, i.TimeStamp
FROM            Portfolio.RadicateInvoiceD AS rd INNER JOIN
                         Billing.Invoice AS i ON rd.InvoiceNumber = i.InvoiceNumber