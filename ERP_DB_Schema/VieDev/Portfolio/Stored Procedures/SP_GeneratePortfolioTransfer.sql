-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-10
-- Description:	Genera un cruce de anticipos vs CxC
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_GeneratePortfolioTransfer]
	@ListPortfolioAdvanceCrossingXml XML,
	@OperativeUnitId INT,
	@UserCode VARCHAR(20),
	@AccountReceivableId INT,
	@CompanyType TINYINT,
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@Id_Output INT,
			------------------------------
			@OfficalCurrencyId INT

	DECLARE @ListPortfolioAdvanceCrossing TABLE
	(
		PortfolioAdvanceId INT,
		CrossingValue DECIMAL(18, 2),
		EntityName varchar(20)
	)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY

		SET @OfficalCurrencyId = (SELECT TOP 1 OfficialCurrencyId FROM GeneralLedger.CompanySettings WITH(NOLOCK))
		INSERT INTO @ListPortfolioAdvanceCrossing
			SELECT 
				t.x.value('Id[1]', 'INT'),
				t.x.value('CrossingValue[1]', 'DECIMAL(18, 2)'),
				IIF(t.x.value('EntityName[1]', 'Varchar(20)') = '',NULL,t.x.value('EntityName[1]', 'Varchar(20)'))
			FROM @ListPortfolioAdvanceCrossingXml.nodes('ListPortfolioAdvanceCrossing') t(x)

		IF EXISTS (SELECT 1 FROM @ListPortfolioAdvanceCrossing) 
		BEGIN			
			DECLARE @PortfolioAdvanceRows INT = 1, 
					@PortfolioAdvanceId INT = 0,
					@CrossingValue DECIMAL(18, 2),
					@EntityName VARCHAR(20)

			WHILE @PortfolioAdvanceRows > 0
			BEGIN				
				SELECT TOP 1 
					@PortfolioAdvanceId = PortfolioAdvanceId,
					@CrossingValue = CrossingValue,
					@EntityName = EntityName
				FROM @ListPortfolioAdvanceCrossing 
				WHERE PortfolioAdvanceId > @PortfolioAdvanceId 
				ORDER BY PortfolioAdvanceId

				SET @PortfolioAdvanceRows = @@ROWCOUNT
				IF @PortfolioAdvanceRows = 0 
				BEGIN
					BREAK
				END

				/******************************************** GENERAR COMPROBANTE ********************************************/
				
				SELECT @SubXml = CONVERT
				(
					XML,
					(
						SELECT
							PortfolioTransfer.*,
							PortfolioTransferDetail.*
						FROM
						(
							SELECT	0 Id,
									'' Code,
									@OperativeUnitId OperatingUnitId,
									[Common].[GETDATE]() DocumentDate,
									pa.ThirdPartyId,
									pa.Id PortfolioAdvanceId,
									0 TransferType,
									pa.MainAccountId,
									pa.CostCenterId,
									CONCAT( 'Traslado de anticipo modulo facturación',
									CASE @EntityName
									WHEN 'BasicBilling' THEN ' (Fact. Basica)'
									ELSE '' 
									END) Observations ,
									2 Status,
									pa.CurrencyId,
									COALESCE(@EntityName,'Invoice') as EntityName
							FROM Portfolio.PortfolioAdvance pa WITH(NOLOCK)
							WHERE pa.Id = @PortfolioAdvanceId
						) PortfolioTransfer 
						JOIN 
						(
							SELECT
								0 PortfolioTrasferId,
								ar.Id AccountReceivableId,
								ara.MainAccountId,
								ara.CostCenterId,
								IIF
								(	--Comparamos el valor del cruce vs el saldo del anticipo convertido en la moneda de la factura 
									@CrossingValue = ROUND(Common.CurrencyConverterByModule(pa.Balance, ISNULL(pa.CurrencyId, @OfficalCurrencyId), ar.CurrencyId,NULL,COALESCE(@EntityName,'Invoice'),CAST(Common.GETDATE() as DATE)), 2), 
									--Si el saldo convertido es igual al valor del cruce, significa que el anticipo se afecta en su totalidad
									pa.Balance,
									--De lo contrario, convertimos el valor a cruzar en la moneda del anticipo para sacar el valor a afectar del anticipo
									ROUND(Common.CurrencyConverterByModule(@CrossingValue, ar.CurrencyId, ISNULL(pa.CurrencyId, @OfficalCurrencyId),NULL,COALESCE(@EntityName,'Invoice'),CAST(Common.GETDATE() as DATE)), 2)
								) Value,
								1 TRMValue,
								0 ChangeTracker,
								@CrossingValue ValueInCurrencyInvoice
							FROM Portfolio.AccountReceivable ar WITH(NOLOCK)
							JOIN Portfolio.AccountReceivableAccounting ara WITH(NOLOCK) ON ar.Id = ara.AccountReceivableId
							JOIN Portfolio.PortfolioAdvance pa WITH(NOLOCK) ON pa.Id = @PortfolioAdvanceId
							WHERE ar.Id = @AccountReceivableId
						) PortfolioTransferDetail ON PortfolioTransfer.Id = PortfolioTransferDetail.PortfolioTrasferId
						For XML AUTO,TYPE, ELEMENTS
					)
				)

				EXEC [Portfolio].[SP_SavePortfolioTransfer_Output] @SubXml, @UserCode, @CompanyType, @Code_Output OUTPUT, @Message_Output OUTPUT, NULL, NULL

				IF @Code_Output <> 0
				BEGIN
					SELECT	@CodeResult = 999,
							@MessageResult = ISNULL(@Message_Output, '')
					RETURN
				END

				SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
			END
		END

		SELECT	@CodeResult = 0,
				@MessageResult = ISNULL(@Message, '')
	END TRY
	BEGIN CATCH
		SELECT	@CodeResult = 99,
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20))
	END CATCH	
END