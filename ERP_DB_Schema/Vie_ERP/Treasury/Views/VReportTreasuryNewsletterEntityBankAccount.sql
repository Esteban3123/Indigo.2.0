

CREATE VIEW [Treasury].[VReportTreasuryNewsletterEntityBankAccount]
AS
SELECT	CONCAT(Datos.NameVoucher,'-',Datos.EntityBankId,'-',Datos.EntityBankAccountNumber,'-',Datos.Code) AS Row
		,*
FROM 
(
	SELECT	'Recibo C.' AS NameVoucher
			,EB.Id AS EntityBankId
			,EB.Code AS EntityBankAccountCode
			,B.Name AS EntityBankAccountName
			,EB.Type
			,EB.Number AS EntityBankAccountNumber
			,EB.Status AS EntityBankStatus
			,MA.Number
			,TP.Nit AS ThirdPartyNit
			,TP.Name AS ThirdPartyName
			,CRC.Code AS Code
			,CRC.DocumentDate AS DocumentDate
			,0 AS CheckNumber
			,CRC.Detail AS Detail
			,0 AS VoucherClass
			,1 AS VoucherType
			,1 AS Nature
			,SUM(pm.Value) AS Value
			,CRC.Status AS Status
			,C.Abbreviation CurrencyAbbreviation
	FROM Treasury.CashReceipts CRC WITH (NOLOCK)
	INNER JOIN Treasury.PaymentMethods pm ON pm.IdCashReceipt = CRC.Id
	INNER JOIN Common.ThirdParty TP WITH (NOLOCK) ON TP.Id = CRC.IdThirdParty
	INNER JOIN Treasury.EntityBankAccounts EB WITH (NOLOCK) ON EB.Id = CRC.IdBankAccount
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id = EB.IdMainAccount
	INNER JOIN Payroll.Bank B WITH (NOLOCK) ON B.Id = EB.IdBank
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.Id = EB.CurrencyId
	GROUP BY 
		EB.Id,EB.Code,B.Name,EB.Type,EB.Number,EB.Status,MA.Number
		,TP.Nit,TP.Name
		,CRC.Code,CRC.DocumentDate,CRC.Detail,CRC.Status,C.Abbreviation
UNION ALL
	SELECT	'Consignación' AS NameVoucher
			,EB.Id AS EntityBankId
			,EB.Code AS EntityBankAccountCode
			,B.Name AS EntityBankAccountName
			,EB.Type
			,EB.Number AS EntityBankAccountNumber
			,EB.Status AS EntityBankStatus
			,MA.Number
			,NULL AS ThirdPartyNit
			,NULL AS ThirdPartyName
			,CN.Code AS Code
			,CN.DocumentDate AS DocumentDate
			,0 AS CheckNumber
			,CN.Description AS Detail
			,0 AS VoucherClass
			,1 AS VoucherType
			,1 AS Nature
			,CN.Value AS Value
			,CN.Status AS Status
			,C.Abbreviation CurrencyAbbreviation
	FROM Treasury.Consignment CN WITH (NOLOCK)
	INNER JOIN Treasury.EntityBankAccounts EB WITH (NOLOCK) ON EB.Id = CN.EntityBankAccountId
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id = EB.IdMainAccount
	INNER JOIN Payroll.Bank B WITH (NOLOCK) ON B.Id = EB.IdBank
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.Id = EB.CurrencyId
UNION ALL
	SELECT	'Fondo Caja M' AS NameVoucher
			,EB.Id AS EntityBankId
			,EB.Code AS EntityBankAccountCode
			,B.Name AS EntityBankAccountName
			,EB.Type
			,EB.Number AS EntityBankAccountNumber
			,EB.Status AS EntityBankStatus
			,MA.Number
			,NULL AS ThirdPartyNit
			,NULL AS ThirdPartyName
			,CCS.Code AS Code
			,CCS.DocumentDate AS DocumentDate
			,0 AS CheckNumber
			,IIF(CCS.DocumentType = 1, 'Aumento de Caja Menor', 'Disminución de Caja Menor') + ' desde ' + IIF(CCS.SourceType = 1, 'Caja Mayor', 'Cuenta Bancaria') AS Detail
			,0 AS VoucherClass
			,IIF(CCS.DocumentType = 1, 2, 1) AS VoucherType
			,IIF(CCS.DocumentType = 1, 2, 1) AS Nature
			,CCS.Value AS Value
			,CCS.Status AS Status
			,C.Abbreviation CurrencyAbbreviation
	FROM Treasury.ConstitutionCashSmaller CCS WITH (NOLOCK)
	INNER JOIN Treasury.EntityBankAccounts EB WITH (NOLOCK) ON EB.Id = CCS.EntityBankAccountId
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id = EB.IdMainAccount
	INNER JOIN Payroll.Bank B WITH (NOLOCK) ON B.Id = EB.IdBank
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.Id = EB.CurrencyId
UNION ALL
	SELECT	'Comp. E.' AS NameVoucher
			,EB.Id AS EntityBankId
			,EB.Code AS EntityBankAccountCode
			,B.Name AS EntityBankAccountName
			,EB.Type
			,EB.Number AS EntityBankAccountNumber
			,EB.Status AS EntityBankStatus
			,MA.Number
			,TP.Nit AS ThirdPartyNit
			,TP.Name AS ThirdPartyName
			,VT.Code AS Code
			,VT.DocumentDate AS DocumentDate
			,VT.CheckNumber AS CheckNumber
			,VT.Detail AS Detail
			,VT.VoucherClass AS VoucherClass
			,2 AS VoucherType
			,2 AS Nature
			,VT.Value AS Value
			,VT.Status AS Status
			,c.Abbreviation CurrencyAbbreviation
	FROM Treasury.VoucherTransaction VT WITH (NOLOCK)
	INNER JOIN Treasury.EntityBankAccounts EB WITH (NOLOCK) ON EB.Id = VT.IdEntityBankAccount
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id = EB.IdMainAccount
	INNER JOIN Payroll.Bank B WITH (NOLOCK) ON B.Id = EB.IdBank
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.Id = VT.CurrencyId
	LEFT JOIN Common.ThirdParty TP WITH (NOLOCK) ON TP.Id = VT.IdThirdParty
UNION ALL
	SELECT	'Comp. E - Tras.' AS NameVoucher
			,EB.Id AS EntityBankId
			,EB.Code AS EntityBankAccountCode
			,B.Name AS EntityBankAccountName
			,EB.Type
			,EB.Number AS EntityBankAccountNumber
			,EB.Status AS EntityBankStatus
			,MA.Number
			,TP.Nit AS ThirdPartyNit
			,TP.Name AS ThirdPartyName
			,VT.Code AS Code
			,VT.DocumentDate AS DocumentDate
			,VT.CheckNumber AS CheckNumber
			,VTD.Detail AS Detail
			,VT.VoucherClass AS VoucherClass
			,1 AS VoucherType
			,1 AS Nature
			,VTD.Value AS Value			
			,VT.Status AS Status
			,c.Abbreviation CurrencyAbbreviation
	FROM Treasury.VoucherTransactionDetails VTD WITH (NOLOCK)
	INNER JOIN Treasury.VoucherTransaction VT WITH (NOLOCK) ON VT.Id = VTD.IdVoucherTransaction
	INNER JOIN Treasury.EntityBankAccounts EB WITH (NOLOCK) ON EB.Id = VTD.IdEntityBankAccount
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id = EB.IdMainAccount
	INNER JOIN Payroll.Bank B WITH (NOLOCK) ON B.Id = EB.IdBank
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.Id =VT.CurrencyId
	LEFT JOIN Common.ThirdParty TP WITH (NOLOCK) ON TP.Id = VTD.IdThirdParty
UNION ALL
	SELECT	'Nota' AS NameVoucher
			,EB.Id AS EntityBankId
			,EB.Code AS EntityBankAccountCode
			,B.Name AS EntityBankAccountName
			,EB.Type
			,EB.Number AS EntityBankAccountNumber
			,EB.Status AS EntityBankStatus
			,MA.Number
			,NULL AS ThirdPartyNit
			,NULL AS ThirdPartyName
			,TN.Code AS Code
			,TN.NoteDate AS DocumentDate
			,0 AS CheckNumber
			,TN.Description AS Detail
			,0 AS VoucherClass
			,TN.Nature AS VoucherType
			,TN.Nature
			,TN.Value AS Value
			,TN.Status AS Status
			,c.Abbreviation CurrencyAbbreviation
	FROM Treasury.TreasuryNote TN WITH (NOLOCK)
	INNER JOIN Treasury.EntityBankAccounts EB WITH (NOLOCK) ON EB.Id = TN.EntityBankAccountId
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id = EB.IdMainAccount
	INNER JOIN Payroll.Bank B WITH (NOLOCK) ON B.Id = EB.IdBank
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.Id = EB.CurrencyId
	WHERE TN.NoteType = 1
UNION ALL
	SELECT	IIF(TN.NoteType = 4,'Nota Rev RC', 'Nota Dev RC') AS NameVoucher
			,EB.Id AS EntityBankId
			,EB.Code AS EntityBankAccountCode
			,B.Name AS EntityBankAccountName
			,EB.Type
			,EB.Number AS EntityBankAccountNumber
			,EB.Status AS EntityBankStatus
			,MA.Number
			,TP.Nit AS ThirdPartyNit
			,TP.Name AS ThirdPartyName
			,TN.Code AS Code
			,TN.NoteDate AS DocumentDate
			,0 AS CheckNumber
			,TN.Description AS Detail
			,0 AS VoucherClass
			,2 AS VoucherType
			,IIF(tn.NoteType = 4,2, NULL) Nature
			,SUM(PM.Value) AS Value
			,TN.Status AS Status
			,c.Abbreviation CurrencyAbbreviation
	FROM Treasury.TreasuryNote TN WITH (NOLOCK)
	JOIN Treasury.CashReceipts CR WITH (NOLOCK) ON CR.Id = TN.CashReceiptId
	JOIN Treasury.PaymentMethods pm WITH (NOLOCK) ON CR.Id = pm.IdCashReceipt
	JOIN Treasury.EntityBankAccounts EB WITH (NOLOCK) ON EB.Id = ISNULL(pm.IdEntityBankAccount, cr.IdBankAccount)
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id = EB.IdMainAccount
	INNER JOIN Payroll.Bank B WITH (NOLOCK) ON B.Id = EB.IdBank
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.Id = EB.CurrencyId
	LEFT JOIN Common.ThirdParty TP WITH (NOLOCK) ON TP.Id = CR.IdThirdParty
	WHERE TN.NoteType = 4 OR tn.NoteType = 7
	GROUP BY 
		EB.Id,EB.Code,B.Name,EB.Type,EB.Number,EB.Status,MA.Number,
		TP.Nit,TP.Name, 
		TN.Code,TN.NoteDate,TN.Description,TN.Status,c.Abbreviation, tn.NoteType
UNION ALL
	SELECT	'Nota Dev Consignación' AS NameVoucher
			,EB.Id AS EntityBankId
			,EB.Code AS EntityBankAccountCode
			,B.Name AS EntityBankAccountName
			,EB.Type
			,EB.Number AS EntityBankAccountNumber
			,EB.Status AS EntityBankStatus
			,MA.Number
			,NULL AS ThirdPartyNit
			,NULL AS ThirdPartyName
			,TN.Code AS Code
			,TN.NoteDate AS DocumentDate
			,0 AS CheckNumber
			,TN.Description AS Detail
			,0 AS VoucherClass
			,2 AS VoucherType
			,2 AS Nature
			,CN.Value AS Value
			,TN.Status AS Status
			,c.Abbreviation CurrencyAbbreviation
	FROM Treasury.TreasuryNote TN WITH (NOLOCK)
	JOIN Treasury.Consignment CN WITH (NOLOCK) ON TN.ConsignmentId = CN.Id
	JOIN Treasury.EntityBankAccounts EB WITH (NOLOCK) ON CN.EntityBankAccountId = EB.Id
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id = EB.IdMainAccount
	INNER JOIN Payroll.Bank B WITH (NOLOCK) ON B.Id = EB.IdBank
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.Id = EB.CurrencyId
	WHERE TN.NoteType = 5
UNION ALL
	SELECT	'Nota Dev CE' AS NameVoucher
			,EB.Id AS EntityBankId
			,EB.Code AS EntityBankAccountCode
			,B.Name AS EntityBankAccountName
			,EB.Type
			,EB.Number AS EntityBankAccountNumber
			,EB.Status AS EntityBankStatus
			,MA.Number
			,TP.Nit AS ThirdPartyNit
			,TP.Name AS ThirdPartyName
			,TN.Code AS Code
			,TN.NoteDate AS DocumentDate
			,0 AS CheckNumber
			,TN.Description AS Detail
			,0 AS VoucherClass
			,1 AS VoucherType
			,1 AS Nature
			,TN.Value AS Value
			,TN.Status AS Status
			,c.Abbreviation CurrencyAbbreviation
	FROM Treasury.TreasuryNote TN WITH (NOLOCK)
	INNER JOIN Treasury.VoucherTransaction VT WITH (NOLOCK) ON VT.Id = TN.VoucherTransactionId
	INNER JOIN Treasury.EntityBankAccounts EB WITH (NOLOCK) ON EB.Id = VT.IdEntityBankAccount
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id = EB.IdMainAccount
	INNER JOIN Payroll.Bank B WITH (NOLOCK) ON B.Id = EB.IdBank
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.Id = VT.CurrencyId
	LEFT JOIN Common.ThirdParty TP ON TP.Id = VT.IdThirdParty
	WHERE TN.NoteType = 3
UNION ALL
	SELECT	'Nota Dev CE - Tras.' AS NameVoucher
			,EB.Id AS EntityBankId
			,EB.Code AS EntityBankAccountCode
			,B.Name AS EntityBankAccountName
			,EB.Type
			,EB.Number AS EntityBankAccountNumber
			,EB.Status AS EntityBankStatus
			,MA.Number
			,TP.Nit AS ThirdPartyNit
			,TP.Name AS ThirdPartyName
			,TN.Code AS Code
			,TN.NoteDate AS DocumentDate
			,0 AS CheckNumber
			,TN.Description AS Detail
			,0 AS VoucherClass
			,2 AS VoucherType
			,2 AS Nature
			,VTD.Value AS Value
			,TN.Status AS Status
			,c.Abbreviation CurrencyAbbreviation
	FROM Treasury.TreasuryNote TN WITH (NOLOCK)
	INNER JOIN Treasury.VoucherTransaction VT WITH (NOLOCK) ON VT.Id = TN.VoucherTransactionId
	INNER JOIN Treasury.VoucherTransactionDetails VTD WITH (NOLOCK) ON VT.Id = VTD.IdVoucherTransaction
	INNER JOIN Treasury.EntityBankAccounts EB WITH (NOLOCK) ON EB.Id = VTD.IdEntityBankAccount
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id = EB.IdMainAccount
	INNER JOIN Payroll.Bank B WITH (NOLOCK) ON B.Id = EB.IdBank
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.Id = VT.CurrencyId
	LEFT JOIN Common.ThirdParty TP WITH (NOLOCK) ON TP.Id = VTD.IdThirdParty
	WHERE TN.NoteType = 3
) AS Datos