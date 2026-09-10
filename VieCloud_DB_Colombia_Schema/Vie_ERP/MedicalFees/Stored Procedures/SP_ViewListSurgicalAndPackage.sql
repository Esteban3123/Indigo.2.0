

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta todos los servicios quirúrgicos y paquetes facturados asociados a una factura específica, identificada por su ID. Integra el detalle quirúrgico de la orden de servicio con el catálogo de servicios de la IPS, la información del profesional de la salud que realizó el procedimiento, la unidad funcional ejecutora, el código CUPS, el grupo de facturación y el encabezado de la factura. Devuelve valores de venta, costos, honorarios médicos, estado de causación de honorarios, porcentaje cobrado, cuenta contable de ingresos y datos de auditoría (creación, modificación, confirmación), con un color indicador del estado de la causación. Se usa en el módulo de honorarios médicos para visualizar, liquidar y auditar los servicios quirúrgicos cobrados dentro de una factura.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'PROCEDURE', @level1name = N'SP_ViewListSurgicalAndPackage';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'PROCEDURE', @level1name = N'SP_ViewListSurgicalAndPackage';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida y devuelve los servicios quirúrgicos y de paquetes asociados a una factura, incluyendo su causación de honorarios médicos y un código de color según el estado de causación.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ViewListSurgicalAndPackage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura indicada debe existir en Billing.Invoice; Los detalles de orden de servicio deben tener IsDelete = 0 para ser considerados; Para incluir registros quirúrgicos directos: Presentation=2 con RecordType=1 y factura en Status=1, o bien IsPackage=0 con Presentation=3; Para incluir contenido de paquetes: el detalle padre debe tener IsPackage=1', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ViewListSurgicalAndPackage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan detalles con IsDelete=0; SelectOption siempre se inicializa en 0; MedicalFeesContractId siempre se entrega en 0 (no se toma del contrato real); ThirdPartyDiscountPercentage se fija en 0 para los flujos quirúrgicos directos y de paquete tipo 2; El emparejamiento con MedicalFeesCausation es opcional (LEFT JOIN); si no existe causación, los campos relacionados quedan en NULL y Color=-1; El procedimiento es de solo lectura: no modifica tablas físicas', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ViewListSurgicalAndPackage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Detalle de factura; Orden de servicio; Detalle de orden de servicio; Servicio quirúrgico; Paquete de servicios; Honorarios médicos; Causación de honorarios médicos; Contrato de honorarios médicos; Profesional de la salud; CUPS; IPS; Unidad funcional; Grupo de facturación; Tarifario (manual de tarifas)', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ViewListSurgicalAndPackage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableReturn: Cuando ServiceOrderDetail.Presentation=2 y RecordType=1 con Invoice.Status=1, o cuando IsPackage=0 y Presentation=3, se insertan los servicios quirúrgicos desde ServiceOrderDetailSurgical para la factura.; [INSERT] @TableReturn: Cuando el detalle padre tiene IsPackage=1 y el detalle hijo (PackageServiceOrderDetailId) tiene Presentation=1, se inserta el contenido no quirúrgico del paquete.; [INSERT] @TableReturn: Cuando el detalle padre tiene IsPackage=1 y el detalle hijo tiene Presentation=2, se inserta el contenido quirúrgico del paquete tomando datos de ServiceOrderDetailSurgical del hijo.; [INSERT] @TableReturn: Cuando IsPackage=0 y Presentation=3, se inserta el detalle quirúrgico tomando los valores directamente de ServiceOrderDetail (sin tabla quirúrgica) emparejando la causación por HealthProfessionalCode.; [RETURN_RESULT] RESULT: Al final retorna todos los registros consolidados de @TableReturn.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ViewListSurgicalAndPackage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TotalSalesPrice = 0 → Se usa RateManualSalePrice como TotalSalesPrice else Se conserva TotalSalesPrice original; si MedicalFeesCausation.Status = 2 → Color = -256 (amarillo) else Se evalúa siguiente estado; si MedicalFeesCausation.Status = 3 → Color = -7155632 else Se evalúa siguiente estado; si MedicalFeesCausation.Status = 1 → Color = -1048576 (rojo) else Color = -1 (sin causación o estado distinto); si Presentation=2 y RecordType=1 y Invoice.Status=1, o IsPackage=0 y Presentation=3 → Incluye registros quirúrgicos individuales de la factura; si SOD.IsPackage=1 y SODD.Presentation=1 → Incluye ítems no quirúrgicos contenidos en paquetes; si SOD.IsPackage=1 y SODD.Presentation=2 → Incluye ítems quirúrgicos contenidos en paquetes; si SOD.IsPackage=0 y SOD.Presentation=3 → Incluye quirúrgicos derivados directamente del detalle de orden', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ViewListSurgicalAndPackage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetailSurgical; Billing.ServiceOrderDetail; Billing.ServiceOrder; Billing.InvoiceDetail; Billing.Invoice; Billing.BillingGroup; Contract.IPSService; Contract.CUPSEntity; Common.ThirdParty; Payroll.FunctionalUnit; MedicalFees.MedicalFeesCausation; MedicalFees.MedicalFeesContract', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ViewListSurgicalAndPackage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ViewListSurgicalAndPackage';
-- GO
