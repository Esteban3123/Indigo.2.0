CREATE PROCEDURE [Billing].[SP_GenerateJournalVoucherDetailsPackage]
	@InvoiceId int,
	@UserCode varchar(20),
	@isAnnulment bit,
	--Salidas
    @StatusResult bit output,
    @MessageResult varchar(max) output
as
begin
	SET NOCOUNT ON
	
	begin try
		--SP Para generación de los detalles del comprobante contable de paquetes
		declare @OperationUnitId Int,
			@RevenueControlDetailId Int

		select @RevenueControlDetailId = RevenueControlDetailId
			, @OperationUnitId = OperatingUnitId 
		from Billing.Invoice (nolock) where Id = @InvoiceId

		declare @tbJournalVoucherDetails table (
			IdMainAccount int, 
			IdThirdParty int, 
			IdCostCenter int, 
			CreditValue numeric(18, 2), 
			DebitValue numeric(18, 2), 
			Detail varchar(300)
		)
		declare @tbServiceOrderDetailPackages table (
			ServiceOrderDetailId int not null,
			ContractPackageId int not null,
			CostCenterId int,
			GrandTotalSalesPrice numeric(18, 0)
		)
		declare @tbServiciosEmpaquetados table (
			ServiceOrderDetailId int not null,
			IPServiceId int,
			CUPSEntityId int,
			ProductId int,
			Quantity int,
			UnitValue numeric(18, 0),
			CostCenterId int,
			FunctionalUnitType tinyint,
			BillingConceptId int,
			GrandTotalSalesPrice numeric(18, 0) not null --ValorNegociado
		)
		declare @tbConfiguracionPaquete table (
			ContractPackageItemId int,
			ItemType tinyint,
			ItemId int,
			ValorUnitarioAsignado numeric(18, 0),
			CantidadConfigurada int,
			ValorUnitarioLiquidacion numeric(18, 0),
			CantidadUtilizada int,
			ValorPaquete numeric(18, 0),
			ValorLiquidacion numeric(18, 0),
			VariacionCantidad int,
			DesviacionPrecionUnitario numeric(18, 0),
			DesviacionFavorable numeric(18, 0),
			CostCenterId int,
			FunctionalUnitType tinyint,
			BillingConceptId int
		)

		declare @ThirdPartyFolioId int		
		delete from @tbJournalVoucherDetails
		delete from @tbServiceOrderDetailPackages
		delete from @tbServiciosEmpaquetados
		delete from @tbConfiguracionPaquete

		select @ThirdPartyFolioId = ThirdPartyId from Billing.RevenueControlDetail (nolock) where Id = @RevenueControlDetailId

		--servicios de tipo paquete en el folio
		insert into @tbServiceOrderDetailPackages
		select distinct sod.Id, sod.ContractPackageId, sod.CostCenterId, sod.GrandTotalSalesPrice
		from Billing.RevenueControlDetail rcd
		join Billing.ServiceOrderDetailDistribution sodd on sodd.RevenueControlDetailId = rcd.Id
		join Billing.ServiceOrderDetail sod on sodd.ServiceOrderDetailId = sod.Id
		where sod.IsPackage = 1 and rcd.Id = @RevenueControlDetailId

		-- Items empaquetados para todos los paquetes del folio
		insert into @tbServiciosEmpaquetados
		select sod.Id
			, sod.IPSServiceId
			, sod.CUPSEntityId
			, sod.ProductId
			, sod.InvoicedQuantity
			, sod.SubTotalSalesPrice
			, sod.CostCenterId
			, fu.UnitType
			, sod.BillingConceptId
			, sod.GrandTotalSalesPrice
		from Billing.ServiceOrderDetail sod (nolock)
		join @tbServiceOrderDetailPackages tp on tp.ServiceOrderDetailId = sod.PackageServiceOrderDetailId
		join Payroll.FunctionalUnit fu (nolock) on sod.PerformsFunctionalUnitId = fu.Id

		-- Items configurados para los paquetes			
		insert into @tbConfiguracionPaquete	
		select * from (
			select 
				cpp.Id
				, 1 as ItemType
				, cpp.ProductId as ItemId
				, cpp.UnitValue as ValorUnitarioAsignado
				, cpp.Quantity as CantidadConfigurada
				, te.UnitValue as ValorUnitarioLiquidacion
				, te.Quantity as CantidadUtilizada
				, (cpp.UnitValue * cpp.Quantity) as ValorPaquete
				, (te.UnitValue * te.Quantity) as ValorLiquidacion
				, (cpp.Quantity - te.Quantity) as VariacionCantidad
				, (cpp.UnitValue - te.UnitValue) * te.Quantity as DesviacionPrecionUnitario
				, (cpp.UnitValue * (cpp.Quantity - te.Quantity)) as DesviacionFavorable
				, te.CostCenterId
				, te.FunctionalUnitType
				, te.BillingConceptId
			from @tbServiceOrderDetailPackages tp
			join Contract.ContractPackageProduct cpp (nolock) on cpp.ContractPackageId = tp.ContractPackageId
			join @tbServiciosEmpaquetados te on cpp.ProductId = te.ProductId

			union all

			select 
				cps.Id
				, 2 as ItemType
				, cps.CUPSEntityId as ItemId
				, cps.UnitValue as ValorUnitarioAsignado
				, cps.Quantity as CantidadConfigurada
				, te.UnitValue as ValorUnitarioLiquidacion
				, te.Quantity as CantidadUtilizada
				, (cps.UnitValue * cps.Quantity) as ValorPaquete
				, (te.UnitValue * te.Quantity) as ValorLiquidacion
				, (cps.Quantity - te.Quantity)as VariacionCantidad
				, (cps.UnitValue - te.UnitValue) * te.Quantity as DesviacionPrecionUnitario
				, (cps.UnitValue * (cps.Quantity - te.Quantity)) as DesviacionFavorable
				, te.CostCenterId
				, te.FunctionalUnitType
				, te.BillingConceptId
			from @tbServiceOrderDetailPackages tp
			join Contract.ContractPackageService cps (nolock) on cps.ContractPackageId = tp.ContractPackageId
			join @tbServiciosEmpaquetados te on cps.CUPSEntityId = te.CUPSEntityId
		) as t
		
		declare @valorNegociado decimal = (select sum(GrandTotalSalesPrice) from @tbServiceOrderDetailPackages)
		declare @valorProyectado decimal = (select sum(ValorPaquete) from @tbConfiguracionPaquete)

		declare @AccountingPackageMainAccountId int, -- Cuenta de ingreso del paquete
			@ProjectedVariationPriceMainAccountId int,
			@LiquidatedPackageJournalVoucherTypeId int

		select @AccountingPackageMainAccountId = AccountingPackageMainAccountId
			, @ProjectedVariationPriceMainAccountId = ProjectedVariationPriceMainAccountId
			, @LiquidatedPackageJournalVoucherTypeId = LiquidatedPackageJournalVoucherTypeId
		from Billing.SettingsBilling (nolock) where IdOperatingUnit = @OperationUnitId

		if @AccountingPackageMainAccountId is null begin
			; throw 51000, 'La cuenta contable para contabilizar paquetes no ha sido configurada en los parámetros de facturación.', 1;
		end

		if @ProjectedVariationPriceMainAccountId is null begin
			; throw 51000, 'La cuenta contable de variación del precio del paquete no ha sido configurada en los parámetros de facturación.', 1;
		end

		if @LiquidatedPackageJournalVoucherTypeId is null begin
			; throw 51000, 'Tipo de comprobante paquetes liquidados no ha sido configurado en los parámetros de facturación para la unidad operativa indicada.', 1;
		end
				
		insert into @tbJournalVoucherDetails
		values 
			(@AccountingPackageMainAccountId, @ThirdPartyFolioId, 
				(
					select top 1 CostCenterId from @tbServiceOrderDetailPackages
				), 
				iif(@isAnnulment = 1, 0, @valorNegociado), iif(@isAnnulment = 1, @valorNegociado, 0), 'Valor Facturado'
			),
			(@ProjectedVariationPriceMainAccountId, @ThirdPartyFolioId, (
					select top 1 CostCenterId from @tbServiceOrderDetailPackages ----------------------?????????????????????????
				), 
				iif(@isAnnulment = 1, iif(@valorNegociado - @valorProyectado > 0, @valorNegociado - @valorProyectado, 0), iif(@valorNegociado - @valorProyectado > 0, 0, @valorProyectado - @valorNegociado)),
				iif(@isAnnulment = 1, iif(@valorNegociado - @valorProyectado > 0, 0, @valorProyectado - @valorNegociado), iif(@valorNegociado - @valorProyectado > 0, @valorNegociado - @valorProyectado, 0)) , 
				'Variación de precio proyectado'
			)
		
		declare @err varchar(max) = stuff((select distinct concat(CHAR(13), CHAR(10), bc.Code, ' - ', bc.[Name])
		from @tbConfiguracionPaquete tb 
		join Billing.BillingConcept bc on tb.BillingConceptId = bc.Id
		left join Billing.BillingConceptAccountingPackage bca on bca.BillingConceptId = bc.Id and bca.UnitType = tb.FunctionalUnitType
		where tb.ItemType = 2 and bca.Id is null FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
		
		if @err is not null begin
			set @err = 'Los siguientes conceptos de facturación no tienen parametrizado la cuenta de conceptos correctamente. ' + @err
			; throw 51000, @err, 1;
		end
		 
		-- Servicios
		insert into @tbJournalVoucherDetails
		select 
			(
				select top 1 bcp.ConceptMainAccountId
				from Billing.BillingConceptAccountingPackage bcp (nolock)
				where bcp.BillingConceptId = tb.BillingConceptId and bcp.UnitType = tb.FunctionalUnitType
			)
			, @ThirdPartyFolioId
			, tb.CostCenterId
			, iif(@isAnnulment = 1, tb.ValorLiquidacion, 0)
			, iif(@isAnnulment = 1, 0, tb.ValorLiquidacion)
			, 'Valor facturado del paquete servicios'
		from @tbConfiguracionPaquete tb where tb.ItemType = 2

		if exists (select * from @tbJournalVoucherDetails where IdMainAccount is null) begin
			; throw 51000, 'Existen cuentas para los servicios que no han sido configuradas en los conceptos de facturación.', 1;
		end

		--Medicamentos o insumos
		insert into @tbJournalVoucherDetails
		select 
			(
				select top 1 pg.AccountingPackageMainAccountId 
				from inventory.ProductGroup pg (nolock) 
				join inventory.inventoryproduct pr (nolock) on pr.ProductGroupId = pg.Id
				where pr.Id = tb.ItemId
			)
			, @ThirdPartyFolioId
			, tb.CostCenterId
			, iif(@isAnnulment = 1, tb.ValorLiquidacion, 0)
			, iif(@isAnnulment = 1, 0, tb.ValorLiquidacion)
			, 'Valor facturado del paquete medicamentos/insumos'
		from @tbConfiguracionPaquete tb where tb.ItemType = 1		

		if exists (select * from @tbJournalVoucherDetails where IdMainAccount is null) begin
			; throw 51000, 'Existen cuentas para los medicamentos o insumos que no han sido configuradas en los grupos de productos', 1;
		end

		--Desviación favorable servicios
		insert into @tbJournalVoucherDetails
		select 
			(
				select top 1 bcp.FavorableDeviationMainAccountId
				from Billing.BillingConceptAccountingPackage bcp (nolock)
				where bcp.BillingConceptId = tb.BillingConceptId and bcp.UnitType = tb.FunctionalUnitType
			)
			, @ThirdPartyFolioId
			, tb.CostCenterId
			, iif(@isAnnulment = 1, tb.DesviacionFavorable,0)
			, iif(@isAnnulment = 1, 0, tb.DesviacionFavorable)
			, 'Desv. Favorable servicios'
		from @tbConfiguracionPaquete tb where tb.ItemType = 2 and tb.DesviacionFavorable <> 0

		if exists (select * from @tbJournalVoucherDetails where IdMainAccount is null) begin
			; throw 51000, 'Existen cuentas para los servicios que no han sido configuradas en los conceptos de facturación para la desv. favorable.', 1;
		end

		--Desviación favorable medicamentos
		insert into @tbJournalVoucherDetails
		select 
			(
				select top 1 pg.FavorableDeviationMainAccountId
				from inventory.ProductGroup pg (nolock) 
				join inventory.inventoryproduct pr (nolock) on pr.ProductGroupId = pg.Id
				where pr.Id = tb.ItemId
			)
			, @ThirdPartyFolioId
			, tb.CostCenterId
			, iif(@isAnnulment = 1, tb.DesviacionFavorable, 0)
			, iif(@isAnnulment = 1, 0, tb.DesviacionFavorable)
			, 'Desv. Favorable medicamentos/insumos'
		from @tbConfiguracionPaquete tb where tb.ItemType = 1 and tb.DesviacionFavorable <> 0

		if exists (select * from @tbJournalVoucherDetails where IdMainAccount is null) begin
			; throw 51000, 'Existen cuentas para los medicamentos o insumos que no han sido configuradas en los grupos de productos para la desv. favorable', 1;
		end

		--Variación PV servicios
		insert into @tbJournalVoucherDetails
		select 
			(
				select top 1 bcp.VariationPVMainAccountId
				from Billing.BillingConceptAccountingPackage bcp (nolock)
				where bcp.BillingConceptId = tb.BillingConceptId and bcp.UnitType = tb.FunctionalUnitType
			)
			, @ThirdPartyFolioId
			, tb.CostCenterId
			, iif(@isAnnulment = 1, iif(tb.DesviacionPrecionUnitario > 0, tb.DesviacionPrecionUnitario, 0), iif(tb.DesviacionPrecionUnitario > 0, 0, -1*tb.DesviacionPrecionUnitario))
			, iif(@isAnnulment = 1, iif(tb.DesviacionPrecionUnitario > 0, 0, -1*tb.DesviacionPrecionUnitario), iif(tb.DesviacionPrecionUnitario > 0, tb.DesviacionPrecionUnitario, 0))
			, 'Variación PV Servicios'
		from @tbConfiguracionPaquete tb where tb.ItemType = 2 and tb.DesviacionPrecionUnitario <> 0

		if exists (select * from @tbJournalVoucherDetails where IdMainAccount is null) begin
			; throw 51000, 'Existen cuentas para los servicios que no han sido configuradas en los conceptos de facturación para la desv. favorable.', 1;
		end

		--Variación PV medicamentos
		insert into @tbJournalVoucherDetails
		select 
			(
				select top 1 pg.VariationPVMainAccountId
				from inventory.ProductGroup pg (nolock) 
				join inventory.inventoryproduct pr (nolock) on pr.ProductGroupId = pg.Id
				where pr.Id = tb.ItemId
			)
			, @ThirdPartyFolioId
			, tb.CostCenterId
			, iif(@isAnnulment = 1, iif(tb.DesviacionPrecionUnitario > 0, tb.DesviacionPrecionUnitario, 0), iif(tb.DesviacionPrecionUnitario > 0, 0, -1*tb.DesviacionPrecionUnitario))
			, iif(@isAnnulment = 1, iif(tb.DesviacionPrecionUnitario > 0, 0, -1*tb.DesviacionPrecionUnitario), iif(tb.DesviacionPrecionUnitario > 0, tb.DesviacionPrecionUnitario, 0))
			, 'Variación PV medicamentos/insumos'
		from @tbConfiguracionPaquete tb where tb.ItemType = 1 and tb.DesviacionPrecionUnitario <> 0

		if exists (select * from @tbJournalVoucherDetails where IdMainAccount is null) begin
			; throw 51000, 'Existen cuentas para los medicamentos o insumos que no han sido configuradas en los grupos de productos para la desv. favorable', 1;
		end

		select	@StatusResult = convert(bit, 1), @MessageResult = ''
	end try
	begin catch
		select	@StatusResult = convert(bit, 0), @MessageResult = error_message()
	end catch
	
	select * from @tbJournalVoucherDetails
end