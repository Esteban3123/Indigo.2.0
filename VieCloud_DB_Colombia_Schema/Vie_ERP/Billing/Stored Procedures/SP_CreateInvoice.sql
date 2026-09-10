-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2015-09-24
-- Description:	Genera la factura y las cuentas por cobrar de un folio a liquidar
-- =============================================
CREATE PROCEDURE [Billing].[SP_CreateInvoice]
	-- Add the parameters for the stored procedure here
	@RevenueControlDetailId as int,
	@BillingAuthorizationId as int,
	@OperativeUnitId as int,
	@PatientCode as varchar(100),
	@AdmissionNumber as varchar(50),
	@ContainerNameCrystal as varchar(20),
	@userCode as varchar(20),
	@TotalPatientDiscount as decimal(18,0),
	@CrossingValue decimal(18,0),
	@CloseAdmission bit,
	@OutputDate datetime,
	@IsCutAccount bit,
	@OutputDiagnosis char(4),
	@InitialDate datetime,
	@CutType int
AS
BEGIN
	SET NOCOUNT ON;	
	BEGIN TRY	
		declare @FolioId int
		declare @FolioType tinyint		
		declare @FolioOrder tinyint
		declare @careGroupId int
		declare @HealthAdministratorId int
		declare @RevenueControlDetailThirdPartyId int
		declare @TotalFolio decimal(18,0)
		declare @TotalPatientWithDiscount decimal(18,0)
		declare @VoucherValue decimal(18,0)
		declare @TotalPatientSalesPrice decimal(18,0)
		declare @PatientDiscount decimal(18,0)
		declare @PatientDiscountPercentage decimal(5,2)		
		declare @InvoiceNumber as varchar(20)		
		declare @InvoiceDeadlines int
		declare @ThirdPartyDiscountValue decimal(18,0) = 0
		declare @IPTIPOPAC int
		declare @IPTIPOAFI int
		declare @CAPACIPAG int
		declare @NIVECODIGO varchar(10)
		declare @ContractExecuteValue decimal(18,0)
		declare @ContractId int
		declare @LiquidationType tinyint
		declare @CareGroupType tinyint
		declare @DocumentsGenerated varchar(400)	
		declare @ResponsibleRecoveryFee tinyint	
		declare @StatusContract tinyint
		declare @TerminationControl tinyint
		declare @ContractValue decimal(18,0)
		declare @messageValidationContract varchar(max) = ''
		declare @ContractNotificationValueType tinyint
		declare @ContractPercentageNotification decimal(5,2)
		declare @ContractNotificationValue decimal(18,0)
		declare @ContractCodeName varchar(50)
		declare @ContractEndDate date
		declare @ContractNotificationTimeType tinyint
		declare @ContractNotificationDays int
		declare @InvoiceCategoryId int
		
		declare @GenerateAccountReceivableEntity bit = 0
        declare @GenerateAccountReceivablePatient bit = 0
        declare @GenerateTransfer bit = 0
        declare @GenerateAccountReceivablePagare bit = 0
		declare @InvoicePrefix as varchar(5)				
		declare @AuthorizationConsecutive bigint		
		declare @AuthorizationFinalInvoice bigint
		declare @AuthorizationInitialDate date
		declare @AuthorizationFinalDate date
		declare @invoiceDate datetime = Common.Getdate()

		---Recalculo el Folio
		Exec Billing.SP_UpdateRevenueControlDetailValuesNoSelect @RevenueControlDetailId, @OperativeUnitId

		Select @FolioId = rdc.Id,
			   @FolioType = rdc.FolioType,
			   @careGroupId = rdc.CareGroupId,
			   @HealthAdministratorId = rdc.HealthAdministratorId,
			   @RevenueControlDetailThirdPartyId = rdc.ThirdPartyId,
			   @TotalFolio = rdc.TotalFolio,
			   @TotalPatientWithDiscount = rdc.TotalPatientWithDiscount,
			   @VoucherValue = rdc.ValueVoucher,
			   @TotalPatientSalesPrice = rdc.TotalPatientSalesPrice,
			   @InvoiceCategoryId = rdc.InvoiceCategoryId,
			   @PatientDiscount = rdc.PatientDiscount,
			   @PatientDiscountPercentage = rdc.PatientDiscountPercentage, 
			   @ResponsibleRecoveryFee = rdc.ResponsibleRecoveryFee,
			   @FolioOrder = rdc.FolioOrder,			   
			   @InvoiceDeadlines = cg.InvoiceDeadlines,
			   @ContractExecuteValue = c.ExecuteValue,
			   @CareGroupType = cg.CareGroupType,			   
			   @LiquidationType = cg.LiquidationType,
			   @ContractId = c.Id,
			   @StatusContract = c.[Status],
			   @TerminationControl = cd.TerminationControl,
			   @ContractValue = c.ContractValue,
			   @ContractNotificationValueType = cd.NotificationValueType,
			   @ContractPercentageNotification = cd.PercentageNotification,
			   @ContractNotificationValue = cd.NotificationValue,
			   @ContractCodeName = Concat(c.Code, ' - ', cd.ContractName),
			   @ContractEndDate = cd.BillingEndDate,
			   @ContractNotificationTimeType = cd.NotificationTimeType,
			   @ContractNotificationDays = cd.NotificationDays
		From Billing.RevenueControlDetail rdc With(Nolock)
		Inner Join [Contract].CareGroup cg  With(Nolock) on rdc.CareGroupId = cg.Id
		Left Join [Contract].[Contract] c With(Nolock) on cg.ContractId = c.Id
		left join Contract.ContractDetail cd With(Nolock) on cd.ContractId = c.Id and cd.ValidRecord = 1
		Where rdc.id = @RevenueControlDetailId
		
		--Si el grupo de atención es de tipo '1 = EAPB CON CONTRATO', se valida que el contrato asociado tenga un detalle agregado
		if @CareGroupType = 1
		begin
			if not exists(select 1 
			from Contract.CareGroup cg 
			inner join Contract.Contract c on c.Id = cg.ContractId
			inner join Contract.ContractDetail cd on cd.ContractId = c.Id and cd.ValidRecord = 1
			where cg.Id = @careGroupId)
			begin
				select Convert(bit, 0) as StatusResult, 'No se puede liquidar debido a que el contrato asociado al grupo de atención no tiene detalles de contrato o no tiene un detalle válido' as MessageResult,
					NULL as CareGroupType, NULL as LiquidationType, NULL as InvoiceId, NULL as InvoiceNumber, NULL as InvoiceThirdPartySalesValue, NULL as FolioCareGroupId, 
					NULL as FolioThirdPartyId, NULL as FolioTotalPatientWithDiscount, NULL as MessageValidationContract, NULL as InvoiceCategory
				return
			end
		end

		if @VoucherValue <> 0
		begin
			set @TotalPatientSalesPrice = @VoucherValue
			set @TotalPatientWithDiscount = @VoucherValue
		end

		Declare @Cero Int = 0, 
				@Uno Int = 1, 
				@Dos Int = 2, 
				@Tres Int = 3, 
				@Cinco Int = 5, 
				@NueveNueve Int = 99				

		--- Se valida que la autorizacion este activa
		if Not Exists (Select 1 From Billing.BillingAuthorization With(Nolock) Where Id = @BillingAuthorizationId And [Status] = @Uno) 
		begin
			select CONVERT(bit, 0) as StatusResult, 'No se puede liquidar debido a que la autorizacion de facturacion esta inactiva ' + 
				convert(varchar(10),@FolioOrder) as MessageResult ,NULL as CareGroupType, NULL as LiquidationType, NULL as InvoiceId, 
				NULL as InvoiceNumber, NULL as InvoiceThirdPartySalesValue, NULL as FolioCareGroupId, NULL as FolioThirdPartyId, 
				NULL as FolioTotalPatientWithDiscount, NULL as MessageValidationContract, NULL as InvoiceCategory
			return
		end

		--Valido que el folio no tenga items descuadrados en su operacion de multiplicacion por valor unitario		
		Declare @MessageItems Varchar(max) = 'Item: ' + (Select isnull(ips.[Name], ip.[Name]) + ' Cantidad: ' + cast(sod.InvoicedQuantity as varchar(200)) + ' Valor Unitario: ' +  cast(sod.TotalSalesPrice as varchar(200)) + ' Total: ' + cast(sodd.GrandTotalSalesPrice as varchar(300))
		From Billing.RevenueControlDetail rcd With(Nolock)
		Inner Join Billing.ServiceOrderDetailDistribution sodd With(Nolock) On sodd.RevenueControlDetailId = rcd.Id
		Inner Join Billing.ServiceOrderDetail sod With(Nolock) On sod.Id = sodd.ServiceOrderDetailId
		Left Join [Contract].IPSService ips With(Nolock) On ips.Id = sod.IPSServiceId
		Left Join Inventory.InventoryProduct ip With(Nolock) On ip.Id = sod.ProductId
		where (round(sod.InvoicedQuantity * sod.TotalSalesPrice, 0) <> sodd.GrandTotalSalesPrice 
			And round(sod.InvoicedQuantity * sod.TotalSalesPrice, -2) <> sodd.GrandTotalSalesPrice) And sodd.DistributionType = @Uno
			And rcd.Id = @RevenueControlDetailId For Xml Path(''))

		if @MessageItems Is Not Null 
		begin			
			select CONVERT(bit, 0) as StatusResult, 'No se puede liquidar debido a que ya hay items que se encuentran desbalanceados: ' + @MessageItems as MessageResult ,NULL as CareGroupType, NULL as LiquidationType, NULL as InvoiceId, NULL as InvoiceNumber, NULL as InvoiceThirdPartySalesValue, NULL as FolioCareGroupId, NULL as FolioThirdPartyId, NULL as FolioTotalPatientWithDiscount, NULL as MessageValidationContract, NULL as InvoiceCategory
			Return
		end
		
		--==validación de que no haya una factura activa para el mismo folio
		if EXISTS (Select Id From Billing.Invoice With (Nolock) where RevenueControlDetailId = @REVENUECONTROLDETAILID and [Status] = @Uno)
		begin 
			select CONVERT(bit, 0) as StatusResult, 'No se puede liquidar debido a que ya hay una factura activa para el folio '+convert(varchar(10),@FolioOrder) as MessageResult,
				NULL as CareGroupType, NULL as LiquidationType, NULL as InvoiceId, NULL as InvoiceNumber, NULL as InvoiceThirdPartySalesValue, 
				NULL as FolioCareGroupId, NULL as FolioThirdPartyId, NULL as FolioTotalPatientWithDiscount, NULL as MessageValidationContract, NULL as InvoiceCategory
			Return
		end

		DECLARE @BillingAuthorizationIdForInvoice AS INT
		--==obtengo el # de la factura
		If @LiquidationType = 1 --Verifico si es o no pago por servicios, para sacar el consecutivo de la tabla correspondiente
		Begin
			If @FolioType = 4 -- Si es 4 - Aseguradoras
				Set @FolioType = 2	--esto lo dijo el PEZ: 2 - EAPB sin contrato

			--==Actualizo la tabla de billing authorization con el nuevo consecutivo			
			Update Billing.BillingAuthorization 
				Set @InvoicePrefix = InvoicePrefix, 
					@AuthorizationConsecutive = Consecutive = Consecutive + 1,
					@AuthorizationFinalInvoice = FinalInvoice,
					@AuthorizationInitialDate = InitialDate,
					@AuthorizationFinalDate = FinalDate
			Where Id = @BillingAuthorizationId

			Set @AuthorizationConsecutive -= 1
			SET @BillingAuthorizationIdForInvoice = @BillingAuthorizationId

			--==Validación de consecutivos de autorización
			If @AuthorizationConsecutive = @AuthorizationFinalInvoice
			begin
				Select Convert(bit, 0) as StatusResult, 'No hay consecutivos disponibles para asignar a la factura del número de autorización asignado' as MessageResult,
					NULL as CareGroupType, NULL as LiquidationType, NULL as InvoiceId, NULL as InvoiceNumber, NULL as InvoiceThirdPartySalesValue, NULL as FolioCareGroupId, 
					NULL as FolioThirdPartyId, NULL as FolioTotalPatientWithDiscount, NULL as MessageValidationContract, NULL as InvoiceCategory
				Return
			end

			--Si ya se ha parametrizado las fechas de la vigencia
			IF @AuthorizationInitialDate IS NOT NULL AND @AuthorizationFinalDate IS NOT NULL
			BEGIN
				--==Validación de autorización vigente
				IF @invoiceDate < @AuthorizationInitialDate OR @invoiceDate >= @AuthorizationFinalDate
				BEGIN
					SELECT Convert(bit, 0) as StatusResult, 'La autorización asignada no se encuentra vigente' as MessageResult,
						NULL as CareGroupType, NULL as LiquidationType, NULL as InvoiceId, NULL as InvoiceNumber, NULL as InvoiceThirdPartySalesValue, NULL as FolioCareGroupId, 
						NULL as FolioThirdPartyId, NULL as FolioTotalPatientWithDiscount, NULL as MessageValidationContract, NULL as InvoiceCategory
					RETURN
				END
			END

			Set @InvoiceNumber = Concat(@InvoicePrefix, @AuthorizationConsecutive)
		end
		else
		begin
			set @FolioType = 5 --Cambio el tipo de folio a control de capitación
			declare @IdSetting int

			Update Billing.SettingsBilling 
				Set @IdSetting = Id, 
					@InvoicePrefix = PrefixConsecutiveCapitation,
					@AuthorizationConsecutive = ConsecutiveControlCapitation = ConsecutiveControlCapitation + 1
			Where IdOperatingUnit = @OperativeUnitId
						
			Set @AuthorizationConsecutive -= 1

			If IsNull(@IdSetting, 0) <= 0
			Begin
				Select Convert(bit, 0) as StatusResult, 'No se puede liquidar debido a que la unidad operativa con Id '+convert(varchar(10),@OperativeUnitId)+' no existe' as MessageResult, 
					NULL as CareGroupType, NULL as LiquidationType, NULL as InvoiceId, NULL as InvoiceNumber, NULL as InvoiceThirdPartySalesValue, NULL as FolioCareGroupId, 
					NULL as FolioThirdPartyId, NULL as FolioTotalPatientWithDiscount, NULL as MessageValidationContract, NULL as InvoiceCategory
				Return
			End

			Set @InvoiceNumber = Concat(@InvoicePrefix, @AuthorizationConsecutive)
		end

		--Actualizo el tipo de liquidación en el folio para que coincida con el del grupo de atención
		--================================================================================================
		--update Billing.RevenueControlDetail set LiquidationType = @LiquidationType where Id = @RevenueControlDetailId
		
		--==================GUARDA DESCUENTOS===========================
		declare @InvoiceThirdPartySalesValue decimal(18,0) = 0
		DECLARE @InvoiceValue AS DECIMAL(20, 2) = 0

		if @TotalPatientDiscount <> 0
		begin						
			if @CareGroupType = 3
			begin
				--Grupo de atención a particular
				set @ThirdPartyDiscountValue = @TotalPatientDiscount
				set @TotalFolio = @TotalFolio - @TotalPatientDiscount
				set @PatientDiscountPercentage = 0
				set @PatientDiscount = 0
			end				
			else
			begin
				set @PatientDiscount = @TotalPatientDiscount
				set @PatientDiscountPercentage = @TotalPatientDiscount * 100 / @TotalPatientSalesPrice
				set @InvoiceThirdPartySalesValue = @TotalFolio - @TotalPatientSalesPrice
				set @TotalFolio -= @TotalPatientDiscount
				set @TotalPatientWithDiscount = @TotalPatientWithDiscount - @TotalPatientDiscount
			end
		end

		if @CareGroupType = 3 OR @TotalPatientDiscount = 0
		begin			
			set @InvoiceThirdPartySalesValue = @TotalFolio - @TotalPatientSalesPrice			
		end

		SET @InvoiceValue = @InvoiceThirdPartySalesValue + Iif(@FolioType = 3, 0, @TotalPatientWithDiscount)

		--==================================================
		
		Select @ThirdPartyDiscountValue += SUM(GrandTotalDiscount) 
		From Billing.ServiceOrderDetailDistribution sodd With(Nolock)
		Inner Join Billing.ServiceOrderDetail sod With(Nolock) on sodd.ServiceOrderDetailId = sod.Id
		Where sodd.RevenueControlDetailId = @RevenueControlDetailId And sod.IsDelete = @Cero
		
		--==Obtengo los datos de la tabla de INPACIENT
		declare @sqlCrystal as nvarchar(500) = 'select @IPTIPOPAC=IPTIPOPAC,@IPTIPOAFI=IPTIPOAFI,@CAPACIPAG=CAPACIPAG,@NIVECODIGO=NIVECODIGO from ['+@ContainerNameCrystal+'].[dbo].[INPACIENT] where IPCODPACI = '''+@PatientCode+'''';
		exec sp_executesql @sqlCrystal, N'@IPTIPOPAC int output,@IPTIPOAFI int output,@CAPACIPAG int output,@NIVECODIGO char(2) output',@IPTIPOPAC output,@IPTIPOAFI output,@CAPACIPAG output,@NIVECODIGO output

		if Exists 
		(
			Select 1 
			From Billing.RevenueControlDetail i With(Nolock)
			Inner Join [Contract].CareGroup c With(Nolock) On c.Id = i.CareGroupId 
			Where i.Id = @RevenueControlDetailId And HealthAdministratorId Is Null 
				And c.EntityType <> @NueveNueve And c.CareGroupType <> @Tres
		) begin
			select CONVERT(bit, 0) as StatusResult, 'Error la factura no puede quedar con la entidad administradora vacia' as MessageResult ,NULL as CareGroupType, NULL as LiquidationType, NULL as InvoiceId, NULL as InvoiceNumber, NULL as InvoiceThirdPartySalesValue, NULL as FolioCareGroupId, NULL as FolioThirdPartyId, NULL as FolioTotalPatientWithDiscount, NULL as MessageValidationContract, NULL as InvoiceCategory
			return
		end
		
		-- Entidad administradora por defecto para particulares
		IF @FolioType = 3 AND @HealthAdministratorId IS NULL
		BEGIN
			SELECT @HealthAdministratorId = ParticularHealthAdministratorId
			FROM Billing.SettingsBilling
			WHERE IdOperatingUnit = @OperativeUnitId
		END

		--====================GENERACIÓN DE FACTURA======================

		Insert Into Billing.Invoice 
		(
			[OperatingUnitId],[DocumentType],[InvoiceNumber],[RevenueControlDetailId],[AdmissionNumber],
			[HealthAdministratorId],[ThirdPartyId],[PatientCode],[CareGroupId],[InvoiceDate],
			[InvoiceExpirationDate],[TotalInvoice],[CapitationPatientValue],[ThirdPartySalesValue],
			[ThirdPartyDiscountValue],[ResponsibleRecoveryFee],[TotalPatientSalesPrice],[PatientDiscount],
			[PatientDiscountPercentage],[TotalPatientWithDiscount],[ValueVoucher],[PatientPaidValue],
			[ThirdPartyAccountReceivableValue],[PatientAccountReceivableValue],[PatientType],
			[PatientAffiliatedType],[PatientPaidAbility],[PatientSocialClass],[CREETaxRetentionValue],
			[CREETaxRetentionBaseValue],[Status],[InvoicedUser],[InvoicedDate],[InvoiceCategoryId],
			OutputDate,IsCutAccount,OutputDiagnosis,InitialDate,CutType,BillingAuthorizationId,
			ContractId, InvoiceValue, ValueTax, TotalValue
		)
		Values 
		(
			@OperativeUnitId,@FolioType,@InvoiceNumber,@RevenueControlDetailId,@AdmissionNumber,
			@HealthAdministratorId,@RevenueControlDetailThirdPartyId,@PatientCode,@careGroupId,@invoiceDate,
			DATEADD(DAY, @InvoiceDeadlines, Common.Getdate()),@TotalFolio,0,@InvoiceThirdPartySalesValue,
			@ThirdPartyDiscountValue,@ResponsibleRecoveryFee,@TotalPatientSalesPrice,@PatientDiscount,
			@PatientDiscountPercentage,@TotalPatientWithDiscount,@VoucherValue,@CrossingValue,
			@InvoiceThirdPartySalesValue,@TotalPatientWithDiscount,@IPTIPOPAC,
			@IPTIPOAFI,@CAPACIPAG,@NIVECODIGO,0,
			0,1,@userCode,Common.Getdate(),@InvoiceCategoryId,
			@OutputDate,@IsCutAccount,@OutputDiagnosis,@InitialDate,@CutType,@BillingAuthorizationIdForInvoice,
			@ContractId, @InvoiceValue, 0, @InvoiceThirdPartySalesValue
		)
		
		Declare @newInvoiceId As Int = SCOPE_IDENTITY()
		
		if Exists 
		(
			Select 1
			From Billing.ServiceOrderDetailDistribution i With(Nolock)
			Inner Join Billing.ServiceOrderDetail sod With(Nolock) On i.ServiceOrderDetailId = sod.Id
			Where i.RevenueControlDetailId = @RevenueControlDetailId And i.DistributionType = @Uno
				And (round(sod.InvoicedQuantity * sod.TotalSalesPrice, 0) <> i.GrandTotalSalesPrice 
				And ROUND(sod.InvoicedQuantity * sod.TotalSalesPrice,-2) <> i.GrandTotalSalesPrice)
		) begin			
			Select CONVERT(bit, 0) as StatusResult, 'Error de datos, no concuerda la operacion (Cantidad * ValorUnitario) = Total' as MessageResult ,NULL as CareGroupType, NULL as LiquidationType, NULL as InvoiceId, NULL as InvoiceNumber, NULL as InvoiceThirdPartySalesValue, NULL as FolioCareGroupId, NULL as FolioThirdPartyId, NULL as FolioTotalPatientWithDiscount, NULL as MessageValidationContract, NULL as InvoiceCategory
			Return
		end

		if Exists 
		(
			Select 1
			From Billing.ServiceOrderDetailDistribution i With(Nolock) 
			Inner Join Billing.ServiceOrderDetail sod With(Nolock) on sod.Id = i.ServiceOrderDetailId 
			Where i.RevenueControlDetailId = @RevenueControlDetailId 
				And sod.SettlementType = @Tres and i.GrandTotalSalesPrice > @Cero
		) begin
			select CONVERT(bit, 0) as StatusResult, 'Error de datos, El item se encuentra incluido al 100% pero el valor es mayor a 0' as MessageResult ,NULL as CareGroupType, NULL as LiquidationType, NULL as InvoiceId, NULL as InvoiceNumber, NULL as InvoiceThirdPartySalesValue, NULL as FolioCareGroupId, NULL as FolioThirdPartyId, NULL as FolioTotalPatientWithDiscount, NULL as MessageValidationContract, NULL as InvoiceCategory
			return
		end

		Update sodd
			Set GrandTotalSalesPrice = 0, 
				GrandTotalDiscount = 0, 
				ThirdPartySalesPrice = 0, 
				SubTotalPatientSalesPrice = 0
		From Billing.ServiceOrderDetailDistribution sodd
		Inner Join Billing.ServiceOrderDetail sod With(Nolock) on sod.Id = sodd.ServiceOrderDetailId
		Where sodd.RevenueControlDetailId = @RevenueControlDetailId and sod.SettlementType = @Tres

		Update sod 
			Set SubTotalSalesPrice = 0, 
				ThirdPartyDiscount = 0, 
				TotalSalesPrice = 0, 
				GrandTotalSalesPrice = 0
		From Billing.ServiceOrderDetailDistribution sodd
		Inner join Billing.ServiceOrderDetail sod With(Nolock) on sod.Id = sodd.ServiceOrderDetailId
		where sodd.RevenueControlDetailId = @RevenueControlDetailId and sod.SettlementType = @Tres
		
		--Se valida si hay detalles de la orden de servicio que manejen RIAS
		if exists
		(
			select 1
			from Billing.ServiceOrderDetailDistribution sodd With(Nolock)
			inner join Billing.ServiceOrderDetail sod With(Nolock) on sod.Id = sodd.ServiceOrderDetailId
			where sodd.RevenueControlDetailId = @RevenueControlDetailId and sod.ApplyRIAS = 1
		)
		begin
			--Se actualiza el campo IDFACTURA de la tabla RIASCUPSXPACIENTE
			update rcp set rcp.IDFACTURA = @newInvoiceId
			from Billing.ServiceOrderDetailDistribution sodd With(Nolock)
			inner join Billing.ServiceOrderDetail sod With(Nolock) on sod.Id = sodd.ServiceOrderDetailId
			inner join Contract.CUPSEntity ce With(Nolock) on ce.Id = sod.CUPSEntityId
			inner join .RIASCUPSPACIENTE rcp With(Nolock) on rcp.IPCODPACI = @PatientCode and rcp.IDRIASCUPS = sod.RIASCupsId and rcp.CODSERIPS = ce.Code and rcp.IDDETALLEORDENSERVICIO = sod.Id
			where sodd.RevenueControlDetailId = @RevenueControlDetailId and sod.ApplyRIAS = 1				
		end

		--Tabla para obtener los ids del detalle de la factura
		declare @InvoiceDetailIds table(Id int primary key)

		--====detalles de la factura
		Insert Into Billing.InvoiceDetail 
		(
			InvoiceId,ServiceOrderDetailId,GrandTotalSalesPrice,
			GrandTotalDiscount,DistributionType,ThirdPartySalesPrice,
			ThirdPartyPercentage,ApplyRecoveryFee,RecoveryFeeType,
			SubTotalPatientSalesPrice,PatientPercentage, InvoicedQuantity,
			TotalSalesPrice, ServiceDate,ThirdPartyDiscount,
			Presentation, RecordType, Balance
		) Output inserted.Id Into @InvoiceDetailIds(Id)
		Select @newInvoiceId, sodd.ServiceOrderDetailId,sodd.GrandTotalSalesPrice,
			sodd.GrandTotalDiscount,sodd.DistributionType,sodd.ThirdPartySalesPrice,
			sodd.ThirdPartyPercentage,sodd.ApplyRecoveryFee,sodd.RecoveryFeeType,
			sodd.SubTotalPatientSalesPrice,sodd.PatientPercentage, sod.InvoicedQuantity, 
			sod.TotalSalesPrice, sod.ServiceDate, sod.ThirdPartyDiscount, 
			sod.Presentation, sod.RecordType, sodd.ThirdPartySalesPrice
		From Billing.ServiceOrderDetailDistribution sodd With(Nolock)
		Inner Join Billing.ServiceOrderDetail sod With(Nolock) On sod.Id = sodd.ServiceOrderDetailId
		Where RevenueControlDetailId = @RevenueControlDetailId And sod.IsDelete = @Cero
		
		--Inserto en la tabla InvoiceDetailSurgical con los ids del detalle de la factura obtenidos anteriormente
		Insert Into Billing.InvoiceDetailSurgical
		(
			InvoiceDetailId, IPSServiceId, InvoicedQuantity, LiquidationPercentage, 
			RateManualSalePrice, TotalSalesPrice, PerformsHealthProfessionalCode,
			PerformsHealthProfessionalThirdPartyId, CostValue, BillingConceptId, 
			CostCenterId, RateManualDetailSurgicalId, SurchargeApply, 
			OnlyMedicalFees, IncomeMainAccountId, Balance
		)
		Select ind.Id, sods.IPSServiceId, sods.InvoicedQuantity, sods.LiquidationPercentage, 
			sods.RateManualSalePrice, sods.TotalSalesPrice, sods.PerformsHealthProfessionalCode, 
			sods.PerformsHealthProfessionalThirdPartyId, sods.CostValue, sods.BillingConceptId, 
			sods.CostCenterId, sods.RateManualDetailSurgicalId, sods.SurchargeApply, 
			sods.OnlyMedicalFees, sods.IncomeMainAccountId, sods.TotalSalesPrice
		From Billing.InvoiceDetail ind With(Nolock)
		Inner Join Billing.ServiceOrderDetailSurgical sods With(Nolock) On sods.ServiceOrderDetailId = ind.ServiceOrderDetailId
		Inner Join @InvoiceDetailIds idt On idt.Id = ind.Id

		--==Se valida que el valor de la factura corresponda con el de sus detalles
		IF EXISTS 
		(
			SELECT 1
			FROM Billing.Invoice i
			JOIN 
			(
				SELECT InvoiceId, SUM(ThirdPartySalesPrice) ThirdPartySalesPrice
				FROM Billing.InvoiceDetail
				GROUP BY InvoiceId
			) id ON i.Id = id.InvoiceId
			WHERE i.Id = @newInvoiceId AND i.ThirdPartySalesValue <> id.ThirdPartySalesPrice
		)
		begin
			select CONVERT(bit, 0) as StatusResult, 'Error de datos, El valor de la Factura no corresponde con el de sus Detalles (Por favor verifique los descuentos)' as MessageResult ,NULL as CareGroupType, NULL as LiquidationType, NULL as InvoiceId, NULL as InvoiceNumber, NULL as InvoiceThirdPartySalesValue, NULL as FolioCareGroupId, NULL as FolioThirdPartyId, NULL as FolioTotalPatientWithDiscount, NULL as MessageValidationContract, NULL as InvoiceCategory
			return
		end

		declare @CalculateTaxAdvance tinyint,
				@CustomerId INT

		--Parámetros de facturación
		SELECT @CalculateTaxAdvance = CalculateTaxAdvance
		FROM Billing.SettingsBilling
		WHERE IdOperatingUnit = @OperativeUnitId

		--==RETENCIONES DEL CLIENTE ASOCIADAS A LA FACTURA
		IF @FolioType <> 5 AND @CalculateTaxAdvance IN (1, 2)
		BEGIN
			Select @CustomerId = Id From Common.Customer With(Nolock) Where ThirdPartyId = @RevenueControlDetailThirdPartyId

			INSERT INTO Billing.InvoiceCustomerRetention
			(
				InvoiceId, CustomerRetentionId, CalculateTaxAdvance, RetentionType, RetentionRate, BaseValue, Value
			)
			SELECT @newInvoiceId, cr.Id, @CalculateTaxAdvance, ma.RetencionType, rc.Rate, IIF(ma.RetencionType = 2, 0, @InvoiceThirdPartySalesValue), ROUND(IIF(ma.RetencionType = 2, 0, @InvoiceThirdPartySalesValue) * rc.Rate / 100, 2)
			FROM Common.CustomerRetention cr
			JOIN Portfolio.PortfolioNoteConcept pnc ON cr.PortfolioNoteConceptId = pnc.Id
			JOIN GeneralLedger.MainAccounts ma ON pnc.IdAccount = ma.Id
			JOIN GeneralLedger.RetentionConcepts rc ON cr.RetentionConceptId = rc.Id
			WHERE cr.CustomerId = @CustomerId AND cr.Status = 1
				AND ROUND(IIF(ma.RetencionType = 2, 0, @InvoiceThirdPartySalesValue) * rc.Rate / 100, 2) <> 0
		END

		--==VALIDACIONES Y NOTIFICACIONES DEL CONTRATO
		If @CareGroupType = 1
		begin			
			--==Validaciones del contrato
			if @StatusContract = 2 or @StatusContract = 3
			begin
				select CONVERT(bit, 0) as StatusResult, 'No se puede liquidar el folio ('+ CAST(@FolioOrder AS VARCHAR(20))+') debido a que el contrato esta ('+IIF(@StatusContract=2,'Suspendido','Terminado')+')' as MessageResult ,NULL as CareGroupType, NULL as LiquidationType, NULL as InvoiceId, NULL as InvoiceNumber, NULL as InvoiceThirdPartySalesValue, NULL as FolioCareGroupId, NULL as FolioThirdPartyId, NULL as FolioTotalPatientWithDiscount, NULL as MessageValidationContract, NULL as InvoiceCategory
				return
			end

			Declare @NewExecuteValue decimal(18,0) = (@ContractExecuteValue+@TotalFolio-@TotalPatientWithDiscount)
			
			if @TerminationControl = 3
			begin
				--Terminación del contrato por valor del contrato
				if @NewExecuteValue > @ContractValue
				begin
					select CONVERT(bit, 0) as StatusResult, ('El folio '+convert(varchar(10),@FolioOrder)+' no se pueden liquidar debido a que se superaría el valor del contrato '+@ContractCodeName) as MessageResult ,NULL as CareGroupType, NULL as LiquidationType, NULL as InvoiceId, NULL as InvoiceNumber, NULL as InvoiceThirdPartySalesValue, NULL as FolioCareGroupId, NULL as FolioThirdPartyId, NULL as FolioTotalPatientWithDiscount, NULL as MessageValidationContract, NULL as InvoiceCategory
					return
				end
				if @ContractNotificationValueType=2 and @NewExecuteValue > @ContractValue*@ContractPercentageNotification/100
					set @messageValidationContract += 'Atención: queda un saldo restante de '+convert(varchar(20),(@ContractValue-@NewExecuteValue))+' para la terminación del contrato '+@ContractCodeName
				else if @ContractNotificationValueType=3 and @NewExecuteValue > @ContractNotificationValue
					set @messageValidationContract += 'Atención: queda un saldo restante de '+convert(varchar(20),(@ContractValue-@NewExecuteValue))+' para la terminación del contrato '+@ContractCodeName
			end
			else if @TerminationControl = 2
			begin
				--Terminación del contrato por Fecha del contrato
				if Common.Getdate() > @ContractEndDate
				begin
					select CONVERT(bit, 0) as StatusResult, ('El folio '+convert(varchar(10),@FolioOrder)+' no se pueden liquidar debido a ya se venció la fecha del contrato '+@ContractCodeName) as MessageResult ,NULL as CareGroupType, NULL as LiquidationType, NULL as InvoiceId, NULL as InvoiceNumber, NULL as InvoiceThirdPartySalesValue, NULL as FolioCareGroupId, NULL as FolioThirdPartyId, NULL as FolioTotalPatientWithDiscount, NULL as MessageValidationContract, NULL as InvoiceCategory
					return
				end
				if @ContractNotificationTimeType=2 and DATEADD(DAY, @ContractNotificationDays, Common.Getdate()) >= @ContractEndDate
					set @messageValidationContract += 'Atención: La fecha del contrato vencerá en '+convert(varchar(20), DATEDIFF(DD, @ContractEndDate, DATEADD(DAY, @ContractNotificationDays, Common.Getdate())))+' días'
			end
			else if @TerminationControl = 4
			begin
				--Terminación del contrato por Fecha o Valor del contrato
				if @NewExecuteValue > @ContractValue
				begin
					select CONVERT(bit, 0) as StatusResult, ('El folio '+convert(varchar(10),@FolioOrder)+' no se pueden liquidar debido a que se superaría el valor del contrato '+@ContractCodeName) as MessageResult ,NULL as CareGroupType, NULL as LiquidationType, NULL as InvoiceId, NULL as InvoiceNumber, NULL as InvoiceThirdPartySalesValue, NULL as FolioCareGroupId, NULL as FolioThirdPartyId, NULL as FolioTotalPatientWithDiscount, NULL as MessageValidationContract, NULL as InvoiceCategory
					return
				end
				if Common.Getdate() > @ContractEndDate
				begin
					select CONVERT(bit, 0) as StatusResult, ('El folio '+convert(varchar(10),@FolioOrder)+' no se pueden liquidar debido a ya se venció la fecha del contrato '+@ContractCodeName) as MessageResult ,NULL as CareGroupType, NULL as LiquidationType, NULL as InvoiceId, NULL as InvoiceNumber, NULL as InvoiceThirdPartySalesValue, NULL as FolioCareGroupId, NULL as FolioThirdPartyId, NULL as FolioTotalPatientWithDiscount, NULL as MessageValidationContract, NULL as InvoiceCategory
					return
				end
				if @ContractNotificationValueType=2 and @NewExecuteValue > @ContractValue*@ContractPercentageNotification/100
					set @messageValidationContract += 'Atención: queda un saldo restante de '+convert(varchar(20),(@ContractValue-@NewExecuteValue))+' para la terminación del contrato '+@ContractCodeName
				else if @ContractNotificationValueType=3 and @NewExecuteValue > @ContractNotificationValue
					set @messageValidationContract += 'Atención: queda un saldo restante de '+convert(varchar(20),(@ContractValue-@NewExecuteValue))+' para la terminación del contrato '+@ContractCodeName
				if @ContractNotificationTimeType=2 and DATEADD(DAY, @ContractNotificationDays, Common.Getdate()) >= @ContractEndDate
					set @messageValidationContract += 'Atención: La fecha del contrato vencerá en '+convert(varchar(20), DATEDIFF(DD, @ContractEndDate, DATEADD(DAY, @ContractNotificationDays, Common.Getdate())))+' días'
			end
			--===END validaciones contrato
			update [Contract].[Contract] Set ExecuteValue = @NewExecuteValue Where Id = @ContractId
		end
		
		--================FIN GENERACION FACTURA==================
		
		--==Actualización de las banderas de Invoice en las tablas de ADCONCOEX, AMBORDIMAG, AMBORDLAB, AMBORDPAT (Citas)
		declare @ControlExternalConsultation tinyint
		declare @ControlExternalConsultationCode numeric(18,0)
		declare @Cantidad int
		declare @tmpconsultation table(Id int identity(1,1),ControlExternalConsultation tinyint,ControlExternalConsultationCode numeric(18,0))
			
		Insert Into @tmpconsultation (ControlExternalConsultation,ControlExternalConsultationCode)
			Select sod.ControlExternalConsultation, sod.ControlExternalConsultationCode 
			From Billing.ServiceOrderDetailDistribution sodd With(Nolock)
			Inner Join Billing.ServiceOrderDetail sod With(Nolock) on sodd.ServiceOrderDetailId = sod.Id 
			Where sod.ControlExternalConsultation is not null 
				And sod.ControlExternalConsultation > @Cero 
				And sodd.RevenueControlDetailId = @RevenueControlDetailId
			Group By sod.ControlExternalConsultation, sod.ControlExternalConsultationCode
			
		select @cantidad = count(*) from @tmpconsultation
		declare @contador int = 1
		while @contador <= @cantidad
		begin
			select @ControlExternalConsultation=ControlExternalConsultation,@ControlExternalConsultationCode=ControlExternalConsultationCode from @tmpconsultation where Id = @contador
				
			if @ControlExternalConsultation = 1 --Laboratorios
				set @sqlCrystal = 'update ['+@ContainerNameCrystal+'].[dbo].[AMBORDLAB] set GENINVOICE=@NumInvoice,GENINVOICEID=@newInvoiceId where AUTO = '+CONVERT(varchar(20), @ControlExternalConsultationCode);
			else if @ControlExternalConsultation = 2 --Patologias
				set @sqlCrystal = 'update ['+@ContainerNameCrystal+'].[dbo].[AMBORDPAT] set GENINVOICE=@NumInvoice,GENINVOICEID=@newInvoiceId where AUTO = '+CONVERT(varchar(20), @ControlExternalConsultationCode);
			else if @ControlExternalConsultation = 3 --Imagenes
				set @sqlCrystal = 'update ['+@ContainerNameCrystal+'].[dbo].[AMBORDIMA] set GENINVOICE=@NumInvoice,GENINVOICEID=@newInvoiceId where AUTO = '+CONVERT(varchar(20), @ControlExternalConsultationCode);
			else if @ControlExternalConsultation = 8 --Consulta Externa
				set @sqlCrystal = 'update ['+@ContainerNameCrystal+'].[dbo].[ADCONCOEX] set GENINVOICE=@NumInvoice,GENINVOICEID=@newInvoiceId where CODCONCEC = '+CONVERT(varchar(20), @ControlExternalConsultationCode);
				
			if @ControlExternalConsultation = 1 or @ControlExternalConsultation = 2 or @ControlExternalConsultation = 3 or @ControlExternalConsultation = 8
			begin
				--print convert(varchar(20), @ControlExternalConsultation)
				exec sp_executesql @sqlCrystal, N'@NumInvoice varchar(20),@newInvoiceId int',@InvoiceNumber, @newInvoiceId
			end
			set @contador += 1
		end
		
		declare @IdInpacientTopAnu int
		declare @PAGADOCMO numeric(18,0)
		declare @PAGADOCOP numeric(18,0)
		declare @PAGADOCRE numeric(18,0)
		declare @PreviusPAGADOCMO numeric(18,0)
		declare @PreviusPAGADOCOP numeric(18,0)
		declare @PreviusPAGADOCRE numeric(18,0)

		set @sqlCrystal = 'select @IdInpacientTopAnu=Id,@PreviusPAGADOCMO=PAGADOCMO,@PreviusPAGADOCOP=PAGADOCOP,@PreviusPAGADOCRE=PAGADOCRE from '+@ContainerNameCrystal+'.dbo.INPACIENTTOPANU where IPCODPACI = '''+@PatientCode+''' and ANIO = YEAR(Common.Getdate())';
		exec sp_executesql @sqlCrystal, N'@IdInpacientTopAnu int output,@PreviusPAGADOCMO numeric(18,0) output,@PreviusPAGADOCOP numeric(18,0) output,@PreviusPAGADOCRE numeric(18,0) output', @IdInpacientTopAnu output,@PreviusPAGADOCMO output,@PreviusPAGADOCOP output,@PreviusPAGADOCRE output
		--print @sqlCrystal

		Select @PAGADOCMO=COALESCE(sum(SubTotalPatientSalesPrice), 0) 
		From Billing.ServiceOrderDetailDistribution With(Nolock)
		Where RevenueControlDetailId = @RevenueControlDetailId and ApplyRecoveryFee = @Dos and RecoveryFeeType = @Dos

		Select @PAGADOCOP=COALESCE(sum(SubTotalPatientSalesPrice), 0) 
		From Billing.ServiceOrderDetailDistribution With(Nolock) 
		Where RevenueControlDetailId = @RevenueControlDetailId and ApplyRecoveryFee = @Dos and RecoveryFeeType = @Tres

		Select @PAGADOCRE=COALESCE(sum(SubTotalPatientSalesPrice), 0 ) 
		From Billing.ServiceOrderDetailDistribution With(Nolock) 
		Where RevenueControlDetailId = @RevenueControlDetailId and ApplyRecoveryFee = @Dos and RecoveryFeeType = @Cinco
		
		if @IdInpacientTopAnu > 0
		begin
			set @sqlCrystal = 'update '+@ContainerNameCrystal+'.dbo.INPACIENTTOPANU set PAGADOCMO = '+CONVERT(varchar(20), (@PreviusPAGADOCMO+@PAGADOCMO))+', PAGADOCOP = '+CONVERT(varchar(20), (@PreviusPAGADOCOP+@PAGADOCOP))+', PAGADOCRE = '+CONVERT(varchar(20), (@PreviusPAGADOCRE+@PAGADOCRE))+' where Id = '+CONVERT(varchar(20), @IdInpacientTopAnu)
		end
		else
		begin
			set @sqlCrystal = 'insert into '+@ContainerNameCrystal+'.dbo.INPACIENTTOPANU (ANIO,IPCODPACI,PAGADOCMO, PAGADOCOP, PAGADOCRE) values (YEAR(Common.Getdate()),'''+@PatientCode+''','+CONVERT(varchar(20), @PAGADOCMO)+', '+CONVERT(varchar(20), @PAGADOCOP)+', '+CONVERT(varchar(20), @PAGADOCRE)+')'
		end
		exec sp_executesql @sqlCrystal
		
		--==Actualizando datos del folio : estado, autorización, campos de descuento a paciente
		
		Update Billing.RevenueControlDetail 
			Set PatientDiscount = @PatientDiscount,
				PatientDiscountPercentage = @PatientDiscountPercentage,
				TotalFolio = @TotalFolio,
				TotalPatientWithDiscount = @TotalPatientWithDiscount,
				BillingAuthorizationId = @BillingAuthorizationId,
				[Status] = 2,
				OutputDate = @OutputDate,
				IsCutAccount = @IsCutAccount,
				OutputDiagnosis = @OutputDiagnosis 
		Where Id = @RevenueControlDetailId

		--===CERRAR INGRESO==============
		if @CloseAdmission = 0
		begin
			declare @EstadoIngreso char(1)
			set @sqlCrystal = 'select @EstadoIngreso=IESTADOIN from ['+@ContainerNameCrystal+'].[dbo].[ADINGRESO] where NUMINGRES = '''+@AdmissionNumber+''''
			exec sp_executesql @sqlCrystal, N'@EstadoIngreso char(1) output', @EstadoIngreso output

			if @EstadoIngreso = ' '
			begin
				set @sqlCrystal = 'update ['+@ContainerNameCrystal+'].[dbo].[ADINGRESO] set IESTADOIN = ''P'' where NUMINGRES = '''+@AdmissionNumber+''''
				exec sp_executesql @sqlCrystal
			end			
		end
		else if @CloseAdmission = 1
		begin
			DECLARE @StatusResult BIT,
					@MessageResult VARCHAR(MAX)

			EXEC [Billing].[SP_CloseAdmission_Output] 
				@AdmissionNumber, 
				@ContainerNameCrystal, 
				--------------------------------------------
				@StatusResult OUTPUT, 
				@MessageResult OUTPUT

			IF ISNULL(@StatusResult, 0) = 0
			BEGIN
				select CONVERT(bit, 0) as StatusResult, ISNULL(@MessageResult, 'Error al cerrar ingreso') as MessageResult ,NULL as CareGroupType, NULL as LiquidationType, NULL as InvoiceId, NULL as InvoiceNumber, NULL as InvoiceThirdPartySalesValue, NULL as FolioCareGroupId, NULL as FolioThirdPartyId, NULL as FolioTotalPatientWithDiscount, NULL as MessageValidationContract, NULL as InvoiceCategory
				return
			END
		end
		--===============================

		select CONVERT(bit, 1) as StatusResult, 'OK' as MessageResult ,@CareGroupType as CareGroupType, @LiquidationType as LiquidationType, @newInvoiceId as InvoiceId, 
		@InvoiceNumber as InvoiceNumber, @InvoiceThirdPartySalesValue as InvoiceThirdPartySalesValue, @careGroupId as FolioCareGroupId, @RevenueControlDetailThirdPartyId as FolioThirdPartyId,
		@TotalPatientWithDiscount as FolioTotalPatientWithDiscount,@messageValidationContract as MessageValidationContract, @InvoiceCategoryId as InvoiceCategory
	END TRY
	BEGIN CATCH
		select CONVERT(bit, 0) as StatusResult, 'Se ha producido un error!'+ ERROR_MESSAGE() as MessageResult ,NULL as CareGroupType, NULL as LiquidationType, NULL as InvoiceId, 
		NULL as InvoiceNumber, NULL as InvoiceThirdPartySalesValue, NULL as FolioCareGroupId, NULL as FolioThirdPartyId, NULL as FolioTotalPatientWithDiscount, NULL as MessageValidationContract, NULL as InvoiceCategory
	END CATCH	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera la factura (cuenta por cobrar) a partir de un folio de liquidación previamente calculado, orquestando todo el proceso de facturación hospitalaria y ambulatoria. Primero recalcula los valores del folio llamando a SP_UpdateRevenueControlDetailValuesNoSelect, luego valida el contrato con la entidad pagadora (EPS/aseguradora) a través de CareGroup y Contract, y finalmente crea la factura con su consecutivo/prefijo, distribuyendo los valores entre el tercero responsable (asegurador) y el paciente (copagos, cuotas moderadoras, descuentos). Utiliza SQL dinámico en tiempo de ejecución para construir los documentos financieros (cuentas por cobrar de entidad, paciente, pagaré y traslados) según el tipo de liquidación, grupo de atención y tipo de folio, tocando las entidades de folio de facturación, detalle de órdenes de servicio, distribución financiera por ítem, catálogo de servicios IPS e inventario de productos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CreateInvoice';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CreateInvoice';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_UpdateRevenueControlDetailValuesNoSelect; Billing.SP_CloseAdmission_Output', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControlDetail; Contract.CareGroup; Contract.Contract; Contract.ContractDetail; Billing.BillingAuthorization; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; Contract.IPSService; Inventory.InventoryProduct; Billing.Invoice; Billing.SettingsBilling; Billing.InvoiceDetail; Billing.ServiceOrderDetailSurgical; Contract.CUPSEntity; Common.Customer; Common.CustomerRetention; Portfolio.PortfolioNoteConcept; GeneralLedger.MainAccounts; GeneralLedger.RetentionConcepts', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateInvoice';
-- GO
