

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
	AND mfc.CausationRecognitionId IS NULL  -- Solo causaciones SIN reconocimiento
	AND mfc.MedicalFeesContractValue > 0 -- Solo causaciones con valor mayor a 0
	AND ISNULL(s2.Id, s.Id) IS NOT NULL

GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de causaciones de honorarios médicos que aún no tienen reconocimiento registrado, con valor mayor a cero y proveedor/médico identificado. Integra la causación con el contrato de honorarios (médico individual o agremiación), el paciente, el ingreso, el detalle de orden de servicio, la EPS o pagador, el grupo de atención, el procedimiento CUPS y la unidad funcional donde se realizó la prestación. Para cada causación muestra si el servicio ya fue facturado (con número de factura) o permanece sin facturar, permitiendo a las áreas de cartera y liquidación de honorarios identificar las causaciones pendientes de reconocimiento y gestionar su cobro o registro contable.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'VIEW', @level1name = N'ViewListCausationwithoutRecognition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'VIEW', @level1name = N'ViewListCausationwithoutRecognition';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las causaciones de honorarios médicos activas, con valor positivo, que aún no han sido reconocidas, mostrando datos del profesional, contrato, paciente, servicio y estado de facturación.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationwithoutRecognition';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La causación debe existir y estar relacionada con un profesional (INPROFSAL), un contrato de honorarios, un paciente, un ingreso, un detalle de orden de servicio, una orden, administradora, grupo de atención, unidad funcional y entidad CUPS.; Debe poder resolverse un proveedor (Supplier) válido, ya sea por el contrato (ContractType=2) o por el profesional (ContractType=1).', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationwithoutRecognition';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen causaciones activas (Status=1).; Nunca se incluyen causaciones que ya tengan reconocimiento (CausationRecognitionId IS NOT NULL).; Nunca se incluyen causaciones con valor de contrato menor o igual a cero.; Toda fila expuesta tiene un proveedor identificable (SupplierId no nulo).; El proveedor mostrado depende del tipo de contrato: profesional para ''Médico'', contrato para ''Agremiación''.; Solo se vinculan facturas en estado 1 y con control de ingresos (RevenueControlDetailId no nulo).', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationwithoutRecognition';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Causación de honorarios médicos; Reconocimiento de causación; Contrato de honorarios médicos; Tipo de contrato (Médico/Agremiación); Profesional de la salud; Proveedor; Paciente; Ingreso/Admisión; Orden de servicio; Administradora de salud; Grupo de atención; Unidad funcional; CUPS; Facturación; Factura; Control de ingresos', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationwithoutRecognition';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MedicalFees.ViewListCausationwithoutRecognition: Devuelve causaciones donde Status=1, CausationRecognitionId IS NULL, MedicalFeesContractValue>0 y existe un Supplier resoluble.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationwithoutRecognition';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si mc.ContractType = 1 → Tipo de contrato ''Médico''; el proveedor se toma desde el profesional (i.GENPROVEE) y se muestra s2.Name else Si ContractType = 2, tipo ''Agremiación''; el proveedor se toma del contrato (mc.SupplierId) y se muestra s.Name; si id.Id IS NOT NULL (existe InvoiceDetail asociado) → ServiceStatus = ''Facturado'' y se muestra InvoiceNumber else ServiceStatus = ''Sin Facturar'' e InvoiceNumber NULL; si inv.Status = 1 AND inv.RevenueControlDetailId IS NOT NULL → Se enlaza la factura para mostrar su número else No se asocia número de factura aunque exista InvoiceDetail', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationwithoutRecognition';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalFees.MedicalFeesCausation; MedicalFees.MedicalFeesContract; INPROFSAL; INPACIENT; ADINGRESO; Billing.ServiceOrderDetail; Billing.ServiceOrder; Contract.HealthAdministrator; Contract.CareGroup; Payroll.FunctionalUnit; Contract.CUPSEntity; Common.Supplier; Billing.InvoiceDetail; Billing.Invoice', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationwithoutRecognition';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListCausationwithoutRecognition';
GO
