-- =============================================
-- Author:		Diego Roldan
-- Create date: 29-09-2016
-- Description:	Procedimiento para generar la programacion de pagos
-- =============================================
CREATE PROCEDURE [Treasury].[SP_SaveSchedulePayment]
	@SchedulePaymentXml AS XML,
	@User VARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--variable de la cabecera
	declare @IdSchedulePayment int, @CodeSchedulePayment varchar(20),@ScheduledDate date,
		@EntityBankAccountId int,@CostCenterId int,@PaymentMethod tinyint,@CheckId int,
		@NumberNote varchar(50),@TaxByMil bit,@OperativeUnitId int,@Status tinyint,@PaymentDate datetime
	declare @Prefix VARCHAR(50)= ''
	--tabla del detalle

	declare @SchedulePaymentDetail TABLE
	(
		ChangeTracker VARCHAR(30),
		[IdTmp] [int] NOT NULL,
		[Id] [int] NOT NULL,
		[SupplierId] [int] NOT NULL,
		[ThirdPartyId] [int] NOT NULL,
		[SchedulePaymentId] [int] NOT NULL,
		[DistributionLineId] [int] NOT NULL,
		[ExpenseConceptId] [int] NOT NULL,
		[MainAccountId] [int] NOT NULL,
		[Nature] [tinyint] NOT NULL,
		[AccountPayableId] [int] NOT NULL,
		[AccountPayableShareId] [int] NOT NULL,
		[AmountPaid] [numeric](18, 2) NOT NULL,
		[AmountPercent] [decimal](5, 2) NOT NULL,
		[PaymentConceptId] [int] NOT NULL,
		[Paid] [bit] NOT NULL,
		[Description] [varchar](300) NULL,
		[VoucherTransactionId] [int] NULL,
		[GeneratedVoucher] [bit],
		InvoiceBalance numeric(18,2),
		BalanceShare numeric(18,2),
		Invoice varchar(30),
		Share int,
		SupplierCode varchar(50),
		SupplierName varchar(300),
		AccountPayableCode varchar(50),
		DiscountValue decimal(18,2)
	)

	declare @SchedulePaymentDetailBudget TABLE
	(
		ChangeTracker VARCHAR(30),
		[Id] [int] NOT NULL,
		[IdSchedulePaymentDetailTmp] [int] NOT NULL,
		[SchedulePaymentDetailId] [int] NOT NULL,
		[ObligationDetailId] [int] NOT NULL,
		[Value] DECIMAL
	)

	declare @SchedulePaymentBankAccount TABLE (
		ChangeTracker VARCHAR(30),
		[Id] [int] NOT NULL,
		[SchedulePaymentId] [int] NOT NULL,
		[SupplierId] [int] NOT NULL,
		[SupplierBankAccountId] [int] NOT NULL
	)

	declare @TempCalculations TABLE (
    TotalInvoiceValue DECIMAL(18, 2),
    TotalAssetValue DECIMAL(18, 2),
    DiscountPercentage DECIMAL(18, 2),
    DiscountValue DECIMAL(18, 2),
    NetHistoricalValue DECIMAL(18, 2)
)

	--Tabla para ir almacenando los errores 
	DECLARE @TableErrors as table([message] varchar(300))
	--variable para retornar todos los mensajes de error que tenga la tabla de errores
	DECLARE @ErrorsValidation varchar(max)

	BEGIN TRY
		select 
			@IdSchedulePayment = t.x.value('Id[1]', 'int'),
			@CodeSchedulePayment = t.x.value('Code[1]','varchar(20)'),
			@ScheduledDate=t.x.value('ScheduledDate[1]','date'),
			--@ScheduledDate = convert(datetime, t.x.value('ScheduledDate[1]', 'nvarchar(19)'), 103),
			@EntityBankAccountId =t.x.value('EntityBankAccountId[1]', 'int'),
			@CostCenterId =t.x.value('CostCenterId[1]', 'int'),
			@PaymentMethod  = t.x.value('PaymentMethod [1]','tinyint'),
			@CheckId =t.x.value('CheckId[1]', 'int'),
			@NumberNote = t.x.value('NumberNote[1]','varchar(50)'),
			@TaxByMil = t.x.value('TaxByMil[1]','bit'),
			@OperativeUnitId = t.x.value('OperativeUnitId[1]','int'),		
			@Status = t.x.value('Status[1]','tinyint'),
			@PaymentDate = t.x.value('PaymentDate[1]','Datetime')
		from @SchedulePaymentXml.nodes('/SchedulePayment') t(x);

		---detalle
		insert into @SchedulePaymentDetail 
			SELECT 
				t.x.value('ChangeTracker[1]', 'varchar(30)') AS ChangeTracker,
				t.x.value('IdTmp[1]', 'int') AS IdTmp,
				t.x.value('Id[1]', 'int') AS Id,
				t.x.value('SupplierId[1]', 'int') AS SupplierId,
				t.x.value('ThirdPartyId[1]', 'int') AS ThirdPartyId,
				t.x.value('SchedulePaymentId[1]', 'int') AS SchedulePaymentId,
				t.x.value('DistributionLineId[1]', 'int') AS DistributionLineId,
				t.x.value('ExpenseConceptId[1]', 'int') AS ExpenseConceptId,
				t.x.value('MainAccountId[1]', 'int') AS MainAccountId,
				t.x.value('Nature[1]', 'tinyint') AS Nature,		
				t.x.value('AccountPayableId[1]', 'int') AS AccountPayableId,
				t.x.value('AccountPayableShareId[1]', 'int') AS AccountPayableShareId,
				t.x.value('AmountPaid[1]', 'decimal(18,2)') AS AmountPaid,
				t.x.value('AmountPercent[1]', 'decimal(5,2)') AS AmountPercent,
				t.x.value('PaymentConceptId[1]', 'int') AS PaymentConceptId,
				t.x.value('Paid[1]','bit') as Paid,
				t.x.value('Description[1]', 'varchar(300)') AS Observation,
				t.x.value('VoucherTransactionId[1]', 'int') AS VoucherTransactionId,
				t.x.value('GeneratedVoucher[1]','bit') as GeneratedVoucher,
				t.x.value('InvoiceBalance[1]', 'decimal(18,2)') AS InvoiceBalance,
				t.x.value('BalanceShare[1]', 'decimal(18,2)') AS BalanceShare,
				t.x.value('Invoice[1]', 'varchar(30)') AS Invoice,
				t.x.value('Share[1]', 'int') AS Share,
				t.x.value('SupplierCode[1]', 'varchar(50)') AS SupplierCode,
				t.x.value('SupplierName[1]', 'varchar(300)') AS SupplierName,
				t.x.value('AccountPayableCode[1]', 'varchar(50)') AS AccountPayableCode,
				t.x.value('DiscountValue[1]', 'decimal(18,2)') AS DiscountValue
			from @SchedulePaymentXml.nodes('/SchedulePayment/SchedulePaymentDetail') t(x);

		insert into @SchedulePaymentDetailBudget 
			select	t.x.value('ChangeTracker[1]', 'varchar(30)') AS ChangeTracker,
					t.x.value('Id[1]', 'int') AS Id,
					t.x.value('IdSchedulePaymentDetailTmp[1]', 'int') AS IdSchedulePaymentDetailTmp,
					t.x.value('SchedulePaymentDetailId[1]', 'int') AS SchedulePaymentDetailId,
					t.x.value('ObligationDetailId[1]', 'int') AS ObligationDetailId,
					t.x.value('Value[1]', 'decimal') AS Value
			from @SchedulePaymentXml.nodes('/SchedulePayment/SchedulePaymentDetail/SchedulePaymentDetailBudget') t(x);

			Insert into @SchedulePaymentBankAccount 
			select	t.x.value('ChangeTracker[1]', 'varchar(30)') AS ChangeTracker,
					t.x.value('Id[1]', 'int') AS Id,
					t.x.value('SchedulePaymentId[1]', 'int') AS IdSchedulePaymentDetailTmp,
					t.x.value('SupplierId[1]', 'int') AS SchedulePaymentDetailId,
					t.x.value('SupplierBankAccountId[1]', 'int') AS ObligationDetailId
					from @SchedulePaymentXml.nodes('/SchedulePayment/SchedulePaymentBankAccount') t(x);

		
		DECLARE @ConfirmationUser AS VARCHAR(20)= CASE WHEN @Status <> 2 THEN NULL ELSE @User END;
        DECLARE @ConfirmationDate AS DATETIME= CASE WHEN @Status <> 2 THEN NULL ELSE [Common].[GETDATE]() END;
        DECLARE @AnnulmentUser AS VARCHAR(20)= CASE WHEN @Status <> 3 THEN NULL ELSE @User END;
        DECLARE @AnnulmentDate AS DATETIME= CASE WHEN @Status <> 3 THEN NULL ELSE [Common].[GETDATE]() END;

		if (@status <>3) begin		
			--se valida la programacion
			insert into @TableErrors 
			select 'El saldo de la factura ('+ Invoice + ', cuota  '+ cast( Share as varchar(30)) +')  para el Proveedor '+ SupplierCode + ' - ' + SupplierName +' tiene el saldo inferior al valor a pagar'+ CHAR(13) + CHAR(10) 
			from @SchedulePaymentDetail where AmountPaid > BalanceShare and ChangeTracker <> 'Deleted'

			--si existen errores  se termina el proceso 
			if(select COUNT(*) from @TableErrors )>0 begin				
				SELECT @ErrorsValidation = COALESCE(@ErrorsValidation + '', '') + [message]  from @TableErrors				
				SELECT '999' AS CodeMessage,@ErrorsValidation AS Message,0 IdSchedulePayment,CAST(3 AS TINYINT) AS [Status];
				RETURN;
			end
		end

		if @IdSchedulePayment = 0 begin
			
			
			if @CodeSchedulePayment = '' begin
				--secuencia numerica
				DECLARE @idSequenceDetail INT;
				DECLARE @pattern VARCHAR(300);
				DECLARE @NextS BIGINT;
				DECLARE @Scope VARCHAR(5);
				DECLARE @IdSequence INT;
				DECLARE @IdSequenceCommon INT;

				SELECT @IdSequence = Id,@Scope = Scope,@IdSequenceCommon = IdSequence FROM Treasury.TreasurySequence WHERE IdForm = 646;
			
				IF @Scope = 'O'	BEGIN --- Secuencia por Prefijo		
					SELECT @pattern = cs.Pattern,@idSequenceDetail = psd.Id,@NextS = psd.[Next]
					FROM Treasury.TreasurySequenceDetail psd
					INNER JOIN Common.Sequense cs ON cs.Id = psd.IdSequense
					WHERE psd.Prefix = @Prefix and psd.IdSequenseTreasuryC = @IdSequence;
					IF(@idSequenceDetail = 0)BEGIN
						--se agrega un nuevo registro al detalle con el prefijo que no existe
						INSERT INTO Treasury.TreasurySequenceDetail (IdSequenseTreasuryC, IdSequense, IdOperatingUnit, Next, Prefix)
						VALUES(@IdSequence,@IdSequenceCommon,@OperativeUnitId,1,@Prefix);
						SET @idSequenceDetail = SCOPE_IDENTITY();
						SET @NextS = 1;
					END;
				END
				ELSE BEGIN -- Secuencia por Unidad operativa
		
					SELECT @pattern = cs.Pattern,@idSequenceDetail = psd.Id,@NextS = psd.[Next]
					FROM Treasury.TreasurySequenceDetail psd
					INNER JOIN Common.Sequense cs ON cs.Id = psd.IdSequense
					WHERE psd.IdSequenseTreasuryC  = @IdSequence AND IdOperatingUnit = @OperativeUnitId;
				END;
			
				print 'Prefijo ' + cast(@Prefix as varchar(20))
				print 'Patron' +  cast(@pattern as varchar(60))
				print 'Next' +  cast(@NextS as varchar(20))
				SELECT @CodeSchedulePayment = dbo.GetSequence(@Prefix, @pattern, @NextS);
				--valido que la secuancia tenga valor disponible
				IF @CodeSchedulePayment = '__ERROR_MAXVALUE__' BEGIN
					SELECT '999' AS CodeMessage,'La secuencia alcanzo su valor maximo' AS Message,0 IdSchedulePayment,CAST(3 AS TINYINT) AS [Status];
					RETURN;
				END;
            
				--actualizo la secuencia
				UPDATE Treasury.TreasurySequenceDetail
				SET [Next]+=1 WHERE Id = @idSequenceDetail;
			end

			--se inserta la cabecera
			INSERT INTO [Treasury].[SchedulePayment]
			   ([Code]
			   ,[ScheduledDate]
			   ,[EntityBankAccountId]
			   ,[CostCenterId]
			   ,[PaymentMethod]
			   ,[CheckId]
			   ,[NumberNote]
			   ,[TaxByMil]
			   ,[OperativeUnitId]
			   ,[Status]
			   ,[CreationUser]
			   ,[CreationDate]
			   ,[ModificationUser]
			   ,[ModificationDate]
			   ,[ConfirmationUser]
			   ,[ConfirmationDate]
			   ,[AnnulmentUser]
			   ,[AnnulmentDate]
			   ,[PartialPaymentUser]
			   ,[PartialPaymentDate]
			   ,[FullPaymentUser]
			   ,[FullPaymentDate]
			   ,[PaymentDate])
		 VALUES
			   (@CodeSchedulePayment
			   ,@ScheduledDate
			   ,@EntityBankAccountId
			   ,@CostCenterId
			   ,@PaymentMethod
			   ,@CheckId
			   ,@NumberNote
			   ,@TaxByMil
			   ,@OperativeUnitId
			   ,@Status
			   ,@User
			   ,[Common].[GETDATE]()
			   ,null
			   ,null
			   ,@ConfirmationUser
			   ,@ConfirmationDate
			   ,null
			   ,null
			   ,null
			   ,null
			   ,null
			   ,null
			   ,@PaymentDate)

			SET @IdSchedulePayment = SCOPE_IDENTITY();		
			update @SchedulePaymentDetail set SchedulePaymentId = @IdSchedulePayment	
		end
		else begin
			--se actuliza la cabecera
			UPDATE [Treasury].[SchedulePayment]
		   SET [ScheduledDate] = @ScheduledDate
			  ,[EntityBankAccountId] = @EntityBankAccountId
			  ,[CostCenterId] = @CostCenterId
			  ,[PaymentMethod] = @PaymentMethod
			  ,[CheckId] = @CheckId
			  ,[NumberNote] = @NumberNote
			  ,[TaxByMil] = @TaxByMil
			  ,[OperativeUnitId] = @OperativeUnitId
			  ,[Status] = @Status			  
			  ,[ModificationUser] = @User
			  ,[ModificationDate] = [Common].[GETDATE]()
			  ,[ConfirmationUser] = @ConfirmationUser
			  ,[ConfirmationDate] = @ConfirmationDate
			  ,[AnnulmentUser] = @AnnulmentUser
			  ,[AnnulmentDate] = @AnnulmentDate
			  ,[PaymentDate] = @PaymentDate
		 WHERE Id = @IdSchedulePayment
		end

		IF (@Status =1 OR @Status=2) BEGIN
		/************************ELIMINADO,GUARDADO, ACTUALIZACION Y VALIDACION DE INFORMACION BANCARIA PROVEEDORES*************************/
			DELETE spba 
			from Treasury.SchedulePaymentBankAccount spba  WITH(NOLOCK)
			left JOIN @SchedulePaymentBankAccount spbat on  spba.SupplierId= spbat.SupplierId
			where spbat.Id is null and spba.SchedulePaymentId=@IdSchedulePayment

			-- se actualiza los que ya existen
			update spba
			SET SupplierBankAccountId = spbat.SupplierBankAccountId
			from @SchedulePaymentBankAccount spbat
			JOIN  Treasury.SchedulePaymentBankAccount spba WITH(NOLOCK) on spba.SchedulePaymentId=spbat.SchedulePaymentId and spba.SupplierId= spbat.SupplierId

			--Se guardan los datos que no existan actualmente en la tabla
			INSERT INTO Treasury.SchedulePaymentBankAccount
			SELECT @IdSchedulePayment,spbat.SupplierId,spbat.SupplierBankAccountId
			from @SchedulePaymentBankAccount spbat
			LEFT JOIN  Treasury.SchedulePaymentBankAccount spba WITH(NOLOCK) on spba.SchedulePaymentId=@IdSchedulePayment and spba.SupplierId= spbat.SupplierId
			where spba.id is NULL

			-- se valida que los proveedores que no vengan en la tabla de info bancaria, pero que estan en el detalle tenga informacion bancaria parametrizada, para posteriormente insertarlos
			IF EXISTS(	SELECT 1
						FROM (	SELECT spd.SupplierId
								FROM @SchedulePaymentDetail spd
								LEFT JOIN Treasury.SchedulePaymentBankAccount spba WITH(NOLOCK) on spba.SchedulePaymentId=@IdSchedulePayment AND spd.SupplierId = spba.SupplierId
								where spba.Id is null and spd.ChangeTracker <> 'Deleted'
								GROUP by spd.SupplierId) s
						left join Common.SupplierBankAccount sba with(NOLOCK) on s.SupplierId=sba.SupplierId and sba.PaymentDefault=1
						where sba.Id is null
							)
			BEGIN		
				insert into @TableErrors 
				select CONCAT('El proveedor :',s.SupplierCodeName,' No tiene parametrizada una cuenta bancaria por defecto; ')
						FROM (	SELECT spd.SupplierId, concat(s.Code,' - ',s.Name) SupplierCodeName
								FROM @SchedulePaymentDetail spd
								join Common.Supplier s WITH(NOLOCK) on s.Id=spd.SupplierId
								LEFT JOIN Treasury.SchedulePaymentBankAccount spba WITH(NOLOCK) on spba.SchedulePaymentId=@IdSchedulePayment AND spd.SupplierId = spba.SupplierId
								where spba.Id is null and spd.ChangeTracker <> 'Deleted'
								GROUP by spd.SupplierId,s.Code,s.Name) s
						left join Common.SupplierBankAccount sba with(NOLOCK) on s.SupplierId=sba.SupplierId and sba.PaymentDefault=1
						where sba.Id is null

				--si existen errores  se termina el proceso 
				if(select COUNT(*) from @TableErrors )>0 begin				
					SELECT @ErrorsValidation = COALESCE(@ErrorsValidation + '', '') + [message]  from @TableErrors				
					SELECT '999' AS CodeMessage,@ErrorsValidation AS Message,0 IdSchedulePayment,CAST(3 AS TINYINT) AS [Status];
					RETURN;
				end
			END
			ELSE BEGIN
				
				--Se guardan los datos que no existan actualmente en la tabla
				INSERT INTO Treasury.SchedulePaymentBankAccount
				SELECT @IdSchedulePayment,s.SupplierId,sba.Id
				FROM (	SELECT spd.SupplierId
						FROM @SchedulePaymentDetail spd
						LEFT JOIN Treasury.SchedulePaymentBankAccount spba WITH(NOLOCK) on spba.SchedulePaymentId=@IdSchedulePayment AND spd.SupplierId = spba.SupplierId
						where spba.Id is null and spd.ChangeTracker <> 'Deleted'
						GROUP by spd.SupplierId) s
				JOIN Common.SupplierBankAccount sba with(NOLOCK) on s.SupplierId=sba.SupplierId and sba.PaymentDefault=1			
			END
			/*************************************************************************************************************************************************************/
		END

		--si se esta guardando o confirmando
		IF(@Status <> 3)BEGIN
			--se eliminan todos los detalles
			delete spdb from @SchedulePaymentDetailBudget spdbTmp join Treasury.SchedulePaymentDetailBudget spdb ON spdbTmp.Id = spdb.Id where spdbTmp.ChangeTracker='Deleted'
			delete spdb from Treasury.SchedulePaymentDetailBudget spdb JOIN Treasury.SchedulePaymentDetail spd ON spdb.SchedulePaymentDetailId = spd.Id JOIN @SchedulePaymentDetail spdTmp ON spd.Id = spdTmp.Id WHERE spdTmp.ChangeTracker='Deleted'

			delete Treasury.SchedulePaymentDetail from @SchedulePaymentDetail spdTmp inner join Treasury.SchedulePaymentDetail spd on spdTmp.Id = spd.Id where spdTmp.ChangeTracker='Deleted'

			--se actualizan los detalles
			UPDATE spdb
			   SET	[ObligationDetailId] = spdbtmp.ObligationDetailId,
					[Value] = spdbtmp.Value
			from @SchedulePaymentDetailBudget spdbtmp 
			JOIN Treasury.SchedulePaymentDetailBudget spdb ON spdbtmp.Id = spdb.Id 
			WHERE spdbtmp.ChangeTracker='Modified'

			UPDATE [Treasury].[SchedulePaymentDetail]
			   SET [SupplierId] = spdTmp.SupplierId
				  ,[ThirdPartyId] = spdTmp.ThirdPartyId
				  ,[SchedulePaymentId] = spdTmp.SchedulePaymentId
				  ,[DistributionLineId] = spdTmp.DistributionLineId
				  ,[ExpenseConceptId] = spdTmp.ExpenseConceptId
				  ,[MainAccountId] = spdTmp.MainAccountId
				  ,[Nature] = spdTmp.Nature
				  ,[AccountPayableId] = spdTmp.AccountPayableId
				  ,[AccountPayableShareId] = spdTmp.AccountPayableShareId
				  ,[AmountPaid] = spdTmp.AmountPaid
				  ,[AmountPercent] = case when  spdTmp.AmountPercent > 0 then spdTmp.AmountPercent else spd.AmountPercent end
				  ,[PaymentConceptId] = spdTmp.PaymentConceptId
				  ,[Paid] = spdTmp.Paid
				  ,[Description] = spdTmp.[Description]
				  ,[VoucherTransactionId] = spdTmp.VoucherTransactionId
				  ,[GeneratedVoucher] = spdTmp.GeneratedVoucher
				  ,[DiscountValue] = spdTmp.DiscountValue
			from @SchedulePaymentDetail spdTmp inner join Treasury.SchedulePaymentDetail spd on spdTmp.Id = spd.Id where spdTmp.ChangeTracker='Modified'
			-- Se actualizan los activos cuando se hace una dispersión de fondos que provenga de ingreso de activos

			
			UPDATE FixedAsset.FixedAssetPhysicalAsset

				SET 
				--FinancialDiscount = spd.DiscountValue * (faei.TotalValue / ap.InvoiceValue),
				FinancialDiscount = faei.UnitValue * (spd.DiscountValue/ap.InvoiceValue * 100)/100,
				NetHistoricalValue = fapa.HistoricalValue - (spd.DiscountValue * (faei.TotalValue / ap.InvoiceValue))

				FROM FixedAsset.FixedAssetEntry fae
				JOIN FixedAsset.FixedAssetEntryItem faei on faei.FixedAssetEntryId = fae.Id
				JOIN FixedAsset.FixedAssetEntryItemDetail faeid on faei.Id = faeid.FixedAssetEntryItemId
				JOIN FixedAsset.FixedAssetPhysicalAsset fapa on fapa.Plate = faeid.Plate
				JOIN Payments.AccountPayable ap on ap.Id = fae.AccountPayableId
				JOIN @SchedulePaymentDetail spd ON spd.AccountPayableId = ap.Id
				WHERE ap.EntityName = 'FixedAssetEntry'

			DECLARE @SchedulePaymentDetailRows INT,
					@SchedulePaymentDetailIdTmp INT,
					@SchedulePaymentDetailId INT

			SELECT	@SchedulePaymentDetailRows = 1,
					@SchedulePaymentDetailIdTmp = 0

			WHILE @SchedulePaymentDetailRows > 0
			BEGIN
				SELECT TOP 1
					@SchedulePaymentDetailIdTmp = IdTmp
				FROM @SchedulePaymentDetail 
				WHERE ChangeTracker = 'Added'
					AND IdTmp > @SchedulePaymentDetailIdTmp
				ORDER BY IdTmp

				SET @SchedulePaymentDetailRows = @@ROWCOUNT
				IF @SchedulePaymentDetailRows = 0
					BREAK

				--se insertan los detalles
				INSERT INTO [Treasury].[SchedulePaymentDetail]
					select [SupplierId]
					   ,[ThirdPartyId]
					   ,[SchedulePaymentId]
					   ,[DistributionLineId]
					   ,[ExpenseConceptId]
					   ,[MainAccountId]
					   ,[Nature]
					   ,[AccountPayableId]
					   ,[AccountPayableShareId]
					   ,[AmountPaid]
					   ,[AmountPercent]
					   ,[PaymentConceptId]
					   ,[Paid]
					   ,[Description]
					   ,[VoucherTransactionId]
					   ,[GeneratedVoucher]
					   ,[DiscountValue]
					from @SchedulePaymentDetail 
					where ChangeTracker='Added' AND IdTmp = @SchedulePaymentDetailIdTmp

				SET @SchedulePaymentDetailId = SCOPE_IDENTITY()

				INSERT INTO Treasury.SchedulePaymentDetailBudget
					SELECT @SchedulePaymentDetailId, ObligationDetailId, Value
					FROM @SchedulePaymentDetailBudget
					WHERE IdSchedulePaymentDetailTmp = @SchedulePaymentDetailIdTmp AND ChangeTracker = 'Added' AND Value > 0
			END

			INSERT INTO Treasury.SchedulePaymentDetailBudget
				SELECT spdbTmp.SchedulePaymentDetailId, spdbTmp.ObligationDetailId, spdbTmp.Value
				FROM @SchedulePaymentDetailBudget spdbTmp
				JOIN @SchedulePaymentDetail spdTmp ON spdbTmp.IdSchedulePaymentDetailTmp = spdTmp.IdTmp
				WHERE spdbTmp.ChangeTracker = 'Added' AND spdTmp.ChangeTracker = 'Modified' AND Value > 0
		end

		if @Status = 2 begin
			SELECT '0' AS CodeMessage,'Se confirmo correctamente la programación de pagos' AS Message,@IdSchedulePayment as IdSchedulePayment,cast(1 as tinyint) as [Status];
			RETURN;		
		end
		else begin
			SELECT '0' AS CodeMessage,'' AS Message,@IdSchedulePayment as IdSchedulePayment,cast(1 as tinyint) as [Status];
			 RETURN;		
		end
		

	END TRY
    BEGIN CATCH
        SELECT '999' AS CodeMessage,ERROR_MESSAGE()+', Linea: '+CAST(ERROR_LINE() AS VARCHAR(20)) Message,0 AS IdSchedulePayment,CAST(3 AS TINYINT) AS [Status];
    END CATCH	

	--SELECT '999' AS CodeMessage,'' Message,0 AS IdSchedulePayment,CAST(3 AS TINYINT) AS [Status];
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que crea o actualiza una programación de pagos a proveedores en el módulo de Tesorería. Recibe en formato XML la cabecera del pago programado (fecha, método de pago, unidad operativa, cuenta bancaria de la entidad, cheque, nota) junto con sus líneas de detalle (factura del proveedor, cuota, monto a pagar, saldo de la cuota, descuento, concepto presupuestal) y las cuentas bancarias de los proveedores beneficiarios. Genera el código o consecutivo del documento consultando las tablas de secuencias de Tesorería (TreasurySequence y TreasurySequenceDetail) y el patrón de numeración definido en Common.Sequense, garantizando un número único por unidad operativa. Acumula errores de validación durante el proceso y los retorna al llamador, permitiendo controlar el flujo de aprobación y registro contable de las obligaciones por pagar.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveSchedulePayment';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveSchedulePayment';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (crea, actualiza, confirma o anula) una programación de pagos de tesorería con su cabecera, detalles, distribución presupuestal y cuentas bancarias de proveedores, validando saldos de facturas y disponibilidad de cuentas por defecto.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveSchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener un nodo /SchedulePayment con la cabecera y opcionalmente nodos SchedulePaymentDetail, SchedulePaymentDetailBudget y SchedulePaymentBankAccount; Debe existir configuración en Treasury.TreasurySequence para IdForm = 646 cuando se genera un nuevo código; Cada detalle del XML debe traer ChangeTracker (''Added'',''Modified'',''Deleted''); Para guardar/confirmar (@Status 1 ó 2) cada proveedor referenciado debe tener una cuenta bancaria por defecto en Common.SupplierBankAccount (PaymentDefault=1); El BalanceShare informado en el detalle debe ser >= AmountPaid', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveSchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El consecutivo (Code) sólo se genera cuando la programación es nueva (@IdSchedulePayment=0) y viene sin código; Cada vez que se genera un Code se incrementa Next en Treasury.TreasurySequenceDetail; ConfirmationUser/Date sólo se llenan cuando @Status = 2; AnnulmentUser/Date sólo cuando @Status = 3; Las anulaciones (@Status=3) no modifican detalles, presupuestos ni cuentas bancarias asociadas; No se procesan detalles cuyo ChangeTracker sea ''Deleted'' en validaciones (siempre se excluyen); Sólo se insertan registros en SchedulePaymentDetailBudget cuando Value > 0; Toda programación confirmada/guardada exige que cada proveedor tenga al menos una cuenta bancaria con PaymentDefault=1; El AmountPaid de cada cuota nunca puede ser mayor que el BalanceShare (saldo de la cuota); Si el AmountPercent enviado es 0, se preserva el valor previo de la base', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveSchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Programación de pagos; Proveedor; Cuenta bancaria de proveedor; Cuenta por pagar; Cuota de factura; Saldo de factura; Secuencia/consecutivo de tesorería; Unidad operativa; Obligación presupuestal; Activo fijo; Descuento financiero; Confirmación / Anulación de pago', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveSchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Status <> 3 (no es anulación) → Se valida que el AmountPaid de cada detalle no exceda el BalanceShare; si hay errores se aborta retornando CodeMessage 999; si @IdSchedulePayment = 0 (nueva programación) → Genera un nuevo Code (si viene vacío) usando la secuencia configurada e inserta cabecera en Treasury.SchedulePayment else Actualiza la cabecera existente en Treasury.SchedulePayment; si Scope de la secuencia = ''O'' → Busca/crea TreasurySequenceDetail por Prefijo; si no existe lo inserta con Next=1 else Busca TreasurySequenceDetail por IdOperatingUnit; si @CodeSchedulePayment devuelto por dbo.GetSequence = ''__ERROR_MAXVALUE__'' → Aborta retornando ''La secuencia alcanzo su valor maximo'' con CodeMessage 999; si @Status = 1 OR @Status = 2 (guardar o confirmar) → Sincroniza Treasury.SchedulePaymentBankAccount (delete/update/insert) y valida que cada proveedor del detalle tenga una SupplierBankAccount con PaymentDefault=1; si falta, aborta con error; si @Status <> 3 → Procesa los detalles según ChangeTracker: borra los ''Deleted'', actualiza los ''Modified'', e inserta los ''Added'' (con sus presupuestos), y actualiza activos fijos cuando aplica; si @Status = 2 → Setea ConfirmationUser/ConfirmationDate y retorna mensaje ''Se confirmo correctamente la programación de pagos''; si @Status = 3 (anulación) → Setea AnnulmentUser/AnnulmentDate; no procesa detalles ni cuentas bancarias; si ap.EntityName = ''FixedAssetEntry'' en el detalle → Recalcula FinancialDiscount y NetHistoricalValue en FixedAsset.FixedAssetPhysicalAsset prorrateando el descuento sobre el valor del activo; si spdTmp.AmountPercent > 0 al actualizar detalle → Usa el nuevo AmountPercent del XML; en caso contrario conserva el valor existente', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveSchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE; dbo.GetSequence', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveSchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.TreasurySequence; Treasury.TreasurySequenceDetail; Common.Sequense; Treasury.SchedulePaymentBankAccount; Common.SupplierBankAccount; Common.Supplier; Treasury.SchedulePaymentDetail; Treasury.SchedulePaymentDetailBudget; FixedAsset.FixedAssetEntry; FixedAsset.FixedAssetEntryItem; FixedAsset.FixedAssetEntryItemDetail; FixedAsset.FixedAssetPhysicalAsset; Payments.AccountPayable', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveSchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveSchedulePayment';
-- GO
