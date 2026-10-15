



CREATE VIEW [Treasury].[VReportCashBook]
AS

WITH Cte_temp as (	SELECT top 1
						c.Id CurrencyId, 
						c.Abbreviation as CurrencyAbbreviation 
					from GeneralLedger.CompanySettings cs with(NOLOCK)
					JOIN Common.Currency c WITH(NOLOCK) ON c.Id= cs.OfficialCurrencyId)

SELECT	v.Row,
		v.NameVoucher,
		v.CashRegisterId,
		v.CashRegisterCode,
		v.CashRegisterName,
		v.CashRegisterStatus,		
		v.IdBankAccount,
		v.BankAccountCode,
		v.BankAccountName,
		v.BankAccountStatus,
		v.Number,
		v.ThirdPartyNit,
		v.ThirdPartyName,
		v.Code,
		v.DocumentDate,
		v.Detail,
		v.VoucherType,
		v.PaymentMethod,
		IIF(v.Nature = 1, v.Value, 0) AS ValueDebit,
		IIF(v.Nature = 1, 0, v.Value) AS ValueCredit,
		v.Status,
		v.UserCode,
		v.CodeNameUser,
		ISNULL(v.CurrencyId,cte.CurrencyId) AS CurrencyId,
		ISNULL(v.CurrencyAbbreviation,cte.CurrencyAbbreviation) AS CurrencyAbbreviation
FROM [Treasury].[VReportTreasuryNewsletterCash] v WITH (NOLOCK)
JOIN Cte_temp cte on 1=1