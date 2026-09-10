-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-10
-- Description:	Reversa las cuentas por cobrar y los cruces de anticipos vs CxC
-- =============================================
CREATE PROCEDURE [Billing].[SP_ReversePortfolioByInvoiceId_Output]
	@OperatingUnitId INT,
	@InvoiceId INT,
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

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @LiquidationType TINYINT,
			@PortfolioAdvanceValue NUMERIC(18,2),
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@Id_Output INT

	/*****************************************************************************************************************/

	BEGIN TRY

		SELECT @LiquidationType = rcd.LiquidationType
		FROM Billing.Invoice i
		LEFT JOIN Billing.RevenueControlDetail rcd ON i.RevenueControlDetailId = rcd.Id
		WHERE i.Id = @InvoiceId

		IF EXISTS ( 	
			SELECT 1 
			FROM Billing.InvoicePortfolioAdvance ipa
			JOIN Billing.InvoiceEntityCapitated iec ON iec.InvoiceId = ipa.InvoiceId
			JOIN Portfolio.PortfolioAdvance pa ON pa.Id = ipa.PortfolioAdvanceId
			JOIN Treasury.CashReceiptDetails crd ON crd.Id = pa.CashReceiptDetailId
			JOIN Treasury.CashReceiptConcepts crc ON crc.Id = crd.IdCashReceiptConcept
			WHERE ipa.InvoiceId = @InvoiceId AND crc.IsFixedAmountInvoiceAdvance = 1
		) BEGIN

			SELECT @PortfolioAdvanceValue = SUM(ipa.Value) 
			FROM Billing.InvoicePortfolioAdvance ipa
			JOIN Billing.InvoiceEntityCapitated iec ON iec.InvoiceId = ipa.InvoiceId
			JOIN Portfolio.PortfolioAdvance pa ON pa.Id = ipa.PortfolioAdvanceId
			JOIN Treasury.CashReceiptDetails crd ON crd.Id = pa.CashReceiptDetailId
			JOIN Treasury.CashReceiptConcepts crc ON crc.Id = crd.IdCashReceiptConcept
			WHERE ipa.InvoiceId = @InvoiceId AND crc.IsFixedAmountInvoiceAdvance = 1
		END

		/*************************************************************************************************************/

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
			IF @LiquidationType = 1 OR @PortfolioAdvanceValue > 0
			BEGIN --Pago por Servicios	
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

			/*********************************************************************************************************/

			--- Actualizo la cabecera
			-- Se limpian los saldos de deterioro/provisión junto con el Balance para no violar
			-- CK_AccountReceivable / CK_AccountReceivable_1 / CK_AccountReceivable_ValidateDeteriorationBalance
			-- (Bug 39764 / IND708288: quedaban con deterioro > 0 mientras Balance pasaba a 0)
			UPDATE Portfolio.AccountReceivable
				SET Status = 3,
					Balance = 0,
					DeteriorationBalance = 0,
					DeteriorationBalanceCurrentYear = 0,
					DeteriorationBalancePreviousYear = 0,
					ProvisionBalance = 0,
					AnnulmentDate = @AnnulmentDate,
					AnnulmentUser = @CodeUser
			WHERE Id = @AccountReceivableId

			--- Actualizo las cuotas
			UPDATE Portfolio.AccountReceivableShare
				SET Balance = 0
			WHERE AccountReceivableId = @AccountReceivableId

			--- Actualizo las cuentas contables
			UPDATE Portfolio.AccountReceivableAccounting
				SET Balance = 0
			FROM Portfolio.AccountReceivable ar 
			WHERE AccountReceivableId = @AccountReceivableId

			/*********************************************************************************************************/

			EXEC [Portfolio].[SP_GenerateRecognitionModificationByAccountReceivableId_Output] @AccountReceivableId, @CodeUser, @Code_Output OUT, @Message_Output OUT

			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = ISNULL(@Message_Output, 'No se pudo realizar la modificación del reconocimiento asociado a la factura')
				RETURN
			END

			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reversa las cuentas por cobrar y los cruces de anticipos contra cuentas por cobrar (CxC) asociados a una factura específica, permitiendo anular la cartera generada al momento de la facturación. Recorre todas las cuentas por cobrar vinculadas a la factura (tipo pagaré o paciente), deshace los cruces de anticipos de cartera que hayan sido aplicados contra esa factura (incluyendo anticipos de facturas de capitación con monto fijo), y actualiza el estado de cada cuenta por cobrar y sus distribuciones. Se usa en el proceso de anulación de facturas de cobro —tanto de pago por servicios como de capitación— para garantizar que los saldos de anticipos, recibos de caja y cuentas por cobrar queden consistentes tras una reversión contable o administrativa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReversePortfolioByInvoiceId_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReversePortfolioByInvoiceId_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reversa todas las cuentas por cobrar asociadas a una factura, anula los cruces de anticipos vs CxC mediante notas de cartera y regenera la modificación del reconocimiento contable correspondiente.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByInvoiceId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe existir en Billing.Invoice para poder obtener su LiquidationType vía RevenueControlDetail.; Deben existir cuentas por cobrar en Portfolio.AccountReceivable asociadas al InvoiceId; si no, el ciclo principal no ejecuta acciones.; Para reversar cruces de anticipo, las PortfolioTransfer asociadas deben estar en Status = 2 (activas/aplicadas).; Se requiere un usuario (@CodeUser) y fecha de anulación (@AnnulmentDate) válidos para registrar la reversión.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByInvoiceId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda CxC reversada queda con Status=3 y Balance=0, junto con sus cuotas y cuentas contables en Balance=0.; La reversión recorre las CxC de la factura en orden descendente de Id (de la más reciente a la más antigua).; Solo se generan notas de reversión por PortfolioTransfer cuyo Status=2.; Las notas de cartera generadas se crean siempre con NoteType=5, Nature=1, Status=2 y ChangeTracker=''Added''.; Si falla cualquier paso intermedio, no se confirma resultado exitoso: el procedimiento retorna 999 manteniendo el cambio parcial dentro del flujo (no hay transacción explícita).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByInvoiceId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por cobrar (CxC); Anticipo de cartera; Cruce de anticipo vs CxC; Nota de cartera (reversión); Pagaré; Paciente; Factura de capitación; Recibo de caja / concepto de recaudo; Tipo de liquidación (Pago por Servicios); Reconocimiento contable de cartera; Transferencia de cartera', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByInvoiceId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Portfolio.AccountReceivable: Por cada CxC asociada a la factura: marca Status=3 (anulada), Balance=0, registra AnnulmentDate y AnnulmentUser con los valores recibidos.; [UPDATE] Portfolio.AccountReceivableShare: Para cada CxC anulada, pone Balance=0 en todas sus cuotas (AccountReceivableShare) asociadas.; [UPDATE] Portfolio.AccountReceivableAccounting: Para cada CxC anulada, pone Balance=0 en sus registros contables (AccountReceivableAccounting).; [INSERT] Portfolio.PortfolioNote: Cuando LiquidationType=1 o existe valor de anticipo cruzado (>0), por cada PortfolioTransfer en Status=2 vinculada a la CxC se invoca SP_SavePortfolioNote_Output con NoteType=5, Nature=1, Status=2, ChangeTracker=''Added'' y observación ''Reversión del cruce de Anticipo vs CxC: <code> por: <razón>''.; [RETURN_RESULT] @MessageResult: Si cualquier sub-EXEC (SP_SavePortfolioNote_Output o SP_GenerateRecognitionModificationByAccountReceivableId_Output) devuelve código distinto de 0, retorna CodeResult=999 con el mensaje del sub-proceso o uno por defecto y termina.; [RETURN_RESULT] @MessageResult: En éxito retorna CodeResult=0 y un mensaje acumulado por cada CxC reversada con leyenda ''Se reversó la Cuenta por Cobrar tipo Pagaré: <code>'' (AccountReceivableType=4) o ''al Paciente: <code>'' (AccountReceivableType=6).; [RAISERROR] @MessageResult: En CATCH devuelve CodeResult=999 con ERROR_MESSAGE() y el número de línea, sin propagar excepción.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByInvoiceId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen InvoicePortfolioAdvance ligados a InvoiceEntityCapitated cuyo concepto de recibo de caja tiene IsFixedAmountInvoiceAdvance=1 → Calcula @PortfolioAdvanceValue como suma de ipa.Value de esos anticipos, habilitando la rama de reversión de cruces. else @PortfolioAdvanceValue queda NULL y la reversión de cruces solo se ejecuta si LiquidationType=1.; si @LiquidationType = 1 OR @PortfolioAdvanceValue > 0 (pago por servicios o factura con anticipo de monto fijo) → Itera todas las PortfolioTransfer en Status=2 de la CxC y genera notas de cartera de reversión vía Portfolio.SP_SavePortfolioNote_Output. else Omite la generación de notas de cartera y procede directamente a anular la CxC.; si AccountReceivableType del registro recorrido → Si =4 etiqueta el mensaje como ''tipo Pagaré''; si =6 etiqueta como ''al Paciente'' al construir el mensaje de salida.; si @Code_Output <> 0 tras llamar SP_SavePortfolioNote_Output o SP_GenerateRecognitionModificationByAccountReceivableId_Output → Asigna CodeResult=999, propaga el mensaje del sub-proceso y hace RETURN inmediato abortando la reversión.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByInvoiceId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.SP_SavePortfolioNote_Output; Portfolio.SP_GenerateRecognitionModificationByAccountReceivableId_Output', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByInvoiceId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.RevenueControlDetail; Billing.InvoicePortfolioAdvance; Billing.InvoiceEntityCapitated; Portfolio.PortfolioAdvance; Treasury.CashReceiptDetails; Treasury.CashReceiptConcepts; Portfolio.AccountReceivable; Portfolio.PortfolioTransferDetail; Portfolio.PortfolioTransfer', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByInvoiceId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByInvoiceId_Output';
-- GO
