-- ===============================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 22/04/2021
-- Description:	Procedimiento que se encarga de guardar los paquetes maestros y paquetes personalizados en sus tablas respectivas
-- ===============================================================================================================================

CREATE PROCEDURE [MixingStation].[SP_SavePackage]
	@Xml xml,
	@UserCode VARCHAR(20)
AS
BEGIN
	
	--Variables para obtener la cabecera
	DECLARE @Id INT, @Code VARCHAR(20), @Name VARCHAR(200), @Description VARCHAR(max), @RiskLevelId INT, @CodeAlternative VARCHAR(30), 
	@CodeAlternativeTwo VARCHAR(20), @ProductGroupId INT, @ProductSubGroupId INT, @ManufacturerId INT, @CodeSICE VARCHAR(20), @POSProduct BIT, @BillingGroupId INT, 
	@ProductControl BIT, @ProductWithPriceControl BIT, @AuthorizationByOrderNumber INT, @MaximumControlPeriod BIT, @OsmolarityTotal DECIMAL(6,4), 
	@VolumeTotalOrder DECIMAL(8,2), @VolumeTotalOrderMeasurementUnitId INT, @VolumeTotalOrderPurga DECIMAL(8,2), @WeightTotalSolution DECIMAL(8,2), @State BIT,
	@MeasurementUnitId INT, @StabilityHour TIME, @EnvironmentalTemperatureTerm INT, @Purge DECIMAL(5,2), @InfusionSpeed INT, @PreparationInstructions VARCHAR(max),
	@SpecialConsiderations VARCHAR(max), @UnitDoseTypeId INT, @Concentration DECIMAL(18,4), @ConcentrationMeasurementUnitId INT, @ProductId INT, @Justification nvarchar(400), 
	@StandardMix BIT, @PhotoProtection BIT, @PreparationType TINYINT, @PersonalizedMasterPreparation BIT, @AssociatedPackageId INT, @LabelType TINYINT,
	@OperatingUnitId INT, @IsPackagePersonalized BIT, @VehicleOptimization BIT, @Storage INT, @Readjustments TINYINT, @ConcentrationAntibiotic VARCHAR(50),
	@VolumeTotalPrepared DECIMAL(18,4), @MeasurementPreparedId INT, @NptId INT, @MainDrugId INT, @StabilityDays INT, @TypeStability TINYINT

	DECLARE @IdTmp INT, @CodeTmp VARCHAR(20), @MsClass INT

	--Tabla de detalles 
	DECLARE @PackageDetail MixingStation.PackageDetailType
	
	BEGIN TRY
		
		--Se obtienen los datos para la cabecera
		SELECT 
			@Id = t.x.value('Id[1]','INT'),
			@Code = t.x.value('Code[1]','VARCHAR(20)'),
			@Name = t.x.value('Name[1]','VARCHAR(100)'),
			@Description = IIF(t.x.value('Description[1]','VARCHAR(max)') = '', null, t.x.value('Description[1]','VARCHAR(max)')),
			@RiskLevelId = IIF(t.x.value('RiskLevelId[1]','VARCHAR(20)') = '', null, t.x.value('RiskLevelId[1]','VARCHAR(20)')),
			@CodeAlternative = IIF(t.x.value('CodeAlternative[1]','VARCHAR(30)') = '', null, t.x.value('CodeAlternative[1]','VARCHAR(30)')),
			@CodeAlternativeTwo = IIF(t.x.value('CodeAlternativeTwo[1]','VARCHAR(20)') = '', null, t.x.value('CodeAlternativeTwo[1]','VARCHAR(20)')),
			@ProductGroupId = IIF(t.x.value('ProductGroupId[1]','VARCHAR(20)') = '', null, t.x.value('ProductGroupId[1]','VARCHAR(20)')),
			@ProductSubGroupId = IIF(t.x.value('ProductSubGroupId[1]','VARCHAR(20)') = '', null, t.x.value('ProductSubGroupId[1]','VARCHAR(20)')),
			@ManufacturerId = IIF(t.x.value('ManufacturerId[1]','VARCHAR(20)') = '', null, t.x.value('ManufacturerId[1]','VARCHAR(20)')),
			@CodeSICE = IIF(t.x.value('CodeSICE[1]','VARCHAR(20)') = '', null, t.x.value('CodeSICE[1]','VARCHAR(20)')),
			@POSProduct = IIF(t.x.value('POSProduct[1]','VARCHAR(20)') = '', null, t.x.value('POSProduct[1]','VARCHAR(20)')),
			@BillingGroupId = IIF(t.x.value('BillingGroupId[1]','VARCHAR(20)') = '', null, t.x.value('BillingGroupId[1]','VARCHAR(20)')),
			@ProductControl = IIF(t.x.value('ProductControl[1]','VARCHAR(20)') = '', null, t.x.value('ProductControl[1]','VARCHAR(20)')),
			@ProductWithPriceControl = IIF(t.x.value('ProductWithPriceControl[1]','VARCHAR(20)') = '', null, t.x.value('ProductWithPriceControl[1]','VARCHAR(20)')),
			@AuthorizationByOrderNumber = IIF(t.x.value('AuthorizationByOrderNumber[1]','VARCHAR(20)') = '', null, t.x.value('AuthorizationByOrderNumber[1]','VARCHAR(20)')),
			@MaximumControlPeriod = IIF(t.x.value('MaximumControlPeriod[1]','VARCHAR(20)') = '', null, t.x.value('@MaximumControlPeriod[1]','VARCHAR(20)')),
			@OsmolarityTotal = IIF(t.x.value('OsmolarityTotal[1]','VARCHAR(20)') = '', null, REPLACE(t.x.value('OsmolarityTotal[1]','VARCHAR(20)'), ',', '.')),
			@VolumeTotalOrder = IIF(t.x.value('VolumeTotalOrder[1]','VARCHAR(20)') = '', null, REPLACE(t.x.value('VolumeTotalOrder[1]','VARCHAR(20)'), ',', '.')),
			@VolumeTotalOrderMeasurementUnitId = IIF(t.x.value('VolumeTotalOrderMeasurementUnitId[1]','VARCHAR(20)') = '', null, t.x.value('VolumeTotalOrderMeasurementUnitId[1]','VARCHAR(20)')),
			@VolumeTotalOrderPurga = IIF(t.x.value('VolumeTotalOrderPurga[1]','VARCHAR(20)') = '', null, REPLACE(t.x.value('VolumeTotalOrderPurga[1]','VARCHAR(20)'), ',', '.')),
			@WeightTotalSolution = IIF(t.x.value('WeightTotalSolution[1]','VARCHAR(20)') = '', null, REPLACE(t.x.value('WeightTotalSolution[1]','VARCHAR(20)'), ',', '.')),
			@State = t.x.value('State[1]','BIT'),
			@MeasurementUnitId = IIF(t.x.value('MeasurementUnitId[1]','VARCHAR(20)') = '', null, t.x.value('MeasurementUnitId[1]','VARCHAR(20)')),
			@StabilityHour = t.x.value('StabilityHour[1]','TIME'),
			@EnvironmentalTemperatureTerm = t.x.value('EnvironmentalTemperatureTerm[1]','INT'),
			@Purge = REPLACE(t.x.value('Purge[1]','VARCHAR(20)'), ',', '.'),
			@InfusionSpeed = IIF(t.x.value('InfusionSpeed[1]','VARCHAR(20)') = '', null, t.x.value('InfusionSpeed[1]','VARCHAR(20)')),
			@PreparationInstructions = IIF(t.x.value('PreparationInstructions[1]','VARCHAR(max)') = '', null, t.x.value('PreparationInstructions[1]','VARCHAR(max)')),
			@SpecialConsiderations = IIF(t.x.value('SpecialConsiderations[1]','VARCHAR(max)') = '', null, t.x.value('SpecialConsiderations[1]','VARCHAR(max)')),
			@UnitDoseTypeId = t.x.value('UnitDoseTypeId[1]','INT'),
			@Concentration = IIF(t.x.value('Concentration[1]','VARCHAR(20)') = '', null,REPLACE(t.x.value('Concentration[1]','VARCHAR(20)'), ',', '.')),
			@ConcentrationMeasurementUnitId = IIF(t.x.value('ConcentrationMeasurementUnitId[1]','VARCHAR(20)') = '', null, t.x.value('ConcentrationMeasurementUnitId[1]','VARCHAR(20)')),
			@ProductId = IIF(t.x.value('ProductId[1]','VARCHAR(20)') = '', null, t.x.value('ProductId[1]','VARCHAR(20)')),
			@Justification = IIF(t.x.value('Justification[1]','VARCHAR(400)') = '', null, t.x.value('Justification[1]','VARCHAR(400)')),
			@StandardMix = IIF(t.x.value('StandardMix[1]','VARCHAR(20)') = '', null, t.x.value('StandardMix[1]','VARCHAR(20)')),
			@PhotoProtection = t.x.value('PhotoProtection[1]','BIT'),
			@PreparationType = IIF(t.x.value('PreparationType[1]','VARCHAR(20)') = '', null, t.x.value('PreparationType[1]','VARCHAR(20)')),
			@PersonalizedMasterPreparation = t.x.value('PersonalizedMasterPreparation[1]','BIT'),
			@AssociatedPackageId = IIF(t.x.value('AssociatedPackageId[1]','VARCHAR(20)') = '', null, t.x.value('AssociatedPackageId[1]','VARCHAR(20)')),
			@LabelType = IIF(t.x.value('LabelType[1]','VARCHAR(20)') = '', null, t.x.value('LabelType[1]','VARCHAR(20)')),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','INT'),
			@IsPackagePersonalized = t.x.value('IsPackagePersonalized[1]','BIT'),
			@VehicleOptimization = t.x.value('VehicleOptimization[1]','BIT'),
			@Storage = IIF(t.x.value('Storage[1]','VARCHAR(20)') = '', null, t.x.value('Storage[1]','VARCHAR(20)')),
			@Readjustments = IIF(t.x.value('Readjustments[1]','VARCHAR(20)') = '', null, t.x.value('Readjustments[1]','VARCHAR(20)')),
			@ConcentrationAntibiotic = IIF(t.x.value('ConcentrationAntibiotic[1]','VARCHAR(20)') = '', null, t.x.value('ConcentrationAntibiotic[1]','VARCHAR(20)')),
			@VolumeTotalPrepared = IIF(t.x.value('VolumeTotalPrepared[1]','VARCHAR(20)') = '', null,REPLACE(t.x.value('VolumeTotalPrepared[1]','VARCHAR(20)'), ',', '.')),
			@MeasurementPreparedId = IIF(t.x.value('MeasurementPreparedId[1]','VARCHAR(20)') = '', null, t.x.value('MeasurementPreparedId[1]','VARCHAR(20)')),
			@NptId = IIF(t.x.value('NptId[1]','VARCHAR(20)') = '', null, t.x.value('NptId[1]','VARCHAR(20)')),
			@MainDrugId = t.x.value('MainDrugId[1]','INT'),
			@StabilityDays = t.x.value('StabilityDays[1]','INT'),
			@TypeStability = IIF(t.x.value('TypeStability[1]','VARCHAR(20)') = '', null, t.x.value('TypeStability[1]','VARCHAR(20)'))
		FROM @Xml.nodes('/Data') t(x)

		--Se obtienen los detalles del xml
		INSERT INTO @PackageDetail
		SELECT 
			t.x.value('Id[1]','INT') as Id,
			t.x.value('PackageId[1]','INT') as PackageId,
			IIF(t.x.value('ProductId[1]','VARCHAR(20)') = '', null, t.x.value('ProductId[1]','VARCHAR(20)')) as ProductId,
			IIF(t.x.value('Quantity[1]','VARCHAR(20)') = '', null, REPLACE(t.x.value('Quantity[1]','VARCHAR(20)'), ',', '.')) as Quantity,
			IIF(t.x.value('MeasurementUnitId[1]','VARCHAR(20)') = '', null, t.x.value('MeasurementUnitId[1]','VARCHAR(20)')) as MeasurementUnitId,
			IIF(t.x.value('Volume[1]','VARCHAR(20)') = '', null, REPLACE(t.x.value('Volume[1]','VARCHAR(20)'), ',', '.')) as Volume,
			IIF(t.x.value('VolumeMeasureUnit[1]','VARCHAR(20)') = '', null, t.x.value('VolumeMeasureUnit[1]','VARCHAR(20)')) as VolumeMeasureUnit,
			t.x.value('Thinner[1]','BIT') as Thinner,
			t.x.value('Vehicle[1]','BIT') as Vehicle,
			IIF(t.x.value('Osmolarity[1]','VARCHAR(20)') = '', null, cast(REPLACE(t.x.value('Osmolarity[1]','VARCHAR(10)'), ',', '.') as DECIMAL(5, 1))),
			REPLACE(t.x.value('Density[1]','VARCHAR(20)'), ',', '.') as Density,
			IIF(t.x.value('AtcId[1]','VARCHAR(20)') = '', null, t.x.value('AtcId[1]','VARCHAR(20)')) as AtcId,
			IIF(t.x.value('SupplieId[1]','VARCHAR(20)') = '', null, t.x.value('SupplieId[1]','VARCHAR(20)')) as SupplieId,
			IIF(t.x.value('ComponentType[1]','VARCHAR(20)') = '', null, t.x.value('ComponentType[1]','VARCHAR(20)')) as ComponentType,
			IIF(t.x.value('MainMedicine[1]','VARCHAR(20)') = '', null, t.x.value('MainMedicine[1]','VARCHAR(20)')) as MainMedicine,
			IIF(t.x.value('PreparationType[1]','VARCHAR(20)') = '', null, t.x.value('PreparationType[1]','VARCHAR(20)')) as PreparationType,
			IIF(t.x.value('Dilution[1]','VARCHAR(20)') = '', null, REPLACE(t.x.value('Dilution[1]','VARCHAR(20)'), ',', '.')) as Dilution,
			IIF(t.x.value('Concentration[1]','VARCHAR(20)') = '', null, REPLACE(t.x.value('Concentration[1]','VARCHAR(20)'), ',', '.')) as Concentration,
			IIF(t.x.value('AmountTime[1]','VARCHAR(20)') = '', null, t.x.value('AmountTime[1]','VARCHAR(20)')) as AmountTime,
			IIF(t.x.value('TimeUnit[1]','VARCHAR(20)') = '', null, t.x.value('TimeUnit[1]','VARCHAR(20)')) as TimeUnit,
			IIF(t.x.value('VolumeTotal[1]','VARCHAR(20)') = '', null, REPLACE(t.x.value('VolumeTotal[1]','VARCHAR(20)'), ',', '.')) as VolumeTotal,
			IIF(t.x.value('NPTItemOrder[1]','VARCHAR(20)') = '', null, t.x.value('NPTItemOrder[1]','VARCHAR(20)')) as NPTItemOrder,
			IIF(t.x.value('ComplementaryMedicine[1]','VARCHAR(20)') = '', null, t.x.value('ComplementaryMedicine[1]','BIT')) as ComplementaryMedicine,
			t.x.value('IsDelete[1]','BIT') as IsDelete
		FROM @Xml.nodes('/Data/Details') t(x)

		-- Verificamos si es un paquete nuevo o es una actualizacion 
		DECLARE @UpdatePackage BIT
		IF @Id > 0 
			SET @UpdatePackage = 1
		ELSE
			SET @UpdatePackage = 0		

		SET @IdTmp = NULL
		SET @CodeTmp = NULL

		SELECT @MsClass = udt.MSClass
		FROM MixingStation.UnitDoseType udt
		WHERE udt.Id = @UnitDoseTypeId

		--Se valida que los detalles a eliminar no estén parametrizados en una tarifa de productos
		IF @IsPackagePersonalized = 0
		BEGIN
			DECLARE @TarifasConFlictivas VARCHAR(MAX)
			SELECT @TarifasConFlictivas = STRING_AGG(Code_Name, ', ')
			FROM (
				SELECT DISTINCT pr.Code + ' - ' + pr.Name AS Code_Name
				FROM @PackageDetail pd
				INNER JOIN Inventory.ProductRateDetailPackage prdp ON prdp.PackageDetailId = pd.Id
				INNER JOIN Inventory.ProductRateDetail prd ON prd.Id = prdp.ProductRateDetailId
				INNER JOIN Inventory.ProductRate pr ON pr.Id = prd.ProductRateId
				WHERE pd.IsDelete = 1
			) AS sub

			IF @TarifasConFlictivas IS NOT NULL BEGIN
				SELECT 999 AS CodeMessage,
					'El paquete está parametrizado en tarifa de productos, para modificarlo debe retirarlo de dicha parametrización.' AS Message,
					0 Id, '' Code
				RETURN
			END
		END

		--Se eliminan los detalles
		IF @IsPackagePersonalized = 0
		BEGIN
			DELETE FROM MixingStation.PackageDetail WHERE Id in (SELECT Id FROM @PackageDetail WHERE IsDelete = 1)
		END
		ELSE BEGIN
			DELETE FROM MixingStation.PackagePersonalizedDetail WHERE Id in (SELECT Id FROM @PackageDetail WHERE IsDelete = 1)
		END

		DELETE FROM @PackageDetail WHERE IsDelete = 1

		--Permite saber que id de formulario escoger para el tema de secuencia numerica
		DECLARE @FormId VARCHAR(10) = ''

		IF @IsPackagePersonalized = 0 --Si se guarda el maestro de paquetes
		BEGIN
			SET @FormId = '2065'
		END
		ELSE BEGIN --Si se guarda el personalizado
			SET @FormId = '2239'
		END

		IF EXISTS(SELECT * FROM @PackageDetail WHERE Thinner = 1 AND Vehicle = 1) BEGIN
			SELECT 999 as CodeMessage, 'Existen detalles(Medicamentos) que son diluyente y vehiculo al mismo tiempo' as Message, 0 Id, '' Code
			RETURN
		END

		--IF EXISTS(SELECT p.Id, COUNT(*) Quantity
		--			FROM MixingStation.Package p
		--			RIGHT JOIN MixingStation.RequestMixingStationDetail rmsd ON p.Id = rmsd.PackageId
		--			WHERE p.PersonalizedMasterPreparation = 0  AND rmsd.PackageId = @Id
		--			GROUP BY p.Id
		--) BEGIN
		--	SELECT 999 as CodeMessage, 'El paquete no se puede modificar, ya fue asignado a un proceso de producción.' AS MESSAGE, 0 Id, '' Code
		--	RETURN
		--END

		---------------------------------------------------------------------------------------------------------------

		-- Si es nuevo y ya existe un paquete personalizado igual, retornamos el existente
		IF @UpdatePackage = 0 AND @IsPackagePersonalized = 1
		BEGIN
			SELECT TOP 1 @IdTmp = pp.Id, @CodeTmp = pp.Code
			FROM MixingStation.GetExistingPackagePersonalizedDetail(@AssociatedPackageId, @UnitDoseTypeId, @PackageDetail) pp

			IF @IdTmp IS NOT NULL BEGIN
				SELECT TOP 1 
					0 as CodeMessage, 'Se encontró el paquete con código ' + @CodeTmp as Message, @IdTmp Id, @CodeTmp Code
				
				RETURN
			END
		END
		ELSE IF ISNULL(@IsPackagePersonalized, 0) = 0 
		BEGIN
			SELECT TOP 1 @IdTmp = pp.Id, @CodeTmp = pp.Code
			FROM MixingStation.GetExistingPackageDetail(@UnitDoseTypeId, @PackageDetail) pp
			WHERE pp.Id <> @Id

			IF @IdTmp IS NOT NULL BEGIN
				SELECT TOP 1 
					999 as CodeMessage, CONCAT('El paquete ', @CodeTmp,' contiene los mismos componentes que el paquete a guardar') as Message, @IdTmp Id, @CodeTmp Code
				
				RETURN
			END
		END

		IF ISNULL(@Code, '') = ''
		BEGIN
			--Consultamos si la secuencia es con O o OU
			DECLARE @scope VARCHAR(5) = ''
			DECLARE @idSequenceDetail INT
			DECLARE @pattern VARCHAR(300)
			DECLARE @NextS INT
			SELECT @scope = Scope FROM MixingStation.MixingStationSequence
			WHERE IdForm = @FormId

			IF @scope = 'O' --Si el ambito es por organización
			BEGIN
				SELECT top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
				FROM MixingStation.MixingStationSequenceDetail bsd 
				INNER JOIN MixingStation.MixingStationSequence bs on bs.Id = bsd.IdSequenseMixingStationC
				INNER JOIN Common.Sequense cs on cs.Id = bsd.IdSequense
				WHERE bs.IdForm = @FormId
				ORDER BY bsd.Next DESC
			END
			ELSE BEGIN --Si el ambito es por unidad operativa
				SELECT top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
				FROM MixingStation.MixingStationSequenceDetail bsd 
				INNER JOIN MixingStation.MixingStationSequence bs on bs.Id = bsd.IdSequenseMixingStationC
				INNER JOIN Common.Sequense cs on cs.Id = bsd.IdSequense
				WHERE bs.IdForm = @FormId and bsd.IdOperatingUnit = @OperatingUnitId
				ORDER BY bsd.Next DESC
			END
					
			IF (@idSequenceDetail is null)
			BEGIN
				SELECT 999 as CodeMessage, 'Secuencia no encontrada para generar el paquete' + iif(@FormId = '2239', ' personalizado', '') as Message, 0 Id, '' Code
				return
			END

			SELECT @Code = dbo.GetSequence('',@pattern,@NextS)
			UPDATE MixingStation.MixingStationSequenceDetail SET [Next] += 1 WHERE Id = @idSequenceDetail
		END

		IF ISNULL(@Id, 0) = 0
		BEGIN
			IF @IsPackagePersonalized = 0 --Se inserta la cabecera para el paquete maestro
			BEGIN 
				INSERT INTO [MixingStation].[Package]([Code],[Name],[Description],[RiskLevelId],[CodeAlternative],[CodeAlternativeTwo],[ProductGroupId],[ProductSubGroupId],
				[ManufacturerId],[CodeSICE],[POSProduct],[BillingGroupId],[ProductControl],[ProductWithPriceControl],[AuthorizationByOrderNumber],[MaximumControlPeriod],[OsmolarityTotal],
				[VolumeTotalOrder],[VolumeTotalOrderMeasurementUnitId],[VolumeTotalOrderPurga],[WeightTotalSolution],[CreationUser],[CreationDate],[State],[MeasurementUnitId],[StabilityHour],[RefrigeratedTerm],
				[EnvironmentalTemperatureTerm],[Purge],[InfusionSpeed],[PreparationInstructions],[SpecialConsiderations],[UnitDoseTypeId],[Concentration],[ConcentrationMeasurementUnitId],
				[ProductId],[Justification],[StandardMix],[PhotoProtection],[PreparationType],[PersonalizedMasterPreparation],[AssociatedPackageId],[LabelType],
				[VehicleOptimization],[Storage], [Readjustments], [ConcentrationAntibiotic], [VolumeTotalPrepared], [MeasurementPreparedId], [NptId], [MainDrugId], [StabilityDays], [TypeStability])
				VALUES(@Code, @Name, @Description, @RiskLevelId, @CodeAlternative, @CodeAlternativeTwo, @ProductGroupId, @ProductSubGroupId, @ManufacturerId, @CodeSICE,
				@POSProduct, @BillingGroupId, @ProductControl, @ProductWithPriceControl, @AuthorizationByOrderNumber, @MaximumControlPeriod, @OsmolarityTotal, @VolumeTotalOrder,
				@VolumeTotalOrderMeasurementUnitId, @VolumeTotalOrderPurga, @WeightTotalSolution, @UserCode, Common.GETDATE(), @State, @MeasurementUnitId, @StabilityHour, 0,
				@EnvironmentalTemperatureTerm, @Purge, @InfusionSpeed, @PreparationInstructions, @SpecialConsiderations, @UnitDoseTypeId, @Concentration, @ConcentrationMeasurementUnitId, 
				@ProductId, @Justification, @StandardMix, @PhotoProtection, @PreparationType, @PersonalizedMasterPreparation, @AssociatedPackageId, @LabelType,
				@VehicleOptimization, @Storage, @Readjustments, @ConcentrationAntibiotic, @VolumeTotalPrepared, @MeasurementPreparedId, @NptId, @MainDrugId, @StabilityDays, @TypeStability)
			END
			ELSE BEGIN --Se inserta la cabecera para el paquete personalizado
				INSERT INTO [MixingStation].[PackagePersonalized]([Code],[Name],[Description],[RiskLevelId],[CodeAlternative],[CodeAlternativeTwo],[ProductGroupId],[ProductSubGroupId],
				[ManufacturerId],[CodeSICE],[POSProduct],[BillingGroupId],[ProductControl],[ProductWithPriceControl],[AuthorizationByOrderNumber],[MaximumControlPeriod],[OsmolarityTotal],
				[VolumeTotalOrder],[VolumeTotalOrderMeasurementUnitId],[VolumeTotalOrderPurga],[WeightTotalSolution],[CreationUser],[CreationDate],[State],[MeasurementUnitId], [RefrigeratedTerm],
				[EnvironmentalTemperatureTerm],[Purge],[InfusionSpeed],[PreparationInstructions],[SpecialConsiderations],[UnitDoseTypeId],[Concentration],[ConcentrationMeasurementUnitId],
				[ATCId],[Justification],[StandardMix],[PhotoProtection],[PreparationType],[PersonalizedMasterPreparation],[AssociatedPackageId],[LabelType],
				[Storage], [ConcentrationAntibiotic], [VolumeTotalPrepared], [MeasurementPreparedId], [MainDrugId])
				VALUES(@Code, @Name, @Description, @RiskLevelId, @CodeAlternative, @CodeAlternativeTwo, @ProductGroupId, @ProductSubGroupId, @ManufacturerId, @CodeSICE,
				@POSProduct, @BillingGroupId, @ProductControl, @ProductWithPriceControl, @AuthorizationByOrderNumber, @MaximumControlPeriod, @OsmolarityTotal, @VolumeTotalOrder,
				@VolumeTotalOrderMeasurementUnitId, @VolumeTotalOrderPurga, @WeightTotalSolution, @UserCode, Common.GETDATE(), @State, @MeasurementUnitId, 0, 
				@EnvironmentalTemperatureTerm, @Purge, @InfusionSpeed, @PreparationInstructions, @SpecialConsiderations, @UnitDoseTypeId, @Concentration, @ConcentrationMeasurementUnitId, 
				@ProductId, @Justification, @StandardMix, @PhotoProtection, @PreparationType, @PersonalizedMasterPreparation, @AssociatedPackageId, @LabelType,
				@Storage, @ConcentrationAntibiotic, @VolumeTotalPrepared, @MeasurementPreparedId, @MainDrugId)
			END

			SET @Id = SCOPE_IDENTITY()
		END
		ELSE BEGIN 
			IF @IsPackagePersonalized = 0 --Se actualiza la cabecera para el paquete maestro
			BEGIN
				UPDATE [MixingStation].[Package] SET [Code] = @Code, [Name] = @Name, [Description] = @Description, [RiskLevelId] = @RiskLevelId,
				[CodeAlternative] = @CodeAlternative, [CodeAlternativeTwo] = @CodeAlternativeTwo, [ProductGroupId] = @ProductGroupId, [ProductSubGroupId] = @ProductSubGroupId,
				[ManufacturerId] = @ManufacturerId, [CodeSICE] = @CodeSICE, [POSProduct] = @POSProduct, [BillingGroupId] = @BillingGroupId, [ProductControl] = @ProductControl,
				[ProductWithPriceControl] = @ProductWithPriceControl, [AuthorizationByOrderNumber] = @AuthorizationByOrderNumber, [MaximumControlPeriod] = @MaximumControlPeriod, 
				[OsmolarityTotal] = @OsmolarityTotal, [VolumeTotalOrder] = @VolumeTotalOrder, [VolumeTotalOrderMeasurementUnitId] = @VolumeTotalOrderMeasurementUnitId, 
				[VolumeTotalOrderPurga] = @VolumeTotalOrderPurga, [WeightTotalSolution] = @WeightTotalSolution, [ModificationUser] = @UserCode, [ModificationDate] = Common.GetDate(),
				[State] = @State, [MeasurementUnitId] = @MeasurementUnitId, [StabilityHour] = @StabilityHour, [RefrigeratedTerm] = 0, [EnvironmentalTemperatureTerm] = @EnvironmentalTemperatureTerm,
				[Purge] = @Purge, [InfusionSpeed] = @InfusionSpeed, [PreparationInstructions] = @PreparationInstructions, [SpecialConsiderations] = @SpecialConsiderations, 
				[UnitDoseTypeId] = @UnitDoseTypeId, [Concentration] = @Concentration, [ConcentrationMeasurementUnitId] = @ConcentrationMeasurementUnitId, [ProductId] = @ProductId, 
				[Justification] = @Justification, [StandardMix] = @StandardMix, [PhotoProtection] = @PhotoProtection, [PreparationType] = @PreparationType, 
				[PersonalizedMasterPreparation] = @PersonalizedMasterPreparation, [AssociatedPackageId] = @AssociatedPackageId, [LabelType] = @LabelType,
				[VehicleOptimization] = @VehicleOptimization, [Storage] = @Storage, [Readjustments] = @Readjustments, [ConcentrationAntibiotic] = @ConcentrationAntibiotic,
				[VolumeTotalPrepared] = @VolumeTotalPrepared, [MeasurementPreparedId] = @MeasurementPreparedId, [NptId] = @NptId, [MainDrugId] = @MainDrugId, 
				[StabilityDays] = @StabilityDays, [TypeStability] = @TypeStability
				WHERE Id = @Id
			END
			ELSE BEGIN
				UPDATE [MixingStation].[PackagePersonalized] SET [Code] = @Code, [Name] = @Name, [Description] = @Description, [RiskLevelId] = @RiskLevelId,
				[CodeAlternative] = @CodeAlternative, [CodeAlternativeTwo] = @CodeAlternativeTwo, [ProductGroupId] = @ProductGroupId, [ProductSubGroupId] = @ProductSubGroupId,
				[ManufacturerId] = @ManufacturerId, [CodeSICE] = @CodeSICE, [POSProduct] = @POSProduct, [BillingGroupId] = @BillingGroupId, [ProductControl] = @ProductControl,
				[ProductWithPriceControl] = @ProductWithPriceControl, [AuthorizationByOrderNumber] = @AuthorizationByOrderNumber, [MaximumControlPeriod] = @MaximumControlPeriod, 
				[OsmolarityTotal] = @OsmolarityTotal, [VolumeTotalOrder] = @VolumeTotalOrder, [VolumeTotalOrderMeasurementUnitId] = @VolumeTotalOrderMeasurementUnitId, 
				[VolumeTotalOrderPurga] = @VolumeTotalOrderPurga, [WeightTotalSolution] = @WeightTotalSolution, [ModificationUser] = @UserCode, [ModificationDate] = Common.GetDate(),
				[State] = @State, [MeasurementUnitId] = @MeasurementUnitId, [RefrigeratedTerm] = 0, [EnvironmentalTemperatureTerm] = @EnvironmentalTemperatureTerm,
				[Purge] = @Purge, [InfusionSpeed] = @InfusionSpeed, [PreparationInstructions] = @PreparationInstructions, [SpecialConsiderations] = @SpecialConsiderations, 
				[UnitDoseTypeId] = @UnitDoseTypeId, [Concentration] = @Concentration, [ConcentrationMeasurementUnitId] = @ConcentrationMeasurementUnitId, [ATCId] = @ProductId, 
				[Justification] = @Justification, [StandardMix] = @StandardMix, [PhotoProtection] = @PhotoProtection, [PreparationType] = @PreparationType, 
				[PersonalizedMasterPreparation] = @PersonalizedMasterPreparation, [AssociatedPackageId] = @AssociatedPackageId, [LabelType] = @LabelType,
				[Storage] = @Storage, [ConcentrationAntibiotic] = @ConcentrationAntibiotic, [VolumeTotalPrepared] = @VolumeTotalPrepared, [MeasurementPreparedId]= @MeasurementPreparedId, [MainDrugId] = @MainDrugId
				WHERE Id = @Id
			END
		END
		
		--Se insertan los nuevos detalles
		IF @IsPackagePersonalized = 0
		BEGIN
			INSERT INTO [MixingStation].[PackageDetail]([PackageId],[ProductId],[Quantity],[MeasurementUnitId],[Volume],[VolumeMeasureUnit],[Thinner],[Vehicle],
			[CreationUser],[CreationDate],[Osmolarity],[Density],[AtcId],[SupplieId],[ComponentType],[MainMedicine],[PreparationType],[Dilution], 
			[Concentration], [AmountTime],[TimeUnit],[VolumeTotal],[NPTItemOrder], [ComplementaryMedicine])
			SELECT @Id,ProductId,Quantity,MeasurementUnitId,Volume,VolumeMeasureUnit,Thinner,Vehicle,
			@UserCode,Common.GETDATE(),Osmolarity,Density,AtcId,SupplieId,ComponentType,MainMedicine,PreparationType,Dilution,Concentration,
			AmountTime,TimeUnit,VolumeTotal, NPTItemOrder, ComplementaryMedicine
			FROM @PackageDetail
			WHERE Id = 0
		END
		ELSE BEGIN
			INSERT INTO [MixingStation].[PackagePersonalizedDetail]([PackagePersonalizedId],[ProductId],[Quantity],[MeasurementUnitId],[Volume],[VolumeMeasureUnit],[Thinner],
			[Vehicle],[CreationUser],[CreationDate],[Osmolarity],[Density],[AtcId],[SupplieId],[ComponentType],[MainMedicine],[PreparationType],[Dilution], 
			[Concentration],[AmountTime],[TimeUnit],[VolumeTotal],[NPTItemOrder],[ComplementaryMedicine])
			SELECT @Id,ProductId,Quantity,MeasurementUnitId,Volume,VolumeMeasureUnit,Thinner,Vehicle,
			@UserCode,Common.GETDATE(),Osmolarity,Density,AtcId,SupplieId,ComponentType,MainMedicine,PreparationType,Dilution,Concentration,
			AmountTime,TimeUnit,VolumeTotal,NPTItemOrder,ComplementaryMedicine
			FROM @PackageDetail
			WHERE Id = 0
		END
				
		--Se actualizan los detalles
		IF @IsPackagePersonalized = 0
		BEGIN
			UPDATE pd SET pd.[PackageId] = pdt.PackageId, pd.[ProductId] = pdt.ProductId, pd.[Quantity] = pdt.Quantity, pd.[MeasurementUnitId] = pdt.MeasurementUnitId,
			pd.[Volume] = pdt.Volume, pd.[VolumeMeasureUnit] = pdt.VolumeMeasureUnit, pd.[Thinner] = pdt.Thinner, pd.[Vehicle] = pdt.Vehicle,
			pd.[ModificationUser] = @UserCode, pd.[ModificationDate] = Common.GetDate(), pd.[Osmolarity] = pdt.Osmolarity, pd.[Density] = pdt.Density, 
			pd.[AtcId] = pdt.AtcId, pd.[SupplieId] = pdt.SupplieId, pd.[ComponentType] = pdt.ComponentType, pd.[MainMedicine] = pdt.MainMedicine, 
			pd.[PreparationType] = pdt.Preparationtype, pd.[Dilution] = pdt.Dilution, pd.[Concentration] = pdt.Concentration,
			pd.[AmountTime] = pdt.AmountTime, pd.[TimeUnit] = pdt.TimeUnit, pd.[VolumeTotal] = pdt.VolumeTotal, pd.[NPTItemOrder] = pdt.NPTItemOrder,
			pd.[ComplementaryMedicine] = pdt.ComplementaryMedicine
			FROM @PackageDetail pdt
			INNER JOIN MixingStation.PackageDetail pd on pd.Id = pdt.Id
			WHERE pdt.Id > 0
		END
		ELSE BEGIN
			UPDATE pd SET pd.[PackagePersonalizedId] = pdt.PackageId, pd.[ProductId] = pdt.ProductId, pd.[Quantity] = pdt.Quantity, pd.[MeasurementUnitId] = pdt.MeasurementUnitId,
			pd.[Volume] = pdt.Volume, pd.[VolumeMeasureUnit] = pdt.VolumeMeasureUnit, pd.[Thinner] = pdt.Thinner, pd.[Vehicle] = pdt.Vehicle,
			pd.[ModificationUser] = @UserCode, pd.[ModificationDate] = Common.GetDate(), pd.[Osmolarity] = pdt.Osmolarity, pd.[Density] = pdt.Density, 
			pd.[AtcId] = pdt.AtcId, pd.[SupplieId] = pdt.SupplieId, pd.[ComponentType] = pdt.ComponentType, pd.[MainMedicine] = pdt.MainMedicine,
			pd.[PreparationType] = pdt.Preparationtype, pd.[Dilution] = pdt.Dilution, pd.[Concentration] = pdt.Concentration,
			pd.[AmountTime] = pdt.AmountTime, pd.[TimeUnit] = pdt.TimeUnit, pd.[VolumeTotal] = pdt.VolumeTotal, pd.[NPTItemOrder] = pdt.NPTItemOrder,
			pd.[ComplementaryMedicine] = pdt.ComplementaryMedicine
			FROM @PackageDetail pdt
			INNER JOIN MixingStation.PackagePersonalizedDetail pd on pd.Id = pdt.Id
			WHERE pdt.Id > 0
		END
		--Se retorna el ok
		IF @UpdatePackage = 1
		SELECT 0 as CodeMessage, 'Se actualizo el paquete con código ' + @Code as Message, @Id Id, @Code Code
		ELSE
		SELECT 0 as CodeMessage, 'Se generó el paquete con código ' + @Code as Message, @Id Id, @Code Code
		return

	END TRY
	BEGIN CATCH

		--Se retorna el error
		SELECT 999 as CodeMessage, ERROR_MESSAGE() as Message, 0 Id, '' Code
		return

	END CATCH

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que registra y actualiza paquetes maestros y paquetes personalizados de la estación de mezclas (MixingStation), los cuales representan preparaciones farmacéuticas como mezclas intravenosas, nutrición parenteral y dosis unitarias. Recibe un XML con todos los atributos del paquete (cabecera con datos como nombre, código, osmolaridad, volumen total, estabilidad, instrucciones de preparación, fotoprotección, tipo de preparación, entre otros) y una tabla de detalle de tipo PackageDetailType con los componentes o insumos del paquete. Según si el paquete es personalizado o maestro, inserta o actualiza los registros en las tablas PackageDetail y PackagePersonalizedDetail, usando las funciones GetExistingPackageDetail y GetExistingPackagePersonalizedDetail para detectar si el registro ya existe, y generando el código automáticamente a través de la tabla de secuencias MixingStationSequence enlazada con Common.Sequense cuando se trata de un nuevo paquete. También gestiona el tipo de dosis unitaria (UnitDoseType) y su clasificación (MSClass) para categorizar correctamente la preparación dentro del sistema de farmacia.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_SavePackage';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_SavePackage';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (inserta o actualiza) la cabecera y los detalles de un paquete de mezcla farmacéutica —maestro o personalizado— a partir de un XML, gestionando generación automática de código por secuencia y validaciones de duplicidad y consistencia.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SavePackage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener un nodo /Data con la cabecera y opcionalmente /Data/Details con los componentes; @UnitDoseTypeId debe existir en MixingStation.UnitDoseType para resolver MSClass; Debe existir configuración en MixingStation.MixingStationSequence/MixingStationSequenceDetail para el FormId correspondiente (2065 maestro, 2239 personalizado); si Scope=''OU'' debe existir detalle para @OperatingUnitId; Para actualizar (Id>0) debe existir el registro en Package o PackagePersonalized según corresponda; Los Ids de detalle marcados con IsDelete=1 deben corresponder a registros existentes en PackageDetail/PackagePersonalizedDetail', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SavePackage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las dos ramas (maestro y personalizado) son mutuamente excluyentes según @IsPackagePersonalized y nunca tocan las tablas de la otra rama; El campo RefrigeratedTerm siempre se inserta/actualiza con valor 0; CreationUser/CreationDate sólo se setean en INSERT; ModificationUser/ModificationDate sólo en UPDATE, usando @UserCode y Common.GetDate(); Un detalle no puede ser simultáneamente diluyente y vehículo (Thinner=1 AND Vehicle=1) — el procedimiento rechaza la operación completa; La generación automática de Code sólo ocurre cuando no se envía Code; al generarlo se incrementa atómicamente el Next del MixingStationSequenceDetail correspondiente; Los detalles marcados con IsDelete=1 se eliminan físicamente antes del flujo de inserción/actualización; Los registros de @PackageDetail con Id=0 se insertan como nuevos detalles; los de Id>0 se actualizan; La salida estándar siempre es un resultset con columnas (CodeMessage, Message, Id, Code); CodeMessage=0 indica éxito y 999 indica error/validación; Cualquier excepción en el TRY se captura y devuelve como CodeMessage=999 con ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SavePackage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paquete maestro de mezclas; paquete personalizado; componentes diluyente y vehículo; dosis unitaria (UnitDoseType); secuencia/consecutivo por formulario; ámbito de secuencia por organización o unidad operativa; estabilidad y fotoprotección de la preparación; osmolaridad y volumen total; ATC y medicamento principal; nutrición parenteral (NptId)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SavePackage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Id > 0 → Marca @UpdatePackage=1 y procede en modo actualización (UPDATE de cabecera) else Modo inserción (@UpdatePackage=0): genera código si falta y hace INSERT de cabecera; si @IsPackagePersonalized = 0 → Opera contra MixingStation.Package y MixingStation.PackageDetail; usa FormId ''2065'' para secuencia else Opera contra MixingStation.PackagePersonalized y MixingStation.PackagePersonalizedDetail; usa FormId ''2239'' para secuencia; si EXISTS detalle con Thinner=1 AND Vehicle=1 en @PackageDetail → Aborta retornando CodeMessage=999 con mensaje ''Existen detalles(Medicamentos) que son diluyente y vehiculo al mismo tiempo''; si @UpdatePackage=0 AND @IsPackagePersonalized=1 y GetExistingPackagePersonalizedDetail devuelve un paquete coincidente → Retorna CodeMessage=0 con el Id/Code del paquete personalizado existente y termina sin insertar; si @IsPackagePersonalized=0 y GetExistingPackageDetail devuelve un paquete distinto a @Id con los mismos componentes → Aborta con CodeMessage=999 indicando que ya existe un paquete con los mismos componentes; si ISNULL(@Code,'''') = '''' (no llega código) → Genera el código vía dbo.GetSequence usando MixingStationSequence/SequenceDetail según Scope (''O'' organización o por OperatingUnit) e incrementa el Next del detalle de secuencia; si @scope = ''O'' → Toma la secuencia por IdForm sin filtrar unidad operativa else Filtra la secuencia adicionalmente por bsd.IdOperatingUnit = @OperatingUnitId; si @idSequenceDetail IS NULL tras buscar la secuencia → Aborta con CodeMessage=999 ''Secuencia no encontrada para generar el paquete (personalizado si FormId=''2239'')''; si ISNULL(@Id,0)=0 → INSERT de cabecera en Package o PackagePersonalized y captura SCOPE_IDENTITY() como nuevo Id else UPDATE de cabecera del registro existente con ModificationUser/ModificationDate', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SavePackage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetSequence; Common.GETDATE; MixingStation.GetExistingPackagePersonalizedDetail; MixingStation.GetExistingPackageDetail', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SavePackage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.UnitDoseType; MixingStation.MixingStationSequence; MixingStation.MixingStationSequenceDetail; Common.Sequense; MixingStation.GetExistingPackagePersonalizedDetail; MixingStation.GetExistingPackageDetail; MixingStation.PackageDetail; MixingStation.PackagePersonalizedDetail', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SavePackage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SavePackage';
-- GO
