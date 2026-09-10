-- ===============================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 29/01/2020
-- Description:	Procedimiento que se encarga del proceso cotizaciones 
-- ===============================================================================================================================
CREATE PROCEDURE [Billing].[SP_SaveQuotation]
	@Xml xml,
	@UserCode varchar(20)
AS
BEGIN
	
	--Variables para guardar la cabecera
	declare @Id int, @Code varchar(20), @DocumentDate datetime, @QuotationType tinyint, @AdmissionNumber varchar(10), @ThirdPartyId int, @OperatingUnitId int, @Status tinyint, @Description varchar(max),
	@IsEconomicActivity INT

	--Tabla de detalles de ordenes de servicio para la cotización
	declare @QuotationServiceOrderDetail table(RowId int, Id int, QuotationId int, CareGroupId int, HealthAdministratorId int, ThirdPartyId int, ServiceType tinyint, RecordType tinyint, CUPSEntityId int, 
	IPSServiceId int, HospitalStayId int, HospitalStayDetailId int, ControlExternalConsultation tinyint, ControlExternalConsultationCode numeric(18,0), CUPSAssociateService bit, 
	CodeAssociateService varchar(50), IsPackage bit, Packaging bit, PackageServiceOrderDetailId int, LiquidationType tinyint, Presentation tinyint, ProductId int, InvoicedQuantity int, 
	SupplyQuantity int, DevolutionQuantity int, RateManualSalePrice numeric(18,0), CostValue numeric(18,2), ServiceDate datetime, AuthorizationNumber varchar(20), PerformsFunctionalUnitId int, 
	PerformsHealthProfessionalCode char(20), PerformsProfessionalSpecialty char(3), PerformsHealthProfessionalThirdPartyId int, BillingConceptId int, CostCenterId int, SettlementType tinyint, 
	IncludeServiceOrderDetailId int, RecoveryRatio numeric(5,2), RateManualId int, RateManualType tinyint, RateManualDetailId int, DefinitionRateDetailId int, DefinitionRateDetailConditionId int, 
	SubTotalSalesPrice numeric(18,2), ThirdPartyDiscount numeric(18,0), ThirdPartyDiscountPercentage numeric(5,2), TotalSalesPrice numeric(18,2), GrandTotalSalesPrice numeric(18,0), 
	SurchargeApply bit, SurgicalInterventionType tinyint, SurgeryNumber tinyint, IsFirstEvent bit, IsAnnulled bit, IsDelete bit, IncomeMainAccountId int, 
	ApplyRIAS bit, RIASCupsId int, CUPSEntityContractDescriptionId int, ItemDelete bit,EconomicActivityId Int)--cambio

	--Tabla de detalles de distribución de ordenes de servicio para la cotización
	declare @QuotationServiceOrderDetailDistribution table(RowId int, Id int, QuotationServiceOrderDetailId int, Quantity int, GrandTotalSalesPrice numeric(18,0), GrandTotalDiscount numeric(18,0), 
	DistributionType tinyint, ThirdPartySalesPrice numeric(18,0), ThirdPartyPercentage numeric(5,2), ApplyRecoveryFee tinyint, RecoveryFeeType tinyint, 
	SubTotalPatientSalesPrice numeric(18,0), PatientPercentage numeric(5,2), LastCaregroupId int, ItemDelete bit)

	--Tabla de detalles qx de orden de servicio para la cotización
	declare @QuotationServiceOrderDetailSurgical table(RowId int, Id int, QuotationServiceOrderDetailId int, IPSServiceId int, InvoicedQuantity int, LiquidationPercentage numeric(5,2), 
	RateManualSalePrice numeric(18,0), TotalSalesPrice numeric(18,0), PerformsHealthProfessionalCode char(20), PerformsHealthProfessionalThirdPartyId int, CostValue numeric(18,2), 
	BillingConceptId int, CostCenterId int, RateManualDetailSurgicalId int, SurchargeApply bit, OnlyMedicalFees bit, IncomeMainAccountId int, ItemDelete bit,EconomicActivityId Int)--cambio

	--Tabla de detalles de dispensación para la cotización
	declare @QuotationPharmaceuticalDispensingDetail table(RowId int, Id int, QuotationId int, CareGroupId int, HealthAdministratorId int, ThirdPartyId int, ProductId int, WarehouseId int, 
	Quantity int, ReturnedQuantity int, ServiceDate datetime, FunctionalUnitId int, OrderedHealthProfessionalCode char(20), OrderedProfessionalSpecialty char(3), 
	OrderedHealthProfessionalThirdPartyId int, AuthorizationNumber varchar(20), LiquidationType tinyint, CupsEntityId int, SurchargeApply bit, SalePrice numeric(20,4), 
	AverageCost numeric(20,4), DiscountPercentage numeric(5,2), DiscountValue numeric(20,4), TotalSalesPrice numeric(20,4), GrandTotalSalesPrice numeric(20,4), ItemDelete bit,EconomicActivityId Int)--cambio

	--Tabla de lotes de dispensación para la cotización
	declare @QuotationPharmaceuticalDispensingDetailBatchSerial table(RowId int, Id int, QuotationPharmaceuticalDispensingDetailId int, PhysicalInventoryId int, Quantity int, 
	OutstandingQuantity int, PhysicalInventoryCustodyId int, ItemDelete bit)

	--Variable en donde se almacenan los mensajes que se va a devolver
	declare @MessageReturn varchar(max)

	Begin try
		
		--Se obtienen los datos para la cabecera
		select 
			@Id = t.x.value('Id[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@DocumentDate = convert(date, t.x.value('DocumentDate[1]','varchar(20)'), 103),
			@QuotationType = t.x.value('QuotationType[1]','tinyint'),
			@AdmissionNumber = IIF(t.x.value('AdmissionNumber[1]','varchar(10)') = '', null, t.x.value('AdmissionNumber[1]','varchar(10)')),
			@ThirdPartyId = IIF(t.x.value('ThirdPartyId[1]','varchar(20)') = '', null, t.x.value('ThirdPartyId[1]','varchar(20)')),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@Status = t.x.value('Status[1]','tinyint'),
			@Description = IIF(t.x.value('Description[1]','varchar(max)') = '', null, t.x.value('Description[1]','varchar(max)'))
		from @Xml.nodes('/Quotation') t(x)
		
		--Si se esta anulando
		if @Status = 3
		begin
			--Se actualiza el campo de cotización de la dashboard de cotizaciones
			update Billing.DashboardQuoted set QuotationId = null where QuotationId = @Id

			update Billing.Quotation set Status = 3 where Id = @Id
			select 0 as CodeMessage, 'El registro con código ' + @Code + ' se anuló correctamente' as Message, @Id QuotationId, @Code QuotationCode
			return
		end
		
		--Si se esta desconfirmando
		if @Status = 0
		begin
			--Se valida que no haya items del detalle de la cotización que este asociada a una orden de servicio
			if exists(select 1
			from Billing.Quotation q
			inner join Billing.QuotationServiceOrderDetail qsod on qsod.QuotationId = q.Id
			inner join Billing.ServiceOrderDetail sod on sod.QuotationServiceOrderDetailId = qsod.Id
			where q.Id = @Id)
			begin
				select 999 as CodeMessage, 'No se puede desconfirmar porque hay detalles que ya están asociados a una orden de servicio' as Message, 0 QuotationId, '' QuotationCode
				return
			end

			update Billing.Quotation set Status = 1 where Id = @Id
			select 0 as CodeMessage, 'El registro con código ' + @Code + ' se desconfirmó correctamente' as Message, @Id QuotationId, @Code QuotationCode
			return
		end
		--OBTENEMOS SI ES GENERADORA DE ACTIVIDAD ECONOMICA
		 SET @IsEconomicActivity = ISNULL((SELECT TOP 1 TransactionEconomicActivity  FROM GeneralLedger.CompanySettings),0)

		--Se obtienen los detalles de ServiceOrderDetail del xml
		insert into @QuotationServiceOrderDetail
		select 
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('QuotationId[1]','int') as QuotationId,
			t.x.value('CareGroupId[1]','int') as CareGroupId,
			IIF(t.x.value('HealthAdministratorId[1]','varchar(20)') = '', null, t.x.value('HealthAdministratorId[1]','varchar(20)')) as HealthAdministratorId,
			IIF(t.x.value('ThirdPartyId[1]','varchar(20)') = '', null, t.x.value('ThirdPartyId[1]','varchar(20)')) as ThirdPartyId,
			t.x.value('ServiceType[1]','tinyint') as ServiceType,
			t.x.value('RecordType[1]','tinyint') as RecordType,
			IIF(t.x.value('CUPSEntityId[1]','varchar(20)') = '', null, t.x.value('CUPSEntityId[1]','varchar(20)')) as CUPSEntityId,
			IIF(t.x.value('IPSServiceId[1]','varchar(20)') = '', null, t.x.value('IPSServiceId[1]','varchar(20)')) as IPSServiceId,
			IIF(t.x.value('HospitalStayId[1]','varchar(20)') = '', null, t.x.value('HospitalStayId[1]','varchar(20)')) as HospitalStayId,
			IIF(t.x.value('HospitalStayDetailId[1]','varchar(20)') = '', null, t.x.value('HospitalStayDetailId[1]','varchar(20)')) as HospitalStayDetailId,
			IIF(t.x.value('ControlExternalConsultation[1]','varchar(20)') = '', null, t.x.value('ControlExternalConsultation[1]','varchar(20)')) as ControlExternalConsultation,
			IIF(t.x.value('ControlExternalConsultationCode[1]','varchar(20)') = '', null, t.x.value('ControlExternalConsultationCode[1]','varchar(20)')) as ControlExternalConsultationCode,
			t.x.value('CUPSAssociateService[1]','bit') as CUPSAssociateService,
			IIF(t.x.value('CodeAssociateService[1]','varchar(50)') = '', null, t.x.value('CodeAssociateService[1]','varchar(50)')) as CodeAssociateService,
			t.x.value('IsPackage[1]','bit') as IsPackage,
			t.x.value('Packaging[1]','bit') as Packaging,
			IIF(t.x.value('PackageServiceOrderDetailId[1]','varchar(20)') = '', null, t.x.value('PackageServiceOrderDetailId[1]','varchar(20)')) as PackageServiceOrderDetailId,
			t.x.value('LiquidationType[1]','tinyint') as LiquidationType,
			IIF(t.x.value('Presentation[1]','varchar(20)') = '', null, t.x.value('Presentation[1]','varchar(20)')) as Presentation,
			IIF(t.x.value('ProductId[1]','varchar(20)') = '', null, t.x.value('ProductId[1]','varchar(20)')) as ProductId,
			t.x.value('InvoicedQuantity[1]','int') as InvoicedQuantity,
			t.x.value('SupplyQuantity[1]','int') as SupplyQuantity,
			t.x.value('DevolutionQuantity[1]','int') as DevolutionQuantity,
			REPLACE(t.x.value('RateManualSalePrice[1]','varchar(20)'), ',', '.') as RateManualSalePrice,
			REPLACE(t.x.value('CostValue[1]','varchar(20)'), ',', '.') as CostValue,
			convert(date, t.x.value('ServiceDate[1]','varchar(20)'), 103),
			IIF(t.x.value('AuthorizationNumber[1]','varchar(20)') = '', null, t.x.value('AuthorizationNumber[1]','varchar(20)')) as AuthorizationNumber,
			t.x.value('PerformsFunctionalUnitId[1]','int') as PerformsFunctionalUnitId,
			IIF(t.x.value('PerformsHealthProfessionalCode[1]','char(20)') = '', null, t.x.value('PerformsHealthProfessionalCode[1]','char(20)')) as PerformsHealthProfessionalCode,
			IIF(t.x.value('PerformsProfessionalSpecialty[1]','char(3)') = '', null, t.x.value('PerformsProfessionalSpecialty[1]','char(3)')) as PerformsHealthProfessionalCode,
			IIF(t.x.value('PerformsHealthProfessionalThirdPartyId[1]','varchar(20)') = '', null, t.x.value('PerformsHealthProfessionalThirdPartyId[1]','varchar(20)')) as PerformsHealthProfessionalThirdPartyId,
			IIF(t.x.value('BillingConceptId[1]','varchar(20)') = '', null, t.x.value('BillingConceptId[1]','varchar(20)')) as BillingConceptId,
			t.x.value('CostCenterId[1]','int') as CostCenterId,
			t.x.value('SettlementType[1]','tinyint') as SettlementType,
			IIF(t.x.value('IncludeServiceOrderDetailId[1]','varchar(20)') = '', null, t.x.value('IncludeServiceOrderDetailId[1]','varchar(20)')) as IncludeServiceOrderDetailId,
			IIF(t.x.value('RecoveryRatio[1]','varchar(20)') = '', null, REPLACE(t.x.value('RecoveryRatio[1]','varchar(20)'), ',', '.')) as RecoveryRatio,
			IIF(t.x.value('RateManualId[1]','varchar(20)') = '', null, t.x.value('RateManualId[1]','varchar(20)')) as RateManualId,
			IIF(t.x.value('RateManualType[1]','varchar(20)') = '', null, t.x.value('RateManualType[1]','varchar(20)')) as RateManualType,
			IIF(t.x.value('RateManualDetailId[1]','varchar(20)') = '', null, t.x.value('RateManualDetailId[1]','varchar(20)')) as RateManualDetailId,
			IIF(t.x.value('DefinitionRateDetailId[1]','varchar(20)') = '', null, t.x.value('DefinitionRateDetailId[1]','varchar(20)')) as DefinitionRateDetailId,
			IIF(t.x.value('DefinitionRateDetailConditionId[1]','varchar(20)') = '', null, t.x.value('DefinitionRateDetailConditionId[1]','varchar(20)')) as DefinitionRateDetailConditionId,
			REPLACE(t.x.value('SubTotalSalesPrice[1]','varchar(20)'), ',', '.') as SubTotalSalesPrice,
			REPLACE(t.x.value('ThirdPartyDiscount[1]','varchar(20)'), ',', '.') as ThirdPartyDiscount,
			REPLACE(t.x.value('ThirdPartyDiscountPercentage[1]','varchar(20)'), ',', '.') as ThirdPartyDiscountPercentage,
			REPLACE(t.x.value('TotalSalesPrice[1]','varchar(20)'), ',', '.') as TotalSalesPrice,
			REPLACE(t.x.value('GrandTotalSalesPrice[1]','varchar(20)'), ',', '.') as GrandTotalSalesPrice,
			t.x.value('SurchargeApply[1]','bit') as SurchargeApply,
			IIF(t.x.value('SurgicalInterventionType[1]','varchar(20)') = '', null, t.x.value('SurgicalInterventionType[1]','varchar(20)')) as SurgicalInterventionType,
			t.x.value('SurgeryNumber[1]','tinyint') as SurgeryNumber,
			t.x.value('IsFirstEvent[1]','bit') as IsFirstEvent,
			t.x.value('IsAnnulled[1]','bit') as IsAnnulled,
			t.x.value('IsDelete[1]','bit') as IsDelete,
			t.x.value('IncomeMainAccountId[1]','int') as IncomeMainAccountId,
			IIF(t.x.value('ApplyRIAS[1]','varchar(20)') = '', null, t.x.value('ApplyRIAS[1]','varchar(20)')) as ApplyRIAS,
			IIF(t.x.value('RIASCupsId[1]','varchar(20)') = '', null, t.x.value('RIASCupsId[1]','varchar(20)')) as RIASCupsId,
			IIF(t.x.value('CUPSEntityContractDescriptionId[1]','varchar(20)') = '', null, t.x.value('CUPSEntityContractDescriptionId[1]','varchar(20)')) as CUPSEntityContractDescriptionId,
			t.x.value('ItemDelete[1]','bit') as ItemDelete,
			NULL
		from @Xml.nodes('/Quotation/QuotationServiceOrderDetail') t(x)
		

		--Se obtienen los detalles de ServiceOrderDetailDistribution del xml
		insert into @QuotationServiceOrderDetailDistribution
		select 
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('QuotationServiceOrderDetailId[1]','int') as QuotationServiceOrderDetailId,
			t.x.value('Quantity[1]','int') as Quantity,
			REPLACE(t.x.value('GrandTotalSalesPrice[1]','varchar(20)'), ',', '.') as GrandTotalSalesPrice,
			REPLACE(t.x.value('GrandTotalDiscount[1]','varchar(20)'), ',', '.') as GrandTotalDiscount,
			t.x.value('DistributionType[1]','tinyint') as DistributionType,
			REPLACE(t.x.value('ThirdPartySalesPrice[1]','varchar(20)'), ',', '.') as ThirdPartySalesPrice,
			REPLACE(t.x.value('ThirdPartyPercentage[1]','varchar(20)'), ',', '.') as ThirdPartyPercentage,
			t.x.value('ApplyRecoveryFee[1]','tinyint') as ApplyRecoveryFee,
			t.x.value('RecoveryFeeType[1]','tinyint') as RecoveryFeeType,
			REPLACE(t.x.value('SubTotalPatientSalesPrice[1]','varchar(20)'), ',', '.') as SubTotalPatientSalesPrice,
			REPLACE(t.x.value('PatientPercentage[1]','varchar(20)'), ',', '.') as PatientPercentage,
			t.x.value('LastCaregroupId[1]','int') as LastCaregroupId,
			t.x.value('ItemDelete[1]','bit') as ItemDelete
		from @Xml.nodes('/Quotation/QuotationServiceOrderDetail/QuotationServiceOrderDetailDistribution') t(x)

		--Se obtienen los detalles de ServiceOrderDetailSurgical del xml
		insert into @QuotationServiceOrderDetailSurgical
		select 
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('QuotationServiceOrderDetailId[1]','int') as QuotationServiceOrderDetailId,
			t.x.value('IPSServiceId[1]','int') as IPSServiceId,
			t.x.value('InvoicedQuantity[1]','int') as InvoicedQuantity,
			REPLACE(t.x.value('LiquidationPercentage[1]','varchar(20)'), ',', '.') as LiquidationPercentage,
			REPLACE(t.x.value('RateManualSalePrice[1]','varchar(20)'), ',', '.') as RateManualSalePrice,
			REPLACE(t.x.value('TotalSalesPrice[1]','varchar(20)'), ',', '.') as TotalSalesPrice,
			IIF(t.x.value('PerformsHealthProfessionalCode[1]','char(20)') = '', null, t.x.value('PerformsHealthProfessionalCode[1]','char(20)')) as PerformsHealthProfessionalCode,
			IIF(t.x.value('PerformsHealthProfessionalThirdPartyId[1]','varchar(20)') = '', null, t.x.value('PerformsHealthProfessionalThirdPartyId[1]','varchar(20)')) as PerformsHealthProfessionalThirdPartyId,
			REPLACE(t.x.value('CostValue[1]','varchar(20)'), ',', '.') as CostValue,
			t.x.value('BillingConceptId[1]','int') as BillingConceptId,
			t.x.value('CostCenterId[1]','int') as CostCenterId,
			IIF(t.x.value('RateManualDetailSurgicalId[1]','varchar(20)') = '', null, t.x.value('RateManualDetailSurgicalId[1]','varchar(20)')) as RateManualDetailSurgicalId,
			t.x.value('SurchargeApply[1]','bit') as SurchargeApply,
			t.x.value('OnlyMedicalFees[1]','bit') as OnlyMedicalFees,
			IIF(t.x.value('IncomeMainAccountId[1]','varchar(20)') = '' or t.x.value('IncomeMainAccountId[1]','varchar(20)') = '0', null, t.x.value('IncomeMainAccountId[1]','varchar(20)')) as IncomeMainAccountId,
			t.x.value('ItemDelete[1]','bit') as ItemDelete,
			NULL
		from @Xml.nodes('/Quotation/QuotationServiceOrderDetail/QuotationServiceOrderDetailSurgical') t(x)

		--Se obtienen los detalles de PharmaceuticalDispensingDetail del xml
		insert into @QuotationPharmaceuticalDispensingDetail
		select 
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('QuotationId[1]','int') as QuotationId,
			t.x.value('CareGroupId[1]','int') as CareGroupId,
			IIF(t.x.value('HealthAdministratorId[1]','varchar(20)') = '', null, t.x.value('HealthAdministratorId[1]','varchar(20)')) as HealthAdministratorId,
			IIF(t.x.value('ThirdPartyId[1]','varchar(20)') = '', null, t.x.value('ThirdPartyId[1]','varchar(20)')) as ThirdPartyId,
			t.x.value('ProductId[1]','int') as ProductId,
			t.x.value('WarehouseId[1]','int') as WarehouseId,
			t.x.value('Quantity[1]','int') as Quantity,
			t.x.value('ReturnedQuantity[1]','int') as ReturnedQuantity,
			convert(date, t.x.value('ServiceDate[1]','varchar(20)'), 103) as ServiceDate,
			t.x.value('FunctionalUnitId[1]','int') as FunctionalUnitId,
			IIF(t.x.value('OrderedHealthProfessionalCode[1]','char(20)') = '', null, t.x.value('OrderedHealthProfessionalCode[1]','char(20)')) as OrderedHealthProfessionalCode,
			IIF(t.x.value('OrderedProfessionalSpecialty[1]','char(3)') = '', null, t.x.value('OrderedProfessionalSpecialty[1]','char(3)')) as OrderedProfessionalSpecialty,
			IIF(t.x.value('OrderedHealthProfessionalThirdPartyId[1]','varchar(20)') = '', null, t.x.value('OrderedHealthProfessionalThirdPartyId[1]','varchar(20)')) as OrderedHealthProfessionalThirdPartyId,
			IIF(t.x.value('AuthorizationNumber[1]','varchar(20)') = '', null, t.x.value('AuthorizationNumber[1]','varchar(20)')) as AuthorizationNumber,
			t.x.value('LiquidationType[1]','tinyint') as LiquidationType,
			IIF(t.x.value('CupsEntityId[1]','varchar(20)') = '', null, t.x.value('CupsEntityId[1]','varchar(20)')) as CupsEntityId,
			t.x.value('SurchargeApply[1]','bit') as SurchargeApply,
			REPLACE(t.x.value('SalePrice[1]','varchar(20)'), ',', '.') as SalePrice,
			REPLACE(t.x.value('AverageCost[1]','varchar(20)'), ',', '.') as AverageCost,
			REPLACE(t.x.value('DiscountPercentage[1]','varchar(20)'), ',', '.') as DiscountPercentage,
			REPLACE(t.x.value('DiscountValue[1]','varchar(20)'), ',', '.') as DiscountValue,
			REPLACE(t.x.value('TotalSalesPrice[1]','varchar(20)'), ',', '.') as TotalSalesPrice,
			REPLACE(t.x.value('GrandTotalSalesPrice[1]','varchar(20)'), ',', '.') as GrandTotalSalesPrice,
			t.x.value('ItemDelete[1]','bit') as ItemDelete,
			NULL
		from @Xml.nodes('/Quotation/QuotationPharmaceuticalDispensingDetail') t(x)

		--Se obtienen los detalles de PharmaceuticalDispensingDetailBatch del xml
		insert into @QuotationPharmaceuticalDispensingDetailBatchSerial
		select 
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('QuotationPharmaceuticalDispensingDetailId[1]','int') as QuotationPharmaceuticalDispensingDetailId,
			IIF(t.x.value('PhysicalInventoryId[1]','varchar(20)') = '', null, t.x.value('PhysicalInventoryId[1]','varchar(20)')) as PhysicalInventoryId,
			t.x.value('Quantity[1]','int') as Quantity,
			t.x.value('OutstandingQuantity[1]','int') as OutstandingQuantity,
			IIF(t.x.value('PhysicalInventoryCustodyId[1]','varchar(20)') = '', null, t.x.value('PhysicalInventoryCustodyId[1]','varchar(20)')) as PhysicalInventoryCustodyId,
			t.x.value('ItemDelete[1]','bit') as ItemDelete
		from @Xml.nodes('/Quotation/QuotationPharmaceuticalDispensingDetail/QuotationPharmaceuticalDispensingDetailBatchSerial') t(x)

		--Se eliminan los detalles
		delete from Billing.QuotationServiceOrderDetailSurgical where Id in (select Id from @QuotationServiceOrderDetailSurgical where ItemDelete = 1 and Id > 0)
		delete from @QuotationServiceOrderDetailSurgical where ItemDelete = 1 and Id > 0

		delete from Billing.QuotationServiceOrderDetailDistribution where Id in (select Id from @QuotationServiceOrderDetailDistribution where ItemDelete = 1 and Id > 0)
		delete from @QuotationServiceOrderDetailDistribution where ItemDelete = 1 and Id > 0

		delete from Billing.QuotationServiceOrderDetail where Id in (select Id from @QuotationServiceOrderDetail where ItemDelete = 1 and Id > 0)
		delete from @QuotationServiceOrderDetail where ItemDelete = 1 and Id > 0

		delete from Billing.QuotationPharmaceuticalDispensingDetailBatchSerial where Id in (select Id from @QuotationPharmaceuticalDispensingDetailBatchSerial where ItemDelete = 1 and Id > 0)
		delete from @QuotationPharmaceuticalDispensingDetailBatchSerial where ItemDelete = 1 and Id > 0

		delete from Billing.QuotationPharmaceuticalDispensingDetail where Id in (select Id from @QuotationPharmaceuticalDispensingDetail where ItemDelete = 1 and Id > 0)
		delete from @QuotationPharmaceuticalDispensingDetail where ItemDelete = 1 and Id > 0

		--Tabla para almacenar los errores de actividad economica
			DECLARE @ErrorSTable TABLE ( --Hubo cambios aqui
				CodeResult VARCHAR(10),
				MessageResult VARCHAR(MAX),
				StatusResult INT,
				Id INT
			);
		
		--SELECT * FROM @QuotationPharmaceuticalDispensingDetail--BORRAR
		--SELECT * FROM @QuotationServiceOrderDetail--BORRAR
		--SELECT * FROM @QuotationServiceOrderDetailSurgical--BORRAR
		
		
			IF(@IsEconomicActivity = 1)--cambios
			BEGIN 
			UPDATE QPD SET QPD.EconomicActivityId = PG.EconomicActivityId FROM Inventory.ProductGroup PG
																	  JOIN Inventory.InventoryProduct IPR ON PG.ID = IPR.ProductGroupId
																	  JOIN @QuotationPharmaceuticalDispensingDetail QPD ON IPR.ID = QPD.ProductId

			
			UPDATE QSDL SET QSDL.EconomicActivityId = BC.EconomicActivityId FROM @QuotationServiceOrderDetail QSDL
												JOIN Billing.BillingConcept BC ON BC.Id = QSDL.BillingConceptId;

			UPDATE QSDLS SET QSDLS.EconomicActivityId = BC.EconomicActivityId FROM @QuotationServiceOrderDetailSurgical QSDLS
												JOIN Billing.BillingConcept BC ON BC.Id = QSDLS.BillingConceptId;
			INSERT INTO @ErrorsTable (CodeResult, MessageResult, StatusResult, Id)
				SELECT 
								'999', 
								CONCAT('No se tiene definida una Actividad Económica Generadora de Ingreso en el Grupo ', p.Name, ' asociado al Producto ', pg.name),
								3, 
								0
								FROM @QuotationPharmaceuticalDispensingDetail QPtd
								JOIN Inventory.InventoryProduct p on p.Id = QPtd.ProductId
								JOIN Inventory.ProductGroup pg on pg.Id = p.ProductGroupId
								WHERE QPtd.EconomicActivityId IS NULL
								UNION
								SELECT 
								'999', 
								CONCAT('No se tiene definida una Actividad Económica Generadora de Ingreso en el Concepto de Facturación ', BC.Name, ' asociado al servicio ', IPSS.Name),
								3, 
								0
								FROM @QuotationServiceOrderDetailSurgical TS
								JOIN Billing.BillingConcept BC ON BC.ID = TS.BillingConceptId
								JOIN Contract.IPSService IPSS ON IPSS.Id = TS.IPSServiceId
								WHERE TS.EconomicActivityId IS NULL
								UNION
								SELECT 
								'999', 
								CONCAT('No se tiene definida una Actividad Económica Generadora de Ingreso en el Concepto de Facturación ', BC.Name, ' asociado al servicio ', IPSS.Name),
								3, 
								0
								FROM @QuotationServiceOrderDetail QSOD
								JOIN Billing.BillingConcept BC ON BC.ID = QSOD.BillingConceptId
								JOIN Contract.IPSService IPSS ON IPSS.Id = QSOD.IPSServiceId
								WHERE QSOD.EconomicActivityId IS NULL AND QSOD.ProductId IS NULL
			END
			

		--SELECT * FROM @QuotationPharmaceuticalDispensingDetail--BORRAR
		--SELECT * FROM @QuotationServiceOrderDetail--BORRAR
		--SELECT * FROM @QuotationServiceOrderDetailSurgical--BORRAR
			IF( SELECT COUNT(*) FROM @ErrorSTable)>0
			BEGIN
				DECLARE @AllErrorsS NVARCHAR(MAX) = '';
				SELECT @AllErrorsS = STRING_AGG(MessageResult, CHAR(10)) 
				FROM @ErrorsTable;

				SELECT	'999' as CodeMessage, 
						 @AllErrorsS as Message, 
						 0 QuotationId,
						'' QuotationCode

				RETURN;
			END --hasta aqui
			

		--Se establece la entidad administradora particular de la orden de servicio
		UPDATE qsod SET qsod.HealthAdministratorId = sb.ParticularHealthAdministratorId
		FROM @QuotationServiceOrderDetail qsod
		JOIN Contract.CareGroup cg ON qsod.CareGroupId = cg.Id		
		JOIN Billing.SettingsBilling sb ON sb.IdOperatingUnit = @OperatingUnitId
		WHERE qsod.HealthAdministratorId IS NULL
			AND cg.CareGroupType = 3

		--Se establece la entidad administradora particular de la dispensacion
		UPDATE qpdd SET qpdd.HealthAdministratorId = sb.ParticularHealthAdministratorId
		FROM @QuotationPharmaceuticalDispensingDetail qpdd
		JOIN Contract.CareGroup cg ON qpdd.CareGroupId = cg.Id		
		JOIN Billing.SettingsBilling sb ON sb.IdOperatingUnit = @OperatingUnitId
		WHERE qpdd.HealthAdministratorId IS NULL
			AND cg.CareGroupType = 3

		--Si no viene el código se genera
		if @Code = '' or @Code is null
		begin
			--Consultamos si la secuencia es con O o OU
			declare @scope varchar(5) = ''
			declare @idSequenceDetail int
			declare @pattern varchar(300)
			declare @NextS int
			select @scope = Scope from Billing.BillingSequence
			where IdForm = '1038'

			if @scope = 'O' --Si el ambito es por organización
			begin
				select top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
				from Billing.BillingSequenceDetail bsd 
				inner join Billing.BillingSequence bs on bs.Id = bsd.IdSequenseBillingC
				inner join Common.Sequense cs on cs.Id = bsd.IdSequense
				where bs.IdForm = '1038'
				order by bsd.Next desc
			end
			else begin --Si el ambito es por unidad operativa
				select top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
				from Billing.BillingSequenceDetail bsd 
				inner join Billing.BillingSequence bs on bs.Id = bsd.IdSequenseBillingC
				inner join Common.Sequense cs on cs.Id = bsd.IdSequense
				where bs.IdForm = '1038' and bsd.IdOperatingUnit = @OperatingUnitId
				order by bsd.Next desc
			end
					
			if (@idSequenceDetail is null)
			Begin
				select 999 as CodeMessage, 'Secuencia no encontrada para generar la cotización' as Message, 0 QuotationId, '' QuotationCode
				return
			End

			select @Code = dbo.GetSequence('',@pattern,@NextS)
			update Billing.BillingSequenceDetail set [Next] += 1 where Id = @idSequenceDetail
		end

		--Si el tipo de cotización es intrahospitalario se obtiene el tercero de la admision
		if @QuotationType = 1
		begin
			declare @IPCODPACI varchar(20) = (select IPCODPACI from .ADINGRESO where NUMINGRES = @AdmissionNumber)
			set @ThirdPartyId = (select Id from Common.ThirdParty where Nit = @IPCODPACI)
		end

		if @Id = 0 or @Id is null --Se guarda la cabecera
		begin
			insert into [Billing].[Quotation]([Code], [DocumentDate], [QuotationType], [AdmissionNumber], [ThirdPartyId], [Description], [OperatingUnitId], [Status], 
			[CreationUser], [CreationDate], [ConfirmationUser], [ConfirmationDate])
			values(@Code, @DocumentDate, @QuotationType, @AdmissionNumber, @ThirdPartyId, @Description, @OperatingUnitId, @Status, 
			@UserCode, [Common].[GETDATE](), IIF(@Status = 2, @UserCode, null), IIF(@Status = 2, [Common].[GETDATE](), null))

			set @Id = SCOPE_IDENTITY()
		end
		else begin --Se actualiza la cabecera
			update [Billing].[Quotation] set [Code] = @Code, [DocumentDate] = @DocumentDate, [QuotationType] = @QuotationType, [AdmissionNumber] = @AdmissionNumber,
			[ThirdPartyId] = @ThirdPartyId, [OperatingUnitId] = @OperatingUnitId, [Status] = @Status, [ModificationUser] = @UserCode, [ModificationDate] = [Common].[GETDATE](),
			[AnnulmentUser] = IIF(@Status = 3, @UserCode, null), [AnnulmentDate] = IIF(@Status = 3, [Common].[GETDATE](), null), [Description] = @Description,
			[ConfirmationUser] = IIF(@Status = 2, @UserCode, null), [ConfirmationDate] = IIF(@Status = 2, [Common].[GETDATE](), null)
			where Id = @Id
		end
		
		--Variables para recorrer el detalle de ServiceOrderDetail
		declare @Rows int = 1, @RowId int = 0, @DetailId int, @QuotationId int, @CareGroupId int, @HealthAdministratorId int, @DetailThirdPartyId int, 
		@ServiceType tinyint, @RecordType tinyint, @CUPSEntityId int, @IPSServiceId int, @HospitalStayId int, @HospitalStayDetailId int, @ControlExternalConsultation tinyint, 
		@ControlExternalConsultationCode numeric(18,0), @CUPSAssociateService bit, @CodeAssociateService varchar(50), @IsPackage bit, @Packaging bit, @PackageServiceOrderDetailId int, 
		@LiquidationType tinyint, @Presentation tinyint, @ProductId int, @InvoicedQuantity int, @SupplyQuantity int, @DevolutionQuantity int, @RateManualSalePrice numeric(18,0), 
		@CostValue numeric(18,2), @ServiceDate datetime, @AuthorizationNumber varchar(20), @PerformsFunctionalUnitId int, @PerformsHealthProfessionalCode char(20), 
		@PerformsProfessionalSpecialty char(3), @PerformsHealthProfessionalThirdPartyId int, @BillingConceptId int, @CostCenterId int, @SettlementType tinyint, @IncludeServiceOrderDetailId int, 
		@RecoveryRatio numeric(5,2), @RateManualId int, @RateManualType tinyint, @RateManualDetailId int, @DefinitionRateDetailId int, @DefinitionRateDetailConditionId int, 
		@SubTotalSalesPrice numeric(18,2), @ThirdPartyDiscount numeric(18,0), @ThirdPartyDiscountPercentage numeric(5,2), @TotalSalesPrice numeric(18,2), @GrandTotalSalesPrice numeric(18,0), 
		@SurchargeApply bit, @SurgicalInterventionType tinyint, @SurgeryNumber tinyint, @IsFirstEvent bit, @IsAnnulled bit, @IsDelete bit, @IncomeMainAccountId int, 
		@ApplyRIAS bit, @RIASCupsId int, @CUPSEntityContractDescriptionId int,@EconomicActiviyId int

		while @Rows > 0
		begin
			select top 1 @RowId = RowId, @QuotationId = QuotationId, @DetailId = Id, @CareGroupId = CareGroupId, @HealthAdministratorId = HealthAdministratorId, 
			@DetailThirdPartyId = ThirdPartyId, @ServiceType = ServiceType, @RecordType = RecordType, @CUPSEntityId = CUPSEntityId, @IPSServiceId = IPSServiceId, 
			@HospitalStayId = HospitalStayId, @HospitalStayDetailId = HospitalStayDetailId, @ControlExternalConsultation = ControlExternalConsultation, 
			@ControlExternalConsultationCode = ControlExternalConsultationCode, @CUPSAssociateService = CUPSAssociateService, @CodeAssociateService = CodeAssociateService, 
			@IsPackage = IsPackage, @Packaging = Packaging, @PackageServiceOrderDetailId = PackageServiceOrderDetailId, @LiquidationType = LiquidationType, @Presentation = Presentation, 
			@ProductId = ProductId, @InvoicedQuantity = InvoicedQuantity, @SupplyQuantity = SupplyQuantity, @DevolutionQuantity = DevolutionQuantity, @RateManualSalePrice = RateManualSalePrice, 
			@CostValue = CostValue, @ServiceDate = ServiceDate, @AuthorizationNumber = AuthorizationNumber, @PerformsFunctionalUnitId = PerformsFunctionalUnitId, 
			@PerformsHealthProfessionalCode = PerformsHealthProfessionalCode, @PerformsProfessionalSpecialty = PerformsProfessionalSpecialty, 
			@PerformsHealthProfessionalThirdPartyId = PerformsHealthProfessionalThirdPartyId, @BillingConceptId = BillingConceptId, @CostCenterId = CostCenterId, 
			@SettlementType = SettlementType, @IncludeServiceOrderDetailId = IncludeServiceOrderDetailId, @RecoveryRatio = RecoveryRatio, @RateManualId = RateManualId, 
			@RateManualType = RateManualType, @RateManualDetailId = RateManualDetailId, @DefinitionRateDetailId = DefinitionRateDetailId, @DefinitionRateDetailConditionId = DefinitionRateDetailConditionId, 
			@SubTotalSalesPrice = SubTotalSalesPrice, @ThirdPartyDiscount = ThirdPartyDiscount, @ThirdPartyDiscountPercentage = ThirdPartyDiscountPercentage, 
			@TotalSalesPrice = TotalSalesPrice, @GrandTotalSalesPrice = GrandTotalSalesPrice, @SurchargeApply = SurchargeApply, @SurgicalInterventionType = SurgicalInterventionType, 
			@SurgeryNumber = SurgeryNumber, @IsFirstEvent = IsFirstEvent, @IsAnnulled = IsAnnulled, @IsDelete = IsDelete, @IncomeMainAccountId = IncomeMainAccountId, 
			@ApplyRIAS = ApplyRIAS, @RIASCupsId = RIASCupsId, @CUPSEntityContractDescriptionId = CUPSEntityContractDescriptionId, @EconomicActiviyId = EconomicActivityId
			from @QuotationServiceOrderDetail
			where RowId > @RowId 
			order by RowId

			set @Rows = @@RowCount
			if @Rows = 0 
				break

			if @DetailId = 0 --Se inserta el detalle
			begin
				insert into [Billing].[QuotationServiceOrderDetail]([QuotationId], [CareGroupId], [HealthAdministratorId], [ThirdPartyId], [ServiceType], [RecordType], [CUPSEntityId],
				[IPSServiceId], [HospitalStayId], [HospitalStayDetailId], [ControlExternalConsultation], [ControlExternalConsultationCode], [CUPSAssociateService], [CodeAssociateService],
				[IsPackage], [Packaging], [PackageServiceOrderDetailId], [LiquidationType], [Presentation], [ProductId], [InvoicedQuantity], [SupplyQuantity], [DevolutionQuantity],
				[RateManualSalePrice], [CostValue], [ServiceDate], [AuthorizationNumber], [PerformsFunctionalUnitId], [PerformsHealthProfessionalCode], [PerformsProfessionalSpecialty],
				[PerformsHealthProfessionalThirdPartyId], [BillingConceptId], [CostCenterId], [SettlementType], [IncludeServiceOrderDetailId], [RecoveryRatio], [RateManualId], 
				[RateManualType], [RateManualDetailId], [DefinitionRateDetailId], [DefinitionRateDetailConditionId], [SubTotalSalesPrice], [ThirdPartyDiscount], [ThirdPartyDiscountPercentage],
				[TotalSalesPrice], [GrandTotalSalesPrice], [SurchargeApply], [SurgicalInterventionType], [SurgeryNumber], [IsFirstEvent], [IsAnnulled], [IsDelete], [IncomeMainAccountId],
				[ApplyRIAS], [RIASCupsId], [CUPSEntityContractDescriptionId],[EconomicActivityId])
				values(@Id, @CareGroupId, @HealthAdministratorId, @DetailThirdPartyId, @ServiceType, @RecordType, @CUPSEntityId, @IPSServiceId, @HospitalStayId, @HospitalStayDetailId, 
				@ControlExternalConsultation, @ControlExternalConsultationCode, @CUPSAssociateService, @CodeAssociateService, @IsPackage, @Packaging, @PackageServiceOrderDetailId, 
				@LiquidationType, @Presentation, @ProductId, @InvoicedQuantity, @SupplyQuantity, @DevolutionQuantity, @RateManualSalePrice, @CostValue, @ServiceDate, @AuthorizationNumber, 
				@PerformsFunctionalUnitId, @PerformsHealthProfessionalCode, @PerformsProfessionalSpecialty, @PerformsHealthProfessionalThirdPartyId, @BillingConceptId, @CostCenterId, 
				@SettlementType, @IncludeServiceOrderDetailId, @RecoveryRatio, @RateManualId, @RateManualType, @RateManualDetailId, @DefinitionRateDetailId, @DefinitionRateDetailConditionId, 
				@SubTotalSalesPrice, @ThirdPartyDiscount, @ThirdPartyDiscountPercentage, @TotalSalesPrice, @GrandTotalSalesPrice, @SurchargeApply, @SurgicalInterventionType, @SurgeryNumber, 
				@IsFirstEvent, @IsAnnulled, @IsDelete, @IncomeMainAccountId, @ApplyRIAS, @RIASCupsId, @CUPSEntityContractDescriptionId,@EconomicActiviyId)

				set @DetailId = SCOPE_IDENTITY()
			end
			else begin --Se actualiza el detalle
				update [Billing].[QuotationServiceOrderDetail] set [QuotationId] = @QuotationId, [CareGroupId] = @CareGroupId, [HealthAdministratorId] = @HealthAdministratorId, 
				[ThirdPartyId] = @DetailThirdPartyId, [ServiceType] = @ServiceType, [RecordType] = @RecordType, [CUPSEntityId] = @CUPSEntityId, [IPSServiceId] = @IPSServiceId, 
				[HospitalStayId] = @HospitalStayId, [HospitalStayDetailId] = @HospitalStayDetailId, [ControlExternalConsultation] = @ControlExternalConsultation, 
				[ControlExternalConsultationCode] = @ControlExternalConsultationCode, [CUPSAssociateService] = @CUPSAssociateService, [CodeAssociateService] = @CodeAssociateService,
				[IsPackage] = @IsPackage, [Packaging] = @Packaging, [PackageServiceOrderDetailId] = @PackageServiceOrderDetailId, [LiquidationType] = @LiquidationType, [Presentation] = @Presentation,
				[ProductId] = @ProductId, [InvoicedQuantity] = @InvoicedQuantity, [SupplyQuantity] = @SupplyQuantity, [DevolutionQuantity] = @DevolutionQuantity, 
				[RateManualSalePrice] = @RateManualSalePrice, [CostValue] = @CostValue, [ServiceDate] = @ServiceDate, [AuthorizationNumber] = @AuthorizationNumber, 
				[PerformsFunctionalUnitId] = @PerformsFunctionalUnitId, [PerformsHealthProfessionalCode] = @PerformsHealthProfessionalCode, 
				[PerformsProfessionalSpecialty] = @PerformsProfessionalSpecialty, [PerformsHealthProfessionalThirdPartyId] = @PerformsHealthProfessionalThirdPartyId, [BillingConceptId] = @BillingConceptId, 
				[CostCenterId] = @CostCenterId, [SettlementType] = @SettlementType, [IncludeServiceOrderDetailId] = @IncludeServiceOrderDetailId, [RecoveryRatio] = @RecoveryRatio, 
				[RateManualId] = @RateManualId, [RateManualType] = @RateManualType, [RateManualDetailId] = @RateManualDetailId, [DefinitionRateDetailId] = @DefinitionRateDetailId, 
				[DefinitionRateDetailConditionId] = @DefinitionRateDetailConditionId, [SubTotalSalesPrice] = @SubTotalSalesPrice, [ThirdPartyDiscount] = @ThirdPartyDiscount, 
				[ThirdPartyDiscountPercentage] = @ThirdPartyDiscountPercentage, [TotalSalesPrice] = @TotalSalesPrice, [GrandTotalSalesPrice] = @GrandTotalSalesPrice, [SurchargeApply] = @SurchargeApply,
				[SurgicalInterventionType] = @SurgicalInterventionType, [SurgeryNumber] = @SurgeryNumber, [IsFirstEvent] = @IsFirstEvent, [IsAnnulled] = @IsAnnulled, [IsDelete] = @IsDelete, 
				[IncomeMainAccountId] = @IncomeMainAccountId, [ApplyRIAS] = @ApplyRIAS, [RIASCupsId] = @RIASCupsId, [CUPSEntityContractDescriptionId] = @CUPSEntityContractDescriptionId,[EconomicActivityId] = @EconomicActiviyId
				where Id = @DetailId
			end

			--Se insertan las distribuciones
			insert into [Billing].[QuotationServiceOrderDetailDistribution]([QuotationServiceOrderDetailId], [Quantity], [GrandTotalSalesPrice], [GrandTotalDiscount], [DistributionType], 
			[ThirdPartySalesPrice], [ThirdPartyPercentage], [ApplyRecoveryFee], [RecoveryFeeType], [SubTotalPatientSalesPrice], [PatientPercentage], [LastCaregroupId])
			select @DetailId, Quantity, GrandTotalSalesPrice, GrandTotalDiscount, DistributionType, ThirdPartySalesPrice, ThirdPartyPercentage, ApplyRecoveryFee, RecoveryFeeType, SubTotalPatientSalesPrice, 
			PatientPercentage, LastCaregroupId
			from @QuotationServiceOrderDetailDistribution
			where Id = 0 AND RowId = @RowId

			--Se actualizan las distribuciones
			update qd set qd.QuotationServiceOrderDetailId = qdTemp.QuotationServiceOrderDetailId, qd.Quantity = qdTemp.Quantity, qd.GrandTotalSalesPrice = qdTemp.GrandTotalSalesPrice, 
			qd.GrandTotalDiscount = qdTemp.GrandTotalDiscount, qd.DistributionType = qdTemp.DistributionType, qd.ThirdPartySalesPrice = qdTemp.ThirdPartySalesPrice, 
			qd.ThirdPartyPercentage = qdTemp.ThirdPartyPercentage, qd.ApplyRecoveryFee = qdTemp.ApplyRecoveryFee, qd.RecoveryFeeType = qdTemp.RecoveryFeeType, 
			qd.SubTotalPatientSalesPrice = qdTemp.SubTotalPatientSalesPrice, qd.PatientPercentage = qdTemp.PatientPercentage, qd.LastCaregroupId = qdTemp.LastCaregroupId
			from @QuotationServiceOrderDetailDistribution qdTemp
			inner join Billing.QuotationServiceOrderDetailDistribution qd on qd.Id = qdTemp.Id
			where qdTemp.Id > 0 AND RowId = @RowId

			--Se insertan los qx
			insert into [Billing].[QuotationServiceOrderDetailSurgical]([QuotationServiceOrderDetailId], [IPSServiceId], [InvoicedQuantity], [LiquidationPercentage], [RateManualSalePrice],
			[TotalSalesPrice], [PerformsHealthProfessionalCode], [PerformsHealthProfessionalThirdPartyId], [CostValue], [BillingConceptId], [CostCenterId], [RateManualDetailSurgicalId],
			[SurchargeApply], [OnlyMedicalFees], [IncomeMainAccountId],[EconomicActivityId])
			select @DetailId, IPSServiceId, InvoicedQuantity, LiquidationPercentage, RateManualSalePrice, TotalSalesPrice, PerformsHealthProfessionalCode, PerformsHealthProfessionalThirdPartyId, 
			CostValue, BillingConceptId, CostCenterId, RateManualDetailSurgicalId, SurchargeApply, OnlyMedicalFees, IncomeMainAccountId,EconomicActivityId		
			from @QuotationServiceOrderDetailSurgical
			where Id = 0 AND RowId = @RowId

			--Se actualizan los qx
			update qs set qs.QuotationServiceOrderDetailId = qsTemp.QuotationServiceOrderDetailId, qs.IPSServiceId = qsTemp.IPSServiceId,
			qs.InvoicedQuantity = qsTemp.InvoicedQuantity, qs.LiquidationPercentage = qsTemp.LiquidationPercentage, qs.RateManualSalePrice = qsTemp.RateManualSalePrice, 
			qs.TotalSalesPrice = qsTemp.TotalSalesPrice, qs.PerformsHealthProfessionalCode = qsTemp.PerformsHealthProfessionalCode, 
			qs.PerformsHealthProfessionalThirdPartyId = qsTemp.PerformsHealthProfessionalThirdPartyId, qs.CostValue = qsTemp.CostValue, qs.BillingConceptId = qsTemp.BillingConceptId, 
			qs.CostCenterId = qsTemp.CostCenterId, qs.RateManualDetailSurgicalId = qsTemp.RateManualDetailSurgicalId, qs.SurchargeApply = qsTemp.SurchargeApply, 
			qs.OnlyMedicalFees = qsTemp.OnlyMedicalFees, qs.IncomeMainAccountId = qsTemp.IncomeMainAccountId,qs.EconomicActivityId = qsTemp.EconomicActivityId
			from @QuotationServiceOrderDetailSurgical qsTemp
			inner join Billing.QuotationServiceOrderDetailSurgical qs on qs.Id = qsTemp.Id
			where qsTemp.Id > 0 AND RowId = @RowId
		end

		--Se inicializa las variables para recorrer el otro detalle
		set @Rows = 1 
		set @RowId = 0

		--Variables para recorrer el detalle de PharmaceuticalDispensingDetail, se declaran algunos campos ya que arriba en el primer while se declaran los otros
		declare @WarehouseId int, @Quantity int, @ReturnedQuantity int, @FunctionalUnitId int, @OrderedHealthProfessionalCode char(20), @OrderedProfessionalSpecialty char(3), 
		@OrderedHealthProfessionalThirdPartyId int, @SalePrice numeric(20,4), @AverageCost numeric(20,4), @DiscountPercentage numeric(5,2), @DiscountValue numeric(20,4),@EconomicActiviyIdP Int

		while @Rows > 0
		begin
			select top 1 @RowId = RowId, @DetailId = Id, @QuotationId = QuotationId, @CareGroupId = CareGroupId, @HealthAdministratorId = HealthAdministratorId, @DetailThirdPartyId = ThirdPartyId,
			@ProductId = ProductId, @WarehouseId = WarehouseId, @Quantity = Quantity, @ReturnedQuantity = ReturnedQuantity, @ServiceDate = ServiceDate, @FunctionalUnitId = FunctionalUnitId,
			@OrderedHealthProfessionalCode = OrderedHealthProfessionalCode, @OrderedProfessionalSpecialty = OrderedProfessionalSpecialty, 
			@OrderedHealthProfessionalThirdPartyId = OrderedHealthProfessionalThirdPartyId, @AuthorizationNumber = AuthorizationNumber, @LiquidationType = LiquidationType, @CupsEntityId = CupsEntityId,
			@SurchargeApply = SurchargeApply, @SalePrice = SalePrice, @AverageCost = AverageCost, @DiscountPercentage = DiscountPercentage, @DiscountValue = DiscountValue, 
			@TotalSalesPrice = TotalSalesPrice, @GrandTotalSalesPrice = GrandTotalSalesPrice, @EconomicActiviyIdP = EconomicActivityId
			from @QuotationPharmaceuticalDispensingDetail
			where RowId > @RowId 
			order by RowId

			set @Rows = @@RowCount
			if @Rows = 0 
				break

			if @DetailId = 0 --Se inserta el detalle
			begin
				insert into [Billing].[QuotationPharmaceuticalDispensingDetail]([QuotationId], [CareGroupId], [HealthAdministratorId], [ThirdPartyId], [ProductId], [WarehouseId], [Quantity],
				[ReturnedQuantity], [ServiceDate], [FunctionalUnitId], [OrderedHealthProfessionalCode], [OrderedProfessionalSpecialty], [OrderedHealthProfessionalThirdPartyId], [AuthorizationNumber],
				[LiquidationType], [CupsEntityId], [SurchargeApply], [SalePrice], [AverageCost], [DiscountPercentage], [DiscountValue], [TotalSalesPrice], [GrandTotalSalesPrice],[EconomicActivityId])
				values(@Id, @CareGroupId, @HealthAdministratorId, @DetailThirdPartyId, @ProductId, @WarehouseId, @Quantity, @ReturnedQuantity, @ServiceDate, @FunctionalUnitId, 
				@OrderedHealthProfessionalCode, @OrderedProfessionalSpecialty, @OrderedHealthProfessionalThirdPartyId, @AuthorizationNumber, @LiquidationType, @CupsEntityId, @SurchargeApply, 
				@SalePrice, @AverageCost, @DiscountPercentage, @DiscountValue, @TotalSalesPrice, @GrandTotalSalesPrice,@EconomicActiviyIdP)

				set @DetailId = SCOPE_IDENTITY()
			end
			else begin --Se actualiza el detalle
				update [Billing].[QuotationPharmaceuticalDispensingDetail] set QuotationId = @QuotationId, CareGroupId = @CareGroupId, HealthAdministratorId = @HealthAdministratorId, 
				ThirdPartyId = @DetailThirdPartyId, ProductId = @ProductId, WarehouseId = @WarehouseId, Quantity = @Quantity, ReturnedQuantity = @ReturnedQuantity, 
				ServiceDate = @ServiceDate, FunctionalUnitId = @FunctionalUnitId, OrderedHealthProfessionalCode = @OrderedHealthProfessionalCode, 
				OrderedProfessionalSpecialty = @OrderedProfessionalSpecialty, OrderedHealthProfessionalThirdPartyId = @OrderedHealthProfessionalThirdPartyId, 
				AuthorizationNumber = @AuthorizationNumber, LiquidationType = @LiquidationType, CupsEntityId = @CupsEntityId, SurchargeApply = @SurchargeApply, 
				SalePrice = @SalePrice, AverageCost = @AverageCost, DiscountPercentage = @DiscountPercentage, DiscountValue = @DiscountValue, TotalSalesPrice = @TotalSalesPrice, 
				GrandTotalSalesPrice = @GrandTotalSalesPrice, EconomicActivityId = @EconomicActiviyIdP
				where Id = @DetailId
			end

			--Se insertan los lotes
			insert into [Billing].[QuotationPharmaceuticalDispensingDetailBatchSerial]([QuotationPharmaceuticalDispensingDetailId], [PhysicalInventoryId], [Quantity], 
			[OutstandingQuantity], [PhysicalInventoryCustodyId])
			select @DetailId, PhysicalInventoryId, Quantity, OutstandingQuantity, PhysicalInventoryCustodyId
			from @QuotationPharmaceuticalDispensingDetailBatchSerial
			where Id = 0 AND RowId = @RowId

			--Se actualizan los lotes
			update bs set bs.QuotationPharmaceuticalDispensingDetailId = bsTemp.QuotationPharmaceuticalDispensingDetailId, bs.PhysicalInventoryId = bsTemp.PhysicalInventoryId, 
			bs.Quantity = bsTemp.Quantity, bs.OutstandingQuantity = bsTemp.OutstandingQuantity, bs.PhysicalInventoryCustodyId = bsTemp.PhysicalInventoryCustodyId
			from @QuotationPharmaceuticalDispensingDetailBatchSerial bsTemp
			inner join Billing.QuotationPharmaceuticalDispensingDetailBatchSerial bs on bs.Id = bsTemp.Id
			where bsTemp.Id > 0 AND RowId = @RowId
		end
		--SELECT * FROM @QuotationServiceOrderDetail--BORRAR
		--SELECT * FROM @QuotationServiceOrderDetailSurgical--BORRAR
		--SELECT * FROM @QuotationPharmaceuticalDispensingDetail--BORRAR
		--SELECT * FROM @ErrorsTable
		--select top 10* from [Billing].[QuotationServiceOrderDetail] order by id desc
		--select top 10* from [Billing].[QuotationPharmaceuticalDispensingDetail] order by id desc

		--Se asigna el mensaje a retornar
		set @MessageReturn = case @Status 
								when 2 then 'Se guardó y se confirmó la cotización con código ' + @Code 
								when 3 then 'Se anuló la cotización con código ' + @Code 
								else 'Se guardó la cotización con código ' + @Code 
							 end 

		--Se retorna el ok
		select 0 as CodeMessage, @MessageReturn as Message, @Id QuotationId, @Code QuotationCode
		return

	End try
	Begin Catch

		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message, 0 QuotationId, '' QuotationCode
		return

	End Catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que gestiona el ciclo completo de cotizaciones de facturación para pacientes: crea, actualiza, confirma, desconfirma y anula cotizaciones. Procesa el encabezado de la cotización junto con sus líneas de detalle de servicios (procedimientos, medicamentos, estancias hospitalarias, cirugías) y la distribución financiera entre el tercero pagador (EPS, aseguradora) y el paciente, recibiendo toda la información en formato XML. Interactúa con las tablas de ingresos de pacientes (admisiones y urgencias), el dashboard de cotizaciones, el detalle quirúrgico de servicios y la configuración contable de la empresa para registrar la actividad económica. Es el núcleo del proceso de cotización previa a la facturación, permitiendo calcular y guardar los valores de venta, descuentos, tarifas y cuotas moderadoras antes de generar una factura formal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_SaveQuotation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_SaveQuotation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (crea/actualiza/anula/desconfirma) una cotización de facturación con sus detalles de órdenes de servicio, distribuciones, quirúrgicos, dispensación farmacéutica y lotes, validando actividad económica generadora de ingreso cuando aplica.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveQuotation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener un nodo /Quotation con los datos de cabecera (Id, Code, DocumentDate, QuotationType, OperatingUnitId, Status).; Para desconfirmar (Status=0) ningún detalle de la cotización debe estar asociado a una Billing.ServiceOrderDetail vía QuotationServiceOrderDetailId.; Para generar el código automático debe existir configuración en Billing.BillingSequence para IdForm=''1038'' y un Billing.BillingSequenceDetail asociado (por organización o por unidad operativa según Scope).; Si el tipo de cotización es intrahospitalario (QuotationType=1) debe existir el ingreso en .ADINGRESO con NUMINGRES=@AdmissionNumber y un Common.ThirdParty cuyo Nit coincida con IPCODPACI.; Si GeneralLedger.CompanySettings.TransactionEconomicActivity = 1, todos los productos dispensados deben tener Inventory.ProductGroup.EconomicActivityId definido y todos los servicios deben tener Billing.BillingConcept.EconomicActivityId definido.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveQuotation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Billing.DashboardQuoted: Cuando @Status=3 (anulación), se limpia (QuotationId=null) en DashboardQuoted donde QuotationId=@Id antes de anular la cotización.; [UPDATE] Billing.Quotation: Cuando @Status=3 se marca Status=3 (anulada); cuando @Status=0 se vuelve a Status=1 (desconfirmada); en update normal se setean AnnulmentUser/Date sólo si Status=3 y ConfirmationUser/Date sólo si Status=2.; [INSERT] Billing.Quotation: Si @Id es 0 o null se crea la cabecera con CreationUser=@UserCode y, si Status=2, también ConfirmationUser/ConfirmationDate; en otro caso quedan en null.; [DELETE] Billing.QuotationServiceOrderDetail: Se eliminan filas cuyos Id están en el XML con ItemDelete=1 e Id>0 (idem para Surgical, Distribution, PharmaceuticalDispensingDetail y BatchSerial, en orden hijo→padre).; [INSERT] Billing.QuotationServiceOrderDetail: Por cada fila del XML con Id=0 se inserta un detalle nuevo asociado al Id de la cotización; si Id>0 se actualiza la fila existente.; [INSERT] Billing.QuotationServiceOrderDetailDistribution: Por cada distribución del XML con Id=0 y RowId del detalle padre se inserta vinculada al @DetailId recién insertado/actualizado; si Id>0 se actualiza.; [INSERT] Billing.QuotationServiceOrderDetailSurgical: Por cada quirúrgico del XML con Id=0 y RowId del detalle padre se inserta vinculado al detalle de servicio; si Id>0 se actualiza.; [INSERT] Billing.QuotationPharmaceuticalDispensingDetail: Por cada dispensación del XML con Id=0 se inserta nueva; si Id>0 se actualiza con los nuevos valores incluyendo EconomicActivityId.; [INSERT] Billing.QuotationPharmaceuticalDispensingDetailBatchSerial: Por cada lote/serie del XML con Id=0 se inserta vinculado al detalle de dispensación; si Id>0 se actualiza.; [UPDATE] Billing.BillingSequenceDetail: Cuando se genera el código automáticamente, se incrementa Next en 1 sobre el detalle de secuencia usado (Id=@idSequenceDetail).; [UPDATE] Billing.QuotationServiceOrderDetail: Para detalles de CareGroup con CareGroupType=3 y HealthAdministratorId nulo, se asigna sb.ParticularHealthAdministratorId desde Billing.SettingsBilling de la unidad operativa.; [UPDATE] Billing.QuotationPharmaceuticalDispensingDetail: Para dispensaciones de CareGroup con CareGroupType=3 y HealthAdministratorId nulo, se asigna sb.ParticularHealthAdministratorId desde Billing.SettingsBilling de la unidad operativa.; [UPDATE] Billing.QuotationServiceOrderDetail: Si @IsEconomicActivity=1, EconomicActivityId del detalle se toma del BillingConcept asociado (BC.EconomicActivityId) antes de persistir.; [UPDATE] Billing.QuotationServiceOrderDetailSurgical: Si @IsEconomicActivity=1, EconomicActivityId del quirúrgico se toma del BillingConcept asociado.; [UPDATE] Billing.QuotationPharmaceuticalDispensingDetail: Si @IsEconomicActivity=1, EconomicActivityId del producto dispensado se toma del ProductGroup del producto.; [RETURN_RESULT] Billing.Quotation: Devuelve un único resultset con CodeMessage (0 ok / 999 error), Message descriptivo, QuotationId y QuotationCode; en CATCH devuelve 999 con ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveQuotation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveQuotation';
-- GO
