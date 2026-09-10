-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-04-23
-- Description:	Reversa una consignacion
-- =============================================
CREATE PROCEDURE [Treasury].[SP_ReverseConsignment]
	@TreasuryNoteId INT,	
	@CodeUser VARCHAR(20)	
AS
BEGIN
	SET NOCOUNT ON;

	/************************************* VARIABLES ************************************/
	
	DECLARE @TreasuryNoteCode VARCHAR(20),
			@ConsignmentId INT

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
		IsClosedYear TINYINT DEFAULT(0),
		CurrencyId INT
	)

	--Se declara una tabla temporal para los detalles del comprobante
	DECLARE @JournalVourcherDetailTmp TABLE 
	(
		Id INT DEFAULT(0),
		IdAccounting INT DEFAULT(0),
		IdMainAccount INT,
		IdThirdParty INT,
		IdCostCenter INT,
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
			--------------------------------------
			@JournalVoucherXML XML,
			@CodeMessage INT,
			@Message VARCHAR(MAX),
			@IdJournalVoucherResult INT,
			--------------------------------------
			@Consecutive VARCHAR(30),
			--------Moneda de la nota----
			@TreasuryNoteCurrencyId INT

	--tabla temporal para almacenar el resultado deL movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	BEGIN TRY

		SELECT 
			@TreasuryNoteCode = tn.Code,
			@ConsignmentId = tn.ConsignmentId,
			@JournalVoucherTypeId = st.JournalVoucherTypeTreasuryNotes,
			@TreasuryNoteCurrencyId = tn.CurrencyId
		FROM Treasury.TreasuryNote tn
		LEFT JOIN Treasury.SettingsTreasury st ON tn.OperatingUnitId = st.IdOperatingUnit
		WHERE tn.Id = @TreasuryNoteId

		/*************************************VALIDACIONES************************************/
		
		IF NOT EXISTS 
		(
			SELECT *
			FROM Treasury.Consignment c
			WHERE c.Id = @ConsignmentId AND c.Status = 2
		)
		BEGIN
			SELECT 999 AS CodeMessage, 'El registro no existe o no se encuentra confirmado' AS [Message], 0 AS IdJournalVoucher, '' AS Consecutive
			RETURN
		END

		IF EXISTS 
		(
			SELECT 1
			FROM Treasury.Consignment c
			JOIN Treasury.ConsignmentDetail cd ON c.Id = cd.ConsignmentTransferId
			WHERE c.Id = @ConsignmentId
			GROUP BY c.Id, c.Value
			HAVING c.Value <> SUM(cd.Value)
		)
		BEGIN
			SELECT 999 AS CodeMessage, 'El valor consignado no corresponde con la suma de valores de las cajas' AS [Message], 0 AS IdJournalVoucher, '' AS Consecutive
			RETURN
		END

		IF EXISTS 
		(
			SELECT *
			FROM Treasury.Consignment c
			JOIN Treasury.EntityBankAccounts eba ON c.EntityBankAccountId = eba.Id
			WHERE c.Id = @ConsignmentId AND c.Value > eba.CurrentBalance
		)
		BEGIN
			SELECT 999 AS CodeMessage, 'El saldo de la cuenta bancaria ' + b.Name + ' # Cta. ' + eba.Number + ' es menor al valor consignado'  AS [Message], 0 AS IdJournalVoucher, '' AS Consecutive
			FROM Treasury.Consignment c
			JOIN Treasury.EntityBankAccounts eba ON c.EntityBankAccountId = eba.Id
			JOIN Payroll.Bank b ON eba.IdBank = b.Id
			WHERE c.Id = @ConsignmentId AND c.Value > eba.CurrentBalance
			RETURN
		END

		/*************************************** PROCESO BALANCE DE TESORERIA ************************************/

		INSERT INTO [Treasury].[TreasuryBalance]
		(
			[DocumentNumber],[DocumentDate],[DocumentType],[Nature],[CashRegisterId],[EntityBankAccountId],[PreviousBalance],[ValueMovement],[CreationDate]
		)
		SELECT @TreasuryNoteCode, [Common].[GETDATE](), 4, 2, NULL, eba.Id, eba.CurrentBalance, c.Value, [Common].[GETDATE]()
		FROM Treasury.Consignment c
		JOIN Treasury.EntityBankAccounts eba ON c.EntityBankAccountId = eba.Id
		WHERE c.Id = @ConsignmentId

		INSERT INTO [Treasury].[TreasuryBalance]
		(
			[DocumentNumber],[DocumentDate],[DocumentType],[Nature],[CashRegisterId],[EntityBankAccountId],[PreviousBalance],[ValueMovement],[CreationDate]
		)
		SELECT @TreasuryNoteCode, [Common].[GETDATE](), 4, 1, cr.Id, NULL, cr.CurrentBalance, cd.Value, [Common].[GETDATE]()
		FROM Treasury.Consignment c
		JOIN Treasury.ConsignmentDetail cd ON c.Id = cd.ConsignmentTransferId
		JOIN Treasury.CashRegisters cr ON cd.CashRegisterId = cr.Id
		WHERE c.Id = @ConsignmentId

		/*************************************** PROCESO SALDO DE BANCO Y CAJA ************************************/

		UPDATE eba 
			SET eba.CurrentBalance = eba.CurrentBalance - c.Value,
				eba.ModificationUser = @CodeUser,
				eba.ModificationDate = [Common].[GETDATE]()
		FROM Treasury.Consignment c
		JOIN Treasury.EntityBankAccounts eba ON c.EntityBankAccountId = eba.Id
		WHERE c.Id = @ConsignmentId
		
		UPDATE cr 
			SET cr.CurrentBalance = cr.CurrentBalance + cd.Value,
				cr.ModificationUser = @CodeUser,
				cr.ModificationDate = [Common].[GETDATE]()
		FROM Treasury.Consignment c
		JOIN Treasury.ConsignmentDetail cd ON c.Id = cd.ConsignmentTransferId
		JOIN Treasury.CashRegisters cr ON cd.CashRegisterId = cr.Id
		WHERE c.Id = @ConsignmentId

		/*************************************** PROCESO CONTABLE ************************************/

		INSERT INTO @JournalVourcherTmp
		(
			IdJournalVoucher, 
			VoucherDate, 
			Status, 
			Detail, 
			EntityCode, 
			EntityId, 
			EntityName,
			CurrencyId
		)
		SELECT
			@JournalVoucherTypeId, 
			tn.NoteDate, 
			2, 
			'Reversión de Consignación con Nota ' +  tn.Code + '. ' + tn.Description, 
			tn.Code, 
			tn.Id, 
			'TreasuryNote'
			,@TreasuryNoteCurrencyId
		FROM Treasury.TreasuryNote tn
		WHERE tn.Id = @TreasuryNoteId

		INSERT INTO @JournalVourcherDetailTmp
		(
			IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail
		)
		SELECT
			ma.Id AS IdMainAccount,
			IIF(ma.HandlesThirdParty =1, eba.ThirdPartyId, NULL) AS IdThirdParty,
			IIF(ma.HandlesCostCenter = 1, c.CostCenterId, NULL) AS IdCostCenter,			
			0,
			c.Value,
			c.Description
		FROM Treasury.Consignment c
		JOIN Treasury.EntityBankAccounts eba ON c.EntityBankAccountId = eba.Id
		JOIN GeneralLedger.MainAccounts ma ON c.MainAccountId = ma.Id
		WHERE c.Id = @ConsignmentId

		UNION ALL

		SELECT
			cd.MainAccountId AS IdMainAccount,
			IIF(ma.HandlesThirdParty =1, cr.ThirdPartyId, NULL) AS IdThirdParty,
			IIF(ma.HandlesCostCenter = 1, cd.CostCenterId, NULL) AS IdCostCenter,
			cd.Value,
			0,
			c.Description
		FROM Treasury.Consignment c
		JOIN Treasury.ConsignmentDetail cd ON c.Id = cd.ConsignmentTransferId
		JOIN Treasury.CashRegisters cr ON cd.CashRegisterId = cr.Id
		JOIN GeneralLedger.MainAccounts ma ON cd.MainAccountId = ma.Id
		WHERE c.Id = @ConsignmentId

		/************************************************************************************************************************************/

		--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
		SELECT @JournalVoucherXML = CONVERT(xml, 
			(
				SELECT * FROM @JournalVourcherTmp JournalVoucher 
				JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
				For xml AUTO,TYPE, ELEMENTS
			)
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
			SELECT 999 as CodeMessage, @Message as Message, 0 AS IdJournalVoucher, '' AS Consecutive
			RETURN
		END
		
		SELECT 
			@Message = 'Comprobante Contable ' + jvt.Code + ' - ' + jvt.Name + ' con Consecutivo ' + CAST(jv.Consecutive AS VARCHAR(30)),
			@Consecutive = CAST(jv.Consecutive AS VARCHAR(30))
		FROM GeneralLedger.JournalVouchers jv
		JOIN GeneralLedger.JournalVoucherTypes jvt ON jv.IdJournalVoucher = jvt.Id
		WHERE jv.Id = @IdJournalVoucherResult

		/*************************************** ACTUALIZACION DE LA CONSIGNACION ************************************/

		UPDATE c
			SET c.Status = 4,
				c.ModificationUser = @CodeUser,
				c.ModificationDate = [Common].[GETDATE](),
				c.ReversedUser = @CodeUser,
				c.ReversedDate = [Common].[GETDATE]()
		FROM Treasury.Consignment c
		WHERE c.Id = @ConsignmentId

		SELECT 0 AS CodeMessage, @Message AS [Message], @IdJournalVoucherResult AS IdJournalVoucher, @Consecutive AS Consecutive

	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS [Message], 0 AS IdJournalVoucher, '' AS Consecutive
	END CATCH
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reversa una consignación bancaria previamente confirmada en el módulo de Tesorería. Recibe el identificador de una nota de tesorería y el usuario que ejecuta la operación; valida que la consignación esté en estado confirmado, que el valor total coincida con la suma de los detalles por caja y que el saldo actual de la cuenta bancaria sea suficiente para revertir el movimiento. Si todas las validaciones son exitosas, descuenta el valor de la cuenta bancaria (EntityBankAccounts), devuelve el saldo a las cajas origen (CashRegisters), registra los movimientos en el balance de tesorería (TreasuryBalance) y genera el comprobante contable correspondiente según el tipo de comprobante configurado en SettingsTreasury para notas de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseConsignment';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseConsignment';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reversa una consignación bancaria previamente confirmada: devuelve los fondos a las cajas, descuenta el saldo bancario, registra movimientos de balance y genera el comprobante contable inverso.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseConsignment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La nota de tesorería (TreasuryNote) debe existir y tener ConsignmentId asociado; Debe existir configuración en SettingsTreasury para la unidad operativa de la nota con JournalVoucherTypeTreasuryNotes definido; La consignación referenciada debe estar en Status=2 (confirmada); La suma de ConsignmentDetail.Value debe coincidir con Consignment.Value; El saldo actual (CurrentBalance) de la cuenta bancaria de la consignación debe ser suficiente para descontar el valor consignado', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseConsignment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reversan consignaciones en estado confirmado (Status=2); al reversarlas pasan a Status=4; El valor de la consignación debe ser igual a la sumatoria de los valores de sus detalles (cajas) para poder reversarse; El saldo actual de la cuenta bancaria debe ser >= al valor consignado antes de reversar; La reversión es contablemente simétrica al asiento original: debita las cajas (CashRegisters) y acredita la cuenta bancaria (EntityBankAccounts); Toda reversión exitosa genera un comprobante contable cuyo tipo proviene de SettingsTreasury.JournalVoucherTypeTreasuryNotes según la unidad operativa de la nota; Si falla el guardado del comprobante contable, NO se actualiza el estado de la consignación a reversada (queda en estado anterior); Cada movimiento de reversión registra dos filas en TreasuryBalance: DocumentType=4 con Nature=2 (banco) y Nature=1 (caja); El uso de tercero/centro de costo en cada línea del comprobante depende de la configuración de la cuenta principal (HandlesThirdParty/HandlesCostCenter)', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseConsignment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reversión de consignación; Nota de tesorería; Saldo de cuenta bancaria; Saldo de caja registradora; Balance de tesorería; Comprobante contable; Tercero contable; Centro de costo; Plan de cuentas (cuenta principal)', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseConsignment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Treasury.TreasuryBalance: Por cada reversión inserta una fila con DocumentType=4 y Nature=2 referenciando la cuenta bancaria afectada (EntityBankAccountId), con su saldo previo y el valor de la consignación; [INSERT] Treasury.TreasuryBalance: Por cada caja del detalle inserta una fila con DocumentType=4 y Nature=1 referenciando CashRegisterId, registrando saldo previo de la caja y el valor del detalle; [UPDATE] Treasury.EntityBankAccounts: Resta c.Value al CurrentBalance de la cuenta bancaria de la consignación y registra ModificationUser/ModificationDate; [UPDATE] Treasury.CashRegisters: Suma cd.Value al CurrentBalance de cada caja involucrada en el ConsignmentDetail (devolución de fondos) y registra ModificationUser/ModificationDate; [UPDATE] Treasury.Consignment: Tras reversión exitosa cambia Status=4 y guarda ReversedUser, ReversedDate, ModificationUser y ModificationDate; [INSERT] GeneralLedger.JournalVouchers: Vía SP_CreateAndValidateJournalVoucherMovement crea comprobante contable de reversión: debita cuentas de cajas (cd.MainAccountId) y acredita cuenta bancaria (c.MainAccountId) por el valor correspondiente; [RETURN_RESULT] Result: Devuelve CodeMessage (0=éxito, 999=error), Message, IdJournalVoucher y Consecutive del comprobante contable generado; [RETURN_RESULT] Result: En CATCH retorna CodeMessage=999 con ERROR_MESSAGE() + número de línea', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseConsignment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe Consignment con Id=@ConsignmentId y Status=2 (confirmada) → Retorna CodeMessage=999 con mensaje ''El registro no existe o no se encuentra confirmado'' y termina else Continúa validaciones; si c.Value <> SUM(cd.Value) entre Consignment y ConsignmentDetail → Retorna 999 ''El valor consignado no corresponde con la suma de valores de las cajas'' y termina; si c.Value > eba.CurrentBalance (saldo bancario insuficiente) → Retorna 999 indicando banco y número de cuenta con saldo insuficiente y termina; si @CodeMessage = 999 tras invocar SP_CreateAndValidateJournalVoucherMovement → Retorna 999 con el mensaje recibido y termina sin actualizar estado de la consignación; si ma.HandlesThirdParty = 1 (cuenta maneja terceros) → Asigna IdThirdParty (eba.ThirdPartyId o cr.ThirdPartyId) en el detalle contable else IdThirdParty queda NULL; si ma.HandlesCostCenter = 1 (cuenta maneja centro de costo) → Asigna IdCostCenter en el detalle contable else IdCostCenter queda NULL', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseConsignment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseConsignment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.TreasuryNote; Treasury.SettingsTreasury; Treasury.Consignment; Treasury.ConsignmentDetail; Treasury.EntityBankAccounts; Treasury.CashRegisters; Payroll.Bank; GeneralLedger.MainAccounts; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseConsignment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseConsignment';
-- GO
