-- ===============================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 15/01/2020
-- Description:	Procedimiento que se encarga del proceso de devolución parcial de ventas 
-- ===============================================================================================================================
CREATE PROCEDURE [Billing].[SP_SaveDocumentInvoiceProductSalesDevolution]
	@Xml xml,
	@UserCode varchar(20)
AS
BEGIN
	
	--Variables para guardar la cabecera
	declare @Id int, @Code varchar(20), @DocumentDate datetime, @WarehouseId int, @Detail varchar(300), @DocumentInvoiceProductSalesId int, @FreightValue numeric(20,4), @FreightIVAPercentage numeric(5,2),
	@FreightIVAValue numeric(20,4), @Value numeric(20,4), @ValueDiscount numeric(20,4), @ValueTax numeric(20,4), @WithholdingTax numeric(20,4), @WithholdingICA numeric(20,4), @RetentionSource numeric(20,4),
	@RetentionOther numeric(20,4), @DeductionOther numeric(20,4), @DistrictTax numeric(20,4), @TotalValue numeric(20,4), @Status tinyint, @OperatingUnitId int, @CompanyType tinyint

	--Tabla de detalles de la devolución
	declare @TableDocumentInvoiceProductSalesDevolutionDetail table(Id int, DocumentInvoiceProductSalesDevolutionId int, DocumentInvoiceProductSalesDetailBatchSerialId int, Quantity int, IsDelete bit,
	SubTotalValue numeric(20,4), DiscountValue numeric(20,4), RTFValue numeric(20,4), RTFPercentage numeric(5,2))
	
	--Tabla en donde se almacena la cabecera de la nota
	declare @TablePortfolioNote table(Id int, Code varchar(20), NoteDate datetime, CustomerId int, Observations varchar(max), Nature tinyint, NoteType tinyint, OperatingUnitId int, Status tinyint)

	declare @TablePortfolioNoteAccountReceivableAdvance table(Id int, PortfolioNoteId int, AccountReceivableId int, MainAccountId int, AccountReceivableAccountingId int, AdjusmentValue numeric(20,2), 
	PercentageValue numeric(5,2), PreviousBalance numeric(20,2), Balance numeric(20,2), ChangeTracker varchar(30))

	declare @TablePortfolioNoteDetail table(Id int, PortfolioNoteId int, PortfolioNoteConceptId int, MainAccountId int, ThirdPartyId int, CostCenterId int, Nature tinyint, Value numeric(20,2), 
	RetentionConceptId int, BaseValue decimal(20,2), Percentage numeric(5,2), Observations varchar(3000), ChangeTracker varchar(30))

	--Id del cliente
	declare @CustomerId int

	--Id del concepto de nota
	declare @InvoiceProductDevolutionPartialConceptNoteId int

	--Permite saber si se asigna centro costo, este campo se saca de parametros de facturacion
	declare @AssociateCostCenter tinyint

	--Cuenta contable retención iva se obtiene de parámetros de facturación
	declare @ReteIVAMainAccountId int

	--Concepto de reteIva - se obtiene de parámetros de facturación
	declare @ReteIVAConceptId int

	--Cuenta contable retención ica se obtiene de parámetros de facturación
	declare @ReteICAMainAccountId int

	--Cuenta contable retención en la fuente se obtiene de parámetros de facturación
	declare @ReteFuenteMainAccountId int

	--Cuenta contable para el iva se obtiene de parámetros de facturación
	declare @IVAPaymentMainAccountId int

	--Permite saber si maneja tercero la cuenta contable
	declare @HandlesThirdParty bit

	--Permite saber si maneja centro costo
	declare @HandlesCostCenter bit

	--Valor base para el calculo de retenciones
	declare @BaseValue numeric(20, 4)

	--Xml que representa a las tablas de notas
	declare @PortfolioNoteXML xml

	--Xml para enviar al proceso de Kardex
	declare @KardexXML xml

	--Variables para el resultado del proceso de nota
	declare @CodeResultNote int, @MessageResultNote varchar(MAX), @NoteId int, @NoteCode varchar(20)

	--Variables para el resultado del proceso de kardex
	declare @CodeResultKardex int, @MessageResultKardex varchar(MAX)

	--Mensaje que devuelve las diferentes acciones
	declare @MessageReturn varchar(300)

	--Obtiene los errores de algunas validaciones
	declare @Errors varchar(max) = ''

	Begin try
	
		--Si se esta anulando
		if @Status = 3
		begin
			update Inventory.DocumentInvoiceProductSalesDevolution set Status = 3 where Id = @Id
			select 0 as CodeMessage, 'El registro con código ' + @Code + ' se anuló correctamente' as Message, @Id DevolutionId, @Code DevolutionCode
			return
		end

		--Se obtienen los datos del xml para la cabecera
		select 
			@Id = t.x.value('Id[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
			@WarehouseId = t.x.value('WarehouseId[1]','int'),
			@Detail = IIF(t.x.value('Detail[1]','varchar(300)') = '', null, t.x.value('Detail[1]','varchar(300)')),
			@DocumentInvoiceProductSalesId = t.x.value('DocumentInvoiceProductSalesId[1]','int'),
			@FreightValue = REPLACE(t.x.value('FreightValue[1]','varchar(20)'), ',', '.'),
			@FreightIVAPercentage = REPLACE(t.x.value('FreightIVAPercentage[1]','varchar(20)'), ',', '.'),
			@FreightIVAValue = REPLACE(t.x.value('FreightIVAValue[1]','varchar(20)'), ',', '.'),
			@Value = REPLACE(t.x.value('Value[1]','varchar(20)'), ',', '.'),
			@ValueDiscount = REPLACE(t.x.value('ValueDiscount[1]','varchar(20)'), ',', '.'),
			@ValueTax = REPLACE(t.x.value('ValueTax[1]','varchar(20)'), ',', '.'),
			@WithholdingTax = REPLACE(t.x.value('WithholdingTax[1]','varchar(20)'), ',', '.'),
			@WithholdingICA = REPLACE(t.x.value('WithholdingICA[1]','varchar(20)'), ',', '.'),
			@RetentionSource = REPLACE(t.x.value('RetentionSource[1]','varchar(20)'), ',', '.'),
			@RetentionOther = REPLACE(t.x.value('RetentionOther[1]','varchar(20)'), ',', '.'),
			@DeductionOther = REPLACE(t.x.value('DeductionOther[1]','varchar(20)'), ',', '.'),
			@DistrictTax = REPLACE(t.x.value('DistrictTax[1]','varchar(20)'), ',', '.'),
			@TotalValue = REPLACE(t.x.value('TotalValue[1]','varchar(20)'), ',', '.'),
			@Status = t.x.value('Status[1]','tinyint'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@CompanyType = t.x.value('CompanyType[1]','tinyint')
		from @Xml.nodes('/DocumentInvoiceProductSalesDevolution') t(x)

		--Se obtienen los detalles del xml
		insert into @TableDocumentInvoiceProductSalesDevolutionDetail
		select 
			t.x.value('Id[1]','int') as Id,
			t.x.value('DocumentInvoiceProductSalesDevolutionId[1]','int') as DocumentInvoiceProductSalesDevolutionId,
			t.x.value('DocumentInvoiceProductSalesDetailBatchSerialId[1]','int') as DocumentInvoiceProductSalesDetailBatchSerialId,
			t.x.value('Quantity[1]','int') as Quantity,
			t.x.value('IsDelete[1]','bit') as IsDelete,
			REPLACE(t.x.value('SubTotalValue[1]','varchar(20)'), ',', '.') as SubTotalValue,
			REPLACE(t.x.value('DiscountValue[1]','varchar(20)'), ',', '.') as DiscountValue,
			REPLACE(t.x.value('RTFValue[1]','varchar(20)'), ',', '.') as RTFValue,
			REPLACE(t.x.value('RTFPercentage[1]','varchar(20)'), ',', '.') as RTFPercentage
		from @Xml.nodes('/DocumentInvoiceProductSalesDevolution/DocumentInvoiceProductSalesDevolutionDetail') t(x)
		
		--Se eliminan los detalles
		delete from Inventory.DocumentInvoiceProductSalesDevolutionDetail where Id in (select Id from @TableDocumentInvoiceProductSalesDevolutionDetail where Id > 0 and IsDelete = 1)
		delete from @TableDocumentInvoiceProductSalesDevolutionDetail where Id > 0 and IsDelete = 1

		--Si no viene el código se genera
		if @Code = '' or @Code is null
		begin
			--Consultamos si la secuencia es con O o OU
			declare @scope varchar(5) = ''
			declare @idSequenceDetail int
			declare @pattern varchar(300)
			declare @NextS int
			select @scope = Scope from Billing.BillingSequence
			where IdForm = '2100'

			if @scope = 'O' --Si el ambito es por organización
			begin
				select top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
				from Billing.BillingSequenceDetail bsd 
				inner join Billing.BillingSequence bs on bs.Id = bsd.IdSequenseBillingC
				inner join Common.Sequense cs on cs.Id = bsd.IdSequense
				where bs.IdForm = '2100'
				order by bsd.Next desc
			end
			else begin --Si el ambito es por unidad operativa
				select top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
				from Billing.BillingSequenceDetail bsd 
				inner join Billing.BillingSequence bs on bs.Id = bsd.IdSequenseBillingC
				inner join Common.Sequense cs on cs.Id = bsd.IdSequense
				where bs.IdForm = '2100' and bsd.IdOperatingUnit = @OperatingUnitId
				order by bsd.Next desc
			end
					
			if (@idSequenceDetail is null)
			Begin
				select 999 as CodeMessage, 'Secuencia no encontrada para generar la devolución' as Message, 0 DevolutionId, '' DevolutionCode
				return
			End

			select @Code = dbo.GetSequence('',@pattern,@NextS)
			update Billing.BillingSequenceDetail set [Next] += 1 where Id = @idSequenceDetail
		end

		if @Id = 0 or @Id is null --Se guarda la cabecera
		begin
			insert into [Inventory].[DocumentInvoiceProductSalesDevolution]([Code], [DocumentDate], [WarehouseId], [Detail], [DocumentInvoiceProductSalesId], [FreightValue], [FreightIVAPercentage], 
			[FreightIVAValue], [Value], [ValueDiscount], [ValueTax], [WithholdingTax], [WithholdingICA], [RetentionSource], [RetentionOther], [DeductionOther], [DistrictTax], [TotalValue], [Status],
			[CreationUser], [CreationDate], [ConfirmationUser], [ConfirmationDate])
			values(@Code, @DocumentDate, @WarehouseId, @Detail, @DocumentInvoiceProductSalesId, @FreightValue, @FreightIVAPercentage, @FreightIVAValue, @Value, @ValueDiscount, @ValueTax, @WithholdingTax,
			@WithholdingICA, @RetentionSource, @RetentionOther, @DeductionOther, @DistrictTax, @TotalValue, @Status, 
			@UserCode, [Common].[GETDATE](), IIF(@Status = 2, @UserCode, null), IIF(@Status = 2, [Common].[GETDATE](), null))

			set @Id = SCOPE_IDENTITY()
		end
		else begin --Se actualiza la cabecera
			update [Inventory].[DocumentInvoiceProductSalesDevolution] set [Code] = @Code, [DocumentDate] = @DocumentDate, [WarehouseId] = @WarehouseId, [Detail] = @Detail,
			[DocumentInvoiceProductSalesId] = @DocumentInvoiceProductSalesId, [FreightValue] = @FreightValue, [FreightIVAPercentage] = @FreightIVAPercentage, [FreightIVAValue] = @FreightIVAValue, 
			[Value] = @Value, [ValueDiscount] = @ValueDiscount, [ValueTax] = @ValueTax, [WithholdingTax] = @WithholdingTax, [WithholdingICA] = @WithholdingICA, [RetentionSource] = @RetentionSource, 
			[RetentionOther] = @RetentionOther, [DeductionOther] = @DeductionOther, [DistrictTax] = @DistrictTax, [TotalValue] = @TotalValue, [Status] = @Status, 
			[ModificationUser] = IIF(@Status = 1, @UserCode, null), [ModificationDate] = IIF(@Status = 1, [Common].[GETDATE](), null), 
			[ConfirmationUser] = IIF(@Status = 2, @UserCode, null), [ConfirmationDate] = IIF(@Status = 2, [Common].[GETDATE](), null), 
			[AnnulmentUser] = IIF(@Status = 3, @UserCode, null), [AnnulmentDate] = IIF(@Status = 3, [Common].[GETDATE](), null)
			where Id = @Id
		end

		--Se guardan los detalles
		insert into [Inventory].[DocumentInvoiceProductSalesDevolutionDetail]([DocumentInvoiceProductSalesDevolutionId], [DocumentInvoiceProductSalesDetailBatchSerialId], [Quantity])
		select @Id, DocumentInvoiceProductSalesDetailBatchSerialId, Quantity
		from @TableDocumentInvoiceProductSalesDevolutionDetail
		where Id = 0 and IsDelete = 0
			
		--Se actualizan los detalles
		update d set d.DocumentInvoiceProductSalesDetailBatchSerialId = t.DocumentInvoiceProductSalesDetailBatchSerialId, d.Quantity = t.Quantity
		from @TableDocumentInvoiceProductSalesDevolutionDetail t
		inner join Inventory.DocumentInvoiceProductSalesDevolutionDetail d on d.Id = t.Id
		where t.Id > 0 and t.IsDelete = 0

		-- se determina si es una devolucion parcial o total
		DECLARE @DevolutionQuantity INT,
		--total 1; partial 0
				@PartialOrTotalDevolution BIT

				SELECT @DevolutionQuantity = SUM(t.Quantity)
				FROM @TableDocumentInvoiceProductSalesDevolutionDetail t
				WHERE t.IsDelete = 0
		
		if @DevolutionQuantity<(SELECT SUM(dips.OutstandingQuantity)
								FROM @TableDocumentInvoiceProductSalesDevolutionDetail t
								INNER JOIN Inventory.DocumentInvoiceProductSalesDetailBatchSerial dips WITH (NOLOCK) on  t.DocumentInvoiceProductSalesDetailBatchSerialId = dips.Id
								WHERE t.IsDelete =0) BEGIN
			 select @PartialOrTotalDevolution = 0
		END
		ELSE BEGIN 
		select @PartialOrTotalDevolution = 1
		END

		--Si se va a confirmar
		if @Status = 2
		begin
			--Se valida que el tercero de la factura de productos exista como cliente
			if not exists(select 1
			from Inventory.DocumentInvoiceProductSalesDevolution dev
			inner join Inventory.DocumentInvoiceProductSales ps on ps.Id = dev.DocumentInvoiceProductSalesId
			inner join Common.Customer c on c.ThirdPartyId = ps.ThirdPartyId
			where dev.Id = @Id)
			begin
				select 999 as CodeMessage, 'El tercero no existe como cliente' as Message, 0 DevolutionId, '' DevolutionCode
				return
			end

			--Se obtiene el id del cliente
			select @CustomerId = c.Id
			from Inventory.DocumentInvoiceProductSalesDevolution dev
			inner join Inventory.DocumentInvoiceProductSales ps on ps.Id = dev.DocumentInvoiceProductSalesId
			inner join Common.Customer c on c.ThirdPartyId = ps.ThirdPartyId
			where dev.Id = @Id

			--Se valida que exista parámetros de facturación para la unidad operativa
			if not exists(select 1
			from Billing.SettingsBilling s
			where s.IdOperatingUnit = @OperatingUnitId)
			begin
				select 999 as CodeMessage, 'No existe parámetros de facturación para la unidad operativa escogida' as Message, 0 DevolutionId, '' DevolutionCode
				return
			end

			--Se valida que exista parámetros de inventarios para la unidad operativa
			if not exists(select 1
			from Inventory.SettingInventory s
			where s.OperatingUnitId = @OperatingUnitId)
			begin
				select 999 as CodeMessage, 'No existe parámetros de inventario para la unidad operativa escogida' as Message, 0 DevolutionId, '' DevolutionCode
				return
			end

			--Se valida si se ha diligenciado el concepto de nota en los parametros de facturacion
			if exists(select 1
			from Billing.SettingsBilling s
			where s.IdOperatingUnit = @OperatingUnitId and s.InvoiceProductDevolutionPartialConceptNoteId is null)
			begin
				select 999 as CodeMessage, 'No se ha diligenciado el campo Concepto Nota Devolución Parcial Factura de Productos del formulario parámetros de facturación' as Message, 0 DevolutionId, '' DevolutionCode
				return
			end		

			--Se obtiene los campos necesarios de los parametros de facturacion
			select @InvoiceProductDevolutionPartialConceptNoteId = InvoiceProductDevolutionPartialConceptNoteId, @AssociateCostCenter = AssociateCostCenter,
			@ReteIVAMainAccountId = ReteIVAMainAccountId, @ReteIVAConceptId = ReteIVAConceptId, @ReteICAMainAccountId = ReteICAMainAccountId, @ReteFuenteMainAccountId = ReteFuenteMainAccountId, @IVAPaymentMainAccountId = IVAPaymentMainAccountId
			from Billing.SettingsBilling 
			where IdOperatingUnit = @OperatingUnitId

			--Se inserta en la tabla temporal la cabecera de la nota
			insert into @TablePortfolioNote(Id, [Code], [NoteDate], [CustomerId], [Observations], [Nature], [NoteType], [OperatingUnitId], [Status])
			values(0, '', @DocumentDate, @CustomerId, @Detail, 2, 1, @OperatingUnitId, 2)

			--Se inserta la factura en la nota
			insert into @TablePortfolioNoteAccountReceivableAdvance(Id, PortfolioNoteId, [AccountReceivableId], [MainAccountId], [AccountReceivableAccountingId], [AdjusmentValue], [PercentageValue], 
			[PreviousBalance], [Balance], ChangeTracker)
			select top 1 0, 0, ar.Id, ara.MainAccountId, ara.Id, dev.TotalValue, 0, 0, 0, 'Added'
			from Inventory.DocumentInvoiceProductSalesDevolution dev
			inner join Inventory.DocumentInvoiceProductSales ps on ps.Id = dev.DocumentInvoiceProductSalesId
			inner join Portfolio.AccountReceivable ar on ar.InvoiceId = ps.InvoiceId
			inner join Portfolio.AccountReceivableAccounting ara on ara.AccountReceivableId = ar.Id
			where dev.Id = @Id
     
			--Inserto la cuenta del ingreso al debito
			insert into @TablePortfolioNoteDetail(Id, PortfolioNoteId, [PortfolioNoteConceptId], [MainAccountId], [ThirdPartyId], [CostCenterId], [Nature], [Value], [Observations], ChangeTracker)
			select 0, 0, @InvoiceProductDevolutionPartialConceptNoteId, ma.Id, ps.ThirdPartyId, 
			IIF(ma.HandlesCostCenter = 1, IIF(@AssociateCostCenter = 1, fu.CostCenterId, pg.CostCenterId), null), 
			1, devDe.SubTotalValue - devDe.DiscountValue, 'Detalle generado desde la devolución parcial de factura de productos - Cuenta del Ingreso', 'Added'
			from @TableDocumentInvoiceProductSalesDevolutionDetail devDe
			inner join Inventory.DocumentInvoiceProductSalesDetailBatchSerial psdbs on psdbs.Id = devDe.DocumentInvoiceProductSalesDetailBatchSerialId
			inner join Inventory.DocumentInvoiceProductSalesDetail psd on psd.Id = psdbs.DocumentInvoiceProductSalesDetailId
			inner join Inventory.DocumentInvoiceProductSales ps on ps.Id = psd.DocumentInvoiceProductSalesId
			inner join Inventory.InventoryProduct p on p.Id = psd.ProductId
			inner join Inventory.ProductGroup pg on pg.Id = p.ProductGroupId
			inner join GeneralLedger.MainAccounts ma on ma.Id = pg.IncomeAccountId
			inner join Payroll.FunctionalUnit fu on fu.Id = ps.FunctionalUnitId
			where devDe.Quantity >0

			--Inserto la cuenta de inventario al debito
			insert into @TablePortfolioNoteDetail(Id, PortfolioNoteId, [PortfolioNoteConceptId], [MainAccountId], [ThirdPartyId], [CostCenterId], [Nature], [Value], [Observations], ChangeTracker)
			select 0, 0, @InvoiceProductDevolutionPartialConceptNoteId, ma.Id, ps.ThirdPartyId, 
			IIF(ma.HandlesCostCenter = 1, IIF(@AssociateCostCenter = 1, fu.CostCenterId, pg.CostCenterId), null), 
			1, ROUND(psd.InventoryProductCost * devDe.Quantity, 0), 'Detalle generado desde la devolución parcial de factura de productos - Cuenta de Inventario', 'Added'
			from @TableDocumentInvoiceProductSalesDevolutionDetail devDe
			inner join Inventory.DocumentInvoiceProductSalesDetailBatchSerial psdbs on psdbs.Id = devDe.DocumentInvoiceProductSalesDetailBatchSerialId
			inner join Inventory.DocumentInvoiceProductSalesDetail psd on psd.Id = psdbs.DocumentInvoiceProductSalesDetailId
			inner join Inventory.DocumentInvoiceProductSales ps on ps.Id = psd.DocumentInvoiceProductSalesId
			inner join Inventory.InventoryProduct p on p.Id = psd.ProductId
			inner join Inventory.ProductGroup pg on pg.Id = p.ProductGroupId
			inner join Payments.AccountPayableConcepts apc on apc.Id = pg.InventoryAccountPayableConceptId
			inner join GeneralLedger.MainAccounts ma on ma.Id = apc.IdAccount
			inner join Payroll.FunctionalUnit fu on fu.Id = ps.FunctionalUnitId
			where devDe.Quantity >0
			--Se valida que este parametrizada la cuenta del costo para el grupo de producto
			if exists(select 1
			from @TableDocumentInvoiceProductSalesDevolutionDetail devDe
			inner join Inventory.DocumentInvoiceProductSalesDetailBatchSerial psdbs on psdbs.Id = devDe.DocumentInvoiceProductSalesDetailBatchSerialId
			inner join Inventory.DocumentInvoiceProductSalesDetail psd on psd.Id = psdbs.DocumentInvoiceProductSalesDetailId
			inner join Inventory.InventoryProduct p on p.Id = psd.ProductId
			inner join Inventory.ProductGroup pg on pg.Id = p.ProductGroupId
			where pg.InventoryCostMainAccountId is null)
			begin
				select @Errors = stuff((select N'; El grupo ' + pg.Code + ' - ' + pg.Name + ' no tiene parametrizada la cuenta del costo'
				from @TableDocumentInvoiceProductSalesDevolutionDetail devDe
				inner join Inventory.DocumentInvoiceProductSalesDetailBatchSerial psdbs on psdbs.Id = devDe.DocumentInvoiceProductSalesDetailBatchSerialId
				inner join Inventory.DocumentInvoiceProductSalesDetail psd on psd.Id = psdbs.DocumentInvoiceProductSalesDetailId
				inner join Inventory.InventoryProduct p on p.Id = psd.ProductId
				inner join Inventory.ProductGroup pg on pg.Id = p.ProductGroupId
				where pg.InventoryCostMainAccountId is null
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				select 999 as CodeMessage, @Errors as Message, 0 DevolutionId, '' DevolutionCode
				return
			end

			--Crea el detalle al credito por el costo
			insert into @TablePortfolioNoteDetail(Id, PortfolioNoteId, [PortfolioNoteConceptId], [MainAccountId], [ThirdPartyId], [CostCenterId], [Nature], [Value], [Observations], ChangeTracker)
			select 0, 0, @InvoiceProductDevolutionPartialConceptNoteId, ma.Id, ps.ThirdPartyId, 
			IIF(ma.HandlesCostCenter = 1, IIF(@AssociateCostCenter = 1, fu.CostCenterId, pg.CostCenterId), null), 
			2, ROUND(psd.InventoryProductCost * devDe.Quantity, 0), 'Detalle generado desde la devolución parcial de factura de productos - Cuenta del Costo', 'Added'
			from @TableDocumentInvoiceProductSalesDevolutionDetail devDe
			inner join Inventory.DocumentInvoiceProductSalesDetailBatchSerial psdbs on psdbs.Id = devDe.DocumentInvoiceProductSalesDetailBatchSerialId
			inner join Inventory.DocumentInvoiceProductSalesDetail psd on psd.Id = psdbs.DocumentInvoiceProductSalesDetailId
			inner join Inventory.DocumentInvoiceProductSales ps on ps.Id = psd.DocumentInvoiceProductSalesId
			inner join Inventory.InventoryProduct p on p.Id = psd.ProductId
			inner join Inventory.ProductGroup pg on pg.Id = p.ProductGroupId
			inner join GeneralLedger.MainAccounts ma on ma.Id = pg.InventoryCostMainAccountId
			inner join Payroll.FunctionalUnit fu on fu.Id = ps.FunctionalUnitId
			where devDe.Quantity>0
			--Se crea detalle si la factura maneja retención iva
			if @WithholdingTax > 0
			begin
				--Se valida el campo retención iva de parámetros de facturación
				if exists(select 1 from Billing.SettingsBilling where IdOperatingUnit = @OperatingUnitId and ReteIVAMainAccountId is null)
				begin
					select 999 as CodeMessage, 'No se ha parametrizado la cuenta contable ReteIVA en parámetros de facturación' as Message, 0 DevolutionId, '' DevolutionCode
					return
				end

				--Se obtiene los campos necesarios de la cuenta contable
				select @HandlesThirdParty = HandlesThirdParty, @HandlesCostCenter = HandlesCostCenter from GeneralLedger.MainAccounts where Id = @ReteIVAMainAccountId

				--Se inserta el detalle para la retención iva
				insert into @TablePortfolioNoteDetail(Id, PortfolioNoteId, [PortfolioNoteConceptId], [MainAccountId], [ThirdPartyId], [CostCenterId], [Nature], [Value], [Observations],
				[Percentage], [RetentionConceptId], [BaseValue], ChangeTracker)
				select 0, 0, @InvoiceProductDevolutionPartialConceptNoteId, @ReteIVAMainAccountId, ps.ThirdPartyId, 
				IIF(@HandlesCostCenter = 1, fu.CostCenterId, null), 
				2, @WithholdingTax, 'Detalle generado desde la devolución parcial de factura de productos - ReteIVA',
				--Retenciones
				rc.Rate, rc.Id, dipsd.ValueTax, 'Added'
				from Inventory.DocumentInvoiceProductSales ps
				inner join Inventory.DocumentInvoiceProductSalesDevolution dipsd ON ps.Id = dipsd.DocumentInvoiceProductSalesId
				inner join Payroll.FunctionalUnit fu on fu.Id = ps.FunctionalUnitId
				inner join GeneralLedger.RetentionConcepts rc on rc.Id = @ReteIVAConceptId
				where ps.Id = @DocumentInvoiceProductSalesId
			end

			--Se crea el detalle si la factura maneja retención ica
			if @WithholdingICA > 0
			begin
				--Se valida el campo retención ica de parámetros de facturación
				if exists(select 1 from Billing.SettingsBilling where IdOperatingUnit = @OperatingUnitId and ReteICAMainAccountId is null)
				begin
					select 999 as CodeMessage, 'No se ha parametrizado la cuenta contable ReteICA en parámetros de facturación' as Message, 0 DevolutionId, '' DevolutionCode
					return
				end

				--Se obtiene los campos necesarios de la cuenta contable
				select @HandlesThirdParty = HandlesThirdParty, @HandlesCostCenter = HandlesCostCenter from GeneralLedger.MainAccounts where Id = @ReteICAMainAccountId

				--Se obtiene el valor base
				set @BaseValue = (select SUM(SubTotalValue - DiscountValue) from @TableDocumentInvoiceProductSalesDevolutionDetail)

				--Se inserta el detalle para la retención ica
				insert into @TablePortfolioNoteDetail(Id, PortfolioNoteId, [PortfolioNoteConceptId], [MainAccountId], [ThirdPartyId], [CostCenterId], [Nature], [Value], [Observations], 
				[Percentage], [RetentionConceptId], [BaseValue], ChangeTracker)
				select 0, 0, @InvoiceProductDevolutionPartialConceptNoteId, @ReteICAMainAccountId, ps.ThirdPartyId, 
				IIF(@HandlesCostCenter = 1, fu.CostCenterId, null), 
				2, @WithholdingICA, 'Detalle generado desde la devolución parcial de factura de productos - ReteICA', ISNULL(rc.Rate, t.IcaPercentage), rc.Id, @BaseValue, 'Added'
				from Inventory.DocumentInvoiceProductSales ps
				inner join Payroll.FunctionalUnit fu on fu.Id = ps.FunctionalUnitId
				inner join Common.ThirdParty t on t.Id = ps.ThirdPartyId
				left join Payroll.BranchOffice bo on bo.Id = ps.BranchOfficeId
				left join Common.City c on c.Id = bo.CityId
				left join GeneralLedger.RetentionConcepts rc on rc.Id = c.ICARetentionConceptId
				where ps.Id = @DocumentInvoiceProductSalesId
			end
			
			--Se crea el detalle si la factura maneja retención en la fuente
			if @RetentionSource > 0
			begin
				--Se valida el campo retención en la fuente de parámetros de facturación
				if exists(select 1 from Billing.SettingsBilling where IdOperatingUnit = @OperatingUnitId and ReteFuenteMainAccountId is null)
				begin
					select 999 as CodeMessage, 'No se ha parametrizado la cuenta contable ReteFuente en parámetros de facturación' as Message, 0 DevolutionId, '' DevolutionCode
					return
				end
				
				--Se obtiene los campos necesarios de la cuenta contable
				select @HandlesThirdParty = HandlesThirdParty, @HandlesCostCenter = HandlesCostCenter from GeneralLedger.MainAccounts where Id = @ReteFuenteMainAccountId

				--Se obtiene el valor base
				set @BaseValue = (select SUM(SubTotalValue - DiscountValue) from @TableDocumentInvoiceProductSalesDevolutionDetail)

				--Se inserta el detalle para la retención en la fuente
				insert into @TablePortfolioNoteDetail(Id, PortfolioNoteId, [PortfolioNoteConceptId], [MainAccountId], [ThirdPartyId], [CostCenterId], [Nature], [Value], [Observations],
				[Percentage], [RetentionConceptId], [BaseValue], ChangeTracker)
				select top 1 0, 0, @InvoiceProductDevolutionPartialConceptNoteId, @ReteFuenteMainAccountId, ps.ThirdPartyId, 
				IIF(@HandlesCostCenter = 1, fu.CostCenterId, null), 
				2, @RetentionSource, 'Detalle generado desde la devolución parcial de factura de productos - ReteFuente', rc.Rate, rc.Id, @BaseValue, 'Added'
				from @TableDocumentInvoiceProductSalesDevolutionDetail temp
				inner join Inventory.DocumentInvoiceProductSalesDetailBatchSerial psdbs on psdbs.Id = temp.DocumentInvoiceProductSalesDetailBatchSerialId
				inner join Inventory.DocumentInvoiceProductSalesDetail psd on psd.Id = psdbs.DocumentInvoiceProductSalesDetailId
				inner join Inventory.DocumentInvoiceProductSales ps on ps.Id = psd.DocumentInvoiceProductSalesId
				inner join Inventory.InventoryProduct p on p.Id = psd.ProductId
				inner join Inventory.ProductGroup pg on pg.Id = p.ProductGroupId
				inner join Payroll.FunctionalUnit fu on fu.Id = ps.FunctionalUnitId
				left join GeneralLedger.RetentionConcepts rc on rc.Id = pg.ReteFuenteConceptId
				where temp.RTFValue > 0 and temp.RTFPercentage > 0 and temp.Quantity>0
			end

			--Se crea el detalle si la factura maneja iva
			if @ValueTax > 0
			begin
				--Se valida el campo cuenta contable iva de parámetros de facturación
				if exists(select 1 from Billing.SettingsBilling where IdOperatingUnit = @OperatingUnitId and IVAPaymentMainAccountId is null)
				begin
					select 999 as CodeMessage, 'No se ha parametrizado la cuenta contable IVA por pagar en parámetros de facturación' as Message, 0 DevolutionId, '' DevolutionCode
					return
				end

				--Se obtiene los campos necesarios de la cuenta contable
				select @HandlesThirdParty = HandlesThirdParty, @HandlesCostCenter = HandlesCostCenter from GeneralLedger.MainAccounts where Id = @IVAPaymentMainAccountId

				--Se inserta el detalle para el iva
				insert into @TablePortfolioNoteDetail(Id, PortfolioNoteId, [PortfolioNoteConceptId], [MainAccountId], [ThirdPartyId], [CostCenterId], [Nature], [Value], [Observations], ChangeTracker)
				select 0, 0, @InvoiceProductDevolutionPartialConceptNoteId, @IVAPaymentMainAccountId, ps.ThirdPartyId, 
				IIF(@HandlesCostCenter = 1, fu.CostCenterId, null), 
				1, @ValueTax, 'Detalle generado desde la devolución parcial de factura de productos - IVA', 'Added'
				from Inventory.DocumentInvoiceProductSales ps
				inner join Payroll.FunctionalUnit fu on fu.Id = ps.FunctionalUnitId
				where ps.Id = @DocumentInvoiceProductSalesId
			end
			
											  
														 

			--Se crea el xml de la nota
			select @PortfolioNoteXML =  convert(xml, (
				select *,
				(select * from @TablePortfolioNoteAccountReceivableAdvance FOR XML PATH('PortfolioNoteAccountReceivableAdvance'), TYPE),
				(select * from @TablePortfolioNoteDetail FOR XML PATH('PortfolioNoteDetail'), TYPE)
				from @TablePortfolioNote
				FOR XML PATH(''), ROOT('PortfolioNote')
			))		
					
			--Se ejecuta el proceso de nota
			exec [Portfolio].[SP_SavePortfolioNote_Output] @PortfolioNoteXML, @UserCode, @CompanyType, @CodeResultNote output, @MessageResultNote output, @NoteId output, @NoteCode output

			--Se valida si el proceso de nota fallo
			if @CodeResultNote = 999
			begin
				select 999 as CodeMessage, @MessageResultNote as Message, 0 DevolutionId, '' DevolutionCode
				return
			end

			--Se valida que las cantidades a disminuir no sean menores a la cantidad pendiente
			if exists(select 1
			from Inventory.DocumentInvoiceProductSalesDevolutionDetail devDe
			inner join Inventory.DocumentInvoiceProductSalesDetailBatchSerial psdbs on psdbs.Id = devDe.DocumentInvoiceProductSalesDetailBatchSerialId
			where devDe.DocumentInvoiceProductSalesDevolutionId = @Id and devDe.Quantity > psdbs.OutstandingQuantity)
			begin
				select @Errors = stuff((select N'; La cantidad a devolver(' + CAST(devDe.Quantity as varchar(20)) + ') es mayor a la cantidad pendiente(' + CAST(psdbs.OutstandingQuantity as varchar(20)) + ') del producto ' + pro.Code + ' - ' + pro.Name
				from Inventory.DocumentInvoiceProductSalesDevolutionDetail devDe
				inner join Inventory.DocumentInvoiceProductSalesDetailBatchSerial psdbs on psdbs.Id = devDe.DocumentInvoiceProductSalesDetailBatchSerialId
				inner join Inventory.DocumentInvoiceProductSalesDetail psd on psd.Id = psdbs.DocumentInvoiceProductSalesDetailId
				inner join Inventory.InventoryProduct pro on pro.Id = psd.ProductId
				where devDe.DocumentInvoiceProductSalesDevolutionId = @Id and devDe.Quantity > psdbs.OutstandingQuantity
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				select 999 as CodeMessage, @Errors as Message, 0 DevolutionId, '' DevolutionCode
				return
			end

			--Se disminuye las cantidades
			update psdbs set psdbs.OutstandingQuantity -= devDe.Quantity
			from Inventory.DocumentInvoiceProductSalesDevolutionDetail devDe
			inner join Inventory.DocumentInvoiceProductSalesDetailBatchSerial psdbs on psdbs.Id = devDe.DocumentInvoiceProductSalesDetailBatchSerialId
			where devDe.DocumentInvoiceProductSalesDevolutionId = @Id

			--SE VALIDA si la factura tiene Remissiones de salida importadas, para hacer los movimiento devuelta de las cantidades pendientes
			if EXISTS(	SELECT top 1 1
						FROM @TableDocumentInvoiceProductSalesDevolutionDetail t
						INNER JOIN Inventory.DocumentInvoiceProductSalesDetailBatchSerial dipsdb WITH (NOLOCK) ON t.DocumentInvoiceProductSalesDetailBatchSerialId= dipsdb.Id
						INNER JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd WITH(NOLOCK) ON dipsdb.DocumentInvoiceProductSalesDetailId = dipsd.Id
						where dipsd.ImportSource IS NOT NULL AND dipsd.ImportSource = 1 ) BEGIN
						
						if EXISTS(SELECT 1 FROM Inventory.DocumentInvoiceProductSalesDevolutionDetail devDe
											JOIN Inventory.DocumentInvoiceProductSalesDetailBatchSerial dipsdb on devDe.DocumentInvoiceProductSalesDetailBatchSerialId = dipsdb.Id
											JOIN Inventory.RemissionOutputDetailPhysical rodp on dipsdb.RemissionOutputPhysicalId = rodp.Id
											WHERE devDe.DocumentInvoiceProductSalesDevolutionId = @Id AND (rodp.Quantity < devDe.Quantity or ((rodp.Quantity -rodp.OutstandingQuantity) < devDe.Quantity)) ) BEGIN

											SELECT @Errors = stuff((select concat( N'; La cantidad a devolver(', CAST(devDe.Quantity as varchar(20)), ') es mayor a la cantidad masima de la remision (', CAST(rodp.OutstandingQuantity as varchar(20)), ') del producto ' , pro.Code , ' - ' , pro.Name)
											FROM Inventory.DocumentInvoiceProductSalesDevolutionDetail devDe
											JOIN Inventory.DocumentInvoiceProductSalesDetailBatchSerial dipsdb on devDe.DocumentInvoiceProductSalesDetailBatchSerialId = dipsdb.Id
											JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd on dipsd.DocumentInvoiceProductSalesId = dipsd.Id
											JOIN Inventory.InventoryProduct pro on dipsd.ProductId = pro.id
											JOIN Inventory.RemissionOutputDetailPhysical rodp on dipsdb.RemissionOutputPhysicalId = rodp.Id
											WHERE rodp.Quantity < devDe.Quantity or ((rodp.Quantity -rodp.OutstandingQuantity) < devDe.Quantity) and devDe.DocumentInvoiceProductSalesDevolutionId = @Id
											for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

											select 999 as CodeMessage, @Errors as Message, 0 DevolutionId, '' DevolutionCode
											return
											END
						UPDATE rodp 
						SET rodp.OutstandingQuantity += devDe.Quantity
						FROM Inventory.DocumentInvoiceProductSalesDevolutionDetail devDe
						inner join Inventory.DocumentInvoiceProductSalesDetailBatchSerial dipsdb on devDe.DocumentInvoiceProductSalesDetailBatchSerialId= dipsdb.Id
						inner join Inventory.DocumentInvoiceProductSalesDetail dipsd on dipsdb.DocumentInvoiceProductSalesDetailId = dipsd.Id
						inner join Inventory.RemissionOutputDetailPhysical rodp on dipsdb.RemissionOutputPhysicalId = rodp.Id
						where devDe.DocumentInvoiceProductSalesDevolutionId = @Id	

			END

			--Se crea el xml del kardex
			select @KardexXML =  convert(xml, (
				select ps.ThirdPartyId, psd.ProductId, 1 MovementType, @WarehouseId WarehouseId, devDev.Quantity, k.[Value] Value, 1 AffectAverageCost
				from @TableDocumentInvoiceProductSalesDevolutionDetail devDev
				inner join Inventory.DocumentInvoiceProductSalesDetailBatchSerial psdbs on psdbs.Id = devDev.DocumentInvoiceProductSalesDetailBatchSerialId
				inner join Inventory.DocumentInvoiceProductSalesDetail psd on psd.Id = psdbs.DocumentInvoiceProductSalesDetailId
				inner join Inventory.DocumentInvoiceProductSales ps on ps.Id = psd.DocumentInvoiceProductSalesId
				left outer join Inventory.Kardex k on k.EntityId = ps.Id and k.EntityName = 'DocumentInvoiceProductSales'
				and k.ProductId = psd.ProductId
				where devDev.Quantity > 0
				FOR XML PATH('Kardex')
			))		

			--Se ejecuta el proceso del kardex
			exec [Inventory].[SP_SavePhysicalInventoryKardex_Output] @KardexXML, @Id,@Code, 'DocumentInvoiceProductSalesDevolution', @UserCode, 0, @CodeResultKardex output, @MessageResultKardex output

			--Se valida si el proceso de kardex fallo
			if @CodeResultKardex = 999
			begin
				select 999 as CodeMessage, @MessageResultKardex as Message, 0 DevolutionId, '' DevolutionCode
				return
			end
		end

		--Se asigna el mensaje a retornar
		set @MessageReturn = case @Status 
								when 2 then 'Se guardó y se confirmó la devolución con código ' + @Code 
								when 3 then 'Se anuló la devolución con código ' + @Code 
								else 'Se guardó la devolución con código ' + @Code 
							 end + CHAR(13) + CHAR(10) + @MessageResultNote

		--Se retorna el ok
		select 0 as CodeMessage, @MessageReturn as Message, @Id DevolutionId, @Code DevolutionCode
		return

	End try
	Begin Catch

		--Se retorna el error
		select 999 as CodeMessage,CONCAT( ERROR_MESSAGE(),' - ', ERROR_LINE())  as Message, 0 DevolutionId, '' DevolutionCode
		return

	End Catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra y gestiona devoluciones parciales de ventas de productos en inventario (notas crédito), a partir de un XML con la cabecera y el detalle de los ítems devueltos. Crea o actualiza el documento de devolución en Inventory.DocumentInvoiceProductSalesDevolution y su detalle de líneas en DocumentInvoiceProductSalesDevolutionDetail, asignando automáticamente el consecutivo del código del documento mediante las tablas BillingSequence, BillingSequenceDetail y Common.Sequense. Además, genera la nota de cartera contable correspondiente (nota crédito al cliente), calcula retenciones (IVA, ICA, fuente) y actualiza el kardex de inventario para reflejar el reingreso de mercancía devuelta; también permite anular una devolución existente cambiando su estado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDocumentInvoiceProductSalesDevolution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDocumentInvoiceProductSalesDevolution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (crear/actualizar/anular) una devolución parcial o total de factura de venta de productos, generando la nota de cartera contable asociada con sus retenciones e IVA, ajustando cantidades pendientes de lotes/seriales y remisiones, y registrando el movimiento de kardex.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDocumentInvoiceProductSalesDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener el nodo /DocumentInvoiceProductSalesDevolution con la cabecera y opcionalmente sus DocumentInvoiceProductSalesDevolutionDetail.; Para confirmar (Status=2): el ThirdPartyId de la factura de productos asociada debe existir como Common.Customer.; Para confirmar: debe existir Billing.SettingsBilling para la unidad operativa indicada.; Para confirmar: debe existir Inventory.SettingInventory para la unidad operativa indicada.; Para confirmar: SettingsBilling.InvoiceProductDevolutionPartialConceptNoteId no puede ser nulo.; Si @WithholdingTax > 0: SettingsBilling.ReteIVAMainAccountId no puede ser nulo.; Si @WithholdingICA > 0: SettingsBilling.ReteICAMainAccountId no puede ser nulo.; Si @RetentionSource > 0: SettingsBilling.ReteFuenteMainAccountId no puede ser nulo.; Si @ValueTax > 0: SettingsBilling.IVAPaymentMainAccountId no puede ser nulo.; Todos los ProductGroup involucrados deben tener parametrizado InventoryCostMainAccountId.; Las cantidades a devolver por línea no pueden superar OutstandingQuantity del DocumentInvoiceProductSalesDetailBatchSerial correspondiente.; Si las líneas provienen de remisiones importadas (ImportSource=1), la cantidad a devolver no puede superar la cantidad ni la (Quantity-OutstandingQuantity) de RemissionOutputDetailPhysical.; Si no se envía Code, debe existir una secuencia configurada (BillingSequence/BillingSequenceDetail) para IdForm=''2100'' (y para la unidad operativa si Scope <> ''O'').', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDocumentInvoiceProductSalesDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución parcial de factura de venta de productos; Nota de cartera (nota crédito); Cuenta por cobrar; Retención IVA (ReteIVA); Retención ICA (ReteICA); Retención en la fuente (ReteFuente); IVA por pagar; Cuenta del ingreso; Cuenta de inventario; Cuenta del costo; Kardex / movimiento de inventario; Remisiones de salida importadas; Cliente / Tercero; Unidad operativa; Centro de costo; Lote/Serial de producto; Secuencia de numeración (alcance Organización vs Unidad Operativa)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDocumentInvoiceProductSalesDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.SP_SavePortfolioNote_Output; Inventory.SP_SavePhysicalInventoryKardex_Output; Common.GETDATE; dbo.GetSequence', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDocumentInvoiceProductSalesDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.BillingSequence; Billing.BillingSequenceDetail; Common.Sequense; Inventory.DocumentInvoiceProductSalesDevolution; Inventory.DocumentInvoiceProductSales; Common.Customer; Billing.SettingsBilling; Inventory.SettingInventory; Portfolio.AccountReceivable; Portfolio.AccountReceivableAccounting; Inventory.DocumentInvoiceProductSalesDetailBatchSerial; Inventory.DocumentInvoiceProductSalesDetail; Inventory.InventoryProduct; Inventory.ProductGroup; GeneralLedger.MainAccounts; Payroll.FunctionalUnit; Payments.AccountPayableConcepts; GeneralLedger.RetentionConcepts; Common.ThirdParty; Payroll.BranchOffice; Common.City; Inventory.DocumentInvoiceProductSalesDevolutionDetail; Inventory.RemissionOutputDetailPhysical; Inventory.Kardex', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDocumentInvoiceProductSalesDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDocumentInvoiceProductSalesDevolution';
-- GO
