-- =============================================
-- Author:		Hector Rodriguez Rubiano
-- Create date: 2019-06-18
-- Description:	Reversa un cruce de cuentas CxC vs CxP
-- =============================================
CREATE PROCEDURE [Treasury].[SP_ReverseCrossingAccount]
(
@TreasuryNoteId INT,	
@CodeUser VARCHAR(20)
)
AS
BEGIN
	SET NOCOUNT ON;
    ---detalles cxp
	DECLARE @DETALLES AS TABLE
	(
	IdAuto INT IDENTITY(1,1)
	, IdParticion INT
	, IdDetalle INT
	, MainAccountId INT
	, ThirdPartyId INT
	, CostCenterId INT NULL
	, AccountId INT
	, CrossingValue NUMERIC(20,4)
	, Detail VARCHAR(MAX) NULL
	, SharesId INT
	, InitialValue NUMERIC(20,4)
	, CrossingValueShare NUMERIC(20,4)
	, Balance NUMERIC(20,4)
	, ValueReversion NUMERIC(20,4)
	)
	---detalles cxc
	DECLARE @DETALLESCxC AS TABLE
		(
		IdAuto INT IDENTITY(1,1)
		, IdParticion INT
		, IdDetalle INT
		, ThirdPartyId INT
		, CostCenterId INT NULL
		, AccountId INT
		, AccountParentId INT
		, MainAccountId INT
		, CrossingValue NUMERIC(20,4)
		, Detail VARCHAR(MAX) NULL
		, SharesId INT
		, InitialValue NUMERIC(20,4)
		, CrossingValueShare NUMERIC(20,4)
		, Balance NUMERIC(20,4)
		, ValueReversion NUMERIC(20,4)
		)
	
	---Ajusta detalles cxc
	DECLARE @DETALLESCxCAJUSTADO AS TABLE
		(
		IdAuto INT IDENTITY(1,1)
		, IdParticion INT
		, IdAutoCxC INT
		, IdDetalle INT
		, AccountId INT
		, AccountParentId INT
		, CrossingValue NUMERIC(20,4)
		, SharesId INT
		, InitialValue NUMERIC(20,4)
		, Balance NUMERIC(20,4)
		, ValueReversion NUMERIC(20,4)
		)
	/******************************** VARIABLES CONTABLES *******************************/

	--Se declara una tabla con los datos para la cabecera del comprobante contable 
	DECLARE @JournalVourcherTmp TABLE 
	(
		Id INT DEFAULT(0),
		Consecutive BIGINT DEFAULT(0),
		LegalBookId INT,
		IdJournalVoucher INT,
		VoucherDate VARCHAR(30),
		Imported VARCHAR(5) DEFAULT('False'),
		Status TINYINT,
		Detail VARCHAR(MAX),
		EntityCode VARCHAR(20),
		EntityId INT,
		EntityName VARCHAR(250),
		OriginEntityName VARCHAR(250),
		IsClosedYear TINYINT DEFAULT(0)
	)

	--Se declara una tabla temporal para los detalles del comprobante
	DECLARE @JournalVourcherDetailTmp TABLE 
	(
		Id INT DEFAULT(0),
		IdAccounting INT DEFAULT(0),
		IdMainAccount INT,
		IdThirdParty INT,
		IdCostCenter INT NULL,
		DebitValue DECIMAL(18,2),
		CreditValue DECIMAL(18,2),
		Detail VARCHAR(500),
		IdRetention INT,
		RetentionRate DECIMAL(6,3),
		BaseValue DECIMAL(18,2),
		BillingValue DECIMAL(18,2)
	)

	--Variable para obtener el xml
	DECLARE @JournalVoucherTypeId AS INT,
			@JournalVoucherXML XML,
			@CodeMessage INT,
			@Message VARCHAR(MAX),
			@IdJournalVoucherResult INT,
			@Consecutive VARCHAR(30)

    DECLARE @TreasuryNoteCode VARCHAR(20),
				@CrossingAccountId INT
	DECLARE @AccountParentId INT
    DECLARE @IdAuto INT = 1
	DECLARE @IdParticion INT = 1
	DECLARE @IdMax INT = 0
	DECLARE @AccountId INT
	DECLARE @CrossingValue NUMERIC(20,4) = 1
	DECLARE @CrossingValueTemp NUMERIC(20,4) = 1
	DECLARE @SharesId INT
	DECLARE @InitialValue NUMERIC(20,4)
	DECLARE @CrossingValueShare NUMERIC(20,4)
	DECLARE @Balance NUMERIC(20,4)
	DECLARE @IdAutoCxC INT

	--tabla temporal para almacenar el resultado deL movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	BEGIN TRY

		SELECT
			@TreasuryNoteCode = tn.Code
		   ,@CrossingAccountId = tn.CrossingAccountId
		   ,@JournalVoucherTypeId = st.JournalVoucherTypeTreasuryNotes
		FROM Treasury.TreasuryNote tn
		LEFT JOIN Treasury.SettingsTreasury st
			ON tn.OperatingUnitId = st.IdOperatingUnit
		WHERE tn.Id = @TreasuryNoteId

		INSERT INTO @DETALLES
			SELECT
				ROW_NUMBER() OVER (PARTITION BY CADP.AccountPayableId ORDER BY APS.ID ASC)
			   ,CADP.Id
			   ,CADP.MainAccountId
			   ,ThirdPartyId =
				CASE
					WHEN CA.CrossingType = 1 THEN CA.ThirdPartyId
					ELSE AP.IdThirdParty
				END
			   ,AP.IdCostCenter
			   ,CADP.AccountPayableId
			   ,CADP.CrossingValue
			   ,CADP.Detail
			   ,APS.Id
			   ,APS.InitialValue
			   ,APS.CrossingValue
			   ,APS.Balance
			   ,0
			FROM Treasury.CrossingAccount CA WITH (NOLOCK)
			INNER JOIN Treasury.CrossingAccountDetailCxP CADP WITH (NOLOCK)
				ON CADP.CrossingAccountId = CA.Id
			INNER JOIN Payments.AccountPayable AP WITH (NOLOCK)
				ON AP.Id = CADP.AccountPayableId
			INNER JOIN Payments.AccountPayableShares APS WITH (NOLOCK)
				ON APS.IdAccountPayable = AP.Id
			WHERE CA.ID = @CrossingAccountId
			ORDER BY CADP.AccountPayableId

		SELECT
			@IdMax = MAX(IdAuto)
		FROM @DETALLES

		WHILE @IdAuto <= @IdMax
		BEGIN
			SELECT
				@IdParticion = D.IdParticion
			   ,@AccountId = D.AccountId
			   ,@CrossingValue = D.CrossingValue
			   ,@SharesId = D.SharesId
			   ,@InitialValue = D.InitialValue
			   ,@CrossingValueShare = D.CrossingValueShare
			   ,@Balance = D.Balance
			FROM @detalles D
			WHERE IdAuto = @IdAuto

			IF @IdParticion = 1
			BEGIN
				SET @CrossingValueTemp = @CrossingValue
			END

			if @CrossingValueTemp > 0 AND @CrossingValueShare > 0 
			BEGIN
				IF @CrossingValueShare > @CrossingValueTemp SET @CrossingValueShare = @CrossingValueTemp
				UPDATE Payments.AccountPayableShares
				SET CrossingValue = CrossingValue - @CrossingValueShare
				   ,Balance = Balance + @CrossingValueShare
				WHERE ID = @SharesId
				UPDATE @DETALLES
				SET CrossingValueShare = CrossingValueShare - @CrossingValueShare
				   ,Balance = Balance + @CrossingValueShare
				WHERE IdAuto >= @IdAuto
				AND SharesId = @SharesId
				UPDATE Payments.AccountPayable
				SET Balance = Balance + @CrossingValueShare
				WHERE ID = @AccountId
				UPDATE @DETALLES
				SET ValueReversion = @CrossingValueShare
				WHERE IdAuto = @IdAuto
				SET @CrossingValueTemp = @CrossingValueTemp - @CrossingValueShare
			END
			ELSE 
			BEGIN
				IF @CrossingValueTemp > 0 AND @CrossingValueShare = 0 
				BEGIN
					IF @InitialValue > 0 AND @InitialValue >= @CrossingValueTemp AND @InitialValue >= @Balance + @CrossingValueTemp
					BEGIN
						UPDATE Payments.AccountPayableShares
						SET CrossingValue = 0
						   ,Balance = Balance + @CrossingValueTemp
						WHERE ID = @SharesId
						UPDATE @DETALLES
						SET CrossingValueShare = 0
						   ,Balance = Balance + @CrossingValueTemp
						WHERE IdAuto >= @IdAuto
						AND SharesId = @SharesId
						UPDATE Payments.AccountPayable
						SET Balance = Balance + @CrossingValueTemp
						WHERE ID = @AccountId
						UPDATE @DETALLES
						SET ValueReversion = @CrossingValueTemp
						WHERE IdAuto = @IdAuto
						SET @CrossingValueTemp = 0
					END
					ELSE
					BEGIN
 						IF @InitialValue > 0 AND @InitialValue >= @CrossingValueTemp 
						BEGIN
							UPDATE Payments.AccountPayableShares
							SET CrossingValue = 0
							   ,Balance = Balance + (@InitialValue - @Balance)
							WHERE ID = @SharesId
							UPDATE @DETALLES
							SET CrossingValueShare = 0
							   ,Balance = Balance + (@InitialValue - @Balance)
							WHERE IdAuto >= @IdAuto
							AND SharesId = @SharesId
							UPDATE Payments.AccountPayable
							SET Balance = Balance + (@InitialValue - @Balance)
							WHERE ID = @AccountId
							UPDATE @DETALLES
							SET ValueReversion = (@InitialValue - @Balance)
							WHERE IdAuto = @IdAuto
							SET @CrossingValueTemp = @CrossingValueTemp - (@InitialValue - @Balance)
						END
						ELSE
						BEGIN
 							IF @InitialValue > 0 AND @InitialValue < @CrossingValueTemp 
							BEGIN
								UPDATE Payments.AccountPayableShares
								SET CrossingValue = 0
								   ,Balance = Balance + (@InitialValue - @Balance)
								WHERE ID = @SharesId
								UPDATE @DETALLES
								SET CrossingValueShare = 0
								   ,Balance = Balance + (@InitialValue - @Balance)
								WHERE IdAuto >= @IdAuto
								AND SharesId = @SharesId
								UPDATE Payments.AccountPayable
								SET Balance = Balance + (@InitialValue - @Balance)
								WHERE ID = @AccountId
								UPDATE @DETALLES
								SET ValueReversion = (@InitialValue - @Balance)
								WHERE IdAuto = @IdAuto
								SET @CrossingValueTemp = @CrossingValueTemp - (@InitialValue - @Balance)
							END
						END
					END
				END
			END
			SET @IdAuto = @IdAuto + 1
		END

		INSERT INTO @DETALLESCxC
		SELECT
			IdParticion = ROW_NUMBER() OVER (PARTITION BY CADC.AccountReceivableId ORDER BY ARS.Id ASC)
			,CADC.Id
			,ThirdPartyId =
			CASE
				WHEN CA.CrossingType = 1 THEN CA.ThirdPartyId
				ELSE AR.ThirdPartyId
			END
			,ARA.CostCenterId
			,CADC.AccountReceivableId
			,CADC.AccountReceivableAccountingId
			,CADC.MainAccountId
			,CADC.CrossingValue
			,CADC.Detail
			,ARS.Id
			,ARS.[Value]
			,ARS.CrossingValue
			,ARS.Balance
			,0
		FROM Treasury.CrossingAccount CA WITH (NOLOCK)
		INNER JOIN Treasury.CrossingAccountDetailCxC CADC WITH (NOLOCK)
			ON CADC.CrossingAccountId = CA.ID
		INNER JOIN Portfolio.AccountReceivable AR WITH (NOLOCK)
			ON AR.Id = CADC.AccountReceivableId
		INNER JOIN Portfolio.AccountReceivableAccounting ARA WITH (NOLOCK)
			ON ARA.Id = CADC.AccountReceivableAccountingId
		INNER JOIN Portfolio.AccountReceivableShare ARS WITH (NOLOCK)
			ON ARS.AccountReceivableId = CADC.AccountReceivableId
		WHERE CA.Id = @CrossingAccountId
		ORDER BY CADC.AccountReceivableId

		SET @IdAuto = 1
		SET @CrossingValueTemp = 1
		SELECT
			@IdMax = MAX(IdAuto)
		FROM @DETALLESCxC

		WHILE @IdAuto <= @IdMax
		BEGIN
			SELECT
				@IdParticion = D.IdParticion
			   ,@AccountId = D.AccountId
			   ,@AccountParentId = D.AccountParentId
			   ,@CrossingValue = D.CrossingValue
			   ,@SharesId = D.SharesId
			   ,@InitialValue = D.InitialValue
			   ,@CrossingValueShare = D.CrossingValueShare
			   ,@Balance = D.Balance
			FROM @DETALLESCxC D
			WHERE IdAuto = @IdAuto

			IF @IdParticion = 1
			BEGIN
				SET @CrossingValueTemp = @CrossingValue
			END

			if @CrossingValueTemp > 0 AND @CrossingValueShare > 0 
			BEGIN
				IF @CrossingValueShare > @CrossingValueTemp SET @CrossingValueShare = @CrossingValueTemp
				UPDATE Portfolio.AccountReceivableShare
				SET CrossingValue = CrossingValue - @CrossingValueShare
					,Balance = Balance + @CrossingValueShare
				WHERE ID = @SharesId
				UPDATE @DETALLESCxC
				SET CrossingValueShare = CrossingValueShare - @CrossingValueShare
					,Balance = Balance + @CrossingValueShare
				WHERE IdAuto >= @IdAuto
				AND SharesId = @SharesId
				UPDATE Portfolio.AccountReceivable
				SET Balance = Balance + @CrossingValueShare
				WHERE ID = @AccountId
				UPDATE Portfolio.AccountReceivableAccounting
				SET Balance = Balance + @CrossingValueShare
				WHERE Id = @AccountParentId
				UPDATE @DETALLESCxC
				SET ValueReversion = @CrossingValueShare
				WHERE IdAuto = @IdAuto
				SET @CrossingValueTemp = @CrossingValueTemp - @CrossingValueShare
			END
			ELSE 
			BEGIN
				IF @CrossingValueTemp > 0 AND @CrossingValueShare = 0 
				BEGIN
					IF @InitialValue > 0 AND @InitialValue >= @CrossingValueTemp AND @InitialValue >= @Balance + @CrossingValueTemp
					BEGIN
						UPDATE Portfolio.AccountReceivableShare
						SET CrossingValue = 0
							,Balance = Balance + @CrossingValueTemp
						WHERE ID = @SharesId
						UPDATE @DETALLESCxC
						SET CrossingValueShare = 0
							,Balance = Balance + @CrossingValueTemp
						WHERE IdAuto >= @IdAuto
						AND SharesId = @SharesId
						UPDATE Portfolio.AccountReceivable
						SET Balance = Balance + @CrossingValueTemp
						WHERE ID = @AccountId
						UPDATE Portfolio.AccountReceivableAccounting
						SET Balance = Balance + @CrossingValueTemp
						WHERE Id = @AccountParentId
						UPDATE @DETALLESCxC
						SET ValueReversion = @CrossingValueTemp
						WHERE IdAuto = @IdAuto
						SET @CrossingValueTemp = 0
					END
					ELSE
					BEGIN
 						IF @InitialValue > 0 AND @InitialValue >= @CrossingValueTemp 
						BEGIN
							UPDATE Portfolio.AccountReceivableShare
							SET CrossingValue = 0
								,Balance = Balance + (@InitialValue - @Balance)
							WHERE ID = @SharesId
							UPDATE @DETALLESCxC
							SET CrossingValueShare = 0
								,Balance = Balance + (@InitialValue - @Balance)
							WHERE IdAuto >= @IdAuto
							AND SharesId = @SharesId
							UPDATE Portfolio.AccountReceivable
							SET Balance = Balance + (@InitialValue - @Balance)
							WHERE ID = @AccountId
							UPDATE Portfolio.AccountReceivableAccounting
							SET Balance = Balance + (@InitialValue - @Balance)
							WHERE Id = @AccountParentId
							UPDATE @DETALLESCxC
							SET ValueReversion = (@InitialValue - @Balance)
							WHERE IdAuto = @IdAuto
							SET @CrossingValueTemp = @CrossingValueTemp - (@InitialValue - @Balance)
						END
						ELSE 
						BEGIN
							IF @InitialValue > 0 AND @InitialValue < @CrossingValueTemp 
							BEGIN
								UPDATE Portfolio.AccountReceivableShare
								SET CrossingValue = 0
									,Balance = Balance + (@InitialValue - @Balance)
								WHERE ID = @SharesId
								UPDATE @DETALLESCxC
								SET CrossingValueShare = 0
									,Balance = Balance + (@InitialValue - @Balance)
								WHERE IdAuto >= @IdAuto
								AND SharesId = @SharesId
								UPDATE Portfolio.AccountReceivable
								SET Balance = Balance + (@InitialValue - @Balance)
								WHERE ID = @AccountId
								UPDATE Portfolio.AccountReceivableAccounting
								SET Balance = Balance + (@InitialValue - @Balance)
								WHERE Id = @AccountParentId
								UPDATE @DETALLESCxC
								SET ValueReversion = (@InitialValue - @Balance)
								WHERE IdAuto = @IdAuto
								SET @CrossingValueTemp = @CrossingValueTemp - (@InitialValue - @Balance)
							END
						END
					END
				END
			END
			SET @IdAuto = @IdAuto + 1
		END

		INSERT INTO @DETALLESCxCAJUSTADO
		SELECT
		    IdParticion = ROW_NUMBER() OVER (PARTITION BY D.AccountId ORDER BY D.SharesId ASC)
			,D.IdAuto
			,D.IdDetalle
			,D.AccountId
			,D.AccountParentId
			,A.CrossingValue - A.ValueReversion
			,D.SharesId
			,D.InitialValue
			,D.Balance
			,D.ValueReversion
		FROM 
		(SELECT 
		IdDetalle, CrossingValue, ValueReversion = SUM(ValueReversion)
		FROM @DETALLESCxC
		GROUP BY IdDetalle, CrossingValue
		HAVING (CrossingValue - SUM(ValueReversion)) > 0
		) A
		INNER JOIN @DETALLESCxC D
		ON D.IdDetalle = A.IdDetalle
		WHERE D.InitialValue - D.Balance > 0
		ORDER BY D.AccountId

		SET @IdAuto = 1
		SET @CrossingValueTemp = 1
		SELECT
			@IdMax = MAX(IdAuto)
		FROM @DETALLESCxCAJUSTADO

		WHILE @IdAuto <= @IdMax
		BEGIN
			SELECT
				@IdParticion = D.IdParticion
				,@IdAutoCxC = D.IdAutoCxC
			   ,@AccountId = D.AccountId
			   ,@AccountParentId = D.AccountParentId
			   ,@CrossingValue = D.CrossingValue
			   ,@SharesId = D.SharesId
			   ,@InitialValue = D.InitialValue
			   ,@Balance = D.Balance
			FROM @DETALLESCxCAJUSTADO D
			WHERE IdAuto = @IdAuto

			IF @IdParticion = 1
			BEGIN
				SET @CrossingValueTemp = @CrossingValue
			END

			if @CrossingValueTemp > 0
			BEGIN
			    SET @CrossingValueShare = @CrossingValueTemp
				IF (@InitialValue - @Balance) < @CrossingValueTemp SET @CrossingValueShare = (@InitialValue - @Balance)
				UPDATE Portfolio.AccountReceivableShare
				SET CrossingValue = CrossingValue - @CrossingValueShare
					,Balance = Balance + @CrossingValueShare
				WHERE ID = @SharesId
				UPDATE @DETALLESCxCAJUSTADO
				SET Balance = Balance + @CrossingValueShare
				WHERE IdAuto >= @IdAuto
				AND SharesId = @SharesId
				UPDATE @DETALLESCxC
				SET CrossingValueShare = CrossingValueShare - @CrossingValueShare
					,Balance = Balance + @CrossingValueShare
				WHERE IdAuto >= @IdAutoCxC
				AND SharesId = @SharesId
				UPDATE Portfolio.AccountReceivable
				SET Balance = Balance + @CrossingValueShare
				WHERE ID = @AccountId
				UPDATE Portfolio.AccountReceivableAccounting
				SET Balance = Balance + @CrossingValueShare
				WHERE Id = @AccountParentId
				UPDATE @DETALLESCxC
				SET ValueReversion = ValueReversion + @CrossingValueShare
				WHERE IdAuto = @IdAutoCxC
				SET @CrossingValueTemp = @CrossingValueTemp - @CrossingValueShare
				
			END
			SET @IdAuto = @IdAuto + 1
		END

		UPDATE @DETALLESCxC SET CrossingValueShare = 0 WHERE CrossingValueShare < 0

		/*************************************** PROCESO CONTABLE ************************************/

		INSERT INTO @JournalVourcherTmp (IdJournalVoucher,
			VoucherDate,
			Status,
			Detail,
			EntityCode,
			EntityId,
			EntityName)
		SELECT
			@JournalVoucherTypeId
			,tn.NoteDate
			,2
			,'Reversión de Cruce CxC vs CxP ' + tn.Code + '. ' + tn.Description
			,tn.Code
			,tn.Id
			,'TreasuryNote'
		FROM Treasury.TreasuryNote tn
		WHERE tn.Id = @TreasuryNoteId

		INSERT INTO @JournalVourcherDetailTmp (IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail)
			SELECT
				MainAccountId
			   ,ThirdPartyId
			   ,CostCenterId
			   ,0
			   ,VALOR
			   ,Detail
			FROM (SELECT
					D.IdDetalle
				   ,D.Detail
				   ,D.MainAccountId
				   ,D.ThirdPartyId
				   ,D.CostCenterId
				   ,VALOR = SUM(D.ValueReversion)
				FROM @DETALLES D
				WHERE D.ValueReversion > 0
				GROUP BY D.IdDetalle
						,D.Detail
						,D.MainAccountId
						,D.ThirdPartyId
						,D.CostCenterId) A

		INSERT INTO @JournalVourcherDetailTmp (IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail)
			SELECT
				MainAccountId
			   ,ThirdPartyId
			   ,CostCenterId
			   ,VALOR
			   ,0
			   ,Detail
			FROM (SELECT
					D.IdDetalle
				   ,D.Detail
				   ,D.MainAccountId
				   ,D.ThirdPartyId
				   ,D.CostCenterId
				   ,VALOR = SUM(D.ValueReversion)
				FROM @DETALLESCxC D
				WHERE D.ValueReversion > 0
				GROUP BY D.IdDetalle
						,D.Detail
						,D.MainAccountId
						,D.ThirdPartyId
						,D.CostCenterId) A

		INSERT INTO @JournalVourcherDetailTmp (IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail)
			SELECT
				MainAccountId
			   ,ThirdPartyId
			   ,CostCenterId
			   ,CASE
					WHEN Nature = 2 THEN [VALUE]
					ELSE 0
				END
			   ,CASE
					WHEN Nature = 1 THEN [VALUE]
					ELSE 0
				END
			   ,Detail
			FROM Treasury.CrossingAccountDetailOtherConcept
			WHERE CrossingAccountId = @CrossingAccountId
			AND [Value] > 0

		/************************************************************************************************************************************/

		--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
		SELECT
			@JournalVoucherXML = CONVERT(XML, (SELECT
					*
				FROM @JournalVourcherTmp JournalVoucher
				JOIN @JournalVourcherDetailTmp JournalVoucherDetail
					ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting
				FOR XML AUTO, TYPE, ELEMENTS)
			)

		--Se consume el sp que guarda el comprobante contable

		insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,@CodeUser 
			select 
				@CodeMessage = rjv.code, 
				@Message = rjv.MessageResult, 
				@IdJournalVoucherResult = rjv.IdJournalVoucher
			from @resultJournalVoucher rjv

		--Se valida que no hayan errores en el guardado del comprobante contable
		IF @CodeMessage = '999'
		BEGIN
		SELECT
			999 AS CodeMessage
		   ,@Message AS Message
		   ,0 AS IdJournalVoucher
		   ,'' AS Consecutive
		RETURN
		END

		SELECT
			@Message = 'Comprobante Contable ' + jvt.Code + ' - ' + jvt.Name + ' con Consecutivo ' + CAST(jv.Consecutive AS VARCHAR(30))
		   ,@Consecutive = CAST(jv.Consecutive AS VARCHAR(30))
		FROM GeneralLedger.JournalVouchers jv
		JOIN GeneralLedger.JournalVoucherTypes jvt
			ON jv.IdJournalVoucher = jvt.Id
		WHERE jv.Id = @IdJournalVoucherResult

		/*************************************** ACTUALIZACION DE LA CONSIGNACION ************************************/

		UPDATE c
		SET c.Status = 4
		   ,c.ModificationUser = @CodeUser
		   ,c.ModificationDate = [Common].[GETDATE]()
		   ,c.ReversedUser = @CodeUser
		   ,c.ReversedDate = [Common].[GETDATE]()
		FROM Treasury.CrossingAccount c
		WHERE c.Id = @CrossingAccountId

		SELECT
			0 AS CodeMessage
		   ,@Message AS [Message]
		   ,@IdJournalVoucherResult AS IdJournalVoucher
		   ,@Consecutive AS Consecutive
	END TRY
	BEGIN CATCH
	SELECT
		999 AS CodeMessage
	   ,ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS [Message]
	   ,0 AS IdJournalVoucher
	   ,'' AS Consecutive
	END CATCH
END