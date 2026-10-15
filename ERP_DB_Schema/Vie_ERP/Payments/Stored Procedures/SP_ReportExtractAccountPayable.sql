-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-01-30
-- Description:	Procedimiento para el reporte de extracto de cuentas por pagar
-- =============================================
CREATE PROCEDURE [Payments].[SP_ReportExtractAccountPayable]
	@InitialDate AS DATE,
	@EndDate AS DATE,
	@InitialNit AS varchar(20),
	@EndNit AS varchar(20),
	@InitialBillNumber AS varchar(100),
	@EndBillNumber AS varchar(100),
	@Type AS TINYINT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		DECLARE @OffialCurrencyId AS INT
		SET @OffialCurrencyId = (SELECT TOP 1 cs.OfficialCurrencyId FROM GeneralLedger.CompanySettings cs)
		SELECT
			tp.Nit AS ThirdPartyNit, 
			tp.Name AS ThirdPartyName, 
			dl.Name AS DistributionLineName, 
			ma.Number AS AccountNumber, 
			ma.Name AS AccountName, 
			ap.BillNumber AS BillNumber, 
			CASE 
				WHEN ap.EntityName = 'AccountPayable' THEN 'Cuenta Por Pagar' 
				WHEN ap.EntityName = 'InitialBalance' THEN 'Saldo Inicial' 
			END AS EntityName, 
			IIF(@Type = 3, ap.Value, Common.CurrencyConverter(ap.Value, ISNULL(ap.CurrencyId, @OffialCurrencyId), @OffialCurrencyId)) AS BillValueInitial, 
			IIF(@Type = 3, ap.Balance, Common.CurrencyConverter(ap.Balance, ISNULL(ap.CurrencyId, @OffialCurrencyId), @OffialCurrencyId)) AS BillCurrentBalance, 
			data.MovesCode,
			data.MovesDate,
			IIF(@Type = 3, data.MovesDebit, Common.CurrencyConverter(data.MovesDebit, ISNULL(ap.CurrencyId, @OffialCurrencyId), @OffialCurrencyId)) MovesDebit,
			IIF(@Type = 3, data.MovesCredit, Common.CurrencyConverter(data.MovesCredit, ISNULL(ap.CurrencyId, @OffialCurrencyId), @OffialCurrencyId)) MovesCredit,
			data.VoucherName,
			ap.IdSupplier AS IdSupplier,
			ap.Id AS IdBills,
			data.Status,
			ap.Code AS CodeAccountPayable,
			ISNULL(data.Abbreviation, c.Abbreviation) CurrencyAbbreviation
		FROM Payments.AccountPayable ap WITH (NOLOCK)
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON tp.Id = ap.IdThirdParty 
		JOIN Common.SuppliersDistributionLines sdl WITH (NOLOCK) ON sdl.Id = ap.IdSuppliersDistributionLines 
		JOIN Common.DistributionLines dl WITH (NOLOCK) ON dl.Id = sdl.IdDistributionLine 
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.Id = dl.IdMainAccount
		JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ISNULL(ap.CurrencyId, @OffialCurrencyId)
		JOIN 
		(
			SELECT 
				ap.Id AS AccountPayableId,
				ap.Code AS MovesCode, 
				ap.DocumentDate AS MovesDate, 
				0 AS MovesDebit, 
				CASE ap.InitialBalance 
					WHEN 1 THEN ISNULL(iniap.Balance, ap.Value) 
					ELSE ap.Value 
				END AS MovesCredit, 
				'Cuenta por pagar' AS VoucherName, 
				ap.Status AS Status,
				C.Abbreviation
			FROM [Payments].[AccountPayable] AP WITH (NOLOCK) 	
			JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ISNULL(ap.CurrencyId, @OffialCurrencyId)
			LEFT JOIN Payments.InitialBalanceAccountPayable iniap WITH (NOLOCK) on iniap.AccountPayableId = ap.Id
			WHERE ap.Status = 2
			-------------------------------------------------------------------------------------------------------
			UNION ALL
			-------------------------------------------------------------------------------------------------------
			SELECT 
				papa.AccountPayableId,
				pn.Code AS MovesCode, 
				pn.NoteDate AS MovesDate, 
				SUM(CASE 
					WHEN pn.Nature = 1 THEN ISNULL(papa.AdjustmentValueShare, papa.AdjusmentValue)
					ELSE 0 
				END) AS MovesDebit, 
				SUM(CASE 
					WHEN pn.Nature = 2 THEN ISNULL(papa.AdjustmentValueShare, papa.AdjusmentValue)
					ELSE 0 
				END) AS MovesCredit, 
				'Nota de CxP' AS VoucherName, 		
				pn.Status AS Status,
				C.Abbreviation
			FROM [Payments].[PaymentNotesAccountPayableAdvance] papa  WITH (NOLOCK) 
			JOIN Payments.PaymentNotes pn WITH (NOLOCK) ON pn.Id = papa.PaymentNoteId 
			JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ISNULL(pn.CurrencyId, @OffialCurrencyId)
			WHERE pn.Status = 2
			GROUP BY papa.AccountPayableId, pn.Code, pn.NoteDate, pn.Status, c.Abbreviation
			-------------------------------------------------------------------------------------------------------
			UNION ALL
			-------------------------------------------------------------------------------------------------------
			SELECT 
				ap.Id AccountPayableId,
				pn.Code AS MovesCode, 
				pn.NoteDate AS MovesDate, 
				ap.Value AS MovesDebit, 
				0 AS MovesCredit, 
				'Nota de CxP' AS VoucherName, 		
				pn.Status AS Status,
				C.Abbreviation
			FROM [Payments].[AccountPayable] ap WITH (NOLOCK) 
			JOIN Payments.PaymentNotes pn WITH (NOLOCK) ON pn.IdAccountPayable = ap.Id 
			JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ISNULL(ap.CurrencyId, @OffialCurrencyId)
			WHERE pn.Status = 2
			-------------------------------------------------------------------------------------------------------
			UNION ALL
			-------------------------------------------------------------------------------------------------------
			SELECT 
				ptd.AccountPayableId,
				pt.Code AS MovesCode, 
				pt.DocumentDate AS MovesDate, 
				IIF(ptd.ValueInCurrencyInvoice = 0, ptd.Value, ptd.ValueInCurrencyInvoice) AS MovesDebit, 
				0 AS MovesCredit, 
				'Cruce Anticipo vs CxP' AS VoucherName, 
				pt.Status AS Status,
				NULL
			FROM [Payments].[PaymentTransferDetail] ptd WITH (NOLOCK) 
			JOIN Payments.PaymentTransfer PT WITH (NOLOCK) ON pt.Id = ptd.PaymentTransferId 
			WHERE pt.Status = 2
			-------------------------------------------------------------------------------------------------------
			UNION ALL
			-------------------------------------------------------------------------------------------------------
			SELECT 
				cadcxp.AccountPayableId,
				ca.Code AS MovesCode, 
				ca.DocumentDate AS MovesDate, 
				cadcxp.CrossingValue AS MovesDebit, 
				0 AS MovesCredit, 
				'Cruce de Cuenta' AS VoucherName, 
				ca.Status AS Status,
				NULL
			FROM [Treasury].[CrossingAccountDetailCxP] cadcxp WITH (NOLOCK) 
			JOIN Treasury.CrossingAccount ca WITH (NOLOCK) ON ca.Id = cadcxp.CrossingAccountId
			WHERE ca.Status = 2
			-------------------------------------------------------------------------------------------------------
			UNION ALL
			-------------------------------------------------------------------------------------------------------
			SELECT 
				db.IdAccountPayable AS AccountPayableId,
				vt.Code AS MovesCode, 
				vt.DocumentDate AS MovesDate, 
				IIF(db.ValueInCurrencyHeader = 0, db.AdvancedValue, db.ValueInCurrencyHeader) AS MovesDebit, 
				0 AS MovesCredit, 
				'Comprobante de Egreso' AS VoucherName, 
				vt.Status AS Status,
				C.Abbreviation
			FROM [Treasury].[DischargeBill] db WITH (NOLOCK) 
			JOIN Treasury.VoucherTransactionDetails vtd WITH (NOLOCK) ON vtd.Id = db.IdVoucherTransactionD 
			JOIN Treasury.VoucherTransaction vt WITH (NOLOCK) ON vt.Id = vtd.IdVoucherTransaction 
			JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ISNULL(VT.CurrencyId, @OffialCurrencyId)
			WHERE vt.Status IN (2, 4)
			-------------------------------------------------------------------------------------------------------
			UNION ALL
			-------------------------------------------------------------------------------------------------------	
			SELECT
				db.IdAccountPayable,
				tn.Code AS MovesCode, 
				tn.NoteDate AS MovesDate, 
				0 AS MovesDebit, 
				IIF(db.ValueInCurrencyHeader = 0, db.AdvancedValue, db.ValueInCurrencyHeader) AS MovesCredit, 
				'Nota de Tesoreria' AS VoucherName, 		
				tn.Status AS Status,
				C.Abbreviation
			FROM [Treasury].[TreasuryNote] tn WITH (NOLOCK) 
			JOIN Treasury.VoucherTransaction vt WITH (NOLOCK) ON vt.Id = tn.VoucherTransactionId 
			JOIN Treasury.VoucherTransactionDetails vtd WITH (NOLOCK) ON vt.Id = vtd.IdVoucherTransaction 
			JOIN Treasury.DischargeBill db WITH (NOLOCK) ON vtd.Id = DB.IdVoucherTransactionD 
			JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ISNULL(TN.CurrencyId, @OffialCurrencyId)
			WHERE tn.Status = 2 AND vt.Status IN (2, 4)
			-------------------------------------------------------------------------------------------------------
			UNION ALL
			-------------------------------------------------------------------------------------------------------
			SELECT
				crdap.AccountPayableId,
				CR.Code AS MovesCode, 
				CR.DocumentDate AS MovesDate, 
				0 AS MovesDebit, 
				IIF(crd.ValueInCurrencyHeader = 0, crdap.RefundValue, crd.ValueInCurrencyHeader) AS MovesCredit, 
				'R. Caja Reintegro CxP' AS VoucherName, 
				cr.Status AS Status,
				C.Abbreviation
			FROM Treasury.CashReceipts cr WITH (NOLOCK) 
			JOIN Treasury.CashReceiptDetails AS crd WITH (NOLOCK) ON cr.id = crd.IdCashReceipt 
			JOIN Treasury.CashReceiptDetailAccountPayable as crdap WITH (NOLOCK) ON crd.ID = crdap.CashReceiptDetailId 	
			JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ISNULL(CRD.CurrencyId, @OffialCurrencyId)
			WHERE cr.Status = 2
		) AS data ON ap.Id = data.AccountPayableId
		WHERE 
			(
				@InitialDate IS NULL
				OR
				CAST(data.MovesDate AS DATE) BETWEEN @InitialDate AND @EndDate
			)
			AND
			(
				@InitialNit IS NULL
				OR
				tp.Nit BETWEEN @InitialNit AND @EndNit
			)
			AND
			(
				@InitialBillNumber IS NULL
				OR
				ap.BillNumber BETWEEN @InitialBillNumber AND @EndBillNumber
			)
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END