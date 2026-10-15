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