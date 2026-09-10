
CREATE PROCEDURE [Glosas].[SP_ReverseJuridicalTransfer]
(
@Id int
,@IdOperatingUnit INT
, @CodeUser varchar(20)
, @GenerateReclassification bit
)
AS 
BEGIN
	--SET XACT_ABORT ON;
	SET NOCOUNT ON

	DECLARE @Reclassified bit
	DECLARE @SequenceId int
	DECLARE @pattern VARCHAR(300)
	DECLARE @NextS BIGINT
	DECLARE @Message VARCHAR(MAX) = ''
	DECLARE @TimeParametersId TINYINT
	DECLARE @JournalVoucherTypeId INT
	DECLARE @LegalBookId integer
	DECLARE @JournalVoucherXML XML
	DECLARE @AccountReceivableId int
	DECLARE @InvoiceNumber varchar(50)
	DECLARE @Status CHAR(1)

	DECLARE @TJD AS TABLE
	(Id int
	,AccountReceivableId int NULL
	,InvoiceNumber varchar(50)
	,LegalTransferValue MONEY NULL
	,InvoiceBalance numeric(18,2)
	,CanReversed bit
	,Mensaje varchar(8000) NULL
	,CostCenterId INT NULL
	,ThirdPartyId INT
	,AccountWithoutRadicateId  INT NULL
	,AccountRadicateId  INT NULL
	,AccountObjectionRemediedId  INT NULL
	,AccountConciliationId INT NULL
	,AccountLegalCollectionId INT NULL)

	DECLARE @GPG AS TABLE
	(Id INT
	,InvoiceNumber varchar(50)
	,LegalTransferValue money
	,TempState TINYINT
	,State TINYINT
	)

	DECLARE @GMG AS TABLE 
	(
	Id INT
	,InvoiceNumber varchar(50)
	,LegalTransferValue money
	)

	DECLARE @Reclassification Table
	(
	Code varchar(20) NULL
	,DocumentType tinyint
	,AccountReceivableId int
	,SourceAccountId int
	,TargetAccountId int
	,[Value] numeric(18,0)
	,[Status] tinyint
	,CreationUser varchar(20)
	,CreationDate datetime
	,ModificationUser varchar(20)
	,ModificationDate datetime
	,ConfirmationUser varchar(20)
	,ConfirmationDate datetime
	,InvoiceNumber varchar(50)
	)

	DECLARE @OutputTbl TABLE
	(
	ReclassificationId int
	,AccountReceivableId int
	,Code varchar(20)
	)

	--Se declara una tabla con los datos para la cabecera del comprobante contable 
	DECLARE @JournalVourcherTmp TABLE (
		Id INT, 
		Consecutive BIGINT,
		LegalBookId INT, 
		IdJournalVoucher INT, 
		VoucherDate VARCHAR(30),
		Imported VARCHAR(5),
		Status TINYINT,
		Detail VARCHAR(500),
		EntityCode VARCHAR(20),
		EntityId INT,
		EntityName VARCHAR(250),
		IsClosedYear VARCHAR(5)
		,AccountReceivableId int
	)

	--Se declara una tabla temporal para los detalles del comprobante
	DECLARE @JournalVourcherDetailTmp TABLE (
		Id INT,
		IdAccounting INT,
		IdMainAccount INT,
		IdThirdParty INT,
		IdCostCenter INT,
		DebitValue DECIMAL(20,4),
		CreditValue DECIMAL(20,4),
		Detail VARCHAR(500),
		IdRetention INT,
		RetentionRate decimal(5,3),
		BaseValue decimal(18,2),
		BillingValue decimal(18,2)
		,AccountReceivableId int
	)

	--Tabla temporal para guardar el resultado del save del comprobante contable
	DECLARE @resultJournalVoucher TABLE (
		code varchar(20),
		MessageResult varchar(max),
		IdJournalVoucher integer
	)

	DECLARE @result TABLE (
		code varchar(20),
		MessageResult varchar(max),
		IdJournalVoucher integer,
		InvoiceNumber varchar(50)
	)

	--Datos de la reclasificación
	select @Reclassified = TJ.Reclassified, @Status = TJ.[State] from Glosas.TransferJuridicalDebtCollectionC TJ
	WHERE TJ.Id = @Id

	IF @Status = 3
	BEGIN
		INSERT INTO @result(code, MessageResult, IdJournalVoucher, InvoiceNumber)
		SELECT '999', 'El traslado juridico ya se reverso', 0, ''
		SELECT code, MessageResult, IdJournalVoucher, InvoiceNumber FROM @result
		RETURN
	END

	if @Reclassified = 1 --si hizo reclasificacion de cuenta
	begin
		INSERT INTO @TJD(Id, AccountReceivableId, InvoiceNumber, LegalTransferValue, InvoiceBalance, CanReversed, CostCenterId, ThirdPartyId, AccountWithoutRadicateId
			,AccountRadicateId, AccountObjectionRemediedId, AccountConciliationId, AccountLegalCollectionId)
		SELECT TJD.Id, ISNULL(TJD.AccountReceivableId, AR.ID), TJD.InvoiceNumber, TJD.LegalTransferValue, ISNULL(AR.Balance, 0), cast(1 as bit)
		, AR.CostCenterId, AR.ThirdPartyId, AR.AccountWithoutRadicateId
		, AR.AccountRadicateId, AR.AccountObjectionRemediedId, AR.AccountConciliationId, AR.AccountLegalCollectionId
		FROM Glosas.TransferJuridicalDebtCollectionD TJD
		LEFT OUTER JOIN Portfolio.AccountReceivable AR
		ON (TJD.AccountReceivableId IS NULL AND AR.InvoiceNumber = TJD.InvoiceNumber) OR (TJD.AccountReceivableId IS NOT NULL AND AR.Id = TJD.AccountReceivableId)
		WHERE
		TJD.TransferJuridicalDebtCollectionCId = @Id
		AND TJD.ReversedUser IS NULL

		if (select count(1) from @TJD) = 0
		begin
			INSERT INTO @result(code, MessageResult, IdJournalVoucher, InvoiceNumber)
			SELECT '999', 'No se encontro información', 0, ''
			SELECT code, MessageResult, IdJournalVoucher, InvoiceNumber FROM @result
			RETURN
		end
				
		UPDATE @TJD SET CanReversed = 0, Mensaje = 'La factura ' + [@TJD].InvoiceNumber + 'no tiene saldo' WHERE [@TJD].InvoiceBalance <= 0

		INSERT INTO @GPG(Id, InvoiceNumber, LegalTransferValue, TempState, [State])
		SELECT G.Id, G.InvoiceNumber, G.LegalTransferValue, G.TempState, G.[State]
		from @TJD
		INNER JOIN Glosas.GlosaPortfolioGlosada G
		ON G.InvoiceNumber = [@TJD].InvoiceNumber
		AND G.LegalTransferValue > 0
		AND [@TJD].CanReversed = 1

		INSERT INTO @GMG(Id, InvoiceNumber, LegalTransferValue)
		SELECT G.Id, G.InvoiceNumber, G.LegalTransferValue
		from @TJD
		INNER JOIN Glosas.GlosaMovementGlosa G
		ON G.InvoiceNumber = [@TJD].InvoiceNumber
		AND G.LegalTransferValue > 0
		AND [@TJD].CanReversed = 1

		UPDATE @TJD set CanReversed = 0, Mensaje = 'El saldo de la factura ' + [@TJD].InvoiceNumber + ' es menor al valor glosado ' + CONVERT(VARCHAR(12), T.ValorGlosa)
		from @TJD
		inner join
		(select [@GPG].InvoiceNumber, ValorGlosa = SUM(LegalTransferValue) from @GPG GROUP BY [@GPG].InvoiceNumber) T
		ON T.InvoiceNumber = [@TJD].InvoiceNumber
		AND T.ValorGlosa > [@TJD].InvoiceBalance

		UPDATE @TJD set CanReversed = 0, Mensaje = 'El saldo de la factura ' + [@TJD].InvoiceNumber + ' es menor al valor pendiente de conciliar ' + CONVERT(VARCHAR(12), T.ValorPendienteConcilia)
		from @TJD
		inner join
		(select [@GMG].InvoiceNumber, ValorPendienteConcilia = SUM(LegalTransferValue) from @GMG GROUP BY [@GMG].InvoiceNumber) T
		ON T.InvoiceNumber = [@TJD].InvoiceNumber
		AND T.ValorPendienteConcilia > [@TJD].InvoiceBalance

		UPDATE @TJD set CanReversed = 0, Mensaje = 'La cuenta de traslado a cobro jurídico esta vacia para la factura: ' + [@TJD].InvoiceNumber
		WHERE 
		[@TJD].CanReversed = 1
		AND [@TJD].AccountLegalCollectionId IS NULL

		If @GenerateReclassification = 1
		BEGIN
			UPDATE @TJD set CanReversed = 0, Mensaje = 'Para la factura: ' + [@TJD].InvoiceNumber + ' el último proceso de reclasificación no corresponde a un traslado jurídico '
			FROM @TJD
			INNER JOIN
			(
			SELECT 
			ROW_NUMBER() OVER (PARTITION BY ARA.AccountReceivableId ORDER BY ARA.Id DESC) IdParticion
			,ARA.AccountReceivableId
			,ARA.MainAccountId
			FROM @TJD
			INNER JOIN Portfolio.AccountReceivableAccounting ARA
			ON ARA.AccountReceivableId = [@TJD].AccountReceivableId
			AND [@TJD].CanReversed = 1
			) T
			ON 
			T.IdParticion = 1
			AND T.AccountReceivableId = [@TJD].AccountReceivableId
			AND T.MainAccountId <> [@TJD].AccountLegalCollectionId
			WHERE
			[@TJD].CanReversed = 1
		END

		IF (SELECT COUNT(1) FROM @TJD where [@TJD].CanReversed = 1) > 0
		BEGIN
		
			 If @GenerateReclassification = 1 
			 BEGIN
				SELECT @SequenceId = CASE WHEN PS.SCOPE = 'O' THEN SE.Id
						WHEN PS.Scope = 'OU' THEN SEOU.Id
						ELSE 0
					END 
					,@pattern = S.Pattern
					,@NextS = CASE WHEN PS.SCOPE = 'O' THEN SE.Next
						WHEN PS.Scope = 'OU' THEN SEOU.Next
						ELSE -1
					END 
				FROM Portfolio.PortfolioSequence PS
				LEFT OUTER JOIN Portfolio.PortfolioSequenceDetail SE
				ON SE.IdSequensePortfolioC = PS.Id
				AND SE.IdOperatingUnit IS NULL
				LEFT OUTER JOIN Portfolio.PortfolioSequenceDetail SEOU
				ON SE.IdSequensePortfolioC = PS.Id
				AND SE.IdOperatingUnit = @IdOperatingUnit
				LEFT OUTER JOIN Common.Sequense S
				ON S.Id = CASE WHEN PS.SCOPE = 'O' THEN SE.IdSequense
							WHEN PS.Scope = 'OU' THEN SEOU.IdSequense
							ELSE 0
						END 
				WHERE PS.IdForm = '509'
				AND PS.Sequential = 1

				if @SequenceId = 0 or @SequenceId is null
				BEGIN
					SET @Message = 'No se encontró la secuencia para el formulario Radicación de Cuentas'
				END
				ELSE
				BEGIN
					SELECT 
					@TimeParametersId = TP.Id
					,@JournalVoucherTypeId = TP.TransferLegalJournalVoucherTypeId
					FROM Glosas.TimeParameters TP
					WHERE TP.IdOperatingUnit = @IdOperatingUnit
					if @TimeParametersId = 0 OR @TimeParametersId is NULL
					BEGIN
						SET @Message = 'No se encontrarón parametros de glosas'
					END
					ELSE
					BEGIN
						IF @JournalVoucherTypeId = 0 OR @JournalVoucherTypeId is NULL
						BEGIN
							SET @Message = 'El tipo de documento contable para traslado a cobro juridico esta vacio'
						END
					END
				END

				IF LEN(@Message) = 0 
				BEGIN
								
					select @LegalBookId = id  from GeneralLedger.LegalBook where OfficialBook = 1

					INSERT INTO @Reclassification (DocumentType,AccountReceivableId,SourceAccountId,TargetAccountId,[Value],[Status],CreationUser,CreationDate
					,ModificationUser,ModificationDate,ConfirmationUser,ConfirmationDate,InvoiceNumber)
					SELECT 
					CAST(7 AS TINYINT) --Reversion traslado juridico
					,PR.AccountReceivableId
					,PR.TargetAccountId
					,PR.SourceAccountId
					,case when pr.SourceAccountId in (T.AccountWithoutRadicateId,T.AccountRadicateId) THEN T.InvoiceBalance - ISNULL(GM.ValorGlosa, 0)
							WHEN pr.SourceAccountId = T.AccountObjectionRemediedId THEN ISNULL(G.ValorGlosa, 0)
							WHEN pr.SourceAccountId = T.AccountConciliationId THEN ISNULL(GM.ValorGlosa, 0)
							ELSE 0
							END
					,2
					,@CodeUser
					,[Common].[GETDATE]()
					,@CodeUser
					,[Common].[GETDATE]()
					,@CodeUser
					,[Common].[GETDATE]()
					,T.InvoiceNumber
					FROM Portfolio.PortfolioReclassification PR
					INNER JOIN 
					(
					Select 
					IdParticion = ROW_NUMBER() OVER (PARTITION BY PR1.AccountReceivableId ORDER BY PR1.Id DESC)
					,PR1.AccountReceivableId
					,PR1.Code
					,[@TJD].InvoiceNumber
					,[@TJD].InvoiceBalance
					,[@TJD].AccountWithoutRadicateId
					,[@TJD].AccountRadicateId
					,[@TJD].AccountObjectionRemediedId 
					,[@TJD].AccountConciliationId 
					,[@TJD].AccountLegalCollectionId
					from @TJD
					inner join Portfolio.PortfolioReclassification PR1
					ON PR1.AccountReceivableId = [@TJD].AccountReceivableId
					WHERE [@TJD].CanReversed = 1
					) T
					ON T.IdParticion = 1
					AND T.Code = PR.Code
					LEFT OUTER JOIN (select [@GPG].InvoiceNumber, ValorGlosa = SUM(LegalTransferValue) from @GPG GROUP BY [@GPG].InvoiceNumber) G
					ON G.InvoiceNumber = T.InvoiceNumber
					LEFT OUTER JOIN (select [@GMG].InvoiceNumber, ValorGlosa = SUM(LegalTransferValue) from @GMG GROUP BY [@GMG].InvoiceNumber) GM
					ON GM.InvoiceNumber = T.InvoiceNumber

					IF (SELECT COUNT(1) FROM @Reclassification R where R.[Value] > 0 
						) > 0
					BEGIN
						delete @Reclassification where [Value] = 0 
				
						SELECT TOP 1 @AccountReceivableId = AccountReceivableId, @InvoiceNumber = InvoiceNumber FROM @Reclassification

						WHILE @AccountReceivableId IS NOT NULL AND @AccountReceivableId > 0
						BEGIN
							update @Reclassification set Code = dbo.GetSequence('', @pattern, @NextS) where  AccountReceivableId = @AccountReceivableId
							if (SELECT COUNT(1) FROM @Reclassification R WHERE R.CODE IS NOT NULL AND R.Code <> '__ERROR_MAXVALUE__' AND R.AccountReceivableId = @AccountReceivableId) > 0
							BEGIN

								DELETE @OutputTbl

								BEGIN TRY

									BEGIN TRANSACTION;

									UPDATE PG SET BalanceGlosa = [@GPG].LegalTransferValue, LegalTransferValue = 0, State = [@GPG].TempState, TempState = 15
									from @TJD
									inner join @GPG
									on [@GPG].InvoiceNumber = [@TJD].InvoiceNumber
									inner join Glosas.GlosaPortfolioGlosada PG
									ON PG.Id = [@GPG].Id
									where [@TJD].CanReversed = 1
									AND [@TJD].AccountReceivableId = @AccountReceivableId

									UPDATE MG SET ValuePendingConciliation = [@GMG].LegalTransferValue, LegalTransferValue = 0
									from @TJD
									inner join @GMG
									on [@GMG].InvoiceNumber = [@TJD].InvoiceNumber
									inner join Glosas.GlosaMovementGlosa MG
									ON MG.Id = [@GMG].Id
									where [@TJD].CanReversed = 1
									AND [@TJD].AccountReceivableId = @AccountReceivableId

									UPDATE AR SET PortfolioStatus = 3
									FROM @TJD
									inner join Portfolio.AccountReceivable AR
									ON AR.Id = [@TJD].AccountReceivableId
									WHERE [@TJD].CanReversed = 1
									AND [@TJD].AccountReceivableId = @AccountReceivableId
		
									UPDATE TTJD SET ReversedUser = @CodeUser, ReversedDate = getdate()
									from @TJD
									inner JOIN Glosas.TransferJuridicalDebtCollectionD TTJD
									ON TTJD.Id = [@TJD].Id
									WHERE [@TJD].CanReversed = 1
									AND [@TJD].AccountReceivableId = @AccountReceivableId

									INSERT INTO Portfolio.PortfolioReclassification (Code,DocumentType,AccountReceivableId,SourceAccountId,TargetAccountId,[Value],[Status],CreationUser,CreationDate,ModificationUser,
												ModificationDate,ConfirmationUser,ConfirmationDate)
									OUTPUT INSERTED.ID, INSERTED.AccountReceivableId, INSERTED.Code INTO @OutputTbl (ReclassificationId,AccountReceivableId,Code)
									SELECT R.Code,R.DocumentType,R.AccountReceivableId,R.SourceAccountId,R.TargetAccountId,R.[Value],R.[Status],R.CreationUser,R.CreationDate,R.ModificationUser,
												R.ModificationDate,R.ConfirmationUser,R.ConfirmationDate
									FROM @Reclassification R WHERE R.AccountReceivableId = @AccountReceivableId;

									/* -------------------- CABECERA DEL COMPROBANTE -------------------- */
									DELETE FROM @JournalVourcherTmp
									INSERT INTO @JournalVourcherTmp (Id, Consecutive, LegalBookId, IdJournalVoucher, VoucherDate, Imported, [Status], Detail, EntityCode, EntityId, EntityName, IsClosedYear, AccountReceivableId)
									SELECT 0, 0, @LegalBookId, @JournalVoucherTypeId, [Common].[GETDATE](), 'False', 2, 'Comprobante de reclasificación - Reversión de Traslado Cobro Juridico'
									,T.Code , T.Id, 'PortfolioReclassification', 0, t.AccountReceivableId
									FROM
									(SELECT Id = Min(OP.ReclassificationId), OP.Code, OP.AccountReceivableId from  @OutputTbl OP GROUP BY  OP.Code, OP.AccountReceivableId) T

									/* -------------------- DETALLE DEL COMPROBANTE -------------------- */			
			
									DELETE FROM @JournalVourcherDetailTmp
									INSERT INTO @JournalVourcherDetailTmp
									( Id, IdAccounting, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue, AccountReceivableId )
									SELECT 
											0 AS Id, 
											0 AS IdAccounting, 
											IdMainAccount = MA.Id
											,CASE ma.HandlesThirdParty WHEN 1 THEN [@TJD].ThirdPartyId ELSE NULL END AS IdThirdParty,
											CASE ma.HandlesCostCenter WHEN 1 THEN [@TJD].CostCenterId ELSE NULL END AS IdCostCenter,
											R.[Value] AS DebitValue,
											0 AS CreditValue,
											'Comprobante de reclasificación - Reversión de Traslado Cobro Juridico' AS Detail,
											NULL AS IdRetention, 
											0 AS RetentionRate, 
											0 AS BaseValue, 
											0 AS BillingValue
											,R.AccountReceivableId
										FROM @Reclassification R
										INNER JOIN GeneralLedger.MainAccounts MA
										ON MA.ID = R.TargetAccountId
										INNER JOIN @TJD
										ON [@TJD].AccountReceivableId = R.AccountReceivableId
										WHERE R.AccountReceivableId = @AccountReceivableId
										UNION ALL
										SELECT 
											0 AS Id, 
											0 AS IdAccounting,
											IdMainAccount = MA.Id,
											CASE ma.HandlesThirdParty WHEN 1 THEN [@TJD].ThirdPartyId ELSE NULL END AS IdThirdParty,
											CASE ma.HandlesCostCenter WHEN 1 THEN [@TJD].CostCenterId ELSE NULL END AS IdCostCenter,
											0 AS DebitValue,
											R.[Value] AS CreditValue,
											'Comprobante de reclasificación - Reversión de Traslado Cobro Juridico' AS Detail,
											NULL AS IdRetention, 
											0 AS RetentionRate, 
											0 AS BaseValue, 
											0 AS BillingValue
											,R.AccountReceivableId
										FROM (SELECT R1.AccountReceivableId, R1.SourceAccountId, [Value] = Sum(R1.[Value]) FROM  @Reclassification R1 
											WHERE R1.AccountReceivableId = @AccountReceivableId GROUP BY R1.AccountReceivableId, R1.SourceAccountId) R
										INNER JOIN GeneralLedger.MainAccounts MA
										ON MA.ID = R.SourceAccountId
										INNER JOIN @TJD
										ON [@TJD].AccountReceivableId = R.AccountReceivableId
										WHERE R.AccountReceivableId = @AccountReceivableId

										UPDATE Portfolio.AccountReceivableAccounting SET Balance = 0
										WHERE AccountReceivableId = @AccountReceivableId AND Balance > 0

										INSERT INTO Portfolio.AccountReceivableAccounting (AccountReceivableId,MainAccountId,ThirdPartyId,CostCenterId,[Value],Balance)
										SELECT  R.AccountReceivableId, R.TargetAccountId, [@TJD].ThirdPartyId, [@TJD].CostCenterId, R.[Value], R.[Value]
										FROM @Reclassification R
										INNER JOIN @TJD
										ON [@TJD].AccountReceivableId = R.AccountReceivableId 
										LEFT OUTER JOIN Portfolio.AccountReceivableAccounting ARA 
										ON ARA.AccountReceivableId = R.AccountReceivableId 
										AND ARA.MainAccountId = R.TargetAccountId
										WHERE R.AccountReceivableId = @AccountReceivableId
										AND ARA.Id IS NULL

										UPDATE ARA SET Value = R.[Value], Balance = R.[Value]
										FROM @Reclassification R
										INNER JOIN @TJD
										ON [@TJD].AccountReceivableId = R.AccountReceivableId 
										INNER JOIN Portfolio.AccountReceivableAccounting ARA 
										ON ARA.AccountReceivableId = R.AccountReceivableId 
										AND ARA.MainAccountId = R.TargetAccountId
										WHERE R.AccountReceivableId = @AccountReceivableId
										
										--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
									DELETE @resultJournalVoucher

									SELECT @JournalVoucherXML = convert(xml, (
										SELECT * FROM @JournalVourcherTmp JournalVoucher 
										JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.AccountReceivableId = JournalVoucherDetail.AccountReceivableId For xml AUTO,TYPE, ELEMENTS))

									--Se consume el sp que guarda el comprobante contable
									INSERT @resultJournalVoucher (code,	MessageResult, IdJournalVoucher) EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML, @CodeUser
						
									--Se valida que no hayan errores en el guardado del comprobante contable
									IF EXISTS (SELECT code FROM @resultJournalVoucher WHERE code = '999')
									BEGIN	
										SELECT @Message = MessageResult FROM @resultJournalVoucher WHERE code = '999'
										ROLLBACK;

										INSERT INTO @result (code, MessageResult, IdJournalVoucher, InvoiceNumber) VALUES ('999', 'Se genero el siguiente error para la factura ' + @InvoiceNumber + ': ' +  @Message, 0, @InvoiceNumber)
									END
									ELSE
									BEGIN
										SET @NextS = @NextS + 1
										--actualizo la secuencia
										UPDATE Portfolio.PortfolioSequenceDetail
										SET [Next]=@NextS WHERE Id = @SequenceId;
										IF @@ERROR = 0
										COMMIT;
										INSERT INTO @result (code, MessageResult, IdJournalVoucher, InvoiceNumber)
										SELECT code, 'Para la factura ' + @InvoiceNumber + ': ' + MessageResult, IdJournalVoucher, @InvoiceNumber FROM @resultJournalVoucher
										union ALL
										SELECT '0', 'Para la factura ' + @InvoiceNumber + ': se genero el comprobante de reclasificación ' + Code, 0, @InvoiceNumber FROM @OutputTbl
									END
	
								END TRY
								BEGIN CATCH
									ROLLBACK;
									INSERT INTO @result (code, MessageResult, IdJournalVoucher, InvoiceNumber) VALUES ('999', 'Se genero el siguiente error para la factura ' + @InvoiceNumber + ': ' + ERROR_MESSAGE(), 0, @InvoiceNumber)
								END CATCH;

								DELETE @Reclassification WHERE AccountReceivableId = @AccountReceivableId
								SELECT TOP 1 @AccountReceivableId = AccountReceivableId, @InvoiceNumber = InvoiceNumber FROM @Reclassification
							END
							ELSE
							BEGIN
								--SET @Message = 'La secuencia alcanzo su valor maximo'
								UPDATE @TJD set CanReversed = 0, Mensaje = 'No se reverso el traslado juridico para la factura: ' + [@TJD].InvoiceNumber + ', la secuencia alcanzo su valor maximo'
								from @Reclassification R 
								INNER JOIN @TJD
								ON [@TJD].AccountReceivableId = R.AccountReceivableId
								AND [@TJD].CanReversed = 1
								SET @AccountReceivableId = 0
							END
						END
					END
				 END
			 
			
			END
			ELSE
			BEGIN
			 PRINT 'AQUI'
				DECLARE @FACTURAS AS TABLE
				(RowId int identity(1,1)
				,Id int 
				,Number varchar(50)
				)

				insert into @FACTURAS (Id, Number)
				select distinct [@TJD].AccountReceivableId, [@TJD].InvoiceNumber  from @TJD where CanReversed = 1

				--SELECT TOP 1 @AccountReceivableId = Id, @InvoiceNumber = Number FROM @FACTURAS

				--select * from @FACTURAS
				--return

				declare @Rows int = 1, @RowId int = 0

				while @Rows > 0		--WHILE @AccountReceivableId IS NOT NULL AND @AccountReceivableId > 0
				BEGIN
					BEGIN TRY

						SELECT TOP 1 @RowId = RowId, @AccountReceivableId = Id, @InvoiceNumber = Number 
						FROM @FACTURAS
						where RowId > @RowId 
						order by RowId

						set @Rows = @@RowCount
						if @Rows = 0		
						break

						BEGIN TRANSACTION;
						PRINT 'A1'
						UPDATE PG SET BalanceGlosa = [@GPG].LegalTransferValue, LegalTransferValue = 0, State = [@GPG].TempState, TempState = 15
						from @TJD
						inner join @GPG
						on [@GPG].InvoiceNumber = [@TJD].InvoiceNumber
						inner join Glosas.GlosaPortfolioGlosada PG
						ON PG.Id = [@GPG].Id
						where [@TJD].CanReversed = 1
						AND [@TJD].AccountReceivableId = @AccountReceivableId

						PRINT 'A2'
						UPDATE MG SET ValuePendingConciliation = [@GMG].LegalTransferValue, LegalTransferValue = 0
						from @TJD
						inner join @GMG
						on [@GMG].InvoiceNumber = [@TJD].InvoiceNumber
						inner join Glosas.GlosaMovementGlosa MG
						ON MG.Id = [@GMG].Id
						where [@TJD].CanReversed = 1
						AND [@TJD].AccountReceivableId = @AccountReceivableId

						PRINT 'A3'
						UPDATE AR SET PortfolioStatus = 3
						FROM @TJD
						inner join Portfolio.AccountReceivable AR
						ON AR.Id = [@TJD].AccountReceivableId
						WHERE [@TJD].CanReversed = 1
						AND [@TJD].AccountReceivableId = @AccountReceivableId
		
		PRINT 'A4'
						UPDATE TTJD SET ReversedUser = @CodeUser, ReversedDate = getdate()
						from @TJD
						inner JOIN Glosas.TransferJuridicalDebtCollectionD TTJD
						ON TTJD.Id = [@TJD].Id
						WHERE [@TJD].CanReversed = 1
						AND [@TJD].AccountReceivableId = @AccountReceivableId

						COMMIT;
						
						INSERT INTO @result (code, MessageResult, IdJournalVoucher, InvoiceNumber)
						values('0', 'Se reverso la factura ' + @InvoiceNumber, 0, @InvoiceNumber)
										
					END TRY
					BEGIN CATCH
						ROLLBACK;
						INSERT INTO @result (code, MessageResult, IdJournalVoucher, InvoiceNumber) VALUES ('999', 'Se genero el siguiente error para la factura ' + @InvoiceNumber + ':' + ERROR_MESSAGE(), 0, @InvoiceNumber)
					END CATCH;
					
					--DELETE @FACTURAS WHERE Id = @AccountReceivableId
					--SELECT TOP 1 @AccountReceivableId = Id, @InvoiceNumber = Number FROM @FACTURAS
				END
			END
		END
	end
	ELSE
	BEGIN
		BEGIN TRY
			BEGIN TRANSACTION;
			UPDATE Glosas.TransferJuridicalDebtCollectionD SET ReversedUser = @CodeUser, ReversedDate = getdate()
			WHERE TransferJuridicalDebtCollectionCId = @Id
			COMMIT;
		END TRY
		BEGIN CATCH
			ROLLBACK;
			INSERT INTO @result (code, MessageResult, IdJournalVoucher, InvoiceNumber) VALUES ('999', 'Se genero el siguiente error:' + ERROR_MESSAGE(), 0, '')
		END CATCH;
	END

	if (select count(1) from Glosas.TransferJuridicalDebtCollectionD where TransferJuridicalDebtCollectionCId = @Id and ReversedUser IS NULL) = 0
	BEGIN
		BEGIN TRY
		PRINT 'A10'
			BEGIN TRANSACTION;
			update Glosas.TransferJuridicalDebtCollectionC set [state] = IIF(@GenerateReclassification = 0, 1, 3), ReversedUser = @CodeUser, ReversedDate = getdate() where Id = @Id
			INSERT INTO @result(code, MessageResult, IdJournalVoucher, InvoiceNumber)
			VALUES ('0', 'Se reverso el traslado jurídico', 0, '')
			COMMIT;
		END TRY
		BEGIN CATCH
			ROLLBACK;
			INSERT INTO @result (code, MessageResult, IdJournalVoucher, InvoiceNumber) VALUES ('999', 'Se genero el siguiente error:' + ERROR_MESSAGE(), 0, '')
		END CATCH;
	END

	IF LEN(@Message) <> 0
	BEGIN
		INSERT INTO @result(code, MessageResult, IdJournalVoucher, InvoiceNumber)
		VALUES ('999', @Message, 0, '')
	END

	IF (SELECT COUNT(1) FROM @TJD WHERE CanReversed = 0) > 0
	BEGIN
		INSERT INTO @result(code, MessageResult, IdJournalVoucher, InvoiceNumber)
		SELECT '999', [@TJD].Mensaje, 0, [@TJD].InvoiceNumber FROM @TJD WHERE CanReversed = 0
	END

	SELECT code, MessageResult, IdJournalVoucher, InvoiceNumber FROM @result

END

--SELECT * FROM Glosas.TransferJuridicalDebtCollectionC WHERE JuridicalTransferConsecutive = 234

--BEGIN TRAN
--EXEC [Glosas].[SP_ReverseJuridicalTransfer]
--252
--,1
--,'999'
--,0
--ROLLBACK
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Revierte un traslado de glosas a cobro jurídico previamente registrado en el sistema de cartera. Dado el identificador de un encabezado de transferencia jurídica, valida que no haya sido revertido antes (estado 3), verifica que las facturas involucradas tengan saldo suficiente frente a los valores glosados y pendientes de conciliación, y ejecuta la reversión contable mediante la generación de un comprobante de diario. Si el traslado original incluyó reclasificación de cuentas por cobrar, también deshace esas reclasificaciones contables sobre las cuentas de cartera afectadas (sin radicar, radicadas, con objeción subsanada, conciliación y cobro jurídico). Afecta las tablas de detalle y encabezado de traslados jurídicos (TransferJuridicalDebtCollectionC y TransferJuridicalDebtCollectionD), las cuentas por cobrar del módulo de cartera (AccountReceivable), los movimientos de glosa y las glosas de portafolio, dejando trazabilidad del usuario y fecha de reversión en cada registro procesado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseJuridicalTransfer';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseJuridicalTransfer';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reversa un traslado de cartera a cobro jurídico, restaurando saldos de glosas y cartera, y generando opcionalmente la reclasificación contable inversa con su comprobante.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseJuridicalTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El encabezado del traslado jurídico debe existir y no estar en estado 3 (ya reversado); Deben existir detalles del traslado sin ReversedUser asignado; Si se exige reclasificación: debe existir secuencia configurada para el formulario 509 con Sequential=1, parámetros de glosas (TimeParameters) para la unidad operativa y un tipo de comprobante contable para traslado jurídico definido; Cada factura a reversar debe tener saldo (>0) en la cuenta por cobrar y debe tener AccountLegalCollectionId definido; El último registro de AccountReceivableAccounting de la cuenta debe corresponder a la cuenta de cobro jurídico (cuando se exige reclasificación); El saldo de la factura debe ser mayor o igual al valor glosado y al valor pendiente por conciliar', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseJuridicalTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Glosas.GlosaPortfolioGlosada: Cuando la factura puede reversarse, se restaura BalanceGlosa con el LegalTransferValue previo, se pone LegalTransferValue=0, State vuelve al TempState anterior y TempState=15; [UPDATE] Glosas.GlosaMovementGlosa: Cuando la factura puede reversarse, se restaura ValuePendingConciliation con el LegalTransferValue previo y se pone LegalTransferValue=0; [UPDATE] Portfolio.AccountReceivable: Cuando la factura puede reversarse, PortfolioStatus se establece en 3; [UPDATE] Glosas.TransferJuridicalDebtCollectionD: Para cada detalle reversado se asigna ReversedUser=@CodeUser y ReversedDate=getdate(); [INSERT] Portfolio.PortfolioReclassification: Si @GenerateReclassification=1 y el valor calculado es >0, se inserta reclasificación con DocumentType=7 (Reversión traslado jurídico), Status=2, intercambiando Source/Target respecto a la última reclasificación, con valor calculado según la cuenta origen (saldo - glosa, valor glosa u otros); [UPDATE] Portfolio.AccountReceivableAccounting: Para registros con Balance>0 de la cuenta reversada se pone Balance=0; [INSERT] Portfolio.AccountReceivableAccounting: Cuando no existe fila para (AccountReceivableId, TargetAccountId) se inserta nueva con Value=Balance=R.Value; [UPDATE] Portfolio.AccountReceivableAccounting: Si ya existe la combinación (AccountReceivableId, TargetAccountId) se actualiza Value y Balance al valor de la reclasificación; [UPDATE] Portfolio.PortfolioSequenceDetail: Tras grabar exitosamente el comprobante contable, se incrementa Next en 1 para la secuencia usada; [UPDATE] Glosas.TransferJuridicalDebtCollectionC: Cuando todos los detalles del traslado quedan reversados, se actualiza el encabezado con state=1 si no se generó reclasificación o state=3 si sí, además ReversedUser y ReversedDate; [UPDATE] Glosas.TransferJuridicalDebtCollectionD: Cuando Reclassified=0, se marcan todos los detalles del traslado con ReversedUser y ReversedDate sin tocar saldos; [RETURN_RESULT] RESULT: Devuelve tabla con code/MessageResult/IdJournalVoucher/InvoiceNumber: ''999'' si ya estaba reversado, sin información, errores de secuencia/parámetros, errores capturados o saldos insuficientes; ''0'' en éxitos', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseJuridicalTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; dbo.GetSequence; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseJuridicalTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.TransferJuridicalDebtCollectionC; Glosas.TransferJuridicalDebtCollectionD; Portfolio.AccountReceivable; Glosas.GlosaPortfolioGlosada; Glosas.GlosaMovementGlosa; Portfolio.AccountReceivableAccounting; Portfolio.PortfolioReclassification; Portfolio.PortfolioSequence; Portfolio.PortfolioSequenceDetail; Common.Sequense; Glosas.TimeParameters; GeneralLedger.LegalBook; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseJuridicalTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseJuridicalTransfer';
-- GO
