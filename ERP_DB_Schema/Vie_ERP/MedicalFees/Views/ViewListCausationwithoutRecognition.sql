CREATE VIEW [MedicalFees].[ViewListCausationwithoutRecognition]
AS

SELECT 
	mfc.Id
	,CONCAT(RTRIM(LTRIM(i.CODPROSAL)),' - ',i.NOMMEDICO) ProfessionalCodeName
	,CONCAT(mc.Code, ' - ', mc.ContractName) ContractCodeName
	,CASE mc.ContractType
		WHEN 1 THEN 'Médico'
		WHEN 2 THEN 'Agremiación'
	END AS ContractTypeName
	,ISNULL(s2.Id, s.Id) SupplierId
	,IIF(mc.ContractType = 1, s2.Name, s.Name) SupplierName
	,mfc.AdmissionNumber
	,CONCAT(LTRIM(RTRIM(a.IPCODPACI)), ' - ', a.IPNOMCOMP) PatientCodeName
	,CONCAT(c.Code, ' - ', c.Name) CareGroupCodeName
	,CONCAT(ha.Code, ' - ', ha.Name) HealthAdministratorCodeName
	,CONCAT(ce.Code, ' - ', ce.Description) CupsEntityCodeName
	,mfc.InvoiceQuantity
	,sod.ServiceDate
	,CONCAT(fu.Code, ' - ', fu.Name) FunctionalUnitCodeName
	,sod.TotalSalesPrice
	,mfc.CausationDate
	,mfc.MedicalFeesContractValue CausationValue
	,IIF(id.Id IS NOT NULL, 'Facturado', 'Sin Facturar') ServiceStatus
	,IIF(id.Id IS NOT NULL, inv.InvoiceNumber, NULL) InvoiceNumber
	,so.OperatingUnitId
FROM MedicalFees.MedicalFeesCausation mfc
INNER JOIN INPROFSAL i ON i.CODPROSAL = mfc.HealthProfessionalCode
INNER JOIN MedicalFees.MedicalFeesContract mc ON mfc.MedicalFeesContractId = mc.Id
INNER JOIN INPACIENT a ON a.IPCODPACI = mfc.PatientCode
INNER JOIN ADINGRESO ad ON ad.NUMINGRES = mfc.AdmissionNumber
INNER JOIN Billing.ServiceOrderDetail sod ON sod.Id = mfc.ServiceOrderDetailId
INNER JOIN Billing.ServiceOrder so ON so.Id = sod.ServiceOrderId
INNER JOIN Contract.HealthAdministrator ha ON ha.Id = sod.HealthAdministratorId
INNER JOIN Contract.CareGroup c ON c.Id = sod.CareGroupId
INNER JOIN Payroll.FunctionalUnit fu ON fu.Id = sod.PerformsFunctionalUnitId
INNER JOIN Contract.CUPSEntity ce ON ce.Id = sod.CUPSEntityId
LEFT JOIN Common.Supplier s ON s.Id = mc.SupplierId AND mc.ContractType = 2
LEFT JOIN Common.Supplier s2 ON s2.Id = i.GENPROVEE AND mc.ContractType = 1
LEFT JOIN Billing.InvoiceDetail id ON id.Id = mfc.InvoiceDetailId AND id.ServiceOrderDetailId = mfc.ServiceOrderDetailId
LEFT JOIN Billing.Invoice inv ON inv.Id = id.InvoiceId AND inv.Status = 1 AND inv.RevenueControlDetailId IS NOT NULL
WHERE mfc.Status = 1
	AND mfc.CausationRecognitionId IS NULL  --Solo causaciones SIN reconocimiento
	AND mfc.MedicalFeesContractValue > 0 -- Solo causaciones con valor mayor a 0
	AND ISNULL(s2.Id, s.Id) IS NOT NULL
