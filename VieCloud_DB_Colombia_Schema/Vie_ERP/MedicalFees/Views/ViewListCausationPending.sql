

CREATE VIEW [MedicalFees].[ViewListCausationPending]
AS

SELECT 
	cp.Id
	,sod.Id AS ServiceOrderDetailId
	,CONCAT(RTRIM(LTRIM(i.CODPROSAL)),' - ',i.NOMMEDICO) ProfessionalCodeName
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de causaciones pendientes de honorarios médicos que aún no han sido liquidadas o procesadas. Integra los detalles de órdenes de servicio facturadas (procedimientos, cantidades, tarifas y fecha de servicio) con el profesional de la salud ejecutante, el contrato de honorarios médicos aplicable (individual o por agremiación), la EPS o pagador responsable, el grupo de atención, la unidad funcional y el código CUPS del procedimiento. Permite identificar qué servicios prestados tienen causación pendiente de honorarios, si ya fueron facturados o no, el valor de venta total y cualquier mensaje de error en el proceso de causación. Es el insumo principal para la gestión y auditoría del pago de honorarios a médicos y agremiaciones con contrato vigente en la institución.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'VIEW', @level1name = N'ViewListCausationPending';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'VIEW', @level1name = N'ViewListCausationPending';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las causaciones de honorarios médicos pendientes con datos del profesional, contrato, paciente, servicio y proveedor asociado, identificando si el servicio está facturado o no.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationPending';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existir registro en CausationPending con un ServiceOrderDetailId válido en el JSON Data; El detalle de orden de servicio debe existir en Billing.ServiceOrderDetail con su orden, administradora, grupo de atención, unidad funcional y entidad CUPS asociadas', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationPending';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda fila listada tiene un proveedor identificable (médico o agremiación); El contrato de honorarios considerado es el marcado como liquidación por defecto del profesional (LiquidateDefault = 1); CausationDate siempre es NULL y CausationValue siempre 0, indicando que la causación aún no se ha materializado; El tipo de contrato solo puede interpretarse como ''Médico'' (1) o ''Agremiación'' (2)', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationPending';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Causación de honorarios médicos pendiente; Profesional de la salud; Contrato de honorarios médicos; Tipo de contrato (Médico/Agremiación); Proveedor; Paciente; Administradora de salud (EPS); Grupo de atención; Unidad funcional; CUPS; Orden de servicio; Estado de facturación (Facturado/No Facturado)', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationPending';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MedicalFees.ViewListCausationPending: Solo retorna filas donde exista un proveedor resoluble: ISNULL(s2.Id, s.Id) IS NOT NULL', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationPending';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si mc.ContractType = 1 → Se clasifica como contrato ''Médico'' y el proveedor proviene de INPROFSAL.GENPROVEE (s2) else Si ContractType = 2 se clasifica como ''Agremiación'' y el proveedor proviene de mc.SupplierId (s); si cp.InvoiceNumber = ''SIN-FACTURA'' → Se marca ServiceStatus = ''No Facturado'' else Se marca ServiceStatus = ''Facturado''', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationPending';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalFees.CausationPending; Billing.ServiceOrderDetail; Billing.ServiceOrder; Contract.HealthAdministrator; Contract.CareGroup; Payroll.FunctionalUnit; Contract.CUPSEntity; MedicalFees.HealthProfessionalContract; MedicalFees.MedicalFeesContract; INPROFSAL; Common.Supplier', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationPending';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationPending';
GO
