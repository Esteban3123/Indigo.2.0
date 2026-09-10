
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

		--Se genera el mensaje a devolver
		declare @Message varchar(max) = 'Se confirmó correctamente con código ' + @Code
		select @Message = @Message + CHAR(13) + CHAR(10) + 'Se generó comprobante contable de tipo ' + Code + ' - ' + Name
		from GeneralLedger.JournalVoucherTypes where Id = @ConsignmentInventoryRemissionJournalVoucherTypeId

		select 0 as CodeMessage, @Message as Message
	end try
	begin catch

		select 999 as CodeMessage, ERROR_MESSAGE() as Message

	end catch
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el comprobante contable (asiento de diario) correspondiente a una remisión de inventario en consignación. A partir del ID de la remisión, obtiene los parámetros contables configurados para la unidad operativa (tipo de comprobante y tratamiento del IVA al costo), construye la cabecera y el detalle del comprobante agrupando débitos y créditos por cuenta contable y tercero según los productos y grupos de inventario involucrados, y finalmente invoca el procedimiento de contabilidad general para crear y validar el movimiento contable. Existe para garantizar que cada ingreso de mercancía de proveedores en modalidad de consignación quede reflejado automáticamente en el libro oficial de contabilidad.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByConsignmentInventoryRemission';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByConsignmentInventoryRemission';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera y persiste el comprobante contable asociado a una remisión de inventario en consignación, construyendo cabecera y detalles débito/crédito a partir de las cuentas configuradas en el grupo de producto, y delegando la creación al SP contable central.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentInventoryRemission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La remisión en Inventory.ConsignmentInventoryRemission identificada debe existir y tener Code, OperatingUnitId, SupplierId y CurrencyId.; Inventory.SettingInventory debe estar parametrizada para la OperatingUnitId de la remisión, definiendo ConsignmentMerchandiseJournalVoucherTypeId e IVACost.; Debe existir un libro oficial en GeneralLedger.LegalBook con OfficialBook = 1.; Cada ProductGroup involucrado debe tener configuradas las cuentas ConsignmentMerchandiseDebitAccountId y ConsignmentMerchandiseCreditAccountId en GeneralLedger.MainAccounts.; Cuando la cuenta principal tenga HandlesThirdParty = 1, el proveedor (Common.Supplier) debe tener IdThirdParty asociado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentInventoryRemission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El comprobante siempre se registra contra el libro marcado como OfficialBook = 1.; El tipo de comprobante usado siempre proviene de SettingInventory.ConsignmentMerchandiseJournalVoucherTypeId para la unidad operativa de la remisión.; La cabecera siempre se inserta con Imported=''False'', Status=2, IsClosedYear=0 y EntityName=''ConsignmentInventoryRemission''.; Los valores debe/haber se calculan siempre como SUM(Quantity * UnitValue) de los detalles de la remisión, garantizando consistencia con lo registrado en kardex.; Las líneas se agrupan por cuenta principal y, condicionalmente, por tercero (proveedor), evitando duplicar registros por producto.; La fecha del comprobante se toma siempre de Common.GETDATE() y no de la remisión.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentInventoryRemission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Remisión de inventario en consignación; Comprobante contable; Libro oficial; Cuentas contables débito/crédito de mercancía en consignación; Tercero / proveedor; Grupo de producto; Kardex / valor unitario; Unidad operativa; IVA al costo', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentInventoryRemission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] GeneralLedger.JournalVouchers: Vía EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement con el XML armado, se inserta la cabecera del comprobante con LegalBookId del libro oficial, tipo = ConsignmentMerchandiseJournalVoucherTypeId, fecha = Common.GETDATE(), Imported=''False'', Status=2, EntityCode=Code de la remisión, EntityId=@Id, EntityName=''ConsignmentInventoryRemission'', IsClosedYear=0 y CurrencyId de la remisión.; [INSERT] GeneralLedger.JournalVouchers: Se generan líneas DÉBITO agrupadas por cuenta principal (ConsignmentMerchandiseDebitAccountId del ProductGroup) y, si HandlesThirdParty=1, por IdThirdParty del proveedor, con valor SUM(Quantity*UnitValue) de los detalles de la remisión.; [INSERT] GeneralLedger.JournalVouchers: Se generan líneas CRÉDITO agrupadas por ConsignmentMerchandiseCreditAccountId del ProductGroup y, si HandlesThirdParty=1, por IdThirdParty del proveedor, con valor SUM(Quantity*UnitValue).; [RETURN_RESULT] Resultset: Si la creación del comprobante retorna code=''999'' se devuelve (CodeMessage=999, Message=mensaje de error) y se aborta; en éxito se devuelve (CodeMessage=0, Message) con el código de la remisión, tipo y consecutivo del comprobante.; [RETURN_RESULT] Resultset: Cualquier excepción capturada en CATCH retorna CodeMessage=999 y ERROR_MESSAGE() como Message.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentInventoryRemission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ma.HandlesThirdParty = 1 en la cuenta principal débito/crédito → La línea contable se asocia al IdThirdParty del proveedor (Common.Supplier) else El tercero de la línea queda en NULL; si El SP GeneralLedger.SP_CreateAndValidateJournalVoucherMovement devuelve code = ''999'' → Se retorna inmediatamente CodeMessage=999 con el mensaje de error y no se continúa con el flujo de éxito else Se obtiene el IdJournalVoucher y consecutivo y se construye mensaje de éxito', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentInventoryRemission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentInventoryRemission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ConsignmentInventoryRemission; Inventory.ConsignmentInventoryRemissionDetail; Inventory.SettingInventory; Inventory.InventoryProduct; Inventory.ProductGroup; GeneralLedger.LegalBook; GeneralLedger.MainAccounts; Common.Supplier; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentInventoryRemission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByConsignmentInventoryRemission';
-- GO
