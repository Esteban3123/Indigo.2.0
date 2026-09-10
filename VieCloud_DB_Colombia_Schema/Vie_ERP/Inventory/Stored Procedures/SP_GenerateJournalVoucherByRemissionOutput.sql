

-- ===============================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 13/10/2017
-- Description:	Procedimiento que se encarga de generar el comprobante contable para la remisión de salida
-- ==============================================================================================================
CREATE PROCEDURE [Inventory].[SP_GenerateJournalVoucherByRemissionOutput] 
	@Id As int,
	@CodeUser as varchar(20)
AS
BEGIN
	
	begin try

		--Se declara una tabla con los datos para la cabecera del comprobante contable 
		declare @JournalVourcherTmp table (Id integer ,Consecutive bigint,LegalBookId integer,IdJournalVoucher integer,VoucherDate varchar(30),
		Imported varchar(5),[Status] tinyint,Detail varchar(500),EntityCode  varchar(20),EntityId integer,EntityName varchar(250),IsClosedYear tinyint)

		--Se declara una tabla temporal para los detalles del comprobante
		declare @JournalVourcherDetailTmp table (Id integer,IdAccounting integer,IdMainAccount integer,IdThirdParty integer,IdCostCenter integer,
		DebitValue decimal(18,2),CreditValue decimal(18,2),Detail varchar(500),IdRetention integer,RetentionRate decimal(5,2),
		BaseValue decimal(18,0),BillingValue decimal(18,0) )

		--Tabla temporal para guardar el resultado del save del comprobante contable
		declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

		--Variable para obtener el xml
		declare @JournalVoucherXML as XML

		--Id de la unidad operativa de la remision
		declare @OperatingUnitId int 

		--Codigo de la remision
		declare @Code varchar(20) 

		--Se obtiene el codigo y la unidad operativa de la remision de salida
		select @Code = Code, @OperatingUnitId = OperatingUnitId from Inventory.RemissionOutput where Id = @Id

		--Permite saber si el iva va al costo
		declare @IVACost bit

		--Id del tipo de comprobante contable
		declare @RemissionOutputJournalVoucherTypeId int

		--Se obtiene el tipo de comprobante contable por unidad operativa y el iva al costo
		 select @RemissionOutputJournalVoucherTypeId = RemissionOutputJournalVoucherTypeId, @IVACost = IVACost from Inventory.SettingInventory where OperatingUnitId = @OperatingUnitId
		
		--Se obntiene el id del libro oficial
		declare @LegalBookId int = (select Id from GeneralLedger.LegalBook where OfficialBook = 1)
			
		--Inserto la cabecera del comprobante contable
		insert into @JournalVourcherTmp(Id, Consecutive, LegalBookId, IdJournalVoucher, VoucherDate, Imported, [Status], Detail, EntityCode,
		EntityId, EntityName, IsClosedYear)
		values(0, 0, @LegalBookId, @RemissionOutputJournalVoucherTypeId, [Common].[GETDATE](), 'False', 2,
		'Comprobante contable generado desde Remisión de Salida', @Code, @Id, 'RemissionOutput', 0)

		--Se insertan los detalles del comprobante contable crédito
		insert into  @JournalVourcherDetailTmp
		select 0,0,
		ma.Id,
		case ma.HandlesThirdParty when 1 then t.Id else NULL end as ThirdPartyId ,
		NULL, --case ma.HandlesCostCenter when 1 then fu.CostCenterId else NULL end as CostCenterId, preguntar de donde se saca
		0,
		rod.TotalPriceWithDiscount,
		'Detalle generado desde Remisión de Salida ',NULL,0,0,0 
		from Inventory.RemissionOutput ro
		inner join Inventory.RemissionOutputDetail rod on rod.RemissionOutputId = ro.Id
		inner join Inventory.InventoryProduct p on p.Id = rod.ProductId
		inner join Inventory.ProductGroup g on g.Id = p.ProductGroupId
		inner join GeneralLedger.MainAccounts ma on ma.Id = g.ReferenceOutputCreditAccountId
		inner join Common.Customer c on c.Id = ro.CustomerId
		inner join Common.ThirdParty t on t.Id = c.ThirdPartyId
		where ro.Id = @Id

		--Se insertan los detalles del comprobante contable débito
		insert into  @JournalVourcherDetailTmp
		select 0,0,
		ma.Id,
		case ma.HandlesThirdParty when 1 then t.Id else NULL end as ThirdPartyId ,
		NULL, --case ma.HandlesCostCenter when 1 then fu.CostCenterId else NULL end as CostCenterId, preguntar de donde se saca
		rod.TotalPriceWithDiscount,
		0,
		'Detalle generado desde Remisión de Salida ',NULL,0,0,0 
		from Inventory.RemissionOutput ro
		inner join Inventory.RemissionOutputDetail rod on rod.RemissionOutputId = ro.Id
		inner join Inventory.InventoryProduct p on p.Id = rod.ProductId
		inner join Inventory.ProductGroup g on g.Id = p.ProductGroupId
		inner join GeneralLedger.MainAccounts ma on ma.Id = g.ReferenceOutputDebitAccountId
		inner join Common.Customer c on c.Id = ro.CustomerId
		inner join Common.ThirdParty t on t.Id = c.ThirdPartyId
		where ro.Id = @Id

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

		--Se genera el mensaje a devolver
		declare @Message varchar(max) = 'Se confirmó correctamente con código ' + @Code
		select @Message = @Message + CHAR(13) + CHAR(10) + 'Se generó comprobante contable de tipo ' + Code + ' - ' + Name
		from GeneralLedger.JournalVoucherTypes where Id = @RemissionOutputJournalVoucherTypeId

		--commit transaction
		select 0 as CodeMessage, @Message as Message

	end try
	begin catch

		--rollback transaction
		select 999 as CodeMessage, ERROR_MESSAGE() as Message

	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el comprobante contable (asiento de diario) correspondiente a una remisión de salida de inventario, a partir del identificador de la remisión. Consulta la configuración contable del módulo de inventario para obtener el tipo de comprobante y si el IVA va al costo, luego construye la cabecera y los renglones de crédito y débito usando las cuentas contables de referencia de salida definidas por grupo de producto. Invoca el procedimiento GeneralLedger.SP_CreateAndValidateJournalVoucherMovement con el asiento en formato XML, valida que no haya errores y retorna el consecutivo del comprobante generado junto con el código de la remisión. Se usa en el cierre contable de despachos o entregas de productos desde almacén, garantizando que cada remisión de salida quede respaldada por su movimiento contable en el libro oficial.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByRemissionOutput';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByRemissionOutput';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el comprobante contable asociado a una remisión de salida de inventario, construyendo cabecera y detalles débito/crédito según las cuentas configuradas en el grupo del producto, y delegando su persistencia al SP contable central.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionOutput';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La remisión de salida debe existir en Inventory.RemissionOutput con su Code y OperatingUnitId.; La unidad operativa de la remisión debe tener configuración en Inventory.SettingInventory con RemissionOutputJournalVoucherTypeId definido.; Debe existir un libro oficial (OfficialBook = 1) en GeneralLedger.LegalBook.; Cada producto de la remisión debe pertenecer a un ProductGroup con cuentas ReferenceOutputCreditAccountId y ReferenceOutputDebitAccountId configuradas.; El cliente de la remisión debe tener un ThirdParty asociado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionOutput';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El comprobante contable se genera siempre contra el libro oficial (LegalBook.OfficialBook = 1).; El tipo de comprobante usado proviene exclusivamente de SettingInventory.RemissionOutputJournalVoucherTypeId de la unidad operativa de la remisión.; Para cada línea de detalle se generan dos asientos balanceados (un crédito y un débito) por el mismo valor TotalPriceWithDiscount.; El IdCostCenter en los detalles contables siempre se inserta como NULL (no se determina centro de costo).; El estado de la cabecera del comprobante se inserta con Status=2 e Imported=''False''.; EntityName se fija en ''RemissionOutput'' y EntityId en el Id de la remisión, manteniendo trazabilidad con el origen.; Cualquier excepción no aborta con error SQL; se captura y se devuelve como CodeMessage=999.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionOutput';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Remisión de salida; Comprobante contable; Libro oficial; Cuenta contable de débito/crédito; Grupo de producto; Tercero; Cliente; Unidad operativa; IVA al costo; Consecutivo contable', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionOutput';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] GeneralLedger.JournalVouchers: A través de EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement se crea un comprobante contable tipo RemissionOutputJournalVoucherTypeId, con EntityName=''RemissionOutput'', EntityId=@Id, EntityCode=Code de la remisión y detalle ''Comprobante contable generado desde Remisión de Salida''.; [INSERT] GeneralLedger.JournalVouchers: Por cada línea de RemissionOutputDetail se inserta un movimiento crédito en la cuenta ReferenceOutputCreditAccountId del ProductGroup por TotalPriceWithDiscount, asignando ThirdParty del cliente solo si la cuenta HandlesThirdParty=1.; [INSERT] GeneralLedger.JournalVouchers: Por cada línea de RemissionOutputDetail se inserta un movimiento débito en la cuenta ReferenceOutputDebitAccountId del ProductGroup por TotalPriceWithDiscount, asignando ThirdParty del cliente solo si la cuenta HandlesThirdParty=1.; [RETURN_RESULT] @resultJournalVoucher: Si el SP de guardado retorna code=''999'', devuelve SELECT 999 as CodeMessage y el MessageResult del error y termina.; [RETURN_RESULT] @resultJournalVoucher: En éxito devuelve SELECT 0 as CodeMessage y un mensaje con el código de la remisión, tipo de comprobante (Code-Name de JournalVoucherTypes) y consecutivo generado.; [RETURN_RESULT] @resultJournalVoucher: En el bloque CATCH devuelve SELECT 999 as CodeMessage con ERROR_MESSAGE() como Message.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionOutput';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ma.HandlesThirdParty = 1 (en cuenta principal del grupo del producto) → Asigna IdThirdParty con el ThirdParty del cliente de la remisión en el detalle contable. else IdThirdParty queda NULL.; si (select code from @resultJournalVoucher) = ''999'' → Retorna error 999 con el mensaje de SP_CreateAndValidateJournalVoucherMovement y termina la ejecución. else Continúa para construir mensaje de éxito con consecutivo del comprobante.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionOutput';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionOutput';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.RemissionOutput; Inventory.SettingInventory; GeneralLedger.LegalBook; Inventory.RemissionOutputDetail; Inventory.InventoryProduct; Inventory.ProductGroup; GeneralLedger.MainAccounts; Common.Customer; Common.ThirdParty; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionOutput';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionOutput';
-- GO
