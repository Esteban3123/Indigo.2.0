










CREATE VIEW [Treasury].[VReportAdvancesList]
as
select ROW_NUMBER() OVER(ORDER BY MovesDate ASC) as Row ,* from (
SELECT 
CTP.Nit as ThirdPartyNit
,CTP.Name as ThirdPartyName
,GMA.Number as AccountNumber
,GMA.Name as AccountName
,AP.Value as BillValueInitial
,AP.Balance as BillCurrentBalance
,AP.Id as AdvanceId
,AP.Code as AdvanceCode
,AP.Code as MovesCode
,AP.DocumentDate as MovesDate
,CAST(AP.Value AS DECIMAL(20,2)) as MovesDebit
,CAST(0 AS DECIMAL(20,2)) as MovesCredit
,'Anticipo' as NameVoucher
,AP.Status as Status
,C.Abbreviation AS Abbreviation
  FROM [Payments].[AdvancePayments] AP with (nolock)
  inner join Common.Supplier CS with (nolock) on AP.IdSupplier = CS.Id
  inner join common.ThirdParty CTP with (nolock) on CS.IdThirdParty = CTP.Id 
  inner join GeneralLedger.MainAccounts GMA with (nolock) on AP.IdAccount = GMA.Id
  INNER JOIN Common.Currency C WITH (NOLOCK) ON C.Id = AP.CurrencyId
  where AP.Status = 2
  union all
SELECT 
CTP.Nit as ThirdPartyNit
,CTP.Name as ThirdPartyName
,GMA.Number as AccountNumber
,GMA.Name as AccountName
,AP.Value as BillValueInitial
,AP.Balance as BillCurrentBalance
,AP.Id as AdvanceId
,AP.Code as AdvanceCode
,CR.Code as MovesCode
,CR.DocumentDate as MovesDate
,CAST(0 AS DECIMAL(20,2)) as MovesDebit
,CAST(CRAP.PaymentValue AS DECIMAL(20,2)) as MovesCredit
,'Reintegro RC' as NameVoucher
,CR.Status as Status
,C.Abbreviation AS Abbreviation
  FROM [Treasury].[CashReceiptAdvancePayment] CRAP with (nolock) 
  inner join Treasury.CashReceiptDetails CRD with (nolock) on CRD.Id = CRAP.CashReceiptDetailId
  inner join Treasury.CashReceipts CR with (nolock) on CR.Id = CRD.IdCashReceipt 
  inner join Common.ThirdParty CTP with (nolock) on CTP.Id = CR.IdThirdParty
  inner join GeneralLedger.MainAccounts GMA with (nolock) on GMA.Id = CRD.IdMainAccount
  inner join Payments.AdvancePayments AP with (nolock) on AP.Id = CRAP.AdvancePaymentId 
  INNER JOIN Common.Currency C WITH (NOLOCK) ON C.Id = AP.CurrencyId
  where CR.Status = 2
  union all
SELECT 
CTP.Nit as ThirdPartyNit
,CTP.Name as ThirdPartyName
,GMA.Number as AccountNumber
,GMA.Name as AccountName
,AP.Value as BillValueInitial
,AP.Balance as BillCurrentBalance
,AP.Id as AdvanceId
,AP.Code as AdvanceCode
,PN.Code as MovesCode
,PN.NoteDate as MovesDate
,case when PN.Nature = 1 then CAST(PNAPA.AdjusmentValue AS DECIMAL(20,2)) else CAST(0 AS DECIMAL(20,2)) end as MovesDebit
,case when PN.Nature = 2 then CAST(PNAPA.AdjusmentValue AS DECIMAL(20,2)) else CAST(0 AS DECIMAL(20,2)) end as MovesCredit
,'Nota De Pago' as NameVoucher
,PN.Status as Status
,C.Abbreviation AS Abbreviation
  FROM [Payments].[PaymentNotesAccountPayableAdvance] PNAPA with (nolock)
  inner join Payments.PaymentNotes PN with (nolock) on PN.Id = PNAPA.PaymentNoteId 
  inner join Common.Supplier CS with (nolock) on CS.Id = PN.IdSupplier 
  inner join Common.ThirdParty CTP with (nolock) on CTP.Id= CS.IdThirdParty
  inner join Payments.AdvancePayments AP with (nolock) on AP.Id = PNAPA.AdvancePaymentId 
  inner join GeneralLedger.MainAccounts GMA with (nolock) on GMA.Id = AP.IdAccount
  INNER JOIN Common.Currency C WITH (NOLOCK) ON C.Id = AP.CurrencyId
 where PN.Status = 2
 union all
SELECT 
CTP.Nit as ThirdPartyNit
,CTP.Name as ThirdPartyName
,GMA.Number as AccountNumber
,GMA.Name as AccountName
,AP.Value as BillValueInitial
,AP.Balance as BillCurrentBalance
,AP.Id as AdvanceId
,AP.Code as AdvanceCode
,PT.Code as MovesCode
,PT.DocumentDate as MovesDate
,CAST(0 AS DECIMAL(20,2)) as MovesDebit
,CAST(((coalesce(ptd.ValueDetails, 0) + coalesce(ptoc.ValueDebit, 0)) - coalesce(ptoc.ValueCredit, 0)) AS DECIMAL(20,2)) as MovesCredit
,'Traslado De Pago' as NameVoucher
,PT.Status as Status
,C.Abbreviation AS Abbreviation
  FROM [Payments].[PaymentTransfer] PT with (nolock)
  inner join Common.ThirdParty CTP with (nolock) on CTP.Id = PT.ThirdPartyId
  inner join GeneralLedger.MainAccounts GMA with (nolock) on GMA.Id = PT.MainAccountId
  inner join Payments.AdvancePayments AP with (nolock) on AP.Id = PT.AdvancePaymentId
  left join (select pt.Id as PaymentTransferId, sum(ptd.Value) as ValueDetails from Payments.PaymentTransfer pt inner join Payments.PaymentTransferDetail ptd on pt.Id=ptd.PaymentTransferId group by pt.Id) ptd on pt.Id=ptd.PaymentTransferId
  left join (select pt.Id as PaymentTransferId, CASE ptoc.Nature WHEN 2 THEN sum(coalesce(ptoc.Value, 0)) ELSE 0 END as ValueCredit, CASE ptoc.Nature WHEN 1 THEN sum(coalesce(ptoc.Value, 0)) ELSE 0 END as ValueDebit from Payments.PaymentTransfer pt left join Payments.PaymentTransferOtherConcept ptoc on ptoc.PaymentTransferId=pt.Id group by pt.Id, ptoc.Nature) ptoc on ptoc.PaymentTransferId=pt.Id
  INNER JOIN Common.Currency C WITH (NOLOCK) ON C.Id = AP.CurrencyId
where PT.Status =2
  ) as Datos