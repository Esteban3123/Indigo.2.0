-- =============================================
-- Author:		William Otalora
-- Create date: 2022-01-17
-- Description:	Procedimiento que se encarga de reversar la factura basica
-- =============================================
CREATE PROCEDURE [Billing].[SP_ReverseBasicBilling]
	@Id AS INT,
	@CodeUser AS VARCHAR(20),
	--------------------------------------------
	@ReversalReasonId AS INT,
	@ReversalReasonDescription AS VARCHAR(200),
	--------------------------------------------
	@CompanyType INT
AS
BEGIN
	SET NOCOUNT ON

	--Declarar las variables
	DECLARE @Code VARCHAR(20),
			@OperatingUnitId INT,			
			@FunctionalUnitId INT,
			@WarehouseId INT,
			@DocumentDate DATETIME,
			@ThirdPartyIdDocument INT,
			--------------------------------------------
			@InvoiceId INT, 
			@InvoiceNumber VARCHAR(15),
			@AccountReceivableId INT,
			@Consecutive  VARCHAR(MAX),
			--------------------------------------------
			@Message VARCHAR(MAX),
			--------------------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)
			--------------------------------------------

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
		[BaseValue] [decimal](18, 0) NULL ,
		[BillingValue] [decimal](18, 0) NULL
	)

	BEGIN TRY
		SELECT	@Code = bb.Code,
				@OperatingUnitId = bb.OperatingUnitId,
				@FunctionalUnitId = bb.FunctionalUnitId,
				@WarehouseId = bb.WarehouseId,
				@DocumentDate = [Common].[GETDATE](),
				@ThirdPartyIdDocument = t.Id
		FROM Billing.BasicBilling bb
		JOIN Common.Customer c ON bb.CustomerId = c.Id
		JOIN Common.ThirdParty t ON t.Id = c.ThirdPartyId
		WHERE bb.Id = @Id

		DECLARE @Detail as VARCHAR(MAX) = 'Reversión de venta de producto en consignacion Factura No. ' + @Code


		/*************************************************** VALIDACIONES Y PROCESOS POR TIPO DE DETALLE ***************************************************/

		-- Verifica el estado
		
		IF EXISTS (SELECT 1 FROM Billing.BasicBilling bb WHERE bb.Id = @Id AND bb.Status = 3)
		BEGIN
			SELECT 999 as CodeMessage, 'El registro se encuentra en estado: Anulado' as Message, 0 AS InvoiceId, '' AS InvoiceNumber, 0 as AccountReceivableId
			FROM Billing.BasicBilling bb
			WHERE bb.Id = @Id
			RETURN
		END

		-- Verifica libro
		IF NOT EXISTS (SELECT 1 FROM GeneralLedger.VieBot vb WHERE vb.Form = 'BasicBilling' AND vb.Allow = 1)
		BEGIN
			SELECT 999 as CodeMessage, 'Debe parametrizar al menos un libro contable' as Message, 0 AS InvoiceId, '' AS InvoiceNumber, 0 as AccountReceivableId
			RETURN
		END
			
		-- Verifica Si tiene productos y el tipo de detalle(DetailType) sea de Producto(1)
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
					@MovementType INT = 1,					
					@BatchSerialId INT,
					@Quantity INT,
					@Value DECIMAL(18,2),
					@WarehouseIds INT,
					@AffectAverageCost INT = 0

			--cursor
			DECLARE cursor_inventory CURSOR FOR 
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
				--Generamos el XML para consumir el SP encargado del movimiento del kardex
				SELECT @SubXml = CONVERT
				(
					XML, 
					(
						SELECT Kardex.*
						FROM 
						( 
							SELECT
								@ThirdPartyIdDocument ThirdPartyId,
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
				
				--Ir al SP del Kardex
				EXEC Inventory.SP_SavePhysicalInventoryKardex_Output @SubXml, @Id, @Code, 'BasicBillingDevolution', @CodeUser, 1, @Code_Output OUT, @Message_Output OUT

				IF @Code_Output <> 0
				BEGIN
					INSERT INTO @table_errors 
						SELECT ISNULL(@Message_Output, 'No se pudo afectar el kardex')
					GOTO cursor_inventory_end
				END

				IF @WarehouseConsignment = 1
				BEGIN
					-----------------------------------------------VALIDAMOS QUE EL PARAMETRO DE ApplyBasicBilling ESTE PARAMETRIZADO --------------------------------
					IF NOT EXISTS (select 1 from Billing.SettingsBilling where IdOperatingUnit= @OperatingUnitId and ApplyBasicBilling = 1)
					BEGIN
						SELECT 999 as CodeMessage,
							   CONCAT('El producto ', ipro.Code, ' es de un almacén en consignación  y el parametro, Aplica facturación básica esta inactivo.') as Message,
							   0 AS InvoiceId,
							   '' AS InvoiceNumber,
							   0 as AccountReceivableId
						FROM Inventory.InventoryProduct ipro
						WHERE Id = @ProductId
						RETURN
					END
					-----------------------------------------------CONTABILIZACION RECONOCIMIENTO DE VENTA EN PRODUCTOS-----------------------------------------------
			
					--se crea xml para crear movimiento contable de reconocimiento del producto 

					--Se saca el valor del producto segun el guardado en JournalVoucherDetail
					DECLARE @ProductCost as DECIMAL(18,2)
					SELECT TOP 1 
						@ProductCost = ISNULL(jvd.DebitValue, jvd.CreditValue)
					FROM GeneralLedger.JournalVouchers jv
					join GeneralLedger.JournalVoucherDetails jvd on jvd.IdAccounting = jv.Id
					WHERE jv.Id=@Id AND jv.EntityName='BasicBilling' and jvd.IdRetention is null and jvd.RetentionRate is NULL and jvd.BaseValue is null and jvd.BillingValue is null 
				
					--se crea cuenta contable CXP Provisional
					--se saca la cuenta contrapartida al costo de inventario en consignacion y el valor promedio del producto
					DECLARE @ThirdPartyId as INT
					DECLARE @CostCenterId as INT
					DECLARE @CounterpartCostConsignedInventoryId as INT

					select 
						@ThirdPartyId = c.ThirdPartyId,
						@CostCenterId = fu.CostCenterId
					from Billing.BasicBilling bb
					join Common.Customer c on c.Id = bb.CustomerId
					join Payroll.FunctionalUnit fu on fu.Id = bb.FunctionalUnitId
					where bb.Id = @Id

					select
						@CounterpartCostConsignedInventoryId= pg.CounterpartCostConsignedInventoryId,
						@ProductCost = ip.ProductCost
					from Inventory.InventoryProduct ip
					join Inventory.ProductGroup pg on pg.Id = ip.ProductGroupId
					where ip.Id = @ProductId

					insert into @JournalVoucherDetails 
						select 0,0,@CounterpartCostConsignedInventoryId,@ThirdPartyId,
							   @CostCenterId, @ProductCost,0,@Detail, null,null, null,null  
			
					--Se crea cuenta contable costo
					DECLARE @InventoryCostMainAccountId as INT

					select
						@InventoryCostMainAccountId= pg.InventoryCostMainAccountId
					from Inventory.InventoryProduct ip
					join Inventory.ProductGroup pg on pg.Id = ip.ProductGroupId
					where ip.Id = @ProductId

					insert into @JournalVoucherDetails 
						select 0,0,@CounterpartCostConsignedInventoryId,@ThirdPartyId,
							   @CostCenterId ,0,@ProductCost,@Detail, null,null, null,null  

					--Se crea cuenta contable de Control
					DECLARE @ConsignmentMerchandiseDebitAccountId AS INT
					DECLARE @ConsignmentMerchandiseCreditAccountId AS INT

					select 
						@ConsignmentMerchandiseDebitAccountId= ConsignmentMerchandiseDebitAccountId,
						@ConsignmentMerchandiseCreditAccountId=ConsignmentMerchandiseCreditAccountId 
					from Inventory.InventoryProduct ip
					join Inventory.ProductGroup pg on pg.Id = ip.ProductGroupId
					where ip.Id = @ProductId

					insert into @JournalVoucherDetails 
						select 0,0,@ConsignmentMerchandiseDebitAccountId,@ThirdPartyId,
							   @CostCenterId, @ProductCost,0,@Detail, null,null, null,null  

					insert into @JournalVoucherDetails 
						select 0,0,@ConsignmentMerchandiseCreditAccountId,@ThirdPartyId,
							   @CostCenterId ,0,@ProductCost,@Detail, null,null, null,null  

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
		
		IF EXISTS (SELECT 1 from @JournalVoucherDetails)
			BEGIN
			-----Se crea cabecera el comprobante 

			--Obtengo el libro oficial
			Declare @Uno Tinyint = 1
			declare @LegalBookId integer
			select @LegalBookId = id  from GeneralLedger.LegalBook With(Nolock) where OfficialBook = @Uno

			--se toma la moneda oficial
			DECLARE @CurrencyId as INT
			SELECT top 1  @CurrencyId = OfficialCurrencyId from GeneralLedger.CompanySettings

			--se trae la cuenta parametrizada para comprobante de reconocimiento de venta en consignacion
			Declare @JournalVoucherType as int,
					@ApplyElectronicSalesTicket BIT

			SELECT	@JournalVoucherType=ReversalRecognitionConsignmentSale,
					@ApplyElectronicSalesTicket = ApplyElectronicSalesTicket
			FROM Billing.SettingsBilling 
			WHERE IdOperatingUnit= @OperatingUnitId

			--Si aplica la logica de tiquete electronico de venta se cambia el tipo de comprobante contable para la reversion
			IF @ApplyElectronicSalesTicket = 1 AND EXISTS(SELECT 1 
																FROM Billing.BasicBilling bb
																JOIN Common.Customer c ON c.Id = bb.CustomerId
																JOIN Common.ThirdParty th ON th.Id = c.ThirdPartyId
																WHERE th.ElectronicBiller = 0 AND bb.Id = @Id) BEGIN

				SELECT @JournalVoucherType = sb.AccountingVoucherReversalId
				FROM Billing.SettingsBilling sb
				WHERE sb.IdOperatingUnit = @OperatingUnitId
			END

			--se inserta la cabecera
			insert into @JournalVouchers 
			select 0,0,0,@LegalBookId,@JournalVoucherType,@DocumentDate ,0,2,@Detail,@Code,@Id,'BasicBilling',0,@CurrencyId,@CodeUser,[Common].[GETDATE](),@CodeUser,[Common].[GETDATE](),@CodeUser,[Common].[GETDATE]()
			from Treasury.SettingsTreasury With(Nolock) where IdOperatingUnit =@OperatingUnitId 

			--tabla temporal para almacenar el resultado del movimiento contable
			declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)
			select @SubXml =  convert
			(
				xml, 
				(
					select * from @JournalVouchers JournalVoucher 
					inner join @JournalVoucherDetails JournalVoucherDetail on JournalVoucher.id = JournalVoucherDetail.IdAccounting   
					For xml AUTO,TYPE, ELEMENTS
					)
				)

			insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @SubXml, @CodeUser 
		END

		--REMISION

		--Generamos el XML para consumir el SP encargado del movimiento del kardex (actualizacion de la cantidad de productos)
		IF EXISTS ( SELECT 1
						FROM Billing.BasicBillingDetail bbd
						JOIN Inventory.InventoryProduct ip on bbd.ProductId = ip.Id
						LEFT JOIN Billing.BasicBillingDetailItem bbdi on bbd.Id = bbdi.BasicBillingDetailId
						LEFT JOIN Inventory.PhysicalInventory p on bbdi.BasicBillingDetailId = p.Id
						LEFT JOIN Inventory.Warehouse w on w.Id = bbd.WarehouseId
						WHERE bbd.BasicBillingId = @Id AND bbd.DetailType = 1 and W.WarehouseConsignment=1)
			BEGIN
					SET @Message_Output=''
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
									@FunctionalUnitId FunctionalUnitId,
									@MovementType MovementType,
									p.BatchSerialId As BatchSerialId, 
									ISNULL(bbdi.Quantity, bbd.Quantity) As Quantity, 
									ip.ProductCost As Value,			   
									w.Id as WarehouseId,
									@Id as OriginId
								FROM Billing.BasicBillingDetail bbd
								JOIN Inventory.InventoryProduct ip ON bbd.ProductId = ip.Id
								LEFT JOIN Billing.BasicBillingDetailItem bbdi ON bbd.Id = bbdi.BasicBillingDetailId
								LEFT JOIN Inventory.PhysicalInventory p ON bbdi.PhysicalInventoryId = p.Id
								LEFT JOIN Inventory.Warehouse W ON W.Id = bbd.WarehouseId
								WHERE bbd.BasicBillingId = @Id AND bbd.DetailType = 1 and W.WarehouseConsignment=1
								) Remission
							For XML AUTO,TYPE, ELEMENTS
						)
					)

					EXEC Inventory.SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission @SubXml, @Id, @Code, 'BasicBilling', @CodeUser, @Message_Output OUT

					IF LEN(@Message_Output) > 0
					BEGIN
						INSERT INTO @table_errors 
							SELECT ISNULL(@Message_Output, 'No se pudo afectar la cantidad del inventario en consignación')
					END
				END
		--------------------------------------------------------------------------------------------------------------------
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
				bb.Status = 3,
				bb.ModificationUser = @CodeUser,
				bb.ModificationDate = [Common].[GETDATE](),
				bb.AnnulmentUser = @CodeUser,
				bb.AnnulmentDate = [Common].[GETDATE](),
				bb.ReversalReasonId = @ReversalReasonId,
				bb.ReversalReasonDescription =  @ReversalReasonDescription

		FROM Billing.BasicBilling bb
		WHERE bb.Id = @Id

		SET @Message = 'Se anuló la Factura Básica ' + @Code

		--================================CONSULTAR Y ANULAR LA FACTURA RELACIONADA================================

		SELECT	@InvoiceId = i.Id,
				@InvoiceNumber = i.InvoiceNumber,
				@AccountReceivableId = ar.Id
		FROM Billing.Invoice i
		JOIN Billing.BasicBilling bb ON bb.InvoiceId = i.Id
		JOIN Portfolio.AccountReceivable ar ON ar.InvoiceId = i.Id 
		WHERE bb.Id = @Id

		UPDATE Billing.Invoice
		SET Status = 2
		WHERE Id = @InvoiceId

		/************************************************************************************************************************************/		

		SELECT 0 AS CodeMessage, ISNULL(@Message, '') as Message, @InvoiceId as InvoiceId, @InvoiceNumber as InvoiceNumber, @AccountReceivableId as AccountReceivableId
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + CHAR(13) + CHAR(10) + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, 0 AS InvoiceId, '' AS InvoiceNumber, 0 as AccountReceivableId
	END CATCH
END