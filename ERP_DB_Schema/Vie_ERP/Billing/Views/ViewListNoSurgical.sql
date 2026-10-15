


CREATE VIEW [Billing].[ViewListNoSurgical]
as
(

select  
		serviceOrderDetail.Id as Id,
		serviceOrderDetail.Id as ServiceOrderDetailId,
		serviceOrderDetail.RateManualSalePrice as RateManualSalePrice,
		serviceOrderDetail.InvoicedQuantity,
		ipsService .Id as IPSServiceId,
		CONCAT(ipsService .Code,' - ',ipsService .Name) as IPSServiceDescription,		
		CONCAT(RTRIM(serviceOrderDetail.PerformsHealthProfessionalCode),' - ',thirdParty .Name) as ThirdPartyDescription,
		invoiceDetail .InvoiceId as InvoiceId,
		cupsEntity.Id as CupsEntityId,
		CONCAT(cupsEntity.Code,' - ',cupsEntity.Description) as CupsEntityDescription,
		billingGroup.Id as BillingGroupId,
		CONCAT(billingGroup .Code,' - ',billingGroup.Name) as BillingGroupDescription,
		serviceOrder .Status,
		cast(0 as BIT)	SelectOption,
		serviceOrderDetail .CareGroupId,
		isnull(serviceOrderDetail .RateManualId,0) RateManualId,
		serviceOrderDetail .GrandTotalSalesPrice,
		serviceOrderDetail .Presentation,
		case when serviceOrderDetail.TotalSalesPrice > 0 then serviceOrderDetail.TotalSalesPrice else serviceOrderDetail.RateManualSalePrice end as TotalSalesPrice,
		serviceOrderDetail.SubTotalSalesPrice,
		serviceOrderDetail.ThirdPartyDiscountPercentage,
		serviceOrderDetail.PerformsHealthProfessionalCode PerformsHealthProfessionalCode,
		serviceOrderDetail .PerformsHealthProfessionalThirdPartyId as ThirdPartyId,
		serviceOrderDetail.ServiceDate,
		serviceOrderDetail.LiquidationType,
		serviceOrderDetail.PerformsProfessionalSpecialty,
		ISNULL(serviceOrderDetail.RateManualType,0)RateManualType,
		MedicalFeesContractId = 0,
		serviceOrder .Id as ServiceOrderId,
		ISNULL(medicalFeesCausation.AmountPayable,0) AmountPayable,
		ISNULL(medicalFeesCausation.TotalAmountPayable,0) as TotalAmountPayable,
		ISNULL(medicalFeesCausation.Id,0) as MedicalFeesCausationId,
		medicalFeesCausation.CreationUser,
		medicalFeesCausation.CreationDate,
		medicalFeesCausation.ModificationUser,
		medicalFeesCausation.ModificationDate,
		medicalFeesCausation.ConfirmationUser,
		medicalFeesCausation.ConfirmationDate,
		ISNULL(medicalFeesCausation.[Status],0) as StatusMedicalFeesCausation,
		ISNULL(medicalFeesCausation.TotalAmountPayableReal,0) TotalAmountPayableReal,
		ISNULL(medicalFeesCausation.PercentageCashed,0) PercentageCashed,
		invoiceDetail.Id as InvoiceDetailId,
		CONCAT(medicalFeesContract .Code,' - ',medicalFeesContract .ContractName) as MedicalFeesContractCodeName,
		serviceOrderDetail.IncomeMainAccountId,
		serviceOrderDetail.BillingConceptId,
		serviceOrderDetail.CostCenterId,
		ISNULL(serviceOrderDetail.RateManualDetailId,0) RateManualDetailId,
		RateManualDetailSurgicalId = 0,
		CostValue = 0,
		OnlyMedicalFees = 0,
		ServiceOrderDetailSurgicalId = 0,
		CONCAT(functionalUnit.Code,' - ',functionalUnit.Name) as FunctionalUnitDescription,
		serviceOrder.CreationUser as CreationUserServiceOrder,
		case when medicalFeesCausation.[Status] = 2 then -256 when medicalFeesCausation.[Status] = 3 then -7155632 when  medicalFeesCausation.[Status] = 1 then -1048576 else -1 end as Color,
		medicalFeesCausation.CausationDate,
		serviceOrder.AdmissionNumber,
		cupsEntity.Code,
		cupsEntity.ServiceType,
		invoice.InvoiceNumber,
		invoice.PatientCode
from
		Billing.ServiceOrderDetail serviceOrderDetail with (nolock)
		inner join [Payroll].FunctionalUnit functionalUnit with (nolock) on serviceOrderDetail.PerformsFunctionalUnitId = functionalUnit.Id
		inner join [Contract].IPSService ipsService with (nolock) on serviceOrderDetail .IPSServiceId = ipsService .Id
		inner join [Common].ThirdParty thirdParty with (nolock) on serviceOrderDetail.PerformsHealthProfessionalThirdPartyId = thirdParty .Id
		inner join Billing .ServiceOrder serviceOrder with (nolock) on serviceOrderDetail .ServiceOrderId = serviceOrder .Id
		inner join Billing .InvoiceDetail invoiceDetail with (nolock) on serviceOrderDetail .Id = invoiceDetail .ServiceOrderDetailId
		inner join Billing .Invoice invoice with (nolock) on invoiceDetail .InvoiceId = invoice .Id
		inner join [Contract] .CUPSEntity cupsEntity with (nolock) on serviceOrderDetail .CUPSEntityId = cupsEntity .Id
		inner join Billing .BillingGroup billingGroup with (nolock) on cupsEntity .BillingGroupId = billingGroup .Id
		left join MedicalFees .MedicalFeesCausation medicalFeesCausation with (nolock) on invoiceDetail.Id = medicalFeesCausation.InvoiceDetailId and medicalFeesCausation.ServiceOrderDetailId = serviceOrderDetail.Id and medicalFeesCausation.ServiceOrderDetailSurgicalId is null
		left join MedicalFees .MedicalFeesContract medicalFeesContract with (nolock) on medicalFeesCausation.MedicalFeesContractId = medicalFeesContract.Id
where serviceOrderDetail .Presentation = 1 And serviceOrderDetail .RecordType = 1 And invoice .Status = 1 and serviceOrderDetail.IsDelete = 0


union all


select 
		serviceOrderDetail.Id as Id,
		serviceOrderDetail.Id as ServiceOrderDetailId,
		sods.RateManualSalePrice as RateManualSalePrice,
		sods.InvoicedQuantity,
		ipsService .Id as IPSServiceId,
		CONCAT(ipsService .Code,' - ',ipsService .Name) as IPSServiceDescription,
		CONCAT(RTRIM(sods.PerformsHealthProfessionalCode),' - ',thirdParty .Name) as ThirdPartyDescription,
		invoiceDetail .InvoiceId as InvoiceId,
		cupsEntity.Id as CupsEntityId,
		CONCAT(cupsEntity.Code,' - ',cupsEntity.Description) as CupsEntityDescription,
		billingGroup.Id as BillingGroupId,
		CONCAT(billingGroup .Code,' - ',billingGroup.Name) as BillingGroupDescription,
		serviceOrder .Status,
		cast(0 as BIT) SelectOption,
		serviceOrderDetail .CareGroupId,
		isnull(serviceOrderDetail .RateManualId,0) RateManualId,
		serviceOrderDetail .GrandTotalSalesPrice,
		serviceOrderDetail .Presentation,
		case when serviceOrderDetail.TotalSalesPrice > 0 then serviceOrderDetail.TotalSalesPrice else serviceOrderDetail.RateManualSalePrice end as TotalSalesPrice,
		serviceOrderDetail.SubTotalSalesPrice,
		serviceOrderDetail.ThirdPartyDiscountPercentage,
		sods.PerformsHealthProfessionalCode,
		sods .PerformsHealthProfessionalThirdPartyId as ThirdPartyId,
		serviceOrderDetail.ServiceDate,
		serviceOrderDetail.LiquidationType,
		serviceOrderDetail.PerformsProfessionalSpecialty,
		serviceOrderDetail.RateManualType,
		MedicalFeesContractId = 0,
		serviceOrder .Id as ServiceOrderId,
		ISNULL(medicalFeesCausation.AmountPayable,0) AmountPayable,
		ISNULL(medicalFeesCausation.TotalAmountPayable,0) as TotalAmountPayable,
		ISNULL(medicalFeesCausation.Id,0) as MedicalFeesCausationId,
		medicalFeesCausation.CreationUser,
		medicalFeesCausation.CreationDate,
		medicalFeesCausation.ModificationUser,
		medicalFeesCausation.ModificationDate,
		medicalFeesCausation.ConfirmationUser,
		medicalFeesCausation.ConfirmationDate,
		ISNULL(medicalFeesCausation.[Status],0) as StatusMedicalFeesCausation,
		ISNULL(medicalFeesCausation.TotalAmountPayableReal,0) TotalAmountPayableReal,
		ISNULL(medicalFeesCausation.PercentageCashed,0) PercentageCashed,
		invoiceDetail.Id as InvoiceDetailId,
		CONCAT(medicalFeesContract .Code,' - ',medicalFeesContract .ContractName) as MedicalFeesContractCodeName,
		sods.IncomeMainAccountId,
		sods.BillingConceptId,
		sods.CostCenterId,
		ISNULL(serviceOrderDetail.RateManualDetailId,0) RateManualDetailId,
		sods.RateManualDetailSurgicalId,
		sods.CostValue,
		sods.OnlyMedicalFees,
		sods.Id as ServiceOrderDetailSurgicalId,
		CONCAT(functionalUnit.Code,' - ',functionalUnit.Name) as FunctionalUnitDescription,
		serviceOrder.CreationUser as CreationUserServiceOrder,
		case when medicalFeesCausation.[Status] = 2 then -256 when medicalFeesCausation.[Status] = 3 then -7155632 when  medicalFeesCausation.[Status] = 1 then -1048576 else -1 end as Color,
		medicalFeesCausation.CausationDate,
		serviceOrder.AdmissionNumber,
		cupsEntity.Code,
		cupsEntity.ServiceType,
		invoice.InvoiceNumber,
		invoice.PatientCode
from
		Billing.ServiceOrderDetailSurgical sods with (nolock) 
		inner join Billing.ServiceOrderDetail serviceOrderDetail with (nolock) on sods.ServiceOrderDetailId = serviceOrderDetail.Id
		inner join [Payroll].FunctionalUnit functionalUnit with (nolock) on serviceOrderDetail.PerformsFunctionalUnitId = functionalUnit.Id
		inner join [Contract].IPSService ipsService with (nolock) on sods.IPSServiceId = ipsService .Id
		inner join [Common].ThirdParty thirdParty with (nolock) on sods.PerformsHealthProfessionalThirdPartyId = thirdParty .Id
		inner join Billing .ServiceOrder serviceOrder with (nolock) on serviceOrderDetail .ServiceOrderId = serviceOrder .Id
		inner join Billing .InvoiceDetail invoiceDetail with (nolock) on serviceOrderDetail .Id = invoiceDetail .ServiceOrderDetailId
		inner join Billing .Invoice invoice with (nolock) on invoiceDetail .InvoiceId = invoice .Id
		inner join [Contract] .CUPSEntity cupsEntity with (nolock) on serviceOrderDetail .CUPSEntityId = cupsEntity .Id
		inner join Billing .BillingGroup billingGroup with (nolock) on cupsEntity .BillingGroupId = billingGroup .Id
		left join MedicalFees .MedicalFeesCausation medicalFeesCausation with (nolock) on invoiceDetail.Id = medicalFeesCausation.InvoiceDetailId and medicalFeesCausation.ServiceOrderDetailSurgicalId = sods.Id
		left join MedicalFees .MedicalFeesContract medicalFeesContract with (nolock) on medicalFeesCausation.MedicalFeesContractId = medicalFeesContract.Id
where serviceOrderDetail .Presentation = 1 And serviceOrderDetail .RecordType = 1 And invoice .Status = 1 and serviceOrderDetail.IsDelete = 0


)