
CREATE PROCEDURE [Glosas].[CorregirSaldoCartera]

AS
BEGIN

	BEGIN TRY

begin transaction CorregirSaldoCartera

	
	declare @count as integer = (select count(*) from [dbo].SALDOSGLOSAS_CORREGIRSALDOCARTERA)
	--select @count
	declare @contador as integer = 0

	WHILE @contador < @count BEGIN

		set @contador = @contador + 1
		declare   @InvoiceNumber varchar(20)
		declare  @Value numeric(18,0)
		declare  @Balance numeric(18,0)
		declare @MainAccountSinRadicar as varchar(20)
	
		select   @InvoiceNumber = Factura,@Value = Valor,@Balance = saldo,@MainAccountSinRadicar = cuentasaldo from dbo.SALDOSGLOSAS_CORREGIRSALDOCARTERA where Id = @contador

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @MainAccountSinRadicar and LegalBookId = 1 ) = 0 begin
			print 'holaa11'
			select '999' as CodigoMensaje, 'No se encontro cuenta @MainAccountSinRadicar' + convert(varchar(20), @MainAccountSinRadicar) AS  Mensaje
			rollback transaction SALDOGLOSAYACARTERA
			return
		end
				
		declare @MainAccountId int
		select @MainAccountId = Id from GeneralLedger.MainAccounts where  Number = @MainAccountSinRadicar and LegalBookId = 1

		if (select count(*) from [Portfolio].[AccountReceivable] where  InvoiceNumber = @InvoiceNumber  ) = 0 begin
			print 'holaa12'
			select '999' as CodigoMensaje, 'No se encontro cuenta por cobrar para la factura' + convert(varchar(20), @InvoiceNumber) AS  Mensaje
			rollback transaction CorregirSaldoCartera
			return
		end

		
						

		declare @AccountReceivableId as int
		select @AccountReceivableId = Id  from  [Portfolio].[AccountReceivable] where InvoiceNumber = @InvoiceNumber 

		update [Portfolio].[AccountReceivableAccounting] set Balance = @Balance where AccountReceivableId = @AccountReceivableId AND MainAccountId = @MainAccountId

	

		declare @saldoActualizar as decimal(18,0)
		select @saldoActualizar = sum(Balance) from [Portfolio].[AccountReceivableAccounting] where [AccountReceivableId] = @AccountReceivableId

		 update Portfolio.AccountReceivable set Balance = @saldoActualizar where id = @AccountReceivableId

		 update Portfolio.AccountReceivableShare set Balance = @saldoActualizar where AccountReceivableId = @AccountReceivableId
		
	  
		print @contador
	END 
	
	
	

	 Commit Transaction CorregirSaldoCartera
		--rollback transaction CorregirSaldoCartera
	END TRY
	BEGIN CATCH
		rollback transaction CorregirSaldoCartera
		SELECT
			ERROR_NUMBER() AS CodigoMensaje,
			ERROR_MESSAGE() AS  Mensaje
	END CATCH

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de corrección masiva de saldos de cartera afectados por glosas. Lee una tabla de trabajo (SALDOSGLOSAS_CORREGIRSALDOCARTERA) que contiene registros con número de factura, valor, saldo corregido y cuenta contable, y para cada registro actualiza el saldo de la cuenta contable específica en AccountReceivableAccounting, recalcula el saldo total de la cuenta por cobrar en AccountReceivable y sincroniza las cuotas de pago en AccountReceivableShare. Se usa para ajustar manualmente los saldos pendientes de facturas cuando existen diferencias o glosas que distorsionan el saldo real de cartera.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'CorregirSaldoCartera';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'CorregirSaldoCartera';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recorre una tabla staging de ajustes y corrige el saldo contable de cuentas por cobrar (factura) sincronizando saldos en cartera, su detalle contable y sus cuotas.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'CorregirSaldoCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla staging dbo.SALDOSGLOSAS_CORREGIRSALDOCARTERA debe estar poblada con Id consecutivo desde 1 y columnas Factura, Valor, saldo y cuentasaldo.; Cada cuentasaldo debe existir en GeneralLedger.MainAccounts con LegalBookId = 1.; Cada Factura debe existir en Portfolio.AccountReceivable.; Debe existir registro en Portfolio.AccountReceivableAccounting para la combinación AccountReceivable y MainAccount a actualizar.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'CorregirSaldoCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran cuentas contables del libro legal con LegalBookId=1.; Tras la corrección, Portfolio.AccountReceivable.Balance siempre queda igual a la suma de los Balance de su detalle en AccountReceivableAccounting.; Portfolio.AccountReceivableShare.Balance queda igualado al saldo total recalculado de la cuenta por cobrar (mismo valor para todas las cuotas).; Toda la corrección se ejecuta dentro de una transacción explícita; cualquier validación fallida o error revierte todos los cambios.; El recorrido se realiza secuencialmente sobre Id de la staging desde 1 hasta el conteo total de filas.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'CorregirSaldoCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosas; Cartera / Cuentas por cobrar; Factura; Saldo contable; Plan de cuentas (MainAccount); Libro legal contable; Cuotas de cuenta por cobrar (Share)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'CorregirSaldoCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Portfolio.AccountReceivableAccounting: Para cada fila de la staging, actualiza Balance con el saldo indicado en la cuenta contable (LegalBookId=1) y la cuenta por cobrar correspondientes a la factura.; [UPDATE] Portfolio.AccountReceivable: Tras ajustar el detalle contable, recalcula Balance como SUM(Balance) de todas las filas de AccountReceivableAccounting de esa cuenta por cobrar.; [UPDATE] Portfolio.AccountReceivableShare: Asigna a Balance de todas las cuotas (Share) el mismo saldo total recalculado de la cuenta por cobrar.; [RETURN_RESULT] (resultset): Si no se encuentra la cuenta contable para cuentasaldo con LegalBookId=1, hace rollback y devuelve CodigoMensaje=''999'' con mensaje de cuenta no encontrada.; [RETURN_RESULT] (resultset): Si no existe AccountReceivable para la factura, hace rollback y devuelve CodigoMensaje=''999'' con mensaje de cuenta por cobrar no encontrada.; [RETURN_RESULT] (resultset): Ante cualquier error capturado, hace rollback y devuelve ERROR_NUMBER y ERROR_MESSAGE como CodigoMensaje y Mensaje.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'CorregirSaldoCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe MainAccount con Number=cuentasaldo y LegalBookId=1 → Rollback y retorno con mensaje ''999'' indicando cuenta no encontrada else Obtiene MainAccountId y continúa validando la factura; si No existe AccountReceivable con InvoiceNumber=Factura → Rollback y retorno con mensaje ''999'' indicando cuenta por cobrar no encontrada else Procede a actualizar Balance contable, recalcular saldo y propagarlo a AccountReceivable y AccountReceivableShare', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'CorregirSaldoCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.SALDOSGLOSAS_CORREGIRSALDOCARTERA; GeneralLedger.MainAccounts; Portfolio.AccountReceivable; Portfolio.AccountReceivableAccounting', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'CorregirSaldoCartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'CorregirSaldoCartera';
-- GO
