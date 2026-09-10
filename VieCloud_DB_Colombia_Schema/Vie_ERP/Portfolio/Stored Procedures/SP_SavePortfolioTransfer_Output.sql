-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-10
-- Description:	Procedimiento que se encarga de guardar y confirmar el cruce de anticipo vs cxc
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_SavePortfolioTransfer_Output]
	@PortfolioTransferXml XML,
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CompanyType TINYINT,
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	@Id INT OUTPUT,
	@Code VARCHAR(20) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables para obtener la cabecera
	DECLARE @OperatingUnitId INT,
			@DocumentDate DATETIME, 
			@CustomerId INT, 
			@ThirdPartyId INT, 
			@PortfolioAdvanceId INT, 
			@TransferType TINYINT, 
			@MainAccountId INT, 
			@CostCenterId INT, 
			@Observations VARCHAR(300), 
			@Status TINYINT,
			@CurrencyIdAdvanced INT,
			@OfficialCurrency  INT,
			@EntityName varchar(100),
			------------------------------
			@IdForm INT = 687,
			@DocumentType INT = 2,
			------------------------------
			@PortfolioNoteId INT,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			----------------------------------
			@Message_DiffAdjustment VARCHAR(1000)
	
	--Tabla temporal para obtener los detalles de portfolioTransfer
	DECLARE @PortfolioTransferDetail TABLE
	(
		Id INT, 
		PortfolioTrasferId INT, 
		AccountReceivableId INT, 
		MainAccountId INT, 
		CostCenterId INT, 
		Value DECIMAL(18,2), 
		ChangeTracker BIT,
		TRMValue NUMERIC(20,5),
		ValueInCurrencyInvoice NUMERIC(18,2)
	)

	--Tabla temporal para obtener los detalles de portfolioTransferOtherConcept
	DECLARE @PortfolioTransferOtherConcept TABLE
	(
		Id INT, 
		PortfolioTrasferId INT, 
		PortfolioNoteConceptId INT, 
		MainAccountId INT, 
		ThirdPartyId INT,
		CostCenterId INT, 
		Nature TINYINT, 
		Value DECIMAL(18,2), 
		ChangeTracker BIT
	)
	
	declare @responseRevaluation table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	BEGIN TRY
		DECLARE @ownTran bit = 0
		IF @@TRANCOUNT = 0
		BEGIN
			BEGIN TRANSACTION
			SET @ownTran = 1
		END
		--Se obtienen los datos de la cabecera
		SELECT	@Id = t.x.value('Id[1]','INT'),
				@OperatingUnitId = t.x.value('OperatingUnitId[1]','INT'),
				@Code = t.x.value('Code[1]','VARCHAR(20)'),
				@DocumentDate = t.x.value('DocumentDate[1]','DATETIME'),
				@CustomerId = IIF(t.x.value('CustomerId[1]','INT') = 0, NULL, t.x.value('CustomerId[1]','INT')),
				@ThirdPartyId = t.x.value('ThirdPartyId[1]','INT'),
				@PortfolioAdvanceId = t.x.value('PortfolioAdvanceId[1]','INT'),
				@TransferType = t.x.value('TransferType[1]','TINYINT'),
				@MainAccountId = t.x.value('MainAccountId[1]','INT'),
				@CostCenterId = IIF(t.x.value('CostCenterId[1]','INT') = 0, NULL, t.x.value('CostCenterId[1]','INT')),
				@Observations = t.x.value('Observations[1]','VARCHAR(300)'),
				@Status = t.x.value('Status[1]','TINYINT'),
				@CurrencyIdAdvanced = t.x.value('CurrencyId[1]','INT'),
				----------------------------------------------------------------------------------------------------
				@PortfolioNoteId = t.x.value('PortfolioNoteId[1]','INT'),
				@EntityName =  t.x.value('EntityName[1]','VARCHAR(100)')
		FROM @PortfolioTransferXml.nodes('/PortfolioTransfer') t(x)

		SET @OfficialCurrency = (SELECT TOP 1 OfficialCurrencyId FROM GeneralLedger.CompanySettings WITH (NOLOCK))
		IF @CurrencyIdAdvanced=0 OR @CurrencyIdAdvanced IS NULL BEGIN
			SET @CurrencyIdAdvanced= @OfficialCurrency
		END

		IF NOT EXISTS (SELECT 1 FROM Portfolio.SettingPortfolio WITH (NOLOCK) WHERE OperatingUnitId = @OperatingUnitId)
		BEGIN
			SELECT @CodeResult = 999,
				   @MessageResult = CONCAT('No existen parametros de cartera con unidad operativa ', ou.UnitCode, ' - ', ou.UnitName),
				   @Id = 0,
				   @Code = ''
			FROM Common.OperatingUnit ou WITH (NOLOCK)
			WHERE ou.Id = @OperatingUnitId
			IF @ownTran = 1 ROLLBACK TRANSACTION
			RETURN
		END

		IF @Status = 4
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM Portfolio.PortfolioTransfer with (nolock) WHERE Id = @Id AND Status = 2)
			BEGIN
				SELECT	@Message = 'El Cruce de Anticipo vs CxC se encuentra en estado: ' + IIF(Status = 1, 'Registrado', 'Anulado')
				FROM Portfolio.PortfolioTransfer
				WHERE Id = @Id

				SELECT	@CodeResult = 999,
						@MessageResult = ISNULL(@Message, 'No existe el Cruce de Anticipo vs CxC'),
						@Id = 0,
						@Code = ''
				IF @ownTran = 1 ROLLBACK TRANSACTION
				RETURN
			END

			IF NOT EXISTS (SELECT 1 FROM Portfolio.PortfolioNote with (nolock) WHERE Id = @PortfolioNoteId AND Status = 2)
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = 'No existe la nota de revesión de anticipo o no se esta confirmando',
						@Id = 0,
						@Code = ''
				IF @ownTran = 1 ROLLBACK TRANSACTION
				RETURN
			END
		END
		ELSE
		BEGIN
			IF EXISTS (SELECT 1 FROM Portfolio.PortfolioTransfer with (nolock) WHERE Id = @Id AND Status <> 1)
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = 'El Cruce de Anticipo vs CxC se encuentra en estado: ' + CASE Status 
																									WHEN 2 THEN 'Confirmado'
																									WHEN 3 THEN 'Anulado'
																									WHEN 4 THEN 'Reversado'
																								  END,
						@Id = 0,
						@Code = ''
				FROM Portfolio.PortfolioTransfer with (nolock)
				WHERE Id = @Id AND Status <> 1
				IF @ownTran = 1 ROLLBACK TRANSACTION
				RETURN
			END
		END

		IF @Status = 3
		BEGIN
			UPDATE [Portfolio].[PortfolioTransfer]
				SET [Status] = @Status,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @CodeUser,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END
		ELSE
		BEGIN
			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @PortfolioTransferDetail
				SELECT	ISNULL(t.x.value('Id[1]','INT'), 0) AS Id,
						t.x.value('PortfolioTrasferId[1]','INT') AS PortfolioTrasferId,
						t.x.value('AccountReceivableId[1]','INT') AS AccountReceivableId,
						t.x.value('MainAccountId[1]','INT') AS MainAccountId,
						IIF(t.x.value('CostCenterId[1]','INT') = 0, NULL, t.x.value('CostCenterId[1]','INT')) CostCenterId,
						t.x.value('Value[1]','DECIMAL(18,2)') AS Value,
						t.x.value('ChangeTracker[1]','BIT') AS ChangeTracker,
						T.x.value('TRMValue[1]', 'NUMERIC(20,5)') AS TRMValue,
						IIF(COALESCE(t.x.value('ValueInCurrencyInvoice[1]','DECIMAL(18,2)'),0)=0,
								t.x.value('Value[1]','DECIMAL(18,2)'),
								t.x.value('ValueInCurrencyInvoice[1]','DECIMAL(18,2)'))  as ValueInCurrencyInvoice
				FROM @PortfolioTransferXml.nodes('/PortfolioTransfer/PortfolioTransferDetail') t(x)

			
			--se eliminan los detalles marcados para su eliminación
			DELETE ptd
			FROM @PortfolioTransferDetail d
			JOIN [Portfolio].[PortfolioTransferDetail] ptd with (nolock) ON d.Id = ptd.Id
			WHERE ptd.PortfolioTrasferId = @Id AND d.ChangeTracker = 1

			DELETE d FROM @PortfolioTransferDetail d WHERE d.ChangeTracker = 1

			/*************Se actualiza la entidad que viene del XML para insertar el valor en la moneda del factura***************************************/

			update d SET ValueInCurrencyInvoice = iif(ROUND(common.CurrencyConverterByModule(ara.Balance,ISNULL(ar.CurrencyId,@OfficialCurrency),@CurrencyIdAdvanced,NULL,@EntityName,CAST(Common.GETDATE() as DATE)),2) <= d.value,ara.Balance,common.CurrencyConverterByModule(d.Value,@CurrencyIdAdvanced,ISNULL(ar.CurrencyId,@OfficialCurrency),NULL,@EntityName,CAST(Common.GETDATE() as date)))
			from @PortfolioTransferDetail d
			JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) on d.AccountReceivableId=ar.Id
			JOIN  Portfolio.AccountReceivableAccounting ara WITH(NOLOCK) on d.AccountReceivableId = ara.AccountReceivableId and d.MainAccountId=ara.MainAccountId			
			/*********************************************************************************************************************************************/
			

			--Se obtiene los detalles del XML(PortfolioTrabsferOtherConcept)
			INSERT INTO @PortfolioTransferOtherConcept
				SELECT 
					ISNULL(t.x.value('Id[1]','INT'), 0) AS Id,
					t.x.value('PortfolioTransferId[1]','INT') AS PortfolioTransferId,
					t.x.value('PortfolioNoteConceptId[1]','INT') AS PortfolioNoteConceptId,
					t.x.value('MainAccountId[1]','INT') AS MainAccountId,
					IIF(t.x.value('ThirdPartyId[1]','INT') = 0, NULL, t.x.value('ThirdPartyId[1]','INT')) AS ThirdPartyId,
					IIF(t.x.value('CostCenterId[1]','INT') = 0, NULL, t.x.value('CostCenterId[1]','INT')) AS CostCenterId,
					t.x.value('Nature[1]','TINYINT') AS Nature,
					t.x.value('Value[1]','DECIMAL(18,2)') AS Value,
					t.x.value('ChangeTracker[1]','BIT') AS ChangeTracker					
				FROM @PortfolioTransferXml.nodes('/PortfolioTransfer/PortfolioTransferOtherConcept') t(x)

			--se eliminan los detalles marcados para su eliminación
			DELETE ptoc
			FROM @PortfolioTransferOtherConcept d
			JOIN [Portfolio].[PortfolioTransferOtherConcept] ptoc with (nolock) ON d.Id = ptoc.Id
			WHERE ptoc.PortfolioTransferId = @Id AND d.ChangeTracker = 1

			DELETE d FROM @PortfolioTransferOtherConcept d WHERE d.ChangeTracker = 1			

			/*************************************VALIDACIONES************************************/

			--Validar el periodo se encuentre abierto en contabilidad
			IF NOT EXISTS (SELECT 1 FROM [GeneralLedger].[ClosedMonth] with (nolock) WHERE [Year] = Year(@DocumentDate) AND [Month] = Month(@DocumentDate) and Status = 1)
			BEGIN
				SELECT @CodeResult = 999,
						@MessageResult = 'El periodo ' + CONCAT(CAST(YEAR(@DocumentDate) AS VARCHAR(4)), '-', CAST(MONTH(@DocumentDate) AS VARCHAR(2))) + ' no se encuentra abierto',
						@Id = 0,
						@Code = ''
				IF @ownTran = 1 ROLLBACK TRANSACTION
				RETURN
			END
		
			--Validar que el cruce de anticipo tenga detalles
			IF NOT EXISTS
			(
					SELECT 1
					FROM [Portfolio].[PortfolioTransferDetail] d with (nolock)
					WHERE d.PortfolioTrasferId = @Id
				UNION ALL
					SELECT 1
					FROM @PortfolioTransferDetail d
				UNION ALL
					SELECT 1
					FROM [Portfolio].[PortfolioTransferOtherConcept] d with (nolock)
					WHERE d.PortfolioTransferId = @Id
				UNION ALL
					SELECT 1
					FROM @PortfolioTransferOtherConcept d
			)
			BEGIN
				SELECT @CodeResult = 999,
						@MessageResult = 'El Cruce de Anticipos no tiene detalles',
						@Id = 0,
						@Code = ''
				IF @ownTran = 1 ROLLBACK TRANSACTION
				RETURN 
			END

			--Validar que el cruce de anticipo tenga detalles
			IF @Status <> 4 AND  NOT EXISTS
			(
					SELECT 1
					FROM [Portfolio].[PortfolioTransferDetail] d with (nolock)
					WHERE d.PortfolioTrasferId = @Id
				UNION ALL
					SELECT 1
					FROM @PortfolioTransferDetail d
			)
			BEGIN
				SELECT @CodeResult = 999,
						@MessageResult = 'El Cruce de Anticipos no tiene facturas',
						@Id = 0,
						@Code = ''
				IF @ownTran = 1 ROLLBACK TRANSACTION
				RETURN 
			END

			--Validar el valor de los detalles sean válidos
			IF EXISTS
			(
					SELECT 1
					FROM @PortfolioTransferDetail d
					WHERE NOT d.Value > 0
				UNION ALL
					SELECT 1
					FROM @PortfolioTransferOtherConcept d
					WHERE NOT d.Value > 0
			)
			BEGIN
				SELECT @CodeResult = 999,
						@MessageResult = 'El Cruce de Anticipos tiene detalles en 0 o negativos',
						@Id = 0,
						@Code = ''
				IF @ownTran = 1 ROLLBACK TRANSACTION
				RETURN 
			END

			--Se valida que no existan detalles duplicados
			IF EXISTS
			(
				SELECT 1
				FROM
				(
						SELECT d.AccountReceivableId, d.MainAccountId
						FROM [Portfolio].[PortfolioTransferDetail] d with (nolock)
						WHERE d.PortfolioTrasferId = @Id
					UNION ALL
						SELECT d.AccountReceivableId, d.MainAccountId
						FROM @PortfolioTransferDetail d
				) d
				GROUP BY d.AccountReceivableId, d.MainAccountId
				HAVING COUNT(1) > 1
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - La factura ' + ar.InvoiceNumber + ' de la cuenta contable ' + CONCAT(ma.Number, ' - ', ma.Name) + ' se encuentra duplicada.'
						FROM
						(
								SELECT d.AccountReceivableId, d.MainAccountId
								FROM [Portfolio].[PortfolioTransferDetail] d with (nolock)
								WHERE d.PortfolioTrasferId = @Id
							UNION ALL
								SELECT d.AccountReceivableId, d.MainAccountId
								FROM @PortfolioTransferDetail d
						) d
						JOIN Portfolio.AccountReceivable ar with (nolock) ON d.AccountReceivableId = ar.Id
						JOIN GeneralLedger.MainAccounts ma with (nolock) ON d.MainAccountId = ma.Id
						GROUP BY d.AccountReceivableId, ar.InvoiceNumber, d.MainAccountId, ma.Number, ma.Name
						HAVING COUNT(1) > 1
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeResult = 999, 
						@MessageResult = ISNULL(@Message, ''), 
						@Id = 0, 
						@Code = '' 
				IF @ownTran = 1 ROLLBACK TRANSACTION
				RETURN 
			END

			--Se valida que no existan otros conceptos duplicados
			IF EXISTS
			(
				SELECT 1
				FROM
				(
						SELECT d.PortfolioNoteConceptId, d.MainAccountId, d.ThirdPartyId, d.CostCenterId, d.Nature
						FROM [Portfolio].[PortfolioTransferOtherConcept] d with (nolock)
						WHERE d.PortfolioTransferId = @Id
					UNION ALL
						SELECT d.PortfolioNoteConceptId, d.MainAccountId, d.ThirdPartyId, d.CostCenterId, d.Nature
						FROM @PortfolioTransferOtherConcept d
				) d
				GROUP BY d.PortfolioNoteConceptId, d.MainAccountId, d.ThirdPartyId, d.CostCenterId, d.Nature
				HAVING COUNT(1) > 1
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - El conceto ' + pnc.Code + ' con cuenta contable ' + ma.Number + IIF(tp.Id IS NULL, '', CONCAT(' tercero ', tp.Nit)) + IIF(cc.Id IS NULL, '', CONCAT(' centro de costo ', cc.Code)) + ' se encuentra duplicado.'
						FROM
						(
								SELECT d.PortfolioNoteConceptId, d.MainAccountId, d.ThirdPartyId, d.CostCenterId
								FROM [Portfolio].[PortfolioTransferOtherConcept] d with (nolock)
								WHERE d.PortfolioTransferId = @Id
							UNION ALL
								SELECT d.PortfolioNoteConceptId, d.MainAccountId, d.ThirdPartyId, d.CostCenterId
								FROM @PortfolioTransferOtherConcept d
						) d
						JOIN Portfolio.PortfolioNoteConcept pnc WITH(NOLOCK) ON d.PortfolioNoteConceptId = pnc.Id
						JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON d.MainAccountId = ma.Id
						LEFT JOIN Common.ThirdParty tp WITH(NOLOCK) ON d.ThirdPartyId = tp.Id
						LEFT JOIN Payroll.CostCenter cc WITH(NOLOCK) ON d.CostCenterId = cc.Id
						GROUP BY d.PortfolioNoteConceptId, pnc.Code, d.MainAccountId, ma.Number, tp.Id, tp.Nit, cc.Id, cc.Code
						HAVING COUNT(1) > 1
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeResult = 999, 
						@MessageResult = ISNULL(@Message, ''), 
						@Id = 0, 
						@Code = '' 
				IF @ownTran = 1 ROLLBACK TRANSACTION
				RETURN 
			END

			--Si se esta confirmando o reversando
			IF @Status IN (2, 4)
			BEGIN
				-- Se valida que en TRM 1 no cambie el valor in currency (con tolerancia dinámica basada en RoundingType de la moneda)
				IF EXISTS
				(
					SELECT 1
					FROM
					(
							SELECT d.AccountReceivableId, d.Value, d.ValueInCurrencyInvoice, ar.CurrencyId
							FROM [Portfolio].[PortfolioTransferDetail] d with (nolock)
							JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) ON d.AccountReceivableId = ar.Id
							WHERE d.PortfolioTrasferId = @Id
								AND d.TRMValue = 1 
						UNION ALL
							SELECT d.AccountReceivableId, d.Value, d.ValueInCurrencyInvoice, ar.CurrencyId
							FROM @PortfolioTransferDetail d
							JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) ON d.AccountReceivableId = ar.Id
							WHERE d.TRMValue = 1
					) d
					JOIN Common.Currency c WITH(NOLOCK) ON d.CurrencyId = c.Id
					WHERE ABS(d.Value - d.ValueInCurrencyInvoice) > Common.GetRoundTolerance(c.RoundingType)
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - La factura ' + ar.InvoiceNumber + ' tiene desbalance en sus saldos. Valor: ' + FORMAT(d.Value, 'N2') + ', Valor en moneda factura: ' + FORMAT(d.ValueInCurrencyInvoice, 'N2') + ', Diferencia: ' + FORMAT(ABS(d.Value - d.ValueInCurrencyInvoice), 'N2') + ', Tolerancia: ' + FORMAT(Common.GetRoundTolerance(c.RoundingType), 'N2')
							FROM Portfolio.AccountReceivable ar with (nolock)
							JOIN Common.Currency c WITH(NOLOCK) ON ar.CurrencyId = c.Id
							JOIN
							(
								SELECT d.AccountReceivableId, d.Value, d.ValueInCurrencyInvoice
								FROM
								(
										SELECT d.AccountReceivableId, d.Value, d.ValueInCurrencyInvoice, ar.CurrencyId
										FROM [Portfolio].[PortfolioTransferDetail] d with (nolock)
										JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) ON d.AccountReceivableId = ar.Id
										WHERE d.PortfolioTrasferId = @Id
											AND d.TRMValue = 1
									UNION ALL
										SELECT d.AccountReceivableId, d.Value, d.ValueInCurrencyInvoice, ar.CurrencyId
										FROM @PortfolioTransferDetail d
										JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) ON d.AccountReceivableId = ar.Id
										WHERE d.TRMValue = 1
								) d
								JOIN Common.Currency c WITH(NOLOCK) ON d.CurrencyId = c.Id
								WHERE ABS(d.Value - d.ValueInCurrencyInvoice) > Common.GetRoundTolerance(c.RoundingType)
							) d ON ar.Id = d.AccountReceivableId
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT @CodeResult = 999, 
						   @MessageResult = ISNULL(@Message, ''), 
						   @Id = 0, 
						   @Code = '' 
					IF @ownTran = 1 ROLLBACK TRANSACTION
					RETURN 
				END

				--Se valida que el valor de los otros conceptos no supere el valor de los detalles
				IF EXISTS
				(
					SELECT 1
					FROM
					(
						SELECT @Id PortfolioTransferId, SUM(d.Value) Value
						FROM
						(
								SELECT d.Value
								FROM [Portfolio].[PortfolioTransferDetail] d with (nolock)
								WHERE d.PortfolioTrasferId = @Id
							UNION ALL
								SELECT d.Value
								FROM @PortfolioTransferDetail d
						) d
					) ptd
					FULL JOIN
					(
						SELECT @Id PortfolioTransferId, SUM(d.Value * IIF(d.Nature = 1, 1, -1)) Value
						FROM @PortfolioTransferOtherConcept d
					) ptoc ON ptd.PortfolioTransferId = ptoc.PortfolioTransferId
					WHERE ISNULL(ptoc.Value, 0) > ISNULL(ptd.Value, 0)
				)
				BEGIN
					SELECT @CodeResult = 999,
							@MessageResult = 'El valor de otros conceptos es mayor al total de las facturas',
							@Id = 0,
							@Code = ''
					IF @ownTran = 1 ROLLBACK TRANSACTION
					RETURN 
				END

				--Validar que los detalles no superen el saldo (Si se confirma) o el valor inicial (Si se reversa) del anticipo
				IF EXISTS
				(
					SELECT 1
					FROM
					(
						SELECT @Id Id, pa.Balance, pa.Value
						FROM Portfolio.PortfolioAdvance pa with (nolock)
						WHERE pa.Id = @PortfolioAdvanceId
					) pt
					JOIN
					(
						SELECT d.PortfolioTransferId, SUM(d.DebitValue) DebitValue, SUM(d.CreditValue) CreditValue
						FROM
						(
								SELECT @Id PortfolioTransferId, 0 DebitValue, d.Value CreditValue
								FROM [Portfolio].[PortfolioTransferDetail] d with (nolock)
								WHERE d.PortfolioTrasferId = @Id
							UNION ALL
								SELECT @Id PortfolioTransferId, 0 DebitValue, d.Value CreditValue
								FROM @PortfolioTransferDetail d
							UNION ALL
								SELECT @Id PortfolioTransferId, IIF(d.Nature = 1, d.Value, 0) DebitValue, IIF(d.Nature = 1, 0, d.Value) CreditValue
								FROM [Portfolio].[PortfolioTransferOtherConcept] d with (nolock)
								WHERE d.PortfolioTransferId = @Id
							UNION ALL
								SELECT @Id PortfolioTransferId, IIF(d.Nature = 1, d.Value, 0) DebitValue, IIF(d.Nature = 1, 0, d.Value) CreditValue
								FROM @PortfolioTransferOtherConcept d
						) d
						GROUP BY d.PortfolioTransferId
					) ptd ON pt.Id = ptd.PortfolioTransferId
					WHERE 
					(
						(@Status = 2 AND ptd.CreditValue > (pt.Balance + ptd.DebitValue))				--Validar el saldo del anticipo
						OR
						(@Status = 4 AND (pt.Balance + ptd.CreditValue - ptd.DebitValue) > pt.Value)	--Validar el valor inicial del anticipo
					)
				)
				BEGIN
					SELECT @CodeResult = 999,
							@MessageResult = 'El valor del traslado es mayor al ' + IIF(@Status = 4, 'valor', 'saldo') + ' del anticipo',
							@Id = 0,
							@Code = ''
					IF @ownTran = 1 ROLLBACK TRANSACTION
					RETURN 
				END

			/********************************************************************/

				IF @Status <> 4 and @Id <> 0 AND EXISTS(	SELECT	1
										FROM [Portfolio].[PortfolioTransferDetail] d with (nolock)
										JOIN [Portfolio].[PortfolioTransfer] c WITH(NOLOCK) on d.PortfolioTrasferId=c.Id
										JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) on d.AccountReceivableId = ar.Id
										WHERE d.PortfolioTrasferId = @Id and cast(c.CreationDate as date)<> cast(common.GETDATE() AS DATE)
										and (Common.CurrencyConverterByModule(1,@CurrencyIdAdvanced,ISNULL(ar.CurrencyId,@OfficialCurrency),NULL,@EntityName,cast(c.CreationDate as date)) <> (Common.CurrencyConverterByModule(1,@CurrencyIdAdvanced,ISNULL(ar.CurrencyId,@OfficialCurrency),NULL,@EntityName,cast(Common.GETDATE() as date))))) 
				BEGIN

						SELECT @CodeResult = 999,
									@MessageResult = 'El valor del TRM para los detalle previamente guardado ha cambiado por favor re-calcule los detalles de las facturas ',
									@Id = 0,
									@Code = ''
							IF @ownTran = 1 ROLLBACK TRANSACTION
							RETURN 
				
				END

			/********************************************************************/

				--Validar que los detalles no superen el saldo (Si se confirma) o el valor inicial (Si se reversa) de la factura
				IF EXISTS
				(
					SELECT 1
					FROM Portfolio.AccountReceivable ar with (nolock)
					JOIN
					(
						SELECT d.AccountReceivableId, SUM(d.Value) Value
						FROM
						(
								SELECT d.AccountReceivableId, d.ValueInCurrencyInvoice Value
								FROM [Portfolio].[PortfolioTransferDetail] d with (nolock)
								WHERE d.PortfolioTrasferId = @Id
							UNION ALL
								SELECT d.AccountReceivableId, d.ValueInCurrencyInvoice Value
								FROM @PortfolioTransferDetail d
						) d
						GROUP BY d.AccountReceivableId
					) d ON ar.Id = d.AccountReceivableId
					WHERE
					(
						(@Status <> 4 AND ROUND(d.Value, 2) > ROUND(ar.Balance, 2))
						OR
						(@Status = 4 AND ROUND(d.Value + ar.Balance, 2) > ROUND(ar.Value, 2))
					)
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + IIF
							(
								@Status = 4,
								' - No se puede superar el valor inicial (' + FORMAT(ar.Value, 'C0', 'es-CO') + ') de la factura ' + ar.InvoiceNumber + ': valor a máximo reversar (' + FORMAT(ar.Value - ar.Balance, 'C0', 'es-CO') + ') - valor a reversar (' + FORMAT(d.Value, 'C0', 'es-CO') + ')',
								' - El saldo de la factura ' + ar.InvoiceNumber + ' (' + FORMAT(ar.Balance, 'C0', 'es-CO') + ') es menor que el valor a trasladar (' + FORMAT(d.Value, 'C0', 'es-CO') + ')'
							)
							FROM Portfolio.AccountReceivable ar with (nolock)
							JOIN
							(
								SELECT d.AccountReceivableId, SUM(d.Value) Value
								FROM
								(
										SELECT d.AccountReceivableId, d.ValueInCurrencyInvoice Value
										FROM [Portfolio].[PortfolioTransferDetail] d with (nolock)
										WHERE d.PortfolioTrasferId = @Id
									UNION ALL
										SELECT d.AccountReceivableId, d.ValueInCurrencyInvoice Value
										FROM @PortfolioTransferDetail d
								) d
								GROUP BY d.AccountReceivableId
							) d ON ar.Id = d.AccountReceivableId
							WHERE
							(
								(@Status <> 4 AND d.Value > ar.Balance)
								OR
								(@Status = 4 AND (d.Value + ar.Balance) > ar.Value)
							)
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT @CodeResult = 999, 
						   @MessageResult = ISNULL(@Message, ''), 
						   @Id = 0, 
						   @Code = '' 
					IF @ownTran = 1 ROLLBACK TRANSACTION
					RETURN 
				END

				--Validar que los detalles no superen el saldo (Si se confirma) o el valor inicial (Si se reversa) de la factura en la cuenta contable
				IF EXISTS
				(
					SELECT 1
					FROM
					(
						SELECT d.AccountReceivableId, d.MainAccountId, SUM(d.Value) Value
						FROM
						(
								SELECT d.AccountReceivableId, d.MainAccountId, d.ValueInCurrencyInvoice Value
								FROM [Portfolio].[PortfolioTransferDetail] d with (nolock)
								WHERE d.PortfolioTrasferId = @Id
							UNION ALL
								SELECT d.AccountReceivableId, d.MainAccountId, d.ValueInCurrencyInvoice Value
								FROM @PortfolioTransferDetail d
						) d
						GROUP BY d.AccountReceivableId, d.MainAccountId
					) d
					LEFT JOIN Portfolio.AccountReceivableAccounting ara with (nolock) ON d.AccountReceivableId = ara.AccountReceivableId AND d.MainAccountId = ara.MainAccountId
					WHERE
					(
						(@Status <> 4 AND ROUND(d.Value, 2) > ROUND(ISNULL(ara.Balance, 0), 2))
						OR
						(@Status = 4 AND ROUND(d.Value + ISNULL(ara.Balance, 0), 2) > ROUND(ISNULL(ara.Value, 0), 2))
					)
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - El ', IIF(@Status = 4, 'valor', 'saldo'), ' de la cuenta contable ', CONCAT(ma.Number, ' - ', ma.Name), ' de la factura ', ar.InvoiceNumber, ' (', IIF(@Status = 4, ara.Value, ara.Balance), ') es menor que el valor a trasladar (', d.Value, ')')
							FROM
							(
								SELECT d.AccountReceivableId, d.MainAccountId, SUM(d.Value) Value
								FROM
								(
										SELECT d.AccountReceivableId, d.MainAccountId, d.ValueInCurrencyInvoice Value
										FROM [Portfolio].[PortfolioTransferDetail] d with (nolock)
										WHERE d.PortfolioTrasferId = @Id
									UNION ALL
										SELECT d.AccountReceivableId, d.MainAccountId, d.ValueInCurrencyInvoice Value
										FROM @PortfolioTransferDetail d
								) d
								GROUP BY d.AccountReceivableId, d.MainAccountId
							) d
							LEFT JOIN Portfolio.AccountReceivableAccounting ara with (nolock) ON d.AccountReceivableId = ara.AccountReceivableId AND d.MainAccountId = ara.MainAccountId
							LEFT JOIN Portfolio.AccountReceivable ar with (nolock) ON d.AccountReceivableId = ar.Id
							LEFT JOIN GeneralLedger.MainAccounts ma with (nolock) ON d.MainAccountId = ma.Id
							WHERE
							(
								(@Status <> 4 AND d.Value > ISNULL(ara.Balance, 0))
								OR
								(@Status = 4 AND (d.Value + ISNULL(ara.Balance, 0)) > ISNULL(ara.Value, 0))
							)
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT @CodeResult = 999, 
						   @MessageResult = ISNULL(@Message, ''), 
						   @Id = 0, 
						   @Code = '' 
					IF @ownTran = 1 ROLLBACK TRANSACTION
					RETURN 
				END

				--Si la factura se encuentra glosada, se valida el estado de la glosa
				IF EXISTS
				( 
					SELECT 1
					FROM
					(
						SELECT ar.AccountReceivableId, SUM(ar.Value) Value
						FROM
						(
							SELECT d.AccountReceivableId, d.ValueInCurrencyInvoice Value
							FROM [Portfolio].[PortfolioTransferDetail] d with (nolock)
							WHERE d.PortfolioTrasferId = @Id
						UNION ALL
							SELECT d.AccountReceivableId, d.ValueInCurrencyInvoice Value
							FROM @PortfolioTransferDetail d
						) ar
						GROUP BY ar.AccountReceivableId
					) d
					JOIN Portfolio.AccountReceivable ar with (nolock) ON d.AccountReceivableId = ar.Id
					JOIN Glosas.GlosaPortfolioGlosada gpg with (nolock) ON ar.InvoiceNumber = gpg.InvoiceNumber
					WHERE @Status <> 4
						AND gpg.State IN ('1','3','4','5','6','7','15')
						AND ar.Balance - gpg.BalanceGlosa < d.Value
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - La factura ', ar.InvoiceNumber, ' no se puede pagar porque se encuentra ', 
									CASE gpg.State 
										WHEN 1 THEN 'Pendiente confirmar recepcion'  
										WHEN 3 THEN 'Pendiente envio de oficio'  
										WHEN 4 THEN 'Pendiente confirmar reiteracion'  
										WHEN 5 THEN 'Pendiente evaluacion reiteracion'  
										WHEN 6 THEN 'Pendiente conciliacion'  
										WHEN 7 THEN 'Pendiente confirmar factura conciliacion' 
										WHEN 15 THEN 'Factura cobro juridico'
									END, ' y el valor pagado (', FORMAT(d.Value, 'C2', 'es-CO'), ') supera el saldo sin glosa (', FORMAT(ar.Balance - gpg.BalanceGlosa, 'C2', 'es-CO'), ')')
							FROM
							(
								SELECT ar.AccountReceivableId, SUM(ar.Value) Value
								FROM
								(
									SELECT d.AccountReceivableId, d.ValueInCurrencyInvoice Value
									FROM [Portfolio].[PortfolioTransferDetail] d with (nolock)
									WHERE d.PortfolioTrasferId = @Id
								UNION ALL
									SELECT d.AccountReceivableId, d.ValueInCurrencyInvoice Value
									FROM @PortfolioTransferDetail d
								) ar
								GROUP BY ar.AccountReceivableId
							) d
							JOIN Portfolio.AccountReceivable ar with (nolock) ON d.AccountReceivableId = ar.Id
							JOIN Glosas.GlosaPortfolioGlosada gpg with (nolock) ON ar.InvoiceNumber = gpg.InvoiceNumber
							WHERE @Status <> 4
								AND gpg.State IN ('1','3','4','5','6','7','15')
								AND ar.Balance - gpg.BalanceGlosa < d.Value
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT @CodeResult = 999, 
							@MessageResult = ISNULL(@Message, ''), 
							@Id = 0, 
							@Code = '' 
					IF @ownTran = 1 ROLLBACK TRANSACTION
					RETURN
				END
			END

			/*************************************************************************************/

			IF @Status = 4
			BEGIN
				UPDATE [Portfolio].[PortfolioTransfer]
					SET [Status] = @Status,
						[ModificationUser] = @CodeUser,
						[ModificationDate] = [Common].[GETDATE](),
						[ReversalUser] = @CodeUser,
						[RecersalDate] = @DocumentDate
				WHERE Id = @Id
			END
			ELSE
			BEGIN
				DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @CodeUser ELSE NULL END
				DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

				IF @Id = 0
				BEGIN
					--Si se esta insertando por primera vez se consulta la secuencia numerica
					DECLARE @IsManual BIT
				
					EXEC Common.SP_GetSequence 160, @IdForm, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

					IF @Code_Output <> 0
					BEGIN
						SELECT	@CodeResult = 999, 
								@MessageResult = REPLACE(@Message_Output, '{0}', 'Cruce de Anticipo vs CxC'), 
								@Id = 0, 
								@Code = ''
						IF @ownTran = 1 ROLLBACK TRANSACTION
						RETURN
					END

					--Se inserta la cabecera
					INSERT INTO [Portfolio].[PortfolioTransfer]
					(
						[OperatingUnitId],[Code],[DocumentDate],[CustomerId],[ThirdPartyId],
						[PortfolioAdvanceId],[TransferType],[MainAccountId],[CostCenterId],[Observations],
						[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate]
					)
					SELECT @OperatingUnitId,@Code,@DocumentDate,@CustomerId,@ThirdPartyId,
						@PortfolioAdvanceId,@TransferType,@MainAccountId,@CostCenterId,@Observations,
						@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate

					--Obtengo el id de la cabcera
					SET @Id = SCOPE_IDENTITY()
				END
				ELSE --Si se esta actualizando
				BEGIN
					UPDATE [Portfolio].[PortfolioTransfer]
						SET [OperatingUnitId] = @OperatingUnitId,
							[Code] = @Code,
							[DocumentDate] = @DocumentDate,
							[CustomerId] = @CustomerId,
							[ThirdPartyId] = @ThirdPartyId,
							[PortfolioAdvanceId] = @PortfolioAdvanceId,
							[TransferType] = @TransferType,
							[MainAccountId] = @MainAccountId,
							[CostCenterId] = @CostCenterId,
							[Observations] = @Observations,
							[Status] = @Status,
							[ModificationUser] = @CodeUser,
							[ModificationDate] = [Common].[GETDATE](),
							[ConfirmationUser] = @ConfirmationUser,
							[ConfirmationDate] = @ConfirmationDate
					WHERE Id = @Id
				END

				/*************************************************************************************/

				UPDATE ptd 
					SET ptd.AccountReceivableId = d.AccountReceivableId, 
						ptd.MainAccountId = d.MainAccountId, 
						ptd.CostCenterId = d.CostCenterId, 
						ptd.Value = d.Value	,
						ptd.TRMValue= D.TRMValue,
						ptd.ValueInCurrencyInvoice = d.ValueInCurrencyInvoice
				FROM [Portfolio].[PortfolioTransferDetail] ptd with (nolock)
				JOIN @PortfolioTransferDetail d ON ptd.Id = d.Id
				JOIN Portfolio.AccountReceivable ar WITH(nolock) on d.AccountReceivableId= ar.Id
				JOIN Portfolio.AccountReceivableAccounting ara WITH(NOLOCK) on d.AccountReceivableId= ara.AccountReceivableId and ara.MainAccountId=d.MainAccountId
				WHERE ptd.PortfolioTrasferId = @Id

				INSERT INTO [Portfolio].[PortfolioTransferDetail] 
				(
					[PortfolioTrasferId],[AccountReceivableId],[MainAccountId],[CostCenterId],[Value],[TRMValue],[ValueInCurrencyInvoice]
				)
				SELECT @Id, AccountReceivableId, MainAccountId, d.CostCenterId, d.Value , d.TRMValue, d.ValueInCurrencyInvoice
				FROM @PortfolioTransferDetail d
				JOIN Portfolio.AccountReceivable ar WITH(nolock) on d.AccountReceivableId= ar.Id
				WHERE d.Id = 0

				/*************************************************************************************/

				UPDATE ptoc 
					SET ptoc.PortfolioNoteConceptId = d.PortfolioNoteConceptId, 
						ptoc.MainAccountId = d.MainAccountId, 
						ptoc.ThirdPartyId = d.ThirdPartyId,
						ptoc.CostCenterId = d.CostCenterId,
						ptoc.Nature = d.Nature, 
						ptoc.Value = d.Value					
				FROM [Portfolio].[PortfolioTransferOtherConcept] ptoc  with (nolock)
				JOIN @PortfolioTransferOtherConcept d ON ptoc.Id = d.Id
				WHERE ptoc.PortfolioTransferId = @Id

				INSERT INTO [Portfolio].[PortfolioTransferOtherConcept] 
				(
					[PortfolioTransferId],[PortfolioNoteConceptId],[MainAccountId],[ThirdPartyId],[CostCenterId], [Nature],[Value]
				)
				SELECT @Id, PortfolioNoteConceptId, MainAccountId, ThirdPartyId, CostCenterId, Nature, Value 
				FROM @PortfolioTransferOtherConcept 
				WHERE Id = 0
			END

			/*************************************************************************************/

			IF @Status IN (2, 4)
			BEGIN

			/*********************** AJUSTE DIFERENCIAL ANTICPOS ******************************/
				DELETE from @responseRevaluation
				SET @Message_DiffAdjustment = ''
				declare @ListPortfolioAdvance TABLE (	Id INT  NOT NULL,
														ValueAdjustment NUMERIC(20,2) NOT NULL,
														EntityName VARCHAR(250),
														EntityId INT,
														DocumentDate DATE)

					declare @ListPortfolioAdvanceXml as XML,
							@XmlOutput as XML
								
					INSERT INTO @ListPortfolioAdvance (Id,ValueAdjustment,EntityName,EntityId, DocumentDate)
					SELECT	pt.PortfolioAdvanceId,
							ABS(SUM(d.DebitValue - d.CreditValue)) ValueAdjustment,
							'PortfolioTransfer',
							@Id,
							@DocumentDate
					FROM Portfolio.PortfolioTransfer pt with (nolock)
					JOIN
					(
						SELECT @Id PortfolioTransferId, 0 DebitValue, d.Value CreditValue
						FROM Portfolio.PortfolioTransferDetail d with (nolock)
						WHERE d.PortfolioTrasferId = @Id
						UNION ALL
						SELECT @Id PortfolioTransferId, IIF(d.Nature = 1, d.Value, 0), IIF(d.Nature = 1, 0, d.Value) CreditValue
						FROM Portfolio.PortfolioTransferOtherConcept d with (nolock)
						WHERE d.PortfolioTransferId = @Id

					) d ON pt.Id = d.PortfolioTransferId
					WHERE pt.Id = @Id
					GROUP BY pt.PortfolioAdvanceId,pt.DocumentDate

					SELECT @ListPortfolioAdvanceXml = CONVERT(xml, 
																(
																	SELECT * FROM @ListPortfolioAdvance AS PortfolioAdvance 
																	For xml AUTO,TYPE, ELEMENTS
																))
				
					
					EXEC [Portfolio].[SP_PortfolioAdvanceRevaluation_Output]
							@ListPortfolioAdvanceXml,
							@CodeUser,@XmlOutput OUTPUT
						
					INSERT @responseRevaluation
					SELECT
					t.x.value('Code[1]', 'Varchar(20)')  Code,
					t.x.value('MessageOutput[1]', 'varchar(max)')  MessageOutput,
					t.x.value('JournalVoucherId[1]', 'INT')  JournalVoucherId
					from @XmlOutput.nodes('/TableResult') t(x);

					IF EXISTS(select 1 from @responseRevaluation) BEGIN
						set @Message_DiffAdjustment = (select CONCAT( 'Ajuste diferencial anticipo : ',STRING_AGG(MessageResult, ', '))   from @responseRevaluation)

						if EXISTS(select 1 from @responseRevaluation WHERE Code = '999')
						Begin	
						
							SELECT	@CodeResult = 999, 
									@MessageResult = CONCAT(@Message_DiffAdjustment, ' - ', 'Cruce de Anticipo vs CxC'), 
									@Id = 0, 
									@Code = ''
							IF @ownTran = 1 ROLLBACK TRANSACTION
							RETURN
						End 
					END

			/*********************** ************************ ******************************/

				-- Se actualiza el anticipo
				UPDATE pa
					SET pa.Balance = pa.Balance + (pt.Value * IIF(@Status = 4, -1, 1)),
						pa.TransferValue = pa.TransferValue - (pt.Value * IIF(@Status = 4, -1, 1))
				FROM Portfolio.PortfolioAdvance pa with (nolock)
				JOIN
				(
					SELECT pt.PortfolioAdvanceId, SUM(d.DebitValue - d.CreditValue) Value
					FROM Portfolio.PortfolioTransfer pt with (nolock)
					JOIN
					(
						SELECT @Id PortfolioTransferId, 0 DebitValue, d.Value CreditValue
						FROM Portfolio.PortfolioTransferDetail d with (nolock)
						WHERE d.PortfolioTrasferId = @Id
						UNION ALL
						SELECT @Id PortfolioTransferId, IIF(d.Nature = 1, d.Value, 0), IIF(d.Nature = 1, 0, d.Value) CreditValue
						FROM Portfolio.PortfolioTransferOtherConcept d with (nolock)
						WHERE d.PortfolioTransferId = @Id
					) d ON pt.Id = d.PortfolioTransferId
					WHERE pt.Id = @Id
					GROUP BY pt.PortfolioAdvanceId
				) pt ON pa.Id = pt.PortfolioAdvanceId

				IF EXISTS
				(
					SELECT 1 
					FROM Portfolio.PortfolioTransferDetail with (nolock)
					WHERE PortfolioTrasferId = @Id
				)
				BEGIN
					/********************************** PAGOS PARCIALES **********************************/

					IF @Status = 2 AND EXISTS
					( 
						SELECT 1
						FROM 
						(
							SELECT AccountReceivableId,SUM(ptd.ValueInCurrencyInvoice) Value
							FROM Portfolio.PortfolioTransferDetail ptd with (nolock)
							JOIN Portfolio.PortfolioTransfer pt WITH(NOLOCK) on pt.Id = ptd.PortfolioTrasferId
							WHERE pt.Id = @Id
							GROUP BY AccountReceivableId
						) ptd
						JOIN Portfolio.AccountReceivable ar with (nolock) ON ptd.AccountReceivableId = ar.Id
						JOIN Glosas.GlosaPortfolioGlosada gpg with (nolock) ON ar.InvoiceNumber = gpg.InvoiceNumber 
						WHERE gpg.BalanceGlosa > 0 
							AND (ptd.Value - (ar.Balance - gpg.BalanceGlosa)) > 0	-- Luego de pagar el saldo sin glosa, se valida que aun quede valor que pueda afectar el saldo de glosa
							AND gpg.State IN ('2','9','11','12','14') 
					)
					BEGIN
						--se declara una tabla temporal para hacer pagos parciales
						DECLARE @PartialPaymentCTmp TABLE
						(
							Id INT,
							CustomerId INT,
							DocumentDate DATETIME,
							State CHAR(1),
							Comments VARCHAR(250),
							EntityId INT,
							EntityCode VARCHAR(20),
							EntityName VARCHAR(250)
						)
					
						DECLARE @PartialPaymentDTmp TABLE
						(
							Id INT, 
							PartialPaymentsCId INT,
							PortfolioGlosaId INT,
							InvoiceNumber VARCHAR(50),
							InvoiceDate DATETIME,
							RadicatedNumber VARCHAR(50),
							RadicatedDate DATETIME,
							PatientCode VARCHAR(15),
							PatientName VARCHAR(200),
							ContractCode VARCHAR(15),
							ValuePendingConciliation DECIMAL(18,2),
							ValuePayments DECIMAL(18,2)
						)

						DECLARE @resultPartialPaymentC TABLE 
						(
							code VARCHAR(20),
							MessageResult VARCHAR(MAX)
						)

						INSERT INTO @PartialPaymentCTmp 
							SELECT 0,@CustomerId,@DocumentDate,2,'Creado desde cruce de anticipos por CXC',@Id,@Code,'PortfolioTransfer'

						INSERT INTO @PartialPaymentDTmp
							SELECT 0,0,gpg.Id,gpg.InvoiceNumber,ar.AccountReceivableDate,gpg.RadicatedNumber,gpg.RadicatedDate,gpg.PatientCode,gpg.PatientName,ISNULL(gpg.ContractCode, ''),gpg.BalanceGlosa,(ptd.Value - (ar.Balance - gpg.BalanceGlosa)) Value  
							FROM 
							(
								SELECT AccountReceivableId,SUM(ptd.ValueInCurrencyInvoice) Value
								FROM Portfolio.PortfolioTransferDetail ptd with (nolock)
								WHERE PortfolioTrasferId = @Id
								GROUP BY AccountReceivableId
							) ptd
							JOIN Portfolio.AccountReceivable ar with (nolock) ON ptd.AccountReceivableId = ar.Id
							JOIN Glosas.GlosaPortfolioGlosada gpg with (nolock) ON ar.InvoiceNumber = gpg.InvoiceNumber 
							WHERE gpg.BalanceGlosa > 0 
								AND (ptd.Value - (ar.Balance - gpg.BalanceGlosa)) > 0	-- Luego de pagar el saldo sin glosa, se valida que aun quede valor que pueda afectar el saldo de glosa
								AND gpg.State IN ('2','9','11','12','14')
		
						IF EXISTS (SELECT 1 FROM @PartialPaymentDTmp)
						BEGIN
							SELECT @SubXml = CONVERT
							(
								XML,
								(
									SELECT * 
									FROM @PartialPaymentCTmp PartialPaymentsC 
									JOIN @PartialPaymentDTmp PartialPaymentsD ON PartialPaymentsC.id = PartialPaymentsD.PartialPaymentsCId
									FOR XML AUTO,TYPE, ELEMENTS
								)
							)
					
							--ejecuto el sp de pagos parciales
							INSERT @resultPartialPaymentC 
								EXEC Glosas.SP_GeneratePartialPayments  @SubXml, @CodeUser

							IF (SELECT code FROM @resultPartialPaymentC) = '999' BEGIN
								SELECT @CodeResult = 999, 
										@MessageResult = ISNULL(MessageResult, ''), 
										@Id = 0, 
										@Code = '' 
								FROM @resultPartialPaymentC

								IF @ownTran = 1 ROLLBACK TRANSACTION
								RETURN
							END
						END
					END

					/******************************** PRESUPUESTO *******************************/

					IF @Status = 2
					BEGIN
						/******************************** RECAUDO PRESUPUESTAL *******************************/

						EXEC [Portfolio].[SP_GenerateCollectionByPortfolioTransferId_Output] @OperatingUnitId, @Id, @CodeUser, @Code_Output OUT, @Message_Output OUT
						IF @Code_Output <> 0
						BEGIN
							SELECT	@CodeResult = 999,
									@MessageResult = ISNULL(@Message_Output, 'No se pudo generar el recaudo presupuestal'),
									@Id = 0,
									@Code = ''
							IF @ownTran = 1 ROLLBACK TRANSACTION
							RETURN
						END

						SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', CHAR(13) + CHAR(10) + @Message_Output)
					END
					ELSE IF @Status = 4
					BEGIN
						/******************************** MODIFICACION RECAUDO PRESUPUESTAL *******************************/

						EXEC [Portfolio].[SP_ReverseCollectionByPortfolioTransferId_Output] @OperatingUnitId, @PortfolioNoteId, @CodeUser, @Code_Output OUT, @Message_Output OUT
						IF @Code_Output <> 0
						BEGIN
							SELECT	@CodeResult = 999,
									@MessageResult = ISNULL(@Message_Output, 'No se pudo generar la modificación del recaudo presupuestal'),
									@Id = 0,
									@Code = ''
							IF @ownTran = 1 ROLLBACK TRANSACTION
							RETURN
						END

						SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', CHAR(13) + CHAR(10) + @Message_Output)
					END

					/******************************** COMPROBANTE CONTABLE *******************************/

					DECLARE @ParameterXML as xml  = '<Parameters></Parameters>'
					set @ParameterXML.modify('insert	
												<EntityName>{sql:variable("@EntityName") }</EntityName>
												into (/Parameters)[1]')

					EXEC [Portfolio].[SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad] @Id, @CodeUser, @ParameterXML, @Code_Output OUT, @Message_Output OUT

					IF @Code_Output <> 0
					BEGIN

						SELECT	@CodeResult = 999,
								@MessageResult = ISNULL(@Message_Output, 'No se pudo generar el comprobante contable del cruce de anticipo'),
								@Id = 0,
								@Code = ''
						IF @ownTran = 1 ROLLBACK TRANSACTION
						RETURN
					END

					SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', CHAR(13) + CHAR(10) + @Message_Output)

					/********************************** ACTUALIZACIONES **********************************/
						/* Ajuste diferencial por documento de cuentas por cobrar */
							declare @_DiffAccountReceivableId int,
									@_DiffValueAdjustment DECIMAL(20,2)

							DECLARE Revaluation_Cursor CURSOR FOR  
						
									SELECT	ar.Id,
											IIF(Common.CurrencyConverterByModule(	ar.Balance,	isnull(ar.CurrencyId,@OfficialCurrency),@CurrencyIdAdvanced,NULL,@EntityName,CAST(Common.GETDATE() as DATE)) = ptd.ValueTransfer,
																					ar.Balance,ptd.Value) as  ValueAdjustment
									FROM Portfolio.AccountReceivable ar with (nolock)
									JOIN 
									(
										SELECT ptd.AccountReceivableId, SUM(ptd.ValueInCurrencyInvoice) Value, sum(ptd.Value) ValueTransfer
										FROM Portfolio.PortfolioTransferDetail ptd with (nolock)
										JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) on ptd.AccountReceivableId =ar.Id
										WHERE ptd.PortfolioTrasferId = @Id
										GROUP BY ptd.AccountReceivableId 
									) ptd ON ar.Id = ptd.AccountReceivableId

							OPEN Revaluation_Cursor  
							FETCH NEXT FROM Revaluation_Cursor   
							INTO @_DiffAccountReceivableId,@_DiffValueAdjustment
  
							WHILE @@FETCH_STATUS = 0
							BEGIN

								DELETE FROM @responseRevaluation

								INSERT @responseRevaluation
								EXEC [Portfolio].[SP_AccountReceivableRevaluation]
									@_DiffAccountReceivableId,
									@_DiffValueAdjustment,
									@Code,
									@Id,
									'PortfolioTransfer',
									@CodeUser,
									@DocumentDate

								if (select COUNT(*) from @responseRevaluation WHERE Code = '999') > 0
								Begin
									set @Message_Output = (select STRING_AGG(MessageResult, ', ') from @responseRevaluation)

									CLOSE Revaluation_Cursor;
									DEALLOCATE Revaluation_Cursor;

									SELECT	@CodeResult = 999,
									@MessageResult = ISNULL(@Message_Output, 'No se pudo generar el ajuste diferencial cxc'),
									@Id = 0,
									@Code = ''
									IF @ownTran = 1 ROLLBACK TRANSACTION
									RETURN
								End

								SELECT @Message_Output = STRING_AGG(MessageResult, ', ')
								FROM @responseRevaluation
								WHERE ISNULL(MessageResult, '') <> ''

								IF ISNULL(@Message_Output, '') <> ''
									SET @Message_DiffAdjustment = CONCAT(@Message_DiffAdjustment, IIF(ISNULL(@Message_DiffAdjustment, '') = '', '', ' - '), @Message_Output)

							NEXT_ROW:
							FETCH NEXT FROM Revaluation_Cursor   
							INTO @_DiffAccountReceivableId,@_DiffValueAdjustment

							END
							CLOSE Revaluation_Cursor;  
							DEALLOCATE Revaluation_Cursor;

						/*------------------------------------------------------------*/

					UPDATE ar
						SET ar.Balance = ar.Balance - (	IIF(Common.CurrencyConverterByModule(ar.Balance,isnull(ar.CurrencyId,@OfficialCurrency),@CurrencyIdAdvanced,NULL,@EntityName,CAST(Common.GETDATE() as DATE)) = ptd.ValueTransfer,ar.Balance,ptd.Value) * IIF(@Status = 4, -1, 1))
					FROM Portfolio.AccountReceivable ar with (nolock)
					JOIN 
					(
						SELECT ptd.AccountReceivableId, SUM(ptd.ValueInCurrencyInvoice) Value, sum(ptd.Value) ValueTransfer
						FROM Portfolio.PortfolioTransferDetail ptd with (nolock)
						JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) on ptd.AccountReceivableId =ar.Id
						WHERE ptd.PortfolioTrasferId = @Id
						GROUP BY ptd.AccountReceivableId 
					) ptd ON ar.Id = ptd.AccountReceivableId

					UPDATE ara
						SET ara.Balance = ara.Balance - (IIF(Common.CurrencyConverterByModule(ara.Balance,isnull(ar.CurrencyId,@OfficialCurrency),@CurrencyIdAdvanced,NULL,@EntityName,cast(Common.GETDATE() as date)) = ptd.ValueTransfer,ara.Balance,ptd.Value) * IIF(@Status = 4, -1, 1))
					FROM Portfolio.AccountReceivableAccounting ara with (nolock)
					JOIN 
					(
						SELECT ptd.AccountReceivableId, ptd.MainAccountId, SUM(ptd.ValueInCurrencyInvoice) Value, sum(ptd.Value) ValueTransfer
						FROM Portfolio.PortfolioTransferDetail ptd with (nolock)
						JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) on ptd.AccountReceivableId =ar.Id
						WHERE ptd.PortfolioTrasferId = @Id
						GROUP BY ptd.AccountReceivableId, ptd.MainAccountId
					) ptd ON ara.AccountReceivableId = ptd.AccountReceivableId AND ara.MainAccountId = ptd.MainAccountId
					JOIN Portfolio.AccountReceivable ar with (nolock) on ara.AccountReceivableId=ar.Id

					UPDATE ars
						SET ars.Balance = ars.Balance - (IIF(Common.CurrencyConverterByModule(ars.Balance,isnull(ar.CurrencyId,@OfficialCurrency),@CurrencyIdAdvanced,NULL,@EntityName,CAST(Common.GETDATE() as DATE)) = ptd.ValueTransfer,ars.Balance,ptd.Value) * IIF(@Status = 4, -1, 1)),
							ars.TransferValue = ars.TransferValue + (ptd.Value * IIF(@Status = 4, -1, 1))
					FROM Portfolio.AccountReceivable ar with (nolock)
					JOIN Portfolio.AccountReceivableShare ars with (nolock) ON ar.Id = ars.AccountReceivableId
					JOIN 
					(
						SELECT ptd.AccountReceivableId, SUM(ptd.ValueInCurrencyInvoice) Value, SUM(ptd.Value) ValueTransfer
						FROM Portfolio.PortfolioTransferDetail ptd with (nolock)
						JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) on ptd.AccountReceivableId =ar.Id
						WHERE ptd.PortfolioTrasferId = @Id
						GROUP BY ptd.AccountReceivableId
					) ptd ON ar.Id = ptd.AccountReceivableId
					WHERE ar.NumberShares = 1

					UPDATE gpg SET gpg.BalanceInvoice = ar.Balance
					FROM 
					(
						SELECT ptd.AccountReceivableId, CAST(SUM(ptd.ValueInCurrencyInvoice) AS DECIMAL(18,2)) Value
						FROM Portfolio.PortfolioTransferDetail ptd with (nolock)
						JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) on ptd.AccountReceivableId =ar.Id
						WHERE ptd.PortfolioTrasferId = @Id
						GROUP BY ptd.AccountReceivableId 
					) ptd
					JOIN Portfolio.AccountReceivable ar with (nolock) ON ptd.AccountReceivableId = ar.Id				
					JOIN Glosas.GlosaPortfolioGlosada gpg with (nolock) ON ar.InvoiceNumber = gpg.InvoiceNumber 
					WHERE @Status <> 4
						AND gpg.State IN ('1') 

					DECLARE @PortfolioTransferDetailRows INT = 1,
							@PortfolioTransferDetailId INT = 0,
							@AccountReceivableId INT,
							@PortfolioTransferDetailValue DECIMAL(18,2),
							--------------------------------------
							@AccountReceivableShareRows INT,
							@AccountReceivableShareId INT,
							@PortfolioTransferDetailIAccountShareId INT,
							--------------------------------------
							@TransferValue DECIMAL(18,2),
							@TransferValueInCurrencyInvoice DECIMAL(18,2),
							@ValueInCurrencyInvoice DECIMAL(18,2),
							@CurrencyIdInvoice INT

					WHILE @PortfolioTransferDetailRows > 1
					BEGIN
						SELECT TOP 1 
							@PortfolioTransferDetailId = ptd.Id,
							@AccountReceivableId = ptd.AccountReceivableId, 
							@PortfolioTransferDetailValue = ptd.Value,
							@ValueInCurrencyInvoice= ptd.ValueInCurrencyInvoice,
							--------------------------------------
							@AccountReceivableShareRows = 1,
							@AccountReceivableShareId = 0,
							@CurrencyIdInvoice = ISNULL(ar.CurrencyId,@OfficialCurrency)
						FROM Portfolio.PortfolioTransferDetail ptd with (nolock)
						JOIN Portfolio.AccountReceivable ar with (nolock) ON ptd.AccountReceivableId = ar.Id
						LEFT JOIN Portfolio.PortfolioTransferDetailIAccountShare ptds with (nolock) ON ptd.Id = ptds.PortfolioTransferDetailId
						WHERE ptd.PortfolioTrasferId = @Id AND ar.NumberShares > 1
							AND ptd.Id > @PortfolioTransferDetailId
							AND
							(
								@Status <> 4 OR ptds.Id IS NOT NULL
							)
						ORDER BY ptd.Id

						SET @PortfolioTransferDetailRows = @@ROWCOUNT
						IF @PortfolioTransferDetailRows = 0 
						BEGIN
							BREAK
						END

						WHILE @AccountReceivableShareRows > 1
						BEGIN
							SELECT TOP 1 
								@AccountReceivableShareId = ars.Id,
								@PortfolioTransferDetailIAccountShareId = ptds.Id,
								@TransferValue = IIF(@Status = 4,	ptds.Value,
																	Common.CalculateAdjustedValue(@PortfolioTransferDetailValue, @PortfolioTransferDetailValue, common.CurrencyConverterByModule(ars.Balance,@CurrencyIdInvoice,@CurrencyIdAdvanced,NULL,@EntityName,CAST(Common.GETDATE() as DATE)))),
								@TransferValueInCurrencyInvoice = IIF(@Status = 4,	Common.CurrencyConverterByModule(ptds.Value,@CurrencyIdAdvanced,@CurrencyIdInvoice,NULL, @EntityName,CAST(Common.GETDATE() as DATE)),
																					Common.CalculateAdjustedValue(@ValueInCurrencyInvoice,@ValueInCurrencyInvoice,ars.Balance))
							FROM Portfolio.AccountReceivableShare ars with (nolock)
							LEFT JOIN Portfolio.PortfolioTransferDetailIAccountShare ptds with (nolock) ON @PortfolioTransferDetailId = ptds.PortfolioTransferDetailId AND ars.Id = ptds.AccountReceivableShareId
							WHERE ars.AccountReceivableId = @AccountReceivableId
								AND ars.Id > @AccountReceivableShareId
								AND
								(
									@Status <> 4 OR ptds.Id IS NOT NULL
								)
							ORDER BY ars.Id

							SET @AccountReceivableShareRows = @@ROWCOUNT
							IF @AccountReceivableShareRows = 0 OR @PortfolioTransferDetailValue = 0
							BEGIN
								BREAK
							END

							UPDATE Portfolio.AccountReceivableShare
								SET Balance = Balance - ((@TransferValueInCurrencyInvoice) * IIF(@Status = 4, -1, 1)),
									TransferValue = TransferValue + ((@TransferValueInCurrencyInvoice) * IIF(@Status = 4, -1, 1))
							WHERE Id = @AccountReceivableShareId

							IF @Status = 4
							BEGIN
								UPDATE [Portfolio].[PortfolioTransferDetailIAccountShare]
									SET Value = Value - @TransferValue
								WHERE Id = @PortfolioTransferDetailIAccountShareId
							END
							ELSE
							BEGIN
								INSERT INTO [Portfolio].[PortfolioTransferDetailIAccountShare]
								(
									[PortfolioTransferDetailId],[AccountReceivableId],[AccountReceivableShareId],[Value]
								)
								VALUES
								(
									@PortfolioTransferDetailId, @AccountReceivableId, @AccountReceivableShareId, @TransferValue
								)
							END

							SET @PortfolioTransferDetailValue = @PortfolioTransferDetailValue - @TransferValue
						END
					END
				END
			END
		END

		/************************************* TABLA DE CONTROL ************************************/

		IF @Status = 1
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM Portfolio.PortfolioControl with (nolock) WHERE DocumentType = @DocumentType AND DocumentNumber = @Code)
			BEGIN
				INSERT INTO Portfolio.PortfolioControl (DocumentNumber, DocumentType, DocumentUser, DocumentDate)
				SELECT @Code, @DocumentType, @CodeUser, @DocumentDate
			END
		END
		ELSE
		BEGIN
			DELETE FROM Portfolio.PortfolioControl  WHERE DocumentType = @DocumentType AND DocumentNumber = @Code
		END	

		/**************************************** RESULTADO ****************************************/

		IF @ownTran = 1 COMMIT TRANSACTION
		SELECT @CodeResult = 0,
			   @MessageResult = CASE @Status
								   WHEN 2 THEN CONCAT('Se guardó y confirmó el Cruce de Anticipo vs CxC con código ', @Code)
								   WHEN 3 THEN CONCAT('Se anuló el Cruce de Anticipo vs CxC con código ', @Code)
								   WHEN 4 THEN CONCAT('Se reversó el Cruce de Anticipo vs CxC con código ', @Code)
								   ELSE CONCAT('Se guardó el Cruce de Anticipo vs CxC con código ', @Code)
							   END + ISNULL(@Message, '') + IIF(ISNULL(@Message_DiffAdjustment, '') = '', '', ' - ' + @Message_DiffAdjustment)
	END TRY
	BEGIN CATCH
		DECLARE @ErrorNumber INT = ERROR_NUMBER(),
				@ErrorSeverity INT = ERROR_SEVERITY(),
				@ErrorState INT = ERROR_STATE(),
				@ErrorProcedure SYSNAME = ISNULL(ERROR_PROCEDURE(), OBJECT_NAME(@@PROCID)),
				@ErrorLine INT = ERROR_LINE(),
				@ErrorMessage VARCHAR(MAX) = ERROR_MESSAGE()

		SELECT	@CodeResult = 999,
				@MessageResult = CONCAT(
					'Error en ', @ErrorProcedure,
					' - Linea: ', @ErrorLine,
					' - Numero: ', @ErrorNumber,
					' - Estado: ', @ErrorState,
					' - Severidad: ', @ErrorSeverity,
					' - Mensaje: ', @ErrorMessage
				),
				@Id = 0,
				@Code = ''
		IF XACT_STATE() <> 0 AND @ownTran = 1
			ROLLBACK TRANSACTION
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que guarda y confirma el cruce de anticipos contra cuentas por cobrar (CxC) en el módulo de cartera. Recibe los datos del traslado en formato XML e identifica si la operación corresponde a una confirmación, anulación o reversión del cruce, actualizando el estado del traslado y las notas de cartera asociadas. Valida que existan parámetros de cartera configurados para la unidad operativa, que el anticipo y la CxC estén en el estado correcto para ser procesados, y registra la trazabilidad del usuario y fecha de cada acción. Interactúa con las tablas de traslados de cartera, detalle de traslados, notas de cartera y configuración contable para garantizar la consistencia del cruce de anticipos en la cartera de la institución.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SavePortfolioTransfer_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SavePortfolioTransfer_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cruce de anticipo vs CxC; Anticipo de cartera; Cuenta por cobrar (factura); Cuotas de cartera (AccountReceivableShare); Glosas de facturas (EPS/EAPB); Pagos parciales por glosa; Recaudo presupuestal; Comprobante contable (Journal Voucher); Ajuste diferencial cambiario (revaluación); TRM / conversión de moneda; Periodo contable cerrado; Centro de costo; Tercero (NIT); Plan de cuentas (PUC); Otros conceptos (naturaleza débito/crédito)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioTransfer_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Status = 3 (anulación) → Solo se actualiza PortfolioTransfer marcando AnnulmentUser/AnnulmentDate y se elimina de PortfolioControl; si @Status = 4 (reversión) → Exige que el traslado esté en estado 2 (Confirmado) y exista PortfolioNote en estado 2; actualiza PortfolioTransfer con ReversalUser y RecersalDate; ejecuta SP_ReverseCollectionByPortfolioTransferId_Output else Se exige que el traslado esté en estado 1 (Registrado) para poder modificarlo; si @Status = 2 (confirmación) → Asigna ConfirmationUser/ConfirmationDate, ejecuta SP_GenerateCollectionByPortfolioTransferId_Output (recaudo presupuestal) y, si hay glosas activas, dispara Glosas.SP_GeneratePartialPayments; si @Id = 0 (nuevo registro) → Obtiene secuencia con Common.SP_GetSequence (form 687) e INSERTA cabecera en Portfolio.PortfolioTransfer else UPDATE de la cabecera existente; si @CurrencyIdAdvanced = 0 OR NULL → Se asume @OfficialCurrency de GeneralLedger.CompanySettings; si @Status IN (2,4) y existen detalles en PortfolioTransferDetail → Ejecuta ajuste diferencial de anticipos (SP_PortfolioAdvanceRevaluation_Output), genera comprobante contable (SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad) y aplica revaluación por cada AccountReceivable vía cursor con SP_AccountReceivableRevaluation; si ar.NumberShares > 1 → Distribuye el valor del traslado entre las cuotas (AccountReceivableShare) usando Common.CalculateAdjustedValue, insertando o actualizando PortfolioTransferDetailIAccountShare según sea confirmación o reversión; si Factura tiene glosa con BalanceGlosa>0 y State IN (2,9,11,12,14) y el valor a pagar excede el saldo sin glosa (solo @Status=2) → Genera pagos parciales mediante Glosas.SP_GeneratePartialPayments; si Factura glosada con State IN (1,3,4,5,6,7,15) y ar.Balance - gpg.BalanceGlosa < d.Value → Rechaza con código 999 indicando que la factura está en proceso de glosa y el valor supera el saldo sin glosa', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioTransfer_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence; Portfolio.SP_PortfolioAdvanceRevaluation_Output; Glosas.SP_GeneratePartialPayments; Portfolio.SP_GenerateCollectionByPortfolioTransferId_Output; Portfolio.SP_ReverseCollectionByPortfolioTransferId_Output; Portfolio.SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad; Portfolio.SP_AccountReceivableRevaluation', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioTransfer_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Portfolio.SettingPortfolio; Common.OperatingUnit; Portfolio.PortfolioTransfer; Portfolio.PortfolioNote; Portfolio.PortfolioTransferDetail; Portfolio.AccountReceivable; Portfolio.AccountReceivableAccounting; Portfolio.PortfolioTransferOtherConcept; GeneralLedger.ClosedMonth; GeneralLedger.MainAccounts; Portfolio.PortfolioNoteConcept; Common.ThirdParty; Payroll.CostCenter; Common.Currency; Portfolio.PortfolioAdvance; Glosas.GlosaPortfolioGlosada; Portfolio.AccountReceivableShare; Portfolio.PortfolioTransferDetailIAccountShare', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioTransfer_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioTransfer_Output';
-- GO