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

		--Se obtiene el id que genero el comprobante contable
		declare @JournalVoucherId as int = (select IdJournalVoucher  from @resultJournalVoucher)
		--Se obtiene el consecutivo que generó el comprobante contable
		declare @Consecutive as varchar(max) = isnull((select cast( Consecutive as varchar(30))  from GeneralLedger.JournalVouchers where id = @JournalVoucherId ), 0)
		--Se genera el mensaje a devolver
		declare @Message varchar(max)
		select @Message = 'Se generó comprobante contable tipo ' + Code + ' - ' + Name + ' con consecutivo ' + @Consecutive 
		from GeneralLedger.JournalVoucherTypes where Id = @TransferBetweenWarehousesConsignmentJournalVoucherTypeId

		select 0 as CodeMessage, @Message as Message
	end try
	begin catch
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
	end catch
END