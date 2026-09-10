

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de servicios NO quirúrgicos facturados, utilizada para la liquidación y causación de honorarios médicos. Integra los detalles de órdenes de servicio (procedimientos, insumos, estancias), su línea de factura activa, el servicio IPS y su código CUPS, el grupo de facturación, la unidad funcional donde se realizó la prestación, y el profesional ejecutante con su tercero asociado. Combina dos bloques mediante UNION ALL: el primero para servicios directos con presentación estándar, y el segundo para sub-detalles relacionados (por ejemplo, ítems de una orden padre), incluyendo en ambos casos la causación de honorarios médicos si existe (monto pagable, porcentaje cobrado, estado y fechas de confirmación), el contrato de honorarios vinculado, y datos de la factura como número de factura y código del paciente. Sirve principalmente para la pantalla y reportes de liquidación de honorarios médicos no quirúrgicos, permitiendo visualizar qué está facturado, en qué estado de causación se encuentra y cuánto corresponde pagar al profesional.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListNoSurgical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListNoSurgical';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica los detalles de órdenes de servicio no quirúrgicos y quirúrgicos facturados, junto con su causación de honorarios médicos, para listar ítems facturables presentados con su estado y montos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListNoSurgical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El detalle de orden de servicio debe estar presentado (Presentation = 1); El detalle debe ser de tipo registro estándar (RecordType = 1); La factura asociada debe estar activa (Invoice.Status = 1); El detalle de orden de servicio no debe estar eliminado lógicamente (IsDelete = 0); Debe existir InvoiceDetail vinculado al ServiceOrderDetail (INNER JOIN obligatorio)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListNoSurgical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen ítems cuya factura esté activa (Invoice.Status=1) y cuyo detalle esté presentado, sea de tipo 1 y no eliminado; MedicalFeesContractId siempre se expone como 0 (constante) y MedicalFeesContractCodeName proviene del LEFT JOIN a MedicalFeesContract; Cuando no existe causación de honorarios, los montos AmountPayable, TotalAmountPayable, TotalAmountPayableReal, PercentageCashed y Status se devuelven como 0 vía ISNULL; SelectOption siempre se devuelve en 0 (BIT); En la rama no quirúrgica, ServiceOrderDetailSurgicalId, RateManualDetailSurgicalId, CostValue y OnlyMedicalFees siempre son 0; La causación de honorarios no quirúrgica se identifica porque ServiceOrderDetailSurgicalId IS NULL; la quirúrgica porque coincide con sods.Id; Color codifica visualmente el estado de la causación de honorarios (1, 2, 3) y -1 cuando no aplica', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListNoSurgical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de servicio; Detalle de orden de servicio; Factura; Detalle de factura; Honorarios médicos (Medical Fees); Causación de honorarios médicos; Contrato de honorarios médicos; CUPS; Grupo de facturación; Servicio IPS; Unidad funcional; Profesional de la salud; Procedimiento quirúrgico; Tarifario manual', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListNoSurgical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve la unión de detalles no quirúrgicos (desde Billing.ServiceOrderDetail) y quirúrgicos (desde Billing.ServiceOrderDetailSurgical) que cumplen Presentation=1, RecordType=1, Invoice.Status=1 e IsDelete=0.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListNoSurgical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si serviceOrderDetail.TotalSalesPrice > 0 → TotalSalesPrice = serviceOrderDetail.TotalSalesPrice else TotalSalesPrice = serviceOrderDetail.RateManualSalePrice (se usa el precio del tarifario manual); si medicalFeesCausation.Status = 2 → Color = -256 (amarillo); si medicalFeesCausation.Status = 3 → Color = -7155632; si medicalFeesCausation.Status = 1 → Color = -1048576; si Sin causación o estado distinto a 1/2/3 → Color = -1 (valor por defecto); si Origen del registro: rama 1 (no quirúrgico) vs rama 2 (quirúrgico vía ServiceOrderDetailSurgical) → En la rama no quirúrgica se fijan RateManualDetailSurgicalId=0, CostValue=0, OnlyMedicalFees=0, ServiceOrderDetailSurgicalId=0; en la quirúrgica se toman desde sods (ServiceOrderDetailSurgical); si Vínculo con MedicalFeesCausation (rama no quirúrgica) → JOIN exige medicalFeesCausation.ServiceOrderDetailSurgicalId IS NULL (causación no asociada a quirúrgico) else En la rama quirúrgica el JOIN exige medicalFeesCausation.ServiceOrderDetailSurgicalId = sods.Id', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListNoSurgical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetail; Payroll.FunctionalUnit; Contract.IPSService; Common.ThirdParty; Billing.ServiceOrder; Billing.InvoiceDetail; Billing.Invoice; Contract.CUPSEntity; Billing.BillingGroup; MedicalFees.MedicalFeesCausation; MedicalFees.MedicalFeesContract; Billing.ServiceOrderDetailSurgical', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListNoSurgical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListNoSurgical';
GO
