

-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 16/02/2016
-- Description:	Procedimiento que se encarga de confirmar la liquidación de honorarios médicos
-- =============================================
CREATE PROCEDURE [MedicalFees].[SP_ConfirmMedicalFeesLiquidation] 
	@MedicalFeesLiquidationXml as Xml,
	@CodeUser as varchar(20)
AS
BEGIN

	--Se declaran las variables para obtener la cabecera
	declare @Id int, @Code varchar(20), @OperatingUnitId int, @LiquidationType tinyint, @MedicalFeesContractId int,
	 @HealthProfessionalCode char(20), @SupplierId int, @SuppliersDistributionLineId int, @CostCenterId int, @InitialDate datetime, 
	 @EndDate datetime, @BillNumber varchar(20), @DocumentDate datetime, @FilingUnitId int, @SupplierTypeId int, @AccountPayableId int, @Status tinyint
	
	--Tabla temporal para obtener los detalles de MedicalFeesLiquidation
	declare @MedicalFeesLiquidationDetail table(Id int, LiquidationType tinyint, MedicalFeesLiquidacionId int, MedicalFeesCausationId int, ChangeTracker int, ChangeStatusMedicalFeesCausation bit)

	Begin try
	
		--Se obtiene la cabecera del xml(MedicalFeesLiquidation)
		select 
		@Id = t.x.value('Id[1]','int'),
		@Code = t.x.value('Code[1]','varchar(20)'),
		@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
		@LiquidationType = t.x.value('LiquidationType[1]','tinyint'),
		@MedicalFeesContractId = case t.x.value('MedicalFeesContractId[1]','int') when 0 then null else t.x.value('MedicalFeesContractId[1]','int') end,
		@HealthProfessionalCode = case t.x.value('HealthProfessionalCode[1]','char(20)') when '0' then null else t.x.value('HealthProfessionalCode[1]','char(20)') end,
		@SupplierId = t.x.value('SupplierId[1]','int'),
		@SuppliersDistributionLineId = t.x.value('SuppliersDistributionLineId[1]','int'),
		@CostCenterId = case t.x.value('CostCenterId[1]','int') when 0 then null else t.x.value('CostCenterId[1]','int') end,
		@InitialDate = convert(datetime, t.x.value('InitialDate[1]','varchar(20)'), 103),
		@EndDate = convert(datetime, t.x.value('EndDate[1]','varchar(20)'), 103),
		@BillNumber = t.x.value('BillNumber[1]','varchar(20)'),		
		@DocumentDate = convert(datetime, t.x.value('DocumentDate[1]','varchar(20)'), 103),
		@FilingUnitId = t.x.value('FilingUnitId[1]','int'),
		@SupplierTypeId = t.x.value('SupplierTypeId[1]','int'),
		@AccountPayableId = case t.x.value('AccountPayableId[1]','int') when 0 then null else t.x.value('AccountPayableId[1]','int') end,
		@Status = t.x.value('Status[1]','tinyint')
		from @MedicalFeesLiquidationXml.nodes('/MedicalFeesLiquidation') t(x)
		
		--Se obtiene los detalles del xml(MedicalFeesLiquidationDeatil)
		insert into @MedicalFeesLiquidationDetail
		select 
		t.x.value('Id[1]','int') as Id,
		case when t.x.value('LiquidationType[1]','tinyint') = 0 then null else t.x.value('LiquidationType[1]','tinyint') end as LiquidationType,
		t.x.value('MedicalFeesLiquidacionId[1]','int') as MedicalFeesLiquidacionId,
		t.x.value('MedicalFeesCausationId[1]','int') as MedicalFeesCausationId,
		t.x.value('ChangeTracker[1]','int') as ChangeTracker,
		t.x.value('ChangeStatusMedicalFeesCausation[1]','bit') as ChangeStatusMedicalFeesCausation
		from @MedicalFeesLiquidationXml.nodes('/MedicalFeesLiquidation/MedicalFeesLiquidationDetail') t(x)

		--Código de la CxP
		declare @CodeAccountPayable varchar(20)

		-- Consultamos la secuencia numerica del form de CxP
		declare @idSequenceDetail int
		declare @pattern varchar(300)
		declare @NextS int
		select @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  from Payments.PaymentsSecuenceDetail bsd inner join Payments.PaymentsSecuence bs on bs.Id = bsd.IdSequensePaymentsC inner join Common.Sequense cs on cs.Id = bsd.IdSequense
		where bs.IdForm = '730'
		if (@idSequenceDetail is null)
		Begin
			select 999 as CodeMessage, 'Secuencia no encontrada para generar la cuenta por pagar'  as Message, '' as CodeAccountPayable, 0 as AccountPayableId
			return
		End
		select @CodeAccountPayable = dbo.GetSequence('',@pattern,@NextS)
		update Payments.PaymentsSecuenceDetail set [Next] = [Next] + 1 where Id = @idSequenceDetail

		--Valido que el mes este abrierto
		if(select count(*) from [GeneralLedger].[ClosedMonth] where [Year] = Year(@DocumentDate) and [Month] = Month(@DocumentDate) and Status = 1) = 0 --- Si el mes no esta abierto
		Begin
			select 999 as CodeMessage, 'El mes ' + cast(MONTH(@DocumentDate) as varchar(2)) + ' no se encuentra abierto' as Message, '' as CodeAccountPayable, 0 as AccountPayableId
			return
		End

		--Se valida que haya parámetros de empresa para poder sacar el valor del consecutivo del radicado
		if (select count(*) from [GeneralLedger].CompanySettings) = 0
		Begin
			select 999 as CodeMessage, 'No existe parámetros de empresa para obtener el consecutivo de radicación' as Message, '' as CodeAccountPayable, 0 as AccountPayableId
			return
		End

		--Se obtiene el consecutivo del radicado
		declare @ConsecutiveRadicate bigint
		select @ConsecutiveRadicate = ConsecutiveFiling from [GeneralLedger].CompanySettings
		--Se actualiza el consecutivo del radicado aumentandole uno
		update GeneralLedger.CompanySettings set ConsecutiveFiling = ConsecutiveFiling + 1

		--Se consulta si el proveedor ya tiene asociado el No. de factura
		if (select COUNT(*) from Payments.AccountPayable where BillNumber = @BillNumber and IdSupplier = @SupplierId) > 0
		Begin
			declare @CodeSupplier varchar(100), @NameSupplier varchar(100)
			select @CodeSupplier = Code, @NameSupplier = Name from Common.Supplier where Id = @SupplierId
			select 999 as CodeMessage, 'El proveedor ' + @CodeSupplier + ' - ' + @NameSupplier + ' ya tiene asociado el número de factura ' + @BillNumber as Message, '' as CodeAccountPayable, 0 as AccountPayableId
			return
		End

		IF (SELECT COUNT(1) FROM Payments.AccountPayable WHERE CODE = @CodeAccountPayable) > 0
		BEGIN
		select 999 as CodeMessage, 'Ya existe una cuenta por pagar con el código ' + @CodeAccountPayable as Message, '' as CodeAccountPayable, 0 as AccountPayableId
			return
		END

		--Se valida si existe parametros de honorarios medicos para poder sacar los conceptos
		if (select COUNT(*) from MedicalFees.SettingMedicalFees) = 0
		Begin
			select 999 as CodeMessage, 'No existe parámetros de honorarios médicos' as Message, '' as CodeAccountPayable, 0 as AccountPayableId
			return
		End

		--Se declaran las variables de la cabecera de la CxP
		declare @IdSupplier int
		declare @IdThirdParty int
		declare @IdAccount int
		declare @IdSuppliersDistributionLines int
		declare @Term int

		declare @PivotSupplierId int
		declare @PivotSupplierDistributionLineId int 
		if @MedicalFeesContractId is null --Estandar
		Begin
			--Se consulta el médico con el código ingresado en el form
			if (select COUNT(*) from dbo.INPROFSAL where CODPROSAL = @HealthProfessionalCode) = 0 --Si no existe el médico
			Begin
				select 999 as CodeMessage, 'El código ' + @HealthProfessionalCode + ' no existe como médico' as Message, '' as CodeAccountPayable, 0 as AccountPayableId
				return
			End
			
			--Se consulta el médico que viene desde liquidación de honorarios
			select @PivotSupplierId = GENPROVEE, @PivotSupplierDistributionLineId = GENLINDIST from dbo.INPROFSAL where CODPROSAL = @HealthProfessionalCode

		End
		Else --Agremiaciones
		Begin
			--Se consulta el contrato que viene desde liquidación de honorarios
			select @PivotSupplierId = SupplierId, @PivotSupplierDistributionLineId = SupplierDistributionLineId from MedicalFees.MedicalFeesContract where Id = @MedicalFeesContractId
		End

		--Se consulta el proveedor con el id que se obtuvo anteriormente
		select @IdSupplier = Id, @IdThirdParty = IdThirdParty, @Term = TimeLimitDays from Common.Supplier where Id = @PivotSupplierId

		--- Determina si  puede o no manejar documento soporte
		DECLARE @HandlesDocumentSupport BIT = (SELECT HandlesSupportDocument 
												 FROM GeneralLedger.GeneralLedgerSettings 
												 WHERE IdOperatingUnit = @OperatingUnitId)

		IF @HandlesDocumentSupport = 1
		BEGIN
		-- Si el tercero es facturador electronico, no genera documento soporte
			SELECT @HandlesDocumentSupport = 
				IIF(tp.ElectronicBiller = 1, 0, 1)
			FROM Common.Supplier S
			INNER JOIN Common.ThirdParty tp ON tp.Id = S.IdThirdParty
			WHERE S.Id = @IdSupplier
		END

		--Se consulta la linea de distribucion que se obtuvo anteriormente
		select @IdAccount = ma.Id, @IdSuppliersDistributionLines = sdl.Id 
		from Common.SuppliersDistributionLines sdl
		inner join Common.DistributionLines dl on dl.Id = sdl.IdDistributionLine
		inner join GeneralLedger.MainAccounts ma on ma.Id = dl.IdMainAccount
		where sdl.Id = @PivotSupplierDistributionLineId

		declare @ValuePayments decimal(18,0) = 0
		declare @ValueDeductions decimal(18,0) = 0
		
		select @ValuePayments = SUM(mfc.TotalAmountPayable)
		from @MedicalFeesLiquidationDetail mfld
		inner join MedicalFees.MedicalFeesCausation mfc on mfc.Id = mfld.MedicalFeesCausationId
		where mfld.LiquidationType = 1 and mfld.ChangeTracker <> 3

		if (select COUNT(*) from @MedicalFeesLiquidationDetail where (LiquidationType = 2 or LiquidationType = 3) and ChangeTracker <> 3) > 0
		Begin
			select @ValueDeductions = SUM(mfc.TotalAmountPayable)
		from @MedicalFeesLiquidationDetail mfld
		inner join MedicalFees.MedicalFeesCausation mfc on mfc.Id = mfld.MedicalFeesCausationId
		where (mfld.LiquidationType = 2 or mfld.LiquidationType = 3) and mfld.ChangeTracker <> 3
		End

		declare @ValueTotalLiquidation decimal(18,0) = @ValuePayments - @ValueDeductions

		--Se valida que el valor total de liquidacion no sea menor a cero
		if @ValueTotalLiquidation < 0
		Begin
			select 999 as CodeMessage, 'El valor de la cuenta por pagar no puede ser menor a cero' as Message, '' as CodeAccountPayable, 0 as AccountPayableId
			return
		End

		--Guardo la cabecera de la CxP
		INSERT INTO 
		[Payments].[AccountPayable] ([Code],[NumberFiling],[EntityId],[EntityCode],[EntityName],[IdSupplier],[IdThirdParty],[IdAccount],
		[IdCostCenter],[BillNumber],[BillDate],[DocumentDate],[ServicePeriodDate],[FilingUnitId],[SupplierTypeId],[Term],[ExpirationDate],[Coments],[Status],
		[InitialBalance],[IdInitialBalance],[PreviousBudget],[Shares],[InvoiceValue],[Value],[Balance],[IdOperatingUnit],[IdSuppliersDistributionLines],
		[CreationUser],[CreationDate],[HandlesDocumentSupport])
		VALUES 
		(@CodeAccountPayable,@ConsecutiveRadicate,@Id,@Code,'MedicalFeesLiquidation',@IdSupplier,@IdThirdParty,@IdAccount,@CostCenterId,@BillNumber,
		@DocumentDate,@DocumentDate,@DocumentDate,@FilingUnitId,@SupplierTypeId,@Term,DATEADD(day,@Term,@DocumentDate),
		'Cuenta por pagar generada desde liquidación de honorarios médicos',1,0,null,0,1,@ValueTotalLiquidation,@ValueTotalLiquidation,@ValueTotalLiquidation
		,@OperatingUnitId,@IdSuppliersDistributionLines,@CodeUser,Common.GETDATE(), @HandlesDocumentSupport)

		--Se obtiene el id de la cabecera de la CxP
		set @AccountPayableId = SCOPE_IDENTITY()

		--Se guarda una cuota
		INSERT INTO [Payments].[AccountPayableShares] ([IdAccountPayable],[Share],[DateExpires],[InitialValue],[Balance])
			VALUES (@AccountPayableId,1,DATEADD(day,@Term,@DocumentDate),@ValueTotalLiquidation,@ValueTotalLiquidation)
				
		--Se obtiene el concepto de pago
		DECLARE @AccountPayableConceptId INT = (SELECT AccountPayableConceptId FROM MedicalFees.SettingMedicalFees)

		--Tabla temporal para guardar los conceptos de la CxP 
		DECLARE @AccountPayableDetailConcept TABLE( 
				[IdAccountPayable] [int] NOT NULL,
				[IdConceptAccountPayable] [int] NOT NULL,
				[IdAccount] [int] NOT NULL,
				[IdThirdParty] [int] NOT NULL,
				[IdCostCenter] [int] NULL,
				[Nature] [tinyint] NOT NULL,
				[BaseValue] [numeric](20, 4) NOT NULL,
				[BillingValue] [numeric](20, 4) NULL,
				[Value] [numeric](20, 4) NOT NULL,
				[IdRetentionConcept] [int] NULL,
				[Percentage] [decimal](18, 3) NULL,
				[Detail] [varchar](500) NULL,
				[DeferredCausation] [bit] NULL
			)

		--Realizo un cursor para recorrer los distintos conceptos de facturación parametrizados para la liquidación de honorarios médicos
		DECLARE @BillingConceptId INT
		DECLARE @FunctionalUnitId INT
		declare @MedicalFeesCausationId int
		DECLARE @errors VARCHAR(MAX) = ''

		DECLARE billingConceptsBymedicalFeesLiquidation_cursor CURSOR FOR 
			SELECT DISTINCT ce.BillingConceptId, sod.PerformsFunctionalUnitId, mfc.Id
				FROM @MedicalFeesLiquidationDetail mfld
				INNER JOIN MedicalFees.MedicalFeesCausation mfc ON mfc.Id = mfld.MedicalFeesCausationId
				INNER JOIN Billing.ServiceOrderDetail sod ON sod.Id = mfc.ServiceOrderDetailId
				INNER JOIN Contract.CUPSEntity ce ON ce.Id = sod.CUPSEntityId

		OPEN billingConceptsBymedicalFeesLiquidation_cursor

			FETCH NEXT FROM billingConceptsBymedicalFeesLiquidation_cursor 
				INTO @BillingConceptId, @FunctionalUnitId, @MedicalFeesCausationId

			WHILE @@FETCH_STATUS = 0
			BEGIN								
				DECLARE @ConceptType INT
				declare @ContabilizationType tinyint
				DECLARE @ConceptCode VARCHAR(20)
				DECLARE @FeesExpensesAccountId INT				

				--Obtengo la cuenta de gastos de honorarios parametrizada en el concepto de facturación
				SELECT @ConceptCode = bc.Code, @ConceptType = bc.ConceptType, @FeesExpensesAccountId = bc.FeesExpensesAccountId, @ContabilizationType = bc.AccountingType
					FROM Billing.BillingConcept bc 
					WHERE bc.Id = @BillingConceptId

				IF @ConceptType = 2 and @ContabilizationType = 2 BEGIN
					--Si el concepto de facturacion es definido por tipo de unidad, obtengo la cuenta de gastos de honorarios del detalle
					DECLARE @UnitType INT = (SELECT fu.UnitType FROM Payroll.FunctionalUnit fu WHERE fu.Id = @FunctionalUnitId)

					SET @FeesExpensesAccountId = ISNULL((SELECT  bca.FeesExpensesAccountId 
							FROM Billing.BillingConceptAccount bca 
							WHERE bca.BillingConceptId = @BillingConceptId 
								AND bca.UnitType = Billing.fnGetUnitType(@UnitType)), 0)
				END

				--Se valida que la cuenta de gasto este parametrizada en el concepto
				IF NOT(ISNULL(@FeesExpensesAccountId, 0) > 0)
				BEGIN
					SET @errors = @errors + 'El concepto de facturación "' + @ConceptCode + '" no tiene parametrizada la cuenta de gastos de Honorarios' + ' con la causaciòn ' + CAST(@MedicalFeesCausationId as varchar)
					GOTO FIN_billingConceptsBymedicalFeesLiquidation_CURSOR
				END
				
				--Se guarda los conceptos de la CxP en la tabla temporal
				INSERT INTO @AccountPayableDetailConcept
						( [IdAccountPayable], [IdConceptAccountPayable], [IdAccount], [IdThirdParty], [IdCostCenter], [Nature], [BaseValue], [BillingValue], [Value], [IdRetentionConcept], [Percentage], [Detail], [DeferredCausation] )
					SELECT x.AccountPayableId, x.AccountPayableConceptId, x.MainAccountId, x.ThirdPartyId, x.CostCenterId, x.Nature, SUM(x.BaseValue), SUM(x.BillingValue), SUM(x.Value), x.RetentionId, x.Percentage, x.Detail, x.DeferredCausation
						FROM 
						(
							SELECT 
								@AccountPayableId AS AccountPayableId, @AccountPayableConceptId AS AccountPayableConceptId, 
								ma.Id AS MainAccountId, @IdThirdParty AS ThirdPartyId, 
								CASE ma.HandlesCostCenter WHEN 0 THEN NULL ELSE sod.CostCenterId END AS CostCenterId,
								CASE mfld.LiquidationType WHEN 1 THEN 1 ELSE 2 END AS Nature, 
								mfc.TotalAmountPayable AS BaseValue, mfc.TotalAmountPayable AS BillingValue, mfc.TotalAmountPayable AS Value, 
								NULL AS RetentionId, NULL AS Percentage, 
								'Detalle de cuenta por pagar generada desde liquidación de honorarios médicos' AS Detail, 0 AS DeferredCausation
							FROM @MedicalFeesLiquidationDetail mfld
							INNER JOIN MedicalFees.MedicalFeesCausation mfc ON mfc.Id = mfld.MedicalFeesCausationId
							INNER JOIN Billing.ServiceOrderDetail sod ON sod.Id = mfc.ServiceOrderDetailId
							INNER JOIN Contract.CUPSEntity ce ON ce.Id = sod.CUPSEntityId
							INNER JOIN Billing.BillingConcept bc ON bc.Id = ce.BillingConceptId
							INNER JOIN GeneralLedger.MainAccounts ma ON ma.Id = @FeesExpensesAccountId
							WHERE ce.BillingConceptId = @BillingConceptId 
								AND sod.PerformsFunctionalUnitId = @FunctionalUnitId
								And mfc.Id = @MedicalFeesCausationId
								AND mfld.ChangeTracker <> 3
						) x 
						GROUP BY x.AccountPayableId, x.AccountPayableConceptId, x.MainAccountId, x.ThirdPartyId, x.CostCenterId, x.Nature, x.RetentionId, x.Percentage, x.Detail, x.DeferredCausation
						
				FIN_billingConceptsBymedicalFeesLiquidation_CURSOR:
				FETCH NEXT FROM billingConceptsBymedicalFeesLiquidation_cursor 
					INTO @BillingConceptId, @FunctionalUnitId, @MedicalFeesCausationId
			END 
		CLOSE billingConceptsBymedicalFeesLiquidation_cursor
		DEALLOCATE billingConceptsBymedicalFeesLiquidation_cursor

		--Si encuentra errores no puede continuar y retorna el error
		if @errors <> ''
		Begin
			select 999 AS CodeMessage, @errors AS Message, '' AS CodeAccountPayable, 0 AS AccountPayableId
			return
		End

		--select * from @AccountPayableDetailConcept

		--SELECT x.IdAccountPayable, x.IdConceptAccountPayable, x.IdAccount, x.IdThirdParty, x.IdCostCenter, x.Nature, SUM(x.BaseValue), SUM(x.BillingValue), SUM(x.Value), x.IdRetentionConcept, x.Percentage, x.Detail, x.DeferredCausation
		--		FROM @AccountPayableDetailConcept x 
		--		GROUP BY x.IdAccountPayable, x.IdConceptAccountPayable, x.IdAccount, x.IdThirdParty, x.IdCostCenter, x.Nature, x.IdRetentionConcept, x.Percentage, x.Detail, x.DeferredCausation

		--Se guarda los conceptos de la CxP totalizados
		INSERT INTO [Payments].[AccountPayableDetailConcept] 
				( [IdAccountPayable], [IdConceptAccountPayable], [IdAccount], [IdThirdParty], [IdCostCenter], [Nature], [BaseValue], [BillingValue], [Value], [IdRetentionConcept], [Percentage], [Detail], [DeferredCausation] )
			SELECT x.IdAccountPayable, x.IdConceptAccountPayable, x.IdAccount, x.IdThirdParty, x.IdCostCenter, x.Nature, SUM(x.BaseValue), SUM(x.BillingValue), SUM(x.Value), x.IdRetentionConcept, x.Percentage, x.Detail, x.DeferredCausation
				FROM @AccountPayableDetailConcept x 
				GROUP BY x.IdAccountPayable, x.IdConceptAccountPayable, x.IdAccount, x.IdThirdParty, x.IdCostCenter, x.Nature, x.IdRetentionConcept, x.Percentage, x.Detail, x.DeferredCausation
		
		select 0 as CodeMessage, 'Se confirmó correctamente' as Message, @CodeAccountPayable as CodeAccountPayable, @AccountPayableId as AccountPayableId		
	end try
	begin catch
		select 999 as CodeMessage, ERROR_MESSAGE() as Message, '' as CodeAccountPayable, 0 as AccountPayableId
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma y registra la liquidación de honorarios médicos a partir de un XML con la cabecera y el detalle de la liquidación. Genera automáticamente el código de la cuenta por pagar (CxP) usando la secuencia numérica configurada para el formulario de pagos, valida que el mes contable esté abierto en contabilidad general y obtiene el consecutivo de radicación desde los parámetros de empresa. Verifica que el proveedor no tenga duplicado el número de factura antes de crear el comprobante, integrando así los módulos de honorarios médicos, cuentas por pagar, secuencias de pago y contabilidad general.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmMedicalFeesLiquidation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmMedicalFeesLiquidation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma la liquidación de honorarios médicos generando la cuenta por pagar correspondiente con su cuota y conceptos contables a partir de las causaciones liquidadas.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMedicalFeesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir secuencia configurada en Payments.PaymentsSecuence/PaymentsSecuenceDetail para el formulario IdForm=''730''; El mes/año de DocumentDate debe estar abierto en GeneralLedger.ClosedMonth con Status=1; Debe existir al menos un registro en GeneralLedger.CompanySettings para obtener ConsecutiveFiling; El proveedor no debe tener ya registrada otra cuenta por pagar con el mismo BillNumber; No debe existir otra AccountPayable con el código generado; Debe existir al menos un registro en MedicalFees.SettingMedicalFees; Si no se trata de agremiación (sin MedicalFeesContractId), el HealthProfessionalCode debe existir en dbo.INPROFSAL; Cada concepto de facturación involucrado debe tener parametrizada la cuenta de gastos de honorarios (FeesExpensesAccountId)', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMedicalFeesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El código de la CxP siempre se genera a partir del patrón de secuencia configurado para el formulario 730; La CxP siempre se crea con Status=1, una sola cuota, e InvoiceValue=Value=Balance=ValueTotalLiquidation; El NumberFiling de radicación se asigna desde CompanySettings.ConsecutiveFiling y dicho consecutivo siempre se incrementa; Solo se consideran detalles cuyo ChangeTracker<>3 (no eliminados) tanto para totales como para conceptos; Nunca se permite duplicar BillNumber para el mismo proveedor en Payments.AccountPayable; Cualquier excepción capturada retorna CodeMessage=999 sin propagar la transacción', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMedicalFeesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Payments.PaymentsSecuenceDetail: Tras obtener el código de la CxP vía dbo.GetSequence, se incrementa Next en 1 para el detalle de secuencia del formulario 730; [UPDATE] GeneralLedger.CompanySettings: Tras leer ConsecutiveFiling para usarlo como NumberFiling, se incrementa ConsecutiveFiling en 1; [INSERT] Payments.AccountPayable: Cuando todas las validaciones pasan y ValueTotalLiquidation>=0, se inserta la cabecera de la CxP con EntityCode=código de liquidación, EntityName=''MedicalFeesLiquidation'', Status=1, Value=Balance=InvoiceValue=ValueTotalLiquidation y ExpirationDate=DocumentDate+Term; [INSERT] Payments.AccountPayableShares: Por cada CxP creada se inserta una única cuota (Share=1) con DateExpires=DocumentDate+Term y valor igual a ValueTotalLiquidation; [INSERT] Payments.AccountPayableDetailConcept: Por cada concepto de facturación distinto en los detalles (ChangeTracker<>3) se inserta un detalle agrupado con la cuenta de gasto de honorarios; Nature=1 si LiquidationType=1, sino Nature=2; CostCenter solo si la cuenta lo maneja; [RETURN_RESULT] Payments.AccountPayable: Devuelve CodeMessage=0 y el código y Id de la CxP creada al confirmar exitosamente; CodeMessage=999 con mensaje específico ante cualquier validación fallida o error capturado', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMedicalFeesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MedicalFeesContractId IS NULL → Modo Estándar: obtiene proveedor y línea de distribución desde dbo.INPROFSAL usando HealthProfessionalCode (GENPROVEE, GENLINDIST) else Modo Agremiaciones: obtiene proveedor y línea de distribución desde MedicalFees.MedicalFeesContract; si GeneralLedgerSettings.HandlesSupportDocument=1 y ThirdParty.ElectronicBiller=1 → Se anula el manejo de documento soporte (HandlesDocumentSupport=0) porque el tercero es facturador electrónico else Se conserva HandlesDocumentSupport=1 si la unidad operativa lo maneja; si BillingConcept.ConceptType=2 AND AccountingType=2 → La cuenta de gastos de honorarios se obtiene de Billing.BillingConceptAccount según el UnitType de la unidad funcional else Se usa FeesExpensesAccountId definido directamente en BillingConcept; si LiquidationType del detalle = 1 y ChangeTracker<>3 → Se suma TotalAmountPayable en ValuePayments (Nature=1, débito) else Si LiquidationType es 2 o 3, se suma en ValueDeductions (Nature=2, crédito); si ValueTotalLiquidation = ValuePayments - ValueDeductions < 0 → Se aborta con error 999 ''El valor de la cuenta por pagar no puede ser menor a cero''; si FeesExpensesAccountId no parametrizado (<=0) → Se acumula error sobre el concepto y se salta el INSERT del detalle vía GOTO; al final, si hay errores, se aborta sin insertar detalles', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMedicalFeesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetSequence; Billing.fnGetUnitType; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMedicalFeesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMedicalFeesLiquidation';
-- GO
