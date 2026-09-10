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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de replicación masiva de comprobantes contables entre libros contables (por ejemplo, del libro oficial hacia un libro auxiliar o alternativo). Toma un rango de fechas, un rango de tipos de comprobante y los identificadores del libro origen y destino, y copia todos los comprobantes contables aprobados (estado 2, año no cerrado) que aún no existan en el libro destino. Valida que el libro origen sea el libro oficial, que no haya duplicados en los movimientos contables a replicar, y que la ejecución ocurra en la franja horaria permitida (4PM–6PM). También considera si los comprobantes están asociados a cuentas por pagar de activos fijos o notas de pago, y respeta las reglas de homologación configuradas en VieBot antes de replicar.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_MassiveReplication';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_MassiveReplication';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Replica masivamente los comprobantes contables del libro oficial hacia un libro contable destino, validando homologación de cuentas, periodos abiertos y consecutivos, e impactando el saldo mayorizado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_MassiveReplication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El libro origen debe existir, estar activo (Status=1) y ser el libro oficial (OfficialBook=1); Para cada tipo de comprobante/año a replicar debe existir un consecutivo configurado en el libro destino; El periodo (año-mes) de cada comprobante debe estar abierto en GeneralLedger.ClosedMonth con Status=1; Toda cuenta contable usada en el origen debe tener homologación única hacia el libro destino; La cuenta homóloga destino debe coincidir con la origen en AllowsMovement, HandlesThirdParty y HandlesCostCenter; No deben existir AccountingMovementId duplicados entre los comprobantes seleccionados; El comprobante origen debe estar en Status=2, no ser de año cerrado y pertenecer al libro origen', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_MassiveReplication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se replican comprobantes con Status=2 (confirmados) e IsClosedYear=0; Solo se replican comprobantes cuyo libro origen es el oficial (OfficialBook=1, Status=1); Nunca se replica un AccountingMovementId que ya exista en el libro destino; Solo se procesan formularios cuyo VieBot.HandlesHomologation = 1 (o sin registro); Cada comprobante destino se inserta con CreationUser/ConfirmationUser igual al usuario que invoca, y fechas obtenidas de Common.GETDATE(); Los detalles destino usan la cuenta homóloga (HomologationAccount.MainAccountId) del libro destino, conservando tercero, centro de costo, valores débito/crédito y retenciones; El consecutivo asignado a cada comprobante es contiguo dentro de un mismo tipo y año, y se reserva incrementando JournalVoucherTypeConsecutive.Consecutive por la cantidad replicada; La mayorización mantiene saldos por Año/Mes/Cuenta/Tercero/CentroCosto, tratando NULL como 0 en tercero y centro de costo; Si cualquier validación falla, no se ejecuta inserción ni actualización (RETURN previo al bloque de proceso)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_MassiveReplication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante contable; Libro contable oficial; Libro contable destino; Tipo de comprobante; Consecutivo de comprobante; Periodo contable abierto/cerrado; Homologación de cuentas contables; Cuenta principal; Tercero; Centro de costo; Débito y crédito; Mayorización (saldo del libro mayor); Movimiento contable; Cuenta por pagar; Notas de pago; Activos fijos (FixedAssetEntry, FixedAssetTransaction, FixedAssetEntryDevolution)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_MassiveReplication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Fecha inicial nula o vacía → Se asume 01/01/1900 como fecha inicial; si Fecha final nula o vacía → Se asume la fecha actual + 3 años como fecha final; si Código de tipo de comprobante final nulo o vacío → Se asigna ''zzzz...'' para cubrir todo el rango de tipos; si Libro origen no existe con Status=1 y OfficialBook=1 → Aborta con mensaje ''El Libro Contable Origen debe ser el libro oficial'' (CodeMessage=999); si No se encontraron comprobantes elegibles según filtros → Aborta con mensaje ''No se encontraron datos con los filtros especificados''; si Existen AccountingMovementId duplicados en la selección → Aborta listando los comprobantes duplicados; si Falta consecutivo parametrizado para el tipo de comprobante en el libro destino y año → Aborta listando los tipos sin secuencia configurada; si Existe año/mes del comprobante sin registro abierto en ClosedMonth (Status=1) → Aborta indicando que el periodo contable no está abierto; si Cuenta del libro origen no tiene homóloga en el libro destino → Aborta listando las cuentas no homologadas; si Una cuenta origen está homologada más de una vez al libro destino → Aborta listando las cuentas con homologación múltiple; si La cuenta destino difiere en AllowsMovement, HandlesThirdParty o HandlesCostCenter respecto a la origen → Aborta indicando inconsistencia de estructura entre cuenta origen y homóloga; si Se produce cualquier error en el TRY → CATCH devuelve CodeMessage=999 con ERROR_MESSAGE(); si En la mayorización: existe fila en GeneralLedgerBalance para Year/Month/cuenta/tercero/centro → Se actualiza sumando débito/crédito else Se inserta nueva fila de saldo', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_MassiveReplication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_MassiveReplication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVoucherTypes; GeneralLedger.JournalVouchers; Payments.AccountPayable; Payments.PaymentNotes; GeneralLedger.VieBot; GeneralLedger.LegalBook; GeneralLedger.JournalVoucherTypeConsecutive; GeneralLedger.ClosedMonth; GeneralLedger.JournalVoucherDetails; GeneralLedger.HomologationAccount; GeneralLedger.MainAccounts; GeneralLedger.GeneralLedgerBalance', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_MassiveReplication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_MassiveReplication';
-- GO
