

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

GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todas las causaciones de honorarios médicos que ya tienen un reconocimiento activo (no reversado), sirviendo como base para el proceso de reversión de reconocimientos. Integra información del profesional de la salud, el contrato de honorarios (tipo médico individual o agremiación), el paciente y su ingreso, el detalle de la orden de servicio facturada (EPS/pagador, grupo de atención, código CUPS y unidad funcional), y los valores de causación y reconocimiento. Adicionalmente indica si el servicio está facturado o no, mostrando el número de factura cuando aplica, y expone el proveedor (médico o agremiación) asociado al contrato de honorarios.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'VIEW', @level1name = N'ViewListCausationwithRecognition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'VIEW', @level1name = N'ViewListCausationwithRecognition';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las causaciones de honorarios médicos que ya cuentan con un reconocimiento contable activo, con el fin de habilitar su consulta y eventual reversión.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationwithRecognition';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las causaciones deben estar en estado activo y vinculadas a un reconocimiento existente; El reconocimiento referenciado debe estar activo (no reversado); Cada causación debe tener referencias válidas a profesional de salud, contrato, paciente, ingreso, orden de servicio y su detalle, administradora de salud, grupo de atención, unidad funcional y entidad CUPS', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationwithRecognition';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan causaciones activas (Status = 1); Solo se incluyen causaciones que ya tienen un reconocimiento asociado (CausationRecognitionId IS NOT NULL); Solo se listan reconocimientos en estado activo (State = 1), excluyendo los ya reversados; El proveedor se resuelve de forma excluyente según el tipo de contrato: del profesional para tipo Médico, del contrato para tipo Agremiación; El número de factura solo se expone si la factura tiene Status = 1 y un RevenueControlDetailId no nulo; Las causaciones deben tener integridad referencial completa con profesional, contrato, paciente, ingreso, orden de servicio, administradora de salud, grupo de atención, unidad funcional y entidad CUPS (INNER JOINs)', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationwithRecognition';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Causación de honorarios médicos; Reconocimiento de causación; Reversión de reconocimiento; Profesional de la salud; Contrato de honorarios; Tipo de contrato (Médico/Agremiación); Proveedor; Paciente e ingreso; Orden de servicio; Administradora de salud; Grupo de atención (CareGroup); CUPS; Unidad funcional; Facturación; Comprobante contable (Journal Voucher)', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationwithRecognition';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MedicalFees.MedicalFeesCausation: Devuelve causaciones donde Status = 1 AND CausationRecognitionId IS NOT NULL AND CausationRecognition.State = 1, enriquecidas con datos del reconocimiento, profesional, contrato, proveedor, paciente, servicio y estado de facturación', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationwithRecognition';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ContractType = 1 (Médico) → Etiqueta el tipo como ''Médico'' y toma el proveedor desde el profesional de la salud (i.GENPROVEE) usando su nombre else Si ContractType = 2 (Agremiación), etiqueta como ''Agremiación'' y toma el proveedor desde el contrato (mc.SupplierId); si Existe InvoiceDetail asociado al detalle de orden de servicio causado → El servicio se reporta como ''Facturado'' y se expone el número de factura else Se reporta como ''Sin Facturar'' y el número de factura queda en NULL', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationwithRecognition';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalFees.MedicalFeesCausation; MedicalFees.CausationRecognition; MedicalFees.MedicalFeesContract; dbo.INPROFSAL; dbo.INPACIENT; dbo.ADINGRESO; Billing.ServiceOrderDetail; Billing.ServiceOrder; Contract.HealthAdministrator; Contract.CareGroup; Payroll.FunctionalUnit; Contract.CUPSEntity; Common.Supplier; Billing.InvoiceDetail; Billing.Invoice', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationwithRecognition';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationwithRecognition';
GO
