



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