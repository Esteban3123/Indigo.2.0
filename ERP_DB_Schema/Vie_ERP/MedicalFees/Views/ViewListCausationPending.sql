
CREATE VIEW [MedicalFees].[ViewListCausationPending]
AS

SELECT 
	cp.Id
	,sod.Id AS ServiceOrderDetailId
	,CONCAT(RTRIM(LTRIM(cp.PerformsHealthProfessionalCode)),' - ',i.NOMMEDICO) ProfessionalCodeName
	,CONCAT(mc.Code, ' - ', mc.ContractName) ContractCodeName
	,CASE mc.ContractType
		WHEN 1 THEN 'Médico'
		WHEN 2 THEN 'Agremiación'
	END AS ContractTypeName
	,ISNULL(s2.Id, s.Id) SupplierId
	,IIF(mc.ContractType = 1, s2.Name, s.Name) SupplierName
	,cp.AdmissionNumber
	,CONCAT(LTRIM(RTRIM(cp.PatientCode)), ' - ', cp.PatientName) PatientCodeName
	,CONCAT(c.Code, ' - ', c.Name) CareGroupCodeName
	,CONCAT(ha.Code, ' - ', ha.Name) HealthAdministratorCodeName
	,CONCAT(ce.Code, ' - ', ce.Description) CupsEntityCodeName
	,CAST(JSON_VALUE(cp.Data, '$."InvoicedQuantity"') AS DECIMAL(18,2)) InvoiceQuantity
	,sod.ServiceDate
	,CONCAT(fu.Code, ' - ', fu.Name) FunctionalUnitCodeName
	,sod.TotalSalesPrice
	,NULL CausationDate
	,0 CausationValue
	,IIF(cp.InvoiceNumber = 'SIN-FACTURA', 'No Facturado', 'Facturado') ServiceStatus
	,cp.InvoiceNumber InvoiceNumber
	,cp.Error ErrorMessage
	,so.OperatingUnitId
FROM MedicalFees.CausationPending cp
INNER JOIN Billing.ServiceOrderDetail sod ON sod.Id = CAST(JSON_VALUE(cp.Data, '$.ServiceOrderDetailId') AS INT)
INNER JOIN Billing.ServiceOrder so ON so.Id = sod.ServiceOrderId
INNER JOIN Contract.HealthAdministrator ha ON ha.Id = sod.HealthAdministratorId
INNER JOIN Contract.CareGroup c ON c.Id = sod.CareGroupId
INNER JOIN Payroll.FunctionalUnit fu ON fu.Id = sod.PerformsFunctionalUnitId
INNER JOIN Contract.CUPSEntity ce ON ce.Id = sod.CUPSEntityId
LEFT JOIN MedicalFees.HealthProfessionalContract hpc ON hpc.HealthProfessionalCode = RTRIM(LTRIM(cp.PerformsHealthProfessionalCode)) 
														AND hpc.LiquidateDefault = ISNULL(1, 0)
LEFT JOIN MedicalFees.MedicalFeesContract mc ON mc.Id = ISNULL(hpc.MedicalFeesContractId, 0)
LEFT JOIN INPROFSAL i ON i.CODPROSAL = cp.PerformsHealthProfessionalCode
LEFT JOIN Common.Supplier s ON s.Id = mc.SupplierId AND mc.ContractType = 2
LEFT JOIN Common.Supplier s2 ON s2.Id = i.GENPROVEE AND mc.ContractType = 1
WHERE ISNULL(s2.Id, s.Id) IS NOT NULL