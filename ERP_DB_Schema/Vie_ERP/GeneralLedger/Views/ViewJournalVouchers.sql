CREATE VIEW [GeneralLedger].[ViewJournalVouchers]
AS
	SELECT	jv.Id,
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
			END StatusName		
	FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
	JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
	LEFT JOIN Common.GetEntityNameDescriptions() en ON jv.EntityName = en.EntityName
	LEFT JOIN 
	(
		SELECT IdAccounting, SUM(DebitValue) Value
		FROM GeneralLedger.JournalVoucherDetails WITH (NOLOCK)
		GROUP BY IdAccounting
	) jvd ON jv.Id = jvd.IdAccounting