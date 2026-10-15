CREATE VIEW [MedicalFees].[ViewListMedicalFeesLiquidationDetail]
AS
SELECT mfc.AdmissionNumber,
       CONCAT(RTRIM(p.IPCODPACI), ' - ', p.IPNOMCOMP) AS PatientCode,
       CONCAT(t.Nit, ' - ', t.Name) AS ThirdPartyDescription,
       so.Code AS ServiceOrderCode,
       CONCAT(ips.Code, ' - ', ips.Name) AS IPSServiceName,
       mfc.AmountPayable,
       mfc.InvoiceQuantity,
       mfc.TotalAmountPayable,
       mfc.MedicalFeesContractValue,
       mfc.InvoiceReversal,
       mfc.Status AS StatusCausation,
       mfc.CausationDate,
       mfld.Id,
       mfld.LiquidationType,
       mfld.MedicalFeesLiquidacionId,
       mfld.MedicalFeesCausationId,
       sod.ServiceDate,
	   ind.InvoiceId,
	   mfc.InvoiceDetailId
FROM MedicalFees.MedicalFeesLiquidationDetail mfld
    INNER JOIN MedicalFees.MedicalFeesCausation mfc
        ON mfc.Id = mfld.MedicalFeesCausationId
    INNER JOIN Common.ThirdParty t
        ON t.Id = mfc.ThirdPartyId
    INNER JOIN Billing.ServiceOrder so
        ON so.Id = mfc.ServiceOrderId
    INNER JOIN Billing.ServiceOrderDetail sod
        ON sod.Id = mfc.ServiceOrderDetailId
    INNER JOIN Contract.IPSService ips
        ON ips.Id = sod.IPSServiceId
	INNER JOIN Billing.InvoiceDetail ind WITH(NOLOCK) 
		ON mfc.InvoiceDetailId = ind.Id
    LEFT JOIN dbo.INPACIENT p
        ON p.IPCODPACI = mfc.PatientCode;
GO