-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2015-09-24
-- Description:	Genera la factura y las cuentas por cobrar de un folio a liquidar
-- =============================================
CREATE PROCEDURE [Billing].[SP_ValidateLiquidateFolio]
	-- Add the parameters for the stored procedure here
	@RevenueControlDetailId as int,
	@BillingAuthorizationId as int
AS
BEGIN
	SET NOCOUNT ON;

	declare @InvoicePreviusId int
	declare @FolioOrder tinyint
	declare @messageValidationContract varchar(400)
	declare @StatusContract tinyint
	declare @ContractExecuteValue decimal(18,0)
	declare @TotalFolio decimal(18,0)
	declare @TotalPatientWithDiscount decimal(18,0)
	declare @TerminationControl tinyint
	declare @ContractValue decimal(18,0)
	declare @ContractNotificationValueType tinyint
	declare @ContractEndDate date
	declare @ContractNotificationTimeType tinyint
	declare @ContractNotificationDays int
	declare @ContractPercentageNotification decimal(5,2)
	declare @ContractNotificationValue decimal(18,0)
	declare @ContractCodeName varchar(50)
	declare @careGroupId int
	declare @AuthorizationConsecutive bigint
	declare @AuthorizationFinalInvoice bigint
	declare @AuthorizationInitialDate date
	declare @AuthorizationFinalDate date

	BEGIN TRY

		select @FolioOrder=FolioOrder,@careGroupId=CareGroupId from Billing.RevenueControlDetail where Id = @RevenueControlDetailId

		--==validación de que no haya una factura activa para el mismo folio
		select @InvoicePreviusId=Id from Billing.Invoice where RevenueControlDetailId = @REVENUECONTROLDETAILID and Status = 1
		if @InvoicePreviusId > 0
		begin 
			select '0' as Resultado, 'No se puede liquidar debido a que ya hay una factura activa para el folio '+convert(varchar(10),@FolioOrder) as Mensaje, '' as NotificationContract
			return
		end
		
		--==Validación de consecutivos de autorización
		select @AuthorizationConsecutive = Consecutive,
			@AuthorizationFinalInvoice = FinalInvoice,
			@AuthorizationInitialDate = InitialDate,
			@AuthorizationFinalDate = FinalDate
		from Billing.BillingAuthorization 
		where Id = @BillingAuthorizationId

		if @AuthorizationConsecutive = @AuthorizationFinalInvoice
		begin
			select '0' as Resultado, 'No hay consecutivos disponibles para asignar a la factura del número de autorización asignado' as Mensaje, '' as NotificationContract
			return
		end

		IF @AuthorizationInitialDate IS NOT NULL AND @AuthorizationFinalDate IS NOT NULL
		BEGIN
			--==Validación de consecutivos de autorización
			IF [Common].[GETDATE]() < @AuthorizationInitialDate OR [Common].[GETDATE]() > @AuthorizationFinalDate
			BEGIN
				SELECT '0' as Resultado, 'La autorización asignada no se encuentra vigente' as Mensaje, '' as NotificationContract
				RETURN
			END
		END

		select @StatusContract=c.Status,@ContractExecuteValue=c.ExecuteValue,@ContractValue=c.ContractValue,@TerminationControl=c.TerminationControl,
		@ContractCodeName=CONCAT(c.Code,' - ',c.ContractName),@ContractNotificationValueType=c.NotificationValueType,@ContractNotificationValue=c.NotificationValue,
		@ContractEndDate=c.EndDate,@ContractNotificationTimeType=c.NotificationTimeType,@ContractNotificationDays=c.NotificationDays
		from Contract.CareGroup cg
		inner join Contract.Contract c on cg.ContractId = c.Id
		where cg.Id = @careGroupId

		--==Validaciones del contrato
		if @StatusContract = 2 or @StatusContract = 3
		begin
			select '0' as Resultado, 'No se puede liquidar el folio ('+convert(varchar(10),@FolioOrder)+') debido a que el contrato esta ('+IIF(@StatusContract=2,'Suspendido','Terminado')+')' as Mensaje ,0 as InvoiceId, '' as NotificationContract
			return
		end
		declare @NewExecuteValue decimal(18,0) = (@ContractExecuteValue+@TotalFolio-@TotalPatientWithDiscount)
		if @TerminationControl = 3
		begin
			--Terminación del contrato por valor del contrato
			if @NewExecuteValue > @ContractValue
				set @messageValidationContract += 'El folio '+convert(varchar(10),@FolioOrder)+' no se pueden liquidar debido a que se superaría el valor del contrato '+@ContractCodeName
			if @ContractNotificationValueType=2 and @NewExecuteValue > @ContractValue*@ContractPercentageNotification/100
				set @messageValidationContract += 'Atención: queda un saldo restante de '+convert(varchar(10),(@ContractValue-@NewExecuteValue))+' para la terminación del contrato '+@ContractCodeName
			else if @ContractNotificationValueType=3 and @NewExecuteValue > @ContractNotificationValue
				set @messageValidationContract += 'Atención: queda un saldo restante de '+convert(varchar(10),(@ContractValue-@NewExecuteValue))+' para la terminación del contrato '+@ContractCodeName
		end
		else if @TerminationControl = 2
		begin
			--Terminación del contrato por Fecha del contrato
			if [Common].[GETDATE]() > @ContractEndDate
				set @messageValidationContract += 'El folio '+convert(varchar(10),@FolioOrder)+' no se pueden liquidar debido a ya se venció la fecha del contrato '+@ContractCodeName
			if @ContractNotificationTimeType=2 and DATEADD(DAY, @ContractNotificationDays, [Common].[GETDATE]()) >= @ContractEndDate
				set @messageValidationContract += 'Atención: La fecha del contrato vencerá en '+DATEDIFF(DD, @ContractEndDate, DATEADD(DAY, @ContractNotificationDays, [Common].[GETDATE]()))+' días'
		end
		else if @TerminationControl = 4
		begin
			--Terminación del contrato por Fecha o Valor del contrato
			if @NewExecuteValue > @ContractValue
				set @messageValidationContract += 'El folio '+convert(varchar(10),@FolioOrder)+' no se pueden liquidar debido a que se superaría el valor del contrato '+@ContractCodeName
			if [Common].[GETDATE]() > @ContractEndDate
				set @messageValidationContract += 'El folio '+convert(varchar(10),@FolioOrder)+' no se pueden liquidar debido a ya se venció la fecha del contrato '+@ContractCodeName
			if @ContractNotificationValueType=2 and @NewExecuteValue > @ContractValue*@ContractPercentageNotification/100
				set @messageValidationContract += 'Atención: queda un saldo restante de '+convert(varchar(10),(@ContractValue-@NewExecuteValue))+' para la terminación del contrato '+@ContractCodeName
			else if @ContractNotificationValueType=3 and @NewExecuteValue > @ContractNotificationValue
				set @messageValidationContract += 'Atención: queda un saldo restante de '+convert(varchar(10),(@ContractValue-@NewExecuteValue))+' para la terminación del contrato '+@ContractCodeName
			if @ContractNotificationTimeType=2 and DATEADD(DAY, @ContractNotificationDays, [Common].[GETDATE]()) >= @ContractEndDate
				set @messageValidationContract += 'Atención: La fecha del contrato vencerá en '+DATEDIFF(DD, @ContractEndDate, DATEADD(DAY, @ContractNotificationDays, [Common].[GETDATE]()))+' días'
		end
		--===END validaciones contrato		

		select '1' as Resultado, 'OK' as Mensaje ,@messageValidationContract as NotificationContract
	END TRY
	BEGIN CATCH
		SELECT  '0'  as Resultado ,  'Se ha producido un error!' + ERROR_MESSAGE() Mensaje , 0 as Invoiceid, '' as NotificationContract
	END CATCH

	--select SUM(GrandTotalDiscount) from Billing.ServiceOrderDetailDistribution where RevenueControlDetailId = 1143
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida y prepara la liquidación de un folio de facturación antes de generar la factura y las cuentas por cobrar. Verifica que no exista ya una factura activa para el mismo folio, que la autorización DIAN tenga consecutivos disponibles y se encuentre vigente, y que el contrato asociado al grupo de atención no esté suspendido, terminado, vencido por fecha ni superado en valor. Combina información de los detalles de control de ingresos (RevenueControlDetail), las autorizaciones de numeración DIAN (BillingAuthorization) y el contrato con la entidad pagadora (Contract/CareGroup) para devolver un resultado de aprobación o rechazo con mensajes de alerta sobre el estado del contrato (saldo restante, proximidad de vencimiento, tope de valor).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateLiquidateFolio';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateLiquidateFolio';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida que un folio pueda liquidarse verificando ausencia de factura activa, disponibilidad y vigencia de consecutivos de autorización DIAN, y estado/topes/vigencia del contrato asociado, devolviendo resultado y mensajes de notificación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateLiquidateFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El RevenueControlDetailId debe existir y tener un CareGroupId asociado vinculado a un contrato.; El BillingAuthorizationId debe existir en Billing.BillingAuthorization.; El CareGroup debe estar asociado a un Contract.Contract válido.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateLiquidateFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No se permite liquidar un folio si ya tiene una factura activa (Status=1).; No se permite liquidar usando una autorización DIAN cuyo consecutivo se agotó (Consecutive=FinalInvoice).; No se permite liquidar usando una autorización DIAN fuera de su vigencia (InitialDate–FinalDate).; No se permite liquidar contra un contrato Suspendido (Status=2) o Terminado (Status=3).; Las advertencias de cercanía a topes o vencimiento del contrato se entregan al cliente vía NotificationContract sin bloquear cuando son informativas.; El procedimiento siempre devuelve un resultset con columnas Resultado, Mensaje y NotificationContract.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateLiquidateFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'folio de facturación; factura activa; autorización DIAN; consecutivo de facturación; vigencia de autorización; contrato; estado del contrato (suspendido/terminado); valor ejecutado del contrato; tope de valor del contrato; fecha de terminación del contrato; notificación por porcentaje de ejecución; notificación por días previos al vencimiento; grupo de atención (CareGroup)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateLiquidateFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Si existe Billing.Invoice con RevenueControlDetailId=@RevenueControlDetailId y Status=1, retorna Resultado=''0'' con mensaje de factura activa para el folio.; [RETURN_RESULT] (resultset): Si Consecutive = FinalInvoice en BillingAuthorization, retorna Resultado=''0'' indicando que no hay consecutivos disponibles.; [RETURN_RESULT] (resultset): Si InitialDate y FinalDate de la autorización no son nulas y la fecha actual (Common.GETDATE()) está fuera de ese rango, retorna Resultado=''0'' con mensaje ''La autorización asignada no se encuentra vigente''.; [RETURN_RESULT] (resultset): Si el contrato tiene Status=2 (Suspendido) o Status=3 (Terminado), retorna Resultado=''0'' indicando que el contrato está suspendido o terminado.; [RETURN_RESULT] (resultset): Si todas las validaciones pasan, retorna Resultado=''1'', Mensaje=''OK'' y un NotificationContract con advertencias acumuladas sobre topes o vencimiento del contrato.; [RETURN_RESULT] (resultset): En caso de excepción no controlada, el bloque CATCH retorna Resultado=''0'' con ''Se ha producido un error!'' + ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateLiquidateFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe una Invoice con Status=1 para el mismo RevenueControlDetailId → Retorna error: ya hay factura activa para el folio; si Consecutive = FinalInvoice en la autorización → Retorna error: no hay consecutivos disponibles; si Fecha actual fuera del rango [InitialDate, FinalDate] de la autorización → Retorna error: autorización no vigente; si StatusContract IN (2,3) → Retorna error indicando contrato Suspendido (2) o Terminado (3); si TerminationControl=3 (terminación por valor) → Si NewExecuteValue supera ContractValue se concatena bloqueo; si NotificationValueType=2 y supera porcentaje, o =3 y supera NotificationValue, se concatena alerta de saldo restante; si TerminationControl=2 (terminación por fecha) → Si fecha actual > ContractEndDate se concatena bloqueo; si NotificationTimeType=2 y faltan ≤NotificationDays se concatena alerta de vencimiento próximo; si TerminationControl=4 (terminación por fecha o valor) → Aplica acumulativamente las validaciones de valor y fecha de las opciones 2 y 3', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateLiquidateFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateLiquidateFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControlDetail; Billing.Invoice; Billing.BillingAuthorization; Contract.CareGroup; Contract.Contract', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateLiquidateFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateLiquidateFolio';
-- GO
