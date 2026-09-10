

CREATE PROCEDURE [GeneralLedger].[SP_ConfirmJournalVoucherById]
	@JournalVoucherId as int,
	@CodeUser as varchar(20),
	------------------------------------------------------
	@CodeMessage Int Output,
	@Message Varchar(Max) Output,
	@IdJournalVoucherResult Int Output
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/
	
	DECLARE @IsHomologation BIT = 0,
			@OfficialLegalBookId INT,
			------------- Variables para la contabilizacion
			@Rows INT, 
			@RowId INT,
			@MovementLegalBookId INT,
			@MovementJournalVoucherId INT,
			@MonthMovement INT,
			@Message_Output VARCHAR(MAX)

	DECLARE @IdJournalVoucher INT, 
			@AccountingMovementId INT, 
			@Consecutive BIGINT, 
			@LegalBookId INT, 
			@IdJournalVoucherType INT, 
			@VoucherDate DATETIME, 
			@Imported BIT, 
			@Status TINYINT, 
			@Detail VARCHAR(MAX), 
			@EntityCode VARCHAR(20), 
			@EntityId INT, 
			@EntityName VARCHAR(250), 
			@OriginEntityName VARCHAR(250),
			@IsClosedYear TINYINT,
			@ConfirmationUser VARCHAR(20), 
			@ConfirmationDate DATETIME

	--Tabla para almacenar los detalles del comprobante
	DECLARE @TableDetail TABLE
	(
		RowId Int IDENTITY(1,1),
		Id INT, 
		IdMainAccount INT, 
		IdThirdParty INT, 
		IdCostCenter INT, 
		DebitValue DECIMAL(20, 4), 
		CreditValue DECIMAL(20, 4), 
		Detail VARCHAR(MAX), 
		IdRetention INT, 
		RetentionRate DECIMAL(6, 3), 
		BaseValue DECIMAL(18, 2),
		BillingValue DECIMAL(18, 2), 
		IsDelete BIT
	)

	--Tabla para saber que libros tengo que afectar
	DECLARE @TableBookMovement TABLE
	(
		RowId Int IDENTITY(1,1),
		LegalBookId INT, 
		JournalVoucherId INT
	)
	
	--Tabla para el registro de la homologacion
	CREATE TABLE #TableDetailHomologation
	(	
		LegalBookId INT,
		TableDetailRowId INT,
		---------------------------------------------------
		Id INT, 
		IdMainAccount INT, 
		IdThirdParty INT, 
		IdCostCenter INT, 
		DebitValue DECIMAL(20, 4), 
		CreditValue DECIMAL(20, 4), 
		Detail varchar(MAX), 
		IdRetention INT, 
		RetentionRate DECIMAL(6, 3), 
		BaseValue DECIMAL(18, 2),
		BillingValue DECIMAL(18, 2)
	)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT	@IdJournalVoucher = Id,
				@AccountingMovementId = AccountingMovementId,
				@Consecutive = Consecutive,
				@LegalBookId = LegalBookId,				
				@IdJournalVoucherType = IdJournalVoucher,
				@VoucherDate = VoucherDate,		
				@Imported = Imported,
				@Status = 2,
				@Detail = Detail,
				@EntityCode = EntityCode,
				@EntityId = EntityId,
				@EntityName = EntityName,
				@OriginEntityName = EntityName,
				@IsClosedYear = IsClosedYear
		FROM GeneralLedger.JournalVouchers where Id = @JournalVoucherId

		--Se consulta el libro oficial
		SELECT @OfficialLegalBookId = Id 
		FROM GeneralLedger.LegalBook WITH (NOLOCK)
		WHERE OfficialBook = 1 AND Status = 1

		--Si el registro no cuenta con libro, se asigna el libro oficial
		IF @LegalBookId IS NULL BEGIN
			SELECT @LegalBookId = @OfficialLegalBookId
		END

		--Identificar si es una homologación
		SELECT	@IsHomologation = 1 
		FROM GeneralLedger.JournalVouchers WITH (NOLOCK)
		WHERE @IdJournalVoucher = 0 AND AccountingMovementId = @AccountingMovementId

		/******************************************  VALIDACIONES GENERALES ******************************************/

		--Valido que no se este duplicando el registro
		IF @IdJournalVoucher = 0 AND EXISTS (SELECT 1 FROM GeneralLedger.JournalVouchers WITH (NOLOCK) WHERE AccountingMovementId = @AccountingMovementId AND LegalBookId = @LegalBookId)
		BEGIN
			SELECT	@CodeMessage = 999, 
					@Message = CONCAT('Ya existe un comprobante contable homologado en el libro ', Code, ' - ', Name),
					@IdJournalVoucherResult = 0
			FROM GeneralLedger.LegalBook WITH (NOLOCK)
			WHERE Id = @LegalBookId
			RETURN
		END

		--Valido que si el documento se encuentra homologado, solo se puede editar el documento que pertenece al libro oficial
		IF @LegalBookId <> @OfficialLegalBookId AND EXISTS (SELECT 1 FROM GeneralLedger.JournalVouchers WITH (NOLOCK) WHERE AccountingMovementId = @AccountingMovementId GROUP BY AccountingMovementId HAVING COUNT(1) > 1)
		BEGIN
			SELECT	@CodeMessage = 999, 
					@Message = 'El comprobante contable no se puede editar debido a que es un documento homologo y solo se puede editar el registro correspondiente al libro oficial',
					@IdJournalVoucherResult = 0
			RETURN
		END

		--Valido que el comprobante corresponda con el movimiento
		IF @IdJournalVoucher > 0 AND NOT EXISTS (SELECT 1 FROM GeneralLedger.JournalVouchers WITH (NOLOCK) WHERE Id = @IdJournalVoucher AND AccountingMovementId = @AccountingMovementId)
		BEGIN
			SELECT	@CodeMessage = 999, 
					@Message = 'El comprobante contable no corresponde con el movimiento contable',
					@IdJournalVoucherResult = 0
			RETURN
		END

		--Valido que no exista un documento homologo en un estado diferente
		IF EXISTS 
		(
			SELECT 1 
			FROM GeneralLedger.JournalVouchers jvo WITH (NOLOCK)
			JOIN GeneralLedger.JournalVouchers jvd WITH (NOLOCK) ON jvo.AccountingMovementId = jvd.AccountingMovementId AND jvo.Id > jvd.Id
			WHERE jvo.AccountingMovementId = @AccountingMovementId AND jvo.Status <> jvd.Status
		)
		BEGIN
			SELECT	@CodeMessage = 999, 
					@Message = 'El comprobante contable tiene documentos homologos en diferente estado',
					@IdJournalVoucherResult = 0
			RETURN
		END

		--Valido que el estado corresponda con la accion a realizar
		IF @Status = 4
		BEGIN
			--Si estoy desconfirmando el comprobante contable debe estar confirmado
			IF EXISTS (SELECT 1 FROM GeneralLedger.JournalVouchers WITH (NOLOCK) WHERE AccountingMovementId = @AccountingMovementId AND Status <> 2)
			BEGIN
				SELECT	@CodeMessage = 999,
						@Message = CONCAT('El comprobante contable del Libro ', lb.Code, ' - ', lb.Name, ' Consecutivo ', jv.Consecutive,' y Tipo de Comprobante ', jvt.Code, ' - ', jvt.Name ,' se encuentra en estado: ', CASE jv.Status 
																																																								WHEN 1 THEN 'Registrado'
																																																								WHEN 3 THEN 'Anulado'
																																																								ELSE 'N/A'
																																																							END),
						@IdJournalVoucherResult = 0
				FROM GeneralLedger.JournalVouchers jv WITH (NOLOCK)
				JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON jv.LegalBookId = lb.Id
				JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON jv.IdJournalVoucher = jvt.Id
				WHERE jv.AccountingMovementId = @AccountingMovementId AND jv.Status <> 2
				RETURN
			END

			--No se puede desconfirmar un comprobante contable interfazado, se deben ajustar por notas contables
			IF EXISTS (SELECT 1 FROM GeneralLedger.JournalVouchers WITH (NOLOCK) WHERE AccountingMovementId = @AccountingMovementId AND ISNULL(EntityName, '') NOT IN ('', 'JournalVouchers'))
			BEGIN
				SELECT	@CodeMessage = 999, 
						@Message = 'El comprobante contable no se puede desconfirmar ya que fue un documento interfazado',
						@IdJournalVoucherResult = 0
				RETURN
			END
		END
		ELSE IF @IsHomologation = 0 AND EXISTS (SELECT 1 FROM GeneralLedger.JournalVouchers WITH (NOLOCK) WHERE AccountingMovementId = @AccountingMovementId AND Status <> 1)
		BEGIN
			--Si no es una homologación el comprobante contable debe estar en estado registrado
			SELECT	@CodeMessage = 999,
					@Message = CONCAT('El comprobante contable del Libro ', lb.Code, ' - ', lb.Name, ' Consecutivo ', jv.Consecutive,' y Tipo de Comprobante ', jvt.Code, ' - ', jvt.Name ,' se encuentra en estado: ', CASE jv.Status 
																																																							WHEN 2 THEN 'Confirmado'
																																																							WHEN 3 THEN 'Anulado'
																																																							ELSE 'N/A'
																																																						END),
					@IdJournalVoucherResult = 0
			FROM GeneralLedger.JournalVouchers jv WITH (NOLOCK)
			JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON jv.LegalBookId = lb.Id
			JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON jv.IdJournalVoucher = jvt.Id
			WHERE jv.AccountingMovementId = @AccountingMovementId AND jv.Status <> 1
			RETURN
		END

		/****************************************************** ******************************************************/

		IF @Status = 3
		BEGIN
			UPDATE GeneralLedger.JournalVouchers
				SET Status = @Status,
					ModificationUser = @CodeUser,
					ModificationDate = [Common].[GETDATE](),
					AnnulmentUser = @CodeUser,
					AnnulmentDate = [Common].[GETDATE]()
			WHERE AccountingMovementId = @AccountingMovementId
		END
		ELSE
		BEGIN
			--Libros a contabilizar de acuerdo a la configuración de VIEBOT
			IF @Status = 4
			BEGIN
				INSERT INTO @TableBookMovement
					SELECT j.LegalBookId, j.Id JournalVoucherId
					FROM GeneralLedger.JournalVouchers j WITH (NOLOCK)
					WHERE j.AccountingMovementId = @AccountingMovementId 
			END
			ELSE IF @IsHomologation = 1
			BEGIN
				INSERT INTO @TableBookMovement VALUES (@LegalBookId, @IdJournalVoucher)
			END
			ELSE IF @EntityName = 'JournalVouchers' OR @EntityName = '' 
			BEGIN
				IF EXISTS (SELECT 1 FROM GeneralLedger.JournalVouchers WITH (NOLOCK) WHERE AccountingMovementId = @AccountingMovementId)
				BEGIN
					INSERT INTO @TableBookMovement
						SELECT j.LegalBookId, j.Id JournalVoucherId
						FROM GeneralLedger.JournalVouchers j WITH (NOLOCK)
						WHERE j.AccountingMovementId = @AccountingMovementId 
				END
				ELSE
				BEGIN
					INSERT INTO @TableBookMovement VALUES (@LegalBookId, @IdJournalVoucher)
				END
			END
			ELSE
			BEGIN
				INSERT INTO @TableBookMovement
					SELECT vb.LegalBookId, 0 JournalVoucherId
					FROM [GeneralLedger].[VieBot] vb WITH (NOLOCK)
					WHERE vb.Form = IIF(ISNULL(@OriginEntityName, @EntityName)='MedicalFeesLiquidation','AccountPayable',ISNULL(@OriginEntityName, @EntityName)) AND vb.Allow = 1
						AND
						(
							vb.HandlesHomologation = 1
							OR
							(vb.LegalBookId = @LegalBookId)
						)

				UPDATE tbm
					SET tbm.JournalVoucherId = j.Id
				FROM @TableBookMovement tbm
				JOIN GeneralLedger.JournalVouchers j WITH (NOLOCK) ON tbm.LegalBookId = j.LegalBookId and j.AccountingMovementId = @AccountingMovementId 

				INSERT INTO @TableBookMovement
					SELECT j.LegalBookId, j.Id JournalVoucherId
					FROM GeneralLedger.JournalVouchers j WITH (NOLOCK)
					LEFT JOIN @TableBookMovement tbm ON j.LegalBookId = tbm.LegalBookId
					WHERE j.AccountingMovementId = @AccountingMovementId AND tbm.RowId IS NULL
			END

			/***************************************** VALIDACIONES CABECERA *****************************************/

			--Valido que exista un Libro Oficial
			IF @OfficialLegalBookId IS NULL 
			BEGIN				
				SELECT	@CodeMessage = 999,
						@Message = 'El comprobante contable no se puede crear ya que no existe un libro oficial',
						@IdJournalVoucherResult = 0
				RETURN
			END

			--Valido que exista al menos un libro al cual afectar
			IF NOT EXISTS (SELECT 1 FROM @TableBookMovement)
			BEGIN
				SELECT	@CodeMessage = 999,
						@Message = 'El comprobante contable no se genero ningun comprobante ya que asi esta configurado en VieBot',
						@IdJournalVoucherResult = 0
				RETURN
			END

			--Valido que el año este abierto
			IF EXISTS 
			(
				SELECT 1 
				FROM GeneralLedger.LegalBook lb WITH (NOLOCK)
				JOIN @TableBookMovement tbm ON lb.Id = tbm.LegalBookId
				WHERE YEAR(@VoucherDate) <= LastYearClose
			) 
			BEGIN
				SELECT	@CodeMessage = 999,
						@Message = CONCAT('El comprobante contable no se puede crear ya que el año ', YEAR(@VoucherDate), ' se encuentra cerrado'),
						@IdJournalVoucherResult = 0
				RETURN
			END

			--Valido que el mes este abierto
			IF @IsClosedYear = 0 AND NOT EXISTS (SELECT 1 FROM GeneralLedger.ClosedMonth WITH (NOLOCK) WHERE Year = YEAR(@VoucherDate) AND Month = MONTH(@VoucherDate) AND Status = 1)
			BEGIN
				SELECT	@CodeMessage = 999,
						@Message = CONCAT('El comprobante contable no se puede crear ya que el periodo ', YEAR(@VoucherDate), '-', RIGHT(CONCAT('00', MONTH(@VoucherDate)), 2), ' no se encuentra abierto'),
						@IdJournalVoucherResult = 0
				RETURN
			END

			--Valido que no se haya realizado estimaciones de costos
			IF EXISTS (SELECT 1 FROM Cost.CostEstimationNative WITH (NOLOCK) WHERE Year = YEAR(@VoucherDate) AND Month = MONTH(@VoucherDate))
			BEGIN
				SELECT	@CodeMessage = 999,
						@Message = CONCAT('El comprobante contable no se puede crear ya que existe una estimación de costos en el periodo ', YEAR(@VoucherDate), '-', RIGHT(CONCAT('00', MONTH(@VoucherDate)), 2)),
						@IdJournalVoucherResult = 0
				RETURN
			END

			--Valido que no se pueda hacer un documento con fecha mayor al sistema asi el mes este abierto
			IF CAST(@VoucherDate AS DATE) > CAST([Common].[GETDATE]() AS DATE) 
			BEGIN
				SELECT	@CodeMessage = 999,
						@Message = 'El comprobante contable no se puede crear con fechas superiores a la del sistema',
						@IdJournalVoucherResult = 0
				RETURN
			END

			---Valido que el tipo de comprobante tenga una secuencia numerica para la vigencia y el libro que esta recorriendo
			IF EXISTS
			(
				SELECT 1 
				FROM @TableBookMovement tbm
				LEFT JOIN GeneralLedger.JournalVoucherTypeConsecutive jvtc WITH (NOLOCK) ON tbm.LegalBookId = jvtc.LegalBookId AND @IdJournalVoucherType = jvtc.JournalVoucherTypeId AND jvtc.Year = Year(@VoucherDate)
				WHERE jvtc.Id IS NULL
			) 
			BEGIN
				SELECT	@CodeMessage = 999, 
						@Message = CONCAT('El comprobante contable no se puede crear ya que no existe una secuencia numerica con el libro ', lb.Code,  ' - ', lb.Name, ' para el tipo de comprobante ', jvt.Code, ' - ', jvt.Name, ' en la vigencia ', YEAR(@VoucherDate)),
						@IdJournalVoucherResult = 0
				FROM @TableBookMovement tbm
				LEFT JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON tbm.LegalBookId = lb.Id
				LEFT JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON @IdJournalVoucherType = jvt.Id
				LEFT JOIN GeneralLedger.JournalVoucherTypeConsecutive jvtc WITH (NOLOCK) ON tbm.LegalBookId = jvtc.LegalBookId AND @IdJournalVoucherType = jvtc.JournalVoucherTypeId AND jvtc.Year = Year(@VoucherDate)
				WHERE jvtc.Id IS NULL
				RETURN
			END

			/****************************************************** ******************************************************/

			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @TableDetail
				SELECT	Id,
						IdMainAccount,
						IdThirdParty,
						IdCostCenter,
						DebitValue,
						CreditValue,
						Detail,
						IdRetention,
						RetentionRate,
						BaseValue,
						BillingValue,
						0 AS IsDelete
				FROM GeneralLedger.JournalVoucherDetails
				WHERE IdAccounting = @JournalVoucherId

			
			IF @Status = 2
			BEGIN -- Solo se cargan todos los detalles previamente guardados al confirmar
				--Se obtiene los detalles previamente insertados que no han sido modificados
				INSERT INTO @TableDetail
					SELECT	jvd.Id,
							jvd.IdMainAccount,
							jvd.IdThirdParty,
							jvd.IdCostCenter,
							jvd.DebitValue,
							jvd.CreditValue,
							jvd.Detail,
							jvd.IdRetention,
							jvd.RetentionRate,
							jvd.BaseValue,
							jvd.BillingValue,
							0
					FROM GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK)
					LEFT JOIN @TableDetail d ON jvd.Id = d.Id
					WHERE jvd.IdAccounting = @IdJournalVoucher AND ISNULL(d.Id, 0) = 0

				-- Valido que existan detalles
				IF NOT EXISTS (SELECT 1 FROM @TableDetail)
				BEGIN
					SELECT	@CodeMessage = 999, 
							@Message = 'El comprobante contable no se puede crear ya que no tiene detalles.',
							@IdJournalVoucherResult = 0
					RETURN
				END
			END

			-- Si la cuenta no maneja tercero entonces se quitan los terceros o
			-- Si la cuenta no maneja centro de costo entonces se quitan
			UPDATE td
				SET td.IdThirdParty = IIF(ma.HandlesThirdParty = 0, NULL, td.IdThirdParty),
					td.IdCostCenter = IIF(ma.HandlesCostCenter = 0, NULL, td.IdCostCenter)
			FROM @TableDetail td 
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
			WHERE ma.HandlesThirdParty = 0 OR ma.HandlesCostCenter = 0

			/***************************************** VALIDACIONES DETALLES *****************************************/

			--Valido que no vengan movimientos en negativo
			IF EXISTS (SELECT 1 FROM @TableDetail WHERE DebitValue < 0 OR CreditValue < 0)
			BEGIN
				SELECT	@CodeMessage = 999, 
						@Message = 'El comprobante contable no se puede crear ya que existen movimientos con valores negativos.',
						@IdJournalVoucherResult = 0
				RETURN
			END

			--Se valida que los detalles tengan diligenciado sólo uno de los valores debito o credito (no puede existir detalles debito y credito a la vez)
			IF EXISTS (SELECT 1 FROM @TableDetail WHERE DebitValue > 0 AND CreditValue > 0)
			BEGIN
				SELECT	@CodeMessage = 999, 
						@Message = 'El comprobante contable no se puede crear ya que existen cuentas contables del detalle que son débito y crédito a la vez.',
						@IdJournalVoucherResult = 0
				RETURN
			END

			-- Valido que las cuentas permitan movimientos
			IF EXISTS 
			(
				SELECT 1 
				FROM @TableDetail td 
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
				WHERE ma.AllowsMovement = 0
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ma.Number, ' - ', ma.Name)
						FROM @TableDetail td 
						JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
						WHERE ma.AllowsMovement = 0
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeMessage = 999, 
						@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contable del detalle que no permiten manejar movimientos: ', ISNULL(@Message, '')),
						@IdJournalVoucherResult = 0
				RETURN
			END

			-- Valido que las cuentas esten activas
			IF EXISTS 
			(
				SELECT 1 
				FROM @TableDetail td 
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
				WHERE ma.Status = 0
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ma.Number, ' - ', ma.Name)
						FROM @TableDetail td 
						JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
						WHERE ma.Status = 0
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeMessage = 999, 
						@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contable del detalle que estan inactivas: ', ISNULL(@Message, '')),
						@IdJournalVoucherResult = 0
				RETURN
			END

			-- Valido que las cuentas que manejan retención tengan un concepto de retención
			IF @IsClosedYear = 0 and EXISTS 
			(
				SELECT 1 
				FROM @TableDetail td 
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
				WHERE ma.RetencionType > 0 
					AND (td.IdRetention IS NULL OR td.RetentionRate IS NULL OR td.BaseValue IS NULL) 
					AND @EntityName NOT IN ('VoucherTransaction', 'TreasuryNote', 'PortfolioTransfer', 'PayrollLiquidation')
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ma.Number, ' - ', ma.Name)
						FROM @TableDetail td 
						JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
						WHERE ma.RetencionType > 0 
							AND (td.IdRetention IS NULL OR td.RetentionRate IS NULL OR td.BaseValue IS NULL)
							AND @EntityName NOT IN ('VoucherTransaction', 'TreasuryNote', 'PortfolioTransfer', 'PayrollLiquidation')
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeMessage = 999, 
						@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contables del detalle que manejan retención y no tienen información de la retención: ', ISNULL(@Message, '')),
						@IdJournalVoucherResult = 0
				RETURN
			END

			-- Valido que las cuentas que manejen tercero tengan tercero
			IF EXISTS 
			(
				SELECT 1 
				FROM @TableDetail td 
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
				WHERE ma.HandlesThirdParty = 1 AND td.IdThirdParty IS NULL
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ma.Number, ' - ', ma.Name)
						FROM @TableDetail td 
						JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
						WHERE ma.HandlesThirdParty = 1 AND td.IdThirdParty IS NULL
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeMessage = 999, 
						@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contable del detalle que manejan tercero pero el tercero esta vacio: ', ISNULL(@Message, '')),
						@IdJournalVoucherResult = 0
				RETURN
			END

			-- Valido que los tercero del detalle esten activos
			IF EXISTS 
			(
				SELECT 1 
				FROM @TableDetail td 
				JOIN Common.ThirdParty tp WITH (NOLOCK) ON td.IdThirdParty = tp.Id 
				WHERE tp.State = 0
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', tp.Nit, ' - ', tp.Name)
						FROM @TableDetail td 
						JOIN Common.ThirdParty tp WITH (NOLOCK) ON td.IdThirdParty = tp.Id 
						WHERE tp.State = 0
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeMessage = 999, 
						@Message = CONCAT('El comprobante contable no se puede crear ya que existen terceros del detalle en estado inactivo: ', ISNULL(@Message, '')),
						@IdJournalVoucherResult = 0
				RETURN
			END

			-- Valido que las cuentas que manejen centro de costo tengan centro de costo
			IF EXISTS 
			(
				SELECT 1 
				FROM @TableDetail td 
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
				WHERE ma.HandlesCostCenter = 1 AND td.IdCostCenter IS NULL
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ma.Number, ' - ', ma.Name)
						FROM @TableDetail td 
						JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
						WHERE ma.HandlesCostCenter = 1 AND td.IdCostCenter IS NULL
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeMessage = 999, 
						@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contable del detalle que manejan centro de costo pero el centro de costo esta vacio: ', ISNULL(@Message, '')),
						@IdJournalVoucherResult = 0
				RETURN
			END

			-- Valido que los centros de costos del detalle esten activos
			IF EXISTS 
			(
				SELECT 1 
				FROM @TableDetail td 
				JOIN Payroll.CostCenter cc WITH (NOLOCK) ON td.IdCostCenter = cc.Id 
				WHERE cc.State = 0
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', cc.Code, ' - ', cc.Name)
						FROM @TableDetail td 
						JOIN Payroll.CostCenter cc WITH (NOLOCK) ON td.IdCostCenter = cc.Id 
						WHERE cc.State = 0
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeMessage = 999, 
						@Message = CONCAT('El comprobante contable no se puede crear ya que existen centros de costos del detalle en estado inactivo: ', ISNULL(@Message, '')),
						@IdJournalVoucherResult = 0
				RETURN
			END

			/****************************************************** ******************************************************/

			-- Tabla movimiento con las respectivas cuentas de libros
			INSERT INTO #TableDetailHomologation
				SELECT	ISNULL(ha.LegalBookId, @LegalBookId) LegalBookId,
						td.RowId,
						-----------------------------------
						IIF(ISNULL(ha.LegalBookId, @LegalBookId) = @LegalBookId, td.Id, 0) Id,
						ISNULL(ha.MainAccountId, td.IdMainAccount) IdMainAccount,
						td.IdThirdParty,
						td.IdCostCenter,
						td.DebitValue,
						td.CreditValue,
						td.Detail,
						td.IdRetention,
						td.RetentionRate,
						td.BaseValue,
						td.BillingValue
				FROM @TableDetail td
				LEFT JOIN
				(
						--Cuentas Oficiales
						SELECT DISTINCT tm.LegalBookId, ma.Id OfficialMainAccountId, ma.Id MainAccountId
						FROM @TableDetail td
						JOIN @TableBookMovement tm ON @OfficialLegalBookId = tm.LegalBookId
						JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id AND tm.LegalBookId = ma.LegalBookId
					UNION ALL
						--Cuentas homologas
						SELECT DISTINCT
							tm.LegalBookId, ha.OfficialMainAccountId, ha.MainAccountId
						FROM @TableDetail td
						JOIN @TableBookMovement tm ON @OfficialLegalBookId <> tm.LegalBookId
						JOIN GeneralLedger.HomologationAccount ha WITH (NOLOCK) ON td.IdMainAccount = ha.OfficialMainAccountId
						JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ha.OfficialMainAccountId = ma.Id AND @OfficialLegalBookId = ma.LegalBookId
						JOIN GeneralLedger.MainAccounts mah WITH (NOLOCK) ON tm.LegalBookId = mah.LegalBookId AND ha.MainAccountId = mah.Id
				) ha ON @IsHomologation = 0 AND td.IdMainAccount = ha.OfficialMainAccountId

			/*********************************** VALIDACIONES DETALLES DEFINITIVOS ***********************************/

			-- Valido que todos los detalles pertenezcan al comprobante contable
			IF EXISTS 
			(
				SELECT 1 
				FROM @TableBookMovement tbm
				JOIN #TableDetailHomologation tdh ON tbm.LegalBookId = tdh.LegalBookId
				LEFT JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON tdh.Id = jvd.Id
				WHERE tdh.Id <> 0 AND tbm.JournalVoucherId <> ISNULL(jvd.IdAccounting, 0)
			)
			BEGIN
				SELECT	@CodeMessage = 999, 
						@Message = 'El comprobante contable no se puede crear ya que existen detalles que no pertenecen al comprobante contable',
						@IdJournalVoucherResult = 0
				RETURN
			END

			-- Valido que todos los detalles pertenezcan al correspondiente libro contable
			IF EXISTS 
			(
				SELECT 1 
				FROM #TableDetailHomologation tdh
				LEFT JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON tdh.IdMainAccount = ma.Id
				WHERE tdh.LegalBookId <> ISNULL(ma.LegalBookId, 0)
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Libro: ', lb.Code, ' - ', lb.Name, ' - Cuenta Contable: ', ma.Number, ' - ', ma.Name)
						FROM #TableDetailHomologation tdh
						LEFT JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON tdh.LegalBookId = lb.Id
						LEFT JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON tdh.IdMainAccount = ma.Id
						WHERE tdh.LegalBookId <> ISNULL(ma.LegalBookId, 0)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeMessage = 999, 
						@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contable que no pertenecen al libro contable: ', ISNULL(@Message, '')),
						@IdJournalVoucherResult = 0
				RETURN
			END

			-- Valido que todos los detalles hayan sido homologados
			IF EXISTS 
			(
				SELECT 1 
				FROM 
				(
					SELECT tbm.LegalBookId, td.RowId
					FROM @TableBookMovement tbm, @TableDetail td
				) tbm
				LEFT JOIN #TableDetailHomologation tdh ON tbm.LegalBookId = tdh.LegalBookId AND tbm.RowId = tdh.TableDetailRowId
				WHERE tdh.TableDetailRowId IS NULL
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Libro: ', lb.Code, ' - ', lb.Name, ' - Cuenta Contable: ', ma.Number, ' - ', ma.Name)
						FROM 
						(
							SELECT tbm.LegalBookId, td.RowId, td.IdMainAccount
							FROM @TableBookMovement tbm, @TableDetail td
						) tbm
						LEFT JOIN #TableDetailHomologation tdh ON tbm.LegalBookId = tdh.LegalBookId AND tbm.RowId = tdh.TableDetailRowId
						LEFT JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON tbm.LegalBookId = lb.Id
						LEFT JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON tbm.IdMainAccount = ma.Id
						WHERE tdh.TableDetailRowId IS NULL
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeMessage = 999, 
						@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contable del detalle que no han sido homologadas: ', ISNULL(@Message, '')),
						@IdJournalVoucherResult = 0
				RETURN
			END

			-- Valido que todos los detalles hayan sido homologados solo una vez
			IF EXISTS 
			(
				SELECT 1 
				FROM #TableDetailHomologation tdh
				GROUP BY tdh.LegalBookId, tdh.TableDetailRowId
				HAVING COUNT(1) > 1
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Libro: ', lb.Code, ' - ', lb.Name, ' - Cuenta Contable: ', ma.Number, ' - ', ma.Name)
						FROM 
						(
							SELECT tdh.LegalBookId, tdh.TableDetailRowId
							FROM #TableDetailHomologation tdh
							GROUP BY tdh.LegalBookId, tdh.TableDetailRowId
							HAVING COUNT(1) > 1
						) tbm
						LEFT JOIN @TableDetail td ON tbm.TableDetailRowId = td.RowId
						LEFT JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON tbm.LegalBookId = lb.Id
						LEFT JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeMessage = 999, 
						@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contable del detalle con más de un homologo: ', ISNULL(@Message, '')),
						@IdJournalVoucherResult = 0
				RETURN
			END

			--Validaciones al confirmar
			IF @Status = 2
			BEGIN
				-- Valido que los debitos sean igual que los creditos en los detalles
				IF EXISTS 
				(
					SELECT 1 
					FROM 
					(
						SELECT SUM(DebitValue) DebitValue, SUM(CreditValue) CreditValue 
						FROM @TableDetail
					) d
					WHERE d.DebitValue <> d.CreditValue
				)
				BEGIN
					SELECT	@CodeMessage = 999, 
							@Message = CONCAT('El comprobante contable no se puede crear ya que se encuentra desbalanceado. Debitos: ', FORMAT(SUM(DebitValue), 'C2', 'es-CO'), ' - Creditos:', FORMAT(SUM(CreditValue), 'C2', 'es-CO')),
							@IdJournalVoucherResult = 0
					FROM @TableDetail
					RETURN
				END

				-- Valido que los debitos sean igual que los creditos en los detalles homologados
				IF EXISTS 
				(
					SELECT 1 
					FROM #TableDetailHomologation
					GROUP BY LegalBookId
					HAVING SUM(DebitValue) <> SUM(CreditValue)
				)
				BEGIN
					SELECT	@CodeMessage = 999, 
							@Message = CONCAT('El comprobante contable no se puede crear ya que los detalles luego de homologarse el libro ', lb.Code, ' - ', lb.Name, ' se encuentra desbalanceado. Debitos: ', FORMAT(SUM(DebitValue), 'C2', 'es-CO'), ' - Creditos:', FORMAT(SUM(CreditValue), 'C2', 'es-CO')),
							@IdJournalVoucherResult = 0
					FROM #TableDetailHomologation tdh
					LEFT JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON tdh.LegalBookId = lb.Id
					GROUP BY LegalBookId, lb.Code, lb.Name
					HAVING SUM(DebitValue) <> SUM(CreditValue)
					RETURN
				END
			END

			/************************************************ PROCESO ************************************************/

			--Si apenas se esta creando el comprobante se inserta el registro del movimiento
			IF @AccountingMovementId = 0 
			BEGIN
				INSERT INTO GeneralLedger.AccountingMovement
				(
					LegalBookId, JournalVoucherTypeId, VoucherDate, Detail, EntityCode, EntityId, EntityName, JournalVoucherXml, CreationUser, CreationDate
				)
				SELECT @LegalBookId, @IdJournalVoucherType, @VoucherDate, SUBSTRING(@Detail,1,500), @EntityCode, @EntityId, @EntityName, '', @CodeUser, [Common].[GETDATE]()

				SET @AccountingMovementId = SCOPE_IDENTITY()
			END

			/******************************************** CONTABILIZACION ********************************************/
			
			SET @Rows = 1
			SET @RowId = 0

			WHILE @Rows > 0
			BEGIN
				SELECT TOP 1 
					@RowId = RowId, 
					@MovementLegalBookId = LegalBookId, 
					@MovementJournalVoucherId = JournalVoucherId 
				FROM @TableBookMovement 
				WHERE RowId > @RowId 
				ORDER BY RowId

				SET @Rows = @@ROWCOUNT
				IF @Rows = 0 
					BREAK

				-------------------------------------------------------------------------------------------------------

				--Si apenas se esta guardando, y no es el libro enviado en la cabecera, no realizamos nada
				--Esto a fin de no eliminar los detalles de los documentos homologos, dado que, como se envia por lotes puede generar inconvenientes
				IF @Status = 1 AND @MovementLegalBookId <> @LegalBookId
				BEGIN
					CONTINUE
				END

				--Si estan confirmando el comprobante
				IF @Status = 2 
				BEGIN
					SET @ConfirmationUser = @CodeUser
					SET @ConfirmationDate = [Common].[GETDATE]()
				END
				
				IF @MovementJournalVoucherId = 0 
				BEGIN -- Si el comprobante contable es nuevo
					UPDATE GeneralLedger.JournalVoucherTypeConsecutive 
						SET @Consecutive = Consecutive = Consecutive + 1
					WHERE JournalVoucherTypeId = @IdJournalVoucherType AND LegalBookId = @MovementLegalBookId AND [Year] = Year(@VoucherDate)

					-- Valido que no exista ya un comprobante con el consecutivo para el tipo de comprobante, libro y año
					IF EXISTS 
					(
						SELECT 1 
						FROM GeneralLedger.JournalVouchers jv WITH (NOLOCK)
						WHERE jv.Consecutive = (@Consecutive - 1) 
							AND jv.LegalBookId = @MovementLegalBookId 
							AND jv.IdJournalVoucher = @IdJournalVoucherType
							AND jv.YearMovement = YEAR(@VoucherDate)
					)
					BEGIN
						SELECT	@CodeMessage = 999, 
								@Message = CONCAT('Ya existe un comprobante contable con el consecutivo ', (@Consecutive - 1), 
									' del tipo de comprobante ', (SELECT CONCAT(Code, ' - ', Name) FROM GeneralLedger.JournalVoucherTypes WITH (NOLOCK) WHERE Id = @IdJournalVoucherType), 
									' del año ', YEAR(@VoucherDate),
									' para el libro ',  (SELECT CONCAT(Code, ' - ', Name) FROM GeneralLedger.LegalBook WITH (NOLOCK) WHERE Id = @MovementLegalBookId)),
								@IdJournalVoucherResult = 0
						RETURN
					END

					INSERT INTO [GeneralLedger].[JournalVouchers] 
					(
						[Consecutive],[AccountingMovementId],[LegalBookId],[IdJournalVoucher],[VoucherDate],[YearMovement],[Status],[Imported],
						[Detail],[EntityCode],[EntityId],[EntityName],[IsClosedYear],[CreationUser],[CreationDate],[ConfirmationUser],[ConfirmationDate]
					)
					SELECT	@Consecutive - 1, @AccountingMovementId, @MovementLegalBookId, @IdJournalVoucherType, @VoucherDate, YEAR(@VoucherDate), @Status, @Imported, 
							@Detail, @EntityCode, @EntityId, @EntityName, @IsClosedYear, @CodeUser, [Common].[GETDATE](), @ConfirmationUser, @ConfirmationDate
					
					SET @MovementJournalVoucherId = SCOPE_IDENTITY()
				
					UPDATE @TableBookMovement 
						SET JournalVoucherId = @MovementJournalVoucherId 
					WHERE LegalBookId = @MovementLegalBookId
				END				
				ELSE
				BEGIN -- Si se esta modificando
					UPDATE [GeneralLedger].[JournalVouchers] 
						SET VoucherDate = @VoucherDate, 
							Detail = @Detail, 
							Status = IIF(@Status = 4, 1, @Status),
							ModificationUser = @CodeUser, 
							ModificationDate = [Common].[GETDATE](),
							ConfirmationUser = @ConfirmationUser,
							ConfirmationDate = @ConfirmationDate
					WHERE Id = @MovementJournalVoucherId
				END

				IF @Status <> 4 
				BEGIN
					IF (@MovementLegalBookId <> @OfficialLegalBookId AND EXISTS (SELECT 1 FROM GeneralLedger.JournalVouchers WITH (NOLOCK) WHERE AccountingMovementId = @AccountingMovementId GROUP BY AccountingMovementId HAVING COUNT(1) > 1))
					BEGIN
						DELETE GeneralLedger.JournalVoucherDetails WHERE IdAccounting = @MovementJournalVoucherId
					END

					--Se actualiza los detalles del comprobante
					UPDATE jvd
						SET jvd.IdMainAccount = tdh.IdMainAccount,
							jvd.IdThirdParty = tdh.IdThirdParty,
							jvd.IdCostCenter = tdh.IdCostCenter,
							jvd.DebitValue = tdh.DebitValue,
							jvd.CreditValue = tdh.CreditValue,
							jvd.Detail = tdh.Detail,
							jvd.IdRetention = tdh.IdRetention,
							jvd.RetentionRate = tdh.RetentionRate,
							jvd.BaseValue = tdh.BaseValue,
							jvd.BillingValue = tdh.BillingValue
					FROM #TableDetailHomologation tdh
					JOIN GeneralLedger.JournalVoucherDetails jvd ON tdh.Id = jvd.Id
					WHERE tdh.LegalBookId = @MovementLegalBookId

					--Se inserta los nuevos detalles del comprobante
					INSERT INTO GeneralLedger.JournalVoucherDetails
					(
						IdAccounting, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue
					)
					SELECT @MovementJournalVoucherId, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue
					FROM #TableDetailHomologation tdh
					WHERE tdh.LegalBookId = @MovementLegalBookId AND tdh.Id = 0
				END

				-------------------------------------------------------------------------------------------------------

				--Si se esta confirmando o reversando
				IF @Status IN (2, 4)
				BEGIN
					print 'entra 3'
					IF @Status = 2
					BEGIN
						-- Valido que los debitos sean igual que los creditos en los detalles
						IF EXISTS 
						(
							SELECT 1 
							FROM GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK)
							WHERE jvd.IdAccounting = @MovementJournalVoucherId
							GROUP BY jvd.IdAccounting
							HAVING SUM(DebitValue) <> SUM(CreditValue)
						)
						BEGIN
							SELECT	@CodeMessage = 999, 
									@Message = CONCAT('El comprobante contable no se puede crear ya que se encuentra desbalanceado. Debitos: ', FORMAT(SUM(DebitValue), 'C2', 'es-CO'), ' - Creditos:', FORMAT(SUM(CreditValue), 'C2', 'es-CO')),
									@IdJournalVoucherResult = 0
							FROM GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK)
							WHERE jvd.IdAccounting = @MovementJournalVoucherId
							GROUP BY jvd.IdAccounting
							RETURN
						END
					END

					--Periodo del movimiento
					IF @IsClosedYear = 1
					BEGIN
						SET @MonthMovement  = 14
					END
					ELSE IF @IsClosedYear = 2
					BEGIN
						SET @MonthMovement  = 13
					END
					ELSE
					BEGIN
						SET @MonthMovement = MONTH(@VoucherDate)	
					END

					UPDATE glb
						SET glb.DebitValue = glb.DebitValue + jvd.DebitValue,
							glb.CreditValue = glb.CreditValue + jvd.CreditValue
					FROM 
					(
						SELECT	YEAR(@VoucherDate) Year, @MonthMovement Month,
								jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter,
								SUM(jvd.DebitValue * IIF(@Status = 2, 1, -1)) DebitValue, 
								SUM(jvd.CreditValue * IIF(@Status = 2, 1, -1)) CreditValue
						FROM GeneralLedger.JournalVoucherDetails jvd
						WHERE jvd.IdAccounting = @MovementJournalVoucherId
						GROUP BY jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter
					) jvd
					JOIN GeneralLedger.GeneralLedgerBalance glb ON jvd.Year = glb.Year AND jvd.Month = glb.Month
						AND jvd.IdMainAccount = glb.IdMainAccount AND ISNULL(jvd.IdThirdParty, 0) =ISNULL(glb.IdThirdParty, 0) AND ISNULL(jvd.IdCostCenter, 0) = ISNULL(glb.IdCostCenter, 0)

					INSERT INTO GeneralLedger.GeneralLedgerBalance
					(
						Year, Month, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue
					)
					SELECT	jvd.Year, jvd.Month, jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter, jvd.DebitValue, jvd.CreditValue
					FROM 
					(
						SELECT	YEAR(@VoucherDate) Year, @MonthMovement Month,
								jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter,
								SUM(jvd.DebitValue * IIF(@Status = 2, 1, -1)) DebitValue, 
								SUM(jvd.CreditValue * IIF(@Status = 2, 1, -1)) CreditValue
						FROM GeneralLedger.JournalVoucherDetails jvd
						WHERE jvd.IdAccounting = @MovementJournalVoucherId
						GROUP BY jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter
					) jvd
					LEFT JOIN GeneralLedger.GeneralLedgerBalance glb ON jvd.Year = glb.Year AND jvd.Month = glb.Month
						AND jvd.IdMainAccount = glb.IdMainAccount AND ISNULL(jvd.IdThirdParty, 0) =ISNULL(glb.IdThirdParty, 0) AND ISNULL(jvd.IdCostCenter, 0) = ISNULL(glb.IdCostCenter, 0)
					WHERE glb.Id IS NULL
				END
				print '45 -'
				print @MovementJournalVoucherId
				-------------------------------------------------------------------------------------------------------

				SELECT @Message_Output = CONCAT(ISNULL(@Message_Output + CHAR(13) + CHAR(10), ''), CASE @Status
							WHEN 2 THEN 'Se guardó y confirmó el Comprobante Contable '
							WHEN 3 THEN 'Se anuló el Comprobante Contable '
							WHEN 4 THEN 'Se desconfirmó el Comprobante Contable '
							ELSE 'Se guardó el Comprobante Contable '
						END, jv.Consecutive, ' de tipo ', jvt.Name, ' del libro contable ', lb.Name)
				FROM GeneralLedger.LegalBook lb WITH (NOLOCK)
				JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON lb.Id = jv.LegalBookId
				JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON jv.IdJournalVoucher = jvt.Id
				WHERE jv.Id = @MovementJournalVoucherId
			END
		END

		/****************************************************** ******************************************************/
		print @Message_Output
		print @Message
		SELECT	@CodeMessage = 0, 
				@Message = ISNULL(@Message_Output, CASE @Status
					WHEN 2 THEN 'Se guardó y confirmó el Comprobante Contable'
					WHEN 3 THEN 'Se anuló el Comprobante Contable'
					WHEN 4 THEN 'Se desconfirmó el Comprobante Contable'
					ELSE 'Se guardó el Comprobante Contable '
				END),
				@IdJournalVoucherResult = ISNULL((SELECT TOP 1 JournalVoucherId FROM @TableBookMovement ORDER BY IIF(LegalBookId = @LegalBookId, 0, LegalBookId)), @IdJournalVoucher)
	END TRY
	BEGIN CATCH
		SELECT	@CodeMessage = 999, 
				@Message = CONCAT('Se presentó un error al crear el comprobante contable: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE()),
				@IdJournalVoucherResult = 0
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que confirma (contabiliza) un comprobante contable identificado por su ID, actualizando su estado en el libro mayor general. Valida que el comprobante no esté duplicado, que corresponda al movimiento contable correcto, que no existan documentos homólogos en estados inconsistentes y que el libro contable (oficial o alternativo) esté activo. Si el comprobante pertenece a un esquema de homologación entre libros contables, aplica las reglas de propagación al libro oficial. Devuelve mensajes de error o éxito junto con el ID del comprobante resultante, siendo el eje central del proceso de cierre y legalización de asientos contables en el libro mayor.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmJournalVoucherById';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmJournalVoucherById';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Si no existe libro oficial (OfficialBook=1, Status=1) no se permite procesar el comprobante; Un comprobante homólogo solo puede editarse desde el libro oficial; Todos los comprobantes homólogos del mismo AccountingMovementId deben mantener el mismo Status; No se admiten valores negativos ni filas con débito y crédito simultáneos en el detalle; Solo se aceptan cuentas activas, que permitan movimientos y, cuando aplique, con tercero/centro de costo activos; Al confirmar (@Status=2) la suma de débitos debe igualar la suma de créditos, tanto en el detalle original como por libro tras homologar; La fecha del comprobante no puede ser superior a la fecha actual del sistema; No se contabiliza en períodos cerrados (año<=LastYearClose o mes sin ClosedMonth.Status=1) salvo IsClosedYear>0; No se contabiliza si existe estimación de costos (Cost.CostEstimationNative) en el período del comprobante; Cada cuenta contable del detalle debe pertenecer al LegalBook correspondiente y haber sido homologada exactamente una vez por libro; No se permite desconfirmar comprobantes interfazados (EntityName distinto de '''' o ''JournalVouchers''); Si la cuenta no maneja tercero/centro de costo se anulan esos campos en el detalle antes de procesar; El consecutivo asignado a un nuevo JournalVoucher se obtiene incrementando JournalVoucherTypeConsecutive y debe ser único por libro, tipo y año', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmJournalVoucherById';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante contable (Journal Voucher); Libro contable oficial; Homologación de cuentas entre libros; Consecutivo por tipo de comprobante, libro y vigencia; Cierre anual y mensual contable; Estimación de costos; Balance del libro mayor (saldos por año/mes/cuenta/tercero/centro de costo); Tercero; Centro de costo; Retención (concepto, tasa y base); Documento interfazado; Configuración VieBot por formulario', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmJournalVoucherById';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Status = 3 (anulación) → Actualiza todos los comprobantes del movimiento contable marcándolos como anulados con usuario/fecha de anulación else Procesa flujo de guardado/confirmación/desconfirmación con validaciones, homologación a libros y afectación de balances; si @Status = 4 (desconfirmación) → Carga todos los libros del movimiento, exige que estén en estado Confirmado (=2) y rechaza si fueron interfazados (EntityName distinto de '''' o ''JournalVouchers'') else Si no es homologación exige estado Registrado (=1); si @IsHomologation = 1 (existe registro previo del mismo AccountingMovementId con Id = 0) → Solo afecta el libro y comprobante recibidos else Si EntityName es ''JournalVouchers'' o vacío usa los libros ya existentes del movimiento; en caso contrario consulta GeneralLedger.VieBot por el formulario para determinar libros (mapeando ''MedicalFeesLiquidation'' a ''AccountPayable''); si @AccountingMovementId = 0 → Crea un nuevo registro en GeneralLedger.AccountingMovement y usa SCOPE_IDENTITY como movimiento contable; si @MovementJournalVoucherId = 0 dentro del WHILE → Incrementa el consecutivo en JournalVoucherTypeConsecutive e inserta nuevo JournalVoucher; valida que el consecutivo no exista ya else Actualiza el JournalVoucher existente (fecha, detalle, estado, usuario/fecha de modificación y confirmación). Si @Status=4 vuelve el estado a 1; si @Status = 1 y el libro iterado no es el libro de la cabecera → Salta la iteración (CONTINUE) para no afectar detalles de documentos homólogos en guardado por lotes; si @Status IN (2,4) → Recalcula GeneralLedgerBalance: suma (status=2) o resta (status=4) DebitValue/CreditValue por año/mes/cuenta/tercero/centro de costo, e inserta filas faltantes; si @IsClosedYear = 1 → Asigna mes de movimiento = 14 para el balance else Si @IsClosedYear=2 mes=13; en otro caso mes = MONTH(@VoucherDate); si @MovementLegalBookId <> @OfficialLegalBookId y existen múltiples comprobantes del mismo movimiento → Borra los detalles existentes del comprobante homólogo antes de re-insertarlos', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmJournalVoucherById';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVouchers; GeneralLedger.LegalBook; GeneralLedger.JournalVoucherTypes; GeneralLedger.JournalVoucherTypeConsecutive; GeneralLedger.JournalVoucherDetails; GeneralLedger.MainAccounts; GeneralLedger.HomologationAccount; GeneralLedger.VieBot; GeneralLedger.ClosedMonth; GeneralLedger.GeneralLedgerBalance; Cost.CostEstimationNative; Common.ThirdParty; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmJournalVoucherById';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmJournalVoucherById';
-- GO
