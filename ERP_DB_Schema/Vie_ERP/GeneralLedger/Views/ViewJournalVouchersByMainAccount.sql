
CREATE VIEW [GeneralLedger].[ViewJournalVouchersByMainAccount] AS
	SELECT	
			jv.Id AS JournalVouchersId,
			jv.LegalBookId,
			jvt.Code JournalVoucherTypeCode,
			jvt.Name JournalVoucherTypeName, 		
			jv.Consecutive, 
			jv.VoucherDate,
			en.Description EntityName,
			jv.EntityCode,
			jv.Detail,
			ISNULL(jvd.Value, 0) Value,
			CASE jv.Status
				WHEN 1 THEN 'Registrado'
				WHEN 2 THEN 'Confirmado'
				WHEN 3 THEN 'Anulado'
			END StatusName,	
			jvd.IdMainAccount
	FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
	JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
	LEFT JOIN Common.GetEntityNameDescriptions() en ON jv.EntityName = en.EntityName
	LEFT JOIN 
	(
		SELECT IdAccounting, SUM(DebitValue) value,IdMainAccount
		FROM GeneralLedger.JournalVoucherDetails WITH (NOLOCK)
		GROUP BY IdAccounting,IdMainAccount
	) jvd ON jv.Id = jvd.IdAccounting
	WHERE en.EntityName NOT IN ( 'AccountPayable', 'PaymentNotes','PaymentTransfer','VoucherTransaction','CashReceipts','CrossingAccount')