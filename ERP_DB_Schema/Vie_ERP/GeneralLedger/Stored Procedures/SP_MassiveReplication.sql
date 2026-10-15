-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 09/06/2017
-- Description:	Procedimiento que se encarga de replicar los comprobantes contables con los diferentes libros contables
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_MassiveReplication] 
	@InitialDate as date, --Fecha inicial, puede venir vacío
	@EndDate as date, --Fecha final, puede venir vacío
	@BookOriginId as INT, --Id del libro origen, no puede venir vacío
	@BookDestinationId as INT, --Id del libro destino, no puede venir vacío
	@CodeInitialJournalVoucherType as VARCHAR(20), --Código del tipo de comprobante inicial, puede venir vacío
	@CodeEndJournalVoucherType as VARCHAR(20), --Código del tipo de comprobante final, puede venir vacío
	@CodeUser as VARCHAR(20) --Código del usuario que realiza la operación
AS
BEGIN
	SET NOCOUNT ON;

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @message VARCHAR(MAX),
			-----------------------------------------------------------------------------------------------------------
			@JournalVoucherType_Rows INT = 1,
			@JournalVoucherTypeId INT = 0,
			-----------------------------------------------------------------------------------------------------------
			@Year_Rows INT = 1,
			@Year INT = 0,
			-----------------------------------------------------------------------------------------------------------
			@MinId INT,
			@MaxId INT,
			@LastConsecutive BIGINT

	-------------------------------------------------------------------------------------------------------------------
	
	DECLARE @JournalVourchers TABLE
	(
		Id INT IDENTITY(1,1),
		JournalVoucherTypeId INT,
		Year INT,
		Month INT,
		Consecutive BIGINT DEFAULT(0),
		AccountingMovementId INT,
		JournalVoucherId INT,
		JournalVoucherDestinationId INT
	)

	BEGIN TRY
	
		/***********************************************  ASIGNACIONES ***********************************************/

		--Si la fecha inicial viene vacía, se asigna una fecha
		IF ISNULL(@InitialDate, '') = ''
		BEGIN
			SET @InitialDate = '01/01/1900'
		END

		--Si la fecha final viene vacía, se asigna una fecha aumentandole 3 años a la fecha actual
		IF ISNULL(@EndDate, '') = ''
		BEGIN
			SET @EndDate = DATEADD(YEAR, 3, [Common].[GETDATE]())
		END

		--Se valida que el código del tipo comprobante inicial vengas vacio
		IF @CodeInitialJournalVoucherType IS NULL
		BEGIN
			SET @CodeInitialJournalVoucherType = ''
		END

		--Si el código del tipo de comprobante final viene vacio se llena de esta manera para dar un limite infinito para que consulte por todos los tipo de comprobante
		IF ISNULL(@CodeEndJournalVoucherType, '') = ''
		BEGIN
			SET @CodeEndJournalVoucherType = 'zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz'
		END

		--Se insertan los documentos que se replicaran
		INSERT INTO @JournalVourchers (JournalVoucherTypeId, Year, Month, AccountingMovementId, JournalVoucherId)
			SELECT jvt.Id, YEAR(jv.VoucherDate), MONTH(jv.VoucherDate), jv.AccountingMovementId, jv.Id
			FROM GeneralLedger.JournalVoucherTypes jvt
			JOIN GeneralLedger.JournalVouchers jv ON jvt.Id = jv.IdJournalVoucher
			LEFT JOIN Payments.AccountPayable ap ON jv.EntityName = 'AccountPayable' AND jv.EntityId = ap.Id AND ap.EntityName IN ('FixedAssetEntry', 'FixedAssetTransaction')
			LEFT JOIN Payments.PaymentNotes pn ON jv.EntityName = 'PaymentNotes' AND jv.EntityId = pn.Id AND pn.EntityName IN ('FixedAssetEntryDevolution')
			LEFT JOIN GeneralLedger.VieBot vb ON COALESCE(pn.EntityName, pn.EntityName, jv.EntityName) = vb.Form AND jv.LegalBookId = vb.LegalBookId
			LEFT JOIN
			(
				SELECT jv.AccountingMovementId
				FROM GeneralLedger.JournalVouchers jv
				WHERE jv.LegalBookId = @BookDestinationId
			) jvh ON jv.AccountingMovementId = jvh.AccountingMovementId
			WHERE jv.[Status] = 2
				AND jv.IsClosedYear = 0
				AND jv.LegalBookId = @BookOriginId
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @InitialDate AND @EndDate
				AND jvt.Code BETWEEN @CodeInitialJournalVoucherType AND @CodeEndJournalVoucherType
				AND ISNULL(vb.HandlesHomologation, 1) = 1
				AND jvh.AccountingMovementId IS NULL
			ORDER BY 1, 2, 3, 4

		/***********************************************  VALIDACIONES ***********************************************/

		if DATEPART(hour, Common.GETDATE()) < 16 and DATEPART(hour, Common.GETDATE()) > 18 begin 
			SELECT 999 as CodeMessage, 'Este proceso solo se puede ejecutar entre 4PM y 6PM' as Message
			RETURN
		end

		--Validamos que el documento origen sea el libro oficial
		IF NOT EXISTS (SELECT 1 FROM GeneralLedger.LegalBook WHERE Id = @BookOriginId AND Status = 1 AND OfficialBook = 1)
		BEGIN
			SELECT 999 as CodeMessage, 'El Libro Contable Origen debe ser el libro oficial' as Message
			RETURN
		END

		--Se valida que hayan detalles para poder crear los comprobantes
		IF NOT EXISTS
		(
			SELECT 1 FROM @JournalVourchers
		)
		BEGIN
			SELECT 999 as CodeMessage, 'No se encontraron datos con los filtros especificados' as Message
			RETURN
		END

		--Se valida que no existan detalles duplicados
		IF EXISTS
		(
			SELECT 1 
			FROM @JournalVourchers
			GROUP BY AccountingMovementId
			HAVING COUNT(1) > 1
		)
		BEGIN
			SELECT @message = STUFF((
				SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', 'El comprobante contable ', jv.Consecutive, ' de tipo ', jvt.Code, ' - ', jvt.Name, ' se encuentra duplicado')
				FROM 
				(
					SELECT AccountingMovementId
					FROM @JournalVourchers
					GROUP BY AccountingMovementId
					HAVING COUNT(1) > 1
				) j
				JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON j.AccountingMovementId = jv.AccountingMovementId
				JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON jv.IdJournalVoucher = jvt.Id
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nVARCHAR(max)'), 1, 2, N'')

			SELECT 999 as CodeMessage, @message as Message
			RETURN
		END

		--Se valida que existan los consecutivos para el tipo de comprobante en el libro contable
		IF EXISTS
		(
			SELECT 1 
			FROM @JournalVourchers j
			LEFT JOIN GeneralLedger.JournalVoucherTypeConsecutive jvtc ON j.JournalVoucherTypeId = jvtc.JournalVoucherTypeId AND @BookDestinationId = jvtc.LegalBookId AND j.Year = jvtc.Year
			WHERE jvtc.Id IS NULL
		)
		BEGIN
			SELECT @message = STUFF((
				SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', 'El tipo de comprobante contable ', jvt.Code, ' - ', jvt.Name, ' no tiene parametrizada una secuencia para el libro en el año ', j.Year)
				FROM @JournalVourchers j
				JOIN GeneralLedger.JournalVoucherTypes jvt ON j.JournalVoucherTypeId = jvt.Id
				LEFT JOIN GeneralLedger.JournalVoucherTypeConsecutive jvtc ON j.JournalVoucherTypeId = jvtc.JournalVoucherTypeId AND @BookDestinationId = jvtc.LegalBookId AND j.Year = jvtc.Year
				WHERE jvtc.Id IS NULL
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nVARCHAR(max)'), 1, 2, N'')

			SELECT 999 as CodeMessage, @message as Message
			RETURN
		END

		--Validar que el periodo contable se encuentre abierto
		IF EXISTS
		(
			SELECT 1 
			FROM @JournalVourchers j
			LEFT JOIN GeneralLedger.ClosedMonth cm ON j.Year = cm.Year AND j.Month = cm.Month AND cm.Status = 1
			WHERE cm.Id IS NULL
		)
		BEGIN
			SELECT @message = STUFF((
				SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', 'El periodo contable ', j.Year, '-', j.Month, ' no se encuentra abierto')
				FROM @JournalVourchers j
				LEFT JOIN GeneralLedger.ClosedMonth cm ON j.Year = cm.Year AND j.Month = cm.Month AND cm.Status = 1
				WHERE cm.Id IS NULL
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nVARCHAR(max)'), 1, 2, N'')

			SELECT 999 as CodeMessage, @message as Message
			RETURN
		END

		--Se valida que las cuentas usadas en el periodo del libro origen tengan su homologa en el libro destino
		IF EXISTS 
		(
			SELECT 1
			FROM GeneralLedger.JournalVouchers jv WITH (NOLOCK) 
			JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON jv.IdJournalVoucher = jvt.Id
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			LEFT JOIN
			(
				SELECT ha.OfficialMainAccountId, mah.Id
				FROM GeneralLedger.HomologationAccount ha WITH (NOLOCK) 
				JOIN GeneralLedger.MainAccounts mah WITH (NOLOCK) ON ha.MainAccountId = mah.Id 
				WHERE mah.LegalBookId = @BookDestinationId
			) ha ON jvd.IdMainAccount = ha.OfficialMainAccountId
			WHERE jv.Status = 2
				AND jv.IsClosedYear = 0
				AND jv.LegalBookId = @BookOriginId
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @InitialDate AND @EndDate
				AND jvt.Code BETWEEN @CodeInitialJournalVoucherType AND @CodeEndJournalVoucherType
				AND ha.Id IS NULL
		)
		BEGIN
			SELECT @message = STUFF((
				SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + 'La cuenta contable ' + ma.Number + ' - ' + ma.[Name] + ' del libro contable origen no está homologada'
				FROM GeneralLedger.JournalVouchers jv WITH (NOLOCK) 
				JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON jv.IdJournalVoucher = jvt.Id
				JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id
				LEFT JOIN 
				(
					SELECT ha.OfficialMainAccountId, mah.Id
					FROM GeneralLedger.HomologationAccount ha WITH (NOLOCK) 
					JOIN GeneralLedger.MainAccounts mah WITH (NOLOCK) ON ha.MainAccountId = mah.Id 
					WHERE mah.LegalBookId = @BookDestinationId
				) ha ON jvd.IdMainAccount = ha.OfficialMainAccountId
				WHERE jv.[Status] = 2
					AND jv.IsClosedYear = 0
					AND jv.LegalBookId = @BookOriginId
					AND CAST(jv.VoucherDate AS DATE) BETWEEN @InitialDate AND @EndDate
					AND jvt.Code BETWEEN @CodeInitialJournalVoucherType AND @CodeEndJournalVoucherType
					AND ha.Id IS NULL
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nVARCHAR(max)'), 1, 2, N'')

			SELECT 999 as CodeMessage, @message as Message
			RETURN
		END

		--Se valida que las cuentas contables origen no esten homologadas multiples veces
		IF EXISTS 
		(
			SELECT 1
			FROM GeneralLedger.HomologationAccount ha WITH (NOLOCK)
			JOIN GeneralLedger.MainAccounts mao WITH (NOLOCK) ON ha.OfficialMainAccountId = mao.Id AND mao.LegalBookId = @BookOriginId
			JOIN GeneralLedger.MainAccounts mah WITH (NOLOCK) ON ha.MainAccountId = mah.Id AND mah.LegalBookId = @BookDestinationId
			GROUP BY mao.Id
			HAVING COUNT(1) > 1
		)
		BEGIN
			SELECT @message = STUFF((
				SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + 'La cuenta contable ' + mao.Number + ' - ' + mao.[Name] + ' del libro contable origen se encuentra homologada más de una vez'
				FROM GeneralLedger.HomologationAccount ha WITH (NOLOCK)
				JOIN GeneralLedger.MainAccounts mao WITH (NOLOCK) ON ha.OfficialMainAccountId = mao.Id AND mao.LegalBookId = @BookOriginId
				JOIN GeneralLedger.MainAccounts mah WITH (NOLOCK) ON ha.MainAccountId = mah.Id AND mah.LegalBookId = @BookDestinationId
				GROUP BY mao.Id, mao.Number, mao.Name
				HAVING COUNT(1) > 1
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nVARCHAR(max)'), 1, 2, N'')

			SELECT 999 as CodeMessage, @message as Message
			RETURN
		END

		--Se valida que las cuentas contables destino tengan la misma estructura que la cuenta contable origen
		IF EXISTS 
		(
			SELECT 1
			FROM GeneralLedger.HomologationAccount ha WITH (NOLOCK)
			JOIN GeneralLedger.MainAccounts mao WITH (NOLOCK) ON ha.OfficialMainAccountId = mao.Id AND mao.LegalBookId = @BookOriginId
			JOIN GeneralLedger.MainAccounts mah WITH (NOLOCK) ON ha.MainAccountId = mah.Id AND mah.LegalBookId = @BookDestinationId
			WHERE
			(
				mao.AllowsMovement <> mah.AllowsMovement
				OR
				mao.HandlesThirdParty <> mah.HandlesThirdParty
				OR
				mao.HandlesCostCenter <> mah.HandlesCostCenter
			)
		)
		BEGIN
			SELECT @message = STUFF((
				SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', 'La cuenta contable ', mah.Number, ' - ', mah.Name, ' homologa no tiene la estructura de la cuenta contable ', mao.Number, ' - ', mao.Name, ' original')
				FROM GeneralLedger.HomologationAccount ha WITH (NOLOCK)
				JOIN GeneralLedger.MainAccounts mao WITH (NOLOCK) ON ha.OfficialMainAccountId = mao.Id AND mao.LegalBookId = @BookOriginId
				JOIN GeneralLedger.MainAccounts mah WITH (NOLOCK) ON ha.MainAccountId = mah.Id AND mah.LegalBookId = @BookDestinationId
				WHERE
				(
					mao.AllowsMovement <> mah.AllowsMovement
					OR
					mao.HandlesThirdParty <> mah.HandlesThirdParty
					OR
					mao.HandlesCostCenter <> mah.HandlesCostCenter
				)
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nVARCHAR(max)'), 1, 2, N'')

			SELECT 999 as CodeMessage, @message as Message
			RETURN
		END
		
		/******************************************  GENERACION CONSECUTIVO ******************************************/

		WHILE @JournalVoucherType_Rows > 0
		BEGIN
			SELECT TOP 1
				@JournalVoucherTypeId = j.JournalVoucherTypeId,
				---------------------------------------------------------------
				@Year_Rows = 1,
				@Year = 0
			FROM @JournalVourchers j
			WHERE j.JournalVoucherTypeId > @JournalVoucherTypeId
			ORDER BY j.JournalVoucherTypeId

			-- Verifico que haya encontrado un resultado
			SET @JournalVoucherType_Rows = @@RowCount
			IF @JournalVoucherType_Rows = 0
			BEGIN
				BREAK
			END

			-----------------------------------------------------------------------------------------------------------

			WHILE @Year_Rows > 0
			BEGIN
				SELECT TOP 1
					@Year = j.Year,
					@MinId = MIN(j.Id),
					@MaxId = MAX(j.Id)
				FROM @JournalVourchers j
				WHERE j.JournalVoucherTypeId = @JournalVoucherTypeId
					AND j.Year > @Year
				GROUP BY j.Year
				ORDER BY j.Year

				-- Verifico que haya encontrado un resultado
				SET @Year_Rows = @@RowCount
				IF @Year_Rows = 0
				BEGIN
					BREAK
				END

				-------------------------------------------------------------------------------------------------------

				-- Se actualiza el consecutivo de acuerdo a la cantidad de registros a realizar
				UPDATE jvtc
					SET @LastConsecutive = jvtc.Consecutive = (jvtc.Consecutive + (@MaxId - @MinId + 1))
				FROM GeneralLedger.JournalVoucherTypeConsecutive jvtc
				WHERE jvtc.JournalVoucherTypeId = @JournalVoucherTypeId
					AND jvtc.LegalBookId = @BookDestinationId
					AND jvtc.Year = @Year

				--Actualizamos el consecutivo con el que quedaran los comprobantes
				UPDATE j
					SET j.Consecutive = (@LastConsecutive - 1) - (@MaxId - j.Id)
				FROM @JournalVourchers j
				WHERE j.JournalVoucherTypeId = @JournalVoucherTypeId
					AND j.Year = @Year

			END
		END

		/************************************************** PROCESO **************************************************/

		INSERT INTO GeneralLedger.JournalVouchers
		(
			AccountingMovementId, Consecutive, LegalBookId, IdJournalVoucher, VoucherDate, Imported, Status, Detail,
			EntityCode, EntityId, EntityName, IsClosedYear, CreationUser, CreationDate, ConfirmationUser, ConfirmationDate, YearMovement
		)
		SELECT	jv.AccountingMovementId, j.Consecutive, @BookDestinationId, jv.IdJournalVoucher, jv.VoucherDate, jv.Imported, jv.Status, jv.Detail,
				jv.EntityCode, jv.EntityId, jv.EntityName, jv.IsClosedYear, @CodeUser, [Common].[GETDATE](), @CodeUser, [Common].[GETDATE](), jv.YearMovement
		FROM @JournalVourchers j
		JOIN GeneralLedger.JournalVouchers jv ON j.JournalVoucherId = jv.Id
		ORDER BY jv.IdJournalVoucher

		UPDATE j
			SET j.JournalVoucherDestinationId = jv.Id
		FROM @JournalVourchers j
		JOIN GeneralLedger.JournalVouchers jv ON j.AccountingMovementId = jv.AccountingMovementId AND @BookDestinationId = jv.LegalBookId

		INSERT INTO GeneralLedger.JournalVoucherDetails
		(
			IdAccounting, IdMainAccount, IdThirdParty, IdCostCenter, 
			DebitValue, CreditValue, Detail, 
			IdRetention, RetentionRate, BaseValue, BillingValue
		)
		SELECT	j.JournalVoucherDestinationId, ha.MainAccountId, jvd.IdThirdParty, jvd.IdCostCenter,
				jvd.DebitValue, jvd.CreditValue, jvd.Detail,
				jvd.IdRetention, jvd.RetentionRate, jvd.BaseValue, jvd.BillingValue
		FROM @JournalVourchers j
		JOIN GeneralLedger.JournalVoucherDetails jvd ON j.JournalVoucherId = jvd.IdAccounting
		JOIN GeneralLedger.HomologationAccount ha ON jvd.IdMainAccount = ha.OfficialMainAccountId
		JOIN GeneralLedger.MainAccounts ma ON ha.MainAccountId = ma.Id AND @BookDestinationId = ma.LegalBookId

		/***********************************************  MAYORIZACIÓN ***********************************************/

		UPDATE glb
			SET glb.DebitValue = glb.DebitValue + jvd.DebitValue,
				glb.CreditValue = glb.CreditValue + jvd.CreditValue
		FROM GeneralLedger.GeneralLedgerBalance glb
		JOIN
		(
			SELECT	j.Year, j.Month, jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter,
					SUM(jvd.DebitValue) DebitValue, SUM(jvd.CreditValue) CreditValue
			FROM @JournalVourchers j
			JOIN GeneralLedger.JournalVoucherDetails jvd ON j.JournalVoucherDestinationId = jvd.IdAccounting
			GROUP BY j.Year, j.Month, jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter
		) jvd ON glb.Year = jvd.Year AND glb.Month = jvd.Month
			AND glb.IdMainAccount = jvd.IdMainAccount
			AND ISNULL(glb.IdThirdParty, 0) = ISNULL(jvd.IdThirdParty, 0)
			AND ISNULL(glb.IdCostCenter, 0) = ISNULL(jvd.IdCostCenter, 0)

		INSERT INTO GeneralLedger.GeneralLedgerBalance
		(
			Month, Year, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue
		)
		SELECT jvd.Month, jvd.Year, jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter, jvd.DebitValue, jvd.CreditValue
		FROM 
		(
			SELECT	j.Year, j.Month, jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter,
					SUM(jvd.DebitValue) DebitValue, SUM(jvd.CreditValue) CreditValue
			FROM @JournalVourchers j
			JOIN GeneralLedger.JournalVoucherDetails jvd ON j.JournalVoucherDestinationId = jvd.IdAccounting
			GROUP BY j.Year, j.Month, jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter
		) jvd
		LEFT JOIN GeneralLedger.GeneralLedgerBalance glb
		 ON glb.Year = jvd.Year AND glb.Month = jvd.Month
			AND glb.IdMainAccount = jvd.IdMainAccount
			AND ISNULL(glb.IdThirdParty, 0) = ISNULL(jvd.IdThirdParty, 0)
			AND ISNULL(glb.IdCostCenter, 0) = ISNULL(jvd.IdCostCenter, 0)
		WHERE glb.Id IS NULL
		
		/***********************************************  RESULTADO ***********************************************/

		SELECT @message = STUFF((
				SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', 'Se generó el comprobate contable ', jv.Consecutive, ' de tipo ', jvt.Code, ' - ', jvt.Name)
				FROM @JournalVourchers j
				JOIN GeneralLedger.JournalVouchers jv ON j.JournalVoucherDestinationId = jv.Id
				JOIN GeneralLedger.JournalVoucherTypes jvt ON jv.IdJournalVoucher = jvt.Id
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nVARCHAR(max)'), 1, 2, N'')

		SELECT 0 AS CodeMessage, @message AS Message		
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() AS Message
	END CATCH
END