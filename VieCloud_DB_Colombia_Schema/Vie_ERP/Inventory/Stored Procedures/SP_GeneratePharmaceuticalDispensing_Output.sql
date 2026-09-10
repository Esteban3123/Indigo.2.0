-- =============================================
-- Author:		Cristhian Mauricio Salazar Narvaez
-- Create date: 15/04/2016
-- Description:	Procedimiento para guardar o confirmar las dispensaciones farmaceuticas
-- =============================================
CREATE PROCEDURE [Inventory].[SP_GeneratePharmaceuticalDispensing_Output]
	@XmlPharmaceutical XML,
	@XmlAnnulateDashboard XML,
	@user VARCHAR(20),
	@XmlDispensingIntegrationMedilaser XML = '',

	@CodeMessageResult VARCHAR(20) OUTPUT,
	@ErrorsValidationResult VARCHAR(MAX) OUTPUT,
	@DispensingIdResult INT OUTPUT,
	@DispensingCodeResult VARCHAR(20) OUTPUT,
	@StatusResult TINYINT OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @UserName AS Varchar(50) = (SELECT P.Fullname FROM [Security].[User] U INNER JOIN [Security].[Person] P ON U.IdPerson = P.Id WHERE UserCode = @user)
	DECLARE @TableErrors as table([message] varchar(max))
	DECLARE @ErrorsValidation varchar(max),
		@Dos Tinyint = 2,
		@Tres Tinyint = 3,
		@Cero Tinyint = 0,
		@Deleted Varchar(7) = 'Deleted',
		@Uno Tinyint = 1,
		@IdForm322 int = 322,
		@Cinco Tinyint = 5,
		@InitDate Date = '01-01-1900',
		@Diecinueve Tinyint = 19,
		@SCero Varchar(1) = '0',
		@strPharmaceuticalDispensing Varchar(24) = 'PharmaceuticalDispensing',
		@IdConsecuHojaGastoQX Varchar(8)= '00000031'
	CREATE TABLE #HCKARDPACConcecutives (
		[NUMCONSEC] varchar(50) not null,
		[CODPRODUC] varchar(20) not null,
		[CodeSusceptibleMixingStation] uniqueidentifier null
	)
	CREATE NONCLUSTERED INDEX IX_HCKARDPACConcecutives_NUMCONSEC_CODPRODUC
		ON #HCKARDPACConcecutives ([NUMCONSEC], [CODPRODUC], [CodeSusceptibleMixingStation])

	DECLARE @IdPharmaceutical INT,
			@CodePharmaceutical VARCHAR(20),
			@IsPharmaceuticalDispensing TINYINT,
			@OperatingUnitId INT,
			@AdmissionNumber VARCHAR(20),
			@AdmissionNumberOrigin VARCHAR(20),
			@AdmissionNumberHijo VARCHAR(20),
			@CodePatient VARCHAR(25),
			@DocumentDate DATETIME,
			@AffectInventory BIT,
			@Status TINYINT,
			@FunctionalUnitId AS INT,
			@FunctionalUnitCode VARCHAR(20),
			@ConsecutivePharmacy VARCHAR(20),
			@CareCenterCode VARCHAR(20),
			@HistoryType VARCHAR(20),
			@ConsecutivePescription INT,
			@ConsecutiveInputs VARCHAR(20),
			@ConsecutiveCrystal VARCHAR(20),
			@EntityName varchar(250), @EntityCode varchar(20), @EntityId int,
			@DispensingIntegration TINYINT,
			@OfficeType INT,
			@LogisticOperator INT,
			@MedicalOrderRecipe VARCHAR(20),
			@FunctionalUnitHeonId AS INT,
			-----------------------------------------------
			@HistoryTypeCrystal int,
			@HistoryTypeNameCrystal varchar(max),
			@IDHCORDPRON int, --Permite saber si el registro de la pestaña de quimioterapia es de tipo domiciliaria
			@EconomicActivityId Int -- Obtiene la Actividad economica

	DECLARE @ExecutionId UNIQUEIDENTIFIER = NEWID(),
			@ExecutionPreviousAt DATETIME2(3) = SYSUTCDATETIME(),
			@ExecutionNow DATETIME2(3),
			@ExecutionStepOrder INT = 0

	CREATE TABLE #TablePharmaceuticalDetail
	(
		RowId Int Identity(1,1) Primary Key,
		ChangeTracker varchar(30),
		Id int,
		IdTmp int,
		CareGroupId int NOT NULL,
		HealthAdministratorId int NULL,
		ThirdPartyId int NULL,
		ProductId int NOT NULL,
		CodeProduct varchar(20),
		MedicamentCode varchar(20),
		WarehouseId int NOT NULL,
		Quantity int NOT NULL,
		ServiceDate datetime NOT NULL,
		FunctionalUnitId int NOT NULL,
		CostCenterId int,
		OrderedHealthProfessionalCode char(20) NULL,
		OrderedProfessionalSpecialty char(3) NULL,
		OrderedHealthProfessionalThirdPartyId int NULL,
		AuthorizationNumber varchar(20) NULL,
		LiquidationType tinyint NOT NULL,
		SurchargeApply bit NOT NULL,
		SalePrice numeric(20, 4) NOT NULL,
		AverageCost numeric(20, 4)  NULL,
		FinalProductCost Decimal(18,2) NOT NULL,
		DiscountPercentage numeric(5, 2) NOT NULL,
		DiscountValue numeric(20, 4) NOT NULL,
		TotalSalesPrice numeric(20, 4) NOT NULL,
		GrandTotalSalesPrice numeric(20, 4) NOT NULL,
		ProductType varchar(20),
		CantidadSolicitada int,
		CantidadPendiente int,
		IdProductHeon int,
		recetarioOMedica varchar(20),
		GuardaGastoQX  bit,
		IdProgramacionQXPrincipal int,
		Extramural bit
		,PhysicalInventoryCustodyId int NULL,
		QuotationPharmaceuticalDispensingDetailId int null,
		EntityId int null,
		EntityName varchar(250) null,
		CantidadMezcla int null,
		GrossValue decimal(18,2) not null,
		TaxValue decimal(18,2) not null,
		IvaId int NULL,
		Note Varchar(500),
		EconommicActivityId int,
		PackageId int,
		ProductInventoryCode varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		ProductATCCode varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		ProductSupplieCode varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
	)
	CREATE NONCLUSTERED INDEX IX_TablePharmaceuticalDetail_IdTmp
		ON #TablePharmaceuticalDetail (IdTmp)
		INCLUDE (Id, ProductId, WarehouseId, Quantity, CodeProduct, PhysicalInventoryCustodyId, ChangeTracker, EntityId, EntityName)
	CREATE NONCLUSTERED INDEX IX_TablePharmaceuticalDetail_Id_RowId
		ON #TablePharmaceuticalDetail (Id, RowId)
		INCLUDE (IdTmp, ProductId, Quantity, WarehouseId)
	CREATE NONCLUSTERED INDEX IX_TablePharmaceuticalDetail_Entity
		ON #TablePharmaceuticalDetail (EntityName, EntityId, CodeProduct)
		INCLUDE (ProductId, Quantity, CantidadMezcla)
	CREATE NONCLUSTERED INDEX IX_TablePharmaceuticalDetail_Row_WorkData
		ON #TablePharmaceuticalDetail (ChangeTracker, RowId)
		INCLUDE (ProductId, Quantity, ProductType, CantidadSolicitada, OrderedHealthProfessionalCode, Extramural, EntityId, CantidadMezcla, ProductInventoryCode, ProductATCCode, ProductSupplieCode)

	CREATE TABLE #TableDeliveriesByPharmacyProductType
	(
		PharmaceuticalDispensingDetailIdTmp int,
		HCFARMEPDID int NOT NULL,
		ProductId INT NOT NULL,
		ProductType bit NOT NULL,
		Quantity int NOT NULL,
		ConcentrationByUnit VARCHAR(20) NOT NULL,
		TotalWeight VARCHAR(30) NOT NULL,
		QuantityDeliveryPT INT NULL
	)
	CREATE NONCLUSTERED INDEX IX_TableDeliveriesByPharmacyProductType_HCFARMEPDID_ProductId
		ON #TableDeliveriesByPharmacyProductType (HCFARMEPDID, ProductId)
		INCLUDE (ProductType, Quantity, QuantityDeliveryPT)

	CREATE TABLE #TablePharmaceuticalBatch
	(
		ChangeTracker varchar(30),
		Id int NOT NULL,
		PharmaceuticalDetailIdTmp int,
		PharmaceuticalDispensingDetailId int NOT NULL,
		PhysicalInventoryId int NULL,
		Quantity int NOT NULL,
		OutstandingQuantity int NOT NULL
		,PhysicalInventoryCustodyId int NULL
	)
	CREATE NONCLUSTERED INDEX IX_TablePharmaceuticalBatch_DetailTmp
		ON #TablePharmaceuticalBatch (PharmaceuticalDetailIdTmp)
		INCLUDE (Id, PharmaceuticalDispensingDetailId, PhysicalInventoryId, PhysicalInventoryCustodyId, Quantity, OutstandingQuantity, ChangeTracker)
	CREATE NONCLUSTERED INDEX IX_TablePharmaceuticalBatch_PhysicalInventoryId
		ON #TablePharmaceuticalBatch (PhysicalInventoryId)
		INCLUDE (PharmaceuticalDetailIdTmp, Quantity)
	CREATE NONCLUSTERED INDEX IX_TablePharmaceuticalBatch_PhysicalInventoryCustodyId
		ON #TablePharmaceuticalBatch (PhysicalInventoryCustodyId)
		INCLUDE (PharmaceuticalDetailIdTmp, Quantity)

	DECLARE @MessageReturn varchar(max) = ''
	DECLARE @GetDateTime DATETIME = Common.GETDATE()
	DECLARE @AnnulationDashboardIterations INT = 0

	BEGIN TRY
		SET @ExecutionNow = SYSUTCDATETIME()
		SET @ExecutionStepOrder += 1
		INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
			(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
		VALUES
			(@ExecutionId, @ExecutionStepOrder, 'PREPARACION_TABLAS_TEMPORALES', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, 0, 0, @user, NULL)
		SET @ExecutionPreviousAt = @ExecutionNow

		--- Si hay cosas items para anular desde el dashboard
		IF Exists (select t.x.value('Ingreso[1]','varchar(20)') from @XmlAnnulateDashboard.nodes('/Main/ViewDashboardPharmacyDetail') t(x)
			where t.x.value('Ingreso[1]','varchar(20)') is not null) OR Exists(select t.x.value('Ingreso[1]','varchar(20)') from @XmlAnnulateDashboard.nodes('/Main/ViewDashBoardPharmacy_SurgicalPackageDeatils') t(x)
																				where t.x.value('Ingreso[1]','varchar(20)') is not null)  begin

			declare @TableAnnulate table (
				RowId Int Identity(1,1) Primary Key,
				AdmissionNumber varchar(20),
				PatientCode varchar(50),
				CareCenterCode varchar(50),
				FunctionUnitCode varchar(50),
				ConsecutivePescription varchar(50),
				ConsecutiveInputs varchar(50),
				ConsecutivePharmacy varchar(50),
				Consecutivo varchar(50),
				Medico varchar(100),
				Producto varchar(100),
				CodProducto VARCHAR(100),
				[HCMOANULBId]  [char](4),
				[Description] [Varchar](500),
				AnulateOnly int,
				EntityName varchar(50))

			declare @AdmissionNumberDel varchar(20), @PatientCodeDel varchar(50), @CareCenterCodeDel varchar(50),
				@FunctionUnitCodeDel varchar(50), @ConsecutivePescriptionDel varchar(50), @ConsecutiveInputsDel varchar(50),
				@ConsecutivePharmacyDel varchar(50), @ConsecutivoDel varchar(50), @MedicoDel varchar(100),
				@ProductoDel varchar(100), @CodProductoDel VARCHAR(100), @ConsecutiveKardex decimal(18,0),@ConsecutiveHojaGastoQX decimal(18,0),
				@HCMOANULBId char(4), @Description VARCHAR(500), @AnulateOnly INT,@EntityNameDel varchar(50)

			insert @TableAnnulate
				select
					t.x.value('Ingreso[1]','varchar(20)'),
					t.x.value('CodigoPaciente[1]','varchar(50)'),
					t.x.value('CareCenterCode[1]','varchar(50)'),
					t.x.value('FunctionUnitCode[1]','varchar(50)'),
					t.x.value('ConsecutivePescription[1]','varchar(50)'),
					t.x.value('ConsecutiveInputs[1]','varchar(50)'),
					t.x.value('ConsecutivePharmacy[1]','varchar(50)'),
					t.x.value('Consecutivo[1]','varchar(50)'),
					t.x.value('Medico[1]','varchar(100)'),
					t.x.value('Producto[1]','varchar(100)'),
					t.x.value('CodProducto[1]','varchar(100)'),
					t.x.value('HCMOANULBId[1]','varchar(4)'),
					t.x.value('Description[1]','varchar(500)'),
					t.x.value('AnulateOnly[1]','int'),
					'DashboardPharmacyDetail'
				from @XmlAnnulateDashboard.nodes('/Main/ViewDashboardPharmacyDetail') t(x)

			insert @TableAnnulate
				select
					t.x.value('Ingreso[1]','varchar(20)'),
					t.x.value('CodigoPaciente[1]','varchar(50)'),
					t.x.value('CareCenterCode[1]','varchar(50)'),
					t.x.value('FunctionUnitCode[1]','varchar(50)'),
					t.x.value('ConsecutivePescription[1]','varchar(50)'),
					t.x.value('ConsecutiveInputs[1]','varchar(50)'),
					t.x.value('ConsecutivePharmacy[1]','varchar(50)'),
					t.x.value('Consecutivo[1]','varchar(50)'),
					t.x.value('Medico[1]','varchar(100)'),
					t.x.value('Producto[1]','varchar(100)'),
					t.x.value('CodProducto[1]','varchar(100)'),
					t.x.value('HCMOANULBId[1]','varchar(4)'),
					t.x.value('Description[1]','varchar(500)'),
					t.x.value('AnulateOnly[1]','int'),
					'SurgicalPackageDetails'
				from @XmlAnnulateDashboard.nodes('/Main/ViewDashBoardPharmacy_SurgicalPackageDeatils') t(x)

			-- Bloquear anulaciones si algún ítem ya está siendo procesado por central de mezclas
			IF EXISTS (
				SELECT 1
				FROM @TableAnnulate ta
				JOIN dbo.HCFARMEPD h ON h.CODCONCEC = ta.Consecutivo AND h.CODPRODUC = ta.CodProducto
				WHERE h.SENDTO = 2 AND h.VIEPROCESSED = 1
			)
			BEGIN
				SET @CodeMessageResult = '999'
				SET @ErrorsValidationResult = 'No se puede anular la solicitud porque uno o más medicamentos ya están siendo procesados por la central de mezclas.'
				SET @DispensingIdResult = 0
				SET @DispensingCodeResult = ''
				SET @StatusResult = 3
				GOTO Cleanup
			END

			Declare @Rows Int, @RowId Int
			Set @Rows = 1
			Set @RowId = 1


			WHILE @Rows > 0
			BEGIN
				Select Top 1 @RowId = RowId
					, @AdmissionNumberDel = AdmissionNumber
					, @PatientCodeDel = PatientCode
					, @CareCenterCodeDel = CareCenterCode
					, @FunctionUnitCodeDel = FunctionUnitCode
					, @ConsecutivePescriptionDel = ConsecutivePescription
					, @ConsecutiveInputsDel = ConsecutiveInputs
					, @ConsecutivePharmacyDel = ConsecutivePharmacy
					, @ConsecutivoDel = Consecutivo
					, @MedicoDel = Medico
					, @ProductoDel = Producto
					, @CodProductoDel = CodProducto
					, @HCMOANULBId = [HCMOANULBId]
					, @Description = CONCAT(TRIM(h.DESMOTANU), ' - ',[Description])
					, @AnulateOnly = [AnulateOnly]
					, @EntityNameDel = EntityName
				From @TableAnnulate ta
				Join HCMOANULB h ON h.CODMOTANU = ta.HCMOANULBId
				Where ta.RowId >= @RowId Order By ta.RowId

				Set @Rows = @@ROWCOUNT
				If @Rows = 0
					Break

				IF @CodProductoDel IS NULL BEGIN
				SET @CodeMessageResult = '999'
							SET @ErrorsValidationResult = 'No se encontraron productos pendientes por procesar, asociados a esta solicitud.'
							SET @DispensingIdResult = 0
							SET @DispensingCodeResult = ''
							SET @StatusResult = 3
							GOTO Cleanup
				END

				INSERT INTO [dbo].[HCKARDPAC]
						([NUMCONSEC]
						,[IPCODPACI]
						,[NUMINGRES]
						,[CODCENATE]
						,[UFUCODIGO]
						,[CODPROSAL]
						,[CODPRODUC]
						,[CANPRODUCT]
						,[TIPREGIST]
						,[HCPRESCRN]
						,[HCSOLINSN]
						,[HCCTRAPLN]
						,[HCCTRAPLM]
						,[CODDOCUME]
						,[FECREGKAR]
						,[TIPORIREG]
						,[DESMOVPRO]
						,[JUSANULAC]
						,[CONSECFAR]
						,[FECHAUTIL]
						,[OBSERVACI])
					VALUES
						(NEWID()
						,@PatientCodeDel
						,@AdmissionNumberDel
						,@CareCenterCodeDel
						,@FunctionUnitCodeDel
						,RTRIM(SUBSTRING(@MedicoDel,0,CHARINDEX(' ',@MedicoDel)))
						,@CodProductoDel
						,0
						,'4'
						,@ConsecutivePescriptionDel
						,@ConsecutiveInputsDel
						,null
						,null
						,null
						,@GetDateTime
						,9
						,'Despacho farmacia, origen solicitud: Anulación farmacia - usuario:' + @UserName
						,null
						,@ConsecutivePharmacyDel
						,null
						,null)

				--- Actualizo los estado en la solicitud de farmacia
				Declare @CodProduct Varchar(20) = @CodProductoDel,
						@CodProductNormalized CHAR(20) = CONVERT(CHAR(20), @CodProductoDel),
						@ConsecutivoDelNumeric NUMERIC(18,0) = TRY_CONVERT(NUMERIC(18,0), NULLIF(LTRIM(RTRIM(@ConsecutivoDel)),'')),
						@HCFARMEPDId as INT,
						@ExtramuralDel BIT = NULL,
						@AdministrationRoute as VARCHAR(250),
						@ProccesType TINYINT

				Update dbo.HCFARMEPD Set CANPENPRO = 0, PROESTADO = '3'
				Where CODCONCEC = @ConsecutivoDelNumeric
					and CODPRODUC = @CodProductNormalized

				SELECT TOP 1
					@HCFARMEPDId  = hc.Id,
					@ExtramuralDel = COALESCE(hc.EXTRAMURAL,0),
					@AdministrationRoute = hp.DESADMINI,
					@ProccesType =
						CASE hc.SourceTable
							WHEN 'HCPRESCRA' THEN 1
							WHEN 'HCINFLIQA' THEN 2
							ELSE 1  -- Por defecto, si es nulo o cualquier otro valor
						END
				FROM HCFARMEPD hc
				LEFT JOIN HCPRESCRA hp ON hp.ID = hc.IdSourceTable AND hc.SourceTable = 'HCPRESCRA'
				WHERE hc.NUMINGRES = @AdmissionNumberDel
					AND hc.CODPRODUC = @CodProductNormalized
					AND hc.CODCONCEC = @ConsecutivoDelNumeric

				/***SEGMENTO AUDITORIA MedicalHistory***/
				IF @EntityNameDel = 'DashboardPharmacyDetail' and @ExtramuralDel = 0 AND EXISTS(SELECT 1 FROM Inventory.ATC  WHERE Code = @CodProduct) BEGIN

					INSERT INTO MedicalHistory.TraceabilityDrugs(	[ProductCode],
																	[NUMINGRES] ,
																	[ProfessionalCode],
																	[RegistrationDate] ,
																	[Action],
																	[UFUCODIGO],
																	[IdSourceTable],
																	[SourceTable],
																	[AdministrationRoute],
																	[Justification],
																	[ProccesType])
					VALUES(	@CodProduct,
							@AdmissionNumberDel,
							@user,
							@GetDateTime,
							17, -- Farmacia - Anulación de solicitud de medicamento
							@FunctionUnitCodeDel,
							@HCFARMEPDId,
							'HCFARMEPD',
							@AdministrationRoute,
							@Description,
							@ProccesType)
				END
				/**************************************/

				---se obtiene el id del detalle del item

				If Not Exists (Select 1 From dbo.HCFARMEPD Where CODCONCEC = @ConsecutivoDelNumeric and CANPENPRO > @Cero ) Begin
					Update dbo.HCFARMEPC Set ORDESTADO = '3' where CODCONCEC = @ConsecutivoDelNumeric

					if @HCMOANULBId is not null and @HCMOANULBId <> '' begin
						INSERT INTO [Inventory].[ReasonCancellationOfRequest] values(@ConsecutivoDel,@HCMOANULBId,@Description,@user,[Common].[GETDATE](),null)
					END
					ELSE BEGIN
						set @CodeMessageResult = '999'
						set @ErrorsValidationResult = 'No hay motivo de anulación'
						set @DispensingIdResult = 0
						set @DispensingCodeResult = ''
						set @StatusResult = 3
						GOTO Cleanup
					end
				end
				else if (@AnulateOnly = 1)
				begin
					if @HCMOANULBId is not null and @HCMOANULBId <> '' begin
							INSERT INTO [Inventory].[ReasonCancellationOfRequest] values(@ConsecutivoDel,@HCMOANULBId,@Description,@user,[Common].[GETDATE](),@HCFARMEPDId)
						END
						ELSE BEGIN
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = 'No hay motivo de anulación'
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							GOTO Cleanup
						end
				end

				SET @AnnulationDashboardIterations += 1
				SET @RowId += 1
			END
		END -- Fin si hay anulaciones

		SET @ExecutionNow = SYSUTCDATETIME()
		SET @ExecutionStepOrder += 1
		INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
			(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
		VALUES
			(@ExecutionId, @ExecutionStepOrder, 'ANULACION_DASHBOARD', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, 0, 0, @user, CONCAT('Iteraciones=', @AnnulationDashboardIterations))
		SET @ExecutionPreviousAt = @ExecutionNow

		--- Solo anulación dashboard (sin XML de dispensación): salida explícita; evita el bloque final que asigna éxito con @CodePharmaceutical NULL
		IF (@XmlPharmaceutical IS NULL OR NOT EXISTS (SELECT 1 FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x) WHERE t.x.value('Id[1]','int') IS NOT NULL))
			AND (
				EXISTS (SELECT 1 FROM @XmlAnnulateDashboard.nodes('/Main/ViewDashboardPharmacyDetail') t(x) WHERE t.x.value('Ingreso[1]','varchar(20)') IS NOT NULL)
				OR EXISTS (SELECT 1 FROM @XmlAnnulateDashboard.nodes('/Main/ViewDashBoardPharmacy_SurgicalPackageDeatils') t(x) WHERE t.x.value('Ingreso[1]','varchar(20)') IS NOT NULL)
			)
		BEGIN
			IF @AnnulationDashboardIterations > 0
			BEGIN
				SET @CodeMessageResult = '0'
				SET @ErrorsValidationResult = 'Anulación desde dashboard procesada correctamente.'
				SET @DispensingIdResult = 0
				SET @DispensingCodeResult = ''
				SET @StatusResult = 1
			END
			ELSE
			BEGIN
				SET @CodeMessageResult = '999'
				SET @ErrorsValidationResult = 'No se procesó ninguna anulación: verifique que el motivo (HCMOANULBId) exista en el catálogo y coincida con el XML, y que el detalle de solicitud sea válido.'
				SET @DispensingIdResult = 0
				SET @DispensingCodeResult = ''
				SET @StatusResult = 3
			END
			GOTO Cleanup
		END

		--- Valido si vienen dispensaciones
		IF Exists (SELECT t.x.value('Id[1]','int') FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x) where t.x.value('Id[1]','int') Is Not Null) Begin
			if IsNull((select TOP 1 t.x.value('IsPharmaceuticalDispensing[1]','varchar') from @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x)), '') = '' Begin
				--select '999' as CodeMessage, 'No se encuentra el parametro IsPharmaceuticalDispensing' as Message, 0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
				set @CodeMessageResult = '999'
				set @ErrorsValidationResult = 'No se encuentra el parametro IsPharmaceuticalDispensing'
				set @DispensingIdResult = 0
				set @DispensingCodeResult = ''
				set @StatusResult = 3
				GOTO Cleanup
			END

			IF ISNULL((SELECT TOP 1 t.x.value('DispensingIntegration[1]','varchar') FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x)), '') = '' BEGIN
				--SELECT '999' as CodeMessage, 'No se encuentra el parametro DispensingIntegration' as Message, 0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
				set @CodeMessageResult = '999'
				set @ErrorsValidationResult = 'No se encuentra el parametro DispensingIntegration'
				set @DispensingIdResult = 0
				set @DispensingCodeResult = ''
				set @StatusResult = 3
				GOTO Cleanup
			END

			SELECT
				@IdPharmaceutical = t.x.value('Id[1]','int'),
				@CodePharmaceutical = t.x.value('Code[1]','varchar(20)'),
				@IsPharmaceuticalDispensing = t.x.value('IsPharmaceuticalDispensing[1]','tinyint'),
				@DispensingIntegration = t.x.value('DispensingIntegration[1]','tinyint'),
				@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
				@AdmissionNumber = t.x.value('AdmissionNumber[1]','varchar(20)'),
				@AdmissionNumberOrigin = t.x.value('AdmissionNumberOrigin[1]','varchar(20)'),
				@AdmissionNumberHijo = t.x.value('AdmissionNumber[1]','varchar(20)'),
				@CodePatient = t.x.value('CodePatient[1]','varchar(25)'),
				@DocumentDate = t.x.value('DocumentDate[1]', 'datetime'),
				@AffectInventory = t.x.value('AffectInventory[1]','bit'),
				@Status = t.x.value('Status[1]','tinyint'),
				@FunctionalUnitCode = t.x.value('FunctionUnitCode[1]','varchar(20)'),
				@ConsecutivePharmacy = t.x.value('ConsecutivePharmacy[1]','varchar(20)'),
				@CareCenterCode = t.x.value('CareCenterCode[1]','varchar(20)'),
				@HistoryType =  t.x.value('HistoryType[1]','varchar(20)'),
				@ConsecutivePescription =  t.x.value('ConsecutivePescription[1]','int'),
				@ConsecutiveInputs =  t.x.value('ConsecutiveInputs[1]','varchar(20)'),
				@ConsecutiveCrystal = t.x.value('ConsecutiveCrystal[1]','varchar(20)'),
				---------------------------------------------------------------
				@EntityName = t.x.value('EntityName[1]','varchar(250)'),
				@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
				@EntityId = t.x.value('EntityId[1]','int'),
				---------------------------------------------------------------
				@IDHCORDPRON = t.x.value('IDHCORDPRON[1]','int') --Permite saber si el registro de la pestaña de quimioterapia es de tipo domiciliaria
			FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x)

			IF EXISTS
			(
				SELECT 1
				FROM Inventory.SettingInventory si
				WHERE @DocumentDate < DATEFROMPARTS(si.Year, si.Month, 1)
			)
			BEGIN
				set @CodeMessageResult = '999'
				set @ErrorsValidationResult = 'No se puede dispensar en un periodo cerrado'
				set @DispensingIdResult = 0
				set @DispensingCodeResult = ''
				set @StatusResult = 3
				GOTO Cleanup
			END

			IF @IsPharmaceuticalDispensing = 1
			BEGIN
				IF ISNULL(@EntityName, '') = ''
				BEGIN
					SET @EntityName = 'SavePharmaceuticalDispensing'
				END
				SET @EntityId = @ConsecutiveCrystal
			END

			--Si el proceso viene de la integración por paciente medilaser se asignan algunos datos
			if @XmlDispensingIntegrationMedilaser.exist('*') > 0
			begin
				SELECT
					@AdmissionNumber = t.x.value('AdmissionNumber[1]','varchar(20)'),
					@DispensingIntegration = t.x.value('DispensingIntegration[1]','tinyint')
				FROM @XmlDispensingIntegrationMedilaser.nodes('/TableXml') t(x)
			end

			--- Inserto los detalles
			INSERT INTO #TablePharmaceuticalDetail
					(ChangeTracker, Id, IdTmp, CareGroupId, HealthAdministratorId, ThirdPartyId, ProductId, CodeProduct, MedicamentCode, WarehouseId, Quantity, ServiceDate, FunctionalUnitId, CostCenterId,
					OrderedHealthProfessionalCode, OrderedProfessionalSpecialty, OrderedHealthProfessionalThirdPartyId, AuthorizationNumber, LiquidationType, SurchargeApply,
					SalePrice, AverageCost,FinalProductCost ,DiscountPercentage, DiscountValue, TotalSalesPrice, GrandTotalSalesPrice, ProductType, CantidadSolicitada, CantidadPendiente, idProductHeon, recetarioOMedica,
					GuardaGastoQX,IdProgramacionQXPrincipal,Extramural, QuotationPharmaceuticalDispensingDetailId, EntityId, EntityName, CantidadMezcla,GrossValue,TaxValue,IvaId,Note, PackageId)
				select
					t.x.value('ChangeTracker[1]', 'varchar(30)') AS ChangeTracker,
					t.x.value('Id[1]','int'),
					t.x.value('PharmaceuticalDispensingDetailIdTmp[1]','int'),
					t.x.value('CareGroupId[1]','int'),
					t.x.value('HealthAdministratorId[1]','int'),
					t.x.value('ThirdPartyId[1]','int'),
					t.x.value('ProductId[1]','int'),
					t.x.value('CodeProduct[1]','varchar(20)'),
					t.x.value('MedicamentCode[1]','varchar(20)'),
					t.x.value('WarehouseId[1]','int'),
					t.x.value('Quantity[1]','int'),
					t.x.value('ServiceDate[1]', 'datetime'),
					t.x.value('FunctionalUnitId[1]','int'),
					t.x.value('CostCenterId[1]','int'),
					t.x.value('OrderedHealthProfessionalCode[1]','varchar(20)'),
					t.x.value('OrderedProfessionalSpecialty[1]','varchar(3)'),
					t.x.value('OrderedHealthProfessionalThirdPartyId[1]','int'),
					t.x.value('AuthorizationNumber[1]','varchar(20)'),
					t.x.value('LiquidationType[1]','tinyint'),
					t.x.value('SurchargeApply[1]','bit'),
					t.x.value('SalePrice[1]','numeric(20,4)'),
					t.x.value('AverageCost[1]','numeric(20,4)'),
					t.x.value('FinalProductCost[1]','Decimal(18,2)'),
					t.x.value('DiscountPercentage[1]','numeric(5,2)'),
					t.x.value('DiscountValue[1]','numeric(20,4)'),
					t.x.value('TotalSalesPrice[1]','numeric(20,4)'),
					t.x.value('GrandTotalSalesPrice[1]','numeric(20,4)'),
					t.x.value('ProductType[1]','varchar(20)'),
					t.x.value('CantidadSolicitada[1]','int'),
					t.x.value('CantidadPendiente[1]','int'),
					t.x.value('idProductoHeon[1]','int'),
					t.x.value('recetarioOMedica[1]','varchar(20)'),
					t.x.value('GuardaGastoQX[1]','bit'),
					t.x.value('IdProgramacionQXPrincipal[1]','int'),
					t.x.value('Extramural[1]','bit'),
					IIF(t.x.value('QuotationPharmaceuticalDispensingDetailId[1]','int') = 0, null, t.x.value('QuotationPharmaceuticalDispensingDetailId[1]','int')),
					IIF(t.x.value('EntityId[1]','int') = 0, null, t.x.value('EntityId[1]','int')),
					IIF(t.x.value('EntityName[1]','varchar(250)') = '', null, t.x.value('EntityName[1]','varchar(250)')),
					t.x.value('CantidadMezcla[1]', 'int'),
					t.x.value('GrossValue[1]','Decimal(18,2)'),
					t.x.value('TaxValue[1]','Decimal(18,2)'),
					NULL,
					t.x.value('Note[1]','Varchar(500)'),
					IIF(t.x.value('PackageId[1]','int') = 0, null, t.x.value('PackageId[1]','int'))
				from @XmlPharmaceutical.nodes('/PharmaceuticalDispensing/PharmaceuticalDispensingDetail') t(x)

			declare @DispensingWithoutAuthorization bit
			select top 1 @DispensingWithoutAuthorization = DispensingWithoutAuthorization from Inventory.SettingInventory where OperatingUnitId = @OperatingUnitId

			--validar cantidad autorizada contra la enviada
			if isnull(@DispensingWithoutAuthorization, 1) = 0 and exists (select 1
				from #TablePharmaceuticalDetail td
				join IHLISTPRO LP (nolock) on td.MedicamentCode = LP.CODPRODUC
				left join (
					SELECT P.NUMINGRES, P.CODPRODUC, SUM(P.CANPEDPRO) - ISNULL([dbo].[fnCalcularCantidadDispensada](p.NUMINGRES, P.CODPRODUC), 0) as SaldoAutorizadoMipres
					FROM dbo.HCJUNOPOM P 
					where p.NUMINGRES = @AdmissionNumber and P.CODMINSALUD <> ''
					GROUP BY P.NUMINGRES, P.CODPRODUC
				) ap on ap.NUMINGRES = @AdmissionNumber and ap.CODPRODUC = td.MedicamentCode
				where LP.NOPOSPROD = 1 and isnull(ap.SaldoAutorizadoMipres, 0) < td.Quantity) begin

				INSERT INTO @TableErrors
					SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', td.CodeProduct, '(Almacen: ', w.Code, ' - ', w.Name, ')')
					from #TablePharmaceuticalDetail td
					JOIN Inventory.Warehouse w ON td.WarehouseId = w.Id
					join IHLISTPRO LP (nolock) on td.MedicamentCode = LP.CODPRODUC
					left join (
						SELECT P.NUMINGRES, P.CODPRODUC, SUM(P.CANPEDPRO) - ISNULL([dbo].[fnCalcularCantidadDispensada](p.NUMINGRES, P.CODPRODUC), 0) as SaldoAutorizadoMipres
						FROM dbo.HCJUNOPOM P 
						where p.NUMINGRES = @AdmissionNumber and P.CODMINSALUD <> ''
						GROUP BY P.NUMINGRES, P.CODPRODUC
					) ap on ap.NUMINGRES = @AdmissionNumber and ap.CODPRODUC = td.MedicamentCode
					where LP.NOPOSPROD = 1 and isnull(ap.SaldoAutorizadoMipres, 0) < td.Quantity

				Select @ErrorsValidation = COALESCE(@ErrorsValidation + '', '') + [message]  from @TableErrors

				set @CodeMessageResult = '999'
				set @ErrorsValidationResult = 'Los siguientes productos no pueden ser dispensados porque la cantidad entregada supera la cantidad autorizada: ' + CHAR(13) + CHAR(10) + ISNULL(@ErrorsValidation, '')
				set @DispensingIdResult = 0
				set @DispensingCodeResult = ''
				set @StatusResult = 3
				GOTO Cleanup
			end

			--Valido si el registro es de tipo domiciliaria de la pestaña de quimioterapia
			if @IDHCORDPRON is not null and @IDHCORDPRON > 0
			begin
				--Se obtiene el id del ciclo
				declare @IDHCORDCICLOS int
				declare @IDHCORDQUIMIO int
				declare @CICLO int
				select top 1 @IDHCORDCICLOS = Id, @IDHCORDQUIMIO = IDHCORDQUIMIO, @CICLO = CICLO from EHR.HCORDCICLOS where IDHCORDPRON = @IDHCORDPRON

				--Se valida si el ciclo es 100% domiciliario
				if (select COUNT(*) from EHR.HCORDCICLOSD where IDHCORDCICLOS = @IDHCORDCICLOS) =
				   (select COUNT(*) from EHR.HCORDCICLOSD where IDHCORDCICLOS = @IDHCORDCICLOS and ADMISTRADIACASA = 1)
				begin
					--Se actualiza el estado de la cabacera
					update EHR.HCORDCICLOS set ESTADO = 3 where Id = @IDHCORDCICLOS

					--Se valida si es el último ciclo
					if @CICLO = (select CICLOS from EHR.HCORDQUIMIO where ID = @IDHCORDQUIMIO)
					begin
						update EHR.HCORDQUIMIO set ESTADO = 3 where ID = @IDHCORDQUIMIO
					end
				end

				--Se valida si esta en estado 1 para poder actualizar
				if (select ESTSERIPS from .HCORDPRON where AUTO = @IDHCORDPRON) = '1'
				begin
					--Se actualiza el estado
					update .HCORDPRON set ESTSERIPS = '2' where AUTO = @IDHCORDPRON

					--Se realiza un registro en la tabla HCCUMPLITRATAESPECIAL
					insert into .HCCUMPLITRATAESPECIAL(IPCODPACI, IDHCORDPRON, NUMINGRES, CODCENATE, UFUCODIGO, CODPROSAL, CODESPECI, FECHAREGISTRO)
					select top 1 @CodePatient, @IDHCORDPRON, @AdmissionNumber, @CareCenterCode, @FunctionalUnitCode, ISNULL(OrderedHealthProfessionalCode, ''), ISNULL(OrderedProfessionalSpecialty, ''), [Common].[GETDATE]()
					from #TablePharmaceuticalDetail
				end
			end

			--Validamos que no existan dispensaciones usando el almacén en tránsito
			IF EXISTS
			(
				SELECT 1
				FROM #TablePharmaceuticalDetail pdd
				JOIN Inventory.Warehouse w ON pdd.WarehouseId = w.Id
				WHERE w.TransitStore = 1
			)
			BEGIN
				INSERT INTO @TableErrors
					SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', pdd.CodeProduct, '(Almacen: ', w.Code, ' - ', w.Name, ')')
					FROM #TablePharmaceuticalDetail pdd
					JOIN Inventory.Warehouse w ON pdd.WarehouseId = w.Id
					WHERE w.TransitStore = 1

				Select @ErrorsValidation = COALESCE(@ErrorsValidation + '', '') + [message]  from @TableErrors

				set @CodeMessageResult = '999'
				set @ErrorsValidationResult = 'Los siguientes productos no pueden ser dispensados desde un almacén de tránsito: ' + CHAR(13) + CHAR(10) + ISNULL(@ErrorsValidation, '')
				set @DispensingIdResult = 0
				set @DispensingCodeResult = ''
				set @StatusResult = 3
				GOTO Cleanup
			END

			--Si viene de integración con HEON, se valida que la orden médica de los productos no están ya dentro de la tabla de control
			if @DispensingIntegration = 2
			begin
				If Exists (Select td.Id
					from #TablePharmaceuticalDetail td
					inner join Inventory.ControlIntegrationHeonDetail cihd  on cihd.MedicalOrderRecipe = td.recetarioOMedica
					inner join Inventory.ControlIntegrationHeon cih  on cih.Id = cihd.ControlIntegrationHeonId
					where cihd.[Status] = @Dos)
				Begin
					Insert Into @TableErrors
					select Concat('- La orden médica ',cihd.MedicalOrderRecipe,' del producto ',td.CodeProduct,' ya tiene un proceso pendiente en las tablas de control de integración. Quedó pendiente en procesar por ',cihd.[Message],'.   ')
					from #TablePharmaceuticalDetail td
					inner join Inventory.ControlIntegrationHeonDetail cihd  on cihd.MedicalOrderRecipe = td.recetarioOMedica
					inner join Inventory.ControlIntegrationHeon cih  on cih.Id = cihd.ControlIntegrationHeonId
					where cihd.[Status] = @Dos

					Select @ErrorsValidation = COALESCE(@ErrorsValidation + '', '') + [message]  from @TableErrors
					--select '999' as CodeMessage, @ErrorsValidation as Message, 0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
					set @CodeMessageResult = '999'
					set @ErrorsValidationResult = @ErrorsValidation
					set @DispensingIdResult = 0
					set @DispensingCodeResult = ''
					set @StatusResult = 3
					GOTO Cleanup
				END
			END
			---- se actualiza el costo promedio y Ultimo Costo
			--update #TablePharmaceuticalDetail set AverageCost = (select ProductCost from Inventory.InventoryProduct where Id = ProductId)
			update tpd SET	tpd.AverageCost = ip.ProductCost,
							tpd.FinalProductCost =ip.FinalProductCost
			from #TablePharmaceuticalDetail tpd
			JOIN Inventory.InventoryProduct ip  ON tpd.ProductId =ip.Id

			UPDATE tpd
			SET ProductInventoryCode = ip.Code COLLATE SQL_Latin1_General_CP1_CI_AS,
				ProductATCCode = atc.Code COLLATE SQL_Latin1_General_CP1_CI_AS,
				ProductSupplieCode = ins.Code COLLATE SQL_Latin1_General_CP1_CI_AS
			FROM #TablePharmaceuticalDetail tpd
			JOIN Inventory.InventoryProduct ip  ON ip.Id = tpd.ProductId
			LEFT JOIN Inventory.ATC atc  ON atc.Id = ip.ATCId
			LEFT JOIN Inventory.InventorySupplie ins  ON ins.Id = ip.SupplieId

			--- Inserto la informacion de las entregas
			insert into #TableDeliveriesByPharmacyProductType (PharmaceuticalDispensingDetailIdTmp,HCFARMEPDID,ProductId, ProductType, Quantity,ConcentrationByUnit,TotalWeight, QuantityDeliveryPT)
				select
					t.x.value('PharmaceuticalDispensingDetailIdTmp[1]','int'),
					t.x.value('HCFARMEPDID[1]','int'),
					t.x.value('ProductId[1]','int'),
					t.x.value('ProductType[1]','bit'),
					t.x.value('Quantity[1]','int'),
					t.x.value('ConcentrationByUnit[1]','varchar(20)'),
					t.x.value('TotalWeight[1]','varchar(30)'),
					t.x.value('QuantityDeliveryPT[1]','int')
				from @XmlPharmaceutical.nodes('/PharmaceuticalDispensing/PharmaceuticalDispensingDetail/DeliveriesByPharmacyProductTypeModel') t(x)

			--- Inserto los seriales
			insert into #TablePharmaceuticalBatch (ChangeTracker,Id,PharmaceuticalDetailIdTmp,PharmaceuticalDispensingDetailId,PhysicalInventoryId,Quantity,OutstandingQuantity,PhysicalInventoryCustodyId)
				select
					t.x.value('ChangeTracker[1]', 'varchar(30)') AS ChangeTracker,
					t.x.value('Id[1]','int'),
					t.x.value('PharmaceuticalDispensingDetailIdTmp[1]','int'),
					t.x.value('PharmaceuticalDispensingDetailId[1]','int'),
					t.x.value('PhysicalInventoryId[1]','int'),
					t.x.value('Quantity[1]','int'),
					t.x.value('OutstandingQuantity[1]','int'),
					t.x.value('PhysicalInventoryCustodyId[1]','int')
				from @XmlPharmaceutical.nodes('/PharmaceuticalDispensing/PharmaceuticalDispensingDetail/PharmaceuticalDispensingDetailBatchSerial') t(x)

			update #TablePharmaceuticalBatch set PhysicalInventoryCustodyId = null where PhysicalInventoryCustodyId = 0
			update #TablePharmaceuticalBatch set PhysicalInventoryId = null where PhysicalInventoryId = 0

			update #TablePharmaceuticalDetail set PhysicalInventoryCustodyId = tpb.PhysicalInventoryCustodyId
			from #TablePharmaceuticalBatch tpb
			where #TablePharmaceuticalDetail.IdTmp = tpb.PharmaceuticalDetailIdTmp

			-- Actualizamos la cantidad de la cabecera de acuerdo a los lotes
			update pdd
				set pdd.Quantity = IIF(w.VirtualStore = 1, ISNULL(pddbs.Quantity, pdd.Quantity),  ISNULL(pddbs.Quantity, 0)),
					pdd.GrandTotalSalesPrice = IIF(w.VirtualStore = 1, ISNULL(pddbs.Quantity, pdd.Quantity),  ISNULL(pddbs.Quantity, 0)) * pdd.TotalSalesPrice
			from #TablePharmaceuticalDetail pdd
			JOIN Inventory.Warehouse w ON pdd.WarehouseId = w.Id
			LEFT JOIN
			(
				SELECT PharmaceuticalDetailIdTmp, SUM(Quantity) Quantity
				FROM #TablePharmaceuticalBatch
				GROUP BY PharmaceuticalDetailIdTmp
			) pddbs ON pdd.IdTmp = pddbs.PharmaceuticalDetailIdTmp

			SET @ExecutionNow = SYSUTCDATETIME()
			SET @ExecutionStepOrder += 1
			INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
				(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
			VALUES
				(@ExecutionId, @ExecutionStepOrder, 'CARGA_XML_DETALLE_LOTES', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, NULL)
			SET @ExecutionPreviousAt = @ExecutionNow

			--Validamos que no existan detalles con cantidad en 0
			If Exists (Select 1 from #TablePharmaceuticalDetail where Quantity <= 0)
			Begin
				Insert Into @TableErrors
				select Concat('- El producto ',td.CodeProduct,' no tiene cantidades válidas. ')
				from #TablePharmaceuticalDetail td
				where td.Quantity <= 0

				Select @ErrorsValidation = COALESCE(@ErrorsValidation + '', '') + [message]  from @TableErrors
				--select '999' as CodeMessage, @ErrorsValidation as Message, 0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
				set @CodeMessageResult = '999'
				set @ErrorsValidationResult = @ErrorsValidation
				set @DispensingIdResult = 0
				set @DispensingCodeResult = ''
				set @StatusResult = 3
				GOTO Cleanup
			END

			IF @IsPharmaceuticalDispensing = 0
			BEGIN
				--Siempre afecto inventario, solo no lo hago si todos los detalles provienen de almacenes virtuales
				IF EXISTS (SELECT pdd.RowId FROM #TablePharmaceuticalDetail pdd JOIN Inventory.WareHouse wh  ON pdd.WareHouseId = wh.Id WHERE wh.VirtualStore = @Cero)
				BEGIN
					SET @AffectInventory = 1
				END
				ELSE
				BEGIN
					SET @AffectInventory = 0
				END
			END

			-- valido si afecta o no el inventario
			if @AffectInventory <> 0
			begin
				if Exists
				(
					select pd.RowId from #TablePharmaceuticalDetail pd
					inner join Inventory.Warehouse w  ON pd.WarehouseId = w.Id
					inner join Inventory.InventoryProduct pr  on pd.ProductId = pr.Id
					inner join Inventory.ProductSubGroup ps  on ps.Id = pr.ProductSubGroupId
					where w.VirtualStore = @Cero
						AND ps.HandlesBatch = @Uno
						AND ChangeTracker <> @Deleted
						AND NOT EXISTS
						(
							SELECT 1
							FROM #TablePharmaceuticalBatch pbatch
							WHERE pbatch.PharmaceuticalDetailIdTmp = pd.IdTmp
						)
				)
				begin

					set @CodeMessageResult = '999'
					set @ErrorsValidationResult = 'los productos que se van a dispensar no tiene detalles de lotes, por favor contacte al Administrador del Sistema'
					set @DispensingIdResult = 0
					set @DispensingCodeResult = ''
					set @StatusResult = 3
					GOTO Cleanup
				END

				IF EXISTS
				(
					SELECT 1
					FROM #TablePharmaceuticalDetail pdd
					JOIN #TablePharmaceuticalBatch pddbs ON pdd.IdTmp = pddbs.PharmaceuticalDetailIdTmp
					JOIN Inventory.PhysicalInventory phy ON pddbs.PhysicalInventoryId = phy.Id
					JOIN Inventory.BatchSerial bs ON phy.BatchSerialId = bs.Id
					WHERE CAST(@DocumentDate AS DATE) > bs.ExpirationDate
				)
				begin
					INSERT INTO @TableErrors
						SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', pdd.CodeProduct, '(Lote: ', bs.BatchCode, ')')
						FROM #TablePharmaceuticalDetail pdd
						JOIN #TablePharmaceuticalBatch pddbs ON pdd.IdTmp = pddbs.PharmaceuticalDetailIdTmp
						JOIN Inventory.PhysicalInventory phy ON pddbs.PhysicalInventoryId = phy.Id
						JOIN Inventory.BatchSerial bs ON phy.BatchSerialId = bs.Id
						WHERE CAST(@DocumentDate AS DATE) > bs.ExpirationDate

					Select @ErrorsValidation = COALESCE(@ErrorsValidation + '', '') + [message]  from @TableErrors

					set @CodeMessageResult = '999'
					set @ErrorsValidationResult = 'Los siguientes productos no pueden ser dispensados por que se encuentran vencidos: ' + CHAR(13) + CHAR(10) + ISNULL(@ErrorsValidation, '')
					set @DispensingIdResult = 0
					set @DispensingCodeResult = ''
					set @StatusResult = 3
					GOTO Cleanup
				END
			END

			--valido que los productos tengan valor en AverageCost
			if Exists (select Id from #TablePharmaceuticalDetail where AverageCost = @Cero and ChangeTracker <> @Deleted AND PhysicalInventoryCustodyId is NULL) begin
				Insert Into @TableErrors
				select Concat('El detalle con el producto ',p.Code,' - ',p.[Name],', tiene el costo promedio en $0',CHAR(13),CHAR(10)) From #TablePharmaceuticalDetail pdd
				inner join Inventory.InventoryProduct p on pdd.ProductId = p.id
				where AverageCost = @Cero And ChangeTracker <> @Deleted
				and pdd.PhysicalInventoryCustodyId IS NULL
				SELECT @ErrorsValidation = COALESCE(@ErrorsValidation + '', '') + [message]  from @TableErrors

				--select '999' as CodeMessage, @ErrorsValidation as Message, 0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
				set @CodeMessageResult = '999'
				set @ErrorsValidationResult = @ErrorsValidation
				set @DispensingIdResult = 0
				set @DispensingCodeResult = ''
				set @StatusResult = 3
				GOTO Cleanup
			END

			/*--------------- insercion o update:notas del quimico farmaceutico--------------------*/
			IF EXISTS (SELECT 1 FROM #TablePharmaceuticalDetail tpd WHERE tpd.Note is not null AND tpd.Note <> '') BEGIN
				update pdn
					set pdn.Note = tpd.Note
				from #TablePharmaceuticalDetail tpd
				JOIN Inventory.ATC atc  ON tpd.CodeProduct = atc.Code
				JOIN  HCFARMEPD h  on tpd.CodeProduct = h.CODPRODUC and tpd.EntityId = h.ID and tpd.EntityName = 'HCFARMEPD'
				JOIN Inventory.PharmaceuticalDispensingNotes pdn  ON pdn.AdmissionNumber = @AdmissionNumber AND pdn.ATCId =atc.Id AND h.ID = pdn.PharmaceuticalRequestDetailId
				WHERE tpd.Note is not null AND tpd.Note <> ''

				insert into Inventory.PharmaceuticalDispensingNotes
				select	h.ID,
						tpd.Note,
						@user,
						Common.GETDATE(),
						atc.Id,
						@AdmissionNumber
				from #TablePharmaceuticalDetail tpd
				JOIN Inventory.ATC atc  ON tpd.CodeProduct = atc.Code
				JOIN  HCFARMEPD h  on tpd.CodeProduct = h.CODPRODUC and tpd.EntityId = h.ID and tpd.EntityName = 'HCFARMEPD'
				LEFT JOIN Inventory.PharmaceuticalDispensingNotes pdn  ON pdn.AdmissionNumber = @AdmissionNumber AND pdn.ATCId =atc.Id AND h.ID = pdn.PharmaceuticalRequestDetailId
				WHERE tpd.Note is not null AND tpd.Note <> '' AND pdn.Id is NULL
			END
			-----------------------------------------------------------------------------------------
			--- si es nuevo
			if @IdPharmaceutical = 0 begin
				--- Obtenemos la secuencia numerica
				if IsNull(@CodePharmaceutical, '') = '' begin
					if Not Exists (select Id from Inventory.InventorySequence  where IdForm = @IdForm322) begin
						--select '999' as CodeMessage, 'No existe secuencia numerica para el formulario de Dispensación Farmaceutica' as Message,
						--0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
						set @CodeMessageResult = '999'
						set @ErrorsValidationResult = 'No existe secuencia numerica para el formulario de Dispensación Farmaceutica'
						set @DispensingIdResult = 0
						set @DispensingCodeResult = ''
						set @StatusResult = 3
						GOTO Cleanup
					END

					declare @idSequenceDetail int, @pattern varchar(300), @NextS bigint, @Scope varchar(5), @IdSequence int
						, @IdSequenceCommon int, @Prefix varchar(20) = ''

					select @IdSequence = Id, @Scope = Scope, @IdSequenceCommon = IdSequence from Inventory.InventorySequence  Where IdForm = @IdForm322

					if @Scope = 'O' begin --- Secuencia por Prefijo
						Select Top 1 @pattern = cs.Pattern
							, @idSequenceDetail = psd.Id
						From Inventory.InventorySequenceDetail psd 
						Inner Join Common.Sequense cs  on cs.Id = psd.IdSequense
						Where psd.InventorySequenceId = @IdSequence
					end
					ELSE BEGIN -- Secuencia por Unidad operativa
						SELECT @pattern = cs.Pattern, @idSequenceDetail = psd.Id
						FROM Inventory.InventorySequenceDetail psd 
						INNER JOIN Common.Sequense cs  ON cs.Id = psd.IdSequense
						WHERE psd.InventorySequenceId = @IdSequence AND IdOperatingUnit = @OperatingUnitId
					END

					Update Inventory.InventorySequenceDetail Set @NextS = [Next] += 1 where Id = @idSequenceDetail
					Select @CodePharmaceutical = dbo.GetSequence(@Prefix,@pattern,(@NextS - 1))

					INSERT INTO Inventory.InventoryControlDocument(DocumentNumber,DocumentType, DocumentUser, DocumentDate) VALUES(@CodePharmaceutical, 5, @user, @DocumentDate)
				END

				INSERT INTO [Inventory].[PharmaceuticalDispensing](Code,OperatingUnitId,AdmissionNumber,DocumentDate,AffectInventory,[Status],CreationUser,CreationDate,EntityName, EntityCode, EntityId)
					VALUES (@CodePharmaceutical, @OperatingUnitId, @AdmissionNumber, @DocumentDate, @AffectInventory, @Status, @user, [Common].[GETDATE](), @EntityName, @EntityCode, @EntityId)

				set @IdPharmaceutical = SCOPE_IDENTITY()

				IF @Status = 2 BEGIN
					UPDATE Inventory.PharmaceuticalDispensing SET ModificationUser = @user, ModificationDate = [Common].[GETDATE](), ConfirmationUser = @user, ConfirmationDate = [Common].[GETDATE]() WHERE Id = @IdPharmaceutical
				END
			END
			ELSE BEGIN
				if EXISTS (select 1 from Billing.ServiceOrder  where EntityCode = @CodePharmaceutical and EntityName = @strPharmaceuticalDispensing) begin
					--select '999' as CodeMessage, 'Esta dispensación ya generó una Órden de servicio' as Message,
						--0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
					set @CodeMessageResult = '999'
					set @ErrorsValidationResult = 'Esta dispensación ya generó una Órden de servicio'
					set @DispensingIdResult = 0
					set @DispensingCodeResult = ''
					set @StatusResult = 3
					GOTO Cleanup
				END

				IF @Status = 2 begin
					update Inventory.PharmaceuticalDispensing set ModificationUser = @user, ModificationDate = [Common].[GETDATE](), ConfirmationUser = @user, ConfirmationDate = [Common].[GETDATE]() where Id = @IdPharmaceutical
				end
				ELSE IF @Status = 3 begin
					update Inventory.PharmaceuticalDispensing set ModificationUser = @user, ModificationDate = [Common].[GETDATE](), AnnulmentUser = @user, AnnulmentDate = [Common].[GETDATE]() where Id = @IdPharmaceutical
					--- Elimino el control de inventarios
					DELETE FROM Inventory.InventoryControlDocument WHERE DocumentNumber = @CodePharmaceutical AND DocumentType = @Cinco

					--Se nulea el campo de cotización
					update #TablePharmaceuticalDetail set QuotationPharmaceuticalDispensingDetailId = null
					update Inventory.PharmaceuticalDispensingDetail set QuotationPharmaceuticalDispensingDetailId = null where PharmaceuticalDispensingId = @IdPharmaceutical
				END
				ELSE BEGIN
					UPDATE Inventory.PharmaceuticalDispensing SET ModificationUser = @user, ModificationDate = [Common].[GETDATE]() WHERE Id = @IdPharmaceutical
				END
			END

			SET @ExecutionNow = SYSUTCDATETIME()
			SET @ExecutionStepOrder += 1
			INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
				(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
			VALUES
				(@ExecutionId, @ExecutionStepOrder, 'GUARDA_ENCABEZADO', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, CONCAT('Estado=', @Status, ';Integracion=', @DispensingIntegration))
			SET @ExecutionPreviousAt = @ExecutionNow

			--DEBO TRAER EL NUMERO DE DOCUMENTO DEL PACIENTE
			declare @PatientCode varchar(25) = ''
			IF @DispensingIntegration = 1
			BEGIN
				SET @PatientCode = (select IPCODPACI from dbo.ADINGRESO ad  where NUMINGRES = @AdmissionNumberHijo)
			END
			ELSE IF @DispensingIntegration = 2 or @DispensingIntegration = 3
			BEGIN
				-- De acuerdo con el tipo de integración (1 - Nativa, 2 - Heon, 3 - Integración Medilaser)
				set @PatientCode = @CodePatient

				--Valido la siguiente información si es 2-Heon
				IF @DispensingIntegration = 2
				begin
					--Validamos que se haya recibido la información necesaria
					IF ISNULL((SELECT t.x.value('OfficeType[1]','varchar') FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x)), '') = '' BEGIN
						--SELECT '999' as CodeMessage, 'No se encuentra el parametro OfficeType' as Message, 0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
						set @CodeMessageResult = '999'
						set @ErrorsValidationResult = 'No se encuentra el parametro OfficeType'
						set @DispensingIdResult = 0
						set @DispensingCodeResult = ''
						set @StatusResult = 3
						GOTO Cleanup
					END
					IF ISNULL((SELECT t.x.value('LogisticOperator[1]','varchar') FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x)), '') = '' BEGIN
						--SELECT '999' as CodeMessage, 'No se encuentra el parametro LogisticOperator' as Message, 0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
						set @CodeMessageResult = '999'
						set @ErrorsValidationResult = 'No se encuentra el parametro LogisticOperator'
						set @DispensingIdResult = 0
						set @DispensingCodeResult = ''
						set @StatusResult = 3
						GOTO Cleanup
					END
					IF ISNULL((SELECT t.x.value('MedicalOrderRecipe[1]','varchar') FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x)), '') = '' BEGIN
						--SELECT '999' as CodeMessage, 'No se encuentra el parametro MedicalOrderRecipe' as Message, 0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
						set @CodeMessageResult = '999'
						set @ErrorsValidationResult = 'No se encuentra el parametro MedicalOrderRecipe'
						set @DispensingIdResult = 0
						set @DispensingCodeResult = ''
						set @StatusResult = 3
						GOTO Cleanup
					END
				END

				DECLARE @FunctionalUnitName VARCHAR(200)
						, @CareGroupId INT
						, @HealthAdministratorId INT
						, @CODEMPRES CHAR(5)
						, @AUUBICACI CHAR(20)

				--Validamos la siguiente info si es 2-Heon
				IF @DispensingIntegration = 2
				begin

					SELECT
						@FunctionalUnitName = t.x.value('FunctionUnitName[1]','varchar(200)'),
						@OfficeType = t.x.value('OfficeType[1]','int'),
						@LogisticOperator = t.x.value('LogisticOperator[1]','varchar(20)'),
						@MedicalOrderRecipe = t.x.value('MedicalOrderRecipe[1]','varchar(20)')
					FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x)

					--Obtengo la primera unidad funcional que exista
					SELECT TOP 1 @FunctionalUnitId = Id, @FunctionalUnitCode = Code
					FROM Payroll.FunctionalUnit 
					WHERE [State] = @Uno

					--Obtengo la entidad administradora de acuerdo al contrato que tiene el grupo de atencion
					SELECT @HealthAdministratorId = c.HealthAdministratorId,
							@CareGroupId = cg.Id
					FROM #TablePharmaceuticalDetail t
					JOIN [Contract].CareGroup cg  ON t.CareGroupId = cg.Id
					JOIN [Contract].[Contract] c  ON cg.ContractId = c.Id
					JOIN Inventory.CareGroupByCareCenter cgcc  ON cg.Id = cgcc.CareGroupId
					WHERE cgcc.CareCenterCode = @CareCenterCode
					ORDER BY c.HealthAdministratorId

					SELECT @FunctionalUnitHeonId = ISNULL((SELECT TOP 1 Id FROM Inventory.FunctionalUnit  WHERE [Name] = @FunctionalUnitName AND CareCenterCode = @CareCenterCode), 0)
					--Validamos si existe la unidad funcional de integracion con Heon
					IF @FunctionalUnitHeonId = 0 BEGIN
						INSERT INTO Inventory.FunctionalUnit
								([Name], CareCenterCode)
						SELECT @FunctionalUnitName AS [Name], @CareCenterCode AS CareCenterId

						SET @FunctionalUnitHeonId = SCOPE_IDENTITY()
					END

					--Obtengo la primera empresa
					SELECT TOP 1 @CODEMPRES = CODEMPRES
					FROM dbo.ADEMPRESA 

					--Obtengo la primera ubicacion del departamento del centro de atencion
					SELECT TOP 1 @AUUBICACI = u.AUUBICACI
					FROM dbo.INUBICACI u 
					JOIN dbo.ADCENATEN ca  ON u.DEPMUNCOD = ca.DEPMUNCOD
					WHERE ca.CODCENATE = @CareCenterCode

					--Validamos si existe el paciente
					IF NOT EXISTS(SELECT IPCODPACI FROM dbo.INPACIENT WHERE IPCODPACI = @CodePatient) BEGIN
						INSERT INTO dbo.INPACIENT
							   (IPCODPACI, IPTIPODOC, CODIGONIT, IPEXPEDIC, IPPRIAPEL, IPSEGAPEL, IPPRINOMB, IPSEGNOMB, IPNOMCOMP, CODEMPRES, IPTIPOPAC, IPTIPOAFI, CAPACIPAG, AUUBICACI, NIVCODIGO,
							   IPDIRECCI, IPTELEFON, IPTELMOVI, IPFECNACI, CODACTIVI, IPSEXOPAC, IPESTADOC, TIPCOBSAL, ESTADOPAC, INDAUDFOR, NUMCARPET, CODUSUCRE, FECREGCRE, GENCAREGROUP,GENCONENTITY)
							SELECT
								@CodePatient AS INPACIENT,
								6 AS IPTIPODOC,
								@CodePatient AS CODIGONIT,
								[Common].[GETDATE]() AS IPEXPEDIC,
								SUBSTRING(t.x.value('PantientLastName[1]','VARCHAR(150)'), 0, 20) AS IPPRIAPEL,
								SUBSTRING(t.x.value('PantientSecondLastName[1]','VARCHAR(150)'), 0, 20) AS IPSEGAPEL,
								SUBSTRING(t.x.value('PantientFirstName[1]','VARCHAR(150)'), 0, 20) AS IPPRINOMB,
								SUBSTRING(t.x.value('PantientMiddleName[1]','VARCHAR(150)'), 0, 20) AS IPSEGNOMB,
								SUBSTRING(t.x.value('PantientName[1]','VARCHAR(300)'), 0, 250) AS IPNOMCOMP,
								@CODEMPRES AS CODEMPRES,
								1 AS IPTIPOPAC,
								2 AS IPTIPOAFI,
								0 AS CAPACIPAG,
								@AUUBICACI AUUBICACI,
								'01' NIVCODIGO,
								ca.DIRCENATE IPDIRECCI,
								ca.NUMTELCEN IPTELEFON,
								ca.NUMCELCEN IPTELMOVI,
								[Common].[GETDATE]() IPFECNACI,
								'9998' CODACTIVI,
								1 IPSEXOPAC,
								1 IPESTADOC,
								1 TIPCOBSAL,
								1 ESTADOPAC,
								0 INDAUDFOR,
								RIGHT('000000000000000000' + @CodePatient, 25) NUMCARPET,
								@user CODUSUCRE,
								[Common].[GETDATE]() FECREGCRE,
								@CareGroupId,
								@HealthAdministratorId
							FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x)
							JOIN dbo.ADCENATEN ca  ON ca.CODCENATE = @CareCenterCode
					END
				END

				DECLARE @ThirdPartyId AS INT = ISNULL((SELECT TOP 1 Id FROM Common.ThirdParty  WHERE Nit = @CodePatient), 0)
				--Validamos si existe el tercero
				IF @ThirdPartyId = 0 BEGIN
					DECLARE @PersonId AS INT = ISNULL((SELECT TOP 1 Id FROM Common.Person  WHERE IdentificationNumber = @CodePatient), 0)
					--Validamos si existe la persona
					IF @PersonId = 0 BEGIN
						INSERT INTO Common.Person
							   (IdentificationNumber, IdentificationType, FirstLastName, SecondLastName, FirstName, SecondName, State)
							SELECT
								@CodePatient AS IdentificationNumber,
								5 AS IdentificationType,
								SUBSTRING(t.x.value('PantientLastName[1]','VARCHAR(150)'), 0, 50) AS FirstLastName,
								SUBSTRING(t.x.value('PantientSecondLastName[1]','VARCHAR(150)'), 0, 50) AS SecondLastName,
								SUBSTRING(t.x.value('PantientFirstName[1]','VARCHAR(150)'), 0, 50) AS FirstName,
								SUBSTRING(t.x.value('PantientMiddleName[1]','VARCHAR(150)'), 0, 50) AS SecondName,
								1 AS State
							FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x)

						SET @PersonId = SCOPE_IDENTITY()
					END

					INSERT INTO Common.ThirdParty
						   (PersonId, Nit, DigitVerification, Name, PersonType, RetentionType, ContributionType, StateEnterpriseType, IVARetentionAccountPayableConceptId, Ica, IcaPercentage, IcaTop,
						   IcaTopValue, HandlesBranchOffice, State, CreationDate, UserId)
						SELECT
							@PersonId AS PersonId,
							@CodePatient AS Nit,
							NULL AS DigitVerification,
							t.x.value('PantientName[1]','VARCHAR(150)') AS Name,
							1 AS PersonType,
							0 AS RetentionType,
							0 AS ContributionType,
							0 AS StateEnterpriseType,
							NULL AS IVARetentionAccountPayableConceptId,
							0 Ica,
							0 IcaPercentage,
							0 IcaTop,
							0 IcaTopValue,
							0 HandlesBranchOffice,
							1 State,
							[Common].[GETDATE]() CreationDate,
							1 UserId
						FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x)

					SET @ThirdPartyId = SCOPE_IDENTITY()
				END

				--Si el proceso viene de la integracion entre medilaser y farmaQx, se obtienen algunos datos
				if @DispensingIntegration = 3
				begin
					SELECT
						@HealthAdministratorId = t.x.value('HealthAdministratorId[1]','int'),
						@FunctionalUnitId = t.x.value('FunctionalUnitId[1]','int')
					FROM @XmlDispensingIntegrationMedilaser.nodes('/TableXml') t(x)
				end

				UPDATE tpd
					SET
						tpd.FunctionalUnitId = @FunctionalUnitId,
						tpd.HealthAdministratorId = @HealthAdministratorId,
						tpd.ThirdPartyId = @ThirdPartyId,
						tpd.OrderedHealthProfessionalThirdPartyId = IIF( tpd.OrderedHealthProfessionalThirdPartyId IS NULL
																		OR tpd.OrderedHealthProfessionalThirdPartyId =0,
																		@ThirdPartyId,tpd.OrderedHealthProfessionalThirdPartyId)
				FROM #TablePharmaceuticalDetail tpd

				--Validamos si existe el ingreso
				IF NOT EXISTS(SELECT NUMINGRES FROM dbo.ADINGRESO  WHERE NUMINGRES = @AdmissionNumber)
				BEGIN
					--Se inserta el ingreso el ingreso
					INSERT INTO dbo.ADINGRESO
							(NUMINGRES,
							IPCODPACI,
							TIPOINGRE,
							IINGREPOR,
							ITIPORIES,
							ICAUSAING,
							CODENTIDA,
							IFECHAING,
							ILIQUIDAC,
							ICONTROLI,
							CODCENATE,
							UFUCODIGO,
							IESTADOIN,
							IREINGRES,
							UFUACTPAC,
							GENCAREGROUP,
							GENCONENTITY,
							CODUSUCRE,
							FECREGCRE,
							INDAUDFOR)
						SELECT
							@AdmissionNumber AS NUMINGRES,
							@CodePatient IPCODPACI,
							1 TIPOINGRE,
							1 IINGREPOR,
							1 ITIPORIES,
							3 ICAUSAING,
							--@CareCenterCode CODENTIDA,
							'0001' CODENTIDA,
							[Common].[GETDATE]() IFECHAING,
							1 ILIQUIDAC,
							'' ICONTROLI,
							@CareCenterCode CODCENATE,
							@FunctionalUnitCode UFUCODIGO,
							'' IESTADOIN,
							0 IREINGRES,
							@FunctionalUnitCode UFUACTPAC,
							@CareGroupId,
							@HealthAdministratorId,
							@user AS CODUSUCRE,
							[Common].[GETDATE]() FECREGCRE,
							0 AS INDAUDFOR
						FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x)

				END
			END
			ELSE BEGIN
				--- Consultamos indigo crystal para ver si se usa el ingreso de la madre o del hijo (Binomio Madre/Hijo)
				print 'Admision hijo: ' + @AdmissionNumber
				IF EXISTS (SELECT NUMINGRESHIJO FROM dbo.HCINGRESORECNAC  WHERE NUMINGRESHIJO = @AdmissionNumber) BEGIN
					declare @TipoIngresoHijo as tinyint = (SELECT top 1 TIPREGISTRO FROM dbo.HCINGRESORECNAC  WHERE NUMINGRESHIJO = @AdmissionNumber )
					print  'consecutivo farmacia' + @ConsecutivePharmacy + ' - ' + cast(@TipoIngresoHijo as varchar(20))
					IF @TipoIngresoHijo = 1 BEGIN
						--si el tipo de ingreso es de estancia conjunta con la madre
						SELECT @AdmissionNumber = NUMINGRES FROM dbo.HCINGRESORECNAC  WHERE NUMINGRESHIJO = @AdmissionNumber
					END
				END
			END

			--- =========================================================================================================

			--- Actualizo la cabacera de la dispensacion
			update Inventory.PharmaceuticalDispensing set AdmissionNumber = @AdmissionNumber, DocumentDate = @DocumentDate, AffectInventory = @AffectInventory, [Status] = @Status where Id = @IdPharmaceutical

			--- Actualizo los detalles de la dispensacion
			update Inventory.PharmaceuticalDispensingDetail set CareGroupId = pdtmp.CareGroupId
				,HealthAdministratorId = pdtmp.HealthAdministratorId
				,ThirdPartyId = pdtmp.ThirdPartyId
				,ProductId = pdtmp.ProductId
				,WarehouseId = pdtmp.WarehouseId
				,Quantity = pdtmp.Quantity
				,ServiceDate = pdtmp.ServiceDate
				,FunctionalUnitId = pdtmp.FunctionalUnitId
				,OrderedHealthProfessionalCode = pdtmp.OrderedHealthProfessionalCode
				,OrderedProfessionalSpecialty = pdtmp.OrderedProfessionalSpecialty
				,OrderedHealthProfessionalThirdPartyId = pdtmp.OrderedHealthProfessionalThirdPartyId
				,AuthorizationNumber = pdtmp.AuthorizationNumber
				,LiquidationType = pdtmp.LiquidationType
				,SurchargeApply = pdtmp.SurchargeApply
				,SalePrice = pdtmp.SalePrice
				,AverageCost = pdtmp.AverageCost
				,FinalProductCost = pdtmp.FinalProductCost
				,DiscountPercentage = pdtmp.DiscountPercentage
				,DiscountValue = pdtmp.DiscountValue
				,TotalSalesPrice = pdtmp.TotalSalesPrice
				,GrandTotalSalesPrice = pdtmp.GrandTotalSalesPrice
				,QuotationPharmaceuticalDispensingDetailId = pdtmp.QuotationPharmaceuticalDispensingDetailId
				,EntityId = pdtmp.EntityId
				,EntityName = pdtmp.EntityName
				,GrossValue = pdtmp.GrossValue
				,TaxValue =pdtmp.TaxValue
				,IvaId = ip.IVAId
			from #TablePharmaceuticalDetail pdtmp
			inner join Inventory.PharmaceuticalDispensingDetail pd on pd.Id = pdtmp.Id
			left join Inventory.InventoryProduct ip on ip.Id = pd.ProductId and ip.TaxedProduct=1 and ip.LiquidateSalesTaxes=1 -- el iva solo si es producto gravado y liquida iva en ventas
			where pdtmp.Id > @Cero and pdtmp.ChangeTracker <> @Deleted

			update Inventory.PharmaceuticalDispensingDetailBatchSerial
				set PhysicalInventoryId = bat.PhysicalInventoryId
					,Quantity = bat.Quantity
					,OutstandingQuantity = bat.OutstandingQuantity
					,PhysicalInventoryCustodyId = bat.PhysicalInventoryCustodyId
			from #TablePharmaceuticalDetail pdtmp
			inner join #TablePharmaceuticalBatch bat on bat.PharmaceuticalDetailIdTmp = pdtmp.IdTmp
			inner join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbatch on pddbatch.Id = bat.Id
			where pdtmp.Id > @Cero and bat.ChangeTracker <> @Deleted

			--LOGICA DE REGISTRO EN LA HOJA DE GASTO QUIRURGICA
			declare @GuardaGastoQXTmp bit
			declare @IdProgramacionQXPrincipalTmp int
			declare @IdCabeceraHojaGastoQX int
			set @GuardaGastoQXTmp = 0
			SELECT top 1 @GuardaGastoQXTmp = GuardaGastoQX, @IdProgramacionQXPrincipalTmp = IdProgramacionQXPrincipal FROM #TablePharmaceuticalDetail where GuardaGastoQX = 1 AND IdProgramacionQXPrincipal IS NOT NULL
			--si viene para guardar en gasto de paquetes QX procedemos a realizar la insercion
			IF @GuardaGastoQXTmp = 1 BEGIN

				--si hoja de gasto no existe, creamos cabecera
				IF Not Exists(select IDAGEPROGQX from dbo.HCHOJAGASTOQX  where  IDAGEPROGQX = @IdProgramacionQXPrincipalTmp AND NUMINGRES = @AdmissionNumber) BEGIN

					Update dbo.INCONSECU Set @ConsecutiveHojaGastoQX = CONNUMACT += 1 where IDCONSECU = @IdConsecuHojaGastoQX

					INSERT INTO dbo.HCHOJAGASTOQX([CONSECUTIVO],[IPCODPACI],[NUMINGRES],[IDAGEPROGQX],[FECHAREGISTRO],[ESTADO])
						VALUES
							(@ConsecutiveHojaGastoQX
							,@CodePatient
							,@AdmissionNumber
							,@IdProgramacionQXPrincipalTmp
							,@DocumentDate
							,1)

					SET @IdCabeceraHojaGastoQX = SCOPE_IDENTITY()

				END ELSE BEGIN
					SET @IdCabeceraHojaGastoQX = (SELECT TOP 1 ID FROM dbo.HCHOJAGASTOQX  WHERE  IDAGEPROGQX = @IdProgramacionQXPrincipalTmp AND NUMINGRES = @AdmissionNumber)
				END

				update Inventory.PharmaceuticalDispensing
					set SurgeryExpenseSheetId = @IdCabeceraHojaGastoQX,
						EntityId = ISNULL(EntityId, @IdCabeceraHojaGastoQX)
				where  Id = @IdPharmaceutical

				--Validamos que se haya la hoja qx no tenga devolutivos pendientes
					IF EXISTS(	SELECT 1
							FROM HCDEVMEDC AS C 
							INNER JOIN HCDEVMEDD AS D  ON C.CODCONCEC = D.CODCONCEC
							WHERE C.NUMINGRES = @AdmissionNumber
							AND C.IDHCHOJAGASTOQX = @IdCabeceraHojaGastoQX AND C.DEVESTADO = '1'
							AND D.CODPRODUC IN (SELECT td.CodeProduct
												FROM #TablePharmaceuticalDetail td
												WHERE td.ChangeTracker <> @Deleted) ) BEGIN
						set @CodeMessageResult = '999'
						set @ErrorsValidationResult =(SELECT  CONCAT('No se puede dispensar ya que existen devoluciones pendientes por confimar en la Hoja QX. Productos : ',STRING_AGG(D.CODPRODUC,','))
														FROM HCDEVMEDC AS C 
														INNER JOIN HCDEVMEDD AS D  ON C.CODCONCEC = D.CODCONCEC
														WHERE C.NUMINGRES = @AdmissionNumber
														AND C.IDHCHOJAGASTOQX = @IdCabeceraHojaGastoQX AND C.DEVESTADO = '1'
														AND D.CODPRODUC IN (SELECT td.CodeProduct
																			FROM #TablePharmaceuticalDetail td
																			WHERE td.ChangeTracker <> @Deleted))
						set @DispensingIdResult = 0
						set @DispensingCodeResult = ''
						set @StatusResult = 3
						GOTO Cleanup
					END

				Declare @ProductIdTmp int
				Declare @ProductCodeTmp varchar(50)
				Declare @PharmaceuticalDetailIdTmp int
				declare @CantidadEntregada as int
				---------
				Declare @CodProductTmp varchar(20)
				Declare @OriginProduct as int
				Declare @EntityIdTmp  as int
				Declare @EntityNameTmp as varchar(250)
				Declare @PackageId as int
				---------
				Set @Rows = 1
				Set @RowId = 1

				WHILE @Rows > 0
				BEGIN
					Select Top 1
						@RowId = RowId,
						@ProductIdTmp = ProductId,
						@ProductCodeTmp = CodeProduct,
						@CantidadEntregada = Quantity,
						@PharmaceuticalDetailIdTmp =IdTmp,
						@CodProductTmp = CodeProduct,
						@PackageId = PackageId
					From #TablePharmaceuticalDetail
					where Id = 0 And RowId >= @RowId
					Order By RowId

					Set @Rows = @@ROWCOUNT
					If @Rows = 0
						Break

						--saber si viene del paquete qx o si son solicitudes extras realizadas por la enfermera
					SET @OriginProduct = (SELECT TOP 1 IIF(IDETIPHIS IS NULL,1,2) from dbo.HCFARMEPD where CODCONCEC = @ConsecutivePharmacy
											  AND CODPRODUC = @CodProductTmp AND (IDETIPHIS = 'ENFERMER1' OR IDETIPHIS is NULL) AND NUMINGRES = @AdmissionNumber )

					--si no existe producto en la hoja de gasto , procedemos a insertarlo
					IF Not Exists(
									select IDHCHOJAGASTOQX
									from dbo.HCHOJAGASTOQXD
									where  CODPRODUC = @CodProductTmp
										AND IDHCHOJAGASTOQX = @IdCabeceraHojaGastoQX
										AND RequestType = @OriginProduct
										AND ISNULL(@PackageId, 0) = ISNULL(IdNursingPackagesOrder,0)
					)	BEGIN
						/****NursingPackagesOrder*****/
						IF @PackageId IS NULL
						BEGIN
							SELECT top 1 @PackageId = npo.Id
							FROM MedicalHistory.NursingPackagesOrder npo
							join MedicalHistory.NursingPackagesOrderDetail npod on npo.Id= npod.IdNursingPackagesOrder
							WHERE npo.IDHCFARMEPC=@ConsecutiveCrystal AND npod.CODPRODUC =@CodProductTmp and npo.NUMINGRES = @AdmissionNumber
						END
						/********/

						--insertamos las dispenciones nuevas en el detalle de la hoja de gasto
						INSERT INTO dbo.HCHOJAGASTOQXD
						(
							[IDHCHOJAGASTOQX],
							[CODPRODUC],
							[IDPRODUCTO],
							[CANTIDADENTREGADA],
							[CANTIDADGASTADA],
							[CANTIDADDEVOLVER],
							[CANTIDADACEPTADADEV],
							[ORIGENSOLICITUD],
							[FECHAREGISTRO],
							[RequestType],
							[StatusOrder],
							[IdNursingPackagesOrder]
						)
						Select	@IdCabeceraHojaGastoQX
								,CodeProduct
								,null
								,Quantity
								,0--<CANTIDADGASTADA, int,>
								,Quantity--<CANTIDADDEVOLVER, int,>
								,0--<CANTIDADACEPTADADEV>
								,1--Dispensacion
								,@DocumentDate
								,iif(@OriginProduct=2 and @PackageId is not null,3,@OriginProduct ) -- si viene de enfermeria y es paquete es 3
								,1
								,@PackageId
						from #TablePharmaceuticalDetail
						where IdTmp = @PharmaceuticalDetailIdTmp and ChangeTracker <> @Deleted
					END
					ELSE
					BEGIN
						--si el producto ya esta en la hoja de gasto procedemos a actualziar cantidades
						UPDATE dbo.HCHOJAGASTOQXD
							SET [CANTIDADENTREGADA] += dtmp.Quantity,[CANTIDADDEVOLVER] +=dtmp.Quantity
						FROM #TablePharmaceuticalDetail dtmp
						INNER JOIN dbo.HCHOJAGASTOQXD HD ON dtmp.CodeProduct = HD.CODPRODUC AND HD.IDHCHOJAGASTOQX = @IdCabeceraHojaGastoQX
						WHERE dtmp.IdTmp = @PharmaceuticalDetailIdTmp AND ChangeTracker <> @Deleted
							AND HD.CODPRODUC = @ProductCodeTmp
							AND RequestType = @OriginProduct
							AND ISNULL(@PackageId, 0) = ISNULL(IdNursingPackagesOrder,0)
					END

					--- Inserto en el Kardex de crystal
					if @HistoryType = 'ENFERMER1'
					begin
						set @HistoryTypeCrystal = 2
						SET @HistoryTypeNameCrystal = 'Despacho farmacia paquete quirúrgico, origen solicitud: Despacho de farmacia - solicitud de enfermería - usuario: ' + @UserName
					END
					ELSE IF @HistoryType = 'CODIGOAZU'
					begin
						set @HistoryTypeCrystal = 3
						SET @HistoryTypeNameCrystal = 'Despacho farmacia paquete quirúrgico, origen solicitud: Despacho de farmacia - solicitud de emergencia - usuario: ' + @UserName
					END
					ELSE
					BEGIN
						set @HistoryTypeCrystal = 1
						IF ISNULL(@EntityName, '') = 'PharmaceuticalDispensingTransfer'
						BEGIN
							SET @HistoryTypeNameCrystal = CONCAT('Traslado dispensación por ingreso ', @EntityCode, ': Ingreso origen ', @AdmissionNumberOrigin, ' - ingreso destino ', @AdmissionNumber, ' - usuario: ', @UserName)
						END
						ELSE
						BEGIN
							SET @HistoryTypeNameCrystal = 'Despacho farmacia paquete quirúrgico, origen solicitud: Despacho de farmacia - solicitud del médico - usuario: ' + @UserName
						END
					END

					INSERT INTO [dbo].[HCKARDPAC]
					(
						[NUMCONSEC],[IPCODPACI],[NUMINGRES],[CODCENATE],[UFUCODIGO],[CODPROSAL]
						,[CODPRODUC],[CANPRODUCT],[TIPREGIST],[HCPRESCRN],[HCSOLINSN],[HCCTRAPLN]
						,[HCCTRAPLM],[CODDOCUME],[FECREGKAR],[TIPORIREG],[DESMOVPRO],[JUSANULAC]
						,[CONSECFAR],[FECHAUTIL],[OBSERVACI]
					)
					SELECT	NEWID(),@PatientCode,@AdmissionNumber,@CareCenterCode,@FunctionalUnitCode
							,OrderedHealthProfessionalCode,CodeProduct,Quantity,'1',@ConsecutivePescription,@ConsecutiveInputs
							,NULL,NULL,NULL,@GetDateTime,@HistoryTypeCrystal,@HistoryTypeNameCrystal,NULL
							,@ConsecutivePharmacy,NULL,NULL
					from #TablePharmaceuticalDetail
					where IdTmp = @PharmaceuticalDetailIdTmp and ChangeTracker <> @Deleted

					SET @RowId += 1
				END --fin ciclo

				IF Exists
				(

					SELECT TOP 1 *
					FROM dbo.HCHOJAGASTOQX Qx
					JOIN dbo.HCFARMEPD FPD ON QX.NUMINGRES = FPD.NUMINGRES
					WHERE FPD.CODCONCEC =  @ConsecutivePharmacy
						AND QX.NUMINGRES =  @AdmissionNumber AND QX.ID = @IdCabeceraHojaGastoQX
				)
				BEGIN
					UPDATE dbo.HCHOJAGASTOQX
					set ESTADO = 1
					WHERE ID = @IdCabeceraHojaGastoQX
				END

			END --fin guardar gasto hoja QX
			--FIN LOGICA DE REGISTRO EN LA HOJA DE GASTO QUIRURGICA

			set @PharmaceuticalDetailIdTmp = 0
			Set @Rows = 1
			Set @RowId = 1

			While @Rows > 0
			begin

				Select Top 1 @RowId = RowId, @PharmaceuticalDetailIdTmp = IdTmp From #TablePharmaceuticalDetail where Id = 0 And RowId >= @RowId Order By RowId

				Set @Rows = @@ROWCOUNT
				If @Rows = 0
					Break

						/*************************TARIFA DE PRODUCTOS NUEVA**********************/
						IF EXISTS(	SELECT 1
									FROM #TablePharmaceuticalDetail tpd
									JOIN Inventory.InventoryProduct ip  ON tpd.ProductId = ip.Id
									JOIN Contract.CareGroup cg  ON tpd.CareGroupId = cg.Id
									JOIN Inventory.ProductRate pr  ON cg.ProductRateId = pr.Id
									JOIN Inventory.ProductRateGeneral prg  ON prg.ProductRateId = pr.Id
									WHERE tpd.IdTmp = @PharmaceuticalDetailIdTmp
									  AND Common.GETDATE() BETWEEN prg.InitialDate AND prg.EndDate
									  AND (   (prg.RuleType = 1 AND prg.ProductTypeId    = ip.ProductTypeId)
										   OR (prg.RuleType = 2 AND prg.ProductGroupId   = ip.ProductGroupId)
										   OR (prg.RuleType = 3 AND prg.ProductSubGroupId = ip.ProductSubGroupId) )) BEGIN

										declare	@SalePriceIncludeTax BIT = (select top 1 cs.SalePriceIncludeTax from GeneralLedger.CompanySettings cs)
										declare @Value as NUMERIC(20,2)

										declare @RoundPrecision INT = ISNULL((SELECT TOP 1 Common.GetRoundPrecision(c.RoundingType)
																			   FROM GeneralLedger.CompanySettings cs
																			   JOIN Common.Currency c WITH(NOLOCK) ON c.Id = cs.OfficialCurrencyId), 2)

										UPDATE tpd SET
											tpd.GrossValue           = ROUND((ip.FinalProductCost + (ip.FinalProductCost * (COALESCE(a.Percentage, prg.Percentage, 0) / 100))), @RoundPrecision),
											tpd.TaxValue             = 0,
											tpd.TotalSalesPrice      = ROUND((ip.FinalProductCost + (ip.FinalProductCost * (COALESCE(a.Percentage, prg.Percentage, 0) / 100))), @RoundPrecision),
											tpd.GrandTotalSalesPrice = ROUND((ip.FinalProductCost + (ip.FinalProductCost * (COALESCE(a.Percentage, prg.Percentage, 0) / 100))), @RoundPrecision) * tpd.Quantity,
											tpd.DiscountValue        = 0,
											tpd.IvaId                = null,
											tpd.SalePrice            = ROUND((ip.FinalProductCost + (ip.FinalProductCost * (COALESCE(a.Percentage, prg.Percentage, 0) / 100))), @RoundPrecision)
										FROM #TablePharmaceuticalDetail tpd
										JOIN Inventory.InventoryProduct ip  ON tpd.ProductId = ip.Id
										JOIN Contract.CareGroup cg  ON tpd.CareGroupId = cg.Id
										JOIN Inventory.ProductRate pr  ON cg.ProductRateId = pr.Id
										JOIN Inventory.ProductRateGeneral prg  ON prg.ProductRateId = pr.Id
											AND (   (prg.RuleType = 1 AND prg.ProductTypeId    = ip.ProductTypeId)
												 OR (prg.RuleType = 2 AND prg.ProductGroupId   = ip.ProductGroupId)
												 OR (prg.RuleType = 3 AND prg.ProductSubGroupId = ip.ProductSubGroupId) )
										OUTER APPLY (SELECT TOP 1 prgs.Percentage
													 FROM Inventory.ProductRateGeneralCondition prgs 
													 WHERE prgs.ProductRateGeneralId = prg.Id
													   AND (ip.FinalProductCost * tpd.Quantity) BETWEEN prgs.InitialValue AND prgs.EndValue) A
										WHERE tpd.IdTmp = @PharmaceuticalDetailIdTmp
										  AND Common.GETDATE() BETWEEN prg.InitialDate AND prg.EndDate
						END
						/***********************************************************************/


				--- Inserto los detalles de la dispensacion
				INSERT INTO [Inventory].[PharmaceuticalDispensingDetail]
						([PharmaceuticalDispensingId]
						,[CareGroupId]
						,[HealthAdministratorId]
						,[ThirdPartyId]
						,[ProductId]
						,[WarehouseId]
						,[Quantity]
						,[ReturnedQuantity]
						,[ServiceDate]
						,[FunctionalUnitId]
						,[OrderedHealthProfessionalCode]
						,[OrderedProfessionalSpecialty]
						,[OrderedHealthProfessionalThirdPartyId]
						,[AuthorizationNumber]
						,[LiquidationType]
						,[CupsEntityId]
						,[SurchargeApply]
						,[SalePrice]
						,[AverageCost]
						,[DiscountPercentage]
						,[DiscountValue]
						,[TotalSalesPrice]
						,[GrandTotalSalesPrice]
						,[QuotationPharmaceuticalDispensingDetailId]
						,[EntityId]
						,[EntityName]
						,[FinalProductCost]
						,[GrossValue]
						,[TaxValue]
						,[IvaId]
						)
					select @IdPharmaceutical, CareGroupId, HealthAdministratorId, ThirdPartyId, ProductId, WarehouseId, Quantity
						, 0, ServiceDate, FunctionalUnitId, OrderedHealthProfessionalCode, OrderedProfessionalSpecialty, OrderedHealthProfessionalThirdPartyId
						,AuthorizationNumber, LiquidationType, null, SurchargeApply, SalePrice, AverageCost, DiscountPercentage, DiscountValue, TotalSalesPrice, GrandTotalSalesPrice,
						QuotationPharmaceuticalDispensingDetailId, EntityId, EntityName,tpd.FinalProductCost,GrossValue,TaxValue, ip.IVAId
					from #TablePharmaceuticalDetail tpd
					left join Inventory.InventoryProduct ip on ip.Id = tpd.ProductId and ip.TaxedProduct=1 and ip.LiquidateSalesTaxes=1 -- el iva solo si es producto gravado y liquida iva en ventas
					where IdTmp = @PharmaceuticalDetailIdTmp

				declare @IdPharmaceuticalDetail int = SCOPE_IDENTITY()
				update #TablePharmaceuticalDetail set Id = @IdPharmaceuticalDetail where IdTmp = @PharmaceuticalDetailIdTmp

				INSERT INTO Inventory.DeliveriesByPharmacyProductType
				([HCFARMEPDId]
				,[ProductType]
				,[Quantity]
				,[ConcentrationByUnit]
				,[TotalWeight])
				SELECT HCFARMEPDID, ProductType, Quantity, ConcentrationByUnit, TotalWeight FROM #TableDeliveriesByPharmacyProductType
				WHERE PharmaceuticalDispensingDetailIdTmp = @PharmaceuticalDetailIdTmp

				SELECT TOP 1 @AdministrationRoute = hpa.DESADMINI
				FROM #TablePharmaceuticalDetail temp
				JOIN HCFARMEPD hcd ON hcd.ID = temp.EntityId AND temp.EntityName = 'HCFARMEPD'
				JOIN HCPRESCRA hpa ON hpa.ID = hcd.IdSourceTable AND hcd.SourceTable = 'HCPRESCRA'
				WHERE hcd.NUMINGRES = @AdmissionNumber AND hcd.CODPRODUC = temp.MedicamentCode
				AND temp.IdTmp = @PharmaceuticalDetailIdTmp

				/***SEGMENTO AUDITORIA MedicalHistory***/
				INSERT INTO MedicalHistory.TraceabilityDrugs(	[ProductCode],
																[NUMINGRES] ,
																[ProfessionalCode],
																[RegistrationDate],
																[Action],
																[UFUCODIGO],
																[IdSourceTable],
																[SourceTable],
																[AdministrationRoute],
																[ProccesType])

				SELECT	atc.Code,
						@AdmissionNumber,
						@user,
						@GetDateTime,
						16,  -- Farmacia - Entrega de medicamento
						@FunctionalUnitCode,
						@IdPharmaceuticalDetail,
						'PharmaceuticalDispensingDetail',
						@AdministrationRoute,
						CASE h.SourceTable
							WHEN 'HCPRESCRA' THEN 1
							WHEN 'HCINFLIQA' THEN 2
							ELSE 1  -- Por defecto, si es nulo o cualquier otro valor
						END AS ProccesType
				FROM #TablePharmaceuticalDetail temp
				JOIN Inventory.ATC atc  ON temp.MedicamentCode =atc.Code
				JOIN HCFARMEPD h ON h.ID = temp.EntityId
				WHERE IdTmp = @PharmaceuticalDetailIdTmp AND @EntityName = 'SaveDashboardPharmacy' and coalesce(temp.Extramural,0) =0

				/******************************************/
				IF NOT EXISTS ( SELECT 1 FROM #TablePharmaceuticalBatch where PharmaceuticalDetailIdTmp = @PharmaceuticalDetailIdTmp)
				BEGIN
					IF EXISTS (SELECT 1 FROM #TablePharmaceuticalDetail pd JOIN Inventory.Warehouse w ON pd.WarehouseId = w.Id WHERE pd.IdTmp = @PharmaceuticalDetailIdTmp AND w.VirtualStore = 1)
					BEGIN
						DECLARE @physicalInventoryId AS INT
						SET @physicalInventoryId = NULL

						SELECT TOP 1 @physicalInventoryId = phi.Id
						FROM #TablePharmaceuticalDetail pd
						JOIN Inventory.PhysicalInventory phi ON pd.WarehouseId = phi.WarehouseId AND pd.ProductId = phi.ProductId
						WHERE pd.IdTmp = @PharmaceuticalDetailIdTmp

						IF ISNULL(@physicalInventoryId, 0) = 0
						BEGIN
							INSERT INTO [Inventory].[PhysicalInventory]
								([WarehouseId],[ProductId],[BatchSerialId],[Quantity])
								SELECT pd.WarehouseId, pd.ProductId, NULL, 0
								FROM #TablePharmaceuticalDetail pd
								WHERE pd.IdTmp = @PharmaceuticalDetailIdTmp

							SET @physicalInventoryId = SCOPE_IDENTITY()
						END

						INSERT INTO #TablePharmaceuticalBatch
							(ChangeTracker,Id,PharmaceuticalDetailIdTmp,PharmaceuticalDispensingDetailId,PhysicalInventoryId,Quantity,OutstandingQuantity)
							SELECT
								'Add',0,pd.IdTmp,0,@physicalInventoryId,pd.Quantity,pd.Quantity
							FROM #TablePharmaceuticalDetail pd
							WHERE pd.IdTmp = @PharmaceuticalDetailIdTmp
					END
				END
				ELSE IF EXISTS (SELECT 1 FROM #TablePharmaceuticalDetail pd JOIN Inventory.Warehouse w ON pd.WarehouseId = w.Id JOIN #TablePharmaceuticalBatch pdbs ON pd.IdTmp = pdbs.PharmaceuticalDetailIdTmp WHERE pd.IdTmp = @PharmaceuticalDetailIdTmp AND w.VirtualStore = 1 AND ISNULL(pdbs.PhysicalInventoryId, 0) = 0)
				BEGIN
					INSERT INTO [Inventory].[PhysicalInventory]
						([WarehouseId],[ProductId],[BatchSerialId],[Quantity])
					SELECT DISTINCT pd.WarehouseId, pd.ProductId, NULL, 0
					FROM #TablePharmaceuticalDetail pd
					JOIN Inventory.Warehouse w ON pd.WarehouseId = w.Id
					JOIN #TablePharmaceuticalBatch pdbs ON pd.IdTmp = pdbs.PharmaceuticalDetailIdTmp
					LEFT JOIN Inventory.PhysicalInventory phy ON phy.WarehouseId = pd.WarehouseId AND phy.ProductId = pd.ProductId
					WHERE pd.IdTmp = @PharmaceuticalDetailIdTmp
						AND w.VirtualStore = 1
						AND ISNULL(pdbs.PhysicalInventoryId, 0) = 0
						AND phy.Id IS NULL

					UPDATE pdbs SET pdbs.PhysicalInventoryId = phy.Id
					FROM #TablePharmaceuticalDetail pd
					JOIN Inventory.Warehouse w ON pd.WarehouseId = w.Id
					JOIN #TablePharmaceuticalBatch pdbs ON pd.IdTmp = pdbs.PharmaceuticalDetailIdTmp
					JOIN Inventory.PhysicalInventory phy ON phy.WarehouseId = pd.WarehouseId AND phy.ProductId = pd.ProductId
					WHERE pd.IdTmp = @PharmaceuticalDetailIdTmp
						AND w.VirtualStore = 1
						AND ISNULL(pdbs.PhysicalInventoryId, 0) = 0
				END

				INSERT INTO [Inventory].[PharmaceuticalDispensingDetailBatchSerial]
				(
					[PharmaceuticalDispensingDetailId]
					,[PhysicalInventoryId]
					,[Quantity]
					,[OutstandingQuantity]
					,PhysicalInventoryCustodyId
				)
				select @IdPharmaceuticalDetail, CASE WHEN PhysicalInventoryId = 0 THEN NULL ELSE PhysicalInventoryId END, Quantity, OutstandingQuantity
				, CASE WHEN PhysicalInventoryCustodyId = 0 THEN NULL ELSE PhysicalInventoryCustodyId END
				from #TablePharmaceuticalBatch where PharmaceuticalDetailIdTmp = @PharmaceuticalDetailIdTmp

				SET @RowId += 1
			END

			--PRINT 'Eliminamos los detalles'
			--eliminams los detalles
			delete Inventory.PharmaceuticalDispensingDetailBatchSerial
			from #TablePharmaceuticalBatch pddbsTmp
			Inner Join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs On pddbsTmp.Id = pddbs.Id
			Where pddbsTmp.ChangeTracker = @Deleted

			delete Inventory.PharmaceuticalDispensingDetail
			from #TablePharmaceuticalDetail pddTmp
			inner join Inventory.PharmaceuticalDispensingDetail pdd On pddTmp.Id = pdd.Id
			where pddTmp.ChangeTracker = @Deleted

			SET @ExecutionNow = SYSUTCDATETIME()
			SET @ExecutionStepOrder += 1
			INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
				(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
			VALUES
				(@ExecutionId, @ExecutionStepOrder, 'GUARDA_DETALLE_LOTES', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, NULL)
			SET @ExecutionPreviousAt = @ExecutionNow

			--- Si estoy confirmando hago las interfaces
			IF @Status = 2 BEGIN
				set @MessageReturn = 'Se confirmo correctamente la Dispensacion ' + @CodePharmaceutical

				IF @DispensingIntegration = 1
				BEGIN
					declare @ValidationAdmission varchar(20) = @AdmissionNumber
					if @AdmissionNumber <> @AdmissionNumberHijo
					begin
						 set @ValidationAdmission = @AdmissionNumberHijo
					END
					--- Valido si esta desde el dashboard que haya cambiado de unidad funcional
					declare @QuantityStay int = (select count(*) from dbo.CHREGESTA  where cast(FECFINEST as date) <= @InitDate and NUMINGRES = @ValidationAdmission)
					IF @QuantityStay > 1
					BEGIN -- Si el paciente tiene orden de traslado
						if Exists (select ch.CODICAMAS from dbo.CHREGESTA ch 
							Inner Join dbo.CHCAMASHO ca  On ca.CODICAMAS = ch.CODICAMAS
							Where cast(FECFINEST As Date) <= @InitDate And ch.NUMINGRES = @AdmissionNumber And ca.CODCONCEC Is Not Null)
						Begin

							--select '999' as CodeMessage, 'El paciente tiene pendiente una aceptación de medicamentos por motivo de traslado de hospitalización'  as [Message],
								--0 as DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = 'El paciente tiene pendiente una aceptación de medicamentos por motivo de traslado de hospitalización'
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							GOTO Cleanup
						END
						IF NOT EXISTS (SELECT ch.CODICAMAS FROM dbo.CHREGESTA ch 
							INNER JOIN dbo.CHCAMASHO ca  ON ca.CODICAMAS = ch.CODICAMAS
							INNER JOIN dbo.INUNIFUNC uni  ON uni.UFUCODIGO = ca.UFUCODIGO
							WHERE CAST(FECFINEST AS DATE) <= @InitDate AND ch.NUMINGRES = @AdmissionNumber AND uni.UFUTIPUNI = @Diecinueve)
						BEGIN
						--select '999' as CodeMessage, 'El paciente tiene un error en el modulo de hospitalización, está asignado en dos o mas camas'  as [Message],
						--0 as DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
						set @CodeMessageResult = '999'
						set @ErrorsValidationResult = 'El paciente tiene un error en el modulo de hospitalización, está asignado en dos o mas camas'
						set @DispensingIdResult = 0
						set @DispensingCodeResult = ''
						set @StatusResult = 3
						GOTO Cleanup
						END
					END
				END
				--- Valido que exista parametros de inventarios por unidad funcional
				if Not Exists (select Id from Inventory.SettingInventory  where OperatingUnitId = @OperatingUnitId) begin
					--select '999' as CodeMessage, 'No se encontraron parametros de inventarios creados para la unidad operativa ' +
						--isnull((select UnitName from Common.OperatingUnit  where Id = @OperatingUnitId),'') as Message,
						--0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
					set @CodeMessageResult = '999'
					set @ErrorsValidationResult = 'No se encontraron parametros de inventarios creados para la unidad operativa'
					set @DispensingIdResult = 0
					set @DispensingCodeResult = ''
					set @StatusResult = 3
					GOTO Cleanup
				END

				declare @SettingInventoryId int,
						@AssociateCostCenter tinyint,
						@AssociateCostMainAccount tinyint

				SELECT
					@SettingInventoryId = Id,
					@AssociateCostCenter = AssociateCostCenter,
					@AssociateCostMainAccount = AssociateCostMainAccount
				FROM Inventory.SettingInventory 
				WHERE OperatingUnitId = @OperatingUnitId

				declare @TableServiceOrder table(Code varchar(20) NOT NULL,AdmissionNumber varchar(20), PatientCode varchar(25) NOT NULL,OrderDate datetime NOT NULL
					,[AffectInventory] [bit] NOT NULL
					, EntityCode varchar(20) NULL, EntityId int NULL
					, EntityName varchar(250) NULL,OperatingUnitId int NOT NULL,[Status] tinyint NOT NULL)
				declare @TableServiceOrderDetail table(RowXml int identity(1,1)
					,CareGroupId int NOT NULL
					,HealthAdministratorId int NULL
					,ThirdPartyId int NULL
					,ServiceType tinyint NOT NULL
					,RecordType tinyint NOT NULL
					,CUPSEntityId int NULL
					,IPSServiceId int NULL
					,HospitalStayId int NULL
					,HospitalStayDetailId int NULL
					,ControlExternalConsultation tinyint NULL
					,ControlExternalConsultationCode numeric(18, 0) NULL
					,CUPSAssociateService bit NOT NULL
					,CodeAssociateService varchar(50) NULL
					,IsPackage bit NOT NULL
					,Packaging bit NOT NULL
					,PackageServiceOrderDetailId int NULL
					,LiquidationType tinyint NOT NULL
					,Presentation tinyint NULL,ProductId int NULL,InvoicedQuantity int NOT NULL,SupplyQuantity int NOT NULL
					,DevolutionQuantity int NOT NULL,RateManualSalePrice numeric(20, 2) NOT NULL,CostValue numeric(20, 2) NOT NULL
					,ServiceDate datetime NOT NULL,AuthorizationNumber varchar(20) NULL,PerformsFunctionalUnitId int NOT NULL
					,PerformsHealthProfessionalCode char(20) NULL,PerformsProfessionalSpecialty char(3) NULL,PerformsHealthProfessionalThirdPartyId int NULL
					,BillingConceptId int NULL,CostCenterId int NOT NULL,SettlementType tinyint NOT NULL,IncludeServiceOrderDetailId int NULL
					,RecoveryRatio numeric(5, 2) NULL,RateManualId int NULL,RateManualType tinyint NULL,RateManualDetailId int NULL
					,DefinitionRateDetailId int NULL,DefinitionRateDetailConditionId int NULL,SubTotalSalesPrice numeric(20, 2) NOT NULL
					,ThirdPartyDiscount numeric(20, 2) NOT NULL,ThirdPartyDiscountPercentage numeric(5, 2) NOT NULL,TotalSalesPrice numeric(20, 2) NOT NULL
					,GrandTotalSalesPrice numeric(20, 2) NOT NULL,SurchargeApply bit NOT NULL,SurgicalInterventionType tinyint NULL
					,SurgeryNumber tinyint NOT NULL,IsFirstEvent bit NOT NULL,IsAnnulled bit NOT NULL,IsDelete bit NOT NULL,IncomeMainAccountId int NOT NULL
					,FinalProductCost DECIMAL(20,2) NOT NULL, GrossValue NUMERIC(20,2), TaxValue NUMERIC(20,2), EconomicActivityId INT)

				UPDATE tpd SET
					tpd.GrossValue = tpd.GrossValue + tpd.TaxValue,
					tpd.TaxValue   = 0
				FROM #TablePharmaceuticalDetail tpd
				JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON ip.Id = tpd.ProductId
				WHERE NOT (ip.TaxedProduct = 1 AND ip.LiquidateSalesTaxes = 1)
				  AND tpd.TaxValue > 0

				IF EXISTS (SELECT 1
							FROM #TablePharmaceuticalDetail tpd
							JOIN Inventory.InventoryProduct ip  on  tpd.ProductId =ip.Id
							JOIN Inventory.ProductType pt  on ip.ProductTypeId= pt.Id
							WHERE PhysicalInventoryCustodyId is NULL AND pt.Class <> 5)
				BEGIN
					--- inserto la cabecera de la orden de servicio tmp
					insert into @TableServiceOrder values ('', @AdmissionNumber, @PatientCode, @DocumentDate, @AffectInventory, @CodePharmaceutical, @IdPharmaceutical, 'PharmaceuticalDispensing', @OperatingUnitId, 1)

					--- Ahora inserto los detalles de la orden de servicio
					insert into @TableServiceOrderDetail (CareGroupId,HealthAdministratorId,ThirdPartyId,ServiceType,RecordType,CUPSEntityId,IPSServiceId,HospitalStayId ,HospitalStayDetailId
							,ControlExternalConsultation,ControlExternalConsultationCode,CUPSAssociateService,CodeAssociateService,IsPackage,Packaging,PackageServiceOrderDetailId
							,LiquidationType,Presentation,ProductId,InvoicedQuantity,SupplyQuantity,DevolutionQuantity,RateManualSalePrice,CostValue
							,ServiceDate,AuthorizationNumber,PerformsFunctionalUnitId,PerformsHealthProfessionalCode,PerformsProfessionalSpecialty
							,PerformsHealthProfessionalThirdPartyId,BillingConceptId,CostCenterId
							,SettlementType,IncludeServiceOrderDetailId,RecoveryRatio,RateManualId,RateManualType,RateManualDetailId,DefinitionRateDetailId,DefinitionRateDetailConditionId
							,SubTotalSalesPrice,ThirdPartyDiscount,ThirdPartyDiscountPercentage,TotalSalesPrice,GrandTotalSalesPrice,SurchargeApply,SurgicalInterventionType,SurgeryNumber,IsFirstEvent,IsAnnulled,IsDelete,IncomeMainAccountId
							,FinalProductCost, GrossValue, TaxValue,EconomicActivityId)
						select CareGroupId, HealthAdministratorId, pd.ThirdPartyId, 0,2,null,null,null,null,
						null, null, 0, null,0,0,null,
						5,null, ProductId, Quantity, Quantity, 0, SalePrice, AverageCost,
						ServiceDate, AuthorizationNumber, FunctionalUnitId, OrderedHealthProfessionalCode, OrderedProfessionalSpecialty,
						OrderedHealthProfessionalThirdPartyId, null, case @AssociateCostCenter when 1 then ISNULL(pd.CostCenterId,f.CostCenterId) when 2 then pg.CostCenterId when 3 then w.CostCenterId end,
						1,null, null, null,null,null,null,null,
						SalePrice, DiscountValue, DiscountPercentage, TotalSalesPrice, GrandTotalSalesPrice, SurchargeApply, null, 0, 1, 0,0, pg.IncomeAccountId
						,pd.FinalProductCost, pd.GrossValue, pd.TaxValue,pg.EconomicActivityId
						from #TablePharmaceuticalDetail pd
						left join Payroll.FunctionalUnit f on pd.FunctionalUnitId = f.Id
						join Inventory.InventoryProduct p on p.Id = pd.ProductId
						join Inventory.ProductGroup pg on pg.Id = p.ProductGroupId
						join Inventory.Warehouse w on w.Id = pd.WarehouseId
						JOIN Inventory.ProductType pt on p.ProductTypeId = pt.Id
						where pd.ChangeTracker <> @Deleted and pt.Class <> 5
						AND pd.PhysicalInventoryCustodyId is NULL

					 --- Realizo la interfaz con Ordenes de servicios -- Facturacion
					declare @OrderXml xml = (
					select  0 as Id, ServiceOrder.Code, ServiceOrder.AdmissionNumber, ServiceOrder.PatientCode, ServiceOrder.OrderDate, ServiceOrder.AffectInventory, ServiceOrder.EntityCode, ServiceOrder.EntityId, ServiceOrder.EntityName, ServiceOrder.OperatingUnitId, ServiceOrder.Status
							,ServiceOrderDetail.RowXml, 0 as Id, ServiceOrderDetail.CareGroupId, ServiceOrderDetail.HealthAdministratorId, ServiceOrderDetail.ThirdPartyId, ServiceOrderDetail.ServiceType, ServiceOrderDetail.RecordType, ServiceOrderDetail.CUPSEntityId, ServiceOrderDetail.IPSServiceId, ServiceOrderDetail.HospitalStayId, ServiceOrderDetail.HospitalStayDetailId
							,ServiceOrderDetail.ControlExternalConsultation, ServiceOrderDetail.ControlExternalConsultationCode, ServiceOrderDetail.CUPSAssociateService, ServiceOrderDetail.CodeAssociateService, ServiceOrderDetail.IsPackage, ServiceOrderDetail.Packaging, ServiceOrderDetail.PackageServiceOrderDetailId, ServiceOrderDetail.LiquidationType
							,ServiceOrderDetail.Presentation, ServiceOrderDetail.ProductId, ServiceOrderDetail.InvoicedQuantity, ServiceOrderDetail.SupplyQuantity, ServiceOrderDetail.DevolutionQuantity, ServiceOrderDetail.RateManualSalePrice, ServiceOrderDetail.CostValue, ServiceOrderDetail.ServiceDate, ServiceOrderDetail.AuthorizationNumber, ServiceOrderDetail.PerformsFunctionalUnitId
							,ServiceOrderDetail.PerformsHealthProfessionalCode, ServiceOrderDetail.PerformsProfessionalSpecialty, ServiceOrderDetail.PerformsHealthProfessionalThirdPartyId, ServiceOrderDetail.BillingConceptId, ServiceOrderDetail.CostCenterId, ServiceOrderDetail.SettlementType, ServiceOrderDetail.IncludeServiceOrderDetailId, ServiceOrderDetail.RecoveryRatio
							,ServiceOrderDetail.RateManualId, ServiceOrderDetail.RateManualType, ServiceOrderDetail.RateManualDetailId, ServiceOrderDetail.DefinitionRateDetailId, ServiceOrderDetail.DefinitionRateDetailConditionId, ServiceOrderDetail.SubTotalSalesPrice, ServiceOrderDetail.ThirdPartyDiscount, ServiceOrderDetail.ThirdPartyDiscountPercentage
							,ServiceOrderDetail.TotalSalesPrice, ServiceOrderDetail.GrandTotalSalesPrice, ServiceOrderDetail.SurchargeApply, ServiceOrderDetail.SurgicalInterventionType, ServiceOrderDetail.SurgeryNumber, ServiceOrderDetail.IsFirstEvent, ServiceOrderDetail.IsAnnulled, ServiceOrderDetail.IsDelete, ServiceOrderDetail.IncomeMainAccountId, 'Added' as EntityState
							,ServiceOrderDetail.FinalProductCost, ServiceOrderDetail.GrossValue, ServiceOrderDetail.TaxValue,ServiceOrderDetail.EconomicActivityId
						from @TableServiceOrder as ServiceOrder
						CROSS APPLY @TableServiceOrderDetail as ServiceOrderDetail
						For Xml Auto, Elements)

					DECLARE @IESTADOIN CHAR(1)

					SET @ExecutionNow = SYSUTCDATETIME()
					SET @ExecutionStepOrder += 1
					INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
						(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
					VALUES
						(@ExecutionId, @ExecutionStepOrder, 'CONFIRMACION_PREPARA_ORDEN_SERVICIO', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, CONCAT('Integracion=', @DispensingIntegration))
					SET @ExecutionPreviousAt = @ExecutionNow

					SELECT @IESTADOIN = IESTADOIN
					FROM dbo.ADINGRESO 
					WHERE NUMINGRES = @AdmissionNumber

					IF @DispensingIntegration = 1
					BEGIN
						IF EXISTS (SELECT 1 FROM #TablePharmaceuticalDetail WHERE Extramural = 1)
						BEGIN
							IF @IESTADOIN = 'A'
							BEGIN
								SET @CodeMessageResult = '999'
								SET @ErrorsValidationResult = 'No se puede generar la dispensación en el ingreso actual porque se encuentra anulado'
								SET @DispensingIdResult = 0
								SET @DispensingCodeResult = ''
								SET @StatusResult = 3
								GOTO Cleanup
							END
							ELSE IF @IESTADOIN = 'F'
							BEGIN
								UPDATE ai
									SET ai.IESTADOIN = 'P'
								FROM dbo.ADINGRESO ai
								WHERE ai.NUMINGRES = @AdmissionNumber
							END
							ELSE IF @IESTADOIN = 'C'
							BEGIN
								UPDATE ai
									SET ai.IESTADOIN = ''
								FROM dbo.ADINGRESO ai
								WHERE ai.NUMINGRES = @AdmissionNumber
							END
						END
					END

					DECLARE @CodeResultSo VARCHAR(3),
					@MessageResultSo VARCHAR(MAX),
					@StatusResultSo TINYINT,
					@IdSo INT

					EXEC Billing.SP_GenerateServiceOrder_Output
													@ServiceOrderXml = @OrderXml,
													@User = @User,
													--Salidas
													@CodeResult = @CodeResultSo OUTPUT,
													@MessageResult = @MessageResultSo OUTPUT,
													@StatusResult = @StatusResultSo OUTPUT,
													@Id = @IdSo OUTPUT,
													@ExecutionId = @ExecutionId

					SET @ExecutionNow = SYSUTCDATETIME()
					SET @ExecutionStepOrder += 1
					INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
						(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
					VALUES
						(@ExecutionId, @ExecutionStepOrder, 'CONFIRMACION_ORDEN_SERVICIO', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, CONCAT('EstadoOrdenServicio=', @StatusResultSo, ';IdOrdenServicio=', @IdSo))
					SET @ExecutionPreviousAt = @ExecutionNow

					if @StatusResultSo = @Tres begin
						--select CodeMessage, Message, 0 as DispensingId, '' as DispensingCode, [Status] from @TableResultOrder
						set @CodeMessageResult = @CodeResultSo
						set @ErrorsValidationResult = @MessageResultSo
						set @DispensingIdResult = 0
						set @DispensingCodeResult = ''
						set @StatusResult = @StatusResultSo
						GOTO Cleanup
					END

					SET @MessageReturn += ', ' + @MessageResultSo
				END

				--- Ahora Afecto el Inventario, Kardex y Contabilidad
				if @AffectInventory = 1 begin
					declare @KardexXml xml = (select null as ThirdPartyId, phy.ProductId, phy.BatchSerialId, 2 as MovementType,
						phy.WarehouseId, bat.Quantity, ip.ProductCost as Value, 0 as AffectAverageCost
					FROM #TablePharmaceuticalDetail pd
					inner join Inventory.Warehouse w  ON pd.WarehouseId = w.Id
					inner join #TablePharmaceuticalBatch bat on pd.IdTmp = bat.PharmaceuticalDetailIdTmp
					inner join Inventory.PhysicalInventory phy  on phy.Id = bat.PhysicalInventoryId
					inner join Inventory.InventoryProduct ip  on ip.Id = pd.ProductId
					where w.VirtualStore = @Cero AND pd.ChangeTracker <> @Deleted AND bat.ChangeTracker <> @Deleted
						and pd.PhysicalInventoryCustodyId is NULL
					for xml path('Kardex'), elements)

					SET @ExecutionNow = SYSUTCDATETIME()
					SET @ExecutionStepOrder += 1
					INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
						(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
					VALUES
						(@ExecutionId, @ExecutionStepOrder, 'CONFIRMACION_PREPARA_KARDEX_FISICO', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, CONCAT('TieneXml=', IIF(@KardexXml IS NULL, 0, 1)))
					SET @ExecutionPreviousAt = @ExecutionNow

					IF @KardexXml IS NOT NULL
					BEGIN

						declare @TableResultKardex table(CodeMessage varchar(20), [Message] varchar(1000), [Status] tinyint)
						--select @KardexXml, @IdPharmaceutical, @CodePharmaceutical,@User
						insert into @TableResultKardex

							exec [Inventory].[SP_SavePhysicalInventoryKardex] @KardexXml, @IdPharmaceutical, @CodePharmaceutical, 'PharmaceuticalDispensing', @User

						SET @ExecutionNow = SYSUTCDATETIME()
						SET @ExecutionStepOrder += 1
						INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
							(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
						VALUES
							(@ExecutionId, @ExecutionStepOrder, 'CONFIRMACION_KARDEX_FISICO', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, NULL)
						SET @ExecutionPreviousAt = @ExecutionNow

						IF EXISTS (SELECT CodeMessage FROM @TableResultKardex WHERE [Status] = @Tres) BEGIN
							--select '999' as CodeMessage,[Message], 0 as DispensingId, '' as DispensingCode, [Status] from @TableResultKardex
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = (select Message from @TableResultKardex)
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = (select Status from @TableResultKardex)
							GOTO Cleanup
						END
					END

					declare @KardexXmlCustody xml = (select null as ThirdPartyId, pd.ProductId, phy.BatchSerialId, 2 as MovementType,
						pd.WarehouseId, bat.Quantity, ip.ProductCost as Value, 0 as AffectAverageCost
					FROM #TablePharmaceuticalDetail pd
					inner join Inventory.Warehouse w  ON pd.WarehouseId = w.Id
					inner join #TablePharmaceuticalBatch bat on pd.IdTmp = bat.PharmaceuticalDetailIdTmp
					inner join Inventory.PhysicalInventoryCustody phy  on phy.Id = bat.PhysicalInventoryCustodyId
					inner join Inventory.InventoryProduct ip  on ip.Id = pd.ProductId
					where w.CustodyStore = @Uno AND pd.ChangeTracker <> @Deleted AND bat.ChangeTracker <> @Deleted
						and pd.PhysicalInventoryCustodyId IS NOT NULL
					for xml path('Kardex'), elements)

					SET @ExecutionNow = SYSUTCDATETIME()
					SET @ExecutionStepOrder += 1
					INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
						(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
					VALUES
						(@ExecutionId, @ExecutionStepOrder, 'CONFIRMACION_PREPARA_KARDEX_CUSTODIA', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, CONCAT('TieneXml=', IIF(@KardexXmlCustody IS NULL, 0, 1)))
					SET @ExecutionPreviousAt = @ExecutionNow


					IF @KardexXmlCustody IS NOT NULL
					BEGIN
					declare @TableResultKardexCustody table(CodeMessage varchar(20), [Message] varchar(1000), [Status] tinyint)
					insert into @TableResultKardexCustody
						exec [Inventory].[SP_SavePhysicalInventoryCustodyKardexCustody] @KardexXmlCustody, @AdmissionNumber, @IdPharmaceutical, @CodePharmaceutical, 'PharmaceuticalDispensing', @User

						SET @ExecutionNow = SYSUTCDATETIME()
						SET @ExecutionStepOrder += 1
						INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
							(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
						VALUES
							(@ExecutionId, @ExecutionStepOrder, 'CONFIRMACION_KARDEX_CUSTODIA', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, NULL)
						SET @ExecutionPreviousAt = @ExecutionNow

						IF EXISTS (SELECT CodeMessage FROM @TableResultKardexCustody WHERE [Status] = @Tres) BEGIN
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = (select Message from @TableResultKardexCustody)
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = (select Status from @TableResultKardexCustody)
							GOTO Cleanup
						END
					END

					--- Actualizo las remisiones de inventario en consignación si las hubiera
					declare @RemissionXml xml = (select DISTINCT pd.Id AS EntityDetailId, @OperatingUnitId AS OperatingUnitId, pd.FunctionalUnitId, pd.ProductId, phy.BatchSerialId, 2 as MovementType, pd.WarehouseId, bat.Quantity, ip.ProductCost as Value
						FROM #TablePharmaceuticalDetail pd
						INNER JOIN #TablePharmaceuticalBatch bat on pd.IdTmp = bat.PharmaceuticalDetailIdTmp
						INNER JOIN Inventory.PhysicalInventory phy  on phy.Id = bat.PhysicalInventoryId
						INNER JOIN Inventory.InventoryProduct ip  on ip.Id = pd.ProductId
						INNER JOIN Inventory.Warehouse w  ON pd.WarehouseId = w.Id
						left JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs  ON cirdbs.BatchSerialId = phy.BatchSerialId
						WHERE w.WarehouseConsignment = @Uno AND pd.ChangeTracker <> @Deleted and bat.ChangeTracker <> @Deleted
						for xml path('Remission'), elements)

					SET @ExecutionNow = SYSUTCDATETIME()
					SET @ExecutionStepOrder += 1
					INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
						(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
					VALUES
						(@ExecutionId, @ExecutionStepOrder, 'CONFIRMACION_PREPARA_REMISION_CONSIGNACION', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, CONCAT('TieneXml=', IIF(@RemissionXml IS NULL, 0, 1)))
					SET @ExecutionPreviousAt = @ExecutionNow

					IF @RemissionXml IS NOT NULL BEGIN
						DECLARE @MessageReturnRemission VARCHAR(MAX)
						EXEC [Inventory].[SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission] @RemissionXml, @IdPharmaceutical, @CodePharmaceutical, 'PharmaceuticalDispensing', @User, @MessageReturnRemission OUTPUT

						SET @ExecutionNow = SYSUTCDATETIME()
						SET @ExecutionStepOrder += 1
						INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
							(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
						VALUES
							(@ExecutionId, @ExecutionStepOrder, 'CONFIRMACION_REMISION_CONSIGNACION', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, IIF(ISNULL(@MessageReturnRemission, '') = '', NULL, @MessageReturnRemission))
						SET @ExecutionPreviousAt = @ExecutionNow

						IF ISNULL(@MessageReturnRemission, '') <> '' BEGIN
							--select '999' as CodeMessage, @MessageReturnRemission [Message], 0 as DispensingId, '' as DispensingCode, CAST(3 AS TINYINT) [Status]
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = @MessageReturnRemission
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							GOTO Cleanup
						END
					END

					IF @DispensingIntegration = 1 BEGIN
						--- Ahora afecto contabilidad
						if Exists (select NUMINGRES from dbo.ADINGRESO  where NUMINGRES = @AdmissionNumber and GENCONENTITY is null) begin --- Valido que tenga entidad administradora
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = 'El ingreso '+ @AdmissionNumber +' no tiene asignada una entidad administradora de salud'
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							GOTO Cleanup
						END
						--- Valido que la entidad administradora este en la tabla
						IF NOT EXISTS (SELECT Id FROM [Contract].HealthAdministrator  WHERE Id = (SELECT GENCONENTITY FROM dbo.ADINGRESO  WHERE NUMINGRES = @AdmissionNumber)) BEGIN
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = 'No se encontro entidad administradora para el ingreso ' + @AdmissionNumber
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							GOTO Cleanup
						END
					END

					SET @ExecutionNow = SYSUTCDATETIME()
					SET @ExecutionStepOrder += 1
					INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
						(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
					VALUES
						(@ExecutionId, @ExecutionStepOrder, 'CONFIRMACION_VALIDACIONES_CONTABLES', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, CONCAT('Integracion=', @DispensingIntegration))
					SET @ExecutionPreviousAt = @ExecutionNow

					IF EXISTS
					(
						SELECT 1
						FROM #TablePharmaceuticalDetail pd
						JOIN Inventory.Warehouse w  ON pd.WarehouseId = w.Id
						WHERE PhysicalInventoryCustodyId IS NULL AND w.ControlStore = @Cero
					)
					BEGIN

						declare @TableJournalVoucher table(IdJournalVoucher int NOT NULL, VoucherDate datetime NOT NULL, Imported bit NOT NULL, [Status] tinyint NOT NULL, Detail varchar(500) NULL, EntityCode varchar(20) NULL, EntityId int NULL, EntityName varchar(250) NULL, IsClosedYear bit NOT NULL)
						declare @TableJournalVoucherDetail table(IdMainAccount int NOT NULL, IdThirdParty int NULL, IdCostCenter int NULL,DebitValue decimal(18, 2) NOT NULL,CreditValue decimal(18, 2) NOT NULL,Detail varchar(max) NULL,IdRetention int NULL,RetentionRate decimal(5, 2) NULL,BaseValue decimal(18, 0) NULL,BillingValue decimal(18, 0) NULL)
						declare @SalesJournalVoucherTypeId int, @CreditThirdPartyId int, @PharmaceuticalDispensingGetThirdParty tinyint, @PatientThirdPartyId int , @HealAdministratorAdmissionThirdPartyId int
						select @SalesJournalVoucherTypeId = SalesJournalVoucherTypeId, @PharmaceuticalDispensingGetThirdParty = PharmaceuticalDispensingGetThirdParty from Inventory.SettingInventory where OperatingUnitId = @OperatingUnitId

						declare @NitPatient varchar(25) = @PatientCode
						IF @DispensingIntegration = 1 BEGIN
							SET @NitPatient = (select IPCODPACI from dbo.ADINGRESO  where NUMINGRES = @AdmissionNumber)
							SET @HealAdministratorAdmissionThirdPartyId = (SELECT ThirdPartyId FROM [Contract].HealthAdministrator 
								WHERE Id = (SELECT GENCONENTITY FROM dbo.ADINGRESO  WHERE NUMINGRES = @AdmissionNumber))
						END

						set @PatientThirdPartyId = (select Id from Common.ThirdParty  where Nit = @NitPatient)
						IF ISNULL(@PatientThirdPartyId, 0) = 0 begin
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = 'El paciente con identificacion ' + @NitPatient + ' no existe como tercero en Indigo Vie'
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							GOTO Cleanup
						END

						IF @DispensingIntegration = 2 or @DispensingIntegration = 3 BEGIN
							SET @HealAdministratorAdmissionThirdPartyId = @PatientThirdPartyId
						END

						if @PharmaceuticalDispensingGetThirdParty = 1 begin -- Tomar Tercero del paciente
							set @CreditThirdPartyId = @PatientThirdPartyId
						end
						ELSE BEGIN --- Tercero Especifico
							SET @CreditThirdPartyId = (SELECT PharmaceuticalDispensingThirdPartyId FROM Inventory.SettingInventory  WHERE OperatingUnitId = @OperatingUnitId)
						END

						---Inserto la cabecera del comprobante
						insert into @TableJournalVoucher
							values (@SalesJournalVoucherTypeId, @DocumentDate, 0, 2, 'Comprobante generado por la dispensacion ' + @CodePharmaceutical, @CodePharmaceutical, @IdPharmaceutical, 'PharmaceuticalDispensing', 0)

						--- Valido que los productos tengan grupo y el costo sea mayor a 0
						if Exists (select pd.RowId from #TablePharmaceuticalDetail pd
							Inner Join Inventory.InventoryProduct ip  On pd.ProductId = ip.Id
							Where ip.ProductGroupId is null And pd.ChangeTracker <> @Deleted
							AND pd.PhysicalinventoryCustodyId is NULL) Begin

							declare @errors varchar(MAX)
							select @errors=stuff((select N'; El producto ' + ip.Code	+ ' - ' + ip.Name + ' no tiene un grupo asociado'
							from #TablePharmaceuticalDetail pd inner join Inventory.InventoryProduct ip  on pd.ProductId = ip.Id where ip.ProductGroupId is null and pd.ChangeTracker <> @Deleted AND pd.PhysicalinventoryCustodyId is NULL
							for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
							--select '999' as CodeMessage, @errors as [Message], 0 as DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = @errors
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							GOTO Cleanup
						END

						if Exists (select pd.RowId from #TablePharmaceuticalDetail pd
							inner join Inventory.InventoryProduct ip on pd.ProductId = ip.Id
							where isnull(ip.ProductCost,0) = 0 and pd.ChangeTracker<>@Deleted
							AND pd.PhysicalinventoryCustodyId is NULL) begin

							declare @errorsCostZero varchar(MAX)
							select @errorsCostZero=stuff((select N'; El producto ' + ip.Code	+ ' - ' + ip.Name + ' tiene un costo de 0'
							from #TablePharmaceuticalDetail pd inner join Inventory.InventoryProduct ip  on pd.ProductId = ip.Id where isnull(ip.ProductCost,0) = 0 and pd.ChangeTracker <> @Deleted AND pd.PhysicalinventoryCustodyId is NULL
							for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
							--select '999' as CodeMessage, @errorsCostZero as [Message], 0 as DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = @errorsCostZero
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							GOTO Cleanup
						END

						IF EXISTS
						(
							SELECT 1
							FROM #TablePharmaceuticalDetail pdd
							JOIN Inventory.InventoryProduct ip  ON ip.Id = pdd.ProductId
							LEFT JOIN Inventory.SettingInventoryFunctionalUnit sifu  ON @AssociateCostMainAccount = 1 AND sifu.SettingInventoryId = @SettingInventoryId AND pdd.FunctionalUnitId = sifu.FunctionalUnitId
							LEFT JOIN Inventory.ProductGroupFunctionalUnit pgfu  ON @AssociateCostMainAccount = 2 AND ip.ProductGroupId = pgfu.ProductGroupId AND pdd.FunctionalUnitId = pgfu.FunctionalUnitId
							WHERE pdd.ChangeTracker <> @Deleted
								AND PhysicalinventoryCustodyId is NULL
								AND ISNULL(sifu.Id, pgfu.Id) IS NULL
						) BEGIN
							declare @errorsFunctional varchar(MAX)
							select @errorsFunctional = stuff((
								SELECT DISTINCT N'; La unidad funcional ' + fu.Code	+ ' - ' + fu.Name + ' no esta parametrizada en ' +
									IIF(@AssociateCostMainAccount = 1, 'los parametros de inventarios', CONCAT('el grupo de producto ', pg.Code, ' - ', pg.Name))
								FROM #TablePharmaceuticalDetail pdd
								JOIN Payroll.FunctionalUnit fu  ON pdd.FunctionalUnitId = fu.Id
								JOIN Inventory.InventoryProduct ip  ON ip.Id = pdd.ProductId
								JOIN Inventory.ProductGroup pg  ON ip.ProductGroupId = pg.Id
								LEFT JOIN Inventory.SettingInventoryFunctionalUnit sifu  ON @AssociateCostMainAccount = 1 AND sifu.SettingInventoryId = @SettingInventoryId AND pdd.FunctionalUnitId = sifu.FunctionalUnitId
								LEFT JOIN Inventory.ProductGroupFunctionalUnit pgfu  ON @AssociateCostMainAccount = 2 AND ip.ProductGroupId = pgfu.ProductGroupId AND pdd.FunctionalUnitId = pgfu.FunctionalUnitId
								WHERE pdd.ChangeTracker <> @Deleted
									AND PhysicalinventoryCustodyId is NULL
									AND ISNULL(sifu.Id, pgfu.Id) IS NULL
							for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = @errorsFunctional
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							GOTO Cleanup
						END

						/* validamos si el producto sale de un almacen en consigna, si parametro costeo inv en consigna esta activo y que exista ese producto en la tabla de ConsignmentCostListDetail*/

						declare
								@CostNew as decimal(12,2),
								@flagCostNew as BIT = 0
						SELECT
							@CostNew = ccld.CostNew,
							@FlagCostNew = iif(w.WarehouseConsignment = 1 and s.ConsignmentInventoryCosting = 1 and ccld.CostNew is not NULL , 1,0)
						FROM Inventory.Warehouse w
						join #TablePharmaceuticalDetail tmppd on tmppd.WarehouseId = w.Id
						JOIN Common.Supplier s  ON w.SupplierId = s.Id
						JOIN Inventory.ConsignmentCostList ccl on ccl.SupplierId = w.SupplierId AND ccl.OperatingUnitId = @OperatingUnitId
						join Inventory.ConsignmentCostListDetail ccld on ccld.ConsignmentCostListId = ccl.id and ccld.ProductId = tmppd.ProductId
						where w.id = tmppd.WarehouseId

						/**** DETALLE CREDITO ****/
							---- (inventario de almacenes en consignación)
							INSERT INTO @TableJournalVoucherDetail
								SELECT
									ma.Id,
									CASE ma.HandlesThirdParty WHEN 1 THEN s.IdThirdParty ELSE null END,
									CASE ma.HandlesCostCenter WHEN 1 THEN CASE @AssociateCostCenter WHEN 2 THEN g.CostCenterId ELSE w.CostCenterId END ELSE null END,
									0,
									iif(@flagCostNew = 1,
										ROUND(@CostNew * pd.Quantity,2),
										ROUND(ip.ProductCost * pd.Quantity,2)
									),
									'Generada desde Dispensacion ' + @CodePharmaceutical,
									null,
									null,
									null,
									null
								FROM #TablePharmaceuticalDetail pd
								INNER JOIN Inventory.InventoryProduct ip  ON ip.Id = pd.ProductId
								INNER JOIN Inventory.ProductGroup g  ON g.Id = ip.ProductGroupId
								INNER JOIN Inventory.Warehouse w  ON w.Id = pd.WarehouseId
								INNER JOIN GeneralLedger.MainAccounts ma  ON ma.Id = g.CounterpartCostConsignedInventoryId
								INNER JOIN Common.Supplier s  ON w.SupplierId = s.Id
								WHERE w.VirtualStore = @Cero AND w.ControlStore = @Cero AND w.WarehouseConsignment = @Uno AND pd.ChangeTracker <> @Deleted
								AND pd.PhysicalinventoryCustodyId is NULL

							---- (demás inventario)
							insert into @TableJournalVoucherDetail
								SELECT
									ma.Id,
									CASE ma.HandlesThirdParty when 1 then @CreditThirdPartyId else null end,
									CASE ma.HandlesCostCenter when 1 then case @AssociateCostCenter when 2 then g.CostCenterId else w.CostCenterId end else null end,
									0,
									iif(@flagCostNew = 1,
										ROUND(@CostNew * pd.Quantity,2),
										ROUND(ip.ProductCost * pd.Quantity,2)
									),
									'Generada desde Dispensacion ' + @CodePharmaceutical,
									null,
									null,
									null,
									null
								FROM #TablePharmaceuticalDetail pd
								INNER JOIN Inventory.InventoryProduct ip  ON ip.Id = pd.ProductId
								INNER JOIN Inventory.ProductGroup g  ON g.Id = ip.ProductGroupId
								INNER JOIN Payments.AccountPayableConcepts apc  ON apc.Id = g.InventoryAccountPayableConceptId
								INNER JOIN Inventory.Warehouse w  ON w.Id = pd.WarehouseId
								INNER JOIN GeneralLedger.MainAccounts ma  ON ma.Id = apc.IdAccount
								WHERE w.VirtualStore = @Cero AND w.ControlStore = @Cero AND w.WarehouseConsignment <> @Uno AND pd.ChangeTracker <> @Deleted
								AND pd.PhysicalinventoryCustodyId is NULL

					declare @IdSolicitud as INT

					---se optiene el id de la solicitud por si los productos individualmetne manejan su propio centro de costo
					select @IdSolicitud= hcc.CODCONCEC
					from MedicalHistory.NursingPackagesOrder npo
					join [dbo].[AGPAQUETES] AP on npo.IDAGPAQUETES = ap.ID
					join HCFARMEPC hcc on npo.IDHCFARMEPC = hcc.CODCONCEC
					where npo.Id = @PackageId

						/**** DETALLE DEBITO ****/
						insert into @TableJournalVoucherDetail
							select ma.Id
								,case ma.HandlesThirdParty when 1 then case cg.CareGroupType when 1 then ha.ThirdPartyId when 3 then @PatientThirdPartyId else @HealAdministratorAdmissionThirdPartyId end else null end
								,
								---si el producto maneja sui propio centro de costo se le asigna ese si no continua la logica antigua de obtener el centro de costo de la unidad funcional
								isnull(dr.IdCostCenter
										,
										(
										case ma.HandlesCostCenter
												when 1 then case @AssociateCostCenter
																when 1 then ISNULL(pd.CostCenterId, fu.CostCenterId)
																when 2 then pg.CostCenterId
																when 3 then w.CostCenterId
															end
												else
													null
												end
												)
									)
								,iif(@flagCostNew = 1,
									 ROUND(@CostNew * pd.Quantity,2),
									 ROUND(ip.ProductCost * pd.Quantity,2)
								)
								,0
								,'Generada desde Dispensacion ' + @CodePharmaceutical
								,null
								,null
								,null
								,null
							from #TablePharmaceuticalDetail pd
							inner join Inventory.Warehouse w  on w.Id = pd.WarehouseId
							inner join Payroll.FunctionalUnit fu  on fu.Id = pd.FunctionalUnitId
							inner join Inventory.InventoryProduct ip  on ip.Id = pd.ProductId
							inner join Inventory.ProductGroup pg  on pg.Id = ip.ProductGroupId
							inner join [Contract].CareGroup cg  on cg.Id = pd.CareGroupId
							left join [Contract].[Contract] c  on c.Id = cg.ContractId
							left join [Contract].HealthAdministrator ha  on ha.Id = c.HealthAdministratorId
							left join Inventory.SettingInventoryFunctionalUnit sifu  on @AssociateCostMainAccount = 1 AND sifu.SettingInventoryId = @SettingInventoryId AND pd.FunctionalUnitId = sifu.FunctionalUnitId
							left join Inventory.ProductGroupFunctionalUnit pgfu  on @AssociateCostMainAccount = 2 AND pg.Id = pgfu.ProductGroupId AND pd.FunctionalUnitId = pgfu.FunctionalUnitId
							left join GeneralLedger.MainAccounts ma  on ma.Id = ISNULL(sifu.CostAccountId, pgfu.CostAccountId)
							LEFT JOIN (
								SELECT   npo.NUMINGRES,paqd.CODPRODUC, npo.IDHCFARMEPC,paqd.IdCostCenter
								FROM MedicalHistory.NursingPackagesOrder npo 
								JOIN dbo.AGPAQUETES paq  ON paq.ID = npo.IDAGPAQUETES
								join AGPAQUETESD paqd  on paqd.IDAGPAQUETE = paq.ID
								LEFT JOIN HCFARMEPC hc  ON hc.CODCONCEC = npo.IDHCFARMEPC
								where paqd.IdCostCenter IS NOT NULL
							) as dr ON dr.NUMINGRES = @AdmissionNumber AND dr.CODPRODUC = pd.CodeProduct  and dr.IDHCFARMEPC = @IdSolicitud
							where w.VirtualStore = @Cero AND w.ControlStore = @Cero AND pd.ChangeTracker <> @Deleted AND pd.PhysicalinventoryCustodyId is NULL
						--- Realizo la interfaz con contabilidad
						declare @JournalXml xml =(select *
						from @TableJournalVoucher as JournalVoucher
						CROSS APPLY @TableJournalVoucherDetail as JournalVoucherDetail
						for xml auto, elements)

						declare @TableResultJournal table(CodeMessage varchar(20), Message varchar(max), IdJournalVoucher int)

						insert into @TableResultJournal
							exec [GeneralLedger].[SP_CreateAndValidateJournalVoucherMovement] @JournalXml, @User

						SET @ExecutionNow = SYSUTCDATETIME()
						SET @ExecutionStepOrder += 1
						INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
							(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
						VALUES
							(@ExecutionId, @ExecutionStepOrder, 'CONFIRMACION_COMPROBANTE_CONTABLE', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, NULL)
						SET @ExecutionPreviousAt = @ExecutionNow

						if Exists (select CodeMessage from @TableResultJournal where CodeMessage <> @SCero) begin

							--select CodeMessage, Message, 0 as DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status] from @TableResultJournal
							set @CodeMessageResult = (select CodeMessage from @TableResultJournal)
							set @ErrorsValidationResult = (select Message from @TableResultJournal)
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							GOTO Cleanup
						END
						SET @MessageReturn += ', ' + (SELECT [Message] FROM @TableResultJournal WHERE CodeMessage = 0)
					END
				END-- Fin si Afecta Inventario y contabilidad

				--- Elimino el control de inventarios
				delete from Inventory.InventoryControlDocument where DocumentNumber = @CodePharmaceutical and DocumentType = 5

				IF @DispensingIntegration = 1 BEGIN
					---- Realizo las modificacion en el modulo del EHR
					if @FunctionalUnitCode is not null and @ConsecutivePharmacy is not null and @CareCenterCode is not null begin
						---Validaciones
						if Exists (select NUMINGRES from dbo.CHREGEGRE where NUMINGRES = @AdmissionNumber) begin
							IF NOT EXISTS (SELECT 1 FROM #TablePharmaceuticalDetail WHERE Extramural = 1)
							BEGIN
								set @CodeMessageResult = '999'
								set @ErrorsValidationResult = 'El paciente ya fue egresado de la institución, favor actualizar los datos'
								set @DispensingIdResult = 0
								set @DispensingCodeResult = ''
								set @StatusResult = 3
								GOTO Cleanup
							END
						END
						-- varifico si yaexiste un inventario fisico en crystal
						declare @ProductId int, @ProductCodeCrystal varchar(20), @QuantityCrystal int, @ProductTypeCrystal varchar(20)
							, @CantidadSolicitadaCrystal int, @ProfessionalCrystal varchar(20), @Extramural bit , @EntityDId int, @CantidadMezcla int
							, @TotProUni NUMERIC(18, 2), @ProductInventoryCode varchar(20), @ProductATCCode varchar(20), @ProductSupplieCode varchar(20)

						Set @Rows = 1
						Set @RowId = 1

						WHILE @Rows > 0
						BEGIN

							SELECT TOP 1 @RowId = pdd.RowId
										, @ProductId = pdd.ProductId
										, @QuantityCrystal = pdd.Quantity
										, @ProductTypeCrystal = pdd.ProductType
										, @CantidadSolicitadaCrystal = pdd.CantidadSolicitada
										, @ProfessionalCrystal = pdd.OrderedHealthProfessionalCode
										, @Extramural = pdd.Extramural
										, @EntityDId = pdd.EntityId
										, @CantidadMezcla = CantidadMezcla
										, @ProductInventoryCode = pdd.ProductInventoryCode
										, @ProductATCCode = pdd.ProductATCCode
										, @ProductSupplieCode = pdd.ProductSupplieCode
							FROM #TablePharmaceuticalDetail pdd
							WHERE ChangeTracker <> @Deleted AND RowId >= @RowId Order By RowId

							Set @Rows = @@ROWCOUNT
							If @Rows = 0
								Break

							SET @ProductCodeCrystal = @ProductInventoryCode

							DECLARE @TotalQuantity INT
							IF EXISTS(SELECT 1 FROM #TableDeliveriesByPharmacyProductType WHERE HCFARMEPDID = @EntityDId AND ProductId = @ProductId)
							BEGIN
								SELECT @TotalQuantity = Quantity
								FROM #TableDeliveriesByPharmacyProductType
								WHERE HCFARMEPDID = @EntityDId AND ProductId = @ProductId

								SET @QuantityCrystal = @TotalQuantity
							END

							IF NOT EXISTS
							(
								SELECT 1
								FROM HCFARMEPD
								WHERE CODPRODUC = @ProductCodeCrystal
									AND CODCONCEC = @ConsecutiveCrystal
							)
							BEGIN--MEDICAMENTO
								SET @ProductCodeCrystal = @ProductATCCode

								IF @ProductCodeCrystal IS NULL
								BEGIN --INSUMO
									SET @ProductCodeCrystal = @ProductSupplieCode
								END
							END

							declare @CodeSusceptibleMixingStation uniqueidentifier,
									@SendTo tinyint

							SELECT TOP 1 @CodeSusceptibleMixingStation = CodeSusceptibleMixingStation,
										 @SendTo = SENDTO
							FROM HCFARMEPD
							WHERE CODCONCEC = @ConsecutiveCrystal AND @EntityDId = ID

							SELECT TOP 1 @TotProUni = hp.TOTPROUNI
							FROM HCFARMEPD hc
							JOIN HCPRESCRA hp ON hp.ID = hc.IdSourceTable AND hc.SourceTable = 'HCPRESCRA'
							WHERE hc.ID = @EntityDId
								--Si no es extramural generamos registro en el fisico y kardex del paciente para que enfermeria pueda aplicar
								if @Extramural = 0 and  @GuardaGastoQXTmp = 0 begin
									DECLARE @RequestedQuantity INT
									SELECT @RequestedQuantity = ISNULL(SUM(hcd.CANPEDPRO), 0)
									FROM HCFARMEPC hcc
									JOIN HCFARMEPD hcd ON hcc.CODCONCEC = hcd.CODCONCEC
									WHERE hcd.IPCODPACI = @PatientCode
										AND hcd.NUMINGRES = @AdmissionNumberHijo
										AND hcd.CODCENATE = @CareCenterCode
										AND hcd.UFUCODIGO = @FunctionalUnitCode
										AND hcd.CODPRODUC = @ProductCodeCrystal
										AND hcc.ORDESTADO <> 3
										AND hcd.ID = @EntityDId  -- división de dosis: filtrar por línea HCFARMEPD específica para no sumar cantidades de otros enrutamientos
									GROUP BY hcd.IPCODPACI, hcd.NUMINGRES, hcd.CODCENATE, hcd.UFUCODIGO, hcd.CODPRODUC

									if @SendTo = 2 begin

										update pd set IsDispensed = 1
										from MedicalHistory.PharmaDose pd (nolock)
										where CodeSusceptibleMixingStation = @CodeSusceptibleMixingStation And IsDispensed = 0
											And GroupingCodeDose In (
												select distinct top (@CantidadMezcla) GroupingCodeDose
												from MedicalHistory.PharmaDose (nolock)
												where CodeSusceptibleMixingStation = @CodeSusceptibleMixingStation And IsDispensed = 0
											)

										--'central de mezclas, siempre se crea un registro'
										INSERT INTO [dbo].[HCFISIPRO]
										   ([IPCODPACI]
										   ,[NUMINGRES]
										   ,[CODCENATE]
										   ,[UFUCODIGO]
										   ,[CODPRODUC]
										   ,[TIPPRODUC]
										   ,[CANACTPRO]
										   ,[CANPEDPRO]
										   ,[CANPENPRO]
										   ,[TOTPROUNI]
										   ,[DOSPROACU]
										   ,[TOTHORACU]
										   ,[CODUNIMED]
										   ,[INDAUDFOR]
										   ,[TIPREGIST])
										VALUES
										   (@PatientCode
										   ,@AdmissionNumberHijo
										   ,@CareCenterCode
										   ,@FunctionalUnitCode
										   ,@ProductCodeCrystal
										   ,@ProductTypeCrystal
										   ,@QuantityCrystal
										   ,@QuantityCrystal -- CANPEDPRO: cantidad pedida
										   ,0               -- CANPENPRO: pendiente = 0 (ya fue dispensado y confirmado)
										   ,@TotProUni
										   ,NULL
										   ,NULL
										   ,NULL
										   ,0
										   ,1)
									end
									else begin
										IF exists(SELECT 1 FROM Inventory.InventoryProduct ip
													JOIN Inventory.ProductType pt ON pt.Id = ip.ProductTypeId
													WHERE ip.id = @ProductId AND pt.Class = 5) AND @SendTo = 1 BEGIN

											INSERT INTO [dbo].[HCFISIPRO]
											   ([IPCODPACI]
											   ,[NUMINGRES]
											   ,[CODCENATE]
											   ,[UFUCODIGO]
											   ,[CODPRODUC]
											   ,[TIPPRODUC]
											   ,[CANACTPRO]
											   ,[CANPEDPRO]
											   ,[CANPENPRO]
											   ,[TOTPROUNI]
											   ,[DOSPROACU]
											   ,[TOTHORACU]
											   ,[CODUNIMED]
											   ,[INDAUDFOR]
											   ,[TIPREGIST])
											VALUES
											   (@PatientCode
											   ,@AdmissionNumberHijo
											   ,@CareCenterCode
											   ,@FunctionalUnitCode
											   ,@ProductCodeCrystal
											   ,@ProductTypeCrystal
											   ,@TotalQuantity  -- CANACTPRO: cantidad actual dispensada
											   ,@TotalQuantity  -- CANPEDPRO: cantidad pedida
											   ,0               -- CANPENPRO: pendiente = 0 (ya fue dispensado y confirmado)
											   ,NULL
											   ,NULL
											   ,NULL
											   ,NULL
											   ,0
											   ,1)
										END
										ELSE

										IF EXISTS (
											SELECT IPCODPACI
											FROM dbo.HCFISIPRO
											WHERE IPCODPACI = @PatientCode
												AND NUMINGRES = @AdmissionNumberHijo
												AND CODCENATE = @CareCenterCode
												AND UFUCODIGO = @FunctionalUnitCode
												AND CODPRODUC = @ProductCodeCrystal
												AND TIPREGIST = 0
										)
										BEGIN
											UPDATE dbo.HCFISIPRO
											SET CANACTPRO += @QuantityCrystal,
												CANPEDPRO = @RequestedQuantity
											WHERE IPCODPACI = @PatientCode
												AND NUMINGRES = @AdmissionNumberHijo
												AND CODCENATE = @CareCenterCode
												AND UFUCODIGO = @FunctionalUnitCode
												AND CODPRODUC = @ProductCodeCrystal
												AND TIPREGIST = 0
										END
										ELSE BEGIN
											IF @ProductTypeCrystal = 5 --Si es un paquete de enfermeria
											BEGIN
												IF NOT EXISTS (
													SELECT 1
													FROM dbo.HCFISIPRO
													WHERE IPCODPACI = @PatientCode
														AND NUMINGRES = @AdmissionNumberHijo
														AND CODCENATE = @CareCenterCode
														AND UFUCODIGO = @FunctionalUnitCode
														AND CODPRODUC = @ProductCodeCrystal
														AND TIPREGIST = 0
												)
												BEGIN
													INSERT INTO [dbo].[HCFISIPRO]
														([IPCODPACI]
														,[NUMINGRES]
														,[CODCENATE]
														,[UFUCODIGO]
														,[CODPRODUC]
														,[TIPPRODUC]
														,[CANACTPRO]
														,[CANPEDPRO]
														,[CANPENPRO]
														,[TOTPROUNI]
														,[DOSPROACU]
														,[TOTHORACU]
														,[CODUNIMED]
														,[INDAUDFOR]
														,[TIPREGIST])
													SELECT TOP 1  -- Evita duplicidad de registros, los registros adicionales actualizan cantiaddes en el update
														@PatientCode
														,@AdmissionNumberHijo
														,@CareCenterCode
														,@FunctionalUnitCode
														,@ProductCodeCrystal
														,SFD.TIPOREGIS
														,@QuantityCrystal
														,@RequestedQuantity
														,@CantidadSolicitadaCrystal - @QuantityCrystal
														,NULL
														,NULL
														,NULL
														,NULL
														,0
														,0
													FROM dbo.HCFARMEPD SFD
													LEFT JOIN MedicalHistory.NursingPackagesOrder npo
														ON npo.IDHCFARMEPC = SFD.CODCONCEC
														AND npo.IDAGPAQUETES = SFD.IDAGPAQUETES
													WHERE npo.IDHCFARMEPC = @ConsecutiveCrystal
														AND SFD.CODPRODUC = @ProductCodeCrystal
														AND (@EntityDId IS NULL OR SFD.ID = @EntityDId)
													ORDER BY SFD.ID DESC
												END
											END
											ELSE
											BEGIN

												INSERT INTO [dbo].[HCFISIPRO]
												   ([IPCODPACI]
												   ,[NUMINGRES]
												   ,[CODCENATE]
												   ,[UFUCODIGO]
												   ,[CODPRODUC]
												   ,[TIPPRODUC]
												   ,[CANACTPRO]
												   ,[CANPEDPRO]
												   ,[CANPENPRO]
												   ,[TOTPROUNI]
												   ,[DOSPROACU]
												   ,[TOTHORACU]
												   ,[CODUNIMED]
												   ,[INDAUDFOR])
												VALUES
												   (@PatientCode
												   ,@AdmissionNumberHijo
												   ,@CareCenterCode
												   ,@FunctionalUnitCode
												   ,@ProductCodeCrystal
												   ,@ProductTypeCrystal
												   ,@QuantityCrystal
												   ,ISNULL(@TotalQuantity, @CantidadSolicitadaCrystal)
												   ,@CantidadSolicitadaCrystal - @QuantityCrystal  -- CANPENPRO: pendiente real = solicitado - dispensado (no @TotalQuantity que es la cantidad dispensada, no la pendiente)
												   ,NULL
												   ,NULL
												   ,NULL
												   ,NULL
												   ,0)
											END
										END
									end

									--- Inserto en el Kardex de crystal
									if @HistoryType = 'ENFERMER1' begin
										set @HistoryTypeCrystal = 2
										SET @HistoryTypeNameCrystal = 'Despacho farmacia, origen solicitud: Despacho de farmacia - solicitud de enfermería - usuario: ' + @UserName
									END
									ELSE IF @HistoryType = 'CODIGOAZU' begin
										set @HistoryTypeCrystal = 3
										SET @HistoryTypeNameCrystal = 'Despacho farmacia, origen solicitud: Despacho de farmacia - solicitud de emergencia - usuario: ' + @UserName
									END
									ELSE BEGIN
										set @HistoryTypeCrystal = 1
										IF ISNULL(@EntityName, '') = 'PharmaceuticalDispensingTransfer'
										BEGIN
											SET @HistoryTypeNameCrystal = CONCAT('Traslado dispensación por ingreso ', @EntityCode, ': Ingreso origen ', @AdmissionNumberOrigin, ' - ingreso destino ', @AdmissionNumber, ' - usuario: ', @UserName)
										END
										ELSE
										BEGIN
											SET @HistoryTypeNameCrystal = 'Despacho farmacia, origen solicitud: Despacho de farmacia - solicitud del médico - usuario: ' + @UserName
										END
									END

									if @SendTo = 2 begin
										IF @CodeSusceptibleMixingStation IS NOT NULL BEGIN
											IF EXISTS ( SELECT 1 FROM MixingStation.ConfirmationUnitDose cu
											JOIN MedicalHistory.PharmaDose ph ON ph.GroupingCodeDose = cu.GroupingCodeDose
											JOIN MixingStation.Package p ON p.Id = cu.PackageId
											JOIN MixingStation.UnitDoseType ud ON ud.Id = p.UnitDoseTypeId
											WHERE ph.CodeSusceptibleMixingStation = @CodeSusceptibleMixingStation AND ud.MSClass = 9) BEGIN

												UPDATE HCKARDPAC
												SET CODPRODUC = @ProductCodeCrystal
												WHERE CodeSusceptibleMixingStation = @CodeSusceptibleMixingStation
											END
										END

										INSERT INTO [dbo].[HCKARDPAC]
											([NUMCONSEC]
											,[IPCODPACI]
											,[NUMINGRES]
											,[CODCENATE]
											,[UFUCODIGO]
											,[CODPROSAL]
											,[CODPRODUC]
											,[CANPRODUCT]
											,[TIPREGIST]
											,[HCPRESCRN]
											,[HCSOLINSN]
											,[HCCTRAPLN]
											,[HCCTRAPLM]
											,[CODDOCUME]
											,[FECREGKAR]
											,[TIPORIREG]
											,[DESMOVPRO]
											,[JUSANULAC]
											,[CONSECFAR]
											,[FECHAUTIL]
											,[OBSERVACI]
											,[CodeSusceptibleMixingStation])
										OUTPUT INSERTED.NUMCONSEC, INSERTED.CODPRODUC, INSERTED.CodeSusceptibleMixingStation into #HCKARDPACConcecutives
										select
											 NEWID()
											, @PatientCode
											, @AdmissionNumberHijo
											, @CareCenterCode
											, @FunctionalUnitCode
											, @ProfessionalCrystal
											, @ProductCodeCrystal
											, 1
											, '1'
											, @ConsecutivePescription
											, @ConsecutiveInputs
											, NULL
											, NULL
											, NULL
											, @GetDateTime
											, @HistoryTypeCrystal
											, @HistoryTypeNameCrystal
											, NULL
											, @ConsecutivePharmacy
											, NULL
											, NULL
											, @CodeSusceptibleMixingStation
										from (
											SELECT ROW_NUMBER() OVER(ORDER BY Id ASC) as Row, Id from billing.DuplicateRows(1, @QuantityCrystal)
										) c

									end
									ELSE BEGIN

								IF exists(SELECT 1 FROM Inventory.InventoryProduct ip
											JOIN Inventory.ProductType pt ON pt.Id = ip.ProductTypeId
											WHERE ip.id = @ProductId AND pt.Class = 5) AND @SendTo = 1 BEGIN

									INSERT INTO [dbo].[HCKARDPAC]
										([NUMCONSEC]
										,[IPCODPACI]
										,[NUMINGRES]
										,[CODCENATE]
										,[UFUCODIGO]
										,[CODPROSAL]
										,[CODPRODUC]
										,[CANPRODUCT]
										,[TIPREGIST]
										,[HCPRESCRN]
										,[HCSOLINSN]
										,[HCCTRAPLN]
										,[HCCTRAPLM]
										,[CODDOCUME]
										,[FECREGKAR]
										,[TIPORIREG]
										,[DESMOVPRO]
										,[JUSANULAC]
										,[CONSECFAR]
										,[FECHAUTIL]
										,[OBSERVACI]
										,[CodeSusceptibleMixingStation])
									OUTPUT INSERTED.NUMCONSEC, INSERTED.CODPRODUC, INSERTED.CodeSusceptibleMixingStation into #HCKARDPACConcecutives
									select
										 NEWID()
										, @PatientCode
										, @AdmissionNumberHijo
										, @CareCenterCode
										, @FunctionalUnitCode
										, @ProfessionalCrystal
										, @ProductCodeCrystal
										, 1
										, '1'
										, @ConsecutivePescription
										, @ConsecutiveInputs
										, NULL
										, NULL
										, NULL
										, @GetDateTime
										, @HistoryTypeCrystal
										, @HistoryTypeNameCrystal
										, NULL
										, @ConsecutivePharmacy
										, NULL
										, NULL
										, @CodeSusceptibleMixingStation
									from (
										SELECT ROW_NUMBER() OVER(ORDER BY Id ASC) as Row, Id from billing.DuplicateRows(1, @QuantityCrystal)
									) c

								END
									ELSE BEGIN

										INSERT INTO [dbo].[HCKARDPAC]
											([NUMCONSEC]
											,[IPCODPACI]
											,[NUMINGRES]
											,[CODCENATE]
											,[UFUCODIGO]
											,[CODPROSAL]
											,[CODPRODUC]
											,[CANPRODUCT]
											,[TIPREGIST]
											,[HCPRESCRN]
											,[HCSOLINSN]
											,[HCCTRAPLN]
											,[HCCTRAPLM]
											,[CODDOCUME]
											,[FECREGKAR]
											,[TIPORIREG]
											,[DESMOVPRO]
											,[JUSANULAC]
											,[CONSECFAR]
											,[FECHAUTIL]
											,[OBSERVACI])
										select
											 NEWID()
											,@PatientCode
											,@AdmissionNumberHijo
											,@CareCenterCode
											,@FunctionalUnitCode
											,@ProfessionalCrystal
											,@ProductCodeCrystal
											,@QuantityCrystal
											,'1'
											,@ConsecutivePescription
											,@ConsecutiveInputs
											,NULL
											,NULL
											,NULL
											,@GetDateTime
											,@HistoryTypeCrystal
											,@HistoryTypeNameCrystal
											,NULL
											,@ConsecutivePharmacy
											,NULL
											,NULL
									END
								END
								END
								--- Actualizo los estado en la solicitud de farmacia

							if ISNULL(@EntityName, '') <> 'PharmaceuticalDispensingTransfer'
								AND NOT EXISTS
								(
									select 1 from dbo.HCFARMEPD
									
									Where CODCONCEC = @ConsecutiveCrystal and CODPRODUC = @ProductCodeCrystal
								)
								AND NOT EXISTS (
									SELECT 1
									FROM Inventory.InventoryProduct p
									JOIN Inventory.ATC med ON med.Id = p.ATCId
									WHERE med.code = @ProductCodeCrystal
							)
								AND NOT EXISTS (
									SELECT 1
									FROM Inventory.InventoryProduct p
									JOIN Inventory.InventorySupplie ins ON ins.Id = p.SupplieId
									WHERE p.Code = @ProductCodeCrystal
								)

								begin
									set @CodeMessageResult = '999'
									set @ErrorsValidationResult = 'El producto '+ @ProductCodeCrystal + ' no fue solicitado'
									set @DispensingIdResult = 0
									set @DispensingCodeResult = ''
									set @StatusResult = 3
									GOTO Cleanup
								END

								If @SendTo = 2 Begin
									declare @CantidadDosis Int,
										@CantidadPreviaEntregada Int

									select @CantidadDosis = count(*)
									from (
										select 1 as item
										from MedicalHistory.PharmaDose (nolock)
										where CodeSusceptibleMixingStation = @CodeSusceptibleMixingStation
										group by GroupingCodeDose
									) as t

									select @CantidadPreviaEntregada = count(*)
									from (
										select 1 as item
										from MedicalHistory.PharmaDose (nolock)
										where CodeSusceptibleMixingStation = @CodeSusceptibleMixingStation And IsDispensed = 1
										group by GroupingCodeDose
									) as t

									--@CantidadPreviaEntregada solo se toma esta variable debido a que algunas líneas arriba se está actualizando con la cantidad a entregar
									declare @porcentaje decimal(5, 2) = cast((@CantidadPreviaEntregada) as decimal(5, 2)) / cast(@CantidadDosis as decimal(5, 2))

									UPDATE dbo.HCFARMEPD
									SET
										CANPENPRO = CEILING(CANPEDPRO - (CANPEDPRO * @porcentaje)),
										PROESTADO = IIF(CEILING(CANPEDPRO - (CANPEDPRO * @porcentaje)) = 0, 2, PROESTADO)
									WHERE CodeSusceptibleMixingStation = @CodeSusceptibleMixingStation;

								End
								else begin

									DECLARE @TotalQuantityTmp INT
									IF EXISTS(SELECT 1 FROM #TableDeliveriesByPharmacyProductType WHERE HCFARMEPDID = @EntityDId AND ProductId = @ProductId)
									BEGIN
										SELECT @TotalQuantityTmp =
											CASE WHEN ProductType = 0 THEN QuantityDeliveryPT ELSE Quantity END
										FROM #TableDeliveriesByPharmacyProductType
										WHERE HCFARMEPDID = @EntityDId AND ProductId = @ProductId

										SET @QuantityCrystal = @TotalQuantityTmp
									END
									--- Actualizo los estado en la solicitud de farmacia
									if (
										select SUM(CANPENPRO)
										from dbo.HCFARMEPD (Nolock)
										Where CODCONCEC = @ConsecutiveCrystal
											and CODPRODUC = @ProductCodeCrystal
											and (@EntityDId is null or @EntityDId = ID)
										) < @QuantityCrystal begin

										set @CodeMessageResult = '999'
										set @ErrorsValidationResult = 'La cantidad pendiente por entregar del producto '+ @ProductCodeCrystal + ' es menor a la cantidad a dispensar'
										set @DispensingIdResult = 0
										set @DispensingCodeResult = ''
										set @StatusResult = 3
										GOTO Cleanup
									END

									DECLARE @Remaining INT = @QuantityCrystal; -- Cantidad total a distribuir
									WHILE @Remaining > 0
									BEGIN
										DECLARE @FilaId INT, @pendingRow INT;
										-- Selecciona la siguiente fila pendiente a afectar
										SELECT TOP 1 @FilaId = ID, @pendingRow = CANPENPRO
										FROM dbo.HCFARMEPD WITH (UPDLOCK)
										WHERE CODCONCEC = @ConsecutiveCrystal
										  AND CODPRODUC = @ProductCodeCrystal
										  AND CANPENPRO > 0
										  AND (@EntityDId IS NULL OR @EntityDId = ID)
										ORDER BY ID
										-- Si ya no hay más filas, salir del bucle
										IF @FilaId IS NULL
											BREAK;
										-- Si la fila tiene menos o igual que lo que falta por descontar
										IF @pendingRow <= @Remaining
										BEGIN
											UPDATE dbo.HCFARMEPD
											SET CANPENPRO = 0, PROESTADO = 2 -- Estado entregado
											WHERE ID = @FilaId;
											SET @Remaining -= @pendingRow;
										END
										ELSE
										BEGIN
											UPDATE dbo.HCFARMEPD
											SET CANPENPRO = CANPENPRO - @Remaining
											WHERE ID = @FilaId;
											SET @Remaining = 0;
										END
									END
									-- Si ya no queda pendiente, actualizar el estado a PROESTADO = 2
									IF (
										SELECT SUM(CANPENPRO)
										FROM dbo.HCFARMEPD 
										WHERE CODCONCEC = @ConsecutiveCrystal
										  AND CODPRODUC = @ProductCodeCrystal
										  AND (@EntityDId IS NULL OR @EntityDId = ID)
									) = 0
									BEGIN
										UPDATE dbo.HCFARMEPD
										SET PROESTADO = 2
										WHERE CODCONCEC = @ConsecutiveCrystal
										  AND CODPRODUC = @ProductCodeCrystal
										  AND (@EntityDId IS NULL OR @EntityDId = ID);
									END

								end

								if Not Exists (select CODCONCEC from dbo.HCFARMEPD  where CODCONCEC = @ConsecutiveCrystal and CANPENPRO > 0) Begin
									update dbo.HCFARMEPC set ORDESTADO = '2' where CODCONCEC = @ConsecutiveCrystal
								end

								--Actualizo el estado del paquete de enfermeria cuando la cantidad a solicitar sea igual a la despachada
								UPDATE npo
									SET npo.Status = @Dos
								FROM MedicalHistory.NursingPackagesOrder npo
								JOIN MedicalHistory.NursingPackagesOrderDetail npod ON npo.Id = npod.IdNursingPackagesOrder
								WHERE npo.IDHCFARMEPC = @ConsecutiveCrystal
									AND npod.Quantity = @QuantityCrystal

							SET @RowId += 1
						END --Fin ciclo detalle dispensación

						--Valida que los detalles asociados a la cabecera tengan el estado correspondiente
						If EXISTS (
									SELECT 1 FROM dbo.HCFARMEPC hc
										JOIN dbo.HCFARMEPD hd ON hc.CODCONCEC = hd.CODCONCEC
									WHERE hc.CODCONCEC = @ConsecutiveCrystal
										AND hc.ORDESTADO <> 1 --El estado de la cabecera esta como entregado o anulado
										AND hd.PROESTADO = 1 --El estado del detalle esta como pendiente
										AND hd.CANPENPRO > 0 --Solo debe bloquear si aun hay cantidad pendiente real
									)
						BEGIN
							SET @CodeMessageResult = '999'
							SET @ErrorsValidationResult = 'Aún hay detalles que se encuentran en estado pendiente'
							SET @DispensingIdResult = 0
							SET @DispensingCodeResult = ''
							SET @StatusResult = 3
							GOTO Cleanup
						END

					END -- Fin Integracion con EHR
				END

				SET @ExecutionNow = SYSUTCDATETIME()
				SET @ExecutionStepOrder += 1
				INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
					(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
				VALUES
					(@ExecutionId, @ExecutionStepOrder, 'CONFIRMACION_INTEGRACION_HCE', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, CONCAT('Integracion=', @DispensingIntegration))
				SET @ExecutionPreviousAt = @ExecutionNow

				-- valido si afecta o no el inventario
				if @AffectInventory <> 0 begin
					if Exists (
						select 1
						from Inventory.PharmaceuticalDispensingDetail pd 
						where pd.PharmaceuticalDispensingId = @IdPharmaceutical
							and not exists
							(
								select 1
								from Inventory.PharmaceuticalDispensingDetailBatchSerial bs 
								where bs.PharmaceuticalDispensingDetailId = pd.Id
							)
					) begin

						set @CodeMessageResult = '999'
						set @ErrorsValidationResult = 'La estructura presenta inconsistencias por favor contacte al administrador'
						set @DispensingIdResult = 0
						set @DispensingCodeResult = ''
						set @StatusResult = 3
						GOTO Cleanup
					END
				END

				SET @ExecutionNow = SYSUTCDATETIME()
				SET @ExecutionStepOrder += 1
				INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
					(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
				VALUES
					(@ExecutionId, @ExecutionStepOrder, 'CONFIRMACION_VALIDA_DETALLES_CON_LOTE', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, CONCAT('AfectaInventario=', @AffectInventory))
				SET @ExecutionPreviousAt = @ExecutionNow

				CREATE TABLE #ValidacionInventarioFisico
				(
					ProductId INT NOT NULL,
					PhysicalInventoryId INT NOT NULL,
					PhysicalInventoryProductId INT NULL,
					DetailATCId INT NULL,
					PhysicalInventoryATCId INT NULL
				)

				INSERT INTO #ValidacionInventarioFisico
					(ProductId, PhysicalInventoryId)
				SELECT DISTINCT
					pd.ProductId,
					pdb.PhysicalInventoryId
				FROM Inventory.PharmaceuticalDispensingDetail pd 
				INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pdb 
					ON pd.Id = pdb.PharmaceuticalDispensingDetailId
				WHERE pd.PharmaceuticalDispensingId = @IdPharmaceutical
					AND pdb.PhysicalInventoryId IS NOT NULL

				CREATE NONCLUSTERED INDEX IX_ValidacionInventarioFisico_PhysicalInventoryId
					ON #ValidacionInventarioFisico (PhysicalInventoryId)
					INCLUDE (ProductId)

				CREATE NONCLUSTERED INDEX IX_ValidacionInventarioFisico_ProductId
					ON #ValidacionInventarioFisico (ProductId)
					INCLUDE (PhysicalInventoryId, PhysicalInventoryProductId)

				SET @ExecutionNow = SYSUTCDATETIME()
				SET @ExecutionStepOrder += 1
				INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
					(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
				VALUES
					(@ExecutionId, @ExecutionStepOrder, 'CONFIRMACION_PREPARA_VALIDACION_INVENTARIO_FISICO', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, CONCAT('FilasValidacion=', (SELECT COUNT(1) FROM #ValidacionInventarioFisico)))
				SET @ExecutionPreviousAt = @ExecutionNow

				UPDATE vif
					SET PhysicalInventoryProductId = pin.ProductId
				FROM #ValidacionInventarioFisico vif
				INNER JOIN Inventory.PhysicalInventory pin 
					ON pin.Id = vif.PhysicalInventoryId

				SET @ExecutionNow = SYSUTCDATETIME()
				SET @ExecutionStepOrder += 1
				INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
					(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
				VALUES
					(@ExecutionId, @ExecutionStepOrder, 'CONFIRMACION_CARGA_INVENTARIO_FISICO', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, CONCAT('FilasValidacion=', (SELECT COUNT(1) FROM #ValidacionInventarioFisico)))
				SET @ExecutionPreviousAt = @ExecutionNow

				UPDATE vif
					SET DetailATCId = ipd.ATCId,
						PhysicalInventoryATCId = ipin.ATCId
				FROM #ValidacionInventarioFisico vif
				INNER JOIN Inventory.InventoryProduct ipd 
					ON ipd.Id = vif.ProductId
				INNER JOIN Inventory.InventoryProduct ipin 
					ON ipin.Id = vif.PhysicalInventoryProductId

				IF EXISTS (
					SELECT 1
					FROM #ValidacionInventarioFisico vif
					WHERE vif.DetailATCId <> vif.PhysicalInventoryATCId
				)
				BEGIN
					set @CodeMessageResult = '999'
					set @ErrorsValidationResult = 'Los productos no coinciden entre el detalle y subdetalle por favor contacte al administrador'
					set @DispensingIdResult = 0
					set @DispensingCodeResult = ''
					set @StatusResult = 3
					GOTO Cleanup
				END

				SET @ExecutionNow = SYSUTCDATETIME()
				SET @ExecutionStepOrder += 1
				INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
					(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
				VALUES
					(@ExecutionId, @ExecutionStepOrder, 'CONFIRMACION_COMPARA_ATC_INVENTARIO_FISICO', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, CONCAT('FilasValidacion=', (SELECT COUNT(1) FROM #ValidacionInventarioFisico)))
				SET @ExecutionPreviousAt = @ExecutionNow

				if Exists (select pd.Id from Inventory.PharmaceuticalDispensingDetail pd 
					inner join Inventory.PharmaceuticalDispensingDetailBatchSerial pdb  on pd.Id = pdb.PharmaceuticalDispensingDetailId
					inner join Inventory.PhysicalInventoryCustody pin  on pin.Id = pdb.PhysicalInventoryCustodyId
					where pd.PharmaceuticalDispensingId = @IdPharmaceutical and pd.ProductId <> pin.ProductId
					and pdb.PhysicalInventoryCustodyId IS NOT NULL) begin

					set @CodeMessageResult = '999'
					set @ErrorsValidationResult = 'Los productos no coinciden entre el detalle y subdetalle por favor contacte al administrador'
					set @DispensingIdResult = 0
					set @DispensingCodeResult = ''
					set @StatusResult = 3
					GOTO Cleanup
				END

				SET @ExecutionNow = SYSUTCDATETIME()
				SET @ExecutionStepOrder += 1
				INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
					(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
				VALUES
					(@ExecutionId, @ExecutionStepOrder, 'CONFIRMACION_VALIDA_PRODUCTO_INVENTARIO_CUSTODIA', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, NULL)
				SET @ExecutionPreviousAt = @ExecutionNow

				declare @UserNameCreation varchar(200) =''
				if (@IdPharmaceutical > 0) begin
					declare @userCode varchar(20)
					select @userCode = CreationUser  from PharmaceuticalDispensing  where id = @IdPharmaceutical

					SELECT @UserNameCreation = ' *USERINDIGO* '+ u.UserCode + ' - '+ p.Fullname
					FROM [Security].[User] u
					INNER JOIN [Security].Person p ON u.IdPerson = p.Id
					WHERE u.UserCode = @userCode
				END

				-- De acuerdo con el tipo de integración (2 - Heon) Guardamos la data para ser enviada al servicio
				IF @DispensingIntegration = 2 BEGIN
					--Almacenamos el registro de la dispensacion por unidad funcional de Heon
					INSERT INTO [Inventory].[DispensingByFunctionalUnit]
							   ([FunctionalUnitId], [PharmaceuticalDispensingId])
						SELECT @FunctionalUnitHeonId, @IdPharmaceutical
				END

				SET @ExecutionNow = SYSUTCDATETIME()
				SET @ExecutionStepOrder += 1
				INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
					(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
				VALUES
					(@ExecutionId, @ExecutionStepOrder, 'CONFIRMACION_PREPARA_RESULTADO_FINAL', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, CONCAT('Integracion=', @DispensingIntegration))
				SET @ExecutionPreviousAt = @ExecutionNow

				set @CodeMessageResult = '0'
				SET @ErrorsValidationResult = IIF(ISNULL(@EntityName, '') <> 'PharmaceuticalDispensingTransfer', @MessageReturn+@UserNameCreation, CONCAT('Se guardó y confirmó la dispensación ', @CodePharmaceutical))
				set @DispensingIdResult = isnull(@IdPharmaceutical,0)
				set @DispensingCodeResult = isnull(@CodePharmaceutical, '')
				set @StatusResult = 1

				SET @ExecutionNow = SYSUTCDATETIME()
				SET @ExecutionStepOrder += 1
				INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
					(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
				VALUES
					(@ExecutionId, @ExecutionStepOrder, 'CONFIRMACION_RESULTADO', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, CONCAT('AfectaInventario=', @AffectInventory, ';Integracion=', @DispensingIntegration))
				SET @ExecutionPreviousAt = @ExecutionNow

			--**********************
		IF @DispensingIntegration = 1 AND @DispensingIdResult > 0 BEGIN

			-- ============================================================
			-- Declaración de variables FUERA del loop
			-- ============================================================
			DECLARE
				-- Grupo 1: Variables de producto
				@productTypeClass tinyint,
				@atcCode varchar(20),
				@IDFisipro Int,
				@PharmaceuticalDispensingDetailId Int,
				@ProductName VARCHAR(300),

				-- Grupo 2: Variables de lote
				@BatchCodeTmp VARCHAR(50),
				@BatchQuantityTmp INT,
				@ExpirationDateTmp Datetime,

				-- Grupo 3: Variables de mezcla
				@NewId AS UNIQUEIDENTIFIER,
				@MeasurementUnitCode VARCHAR(10),
				@Dose NUMERIC(18,2),
				@UnitDoseTypeId INT,
				@CodProSal CHAR(20),
				@SourceTable varchar(20),
				@IdSourceTable INT,

				-- Grupo 4: Variables auxiliares del WHILE interno
				@Row INT,
				@MaxRows INT,
				@RowProductId INT,
				@RowBatchCode VARCHAR(50),
				@RowGroupingCodeDose UNIQUEIDENTIFIER

			-- Declarar tablas temporales
			CREATE TABLE #tmpPhysicalCum (
				Id Int Identity (1, 1) primary key,
				BatchCode varchar(50),
				ExpirationDate DateTime,
				Quantity Int,
				CodeSusceptibleMixingStation uniqueidentifier
			)
			CREATE NONCLUSTERED INDEX IX_tmpPhysicalCum_BatchCode
				ON #tmpPhysicalCum (BatchCode)
				INCLUDE (Quantity, ExpirationDate, CodeSusceptibleMixingStation)
			CREATE NONCLUSTERED INDEX IX_tmpPhysicalCum_CodeSusceptibleMixingStation
				ON #tmpPhysicalCum (CodeSusceptibleMixingStation)
				INCLUDE (BatchCode, Quantity, ExpirationDate)

			CREATE TABLE #TmpGroupingCodes (
				RowId INT IDENTITY(1,1) PRIMARY KEY,
				ProductId INT,
				Quantity INT,
				BatchCode VARCHAR(50),
				ExpirationDate DateTime,
				GroupingCodeDose UNIQUEIDENTIFIER,
				CodeSusceptibleMixingStation UNIQUEIDENTIFIER
			);
			CREATE NONCLUSTERED INDEX IX_TmpGroupingCodes_Product_Batch_Grouping
				ON #TmpGroupingCodes (ProductId, BatchCode, GroupingCodeDose)
				INCLUDE (Quantity, ExpirationDate, CodeSusceptibleMixingStation)
			CREATE NONCLUSTERED INDEX IX_TmpGroupingCodes_GroupingCodeDose
				ON #TmpGroupingCodes (GroupingCodeDose)
				INCLUDE (ProductId, BatchCode, Quantity, CodeSusceptibleMixingStation)

			-- Tabla para mapeo de DetailPhysicalCUM con HCKARDPAC
			-- Incluye RowNum para relación 1:1 con múltiples lotes del mismo producto
			CREATE TABLE #DetailCUMMapping (
			    RowNum INT,
			    ProductId INT,
			    BatchCode VARCHAR(50),
			    GroupingCodeDose UNIQUEIDENTIFIER,
			    IdDetailPhysicalCUM INT,
			    CodeSusceptibleMixingStation UNIQUEIDENTIFIER
			)
			CREATE NONCLUSTERED INDEX IX_DetailCUMMapping_CodeSusceptibleMixingStation_RowNum
				ON #DetailCUMMapping (CodeSusceptibleMixingStation, RowNum)
				INCLUDE (IdDetailPhysicalCUM, ProductId, BatchCode, GroupingCodeDose)

			-- Inicializar variables del loop DESPUÉS de las declaraciones
			Set @Rows = 1
			Set @RowId = 1

			-- Iniciar el WHILE principal
			while @Rows > 0
				begin
					-- Reinicializar variables críticas al inicio de cada iteración
					SET @NewId = NULL
					SET @productTypeClass = NULL
					SET @atcCode = NULL
					SET @IDFisipro = NULL
					SET @PharmaceuticalDispensingDetailId = NULL
					SET @ProductName = NULL
					SET @BatchCodeTmp = NULL
					SET @BatchQuantityTmp = NULL
					SET @ExpirationDateTmp = NULL
					SET @SourceTable = NULL
					SET @IdSourceTable = NULL
					SET @MeasurementUnitCode = NULL
					SET @Dose = NULL
					SET @CodProSal = NULL
					SET @UnitDoseTypeId = NULL

					Select Top 1
						@RowId = RowId,
						@ProductIdTmp = ProductId,
						@ProductCodeTmp = CodeProduct,
						@CantidadEntregada = Quantity,
						@PharmaceuticalDetailIdTmp =IdTmp,
						@CodProductTmp = CodeProduct,
						@EntityIdTmp = EntityId,
						@EntityNameTmp = EntityName
					From #TablePharmaceuticalDetail
					where RowId >= @RowId
					Order By RowId

					set @Rows = @@ROWCOUNT
					if @Rows = 0
						Break

					IF @ProductIdTmp >  0 BEGIN

						/*
						Clase del tipo de producto
						1 - Grupo
						2 - Item Medicamento
						3 - Item Insumo
						4 - Item Otro
						5 - Item Producción
						*/
						select top 1 @productTypeClass = pt.Class, @ProductName = pr.Name
						from Inventory.InventoryProduct pr (nolock)
						join Inventory.ProductType pt (nolock) on pr.ProductTypeId = pt.Id
						where pr.Id = @ProductIdTmp

						if @productTypeClass = 5 begin
							select top 1
								@atcCode = atc.Code,
								@PharmaceuticalDispensingDetailId = pdd.Id
							from Inventory.PharmaceuticalDispensing pd 
							join Inventory.PharmaceuticalDispensingDetail pdd  on pdd.PharmaceuticalDispensingId = pd.Id
							join Inventory.InventoryProduct ipr  on pdd.ProductId = ipr.Id
							join Inventory.ATC atc  on ipr.ATCId = atc.Id
							where pd.Id = @DispensingIdResult And pdd.ProductId = @ProductIdTmp
								AND (ISNULL(pdd.EntityId, 0) = ISNULL(@EntityIdTmp, 0) AND ISNULL(pdd.EntityName, '') = ISNULL(@EntityNameTmp, ''))

							select top 1 @IDFisipro = ID
							from HCFISIPRO fi 
							where fi.CODPRODUC = @atcCode
								And fi.NUMINGRES = @AdmissionNumber
								AND fi.TIPREGIST = 1
								AND fi.CODCENATE = @CareCenterCode
							order by fi.ID desc

								if @IDFisipro is null begin
									IF ISNULL(@ConsecutiveCrystal, '') = '' OR @ConsecutiveCrystal = '0' BEGIN
										-- Dosis estándar dispensada directamente (sin solicitud médica / sin HCFARMEPC).
										-- ConsecutiveCrystal = 0 confirma que no hay orden clínica de origen.
							-- Se omite el bloque NPT/PharmaDose - el movimiento de inventario ya ocurrió.
							-- La orden de servicio se genera desde la capa de presentación con los componentes del paquete.
										goto NextProductClass5
									END
									-- Si ConsecutiveCrystal != 0 pero sin HCFISIPRO: error legítimo (producto NPT sin inventario físico)
									SET @CodeMessageResult = '999'
									SET @ErrorsValidationResult = 'No se puede dispensar ya que existen items de producción que no cuentan con cantidades en el inventario físico para el ingreso indicado'
									SET @DispensingIdResult = 0
									SET @DispensingCodeResult = ''
									SET @StatusResult = 3
									GOTO Cleanup
							end

							-- Limpiamos los datos para cada iteración
							delete from #tmpPhysicalCum
							DELETE FROM #TmpGroupingCodes
							-- Para preparaciones NPT: HCFARMEPD contiene los medicamentos ingredientes de la preparación,
							-- no el producto terminado (PKG) ni el medicamento de referencia (@atcCode). El EntityId
							-- vincula la dispensación con la línea de solicitud. Se usa solo EntityId para el JOIN.
							insert into #tmpPhysicalCum
							select bs.BatchCode, bs.ExpirationDate, pdbs.Quantity, h.CodeSusceptibleMixingStation
							from Inventory.PharmaceuticalDispensingDetailBatchSerial pdbs (nolock)
							join Inventory.PhysicalInventory pin (nolock) on pdbs.PhysicalInventoryId = pin.Id
							join Inventory.BatchSerial bs (nolock) on pin.BatchSerialId = bs.Id
							JOIN HCFARMEPD h  ON h.ID = @EntityIdTmp
							where pdbs.PharmaceuticalDispensingDetailId = @PharmaceuticalDispensingDetailId
							AND bs.ProductId = @ProductIdTmp

							-- Validar que existen registros en #tmpPhysicalCum
							IF NOT EXISTS (SELECT 1 FROM #tmpPhysicalCum) BEGIN
								SET @CodeMessageResult = '999'
								SET @ErrorsValidationResult = 'No se encontraron lotes disponibles para el producto ' + @atcCode + ' en el inventario físico. Verifique que los lotes seleccionados correspondan al producto correcto.'
								SET @DispensingIdResult = 0
								SET @DispensingCodeResult = ''
								SET @StatusResult = 3
								GOTO Cleanup
							END

							SELECT TOP(1)
							@BatchCodeTmp = BatchCode,
							@BatchQuantityTmp = Quantity,
							@ExpirationDateTmp = ExpirationDate
							FROM #tmpPhysicalCum

						-- Validar que la cantidad es válida
							IF @BatchQuantityTmp IS NULL OR @BatchQuantityTmp <= 0 BEGIN
								SET @CodeMessageResult = '999'
							SET @ErrorsValidationResult = 'La cantidad del lote para el producto ' + @atcCode + ' no es válida. Cantidad: ' + ISNULL(CAST(@BatchQuantityTmp AS VARCHAR), 'NULL')
								SET @DispensingIdResult = 0
								SET @DispensingCodeResult = ''
								SET @StatusResult = 3
								GOTO Cleanup
							END

							SELECT
								@MeasurementUnitCode = ISNULL(hcf.CODUNIMED, hcp.CODUNIMED)
								,@Dose = ISNULL(hcf.DOSISPROD, hcp.DOSISPROD)
								,@CodProSal = hcf.CODPROSAL
								,@SourceTable = hcf.SourceTable
								,@IdSourceTable = hcf.IdSourceTable
							FROM HCFARMEPD hcf
							LEFT JOIN HCPRESCRA hcp ON hcp.ID = hcf.IdSourceTable AND hcf.SourceTable = 'HCPRESCRA'
							WHERE hcf.ID = @EntityIdTmp  -- Usar @EntityIdTmp que se asigna en cada iteración

							SELECT TOP 1
								@UnitDoseTypeId = rmsd.UnitDoseTypeId
							FROM MixingStation.RequestPackageDetailStatus rpds
							JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.Id = rpds.RequestMixingStationDetailId
							JOIN #tmpPhysicalCum tmp ON tmp.BatchCode = rpds.BatchCode
							WHERE rpds.ProductId = @ProductIdTmp

							IF EXISTS (SELECT 1 FROM #tmpPhysicalCum WHERE CodeSusceptibleMixingStation IS NULL) BEGIN

								SET @NewId = NEWID()
								INSERT INTO MedicalHistory.ProductSusceptibleMixingStation
								(CodeSusceptibleMixingStation
								,Origin
								,IdOrigin
								,FullProductName
								,ApplicationsNumber
								,MainDrugCode
								,CenterAttentionCode
								,FunctionalUnitCode
								,ProfessionalCode
								,CreationDate)
								VALUES(@NewId
								,ISNULL(NULLIF(RTRIM(@SourceTable), ''), N'HCFARMEPD')
								,ISNULL(@IdSourceTable, @EntityIdTmp)
								,@ProductName
								,@BatchQuantityTmp  -- ApplicationsNumber: cantidad física real del lote (no la del XML del detalle)
								,@atcCode
								,@CareCenterCode
								,@FunctionalUnitCode
								,@CodProSal
								,Common.GETDATE())

								UPDATE #tmpPhysicalCum
								SET CodeSusceptibleMixingStation = @NewId
								WHERE CodeSusceptibleMixingStation IS NULL

								-- Sincronizar HCFARMEPD para que ph_fallback en SP_GetProcessedMedicationItemsForBilling
								-- pueda encontrar los PharmaDose de esta dispensación.
								-- Escenario: dosis dividida (split-routing) donde SENDTO=1 (Farmacia) tiene
								-- CodeSusceptibleMixingStation=NULL hasta que se dispensa.
								-- Sin este UPDATE, el JOIN en ph_fallback retorna NULL y la fila queda excluida del tab.
								UPDATE HCFARMEPD
								SET CodeSusceptibleMixingStation = @NewId
								WHERE ID = @EntityIdTmp
								  AND CodeSusceptibleMixingStation IS NULL

							END

							IF NOT EXISTS (SELECT 1 FROM MedicalHistory.PharmaDose ph
														JOIN #tmpPhysicalCum tmp ON tmp.CodeSusceptibleMixingStation = ph.CodeSusceptibleMixingStation) BEGIN

								IF @NewId IS NULL BEGIN
									SET @NewId = (SELECT TOP 1 CodeSusceptibleMixingStation FROM #tmpPhysicalCum)
								END

								-- Limpiar #TmpGroupingCodes antes de insertar nuevos registros
								DELETE FROM #TmpGroupingCodes

								INSERT INTO MedicalHistory.PharmaDose
									(IDHCFARMEPC, CodeSusceptibleMixingStation, ProductCode, MeasurementUnitCode, UnitDoseTypeId, Dose,
									 GroupingCodeDose, DeliveryStatus, QuantityReceivable, AppliedDose, AppliedDateDose, MixingStationId, IsDispensed, IsTransformedProductStandardDispensation)
								OUTPUT @ProductIdTmp,1, @BatchCodeTmp, @ExpirationDateTmp, INSERTED.GroupingCodeDose, @NewId
								INTO #TmpGroupingCodes(ProductId,Quantity, BatchCode, ExpirationDate, GroupingCodeDose, CodeSusceptibleMixingStation)
								SELECT
									@ConsecutiveCrystal, @NewId, @atcCode, @MeasurementUnitCode, @UnitDoseTypeId, @Dose,
									NEWID(), 0, 0, 0, NULL, NULL, 1, 1
								FROM (SELECT TOP(@BatchQuantityTmp) 1 AS n FROM sys.all_objects) AS Numbers

								SET @Row = 1
								SELECT @MaxRows = COUNT(*) FROM #TmpGroupingCodes

								WHILE @Row <= @MaxRows
								BEGIN
									SELECT @RowProductId = ProductId, @RowBatchCode = BatchCode, @RowGroupingCodeDose = GroupingCodeDose
										FROM #TmpGroupingCodes WHERE RowId = @Row

										UPDATE TOP (1) rpds
										SET GroupingCodeDose = @RowGroupingCodeDose
										FROM MixingStation.RequestPackageDetailStatus rpds
										WHERE rpds.ProductId = @RowProductId AND rpds.BatchCode = @RowBatchCode AND rpds.GroupingCodeDose IS NULL

										SET @Row = @Row + 1
									END

								UPDATE kp SET CodeSusceptibleMixingStation = @NewId
								FROM HCKARDPAC kp
								JOIN #HCKARDPACConcecutives ik ON kp.NUMCONSEC = ik.NUMCONSEC AND kp.CODPRODUC = ik.CODPRODUC
								WHERE kp.CodeSusceptibleMixingStation IS NULL AND kp.CODPRODUC = @atcCode
							END

							-- Determinar si necesitamos limpiar o agregar a #TmpGroupingCodes
							IF @SendTo = 1 BEGIN
								-- Para @SendTo = 1, AGREGAR registros existentes (no limpiar)
								IF EXISTS (SELECT 1 FROM #TmpGroupingCodes) BEGIN

									INSERT INTO #TmpGroupingCodes(ProductId,Quantity, BatchCode, ExpirationDate, GroupingCodeDose, CodeSusceptibleMixingStation)
										SELECT
											@ProductIdTmp,
											@BatchQuantityTmp,
											tpc.BatchCode,
											tpc.ExpirationDate,
											pd.GroupingCodeDose,
											tpc.CodeSusceptibleMixingStation
										FROM #tmpPhysicalCum tpc
										JOIN MedicalHistory.PharmaDose pd ON pd.CodeSusceptibleMixingStation = tpc.CodeSusceptibleMixingStation
										WHERE pd.ProductCode = @atcCode
										  AND NOT EXISTS (
											  SELECT 1 FROM #TmpGroupingCodes tc
											  WHERE tc.ProductId = @ProductIdTmp AND tc.BatchCode = tpc.BatchCode AND tc.GroupingCodeDose = pd.GroupingCodeDose);
								END -- Fin IF EXISTS
							END -- Fin IF @SendTo = 1
								ELSE BEGIN
									-- Para @SendTo <> 1 (Central de Mezclas), LIMPIAR antes de insertar
									DELETE from #TmpGroupingCodes
									INSERT INTO #TmpGroupingCodes(ProductId,Quantity, BatchCode, ExpirationDate, GroupingCodeDose, CodeSusceptibleMixingStation)
									SELECT
										@ProductIdTmp,
										@BatchQuantityTmp,
										tpc.BatchCode,
										tpc.ExpirationDate,
										NULL,
										tpc.CodeSusceptibleMixingStation
									FROM #tmpPhysicalCum tpc
							END -- Fin ELSE @SendTo

							-- ============================================================
							-- REEMPLAZO DE CURSOR POR OPERACIONES SET-BASED
							-- ============================================================

							-- Completar GroupingCodeDose NULL en #TmpGroupingCodes
							UPDATE tgc
							SET GroupingCodeDose = pd.GroupingCodeDose
							FROM #TmpGroupingCodes tgc
							JOIN MixingStation.RequestPackageDetailStatus rpds ON
								rpds.BatchCode = IIF(@SendTo = 1, @BatchCodeTmp, tgc.BatchCode)
								AND rpds.ProductId = tgc.ProductId
							JOIN MedicalHistory.PharmaDose pd ON
								pd.GroupingCodeDose = rpds.GroupingCodeDose
							WHERE tgc.GroupingCodeDose IS NULL
								AND pd.CodeSusceptibleMixingStation = tgc.CodeSusceptibleMixingStation

							-- Actualizar registros existentes en DetailPhysicalCUM
							UPDATE dpc
							SET DispensedQuantity = dpc.DispensedQuantity + tgc.Quantity
							FROM MedicalHistory.DetailPhysicalCUM dpc
							JOIN #TmpGroupingCodes tgc ON
								dpc.IDHCFISIPRO = @IDFisipro
								AND dpc.ProductId = tgc.ProductId
								AND dpc.GroupingCodeDose = tgc.GroupingCodeDose
								AND dpc.BatchCode = IIF(@SendTo = 1, @BatchCodeTmp, tgc.BatchCode)

							-- Insertar nuevos registros en DetailPhysicalCUM
							INSERT INTO MedicalHistory.DetailPhysicalCUM
								(IDHCFISIPRO, BatchCode, ProductId, DateExpiration, [Hour], DispensedQuantity, GroupingCodeDose, UsedQuantity)
							SELECT
								@IDFisipro,
								IIF(@SendTo = 1, @BatchCodeTmp, tgc.BatchCode),
								tgc.ProductId,
								IIF(@SendTo = 1, @ExpirationDateTmp, tgc.ExpirationDate),
								Common.GETDATE(),
								tgc.Quantity,
								tgc.GroupingCodeDose,
								0
							FROM #TmpGroupingCodes tgc
							WHERE NOT EXISTS (
								SELECT 1
								FROM MedicalHistory.DetailPhysicalCUM dpc
								WHERE dpc.IDHCFISIPRO = @IDFisipro
									AND dpc.ProductId = tgc.ProductId
									AND dpc.GroupingCodeDose = tgc.GroupingCodeDose
									AND dpc.BatchCode = IIF(@SendTo = 1, @BatchCodeTmp, tgc.BatchCode)
							)

							-- Actualizar PharmaDose para marcar como entregado
							UPDATE pd
							SET DeliveryStatus = 1
							FROM MedicalHistory.PharmaDose pd
							WHERE pd.IsDispensed = 1
								AND EXISTS (
									SELECT 1 FROM #TmpGroupingCodes tgc
									WHERE tgc.GroupingCodeDose = pd.GroupingCodeDose
								)

							-- Actualizar HCKARDPAC con IdDetailPhysicalCUM y DESMOVPRO
							DELETE FROM #DetailCUMMapping

							-- Insertar con ROW_NUMBER para ordenar los lotes por producto/mezcla
							INSERT INTO #DetailCUMMapping
							SELECT
								ROW_NUMBER() OVER (PARTITION BY tgc.CodeSusceptibleMixingStation ORDER BY dpc.BatchCode, dpc.ID) as RowNum,
								dpc.ProductId,
								dpc.BatchCode,
								dpc.GroupingCodeDose,
								dpc.ID,
								tgc.CodeSusceptibleMixingStation
							FROM MedicalHistory.DetailPhysicalCUM dpc
							JOIN #TmpGroupingCodes tgc ON
								dpc.IDHCFISIPRO = @IDFisipro
								AND dpc.ProductId = tgc.ProductId
								AND dpc.GroupingCodeDose = tgc.GroupingCodeDose
								AND dpc.BatchCode = IIF(@SendTo = 1, @BatchCodeTmp, tgc.BatchCode)

							-- Ahora actualizar HCKARDPAC usando el mapeo con ROW_NUMBER
							-- Esto asegura que cada registro de HCKARDPAC se actualice con su lote específico
							;WITH HCKARDPAC_Numbered AS (
								SELECT
									kp.NUMCONSEC,
									kp.CODPRODUC,
									kp.CodeSusceptibleMixingStation,
									ROW_NUMBER() OVER (PARTITION BY kp.CodeSusceptibleMixingStation ORDER BY kp.NUMCONSEC) as RowNum
								FROM HCKARDPAC kp
								JOIN #HCKARDPACConcecutives ik ON
									kp.NUMCONSEC = ik.NUMCONSEC
									AND kp.CODPRODUC = ik.CODPRODUC
									AND kp.CodeSusceptibleMixingStation = ik.CodeSusceptibleMixingStation
								WHERE kp.IdDetailPhysicalCUM IS NULL
									AND kp.CODPRODUC = @atcCode)

							UPDATE kp
							SET
								IdDetailPhysicalCUM = dcm.IdDetailPhysicalCUM,
								DESMOVPRO = 'Despacho farmacia, origen solicitud: Producto procesado por la central de mezclas - usuario: (' + @UserName + ') - lote: ' + dcm.BatchCode
							FROM HCKARDPAC kp
							JOIN HCKARDPAC_Numbered kpn ON
								kp.NUMCONSEC = kpn.NUMCONSEC
								AND kp.CODPRODUC = kpn.CODPRODUC
								AND kp.CodeSusceptibleMixingStation = kpn.CodeSusceptibleMixingStation
							JOIN #DetailCUMMapping dcm ON
								dcm.CodeSusceptibleMixingStation = kpn.CodeSusceptibleMixingStation
								AND dcm.RowNum = kpn.RowNum
							WHERE kp.IdDetailPhysicalCUM IS NULL
								AND kp.CODPRODUC = @atcCode

							-- ============================================================
							-- Operaciones SET-BASED completadas
							-- ============================================================
							END
						END

						NextProductClass5:
						SET @RowId += 1
					END
				end
				--**********************

				SET @ExecutionNow = SYSUTCDATETIME()
				SET @ExecutionStepOrder += 1
				INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
					(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
				VALUES
					(@ExecutionId, @ExecutionStepOrder, 'ESTACION_MEZCLAS_CLASE_5', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, NULL)
				SET @ExecutionPreviousAt = @ExecutionNow

				GOTO Cleanup
			END --- Fin si esta confirmando
		END --- Fin si vienen dispensaciones


		-- valido si afecta o no el inventario
		if @AffectInventory <> 0 begin
			if Exists (
				select 1
				from Inventory.PharmaceuticalDispensingDetail pd 
				where pd.PharmaceuticalDispensingId = @IdPharmaceutical
					and not exists
					(
						select 1
						from Inventory.PharmaceuticalDispensingDetailBatchSerial bs 
						where bs.PharmaceuticalDispensingDetailId = pd.Id
					)
			) begin
				set @CodeMessageResult = '999'
				set @ErrorsValidationResult = 'La estructura presenta inconsistencias por favor contacte al administrador'
				set @DispensingIdResult = 0
				set @DispensingCodeResult = ''
				set @StatusResult = 3
				GOTO Cleanup
			END
		END
		IF EXISTS (SELECT pd.Id FROM Inventory.PharmaceuticalDispensingDetail pd 
			INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pdb  ON pd.Id = pdb.PharmaceuticalDispensingDetailId
			INNER JOIN Inventory.PhysicalInventory pin  ON pin.Id = pdb.PhysicalInventoryId
			WHERE pd.PharmaceuticalDispensingId = @IdPharmaceutical AND pd.ProductId <> pin.ProductId
			AND pdb.PhysicalInventoryId IS NOT NULL) BEGIN
			SET @CodeMessageResult = '999'
			SET @ErrorsValidationResult = 'Los productos no coinciden entre el detalle y subdetalle por favor contacte al administrador'
			SET @DispensingIdResult = 0
			SET @DispensingCodeResult = ''
			SET @StatusResult = 3
			GOTO Cleanup
		END

		IF EXISTS (SELECT pd.Id FROM Inventory.PharmaceuticalDispensingDetail pd 
			INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pdb  ON pd.Id = pdb.PharmaceuticalDispensingDetailId
			INNER JOIN Inventory.PhysicalInventoryCustody pin  ON pin.Id = pdb.PhysicalInventoryCustodyId
			WHERE pd.PharmaceuticalDispensingId = @IdPharmaceutical AND pd.ProductId <> pin.ProductId
			AND pdb.PhysicalInventoryCustodyId IS NOT NULL) BEGIN
			SET @CodeMessageResult = '999'
			SET @ErrorsValidationResult = 'Los productos no coinciden entre el detalle y subdetalle por favor contacte al administrador'
			SET @DispensingIdResult = 0
			SET @DispensingCodeResult = ''
			SET @StatusResult = 3
			GOTO Cleanup
		END

		SET @ExecutionNow = SYSUTCDATETIME()
		SET @ExecutionStepOrder += 1
		INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
			(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
		VALUES
			(@ExecutionId, @ExecutionStepOrder, 'VALIDACIONES_FINALES', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, NULL)
		SET @ExecutionPreviousAt = @ExecutionNow

		SET @CodeMessageResult = '0'
		SET @ErrorsValidationResult = 'Se guardo correctamente la dispensacion ' + @CodePharmaceutical
		SET @DispensingIdResult = ISNULL(@IdPharmaceutical,0)
		SET @DispensingCodeResult = ISNULL(@CodePharmaceutical, '')
		SET @StatusResult = 1

	END TRY
	BEGIN CATCH
		BEGIN TRY
			SET @ExecutionNow = SYSUTCDATETIME()
			SET @ExecutionStepOrder += 1
			INSERT INTO Inventory.PharmaceuticalDispensingExecutionLog
				(ExecutionId, StepOrder, StepName, StartedAt, EndedAt, PharmaceuticalDispensingId, DispensingCode, AdmissionNumber, DetailRows, BatchRows, UserCode, Observation)
			VALUES
				(@ExecutionId, @ExecutionStepOrder, 'ERROR_CATCH', @ExecutionPreviousAt, @ExecutionNow, @IdPharmaceutical, @CodePharmaceutical, @AdmissionNumber, (SELECT COUNT(1) FROM #TablePharmaceuticalDetail), (SELECT COUNT(1) FROM #TablePharmaceuticalBatch), @user, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)))
		END TRY
		BEGIN CATCH
		END CATCH

		SET @CodeMessageResult = '999'
		SET @ErrorsValidationResult = (SELECT ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)))
		SET @DispensingIdResult = 0
		SET @DispensingCodeResult = ''
		SET @StatusResult = 3
	END CATCH

Cleanup:
	DROP TABLE IF EXISTS #DetailCUMMapping
	DROP TABLE IF EXISTS #TmpGroupingCodes
	DROP TABLE IF EXISTS #tmpPhysicalCum
	DROP TABLE IF EXISTS #ValidacionInventarioFisico
	DROP TABLE IF EXISTS #TablePharmaceuticalBatch
	DROP TABLE IF EXISTS #TableDeliveriesByPharmacyProductType
	DROP TABLE IF EXISTS #TablePharmaceuticalDetail
	DROP TABLE IF EXISTS #HCKARDPACConcecutives
	RETURN
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera y confirma las dispensaciones farmacéuticas de salida desde el inventario. Gestiona la entrega de medicamentos e insumos a pacientes hospitalizados o en urgencias, registrando detalle de productos, lotes, cantidades, precios, descuentos e impuestos. Integra información del catálogo de productos (InventoryProduct), datos del paciente y su ingreso hospitalario (CHREGESTA, HCINGRESORECNAC), terceros como aseguradoras y entidades (ThirdParty), y el usuario que ejecuta la dispensación (Security.User / Security.Person). También permite anular ítems desde el tablero de farmacia y soporta integraciones externas de dispensación (Medilaser), devolviendo como resultado el código e identificador de la dispensación generada junto con mensajes de validación y estado del proceso.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePharmaceuticalDispensing_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePharmaceuticalDispensing_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procesa anulaciones masivas de solicitudes de farmacia desde el dashboard y guarda/confirma dispensaciones farmacéuticas, afectando inventario, kardex, hoja de gasto QX, contabilidad y orden de servicio según integración (nativa, HEON o Medilaser).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDispensing_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el usuario en Security.User unido a Security.Person para resolver el nombre completo del usuario.; El XML de dispensación debe traer Id, IsPharmaceuticalDispensing y DispensingIntegration; de lo contrario se retorna error 999.; No se permite dispensar en un periodo cerrado (DocumentDate menor al primer día del Year/Month en Inventory.SettingInventory).; Cada item a anular debe traer CodProducto y motivo de anulación (HCMOANULBId); si falta se retorna error 999.; Si DispensingIntegration=2 (HEON) deben venir OfficeType, LogisticOperator y MedicalOrderRecipe.; Cuando la dispensación afecta inventario, los productos cuyo subgrupo maneja lotes (HandlesBatch=1) y no provienen de almacén virtual deben tener detalles de lote en #TablePharmaceuticalBatch.; Los productos no pueden estar vencidos (BatchSerial.ExpirationDate >= DocumentDate).; No se permite dispensar desde almacenes en tránsito (Warehouse.TransitStore=1).; Cuando DispensingWithoutAuthorization=0 y el producto es NO POS, la cantidad dispensada no puede superar el saldo autorizado MIPRES (HCJUNOPOM).; Para integración HEON, una orden médica no puede tener un proceso pendiente previo en ControlIntegrationHeonDetail con Status=2.; Los productos deben tener AverageCost > 0 (excepto si están en custodia).; Las cantidades de los detalles deben ser > 0.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDispensing_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDispensing_Output';
-- GO
