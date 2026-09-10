-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_GeneratePortfolioReclasification]
	@OperatingUnitId INT,
	@IdRadicateInvoice INT,
	@CodeUser VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @CodeRadicate VARCHAR(200),
			@RadicateDate DATETIME,
			@Status CHAR(1),
			@ConfirmDate DATETIME,
			------------------------------
			@Message VARCHAR(MAX)	
	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY

		SELECT 
			@CodeRadicate = RadicatedConsecutive, 
			@RadicateDate = DocumentDate,
			@Status = State,
			@ConfirmDate = ISNULL(ConfirmDate, DocumentDate)
		FROM Portfolio.RadicateInvoiceC 
		WHERE Id = @IdRadicateInvoice
		
		/************************************************ VALIDACIONES ***********************************************/
				
		IF @Status <> '1'
		BEGIN
			SELECT	'999' CodeMessage, 
					'La radicación se encuentra en estado ' + CASE @Status  
																WHEN '2' THEN 'Confirmado'
																WHEN '4' THEN 'Anulado'
																ELSE 'N/A'
															END MessageResult
			RETURN
		END

		IF EXISTS 
		(
			SELECT 1 
			FROM Portfolio.RadicateInvoiceD rid
			LEFT JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber
			WHERE RadicateInvoiceCId = @IdRadicateInvoice AND ar.Id IS NULL
		)
		BEGIN				
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + rid.InvoiceNumber
						FROM Portfolio.RadicateInvoiceD rid
						LEFT JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber
						WHERE RadicateInvoiceCId = @IdRadicateInvoice AND ar.Id IS NULL
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT '999' CodeMessage, 'No se encontró cuenta por cobrar para las facturas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') MessageResult
			RETURN
		END

		IF EXISTS 
		(
			SELECT 1 
			FROM Portfolio.RadicateInvoiceD 
			WHERE RadicateInvoiceCId = @IdRadicateInvoice 
			GROUP BY InvoiceNumber 
			HAVING COUNT(*) > 1
		)
		BEGIN 
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + InvoiceNumber
						FROM Portfolio.RadicateInvoiceD 
						WHERE RadicateInvoiceCId = @IdRadicateInvoice 
						GROUP BY InvoiceNumber HAVING COUNT(*) > 1
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
			SELECT '999' CodeMessage, 'No se logro guardar el radicado debido a que las siguientes facturas estan repetidas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') MessageResult
			RETURN
		END

		IF EXISTS 
		( 
			SELECT 1
			FROM Portfolio.RadicateInvoiceD rid 
			JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
			LEFT JOIN Portfolio.AccountReceivableAccounting ara ON ar.Id = ara.AccountReceivableId AND ar.AccountWithoutRadicateId = ara.MainAccountId
			WHERE rid.RadicateInvoiceCId = @IdRadicateInvoice AND ara.Id IS NULL
		)
		BEGIN			
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + rid.InvoiceNumber
						FROM Portfolio.RadicateInvoiceD rid 
						JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
						LEFT JOIN Portfolio.AccountReceivableAccounting ara ON ar.Id = ara.AccountReceivableId AND ar.AccountWithoutRadicateId = ara.MainAccountId
						WHERE rid.RadicateInvoiceCId = @IdRadicateInvoice AND ara.Id IS NULL
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
			SELECT '999' CodeMessage, 'No se encontro saldo en la cuenta sin radicar para las facturas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') MessageResult
			RETURN
		END

		IF EXISTS 
		( 
			SELECT 1
			FROM Portfolio.RadicateInvoiceD rid 
			JOIN
			(
				SELECT rid.InvoiceNumber
				FROM Portfolio.RadicateInvoiceC ri 
				JOIN Portfolio.RadicateInvoiceD rid ON ri.Id = rid.RadicateInvoiceCId
				WHERE ri.Id <> @IdRadicateInvoice AND ri.State = '2'
			) d ON rid.InvoiceNumber = d.InvoiceNumber
			WHERE rid.RadicateInvoiceCId = @IdRadicateInvoice AND rid.Devolution = 0
		)
		BEGIN			
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + rid.InvoiceNumber
						FROM Portfolio.RadicateInvoiceD rid 
						JOIN
						(
							SELECT rid.InvoiceNumber
							FROM Portfolio.RadicateInvoiceC ri 
							JOIN Portfolio.RadicateInvoiceD rid ON ri.Id = rid.RadicateInvoiceCId
							WHERE ri.Id <> @IdRadicateInvoice AND ri.State = '2'
						) d ON rid.InvoiceNumber = d.InvoiceNumber
						WHERE rid.RadicateInvoiceCId = @IdRadicateInvoice AND rid.Devolution = 0
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
			SELECT '999' CodeMessage, 'Las siguientes facturas ya se encuentran radicadas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') MessageResult
			RETURN
		END

		/***************************************** RECLASIFICAR LAS FACTURAS *****************************************/

		-- Se actualiza el saldo en el detalle de la radicación por el saldo actual de la factura
		UPDATE rid 
			SET rid.BalanceInvoice = ar.Balance
		FROM Portfolio.RadicateInvoiceD rid 
		JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
		WHERE rid.RadicateInvoiceCId = @IdRadicateInvoice

		-- Se actualiza el movimiento por notas del detalle de la radicación
		UPDATE rid 
			SET rid.CreditNoteValue = ars.CreditValue,
				rid.DebitNoteValue = ars.DebitValue
		FROM Portfolio.RadicateInvoiceD rid 
		JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
		JOIN Portfolio.AccountReceivableShare ars on ar.Id = ars.AccountReceivableId
		WHERE rid.RadicateInvoiceCId = @IdRadicateInvoice

		SELECT @Message = STUFF((
					SELECT ', ' + rid.InvoiceNumber
					FROM Portfolio.RadicateInvoiceD rid
					JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
					WHERE RadicateInvoiceCId = @IdRadicateInvoice
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

		
		DECLARE @CurrencyId INT
		DECLARE @MessageTabla  TABLE (	code INT,
										MessageOut VARCHAR(max))
		DECLARE currency_Cursor CURSOR FOR
			SELECT	ar.CurrencyId
			FROM Portfolio.RadicateInvoiceD rid
			JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
			WHERE rid.RadicateInvoiceCId = @IdRadicateInvoice AND rid.BalanceInvoice <> 0
			GROUP by ar.CurrencyId
		
		OPEN currency_Cursor
		FETCH NEXT FROM currency_Cursor
		INTO @CurrencyId
		WHILE @@FETCH_STATUS = 0  
		BEGIN 
			
		------------------------------
		DECLARE	@SubXml XML = NULL,
				@Code_Output INT = NULL,
				@Message_Output VARCHAR(MAX) = NULL
			
			SELECT @SubXml = CONVERT
					(
						XML, 
						(
							SELECT *
							FROM 
							(
								SELECT	0 Id,
										@OperatingUnitId OperatingUnitId,
										'' Code,
										@ConfirmDate DocumentDate,
										1 DocumentType,
										'Radicacion de Cuentas No. ' + @CodeRadicate + ' Facturas: ' + @Message Detail,
										2 Status,
										ri.Id EntityId,
										ri.RadicatedConsecutive EntityCode,
										'RadicateInvoiceC' EntityName,
										@CurrencyId as CurrencyId
								FROM Portfolio.RadicateInvoiceC ri
								WHERE ri.Id = @IdRadicateInvoice
							) PortfolioReclassification
							JOIN
							(
								SELECT	0 PortfolioReclassificationId,
										0 Id,
										ar.Id AccountReceivableId,
										ar.AccountWithoutRadicateId SourceAccountId,
										ar.AccountRadicateId TargetAccountId,
										rid.BalanceInvoice Value
								FROM Portfolio.RadicateInvoiceD rid
								JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
								WHERE rid.RadicateInvoiceCId = @IdRadicateInvoice AND rid.BalanceInvoice <> 0 and ar.CurrencyId = @CurrencyId
							) PortfolioReclassificationDetail ON PortfolioReclassification.Id = PortfolioReclassificationDetail.PortfolioReclassificationId
							For xml AUTO,TYPE, ELEMENTS
						)
					)

			EXEC [Portfolio].[SP_SavePortfolioReclassification_Output] @SubXml, @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL

			IF @Code_Output <> 0
			BEGIN
				SELECT '999' CodeMessage, ISNULL(@Message_Output, 'No se pudo generar la Reclasificación de Documentos de Cartera') MessageResult
				RETURN
			END
			
			INSERT into @MessageTabla VALUES(@Code_Output,@Message_Output)

		NEXT_ROW:
		FETCH NEXT FROM currency_Cursor   
		INTO @CurrencyId
		END
		CLOSE currency_Cursor;  
		DEALLOCATE currency_Cursor;
		
		SET @Message = 'Se guardó y confirmó la radicación de cuentas con consecutivo ' + @CodeRadicate

		SELECT @Message =  @Message + CHAR(13) + CHAR(10) + ISNULL(STRING_AGG(MessageOut,';'), '')
		FROM @MessageTabla

		/**************************************** ACTUALIZACION DE REGISTROS *****************************************/
		
		UPDATE ar 
			SET PortfolioStatus = 3 
		FROM Portfolio.RadicateInvoiceD rid 
		JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber -- AND ar.AccountReceivableType = 2
		WHERE rid.RadicateInvoiceCId = @IdRadicateInvoice

		UPDATE rid 
			SET State = 2, 
				RadicatedDate = [Common].[GETDATE](), 
				RadicatedNumber = @CodeRadicate 
		FROM Portfolio.RadicateInvoiceD rid
		WHERE RadicateInvoiceCId = @IdRadicateInvoice

		DECLARE @UserId INT

		SELECT @UserId = u.Id
		FROM Security.[User] u 
		WHERE u.UserCode = @CodeUser

		UPDATE ri
			SET ri.State = '2',
				ri.ConfirmDateSystem = [Common].[GETDATE](),
				ri.ConfirmUser = @UserId,
				ri.ModificationUser = @CodeUser,
				ri.ConfirmDate = @ConfirmDate
		FROM Portfolio.RadicateInvoiceC ri
		WHERE ri.Id = @IdRadicateInvoice

		/************************************************* RESULTADO *************************************************/

		SELECT	'0' CodeMessage, 
				CHAR(13) + CHAR(10) + ISNULL(@Message, '') MessageResult

	END TRY
	BEGIN CATCH

		IF CURSOR_STATUS('global','currency_Cursor') >= -1  BEGIN
		  IF CURSOR_STATUS('global','currency_Cursor') > -1 BEGIN
			CLOSE currency_Cursor
		  END
		 DEALLOCATE currency_Cursor
		END

		SELECT '999' CodeMessage, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que ejecuta la reclasificación contable de un radicado de facturas de cartera: toma un radicado de cobro pendiente (estado ''Generado''), valida que todas las facturas del detalle existan como cuentas por cobrar, que no estén duplicadas, que tengan saldo en la cuenta sin radicar y que no hayan sido ya confirmadas en otro radicado. Si todas las validaciones pasan, actualiza los saldos, notas crédito y débito en el detalle del radicado (RadicateInvoiceD) tomando los valores actuales de AccountReceivable y AccountReceivableShare, y reclasifica contablemente los movimientos en AccountReceivableAccounting, trasladando el saldo desde la cuenta ''sin radicar'' hacia la cuenta contable del radicado confirmado. Se usa en el proceso de cartera para oficializar el paso de una factura del estado ''pendiente de radicar'' al estado de ''radicada ante el pagador'' (EPS, aseguradora o empresa).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePortfolioReclasification';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePortfolioReclasification';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma una radicación de facturas de cartera, valida su consistencia, reclasifica contablemente los saldos de la cuenta sin radicar a la cuenta radicada por moneda y deja el radicado en estado confirmado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioReclasification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El radicado (RadicateInvoiceC) debe existir y estar en estado ''1'' (no confirmado ni anulado).; Toda factura del detalle debe tener una cuenta por cobrar asociada en Portfolio.AccountReceivable.; Las facturas del detalle no pueden estar repetidas dentro del mismo radicado.; Las facturas con AccountReceivableType=2 deben tener saldo en la cuenta sin radicar (registro en AccountReceivableAccounting cuyo MainAccountId coincide con AccountWithoutRadicateId).; Las facturas no marcadas como devolución no pueden estar ya radicadas y confirmadas (State=''2'') en otro RadicateInvoiceC.; El usuario @CodeUser debe existir en Security.User para resolver su Id.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioReclasification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan saldos sobre cuentas por cobrar con AccountReceivableType=2 al construir la reclasificación y al sincronizar saldos.; La reclasificación se segmenta por moneda: una llamada a SP_SavePortfolioReclassification_Output por cada CurrencyId distinto con saldo no nulo.; Las facturas con BalanceInvoice=0 no generan movimiento de reclasificación.; Las facturas marcadas como Devolution=1 quedan exentas de la validación de duplicidad contra otros radicados confirmados.; Un radicado solo puede confirmarse una vez (requiere State=''1'' al inicio y termina en State=''2'').; ConfirmDate del encabezado se preserva: si venía nulo se reemplaza por DocumentDate, no por la fecha del sistema.; El detalle XML enviado al SP de reclasificación mueve valor desde AccountWithoutRadicateId (origen) hacia AccountRadicateId (destino) por el monto BalanceInvoice.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioReclasification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Portfolio.RadicateInvoiceD: Antes de reclasificar, BalanceInvoice se sincroniza con el saldo actual (ar.Balance) de la cuenta por cobrar tipo 2 vinculada por InvoiceNumber.; [UPDATE] Portfolio.RadicateInvoiceD: CreditNoteValue y DebitNoteValue se actualizan con los valores (CreditValue/DebitValue) provenientes de AccountReceivableShare para la cuenta por cobrar tipo 2 asociada.; [UPDATE] Portfolio.RadicateInvoiceD: Al confirmar, cada detalle queda con State=2, RadicatedDate=[Common].[GETDATE]() y RadicatedNumber=consecutivo del radicado.; [UPDATE] Portfolio.AccountReceivable: Toda cuenta por cobrar cuya InvoiceNumber esté en el detalle del radicado pasa a PortfolioStatus=3 (radicada).; [UPDATE] Portfolio.RadicateInvoiceC: El encabezado del radicado se confirma: State=''2'', ConfirmDateSystem=[Common].[GETDATE](), ConfirmUser=Id del usuario, ModificationUser=@CodeUser y ConfirmDate=ConfirmDate previo (o DocumentDate si era null).; [RETURN_RESULT] Portfolio.SP_SavePortfolioReclassification_Output: Por cada CurrencyId distinto presente en el detalle con BalanceInvoice<>0, se invoca el SP para generar la reclasificación moviendo de SourceAccountId=AccountWithoutRadicateId a TargetAccountId=AccountRadicateId el valor BalanceInvoice.; [RETURN_RESULT] RESULT: Si alguna validación falla retorna CodeMessage=''999'' con el listado de facturas problemáticas; en éxito retorna CodeMessage=''0'' con mensaje de confirmación más los mensajes acumulados de las reclasificaciones por moneda.; [RETURN_RESULT] RESULT: Si SP_SavePortfolioReclassification_Output retorna Code_Output<>0 se aborta y devuelve CodeMessage=''999'' con su mensaje (o ''No se pudo generar la Reclasificación de Documentos de Cartera'').', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioReclasification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Estado del radicado <> ''1'' → Retorna error ''999'' indicando si está Confirmado (2), Anulado (4) o N/A. else Continúa con las validaciones de detalle.; si Existen facturas del detalle sin AccountReceivable asociado → Retorna ''999'' listando las facturas sin cuenta por cobrar.; si Hay InvoiceNumber duplicados en el detalle (COUNT(*)>1) → Retorna ''999'' listando las facturas repetidas.; si Para AccountReceivableType=2 no existe registro en AccountReceivableAccounting con MainAccountId=AccountWithoutRadicateId → Retorna ''999'' indicando que no hay saldo en la cuenta sin radicar.; si Existe otra RadicateInvoiceC en State=''2'' que ya contiene la misma InvoiceNumber y rid.Devolution=0 → Retorna ''999'' indicando facturas ya radicadas.; si Para cada CurrencyId con BalanceInvoice<>0 (cursor) → Construye XML de reclasificación y ejecuta SP_SavePortfolioReclassification_Output; si Code_Output<>0 aborta.; si Excepción capturada en BEGIN CATCH → Cierra y libera el cursor currency_Cursor si está abierto y retorna ''999'' con ERROR_MESSAGE() y ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioReclasification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.SP_SavePortfolioReclassification_Output; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioReclasification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioReclasification';
-- GO
