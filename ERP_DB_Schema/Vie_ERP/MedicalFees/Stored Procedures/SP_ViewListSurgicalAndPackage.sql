





-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 22/03/2016
-- Description:	Procedimiento que se encarga de obtener los servicios quirurgicos
-- =============================================
CREATE PROCEDURE [MedicalFees].[SP_ViewListSurgicalAndPackage] 
	@InvoiceId as int
AS
BEGIN

	declare @TableReturn table(ServiceOrderDetailId int, RateManualSalePrice decimal(18,0), InvoicedQuantity int, IPSServiceId int, IPSServiceDescription varchar(100),
								IPSServiceDescriptionSOD varchar(100), IPSServiceSODId int, ThirdPartyDescription varchar(100), InvoiceId int, CupsEntityId int, CupsEntityDescription varchar(100), 
								BillingGroupId int, BillingGroupDescription varchar(100), Status int, SelectOption bit, CareGroupId int, RateManualId int, RateManualType int, 
								GrandTotalSalesPrice decimal(18,0), TotalSalesPrice decimal(18,0), SubTotalSalesPrice decimal(18,0), ThirdPartyDiscountPercentage decimal(18,0), 
								CostValue decimal(18,0), RateManualDetailSurgicalId int, BillingConceptId int, CostCenterId int, OnlyMedicalFees bit, Presentation int, PerformsHealthProfessionalCode varchar(100),
								ThirdPartyId int, ServiceDate datetime, LiquidationType int, PerformsProfessionalSpecialty varchar(20), MedicalFeesContractId int, ServiceOrderId int, ServiceOrderDetailSurgicalId int,
								AmountPayable decimal(18,0), TotalAmountPayable decimal(18,0), MedicalFeesCausationId int, CreationUser varchar(50), CreationDate datetime, ModificationUser varchar(50),
								ModificationDate datetime, ConfirmationUser varchar(50), ConfirmationDate datetime, StatusMedicalFeesCausation int, TotalAmountPayableReal decimal(18,0),
								PercentageCashed decimal(18,0), InvoiceDetailId int, MedicalFeesContractCodeName varchar(100), IncomeMainAccountId int, FunctionalUnitDescription varchar(100),
								CreationUserServiceOrder varchar(50), Color int, CausationDate datetime, IncludeServiceOrderDetailId int)

		--Se inserta los quirurgicos
		Insert into @TableReturn
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
		serviceOrderDetail.IncludeServiceOrderDetailId
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
		where invoice.Id = @InvoiceId and serviceOrderDetail.IsDelete = 0 and ((serviceOrderDetail .Presentation = 2 And serviceOrderDetail .RecordType = 1 And invoice .Status = 1) or (serviceOrderDetail.IsPackage = 0 and serviceOrderDetail.Presentation = 3))


		--Se inserta los quirurgicos
		Insert into @TableReturn
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
		SODD.IncludeServiceOrderDetailId
		from Billing.ServiceOrderDetail SOD with (nolock)
		inner join [Payroll].FunctionalUnit functionalUnit with (nolock) on SOD.PerformsFunctionalUnitId = functionalUnit.Id
		inner join Billing.ServiceOrderDetail SODD with (nolock)on SOD.Id = SODD.PackageServiceOrderDetailId
		inner join [Contract].IPSService ipsService with (nolock)on SODD.IPSServiceId = ipsService.Id
		inner join [Contract].IPSService ipsServiceSOD with (nolock)on SOD.IPSServiceId = ipsServiceSOD.Id
		inner join [Common].ThirdParty thirdParty with (nolock)on SODD.PerformsHealthProfessionalThirdPartyId = thirdParty.Id
		inner join Billing .InvoiceDetail invoiceDetail with (nolock)on SOD .Id = invoiceDetail .ServiceOrderDetailId
		inner join [Contract] .CUPSEntity cupsEntity with (nolock)on SODD .CUPSEntityId = cupsEntity .Id
		inner join Billing .BillingGroup billingGroup with (nolock)on cupsEntity .BillingGroupId = billingGroup .Id
		inner join Billing .ServiceOrder serviceOrder with (nolock)on SOD .ServiceOrderId = serviceOrder .Id
		left join MedicalFees .MedicalFeesCausation medicalFeesCausation with (nolock)on invoiceDetail.Id = medicalFeesCausation.InvoiceDetailId and SODD.Id = medicalFeesCausation.ServiceOrderDetailId
		left join MedicalFees .MedicalFeesContract medicalFeesContract with (nolock)on medicalFeesCausation.MedicalFeesContractId = medicalFeesContract.Id
		where InvoiceId = @InvoiceId and SOD.IsPackage = 1 and SODD.Presentation = 1 and SOD.IsDelete = 0


		--Se inserta los quirurgicos
		Insert into @TableReturn
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
		SODD.IncludeServiceOrderDetailId
		from Billing.ServiceOrderDetail SOD with (nolock)
		inner join [Payroll].FunctionalUnit functionalUnit with (nolock)on SOD.PerformsFunctionalUnitId = functionalUnit.Id
		inner join Billing.ServiceOrderDetail SODD with (nolock)on SOD.Id = SODD.PackageServiceOrderDetailId
		inner join Billing.ServiceOrderDetailSurgical SODS with (nolock)on SODS.ServiceOrderDetailId = SODD.Id
		inner join [Contract].IPSService ipsService with (nolock) on SODS.IPSServiceId = ipsService.Id
		inner join [Contract].IPSService ipsServiceSOD with (nolock) on SOD.IPSServiceId = ipsServiceSOD.Id
		inner join [Contract].IPSService ips with (nolock) on SODD.IPSServiceId = ips .Id
		inner join [Common].ThirdParty thirdParty with (nolock) on SODS.PerformsHealthProfessionalThirdPartyId = thirdParty.Id
		inner join Billing .InvoiceDetail invoiceDetail with (nolock) on SOD .Id = invoiceDetail .ServiceOrderDetailId
		inner join [Contract] .CUPSEntity cupsEntity with (nolock) on SODD .CUPSEntityId = cupsEntity .Id
		inner join Billing .BillingGroup billingGroup with (nolock) on cupsEntity .BillingGroupId = billingGroup .Id
		inner join Billing .ServiceOrder serviceOrder with (nolock) on SOD .ServiceOrderId = serviceOrder .Id
		left join MedicalFees .MedicalFeesCausation medicalFeesCausation with (nolock) on invoiceDetail.Id = medicalFeesCausation.InvoiceDetailId and SODS.Id = medicalFeesCausation.ServiceOrderDetailSurgicalId
		left join MedicalFees .MedicalFeesContract medicalFeesContract with (nolock) on medicalFeesCausation.MedicalFeesContractId = medicalFeesContract.Id
		where InvoiceId = @InvoiceId and SOD.IsPackage = 1 and SODD.Presentation = 2 and SOD.IsDelete = 0


		--Se insertan los quirurgicos
		Insert into @TableReturn
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
		SOD.IncludeServiceOrderDetailId
		from Billing.ServiceOrderDetail SOD with (nolock)
		inner join [Payroll].FunctionalUnit functionalUnit with (nolock) on SOD.PerformsFunctionalUnitId = functionalUnit.Id
		inner join [Contract].IPSService ipsService with (nolock) on SOD.IPSServiceId = ipsService.Id
		inner join [Common].ThirdParty thirdParty with (nolock) on SOD.PerformsHealthProfessionalThirdPartyId = thirdParty.Id
		inner join Billing .InvoiceDetail invoiceDetail with (nolock) on SOD .Id = invoiceDetail .ServiceOrderDetailId
		inner join [Contract] .CUPSEntity cupsEntity with (nolock) on SOD .CUPSEntityId = cupsEntity .Id
		inner join Billing .BillingGroup billingGroup with (nolock) on cupsEntity .BillingGroupId = billingGroup .Id
		inner join Billing .ServiceOrder serviceOrder with (nolock) on SOD .ServiceOrderId = serviceOrder .Id
		left join MedicalFees .MedicalFeesCausation medicalFeesCausation with (nolock) on invoiceDetail.Id = medicalFeesCausation.InvoiceDetailId and SOD.Id = medicalFeesCausation.ServiceOrderDetailId and SOD.PerformsHealthProfessionalCode = medicalFeesCausation.HealthProfessionalCode
		left join MedicalFees .MedicalFeesContract medicalFeesContract with (nolock) on medicalFeesCausation.MedicalFeesContractId = medicalFeesContract.Id
		where InvoiceId = @InvoiceId and SOD.IsPackage = 0 and SOD.Presentation = 3 and SOD.IsDelete = 0
		

		--Se retorna toda la tabla
		select * from @TableReturn
	
END