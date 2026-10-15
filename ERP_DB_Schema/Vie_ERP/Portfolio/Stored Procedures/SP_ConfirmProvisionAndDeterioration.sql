-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 03/12/2015
-- Description:	Procedimiento que se encarga de confirmar la provision y deterioro
-- Modify: Diego Andrés Roldán Lozano
-- Modify date:	2017-10-20
-- =============================================

CREATE PROCEDURE [Portfolio].[SP_ConfirmProvisionAndDeterioration] 
	@PortfolioProvisionId as int,
	@CodeUser as varchar(20),
	@OperativeUnitId  as int
AS
BEGIN
	
	/******************************************* VARIABLES *******************************************/

	DECLARE @Code as varchar(20),
			@DocumentDate as date,
			@CourtDate as date,
			@Process as int,
			@ApplyDeterioration int,
			@OperatingUnitId as int

	--Tabla para almacenar los items del listado que viene en el xml
	DECLARE @TableXmlObject TABLE
	(
		PortfolioProvisionDetailId int, 
		AccountReceivableId int, 
		InvoiceNumber varchar(50), 
		[Value] numeric(18,2), 
		BalanceAccountReceivable numeric(18,2),
		ValueGlosado numeric(18,2),
		AccumulatedDeterioration numeric(18,2),
		DeteriorationBalanceCurrentYear numeric(18,2),
		DeteriorationBalancePreviousYear numeric(18,2),
		CurrentDeteriorationYear numeric(18,2),
		InvoiceDocumentType tinyint,
		LegalBookId int
	)

	--Se declara una tabla con los datos para la cabecera del comprobante contable 
	DECLARE @JournalVourcherTmp TABLE 
	(
		Id integer,
		Consecutive bigint,
		LegalBookId integer,
		IdJournalVoucher integer,
		VoucherDate varchar(30),
		Imported varchar(5),
		[Status] tinyint,
		Detail varchar(500),
		EntityCode varchar(20),
		EntityId integer,
		EntityName varchar(250),
		IsClosedYear tinyint
	)

	--Se declara una tabla temporal para los detalles del comprobante
	DECLARE @JournalVourcherDetailTmp TABLE 
	(
		Id integer DEFAULT(0),
		IdAccounting integer DEFAULT(0),
		IdMainAccount integer,
		IdThirdParty integer,
		IdCostCenter integer,
		DebitValue decimal(18,2),
		CreditValue decimal(18,2),
		Detail varchar(500),
		IdRetention integer,
		RetentionRate decimal(5,2) DEFAULT(0),
		BaseValue decimal(18,0) DEFAULT(0),
		BillingValue decimal(18,0) DEFAULT(0)
	)  
		
	DECLARE --Libro oficial
			@LegalBookId integer,
			--Tipo de comprobante contable
			@IdJournalVoucher as int,
			--Variable para obtener el total de mensajes de la tabla temporal de errores
			@errors varchar(max),
			--Variables para cuentas de deterioro de facturación básica
			@BasicBillingDebitAccount int,
			@BasicBillingCreditAccount int,
			@BasicBillingReversalAccount int,
			@BasicBillingPreviousPeriodReversalAccount int,
			--Variables para manejar múltiples libros cuando ApplyDeterioration = 3
			@CurrentLegalBookId INT,
			@AllConsecutives VARCHAR(MAX),
			@CurrentConsecutive VARCHAR(30),
			--Variables para el proceso de comprobantes contables
			@JournalVoucherId INT,
			@Consecutive VARCHAR(MAX),
			@Message VARCHAR(MAX),
			@errorJV VARCHAR(MAX),
			@JournalVoucherXML XML

	--Tabla para ir almacenando los errores 
	DECLARE @TableErrors as TABLE([message] varchar(300))
	
	--Tabla temporal para guardar el resultado del save del comprobante contable
	DECLARE @resultJournalVoucher TABLE (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)
	
	/******************************************* ASIGNACIONES *******************************************/

	BEGIN TRY
		
		SELECT 
			@Code = pp.Code,
			@DocumentDate = pp.DocumentDate,
			@CourtDate = pp.CourtDate,
			@Process = pp.DocumentType,
			@ApplyDeterioration = pp.ApplyDeterioration,
			@OperatingUnitId = pp.OperatingUnitId
		FROM Portfolio.PortfolioProvision pp
		WHERE pp.Id = @PortfolioProvisionId

		--Si es deterioro con ApplyDeterioration = 3, cargar cuentas de facturación básica
		IF @Process = 2 AND @ApplyDeterioration = 3
		BEGIN
			SELECT 
				@BasicBillingDebitAccount = dbbp.DebitDeteriorationAccount,
				@BasicBillingCreditAccount = dbbp.CreditDeteriorationAccount,
				@BasicBillingReversalAccount = dbbp.ReversalDeteriorationAccount,
				@BasicBillingPreviousPeriodReversalAccount = dbbp.PreviousPeriodReversalAccount
			FROM Portfolio.DeteriorationBasicBillingPortfolio dbbp
			JOIN Portfolio.SettingPortfolio sp ON sp.Id = dbbp.SettingPortfolioId
			WHERE sp.OperatingUnitId = @OperativeUnitId
		END

		--Se obtienen los items del xml
		INSERT INTO @TableXmlObject
			(
				PortfolioProvisionDetailId, 
				AccountReceivableId, 
				InvoiceNumber, 
				[Value], 
				BalanceAccountReceivable, 
				ValueGlosado, 
				AccumulatedDeterioration,
				DeteriorationBalanceCurrentYear,
				DeteriorationBalancePreviousYear,
				CurrentDeteriorationYear,
				InvoiceDocumentType,
				LegalBookId
			)
			SELECT 
				ppd.Id, 
				ppd.AccountReceivableId, 
				ppd.InvoiceNumber, 
				ppd.[Value], 
				ppd.BalanceAccountReceivable, 
				ppd.ValueGlosado, 
				ppd.AccumulatedDeterioration, 
				ppd.DeteriorationBalanceCurrentYear,
				ppd.DeteriorationBalancePreviousYear,
				ppd.CurrentDeteriorationYear,
				ISNULL(inv.DocumentType, 0),
				ppd.LegalBookId
			FROM Portfolio.PortfolioProvisionDetail ppd
			JOIN Portfolio.AccountReceivable ar ON ar.Id = ppd.AccountReceivableId
			LEFT JOIN Billing.Invoice inv ON inv.Id = ar.InvoiceId
			WHERE ppd.PortfolioProvisionId = @PortfolioProvisionId

	/******************************************* VALIDACIONES *******************************************/
		
		IF EXISTS (SELECT 1 FROM Portfolio.PortfolioProvision pp WHERE pp.Id = @PortfolioProvisionId AND pp.Status <> 1)
		BEGIN
			SELECT	999 as CodeMessage, 
					'La Modificación de Obligación se encuentra en estado: ' + IIF(pp.Status = 2, 'Confirmado', 'Anulado') as Message, 
					'' AS Consecutive
			FROM Portfolio.PortfolioProvision pp 
			WHERE pp.Id = @PortfolioProvisionId
			RETURN
		END

		--Se valida que exista parámetros con la unidad operativa escogida
		IF NOT EXISTS (SELECT sp.Id FROM Portfolio.SettingPortfolio sp WHERE OperatingUnitId = @OperatingUnitId)
		BEGIN
			SELECT 999 AS CodeMessage, 'No existe parámetros de cartera para la unidad operativa escogida' AS Message, '' AS Consecutive
			RETURN 
		END

		--Se valida que las facturas tengan diligenciado los campos necesarios para las cuentas de provision o deterioro dependiendo del proceso
		IF @Process = 1 --Provisión
		BEGIN
			--La cuenta Credito para la provisión
			INSERT INTO @TableErrors
				SELECT 'La factura ' + tXml.InvoiceNumber + ' con código(CxC) ' + ar.Code + ' no tiene diligenciado el campo CreditProvisionAccountId' + CHAR(13) + CHAR(10)
				FROM @TableXmlObject tXml
				JOIN Portfolio.AccountReceivable ar on ar.Id = tXml.AccountReceivableId
				WHERE ar.CreditProvisionAccountId IS NULL

			--La cuenta Debito para la provisión
			INSERT INTO @TableErrors
				SELECT 'La factura ' + tXml.InvoiceNumber + ' con código(CxC) ' + ar.Code + ' no tiene diligenciado el campo DebitProvisionAccountId' + CHAR(13) + CHAR(10)
				FROM @TableXmlObject tXml
				JOIN Portfolio.AccountReceivable ar on ar.Id = tXml.AccountReceivableId
				WHERE ar.DebitProvisionAccountId IS NULL

			IF NOT EXISTS (SELECT 1 FROM @TableErrors)
			BEGIN
				--Se valida que el valor a provisionar no sea mayor al saldo de la factura
				INSERT INTO @TableErrors
					SELECT 'El saldo de la factura ' + tXml.InvoiceNumber + ' con código(CxC) ' + ar.Code + ' es menor al valor a Provisionar' + CHAR(13) + CHAR(10)
					FROM @TableXmlObject tXml
					JOIN Portfolio.AccountReceivable ar on ar.Id = tXml.AccountReceivableId
					WHERE tXml.Value > ar.Balance

				IF NOT EXISTS (SELECT 1 FROM @TableErrors)
				BEGIN
					--Se valida que la sumatoria del valor de provision con balanceProvision no debe superar el saldo
					--Se valida que Value + BalanceProvision no sea mayor al saldo de la factura
					INSERT INTO @TableErrors
						SELECT 'El saldo de la factura ' + tXml.InvoiceNumber + ' con código(CxC) ' + ar.Code + ' es menor al Valor Provisión + Saldo Provisión' + CHAR(13) + CHAR(10)
						FROM @TableXmlObject tXml
						JOIN Portfolio.AccountReceivable ar on ar.Id = tXml.AccountReceivableId
						WHERE (tXml.[Value] + ar.ProvisionBalance) > ar.Balance
				END
			END
		END
		ELSE IF @Process = 2 --Deterioro
		BEGIN 
			--Si es ApplyDeterioration = 3, validar cuentas de facturación básica para DocumentType 6 y 7
			IF @ApplyDeterioration = 3
			BEGIN
				--Validar que existan facturas con DocumentType 6 o 7 y que las cuentas básicas estén parametrizadas
				IF EXISTS (SELECT 1 FROM @TableXmlObject WHERE InvoiceDocumentType IN (6, 7))
				BEGIN
					IF @BasicBillingDebitAccount IS NULL OR @BasicBillingCreditAccount IS NULL 
						OR @BasicBillingReversalAccount IS NULL OR @BasicBillingPreviousPeriodReversalAccount IS NULL
					BEGIN
						INSERT INTO @TableErrors
							SELECT 'No se tienen parametrizadas las cuentas para la contabilización de las facturas básicas en la Unidad Operativa ' + 
								   CONCAT(ou.UnitCode, ' - ', ou.UnitName)
							FROM Common.OperatingUnit ou
							WHERE ou.Id = @OperativeUnitId
					END
				END
			END

			--La cuenta Credito para el deterioro
			--Solo se excluyen facturas tipo 6 y 7 cuando ApplyDeterioration = 3 (usan cuentas básicas)
			--Las demás facturas siempre deben validar sus cuentas de AccountReceivable
			INSERT INTO @TableErrors
				SELECT 'La factura ' + tXml.InvoiceNumber + ' con código(CxC) ' + ar.Code + ' no tiene diligenciado el campo CreditAccountDeteriorationId' + CHAR(13) + CHAR(10)
				FROM @TableXmlObject tXml
				JOIN Portfolio.AccountReceivable ar on ar.Id = tXml.AccountReceivableId
				WHERE ar.CreditAccountDeteriorationId IS NULL
					AND tXml.InvoiceDocumentType NOT IN (6, 7)

			--La cuenta Debito para el deterioro
			--Solo se excluyen facturas tipo 6 y 7 cuando ApplyDeterioration = 3 (usan cuentas básicas)
			--Las demás facturas siempre deben validar sus cuentas de AccountReceivable
			INSERT INTO @TableErrors
				SELECT 'La factura ' + tXml.InvoiceNumber + ' con código(CxC) ' + ar.Code + ' no tiene diligenciado el campo DebitAccountDeteriorationId' + CHAR(13) + CHAR(10)
				FROM @TableXmlObject tXml
				JOIN Portfolio.AccountReceivable ar on ar.Id = tXml.AccountReceivableId
				WHERE ar.DebitAccountDeteriorationId IS NULL
					AND tXml.InvoiceDocumentType NOT IN (6, 7)

			--La cuenta para la reversión del deterioro
			--Solo se excluyen facturas tipo 6 y 7 cuando ApplyDeterioration = 3 (usan cuentas básicas)
			--Las demás facturas siempre deben validar sus cuentas de AccountReceivable
			INSERT INTO @TableErrors
				SELECT 'La factura ' + tXml.InvoiceNumber + ' con código(CxC) ' + ar.Code + ' no tiene diligenciado el campo ReversalAccountDeteriorationId' + CHAR(13) + CHAR(10)
				FROM @TableXmlObject tXml
				JOIN Portfolio.AccountReceivable ar on ar.Id = tXml.AccountReceivableId
				WHERE ar.ReversalAccountDeteriorationId IS NULL
					AND tXml.InvoiceDocumentType NOT IN (6, 7)

			--La cuenta para la reversión del deterioro de años anteriores
			--Solo se excluyen facturas tipo 6 y 7 cuando ApplyDeterioration = 3 (usan cuentas básicas)
			--Las demás facturas siempre deben validar sus cuentas de AccountReceivable
			INSERT INTO @TableErrors
				SELECT 'La factura ' + tXml.InvoiceNumber + ' con código(CxC) ' + ar.Code + ' no tiene diligenciado el campo PreviousPeriodReversalAccountDeteriorationId' + CHAR(13) + CHAR(10)
				FROM @TableXmlObject tXml
				JOIN Portfolio.AccountReceivable ar on ar.Id = tXml.AccountReceivableId
				WHERE ar.PreviousPeriodReversalAccountDeteriorationId IS NULL
					AND tXml.InvoiceDocumentType NOT IN (6, 7)

			--Si no hay errores valido datos del deterioro
			IF NOT EXISTS (SELECT 1 FROM @TableErrors)
			BEGIN
				--Validamos que no se haya hecho deterioro a las mismas facturas para el mismo mes o posteriores
				INSERT INTO @TableErrors 
					SELECT 'Ya se ha generado deterioro para la factura ' + ppd.InvoiceNumber + ' con código(CxC) ' + ar.Code + ' en el mes ' + cast(month(pp.CourtDate) as varchar) + 
							' - año ' + cast(year(pp.CourtDate) as varchar) + CHAR(13) + CHAR(10)
					FROM @TableXmlObject tXml
					JOIN Portfolio.PortfolioProvisionDetail ppd ON tXml.AccountReceivableId = ppd.AccountReceivableId
					JOIN Portfolio.PortfolioProvision pp ON ppd.PortfolioProvisionId = pp.Id
					JOIN Portfolio.AccountReceivable ar on tXml.AccountReceivableId = ar.Id
					WHERE pp.[Status] = 2 AND pp.DocumentType = 2
						AND pp.Id <> @PortfolioProvisionId					
						AND (
								(YEAR(pp.CourtDate) = YEAR(@CourtDate) And MONTH(pp.CourtDate) >= MONTH(@CourtDate))
								OR
								(YEAR(pp.CourtDate) > YEAR(@CourtDate))
							)

				IF NOT EXISTS (SELECT 1 FROM @TableErrors)
				BEGIN
					--Valida que el saldo que se envía coincida con el saldo que debería ser debido a que la factura pudo haber tenido mas movimientos
					INSERT INTO @TableErrors 
						SELECT 'El saldo de la factura ' + tXml.InvoiceNumber + ' ha cambiado, por favor actualicela ' + CHAR(13) + CHAR(10)
						FROM @TableXmlObject tXml 
						JOIN Portfolio.AccountReceivable ar ON tXml.AccountReceivableId = ar.Id
						WHERE ar.Balance <> tXml.BalanceAccountReceivable

					IF NOT EXISTS (SELECT 1 FROM @TableErrors)
					BEGIN						
						--Valida que el balance del deterioro que se envía coincida con el balance del deterioro actual de la cuenta por cobrar
						INSERT INTO @TableErrors 
							SELECT 'El deterioro acumulado de la factura ' + tXml.InvoiceNumber + ' ha cambiado, por favor actualicela ' + CHAR(13) + CHAR(10)
							FROM @TableXmlObject tXml 
							JOIN Portfolio.AccountReceivable ar ON tXml.AccountReceivableId = ar.Id
							WHERE ar.DeteriorationBalance <> tXml.AccumulatedDeterioration

						IF NOT EXISTS (SELECT 1 FROM @TableErrors)
						BEGIN
							--Valida que el balance del deterioro del año actual que se envía coincida con el balance del deterioro del año actual de la cuenta por cobrar
							INSERT INTO @TableErrors 
								SELECT 'El deterioro acumulado en el año de la factura ' + tXml.InvoiceNumber + ' ha cambiado, por favor actualicela ' + CHAR(13) + CHAR(10)
								FROM @TableXmlObject tXml 
								JOIN Portfolio.AccountReceivable ar ON tXml.AccountReceivableId = ar.Id
								WHERE ar.DeteriorationBalanceCurrentYear <> tXml.DeteriorationBalanceCurrentYear

							IF NOT EXISTS (SELECT 1 FROM @TableErrors)
							BEGIN
								--Valida que el balance del deterioro de años anteriores que se envía coincida con el balance del deterioro de años anteriores de la cuenta por cobrar
								INSERT INTO @TableErrors 
									SELECT 'El deterioro acumulado de años anteriores de la factura ' + tXml.InvoiceNumber + ' ha cambiado, por favor actualicela ' + CHAR(13) + CHAR(10)
									FROM @TableXmlObject tXml 
									JOIN Portfolio.AccountReceivable ar ON tXml.AccountReceivableId = ar.Id
									WHERE ar.DeteriorationBalancePreviousYear <> tXml.DeteriorationBalancePreviousYear

								IF NOT EXISTS (SELECT 1 FROM @TableErrors)
								BEGIN
									--Valida que el año del ultimo deterioro que se envía coincida con el año del ultimo deterioro de la cuenta por cobrar
									INSERT INTO @TableErrors 
										SELECT 'El año del ultimo deterioro de la factura ' + tXml.InvoiceNumber + ' ha cambiado, por favor actualicela ' + CHAR(13) + CHAR(10)
										FROM @TableXmlObject tXml 
										JOIN Portfolio.AccountReceivable ar ON tXml.AccountReceivableId = ar.Id
										WHERE ar.CurrentDeteriorationYear <> tXml.CurrentDeteriorationYear

									IF NOT EXISTS (SELECT 1 FROM @TableErrors)
									BEGIN
										--Se valida que el valor a deteriorar no sea mayor al saldo de la factura
										INSERT INTO @TableErrors
											SELECT 'El saldo de la factura ' + tXml.InvoiceNumber + ' con código(CxC) ' + ar.Code + ' es menor al valor a Deteriorar' + CHAR(13) + CHAR(10)
											FROM @TableXmlObject tXml 
											JOIN Portfolio.AccountReceivable ar ON tXml.AccountReceivableId = ar.Id
											WHERE tXml.Value > ar.Balance
									END
								END
							END
						END
					END
				END
			END
		END

		--Si existen errores de los campos retorno el mensaje
		IF (SELECT COUNT(*) FROM @TableErrors) > 0
		BEGIN
			SELECT @errors = COALESCE(@errors + '', '') + [message] FROM @TableErrors
			SELECT 999 as CodeMessage, @errors as Message, '' as Consecutive
			RETURN 
		END

	/******************************************* CONTABILIZACIONES *******************************************/

		--Se obtiene el tipo de comprobante contable dependiendo del proceso, provisión o deterioro
		SELECT @IdJournalVoucher = CASE @Process 
										WHEN 1 THEN JournalVoucherTypeProvisionId 
										WHEN 2 THEN JournalVoucherTypeDeteriorationAccountId 
									END
		FROM Portfolio.SettingPortfolio 
		WHERE OperatingUnitId = @OperatingUnitId

		--Obtengo el libro oficial
		SELECT @LegalBookId = Id  FROM GeneralLedger.LegalBook WHERE OfficialBook = 1

		DECLARE @EntityId INT = @PortfolioProvisionId,
				@EntityCode VARCHAR(20) = @Code,
				@EntityName VARCHAR(250) = 'PortfolioProvisionAndDeterioration'
		
		--Inicializar variable para acumular consecutivos
		SET @AllConsecutives = ''

		--Si ApplyDeterioration = 3, procesar por cada LegalBookId
		IF @ApplyDeterioration = 3
		BEGIN
			--Cursor para iterar sobre cada LegalBookId
			DECLARE LegalBookCursor CURSOR FOR
				SELECT DISTINCT LegalBookId 
				FROM @TableXmlObject
				WHERE LegalBookId IS NOT NULL
			
			OPEN LegalBookCursor
			FETCH NEXT FROM LegalBookCursor INTO @CurrentLegalBookId

			WHILE @@FETCH_STATUS = 0
			BEGIN
				--Limpiar tablas temporales
				DELETE FROM @JournalVourcherTmp
				DELETE FROM @JournalVourcherDetailTmp
				
				--Inserto la cabecera del comprobante contable para este libro
				INSERT INTO @JournalVourcherTmp
					(Id,Consecutive,LegalBookId,IdJournalVoucher,VoucherDate,Imported,[Status],Detail,EntityCode, EntityId,EntityName,IsClosedYear)
				VALUES (0,0,@CurrentLegalBookId,@IdJournalVoucher,@DocumentDate,'False',2,'Comprobante contable generado desde Provisión/Deterioro de Cartera', @EntityCode, @EntityId, @EntityName,0)
		
				--Se insertan los detalles del comprobante contable débito
				INSERT INTO @JournalVourcherDetailTmp 
					(
						IdMainAccount, 
						IdThirdParty, 
						IdCostCenter, 
						DebitValue, 
						CreditValue, 
						Detail
					)
					SELECT
						IdMainAccount, 
						IdThirdParty, 
						IdCostCenter, 
						SUM(DebitValue) DebitValue, 
						SUM(CreditValue) CreditValue, 
						'' Detail
					FROM
					(
						SELECT 
							ma.Id IdMainAccount,
							CASE ma.HandlesThirdParty WHEN 1 THEN ar.ThirdPartyId ELSE NULL END as IdThirdParty ,
							CASE ma.HandlesCostCenter WHEN 1 THEN ar.CostCenterId ELSE NULL END as IdCostCenter,
							CASE @Process
								WHEN 1 THEN tXml.Value 
								WHEN 2 THEN ABS(tXml.Value - tXml.AccumulatedDeterioration)
							END DebitValue,
							0 CreditValue
						FROM @TableXmlObject tXml
						JOIN Portfolio.AccountReceivable ar ON tXml.AccountReceivableId = ar.Id
						JOIN GeneralLedger.MainAccounts ma ON ma.Id = CASE @Process 
														WHEN 1 THEN ar.DebitProvisionAccountId
														WHEN 2 THEN 
															CASE
																--Usar cuentas de facturación básica cuando DocumentType es 6 o 7
																WHEN @ApplyDeterioration = 3 AND tXml.InvoiceDocumentType IN (6, 7) THEN
																	CASE
																		WHEN tXml.Value > tXml.AccumulatedDeterioration THEN @BasicBillingDebitAccount
																		WHEN tXml.Value < tXml.AccumulatedDeterioration THEN @BasicBillingCreditAccount
																	END
																--Usar cuentas de AccountReceivable para otros casos
																WHEN tXml.Value > tXml.AccumulatedDeterioration THEN ar.DebitAccountDeteriorationId
																WHEN tXml.Value < tXml.AccumulatedDeterioration THEN ar.CreditAccountDeteriorationId
															END
													  END
						WHERE ar.Id = tXml.AccountReceivableId
							AND tXml.LegalBookId = @CurrentLegalBookId
					) jvd
					GROUP BY IdMainAccount, IdThirdParty, IdCostCenter
					HAVING SUM(DebitValue - CreditValue) <> 0
		
				--Se insertan los detalles del comprobante contable crédito
				INSERT INTO  @JournalVourcherDetailTmp
					(
						IdMainAccount, 
						IdThirdParty, 
						IdCostCenter, 
						DebitValue, 
						CreditValue, 
						Detail
					)
					SELECT
						IdMainAccount, 
						IdThirdParty, 
						IdCostCenter, 
						SUM(DebitValue) DebitValue, 
						SUM(CreditValue) CreditValue, 
						'' Detail
					FROM
					(
						SELECT 
							ma.Id IdMainAccount,
							CASE ma.HandlesThirdParty WHEN 1 THEN ar.ThirdPartyId ELSE NULL END as IdThirdParty ,
							CASE ma.HandlesCostCenter WHEN 1 THEN ar.CostCenterId ELSE NULL END as IdCostCenter,
							0 DebitValue,
							CASE tXml.Type 
								-- Provision
								WHEN 1 THEN tXml.Value 
								--Deterioro
								WHEN 2 THEN tXml.Value - tXml.AccumulatedDeterioration
								-- Reversion deterioro año actual
								WHEN 3 THEN IIF((tXml.AccumulatedDeterioration - tXml.Value) > tXml.DeteriorationBalanceCurrentYear, tXml.DeteriorationBalanceCurrentYear, (tXml.AccumulatedDeterioration - tXml.Value))
								-- Reversion deterioro años anteriores
								WHEN 4 THEN IIF(tXml.AccumulatedDeterioration = tXml.DeteriorationBalanceCurrentYear, tXml.DeteriorationBalanceCurrentYear - tXml.Value, (tXml.AccumulatedDeterioration - tXml.Value) - tXml.DeteriorationBalanceCurrentYear)
							END CreditValue
						FROM 
						(
							SELECT 
								tXml.AccountReceivableId,
								tXml.Value,
								tXml.AccumulatedDeterioration,
								tXml.DeteriorationBalanceCurrentYear,
								tXml.DeteriorationBalancePreviousYear,
								tXml.CurrentDeteriorationYear,
								tXml.InvoiceDocumentType,
								CASE @Process 
									WHEN 1 THEN 1
									WHEN 2 THEN 
										CASE 
											WHEN tXml.Value > tXml.AccumulatedDeterioration THEN 2
											WHEN tXml.Value < tXml.AccumulatedDeterioration THEN 3							
										END
								END Type
							FROM @TableXmlObject tXml
							WHERE tXml.LegalBookId = @CurrentLegalBookId
								AND (@Process = 1 OR 
								(
									@Process = 2 AND 
									(
										tXml.Value > tXml.AccumulatedDeterioration 
										OR 
										(
											tXml.Value < tXml.AccumulatedDeterioration AND tXml.DeteriorationBalanceCurrentYear > 0 AND tXml.CurrentDeteriorationYear = YEAR(@CourtDate)
										)
									)
								))

							UNION ALL

							--Reversion del deterioro de años anteriores
							SELECT 
								tXml.AccountReceivableId,
								tXml.Value,
								tXml.AccumulatedDeterioration,
								IIF(tXml.CurrentDeteriorationYear < YEAR(@CourtDate), 0, tXml.DeteriorationBalanceCurrentYear) DeteriorationBalanceCurrentYear,
								tXml.DeteriorationBalancePreviousYear + IIF(tXml.CurrentDeteriorationYear < YEAR(@CourtDate), tXml.DeteriorationBalanceCurrentYear, 0) DeteriorationBalancePreviousYear,
								tXml.CurrentDeteriorationYear,
								tXml.InvoiceDocumentType,
								4 Type
							FROM @TableXmlObject tXml
							WHERE tXml.LegalBookId = @CurrentLegalBookId
								AND @Process = 2 
								AND tXml.Value < tXml.AccumulatedDeterioration 
								AND 
								(
									(tXml.CurrentDeteriorationYear < YEAR(@CourtDate))
									OR
									(tXml.AccumulatedDeterioration - tXml.Value) > tXml.DeteriorationBalanceCurrentYear
								)
						) tXml
						JOIN Portfolio.AccountReceivable ar ON tXml.AccountReceivableId = ar.Id
						JOIN GeneralLedger.MainAccounts ma on ma.Id = CASE 
															--Usar cuentas de facturación básica cuando DocumentType es 6 o 7
															WHEN @ApplyDeterioration = 3 AND tXml.InvoiceDocumentType IN (6, 7) THEN
																CASE tXml.Type
																	WHEN 1 THEN ar.CreditProvisionAccountId
																	WHEN 2 THEN @BasicBillingCreditAccount
																	WHEN 3 THEN @BasicBillingReversalAccount
																	WHEN 4 THEN @BasicBillingPreviousPeriodReversalAccount
																END
															--Usar cuentas de AccountReceivable para otros casos
															ELSE
																CASE tXml.Type 
																	WHEN 1 THEN ar.CreditProvisionAccountId
																	WHEN 2 THEN ar.CreditAccountDeteriorationId
																	WHEN 3 THEN ar.ReversalAccountDeteriorationId 
																	WHEN 4 THEN ar.PreviousPeriodReversalAccountDeteriorationId
																END
														  END
						WHERE ar.Id = tXml.AccountReceivableId
					) jvd
					GROUP BY IdMainAccount, IdThirdParty, IdCostCenter
					HAVING SUM(DebitValue - CreditValue) <> 0

				--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
				SELECT @JournalVoucherXML =  convert(xml, (SELECT * 
					FROM @JournalVourcherTmp JournalVoucher 
					JOIN @JournalVourcherDetailTmp JournalVoucherDetail on JournalVoucher.Id = JournalVoucherDetail.IdAccounting For xml AUTO,TYPE, ELEMENTS))
				
				--Limpiar resultados anteriores
				DELETE FROM @resultJournalVoucher
				
				--Se consume el sp que guarda el comprobante contable
				INSERT @resultJournalVoucher exec  GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML, @CodeUser 
				
				--Se valida que no hayan errores en el guardado del comprobante contable
				IF (SELECT code  FROM @resultJournalVoucher) = '999' 
				BEGIN			
					SELECT @errorJV = MessageResult  FROM @resultJournalVoucher 
					SELECT 999 as CodeMessage, @errorJV as Message, '' as Consecutive
					
					CLOSE LegalBookCursor
					DEALLOCATE LegalBookCursor
					RETURN 
				END

				--Se obtiene el id que genero el comprobante contable
				SELECT @JournalVoucherId = IdJournalVoucher  FROM @resultJournalVoucher

				--Se obtiene el consecutivo que generó el comprobante contable
				SET @CurrentConsecutive = ISNULL((SELECT CAST(Consecutive as varchar(30)) FROM GeneralLedger.JournalVouchers WHERE id = @JournalVoucherId), '0')
				
				--Acumular consecutivos
				IF @AllConsecutives = ''
					SET @AllConsecutives = @CurrentConsecutive
				ELSE
					SET @AllConsecutives = @AllConsecutives + ', ' + @CurrentConsecutive

				FETCH NEXT FROM LegalBookCursor INTO @CurrentLegalBookId
			END

			CLOSE LegalBookCursor
			DEALLOCATE LegalBookCursor

			--Se genera el mensaje a devolver para múltiples comprobantes
			SET @Message = 'Se confirmó correctamente'
			SELECT @Message = @Message + CHAR(13) + CHAR(10) + 'Se generaron comprobantes contables tipo ' + Code + ' - ' + Name + ' con consecutivos: ' + @AllConsecutives 
			FROM GeneralLedger.JournalVoucherTypes WHERE Id = @IdJournalVoucher
			
			SET @Consecutive = @AllConsecutives
		END
		ELSE
		BEGIN
			--Proceso original cuando NO es ApplyDeterioration = 3
			--Inserto la cabecera del comprobante contable
			INSERT INTO @JournalVourcherTmp
				   (Id,Consecutive,LegalBookId,IdJournalVoucher,VoucherDate,Imported,[Status],Detail,EntityCode, EntityId,EntityName,IsClosedYear)
				values (0,0,@LegalBookId,@IdJournalVoucher,@DocumentDate,'False',2,'Comprobante contable generado desde Provisión/Deterioro de Cartera', @EntityCode, @EntityId, @EntityName,0)
			
			--Se insertan los detalles del comprobante contable débito
			INSERT INTO @JournalVourcherDetailTmp 
				(
					IdMainAccount, 
					IdThirdParty, 
					IdCostCenter, 
					DebitValue, 
					CreditValue, 
					Detail
				)
				SELECT
					IdMainAccount, 
					IdThirdParty, 
					IdCostCenter, 
					SUM(DebitValue) DebitValue, 
					SUM(CreditValue) CreditValue, 
					'' Detail
				FROM
				(
					SELECT 
						ma.Id IdMainAccount,
						CASE ma.HandlesThirdParty WHEN 1 THEN ar.ThirdPartyId ELSE NULL END as IdThirdParty ,
						CASE ma.HandlesCostCenter WHEN 1 THEN ar.CostCenterId ELSE NULL END as IdCostCenter,
						CASE @Process
							WHEN 1 THEN tXml.Value 
							WHEN 2 THEN ABS(tXml.Value - tXml.AccumulatedDeterioration)
						END DebitValue,
						0 CreditValue
					FROM @TableXmlObject tXml
					JOIN Portfolio.AccountReceivable ar ON tXml.AccountReceivableId = ar.Id
					JOIN GeneralLedger.MainAccounts ma ON ma.Id = CASE @Process 
																	WHEN 1 THEN ar.DebitProvisionAccountId
																	WHEN 2 THEN 
																		CASE
																			WHEN tXml.Value > tXml.AccumulatedDeterioration THEN ar.DebitAccountDeteriorationId
																			--Reversion del deterioro
																			WHEN tXml.Value < tXml.AccumulatedDeterioration THEN ar.CreditAccountDeteriorationId
																		END
																  END
					WHERE ar.Id = tXml.AccountReceivableId
				) jvd
				GROUP BY IdMainAccount, IdThirdParty, IdCostCenter
				HAVING SUM(DebitValue - CreditValue) <> 0
			
			--Se insertan los detalles del comprobante contable crédito
			INSERT INTO  @JournalVourcherDetailTmp
				(
					IdMainAccount, 
					IdThirdParty, 
					IdCostCenter, 
					DebitValue, 
					CreditValue, 
					Detail
				)
				SELECT
					IdMainAccount, 
					IdThirdParty, 
					IdCostCenter, 
					SUM(DebitValue) DebitValue, 
					SUM(CreditValue) CreditValue, 
					'' Detail
				FROM
				(
					SELECT 
						ma.Id IdMainAccount,
						CASE ma.HandlesThirdParty WHEN 1 THEN ar.ThirdPartyId ELSE NULL END as IdThirdParty ,
						CASE ma.HandlesCostCenter WHEN 1 THEN ar.CostCenterId ELSE NULL END as IdCostCenter,
						0 DebitValue,
						CASE tXml.Type 
							-- Provision
							WHEN 1 THEN tXml.Value 
							--Deterioro
							WHEN 2 THEN tXml.Value - tXml.AccumulatedDeterioration
							-- Reversion deterioro año actual
							WHEN 3 THEN IIF((tXml.AccumulatedDeterioration - tXml.Value) > tXml.DeteriorationBalanceCurrentYear, tXml.DeteriorationBalanceCurrentYear, (tXml.AccumulatedDeterioration - tXml.Value))
							-- Reversion deterioro años anteriores
							WHEN 4 THEN IIF(tXml.AccumulatedDeterioration = tXml.DeteriorationBalanceCurrentYear, tXml.DeteriorationBalanceCurrentYear - tXml.Value, (tXml.AccumulatedDeterioration - tXml.Value) - tXml.DeteriorationBalanceCurrentYear)
						END CreditValue
					FROM 
					(
						SELECT 
							tXml.AccountReceivableId,
							tXml.Value,
							tXml.AccumulatedDeterioration,
							tXml.DeteriorationBalanceCurrentYear,
							tXml.DeteriorationBalancePreviousYear,
							tXml.CurrentDeteriorationYear,						
							CASE @Process 
								WHEN 1 THEN 1
								WHEN 2 THEN 
									CASE 
										WHEN tXml.Value > tXml.AccumulatedDeterioration THEN 2
										WHEN tXml.Value < tXml.AccumulatedDeterioration THEN 3							
									END
							END Type
						FROM @TableXmlObject tXml
						WHERE @Process = 1 OR 
							(
								@Process = 2 AND 
								(
									tXml.Value > tXml.AccumulatedDeterioration 
									OR 
									(
										tXml.Value < tXml.AccumulatedDeterioration AND tXml.DeteriorationBalanceCurrentYear > 0 AND tXml.CurrentDeteriorationYear = YEAR(@CourtDate)
									)
								)
							)

						UNION ALL

						--Reversion del deterioro de años anteriores
						SELECT 
							tXml.AccountReceivableId,
							tXml.Value,
							tXml.AccumulatedDeterioration,
							IIF(tXml.CurrentDeteriorationYear < YEAR(@CourtDate), 0, tXml.DeteriorationBalanceCurrentYear) DeteriorationBalanceCurrentYear,
							tXml.DeteriorationBalancePreviousYear + IIF(tXml.CurrentDeteriorationYear < YEAR(@CourtDate), tXml.DeteriorationBalanceCurrentYear, 0) DeteriorationBalancePreviousYear,
							tXml.CurrentDeteriorationYear,
							4 Type
						FROM @TableXmlObject tXml
						WHERE @Process = 2 
							AND tXml.Value < tXml.AccumulatedDeterioration 
							AND 
							(
								(tXml.CurrentDeteriorationYear < YEAR(@CourtDate))
								OR
								(tXml.AccumulatedDeterioration - tXml.Value) > tXml.DeteriorationBalanceCurrentYear
							)
					) tXml
					JOIN Portfolio.AccountReceivable ar ON tXml.AccountReceivableId = ar.Id
					JOIN GeneralLedger.MainAccounts ma on ma.Id = CASE tXml.Type 
																	WHEN 1 THEN ar.CreditProvisionAccountId
																	WHEN 2 THEN ar.CreditAccountDeteriorationId
																	WHEN 3 THEN ar.ReversalAccountDeteriorationId 
																	WHEN 4 THEN ar.PreviousPeriodReversalAccountDeteriorationId
																  END
					WHERE ar.Id = tXml.AccountReceivableId
				) jvd
				GROUP BY IdMainAccount, IdThirdParty, IdCostCenter
				HAVING SUM(DebitValue - CreditValue) <> 0

			--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
			SELECT @JournalVoucherXML =  convert(xml, (SELECT * 
				FROM @JournalVourcherTmp JournalVoucher 
				JOIN @JournalVourcherDetailTmp JournalVoucherDetail on JournalVoucher.Id = JournalVoucherDetail.IdAccounting For xml AUTO,TYPE, ELEMENTS))
			
			--Se consume el sp que guarda el comprobante contable
			INSERT @resultJournalVoucher exec  GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML, @CodeUser 
			
			--Se valida que no hayan errores en el guardado del comprobante contable
			IF (SELECT code  FROM @resultJournalVoucher) = '999' 
			BEGIN			
				SELECT @errorJV = MessageResult  FROM @resultJournalVoucher 
				SELECT 999 as CodeMessage, @errorJV as Message, '' as Consecutive
				RETURN 
			END

			--Se obtiene el id que genero el comprobante contable
			SELECT @JournalVoucherId = IdJournalVoucher  FROM @resultJournalVoucher

			--Se obtiene el consecutivo que generó el comprobante contable
			SET @Consecutive = isnull( (SELECT cast( Consecutive as varchar(30))  FROM GeneralLedger.JournalVouchers WHERE id = @JournalVoucherId ),'0')

			--Se genera el mensaje a devolver
			SET @Message = 'Se confirmó correctamente'
			SELECT @Message = @Message + CHAR(13) + CHAR(10) + 'Se generó comprobante contable tipo ' + Code + ' - ' + Name + ' con consecutivo ' + @Consecutive 
			FROM GeneralLedger.JournalVoucherTypes WHERE Id = @IdJournalVoucher
		END

		--Se actualizan los campos de la cuenta por cobrar en la tabla AccountReceivable segun corresponda
		UPDATE ar 
			SET --Provision
				ProvisionBalance = CASE @Process 
										WHEN 1 THEN ar.ProvisionBalance + tXml.[Value] 
										WHEN 2 THEN ar.ProvisionBalance 
								   END, 
				--Deterioro
				DeteriorationBalance = CASE @Process 
											WHEN 1 THEN ar.DeteriorationBalance 
											WHEN 2 THEN tXml.[Value] 
									   END,
				DeteriorationBalanceCurrentYear = CASE @Process 
											WHEN 1 THEN ar.DeteriorationBalanceCurrentYear 
											WHEN 2 THEN 
												CASE 
													WHEN tXml.Value > tXml.AccumulatedDeterioration THEN 
														(tXml.Value - tXml.AccumulatedDeterioration) + IIF
															(
																tXml.CurrentDeteriorationYear < YEAR(@CourtDate),
																0,
																tXml.DeteriorationBalanceCurrentYear
															)
													WHEN tXml.Value < tXml.AccumulatedDeterioration THEN 
														ar.DeteriorationBalanceCurrentYear - IIF
															(
																tXml.CurrentDeteriorationYear < YEAR(@CourtDate),
																tXml.DeteriorationBalanceCurrentYear,
																IIF
																(
																	(tXml.AccumulatedDeterioration - tXml.Value) > tXml.DeteriorationBalanceCurrentYear, 
																	tXml.DeteriorationBalanceCurrentYear, 
																	tXml.AccumulatedDeterioration - tXml.Value
																)
															)
													ELSE ar.DeteriorationBalanceCurrentYear 
												END
									   END,
				DeteriorationBalancePreviousYear = CASE @Process 
											WHEN 1 THEN ar.DeteriorationBalancePreviousYear 
											WHEN 2 THEN 
												CASE
													WHEN tXml.Value > tXml.AccumulatedDeterioration THEN  
														ar.DeteriorationBalancePreviousYear + IIF
															(
																tXml.CurrentDeteriorationYear < YEAR(@CourtDate),
																tXml.DeteriorationBalanceCurrentYear,
																0
															)
													WHEN tXml.Value < tXml.AccumulatedDeterioration THEN 
														ar.DeteriorationBalancePreviousYear - IIF
															(
																(tXml.AccumulatedDeterioration - tXml.Value) > tXml.DeteriorationBalanceCurrentYear,
																(tXml.AccumulatedDeterioration - tXml.Value) - tXml.DeteriorationBalanceCurrentYear,
																0
															) + IIF
															(
																tXml.CurrentDeteriorationYear < YEAR(@CourtDate),
																IIF
																(
																	(tXml.AccumulatedDeterioration - tXml.Value) > tXml.DeteriorationBalanceCurrentYear, 
																	0, 
																	tXml.DeteriorationBalanceCurrentYear - (tXml.AccumulatedDeterioration - tXml.Value)
																),
																0
															)
													ELSE ar.DeteriorationBalancePreviousYear
												END
									   END,
				CurrentDeteriorationYear = CASE @Process 
											WHEN 1 THEN ar.CurrentDeteriorationYear
											WHEN 2 THEN YEAR(@CourtDate)
									   END
		FROM @TableXmlObject tXml
		JOIN Portfolio.AccountReceivable ar ON ar.Id = tXml.AccountReceivableId
		JOIN Portfolio.PortfolioProvisionDetail ppd ON ppd.AccountReceivableId = ar.Id AND ppd.PortfolioProvisionId = @PortfolioProvisionId
		LEFT JOIN GeneralLedger.LegalBook lb ON lb.Id = ppd.LegalBookId
		WHERE (ppd.LegalBookId IS NULL OR lb.OfficialBook = 1)
		
		-- Confirmamos el registro de provisión / deterioro
		Update Portfolio.PortfolioProvision
			set Code = @Code, 
				[Status] = 2, 
				ConfirmationUser = @CodeUser, 
				ConfirmationDate = [Common].[GETDATE]()
		WHERE Id = @PortfolioProvisionId
		

		--Se retorna el ok
		SELECT 0 as CodeMessage, @Message as Message, @Consecutive as Consecutive		
	END try
	BEGIN catch
		--Se retorna el error
		SELECT 999 as CodeMessage, ERROR_MESSAGE() + ' linea: ' + cast(ERROR_LINE() as varchar(20) )  as Message, '' as Consecutive
	END catch
END