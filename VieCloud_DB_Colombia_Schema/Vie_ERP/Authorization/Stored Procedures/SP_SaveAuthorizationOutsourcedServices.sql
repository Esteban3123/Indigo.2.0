

-- ===============================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 13/07/2020
-- Description:	Procedimiento que se encarga del proceso de autorización de servicios tercerizados
-- ===============================================================================================================================
CREATE PROCEDURE [Authorization].[SP_SaveAuthorizationOutsourcedServices]
	@Xml xml,
	@UserCode varchar(20)
AS
BEGIN
	
	--Variables para guardar la cabecera
	declare @Id int, @Code varchar(20), @DocumentDate datetime, @Type tinyint, @AdmissionNumber varchar(10), @ThirdPartyId int, @OperatingUnitId int, @Status tinyint, @Description varchar(max)

	--Tabla de detalles
	declare @AuthorizationOutsourcedServicesServiceOrderDetail table(RowId int, Id int, AuthorizationOutsourcedServicesId int, CareGroupId int, HealthAdministratorId int, ThirdPartyId int, ServiceType tinyint, 
	RecordType tinyint, CUPSEntityId int, 
	IPSServiceId int, HospitalStayId int, HospitalStayDetailId int, ControlExternalConsultation tinyint, ControlExternalConsultationCode numeric(18,0), CUPSAssociateService bit, 
	CodeAssociateService varchar(50), IsPackage bit, Packaging bit, PackageServiceOrderDetailId int, LiquidationType tinyint, Presentation tinyint, ProductId int, InvoicedQuantity int, 
	SupplyQuantity int, DevolutionQuantity int, RateManualSalePrice numeric(18,0), CostValue numeric(18,2), ServiceDate datetime, AuthorizationNumber varchar(20), PerformsFunctionalUnitId int, 
	PerformsHealthProfessionalCode char(20), PerformsProfessionalSpecialty char(3), PerformsHealthProfessionalThirdPartyId int, BillingConceptId int, CostCenterId int, SettlementType tinyint, 
	IncludeServiceOrderDetailId int, RecoveryRatio numeric(5,2), RateManualId int, RateManualType tinyint, RateManualDetailId int, DefinitionRateDetailId int, DefinitionRateDetailConditionId int, 
	SubTotalSalesPrice numeric(18,2), ThirdPartyDiscount numeric(18,0), ThirdPartyDiscountPercentage numeric(5,2), TotalSalesPrice numeric(18,2), GrandTotalSalesPrice numeric(18,0), 
	SurchargeApply bit, SurgicalInterventionType tinyint, SurgeryNumber tinyint, IsFirstEvent bit, IsAnnulled bit, IsDelete bit, IncomeMainAccountId int, 
	ApplyRIAS bit, RIASCupsId int, CUPSEntityContractDescriptionId int, ItemDelete bit)
	
	--Tabla de detalles qx 
	declare @AuthorizationOutsourcedServicesServiceOrderDetailSurgical table(RowId int, Id int, AuthorizationOutsourcedServicesServiceOrderDetailId int, IPSServiceId int, 
	InvoicedQuantity int, LiquidationPercentage numeric(5,2), 
	RateManualSalePrice numeric(18,0), TotalSalesPrice numeric(18,0), PerformsHealthProfessionalCode char(20), PerformsHealthProfessionalThirdPartyId int, CostValue numeric(18,2), 
	BillingConceptId int, CostCenterId int, RateManualDetailSurgicalId int, SurchargeApply bit, OnlyMedicalFees bit, IncomeMainAccountId int, ItemDelete bit)

	--Tabla de detalles de dispensación
	declare @AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail table(RowId int, Id int, AuthorizationOutsourcedServicesId int, CareGroupId int, 
	HealthAdministratorId int, ThirdPartyId int, ProductId int, WarehouseId int, 
	Quantity int, ReturnedQuantity int, ServiceDate datetime, FunctionalUnitId int, OrderedHealthProfessionalCode char(20), OrderedProfessionalSpecialty char(3), 
	OrderedHealthProfessionalThirdPartyId int, AuthorizationNumber varchar(20), LiquidationType tinyint, CupsEntityId int, SurchargeApply bit, SalePrice numeric(20,4), 
	AverageCost numeric(20,4), DiscountPercentage numeric(5,2), DiscountValue numeric(20,4), TotalSalesPrice numeric(20,4), GrandTotalSalesPrice numeric(20,4), ItemDelete bit)
	
	--Tabla en donde se almacena los ids de los trámites seleccionados para poder asociar la autorización de servicios tercerizados
	declare @TableTraceabilityPaperworkIds table(TraceabilityPaperworkId int)

	--Variable en donde se almacenan los mensajes que se va a devolver
	declare @MessageReturn varchar(max)

	Begin try
		
		--Se obtienen los datos para la cabecera
		select 
			@Id = t.x.value('Id[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@DocumentDate = convert(date, t.x.value('DocumentDate[1]','varchar(20)'), 103),
			@Type = t.x.value('Type[1]','tinyint'),
			@AdmissionNumber = IIF(t.x.value('AdmissionNumber[1]','varchar(10)') = '', null, t.x.value('AdmissionNumber[1]','varchar(10)')),
			@ThirdPartyId = IIF(t.x.value('ThirdPartyId[1]','varchar(20)') = '', null, t.x.value('ThirdPartyId[1]','varchar(20)')),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@Status = t.x.value('Status[1]','tinyint'),
			@Description = IIF(t.x.value('Description[1]','varchar(max)') = '', null, t.x.value('Description[1]','varchar(max)'))
		from @Xml.nodes('/AuthorizationOutsourcedServices') t(x)
		
		--Si se esta anulando
		if @Status = 3
		begin
			update [Authorization].AuthorizationOutsourcedServices set Status = 3 where Id = @Id
			select 0 as CodeMessage, 'El registro con código ' + @Code + ' se anuló correctamente' as Message, @Id AuthorizationOutsourcedServicesId, @Code AuthorizationOutsourcedServicesCode
			return
		end
		
		--Si se esta desconfirmando
		if @Status = 0
		begin
			----Se valida que no haya items del detalle de la cotización que este asociada a una orden de servicio
			--if exists(select 1
			--from Billing.Quotation q
			--inner join Billing.QuotationServiceOrderDetail qsod on qsod.QuotationId = q.Id
			--inner join Billing.ServiceOrderDetail sod on sod.QuotationServiceOrderDetailId = qsod.Id
			--where q.Id = @Id)
			--begin
			--	select 999 as CodeMessage, 'No se puede desconfirmar porque hay detalles que ya están asociados a una orden de servicio' as Message, 0 AuthorizationOutsourcedServicesId, '' AuthorizationOutsourcedServicesCode
			--	return
			--end

			update [Authorization].AuthorizationOutsourcedServices set Status = 1 where Id = @Id
			select 0 as CodeMessage, 'El registro con código ' + @Code + ' se desconfirmó correctamente' as Message, @Id AuthorizationOutsourcedServicesId, @Code AuthorizationOutsourcedServicesCode
			return
		end

		--Se obtienen los detalles de ServiceOrderDetail del xml
		insert into @AuthorizationOutsourcedServicesServiceOrderDetail
		select 
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('AuthorizationOutsourcedServicesId[1]','int') as AuthorizationOutsourcedServicesId,
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
			t.x.value('ItemDelete[1]','bit') as ItemDelete
		from @Xml.nodes('/AuthorizationOutsourcedServices/AuthorizationOutsourcedServicesServiceOrderDetail') t(x)
		
		--Se obtienen los detalles de ServiceOrderDetailSurgical del xml
		insert into @AuthorizationOutsourcedServicesServiceOrderDetailSurgical
		select 
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('AuthorizationOutsourcedServicesServiceOrderDetailId[1]','int') as AuthorizationOutsourcedServicesServiceOrderDetailId,
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
			t.x.value('ItemDelete[1]','bit') as ItemDelete
		from @Xml.nodes('/AuthorizationOutsourcedServices/AuthorizationOutsourcedServicesServiceOrderDetail/AuthorizationOutsourcedServicesServiceOrderDetailSurgical') t(x)

		--Se obtienen los detalles de PharmaceuticalDispensingDetail del xml
		insert into @AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail
		select 
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('AuthorizationOutsourcedServicesId[1]','int') as AuthorizationOutsourcedServicesId,
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
			t.x.value('ItemDelete[1]','bit') as ItemDelete
		from @Xml.nodes('/AuthorizationOutsourcedServices/AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail') t(x)

		--Se obtienen los ids de los trámites del xml
		insert into @TableTraceabilityPaperworkIds
		select 
			t.x.value('TraceabilityPaperworkId[1]','int') as TraceabilityPaperworkId
		from @Xml.nodes('/AuthorizationOutsourcedServices/TraceabilityPaperwork') t(x)

		--Se eliminan los detalles
		delete from [Authorization].AuthorizationOutsourcedServicesServiceOrderDetailSurgical where Id in (select Id from @AuthorizationOutsourcedServicesServiceOrderDetailSurgical where ItemDelete = 1 and Id > 0)
		delete from @AuthorizationOutsourcedServicesServiceOrderDetailSurgical where ItemDelete = 1 and Id > 0
		
		delete from [Authorization].AuthorizationOutsourcedServicesServiceOrderDetail where Id in (select Id from @AuthorizationOutsourcedServicesServiceOrderDetail where ItemDelete = 1 and Id > 0)
		delete from @AuthorizationOutsourcedServicesServiceOrderDetail where ItemDelete = 1 and Id > 0
		
		delete from [Authorization].AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail where Id in (select Id from @AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail where ItemDelete = 1 and Id > 0)
		delete from @AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail where ItemDelete = 1 and Id > 0

		--Si no viene el código se genera
		if @Code = '' or @Code is null
		begin
			--Consultamos si la secuencia es con O o OU
			declare @scope varchar(5) = ''
			declare @idSequenceDetail int
			declare @pattern varchar(300)
			declare @NextS int
			select @scope = Scope from [Authorization].AuthorizationSequence
			where IdForm = '2183'

			if @scope = 'O' --Si el ambito es por organización
			begin
				select top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
				from [Authorization].AuthorizationSequenceDetail bsd 
				inner join [Authorization].AuthorizationSequence bs on bs.Id = bsd.IdSequenseAuthorizationC
				inner join Common.Sequense cs on cs.Id = bsd.IdSequense
				where bs.IdForm = '2183'
				order by bsd.Next desc
			end
			else begin --Si el ambito es por unidad operativa
				select top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
				from [Authorization].AuthorizationSequenceDetail bsd 
				inner join [Authorization].AuthorizationSequence bs on bs.Id = bsd.IdSequenseAuthorizationC
				inner join Common.Sequense cs on cs.Id = bsd.IdSequense
				where bs.IdForm = '2183' and bsd.IdOperatingUnit = @OperatingUnitId
				order by bsd.Next desc
			end
					
			if (@idSequenceDetail is null)
			Begin
				select 999 as CodeMessage, 'Secuencia no encontrada para generar la autorización de servicio tercerizado' as Message, 0 AuthorizationOutsourcedServicesId, '' AuthorizationOutsourcedServicesCode
				return
			End

			select @Code = dbo.GetSequence('',@pattern,@NextS)
			update [Authorization].AuthorizationSequenceDetail set [Next] += 1 where Id = @idSequenceDetail
		end

		--Si el tipo de cotización es intrahospitalario se obtiene el tercero de la admision
		if @Type = 1
		begin
			declare @IPCODPACI varchar(20) = (select IPCODPACI from .ADINGRESO where NUMINGRES = @AdmissionNumber)
			set @ThirdPartyId = (select Id from Common.ThirdParty where Nit = @IPCODPACI)
		end

		if @Id = 0 or @Id is null --Se guarda la cabecera
		begin
			insert into [Authorization].AuthorizationOutsourcedServices([Code], [DocumentDate], [Type], [AdmissionNumber], [ThirdPartyId], [Description], [OperatingUnitId], [Status], 
			[CreationUser], [CreationDate], [ConfirmationUser], [ConfirmationDate])
			values(@Code, @DocumentDate, @Type, @AdmissionNumber, @ThirdPartyId, @Description, @OperatingUnitId, @Status, 
			@UserCode, [Common].[GETDATE](), IIF(@Status = 2, @UserCode, null), IIF(@Status = 2, [Common].[GETDATE](), null))

			set @Id = SCOPE_IDENTITY()
		end
		else begin --Se actualiza la cabecera
			update [Authorization].AuthorizationOutsourcedServices set [Code] = @Code, [DocumentDate] = @DocumentDate, [Type] = @Type, [AdmissionNumber] = @AdmissionNumber,
			[ThirdPartyId] = @ThirdPartyId, [OperatingUnitId] = @OperatingUnitId, [Status] = @Status, [ModificationUser] = @UserCode, [ModificationDate] = [Common].[GETDATE](),
			[AnnulmentUser] = IIF(@Status = 3, @UserCode, null), [AnnulmentDate] = IIF(@Status = 3, [Common].[GETDATE](), null), [Description] = @Description,
			[ConfirmationUser] = IIF(@Status = 2, @UserCode, null), [ConfirmationDate] = IIF(@Status = 2, [Common].[GETDATE](), null)
			where Id = @Id
		end
		
		--Variables para recorrer el detalle de ServiceOrderDetail
		declare @Rows int = 1, @RowId int = 0, @DetailId int, @AuthorizationOutsourcedServicesId int, @CareGroupId int, @HealthAdministratorId int, @DetailThirdPartyId int, 
		@ServiceType tinyint, @RecordType tinyint, @CUPSEntityId int, @IPSServiceId int, @HospitalStayId int, @HospitalStayDetailId int, @ControlExternalConsultation tinyint, 
		@ControlExternalConsultationCode numeric(18,0), @CUPSAssociateService bit, @CodeAssociateService varchar(50), @IsPackage bit, @Packaging bit, @PackageServiceOrderDetailId int, 
		@LiquidationType tinyint, @Presentation tinyint, @ProductId int, @InvoicedQuantity int, @SupplyQuantity int, @DevolutionQuantity int, @RateManualSalePrice numeric(18,0), 
		@CostValue numeric(18,2), @ServiceDate datetime, @AuthorizationNumber varchar(20), @PerformsFunctionalUnitId int, @PerformsHealthProfessionalCode char(20), 
		@PerformsProfessionalSpecialty char(3), @PerformsHealthProfessionalThirdPartyId int, @BillingConceptId int, @CostCenterId int, @SettlementType tinyint, @IncludeServiceOrderDetailId int, 
		@RecoveryRatio numeric(5,2), @RateManualId int, @RateManualType tinyint, @RateManualDetailId int, @DefinitionRateDetailId int, @DefinitionRateDetailConditionId int, 
		@SubTotalSalesPrice numeric(18,2), @ThirdPartyDiscount numeric(18,0), @ThirdPartyDiscountPercentage numeric(5,2), @TotalSalesPrice numeric(18,2), @GrandTotalSalesPrice numeric(18,0), 
		@SurchargeApply bit, @SurgicalInterventionType tinyint, @SurgeryNumber tinyint, @IsFirstEvent bit, @IsAnnulled bit, @IsDelete bit, @IncomeMainAccountId int, 
		@ApplyRIAS bit, @RIASCupsId int, @CUPSEntityContractDescriptionId int

		while @Rows > 0
		begin
			select top 1 @RowId = RowId, @AuthorizationOutsourcedServicesId = AuthorizationOutsourcedServicesId, @DetailId = Id, @CareGroupId = CareGroupId, @HealthAdministratorId = HealthAdministratorId, 
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
			@ApplyRIAS = ApplyRIAS, @RIASCupsId = RIASCupsId, @CUPSEntityContractDescriptionId = CUPSEntityContractDescriptionId
			from @AuthorizationOutsourcedServicesServiceOrderDetail
			where RowId > @RowId 
			order by RowId

			set @Rows = @@RowCount
			if @Rows = 0 
				break

			if @DetailId = 0 --Se inserta el detalle
			begin
				insert into [Authorization].AuthorizationOutsourcedServicesServiceOrderDetail([AuthorizationOutsourcedServicesId], [CareGroupId], [HealthAdministratorId], 
				[ThirdPartyId], [ServiceType], [RecordType], [CUPSEntityId],
				[IPSServiceId], [HospitalStayId], [HospitalStayDetailId], [ControlExternalConsultation], [ControlExternalConsultationCode], [CUPSAssociateService], [CodeAssociateService],
				[IsPackage], [Packaging], [PackageServiceOrderDetailId], [LiquidationType], [Presentation], [ProductId], [InvoicedQuantity], [SupplyQuantity], [DevolutionQuantity],
				[RateManualSalePrice], [CostValue], [ServiceDate], [AuthorizationNumber], [PerformsFunctionalUnitId], [PerformsHealthProfessionalCode], [PerformsProfessionalSpecialty],
				[PerformsHealthProfessionalThirdPartyId], [BillingConceptId], [CostCenterId], [SettlementType], [IncludeServiceOrderDetailId], [RecoveryRatio], [RateManualId], 
				[RateManualType], [RateManualDetailId], [DefinitionRateDetailId], [DefinitionRateDetailConditionId], [SubTotalSalesPrice], [ThirdPartyDiscount], [ThirdPartyDiscountPercentage],
				[TotalSalesPrice], [GrandTotalSalesPrice], [SurchargeApply], [SurgicalInterventionType], [SurgeryNumber], [IsFirstEvent], [IsAnnulled], [IsDelete], [IncomeMainAccountId],
				[ApplyRIAS], [RIASCupsId], [CUPSEntityContractDescriptionId])
				values(@Id, @CareGroupId, @HealthAdministratorId, @DetailThirdPartyId, @ServiceType, @RecordType, @CUPSEntityId, @IPSServiceId, @HospitalStayId, @HospitalStayDetailId, 
				@ControlExternalConsultation, @ControlExternalConsultationCode, @CUPSAssociateService, @CodeAssociateService, @IsPackage, @Packaging, @PackageServiceOrderDetailId, 
				@LiquidationType, @Presentation, @ProductId, @InvoicedQuantity, @SupplyQuantity, @DevolutionQuantity, @RateManualSalePrice, @CostValue, @ServiceDate, @AuthorizationNumber, 
				@PerformsFunctionalUnitId, @PerformsHealthProfessionalCode, @PerformsProfessionalSpecialty, @PerformsHealthProfessionalThirdPartyId, @BillingConceptId, @CostCenterId, 
				@SettlementType, @IncludeServiceOrderDetailId, @RecoveryRatio, @RateManualId, @RateManualType, @RateManualDetailId, @DefinitionRateDetailId, @DefinitionRateDetailConditionId, 
				@SubTotalSalesPrice, @ThirdPartyDiscount, @ThirdPartyDiscountPercentage, @TotalSalesPrice, @GrandTotalSalesPrice, @SurchargeApply, @SurgicalInterventionType, @SurgeryNumber, 
				@IsFirstEvent, @IsAnnulled, @IsDelete, @IncomeMainAccountId, @ApplyRIAS, @RIASCupsId, @CUPSEntityContractDescriptionId)

				set @DetailId = SCOPE_IDENTITY()
			end
			else begin --Se actualiza el detalle
				update [Authorization].AuthorizationOutsourcedServicesServiceOrderDetail set [AuthorizationOutsourcedServicesId] = @AuthorizationOutsourcedServicesId, [CareGroupId] = @CareGroupId, 
				[HealthAdministratorId] = @HealthAdministratorId, 
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
				[IncomeMainAccountId] = @IncomeMainAccountId, [ApplyRIAS] = @ApplyRIAS, [RIASCupsId] = @RIASCupsId, [CUPSEntityContractDescriptionId] = @CUPSEntityContractDescriptionId
				where Id = @DetailId
			end

			--Se insertan los qx
			insert into [Authorization].AuthorizationOutsourcedServicesServiceOrderDetailSurgical([AuthorizationOutsourcedServicesServiceOrderDetailId], [IPSServiceId], [InvoicedQuantity], 
			[LiquidationPercentage], [RateManualSalePrice],
			[TotalSalesPrice], [PerformsHealthProfessionalCode], [PerformsHealthProfessionalThirdPartyId], [CostValue], [BillingConceptId], [CostCenterId], [RateManualDetailSurgicalId],
			[SurchargeApply], [OnlyMedicalFees], [IncomeMainAccountId])
			select @DetailId, IPSServiceId, InvoicedQuantity, LiquidationPercentage, RateManualSalePrice, TotalSalesPrice, PerformsHealthProfessionalCode, PerformsHealthProfessionalThirdPartyId, 
			CostValue, BillingConceptId, CostCenterId, RateManualDetailSurgicalId, SurchargeApply, OnlyMedicalFees, IncomeMainAccountId			
			from @AuthorizationOutsourcedServicesServiceOrderDetailSurgical
			where Id = 0

			--Se actualizan los qx
			update qs set qs.AuthorizationOutsourcedServicesServiceOrderDetailId = qsTemp.AuthorizationOutsourcedServicesServiceOrderDetailId, qs.IPSServiceId = qsTemp.IPSServiceId,
			qs.InvoicedQuantity = qsTemp.InvoicedQuantity, qs.LiquidationPercentage = qsTemp.LiquidationPercentage, qs.RateManualSalePrice = qsTemp.RateManualSalePrice, 
			qs.TotalSalesPrice = qsTemp.TotalSalesPrice, qs.PerformsHealthProfessionalCode = qsTemp.PerformsHealthProfessionalCode, 
			qs.PerformsHealthProfessionalThirdPartyId = qsTemp.PerformsHealthProfessionalThirdPartyId, qs.CostValue = qsTemp.CostValue, qs.BillingConceptId = qsTemp.BillingConceptId, 
			qs.CostCenterId = qsTemp.CostCenterId, qs.RateManualDetailSurgicalId = qsTemp.RateManualDetailSurgicalId, qs.SurchargeApply = qsTemp.SurchargeApply, 
			qs.OnlyMedicalFees = qsTemp.OnlyMedicalFees, qs.IncomeMainAccountId = qsTemp.IncomeMainAccountId
			from @AuthorizationOutsourcedServicesServiceOrderDetailSurgical qsTemp
			inner join [Authorization].AuthorizationOutsourcedServicesServiceOrderDetailSurgical qs on qs.Id = qsTemp.Id
			where qsTemp.Id > 0
		end

		--Se inicializa las variables para recorrer el otro detalle
		set @Rows = 1 
		set @RowId = 0

		--Variables para recorrer el detalle de PharmaceuticalDispensingDetail, se declaran algunos campos ya que arriba en el primer while se declaran los otros
		declare @WarehouseId int, @Quantity int, @ReturnedQuantity int, @FunctionalUnitId int, @OrderedHealthProfessionalCode char(20), @OrderedProfessionalSpecialty char(3), 
		@OrderedHealthProfessionalThirdPartyId int, @SalePrice numeric(20,4), @AverageCost numeric(20,4), @DiscountPercentage numeric(5,2), @DiscountValue numeric(20,4)

		while @Rows > 0
		begin
			select top 1 @RowId = RowId, @DetailId = Id, @AuthorizationOutsourcedServicesId = AuthorizationOutsourcedServicesId, @CareGroupId = CareGroupId, 
			@HealthAdministratorId = HealthAdministratorId, @DetailThirdPartyId = ThirdPartyId,
			@ProductId = ProductId, @WarehouseId = WarehouseId, @Quantity = Quantity, @ReturnedQuantity = ReturnedQuantity, @ServiceDate = ServiceDate, @FunctionalUnitId = FunctionalUnitId,
			@OrderedHealthProfessionalCode = OrderedHealthProfessionalCode, @OrderedProfessionalSpecialty = OrderedProfessionalSpecialty, 
			@OrderedHealthProfessionalThirdPartyId = OrderedHealthProfessionalThirdPartyId, @AuthorizationNumber = AuthorizationNumber, @LiquidationType = LiquidationType, @CupsEntityId = CupsEntityId,
			@SurchargeApply = SurchargeApply, @SalePrice = SalePrice, @AverageCost = AverageCost, @DiscountPercentage = DiscountPercentage, @DiscountValue = DiscountValue, 
			@TotalSalesPrice = TotalSalesPrice, @GrandTotalSalesPrice = GrandTotalSalesPrice
			from @AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail
			where RowId > @RowId 
			order by RowId

			set @Rows = @@RowCount
			if @Rows = 0 
				break

			if @DetailId = 0 --Se inserta el detalle
			begin
				insert into [Authorization].AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail([AuthorizationOutsourcedServicesId], [CareGroupId], [HealthAdministratorId], 
				[ThirdPartyId], [ProductId], [WarehouseId], [Quantity],
				[ReturnedQuantity], [ServiceDate], [FunctionalUnitId], [OrderedHealthProfessionalCode], [OrderedProfessionalSpecialty], [OrderedHealthProfessionalThirdPartyId], [AuthorizationNumber],
				[LiquidationType], [CupsEntityId], [SurchargeApply], [SalePrice], [AverageCost], [DiscountPercentage], [DiscountValue], [TotalSalesPrice], [GrandTotalSalesPrice])
				values(@Id, @CareGroupId, @HealthAdministratorId, @DetailThirdPartyId, @ProductId, @WarehouseId, @Quantity, @ReturnedQuantity, @ServiceDate, @FunctionalUnitId, 
				@OrderedHealthProfessionalCode, @OrderedProfessionalSpecialty, @OrderedHealthProfessionalThirdPartyId, @AuthorizationNumber, @LiquidationType, @CupsEntityId, @SurchargeApply, 
				@SalePrice, @AverageCost, @DiscountPercentage, @DiscountValue, @TotalSalesPrice, @GrandTotalSalesPrice)

				set @DetailId = SCOPE_IDENTITY()
			end
			else begin --Se actualiza el detalle
				update [Authorization].AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail set AuthorizationOutsourcedServicesId = @AuthorizationOutsourcedServicesId, 
				CareGroupId = @CareGroupId, HealthAdministratorId = @HealthAdministratorId, 
				ThirdPartyId = @DetailThirdPartyId, ProductId = @ProductId, WarehouseId = @WarehouseId, Quantity = @Quantity, ReturnedQuantity = @ReturnedQuantity, 
				ServiceDate = @ServiceDate, FunctionalUnitId = @FunctionalUnitId, OrderedHealthProfessionalCode = @OrderedHealthProfessionalCode, 
				OrderedProfessionalSpecialty = @OrderedProfessionalSpecialty, OrderedHealthProfessionalThirdPartyId = @OrderedHealthProfessionalThirdPartyId, 
				AuthorizationNumber = @AuthorizationNumber, LiquidationType = @LiquidationType, CupsEntityId = @CupsEntityId, SurchargeApply = @SurchargeApply, 
				SalePrice = @SalePrice, AverageCost = @AverageCost, DiscountPercentage = @DiscountPercentage, DiscountValue = @DiscountValue, TotalSalesPrice = @TotalSalesPrice, 
				GrandTotalSalesPrice = @GrandTotalSalesPrice
				where Id = @DetailId
			end
		end

		--Se actualiza el estado y el id de la autorización de servicios tercerizados en la tabla del trámite
		update t set t.Status = 14, t.AuthorizationOutsourcedServicesId = @Id
		from @TableTraceabilityPaperworkIds temp
		inner join [Authorization].TraceabilityPaperwork t on t.Id = temp.TraceabilityPaperworkId

		--Se asigna el mensaje a retornar
		set @MessageReturn = case @Status 
								when 2 then 'Se guardó y se confirmó la autorización con código ' + @Code 
								when 3 then 'Se anuló la autorización con código ' + @Code 
								else 'Se guardó la autorización con código ' + @Code 
							 end 

		--Se retorna el ok
		select 0 as CodeMessage, @MessageReturn as Message, @Id AuthorizationOutsourcedServicesId, @Code AuthorizationOutsourcedServicesCode
		return

	End try
	Begin Catch

		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message, 0 AuthorizationOutsourcedServicesId, '' AuthorizationOutsourcedServicesCode
		return

	End Catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda o actualiza una autorización de servicios tercerizados (outsourcing), procesando desde un XML los datos de cabecera y los distintos tipos de detalle: ítems de órdenes de servicio (procedimientos CUPS, estancias hospitalarias, medicamentos), detalle quirúrgico y dispensación farmacéutica. Gestiona todos los estados del ciclo de vida del documento: creación, confirmación, desconfirmación y anulación, generando el consecutivo de autorización según la secuencia configurada. Consulta el ingreso del paciente en ADINGRESO para validar el número de admisión, y persiste los detalles en las tablas AuthorizationOutsourcedServices, AuthorizationOutsourcedServicesServiceOrderDetail, AuthorizationOutsourcedServicesServiceOrderDetailSurgical y AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail. Es el punto central de registro y control de autorizaciones emitidas a proveedores o contratistas externos para servicios prestados a pacientes.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAuthorizationOutsourcedServices';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAuthorizationOutsourcedServices';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe traer un nodo /AuthorizationOutsourcedServices con cabecera, y opcionalmente nodos hijos de detalle (ServiceOrderDetail, ServiceOrderDetailSurgical, PharmaceuticalDispensingDetail) y TraceabilityPaperwork.; Las fechas en el XML (DocumentDate, ServiceDate) deben venir en formato dd/mm/yyyy (estilo 103).; Debe existir configuración en Authorization.AuthorizationSequence para IdForm=''2183'' cuando se requiere generar código automáticamente; si Scope no es ''O'' debe existir AuthorizationSequenceDetail para la unidad operativa indicada.; Si @Type=1 (intrahospitalario), el @AdmissionNumber debe corresponder a un registro existente en ADINGRESO y su IPCODPACI debe coincidir con un Nit en Common.ThirdParty.; Para anulación/desconfirmación (@Status=3 o 0) debe existir el registro con @Id en AuthorizationOutsourcedServices.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationOutsourcedServices';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La anulación (Status=3) y la desconfirmación (Status=0) solo modifican el campo Status de la cabecera y no procesan detalles ni trámites.; El consecutivo solo se genera y se incrementa cuando el código llega vacío o nulo; siempre que se genera, se incrementa [Next] del AuthorizationSequenceDetail correspondiente.; El alcance de la secuencia (organización vs unidad operativa) se rige por AuthorizationSequence.Scope para IdForm=''2183''.; ConfirmationUser/ConfirmationDate solo se setean cuando @Status=2; AnnulmentUser/Date solo cuando @Status=3.; Los ítems con ItemDelete=1 e Id>0 se eliminan físicamente de la tabla destino antes de procesar inserciones/actualizaciones.; En cotizaciones intrahospitalarias (@Type=1), el ThirdPartyId de la cabecera se sobreescribe con el tercero asociado al paciente de la admisión.; Los detalles quirúrgicos nuevos quedan ligados al AuthorizationOutsourcedServicesServiceOrderDetailId recién creado/actualizado en la iteración (@DetailId).; Los trámites asociados quedan en Status=14 al guardarse la autorización tercerizada.; Cualquier excepción en el TRY captura el error y devuelve CodeMessage=999 con ERROR_MESSAGE() sin propagarlo.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationOutsourcedServices';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autorización de servicios tercerizados; Servicios outsourcing; Detalle quirúrgico (cirugía); Dispensación farmacéutica; Trámite de autorización (traceability); Secuencia/consecutivo de autorización; Admisión hospitalaria (intrahospitalario); Tercero (NIT); Unidad operativa; CUPS / IPS Service / RIAS', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationOutsourcedServices';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Status = 3 (anulación) → Actualiza Status=3 en AuthorizationOutsourcedServices y retorna mensaje de anulación, sin procesar detalles ni trámites.; si @Status = 0 (desconfirmación) → Actualiza Status=1 en AuthorizationOutsourcedServices y retorna mensaje de desconfirmación, sin procesar detalles.; si @Code es vacío o NULL → Genera consecutivo consultando AuthorizationSequence/AuthorizationSequenceDetail según Scope (''O'' organización vs unidad operativa) e incrementa el [Next]. else Conserva el código recibido sin tocar la secuencia.; si Scope = ''O'' en AuthorizationSequence (IdForm=2183) → Toma el patrón/secuencia sin filtrar por unidad operativa. else Filtra AuthorizationSequenceDetail adicionalmente por IdOperatingUnit = @OperatingUnitId.; si @idSequenceDetail IS NULL tras buscar la secuencia → Retorna CodeMessage=999 con ''Secuencia no encontrada para generar la autorización de servicio tercerizado'' y termina.; si @Type = 1 (intrahospitalario) → Recupera IPCODPACI de ADINGRESO por NUMINGRES=@AdmissionNumber y reasigna @ThirdPartyId al Id de Common.ThirdParty cuyo Nit coincide.; si @Id = 0 o NULL → INSERT en AuthorizationOutsourcedServices con CreationUser/CreationDate y, si @Status=2, ConfirmationUser/ConfirmationDate. else UPDATE de la cabecera fijando ModificationUser/Date; si @Status=3 set AnnulmentUser/Date; si @Status=2 set ConfirmationUser/Date.; si Detalle ServiceOrderDetail con @DetailId = 0 → INSERT en AuthorizationOutsourcedServicesServiceOrderDetail. else UPDATE del registro existente por Id.; si Detalle quirúrgico con Id = 0 en tabla temporal → INSERT en AuthorizationOutsourcedServicesServiceOrderDetailSurgical asociado al @DetailId recién insertado/actualizado. else UPDATE de los quirúrgicos existentes (Id>0) por Id.; si Detalle PharmaceuticalDispensing con @DetailId = 0 → INSERT en AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail. else UPDATE del registro existente por Id.; si Existen TraceabilityPaperworkIds en el XML → Actualiza TraceabilityPaperwork fijando Status=14 y AuthorizationOutsourcedServicesId=@Id para esos trámites.; si @Status final del save → Construye el mensaje: 2=''Se guardó y se confirmó la autorización…'', 3=''Se anuló…'', otro=''Se guardó la autorización…''.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationOutsourcedServices';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetSequence; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationOutsourcedServices';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.AuthorizationOutsourcedServices; Authorization.AuthorizationSequence; Authorization.AuthorizationSequenceDetail; Common.Sequense; ADINGRESO; Common.ThirdParty; Authorization.TraceabilityPaperwork', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationOutsourcedServices';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationOutsourcedServices';
-- GO
