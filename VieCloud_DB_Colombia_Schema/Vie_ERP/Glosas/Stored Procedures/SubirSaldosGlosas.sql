
CREATE  PROCEDURE [Glosas].[SubirSaldosGlosas]

AS
BEGIN

	BEGIN TRY

begin transaction SALDOGLOSA

	
	
		
	declare @count as integer = (select count(*) from dbo.[SALDOSGLOSAS])
	--select @count
	declare @contador as integer = 0

	WHILE @contador < @count BEGIN

		set @contador = @contador + 1
		print 'holaa'
		declare @Code  varchar(20)
        declare @OperatingUnitId int = 14 --neiva
        declare @AccountReceivableType tinyint = 2 --ley 100
        declare   @ThirdPartyId int
        declare   @CustomerId int
        declare  @SellerId int = 1 --unico registro 
        declare   @InvoiceId int = null
        declare   @InvoiceNumber varchar(20)
        declare   @AccountReceivableDate datetime
        declare   @Term int = 30 --30 dias
        declare  @ExpiredDate datetime
        declare   @Observations varchar(300)
        declare   @PortfolioStatus tinyint = 3 -- radicada confirmada
        declare   @OpeningBalance bit = 1 --por saldo inicial
        declare   @RecognitionId int = null -- no presupuesto
        declare   @PaymentAgreement bit = 0 --no acuerdo de pago
        declare    @RegistrationAdjusted bit = 0 -- registro NO ajustado
        declare   @MainAccountWithoutFilingId int
        declare  @NumberShares int = 1 --1 cuota
        declare  @Value numeric(18,0)
        declare  @Balance numeric(18,0)
        declare   @Status tinyint = 2 --confirmado
        declare   @CostCenterId int
        declare   @InvoiceCategoryId int = 4 --categoria del la CXC
        declare   @AccountWithoutRadicateId int
        declare   @AccountRadicateId int
        declare   @AccountObjectionRemediedId int
        declare   @AccountConciliationId int
        declare   @AccountLegalCollectionId int
        declare   @AccountDebtorOrder int = null
        declare   @AccountCreditorOrder int = null
        declare   @AffectBudget bit = 0 --no
        declare   @BudgetId int = null
        

		--variables temporales
		declare @nit as varchar(20)
		declare @CuentaSinRadicar as varchar(20)
		declare @CentroCOsto as varchar(20)
		declare @CuentaRadicar as varchar(20)
		declare @CuentaGlosa as varchar(20)
		declare @CuentaConciliacion as varchar(20)
		declare @CuentaTrasladojuridico as varchar(20)

		declare @CuentaContableSaldo as varchar(20)
		print 'holaa2'
		select  @code = Factura, @nit = Cliente, @InvoiceNumber = Factura,@AccountReceivableDate= Fecha ,@ExpiredDate = DATEADD(month, 1, Fecha),
		@Observations = Observacion, @CuentaSinRadicar = CuentaSinRadicar, @Value = Valor,@Balance = saldo,
		@CuentaRadicar =CuentaRadicada, @CuentaGlosa=CuentaGlosa,@CuentaConciliacion=CuentaConciliacion,@CuentaTrasladojuridico=CuentaJuridico
		,@CuentaContableSaldo = CuentaContableSaldo,@CentroCOsto = CentroCosto from dbo.[SALDOSGLOSAS] where Id = @contador
		

		print 'holaa3'

		if (select count(*) from Common.ThirdParty where nit = @nit  ) = 0 begin
		print 'holaa4'
			select '999' as CodigoMensaje, 'No se encontro el tercero' + convert(varchar(20), @nit) AS  Mensaje
			rollback transaction SALDOGLOSA
			return
		end

		if (select count(*) from Common.Customer where nit = @nit  ) = 0 begin
		print 'holaa5'
			select '999' as CodigoMensaje, 'No se encontro cliente' + convert(varchar(20), @nit) AS  Mensaje
			rollback transaction SALDOGLOSA
			return
		end

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaSinRadicar and LegalBookId = 1  ) = 0 begin
			print 'holaa6'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaSinRadicar' + convert(varchar(20), @CuentaSinRadicar) AS  Mensaje
			rollback transaction SALDOGLOSA
			return
		end

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaRadicar and LegalBookId = 1 ) = 0 begin
			print 'holaa7'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaRadicar' + convert(varchar(20), @CuentaRadicar) AS  Mensaje
			rollback transaction SALDOGLOSA
			return
		end

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaGlosa and LegalBookId = 1 ) = 0 begin
			print 'holaa8'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaGlosa' + convert(varchar(20), @CuentaGlosa) AS  Mensaje
			rollback transaction SALDOGLOSA
			return
		end

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaConciliacion  and LegalBookId = 1) = 0 begin
			print 'holaa9'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaConciliacion' + convert(varchar(20), @CuentaConciliacion) AS  Mensaje
			rollback transaction SALDOGLOSA
			return
		end

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaTrasladojuridico  and LegalBookId = 1) = 0 begin
			print 'holaa10'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaTrasladojuridico' + convert(varchar(20), @CuentaTrasladojuridico) AS  Mensaje
			rollback transaction SALDOGLOSA
			return
		end

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaContableSaldo and LegalBookId = 1 ) = 0 begin
			print 'holaa11'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaContableSaldo' + convert(varchar(20), @CuentaContableSaldo) AS  Mensaje
			rollback transaction SALDOGLOSA
			return
		end

		if (select count(*) from [Payroll].[CostCenter] where  Code = @CentroCOsto  ) = 0 begin
			print 'holaa12'
			select '999' as CodigoMensaje, 'No se encontro centro de costo' + convert(varchar(20), isnull(@CentroCOsto,'')) AS  Mensaje
			rollback transaction SALDOGLOSA
			return
		end

		select @ThirdPartyId = id from Common.ThirdParty where nit = @nit   
		select @CustomerId = Id from Common.Customer where nit = @nit
		select @MainAccountWithoutFilingId = Id ,@AccountWithoutRadicateId = Id from GeneralLedger.MainAccounts where  Number = @CuentaSinRadicar and LegalBookId = 1
		select @CostCenterId = Id from [Payroll].[CostCenter] where Code = @CentroCOsto 
		
		select @AccountRadicateId = Id from GeneralLedger.MainAccounts where  Number = @CuentaRadicar and LegalBookId = 1
		select @AccountObjectionRemediedId = Id from GeneralLedger.MainAccounts where  Number = @CuentaGlosa and LegalBookId = 1
		select @AccountConciliationId = Id from GeneralLedger.MainAccounts where  Number = @CuentaConciliacion and LegalBookId = 1
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
           ,@PortfolioStatus
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
           ,'20/01/2016'
           ,null
           ,null
           ,'999'
           ,'20/01/2016'
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
           ,@Balance
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
	
	

      Commit Transaction SALDOGLOSA
		--rollback transaction SALDOGLOSA
	END TRY
	BEGIN CATCH
		rollback transaction SALDOGLOSA
		SELECT
			ERROR_NUMBER() AS CodigoMensaje,
			ERROR_MESSAGE() AS  Mensaje
	END CATCH

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que migra o carga saldos iniciales de glosas (objeciones de pagadores a facturas) desde la tabla temporal SALDOSGLOSAS hacia el módulo de cartera y contabilidad del sistema. Por cada registro de glosa, valida que existan el tercero (NIT) en Common.ThirdParty, el cliente en Common.Customer, las cuentas contables requeridas (sin radicar, radicada, glosa, conciliación, jurídico y saldo) en el libro contable principal de GeneralLedger.MainAccounts, y el centro de costo en Payroll.CostCenter; si alguna validación falla, revierte toda la operación con rollback. Su propósito es dejar registradas en el sistema las cuentas por cobrar de glosas históricas con su estado, valor, saldo pendiente y clasificación contable, permitiendo el seguimiento financiero y contable de las objeciones realizadas por EPS, aseguradoras u otros pagadores a las facturas emitidas por la institución.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SubirSaldosGlosas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SubirSaldosGlosas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Migra masivamente saldos históricos de glosas a cuentas por cobrar, creando los registros contables y de cuotas asociados, validando la existencia previa de terceros, clientes, cuentas contables y centro de costo.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosGlosas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla dbo.SALDOSGLOSAS debe contener registros con Id secuencial desde 1 hasta el total de filas (se itera por contador incremental).; Cada nit referenciado debe existir en Common.ThirdParty.; Cada nit referenciado debe existir en Common.Customer.; Las cuentas contables (CuentaSinRadicar, CuentaRadicada, CuentaGlosa, CuentaConciliacion, CuentaJuridico, CuentaContableSaldo) deben existir en GeneralLedger.MainAccounts con LegalBookId = 1.; El centro de costo (CentroCosto) debe existir en Payroll.CostCenter.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosGlosas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda la carga se ejecuta dentro de una única transacción nombrada SALDOGLOSA: o se confirman todos los registros o se revierten todos.; Cualquier validación fallida en cualquier fila aborta el proceso completo (no se procesan filas posteriores).; Las cuentas contables sólo se buscan en el libro legal (LegalBookId = 1).; Toda cuenta por cobrar creada queda con OpeningBalance=1, AccountReceivableType=2 (Ley 100), OperatingUnitId=14 (Neiva), PortfolioStatus=3 y Status=2.; Cada cuenta por cobrar generada tiene exactamente una cuota con saldo igual al valor total y vencimiento un mes después de la fecha origen.; Los registros se crean siempre con CreationUser/ConfirmationUser=''999'' y fecha fija ''20/01/2016'', identificándolos como carga masiva inicial.; No se afecta presupuesto (AffectBudget=0, BudgetId=null) ni se asocia reconocimiento ni acuerdo de pago.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosGlosas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Cuenta por cobrar; Saldo inicial / cartera histórica; Tercero; Cliente / pagador; Cuenta contable (PUC); Cuenta sin radicar; Cuenta radicada; Cuenta de glosa; Cuenta de conciliación; Cuenta de traslado jurídico; Centro de costo; Cuota de cartera; Ley 100; Unidad operativa (Neiva)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosGlosas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Portfolio.AccountReceivable: Por cada fila de dbo.SALDOSGLOSAS válida se inserta una cuenta por cobrar tipo Ley 100 (AccountReceivableType=2), unidad operativa Neiva (=14), estado de cartera ''radicada confirmada'' (=3), Status confirmado (=2), por saldo inicial (OpeningBalance=1), término 30 días, sin presupuesto, sin acuerdo de pago, categoría CXC=4, vendedor=1, usuario de creación/confirmación ''999'' y fecha fija ''20/01/2016''.; [INSERT] Portfolio.AccountReceivableAccounting: Tras insertar la AccountReceivable, se registra su contraparte contable usando la cuenta CuentaContableSaldo y el centro de costo, con Value y Balance iguales al saldo de la glosa.; [INSERT] Portfolio.AccountReceivableShare: Se crea una única cuota (Number=1, NumberShares=1) con ExpiredDate = Fecha + 1 mes, Value y Balance = saldo, y todos los demás importes (débito, crédito, intereses, abonos, etc.) inicializados en 0.; [RETURN_RESULT] (resultset): Si una validación falla se devuelve un resultset con CodigoMensaje=''999'' y un Mensaje describiendo la entidad faltante (tercero, cliente, cuenta contable específica o centro de costo).; [RETURN_RESULT] (resultset): En CATCH se devuelve ERROR_NUMBER() y ERROR_MESSAGE() como CodigoMensaje/Mensaje tras revertir la transacción.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosGlosas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe tercero con el nit en Common.ThirdParty → Retorna mensaje ''999 - No se encontro el tercero'' y hace rollback de la transacción SALDOGLOSA; si No existe cliente con el nit en Common.Customer → Retorna mensaje ''999 - No se encontro cliente'' y hace rollback; si Cualquiera de las cuentas (SinRadicar, Radicada, Glosa, Conciliacion, Juridico, ContableSaldo) no existe en GeneralLedger.MainAccounts con LegalBookId=1 → Retorna mensaje ''999'' indicando la cuenta faltante y hace rollback; si El centro de costo no existe en Payroll.CostCenter → Retorna mensaje ''999 - No se encontro centro de costo'' y hace rollback; si Se produce cualquier excepción dentro del TRY → Hace rollback y devuelve ERROR_NUMBER y ERROR_MESSAGE', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosGlosas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.SALDOSGLOSAS; Common.ThirdParty; Common.Customer; GeneralLedger.MainAccounts; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosGlosas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosGlosas';
-- GO
