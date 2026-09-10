
-- ==========================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 21/06/2019
-- Description:	Store que se encarga de guardar el producto
-- ==========================================================
CREATE PROCEDURE [Inventory].[SP_SaveInventoryProduct]
	@Xml xml,
	@UserCode varchar(20),
	@OperatingUnitId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Variables para asignar los valores desde el xml para poder guardar
	declare @Id int, @Code varchar(20), @Name varchar(200), @ProductTypeId int, @ATCId int, @CodeCUM varchar(20), @CodeAlternative varchar(30), @CodeAlternativeTwo varchar(20), @Description varchar(max),
    @ProductGroupId int, @ProductSubGroupId int, @MeasurementUnitId int, @PackagingUnitId int, @ManufacturerId int, @Osmolarity numeric(18,2), @IVAId int, @Presentation varchar(200), @CodeSICE varchar(20), @HandlesSerial bit,
    @HandlesHealthRegistration bit, @HealthRegistration varchar(30), @ExpirationDate datetime, @BillingGroupId int, @ProductControl bit, @ProductWithPriceControl bit, @POSProduct bit, 
	@AuthorizationByOrderNumber int, @ExpirationDay int, @MaximumControlPeriod bit, @ControlDays int, @ControlOrderQuantity bit, @ProductOrderAmount int, @LastPurchase datetime, @LastSale datetime,
    @ProductOrigin tinyint, @MinimumStock int, @MaximumStock int, @CommissionPercentage numeric(5,2), @RepositionPoint int, @ResetTime int, @CurrencyType tinyint, @ProductCost numeric(18,2),
	@FinalProductCost numeric(18,2), @SellingPrice numeric(18,2), @AllPOSPathologies bit, @Status bit, @BillingGroupNoPosId int, @ControlCostPercentage numeric(5,2), @InventoryRiskLevelId int,
    @SerialNumber varchar(40), @DriveUnit int, @MinimumTemperature int, @MaximumTemperature int, @SanitaryRegistration int, @Consumption bit, 
	@JustificationSuppliesDispositives bit, @OsteosynthesisMaterial bit, @Abbreviation varchar(20), @SupplieId int, @IUM VARCHAR(15),@Storage INT, @TaxedProduct bit,
	@LiquidateSalesTaxes BIT, @SismedReport BIT, @DairyComponent BIT, @DairyComponentType int,@MedicationTypeId int, @WeightParenteralNutritionSupply decimal(18,2)
	
	--Tabla en donde se almacenan los códigos de barra del producto
	declare @TableProductBarcode table(Id int, Barcode varchar(300), IsDelete bit)

	--Tabla en donde se almacenan las patologías que vienen del xml
	declare @TablePathologies table(Id int, DiagnosticId int, DiagnosticCode varchar(20), ProductId int, IsDelete bit, MinimumAge tinyint, MaximumAge tinyint, AgeMeasure tinyint)

	--Tabla en donde se almacenan los atributos del producto
	declare @TableInventoryProductAttribute table(Id int, AttributeProductTypeId int, Value varchar(100), IsDelete bit)

	--Variable para obtener los errores de las validaciones
	declare @Errors varchar(MAX) = ''

	--Clase del producto
	declare @ClassProductType TINYINT

	--Tabla donde almacena la jerarquia asociada del producto
	DECLARE @ProductHierarchy TABLE(	Id INT,
										HierarchyProductFinalId INT,
										ProductId INT,
										ParentProductId INT,
										ConversionUnit BIGINT,
										IsDelete BIT)

	begin try
		
		--Se obtienen los datos del xml
		select 
			@Id = t.x.value('Id[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@Name = dbo.DecodeXmlToText(t.x.value('Name[1]','varchar(200)')),
			@ProductTypeId = t.x.value('ProductTypeId[1]','int'),
			@ATCId = IIF(t.x.value('ATCId[1]','varchar(20)') = '', null, t.x.value('ATCId[1]','varchar(20)')),
			@CodeCUM = IIF(t.x.value('CodeCUM[1]','varchar(20)') = '', null, t.x.value('CodeCUM[1]','varchar(20)')),
			@CodeAlternative = IIF(t.x.value('CodeAlternative[1]','varchar(30)') = '', null, t.x.value('CodeAlternative[1]','varchar(30)')),
			@CodeAlternativeTwo = IIF(t.x.value('CodeAlternativeTwo[1]','varchar(20)') = '', null, t.x.value('CodeAlternativeTwo[1]','varchar(20)')),
			@Description = IIF(t.x.value('Description[1]','varchar(max)') = '', null, dbo.DecodeXmlToText(t.x.value('Description[1]','varchar(max)'))),
			@ProductGroupId = IIF(t.x.value('ProductGroupId[1]','int')='',NULL,t.x.value('ProductGroupId[1]','int')),
			@ProductSubGroupId = IIF(t.x.value('ProductSubGroupId[1]','int')='',NULL,t.x.value('ProductSubGroupId[1]','int')),
			@MeasurementUnitId = IIF(t.x.value('MeasurementUnitId[1]','varchar(20)') = '', null, t.x.value('MeasurementUnitId[1]','varchar(20)')),
			@PackagingUnitId = t.x.value('PackagingUnitId[1]','int'),
			@ManufacturerId = IIF(t.x.value('ManufacturerId[1]','varchar(20)') = '', null, t.x.value('ManufacturerId[1]','varchar(20)')),
			@Osmolarity = IIF(t.x.value('Osmolarity[1]','varchar(20)') = '', null, REPLACE(t.x.value('Osmolarity[1]','varchar(20)'), ',', '.')),
			@IVAId = IIF(t.x.value('IVAId[1]','varchar(20)') = '', null, t.x.value('IVAId[1]','varchar(20)')),
			@Presentation = IIF(t.x.value('Presentation[1]','varchar(200)') = '', null, t.x.value('Presentation[1]','varchar(200)')),
			@CodeSICE = IIF(t.x.value('CodeSICE[1]','varchar(20)') = '', null, t.x.value('CodeSICE[1]','varchar(20)')),
			@HandlesSerial = IIF(t.x.value('HandlesSerial[1]','varchar(20)') = '', null, t.x.value('HandlesSerial[1]','varchar(20)')),
			@HandlesHealthRegistration = IIF(t.x.value('HandlesHealthRegistration[1]','varchar(20)') = '', null, t.x.value('HandlesHealthRegistration[1]','varchar(20)')),
			@HealthRegistration = IIF(t.x.value('HealthRegistration[1]','varchar(30)') = '', null, t.x.value('HealthRegistration[1]','varchar(30)')),
			@ExpirationDate = IIF(t.x.value('ExpirationDate[1]','varchar(30)') = '', null, convert(date, t.x.value('ExpirationDate[1]','varchar(20)'), 103)),
			@BillingGroupId = IIF(t.x.value('BillingGroupId[1]','varchar(20)') = '', null, t.x.value('BillingGroupId[1]','varchar(20)')),
			@ProductControl = IIF(t.x.value('ProductControl[1]','varchar(20)') = '', null, t.x.value('ProductControl[1]','varchar(20)')),
			@ProductWithPriceControl = IIF(t.x.value('ProductWithPriceControl[1]','varchar(20)') = '', null, t.x.value('ProductWithPriceControl[1]','varchar(20)')),
			@POSProduct = IIF(t.x.value('POSProduct[1]','varchar(20)') = '', null, t.x.value('POSProduct[1]','varchar(20)')),
			@AuthorizationByOrderNumber = IIF(t.x.value('AuthorizationByOrderNumber[1]','varchar(20)') = '', null, t.x.value('AuthorizationByOrderNumber[1]','varchar(20)')),
			@ExpirationDay = IIF(t.x.value('ExpirationDay[1]','varchar(20)') = '', null, t.x.value('ExpirationDay[1]','varchar(20)')),
			@MaximumControlPeriod = IIF(t.x.value('MaximumControlPeriod[1]','varchar(20)') = '', null, t.x.value('MaximumControlPeriod[1]','varchar(20)')),
			@ControlDays = IIF(t.x.value('ControlDays[1]','varchar(20)') = '', null, t.x.value('ControlDays[1]','varchar(20)')),
			@ControlOrderQuantity = IIF(t.x.value('ControlOrderQuantity[1]','varchar(20)') = '', null, t.x.value('ControlOrderQuantity[1]','varchar(20)')),
			@ProductOrderAmount = IIF(t.x.value('ProductOrderAmount[1]','varchar(20)') = '', null, t.x.value('ProductOrderAmount[1]','varchar(20)')),
			@LastPurchase = IIF(t.x.value('LastPurchase[1]','varchar(30)') = '', null, convert(date, t.x.value('LastPurchase[1]','varchar(20)'), 103)),
			@LastSale = IIF(t.x.value('LastSale[1]','varchar(30)') = '', null, convert(date, t.x.value('LastSale[1]','varchar(20)'), 103)),
			@ProductOrigin = IIF(t.x.value('ProductOrigin[1]','varchar(20)') = '', null, t.x.value('ProductOrigin[1]','varchar(20)')),
			@MinimumStock = IIF(t.x.value('MinimumStock[1]','varchar(20)') = '', null, t.x.value('MinimumStock[1]','varchar(20)')),
			@MaximumStock = IIF(t.x.value('MaximumStock[1]','varchar(20)') = '', null, t.x.value('MaximumStock[1]','varchar(20)')),
			@CommissionPercentage = IIF(t.x.value('CommissionPercentage[1]','varchar(20)') = '', null, REPLACE(t.x.value('CommissionPercentage[1]','varchar(20)'), ',', '.')),
			@RepositionPoint = IIF(t.x.value('RepositionPoint[1]','varchar(20)') = '', null, t.x.value('RepositionPoint[1]','varchar(20)')),
			@ResetTime = IIF(t.x.value('ResetTime[1]','varchar(20)') = '', null, t.x.value('ResetTime[1]','varchar(20)')),
			@CurrencyType = IIF(t.x.value('CurrencyType[1]','varchar(20)') = '', null, t.x.value('CurrencyType[1]','varchar(20)')),
			@ProductCost = IIF(t.x.value('ProductCost[1]','varchar(20)') = '', null, REPLACE(t.x.value('ProductCost[1]','varchar(20)'), ',', '.')),
			@FinalProductCost = IIF(t.x.value('FinalProductCost[1]','varchar(20)') = '', null, REPLACE(t.x.value('FinalProductCost[1]','varchar(20)'), ',', '.')),
			@SellingPrice = IIF(t.x.value('SellingPrice[1]','varchar(20)') = '', null, REPLACE(t.x.value('SellingPrice[1]','varchar(20)'), ',', '.')),
			@AllPOSPathologies = IIF(t.x.value('AllPOSPathologies[1]','varchar(20)') = '', null, t.x.value('AllPOSPathologies[1]','varchar(20)')),
			@Status = t.x.value('Status[1]','bit'),
			@BillingGroupNoPosId = IIF(t.x.value('BillingGroupNoPosId[1]','varchar(20)') = '', null, t.x.value('BillingGroupNoPosId[1]','varchar(20)')),
			@ControlCostPercentage = IIF(t.x.value('ControlCostPercentage[1]','varchar(20)') = '', null, REPLACE(t.x.value('ControlCostPercentage[1]','varchar(20)'), ',', '.')),
			@InventoryRiskLevelId = IIF(t.x.value('InventoryRiskLevelId[1]','varchar(20)') = '', null, t.x.value('InventoryRiskLevelId[1]','varchar(20)')),
			@SerialNumber = IIF(t.x.value('SerialNumber[1]','varchar(40)') = '', null, t.x.value('SerialNumber[1]','varchar(40)')),
			@DriveUnit = IIF(t.x.value('DriveUnit[1]','varchar(20)') = '', null, t.x.value('DriveUnit[1]','varchar(20)')),
			@MinimumTemperature = IIF(t.x.value('MinimumTemperature[1]','varchar(20)') = '', null, t.x.value('MinimumTemperature[1]','varchar(20)')),
			@MaximumTemperature = IIF(t.x.value('MaximumTemperature[1]','varchar(20)') = '', null, t.x.value('MaximumTemperature[1]','varchar(20)')),
			@SanitaryRegistration = IIF(t.x.value('SanitaryRegistration[1]','varchar(20)') = '', null, t.x.value('SanitaryRegistration[1]','varchar(20)')),
			@Consumption = IIF(t.x.value('Consumption[1]','varchar(20)') = '', null, t.x.value('Consumption[1]','varchar(20)')),
			@JustificationSuppliesDispositives = IIF(t.x.value('JustificationSuppliesDispositives[1]','varchar(20)') = '', null, t.x.value('JustificationSuppliesDispositives[1]','varchar(20)')),
			@OsteosynthesisMaterial = IIF(t.x.value('OsteosynthesisMaterial[1]','varchar(20)') = '', null, t.x.value('OsteosynthesisMaterial[1]','varchar(20)')),
			@Abbreviation = IIF(t.x.value('Abbreviation[1]','varchar(20)') = '', null, dbo.DecodeXmlToText(t.x.value('Abbreviation[1]','varchar(20)'))),
			@SupplieId = IIF(t.x.value('SupplieId[1]','int') = 0, null, t.x.value('SupplieId[1]','int')),
			@IUM = IIF(t.x.value('IUM[1]','varchar(15)') = '', null, t.x.value('IUM[1]','varchar(15)')),
			@Storage = IIF(t.x.value('Storage[1]','int') = 0, null, t.x.value('Storage[1]','int')),
			@TaxedProduct = t.x.value('TaxedProduct[1]','varchar(20)'),
			@LiquidateSalesTaxes = t.x.value('LiquidateSalesTaxes[1]','BIT'),
			@SismedReport = t.x.value('SismedReport[1]','BIT'),
			@DairyComponent = t.x.value('DairyComponent[1]', 'BIT'),
			@DairyComponentType = IIF(t.x.value('DairyComponentType[1]','int') = 0, null, t.x.value('DairyComponentType[1]','int')),
			@MedicationTypeId =IIF(t.x.value('MedicationTypeId[1]','int') = 0, null, t.x.value('MedicationTypeId[1]','int')),
			@WeightParenteralNutritionSupply = IIF(t.x.value('WeightParenteralNutritionSupply[1]','varchar(20)') = '', null, REPLACE(t.x.value('WeightParenteralNutritionSupply[1]','varchar(20)'), ',', '.'))
		from @Xml.nodes('/InventoryProduct') t(x)

		--Se obtienen los atributos
		insert into @TableInventoryProductAttribute
		select 
			t.x.value('Id[1]','int') as Id,
			t.x.value('AttributeProductTypeId[1]','int') as AttributeProductTypeId,
			t.x.value('Value[1]','varchar(100)') as Value,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/InventoryProduct/InventoryProductAttribute') t(x)

		--Se obtienen los códigos de barra
		insert into @TableProductBarcode
		select 
			t.x.value('Id[1]','int') as Id,
			t.x.value('Barcode[1]','varchar(300)') as Barcode,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/InventoryProduct/ProductBarcode') t(x)

		--Se obtienen las patologías
		insert into @TablePathologies
		select 
		t.x.value('Id[1]','int') as Id,
		t.x.value('DiagnosticId[1]','int') as DiagnosticId,
		t.x.value('DiagnosticCode[1]','varchar(20)') as DiagnosticCode,		
		t.x.value('ProductId[1]','int') as ProductId,
		t.x.value('IsDelete[1]','bit') as IsDelete,
		t.x.value('MinimumAge[1]','tinyint') as MinimumAge,
		t.x.value('MaximumAge[1]','tinyint') as MaximumAge,
		t.x.value('AgeMeasure[1]','tinyint') as AgeMeasure
		from @Xml.nodes('/InventoryProduct/POSPathologies') t(x)
		/***************************************************************************************/
		--Insertar los datos de la jerarquia si las hay
		insert into @ProductHierarchy
		select 
			t.x.value('Id[1]','int') as Id,
			t.x.value('HierarchyProductFinalId[1]','int') as HierarchyProductFinalId,
			t.x.value('ProductId[1]','int') as ProductId,
			t.x.value('ParentProductId[1]','int') as ParentProductId,
			t.x.value('ConversionUnit[1]','BIGINT') as ConversionUnit,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/InventoryProduct/ProductHierarchy') t(x)

		DELETE ph
		FROM Inventory.ProductHierarchy ph WITH(NOLOCK)
		JOIN @ProductHierarchy p ON ph.Id= p.Id AND p.IsDelete =1

		DELETE @ProductHierarchy WHERE IsDelete =1

		UPDATE ph
		SET ph.HierarchyProductFinalId =P.HierarchyProductFinalId,
			ph.ProductId = P.ProductId,
			ph.ParentProductId = P.ParentProductId,
			ph.ConversionUnit = P.ConversionUnit
		FROM Inventory.ProductHierarchy ph WITH(NOLOCK)
		JOIN @ProductHierarchy p ON ph.Id= p.Id

		INSERT INTO Inventory.ProductHierarchy
		SELECT P.HierarchyProductFinalId,P.ProductId,P.ParentProductId,P.ConversionUnit
		FROM @ProductHierarchy p
		WHERE Id =0
		/***************************************************************************************/
		--Si hay códigos de barras para eliminar
		delete from Inventory.ProductBarcode where Id in (select Id from @TableProductBarcode where IsDelete = 1)
		delete from @TableProductBarcode where IsDelete = 1	

		--Si hay atributos para eliminar
		delete from Inventory.InventoryProductAttribute where Id in (select Id from @TableInventoryProductAttribute where IsDelete = 1)
		delete from @TableInventoryProductAttribute where IsDelete = 1

		--Se genera el código del prodcuto si viene por secuencia numérica		
		if @Code = '' or @Code is null
		BEGIN
			--Consultamos si la secuencia es con O o OU
			declare @scope varchar(5) = ''
			declare @idSequenceDetail int
			declare @pattern varchar(300)
			declare @NextS int
			select @scope = Scope from Inventory.InventorySequence where IdForm = '304'

			--Se valida el scope
			if @scope = 'O'
			Begin
				-- Consultamos la secuencia numerica del form de CxP
				select top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
				from Inventory.InventorySequenceDetail bsd 
				inner join Inventory.InventorySequence bs on bs.Id = bsd.InventorySequenceId
				inner join Common.Sequense cs on cs.Id = bsd.IdSequense
				where bs.IdForm = '304'
				order by bsd.Next desc
			End
			Else
			Begin
				-- Consultamos la secuencia numerica del form de CxP
				select @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
				from Inventory.InventorySequenceDetail bsd 
				inner join Inventory.InventorySequence bs on bs.Id = bsd.InventorySequenceId
				inner join Common.Sequense cs on cs.Id = bsd.IdSequense
				where bs.IdForm = '304' and bsd.IdOperatingUnit = @OperatingUnitId
			End

			if (@idSequenceDetail is null)
			Begin
				select 999 as CodeResult, 'Secuencia no encontrada para generar el producto' as MessageResult, '' as Code, 0 as Id
				return
			End

			select @Code = dbo.GetSequence('',@pattern,@NextS)
			update Inventory.InventorySequenceDetail set [Next] += 1 where Id = @idSequenceDetail
		end
		
		--Si el producto se va a incativar, se valida que no tenga cantidades en el inventario fisico
		if @Status = 0 and @Id is not null and @Id > 0
		begin
			--Se valida si hay cantidades en el inventario fisico
			if EXISTS(select 1 from Inventory.PhysicalInventory where ProductId = @Id and Quantity > 0)
			begin
				select 999 as CodeResult, 'El producto no se puede inactivar porque tiene cantidades en el inventario físico' as MessageResult, '' as Code, 0 as Id
				return
			end
		end

		IF NOT EXISTS (SELECT 1 FROM .SEGusuaru WHERE CODUSUARI = @UserCode)
		BEGIN
			select 999 as CodeResult, CONCAT('El usuario ', ISNULL(@UserCode, ''), ' no existe en Crystal') as MessageResult, '' as Code, 0 as Id
			return
		END
			
		--Si se va a guardar el producto
		if @Id = 0 or @Id is null
		begin
			INSERT INTO [Inventory].[InventoryProduct]([Code], [Name], [ProductTypeId], [ATCId], [CodeCUM], [CodeAlternative], [CodeAlternativeTwo], [Description], [ProductGroupId],
			[ProductSubGroupId], [MeasurementUnitId], [PackagingUnitId], [ManufacturerId], [Osmolarity], [IVAId], [Presentation], [CodeSICE], [HandlesSerial], [HandlesHealthRegistration],
			[HealthRegistration], [ExpirationDate], [BillingGroupId], [ProductControl], [ProductWithPriceControl], [POSProduct], [AuthorizationByOrderNumber], [ExpirationDay],
			[MaximumControlPeriod], [ControlDays], [ControlOrderQuantity], [ProductOrderAmount], [LastPurchase], [LastSale], [ProductOrigin], [MinimumStock], [MaximumStock], 
			[CommissionPercentage], [RepositionPoint], [ResetTime], [CurrencyType], [ProductCost], [FinalProductCost], [SellingPrice], [AllPOSPathologies], [Status], [CreationUser],[CreationDate],
			[BillingGroupNoPosId], [ControlCostPercentage], [InventoryRiskLevelId], [SerialNumber], [DriveUnit], [MinimumTemperature], [MaximumTemperature], [SanitaryRegistration], 
			Consumption, JustificationSuppliesDispositives, OsteosynthesisMaterial, Abbreviation, SupplieId, IUM, Storage, TaxedProduct,LiquidateSalesTaxes,SismedReport, DairyComponent, 
			DairyComponentType,MedicationTypeId, WeightParenteralNutritionSupply)
			VALUES(@Code, @Name, @ProductTypeId, @ATCId, @CodeCUM, @CodeAlternative, @CodeAlternativeTwo, @Description, @ProductGroupId, @ProductSubGroupId, @MeasurementUnitId, @PackagingUnitId, 
			@ManufacturerId, @Osmolarity, @IVAId, @Presentation, @CodeSICE, @HandlesSerial, @HandlesHealthRegistration, @HealthRegistration, @ExpirationDate, @BillingGroupId, @ProductControl, @ProductWithPriceControl, 
			@POSProduct, @AuthorizationByOrderNumber, @ExpirationDay, @MaximumControlPeriod, @ControlDays, @ControlOrderQuantity, @ProductOrderAmount, @LastPurchase, @LastSale, @ProductOrigin, 
			@MinimumStock, @MaximumStock, @CommissionPercentage, @RepositionPoint, @ResetTime, @CurrencyType, @ProductCost, @FinalProductCost, @SellingPrice, @AllPOSPathologies, @Status, 
			@UserCode, [Common].[GETDATE](), @BillingGroupNoPosId, @ControlCostPercentage, @InventoryRiskLevelId, @SerialNumber, @DriveUnit, @MinimumTemperature, @MaximumTemperature, 
			@SanitaryRegistration, @Consumption, @JustificationSuppliesDispositives, @OsteosynthesisMaterial, @Abbreviation, @SupplieId, @IUM, @Storage, @TaxedProduct,@LiquidateSalesTaxes,@SismedReport,
			@DairyComponent, @DairyComponentType,@MedicationTypeId, @WeightParenteralNutritionSupply)

			--Se obtiene el id generado
			set @Id = SCOPE_IDENTITY()
		end
		else begin --Si se va a actualizar
			UPDATE [Inventory].[InventoryProduct] SET [Code] = @Code, [Name] = @Name, [ProductTypeId] = @ProductTypeId, [ATCId] = @ATCId, [CodeCUM] = @CodeCUM, [CodeAlternative] = @CodeAlternative, 
			[CodeAlternativeTwo] = @CodeAlternativeTwo, [Description] = @Description, [ProductGroupId] = @ProductGroupId, [ProductSubGroupId] = @ProductSubGroupId, [MeasurementUnitId] = @MeasurementUnitId, 
			[PackagingUnitId] = @PackagingUnitId, [ManufacturerId] = @ManufacturerId, [Osmolarity] = @Osmolarity, [IVAId] = @IVAId, [Presentation] = @Presentation, [CodeSICE] = @CodeSICE, [HandlesSerial] = @HandlesSerial, 
			[HandlesHealthRegistration] = @HandlesHealthRegistration, [HealthRegistration] = @HealthRegistration, [ExpirationDate] = @ExpirationDate, [BillingGroupId] = @BillingGroupId, 
			[ProductControl] = @ProductControl, [ProductWithPriceControl] = @ProductWithPriceControl, [POSProduct] = @POSProduct, [AuthorizationByOrderNumber] = @AuthorizationByOrderNumber, 
			[ExpirationDay] = @ExpirationDay, [MaximumControlPeriod] = @MaximumControlPeriod, [ControlDays] = @ControlDays, [ControlOrderQuantity] = @ControlOrderQuantity, 
			[ProductOrderAmount] = ProductOrderAmount, [LastPurchase] = @LastPurchase, [LastSale] = @LastSale, [ProductOrigin] = @ProductOrigin, [MinimumStock] = @MinimumStock, [MaximumStock] = @MaximumStock, 
			[CommissionPercentage] = @CommissionPercentage, [RepositionPoint] = @RepositionPoint, [ResetTime] = @ResetTime, [CurrencyType] = @CurrencyType, [ProductCost] = @ProductCost, 
			[FinalProductCost] = @FinalProductCost, [SellingPrice] = @SellingPrice, [AllPOSPathologies] = @AllPOSPathologies, [Status] = @Status, [ModificationUser] = @UserCode, 
			[ModificationDate] = [Common].[GETDATE](), [BillingGroupNoPosId] = @BillingGroupNoPosId, [ControlCostPercentage] = @ControlCostPercentage, [InventoryRiskLevelId] = @InventoryRiskLevelId, 
			[SerialNumber] = @SerialNumber, [DriveUnit] = @DriveUnit, [MinimumTemperature] = @MinimumTemperature, [MaximumTemperature] = @MaximumTemperature, 
			[SanitaryRegistration] = @SanitaryRegistration, Consumption = @Consumption, JustificationSuppliesDispositives = @JustificationSuppliesDispositives, OsteosynthesisMaterial = @OsteosynthesisMaterial,
			Abbreviation = @Abbreviation, SupplieId = @SupplieId, IUM = @IUM, [Storage] = @Storage, [TaxedProduct] = @TaxedProduct,  [LiquidateSalesTaxes] = @LiquidateSalesTaxes,[SismedReport]= @SismedReport,
			[DairyComponent] = @DairyComponent, [DairyComponentType] = @DairyComponentType,[MedicationTypeId]=@MedicationTypeId, [WeightParenteralNutritionSupply] = @WeightParenteralNutritionSupply
			WHERE Id = @Id
		end

		--Si hay códigos de barras
		if EXISTS (select 1 from @TableProductBarcode where IsDelete = 0)
		begin
			--Se valida que no existan los codigos de barras previamente
			if EXISTS (select *
			from Inventory.InventoryProduct ip
			inner join Inventory.ProductBarcode pb on pb.ProductId = ip.Id
			inner join @TableProductBarcode t on t.Barcode = pb.Barcode
			where t.IsDelete = 0 and ip.Id <> @Id)
			begin
				set @Errors = ''
				select @Errors = stuff((select N'; El código de barras ' + pb.Barcode + ' ya existe para el producto ' + ip.Code + ' - ' + ip.Name
				from Inventory.InventoryProduct ip
				inner join Inventory.ProductBarcode pb on pb.ProductId = ip.Id
				inner join @TableProductBarcode t on t.Barcode = pb.Barcode
				where t.IsDelete = 0 and ip.Id <> @Id
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				select 999 as CodeResult, @Errors as MessageResult, '' as Code, 0 as Id
				return
			end

			--Se insertan los códigos de barras
			INSERT INTO [Inventory].[ProductBarcode]([ProductId], [CreationDate], [Barcode])
			select @Id, [Common].[GETDATE](), Barcode
			from @TableProductBarcode
			where IsDelete = 0
		end

		--Si hay atributos para actualizar
		if EXISTS(select 1 from @TableInventoryProductAttribute where Id > 0 and IsDelete = 0)
		begin
			update p set p.AttributeProductTypeId = temp.AttributeProductTypeId, p.Value = temp.Value
			from @TableInventoryProductAttribute temp
			inner join Inventory.InventoryProductAttribute p on p.Id = temp.Id
		end

		--Si hay atributos para insertar
		if EXISTS(select 1 from @TableInventoryProductAttribute where Id = 0 and IsDelete = 0)
		begin
			insert into Inventory.InventoryProductAttribute(ProductId, AttributeProductTypeId, Value)
			select @Id, AttributeProductTypeId, Value
			from @TableInventoryProductAttribute
			where Id = 0 and IsDelete = 0
		end

		--Si la clase del producto es diferente a Item Otro(4) y a Item Medicamento(2)
		if EXISTS (select 1 from Inventory.ProductType where Id = @ProductTypeId AND Class not in (4, 2))
		begin
			--Código para validar el producto en Crystal, ya sea sacado del ATC o del Producto
			declare @CodeCrystal varchar(20) = ''

			--Variables que se utilizan cuando el producto tiene ATC, de lo contrario va en null
			declare @CODDCIMED varchar(20) = NULL
			declare @CODGRUFAR varchar(20) = NULL
			declare @CODVIAADM varchar(20) = NULL
			declare @CONCENMED varchar(50) = NULL
			declare @CODFORMED varchar(20) = NULL
			declare @TIEESTMED int = NULL
			declare @TIPFORMED varchar(5) = NULL
			declare @PESTOTMED numeric(18, 2) = NULL
			declare @CODUNIPES varchar(20) = NULL
			declare @VOLTOTMED numeric(18, 2) = NULL
			declare @CODUNIVOL varchar(20) = NULL
			declare @CODUNIADM varchar(20) = NULL
			declare @ESPDILPRO bit = 0
			declare @ABRPROMEZ varchar(60) = NULL
			declare @RETRASOGE bit = 0
			declare @JUSINMEDI bit = 0
			declare @ADVERTENC varchar(max) = NULL
			declare @POSOLOGIA varchar(max) = NULL
			declare @MEDTRAZA bit = NULL

			--Si el producto en VIE tiene asociado un ATC
			if ISNULL(@ATCId, 0) > 0
			begin
				--Se obtiene el código de la tabla ATC
				select @CodeCrystal = Code, @CONCENMED = Concentration, @TIEESTMED = StabilityMaximumHours, @TIPFORMED = CAST(FormulationType as varchar(5)), @PESTOTMED = Weight,
				@VOLTOTMED = Volume, @ESPDILPRO = DiluentProduct, @ABRPROMEZ = SUBSTRING(AbbreviationName, 0, 60), @RETRASOGE = TransferSurplusProduct, @JUSINMEDI = ISNULL(JustificationOfInputs,0),
				@ADVERTENC = Warning, @POSOLOGIA = Dosage, @MEDTRAZA = IndicatorDrug
				from Inventory.ATC 
				where Id = @ATCId
								
				--Se valida que el DCI asignado en el ATC tenga una clase seleccionada
				if EXISTS (select 1
				from Inventory.ATC a
				inner join Inventory.DCI d on d.Id = a.DCIId
				where a.Id = @ATCId and d.DCICrystal is null)
				begin
					select 999 as CodeResult, 'El DCI asignado al ATC no tiene una clase seleccionada' as MessageResult, '' as Code, 0 as Id
					return
				end

				--Se obtiene el CODCIMED
				set @CODDCIMED = (select d.DCICrystal
								from Inventory.ATC a
								inner join Inventory.DCI d on d.Id = a.DCIId
								where a.Id = @ATCId)

				--Se valida que el grupo farmacologico asignado al atc tenga una clase seleccionada
				if EXISTS (select 1
				from Inventory.ATC a
				inner join Inventory.PharmacologicalGroup pg on pg.Id = a.PharmacologicalGroupId
				where a.Id = @ATCId and pg.CrystalPharmacologicalGroup is null)
				begin
					select 999 as CodeResult, 'El grupo farmacológico asignado al ATC no tiene una clase seleccionada' as MessageResult, '' as Code, 0 as Id
					return
				end

				--Se obtiene el CODGRUFAR
				set @CODGRUFAR = (select pg.CrystalPharmacologicalGroup
								from Inventory.ATC a
								inner join Inventory.PharmacologicalGroup pg on pg.Id = a.PharmacologicalGroupId
								where a.Id = @ATCId)

				--Se valida que la forma farmaceutica tenga una clase seleccionada
				if EXISTS (select 1
				from Inventory.ATC a
				--inner join Inventory.AdministrationRoute ar on ar.Id = a.AdministrationRouteId
				--inner join Inventory.PharmaceuticalForm pf on pf.Id = ar.PharmaceuticalFormId
				inner join Inventory.PharmaceuticalForm pf on pf.Id = a.PharmaceuticalFormId
				where a.Id = @ATCId and (pf.CrystalMedicalForm is null or pf.CrystalMedicalForm = '   '))
				begin
					--select 999 as CodeResult, 'La forma farmaceutica asociada a la vía de administración del ATC no tiene una clase seleccionada' as MessageResult, '' as Code, 0 as Id
					select 999 as CodeResult, 'La forma farmaceutica del ATC no tiene una clase seleccionada' as MessageResult, '' as Code, 0 as Id
					return
				end

				--Se obtiene CODFORMED
				set @CODFORMED = (select pf.CrystalMedicalForm
								from Inventory.ATC a
								--inner join Inventory.AdministrationRoute ar on ar.Id = a.AdministrationRouteId
								--inner join Inventory.PharmaceuticalForm pf on pf.Id = ar.PharmaceuticalFormId
								inner join Inventory.PharmaceuticalForm pf on pf.Id = a.PharmaceuticalFormId
								where a.Id = @ATCId)

				--Se valida que la via de administracion del ATC tenga una clase seleccionada
				if EXISTS (select 1
				from Inventory.ATC a
				inner join inventory.ATCAdministrationRoute ATCAR
				ON ATCAR.ATCId = a.Id
				--inner join Inventory.AdministrationRoute ar on ar.Id = a.AdministrationRouteId
				inner join Inventory.AdministrationRoute ar on ar.Id = ATCAR.AdministrationRouteId
				where a.Id = @ATCId and ar.CrystalAdministrationRoute is null)
				begin
					--select 999 as CodeResult, 'La vía de administración del ATC no tiene una clase seleccionada' as MessageResult, '' as Code, 0 as Id
					set @errors =  ''
					SELECT @errors = stuff((SELECT DISTINCT N', ' + ar.Code + ''
					FROM Inventory.ATC a
					inner join inventory.ATCAdministrationRoute ATCAR
					ON ATCAR.ATCId = a.Id
					inner join Inventory.AdministrationRoute ar on ar.Id = ATCAR.AdministrationRouteId
					where a.Id = @ATCId and ar.CrystalAdministrationRoute is null
					for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
					select 999 as CodeResult, 'Las siguientes vías de administración del ATC no tiene una clase seleccionada: ' + @errors as MessageResult, '' as Code, 0 as Id
					return
				end

				--Se obtiene CODVIAADM
				set @CODVIAADM = (select ar.CrystalAdministrationRoute
								from Inventory.ATC a
								inner join Inventory.AdministrationRoute ar on ar.Id = a.AdministrationRouteId
								where a.Id = @ATCId)

				--Se valida que la unidad de medida para la via de administracion tenga una clase seleccionada
				if EXISTS (select 1
				from Inventory.ATC a
				inner join Inventory.InventoryMeasurementUnit m on m.Id = a.AdministrationUnitId
				where a.Id = @ATCId and m.CrystalMeasurementUnit is null and a.AdministrationUnitId is not null)
				begin
					select 999 as CodeResult, 'La unidad de medida para unidad de administración del ATC no tiene una clase seleccionada' as MessageResult, '' as Code, 0 as Id
					return
				end

				set @CODUNIADM = (select m.CrystalMeasurementUnit
								from Inventory.ATC a
								inner join Inventory.InventoryMeasurementUnit m on m.Id = a.AdministrationUnitId
								where a.Id = @ATCId and a.AdministrationUnitId is not null)

				--Se valida que la unidad de medida para volumen tenga una clase seleccionada
				if EXISTS (select 1
				from Inventory.ATC a
				inner join Inventory.InventoryMeasurementUnit m on m.Id = a.VolumeMeasureUnit
				where a.Id = @ATCId and m.CrystalMeasurementUnit is null and a.VolumeMeasureUnit is not null)
				begin
					select 999 as CodeResult, 'La unidad de medida para volumen del ATC no tiene una clase seleccionada' as MessageResult, '' as Code, 0 as Id
					return
				end
				
				set @CODUNIVOL = (select m.CrystalMeasurementUnit
								from Inventory.ATC a
								inner join Inventory.InventoryMeasurementUnit m on m.Id = a.VolumeMeasureUnit
								where a.Id = @ATCId and a.VolumeMeasureUnit is not null)

				--Se valida que la unidad de medida para peso tenga una clase seleccionada
				if EXISTS (select 1
				from Inventory.ATC a
				inner join Inventory.InventoryMeasurementUnit m on m.Id = a.WeightMeasureUnit
				where a.Id = @ATCId and m.CrystalMeasurementUnit is null and a.WeightMeasureUnit is not null)
				begin
					select 999 as CodeResult, 'La unidad de medida para peso del ATC no tiene una clase seleccionada' as MessageResult, '' as Code, 0 as Id
					return
				end

				--Se obtiene CODUNIPES
				set @CODUNIPES = (select m.CrystalMeasurementUnit
								from Inventory.ATC a
								inner join Inventory.InventoryMeasurementUnit m on m.Id = a.WeightMeasureUnit
								where a.Id = @ATCId and a.WeightMeasureUnit is not null)

				UPDATE ip
					SET ip.ProductControl = @ProductControl
				FROM Inventory.InventoryProduct ip
				WHERE ip.ATCId = @ATCId
			end
			else begin --Si no tiene asociado un ATC 
				--Se obtiene el código de InventoryProduct
				set @CodeCrystal = @Code
			end

			if ISNULL(@SupplieId, 0) > 0 --INSUMO
			begin
				--Se obtiene el código de la tabla ATC
				select @CodeCrystal = Code
				from Inventory.InventorySupplie 
				where Id = @SupplieId
			END

			--Se valida si se actualiza el producto en Crystal
			--if EXISTS (select 1 from .IHLISTPRO where CODPRODUC = @CodeCrystal)
			--begin
			--	UPDATE [dbo].[IHLISTPRO] SET [CODPRODUC] = @CodeCrystal, [CODDCIMED] = @CODDCIMED, [DESPRODUC] = @Name, [NOPOSPROD] = IIF(@POSProduct = 1, 0, 1), 
			--	[TIPPRODUC] = IIF(@ATCId is null, '2', '1'), [MANCONPRO] = 0, [REGINVACT] = @HandlesHealthRegistration, [REGINVIMA] = IIF(@HandlesHealthRegistration = 1, @HealthRegistration, ''), 
			--	[CODGRUFAR] = @CODGRUFAR, [CODVIAADM] = @CODVIAADM, [CONCENMED] = @CONCENMED, [PRESENMED] = @Presentation, [CODFORMED] = @CODFORMED, [TIEESTMED] = @TIEESTMED, 
			--	[TIPFORMED] = @TIPFORMED, [PESTOTMED] = @PESTOTMED, [CODUNIPES] = @CODUNIPES, [VOLTOTMED] = @VOLTOTMED, [CODUNIVOL] = @CODUNIVOL, [CODUNIADM] = @CODUNIADM, 
			--	[CALCANAUT] = 0, [ESPDILPRO] = @ESPDILPRO, [ABRPROMEZ] = @ABRPROMEZ, [PROESTADO] = @Status, [PROCONTRO] = IIF(@ATCId is not null, @ProductControl, 1), [TODASPATO] = ISNULL(@AllPOSPathologies, 0), 
			--	[MANLOCALI] = 0, [RETRASOGE] = @RETRASOGE, [CODUSUMOD] = @UserCode, [FECUSUMOD] = [Common].[GETDATE](), [JUSINMEDI] = ISNULL(@JustificationSuppliesDispositives,0), [CODJUMEES] = 0, 
			--	[ADVERTENC] = @ADVERTENC, [POSOLOGIA] = @POSOLOGIA, [MEDTRAZA] = @MEDTRAZA, CONSUMPTION = @Consumption, MATOSTOSIN = @OsteosynthesisMaterial
			--	WHERE CODPRODUC = @CodeCrystal
			--end
			--else begin --Si se inserta el producto en Crystal
			--	INSERT INTO [dbo].[IHLISTPRO]([CODPRODUC], [CODDCIMED], [DESPRODUC], [NOPOSPROD], [TIPPRODUC], [MANCONPRO], [REGINVACT], [REGINVIMA], [CODGRUFAR], [CODVIAADM], [CONCENMED], [PRESENMED],
			--	[CODFORMED], [TIEESTMED], [TIPFORMED], [PESTOTMED], [CODUNIPES], [VOLTOTMED], [CODUNIVOL], [CODUNIADM], [CALCANAUT], [ESPDILPRO], [ABRPROMEZ], [PROESTADO], [PROCONTRO], [TODASPATO], 
			--	[MANLOCALI], [RETRASOGE], [CODUSUCRE], [FECUSUCRE], [JUSINMEDI], [CODJUMEES], [ADVERTENC], [POSOLOGIA], [MEDTRAZA], CONSUMPTION, MATOSTOSIN)
			--	 VALUES(@CodeCrystal, @CODDCIMED, @Name, IIF(@POSProduct = 1, 0, 1), IIF(@ATCId is null, '2', '1'), 0, @HandlesHealthRegistration, IIF(@HandlesHealthRegistration = 1, @HealthRegistration, ''),
			--	 @CODGRUFAR, @CODVIAADM, @CONCENMED, @Presentation, @CODFORMED, @TIEESTMED, @TIPFORMED, @PESTOTMED, @CODUNIPES, @VOLTOTMED, @CODUNIVOL, @CODUNIADM, 0, @ESPDILPRO, @ABRPROMEZ,
			--	 @Status, IIF(@ATCId is not null, @ProductControl, 1), ISNULL(@AllPOSPathologies, 0), 0, @RETRASOGE, @UserCode, [Common].[GETDATE](), ISNULL(@JustificationSuppliesDispositives,0), 0, @ADVERTENC, 
			--	 @POSOLOGIA, @MEDTRAZA, @Consumption, @OsteosynthesisMaterial)
			--end
			
			--Si el medicamento es pos se realiza la lógica de las patologías
			if @POSProduct = 1 and ISNULL(@AllPOSPathologies, 0) = 0
			begin
				--Si hay patologías para guardar
				if(select count(*) from @TablePathologies where Id = 0 and IsDelete = 0) > 0
				begin
					--Se guarda en VIE
					insert into Inventory.POSPathologies(ProductId, DiagnosticId, MedicamentId, MinimumAge, MaximumAge, AgeMeasure)
					select @Id, DiagnosticId, NULL, MinimumAge, MaximumAge, AgeMeasure
					from @TablePathologies
					where Id = 0 and IsDelete = 0

					--Se guarda en Crystal
					insert into .INPRODPAT(IPRCODIGO, CODDIAGNO, MinimumAge, MaximumAge, AgeMeasure)
					select @CodeCrystal, DiagnosticCode, MinimumAge, MaximumAge, AgeMeasure
					from @TablePathologies
					where Id = 0 and IsDelete = 0
				end

				--Si hay patologías para eliminar
				if(select count(*) from @TablePathologies where IsDelete = 1) > 0
				begin
					--Se eliminan de VIE
					delete from Inventory.POSPathologies where Id in (select Id from @TablePathologies where IsDelete = 1)

					--Se eliminan de Crystal
					--delete from .INPRODPAT where IPRCODIGO = @CodeCrystal and CODDIAGNO in (select DiagnosticCode from @TablePathologies where IsDelete = 1)
					delete from .INPRODPAT
					FROM @TablePathologies P
					where
					P.IsDelete = 1 
					AND INPRODPAT.IPRCODIGO = @CodeCrystal
					AND INPRODPAT.CODDIAGNO = P.DiagnosticCode
					AND INPRODPAT.MinimumAge = P.MinimumAge
					AND INPRODPAT.MaximumAge = P.MaximumAge
					AND INPRODPAT.AgeMeasure = P.AgeMeasure
				end
			end
			else begin --Si el medicamento es no pos se eliminan todas las patologias asociadas o si el medicamento maneja todas las patologias
				--Se eliminan de VIE
				delete from Inventory.POSPathologies where ProductId = @Id

				--Se eliminan de Crystal
				delete from .INPRODPAT where IPRCODIGO = @CodeCrystal
			end
		end
		else if EXISTS (select 1 from Inventory.ProductType where Id = @ProductTypeId AND Class in (2)) begin --Si es medicamento se buscan todos los hijos y si alguno es controlado se actualiza el papa
			declare @ProControlTemp bit = 0
			if exists(select 1 from Inventory.InventoryProduct ip where ip.ATCId = @ATCId and ip.ProductControl = 1)
			begin
				set @ProControlTemp = 1
			end
			
			update i set i.PROCONTRO = @ProControlTemp
			from Inventory.ATC m
			inner join .IHLISTPRO i on i.CODPRODUC = m.Code
			where m.Id = @ATCId
		end

		--Retorna el ok
		select 0 as CodeResult, 'Se guardó correctamente' as MessageResult, @Code as Code, @Id as Id
		return

	end try
	begin catch

		--Retorna el error
		select 999 as CodeResult, ERROR_MESSAGE() as MessageResult, '' as Code, 0 as Id
		return

	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda o actualiza un producto del inventario a partir de un mensaje XML con todos sus datos maestros: código, nombre, tipo, clasificación ATC, código CUM, unidad de medida, empaque, fabricante, IVA, precio de costo y venta, control sanitario, registro de salud, parámetros de stock mínimo y máximo, temperaturas de almacenamiento, entre otros. Además procesa y sincroniza las colecciones relacionadas al producto: códigos de barras (ProductBarcode), jerarquías de presentación como caja-blíster-unidad (ProductHierarchy) y atributos adicionales configurables (InventoryProductAttribute), aplicando inserciones, actualizaciones o eliminaciones lógicas según corresponda. Se usa desde el módulo de inventario para el mantenimiento del catálogo de productos farmacéuticos, insumos y dispositivos médicos, incluyendo información para SISMED, componentes lácteos, tipo de medicamento y nutrición parenteral.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveInventoryProduct';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveInventoryProduct';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Crea o actualiza un producto del inventario (medicamentos, insumos, dispositivos) junto con sus códigos de barra, atributos, jerarquía, patologías POS e integración con el sistema externo Crystal, validando reglas de negocio asociadas al ATC.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryProduct';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener un nodo /InventoryProduct con los datos del producto.; El usuario indicado debe existir en la tabla SEGusuaru (Crystal); de lo contrario se aborta con código 999.; Si se inactiva un producto existente (Status=0 y Id>0), el producto no debe tener cantidades en Inventory.PhysicalInventory (Quantity>0).; Si el código del producto se genera automáticamente, debe existir una secuencia configurada para IdForm=''304'' (y según el Scope, además para la unidad operativa).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryProduct';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.DecodeXmlToText; Common.GETDATE; dbo.GetSequence', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryProduct';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventorySequence; Inventory.InventorySequenceDetail; Common.Sequense; Inventory.PhysicalInventory; Inventory.InventoryProduct; Inventory.ProductBarcode; Inventory.ProductType; Inventory.ATC; Inventory.DCI; Inventory.PharmacologicalGroup; Inventory.PharmaceuticalForm; Inventory.ATCAdministrationRoute; Inventory.AdministrationRoute; Inventory.InventoryMeasurementUnit; Inventory.InventorySupplie; Inventory.ProductHierarchy; Inventory.InventoryProductAttribute; Inventory.POSPathologies; SEGusuaru; IHLISTPRO; INPRODPAT', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryProduct';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryProduct';
-- GO
