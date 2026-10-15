
CREATE VIEW [MedicalFees].[ViewListPaymentsMedicalFeesLiquidation]
AS
SELECT mfc.Id as MedicalFeesCausationId,
       mfc.AdmissionNumber,
       mfc.PatientCode,
       CONCAT(RTRIM(mfc.PatientCode), ' - ', RTRIM(p.IPNOMCOMP)) AS PatientDescription,
       mfc.HealthProfessionalCode,
       mfc.ThirdPartyId,
       CONCAT(t.Nit, ' - ', t.Name) AS ThirdPartyDescription,
       mfc.MedicalFeesContractId,
       mfc.CausationDate,
       mfc.ServiceOrderId,
       so.Code AS CodeServiceOrder,
       mfc.ServiceOrderDetailId,
       mfc.AmountPayable,
       mfc.MedicalFeesContractValue,
       mfc.InvoiceQuantity,
       mfc.TotalAmountPayable,
       mfc.MedicalFeePaid,
       mfc.InvoiceReversal,
       mfc.ReassessmentForReversal,
       mfc.ReassessmentForObjection,
       mfc.ObjectionAccepted,
       mfc.Status,
       CONCAT(mfc.AdmissionNumber, ' - ', mfc.PatientCode) AS AdmissionNumberPatientCode,
       CONCAT(ips.Code, ' - ', ips.Name) AS IPSServiceName,
       sod.ServiceDate,
	   ind.InvoiceId,
	   mfc.InvoiceDetailId
FROM [MedicalFees].MedicalFeesCausation mfc WITH (NOLOCK)
    INNER JOIN [Common].ThirdParty t WITH (NOLOCK)
        ON mfc.ThirdPartyId = t.Id
    INNER JOIN [Billing].ServiceOrder so WITH (NOLOCK)
        ON mfc.ServiceOrderId = so.Id
    INNER JOIN [Billing].ServiceOrderDetail sod WITH (NOLOCK)
        ON mfc.ServiceOrderDetailId = sod.Id
    INNER JOIN [Contract].IPSService ips WITH (NOLOCK)
        ON sod.IPSServiceId = ips.Id
	INNER JOIN Billing.InvoiceDetail ind WITH(NOLOCK) ON mfc.InvoiceDetailId = ind.Id
    LEFT JOIN dbo.INPACIENT p WITH (NOLOCK)
        ON mfc.PatientCode = p.IPCODPACI