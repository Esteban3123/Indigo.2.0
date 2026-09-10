
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que calcula y actualiza el saldo del libro mayor contable para un período mensual, año, libro legal y cuenta contable específicos. Primero valida que todos los comprobantes contables (asientos) del período estén confirmados; si encuentra alguno pendiente de confirmación, retorna error indicando qué comprobante y tipo de documento está sin confirmar. Opcionalmente verifica que el total de débitos y créditos de los comprobantes confirmados esté balanceado; si detecta desbalance, retorna los comprobantes desbalanceados con sus valores de débito y crédito para corrección. Una vez superadas las validaciones, elimina los saldos previos del período en la tabla de saldos del mayor (GeneralLedgerBalance) y los recalcula e inserta consolidando los movimientos de débito y crédito de los comprobantes confirmados, agrupados por cuenta contable, tercero y centro de costo. Se usa en el cierre contable mensual o reproceso de saldos contables del libro mayor.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_GeneralLedgerBalance';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_GeneralLedgerBalance';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recalcula y consolida los saldos mensuales del libro mayor por cuenta, tercero y centro de costo, validando previamente la confirmación y el balance de los comprobantes del período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_GeneralLedgerBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El año y período recibidos deben permitir construir una fecha válida (DATEFROMPARTS).; Si se indica una cuenta principal, debe existir en MainAccounts asociada al libro legal indicado para obtener su número.; Para recalcular saldos, los comprobantes del período deben estar confirmados (Status=2) y no pertenecer a un año cerrado (IsClosedYear=0).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_GeneralLedgerBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consolidan saldos a partir de comprobantes con Status=2 (confirmados) e IsClosedYear=0.; Nunca se recalculan saldos si existe al menos un comprobante del período sin confirmar (Status=1).; Si @ValidateMovement=1 y hay desbalance débito/crédito, no se modifica GeneralLedgerBalance.; El recálculo siempre borra primero los saldos previos del mismo período, año, libro y prefijo de cuenta antes de insertar.; El período de fechas se delimita siempre desde el primer al último día del mes (@Year,@Period).; El filtro de cuentas se aplica por prefijo (LIKE Number+''%''), permitiendo procesar jerarquías completas.; La validación de balance se hace con SUM redondeado a la decena (ROUND(...,-1)).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_GeneralLedgerBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Libro mayor; Comprobante contable (Journal Voucher); Tipo de comprobante contable; Cuenta contable principal; Libro legal; Tercero; Centro de costo; Débito y crédito; Saldo contable por período; Cierre contable / año cerrado; Confirmación de comprobante; Balance débito-crédito', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_GeneralLedgerBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableResult: Cuando existen comprobantes con Status=1 (no confirmados) en el período y libro, se inserta un mensaje de error (Status=3, código 999) por cada comprobante indicando consecutivo y tipo de documento, y se retorna sin recalcular saldos.; [INSERT] @TableResult: Cuando @ValidateMovement=1 y la suma redondeada de débitos difiere de la de créditos sobre comprobantes confirmados del período, se insertan los comprobantes cuyo SUM(Debit)<>SUM(Credit) con Status=4 (desbalanceado) y se retorna sin recalcular saldos.; [DELETE] GeneralLedger.GeneralLedgerBalance: Antes de recalcular, elimina todos los saldos cuyo MainAccount.Number empiece por el número de la cuenta seleccionada, en el mismo LegalBookId, Month=@Period y Year=@Year.; [INSERT] GeneralLedger.GeneralLedgerBalance: Inserta el consolidado de SUM(DebitValue) y SUM(CreditValue) agrupado por mes, año, cuenta principal, tercero y centro de costo, tomando solo comprobantes con Status=2 e IsClosedYear=0 cuya fecha esté entre el primer y último día del período y cuya cuenta empiece por el número filtrado.; [INSERT] @TableResult: Al finalizar correctamente, inserta un mensaje ''Proceso finalizado correctamente'' con Status=1.; [INSERT] @TableResult: En el bloque CATCH, inserta un mensaje con código 999, Status=3 y el ERROR_MESSAGE() junto con la línea del error.; [RETURN_RESULT] @TableResult: En todas las rutas (no confirmados, desbalance, éxito y error) devuelve el contenido de @TableResult al cliente.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_GeneralLedgerBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@MainAccountId,0) > 0 → Obtiene el Number de la cuenta principal para filtrar por prefijo; si es 0/NULL, el filtro queda como ''%'' (todas las cuentas del libro). else Procesa todas las cuentas del libro legal (prefijo vacío).; si Existen comprobantes con Status=1 (no confirmados) en el período/cuenta/libro → Reporta error por cada comprobante no confirmado y aborta el proceso. else Continúa con validación de balance y recálculo.; si @ValidateMovement = 1 → Calcula SUM redondeado de débitos y créditos sobre comprobantes confirmados; si difieren, reporta los desbalanceados y aborta. else Omite la validación de balance y procede al recálculo.; si @DebitValue <> @CreditValue (tras redondeo a la decena) → Inserta en el resultado los comprobantes cuyo SUM(Debit)<>SUM(Credit) con Status=4 y retorna. else Continúa al recálculo de saldos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_GeneralLedgerBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.MainAccounts; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; GeneralLedger.JournalVoucherTypes; GeneralLedger.GeneralLedgerBalance', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_GeneralLedgerBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_GeneralLedgerBalance';
-- GO
