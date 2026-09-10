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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Revierte (anula) un cruce de cuentas entre cuentas por cobrar (CxC) y cuentas por pagar (CxP) previamente registrado en tesorería. A partir del identificador de una nota de tesorería, recupera el cruce de cuentas asociado y deshace los movimientos de compensación: restaura los saldos y valores cruzados en las cuotas y facturas de cuentas por pagar (AccountPayableShares, AccountPayable), y hace lo equivalente del lado de las cuentas por cobrar. Una vez revertidos los saldos financieros, genera el comprobante contable de reversión usando el tipo de comprobante configurado para notas de tesorería en la unidad operativa (SettingsTreasury), dejando trazabilidad completa del movimiento contable inverso. Se utiliza cuando se necesita deshacer una compensación entre deudas y acreencias de terceros, corrigiendo tanto los saldos operativos como la contabilidad.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseCrossingAccount';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseCrossingAccount';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reversa un cruce de cuentas (CxC vs CxP) restituyendo saldos en cuotas y documentos involucrados, generando el comprobante contable de reversión y marcando el cruce como reversado.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCrossingAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@TreasuryNoteId debe existir en Treasury.TreasuryNote y tener un CrossingAccountId asociado al cruce a reversar.; La unidad operativa de la nota debe estar configurada en Treasury.SettingsTreasury con un JournalVoucherTypeTreasuryNotes válido para generar el comprobante contable.; Debe existir el CrossingAccount referenciado y sus detalles en CrossingAccountDetailCxP y/o CrossingAccountDetailCxC.; Las cuentas por pagar/cobrar y sus cuotas referenciadas deben existir en Payments.AccountPayable/Shares y Portfolio.AccountReceivable/Accounting/Share.; @CodeUser debe ser un código de usuario válido para auditoría y para el SP contable.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCrossingAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La reversión nunca devuelve más de lo que originalmente fue cruzado: @CrossingValueShare se acota al @CrossingValueTemp pendiente.; Tras la reversión, el CrossingValue de las cuotas no queda negativo: se aplica UPDATE @DETALLESCxC SET CrossingValueShare = 0 WHERE CrossingValueShare < 0.; Por cada peso reversado, se incrementa Balance del documento y de la cuota en igual magnitud y se decrementa CrossingValue de la cuota en la misma magnitud (conservación de saldo).; Los detalles CxP se contabilizan al débito (DebitValue) y los CxC al crédito (CreditValue) en el comprobante de reversión.; Sólo entran al comprobante contable los detalles con ValueReversion > 0 y los conceptos otros con Value > 0.; El comprobante se inserta con Status=2 (estado fijo) y EntityName=''TreasuryNote'' apuntando a la nota de tesorería origen.; Si el SP contable falla (CodeMessage=999), no se modifica el estado del CrossingAccount (no se marca como reversado).; El tipo de comprobante a generar se toma de SettingsTreasury.JournalVoucherTypeTreasuryNotes según la unidad operativa de la nota.; Errores en cualquier punto del TRY se capturan y retornan como CodeMessage=999 con ERROR_MESSAGE() y línea, sin propagar excepción.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCrossingAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cruce de cuentas (CxC vs CxP); Reversión de cruce; Cuentas por pagar y sus cuotas; Cuentas por cobrar y sus cuotas; Nota de tesorería; Comprobante contable / Journal Voucher; Tercero / Centro de costo / Cuenta principal; Conceptos contables adicionales (Nature débito/crédito)', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCrossingAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @IdParticion = 1 (primera cuota de la partición de un documento) → Reinicia @CrossingValueTemp con el CrossingValue total a reversar para ese documento; si @CrossingValueTemp > 0 AND @CrossingValueShare > 0 (existe saldo cruzado por reversar en la cuota) → Devuelve directamente @CrossingValueShare (acotado al pendiente) restando de CrossingValue y sumando al Balance en cuotas y documento; si @CrossingValueTemp > 0 AND @CrossingValueShare = 0 AND @InitialValue >= @CrossingValueTemp AND @InitialValue >= @Balance + @CrossingValueTemp → Aplica reversión por @CrossingValueTemp completo en la cuota, dejando CrossingValue=0; si @CrossingValueTemp > 0 AND @CrossingValueShare = 0 AND @InitialValue >= @CrossingValueTemp (no cabe en Balance+temp) → Aplica reversión sólo por (@InitialValue - @Balance) y descuenta ese monto del temporal; si @CrossingValueTemp > 0 AND @CrossingValueShare = 0 AND 0 < @InitialValue < @CrossingValueTemp → Aplica reversión parcial por (@InitialValue - @Balance) y continúa con el remanente en siguientes cuotas; si Para CxC ajustada: (@InitialValue - @Balance) < @CrossingValueTemp → Acota @CrossingValueShare a (@InitialValue - @Balance) antes de aplicar la reversión adicional; si CrossingType = 1 en Treasury.CrossingAccount → Usa CA.ThirdPartyId como tercero del detalle; en caso contrario usa el tercero del documento (AP/AR); si Concepto otro con Nature = 2 → Lo registra como DebitValue; si Nature = 1 lo registra como CreditValue; si @CodeMessage = 999 al crear el comprobante contable → Retorna inmediatamente con CodeMessage=999 y el mensaje de error sin actualizar el cruce else Continúa, marca el cruce como reversado (Status=4) y devuelve el consecutivo del comprobante', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCrossingAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCrossingAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.TreasuryNote; Treasury.SettingsTreasury; Treasury.CrossingAccount; Treasury.CrossingAccountDetailCxP; Treasury.CrossingAccountDetailCxC; Treasury.CrossingAccountDetailOtherConcept; Payments.AccountPayable; Payments.AccountPayableShares; Portfolio.AccountReceivable; Portfolio.AccountReceivableAccounting; Portfolio.AccountReceivableShare; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCrossingAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCrossingAccount';
-- GO
