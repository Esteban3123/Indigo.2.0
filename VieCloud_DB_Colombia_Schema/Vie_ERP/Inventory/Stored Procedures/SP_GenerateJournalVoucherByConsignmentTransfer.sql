-- ===============================================================================================================
-- Author:		Diego A. Roldán
-- Create date: 2023-06-01
-- Description:	Procedimiento que se encarga de generar el comprobante contable para el traslado en consignación de salida
-- ==============================================================================================================
CREATE PROCEDURE [Inventory].[SP_GenerateJournalVoucherByConsignmentTransfer] 
	@Id As int,
	@CodeUser as varchar(20)
AS
BEGIN
	begin try
		--Se declara una tabla con los datos para la cabecera del comprobante contable 
		declare @JournalVourcherTmp table (
			Id integer DEFAULT(0),
			Consecutive bigint DEFAULT(0),
			LegalBookId integer,
			IdJournalVoucher integer,
			VoucherDate varchar(30),
			Imported varchar(5),
			[Status] tinyint,
			Detail varchar(500),
			EntityCode  varchar(20),
			EntityId integer,
			EntityName varchar(250),
			IsClosedYear tinyint,
			CurrencyId INT
		)

		--Se declara una tabla temporal para los detalles del comprobante
		declare @JournalVourcherDetailTmp table (
			Id integer DEFAULT(0),
			IdAccounting integer DEFAULT(0),
			IdMainAccount integer,
			IdThirdParty integer,
			IdCostCenter integer,
			DebitValue decimal(20,4),
			CreditValue decimal(20,4),
			Detail varchar(500),
			IdRetention integer,
			RetentionRate decimal(5,3) DEFAULT(0),
			BaseValue decimal(18,2) DEFAULT(0),
			BillingValue decimal(18,2) DEFAULT(0)
		)

		--Tabla temporal para guardar el resultado del save del comprobante contable
		declare @resultJournalVoucher table (
			code varchar(20),
			MessageResult varchar(max),
			IdJournalVoucher integer
		)

		--Variable para obtener el xml
		declare @JournalVoucherXML as XML
		--Codigo de la remision de entrada
		declare @Code varchar(20)
		--Id de la unidad operativa de la remision
		declare @OperatingUnitId int 		
		--Se obtiene el codigo y la unidad operativa de la remision de entrada
		select @Code = Code, @OperatingUnitId = OperatingUnitId from Inventory.ConsignmentTransfer where Id = @Id
		--Id del tipo de comprobante contable
		declare @TransferBetweenWarehousesConsignmentJournalVoucherTypeId int
		--Se obtiene el tipo de comprobante contable por unidad operativa y el iva al costo
		select 
			@TransferBetweenWarehousesConsignmentJournalVoucherTypeId = TransferBetweenWarehousesConsignmentId
		from Inventory.SettingInventory 
		where OperatingUnitId = @OperatingUnitId
		
		--Se obtiene el id del libro oficial
		declare @LegalBookId int = (select top 1 Id from GeneralLedger.LegalBook where OfficialBook = 1)
		-- Detalle de la cabecera
		declare @CurrencyId int = (select top 1 cir.OfficialCurrencyId from GeneralLedger.CompanySettings cir)

		--Inserto la cabecera del comprobante contable
		insert into @JournalVourcherTmp 
			(LegalBookId, IdJournalVoucher, VoucherDate, Imported, [Status], Detail, EntityCode, EntityId, EntityName, IsClosedYear,CurrencyId)
			values 
			(
				@LegalBookId
				, @TransferBetweenWarehousesConsignmentJournalVoucherTypeId
				, [Common].[GETDATE]()
				, 'False'
				, 2
				, 'Comprobante contable generado desde traslado en consignación'
				, @Code
				, @Id
				, 'ConsignmentTransfer'
				, 0
				, @CurrencyId
			)

		--Se insertan los detalles del comprobante contable (Garantizando que se inserte exactamente lo que se registro en el kardex)
		INSERT INTO @JournalVourcherDetailTmp (IdMainAccount, IdThirdParty, DebitValue, CreditValue, Detail)
			--Unificamos Detalles Débito
			select
				ma.Id, 
				iif(ma.HandlesThirdParty = 1, s.IdThirdParty, NULL),
				sum(ctd.Quantity * ctd.ProductCost),
				0,
				'Detalle generado desde traslado en consignación'
			from Inventory.ConsignmentTransfer ct
			join Inventory.ConsignmentTransferDetail ctd on ctd.ConsignmentTransferId = ct.Id
			join Inventory.InventoryProduct pr on ctd.ProductId = pr.Id
			join Inventory.ProductGroup pg on pr.ProductGroupId = pg.Id
			join GeneralLedger.MainAccounts ma on pg.ConsignmentMerchandiseDebitAccountId = ma.Id
			join Inventory.Warehouse wh on ct.WarehouseId = wh.Id
			join Common.Supplier s on wh.SupplierId = s.Id
			where ct.Id = @Id
			group by ma.Id, ma.HandlesThirdParty, s.IdThirdParty
			union
			--Unificamos Detalles Crédito
			select
				ma.Id, 
				iif(ma.HandlesThirdParty = 1, s.IdThirdParty, NULL),
				0,
				sum(ctd.Quantity * ctd.ProductCost),
				'Detalle generado desde traslado en consignación'
			from Inventory.ConsignmentTransfer ct
			join Inventory.ConsignmentTransferDetail ctd on ctd.ConsignmentTransferId = ct.Id
			join Inventory.InventoryProduct p on ctd.ProductId = p.Id
			join Inventory.ProductGroup g on g.Id = p.ProductGroupId
			join GeneralLedger.MainAccounts ma on ma.Id = g.ConsignmentMerchandiseCreditAccountId
			join Inventory.Warehouse wh on ct.WarehouseId = wh.Id
			join Common.Supplier s on wh.SupplierId = s.Id
			where ct.Id = @Id
			group by ma.Id, ma.HandlesThirdParty, s.IdThirdParty
		
		--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
		select @JournalVoucherXML =  convert(xml, (select * 
				from @JournalVourcherTmp JournalVoucher 
				inner join @JournalVourcherDetailTmp JournalVoucherDetail on JournalVoucher.Id = JournalVoucherDetail.IdAccounting For xml AUTO,TYPE, ELEMENTS))

		--Se consume el sp que guarda el comprobante contable
		insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML, @CodeUser

		--Se valida que no hayan errores en el guardado del comprobante contable
		if (select code from @resultJournalVoucher) = '999' 
		Begin
			declare @errorJV varchar(max)
			select @errorJV = MessageResult  from @resultJournalVoucher 
			select 999 as CodeMessage, @errorJV as Message
			return
		End

		--Se genera el mensaje a devolver
		declare @Message varchar(max)
		select @Message = 'Se generó comprobante contable de tipo ' + Code + ' - ' + Name
		from GeneralLedger.JournalVoucherTypes where Id = @TransferBetweenWarehousesConsignmentJournalVoucherTypeId

		select 0 as CodeMessage, @Message as Message
	end try
	begin catch
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
	end catch
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el comprobante contable (asiento de diario) correspondiente a un traslado de mercancía en consignación de salida entre bodegas. A partir del identificador del traslado en consignación, obtiene el tipo de comprobante configurado en los parámetros del módulo de inventario para la unidad operativa, construye la cabecera y el detalle del asiento con los valores débito y crédito calculados según el costo de los productos trasladados (cantidad × costo unitario), agrupados por cuenta contable principal y tercero proveedor. Invoca el procedimiento central de contabilidad para crear y validar el comprobante, y si la operación es exitosa, registra el identificador y consecutivo generado en el traslado de consignación para su trazabilidad. Existe para garantizar que cada traslado en consignación quede respaldado automáticamente con su movimiento contable en el libro oficial de la empresa.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByConsignmentTransfer';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByConsignmentTransfer';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el comprobante contable correspondiente a un traslado de mercancía en consignación de salida, registrando los movimientos débito/crédito por cuentas configuradas en el grupo de producto.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro de traslado en consignación con el Id recibido en Inventory.ConsignmentTransfer.; Debe existir configuración en Inventory.SettingInventory para la unidad operativa del traslado, con el tipo de comprobante de traslado en consignación definido.; Debe existir un libro oficial en GeneralLedger.LegalBook (OfficialBook = 1).; Debe existir configuración de moneda oficial en GeneralLedger.CompanySettings.; La bodega del traslado debe estar asociada a un proveedor (Warehouse.SupplierId) con tercero contable.; El grupo de producto debe tener configuradas las cuentas ConsignmentMerchandiseDebitAccountId y ConsignmentMerchandiseCreditAccountId.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El comprobante siempre se crea contra el libro oficial (OfficialBook=1).; La moneda usada es siempre la moneda oficial configurada en CompanySettings.; El comprobante se crea con Status=2 e Imported=''False''.; Los valores débito y crédito del comprobante se calculan como Quantity * ProductCost del kardex del traslado, asegurando consistencia con el inventario.; Se agrupa por cuenta y tercero, garantizando un único renglón por combinación cuenta/tercero.; El tipo de comprobante depende de la unidad operativa del traslado.; La fecha del comprobante es la fecha actual obtenida de Common.GETDATE().', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Traslado en consignación; Comprobante contable; Kardex; Cuenta contable; Tercero; Proveedor; Bodega; Grupo de producto; Libro oficial; Moneda oficial; Unidad operativa; Débito; Crédito', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] GeneralLedger.JournalVouchers: Vía EXEC de GeneralLedger.SP_CreateAndValidateJournalVoucherMovement: se crea la cabecera del comprobante con el tipo TransferBetweenWarehousesConsignmentId, libro oficial, fecha actual, estado 2, EntityName=''ConsignmentTransfer'' y EntityCode = código del traslado.; [INSERT] GeneralLedger.JournalVouchers: Por cada cuenta débito (ConsignmentMerchandiseDebitAccountId del grupo de producto) se inserta un detalle con DebitValue = SUM(Quantity*ProductCost) agrupado por cuenta y tercero (proveedor de la bodega) si la cuenta maneja terceros.; [INSERT] GeneralLedger.JournalVouchers: Por cada cuenta crédito (ConsignmentMerchandiseCreditAccountId del grupo de producto) se inserta un detalle con CreditValue = SUM(Quantity*ProductCost) agrupado por cuenta y tercero (proveedor de la bodega) si la cuenta maneja terceros.; [RETURN_RESULT] @resultJournalVoucher: Si el SP de creación devuelve code=''999'', retorna CodeMessage=999 con el mensaje de error y termina sin generar comprobante.; [RETURN_RESULT] @resultJournalVoucher: Si la creación es exitosa, retorna CodeMessage=0 con mensaje ''Se generó comprobante contable tipo {Code} - {Name} con consecutivo {Consecutive}''.; [RETURN_RESULT] @resultJournalVoucher: Cualquier excepción en el TRY se captura y retorna CodeMessage=999 con ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ma.HandlesThirdParty = 1 en la cuenta contable → El detalle se asocia al tercero del proveedor de la bodega (Supplier.IdThirdParty) else El detalle se inserta con IdThirdParty NULL; si El SP GeneralLedger.SP_CreateAndValidateJournalVoucherMovement devuelve code = ''999'' → Se retorna error y se aborta el proceso sin notificar éxito else Se obtiene el JournalVoucherId y consecutivo y se construye mensaje de éxito', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ConsignmentTransfer; Inventory.ConsignmentTransferDetail; Inventory.SettingInventory; Inventory.InventoryProduct; Inventory.ProductGroup; Inventory.Warehouse; GeneralLedger.LegalBook; GeneralLedger.CompanySettings; GeneralLedger.MainAccounts; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes; Common.Supplier', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentTransfer';
-- GO
