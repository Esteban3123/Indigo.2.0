-- =============================================
-- Author:		Andres Alarcon
-- Create date: 2025-10-16
-- Description:	Vista para listar causaciones YA reconocidas (para reversión)
-- =============================================

CREATE VIEW [MedicalFees].[ViewListCausationwithRecognition]
AS

SELECT 
	-- ID único para el grid
	mfc.Id AS CausationId
	
	-- Información del reconocimiento
	,cr.Id AS CausationRecognitionId
	,cr.VoucherDate AS RecognitionDate
	,cr.TotalSupplier AS TotalRecognition
	,cr.JournalVoucherConsecutive AS JournalVoucherConsecutive
	,cr.State AS RecognitionState
	
	-- Profesional de la salud
	,CONCAT(RTRIM(LTRIM(i.CODPROSAL)),' - ',i.NOMMEDICO) ProfessionalCodeName
	
	-- Contrato
	,CONCAT(mc.Code, ' - ', mc.ContractName) ContractCodeName
	,CASE mc.ContractType
		WHEN 1 THEN 'Médico'
		WHEN 2 THEN 'Agremiación'
	END AS ContractTypeName
	
	-- Proveedor (Supplier)
	,ISNULL(s2.Id, s.Id) SupplierId
	,IIF(mc.ContractType = 1, s2.Name, s.Name) SupplierName
	
	-- Información del servicio
	,mfc.AdmissionNumber
	,CONCAT(LTRIM(RTRIM(a.IPCODPACI)), ' - ', a.IPNOMCOMP) PatientCodeName
	,CONCAT(c.Code, ' - ', c.Name) CareGroupCodeName
	,CONCAT(ha.Code, ' - ', ha.Name) HealthAdministratorCodeName
	,CONCAT(ce.Code, ' - ', ce.Description) CupsEntityCodeName
	,mfc.InvoiceQuantity
	,sod.ServiceDate
	,CONCAT(fu.Code, ' - ', fu.Name) FunctionalUnitCodeName
	
	-- Valores
	,sod.TotalSalesPrice
	,mfc.CausationDate
	,mfc.MedicalFeesContractValue AS CausationValue
	,mfc.TotalAmountPayable AS TotalAmountPayable
	
	-- Estado de facturación
	,IIF(id.Id IS NOT NULL, 'Facturado', 'Sin Facturar') ServiceStatus
	,IIF(id.Id IS NOT NULL, inv.InvoiceNumber, NULL) InvoiceNumber
	
	-- Unidad operativa
	,so.OperatingUnitId
	
	-- Usuario que creó el reconocimiento
	,cr.CreationUser AS RecognitionUser
	
FROM MedicalFees.MedicalFeesCausation mfc

-- Reconocimiento (INNER JOIN porque solo queremos causaciones reconocidas)
INNER JOIN MedicalFees.CausationRecognition cr ON cr.Id = mfc.CausationRecognitionId

-- Profesional de la salud
INNER JOIN INPROFSAL i ON i.CODPROSAL = mfc.HealthProfessionalCode

-- Contrato
INNER JOIN MedicalFees.MedicalFeesContract mc ON mfc.MedicalFeesContractId = mc.Id

-- Paciente e ingreso
INNER JOIN INPACIENT a ON a.IPCODPACI = mfc.PatientCode
INNER JOIN ADINGRESO ad ON ad.NUMINGRES = mfc.AdmissionNumber

-- Orden de servicio
INNER JOIN Billing.ServiceOrderDetail sod ON sod.Id = mfc.ServiceOrderDetailId
INNER JOIN Billing.ServiceOrder so ON so.Id = sod.ServiceOrderId

-- Datos del servicio
INNER JOIN Contract.HealthAdministrator ha ON ha.Id = sod.HealthAdministratorId
INNER JOIN Contract.CareGroup c ON c.Id = sod.CareGroupId
INNER JOIN Payroll.FunctionalUnit fu ON fu.Id = sod.PerformsFunctionalUnitId
INNER JOIN Contract.CUPSEntity ce ON ce.Id = sod.CUPSEntityId

-- Proveedor (puede ser médico o agremiación)
LEFT JOIN Common.Supplier s ON s.Id = mc.SupplierId AND mc.ContractType = 2
LEFT JOIN Common.Supplier s2 ON s2.Id = i.GENPROVEE AND mc.ContractType = 1

-- Facturación (opcional)
LEFT JOIN Billing.InvoiceDetail id ON id.Id = mfc.InvoiceDetailId AND id.ServiceOrderDetailId = mfc.ServiceOrderDetailId
LEFT JOIN Billing.Invoice inv ON inv.Id = id.InvoiceId AND inv.Status = 1 AND inv.RevenueControlDetailId IS NOT NULL

WHERE mfc.Status = 1
	AND mfc.CausationRecognitionId IS NOT NULL  --Solo causaciones CON reconocimiento
	AND cr.State = 1  --Solo reconocimientos ACTIVOS (no reversados)
