CREATE PROCEDURE [Billing].[SP_CreatePackageJournalVoucherAnnulment_Output]
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

		Declare @ReversionLiquidatedPackageJournalVoucherTypeId int
		select @ReversionLiquidatedPackageJournalVoucherTypeId = ReversionLiquidatedPackageJournalVoucherTypeId
		from Billing.SettingsBilling (nolock) where IdOperatingUnit = @OperationUnitId

		if @ReversionLiquidatedPackageJournalVoucherTypeId is null begin
			; throw 51000, 'Tipo de comprobante reversión de paquetes liquidados no ha sido configurado en los parámetros de facturación para la unidad operativa indicada.', 1;
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
		exec [Billing].[SP_GenerateJournalVoucherDetailsPackage] @InvoiceId, @UserCode, 1, @StatusResult output, @MessageResult output

		declare @JournalXml xml = 
		(
			SELECT *
			FROM 
			(
				SELECT 
					@ReversionLiquidatedPackageJournalVoucherTypeId AS IdJournalVoucher,
					@InvoiceDate AS VoucherDate,
					0 AS Imported,
					2 AS Status,
					CONCAT('Contabilización de distribución de paquetes No. ', @InvoiceNumber, ' anulación') as Detail,
					@InvoiceNumber AS EntityCode,
					@InvoiceId AS EntityId,
					'Invoice' AS EntityName,
					0 AS IsClosedYear
			) As JournalVoucher
			CROSS APPLY @tbJournalVoucherDetails As JournalVoucherDetail
			For Xml Auto, Elements
		)

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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera y registra el comprobante contable de reversión (anulación) correspondiente a la liquidación de paquetes de servicios de salud facturados. Recibe el identificador de una factura anulada, consulta sus datos en Billing.Invoice y obtiene el tipo de comprobante de reversión configurado en SettingsBilling para la unidad operativa. Construye el detalle contable llamando a SP_GenerateJournalVoucherDetailsPackage y luego crea y valida el movimiento en el libro mayor mediante SP_CreateAndValidateJournalVoucherMovement, retornando el estado y mensaje de resultado del proceso de anulación contable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CreatePackageJournalVoucherAnnulment_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CreatePackageJournalVoucherAnnulment_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el comprobante contable de reversión/anulación para una factura de paquetes liquidados, construyendo el XML del asiento y delegando su creación y validación al módulo de contabilidad general.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreatePackageJournalVoucherAnnulment_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe existir en Billing.Invoice para poder obtener OperatingUnitId, InvoiceNumber e InvoiceDate.; La unidad operativa de la factura debe tener configurado ReversionLiquidatedPackageJournalVoucherTypeId en Billing.SettingsBilling; de lo contrario se lanza error 51000.; SP_GenerateJournalVoucherDetailsPackage debe poder producir el detalle contable (líneas débito/crédito) para la factura indicada.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreatePackageJournalVoucherAnnulment_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El comprobante de anulación siempre se emite con Status=2 e Imported=0 e IsClosedYear=0.; El tipo de comprobante usado siempre proviene del parámetro ReversionLiquidatedPackageJournalVoucherTypeId configurado por unidad operativa en Billing.SettingsBilling.; La fecha del comprobante (VoucherDate) coincide siempre con la InvoiceDate de la factura.; El asiento se asocia a la entidad ''Invoice'' usando el Id y el InvoiceNumber de la factura como EntityId/EntityCode.; El detalle textual del comprobante siempre incluye el número de factura y la palabra ''anulación''.; Ante cualquier error (configuración faltante, fallas en validación o excepción), nunca se reporta éxito: StatusResult queda en 0.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreatePackageJournalVoucherAnnulment_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Comprobante contable; Anulación/reversión; Paquetes liquidados; Unidad operativa; Parámetros de facturación; Asiento contable (débito/crédito)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreatePackageJournalVoucherAnnulment_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] GeneralLedger (vía SP_CreateAndValidateJournalVoucherMovement): Cuando el detalle del comprobante se genera correctamente, se construye un XML con cabecera (tipo=ReversionLiquidatedPackageJournalVoucherTypeId, Status=2, Imported=0, IsClosedYear=0, Detail=''Contabilización de distribución de paquetes No. <InvoiceNumber> anulación'', EntityName=''Invoice'') y sus líneas, y se invoca SP_CreateAndValidateJournalVoucherMovement para crear y validar el asiento.; [RETURN_RESULT] (parámetros de salida): Si en @TableResultJournal existe algún CodeMessage <> 0, retorna @StatusResult=0 y @MessageResult=''Ocurrieron errores al intentar Generar el comprobante contable de los paquetes: '' + mensaje (o ''No se pudo generar el comprobante contable'' si es null).; [RETURN_RESULT] (parámetros de salida): Si no hay errores en la creación del comprobante, retorna @StatusResult=1 y @MessageResult=''''.; [RAISERROR] (error): Si ReversionLiquidatedPackageJournalVoucherTypeId es NULL para la unidad operativa, lanza THROW 51000 indicando que el tipo de comprobante de reversión de paquetes liquidados no ha sido configurado.; [RETURN_RESULT] (parámetros de salida): Cualquier excepción capturada en el CATCH retorna @StatusResult=0 y @MessageResult=ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreatePackageJournalVoucherAnnulment_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @ReversionLiquidatedPackageJournalVoucherTypeId IS NULL → THROW 51000 indicando falta de configuración del tipo de comprobante de reversión para la unidad operativa. else Continúa con la generación del detalle y creación del comprobante contable.; si EXISTS en @TableResultJournal con CodeMessage <> 0 tras invocar SP_CreateAndValidateJournalVoucherMovement → Devuelve StatusResult=0 con mensaje de error compuesto y termina con RETURN. else Devuelve StatusResult=1 y MessageResult vacío indicando éxito.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreatePackageJournalVoucherAnnulment_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_GenerateJournalVoucherDetailsPackage; GeneralLedger.SP_CreateAndValidateJournalVoucherMovement', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreatePackageJournalVoucherAnnulment_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.SettingsBilling', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreatePackageJournalVoucherAnnulment_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreatePackageJournalVoucherAnnulment_Output';
-- GO
