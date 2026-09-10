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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera y registra el comprobante contable (asiento contable) correspondiente a la liquidación de paquetes de servicios facturados. Dado el identificador de una factura, consulta la unidad operativa y el tipo de comprobante configurado en los parámetros de facturación, obtiene el detalle de los movimientos contables del paquete mediante SP_GenerateJournalVoucherDetailsPackage, y luego crea y valida el asiento en el libro mayor llamando a SP_CreateAndValidateJournalVoucherMovement. Existe para automatizar la contabilización de la distribución de paquetes al momento de facturar, garantizando que cada factura de paquete quede respaldada por su respectivo comprobante contable en la contabilidad general.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CreatePackageJournalVoucher_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CreatePackageJournalVoucher_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el comprobante contable de distribución de paquetes liquidados para una factura, construyendo el detalle por cuenta/tercero/centro de costo y registrándolo mediante el módulo de libro mayor.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreatePackageJournalVoucher_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura indicada debe existir en Billing.Invoice (de allí se obtiene unidad operativa, número y fecha); Debe existir registro en Billing.SettingsBilling para la unidad operativa de la factura con LiquidatedPackageJournalVoucherTypeId configurado; SP_GenerateJournalVoucherDetailsPackage debe retornar exitosamente los detalles (débitos/créditos por cuenta, tercero y centro de costo) para la factura', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreatePackageJournalVoucher_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El tipo de comprobante usado siempre proviene de SettingsBilling.LiquidatedPackageJournalVoucherTypeId asociado a la unidad operativa de la factura; El comprobante se crea con Imported=0, Status=2 e IsClosedYear=0; El detalle del comprobante siempre incluye el texto ''Contabilización de distribución de paquetes No. '' concatenado con el número de la factura; EntityCode del comprobante es siempre el InvoiceNumber; EntityId y EntityName se envían en null; Cualquier excepción no controlada se captura y se devuelve como @StatusResult=0 con el mensaje de error del sistema (sin propagar la excepción); Si la generación de detalles falla, no se invoca la creación del comprobante en GeneralLedger', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreatePackageJournalVoucher_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante contable; Paquetes liquidados; Factura; Unidad operativa; Parámetros de facturación; Distribución de paquetes; Tercero; Centro de costo; Cuenta contable', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreatePackageJournalVoucher_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RAISERROR] Billing.SettingsBilling: Si LiquidatedPackageJournalVoucherTypeId es NULL para la unidad operativa, lanza THROW 51000 con mensaje sobre falta de configuración del tipo de comprobante de paquetes liquidados; [INSERT] GeneralLedger (vía SP_CreateAndValidateJournalVoucherMovement): Construye un XML con encabezado (tipo comprobante liquidado, fecha de factura, Status=2, Imported=0, detalle ''Contabilización de distribución de paquetes No. <InvoiceNumber>'') y los detalles generados, y lo envía a SP_CreateAndValidateJournalVoucherMovement para crear el movimiento contable; [RETURN_RESULT] @StatusResult/@MessageResult: Devuelve StatusResult=1 y MessageResult='''' cuando el comprobante se crea sin errores; en caso de fallo en detalles o en la creación, devuelve StatusResult=0 con mensaje prefijado ''Ocurrieron errores al intentar Generar el comprobante contable de los paquetes: ...''; [RETURN_RESULT] @StatusResult/@MessageResult: Ante cualquier excepción capturada en CATCH, devuelve StatusResult=0 y MessageResult con ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreatePackageJournalVoucher_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LiquidatedPackageJournalVoucherTypeId no configurado en SettingsBilling para la unidad operativa de la factura → Lanza THROW 51000 indicando que el tipo de comprobante de paquetes liquidados no está parametrizado else Continúa con la generación del comprobante; si SP_GenerateJournalVoucherDetailsPackage retorna @StatusResult = 0 → Retorna estado falso con mensaje ''Ocurrieron errores al intentar Generar el comprobante contable de los paquetes: ...'' y termina sin invocar al GeneralLedger else Construye el XML y procede a crear el comprobante; si SP_CreateAndValidateJournalVoucherMovement devuelve algún registro con CodeMessage <> 0 → Retorna estado falso con el mensaje de error proveniente del libro mayor else Retorna estado verdadero con mensaje vacío indicando éxito', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreatePackageJournalVoucher_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_GenerateJournalVoucherDetailsPackage; GeneralLedger.SP_CreateAndValidateJournalVoucherMovement', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreatePackageJournalVoucher_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.SettingsBilling', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreatePackageJournalVoucher_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreatePackageJournalVoucher_Output';
-- GO
