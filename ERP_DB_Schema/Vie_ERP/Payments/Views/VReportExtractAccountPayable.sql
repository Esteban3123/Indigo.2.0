


CREATE VIEW [Payments].[VReportExtractAccountPayable]
AS

SELECT        ROW_NUMBER() OVER (ORDER BY MovesDate ASC) AS Row, *
FROM            (SELECT distinct TP.Nit AS ThirdPartyNit, TP.Name AS ThirdPartyName, DL.Name AS DistributionLineName, MA.Number AS AccountNumber, MA.Name AS AccountName, AP.BillNumber AS BillNumber, 
                                                    CASE WHEN AP.EntityName = 'AccountPayable' THEN 'Cuenta Por Pagar' WHEN AP.EntityName = 'InitialBalance' THEN 'Saldo Inicial' END AS EntityName, AP.Value AS BillValueInitial, 
                                                    AP.Balance AS BillCurrentBalance, PN.Code AS MovesCode, PN.NoteDate AS MovesDate, CASE WHEN PN.Nature = 1 THEN PAPA.AdjusmentValue ELSE 0 END AS MovesDebit, 
                                                    CASE WHEN PN.Nature = 2 THEN PAPA.AdjusmentValue ELSE 0 END AS MovesCredit, 'Nota de CxP' AS VoucherName, AP.IdSupplier AS IdSupplier, AP.Id AS IdBills, PN.Status AS Status, 
                                                    AP.Code AS CodeAccountPayable, c.Abbreviation CurrencyAbbreviation
                          FROM            [Payments].[PaymentNotesAccountPayableAdvance] PAPA  with (nolock) INNER JOIN
												    Payments.PaymentNotes PN with (nolock) ON PN.Id = PAPA.PaymentNoteId INNER JOIN
                                                    Payments.AccountPayable AP with (nolock) ON AP.Id = PAPA.AccountPayableId INNER JOIN
                                                    Common.ThirdParty TP with (nolock) ON TP.Id = AP.IdThirdParty INNER JOIN
                                                    Common.SuppliersDistributionLines SDL with (nolock) ON SDL.Id = AP.IdSuppliersDistributionLines INNER JOIN
                                                    Common.DistributionLines DL with (nolock) ON DL.Id = SDL.IdDistributionLine INNER JOIN
                                                    GeneralLedger.MainAccounts MA with (nolock) ON MA.Id = DL.IdMainAccount
													LEFT JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ap.CurrencyId
                          WHERE        PN.Status = 2
                          UNION ALL
                          SELECT        TP.Nit AS ThirdPartyNit, TP.Name AS ThirdPartyName, DL.Name AS DistributionLineName, MA.Number AS AccountNumber, MA.Name AS AccountName, AP.BillNumber AS BillNumber, 
                                                   CASE WHEN AP.EntityName = 'AccountPayable' THEN 'Cuenta Por Pagar' WHEN AP.EntityName = 'InitialBalance' THEN 'Saldo Inicial' END AS EntityName, AP.Value AS BillValueInitial, 
                                                   AP.Balance AS BillCurrentBalance, PT.Code AS MovesCode, PT.DocumentDate AS MovesDate, PTD.Value AS MovesDebit, 0 AS MovesCredit, 'Cruce Anticipo vs CxP' AS VoucherName, AP.IdSupplier AS IdSupplier, 
                                                   AP.Id AS IdBills, PT.Status AS Status, AP.Code AS CodeAccountPayable,  c.Abbreviation CurrencyAbbreviation
                          FROM            [Payments].[PaymentTransferDetail] PTD with (nolock) INNER JOIN
                                                   Payments.PaymentTransfer PT with (nolock) ON PT.Id = PTD.PaymentTransferId INNER JOIN
                                                   Payments.AccountPayable AP with (nolock) ON AP.Id = PTD.AccountPayableId INNER JOIN
                                                   Common.ThirdParty TP with (nolock) ON TP.Id = AP.IdThirdParty INNER JOIN
                                                   Common.SuppliersDistributionLines SDL with (nolock) ON SDL.Id = AP.IdSuppliersDistributionLines INNER JOIN
                                                   Common.DistributionLines DL with (nolock) ON DL.Id = SDL.IdDistributionLine INNER JOIN
                                                   GeneralLedger.MainAccounts MA with (nolock) ON MA.Id = DL.IdMainAccount
												   LEFT JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ap.CurrencyId
                          WHERE        PT.Status = 2
                          UNION ALL
                          SELECT        TP.Nit AS ThirdPartyNit, TP.Name AS ThirdPartyName, DL.Name AS DistributionLineName, MA.Number AS AccountNumber, MA.Name AS AccountName, AP.BillNumber AS BillNumber, 
                                                   CASE WHEN AP.EntityName = 'AccountPayable' THEN 'Cuenta Por Pagar' WHEN AP.EntityName = 'InitialBalance' THEN 'Saldo Inicial' END AS EntityName, AP.Value AS BillValueInitial, 
                                                   AP.Balance AS BillCurrentBalance, CA.Code AS MovesCode, CA.DocumentDate AS MovesDate, CADCXP.CrossingValue AS MovesDebit, 0 AS MovesCredit, 'Cruce de Cuenta' AS VoucherName, 
                                                   AP.IdSupplier AS IdSupplier, AP.Id AS IdBills, CA.Status AS Status, AP.Code AS CodeAccountPayable, c.Abbreviation CurrencyAbbreviation
                          FROM            [Treasury].[CrossingAccountDetailCxP] CADCXP with (nolock) INNER JOIN
                                                   Treasury.CrossingAccount CA with (nolock) ON CA.Id = CADCXP.CrossingAccountId INNER JOIN
                                                   Payments.AccountPayable AP with (nolock) ON AP.Id = CADCXP.AccountPayableId INNER JOIN
                                                   Common.ThirdParty TP with (nolock) ON TP.Id = AP.IdThirdParty INNER JOIN
                                                   Common.SuppliersDistributionLines SDL with (nolock) ON SDL.Id = AP.IdSuppliersDistributionLines INNER JOIN
                                                   Common.DistributionLines DL with (nolock) ON DL.Id = SDL.IdDistributionLine INNER JOIN
                                                   GeneralLedger.MainAccounts MA with (nolock) ON MA.Id = DL.IdMainAccount
												    LEFT JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ap.CurrencyId
                          WHERE        CA.Status = 2
                          UNION ALL
                          SELECT        TP.Nit AS ThirdPartyNit, TP.Name AS ThirdPartyName, DL.Name AS DistributionLineName, MA.Number AS AccountNumber, MA.Name AS AccountName, AP.BillNumber AS BillNumber, 
                                                   CASE WHEN AP.EntityName = 'AccountPayable' THEN 'Cuenta Por Pagar' WHEN AP.EntityName = 'InitialBalance' THEN 'Saldo Inicial' END AS EntityName, AP.Value AS BillValueInitial, 
                                                   AP.Balance AS BillCurrentBalance, VT.Code AS MovesCode, VT.DocumentDate AS MovesDate, DB.AdvancedValue AS MovesDebit, 0 AS MovesCredit, 'Comprobante de Egreso' AS VoucherName, 
                                                   AP.IdSupplier AS IdSupplier, AP.Id AS IdBills, VT.Status AS Status, AP.Code AS CodeAccountPayable, c.Abbreviation CurrencyAbbreviation 
                          FROM            [Treasury].[DischargeBill] DB with (nolock) INNER JOIN
                                                   Treasury.VoucherTransactionDetails VTD with (nolock) ON VTD.Id = DB.IdVoucherTransactionD INNER JOIN
                                                   Treasury.VoucherTransaction VT with (nolock) ON VT.Id = VTD.IdVoucherTransaction INNER JOIN
                                                   Payments.AccountPayable AP with (nolock) ON AP.Id = DB.IdAccountPayable INNER JOIN
                                                   Common.ThirdParty TP with (nolock) ON TP.Id = AP.IdThirdParty INNER JOIN
                                                   Common.SuppliersDistributionLines SDL with (nolock) ON SDL.Id = AP.IdSuppliersDistributionLines INNER JOIN
                                                   Common.DistributionLines DL with (nolock) ON DL.Id = SDL.IdDistributionLine INNER JOIN
                                                   GeneralLedger.MainAccounts MA with (nolock) ON MA.Id = DL.IdMainAccount
												   LEFT JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ap.CurrencyId
                          WHERE        VT.Status IN (2, 4)
                          UNION ALL
                          SELECT        TP.Nit AS ThirdPartyNit, TP.Name AS ThirdPartyName, DL.Name AS DistributionLineName, MA.Number AS AccountNumber, MA.Name AS AccountName, AP.BillNumber AS BillNumber, 
                                                   CASE WHEN AP.EntityName = 'AccountPayable' THEN 'Cuenta Por Pagar' WHEN AP.EntityName = 'InitialBalance' THEN 'Saldo Inicial' END AS EntityName, AP.Value AS BillValueInitial, 
                                                   AP.Balance AS BillCurrentBalance, AP.Code AS MovesCode, AP.DocumentDate AS MovesDate, 0 AS MovesDebit, case AP.InitialBalance when 1 then isnull(iniap.Balance,AP.Value) else AP.Value end AS MovesCredit, 'Cuenta por pagar' AS VoucherName, 
                                                   AP.IdSupplier AS IdSupplier, AP.Id AS IdBills, AP.Status AS Status, AP.Code AS CodeAccountPayable, c.Abbreviation CurrencyAbbreviation
                          FROM            [Payments].[AccountPayable] AP with (nolock) INNER JOIN
                                                   Common.ThirdParty TP with (nolock) ON TP.Id = AP.IdThirdParty INNER JOIN
                                                   Common.SuppliersDistributionLines SDL with (nolock) ON SDL.Id = AP.IdSuppliersDistributionLines INNER JOIN
                                                   Common.DistributionLines DL with (nolock) ON DL.Id = SDL.IdDistributionLine INNER JOIN
                                                   GeneralLedger.MainAccounts MA with (nolock) ON MA.Id = DL.IdMainAccount left join 
												   Payments.InitialBalanceAccountPayable iniap on iniap.AccountPayableId = AP.Id
												   LEFT JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ap.CurrencyId
                          WHERE        AP.Status = 2
                          UNION ALL
                          SELECT        TP.Nit AS ThirdPartyNit, TP.Name AS ThirdPartyName, DL.Name AS DistributionLineName, MA.Number AS AccountNumber, MA.Name AS AccountName, AP.BillNumber AS BillNumber, 
                                                   CASE WHEN AP.EntityName = 'AccountPayable' THEN 'Cuenta Por Pagar' WHEN AP.EntityName = 'InitialBalance' THEN 'Saldo Inicial' END AS EntityName, AP.Value AS BillValueInitial, 
                                                   AP.Balance AS BillCurrentBalance, TN.Code AS MovesCode, TN.NoteDate AS MovesDate, 0 AS MovesDebit, DB.AdvancedValue AS MovesCredit, 'Nota de Tesoreria' AS VoucherName, 
                                                   AP.IdSupplier AS IdSupplier, AP.Id AS IdBills, TN.Status AS Status, AP.Code AS CodeAccountPayable, c.Abbreviation CurrencyAbbreviation
                          FROM            [Treasury].[TreasuryNote] TN with (nolock) INNER JOIN
                                                   Treasury.VoucherTransaction VT with (nolock) ON VT.Id = TN.VoucherTransactionId INNER JOIN
                                                   Treasury.VoucherTransactionDetails VTD with (nolock) ON VT.Id = VTD.IdVoucherTransaction INNER JOIN
                                                   Treasury.DischargeBill DB with (nolock) ON VTD.Id = DB.IdVoucherTransactionD INNER JOIN
                                                   Payments.AccountPayable AP with (nolock) ON AP.Id = DB.IdAccountPayable INNER JOIN
                                                   Common.ThirdParty TP with (nolock) ON TP.Id = AP.IdThirdParty INNER JOIN
                                                   Common.SuppliersDistributionLines SDL with (nolock) ON SDL.Id = AP.IdSuppliersDistributionLines INNER JOIN
                                                   Common.DistributionLines DL  with (nolock) ON DL.Id = SDL.IdDistributionLine INNER JOIN
                                                   GeneralLedger.MainAccounts MA with (nolock) ON MA.Id = DL.IdMainAccount
												   LEFT JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ap.CurrencyId
                          WHERE        TN.Status = 2 AND VT.Id IN
                                                       (SELECT        VTD.IdVoucherTransaction
                                                         FROM            [Treasury].[DischargeBill] DB with (nolock) INNER JOIN
                                                                                   Treasury.VoucherTransactionDetails VTD with (nolock) ON VTD.Id = DB.IdVoucherTransactionD
                                                         WHERE        VT.Status IN (2, 4))

					 UNION ALL
                          SELECT        TP.NIT AS ThirdPartyNit, TP.Name AS ThirdPartyName, DL.Name AS DistributionLineName, MA.Number AS AccountNumber, MA.Name AS AccountName, AP.BillNumber AS BillNumber, 
                                                   CASE WHEN AP.EntityName = 'AccountPayable' THEN 'Cuenta Por Pagar' WHEN AP.EntityName = 'InitialBalance' THEN 'Saldo Inicial' END AS EntityName, AP.Value AS BillValueInitial, 
                                                   AP.Balance AS BillCurrentBalance, CR.Code AS MovesCode, CR.DocumentDate AS MovesDate, 0 AS MovesDebit, CRDAP.RefundValue AS MovesCredit, 'R. Caja Reintegro CxP' AS VoucherName, 
                                                   AP.IdSupplier AS IdSupplier, AP.Id AS IdBills, CR.Status AS Status, AP.Code AS CodeAccountPayable, c.Abbreviation CurrencyAbbreviation
                          FROM            Treasury.CashReceipts CR with (nolock) INNER JOIN
                                                   Treasury.CashReceiptDetails AS CRD with (nolock) ON CR.id = CRD.IdCashReceipt INNER JOIN 
												   Treasury.CashReceiptDetailAccountPayable as CRDAP with (nolock) ON CRD.ID = CRDAP.CashReceiptDetailId INNER JOIN
                                                   Payments.AccountPayable AP with (nolock) ON AP.Id = CRDAP.AccountPayableId INNER JOIN
                                                   Common.ThirdParty TP with (nolock) ON TP.Id = AP.IdThirdParty INNER JOIN
                                                   Common.SuppliersDistributionLines SDL with (nolock) ON SDL.Id = AP.IdSuppliersDistributionLines INNER JOIN
                                                   Common.DistributionLines DL  with (nolock) ON DL.Id = SDL.IdDistributionLine INNER JOIN
                                                   GeneralLedger.MainAccounts MA with (nolock) ON MA.Id = DL.IdMainAccount
												   LEFT JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ap.CurrencyId
                          WHERE        CR.Status = 2) AS Datos