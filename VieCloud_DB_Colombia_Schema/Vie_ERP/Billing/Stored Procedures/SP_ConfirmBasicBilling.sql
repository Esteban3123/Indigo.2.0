
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-11-19
-- Description:	Procedimiento que se encarga de confirmar la factura basica
-- =============================================
CREATE PROCEDURE [Billing].[SP_ConfirmBasicBilling] 
    @Id AS INT,
	@CodeUser AS VARCHAR(20),
	----------------------------------------------
	@CashReceiptsXml AS XML,
	@CompanyType INT
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables
	DECLARE @Code VARCHAR(20),			
			@OperatingUnitId INT,			
			@FunctionalUnitId INT,
			@WarehouseId INT,	
			@DocumentDate DATETIME,
			-------------------------
			@InvoiceId INT, 
			@InvoiceNumber VARCHAR(15),
			@AccountReceivableId INT,
			@Consecutive  VARCHAR(MAX),
			@CurrencyId INT,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@IdCashReceipt INT
	-------------------------------------------- VARIABLES DE XML --------------------------------------------
	--Tabla errores
	DECLARE @table_errors TABLE
	(
		Message VARCHAR(MAX)
	)

	--tabla para la cabecera del comprobante contable de reconocimiento
	declare @JournalVouchers as table
	(
		[Id] [int] NOT NULL,
		[AccountingMovementId] [int] NOT NULL,
		[Consecutive] [bigint] NOT NULL,
		[LegalBookId] [int] NOT NULL ,
		[IdJournalVoucher] [int] NOT NULL,
		[VoucherDate] [datetime] NOT NULL,
		[Imported] [bit] NOT NULL,
		[Status] [tinyint] NOT NULL,
		[Detail] [varchar](MAX) NULL ,
		[EntityCode] [varchar](20) NULL,
		[EntityId] [int] NULL,
		[EntityName] [varchar](250) NULL,
		[IsClosedYear] [bit] NOT NULL,
		[CurrencyId] [int],
		[CreationUser] [varchar](20) NOT NULL,
		[CreationDate] [datetime] NOT NULL,
		[ModificationUser] [varchar](20) NULL,
		[ModificationDate] [datetime] NULL,
		[ConfirmationUser] [varchar](20) NULL,
		[ConfirmationDate] [datetime] NULL
	)
	--detalles del comprobante contable
	declare @JournalVoucherDetails as table
	(
		[Id] [int] NOT NULL,
		[IdAccounting] [int] NOT NULL,
		[IdMainAccount] [int] NOT NULL,
		[IdThirdParty] [int] NULL,
		[IdCostCenter] [int] NULL,
		[DebitValue] [decimal](18, 2) NOT NULL ,
		[CreditValue] [decimal](18, 2) NOT NULL ,
		[Detail] [varchar](max) NULL ,
		[IdRetention] [int] NULL,
		[RetentionRate] [decimal](5, 3) NULL,
		[BaseValue] [decimal](18, 2) NULL ,
		[BillingValue] [decimal](18, 2) NULL
	)

	declare @ConsignmentProductCostList as table 
	(
		[EntityDetailId] [int] NOT NULL,
		[InvoiceWithCostList] [bit] NULL
	)

	BEGIN TRY

		SELECT	@Code = bb.Code,
				@OperatingUnitId = bb.OperatingUnitId,
				@FunctionalUnitId = bb.FunctionalUnitId,
				@WarehouseId = bb.WarehouseId,
				@DocumentDate = [Common].[GETDATE](),
				@CurrencyId = bb.CurrencyId
		FROM Billing.BasicBilling bb
		WHERE bb.Id = @Id
		
		DECLARE @Detail as VARCHAR(MAX) = 'Reconocimiento de venta de producto en consignacion Factura No. ' + @Code

		/*************************************************** VALIDACIONES Y PROCESOS POR TIPO DE DETALLE ***************************************************/

		--Verifica el estado

		IF EXISTS (SELECT 1 FROM Billing.BasicBilling bb WHERE bb.Id = @Id AND bb.Status <> 1)
		BEGIN
			SELECT 999 as CodeMessage, 'El registro se encuentra en estado: ' + IIF(bb.Status = 2, 'Confirmado', 'Anulado') as Message, 0 AS InvoiceId, '' AS InvoiceNumber, 0 as AccountReceivableId
			FROM Billing.BasicBilling bb
			WHERE bb.Id = @Id
			RETURN
		END

		IF NOT EXISTS (SELECT 1 FROM GeneralLedger.VieBot vb WHERE vb.Form = 'BasicBilling' AND vb.Allow = 1)
		BEGIN
			SELECT 999 as CodeMessage, 'Debe parametrizar al menos un libro contable' as Message, 0 AS InvoiceId, '' AS InvoiceNumber, 0 as AccountReceivableId
			RETURN
		END

	
	-- Validaciones de IVA con tabla temporal para evitar consultas duplicadas
	DECLARE @IVA_Validation_Temp TABLE (
		DetailType TINYINT,
		ItemCode VARCHAR(100),
		PercentageIVA DECIMAL(5,2),
		IVAPercentage DECIMAL(5,2),
		MainAccountId INT,
		DetailTypeDescription VARCHAR(50)
	)

	INSERT INTO @IVA_Validation_Temp
	SELECT 
		bbd.DetailType,
		COALESCE(ip.Code, bc.Code, fapa.Plate, fai.Code) AS ItemCode,
		bbd.PercentageIVA,
		ISNULL(iva.Percentage, 0) AS IVAPercentage,
		ma.Id AS MainAccountId,
		CASE bbd.DetailType 
			WHEN 1 THEN 'Producto ' 
			WHEN 2 THEN 'Servicio ' 
			WHEN 3 THEN 'Activo Fijo ' 
			WHEN 4 THEN 'Parte de Activo Fijo ' 
		END AS DetailTypeDescription
	FROM Billing.BasicBillingDetail bbd
	LEFT JOIN Inventory.InventoryProduct ip ON bbd.DetailType = 1 AND bbd.ProductId = ip.Id
	LEFT JOIN Billing.BillingConcept bc ON bbd.DetailType = 2 AND bbd.BillingConceptId = bc.Id
	LEFT JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON bbd.DetailType = 3 AND bbd.PhysicalAssetId = fapa.Id
	LEFT JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
	LEFT JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) ON (bbd.DetailType = 1 AND ip.IVAId=iva.Id) OR
															(bbd.DetailType=2 AND bc.IVAId =iva.Id) OR
															(bbd.DetailType=3 AND iva.Id=fai.IVAId)
	LEFT JOIN GeneralLedger.MainAccounts ma ON ma.Id = iva.IdAccountSale
	WHERE bbd.BasicBillingId = @Id AND bbd.PercentageIVA > 0

	-- Validación de cambio en porcentaje de IVA
	INSERT INTO @table_errors 
		SELECT CONCAT('El porcentaje del IVA del ', DetailTypeDescription, ItemCode, ' ha cambiado en la parametrización del item')
		FROM @IVA_Validation_Temp
		WHERE PercentageIVA <> IVAPercentage
	
	-- Validación de cuenta contable no parametrizada
	INSERT INTO @table_errors 
		SELECT CONCAT('La cuenta contable para el IVA del ', DetailTypeDescription, ItemCode, ' no se encuentra parametrizada')
		FROM @IVA_Validation_Temp
		WHERE MainAccountId IS NULL
		-- Verifica Si tiene productos
		IF EXISTS 
		(
			SELECT 1
			FROM Billing.BasicBilling bb
			JOIN Billing.BasicBillingDetail bbd ON bb.Id = bbd.BasicBillingId
			WHERE bb.Id = @Id AND bbd.DetailType = 1
		)
		BEGIN		
			DECLARE @WarehouseConsignment BIT,
					@EntityDetailId INT,
					@ProductId INT,
					@MovementType INT = 2,					
					@BatchSerialId INT,
					@Quantity INT,
					@Value DECIMAL(18,2),
					@WarehouseIds INT,
					@AffectAverageCost INT = 0
					
			DECLARE cursor_inventory CURSOR LOCAL FOR 
			
				SELECT
					ISNULL(bbdi.Id, bbd.Id) EntityDetailId,
					bbd.ProductId As ProductId, 		
					p.BatchSerialId As BatchSerialId, 
					ISNULL(bbdi.Quantity, bbd.Quantity) As Quantity, 
					ip.ProductCost As Value,
					w.Id as WarehouseIds,
					w.WarehouseConsignment
				FROM Billing.BasicBillingDetail bbd
				JOIN Inventory.InventoryProduct ip ON bbd.ProductId = ip.Id
				LEFT JOIN Billing.BasicBillingDetailItem bbdi ON bbd.Id = bbdi.BasicBillingDetailId
				LEFT JOIN Inventory.PhysicalInventory p ON bbdi.PhysicalInventoryId = p.Id
				LEFT JOIN Inventory.Warehouse W ON W.Id = bbd.WarehouseId
				WHERE bbd.BasicBillingId = @Id AND bbd.DetailType = 1
		
			OPEN cursor_inventory

			FETCH NEXT FROM cursor_inventory INTO @EntityDetailId,
					@ProductId,
					@BatchSerialId,
					@Quantity,
					@Value,
					@WarehouseIds,
					@WarehouseConsignment

			WHILE @@fetch_status = 0
			BEGIN 

			IF EXISTS 
				(
					SELECT 1 FROM Billing.BasicBillingDetail bbd
					JOIN Billing.BasicBillingDetailItem bbdi with(Nolock) ON bbdi.BasicBillingDetailId = bbd.Id
					JOIN Inventory.Warehouse wh WITH (NOLOCK) ON wh.Id = bbd.WarehouseId
					JOIN Common.Supplier s WITH (NOLOCK) ON s.Id = wh.SupplierId
					WHERE s.ConsignmentInventoryCosting = 1 AND bbdi.Id = @EntityDetailId AND @WarehouseConsignment=1
				)
				
				BEGIN
						IF NOT EXISTS
							(
								SELECT 1 
								FROM Billing.BasicBillingDetail bbd with(Nolock)
								JOIN  Billing.BasicBilling bb with(NOLOCK)on bbd.BasicBillingId =bb.id
								JOIN Billing.BasicBillingDetailItem bbdi with(Nolock) on bbdi.BasicBillingDetailId = bbd.Id
								JOIN Inventory.Warehouse wh with(nolock)on  wh.Id = bbd.WarehouseId
								JOIN Common.Supplier s with(nolock)on s.Id = wh.SupplierId
								JOIN Inventory.ConsignmentCostList ccl with(nolock) on ccl.SupplierId=s.Id
								JOIN Inventory.ConsignmentCostListDetail ccld  WITH (NOLOCK) ON ccld.ConsignmentCostListId = ccl.Id
								WHERE ccl.SupplierId = wh.SupplierId and bbdi.Id=@EntityDetailId
								AND ccld.ProductId = bbd.ProductId AND CAST(bb.DocumentDate AS DATE) <= CAST(ccl.EffectiveDate AS DATE) AND ccl.OperatingUnitId = @OperatingUnitId
							)							
								BEGIN

									DECLARE @SupplierCode AS VARCHAR(MAX)
									DECLARE @ProductCode AS VARCHAR(MAX)
									SET @SupplierCode = 
											(
												SELECT TOP 1 s.Code FROM Billing.BasicBilling
												JOIN Billing.BasicBillingDetail bbd WITH (NOLOCK) ON bbd.ProductId = @ProductId
												JOIN Billing.BasicBillingDetailItem bbdi with(Nolock) ON bbdi.BasicBillingDetailId = bbd.Id
												JOIN Inventory.Warehouse wh WITH (NOLOCK) ON wh.Id = bbd.WarehouseId
												JOIN Common.Supplier s WITH (NOLOCK) ON s.Id = wh.SupplierId
												WHERE bbdi.Id = @EntityDetailId 
											)										
									SET @ProductCode = 
											(
												SELECT TOP 1 ip.Code FROM Billing.BasicBilling bb
												JOIN Billing.BasicBillingDetail bbd ON bbd.BasicBillingId = bb.Id
												JOIN Billing.BasicBillingDetailItem bbdi with(Nolock) ON bbdi.BasicBillingDetailId = bbd.Id
												JOIN Inventory.InventoryProduct ip ON ip.Id = bbd.ProductId
												WHERE bbdi.Id = @EntityDetailId
											)
										
									INSERT INTO @table_errors VALUES ('Producto ' + @ProductCode + ' no se encuentra en la lista de costos vigentes en consignación del proveedor con codigo '+ @SupplierCode)
									GOTO cursor_inventory_end
								END
							ELSE
								BEGIN
									INSERT INTO @ConsignmentProductCostList VALUES (@EntityDetailId, 1)
								END
					END

				--Generamos el XML para consumir el SP encargado del movimiento del kardex
				SELECT @SubXml = CONVERT
				(
					XML, 
					(
						SELECT Kardex.*
						FROM 
						( 
							SELECT
								@ProductId ProductId,
								@MovementType MovementType,
								@WarehouseIds WarehouseId,
								@BatchSerialId BatchSerialId,
								@Quantity Quantity,
								@DocumentDate DocumentDate,
								@Value Value,
								@AffectAverageCost AffectAverageCost
						) Kardex
						For XML AUTO,TYPE, ELEMENTS
					)
				)
				
				EXEC Inventory.SP_SavePhysicalInventoryKardex_Output @SubXml, @Id, @Code, 'BasicBilling', @CodeUser, 1, @Code_Output OUT, @Message_Output OUT
				
				IF @Code_Output <> 0
				BEGIN
			
					INSERT INTO @table_errors 
						SELECT ISNULL(@Message_Output, 'No se pudo afectar el kardex')
					GOTO cursor_inventory_end
				END
				
				IF @WarehouseConsignment = 1
				BEGIN

					DECLARE @ProductCost as DECIMAL(18,2)
					DECLARE @ThirdPartyId as INT
					DECLARE @CostCenterId as INT
					DECLARE @CounterpartCostConsignedInventoryId as INT

					IF EXISTS 
					(
						SELECT 1 FROM Billing.BasicBillingDetail bbd
						JOIN Billing.BasicBillingDetailItem bbdi ON bbdi.BasicBillingDetailId = bbd.Id
						JOIN Inventory.Warehouse wh ON wh.Id = bbd.WarehouseId
						JOIN Common.Supplier s ON s.Id = wh.SupplierId
						WHERE s.ConsignmentInventoryCosting = 1 AND bbdi.Id = @EntityDetailId AND @WarehouseConsignment=1
					) BEGIN

							IF EXISTS 
								(
									SELECT 1 FROM @ConsignmentProductCostList cpcl
									WHERE cpcl.EntityDetailId = @EntityDetailId 
									AND cpcl.InvoiceWithCostList = 1 
								)
							BEGIN
								SET @ProductCost =
									(
											SELECT ccld.CostNew 
											FROM Billing.BasicBillingDetail bbd 
											JOIN Billing.BasicBillingDetailItem bbdi on bbdi.BasicBillingDetailId = bbd.Id
											JOIN Inventory.Warehouse wh ON wh.Id = bbd.WarehouseId
											JOIN Inventory.ConsignmentCostList ccl ON ccl.SupplierId = wh.SupplierId
											JOIN Inventory.ConsignmentCostListDetail ccld with(Nolock) ON ccld.ConsignmentCostListId = ccl.Id
											WHERE bbdi.Id = @EntityDetailId AND bbd.ProductId = ccld.ProductId AND ccl.OperatingUnitId = @OperatingUnitId
									)
							END

					END
					set @Message_Output =''
					---------------------------------------------CONTABILIZACION RECONOCIMIENTO DE VENTA EN PRODUCTOS---------------------------
				
					--se crea xml para crear movimiento contable de reconocimiento del producto 
					--se crea cuenta contable CXP Provisional
					--se saca la cuenta contrapartida al costo de inventario en consignacion y el valor promedio del producto
					
					--select 
					--	@ThirdPartyId = c.ThirdPartyId,
					--	@CostCenterId = fu.CostCenterId
					--from Billing.BasicBilling bb
					--join Billing.BasicBillingDetail bbd on bbd.BasicBillingId = bb.Id
					--join Common.Customer c on c.Id = bb.CustomerId
					--join Payroll.FunctionalUnit fu on fu.Id = bbd.FunctionalUnitId
					--where bb.Id = @Id

							-- Primero obtener el centro de costo
				SELECT @CostCenterId = fu.CostCenterId
				FROM Billing.BasicBilling bb
				JOIN Billing.BasicBillingDetail bbd ON bbd.BasicBillingId = bb.Id
				JOIN Payroll.FunctionalUnit fu ON fu.Id = bbd.FunctionalUnitId
				WHERE bb.Id = @Id
	
				-- Obtener el tercero del PROVEEDOR del almacén de consignación
				DECLARE @SupplierThirdPartyId INT
	
				SELECT @SupplierThirdPartyId = s.IdThirdParty
				FROM Inventory.Warehouse wh
				JOIN Common.Supplier s ON s.Id = wh.SupplierId
				WHERE wh.Id = @WarehouseIds
				  AND wh.WarehouseConsignment = 1  -- Solo para consignación
	
				-- Si encontramos un proveedor (es consignación), usar su tercero
				-- Si no (inventario normal), usar el tercero del cliente
				IF @SupplierThirdPartyId IS NOT NULL
				BEGIN
					-- Es consignación: usar tercero del PROVEEDOR
					SET @ThirdPartyId = @SupplierThirdPartyId
				END
				ELSE
				BEGIN
					-- NO es consignación: usar tercero del CLIENTE (comportamiento original)
					SELECT @ThirdPartyId = c.ThirdPartyId
					FROM Billing.BasicBilling bb
					JOIN Common.Customer c ON c.Id = bb.CustomerId
					WHERE bb.Id = @Id
				END

				IF @ProductCost IS NULL BEGIN
					SELECT @ProductCost = ip.ProductCost
					FROM Inventory.InventoryProduct ip
					WHERE ip.Id = @ProductId
				END

				-- Variables para las cuentas contables del ProductGroup
				DECLARE @InventoryCostMainAccountId as INT
				DECLARE @ConsignmentMerchandiseDebitAccountId AS INT
				DECLARE @ConsignmentMerchandiseCreditAccountId AS INT

				-- Consolidar consultas a ProductGroup en una sola
				SELECT
					@CounterpartCostConsignedInventoryId = pg.CounterpartCostConsignedInventoryId,
					@InventoryCostMainAccountId = pg.InventoryCostMainAccountId,
					@ConsignmentMerchandiseDebitAccountId = pg.ConsignmentMerchandiseDebitAccountId,
					@ConsignmentMerchandiseCreditAccountId = pg.ConsignmentMerchandiseCreditAccountId
				FROM Inventory.InventoryProduct ip
				JOIN Inventory.ProductGroup pg ON pg.Id = ip.ProductGroupId
				WHERE ip.Id = @ProductId

				-- Variables para validar si las cuentas manejan terceros y centros de costo
				DECLARE @CounterpartHandlesThirdParty BIT, @CounterpartHandlesCostCenter BIT
				DECLARE @InventoryCostHandlesThirdParty BIT, @InventoryCostHandlesCostCenter BIT
				DECLARE @ConsignmentDebitHandlesThirdParty BIT, @ConsignmentDebitHandlesCostCenter BIT
				DECLARE @ConsignmentCreditHandlesThirdParty BIT, @ConsignmentCreditHandlesCostCenter BIT

				-- Consolidar consultas a MainAccounts en una sola
				SELECT 
					@CounterpartHandlesThirdParty = MAX(CASE WHEN ma.Id = @CounterpartCostConsignedInventoryId THEN ma.HandlesThirdParty ELSE 0 END),
					@CounterpartHandlesCostCenter = MAX(CASE WHEN ma.Id = @CounterpartCostConsignedInventoryId THEN ma.HandlesCostCenter ELSE 0 END),
					@InventoryCostHandlesThirdParty = MAX(CASE WHEN ma.Id = @InventoryCostMainAccountId THEN ma.HandlesThirdParty ELSE 0 END),
					@InventoryCostHandlesCostCenter = MAX(CASE WHEN ma.Id = @InventoryCostMainAccountId THEN ma.HandlesCostCenter ELSE 0 END),
					@ConsignmentCreditHandlesThirdParty = MAX(CASE WHEN ma.Id = @ConsignmentMerchandiseCreditAccountId THEN ma.HandlesThirdParty ELSE 0 END),
					@ConsignmentCreditHandlesCostCenter = MAX(CASE WHEN ma.Id = @ConsignmentMerchandiseCreditAccountId THEN ma.HandlesCostCenter ELSE 0 END),
					@ConsignmentDebitHandlesThirdParty = MAX(CASE WHEN ma.Id = @ConsignmentMerchandiseDebitAccountId THEN ma.HandlesThirdParty ELSE 0 END),
					@ConsignmentDebitHandlesCostCenter = MAX(CASE WHEN ma.Id = @ConsignmentMerchandiseDebitAccountId THEN ma.HandlesCostCenter ELSE 0 END)
				FROM GeneralLedger.MainAccounts ma
				WHERE ma.Id IN (@CounterpartCostConsignedInventoryId, @InventoryCostMainAccountId, 
								@ConsignmentMerchandiseCreditAccountId, @ConsignmentMerchandiseDebitAccountId)

				-- Insertar detalles del comprobante contable
				INSERT INTO @JournalVoucherDetails 
					SELECT 0,0,@CounterpartCostConsignedInventoryId,
						   CASE WHEN @CounterpartHandlesThirdParty = 1 THEN @ThirdPartyId ELSE NULL END,
						   CASE WHEN @CounterpartHandlesCostCenter = 1 THEN @CostCenterId ELSE NULL END,
						   0,@ProductCost,@Detail, null,null, null,null  
		 
				INSERT INTO @JournalVoucherDetails 
					SELECT 0,0,@InventoryCostMainAccountId,
						   CASE WHEN @InventoryCostHandlesThirdParty = 1 THEN @ThirdPartyId ELSE NULL END,
						   CASE WHEN @InventoryCostHandlesCostCenter = 1 THEN @CostCenterId ELSE NULL END,
						   @ProductCost,0,@Detail, null,null, null,null  

				INSERT INTO @JournalVoucherDetails 
					SELECT 0,0,@ConsignmentMerchandiseCreditAccountId,
						   CASE WHEN @ConsignmentCreditHandlesThirdParty = 1 THEN @ThirdPartyId ELSE NULL END,
						   CASE WHEN @ConsignmentCreditHandlesCostCenter = 1 THEN @CostCenterId ELSE NULL END,
						   @ProductCost,0,@Detail, null,null, null,null  

				INSERT INTO @JournalVoucherDetails 
				SELECT 0,0,@ConsignmentMerchandiseDebitAccountId,
					   CASE WHEN @ConsignmentDebitHandlesThirdParty = 1 THEN @ThirdPartyId ELSE NULL END,
					   CASE WHEN @ConsignmentDebitHandlesCostCenter = 1 THEN @CostCenterId ELSE NULL END,
					   0,@ProductCost,@Detail, null,null, null,null

				END
				
				cursor_inventory_end:
				FETCH NEXT FROM cursor_inventory INTO @EntityDetailId,
					@ProductId,
					@BatchSerialId,
					@Quantity,
					@Value,
					@WarehouseIds,
					@WarehouseConsignment
			END

			CLOSE cursor_inventory
			DEALLOCATE cursor_inventory
		END
	
		--Generamos el XML para consumir el SP encargado del movimiento del kardex
				  
		IF EXISTS (SELECT 1
							FROM Billing.BasicBillingDetail bbd
							JOIN Inventory.InventoryProduct ip ON bbd.ProductId = ip.Id
							LEFT JOIN Billing.BasicBillingDetailItem bbdi ON bbd.Id = bbdi.BasicBillingDetailId
							LEFT JOIN Inventory.PhysicalInventory p ON bbdi.PhysicalInventoryId = p.Id
							LEFT JOIN Inventory.Warehouse W ON W.Id = bbd.WarehouseId
							WHERE bbd.BasicBillingId = @Id AND bbd.DetailType = 1 and W.WarehouseConsignment=1)
		BEGIN
		
			SELECT @SubXml = CONVERT
						(
							XML, 
							(
								SELECT Remission.*
								FROM 
								( 
									SELECT 
									ISNULL(bbdi.Id, bbd.Id) EntityDetailId,
									bbd.ProductId As ProductId, 	
									@OperatingUnitId OperatingUnitId,
									bbd.FunctionalUnitId FunctionalUnitId,
									@MovementType MovementType,
									p.BatchSerialId As BatchSerialId, 
									ISNULL(bbdi.Quantity, bbd.Quantity) As Quantity, 
									CASE 
										WHEN cpcl.InvoiceWithCostList IS NULL THEN ip.ProductCost
										ELSE ccld.CostNew
									END as Value,
									w.Id as WarehouseId,
									cpcl.InvoiceWithCostList InvoiceWithCostList
								FROM Billing.BasicBillingDetail bbd								
								JOIN Inventory.InventoryProduct ip ON bbd.ProductId = ip.Id								
								LEFT JOIN Billing.BasicBillingDetailItem bbdi ON bbd.Id = bbdi.BasicBillingDetailId
								JOIN @ConsignmentProductCostList cpcl ON cpcl.EntityDetailId = bbdi.Id
								LEFT JOIN Inventory.PhysicalInventory p ON bbdi.PhysicalInventoryId = p.Id
								LEFT JOIN Inventory.Warehouse W ON W.Id = bbd.WarehouseId
								JOIN Common.Supplier s ON s.Id = w.SupplierId
								INNER JOIN Inventory.ConsignmentCostList ccl with(Nolock) ON ccl.SupplierId = W.SupplierId
								LEFT JOIN Inventory.ConsignmentCostListDetail ccld with(Nolock) ON ccld.ConsignmentCostListId = ccl.Id and ccld.ProductId = ip.Id
								WHERE bbd.BasicBillingId = @Id AND bbd.DetailType = 1 and W.WarehouseConsignment=1 AND ccl.OperatingUnitId = @OperatingUnitId AND s.ConsignmentInventoryCosting = 1

								UNION ALL

								SELECT 
									ISNULL(bbdi.Id, bbd.Id) EntityDetailId,
									bbd.ProductId As ProductId, 	
									@OperatingUnitId OperatingUnitId,
									bbd.FunctionalUnitId FunctionalUnitId,
									@MovementType MovementType,
									p.BatchSerialId As BatchSerialId, 
									ISNULL(bbdi.Quantity, bbd.Quantity) As Quantity, 
									ip.ProductCost As Value,
									w.Id as WarehouseId
									,NULL
								FROM Billing.BasicBillingDetail bbd
								JOIN Inventory.InventoryProduct ip ON bbd.ProductId = ip.Id
								LEFT JOIN Billing.BasicBillingDetailItem bbdi ON bbd.Id = bbdi.BasicBillingDetailId
								LEFT JOIN Inventory.PhysicalInventory p ON bbdi.PhysicalInventoryId = p.Id
								LEFT JOIN Inventory.Warehouse W ON W.Id = bbd.WarehouseId
								JOIN Common.Supplier s ON s.Id = w.SupplierId
								WHERE bbd.BasicBillingId = @Id AND bbd.DetailType = 1 and W.WarehouseConsignment=1 AND s.ConsignmentInventoryCosting = 0

								) Remission
								For XML AUTO,TYPE, ELEMENTS
							)
						)     
				
				SET @Message_Output = ''
				EXEC Inventory.SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission @SubXml, @Id, @Code, 'BasicBilling', @CodeUser, @Message_Output OUT

				IF LEN(@Message_Output) > 0
				BEGIN
					INSERT INTO @table_errors 
						SELECT ISNULL(@Message_Output, 'No se pudo afectar la cantidad del inventario en consignación')
				END
		END
	
	--Si tiene Activos Fijos
	IF EXISTS 
	(
		SELECT 1
		FROM Billing.BasicBilling bb
		JOIN Billing.BasicBillingDetail bbd ON bb.Id = bbd.BasicBillingId
		WHERE bb.Id = @Id AND bbd.DetailType = 3
	)
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM FixedAsset.SettingFixedAsset sfa WHERE sfa.OperatingUnitId = @OperatingUnitId)
		BEGIN
			INSERT INTO @table_errors VALUES ('No existe parámetros de activo fijo para la unidad operativa escogida.')
		END
		ELSE IF NOT EXISTS 
		(
			SELECT 1 
			FROM Billing.BasicBilling bb
			JOIN FixedAsset.SettingFixedAsset sfa ON sfa.OperatingUnitId = bb.OperatingUnitId
			WHERE YEAR(bb.DocumentDate) = YEAR(sfa.ProcessDate) AND MONTH(bb.DocumentDate) = MONTH(sfa.ProcessDate)
		)
		BEGIN
			INSERT INTO @table_errors VALUES ('El mes y año de la factura debe ser la misma de proceso de parámetros de Activos Fijos.')
		END
		ELSE
		BEGIN
			-- Tabla temporal para validaciones de activos fijos
			DECLARE @FixedAsset_Validation_Temp TABLE (
				Id INT,
				Plate VARCHAR(50),
				HasOutput TINYINT,
				AdquisitionType TINYINT,
				Depreciate BIT,
				DetailBookId INT
			)

			INSERT INTO @FixedAsset_Validation_Temp
			SELECT 
				fapa.Id,
				fapa.Plate,
				fapa.HasOutput,
				fapa.AdquisitionType,
				fapa.Depreciate,
				fapadb.Id AS DetailBookId
			FROM Billing.BasicBilling bb
			JOIN Billing.BasicBillingDetail bbd ON bb.Id = bbd.BasicBillingId
			JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON bbd.PhysicalAssetId = fapa.Id
			LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.Id = fapadb.PhysicalAssetId
			WHERE bb.Id = @Id AND bbd.DetailType = 3

			-- Validación de activos con restricciones de venta
			INSERT INTO @table_errors
				SELECT 
					CASE AdquisitionType
						WHEN 3 THEN 'El Activo Fijo con Placa ' + Plate + ' es un comodato y no puede tener la opción de venta'
						WHEN 8 THEN 'El Activo Fijo con Placa ' + Plate + 'es un comodato tercerizado y no puede tener la opción de venta'
						WHEN 10 THEN 'El Activo Fijo con Placa ' + Plate + ' es un renting operativo y no puede tener la opción de venta'
						ELSE 'El Activo Fijo con Placa ' + Plate + ' no se encuentra activo'
					END
				FROM @FixedAsset_Validation_Temp
				WHERE HasOutput <> 0 OR AdquisitionType IN (3, 8, 10)
			
			-- Validación de activos que deprecian sin libros contables
			INSERT INTO @table_errors
				SELECT 'El Activo Fijo con Placa ' + Plate + ' deprecia pero no tiene registrado los libros contables'
				FROM @FixedAsset_Validation_Temp
				WHERE Depreciate = 1 AND DetailBookId IS NULL
					AND NOT EXISTS (SELECT 1 FROM @FixedAsset_Validation_Temp WHERE HasOutput <> 0 OR AdquisitionType IN (3, 8, 10))
			
			-- Actualizar activos fijos si no hay errores
			IF NOT EXISTS (
				SELECT 1 FROM @FixedAsset_Validation_Temp 
				WHERE (HasOutput <> 0 OR AdquisitionType IN (3, 8, 10))
					OR (Depreciate = 1 AND DetailBookId IS NULL)
			)
			BEGIN
				UPDATE fapa
					SET fapa.HasOutput = 1,
						fapa.OutputDate = [Common].[GETDATE](),
						fapa.Status = 0
				FROM Billing.BasicBilling bb
				JOIN Billing.BasicBillingDetail bbd ON bb.Id = bbd.BasicBillingId
				JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON bbd.PhysicalAssetId = fapa.Id
				WHERE bb.Id = @Id AND bbd.DetailType = 3
			END
		END
	END

		/************************************************************************************************************************************/
	
		--Si hay errores
		
		IF EXISTS (SELECT 1 FROM @table_errors)
		BEGIN
			SELECT @Message = STUFF
			(
				(
					SELECT CHAR(13) + CHAR(10) + Message
					FROM @table_errors
					FOR XML PATH(N''), TYPE
				).value(N'.[1]', N'nvarchar(max)'), 1, 2, N''
			)

			SELECT 999 AS CodeMessage, @Message AS Message, 0 AS InvoiceId, '' AS InvoiceNumber, 0 as AccountReceivableId
			RETURN
		END

		/************************************************************************************************************************************/

		--Actualizo el estado de la factura
		UPDATE bb
			SET bb.DocumentDate = @DocumentDate,
				bb.Status = 2,
				bb.ConfirmationUser = @CodeUser,
				bb.ConfirmationDate = [Common].[GETDATE]()
		FROM Billing.BasicBilling bb
		WHERE bb.Id = @Id

		SET @Message = 'Se confirmó la Factura Básica ' + @Code

		--================================GENERACIÓN FACTURA================================

		EXEC Billing.SP_GenerateInvoiceByBasicBilling @Id, @CodeUser, @Code_Output OUT, @Message_Output OUT, @InvoiceId OUT, @InvoiceNumber OUT, @AccountReceivableId OUT

		IF @Code_Output <> 0
		BEGIN
			SELECT  999 as CodeMessage, ISNULL(@Message_Output, 'No se pudo generar la factura') as Message, 0 AS InvoiceId, '' AS InvoiceNumber, 0 as AccountReceivableId
			RETURN
		END

		SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)

		--Actualizo el registro con la factura generada
		UPDATE bb
			SET bb.InvoiceId = @InvoiceId
		FROM Billing.BasicBilling bb
		WHERE bb.Id = @Id
		
		--================================RECIBO DE CAJA================================
		IF @CashReceiptsXml IS NOT NULL
		BEGIN
			----Generamos el XML de la seccion account receivable
			DECLARE @ExistingXml XML = @CashReceiptsXml;

			EXEC [Treasury].[SP_SaveCashReceipts_Output] @ExistingXml, @CodeUser, @CompanyType, @Code_Output OUT, @Message_Output OUT, NULL, @IdCashReceipt OUT
			
			IF @Code_Output <> 0
			BEGIN
				SELECT  999 as CodeMessage, ISNULL(@Message_Output, 'No se pudo generar la factura') as Message, 0 AS InvoiceId, '' AS InvoiceNumber, 0 as AccountReceivableId
				RETURN
			END

			IF not EXISTS(SELECT 1 FROM Treasury.CashReceipts cr WITH(NOLOCK) 
							JOIN Portfolio.PortfolioAdvance pa WITH(NOLOCK) on cr.Id =pa.CashReceiptId
							where cr.Id = @IdCashReceipt) BEGIN
				
				SELECT  999 as CodeMessage, ISNULL(@Message_Output, 'No se pudo crear el Anticipo para cruzar con la factura') as Message, 0 AS InvoiceId, '' AS InvoiceNumber, 0 as AccountReceivableId
				RETURN

			END

				DECLARE  @listPortfolioAdvanceCrossingXml as XML

						SET @listPortfolioAdvanceCrossingXml = CONVERT
			(
				XML, 
				(
								SELECT	ListPortfolioAdvanceCrossing.*
					FROM 
					(
									SELECT	pa.Id,
											pa.Code,
											pa.Balance CrossingValue,
											pa.CashReceiptDetailId CashReceiptDetailIdTmp,
											'BasicBilling' EntityName
									FROM Treasury.CashReceipts cr WITH(NOLOCK) 
									JOIN Portfolio.PortfolioAdvance pa WITH(NOLOCK) on cr.Id =pa.CashReceiptId
									where cr.Id = @IdCashReceipt
								) ListPortfolioAdvanceCrossing
					For XML AUTO,TYPE, ELEMENTS
				)
			)
						 

				declare @Message_Output_Transfer varchar(1000),
						@Code_Output_Transfer INT

			EXEC [Portfolio].[SP_GeneratePortfolioTransfer] @ListPortfolioAdvanceCrossingXml = @listPortfolioAdvanceCrossingXml, @OperativeUnitId =@OperatingUnitId,
															@UserCode =@CodeUser,@AccountReceivableId=@AccountReceivableId,@CompanyType=@CompanyType,
															@CodeResult = @Code_Output_Transfer OUT,@MessageResult = @Message_Output_Transfer OUT
			
			IF @Code_Output_Transfer <> 0
			BEGIN
				SELECT  999 as CodeMessage, ISNULL(@Message_Output_Transfer, 'No se pudo generar la factura') as Message, 0 AS InvoiceId, '' AS InvoiceNumber, 0 as AccountReceivableId
				RETURN
			END
			
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + CONCAT(@Message_Output, ' - ', COALESCE(@Message_Output_Transfer,'')))
		END

		/************************************************************************************************************************************/		

		SELECT 0 AS CodeMessage, ISNULL(@Message, '') as Message, @InvoiceId as InvoiceId, @InvoiceNumber as InvoiceNumber, @AccountReceivableId as AccountReceivableId
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + CHAR(13) + CHAR(10) + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, 0 AS InvoiceId, '' AS InvoiceNumber, 0 as AccountReceivableId
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que confirma (cierra y oficializa) una factura básica de venta previamente creada en estado borrador o prefactura. Valida que el documento esté en estado pendiente, que existan libros contables parametrizados, y que los porcentajes e IVA de cada línea de detalle (productos de inventario, conceptos de servicio, activos fijos) coincidan con la parametrización vigente. Genera los comprobantes contables de reconocimiento de venta, procesa los recibos de caja asociados (vía XML), actualiza el estado de la factura básica a ''Confirmado'' y, si aplica, gestiona el reconocimiento de costo de productos en consignación. Toca las entidades de facturación básica, detalle de factura, inventario, activos fijos y libro mayor contable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmBasicBilling';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmBasicBilling';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma una factura básica validando IVA, inventario y activos fijos, afecta kardex (incluyendo consignación), arma asientos contables de reconocimiento, genera la factura definitiva y opcionalmente registra recibo de caja con cruce de anticipo.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmBasicBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El registro Billing.BasicBilling debe existir y estar en Status = 1 (no confirmado/no anulado); Debe existir al menos un libro contable parametrizado en GeneralLedger.VieBot con Form=''BasicBilling'' y Allow=1; Si hay activos fijos (DetailType=3), debe existir parametrización en FixedAsset.SettingFixedAsset para la unidad operativa y el mes/año del documento debe coincidir con ProcessDate; El porcentaje de IVA registrado en el detalle debe coincidir con el parametrizado en GeneralLedger.GeneralLedgerIVA del producto/servicio/activo; La cuenta contable de venta del IVA (IdAccountSale) debe estar parametrizada en MainAccounts; Para productos en consignación con ConsignmentInventoryCosting=1, debe existir lista de costos vigente (ConsignmentCostList) cuya EffectiveDate sea >= DocumentDate y aplique a la unidad operativa', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmBasicBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Billing.BasicBilling: Cuando todas las validaciones pasan, se actualiza DocumentDate, Status=2 (Confirmado), ConfirmationUser y ConfirmationDate; [UPDATE] Billing.BasicBilling: Tras generar la factura definitiva con SP_GenerateInvoiceByBasicBilling se asigna InvoiceId al registro; [UPDATE] FixedAsset.FixedAssetPhysicalAsset: Si el detalle incluye activos fijos (DetailType=3) y no hay errores, se marca HasOutput=1, OutputDate=fecha actual y Status=0 (dado de baja por venta); [EXEC] Inventory.PhysicalInventory: Por cada producto facturado se invoca SP_SavePhysicalInventoryKardex_Output con MovementType=2 (salida) y AffectAverageCost=0 para descontar del kardex; [EXEC] Inventory.ConsignmentCostList: Cuando la bodega es de consignación se llama a SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission para afectar las cantidades de remisión en consignación; [EXEC] Billing.Invoice: Se invoca Billing.SP_GenerateInvoiceByBasicBilling para generar la factura definitiva, su número y la cuenta por cobrar; [EXEC] Treasury.CashReceipts: Si @CashReceiptsXml no es NULL se ejecuta Treasury.SP_SaveCashReceipts_Output para crear el recibo de caja asociado; [EXEC] Portfolio.PortfolioAdvance: Si se generó recibo de caja, se ejecuta Portfolio.SP_GeneratePortfolioTransfer para cruzar el anticipo creado contra la cuenta por cobrar de la factura; [RETURN_RESULT] Result: Devuelve CodeMessage=0 con InvoiceId, InvoiceNumber y AccountReceivableId en éxito; CodeMessage=999 con mensaje de error consolidado en cualquier validación fallida o excepción', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmBasicBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Billing.BasicBilling.Status <> 1 → Retorna CodeMessage=999 indicando si está Confirmado (2) o Anulado, y aborta el proceso; si No existe registro en GeneralLedger.VieBot con Form=''BasicBilling'' y Allow=1 → Retorna error ''Debe parametrizar al menos un libro contable'' y aborta; si Existe al menos un detalle con DetailType=1 (Producto) → Recorre cursor de productos para validar consignación, afectar kardex y generar líneas del comprobante contable de reconocimiento; si Warehouse.WarehouseConsignment=1 y Supplier.ConsignmentInventoryCosting=1 → Valida que el producto esté en ConsignmentCostList vigente; si no, registra error y salta al siguiente ítem; si sí, marca InvoiceWithCostList=1; si @WarehouseConsignment=1 dentro del cursor → Calcula cuentas (CounterpartCostConsignedInventory, InventoryCost, ConsignmentMerchandiseDebit/Credit) desde ProductGroup e inserta 4 líneas en @JournalVoucherDetails (reconocimiento de venta de producto en consignación) else No se generan asientos contables de reconocimiento de consignación para ese ítem; si @SupplierThirdPartyId IS NOT NULL (bodega es de consignación) → Usa el tercero del proveedor (Supplier.IdThirdParty) en los asientos else Usa el tercero del cliente (Customer.ThirdPartyId); si @ProductCost IS NULL → Toma el costo desde InventoryProduct.ProductCost; si Existe al menos un detalle con DetailType=3 (Activo Fijo) → Valida parametrización en SettingFixedAsset, coincidencia mes/año, restricciones por AdquisitionType (3 comodato, 8 comodato tercerizado, 10 renting), HasOutput y libros contables si Depreciate=1; si todo pasa, marca el activo como dado de baja; si Existen errores acumulados en @table_errors → Concatena los mensajes con CRLF y retorna CodeMessage=999 sin confirmar la factura; si @CashReceiptsXml IS NOT NULL → Genera recibo de caja, valida que se haya creado un PortfolioAdvance asociado y ejecuta SP_GeneratePortfolioTransfer para cruzar el anticipo con la factura; si No existe PortfolioAdvance asociado al CashReceipt creado → Retorna error ''No se pudo crear el Anticipo para cruzar con la factura'' y aborta', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmBasicBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmBasicBilling';
-- GO
