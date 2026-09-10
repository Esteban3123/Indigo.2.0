

-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 24/05/2016
-- Description:	Procedimiento que se encarga de confirmar la salida del activo
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_ConfirmFixedAssetActiveOutput] 
    @FixedAssetActiveOutputXml as Xml,
	@CodeUser as varchar(20)
AS
BEGIN
	
	--Se declaran las variables para obtener la cabecera
	declare @Id int, @OperatingUnitId int, @Code varchar(20), @DocumentDate date, @Observation varchar(500), @Status tinyint
	
	--Tabla temporal de FixedAssetActiveOutputDetail
	declare @FixedAssetActiveOutputDetail table(Id int, FixedAssetActiveOutputId int, ActiveType tinyint, PhysicalAssetId int, PhysicalAssetPartsId int,
	MainAccountId int, OutputType tinyint, LowType tinyint, SalesValue numeric(20,4), ThirdPartyId int, AccountReceivableId int)
	
	Begin try

		--Se obtiene FixedAssetActiveOutput
		select 
		@Id = t.x.value('Id[1]','int'),
		@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
		@Code = t.x.value('Code[1]','varchar(20)'),
		@DocumentDate = t.x.value('DocumentDate[1]','date'),
		@Observation = t.x.value('Observation[1]','varchar(500)'),
		@Status = t.x.value('Status[1]','tinyint')
		from @FixedAssetActiveOutputXml.nodes('/FixedAssetActiveOutput') t(x)
		
		--Se obtiene FixedAssetActiveOutputDetail
		insert into @FixedAssetActiveOutputDetail
		select 
		t.x.value('Id[1]','int') as Id,
		t.x.value('FixedAssetActiveOutputId[1]','int') as FixedAssetActiveOutputId,
		t.x.value('ActiveType[1]','tinyint') as ActiveType,
		case when t.x.value('PhysicalAssetId[1]','int') = 0 then null else t.x.value('PhysicalAssetId[1]','int') end as PhysicalAssetId,
		case when t.x.value('PhysicalAssetPartsId[1]','int') = 0 then null else t.x.value('PhysicalAssetPartsId[1]','int') end as PhysicalAssetPartsId,
		t.x.value('MainAccountId[1]','int') as MainAccountId,
		t.x.value('OutputType[1]','tinyint') as OutputType,
		t.x.value('LowType[1]','tinyint') as LowType,
		t.x.value('SalesValue[1]','numeric(20,4)') as SalesValue,
		case when t.x.value('ThirdPartyId[1]','int') = 0 then null else t.x.value('ThirdPartyId[1]','int') end as ThirdPartyId,
		case when t.x.value('AccountReceivableId[1]','int') = 0 then null else t.x.value('AccountReceivableId[1]','int') end as AccountReceivableId
		from @FixedAssetActiveOutputXml.nodes('/FixedAssetActiveOutput/FixedAssetActiveOutputDetail') t(x)

		--Se valida que exista parámetros de activos fijos
		if (select count(*) from FixedAsset.SettingFixedAsset where OperatingUnitId = @OperatingUnitId) = 0
		Begin
			select 999 as CodeMessage, 'No existe parámetros de activo fijo para la unidad operativa seleccionada' as Message, '' as Code, 0 as Id, '' as ResultConsecutives, '' as JournalVoucherType
			return
		End

		declare @MonthSettings int
		declare @YearSettings int
		select @MonthSettings = MONTH(ProcessDate), @YearSettings = YEAR(ProcessDate) 
		from FixedAsset.SettingFixedAsset where OperatingUnitId = @OperatingUnitId
		--Se valida que la fecha de salida este en el mismo mes y año que la fecha de parámetros
		if @MonthSettings <> MONTH(@DocumentDate) or @YearSettings <> YEAR(@DocumentDate)
		Begin
			select 999 as CodeMessage, 'El mes y el año de la fecha de documento no es igual a la fecha de proceso de parámetros de activo fijo' as Message, '' as Code, 0 as Id, '' as ResultConsecutives, '' as JournalVoucherType
			return
		End

		--Valido que el mes este abierto
		if(select count(*) from [GeneralLedger].[ClosedMonth] where [Year] = Year(@DocumentDate) and [Month] = Month(@DocumentDate) and Status = 1) = 0 --- Si el mes no esta abierto
		Begin
			select 999 as CodeMessage, 'El mes ' + cast(MONTH(@DocumentDate) as varchar(2)) + ' no se encuentra abierto' as Message, '' as Code, 0 as Id, '' as ResultConsecutives, '' as JournalVoucherType
			return
		End

		--Se declara una tabla con los datos para la cabecera del comprobante contable 
		declare @JournalVourcherTmp table (Consecutive bigint,LegalBookId integer,IdJournalVoucher integer,VoucherDate varchar(30),
		Imported varchar(5),[Status] tinyint,Detail varchar(500),EntityCode  varchar(20),EntityId integer,EntityName varchar(250),IsClosedYear varchar(5))

		--Se declara una tabla temporal para los detalles del comprobante
		declare @JournalVourcherDetailTmp table (Id integer,IdAccounting integer,IdMainAccount integer,IdThirdParty integer,IdCostCenter integer,
		DebitValue decimal(18,2),CreditValue decimal(18,2),Detail varchar(500),IdRetention integer,RetentionRate decimal(5,2),BaseValue decimal(18,0),
		BillingValue decimal(18,0), LegalBookId integer)  

		--Se ingresan las cabeceras de los comprobantes contables para cada uno de los libros
		INSERT INTO @JournalVourcherTmp
			(Consecutive, LegalBookId, IdJournalVoucher, VoucherDate, Imported, Status, Detail, EntityName)
		select x.Consecutive, x.LegalBookId, x.IdJournalVoucher, [Common].[GETDATE](), x.Imported, x.Status, x.Detail, x.EntityName
		 from (
		select 0 as Consecutive, fapadb.LegalBookId aS LegalBookId, sfa.IdOutputAccountingVoucher as IdJournalVoucher, 
		0 as Imported, 2 as Status, 'Comprobante contable generado desde salida de activos' as Detail, 'FixedAssetActiveOutput' as EntityName
		from @FixedAssetActiveOutputDetail faaod
		inner join FixedAsset.FixedAssetActiveOutput faao on faao.Id = faaod.FixedAssetActiveOutputId
		inner join FixedAsset.SettingFixedAsset sfa on sfa .OperatingUnitId = faao.OperatingUnitId
		inner join fixedasset.FixedAssetPhysicalAssetDetailBook fapadb on fapadb.PhysicalAssetId = faaod.PhysicalAssetId) x 
		group by x.LegalBookId, x.Consecutive, x.IdJournalVoucher, x.Imported, x.Status, x.Detail,x.EntityName
		
		--Siempre creo un detalle de comprobante a la cuenta de ingreso del catalogo y el valor es el valor historico y se va al credito
		INSERT INTO @JournalVourcherDetailTmp
			(IdAccounting, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, 
			BillingValue, LegalBookId)
		select x.IdAccounting, x.IdMainAccount, x.IdThirdParty, x.IdCostCenter, x.DebitValue, SUM(x.CreditValue), x.Detail, x.IdRetention, x.RetentionRate,
		x.BaseValue, x.BillingValue, x.LegalBookId
		 from (
		select 0 as IdAccounting, faic.IncomeAccountId as IdMainAccount, far.ThirdPartyId as IdThirdParty, fu.CostCenterId as IdCostCenter, 0 as DebitValue, 
		fapa.HistoricalValue as CreditValue, 'Detalle generado desde salida de activos' as Detail, NULL as IdRetention, 0 as RetentionRate, 0 as BaseValue, 
		0 as BillingValue, fapadb.LegalBookId as LegalBookId
		from @FixedAssetActiveOutputDetail faaod
		inner join FixedAsset.FixedAssetActiveOutput faao on faao.Id = faaod.FixedAssetActiveOutputId
		inner join FixedAsset.SettingFixedAsset sfa on sfa .OperatingUnitId = faao.OperatingUnitId
		inner join FixedAsset.FixedAssetPhysicalAsset fapa on fapa.Id = faaod.PhysicalAssetId
		inner join FixedAsset.FixedAssetResponsible far on far.Id = fapa.ResponsibleId
		inner join FixedAsset.FixedAssetItem fai on fai.Id = fapa.ItemId
		inner join FixedAsset.FixedAssetItemCatalog faic on faic.Id = fai.ItemCatalogId
		inner join fixedasset.FixedAssetPhysicalAssetDetailBook fapadb on fapadb.PhysicalAssetId = fapa.Id
		inner join FixedAsset.FixedAssetLocation fal on fal.Id = fapa.LocationId
		inner join Payroll.FunctionalUnit fu on fu.Id = fal.FunctionalUnitId) x 
		group by x.IdAccounting, x.IdMainAccount, x.IdThirdParty, x.IdCostCenter, x.DebitValue, x.Detail, x.IdRetention, x.RetentionRate, x.BaseValue,
		x.BillingValue, x.LegalBookId
		
		--Si ResidualValue esta en cero es porque se deprecio totalmente y creo un detalle de comrpobante con la cuenta del catalogo cuenta de depreciacion
		--y el valor es el campo DepreciatedValue y se lleva al debito
		--ó
		--Si ResidualValue no esta en cero es porque no se ha depreciado totalmente y creo un detalle de comrpobante con la cuenta del catalogo 
		--cuenta de depreciacion y el valor es el campo DepreciatedValue y se lleva al debito
		INSERT INTO @JournalVourcherDetailTmp
			(IdAccounting, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, 
			BillingValue, LegalBookId)
		select x.IdAccounting, x.IdMainAccount, x.IdThirdParty, x.IdCostCenter, SUM(x.DebitValue), x.CreditValue, x.Detail, x.IdRetention, x.RetentionRate,
		x.BaseValue, x.BillingValue, x.LegalBookId
		 from (
		select 0 as IdAccounting, faic.DepreciationAccountId as IdMainAccount, far.ThirdPartyId as IdThirdParty, fu.CostCenterId as IdCostCenter, 
		fapadb.DepreciatedValue as DebitValue, 0 as CreditValue, 'Detalle generado desde salida de activos' as Detail, NULL as IdRetention, 
		0 as RetentionRate, 0 as BaseValue, 0 as BillingValue, fapadb.LegalBookId as LegalBookId
		from @FixedAssetActiveOutputDetail faaod
		inner join FixedAsset.FixedAssetActiveOutput faao on faao.Id = faaod.FixedAssetActiveOutputId
		inner join FixedAsset.SettingFixedAsset sfa on sfa .OperatingUnitId = faao.OperatingUnitId
		inner join FixedAsset.FixedAssetPhysicalAsset fapa on fapa.Id = faaod.PhysicalAssetId
		inner join FixedAsset.FixedAssetResponsible far on far.Id = fapa.ResponsibleId
		inner join FixedAsset.FixedAssetItem fai on fai.Id = fapa.ItemId
		inner join FixedAsset.FixedAssetItemCatalog faic on faic.Id = fai.ItemCatalogId
		inner join fixedasset.FixedAssetPhysicalAssetDetailBook fapadb on fapadb.PhysicalAssetId = fapa.Id
		inner join FixedAsset.FixedAssetLocation fal on fal.Id = fapa.LocationId
		inner join Payroll.FunctionalUnit fu on fu.Id = fal.FunctionalUnitId
		where (fapadb.ResidualValue = 0 or fapadb.ResidualValue > 0) and fapadb.DepreciatedValue > 0) x 
		group by x.IdAccounting, x.IdMainAccount, x.IdThirdParty, x.IdCostCenter, x.CreditValue, x.Detail, x.IdRetention, x.RetentionRate, x.BaseValue,
		x.BillingValue, x.LegalBookId

		--Además creo otro detalle de comprobante por el valor del campo ResidualValue y lo llevo a la cuenta perdida del ejercicio del catalogo y se lleva
		--al debito
		INSERT INTO @JournalVourcherDetailTmp
			(IdAccounting, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, 
			BillingValue, LegalBookId)
		select x.IdAccounting, x.IdMainAccount, x.IdThirdParty, x.IdCostCenter, SUM(x.DebitValue), x.CreditValue, x.Detail, x.IdRetention, x.RetentionRate,
		x.BaseValue, x.BillingValue, x.LegalBookId
		 from (
		select 0 as IdAccounting, faic.LossMainAccountId as IdMainAccount, far.ThirdPartyId as IdThirdParty, fu.CostCenterId as IdCostCenter, 
		fapadb.ResidualValue as DebitValue, 0 as CreditValue, 'Detalle generado desde salida de activos' as Detail, NULL as IdRetention, 
		0 as RetentionRate, 0 as BaseValue, 0 as BillingValue, fapadb.LegalBookId as LegalBookId
		from @FixedAssetActiveOutputDetail faaod
		inner join FixedAsset.FixedAssetActiveOutput faao on faao.Id = faaod.FixedAssetActiveOutputId
		inner join FixedAsset.SettingFixedAsset sfa on sfa .OperatingUnitId = faao.OperatingUnitId
		inner join FixedAsset.FixedAssetPhysicalAsset fapa on fapa.Id = faaod.PhysicalAssetId
		inner join FixedAsset.FixedAssetResponsible far on far.Id = fapa.ResponsibleId
		inner join FixedAsset.FixedAssetItem fai on fai.Id = fapa.ItemId
		inner join FixedAsset.FixedAssetItemCatalog faic on faic.Id = fai.ItemCatalogId
		inner join fixedasset.FixedAssetPhysicalAssetDetailBook fapadb on fapadb.PhysicalAssetId = fapa.Id
		inner join FixedAsset.FixedAssetLocation fal on fal.Id = fapa.LocationId
		inner join Payroll.FunctionalUnit fu on fu.Id = fal.FunctionalUnitId
		where fapadb.ResidualValue > 0) x 
		group by x.IdAccounting, x.IdMainAccount, x.IdThirdParty, x.IdCostCenter, x.CreditValue, x.Detail, x.IdRetention, x.RetentionRate, x.BaseValue,
		x.BillingValue, x.LegalBookId

		--Si el detalle de la salida es venta se adicionan dos cuentas con el valor de venta

		--Al credito la cuenta ganacia del ejercicio del catalogo y el valor es el valor de venta
		INSERT INTO @JournalVourcherDetailTmp
			(IdAccounting, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, 
			BillingValue, LegalBookId)
		select x.IdAccounting, x.IdMainAccount, x.IdThirdParty, x.IdCostCenter, x.DebitValue, SUM(x.CreditValue), x.Detail, x.IdRetention, x.RetentionRate,
		x.BaseValue, x.BillingValue, x.LegalBookId
		 from (
		select 0 as IdAccounting, faic.NetIncomeAccountId as IdMainAccount, faaod.ThirdPartyId as IdThirdParty, fu.CostCenterId as IdCostCenter, 0 as DebitValue, 
		faaod.SalesValue as CreditValue, 'Detalle generado desde salida de activos' as Detail, NULL as IdRetention, 0 as RetentionRate, 0 as BaseValue, 
		0 as BillingValue, fapadb.LegalBookId as LegalBookId
		from @FixedAssetActiveOutputDetail faaod
		inner join FixedAsset.FixedAssetActiveOutput faao on faao.Id = faaod.FixedAssetActiveOutputId
		inner join FixedAsset.SettingFixedAsset sfa on sfa .OperatingUnitId = faao.OperatingUnitId
		inner join FixedAsset.FixedAssetPhysicalAsset fapa on fapa.Id = faaod.PhysicalAssetId
		inner join FixedAsset.FixedAssetItem fai on fai.Id = fapa.ItemId
		inner join FixedAsset.FixedAssetItemCatalog faic on faic.Id = fai.ItemCatalogId
		inner join fixedasset.FixedAssetPhysicalAssetDetailBook fapadb on fapadb.PhysicalAssetId = fapa.Id
		inner join FixedAsset.FixedAssetLocation fal on fal.Id = fapa.LocationId
		inner join Payroll.FunctionalUnit fu on fu.Id = fal.FunctionalUnitId
		where faaod.OutputType = 2) x 
		group by x.IdAccounting, x.IdMainAccount, x.IdThirdParty, x.IdCostCenter, x.DebitValue, x.Detail, x.IdRetention, x.RetentionRate, x.BaseValue,
		x.BillingValue, x.LegalBookId

		--Al debito la cuenta CxC y ventas de parametros y el valor es el valor de venta
		INSERT INTO @JournalVourcherDetailTmp
			(IdAccounting, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, 
			BillingValue, LegalBookId)
		select x.IdAccounting, x.IdMainAccount, x.IdThirdParty, x.IdCostCenter, SUM(x.DebitValue), x.CreditValue, x.Detail, x.IdRetention, x.RetentionRate,
		x.BaseValue, x.BillingValue, x.LegalBookId
		 from (
		select 0 as IdAccounting, sfa.SalesMainAccountId as IdMainAccount, faaod.ThirdPartyId as IdThirdParty, fu.CostCenterId as IdCostCenter, 
		faaod.SalesValue as DebitValue, 0 as CreditValue, 'Detalle generado desde salida de activos' as Detail, NULL as IdRetention, 
		0 as RetentionRate, 0 as BaseValue, 0 as BillingValue, fapadb.LegalBookId as LegalBookId
		from @FixedAssetActiveOutputDetail faaod
		inner join FixedAsset.FixedAssetActiveOutput faao on faao.Id = faaod.FixedAssetActiveOutputId
		inner join FixedAsset.SettingFixedAsset sfa on sfa .OperatingUnitId = faao.OperatingUnitId
		inner join FixedAsset.FixedAssetPhysicalAsset fapa on fapa.Id = faaod.PhysicalAssetId
		inner join FixedAsset.FixedAssetItem fai on fai.Id = fapa.ItemId
		inner join FixedAsset.FixedAssetItemCatalog faic on faic.Id = fai.ItemCatalogId
		inner join fixedasset.FixedAssetPhysicalAssetDetailBook fapadb on fapadb.PhysicalAssetId = fapa.Id
		inner join FixedAsset.FixedAssetLocation fal on fal.Id = fapa.LocationId
		inner join Payroll.FunctionalUnit fu on fu.Id = fal.FunctionalUnitId
		where faaod.OutputType = 2) x 
		group by x.IdAccounting, x.IdMainAccount, x.IdThirdParty, x.IdCostCenter, x.CreditValue, x.Detail, x.IdRetention, x.RetentionRate, x.BaseValue,
		x.BillingValue, x.LegalBookId
		
		--Si el detalle es de reposicion se adicionan dos cuentas con el valor de la reposicion

		--Al credito la cuenta credito de reposicion del catalogo y el valor es el valor de reposicion
		INSERT INTO @JournalVourcherDetailTmp
			(IdAccounting, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, 
			BillingValue, LegalBookId)
		select x.IdAccounting, x.IdMainAccount, x.IdThirdParty, x.IdCostCenter, x.DebitValue, SUM(x.CreditValue), x.Detail, x.IdRetention, x.RetentionRate,
		x.BaseValue, x.BillingValue, x.LegalBookId
		 from (
		select 0 as IdAccounting, faic.ReplacementCreditMainAccountId as IdMainAccount, faaod.ThirdPartyId as IdThirdParty, fu.CostCenterId as IdCostCenter, 
		0 as DebitValue, faaod.SalesValue as CreditValue, 'Detalle generado desde salida de activos' as Detail, NULL as IdRetention, 
		0 as RetentionRate, 0 as BaseValue, 0 as BillingValue, fapadb.LegalBookId as LegalBookId
		from @FixedAssetActiveOutputDetail faaod
		inner join FixedAsset.FixedAssetActiveOutput faao on faao.Id = faaod.FixedAssetActiveOutputId
		inner join FixedAsset.SettingFixedAsset sfa on sfa .OperatingUnitId = faao.OperatingUnitId
		inner join FixedAsset.FixedAssetPhysicalAsset fapa on fapa.Id = faaod.PhysicalAssetId
		inner join FixedAsset.FixedAssetItem fai on fai.Id = fapa.ItemId
		inner join FixedAsset.FixedAssetItemCatalog faic on faic.Id = fai.ItemCatalogId
		inner join fixedasset.FixedAssetPhysicalAssetDetailBook fapadb on fapadb.PhysicalAssetId = fapa.Id
		inner join FixedAsset.FixedAssetLocation fal on fal.Id = fapa.LocationId
		inner join Payroll.FunctionalUnit fu on fu.Id = fal.FunctionalUnitId
		where faaod.OutputType = 1 and faaod.LowType = 3) x 
		group by x.IdAccounting, x.IdMainAccount, x.IdThirdParty, x.IdCostCenter, x.DebitValue, x.Detail, x.IdRetention, x.RetentionRate, x.BaseValue,
		x.BillingValue, x.LegalBookId

		--Al debito la cuenta reposicion no responsabilidades de parametros y el valor es el valor de reposicion
		INSERT INTO @JournalVourcherDetailTmp
			(IdAccounting, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, 
			BillingValue, LegalBookId)
		select x.IdAccounting, x.IdMainAccount, x.IdThirdParty, x.IdCostCenter, SUM(x.DebitValue), x.CreditValue, x.Detail, x.IdRetention, x.RetentionRate,
		x.BaseValue, x.BillingValue, x.LegalBookId
		 from (
		select 0 as IdAccounting, sfa.ReplacementMainAccountId as IdMainAccount, faaod.ThirdPartyId as IdThirdParty, fu.CostCenterId as IdCostCenter, 
		faaod.SalesValue as DebitValue, 0 as CreditValue, 'Detalle generado desde salida de activos' as Detail, NULL as IdRetention, 
		0 as RetentionRate, 0 as BaseValue, 0 as BillingValue, fapadb.LegalBookId as LegalBookId
		from @FixedAssetActiveOutputDetail faaod
		inner join FixedAsset.FixedAssetActiveOutput faao on faao.Id = faaod.FixedAssetActiveOutputId
		inner join FixedAsset.SettingFixedAsset sfa on sfa .OperatingUnitId = faao.OperatingUnitId
		inner join FixedAsset.FixedAssetPhysicalAsset fapa on fapa.Id = faaod.PhysicalAssetId
		inner join FixedAsset.FixedAssetItem fai on fai.Id = fapa.ItemId
		inner join FixedAsset.FixedAssetItemCatalog faic on faic.Id = fai.ItemCatalogId
		inner join fixedasset.FixedAssetPhysicalAssetDetailBook fapadb on fapadb.PhysicalAssetId = fapa.Id
		inner join FixedAsset.FixedAssetLocation fal on fal.Id = fapa.LocationId
		inner join Payroll.FunctionalUnit fu on fu.Id = fal.FunctionalUnitId
		where faaod.OutputType = 1 and faaod.LowType = 3) x 
		group by x.IdAccounting, x.IdMainAccount, x.IdThirdParty, x.IdCostCenter, x.CreditValue, x.Detail, x.IdRetention, x.RetentionRate, x.BaseValue,
		x.BillingValue, x.LegalBookId

		--Se guarda los comprobantes generados
		Declare @TempLegalBookId int
		Declare InfoItem Cursor For Select LegalBookId From @JournalVourcherTmp
		
		declare @ResultConsecutives varchar(max) = ''
		
		Open InfoItem
		Fetch Next From InfoItem Into @TempLegalBookId
		While @@fetch_status = 0
		Begin
			
			--genero el XML para guardar el comprobante 
			declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)
			declare @JournalVoucherXML as XML
			select @JournalVoucherXML =  convert(xml, (select * 
			from @JournalVourcherTmp JournalVoucher 
			inner join @JournalVourcherDetailTmp JournalVoucherDetail on JournalVoucher.LegalBookId = JournalVoucherDetail.LegalBookId
			where JournalVoucher.LegalBookId = @TempLegalBookId For xml AUTO,TYPE, ELEMENTS))
		
			insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,@CodeUser 
		
			if (select code  from @resultJournalVoucher) = '999' 
			Begin			
				Close InfoItem
				Deallocate InfoItem
				declare @errorJV varchar(max)
				select @errorJV = MessageResult  from @resultJournalVoucher 
				select 999 as CodeMessage, @errorJV as Message, '' as Code, 0 as Id, '' as ResultConsecutives, '' as JournalVoucherType
				return
			End  

			declare @JournalVoucherId as int
			select @JournalVoucherId = IdJournalVoucher  from @resultJournalVoucher
			declare @Consecutive as varchar(max) = isnull( (select cast( Consecutive as varchar(30))  
			from GeneralLedger.JournalVouchers where id = @JournalVoucherId ),0)

			set @ResultConsecutives = @ResultConsecutives + @Consecutive + ','

			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @TempLegalBookId
			continue

		End
		Close InfoItem
		Deallocate InfoItem
		
		--Se obtiene el tipo de comprobante
		declare @JournalVoucherType varchar(max) 
		select @JournalVoucherType = CONCAT(jvt.Code, ' - ', jvt.Name) 
		from FixedAsset.SettingFixedAsset sfa
		inner join GeneralLedger.JournalVoucherTypes jvt on jvt.Id = sfa.IdOutputAccountingVoucher
		where sfa.OperatingUnitId = @OperatingUnitId

		--Se actualizan los campos HasOutput y OutputDate de la tabla FixedAssetPhysical
		update FixedAsset.FixedAssetPhysicalAsset 
			set HasOutput = 1, 
				OutputDate = @DocumentDate,
				Status = 0
		where Id in (select PhysicalAssetId from @FixedAssetActiveOutputDetail)

		select 0 as CodeMessage, 'Se confirmó correctamente' as Message, @Code as Code, @Id as Id, @ResultConsecutives as ResultConsecutives, @JournalVoucherType as JournalVoucherType
		
	end try
	begin catch
		select 999 as CodeMessage, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(10)) as Message, '' as Code, 0 as Id, '' as ResultConsecutives, '' as JournalVoucherType
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que confirma la salida (baja o retiro) de activos fijos en el módulo de activos fijos. Recibe un XML con la cabecera y el detalle de la salida, valida que existan parámetros configurados para la unidad operativa, que la fecha del documento corresponda al mes y año del periodo de proceso, y que el mes contable esté abierto. Una vez superadas las validaciones, genera los comprobantes contables (vouchers) en el libro contable legal correspondiente, registrando los movimientos de débito y crédito asociados a la salida del activo, como baja por venta, donación u otro tipo de retiro.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmFixedAssetActiveOutput';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmFixedAssetActiveOutput';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma la salida (baja, venta o reposición) de activos fijos: valida parámetros y período contable, genera los comprobantes contables por libro legal y marca los activos como salidos.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmFixedAssetActiveOutput';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en FixedAsset.SettingFixedAsset para la unidad operativa indicada; El mes y año de la fecha de documento deben coincidir con la fecha de proceso (ProcessDate) configurada en SettingFixedAsset; El mes/año de la fecha de documento debe estar abierto en GeneralLedger.ClosedMonth (Status = 1); El XML de entrada debe contener cabecera FixedAssetActiveOutput y al menos un detalle FixedAssetActiveOutputDetail; Los activos referenciados deben tener configurado FixedAssetPhysicalAssetDetailBook por libro legal y catálogo contable (FixedAssetItemCatalog) con cuentas asociadas', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmFixedAssetActiveOutput';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] GeneralLedger.JournalVouchers: Por cada LegalBookId distinto se invoca SP_CreateAndValidateJournalVoucherMovement para crear un comprobante contable cuyo tipo es SettingFixedAsset.IdOutputAccountingVoucher con detalle ''Comprobante contable generado desde salida de activos''; [INSERT] GeneralLedger.JournalVouchers: Siempre se genera un detalle al CRÉDITO en la cuenta IncomeAccountId del catálogo del ítem, por el HistoricalValue del activo; [INSERT] GeneralLedger.JournalVouchers: Cuando DepreciatedValue > 0 se inserta un detalle al DÉBITO en la cuenta DepreciationAccountId del catálogo, por el valor de DepreciatedValue; [INSERT] GeneralLedger.JournalVouchers: Cuando ResidualValue > 0 se inserta un detalle al DÉBITO en la cuenta LossMainAccountId del catálogo, por el valor de ResidualValue (pérdida del ejercicio); [INSERT] GeneralLedger.JournalVouchers: Cuando OutputType = 2 (venta) se inserta un detalle al CRÉDITO en NetIncomeAccountId del catálogo y otro al DÉBITO en SalesMainAccountId de SettingFixedAsset, ambos por SalesValue; [INSERT] GeneralLedger.JournalVouchers: Cuando OutputType = 1 y LowType = 3 (reposición) se inserta un detalle al CRÉDITO en ReplacementCreditMainAccountId del catálogo y otro al DÉBITO en ReplacementMainAccountId de SettingFixedAsset, ambos por SalesValue; [UPDATE] FixedAsset.FixedAssetPhysicalAsset: Tras generar los comprobantes, los activos físicos del detalle se actualizan con HasOutput=1, OutputDate=DocumentDate y Status=0; [RETURN_RESULT] : Devuelve CodeMessage=0 y los consecutivos de comprobantes generados (ResultConsecutives) más el tipo de comprobante; en error retorna CodeMessage=999 con el mensaje correspondiente; [RETURN_RESULT] : Si SP_CreateAndValidateJournalVoucherMovement responde code=999 se cierra el cursor y se retorna el error sin actualizar los activos', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmFixedAssetActiveOutput';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe SettingFixedAsset para la OperatingUnitId → Retorna error 999 ''No existe parámetros de activo fijo para la unidad operativa seleccionada'' y termina; si MONTH/YEAR(DocumentDate) <> MONTH/YEAR(SettingFixedAsset.ProcessDate) → Retorna error 999 indicando que el mes/año del documento no coincide con la fecha de proceso de parámetros y termina; si No existe ClosedMonth con Status=1 para el año/mes del DocumentDate → Retorna error 999 ''El mes X no se encuentra abierto'' y termina; si fapadb.DepreciatedValue > 0 (con ResidualValue >= 0) → Genera detalle débito a cuenta de depreciación por DepreciatedValue else No se genera ese detalle; si fapadb.ResidualValue > 0 → Genera detalle débito a cuenta de pérdida del ejercicio por ResidualValue; si OutputType = 2 (venta) → Genera dos detalles adicionales: crédito a cuenta ganancia (NetIncomeAccountId) y débito a cuenta de ventas (SalesMainAccountId), por SalesValue; si OutputType = 1 y LowType = 3 (reposición) → Genera dos detalles adicionales: crédito a ReplacementCreditMainAccountId y débito a ReplacementMainAccountId, por SalesValue; si SP_CreateAndValidateJournalVoucherMovement retorna code=''999'' → Cierra y desasigna el cursor, retorna error 999 con el mensaje recibido y aborta el proceso', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmFixedAssetActiveOutput';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmFixedAssetActiveOutput';
-- GO
