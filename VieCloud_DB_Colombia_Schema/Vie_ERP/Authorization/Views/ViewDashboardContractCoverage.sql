

CREATE VIEW [Authorization].[ViewDashboardContractCoverage]
AS

	
	select CONCAT(sod.Id, '') Id, sod.Id ServiceOrderDetailId, so.Code ServiceOrderCode, so.CreationDate ServiceOrderDate, sod.ServiceDate, so.AdmissionNumber, 
	so.Status ServiceOrderStatus, case so.Status when 1 then 'Registrada' when 2 then 'Facturada' else 'Anulada' end ServiceOrderStatusName,
	ing.CODDIAING DiagnosticCode, ing.CODCENATE CareCenterCode,
	so.PatientCode, pa.IPNOMCOMP PatientName, rtrim(ltrim(so.PatientCode)) + ' - ' + rtrim(ltrim(pa.IPNOMCOMP)) PatientCodeName,
	ce.Code ServiceCode, ce.Description ServiceName, ce.Code + ' - ' + ce.Description ServiceCodeName, 1 ServiceType,
	cd.Id ContractDescriptionId, cd.Code ContractDescriptionCode, cd.Name ContractDescriptionName, cd.Code + ' - ' + cd.Name ContractDescriptionCodeName,
	sod.InvoicedQuantity,
	fu.Id FunctionalUnitId, fu.Code FunctionalUnitCode, fu.Name FunctionalUnitName, fu.Code + ' - ' + fu.Name FunctionalUnitCodeName,
	sod.PerformsHealthProfessionalCode ProfessionalCode,
	cg.Id CareGroupId, cg.Code CareGroupCode, cg.Name CareGroupName, cg.Code + ' - ' + cg.Name CareGroupCodeName,
	ha.Id HealthAdministratorId, ha.Code HealthAdministratorCode, ha.Name HealthAdministratorName, ha.Code + ' - ' + ha.Name HealthAdministratorCodeName,
	pc.Contracted, pc.Quoted
	from Billing.ServiceOrderDetail sod
	inner join Billing.ServiceOrder so on so.Id = sod.ServiceOrderId
	inner join .ADINGRESO ing on ing.NUMINGRES = so.AdmissionNumber
	inner join Contract.CUPSEntity ce on ce.Id = sod.CUPSEntityId
	inner join Contract.CareGroup cg on cg.Id = sod.CareGroupId
	inner join Contract.ProcedureCups pc on pc.ProceduresTemplateId = cg.ProcedureTemplateId and pc.CupsId = ce.Id and ISNULL(pc.CUPSEntityContractDescriptionId, 0) = ISNULL(sod.CUPSEntityContractDescriptionId, 0)
	inner join .INPACIENT pa on pa.IPCODPACI = so.PatientCode
	inner join Payroll.FunctionalUnit fu on fu.Id = sod.PerformsFunctionalUnitId
	inner join Contract.HealthAdministrator ha on ha.Id = sod.HealthAdministratorId
	left join Contract.CUPSEntityContractDescriptions cecd on cecd.Id = sod.CUPSEntityContractDescriptionId
	left join Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	where pc.Contracted = 0 and sod.RecordType = 1 and ((ing.TIPOINGRE = 2 and ing.TRATAESPECIA is null) or ing.TRATAESPECIA = 3) and sod.ContractCoverageStatus is null and sod.ContractCoverageObservations is null

	union all

	select CONCAT(sod.Id, '') Id, sod.Id ServiceOrderDetailId, so.Code ServiceOrderCode, so.CreationDate ServiceOrderDate, sod.ServiceDate, so.AdmissionNumber, 
	so.Status ServiceOrderStatus, case so.Status when 1 then 'Registrada' when 2 then 'Facturada' else 'Anulada' end ServiceOrderStatusName,
	ing.CODDIAING DiagnosticCode, ing.CODCENATE CareCenterCode,
	so.PatientCode, pa.IPNOMCOMP PatientName, rtrim(ltrim(so.PatientCode)) + ' - ' + rtrim(ltrim(pa.IPNOMCOMP)) PatientCodeName,
	pro.Code ServiceCode, pro.Description ServiceName, pro.Code + ' - ' + pro.Description ServiceCodeName, 2 ServiceType,
	cd.Id ContractDescriptionId, cd.Code ContractDescriptionCode, cd.Name ContractDescriptionName, cd.Code + ' - ' + cd.Name ContractDescriptionCodeName,
	sod.InvoicedQuantity,
	fu.Id FunctionalUnitId, fu.Code FunctionalUnitCode, fu.Name FunctionalUnitName, fu.Code + ' - ' + fu.Name FunctionalUnitCodeName,
	sod.PerformsHealthProfessionalCode ProfessionalCode,
	cg.Id CareGroupId, cg.Code CareGroupCode, cg.Name CareGroupName, cg.Code + ' - ' + cg.Name CareGroupCodeName,
	ha.Id HealthAdministratorId, ha.Code HealthAdministratorCode, ha.Name HealthAdministratorName, ha.Code + ' - ' + ha.Name HealthAdministratorCodeName,
	prd.Contracted, prd.Quoted
	from Billing.ServiceOrderDetail sod
	inner join Billing.ServiceOrder so on so.Id = sod.ServiceOrderId
	inner join .ADINGRESO ing on ing.NUMINGRES = so.AdmissionNumber
	inner join Inventory.InventoryProduct pro on pro.Id = sod.ProductId
	inner join Contract.CareGroup cg on cg.Id = sod.CareGroupId
	inner join Inventory.ProductRateDetail prd on prd.ProductRateId = cg.ProductRateId and prd.ProductId = pro.Id and sod.ServiceDate >= prd.InitialDate and sod.ServiceDate <= prd.EndDate
	inner join .INPACIENT pa on pa.IPCODPACI = so.PatientCode
	inner join Payroll.FunctionalUnit fu on fu.Id = sod.PerformsFunctionalUnitId
	inner join Contract.HealthAdministrator ha on ha.Id = sod.HealthAdministratorId
	left join Contract.CUPSEntityContractDescriptions cecd on cecd.Id = sod.CUPSEntityContractDescriptionId
	left join Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	where prd.Contracted = 0 and sod.RecordType = 2 and ((ing.TIPOINGRE = 2 and ing.TRATAESPECIA is null) or ing.TRATAESPECIA = 3) and sod.ContractCoverageStatus is null and sod.ContractCoverageObservations is null
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Panel de control (dashboard) de cobertura contractual: identifica servicios y productos facturados en órdenes de hospitalización o urgencias especializadas (tratamiento especial tipo 3) que NO están contratados con la entidad pagadora (EPS/aseguradora). Combina dos bloques: procedimientos CUPS y medicamentos/insumos de inventario, cruzando el detalle de facturación (ServiceOrderDetail) con el ingreso del paciente (ADINGRESO), el catálogo de servicios o productos, las reglas del grupo de atención del contrato (CareGroup con sus plantillas de CUPS o tarifas de productos), y los datos del paciente. Solo muestra ítems donde el indicador ''Contratado'' es falso y aún no tienen estado ni observación de cobertura contractual, es decir, glosas o alertas de cobertura pendientes de gestión. Sirve para que el área de autorización y contratación detecte y resuelva brechas entre lo facturado y lo pactado con cada pagador.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewDashboardContractCoverage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewDashboardContractCoverage';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los detalles de órdenes de servicio (procedimientos CUPS y productos) cuya cobertura contractual aún no ha sido evaluada y cuyo CUPS o tarifa no está contratado, restringiendo a ingresos ambulatorios o con tratamiento especial tipo 3, para alimentar un dashboard de cobertura.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewDashboardContractCoverage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de orden de servicio (Billing.ServiceOrder) y su detalle (Billing.ServiceOrderDetail); El ingreso (ADINGRESO) debe existir y referenciar el AdmissionNumber de la orden; Para RecordType=1: el CUPS debe estar parametrizado en Contract.ProcedureCups bajo la plantilla del CareGroup; Para RecordType=2: el producto debe tener tarifa (Inventory.ProductRateDetail) vigente para la fecha del servicio bajo la tarifa del CareGroup; El detalle debe tener asignados CareGroup, FunctionalUnit (PerformsFunctionalUnitId) y HealthAdministrator; El paciente (INPACIENT) debe existir para el PatientCode de la orden', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewDashboardContractCoverage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone ítems cuyo CUPS o producto NO está contratado (pc.Contracted = 0 o prd.Contracted = 0); Solo expone ítems sin estado ni observación de cobertura contractual previamente registrados (ContractCoverageStatus IS NULL y ContractCoverageObservations IS NULL); Solo considera ingresos ambulatorios (TIPOINGRE = 2) sin tratamiento especial, o ingresos cuyo tratamiento especial sea de tipo 3; El RecordType del detalle determina la naturaleza del ítem: 1 = servicio/CUPS, 2 = producto de inventario; Para productos, la tarifa debe estar vigente respecto a la fecha de servicio (ServiceDate entre InitialDate y EndDate de ProductRateDetail); El emparejamiento de ProcedureCups exige misma plantilla de procedimiento del CareGroup, mismo CUPS y misma descripción de contrato (con tratamiento de NULL como 0)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewDashboardContractCoverage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de servicio; Detalle de orden de servicio; Ingreso/admisión hospitalaria; Diagnóstico de ingreso; Centro de atención; Paciente; CUPS (procedimientos en salud); Grupo de atención; Contrato y descripción de contrato; Administradora de salud; Unidad funcional; Producto de inventario; Tarifa de producto; Cobertura contractual; Tratamiento especial', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewDashboardContractCoverage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ServiceOrderDetail: Devuelve detalles RecordType=1 cuando pc.Contracted=0, ContractCoverageStatus y ContractCoverageObservations son NULL, y el ingreso es ambulatorio sin tratamiento especial o con TRATAESPECIA=3; [RETURN_RESULT] Billing.ServiceOrderDetail: Devuelve detalles RecordType=2 cuando prd.Contracted=0, la tarifa del producto está vigente en la fecha de servicio, ContractCoverageStatus y ContractCoverageObservations son NULL, y el ingreso es ambulatorio sin tratamiento especial o con TRATAESPECIA=3', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewDashboardContractCoverage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si sod.RecordType = 1 (procedimiento/CUPS) → Une con Contract.CUPSEntity y Contract.ProcedureCups para validar cobertura contractual del CUPS y marca ServiceType=1; si sod.RecordType = 2 (producto/insumo) → Une con Inventory.InventoryProduct e Inventory.ProductRateDetail (vigente entre InitialDate y EndDate) para validar cobertura tarifaria del producto y marca ServiceType=2; si so.Status = 1 / 2 / otro → Etiqueta el estado de la orden como ''Registrada'' / ''Facturada'' / ''Anulada'' respectivamente', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewDashboardContractCoverage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetail; Billing.ServiceOrder; ADINGRESO; Contract.CUPSEntity; Contract.CareGroup; Contract.ProcedureCups; INPACIENT; Payroll.FunctionalUnit; Contract.HealthAdministrator; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Inventory.InventoryProduct; Inventory.ProductRateDetail', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewDashboardContractCoverage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewDashboardContractCoverage';
GO
