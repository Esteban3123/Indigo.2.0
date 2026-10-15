CREATE VIEW [Treasury].[VReportTreasuryNewsletterCash]
AS
SELECT	CONCAT(Datos.NameVoucher,'-',Datos.CashRegisterId,'-',IdBankAccount, '-',Datos.Code,'-',datos.PaymentMethod,'-',Datos.UserIntId) AS Row
		,*
FROM 
(
	SELECT	'Recibo C.' AS NameVoucher
			,CR.Id AS CashRegisterId
			,CR.Code AS CashRegisterCode
			,CR.Name AS CashRegisterName
			,CR.Status CashRegisterStatus
			,EBA.Id AS IdBankAccount
			,EBA.Code AS BankAccountCode
			,EBA.Name AS BankAccountName
			,EBA.Status BankAccountStatus
			,MA.Number
			,TP.Nit AS ThirdPartyNit
			,TP.Name AS ThirdPartyName
			,CRC.Code AS Code
			,CRC.DocumentDate AS DocumentDate
			,0 AS CheckNumber
			,CRC.Detail AS Detail
			,0 AS VoucherClass
			,1 AS VoucherType
			,ISNULL(PM.PaymentMethodTypes, 1) AS PaymentMethod
			,1 AS Nature
			,SUM(ISNULL(PM.Value, CRC.Value)) AS Value
			,CRC.Status AS Status
			,CRC.CreationUser AS UserCode
			,concat(p.Fullname,'-',CRC.CreationUser) CodeNameUser
			,c.Id CurrencyId
			,c.Abbreviation CurrencyAbbreviation
			,u.id as UserIntId
	FROM Treasury.CashReceipts CRC
	INNER JOIN Common.ThirdParty TP WITH(NOLOCK) ON TP.Id = CRC.IdThirdParty
	LEFT JOIN Treasury.CashRegisters CR WITH(NOLOCK) ON CR.Id = CRC.IdCashRegister
	LEFT JOIN(	SELECT EBA.Id,EBA.Code,EBA.Number,EBA.IdMainAccount,B.Name,EBA.Status,EBA.CurrencyId
				FROM Treasury.EntityBankAccounts EBA WITH(NOLOCK)
				JOIN Payroll.Bank B WITH(NOLOCK) on EBA.IdBank=B.Id) EBA ON EBA.Id=CRC.IdBankAccount	
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id = ISNULL(CR.IdMainAccount,EBA.IdMainAccount)
	INNER JOIN Treasury.PaymentMethods PM WITH (NOLOCK) ON CRC.Id = PM.IdCashReceipt
	LEFT JOIN Security.[UserInt] u  ON CRC.CreationUser =u.UserCode 
	LEFT JOIN Security.PersonInt p  ON u.IdPerson=p.Id
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.id= isnull(CR.CurrencyId,EBA.CurrencyId)
		group by CR.Id 
			,CR.Code
			,CR.Name
			,CR.Status
			,EBA.Id 
			,EBA.Code
			,EBA.Name
			,EBA.Status
			,MA.Number
			,TP.Nit 
			,TP.Name 
			,CRC.Code 
			,CRC.DocumentDate 
			,CRC.Detail
			,ISNULL(PM.PaymentMethodTypes, 1)
			,CRC.Status
			,CRC.CreationUser
			,concat(p.Fullname,'-',CRC.CreationUser)
			,c.Id
			,c.Abbreviation
			,u.id
UNION ALL
	SELECT	'Consignación' AS NameVoucher
			,CR.Id AS CashRegisterId
			,CR.Code AS CashRegisterCode
			,CR.Name AS CashRegisterName
			,CR.Status CashRegisterStatus
			,NULL AS IdBankAccount
			,NULL AS BankAccountCode
			,NULL AS BankAccountName
			,NULL AS BankAccountStatus
			,MA.Number
			,NULL AS ThirdPartyNit
			,NULL AS ThirdPartyName
			,CN.Code AS Code
			,CN.DocumentDate AS DocumentDate
			,0 AS CheckNumber
			,CN.Description AS Detail
			,0 AS VoucherClass
			,2 AS VoucherType
			,4 AS PaymentMethod
			,2 AS Nature
			,CD.Value AS Value
			,CN.Status AS Status
			,CN.CreationUser AS UserCode
			,concat(p.Fullname,'-',CN.CreationUser) CodeNameUser
			,c.Id CurrencyId
			,c.Abbreviation CurrencyAbbreviation
			,u.id as UserIntId
	FROM Treasury.ConsignmentDetail CD WITH (NOLOCK)
	INNER JOIN Treasury.Consignment CN WITH (NOLOCK) ON CN.Id = CD.ConsignmentTransferId
	INNER JOIN Treasury.CashRegisters CR WITH (NOLOCK) ON CR.Id = CD.CashRegisterId
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id = CR.IdMainAccount
	LEFT JOIN Security.[UserInt] u  ON CN.CreationUser =u.UserCode 
	LEFT JOIN Security.PersonInt p  ON u.IdPerson=p.Id
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.id= CR.CurrencyId

UNION ALL
	SELECT	'Fondo Caja M' AS NameVoucher
			,CR.Id AS CashRegisterId
			,CR.Code AS CashRegisterCode
			,CR.Name AS CashRegisterName
			,CR.Status CashRegisterStatus
			,EBA.Id AS IdBankAccount
			,EBA.Code AS BankAccountCode
			,EBA.Name AS BankAccountName
			,EBA.Status BankAccountStatus
			,MA.Number
			,NULL AS ThirdPartyNit
			,NULL AS ThirdPartyName
			,CCS.Code AS Code
			,CCS.DocumentDate AS DocumentDate
			,0 AS CheckNumber
			,IIF(CCS.DocumentType = 1, 'Aumento de Caja Menor', 'Disminución de Caja Menor') + ' desde ' + IIF(CCS.SourceType = 1, 'Caja Mayor', 'Cuenta Bancaria') AS Detail
			,0 AS VoucherClass
			,IIF(CCS.DocumentType = 1, 2, 1) AS VoucherType
			,5 AS PaymentMethod
			,IIF(CCS.DocumentType = 1, 2, 1) AS Nature
			,CCS.Value AS Value
			,CCS.Status AS Status
			,CCS.CreationUser AS UserCode
			,concat(p.Fullname,'-',CCS.CreationUser) CodeNameUser
			,c.Id CurrencyId
			,c.Abbreviation CurrencyAbbreviation
			,u.id as UserIntId
	FROM Treasury.ConstitutionCashSmaller CCS WITH (NOLOCK)
	LEFT JOIN Treasury.CashRegisters CR WITH (NOLOCK) ON CR.Id = CCS.CashRegisterId
	LEFT JOIN(	SELECT EBA.Id,EBA.Code,EBA.Number,EBA.IdMainAccount,B.Name,EBA.Status,EBA.CurrencyId
				FROM Treasury.EntityBankAccounts EBA WITH(NOLOCK)
				JOIN Payroll.Bank B WITH(NOLOCK) on EBA.IdBank=B.Id) EBA ON EBA.Id=CCS.EntityBankAccountId
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id = ISNULL(CR.IdMainAccount,EBA.IdMainAccount)
	LEFT JOIN Security.[UserInt] u ON CCS.CreationUser =u.UserCode 
	LEFT JOIN Security.PersonInt p  ON u.IdPerson=p.Id
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.id= isnull(CR.CurrencyId,EBA.CurrencyId)

UNION ALL
	SELECT	'Fondo Caja M' AS NameVoucher
			,CR.Id AS CashRegisterId
			,CR.Code AS CashRegisterCode
			,CR.Name AS CashRegisterName
			,CR.Status CashRegisterStatus
			,EBA.Id AS IdBankAccount
			,EBA.Code AS BankAccountCode
			,EBA.Name AS BankAccountName
			,EBA.Status BankAccountStatus
			,MA.Number
			,NULL AS ThirdPartyNit
			,NULL AS ThirdPartyName
			,CCS.Code AS Code
			,CCS.DocumentDate AS DocumentDate
			,0 AS CheckNumber
			,IIF(CCS.DocumentType = 1, 'Aumento de Caja Menor', 'Disminución de Caja Menor') + ' desde ' + IIF(CCS.SourceType = 1, 'Caja Mayor', 'Cuenta Bancaria') AS Detail
			,0 AS VoucherClass
			,IIF(CCS.DocumentType = 1, 1, 2) AS VoucherType
			,5 AS PaymentMethod
			,IIF(CCS.DocumentType = 1, 1, 2) AS Nature
			,CCS.Value AS Value
			,CCS.Status AS Status
			,CCS.CreationUser AS UserCode
			,concat(p.Fullname,'-',CCS.CreationUser) CodeNameUser
			,c.Id CurrencyId
			,c.Abbreviation CurrencyAbbreviation
			,u.id as UserIntId
	FROM Treasury.ConstitutionCashSmaller CCS WITH (NOLOCK)
	LEFT JOIN Treasury.CashRegisters CR WITH (NOLOCK) ON CR.Id = CCS.CashRegisterSmallerId
	LEFT JOIN(	SELECT EBA.Id,EBA.Code,EBA.Number,EBA.IdMainAccount,B.Name,EBA.Status,EBA.CurrencyId
				FROM Treasury.EntityBankAccounts EBA WITH(NOLOCK)
				JOIN Payroll.Bank B WITH(NOLOCK) on EBA.IdBank=B.Id) EBA ON EBA.Id=CCS.EntityBankAccountId
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id =ISNULL(CR.IdMainAccount,EBA.IdMainAccount)
	LEFT JOIN Security.[UserInt] u ON CCS.CreationUser =u.UserCode 
	LEFT JOIN Security.PersonInt p  ON u.IdPerson=p.Id
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.id= isnull(CR.CurrencyId,EBA.CurrencyId)

UNION ALL
	SELECT	'Comp. E.' AS NameVoucher
			,CR.Id AS CashRegisterId
			,CR.Code AS CashRegisterCode
			,CR.Name AS CashRegisterName
			,CR.Status CashRegisterStatus
			,EBA.Id AS IdBankAccount
			,EBA.Code AS BankAccountCode
			,EBA.Name AS BankAccountName
			,EBA.Status BankAccountStatus
			,MA.Number
			,TP.Nit AS ThirdPartyNit
			,TP.Name AS ThirdPartyName
			,VT.Code AS Code
			,VT.DocumentDate AS DocumentDate
			,VT.CheckNumber AS CheckNumber
			,VT.Detail AS Detail
			,VT.VoucherClass AS VoucherClass
			,2 AS VoucherType
			,CASE ISNULL(VT.PaymentMethod, 0)
				WHEN 0 THEN 1
				WHEN 1 THEN 2
				WHEN 2 THEN 4
			END AS PaymentMethod
			,2 AS Nature
			,VT.Value AS Value
			,VT.Status AS Status
			,VT.CreationUser AS UserCode
			,concat(p.Fullname,'-',VT.CreationUser) CodeNameUser
			,c.Id CurrencyId
			,c.Abbreviation CurrencyAbbreviation
			,u.id as UserIntId
	FROM Treasury.VoucherTransaction VT WITH (NOLOCK)
	LEFT JOIN Treasury.CashRegisters CR WITH (NOLOCK) ON CR.Id = VT.IdCashRegister
	LEFT JOIN(	SELECT EBA.Id,EBA.Code,EBA.Number,EBA.IdMainAccount,B.Name,EBA.Status,EBA.CurrencyId
				FROM Treasury.EntityBankAccounts EBA WITH(NOLOCK)
				JOIN Payroll.Bank B WITH(NOLOCK) on EBA.IdBank=B.Id) EBA ON EBA.Id=VT.IdEntityBankAccount
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id = ISNULL(CR.IdMainAccount,EBA.IdMainAccount)
	LEFT JOIN Security.[UserInt] u  ON VT.CreationUser =u.UserCode 
	LEFT JOIN Security.PersonInt p  ON u.IdPerson=p.Id
	LEFT JOIN Common.ThirdParty TP WITH (NOLOCK) ON TP.Id = VT.IdThirdParty
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.id= isnull(CR.CurrencyId,EBA.CurrencyId)

UNION ALL	
	SELECT	'Comp. E.' AS NameVoucher
			,CR.Id AS CashRegisterId
			,CR.Code AS CashRegisterCode
			,CR.Name AS CashRegisterName
			,CR.Status CashRegisterStatus
			,EBA.Id AS IdBankAccount
			,EBA.Code AS BankAccountCode
			,EBA.Name AS BankAccountName
			,EBA.Status BankAccountStatus
			,MA.Number
			,TP.Nit AS ThirdPartyNit
			,TP.Name AS ThirdPartyName
			,VT.Code AS Code
			,VT.DocumentDate AS DocumentDate
			,VT.CheckNumber AS CheckNumber
			,VTD.Detail AS Detail
			,VT.VoucherClass AS VoucherClass
			,1 AS VoucherType
			,1 AS PaymentMethod
			,1 AS Nature
			,VTD.Value AS Value
			,VT.Status AS Status
			,VT.CreationUser AS UserCode
			,concat(p.Fullname,'-',VT.CreationUser) CodeNameUser
			,c.Id CurrencyId
			,c.Abbreviation CurrencyAbbreviation
			,u.id as UserIntId
	FROM Treasury.VoucherTransactionDetails VTD WITH (NOLOCK)
	INNER JOIN Treasury.VoucherTransaction VT WITH (NOLOCK) ON VT.Id = VTD.IdVoucherTransaction
	LEFT JOIN Treasury.CashRegisters CR WITH (NOLOCK) ON CR.Id = VTD.CashRegisterId
	LEFT JOIN(	SELECT EBA.Id,EBA.Code,EBA.Number,EBA.IdMainAccount,B.Name,EBA.Status
				FROM Treasury.EntityBankAccounts EBA WITH(NOLOCK)
				JOIN Payroll.Bank B WITH(NOLOCK) on EBA.IdBank=B.Id) EBA ON EBA.Id=VTD.IdEntityBankAccount
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id = ISNULL(CR.IdMainAccount,EBA.IdMainAccount)
	LEFT JOIN Security.[UserInt] u  ON VT.CreationUser =u.UserCode 
	LEFT JOIN Security.PersonInt p  ON u.IdPerson=p.Id
	LEFT JOIN Common.ThirdParty TP WITH (NOLOCK) ON TP.Id = VTD.IdThirdParty
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.id= VT.CurrencyId

UNION ALL

	SELECT	'Nota' AS NameVoucher
			,CR.Id AS CashRegisterId
			,CR.Code AS CashRegisterCode
			,CR.Name AS CashRegisterName
			,CR.Status CashRegisterStatus
			,EBA.Id AS IdBankAccount
			,EBA.Code AS BankAccountCode
			,EBA.Name AS BankAccountName
			,EBA.Status BankAccountStatus
			,MA.Number
			,NULL AS ThirdPartyNit
			,NULL AS ThirdPartyName
			,TN.Code AS Code
			,TN.NoteDate AS DocumentDate
			,0 AS CheckNumber
			,TN.Description AS Detail
			,0 AS VoucherClass
			,TN.Nature AS VoucherType
			,5 AS PaymentMethod
			,TN.Nature
			,TN.Value AS Value
			,TN.Status AS Status
			,TN.CreationUser AS UserCode
			,concat(p.Fullname,'-',tn.CreationUser) CodeNameUser
			,c.Id CurrencyId
			,c.Abbreviation CurrencyAbbreviation
			,u.id as UserIntId
	FROM Treasury.TreasuryNote TN WITH (NOLOCK)
	LEFT JOIN Treasury.CashRegisters CR WITH (NOLOCK) ON CR.Id = TN.CashRegisterId
	LEFT JOIN(	SELECT EBA.Id,EBA.Code,EBA.Number,EBA.IdMainAccount,B.Name,EBA.Status,EBA.CurrencyId
				FROM Treasury.EntityBankAccounts EBA WITH(NOLOCK)
				JOIN Payroll.Bank B WITH(NOLOCK) on EBA.IdBank=B.Id) EBA ON EBA.Id=TN.EntityBankAccountId
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id = ISNULL(CR.IdMainAccount,EBA.IdMainAccount)
	LEFT JOIN Security.[UserInt] u  ON TN.CreationUser =u.UserCode 
	LEFT JOIN Security.PersonInt p  ON u.IdPerson=p.Id
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.id= ISNULL(CR.CurrencyId,EBA.CurrencyId)
	WHERE TN.NoteType = 2
UNION ALL
	SELECT	'Nota Rev RC' AS NameVoucher
			,CR.Id AS CashRegisterId
			,CR.Code AS CashRegisterCode
			,CR.Name AS CashRegisterName
			,CR.Status CashRegisterStatus
			,EBA.Id AS IdBankAccount
			,EBA.Code AS BankAccountCode
			,EBA.Name AS BankAccountName
			,EBA.Status BankAccountStatus
			,MA.Number
			,TP.Nit AS ThirdPartyNit
			,TP.Name AS ThirdPartyName
			,TN.Code AS Code
			,TN.NoteDate AS DocumentDate
			,0 AS CheckNumber
			,TN.Description AS Detail
			,0 AS VoucherClass
			,2 AS VoucherType
			,ISNULL(PM.PaymentMethodTypes, 1) AS PaymentMethod
			,2 AS Nature
			,SUM(ISNULL(PM.Value, CRE.Value)) AS Value
			,TN.Status AS Status
			,TN.CreationUser AS UserCode
			,concat(p.Fullname,'-',tn.CreationUser) CodeNameUser
			,c.Id CurrencyId
			,c.Abbreviation CurrencyAbbreviation
			,u.id as UserIntId
	FROM Treasury.TreasuryNote TN WITH (NOLOCK)
	INNER JOIN Treasury.CashReceipts CRE WITH (NOLOCK) ON CRE.Id = TN.CashReceiptId
	LEFT JOIN Treasury.CashRegisters CR WITH (NOLOCK) ON CR.Id = CRE.IdCashRegister
	LEFT JOIN(	SELECT EBA.Id,EBA.Code,EBA.Number,EBA.IdMainAccount,B.Name,EBA.Status,EBA.CurrencyId
				FROM Treasury.EntityBankAccounts EBA WITH(NOLOCK)
				JOIN Payroll.Bank B WITH(NOLOCK) on EBA.IdBank=B.Id) EBA ON EBA.Id=CRE.IdBankAccount
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id = ISNULL(CR.IdMainAccount,EBA.IdMainAccount)
	LEFT JOIN Security.[UserInt] u  ON TN.CreationUser =u.UserCode 
	LEFT JOIN Security.PersonInt p  ON u.IdPerson=p.Id
	LEFT JOIN Common.ThirdParty TP WITH (NOLOCK) ON TP.Id = CRE.IdThirdParty
	LEFT JOIN Treasury.PaymentMethods PM WITH (NOLOCK) ON CRE.Id = PM.IdCashReceipt
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.id= ISNULL(CR.CurrencyId,EBA.CurrencyId)
	WHERE TN.NoteType = 4
	GROUP BY CR.Id 
		,CR.Code
		,CR.Name
		,CR.Status
		,EBA.Id 
		,EBA.Code
		,EBA.Name
		,EBA.Status
		,MA.Number
		,TP.Nit 
		,TP.Name 
		,TN.Code 
		,TN.NoteDate 
		,TN.Description
		,ISNULL(PM.PaymentMethodTypes, 1)
		,TN.Status
		,TN.CreationUser
		,concat(p.Fullname,'-',TN.CreationUser)
		,c.Id
		,c.Abbreviation
		,u.id
UNION ALL
	SELECT	'Nota Rev Consignación' AS NameVoucher
			,CR.Id AS CashRegisterId
			,CR.Code AS CashRegisterCode
			,CR.Name AS CashRegisterName
			,CR.Status CashRegisterStatus
			,NULL AS IdBankAccount
			,NULL AS BankAccountCode
			,NULL AS BankAccountName
			,NULL AS BankAccountStatus
			,MA.Number
			,NULL AS ThirdPartyNit
			,NULL AS ThirdPartyName
			,TN.Code AS Code
			,TN.NoteDate AS DocumentDate
			,0 AS CheckNumber
			,TN.Description AS Detail
			,0 AS VoucherClass
			,1 AS VoucherType
			,4 AS PaymentMethod
			,1 AS Nature
			,CD.Value AS Value
			,TN.Status AS Status
			,TN.CreationUser AS UserCode
			,concat(p.Fullname,'-',tn.CreationUser) CodeNameUser
			,CU.Id CurrencyId
			,CU.Abbreviation CurrencyAbbreviation
			,u.id as UserIntId
	FROM Treasury.TreasuryNote TN WITH (NOLOCK)
	INNER JOIN Treasury.Consignment C WITH (NOLOCK) ON C.Id = TN.ConsignmentId
	INNER JOIN Treasury.ConsignmentDetail CD WITH (NOLOCK) ON C.Id = CD.ConsignmentTransferId
	INNER JOIN Treasury.CashRegisters CR WITH (NOLOCK) ON CD.CashRegisterId = CR.Id
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id = CR.IdMainAccount
	LEFT JOIN Security.[UserInt] u ON TN.CreationUser =u.UserCode 
	LEFT JOIN Security.PersonInt p  ON u.IdPerson=p.Id
	LEFT JOIN Common.Currency CU WITH(NOLOCK) ON CU.id=CR.CurrencyId
	WHERE TN.NoteType = 5
UNION ALL
	SELECT	'Nota Rev CE' AS NameVoucher
			,CR.Id AS CashRegisterId
			,CR.Code AS CashRegisterCode
			,CR.Name AS CashRegisterName
			,CR.Status CashRegisterStatus
			,EBA.Id AS IdBankAccount
			,EBA.Code AS BankAccountCode
			,EBA.Name AS BankAccountName
			,EBA.Status BankAccountStatus
			,MA.Number
			,TP.Nit AS ThirdPartyNit
			,TP.Name AS ThirdPartyName
			,TN.Code AS Code
			,TN.NoteDate AS DocumentDate
			,0 AS CheckNumber
			,TN.Description AS Detail
			,0 AS VoucherClass
			,1 AS VoucherType
			,CASE ISNULL(VT.PaymentMethod, 0)
				WHEN 0 THEN 1
				WHEN 1 THEN 2
				WHEN 2 THEN 4
			END AS PaymentMethod
			,1 AS Nature
			,TN.Value AS Value
			,TN.Status AS Status
			,TN.CreationUser AS UserCode
			,concat(p.Fullname,'-',tn.CreationUser) CodeNameUser
			,c.Id CurrencyId
			,c.Abbreviation CurrencyAbbreviation
			,u.id as UserIntId
	FROM Treasury.TreasuryNote TN WITH (NOLOCK)
	INNER JOIN Treasury.VoucherTransaction VT WITH (NOLOCK) ON VT.Id = TN.VoucherTransactionId
	LEFT JOIN Treasury.CashRegisters CR WITH (NOLOCK) ON CR.Id = VT.IdCashRegister
	LEFT JOIN(	SELECT EBA.Id,EBA.Code,EBA.Number,EBA.IdMainAccount,B.Name,EBA.Status,EBA.CurrencyId
				FROM Treasury.EntityBankAccounts EBA WITH(NOLOCK)
				JOIN Payroll.Bank B WITH(NOLOCK) on EBA.IdBank=B.Id) EBA ON EBA.Id=VT.IdEntityBankAccount
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id =ISNULL(CR.IdMainAccount,EBA.IdMainAccount)
	LEFT JOIN Security.[UserInt] u  ON TN.CreationUser =u.UserCode 
	LEFT JOIN Security.PersonInt p  ON u.IdPerson=p.Id
	LEFT JOIN Common.ThirdParty TP WITH (NOLOCK) ON TP.Id = VT.IdThirdParty
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.id=ISNULL(CR.CurrencyId,EBA.CurrencyId)
	WHERE TN.NoteType = 3
UNION ALL
	SELECT	'Nota Rev CE - Tras.' AS NameVoucher
			,CR.Id AS CashRegisterId
			,CR.Code AS CashRegisterCode
			,CR.Name AS CashRegisterName
			,CR.Status CashRegisterStatus
			,EBA.Id AS IdBankAccount
			,EBA.Code AS BankAccountCode
			,EBA.Name AS BankAccountName
			,EBA.Status BankAccountStatus
			,MA.Number
			,TP.Nit AS ThirdPartyNit
			,TP.Name AS ThirdPartyName
			,TN.Code AS Code
			,TN.NoteDate AS DocumentDate
			,0 AS CheckNumber
			,TN.Description AS Detail
			,0 AS VoucherClass
			,2 AS VoucherType
			,1 AS PaymentMethod
			,2 AS Nature
			,VTD.Value AS Value
			,TN.Status AS Status
			,TN.CreationUser AS UserCode
			,concat(p.Fullname,'-',tn.CreationUser) CodeNameUser
			,c.Id CurrencyId
			,c.Abbreviation CurrencyAbbreviation
			,u.id as UserIntId
	FROM Treasury.TreasuryNote TN WITH (NOLOCK)
	INNER JOIN Treasury.VoucherTransaction VT WITH (NOLOCK) ON VT.Id = TN.VoucherTransactionId
	INNER JOIN Treasury.VoucherTransactionDetails VTD WITH (NOLOCK) ON VT.Id = VTD.IdVoucherTransaction
	LEFT JOIN Treasury.CashRegisters CR WITH (NOLOCK) ON CR.Id = VTD.CashRegisterId
	LEFT JOIN(	SELECT EBA.Id,EBA.Code,EBA.Number,EBA.IdMainAccount,B.Name,EBA.Status
				FROM Treasury.EntityBankAccounts EBA WITH(NOLOCK)
				JOIN Payroll.Bank B WITH(NOLOCK) on EBA.IdBank=B.Id) EBA ON EBA.Id=VTD.IdEntityBankAccount
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id =ISNULL(CR.IdMainAccount,EBA.IdMainAccount)
	LEFT JOIN Security.[UserInt] u  ON TN.CreationUser =u.UserCode 
	LEFT JOIN Security.PersonInt p  ON u.IdPerson=p.Id
	LEFT JOIN Common.ThirdParty TP WITH (NOLOCK) ON TP.Id = VTD.IdThirdParty
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.id=VT.CurrencyId
	WHERE TN.NoteType = 3

	UNION ALL

	SELECT	'Nota Dev RC' AS NameVoucher
			,CR.Id AS CashRegisterId
			,CR.Code AS CashRegisterCode
			,CR.Name AS CashRegisterName
			,CR.Status CashRegisterStatus
			,EBA.Id AS IdBankAccount
			,EBA.Code AS BankAccountCode
			,EBA.Name AS BankAccountName
			,EBA.Status BankAccountStatus
			,MA.Number
			,TP.Nit AS ThirdPartyNit
			,TP.Name AS ThirdPartyName
			,TN.Code AS Code
			,TN.NoteDate AS DocumentDate
			,0 AS CheckNumber
			,TN.Description AS Detail
			,0 AS VoucherClass
			,2 AS VoucherType
			,ISNULL(PM.PaymentMethodTypes, 1) AS PaymentMethod
			,2 AS Nature
			,ISNULL(PM.Value, CRE.Value) AS Value
			,TN.Status AS Status
			,TN.CreationUser AS UserCode
			,concat(p.Fullname,'-',tn.CreationUser) CodeNameUser
			,c.Id CurrencyId
			,c.Abbreviation CurrencyAbbreviation
			,u.id as UserIntId
	FROM Treasury.TreasuryNote TN WITH (NOLOCK)
	INNER JOIN Treasury.CashReceipts CRE WITH (NOLOCK) ON CRE.Id = TN.CashReceiptId
	LEFT JOIN Treasury.CashRegisters CR WITH (NOLOCK) ON CR.Id = TN.CashRegisterId
	LEFT JOIN(	SELECT EBA.Id,EBA.Code,EBA.Number,EBA.IdMainAccount,B.Name,EBA.Status,EBA.CurrencyId
				FROM Treasury.EntityBankAccounts EBA WITH(NOLOCK)
				JOIN Payroll.Bank B WITH(NOLOCK) on EBA.IdBank=B.Id) EBA ON EBA.Id=TN.EntityBankAccountId
	INNER JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON MA.Id =ISNULL(CR.IdMainAccount,EBA.IdMainAccount)
	LEFT JOIN Security.[UserInt] u  ON TN.CreationUser =u.UserCode 
	LEFT JOIN Security.PersonInt p  ON u.IdPerson=p.Id
	LEFT JOIN Common.ThirdParty TP WITH (NOLOCK) ON TP.Id = CRE.IdThirdParty
	LEFT JOIN Treasury.PaymentMethods PM WITH (NOLOCK) ON CRE.Id = PM.IdCashReceipt
	LEFT JOIN Common.Currency C WITH(NOLOCK) ON C.id=ISNULL(CR.CurrencyId,EBA.CurrencyId)
	WHERE TN.NoteType =7
) AS Datos
GO