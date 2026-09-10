-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-03-22
-- Description:	Procedimiento para guardar o confirmar las dispensaciones farmaceuticas
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SavePharmaceuticalDispensingNative_Output]
	@XmlPharmaceutical XML,
	@UserCode VARCHAR(20),
	---------------------------------------------------------------------------
	@CodeResult Int OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	------------------------------------------------------
	@Id INT OUTPUT,
	@Code VARCHAR(20) OUTPUT	
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Prefix VARCHAR(4),
			@OperatingUnitId INT,
			@AdmissionNumberOrigin VARCHAR(20),
			@AdmissionNumber VARCHAR(20),
			@PatientCode VARCHAR(20),
			@PatientThirdPartyId INT,
			@DocumentDate DATETIME,
			@AffectInventory BIT,
			@SurgeryExpenseSheetId INT,
			@Status TINYINT,
			@EntityName VARCHAR(250),
			@EntityCode VARCHAR(20),
			@EntityId INT,
			@IsPharmaceuticalDispensing TINYINT,
			@DispensingIntegration TINYINT,
			-----------------------------------------------
			@InitDate DATE,
			@CareCenterCode VARCHAR(20),
			@FunctionalUnitCode VARCHAR(20),
			@ConsecutivePharmacy VARCHAR(20),			
			@HistoryType VARCHAR(20),
			@ConsecutivePescription INT,
			@ConsecutiveInputs VARCHAR(20),
			@ConsecutiveCrystal VARCHAR(20),
			@IDHCORDPRON INT, --Permite saber si el registro de la pestaña de quimioterapia es de tipo domiciliaria
			@QuantityStay INT,
			-----------------------------------------------
			@Rows INT,
			@RowId INT,
			@PharmaceuticalDispensingDetailId INT,
			@PharmaceuticalDispensingThirdPartyId INT,
			-----------------------------------------------
			@IdForm INT = 322,
			@DocumentTypeControl INT = 5,
			-----------------------------------------------
			@Message VARCHAR(MAX),
			-----------------------------------------------
			@SettingInventoryId INT,
			@AssociateCostCenter TINYINT,
			@AssociateCostMainAccount TINYINT,
			@PharmaceuticalDispensingGetThirdParty TINYINT,
			@PharmaceuticalDispensingThirdParty INT,
			@JournalVoucherId INT,
			-----------------------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			-----------------------------------------------
			@Extramural BIT,
			----------------------------------------------- QUIMIOTERAPIA
			@IDHCORDCICLOS INT,
			@IDHCORDQUIMIO INT,
			@CICLO INT,
			----------------------------------------------- HOJA DE GASTO QUIRURGICA
			@IdCabeceraHojaGastoQX INT,
			@GuardaGastoQX BIT,
			@IdProgramacionQXPrincipal INT,
			@ConsecutiveHojaGastoQX DECIMAL(18,0),
			-----------------------------------------------
			@HistoryTypeCrystal INT,
			@HistoryTypeNameCrystal VARCHAR(MAX),
			@ConsecutiveKardexDispesing DECIMAL(18,0)

	--Tabla temporal de los detalles
	DECLARE @Detail TABLE
	(
		IdTmp INT,
		Id INT,
		-----------------------------------------------
		CareGroupId INT NOT NULL,
		HealthAdministratorId INT,
		ThirdPartyId INT,
		ProductType VARCHAR(20),
		ProductId INT NOT NULL,
		CodeProduct VARCHAR(20),
		WarehouseId INT NOT NULL,
		CantidadSolicitada INT, 
		Quantity INT NOT NULL,
		CantidadPendiente INT,
		ServiceDate DATETIME NOT NULL,
		FunctionalUnitId INT NOT NULL,
		OrderedHealthProfessionalCode CHAR(20),
		OrderedProfessionalSpecialty CHAR(3),
		OrderedHealthProfessionalThirdPartyId INT,
		AuthorizationNumber VARCHAR(20),
		LiquidationType TINYINT NOT NULL,
		SurchargeApply BIT NOT NULL,
		SalePrice numeric(20, 4) NOT NULL,
		AverageCost numeric(20, 4) NOT NULL,
		DiscountPercentage numeric(5, 2) NOT NULL,
		DiscountValue numeric(20, 4) NOT NULL,
		TotalSalesPrice numeric(20, 4) NOT NULL,
		GrandTotalSalesPrice numeric(20, 4) NOT NULL,
		EntityId INT,
		EntityName VARCHAR(250),
		ChangeTracker VARCHAR(30),
		-----------------------------------------------
		GuardaGastoQX  BIT,
		IdProgramacionQXPrincipal INT,
		Extramural BIT,
		QuotationPharmaceuticalDispensingDetailId INT		
	)

	--Tabla temporal de los subdetalles
	DECLARE @DetailBatchSerial TABLE
	(	
		PharmaceuticalDetailIdTmp INT,
		Id INT NOT NULL,		
		PharmaceuticalDispensingDetailId INT NOT NULL,
		PhysicalInventoryId INT,
		Quantity INT NOT NULL,
		OutstandingQuantity INT NOT NULL,
		PhysicalInventoryCustodyId INT,
		ChangeTracker VARCHAR(30)
	)

	--tabla temporal para almacenar el resultado del movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT	@Prefix = t.x.value('Prefix[1]','varchar(4)'),
				@OperatingUnitId = t.x.value('OperatingUnitId[1]','INT'),
				@Id = t.x.value('Id[1]','INT'),
				@Code = t.x.value('Code[1]','VARCHAR(20)'),
				@AdmissionNumberOrigin = t.x.value('AdmissionNumberOrigin[1]','VARCHAR(20)'),
				@AdmissionNumber = t.x.value('AdmissionNumber[1]','VARCHAR(20)'),
				@DocumentDate = t.x.value('DocumentDate[1]', 'DATETIME'),
				@AffectInventory = t.x.value('AffectInventory[1]','BIT'),
				@Status = t.x.value('Status[1]','TINYINT'),
				@EntityName = t.x.value('EntityName[1]','VARCHAR(250)'),
				@EntityCode = t.x.value('EntityCode[1]','VARCHAR(20)'),
				@EntityId = t.x.value('EntityId[1]','INT'),
				@IsPharmaceuticalDispensing = t.x.value('IsPharmaceuticalDispensing[1]','TINYINT'),
				@DispensingIntegration = t.x.value('DispensingIntegration[1]','TINYINT'),
				-------------------------------------------------------------------
				@InitDate = DATEFROMPARTS(1900, 1, 1),
				@CareCenterCode = t.x.value('CareCenterCode[1]','VARCHAR(20)'),
				@FunctionalUnitCode = t.x.value('FunctionUnitCode[1]','VARCHAR(20)'),
				@ConsecutivePharmacy = t.x.value('ConsecutivePharmacy[1]','VARCHAR(20)'),			
				@HistoryType =  t.x.value('HistoryType[1]','VARCHAR(20)'),
				@ConsecutivePescription =  t.x.value('ConsecutivePescription[1]','INT'),
				@ConsecutiveInputs =  t.x.value('ConsecutiveInputs[1]','VARCHAR(20)'),
				@ConsecutiveCrystal = t.x.value('ConsecutiveCrystal[1]','VARCHAR(20)'),
				@IDHCORDPRON = t.x.value('IDHCORDPRON[1]','INT') --Permite saber si el registro de la pestaña de quimioterapia es de tipo domiciliaria
		FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x)

		IF EXISTS (SELECT 1 FROM Inventory.PharmaceuticalDispensing WITH (NOLOCK) WHERE Id = @Id AND Status <> 1)
		BEGIN
			SELECT @CodeResult = 999, 
				   @MessageResult = 'La Dispensación Farmaceutica se encuentra en estado: ' + IIF(Status = 2, 'Confirmado', 'Anulado')
			FROM Inventory.PharmaceuticalDispensing WITH (NOLOCK)
			WHERE Id = @Id
			RETURN
		END 
		
		IF @Status = 3
		BEGIN
			UPDATE [Inventory].[PharmaceuticalDispensing]
				SET [Status] = @Status,
					[ModificationUser] = @UserCode,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @UserCode,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Id = @Id

			--Se nulea el campo de cotización
			UPDATE Inventory.PharmaceuticalDispensingDetail 
				SET QuotationPharmaceuticalDispensingDetailId = NULL
			WHERE PharmaceuticalDispensingId = @Id
		END
		ELSE
		BEGIN
			/************************************  CARGUE DE INFORMACION CABECERA ************************************/

			SELECT	@SettingInventoryId = Id,
					@AssociateCostCenter = AssociateCostCenter,
					@AssociateCostMainAccount = AssociateCostMainAccount,
					@PharmaceuticalDispensingGetThirdParty = PharmaceuticalDispensingGetThirdParty,
					@PharmaceuticalDispensingThirdParty = PharmaceuticalDispensingThirdPartyId
			FROM Inventory.SettingInventory WITH (NOLOCK) 
			WHERE OperatingUnitId = @OperatingUnitId

			SELECT	@PatientCode = IPCODPACI,
					@PatientThirdPartyId = tp.Id
			FROM dbo.ADINGRESO ad WITH (NOLOCK) 
			LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON ad.IPCODPACI = tp.Nit
			WHERE NUMINGRES = @AdmissionNumber

			--- Valido si esta desde el dashboard que haya cambiado de unidad funcional
			SELECT @QuantityStay = COUNT(1) 
			FROM dbo.CHREGESTA WITH (NOLOCK) 
			WHERE NUMINGRES = @AdmissionNumber AND CAST(FECFINEST AS DATE) <= @InitDate

			IF (@QuantityStay = 1 AND @FunctionalUnitCode IS NOT NULL) 
			BEGIN -- Si solo esta en una cama
				SELECT @FunctionalUnitCode = ca.UFUCODIGO 
				FROM dbo.CHREGESTA ch WITH (NOLOCK)
				JOIN dbo.CHCAMASHO ca WITH (NOLOCK) ON ca.CODICAMAS = ch.CODICAMAS 
				WHERE ch.NUMINGRES = @AdmissionNumber AND CAST(FECFINEST AS DATE) <= @InitDate
			END
			ELSE IF @QuantityStay > 1 
			BEGIN -- Si el paciente tiene orden de traslado
				SET @FunctionalUnitCode = NULL

				SELECT TOP 1 @FunctionalUnitCode = uni.UFUCODIGO 
				FROM dbo.CHREGESTA ch WITH (NOLOCK) 
				JOIN dbo.CHCAMASHO ca WITH (NOLOCK) ON ca.CODICAMAS = ch.CODICAMAS 
				JOIN dbo.INUNIFUNC uni WITH (NOLOCK) ON uni.UFUCODIGO = ca.UFUCODIGO 
				WHERE ch.NUMINGRES = @AdmissionNumber AND CAST(FECFINEST AS DATE) <= @InitDate AND uni.UFUTIPUNI = 19
			END

			/****************************************** VALIDACION CABECERA ******************************************/

			IF NOT EXISTS (SELECT 1 FROM Inventory.SettingInventory WITH (NOLOCK) WHERE OperatingUnitId = @OperatingUnitId)
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = 'No se encontraron parametros de inventarios creados para la unidad operativa'
				RETURN
			END

			IF EXISTS (SELECT 1 FROM Inventory.SettingInventory WITH (NOLOCK) WHERE @DocumentDate < DATEFROMPARTS(Year, Month, 1))
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = 'No se puede dispensar en un periodo cerrado'
				RETURN
			END

			IF EXISTS (SELECT 1 FROM Billing.ServiceOrder WITH (NOLOCK) WHERE EntityCode = @Code AND EntityName = 'PharmaceuticalDispensing')
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = CONCAT('Ya existe una orden de servicio generada a partir de la dispensación ', @Code)
				RETURN
			END

			--- Valido que tenga entidad administradora
			IF EXISTS (SELECT NUMINGRES FROM dbo.ADINGRESO WITH (NOLOCK) WHERE NUMINGRES = @AdmissionNumber AND GENCONENTITY IS NULL)
			BEGIN 
				SELECT	@CodeResult = 999,
						@MessageResult = CONCAT('El ingreso ', @AdmissionNumber, ' no tiene asignada una entidad administradora de salud')
				RETURN
			END

			--- Valido que la entidad administradora este en la tabla
			IF EXISTS 
			(
				SELECT 1 
				FROM dbo.ADINGRESO ing WITH (NOLOCK)
				LEFT JOIN [Contract].HealthAdministrator ha WITH(NOLOCK) ON ing.GENCONENTITY = ha.Id
				WHERE ing.NUMINGRES = @AdmissionNumber AND ha.Id IS NULL
			)
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = CONCAT('No se encontro la entidad administradora asociada al ingreso ', @AdmissionNumber)
				RETURN
			END
			
			IF ISNULL(@PatientThirdPartyId, 0) = 0 
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = CONCAT('El paciente con identificacion ', @PatientCode, ' no existe como tercero en Indigo Vie')
				RETURN
			END

			IF @FunctionalUnitCode IS NOT NULL AND NOT EXISTS (SELECT 1 FROM Payroll.FunctionalUnit WITH (NOLOCK) WHERE Code = @FunctionalUnitCode) 
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = CONCAT('La unidad funcional ', @FunctionalUnitCode, ' no esta homologada en Indigo Vie')
				RETURN
			END

			IF @QuantityStay > 1
			BEGIN
				IF EXISTS 
				(
					SELECT 1 
					FROM dbo.CHREGESTA ch WITH (NOLOCK) 
					JOIN dbo.CHCAMASHO ca WITH (NOLOCK) ON ca.CODICAMAS = ch.CODICAMAS
					WHERE ch.NUMINGRES = @AdmissionNumber AND ca.CODCONCEC IS NOT NULL AND CAST(FECFINEST AS DATE) <= @InitDate
				)
				BEGIN
					SELECT	@CodeResult = 999,
							@MessageResult = 'El paciente tiene pendiente una aceptación de medicamentos por motivo de traslado de hospitalización'
					RETURN
				END

				IF @FunctionalUnitCode IS NULL
				BEGIN
					SELECT	@CodeResult = 999,
							@MessageResult = 'El paciente tiene un error en el modulo de hospitalización, está asignado en dos o mas camas'
					RETURN
				END
			END

			-----------------------------------------------------------------------------------------------------------

			INSERT INTO @Detail 
			(
				IdTmp, Id, CareGroupId, HealthAdministratorId, ThirdPartyId, ProductType, ProductId, CodeProduct, WarehouseId, CantidadSolicitada, Quantity, CantidadPendiente, 
				ServiceDate, FunctionalUnitId, OrderedHealthProfessionalCode, OrderedProfessionalSpecialty, OrderedHealthProfessionalThirdPartyId, AuthorizationNumber, 
				LiquidationType, SurchargeApply, SalePrice, AverageCost, DiscountPercentage, DiscountValue, TotalSalesPrice, GrandTotalSalesPrice, 
				EntityId, EntityName, ChangeTracker, GuardaGastoQX,IdProgramacionQXPrincipal,Extramural, QuotationPharmaceuticalDispensingDetailId
			)
				SELECT	t.x.value('PharmaceuticalDispensingDetailIdTmp[1]','INT'),
						t.x.value('Id[1]','INT'),
						t.x.value('CareGroupId[1]','INT'),
						t.x.value('HealthAdministratorId[1]','INT'),
						t.x.value('ThirdPartyId[1]','INT'),
						t.x.value('ProductType[1]','VARCHAR(20)'),
						t.x.value('ProductId[1]','INT'),
						t.x.value('CodeProduct[1]','VARCHAR(20)'),
						t.x.value('WarehouseId[1]','INT'),
						t.x.value('CantidadSolicitada[1]','INT'),
						t.x.value('Quantity[1]','INT'),
						t.x.value('CantidadPendiente[1]','INT'),
						t.x.value('ServiceDate[1]', 'DATETIME'),
						t.x.value('FunctionalUnitId[1]','INT'),
						t.x.value('OrderedHealthProfessionalCode[1]','VARCHAR(20)'),
						t.x.value('OrderedProfessionalSpecialty[1]','VARCHAR(3)'),
						t.x.value('OrderedHealthProfessionalThirdPartyId[1]','INT'),
						t.x.value('AuthorizationNumber[1]','VARCHAR(20)'),
						t.x.value('LiquidationType[1]','TINYINT'),
						t.x.value('SurchargeApply[1]','BIT'),
						t.x.value('SalePrice[1]','DECIMAL(18,2)'),
						t.x.value('AverageCost[1]','DECIMAL(18,2)'),
						t.x.value('DiscountPercentage[1]','DECIMAL(5,2)'),
						t.x.value('DiscountValue[1]','DECIMAL(18,2)'),
						t.x.value('TotalSalesPrice[1]','DECIMAL(18,2)'),
						t.x.value('GrandTotalSalesPrice[1]','DECIMAL(18,2)'),
						t.x.value('EntityId[1]','INT'),
						t.x.value('EntityName[1]','VARCHAR(250)'),
						t.x.value('ChangeTracker[1]', 'VARCHAR(30)'),
						t.x.value('GuardaGastoQX[1]','BIT'),
						t.x.value('IdProgramacionQXPrincipal[1]','INT'),
						t.x.value('Extramural[1]','BIT'),
						t.x.value('QuotationPharmaceuticalDispensingDetailId[1]','INT')
				FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing/PharmaceuticalDispensingDetail') t(x)

			INSERT INTO @DetailBatchSerial 
			(
				PharmaceuticalDetailIdTmp,Id,PharmaceuticalDispensingDetailId,PhysicalInventoryId,Quantity,OutstandingQuantity,PhysicalInventoryCustodyId,ChangeTracker
			)
				SELECT	t.x.value('PharmaceuticalDispensingDetailIdTmp[1]','INT'),				
						t.x.value('Id[1]','INT'),
						t.x.value('PharmaceuticalDispensingDetailId[1]','INT'),
						t.x.value('PhysicalInventoryId[1]','INT'),
						t.x.value('Quantity[1]','INT'),
						t.x.value('OutstandingQuantity[1]','INT'),
						t.x.value('PhysicalInventoryCustodyId[1]','INT'),
						t.x.value('ChangeTracker[1]', 'VARCHAR(30)')
				FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing/PharmaceuticalDispensingDetail/PharmaceuticalDispensingDetailBatchSerial') t(x)

			/************************************* CARGUE DE INFORMACION DETALLE *************************************/

			SELECT TOP 1 
					@Extramural = MAX(CAST(Extramural AS TINYINT)),
					@GuardaGastoQX = MAX(CAST(GuardaGastoQX AS TINYINT)), 
					@IdProgramacionQXPrincipal = MAX(IdProgramacionQXPrincipal)
			FROM @Detail 
			WHERE ChangeTracker <> 'Deleted'

			SELECT TOP 1 @Prefix = w.Prefix
			FROM @Detail tpdd
			JOIN Inventory.Warehouse w WITH (NOLOCK) ON tpdd.WarehouseId	= w.Id
			WHERE tpdd.ChangeTracker <> 'Deleted'

			IF @IsPharmaceuticalDispensing = 0 
			BEGIN
				--Siempre afecto inventario, solo no lo hago si todos los detalles provienen de almacenes virtuales
				IF EXISTS (SELECT 1 FROM @Detail tpdd JOIN Inventory.WareHouse w WITH (NOLOCK) ON tpdd.WareHouseId = w.Id WHERE tpdd.ChangeTracker <> 'Deleted' AND w.VirtualStore = 0)
				BEGIN
					SET @AffectInventory = 1
				END
				ELSE 
				BEGIN
					SET @AffectInventory = 0
				END	
			END
			ELSE IF @IsPharmaceuticalDispensing = 1
			BEGIN
				IF ISNULL(@EntityName, '') = ''
				BEGIN
					SET @EntityName = 'SaveDashboardPharmacyDevolution'					
				END
				SET @EntityId = @ConsecutiveCrystal
			END

			UPDATE tpdd 
				SET tpdd.CodeProduct = COALESCE(atc.Code, ins.Code, ip.Code),
					tpdd.AverageCost = ip.ProductCost,
					QuotationPharmaceuticalDispensingDetailId = IIF(tpdd.QuotationPharmaceuticalDispensingDetailId = 0, NULL, tpdd.QuotationPharmaceuticalDispensingDetailId)
			FROM @Detail tpdd
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON tpdd.ProductId = ip.Id
			LEFT JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
			LEFT JOIN Inventory.InventorySupplie ins WITH (NOLOCK) ON ip.SupplieId = ins.Id
			WHERE tpdd.ChangeTracker <> 'Deleted'

			UPDATE tpdd
				SET tpdd.FunctionalUnitId = fu.Id
			FROM @Detail tpdd
			JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON @FunctionalUnitCode = fu.Code

			UPDATE @DetailBatchSerial 
				SET PhysicalInventoryId = IIF(PhysicalInventoryId = 0, NULL, PhysicalInventoryId),
					PhysicalInventoryCustodyId = IIF(PhysicalInventoryCustodyId = 0, NULL, PhysicalInventoryCustodyId)
			WHERE ChangeTracker <> 'Deleted'

			UPDATE tpdd
				SET tpdd.Quantity = tpddbs.Quantity
			FROM @Detail tpdd
			JOIN Inventory.Warehouse w WITH (NOLOCK) ON tpdd.WarehouseId = w.Id
			JOIN
			(
				SELECT PharmaceuticalDetailIdTmp, SUM(Quantity) Quantity
				FROM @DetailBatchSerial
				WHERE ChangeTracker <> 'Deleted'
				GROUP BY PharmaceuticalDetailIdTmp
			) tpddbs ON tpdd.IdTmp = tpddbs.PharmaceuticalDetailIdTmp
			WHERE tpdd.ChangeTracker <> 'Deleted' AND w.VirtualStore = 1

			INSERT INTO @DetailBatchSerial
			(
				PharmaceuticalDetailIdTmp, Id, PharmaceuticalDispensingDetailId, Quantity, OutstandingQuantity, ChangeTracker
			)
			SELECT tpdd.IdTmp, 0, 0, tpdd.Quantity, tpdd.Quantity, 'Added'
			FROM Inventory.WareHouse w WITH (NOLOCK)
			JOIN @Detail tpdd ON w.Id = tpdd.WarehouseId
			LEFT JOIN @DetailBatchSerial tpddbs ON tpdd.IdTmp = tpddbs.PharmaceuticalDetailIdTmp
			WHERE w.VirtualStore = 1 AND tpddbs.Id IS NULL

			INSERT INTO [Inventory].[PhysicalInventory] 
			(
				[WarehouseId],[ProductId],[BatchSerialId],[Quantity]
			)
				SELECT DISTINCT tpdd.WarehouseId,tpdd.ProductId, NULL, 0
				FROM Inventory.WareHouse w WITH (NOLOCK)
				JOIN @Detail tpdd ON w.Id = tpdd.WarehouseId
				JOIN @DetailBatchSerial tpddbs ON tpdd.IdTmp = tpddbs.PharmaceuticalDetailIdTmp
				LEFT JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON tpdd.WarehouseId = phy.WarehouseId AND tpdd.ProductId = phy.ProductId
				WHERE w.VirtualStore = 1 AND ISNULL(tpddbs.PhysicalInventoryId, 0) = 0 AND phy.Id IS NULL

			UPDATE tpddbs
				SET tpddbs.PhysicalInventoryId = phy.Id
			FROM Inventory.WareHouse w WITH (NOLOCK)
			JOIN @Detail tpdd ON w.Id = tpdd.WarehouseId
			JOIN @DetailBatchSerial tpddbs ON tpdd.IdTmp = tpddbs.PharmaceuticalDetailIdTmp
			JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON tpdd.WarehouseId = phy.WarehouseId AND tpdd.ProductId = phy.ProductId
			WHERE w.VirtualStore = 1 AND ISNULL(tpddbs.PhysicalInventoryId, 0) = 0

			/***********************************************  VALIDACIONES ***********************************************/

			IF @Extramural = 0 AND EXISTS (SELECT 1 FROM dbo.CHREGEGRE WITH (NOLOCK) WHERE NUMINGRES = @AdmissionNumber)
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = 'El paciente ya fue egresado de la institución, favor actualizar los datos'
				RETURN
			END

			IF @Extramural = 1 AND EXISTS (SELECT 1 FROM dbo.ADINGRESO WITH (NOLOCK) WHERE NUMINGRES = @AdmissionNumber AND IESTADOIN = 'A')
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = 'No se puede generar la dispensación en el ingreso actual porque se encuentra anulado'
				RETURN
			END

			--- Valido que existan detalles
			IF NOT EXISTS (SELECT 1 FROM @Detail WHERE ChangeTracker <> 'Deleted')
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = 'La dispensación no tiene detalles'
				RETURN
			END

			--Validamos que no existan detalles guardados que no esten siendo procesados
			IF EXISTS 
			(
				SELECT 1 
				FROM Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK)
				LEFT JOIN @Detail tpdd ON pdd.Id = tpdd.Id
				WHERE pdd.PharmaceuticalDispensingId = @Id AND tpdd.Id IS NULL
			)
			BEGIN
				SELECT @Message = STUFF((
					SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', ip.Code, '(Almacen: ', w.Code, ' - ', w.Name, ')')
					FROM Inventory.InventoryProduct ip WITH (NOLOCK)
					JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK) ON ip.Id = pdd.ProductId
					JOIN Inventory.Warehouse w WITH (NOLOCK) ON pdd.WarehouseId = w.Id
					LEFT JOIN @Detail tpdd ON pdd.Id = tpdd.Id
					WHERE pdd.PharmaceuticalDispensingId = @Id AND tpdd.Id IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999,
						@MessageResult = 'Los siguientes productos se encuentran guardados pero no fueron incluidos en el proceso de dispensacion: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			--Validamos que no existan dispensaciones usando el almacén en tránsito
			IF EXISTS 
			(
				SELECT 1 
				FROM @Detail tpdd
				JOIN Inventory.Warehouse w WITH (NOLOCK) ON tpdd.WarehouseId = w.Id
				WHERE tpdd.ChangeTracker <> 'Deleted' AND w.TransitStore = 1
			)
			BEGIN
				SELECT @Message = STUFF((
					SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', tpdd.CodeProduct, '(Almacen: ', w.Code, ' - ', w.Name, ')')
					FROM @Detail tpdd
					JOIN Inventory.Warehouse w WITH (NOLOCK) ON tpdd.WarehouseId = w.Id
					WHERE tpdd.ChangeTracker <> 'Deleted' AND w.TransitStore = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999,
						@MessageResult = 'Los siguientes productos no pueden ser dispensados desde un almacén de tránsito: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			--valido que los productos tengan valor en AverageCost
			IF EXISTS (SELECT 1 FROM @Detail WHERE ChangeTracker <> 'Deleted' AND AverageCost <= 0)
			BEGIN
				SELECT @Message = STUFF((
					SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', tpdd.CodeProduct)
					FROM @Detail tpdd
					WHERE tpdd.ChangeTracker <> 'Deleted' AND tpdd.AverageCost <= 0
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999,
						@MessageResult = 'Los siguientes productos no pueden ser dispensados porque tienen costo promedio igual o menor de 0: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			--Validamos que no existan detalles con cantidad en 0
			IF EXISTS (SELECT 1 FROM @Detail WHERE ChangeTracker <> 'Deleted' AND Quantity <= 0)
			BEGIN
				SELECT @Message = STUFF((
					SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', tpdd.CodeProduct, '(Almacen: ', w.Code, ' - ', w.Name, ')')
					FROM @Detail tpdd
					JOIN Inventory.Warehouse w WITH (NOLOCK) ON tpdd.WarehouseId = w.Id
					WHERE tpdd.ChangeTracker <> 'Deleted' AND tpdd.Quantity <= 0
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999,
						@MessageResult = 'Los siguientes productos no pueden ser dispensados porque no tienen cantidades validas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			--Valido que todos los detalles tengan lote asociado
			IF EXISTS
			(
				SELECT 1 
				FROM @Detail tpdd
				LEFT JOIN @DetailBatchSerial tpddbs ON tpdd.IdTmp = tpddbs.PharmaceuticalDetailIdTmp AND tpddbs.ChangeTracker <> 'Deleted'
				WHERE tpdd.ChangeTracker <> 'Deleted' AND tpddbs.Id IS NULL
			) 
			BEGIN
				SELECT @Message = STUFF((
					SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', tpdd.CodeProduct, '(Almacen: ', w.Code, ' - ', w.Name, ')')
					FROM Inventory.Warehouse w WITH (NOLOCK)
					JOIN @Detail tpdd ON w.Id = tpdd.WarehouseId
					LEFT JOIN @DetailBatchSerial tpddbs ON tpdd.IdTmp = tpddbs.PharmaceuticalDetailIdTmp AND tpddbs.ChangeTracker <> 'Deleted'
					WHERE tpdd.ChangeTracker <> 'Deleted' AND tpddbs.Id IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999,
						@MessageResult = 'Los siguientes productos no pueden ser dispensados porque no tienen detalles de lotes: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			--Validamos que no existan detalles de lotes guardados que no esten siendo procesados
			IF EXISTS 
			(
				SELECT 1 
				FROM Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK)
				JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs WITH (NOLOCK) ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
				LEFT JOIN @DetailBatchSerial tpddbs ON pddbs.Id = tpddbs.Id
				WHERE pdd.PharmaceuticalDispensingId = @Id AND tpddbs.Id IS NULL
			)
			BEGIN
				SELECT @Message = STUFF((
					SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', ip.Code, '(', IIF(bs.Id IS NULL, '', 'Lote: ' + bs.BatchCode + ' - '), 'Almacen: ', w.Code, ' - ', w.Name, ')')
					FROM Inventory.InventoryProduct ip WITH (NOLOCK)
					JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK) ON ip.Id = pdd.ProductId
					JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs WITH (NOLOCK) ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
					JOIN Inventory.Warehouse w WITH (NOLOCK) ON pdd.WarehouseId = w.Id
					LEFT JOIN @DetailBatchSerial tpddbs ON pddbs.Id = tpddbs.Id
					LEFT JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON pddbs.PhysicalInventoryId = phy.Id
					LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON phy.BatchSerialId = bs.Id
					WHERE pdd.PharmaceuticalDispensingId = @Id AND tpddbs.Id IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999,
						@MessageResult = 'Los siguientes lotes de productos se encuentran guardados pero no fueron incluidos en el proceso de dispensacion: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			--Validamos que las cantidades de los detalles coincidan con la de los lotes
			IF EXISTS 
			(
				SELECT 1 
				FROM @Detail tpdd
				LEFT JOIN
				(
					SELECT PharmaceuticalDetailIdTmp, SUM(Quantity) Quantity
					FROM @DetailBatchSerial
					WHERE ChangeTracker <> 'Deleted'
					GROUP BY PharmaceuticalDetailIdTmp
				) tpddbs ON tpdd.IdTmp = tpddbs.PharmaceuticalDetailIdTmp
				WHERE tpdd.ChangeTracker <> 'Deleted' AND tpdd.Quantity <> ISNULL(tpddbs.Quantity, 0)
			)
			BEGIN
				SELECT @Message = STUFF((
					SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', tpdd.CodeProduct, '(Almacen: ', w.Code, ' - ', w.Name, ')')
					FROM @Detail tpdd
					JOIN Inventory.Warehouse w WITH (NOLOCK) ON tpdd.WarehouseId = w.Id
					LEFT JOIN
					(
						SELECT PharmaceuticalDetailIdTmp, SUM(Quantity) Quantity
						FROM @DetailBatchSerial
						WHERE ChangeTracker <> 'Deleted'
						GROUP BY PharmaceuticalDetailIdTmp
					) tpddbs ON tpdd.IdTmp = tpddbs.PharmaceuticalDetailIdTmp
					WHERE tpdd.ChangeTracker <> 'Deleted' AND tpdd.Quantity <> ISNULL(tpddbs.Quantity, 0)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999,
						@MessageResult = 'Los siguientes productos no pueden ser dispensados porque la cantidad de los detalles no coincide con la de sus lotes: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			--Validamos que no existan detalles de lotes con cantidad en 0
			IF EXISTS (SELECT 1 FROM @DetailBatchSerial WHERE ChangeTracker <> 'Deleted' AND Quantity <= 0)
			BEGIN
				SELECT @Message = STUFF((
					SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', tpdd.CodeProduct, '(Almacen: ', w.Code, ' - ', w.Name, ')')
					FROM Inventory.Warehouse w WITH (NOLOCK)
					JOIN @Detail tpdd ON w.Id = tpdd.WarehouseId
					JOIN @DetailBatchSerial tpddbs ON tpdd.IdTmp = tpddbs.PharmaceuticalDetailIdTmp
					WHERE tpddbs.ChangeTracker <> 'Deleted' AND tpddbs.Quantity <= 0
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999,
						@MessageResult = 'Los siguientes productos no pueden ser dispensados porque los lotes no tienen cantidades validas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			-- valido si afecta o no el inventario
			IF @AffectInventory = 1 
			BEGIN
				IF EXISTS 
				(
					SELECT 1
					FROM @Detail tpdd
					JOIN @DetailBatchSerial tpddbs ON tpdd.IdTmp = tpddbs.PharmaceuticalDetailIdTmp
					JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON tpddbs.PhysicalInventoryId = phy.Id
					JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON phy.BatchSerialId = bs.Id
					WHERE tpdd.ChangeTracker <> 'Deleted' AND CAST(@DocumentDate AS DATE) > bs.ExpirationDate
				) 
				BEGIN
					SELECT @Message = STUFF((
						SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', tpdd.CodeProduct, '(Lote: ', bs.BatchCode, ' - Almacen: ', w.Code, ' - ', w.Name, ')')
						FROM @Detail tpdd
						JOIN @DetailBatchSerial tpddbs ON tpdd.IdTmp = tpddbs.PharmaceuticalDetailIdTmp
						JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON tpddbs.PhysicalInventoryId = phy.Id
						JOIN Inventory.Warehouse w WITH (NOLOCK) ON tpdd.WarehouseId = w.Id
						JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON phy.BatchSerialId = bs.Id
						WHERE tpdd.ChangeTracker <> 'Deleted' AND CAST(@DocumentDate AS DATE) > bs.ExpirationDate
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT	@CodeResult = 999,
							@MessageResult = 'Los siguientes productos no pueden ser dispensados porque se encuentran vencidos: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
					RETURN
				END
			END

			-- Valido si el producto corresponde con el inventario fisico
			IF EXISTS 
			(
				SELECT 1
				FROM @Detail tpdd
				JOIN @DetailBatchSerial tpddbs ON tpdd.IdTmp = tpddbs.PharmaceuticalDetailIdTmp
				JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON tpddbs.PhysicalInventoryId = phy.Id
				WHERE tpdd.ChangeTracker <> 'Deleted' AND tpddbs.ChangeTracker <> 'Deleted' AND tpdd.ProductId <> phy.ProductId
			) 
			BEGIN
				SELECT @Message = STUFF((
					SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', tpdd.CodeProduct)
					FROM @Detail tpdd
					JOIN @DetailBatchSerial tpddbs ON tpdd.IdTmp = tpddbs.PharmaceuticalDetailIdTmp
					JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON tpddbs.PhysicalInventoryId = phy.Id
					WHERE tpdd.ChangeTracker <> 'Deleted' AND tpddbs.ChangeTracker <> 'Deleted' AND tpdd.ProductId <> phy.ProductId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999,
						@MessageResult = 'Los siguientes productos no pueden ser dispensados porque se no corresponden con el inventario fisico seleccionado: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			-- Valido si el producto corresponde con el inventario fisico de custodia
			IF EXISTS 
			(
				SELECT 1
				FROM @Detail tpdd
				JOIN @DetailBatchSerial tpddbs ON tpdd.IdTmp = tpddbs.PharmaceuticalDetailIdTmp
				JOIN Inventory.PhysicalInventoryCustody phy WITH (NOLOCK) ON tpddbs.PhysicalInventoryCustodyId = phy.Id
				WHERE tpdd.ChangeTracker <> 'Deleted' AND tpddbs.ChangeTracker <> 'Deleted' AND tpdd.ProductId <> phy.ProductId
			) 
			BEGIN
				SELECT @Message = STUFF((
					SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', tpdd.CodeProduct)
					FROM @Detail tpdd
					JOIN @DetailBatchSerial tpddbs ON tpdd.IdTmp = tpddbs.PharmaceuticalDetailIdTmp
					JOIN Inventory.PhysicalInventoryCustody phy WITH (NOLOCK) ON tpddbs.PhysicalInventoryCustodyId = phy.Id
					WHERE tpdd.ChangeTracker <> 'Deleted' AND tpddbs.ChangeTracker <> 'Deleted' AND tpdd.ProductId <> phy.ProductId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999,
						@MessageResult = 'Los siguientes productos no pueden ser dispensados porque se no corresponden con el inventario fisico de custodia seleccionado: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			IF @AssociateCostMainAccount = 1 AND EXISTS
			(
				SELECT 1
				FROM @Detail tpdd
				JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON tpdd.ProductId = ip.Id
				JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
				LEFT JOIN Inventory.SettingInventoryFunctionalUnit sifu WITH (NOLOCK) ON @SettingInventoryId = sifu.SettingInventoryId AND tpdd.FunctionalUnitId = sifu.FunctionalUnitId
				WHERE tpdd.ChangeTracker <> 'Deleted' AND sifu.Id IS NULL
			)
			BEGIN
				SELECT @Message = STUFF((
					SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', fu.Code, ' - ', fu.Name)
					FROM @Detail tpdd
					JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON tpdd.ProductId = ip.Id
					JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
					JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON tpdd.FunctionalUnitId = fu.Id
					LEFT JOIN Inventory.SettingInventoryFunctionalUnit sifu WITH (NOLOCK) ON @SettingInventoryId = sifu.SettingInventoryId AND tpdd.FunctionalUnitId = sifu.FunctionalUnitId
					WHERE tpdd.ChangeTracker <> 'Deleted' AND sifu.Id IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999,
						@MessageResult = 'Las siguientes unidades funcionales no se encuentran en los parámetros de inventario: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END
			ELSE IF @AssociateCostMainAccount = 2 AND EXISTS
			(
				SELECT 1
				FROM @Detail tpdd
				JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON tpdd.ProductId = ip.Id
				JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
				LEFT JOIN Inventory.ProductGroupFunctionalUnit pgfu WITH (NOLOCK) ON pg.Id = pgfu.ProductGroupId AND tpdd.FunctionalUnitId = pgfu.FunctionalUnitId
				WHERE tpdd.ChangeTracker <> 'Deleted' AND pgfu.Id IS NULL
			)
			BEGIN
				SELECT @Message = STUFF((
					SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', fu.Code, ' - ', fu.Name, ' (', pg.Code, ' - ', pg.Name, ')')
					FROM @Detail tpdd
					JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON tpdd.ProductId = ip.Id
					JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
					JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON tpdd.FunctionalUnitId = fu.Id
					LEFT JOIN Inventory.ProductGroupFunctionalUnit pgfu WITH (NOLOCK) ON pg.Id = pgfu.ProductGroupId AND tpdd.FunctionalUnitId = pgfu.FunctionalUnitId
					WHERE tpdd.ChangeTracker <> 'Deleted' AND pgfu.Id IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999,
						@MessageResult = 'Las siguientes unidades funcionales no se encuentran en los parámetros de grupo de producto: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			-- Si se realiza las modificacion en el modulo del EHR
			IF @FunctionalUnitCode IS NOT NULL AND @ConsecutivePharmacy IS NOT NULL AND @CareCenterCode IS NOT NULL
			BEGIN
				-- Validar que los productos dispensados hayan sido solicitados
				IF ISNULL(@EntityName, '') <> 'PharmaceuticalDispensingTransfer' AND EXISTS 
				(
					SELECT 1 
					FROM @Detail tpdd
					LEFT JOIN dbo.HCFARMEPD fd WITH (NOLOCK) ON @ConsecutiveCrystal = fd.CODCONCEC AND tpdd.CodeProduct = fd.CODPRODUC
					WHERE tpdd.ChangeTracker <> 'Deleted' AND fd.CODCONCEC IS NULL
				)
				BEGIN
					SELECT @Message = STUFF((
						SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', tpdd.CodeProduct)
						FROM @Detail tpdd
						LEFT JOIN dbo.HCFARMEPD fd WITH (NOLOCK) ON @ConsecutiveCrystal = fd.CODCONCEC AND tpdd.CodeProduct = fd.CODPRODUC
						WHERE tpdd.ChangeTracker <> 'Deleted' AND fd.CODCONCEC IS NULL
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT	@CodeResult = 999,
							@MessageResult = 'Los siguientes productos no fueron solicitados: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
					RETURN
				END

				-- Validar que la cantidad de los productos dispensados coincida con la solicitada
				IF EXISTS 
				(
					SELECT 1 
					FROM 
					(
						SELECT tpdd.CodeProduct, SUM(tpdd.Quantity) DispensingQuantity
						FROM @Detail tpdd
						WHERE tpdd.ChangeTracker <> 'Deleted'
						GROUP BY tpdd.CodeProduct
					) tpdd
					JOIN 
					(
						SELECT tpdd.CodeProduct, SUM(fd.CANPENPRO) PendingQuantity
						FROM @Detail tpdd
						JOIN dbo.HCFARMEPD fd WITH (NOLOCK) ON @ConsecutiveCrystal = fd.CODCONCEC AND tpdd.CodeProduct = fd.CODPRODUC
						WHERE tpdd.ChangeTracker <> 'Deleted'
						GROUP BY tpdd.CodeProduct
					) fd ON tpdd.CodeProduct = fd.CodeProduct
					WHERE tpdd.DispensingQuantity > fd.PendingQuantity
				)
				BEGIN
					SELECT @Message = STUFF((
						SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', tpdd.CodeProduct)
						FROM 
						(
							SELECT tpdd.CodeProduct, SUM(tpdd.Quantity) DispensingQuantity
							FROM @Detail tpdd
							WHERE tpdd.ChangeTracker <> 'Deleted'
							GROUP BY tpdd.CodeProduct
						) tpdd
						JOIN 
						(
							SELECT tpdd.CodeProduct, SUM(fd.CANPENPRO) PendingQuantity
							FROM @Detail tpdd
							JOIN dbo.HCFARMEPD fd WITH (NOLOCK) ON @ConsecutiveCrystal = fd.CODCONCEC AND tpdd.CodeProduct = fd.CODPRODUC
							WHERE tpdd.ChangeTracker <> 'Deleted'
							GROUP BY tpdd.CodeProduct
						) fd ON tpdd.CodeProduct = fd.CodeProduct
						WHERE tpdd.DispensingQuantity > fd.PendingQuantity
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT	@CodeResult = 999,
							@MessageResult = 'La cantidad pendiente por entregar de los siguientes productos es menor a la cantidad a dispensar: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
					RETURN
				END
			END

			/************************************  INSERTAR / ACTUALIZAR CABECERA ************************************/

			DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @UserCode ELSE NULL END
			DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

			IF @Id = 0
			BEGIN
				--Si se esta insertando por primera vez se consulta la secuencia numerica
				DECLARE @IsManual BIT
				
				EXEC Common.SP_GetSequence 190, @IdForm, @OperatingUnitId, @Prefix, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

				IF @Code_Output <> 0
				BEGIN
					SELECT	@CodeResult = 999, 
							@MessageResult = REPLACE(@Message_Output, '{0}', 'Dispensación Farmacéutica')
					RETURN
				END

				--Se inserta la cabecera
				INSERT INTO [Inventory].[PharmaceuticalDispensing]
				(
					[Code],[OperatingUnitId],[AdmissionNumber],[DocumentDate],[AffectInventory],[EntityName],[EntityCode],[EntityId],
					[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate]
				)
				SELECT	@Code,@OperatingUnitId,@AdmissionNumber,@DocumentDate,@AffectInventory,@EntityName,@EntityCode,@EntityId,
						@Status,@UserCode,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Inventory].[PharmaceuticalDispensing]
					SET [Code] = @Code,
						[AdmissionNumber] = @AdmissionNumber,
						[DocumentDate] = @DocumentDate,
						[AffectInventory] = @AffectInventory,
						[Status] = @Status,
						[ModificationUser] = @UserCode,
						[ModificationDate] = [Common].[GETDATE](),
						[ConfirmationUser] = @ConfirmationUser,
						[ConfirmationDate] = @ConfirmationDate
				WHERE Id = @Id
			END

			/******************************* INSERTAR / ACTUALIZAR / ELIMINAR DETALLES *******************************/

			SET @Rows = 1
			SET @RowId = 0

			WHILE @Rows > 0
			BEGIN
				SELECT TOP 1 
					@RowId = IdTmp 
				FROM @Detail tpdd
				WHERE tpdd.IdTmp > @RowId 
					AND tpdd.ChangeTracker = 'Added'
				ORDER BY tpdd.IdTmp

				SET @Rows = @@ROWCOUNT
				IF @Rows = 0 
					BREAK

				--- Inserto los detalles de la dispensacion
				INSERT INTO [Inventory].[PharmaceuticalDispensingDetail]
				(
					[PharmaceuticalDispensingId],[CareGroupId],[HealthAdministratorId],[ThirdPartyId],[ProductId],[WarehouseId],[Quantity],[ReturnedQuantity],
					[ServiceDate],[FunctionalUnitId],[OrderedHealthProfessionalCode],[OrderedProfessionalSpecialty],[OrderedHealthProfessionalThirdPartyId],
					[AuthorizationNumber],[LiquidationType],[CupsEntityId],[SurchargeApply],[SalePrice],[AverageCost],[DiscountPercentage],[DiscountValue],
					[TotalSalesPrice],[GrandTotalSalesPrice],[QuotationPharmaceuticalDispensingDetailId],[EntityId],[EntityName]
				)
					SELECT	@Id, CareGroupId, HealthAdministratorId, ThirdPartyId, ProductId, WarehouseId, Quantity, 0, 
							ServiceDate, FunctionalUnitId, OrderedHealthProfessionalCode, OrderedProfessionalSpecialty, OrderedHealthProfessionalThirdPartyId,
							AuthorizationNumber, LiquidationType, null, SurchargeApply, SalePrice, AverageCost, DiscountPercentage, DiscountValue, 
							TotalSalesPrice, GrandTotalSalesPrice, QuotationPharmaceuticalDispensingDetailId, EntityId, EntityName
					FROM @Detail 
					WHERE IdTmp = @RowId
				
				SET @PharmaceuticalDispensingDetailId = SCOPE_IDENTITY()

				-- Actualizo el Id del detalle de la dispensación
				UPDATE @Detail 
					SET Id = @PharmaceuticalDispensingDetailId 
				WHERE IdTmp = @RowId

				-- Actualizo el Id del detalle de la dispensación en los subdetalles
				UPDATE @DetailBatchSerial 
					SET PharmaceuticalDispensingDetailId = @PharmaceuticalDispensingDetailId 
				WHERE PharmaceuticalDetailIdTmp = @RowId
			END

			--Inserto los detalles de lotes
			INSERT INTO [Inventory].[PharmaceuticalDispensingDetailBatchSerial]
			(
				[PharmaceuticalDispensingDetailId],[PhysicalInventoryId],[Quantity],[OutstandingQuantity],PhysicalInventoryCustodyId
			)
				SELECT PharmaceuticalDispensingDetailId, PhysicalInventoryId, Quantity, OutstandingQuantity, PhysicalInventoryCustodyId
				FROM @DetailBatchSerial 
				WHERE ChangeTracker = 'Added'

			-- Actualizo los detalles de la dispensacion 
			UPDATE pdd
				SET pdd.CareGroupId = tpdd.CareGroupId,
					pdd.HealthAdministratorId = tpdd.HealthAdministratorId,
					pdd.ThirdPartyId = tpdd.ThirdPartyId,
					pdd.ProductId = tpdd.ProductId,
					pdd.WarehouseId = tpdd.WarehouseId,
					pdd.Quantity = tpdd.Quantity,
					pdd.ServiceDate = tpdd.ServiceDate,
					pdd.FunctionalUnitId = tpdd.FunctionalUnitId,
					pdd.OrderedHealthProfessionalCode = tpdd.OrderedHealthProfessionalCode,
					pdd.OrderedProfessionalSpecialty = tpdd.OrderedProfessionalSpecialty,
					pdd.OrderedHealthProfessionalThirdPartyId = tpdd.OrderedHealthProfessionalThirdPartyId,
					pdd.AuthorizationNumber = tpdd.AuthorizationNumber,
					pdd.LiquidationType = tpdd.LiquidationType,
					pdd.SurchargeApply = tpdd.SurchargeApply,
					pdd.SalePrice = tpdd.SalePrice,
					pdd.AverageCost = tpdd.AverageCost,
					pdd.DiscountPercentage = tpdd.DiscountPercentage,
					pdd.DiscountValue = tpdd.DiscountValue,
					pdd.TotalSalesPrice = tpdd.TotalSalesPrice,
					pdd.GrandTotalSalesPrice = tpdd.GrandTotalSalesPrice,
					pdd.QuotationPharmaceuticalDispensingDetailId = tpdd.QuotationPharmaceuticalDispensingDetailId,
					pdd.EntityId = tpdd.EntityId,
					pdd.EntityName = tpdd.EntityName
			FROM @Detail tpdd
			JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK) ON tpdd.Id = pdd.Id
			WHERE pdd.PharmaceuticalDispensingId = @Id AND tpdd.ChangeTracker = 'Modified'

			-- Actualizo los detalles de los lotes
			UPDATE pddbs
				SET pddbs.PhysicalInventoryId = tpddbs.PhysicalInventoryId,
					pddbs.Quantity = tpddbs.Quantity,
					pddbs.OutstandingQuantity = tpddbs.OutstandingQuantity,
					pddbs.PhysicalInventoryCustodyId = tpddbs.PhysicalInventoryCustodyId
			FROM @DetailBatchSerial tpddbs
			JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs WITH (NOLOCK) ON tpddbs.Id = pddbs.Id
			JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK) ON pddbs.PharmaceuticalDispensingDetailId = pdd.Id
			WHERE pdd.PharmaceuticalDispensingId = @Id AND tpddbs.ChangeTracker = 'Modified'

			--eliminamos los detalles de lotes
			DELETE pddbs
			FROM Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK)
			JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs WITH (NOLOCK) ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
			JOIN @DetailBatchSerial tpddbs ON pddbs.Id = tpddbs.Id
			WHERE pdd.PharmaceuticalDispensingId = @Id AND tpddbs.ChangeTracker = 'Deleted'

			--eliminamos los detalles
			DELETE pdd
			FROM Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK)
			JOIN @Detail tpdd ON pdd.Id = tpdd.Id
			WHERE pdd.PharmaceuticalDispensingId = @Id AND tpdd.ChangeTracker = 'Deleted'

			/*********************************************  CONFIRMACIÓN *********************************************/

			IF @Status = 2
			BEGIN			

				/***************************************** ORDEN DE SERVICIO *****************************************/

				IF EXISTS 
				(
					SELECT 1 
					FROM Inventory.Warehouse w WITH (NOLOCK)
					JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK) ON w.Id = pdd.WarehouseId
					WHERE pdd.PharmaceuticalDispensingId = @Id AND w.CustodyStore = 0
				)
				BEGIN
					--Generamos el XML para consumir el SP encargado de generar la orden de servicio
					SELECT @SubXml = CONVERT
					(
						XML, 
						(
							SELECT *
							FROM 
							(
								SELECT	pd.OperatingUnitId,
										0 Id, 
										'' Code, 
										pd.AdmissionNumber, 
										@PatientCode PatientCode, 
										pd.DocumentDate OrderDate, 
										pd.AffectInventory, 										
										1 Status, 
										pd.Id EntityId, 
										pd.Code EntityCode, 
										'PharmaceuticalDispensing' EntityName
								FROM Inventory.PharmaceuticalDispensing pd WITH (NOLOCK)
								WHERE pd.Id = @Id
							) ServiceOrder
							JOIN 
							( 
								SELECT	0 Id, 
										0 ServiceOrderId, 
										pdd.CareGroupId,
										pdd.HealthAdministratorId,
										pdd.ThirdPartyId,
										0 ServiceType,
										2 RecordType,
										0 CUPSAssociateService,
										0 IsPackage,
										0 Packaging,
										5 LiquidationType,
										pdd.ProductId,
										pdd.Quantity InvoicedQuantity,
										pdd.Quantity SupplyQuantity,
										0 DevolutionQuantity,
										pdd.SalePrice RateManualSalePrice,
										pdd.AverageCost CostValue,
										pdd.ServiceDate,
										pdd.AuthorizationNumber,
										pdd.FunctionalUnitId PerformsFunctionalUnitId,
										pdd.OrderedHealthProfessionalCode PerformsHealthProfessionalCode,
										pdd.OrderedProfessionalSpecialty PerformsProfessionalSpecialty,
										pdd.OrderedHealthProfessionalThirdPartyId PerformsHealthProfessionalThirdPartyId,
										CASE @AssociateCostCenter 
											WHEN 1 THEN fu.CostCenterId
											WHEN 2 THEN pg.CostCenterId
											WHEN 3 THEN w.CostCenterId
										END CostCenterId,
										1 SettlementType,
										pdd.SalePrice SubTotalSalesPrice,
										pdd.DiscountValue ThirdPartyDiscount, 
										pdd.DiscountPercentage ThirdPartyDiscountPercentage, 
										pdd.TotalSalesPrice, 
										pdd.GrandTotalSalesPrice, 
										pdd.SurchargeApply, 
										0 SurgeryNumber, 
										1 IsFirstEvent, 
										0 IsAnnulled,
										0 IsDelete, 
										pg.IncomeAccountId IncomeMainAccountId,
										pdd.GrossValue,
										pdd.TaxValue
								FROM Inventory.Warehouse w WITH (NOLOCK)
								JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK) ON w.Id = pdd.WarehouseId
								JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON pdd.ProductId = ip.Id
								JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
								JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON pdd.FunctionalUnitId = fu.Id
								WHERE pdd.PharmaceuticalDispensingId = @Id AND w.CustodyStore = 0
							) ServiceOrderDetail ON ServiceOrderDetail.ServiceOrderId = ServiceOrder.Id
							FOR XML AUTO,TYPE, ELEMENTS
						)
					)

					IF @SubXml IS NOT NULL
					BEGIN
						EXEC Billing.SP_GenerateServiceOrder_Output @SubXml, @UserCode, @Code_Output OUT, @Message_Output OUT, NULL, NULL

						IF @Code_Output <> 0
						BEGIN
							SELECT @CodeResult = 999, 
									@MessageResult = ISNULL(@Message_Output, 'No se puedo generar la orden de servicio.')
							RETURN
						END

						SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
					END
				END

				/***************************************** AFECTA INVENTARIO *****************************************/

				--- Ahora Afecto el Inventario, Kardex y Contabilidad
				IF @AffectInventory = 1 
				BEGIN
					/********************************************  KARDEX ********************************************/

					--Generamos el XML para consumir el SP encargado del movimiento del kardex
					SELECT @SubXml = CONVERT
					(
						XML, 
						(
							SELECT Kardex.*
							FROM 
							( 
								SELECT	@DocumentDate DocumentDate,
										2 MovementType,
										pdd.WarehouseId,
										pdd.ProductId,
										phy.BatchSerialId,
										pddbs.Quantity,
										pdd.AverageCost Value,
										0 AffectAverageCost
								FROM Inventory.Warehouse w WITH (NOLOCK)
								JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK) ON w.Id = pdd.WarehouseId
								JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs WITH (NOLOCK) ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
								JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON pddbs.PhysicalInventoryId = phy.Id
								WHERE pdd.PharmaceuticalDispensingId = @Id AND w.VirtualStore = 0 AND w.CustodyStore = 0
							) Kardex
							FOR XML AUTO,TYPE, ELEMENTS
						)
					)

					IF @SubXml IS NOT NULL
					BEGIN
						EXEC Inventory.SP_SavePhysicalInventoryKardex_Output @SubXml, @Id, @Code, 'PharmaceuticalDispensing', @UserCode, 1, @Code_Output OUT, @Message_Output OUT

						IF @Code_Output <> 0
						BEGIN
							SELECT @CodeResult = 999, 
									@MessageResult = ISNULL(@Message_Output, 'No se puedo afectar el kardex.')
							RETURN
						END

						SET @Message_Output = 'Se realizó la afectación del kardex'
						SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
					END

					--Generamos el XML para consumir el SP encargado del movimiento del kardex de custodia
					SELECT @SubXml = CONVERT
					(
						XML, 
						(
							SELECT Kardex.*
							FROM 
							( 
								SELECT	@DocumentDate DocumentDate,
										2 MovementType,
										pdd.WarehouseId,
										pdd.ProductId,
										pddbs.Quantity,
										pdd.AverageCost Value,
										0 AffectAverageCost
								FROM Inventory.Warehouse w WITH (NOLOCK)
								JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK) ON w.Id = pdd.WarehouseId
								JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs WITH (NOLOCK) ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
								JOIN Inventory.PhysicalInventoryCustody phy WITH (NOLOCK) ON pddbs.PhysicalInventoryCustodyId = phy.Id
								WHERE pdd.PharmaceuticalDispensingId = @Id AND w.CustodyStore = 1
							) Kardex
							FOR XML AUTO,TYPE, ELEMENTS
						)
					)

					IF @SubXml IS NOT NULL
					BEGIN
						EXEC Inventory.SP_SavePhysicalInventoryCustodyKardexCustody @SubXml, @AdmissionNumber, @Id, @Code, 'PharmaceuticalDispensing', @UserCode

						IF @Code_Output <> 0
						BEGIN
							SELECT @CodeResult = 999, 
									@MessageResult = ISNULL(@Message_Output, 'No se puedo afectar el kardex de custodia.')
							RETURN
						END

						SET @Message_Output = 'Se realizó la afectación del kardex de custodia'
						SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
					END

					--Generamos el XML para consumir el SP encargado del movimiento de inventario en consignacion
					SELECT @SubXml = CONVERT
					(
						XML, 
						(
							SELECT Remission.*
							FROM 
							( 
								SELECT	pdd.Id EntityDetailId,
										@OperatingUnitId AS OperatingUnitId,
										pdd.FunctionalUnitId,
										2 MovementType,
										pdd.WarehouseId,
										pdd.ProductId,
										phy.BatchSerialId,
										pddbs.Quantity,
										pdd.AverageCost Value,
										0 AffectAverageCost
								FROM Inventory.Warehouse w WITH (NOLOCK)
								JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK) ON w.Id = pdd.WarehouseId
								JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs WITH (NOLOCK) ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
								JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON pddbs.PhysicalInventoryId = phy.Id
								WHERE pdd.PharmaceuticalDispensingId = @Id AND w.WarehouseConsignment = 1
							) Remission
							FOR XML AUTO,TYPE, ELEMENTS
						)
					)

					IF @SubXml IS NOT NULL
					BEGIN
						EXEC Inventory.SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission @SubXml, @Id, @Code, 'PharmaceuticalDispensing', @UserCode, @Message_Output OUTPUT

						IF @Code_Output <> 0
						BEGIN
							SELECT @CodeResult = 999, 
									@MessageResult = ISNULL(@Message_Output, 'No se puedo afectar el kardex de custodia.')
							RETURN
						END

						SET @Message_Output = 'Se actualizaron las cantidades usadas de inventario en consignacion'
						SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
					END

					/*************************************  COMPROBANTE CONTABLE *************************************/

					IF EXISTS 
					(
						SELECT 1 
						FROM Inventory.Warehouse w WITH (NOLOCK)
						JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK) ON w.Id = pdd.WarehouseId
						WHERE pdd.PharmaceuticalDispensingId = @Id AND w.VirtualStore = 0 AND w.CustodyStore = 0 AND w.ControlStore = 0
					)
					BEGIN
						SET @SubXml = CONVERT
						(
							XML, 
							(
								SELECT *
								FROM 
								(
									SELECT	0 Id,
											0 Consecutive,
											si.SalesJournalVoucherTypeId IdJournalVoucher,
											pd.DocumentDate VoucherDate, 
											'False' Imported,
											2 Status,
											CONCAT('Comprobante generado por la dispensacion', @Code) Detail, 
											'PharmaceuticalDispensing' EntityName,
											@Code EntityCode,
											@Id EntityId,
											0 IsClosedYear
									FROM Inventory.PharmaceuticalDispensing pd WITH (NOLOCK)
									JOIN Inventory.SettingInventory si WITH (NOLOCK) ON pd.OperatingUnitId = si.OperatingUnitId
									WHERE pd.Id = @Id
								) JournalVoucher
								JOIN
								(
									/************************************  DEBITO ************************************/
										SELECT	0 Id,
												0 IdAccounting,
												ma.Id IdMainAccount,
												IIF(ma.HandlesThirdParty = 1, IIF(@PharmaceuticalDispensingGetThirdParty = 1, @PatientThirdPartyId, @PharmaceuticalDispensingThirdParty), NULL) IdThirdParty, 
												IIF(ma.HandlesCostCenter = 1, CASE @AssociateCostCenter 
													WHEN 1 THEN fu.CostCenterId
													WHEN 2 THEN pg.CostCenterId
													WHEN 3 THEN w.CostCenterId
												END, NULL) IdCostCenter,
												(pdd.Quantity * pdd.AverageCost) DebitValue, 
												0 CreditValue
										FROM Inventory.Warehouse w WITH (NOLOCK)
										JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK) ON w.Id = pdd.WarehouseId										
										JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON pdd.ProductId = ip.Id
										JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
										JOIN Inventory.SettingInventoryFunctionalUnit sifu WITH (NOLOCK) ON @SettingInventoryId = sifu.SettingInventoryId AND pdd.FunctionalUnitId = sifu.FunctionalUnitId
										JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON sifu.CostAccountId = ma.Id
										JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON pdd.FunctionalUnitId = fu.Id
										WHERE @AssociateCostMainAccount = 1 AND pdd.PharmaceuticalDispensingId = @Id AND w.VirtualStore = 0 AND w.CustodyStore = 0 AND w.ControlStore = 0
									UNION ALL
										SELECT	0 Id,
												0 IdAccounting,
												ma.Id IdMainAccount,
												IIF(ma.HandlesThirdParty = 1, IIF(@PharmaceuticalDispensingGetThirdParty = 1, @PatientThirdPartyId, @PharmaceuticalDispensingThirdParty), NULL) IdThirdParty, 
												IIF(ma.HandlesCostCenter = 1, CASE @AssociateCostCenter 
													WHEN 1 THEN fu.CostCenterId
													WHEN 2 THEN pg.CostCenterId
													WHEN 3 THEN w.CostCenterId
												END, NULL) IdCostCenter,
												(pdd.Quantity * pdd.AverageCost) DebitValue, 
												0 CreditValue
										FROM Inventory.Warehouse w WITH (NOLOCK)
										JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK) ON w.Id = pdd.WarehouseId										
										JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON pdd.ProductId = ip.Id
										JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
										JOIN Inventory.ProductGroupFunctionalUnit pgfu WITH (NOLOCK) ON pg.Id = pgfu.ProductGroupId AND pdd.FunctionalUnitId = pgfu.FunctionalUnitId
										JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON pgfu.CostAccountId = ma.Id
										JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON pdd.FunctionalUnitId = fu.Id
										WHERE @AssociateCostMainAccount = 2 AND pdd.PharmaceuticalDispensingId = @Id AND w.VirtualStore = 0 AND w.CustodyStore = 0 AND w.ControlStore = 0
									/************************************ CREDITO ************************************/
									UNION ALL
										SELECT	0 Id,
												0 IdAccounting,
												ma.Id IdMainAccount,
												IIF(ma.HandlesThirdParty = 1, IIF(@PharmaceuticalDispensingGetThirdParty = 1, @PatientThirdPartyId, @PharmaceuticalDispensingThirdParty), NULL) IdThirdParty, 
												IIF(ma.HandlesCostCenter = 1, CASE @AssociateCostCenter 
													WHEN 1 THEN fu.CostCenterId
													WHEN 2 THEN pg.CostCenterId
													WHEN 3 THEN w.CostCenterId
												END, NULL) IdCostCenter,
												0 DebitValue, 
												(pdd.Quantity * pdd.AverageCost) CreditValue
										FROM Inventory.Warehouse w WITH (NOLOCK)
										JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK) ON w.Id = pdd.WarehouseId										
										JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON pdd.ProductId = ip.Id
										JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
										JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON pg.InventoryAccountPayableConceptId = apc.Id
										JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON IIF(w.WarehouseConsignment = 1, pg.CounterpartCostConsignedInventoryId, apc.IdAccount) = ma.Id
										JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON pdd.FunctionalUnitId = fu.Id
										WHERE pdd.PharmaceuticalDispensingId = @Id AND w.VirtualStore = 0 AND w.CustodyStore = 0 AND w.ControlStore = 0
								) JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting
								For xml AUTO,TYPE, ELEMENTS
							)
						)

						--Se consume el sp que guarda el movimiento contable
						insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @SubXml,@UserCode 
						select 
										@Code_Output = rjv.code, 
										@Message_Output = rjv.MessageResult, 
										@JournalVoucherId = rjv.IdJournalVoucher
						from @resultJournalVoucher rjv
					
						IF @Code_Output <> 0
						BEGIN
							SELECT	@CodeResult = 999, 
									@MessageResult = 'Ocurrieron errores al intentar Generar el comprobante contable: ' + ISNULL(@Message_Output, 'No se pudo generar el comprobante contable')
							RETURN
						END

						SELECT @Message_Output = CONCAT('Se generó el Comprobante contable de tipo ', jvt.Code, ' - ', jvt.Name)
						FROM Inventory.PharmaceuticalDispensing pd
						JOIN Inventory.SettingInventory si ON pd.OperatingUnitId = si.OperatingUnitId
						JOIN GeneralLedger.JournalVoucherTypes jvt On si.SalesJournalVoucherTypeId = jvt.Id
						Where pd.Id = @Id

						SELECT @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
					END
				END

				/************************************************ EHR ************************************************/

				IF @Extramural = 1 AND EXISTS (SELECT 1 FROM dbo.ADINGRESO WITH (NOLOCK) WHERE NUMINGRES = @AdmissionNumber AND IESTADOIN = 'F')
				BEGIN
					UPDATE dbo.ADINGRESO
						SET IESTADOIN = 'P'
					WHERE NUMINGRES = @AdmissionNumber
				END

				IF @QuantityStay > 0
				BEGIN
					UPDATE dbo.HCFARMEPC SET UFUCODIGO = @FunctionalUnitCode WHERE CODCONCEC = @ConsecutivePharmacy
					UPDATE dbo.HCFARMEPD SET UFUCODIGO = @FunctionalUnitCode WHERE CODCONCEC = @ConsecutivePharmacy
				END

				--Valido si el registro es de tipo domiciliaria de la pestaña de quimioterapia
				IF ISNULL(@IDHCORDPRON, 0) > 0
				BEGIN
					--Se obtiene el id del ciclo					
					SELECT TOP 1 
						@IDHCORDCICLOS = Id, 
						@IDHCORDQUIMIO = IDHCORDQUIMIO, 
						@CICLO = CICLO 
					FROM EHR.HCORDCICLOS WITH (NOLOCK)
					WHERE IDHCORDPRON = @IDHCORDPRON

					--Se valida si el ciclo es 100% domiciliario
					IF 
					(
						(SELECT COUNT(1) FROM EHR.HCORDCICLOSD WITH (NOLOCK) WHERE IDHCORDCICLOS = @IDHCORDCICLOS) 
						= 
						(SELECT COUNT(1) FROM EHR.HCORDCICLOSD WITH (NOLOCK) WHERE IDHCORDCICLOS = @IDHCORDCICLOS AND ADMISTRADIACASA = 1)
					)
					BEGIN
						--Se actualiza el estado de la cabacera
						UPDATE EHR.HCORDCICLOS SET ESTADO = 3 WHERE Id = @IDHCORDCICLOS

						--Se valida si es el último ciclo
						IF @CICLO = (SELECT CICLOS FROM EHR.HCORDQUIMIO WITH (NOLOCK) WHERE ID = @IDHCORDQUIMIO)
						BEGIN
							UPDATE EHR.HCORDQUIMIO SET ESTADO = 3 WHERE ID = @IDHCORDQUIMIO
						END
					END

					--Se valida si esta en estado 1 para poder actualizar
					IF EXISTS (SELECT 1 FROM dbo.HCORDPRON WITH (NOLOCK) WHERE AUTO = @IDHCORDPRON AND ESTSERIPS = '1')
					BEGIN
						--Se actualiza el estado
						update .HCORDPRON set ESTSERIPS = '2' where AUTO = @IDHCORDPRON

						--Se realiza un registro en la tabla HCCUMPLITRATAESPECIAL
						INSERT INTO dbo.HCCUMPLITRATAESPECIAL
						(
							IPCODPACI, IDHCORDPRON, NUMINGRES, CODCENATE, UFUCODIGO, CODPROSAL, CODESPECI, FECHAREGISTRO
						)
							SELECT TOP 1 
								@PatientCode, @IDHCORDPRON, @AdmissionNumber, @CareCenterCode, @FunctionalUnitCode, ISNULL(OrderedHealthProfessionalCode, ''), ISNULL(OrderedProfessionalSpecialty, ''), [Common].[GETDATE]()
							FROM Inventory.PharmaceuticalDispensingDetail WITH (NOLOCK)
							WHERE PharmaceuticalDispensingId = @Id
					END
				END

				--si viene para guardar en gasto de paquetes QX procedemos a realizar la insercion
				IF @GuardaGastoQX = 1 
				BEGIN
					IF @HistoryType = 'ENFERMER1' 
					BEGIN
						SET @HistoryTypeCrystal = 2
						SET @HistoryTypeNameCrystal = 'Despacho Farmacia Paquete Quirúrgico, Origen Solicitud: Despacho de Farmacia - Solicitud de Enfermeria - Usuario: ' + @UserCode
					END
					ELSE IF @HistoryType = 'CODIGOAZU' 
					BEGIN
						set @HistoryTypeCrystal = 3
						SET @HistoryTypeNameCrystal = 'Despacho Farmacia Paquete Quirúrgico, Origen Solicitud: Despacho de Farmacia - Solicitud de Emergencia - Usuario: ' + @UserCode
					END
					ELSE 
					BEGIN
						SET @HistoryTypeCrystal = 1
						IF ISNULL(@EntityName, '') = 'PharmaceuticalDispensingTransfer'
						BEGIN
							SET @HistoryTypeNameCrystal = CONCAT('Traslado Dispensación por Ingreso ', @EntityCode, ': Ingreso Origen ', @AdmissionNumberOrigin, ' - Ingreso Destino ', @AdmissionNumber, ' - Usuario: ', @UserCode)
						END
						ELSE
						BEGIN
							SET @HistoryTypeNameCrystal = 'Despacho Farmacia Paquete Quirúrgico, Origen Solicitud: Despacho de Farmacia - Solicitud del Medico - Usuario: ' + @UserCode
						END
					END

					SELECT @IdCabeceraHojaGastoQX = MIN(ID) 
					FROM dbo.HCHOJAGASTOQX WITH (NOLOCK)
					WHERE IDAGEPROGQX = @IdProgramacionQXPrincipal AND NUMINGRES = @AdmissionNumber

					--si hoja de gasto no existe, creamos cabecera
					IF ISNULL(@IdCabeceraHojaGastoQX, 0) = 0
					BEGIN
						UPDATE dbo.INCONSECU SET @ConsecutiveHojaGastoQX = CONNUMACT += 1 WHERE IDCONSECU = '00000031' 
													
						INSERT INTO dbo.HCHOJAGASTOQX ([CONSECUTIVO],[IPCODPACI],[NUMINGRES],[IDAGEPROGQX],[FECHAREGISTRO],[ESTADO])
							VALUES (@ConsecutiveHojaGastoQX,@PatientCode,@AdmissionNumber,@IdProgramacionQXPrincipal,@DocumentDate,1)

						SET @IdCabeceraHojaGastoQX = SCOPE_IDENTITY()
					END 

					UPDATE Inventory.PharmaceuticalDispensing  
						SET SurgeryExpenseSheetId = @IdCabeceraHojaGastoQX,
							EntityId = ISNULL(EntityId, @IdCabeceraHojaGastoQX)
					WHERE Id = @Id

					SET @Rows = 1
					SET @RowId = 0

					WHILE @Rows > 0
					BEGIN
						SELECT TOP 1 
							@RowId = pdd.Id
						FROM Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK)
						WHERE pdd.PharmaceuticalDispensingId = @Id
							AND pdd.Id > @RowId
						ORDER BY pdd.Id

						SET @Rows = @@ROWCOUNT
						IF @Rows = 0 
							BREAK

						--si el producto ya esta en la hoja de gasto procedemos a actualziar cantidades
						UPDATE hgqx
							SET hgqx.CANTIDADENTREGADA = hgqx.CANTIDADENTREGADA + pdd.Quantity,
								hgqx.CANTIDADDEVOLVER = hgqx.CANTIDADDEVOLVER + pdd.Quantity
						FROM Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK)
						JOIN dbo.HCHOJAGASTOQXD hgqx WITH (NOLOCK) ON @IdCabeceraHojaGastoQX = hgqx.IDHCHOJAGASTOQX AND pdd.ProductId = hgqx.IDPRODUCTO
						WHERE pdd.Id = @RowId

						--insertamos las dispenciones nuevas en el detalle de la hoja de gasto
						INSERT INTO dbo.HCHOJAGASTOQXD
						(
							[IDHCHOJAGASTOQX],[CODPRODUC],[IDPRODUCTO],[CANTIDADENTREGADA],[CANTIDADGASTADA],[CANTIDADDEVOLVER],[CANTIDADACEPTADADEV],[ORIGENSOLICITUD],[FECHAREGISTRO]
						)
						SELECT @IdCabeceraHojaGastoQX,COALESCE(atc.Code, ins.Code, ip.Code),pdd.ProductId,pdd.Quantity,0,pdd.Quantity,0,1,@DocumentDate
						FROM Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK)
						JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON pdd.ProductId = ip.Id
						LEFT JOIN dbo.HCHOJAGASTOQXD hgqx WITH (NOLOCK) ON @IdCabeceraHojaGastoQX = hgqx.IDHCHOJAGASTOQX AND ip.Id = hgqx.IDPRODUCTO
						LEFT JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
						LEFT JOIN Inventory.InventorySupplie ins WITH (NOLOCK) ON ip.SupplieId = ins.Id
						WHERE pdd.Id = @RowId AND hgqx.Id IS NULL

						UPDATE dbo.INCONSECU SET @ConsecutiveKardexDispesing = CONNUMACT += 1 WHERE IDCONSECU = '00000011'

						--- Inserto en el Kardex de crystal
						INSERT INTO [dbo].[HCKARDPAC]
						(
							[NUMCONSEC],[IPCODPACI],[NUMINGRES],[CODCENATE],[UFUCODIGO],[CODPROSAL]
							,[CODPRODUC],[CANPRODUCT],[TIPREGIST],[HCPRESCRN],[HCSOLINSN],[HCCTRAPLN]
							,[HCCTRAPLM],[CODDOCUME],[FECREGKAR],[TIPORIREG],[DESMOVPRO],[JUSANULAC]
							,[CONSECFAR],[FECHAUTIL],[OBSERVACI]
						)
						SELECT	@ConsecutiveKardexDispesing - 1,@PatientCode,@AdmissionNumber,@CareCenterCode,@FunctionalUnitCode,OrderedHealthProfessionalCode
								,COALESCE(atc.Code, ins.Code, ip.Code),Quantity,'1',@ConsecutivePescription,@ConsecutiveInputs,NULL
								,NULL,NULL,[Common].[GETDATE](),@HistoryTypeCrystal,@HistoryTypeNameCrystal,NULL
								,@ConsecutivePharmacy,NULL,NULL
						FROM Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK)
						JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON pdd.ProductId = ip.Id
						LEFT JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
						LEFT JOIN Inventory.InventorySupplie ins WITH (NOLOCK) ON ip.SupplieId = ins.Id
						where pdd.Id = @RowId
					END --fin ciclo						
				END

				---- Realizo las modificacion  en el fisico y kardex del paciente para que enfermeria pueda aplicar
				IF @FunctionalUnitCode IS NOT NULL AND @ConsecutivePharmacy IS NOT NULL AND @CareCenterCode IS NOT NULL
				BEGIN
					--- Inserto en el Kardex de crystal									
					IF @HistoryType = 'ENFERMER1' 
					BEGIN
						SET @HistoryTypeCrystal = 2
						SET @HistoryTypeNameCrystal = 'Despacho Farmacia, Origen Solicitud: Despacho de Farmacia - Solicitud de Enfermeria - Usuario: ' + @UserCode
					END
					ELSE IF @HistoryType = 'CODIGOAZU' 
					BEGIN
						SET @HistoryTypeCrystal = 3
						SET @HistoryTypeNameCrystal = 'Despacho Farmacia, Origen Solicitud: Despacho de Farmacia - Solicitud de Emergencia - Usuario: ' + @UserCode
					END
					ELSE 
					BEGIN
						SET @HistoryTypeCrystal = 1
						IF ISNULL(@EntityName, '') = 'PharmaceuticalDispensingTransfer'
						BEGIN
							SET @HistoryTypeNameCrystal = CONCAT('Traslado Dispensación por Ingreso ', @EntityCode, ': Ingreso Origen ', @AdmissionNumberOrigin, ' - Ingreso Destino ', @AdmissionNumber, ' - Usuario: ', @UserCode)
						END
						ELSE
						BEGIN
							SET @HistoryTypeNameCrystal = 'Despacho Farmacia, Origen Solicitud: Despacho de Farmacia - Solicitud del Medico - Usuario: ' + @UserCode
						END
					END
						
					SET @Rows = 1
					SET @RowId = 0

					WHILE @Rows > 0
					BEGIN
						SELECT TOP 1 
							@RowId = IdTmp
						FROM @Detail
						WHERE ChangeTracker <> 'Deleted' 
							AND Extramural = 0 AND GuardaGastoQX = 0
							AND IdTmp > @RowId 
						ORDER BY IdTmp

						SET @Rows = @@ROWCOUNT
						IF @Rows = 0 
							BREAK

						--si el producto ya esta en la hoja de gasto procedemos a actualziar cantidades
						UPDATE phy
							SET phy.CANACTPRO = phy.CANACTPRO + pdd.Quantity
						FROM @Detail pdd
						JOIN dbo.HCFISIPRO phy WITH (NOLOCK) 
							ON @PatientCode = phy.IPCODPACI AND @AdmissionNumber = phy.NUMINGRES AND @CareCenterCode = phy.CODCENATE
								AND @FunctionalUnitCode = UFUCODIGO AND pdd.CodeProduct = phy.CODPRODUC
						WHERE pdd.IdTmp = @RowId 

						--insertamos las dispenciones nuevas en el detalle de la hoja de gasto
						INSERT INTO dbo.HCFISIPRO
						(
							[IPCODPACI],[NUMINGRES],[CODCENATE],[UFUCODIGO],[CODPRODUC],[TIPPRODUC],
							[CANACTPRO],[CANPEDPRO],[CANPENPRO],[INDAUDFOR]
						)
						SELECT	@PatientCode,@AdmissionNumber,@CareCenterCode,@FunctionalUnitCode,pdd.CodeProduct,pdd.ProductType,
								pdd.Quantity,pdd.CantidadSolicitada,pdd.CantidadSolicitada - pdd.Quantity,0
						FROM @Detail pdd
						LEFT JOIN dbo.HCFISIPRO phy WITH (NOLOCK) 
							ON @PatientCode = phy.IPCODPACI AND @AdmissionNumber = phy.NUMINGRES AND @CareCenterCode = phy.CODCENATE
								AND @FunctionalUnitCode = UFUCODIGO AND pdd.CodeProduct = phy.CODPRODUC
						WHERE pdd.IdTmp = @RowId AND phy.IPCODPACI IS NULL

						UPDATE dbo.INCONSECU SET @ConsecutiveKardexDispesing = CONNUMACT += 1 WHERE IDCONSECU = '00000011'

						--- Inserto en el Kardex de crystal
						INSERT INTO [dbo].[HCKARDPAC]
						(
							[NUMCONSEC],[IPCODPACI],[NUMINGRES],[CODCENATE],[UFUCODIGO],[CODPROSAL]
							,[CODPRODUC],[CANPRODUCT],[TIPREGIST],[HCPRESCRN],[HCSOLINSN],[HCCTRAPLN]
							,[HCCTRAPLM],[CODDOCUME],[FECREGKAR],[TIPORIREG],[DESMOVPRO],[JUSANULAC]
							,[CONSECFAR],[FECHAUTIL],[OBSERVACI]
						)
						SELECT	@ConsecutiveKardexDispesing - 1,@PatientCode,@AdmissionNumber,@CareCenterCode,@FunctionalUnitCode,tpdd.OrderedHealthProfessionalCode
								,tpdd.CodeProduct,tpdd.Quantity,'1',@ConsecutivePescription,@ConsecutiveInputs,NULL
								,NULL,NULL,[Common].[GETDATE](),@HistoryTypeCrystal,@HistoryTypeNameCrystal,NULL
								,@ConsecutivePharmacy,NULL,NULL
						FROM @Detail tpdd
						WHERE tpdd.IdTmp = @RowId

						UPDATE fd
							SET fd.CANPENPRO = fd.CANPENPRO - tpdd.Quantity
						FROM @Detail tpdd
						JOIN dbo.HCFARMEPD fd WITH (NOLOCK) ON @ConsecutiveCrystal = fd.CODCONCEC AND tpdd.CodeProduct = fd.CODPRODUC
						WHERE tpdd.IdTmp = @RowId

						UPDATE fd
							SET fd.PROESTADO = '2'
						FROM @Detail tpdd
						JOIN dbo.HCFARMEPD fd WITH (NOLOCK) ON @ConsecutiveCrystal = fd.CODCONCEC AND tpdd.CodeProduct = fd.CODPRODUC
						WHERE tpdd.IdTmp = @RowId AND fd.CANPEDPRO = 0
					END
					

					--Actualizar cuando es procedimiento quirurgicos 	
					IF @Rows = 0 
					BEGIN
		
						UPDATE fd
								SET fd.CANPENPRO = fd.CANPENPRO - d.Quantity		
						FROM dbo.HCFARMEPD fd
						JOIN (SELECT CodeProduct,CantidadSolicitada,SUM(d.Quantity) AS Quantity FROM  @Detail d GROUP BY CodeProduct,CantidadSolicitada ) as d ON 
						@ConsecutiveCrystal = fd.CODCONCEC AND d.CodeProduct = fd.CODPRODUC  
					END
					
					--Actualizar el estado del detalle en crystal cuando se ha entregado el total dispensado
					UPDATE fd
							SET fd.PROESTADO = '2'				
					FROM dbo.HCFARMEPD fd
					JOIN (SELECT  CodeProduct,CantidadSolicitada,SUM(d.Quantity) AS Quantity FROM  @Detail d GROUP BY CodeProduct,CantidadSolicitada ) as d ON 
					@ConsecutiveCrystal = fd.CODCONCEC AND d.CodeProduct = fd.CODPRODUC AND d.Quantity = d.CantidadSolicitada 

					IF NOT EXISTS (SELECT 1 FROM dbo.HCFARMEPD WITH (NOLOCK) WHERE CODCONCEC = @ConsecutiveCrystal AND CANPENPRO > 0)
					BEGIN
						UPDATE dbo.HCFARMEPC SET ORDESTADO = '2' WHERE CODCONCEC = @ConsecutiveCrystal
					END
				END

				/************************************************ Usuario ************************************************/

				SELECT @Message_Output = CONCAT(' *USERINDIGO* ', pd.CreationUser, ' - ', p.Fullname)
				FROM Inventory.PharmaceuticalDispensing pd WITH (NOLOCK)
				LEFT JOIN [Security].[User] u ON pd.CreationUser = u.UserCode
				LEFT JOIN [Security].Person p ON u.IdPerson = p.Id	
				WHERE pd.Id = @Id

				SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
			END
		END

		IF @Status = 1
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM Inventory.InventoryControlDocument WITH (NOLOCK) WHERE DocumentType = @DocumentTypeControl AND DocumentNumber = @Code)
			BEGIN
				INSERT INTO Inventory.InventoryControlDocument (DocumentNumber, DocumentType, DocumentUser, DocumentDate)
				SELECT @Code, @DocumentTypeControl, @UserCode, @DocumentDate
			END
		END
		ELSE
		BEGIN
			DELETE FROM Inventory.InventoryControlDocument WHERE DocumentType = @DocumentTypeControl AND DocumentNumber = @Code
		END

		SELECT @CodeResult = 0, 
			   @MessageResult = CASE @Status
				   WHEN 2 THEN CONCAT('Se guardó y confirmó la Dispensación Farmacéutica con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló la Dispensación Farmacéutica con código ', @Code)
				   ELSE CONCAT('Se guardó la Dispensación Farmacéutica con código ', @Code)
			   END + IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10) + ISNULL(@Message, ''))
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = CONCAT('SP_SavePharmaceuticalDispensing: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que guarda o confirma una dispensación farmacéutica a partir de un XML con los datos de cabecera y detalle del despacho de medicamentos. Gestiona el ciclo de vida completo del documento de dispensación: crea, modifica o anula registros en PharmaceuticalDispensing y PharmaceuticalDispensingDetail, vinculando el ingreso del paciente (ADINGRESO), el tercero asociado, la configuración de inventario (SettingInventory) y, cuando aplica, los gastos de hoja quirúrgica o ciclos de quimioterapia. También genera el movimiento contable correspondiente y retorna el identificador, código y resultado de la operación como parámetros de salida.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SavePharmaceuticalDispensingNative_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SavePharmaceuticalDispensingNative_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Guarda, confirma o anula dispensaciones farmacéuticas afectando inventario, kardex (físico y de custodia), comprobante contable, orden de servicio de facturación, hoja de gasto quirúrgico y registros del EHR (historia clínica/quimioterapia).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingNative_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El registro objetivo no debe estar en estado distinto de 1 (pendiente); si está Confirmado=2 o Anulado=3 se rechaza.; Debe existir configuración de inventario (SettingInventory) para la unidad operativa.; La fecha de documento no puede ser anterior al periodo abierto (Year/Month) configurado en SettingInventory.; No debe existir previamente una Billing.ServiceOrder con EntityCode=Code y EntityName=''PharmaceuticalDispensing''.; El ingreso (ADINGRESO) debe tener entidad administradora (GENCONENTITY) y esta debe existir homologada en Contract.HealthAdministrator.; El paciente del ingreso debe existir como tercero en Common.ThirdParty.; Si se envía FunctionalUnitCode debe estar homologada en Payroll.FunctionalUnit.; Si el paciente está en más de una cama, debe existir unidad funcional de tipo 19 (hospitalización) y no debe haber traslados pendientes de aceptación de medicamentos.; Todos los detalles deben tener cantidad >0, costo promedio >0 y al menos un lote asociado con cantidad coincidente con la del detalle.; Los productos no pueden venir de almacén en tránsito (TransitStore=1).; Los lotes seleccionados deben corresponder al producto del detalle, tanto en PhysicalInventory como en PhysicalInventoryCustody, y no estar vencidos cuando se afecta inventario.; Si AssociateCostMainAccount=1 las unidades funcionales deben estar parametrizadas en SettingInventoryFunctionalUnit; si =2, deben estar en ProductGroupFunctionalUnit.; Para flujo EHR (FunctionalUnitCode, ConsecutivePharmacy y CareCenterCode no nulos) los productos dispensados deben estar solicitados en HCFARMEPD y la cantidad dispensada no puede superar la pendiente.; Si Extramural=0, el ingreso no debe estar egresado (CHREGEGRE); si Extramural=1, el ingreso no puede estar anulado (IESTADOIN=''A'').', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingNative_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingNative_Output';
-- GO
