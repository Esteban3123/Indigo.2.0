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
			@OfficalCurrencyId INT,
			@InvoiceRoundPrecision INT,
			@InvoiceCurrencyId INT

	DECLARE @ListPortfolioAdvanceCrossing TABLE
	(
		PortfolioAdvanceId INT,
		CrossingValue DECIMAL(18, 2),
		EntityName varchar(20)
	)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY

		SET @OfficalCurrencyId = (SELECT TOP 1 OfficialCurrencyId FROM GeneralLedger.CompanySettings WITH(NOLOCK))
		
		-- Obtener la precisión de redondeo de la moneda de la cuenta por cobrar
		SELECT @InvoiceCurrencyId = ar.CurrencyId,
			   @InvoiceRoundPrecision = Common.GetRoundPrecision(c.RoundingType)
		FROM Portfolio.AccountReceivable ar WITH(NOLOCK)
		JOIN Common.Currency c WITH(NOLOCK) ON ar.CurrencyId = c.Id
		WHERE ar.Id = @AccountReceivableId

		-- Si no se encontró, usar precisión por defecto (2 decimales)
		SET @InvoiceRoundPrecision = ISNULL(@InvoiceRoundPrecision, 2)

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
								(
									-- Si la moneda del anticipo es la misma que la de la cuenta por cobrar, usar el valor redondeado según la moneda
									ISNULL(pa.CurrencyId, @OfficalCurrencyId) = ar.CurrencyId,
									ROUND(@CrossingValue, @InvoiceRoundPrecision),
									-- Si las monedas son diferentes, aplicar conversión con redondeo adecuado
									IIF
									(	--Comparamos el valor del cruce vs el saldo del anticipo convertido en la moneda de la factura 
										ROUND(@CrossingValue, @InvoiceRoundPrecision) = ROUND(Common.CurrencyConverterByModule(pa.Balance, ISNULL(pa.CurrencyId, @OfficalCurrencyId), ar.CurrencyId,NULL,COALESCE(@EntityName,'Invoice'),CAST(Common.GETDATE() as DATE)), @InvoiceRoundPrecision), 
										--Si el saldo convertido es igual al valor del cruce, significa que el anticipo se afecta en su totalidad
										pa.Balance,
										--De lo contrario, convertimos el valor a cruzar en la moneda del anticipo para sacar el valor a afectar del anticipo
										ROUND(Common.CurrencyConverterByModule(@CrossingValue, ar.CurrencyId, ISNULL(pa.CurrencyId, @OfficalCurrencyId),NULL,COALESCE(@EntityName,'Invoice'),CAST(Common.GETDATE() as DATE)), @InvoiceRoundPrecision)
									)
								) Value,
								1 TRMValue,
								0 ChangeTracker,
								-- Aplicar el mismo redondeo a ValueInCurrencyInvoice para evitar diferencias de redondeo
								ROUND(@CrossingValue, @InvoiceRoundPrecision) ValueInCurrencyInvoice
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el traslado (cruce) de uno o varios anticipos de cartera contra una cuenta por cobrar específica (factura o cuenta de cobro). Recibe un listado de anticipos en formato XML con sus montos de cruce, valida la moneda y la precisión de redondeo consultando la configuración contable y el catálogo de monedas, y construye el comprobante de traslado con sus detalles contables (cuenta principal, tercero, centro de costo) tomando datos de las tablas de anticipos, cuentas por cobrar y su registro contable. Si las monedas del anticipo y la factura difieren, aplica conversión de divisas antes de registrar el valor; finalmente invoca SP_SavePortfolioTransfer_Output para persistir cada comprobante generado y actualizar los saldos correspondientes en el módulo de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePortfolioTransfer';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePortfolioTransfer';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera y persiste los comprobantes de traslado/cruce entre anticipos de cartera y una cuenta por cobrar, manejando conversiones de moneda y redondeos para afectar los saldos correctos.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir registro en Portfolio.AccountReceivable para el AccountReceivableId recibido, junto con su AccountReceivableAccounting.; Cada PortfolioAdvanceId del XML debe existir en Portfolio.PortfolioAdvance.; Debe existir al menos un registro en GeneralLedger.CompanySettings con OfficialCurrencyId definido.; El XML debe seguir la estructura <ListPortfolioAdvanceCrossing> con nodos Id, CrossingValue y EntityName.; La moneda de la CxC debe estar registrada en Common.Currency.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La precisión de redondeo siempre cae a 2 decimales si la moneda no define RoundingType.; Si la moneda del anticipo es NULL se asume la moneda oficial de CompanySettings.; EntityName por defecto es ''Invoice'' cuando no llega valor en el XML.; Cuando el saldo del anticipo convertido coincide con el cruce, se afecta el anticipo en su totalidad (Value=pa.Balance) evitando residuos por redondeo.; ValueInCurrencyInvoice siempre se almacena en la moneda de la CxC con el mismo redondeo del cruce.; El TransferType del comprobante generado es siempre 0 y Status siempre 2.; Si cualquier comprobante falla al persistirse, se interrumpe el procesamiento de los siguientes anticipos del lote.; El procesamiento de anticipos se hace secuencialmente en orden ascendente de PortfolioAdvanceId.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'anticipo de cartera; cuenta por cobrar; cruce de anticipos; traslado de anticipo; comprobante contable; moneda oficial; conversión de moneda; redondeo por moneda; facturación básica; centro de costo; cuenta contable principal; tercero', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Portfolio.PortfolioTransfer: Por cada anticipo del XML se construye un XML de comprobante y se invoca SP_SavePortfolioTransfer_Output, que crea el traslado con OperatingUnitId, ThirdParty, MainAccountId, CostCenterId del anticipo, fecha = Common.GETDATE(), Status=2 y TransferType=0.; [INSERT] Portfolio.PortfolioTransferDetail: Se inserta el detalle con AccountReceivableId, cuentas contables de AccountReceivableAccounting, Value calculado según moneda/redondeo, TRMValue=1 y ValueInCurrencyInvoice = ROUND(CrossingValue, InvoiceRoundPrecision).; [RETURN_RESULT] @CodeResult/@MessageResult: Devuelve CodeResult=0 y mensaje acumulado en éxito; 999 con mensaje del SP hijo si éste falla; 99 con ERROR_MESSAGE+línea si ocurre excepción.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EntityName del XML viene vacío ('''') → Se asigna NULL al EntityName del registro temporal else Se conserva el valor recibido; si EntityName = ''BasicBilling'' → Se concatena '' (Fact. Basica)'' al texto de Observations del comprobante else Observations queda solo como ''Traslado de anticipo modulo facturación''; si ISNULL(pa.CurrencyId, OfficialCurrencyId) = ar.CurrencyId (misma moneda anticipo y CxC) → Value = ROUND(CrossingValue, InvoiceRoundPrecision) sin conversión else Se aplica conversión de moneda mediante Common.CurrencyConverterByModule; si Con monedas distintas: ROUND(CrossingValue) = ROUND(CurrencyConverterByModule(pa.Balance, monedaAnticipo, monedaCxC)) → Value = pa.Balance (se afecta el anticipo en su totalidad) else Value = ROUND(CurrencyConverterByModule(CrossingValue, monedaCxC, monedaAnticipo)) — se convierte a moneda del anticipo; si @Code_Output <> 0 tras EXEC SP_SavePortfolioTransfer_Output → Se asigna CodeResult=999 con el mensaje devuelto y se hace RETURN abortando el resto del lote else Se acumula el mensaje y continúa con el siguiente anticipo; si ERROR en BEGIN CATCH → CodeResult=99 y MessageResult con ERROR_MESSAGE() + número de línea', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.SP_SavePortfolioTransfer_Output; Common.GetRoundPrecision; Common.GETDATE; Common.CurrencyConverterByModule', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Portfolio.AccountReceivable; Common.Currency; Portfolio.PortfolioAdvance; Portfolio.AccountReceivableAccounting', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioTransfer';
-- GO
