-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-06-01
-- Description:	Distribuye un servicio
-- =============================================
CREATE Procedure [Billing].[SP_DistributeFolio]
	@RevenueControlId Int,
	@SourceFolioId Int,
	@TargetFolioId Int,
	@DistribType Tinyint,
	@DistributeQuantity Int,
	@CaregroupId Int,
	@CareGroupIdTarget Int,
	@ChangeRateServicesNeccesary Bit,
	@User Varchar(20),
	@PatientGenus Int,
	@PatientBirth DateTime,
	@OnlyRateChange Bit,
	@ThirdPartyPatientId Int,
	@HealthAdministratorId Int,
	@FolioType Tinyint,
	@ContractEntityId Int,
	@ThirdPartyId Int,
	@ProductsAndServicesXml Xml,
	@HomologationsXml Xml,
	@ListServiceOrderDetailXml Xml
AS
Begin
	Set Nocount On;
	Begin Try
		Declare @srFolioQuantity Int,
			@srCareGroupId Int,
			@srHealthAdministratorId Int,
			@srThirdPartyId Int,
			@tgCareGroupId Int,
			@ServiceOrderId Int		

		--variable que se usan para el valor unitario
		declare 
			@LiquidateMasterAccount as bit,
			@IvaId as int,
			@IvaPercentage numeric(5,2),
			@GrossValue as numeric(20, 2),
			@TaxValue as numeric(20, 2)

		Select @SourceFolioId = rcd.Id,
			@srFolioQuantity = rc.FolioQuantity,
			@srCareGroupId = rcd.CareGroupId,
			@srHealthAdministratorId = rcd.HealthAdministratorId,
			@srThirdPartyId = rcd.ThirdPartyId
		From Billing.RevenueControlDetail rcd With(Nolock)
		Inner Join Billing.RevenueControl rc With(Nolock) On rcd.RevenueControlId = rc.Id
		Where rcd.Id = @SourceFolioId
		
		If @SourceFolioId Is Null Begin
			Select Convert(Bit, 0) As [StatusResult], '{ERR1}' As [MessageResult], '' As [Message], Null As ObjectEmbbeded, Null As ObjectEmbbededAux
			Return
		End
		If IsNull(@TargetFolioId, 0) > 0 Begin
			--El folio existe y se debe consultar
			Select @tgCareGroupId = rcd.CareGroupId
			From Billing.RevenueControlDetail rcd With(Nolock)
			Inner Join Billing.RevenueControl rc With(Nolock) On rcd.RevenueControlId = rc.Id
			Where rcd.Id = @TargetFolioId
			If @tgCareGroupId Is Null Begin 
				--El folio destino no existe
				Select Convert(Bit, 0) As [StatusResult], '{ERR2}' As [MessageResult], '' As [Message], Null As ObjectEmbbeded, Null As ObjectEmbbededAux
				Return
			End
		End
		Else Begin
			
			If @DistribType <> 4 begin
				--Creamos un nuevo folio
				Insert Into Billing.RevenueControlDetail
				(RevenueControlId, FolioOrder, FolioType, LiquidationType, ContractEntityId,
				HealthAdministratorId, ThirdPartyId, CareGroupId, TotalFolio, ResponsibleRecoveryFee, TotalPatientSalesPrice,
				PatientDiscount, PatientDiscountPercentage, TotalPatientWithDiscount, ValueCopay, ValueFeeModerator,
				ValueVoucher, [Status], CreationUser, CreationDate)
				Values(@RevenueControlId,@srFolioQuantity + 1,@FolioType,1,@ContractEntityId,@HealthAdministratorId,
					@ThirdPartyId,@CaregroupId,0,1,0,0,0,0,0,0,0,1,@User,GetDate())

				Set @TargetFolioId = Scope_Identity()
				Set @tgCareGroupId = @CaregroupId

				Update Billing.RevenueControl Set FolioQuantity = FolioQuantity + 1
				Where Id = @RevenueControlId
			End
			Else Begin
				Set @TargetFolioId = -1
				Set @tgCareGroupId = @CaregroupId
			End
			
		End

		Declare @listSourceDelete Table(ServiceOrderDetailDistributionId Int)
		Declare @listSourceDeleteServiceOrderDetail Table(ServiceOrderDetailId Int)
		Declare @listDeleteServiceOrderDetail Table(ServiceOrderDetailId Int)

		Declare @ProductAndServices Table(
			RowId Int Identity(1,1) Primary Key,
			Id Int,
			ServiceOrderDetailId Int,
			SourceFolioValue Decimal(18, 2),
			TargetFolioValue Decimal(18, 2),
			SourceFolioGrandTotalDiscountValue Decimal(18, 2),
			TargetFolioGrandTotalDiscountValue Decimal(18, 2),
			GuidHomologation Varchar(50) Null,
			SourceDistribType Tinyint Null,
			TargetDistribType Tinyint Null,
			InvoiceQuantity Int Null,
			ProductDefaultPOSId Int Null,
			ProductPOSTotalValue Decimal(18, 2)
		)
		Insert Into @ProductAndServices
		Select t.x.value('Id[1]', 'Int'),
			t.x.value('ServiceOrderDetailId[1]', 'Int'),
			t.x.value('SourceFolioValue[1]', 'Decimal(18, 2)'),
			t.x.value('TargetFolioValue[1]', 'Decimal(18, 2)'),
			t.x.value('SourceFolioGrandTotalDiscountValue[1]', 'Decimal(18, 2)'),
			t.x.value('TargetFolioGrandTotalDiscountValue[1]', 'Decimal(18, 2)'),
			t.x.value('GuidHomologation[1]', 'Varchar(50)'),
			t.x.value('SourceDistribType[1]', 'Tinyint'),
			t.x.value('TargetDistribType[1]', 'Tinyint'),
			t.x.value('InvoiceQuantity[1]', 'Int'),
			t.x.value('ProductDefaultPOSId[1]', 'Int'),
			t.x.value('ProductPOSTotalValue[1]', 'Decimal(18, 2)')
		From @ProductsAndServicesXml.nodes('ProductsAndServicesXml') t(x)

		Declare @distributionsSource Table(
			Id Int Primary Key,
			RevenueControlDetailId Int,
			ServiceOrderDetailId Int,
			Quantity Int,
			GrandTotalSalesPrice Decimal(20,2),
			GrandTotalDiscount Decimal(20,2),
			DistributionType Tinyint,
			ThirdPartySalesPrice Decimal(20,2),
			ThirdPartyPercentage Decimal(5, 2),
			ApplyRecoveryFee Tinyint,
			RecoveryFeeType Tinyint,
			SubTotalPatientSalesPrice Decimal(20,2),
			PatientPercentage Decimal(5, 2),
			LastCaregroupId Int,
			--ServiceOrderDetail
			ServiceOrderId Int,
			CareGroupId Int,
			HealthAdministratorId Int Null,
			ThirdPartyId Int Null,
			ServiceType Tinyint,
			RecordType Tinyint,
			CUPSEntityId Int Null,
			IPSServiceId Int Null,
			HospitalStayId Int Null,
			HospitalStayDetailId Int Null,
			ControlExternalConsultation Tinyint Null,
			ControlExternalConsultationCode Decimal(18, 0) Null,
			CUPSAssociateService Bit,
			CodeAssociateService Varchar(50) Null,
			IsPackage Bit,
			Packaging Bit,
			PackageServiceOrderDetailId Int Null,
			LiquidationType Tinyint,
			Presentation Tinyint Null,
			ProductId Int Null,
			InvoicedQuantity Int,
			SupplyQuantity Int,
			DevolutionQuantity Int,
			RateManualSalePrice Decimal(20,2),
			CostValue Decimal(20, 2),
			ServiceDate DateTime,
			AuthorizationNumber Varchar(20),
			PerformsFunctionalUnitId Int,
			PerformsHealthProfessionalCode Char(20),
			PerformsProfessionalSpecialty Char(3),
			PerformsHealthProfessionalThirdPartyId Int Null,
			BillingConceptId Int Null,
			CostCenterId Int,
			SettlementType Tinyint,
			IncludeServiceOrderDetailId Int Null,
			RecoveryRatio Decimal(5, 2) Null,
			RateManualId Int Null,
			RateManualType Tinyint Null,
			RateManualDetailId Int Null,
			DefinitionRateDetailId Int Null,
			DefinitionRateDetailConditionId Int Null,
			SubTotalSalesPrice_1 Decimal(18, 2),
			ThirdPartyDiscount_1 Decimal(18, 0),
			ThirdPartyDiscountPercentage Decimal(5, 2),
			TotalSalesPrice Decimal(20, 2),
			GrandTotalSalesPrice_1 Decimal(20, 2),
			SurchargeApply Bit,
			SurgicalInterventionType Tinyint Null,
			SurgeryNumber Tinyint,
			IsFirstEvent Bit,
			IsAnnulled Bit,
			IsDelete Bit,
			IncomeMainAccountId Int,
			--Extender
			CodeNameSpeciality Varchar(300) Null,
			CodeNameFunctionalUnit Varchar(300) Null,
			CodeNameHealthAdministrator Varchar(300) Null,				
			PreviusServiceOrderDetailId Int Null,				
			CodeNameCareGroup Varchar(320) Null,
			CodeNameCostCenter Varchar(300) Null,
			CodeNameCups Varchar(320) Null,
			CodeNameHealthProfessional Varchar(300) Null,
			CodeNameIpsService Varchar(300) Null,
			CodeNameProduct Varchar(300) Null,				
			IsSOAT Bit,
			ServiceOrderDetailSurgicalXml Xml Null
		)
		Declare @distributionstarget Table(
			Id Int Primary Key,
			RevenueControlDetailId Int,
			ServiceOrderDetailId Int,
			Quantity Int,
			GrandTotalSalesPrice Decimal(20,2),
			GrandTotalDiscount Decimal(20,2),
			DistributionType Tinyint,
			ThirdPartySalesPrice Decimal(20,2),
			ThirdPartyPercentage Decimal(5, 2),
			ApplyRecoveryFee Tinyint,
			RecoveryFeeType Tinyint,
			SubTotalPatientSalesPrice Decimal(20,2),
			PatientPercentage Decimal(5, 2),
			LastCaregroupId Int,
			--ServiceOrderDetail
			ServiceOrderId Int,
			CareGroupId Int,
			HealthAdministratorId Int Null,
			ThirdPartyId Int Null,
			ServiceType Tinyint,
			RecordType Tinyint,
			CUPSEntityId Int Null,
			IPSServiceId Int Null,
			HospitalStayId Int Null,
			HospitalStayDetailId Int Null,
			ControlExternalConsultation Tinyint Null,
			ControlExternalConsultationCode Decimal(18, 0) Null,
			CUPSAssociateService Bit,
			CodeAssociateService Varchar(50) Null,
			IsPackage Bit,
			Packaging Bit,
			PackageServiceOrderDetailId Int Null,
			LiquidationType Tinyint,
			Presentation Tinyint Null,
			ProductId Int Null,
			InvoicedQuantity Int,
			SupplyQuantity Int,
			DevolutionQuantity Int,
			RateManualSalePrice Decimal(20,2),
			CostValue Decimal(20, 2),
			ServiceDate DateTime,
			AuthorizationNumber Varchar(20),
			PerformsFunctionalUnitId Int,
			PerformsHealthProfessionalCode Char(20),
			PerformsProfessionalSpecialty Char(3),
			PerformsHealthProfessionalThirdPartyId Int Null,
			BillingConceptId Int Null,
			CostCenterId Int,
			SettlementType Tinyint,
			IncludeServiceOrderDetailId Int Null,
			RecoveryRatio Decimal(5, 2) Null,
			RateManualId Int Null,
			RateManualType Tinyint Null,
			RateManualDetailId Int Null,
			DefinitionRateDetailId Int Null,
			DefinitionRateDetailConditionId Int Null,
			SubTotalSalesPrice_1 Decimal(18, 2),
			ThirdPartyDiscount_1 Decimal(18, 0),
			ThirdPartyDiscountPercentage Decimal(5, 2),
			TotalSalesPrice Decimal(20, 2),
			GrandTotalSalesPrice_1 Decimal(20,2),
			SurchargeApply Bit,
			SurgicalInterventionType Tinyint Null,
			SurgeryNumber Tinyint,
			IsFirstEvent Bit,
			IsAnnulled Bit,
			IsDelete Bit,
			IncomeMainAccountId Int,
			--Extender
			CodeNameSpeciality Varchar(300) Null,
			CodeNameFunctionalUnit Varchar(300) Null,
			CodeNameHealthAdministrator Varchar(300) Null,				
			PreviusServiceOrderDetailId Int Null,				
			CodeNameCareGroup Varchar(320) Null,
			CodeNameCostCenter Varchar(300) Null,
			CodeNameCups Varchar(320) Null,
			CodeNameHealthProfessional Varchar(300) Null,
			CodeNameIpsService Varchar(300) Null,
			CodeNameProduct Varchar(300) Null,				
			IsSOAT Bit,
			ServiceOrderDetailSurgicalXml Xml Null
		)

		Insert Into @distributionsSource
		Select sodd.Id,
			sodd.RevenueControlDetailId,
			sodd.ServiceOrderDetailId,
			sodd.Quantity,
			sodd.GrandTotalSalesPrice,
			sodd.GrandTotalDiscount,
			sodd.DistributionType,
			sodd.ThirdPartySalesPrice,
			sodd.ThirdPartyPercentage,
			sodd.ApplyRecoveryFee,
			sodd.RecoveryFeeType,
			sodd.SubTotalPatientSalesPrice,
			sodd.PatientPercentage,
			sodd.LastCaregroupId,
			--ServiceOrderDetail
			sod.ServiceOrderId,
			sod.CareGroupId,
			sod.HealthAdministratorId,
			sod.ThirdPartyId,
			sod.ServiceType,
			sod.RecordType,
			sod.CUPSEntityId,
			sod.IPSServiceId,
			sod.HospitalStayId,
			sod.HospitalStayDetailId,
			sod.ControlExternalConsultation,
			sod.ControlExternalConsultationCode,
			sod.CUPSAssociateService,
			sod.CodeAssociateService,
			sod.IsPackage,
			sod.Packaging,
			sod.PackageServiceOrderDetailId,
			sod.LiquidationType,
			sod.Presentation,
			sod.ProductId,
			sod.InvoicedQuantity,
			sod.SupplyQuantity,
			sod.DevolutionQuantity,
			sod.RateManualSalePrice,
			sod.CostValue,
			sod.ServiceDate,
			sod.AuthorizationNumber,
			sod.PerformsFunctionalUnitId,
			sod.PerformsHealthProfessionalCode,
			sod.PerformsProfessionalSpecialty,
			sod.PerformsHealthProfessionalThirdPartyId,
			sod.BillingConceptId,
			sod.CostCenterId,
			sod.SettlementType,
			sod.IncludeServiceOrderDetailId,
			sod.RecoveryRatio,
			sod.RateManualId,
			sod.RateManualType,
			sod.RateManualDetailId,
			sod.DefinitionRateDetailId,
			sod.DefinitionRateDetailConditionId,
			sod.SubTotalSalesPrice,
			sod.ThirdPartyDiscount,
			sod.ThirdPartyDiscountPercentage,
			sod.TotalSalesPrice,
			sod.GrandTotalSalesPrice,
			sod.SurchargeApply,
			sod.SurgicalInterventionType,
			sod.SurgeryNumber,
			sod.IsFirstEvent,
			sod.IsAnnulled,
			sod.IsDelete,
			sod.IncomeMainAccountId,
			--Extender
			Null,
			Null,
			Null,
			Null,
			Null,
			Null,
			Null,
			Null,
			Null,
			Null,
			0,
			Null
		From Billing.ServiceOrderDetailDistribution sodd With(Nolock)
		Inner Join Billing.ServiceOrderDetail sod With(Nolock) On sodd.ServiceOrderDetailId = sod.Id
		Inner Join @ProductAndServices ps On ps.Id = sodd.Id

		Insert Into @distributionstarget
		Select sodd.Id,
			sodd.RevenueControlDetailId,
			sodd.ServiceOrderDetailId,
			sodd.Quantity,
			sodd.GrandTotalSalesPrice,
			sodd.GrandTotalDiscount,
			sodd.DistributionType,
			sodd.ThirdPartySalesPrice,
			sodd.ThirdPartyPercentage,
			sodd.ApplyRecoveryFee,
			sodd.RecoveryFeeType,
			sodd.SubTotalPatientSalesPrice,
			sodd.PatientPercentage,
			sodd.LastCaregroupId,
			--ServiceOrderDetail
			sod.ServiceOrderId,
			sod.CareGroupId,
			sod.HealthAdministratorId,
			sod.ThirdPartyId,
			sod.ServiceType,
			sod.RecordType,
			sod.CUPSEntityId,
			sod.IPSServiceId,
			sod.HospitalStayId,
			sod.HospitalStayDetailId,
			sod.ControlExternalConsultation,
			sod.ControlExternalConsultationCode,
			sod.CUPSAssociateService,
			sod.CodeAssociateService,
			sod.IsPackage,
			sod.Packaging,
			sod.PackageServiceOrderDetailId,
			sod.LiquidationType,
			sod.Presentation,
			sod.ProductId,
			sod.InvoicedQuantity,
			sod.SupplyQuantity,
			sod.DevolutionQuantity,
			sod.RateManualSalePrice,
			sod.CostValue,
			sod.ServiceDate,
			sod.AuthorizationNumber,
			sod.PerformsFunctionalUnitId,
			sod.PerformsHealthProfessionalCode,
			sod.PerformsProfessionalSpecialty,
			sod.PerformsHealthProfessionalThirdPartyId,
			sod.BillingConceptId,
			sod.CostCenterId,
			sod.SettlementType,
			sod.IncludeServiceOrderDetailId,
			sod.RecoveryRatio,
			sod.RateManualId,
			sod.RateManualType,
			sod.RateManualDetailId,
			sod.DefinitionRateDetailId,
			sod.DefinitionRateDetailConditionId,
			sod.SubTotalSalesPrice,
			sod.ThirdPartyDiscount,
			sod.ThirdPartyDiscountPercentage,
			sod.TotalSalesPrice,
			sod.GrandTotalSalesPrice,
			sod.SurchargeApply,
			sod.SurgicalInterventionType,
			sod.SurgeryNumber,
			sod.IsFirstEvent,
			sod.IsAnnulled,
			sod.IsDelete,
			sod.IncomeMainAccountId,
			--Extender
			Null,
			Null,
			Null,
			Null,
			Null,
			Null,
			Null,
			Null,
			Null,
			Null,
			0,
			Null
		From Billing.ServiceOrderDetailDistribution sodd With(Nolock)
		Inner Join Billing.ServiceOrderDetail sod With(Nolock) On sodd.ServiceOrderDetailId = sod.Id
		Inner Join @ProductAndServices ps On ps.ServiceOrderDetailId = sod.Id
		
		--consultamos el id del iva del detalle de la orden de servicio
		SELECT 
			@IvaId = sod.IvaId,
			@LiquidateMasterAccount = sb.LiquidateMasterAccount,
			@IvaPercentage = glIVA.Percentage
		FROM Billing.ServiceOrderDetail sod
			JOIN Billing.ServiceOrder so on so.Id = sod.ServiceOrderId
			JOIN Billing.SettingsBilling sb on sb.IdOperatingUnit = so.OperatingUnitId
			join GeneralLedger.GeneralLedgerIVA glIVA on glIVA.Id = sod.IvaId
			JOIN @ProductAndServices ps on ps.ServiceOrderDetailId = sod.id
		WHERE sod.Id = ps.ServiceOrderDetailId

		-- Validar si los productos estan cubiertos
		IF EXISTS(SELECT 1
					FROM @ProductAndServices ps
					JOIN @distributionsSource sod ON ps.Id = sod.Id
					JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON ip.Id=sod.ProductId
					JOIN Contract.CareGroup cg WITH (NOLOCK) ON @CareGroupIdTarget = cg.Id
					LEFT JOIN Inventory.ProductRateGeneral prg WITH (NOLOCK) ON cg.ProductRateId = prg.ProductRateId 
					AND ((prg.RuleType=1 and prg.ProductTypeId=ip.ProductTypeId) or (prg.RuleType=2 and prg.ProductGroupId= ip.ProductGroupId)
					or (prg.RuleType=3 and prg.ProductSubGroupId=ip.ProductSubGroupId)) AND CAST(sod.ServiceDate AS DATE) BETWEEN prg.InitialDate AND prg.EndDate
					WHERE sod.RecordType = 2 AND prg.Id IS NULL ) BEGIN

					DECLARE @TempIdOldProductRate TABLE (productId int) -- tabla temp para almacenar los productos que no estan cubiertos por la nueva tarificacion para validar
					--si esta en la antigua

					insert into @TempIdOldProductRate
					SELECT ip.Id
					FROM @ProductAndServices ps
					JOIN @distributionsSource sod ON ps.Id = sod.Id
					JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON ip.Id=sod.ProductId
					JOIN Contract.CareGroup cg WITH (NOLOCK) ON @CareGroupIdTarget = cg.Id
					LEFT JOIN Inventory.ProductRateGeneral prg WITH (NOLOCK) ON cg.ProductRateId = prg.ProductRateId 
					AND ((prg.RuleType=1 and prg.ProductTypeId=ip.ProductTypeId) or (prg.RuleType=2 and prg.ProductGroupId= ip.ProductGroupId)
					or (prg.RuleType=3 and prg.ProductSubGroupId=ip.ProductSubGroupId)) AND CAST(sod.ServiceDate AS DATE) BETWEEN prg.InitialDate AND prg.EndDate
					WHERE sod.RecordType = 2 AND prg.Id IS NULL

					IF EXISTS 
					(
						SELECT 1 
						FROM @ProductAndServices ps
						JOIN @distributionsSource sod ON ps.Id = sod.Id
						JOIN Contract.CareGroup cg WITH (NOLOCK) ON @CareGroupIdTarget = cg.Id
						JOIN @TempIdOldProductRate temp on sod.ProductId=temp.productId
						LEFT JOIN Inventory.ProductRateDetail prd WITH (NOLOCK) ON cg.ProductRateId = prd.ProductRateId AND sod.ProductId = prd.ProductId AND CAST(sod.ServiceDate AS DATE) BETWEEN prd.InitialDate AND prd.EndDate
						WHERE sod.RecordType = 2 AND prd.Id IS NULL
					) 
					BEGIN
						DECLARE @MessageResultOut VARCHAR(MAX)
						SELECT @MessageResultOut = STUFF((
								SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ip.Code, ' - ' , ip.Name)
								FROM @ProductAndServices ps
								JOIN @distributionsSource sod ON ps.Id = sod.Id
								JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON sod.ProductId = ip.Id
								JOIN Contract.CareGroup cg WITH (NOLOCK) ON @CareGroupIdTarget = cg.Id
								JOIN @TempIdOldProductRate temp on sod.ProductId=temp.productId
								LEFT JOIN Inventory.ProductRateDetail prd WITH (NOLOCK) ON cg.ProductRateId = prd.ProductRateId AND sod.ProductId = prd.ProductId AND CAST(sod.ServiceDate AS DATE) BETWEEN prd.InitialDate AND prd.EndDate
								WHERE sod.RecordType = 2 AND prd.Id IS NULL
								FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
						SET @MessageResultOut = CONCAT('Los siguientes productos no se encuentran cubiertos: ' + CHAR(13) + CHAR(10), @MessageResultOut)

						Select Convert(Bit, 0) As [StatusResult], @MessageResultOut As [MessageResult], '' As [Message], Null As ObjectEmbbeded, Null As ObjectEmbbededAux			
						RETURN
					END
		END

		
		Declare @saveTargetFolio Bit = 0

		Declare @Rows Int, @RowId Int
		Set @Rows = 1
		Set @RowId = 1

		Declare @ServiceId Int, 
			@ServiceOrderDetailId Int,
			@SourceFolioValue Decimal(20,2),
			@TargetFolioValue Decimal(20,2),
			@SourceFolioGrandTotalDiscountValue Decimal(20,2),
			@TargetFolioGrandTotalDiscountValue Decimal(20,2),
			@GuidHomologation Varchar(50),
			@SourceDistribType Tinyint,
			@TargetDistribType Tinyint,
			@InvoiceQuantity Int,
			@ProductDefaultPOSId Int,
			@ProductPOSTotalValue Decimal(20,2)

		While @Rows > 0
		Begin

			Select Top 1 @RowId = RowId,
				@ServiceId = Id,
				@ServiceOrderDetailId = ServiceOrderDetailId,
				@SourceFolioValue = SourceFolioValue,
				@TargetFolioValue = TargetFolioValue,
				@SourceFolioGrandTotalDiscountValue = SourceFolioGrandTotalDiscountValue,
				@TargetFolioGrandTotalDiscountValue = TargetFolioGrandTotalDiscountValue,
				@GuidHomologation = GuidHomologation,
				@SourceDistribType = SourceDistribType,
				@TargetDistribType = TargetDistribType,
				@InvoiceQuantity = InvoiceQuantity,
				@ProductDefaultPOSId = ProductDefaultPOSId,
				@ProductPOSTotalValue = ProductPOSTotalValue
			From @ProductAndServices 
			Where RowId >= @RowId Order By RowId

			Set @Rows = @@ROWCOUNT
			If @Rows = 0 
				Break
			--Si esta incluido dentro de otro servicio puede ir TargetValue = 0
			Declare @sourceDistId Int,@sourceDistRevenueControlDetailId Int,@sourceDistServiceOrderDetailId Int,@sourceDistQuantity Int,
				@sourceDistGrandTotalSalesPrice Decimal(20,2),@sourceDistGrandTotalDiscount Decimal(20,2),@sourceDistDistributionType Tinyint,
				@sourceDistThirdPartySalesPrice Decimal(20,2),@sourceDistThirdPartyPercentage Decimal(5, 2),@sourceDistApplyRecoveryFee Tinyint,
				@sourceDistRecoveryFeeType Tinyint,@sourceDistSubTotalPatientSalesPrice Decimal(20,2),@sourceDistPatientPercentage Decimal(5, 2),
				@sourceDistLastCaregroupId Int,@sourceDistServiceOrderId Int,@sourceDistCareGroupId Int,@sourceDistHealthAdministratorId Int,
				@sourceDistThirdPartyId Int,@sourceDistServiceType Tinyint,@sourceDistRecordType Tinyint,@sourceDistCUPSEntityId Int,
				@sourceDistIPSServiceId Int,@sourceDistHospitalStayId Int,@sourceDistHospitalStayDetailId Int,@sourceDistControlExternalConsultation Tinyint,
				@sourceDistControlExternalConsultationCode Decimal(18, 0),@sourceDistCUPSAssociateService Bit,@sourceDistCodeAssociateService Varchar(50),
				@sourceDistIsPackage Bit,@sourceDistPackaging Bit,@sourceDistPackageServiceOrderDetailId Int,@sourceDistLiquidationType Tinyint,
				@sourceDistPresentation Tinyint,@sourceDistProductId Int,@sourceDistInvoicedQuantity Int,@sourceDistSupplyQuantity Int,
				@sourceDistDevolutionQuantity Int,@sourceDistRateManualSalePrice Decimal(20,2),@sourceDistCostValue Decimal(20, 2),
				@sourceDistServiceDate DateTime,@sourceDistAuthorizationNumber Varchar(20),@sourceDistPerformsFunctionalUnitId Int,
				@sourceDistPerformsHealthProfessionalCode Char(20),@sourceDistPerformsProfessionalSpecialty Char(3),@sourceDistPerformsHealthProfessionalThirdPartyId Int,
				@sourceDistBillingConceptId Int,@sourceDistCostCenterId Int,@sourceDistSettlementType Tinyint,@sourceDistIncludeServiceOrderDetailId Int,
				@sourceDistRecoveryRatio Decimal(5, 2),@sourceDistRateManualId Int,@sourceDistRateManualType Tinyint,@sourceDistRateManualDetailId Int,
				@sourceDistDefinitionRateDetailId Int,@sourceDistDefinitionRateDetailConditionId Int,@sourceDistSubTotalSalesPrice_1 Decimal(20, 2),
				@sourceDistThirdPartyDiscount_1 Decimal(20,2),@sourceDistThirdPartyDiscountPercentage Decimal(5, 2),@sourceDistTotalSalesPrice Decimal(20, 2),
				@sourceDistGrandTotalSalesPrice_1 Decimal(20,2),@sourceDistSurchargeApply Bit,@sourceDistSurgicalInterventionType Tinyint,
				@sourceDistSurgeryNumber Tinyint,@sourceDistIsFirstEvent Bit,@sourceDistIsAnnulled Bit,
				@sourceDistIsDelete Bit,@sourceDistIncomeMainAccountId Int,@sourceDistCodeNameSpeciality Varchar(300),
				@sourceDistCodeNameFunctionalUnit Varchar(300),@sourceDistCodeNameHealthAdministrator Varchar(300),@sourceDistPreviusServiceOrderDetailId Int,				
				@sourceDistCodeNameCareGroup Varchar(320),@sourceDistCodeNameCostCenter Varchar(300),@sourceDistCodeNameCups Varchar(320),
				@sourceDistCodeNameHealthProfessional Varchar(300),@sourceDistCodeNameIpsService Varchar(300),@sourceDistCodeNameProduct Varchar(300),				
				@sourceDistIsSOAT Bit,@sourceDistServiceOrderDetailSurgicalXml Xml

			Declare @targetDistId Int,@targetDistRevenueControlDetailId Int,@targetDistServiceOrderDetailId Int,@targetDistQuantity Int,
				@targetDistGrandTotalSalesPrice Decimal(20,2),@targetDistGrandTotalDiscount Decimal(20,2),@targetDistDistributionType Tinyint,
				@targetDistThirdPartySalesPrice Decimal(20,2),@targetDistThirdPartyPercentage Decimal(5, 2),@targetDistApplyRecoveryFee Tinyint,
				@targetDistRecoveryFeeType Tinyint,@targetDistSubTotalPatientSalesPrice Decimal(20,2),@targetDistPatientPercentage Decimal(5, 2),
				@targetDistLastCaregroupId Int,@targetDistServiceOrderId Int,@targetDistCareGroupId Int,@targetDistHealthAdministratorId Int,
				@targetDistThirdPartyId Int,@targetDistServiceType Tinyint,@targetDistRecordType Tinyint,@targetDistCUPSEntityId Int,
				@targetDistIPSServiceId Int,@targetDistHospitalStayId Int,@targetDistHospitalStayDetailId Int,
				@targetDistControlExternalConsultation Tinyint,@targetDistControlExternalConsultationCode Decimal(18, 0),
				@targetDistCUPSAssociateService Bit,@targetDistCodeAssociateService Varchar(50),@targetDistIsPackage Bit,
				@targetDistPackaging Bit,@targetDistPackageServiceOrderDetailId Int,@targetDistLiquidationType Tinyint,
				@targetDistPresentation Tinyint,@targetDistProductId Int,@targetDistInvoicedQuantity Int,
				@targetDistSupplyQuantity Int,@targetDistDevolutionQuantity Int,@targetDistRateManualSalePrice Decimal(20,2),
				@targetDistCostValue Decimal(20, 2),@targetDistServiceDate DateTime,@targetDistAuthorizationNumber Varchar(20),
				@targetDistPerformsFunctionalUnitId Int,@targetDistPerformsHealthProfessionalCode Char(20),
				@targetDistPerformsProfessionalSpecialty Char(3),@targetDistPerformsHealthProfessionalThirdPartyId Int,
				@targetDistBillingConceptId Int,@targetDistCostCenterId Int,@targetDistSettlementType Tinyint,
				@targetDistIncludeServiceOrderDetailId Int,@targetDistRecoveryRatio Decimal(5, 2),@targetDistRateManualId Int,
				@targetDistRateManualType Tinyint,@targetDistRateManualDetailId Int,@targetDistDefinitionRateDetailId Int,@targetDistDefinitionRateDetailConditionId Int,
				@targetDistSubTotalSalesPrice_1 Decimal(20, 2),@targetDistThirdPartyDiscount_1 Decimal(20, 2),@targetDistThirdPartyDiscountPercentage Decimal(5, 2),
				@targetDistTotalSalesPrice Decimal(20, 2),@targetDistGrandTotalSalesPrice_1 Decimal(20, 2),@targetDistSurchargeApply Bit,
				@targetDistSurgicalInterventionType Tinyint,@targetDistSurgeryNumber Tinyint,@targetDistIsFirstEvent Bit,
				@targetDistIsAnnulled Bit,@targetDistIsDelete Bit,@targetDistIncomeMainAccountId Int,@targetDistCodeNameSpeciality Varchar(300),
				@targetDistCodeNameFunctionalUnit Varchar(300),@targetDistCodeNameHealthAdministrator Varchar(300),				
				@targetDistPreviusServiceOrderDetailId Int,@targetDistCodeNameCareGroup Varchar(320),@targetDistCodeNameCostCenter Varchar(300),
				@targetDistCodeNameCups Varchar(320),@targetDistCodeNameHealthProfessional Varchar(300),@targetDistCodeNameIpsService Varchar(300),
				@targetDistCodeNameProduct Varchar(300),@targetDistIsSOAT Bit,@targetDistServiceOrderDetailSurgicalXml Xml

			
			Select Top 1 @sourceDistId=Id,@sourceDistRevenueControlDetailId=RevenueControlDetailId,
				@sourceDistServiceOrderDetailId=ServiceOrderDetailId,@sourceDistQuantity=Quantity,
				@sourceDistGrandTotalSalesPrice=GrandTotalSalesPrice,@sourceDistGrandTotalDiscount=GrandTotalDiscount,
				@sourceDistDistributionType=DistributionType,@sourceDistThirdPartySalesPrice=ThirdPartySalesPrice,
				@sourceDistThirdPartyPercentage=ThirdPartyPercentage,@sourceDistApplyRecoveryFee=ApplyRecoveryFee,
				@sourceDistRecoveryFeeType=RecoveryFeeType,@sourceDistSubTotalPatientSalesPrice=SubTotalPatientSalesPrice,
				@sourceDistPatientPercentage=PatientPercentage,@sourceDistLastCaregroupId=LastCaregroupId,
				@sourceDistServiceOrderId=ServiceOrderId,@sourceDistCareGroupId=CareGroupId,@sourceDistHealthAdministratorId=HealthAdministratorId,
				@sourceDistThirdPartyId=ThirdPartyId,@sourceDistServiceType=ServiceType,@sourceDistRecordType=RecordType,@sourceDistCUPSEntityId=CUPSEntityId,
				@sourceDistIPSServiceId=IPSServiceId,@sourceDistHospitalStayId=HospitalStayId,@sourceDistHospitalStayDetailId=HospitalStayDetailId,
				@sourceDistControlExternalConsultation=ControlExternalConsultation,@sourceDistControlExternalConsultationCode=ControlExternalConsultationCode,
				@sourceDistCUPSAssociateService=CUPSAssociateService,@sourceDistCodeAssociateService=CodeAssociateService,
				@sourceDistIsPackage=IsPackage,@sourceDistPackaging=Packaging,@sourceDistPackageServiceOrderDetailId=PackageServiceOrderDetailId,
				@sourceDistLiquidationType=LiquidationType,@sourceDistPresentation=Presentation,@sourceDistProductId=ProductId,
				@sourceDistInvoicedQuantity=InvoicedQuantity,@sourceDistSupplyQuantity=SupplyQuantity,
				@sourceDistDevolutionQuantity=DevolutionQuantity,@sourceDistRateManualSalePrice=RateManualSalePrice,@sourceDistCostValue=CostValue,
				@sourceDistServiceDate=ServiceDate,@sourceDistAuthorizationNumber=AuthorizationNumber,@sourceDistPerformsFunctionalUnitId=PerformsFunctionalUnitId,
				@sourceDistPerformsHealthProfessionalCode=PerformsHealthProfessionalCode,@sourceDistPerformsProfessionalSpecialty=PerformsProfessionalSpecialty,
				@sourceDistPerformsHealthProfessionalThirdPartyId=PerformsHealthProfessionalThirdPartyId,@sourceDistBillingConceptId=BillingConceptId,
				@sourceDistCostCenterId=CostCenterId,@sourceDistSettlementType=SettlementType,@sourceDistIncludeServiceOrderDetailId=IncludeServiceOrderDetailId,
				@sourceDistRecoveryRatio=RecoveryRatio,@sourceDistRateManualId=RateManualId,@sourceDistRateManualType=RateManualType,
				@sourceDistRateManualDetailId=RateManualDetailId,@sourceDistDefinitionRateDetailId=DefinitionRateDetailId,
				@sourceDistDefinitionRateDetailConditionId=DefinitionRateDetailConditionId,@sourceDistSubTotalSalesPrice_1=SubTotalSalesPrice_1,
				@sourceDistThirdPartyDiscount_1=ThirdPartyDiscount_1,@sourceDistThirdPartyDiscountPercentage=ThirdPartyDiscountPercentage,
				@sourceDistTotalSalesPrice=TotalSalesPrice,@sourceDistGrandTotalSalesPrice_1=GrandTotalSalesPrice_1,@sourceDistSurchargeApply=SurchargeApply,
				@sourceDistSurgicalInterventionType=SurgicalInterventionType,@sourceDistSurgeryNumber=SurgeryNumber,@sourceDistIsFirstEvent=IsFirstEvent,
				@sourceDistIsAnnulled=IsAnnulled,@sourceDistIsDelete=IsDelete,@sourceDistIncomeMainAccountId=IncomeMainAccountId,
				@sourceDistCodeNameSpeciality=CodeNameSpeciality,@sourceDistCodeNameFunctionalUnit=CodeNameFunctionalUnit,
				@sourceDistCodeNameHealthAdministrator=CodeNameHealthAdministrator,@sourceDistPreviusServiceOrderDetailId=PreviusServiceOrderDetailId,				
				@sourceDistCodeNameCareGroup=CodeNameCareGroup,@sourceDistCodeNameCostCenter=CodeNameCostCenter,@sourceDistCodeNameCups=CodeNameCups,
				@sourceDistCodeNameHealthProfessional=CodeNameHealthProfessional,@sourceDistCodeNameIpsService=CodeNameIpsService,
				@sourceDistCodeNameProduct=CodeNameProduct,@sourceDistIsSOAT=IsSOAT,@sourceDistServiceOrderDetailSurgicalXml=ServiceOrderDetailSurgicalXml
			From @distributionsSource Where Id = @ServiceId

			If Exists (Select 1 From @distributionstarget 
				Where ServiceOrderDetailId = @ServiceOrderDetailId
					And RevenueControlDetailId = @TargetFolioId) Begin
				--Si encuentra el mismo item en el folio destino (en pocas palabras si esta distribuido): lo une (eliminando el de origen)
				--Si esta distribuito por corte de cuentas entonces debe retarificarse. Corte de cuentas es cuando el caregroup del folio destino es distinto al de origen
				Select Top 1 @targetDistId=Id,@targetDistRevenueControlDetailId=RevenueControlDetailId,
					@targetDistServiceOrderDetailId=ServiceOrderDetailId,@targetDistQuantity=Quantity,
					@targetDistGrandTotalSalesPrice=GrandTotalSalesPrice,@targetDistGrandTotalDiscount=GrandTotalDiscount,
					@targetDistDistributionType=DistributionType,@targetDistThirdPartySalesPrice=ThirdPartySalesPrice,
					@targetDistThirdPartyPercentage=ThirdPartyPercentage,@targetDistApplyRecoveryFee=ApplyRecoveryFee,
					@targetDistRecoveryFeeType=RecoveryFeeType,@targetDistSubTotalPatientSalesPrice=SubTotalPatientSalesPrice,
					@targetDistPatientPercentage=PatientPercentage,@targetDistLastCaregroupId=LastCaregroupId,
					@targetDistServiceOrderId=ServiceOrderId,@targetDistCareGroupId=CareGroupId,@targetDistHealthAdministratorId=HealthAdministratorId,
					@targetDistThirdPartyId=ThirdPartyId,@targetDistServiceType=ServiceType,@targetDistRecordType=RecordType,@targetDistCUPSEntityId=CUPSEntityId,
					@targetDistIPSServiceId=IPSServiceId,@targetDistHospitalStayId=HospitalStayId,@targetDistHospitalStayDetailId=HospitalStayDetailId,
					@targetDistControlExternalConsultation=ControlExternalConsultation,@targetDistControlExternalConsultationCode=ControlExternalConsultationCode,
					@targetDistCUPSAssociateService=CUPSAssociateService,@targetDistCodeAssociateService=CodeAssociateService,
					@targetDistIsPackage=IsPackage,@targetDistPackaging=Packaging,@targetDistPackageServiceOrderDetailId=PackageServiceOrderDetailId,
					@targetDistLiquidationType=LiquidationType,@targetDistPresentation=Presentation,@targetDistProductId=ProductId,
					@targetDistInvoicedQuantity=InvoicedQuantity,@targetDistSupplyQuantity=SupplyQuantity,
					@targetDistDevolutionQuantity=DevolutionQuantity,@targetDistRateManualSalePrice=RateManualSalePrice,@targetDistCostValue=CostValue,
					@targetDistServiceDate=ServiceDate,@targetDistAuthorizationNumber=AuthorizationNumber,@targetDistPerformsFunctionalUnitId=PerformsFunctionalUnitId,
					@targetDistPerformsHealthProfessionalCode=PerformsHealthProfessionalCode,@targetDistPerformsProfessionalSpecialty=PerformsProfessionalSpecialty,
					@targetDistPerformsHealthProfessionalThirdPartyId=PerformsHealthProfessionalThirdPartyId,@targetDistBillingConceptId=BillingConceptId,
					@targetDistCostCenterId=CostCenterId,@targetDistSettlementType=SettlementType,@targetDistIncludeServiceOrderDetailId=IncludeServiceOrderDetailId,
					@targetDistRecoveryRatio=RecoveryRatio,@targetDistRateManualId=RateManualId,@targetDistRateManualType=RateManualType,
					@targetDistRateManualDetailId=RateManualDetailId,@targetDistDefinitionRateDetailId=DefinitionRateDetailId,
					@targetDistDefinitionRateDetailConditionId=DefinitionRateDetailConditionId,@targetDistSubTotalSalesPrice_1=SubTotalSalesPrice_1,
					@targetDistThirdPartyDiscount_1=ThirdPartyDiscount_1,@targetDistThirdPartyDiscountPercentage=ThirdPartyDiscountPercentage,
					@targetDistTotalSalesPrice=TotalSalesPrice,@targetDistGrandTotalSalesPrice_1=GrandTotalSalesPrice_1,@targetDistSurchargeApply=SurchargeApply,
					@targetDistSurgicalInterventionType=SurgicalInterventionType,@targetDistSurgeryNumber=SurgeryNumber,@targetDistIsFirstEvent=IsFirstEvent,
					@targetDistIsAnnulled=IsAnnulled,@targetDistIsDelete=IsDelete,@targetDistIncomeMainAccountId=IncomeMainAccountId,
					@targetDistCodeNameSpeciality=CodeNameSpeciality,@targetDistCodeNameFunctionalUnit=CodeNameFunctionalUnit,
					@targetDistCodeNameHealthAdministrator=CodeNameHealthAdministrator,@targetDistPreviusServiceOrderDetailId=PreviusServiceOrderDetailId,				
					@targetDistCodeNameCareGroup=CodeNameCareGroup,@targetDistCodeNameCostCenter=CodeNameCostCenter,@targetDistCodeNameCups=CodeNameCups,
					@targetDistCodeNameHealthProfessional=CodeNameHealthProfessional,@targetDistCodeNameIpsService=CodeNameIpsService,
					@targetDistCodeNameProduct=CodeNameProduct,@targetDistIsSOAT=IsSOAT,@targetDistServiceOrderDetailSurgicalXml=ServiceOrderDetailSurgicalXml
				From @distributionstarget 
				Where ServiceOrderDetailId = @ServiceOrderDetailId
					And RevenueControlDetailId = @TargetFolioId

				If @targetDistDistributionType <> 5 Begin
					Set @targetDistGrandTotalSalesPrice += @TargetFolioValue
					Set @targetDistGrandTotalDiscount += @TargetFolioGrandTotalDiscountValue
					Set @targetDistThirdPartySalesPrice = @targetDistGrandTotalSalesPrice
				End

				Set @targetDistThirdPartyPercentage = 100
				Set @targetDistApplyRecoveryFee = 1
				Set @targetDistRecoveryFeeType = 1
				Set @targetDistSubTotalPatientSalesPrice = 0
				Set @targetDistDistributionType = 1
				Set @targetDistPatientPercentage = 0

				If @srCareGroupId <> @tgCareGroupId Begin
					Set @CareGroupIdTarget = @tgCareGroupId
				End
				Insert Into @listSourceDelete Values(@sourceDistId)

				Update Billing.ServiceOrderDetailDistribution Set GrandTotalSalesPrice = @targetDistGrandTotalSalesPrice,
					GrandTotalDiscount = @targetDistGrandTotalDiscount,
					ThirdPartySalesPrice = @targetDistThirdPartySalesPrice,
					ThirdPartyPercentage = @targetDistThirdPartyPercentage,
					ApplyRecoveryFee = @targetDistApplyRecoveryFee,
					RecoveryFeeType = @targetDistRecoveryFeeType,
					SubTotalPatientSalesPrice = @targetDistSubTotalPatientSalesPrice,
					DistributionType = @targetDistDistributionType,
					PatientPercentage = @targetDistPatientPercentage
				Where Id = @targetDistId

			End
			Else If @sourceDistDistributionType = 5 And Exists (Select 1 From @distributionstarget 
				Where CodeAssociateService = @GuidHomologation) Begin

				Select Top 1 @targetDistId=sodd.Id,@targetDistRevenueControlDetailId=RevenueControlDetailId,
					@targetDistServiceOrderDetailId=ServiceOrderDetailId,@targetDistQuantity=Quantity,
					@targetDistGrandTotalSalesPrice=sodd.GrandTotalSalesPrice,@targetDistGrandTotalDiscount=GrandTotalDiscount,
					@targetDistDistributionType=DistributionType,@targetDistThirdPartySalesPrice=ThirdPartySalesPrice,
					@targetDistThirdPartyPercentage=ThirdPartyPercentage,@targetDistApplyRecoveryFee=ApplyRecoveryFee,
					@targetDistRecoveryFeeType=RecoveryFeeType,@targetDistSubTotalPatientSalesPrice=SubTotalPatientSalesPrice,
					@targetDistPatientPercentage=PatientPercentage,@targetDistLastCaregroupId=LastCaregroupId,
					@targetDistServiceOrderId=ServiceOrderId,@targetDistCareGroupId=CareGroupId,@targetDistHealthAdministratorId=HealthAdministratorId,
					@targetDistThirdPartyId=ThirdPartyId,@targetDistServiceType=ServiceType,@targetDistRecordType=RecordType,@targetDistCUPSEntityId=CUPSEntityId,
					@targetDistIPSServiceId=IPSServiceId,@targetDistHospitalStayId=HospitalStayId,@targetDistHospitalStayDetailId=HospitalStayDetailId,
					@targetDistControlExternalConsultation=ControlExternalConsultation,@targetDistControlExternalConsultationCode=ControlExternalConsultationCode,
					@targetDistCUPSAssociateService=CUPSAssociateService,@targetDistCodeAssociateService=CodeAssociateService,
					@targetDistIsPackage=IsPackage,@targetDistPackaging=Packaging,@targetDistPackageServiceOrderDetailId=PackageServiceOrderDetailId,
					@targetDistLiquidationType=LiquidationType,@targetDistPresentation=Presentation,@targetDistProductId=ProductId,
					@targetDistInvoicedQuantity=InvoicedQuantity,@targetDistSupplyQuantity=SupplyQuantity,
					@targetDistDevolutionQuantity=DevolutionQuantity,@targetDistRateManualSalePrice=RateManualSalePrice,@targetDistCostValue=CostValue,
					@targetDistServiceDate=ServiceDate,@targetDistAuthorizationNumber=AuthorizationNumber,@targetDistPerformsFunctionalUnitId=PerformsFunctionalUnitId,
					@targetDistPerformsHealthProfessionalCode=PerformsHealthProfessionalCode,@targetDistPerformsProfessionalSpecialty=PerformsProfessionalSpecialty,
					@targetDistPerformsHealthProfessionalThirdPartyId=PerformsHealthProfessionalThirdPartyId,@targetDistBillingConceptId=BillingConceptId,
					@targetDistCostCenterId=CostCenterId,@targetDistSettlementType=SettlementType,@targetDistIncludeServiceOrderDetailId=IncludeServiceOrderDetailId,
					@targetDistRecoveryRatio=RecoveryRatio,@targetDistRateManualId=RateManualId,@targetDistRateManualType=RateManualType,
					@targetDistRateManualDetailId=RateManualDetailId,@targetDistDefinitionRateDetailId=DefinitionRateDetailId,
					@targetDistDefinitionRateDetailConditionId=DefinitionRateDetailConditionId,@targetDistSubTotalSalesPrice_1=sod.SubTotalSalesPrice,
					@targetDistThirdPartyDiscount_1=sod.ThirdPartyDiscount,@targetDistThirdPartyDiscountPercentage=ThirdPartyDiscountPercentage,
					@targetDistTotalSalesPrice=TotalSalesPrice,@targetDistGrandTotalSalesPrice_1=sod.GrandTotalSalesPrice,@targetDistSurchargeApply=SurchargeApply,
					@targetDistSurgicalInterventionType=SurgicalInterventionType,@targetDistSurgeryNumber=SurgeryNumber,@targetDistIsFirstEvent=IsFirstEvent,
					@targetDistIsAnnulled=IsAnnulled,@targetDistIsDelete=IsDelete,@targetDistIncomeMainAccountId=IncomeMainAccountId,
					@targetDistCodeNameSpeciality='',@targetDistCodeNameFunctionalUnit='',
					@targetDistCodeNameHealthAdministrator='',@targetDistPreviusServiceOrderDetailId=Null,				
					@targetDistCodeNameCareGroup='',@targetDistCodeNameCostCenter='',@targetDistCodeNameCups='',
					@targetDistCodeNameHealthProfessional='',@targetDistCodeNameIpsService='',
					@targetDistCodeNameProduct='',@targetDistIsSOAT=0,@targetDistServiceOrderDetailSurgicalXml=Null
				From Billing.ServiceOrderDetailDistribution sodd With(Nolock)
				Inner Join Billing.ServiceOrderDetail sod With(Nolock) On sodd.ServiceOrderDetailId = sod.Id
				Where sodd.RevenueControlDetailId = @TargetFolioId
					And sod.CodeAssociateService = @GuidHomologation

				If @targetDistId Is Null Begin					
					Set @sourceDistRevenueControlDetailId = @TargetFolioId
					--TODO: SaveEntity
					Update Billing.ServiceOrderDetailDistribution Set RevenueControlDetailId = @sourceDistRevenueControlDetailId
					Where Id = @sourceDistId
					Set @RowId += 1
					Continue
				End
				Declare @InvoiceId Int
				If @targetDistId Is Not Null And @targetDistCUPSAssociateService = 1 Begin
					Set @sourceDistDistributionType = 1
					Insert Into @listSourceDelete Values (@targetDistId)
					
					Select @InvoiceId = Id From Billing.InvoiceDetail id With(Nolock) Where ServiceOrderDetailId = @targetDistServiceOrderDetailId	
					If @InvoiceId Is Not Null Begin
						Set @targetDistIsDelete = 1
						Update Billing.ServiceOrderDetail Set IsDelete = 1
						Where Id = @targetDistServiceOrderDetailId
						--TODO: saveentity
					End
					Else Begin
						Insert Into @listSourceDeleteServiceOrderDetail Values(@targetDistServiceOrderDetailId)
					End
					Set @sourceDistSettlementType = 1
					Set @sourceDistSubTotalSalesPrice_1 = (@targetDistGrandTotalSalesPrice + @sourceDistGrandTotalSalesPrice) / @sourceDistQuantity
					Set @sourceDistTotalSalesPrice = @sourceDistSubTotalSalesPrice_1
					Set @sourceDistGrandTotalSalesPrice_1 = @targetDistGrandTotalSalesPrice + @sourceDistGrandTotalSalesPrice
					Set @sourceDistCodeAssociateService = Null
					Set @sourceDistRevenueControlDetailId = @targetDistRevenueControlDetailId
					Set @sourceDistGrandTotalSalesPrice = @sourceDistGrandTotalSalesPrice + @targetDistGrandTotalSalesPrice
					Set @sourceDistThirdPartySalesPrice = @sourceDistGrandTotalSalesPrice
					--TODO: saveentity

					Update Billing.ServiceOrderDetailDistribution Set DistributionType = @sourceDistDistributionType,
						RevenueControlDetailId = @sourceDistRevenueControlDetailId,
						GrandTotalSalesPrice = @sourceDistGrandTotalSalesPrice,
						ThirdPartySalesPrice = @sourceDistThirdPartySalesPrice
					Where Id = @sourceDistId

					Update Billing.ServiceOrderDetail Set SettlementType = @sourceDistSettlementType,
						SubTotalSalesPrice = @sourceDistSubTotalSalesPrice_1,
						TotalSalesPrice = @sourceDistTotalSalesPrice,
						GrandTotalSalesPrice = @sourceDistGrandTotalSalesPrice_1,
						CodeAssociateService = Null
					Where Id = @sourceDistServiceOrderDetailId
				End
				Else Begin
					Set @targetDistDistributionType = 1
					Insert Into @listSourceDelete Values(@sourceDistId)

					Select @InvoiceId = Id From Billing.InvoiceDetail id With(Nolock) Where ServiceOrderDetailId = @sourceDistServiceOrderDetailId	
					If @InvoiceId Is Not Null Begin
						Set @sourceDistIsDelete = 1
						Update Billing.ServiceOrderDetail Set IsDelete = 1
						Where Id = @sourceDistServiceOrderDetailId
						--TODO: saveentity
					End
					Else Begin
						Insert Into @listSourceDeleteServiceOrderDetail Values(@sourceDistServiceOrderDetailId)
					End
					Set @targetDistSettlementType = 1
					Set @targetDistSubTotalSalesPrice_1 = (@targetDistGrandTotalSalesPrice + @sourceDistGrandTotalSalesPrice) / @sourceDistQuantity
					Set @targetDistTotalSalesPrice = @targetDistSubTotalSalesPrice_1
					Set @targetDistGrandTotalSalesPrice_1 = @targetDistGrandTotalSalesPrice + @sourceDistGrandTotalSalesPrice
					Set @targetDistCodeAssociateService = Null
					Set @targetDistRevenueControlDetailId = @sourceDistRevenueControlDetailId
					Set @targetDistGrandTotalSalesPrice = @sourceDistGrandTotalSalesPrice + @targetDistGrandTotalSalesPrice
					Set @targetDistThirdPartySalesPrice = @targetDistGrandTotalSalesPrice
					--TODO: saveentity

					Update Billing.ServiceOrderDetailDistribution Set DistributionType = @targetDistDistributionType,
						RevenueControlDetailId = @targetDistRevenueControlDetailId,
						GrandTotalSalesPrice = @targetDistGrandTotalSalesPrice,
						ThirdPartySalesPrice = @targetDistThirdPartySalesPrice
					Where Id = @targetDistId

					Update Billing.ServiceOrderDetail Set SettlementType = @targetDistSettlementType,
						SubTotalSalesPrice = @targetDistSubTotalSalesPrice_1,
						TotalSalesPrice = @targetDistTotalSalesPrice,
						GrandTotalSalesPrice = @targetDistGrandTotalSalesPrice_1,
						CodeAssociateService = Null
					Where Id = @targetDistServiceOrderDetailId
				End

			End
			Else Begin
			
				If @DistribType = 4 Begin	--Distribucion por unidades

					Select @ServiceOrderId = Id From Billing.ServiceOrder so With(Nolock) Where Id = @sourceDistServiceOrderId
					If @ServiceOrderId Is Null Begin
						Select Convert(Bit, 0) As [StatusResult], 'No se encontró la orden de servicio' As [MessageResult], '' As [Message], Null As ObjectEmbbeded, Null As ObjectEmbbededAux
						Return
					End

					Set @targetDistServiceOrderId = @sourceDistServiceOrderId
					Set @targetDistCareGroupId = @sourceDistCareGroupId
					Set @targetDistHealthAdministratorId = @sourceDistHealthAdministratorId
					Set @targetDistThirdPartyId = @sourceDistThirdPartyId
					Set @targetDistRecordType = @sourceDistRecordType
					Set @targetDistCUPSEntityId = @sourceDistCUPSEntityId
					Set @targetDistIPSServiceId = @sourceDistIPSServiceId
					Set @targetDistHospitalStayId = @sourceDistHospitalStayId
					Set @targetDistHospitalStayDetailId = @sourceDistHospitalStayDetailId
					Set @targetDistCUPSAssociateService = @sourceDistCUPSAssociateService
					Set @targetDistCodeAssociateService = @sourceDistCodeAssociateService
					Set @targetDistIsPackage = @sourceDistIsPackage
					Set @targetDistPackaging = @sourceDistPackaging
					Set @targetDistPackageServiceOrderDetailId = @sourceDistPackageServiceOrderDetailId
					Set @targetDistLiquidationType = @sourceDistLiquidationType
					Set @targetDistPresentation = @sourceDistPresentation
					Set @targetDistProductId = @sourceDistProductId
					Set @targetDistInvoicedQuantity = @DistributeQuantity
					Set @targetDistSupplyQuantity = @sourceDistSupplyQuantity
					Set @targetDistDevolutionQuantity = @sourceDistDevolutionQuantity
					Set @targetDistRateManualSalePrice = @sourceDistRateManualSalePrice
					Set @targetDistCostValue = @sourceDistCostValue
					Set @targetDistServiceDate = @sourceDistServiceDate
					Set @targetDistAuthorizationNumber = @sourceDistAuthorizationNumber
					Set @targetDistPerformsFunctionalUnitId = @sourceDistPerformsFunctionalUnitId
					Set @targetDistPerformsHealthProfessionalCode = @sourceDistPerformsHealthProfessionalCode
					Set @targetDistPerformsHealthProfessionalThirdPartyId = @sourceDistPerformsHealthProfessionalThirdPartyId
					Set @targetDistBillingConceptId = @sourceDistBillingConceptId
					Set @targetDistCostCenterId = @sourceDistCostCenterId
					Set @targetDistSettlementType = @sourceDistSettlementType
					Set @targetDistIncludeServiceOrderDetailId = @sourceDistIncludeServiceOrderDetailId
					Set @targetDistRecoveryRatio = @sourceDistRecoveryRatio
					Set @targetDistRateManualId = @sourceDistRateManualId
					Set @targetDistRateManualType = @sourceDistRateManualType
					Set @targetDistRateManualDetailId = @sourceDistRateManualDetailId
					Set @targetDistSubTotalSalesPrice_1 = @sourceDistSubTotalSalesPrice_1
					Set @targetDistThirdPartyDiscount_1 = @sourceDistThirdPartyDiscount_1
					Set @targetDistThirdPartyDiscountPercentage = @sourceDistThirdPartyDiscountPercentage
					Set @targetDistTotalSalesPrice = @sourceDistTotalSalesPrice
					Set @targetDistGrandTotalSalesPrice_1 = @sourceDistTotalSalesPrice * @DistributeQuantity
					Set @targetDistSurchargeApply = @sourceDistSurchargeApply
					Set @targetDistSurgicalInterventionType = @sourceDistSurgicalInterventionType
					Set @targetDistSurgeryNumber = @sourceDistSurgeryNumber
					Set @targetDistIsFirstEvent = @sourceDistIsFirstEvent
					Set @targetDistIncomeMainAccountId = @sourceDistIncomeMainAccountId
					
					Set @sourceDistQuantity -= @DistributeQuantity
					Set @sourceDistInvoicedQuantity -= @DistributeQuantity
					Set @sourceDistGrandTotalSalesPrice_1 = @sourceDistTotalSalesPrice * @sourceDistInvoicedQuantity
					Set @sourceDistGrandTotalSalesPrice = @sourceDistTotalSalesPrice * @sourceDistQuantity
					Set @sourceDistGrandTotalDiscount = @sourceDistThirdPartyDiscount_1 * @sourceDistQuantity
					Set @targetDistRevenueControlDetailId = @sourceDistRevenueControlDetailId
					Set @targetDistQuantity = @DistributeQuantity
					Set @targetDistGrandTotalSalesPrice = @sourceDistTotalSalesPrice * @targetDistQuantity
					Set @targetDistGrandTotalDiscount = @targetDistThirdPartyDiscount_1 * @targetDistQuantity
					Set @targetDistDistributionType = @SourceDistribType
					Set @targetDistThirdPartySalesPrice = @targetDistGrandTotalSalesPrice
					Set @targetDistThirdPartyPercentage = 100

					Set @TargetFolioId = @SourceFolioId
					Set @CareGroupIdTarget = @srCareGroupId

					/*seccion del final*/
					Set @sourceDistGrandTotalDiscount = @SourceFolioGrandTotalDiscountValue
					Set @sourceDistThirdPartySalesPrice = @sourceDistGrandTotalSalesPrice
					Set @sourceDistThirdPartyPercentage = 100
					Set @targetDistApplyRecoveryFee = 1
					Set @sourceDistApplyRecoveryFee = 1
					Set @targetDistRecoveryFeeType = 1
					Set @sourceDistRecoveryFeeType = 1
					Set @targetDistSubTotalPatientSalesPrice = 0
					Set @sourceDistSubTotalPatientSalesPrice = 0
					Set @targetDistPatientPercentage = 0
					Set @sourceDistPatientPercentage = 0
					Set @targetDistLastCaregroupId = @CaregroupId
					/**/
					
					-- Se obtiene el valor unitario y el valor del impuesto
					SELECT  
						@GrossValue = GrossValue,
						@TaxValue = TaxValue
					from Billing.SetValueSalesPrice(1,@targetDistSubTotalSalesPrice_1,@IvaPercentage)
					
					Insert Into Billing.ServiceOrderDetail
					(ServiceOrderId,CareGroupId,HealthAdministratorId,ThirdPartyId,RecordType,CUPSEntityId,IPSServiceId,
					HospitalStayId,HospitalStayDetailId,CUPSAssociateService,CodeAssociateService,IsPackage,Packaging,
					PackageServiceOrderDetailId,LiquidationType,Presentation,ProductId,InvoicedQuantity,SupplyQuantity,
					DevolutionQuantity,RateManualSalePrice,CostValue,ServiceDate,AuthorizationNumber,PerformsFunctionalUnitId,
					PerformsHealthProfessionalCode,PerformsHealthProfessionalThirdPartyId,BillingConceptId,CostCenterId,
					SettlementType,IncludeServiceOrderDetailId,RecoveryRatio,RateManualId,RateManualType,RateManualDetailId,
					SubTotalSalesPrice,ThirdPartyDiscount,ThirdPartyDiscountPercentage,TotalSalesPrice,GrandTotalSalesPrice,
					SurchargeApply,SurgicalInterventionType,SurgeryNumber,IsFirstEvent,IncomeMainAccountId,GrossValue,TaxValue,IvaId)
					Values (@targetDistServiceOrderId,@targetDistCareGroupId,@targetDistHealthAdministratorId,@targetDistThirdPartyId,
					@targetDistRecordType,@targetDistCUPSEntityId,@targetDistIPSServiceId,@targetDistHospitalStayId,@targetDistHospitalStayDetailId,
					@targetDistCUPSAssociateService,@targetDistCodeAssociateService,@targetDistIsPackage,@targetDistPackaging,@targetDistPackageServiceOrderDetailId,
					@targetDistLiquidationType,@targetDistPresentation,@targetDistProductId,@targetDistInvoicedQuantity,@targetDistSupplyQuantity,
					@targetDistDevolutionQuantity,@targetDistRateManualSalePrice,@targetDistCostValue,@targetDistServiceDate,
					@targetDistAuthorizationNumber,@targetDistPerformsFunctionalUnitId,@targetDistPerformsHealthProfessionalCode,
					@targetDistPerformsHealthProfessionalThirdPartyId,@targetDistBillingConceptId,@targetDistCostCenterId,@targetDistSettlementType,
					@targetDistIncludeServiceOrderDetailId,@targetDistRecoveryRatio,@targetDistRateManualId,@targetDistRateManualType,
					@targetDistRateManualDetailId,@targetDistSubTotalSalesPrice_1,@targetDistThirdPartyDiscount_1,@targetDistThirdPartyDiscountPercentage,
					@targetDistTotalSalesPrice,@targetDistGrandTotalSalesPrice_1,@targetDistSurchargeApply,@targetDistSurgicalInterventionType,
					@targetDistSurgeryNumber,@targetDistIsFirstEvent,@targetDistIncomeMainAccountId,@GrossValue,@TaxValue,@IvaId)
					
					Set @targetDistServiceOrderDetailId = Scope_Identity()
					
					Insert Into Billing.ServiceOrderDetailDistribution
					(RevenueControlDetailId,ServiceOrderDetailId,Quantity,GrandTotalSalesPrice,
					GrandTotalDiscount,DistributionType,ThirdPartySalesPrice,ThirdPartyPercentage,
					ApplyRecoveryFee,RecoveryFeeType,SubTotalPatientSalesPrice,PatientPercentage,
					LastCaregroupId,SubTotalSalesPrice,GrandTotalTaxes)
					Values(@targetDistRevenueControlDetailId,@targetDistServiceOrderDetailId,@targetDistQuantity,
					@targetDistGrandTotalSalesPrice,@targetDistGrandTotalDiscount,@targetDistDistributionType,
					@targetDistThirdPartySalesPrice,@targetDistThirdPartyPercentage,@targetDistApplyRecoveryFee,
					@targetDistRecoveryFeeType,@targetDistSubTotalPatientSalesPrice,@targetDistPatientPercentage,
					@targetDistLastCaregroupId,
					iif(@LiquidateMasterAccount = 1,(@GrossValue * @targetDistInvoicedQuantity),0),IIF(@LiquidateMasterAccount = 1,(@TaxValue * @targetDistInvoicedQuantity),0)
					)
					
					Set @targetDistId = Scope_Identity()

					Update Billing.ServiceOrderDetail Set InvoicedQuantity = @sourceDistInvoicedQuantity,
						GrandTotalSalesPrice = @sourceDistGrandTotalSalesPrice_1
					Where Id = @sourceDistServiceOrderDetailId

					Update Billing.ServiceOrderDetailDistribution Set Quantity = @sourceDistQuantity,
						GrandTotalSalesPrice = @sourceDistGrandTotalSalesPrice_1,
						GrandTotalDiscount = @sourceDistGrandTotalDiscount
					Where Id = @sourceDistId

				End
				Else If @DistribType = 5 Begin
				
					--se crea un nuevo detalle para la orden de servicio igual al del item origen y se merma las cantidades al distribution y tambien al SOD
					--en este caso el item creado queda en el mismo folio de origen
					Select @ServiceOrderId = Id From Billing.ServiceOrder so With(Nolock) Where Id = @sourceDistServiceOrderId
					If @ServiceOrderId Is Null Begin
						Select Convert(Bit, 0) As [StatusResult], 'No se encontró la orden de servicio' As [MessageResult], '' As [Message], Null As ObjectEmbbeded, Null As ObjectEmbbededAux
						Return
					End
					Declare @_GuidHomologation Varchar(50) = (Select NewId())--Sin reemplazar los guiones para este tipo de distribucion '.Replace("-", "")

					Declare @ProductCost Decimal(18, 2),
						@ProductIncomeAccountId Int,
						@ProductCodeName Varchar(320),
						@ProductGroupId Int,
						@ProductSalesValue Decimal(18, 2)

					Select @ProductCost = ipr.ProductCost,
						@ProductGroupId = ipr.ProductGroupId,
						@ProductCodeName = Concat(ipr.Code, ' - ', ipr.[Name]),
						@ProductIncomeAccountId = pg.IncomeAccountId
					From Inventory.InventoryProduct ipr With(Nolock) 
					Left Join Inventory.ProductGroup pg With(Nolock) On ipr.ProductGroupId = pg.Id
					Where ipr.Id = @ProductDefaultPOSId

					If @ProductGroupId Is Null Begin
						Select Convert(Bit, 0) As [StatusResult], 'El Producto ' + @ProductCodeName + ' no tiene un grupo asociado' As [MessageResult], '' As [Message], Null As ObjectEmbbeded, Null As ObjectEmbbededAux
						Return
					End
					Select @ProductSalesValue = prd.SalesValue
					From [Contract].CareGroup cg With(Nolock)
					Inner Join Inventory.ProductRate pr With(Nolock) On cg.ProductRateId = pr.Id
					Inner Join Inventory.ProductRateDetail prd With(Nolock) On prd.ProductRateId = pr.Id
					Where cg.Id = @srCareGroupId And prd.ProductId = @ProductDefaultPOSId And (prd.InitialDate <= @sourceDistServiceDate And prd.EndDate >= @sourceDistServiceDate)

					Set @targetDistCareGroupId = @srCareGroupId
					Set @targetDistHealthAdministratorId = @srHealthAdministratorId
					Set @targetDistThirdPartyId = @srThirdPartyId
					Set @targetDistRecordType = 2
					Set @targetDistCodeAssociateService = @_GuidHomologation
					Set @targetDistLiquidationType = 5
					Set @targetDistProductId = @ProductDefaultPOSId
					Set @targetDistInvoicedQuantity = @InvoiceQuantity
					Set @targetDistSupplyQuantity = @InvoiceQuantity
					Set @targetDistDevolutionQuantity = 0
					Set @targetDistRateManualSalePrice = @ProductSalesValue
					Set @targetDistCostValue = @ProductCost
					Set @targetDistServiceDate = @sourceDistServiceDate
					Set @targetDistAuthorizationNumber = @sourceDistAuthorizationNumber
					Set @targetDistPerformsFunctionalUnitId = @sourceDistPerformsFunctionalUnitId
					Set @targetDistPerformsHealthProfessionalCode = @sourceDistPerformsHealthProfessionalCode
					Set @targetDistPerformsProfessionalSpecialty = @sourceDistPerformsProfessionalSpecialty
					Set @targetDistPerformsHealthProfessionalThirdPartyId = @sourceDistPerformsHealthProfessionalThirdPartyId
					Set @targetDistSettlementType = @sourceDistSettlementType
					Set @targetDistRecoveryRatio = Null
					Set @targetDistSubTotalSalesPrice_1 = @ProductSalesValue
					Set @targetDistThirdPartyDiscount_1 = 0
					Set @targetDistIsFirstEvent = 1
					Set @targetDistThirdPartyDiscountPercentage = 0
					Set @targetDistTotalSalesPrice = @ProductSalesValue
					Set @targetDistGrandTotalSalesPrice_1 = @ProductSalesValue * @InvoiceQuantity
					Set @targetDistSurchargeApply = 0
					Set @targetDistIncomeMainAccountId = @ProductIncomeAccountId
					Set @targetDistCostCenterId = @sourceDistCostCenterId
					--

					Set @targetDistCUPSAssociateService = 1
					Set @sourceDistCodeAssociateService = @_GuidHomologation
					Set @sourceDistRevenueControlDetailId = @TargetFolioId
					Set @sourceDistDistributionType = @SourceDistribType

					Set @targetDistRevenueControlDetailId = @SourceFolioId
					Set @targetDistQuantity = @sourceDistQuantity
					Set @targetDistGrandTotalDiscount = 0
					Set @targetDistDistributionType = @TargetDistribType
					Set @targetDistThirdPartyPercentage = 100
					Set @targetDistApplyRecoveryFee = 1
					Set @targetDistRecoveryFeeType = 1
					Set @targetDistSubTotalPatientSalesPrice = 0
					Set @targetDistPatientPercentage = 0
					Set @targetDistLastCaregroupId = @sourceDistLastCaregroupId

					--Si el valor total de diferencia calculado en la rejilla es cero debemos dejar los valores de ServiceOrderDetail del nuevo item en cero
					--al igual que establecer que esta incluido en un servicio que para este caso es el producto No POS inicialmente cargado en la orden
					--no se actualizan los valores del detail origial (NOPOS) ya que no afecta facturacion y evitamos una transaccion adicional

					If IsNull(@TargetFolioValue, 0) = 0 Begin
						Set @sourceDistSettlementType = 3 --Incluido 100%
						Set @sourceDistIncludeServiceOrderDetailId = NULL --Asignar cuando tenga el id
						Set @targetDistSubTotalSalesPrice_1 = @sourceDistSubTotalSalesPrice_1
						Set @targetDistTotalSalesPrice = @sourceDistTotalSalesPrice
						Set @targetDistGrandTotalSalesPrice_1 = @sourceDistGrandTotalSalesPrice_1
						Set @targetDistGrandTotalSalesPrice = @targetDistGrandTotalSalesPrice_1
						Set @targetDistThirdPartySalesPrice = @targetDistGrandTotalSalesPrice_1
						Set @sourceDistGrandTotalSalesPrice = 0
						Set @sourceDistThirdPartySalesPrice = 0
					End
					Else Begin
						Set @sourceDistGrandTotalSalesPrice = @TargetFolioValue
						Set @sourceDistThirdPartySalesPrice = @TargetFolioValue
						Set @targetDistGrandTotalSalesPrice = @ProductPOSTotalValue
						Set @targetDistThirdPartySalesPrice = @ProductPOSTotalValue
					End
					Set @targetDistServiceOrderId = @ServiceOrderId

					--Guardamos el serviceOrderDetail
					Set @targetDistCUPSEntityId = Null
					Set @targetDistIPSServiceId = Null
					Set @targetDistHospitalStayId = Null
					Set @targetDistHospitalStayDetailId = Null
					Set @targetDistIsPackage= 0
					Set @targetDistPackaging = 0
					Set @targetDistPackageServiceOrderDetailId = Null
					Set @targetDistPresentation = Null
					Set @targetDistBillingConceptId = Null
					Set @targetDistIncludeServiceOrderDetailId = Null
					Set @targetDistRateManualId = Null
					Set @targetDistRateManualType = Null
					Set @targetDistRateManualDetailId = Null
					Set @targetDistSurgicalInterventionType = Null
					Set @targetDistSurgeryNumber = 0

					/*seccion del final*/
					Set @sourceDistGrandTotalDiscount = @SourceFolioGrandTotalDiscountValue
					Set @sourceDistThirdPartySalesPrice = @sourceDistGrandTotalSalesPrice
					Set @sourceDistThirdPartyPercentage = 100
					Set @targetDistApplyRecoveryFee = 1
					Set @sourceDistApplyRecoveryFee = 1
					Set @targetDistRecoveryFeeType = 1
					Set @sourceDistRecoveryFeeType = 1
					Set @targetDistSubTotalPatientSalesPrice = 0
					Set @sourceDistSubTotalPatientSalesPrice = 0
					Set @targetDistPatientPercentage = 0
					Set @sourceDistPatientPercentage = 0
					Set @targetDistLastCaregroupId = @CaregroupId
					/**/

					Insert Into Billing.ServiceOrderDetail
					(ServiceOrderId,CareGroupId,HealthAdministratorId,ThirdPartyId,RecordType,CUPSEntityId,IPSServiceId,
					HospitalStayId,HospitalStayDetailId,CUPSAssociateService,CodeAssociateService,IsPackage,Packaging,
					PackageServiceOrderDetailId,LiquidationType,Presentation,ProductId,InvoicedQuantity,SupplyQuantity,
					DevolutionQuantity,RateManualSalePrice,CostValue,ServiceDate,AuthorizationNumber,PerformsFunctionalUnitId,
					PerformsHealthProfessionalCode,PerformsHealthProfessionalThirdPartyId,BillingConceptId,CostCenterId,
					SettlementType,IncludeServiceOrderDetailId,RecoveryRatio,RateManualId,RateManualType,RateManualDetailId,
					SubTotalSalesPrice,ThirdPartyDiscount,ThirdPartyDiscountPercentage,TotalSalesPrice,GrandTotalSalesPrice,
					SurchargeApply,SurgicalInterventionType,SurgeryNumber,IsFirstEvent,IncomeMainAccountId)
					Values (@targetDistServiceOrderId,@targetDistCareGroupId,@targetDistHealthAdministratorId,@targetDistThirdPartyId,
					@targetDistRecordType,@targetDistCUPSEntityId,@targetDistIPSServiceId,@targetDistHospitalStayId,@targetDistHospitalStayDetailId,
					@targetDistCUPSAssociateService,@targetDistCodeAssociateService,@targetDistIsPackage,@targetDistPackaging,@targetDistPackageServiceOrderDetailId,
					@targetDistLiquidationType,@targetDistPresentation,@targetDistProductId,@targetDistInvoicedQuantity,@targetDistSupplyQuantity,
					@targetDistDevolutionQuantity,@targetDistRateManualSalePrice,@targetDistCostValue,@targetDistServiceDate,
					@targetDistAuthorizationNumber,@targetDistPerformsFunctionalUnitId,@targetDistPerformsHealthProfessionalCode,
					@targetDistPerformsHealthProfessionalThirdPartyId,@targetDistBillingConceptId,@targetDistCostCenterId,@targetDistSettlementType,
					@targetDistIncludeServiceOrderDetailId,@targetDistRecoveryRatio,@targetDistRateManualId,@targetDistRateManualType,
					@targetDistRateManualDetailId,@targetDistSubTotalSalesPrice_1,@targetDistThirdPartyDiscount_1,@targetDistThirdPartyDiscountPercentage,
					@targetDistTotalSalesPrice,@targetDistGrandTotalSalesPrice_1,@targetDistSurchargeApply,@targetDistSurgicalInterventionType,
					@targetDistSurgeryNumber,@targetDistIsFirstEvent,@targetDistIncomeMainAccountId)

					Set @targetDistServiceOrderDetailId = Scope_Identity()

					Insert Into Billing.ServiceOrderDetailDistribution
					(RevenueControlDetailId,ServiceOrderDetailId,Quantity,GrandTotalSalesPrice,
					GrandTotalDiscount,DistributionType,ThirdPartySalesPrice,ThirdPartyPercentage,
					ApplyRecoveryFee,RecoveryFeeType,SubTotalPatientSalesPrice,PatientPercentage,
					LastCaregroupId)
					Values(@targetDistRevenueControlDetailId,@targetDistServiceOrderDetailId,@targetDistQuantity,
					@targetDistGrandTotalSalesPrice,@targetDistGrandTotalDiscount,@targetDistDistributionType,
					@targetDistThirdPartySalesPrice,@targetDistThirdPartyPercentage,@targetDistApplyRecoveryFee,
					@targetDistRecoveryFeeType,@targetDistSubTotalPatientSalesPrice,@targetDistPatientPercentage,
					@targetDistLastCaregroupId)

					Set @targetDistId = Scope_Identity()

					Update Billing.ServiceOrderDetail Set CodeAssociateService = @sourceDistCodeAssociateService,
						SettlementType = @sourceDistSettlementType, IncludeServiceOrderDetailId = @sourceDistIncludeServiceOrderDetailId
					Where Id = @sourceDistServiceOrderDetailId

					Update Billing.ServiceOrderDetailDistribution Set RevenueControlDetailId = @sourceDistRevenueControlDetailId,
						DistributionType = @sourceDistDistributionType,
						GrandTotalSalesPrice = @sourceDistGrandTotalSalesPrice,
						ThirdPartySalesPrice = @sourceDistThirdPartySalesPrice
					Where Id = @sourceDistId

					Set @saveTargetFolio = 1
				End
Else Begin

    Set @targetDistServiceOrderDetailId = @ServiceOrderDetailId
    Set @targetDistQuantity = @sourceDistQuantity
    Set @sourceDistGrandTotalSalesPrice = @SourceFolioValue
    Set @targetDistGrandTotalSalesPrice = @TargetFolioValue
    Set @targetDistGrandTotalDiscount = @TargetFolioGrandTotalDiscountValue
    Set @sourceDistDistributionType = @SourceDistribType
    Set @targetDistDistributionType = @TargetDistribType
    Set @targetDistThirdPartySalesPrice = @targetDistGrandTotalSalesPrice
    Set @targetDistThirdPartyPercentage = 100
    Set @targetDistRevenueControlDetailId = @TargetFolioId

    Set @targetDistApplyRecoveryFee = 1
    Set @targetDistRecoveryFeeType = 1
    Set @targetDistSubTotalPatientSalesPrice = 0
    Set @targetDistPatientPercentage = 0
    Set @targetDistLastCaregroupId = @CaregroupId
    
    /*seccion del final*/
    Set @sourceDistGrandTotalDiscount = @SourceFolioGrandTotalDiscountValue
    Set @sourceDistThirdPartySalesPrice = @sourceDistGrandTotalSalesPrice
    Set @sourceDistThirdPartyPercentage = 100
    Set @targetDistApplyRecoveryFee = 1
    Set @sourceDistApplyRecoveryFee = 1
    Set @targetDistRecoveryFeeType = 1
    Set @sourceDistRecoveryFeeType = 1
    Set @targetDistSubTotalPatientSalesPrice = 0
    Set @sourceDistSubTotalPatientSalesPrice = 0
    Set @targetDistPatientPercentage = 0
    Set @sourceDistPatientPercentage = 0
    Set @targetDistLastCaregroupId = @CaregroupId
    /**/

    -- ============ ESTO ES LO NUEVO (FIX) ============
    SELECT
        @GrossValue = GrossValue,
        @TaxValue = TaxValue
    FROM Billing.SetValueSalesPrice(1, @targetDistGrandTotalSalesPrice, @IvaPercentage)
    -- ==================================================

    Insert Into Billing.ServiceOrderDetailDistribution
        (RevenueControlDetailId,ServiceOrderDetailId,Quantity,GrandTotalSalesPrice,
        GrandTotalDiscount,DistributionType,ThirdPartySalesPrice,ThirdPartyPercentage,
        ApplyRecoveryFee,RecoveryFeeType,SubTotalPatientSalesPrice,PatientPercentage,
        LastCaregroupId
        ,SubTotalSalesPrice,GrandTotalTaxes)          -- <<< COLUMNAS AGREGADAS
    Values(@targetDistRevenueControlDetailId,@targetDistServiceOrderDetailId,@targetDistQuantity,
        @targetDistGrandTotalSalesPrice,@targetDistGrandTotalDiscount,@targetDistDistributionType,
        @targetDistThirdPartySalesPrice,@targetDistThirdPartyPercentage,@targetDistApplyRecoveryFee,
        @targetDistRecoveryFeeType,@targetDistSubTotalPatientSalesPrice,@targetDistPatientPercentage,
        @targetDistLastCaregroupId
        ,IIF(@LiquidateMasterAccount = 1, @GrossValue, 0), IIF(@LiquidateMasterAccount = 1, @TaxValue, 0))  -- <<< VALORES AGREGADOS
        
    Set @targetDistId = Scope_Identity()

    If Not (@sourceDistGrandTotalSalesPrice = 0 And @sourceDistDistributionType <> 5) Begin
        Update Billing.ServiceOrderDetailDistribution Set 
            GrandTotalSalesPrice = @sourceDistGrandTotalSalesPrice,
            DistributionType = @sourceDistDistributionType
        Where Id = @sourceDistId
    End					
    
    Set @saveTargetFolio = 1
End

				--Set @sourceDistGrandTotalDiscount = @SourceFolioGrandTotalDiscountValue
				--Set @sourceDistThirdPartySalesPrice = @sourceDistGrandTotalSalesPrice
				--Set @sourceDistThirdPartyPercentage = 100
				--Set @targetDistApplyRecoveryFee = 1
				--Set @sourceDistApplyRecoveryFee = 1 //esta parte se metio dentro de cada uno de los condicionales
				--Set @targetDistRecoveryFeeType = 1
				--Set @sourceDistRecoveryFeeType = 1
				--Set @targetDistSubTotalPatientSalesPrice = 0
				--Set @sourceDistSubTotalPatientSalesPrice = 0
				--Set @targetDistPatientPercentage = 0
				--Set @sourceDistPatientPercentage = 0
				--Set @targetDistLastCaregroupId = @CaregroupId

				Update Billing.ServiceOrderDetailDistribution Set ApplyRecoveryFee = @targetDistApplyRecoveryFee,
					RecoveryFeeType = @targetDistRecoveryFeeType, SubTotalPatientSalesPrice = @targetDistSubTotalPatientSalesPrice,
					PatientPercentage = @targetDistPatientPercentage, LastCaregroupId = @targetDistLastCaregroupId
				Where Id = @targetDistId

				If @sourceDistGrandTotalSalesPrice = 0 And @sourceDistDistributionType <> 5
					Insert Into @listSourceDelete Values (@sourceDistId)
				Else
					Update Billing.ServiceOrderDetailDistribution Set 
						GrandTotalDiscount = @sourceDistGrandTotalDiscount,
						ThirdPartySalesPrice = @sourceDistThirdPartySalesPrice,
						ThirdPartyPercentage = @sourceDistThirdPartyPercentage,
						ApplyRecoveryFee = @sourceDistApplyRecoveryFee,
						RecoveryFeeType = @sourceDistRecoveryFeeType,
						SubTotalPatientSalesPrice = @sourceDistSubTotalPatientSalesPrice,
						PatientPercentage = @sourceDistPatientPercentage
					Where Id = @sourceDistId
			End
			Set @RowId += 1
		End

		If @srCareGroupId <> @tgCareGroupId And @DistribType <> 5
			Set @ChangeRateServicesNeccesary = 1
		
		If @saveTargetFolio = 1 Begin
			Update Billing.RevenueControlDetail Set RevenueControlId = @RevenueControlId Where Id = @SourceFolioId
		End
		If @DistribType <> 4 Begin
			Update Billing.RevenueControlDetail Set RevenueControlId = @RevenueControlId Where Id = @TargetFolioId
		End

		Delete sodd
		From Billing.ServiceOrderDetailDistribution sodd
		Inner Join @listSourceDelete ld On sodd.Id = ld.ServiceOrderDetailDistributionId

		--Delete sod
		--From Billing.ServiceOrderDetail sod
		--Inner Join @listDeleteServiceOrderDetail ld On sod.Id = ld.ServiceOrderDetailId

		Declare @response Table(StatusResult Bit, [Message] Varchar(Max))

		IF @DistribType <> 4 And @ChangeRateServicesNeccesary = 0 
		BEGIN
			Insert Into @response Exec Billing.SP_UpdateRevenueControlDetailValues @TargetFolioId, NULL
		END

		Insert Into @response Exec Billing.SP_UpdateRevenueControlDetailValues @SourceFolioId, NULL
		
		If @ChangeRateServicesNeccesary = 1 Begin
							
			Set @CareGroupIdTarget = @tgCareGroupId			

			Declare @tmpResult Table(
				StatusResult Bit,
				MessageResult Varchar(Max),
				[Message] Varchar(Max),
				ObjectEmbbeded Xml,
				ObjectEmbbededAux Xml
			)
		
			Delete From @tmpResult
			Insert Into @tmpResult
			Exec Billing.SP_ChangeRateServices @TargetFolioId, @CareGroupIdTarget, @PatientGenus, 
				@PatientBirth, @HomologationsXml, @OnlyRateChange, @ThirdPartyPatientId, 
				@HealthAdministratorId, @ListServiceOrderDetailXml

			Declare @StatusResult Bit,
					@MessageResult Varchar(Max),
					@Message Varchar(Max),
					@ObjectEmbbeded Xml,
					@ObjectEmbbededAux Xml
			Select @StatusResult = StatusResult, 
				@MessageResult = MessageResult,
				@Message = [Message],
				@ObjectEmbbeded = ObjectEmbbeded,
				@ObjectEmbbededAux = ObjectEmbbededAux
			From @tmpResult
			
			If @StatusResult = 1 Begin
				Select Convert(Bit, 1) As [StatusResult], '' As [MessageResult], '' As [Message],Null As ObjectEmbbeded, Null As ObjectEmbbededAux
				Return
			End
			Else Begin				
				Select Convert(Bit, 0) As [StatusResult], @MessageResult As [MessageResult], @Message As [Message], @ObjectEmbbeded As ObjectEmbbeded, @ObjectEmbbededAux As ObjectEmbbededAux
				Return
			End
		End
		Select Convert(Bit, 1) As [StatusResult], '' As [MessageResult], '' As [Message], Null As ObjectEmbbeded, Null As ObjectEmbbededAux
		--No olvidar insertar el distribution de targetdistribution
	End Try
	Begin Catch
		Select Convert(Bit, 0) As [StatusResult], ERROR_MESSAGE() As [MessageResult], '' As [Message], Null As ObjectEmbbeded, Null As ObjectEmbbededAux
	End Catch
	
End
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribuye servicios o ítems facturables desde un folio de origen hacia un folio destino dentro del control de ingresos de una admisión. Puede crear un nuevo folio en RevenueControlDetail cuando el destino no existe, o reutilizar uno existente, actualizando el contador de folios en RevenueControl. Gestiona el traslado de órdenes de servicio, valores de copago, cuota moderadora, descuentos al paciente y homologaciones de productos, permitiendo reorganizar la distribución de cargos entre diferentes grupos de atención, entidades contratantes o terceros responsables dentro de un mismo proceso de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_DistributeFolio';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_DistributeFolio';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Folio de facturación; Control de ingresos; Distribución de servicios; Orden de servicio; Detalle de orden de servicio; Grupo de cuidado (CareGroup); Corte de cuentas; Homologación de servicios; Tarifa de producto (POS / No POS); Cuota de recuperación; Copago / cuota moderadora; Tercero pagador / Administradora de salud; IVA / impuestos; Liquidación de cuenta maestra; SOAT; Paquetes de servicios', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_DistributeFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @SourceFolioId no se resuelve en RevenueControlDetail → retorna StatusResult=0 con código ''{ERR1}'' y termina; si @TargetFolioId > 0 pero no existe el folio destino (tgCareGroupId NULL) → retorna StatusResult=0 con código ''{ERR2}'' y termina; si @TargetFolioId IS NULL/0 y @DistribType <> 4 → crea un nuevo RevenueControlDetail (folio destino) con FolioOrder=FolioQuantity+1 e incrementa Billing.RevenueControl.FolioQuantity else si @DistribType = 4 fija @TargetFolioId = -1 (no se crea folio destino); si Existe producto con RecordType=2 cuyo CareGroup destino no tiene cobertura en Inventory.ProductRateGeneral ni en Inventory.ProductRateDetail vigente para ServiceDate → retorna StatusResult=0 listando los productos no cubiertos (Code - Name) y termina; si Existe en folio destino una distribución con mismo ServiceOrderDetailId y RevenueControlDetailId → fusiona el ítem destino con el origen (acumula GrandTotalSalesPrice y GrandTotalDiscount si DistributionType<>5, fija ThirdPartyPercentage=100, ApplyRecoveryFee=1, RecoveryFeeType=1, DistributionType=1) y marca el origen para borrado; si @sourceDistDistributionType = 5 y existe en folio destino una distribución con CodeAssociateService = @GuidHomologation → homologa servicios asociados: si hay InvoiceDetail vigente marca IsDelete=1 en ServiceOrderDetail, si no agrega a lista de borrado; recalcula SubTotal/Total/GrandTotal sumando ambos valores y limpia CodeAssociateService; si @DistribType = 4 (distribución por unidades) → valida existencia de ServiceOrder; crea nuevo ServiceOrderDetail y ServiceOrderDetailDistribution con @DistributeQuantity unidades, calcula GrossValue/TaxValue vía Billing.SetValueSalesPrice y descuenta cantidades del origen; si @DistribType = 5 (distribución por homologación a producto POS) → crea un ServiceOrderDetail nuevo para el producto POS por defecto usando tarifa de Inventory.ProductRateDetail vigente; si @TargetFolioValue=0 marca el origen como SettlementType=3 (incluido 100%); requiere que el producto tenga ProductGroupId, si no aborta; si Otros @DistribType (no 4 ni 5) y no fusión → inserta una nueva ServiceOrderDetailDistribution en el folio destino con los valores de @TargetFolioValue y mantiene/actualiza la del origen; si @sourceDistGrandTotalSalesPrice = 0 y @sourceDistDistributionType <> 5 al final del ítem → marca la distribución origen para eliminación (listSourceDelete) else actualiza la distribución origen con descuentos, ThirdParty 100% y recoveryFee=1; si @srCareGroupId <> @tgCareGroupId y @DistribType <> 5 → fuerza @ChangeRateServicesNeccesary = 1 (corte de cuentas, requiere retarificación); si @ChangeRateServicesNeccesary = 1 → ejecuta Billing.SP_ChangeRateServices sobre el folio destino con el nuevo CareGroup y devuelve su resultado else ejecuta Billing.SP_UpdateRevenueControlDetailValues para folio destino (si DistribType<>4) y para folio origen, luego retorna StatusResult=1; si @DistribType <> 4 y @ChangeRateServicesNeccesary = 0 → actualiza valores agregados del folio destino vía SP_UpdateRevenueControlDetailValues; si @targetDistCUPSAssociateService = 1 (en rama de homologación distrib=5) → el ítem destino se elimina/marca y el origen absorbe sus valores else el ítem origen se elimina/marca y el destino absorbe los valores', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_DistributeFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_UpdateRevenueControlDetailValues; Billing.SP_ChangeRateServices; Billing.SetValueSalesPrice', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_DistributeFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControlDetail; Billing.RevenueControl; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; Billing.ServiceOrder; Billing.SettingsBilling; GeneralLedger.GeneralLedgerIVA; Inventory.InventoryProduct; Contract.CareGroup; Inventory.ProductRateGeneral; Inventory.ProductRateDetail; Billing.InvoiceDetail; Inventory.ProductGroup; Inventory.ProductRate; Billing.SetValueSalesPrice', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_DistributeFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_DistributeFolio';
-- GO
