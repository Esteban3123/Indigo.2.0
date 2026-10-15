-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	SP que genera la informacion para el XML FormatoArchiveFT004
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportArchiveFT004]
	@mesFinal INT,
    @Year INT,
    @bookId AS INT	
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY

	DECLARE @ClosingDate DATE = '01/' + RIGHT('0' + CAST(@mesFinal AS VARCHAR(20)), 2)+ '/' + CAST(@Year AS VARCHAR(20)),
			@AfterClosingDate DATE

	SET @ClosingDate = DATEADD(DAY, -1, DATEADD(MONTH, 1, @ClosingDate))
	SET @AfterClosingDate = DATEADD(DAY, 1, @ClosingDate)
	
	SELECT 
		TipoIdAcreedor, IdAcreedor, DvAcreedor, NombreAcreedor, ActividadAcreedor, ConceptoAcreencia, medicionPosterior, 
		SUM(CxPNoVencidas) AS CxPNoVencidas, 
		SUM(CxPMora30dias) AS CxPMora30dias, 
		SUM(CxPMora60dias) AS CxPMora60dias, 
		SUM(CxPMora90dias) AS CxPMora90dias, 
		SUM(CxPMora180dias) AS CxPMora180dias, 
		SUM(CxPMora360dias) AS CxPMora360dias, 
		SUM(CxPMoraMayor360dias) AS CxPMoraMayor360dias, 
		0 AS ajuste, 
		SUM(CxPNoVencidas) + SUM(CxPMora30dias) + SUM(CxPMora60dias) + SUM(CxPMora90dias) + SUM(CxPMora180dias) + SUM(CxPMora360dias) + SUM(CxPMoraMayor360dias) AS saldo
	FROM 
	(
		SELECT
			CASE cp.IdentificationType WHEN 0 THEN 'CC' WHEN 1 THEN 'CE' WHEN 7 THEN 'NI' ELSE 'OT' END AS TipoIdAcreedor,
			ct.Nit AS IdAcreedor,
			ct.DigitVerification AS DvAcreedor,
			ct.[Name] AS NombreAcreedor,
			ISNULL(ct.CodeCIIU,'NA') AS ActividadAcreedor,
			dl.AccusationConcept AS ConceptoAcreencia,
			dl.FinancialInstrument AS medicionPosterior,
			IIF(ap.Age <= 0, ap.Balance, 0) AS CxPNoVencidas,
			IIF(ap.Age > 0 AND ap.Age <= 30, ap.Balance, 0) as CxPMora30dias,
			IIF(ap.Age > 30 AND ap.Age <= 60, ap.Balance, 0) as CxPMora60dias,
			IIF(ap.Age > 60 AND ap.Age <= 90, ap.Balance, 0) as CxPMora90dias,
			IIF(ap.Age > 90 AND ap.Age <= 180, ap.Balance, 0) as CxPMora180dias,
			IIF(ap.Age > 180 AND ap.Age <= 360, ap.Balance, 0) as CxPMora360dias,
			IIF(ap.Age > 360, ap.Balance, 0) as CxPMoraMayor360dias
		FROM 
		(
			SELECT 
				ap.Id,
				ap.IdThirdParty,
				ap.IdSuppliersDistributionLines,
				DATEDIFF(DAY, ap.BillDate, @ClosingDate) AS Age,
				(
					ap.Value --Valor Inicial
					- (ap.Value - ISNULL(ibap.Balance, ap.Value)) --Valor saldo inicial
					- ISNULL(pn.DebitValue, 0) + ISNULL(pn.CreditValue, 0) --Valor Notas
					- ISNULL(pt.TransferValue, 0) --Valor Cruce de Anticipo
					- ISNULL(vt.VoucherTransactionValue, 0) --Valor comprobante de egreso
					- ISNULL(cr.CashReceiptValue, 0) --Valor recibos de caja
					- ISNULL(ca.CrossingValue, 0) --Valor cruzado con una cxp
				) AS Balance
			FROM Payments.AccountPayable ap WITH (NOLOCK)
			LEFT JOIN Payments.InitialBalanceAccountPayable ibap WITH (NOLOCK) ON ap.Id = ibap.AccountPayableId
			LEFT JOIN
			(
				SELECT
					pnapa.AccountPayableId,
					SUM(IIF(pn.Nature = 1, pnapa.AdjusmentValue, 0)) DebitValue,
					SUM(IIF(pn.Nature = 1, 0, pnapa.AdjusmentValue)) CreditValue
				FROM Payments.PaymentNotes pn WITH (NOLOCK)
				JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH (NOLOCK) ON pn.Id = pnapa.PaymentNoteId
				WHERE pn.Status = 2
					AND CAST(pn.NoteDate AS DATE) <= @ClosingDate
				GROUP BY pnapa.AccountPayableId
			) pn ON ap.Id = pn.AccountPayableId
			LEFT JOIN
			(
				SELECT
					ptd.AccountPayableId,
					SUM(ptd.Value) TransferValue
				FROM Payments.PaymentTransfer pt WITH (NOLOCK)
				JOIN Payments.PaymentTransferDetail ptd WITH (NOLOCK) ON pt.Id = ptd.PaymentTransferId
				WHERE pt.Status = 2
					AND CAST(pt.DocumentDate AS DATE) <= @ClosingDate
				GROUP BY ptd.AccountPayableId
			) pt ON ap.Id = pt.AccountPayableId
			LEFT JOIN
			(
				SELECT 
					db.IdAccountPayable,
					SUM(db.AdvancedValue) VoucherTransactionValue
				FROM Treasury.VoucherTransaction vt WITH (NOLOCK)
				JOIN Treasury.VoucherTransactionDetails vtd WITH (NOLOCK) ON vt.Id = vtd.IdVoucherTransaction
				JOIN Treasury.DischargeBill db WITH (NOLOCK) ON vtd.Id = db.IdVoucherTransactionD
				WHERE vt.Status IN (2 , 4)
					AND CAST(vt.DocumentDate AS DATE) <= @ClosingDate
					AND CAST(ISNULL(vt.ReversedDate, @AfterClosingDate) AS DATE) > @ClosingDate
				GROUP BY db.IdAccountPayable
			) vt ON ap.Id = vt.IdAccountPayable
			LEFT JOIN
			(
				SELECT 
					crdap.AccountPayableId,
					SUM(crdap.RefundValue) CashReceiptValue
				FROM Treasury.CashReceipts cr WITH (NOLOCK)
				JOIN Treasury.CashReceiptDetails crd WITH (NOLOCK) ON cr.Id = crd.IdCashReceipt
				JOIN Treasury.CashReceiptDetailAccountPayable crdap WITH (NOLOCK) ON crd.Id = crdap.CashReceiptDetailId
				WHERE cr.Status IN (2 , 4)
					AND CAST(cr.DocumentDate AS DATE) <= @ClosingDate
					AND CAST(ISNULL(cr.ReversedDate, @AfterClosingDate) AS DATE) > @ClosingDate
				GROUP BY crdap.AccountPayableId
			) cr ON ap.Id = cr.AccountPayableId
			LEFT JOIN
			(
				SELECT 
					cad.AccountPayableId,
					SUM(cad.CrossingValue) CrossingValue
				FROM Treasury.CrossingAccount ca WITH (NOLOCK)
				JOIN Treasury.CrossingAccountDetailCxP cad WITH (NOLOCK) ON ca.Id = cad.CrossingAccountId
				WHERE CAST(ca.DocumentDate AS DATE) <= @ClosingDate
					AND ca.Status = 2
				GROUP BY cad.AccountPayableId
			) ca ON ap.Id = ca.AccountPayableId
			WHERE ap.Status = 2
				AND CAST(ap.BillDate AS DATE) <= @ClosingDate
		) AS ap
		INNER JOIN Common.ThirdParty AS ct ON ct.Id = ap.IdThirdParty
		INNER JOIN Common.Person AS cp ON cp.id = ct.PersonId
		LEFT JOIN Common.SuppliersDistributionLines AS sdl ON sdl.id = ap.IdSuppliersDistributionLines
		LEFT JOIN Common.DistributionLines AS dl ON dl.id = sdl.IdDistributionLine
		WHERE ap.Balance <> 0
	) AS Info
	GROUP BY Info.TipoIdAcreedor, Info.IdAcreedor, Info.DvAcreedor, Info.NombreAcreedor, Info.ActividadAcreedor, Info.ConceptoAcreencia, Info.medicionPosterior

END