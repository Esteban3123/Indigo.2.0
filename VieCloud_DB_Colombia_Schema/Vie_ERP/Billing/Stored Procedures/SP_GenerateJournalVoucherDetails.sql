CREATE PROCEDURE [Billing].[SP_GenerateJournalVoucherDetails]
	-- Add the parameters for the stored procedure here
	@RevenueControlDetailId as int,
	@OperativeUnitId as int,
	@reverse as bit,
	@InvoiceId as int
AS
BEGIN
	SET NOCOUNT ON;
	
	--declare @reverse as bit = 0
	declare @TableDetailSurgical as table (DetailId INT, SurgicalId int, Value decimal(20,2), GrandTotalSalesPrice DECIMAL(20,2), AdjustmentId INT, AdjustmentValue Decimal(20,2))

	declare @TableDetail as table (MainAccountId int, ThirdPartyId int, CostCenterId int, CreditValue numeric(21,5), DebitValue numeric(21,5), Detail varchar(300))
	declare @DiscountSalesMainAccountId int
	declare @handlesThirdPartyMainAccountSetting bit
	declare @handlesHandlesCostCenterMainAccountSetting bit
	declare @AccountingForSurgical tinyint
	declare @InvoiceJournalVoucherTypeId int
	declare @RecoveryFeeDiscountMainAccountId int
	declare @MainAccountRecoveryFee int
	declare @TotalPatientSalesPrice decimal(20,2)
	declare @TotalPatientDiscount decimal(20,2)
	declare @ThirdPartyPatientId varchar(30)
	declare @RecoveryFeeCostCenterDiscountId int
	declare @CostCenterGaregroupId as int
	declare @careGroupType as int
	DECLARE @LiquidateMasterAccount as BIT 
	Declare @CurrencyInvoiceId AS integer,
			@ReversalPreviousYearsMainAccountId INT,
			@InvoiceDate DATETIME

	BEGIN TRY
	
		/***********************************************  ASIGNACIONES ***********************************************/
	
		set @careGroupType = (select CareGroupType from [Contract].CareGroup cg inner join Billing.RevenueControlDetail rcd on cg.Id = rcd.CareGroupId Where rcd.Id = @RevenueControlDetailId)

		select	@DiscountSalesMainAccountId=DiscountSalesMainAccountId, 
				@handlesThirdPartyMainAccountSetting = ma.HandlesThirdParty,
				@handlesHandlesCostCenterMainAccountSetting = ma.HandlesCostCenter
		from Inventory.SettingInventory si
		inner join GeneralLedger.MainAccounts ma on si.DiscountSalesMainAccountId = ma.Id
		where si.OperatingUnitId = @OperativeUnitId

		--Para saber si contabiliza el detalle qx contablemente o solo contabiliza el procedimiento qx 
		select	@AccountingForSurgical = AccountingForSurgical,
				@InvoiceJournalVoucherTypeId = InvoiceJournalVoucherTypeId,
				@RecoveryFeeDiscountMainAccountId = RecoveryFeeDiscountMainAccountId,
				@RecoveryFeeCostCenterDiscountId = RecoveryFeeDiscountCostCenterId,
				@LiquidateMasterAccount = LiquidateMasterAccount,
				@ReversalPreviousYearsMainAccountId = ReversalPreviousYearsMainAccountId
		from Billing.SettingsBilling where IdOperatingUnit = @OperativeUnitId
		
		select	@MainAccountRecoveryFee = cas.AccountRecoveryFeeId,
				@CostCenterGaregroupId = cg.CostCenterId,
				@CurrencyInvoiceId = ISNULL(i.CurrencyId,cs.OfficialCurrencyId),
				@InvoiceDate = i.InvoiceDate
		from Billing.RevenueControlDetail rcd WITH(NOLOCK)
		inner join Contract.CareGroup cg WITH(NOLOCK) on rcd.CareGroupId = cg.Id
		inner join [Contract].ContractAccountingStructure cas WITH(NOLOCK) on cas.Id = cg.ContractAccountingStructureId
		left join Billing.Invoice i WITH(NOLOCK) on i.RevenueControlDetailId=rcd.Id
		JOIN GeneralLedger.CompanySettings cs WITH(NOLOCK) on 1=1
		where rcd.Id = @RevenueControlDetailId

		---------------------------------Conversión y recálculo valores Folio --------------------------------------
		
		Declare @RecalculateFolioDetails Table(
										StatusResult			Bit,
										MessageResult			Varchar(Max),
	
										RevenueControlDetailId	INTEGER,
										SodDistributionId		INTEGER,
										SubTotalSalesPrice		NUMERIC(20,2),
										GrandTotalDiscount		NUMERIC(20,2),
										UnitGrossValue			NUMERIC(20,2),
										TaxPercentage			NUMERIC(20,2),
										NetWorth				NUMERIC(20,2),
										NetUnitValue			NUMERIC(20,2),
										GrandTotalTaxes			NUMERIC(20,2),
										GrandTotalSalesPrice	NUMERIC(20,2),
										TotalSalesPrice			NUMERIC(20,2),
										InvoicedQuantity		INTEGER,
										IvaId					INTEGER,
										InvoiceThirdPartySalesValue NUMERIC(20,2),
										SubTotalPatientSalesPrice	NUMERIC(20,2)) 
			INSERT INTO @RecalculateFolioDetails
								 SELECT	StatusResult			,
										MessageResult	,
	
										RevenueControlDetailId	,
										SodDistributionId		,
										SubTotalSalesPrice		,
										GrandTotalDiscount		,
										UnitGrossValue			,
										TaxPercentage			,
										NetWorth				,
										NetUnitValue			,
										GrandTotalTaxes			,
										GrandTotalSalesPrice	,
										TotalSalesPrice			,
										InvoicedQuantity		,
										IvaId,
										InvoiceThirdPartySalesValue ,
										SubTotalPatientSalesPrice
			FROM [Billing].[RecalculateFolioDetailsByCurrency](@RevenueControlDetailId,@CurrencyInvoiceId, IIF(@reverse = 1, @InvoiceDate, NULL),@OperativeUnitId)

		IF EXISTS (SELECT 1 FROM @RecalculateFolioDetails WHERE StatusResult=0) BEGIN
			SELECT * from  @TableDetail
			RETURN
		END

		/************************************************************************************************/

		--==Consultamos el Id del tercero del paciente
		select	@ThirdPartyPatientId = ISNULL(rcd.PatientQuotaResponsibleThirdPartyId, tp.Id)
		from Billing.RevenueControl rc 
		inner join Billing.RevenueControlDetail rcd on rcd.RevenueControlId = rc.Id 
		inner join Common.ThirdParty tp on LTRIM(RTRIM(rc.PatientCode)) = tp.Nit
		where rcd.Id = @RevenueControlDetailId

		--==Si se ha aplicado cuota de recuperación va a la cuenta contable configurada en el grupo de atención del folio
		---- Se vuelve a sumar los detalles ya que ahora asi sea como bono el se distribuye en los detalles
		select @TotalPatientSalesPrice = (	select sum(temp.SubTotalPatientSalesPrice) 
											from Billing.ServiceOrderDetail sod 
											JOIN Billing.ServiceOrderDetailDistribution sodd ON sod.Id = sodd.ServiceOrderDetailId
											JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) on sodd.RevenueControlDetailId=rcd.Id
											JOIN @RecalculateFolioDetails temp on sodd.Id=temp.SodDistributionId
											where sodd.RevenueControlDetailId = @RevenueControlDetailId and sod.IsDelete = 0 and @LiquidateMasterAccount=0)
		select @TotalPatientDiscount=COALESCE(PatientDiscount,0) from Billing.Invoice where Id = @InvoiceId			

		/*******************************************  DETALLES COMPROBANTE *******************************************/

		--==VALOR DEL SERVICIO/PRODUCTO SIN INCLUIR EL DESCUENTO (Valor cobrado a Entidad)
		insert into @TableDetail
			select	Detail.MainAccountId, 
					Detail.ThirdPartyId, 
					Detail.CostCenterId, 
					round(iif(@reverse = 1, 
							 (SUM(EntityValue)-(SUM(EntityValue)/(sum(detail.GrandTotalSalesPrice) + Detail.PatientDiscount))*iif(@careGroupType = 3 and @LiquidateMasterAccount=0, Detail.PatientDiscount,0)) - ISNULL(TaxValue,0),
							  0), 2) as CreditValue,			
					round(iif(@reverse = 1, 0, (SUM(EntityValue)-(SUM(EntityValue)/(SUM(Detail.GrandTotalSalesPrice) + Detail.PatientDiscount))*iif(@careGroupType = 3 and @LiquidateMasterAccount=0, Detail.PatientDiscount,0)) - ISNULL(TaxValue,0)), 2) as DebitValue, 
					Detail.Descripcion				
			from 
			(
				SELECT	ma.Id as MainAccountId,
						IIF(ma.HandlesThirdParty = 1, rcd.ThirdPartyId, null) ThirdPartyId,
						IIF(ma.HandlesCostCenter = 1, sod.CostCenterId, null) CostCenterId,
						IIF(@LiquidateMasterAccount=0, temp.InvoiceThirdPartySalesValue,temp.GrandTotalSalesPrice) as EntityValue,
						'Detalle de la CxC al Cliente' as Descripcion,
						temp.GrandTotalSalesPrice,
						rcd.TotalFolio,
						rcd.ValueVoucher,
						rcd.PatientDiscount,
						rcd.IsMasterAccount,
						rcd.Id RevenueControlDetailId
				from Billing.RevenueControlDetail rcd WITH(NOLOCK)
				join Contract.CareGroup c WITH(NOLOCK) on c.Id = rcd.CareGroupId
				join [Contract].ContractAccountingStructure cas WITH(NOLOCK) on cas.Id = c.ContractAccountingStructureId
				join Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) on sodd.RevenueControlDetailId = rcd.Id
				join Billing.ServiceOrderDetail sod WITH(NOLOCK) on sodd.ServiceOrderDetailId = sod.Id
				JOIN @RecalculateFolioDetails temp  ON sodd.Id=temp.SodDistributionId 
				left join GeneralLedger.MainAccounts ma on ma.Id =(CASE
																	WHEN rcd.IsMasterAccount=4 and c.CareGroupType <>3 THEN @MainAccountRecoveryFee
																	WHEN c.CareGroupType = 3 THEN cas.AccountParticularId																	
																	ELSE cas.AccountWithoutRadicateId END) --IIF(c.CareGroupType = 3, cas.AccountParticularId, cas.AccountWithoutRadicateId)
				where rcd.Id = @revenueControlDetailId and sod.IsDelete = 0 And rcd.TotalFolio <> 0
			) as Detail 
			JOIN Billing.Invoice i WITH(NOLOCK) ON i.RevenueControlDetailId=Detail.RevenueControlDetailId
			LEFT JOIN (	SELECT itd.InvoiceId, SUM(itd.Value) TaxValue 
							FROM [Billing].[InvoiceTaxDevolution] itd WITH(NOLOCK)
							GROUP BY itd.InvoiceId ) itd on  i.Id= itd.InvoiceId
			group by Detail.MainAccountId, Detail.ThirdPartyId, Detail.CostCenterId, Detail.Descripcion, Detail.ValueVoucher,Detail.PatientDiscount, Detail.TotalFolio,Detail.IsMasterAccount,itd.TaxValue

		--==RETENCIONES
		insert into @TableDetail
			SELECT	ma.Id MainAccountId,
					IIF(ma.HandlesThirdParty = 1, i.ThirdPartyId, NULL) ThirdPartyId,
					IIF(ma.HandlesCostCenter = 1, cg.CostCenterId, NULL) CostCenterId,
					SUM(IIF(@reverse = 1, 0, icr.Value)) CreditValue,
					SUM(IIF(@reverse = 1, icr.Value, 0)) DebitValue,
					'Anticipos de impuestos' Detail
			FROM Billing.InvoiceCustomerRetention icr  WITH(NOLOCK)
			JOIN Billing.Invoice i WITH(NOLOCK) ON icr.InvoiceId = i.Id
			JOIN Contract.CareGroup cg WITH(NOLOCK) ON i.CareGroupId = cg.Id
			JOIN Contract.ContractAccountingStructure cas WITH(NOLOCK) ON cg.ContractAccountingStructureId = cas.Id
			JOIN GeneralLedger.MainAccounts ma  WITH(NOLOCK) ON ma.Id = IIF(cg.CareGroupType = 3, cas.AccountParticularId, cas.AccountWithoutRadicateId)
			WHERE icr.InvoiceId = @InvoiceId AND icr.CalculateTaxAdvance = 2
			GROUP BY ma.Id, ma.HandlesThirdParty, i.ThirdPartyId, ma.HandlesCostCenter, cg.CostCenterId
		UNION ALL
			SELECT	ma.Id MainAccountId,
					IIF(ma.HandlesThirdParty = 1, i.ThirdPartyId, NULL) ThirdPartyId,
					IIF(ma.HandlesCostCenter = 1, cg.CostCenterId, NULL) CostCenterId,
					SUM(IIF(@reverse = 1, icr.Value, 0)) CreditValue,
					SUM(IIF(@reverse = 1, 0, icr.Value)) DebitValue,
					'Anticipos de impuestos' Detail
			FROM Billing.InvoiceCustomerRetention icr WITH(NOLOCK)
			JOIN Billing.Invoice i WITH(NOLOCK) ON icr.InvoiceId = i.Id
			JOIN Contract.CareGroup cg WITH(NOLOCK) ON i.CareGroupId = cg.Id
			JOIN Common.CustomerRetention cr WITH(NOLOCK) ON icr.CustomerRetentionId = cr.Id
			JOIN Portfolio.PortfolioNoteConcept pnc WITH(NOLOCK) ON cr.PortfolioNoteConceptId = pnc.Id
			JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = pnc.IdAccount
			WHERE icr.InvoiceId = @InvoiceId AND icr.CalculateTaxAdvance = 2
			GROUP BY ma.Id, ma.HandlesThirdParty, i.ThirdPartyId, ma.HandlesCostCenter, cg.CostCenterId

		--==DESCUENTOS A LA ENTIDAD
		insert into @TableDetail
			select	ma.Id as MainAccountId,
					IIF(ma.HandlesThirdParty = 1, rcd.ThirdPartyId, null) ThirdPartyId,
					IIF(ma.HandlesCostCenter = 1, sod.CostCenterId, null) CostCenterId,
					iif(@reverse = 1, Sum(temp.GrandTotalDiscount), 0) as CreditValue, 
					iif(@reverse = 1, 0, Sum(temp.GrandTotalDiscount)) as DebitValue,
					'Descuentos a la entidad' Detail
			from Billing.RevenueControlDetail rcd WITH(NOLOCK)
			join Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) on sodd.RevenueControlDetailId = rcd.Id
			join @RecalculateFolioDetails temp on sodd.Id=temp.SodDistributionId
			join Billing.ServiceOrderDetail sod WITH(NOLOCK) on sodd.ServiceOrderDetailId = sod.Id
			join Contract.CUPSEntity ce WITH(NOLOCK) on sod.CUPSEntityId = ce.Id
			join Billing.BillingConcept bc WITH(NOLOCK) on ce.BillingConceptId = bc.Id
			join GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = IIF(sod.RecordType = 1, @DiscountSalesMainAccountId, bc.DiscountAccountId)
			where	rcd.Id = @revenueControlDetailId 
					and sod.ThirdPartyDiscount > 0
					and sod.IsDelete = 0 
					and @LiquidateMasterAccount=0
			group by ma.Id,ma.HandlesThirdParty,rcd.ThirdPartyId,ma.HandlesCostCenter,sod.CostCenterId

		--==DESCUENTOS A SERVICIOS - FOLIO PACIENTE - ASEGURADORA (CUENTA MADRE)
		insert into @TableDetail (MainAccountId,ThirdPartyId,CostCenterId,CreditValue,DebitValue,Detail)
		SELECT	ma.Id AS MainAccountId, 
				IIF(ma.HandlesThirdParty = 1, rcd.ThirdPartyId, NULL) AS ThirdPartyId,
				IIF(ma.HandlesCostCenter = 1, sod.CostCenterId, NULL) AS CostCenterId,					
				IIF(@reverse = 1,SUM(temp.GrandTotalDiscount),0) AS CreditValue,
				IIF(@reverse = 1,0,SUM(temp.GrandTotalDiscount)) AS DebitValue,
				'Descuentos a Servicios' Detail
		FROM Billing.RevenueControlDetail rcd WITH(NOLOCK)
		JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) on sodd.RevenueControlDetailId = rcd.Id
		JOIN @RecalculateFolioDetails temp on sodd.Id = temp.SodDistributionId
		JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sodd.ServiceOrderDetailId = sod.Id
		JOIN Contract.CUPSEntity ce WITH(NOLOCK) on sod.CUPSEntityId = ce.Id
		JOIN Payroll.FunctionalUnit fu WITH(NOLOCK) on fu.Id =sod.PerformsFunctionalUnitId
		join Billing.BillingConcept bc WITH(NOLOCK) on ce.BillingConceptId = bc.Id
		left join Billing.BillingConceptAccount bca WITH(NOLOCK) on bca.BillingConceptId = bc.Id and bca.UnitType = iif(fu.UnitType not in (1,2,3,4),2,fu.UnitType)
		join GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = iif(bc.AccountingType=2, bca.DiscountAccountId,bc.DiscountAccountId)
		WHERE rcd.Id = @RevenueControlDetailId 			
			AND sod.RecordType = 1 
			AND sod.Presentation <> 2 
			AND sod.IsDelete = 0 AND @LiquidateMasterAccount =1 AND sodd.GrandTotalDiscount > 0
		GROUP BY ma.Id, ma.HandlesThirdParty, rcd.ThirdPartyId, ma.HandlesCostCenter, sod.CostCenterId,sod.PerformsFunctionalUnitId

		--==DESCUENTOS A Productos - GENERAL CO  - CR
		insert into @TableDetail
		SELECT
			ma.Id AS MainAccountId, 
			IIF(ma.HandlesThirdParty = 1, rcd.ThirdPartyId, NULL) AS IdThirdParty,
			IIF(ma.HandlesCostCenter = 1, sod.CostCenterId, NULL) AS CostCenterId,					
			IIF(@reverse = 1,SUM(temp.GrandTotalDiscount),0) AS CreditValue,
			IIF(@reverse = 1,0,SUM(temp.GrandTotalDiscount)) AS DebitValue,
			'Descuentos a Productos' Detail
		FROM Billing.RevenueControlDetail rcd WITH(NOLOCK)
		JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) on sodd.RevenueControlDetailId = rcd.Id
		JOIN @RecalculateFolioDetails temp on sodd.Id=temp.SodDistributionId
		JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sodd.ServiceOrderDetailId = sod.Id
		JOIN Inventory.InventoryProduct ip WITH(NOLOCK) on sod.ProductId = ip.Id
		join Inventory.ProductGroup pg WITH(NOLOCK) on ip.ProductGroupId = pg.Id
		JOIN Inventory.ProductGroupFunctionalUnit pgfu WITH(NOLOCK) on pgfu.ProductGroupId= pg.Id and sod.PerformsFunctionalUnitId= pgfu.FunctionalUnitId
		join GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = pgfu.DiscountAccountId
		WHERE rcd.Id = @RevenueControlDetailId 			
			AND sod.RecordType = 2 
			AND (sod.Presentation <> 2 OR sod.Presentation is NULL)
			AND sod.IsDelete = 0  AND sodd.GrandTotalDiscount > 0 
		GROUP BY ma.Id, ma.HandlesThirdParty, rcd.ThirdPartyId, ma.HandlesCostCenter, sod.CostCenterId, sod.PerformsFunctionalUnitId
		
		--==IVA DEVUELTO
		insert into @TableDetail
		SELECT
			ma.Id AS MainAccountId, 
			IIF(ma.HandlesThirdParty = 1, rcd.ThirdPartyId, NULL) AS IdThirdParty,
			NULL AS CostCenterId,					
			IIF(@reverse = 1,sum(itd.Value),0) AS CreditValue,
			IIF(@reverse = 1,0,sum(itd.Value)) AS DebitValue,
			'IVA Devuelto' Detail
		FROM Billing.RevenueControlDetail rcd WITH(NOLOCK)
		JOIN Billing.Invoice i WITH(NOLOCK) ON rcd.Id = i.RevenueControlDetailId
		JOIN Billing.InvoiceTaxDevolution itd WITH(NOLOCK) ON itd.InvoiceId=i.Id
		JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) ON itd.TaxId=iva.Id
		JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = iva.IdAccountSale
		WHERE rcd.Id = @RevenueControlDetailId AND itd.ValueInOfficialCurrency > 0
		GROUP BY ma.Id, ma.HandlesThirdParty, rcd.ThirdPartyId, ma.HandlesCostCenter

		--==DESCUENTOS AL PACIENTE
		--if @TotalPatientSalesPrice - @TotalPatientDiscount > 0
		--begin
			insert into @TableDetail 
				select	ma.Id as MainAccountId,
						-- IIF(ma.HandlesThirdParty = 1, @ThirdPartyPatientId, null) ThirdPartyId,
						-- se toma el tercero de la entidad PBI 12961
						IIF(ma.HandlesThirdParty = 1, rcd.ThirdPartyId, null) ThirdPartyId,
						IIF(ma.HandlesCostCenter = 1, @CostCenterGaregroupId, null) CostCenterId,
						iif(@reverse = 1, (@TotalPatientSalesPrice - @TotalPatientDiscount), 0) as CreditValue, 
						iif(@reverse = 1, 0, (@TotalPatientSalesPrice - @TotalPatientDiscount)) as DebitValue,
						-- 'Descuentos al paciente' as Detail
						'Detalle de Copagos' as Detail
				from Billing.RevenueControlDetail rcd WITH(NOLOCK)
				join Contract.CareGroup cg on rcd.CareGroupId = cg.Id
				left join Billing.BillingConcept bc ON cg.BillingConceptCopayId = bc.Id
				-- inner join GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = @MainAccountRecoveryFee
				-- se toma la cuenta contable de recoperacion del grupo de atención PBI 12961
				inner join GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = iif(cg.LiquidationType = 1, bc.CopayMainAccountId, bc.RecoveryFixedAmountMainAccountId)
				where rcd.Id = @revenueControlDetailId AND  @LiquidateMasterAccount= 0
		--end

		--==DESCUENTOS A LA CUOTA DE RECUPERACION
		if @TotalPatientDiscount > 0
		begin
			insert into @TableDetail 
				select	ma.Id as MainAccountId,
						IIF(ma.HandlesThirdParty = 1, @ThirdPartyPatientId, null) ThirdPartyId,
						IIF(ma.HandlesCostCenter = 1, @RecoveryFeeCostCenterDiscountId, null) CostCenterId,
						iif(@reverse = 1, @TotalPatientDiscount, 0) as CreditValue, 
						iif(@reverse = 1, 0, @TotalPatientDiscount) as DebitValue,
						'Descuentos a la cuota de recuperacion' as Detail
				from Billing.RevenueControlDetail rcd WITH(NOLOCK)
				inner join GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = @RecoveryFeeDiscountMainAccountId
				where rcd.Id = @revenueControlDetailId AND  @LiquidateMasterAccount= 0
		end

		--==INGRESOS POR LOS PRODUCTOS Y SERVICIOS
		INSERT INTO @TableDetail
			SELECT 
				IdMainAccount,	--se envia la cta contable
								-- de ingreso y en SP_AnulateInvoice (reversion) se actualiza con la cta de reversion de vigencias anteriores de ser el caso.
				IdThirdParty,
				IdCostCenter,
				IIF(@reverse = 1, DebitValue, CreditValue), 
				IIF(@reverse = 1, CreditValue, DebitValue),
				'Producto/Servicio' --se agrega este detalle para identificar el detalle de las ctas de ingreso de servicio/producto 
									-- BUG-18987 No actualizar sin previo aviso se usa en el SP_AnulateInvoice 
			FROM Billing.fnJournalVoucherIncomeInvoiceDetail(@RevenueControlDetailId, @AccountingForSurgical, 1, IIF(@reverse = 1, @InvoiceDate, NULL)) 

		DELETE FROM @TableDetail where CreditValue = 0 and DebitValue = 0

		SELECT * FROM @TableDetail
	END TRY
	BEGIN CATCH
		select * from @TableDetail
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el detalle del comprobante contable (asiento de diario) para la facturación y las cuentas por cobrar asociadas a un folio de liquidación. Toma un detalle de control de ingresos (folio), recalcula sus valores en la moneda de la factura y construye las líneas contables de débito y crédito para cuentas de ingresos, descuentos, cuotas moderadoras y copagos, respetando la estructura contable del contrato y los parámetros del grupo de atención. También soporta el proceso inverso (reversión de asientos) para anulaciones o ajustes de facturas de períodos anteriores. Es el núcleo del cierre contable del ciclo de facturación: convierte los valores clínicos y comerciales del folio en movimientos contables listos para el libro mayor.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherDetails';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherDetails';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye las líneas de débito/crédito (detalles del comprobante contable) de la factura de un folio, agrupando CxC a la entidad, retenciones, descuentos a entidad/servicios/productos/paciente, IVA devuelto, descuentos de cuota de recuperación e ingresos, soportando reverso.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El RevenueControlDetail debe existir y estar asociado a un CareGroup con ContractAccountingStructure válida.; Debe existir configuración de facturación (Billing.SettingsBilling) e inventario (Inventory.SettingInventory) para la unidad operativa.; Debe existir CompanySettings con moneda oficial configurada.; La función Billing.RecalculateFolioDetailsByCurrency debe retornar StatusResult=1 para todos los registros; de lo contrario se aborta retornando un resultado vacío.; Si @reverse=1, se requiere fecha de factura (InvoiceDate) para recalcular el folio en moneda histórica.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @TableDetail (resultset): Si @RecalculateFolioDetails contiene algún StatusResult=0, se devuelve la tabla vacía y se aborta el proceso.; [RETURN_RESULT] @TableDetail (resultset): Inserta línea de CxC al cliente con la cuenta del MainAccountRecoveryFee si IsMasterAccount=4 y CareGroupType<>3; AccountParticularId si CareGroupType=3; AccountWithoutRadicateId en otro caso. Sólo cuando rcd.TotalFolio<>0 y sod.IsDelete=0.; [RETURN_RESULT] @TableDetail (resultset): Cuando @reverse=1, el valor de la entidad se ajusta restando proporcionalmente el PatientDiscount sólo si CareGroupType=3 y @LiquidateMasterAccount=0, y se descuenta el TaxValue (IVA devuelto).; [RETURN_RESULT] @TableDetail (resultset): Inserta líneas de retenciones (Anticipos de impuestos) sólo cuando InvoiceCustomerRetention.CalculateTaxAdvance=2; usa la cuenta del contrato (Particular o SinRadicar según CareGroupType) y además la cuenta del PortfolioNoteConcept asociado.; [RETURN_RESULT] @TableDetail (resultset): Inserta ''Descuentos a la entidad'' cuando @LiquidateMasterAccount=0, sod.ThirdPartyDiscount>0 y sod.IsDelete=0; usa DiscountSalesMainAccountId si RecordType=1, o BillingConcept.DiscountAccountId.; [RETURN_RESULT] @TableDetail (resultset): Inserta ''Descuentos a Servicios'' (cuenta madre) sólo cuando @LiquidateMasterAccount=1, RecordType=1, Presentation<>2, IsDelete=0 y GrandTotalDiscount>0; usa BillingConceptAccount.DiscountAccountId si AccountingType=2 o BillingConcept.DiscountAccountId.; [RETURN_RESULT] @TableDetail (resultset): Inserta ''Descuentos a Productos'' cuando RecordType=2, Presentation<>2 o NULL, IsDelete=0 y GrandTotalDiscount>0; usa ProductGroupFunctionalUnit.DiscountAccountId.; [RETURN_RESULT] @TableDetail (resultset): Inserta ''IVA Devuelto'' cuando InvoiceTaxDevolution.ValueInOfficialCurrency>0, usando GeneralLedgerIVA.IdAccountSale; CostCenterId siempre NULL.; [RETURN_RESULT] @TableDetail (resultset): Inserta ''Detalle de Copagos'' (paciente) por @TotalPatientSalesPrice-@TotalPatientDiscount sólo cuando @LiquidateMasterAccount=0; usa CopayMainAccountId si LiquidationType=1, o RecoveryFixedAmountMainAccountId.; [RETURN_RESULT] @TableDetail (resultset): Inserta ''Descuentos a la cuota de recuperacion'' por @TotalPatientDiscount sólo si @TotalPatientDiscount>0 y @LiquidateMasterAccount=0, usando RecoveryFeeDiscountMainAccountId y RecoveryFeeDiscountCostCenterId.; [RETURN_RESULT] @TableDetail (resultset): Inserta líneas de ingreso ''Producto/Servicio'' obtenidas de la función Billing.fnJournalVoucherIncomeInvoiceDetail, invirtiendo débito/crédito cuando @reverse=1.; [DELETE] @TableDetail (resultset): Elimina del resultado las filas con CreditValue=0 y DebitValue=0 antes de retornar.; [RETURN_RESULT] @TableDetail (resultset): En caso de error en TRY, el CATCH retorna el contenido actual de @TableDetail sin propagar la excepción.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetails';
-- GO
