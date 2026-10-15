
CREATE VIEW [Treasury].[VReportTreasuryNewsletterSummary]
AS
SELECT	Row,
		CashRegisterId, CashRegisterCode, CashRegisterName as NameCash, CashRegisterStatus,
		EntityBankAccountId, EntityBankAccountCode, EntityBankAccountName as Name, [Type], EntityBankAccountNumber as Number,EntityBankStatus,
		Code, DocumentDate, Number AS MainAccount, ValueDebit AS SumReceipts, ValueCredit AS SumExpenditures, Status,CurrencyAbbreviation
FROM
(
	SELECT	Row,
			CashRegisterId,CashRegisterCode,CashRegisterName,CashRegisterStatus,
			0 AS EntityBankAccountId,'' AS EntityBankAccountCode,'' AS EntityBankAccountName, 0 Type,'' AS EntityBankAccountNumber, CashRegisterStatus EntityBankStatus,
			Code,DocumentDate,Number,Status,
			IIF(Nature = 1, Value, 0) ValueDebit, IIF(Nature = 1, 0, Value) ValueCredit,CurrencyAbbreviation
	FROM [Treasury].[VReportTreasuryNewsletterCash]
	WHERE Status IN (2, 4)
UNION ALL
	SELECT	Row,
			0 AS CashRegisterId,'' AS CashRegisterCode,'' AS CashRegisterName, EntityBankStatus CashRegisterStatus,
			EntityBankId AS EntityBankAccountId,EntityBankAccountCode,EntityBankAccountName,Type,EntityBankAccountNumber,EntityBankStatus,
			Code,DocumentDate,Number,Status,
			IIF(Nature = 1, Value, 0) ValueDebit, IIF(Nature = 1, 0, Value) ValueCredit,CurrencyAbbreviation
	FROM [Treasury].[VReportTreasuryNewsletterEntityBankAccount]
	WHERE Status IN (2, 4)
) AS Datos