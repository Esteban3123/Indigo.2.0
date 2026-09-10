
-- =============================================
-- Author:		Cristhian Mauricio Salazar Narvaez
-- Create date: 15/04/2016
-- Description:	Procedimiento para guardar o confirmar las dispensaciones farmaceuticas
-- =============================================
CREATE PROCEDURE [Inventory].[SP_GeneratePharmaceuticalDispensing_OutputOptimized]
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
	declare @HCKARDPACConcecutives Table(
		[NUMCONSEC] varchar(50) not null
	)

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

	DECLARE @TablePharmaceuticalDetail TABLE
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
		EconommicActivityId int
	)

	DECLARE @TableDeliveriesByPharmacyProductType table
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

	DECLARE @TablePharmaceuticalBatch table
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

	DECLARE @MessageReturn varchar(max) = ''
	DECLARE @GetDateTime DATETIME = Common.GETDATE()

	BEGIN TRY 
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
						@HCFARMEPDId as INT,
						@ExtramuralDel BIT = NULL,
						@AdministrationRoute as VARCHAR(250),
						@ProccesType TINYINT

				Update dbo.HCFARMEPD Set CANPENPRO = 0, PROESTADO = '3' Where CODCONCEC = @ConsecutivoDel and CODPRODUC = @CodProduct

				select @HCFARMEPDId  = Id,@ExtramuralDel = COALESCE(EXTRAMURAL,0)  from HCFARMEPD where NUMINGRES = @AdmissionNumberDel and CODPRODUC = @CodProduct
				
				SELECT TOP 1 @AdministrationRoute = hp.DESADMINI 
				FROM HCFARMEPD hc
				JOIN HCPRESCRA hp ON hp.ID = hc.IdSourceTable AND hc.SourceTable = 'HCPRESCRA'
				WHERE hc.NUMINGRES = @AdmissionNumberDel AND hc.CODPRODUC = @CodProduct

				SELECT 
					@ProccesType =
						CASE hc.SourceTable
							WHEN 'HCPRESCRA' THEN 1  
							WHEN 'HCINFLIQA' THEN 2  
							ELSE 1  -- Por defecto, si es nulo o cualquier otro valor
						END 
				FROM HCFARMEPD hc
				WHERE hc.NUMINGRES = @AdmissionNumberDel AND hc.CODPRODUC = @CodProduct

				/***SEGMENTO AUDITORIA MedicalHistory***/				
				IF @EntityNameDel = 'DashboardPharmacyDetail' and @ExtramuralDel = 0 AND EXISTS(SELECT 1 FROM Inventory.ATC WITH(NOLOCK) WHERE Code = @CodProduct) BEGIN
					
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

				If Not Exists (Select CODCONCEC From dbo.HCFARMEPD Where CODCONCEC = @ConsecutivoDel and CANPENPRO > @Cero ) Begin
					Update dbo.HCFARMEPC Set ORDESTADO = '3' where CODCONCEC = @ConsecutivoDel
					
					if @HCMOANULBId is not null and @HCMOANULBId <> '' begin
						INSERT INTO [Inventory].[ReasonCancellationOfRequest] values(@ConsecutivoDel,@HCMOANULBId,@Description,@user,[Common].[GETDATE](),null)
					END
					ELSE BEGIN
						set @CodeMessageResult = '999'
						set @ErrorsValidationResult = 'No hay motivo de anulación'
						set @DispensingIdResult = 0
						set @DispensingCodeResult = ''
						set @StatusResult = 3
						RETURN
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
							RETURN
						end
				end

				SET @RowId += 1
			END
		END -- Fin si hay anulaciones
		
		--- Valido si vienen dispensaciones
		IF Exists (SELECT t.x.value('Id[1]','int') FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x) where t.x.value('Id[1]','int') Is Not Null) Begin
			if IsNull((select TOP 1 t.x.value('IsPharmaceuticalDispensing[1]','varchar') from @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x)), '') = '' Begin
				--select '999' as CodeMessage, 'No se encuentra el parametro IsPharmaceuticalDispensing' as Message, 0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
				set @CodeMessageResult = '999'
				set @ErrorsValidationResult = 'No se encuentra el parametro IsPharmaceuticalDispensing'
				set @DispensingIdResult = 0
				set @DispensingCodeResult = ''
				set @StatusResult = 3
				RETURN
			END

			IF ISNULL((SELECT TOP 1 t.x.value('DispensingIntegration[1]','varchar') FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x)), '') = '' BEGIN
				--SELECT '999' as CodeMessage, 'No se encuentra el parametro DispensingIntegration' as Message, 0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
				set @CodeMessageResult = '999'
				set @ErrorsValidationResult = 'No se encuentra el parametro DispensingIntegration'
				set @DispensingIdResult = 0
				set @DispensingCodeResult = ''
				set @StatusResult = 3
				RETURN
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
				RETURN
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
			INSERT INTO @TablePharmaceuticalDetail 
					(ChangeTracker, Id, IdTmp, CareGroupId, HealthAdministratorId, ThirdPartyId, ProductId, CodeProduct, MedicamentCode, WarehouseId, Quantity, ServiceDate, FunctionalUnitId, CostCenterId,
					OrderedHealthProfessionalCode, OrderedProfessionalSpecialty, OrderedHealthProfessionalThirdPartyId, AuthorizationNumber, LiquidationType, SurchargeApply,
					SalePrice, AverageCost,FinalProductCost ,DiscountPercentage, DiscountValue, TotalSalesPrice, GrandTotalSalesPrice, ProductType, CantidadSolicitada, CantidadPendiente, idProductHeon, recetarioOMedica,
					GuardaGastoQX,IdProgramacionQXPrincipal,Extramural, QuotationPharmaceuticalDispensingDetailId, EntityId, EntityName, CantidadMezcla,GrossValue,TaxValue,IvaId,Note)
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
					t.x.value('Note[1]','Varchar(500)')
				from @XmlPharmaceutical.nodes('/PharmaceuticalDispensing/PharmaceuticalDispensingDetail') t(x)

			declare @DispensingWithoutAuthorization bit
			select top 1 @DispensingWithoutAuthorization = DispensingWithoutAuthorization from Inventory.SettingInventory where OperatingUnitId = @OperatingUnitId
						
			--validar cantidad autorizada contra la enviada			
			if isnull(@DispensingWithoutAuthorization, 1) = 0 and exists (select 1
				from @TablePharmaceuticalDetail td
				join IHLISTPRO LP (nolock) on td.MedicamentCode = LP.CODPRODUC
				left join (
					SELECT P.NUMINGRES, P.CODPRODUC, SUM(P.CANPEDPRO) - ISNULL([dbo].[fnCalcularCantidadDispensada](p.NUMINGRES, P.CODPRODUC), 0) as SaldoAutorizadoMipres
					FROM dbo.HCJUNOPOM P WITH (NOLOCK)
					where p.NUMINGRES = @AdmissionNumber and P.CODMINSALUD <> ''
					GROUP BY P.NUMINGRES, P.CODPRODUC
				) ap on ap.NUMINGRES = @AdmissionNumber and ap.CODPRODUC = td.MedicamentCode
				where LP.NOPOSPROD = 1 and isnull(ap.SaldoAutorizadoMipres, 0) < td.Quantity) begin
				
				INSERT INTO @TableErrors 
					SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', td.CodeProduct, '(Almacen: ', w.Code, ' - ', w.Name, ')')
					from @TablePharmaceuticalDetail td
					JOIN Inventory.Warehouse w ON td.WarehouseId = w.Id
					join IHLISTPRO LP (nolock) on td.MedicamentCode = LP.CODPRODUC
					left join (
						SELECT P.NUMINGRES, P.CODPRODUC, SUM(P.CANPEDPRO) - ISNULL([dbo].[fnCalcularCantidadDispensada](p.NUMINGRES, P.CODPRODUC), 0) as SaldoAutorizadoMipres
						FROM dbo.HCJUNOPOM P WITH (NOLOCK)
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
				return
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
					from @TablePharmaceuticalDetail
				end
			end
			
			--Validamos que no existan dispensaciones usando el almacén en tránsito
			IF EXISTS 
			(
				SELECT 1 
				FROM @TablePharmaceuticalDetail pdd
				JOIN Inventory.Warehouse w ON pdd.WarehouseId = w.Id
				WHERE w.TransitStore = 1
			)
			BEGIN
				INSERT INTO @TableErrors 
					SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', pdd.CodeProduct, '(Almacen: ', w.Code, ' - ', w.Name, ')')
					FROM @TablePharmaceuticalDetail pdd
					JOIN Inventory.Warehouse w ON pdd.WarehouseId = w.Id
					WHERE w.TransitStore = 1
						
				Select @ErrorsValidation = COALESCE(@ErrorsValidation + '', '') + [message]  from @TableErrors

				set @CodeMessageResult = '999'
				set @ErrorsValidationResult = 'Los siguientes productos no pueden ser dispensados desde un almacén de tránsito: ' + CHAR(13) + CHAR(10) + ISNULL(@ErrorsValidation, '')
				set @DispensingIdResult = 0
				set @DispensingCodeResult = ''
				set @StatusResult = 3
				RETURN
			END				
			
			--Si viene de integración con HEON, se valida que la orden médica de los productos no estén ya dentro de la tabla de control
			if @DispensingIntegration = 2
			begin
				If Exists (Select td.Id
					from @TablePharmaceuticalDetail td
					inner join Inventory.ControlIntegrationHeonDetail cihd With(Nolock) on cihd.MedicalOrderRecipe = td.recetarioOMedica
					inner join Inventory.ControlIntegrationHeon cih With(Nolock) on cih.Id = cihd.ControlIntegrationHeonId
					where cihd.[Status] = @Dos)
				Begin
					Insert Into @TableErrors 
					select Concat('- La orden médica ',cihd.MedicalOrderRecipe,' del producto ',td.CodeProduct,' ya tiene un proceso pendiente en las tablas de control de integración. Quedó pendiente en procesar por ',cihd.[Message],'.   ')
					from @TablePharmaceuticalDetail td
					inner join Inventory.ControlIntegrationHeonDetail cihd With(Nolock) on cihd.MedicalOrderRecipe = td.recetarioOMedica
					inner join Inventory.ControlIntegrationHeon cih With(Nolock) on cih.Id = cihd.ControlIntegrationHeonId
					where cihd.[Status] = @Dos
						
					Select @ErrorsValidation = COALESCE(@ErrorsValidation + '', '') + [message]  from @TableErrors
					--select '999' as CodeMessage, @ErrorsValidation as Message, 0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
					set @CodeMessageResult = '999'
					set @ErrorsValidationResult = @ErrorsValidation
					set @DispensingIdResult = 0
					set @DispensingCodeResult = ''
					set @StatusResult = 3
					RETURN
				END
			END
			---- se actualiza el costo promedio y Ultimo Costo
			--update @TablePharmaceuticalDetail set AverageCost = (select ProductCost from Inventory.InventoryProduct where Id = ProductId)
			update tpd SET	tpd.AverageCost = ip.ProductCost,
							tpd.FinalProductCost =ip.FinalProductCost
			from @TablePharmaceuticalDetail tpd
			JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON tpd.ProductId =ip.Id

			--- Inserto la informacion de las entregas
			insert into @TableDeliveriesByPharmacyProductType (PharmaceuticalDispensingDetailIdTmp,HCFARMEPDID,ProductId, ProductType, Quantity,ConcentrationByUnit,TotalWeight, QuantityDeliveryPT)
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
			insert into @TablePharmaceuticalBatch (ChangeTracker,Id,PharmaceuticalDetailIdTmp,PharmaceuticalDispensingDetailId,PhysicalInventoryId,Quantity,OutstandingQuantity,PhysicalInventoryCustodyId)
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

			update @TablePharmaceuticalBatch set PhysicalInventoryCustodyId = null where PhysicalInventoryCustodyId = 0
			update @TablePharmaceuticalBatch set PhysicalInventoryId = null where PhysicalInventoryId = 0

			update @TablePharmaceuticalDetail set PhysicalInventoryCustodyId = tpb.PhysicalInventoryCustodyId
			from @TablePharmaceuticalBatch tpb
			where [@TablePharmaceuticalDetail].IdTmp = tpb.PharmaceuticalDetailIdTmp
			
			-- Actualizamos la cantidad de la cabecera de acuerdo a los lotes
			update pdd
				set pdd.Quantity = IIF(w.VirtualStore = 1, ISNULL(pddbs.Quantity, pdd.Quantity),  ISNULL(pddbs.Quantity, 0)),
					pdd.GrandTotalSalesPrice = IIF(w.VirtualStore = 1, ISNULL(pddbs.Quantity, pdd.Quantity),  ISNULL(pddbs.Quantity, 0)) * pdd.TotalSalesPrice
			from @TablePharmaceuticalDetail pdd
			JOIN Inventory.Warehouse w ON pdd.WarehouseId = w.Id
			LEFT JOIN
			(
				SELECT PharmaceuticalDetailIdTmp, SUM(Quantity) Quantity
				FROM @TablePharmaceuticalBatch
				GROUP BY PharmaceuticalDetailIdTmp
			) pddbs ON pdd.IdTmp = pddbs.PharmaceuticalDetailIdTmp

			--Validamos que no existan detalles con cantidad en 0
			If Exists (Select 1 from @TablePharmaceuticalDetail where Quantity <= 0)
			Begin
				Insert Into @TableErrors 
				select Concat('- El producto ',td.CodeProduct,' no tiene cantidades válidas. ')
				from @TablePharmaceuticalDetail td
				where td.Quantity <= 0
						
				Select @ErrorsValidation = COALESCE(@ErrorsValidation + '', '') + [message]  from @TableErrors
				--select '999' as CodeMessage, @ErrorsValidation as Message, 0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
				set @CodeMessageResult = '999'
				set @ErrorsValidationResult = @ErrorsValidation
				set @DispensingIdResult = 0
				set @DispensingCodeResult = ''
				set @StatusResult = 3
				RETURN
			END
			
			IF @IsPharmaceuticalDispensing = 0 
			BEGIN
				--Siempre afecto inventario, solo no lo hago si todos los detalles provienen de almacenes virtuales
				IF EXISTS (SELECT pdd.RowId FROM @TablePharmaceuticalDetail pdd JOIN Inventory.WareHouse wh WITH (NOLOCK) ON pdd.WareHouseId = wh.Id WHERE wh.VirtualStore = @Cero) 
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
					select pd.RowId from @TablePharmaceuticalDetail pd
					inner join Inventory.Warehouse w with(nolock) ON pd.WarehouseId = w.Id
					inner join Inventory.InventoryProduct pr with(nolock) on pd.ProductId = pr.Id
					inner join Inventory.ProductSubGroup ps with(nolock) on ps.Id = pr.ProductSubGroupId
					where w.VirtualStore = @Cero 
						AND ps.HandlesBatch = @Uno 
						AND ChangeTracker <> @Deleted
						AND pd.IdTmp not in (select PharmaceuticalDetailIdTmp from @TablePharmaceuticalBatch) 
				) 
				begin

					set @CodeMessageResult = '999'
					set @ErrorsValidationResult = 'los productos que se van a dispensar no tiene detalles de lotes, por favor contacte al Administrador del Sistema'
					set @DispensingIdResult = 0
					set @DispensingCodeResult = ''
					set @StatusResult = 3
					RETURN
				END

				IF EXISTS 
				(
					SELECT 1
					FROM @TablePharmaceuticalDetail pdd
					JOIN @TablePharmaceuticalBatch pddbs ON pdd.IdTmp = pddbs.PharmaceuticalDetailIdTmp
					JOIN Inventory.PhysicalInventory phy ON pddbs.PhysicalInventoryId = phy.Id
					JOIN Inventory.BatchSerial bs ON phy.BatchSerialId = bs.Id
					WHERE CAST(@DocumentDate AS DATE) > bs.ExpirationDate
				) 
				begin
					INSERT INTO @TableErrors 
						SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', pdd.CodeProduct, '(Lote: ', bs.BatchCode, ')')
						FROM @TablePharmaceuticalDetail pdd
						JOIN @TablePharmaceuticalBatch pddbs ON pdd.IdTmp = pddbs.PharmaceuticalDetailIdTmp
						JOIN Inventory.PhysicalInventory phy ON pddbs.PhysicalInventoryId = phy.Id
						JOIN Inventory.BatchSerial bs ON phy.BatchSerialId = bs.Id
						WHERE CAST(@DocumentDate AS DATE) > bs.ExpirationDate
						
					Select @ErrorsValidation = COALESCE(@ErrorsValidation + '', '') + [message]  from @TableErrors

					set @CodeMessageResult = '999'
					set @ErrorsValidationResult = 'Los siguientes productos no pueden ser dispensados por que se encuentran vencidos: ' + CHAR(13) + CHAR(10) + ISNULL(@ErrorsValidation, '')
					set @DispensingIdResult = 0
					set @DispensingCodeResult = ''
					set @StatusResult = 3
					RETURN
				END
			END
			
			--valido que los productos tengan valor en AverageCost
			if Exists (select Id from @TablePharmaceuticalDetail where AverageCost = @Cero and ChangeTracker <> @Deleted AND PhysicalInventoryCustodyId is NULL) begin
				Insert Into @TableErrors 
				select Concat('El detalle con el producto ',p.Code,' - ',p.[Name],', tiene el costo promedio en $0',CHAR(13),CHAR(10)) From @TablePharmaceuticalDetail pdd
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
				RETURN
			END

			/*--------------- insercion o update:notas del quimico farmaceutico--------------------*/
			IF (SELECT COUNT(*) FROM @TablePharmaceuticalDetail tpd WHERE tpd.Note is not null AND tpd.Note <> '') > 0 BEGIN
				update pdn
					set pdn.Note = tpd.Note						
				from @TablePharmaceuticalDetail tpd
				JOIN Inventory.ATC atc WITH(NOLOCK) ON tpd.CodeProduct = atc.Code
				JOIN  HCFARMEPD h WITH(NOLOCK) on tpd.CodeProduct = h.CODPRODUC and tpd.EntityId = h.ID and tpd.EntityName = 'HCFARMEPD'
				JOIN Inventory.PharmaceuticalDispensingNotes pdn WITH(NOLOCK) ON pdn.AdmissionNumber = @AdmissionNumber AND pdn.ATCId =atc.Id AND h.ID = pdn.PharmaceuticalRequestDetailId
				WHERE tpd.Note is not null AND tpd.Note <> ''
	
				insert into Inventory.PharmaceuticalDispensingNotes 	
				select	h.ID,
						tpd.Note,
						@user,
						Common.GETDATE(),
						atc.Id,
						@AdmissionNumber						
				from @TablePharmaceuticalDetail tpd
				JOIN Inventory.ATC atc WITH(NOLOCK) ON tpd.CodeProduct = atc.Code
				JOIN  HCFARMEPD h WITH(NOLOCK) on tpd.CodeProduct = h.CODPRODUC and tpd.EntityId = h.ID and tpd.EntityName = 'HCFARMEPD'
				LEFT JOIN Inventory.PharmaceuticalDispensingNotes pdn WITH(NOLOCK) ON pdn.AdmissionNumber = @AdmissionNumber AND pdn.ATCId =atc.Id AND h.ID = pdn.PharmaceuticalRequestDetailId
				WHERE tpd.Note is not null AND tpd.Note <> '' AND pdn.Id is NULL	
			END
			-----------------------------------------------------------------------------------------
			--- si es nuevo
			if @IdPharmaceutical = 0 begin
				--- Obtenemos la secuencia numerica
				if IsNull(@CodePharmaceutical, '') = '' begin
					if Not Exists (select Id from Inventory.InventorySequence With(Nolock) where IdForm = @IdForm322) begin
						--select '999' as CodeMessage, 'No existe secuencia numerica para el formulario de Dispensación Farmaceutica' as Message, 
						--0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
						set @CodeMessageResult = '999'
						set @ErrorsValidationResult = 'No existe secuencia numerica para el formulario de Dispensación Farmaceutica'
						set @DispensingIdResult = 0
						set @DispensingCodeResult = ''
						set @StatusResult = 3
						RETURN
					END

					declare @idSequenceDetail int, @pattern varchar(300), @NextS bigint, @Scope varchar(5), @IdSequence int
						, @IdSequenceCommon int, @Prefix varchar(20) = ''

					select @IdSequence = Id, @Scope = Scope, @IdSequenceCommon = IdSequence from Inventory.InventorySequence With(Nolock) Where IdForm = @IdForm322
					
					if @Scope = 'O' begin --- Secuencia por Prefijo
						Select Top 1 @pattern = cs.Pattern
							, @idSequenceDetail = psd.Id  
						From Inventory.InventorySequenceDetail psd With(Nolock)
						Inner Join Common.Sequense cs With(Nolock) on cs.Id = psd.IdSequense 
						Where psd.InventorySequenceId = @IdSequence
					end
					ELSE BEGIN -- Secuencia por Unidad operativa
						SELECT @pattern = cs.Pattern, @idSequenceDetail = psd.Id  
						FROM Inventory.InventorySequenceDetail psd WITH(NOLOCK)
						INNER JOIN Common.Sequense cs WITH(NOLOCK) ON cs.Id = psd.IdSequense 
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
				if (select count(*) from Billing.ServiceOrder With(Nolock) where EntityCode = @CodePharmaceutical and EntityName = @strPharmaceuticalDispensing) > 0 begin
					--select '999' as CodeMessage, 'Esta dispensación ya generó una Órden de servicio' as Message, 
						--0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
					set @CodeMessageResult = '999'
					set @ErrorsValidationResult = 'Esta dispensación ya generó una Órden de servicio'
					set @DispensingIdResult = 0
					set @DispensingCodeResult = ''
					set @StatusResult = 3
					RETURN
				END

				IF @Status = 2 begin
					update Inventory.PharmaceuticalDispensing set ModificationUser = @user, ModificationDate = [Common].[GETDATE](), ConfirmationUser = @user, ConfirmationDate = [Common].[GETDATE]() where Id = @IdPharmaceutical
				end
				ELSE IF @Status = 3 begin
					update Inventory.PharmaceuticalDispensing set ModificationUser = @user, ModificationDate = [Common].[GETDATE](), AnnulmentUser = @user, AnnulmentDate = [Common].[GETDATE]() where Id = @IdPharmaceutical
					--- Elimino el control de inventarios
					DELETE FROM Inventory.InventoryControlDocument WHERE DocumentNumber = @CodePharmaceutical AND DocumentType = @Cinco

					--Se nulea el campo de cotización
					update @TablePharmaceuticalDetail set QuotationPharmaceuticalDispensingDetailId = null
					update Inventory.PharmaceuticalDispensingDetail set QuotationPharmaceuticalDispensingDetailId = null where PharmaceuticalDispensingId = @IdPharmaceutical
				END
				ELSE BEGIN
					UPDATE Inventory.PharmaceuticalDispensing SET ModificationUser = @user, ModificationDate = [Common].[GETDATE]() WHERE Id = @IdPharmaceutical
				END
			END			
		
			--DEBO TRAER EL NUMERO DE DOCUMENTO DEL PACIENTE
			declare @PatientCode varchar(25) = ''
			IF @DispensingIntegration = 1 
			BEGIN
				SET @PatientCode = (select IPCODPACI from dbo.ADINGRESO ad With(Nolock) where NUMINGRES = @AdmissionNumberHijo)
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
						RETURN
					END
					IF ISNULL((SELECT t.x.value('LogisticOperator[1]','varchar') FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x)), '') = '' BEGIN
						--SELECT '999' as CodeMessage, 'No se encuentra el parametro LogisticOperator' as Message, 0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
						set @CodeMessageResult = '999'
						set @ErrorsValidationResult = 'No se encuentra el parametro LogisticOperator'
						set @DispensingIdResult = 0
						set @DispensingCodeResult = ''
						set @StatusResult = 3
						RETURN
					END
					IF ISNULL((SELECT t.x.value('MedicalOrderRecipe[1]','varchar') FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x)), '') = '' BEGIN
						--SELECT '999' as CodeMessage, 'No se encuentra el parametro MedicalOrderRecipe' as Message, 0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
						set @CodeMessageResult = '999'
						set @ErrorsValidationResult = 'No se encuentra el parametro MedicalOrderRecipe'
						set @DispensingIdResult = 0
						set @DispensingCodeResult = ''
						set @StatusResult = 3
						RETURN
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
					FROM Payroll.FunctionalUnit With(Nolock)
					WHERE [State] = @Uno

					--Obtengo la entidad administradora de acuerdo al contrato que tiene el grupo de atencion				
					SELECT @HealthAdministratorId = c.HealthAdministratorId,
							@CareGroupId = cg.Id
					FROM @TablePharmaceuticalDetail t
					JOIN [Contract].CareGroup cg With(Nolock) ON t.CareGroupId = cg.Id
					JOIN [Contract].[Contract] c With(Nolock) ON cg.ContractId = c.Id
					JOIN Inventory.CareGroupByCareCenter cgcc With(Nolock) ON cg.Id = cgcc.CareGroupId
					WHERE cgcc.CareCenterCode = @CareCenterCode 
					ORDER BY c.HealthAdministratorId

					SELECT @FunctionalUnitHeonId = ISNULL((SELECT TOP 1 Id FROM Inventory.FunctionalUnit With(Nolock) WHERE [Name] = @FunctionalUnitName AND CareCenterCode = @CareCenterCode), 0)
					--Validamos si existe la unidad funcional de integracion con Heon
					IF @FunctionalUnitHeonId = 0 BEGIN
						INSERT INTO Inventory.FunctionalUnit
								([Name], CareCenterCode)
						SELECT @FunctionalUnitName AS [Name], @CareCenterCode AS CareCenterId

						SET @FunctionalUnitHeonId = SCOPE_IDENTITY()
					END
				
					--Obtengo la primera empresa
					SELECT TOP 1 @CODEMPRES = CODEMPRES
					FROM dbo.ADEMPRESA With(Nolock)
				
					--Obtengo la primera ubicacion del departamento del centro de atencion
					SELECT TOP 1 @AUUBICACI = u.AUUBICACI
					FROM dbo.INUBICACI u With(Nolock)
					JOIN dbo.ADCENATEN ca With(Nolock) ON u.DEPMUNCOD = ca.DEPMUNCOD
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
							JOIN dbo.ADCENATEN ca WITH(NOLOCK) ON ca.CODCENATE = @CareCenterCode
					END
				END
				
				DECLARE @ThirdPartyId AS INT = ISNULL((SELECT TOP 1 Id FROM Common.ThirdParty With(Nolock) WHERE Nit = @CodePatient), 0)
				--Validamos si existe el tercero
				IF @ThirdPartyId = 0 BEGIN
					DECLARE @PersonId AS INT = ISNULL((SELECT TOP 1 Id FROM Common.Person With(Nolock) WHERE IdentificationNumber = @CodePatient), 0)
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
				FROM @TablePharmaceuticalDetail tpd

				--Validamos si existe el ingreso
				IF NOT EXISTS(SELECT NUMINGRES FROM dbo.ADINGRESO WITH(NOLOCK) WHERE NUMINGRES = @AdmissionNumber) 
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
				IF EXISTS (SELECT NUMINGRESHIJO FROM dbo.HCINGRESORECNAC WITH(NOLOCK) WHERE NUMINGRESHIJO = @AdmissionNumber) BEGIN
					declare @TipoIngresoHijo as tinyint = (SELECT top 1 TIPREGISTRO FROM dbo.HCINGRESORECNAC With(Nolock) WHERE NUMINGRESHIJO = @AdmissionNumber )
					print  'consecutivo farmacia' + @ConsecutivePharmacy + ' - ' + cast(@TipoIngresoHijo as varchar(20))
					IF @TipoIngresoHijo = 1 BEGIN
						--si el tipo de ingreso es de estancia conjunta con la madre
						SELECT @AdmissionNumber = NUMINGRES FROM dbo.HCINGRESORECNAC WITH(NOLOCK) WHERE NUMINGRESHIJO = @AdmissionNumber
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
			from @TablePharmaceuticalDetail pdtmp
			inner join Inventory.PharmaceuticalDispensingDetail pd on pd.Id = pdtmp.Id
			left join Inventory.InventoryProduct ip on ip.Id = pd.ProductId and ip.TaxedProduct=1 and ip.LiquidateSalesTaxes=1 -- el iva solo si es producto gravado y liquida iva en ventas
			where pdtmp.Id > @Cero and pdtmp.ChangeTracker <> @Deleted

			update Inventory.PharmaceuticalDispensingDetailBatchSerial 
				set PhysicalInventoryId = bat.PhysicalInventoryId
					,Quantity = bat.Quantity
					,OutstandingQuantity = bat.OutstandingQuantity
					,PhysicalInventoryCustodyId = bat.PhysicalInventoryCustodyId
			from @TablePharmaceuticalDetail pdtmp
			inner join @TablePharmaceuticalBatch bat on bat.PharmaceuticalDetailIdTmp = pdtmp.IdTmp
			inner join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbatch on pddbatch.Id = bat.Id
			where pdtmp.Id > @Cero and bat.ChangeTracker <> @Deleted			
			
			--LOGICA DE REGISTRO EN LA HOJA DE GASTO QUIRURGICA
			declare @GuardaGastoQXTmp bit
			declare @IdProgramacionQXPrincipalTmp int
			declare @IdCabeceraHojaGastoQX int
			set @GuardaGastoQXTmp = 0
			SELECT top 1 @GuardaGastoQXTmp = GuardaGastoQX, @IdProgramacionQXPrincipalTmp = IdProgramacionQXPrincipal FROM @TablePharmaceuticalDetail where GuardaGastoQX = 1 AND IdProgramacionQXPrincipal IS NOT NULL
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
							FROM HCDEVMEDC AS C WITH(NOLOCK) 
							INNER JOIN HCDEVMEDD AS D WITH(NOLOCK) ON C.CODCONCEC = D.CODCONCEC 
							WHERE C.NUMINGRES = @AdmissionNumber 
							AND C.IDHCHOJAGASTOQX = @IdCabeceraHojaGastoQX AND C.DEVESTADO = '1' 
							AND D.CODPRODUC IN (SELECT td.CodeProduct 
												FROM @TablePharmaceuticalDetail td
												WHERE td.ChangeTracker <> @Deleted) ) BEGIN
						set @CodeMessageResult = '999'
						set @ErrorsValidationResult =(SELECT  CONCAT('No se puede dispensar ya que existen devoluciones pendientes por confimar en la Hoja QX. Productos : ',STRING_AGG(D.CODPRODUC,','))
														FROM HCDEVMEDC AS C WITH(NOLOCK) 
														INNER JOIN HCDEVMEDD AS D WITH(NOLOCK) ON C.CODCONCEC = D.CODCONCEC 
														WHERE C.NUMINGRES = @AdmissionNumber 
														AND C.IDHCHOJAGASTOQX = @IdCabeceraHojaGastoQX AND C.DEVESTADO = '1' 
														AND D.CODPRODUC IN (SELECT td.CodeProduct 
																			FROM @TablePharmaceuticalDetail td
																			WHERE td.ChangeTracker <> @Deleted))
						set @DispensingIdResult = 0
						set @DispensingCodeResult = ''
						set @StatusResult = 3
						RETURN
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
						@CodProductTmp = CodeProduct
					From @TablePharmaceuticalDetail 
					where Id = 0 And RowId >= @RowId 
					Order By RowId

					Set @Rows = @@ROWCOUNT
					If @Rows = 0 
						Break

						--saber si viene del paquete qx o si son solicitudes extras realizadas por la enfermera
					SET @OriginProduct = (SELECT TOP 1 IIF(IDETIPHIS IS NULL,1,2) from dbo.HCFARMEPD where CODCONCEC = @ConsecutivePharmacy 
											  AND CODPRODUC = @CodProductTmp AND (IDETIPHIS = 'ENFERMER1' OR IDETIPHIS is NULL) AND NUMINGRES = @AdmissionNumber )
	
					--si no existe producto en la hoja de gasto , procedemos a insertarlo
					IF Not Exists(select IDHCHOJAGASTOQX from dbo.HCHOJAGASTOQXD  where  CODPRODUC = @CodProductTmp AND IDHCHOJAGASTOQX = @IdCabeceraHojaGastoQX AND RequestType = @OriginProduct) 
					BEGIN
						/****NursingPackagesOrder*****/
						DECLARE @IdNursingPackagesOrder INT
						set @IdNursingPackagesOrder = NULL --se limpia la variable

						SELECT top 1 @IdNursingPackagesOrder = npo.Id 
						FROM MedicalHistory.NursingPackagesOrder npo 
						join MedicalHistory.NursingPackagesOrderDetail npod on npo.Id= npod.IdNursingPackagesOrder
						WHERE npo.IDHCFARMEPC=@ConsecutiveCrystal AND npod.CODPRODUC =@CodProductTmp and npo.NUMINGRES = @AdmissionNumber
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
								,iif(@OriginProduct=2 and @IdNursingPackagesOrder is not null,3,@OriginProduct ) -- si viene de enfermeria y es paquete es 3
								,1
								,@IdNursingPackagesOrder
						from @TablePharmaceuticalDetail 
						where IdTmp = @PharmaceuticalDetailIdTmp and ChangeTracker <> @Deleted
					END	
					ELSE 
					BEGIN
						--si el producto ya esta en la hoja de gasto procedemos a actualziar cantidades
						UPDATE dbo.HCHOJAGASTOQXD 
							SET [CANTIDADENTREGADA] += dtmp.Quantity,[CANTIDADDEVOLVER] +=dtmp.Quantity 
						FROM @TablePharmaceuticalDetail dtmp 
						INNER JOIN dbo.HCHOJAGASTOQXD HD ON dtmp.CodeProduct = HD.CODPRODUC AND HD.IDHCHOJAGASTOQX = @IdCabeceraHojaGastoQX
						WHERE dtmp.IdTmp = @PharmaceuticalDetailIdTmp AND ChangeTracker <> @Deleted AND HD.CODPRODUC = @ProductCodeTmp  AND RequestType = @OriginProduct
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
					from @TablePharmaceuticalDetail 
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
			
				Select Top 1 @RowId = RowId, @PharmaceuticalDetailIdTmp = IdTmp From @TablePharmaceuticalDetail where Id = 0 And RowId >= @RowId Order By RowId

				Set @Rows = @@ROWCOUNT
				If @Rows = 0 
					Break

						/*************************TARIFA DE PRODUCTOS NUEVA**********************/
						IF EXISTS(	SELECT  1
									from @TablePharmaceuticalDetail tpd
									JOIN Inventory.InventoryProduct ip with(nolock) on tpd.ProductId=ip.Id
									where ip.ProductTypeId =36 and IdTmp = @PharmaceuticalDetailIdTmp) BEGIN

								IF EXISTS(select 1
											from @TablePharmaceuticalDetail tpd
											join Contract.CareGroup cg WITH(NOLOCK) on tpd.CareGroupId=cg.Id
											join Inventory.ProductRate pr WITH(NOLOCK) on cg.ProductRateId=pr.Id
											JOIN Inventory.ProductRateGeneral prg WITH(NOLOCK) on prg.ProductRateId=pr.Id
											join Inventory.InventoryProduct ip with(NOLOCK) on ip.Id=tpd.ProductId
											where IdTmp = @PharmaceuticalDetailIdTmp and ip.ProductTypeId= 36 and Common.GETDATE() BETWEEN prg.InitialDate AND prg.EndDate ) BEGIN

										declare	@SalePriceIncludeTax BIT = (select top 1 cs.SalePriceIncludeTax from GeneralLedger.CompanySettings cs)
										declare @Value as NUMERIC(20,2)

										 UPDATE tpd SET
											tpd.GrossValue=	ROUND((ip.FinalProductCost + (ip.FinalProductCost*(ISNULL(a.Percentage,0)/100))),2),
											tpd.TaxValue = 0,
											tpd.TotalSalesPrice = ROUND((ip.FinalProductCost + (ip.FinalProductCost*(ISNULL(a.Percentage,0)/100))),2),
											tpd.GrandTotalSalesPrice = ROUND((ip.FinalProductCost + (ip.FinalProductCost*(ISNULL(a.Percentage,0)/100))),2) * tpd.Quantity,
											tpd.DiscountValue=0,
											tpd.IvaId = null,
											tpd.SalePrice = ROUND((ip.FinalProductCost + (ip.FinalProductCost*(ISNULL(a.Percentage,0)/100))),2)
											from @TablePharmaceuticalDetail tpd
											join Inventory.InventoryProduct ip WITH(NOLOCK) on tpd.ProductId= ip.Id
											join Contract.CareGroup cg WITH(NOLOCK) on tpd.CareGroupId=cg.Id
											join Inventory.ProductRate pr WITH(NOLOCK) on cg.ProductRateId=pr.Id
											JOIN Inventory.ProductRateGeneral prg WITH(NOLOCK) on prg.ProductRateId=pr.Id
											OUTER APPLY (select top 1 ISNULL(prgs.Percentage,0) Percentage
														from Inventory.ProductRateGeneralCondition prgs WITH(NOLOCK)
														WHERE prg.Id= prgs.ProductRateGeneralId and (ip.FinalProductCost * tpd.Quantity) BETWEEN prgs.InitialValue and prgs.EndValue) A
											where IdTmp = @PharmaceuticalDetailIdTmp and ip.ProductTypeId= 36 and Common.GETDATE() BETWEEN prg.InitialDate AND prg.EndDate
								END
								ELSE BEGIN
									declare @Product as VARCHAR = (SELECT  STRING_AGG(ip.Code,',') 
																	from @TablePharmaceuticalDetail tpd
																	JOIN Inventory.InventoryProduct ip with(nolock) on tpd.ProductId=ip.Id
																	WHERE IdTmp = @PharmaceuticalDetailIdTmp)
									set @CodeMessageResult = '999'
									set @ErrorsValidationResult = 'No se encontró tarifa PET para el producto'
									set @DispensingIdResult = 0
									set @DispensingCodeResult = ''
									set @StatusResult = 3
									RETURN
									
								END
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
					from @TablePharmaceuticalDetail tpd
					left join Inventory.InventoryProduct ip on ip.Id = tpd.ProductId and ip.TaxedProduct=1 and ip.LiquidateSalesTaxes=1 -- el iva solo si es producto gravado y liquida iva en ventas
					where IdTmp = @PharmaceuticalDetailIdTmp
	
				declare @IdPharmaceuticalDetail int = SCOPE_IDENTITY()
				update @TablePharmaceuticalDetail set Id = @IdPharmaceuticalDetail where IdTmp = @PharmaceuticalDetailIdTmp

				INSERT INTO Inventory.DeliveriesByPharmacyProductType
				([HCFARMEPDId]
				,[ProductType]
				,[Quantity]
				,[ConcentrationByUnit]
				,[TotalWeight])
				SELECT HCFARMEPDID, ProductType, Quantity, ConcentrationByUnit, TotalWeight FROM @TableDeliveriesByPharmacyProductType
				WHERE PharmaceuticalDispensingDetailIdTmp = @PharmaceuticalDetailIdTmp

				SELECT TOP 1 @AdministrationRoute = hpa.DESADMINI 
				FROM @TablePharmaceuticalDetail temp
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
				FROM @TablePharmaceuticalDetail temp
				JOIN Inventory.ATC atc WITH(NOLOCK) ON temp.MedicamentCode =atc.Code
				JOIN HCFARMEPD h ON h.ID = temp.EntityId
				WHERE IdTmp = @PharmaceuticalDetailIdTmp AND @EntityName = 'SaveDashboardPharmacy' and coalesce(temp.Extramural,0) =0

				/******************************************/
				IF NOT EXISTS ( SELECT 1 FROM @TablePharmaceuticalBatch where PharmaceuticalDetailIdTmp = @PharmaceuticalDetailIdTmp)
				BEGIN
					IF EXISTS (SELECT 1 FROM @TablePharmaceuticalDetail pd JOIN Inventory.Warehouse w ON pd.WarehouseId = w.Id WHERE pd.IdTmp = @PharmaceuticalDetailIdTmp AND w.VirtualStore = 1)
					BEGIN
						DECLARE @physicalInventoryId AS INT
						SET @physicalInventoryId = NULL
						
						SELECT TOP 1 @physicalInventoryId = phi.Id
						FROM @TablePharmaceuticalDetail pd 
						JOIN Inventory.PhysicalInventory phi ON pd.WarehouseId = phi.WarehouseId AND pd.ProductId = phi.ProductId
						WHERE pd.IdTmp = @PharmaceuticalDetailIdTmp

						IF ISNULL(@physicalInventoryId, 0) = 0
						BEGIN
							INSERT INTO [Inventory].[PhysicalInventory] 
								([WarehouseId],[ProductId],[BatchSerialId],[Quantity])
								SELECT pd.WarehouseId, pd.ProductId, NULL, 0
								FROM @TablePharmaceuticalDetail pd 
								WHERE pd.IdTmp = @PharmaceuticalDetailIdTmp

							SET @physicalInventoryId = SCOPE_IDENTITY()
						END

						INSERT INTO @TablePharmaceuticalBatch 
							(ChangeTracker,Id,PharmaceuticalDetailIdTmp,PharmaceuticalDispensingDetailId,PhysicalInventoryId,Quantity,OutstandingQuantity)
							SELECT
								'Add',0,pd.IdTmp,0,@physicalInventoryId,pd.Quantity,pd.Quantity
							FROM @TablePharmaceuticalDetail pd 
							WHERE pd.IdTmp = @PharmaceuticalDetailIdTmp
					END
				END
				ELSE IF EXISTS (SELECT 1 FROM @TablePharmaceuticalDetail pd JOIN Inventory.Warehouse w ON pd.WarehouseId = w.Id JOIN @TablePharmaceuticalBatch pdbs ON pd.IdTmp = pdbs.PharmaceuticalDetailIdTmp WHERE pd.IdTmp = @PharmaceuticalDetailIdTmp AND w.VirtualStore = 1 AND ISNULL(pdbs.PhysicalInventoryId, 0) = 0)
				BEGIN
					INSERT INTO [Inventory].[PhysicalInventory] 
						([WarehouseId],[ProductId],[BatchSerialId],[Quantity])
					SELECT DISTINCT pd.WarehouseId, pd.ProductId, NULL, 0
					FROM @TablePharmaceuticalDetail pd 
					JOIN Inventory.Warehouse w ON pd.WarehouseId = w.Id 
					JOIN @TablePharmaceuticalBatch pdbs ON pd.IdTmp = pdbs.PharmaceuticalDetailIdTmp 
					LEFT JOIN Inventory.PhysicalInventory phy ON phy.WarehouseId = pd.WarehouseId AND phy.ProductId = pd.ProductId
					WHERE pd.IdTmp = @PharmaceuticalDetailIdTmp 
						AND w.VirtualStore = 1 
						AND ISNULL(pdbs.PhysicalInventoryId, 0) = 0
						AND phy.Id IS NULL

					UPDATE pdbs SET pdbs.PhysicalInventoryId = phy.Id
					FROM @TablePharmaceuticalDetail pd 
					JOIN Inventory.Warehouse w ON pd.WarehouseId = w.Id 
					JOIN @TablePharmaceuticalBatch pdbs ON pd.IdTmp = pdbs.PharmaceuticalDetailIdTmp 
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
				from @TablePharmaceuticalBatch where PharmaceuticalDetailIdTmp = @PharmaceuticalDetailIdTmp

				SET @RowId += 1
			END
			
			--PRINT 'Eliminamos los detalles'
			--eliminams los detalles
			delete Inventory.PharmaceuticalDispensingDetailBatchSerial 
			from @TablePharmaceuticalBatch pddbsTmp 
			Inner Join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs On pddbsTmp.Id = pddbs.Id 
			Where pddbsTmp.ChangeTracker = @Deleted

			delete Inventory.PharmaceuticalDispensingDetail 
			from @TablePharmaceuticalDetail pddTmp 
			inner join Inventory.PharmaceuticalDispensingDetail pdd On pddTmp.Id = pdd.Id
			where pddTmp.ChangeTracker = @Deleted
										
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
					declare @QuantityStay int = (select count(*) from dbo.CHREGESTA With(Nolock) where cast(FECFINEST as date) <= @InitDate and NUMINGRES = @ValidationAdmission)
					IF @QuantityStay > 1 
					BEGIN -- Si el paciente tiene orden de traslado
						if Exists (select ch.CODICAMAS from dbo.CHREGESTA ch With(Nolock)
							Inner Join dbo.CHCAMASHO ca With(Nolock) On ca.CODICAMAS = ch.CODICAMAS
							Where cast(FECFINEST As Date) <= @InitDate And ch.NUMINGRES = @AdmissionNumber And ca.CODCONCEC Is Not Null) 
						Begin

							--select '999' as CodeMessage, 'El paciente tiene pendiente una aceptación de medicamentos por motivo de traslado de hospitalización'  as [Message], 
								--0 as DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = 'El paciente tiene pendiente una aceptación de medicamentos por motivo de traslado de hospitalización'
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							RETURN
						END
						IF NOT EXISTS (SELECT ch.CODICAMAS FROM dbo.CHREGESTA ch WITH(NOLOCK)
							INNER JOIN dbo.CHCAMASHO ca WITH(NOLOCK) ON ca.CODICAMAS = ch.CODICAMAS 
							INNER JOIN dbo.INUNIFUNC uni WITH(NOLOCK) ON uni.UFUCODIGO = ca.UFUCODIGO 
							WHERE CAST(FECFINEST AS DATE) <= @InitDate AND ch.NUMINGRES = @AdmissionNumber AND uni.UFUTIPUNI = @Diecinueve) 
						BEGIN
						--select '999' as CodeMessage, 'El paciente tiene un error en el modulo de hospitalización, está asignado en dos o mas camas'  as [Message], 
						--0 as DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
						set @CodeMessageResult = '999'
						set @ErrorsValidationResult = 'El paciente tiene un error en el modulo de hospitalización, está asignado en dos o mas camas'
						set @DispensingIdResult = 0
						set @DispensingCodeResult = ''
						set @StatusResult = 3
						RETURN
						END
					END
				END
				--- Valido que exista parametros de inventarios por unidad funcional
				if Not Exists (select Id from Inventory.SettingInventory With(Nolock) where OperatingUnitId = @OperatingUnitId) begin
					--select '999' as CodeMessage, 'No se encontraron parametros de inventarios creados para la unidad operativa ' + 
						--isnull((select UnitName from Common.OperatingUnit With(Nolock) where Id = @OperatingUnitId),'') as Message, 
						--0 DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
					set @CodeMessageResult = '999'
					set @ErrorsValidationResult = 'No se encontraron parametros de inventarios creados para la unidad operativa'
					set @DispensingIdResult = 0
					set @DispensingCodeResult = ''
					set @StatusResult = 3
					RETURN
				END
				
				declare @SettingInventoryId int,
						@AssociateCostCenter tinyint,
						@AssociateCostMainAccount tinyint
				 
				SELECT 
					@SettingInventoryId = Id,
					@AssociateCostCenter = AssociateCostCenter,
					@AssociateCostMainAccount = AssociateCostMainAccount
				FROM Inventory.SettingInventory WITH (NOLOCK) 
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
				
				IF EXISTS (SELECT 1 
							FROM @TablePharmaceuticalDetail tpd
							JOIN Inventory.InventoryProduct ip WITH(NOLOCK) on  tpd.ProductId =ip.Id
							JOIN Inventory.ProductType pt WITH(NOLOCK) on ip.ProductTypeId= pt.Id
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
						from @TablePharmaceuticalDetail pd
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
					SELECT @IESTADOIN = IESTADOIN 
					FROM dbo.ADINGRESO WITH (NOLOCK) 
					WHERE NUMINGRES = @AdmissionNumber

					IF @DispensingIntegration = 1
					BEGIN
						IF EXISTS (SELECT 1 FROM @TablePharmaceuticalDetail WHERE Extramural = 1)
						BEGIN
							IF @IESTADOIN = 'A'
							BEGIN
								SET @CodeMessageResult = '999'
								SET @ErrorsValidationResult = 'No se puede generar la dispensación en el ingreso actual porque se encuentra anulado'
								SET @DispensingIdResult = 0
								SET @DispensingCodeResult = ''
								SET @StatusResult = 3
								RETURN
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
					
					EXEC Billing.SP_GenerateServiceOrder_Output	@OrderXml, 
													@User, 
													--Salidas
													@CodeResultSo OUTPUT, 
													@MessageResultSo OUTPUT, 
													@StatusResultSo OUTPUT,	
													@IdSo OUTPUT
							
					if @StatusResultSo = @Tres begin
						--select CodeMessage, Message, 0 as DispensingId, '' as DispensingCode, [Status] from @TableResultOrder						
						set @CodeMessageResult = @CodeResultSo
						set @ErrorsValidationResult = @MessageResultSo
						set @DispensingIdResult = 0
						set @DispensingCodeResult = ''
						set @StatusResult = @StatusResultSo
						RETURN
					END

					SET @MessageReturn += ', ' + @MessageResultSo
				END
				
				--- Ahora Afecto el Inventario, Kardex y Contabilidad
				if @AffectInventory = 1 begin
					declare @KardexXml xml = (select null as ThirdPartyId, phy.ProductId, phy.BatchSerialId, 2 as MovementType, 
						phy.WarehouseId, bat.Quantity, ip.ProductCost as Value, 0 as AffectAverageCost
					FROM @TablePharmaceuticalDetail pd
					inner join Inventory.Warehouse w with(nolock) ON pd.WarehouseId = w.Id
					inner join @TablePharmaceuticalBatch bat on pd.IdTmp = bat.PharmaceuticalDetailIdTmp
					inner join Inventory.PhysicalInventory phy With(Nolock) on phy.Id = bat.PhysicalInventoryId
					inner join Inventory.InventoryProduct ip With(Nolock) on ip.Id = pd.ProductId 
					where w.VirtualStore = @Cero AND pd.ChangeTracker <> @Deleted AND bat.ChangeTracker <> @Deleted
						and pd.PhysicalInventoryCustodyId is NULL
					for xml path('Kardex'), elements)					
					IF @KardexXml IS NOT NULL
					BEGIN

						declare @TableResultKardex table(CodeMessage varchar(20), [Message] varchar(1000), [Status] tinyint)
						--select @KardexXml, @IdPharmaceutical, @CodePharmaceutical,@User
						insert into @TableResultKardex
							
							exec [Inventory].[SP_SavePhysicalInventoryKardex] @KardexXml, @IdPharmaceutical, @CodePharmaceutical, 'PharmaceuticalDispensing', @User					
					
						IF EXISTS (SELECT CodeMessage FROM @TableResultKardex WHERE [Status] = @Tres) BEGIN
							--select '999' as CodeMessage,[Message], 0 as DispensingId, '' as DispensingCode, [Status] from @TableResultKardex
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = (select Message from @TableResultKardex)
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = (select Status from @TableResultKardex)
							RETURN
						END
					END

					declare @KardexXmlCustody xml = (select null as ThirdPartyId, pd.ProductId, phy.BatchSerialId, 2 as MovementType, 
						pd.WarehouseId, bat.Quantity, ip.ProductCost as Value, 0 as AffectAverageCost
					FROM @TablePharmaceuticalDetail pd
					inner join Inventory.Warehouse w with(nolock) ON pd.WarehouseId = w.Id
					inner join @TablePharmaceuticalBatch bat on pd.IdTmp = bat.PharmaceuticalDetailIdTmp
					inner join Inventory.PhysicalInventoryCustody phy With(Nolock) on phy.Id = bat.PhysicalInventoryCustodyId
					inner join Inventory.InventoryProduct ip With(Nolock) on ip.Id = pd.ProductId 
					where w.CustodyStore = @Uno AND pd.ChangeTracker <> @Deleted AND bat.ChangeTracker <> @Deleted
						and pd.PhysicalInventoryCustodyId IS NOT NULL
					for xml path('Kardex'), elements)					
				
					

					IF @KardexXmlCustody IS NOT NULL
					BEGIN
					declare @TableResultKardexCustody table(CodeMessage varchar(20), [Message] varchar(1000), [Status] tinyint)
					insert into @TableResultKardexCustody
						exec [Inventory].[SP_SavePhysicalInventoryCustodyKardexCustody] @KardexXmlCustody, @AdmissionNumber, @IdPharmaceutical, @CodePharmaceutical, 'PharmaceuticalDispensing', @User					
						IF EXISTS (SELECT CodeMessage FROM @TableResultKardexCustody WHERE [Status] = @Tres) BEGIN
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = (select Message from @TableResultKardexCustody)
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = (select Status from @TableResultKardexCustody)
							RETURN
						END
					END 

					--- Actualizo las remisiones de inventario en consignación si las hubiera
					declare @RemissionXml xml = (select DISTINCT pd.Id AS EntityDetailId, @OperatingUnitId AS OperatingUnitId, pd.FunctionalUnitId, pd.ProductId, phy.BatchSerialId, 2 as MovementType, pd.WarehouseId, bat.Quantity, ip.ProductCost as Value
						FROM @TablePharmaceuticalDetail pd
						INNER JOIN @TablePharmaceuticalBatch bat on pd.IdTmp = bat.PharmaceuticalDetailIdTmp
						INNER JOIN Inventory.PhysicalInventory phy With(Nolock) on phy.Id = bat.PhysicalInventoryId	
						INNER JOIN Inventory.InventoryProduct ip With(Nolock) on ip.Id = pd.ProductId 
						INNER JOIN Inventory.Warehouse w With(Nolock) ON pd.WarehouseId = w.Id
						left JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs WITH(NOLOCK) ON cirdbs.BatchSerialId = phy.BatchSerialId
						WHERE w.WarehouseConsignment = @Uno AND pd.ChangeTracker <> @Deleted and bat.ChangeTracker <> @Deleted
						for xml path('Remission'), elements)

						
					IF @RemissionXml IS NOT NULL BEGIN
						DECLARE @MessageReturnRemission VARCHAR(MAX)
						EXEC [Inventory].[SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission] @RemissionXml, @IdPharmaceutical, @CodePharmaceutical, 'PharmaceuticalDispensing', @User, @MessageReturnRemission OUTPUT
						IF ISNULL(@MessageReturnRemission, '') <> '' BEGIN
							--select '999' as CodeMessage, @MessageReturnRemission [Message], 0 as DispensingId, '' as DispensingCode, CAST(3 AS TINYINT) [Status]
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = @MessageReturnRemission
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							RETURN
						END
					END

					IF @DispensingIntegration = 1 BEGIN
						--- Ahora afecto contabilidad
						if Exists (select NUMINGRES from dbo.ADINGRESO With(Nolock) where NUMINGRES = @AdmissionNumber and GENCONENTITY is null) begin --- Valido que tenga entidad administradora
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = 'El ingreso '+ @AdmissionNumber +' no tiene asignada una entidad administradora de salud'
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							RETURN
						END
						--- Valido que la entidad administradora este en la tabla
						IF NOT EXISTS (SELECT Id FROM [Contract].HealthAdministrator WITH(NOLOCK) WHERE Id = (SELECT GENCONENTITY FROM dbo.ADINGRESO WITH(NOLOCK) WHERE NUMINGRES = @AdmissionNumber)) BEGIN
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = 'No se encontro entidad administradora para el ingreso ' + @AdmissionNumber
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							RETURN
						END
					END

					IF EXISTS 
					(
						SELECT 1 
						FROM @TablePharmaceuticalDetail pd
						JOIN Inventory.Warehouse w With(Nolock) ON pd.WarehouseId = w.Id
						WHERE PhysicalInventoryCustodyId IS NULL AND w.ControlStore = @Cero
					)
					BEGIN
					
						declare @TableJournalVoucher table(IdJournalVoucher int NOT NULL, VoucherDate datetime NOT NULL, Imported bit NOT NULL, [Status] tinyint NOT NULL, Detail varchar(500) NULL, EntityCode varchar(20) NULL, EntityId int NULL, EntityName varchar(250) NULL, IsClosedYear bit NOT NULL)
						declare @TableJournalVoucherDetail table(IdMainAccount int NOT NULL, IdThirdParty int NULL, IdCostCenter int NULL,DebitValue decimal(18, 2) NOT NULL,CreditValue decimal(18, 2) NOT NULL,Detail varchar(max) NULL,IdRetention int NULL,RetentionRate decimal(5, 2) NULL,BaseValue decimal(18, 0) NULL,BillingValue decimal(18, 0) NULL)
						declare @SalesJournalVoucherTypeId int, @CreditThirdPartyId int, @PharmaceuticalDispensingGetThirdParty tinyint, @PatientThirdPartyId int , @HealAdministratorAdmissionThirdPartyId int
						select @SalesJournalVoucherTypeId = SalesJournalVoucherTypeId, @PharmaceuticalDispensingGetThirdParty = PharmaceuticalDispensingGetThirdParty from Inventory.SettingInventory where OperatingUnitId = @OperatingUnitId
					
						declare @NitPatient varchar(25) = @PatientCode					
						IF @DispensingIntegration = 1 BEGIN
							SET @NitPatient = (select IPCODPACI from dbo.ADINGRESO With(Nolock) where NUMINGRES = @AdmissionNumber)
							SET @HealAdministratorAdmissionThirdPartyId = (SELECT ThirdPartyId FROM [Contract].HealthAdministrator WITH(NOLOCK) 
								WHERE Id = (SELECT GENCONENTITY FROM dbo.ADINGRESO WITH(NOLOCK) WHERE NUMINGRES = @AdmissionNumber))
						END					

						set @PatientThirdPartyId = (select Id from Common.ThirdParty With(Nolock) where Nit = @NitPatient)
						IF ISNULL(@PatientThirdPartyId, 0) = 0 begin
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = 'El paciente con identificacion ' + @NitPatient + ' no existe como tercero en Indigo Vie'
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							RETURN
						END

						IF @DispensingIntegration = 2 or @DispensingIntegration = 3 BEGIN
							SET @HealAdministratorAdmissionThirdPartyId = @PatientThirdPartyId
						END	

						if @PharmaceuticalDispensingGetThirdParty = 1 begin -- Tomar Tercero del paciente
							set @CreditThirdPartyId = @PatientThirdPartyId
						end
						ELSE BEGIN --- Tercero Especifico
							SET @CreditThirdPartyId = (SELECT PharmaceuticalDispensingThirdPartyId FROM Inventory.SettingInventory WITH(NOLOCK) WHERE OperatingUnitId = @OperatingUnitId)
						END
					
						---Inserto la cabecera del comprobante
						insert into @TableJournalVoucher
							values (@SalesJournalVoucherTypeId, @DocumentDate, 0, 2, 'Comprobante generado por la dispensacion ' + @CodePharmaceutical, @CodePharmaceutical, @IdPharmaceutical, 'PharmaceuticalDispensing', 0)
					
						--- Valido que los productos tengan grupo y el costo sea mayor a 0
						if Exists (select pd.RowId from @TablePharmaceuticalDetail pd 
							Inner Join Inventory.InventoryProduct ip With(Nolock) On pd.ProductId = ip.Id 
							Where ip.ProductGroupId is null And pd.ChangeTracker <> @Deleted
							AND pd.PhysicalinventoryCustodyId is NULL) Begin

							declare @errors varchar(MAX)
							select @errors=stuff((select N'; El producto ' + ip.Code	+ ' - ' + ip.Name + ' no tiene un grupo asociado'
							from @TablePharmaceuticalDetail pd inner join Inventory.InventoryProduct ip With(Nolock) on pd.ProductId = ip.Id where ip.ProductGroupId is null and pd.ChangeTracker <> @Deleted AND pd.PhysicalinventoryCustodyId is NULL
							for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
							--select '999' as CodeMessage, @errors as [Message], 0 as DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = @errors
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							RETURN
						END
					
						if Exists (select pd.RowId from @TablePharmaceuticalDetail pd 
							inner join Inventory.InventoryProduct ip on pd.ProductId = ip.Id 
							where isnull(ip.ProductCost,0) = 0 and pd.ChangeTracker<>@Deleted
							AND pd.PhysicalinventoryCustodyId is NULL) begin

							declare @errorsCostZero varchar(MAX)
							select @errorsCostZero=stuff((select N'; El producto ' + ip.Code	+ ' - ' + ip.Name + ' tiene un costo de 0'
							from @TablePharmaceuticalDetail pd inner join Inventory.InventoryProduct ip With(Nolock) on pd.ProductId = ip.Id where isnull(ip.ProductCost,0) = 0 and pd.ChangeTracker <> @Deleted AND pd.PhysicalinventoryCustodyId is NULL
							for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
							--select '999' as CodeMessage, @errorsCostZero as [Message], 0 as DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status]
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = @errorsCostZero
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							RETURN
						END
					
						IF EXISTS 
						(
							SELECT 1
							FROM @TablePharmaceuticalDetail pdd
							JOIN Inventory.InventoryProduct ip With(Nolock) ON ip.Id = pdd.ProductId
							LEFT JOIN Inventory.SettingInventoryFunctionalUnit sifu WITH (NOLOCK) ON @AssociateCostMainAccount = 1 AND sifu.SettingInventoryId = @SettingInventoryId AND pdd.FunctionalUnitId = sifu.FunctionalUnitId
							LEFT JOIN Inventory.ProductGroupFunctionalUnit pgfu WITH (NOLOCK) ON @AssociateCostMainAccount = 2 AND ip.ProductGroupId = pgfu.ProductGroupId AND pdd.FunctionalUnitId = pgfu.FunctionalUnitId
							WHERE pdd.ChangeTracker <> @Deleted 
								AND PhysicalinventoryCustodyId is NULL
								AND ISNULL(sifu.Id, pgfu.Id) IS NULL
						) BEGIN
							declare @errorsFunctional varchar(MAX)
							select @errorsFunctional = stuff((
								SELECT DISTINCT N'; La unidad funcional ' + fu.Code	+ ' - ' + fu.Name + ' no esta parametrizada en ' + 
									IIF(@AssociateCostMainAccount = 1, 'los parametros de inventarios', CONCAT('el grupo de producto ', pg.Code, ' - ', pg.Name))
								FROM @TablePharmaceuticalDetail pdd
								JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON pdd.FunctionalUnitId = fu.Id
								JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON ip.Id = pdd.ProductId
								JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
								LEFT JOIN Inventory.SettingInventoryFunctionalUnit sifu WITH (NOLOCK) ON @AssociateCostMainAccount = 1 AND sifu.SettingInventoryId = @SettingInventoryId AND pdd.FunctionalUnitId = sifu.FunctionalUnitId
								LEFT JOIN Inventory.ProductGroupFunctionalUnit pgfu WITH (NOLOCK) ON @AssociateCostMainAccount = 2 AND ip.ProductGroupId = pgfu.ProductGroupId AND pdd.FunctionalUnitId = pgfu.FunctionalUnitId
								WHERE pdd.ChangeTracker <> @Deleted 
									AND PhysicalinventoryCustodyId is NULL
									AND ISNULL(sifu.Id, pgfu.Id) IS NULL
							for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
							
							set @CodeMessageResult = '999'
							set @ErrorsValidationResult = @errorsFunctional
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							RETURN
						END

						/* validamos si el producto sale de un almacen en consigna, si parametro costeo inv en consigna esta activo y que exista ese producto en la tabla de ConsignmentCostListDetail*/

						declare 
								@CostNew as decimal(12,2),
								@flagCostNew as BIT = 0
						SELECT
							@CostNew = ccld.CostNew,
							@FlagCostNew = iif(w.WarehouseConsignment = 1 and s.ConsignmentInventoryCosting = 1 and ccld.CostNew is not NULL , 1,0) 
						FROM Inventory.Warehouse w 
						join @TablePharmaceuticalDetail tmppd on tmppd.WarehouseId = w.Id
						JOIN Common.Supplier s With(Nolock) ON w.SupplierId = s.Id
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
								FROM @TablePharmaceuticalDetail pd
								INNER JOIN Inventory.InventoryProduct ip With(Nolock) ON ip.Id = pd.ProductId
								INNER JOIN Inventory.ProductGroup g With(Nolock) ON g.Id = ip.ProductGroupId
								INNER JOIN Inventory.Warehouse w With(Nolock) ON w.Id = pd.WarehouseId
								INNER JOIN GeneralLedger.MainAccounts ma With(Nolock) ON ma.Id = g.CounterpartCostConsignedInventoryId
								INNER JOIN Common.Supplier s With(Nolock) ON w.SupplierId = s.Id
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
								FROM @TablePharmaceuticalDetail pd
								INNER JOIN Inventory.InventoryProduct ip With(Nolock) ON ip.Id = pd.ProductId
								INNER JOIN Inventory.ProductGroup g With(Nolock) ON g.Id = ip.ProductGroupId	
								INNER JOIN Payments.AccountPayableConcepts apc With(Nolock) ON apc.Id = g.InventoryAccountPayableConceptId
								INNER JOIN Inventory.Warehouse w With(Nolock) ON w.Id = pd.WarehouseId 
								INNER JOIN GeneralLedger.MainAccounts ma With(Nolock) ON ma.Id = apc.IdAccount
								WHERE w.VirtualStore = @Cero AND w.ControlStore = @Cero AND w.WarehouseConsignment <> @Uno AND pd.ChangeTracker <> @Deleted
								AND pd.PhysicalinventoryCustodyId is NULL
					
					declare @IdSolicitud as INT

					---se optiene el id de la solicitud por si los productos individualmetne manejan su propio centro de costo
					select @IdSolicitud= hcc.CODCONCEC
					from MedicalHistory.NursingPackagesOrder npo
					join [dbo].[AGPAQUETES] AP on npo.IDAGPAQUETES = ap.ID
					join HCFARMEPC hcc on npo.IDHCFARMEPC = hcc.CODCONCEC
					where npo.Id =@IdNursingPackagesOrder
				
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
							from @TablePharmaceuticalDetail pd
							inner join Inventory.Warehouse w With(Nolock) on w.Id = pd.WarehouseId
							inner join Payroll.FunctionalUnit fu With(Nolock) on fu.Id = pd.FunctionalUnitId							
							inner join Inventory.InventoryProduct ip With(Nolock) on ip.Id = pd.ProductId
							inner join Inventory.ProductGroup pg With(Nolock) on pg.Id = ip.ProductGroupId							
							inner join [Contract].CareGroup cg With(Nolock) on cg.Id = pd.CareGroupId
							left join [Contract].[Contract] c With(Nolock) on c.Id = cg.ContractId
							left join [Contract].HealthAdministrator ha With(Nolock) on ha.Id = c.HealthAdministratorId
							left join Inventory.SettingInventoryFunctionalUnit sifu With(Nolock) on @AssociateCostMainAccount = 1 AND sifu.SettingInventoryId = @SettingInventoryId AND pd.FunctionalUnitId = sifu.FunctionalUnitId
							left join Inventory.ProductGroupFunctionalUnit pgfu With(Nolock) on @AssociateCostMainAccount = 2 AND pg.Id = pgfu.ProductGroupId AND pd.FunctionalUnitId = pgfu.FunctionalUnitId
							left join GeneralLedger.MainAccounts ma With(Nolock) on ma.Id = ISNULL(sifu.CostAccountId, pgfu.CostAccountId)
							LEFT JOIN (
								SELECT   npo.NUMINGRES,paqd.CODPRODUC, npo.IDHCFARMEPC,paqd.IdCostCenter
								FROM MedicalHistory.NursingPackagesOrder npo WITH (NOLOCK)
								JOIN dbo.AGPAQUETES paq WITH (NOLOCK) ON paq.ID = npo.IDAGPAQUETES
								join AGPAQUETESD paqd WITH (NOLOCK) on paqd.IDAGPAQUETE = paq.ID
								LEFT JOIN HCFARMEPC hc WITH (NOLOCK) ON hc.CODCONCEC = npo.IDHCFARMEPC
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
							exec [GeneralLedger].[SP_SaveJournalVoucher] @JournalXml, @User
							--EXEC [GeneralLedger].[SP_CreateAndValidateJournalVoucherMovement]  @JournalXml, @User

						if Exists (select CodeMessage from @TableResultJournal where CodeMessage <> @SCero) begin
							
							--select CodeMessage, Message, 0 as DispensingId, '' as DispensingCode, cast(3 as tinyint) as [Status] from @TableResultJournal
							set @CodeMessageResult = (select CodeMessage from @TableResultJournal)
							set @ErrorsValidationResult = (select Message from @TableResultJournal)
							set @DispensingIdResult = 0
							set @DispensingCodeResult = ''
							set @StatusResult = 3
							RETURN
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
							IF NOT EXISTS (SELECT 1 FROM @TablePharmaceuticalDetail WHERE Extramural = 1)
							BEGIN
								set @CodeMessageResult = '999'
								set @ErrorsValidationResult = 'El paciente ya fue egresado de la institución, favor actualizar los datos'
								set @DispensingIdResult = 0
								set @DispensingCodeResult = ''
								set @StatusResult = 3
								RETURN
							END
						END
						-- varifico si yaexiste un inventario fisico en crystal
						declare @ProductId int, @ProductCodeCrystal varchar(20), @QuantityCrystal int, @ProductTypeCrystal varchar(20)
							, @CantidadSolicitadaCrystal int, @ProfessionalCrystal varchar(20), @Extramural bit , @EntityDId int, @CantidadMezcla int
						
						--Set @Rows = 1
						--Set @RowId = 1
					
						IF OBJECT_ID('tempdb..#DispenseDetail') IS NOT NULL DROP TABLE #DispenseDetail;
						IF OBJECT_ID('tempdb..#HCKARDPACConcecutives') IS NOT NULL DROP TABLE #HCKARDPACConcecutives;

						CREATE TABLE #HCKARDPACConcecutives (NUMCONSEC UNIQUEIDENTIFIER);

						-- 1. Carga datos preparados
						SELECT 
							pdd.RowId,
							pdd.ProductId,
							ISNULL(td.Quantity, pdd.Quantity) AS QuantityCrystal,
							pdd.ProductType AS ProductTypeCrystal,
							pdd.CantidadSolicitada AS CantidadSolicitadaCrystal,
							pdd.OrderedHealthProfessionalCode AS ProfessionalCrystal,
							pdd.Extramural,
							pdd.EntityId AS EntityDId,
							pdd.CantidadMezcla,
							COALESCE(ip.Code, insup.Code, med.Code) AS ProductCodeCrystal,
							ip.SupplieId,
							ip.ATCId
						INTO #DispenseDetail
						FROM @TablePharmaceuticalDetail pdd
						LEFT JOIN Inventory.InventoryProduct ip ON ip.Id = pdd.ProductId
						LEFT JOIN @TableDeliveriesByPharmacyProductType td ON td.HCFARMEPDID = pdd.EntityId AND td.ProductId = pdd.ProductId
						LEFT JOIN Inventory.InventorySupplie insup ON insup.Id = ip.SupplieId
						LEFT JOIN Inventory.ATC med ON med.Id = ip.ATCId
						WHERE pdd.ChangeTracker <> @Deleted;

						-- 2. Procesamiento por cursor (fila a fila)
						DECLARE DispenseCursor CURSOR LOCAL FAST_FORWARD FOR
							SELECT RowId, ProductId, QuantityCrystal, ProductTypeCrystal, CantidadSolicitadaCrystal,
									ProfessionalCrystal, Extramural, EntityDId, CantidadMezcla, ProductCodeCrystal, SupplieId, ATCId
							FROM #DispenseDetail
							ORDER BY RowId;

						DECLARE
							@SupplieId INT, @ATCId INT, @TotalQuantity INT, @SendTo TINYINT,
							@CodeSusceptibleMixingStation UNIQUEIDENTIFIER;

						OPEN DispenseCursor;
						FETCH NEXT FROM DispenseCursor INTO 
							@RowId, @ProductId, @QuantityCrystal, @ProductTypeCrystal, @CantidadSolicitadaCrystal,
							@ProfessionalCrystal, @Extramural, @EntityDId, @CantidadMezcla, @ProductCodeCrystal, @SupplieId, @ATCId;

						WHILE @@FETCH_STATUS = 0
						BEGIN
							-- Busca código susceptible y destino
							SELECT TOP 1
								@CodeSusceptibleMixingStation = CodeSusceptibleMixingStation,
								@SendTo = SENDTO
							FROM HCFARMEPD
							WHERE CODCONCEC = @ConsecutiveCrystal AND @EntityDId = ID;

							-- Ajusta la cantidad si aplica
							SELECT @TotalQuantity = Quantity
							FROM @TableDeliveriesByPharmacyProductType
							WHERE HCFARMEPDID = @EntityDId AND ProductId = @ProductId;
							IF @TotalQuantity IS NOT NULL
								SET @QuantityCrystal = @TotalQuantity;

							-- Si el producto no existe en HCFARMEPD (ya lo resolviste en la temp con COALESCE, aquí se puede dejar como comentario)
							-- IF NOT EXISTS (SELECT 1 FROM HCFARMEPD WHERE CODPRODUC = @ProductCodeCrystal AND CODCONCEC = @ConsecutiveCrystal) ...

							IF @Extramural = 0 AND @GuardaGastoQXTmp = 0
							BEGIN
								IF @SendTo = 2
								BEGIN
									-- Actualiza IsDispensed para central de mezclas
									UPDATE pd SET IsDispensed = 1
									FROM MedicalHistory.PharmaDose pd (NOLOCK)
									WHERE CodeSusceptibleMixingStation = @CodeSusceptibleMixingStation AND IsDispensed = 0
										AND GroupingCodeDose IN (
										SELECT DISTINCT TOP (@CantidadMezcla) GroupingCodeDose
										FROM MedicalHistory.PharmaDose (NOLOCK)
										WHERE CodeSusceptibleMixingStation = @CodeSusceptibleMixingStation AND IsDispensed = 0
										);

									-- Inserta en HCFISIPRO
									INSERT INTO [dbo].[HCFISIPRO] ([IPCODPACI], [NUMINGRES], [CODCENATE], [UFUCODIGO], [CODPRODUC], [TIPPRODUC],
																	[CANACTPRO], [CANPEDPRO], [CANPENPRO], [TOTPROUNI], [DOSPROACU], [TOTHORACU],
																	[CODUNIMED], [INDAUDFOR], [TIPREGIST])
									VALUES (@PatientCode, @AdmissionNumberHijo, @CareCenterCode, @FunctionalUnitCode,
											@ProductCodeCrystal, @ProductTypeCrystal, @QuantityCrystal, @QuantityCrystal, @QuantityCrystal,
											NULL, NULL, NULL, NULL, 0, 1);
								END
								ELSE
								BEGIN
									-- Insumos/paquete/enfermería
									IF EXISTS (
										SELECT 1 FROM Inventory.InventoryProduct ip
										JOIN Inventory.ProductType pt ON pt.Id = ip.ProductTypeId
										WHERE ip.Id = @ProductId AND pt.Class = 5
									) AND @SendTo = 1
									BEGIN
										INSERT INTO [dbo].[HCFISIPRO] ([IPCODPACI], [NUMINGRES], [CODCENATE], [UFUCODIGO], [CODPRODUC], [TIPPRODUC],
																		[CANACTPRO], [CANPEDPRO], [CANPENPRO], [TOTPROUNI], [DOSPROACU], [TOTHORACU],
																		[CODUNIMED], [INDAUDFOR], [TIPREGIST])
										VALUES (@PatientCode, @AdmissionNumberHijo, @CareCenterCode, @FunctionalUnitCode,
												@ProductCodeCrystal, @ProductTypeCrystal, @TotalQuantity, @TotalQuantity, @TotalQuantity,
												NULL, NULL, NULL, NULL, 0, 1);
									END
									ELSE IF EXISTS (
										SELECT 1 FROM dbo.HCFISIPRO
										WHERE IPCODPACI = @PatientCode AND NUMINGRES = @AdmissionNumberHijo
											AND CODCENATE = @CareCenterCode AND UFUCODIGO = @FunctionalUnitCode AND CODPRODUC = @ProductCodeCrystal
									)
									BEGIN
										UPDATE dbo.HCFISIPRO
										SET CANACTPRO += @QuantityCrystal
										WHERE IPCODPACI = @PatientCode AND NUMINGRES = @AdmissionNumberHijo
											AND CODCENATE = @CareCenterCode AND UFUCODIGO = @FunctionalUnitCode AND CODPRODUC = @ProductCodeCrystal;
									END
									ELSE
									BEGIN
										IF @ProductTypeCrystal = 5
										BEGIN
											INSERT INTO [dbo].[HCFISIPRO] ([IPCODPACI], [NUMINGRES], [CODCENATE], [UFUCODIGO], [CODPRODUC], [TIPPRODUC],
																			[CANACTPRO], [CANPEDPRO], [CANPENPRO], [TOTPROUNI], [DOSPROACU], [TOTHORACU],
																			[CODUNIMED], [INDAUDFOR])
											SELECT 
												@PatientCode, @AdmissionNumberHijo, @CareCenterCode, @FunctionalUnitCode,
												@ProductCodeCrystal, SFD.TIPOREGIS, @QuantityCrystal, @CantidadSolicitadaCrystal,
												@CantidadSolicitadaCrystal - @QuantityCrystal, NULL, NULL, NULL, NULL, 0
											FROM dbo.HCFARMEPD SFD
											LEFT JOIN MedicalHistory.NursingPackagesOrder npo ON npo.IDHCFARMEPC = SFD.CODCONCEC AND npo.IDAGPAQUETES = SFD.IDAGPAQUETES
											WHERE npo.IDHCFARMEPC = @ConsecutiveCrystal AND SFD.CODPRODUC = @ProductCodeCrystal;
										END
										ELSE
										BEGIN
											INSERT INTO [dbo].[HCFISIPRO] ([IPCODPACI], [NUMINGRES], [CODCENATE], [UFUCODIGO], [CODPRODUC], [TIPPRODUC],
																			[CANACTPRO], [CANPEDPRO], [CANPENPRO], [TOTPROUNI], [DOSPROACU], [TOTHORACU],
																			[CODUNIMED], [INDAUDFOR])
											VALUES (@PatientCode, @AdmissionNumberHijo, @CareCenterCode, @FunctionalUnitCode,
													@ProductCodeCrystal, @ProductTypeCrystal, @QuantityCrystal,
													ISNULL(@TotalQuantity, @CantidadSolicitadaCrystal),
													ISNULL(@TotalQuantity, @CantidadSolicitadaCrystal - @QuantityCrystal),
													NULL, NULL, NULL, NULL, 0);
										END
									END
								END
							END

							-- Aquí va el bloque de inserción a HCKARDPAC (ajusta según tus variables y modelo)
							-- Ejemplo:
							INSERT INTO [dbo].[HCKARDPAC] (
								[NUMCONSEC], [IPCODPACI], [NUMINGRES], [CODCENATE], [UFUCODIGO], [CODPROSAL], [CODPRODUC], [CANPRODUCT],
								[TIPREGIST], [HCPRESCRN], [HCSOLINSN], [HCCTRAPLN], [HCCTRAPLM], [CODDOCUME], [FECREGKAR],
								[TIPORIREG], [DESMOVPRO], [JUSANULAC], [CONSECFAR], [FECHAUTIL], [OBSERVACI], [CodeSusceptibleMixingStation]
							)
							OUTPUT INSERTED.NUMCONSEC INTO #HCKARDPACConcecutives
							SELECT 
								NEWID(), @PatientCode, @AdmissionNumberHijo, @CareCenterCode, @FunctionalUnitCode,
								@ProfessionalCrystal, @ProductCodeCrystal, @QuantityCrystal, '1',
								@ConsecutivePescription, @ConsecutiveInputs, NULL, NULL, NULL,
								@GetDateTime, @HistoryTypeCrystal, @HistoryTypeNameCrystal, NULL,
								@ConsecutivePharmacy, NULL, NULL, @CodeSusceptibleMixingStation;

							FETCH NEXT FROM DispenseCursor INTO 
								@RowId, @ProductId, @QuantityCrystal, @ProductTypeCrystal, @CantidadSolicitadaCrystal,
								@ProfessionalCrystal, @Extramural, @EntityDId, @CantidadMezcla, @ProductCodeCrystal, @SupplieId, @ATCId;
						END

						CLOSE DispenseCursor;
						DEALLOCATE DispenseCursor;

						-- Validación final: verifica detalles pendientes en la cabecera
						IF EXISTS (
							SELECT 1 FROM dbo.HCFARMEPC hc
								JOIN dbo.HCFARMEPD hd ON hc.CODCONCEC = hd.CODCONCEC
							WHERE hc.CODCONCEC = @ConsecutiveCrystal
								AND hc.ORDESTADO <> 1 -- Cabecera entregada o anulada
								AND hd.PROESTADO = 1 -- Detalle pendiente
								AND hd.CANPENPRO > 0 -- Solo debe bloquear si aun hay cantidad pendiente real
						)
						BEGIN
							SET @CodeMessageResult = '999'
							SET @ErrorsValidationResult = N'Aún hay detalles que se encuentran en estado pendiente'
							SET @DispensingIdResult = 0
							SET @DispensingCodeResult = ''
							SET @StatusResult = 3
							RETURN
						END

						-- Limpieza
						DROP TABLE #DispenseDetail;
						DROP TABLE #HCKARDPACConcecutives; -- Fin Integracion con EHR
				END
				END

				-- valido si afecta o no el inventario
				if @AffectInventory <> 0 begin
					if Exists (select Id from Inventory.PharmaceuticalDispensingDetail With(Nolock)
						where PharmaceuticalDispensingId = @IdPharmaceutical and Id not in 
							(select PharmaceuticalDispensingDetailId from Inventory.PharmaceuticalDispensingDetailBatchSerial bs With(Nolock)
								inner join Inventory.PharmaceuticalDispensingDetail d With(Nolock) on d.Id = bs.PharmaceuticalDispensingDetailId 
								where PharmaceuticalDispensingId = @IdPharmaceutical)) begin

						set @CodeMessageResult = '999'
						set @ErrorsValidationResult = 'La estructura presenta inconsistencias por favor contacte al administrador'
						set @DispensingIdResult = 0
						set @DispensingCodeResult = ''
						set @StatusResult = 3
						RETURN
					END
				END

				IF EXISTS (
					SELECT pd.Id
					FROM Inventory.PharmaceuticalDispensingDetail pd WITH (NOLOCK)
					INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pdb WITH (NOLOCK) ON pd.Id = pdb.PharmaceuticalDispensingDetailId
					INNER JOIN Inventory.PhysicalInventory pin WITH (NOLOCK) ON pin.Id = pdb.PhysicalInventoryId
					INNER JOIN Inventory.InventoryProduct ipd ON pd.ProductId = ipd.Id
					INNER JOIN Inventory.InventoryProduct ipin ON pin.ProductId = ipin.Id
					WHERE pd.PharmaceuticalDispensingId = @IdPharmaceutical
					  AND ipd.ATCId <> ipin.ATCId
					  AND pdb.PhysicalInventoryId IS NOT NULL
				)
				BEGIN
					set @CodeMessageResult = '999'
					set @ErrorsValidationResult = 'Los productos no coinciden entre el detalle y subdetalle por favor contacte al administrador'
					set @DispensingIdResult = 0
					set @DispensingCodeResult = ''
					set @StatusResult = 3
					RETURN
				END

				if Exists (select pd.Id from Inventory.PharmaceuticalDispensingDetail pd With(Nolock)
					inner join Inventory.PharmaceuticalDispensingDetailBatchSerial pdb With(Nolock) on pd.Id = pdb.PharmaceuticalDispensingDetailId 
					inner join Inventory.PhysicalInventoryCustody pin With(Nolock) on pin.Id = pdb.PhysicalInventoryCustodyId 
					where pd.PharmaceuticalDispensingId = @IdPharmaceutical and pd.ProductId <> pin.ProductId
					and pdb.PhysicalInventoryCustodyId IS NOT NULL) begin

					set @CodeMessageResult = '999'
					set @ErrorsValidationResult = 'Los productos no coinciden entre el detalle y subdetalle por favor contacte al administrador'
					set @DispensingIdResult = 0
					set @DispensingCodeResult = ''
					set @StatusResult = 3
					RETURN
				END

				declare @UserNameCreation varchar(200) =''
				if (@IdPharmaceutical > 0) begin
					declare @userCode varchar(20)
					select @userCode = CreationUser  from PharmaceuticalDispensing With(Nolock) where id = @IdPharmaceutical 
					
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

				set @CodeMessageResult = '0'
				SET @ErrorsValidationResult = IIF(ISNULL(@EntityName, '') <> 'PharmaceuticalDispensingTransfer', @MessageReturn+@UserNameCreation, CONCAT('Se guardó y confirmó la dispensación ', @CodePharmaceutical))
				set @DispensingIdResult = isnull(@IdPharmaceutical,0)
				set @DispensingCodeResult = isnull(@CodePharmaceutical, '')
				set @StatusResult = 1
							
				--**********************
				IF @DispensingIntegration = 1 AND @DispensingIdResult > 0 BEGIN
					Set @Rows = 1
					Set @RowId = 1

					while @Rows > 0
					begin
						Select Top 1 
							@RowId = RowId, 
							@ProductIdTmp = ProductId,
							@ProductCodeTmp = CodeProduct,
							@CantidadEntregada = Quantity, 
							@PharmaceuticalDetailIdTmp =IdTmp,
							@CodProductTmp = CodeProduct,
							@EntityIdTmp = EntityId,
							@EntityNameTmp = EntityName
						From @TablePharmaceuticalDetail 
						where RowId >= @RowId 
						Order By RowId

						set @Rows = @@ROWCOUNT
						if @Rows = 0 
							Break

						if @ProductIdTmp >  0 Begin
							declare @productTypeClass tinyint,
									@atcCode varchar(20),
									@IDFisipro Int,
									@PharmaceuticalDispensingDetailId Int,
									@ProductName VARCHAR(300)

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
							    select top 1 @atcCode = atc.Code,
												@PharmaceuticalDispensingDetailId = pdd.Id
								from Inventory.PharmaceuticalDispensing pd WITH(NOLOCK)
								join Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) on pdd.PharmaceuticalDispensingId = pd.Id
								join Inventory.InventoryProduct ipr WITH(NOLOCK) on pdd.ProductId = ipr.Id
								join Inventory.ATC atc WITH(NOLOCK) on ipr.ATCId = atc.Id
								where pd.Id = @DispensingIdResult And pdd.ProductId = @ProductIdTmp AND (pdd.EntityId = @EntityIdTmp AND pdd.EntityName = @EntityNameTmp)
							
							    select top 1 @IDFisipro = ID
							    from HCFISIPRO fi WITH(NOLOCK)
							    where fi.CODPRODUC = @atcCode And fi.NUMINGRES = @AdmissionNumber AND fi.TIPREGIST = 1
							    order by fi.ID desc

							    if @IDFisipro is null begin
							        SET @CodeMessageResult = '999'
							        SET @ErrorsValidationResult = 'No se puede dispensar ya que existen items de producción que no cuentan con cantidades en el inventario físico para el ingreso indicado'
							        SET @DispensingIdResult = 0
							        SET @DispensingCodeResult = ''
							        SET @StatusResult = 3
							        RETURN
							    end
							
							    declare @tmpPhysicalCum Table(
							        Id Int Identity (1, 1) primary key,
							        BatchCode varchar(50),
							        ExpirationDate DateTime,
							        Quantity Int, 
									CodeSusceptibleMixingStation uniqueidentifier
							    )

								DECLARE @TmpGroupingCodes TABLE (
									RowId INT IDENTITY(1,1) PRIMARY KEY,
									ProductId INT,
									Quantity INT,
									BatchCode VARCHAR(50),
									ExpirationDate DateTime,
									GroupingCodeDose UNIQUEIDENTIFIER,
									CodeSusceptibleMixingStation UNIQUEIDENTIFIER
								);
							 
							    delete from @tmpPhysicalCum
							    insert into @tmpPhysicalCum
							    select bs.BatchCode, bs.ExpirationDate, pdbs.Quantity, h.CodeSusceptibleMixingStation 
							    from Inventory.PharmaceuticalDispensingDetailBatchSerial pdbs (nolock)
							    join Inventory.PhysicalInventory pin (nolock) on pdbs.PhysicalInventoryId = pin.Id
							    join Inventory.BatchSerial bs (nolock) on pin.BatchSerialId = bs.Id
								JOIN @TablePharmaceuticalDetail tpb ON bs.ProductId = tpb.ProductId
								JOIN HCFARMEPD h WITH(NOLOCK) ON tpb.EntityId = h.ID
							    where pdbs.PharmaceuticalDispensingDetailId = @PharmaceuticalDispensingDetailId

								DECLARE @BatchCodeTmp VARCHAR(50)
										,@BatchQuantityTmp INT
										,@ExpirationDateTmp Datetime

									SELECT TOP(1) 
									@BatchCodeTmp = BatchCode,
									@BatchQuantityTmp = Quantity,
									@ExpirationDateTmp = ExpirationDate
									FROM @tmpPhysicalCum

									DECLARE @NewId AS UNIQUEIDENTIFIER
									,@MeasurementUnitCode VARCHAR(10)
									,@Dose NUMERIC(18,2)
									,@UnitDoseTypeId INT
									,@CodProSal CHAR(20)
									,@SourceTable varchar(20)
									,@IdSourceTable INT

									SELECT @MeasurementUnitCode = CODUNIMED
									,@Dose = DOSISPROD
									,@CodProSal = CODPROSAL
									,@SourceTable = SourceTable
									,@IdSourceTable = IdSourceTable
									FROM HCFARMEPD
									WHERE ID = @EntityDId

									SELECT TOP 1 @UnitDoseTypeId = rmsd.UnitDoseTypeId
									FROM MixingStation.RequestPackageDetailStatus rpds
									JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.Id = rpds.RequestMixingStationDetailId
									JOIN @tmpPhysicalCum tmp ON tmp.BatchCode = rpds.BatchCode
									WHERE rpds.ProductId = @ProductIdTmp

								IF EXISTS (SELECT 1 FROM @tmpPhysicalCum WHERE CodeSusceptibleMixingStation IS NULL) BEGIN

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
									,@SourceTable
									,@IdSourceTable
									,@ProductName
									,@CantidadEntregada
									,@ProductCodeCrystal
									,@CareCenterCode
									,@FunctionalUnitCode
									,@CodProSal
									,Common.GETDATE())

									UPDATE @tmpPhysicalCum 
									SET CodeSusceptibleMixingStation = @NewId
									WHERE CodeSusceptibleMixingStation IS NULL

									--INSERT INTO MedicalHistory.PharmaDose
									--	(IDHCFARMEPC, CodeSusceptibleMixingStation, ProductCode, MeasurementUnitCode, UnitDoseTypeId, Dose,
									--	 GroupingCodeDose, DeliveryStatus, QuantityReceivable, AppliedDose, AppliedDateDose, MixingStationId, IsDispensed, IsTransformedProductStandardDispensation)
									--OUTPUT @ProductIdTmp,1, @BatchCodeTmp, @ExpirationDateTmp, INSERTED.GroupingCodeDose, @NewId
									--INTO @TmpGroupingCodes(ProductId,Quantity, BatchCode, ExpirationDate, GroupingCodeDose, CodeSusceptibleMixingStation)
									--SELECT
									--	@ConsecutiveCrystal, @NewId, @ProductCodeCrystal, @MeasurementUnitCode, @UnitDoseTypeId, @Dose,
									--	NEWID(), 0, 0, 0, NULL, NULL, 1, 1
									--FROM (SELECT TOP(@BatchQuantityTmp) 1 AS n FROM sys.all_objects) AS Numbers

									--   DECLARE @Row INT = 1, @MaxRows INT
									--	SELECT @MaxRows = COUNT(*) FROM @TmpGroupingCodes

									--	WHILE @Row <= @MaxRows
									--	BEGIN
									--		DECLARE @RowProductId INT, @RowBatchCode VARCHAR(50), @RowGroupingCodeDose UNIQUEIDENTIFIER
									--		SELECT @RowProductId = ProductId, @RowBatchCode = BatchCode, @RowGroupingCodeDose = GroupingCodeDose
									--		FROM @TmpGroupingCodes WHERE RowId = @Row

									--		UPDATE TOP (1) rpds
									--		SET GroupingCodeDose = @RowGroupingCodeDose
									--		FROM MixingStation.RequestPackageDetailStatus rpds
									--		WHERE rpds.ProductId = @RowProductId AND rpds.BatchCode = @RowBatchCode AND rpds.GroupingCodeDose IS NULL

									--		SET @Row = @Row + 1
									--	END

									--UPDATE kp SET CodeSusceptibleMixingStation = @NewId
									--FROM HCKARDPAC kp WITH(NOLOCK)
									--JOIN @HCKARDPACConcecutives ik ON kp.NUMCONSEC = ik.NUMCONSEC
									--WHERE kp.CodeSusceptibleMixingStation IS NULL AND kp.CODPRODUC = @atcCode
								END

								IF NOT EXISTS (SELECT 1 FROM MedicalHistory.PharmaDose ph
															JOIN @tmpPhysicalCum tmp ON tmp.CodeSusceptibleMixingStation = ph.CodeSusceptibleMixingStation) BEGIN

									IF @NewId IS NULL BEGIN
										SET @NewId = (SELECT TOP 1 CodeSusceptibleMixingStation FROM @tmpPhysicalCum)
									END

									INSERT INTO MedicalHistory.PharmaDose
										(IDHCFARMEPC, CodeSusceptibleMixingStation, ProductCode, MeasurementUnitCode, UnitDoseTypeId, Dose,
										 GroupingCodeDose, DeliveryStatus, QuantityReceivable, AppliedDose, AppliedDateDose, MixingStationId, IsDispensed, IsTransformedProductStandardDispensation)
									OUTPUT @ProductIdTmp,1, @BatchCodeTmp, @ExpirationDateTmp, INSERTED.GroupingCodeDose, @NewId
									INTO @TmpGroupingCodes(ProductId,Quantity, BatchCode, ExpirationDate, GroupingCodeDose, CodeSusceptibleMixingStation)
									SELECT
										@ConsecutiveCrystal, @NewId, @ProductCodeCrystal, @MeasurementUnitCode, @UnitDoseTypeId, @Dose,
										NEWID(), 0, 0, 0, NULL, NULL, 1, 1
									FROM (SELECT TOP(@BatchQuantityTmp) 1 AS n FROM sys.all_objects) AS Numbers

									   DECLARE @Row INT = 1, @MaxRows INT
										SELECT @MaxRows = COUNT(*) FROM @TmpGroupingCodes

										WHILE @Row <= @MaxRows
										BEGIN
											DECLARE @RowProductId INT, @RowBatchCode VARCHAR(50), @RowGroupingCodeDose UNIQUEIDENTIFIER
											SELECT @RowProductId = ProductId, @RowBatchCode = BatchCode, @RowGroupingCodeDose = GroupingCodeDose
											FROM @TmpGroupingCodes WHERE RowId = @Row

											UPDATE TOP (1) rpds
											SET GroupingCodeDose = @RowGroupingCodeDose
											FROM MixingStation.RequestPackageDetailStatus rpds
											WHERE rpds.ProductId = @RowProductId AND rpds.BatchCode = @RowBatchCode AND rpds.GroupingCodeDose IS NULL

											SET @Row = @Row + 1
										END

									UPDATE kp SET CodeSusceptibleMixingStation = @NewId
									FROM HCKARDPAC kp WITH(NOLOCK)
									JOIN @HCKARDPACConcecutives ik ON kp.NUMCONSEC = ik.NUMCONSEC
									WHERE kp.CodeSusceptibleMixingStation IS NULL AND kp.CODPRODUC = @atcCode
								END
								
								IF EXISTS (SELECT 1 FROM @TmpGroupingCodes) AND @SendTo = 1 BEGIN
								
								INSERT INTO @TmpGroupingCodes(ProductId,Quantity, BatchCode, ExpirationDate, GroupingCodeDose, CodeSusceptibleMixingStation)
								SELECT 
									@ProductIdTmp,
									@BatchQuantityTmp,
									tpc.BatchCode,
									tpc.ExpirationDate,
									pd.GroupingCodeDose,
									tpc.CodeSusceptibleMixingStation
								FROM @tmpPhysicalCum tpc
								JOIN MedicalHistory.PharmaDose pd ON pd.CodeSusceptibleMixingStation = tpc.CodeSusceptibleMixingStation
								WHERE pd.ProductCode = @ProductCodeCrystal
								  AND NOT EXISTS (
									  SELECT 1 FROM @TmpGroupingCodes tc
									  WHERE tc.ProductId = @ProductIdTmp AND tc.BatchCode = tpc.BatchCode AND tc.GroupingCodeDose = pd.GroupingCodeDose);
								END
								ELSE BEGIN
									INSERT INTO @TmpGroupingCodes(ProductId,Quantity, BatchCode, ExpirationDate, GroupingCodeDose, CodeSusceptibleMixingStation)
									SELECT 
										@ProductIdTmp,
										@BatchQuantityTmp,
										tpc.BatchCode,
										tpc.ExpirationDate,
										NULL,
										tpc.CodeSusceptibleMixingStation
									FROM @tmpPhysicalCum tpc
								END

							    DECLARE 
									@RowIdTmp INT,
									@GroupingCodeDose UNIQUEIDENTIFIER,
									@BatchCode VARCHAR(50),
									@BatchQuantity INT,
									@ExpirationDate DATETIME,
									@IdDetailPhysicalCUM INT,
									@CodeSusceptibleMixingStationTmp UNIQUEIDENTIFIER

								-- Declara el cursor para recorrer @TmpGroupingCodes
								DECLARE cur CURSOR FOR
									SELECT RowId, ProductId, IIF(@SendTo = 1,@BatchCodeTmp, BatchCode) , GroupingCodeDose, Quantity, IIF(@SendTo = 1, @ExpirationDateTmp, ExpirationDate), CodeSusceptibleMixingStation
									FROM @TmpGroupingCodes
									ORDER BY RowId

								OPEN cur
								FETCH NEXT FROM cur INTO @RowIdTmp, @ProductIdTmp, @BatchCode , @GroupingCodeDose, @BatchQuantity, @ExpirationDate, @CodeSusceptibleMixingStationTmp

								WHILE @@FETCH_STATUS = 0
								BEGIN

							        IF @GroupingCodeDose IS NULL BEGIN
							            SET @GroupingCodeDose = (
							            SELECT TOP 1 pd.GroupingCodeDose
										FROM MedicalHistory.PharmaDose pd WITH(NOLOCK)
										JOIN MixingStation.RequestPackageDetailStatus rpds 
											ON pd.GroupingCodeDose = rpds.GroupingCodeDose AND rpds.BatchCode = @BatchCode
										WHERE pd.CodeSusceptibleMixingStation = @CodeSusceptibleMixingStationTmp)
							        END

									-- Actualiza (o inserta) la cantidad despachada en DetailPhysicalCUM
									UPDATE TOP (1) dpc
										SET DispensedQuantity += @BatchQuantity
									FROM MedicalHistory.DetailPhysicalCUM dpc WITH(NOLOCK)
									WHERE dpc.IDHCFISIPRO = @IDFisipro
										AND dpc.ProductId = @ProductIdTmp
										AND dpc.GroupingCodeDose = @GroupingCodeDose

									IF @@ROWCOUNT = 0
									BEGIN
										INSERT INTO MedicalHistory.DetailPhysicalCUM
											(IDHCFISIPRO, BatchCode, ProductId, DateExpiration, [Hour], DispensedQuantity, GroupingCodeDose, UsedQuantity)
										VALUES
											(@IDFisipro, @BatchCode , @ProductIdTmp, @ExpirationDate, Common.GETDATE(), @BatchQuantity, @GroupingCodeDose, 0)

										-- Obtiene el ID de la inserción reciente
										SET @IdDetailPhysicalCUM = SCOPE_IDENTITY()

										--select * from MedicalHistory.DetailPhysicalCUM WITH(NOLOCK)
										--WHERE Id = @IdDetailPhysicalCUM
									END
									ELSE
									BEGIN
										-- Obtiene el ID actualizado reciente
										SELECT @IdDetailPhysicalCUM = ID
										FROM MedicalHistory.DetailPhysicalCUM dpc WITH(NOLOCK)
										WHERE dpc.IDHCFISIPRO = @IDFisipro
											AND dpc.ProductId = @ProductIdTmp
											AND dpc.GroupingCodeDose = @GroupingCodeDose
									END
									-- Si existe como dispensado, marca como entregado
									IF EXISTS (
										SELECT 1
										FROM MedicalHistory.PharmaDose pd WITH(NOLOCK)
										WHERE pd.GroupingCodeDose = @GroupingCodeDose AND pd.IsDispensed = 1
									)
									BEGIN    
										UPDATE pd SET DeliveryStatus = 1
										FROM MedicalHistory.PharmaDose pd (nolock) 
										WHERE pd.GroupingCodeDose = @GroupingCodeDose
									END

									UPDATE TOP (1) kp 
										SET IdDetailPhysicalCUM = @IdDetailPhysicalCUM,
											DESMOVPRO = 'Despacho farmacia, origen solicitud: Producto procesado por la central de mezclas - usuario: (' + @UserName + ') - lote: ' + @BatchCode 
									FROM HCKARDPAC kp WITH(NOLOCK)
									JOIN @HCKARDPACConcecutives ik ON kp.NUMCONSEC = ik.NUMCONSEC
									WHERE kp.IdDetailPhysicalCUM IS NULL AND kp.CODPRODUC = @atcCode

									FETCH NEXT FROM cur INTO @RowIdTmp, @ProductIdTmp, @BatchCode , @GroupingCodeDose, @BatchQuantity, @ExpirationDate, @CodeSusceptibleMixingStationTmp
								END

								CLOSE cur
								DEALLOCATE cur
							END
						END

						SET @RowId += 1
					end
				end
				--**********************

				RETURN
			END --- Fin si esta confirmando
		END --- Fin si vienen dispensaciones
		
	
		-- valido si afecta o no el inventario
		if @AffectInventory <> 0 begin
			if Exists (select Id from Inventory.PharmaceuticalDispensingDetail With(Nolock)
				where PharmaceuticalDispensingId = @IdPharmaceutical and Id not in 
					(select PharmaceuticalDispensingDetailId 
					from Inventory.PharmaceuticalDispensingDetailBatchSerial bs With(Nolock)
					inner join Inventory.PharmaceuticalDispensingDetail d With(Nolock) On d.Id = bs.PharmaceuticalDispensingDetailId 
					where PharmaceuticalDispensingId = @IdPharmaceutical)) begin
				set @CodeMessageResult = '999'
				set @ErrorsValidationResult = 'La estructura presenta inconsistencias por favor contacte al administrador'
				set @DispensingIdResult = 0
				set @DispensingCodeResult = ''
				set @StatusResult = 3
				RETURN
			END
		END
		IF EXISTS (SELECT pd.Id FROM Inventory.PharmaceuticalDispensingDetail pd WITH(NOLOCK)
			INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pdb WITH(NOLOCK) ON pd.Id = pdb.PharmaceuticalDispensingDetailId 
			INNER JOIN Inventory.PhysicalInventory pin WITH(NOLOCK) ON pin.Id = pdb.PhysicalInventoryId 
			WHERE pd.PharmaceuticalDispensingId = @IdPharmaceutical AND pd.ProductId <> pin.ProductId
			AND pdb.PhysicalInventoryId IS NOT NULL) BEGIN
			SET @CodeMessageResult = '999'
			SET @ErrorsValidationResult = 'Los productos no coinciden entre el detalle y subdetalle por favor contacte al administrador'
			SET @DispensingIdResult = 0
			SET @DispensingCodeResult = ''
			SET @StatusResult = 3
			RETURN
		END

		IF EXISTS (SELECT pd.Id FROM Inventory.PharmaceuticalDispensingDetail pd WITH(NOLOCK)
			INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pdb WITH(NOLOCK) ON pd.Id = pdb.PharmaceuticalDispensingDetailId 
			INNER JOIN Inventory.PhysicalInventoryCustody pin WITH(NOLOCK) ON pin.Id = pdb.PhysicalInventoryCustodyId 
			WHERE pd.PharmaceuticalDispensingId = @IdPharmaceutical AND pd.ProductId <> pin.ProductId
			AND pdb.PhysicalInventoryCustodyId IS NOT NULL) BEGIN
			SET @CodeMessageResult = '999'
			SET @ErrorsValidationResult = 'Los productos no coinciden entre el detalle y subdetalle por favor contacte al administrador'
			SET @DispensingIdResult = 0
			SET @DispensingCodeResult = ''
			SET @StatusResult = 3
			RETURN
		END
		
		SET @CodeMessageResult = '0'
		SET @ErrorsValidationResult = 'Se guardo correctamente la dispensacion ' + @CodePharmaceutical
		SET @DispensingIdResult = ISNULL(@IdPharmaceutical,0)
		SET @DispensingCodeResult = ISNULL(@CodePharmaceutical, '')
		SET @StatusResult = 1

	END TRY
	BEGIN CATCH
		SET @CodeMessageResult = '999'
		SET @ErrorsValidationResult = (SELECT ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)))
		SET @DispensingIdResult = 0
		SET @DispensingCodeResult = ''
		SET @StatusResult = 3
	END CATCH

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera y confirma dispensaciones farmacéuticas de salida en el módulo de inventario. Gestiona el proceso completo de entrega de medicamentos e insumos al paciente o al servicio hospitalario: valida los detalles del producto (medicamento, cantidad, precio, lote, bodega, tipo de liquidación), actualiza el inventario, registra los consumos por ingreso/admisión del paciente y, si aplica, anula ítems pendientes desde el tablero de farmacia. Compone información del catálogo de productos (InventoryProduct), del tercero o aseguradora (ThirdParty), del número de ingreso hospitalario del paciente (CHREGESTA, HCINGRESORECNAC) y del usuario que ejecuta la operación (Security.User / Security.Person), y devuelve el código e identificador de la dispensación generada junto con el estado del proceso y los mensajes de error de validación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePharmaceuticalDispensing_OutputOptimized';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePharmaceuticalDispensing_OutputOptimized';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procesa, guarda y/o confirma dispensaciones farmacéuticas (incluyendo anulaciones desde dashboard, integración con HEON/Medilaser, hoja de gasto quirúrgico, afectación de inventario/kardex y contabilidad), retornando códigos y mensajes de resultado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDispensing_OutputOptimized';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de dispensación debe traer los parámetros IsPharmaceuticalDispensing y DispensingIntegration; si están vacíos se aborta con código ''999''.; DocumentDate no debe caer en un período cerrado registrado en Inventory.SettingInventory (Year/Month).; Para anulaciones que dejan la solicitud sin pendientes o con AnulateOnly=1, debe enviarse HCMOANULBId no nulo/vacío, de lo contrario se devuelve ''No hay motivo de anulación''.; Los detalles deben tener Quantity > 0 después del cálculo por lotes; en caso contrario se rechaza.; Si afecta inventario, los productos cuyo subgrupo HandlesBatch=1 y bodega no virtual deben traer detalles de lotes (@TablePharmaceuticalBatch).; Para productos con AverageCost = 0 (no en custodia) se rechaza la dispensación.; Para confirmar (Status=2) debe existir Inventory.SettingInventory para la OperatingUnitId.; Para integración Heon (DispensingIntegration=2) se requieren los parámetros OfficeType, LogisticOperator y MedicalOrderRecipe.; Si DispensingWithoutAuthorization=0 en SettingInventory, la cantidad dispensada de productos no POS no puede superar la cantidad autorizada en HCJUNOPOM (saldo MIPRES).; No se permite dispensar desde almacenes en tránsito (Warehouse.TransitStore=1).; No se permite dispensar lotes vencidos (BatchSerial.ExpirationDate < DocumentDate).; Si la dispensación viene de integración Heon (=2) y la orden médica ya tiene un proceso pendiente (Status=2) en ControlIntegrationHeonDetail se rechaza.; Si ya se generó una Órden de servicio en Billing.ServiceOrder para el código, no se permite re-procesar.; Para productos ProductTypeId=36 (PET) debe existir tarifa vigente en ProductRateGeneral; si no, se rechaza con ''No se encontró tarifa PET para el producto''.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDispensing_OutputOptimized';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDispensing_OutputOptimized';
-- GO
