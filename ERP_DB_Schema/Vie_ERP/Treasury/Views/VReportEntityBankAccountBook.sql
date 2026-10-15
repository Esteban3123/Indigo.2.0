CREATE VIEW [Treasury].[VReportEntityBankAccountBook]
AS
SELECT	v.Row,
		v.NameVoucher,
		v.EntityBankId AS EntityBankAccountId,
		v.EntityBankAccountCode,
		v.EntityBankAccountName,
		v.Type,
		v.EntityBankAccountNumber,
		v.EntityBankStatus,
		v.Number,
		v.ThirdPartyNit,
		v.ThirdPartyName,
		v.Code,
		v.DocumentDate,
		v.Detail,
		v.VoucherType,
		IIF(v.Nature = 1, v.Value, 0) AS ValueDebit,
		IIF(v.Nature = 1, 0, v.Value) AS ValueCredit,
		v.Status
FROM [Treasury].[VReportTreasuryNewsletterEntityBankAccount] v WITH (NOLOCK)