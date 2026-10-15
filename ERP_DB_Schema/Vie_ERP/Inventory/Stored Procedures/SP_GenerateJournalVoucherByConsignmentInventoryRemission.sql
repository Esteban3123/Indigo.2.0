
-- ===============================================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2017-12-12
-- Description:	Procedimiento que se encarga de generar el comprobante contable para la remisión de inventario en consignacion
-- ==============================================================================================================
CREATE PROCEDURE [Inventory].[SP_GenerateJournalVoucherByConsignmentInventoryRemission] 
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
		select 
			@Code = Code, 
			@OperatingUnitId = OperatingUnitId 
		from Inventory.ConsignmentInventoryRemission 
		where Id = @Id

		--Permite saber si el iva va al costo
		declare @IVACost bit

		--Id del tipo de comprobante contable
		declare @ConsignmentInventoryRemissionJournalVoucherTypeId int

		--Se obtiene el tipo de comprobante contable por unidad operativa y el iva al costo
		 select 
			@ConsignmentInventoryRemissionJournalVoucherTypeId = ConsignmentMerchandiseJournalVoucherTypeId, 
			@IVACost = IVACost 
		from Inventory.SettingInventory 
		where OperatingUnitId = @OperatingUnitId
		
		--Se obtiene el id del libro oficial
		declare @LegalBookId int = (select Id from GeneralLedger.LegalBook where OfficialBook = 1)
			
		-- Detalle de la cabecera
		DECLARE @detail VARCHAR(500) = 'Comprobante contable generado desde Remisión de Inventario en Consignación'

		DECLARE @CurrencyId INT = (SELECT CurrencyId FROM Inventory.ConsignmentInventoryRemission where id = @Id)

		--Inserto la cabecera del comprobante contable
		insert into @JournalVourcherTmp (
										 LegalBookId, 
										 IdJournalVoucher, 
										 VoucherDate, 
										 Imported, 
										 [Status], 
										 Detail, 
										 EntityCode, 
										 EntityId, 
										 EntityName, 
										 IsClosedYear,
										 CurrencyId)
			values (
					@LegalBookId, 
					@ConsignmentInventoryRemissionJournalVoucherTypeId, 
					[Common].[GETDATE](), 
					'False', 
					2, 
					@detail, 
					@Code, 
					@Id, 
					'ConsignmentInventoryRemission', 
					0,
					@CurrencyId)
			
		SET @detail = 'Detalle generado desde Remisión de Inventario en Consignación'

		--Se insertan los detalles del comprobante contable (Garantizando que se inserte exactamente lo que se registro en el kardex)
		INSERT INTO @JournalVourcherDetailTmp (IdMainAccount, IdThirdParty, DebitValue, CreditValue, Detail)
			--Unificamos Detalles Débito
			SELECT
				ma.Id, 
				IIF(ma.HandlesThirdParty = 1, s.IdThirdParty, NULL),
				SUM(cird.Quantity * cird.UnitValue),
				0,
				@detail
			FROM Inventory.ConsignmentInventoryRemission cir
			INNER JOIN Inventory.ConsignmentInventoryRemissionDetail cird on cird.ConsignmentInventoryRemissionId = cir.id 
			INNER JOIN Inventory.InventoryProduct p ON cird.ProductId = p.Id
			INNER JOIN Inventory.ProductGroup g on g.Id = p.ProductGroupId
			INNER JOIN GeneralLedger.MainAccounts ma on ma.Id = g.ConsignmentMerchandiseDebitAccountId
			INNER JOIN Common.Supplier s ON cir.SupplierId = s.Id
			WHERE cir.Id = @Id
			GROUP BY ma.Id, ma.HandlesThirdParty, s.IdThirdParty
			UNION
			--Unificamos Detalles Crédito
			SELECT
				ma.Id, 
				IIF(ma.HandlesThirdParty = 1, s.IdThirdParty, NULL),
				0,
				SUM(cird.Quantity * cird.UnitValue),
				@detail
			FROM Inventory.ConsignmentInventoryRemission cir
			INNER JOIN Inventory.ConsignmentInventoryRemissionDetail cird on cird.ConsignmentInventoryRemissionId = cir.id 
			INNER JOIN Inventory.InventoryProduct p ON cird.ProductId = p.Id
			INNER JOIN Inventory.ProductGroup g on g.Id = p.ProductGroupId
			INNER JOIN GeneralLedger.MainAccounts ma on ma.Id = g.ConsignmentMerchandiseCreditAccountId
			INNER JOIN Common.Supplier s ON cir.SupplierId = s.Id
			WHERE cir.Id = @Id
			GROUP BY ma.Id, ma.HandlesThirdParty, s.IdThirdParty

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
		declare @Consecutive as varchar(max) = isnull( (select cast( Consecutive as varchar(30))  from GeneralLedger.JournalVouchers where id = @JournalVoucherId ),0)

		--Se genera el mensaje a devolver
		declare @Message varchar(max) = 'Se confirmó correctamente con código ' + @Code
		select @Message = @Message + CHAR(13) + CHAR(10) + 'Se generó comprobante contable tipo ' + Code + ' - ' + Name + ' con consecutivo ' + @Consecutive 
		from GeneralLedger.JournalVoucherTypes where Id = @ConsignmentInventoryRemissionJournalVoucherTypeId

		select 0 as CodeMessage, @Message as Message
	end try
	begin catch

		select 999 as CodeMessage, ERROR_MESSAGE() as Message

	end catch
END