

CREATE VIEW [Portfolio].[VReportExtractAccountReceivableTmp]
as
SELECT 
TP.Nit as ThirdPartyNit
,TP.Name as ThirdPartyName
,MA.Number as AccountNumber
,MA.Name as AccountName
,'Factura' as TypeDocument
,AR.InvoiceNumber as DocumentNumber
,case when AR.OpeningBalance = 1 then 'Saldo CxC' when AR.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
,ARA.Value as BillValueInitial
,ARA.Balance as BillCurrentBalance
,PN.Code as MovesCode
,PN.NoteDate as MovesDate
,case when PN.Nature = 1 then PARA.AdjusmentValue else 0 end as MovesDebit
,case when PN.Nature = 2 then PARA.AdjusmentValue else 0 end as MovesCredit
,case when PN.Nature = 1 then 'Nota Débito De CxC' else 'Nota Crédito De CxC' end as VoucherName
,CG.Id as IdCareGroup
,PN.Status as Status
,CG.Code as CareGroupCode
,AR.AccountReceivableType as AccountReceivableType
  FROM [Portfolio].[PortfolioNoteAccountReceivableAdvance] PARA with (nolock) 
  inner join Portfolio.PortfolioNote PN with (nolock) on PN.Id = PARA.PortfolioNoteId 
  inner join Portfolio.AccountReceivable AR with (nolock) on AR.Id = PARA.AccountReceivableId 
  inner join Common.ThirdParty TP with (nolock) on TP.Id = AR.ThirdPartyId 
  inner join Portfolio.AccountReceivableAccounting ARA with (nolock) on ARA.AccountReceivableId = AR.Id AND ARA.MainAccountId = PARA.MainAccountId
  left join Billing.Invoice IV with (nolock) on IV.Id = AR.InvoiceId left join Contract.CareGroup CG with (nolock) on CG.Id = IV.CareGroupId 
  left join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PARA.MainAccountId
  where PN.Status = 2
  union all
SELECT 
TP.Nit as ThirdPartyNit
,TP.Name as ThirdPartyName
,MA.Number as AccountNumber
,MA.Name as AccountName
,'Factura' as TypeDocument
,AR.InvoiceNumber as DocumentNumber
,case when AR.OpeningBalance = 1 then 'Saldo CxC' when AR.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
,AR.Value as BillValueInitial
,AR.Balance as BillCurrentBalance
,PT.Code as MovesCode
,PT.DocumentDate as MovesDate
,0 as MovesDebit
,sum(PTD.Value) as MovesCredit
,'Cruce Ant Vs CxC' as VoucherName
,CG.Id as IdCareGroup
,PT.Status as Status
,CG.Code as CareGroupCode
,AR.AccountReceivableType as AccountReceivableType
  FROM [Portfolio].[PortfolioTransferDetail] PTD with (nolock) 
  inner join Portfolio.PortfolioTransfer PT with (nolock) on PT.Id = PTD.PortfolioTrasferId
  inner join Portfolio.AccountReceivable AR with (nolock) on AR.Id = PTD.AccountReceivableId 
  inner join Common.ThirdParty TP with (nolock) on TP.Id = AR.ThirdPartyId 
  inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PTD.MainAccountId 
  left join Billing.Invoice IV with (nolock) on IV.Id = AR.InvoiceId 
  left join Contract.CareGroup CG with (nolock) on CG.Id = IV.CareGroupId
  where PT.Status = 2
  group by TP.Nit,TP.Name,MA.Number,MA.Name, AR.InvoiceNumber, AR.OpeningBalance, AR.Value, AR.Balance, PT.Code, PT.DocumentDate, CG.Id, PT.Status, CG.Code, AR.AccountReceivableType
 union all
SELECT 
TP.Nit as ThirdPartyNit
,TP.Name as ThirdPartyName
,MA.Number as AccountNumber
,MA.Name as AccountName
,'Factura' as TypeDocument
,AR.InvoiceNumber as DocumentNumber
,case when AR.OpeningBalance = 1 then 'Saldo CxC' when AR.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
,AR.Value as BillValueInitial
,AR.Balance as BillCurrentBalance
,CA.Code as MovesCode
,CA.DocumentDate as MovesDate
,CADCXC.CrossingValue as MovesDebit
,0 as MovesCredit
,'Cruce De Cuenta' as VoucherName
,CG.Id as IdCareGroup
,CA.Status as Status
,CG.Code as CareGroupCode
,AR.AccountReceivableType as AccountReceivableType
  FROM [Treasury].[CrossingAccountDetailCxC] CADCXC with (nolock) 
  inner join Treasury.CrossingAccount CA  with (nolock) on CA.Id = CADCXC.CrossingAccountId 
  inner join Portfolio.AccountReceivable AR with (nolock) on AR.Id = CADCXC.AccountReceivableId 
  inner join Common.ThirdParty TP with (nolock) on TP.Id= AR.ThirdPartyId 
  inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = CADCXC.MainAccountId 
  left join Billing.Invoice IV with (nolock) on IV.Id = AR.InvoiceId 
  left join Contract.CareGroup CG with (nolock) on CG.Id = IV.CareGroupId
 where CA.Status = 2
 union all
SELECT 
TP.Nit as ThirdPartyNit
,TP.Name as ThirdPartyName
,MA.Number as AccountNumber
,MA.Name as AccountName
,'Factura' as TypeDocument
,AR.InvoiceNumber as DocumentNumber
,case when AR.OpeningBalance = 1 then 'Saldo CxC' when AR.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
,AR.Value as BillValueInitial
,AR.Balance as BillCurrentBalance
,CR.Code as MovesCode
,CR.DocumentDate as MovesDate
,case when CRD.Nature = 1 then CRAR.Value else 0 end as MovesDebit
,case when CRD.Nature = 2 then CRAR.Value else 0 end as MovesCredit
,'Recibo De Caja' as VoucherName
,CG.Id as IdCareGroup
,CR.Status as Status
,CG.Code as CareGroupCode
,AR.AccountReceivableType as AccountReceivableType
  FROM [Treasury].[CashReceipts] CR with (nolock)
  inner join Treasury.CashReceiptDetails CRD  with (nolock) on CRD.IdCashReceipt = CR.Id 
  inner join Common.ThirdParty TP with (nolock) on TP.Id = CRD.IdThirdParty 
  inner join Treasury.CashReceiptAccountReceivable CRAR with (nolock) on CRAR.CashReceiptDetailId = CRD.Id 
  inner join Portfolio.AccountReceivable AR with (nolock) on AR.Id = CRAR.AccountReceivableId 
  inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = CRD.IdMainAccount 
  left join Billing.Invoice IV with (nolock) on IV.Id = AR.InvoiceId 
  left join Contract.CareGroup CG with (nolock) on CG.Id = IV.CareGroupId
where CR.Status in (2,4)
union all
SELECT 
TP.Nit as ThirdPartyNit
,TP.Name as ThirdPartyName
,MA.Number as AccountNumber
,MA.Name as AccountName
,'Factura' as TypeDocument
,AR.InvoiceNumber as DocumentNumber
,case when AR.OpeningBalance = 1 then 'Saldo CxC' when AR.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
,AR.Value as BillValueInitial
,case AR.Status when 3 then 0 else AR.Balance end as BillCurrentBalance
,AR.Code as MovesCode
,AR.AccountReceivableDate as MovesDate
,(case AR.OpeningBalance when 0 then AR.Value else (select top 1 Value from Portfolio.AccountReceivableAccounting where AccountReceivableId = AR.Id order by Id asc) end) as MovesDebit
,0 as MovesCredit
,'Cuenta Por Cobrar' as VoucherName
,CG.Id as IdCareGroup
,AR.Status as Status
,CG.Code as CareGroupCode
,AR.AccountReceivableType as AccountReceivableType
  FROM [Portfolio].[AccountReceivable] AR with (nolock)
  inner join Common.ThirdParty TP with (nolock)  on TP.Id = AR.ThirdPartyId
  inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = AR.[AccountWithoutRadicateId]
  left join Billing.Invoice IV with (nolock) on IV.Id = AR.InvoiceId 
  left join Contract.CareGroup CG with (nolock) on CG.Id = IV.CareGroupId
  --where AR.Status = 2 
union all ------ Movimientos de Facturas Anuladas
SELECT 
TP.Nit as ThirdPartyNit
,TP.Name as ThirdPartyName
,MA.Number as AccountNumber
,MA.Name as AccountName
,'Factura' as TypeDocument
,AR.InvoiceNumber as DocumentNumber
,case when AR.OpeningBalance = 1 then 'Saldo CxC' when AR.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
,AR.Value as BillValueInitial
,case AR.Status when 3 then 0 else AR.Balance end as BillCurrentBalance
,(select cast(jvt.Code as varchar(10))+ '-' +cast(jv.Consecutive as varchar(10)) 
	from GeneralLedger.JournalVouchers jv 
	inner join GeneralLedger.JournalVoucherTypes jvt on jv.IdJournalVoucher = jvt.Id where jv.Id = (select MAX(Id) from GeneralLedger.JournalVouchers where EntityCode = AR.InvoiceNumber and EntityName = 'Invoice')) as MovesCode
,i.AnnulmentDate as MovesDate
,0 as MovesDebit
,AR.Value as MovesCredit
,'C. Contable Anulacion CxC' as VoucherName
,CG.Id as IdCareGroup
,AR.Status as Status
,CG.Code as CareGroupCode
,AR.AccountReceivableType as AccountReceivableType
  FROM [Portfolio].[AccountReceivable] AR with (nolock)
  inner join Billing.Invoice i on i.Id = AR.InvoiceId
  inner join Common.ThirdParty TP with (nolock)  on TP.Id = AR.ThirdPartyId
  inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = AR.[AccountWithoutRadicateId]
  left join Billing.Invoice IV with (nolock) on IV.Id = AR.InvoiceId 
  left join Contract.CareGroup CG with (nolock) on CG.Id = IV.CareGroupId
  where AR.Status = 3
union all 
--- Listo todos los anticipos por recibos de caja
SELECT 
TP.Nit as ThirdPartyNit
,TP.Name as ThirdPartyName
,MA.Number as AccountNumber
,MA.Name as AccountName
,'Anticipo' as TypeDocument
,PA.Code as DocumentNumber
,case when PA.OpeningBalance = 1 then 'Saldo CxC' when PA.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
,PA.Value as BillValueInitial
,PA.Balance as BillCurrentBalance
,CR.Code as MovesCode
,CR.DocumentDate as MovesDate
,case when CRD.Nature = 1 then PA.Value else 0 end as MovesDebit
,case when CRD.Nature = 2 then PA.Value else 0 end as MovesCredit
,'Recibo De Caja' as VoucherName
,0 as IdCareGroup
,CR.Status as Status
,'' as CareGroupCode
,0 as AccountReceivableType
  FROM [Treasury].[CashReceipts] CR with (nolock)
  inner join Treasury.CashReceiptDetails CRD with (nolock) on CRD.IdCashReceipt = CR.Id 
  inner join Common.ThirdParty TP with (nolock) on TP.Id = CRD.IdThirdParty 
  inner join Portfolio.PortfolioAdvance PA with (nolock) on PA.CashReceiptDetailId = CRD.Id
  inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PA.MainAccountId
where CR.Status in (2)

union all 
--- Listo todos los anticipos por traslado
SELECT 
TP.Nit as ThirdPartyNit
,TP.Name as ThirdPartyName
,MA.Number as AccountNumber
,MA.Name as AccountName
,'Anticipo' as TypeDocument
,PA.Code as DocumentNumber
,case when PA.OpeningBalance = 1 then 'Saldo CxC' when PA.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
,PA.Value as BillValueInitial
,PA.Balance as BillCurrentBalance
,PT.Code as MovesCode
,PT.DocumentDate as MovesDate
,(SUM(PTD.Value) - ISNULL((select sum(ptoc.Value) from Portfolio.PortfolioTransferOtherConcept ptoc where PT.Id = ptoc.PortfolioTransferId And ptoc.Nature = 1),0) + ISNULL((select sum(ptoc.Value) from Portfolio.PortfolioTransferOtherConcept ptoc where PT.Id = ptoc.PortfolioTransferId And ptoc.Nature = 2),0)) as MovesDebit
,0 as MovesCredit
,'Cruce Ant Vs CxC' as VoucherName
,0 as IdCareGroup
,PT.Status as Status
,'' as CareGroupCode
,0 as AccountReceivableType
  FROM [Portfolio].[PortfolioTransfer] PT with (nolock)
  inner join Portfolio.PortfolioTransferDetail PTD with (nolock) on PTD.PortfolioTrasferId = PT.Id 
  inner join Portfolio.PortfolioAdvance PA with (nolock) on PA.Id = PT.PortfolioAdvanceId
  inner join Common.ThirdParty TP with (nolock) on TP.Id = PA.ThirdPartyId 
  inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PA.MainAccountId
where PT.Status in (2)
group by TP.Nit,TP.Name, MA.Number, MA.Name, PA.Code, PA.OpeningBalance, PA.Value, PA.Balance, PT.Code, PT.DocumentDate, PT.Status, PT.Id

union all 
--- Listo todos los anticipos por Nota
SELECT 
TP.Nit as ThirdPartyNit
,TP.Name as ThirdPartyName
,MA.Number as AccountNumber
,MA.Name as AccountName
,'Anticipo' as TypeDocument
,PA.Code as DocumentNumber
,case when PA.OpeningBalance = 1 then 'Saldo CxC' when PA.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
,PNARA.AdjusmentValue as BillValueInitial
,PNARA.Balance as BillCurrentBalance
,PN.Code as MovesCode
,PN.NoteDate as MovesDate
,case when PN.Nature = 1 then PND.Value else 0 end as MovesDebit
,case when PN.Nature = 2 then PND.Value else 0 end as MovesCredit
,case when PN.Nature = 1 then 'Nota Débito De CxC' else 'Nota Crédito De CxC' end as VoucherName
,0 as IdCareGroup
,PN.Status as Status
,'' as CareGroupCode
,0 as AccountReceivableType
  FROM [Portfolio].[PortfolioNote] PN with (nolock)
  inner join Portfolio.PortfolioNoteDetail PND with (nolock) on PND.PortfolioNoteId = PN.Id 
  inner join Portfolio.PortfolioNoteAccountReceivableAdvance PNARA with (nolock) on PNARA.PortfolioNoteId = PN.Id
  inner join Portfolio.PortfolioAdvance PA with (nolock) on PA.Id = PNARA.PortfolioAdvanceId
  inner join Common.ThirdParty TP with (nolock) on TP.Id = PA.ThirdPartyId 
  inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PA.MainAccountId
where PN.Status in (2)

union all 
--- Listo todos los anticipos por Nota de Distribucción
SELECT 
TP.Nit as ThirdPartyNit
,TP.Name as ThirdPartyName
,MA.Number as AccountNumber
,MA.Name as AccountName
,'Anticipo' as TypeDocument
,PA.Code as DocumentNumber
,case when PA.OpeningBalance = 1 then 'Saldo CxC' when PA.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
,PA.Value as BillValueInitial
,PA.Balance as BillCurrentBalance
,PN.Code as MovesCode
,PN.NoteDate as MovesDate
,(Select SUM(Value) From Portfolio.PortfolioNoteDistribution where PortfolioNoteId = PN.Id) as MovesDebit
,0 as MovesCredit
,case when PN.Nature = 1 then 'Nota Débito De Distribucción' else 'Nota Crédito De Distribucción' end as VoucherName
,0 as IdCareGroup
,PN.Status as Status
,'' as CareGroupCode
,0 as AccountReceivableType
  FROM [Portfolio].[PortfolioNote] PN with (nolock)
  inner join Portfolio.PortfolioAdvance PA with (nolock) on PA.Id = PN.PortfolioAdvanceId
  inner join Common.ThirdParty TP with (nolock) on TP.Id = PA.ThirdPartyId 
  inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PA.MainAccountId
where PN.Status in (2)

Union All
SELECT 
TP.Nit as ThirdPartyNit
,TP.Name as ThirdPartyName
,MA.Number as AccountNumber
,MA.Name as AccountName
,'Anticipo' as TypeDocument
,PA.Code as DocumentNumber
,case when PA.OpeningBalance = 1 then 'Saldo CxC' when PA.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
,PA.Value as BillValueInitial
,PA.Balance as BillCurrentBalance
,PN.Code as MovesCode
,PN.NoteDate as MovesDate
, 0 as MovesDebit
, PND.Value  MovesCredit
,case when PN.Nature = 1 then 'Nota Débito De Distribucción' else 'Nota Crédito De Distribucción' end as VoucherName
,0 as IdCareGroup
,PN.Status as Status
,'' as CareGroupCode
,0 as AccountReceivableType
  FROM [Portfolio].PortfolioNoteDistribution PND with (nolock)
  inner join Portfolio.PortfolioNote PN with (nolock) on PND.PortfolioNoteId = PN.Id 
  inner join Portfolio.PortfolioAdvance PA with (nolock) on PA.Id = PND.PortfolioAdvanceId
  inner join Common.ThirdParty TP with (nolock) on TP.Id = PA.ThirdPartyId 
  inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PA.MainAccountId
where PN.Status in (2)

union all 
--- Listo todos los anticipos por Comprobante Contable
SELECT 
TP.Nit as ThirdPartyNit
,TP.Name as ThirdPartyName
,MA.Number as AccountNumber
,MA.Name as AccountName
,'Anticipo' as TypeDocument
,PA.Code as DocumentNumber
,case when PA.OpeningBalance = 1 then 'Saldo CxC' when PA.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
,PA.Value as BillValueInitial
,PA.Balance as BillCurrentBalance
,VT.Code as MovesCode
,VT.DocumentDate as MovesDate
,sum(case when VTD.Nature = 1 then VTA.Value else 0 end) as MovesDebit
,sum(case when VTD.Nature = 2 then VTA.Value else 0 end) as MovesCredit
,'Reintegro de Anticipo' as VoucherName
,0 as IdCareGroup
,VT.Status as Status
,'' as CareGroupCode
,0 as AccountReceivableType
  FROM [Treasury].[VoucherTransaction] VT with (nolock) 
  inner join Treasury.VoucherTransactionDetails VTD with (nolock) on VTD.IdVoucherTransaction = VT.Id 
    inner join Treasury.VoucherTransactionAdvance VTA with (nolock) on VTA.IdVoucherTransactionD = VTD.Id
  inner join Portfolio.PortfolioAdvance PA with (nolock) on PA.Id = VTA.PortfolioAdvanceId
  inner join Common.ThirdParty TP with (nolock) on TP.Id = PA.ThirdPartyId 
  inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PA.MainAccountId
where VT.Status in (2)
group by TP.Nit,TP.Name,MA.Number,MA.Name,PA.Code,PA.OpeningBalance,PA.Value,PA.Balance,VT.Code,VT.DocumentDate,VTD.Nature,VT.Status

union all
--- listo los anticipos de saldo inicial
SELECT 
TP.Nit as ThirdPartyNit
,TP.Name as ThirdPartyName
,MA.Number as AccountNumber
,MA.Name as AccountName
,'Anticipo' as TypeDocument
,PA.Code as DocumentNumber
,case when PA.OpeningBalance = 1 then 'Saldo CxC' when PA.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
,PA.Value as BillValueInitial
,PA.Balance as BillCurrentBalance
,PA.Code as MovesCode
,PA.DocumentDate as MovesDate
,case MA.Nature when 1 then PA.Value else 0 end as MovesDebit
,case MA.Nature when 2 then PA.Value else 0 end as MovesCredit
,'Saldo Inicial' as VoucherName
,0 as IdCareGroup
,PA.Status as Status
,'' as CareGroupCode
,0 as AccountReceivableType
  FROM Portfolio.PortfolioAdvance PA with (nolock) 
  inner join Common.ThirdParty TP with (nolock) on TP.Id = PA.ThirdPartyId 
  inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PA.MainAccountId
where PA.Status in (2) AND PA.OpeningBalance = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista temporal de extracción para el reporte de cuentas por cobrar de cartera. Consolida en un único resultado todos los movimientos que afectan las cuentas por cobrar de facturas emitidas a terceros (EPS, aseguradoras, empresas): notas débito y crédito de cartera, cruces de anticipos contra CxC, cruces de cuenta de tesorería, recibos de caja y el saldo inicial o apertura de la cuenta por cobrar. Para cada movimiento expone el NIT y nombre del tercero pagador, el número y nombre de la cuenta contable (PUC), el número de factura, el valor original y saldo actual de la CxC, el código y fecha del comprobante, los valores en débito y crédito, el tipo de voucher (nota débito, nota crédito, cruce de anticipo, cruce de cuenta, recibo de caja), el grupo de atención del contrato y el tipo de cuenta por cobrar. Sirve como base de datos intermedia para informes de estado de cartera, antigüedad de saldos por cobrar, conciliación contable y seguimiento de recaudo por entidad pagadora.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'VReportExtractAccountReceivableTmp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'VReportExtractAccountReceivableTmp';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único conjunto los movimientos contables de cuentas por cobrar y anticipos de cartera (notas, traslados, cruces, recibos de caja, comprobantes de tesorería, anulaciones y saldos iniciales) para alimentar reportes de extracto de cartera.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportExtractAccountReceivableTmp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los documentos fuente deben estar en estado válido: notas de cartera (PortfolioNote.Status=2), traslados (PortfolioTransfer.Status=2), cruces (CrossingAccount.Status=2), recibos de caja (CashReceipts.Status IN (2,4) para CxC y =2 para anticipos), comprobantes de tesorería (VoucherTransaction.Status=2) y anticipos de saldo inicial (PortfolioAdvance.Status=2 AND OpeningBalance=1); Cada cuenta por cobrar debe estar asociada a un tercero (ThirdParty) y a una cuenta contable principal (MainAccounts); Para movimientos de anulación, la factura asociada (Billing.Invoice) debe existir y la cuenta por cobrar debe tener Status=3', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportExtractAccountReceivableTmp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todo movimiento queda etiquetado siempre como TypeDocument ''Factura'' (cuando proviene de AccountReceivable) o ''Anticipo'' (cuando proviene de PortfolioAdvance); Los movimientos de anticipos no asocian grupo de atención: IdCareGroup = 0, CareGroupCode = '''' y AccountReceivableType = 0; Los movimientos sólo reflejan documentos en estado confirmado/aplicado (Status = 2), excepto recibos de caja sobre CxC que también incluyen Status = 4 y CxC anuladas (Status = 3) que se reportan exclusivamente en el bloque de anulación; El cálculo de MovesDebit en traslados de anticipos descuenta los otros conceptos de naturaleza 1 y suma los de naturaleza 2 sobre el total del detalle; El código del comprobante contable de anulación se obtiene del último JournalVouchers (MAX(Id)) cuyo EntityCode coincide con el InvoiceNumber y EntityName=''Invoice''; Cada bloque agrupa el valor de movimiento por la combinación de tercero, cuenta contable, documento y comprobante para evitar duplicar líneas detalladas', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportExtractAccountReceivableTmp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por cobrar; Anticipo de cartera; Nota débito de cartera; Nota crédito de cartera; Recibo de caja; Cruce de cuentas; Traslado de cartera; Comprobante contable; Reintegro de anticipo; Saldo inicial; Anulación de factura; Tercero; Plan de cuentas (PUC); Grupo de atención (CareGroup); Distribución de notas', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportExtractAccountReceivableTmp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.VReportExtractAccountReceivableTmp: Devuelve la unión de 13 bloques que tipifican cada movimiento con un VoucherName fijo (Nota Débito/Crédito De CxC, Cruce Ant Vs CxC, Cruce De Cuenta, Recibo De Caja, Cuenta Por Cobrar, C. Contable Anulacion CxC, Nota Débito/Crédito De Distribucción, Reintegro de Anticipo, Saldo Inicial)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportExtractAccountReceivableTmp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AR.OpeningBalance = 1 → EntityName = ''Saldo CxC'' else Si OpeningBalance = 0, EntityName = ''Saldo Inicial''; si PN.Nature = 1 (nota débito) → MovesDebit = valor del ajuste y VoucherName = ''Nota Débito De CxC'' (o ''Nota Débito De Distribucción'') else Si Nature = 2 se asigna MovesCredit y VoucherName de tipo Crédito; si CRD.Nature = 1 en recibos de caja → El valor se imputa como MovesDebit else Si Nature = 2 se imputa como MovesCredit; si AR.Status = 3 (cuenta por cobrar anulada) → BillCurrentBalance se fuerza a 0 y se incluye un movimiento adicional ''C. Contable Anulacion CxC'' con MovesCredit = AR.Value y MovesDate = Invoice.AnnulmentDate else Se mantiene AR.Balance como saldo actual; si AR.OpeningBalance = 0 en bloque de Cuenta Por Cobrar → MovesDebit = AR.Value else MovesDebit = primer Value de AccountReceivableAccounting (TOP 1 ORDER BY Id ASC) — saldo inicial; si MA.Nature = 1 en anticipos de saldo inicial → MovesDebit = PA.Value else Si Nature = 2, MovesCredit = PA.Value; si CR.Status IN (2,4) para recibos aplicados a CxC → Se incluyen recibos confirmados y un cuarto estado adicional else Para anticipos por recibo de caja sólo se acepta CR.Status = 2', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportExtractAccountReceivableTmp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioNoteAccountReceivableAdvance; Portfolio.PortfolioNote; Portfolio.AccountReceivable; Common.ThirdParty; Portfolio.AccountReceivableAccounting; Billing.Invoice; Contract.CareGroup; GeneralLedger.MainAccounts; Portfolio.PortfolioTransferDetail; Portfolio.PortfolioTransfer; Treasury.CrossingAccountDetailCxC; Treasury.CrossingAccount; Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashReceiptAccountReceivable; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes; Portfolio.PortfolioAdvance; Portfolio.PortfolioTransferOtherConcept; Portfolio.PortfolioNoteDetail; Portfolio.PortfolioNoteDistribution; Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.VoucherTransactionAdvance', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportExtractAccountReceivableTmp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportExtractAccountReceivableTmp';
GO
