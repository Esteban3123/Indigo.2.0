

CREATE VIEW [Treasury].[VReportTreasuryNewsletterCash]
AS
SELECT	CONCAT(Datos.NameVoucher,'-',Datos.CashRegisterId,'-',IdBankAccount, '-',Datos.Code,'-',datos.PaymentMethod) AS Row
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte de boletín de caja para tesorería. Consolida en un único conjunto de filas todos los movimientos de efectivo de la entidad, uniendo cuatro tipos de documentos: recibos de caja (cobros a terceros como pacientes, empresas o aseguradoras), consignaciones bancarias, aumentos y disminuciones de fondos de caja menor (caja mayor o cuenta bancaria). Para cada movimiento expone datos clave de negocio como el tipo de comprobante, la caja o cuenta bancaria involucrada, el número de cuenta contable principal, el NIT y nombre del tercero, el código y fecha del documento, el método de pago (efectivo, cheque, tarjeta, depósito, fondo caja menor), el valor, el estado, la moneda y el usuario responsable con su nombre completo. Se usa principalmente para la generación del boletín diario o periódico de caja en tesorería, conciliación de movimientos y cuadre contable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'VReportTreasuryNewsletterCash';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'VReportTreasuryNewsletterCash';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único conjunto los movimientos de tesorería (recibos de caja, consignaciones, caja menor, comprobantes de egreso y notas) para alimentar el reporte/boletín de caja con su clasificación de naturaleza, tipo de comprobante y método de pago.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterCash';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada movimiento debe tener una cuenta principal (MainAccounts) resoluble vía CashRegister.IdMainAccount o EntityBankAccount.IdMainAccount (INNER JOIN obligatorio); Los recibos de caja (CashReceipts) deben tener al menos un registro en Treasury.PaymentMethods (INNER JOIN); Las consignaciones requieren existencia tanto en Consignment como en ConsignmentDetail y referenciar una CashRegister con IdMainAccount; Las notas de tesorería se filtran por NoteType específico según el tipo de reverso/devolución (2, 3, 4, 5, 7); Los terceros referenciados deben existir en Common.ThirdParty para los recibos (INNER JOIN); para egresos y notas el tercero es opcional (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterCash';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Recibos de caja siempre se reportan con VoucherType=1 y Nature=1 (ingreso); Consignaciones siempre se reportan con VoucherType=2, Nature=2 y PaymentMethod=4; Comprobantes de egreso (cabecera) siempre Nature=2 y VoucherType=2; sus detalles invierten a Nature=1/VoucherType=1; Notas de reverso/devolución de recibos (NoteType 4 y 7) siempre VoucherType=2 y Nature=2; Notas de reverso de consignación (NoteType 5) siempre VoucherType=1, Nature=1 y PaymentMethod=4; PaymentMethod=5 se asigna a movimientos de caja menor y a notas genéricas (NoteType=2); CheckNumber solo se materializa para comprobantes de egreso (Comp. E. cabecera); en el resto se fuerza a 0; VoucherClass solo proviene de VoucherTransaction; en el resto de orígenes se fuerza a 0; El usuario creador siempre se enriquece con su nombre completo desde PersonInt vía UserInt (CodeNameUser = Fullname-UserCode); La moneda siempre se resuelve priorizando la de la caja registradora sobre la de la cuenta bancaria', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterCash';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Recibo de caja; Consignación bancaria; Caja menor; Caja mayor; Comprobante de egreso; Nota de tesorería (reverso/devolución); Método de pago (efectivo, cheque, depósito/transferencia); Cuenta bancaria de la entidad; Cuenta contable principal (PUC); Tercero (NIT); Moneda/divisa; Naturaleza del movimiento (ingreso/egreso)', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterCash';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Treasury.VReportTreasuryNewsletterCash: Devuelve filas unificadas etiquetadas con NameVoucher: ''Recibo C.'', ''Consignación'', ''Fondo Caja M'' (dos variantes según origen/destino de caja menor), ''Comp. E.'', ''Nota'', ''Nota Rev RC'', ''Nota Rev Consignación'', ''Nota Rev CE'', ''Nota Rev CE - Tras.'' y ''Nota Dev RC''; [RETURN_RESULT] Treasury.VReportTreasuryNewsletterCash: Construye una clave Row = NameVoucher-CashRegisterId-IdBankAccount-Code-PaymentMethod para identificar de forma única cada fila del reporte', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterCash';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ConstitutionCashSmaller.DocumentType = 1 (aumento) → Detail = ''Aumento de Caja Menor''; en la rama de la caja origen VoucherType=2/Nature=2 (egreso) y en la rama de la caja menor destino VoucherType=1/Nature=1 (ingreso) else Detail = ''Disminución de Caja Menor''; se invierten los VoucherType/Nature entre las dos ramas; si ConstitutionCashSmaller.SourceType = 1 → Texto del detalle agrega '' desde Caja Mayor'' else Texto del detalle agrega '' desde Cuenta Bancaria''; si VoucherTransaction.PaymentMethod (ISNULL,0) → Mapea PaymentMethod del reporte: 0→1 (efectivo), 1→2 (cheque), 2→4 (depósito/transferencia); si TreasuryNote.NoteType = 2 → Se incluye como ''Nota'' genérica con VoucherType = TN.Nature y PaymentMethod=5 else Otros NoteType se enrutan a ramas específicas: 4=''Nota Rev RC'', 5=''Nota Rev Consignación'', 3=''Nota Rev CE'' y ''Nota Rev CE - Tras.'', 7=''Nota Dev RC''; si Existencia de PaymentMethods.Value para el recibo → Value = SUM(PM.Value) agrupado por método de pago else Value = CRC.Value (valor total del recibo) cuando PM.Value es NULL; si CashRegister.CurrencyId / IdMainAccount es NULL → Se usa EntityBankAccount.CurrencyId / IdMainAccount como fallback (ISNULL(CR.x, EBA.x))', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterCash';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.CashReceipts; Common.ThirdParty; Treasury.CashRegisters; Treasury.EntityBankAccounts; Payroll.Bank; GeneralLedger.MainAccounts; Treasury.PaymentMethods; Security.UserInt; Security.PersonInt; Common.Currency; Treasury.ConsignmentDetail; Treasury.Consignment; Treasury.ConstitutionCashSmaller; Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.TreasuryNote', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterCash';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterCash';
GO
