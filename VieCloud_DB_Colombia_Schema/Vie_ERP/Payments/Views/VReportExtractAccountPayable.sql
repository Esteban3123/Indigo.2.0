

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de extracto de cuentas por pagar que consolida todos los movimientos que afectan las facturas o documentos pendientes de pago a proveedores y terceros. Integra cuatro tipos de movimientos en un único resultado: notas de pago (débito/crédito) aplicadas a cuentas por pagar, cruces de anticipos contra cuentas por pagar, cruces de cuentas de tesorería y pagos directos; cada uno con su código de comprobante, fecha, valores débito y crédito. Para cada movimiento expone los datos del proveedor (NIT, nombre), la línea de distribución contable, la cuenta contable principal (número y nombre), el número de factura, el valor inicial y saldo actual de la cuenta por pagar, y la moneda. Sirve como base para reportería financiera y contable de cuentas por pagar, conciliación de saldos con proveedores, trazabilidad de aplicación de notas y anticipos, y seguimiento del estado de documentos de pago aprobados.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'VReportExtractAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'VReportExtractAccountPayable';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único extracto contable los movimientos que afectan las cuentas por pagar de proveedores (notas, cruces, anticipos, egresos, reintegros y saldos iniciales) con sus débitos y créditos para reportería financiera.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VReportExtractAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los documentos origen (notas de pago, transferencias, cruces, comprobantes de egreso, notas de tesorería, recibos de caja y la propia cuenta por pagar) deben encontrarse en estado aprobado/contabilizado (Status = 2), salvo comprobantes de egreso que admiten Status 2 o 4.; Cada cuenta por pagar debe tener tercero (IdThirdParty), línea de distribución de proveedor (IdSuppliersDistributionLines) y cuenta contable principal asociada para aparecer en el reporte.; Para que un saldo inicial use el valor de InitialBalanceAccountPayable, la cuenta por pagar debe tener marca InitialBalance = 1; en caso contrario se toma AP.Value.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VReportExtractAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen movimientos de documentos contabilizados/aprobados (Status=2; egresos también Status=4).; Cada fila representa un único movimiento: o débito o crédito, nunca ambos simultáneamente (el otro queda en 0).; Toda fila pertenece a una cuenta por pagar con tercero, línea de distribución de proveedor y cuenta contable principal vinculados.; La numeración Row se asigna por orden cronológico ascendente de la fecha del movimiento.; La moneda se obtiene siempre desde la cuenta por pagar (AP.CurrencyId), no del documento de movimiento.; El VoucherName identifica de forma fija el tipo de origen contable de cada movimiento.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VReportExtractAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payments.VReportExtractAccountPayable: Devuelve filas numeradas (ROW_NUMBER ordenado por fecha de movimiento ascendente) unificando 8 orígenes de movimientos sobre AccountPayable.; [RETURN_RESULT] Payments.VReportExtractAccountPayable: Para notas de pago (PaymentNotes) con Status=2: si Nature=1 el AdjusmentValue se reporta como MovesDebit, si Nature=2 como MovesCredit, etiquetando VoucherName=''Nota de CxP''.; [RETURN_RESULT] Payments.VReportExtractAccountPayable: Para PaymentTransfer con Status=2 se reporta PTD.Value como MovesDebit y 0 crédito, con VoucherName=''Cruce Anticipo vs CxP''.; [RETURN_RESULT] Payments.VReportExtractAccountPayable: Para CrossingAccount con Status=2 se reporta CrossingValue como MovesDebit, VoucherName=''Cruce de Cuenta''.; [RETURN_RESULT] Payments.VReportExtractAccountPayable: Para VoucherTransaction (egresos) con Status IN (2,4) se reporta DischargeBill.AdvancedValue como MovesDebit, VoucherName=''Comprobante de Egreso''.; [RETURN_RESULT] Payments.VReportExtractAccountPayable: Para AccountPayable con Status=2 se genera la fila base de causación con MovesCredit = ISNULL(InitialBalanceAccountPayable.Balance, AP.Value) cuando InitialBalance=1, o AP.Value en caso contrario; VoucherName=''Cuenta por pagar''.; [RETURN_RESULT] Payments.VReportExtractAccountPayable: Para TreasuryNote con Status=2 cuyo VoucherTransaction tenga descargues vigentes (Status IN 2,4) se reporta AdvancedValue como MovesCredit, VoucherName=''Nota de Tesoreria''.; [RETURN_RESULT] Payments.VReportExtractAccountPayable: Para CashReceipts con Status=2 se reporta RefundValue como MovesCredit, VoucherName=''R. Caja Reintegro CxP'' (reintegros desde caja a CxP).; [RETURN_RESULT] Payments.VReportExtractAccountPayable: EntityName se traduce: ''AccountPayable'' → ''Cuenta Por Pagar'', ''InitialBalance'' → ''Saldo Inicial''; cualquier otro valor queda NULL.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VReportExtractAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PN.Nature = 1 (en notas de pago de CxP) → El valor del ajuste se clasifica como débito del movimiento else Si Nature=2 se clasifica como crédito; otros valores producen 0 en ambos; si AP.EntityName = ''AccountPayable'' → Se etiqueta como ''Cuenta Por Pagar'' else Si EntityName=''InitialBalance'' se etiqueta ''Saldo Inicial''; si AP.InitialBalance = 1 en la fila base de la CxP → El crédito del movimiento toma ISNULL(InitialBalanceAccountPayable.Balance, AP.Value) else El crédito toma AP.Value; si Estado del documento origen → Solo se incluyen documentos con Status = 2 (aprobado/contabilizado) else Para comprobantes de egreso (VoucherTransaction) se admiten Status 2 o 4', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VReportExtractAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VReportExtractAccountPayable';
GO
