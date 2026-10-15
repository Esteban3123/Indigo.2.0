CREATE PROCEDURE [Billing].[SP_CreatePackageJournalVoucher_Output]
	@InvoiceId int,
	@UserCode varchar(20),
	--Salidas
    @StatusResult bit output,
    @MessageResult varchar(max) output
as
begin
	SET NOCOUNT ON
	
	begin try
		--SP Para generación del comprobante contable de paquetes
		declare @OperationUnitId Int,
			@InvoiceNumber varchar(15),
			@InvoiceDate datetime

		select @OperationUnitId = OperatingUnitId 
			, @InvoiceNumber = InvoiceNumber
			, @InvoiceDate = InvoiceDate
		from Billing.Invoice (nolock) where Id = @InvoiceId

		Declare @LiquidatedPackageJournalVoucherTypeId int
		select @LiquidatedPackageJournalVoucherTypeId = LiquidatedPackageJournalVoucherTypeId
		from Billing.SettingsBilling (nolock) where IdOperatingUnit = @OperationUnitId

		if @LiquidatedPackageJournalVoucherTypeId is null begin
			; throw 51000, 'Tipo de comprobante paquetes liquidados no ha sido configurado en los parámetros de facturación para la unidad operativa indicada.', 1;
		end

		declare @tbJournalVoucherDetails table (
			IdMainAccount int, 
			IdThirdParty int, 
			IdCostCenter int, 
			CreditValue numeric(18, 2), 
			DebitValue numeric(18, 2), 
			Detail varchar(300)
		)

		delete from @tbJournalVoucherDetails
		insert into @tbJournalVoucherDetails
		exec [Billing].[SP_GenerateJournalVoucherDetailsPackage] @InvoiceId, @UserCode, 0, @StatusResult output, @MessageResult output

		if @StatusResult = 0 begin
			select	@StatusResult = convert(bit, 0), 
				@MessageResult = 'Ocurrieron errores al intentar Generar el comprobante contable de los paquetes: ' + isnull(@MessageResult, 'No se pudo generar el comprobante contable')
			return
		end

		declare @JournalXml xml = 
		(
			SELECT *
			FROM 
			(
				SELECT 
					@LiquidatedPackageJournalVoucherTypeId AS IdJournalVoucher,
					@InvoiceDate AS VoucherDate,
					0 AS Imported,
					2 AS Status,
					CONCAT('Contabilización de distribución de paquetes No. ', @InvoiceNumber) as Detail,
					@InvoiceNumber AS EntityCode,
					null AS EntityId,
					null AS EntityName,
					0 AS IsClosedYear
			) As JournalVoucher
			CROSS APPLY @tbJournalVoucherDetails As JournalVoucherDetail
			For Xml Auto, Elements
		)

		--select @JournalXml	
		--print cast(@JournalXml as varchar(max))
		--declare @xml xml = (select * from @tbJournalVoucherDetails for xml path)
		--print cast(@JournalXml as varchar(max))

		declare @TableResultJournal table
		(
			CodeMessage varchar(20), 
			[Message] varchar(max), 
			IdJournalVoucher int
		)
		declare @message varchar(max)

		delete from @TableResultJournal
		insert into @TableResultJournal
		exec [GeneralLedger].[SP_CreateAndValidateJournalVoucherMovement] @JournalXml, @UserCode
					
		if exists (select CodeMessage from @TableResultJournal where CodeMessage <> 0) 
		begin
			select @message = [Message] from @TableResultJournal where CodeMessage <> 0
			select	@StatusResult = convert(bit, 0), 
				@MessageResult = 'Ocurrieron errores al intentar Generar el comprobante contable de los paquetes: ' + isnull(@Message, 'No se pudo generar el comprobante contable')
			return
		end

		select	@StatusResult = convert(bit, 1), @MessageResult = ''
	end try
	begin catch
		select	@StatusResult = convert(bit, 0), @MessageResult = error_message()
	end catch
end