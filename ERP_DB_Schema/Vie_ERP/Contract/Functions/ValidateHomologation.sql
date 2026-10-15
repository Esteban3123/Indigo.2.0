CREATE Function [Contract].[ValidateHomologation]
(
	@RevenueControlDetailId Int,
	@CareGroupId Int,
	@distributionToRetarificXml Xml
)
Returns @Homologations Table
(
	StatusResult Bit,
	MessageResult Varchar(255),
	MultipleHomologations Bit,
	ObjectEmbbeded Xml
) 
As
Begin
	
	Declare @MultipleHomologations Bit = 0	
	Declare @distributionToRetarific Table(
		Id Int Primary key,  
		CUPSAssociateService Bit, 
		CodeAssociateService Varchar(50) Null,
		ServiceOrderDetailId Int,
		IsAnulled Bit, 
		IsDelete Bit,
		IsFirstEvent Bit,
		DistributionType Tinyint,
		ServiceOrderDetailSurgicalId Int Null,
		LastCaregroupId Int,
		RecordType Tinyint,
		CUPSEntityId Int Null,
		PerformsFunctionalUnitId Int,
		PerformsProfessionalSpecialty Char(3) Null,
		PerformsHealthProfessionalThirdPartyId Int Null,
		ServiceDate DateTime,
		IPSServiceId Int Null,
		CodeNameSpeciality Varchar(50) Null,
		CodeNameFunctionalUnit Varchar(50) Null,
		CodeNameHealthAdministrator Varchar(50) Null,
		ThirdPartyId Int Null,
		SettlementType Tinyint,
		RIASId int,
		ContractDescriptionId int)

	Insert Into @distributionToRetarific
	Select t.x.value('Id[1]','Int'),
		t.x.value('CUPSAssociateService[1]','Bit'),
		t.x.value('CodeAssociateService[1]','Varchar(50)'),
		t.x.value('ServiceOrderDetailId[1]','Int'),
		t.x.value('IsAnulled[1]','Bit'),
		t.x.value('IsDelete[1]','Bit'),
		t.x.value('IsFirstEvent[1]','Bit'),
		t.x.value('DistributionType[1]','Tinyint'),
		t.x.value('ServiceOrderDetailSurgicalId[1]','Int'),
		t.x.value('LastCaregroupId[1]','Int'),
		t.x.value('RecordType[1]','Tinyint'),
		t.x.value('CUPSEntityId[1]','Int'),
		t.x.value('PerformsFunctionalUnitId[1]','Int'),
		t.x.value('PerformsProfessionalSpecialty[1]','Char(3)'),
		t.x.value('PerformsHealthProfessionalThirdPartyId[1]','Int'),
		t.x.value('ServiceDate[1]','DateTime'),
		t.x.value('IPSServiceId[1]','Int'),
		t.x.value('CodeNameSpeciality[1]','Varchar(50)'),
		t.x.value('CodeNameFunctionalUnit[1]','Varchar(50)'),
		t.x.value('CodeNameHealthAdministrator[1]','Varchar(50)'),
		t.x.value('ThirdPartyId[1]','Int'),
		t.x.value('SettlementType[1]','Tinyint'),
		t.x.value('RIASId[1]','Int'),
		t.x.value('ContractDescriptionId[1]','Int')
	From @distributionToRetarificXml.nodes('/DistributionToRetarific') t(x)

	--Se filtra solo los detalles (Servicios) que no se han retarificado para éste grupo de atención
	Declare @ServiceOrderDetailDistributionId Int, 
		@CUPSAssociateService Bit, 
		@CodeAssociateService Varchar(50),
		@ServiceOrderDetailId Int,
		@IsAnulled Bit, 
		@IsDelete Bit,
		@IsFirstEvent Bit,
		@DistributionType Tinyint,		
		@LastCaregroupId Int,
		@RecordType Tinyint,
		@CUPSEntityId Int,
		@PerformsFunctionalUnitId Int,
		@PerformsProfessionalSpecialty Char(3),
		@ServiceDate DateTime,
		@IPSServiceId Int,
		@ThirdPartyId Int,
		@PerformsHealthProfessionalThirdPartyId Int,
		@RIASId int,
		@ContractDescriptionId int
		
	Declare @Rows Int, @RowId Int

	Set @Rows = 1
	Set @RowId = 1

	While @Rows > 0
	begin
		--Select Top 1 @RowId = RowId, @SurgicalNewXml = ServiceOrderDetailSurgicalXml From @listNewServiceOrderDetail Where RowId >= @RowId Order By RowId
		Select Top 1 @RowId = Id, @ServiceOrderDetailDistributionId = Id
			, @CUPSAssociateService = CUPSAssociateService, @CodeAssociateService = CodeAssociateService, @ServiceOrderDetailId = ServiceOrderDetailId
			, @IsAnulled = IsAnulled, @IsDelete = IsDelete, @IsFirstEvent = IsFirstEvent, @DistributionType = DistributionType, @LastCaregroupId = LastCaregroupId, @RecordType = RecordType
			, @CUPSEntityId = CUPSEntityId, @PerformsFunctionalUnitId = PerformsFunctionalUnitId, @PerformsProfessionalSpecialty = PerformsProfessionalSpecialty
			, @ServiceDate = ServiceDate, @IPSServiceId = IPSServiceId, @ThirdPartyId = ThirdPartyId
			, @PerformsHealthProfessionalThirdPartyId = PerformsHealthProfessionalThirdPartyId
			, @RIASId = RIASId
			, @ContractDescriptionId = ContractDescriptionId
		From @distributionToRetarific 
		Where LastCaregroupId <> @CareGroupId And DistributionType <> 2 And RecordType = 1 And CUPSAssociateService = 0 And Id >= @RowId Order By Id

		Set @Rows = @@ROWCOUNT
		If @Rows = 0 
			Break

		If @RecordType = 1 Begin
			
			If @CUPSAssociateService = 0 Begin
				Declare @ActionResultCupsHomologation Table(StatusResult Bit, MessageResult Varchar(255), 
					CupsHomologationId Int Null,
					IPSServiceId Int Null,
					CupsEntityId Int Null,
					CodeNameCupsEntity Varchar(320) Null,
					CodeNameIpsService Varchar(320) Null,
					Activated Bit)
					
				Insert Into @ActionResultCupsHomologation
				Select StatusResult, MessageResult, CupsHomologationId, IPSServiceId, CupsEntityId
					, CodeNameCupsEntity, CodeNameIpsService, Activated
					From [Contract].[GetHomologationCups](@CareGroupId, @CupsEntityId, @PerformsFunctionalUnitId
					, @PerformsProfessionalSpecialty, @ServiceDate, @IPSServiceId, Null, @RIASId, @ContractDescriptionId)
								
				If (Select Top 1 StatusResult From @ActionResultCupsHomologation) = 1 Begin
					
					If (Select Count(1) From @ActionResultCupsHomologation) = 0 Begin
						--Error ya que debe tener al menos un homologo
						Insert Into @Homologations
						Values (0, (Select Top 1 MessageResult From @ActionResultCupsHomologation), 0, '')
					End
					Else If (Select Count(1) From @ActionResultCupsHomologation) = 1 Begin
						--Solo contiene una homologación
						--todas las homologaciones uno a uno se marcan con la bandera activated = true para tomarlos al retarificar
						Update @ActionResultCupsHomologation Set Activated = 1
					End
					Else If (Select Count(1) From @ActionResultCupsHomologation) > 1 Begin
						Set @MultipleHomologations = 1
					End

					If @PerformsProfessionalSpecialty Is Not Null And @PerformsProfessionalSpecialty <> '' Begin
						Declare @CodeNameSpeciality Varchar(50) = ''
						Select @CodeNameSpeciality = Concat(Ltrim(Rtrim(CODESPECI)), ' - ', Ltrim(RTrim(DESESPECI))) From dbo.INESPECIA With(Nolock)
						Where CODESPECI = @PerformsProfessionalSpecialty
						Update @distributionToRetarific Set CodeNameSpeciality = @CodeNameSpeciality Where Id = @ServiceOrderDetailDistributionId
					End
					Declare @CodeNameFunctionalUnit Varchar(50) = '',
						@CodeNameHealthAdministrator Varchar(50) = ''
					Select @CodeNameHealthAdministrator = Concat(Nit, ' - ', [Name]) From [Common].ThirdParty With(Nolock) Where Id = @PerformsHealthProfessionalThirdPartyId
					Select @CodeNameFunctionalUnit = Concat(Code, ' - ', [Name]) From Payroll.FunctionalUnit With(Nolock) Where Id = @PerformsFunctionalUnitId

					Update @distributionToRetarific Set CodeNameFunctionalUnit = @CodeNameFunctionalUnit
						, CodeNameHealthAdministrator = @CodeNameHealthAdministrator
					Where Id = @ServiceOrderDetailDistributionId
					

					If Exists (Select 1 From @ActionResultCupsHomologation) Begin
						Declare @HomologationsXml Xml = (
							Select CupsHomologationId, IPSServiceId, CupsEntityId, CodeNameCupsEntity, CodeNameIpsService, Activated
							From @ActionResultCupsHomologation For Xml Path('CupsHomologation'), Elements
						)
						Declare @ServiceXml Xml = (
							Select *
							From @distributionToRetarific 
							Where Id = @ServiceOrderDetailDistributionId For Xml Path(''), Elements
						)
						Declare @Obj Xml = (Select @ServiceXml As [Service], @HomologationsXml As Homologations For Xml Path('Homologacion'), Elements)
						Insert Into @Homologations
						values(1, '', @MultipleHomologations, @Obj)

					End

				End
				Else Begin
					Insert Into @Homologations
					values(0, (Select Top 1 MessageResult From @ActionResultCupsHomologation), 0, '')
				End
				Delete From @ActionResultCupsHomologation
			End
		End
			
		Set @RowId += 1
	End
	
	Update @Homologations Set MultipleHomologations = @MultipleHomologations

	Return
End