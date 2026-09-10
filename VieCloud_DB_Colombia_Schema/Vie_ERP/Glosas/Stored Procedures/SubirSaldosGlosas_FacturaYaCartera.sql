
CREATE PROCEDURE [Glosas].[SubirSaldosGlosas_FacturaYaCartera]

AS
BEGIN

	BEGIN TRY

begin transaction SALDOGLOSAYACARTERA

	CREATE TABLE #tablaValidaciones(
		Id integer identity(1,1),
		Mensaje  varchar(200),
		Factura varchar(20),
		valorTotal decimal(18,0),
		cartera decimal(18,0),
		Glosa decimal(18,0),
		valorSaldo decimal(18,0)
	)
		
	declare @count as integer = (select count(*) from [dbo].[SALDOSGLOSAS_ESTANCARTERA])
	--select @count
	declare @contador as integer = 0
	print @count
		print 'Entro'
	WHILE @contador < @count BEGIN

		set @contador = @contador + 1
		declare   @InvoiceNumber varchar(20)
		declare   @ThirdPartyId int
		declare  @Value numeric(18,0)
		declare  @Balance numeric(18,0)

		--variables temporales
		declare @nit as varchar(20)
		declare @CuentaContableSaldo as varchar(20)
		
		select  @nit = Cliente, @InvoiceNumber = Factura,@Value = Valor,@Balance = saldo,@CuentaContableSaldo = CuentaContableSaldo from dbo.[SALDOSGLOSAS_ESTANCARTERA] where Id = @contador

			
		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaContableSaldo and LegalBookId = 1  ) = 0 begin
			print 'holaa11'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaContableSaldo' + convert(varchar(20), @CuentaContableSaldo) AS  Mensaje
			rollback transaction SALDOGLOSAYACARTERA
			return
		end

		if (select count(*) from [Portfolio].[AccountReceivable] where  InvoiceNumber = @InvoiceNumber  ) = 0 begin
			print 'holaa12'
			select '999' as CodigoMensaje, 'No se encontro cuenta por cobrar para la factura' + convert(varchar(20), @InvoiceNumber) AS  Mensaje
			rollback transaction SALDOGLOSAYACARTERA
			return
		end

		
		declare @MainAccountId int
		select @MainAccountId = Id from GeneralLedger.MainAccounts where  Number = @CuentaContableSaldo and LegalBookId = 1
				

		declare @AccountReceivableId as int
		declare @ValorTotalActual as decimal(18,0)
		 declare @saldoCartera as decimal(18,0)
		select @AccountReceivableId = Id , @ThirdPartyId = ThirdPartyId ,@ValorTotalActual = Value,@saldoCartera = Balance  from  [Portfolio].[AccountReceivable] where InvoiceNumber = @InvoiceNumber 

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
           ,null
           ,@Balance
           ,@Balance)

		   declare @saldoActualizar as decimal(18,0)
		   select @saldoActualizar = sum(Balance) from [Portfolio].[AccountReceivableAccounting] where [AccountReceivableId] = @AccountReceivableId

		   if @saldoActualizar  > @ValorTotalActual begin
				insert into #tablaValidaciones 
				select 'El saldo total de movimiento no puede ser mayor al valor de la cuenta por cobrar: ' + convert(varchar(20), @InvoiceNumber) AS  Mensaje, @InvoiceNumber,  @ValorTotalActual,@saldoCartera,@Balance, @saldoActualizar
		   end 

		  /* declare @ValorCuota as decimal(18,0)
		   select @ValorCuota =Value  from Portfolio.AccountReceivableShare where AccountReceivableId = @AccountReceivableId 

		   if @saldoActualizar > @ValorCuota begin 
				 insert into #tablaValidaciones 
				select 'El saldo de la cuota no puede ser mayor al valor de la cuota AccountReceivableShare : ' + convert(varchar(20), @InvoiceNumber) AS  Mensaje, @InvoiceNumber,  @ValorCuota,@saldoActualizar,0,0
			end */

		   update Portfolio.AccountReceivable set Balance = @saldoActualizar where id = @AccountReceivableId

		  update Portfolio.AccountReceivableShare set Value = @saldoActualizar, Balance = @saldoActualizar where AccountReceivableId = @AccountReceivableId
		
	  
		print @contador
	END 
	
	
	if (select COUNT(*) from #tablaValidaciones) > 0 begin
		select * from #tablaValidaciones
		rollback transaction SALDOGLOSAYACARTERA
		return 
	end  

	 Commit Transaction SALDOGLOSAYACARTERA
	--	rollback transaction SALDOGLOSAYACARTERA
	END TRY
	BEGIN CATCH
		rollback transaction SALDOGLOSAYACARTERA
		SELECT
			ERROR_NUMBER() AS CodigoMensaje,
			ERROR_MESSAGE() AS  Mensaje
	END CATCH

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que carga los saldos de glosas pendientes, registrados en la tabla de trabajo SALDOSGLOSAS_ESTANCARTERA, hacia el módulo de cartera del sistema. Por cada factura con glosa, inserta un nuevo movimiento contable en AccountReceivableAccounting asociando el saldo de la glosa a la cuenta contable correspondiente, y actualiza el saldo total de la cuenta por cobrar (AccountReceivable) y de su cuota de pago (AccountReceivableShare) para reflejar el impacto financiero de la glosa. Valida que la cuenta contable exista en el libro legal y que la factura tenga una cuenta por cobrar registrada; si el saldo acumulado supera el valor original de la factura, revierte toda la transacción y reporta las inconsistencias encontradas. Se usa en el cierre o conciliación de cartera de glosas para sincronizar los saldos glosados con la contabilidad de cuentas por cobrar.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SubirSaldosGlosas_FacturaYaCartera';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SubirSaldosGlosas_FacturaYaCartera';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Sincroniza los saldos glosados pendientes con la contabilidad de cuentas por cobrar, generando movimientos contables y actualizando saldos de cartera y cuotas, validando que no superen el valor original de la factura.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosGlosas_FacturaYaCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla staging de saldos de glosas debe estar poblada con los registros a procesar (cliente, factura, valor, saldo, cuenta contable).; Cada cuenta contable referenciada debe existir en el plan de cuentas con LegalBookId = 1.; Cada factura referenciada debe existir como cuenta por cobrar en cartera.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosGlosas_FacturaYaCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda la operación se ejecuta dentro de una única transacción nombrada; cualquier error o inconsistencia revierte todos los cambios.; Solo se opera sobre cuentas contables del libro legal con LegalBookId = 1.; El Balance de la cuenta por cobrar siempre se recalcula como la suma de los Balance de sus movimientos contables asociados.; El valor y saldo de las cuotas se mantienen iguales entre sí y al saldo acumulado tras el procesamiento.; El saldo acumulado de movimientos contables no debe superar el valor total de la cuenta por cobrar; si lo hace, no se persiste.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosGlosas_FacturaYaCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosas; Cartera; Cuentas por cobrar; Saldos contables; Cuenta contable (PUC); Tercero; Factura; Cuotas de pago; Conciliación contable', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosGlosas_FacturaYaCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Portfolio.AccountReceivableAccounting: Por cada registro del staging, inserta un movimiento contable asociando la cuenta por cobrar de la factura, la cuenta contable del saldo, el tercero y el valor/saldo de la glosa (CostCenterId siempre null).; [UPDATE] Portfolio.AccountReceivable: Tras insertar el movimiento, actualiza el Balance de la cuenta por cobrar al valor de la suma de Balance de todos sus registros contables asociados.; [UPDATE] Portfolio.AccountReceivableShare: Actualiza Value y Balance de las cuotas de la cuenta por cobrar al saldo acumulado recalculado.; [INSERT] #tablaValidaciones: Cuando el saldo acumulado de movimientos contables supera el valor total de la cuenta por cobrar, registra una inconsistencia con el mensaje ''El saldo total de movimiento no puede ser mayor al valor de la cuenta por cobrar''.; [RETURN_RESULT] (resultset): Si la cuenta contable no existe (LegalBookId=1) retorna CodigoMensaje ''999'' con mensaje ''No se encontro cuenta'' y hace rollback.; [RETURN_RESULT] (resultset): Si la factura no tiene cuenta por cobrar, retorna CodigoMensaje ''999'' con mensaje ''No se encontro cuenta por cobrar para la factura'' y hace rollback.; [RETURN_RESULT] (resultset): Al finalizar el ciclo, si existen inconsistencias acumuladas, retorna el listado completo de validaciones y hace rollback de toda la transacción.; [RETURN_RESULT] (resultset): En caso de excepción, hace rollback y retorna ERROR_NUMBER y ERROR_MESSAGE.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosGlosas_FacturaYaCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si La cuenta contable del saldo no existe en el plan de cuentas con LegalBookId=1 → Reporta error ''999'' y hace rollback de la transacción, terminando el procedimiento. else Continúa el procesamiento del registro.; si La factura del registro no existe en cuentas por cobrar → Reporta error ''999'' y hace rollback de la transacción, terminando el procedimiento. else Continúa con la inserción contable y actualización de saldos.; si La suma de Balance de los movimientos contables de la cuenta por cobrar excede el valor total de la misma → Registra la inconsistencia en la tabla temporal de validaciones (no aborta de inmediato). else No registra inconsistencia.; si Existen registros en la tabla temporal de validaciones al final del proceso → Devuelve el detalle de inconsistencias y hace rollback de toda la operación. else Confirma (commit) la transacción.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosGlosas_FacturaYaCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.SALDOSGLOSAS_ESTANCARTERA; GeneralLedger.MainAccounts; Portfolio.AccountReceivable; Portfolio.AccountReceivableAccounting', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosGlosas_FacturaYaCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SubirSaldosGlosas_FacturaYaCartera';
-- GO
