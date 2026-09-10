-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-05-01
-- Description:	Retarifica un listado de servicios
-- =============================================
CREATE Procedure [Billing].[SP_ChangeRateServices]
	@RevenueControlDetailId Int,
	@CareGroupId Int,
	@PatientGenus Int,
	@PatientBirth Date,
	@ListHomologationsXml Xml,
	@OnlyRateChange Bit,
	@ThirdPartyPatientId Int,
	@HealthAdministratorId Int,
	@ListServiceOrderDetailWithQxXml Xml
AS
Begin
	Set Nocount On;
		
Begin Try
		
	Declare @StatusResult Bit = 0, @MessageResult Varchar(Max) = '', @Message Varchar(Max) = '', @ObjectEmbbeded Xml, @ObjectEmbbededAux Xml

		Declare @Rows Int, @RowId Int

		Declare @distributionToRetarific Table(Id Int Primary Key, 
			RevenueControlDetailId		Int,
			ServiceOrderDetailId		Int,
			Quantity					Int,
			GrandTotalSalesPrice		Decimal(20,2),
			GrandTotalDiscount			Decimal(20,2),
			DistributionType			Tinyint,
			ThirdPartySalesPrice		Decimal(20,2),
			ThirdPartyPercentage		Decimal(5, 2),
			ApplyRecoveryFee			Tinyint,
			RecoveryFeeType				Tinyint,
			SubTotalPatientSalesPrice	Decimal(20,2),
			PatientPercentage			Decimal(5, 2),
			LastCaregroupId				Int,
			--ServiceOrderDetail
			ServiceOrderId				Int,
			CareGroupId					Int,
			HealthAdministratorId		Int Null,
			ThirdPartyId				Int Null,
			ServiceType					Tinyint,
			RecordType					Tinyint,
			CUPSEntityId				Int Null,
			IPSServiceId				Int Null,
			HospitalStayId				Int Null,
			HospitalStayDetailId		Int Null,
			ControlExternalConsultation Tinyint Null,
			ControlExternalConsultationCode Decimal(18, 0) Null,
			CUPSAssociateService			Bit,
			CodeAssociateService			Varchar(50) Null,
			IsPackage						Bit,
			Packaging						Bit,
			PackageServiceOrderDetailId		Int Null,
			LiquidationType					Tinyint,
			Presentation					Tinyint Null,
			ProductId						Int Null,
			InvoicedQuantity				Int,
			SupplyQuantity					Int,
			DevolutionQuantity				Int,
			RateManualSalePrice				Decimal(20,2),
			CostValue						Decimal(20, 2),
			ServiceDate						DateTime,
			AuthorizationNumber				Varchar(20),
			PerformsFunctionalUnitId		Int,
			PerformsHealthProfessionalCode	Char(20),
			PerformsProfessionalSpecialty	Char(3),
			PerformsHealthProfessionalThirdPartyId	Int Null,
			BillingConceptId						Int Null,
			CostCenterId							Int,
			SettlementType							Tinyint,
			IncludeServiceOrderDetailId				Int Null,
			RecoveryRatio							Decimal(5, 2) Null,
			RateManualId							Int Null,
			RateManualType							Tinyint Null,
			RateManualDetailId						Int Null,
			DefinitionRateDetailId					Int Null,
			DefinitionRateDetailConditionId			Int Null,
			SubTotalSalesPrice_1					Decimal(20,2),
			ThirdPartyDiscount_1					Decimal(20,2),
			ThirdPartyDiscountPercentage			Decimal(5,2),
			TotalSalesPrice							Decimal(20,2),
			GrandTotalSalesPrice_1					Decimal(20,2),
			SurchargeApply							Bit,
			SurgicalInterventionType				Tinyint Null,
			SurgeryNumber							Tinyint,
			IsFirstEvent							Bit,
			IsAnnulled								Bit,
			IsDelete								Bit,
			IncomeMainAccountId						Int,			
			--Extender---------------------------------------			
			CodeNameSpeciality						Varchar(300) Null,
			CodeNameFunctionalUnit					Varchar(300) Null,
			CodeNameHealthAdministrator				Varchar(300) Null,				
			PreviusServiceOrderDetailId				Int Null,				
			CodeNameCareGroup						Varchar(320) Null,
			CodeNameCostCenter						Varchar(300) Null,
			CodeNameCups							Varchar(320) Null,
			CodeNameHealthProfessional				Varchar(300) Null,
			CodeNameIpsService						Varchar(300) Null,
			CodeNameProduct							Varchar(300) Null,				
			IsSOAT									Bit,
			ServiceOrderDetailSurgicalXml			Xml Null,
			RIASId									int,
			ContractDescriptionId					int,
			RoundService							int,
			------------------------------------------------
			GrossValue								numeric(20,2),
			TaxValue								numeric(20,2),
			IvaId									int)
				
		Declare @listDistributionDeleted Table(Id Int Primary Key)
		Declare @listServiceDetailSurgicalDeleted Table(Id Int Primary Key)
		Declare @listServiceDetailDeleted Table(Id Int Primary Key)
		Declare @ListHomologations Table([Service] Xml, Homologations Xml)
		
		Declare @ListServiceOrderDetailWithQx Table 
		(
			RowId Int,
			Id Int,
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
			CostValue Decimal(20,2),
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
			SubTotalSalesPrice Decimal(20,2),
			ThirdPartyDiscount Decimal(20,2),
			ThirdPartyDiscountPercentage Decimal(5, 2),
			TotalSalesPrice Decimal(20, 2),
			GrandTotalSalesPrice Decimal(20, 2),
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
			RoundService int,
			ServiceOrderDetailSurgicalXml Xml Null		
		)

		--Validar si el folio no ha sido facturado, o ha sufrido otro cambio
		IF NOT EXISTS (SELECT 1 FROM Billing.RevenueControlDetail WITH (NOLOCK) WHERE Id = @RevenueControlDetailId AND Status IN (1)) 
		BEGIN
			SELECT @MessageResult = CONCAT('El folio se encuentra ', CASE Status
																			WHEN 2 THEN 'Facturado'
																			WHEN 3 THEN 'Bloquado'
																			WHEN 4 THEN 'Anulado'
																			WHEN 5 THEN 'Reconocimiento Ingresos'
																			WHEN 6 THEN 'Factura Asociada'
																		END)
			FROM Billing.RevenueControlDetail WITH (NOLOCK)
			WHERE Id = @RevenueControlDetailId

			Select Convert(Bit, 0) As [StatusResult], ISNULL(@MessageResult, 'El folio no existe') As [MessageResult], Null As [Message], Null As [ObjectEmbbeded], Null As ObjectEmbbededAux
			RETURN
		END	

		DECLARE @SalesPriceInclude Bit 
		set @SalesPriceInclude = (SELECT top 1 cs.SalePriceIncludeTax from GeneralLedger.CompanySettings cs with(NOLOCK))


		If @ListHomologationsXml Is Not Null
			Insert Into @ListHomologations
			Values(@ListHomologationsXml.query('Homologacion/Service'), @ListHomologationsXml.query('Homologacion/Homologations'))
			
		If @ListServiceOrderDetailWithQxXml Is Not Null
			Insert Into @ListServiceOrderDetailWithQx
			Select 
				t.x.value('RowId[1]', 'Int'),
				t.x.value('Id[1]', 'Int'),
				t.x.value('ServiceOrderId[1]', 'Int'),
				t.x.value('CareGroupId[1]', 'Int'),
				t.x.value('HealthAdministratorId[1]', 'Int'),
				t.x.value('ThirdPartyId[1]', 'Int'),
				t.x.value('ServiceType[1]', 'Tinyint'),
				t.x.value('RecordType[1]', 'Tinyint'),
				t.x.value('CUPSEntityId[1]', 'Int'),
				t.x.value('IPSServiceId[1]', 'Int'),
				t.x.value('HospitalStayId[1]', 'Int'),
				t.x.value('HospitalStayDetailId[1]', 'Int'),
				t.x.value('ControlExternalConsultation[1]', 'Tinyint'),
				t.x.value('ControlExternalConsultationCode[1]', 'Decimal(18,0)'),
				t.x.value('CUPSAssociateService[1]', 'Bit'),
				t.x.value('CodeAssociateService[1]', 'Varchar(50)'),
				t.x.value('IsPackage[1]', 'Bit'),
				t.x.value('Packaging[1]', 'Bit'),
				t.x.value('PackageServiceOrderDetailId[1]', 'Int'),
				t.x.value('LiquidationType[1]', 'Tinyint'),
				t.x.value('Presentation[1]', 'Tinyint'),
				t.x.value('ProductId[1]', 'Int'),
				t.x.value('InvoicedQuantity[1]', 'Int'),
				t.x.value('SupplyQuantity[1]', 'Int'),
				t.x.value('DevolutionQuantity[1]', 'Int'),
				t.x.value('RateManualSalePrice[1]', 'Decimal(20,2)'),
				t.x.value('CostValue[1]', 'Decimal(20,2)'),
				convert(datetime, t.x.value('ServiceDate[1]', 'nvarchar(19)'), 103),
				t.x.value('AuthorizationNumber[1]', 'Varchar(20)'),
				t.x.value('PerformsFunctionalUnitId[1]', 'Int'),
				t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)'),
				t.x.value('PerformsProfessionalSpecialty[1]', 'Char(3)'),
				t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int'),
				t.x.value('BillingConceptId[1]', 'Int'),
				t.x.value('CostCenterId[1]', 'Int'),
				t.x.value('SettlementType[1]', 'Tinyint'),
				t.x.value('IncludeServiceOrderDetailId[1]', 'Int'),
				t.x.value('RecoveryRatio[1]', 'Decimal(5,2)'),
				t.x.value('RateManualId[1]', 'Int'),
				t.x.value('RateManualType[1]', 'Tinyint'),
				t.x.value('RateManualDetailId[1]', 'Int'),
				t.x.value('DefinitionRateDetailId[1]', 'Int'),
				t.x.value('DefinitionRateDetailConditionId[1]', 'Int'),
				t.x.value('SubTotalSalesPrice[1]', 'Decimal(20,2)'),
				t.x.value('ThirdPartyDiscount[1]', 'Decimal(20,2)'),
				t.x.value('ThirdPartyDiscountPercentage[1]', 'Decimal(5,2)'),
				t.x.value('TotalSalesPrice[1]', 'Decimal(20,2)'),
				t.x.value('GrandTotalSalesPrice[1]', 'Decimal(20,2)'),
				t.x.value('SurchargeApply[1]', 'Bit'),
				t.x.value('SurgicalInterventionType[1]', 'Tinyint'),
				t.x.value('SurgeryNumber[1]', 'Tinyint'),
				t.x.value('IsFirstEvent[1]', 'Bit'),
				t.x.value('IsAnnulled[1]', 'Bit'),
				t.x.value('IsDelete[1]', 'Bit'),
				t.x.value('IncomeMainAccountId[1]', 'Int'),
				t.x.value('CodeNameSpeciality[1]', 'Varchar(300)'),
				t.x.value('CodeNameFunctionalUnit[1]', 'Varchar(300)'),
				t.x.value('CodeNameHealthAdministrator[1]', 'Varchar(300)'),
				t.x.value('PreviusServiceOrderDetailId[1]', 'Int'),
				t.x.value('CodeNameCareGroup[1]', 'Varchar(320)'),
				t.x.value('CodeNameCostCenter[1]', 'Varchar(300)'),
				t.x.value('CodeNameCups[1]', 'Varchar(320)'),
				t.x.value('CodeNameHealthProfessional[1]', 'Varchar(300)'),
				t.x.value('CodeNameIpsService[1]', 'Varchar(300)'),
				t.x.value('CodeNameProduct[1]', 'Varchar(300)'),
				t.x.value('IsSOAT[1]', 'Bit'),
				t.x.value('RoundService[1]', 'int'),
				t.x.query('ServiceOrderDetailSurgicalXml/ServiceOrderDetailSurgical')
				From @ListServiceOrderDetailWithQxXml.nodes('/ListServiceOrderDetailWithQx/ServiceOrderDetail') t(x)
		
		--Almacenamos la informacion a retarificar en una tabla temporal para que no haya interbloqueos
		--Se filtra solo los detalles que no se han retarificado para éste grupo de atención
		Insert Into @distributionToRetarific		
			Select Distinct sodd.Id
				, sodd.RevenueControlDetailId
				, sodd.ServiceOrderDetailId
				, sodd.Quantity
				, sodd.GrandTotalSalesPrice
				, sodd.GrandTotalDiscount
				, sodd.DistributionType
				, sodd.ThirdPartySalesPrice
				, sodd.ThirdPartyPercentage
				, sodd.ApplyRecoveryFee
				, sodd.RecoveryFeeType
				, sodd.SubTotalPatientSalesPrice
				, sodd.PatientPercentage
				, sodd.LastCaregroupId
				, sod.ServiceOrderId
				, sod.CareGroupId
				, sod.HealthAdministratorId
				, sod.ThirdPartyId
				, sod.ServiceType
				, sod.RecordType
				, sod.CUPSEntityId
				, sod.IPSServiceId
				, sod.HospitalStayId
				, sod.HospitalStayDetailId
				, sod.ControlExternalConsultation
				, sod.ControlExternalConsultationCode
				, sod.CUPSAssociateService
				, sod.CodeAssociateService
				, sod.IsPackage
				, sod.Packaging
				, sod.PackageServiceOrderDetailId
				, sod.LiquidationType
				, sod.Presentation
				, sod.ProductId
				, sod.InvoicedQuantity
				, sod.SupplyQuantity
				, sod.DevolutionQuantity
				, sod.RateManualSalePrice
				, sod.CostValue
				, sod.ServiceDate
				, sod.AuthorizationNumber
				, sod.PerformsFunctionalUnitId
				, sod.PerformsHealthProfessionalCode
				, sod.PerformsProfessionalSpecialty
				, sod.PerformsHealthProfessionalThirdPartyId
				, sod.BillingConceptId
				, sod.CostCenterId
				, sod.SettlementType
				, sod.IncludeServiceOrderDetailId
				, sod.RecoveryRatio
				, sod.RateManualId
				, sod.RateManualType
				, sod.RateManualDetailId
				, sod.DefinitionRateDetailId
				, sod.DefinitionRateDetailConditionId
				, sod.SubTotalSalesPrice
				, sod.ThirdPartyDiscount
				, sod.ThirdPartyDiscountPercentage
				, sod.TotalSalesPrice
				, sod.GrandTotalSalesPrice
				, sod.SurchargeApply
				, sod.SurgicalInterventionType
				, sod.SurgeryNumber
				, sod.IsFirstEvent
				, sod.IsAnnulled
				, sod.IsDelete
				, sod.IncomeMainAccountId
				, Null
				, Null
				, Null
				, Null
				, Null
				, Null
				, Null
				, Null
				, Null
				, Null
				, 0
				, Null
				, ISNULL(rc.IDRIAS, 0)
				, ISNULL(ced.ContractDescriptionId, 0)
				, sod.RoundService
				, sod.GrossValue
				, sod.TaxValue
				, sod.IvaId
			From Billing.ServiceOrderDetailDistribution sodd With (Nolock)
			Inner Join Billing.ServiceOrderDetail sod With (Nolock) On sodd.ServiceOrderDetailId = sod.Id	
			left join .RIASCUPS rc With (Nolock) on rc.ID = sod.RIASCupsId	
			left join Contract.CUPSEntityContractDescriptions ced With (Nolock) on ced.Id = sod.CUPSEntityContractDescriptionId
			Where sodd.RevenueControlDetailId = @RevenueControlDetailId And (@OnlyRateChange = 1 OR sodd.LastCaregroupId <> @CareGroupId)
				And sod.IsDelete = 0 And sodd.DistributionType <> 5

		DECLARE @FolioCareGroupIdSource INT
		Select @FolioCareGroupIdSource = dtc.LastCaregroupId
		From @distributionToRetarific dtc

		
		-- Validar si los productos estan cubiertos
		IF EXISTS(SELECT 1
					FROM @distributionToRetarific sod
					JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON ip.Id=sod.ProductId
					JOIN Contract.CareGroup cg WITH (NOLOCK) ON @CareGroupId = cg.Id
					LEFT JOIN Inventory.ProductRateGeneral prg WITH (NOLOCK) ON cg.ProductRateId = prg.ProductRateId 
					AND ((prg.RuleType=1 and prg.ProductTypeId=ip.ProductTypeId) or (prg.RuleType=2 and prg.ProductGroupId= ip.ProductGroupId)
					or (prg.RuleType=3 and prg.ProductSubGroupId=ip.ProductSubGroupId)) AND CAST(sod.ServiceDate AS DATE) BETWEEN prg.InitialDate AND prg.EndDate
					WHERE sod.RecordType = 2 AND prg.Id IS NULL ) BEGIN

					DECLARE @TempIdOldProductRate TABLE (productId int)

					insert into @TempIdOldProductRate
					SELECT ip.Id
					FROM @distributionToRetarific sod
					JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON ip.Id=sod.ProductId
					JOIN Contract.CareGroup cg WITH (NOLOCK) ON @CareGroupId = cg.Id
					LEFT JOIN Inventory.ProductRateGeneral prg WITH (NOLOCK) ON cg.ProductRateId = prg.ProductRateId 
					AND ((prg.RuleType=1 and prg.ProductTypeId=ip.ProductTypeId) or (prg.RuleType=2 and prg.ProductGroupId= ip.ProductGroupId)
					or (prg.RuleType=3 and prg.ProductSubGroupId=ip.ProductSubGroupId)) AND CAST(sod.ServiceDate AS DATE) BETWEEN prg.InitialDate AND prg.EndDate
					WHERE sod.RecordType = 2 AND prg.Id IS NULL

					IF EXISTS 
						(
							SELECT 1 
							FROM @distributionToRetarific sod
							JOIN @TempIdOldProductRate tmp on sod.ProductId= tmp.productId
							JOIN Contract.CareGroup cg WITH (NOLOCK) ON @CareGroupId = cg.Id
							LEFT JOIN Inventory.ProductRateDetail prd WITH (NOLOCK) ON cg.ProductRateId = prd.ProductRateId AND sod.ProductId = prd.ProductId AND CAST(sod.ServiceDate AS DATE) BETWEEN prd.InitialDate AND prd.EndDate
							WHERE sod.RecordType = 2 AND prd.Id IS NULL
						) 
						BEGIN
							SELECT @MessageResult = STUFF((
									SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ip.Code, ' - ' , ip.Name)
									FROM @distributionToRetarific sod
									JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON sod.ProductId = ip.Id
									JOIN Contract.CareGroup cg WITH (NOLOCK) ON @CareGroupId = cg.Id
									LEFT JOIN Inventory.ProductRateDetail prd WITH (NOLOCK) ON cg.ProductRateId = prd.ProductRateId AND sod.ProductId = prd.ProductId AND CAST(sod.ServiceDate AS DATE) BETWEEN prd.InitialDate AND prd.EndDate
									WHERE sod.RecordType = 2 AND prd.Id IS NULL
									FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
							Select Convert(Bit, 0) As [StatusResult], CONCAT('Los siguientes productos no se encuentran cubiertos: ' + CHAR(13) + CHAR(10), @MessageResult) As [MessageResult], Null As [Message], Null As [ObjectEmbbeded], Null As ObjectEmbbededAux			
							RETURN
						END
		END
				
		-- ### CAMBIO 1 ### Se agrega la columna OldSurgicalId al final de la tabla temporal
		Declare @RetarificServiceOrderDetailSurgical Table
		(
			[Id] [int] NOT NULL,
			[ServiceOrderDetailId] [int] NOT NULL,
			[CodeNameIpsService] [Varchar](320) NULL,
			[IPSServiceId] [int] NOT NULL,
			[InvoicedQuantity] [int] NOT NULL,
			[LiquidationPercentage] [numeric](5, 2) NOT NULL,
			[RateManualSalePrice] [numeric](20,2) NOT NULL,
			[TotalSalesPrice] [numeric](20,2) NOT NULL,
			[PerformsHealthProfessionalCode] [char](20) NULL,
			[PerformsHealthProfessionalThirdPartyId] [int] NULL,
			[CostValue] [numeric](20, 2) NOT NULL,
			[BillingConceptId] [int] NOT NULL,
			[CostCenterId] [int] NOT NULL,
			[RateManualDetailSurgicalId] [int] NULL,
			[SurchargeApply] [bit] NOT NULL,
			[IncomeMainAccountId] [int] NOT NULL,
			ClassServiceIps Varchar(30),
			RoundService int,
			OldSurgicalId Int NULL -- ### CAMBIO 1 ### Guarda el Id del ServiceOrderDetailSurgical que esta fila reemplaza
		)

		Insert Into @RetarificServiceOrderDetailSurgical
			Select sos.Id, sos.ServiceOrderDetailId,Concat(ips.Code, ' - ', ips.[Name]), sos.IPSServiceId, sos.InvoicedQuantity
				, sos.LiquidationPercentage, sos.RateManualSalePrice, sos.TotalSalesPrice
				, sos.PerformsHealthProfessionalCode, sos.PerformsHealthProfessionalThirdPartyId
				, sos.CostValue, sos.BillingConceptId, sos.CostCenterId, sos.RateManualDetailSurgicalId
				, sos.SurchargeApply, sos.IncomeMainAccountId
				, Null
				, sos.RoundService
				, sos.Id -- ### CAMBIO 2 ### OldSurgicalId = su propio Id (aún es el registro original)
			From [Billing].ServiceOrderDetailSurgical sos WITH (NOLOCK)
			Inner Join @distributionToRetarific rt On rt.ServiceOrderDetailId = sos.ServiceOrderDetailId
			Left Join [Contract].IPSService ips With (Nolock) On sos.IPSServiceId = ips.Id
		
		Update rt 
			Set rt.ServiceOrderDetailSurgicalXml = 
			(
				Select Id
					, ServiceOrderDetailId
					, CodeNameIpsService
					, IPSServiceId
					, InvoicedQuantity
					, LiquidationPercentage
					, RateManualSalePrice
					, TotalSalesPrice
					, PerformsHealthProfessionalCode
					, PerformsHealthProfessionalThirdPartyId
					, CostValue
					, BillingConceptId
					, CostCenterId
					, RateManualDetailSurgicalId
					, SurchargeApply
					, IncomeMainAccountId
					, RoundService
				From @RetarificServiceOrderDetailSurgical rts
				Where rts.ServiceOrderDetailId = rt.ServiceOrderDetailId
				For Xml Path('RetarificServiceOrderDetailSurgical'), Elements
			)
		From @distributionToRetarific rt

        --Si hay items homologos, se eliminan todos los asociados y se dejan los principales (los que tienen CUPSAssociateService = False)
		Insert Into @listDistributionDeleted
			Select Id
			From @distributionToRetarific 
			Where CUPSAssociateService = 1

		Update @distributionToRetarific 
			Set IsDelete = 1
		Where CUPSAssociateService = 1 And IsAnnulled = 1

		Update @distributionToRetarific 
			Set IsFirstEvent = 1
		Where CUPSAssociateService = 1 And IsAnnulled = 0 And IsFirstEvent = 1
			And CodeAssociateService In 
			(
				Select CodeAssociateService 
				From @distributionToRetarific
				Where CodeAssociateService Is Not Null
			)

		Insert Into @listServiceDetailDeleted
			Select ServiceOrderDetailId 
			From @distributionToRetarific Where CUPSAssociateService = 1 And IsAnnulled = 0
		

		-- solo si vamos a retarificar eliminamos los surgical
		IF @FolioCareGroupIdSource <> @CareGroupId BEGIN
			Insert Into @listServiceDetailSurgicalDeleted
			Select rts.Id
			From @distributionToRetarific rt 
			Inner Join @RetarificServiceOrderDetailSurgical rts On rt.ServiceOrderDetailId = rts.ServiceOrderDetailId
			Where ServiceOrderDetailSurgicalXml Is Not Null And DistributionType <> 2
		END

		--Homologaciones
		Declare @NHomologaciones Int, @NServiceOrderDetailWithQX Int
		Select @NHomologaciones = Count(1) From @ListHomologations
		Select @NServiceOrderDetailWithQX = Count(1) From @ListServiceOrderDetailWithQx
		
		Declare @distributionToRetarificXml Xml = 
		(
			Select Id,
				RevenueControlDetailId,
				ServiceOrderDetailId,
				Quantity,
				GrandTotalSalesPrice,
				GrandTotalDiscount,
				DistributionType,
				ThirdPartySalesPrice,
				ThirdPartyPercentage,
				ApplyRecoveryFee,
				RecoveryFeeType,
				SubTotalPatientSalesPrice,
				PatientPercentage,
				LastCaregroupId,
				ServiceOrderId,
				CareGroupId,
				HealthAdministratorId,
				ThirdPartyId,
				ServiceType,
				RecordType,
				CUPSEntityId,
				IPSServiceId,
				HospitalStayId,
				HospitalStayDetailId,
				ControlExternalConsultation,
				ControlExternalConsultationCode,
				CUPSAssociateService,
				CodeAssociateService,
				IsPackage,
				Packaging,
				PackageServiceOrderDetailId,
				LiquidationType,
				Presentation,
				ProductId,
				InvoicedQuantity,
				SupplyQuantity,
				DevolutionQuantity,
				RateManualSalePrice,
				CostValue,
				ServiceDate,
				AuthorizationNumber,
				PerformsFunctionalUnitId,
				PerformsHealthProfessionalCode,
				PerformsProfessionalSpecialty,
				PerformsHealthProfessionalThirdPartyId,
				BillingConceptId,
				CostCenterId,
				SettlementType,
				IncludeServiceOrderDetailId,
				RecoveryRatio,
				RateManualId,
				RateManualType,
				RateManualDetailId,
				DefinitionRateDetailId,
				DefinitionRateDetailConditionId,
				SubTotalSalesPrice_1,
				ThirdPartyDiscount_1,
				ThirdPartyDiscountPercentage,
				TotalSalesPrice,
				GrandTotalSalesPrice_1,
				SurchargeApply,
				SurgicalInterventionType,
				SurgeryNumber,
				IsFirstEvent,
				IsAnnulled,
				IsDelete,
				IncomeMainAccountId,
				CodeNameSpeciality,
				CodeNameFunctionalUnit,
				CodeNameHealthAdministrator,
				PreviusServiceOrderDetailId,
				CodeNameCareGroup,
				CodeNameCostCenter,
				CodeNameCups,
				CodeNameHealthProfessional,
				CodeNameIpsService,
				CodeNameProduct,
				IsSOAT,
				Null,
				RIASId,
				ContractDescriptionId,
				RoundService,
				GrossValue,
				TaxValue,
				IvaId
			From @distributionToRetarific For Xml Path('DistributionToRetarific'), Elements			
		)
				
		Declare @SurgicalNewXml Xml = Null
		Declare @SurgicalRetarificXml Xml = 
		(
			Select Id
				, ServiceOrderDetailId
				, CodeNameIpsService
				, IPSServiceId
				, InvoicedQuantity
				, LiquidationPercentage
				, RateManualSalePrice
				, TotalSalesPrice
				, PerformsHealthProfessionalCode
				, PerformsHealthProfessionalThirdPartyId
				, CostValue
				, BillingConceptId
				, CostCenterId
				, RateManualDetailSurgicalId
				, SurchargeApply
				, IncomeMainAccountId
				, RoundService
			From @RetarificServiceOrderDetailSurgical rts
			For Xml Path('RetarificServiceOrderDetailSurgical'), Elements
		)		
		
		Declare @myResult Table (StatusResult Bit, MessageResult Varchar(Max), [DistributionToRetarific] Xml, [ListNewServiceOrderDetail] Xml)
		Declare @__StateResult Bit, @__MessageResult Varchar(Max), @__ObjectEmbbeded Xml, @__ObjectEmbbededAux Xml
		Declare @listNewServiceOrderDetail Table 
		(
			RowId Int Primary Key Identity(1, 1),
			Id Int,
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
			CostValue Decimal(20,2),
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
			SubTotalSalesPrice Decimal(20,2),
			ThirdPartyDiscount Decimal(20,2),
			ThirdPartyDiscountPercentage Decimal(5, 2),
			TotalSalesPrice Decimal(20,2),
			GrandTotalSalesPrice Decimal(20, 2),
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
			RoundService int,
			ServiceOrderDetailSurgicalXml Xml Null,
			-------------------------------------------
			GrossValue					numeric(20,2),
			TaxValue					numeric(20,2),
			IvaId						int
		)
		
		If @NHomologaciones = 0 And @NServiceOrderDetailWithQX = 0 
		Begin
			--Validamos las homologaciones			
			Declare @Homologations Table 
			(
				StatusResult Bit,
				MessageResult Varchar(255),
				MultipleHomologations Bit,
				ObjectEmbbeded Xml
			)
			
			Insert Into @Homologations
			Select StatusResult, MessageResult, MultipleHomologations, ObjectEmbbeded
			From [Contract].ValidateHomologation(@RevenueControlDetailId, @CareGroupId, @distributionToRetarificXml)
			
			If Exists (Select 1 From @Homologations Where ObjectEmbbeded Is Not Null And Cast(ObjectEmbbeded As varchar(Max)) <> '') Begin				
				Set @ObjectEmbbeded = Cast((
						Select ObjectEmbbeded.query('Homologacion/Service'), ObjectEmbbeded.query('Homologacion/Homologations')
						From @Homologations
						Where ObjectEmbbeded Is Not Null And Cast(ObjectEmbbeded As varchar(Max)) <> ''
						For Xml Path('Homologacion'), Elements
				) As Xml)
			End
			Else BEGIN
				Set @ObjectEmbbeded = Null
			End
			Set @MessageResult = (Select Top 1 MessageResult From @Homologations Where MessageResult Is Not Null And MessageResult <> '')			
			--Si hubo errores

			If Exists (Select StatusResult From @Homologations Where StatusResult = 0) Begin			
				--Mensajes de error en la homologación
				Set @Message = '{ERR1}'
				Select @StatusResult As [StatusResult], @MessageResult As [MessageResult], @Message As [Message], @ObjectEmbbeded As [ObjectEmbbeded], @ObjectEmbbededAux As ObjectEmbbededAux
				Return
			End
			Else If (Select Top 1 MultipleHomologations From @Homologations) = 1 Begin			
				--Si existen homologaciones múltiples
				Set @Message = '{ERR2}'
				Select @StatusResult As [StatusResult], @MessageResult As [MessageResult], @Message As [Message], @ObjectEmbbeded As [ObjectEmbbeded], @ObjectEmbbededAux As ObjectEmbbededAux
				Return
			End
			Else BEGIN			
				exec [Billing].[SP_RunChangeRatesServices_Output] @RevenueControlDetailId, @CareGroupId, @OnlyRateChange
					, @ObjectEmbbeded, @PatientGenus, @PatientBirth, @distributionToRetarificXml, @SurgicalRetarificXml
					, @__StateResult Output, @__MessageResult Output, @__ObjectEmbbeded Output, @__ObjectEmbbededAux Output
				
				Set @MessageResult = @__MessageResult
				
				If @__StateResult = 0 Begin				
					Select @StatusResult As [StatusResult], @MessageResult As [MessageResult], @Message As [Message], @ObjectEmbbeded As [ObjectEmbbeded], @ObjectEmbbededAux As ObjectEmbbededAux
					Return
				End
				
				If @MessageResult <> '' Begin
					Set @Message = '{ERR3}'--Errores de retarificación
					Select @StatusResult As [StatusResult], @MessageResult As [MessageResult], @Message As [Message], @ObjectEmbbeded As [ObjectEmbbeded], @ObjectEmbbededAux As ObjectEmbbededAux
					Return
				End
				--print cast(@__ObjectEmbbeded as VARCHAR(MAX))
				Update rt Set RevenueControlDetailId = t.x.value('RevenueControlDetailId[1]', 'Int'),
					ServiceOrderDetailId = t.x.value('ServiceOrderDetailId[1]', 'Int'),
					Quantity = t.x.value('Quantity[1]', 'Int'),
					GrandTotalSalesPrice = t.x.value('GrandTotalSalesPrice[1]', 'Decimal(20,2)'),
					GrandTotalDiscount = t.x.value('GrandTotalDiscount[1]', 'Decimal(20,2)'),
					DistributionType = t.x.value('DistributionType[1]', 'Tinyint'),
					ThirdPartySalesPrice = t.x.value('ThirdPartySalesPrice[1]', 'Decimal(20,2)'),
					ThirdPartyPercentage = t.x.value('ThirdPartyPercentage[1]', 'Decimal(5,2)'),
					ApplyRecoveryFee = t.x.value('ApplyRecoveryFee[1]', 'Tinyint'),
					RecoveryFeeType = t.x.value('RecoveryFeeType[1]', 'Tinyint'),
					SubTotalPatientSalesPrice = t.x.value('SubTotalPatientSalesPrice[1]', 'Decimal(20,2)'),
					PatientPercentage = t.x.value('PatientPercentage[1]', 'Decimal(5,2)'),
					LastCaregroupId = t.x.value('LastCaregroupId[1]', 'Int'),
					ServiceOrderId = t.x.value('ServiceOrderId[1]', 'Int'),
					CareGroupId = t.x.value('CareGroupId[1]', 'Int'),
					HealthAdministratorId = t.x.value('HealthAdministratorId[1]', 'Int'),
					ThirdPartyId = t.x.value('ThirdPartyId[1]', 'Int'),
					ServiceType = t.x.value('ServiceType[1]', 'Tinyint'),
					RecordType = t.x.value('RecordType[1]', 'Tinyint'),
					CUPSEntityId = t.x.value('CUPSEntityId[1]', 'Int'),
					IPSServiceId = t.x.value('IPSServiceId[1]', 'Int'),
					HospitalStayId = t.x.value('HospitalStayId[1]', 'Int'),
					HospitalStayDetailId = t.x.value('HospitalStayDetailId[1]', 'Int'),
					ControlExternalConsultation = t.x.value('ControlExternalConsultation[1]', 'Tinyint'),
					ControlExternalConsultationCode = t.x.value('ControlExternalConsultationCode[1]', 'Decimal(18,0)'),
					CUPSAssociateService = t.x.value('CUPSAssociateService[1]', 'Bit'),
					CodeAssociateService = t.x.value('CodeAssociateService[1]', 'Varchar(50)'),
					IsPackage = t.x.value('IsPackage[1]', 'Bit'),
					Packaging = t.x.value('Packaging[1]', 'Bit'),
					PackageServiceOrderDetailId = t.x.value('PackageServiceOrderDetailId[1]', 'Int'),
					LiquidationType = t.x.value('LiquidationType[1]', 'Tinyint'),
					Presentation = t.x.value('Presentation[1]', 'Tinyint'),
					ProductId = t.x.value('ProductId[1]', 'Int'),
					InvoicedQuantity = t.x.value('InvoicedQuantity[1]', 'Int'),
					SupplyQuantity = t.x.value('SupplyQuantity[1]', 'Int'),
					DevolutionQuantity = t.x.value('DevolutionQuantity[1]', 'Int'),
					RateManualSalePrice = t.x.value('RateManualSalePrice[1]', 'Decimal(20,2)'),
					CostValue = t.x.value('CostValue[1]', 'Decimal(20,2)'),
					ServiceDate = t.x.value('ServiceDate[1]', 'DateTime'),
					AuthorizationNumber = t.x.value('AuthorizationNumber[1]', 'Varchar(20)'),
					PerformsFunctionalUnitId = t.x.value('PerformsFunctionalUnitId[1]', 'Int'),
					PerformsHealthProfessionalCode = t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)'),
					PerformsProfessionalSpecialty = t.x.value('PerformsProfessionalSpecialty[1]', 'Char(3)'),
					PerformsHealthProfessionalThirdPartyId = t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int'),
					BillingConceptId = t.x.value('BillingConceptId[1]', 'Int'),
					CostCenterId = t.x.value('CostCenterId[1]', 'Int'),
					SettlementType = t.x.value('SettlementType[1]', 'Tinyint'),
					IncludeServiceOrderDetailId = t.x.value('IncludeServiceOrderDetailId[1]', 'Int'),
					RecoveryRatio = t.x.value('RecoveryRatio[1]', 'Decimal(5,2)'),
					RateManualId = t.x.value('RateManualId[1]', 'Int'),
					RateManualType = t.x.value('RateManualType[1]', 'Tinyint'),
					RateManualDetailId = t.x.value('RateManualDetailId[1]', 'Int'),
					DefinitionRateDetailId = t.x.value('DefinitionRateDetailId[1]', 'Int'),
					DefinitionRateDetailConditionId = t.x.value('DefinitionRateDetailConditionId[1]', 'Int'),
					SubTotalSalesPrice_1 = t.x.value('SubTotalSalesPrice_1[1]', 'Decimal(20,2)'),
					ThirdPartyDiscount_1 = t.x.value('ThirdPartyDiscount_1[1]', 'Decimal(20,2)'),
					ThirdPartyDiscountPercentage = t.x.value('ThirdPartyDiscountPercentage[1]', 'Decimal(5,2)'),
					TotalSalesPrice = t.x.value('TotalSalesPrice[1]', 'Decimal(20,2)'),
					GrandTotalSalesPrice_1 = t.x.value('GrandTotalSalesPrice_1[1]', 'Decimal(20,2)'),
					SurchargeApply = t.x.value('SurchargeApply[1]', 'Bit'),
					SurgicalInterventionType = t.x.value('SurgicalInterventionType[1]', 'Tinyint'),
					SurgeryNumber = t.x.value('SurgeryNumber[1]', 'Tinyint'),
					IsFirstEvent = t.x.value('IsFirstEvent[1]', 'Bit'),
					IsAnnulled = t.x.value('IsAnnulled[1]', 'Bit'),
					IsDelete = t.x.value('IsDelete[1]', 'Bit'),
					IncomeMainAccountId = t.x.value('IncomeMainAccountId[1]', 'Int'),
					CodeNameSpeciality = t.x.value('CodeNameSpeciality[1]', 'Varchar(300)'),
					CodeNameFunctionalUnit = t.x.value('CodeNameFunctionalUnit[1]', 'Varchar(300)'),
					CodeNameHealthAdministrator = t.x.value('CodeNameHealthAdministrator[1]', 'Varchar(300)'),
					PreviusServiceOrderDetailId = t.x.value('PreviusServiceOrderDetailId[1]', 'Int'),
					CodeNameCareGroup = t.x.value('CodeNameCareGroup[1]', 'Varchar(320)'),
					CodeNameCostCenter = t.x.value('CodeNameCostCenter[1]', 'Varchar(300)'),
					CodeNameCups = t.x.value('CodeNameCups[1]', 'Varchar(320)'),
					CodeNameHealthProfessional = t.x.value('CodeNameHealthProfessional[1]', 'Varchar(300)'),
					CodeNameIpsService = t.x.value('CodeNameIpsService[1]', 'Varchar(300)'),
					CodeNameProduct = t.x.value('CodeNameProduct[1]', 'Varchar(300)'),
					IsSOAT = t.x.value('IsSOAT[1]', 'Bit'),
					RoundService = t.x.value('RoundService[1]', 'int'),
					ServiceOrderDetailSurgicalXml = t.x.query('ServiceOrderDetailSurgicalXml/ServiceOrderDetailSurgical'),
					GrossValue =t.x.value('GrossValue[1]', 'numeric(20,2)'),
					TaxValue =t.x.value('TaxValue[1]', 'numeric(20,2)'),
					IvaId = t.x.value('IvaId[1]', 'int')
				From @__ObjectEmbbeded.nodes('ServiceOrderDetailDistribution') t(x)
				Inner Join @distributionToRetarific rt On rt.ServiceOrderDetailId = t.x.value('ServiceOrderDetailId[1]', 'Int')
				--SELECT * from @distributionToRetarific
				Insert Into @listNewServiceOrderDetail
				Select 
					t.x.value('Id[1]', 'Int'),
					t.x.value('ServiceOrderId[1]', 'Int'),
					t.x.value('CareGroupId[1]', 'Int'),
					t.x.value('HealthAdministratorId[1]', 'Int'),
					t.x.value('ThirdPartyId[1]', 'Int'),
					t.x.value('ServiceType[1]', 'Tinyint'),
					t.x.value('RecordType[1]', 'Tinyint'),
					t.x.value('CUPSEntityId[1]', 'Int'),
					t.x.value('IPSServiceId[1]', 'Int'),
					t.x.value('HospitalStayId[1]', 'Int'),
					t.x.value('HospitalStayDetailId[1]', 'Int'),
					t.x.value('ControlExternalConsultation[1]', 'Tinyint'),
					t.x.value('ControlExternalConsultationCode[1]', 'Decimal(18,0)'),
					t.x.value('CUPSAssociateService[1]', 'Bit'),
					t.x.value('CodeAssociateService[1]', 'Varchar(50)'),
					t.x.value('IsPackage[1]', 'Bit'),
					t.x.value('Packaging[1]', 'Bit'),
					t.x.value('PackageServiceOrderDetailId[1]', 'Int'),
					t.x.value('LiquidationType[1]', 'Tinyint'),
					t.x.value('Presentation[1]', 'Tinyint'),
					t.x.value('ProductId[1]', 'Int'),
					t.x.value('InvoicedQuantity[1]', 'Int'),
					t.x.value('SupplyQuantity[1]', 'Int'),
					t.x.value('DevolutionQuantity[1]', 'Int'),
					t.x.value('RateManualSalePrice[1]', 'Decimal(20,2)'),
					t.x.value('CostValue[1]', 'Decimal(20,2)'),
					t.x.value('ServiceDate[1]', 'DateTime'),
					t.x.value('AuthorizationNumber[1]', 'Varchar(20)'),
					t.x.value('PerformsFunctionalUnitId[1]', 'Int'),
					t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)'),
					t.x.value('PerformsProfessionalSpecialty[1]', 'Char(3)'),
					t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int'),
					t.x.value('BillingConceptId[1]', 'Int'),
					t.x.value('CostCenterId[1]', 'Int'),
					t.x.value('SettlementType[1]', 'Tinyint'),
					t.x.value('IncludeServiceOrderDetailId[1]', 'Int'),
					t.x.value('RecoveryRatio[1]', 'Decimal(5,2)'),
					t.x.value('RateManualId[1]', 'Int'),
					t.x.value('RateManualType[1]', 'Tinyint'),
					t.x.value('RateManualDetailId[1]', 'Int'),
					t.x.value('DefinitionRateDetailId[1]', 'Int'),
					t.x.value('DefinitionRateDetailConditionId[1]', 'Int'),
					t.x.value('SubTotalSalesPrice[1]', 'Decimal(20,2)'),
					t.x.value('ThirdPartyDiscount[1]', 'Decimal(20,2)'),
					t.x.value('ThirdPartyDiscountPercentage[1]', 'Decimal(5,2)'),
					t.x.value('TotalSalesPrice[1]', 'Decimal(20,2)'),
					t.x.value('GrandTotalSalesPrice[1]', 'Decimal(20,2)'),
					t.x.value('SurchargeApply[1]', 'Bit'),
					t.x.value('SurgicalInterventionType[1]', 'Tinyint'),
					t.x.value('SurgeryNumber[1]', 'Tinyint'),
					t.x.value('IsFirstEvent[1]', 'Bit'),
					t.x.value('IsAnnulled[1]', 'Bit'),
					t.x.value('IsDelete[1]', 'Bit'),
					t.x.value('IncomeMainAccountId[1]', 'Int'),
					t.x.value('CodeNameSpeciality[1]', 'Varchar(300)'),
					t.x.value('CodeNameFunctionalUnit[1]', 'Varchar(300)'),
					t.x.value('CodeNameHealthAdministrator[1]', 'Varchar(300)'),
					t.x.value('PreviusServiceOrderDetailId[1]', 'Int'),
					t.x.value('CodeNameCareGroup[1]', 'Varchar(320)'),
					t.x.value('CodeNameCostCenter[1]', 'Varchar(300)'),
					t.x.value('CodeNameCups[1]', 'Varchar(320)'),
					t.x.value('CodeNameHealthProfessional[1]', 'Varchar(300)'),
					t.x.value('CodeNameIpsService[1]', 'Varchar(300)'),
					t.x.value('CodeNameProduct[1]', 'Varchar(300)'),
					t.x.value('IsSOAT[1]', 'Bit'),
					t.x.value('RoundService[1]', 'int'),
					t.x.query('ServiceOrderDetailSurgicalXml/ServiceOrderDetailSurgical'),
					t.x.value('GrossValue[1]', 'numeric(20,2)'),
					t.x.value('TaxValue[1]', 'numeric(20,2)'),
					t.x.value('IvaId[1]', 'int')
				From @__ObjectEmbbededAux.nodes('/ListNewServiceOrderDetail') t(x)
					
				Delete From @RetarificServiceOrderDetailSurgical				
				Set @Rows = 1
				Set @RowId = 1

				While @Rows > 0
				begin
					Select Top 1 @RowId = RowId, @SurgicalNewXml = ServiceOrderDetailSurgicalXml From @listNewServiceOrderDetail Where RowId >= @RowId Order By RowId
					Set @Rows = @@ROWCOUNT
					If @Rows = 0 
						Break
					-- ### CAMBIO 3 (1/3) ### Se agrega la subconsulta final para poblar OldSurgicalId
					Insert Into @RetarificServiceOrderDetailSurgical
					Select t.x.value('Id[1]', 'Int'),
						t.x.value('ServiceOrderDetailId[1]', 'Int'),
						t.x.value('CodeNameIpsService[1]', 'Varchar(320)'),
						t.x.value('IPSServiceId[1]', 'Int'),
						t.x.value('InvoicedQuantity[1]', 'Int'),
						t.x.value('LiquidationPercentage[1]', 'numeric(5,2)'),
						t.x.value('RateManualSalePrice[1]', 'numeric(20,2)'),
						t.x.value('TotalSalesPrice[1]', 'numeric(20,2)'),
						t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)'),
						t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int'),
						t.x.value('CostValue[1]', 'numeric(20,2)'),
						t.x.value('BillingConceptId[1]', 'Int'),
						t.x.value('CostCenterId[1]', 'Int'),
						t.x.value('RateManualDetailSurgicalId[1]', 'Int'),
						t.x.value('SurchargeApply[1]', 'Bit'),
						t.x.value('IncomeMainAccountId[1]', 'Int'),
						t.x.value('ClassServiceIps[1]', 'Varchar(30)'),
						t.x.value('RoundService[1]', 'Int'),
						( -- ### CAMBIO 3 (1/3) ### Busca el Id viejo equivalente por ServiceOrderDetailId + IPSServiceId
							SELECT TOP 1 sosOld.Id
							FROM Billing.ServiceOrderDetailSurgical sosOld WITH (NOLOCK)
							WHERE sosOld.ServiceOrderDetailId = t.x.value('ServiceOrderDetailId[1]', 'Int')
							  AND sosOld.IPSServiceId = t.x.value('IPSServiceId[1]', 'Int')
						)
					From @SurgicalNewXml.nodes('ServiceOrderDetailSurgical') t(x)		
					Set @RowId += 1
				End

				If Exists (Select Id From @listNewServiceOrderDetail Where Presentation = 2 And SettlementType = 1) Begin
					--Se encontró que hay al menos un detalle de orden de servicio quirurgico entonces retorno las ordenes de servicio para elegir los servicios a cobrar
					
					Delete From @RetarificServiceOrderDetailSurgical Where Id In (Select Id From @listServiceDetailSurgicalDeleted)
					Update ln Set ServiceOrderDetailSurgicalXml = (
						Select * 
						From @RetarificServiceOrderDetailSurgical rt 
						Where rt.ServiceOrderDetailId = ln.Id
						For Xml Path('ServiceOrderDetailSurgical'), Elements
					)
					From @listNewServiceOrderDetail ln
					Set @__ObjectEmbbededAux = (
						Select *
						From @listNewServiceOrderDetail
						For Xml Path('ListNewServiceOrderDetail'), Elements
					)
					Set @ObjectEmbbeded = Null
					Set @ObjectEmbbededAux = @__ObjectEmbbededAux
					Set @Message = '{ERR4}'
					Select @StatusResult As [StatusResult], @MessageResult As [MessageResult], @Message As [Message], @ObjectEmbbeded As [ObjectEmbbeded], @ObjectEmbbededAux As ObjectEmbbededAux
					Return
				End				
			End

		End
		Else Begin
			
			Declare @RateManualSalePriceQx Decimal(18,0),
					@SubTotalSalesPriceQx Decimal(18,2), 
					@ThirdPartyDiscountQx Decimal(18,0), 
					@TotalSalesPriceQx Decimal(18,2), 
					@GrandTotalSalesPriceQx Decimal(18,0),
					@CodeAssociateServiceQx Varchar(50),
					@CareGroupIdQx Int,
					@IPSServiceIdQx Int,
					@PreviusServiceOrderDetailId Int,
					@ServiceOrderDetailIdQx Int,
					@ServiceOrderDetailSurgicalXmlQx Xml

			--Cuando el usuario envia las homologaciones seleccionadas
			If Not Exists (Select Id From @ListServiceOrderDetailWithQx) 
			Begin				
				exec [Billing].[SP_RunChangeRatesServices_Output] @RevenueControlDetailId, @CareGroupId, 0
					, @ListHomologationsXml, @PatientGenus, @PatientBirth, @distributionToRetarificXml, @SurgicalRetarificXml
					, @__StateResult Output, @__MessageResult Output, @__ObjectEmbbeded Output, @__ObjectEmbbededAux Output

				Set @MessageResult = @__MessageResult
				
				If @__StateResult = 0 Begin			
					Select @StatusResult As [StatusResult], @MessageResult As [MessageResult], @Message As [Message], @ObjectEmbbeded As [ObjectEmbbeded], @ObjectEmbbededAux As ObjectEmbbededAux
					Return
				End
				If @MessageResult <> '' Begin
					Set @Message = '{ERR3}'--Errores de retarificación
					Select @StatusResult As [StatusResult], @MessageResult As [MessageResult], @Message As [Message], @ObjectEmbbeded As [ObjectEmbbeded], @ObjectEmbbededAux As ObjectEmbbededAux
					Return
				End
				Update rt Set RevenueControlDetailId = t.x.value('RevenueControlDetailId[1]', 'Int'),
					ServiceOrderDetailId = t.x.value('ServiceOrderDetailId[1]', 'Int'),
					Quantity = t.x.value('Quantity[1]', 'Int'),
					GrandTotalSalesPrice = t.x.value('GrandTotalSalesPrice[1]', 'Decimal(20,2)'),
					GrandTotalDiscount = t.x.value('GrandTotalDiscount[1]', 'Decimal(20,2)'),
					DistributionType = t.x.value('DistributionType[1]', 'Tinyint'),
					ThirdPartySalesPrice = t.x.value('ThirdPartySalesPrice[1]', 'Decimal(20,2)'),
					ThirdPartyPercentage = t.x.value('ThirdPartyPercentage[1]', 'Decimal(5,2)'),
					ApplyRecoveryFee = t.x.value('ApplyRecoveryFee[1]', 'Tinyint'),
					RecoveryFeeType = t.x.value('RecoveryFeeType[1]', 'Tinyint'),
					SubTotalPatientSalesPrice = t.x.value('SubTotalPatientSalesPrice[1]', 'Decimal(20,2)'),
					PatientPercentage = t.x.value('PatientPercentage[1]', 'Decimal(5,2)'),
					LastCaregroupId = t.x.value('LastCaregroupId[1]', 'Int'),
					ServiceOrderId = t.x.value('ServiceOrderId[1]', 'Int'),
					CareGroupId = t.x.value('CareGroupId[1]', 'Int'),
					HealthAdministratorId = t.x.value('HealthAdministratorId[1]', 'Int'),
					ThirdPartyId = t.x.value('ThirdPartyId[1]', 'Int'),
					ServiceType = t.x.value('ServiceType[1]', 'Tinyint'),
					RecordType = t.x.value('RecordType[1]', 'Tinyint'),
					CUPSEntityId = t.x.value('CUPSEntityId[1]', 'Int'),
					IPSServiceId = t.x.value('IPSServiceId[1]', 'Int'),
					HospitalStayId = t.x.value('HospitalStayId[1]', 'Int'),
					HospitalStayDetailId = t.x.value('HospitalStayDetailId[1]', 'Int'),
					ControlExternalConsultation = t.x.value('ControlExternalConsultation[1]', 'Tinyint'),
					ControlExternalConsultationCode = t.x.value('ControlExternalConsultationCode[1]', 'Decimal(18,0)'),
					CUPSAssociateService = t.x.value('CUPSAssociateService[1]', 'Bit'),
					CodeAssociateService = t.x.value('CodeAssociateService[1]', 'Varchar(50)'),
					IsPackage = t.x.value('IsPackage[1]', 'Bit'),
					Packaging = t.x.value('Packaging[1]', 'Bit'),
					PackageServiceOrderDetailId = t.x.value('PackageServiceOrderDetailId[1]', 'Int'),
					LiquidationType = t.x.value('LiquidationType[1]', 'Tinyint'),
					Presentation = t.x.value('Presentation[1]', 'Tinyint'),
					ProductId = t.x.value('ProductId[1]', 'Int'),
					InvoicedQuantity = t.x.value('InvoicedQuantity[1]', 'Int'),
					SupplyQuantity = t.x.value('SupplyQuantity[1]', 'Int'),
					DevolutionQuantity = t.x.value('DevolutionQuantity[1]', 'Int'),
					RateManualSalePrice = t.x.value('RateManualSalePrice[1]', 'Decimal(20,2)'),
					CostValue = t.x.value('CostValue[1]', 'Decimal(20,2)'),
					ServiceDate = t.x.value('ServiceDate[1]', 'DateTime'),
					AuthorizationNumber = t.x.value('AuthorizationNumber[1]', 'Varchar(20)'),
					PerformsFunctionalUnitId = t.x.value('PerformsFunctionalUnitId[1]', 'Int'),
					PerformsHealthProfessionalCode = t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)'),
					PerformsProfessionalSpecialty = t.x.value('PerformsProfessionalSpecialty[1]', 'Char(3)'),
					PerformsHealthProfessionalThirdPartyId = t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int'),
					BillingConceptId = t.x.value('BillingConceptId[1]', 'Int'),
					CostCenterId = t.x.value('CostCenterId[1]', 'Int'),
					SettlementType = t.x.value('SettlementType[1]', 'Tinyint'),
					IncludeServiceOrderDetailId = t.x.value('IncludeServiceOrderDetailId[1]', 'Int'),
					RecoveryRatio = t.x.value('RecoveryRatio[1]', 'Decimal(5,2)'),
					RateManualId = t.x.value('RateManualId[1]', 'Int'),
					RateManualType = t.x.value('RateManualType[1]', 'Tinyint'),
					RateManualDetailId = t.x.value('RateManualDetailId[1]', 'Int'),
					DefinitionRateDetailId = t.x.value('DefinitionRateDetailId[1]', 'Int'),
					DefinitionRateDetailConditionId = t.x.value('DefinitionRateDetailConditionId[1]', 'Int'),
					SubTotalSalesPrice_1 = t.x.value('SubTotalSalesPrice_1[1]', 'Decimal(20,2)'),
					ThirdPartyDiscount_1 = t.x.value('ThirdPartyDiscount_1[1]', 'Decimal(20,2)'),
					ThirdPartyDiscountPercentage = t.x.value('ThirdPartyDiscountPercentage[1]', 'Decimal(5,2)'),
					TotalSalesPrice = t.x.value('TotalSalesPrice[1]', 'Decimal(20,2)'),
					GrandTotalSalesPrice_1 = t.x.value('GrandTotalSalesPrice_1[1]', 'Decimal(20,2)'),
					SurchargeApply = t.x.value('SurchargeApply[1]', 'Bit'),
					SurgicalInterventionType = t.x.value('SurgicalInterventionType[1]', 'Tinyint'),
					SurgeryNumber = t.x.value('SurgeryNumber[1]', 'Tinyint'),
					IsFirstEvent = t.x.value('IsFirstEvent[1]', 'Bit'),
					IsAnnulled = t.x.value('IsAnnulled[1]', 'Bit'),
					IsDelete = t.x.value('IsDelete[1]', 'Bit'),
					IncomeMainAccountId = t.x.value('IncomeMainAccountId[1]', 'Int'),
					CodeNameSpeciality = t.x.value('CodeNameSpeciality[1]', 'Varchar(300)'),
					CodeNameFunctionalUnit = t.x.value('CodeNameFunctionalUnit[1]', 'Varchar(300)'),
					CodeNameHealthAdministrator = t.x.value('CodeNameHealthAdministrator[1]', 'Varchar(300)'),
					PreviusServiceOrderDetailId = t.x.value('PreviusServiceOrderDetailId[1]', 'Int'),
					CodeNameCareGroup = t.x.value('CodeNameCareGroup[1]', 'Varchar(320)'),
					CodeNameCostCenter = t.x.value('CodeNameCostCenter[1]', 'Varchar(300)'),
					CodeNameCups = t.x.value('CodeNameCups[1]', 'Varchar(320)'),
					CodeNameHealthProfessional = t.x.value('CodeNameHealthProfessional[1]', 'Varchar(300)'),
					CodeNameIpsService = t.x.value('CodeNameIpsService[1]', 'Varchar(300)'),
					CodeNameProduct = t.x.value('CodeNameProduct[1]', 'Varchar(300)'),
					IsSOAT = t.x.value('IsSOAT[1]', 'Bit'),
					RoundService = t.x.value('RoundService[1]', 'int'),
					GrossValue = t.x.value('GrossValue[1]', 'numeric(20, 2)'),
					ServiceOrderDetailSurgicalXml = t.x.query('ServiceOrderDetailSurgicalXml/ServiceOrderDetailSurgical')
				From @__ObjectEmbbeded.nodes('ServiceOrderDetailDistribution') t(x)
				Inner Join @distributionToRetarific rt On rt.ServiceOrderDetailId = t.x.value('ServiceOrderDetailId[1]', 'Int')

				Insert Into @listNewServiceOrderDetail
				Select 
					t.x.value('Id[1]', 'Int'),
					t.x.value('ServiceOrderId[1]', 'Int'),
					t.x.value('CareGroupId[1]', 'Int'),
					t.x.value('HealthAdministratorId[1]', 'Int'),
					t.x.value('ThirdPartyId[1]', 'Int'),
					t.x.value('ServiceType[1]', 'Tinyint'),
					t.x.value('RecordType[1]', 'Tinyint'),
					t.x.value('CUPSEntityId[1]', 'Int'),
					t.x.value('IPSServiceId[1]', 'Int'),
					t.x.value('HospitalStayId[1]', 'Int'),
					t.x.value('HospitalStayDetailId[1]', 'Int'),
					t.x.value('ControlExternalConsultation[1]', 'Tinyint'),
					t.x.value('ControlExternalConsultationCode[1]', 'Decimal(18,0)'),
					t.x.value('CUPSAssociateService[1]', 'Bit'),
					t.x.value('CodeAssociateService[1]', 'Varchar(50)'),
					t.x.value('IsPackage[1]', 'Bit'),
					t.x.value('Packaging[1]', 'Bit'),
					t.x.value('PackageServiceOrderDetailId[1]', 'Int'),
					t.x.value('LiquidationType[1]', 'Tinyint'),
					t.x.value('Presentation[1]', 'Tinyint'),
					t.x.value('ProductId[1]', 'Int'),
					t.x.value('InvoicedQuantity[1]', 'Int'),
					t.x.value('SupplyQuantity[1]', 'Int'),
					t.x.value('DevolutionQuantity[1]', 'Int'),
					t.x.value('RateManualSalePrice[1]', 'Decimal(20,2)'),
					t.x.value('CostValue[1]', 'Decimal(20,2)'),
					t.x.value('ServiceDate[1]', 'DateTime'),
					t.x.value('AuthorizationNumber[1]', 'Varchar(20)'),
					t.x.value('PerformsFunctionalUnitId[1]', 'Int'),
					t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)'),
					t.x.value('PerformsProfessionalSpecialty[1]', 'Char(3)'),
					t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int'),
					t.x.value('BillingConceptId[1]', 'Int'),
					t.x.value('CostCenterId[1]', 'Int'),
					t.x.value('SettlementType[1]', 'Tinyint'),
					t.x.value('IncludeServiceOrderDetailId[1]', 'Int'),
					t.x.value('RecoveryRatio[1]', 'Decimal(5,2)'),
					t.x.value('RateManualId[1]', 'Int'),
					t.x.value('RateManualType[1]', 'Tinyint'),
					t.x.value('RateManualDetailId[1]', 'Int'),
					t.x.value('DefinitionRateDetailId[1]', 'Int'),
					t.x.value('DefinitionRateDetailConditionId[1]', 'Int'),
					t.x.value('SubTotalSalesPrice[1]', 'Decimal(20,2)'),
					t.x.value('ThirdPartyDiscount[1]', 'Decimal(20,2)'),
					t.x.value('ThirdPartyDiscountPercentage[1]', 'Decimal(5,2)'),
					t.x.value('TotalSalesPrice[1]', 'Decimal(20,2)'),
					t.x.value('GrandTotalSalesPrice[1]', 'Decimal(20,2)'),
					t.x.value('SurchargeApply[1]', 'Bit'),
					t.x.value('SurgicalInterventionType[1]', 'Tinyint'),
					t.x.value('SurgeryNumber[1]', 'Tinyint'),
					t.x.value('IsFirstEvent[1]', 'Bit'),
					t.x.value('IsAnnulled[1]', 'Bit'),
					t.x.value('IsDelete[1]', 'Bit'),
					t.x.value('IncomeMainAccountId[1]', 'Int'),
					t.x.value('CodeNameSpeciality[1]', 'Varchar(300)'),
					t.x.value('CodeNameFunctionalUnit[1]', 'Varchar(300)'),
					t.x.value('CodeNameHealthAdministrator[1]', 'Varchar(300)'),
					t.x.value('PreviusServiceOrderDetailId[1]', 'Int'),
					t.x.value('CodeNameCareGroup[1]', 'Varchar(320)'),
					t.x.value('CodeNameCostCenter[1]', 'Varchar(300)'),
					t.x.value('CodeNameCups[1]', 'Varchar(320)'),
					t.x.value('CodeNameHealthProfessional[1]', 'Varchar(300)'),
					t.x.value('CodeNameIpsService[1]', 'Varchar(300)'),
					t.x.value('CodeNameProduct[1]', 'Varchar(300)'),
					t.x.value('IsSOAT[1]', 'Bit'),
					t.x.value('RoundService[1]', 'int'),
					t.x.query('ServiceOrderDetailSurgicalXml/ServiceOrderDetailSurgical'),
					t.x.value('GrossValue[1]', 'numeric(20,2)'),
					t.x.value('TaxValue[1]', 'numeric(20,2)'),
					t.x.value('IvaId[1]', 'int')
				From @__ObjectEmbbededAux.nodes('/ListNewServiceOrderDetail') t(x)
				
				--select * from @listNewServiceOrderDetail order by ServiceOrderId asc
				Delete From @RetarificServiceOrderDetailSurgical
				Set @Rows = 1
				Set @RowId = 1

				While @Rows > 0
				begin
					Select Top 1 @RowId = RowId, @SurgicalNewXml = ServiceOrderDetailSurgicalXml From @listNewServiceOrderDetail Where RowId >= @RowId Order By RowId
					Set @Rows = @@ROWCOUNT
					If @Rows = 0 
						Break
					-- ### CAMBIO 3 (2/3) ### Misma subconsulta que en el bloque anterior
					Insert Into @RetarificServiceOrderDetailSurgical
					Select t.x.value('Id[1]', 'Int'),
						t.x.value('ServiceOrderDetailId[1]', 'Int'),
						t.x.value('CodeNameIpsService[1]', 'Varchar(320)'),
						t.x.value('IPSServiceId[1]', 'Int'),
						t.x.value('InvoicedQuantity[1]', 'Int'),
						t.x.value('LiquidationPercentage[1]', 'numeric(5,2)'),
						t.x.value('RateManualSalePrice[1]', 'numeric(20,2)'),
						t.x.value('TotalSalesPrice[1]', 'numeric(20,2)'),
						t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)'),
						t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int'),
						t.x.value('CostValue[1]', 'numeric(20,2)'),
						t.x.value('BillingConceptId[1]', 'Int'),
						t.x.value('CostCenterId[1]', 'Int'),
						t.x.value('RateManualDetailSurgicalId[1]', 'Int'),
						t.x.value('SurchargeApply[1]', 'Bit'),
						t.x.value('IncomeMainAccountId[1]', 'Int'),
						t.x.value('ClassServiceIps[1]', 'Varchar(30)'),
						t.x.value('RoundService[1]', 'Int'),
						( -- ### CAMBIO 3 (2/3) ###
							SELECT TOP 1 sosOld.Id
							FROM Billing.ServiceOrderDetailSurgical sosOld WITH (NOLOCK)
							WHERE sosOld.ServiceOrderDetailId = t.x.value('ServiceOrderDetailId[1]', 'Int')
							  AND sosOld.IPSServiceId = t.x.value('IPSServiceId[1]', 'Int')
						)
					From @SurgicalNewXml.nodes('ServiceOrderDetailSurgical') t(x)		
					Set @RowId += 1
				End

				If Exists (Select Id From @listNewServiceOrderDetail Where Presentation = 2 And SettlementType = 1) 
				Begin
					Delete From @RetarificServiceOrderDetailSurgical Where Id In (Select Id From @listServiceDetailSurgicalDeleted)
					Update ln Set ServiceOrderDetailSurgicalXml = (
						Select * 
						From @RetarificServiceOrderDetailSurgical rt 
						Where rt.ServiceOrderDetailId = ln.Id
						For Xml Path('ServiceOrderDetailSurgical'), Elements
					)
					From @listNewServiceOrderDetail ln
					Set @__ObjectEmbbededAux = (
						Select *
						From @listNewServiceOrderDetail
						For Xml Path('ListNewServiceOrderDetail'), Elements
					)
					
					Set @ObjectEmbbeded = Null
					Set @ObjectEmbbededAux = @__ObjectEmbbededAux
					Set @Message = '{ERR4}'
					Select @StatusResult As [StatusResult], @MessageResult As [MessageResult], @Message As [Message], @ObjectEmbbeded As [ObjectEmbbeded], @ObjectEmbbededAux As ObjectEmbbededAux
					Return
				End	
				Insert Into @ListServiceOrderDetailWithQx
				Select 
					RowId,
					Id,
					ServiceOrderId,
					CareGroupId,
					HealthAdministratorId,
					ThirdPartyId,
					ServiceType,
					RecordType,
					CUPSEntityId,
					IPSServiceId,
					HospitalStayId,
					HospitalStayDetailId,
					ControlExternalConsultation,
					ControlExternalConsultationCode,
					CUPSAssociateService,
					CodeAssociateService,
					IsPackage,
					Packaging,
					PackageServiceOrderDetailId,
					LiquidationType,
					Presentation,
					ProductId,
					InvoicedQuantity,
					SupplyQuantity,
					DevolutionQuantity,
					RateManualSalePrice,
					CostValue,
					ServiceDate,
					AuthorizationNumber,
					PerformsFunctionalUnitId,
					PerformsHealthProfessionalCode,
					PerformsProfessionalSpecialty,
					PerformsHealthProfessionalThirdPartyId,
					BillingConceptId,
					CostCenterId,
					SettlementType,
					IncludeServiceOrderDetailId,
					RecoveryRatio,
					RateManualId,
					RateManualType,
					RateManualDetailId,
					DefinitionRateDetailId,
					DefinitionRateDetailConditionId,
					SubTotalSalesPrice,
					ThirdPartyDiscount,
					ThirdPartyDiscountPercentage,
					TotalSalesPrice,
					GrandTotalSalesPrice,
					SurchargeApply,
					SurgicalInterventionType,
					SurgeryNumber,
					IsFirstEvent,
					IsAnnulled,
					IsDelete,
					IncomeMainAccountId,
					CodeNameSpeciality,
					CodeNameFunctionalUnit,
					CodeNameHealthAdministrator,
					PreviusServiceOrderDetailId,
					CodeNameCareGroup,
					CodeNameCostCenter,
					CodeNameCups,
					CodeNameHealthProfessional,
					CodeNameIpsService,
					CodeNameProduct,
					IsSOAT,
					RoundService,
					ServiceOrderDetailSurgicalXml
				From @listNewServiceOrderDetail
				
			End
			Else Begin
				
				
				Set @Rows = 1
				Set @RowId = 1
				
				While @Rows > 0
				Begin
					Select Top 1 @RowId = RowId, @RateManualSalePriceQx = RateManualSalePrice
						, @SubTotalSalesPriceQx = SubTotalSalesPrice, @ThirdPartyDiscountQx = ThirdPartyDiscount
						, @TotalSalesPriceQx = TotalSalesPrice, @GrandTotalSalesPriceQx = GrandTotalSalesPrice
						, @CodeAssociateServiceQx = CodeAssociateService, @CareGroupIdQx = CareGroupId
						, @IPSServiceIdQx = IPSServiceId, @ServiceOrderDetailSurgicalXmlQx = ServiceOrderDetailSurgicalXml
						, @PreviusServiceOrderDetailId = PreviusServiceOrderDetailId
						, @ServiceOrderDetailIdQx = Id
					From @ListServiceOrderDetailWithQx Where PreviusServiceOrderDetailId > 0 And RowId >= @RowId Order By RowId

					Set @Rows = @@ROWCOUNT
					If @Rows = 0 
						Break

					If Exists (Select Id From @distributionToRetarific Where ServiceOrderDetailId = @PreviusServiceOrderDetailId)
					Begin
						Update dr Set RateManualSalePrice = @RateManualSalePriceQx
							, SubTotalSalesPrice_1 = @SubTotalSalesPriceQx
							, ThirdPartyDiscount_1 = @ThirdPartyDiscountQx
							, TotalSalesPrice = @TotalSalesPriceQx
							, GrandTotalSalesPrice_1 = @GrandTotalSalesPriceQx
							, GrandTotalSalesPrice = @GrandTotalSalesPriceQx
							, ThirdPartySalesPrice = @GrandTotalSalesPriceQx
							, GrandTotalDiscount = @ThirdPartyDiscountQx * Quantity
							, ThirdPartyPercentage = 100
							, SubTotalPatientSalesPrice = 0
							, PatientPercentage = 0
							, CodeAssociateService = @CodeAssociateServiceQx
							, ApplyRecoveryFee = 1
							, RecoveryFeeType = 1
							, CareGroupId = @CareGroupIdQx
							, LastCaregroupId = @CareGroupId
							, IPSServiceId = @IPSServiceIdQx
						From @distributionToRetarific dr
						Where dr.ServiceOrderDetailId = @PreviusServiceOrderDetailId

						
						--Agregar los detalles Quirurgicos
						If (Select Top 1 Presentation From @distributionToRetarific Where ServiceOrderDetailId = @ServiceOrderDetailIdQx) = 2 Begin	
							
							-- ### CAMBIO 3 (3/3) ### Misma subconsulta, tercera y última ocurrencia
							Insert Into @RetarificServiceOrderDetailSurgical
							Select t.x.value('Id[1]', 'Int'),
								t.x.value('ServiceOrderDetailId[1]', 'Int'),
								t.x.value('CodeNameIpsService[1]', 'Varchar(320)'),
								t.x.value('IPSServiceId[1]', 'Int'),
								t.x.value('InvoicedQuantity[1]', 'Int'),
								t.x.value('LiquidationPercentage[1]', 'numeric(5,2)'),
								t.x.value('RateManualSalePrice[1]', 'numeric(20,2)'),
								t.x.value('TotalSalesPrice[1]', 'numeric(20,2)'),
								t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)'),
								t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int'),
								t.x.value('CostValue[1]', 'numeric(20,2)'),
								t.x.value('BillingConceptId[1]', 'Int'),
								t.x.value('CostCenterId[1]', 'Int'),
								t.x.value('RateManualDetailSurgicalId[1]', 'Int'),
								t.x.value('SurchargeApply[1]', 'Bit'),
								t.x.value('IncomeMainAccountId[1]', 'Int'),
								t.x.value('ClassServiceIps[1]', 'Varchar(30)'),
								t.x.value('RoundService[1]', 'Int'),
								( -- ### CAMBIO 3 (3/3) ###
									SELECT TOP 1 sosOld.Id
									FROM Billing.ServiceOrderDetailSurgical sosOld WITH (NOLOCK)
									WHERE sosOld.ServiceOrderDetailId = t.x.value('ServiceOrderDetailId[1]', 'Int')
									  AND sosOld.IPSServiceId = t.x.value('IPSServiceId[1]', 'Int')
								)
							From @ServiceOrderDetailSurgicalXmlQx.nodes('ServiceOrderDetailSurgical') t(x)

							Update @ListServiceOrderDetailWithQx Set ServiceOrderDetailSurgicalXml = Null
							Where RowId = @RowId
						End

					End
		
					Set @RowId += 1
				End				
			End
						
			Update dr Set ServiceOrderDetailSurgicalXml = (
				Select * From @RetarificServiceOrderDetailSurgical rs 
				Where rs.ServiceOrderDetailId = dr.ServiceOrderDetailId
				For Xml Path('RetarificServiceOrderDetailSurgical'), Elements
			)
			From @distributionToRetarific dr
			
			--Aca va el listado de detalles de ordenes de servicio a crear por homologos (res.ObjectEmbbeded con id = 0)
			Set @Rows = 1
			Set @RowId = 1
			Declare @InvoicedQuantityQx Int
			
			While @Rows > 0
			Begin
				Select Top 1 @RowId = RowId, @RateManualSalePriceQx = RateManualSalePrice
					, @SubTotalSalesPriceQx = SubTotalSalesPrice, @ThirdPartyDiscountQx = ThirdPartyDiscount
					, @TotalSalesPriceQx = TotalSalesPrice, @GrandTotalSalesPriceQx = GrandTotalSalesPrice
					, @CodeAssociateServiceQx = CodeAssociateService, @CareGroupIdQx = CareGroupId
					, @IPSServiceIdQx = IPSServiceId, @ServiceOrderDetailSurgicalXmlQx = ServiceOrderDetailSurgicalXml
					, @PreviusServiceOrderDetailId = PreviusServiceOrderDetailId
					, @ServiceOrderDetailIdQx = Id
					, @InvoicedQuantityQx = InvoicedQuantity
				From @ListServiceOrderDetailWithQx Where (PreviusServiceOrderDetailId Is Null Or PreviusServiceOrderDetailId = 0) And RowId >= @RowId Order By RowId

				Set @Rows = @@ROWCOUNT

				If @Rows = 0 
					Break
				
				Update @ListServiceOrderDetailWithQx Set CUPSAssociateService = 1 Where RowId = @RowId

				If @ServiceOrderDetailIdQx = 0 Begin
					INSERT INTO [Billing].[ServiceOrderDetail]
					   ([ServiceOrderId]
					   ,[CareGroupId]
					   ,[HealthAdministratorId]
					   ,[ThirdPartyId]
					   ,[ServiceType]
					   ,[RecordType]
					   ,[CUPSEntityId]
					   ,[IPSServiceId]
					   ,[HospitalStayId]
					   ,[HospitalStayDetailId]
					   ,[ControlExternalConsultation]
					   ,[ControlExternalConsultationCode]
					   ,[CUPSAssociateService]
					   ,[CodeAssociateService]
					   ,[IsPackage]
					   ,[Packaging]
					   ,[PackageServiceOrderDetailId]
					   ,[LiquidationType]
					   ,[Presentation]
					   ,[ProductId]
					   ,[InvoicedQuantity]
					   ,[SupplyQuantity]
					   ,[DevolutionQuantity]
					   ,[RateManualSalePrice]
					   ,[CostValue]
					   ,[ServiceDate]
					   ,[AuthorizationNumber]
					   ,[PerformsFunctionalUnitId]
					   ,[PerformsHealthProfessionalCode]
					   ,[PerformsProfessionalSpecialty]
					   ,[PerformsHealthProfessionalThirdPartyId]
					   ,[BillingConceptId]
					   ,[CostCenterId]
					   ,[SettlementType]
					   ,[IncludeServiceOrderDetailId]
					   ,[RecoveryRatio]
					   ,[RateManualId]
					   ,[RateManualType]
					   ,[RateManualDetailId]
					   ,[DefinitionRateDetailId]
					   ,[DefinitionRateDetailConditionId]
					   ,[SubTotalSalesPrice]
					   ,[ThirdPartyDiscount]
					   ,[ThirdPartyDiscountPercentage]
					   ,[TotalSalesPrice]
					   ,[GrandTotalSalesPrice]
					   ,[SurchargeApply]
					   ,[SurgicalInterventionType]
					   ,[SurgeryNumber]
					   ,[IsFirstEvent]
					   ,[IsAnnulled]
					   ,[IsDelete]
					   ,[IncomeMainAccountId]
					   ,RoundService)
				 Select
					   ServiceOrderId
					   ,CareGroupId
					   ,HealthAdministratorId
					   ,ThirdPartyId
					   ,ServiceType
					   ,RecordType
					   ,CUPSEntityId
					   ,IPSServiceId
					   ,HospitalStayId
					   ,HospitalStayDetailId
					   ,ControlExternalConsultation
					   ,ControlExternalConsultationCode
					   ,CUPSAssociateService
					   ,CodeAssociateService
					   ,IsPackage
					   ,Packaging
					   ,PackageServiceOrderDetailId
					   ,LiquidationType
					   ,Presentation
					   ,ProductId
					   ,InvoicedQuantity
					   ,SupplyQuantity
					   ,DevolutionQuantity
					   ,RateManualSalePrice
					   ,CostValue
					   ,ServiceDate
					   ,AuthorizationNumber
					   ,PerformsFunctionalUnitId
					   ,PerformsHealthProfessionalCode
					   ,PerformsProfessionalSpecialty
					   ,PerformsHealthProfessionalThirdPartyId
					   ,BillingConceptId
					   ,CostCenterId
					   ,SettlementType
					   ,IncludeServiceOrderDetailId
					   ,RecoveryRatio
					   ,RateManualId
					   ,RateManualType
					   ,RateManualDetailId
					   ,DefinitionRateDetailId
					   ,DefinitionRateDetailConditionId
					   ,SubTotalSalesPrice
					   ,ThirdPartyDiscount
					   ,ThirdPartyDiscountPercentage
					   ,TotalSalesPrice
					   ,GrandTotalSalesPrice
					   ,SurchargeApply
					   ,SurgicalInterventionType
					   ,SurgeryNumber
					   ,IsFirstEvent
					   ,IsAnnulled
					   ,IsDelete
					   ,IncomeMainAccountId
					   ,RoundService
					From @ListServiceOrderDetailWithQx
					Where RowId = @RowId

					Set @ServiceOrderDetailIdQx = Scope_Identity()
					Update @ListServiceOrderDetailWithQx Set Id = @ServiceOrderDetailIdQx
					Where RowId = @RowId
				End

				Insert Into Billing.ServiceOrderDetailDistribution (
					RevenueControlDetailId,
					ServiceOrderDetailId,
					Quantity,
					GrandTotalSalesPrice,
					DistributionType,
					ThirdPartySalesPrice,
					ThirdPartyPercentage,
					ApplyRecoveryFee,
					RecoveryFeeType,
					SubTotalPatientSalesPrice,
					PatientPercentage,
					LastCaregroupId)
				Values (@RevenueControlDetailId, @ServiceOrderDetailIdQx, @InvoicedQuantityQx, @GrandTotalSalesPriceQx, 1, @GrandTotalSalesPriceQx, 100, 1, 1, 0, 0, @CareGroupId)

				Set @RowId += 1
			End
		End
		--Elimino los listados a eliminar
		-- ### CAMBIO 4 ### Se retira de aquí el DELETE de Billing.ServiceOrderDetailSurgical.
		-- Se ejecutará más abajo (ver CAMBIO 8), después de insertar los nuevos registros
		-- y reasignar MedicalFees.MedicalFeesCausation.
		Delete From @RetarificServiceOrderDetailSurgical Where Id In (Select Id From @listServiceDetailSurgicalDeleted)
		Delete From @distributionToRetarific Where Id In (Select Id From @listDistributionDeleted)		
		Delete From Billing.ServiceOrderDetailDistribution Where Id In (Select Id From @listDistributionDeleted)
		Delete From Billing.ServiceOrderDetail Where Id In (Select Id From @listServiceDetailDeleted)

		--Aca Voy Empezar a Actualizar contra las tablas verdaderas		

		Update sod 
			Set sod.ServiceOrderId = dr.ServiceOrderId
				, sod.CareGroupId = dr.CareGroupId
				, sod.HealthAdministratorId = dr.HealthAdministratorId
				, sod.ThirdPartyId = dr.ThirdPartyId
				, sod.ServiceType = dr.ServiceType
				, sod.RecordType = dr.RecordType
				, sod.CUPSEntityId = dr.CUPSEntityId
				, sod.IPSServiceId = dr.IPSServiceId
				, sod.HospitalStayId = dr.HospitalStayId
				, sod.HospitalStayDetailId = dr.HospitalStayDetailId
				, sod.ControlExternalConsultation = dr.ControlExternalConsultation
				, sod.ControlExternalConsultationCode = dr.ControlExternalConsultationCode
				, sod.CUPSAssociateService = dr.CUPSAssociateService
				, sod.CodeAssociateService = dr.CodeAssociateService
				, sod.IsPackage = dr.IsPackage
				, sod.Packaging = dr.Packaging
				, sod.PackageServiceOrderDetailId = dr.PackageServiceOrderDetailId
				, sod.LiquidationType = dr.LiquidationType
				, sod.Presentation = dr.Presentation
				, sod.ProductId = dr.ProductId
				, sod.InvoicedQuantity = dr.InvoicedQuantity
				, sod.SupplyQuantity = dr.SupplyQuantity
				, sod.DevolutionQuantity = dr.DevolutionQuantity
				, sod.RateManualSalePrice = dr.RateManualSalePrice
				, sod.CostValue = dr.CostValue
				, sod.ServiceDate = dr.ServiceDate
				, sod.AuthorizationNumber = dr.AuthorizationNumber
				, sod.PerformsFunctionalUnitId = dr.PerformsFunctionalUnitId
				, sod.PerformsHealthProfessionalCode = dr.PerformsHealthProfessionalCode
				, sod.PerformsProfessionalSpecialty = dr.PerformsProfessionalSpecialty
				, sod.PerformsHealthProfessionalThirdPartyId = dr.PerformsHealthProfessionalThirdPartyId
				, sod.BillingConceptId = dr.BillingConceptId
				, sod.CostCenterId = dr.CostCenterId
				, sod.SettlementType = dr.SettlementType
				, sod.IncludeServiceOrderDetailId = dr.IncludeServiceOrderDetailId
				, sod.RecoveryRatio = dr.RecoveryRatio
				, sod.RateManualId = dr.RateManualId
				, sod.RateManualType = dr.RateManualType
				, sod.RateManualDetailId = dr.RateManualDetailId
				, sod.DefinitionRateDetailId = dr.DefinitionRateDetailId
				, sod.DefinitionRateDetailConditionId = dr.DefinitionRateDetailConditionId
				, sod.SubTotalSalesPrice = dr.SubTotalSalesPrice_1
				, sod.ThirdPartyDiscount = dr.ThirdPartyDiscount_1
				, sod.ThirdPartyDiscountPercentage = dr.ThirdPartyDiscountPercentage
				, sod.TotalSalesPrice = dr.TotalSalesPrice
				, sod.GrandTotalSalesPrice = dr.GrandTotalSalesPrice_1
				, sod.SurchargeApply = dr.SurchargeApply
				, sod.SurgicalInterventionType = dr.SurgicalInterventionType
				, sod.SurgeryNumber = dr.SurgeryNumber
				, sod.IsFirstEvent = dr.IsFirstEvent
				, sod.IsAnnulled = dr.IsAnnulled
				, sod.IsDelete = dr.IsDelete
				, sod.IncomeMainAccountId = dr.IncomeMainAccountId
				, sod.RoundService = dr.RoundService
				, sod.ProductLiquidationType =1
				, sod.GrossValue = dr.GrossValue
				, sod.TaxValue = dr.TaxValue
				,sod.IvaId = dr.IvaId
		From Billing.ServiceOrderDetail sod
		Inner Join @distributionToRetarific dr On sod.Id = dr.ServiceOrderDetailId

		Update sod
			Set sod.GrossValue = sod.GrossValue + sod.TaxValue
			  , sod.TaxValue   = 0
			  , sod.IvaId      = Null
		From Billing.ServiceOrderDetail sod
		Inner Join @distributionToRetarific dr On sod.Id = dr.ServiceOrderDetailId
		Inner Join Inventory.InventoryProduct ip With(Nolock) On ip.Id = dr.ProductId
		Where dr.ProductId Is Not Null
		  And Not (ip.TaxedProduct = 1 And ip.LiquidateSalesTaxes = 1)
		  And (sod.TaxValue > 0 Or sod.IvaId Is Not Null)


			Update sod 
			SET	[ServiceOrderId] = dr.[ServiceOrderId]
				,[CareGroupId] = dr.[CareGroupId]
				,[HealthAdministratorId] = dr.[HealthAdministratorId]
				,[ThirdPartyId] = dr.[ThirdPartyId]
				,[ServiceType] = dr.[ServiceType]
				,[RecordType] = dr.[RecordType]
				,[CUPSEntityId] = dr.[CUPSEntityId]
				,[IPSServiceId] = dr.[IPSServiceId]
				,[HospitalStayId] = dr.[HospitalStayId]
				,[HospitalStayDetailId] = dr.[HospitalStayDetailId]
				,[ControlExternalConsultation] = dr.[ControlExternalConsultation]
				,[ControlExternalConsultationCode] = dr.[ControlExternalConsultationCode]
				,[CUPSAssociateService] = dr.[CUPSAssociateService]
				,[CodeAssociateService] = dr.[CodeAssociateService]
				,[IsPackage] = dr.[IsPackage]
				,[Packaging] = dr.[Packaging]
				,[PackageServiceOrderDetailId] = dr.[PackageServiceOrderDetailId]
				,[LiquidationType] = dr.[LiquidationType]
				,[Presentation] = dr.[Presentation]
				,[ProductId] = dr.[ProductId]
				,[InvoicedQuantity] = dr.[InvoicedQuantity]
				,[SupplyQuantity] = dr.[SupplyQuantity]
				,[DevolutionQuantity] = dr.[DevolutionQuantity]
				,[RateManualSalePrice] = dr.[RateManualSalePrice]
				,[CostValue] = dr.[CostValue]
				,[ServiceDate] = dr.[ServiceDate]
				,[AuthorizationNumber] = dr.[AuthorizationNumber]
				,[PerformsFunctionalUnitId] = dr.[PerformsFunctionalUnitId]
				,[PerformsHealthProfessionalCode] = dr.[PerformsHealthProfessionalCode]
				,[PerformsProfessionalSpecialty] = dr.[PerformsProfessionalSpecialty]
				,[PerformsHealthProfessionalThirdPartyId] = dr.[PerformsHealthProfessionalThirdPartyId]
				,[BillingConceptId] = dr.[BillingConceptId]
				,[CostCenterId] = dr.[CostCenterId]
				,[SettlementType] = dr.[SettlementType]
				,[IncludeServiceOrderDetailId] = dr.[IncludeServiceOrderDetailId]
				,[RecoveryRatio] = dr.[RecoveryRatio]
				,[RateManualId] = dr.[RateManualId]
				,[RateManualType] = dr.[RateManualType]
				,[RateManualDetailId] = dr.[RateManualDetailId]
				,[DefinitionRateDetailId] = dr.[DefinitionRateDetailId]
				,[DefinitionRateDetailConditionId] = dr.[DefinitionRateDetailConditionId]
				,[SubTotalSalesPrice] = dr.[SubTotalSalesPrice]
				,[ThirdPartyDiscount] = dr.[ThirdPartyDiscount]
				,[ThirdPartyDiscountPercentage] = dr.[ThirdPartyDiscountPercentage]
				,[TotalSalesPrice] = dr.[TotalSalesPrice]
				,[GrandTotalSalesPrice] = dr.[GrandTotalSalesPrice]
				,[SurchargeApply] = dr.[SurchargeApply]
				,[SurgicalInterventionType] = dr.[SurgicalInterventionType]
				,[SurgeryNumber] = dr.[SurgeryNumber]
				,[IsFirstEvent] = dr.[IsFirstEvent]
				,[IsAnnulled] = dr.[IsAnnulled]
				,[IsDelete] = dr.[IsDelete]
				,[IncomeMainAccountId] = dr.[IncomeMainAccountId]
				,[RoundService] = ISNULL(dr.[RoundService], sod.RoundService)
				, [ProductLiquidationType] = 1
				,[GrossValue] =ssp.GrossValue
				,[TaxValue] = ssp.TaxValue
		From Billing.ServiceOrderDetail sod
		Inner Join @ListServiceOrderDetailWithQx dr On sod.Id = dr.Id
		left join [GeneralLedger].[GeneralLedgerIVA] iva WITH(NOLOCK) on sod.IvaId = iva.Id
		CROSS APPLY Billing.SetValueSalesPrice  ( iif(dr.SubTotalSalesPrice is null,@SalesPriceInclude,1),COALESCE(dr.SubTotalSalesPrice,dr.RateManualSalePrice),isnull(iva.[Percentage],0) ) ssp

		Update sodd 
			Set sodd.RevenueControlDetailId = dr.RevenueControlDetailId
				, sodd.ServiceOrderDetailId = dr.ServiceOrderDetailId
				, sodd.Quantity = dr.Quantity
				, sodd.GrandTotalSalesPrice = dr.GrandTotalSalesPrice
				, sodd.GrandTotalDiscount = dr.GrandTotalDiscount
				, sodd.DistributionType = dr.DistributionType
				, sodd.ThirdPartySalesPrice = dr.ThirdPartySalesPrice
				, sodd.ThirdPartyPercentage = dr.ThirdPartyPercentage
				, sodd.ApplyRecoveryFee = dr.ApplyRecoveryFee
				, sodd.RecoveryFeeType = dr.RecoveryFeeType
				, sodd.SubTotalPatientSalesPrice = dr.SubTotalPatientSalesPrice
				, sodd.PatientPercentage = dr.PatientPercentage
				, sodd.LastCaregroupId = dr.LastCaregroupId
				, sodd.SubTotalSalesPrice =0
		From Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK)
		Inner Join @distributionToRetarific dr On sodd.Id = dr.Id

		INSERT INTO [Billing].[ServiceOrderDetail]
           ([ServiceOrderId]
           ,[CareGroupId]
           ,[HealthAdministratorId]
           ,[ThirdPartyId]
           ,[ServiceType]
           ,[RecordType]
           ,[CUPSEntityId]
           ,[IPSServiceId]
           ,[HospitalStayId]
           ,[HospitalStayDetailId]
           ,[ControlExternalConsultation]
           ,[ControlExternalConsultationCode]
           ,[CUPSAssociateService]
           ,[CodeAssociateService]
           ,[IsPackage]
           ,[Packaging]
           ,[PackageServiceOrderDetailId]
           ,[LiquidationType]
           ,[Presentation]
           ,[ProductId]
           ,[InvoicedQuantity]
           ,[SupplyQuantity]
           ,[DevolutionQuantity]
           ,[RateManualSalePrice]
           ,[CostValue]
           ,[ServiceDate]
           ,[AuthorizationNumber]
           ,[PerformsFunctionalUnitId]
           ,[PerformsHealthProfessionalCode]
           ,[PerformsProfessionalSpecialty]
           ,[PerformsHealthProfessionalThirdPartyId]
           ,[BillingConceptId]
           ,[CostCenterId]
           ,[SettlementType]
           ,[IncludeServiceOrderDetailId]
           ,[RecoveryRatio]
           ,[RateManualId]
           ,[RateManualType]
           ,[RateManualDetailId]
           ,[DefinitionRateDetailId]
           ,[DefinitionRateDetailConditionId]
           ,[SubTotalSalesPrice]
           ,[ThirdPartyDiscount]
           ,[ThirdPartyDiscountPercentage]
           ,[TotalSalesPrice]
           ,[GrandTotalSalesPrice]
           ,[SurchargeApply]
           ,[SurgicalInterventionType]
           ,[SurgeryNumber]
           ,[IsFirstEvent]
           ,[IsAnnulled]
           ,[IsDelete]
           ,[IncomeMainAccountId]
		   ,[RoundService])
     Select
           ServiceOrderId
           ,CareGroupId
           ,HealthAdministratorId
           ,ThirdPartyId
           ,ServiceType
           ,RecordType
           ,CUPSEntityId
           ,IPSServiceId
           ,HospitalStayId
           ,HospitalStayDetailId
           ,ControlExternalConsultation
           ,ControlExternalConsultationCode
           ,CUPSAssociateService
           ,CodeAssociateService
           ,IsPackage
           ,Packaging
           ,PackageServiceOrderDetailId
           ,LiquidationType
           ,Presentation
           ,ProductId
           ,InvoicedQuantity
           ,SupplyQuantity
           ,DevolutionQuantity
           ,RateManualSalePrice
           ,CostValue
           ,ServiceDate
           ,AuthorizationNumber
           ,PerformsFunctionalUnitId
           ,PerformsHealthProfessionalCode
           ,PerformsProfessionalSpecialty
           ,PerformsHealthProfessionalThirdPartyId
           ,BillingConceptId
           ,CostCenterId
           ,SettlementType
           ,IncludeServiceOrderDetailId
           ,RecoveryRatio
           ,RateManualId
           ,RateManualType
           ,RateManualDetailId
           ,DefinitionRateDetailId
           ,DefinitionRateDetailConditionId
           ,SubTotalSalesPrice
           ,ThirdPartyDiscount
           ,ThirdPartyDiscountPercentage
           ,TotalSalesPrice
           ,GrandTotalSalesPrice
           ,SurchargeApply
           ,SurgicalInterventionType
           ,SurgeryNumber
           ,IsFirstEvent
           ,IsAnnulled
           ,IsDelete
           ,IncomeMainAccountId
		   ,RoundService
		From @ListServiceOrderDetailWithQx--@listNewServiceOrderDetail
		Where Id = 0

		-- ### CAMBIO 5 ### Tabla para capturar el mapeo Id-viejo -> Id-nuevo generado por el INSERT siguiente
		Declare @MapeoSurgicalIds Table (OldSurgicalId Int, NewSurgicalId Int)

		-- ### CAMBIO 6 (v2 - corregido) ### 
		-- NOTA: un INSERT...SELECT no permite referenciar columnas de la tabla ORIGEN en OUTPUT
		-- (solo admite inserted.*/deleted.*, que son de la tabla DESTINO). Por eso se usa MERGE:
		-- en MERGE, OUTPUT sí puede referenciar columnas de la tabla origen (alias "src").
		MERGE INTO [Billing].[ServiceOrderDetailSurgical] AS tgt
		USING @RetarificServiceOrderDetailSurgical AS src
		ON 1 = 0 -- nunca hace match a propósito: todas las filas de src son candidatas a "no coincidir" e insertarse
		WHEN NOT MATCHED AND src.Id = 0 THEN
			INSERT
			   ([ServiceOrderDetailId]
			   ,[IPSServiceId]
			   ,[InvoicedQuantity]
			   ,[LiquidationPercentage]
			   ,[RateManualSalePrice]
			   ,[TotalSalesPrice]
			   ,[PerformsHealthProfessionalCode]
			   ,[PerformsHealthProfessionalThirdPartyId]
			   ,[CostValue]
			   ,[BillingConceptId]
			   ,[CostCenterId]
			   ,[RateManualDetailSurgicalId]
			   ,[SurchargeApply]
			   ,[OnlyMedicalFees]
			   ,[IncomeMainAccountId]
			   ,[RoundService])
			VALUES
			   (src.ServiceOrderDetailId
			   ,src.IPSServiceId
			   ,src.InvoicedQuantity
			   ,src.LiquidationPercentage
			   ,src.RateManualSalePrice
			   ,src.TotalSalesPrice
			   ,src.PerformsHealthProfessionalCode
			   ,src.PerformsHealthProfessionalThirdPartyId
			   ,src.CostValue
			   ,src.BillingConceptId
			   ,src.CostCenterId
			   ,src.RateManualDetailSurgicalId
			   ,src.SurchargeApply
			   ,0
			   ,src.IncomeMainAccountId
			   ,ISNULL(src.RoundService, 1))
		OUTPUT inserted.Id, src.OldSurgicalId INTO @MapeoSurgicalIds(NewSurgicalId, OldSurgicalId); -- ### CAMBIO 6 ### (requiere ; al final del MERGE)

		-- ### CAMBIO 7 ### Reasignar la causación médica a los nuevos registros ANTES de borrar los viejos
		UPDATE mfc
			SET mfc.ServiceOrderDetailSurgicalId = m.NewSurgicalId
		FROM MedicalFees.MedicalFeesCausation mfc
		INNER JOIN @MapeoSurgicalIds m ON mfc.ServiceOrderDetailSurgicalId = m.OldSurgicalId
		WHERE m.OldSurgicalId IS NOT NULL

		-- ### CAMBIO 8 ### Ahora sí, se eliminan los ServiceOrderDetailSurgical viejos.
		-- En este punto ya no pueden quedar huérfanos en MedicalFeesCausation porque se reasignaron en el CAMBIO 7.
		Delete From Billing.ServiceOrderDetailSurgical Where Id In (Select Id From @listServiceDetailSurgicalDeleted)

		Declare @CareGroupType Tinyint, @HealthAdministratorContractId Int
			, @ThirdPartyHadministrator Int, @ContractEntityId Int
			, @EntityType Tinyint, @LiquidationTypecg Tinyint

		Select @CareGroupType = cg.CareGroupType, @HealthAdministratorContractId = ct.HealthAdministratorId
			, @ThirdPartyHadministrator = ha.ThirdPartyId, @ContractEntityId = ct.ContractEntityId
			, @EntityType = cg.EntityType
			, @LiquidationTypecg = cg.LiquidationType
		From [Contract].CareGroup cg With(Nolock)
		Left Join [Contract].[Contract] ct With(Nolock) On cg.ContractId = ct.Id
		Left Join [Contract].HealthAdministrator ha With(Nolock) On ct.HealthAdministratorId = ha.Id
		Where cg.Id = @CareGroupId

		If @CareGroupType = 1 Begin --EAPB con contrato
			Update Billing.RevenueControlDetail Set HealthAdministratorId = @HealthAdministratorContractId
				, ThirdPartyId = @ThirdPartyHadministrator
				, ContractEntityId = @ContractEntityId
			Where Id = @RevenueControlDetailId
		End
		Else If @CareGroupType = 2 Or @CareGroupType = 4 Begin	--EAPB sin contrato		
			If @FolioCareGroupIdSource <> @CareGroupId Begin
				--Select Top 1 @HealthAdministratorContractId = Id
				--	, @ThirdPartyHadministrator = ThirdPartyId
				--From [Contract].HealthAdministrator With(Nolock)
				--Where EntityType = @EntityType				
				--If @HealthAdministratorContractId Is Not Null And @HealthAdministratorContractId > 0 Begin
				--	Update Billing.RevenueControlDetail Set HealthAdministratorId = @HealthAdministratorContractId
				--		, ThirdPartyId = @ThirdPartyHadministrator
				--		, ContractEntityId = Null
				--	Where Id = @RevenueControlDetailId
				--End
				if @CareGroupType = 4 
				BEGIN
					Update Billing.RevenueControlDetail Set HealthAdministratorId = NULL
						, ContractEntityId = Null
					Where Id = @RevenueControlDetailId
				End
				else if @CareGroupType = 2
				BEGIN
					Select Top 1 @HealthAdministratorContractId = Id
						, @ThirdPartyHadministrator = ThirdPartyId
					From [Contract].HealthAdministrator With(Nolock)
					Where EntityType = @EntityType	
					If @HealthAdministratorContractId Is Not Null And @HealthAdministratorContractId > 0 Begin
						Update Billing.RevenueControlDetail Set HealthAdministratorId = @HealthAdministratorContractId
							, ThirdPartyId = @ThirdPartyHadministrator
							, ContractEntityId = Null
						Where Id = @RevenueControlDetailId
					End
				END	
				Else Begin
					Declare @EntityTypeName Varchar(30)
					If @EntityType = 1
						Set @EntityTypeName = 'EPS Contributivo'
					Else If @EntityType = 2
						Set @EntityTypeName = 'EPS Subsidiado'
					Else If @EntityType = 3
						Set @EntityTypeName = 'ET Vinculados Municipios'
					Else If @EntityType = 4
						Set @EntityTypeName = 'ET Vinculados Departamentos'
					Else If @EntityType = 5
						Set @EntityTypeName = 'ARL Riesgos Laborales'
					Else If @EntityType = 6
						Set @EntityTypeName = 'MP Medicina Prepagada'
					Else If @EntityType = 7
						Set @EntityTypeName = 'IPS Privada'
					Else If @EntityType = 8
						Set @EntityTypeName = 'IPS Publica'
					Else If @EntityType = 9
						Set @EntityTypeName = 'Regimen Especial'
					Else If @EntityType = 10
						Set @EntityTypeName = 'Accidentes de transito'
					Else If @EntityType = 11
						Set @EntityTypeName = 'Fosyga'
					Else If @EntityType = 12
						Set @EntityTypeName = 'Otros'

					Set @Message = 'No se encontró una entidad administradora de tipo ' + @EntityTypeName
					Select Convert(Bit, 1) As [StatusResult], @MessageResult As [MessageResult], @Message As [Message], Null As [ObjectEmbbeded], Null As ObjectEmbbededAux		
					Return
				End		
			End
		End
		Else If @CareGroupType = 3 Begin	--Particulares
			Update Billing.RevenueControlDetail Set HealthAdministratorId = Null
				, ThirdPartyId = @ThirdPartyPatientId
				, ContractEntityId = Null
			Where Id = @RevenueControlDetailId
		End

		Update Billing.RevenueControlDetail Set CareGroupId = @CareGroupId
			, FolioType = @CareGroupType
			, LiquidationType = @LiquidationTypecg
		Where Id = @RevenueControlDetailId

		--Declare @UpdateFolio Table(StatusResult Bit, MessageResult varchar(100))
		Declare @StatusResultRecalcule Bit, @MessageResultRecalcule Varchar(100)
		
		--Insert Into @UpdateFolio
		Exec Billing.SP_UpdateRevenueControlDetailValues_Output @RevenueControlDetailId, NULL, @StatusResultRecalcule Output, @MessageResultRecalcule Output

		Select Convert(Bit, 1) As [StatusResult], @MessageResult As [MessageResult], @Message As [Message], Null As [ObjectEmbbeded], Null As ObjectEmbbededAux
	
  END TRY	
  BEGIN CATCH			
			Select Convert(Bit, 0) As [StatusResult], ERROR_MESSAGE() As [MessageResult], Null As [Message], Null As [ObjectEmbbeded], Null As ObjectEmbbededAux		
  END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que re-tarififica (cambia la tarifa o precio) de un listado de servicios ya registrados en el control de ingresos de facturación. Recibe el detalle del control de ingresos, el grupo de atención, datos del paciente (sexo y fecha de nacimiento), homologaciones de servicios y parámetros del tercero pagador y la administradora de salud (EPS/aseguradora). Internamente construye una tabla temporal con toda la distribución financiera de cada servicio a re-tarifificar, calculando precios de venta totales, descuentos, cuota de recuperación del paciente, porcentajes del tercero, valores brutos e IVA, y luego actualiza los detalles de la orden de servicio con los nuevos valores tarifarios. Se usa principalmente en el proceso de ajuste de tarifas en cuentas de cobro, corrección de liquidaciones y cambio de contrato o grupo de atención en el módulo de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ChangeRateServices';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ChangeRateServices';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan folios cuyo RevenueControlDetail.Status = 1 (no facturado/bloqueado/anulado/etc.); Solo se incluyen distribuciones con DistributionType <> 5 y ServiceOrderDetail.IsDelete = 0; Cuando @OnlyRateChange=0, solo se retarifican distribuciones cuyo LastCaregroupId difiera del CareGroup destino; Los servicios asociados (CUPSAssociateService=1) no facturados se eliminan; solo se conservan los principales (CUPSAssociateService=0) o los marcados como primer evento; Los productos (RecordType=2) deben estar cubiertos por ProductRateGeneral o ProductRateDetail vigente a la ServiceDate; si no, se aborta sin escribir cambios; Para CareGroupType=3 (particulares) el folio queda sin HealthAdministrator ni ContractEntity y con ThirdPartyId=@ThirdPartyPatientId; Tras la retarificación, RevenueControlDetail siempre queda con CareGroupId, FolioType y LiquidationType del CareGroup destino; Las nuevas distribuciones por homologación se crean con ThirdPartyPercentage=100 y PatientPercentage=0 (cubierto 100% por el tercero); Cualquier excepción no controlada se captura y se devuelve StatusResult=0 con el ERROR_MESSAGE, sin propagar', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeRateServices';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'folio de facturación; retarificación; grupo de atención (CareGroup); homologación de servicios; EAPB con/sin contrato; particulares; administradora de salud (EPS/ARL/MP/etc.); CUPS; servicios quirúrgicos; tarifa de productos (medicamentos/insumos); manual tarifario; distribución tercero/paciente; cuota de recuperación; IVA / precio con o sin impuesto; RIAS', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeRateServices';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RevenueControlDetail.Status NOT IN (1) (no está en estado retarificable) → Devuelve StatusResult=0 con mensaje del estado actual (Facturado/Bloqueado/Anulado/Reconocimiento Ingresos/Factura Asociada) y termina else Continúa el proceso de retarificación; si Existen productos (RecordType=2) cuyo ProductId no está cubierto por ProductRateGeneral ni por ProductRateDetail vigente para la fecha de servicio y tarifa del CareGroup destino → Devuelve mensaje listando códigos y nombres de productos no cubiertos y termina; si @ListHomologationsXml es null y @ListServiceOrderDetailWithQxXml es null (primera llamada) → Invoca Contract.ValidateHomologation; si hay errores retorna {ERR1}; si hay homologaciones múltiples retorna {ERR2}; si todo ok ejecuta SP_RunChangeRatesServices_Output; si SP_RunChangeRatesServices_Output devuelve StateResult=0 o MessageResult no vacío → Retorna {ERR3} (errores de retarificación) y termina; si Existe en @listNewServiceOrderDetail al menos un detalle con Presentation=2 y SettlementType=1 (quirúrgico con liquidación por servicios) → Retorna {ERR4} con el listado de detalles para que el usuario elija los servicios a cobrar; si @FolioCareGroupIdSource <> @CareGroupId (cambia el grupo de atención del folio) → Marca para borrado los ServiceOrderDetailSurgical asociados (excepto DistributionType=2); si CareGroup.CareGroupType = 1 (EAPB con contrato) → Actualiza RevenueControlDetail con HealthAdministratorId, ThirdPartyId y ContractEntityId tomados del Contract; si CareGroup.CareGroupType = 2 (EAPB sin contrato) y cambia el CareGroup del folio → Busca HealthAdministrator por EntityType; si existe actualiza el folio con esa administradora y ContractEntityId=NULL; si no existe retorna mensaje ''No se encontró una entidad administradora de tipo …'' y termina; si CareGroup.CareGroupType = 4 y cambia el CareGroup del folio → Actualiza RevenueControlDetail con HealthAdministratorId=NULL y ContractEntityId=NULL; si CareGroup.CareGroupType = 3 (Particulares) → Actualiza RevenueControlDetail con HealthAdministratorId=NULL, ThirdPartyId=@ThirdPartyPatientId y ContractEntityId=NULL; si Item con CUPSAssociateService=1 e IsAnnulled=1 → Marca el item como IsDelete=1 (se eliminará del folio); si @ListServiceOrderDetailWithQx contiene filas con PreviusServiceOrderDetailId>0 → Aplica los valores quirúrgicos homologados al ServiceOrderDetail previo, fijando ThirdPartyPercentage=100, PatientPercentage=0, ApplyRecoveryFee=1, RecoveryFeeType=1; si Fila de @ListServiceOrderDetailWithQx con Id=0 (servicio homólogo nuevo) → Inserta un nuevo ServiceOrderDetail y crea la distribución asociada en ServiceOrderDetailDistribution con DistributionType=1, ThirdPartyPercentage=100, ApplyRecoveryFee=1', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeRateServices';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Contract.ValidateHomologation; Billing.SP_RunChangeRatesServices_Output; Billing.SetValueSalesPrice; Billing.SP_UpdateRevenueControlDetailValues_Output', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeRateServices';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControlDetail; GeneralLedger.CompanySettings; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; Inventory.InventoryProduct; Contract.CareGroup; Inventory.ProductRateGeneral; Inventory.ProductRateDetail; Billing.ServiceOrderDetailSurgical; Contract.IPSService; Contract.CUPSEntityContractDescriptions; GeneralLedger.GeneralLedgerIVA; Contract.Contract; Contract.HealthAdministrator', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeRateServices';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeRateServices';
-- GO
