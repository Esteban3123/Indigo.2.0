












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