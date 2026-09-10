
-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 19/11/2018
-- Description:	Procedimiento que se encarga de guardar y actualizar los CupsEntity
-- =============================================
CREATE PROCEDURE [Contract].[SP_SaveCupsEntity] 
	@Xml as xml,
	@CodeUser as varchar(20)
AS
BEGIN

	--Variables para obtener la info del xml
	declare @Id int, @CUPSSubGroupId int, @Code varchar(20), @Description varchar(300), @RIPSCode varchar(20), @RIPSDescription varchar(300), @RIPSConcept varchar(2), @RIPSServiceId int, @BillingConceptId int,
	@BillingGroupId int, @ServiceType tinyint, @Status bit, @MinimunAgeUnit tinyint, @MinimunAge int, @MaximumAgeUnit tinyint, @MaximumAge int, @Sex tinyint, @ShowServiceMedicalOrder tinyint,
	@ShowDashboardOf tinyint, @ShowDashboardOfAmbulatory tinyint, @TherapyProcedure bit, @AllowDiligenceInPlace bit, @AllowDiligenceReportRealizationQx bit, @SerialService bit, @RequiresInterpretation bit,
	@RequiresConfirmationRealization bit, @NutritionConsultation bit, @PsychologyConsultation bit, @YoungFirstTimeConsultation bit, @AdultFirstTimeConsultation bit, @AdvisoryPreTestElsaVIH bit,
	@AdvisoryPosTestElsaVIH bit, @NeonatalTSH bit, @SurfaceAntigen bit, @SerologySyphilis bit, @ElisaVIH bit, @Hemoglobin bit, @Creatine bit, @GlycosylatedHemoglobin bit, @Microalbuminuria bit,
	@HDL bit, @DiagnosticSmearMicroscopy bit, @PrenatalControlFirstTime bit, @PrenatalControl bit, @VisualAcuityAssessment bit, @OphthalmologyConsultation bit, 
	@GrowthDevelopmentFirstTimeConsultation bit, @FamilyPlanningFirstTime bit, @Mammography bit, @CervicalBiopsy bit, @BreastBiopsyBacaf bit, @BasalGlycaemia bit, @Creatinuria bit,
	@TotalCholesterol bit, @LDL bit, @PTH bit, @SerineAlbumin bit, @PhosphorusAlbumin bit, @ApplyRIAS bit, @RIASBillingConceptId int, @RIASBillingGroupId int, @OxigenService bit, @FinancedResourceUPC bit,
	@RequestRoomAutomatically bit,@IsPanel bit, @RequiresLaterality bit, @SurgicalReport bit, @ImageGuidanceProcedure bit

	--Tabla para las RIAS
	declare @TableCupsEntityRIAS table(TempId int, Id int, CupsCode varchar(20), RiasId int, RiasDescription varchar(100), ConceptRIPS varchar(2), IsDelete bit)

	--Tabla para las reglas de las RIAS
	declare @TableCupsEntityRIASDetail table(TempId int, Id int, CupsEntityRIASId int, [Rule] tinyint, MinimunAge int, MaximunAge int, Unit tinyint, Frequency int, 
	FrequencyUnit tinyint, RequireMedicalOrder bit, IsDelete bit, PeriodQuantity int, Sex tinyint, OxigenService bit, [Status] bit)

	--Tabla para la asociación entre el cups y la descripción
	declare @TableCupsEntityDescriptions table(Id int, CUPSEntityId int, ContractDescriptionId int, CupsSubgroupId int, BillingGroupId int, BillingConceptId int, IsDelete bit, ItemDelete bit)

	--Id del grupo cups
	declare @CupsGroupId int

	--Codigo del grupo cups
	declare @CupsGroupCode varchar(20)

	--Codigo del sub grupo cups
	declare @CupsSubGroupCode varchar(20)

	--Nombre del grupo
	declare @CupsGroupName AS varchar(200)

	--Tipo del grupo
	declare @CupsGroupImagingType AS bit

	--Si se muestra en dashboard de subgrupo
	declare @CupsSubGroupShowOnImagingDashboard AS bit

	--Nombre del subgrupo
	declare @CupsSubGroupName varchar(200)

	declare @CUPSEntityPanelDetail table (Id int,CUPSEntityPanelId int,CUPSEntityId int, IsDelete bit )

	Begin try
	
		--Se obtiene la cabecera del xml(CupsEntity)
		select 
			@Id = t.x.value('Id[1]','int'),
			@CUPSSubGroupId = t.x.value('CUPSSubGroupId[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@Description = t.x.value('Description[1]','varchar(300)'),
			@RIPSCode = t.x.value('RIPSCode[1]','varchar(20)'),
			@RIPSDescription = t.x.value('RIPSDescription[1]','varchar(300)'),
			@RIPSConcept = t.x.value('RIPSConcept[1]','varchar(2)'),
			@RIPSServiceId = iif( t.x.value('RIPSService[1]','int')='',NULL,t.x.value('RIPSService[1]','int')),
			@BillingConceptId = t.x.value('BillingConceptId[1]','int'),
			@BillingGroupId = t.x.value('BillingGroupId[1]','int'),
			@ServiceType = t.x.value('ServiceType[1]','tinyint'),
			@Status = t.x.value('Status[1]','bit'),
			@MinimunAgeUnit = t.x.value('MinimunAgeUnit[1]','tinyint'),
			@MinimunAge = t.x.value('MinimunAge[1]','int'),
			@MaximumAgeUnit = t.x.value('MaximumAgeUnit[1]','tinyint'),
			@MaximumAge = t.x.value('MaximumAge[1]','int'),
			@Sex = t.x.value('Sex[1]','tinyint'),
			@ShowServiceMedicalOrder = t.x.value('ShowServiceMedicalOrder[1]','tinyint'),
			@ShowDashboardOf = t.x.value('ShowDashboardOf[1]','tinyint'),
			@ShowDashboardOfAmbulatory = t.x.value('ShowDashboardOfAmbulatory[1]','tinyint'),
			@TherapyProcedure = t.x.value('TherapyProcedure[1]','bit'),
			@AllowDiligenceInPlace = t.x.value('AllowDiligenceInPlace[1]','bit'),
			@AllowDiligenceReportRealizationQx = t.x.value('AllowDiligenceReportRealizationQx[1]','bit'),
			@SerialService = t.x.value('SerialService[1]','bit'),
			@RequiresInterpretation = t.x.value('RequiresInterpretation[1]','bit'),
			@RequiresConfirmationRealization = t.x.value('RequiresConfirmationRealization[1]','bit'),
			@NutritionConsultation = t.x.value('NutritionConsultation[1]','bit'),
			@PsychologyConsultation = t.x.value('PsychologyConsultation[1]','bit'),
			@YoungFirstTimeConsultation = t.x.value('YoungFirstTimeConsultation[1]','bit'),
			@AdultFirstTimeConsultation = t.x.value('AdultFirstTimeConsultation[1]','bit'),
			@AdvisoryPreTestElsaVIH = t.x.value('AdvisoryPreTestElsaVIH[1]','bit'),
			@AdvisoryPosTestElsaVIH = t.x.value('AdvisoryPosTestElsaVIH[1]','bit'),
			@NeonatalTSH = t.x.value('NeonatalTSH[1]','bit'),
			@SurfaceAntigen = t.x.value('SurfaceAntigen[1]','bit'),
			@SerologySyphilis = t.x.value('SerologySyphilis[1]','bit'),
			@ElisaVIH = t.x.value('ElisaVIH[1]','bit'),
			@Hemoglobin = t.x.value('Hemoglobin[1]','bit'),
			@Creatine = t.x.value('Creatine[1]','bit'),
			@GlycosylatedHemoglobin = t.x.value('GlycosylatedHemoglobin[1]','bit'),
			@Microalbuminuria = t.x.value('Microalbuminuria[1]','bit'),
			@HDL = t.x.value('HDL[1]','bit'),
			@DiagnosticSmearMicroscopy = t.x.value('DiagnosticSmearMicroscopy[1]','bit'),
			@PrenatalControlFirstTime = t.x.value('PrenatalControlFirstTime[1]','bit'),
			@PrenatalControl = t.x.value('PrenatalControl[1]','bit'),
			@VisualAcuityAssessment = t.x.value('VisualAcuityAssessment[1]','bit'),
			@OphthalmologyConsultation = t.x.value('OphthalmologyConsultation[1]','bit'),
			@GrowthDevelopmentFirstTimeConsultation = t.x.value('GrowthDevelopmentFirstTimeConsultation[1]','bit'),
			@FamilyPlanningFirstTime = t.x.value('FamilyPlanningFirstTime[1]','bit'),
			@Mammography = t.x.value('Mammography[1]','bit'),
			@CervicalBiopsy = t.x.value('CervicalBiopsy[1]','bit'),
			@BreastBiopsyBacaf = t.x.value('BreastBiopsyBacaf[1]','bit'),
			@BasalGlycaemia = t.x.value('BasalGlycaemia[1]','bit'),
			@Creatinuria = t.x.value('Creatinuria[1]','bit'),
			@TotalCholesterol = t.x.value('TotalCholesterol[1]','bit'),
			@LDL = t.x.value('LDL[1]','bit'),
			@PTH = t.x.value('PTH[1]','bit'),
			@SerineAlbumin = t.x.value('SerineAlbumin[1]','bit'),
			@PhosphorusAlbumin = t.x.value('PhosphorusAlbumin[1]','bit'),
			@ApplyRIAS = t.x.value('ApplyRIAS[1]','bit'),
			@RIASBillingConceptId = t.x.value('RIASBillingConceptId[1]','int'),
			@RIASBillingGroupId = t.x.value('RIASBillingGroupId[1]','int'),
			@OxigenService = t.x.value('OxigenService[1]','bit'),
			@FinancedResourceUPC = t.x.value('FinancedResourceUPC[1]','bit'),
			@RequestRoomAutomatically = t.x.value('RequestRoomAutomatically[1]','bit'),
			@IsPanel=t.x.value('IsPanel[1]','bit'),
			@RequiresLaterality = t.x.value('RequiresLaterality[1]', 'bit'),
			@SurgicalReport = t.x.value('SurgicalReport[1]', 'bit'),
			@ImageGuidanceProcedure = t.x.value('ImageGuidanceProcedure[1]', 'bit')
		from @Xml.nodes('/CupsEntity') t(x)

		--Se obtiene las RIAS
		insert into @TableCupsEntityRIAS
		select 
			t.x.value('TempId[1]','int') as TempId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('CupsCode[1]','varchar(20)') as CupsCode,
			t.x.value('RiasId[1]','int') as RiasId,
			t.x.value('RiasDescription[1]','varchar(100)') as RiasDescription,
			t.x.value('ConceptRIPS[1]','varchar(2)') as ConceptRIPS,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/CupsEntity/CupsEntityRIAS') t(x)

		--Se obtiene las reglas de las RIAS
		insert into @TableCupsEntityRIASDetail
		select 
			t.x.value('TempId[1]','int') as TempId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('CupsEntityRIASId[1]','int') as CupsEntityRIASId,
			t.x.value('Rule[1]','tinyint') as [Rule],
			t.x.value('MinimunAge[1]','int') as MinimunAge,
			t.x.value('MaximunAge[1]','int') as MaximunAge,
			t.x.value('Unit[1]','tinyint') as Unit,
			t.x.value('Frequency[1]','int') as Frequency,
			t.x.value('FrequencyUnit[1]','tinyint') as FrequencyUnit,
			t.x.value('RequireMedicalOrder[1]','bit') as RequireMedicalOrder,
			t.x.value('IsDelete[1]','bit') as IsDelete,
			t.x.value('PeriodQuantity[1]','int') as PeriodQuantity,
			t.x.value('Sex[1]','tinyint') as Sex,
			t.x.value('OxigenService[1]','bit') as OxigenService,
			t.x.value('Status[1]','bit') as Status
		from @Xml.nodes('/CupsEntity/CupsEntityRIAS/CupsEntityRIASDetail') t(x)

		--Se obtiene las descripciones
		insert into @TableCupsEntityDescriptions
		select 
			t.x.value('Id[1]','int') as Id,
			t.x.value('CUPSEntityId[1]','int') as CUPSEntityId,
			t.x.value('ContractDescriptionId[1]','int') as ContractDescriptionId,
			t.x.value('CupsSubgroupId[1]','int') as CupsSubgroupId,
			t.x.value('BillingGroupId[1]','int') as BillingGroupId,
			t.x.value('BillingConceptId[1]','int') as BillingConceptId,
			t.x.value('IsDelete[1]','bit') as IsDelete,
			t.x.value('ItemDelete[1]','bit') as ItemDelete
		from @Xml.nodes('/CupsEntity/CUPSEntityContractDescriptions') t(x)

		insert into @CUPSEntityPanelDetail
		select 
			t.x.value('Id[1]','int') as Id,
			t.x.value('CUPSEntityPanelId[1]','int') as CUPSEntityPanelId,
			t.x.value('CUPSEntityId[1]','int') as CUPSEntityId,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/CupsEntity/CUPSEntityPanelDetail') t(x)

		--Si se va a guardar
		if @Id = 0
		begin
			
			INSERT INTO [Contract].[CUPSEntity]([CUPSSubGroupId], [Code], [Description], [RIPSCode], [RIPSDescription], [RIPSConcept], [BillingConceptId], [BillingGroupId],
			[ServiceType], [Status], [CreationUser], [CreationDate], [MinimunAgeUnit], [MinimunAge], [MaximumAgeUnit], [MaximumAge],
			[Sex], [ShowServiceMedicalOrder], [ShowDashboardOf],[ShowDashboardOfAmbulatory], [TherapyProcedure], [AllowDiligenceInPlace], [AllowDiligenceReportRealizationQx], [SerialService], 
			[RequiresInterpretation], [RequiresConfirmationRealization], [NutritionConsultation], [PsychologyConsultation], [YoungFirstTimeConsultation], [AdultFirstTimeConsultation], 
			[AdvisoryPreTestElsaVIH], [AdvisoryPosTestElsaVIH], [NeonatalTSH], [SurfaceAntigen], [SerologySyphilis], [ElisaVIH], [Hemoglobin], [Creatine], [GlycosylatedHemoglobin],
			[Microalbuminuria], [HDL], [DiagnosticSmearMicroscopy], [PrenatalControlFirstTime], [PrenatalControl], [VisualAcuityAssessment], [OphthalmologyConsultation],
			[GrowthDevelopmentFirstTimeConsultation], [FamilyPlanningFirstTime], [Mammography], [CervicalBiopsy], [BreastBiopsyBacaf], [BasalGlycaemia], [Creatinuria],
			[TotalCholesterol], [LDL], [PTH], [SerineAlbumin], [PhosphorusAlbumin], ApplyRIAS, RIASBillingConceptId, RIASBillingGroupId, [OxigenService], FinancedResourceUPC, RequestRoomAutomatically,
			IsPanel, RequiresLaterality, RIPSServiceId, SurgicalReport, ImageGuidanceProcedure)
			VALUES(@CUPSSubGroupId, @Code, @Description, @RIPSCode, @RIPSDescription, @RIPSConcept, @BillingConceptId, @BillingGroupId, @ServiceType, @Status, @CodeUser, [Common].[GETDATE](), 
			@MinimunAgeUnit, @MinimunAge, @MaximumAgeUnit, @MaximumAge, @Sex, @ShowServiceMedicalOrder, @ShowDashboardOf,@ShowDashboardOfAmbulatory, @TherapyProcedure, 
			@AllowDiligenceInPlace, @AllowDiligenceReportRealizationQx, @SerialService, @RequiresInterpretation, @RequiresConfirmationRealization, @NutritionConsultation, @PsychologyConsultation, 
			@YoungFirstTimeConsultation, @AdultFirstTimeConsultation, @AdvisoryPreTestElsaVIH, @AdvisoryPosTestElsaVIH, @NeonatalTSH, @SurfaceAntigen, @SerologySyphilis, @ElisaVIH, @Hemoglobin, 
			@Creatine, @GlycosylatedHemoglobin, @Microalbuminuria, @HDL, @DiagnosticSmearMicroscopy, @PrenatalControlFirstTime, @PrenatalControl, @VisualAcuityAssessment, @OphthalmologyConsultation,
			@GrowthDevelopmentFirstTimeConsultation, @FamilyPlanningFirstTime, @Mammography, @CervicalBiopsy, @BreastBiopsyBacaf, @BasalGlycaemia, @Creatinuria,
			@TotalCholesterol, @LDL, @PTH, @SerineAlbumin, @PhosphorusAlbumin, @ApplyRIAS, @RIASBillingConceptId, @RIASBillingGroupId,@OxigenService, @FinancedResourceUPC, @RequestRoomAutomatically,
			@IsPanel, @RequiresLaterality, @RIPSServiceId, @SurgicalReport, @ImageGuidanceProcedure)

			set @Id = SCOPE_IDENTITY()
		end
		else begin --Si se va actualizar
			
			update [Contract].[CUPSEntity] set CUPSSubGroupId = @CUPSSubGroupId, Code = @Code, Description = @Description, RIPSCode = @RIPSCode, RIPSDescription = @RIPSDescription, 
			RIPSConcept = @RIPSConcept, BillingConceptId = @BillingConceptId, BillingGroupId = @BillingGroupId, ServiceType = @ServiceType, Status = @Status, 
			ModificationUser = @CodeUser, ModificationDate = [Common].[GETDATE](), MinimunAgeUnit = @MinimunAgeUnit, MinimunAge = @MinimunAge, MaximumAgeUnit = @MaximumAgeUnit, MaximumAge = @MaximumAge,
			Sex = @Sex,  ShowServiceMedicalOrder = @ShowServiceMedicalOrder, ShowDashboardOf = @ShowDashboardOf,ShowDashboardOfAmbulatory = @ShowDashboardOfAmbulatory, TherapyProcedure = @TherapyProcedure, AllowDiligenceInPlace = @AllowDiligenceInPlace, 
			AllowDiligenceReportRealizationQx = @AllowDiligenceReportRealizationQx, SerialService = @SerialService, RequiresInterpretation = @RequiresInterpretation, 
			RequiresConfirmationRealization = @RequiresConfirmationRealization, NutritionConsultation = @NutritionConsultation, PsychologyConsultation = @PsychologyConsultation, 
			YoungFirstTimeConsultation = @YoungFirstTimeConsultation, AdultFirstTimeConsultation = @AdultFirstTimeConsultation, AdvisoryPreTestElsaVIH = @AdvisoryPreTestElsaVIH, 
			AdvisoryPosTestElsaVIH = @AdvisoryPosTestElsaVIH, NeonatalTSH = @NeonatalTSH, SurfaceAntigen = @SurfaceAntigen, SerologySyphilis = @SerologySyphilis, ElisaVIH = @ElisaVIH, 
			Hemoglobin = @Hemoglobin, Creatine = @Creatine, GlycosylatedHemoglobin = @GlycosylatedHemoglobin, Microalbuminuria = @Microalbuminuria, HDL = @HDL, DiagnosticSmearMicroscopy = @DiagnosticSmearMicroscopy, 
			PrenatalControlFirstTime = @PrenatalControlFirstTime, PrenatalControl = @PrenatalControl, VisualAcuityAssessment = @VisualAcuityAssessment, OphthalmologyConsultation = @OphthalmologyConsultation,
			GrowthDevelopmentFirstTimeConsultation = @GrowthDevelopmentFirstTimeConsultation, FamilyPlanningFirstTime = @FamilyPlanningFirstTime, Mammography = @Mammography, CervicalBiopsy = @CervicalBiopsy, 
			BreastBiopsyBacaf = @BreastBiopsyBacaf, BasalGlycaemia = @BasalGlycaemia, Creatinuria = @Creatinuria, TotalCholesterol = @TotalCholesterol, LDL = @LDL, PTH = @PTH, 
			SerineAlbumin = @SerineAlbumin, PhosphorusAlbumin = @PhosphorusAlbumin, ApplyRIAS = @ApplyRIAS, RIASBillingConceptId = @RIASBillingConceptId, RIASBillingGroupId = @RIASBillingGroupId,
			OxigenService = @OxigenService, FinancedResourceUPC = @FinancedResourceUPC, RequestRoomAutomatically = @RequestRoomAutomatically, IsPanel=@IsPanel, RequiresLaterality = @RequiresLaterality, RIPSServiceId = @RIPSServiceId,
			SurgicalReport = @SurgicalReport, ImageGuidanceProcedure = @ImageGuidanceProcedure
			where Id = @Id

		end

		/**********************************Creacion,Actualizacion o Eliminacion Detalles PANEL*********************************************/

				DELETE cepd
				FROM contract.CUPSEntityPanelDetail cepd WITH(NOLOCK)
				JOIN @CUPSEntityPanelDetail p ON cepd.Id= p.Id AND p.IsDelete =1

				DELETE @CUPSEntityPanelDetail WHERE IsDelete =1

				UPDATE cepd
				SET cepd.CUPSEntityPanelId=p.CUPSEntityPanelId,
					cepd.CUPSEntityId=p.CUPSEntityId,
					cepd.CreationUser= @CodeUser,
					cepd.CreationDate=[Common].[GETDATE]()
				FROM contract.CUPSEntityPanelDetail cepd WITH(NOLOCK)
				JOIN @CUPSEntityPanelDetail p ON cepd.Id= p.Id

				INSERT INTO contract.CUPSEntityPanelDetail
				SELECT @Id,p.CUPSEntityId,@CodeUser,[Common].[GETDATE]()
				FROM @CUPSEntityPanelDetail p
				WHERE Id =0

		/**********************************************************************************************************************************/
		--Se obtiene la info del subgrupo
		select @CupsSubGroupCode = Code, @CupsGroupId = CupsGroupId, @CupsSubGroupName = Name, @CupsSubGroupShowOnImagingDashboard = ShowOnImagingDashboard 
		from Contract.CupsSubgroup where Id = @CUPSSubGroupId
		
		--Se obtiene la info del grupo
		select @CupsGroupCode = Code, @CupsGroupName = Name, @CupsGroupImagingType = ImagingType from Contract.CupsGroup where Id = @CupsGroupId

		DECLARE @CODGRUSUB CHAR(13) = (SELECT CONCAT(@CupsGroupCode, '-', @CupsSubGroupCode))
		
		--Si no existe el grupo en Crystal se crea
		if NOT EXISTS(select * from dbo.INCUPSGRU where CODGRUIPS = @CupsGroupCode) begin
			insert into dbo.INCUPSGRU(CODGRUIPS, DESGRUIPS, GRUPOIMAG) values(@CupsGroupCode, @CupsGroupName, @CupsGroupImagingType)
		end
		
		--Si no existe el subgrupo en Crystal se crea
		if NOT EXISTS(select * from dbo.INCUPSSUB where CODGRUSUB = @CODGRUSUB) begin
			insert into dbo.INCUPSSUB(CODGRUSUB, CODGRUIPS, CODSUBIPS, DESSUBIPS, MDASHIMAG) 
			values(@CODGRUSUB, @CupsGroupCode, @CupsSubGroupCode, @CupsSubGroupName, ISNULL(@CupsSubGroupShowOnImagingDashboard, 0))
		end
		
		--Si el cups existe en Crystal se actualiza
		if EXISTS(select * from dbo.INCUPSIPS where CODSERIPS = @Code) begin
			
			update dbo.INCUPSIPS
			set
			[CODSERIPS] = @Code, [DESSERIPS] = @Description, [CODGRUIPS] = @CupsGroupCode, [CODSUBIPS] = @CupsSubGroupCode, [CODGRUSUB] = @CODGRUSUB, 
			[TIPSERIPS] = @ShowServiceMedicalOrder, [TIPSERTER] = @TherapyProcedure, [SERPERINF] = @AllowDiligenceReportRealizationQx, [EDADMAXI] = @MaximumAge, 
			[UNIEDAMAX] = case @MaximumAgeUnit when 3 then 0 when 2 then 1 when 1 then 2 end, [EDADMINI] = @MinimunAge, 
			[UNIEDADMI] = case @MinimunAgeUnit when 3 then 0 when 2 then 1 when 1 then 2 end, [SEXOSERIPS] = @Sex, [SIPSESTADO] = @Status, [IPSSERIAD] = @SerialService, [SERIPSDASH] = @ShowDashboardOf,[SERIPSDASHAMBU] = @ShowDashboardOfAmbulatory, 
			[TIPCONNUT] = @NutritionConsultation, [TIPCONPSI] = @PsychologyConsultation, [TIPCONJOV] = @YoungFirstTimeConsultation,  [TIPCONADU] = @AdultFirstTimeConsultation, 
			[TIPASEPRE] = @AdvisoryPreTestElsaVIH, [TIPASEPOS] = @AdvisoryPosTestElsaVIH, 
			[TIPTSHNEO] = @NeonatalTSH, [TIPANTHEP] = @SurfaceAntigen, [TIPSERSIF] = @SerologySyphilis, [TIPELIVIH] = @ElisaVIH, [TIPHEMOGL] = @Hemoglobin, [TIPCREATI] = @Creatine, [TIPHEMGLI] = @GlycosylatedHemoglobin, 
			[TIPMICROA] = @Microalbuminuria, [TIPHDL] = @HDL, [TIPBACDIA] = @DiagnosticSmearMicroscopy, [TIPCPPRIM] = @PrenatalControlFirstTime, [TIPCPRENA] = @PrenatalControl, [TIPVALVIS] = @VisualAcuityAssessment, 
			[TIPCONOFT] = @OphthalmologyConsultation, [TIPCONDES] = @GrowthDevelopmentFirstTimeConsultation, [TIPPLAFAM] = @FamilyPlanningFirstTime, [TIPMAMOG] = @Mammography, [TIPGLIBASA] = @BasalGlycaemia, 
			[TIPBIOCER] = @CervicalBiopsy, [TIPBIOSEN] = @BreastBiopsyBacaf, [CREATINURIA] = @Creatinuria, [COLETOTAL] = @TotalCholesterol, [LDL] = @LDL, [PTH] = @PTH, [ALBUMSERICA] = @SerineAlbumin, 
			[ALBUMFOSFO] = @PhosphorusAlbumin, [EXIGEINTERPRE] = @RequiresInterpretation, [EXIGECONFIRM] = @RequiresConfirmationRealization, [APLICARIAS] = @ApplyRIAS,
			CODGOCUPS = @Code, DESCODCUPS = @Description, [OXIGENSERVICE] = @OxigenService , [SERREASIT] =  @AllowDiligenceInPlace, SERIPSPOS = @FinancedResourceUPC, SOLISALA = @RequestRoomAutomatically, ISPANEL = @IsPanel, RequiresLaterality = @RequiresLaterality,
			ImageGuidanceProcedure = @ImageGuidanceProcedure
			where CODSERIPS = @Code

		end	
		else begin --Si no existe se crea
			
			INSERT INTO dbo.INCUPSIPS
			(
				[CODSERIPS], [DESSERIPS], [CODGRUIPS], [CODSUBIPS], [CODGRUSUB], [NIVSERIPS], [TIPSERIPS], [TIPSERTER], [SERPERINF], 
				[ARSCODIGO], [EDADMAXI], [UNIEDAMAX], [EDADMINI], [UNIEDADMI], [SEXOSERIPS], [SIPSESTADO], [IPSSERIAD], [SERIPSDASH], [TIPCONNUT],[TIPCONPSI], [TIPCONJOV], 
				[TIPCONADU], [TIPASEPRE], [TIPASEPOS], [TIPTSHNEO], [TIPANTHEP], [TIPSERSIF], [TIPELIVIH], [TIPHEMOGL], [TIPCREATI], [TIPHEMGLI], [TIPMICROA], [TIPHDL], [TIPBACDIA], 
				[TIPCPPRIM], [TIPCPRENA], [TIPVALVIS], [TIPCONOFT], [TIPCONDES], [TIPPLAFAM], [TIPMAMOG], [TIPGLIBASA], [TIPBIOCER], [TIPBIOSEN], [CREATINURIA], [COLETOTAL], 
				[LDL], [PTH], [ALBUMSERICA], [ALBUMFOSFO], [EXIGEINTERPRE], [EXIGECONFIRM], [APLICARIAS], CODGOCUPS, DESCODCUPS,[OXIGENSERVICE] , [SERREASIT], SERIPSPOS, SOLISALA, IsPanel,
				RequiresLaterality,[SERIPSDASHAMBU], ImageGuidanceProcedure
			)
			VALUES
			(
				@Code, @Description, @CupsGroupCode, @CupsSubGroupCode, @CODGRUSUB, 1, @ShowServiceMedicalOrder, @TherapyProcedure, @AllowDiligenceReportRealizationQx,
				NULL, @MaximumAge, case @MaximumAgeUnit when 3 then 0 when 2 then 1 when 1 then 2 end, @MinimunAge, case @MinimunAgeUnit when 3 then 0 when 2 then 1 when 1 then 2 end, @Sex, @Status, @SerialService, 
				@ShowDashboardOf, @NutritionConsultation, @PsychologyConsultation, @YoungFirstTimeConsultation,
				@AdultFirstTimeConsultation, @AdvisoryPreTestElsaVIH, @AdvisoryPosTestElsaVIH, @NeonatalTSH, @SurfaceAntigen, @SerologySyphilis, @ElisaVIH, @Hemoglobin, @Creatine, @GlycosylatedHemoglobin,
				@Microalbuminuria, @HDL, @DiagnosticSmearMicroscopy, @PrenatalControlFirstTime, @PrenatalControl, @VisualAcuityAssessment, @OphthalmologyConsultation, @GrowthDevelopmentFirstTimeConsultation,
				@FamilyPlanningFirstTime, @Mammography, @BasalGlycaemia, @CervicalBiopsy, @BreastBiopsyBacaf, @Creatinuria, @TotalCholesterol, @LDL, @PTH, @SerineAlbumin, @PhosphorusAlbumin, 
				@RequiresInterpretation, @RequiresConfirmationRealization, @ApplyRIAS, @Code, @Description,@OxigenService , @AllowDiligenceInPlace, @FinancedResourceUPC, @RequestRoomAutomatically, @IsPanel,
				@RequiresLaterality,@ShowDashboardOfAmbulatory, @ImageGuidanceProcedure
			)

		end
		
		if EXISTS(
			SELECT 1 FROM .RIASCUPSPACIENTE WHERE IDRIASCUPSD IN (SELECT 
				Id FROM [dbo].[RIASCUPSD] WHERE ID in (SELECT 
					Id FROM @TableCupsEntityRIASDetail WHERE Id > 0 and IsDelete = 1))
		)
			BEGIN
				select 999 as CodeMessage, 'No se puede eliminar la regla por que esta asociada a la atención de uno o varios pacientes' as Message
				RETURN
			END
		ELSE
			BEGIN
				--Si hay reglas para eliminar
				if (select COUNT(*) from @TableCupsEntityRIASDetail where Id > 0 and IsDelete = 1) > 0
				begin
					delete from [dbo].[RIASCUPSD] where ID in (select Id from @TableCupsEntityRIASDetail where Id > 0 and IsDelete = 1)
				end
			END

		--Si hay RIAS para eliminar
		if (select COUNT(*) from @TableCupsEntityRIAS where Id > 0 and IsDelete = 1) > 0
		begin
			delete from [dbo].[RIASCUPS] where ID in (select Id from @TableCupsEntityRIAS where Id > 0 and IsDelete = 1)
		end

		--Si hay descripciones para eliminar
		if exists(select 1 from @TableCupsEntityDescriptions where Id > 0 and ItemDelete = 1)
		begin
			delete from Contract.CUPSEntityContractDescriptions where Id in (select Id from @TableCupsEntityDescriptions where Id > 0 and ItemDelete = 1)
		end

		--Tabla para guardar el id que genera al insertar el detalle,
		--se realiza esto para poder guardar masivamente el segundo detalle
		declare @IDSRIAS table(Id int primary key)

		--Si hay RIAS para actualizar
		if (select COUNT(*) from @TableCupsEntityRIAS where Id > 0 and IsDelete = 0) > 0
		begin
			
			--Se actualizan los rias
			update rc set rc.[CODSERIPS] = tr.CupsCode, rc.[IDRIAS] = tr.RiasId, [CONCEPTORIPSRIAS] = tr.ConceptRIPS
			from @TableCupsEntityRIAS tr
			inner join dbo.RIASCUPS rc on rc.ID = tr.Id
			where tr.Id > 0 and tr.IsDelete = 0

		end

		--Si hay RIAS para insertar
		if (select COUNT(*) from @TableCupsEntityRIAS where Id = 0 and IsDelete = 0) > 0
		begin
			
			--Se insertan los RIAS y se obtienen los ids generados
			INSERT INTO [dbo].[RIASCUPS]([CODSERIPS], [IDRIAS], [CONCEPTORIPSRIAS]) output inserted.Id into @IDSRIAS(Id)
			select CupsCode, RiasId, ConceptRIPS
			from @TableCupsEntityRIAS 
			where Id = 0 and IsDelete = 0

			--Se actualiza la tabla temporal de RIAS con los ids generados
			update tr set tr.Id = rc.ID
			from dbo.RIASCUPS rc
			inner join @TableCupsEntityRIAS tr on tr.RiasId = rc.IDRIAS
			where rc.ID in (select Id from @IDSRIAS)

		end

		--Si hay reglas para actualizar
		if (select COUNT(*) from @TableCupsEntityRIASDetail where Id > 0 and IsDelete = 0) > 0
		begin

			update rcd set rcd.[IDRIASCUPS] = trd.CupsEntityRIASId, rcd.[REGLA] = trd.[Rule], rcd.[EDADMINIMA] = trd.MinimunAge, rcd.[EDADMAXIMA] = trd.MaximunAge, 
			rcd.[UNIDADRANGO] = trd.Unit, rcd.[FRECUENCIA] = trd.Frequency, rcd.[UNIDADFRECUENCIA] = trd.FrequencyUnit, rcd.[REQUIEREORDENMED] = trd.RequireMedicalOrder,
			rcd.CANTIDADPERIODO = trd.PeriodQuantity, rcd.SEXO = trd.Sex, rcd.ESTADO = trd.[Status]
			from @TableCupsEntityRIASDetail trd
			inner join dbo.RIASCUPSD rcd on rcd.ID = trd.Id
			where trd.Id > 0 and trd.IsDelete = 0

		end

		--Si hay reglas para insertar
		if (select COUNT(*) from @TableCupsEntityRIASDetail where Id = 0 and IsDelete = 0) > 0
		begin

			--Se insertan las reglas
			INSERT INTO [dbo].[RIASCUPSD]([IDRIASCUPS], [REGLA], [EDADMINIMA], [EDADMAXIMA], [UNIDADRANGO], [FRECUENCIA], [UNIDADFRECUENCIA], 
			[REQUIEREORDENMED], [CANTIDADPERIODO], SEXO, ESTADO)
			select tr.Id, trd.[Rule], trd.MinimunAge, trd.MaximunAge, trd.Unit, trd.Frequency, trd.FrequencyUnit, trd.RequireMedicalOrder, trd.PeriodQuantity, trd.Sex, trd.[Status]
			from @TableCupsEntityRIAS tr
			inner join @TableCupsEntityRIASDetail trd on trd.TempId = tr.TempId
			where trd.id = 0 and trd.IsDelete = 0

		end

		--Si hay descripciones para agregar
		if exists(select 1 from @TableCupsEntityDescriptions where Id = 0 and ItemDelete = 0)
		begin
			INSERT INTO [Contract].[CUPSEntityContractDescriptions]([CUPSEntityId], [ContractDescriptionId], [CupsSubgroupId], [BillingGroupId], [BillingConceptId], IsDelete)
			select @Id, t.ContractDescriptionId, t.CupsSubgroupId, t.BillingGroupId, t.BillingConceptId, t.IsDelete
			from @TableCupsEntityDescriptions t
			where t.Id = 0 and t.ItemDelete = 0
		end

		--Si hay descripciones para actualizar
		if exists(select 1 from @TableCupsEntityDescriptions where Id > 0 and ItemDelete = 0)
		begin
			update c set c.CUPSEntityId = t.CUPSEntityId, c.ContractDescriptionId = t.ContractDescriptionId, c.CupsSubgroupId = t.CupsSubgroupId, 
			c.BillingGroupId = t.BillingGroupId, c.BillingConceptId = t.BillingConceptId, c.IsDelete = t.IsDelete
			from @TableCupsEntityDescriptions t
			inner join Contract.CUPSEntityContractDescriptions c on c.Id = t.Id
			where t.Id > 0 and t.ItemDelete = 0
		end

		select 0 as CodeMessage, 'Se guardó correctamente' as Message
		
	end try
	begin catch
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que crea o actualiza la configuración completa de un servicio CUPS (procedimiento, examen, consulta o terapia) en el catálogo maestro del sistema. Recibe toda la información del servicio en formato XML, incluyendo el código CUPS, descripción, clasificación RIPS, concepto y grupo de facturación, restricciones por edad y sexo, indicadores clínicos especiales (nutrición, psicología, VIH, laboratorios de control prenatal, mamografía, entre otros) y reglas de Rutas Integrales de Atención en Salud (RIAS). Gestiona también la asociación del CUPS con subgrupos, descripciones de contrato, paneles de servicios y la configuración de visualización en órdenes médicas y tableros de control ambulatorio u hospitalario. Es el punto central de administración del catálogo de servicios para contratos, facturación y generación de RIPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCupsEntity';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCupsEntity';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (crea o actualiza) un servicio/procedimiento CUPS junto con su sincronización en el catálogo legacy (Crystal), sus paneles, asociaciones a RIAS con reglas de elegibilidad y descripciones contractuales, recibiendo toda la información en un XML.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCupsEntity';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener el nodo raíz CupsEntity con los atributos del servicio CUPS.; El CUPSSubGroupId del XML debe existir en Contract.CupsSubgroup y su CupsGroupId en Contract.CupsGroup para resolver códigos del grupo/subgrupo.; Para eliminar reglas RIAS (RIASCUPSD), no deben estar asociadas a pacientes en RIASCUPSPACIENTE; de lo contrario no se elimina y se aborta con mensaje de error.; Las unidades de edad (MinimunAgeUnit/MaximumAgeUnit) deben ser 1, 2 o 3 para mapearse correctamente al catálogo legacy (en otros valores el CASE retorna NULL).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCupsEntity';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Contract.CUPSEntity: Cuando el Id recibido = 0, se inserta un nuevo registro de CUPSEntity con CreationUser=@CodeUser y CreationDate=Common.GETDATE(), y se obtiene el Id con SCOPE_IDENTITY().; [UPDATE] Contract.CUPSEntity: Cuando el Id recibido > 0, se actualizan todos los atributos del CUPSEntity con ModificationUser=@CodeUser y ModificationDate=Common.GETDATE() para el Id indicado.; [DELETE] contract.CUPSEntityPanelDetail: Para cada detalle de panel recibido con IsDelete=1, se elimina el registro correspondiente por Id.; [UPDATE] contract.CUPSEntityPanelDetail: Para detalles de panel con Id existente (no marcados para borrado), se actualiza CUPSEntityPanelId, CUPSEntityId y se sella CreationUser=@CodeUser y CreationDate=Common.GETDATE().; [INSERT] contract.CUPSEntityPanelDetail: Para cada detalle de panel con Id=0, se inserta usando el Id del CUPSEntity actual como CUPSEntityPanelId, junto con CreationUser y fecha actual.; [INSERT] dbo.INCUPSGRU: Si no existe un grupo en INCUPSGRU con CODGRUIPS igual al código del grupo CUPS, se inserta con su nombre y tipo de imagen.; [INSERT] dbo.INCUPSSUB: Si no existe un subgrupo en INCUPSSUB con CODGRUSUB = ''<grupo>-<subgrupo>'', se inserta con códigos, nombre y MDASHIMAG (default 0 si nulo).; [UPDATE] dbo.INCUPSIPS: Si existe un servicio en INCUPSIPS con CODSERIPS = @Code, se actualizan sus atributos espejo desde el CUPSEntity, mapeando unidades de edad (3→0, 2→1, 1→2).; [INSERT] dbo.INCUPSIPS: Si no existe el servicio en INCUPSIPS, se inserta el nuevo registro espejo con NIVSERIPS=1 y mapeo de unidades de edad (3→0, 2→1, 1→2).; [DELETE] dbo.RIASCUPSD: Si hay reglas RIAS marcadas con Id>0 e IsDelete=1 y NO están referenciadas en RIASCUPSPACIENTE, se eliminan de RIASCUPSD.; [DELETE] dbo.RIASCUPS: Si hay RIAS con Id>0 e IsDelete=1, se eliminan de RIASCUPS.; [DELETE] Contract.CUPSEntityContractDescriptions: Si hay descripciones con Id>0 e ItemDelete=1, se eliminan de CUPSEntityContractDescriptions.; [UPDATE] dbo.RIASCUPS: Para RIAS con Id>0 e IsDelete=0, se actualizan CODSERIPS, IDRIAS y CONCEPTORIPSRIAS.; [INSERT] dbo.RIASCUPS: Para RIAS con Id=0 e IsDelete=0, se insertan nuevos registros y los Ids generados se capturan en una tabla para ligar el detalle.; [UPDATE] dbo.RIASCUPSD: Para reglas RIAS con Id>0 e IsDelete=0, se actualizan regla, rangos de edad/unidad, frecuencia, requiere orden médica, cantidad por periodo, sexo y estado.; [INSERT] dbo.RIASCUPSD: Para reglas RIAS con Id=0 e IsDelete=0, se insertan nuevas reglas vinculadas al RIAS por TempId (incluye nuevos RIAS recién insertados).; [INSERT] Contract.CUPSEntityContractDescriptions: Para descripciones con Id=0 e ItemDelete=0, se insertan asociadas al CUPSEntity actual.; [UPDATE] Contract.CUPSEntityContractDescriptions: Para descripciones con Id>0 e ItemDelete=0, se actualizan sus campos de relación y banderas.; [RETURN_RESULT] dbo.RIASCUPSPACIENTE: Si existe alguna regla RIAS marcada para borrar que esté asociada a pacientes, se retorna CodeMessage=999 con mensaje de no eliminación y se aborta el flujo con RETURN.; [RETURN_RESULT] (resultset): Al finalizar exitosamente, retorna CodeMessage=0 y mensaje ''Se guardó correctamente''.; [RETURN_RESULT] (resultset): En caso de excepción capturada, retorna CodeMessage=999 con ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCupsEntity';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCupsEntity';
-- GO
