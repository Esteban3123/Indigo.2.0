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