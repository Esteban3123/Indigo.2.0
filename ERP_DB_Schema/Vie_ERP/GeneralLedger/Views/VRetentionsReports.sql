

CREATE VIEW [GeneralLedger].[VRetentionsReports] AS 
(
	SELECT
		jvd.Id
	   ,CAST(jv.VoucherDate AS DATE) AS VoucherDate
	   ,ma.Number
	   ,ma.Name AS Cuenta
	   ,t.Nit
	   ,t.Name AS Tercero
	   ,jvd.DebitValue
	   ,jvd.CreditValue
	   ,jvd.Detail
	   ,ISNULL(rc.Name, '') AS Retencion
	   ,ISNULL(jvd.RetentionRate, 0) AS RetentionRate
	   ,ISNULL(CASE ISNULL(jvd.RetentionRate, 0)
			WHEN 0 THEN CAST(jvd.BaseValue AS BIGINT)
			WHEN 1 THEN CAST(jvd.BaseValue AS BIGINT)
			ELSE CAST(ROUND((jvd.CreditValue * 100 / jvd.RetentionRate), 1) AS BIGINT)
		END, 0) AS Base
	   ,CASE jv.Status
			WHEN 1 THEN 'Registrado'
			WHEN 2 THEN 'Confirmado'
			WHEN 3 THEN 'Anulado'
		END Estado
	   ,IdRetention = ISNULL(jvd.IdRetention, 0)
	   ,CodeNameVoucherType = JVT.Code + ' ' + JVT.Name
	   ,jv.Consecutive
	   ,OriginDocument =
		CASE JV.EntityName
			WHEN 'AccountPayable' THEN 'Cuenta por Pagar'
			WHEN 'BasicBilling' THEN 'Factura básica'
			WHEN 'CashReceipts' THEN 'Recibo de caja'
			WHEN 'PaymentNotes' THEN 'Nota'
			WHEN 'PayrollLiquidation' THEN 'Liquidación de nomina'
			WHEN 'PortfolioNote' THEN 'Nota'
			WHEN 'TreasuryNote' THEN 'Nota'
			WHEN 'VoucherTransaction' THEN 'Egreso'
			WHEN 'PortfolioTransfer' THEN 'Cruce de anticipo vs CxC'
			WHEN 'PaymentTransfer' THEN 'Cruce de anticipo vs CxP'
			WHEN 'PortfolioReclassification' THEN 'Reclasificación documento de cartera'
			ELSE JVT.NAME
		END
		+ ' ' + ISNULL(jv.EntityCode, '')
	FROM GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK)
	JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.Id = jvd.IdMainAccount
	JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON ma.LegalBookId = lb.Id AND lb.OfficialBook = 1
	JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
	INNER JOIN GeneralLedger.JournalVoucherTypes JVT WITH (NOLOCK) ON JVT.ID = JV.IdJournalVoucher
	LEFT JOIN Common.ThirdParty t WITH (NOLOCK) ON t.Id = jvd.IdThirdParty
	LEFT JOIN GeneralLedger.RetentionConcepts rc WITH (NOLOCK) ON rc.Id = jvd.IdRetention
	WHERE ma.AllowsMovement = 1 
		AND jv.Status = 2 
		AND ma.RetencionType <> 0
)