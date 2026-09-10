

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el boletín de movimientos de tesorería agrupados por cuenta bancaria de la entidad. Integra en un único resultado todos los tipos de transacciones financieras registradas: recibos de caja (cobros a pacientes, empresas o aseguradoras), consignaciones bancarias, movimientos de fondo de caja menor (aumentos y disminuciones), comprobantes de egreso y sus traslados, y notas de tesorería. Para cada movimiento expone la cuenta bancaria de la entidad (banco, tipo, número, estado y cuenta contable asociada), el tercero involucrado (NIT y nombre del proveedor, asegurador o empresa), el código y fecha del documento, el valor de la transacción, el tipo de comprobante, la naturaleza débito/crédito y la moneda. Sirve como fuente principal de reportería de extracto o boletín bancario interno, permitiendo auditar y conciliar todos los ingresos y egresos por cuenta bancaria desde un único punto de consulta.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'VReportTreasuryNewsletterEntityBankAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'VReportTreasuryNewsletterEntityBankAccount';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola fuente todos los movimientos de tesorería (recibos, consignaciones, cajas menores, comprobantes de egreso, traslados y notas) asociados a cuentas bancarias de la entidad para el reporte de boletín bancario.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterEntityBankAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada movimiento debe estar vinculado a una cuenta bancaria de la entidad (Treasury.EntityBankAccounts) con cuenta contable principal (GeneralLedger.MainAccounts) y banco (Payroll.Bank) asociados; de lo contrario el INNER JOIN excluye la fila.; Para ''Nota Dev RC'' / ''Nota Rev RC'' la nota debe tener CashReceiptId y métodos de pago asociados.; Para ''Nota Dev Consignación'' la nota debe referenciar una consignación (ConsignmentId) existente.; Para ''Nota Dev CE'' y ''Nota Dev CE - Tras.'' la nota debe referenciar una transacción de comprobante (VoucherTransactionId).', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterEntityBankAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Treasury.VReportTreasuryNewsletterEntityBankAccount: Devuelve una columna ''Row'' construida como CONCAT(NameVoucher,''-'',EntityBankId,''-'',EntityBankAccountNumber,''-'',Code) que actúa como identificador de fila del reporte.; [RETURN_RESULT] Treasury.VReportTreasuryNewsletterEntityBankAccount: Para recibos de caja (CashReceipts) el Value se calcula como SUM(PaymentMethods.Value) agrupado por recibo; VoucherType=1 y Nature=1 (''Recibo C.'').; [RETURN_RESULT] Treasury.VReportTreasuryNewsletterEntityBankAccount: Para consignaciones se etiqueta NameVoucher=''Consignación'', VoucherType=1, Nature=1, sin tercero (Nit/Name en NULL).; [RETURN_RESULT] Treasury.VReportTreasuryNewsletterEntityBankAccount: Para ConstitutionCashSmaller: si DocumentType=1 → Detail=''Aumento de Caja Menor'' y VoucherType=Nature=2; si DocumentType<>1 → ''Disminución de Caja Menor'' y VoucherType=Nature=1. Adicionalmente SourceType=1 → ''desde Caja Mayor'', en caso contrario ''desde Cuenta Bancaria''.; [RETURN_RESULT] Treasury.VReportTreasuryNewsletterEntityBankAccount: Para VoucherTransaction directo (''Comp. E.'') se fija VoucherType=2 y Nature=2 (egreso).; [RETURN_RESULT] Treasury.VReportTreasuryNewsletterEntityBankAccount: Para detalles de VoucherTransaction (''Comp. E - Tras.'') se fija VoucherType=1 y Nature=1, usando VTD.Value y VTD.Detail por línea.; [RETURN_RESULT] Treasury.VReportTreasuryNewsletterEntityBankAccount: Para TreasuryNote.NoteType=1 (''Nota'') VoucherType y Nature toman el valor de TN.Nature.; [RETURN_RESULT] Treasury.VReportTreasuryNewsletterEntityBankAccount: Para TreasuryNote con NoteType=4 → NameVoucher=''Nota Rev RC'' y Nature=2; con NoteType=7 → NameVoucher=''Nota Dev RC'' y Nature=NULL; en ambos VoucherType=2 y Value=SUM(PaymentMethods.Value).; [RETURN_RESULT] Treasury.VReportTreasuryNewsletterEntityBankAccount: Para TreasuryNote.NoteType=5 se etiqueta ''Nota Dev Consignación'', VoucherType=2, Nature=2, Value=Consignment.Value.; [RETURN_RESULT] Treasury.VReportTreasuryNewsletterEntityBankAccount: Para TreasuryNote.NoteType=3 se generan dos filas: ''Nota Dev CE'' (VoucherType=1, Nature=1, Value=TN.Value) basada en VoucherTransaction y ''Nota Dev CE - Tras.'' (VoucherType=2, Nature=2, Value=VTD.Value) por cada detalle del comprobante.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterEntityBankAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CCS.DocumentType = 1 → Detail comienza con ''Aumento de Caja Menor'' y VoucherType=Nature=2 else Detail comienza con ''Disminución de Caja Menor'' y VoucherType=Nature=1; si CCS.SourceType = 1 → Sufijo del Detail es ''desde Caja Mayor'' else Sufijo del Detail es ''desde Cuenta Bancaria''; si TN.NoteType = 1 → Se incluye como ''Nota'' con VoucherType=Nature=TN.Nature; si TN.NoteType = 4 → Se incluye como ''Nota Rev RC'' con Nature=2 else Si NoteType=7 se incluye como ''Nota Dev RC'' con Nature=NULL; si TN.NoteType = 5 → Se incluye como ''Nota Dev Consignación'' uniendo con Treasury.Consignment; si TN.NoteType = 3 → Se incluye como ''Nota Dev CE'' (a nivel cabecera) y como ''Nota Dev CE - Tras.'' (por cada VoucherTransactionDetail); si PaymentMethods.IdEntityBankAccount IS NULL (en notas de devolución de RC) → Se usa CashReceipts.IdBankAccount como cuenta bancaria asociada (ISNULL(pm.IdEntityBankAccount, cr.IdBankAccount)) else Se usa pm.IdEntityBankAccount', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterEntityBankAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterEntityBankAccount';
GO
