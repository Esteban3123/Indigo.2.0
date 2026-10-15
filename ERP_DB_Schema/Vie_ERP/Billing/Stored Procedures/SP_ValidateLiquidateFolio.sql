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