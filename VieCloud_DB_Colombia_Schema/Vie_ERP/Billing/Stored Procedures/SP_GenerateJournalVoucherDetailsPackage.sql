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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera los detalles del comprobante contable (asiento de diario) correspondiente a facturas que contienen servicios empaquetados (paquetes contratados). A partir del ID de una factura, identifica todos los ítems de órdenes de servicio agrupados en paquetes, los cruza con la configuración contractual del paquete (productos y servicios con sus valores y cantidades pactadas versus las realmente liquidadas), y calcula las desviaciones de precio y cantidad para distribuir correctamente los valores en las cuentas contables de ingreso, variación favorable y desvío proyectado. Soporta tanto la generación del comprobante de facturación como su reversión por anulación, y usa los parámetros contables configurados por unidad operativa para determinar las cuentas destino de cada movimiento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherDetailsPackage';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherDetailsPackage';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye los detalles del comprobante contable para facturación o anulación de servicios tipo paquete, calculando débitos/créditos por valor facturado, variaciones de precio y desviaciones favorables sobre servicios y medicamentos/insumos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsPackage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura indicada debe existir en Billing.Invoice y tener un RevenueControlDetailId y OperatingUnitId asociados.; Debe existir un registro en Billing.SettingsBilling para la unidad operativa de la factura con AccountingPackageMainAccountId, ProjectedVariationPriceMainAccountId y LiquidatedPackageJournalVoucherTypeId configurados (no nulos).; El folio (RevenueControlDetail) debe contener al menos un ServiceOrderDetail marcado como IsPackage = 1 para producir filas.; Cada BillingConcept de los servicios empaquetados (ItemType=2) debe tener una fila en Billing.BillingConceptAccountingPackage para su UnitType.; Cada producto (medicamento/insumo, ItemType=1) debe estar asociado a un ProductGroup con cuentas contables configuradas (AccountingPackageMainAccountId, FavorableDeviationMainAccountId, VariationPVMainAccountId).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsPackage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @tbJournalVoucherDetails: Inserta dos filas iniciales: una con AccountingPackageMainAccountId por el ''Valor Facturado'' (=suma GrandTotalSalesPrice del paquete) y otra con ProjectedVariationPriceMainAccountId por la ''Variación de precio proyectado'' (=valorNegociado - valorProyectado), invirtiendo débito/crédito según @isAnnulment.; [INSERT] @tbJournalVoucherDetails: Por cada ítem empaquetado de tipo servicio (ItemType=2) inserta una fila con la cuenta ConceptMainAccountId de BillingConceptAccountingPackage; el ValorLiquidacion va al débito si @isAnnulment=1, al crédito en caso contrario (''Valor facturado del paquete servicios'').; [INSERT] @tbJournalVoucherDetails: Por cada ítem empaquetado tipo producto (ItemType=1) inserta una fila con la cuenta AccountingPackageMainAccountId del ProductGroup del producto; ValorLiquidacion al débito si anulación, al crédito si no (''Valor facturado del paquete medicamentos/insumos'').; [INSERT] @tbJournalVoucherDetails: Inserta filas de ''Desv. Favorable servicios'' usando FavorableDeviationMainAccountId de BillingConceptAccountingPackage solo para ítems ItemType=2 con DesviacionFavorable <> 0.; [INSERT] @tbJournalVoucherDetails: Inserta filas de ''Desv. Favorable medicamentos/insumos'' usando FavorableDeviationMainAccountId del ProductGroup, solo para ItemType=1 con DesviacionFavorable <> 0.; [INSERT] @tbJournalVoucherDetails: Inserta filas ''Variación PV Servicios'' con VariationPVMainAccountId del concepto, solo para ItemType=2 con DesviacionPrecionUnitario <> 0; el signo de la desviación define si va a débito o crédito según @isAnnulment.; [INSERT] @tbJournalVoucherDetails: Inserta filas ''Variación PV medicamentos/insumos'' con VariationPVMainAccountId del ProductGroup, solo para ItemType=1 con DesviacionPrecionUnitario <> 0.; [RETURN_RESULT] @tbJournalVoucherDetails: Al final ejecuta SELECT * FROM @tbJournalVoucherDetails devolviendo todos los renglones del comprobante contable generado.; [RAISERROR] (error): Lanza THROW 51000 cuando AccountingPackageMainAccountId, ProjectedVariationPriceMainAccountId o LiquidatedPackageJournalVoucherTypeId no están configurados en SettingsBilling para la unidad operativa.; [RAISERROR] (error): Lanza THROW 51000 listando los conceptos de facturación (Code - Name) de servicios empaquetados que no tienen parametrización en BillingConceptAccountingPackage para su UnitType.; [RAISERROR] (error): Lanza THROW 51000 cuando, tras cualquier inserción, queda algún IdMainAccount NULL en @tbJournalVoucherDetails (cuentas de servicios, medicamentos, desviación favorable o variación PV no configuradas).; [RETURN_RESULT] @StatusResult/@MessageResult: En caso de excepción, @StatusResult se fija en 0 y @MessageResult al ERROR_MESSAGE(); en éxito @StatusResult=1 y @MessageResult=''''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsPackage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsPackage';
-- GO
