
CREATE VIEW [Billing].[ViewListSurgicalAndPackage]
as
(

select  Data.*
from (
select 
serviceOrderDetail.Id as ServiceOrderDetailId,
serviceOrderDetailSurgical.RateManualSalePrice,
serviceOrderDetail.InvoicedQuantity,
ipsService .Id as IPSServiceId,
CONCAT(ipsService .Code,' - ',ipsService .Name) as IPSServiceDescription,
CONCAT(ServiceOrderDetail.Id,' - ',ipsServiceSOD .Code,' - ',ipsServiceSOD .Name) as IPSServiceDescriptionSOD,
ipsServiceSOD.Id as IPSServiceSODId,
CONCAT(RTRIM(serviceOrderDetailSurgical.PerformsHealthProfessionalCode),' - ',thirdParty .Name) as ThirdPartyDescription,
invoiceDetail .InvoiceId as InvoiceId,
cupsEntity.Id as CupsEntityId,
CONCAT(cupsEntity.Code,' - ',cupsEntity.Description) as CupsEntityDescription,
billingGroup.Id as BillingGroupId,
CONCAT(billingGroup .Code,' - ',billingGroup.Name) as BillingGroupDescription,
serviceOrder .Status,
SelectOption=0,
serviceOrderDetail .CareGroupId,
serviceOrderDetail .RateManualId,
serviceOrderDetail.RateManualType,
serviceOrderDetailSurgical.TotalSalesPrice as GrandTotalSalesPrice,
case when serviceOrderDetailSurgical.TotalSalesPrice = 0 then serviceOrderDetailSurgical.RateManualSalePrice else serviceOrderDetailSurgical.TotalSalesPrice end as TotalSalesPrice,
serviceOrderDetailSurgical.RateManualSalePrice as SubTotalSalesPrice,
ThirdPartyDiscountPercentage = 0,
serviceOrderDetailSurgical.CostValue,
serviceOrderDetailSurgical.RateManualDetailSurgicalId,
serviceOrderDetailSurgical.BillingConceptId,
serviceOrderDetailSurgical.CostCenterId,
serviceOrderDetailSurgical.OnlyMedicalFees,
serviceOrderDetail .Presentation,
serviceOrderDetailSurgical.PerformsHealthProfessionalCode,
serviceOrderDetailSurgical .PerformsHealthProfessionalThirdPartyId as ThirdPartyId,
serviceOrderDetail.ServiceDate,
serviceOrderDetail.LiquidationType,
serviceOrderDetail.PerformsProfessionalSpecialty,
MedicalFeesContractId = 0,
serviceOrder .Id as ServiceOrderId,
serviceOrderDetailSurgical.Id as ServiceOrderDetailSurgicalId,
medicalFeesCausation.AmountPayable,
medicalFeesCausation.TotalAmountPayable as TotalAmountPayable,
medicalFeesCausation.Id as MedicalFeesCausationId,
medicalFeesCausation.CreationUser,
medicalFeesCausation.CreationDate,
medicalFeesCausation.ModificationUser,
medicalFeesCausation.ModificationDate,
medicalFeesCausation.ConfirmationUser,
medicalFeesCausation.ConfirmationDate,
medicalFeesCausation.[Status] as StatusMedicalFeesCausation,
medicalFeesCausation.TotalAmountPayableReal,
medicalFeesCausation.PercentageCashed,
invoiceDetail.Id as InvoiceDetailId,
CONCAT(medicalFeesContract .Code,' - ',medicalFeesContract .ContractName) as MedicalFeesContractCodeName,
serviceOrderDetailSurgical.IncomeMainAccountId,
CONCAT(functionalUnit.Code,' - ',functionalUnit.Name) as FunctionalUnitDescription,
serviceOrder.CreationUser as CreationUserServiceOrder,
case when medicalFeesCausation.[Status] = 2 then -256 when medicalFeesCausation.[Status] = 3 then -7155632 when  medicalFeesCausation.[Status] = 1 then -1048576 else -1 end as Color,
medicalFeesCausation.CausationDate,
serviceOrderDetail.IncludeServiceOrderDetailId,
invoice.InvoiceNumber,
invoice.PatientCode,
serviceOrder.AdmissionNumber
from 
Billing .ServiceOrderDetailSurgical serviceOrderDetailSurgical with (nolock)
inner join [Contract].IPSService ipsService with (nolock) on serviceOrderDetailSurgical.IPSServiceId = ipsService .Id
inner join [Common].ThirdParty thirdParty with (nolock) on serviceOrderDetailSurgical.PerformsHealthProfessionalThirdPartyId = thirdParty .Id
inner join Billing .ServiceOrderDetail serviceOrderDetail with (nolock) on serviceOrderDetailSurgical .ServiceOrderDetailId = serviceOrderDetail .Id
inner join [Payroll].FunctionalUnit functionalUnit with (nolock) on serviceOrderDetail.PerformsFunctionalUnitId = functionalUnit.Id
inner join [Contract].IPSService ipsServiceSOD with (nolock) on serviceOrderDetail.IPSServiceId = ipsServiceSOD .Id
inner join Billing .ServiceOrder serviceOrder with (nolock) on serviceOrderDetail .ServiceOrderId = serviceOrder .Id
inner join Billing .InvoiceDetail invoiceDetail with (nolock) on serviceOrderDetail .Id = invoiceDetail .ServiceOrderDetailId
inner join Billing .Invoice invoice with (nolock) on invoiceDetail .InvoiceId = invoice .Id
inner join [Contract] .CUPSEntity cupsEntity  with (nolock) on serviceOrderDetail .CUPSEntityId = cupsEntity .Id
inner join Billing .BillingGroup billingGroup with (nolock) on cupsEntity .BillingGroupId = billingGroup .Id
left join MedicalFees .MedicalFeesCausation medicalFeesCausation with (nolock) on invoiceDetail.Id = medicalFeesCausation.InvoiceDetailId and medicalFeesCausation.ServiceOrderDetailSurgicalId = serviceOrderDetailSurgical.Id
left join MedicalFees .MedicalFeesContract medicalFeesContract with (nolock) on medicalFeesCausation.MedicalFeesContractId = medicalFeesContract.Id
where serviceOrderDetail.IsDelete = 0 and ((serviceOrderDetail .Presentation = 2 And serviceOrderDetail .RecordType = 1 And invoice .Status = 1) or (serviceOrderDetail.IsPackage = 0 and serviceOrderDetail.Presentation = 3))

union all

select 
SODD.Id as ServiceOrderDetailId,
SODD.RateManualSalePrice,
SODD.InvoicedQuantity,
ipsService.Id as IPSServiceId,
CONCAT(ipsService .Code,' - ',ipsService .Name) as IPSServiceDescription,
CONCAT(SOD.Id,' - ',ipsServiceSOD .Code,' - ',ipsServiceSOD .Name) as IPSServiceDescriptionSOD,
ipsServiceSOD.Id as IPSServiceSODId,
CONCAT(RTRIM(SODD.PerformsHealthProfessionalCode),' - ',thirdParty .Name) as ThirdPartyDescription,
invoiceDetail .InvoiceId as InvoiceId,
cupsEntity.Id as CupsEntityId,
CONCAT(cupsEntity.Code,' - ',cupsEntity.Description) as CupsEntityDescription,
billingGroup.Id as BillingGroupId,
CONCAT(billingGroup .Code,' - ',billingGroup.Name) as BillingGroupDescription,
serviceOrder .Status,
SelectOption=0,
SODD.CareGroupId,
SODD.RateManualId,
SODD.RateManualType,
SODD.GrandTotalSalesPrice,
case when SODD.TotalSalesPrice = 0 then SODD.RateManualSalePrice else SODD.TotalSalesPrice end as TotalSalesPrice,
SOD.SubTotalSalesPrice,
SOD.ThirdPartyDiscountPercentage,
SODD.CostValue,
RateManualDetailSurgicalId=0,
SODD.BillingConceptId,
SODD.CostCenterId,
OnlyMedicalFees=0,
SODD.Presentation,
SODD.PerformsHealthProfessionalCode,
SODD.PerformsHealthProfessionalThirdPartyId as ThirdPartyId,
SODD.ServiceDate,
SODD.LiquidationType,
SODD.PerformsProfessionalSpecialty,
MedicalFeesContractId = 0,
serviceOrder .Id as ServiceOrderId,
null as ServiceOrderDetailSurgicalId,
medicalFeesCausation.AmountPayable,
medicalFeesCausation.TotalAmountPayable as TotalAmountPayable,
medicalFeesCausation.Id as MedicalFeesCausationId,
medicalFeesCausation.CreationUser,
medicalFeesCausation.CreationDate,
medicalFeesCausation.ModificationUser,
medicalFeesCausation.ModificationDate,
medicalFeesCausation.ConfirmationUser,
medicalFeesCausation.ConfirmationDate,
medicalFeesCausation.[Status] as StatusMedicalFeesCausation,
medicalFeesCausation.TotalAmountPayableReal,
medicalFeesCausation.PercentageCashed,
invoiceDetail.Id as InvoiceDetailId,
CONCAT(medicalFeesContract .Code,' - ',medicalFeesContract .ContractName) as MedicalFeesContractCodeName,
SODD.IncomeMainAccountId,
CONCAT(functionalUnit.Code,' - ',functionalUnit.Name) as FunctionalUnitDescription,
serviceOrder.CreationUser as CreationUserServiceOrder,
case when medicalFeesCausation.[Status] = 2 then -256 when medicalFeesCausation.[Status] = 3 then -7155632 when  medicalFeesCausation.[Status] = 1 then -1048576 else -1 end as Color,
medicalFeesCausation.CausationDate,
SODD.IncludeServiceOrderDetailId,
invoice.InvoiceNumber,
invoice.PatientCode,
serviceOrder.AdmissionNumber
from Billing.ServiceOrderDetail SOD with (nolock)
inner join [Payroll].FunctionalUnit functionalUnit with (nolock) on SOD.PerformsFunctionalUnitId = functionalUnit.Id
inner join Billing.ServiceOrderDetail SODD with (nolock)on SOD.Id = SODD.PackageServiceOrderDetailId
inner join [Contract].IPSService ipsService with (nolock)on SODD.IPSServiceId = ipsService.Id
inner join [Contract].IPSService ipsServiceSOD with (nolock)on SOD.IPSServiceId = ipsServiceSOD.Id
inner join [Common].ThirdParty thirdParty with (nolock)on SODD.PerformsHealthProfessionalThirdPartyId = thirdParty.Id
inner join Billing .InvoiceDetail invoiceDetail with (nolock)on SOD .Id = invoiceDetail .ServiceOrderDetailId
inner join Billing .Invoice invoice with (nolock) on invoiceDetail .InvoiceId = invoice .Id
inner join [Contract] .CUPSEntity cupsEntity with (nolock)on SODD .CUPSEntityId = cupsEntity .Id
inner join Billing .BillingGroup billingGroup with (nolock)on cupsEntity .BillingGroupId = billingGroup .Id
inner join Billing .ServiceOrder serviceOrder with (nolock)on SOD .ServiceOrderId = serviceOrder .Id
left join MedicalFees .MedicalFeesCausation medicalFeesCausation with (nolock)on invoiceDetail.Id = medicalFeesCausation.InvoiceDetailId and SODD.Id = medicalFeesCausation.ServiceOrderDetailId
left join MedicalFees .MedicalFeesContract medicalFeesContract with (nolock)on medicalFeesCausation.MedicalFeesContractId = medicalFeesContract.Id
where SOD.IsPackage = 1 and SODD.Presentation = 1 and SOD.IsDelete = 0

union all

select
SODD.Id as ServiceOrderDetailId,
SODS.RateManualSalePrice,
SODS.InvoicedQuantity,
ipsService.Id as IPSServiceId,
CONCAT(ips.Code,' - ',ipsService .Code,' - ',ipsService .Name) as IPSServiceDescription,
CONCAT(SOD.Id,' - ',ipsServiceSOD .Code,' - ',ipsServiceSOD .Name) as IPSServiceDescriptionSOD,
ipsServiceSOD.Id as IPSServiceSODId,
CONCAT(RTRIM(SODS.PerformsHealthProfessionalCode),' - ',thirdParty .Name) as ThirdPartyDescription,
invoiceDetail .InvoiceId as InvoiceId,
cupsEntity.Id as CupsEntityId,
CONCAT(cupsEntity.Code,' - ',cupsEntity.Description) as CupsEntityDescription,
billingGroup.Id as BillingGroupId,
CONCAT(billingGroup .Code,' - ',billingGroup.Name) as BillingGroupDescription,
serviceOrder .Status,
SelectOption=0,
SODD.CareGroupId,
SODD.RateManualId,
SODD.RateManualType,
SODS.TotalSalesPrice as GrandTotalSalesPrice,
case when SODS.TotalSalesPrice = 0 then SODS.RateManualSalePrice else SODS.TotalSalesPrice end as TotalSalesPrice,
SODS.RateManualSalePrice as SubTotalSalesPrice,
ThirdPartyDiscountPercentage = 0,
SODS.CostValue,
SODS.RateManualDetailSurgicalId,
SODS.BillingConceptId,
SODS.CostCenterId,
SODS.OnlyMedicalFees,
SOD.Presentation,
SODS.PerformsHealthProfessionalCode,
SODS.PerformsHealthProfessionalThirdPartyId as ThirdPartyId,
SODD.ServiceDate,
SODD.LiquidationType,
SODD.PerformsProfessionalSpecialty,
MedicalFeesContractId = 0,
serviceOrder .Id as ServiceOrderId,
SODS.Id as ServiceOrderDetailSurgicalId,
medicalFeesCausation.AmountPayable,
medicalFeesCausation.TotalAmountPayable as TotalAmountPayable,
medicalFeesCausation.Id as MedicalFeesCausationId,
medicalFeesCausation.CreationUser,
medicalFeesCausation.CreationDate,
medicalFeesCausation.ModificationUser,
medicalFeesCausation.ModificationDate,
medicalFeesCausation.ConfirmationUser,
medicalFeesCausation.ConfirmationDate,
medicalFeesCausation.[Status] as StatusMedicalFeesCausation,
medicalFeesCausation.TotalAmountPayableReal,
medicalFeesCausation.PercentageCashed,
invoiceDetail.Id as InvoiceDetailId,
CONCAT(medicalFeesContract .Code,' - ',medicalFeesContract .ContractName) as MedicalFeesContractCodeName,
SODS.IncomeMainAccountId,
CONCAT(functionalUnit.Code,' - ',functionalUnit.Name) as FunctionalUnitDescription,
serviceOrder.CreationUser as CreationUserServiceOrder,
case when medicalFeesCausation.[Status] = 2 then -256 when medicalFeesCausation.[Status] = 3 then -7155632 when  medicalFeesCausation.[Status] = 1 then -1048576 else -1 end as Color,
medicalFeesCausation.CausationDate,
SODD.IncludeServiceOrderDetailId,
invoice.InvoiceNumber,
invoice.PatientCode,
serviceOrder.AdmissionNumber
from Billing.ServiceOrderDetail SOD with (nolock)
inner join [Payroll].FunctionalUnit functionalUnit with (nolock)on SOD.PerformsFunctionalUnitId = functionalUnit.Id
inner join Billing.ServiceOrderDetail SODD with (nolock)on SOD.Id = SODD.PackageServiceOrderDetailId
inner join Billing.ServiceOrderDetailSurgical SODS with (nolock)on SODS.ServiceOrderDetailId = SODD.Id
inner join [Contract].IPSService ipsService with (nolock) on SODS.IPSServiceId = ipsService.Id
inner join [Contract].IPSService ipsServiceSOD with (nolock) on SOD.IPSServiceId = ipsServiceSOD.Id
inner join [Contract].IPSService ips with (nolock) on SODD.IPSServiceId = ips .Id
inner join [Common].ThirdParty thirdParty with (nolock) on SODS.PerformsHealthProfessionalThirdPartyId = thirdParty.Id
inner join Billing .InvoiceDetail invoiceDetail with (nolock) on SOD .Id = invoiceDetail .ServiceOrderDetailId
inner join Billing .Invoice invoice with (nolock) on invoiceDetail .InvoiceId = invoice .Id
inner join [Contract] .CUPSEntity cupsEntity with (nolock) on SODD .CUPSEntityId = cupsEntity .Id
inner join Billing .BillingGroup billingGroup with (nolock) on cupsEntity .BillingGroupId = billingGroup .Id
inner join Billing .ServiceOrder serviceOrder with (nolock) on SOD .ServiceOrderId = serviceOrder .Id
left join MedicalFees .MedicalFeesCausation medicalFeesCausation with (nolock) on invoiceDetail.Id = medicalFeesCausation.InvoiceDetailId and SODS.Id = medicalFeesCausation.ServiceOrderDetailSurgicalId
left join MedicalFees .MedicalFeesContract medicalFeesContract with (nolock) on medicalFeesCausation.MedicalFeesContractId = medicalFeesContract.Id
where SOD.IsPackage = 1 and SODD.Presentation = 2 and SOD.IsDelete = 0

union all

--Consulta para traer los paquetes

select 
SOD.Id as ServiceOrderDetailId,
SOD.RateManualSalePrice,
SOD.InvoicedQuantity,
ipsService.Id as IPSServiceId,
CONCAT(ipsService .Code,' - ',ipsService .Name) as IPSServiceDescription,
CONCAT(SOD.Id,' - ',ipsService .Code,' - ',ipsService .Name) as IPSServiceDescriptionSOD,
ipsService.Id as IPSServiceSODId,
CONCAT(RTRIM(SOD.PerformsHealthProfessionalCode),' - ',thirdParty .Name) as ThirdPartyDescription,
invoiceDetail .InvoiceId as InvoiceId,
cupsEntity.Id as CupsEntityId,
CONCAT(cupsEntity.Code,' - ',cupsEntity.Description) as CupsEntityDescription,
billingGroup.Id as BillingGroupId,
CONCAT(billingGroup .Code,' - ',billingGroup.Name) as BillingGroupDescription,
serviceOrder .Status,
SelectOption=0,
SOD.CareGroupId,
SOD.RateManualId,
SOD.RateManualType,
SOD.GrandTotalSalesPrice,
case when SOD.TotalSalesPrice = 0 then SOD.RateManualSalePrice else SOD.TotalSalesPrice end as TotalSalesPrice,
SOD.SubTotalSalesPrice,
SOD.ThirdPartyDiscountPercentage,
SOD.CostValue,
RateManualDetailSurgicalId=0,
SOD.BillingConceptId,
SOD.CostCenterId,
OnlyMedicalFees=0,
SOD.Presentation,
SOD.PerformsHealthProfessionalCode,
SOD.PerformsHealthProfessionalThirdPartyId as ThirdPartyId,
SOD.ServiceDate,
SOD.LiquidationType,
SOD.PerformsProfessionalSpecialty,
MedicalFeesContractId = 0,
serviceOrder .Id as ServiceOrderId,
null as ServiceOrderDetailSurgicalId,
medicalFeesCausation.AmountPayable,
medicalFeesCausation.TotalAmountPayable as TotalAmountPayable,
medicalFeesCausation.Id as MedicalFeesCausationId,
medicalFeesCausation.CreationUser,
medicalFeesCausation.CreationDate,
medicalFeesCausation.ModificationUser,
medicalFeesCausation.ModificationDate,
medicalFeesCausation.ConfirmationUser,
medicalFeesCausation.ConfirmationDate,
medicalFeesCausation.[Status] as StatusMedicalFeesCausation,
medicalFeesCausation.TotalAmountPayableReal,
medicalFeesCausation.PercentageCashed,
invoiceDetail.Id as InvoiceDetailId,
CONCAT(medicalFeesContract .Code,' - ',medicalFeesContract .ContractName) as MedicalFeesContractCodeName,
SOD.IncomeMainAccountId,
CONCAT(functionalUnit.Code,' - ',functionalUnit.Name) as FunctionalUnitDescription,
serviceOrder.CreationUser as CreationUserServiceOrder,
case when medicalFeesCausation.[Status] = 2 then -256 when medicalFeesCausation.[Status] = 3 then -7155632 when  medicalFeesCausation.[Status] = 1 then -1048576 else -1 end as Color,
medicalFeesCausation.CausationDate,
SOD.IncludeServiceOrderDetailId,
invoice.InvoiceNumber,
invoice.PatientCode,
serviceOrder.AdmissionNumber
from Billing.ServiceOrderDetail SOD with (nolock)
inner join [Payroll].FunctionalUnit functionalUnit with (nolock) on SOD.PerformsFunctionalUnitId = functionalUnit.Id
inner join [Contract].IPSService ipsService with (nolock) on SOD.IPSServiceId = ipsService.Id
inner join [Common].ThirdParty thirdParty with (nolock) on SOD.PerformsHealthProfessionalThirdPartyId = thirdParty.Id
inner join Billing .InvoiceDetail invoiceDetail with (nolock) on SOD .Id = invoiceDetail .ServiceOrderDetailId
inner join Billing .Invoice invoice with (nolock) on invoiceDetail .InvoiceId = invoice .Id
inner join [Contract] .CUPSEntity cupsEntity with (nolock) on SOD .CUPSEntityId = cupsEntity .Id
inner join Billing .BillingGroup billingGroup with (nolock) on cupsEntity .BillingGroupId = billingGroup .Id
inner join Billing .ServiceOrder serviceOrder with (nolock) on SOD .ServiceOrderId = serviceOrder .Id
left join MedicalFees .MedicalFeesCausation medicalFeesCausation with (nolock) on invoiceDetail.Id = medicalFeesCausation.InvoiceDetailId and SOD.Id = medicalFeesCausation.ServiceOrderDetailId and SOD.PerformsHealthProfessionalCode = medicalFeesCausation.HealthProfessionalCode
left join MedicalFees .MedicalFeesContract medicalFeesContract with (nolock) on medicalFeesCausation.MedicalFeesContractId = medicalFeesContract.Id
where SOD.IsPackage = 0 and SOD.Presentation = 3 and SOD.IsDelete = 0

union all

--Paquete incluido en otro paquete

select 
SODD.Id as ServiceOrderDetailId,
SODD.RateManualSalePrice,
SODD.InvoicedQuantity,
ipsService.Id as IPSServiceId,
CONCAT(ipsService .Code,' - ',ipsService .Name) as IPSServiceDescription,
CONCAT(SOD.Id,' - ',ipsServiceSOD .Code,' - ',ipsServiceSOD .Name) as IPSServiceDescriptionSOD,
ipsServiceSOD.Id as IPSServiceSODId,
CONCAT(RTRIM(SODD.PerformsHealthProfessionalCode),' - ',thirdParty .Name) as ThirdPartyDescription,
invoiceDetail .InvoiceId as InvoiceId,
cupsEntity.Id as CupsEntityId,
CONCAT(cupsEntity.Code,' - ',cupsEntity.Description) as CupsEntityDescription,
billingGroup.Id as BillingGroupId,
CONCAT(billingGroup .Code,' - ',billingGroup.Name) as BillingGroupDescription,
serviceOrder .Status,
SelectOption=0,
SODD.CareGroupId,
SODD.RateManualId,
SODD.RateManualType,
SODD.GrandTotalSalesPrice,
case when SODD.TotalSalesPrice = 0 then SODD.RateManualSalePrice else SODD.TotalSalesPrice end as TotalSalesPrice,
SOD.SubTotalSalesPrice,
SOD.ThirdPartyDiscountPercentage,
SODD.CostValue,
RateManualDetailSurgicalId=0,
SODD.BillingConceptId,
SODD.CostCenterId,
OnlyMedicalFees=0,
SODD.Presentation,
SODD.PerformsHealthProfessionalCode,
SODD.PerformsHealthProfessionalThirdPartyId as ThirdPartyId,
SODD.ServiceDate,
SODD.LiquidationType,
SODD.PerformsProfessionalSpecialty,
MedicalFeesContractId = 0,
serviceOrder .Id as ServiceOrderId,
null as ServiceOrderDetailSurgicalId,
medicalFeesCausation.AmountPayable,
medicalFeesCausation.TotalAmountPayable as TotalAmountPayable,
medicalFeesCausation.Id as MedicalFeesCausationId,
medicalFeesCausation.CreationUser,
medicalFeesCausation.CreationDate,
medicalFeesCausation.ModificationUser,
medicalFeesCausation.ModificationDate,
medicalFeesCausation.ConfirmationUser,
medicalFeesCausation.ConfirmationDate,
medicalFeesCausation.[Status] as StatusMedicalFeesCausation,
medicalFeesCausation.TotalAmountPayableReal,
medicalFeesCausation.PercentageCashed,
invoiceDetail.Id as InvoiceDetailId,
CONCAT(medicalFeesContract .Code,' - ',medicalFeesContract .ContractName) as MedicalFeesContractCodeName,
SODD.IncomeMainAccountId,
CONCAT(functionalUnit.Code,' - ',functionalUnit.Name) as FunctionalUnitDescription,
serviceOrder.CreationUser as CreationUserServiceOrder,
case when medicalFeesCausation.[Status] = 2 then -256 when medicalFeesCausation.[Status] = 3 then -7155632 when  medicalFeesCausation.[Status] = 1 then -1048576 else -1 end as Color,
medicalFeesCausation.CausationDate,
SODD.IncludeServiceOrderDetailId,
invoice.InvoiceNumber,
invoice.PatientCode,
serviceOrder.AdmissionNumber
from Billing.ServiceOrderDetail SOD with (nolock)
inner join [Payroll].FunctionalUnit functionalUnit with (nolock) on SOD.PerformsFunctionalUnitId = functionalUnit.Id
inner join Billing.ServiceOrderDetail SODD with (nolock)on SOD.Id = SODD.PackageServiceOrderDetailId
inner join [Contract].IPSService ipsService with (nolock)on SODD.IPSServiceId = ipsService.Id
inner join [Contract].IPSService ipsServiceSOD with (nolock)on SOD.IPSServiceId = ipsServiceSOD.Id
inner join [Common].ThirdParty thirdParty with (nolock)on SODD.PerformsHealthProfessionalThirdPartyId = thirdParty.Id
inner join Billing .InvoiceDetail invoiceDetail with (nolock)on SOD .Id = invoiceDetail .ServiceOrderDetailId
inner join Billing .Invoice invoice with (nolock) on invoiceDetail .InvoiceId = invoice .Id
inner join [Contract] .CUPSEntity cupsEntity with (nolock)on SODD .CUPSEntityId = cupsEntity .Id
inner join Billing .BillingGroup billingGroup with (nolock)on cupsEntity .BillingGroupId = billingGroup .Id
inner join Billing .ServiceOrder serviceOrder with (nolock)on SOD .ServiceOrderId = serviceOrder .Id
left join MedicalFees .MedicalFeesCausation medicalFeesCausation with (nolock)on invoiceDetail.Id = medicalFeesCausation.InvoiceDetailId and SODD.Id = medicalFeesCausation.ServiceOrderDetailId
left join MedicalFees .MedicalFeesContract medicalFeesContract with (nolock)on medicalFeesCausation.MedicalFeesContractId = medicalFeesContract.Id
where SOD.IsPackage = 1 and SODD.Presentation = 3 and SOD.IsDelete = 0
	AND SODD.IsPackage = 0 and SODD.Presentation = 3 and SODD.IsDelete = 0

) as Data
)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los servicios quirúrgicos y paquetes facturados, integrando el detalle quirúrgico de cada orden de servicio con su factura, el profesional que realizó el procedimiento, la unidad funcional ejecutante, el código CUPS, el grupo de facturación y los honorarios médicos causados. Combina información de órdenes de servicio, facturas emitidas a pacientes o terceros pagadores (EPS, aseguradoras), contratos de honorarios médicos y el catálogo de servicios de la IPS para ofrecer una vista unificada de cirugías y paquetes quirúrgicos facturados. Se utiliza principalmente en reportería de facturación quirúrgica, liquidación de honorarios médicos y auditoría de cuentas, permitiendo identificar el valor de venta, costo, monto a pagar al médico, estado de causación de honorarios y trazabilidad de cada procedimiento quirúrgico desde la orden hasta la factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListSurgicalAndPackage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListSurgicalAndPackage';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un solo listado los ítems facturables de tipo quirúrgico y de tipo paquete (incluyendo paquetes anidados y sus componentes), enriquecidos con datos de factura, profesional, tarifa, unidad funcional y causación de honorarios médicos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListSurgicalAndPackage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas Billing.ServiceOrderDetail, Billing.ServiceOrder, Billing.Invoice, Billing.InvoiceDetail, Contract.IPSService, Contract.CUPSEntity, Billing.BillingGroup, Common.ThirdParty y Payroll.FunctionalUnit deben tener datos íntegros y referenciables (los joins son INNER).; Los detalles deben estar ligados a un detalle de factura (InvoiceDetail) para aparecer.; Los detalles deben tener IsDelete = 0.; Para la rama de detalles quirúrgicos no-paquete con Presentation=2, la factura asociada debe tener Status = 1.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListSurgicalAndPackage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen detalles de orden de servicio no eliminados (IsDelete = 0) en todas las ramas.; El JOIN con InvoiceDetail es INNER, por lo que únicamente se exponen detalles que ya estén ligados a una factura.; La causación de honorarios médicos (MedicalFeesCausation) se trae mediante LEFT JOIN: un ítem puede no tener causación.; El color asociado a la fila depende exclusivamente del estado (Status) de la causación de honorarios médicos: 1=-1048576, 2=-256, 3=-7155632, otro/null=-1.; SelectOption se inicializa siempre en 0 (la vista no marca selección por defecto).; MedicalFeesContractId y, en algunas ramas, RateManualDetailSurgicalId, OnlyMedicalFees y ThirdPartyDiscountPercentage se devuelven con valor fijo 0 cuando no aplica al tipo de fila.; Para la rama 1 (quirúrgicos individuales) la factura debe estar en Status = 1 cuando Presentation = 2.; Cuando un detalle es paquete (IsPackage = 1), la vista descompone los ítems hijos a través de PackageServiceOrderDetailId.; El emparejamiento con MedicalFeesCausation se realiza diferenciando entre detalle quirúrgico (ServiceOrderDetailSurgicalId) y detalle no quirúrgico (ServiceOrderDetailId), según la rama.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListSurgicalAndPackage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de servicio; Detalle de orden de servicio; Servicio quirúrgico; Paquete de servicios; Factura; Detalle de factura; Causación de honorarios médicos; Contrato de honorarios médicos; Profesional de la salud; Servicio IPS; CUPS; Grupo de facturación; Unidad funcional; Centro de costo; Tarifario (RateManual); Especialidad profesional; Admisión del paciente', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListSurgicalAndPackage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si serviceOrderDetail.Presentation = 2 AND RecordType = 1 AND invoice.Status = 1 (rama 1) → Devuelve detalles quirúrgicos asociados a un ServiceOrderDetailSurgical (servicios quirúrgicos individuales facturados); si serviceOrderDetail.IsPackage = 0 AND Presentation = 3 (rama 1, alterna) → Devuelve detalles que no son paquete y tienen presentación 3, también desde la tabla quirúrgica; si SOD.IsPackage = 1 AND SODD.Presentation = 1 (rama 2) → Devuelve los ítems contenidos dentro de un paquete con presentación tipo 1 (servicios no quirúrgicos del paquete); si SOD.IsPackage = 1 AND SODD.Presentation = 2 (rama 3) → Devuelve los ítems quirúrgicos contenidos dentro de un paquete, uniendo ServiceOrderDetailSurgical del detalle hijo; si SOD.IsPackage = 0 AND SOD.Presentation = 3 (rama 4) → Devuelve los paquetes mismos (cabecera) cuando son presentación 3 y no están marcados como paquete contenedor; si SOD.IsPackage = 1 AND SODD.Presentation = 3 AND SODD.IsPackage = 0 AND SODD.Presentation = 3 (rama 5) → Devuelve paquetes incluidos dentro de otro paquete (anidamiento de paquetes con presentación 3); si MedicalFeesCausation.Status = 2 → Color = -256 (amarillo); si MedicalFeesCausation.Status = 3 → Color = -7155632; si MedicalFeesCausation.Status = 1 → Color = -1048576 else Color = -1 cuando no hay causación o estado distinto; si TotalSalesPrice = 0 → TotalSalesPrice efectivo = RateManualSalePrice else TotalSalesPrice efectivo = TotalSalesPrice original', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListSurgicalAndPackage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetailSurgical; Billing.ServiceOrderDetail; Billing.ServiceOrder; Billing.InvoiceDetail; Billing.Invoice; Billing.BillingGroup; Contract.IPSService; Contract.CUPSEntity; Common.ThirdParty; Payroll.FunctionalUnit; MedicalFees.MedicalFeesCausation; MedicalFees.MedicalFeesContract', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListSurgicalAndPackage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListSurgicalAndPackage';
GO
