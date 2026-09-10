-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-03-22
-- Description:	Procedimiento para guardar o confirmar las dispensaciones farmaceuticas
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SavePharmaceuticalDispensingIntegrated_Output]
	@XmlPharmaceutical XML,
	@XmlDispensingIntegrationMedilaser XML,
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

	DECLARE @OperatingUnitId INT,
			@AdmissionNumber VARCHAR(20),
			@PatientCode VARCHAR(20),
			@DocumentDate DATETIME,
			@AffectInventory BIT,
			@Status TINYINT,
			@EntityName VARCHAR(250),
			@EntityCode VARCHAR(20),
			@EntityId INT,
			@IsPharmaceuticalDispensing TINYINT,
			@DispensingIntegration TINYINT,
			-----------------------------------------------
			@CODEMPRES CHAR(5), 
			@AUUBICACI CHAR(20),
			@PatientThirdPartyId INT,
			@PatientFirstName VARCHAR(20),
			@PatientMiddleName VARCHAR(20),
			@PatientLastName VARCHAR(20),
			@PatientSecondLastName VARCHAR(20),
			@PatientName VARCHAR(250),
			-----------------------------------------------
			@CareCenterCode VARCHAR(20),
			@FunctionalUnitId INT,
			@FunctionalUnitCode VARCHAR(20),
			@FunctionalUnitName VARCHAR(200),
			@CareGroupId INT, 
			@HealthAdministratorId INT,
			@FunctionalUnitHeonId AS INT,
			@ConsecutivePharmacy VARCHAR(20),			
			@HistoryType VARCHAR(20),
			@ConsecutivePescription INT,
			@ConsecutiveInputs VARCHAR(20),
			@ConsecutiveCrystal VARCHAR(20),
			@IDHCORDPRON INT, --Permite saber si el registro de la pestaña de quimioterapia es de tipo domiciliaria
			-----------------------------------------------
			@Message VARCHAR(MAX)

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
		QuotationPharmaceuticalDispensingDetailId INT,
		-----------------------------------------------
		IdProductHeon INT,
		recetarioOMedica VARCHAR(20)
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

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT	@OperatingUnitId = t.x.value('OperatingUnitId[1]','INT'),
				@Id = t.x.value('Id[1]','INT'),
				@Code = t.x.value('Code[1]','VARCHAR(20)'),
				@AdmissionNumber = t.x.value('AdmissionNumber[1]','VARCHAR(20)'),
				@PatientCode = t.x.value('CodePatient[1]','varchar(20)'),
				@DocumentDate = t.x.value('DocumentDate[1]', 'DATETIME'),
				@AffectInventory = t.x.value('AffectInventory[1]','BIT'),
				@Status = t.x.value('Status[1]','TINYINT'),
				@EntityName = t.x.value('EntityName[1]','VARCHAR(250)'),
				@EntityCode = t.x.value('EntityCode[1]','VARCHAR(20)'),
				@EntityId = t.x.value('EntityId[1]','INT'),
				@IsPharmaceuticalDispensing = t.x.value('IsPharmaceuticalDispensing[1]','TINYINT'),
				@DispensingIntegration = t.x.value('DispensingIntegration[1]','TINYINT'),
				-------------------------------------------------------------------
				@PatientFirstName = SUBSTRING(t.x.value('PantientFirstName[1]','VARCHAR(150)'), 0, 20),
				@PatientMiddleName = SUBSTRING(t.x.value('PantientMiddleName[1]','VARCHAR(150)'), 0, 20),
				@PatientLastName = SUBSTRING(t.x.value('PantientLastName[1]','VARCHAR(150)'), 0, 20),
				@PatientSecondLastName = SUBSTRING(t.x.value('PantientSecondLastName[1]','VARCHAR(150)'), 0, 20),				
				@PatientName = SUBSTRING(t.x.value('PantientName[1]','VARCHAR(300)'), 0, 250),
				-------------------------------------------------------------------
				@CareCenterCode = t.x.value('CareCenterCode[1]','VARCHAR(20)'),
				@FunctionalUnitCode = t.x.value('FunctionUnitCode[1]','VARCHAR(20)'),
				@FunctionalUnitName = t.x.value('FunctionUnitName[1]','VARCHAR(200)'),
				@ConsecutivePharmacy = t.x.value('ConsecutivePharmacy[1]','VARCHAR(20)'),			
				@HistoryType =  t.x.value('HistoryType[1]','VARCHAR(20)'),
				@ConsecutivePescription =  t.x.value('ConsecutivePescription[1]','INT'),
				@ConsecutiveInputs =  t.x.value('ConsecutiveInputs[1]','VARCHAR(20)'),
				@ConsecutiveCrystal = t.x.value('ConsecutiveCrystal[1]','VARCHAR(20)'),
				@IDHCORDPRON = t.x.value('IDHCORDPRON[1]','INT') --Permite saber si el registro de la pestaña de quimioterapia es de tipo domiciliaria
		FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x)

		INSERT INTO @Detail 
		(
			IdTmp, Id, CareGroupId, HealthAdministratorId, ThirdPartyId, ProductType, ProductId, CodeProduct, WarehouseId, CantidadSolicitada, Quantity, CantidadPendiente, 
			ServiceDate, FunctionalUnitId, OrderedHealthProfessionalCode, OrderedProfessionalSpecialty, OrderedHealthProfessionalThirdPartyId, AuthorizationNumber, 
			LiquidationType, SurchargeApply, SalePrice, AverageCost, DiscountPercentage, DiscountValue, TotalSalesPrice, GrandTotalSalesPrice, 
			EntityId, EntityName, ChangeTracker, GuardaGastoQX,IdProgramacionQXPrincipal,Extramural, QuotationPharmaceuticalDispensingDetailId,
			IdProductHeon, recetarioOMedica
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
					t.x.value('QuotationPharmaceuticalDispensingDetailId[1]','INT'),
					t.x.value('idProductoHeon[1]','int'),
					t.x.value('recetarioOMedica[1]','varchar(20)')
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

		---------------------------------------------------------------------------------------------------------------

		--Si el proceso viene de la integración por paciente medilaser se asignan algunos datos
		IF @XmlDispensingIntegrationMedilaser.exist('*') > 0
		BEGIN
			SELECT	@AdmissionNumber = t.x.value('AdmissionNumber[1]','varchar(20)'),
					@DispensingIntegration = t.x.value('DispensingIntegration[1]','tinyint'),
					@HealthAdministratorId = t.x.value('HealthAdministratorId[1]','int'),
					@FunctionalUnitId = t.x.value('FunctionalUnitId[1]','int')
			FROM @XmlDispensingIntegrationMedilaser.nodes('/TableXml') t(x)
		END

		/***********************************************  VALIDACIONES ***********************************************/

		IF @DispensingIntegration = 2
		BEGIN
			IF EXISTS
			(
				SELECT 1
				FROM Inventory.ControlIntegrationHeon cih WITH (NOLOCK)
				JOIN Inventory.ControlIntegrationHeonDetail cihd WITH (NOLOCK) ON cih.Id = cihd.ControlIntegrationHeonId
				JOIN @Detail tpdd ON cihd.MedicalOrderRecipe = tpdd.recetarioOMedica
				WHERE cih.Status = 2
			)
			BEGIN
				SELECT @Message = STUFF((
					SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- Orden Médica: ', cihd.MedicalOrderRecipe, '(Producto: ', tpdd.CodeProduct, ' - ', cihd.Message, ')')
					FROM Inventory.ControlIntegrationHeon cih WITH (NOLOCK)
					JOIN Inventory.ControlIntegrationHeonDetail cihd WITH (NOLOCK) ON cih.Id = cihd.ControlIntegrationHeonId
					JOIN @Detail tpdd ON cihd.MedicalOrderRecipe = tpdd.recetarioOMedica
					WHERE cih.Status = 2
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999,
						@MessageResult = 'Los siguientes productos tiene un proceso pendiente en las tablas de control de integración: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END
		END

		/************************************************** PROCESO **************************************************/

		IF @DispensingIntegration = 2
		BEGIN
			--Obtengo la primera unidad funcional que exista
			SELECT TOP 1 
				@FunctionalUnitId = Id, 
				@FunctionalUnitCode = Code
			FROM Payroll.FunctionalUnit WITH (NOLOCK)
			WHERE [State] = 1

			--Obtengo la unidad funcional de HEON
			SELECT TOP 1 
				@FunctionalUnitHeonId = Id 
			FROM Inventory.FunctionalUnit WITH (NOLOCK) 
			WHERE [Name] = @FunctionalUnitName AND CareCenterCode = @CareCenterCode

			--Validamos si existe la unidad funcional de integracion con Heon
			IF ISNULL(@FunctionalUnitHeonId, 0) = 0 BEGIN
				INSERT INTO Inventory.FunctionalUnit ([Name], CareCenterCode)
				SELECT @FunctionalUnitName AS [Name], @CareCenterCode AS CareCenterId

				SET @FunctionalUnitHeonId = SCOPE_IDENTITY()
			END

			--Obtengo la entidad administradora de acuerdo al contrato que tiene el grupo de atencion				
			SELECT TOP 1
					@HealthAdministratorId = c.HealthAdministratorId,
					@CareGroupId = cg.Id
			FROM @Detail tpdd
			JOIN Contract.CareGroup cg WITH (NOLOCK) ON tpdd.CareGroupId = cg.Id
			JOIN Contract.Contract c WITH (NOLOCK) ON cg.ContractId = c.Id
			JOIN Inventory.CareGroupByCareCenter cgcc WITH (NOLOCK) ON cg.Id = cgcc.CareGroupId
			WHERE cgcc.CareCenterCode = @CareCenterCode 
			ORDER BY c.HealthAdministratorId

			--Obtengo la primera empresa
			SELECT TOP 1 @CODEMPRES = CODEMPRES
			FROM dbo.ADEMPRESA WITH (NOLOCK)
				
			--Obtengo la primera ubicacion del departamento del centro de atencion
			SELECT TOP 1 @AUUBICACI = u.AUUBICACI
			FROM dbo.INUBICACI u WITH (NOLOCK)
			JOIN dbo.ADCENATEN ca WITH (NOLOCK) ON u.DEPMUNCOD = ca.DEPMUNCOD
			WHERE ca.CODCENATE = @CareCenterCode
				
			--Validamos si existe el paciente
			IF NOT EXISTS(SELECT 1 FROM dbo.INPACIENT WITH (NOLOCK) WHERE IPCODPACI = @PatientCode) 
			BEGIN
				INSERT INTO dbo.INPACIENT
				(
					IPCODPACI, IPTIPODOC, CODIGONIT, IPEXPEDIC, IPPRIAPEL, IPSEGAPEL, IPPRINOMB, IPSEGNOMB, IPNOMCOMP, CODEMPRES, IPTIPOPAC, IPTIPOAFI, CAPACIPAG, AUUBICACI, NIVCODIGO, 
					IPDIRECCI, IPTELEFON, IPTELMOVI, IPFECNACI, CODACTIVI, IPSEXOPAC, IPESTADOC, TIPCOBSAL, ESTADOPAC, INDAUDFOR, NUMCARPET, CODUSUCRE, FECREGCRE, GENCAREGROUP,GENCONENTITY
				)
					SELECT	@PatientCode AS INPACIENT,
							6 AS IPTIPODOC,
							@PatientCode AS CODIGONIT,
							[Common].[GETDATE]() AS IPEXPEDIC,
							@PatientLastName AS IPPRIAPEL,
							@PatientSecondLastName AS IPSEGAPEL,
							@PatientFirstName AS IPPRINOMB,
							@PatientMiddleName AS IPSEGNOMB,
							@PatientName AS IPNOMCOMP,
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
							RIGHT('000000000000000' + @PatientCode, 15) NUMCARPET, 
							@UserCode CODUSUCRE, 
							[Common].[GETDATE]() FECREGCRE,
							@CareGroupId,
							@HealthAdministratorId
					FROM dbo.ADCENATEN ca WITH(NOLOCK) 
					WHERE ca.CODCENATE = @CareCenterCode
			END

			--Validamos si existe el tercero
			IF NOT EXISTS(SELECT 1 FROM Common.ThirdParty WITH (NOLOCK) WHERE Nit = @PatientCode) 
			BEGIN
				--Validamos si existe la persona
				IF NOT EXISTS(SELECT 1 FROM Common.Person WITH (NOLOCK) WHERE IdentificationNumber = @PatientCode) 
				BEGIN
					INSERT INTO Common.Person
						(
							IdentificationNumber, IdentificationType, FirstName, SecondName, FirstLastName, SecondLastName, State
						)
						SELECT @PatientCode, 5, @PatientFirstName, @PatientMiddleName, @PatientLastName, @PatientSecondLastName, 1
				END

				INSERT INTO Common.ThirdParty
				(
					PersonId, Nit, DigitVerification, Name, PersonType, RetentionType, ContributionType, StateEnterpriseType, IVARetentionAccountPayableConceptId, Ica, IcaPercentage, IcaTop, 
					IcaTopValue, HandlesBranchOffice, State, CreationDate, UserId
				)
					SELECT	Id AS PersonId,
							IdentificationNumber AS Nit,
							NULL AS DigitVerification,							
							@PatientName AS Name,
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
					FROM Common.Person WITH (NOLOCK)
					WHERE IdentificationNumber = @PatientCode
			END
		END

		UPDATE tpdd 
			SET tpdd.FunctionalUnitId = @FunctionalUnitId,
				tpdd.HealthAdministratorId = @HealthAdministratorId,
				tpdd.ThirdPartyId = tp.Id,
				tpdd.OrderedHealthProfessionalThirdPartyId = tp.Id
		FROM @Detail tpdd
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON tp.Nit = @PatientCode

		--Validamos si existe el ingreso
		IF NOT EXISTS (SELECT 1 FROM dbo.ADINGRESO WITH (NOLOCK) WHERE NUMINGRES = @AdmissionNumber) 
		BEGIN
			--Se inserta el ingreso el ingreso
			INSERT INTO dbo.ADINGRESO
			(
				NUMINGRES,IPCODPACI,TIPOINGRE,IINGREPOR,ITIPORIES,ICAUSAING,CODENTIDA,IFECHAING,ILIQUIDAC,ICONTROLI,CODCENATE,
				UFUCODIGO,IESTADOIN,IREINGRES,UFUACTPAC,GENCAREGROUP,GENCONENTITY,CODUSUCRE,FECREGCRE,INDAUDFOR
			)
				SELECT @AdmissionNumber,@PatientCode,1,1,1,3,'0001',[Common].[GETDATE](),1,'',@CareCenterCode,
					@FunctionalUnitCode,'',0,@FunctionalUnitCode,@CareGroupId,@HealthAdministratorId,@UserCode,[Common].[GETDATE](),0
		END

		---------------------------------------------------------------------------------------------------------------

		SELECT @XmlPharmaceutical = CONVERT
		(
			XML, 
			(
				SELECT *
				FROM 
				(
					SELECT	@OperatingUnitId  OperatingUnitId,
							0 Id, 
							'' Code, 
							@AdmissionNumber AdmissionNumber, 
							@DocumentDate DocumentDate, 
							@AffectInventory AffectInventory,							
							1 Status, 
							@EntityName EntityName,							
							@EntityCode EntityCode, 
							@EntityId EntityId,
							@IsPharmaceuticalDispensing IsPharmaceuticalDispensing,
							@DispensingIntegration DispensingIntegration,
							@CareCenterCode CareCenterCode,
							@FunctionalUnitCode FunctionUnitCode,
							@ConsecutivePharmacy ConsecutivePharmacy,
							@HistoryType HistoryType,
							@ConsecutivePescription ConsecutivePescription,
							@ConsecutiveInputs ConsecutiveInputs,
							@ConsecutiveCrystal ConsecutiveCrystal,
							@IDHCORDPRON IDHCORDPRON
				) PharmaceuticalDispensing
				JOIN
				(               
					SELECT  0 PharmaceuticalDispensingId,
							IdTmp PharmaceuticalDispensingDetailIdTmp,
							Id,
							CareGroupId,
							HealthAdministratorId,
							ThirdPartyId,
							ProductType,
							ProductId,
							CodeProduct,
							WarehouseId,
							CantidadSolicitada,
							Quantity,
							CantidadPendiente,
							ServiceDate,
							FunctionalUnitId,
							OrderedHealthProfessionalCode,
							OrderedProfessionalSpecialty,
							OrderedHealthProfessionalThirdPartyId,
							AuthorizationNumber,
							LiquidationType,
							SurchargeApply,
							SalePrice,
							AverageCost,
							DiscountPercentage,
							DiscountValue,
							TotalSalesPrice,
							GrandTotalSalesPrice,
							EntityId,
							EntityName,
							ChangeTracker,
							GuardaGastoQX,
							IdProgramacionQXPrincipal,
							Extramural,
							QuotationPharmaceuticalDispensingDetailId
					FROM @Detail
				) PharmaceuticalDispensingDetail ON PharmaceuticalDispensing.Id = PharmaceuticalDispensingDetail.PharmaceuticalDispensingId
				JOIN
				(               
					SELECT  PharmaceuticalDetailIdTmp PharmaceuticalDispensingDetailIdTmp,
							Id,
							PharmaceuticalDispensingDetailId,
							PhysicalInventoryId,
							Quantity,
							OutstandingQuantity,
							PhysicalInventoryCustodyId,
							ChangeTracker
					FROM @DetailBatchSerial
				) PharmaceuticalDispensingDetailBatchSerial ON PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailIdTmp = PharmaceuticalDispensingDetailBatchSerial.PharmaceuticalDispensingDetailIdTmp
				FOR XML AUTO,TYPE, ELEMENTS
			)
		)

		EXEC [Inventory].[SP_SavePharmaceuticalDispensingNative_Output]
			@XmlPharmaceutical,
			@UserCode,
			---------------------------------------------------------------------------
			@CodeResult OUT,
			@MessageResult OUT,
			------------------------------------------------------
			@Id OUT,
			@Code OUT

		---------------------------------------------------------------------------------------------------------------

		-- De acuerdo con el tipo de integración (2 - Heon) Guardamos la data para ser enviada al servicio
		IF @CodeResult = 0 AND @DispensingIntegration = 2 
		BEGIN
			--Almacenamos el registro de la dispensacion por unidad funcional de Heon
			INSERT INTO [Inventory].[DispensingByFunctionalUnit]
						([FunctionalUnitId], [PharmaceuticalDispensingId])
				SELECT @FunctionalUnitHeonId, @Id				
		END
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = CONCAT('SP_SavePharmaceuticalDispensing: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que guarda o confirma dispensaciones farmacéuticas integradas en el módulo de inventario y farmacia. Recibe un XML con la cabecera de la dispensación (paciente, ingreso, centro de atención, unidad funcional, fecha, estado) y el detalle de cada medicamento o insumo dispensado (producto, bodega, cantidades solicitada/dispensada/pendiente, precios, descuentos, costos promedio, tipo de liquidación, profesional ordenador, número de autorización). Gestiona la afectación real del inventario, registra lotes y series asociados, genera consecutivos de farmacia, recetario y prescripción médica, y retorna el identificador y código del documento creado o confirmado. Es el punto central de integración entre la orden médica, la dispensación farmacéutica y el movimiento de inventario, tocando entidades de paciente, admisión/ingreso, unidad funcional, aseguradora y producto farmacéutico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SavePharmaceuticalDispensingIntegrated_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SavePharmaceuticalDispensingIntegrated_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Orquesta el guardado/confirmación de dispensaciones farmacéuticas, preparando datos maestros (paciente, tercero, ingreso, unidad funcional) cuando provienen de la integración HEON, antes de delegar la persistencia al SP nativo.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingIntegrated_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML @XmlPharmaceutical debe contener un nodo /PharmaceuticalDispensing con la cabecera y sus detalles bajo PharmaceuticalDispensingDetail y PharmaceuticalDispensingDetailBatchSerial.; Si DispensingIntegration = 2 (HEON), no deben existir registros en Inventory.ControlIntegrationHeon con Status = 2 cuyas órdenes médicas (MedicalOrderRecipe) coincidan con las del detalle; de lo contrario se aborta con CodeResult=999.; Para integración HEON debe existir al menos una unidad funcional activa en Payroll.FunctionalUnit (State=1) y al menos una empresa en dbo.ADEMPRESA.; El centro de atención (@CareCenterCode) debe existir en dbo.ADCENATEN para resolver dirección, teléfonos y ubicación al crear paciente/ingreso.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingIntegrated_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Inventory.FunctionalUnit: En integración HEON (DispensingIntegration=2), si no existe una FunctionalUnit con Name=@FunctionalUnitName y CareCenterCode=@CareCenterCode, se crea una nueva y su Id se usa como FunctionalUnitHeonId.; [INSERT] dbo.INPACIENT: En integración HEON, si no existe paciente con IPCODPACI=@PatientCode, se crea con IPTIPODOC=6, IPTIPOPAC=1, IPTIPOAFI=2, NIVCODIGO=''01'', CODACTIVI=''9998'', estado activo, tomando dirección/teléfonos del centro de atención y asignando GENCAREGROUP/GENCONENTITY del contrato resuelto.; [INSERT] Common.Person: En integración HEON, si no existe ThirdParty con Nit=@PatientCode y tampoco Person con IdentificationNumber=@PatientCode, se crea la persona con IdentificationType=5 y State=1.; [INSERT] Common.ThirdParty: En integración HEON, si no existe ThirdParty con Nit=@PatientCode, se crea como PersonType=1 (natural), sin retenciones/ICA, State=1, vinculado a la Person recién creada o existente.; [INSERT] dbo.ADINGRESO: Si no existe ingreso con NUMINGRES=@AdmissionNumber, se inserta uno nuevo con TIPOINGRE=1, IINGREPOR=1, ITIPORIES=1, ICAUSAING=3, CODENTIDA=''0001'', ILIQUIDAC=1, estado 0 y los códigos de centro/unidad funcional/grupo de atención/entidad administradora resueltos.; [UPDATE] @Detail: Tras crear/validar el tercero del paciente, todos los detalles se actualizan con FunctionalUnitId=@FunctionalUnitId, HealthAdministratorId=@HealthAdministratorId y ThirdPartyId/OrderedHealthProfessionalThirdPartyId = ThirdParty.Id donde Nit=@PatientCode.; [INSERT] Inventory.DispensingByFunctionalUnit: Si el SP nativo retorna CodeResult=0 y DispensingIntegration=2, se registra la dispensación contra la unidad funcional HEON (FunctionalUnitHeonId, @Id) para envío posterior al servicio.; [RETURN_RESULT] @CodeResult/@MessageResult: Si hay órdenes médicas con integración HEON pendiente (Status=2), se retorna CodeResult=999 y un mensaje con el listado de órdenes/productos afectados, sin continuar el proceso.; [RETURN_RESULT] @CodeResult/@MessageResult: Cualquier excepción se captura y devuelve CodeResult=999 con mensaje ''SP_SavePharmaceuticalDispensing: <ERROR_MESSAGE> - Linea: <ERROR_LINE>''.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingIntegrated_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingIntegrated_Output';
-- GO
