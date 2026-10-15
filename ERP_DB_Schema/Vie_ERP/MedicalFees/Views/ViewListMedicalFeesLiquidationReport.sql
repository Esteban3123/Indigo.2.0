CREATE VIEW [MedicalFees].[ViewListMedicalFeesLiquidationReport]
AS
SELECT	mfld.Id AS Row
	   ,mfl.Id AS Id
	   ,mfl.Code AS Code
	   ,mfl.HealthProfessionalCode
	   ,CONCAT(RTRIM(t.Nit), ' - ', RTRIM(t.Name)) AS Tercero
	   ,mfl.InitialDate
	   ,mfl.EndDate
	   ,mfld.LiquidationType
	   ,pfu.code AS UnitCode
	   ,pfu.Name AS UnitName
	   ,pcc.Code AS CostCode
	   ,pcc.Name AS CostName
	   ,mfc.AdmissionNumber
	   ,CONCAT(RTRIM(mfc.PatientCode), ' - ', RTRIM(p.IPNOMCOMP)) AS PatientDescription
	   ,cis.Code AS ServiceCode
	   ,cis.Name AS ServiceName
	   ,cce.Code AS CupsCode
	   ,cce.Description AS CupsDescripcion
	   ,mfc.InvoiceQuantity AS Quantity
	   ,mfc.TotalAmountPayable AS CausaTotal
	   ,mfl.CreationUser AS UserPrint
	   ,gm.Number AS NumberAccount
	   ,gm.Name AS NameAccount
	   ,CONCAT(RTRIM(tc.Nit), ' - ', RTRIM(tc.Name)) AS TerceroCausation
	   ,CONCAT(RTRIM(mfcc.Code), ' - ', RTRIM(mfcc.ContractName)) AS ContratoAgremiacion
	   ,mfl.LiquidationType AS TipoLiquidation
FROM MedicalFees.MedicalFeesLiquidationDetail mfld WITH (NOLOCK)
JOIN MedicalFees.MedicalFeesLiquidation mfl WITH (NOLOCK) ON mfl.Id = mfld.MedicalFeesLiquidacionId
JOIN MedicalFees.MedicalFeesCausation mfc WITH (NOLOCK) ON mfc.id = mfld.MedicalFeesCausationId
JOIN MedicalFees.MedicalFeesContract mfcc WITH (NOLOCK) ON mfcc.id = mfc.MedicalFeesContractId
JOIN Common.Supplier cs WITH (NOLOCK) ON cs.Id = mfl.SupplierId
JOIN [Billing].ServiceOrderDetail sod WITH (NOLOCK) ON sod.id = mfc.ServiceOrderDetailId
JOIN [Contract].IPSService cis WITH (NOLOCK) ON cis.Id = sod.IPSServiceId
JOIN [Contract].CUPSEntity cce WITH (NOLOCK) ON cce.Id = sod.CUPSEntityId
LEFT JOIN [Common].ThirdParty tc WITH (NOLOCK) ON tc.id = mfc.ThirdPartyId
LEFT JOIN [Common].ThirdParty t WITH (NOLOCK) ON t.id = cs.IdThirdParty
LEFT JOIN [Payroll].FunctionalUnit pfu WITH (NOLOCK) ON pfu.id = sod.PerformsFunctionalUnitId
LEFT JOIN [Payroll].CostCenter pcc WITH (NOLOCK) ON pcc.Id = sod.CostCenterId
LEFT JOIN [Billing].BillingConcept bc WITH (NOLOCK) ON bc.id = cce.BillingConceptId
LEFT JOIN Billing.BillingConceptAccount bca WITH (NOLOCK) ON bc.Id = bca.BillingConceptId AND bc.AccountingType = 2 AND bca.UnitType = Billing.fnGetUnitType(pfu.UnitType)
LEFT JOIN [GeneralLedger].MainAccounts gm WITH (NOLOCK) ON gm.id = ISNULL(bca.FeesExpensesAccountId, bc.FeesExpensesAccountId)
LEFT JOIN dbo.INPACIENT p WITH (NOLOCK) ON p.IPCODPACI = mfc.PatientCode