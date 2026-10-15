
-- ===============================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 13/10/2017
-- Description:	Procedimiento que se encarga de generar el comprobante contable para la remisión de entrada
-- ========================================exec======================================================================
CREATE PROCEDURE [Inventory].[SP_GenerateJournalVoucherByRemissionEntrance] 
	@Id As int,
	@CodeUser as varchar(20)
AS
BEGIN
	
	begin try

		--Se declara una tabla con los datos para la cabecera del comprobante contable 
		declare @JournalVourcherTmp table (
				Id integer,
				Consecutive bigint,
				LegalBookId integer,
				IdJournalVoucher integer,
				VoucherDate varchar(30),
				Imported varchar(5),
				[Status] tinyint,
				Detail varchar(500),
				EntityCode varchar(20),
				EntityId integer,
				EntityName varchar(250),
				IsClosedYear tinyint,
				CurrencyId integer)

		--Se declara una tabla temporal para los detalles del comprobante
		declare @JournalVourcherDetailTmp table (
				Id integer,
				IdAccounting integer,
				IdMainAccount integer,
				IdThirdParty integer,
				IdCostCenter integer,
				DebitValue decimal(18,2),
				CreditValue decimal(18,2),
				Detail varchar(500),
				IdRetention integer,
				RetentionRate decimal(5,2),
				BaseValue decimal(18,0),
				BillingValue decimal(18,0) )

		--Tabla temporal para guardar el resultado del save del comprobante contable
		declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

		--Variable para obtener el xml
		declare @JournalVoucherXML as XML

		--Id de la unidad operativa de la remision
		declare @OperatingUnitId int 

		--Codigo de la remision de entrada
		declare @Code varchar(20) 

		--Se obtiene el codigo y la unidad operativa de la remision de entrada
		select @Code = Code, @OperatingUnitId = OperatingUnitId from Inventory.RemissionEntrance where Id = @Id

		--Permite saber si el iva va al costo
		declare @TaxRegistration TINYINT

		--Id del tipo de comprobante contable
		declare @RemissionEntranceJournalVoucherTypeId int

		--Se obtiene el tipo de comprobante contable por unidad operativa y el iva al costo
		 select @RemissionEntranceJournalVoucherTypeId = RemissionEntranceJournalVoucherTypeId, @TaxRegistration = TaxRegistration from Inventory.SettingInventory where OperatingUnitId = @OperatingUnitId
		
		if @TaxRegistration = 3
		 BEGIN
		 --se pone por defecto por pbi #18020 en iva al costo cuando es mixto
			select @TaxRegistration = 1
		 END

		--Se obntiene el id del libro oficial
		declare @LegalBookId int = (select Id from GeneralLedger.LegalBook where OfficialBook = 1)

		--Se obtiene la moneda de la remision de entrada
		declare @CurrencyId INT = (select CurrencyId from Inventory.RemissionEntrance WHERE Id = @Id)
			
		--Inserto la cabecera del comprobante contable
		insert into @JournalVourcherTmp(Id, Consecutive, LegalBookId, IdJournalVoucher, VoucherDate, Imported, [Status], Detail, EntityCode,
		EntityId, EntityName, IsClosedYear,CurrencyId)
		values(0, 0, @LegalBookId, @RemissionEntranceJournalVoucherTypeId, [Common].[GETDATE](), 'False', 2,
		'Comprobante contable generado desde Remisión de Entrada', @Code, @Id, 'RemissionEntrance', 0, @CurrencyId)

		--Se insertan los detalles del comprobante contable crédito
		insert into  @JournalVourcherDetailTmp
			select 0,0,
				ma.Id,
				case ma.HandlesThirdParty when 1 then t.Id else NULL end as ThirdPartyId ,
				NULL, --case ma.HandlesCostCenter when 1 then fu.CostCenterId else NULL end as CostCenterId, preguntar de donde se saca
				0,
				CASE @TaxRegistration 
					WHEN 1 THEN ROUND(((red.Quantity * red.GrossUnitValue) *( 1 - red.DiscountPercentage / 100) * (1 + red.IvaPercentage / 100)), 2) 
					--WHEN 1 THEN ((red.Quantity * red.GrossUnitValue) - ROUND((red.Quantity * red.GrossUnitValue * (red.DiscountPercentage / 100)), 2) * (1 + red.IvaPercentage / 100))
					ELSE ((red.Quantity * red.GrossUnitValue) - ROUND((red.Quantity * red.GrossUnitValue * (red.DiscountPercentage / 100)), 2))
					--ELSE  ROUND(((red.Quantity * red.GrossUnitValue)  * ( 1 - red.DiscountPercentage / 100)),2)
				END,
				'Detalle generado desde Remisión de Entrada ',NULL,0,0,0 
			from Inventory.RemissionEntrance re
			inner join Inventory.RemissionEntranceDetail red on red.RemissionEntranceId = re.Id
			inner join Inventory.InventoryProduct p on p.Id = red.ProductId
			inner join Inventory.ProductGroup g on g.Id = p.ProductGroupId
			inner join GeneralLedger.MainAccounts ma on ma.Id = g.ReferenceInputCreditAccountId
			inner join Common.Supplier s on s.Id = re.SupplierId
			inner join Common.ThirdParty t on t.Id = s.IdThirdParty
			where re.Id = @Id

		--Se insertan los detalles del comprobante contable débito
		insert into  @JournalVourcherDetailTmp
			select 0,0,
				ma.Id,
				case ma.HandlesThirdParty when 1 then t.Id else NULL end as ThirdPartyId ,
				NULL, --case ma.HandlesCostCenter when 1 then fu.CostCenterId else NULL end as CostCenterId, preguntar de donde se saca
				CASE @TaxRegistration 
					WHEN 1 THEN ROUND(((red.Quantity * red.GrossUnitValue) *( 1 - red.DiscountPercentage / 100) * (1 + red.IvaPercentage / 100)), 2) 
					--WHEN 1 THEN ((red.Quantity * red.GrossUnitValue) - ROUND((red.Quantity * red.GrossUnitValue * (red.DiscountPercentage / 100)), 2) * (1 + red.IvaPercentage / 100))
					ELSE ((red.Quantity * red.GrossUnitValue) - ROUND((red.Quantity * red.GrossUnitValue * (red.DiscountPercentage / 100)), 2))
					--ELSE  ROUND(((red.Quantity * red.GrossUnitValue)  * ( 1 - red.DiscountPercentage / 100)),2)
				END,
				0,
				'Detalle generado desde Remisión de Entrada ',NULL,0,0,0 
			from Inventory.RemissionEntrance re
			inner join Inventory.RemissionEntranceDetail red on red.RemissionEntranceId = re.Id
			inner join Inventory.InventoryProduct p on p.Id = red.ProductId
			inner join Inventory.ProductGroup g on g.Id = p.ProductGroupId
			inner join GeneralLedger.MainAccounts ma on ma.Id = g.ReferenceInputDebitAccountId
			inner join Common.Supplier s on s.Id = re.SupplierId
			inner join Common.ThirdParty t on t.Id = s.IdThirdParty
			where re.Id = @Id

		--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
		select @JournalVoucherXML =  convert(xml, (select * from @JournalVourcherTmp JournalVoucher 
		inner join @JournalVourcherDetailTmp JournalVoucherDetail on JournalVoucher.Id = JournalVoucherDetail.IdAccounting For xml AUTO,TYPE, ELEMENTS))

		--Se consume el sp que guarda el comprobante contable
		insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML, @CodeUser

		--Se valida que no hayan errores en el guardado del comprobante contable
		if (select code  from @resultJournalVoucher) = '999' 
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
		from GeneralLedger.JournalVoucherTypes where Id = @RemissionEntranceJournalVoucherTypeId

		--commit transaction
		select 0 as CodeMessage, @Message as Message

	end try
	begin catch

		--rollback transaction
		select 999 as CodeMessage, ERROR_MESSAGE() as Message

	end catch

END