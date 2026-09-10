
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

		--Se genera el mensaje a devolver
		declare @Message varchar(max) = 'Se confirmó correctamente con código ' + @Code
		select @Message = @Message + CHAR(13) + CHAR(10) + 'Se generó comprobante contable de tipo ' + Code + ' - ' + Name
		from GeneralLedger.JournalVoucherTypes where Id = @RemissionEntranceJournalVoucherTypeId

		--commit transaction
		select 0 as CodeMessage, @Message as Message

	end try
	begin catch

		--rollback transaction
		select 999 as CodeMessage, ERROR_MESSAGE() as Message

	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el comprobante contable (asiento de diario) correspondiente a una remisión de entrada de inventario, es decir, cuando se recibe mercancía o insumos de un proveedor en el almacén. Toma el identificador de la remisión de entrada, consulta su código, unidad operativa y moneda, y obtiene de la configuración de inventario el tipo de comprobante contable asignado para remisiones, así como si el IVA debe ir al costo o registrarse por separado. Con esa información construye la cabecera y el detalle del comprobante (débitos y créditos por producto, grupo contable, tercero proveedor y valores brutos con descuento e IVA), lo serializa en XML y lo persiste en el libro oficial de contabilidad (GeneralLedger). Existe para automatizar la integración entre el módulo de inventario y la contabilidad general cada vez que se confirma una entrada de mercancía por remisión.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByRemissionEntrance';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByRemissionEntrance';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el comprobante contable (cabecera y detalles débito/crédito) asociado a una remisión de entrada de inventario, invocando el SP de contabilidad para validarlo y persistirlo.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionEntrance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La remisión de entrada debe existir y tener OperatingUnitId y CurrencyId definidos.; Debe existir configuración en Inventory.SettingInventory para la unidad operativa con RemissionEntranceJournalVoucherTypeId y TaxRegistration.; Debe existir un libro oficial (OfficialBook=1) en GeneralLedger.LegalBook.; Cada producto de la remisión debe pertenecer a un ProductGroup con ReferenceInputCreditAccountId y ReferenceInputDebitAccountId definidas.; El proveedor de la remisión debe estar asociado a un ThirdParty.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionEntrance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El comprobante se registra siempre en el libro oficial (OfficialBook=1).; La cabecera se crea con Status=2 e Imported=''False''.; Para cada ítem de la remisión se generan exactamente dos asientos: uno débito y uno crédito por el mismo valor, garantizando partida doble.; El IVA solo se incorpora al valor contable cuando TaxRegistration=1; el caso 3 se trata como 1.; El centro de costo siempre se inserta como NULL (regla pendiente en el código).; La moneda del comprobante es la misma de la remisión de entrada.; La entidad origen del comprobante se etiqueta como ''RemissionEntrance'' con el Id de la remisión.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionEntrance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Remisión de entrada; Comprobante contable; Libro oficial; Plan de cuentas (cuenta débito/crédito de entrada); IVA al costo; Tercero / Proveedor; Grupo de productos; Descuento comercial; Partida doble; Consecutivo contable', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionEntrance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] GeneralLedger.JournalVouchers: Se genera comprobante contable mediante GeneralLedger.SP_CreateAndValidateJournalVoucherMovement con cabecera tipo RemissionEntranceJournalVoucherTypeId, libro oficial, fecha actual, estado 2 (''False'' importado) y entidad ''RemissionEntrance'' referenciando la remisión.; [INSERT] GeneralLedger.JournalVouchers: Por cada detalle de la remisión se inserta una línea CRÉDITO en la cuenta ReferenceInputCreditAccountId del grupo del producto y una línea DÉBITO en ReferenceInputDebitAccountId.; [INSERT] GeneralLedger.JournalVouchers: Si la cuenta tiene HandlesThirdParty=1 se asigna el ThirdParty del proveedor de la remisión; en caso contrario el tercero queda NULL.; [INSERT] GeneralLedger.JournalVouchers: Si TaxRegistration=1 (IVA al costo) el valor del movimiento = ROUND(Quantity*GrossUnitValue*(1-DiscountPercentage/100)*(1+IvaPercentage/100),2); en otro caso = (Quantity*GrossUnitValue) - ROUND(Quantity*GrossUnitValue*(DiscountPercentage/100),2) sin IVA.; [RETURN_RESULT] @resultJournalVoucher: Si el SP de contabilidad retorna code=''999'' se devuelve CodeMessage=999 con el mensaje de error y se aborta el flujo.; [RETURN_RESULT] @resultJournalVoucher: Si la generación es exitosa se devuelve CodeMessage=0 con mensaje que incluye el código de la remisión y el tipo+consecutivo del comprobante contable generado.; [RETURN_RESULT] @resultJournalVoucher: Ante cualquier excepción se devuelve CodeMessage=999 con ERROR_MESSAGE() (no hay rollback explícito implementado).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionEntrance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TaxRegistration = 3 (IVA mixto) → Se fuerza TaxRegistration = 1 (IVA al costo) por defecto según PBI #18020. else Se conserva el valor original de TaxRegistration.; si TaxRegistration = 1 → El valor contable incluye el IVA dentro del costo: ROUND(Q*PU*(1-Desc%)*(1+IVA%),2). else El valor contable se calcula sin IVA: (Q*PU) - ROUND(Q*PU*Desc%,2).; si MainAccount.HandlesThirdParty = 1 → Se registra el tercero del proveedor en el detalle. else El tercero del detalle queda NULL.; si Resultado del SP de comprobante con code = ''999'' → Retorna error 999 con el mensaje y termina la ejecución. else Continúa y construye el mensaje de éxito con el consecutivo generado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionEntrance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionEntrance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.RemissionEntrance; Inventory.SettingInventory; GeneralLedger.LegalBook; Inventory.RemissionEntranceDetail; Inventory.InventoryProduct; Inventory.ProductGroup; GeneralLedger.MainAccounts; Common.Supplier; Common.ThirdParty; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionEntrance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByRemissionEntrance';
-- GO
