
CREATE PROCEDURE [GeneralLedger].[SP_GeneralLedgerBalance]
	@Period AS INT,
	@LegalBookId AS INT,
	@MainAccountId AS INT,
	@ValidateMovement AS BIT,
	@Year AS INT
AS
BEGIN
	SET NOCOUNT ON;

	--- Estado del mensaje 1 - Correcto 2 - Advertencia 3 - Error 4 - Desbalanceado
	DECLARE @TableResult TABLE (
		CodeMessage VARCHAR(10), 
		Message VARCHAR(max), 
		[Status] TINYINT,
		Consecutive BIGINT,
		CodeNameJournalVoucherType VARCHAR(100), 
		ConfirmationDate DATETIME,
		Detail VARCHAR(500),
		DebitValue DECIMAL(21,5),
		CreditValue DECIMAL(21,5)
	)
	
	BEGIN TRY
		SET DATEFORMAT DMY
		DECLARE @initialDate AS DATE = CAST( DATEFROMPARTS(@Year,@Period,1) as date);
		DECLARE @endDate AS DATE = DATEADD(DAY, -1, (DATEADD(MONTH, 1, @initialDate)))

		DECLARE @accountNumber AS VARCHAR(30) = ''
		IF ISNULL(@MainAccountId, 0) > 0 BEGIN
			SELECT @accountNumber = Number FROM GeneralLedger.MainAccounts WHERE Id = @MainAccountId AND LegalBookId = @LegalBookId
		END

		--Valido que todos los documentos que se van a procesar esten confirmados
		IF (
			SELECT COUNT(*) 
			FROM GeneralLedger.JournalVouchers AS jv 
			INNER JOIN GeneralLedger.JournalVoucherDetails AS jvd ON jv.Id = jvd.IdAccounting 
			INNER JOIN GeneralLedger.MainAccounts ma on jvd.IdMainAccount = ma.id  
			WHERE ma.Number like @accountNumber + '%' AND ma.LegalBookId = @LegalBookId AND jv.Status = 1 AND jv.IsClosedYear = 0 AND CAST(jv.VoucherDate AS date) BETWEEN @initialDate AND @endDate
		) > 0 BEGIN
			
			INSERT INTO @TableResult (CodeMessage, Message, [Status])
				SELECT DISTINCT 
					'999', 'El comprobante contable '+ CAST(jv.Consecutive as varchar(30)) + ' con tipo de documento ' + jvt.Code +' - ' + jvt.Name + ' no esta confirmado', cast(3 as tinyint)
				FROM GeneralLedger.JournalVouchers AS jv 
				INNER JOIN GeneralLedger.JournalVoucherDetails AS jvd ON jv.Id = jvd.IdAccounting 
				INNER JOIN GeneralLedger.MainAccounts ma on jvd.IdMainAccount = ma.id
				INNER JOIN GeneralLedger.JournalVoucherTypes AS jvt ON jv.IdJournalVoucher = jvt.Id
				WHERE ma.Number like @accountNumber + '%' AND ma.LegalBookId = @LegalBookId AND jv.Status = 1 AND jv.IsClosedYear = 0 AND CAST(jv.VoucherDate AS date) BETWEEN @initialDate AND @endDate
				
			SELECT * FROM @TableResult 
			RETURN

		END

		IF (@ValidateMovement = 1) begin
			--Hacemos la validacion que los valores esten iguales
			DECLARE @DebitValue DECIMAL(21,5),
				@CreditValue DECIMAL(21,5)
			
			SELECT @DebitValue = ROUND(SUM(jvd.DebitValue),-1), @CreditValue = ROUND(SUM(jvd.CreditValue),-1)
			FROM
			( 
				SELECT jv.Id
				FROM GeneralLedger.JournalVouchers AS jv 
				INNER JOIN GeneralLedger.JournalVoucherDetails AS jvd ON jv.Id = jvd.IdAccounting
				INNER JOIN GeneralLedger.MainAccounts ma on jvd.IdMainAccount = ma.id			
				WHERE ma.LegalBookId = @LegalBookId 
					AND jv.Status = 2 
					AND jv.IsClosedYear = 0 
					AND CAST(jv.VoucherDate AS date) BETWEEN @initialDate AND @endDate 
					AND ma.Number like @accountNumber + '%'
				GROUP BY jv.Id
			) AS jv 
			INNER JOIN GeneralLedger.JournalVoucherDetails AS jvd ON jv.Id = jvd.IdAccounting

			--si los valores no son iguales retorno todos los documentos que tienen esa cuenta para luego ver cual esta desbalanceado
			if (@DebitValue <> @CreditValue ) BEGIN
				
				INSERT INTO @TableResult (CodeMessage, Message, [Status], Consecutive, CodeNameJournalVoucherType, ConfirmationDate, Detail, DebitValue, CreditValue)
					
					SELECT '999', '', cast(4 as tinyint), jv.Consecutive, jvt.Code + ' - ' + jvt.Name, jv.ConfirmationDate, CAST(jv.Detail AS VARCHAR(500)), SUM(JVD.DebitValue), SUM(jvd.CreditValue)
					FROM
					( 
						SELECT jv.Id, jv.Consecutive, jv.IdJournalVoucher, jv.ConfirmationDate, jv.Detail
						FROM GeneralLedger.JournalVouchers AS jv 
						INNER JOIN GeneralLedger.JournalVoucherDetails AS jvd ON jv.Id = jvd.IdAccounting
						INNER JOIN GeneralLedger.MainAccounts ma on jvd.IdMainAccount = ma.id			
						WHERE ma.LegalBookId = @LegalBookId 
							AND jv.Status = 2 
							AND jv.IsClosedYear = 0 
							AND CAST(jv.VoucherDate AS date) BETWEEN @initialDate AND @endDate 
							AND ma.Number like @accountNumber + '%'
						GROUP BY jv.Id, jv.Consecutive, jv.IdJournalVoucher, jv.ConfirmationDate, jv.Detail
					) AS jv 
					INNER JOIN GeneralLedger.JournalVoucherDetails AS jvd ON jv.Id = jvd.IdAccounting
					INNER JOIN GeneralLedger.JournalVoucherTypes AS jvt ON jv.IdJournalVoucher = jvt.Id
					GROUP BY jv.Id, jv.Consecutive, jvt.Code, jvt.Name, jv.ConfirmationDate, jv.Detail
					HAVING SUM(jvd.DebitValue) <> SUM(jvd.CreditValue)			

				SELECT * FROM @TableResult 
				RETURN
			END
		END

		--Se eliminan todos los items de la table de saldos que contengan la cuenta seleccionada
		DELETE glb
			FROM GeneralLedger.GeneralLedgerBalance glb
			INNER JOIN GeneralLedger.MainAccounts ma ON glb.IdMainAccount = ma.Id
			WHERE ma.Number like @accountNumber + '%' AND ma.LegalBookId = @LegalBookId AND glb.Month = @Period and glb.Year = @Year

		--Se inserta el consolidado de datos generados por los comprobantes contables realizados en el periodo
		INSERT INTO GeneralLedger.GeneralLedgerBalance 
				([Month], [Year], [IdMainAccount], [IdThirdParty], [IdCostCenter], [DebitValue], [CreditValue])
			SELECT MONTH(jv.VoucherDate), YEAR(jv.VoucherDate), jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter, SUM(DebitValue), SUM(CreditValue)
			FROM GeneralLedger.JournalVouchers AS jv 
			INNER JOIN GeneralLedger.JournalVoucherDetails AS jvd ON jv.Id = jvd.IdAccounting
			INNER JOIN GeneralLedger.MainAccounts ma on jvd.IdMainAccount = ma.id
			WHERE ma.Number like @accountNumber + '%' AND ma.LegalBookId = @LegalBookId AND jv.Status = 2 AND jv.IsClosedYear = 0 AND CAST(jv.VoucherDate AS date) BETWEEN @initialDate AND @endDate
			GROUP BY MONTH(jv.VoucherDate), YEAR(jv.VoucherDate), jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter

		INSERT INTO @TableResult (CodeMessage, Message, [Status])
			SELECT 0 AS CodeMessage, 'Proceso finalizado correctamente ' AS Message, cast(1 as tinyint) as [Status] 
		
		SELECT * FROM @TableResult 
		RETURN
	END TRY
	BEGIN CATCH
		INSERT INTO @TableResult (CodeMessage, Message, [Status])
			SELECT '999', ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(5)), cast(3 as tinyint)
		
		SELECT * FROM @TableResult
	END CATCH    
END