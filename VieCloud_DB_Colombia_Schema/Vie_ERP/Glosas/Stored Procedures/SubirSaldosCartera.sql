

CREATE  PROCEDURE [Glosas].[SubirSaldosCartera]

AS
BEGIN

	BEGIN TRY

begin transaction SALDOGLOSA

	
	
	CREATE TABLE #tablaValidaciones(
		Id integer identity(1,1),
		Mensaje  varchar(200),
		Factura varchar(20)
	)

	declare @count as integer = (select count(*) from dbo.[SALDOSCARTERA])
	--select @count
	declare @contador as integer = 0

	WHILE @contador < @count BEGIN

		set @contador = @contador + 1
		print 'holaa'
		declare @Code  varchar(20)
        declare @OperatingUnitId int = 1 --pitalito
        declare @AccountReceivableType tinyint = 2 --ley 100
        declare   @ThirdPartyId int
        declare   @CustomerId int
        declare  @SellerId int = null 
        declare   @InvoiceId int = null
        declare   @InvoiceNumber varchar(20)
        declare   @AccountReceivableDate datetime
        declare   @Term int = 30 --30 dias
        declare  @ExpiredDate datetime
        declare   @Observations varchar(300)
        declare   @PortfolioStatus tinyint --= 3 -- radicada confirmada  OJOOOOOOOO
        declare   @OpeningBalance bit = 1 --por saldo inicial
        declare   @RecognitionId int = null -- no presupuesto
        declare   @PaymentAgreement bit = 0 --no acuerdo de pago
        declare    @RegistrationAdjusted bit = 0 -- registro NO ajustado
        declare   @MainAccountWithoutFilingId int
        declare  @NumberShares int = 1 --1 cuota
        declare  @Value numeric(18,0)
        declare  @Balance numeric(18,0)
        declare   @Status tinyint = 2 --confirmado
        declare   @CostCenterId int = null
        declare   @InvoiceCategoryId int = 1 --GENERAL
        declare   @AccountWithoutRadicateId int
        declare   @AccountRadicateId int
        declare   @AccountObjectionRemediedId int = null
        declare   @AccountConciliationId int = null
        declare   @AccountLegalCollectionId int
        declare   @AccountDebtorOrder int 
        declare   @AccountCreditorOrder int 
        declare   @AffectBudget bit = 0 --no
        declare   @BudgetId int = null
        

		--variables temporales
		declare @nit as varchar(20)
		declare @CuentaSinRadicar as varchar(20)
	--	declare @CentroCOsto as varchar(20)
		declare @CuentaRadicar as varchar(20)
		declare @CuentaOrdenGlosa as varchar(20)
		declare @CuentaOrdenAcreedores as varchar(20)
		declare @CuentaTrasladojuridico as varchar(20)
		declare @EstadoFactura as varchar(20)

		declare @CuentaContableSaldo as varchar(20)
		--print 'holaa2'
		select  @code = Factura, @nit = Cliente, @InvoiceNumber = Factura,@AccountReceivableDate= Fecha ,@ExpiredDate = DATEADD(month, 1, Fecha),
		@Observations = Observacion, @CuentaSinRadicar = CuentaSinRadicar, @Value = Valor,@Balance = saldo,
		@CuentaRadicar =CuentaRadicada, @CuentaOrdenGlosa=CuentaOrdenGlosa,@CuentaOrdenAcreedores=CuentaAcreedoresGlosas ,@CuentaTrasladojuridico=CuentaJuridico
		,@CuentaContableSaldo = CuentaContableSaldo, @EstadoFactura = EstadoFactura  from dbo.SALDOSCARTERA where Id = @contador
		

	--	print 'holaa3'

		if (select count(*) from Common.ThirdParty where nit = @nit  ) = 0 begin
		  print 'holaa4'
		    insert into #tablaValidaciones 
			select 'el tercero no existe @nit: ' + convert(varchar(20), @nit) AS  Mensaje, @InvoiceNumber
		/*	select '999' as CodigoMensaje, 'No se encontro el tercero' + convert(varchar(20), @nit) AS  Mensaje
			rollback transaction SALDOGLOSA
			return */
		end

		if (select count(*) from Common.Customer where nit = @nit  ) = 0 begin
			insert into #tablaValidaciones 
			select 'el cliente no existe @nit: ' + convert(varchar(20), @nit) AS  Mensaje, @InvoiceNumber
		    /*print 'holaa5'
			select '999' as CodigoMensaje, 'No se encontro cliente' + convert(varchar(20), @nit) AS  Mensaje
			rollback transaction SALDOGLOSA
			return */
		end

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaSinRadicar and LegalBookId = 1  ) = 0 begin
		   insert into #tablaValidaciones 
			select 'la cuenta sin radicar no existe @CuentaSinRadicar: ' + convert(varchar(20), @CuentaSinRadicar) AS  Mensaje, @InvoiceNumber
			/*print 'holaa6'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaSinRadicar' + convert(varchar(20), @CuentaSinRadicar) AS  Mensaje
			rollback transaction SALDOGLOSA
			return*/
		end

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaRadicar and LegalBookId = 1 ) = 0 begin
		  insert into #tablaValidaciones 
			select 'la cuenta radicADA no existe @CuentaRadicar: ' + convert(varchar(20), @CuentaRadicar) AS  Mensaje, @InvoiceNumber
		/*	print 'holaa7'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaRadicar' + convert(varchar(20), @CuentaRadicar) AS  Mensaje
			rollback transaction SALDOGLOSA
			return*/
		end

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaOrdenGlosa and LegalBookId = 1 ) = 0 begin
		  insert into #tablaValidaciones 
			select 'la cuenta ORDEN DE GLOSA no existe @CuentaOrdenGlosa: ' + convert(varchar(20), @CuentaOrdenGlosa) AS  Mensaje, @InvoiceNumber
		/*	print 'holaa7'
			print 'holaa8'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaOrdenGlosa' + convert(varchar(20), @CuentaOrdenGlosa) AS  Mensaje
			rollback transaction SALDOGLOSA
			return*/
		end

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaOrdenAcreedores  and LegalBookId = 1) = 0 begin
		  insert into #tablaValidaciones 
			select 'la cuenta ORDEN ACREEDORES no existe @@CuentaOrdenAcreedores: ' + convert(varchar(20), @CuentaOrdenAcreedores) AS  Mensaje, @InvoiceNumber
			/*print 'holaa9'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaOrdenAcreedores' + convert(varchar(20), @CuentaOrdenAcreedores) AS  Mensaje
			rollback transaction SALDOGLOSA
			return */
		end

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaTrasladojuridico  and LegalBookId = 1) = 0 begin
		  insert into #tablaValidaciones 
			select 'la cuenta juridico no existe @CuentaTrasladojuridico: ' + convert(varchar(20), @CuentaTrasladojuridico) AS  Mensaje, @InvoiceNumber
			/*print 'holaa10'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaTrasladojuridico' + convert(varchar(20), @CuentaTrasladojuridico) AS  Mensaje
			rollback transaction SALDOGLOSA
			return*/
		end

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaContableSaldo and LegalBookId = 1 ) = 0 begin
		 insert into #tablaValidaciones 
			select 'la cuenta de saldo no existe @CuentaContableSaldo: ' + convert(varchar(20), @CuentaContableSaldo) AS  Mensaje, @InvoiceNumber
			/*print 'holaa11'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaContableSaldo' + convert(varchar(20), @CuentaContableSaldo) AS  Mensaje
			rollback transaction SALDOGLOSA
			return*/
		end

		/*if (select count(*) from [Payroll].[CostCenter] where  Code = @CentroCOsto  ) = 0 begin
		 insert into #tablaValidaciones 
			select 'el centro de costo no existe @CentroCOsto: ' + convert(varchar(20), @CentroCOsto) AS  Mensaje, @InvoiceNumber
		/*	print 'holaa12'
			select '999' as CodigoMensaje, 'No se encontro centro de costo' + convert(varchar(20), isnull(@CentroCOsto,'')) AS  Mensaje
			rollback transaction SALDOGLOSA
			return*/
		end*/

		select @ThirdPartyId = id from Common.ThirdParty where nit = @nit   
		select @CustomerId = Id from Common.Customer where nit = @nit
		select @MainAccountWithoutFilingId = Id ,@AccountWithoutRadicateId = Id from GeneralLedger.MainAccounts where  Number = @CuentaSinRadicar and LegalBookId = 1
		--select @CostCenterId = Id from [Payroll].[CostCenter] where Code = @CentroCOsto 
		
		select @AccountRadicateId = Id from GeneralLedger.MainAccounts where  Number = @CuentaRadicar and LegalBookId = 1
		select @AccountDebtorOrder = Id from GeneralLedger.MainAccounts where  Number = @CuentaOrdenGlosa and LegalBookId = 1
		select @AccountCreditorOrder = Id from GeneralLedger.MainAccounts where  Number = @CuentaOrdenAcreedores and LegalBookId = 1
		select @AccountLegalCollectionId = Id from GeneralLedger.MainAccounts where  Number = @CuentaTrasladojuridico and LegalBookId = 1
		

		declare @MainAccountId int
		select @MainAccountId = Id from GeneralLedger.MainAccounts where  Number = @CuentaContableSaldo and LegalBookId = 1
				
	
INSERT INTO [Portfolio].[AccountReceivable]
           ([Code]
           ,[OperatingUnitId]
           ,[AccountReceivableType]
           ,[ThirdPartyId]
           ,[CustomerId]
           ,[SellerId]
           ,[InvoiceId]
           ,[InvoiceNumber]
           ,[AccountReceivableDate]
           ,[Term]
           ,[ExpiredDate]
           ,[Observations]
           ,[PortfolioStatus]
           ,[OpeningBalance]
           ,[RecognitionId]
           ,[PaymentAgreement]
           ,[RegistrationAdjusted]
           ,[MainAccountWithoutFilingId]
           ,[NumberShares]
           ,[Value]
           ,[Balance]
           ,[Status]
           ,[CostCenterId]
           ,[InvoiceCategoryId]
           ,[AccountWithoutRadicateId]
           ,[AccountRadicateId]
           ,[AccountObjectionRemediedId]
           ,[AccountConciliationId]
           ,[AccountLegalCollectionId]
           ,[AccountDebtorOrder]
           ,[AccountCreditorOrder]
           ,[AffectBudget]
           ,[BudgetId]
           ,[CreationUser]
           ,[CreationDate]
           ,[ModificationUser]
           ,[ModificationDate]
           ,[ConfirmationUser]
           ,[ConfirmationDate]
           ,[AnnulmentUser]
           ,[AnnulmentDate])
     VALUES
           (@code
           ,@OperatingUnitId
           ,@AccountReceivableType
           ,@ThirdPartyId
           ,@CustomerId
           ,@SellerId
           ,@InvoiceId
           ,@InvoiceNumber
           ,@AccountReceivableDate
           ,@Term
           ,@ExpiredDate
           ,@Observations
           ,@EstadoFactura
           ,@OpeningBalance
           ,@RecognitionId
           ,@PaymentAgreement
           ,@RegistrationAdjusted
           ,@MainAccountWithoutFilingId
           ,@NumberShares
           ,@Value
           ,@Balance
           ,@Status
           ,@CostCenterId
           ,@InvoiceCategoryId
           ,@AccountWithoutRadicateId
           ,@AccountRadicateId
           ,@AccountObjectionRemediedId
           ,@AccountConciliationId
           ,@AccountLegalCollectionId
           ,@AccountDebtorOrder
           ,@AccountCreditorOrder
           ,@AffectBudget
           ,@BudgetId
           ,'999'
           ,'27/01/2016'
           ,null
           ,null
           ,'999'
           ,'27/01/2016'
           ,null
           ,null)

		   declare @AccountReceivableId as int
		   Select @AccountReceivableId = SCOPE_IDENTITY()

		  

	  INSERT INTO [Portfolio].[AccountReceivableAccounting]
           ([AccountReceivableId]
           ,[MainAccountId]
           ,[ThirdPartyId]
           ,[CostCenterId]
           ,[Value]
           ,[Balance])
     VALUES
           (@AccountReceivableId
           ,@MainAccountId
           ,@ThirdPartyId
           ,@CostCenterId
           ,@Balance
           ,@Balance)

	  INSERT INTO [Portfolio].[AccountReceivableShare]
           ([AccountReceivableId]
           ,[Number]
           ,[ExpiredDate]
           ,[Value]
           ,[Balance]
           ,[DebitValue]
           ,[CreditValue]
           ,[TransferValue]
           ,[PaymentValue]
           ,[CrossingValue]
           ,[InterestPaymentDate]
           ,[InterestValue]
           ,[SurchargesValue]
           ,[CapitalRepaymentAgreement]
           ,[FinancialInterest]
           ,[RepaymentAgreementInterest])
     VALUES
           (@AccountReceivableId
           ,1
           ,@ExpiredDate  
           ,@Value
           ,@Balance
           ,0
           ,0
           ,0
           ,0
           ,0
           ,null
           ,0
           ,0
           ,0
           ,0
           ,0)
		   

		print @contador
	END 
	
	
	if (select COUNT(*) from #tablaValidaciones) > 0 begin
		select * from #tablaValidaciones
		rollback transaction SALDOGLOSA
		return 
	end  

      --Commit Transaction SALDOGLOSA
		rollback transaction SALDOGLOSA
	END TRY
	BEGIN CATCH
		rollback transaction SALDOGLOSA
		SELECT
			ERROR_NUMBER() AS CodigoMensaje,
			ERROR_MESSAGE() AS  Mensaje
	END CATCH

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de carga masiva de saldos iniciales de cartera de glosas. Lee una tabla de staging (SALDOSCARTERA) con facturas, clientes (NIT), valores, saldos y cuentas contables, y para cada registro valida que el tercero exista en Common.ThirdParty, que el cliente exista en Common.Customer, y que todas las cuentas contables (sin radicar, radicada, orden de glosa, acreedores, traslado jurídico) existan en GeneralLedger.MainAccounts antes de crear la cuenta por cobrar en Portfolio.AccountReceivable. Sirve para migrar o inicializar saldos históricos de cartera de glosas —facturas pendientes de cobro a EPS u otras aseguradoras— garantizando integridad referencial entre terceros, clientes y plan de cuentas contables.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SubirSaldosCartera';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SubirSaldosCartera';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Migra/carga saldos iniciales de cartera desde una tabla staging hacia el módulo de cuentas por cobrar, validando terceros, clientes y cuentas contables, y generando la cuenta por cobrar con su contabilización y cuota única.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla staging dbo.SALDOSCARTERA debe estar poblada con Ids consecutivos desde 1 hasta el total de filas (recorre por contador secuencial).; Por cada fila debe existir el tercero en Common.ThirdParty con el NIT indicado.; Por cada fila debe existir el cliente en Common.Customer con el NIT indicado.; Las cuentas contables (sin radicar, radicada, orden de glosa, orden acreedores, jurídico y cuenta de saldo) deben existir en GeneralLedger.MainAccounts con LegalBookId = 1.; La unidad operativa se asume fija (valor 1, ''pitalito'') y el tipo de cuenta por cobrar es Ley 100.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda cuenta por cobrar generada queda marcada como saldo inicial (OpeningBalance=1), sin acuerdo de pago, sin ajuste de registro, sin afectación presupuestal y con estado confirmado.; Cada cuenta por cobrar genera exactamente una cuota (NumberShares=1) y un único registro de contabilización.; Solo se consideran cuentas contables del libro legal 1 (LegalBookId=1).; El tipo de cartera siempre es Ley 100 (AccountReceivableType=2) y la unidad operativa siempre es 1.; Auditoría de creación y confirmación queda fija con usuario ''999'' y fecha ''27/01/2016''.; El procedimiento nunca persiste cambios: siempre termina con ROLLBACK de la transacción SALDOGLOSA (modo simulación/validación).; Si alguna validación falla para cualquier fila, ninguna fila se persiste (rollback global).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cartera; Saldo inicial de cartera; Cuenta por cobrar; Glosa; Tercero; Cliente (NIT); Cuenta contable (PUC); Cuenta sin radicar; Cuenta radicada; Cuenta orden de glosa; Cuenta orden de acreedores; Cuenta de traslado jurídico; Ley 100; Factura; Cuota de cartera', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #tablaValidaciones: Cuando no existe el tercero, cliente o alguna de las cuentas contables requeridas (sin radicar, radicada, orden glosa, orden acreedores, jurídico, saldo) se inserta un mensaje de error con la factura.; [INSERT] Portfolio.AccountReceivable: Por cada fila de la staging que pase las validaciones se inserta una cuenta por cobrar con OperatingUnitId=1, AccountReceivableType=2 (Ley 100), Term=30 días, OpeningBalance=1 (saldo inicial), PaymentAgreement=0, RegistrationAdjusted=0, NumberShares=1, Status=2 (confirmado), InvoiceCategoryId=1 (GENERAL), AffectBudget=0, ExpiredDate = Fecha + 1 mes, y usuarios/fechas de creación y confirmación fijos (''999'',''27/01/2016'').; [INSERT] Portfolio.AccountReceivableAccounting: Tras insertar la cuenta por cobrar, se registra su contabilización con MainAccountId = cuenta contable de saldo, ThirdPartyId del tercero y Value=Balance=saldo de la staging.; [INSERT] Portfolio.AccountReceivableShare: Tras insertar la cuenta por cobrar, se crea una única cuota (Number=1) con ExpiredDate=Fecha+1mes, Value=Valor, Balance=saldo y todos los demás conceptos (intereses, recargos, abonos, etc.) en cero.; [RETURN_RESULT] #tablaValidaciones: Si al final del bucle existen registros en la tabla de validaciones, se devuelve su contenido como resultado y se hace rollback de toda la transacción.; [RETURN_RESULT] Portfolio.AccountReceivable: Independientemente del éxito, al final se ejecuta ROLLBACK de la transacción SALDOGLOSA (el COMMIT está comentado), por lo que ninguna inserción se persiste; el commit está deshabilitado.; [RETURN_RESULT] ERROR: En el bloque CATCH se hace rollback y se devuelve ERROR_NUMBER y ERROR_MESSAGE como resultado.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe tercero, cliente o alguna cuenta contable requerida para la fila procesada → Se acumula mensaje de error en #tablaValidaciones y se continúa procesando las demás filas (no aborta inmediatamente). else Se procede a resolver los Ids y a insertar la cuenta por cobrar, su contabilización y su cuota.; si Existen registros en #tablaValidaciones al finalizar el recorrido → Devuelve los errores acumulados y hace ROLLBACK de la transacción, abortando toda la carga. else Igualmente ejecuta ROLLBACK (el COMMIT está comentado), no persistiendo cambios.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.SALDOSCARTERA; Common.ThirdParty; Common.Customer; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosCartera';
-- GO
