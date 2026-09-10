-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-12-29
-- Description:	Generar las cuentas por cobrar asociadas a una factura de liquidacion
-- =============================================
CREATE PROCEDURE [Billing].[SP_CreateAccountReceivables_Output]
	@CompanyType TINYINT,
	@OperativeUnitId INT,
	@RevenueControlDetailId INT,
	@InvoiceId INT,
	@FolioType INT,
	@LiquidationType INT,
	@ThirdPartyPatientId INT,
	@ListPortfolioAdvanceCrossingXml XML,
	@UserCode VARCHAR(20),
	@CurrencyId INT,
	@TRMValue Decimal(20,5),
	--Salidas
	@ResultStatus BIT OUTPUT,
	@ResultMessage VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @InvoiceThirdPartyId INT,
			@InvoiceValue DECIMAL(18,2),
			@RetentionValue DECIMAL(18,2),
			@Value DECIMAL(18,2),
			@Balance DECIMAL(18,2),
			@ListPortfolioAdvanceCrossingXmlToTransfer XML,
			@TotalPatientWithDiscount DECIMAL(18,2),
			@TotalCrossingValue DECIMAL(18,2),
			@IsMasterAccount Tinyint,
			-------------------------------------------------------------------
			@Message VARCHAR(MAX)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY
		SET @ResultMessage = ''

		SELECT	@InvoiceThirdPartyId = i.ThirdPartyId,
				@InvoiceValue = (i.ThirdPartySalesValue - ISNULL(itd.TaxValue,0)),
				@RetentionValue = ISNULL(icr.Value, 0),
				@TotalPatientWithDiscount = i.TotalPatientWithDiscount,
				@TotalCrossingValue = i.PatientPaidValue,
				@ListPortfolioAdvanceCrossingXmlToTransfer = IIF(@FolioType = 3 OR rcd.IsMasterAccount =4, @ListPortfolioAdvanceCrossingXml, NULL),
				@IsMasterAccount = rcd.IsMasterAccount
		FROM Billing.Invoice i WITH (NOLOCK)
		JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) on i.RevenueControlDetailId=rcd.Id
		LEFT JOIN
		(
			SELECT InvoiceId, SUM(Value) Value
			FROM Billing.InvoiceCustomerRetention WITH (NOLOCK)
			WHERE InvoiceId = @InvoiceId AND CalculateTaxAdvance = 2
			GROUP BY InvoiceId
		) icr ON i.Id = icr.InvoiceId
		LEFT JOIN (	SELECT itd.InvoiceId, SUM(itd.[Value]) TaxValue
					FROM Billing.InvoiceTaxDevolution itd WITH(NOLOCK)
					GROUP BY itd.InvoiceId
					) itd on i.Id = itd.InvoiceId
		WHERE i.id = @InvoiceId

		---------------------------------------------  PAGO POR SERVICIOS ---------------------------------------------
		
		IF @LiquidationType = 1

		BEGIN
			SET @Value = @InvoiceValue
			SET @Balance = @Value - @RetentionValue

			EXEC [Billing].[SP_CreateAccountReceivable_Output]	@CompanyType,
																@OperativeUnitId,
																@RevenueControlDetailId,
																@InvoiceId, 
																@FolioType,
																@LiquidationType,
																2,
																@InvoiceThirdPartyId, 
																@Value,
																@Balance,
																@ListPortfolioAdvanceCrossingXmlToTransfer, 
																@UserCode, 
																@CurrencyId,
																@TRMValue,
																--Salidas
																@ResultStatus OUTPUT, 
																@Message OUTPUT 

			IF @ResultStatus = 0
			BEGIN
				SELECT	@ResultStatus = CONVERT(BIT, 0), 
						@ResultMessage = ISNULL(@Message, 'Error generando la cuentas por cobrar de la entidad / paciente')
				RETURN
			END
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		--IF @FolioType <> 3 AND @TotalCrossingValue > 0 and @IsMasterAccount = 0
		--BEGIN
		--	SET @Value = @TotalCrossingValue
		--	SET @Balance = IIF(@LiquidationType = 1, @Value, 0)

		--	EXEC [Billing].[SP_CreateAccountReceivable_Output]	@CompanyType,
		--														@OperativeUnitId,
		--														@RevenueControlDetailId,
		--														@InvoiceId, 
		--														@FolioType,
		--														@LiquidationType,
		--														6,
		--														@ThirdPartyPatientId, 
		--														@Value,
		--														@Balance,
		--														@ListPortfolioAdvanceCrossingXml, 
		--														@UserCode, 
		--														@CurrencyId,
		--														@TRMValue,
		--														--Salidas
		--														@ResultStatus OUTPUT, 
		--														@Message OUTPUT 

		--	IF @ResultStatus = 0
		--	BEGIN
		--		SELECT	@ResultStatus = CONVERT(BIT, 0), 
		--				@ResultMessage = ISNULL(@Message, 'Error generando la cuentas por cobrar de la entidad / paciente')
		--		RETURN
		--	END

		--	SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		--END

		SELECT	@ResultStatus = CONVERT(BIT, 1)
	END TRY
	BEGIN CATCH
		SELECT	@ResultStatus = CONVERT(BIT, 0),
				@ResultMessage = CONCAT('Error creando las cuentas por cobrar: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera las cuentas por cobrar asociadas a una factura de liquidación de servicios de salud. A partir de una factura emitida (Billing.Invoice), consulta el detalle del control de ingresos (RevenueControlDetail), las retenciones tributarias aplicadas al cliente (InvoiceCustomerRetention) y las devoluciones de impuestos (InvoiceTaxDevolution) para calcular el valor neto a cobrar y su saldo pendiente. Cuando el tipo de liquidación corresponde a pago por servicios, delega la creación individual de cada cuenta por cobrar al procedimiento SP_CreateAccountReceivable_Output, pasando el tercero responsable del pago, el importe, el saldo, el tipo de folio y los anticipos de portafolio a cruzar. Existe para centralizar la lógica de generación de cuentas por cobrar de facturación, garantizando que los descuentos, retenciones e impuestos queden correctamente descontados antes de registrar la deuda en cartera.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CreateAccountReceivables_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CreateAccountReceivables_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera, para una factura de liquidación de salida, la cuenta por cobrar al tercero pagador descontando devoluciones de impuestos y retenciones, delegando la creación al SP de detalle.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateAccountReceivables_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El @InvoiceId debe existir en Billing.Invoice y estar relacionado con un RevenueControlDetail válido.; La factura debe tener poblados ThirdPartyId, ThirdPartySalesValue, TotalPatientWithDiscount y PatientPaidValue para los cálculos.; Las retenciones consideradas en InvoiceCustomerRetention deben tener CalculateTaxAdvance = 2 para descontar del saldo.; Los parámetros de moneda (@CurrencyId, @TRMValue) y de operación (@CompanyType, @OperativeUnitId) deben corresponder a la liquidación que se está generando.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateAccountReceivables_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor de la CxC del tercero se calcula siempre como ThirdPartySalesValue menos TaxValue (devoluciones de impuestos) de la factura.; El saldo de la CxC se reduce por las retenciones con CalculateTaxAdvance = 2 sumadas en InvoiceCustomerRetention.; Solo se generan CxC del tercero pagador cuando la liquidación es de tipo 1 (pago por servicios).; El XML de cruces de cartera/anticipos solo se transfiere cuando el folio es tipo 3 o la cuenta es maestra (IsMasterAccount = 4); en los demás casos se omite.; El SP hijo se invoca con tipo de tercero fijo = 2 (entidad), nunca con el ThirdPartyPatientId.; Cualquier excepción es capturada y devuelta como ResultStatus=0 con el mensaje y línea del error, sin propagar la excepción.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateAccountReceivables_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuentas por cobrar; Factura de liquidación; Retención tributaria; Devolución de impuestos; Cruce de cartera y anticipos; Tercero pagador / entidad; Folio de facturación; Cuenta maestra (Master Account); TRM / moneda; Pago por servicios', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateAccountReceivables_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[CALL] Billing.SP_CreateAccountReceivable_Output: Cuando @LiquidationType = 1, invoca el SP hijo con tipo de tercero 2 (entidad), Value = ThirdPartySalesValue - TaxValue y Balance = Value - RetentionValue, para crear la CxC del tercero pagador.; [RETURN_RESULT] @ResultStatus/@ResultMessage: Retorna ResultStatus=1 si la creación fue exitosa; ResultStatus=0 con mensaje del SP hijo o ''Error generando la cuentas por cobrar de la entidad / paciente'' cuando el hijo falla; en CATCH retorna ResultStatus=0 con ''Error creando las cuentas por cobrar: <ERROR_MESSAGE> - Linea: <ERROR_LINE>''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateAccountReceivables_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @LiquidationType = 1 (pago por servicios) → Calcula @Value = ThirdPartySalesValue - TaxValue y @Balance = @Value - RetentionValue, y delega en SP_CreateAccountReceivable_Output con tipo de tercero 2 (entidad) para crear la CxC del tercero pagador else No genera cuentas por cobrar (otros tipos de liquidación quedan sin efecto en este flujo); si @FolioType = 3 OR rcd.IsMasterAccount = 4 → Propaga @ListPortfolioAdvanceCrossingXml al SP hijo para aplicar cruces de cartera/anticipos else Envía NULL como XML de cruces, evitando cruces de anticipos en la CxC generada; si @ResultStatus = 0 retornado por SP_CreateAccountReceivable_Output → Aborta con ResultStatus=0 y mensaje ''Error generando la cuentas por cobrar de la entidad / paciente'' (o el mensaje hijo) else Concatena el mensaje hijo al ResultMessage y continúa marcando éxito', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateAccountReceivables_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_CreateAccountReceivable_Output', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateAccountReceivables_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.RevenueControlDetail; Billing.InvoiceCustomerRetention; Billing.InvoiceTaxDevolution', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateAccountReceivables_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateAccountReceivables_Output';
-- GO
