-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-10-06
-- Description:	Procedimiento almacenado para conciliar cxp con contabilidad
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportReconcilePayments]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON

	DECLARE	@DateStart DATE,
			@DateEnd DATE,
			@TypeReport INT,
			@EntityNames VARCHAR(MAX),
			@JournalVoucherTypes VARCHAR(MAX),
			@MainAccounts VARCHAR(MAX),
			-------------
			@FilterByEntityName BIT = 0,
			@FilterByJournalVoucherTypes BIT = 0,
			@FilterByMainAccounts BIT = 0
	
	DECLARE @Table_EntityNames AS TABLE(EntityName VARCHAR(250))
	DECLARE @Table_JournalVoucherTypes AS TABLE(Id INT)
	DECLARE @Table_MainAccounts AS TABLE(Id INT)
	DECLARE @AccountPayableAccount AS TABLE(MainAccountId INT)

	BEGIN TRY

		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@DateStart = t.x.value('DateStart[1]','date'),
				@DateEnd = t.x.value('DateEnd[1]','date'),
				@TypeReport = t.x.value('TypeReport[1]','int'),
				@EntityNames = t.x.value('EntityNames[1]','varchar(max)'),
				@JournalVoucherTypes = t.x.value('JournalVoucherTypes[1]','varchar(max)'),
				@MainAccounts = t.x.value('MainAccounts[1]','varchar(max)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		IF ISNULL(@EntityNames, '') <> ''
		BEGIN
			SET @FilterByEntityName = 1

			INSERT INTO @Table_EntityNames
				SELECT Data Data 
				FROM dbo.Split(@EntityNames, ',')
		END

		IF ISNULL(@JournalVoucherTypes, '') <> ''
		BEGIN
			SET @FilterByJournalVoucherTypes = 1

			INSERT INTO @Table_JournalVoucherTypes
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@JournalVoucherTypes, ',')
		END

		IF ISNULL(@MainAccounts, '') <> ''
		BEGIN
			SET @FilterByMainAccounts = 1

			INSERT INTO @Table_MainAccounts
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@MainAccounts, ',')
		END

		INSERT INTO @AccountPayableAccount
			SELECT DISTINCT ap.IdAccount
			FROM Payments.AccountPayable ap WITH (NOLOCK)

		/******************************************  OBTENCION DE DATOS ******************************************/

		SELECT	k.DocumentDate,
				jv.DocumentDate AccountingDocumentDate,
				en.Description,
				ISNULL(k.EntityName, jv.EntityName) EntityName,
				ISNULL(k.EntityCode, jv.EntityCode) EntityCode,
				jv.JournalVoucherType,
				jv.Consecutive,
				ISNULL(k.MainAccount, jv.MainAccount) MainAccount,
				--------------------------------------------------------			
				ISNULL(k.DebitValue, 0) DebitValue,
				Common.CurrencyConverterWithDate(ISNULL(jv.DebitValue, 0),jv.OfficialCurrencyId,ISNULL(k.CurrencyId,jv.OfficialCurrencyId),jv.CreationDate) AccountingDebitValue, 
				--------------------------------------------------------
				ISNULL(k.CreditValue, 0) CreditValue,
				Common.CurrencyConverterWithDate(ISNULL(jv.CreditValue, 0),jv.OfficialCurrencyId,ISNULL(k.CurrencyId,jv.OfficialCurrencyId),jv.CreationDate) AccountingCreditValue, 
				isnull(k.CurrencyId,jv.OfficialCurrencyId) CurrencyId,--12
				cy.Abbreviation
		FROM
		(
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('AccountPayable' AS VARCHAR(250)) EntityName, ap.Code EntityCode, ap.Id EntityId,
						CAST(ap.DocumentDate AS DATE) DocumentDate,
						SUM(0) DebitValue, 
						SUM(ap.Value) CreditValue,
						ap.CurrencyId
				FROM Payments.AccountPayable ap WITH (NOLOCK)
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ap.IdAccount = ma.Id
				WHERE ap.Status = 2
					AND CAST(ap.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							ap.Code, ap.Id, CAST(ap.DocumentDate AS DATE),ap.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('PaymentNotes' AS VARCHAR(250)) EntityName, pn.Code EntityCode, pn.Id EntityId,
						CAST(pn.NoteDate AS DATE) DocumentDate,
						SUM(IIF(pn.Nature = 1, pnapa.AdjusmentValue, 0)) DebitValue, 
						SUM(IIF(pn.Nature = 1, 0, pnapa.AdjusmentValue)) CreditValue,
						pn.CurrencyId
				FROM Payments.PaymentNotes pn WITH (NOLOCK)
				JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH (NOLOCK) ON pn.Id = pnapa.PaymentNoteId
				JOIN Payments.AccountPayable ap WITH (NOLOCK) ON pnapa.AccountPayableId = ap.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ap.IdAccount = ma.Id
				WHERE pn.Status = 2
					AND CAST(pn.NoteDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							pn.Code, pn.Id, CAST(pn.NoteDate AS DATE),pn.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('PaymentTransfer' AS VARCHAR(250)) EntityName, pt.Code EntityCode, pt.Id EntityId,
						CAST(pt.DocumentDate AS DATE) DocumentDate,
						SUM(ptd.Value) DebitValue, 
						SUM(0) CreditValue,
						ap.CurrencyId
				FROM Payments.PaymentTransfer pt WITH (NOLOCK)
				JOIN Payments.PaymentTransferDetail ptd WITH (NOLOCK) ON pt.Id = ptd.PaymentTransferId
				JOIN Payments.AccountPayable ap WITH (NOLOCK) ON ptd.AccountPayableId = ap.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ap.IdAccount = ma.Id
				WHERE pt.Status = 2
					AND CAST(pt.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							pt.Code, pt.Id, CAST(pt.DocumentDate AS DATE), ap.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('CrossingAccount' AS VARCHAR(250)) EntityName, ca.Code EntityCode, ca.Id EntityId,
						CAST(ca.DocumentDate AS DATE) DocumentDate,
						SUM(cadcxp.CrossingValue) DebitValue, 
						SUM(0) CreditValue,
						ap.CurrencyId
				FROM Treasury.CrossingAccount ca WITH (NOLOCK)
				JOIN Treasury.CrossingAccountDetailCxP cadcxp WITH (NOLOCK) ON ca.Id = cadcxp.CrossingAccountId
				JOIN Payments.AccountPayable ap WITH (NOLOCK) ON cadcxp.AccountPayableId = ap.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ap.IdAccount = ma.Id
				WHERE ca.Status = 2
					AND CAST(ca.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							ca.Code, ca.Id, CAST(ca.DocumentDate AS DATE), ap.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('VoucherTransaction' AS VARCHAR(250)) EntityName, vt.Code EntityCode, vt.Id EntityId,
						CAST(vt.DocumentDate AS DATE) DocumentDate,
						SUM(IIF(vtd.Nature = 1, db.AdvancedValue, 0)) DebitValue, 
						SUM(IIF(vtd.Nature = 1, 0, db.AdvancedValue)) CreditValue,
						vt.CurrencyId
				FROM Treasury.VoucherTransaction vt WITH (NOLOCK)
				JOIN Treasury.VoucherTransactionDetails vtd WITH (NOLOCK) ON vt.Id = vtd.IdVoucherTransaction
				JOIN Treasury.DischargeBill db WITH (NOLOCK) ON vtd.Id = db.IdVoucherTransactionD
				JOIN Payments.AccountPayable ap WITH (NOLOCK) ON db.IdAccountPayable = ap.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ap.IdAccount = ma.Id
				WHERE vt.Status IN (2, 4)
					AND CAST(vt.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							vt.Code, vt.Id, CAST(vt.DocumentDate AS DATE), vt.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('TreasuryNote' AS VARCHAR(250)) EntityName, tn.Code EntityCode, tn.Id EntityId,
						CAST(tn.NoteDate AS DATE) DocumentDate,
						SUM(IIF(vtd.Nature = 1, 0, db.AdvancedValue)) DebitValue, 
						SUM(IIF(vtd.Nature = 1, db.AdvancedValue, 0)) CreditValue,
						tn.CurrencyId
				FROM Treasury.TreasuryNote tn WITH (NOLOCK)
				JOIN Treasury.VoucherTransaction vt WITH (NOLOCK) ON tn.VoucherTransactionId = vt.Id
				JOIN Treasury.VoucherTransactionDetails vtd WITH (NOLOCK) ON vt.Id = vtd.IdVoucherTransaction
				JOIN Treasury.DischargeBill db WITH (NOLOCK) ON vtd.Id = db.IdVoucherTransactionD
				JOIN Payments.AccountPayable ap WITH (NOLOCK) ON db.IdAccountPayable = ap.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ap.IdAccount = ma.Id
				WHERE tn.Status = 2
					AND CAST(tn.NoteDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							tn.Code, tn.Id, CAST(tn.NoteDate AS DATE),tn.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('CashReceipts' AS VARCHAR(250)) EntityName, cr.Code EntityCode, cr.Id EntityId,
						CAST(cr.DocumentDate AS DATE) DocumentDate,
						SUM(IIF(crd.Nature = 1, crdap.RefundValue, 0)) DebitValue, 
						SUM(IIF(crd.Nature = 1, 0, crdap.RefundValue)) CreditValue,
						crd.CurrencyId
				FROM Treasury.CashReceipts cr WITH (NOLOCK)
				JOIN Treasury.CashReceiptDetails crd WITH (NOLOCK) ON cr.Id = crd.IdCashReceipt
				JOIN Treasury.CashReceiptDetailAccountPayable crdap WITH (NOLOCK) ON crd.Id = crdap.CashReceiptDetailId
				JOIN Payments.AccountPayable ap WITH (NOLOCK) ON crdap.AccountPayableId = ap.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ap.IdAccount = ma.Id
				WHERE cr.Status IN (2, 4)
					AND CAST(cr.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							cr.Code, cr.Id, CAST(cr.DocumentDate AS DATE),crd.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('TreasuryNote' AS VARCHAR(250)) EntityName, tn.Code EntityCode, tn.Id EntityId,
						CAST(tn.NoteDate AS DATE) DocumentDate,
						SUM(IIF(crd.Nature = 1, 0, crdap.RefundValue)) DebitValue, 
						SUM(IIF(crd.Nature = 1, crdap.RefundValue, 0)) CreditValue,
						tn.CurrencyId
				FROM Treasury.TreasuryNote tn WITH (NOLOCK)
				JOIN Treasury.CashReceipts cr WITH (NOLOCK) ON tn.CashRegisterId = cr.Id
				JOIN Treasury.CashReceiptDetails crd WITH (NOLOCK) ON cr.Id = crd.IdCashReceipt
				JOIN Treasury.CashReceiptDetailAccountPayable crdap WITH (NOLOCK) ON crd.Id = crdap.CashReceiptDetailId
				JOIN Payments.AccountPayable ap WITH (NOLOCK) ON crdap.AccountPayableId = ap.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ap.IdAccount = ma.Id
				WHERE tn.Status = 2
					AND CAST(tn.NoteDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							tn.Code, tn.Id, CAST(tn.NoteDate AS DATE),tn.CurrencyId
		) k
		FULL JOIN
		(
			SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
					jv.EntityName, jv.EntityCode, jv.EntityId,
					CAST(jv.VoucherDate AS DATE) DocumentDate,
					jv.Consecutive, 
					jvt.Id JournalVoucherTypeId,
					CONCAT(jvt.Code, ' - ', jvt.Name) JournalVoucherType,
					SUM(jvd.DebitValue) DebitValue, 
					SUM(jvd.CreditValue) CreditValue,
					lb.OfficialCurrencyId,
					jv.CreationDate
			FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
			JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			join GeneralLedger.LegalBook lb WITH (NOLOCK) on jv.LegalBookId = lb.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id 
			JOIN @AccountPayableAccount apa ON ma.Id = apa.MainAccountId
			WHERE jv.Status = 2
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
						jv.EntityName, jv.EntityCode, jv.EntityId, 
						CAST(jv.VoucherDate AS DATE), 
						jv.Consecutive, jvt.Id, CONCAT(jvt.Code, ' - ', jvt.Name),lb.OfficialCurrencyId,jv.CreationDate
		) jv ON ISNULL(k.EntityName, '') = ISNULL(jv.EntityName, '') 
			AND ISNULL(k.EntityId, 0) = ISNULL(jv.EntityId, 0) 
			AND k.MainAccountId = jv.MainAccountId
		LEFT JOIN Common.GetEntityNameDescriptions() en ON ISNULL(k.EntityName, jv.EntityName) = en.EntityName
		LEFT JOIN @Table_EntityNames ten ON ISNULL(k.EntityName, jv.EntityName) = ten.EntityName
		LEFT JOIN @Table_JournalVoucherTypes tjvt ON jv.JournalVoucherTypeId = tjvt.Id
		LEFT JOIN @Table_MainAccounts tma ON ISNULL(k.MainAccountId, jv.MainAccountId) = tma.Id
		JOIN Common.Currency cy WITH(NOLOCK) on cy.Id = ISNULL(k.CurrencyId,jv.OfficialCurrencyId)
		WHERE (@FilterByEntityName = 0 OR ten.EntityName IS NOT NULL)
			AND (@FilterByJournalVoucherTypes = 0 OR tjvt.Id IS NOT NULL)
			AND (@FilterByMainAccounts = 0 OR tma.Id IS NOT NULL)
			AND 
			(
				@TypeReport = 3
				OR
				(
					@TypeReport = 2
					AND
					(
						ROUND(ISNULL(k.DebitValue, 0), 2) = ROUND(ISNULL(jv.DebitValue, 0), 2)
						OR 
						ROUND(ISNULL(k.CreditValue, 0), 2) = ROUND(ISNULL(jv.CreditValue, 0), 2)
					)
				)
				OR
				(
					@TypeReport = 1
					AND
					(
						ROUND(ISNULL(k.DebitValue, 0), 2) <> ROUND(ISNULL(jv.DebitValue, 0), 2)
						OR 
						ROUND(ISNULL(k.CreditValue, 0), 2) <> ROUND(ISNULL(jv.CreditValue, 0), 2)
					)
				)
			)
		ORDER BY	ISNULL(k.DocumentDate, jv.DocumentDate), 
					ISNULL(k.EntityName, jv.EntityName), 
					ISNULL(k.EntityCode, jv.EntityCode),
					ISNULL(k.MainAccount, jv.MainAccount),
					ISNULL(k.CurrencyId, jv.OfficialCurrencyId)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento para conciliar las cuentas por pagar (CXP) del módulo de Pagos con la contabilidad del libro mayor. Consolida y compara, dentro de un rango de fechas configurable, los movimientos de cuentas por pagar confirmadas, notas de pago (débito y crédito), transferencias de pago y cruces de cuenta, identificando para cada documento su cuenta contable principal, los valores débito y crédito en módulo y en contabilidad (con conversión de moneda), el tipo de comprobante contable y la entidad (proveedor o tercero). Permite filtrar por nombre de entidad, tipo de comprobante contable y cuentas contables principales, recibiendo todos los criterios de búsqueda empaquetados en un XML. Se usa para generar el reporte de conciliación entre el submódulo de pagos a proveedores y el libro mayor, facilitando la detección de diferencias entre lo registrado en CXP y lo contabilizado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportReconcilePayments';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportReconcilePayments';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de conciliación entre los movimientos de cuentas por pagar (CxP) provenientes de varios módulos operativos y los asientos contables del libro mayor, mostrando coincidencias y/o diferencias por entidad y cuenta principal.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcilePayments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener DateStart, DateEnd y TypeReport (1=diferencias, 2=coincidencias, 3=todos); Las listas EntityNames, JournalVoucherTypes y MainAccounts deben venir separadas por coma si se usan como filtro; Los IDs de JournalVoucherTypes y MainAccounts deben ser convertibles a INT; Debe existir la función dbo.Split para parsear listas y Common.CurrencyConverterWithDate y Common.GetEntityNameDescriptions disponibles', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcilePayments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran documentos operativos en estado 2 (y adicionalmente 4 para VoucherTransaction y CashReceipts); Solo se consideran asientos contables (JournalVouchers) en estado 2; Solo se incluyen asientos contables cuya cuenta principal corresponde a una cuenta usada en Payments.AccountPayable (vía @AccountPayableAccount); El cruce entre operativo y contable se realiza por EntityName, EntityId y MainAccountId; Los valores se convierten a la moneda del documento operativo (o a la oficial si no existe) usando Common.CurrencyConverterWithDate sobre la fecha de creación del asiento; Las comparaciones de igualdad/diferencia entre valores se hacen redondeadas a 2 decimales; El rango de fechas (@DateStart - @DateEnd) se aplica sobre la fecha de documento de cada origen y sobre VoucherDate del asiento contable; Para AccountPayable solo se contabiliza valor en CreditValue; para PaymentTransfer y CrossingAccount solo en DebitValue', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcilePayments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Conciliación contable; Cuentas por pagar (CxP); Notas de pago; Transferencia de pagos; Cruce de cuentas; Comprobantes de egreso (VoucherTransaction); Notas de tesorería; Recibos de caja; Comprobantes contables (JournalVouchers); Plan de cuentas / Cuenta principal; Libro legal / Moneda oficial; Naturaleza débito/crédito; Conversión de moneda', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcilePayments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve el cruce entre movimientos operativos de CxP (AccountPayable, PaymentNotes, PaymentTransfer, CrossingAccount, VoucherTransaction, TreasuryNote, CashReceipts) y comprobantes contables (JournalVouchers) cuando ambos cumplen los filtros y rango de fechas; [RETURN_RESULT] Resultset: En caso de error en el TRY, retorna una fila con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() y línea del error', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcilePayments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @EntityNames no es nulo ni vacío → Activa filtro por EntityName y carga la lista en tabla temporal else No filtra por EntityName; si @JournalVoucherTypes no es nulo ni vacío → Activa filtro por tipo de comprobante contable else No filtra por tipo de comprobante; si @MainAccounts no es nulo ni vacío → Activa filtro por cuentas principales else No filtra por cuenta principal; si @TypeReport = 3 → Incluye todos los registros (conciliados y no conciliados); si @TypeReport = 2 → Solo incluye registros donde el débito o crédito operativo coincide (redondeado a 2 decimales) con el contable; si @TypeReport = 1 → Solo incluye registros donde el débito o crédito operativo difiere del contable; si En PaymentNotes/VoucherTransaction/TreasuryNote/CashReceipts: Nature = 1 → Asigna el valor a DebitValue else Asigna el valor a CreditValue', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcilePayments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split; Common.CurrencyConverterWithDate; Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcilePayments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayable; Payments.PaymentNotes; Payments.PaymentNotesAccountPayableAdvance; Payments.PaymentTransfer; Payments.PaymentTransferDetail; Treasury.CrossingAccount; Treasury.CrossingAccountDetailCxP; Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.DischargeBill; Treasury.TreasuryNote; Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashReceiptDetailAccountPayable; GeneralLedger.MainAccounts; GeneralLedger.JournalVoucherTypes; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; GeneralLedger.LegalBook; Common.Currency', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcilePayments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcilePayments';
-- GO
