-- =============================================
-- Author:      Angi Camila Durán Vargas
-- Create Date: 20/02/2023
-- Description: Se crea procedimiento para que genere el comprobante contable para la reversion parcial- venta en consignación
-- desde nota debito/credito en cartera
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_GenerateJournalVoucher_ReversalsConsignmentSale]
(
    @InvoiceId as INT,
	@PortfolioId as INT,
	@PortfolioCode as VARCHAR(MAX),
	@OperativeUnitId as INT,
	@CodeUser as VARCHAR(MAX)
)
AS
BEGIN
    DECLARE @BasicBillingId as INT,
		    @BasicBillingCode as VARCHAR(MAX),
			@SubXml XML,
			--------------------------------------------
			@Message VARCHAR(MAX),
			--------------------------------------------
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)
			--------------------------------------------

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
	
		SELECT	@BasicBillingId = bb.Id,
				@BasicBillingCode = bb.Code
		FROM Billing.BasicBilling bb
		WHERE bb.InvoiceId = @InvoiceId

		DECLARE @Detail as VARCHAR(MAX) = 'Documento generado para reversion de reconocimiento de los productos en consignacion de la Factura Basica No. ' + @BasicBillingCode

		
		-- Verifica el estado

		IF EXISTS (SELECT 1 FROM Billing.BasicBilling bb WHERE bb.Id = @BasicBillingId AND bb.Status = 3)
		BEGIN
			SELECT 999 as CodeMessage, 'El registro se encuentra en estado: Anulado' as Message, 0 AS InvoiceId, '' AS InvoiceNumber, 0 as AccountReceivableId
			RETURN
		END

		-- Verifica libro
		IF NOT EXISTS (SELECT 1 FROM GeneralLedger.VieBot vb WHERE vb.Form = 'BasicBilling' AND vb.Allow = 1)
		BEGIN
			SELECT 999 as CodeMessage, 'Debe parametrizar al menos un libro contable' as Message, 0 AS InvoiceId, '' AS InvoiceNumber, 0 as AccountReceivableId
			RETURN
		END

		if not exists(SELECT 1
						FROM Billing.BasicBillingDetail bbd
						JOIN Inventory.InventoryProduct ip ON bbd.ProductId = ip.Id
						JOIN Inventory.Warehouse W ON W.Id = bbd.WarehouseId and w.WarehouseConsignment =1						
						WHERE bbd.BasicBillingId = @BasicBillingId AND bbd.DetailType = 1 ) BEGIN
			SELECT 0 as CodeMessage, 'No se genero comprobante contable por que No hay productos en consigna' as Message, 0 AS InvoiceId
			RETURN 
		end

		-- Verifica Si tiene productos y el tipo de detalle(DetailType) sea de Producto(1)
		IF EXISTS 
		(
			SELECT 1
			FROM Billing.BasicBilling bb
			JOIN Billing.BasicBillingDetail bbd ON bb.Id = bbd.BasicBillingId
			WHERE bb.Id = @BasicBillingId AND bbd.DetailType = 1
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
				JOIN Inventory.Warehouse W ON W.Id = bbd.WarehouseId and w.WarehouseConsignment =1
				LEFT JOIN Billing.BasicBillingDetailItem bbdi ON bbd.Id = bbdi.BasicBillingDetailId
				LEFT JOIN Inventory.PhysicalInventory p ON bbdi.PhysicalInventoryId = p.Id
				WHERE bbd.BasicBillingId = @BasicBillingId AND bbd.DetailType = 1 

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
			
			IF @WarehouseConsignment = 1 BEGIN
				-----------------------------------------------CONTABILIZACION RECONOCIMIENTO DE VENTA EN PRODUCTOS-----------------------------------------------
			
				--se crea xml para crear movimiento contable de reconocimiento del producto 

				--Se saca el valor del producto segun el guardado en JournalVoucherDetail
				DECLARE @ProductCost as DECIMAL(18,2)
				select top 1 
					@ProductCost = ISNULL(jvd.DebitValue, jvd.CreditValue)
				from GeneralLedger.JournalVouchers jv
				join GeneralLedger.JournalVoucherDetails jvd on jvd.IdAccounting = jv.Id
				where jv.EntityId=@BasicBillingId AND jv.EntityName='BasicBilling' and jvd.IdRetention is null and jvd.RetentionRate is NULL and jvd.BaseValue is null and jvd.BillingValue is null 
				
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
				where bb.Id = @BasicBillingId

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
	
		-----Se crea cabecera el comprobante 

		--Obtengo el libro oficial
		Declare @Uno Tinyint = 1
		declare @LegalBookId integer
		select @LegalBookId = id  from GeneralLedger.LegalBook With(Nolock) where OfficialBook = @Uno

		--se toma la moneda oficial
		DECLARE @CurrencyId as INT
		SELECT top 1  @CurrencyId = OfficialCurrencyId from GeneralLedger.CompanySettings

		--se trae la cuenta parametrizada para comprobante de reversion parcial de venta en consignacion
		Declare @JournalVoucherType as int	
		select @JournalVoucherType=BasicBillingAnnulmentJournalVoucherTypeId from Billing.SettingsBilling where IdOperatingUnit= @OperativeUnitId

		--se inserta la cabecera
		insert into @JournalVouchers 
		select 0,0,0,@LegalBookId,@JournalVoucherType,[Common].[GETDATE]() ,0,2,@Detail,@PortfolioCode,@PortfolioId,'PortfolioNote',0,@CurrencyId,@CodeUser,[Common].[GETDATE](),@CodeUser,[Common].[GETDATE](),@CodeUser,[Common].[GETDATE]()
		from Treasury.SettingsTreasury With(Nolock) where IdOperatingUnit =@OperativeUnitId 

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
		
		IF ISNULL(@Code_Output, 999) <> 0 
		BEGIN
			SELECT	@Message = ISNULL(@Message_Output, 'Error al generar el comprobante contable')
			RETURN
		END
	END
	SELECT 0 AS CodeMessage, ISNULL(@Message, '') as Message, @InvoiceId as InvoiceId
	END TRY
	BEGIN CATCH
		
		IF CURSOR_STATUS('global','cursor_inventory') >= -1  BEGIN
		  IF CURSOR_STATUS('global','cursor_inventory') > -1 BEGIN
			CLOSE cursor_inventory
		  END
		 DEALLOCATE cursor_inventory
		END
		
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + CHAR(13) + CHAR(10) + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(13)) AS Message, 0 AS InvoiceId
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el comprobante contable de reversión parcial para ventas de productos en consignación, a partir de una factura básica identificada por su número de factura (InvoiceId). Verifica que la factura no esté anulada, que existan libros contables configurados y que los ítems facturados correspondan a productos provenientes de bodegas de consignación; si no cumple estas condiciones, retorna mensajes de advertencia sin generar movimiento. Recorre mediante cursor cada línea de detalle de la factura (BasicBillingDetail) que tenga productos de bodegas en consignación, recupera el costo registrado previamente en el comprobante contable original y genera los asientos contables de reversión (débito/crédito) sobre las cuentas de inventario en consignación y cuentas por pagar provisionales. Se usa desde el módulo de cartera al emitir notas débito o crédito que reviertan el reconocimiento contable de una venta en consignación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucher_ReversalsConsignmentSale';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucher_ReversalsConsignmentSale';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el comprobante contable de reversión parcial del reconocimiento de venta en consignación cuando se emite una nota débito/crédito de cartera sobre una factura básica con productos en bodega de consignación.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucher_ReversalsConsignmentSale';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Billing.BasicBilling asociado al InvoiceId recibido.; La factura básica no debe estar en estado Anulado (Status = 3).; Debe existir al menos un libro contable parametrizado en GeneralLedger.VieBot para el formulario ''BasicBilling'' con Allow = 1.; Debe existir al menos una línea de BasicBillingDetail con DetailType = 1 (producto) cuya bodega esté marcada como WarehouseConsignment = 1.; Debe existir un comprobante contable previo en GeneralLedger.JournalVouchers (EntityName=''BasicBilling'') que tenga el costo del producto registrado, para recuperar el ProductCost.; Debe estar parametrizado el BasicBillingAnnulmentJournalVoucherTypeId en Billing.SettingsBilling para la unidad operativa.; Debe existir un libro oficial (OfficialBook=1) en GeneralLedger.LegalBook y una moneda oficial en GeneralLedger.CompanySettings.; Las cuentas contables CounterpartCostConsignedInventoryId, InventoryCostMainAccountId, ConsignmentMerchandiseDebitAccountId y ConsignmentMerchandiseCreditAccountId deben estar parametrizadas en el ProductGroup del producto.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucher_ReversalsConsignmentSale';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El comprobante contable se registra como nota de cartera: EntityName siempre es ''PortfolioNote'' con EntityId/EntityCode del portafolio recibido.; Siempre se usa el libro contable oficial (LegalBook.OfficialBook=1) y la moneda oficial de CompanySettings.; El tipo de comprobante usado es el BasicBillingAnnulmentJournalVoucherTypeId configurado por unidad operativa.; El detalle textual de cada línea referencia el código de la factura básica origen (''Documento generado para reversion de reconocimiento de los productos en consignacion de la Factura Basica No. ...'').; Los asientos solo se generan para productos cuya bodega tenga WarehouseConsignment=1.; El ProductCost utilizado proviene del comprobante contable original de la factura (JournalVoucherDetails sin retención, base ni billing value) o del costo del producto en InventoryProduct.; El tercero contable es el ThirdPartyId del cliente y el centro de costo es el de la unidad funcional de la factura.; Ante cualquier excepción se libera el cursor cursor_inventory si está abierto.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucher_ReversalsConsignmentSale';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] GeneralLedger.JournalVouchers: Vía EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement con un XML armado: inserta cabecera con LegalBookId del libro oficial, IdJournalVoucher = BasicBillingAnnulmentJournalVoucherTypeId de SettingsBilling, EntityName=''PortfolioNote'', EntityId=@PortfolioId, EntityCode=@PortfolioCode, Status=2 e IsClosedYear=0.; [INSERT] GeneralLedger.JournalVoucherDetails: Por cada producto en consignación se insertan 4 líneas de detalle vía SP_CreateAndValidateJournalVoucherMovement: (1) débito a CounterpartCostConsignedInventoryId por ProductCost, (2) crédito a CounterpartCostConsignedInventoryId (se reutiliza la variable, debería ser InventoryCostMainAccountId) por ProductCost, (3) débito a ConsignmentMerchandiseDebitAccountId por ProductCost, (4) crédito a ConsignmentMerchandiseCreditAccountId por ProductCost; todas con ThirdPartyId del cliente y CostCenterId de la unidad funcional.; [RETURN_RESULT] resultset: Si Status=3 (anulado) retorna CodeMessage=999 ''El registro se encuentra en estado: Anulado''.; [RETURN_RESULT] resultset: Si no hay libro contable parametrizado en VieBot retorna CodeMessage=999 ''Debe parametrizar al menos un libro contable''.; [RETURN_RESULT] resultset: Si no existen productos de bodegas en consignación retorna CodeMessage=0 ''No se genero comprobante contable por que No hay productos en consigna''.; [RETURN_RESULT] resultset: En caso de éxito retorna CodeMessage=0, Message vacío y el InvoiceId; en CATCH retorna CodeMessage=999 con ERROR_MESSAGE() y línea del error.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucher_ReversalsConsignmentSale';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Billing.BasicBilling.Status = 3 (Anulado) → Retorna mensaje de advertencia y termina sin generar comprobante.; si No existe libro contable habilitado en GeneralLedger.VieBot para ''BasicBilling'' → Retorna advertencia de parametrización y termina.; si No hay BasicBillingDetail con DetailType=1 y bodega con WarehouseConsignment=1 → Retorna mensaje ''No hay productos en consigna'' y termina sin generar comprobante.; si Por cada fila del cursor: WarehouseConsignment = 1 → Recupera ProductCost del comprobante contable previo y del ProductGroup, e inserta las 4 líneas contables de reversión (CXP provisional, costo de inventario, débito y crédito de mercancía en consignación). else No genera asientos para esa línea.; si ISNULL(@Code_Output, 999) <> 0 tras invocar SP_CreateAndValidateJournalVoucherMovement → Asigna mensaje de error y retorna sin emitir resultado de éxito.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucher_ReversalsConsignmentSale';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucher_ReversalsConsignmentSale';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucher_ReversalsConsignmentSale';
-- GO
