CREATE PROCEDURE [Billing].[SP_ReversePortfolioByBasicBillingId_Output]
	@OperatingUnitId INT,
	@BasicBillingId INT,
	@AnnulmentDate DATETIME,
	@ReversalReasonDescription VARCHAR(300),
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CompanyType TINYINT,
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--SELECT	@CodeResult = 0,
	--			@MessageResult = ''
	--			return
	/*************************************************** VARIABLES ***************************************************/

	DECLARE @InvoiceId Int,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@Id_Output INT

	/*****************************************************************************************************************/

	BEGIN TRY

		/*************************************************************************************************************/
		SELECT TOP 1 @InvoiceId = InvoiceId
		FROM Billing.BasicBilling WHERE Id = @BasicBillingId

		DECLARE @AccountReceivableRows INT = 1,
				@AccountReceivableId INT = 2147483647,
				@AccountReceivableCode VARCHAR(20),
				@AccountReceivableType TINYINT,
				-------------------------------
				@PortfolioTransferRows INT,
				@PortfolioTransferId INT

		WHILE @AccountReceivableRows > 0
		BEGIN
			SELECT TOP 1
				@AccountReceivableId = ar.Id,
				@AccountReceivableCode = ar.Code,
				@AccountReceivableType = ar.AccountReceivableType,
				-----------------------------
				@PortfolioTransferRows = 1,
				@PortfolioTransferId = 0
			FROM Portfolio.AccountReceivable ar
			WHERE ar.InvoiceId = @InvoiceId
				AND ar.Id < @AccountReceivableId
			ORDER BY ar.Id DESC

			SET @AccountReceivableRows = @@ROWCOUNT
			IF @AccountReceivableRows = 0 
			BEGIN
				BREAK
			END
			
			/*********************************************************************************************************/

			SET @Message_Output = CONCAT('Se reversó la Cuenta por Cobrar ', CASE @AccountReceivableType
					WHEN 4 THEN 'tipo Pagaré: '
					WHEN 6 THEN 'al Paciente: '
				END,  @AccountReceivableCode)
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)

			/*********************************************************************************************************/

			WHILE @PortfolioTransferRows > 0
			BEGIN
					SELECT TOP 1
						@PortfolioTransferId = pt.Id
					FROM Portfolio.AccountReceivable ar
					JOIN Portfolio.PortfolioTransferDetail ptd ON ar.Id = ptd.AccountReceivableId
					JOIN Portfolio.PortfolioTransfer pt ON ptd.PortfolioTrasferId = pt.Id
					WHERE ar.Id = @AccountReceivableId AND pt.Status = 2
						AND pt.Id > @PortfolioTransferId
					ORDER BY pt.Id
					
					SET @PortfolioTransferRows = @@ROWCOUNT
					IF @PortfolioTransferRows = 0 
					BEGIN
						BREAK
					END

					/*************************************************************************************************/

					SELECT @SubXml = CONVERT
					(
						XML, 
						(
							SELECT 
								PortfolioNote.*
							FROM 
							(
								SELECT 
									0 Id,
									'' Code,
									@AnnulmentDate NoteDate,
									'Reversión del cruce de Anticipo vs CxC: ' + pt.Code + ' por: ' + @ReversalReasonDescription Observations,
									1 Nature,
									5 NoteType,
									pt.OperatingUnitId OperatingUnitId,
									2 Status,
									@AnnulmentDate CreationDate,
									pt.Id PortfolioTransferId,
									'Added' ChangeTracker,
									ad.CurrencyId
								FROM Portfolio.PortfolioTransfer pt WITH(NOLOCK)
								JOIN Portfolio.PortfolioAdvance ad WITH(NOLOCK) on ad.Id =pt.PortfolioAdvanceId
								WHERE pt.Id = @PortfolioTransferId
							) PortfolioNote
							For xml AUTO,TYPE, ELEMENTS
						)
					)

					EXEC Portfolio.SP_SavePortfolioNote_Output @SubXml, @CodeUser, @CompanyType, @Code_Output OUT, @Message_Output OUT, NULL, NULL

					IF @Code_Output <> 0
					BEGIN
						SELECT	@CodeResult = 999,
								@MessageResult = ISNULL(@Message_Output, 'No se pudo reversar los cruces de anticipo asociados a la factura')
						RETURN
					END

					SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
				END
		END
		
		/************************************************* RESULTADO *************************************************/

		SELECT	@CodeResult = 0,
				@MessageResult = ISNULL(@Message, '')
	END TRY
	BEGIN CATCH
		SELECT	@CodeResult = 999,
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reversa o anulación de la cartera asociada a un documento de facturación básica (prefactura o factura). Dado el identificador de la facturación básica, ubica todas las cuentas por cobrar vinculadas (tipo pagaré o cuenta de paciente) y, por cada traslado de cartera activo relacionado, genera una nota de reversión del cruce anticipo vs. cuenta por cobrar invocando SP_SavePortfolioNote_Output. Se utiliza cuando una factura debe anularse y es necesario deshacer los cruces de anticipos y transferencias de cartera previamente aplicados, dejando trazabilidad del motivo de reversión y la fecha de anulación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReversePortfolioByBasicBillingId_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReversePortfolioByBasicBillingId_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reversa los cruces de anticipos contra cuentas por cobrar asociados a una facturación básica, generando notas de cartera de reversión por cada transferencia activa de cada CxC de la factura.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByBasicBillingId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El BasicBillingId debe existir en Billing.BasicBilling y tener un InvoiceId asociado.; Deben existir cuentas por cobrar en Portfolio.AccountReceivable vinculadas al InvoiceId para que haya algo que reversar.; Las transferencias de cartera a reversar deben estar en Status = 2.; Cada PortfolioTransfer a reversar debe tener un PortfolioAdvance asociado (PortfolioAdvanceId) para obtener la moneda.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByBasicBillingId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo procesa PortfolioTransfer cuyo Status = 2 (estado activo/aplicado).; La nota de reversión generada se crea siempre con Nature=1, NoteType=5, Status=2 y ChangeTracker=''Added''.; La fecha de la nota (NoteDate y CreationDate) coincide con la fecha de anulación recibida.; Las observaciones de la nota siempre incluyen el prefijo ''Reversión del cruce de Anticipo vs CxC: '' seguido del código de la transferencia y la razón de reversión.; La OperatingUnitId y CurrencyId de la nota se heredan de la transferencia y de su PortfolioAdvance asociado.; Si cualquier llamada interna falla, se retorna CodeResult=999 sin continuar con el resto de transferencias.; Recorre las CxC de la factura en orden descendente por Id y las transferencias en orden ascendente por Id.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByBasicBillingId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por Cobrar; Pagaré; Paciente; Anticipo de cartera; Cruce de anticipo vs CxC; Reversión de cartera; Nota de cartera; Transferencia de cartera; Factura', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByBasicBillingId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.AccountReceivable: Por cada CxC vinculada al InvoiceId de la facturación básica, agrega al mensaje ''Se reversó la Cuenta por Cobrar tipo Pagaré: <code>'' o ''al Paciente: <code>'' según AccountReceivableType (4 o 6).; [INSERT] Portfolio.PortfolioNote: Por cada PortfolioTransfer con Status=2 asociada a la CxC, construye XML con NoteType=5, Nature=1, Status=2, ChangeTracker=''Added'', NoteDate=@AnnulmentDate y observaciones ''Reversión del cruce de Anticipo vs CxC: <Code> por: <ReversalReasonDescription>'', y lo envía a Portfolio.SP_SavePortfolioNote_Output para persistir la nota.; [RETURN_RESULT] @CodeResult/@MessageResult: Si SP_SavePortfolioNote_Output retorna Code_Output<>0, devuelve CodeResult=999 con el mensaje del SP o ''No se pudo reversar los cruces de anticipo asociados a la factura''.; [RAISERROR] @CodeResult/@MessageResult: En caso de excepción, captura en CATCH y devuelve CodeResult=999 con ERROR_MESSAGE() y número de línea.; [RETURN_RESULT] @CodeResult/@MessageResult: Al finalizar sin errores, devuelve CodeResult=0 y un mensaje consolidado con todas las reversiones realizadas separadas por CRLF.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByBasicBillingId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AccountReceivable.AccountReceivableType = 4 → Mensaje describe la CxC reversada como ''tipo Pagaré''; si AccountReceivable.AccountReceivableType = 6 → Mensaje describe la CxC reversada como ''al Paciente''; si Code_Output devuelto por SP_SavePortfolioNote_Output <> 0 → Aborta con CodeResult=999 y mensaje del SP interno o ''No se pudo reversar los cruces de anticipo asociados a la factura'' else Continúa acumulando el mensaje y procesando siguientes transferencias; si No existen más AccountReceivable para la factura (ROWCOUNT=0) → Sale del loop principal y retorna éxito; si Solo se consideran PortfolioTransfer con Status = 2 → Únicamente esos traslados generan nota de reversión', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByBasicBillingId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.SP_SavePortfolioNote_Output', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByBasicBillingId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.BasicBilling; Portfolio.AccountReceivable; Portfolio.PortfolioTransferDetail; Portfolio.PortfolioTransfer; Portfolio.PortfolioAdvance', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByBasicBillingId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByBasicBillingId_Output';
-- GO
