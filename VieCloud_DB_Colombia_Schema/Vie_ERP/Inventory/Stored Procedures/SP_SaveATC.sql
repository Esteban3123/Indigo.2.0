

-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 25/04/2019
-- Description:	Se encarga de guardar en la tabla de ATC(ahora se llama medicamentos) tanto en VIE como en Crystal
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SaveATC]
	@Xml xml,
	@UserCode varchar(20)
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Variables para asignar los valores desde el xml para poder guardar
	declare @Id int, @DCIId int, @Code varchar(20), @Name varchar(200), @AbbreviationName varchar(100), @AdministrationRouteId int, @PharmacologicalGroupId int, @Presentations varchar(160), 
	@Concentration varchar(50), @InventoryRiskLevelId int, @StabilityMinimumHours int, @StabilityMaximumHours numeric(18,2), @FormulationType tinyint, @Weight numeric(18,2), @WeightMeasureUnit int, 
	@Volume numeric(18,2), @VolumeMeasureUnit int, @AdministrationUnitId int, @Warning varchar(max), @WarningHtml varchar(max), @Dosage varchar(max), @DosageHtml varchar(max), @Indications varchar(max), 
	@IndicationsHtml varchar(max), @ContraIndications varchar(max), @ContraIndicationsHtml varchar(max), @Precautions varchar(max), @PrecautionsHtml varchar(max), @AdverseReactions varchar(max), 
	@AdverseReactionsHtml varchar(max),	@AutomaticCalculation bit, @TransferSurplusProduct bit, @DiluentProduct bit, @SupplieProduct bit, @JustificationForSpecialDrugs bit, 
	@JustificationOfInputs bit, @IndicatorDrug bit, @Status bit, @ATCEntityId int, @POSProduct bit, @AllPOSPathologies bit, @BillingGroupNoPosId int, @ProductNPT bit, @ComponentType tinyint, @Osmolarity decimal(18, 2), 
	@Density decimal(6,4), @Consumption bit, @HasSupplieMedicine bit, @Antibiotic bit, @RequireMedicalBoard bit, @HighCost bit, @errors VARCHAR(MAX), @DefineProfessional bit, @ClinicalJustification varchar(2000),
	@Conditioned bit, @UNIRS bit, @Stability bit, @Multidose bit, @SuitableForReconstitution bit, @Combined bit, @ConcentrationQuantity DECIMAL(18,2), @ConcentrationMeasureUnitId int, @UPRUnitsId int, @TotalSubstanceConcentration varchar(50)

	--Tabla en donde se almacenan las patologías que vienen del xml
	declare @TablePathologies table(Id int, DiagnosticId int, DiagnosticCode varchar(20), MedicamentId int, IsDelete tinyint, MinimumAge tinyint, MaximumAge tinyint, AgeMeasure tinyint)

	declare @TableATCAR TABLE (Id int, ATCId int, ARId int, IsDelete bit, ARCode varchar(20) NULL, ARCodeCrystal varchar(20) NULL)--, ARPFCode varchar(20) NULL)

	--Tabla en donde se almacenan las concentraciones por el DCI del ATC
	declare @TableATCConcentrationByDCI TABLE (Id int, AtcId int, DCIId int, Concentration decimal(18,2), ConcentrationMeasureUnitId int,ChangeTracker varchar(30))

	declare @TableRSM TABLE (Id int, ATCId int, ItemType tinyint, SourceId int, IsDelete bit, SourceCode varchar(20) NULL, SourceDescription varchar(255) NULL)

	declare @TechnicalSheet table (Id int,ATCId int, DiagnosticId int, TechnicalSheetType Tinyint, Comment Varchar(300),ChangeTracker varchar(30))

	declare @TableATCClinicalData table (Id int,DataType int, Description nvarchar(max), ControlLaboratoryId int, TimeRequest int, Frequency int, DiagnosisId int, ATCId int, ChangeTracker varchar(30))

	--Tabla en donde realizo todas las validaciones de Crystal
	declare @TableError table(MessageError varchar(200))

	--Variable para concatenar el resultado de los errores de la tabla anterior
	declare @MessageError varchar(max) = ''

	--Código del DCI
	declare @DCICode varchar(20)

	--Código del grupo farmacológico
	declare @PharmacologicalGroupCode varchar(20)

	--Código de la vía de administración
	declare @AdministrationRouteCode varchar(20)

	--Id de la forma farmaceutica
	declare @PharmaceuticalFormId int

	--Código de la forma farmaceutica
	declare @PharmaceuticalFormCode varchar(20)
	
	--Código de la unidad de medida del peso
	declare @WeightMeasureUnitCode varchar(20)

	--Código de la unidad de medida del volumen
	declare @VolumeMeasureUnitCode varchar(20)

	--Código de la unidad de administración
	declare @AdministrationUnitCode varchar(20)

	--Código del nivel de riesgo
	declare @InventoryRiskLevelCode varchar(20)

	begin try
		
		--Se obtiene la cabecera del xml(ATC)
		select 
			@Id = t.x.value('Id[1]','int'),
			@DCIId = t.x.value('DCIId[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@Name = t.x.value('Name[1]','varchar(200)'),
			@AbbreviationName = t.x.value('AbbreviationName[1]','varchar(100)'),
			@AdministrationRouteId = t.x.value('AdministrationRouteId[1]','int'),
			@PharmacologicalGroupId = t.x.value('PharmacologicalGroupId[1]','int'),
			@Presentations = t.x.value('Presentations[1]','varchar(160)'),
			@Concentration = t.x.value('Concentration[1]','varchar(50)'),
			@InventoryRiskLevelId = t.x.value('InventoryRiskLevelId[1]','int'),
			@StabilityMinimumHours = t.x.value('StabilityMinimumHours[1]','int'),
			@StabilityMaximumHours = IIF(t.x.value('StabilityMaximumHours[1]','varchar(20)') = '', null, REPLACE(t.x.value('StabilityMaximumHours[1]','varchar(20)'), ',', '.')),
			@FormulationType = t.x.value('FormulationType[1]','tinyint'),
			@Weight = IIF(t.x.value('Weight[1]','varchar(20)') = '', null, REPLACE(t.x.value('Weight[1]','varchar(20)'), ',', '.')),
			@WeightMeasureUnit = IIF(t.x.value('WeightMeasureUnit[1]','varchar(20)') = '', null, t.x.value('WeightMeasureUnit[1]','varchar(20)')),
			@Volume = IIF(t.x.value('Volume[1]','varchar(20)') = '', null, REPLACE(t.x.value('Volume[1]','varchar(20)'), ',', '.')),
			@VolumeMeasureUnit = IIF(t.x.value('VolumeMeasureUnit[1]','varchar(20)') = '', null, t.x.value('VolumeMeasureUnit[1]','varchar(20)')),
			@AdministrationUnitId = IIF(t.x.value('AdministrationUnitId[1]','varchar(20)') = '', null, t.x.value('AdministrationUnitId[1]','varchar(20)')),
			@Warning = t.x.value('Warning[1]','varchar(max)'),
			@WarningHtml = dbo.DecodeXmlToText(t.x.value('WarningHtml[1]','varchar(max)')),
			@Dosage = t.x.value('Dosage[1]','varchar(max)'),
			@DosageHtml = dbo.DecodeXmlToText(t.x.value('DosageHtml[1]','varchar(max)')),
			@Indications = t.x.value('Indications[1]','varchar(max)'),
			@IndicationsHtml = dbo.DecodeXmlToText(t.x.value('IndicationsHtml[1]','varchar(max)')),
			@ContraIndications = t.x.value('ContraIndications[1]','varchar(max)'),
			@ContraIndicationsHtml = dbo.DecodeXmlToText(t.x.value('ContraIndicationsHtml[1]','varchar(max)')),
			@Precautions = t.x.value('Precautions[1]','varchar(max)'),
			@PrecautionsHtml = dbo.DecodeXmlToText(t.x.value('PrecautionsHtml[1]','varchar(max)')),
			@AdverseReactions = t.x.value('AdverseReactions[1]','varchar(max)'),
			@AdverseReactionsHtml = dbo.DecodeXmlToText(t.x.value('AdverseReactionsHtml[1]','varchar(max)')),
			@AutomaticCalculation = t.x.value('AutomaticCalculation[1]','bit'),
			@TransferSurplusProduct = t.x.value('TransferSurplusProduct[1]','bit'),
			@DiluentProduct = t.x.value('DiluentProduct[1]','bit'),
			@SupplieProduct = t.x.value('SupplieProduct[1]','bit'),
			@JustificationForSpecialDrugs = t.x.value('JustificationForSpecialDrugs[1]','bit'),
			@JustificationOfInputs = t.x.value('JustificationOfInputs[1]','bit'),
			@IndicatorDrug = t.x.value('IndicatorDrug[1]','bit'),
			@Status = t.x.value('Status[1]','bit'),
			@ATCEntityId = t.x.value('ATCEntityId[1]','int'),
			@POSProduct = t.x.value('POSProduct[1]','bit'),
			@AllPOSPathologies = IIF(t.x.value('AllPOSPathologies[1]','bit') = '', 0, t.x.value('AllPOSPathologies[1]','bit')),
			@BillingGroupNoPosId = IIF(t.x.value('BillingGroupNoPosId[1]','int') = '', null, t.x.value('BillingGroupNoPosId[1]','int')),
			@ProductNPT = t.x.value('ProductNPT[1]','bit'),
			@ComponentType = t.x.value('ComponentType[1]','tinyint'),
			@Osmolarity = IIF(t.x.value('Osmolarity[1]','varchar(10)') = '', null, cast(REPLACE(t.x.value('Osmolarity[1]','varchar(10)'), ',', '.') as decimal(18, 2))),
			@Density = IIF(t.x.value('Density[1]','varchar(10)') = '', null, cast(REPLACE(t.x.value('Density[1]','varchar(10)'), ',', '.') as decimal(6, 4))),
			@Consumption = IIF(t.x.value('Consumption[1]','varchar(20)') = '', null, t.x.value('Consumption[1]','varchar(20)')),
			@PharmaceuticalFormId = t.x.value('PharmaceuticalFormId[1]','int'),
			@HasSupplieMedicine = t.x.value('HasSupplieMedicine[1]','bit'),
			@Antibiotic = t.x.value('Antibiotic[1]','bit'),
			@RequireMedicalBoard = t.x.value('RequireMedicalBoard[1]','bit'),
			@HighCost = t.x.value('HighCost[1]','bit'),
			@DefineProfessional = t.x.value('DefineProfessional[1]','bit'),
			@ClinicalJustification = IIF(t.x.value('ClinicalJustification[1]','varchar(2000)') = '', null, t.x.value('ClinicalJustification[1]','varchar(2000)')),
			@Conditioned = t.x.value('Conditioned[1]','bit'),
			@UNIRS = t.x.value('UNIRS[1]','bit'),
			@Stability = t.x.value('Stability[1]','bit'),
			@Multidose = t.x.value('Multidose[1]','bit'),
			@SuitableForReconstitution = t.x.value('SuitableForReconstitution[1]','bit'),
			@Combined = t.x.value('Combined[1]','bit'),
			@ConcentrationQuantity = cast(REPLACE(t.x.value('ConcentrationQuantity[1]','varchar(50)'), ',', '.') as decimal(18, 2)),
			@ConcentrationMeasureUnitId = IIF(t.x.value('ConcentrationMeasureUnitId[1]','int') =0,null,t.x.value('ConcentrationMeasureUnitId[1]','int')) ,
			@UPRUnitsId = t.x.value('UPRUnitsId[1]','int'),
			@TotalSubstanceConcentration = t.x.value('TotalSubstanceConcentration[1]','varchar(50)')

		from @Xml.nodes('/ATC') t(x)

		--Se obtienen las patologías
		insert into @TablePathologies
		select 
		t.x.value('Id[1]','int') as Id,
		t.x.value('DiagnosticId[1]','int') as DiagnosticId,
		t.x.value('DiagnosticCode[1]','varchar(20)') as DiagnosticCode,		
		t.x.value('MedicamentId[1]','int') as MedicamentId,
		t.x.value('IsDelete[1]','tinyint') as IsDelete,
		t.x.value('MinimumAge[1]','tinyint') as MinimumAge,
		t.x.value('MaximumAge[1]','tinyint') as MaximumAge,
		t.x.value('AgeMeasure[1]','tinyint') as AgeMeasure
		from @Xml.nodes('/ATC/ProductPathologies') t(x)

		--se obtiene las vias de administracion
		insert into @TableATCAR (Id, ATCId, ARId, IsDelete)
		SELECT
		t.x.value('Id[1]','int') as Id,
		t.x.value('ATCId[1]','int') as ATCId,
		t.x.value('ARId[1]','int') as ARId,		
		t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/ATC/ATCAR') t(x)

		--se obtiene las concentraciones por DCI
		insert into @TableATCConcentrationByDCI (Id, AtcId, DCIId, Concentration, ConcentrationMeasureUnitId, ChangeTracker)
		SELECT
		t.x.value('Id[1]','int') as Id,
		t.x.value('AtcId[1]','int') as AtcId,
		t.x.value('DCIId[1]','int') as DCIId,		
		cast(REPLACE(t.x.value('Concentration[1]','varchar(50)'), ',', '.') as decimal(18, 2)) as Concentration,
		t.x.value('ConcentrationMeasureUnitId[1]','int') as ConcentrationMeasureUnitId,
		t.x.value('ChangeTracker[1]','varchar(30)') as ChangeTracker
		from @Xml.nodes('/ATC/ATCConcentrationByDCI') t(x)

		--se obtiene las vias de administracion
		insert into @TableRSM (Id, ATCId, ItemType, SourceId, IsDelete)
		SELECT
		t.x.value('Id[1]','int') as Id,
		t.x.value('ATCId[1]','int') as ATCId,
		t.x.value('ItemType[1]','tinyint') as ItemType,	
		t.x.value('SourceId[1]','int') as SourceId,	
		t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/ATC/RSM') t(x)

		--se obtiene los datos adicionales
		insert into @TechnicalSheet ( Id,ATCId, DiagnosticId, TechnicalSheetType, Comment,ChangeTracker)
		SELECT		
		t.x.value('Id[1]','int') as Id,
		t.x.value('ATCId[1]','int') as ATCId,
		t.x.value('DiagnosticId[1]','int') as DiagnosticId,
		t.x.value('TechnicalSheetType[1]','tinyint') as TechnicalSheetType,	
		t.x.value('Comment[1]','varchar(300)') as Comment,
		t.x.value('ChangeTracker[1]','varchar(30)') as ChangeTracker		
		from @Xml.nodes('/ATC/TechnicalSheet') t(x)

		--Se obtienen los datos clínicos
		insert into @TableATCClinicalData (Id, DataType, Description, ControlLaboratoryId, TimeRequest, Frequency, DiagnosisId, ATCId, ChangeTracker)
		SELECT
		t.x.value('Id[1]','int') as Id,
		t.x.value('DataType[1]','int') as DataType,
		t.x.value('Description[1]','nvarchar(max)') as Description,
		IIF(t.x.value('ControlLaboratoryId[1]','int') = '', NULL, t.x.value('ControlLaboratoryId[1]','int')) as ControlLaboratoryId,
		IIF(t.x.value('TimeRequest[1]','int') = '', NULL, t.x.value('TimeRequest[1]','int')) as TimeRequest,
		IIF(t.x.value('Frequency[1]','int') = '', NULL, t.x.value('Frequency[1]','int')) as Frequency,
		IIF(t.x.value('DiagnosisId[1]','int') = '', NULL, t.x.value('DiagnosisId[1]','int')) as DiagnosisId,
		t.x.value('ATCId[1]','int') as ATCId,
		t.x.value('ChangeTracker[1]','varchar(30)') as ChangeTracker
		from @Xml.nodes('/ATC/ATCClinicalData') t(x)

		--Se actualiza la Osmolaridad del Producto si este tiene la misma que el medicamento Cabecera
		UPDATE Inventory.InventoryProduct SET Osmolarity = @Osmolarity WHERE ATCId = @Id AND Osmolarity = (SELECT Osmolarity from Inventory.ATC WHERE Id = @Id)

		select top 1 @AdministrationRouteId = ARId from @TableATCAR where IsDelete = 0

		--Si se va a guardar
		if @Id = 0
		begin
			INSERT INTO [Inventory].[ATC]([DCIId], [Code], [Name], [AbbreviationName], [AdministrationRouteId], [PharmacologicalGroupId], [Presentations], [Concentration], [InventoryRiskLevelId],
			[StabilityMinimumHours], [StabilityMaximumHours], [FormulationType], [Weight], [WeightMeasureUnit], [Volume], [VolumeMeasureUnit], [AdministrationUnitId], [Warning], [WarningHtml], 
			[Dosage], [DosageHtml], [Indications], [IndicationsHtml], [ContraIndications], [ContraIndicationsHtml], [Precautions], [PrecautionsHtml], [AdverseReactions], [AdverseReactionsHtml],
			[AutomaticCalculation], [TransferSurplusProduct], [DiluentProduct], [SupplieProduct], [JustificationForSpecialDrugs], [JustificationOfInputs], [IndicatorDrug], [Status], [CreationUser], [CreationDate],
			ATCEntityId, POSProduct, AllPOSPathologies, BillingGroupNoPosId, ProductNPT, ComponentType, Osmolarity, Density, Consumption, PharmaceuticalFormId, HasSupplieMedicine, Antibiotic, RequireMedicalBoard, HighCost,
			DefineProfessional, ClinicalJustification, Conditioned, UNIRS, Stability, Multidose, SuitableForReconstitution, Combined, ConcentrationQuantity, ConcentrationMeasureUnitId, UPRUnitsId, TotalSubstanceConcentration)
			VALUES(@DCIId, @Code, @Name, @AbbreviationName, @AdministrationRouteId, @PharmacologicalGroupId, @Presentations, @Concentration, @InventoryRiskLevelId, @StabilityMinimumHours,
			@StabilityMaximumHours, @FormulationType, @Weight, @WeightMeasureUnit, @Volume, @VolumeMeasureUnit, @AdministrationUnitId, @Warning, @WarningHtml, @Dosage, @DosageHtml, @Indications,
			@IndicationsHtml, @ContraIndications, @ContraIndicationsHtml, @Precautions, @PrecautionsHtml, @AdverseReactions, @AdverseReactionsHtml, @AutomaticCalculation, @TransferSurplusProduct,
			@DiluentProduct, @SupplieProduct, @JustificationForSpecialDrugs, @JustificationOfInputs, @IndicatorDrug, @Status, @UserCode, [Common].[GETDATE](), @ATCEntityId, @POSProduct, @AllPOSPathologies, @BillingGroupNoPosId,
			@ProductNPT, @ComponentType, @Osmolarity, @Density, @Consumption, @PharmaceuticalFormId, @HasSupplieMedicine, @Antibiotic, @RequireMedicalBoard, @HighCost, @DefineProfessional, @ClinicalJustification,
			@Conditioned, @UNIRS, @Stability, @Multidose, @SuitableForReconstitution, @Combined, @ConcentrationQuantity, @ConcentrationMeasureUnitId, @UPRUnitsId, @TotalSubstanceConcentration)

			set @Id = SCOPE_IDENTITY()
		end
		else begin --Si se va actualizar
			UPDATE [Inventory].[ATC] set [DCIId] = @DCIId, [Code] = @Code, [Name] = @Name, [AbbreviationName] = @AbbreviationName, [AdministrationRouteId] = @AdministrationRouteId, 
			[PharmacologicalGroupId] = @PharmacologicalGroupId, [Presentations] = @Presentations, [Concentration] = @Concentration, [InventoryRiskLevelId] = @InventoryRiskLevelId,
			[StabilityMinimumHours] = @StabilityMinimumHours, [StabilityMaximumHours] = @StabilityMaximumHours, [FormulationType] = @FormulationType, [Weight] = @Weight, 
			[WeightMeasureUnit] = @WeightMeasureUnit, [Volume] = @Volume, [VolumeMeasureUnit] = @VolumeMeasureUnit, [AdministrationUnitId] = @AdministrationUnitId, [Warning] = @Warning, 
			[WarningHtml] = @WarningHtml, [Dosage] = @Dosage, [DosageHtml] = @DosageHtml, [Indications] = @Indications, [IndicationsHtml] = @IndicationsHtml, [ContraIndications] = @ContraIndications, 
			[ContraIndicationsHtml] = @ContraIndicationsHtml, [Precautions] = @Precautions, [PrecautionsHtml] = @PrecautionsHtml, [AdverseReactions] = @AdverseReactions, 
			[AdverseReactionsHtml] = @AdverseReactionsHtml, [AutomaticCalculation] = @AutomaticCalculation, [TransferSurplusProduct] = @TransferSurplusProduct, [DiluentProduct] = @DiluentProduct, SupplieProduct = @SupplieProduct,
			[JustificationForSpecialDrugs] = @JustificationForSpecialDrugs, [JustificationOfInputs] = @JustificationOfInputs, [IndicatorDrug] = @IndicatorDrug, 
			[Status] = @Status, [ModificationUser] = @UserCode, [ModificationDate] = [Common].[GETDATE](), ATCEntityId = @ATCEntityId, POSProduct = @POSProduct, AllPOSPathologies = @AllPOSPathologies,
			BillingGroupNoPosId = @BillingGroupNoPosId, ProductNPT = @ProductNPT, ComponentType = @ComponentType, Osmolarity = @Osmolarity, Density = @Density, Consumption = @Consumption,
			PharmaceuticalFormId = @PharmaceuticalFormId, HasSupplieMedicine = @HasSupplieMedicine, Antibiotic = @Antibiotic, RequireMedicalBoard = @RequireMedicalBoard, 
			HighCost = @HighCost, DefineProfessional = @DefineProfessional, ClinicalJustification = @ClinicalJustification, Conditioned = @Conditioned, UNIRS = @UNIRS, Stability = @Stability, Multidose = @Multidose, SuitableForReconstitution= @SuitableForReconstitution,
			Combined = @Combined, ConcentrationQuantity = @ConcentrationQuantity, ConcentrationMeasureUnitId = @ConcentrationMeasureUnitId, UPRUnitsId = @UPRUnitsId, TotalSubstanceConcentration = @TotalSubstanceConcentration
			WHERE Id = @Id
		end

		/*********************************** INSERTAR / ACTUALIZAR INFORMACION ADICIONAL ***********************************/
		INSERT INTO Inventory.TechnicalSheet
		(
			ATCId, DiagnosticId, TechnicalSheetType, Comment
		)SELECT	ts.ATCId,ts.DiagnosticId,ts.TechnicalSheetType, ts.Comment
		FROM @TechnicalSheet ts WHERE ts.ChangeTracker = 'Added'

		UPDATE its
				SET its.Comment = ts.Comment
		FROM @TechnicalSheet ts	
		JOIN Inventory.TechnicalSheet its ON ts.Id = its.Id
		WHERE its.ATCId = @Id AND ts.ChangeTracker = 'Modified'

		----------------------------------------------------------------------------------------------------------------------

		INSERT INTO Inventory.ATCConcentrationByDCI
		(
			AtcId, DCIId, Concentration, ConcentrationMeasureUnitId
		)SELECT	@Id,ta.DCIId,ta.Concentration, ta.ConcentrationMeasureUnitId
		FROM @TableATCConcentrationByDCI ta WHERE ta.ChangeTracker = 'Added'

		UPDATE its
				SET its.Concentration = ta.Concentration,
					its.ConcentrationMeasureUnitId = ta.ConcentrationMeasureUnitId
		FROM @TableATCConcentrationByDCI ta	
		JOIN Inventory.ATCConcentrationByDCI its ON ta.Id = its.Id
		WHERE its.ATCId = @Id AND ta.ChangeTracker = 'Modified'

		DELETE its
		FROM @TableATCConcentrationByDCI ta	
		JOIN Inventory.ATCConcentrationByDCI its ON ta.Id = its.Id
		WHERE its.ATCId = @Id AND ta.ChangeTracker = 'Deleted'

		----------------------------------------------------------------------------------------------------------------------

		INSERT INTO Inventory.ATCClinicalData
		(
			DataType, Description, ControlLaboratoryId, TimeRequest, Frequency, DiagnosisId, ATCId
		)SELECT tacd.DataType, tacd.Description, tacd.ControlLaboratoryId, tacd.TimeRequest, tacd.Frequency, tacd.DiagnosisId, @Id
		FROM @TableATCClinicalData tacd WHERE tacd.ChangeTracker = 'Added'

		UPDATE acd
				SET acd.DataType = tacd.DataType,
					acd.Description = tacd.Description,
					acd.ControlLaboratoryId = tacd.ControlLaboratoryId,
					acd.TimeRequest = tacd.TimeRequest,
					acd.Frequency = tacd.Frequency,
					acd.DiagnosisId = tacd.DiagnosisId
		FROM @TableATCClinicalData tacd
		JOIN Inventory.ATCClinicalData acd ON tacd.Id = acd.Id
		WHERE acd.ATCId = @Id AND tacd.ChangeTracker = 'Modified'

		DELETE acd
		FROM @TableATCClinicalData tacd
		JOIN Inventory.ATCClinicalData acd ON tacd.Id = acd.Id
		WHERE acd.ATCId = @Id AND tacd.ChangeTracker = 'Deleted'

		----------------------------------------------------------------------------------------------------------------------

		--Se actualizan todos lo productos que tengan asociado el medicamento(antes atc)
		update Inventory.InventoryProduct set Consumption = @Consumption, POSProduct = @POSProduct, AllPOSPathologies = @AllPOSPathologies where ATCId = @Id

		--Se obtiene el código del dci
		select @DCICode = Code from Inventory.DCI where Id = @DCIId

		--Se valida si existe el dci en Crystal
		if(select count(*) from .IHDCIMEDI where CODDCIMED = @DCICode) = 0
		begin
			insert into @TableError values('El DCI con código ' + @DCICode + ' no existe en Crystal')
		end

		--Se obtiene el código del grupo farmacológico
		select @PharmacologicalGroupCode = Code from Inventory.PharmacologicalGroup where Id = @PharmacologicalGroupId

		--Se valida si existe el grupo farmacologico en Crystal
		if(select count(*) from .IHGRUFARM where CODGRUFAR = @PharmacologicalGroupCode) = 0
		begin
			insert into @TableError values('El grupo farmacológico con código ' + @PharmacologicalGroupCode + ' no existe en Crystal')
		end

		--Se obtiene el código y el id de la forma farmaceutica de la vía de administración
		--select @AdministrationRouteCode = Code, @PharmaceuticalFormId = PharmaceuticalFormId from Inventory.AdministrationRoute where Id = @AdministrationRouteId
		UPDATE ATCAR SET ARCode = AR.Code, ARCodeCrystal = AR.CrystalAdministrationRoute--, ARPFCode = PF.Code
		FROM @TableATCAR ATCAR 
		INNER JOIN Inventory.AdministrationRoute AR WITH(NOLOCK)
		ON AR.Id = ATCAR.ARId
		--inner join Inventory.PharmaceuticalForm PF
		--ON PF.Id = AR.PharmaceuticalFormId

		--Se valida si existe la via de administración en Crystal
		--if(select count(*) from .HCVIAADMI where CODVIAADM = @AdministrationRouteCode) = 0
		IF (SELECT COUNT(1) FROM @TableATCAR ATCAR
		LEFT OUTER JOIN .HCVIAADMI AR WITH(NOLOCK)
		ON AR.CODVIAADM = ATCAR.ARCodeCrystal
		WHERE 
		ATCAR.IsDelete = 0 AND AR.CODVIAADM IS NULL) > 0
		begin
			
			SELECT @errors = stuff((SELECT DISTINCT N', ' + ATCAR.ARCode + ''
			FROM @TableATCAR ATCAR
			LEFT OUTER JOIN .HCVIAADMI AR WITH(NOLOCK)
			ON AR.CODVIAADM = ATCAR.ARCodeCrystal
			WHERE 
			ATCAR.IsDelete = 0 AND AR.CODVIAADM IS NULL
			for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			insert into @TableError values('No existe en Crystal las siguientes vías de administración con código :' + @errors)
		end

		--Se obtiene el código de la forma farmaceutica que tiene relacionado la via de administración
		select @PharmaceuticalFormCode = Code from Inventory.PharmaceuticalForm where Id = @PharmaceuticalFormId

		--Se valida si existe la forma farmaceutica en Crystal
		if(select count(*) from .IHFORMEDI where CODFORMED = @PharmaceuticalFormCode) = 0
		begin
			insert into @TableError values('La forma farmaceútica con código ' + @PharmaceuticalFormCode + ' no existe en Crystal')
		end
		--IF (SELECT COUNT(1) FROM @TableATCAR ATCAR
		--LEFT OUTER JOIN .IHFORMEDI PF
		--	ON PF.CODFORMED = ATCAR.ARPFCode
		--WHERE
		--ATCAR.IsDelete = 0 AND PF.CODFORMED IS NULL) > 0
		--begin
		--	set @errors =  ''
		--	SELECT @errors = stuff((SELECT DISTINCT N', ' + ATCAR.ARPFCode + ''
		--	FROM @TableATCAR ATCAR
		--	LEFT OUTER JOIN .IHFORMEDI PF
		--	ON PF.CODFORMED = ATCAR.ARPFCode
		--	WHERE 
		--	ATCAR.IsDelete = 0 AND PF.CODFORMED IS NULL
		--	for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
		--	insert into @TableError values('No existe en Crystal las siguientes formas farmaceúticas con código :' + @errors)
		--end

		--Si el tipo de formula es peso se valida la unidad de medida
		if @FormulationType = 1
		begin 
			--Se obtiene el código de la unidad de medida
			select @WeightMeasureUnitCode = Code from Inventory.InventoryMeasurementUnit where Id = @WeightMeasureUnit

			--Se valida si la unidad de medida existe en Crystal
			if(select count(*) from .INUNIMEDI where CODUNIMED = @WeightMeasureUnitCode) = 0
			begin
				insert into @TableError values('La unidad de medida del peso con código ' + @WeightMeasureUnitCode + ' no existe en Crystal')
			end
		end

		--Si el tipo de formula es volumen se valida la unidad de medida
		if @FormulationType = 2
		begin 
			--Se obtiene el código de la unidad de medida
			select @VolumeMeasureUnitCode = Code from Inventory.InventoryMeasurementUnit where Id = @VolumeMeasureUnit

			--Se valida si la unidad de medida existe en Crystal
			if(select count(*) from .INUNIMEDI where CODUNIMED = @VolumeMeasureUnitCode) = 0
			begin
				insert into @TableError values('La unidad de medida del volumen con código ' + @VolumeMeasureUnitCode + ' no existe en Crystal')
			end
		end

		--Si el tipo de formula es peso-volumen se validan las unidades de medida de los dos
		if @FormulationType = 3
		begin 
			--Se obtiene el código de la unidad de medida del peso
			select @WeightMeasureUnitCode = Code from Inventory.InventoryMeasurementUnit where Id = @WeightMeasureUnit

			--Se valida si la unidad de medida del peso existe en Crystal
			if(select count(*) from .INUNIMEDI where CODUNIMED = @WeightMeasureUnitCode) = 0
			begin
				insert into @TableError values('La unidad de medida para el peso con código ' + @WeightMeasureUnitCode + ' no existe en Crystal')
			end

			--Se obtiene el código de la unidad de medida del volumen
			select @VolumeMeasureUnitCode = Code from Inventory.InventoryMeasurementUnit where Id = @VolumeMeasureUnit

			--Se valida si la unidad de medida del volumen existe en Crystal
			if(select count(*) from .INUNIMEDI where CODUNIMED = @VolumeMeasureUnitCode) = 0
			begin
				insert into @TableError values('La unidad de medida para el volumen con código ' + @VolumeMeasureUnitCode + ' no existe en Crystal')
			end
		end

		--Si el tipo de formula es unidad de administración se valida
		if @FormulationType = 4
		begin
			--Se obtiene el código de la unidad de administración
			select @AdministrationUnitCode = Code from Inventory.InventoryMeasurementUnit where Id = @AdministrationUnitId

			--Se valida si la unidad de medida existe en Crystal
			if(select count(*) from .INUNIMEDI where CODUNIMED = @AdministrationUnitCode) = 0
			begin
				insert into @TableError values('La unidad de medida con código ' + @AdministrationUnitCode + ' no existe en Crystal')
			end
		end

		--Se obtiene el código del nivel de riesgo
		select @InventoryRiskLevelCode = Code from Inventory.InventoryRiskLevel where Id = @InventoryRiskLevelId

		--Se valida si el nivel de riesgo existe en Crystal
		if(select count(*) from .INIVERIES where CODNIVRIE = @InventoryRiskLevelCode) = 0
		begin
			insert into @TableError values('El nivel de riesgo con código ' + @InventoryRiskLevelCode + ' no existe en Crystal')
		end

		UPDATE RSM SET SourceCode = ATC.CODE, SourceDescription = LP.DESPRODUC	 
		from @TableRSM RSM
		INNER JOIN Inventory.ATC  WITH(NOLOCK)
		ON ATC.Id = RSM.SourceId
		LEFT OUTER JOIN .IHLISTPRO LP WITH(NOLOCK)
		ON LP.CODPRODUC = ATC.Code
		WHERE --RSM.Id = 0 and RSM.IsDelete = 0 AND 
		RSM.ItemType = 2 --Medicamento

		UPDATE RSM SET SourceCode = SP.CODE, SourceDescription = LP.DESPRODUC
		from @TableRSM RSM
		INNER JOIN Inventory.InventorySupplie SP WITH(NOLOCK)
		ON SP.Id = RSM.SourceId
		LEFT OUTER JOIN .IHLISTPRO LP WITH(NOLOCK)
		ON LP.CODPRODUC = SP.Code
		WHERE --RSM.Id = 0 and RSM.IsDelete = 0 AND 
		RSM.ItemType = 1 --Insumo

		IF (SELECT COUNT(1) FROM @TableRSM RSM WHERE RSM.Id = 0 and RSM.IsDelete = 0 AND RSM.ItemType = 1 AND RSM.SourceDescription IS NULL) > 0
		BEGIN
			SET @errors = N''
			SELECT @errors = stuff((SELECT DISTINCT N', ' + RSM.SourceCode + ''
			FROM @TableRSM RSM WHERE RSM.Id = 0 and RSM.IsDelete = 0 AND RSM.ItemType = 1 AND RSM.SourceDescription IS NULL
			for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			insert into @TableError values('No existe en Crystal los siguientes códigos de insumos :' + @errors)
		END

		IF (SELECT COUNT(1) FROM @TableRSM RSM WHERE RSM.Id = 0 and RSM.IsDelete = 0 AND RSM.ItemType = 2 AND RSM.SourceDescription IS NULL) > 0
		BEGIN
			SET @errors = N''
			SELECT @errors = stuff((SELECT DISTINCT N', ' + RSM.SourceCode + ''
			FROM @TableRSM RSM WHERE RSM.Id = 0 and RSM.IsDelete = 0 AND RSM.ItemType = 2 AND RSM.SourceDescription IS NULL
			for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			insert into @TableError values('No existe en Crystal los siguientes códigos de medicamentos :' + @errors)
		END

		
		--Si hay errores en la tabla
		if(select count(*) from @TableError) > 0
		begin
			--Se obtienen los errores de la tabla
			select @MessageError = MessageError + CHAR(13) + CHAR(10) + @MessageError from @TableError

			--Retorna el error
			select 999 as CodeResult, @MessageError as MessageResult, '' as Code
			return
		end

		select top 1 
		--@PharmaceuticalFormCode = ARPFCode, 
		@AdministrationRouteCode = ARCodeCrystal from @TableATCAR where IsDelete = 0
		
		if @Multidose = 0 begin
			set @StabilityMaximumHours = 1
		end

		--Si el medicamento no existe en Crystal se crea
		if(select count(*) from .IHLISTPRO where CODPRODUC = @Code) = 0
		begin
			INSERT INTO .[IHLISTPRO]([CODPRODUC], [CODDCIMED], [DESPRODUC], [NOPOSPROD], [TIPPRODUC], [MANCONPRO], [REGINVACT], [REGINVIMA], [CODGRUFAR], [CODVIAADM], [CONCENMED], 
			[PRESENMED], [CODFORMED], [TIEESTMED], [TIPFORMED], [PESTOTMED], [CODUNIPES], [VOLTOTMED], [CODUNIVOL], [CODUNIADM], [CALCANAUT], [ESPDILPRO], [ABRPROMEZ], [PROESTADO], [PROCONTRO],
			[TODASPATO], [MANLOCALI], [RETRASOGE], [CODUSUCRE], [FECUSUCRE], [JUSINMEDI], [CODNIVRIE], [CODJUMEES], [ADVERTENC], [POSOLOGIA], [MEDTRAZA], [MATOSTOSIN], [MEDICAMENTONPT], [OSMOLARIDAD], 
			[DENSIDAD], [CONSUMPTION])
			VALUES(@Code, @DCICode, @Name, case @POSProduct when 0 then 1 else 0 end, iif(@SupplieProduct = 1, 3, 1), 0, 0, null, @PharmacologicalGroupCode, 
			@AdministrationRouteCode, @Concentration, @Presentations, @PharmaceuticalFormCode,
			@StabilityMaximumHours, @FormulationType, iif(@FormulationType = 1 or @FormulationType = 3, @Weight, null), iif(@FormulationType = 1 or @FormulationType = 3, @WeightMeasureUnitCode, null),
			iif(@FormulationType = 2 or @FormulationType = 3, @Volume, null), iif(@FormulationType = 2 or @FormulationType = 3, @VolumeMeasureUnitCode, null),
			iif(@FormulationType = 4, @AdministrationUnitCode, null), @AutomaticCalculation, @DiluentProduct, @AbbreviationName, @Status, 0, @AllPOSPathologies, 0, @TransferSurplusProduct, @UserCode,
			[Common].[GETDATE](), @JustificationOfInputs, @InventoryRiskLevelCode, @JustificationForSpecialDrugs, @Warning, null, @IndicatorDrug, null, @ProductNPT, @Osmolarity, @Density, @Consumption)
		end
		else begin --Si existe se actualiza
			UPDATE .[IHLISTPRO] set [CODPRODUC] = @Code, [CODDCIMED] = @DCICode, [DESPRODUC] = @Name, [NOPOSPROD] = case @POSProduct when 0 then 1 else 0 end, 
			[TIPPRODUC] = iif(@SupplieProduct = 1, 3, 1), 
			[MANCONPRO] = 0, [REGINVACT] = 0, [REGINVIMA] = null, [CODGRUFAR] = @PharmacologicalGroupCode, [CODVIAADM] = @AdministrationRouteCode, [CONCENMED] = @Concentration, 
			[PRESENMED] = @Presentations, [CODFORMED] = @PharmaceuticalFormCode, [TIEESTMED] = @StabilityMaximumHours, [TIPFORMED] = @FormulationType, 
			[PESTOTMED] = iif(@FormulationType = 1 or @FormulationType = 3, @Weight, null), [CODUNIPES] = iif(@FormulationType = 1 or @FormulationType = 3, @WeightMeasureUnitCode, null), 
			[VOLTOTMED] = iif(@FormulationType = 2 or @FormulationType = 3, @Volume, null), [CODUNIVOL] = iif(@FormulationType = 2 or @FormulationType = 3, @VolumeMeasureUnitCode, null), 
			[CODUNIADM] = iif(@FormulationType = 4, @AdministrationUnitCode, null), [CALCANAUT] = @AutomaticCalculation, [ESPDILPRO] = @DiluentProduct, [ABRPROMEZ] = @AbbreviationName, 
			[PROESTADO] = @Status, [TODASPATO] = @AllPOSPathologies, [MANLOCALI] = 0, [RETRASOGE] = @TransferSurplusProduct, [CODUSUMOD] = @UserCode, [FECUSUMOD] = [Common].[GETDATE](), 
			[JUSINMEDI] = @JustificationOfInputs, [CODNIVRIE] = @InventoryRiskLevelCode, [CODJUMEES] = @JustificationForSpecialDrugs, [ADVERTENC] = @Warning, 
			[POSOLOGIA] = null, [MEDTRAZA] = @IndicatorDrug, [MATOSTOSIN] = null, [MEDICAMENTONPT] = @ProductNPT, [OSMOLARIDAD] = @Osmolarity, [DENSIDAD] = @Density, CONSUMPTION = @Consumption
			where CODPRODUC = @Code
		end

		--Si el medicamento es pos se realiza la lógica de las patologías
		if @POSProduct = 1 and @AllPOSPathologies = 0
		begin
			--Si hay patologías para actualizar
			if(select count(1) from @TablePathologies where Id > 0 and IsDelete = 2) > 0 
			begin
				--Se guarda en Crystal
				update  PP set CODDIAGNO = TP.DiagnosticCode, MinimumAge = TP.MinimumAge, MaximumAge = TP.MaximumAge, AgeMeasure = TP.AgeMeasure
				from @TablePathologies TP
				INNER JOIN Inventory.POSPathologies P
				ON P.Id = TP.Id
				INNER JOIN Inventory.Diagnostic D
				ON D.Id = P.DiagnosticId
				INNER JOIN  .INPRODPAT PP
				ON PP.IPRCODIGO = @Code and pp.CODDIAGNO = D.Code AND PP.MinimumAge = P.MinimumAge AND PP.MaximumAge = P.MaximumAge AND PP.AgeMeasure = P.AgeMeasure
				where TP.Id > 0 and TP.IsDelete = 2

				--Se guarda en VIE
				update Inventory.POSPathologies set DiagnosticId = TP.DiagnosticId, MinimumAge = TP.MinimumAge, MaximumAge = TP.MaximumAge, AgeMeasure = TP.AgeMeasure
				from @TablePathologies TP
				where TP.Id > 0 and TP.IsDelete = 2 AND POSPathologies.Id = TP.Id
				
			end

			--Si hay patologías para eliminar
			if(select count(1) from @TablePathologies where IsDelete = 1) > 0
			begin
				--Se eliminan de VIE
				delete from Inventory.POSPathologies where Id in (select Id from @TablePathologies where IsDelete = 1)

				--Se eliminan de Crystal
				delete from .INPRODPAT
				FROM @TablePathologies P
				where
				P.IsDelete = 1 
				AND INPRODPAT.IPRCODIGO = @Code 
				AND INPRODPAT.CODDIAGNO = P.DiagnosticCode
				AND INPRODPAT.MinimumAge = P.MinimumAge
				AND INPRODPAT.MaximumAge = P.MaximumAge
				AND INPRODPAT.AgeMeasure = P.AgeMeasure
				
			end

			--Si hay patologías para guardar
			if(select count(1) from @TablePathologies where Id = 0 and IsDelete = 0) > 0
			begin
				--Se guarda en VIE
				insert into Inventory.POSPathologies(ProductId, DiagnosticId, MedicamentId, MinimumAge, MaximumAge, AgeMeasure)
				select NULL, DiagnosticId, @Id, MinimumAge, MaximumAge, AgeMeasure
				from @TablePathologies
				where Id = 0 and IsDelete = 0

				--Se guarda en Crystal
				insert into .INPRODPAT(IPRCODIGO, CODDIAGNO, MinimumAge, MaximumAge, AgeMeasure)
				select @Code, DiagnosticCode, MinimumAge, MaximumAge, AgeMeasure
				from @TablePathologies
				where Id = 0 and IsDelete = 0
			end

			
		end
		else begin --Si el medicamento es no pos se eliminan todas las patologias asociadas o si el medicamento maneja todas las patologias
			--Se eliminan de VIE
			delete from Inventory.POSPathologies where MedicamentId = @Id

			--Se eliminan de Crystal
			delete from .INPRODPAT where IPRCODIGO = @Code
		end

		--VIAS DE ADMINISTRACION
		--Si hay para guardar
			if(select count(*) from @TableATCAR where Id = 0 and IsDelete = 0) > 0
			begin
				--Se guarda en VIE
				insert into Inventory.ATCAdministrationRoute(ATCId, AdministrationRouteId)
				select @Id, ARId
				from @TableATCAR
				where Id = 0 and IsDelete = 0

				--Se guarda en Crystal
			end

			--Si hay vias de administracion para eliminar
			if(select count(*) from @TableATCAR where IsDelete = 1) > 0
			begin
				--Se eliminan de VIE
				delete from Inventory.ATCAdministrationRoute where Id in (select Id from @TableATCAR where IsDelete = 1)

				--Se eliminan de Crystal
				
			end
		
		--elementos asociados
		if @HasSupplieMedicine = 1
		begin
			--Si hay elementos para guardar
			if(select count(1) from @TableRSM where Id = 0 and IsDelete = 0) > 0
			begin
				--Se guarda en VIE
				insert into Inventory.RelatedSupplieMedicine(ATCId, ItemType, SourceId)
				select @Id, ItemType, SourceId
				from @TableRSM
				where Id = 0 and IsDelete = 0

				
				--Se guarda en Crystal
				insert into .IHPROINSU (CODPRODUC,CODINSUMO,DESPRODUC,CANTIDAD,DEFECTO)
				SELECT @Code, SourceCode, SourceDescription, 1, 0
				FROM @TableRSM
				where Id = 0 and IsDelete = 0
			end

			--Si hay elementos para eliminar
			if(select count(1) from @TableRSM where IsDelete = 1) > 0
			begin
				--Se eliminan de VIE
				delete from Inventory.RelatedSupplieMedicine where Id in (select Id from @TableRSM where IsDelete = 1)

				--Se eliminan de Crystal
				DELETE FROM .IHPROINSU WHERE CODPRODUC = @Code AND CODINSUMO IN (SELECT RSM.SourceCode from @TableRSM RSM where IsDelete = 1)
			end
		end
		else begin --Si el medicamento no tiene elementos asociados, se eliminan
			--Se eliminan de VIE
			delete from Inventory.RelatedSupplieMedicine where ATCId = @Id

			--Se eliminan de Crystal
			DELETE FROM .IHPROINSU WHERE CODPRODUC = @Code
		end

		--Retorna el ok
		select 0 as CodeResult, 'Se guardó correctamente' as MessageResult, @Code as Code
		return
	end try
	begin catch
		--Retorna el error
		select 999 as CodeResult, CONCAT('Linea: ', ERROR_LINE(), ' - ', ERROR_MESSAGE()) as MessageResult, '' as Code
		return
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que crea o actualiza un medicamento (llamado ATC) en el sistema de inventario farmacéutico. Recibe toda la información del medicamento en formato XML: datos generales del fármaco (código, nombre, DCI, grupo farmacológico, vía de administración, concentración, peso, volumen, presentaciones, niveles de riesgo, estabilidad), indicadores clínicos (antibiótico, alto costo, requiere junta médica, producto POS, NPT, diluente, insumo, etc.), textos clínicos enriquecidos (advertencias, dosificación, indicaciones, contraindicaciones, precauciones, reacciones adversas), y colecciones relacionadas como rutas de administración (ATCAR), concentraciones por DCI, referencias a servicios/medicamentos relacionados (RSM), fichas técnicas por diagnóstico (TechnicalSheet) y datos clínicos de control (ClinicalData con laboratorios, frecuencia y diagnóstico). Sincroniza el registro tanto en la base VIE como en Crystal, validando la consistencia de datos maestros (DCI, grupo farmacológico, forma farmacéutica, unidades de medida) y acumulando errores de validación antes de confirmar o revertir la transacción.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveATC';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveATC';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (crea o actualiza) un medicamento ATC y todas sus entidades relacionadas (patologías POS, vías de administración, concentraciones por DCI, datos clínicos, fichas técnicas, insumos asociados) replicando los cambios tanto en la base VIE (Inventory) como en el sistema legado Crystal (IHLISTPRO, INPRODPAT, IHPROINSU).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveATC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir la estructura /ATC con secciones ProductPathologies, ATCAR, ATCConcentrationByDCI, RSM, TechnicalSheet y ATCClinicalData.; Los códigos referenciados (DCI, grupo farmacológico, vías de administración, forma farmacéutica, unidades de medida según FormulationType, nivel de riesgo) deben existir previamente en las tablas Crystal (IHDCIMEDI, IHGRUFARM, HCVIAADMI, IHFORMEDI, INUNIMEDI, INIVERIES); en caso contrario se aborta con CodeResult=999.; Si ItemType=1 (insumo) o ItemType=2 (medicamento) en RSM, los códigos deben existir en .IHLISTPRO de Crystal.; Para FormulationType=1 (peso) se requiere WeightMeasureUnit válido; para 2 (volumen) VolumeMeasureUnit; para 3 ambos; para 4 AdministrationUnitId.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveATC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveATC';
-- GO
