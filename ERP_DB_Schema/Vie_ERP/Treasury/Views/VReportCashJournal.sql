


CREATE VIEW [Treasury].[VReportCashJournal]
as
select ROW_NUMBER() OVER(ORDER BY DocumentDate ASC) as Row ,* from (
SELECT 'Comprobante de Egreso' as NameVoucher
		,CR.Id as CashRegisterId
		,VT.DocumentDate as DocumentDate
		,CR.Code as CashRegisterCode
		,CR.Name as CashRegisterName
		,0 as ValueDebit
		,VT.Value as ValueCredit
		,TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,VT.Code as Code
		,VT.Detail as Detail
		,2 as VoucherType
		,VT.Status as Status
		,1 as PaymentMethod
		,subconsulta.BillNumber as BillNumber
  FROM  Treasury.VoucherTransaction VT with (nolock) 
   inner join Treasury.CashRegisters CR with (nolock) on CR.Id = VT.IdCashRegister 
   left join Common.ThirdParty TP with (nolock) on TP.Id = VT.IdThirdParty left join 
  (select AP.BillNumber, VT.Id 
  from  Treasury.VoucherTransaction VT with (nolock)
  inner join Treasury.VoucherTransactionDetails VTD with (nolock) on VT.Id = VTD.IdVoucherTransaction 
  left join treasury.DischargeBill DB with (nolock) on VTD.Id = DB.IdVoucherTransactionD
  left join Payments.AccountPayable AP with (nolock)on AP.Id = DB.IdAccountPayable where VT.IdCashRegister is not null) as subconsulta  on subconsulta.Id = VT.Id
  UNION ALL
  SELECT 'Recibo de Caja' as NameVoucher
		,CR.Id as CashRegisterId
		,CRC.DocumentDate as DocumentDate
		,CR.Code as CashRegisterCode
		,CR.Name as CashRegisterName
		,CRC.Value as ValueDebit
		,0 as ValueCredit
		,TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,CRC.Code as Code
		,CRC.Detail as Detail
		,1 as VoucherType
		,CRC.Status as Status
		,PM.PaymentMethodTypes as PaymentMethod
		,subconsulta.BillNumber as BillNumber
		FROM Treasury.CashReceipts CRC with (nolock)
		inner join Treasury.CashRegisters CR with (nolock) on CR.Id = CRC.IdCashRegister 
		inner join Treasury.PaymentMethods PM  with (nolock) on CRC.Id = PM.IdCashReceipt 
		left join Common.ThirdParty TP with (nolock) on TP.Id = CRC.IdThirdParty 
		left join
		(select CRAR.InvoiceNumber as BillNumber, CR.Id , CR.Code
		from Treasury.CashReceipts CR with (nolock) 
		inner join Treasury.CashReceiptDetails CRD with (nolock) on CR.Id = CRD.IdCashReceipt 
		inner join treasury.CashReceiptAccountReceivable CRAR with (nolock) on CRD.Id = CRAR.CashReceiptDetailId 
		where CR.IdCashRegister is not null) as subconsulta  on subconsulta.Id = CRC.Id
  UNION ALL
  SELECT 'Consignación' as NameVoucher
		,CR.Id as CashRegisterId
		,C.DocumentDate as DocumentDate
		,CR.Code as CashRegisterCode
		,CR.Name as CashRegisterName
		,0 as ValueDebit
		,CD.Value as ValueCredit
		,'' as ThirdPartyNit
		,'Documento De Consignacion' as ThirdPartyName
		,C.Code as Code
		,C.Description as Detail
		,3 as VoucherType
		,C.Status as Status
		,4 as PaymentMethod
		,null as BillNumber
		FROM Treasury.ConsignmentDetail CD with (nolock)
		inner join Treasury.Consignment C with (nolock) on  C.Id = CD.ConsignmentTransferId 
		inner join Treasury.CashRegisters CR with (nolock) on CR.Id = CD.CashRegisterId
	UNION ALL
	SELECT 'Comprobante de Egreso' as NameVoucher
		,CR.Id as CashRegisterId
		,VT.DocumentDate as DocumentDate
		,CR.Code as CashRegisterCode
		,CR.Name as CashRegisterName
		,VTD.Value as ValueDebit
		,0 as ValueCredit
		,TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,VT.Code as Code
		,VTD.Detail as Detail
		,2 as VoucherType
		,VT.Status as Status
		,1 as PaymentMethod
		,null as BillNumber
  FROM  Treasury.VoucherTransactionDetails VTD with (nolock)
  inner join  Treasury.VoucherTransaction VT  with (nolock) on VT.Id = VTD.IdVoucherTransaction 
  inner join Treasury.CashRegisters CR with (nolock) on CR.Id = VTD.CashRegisterId  
		left join Common.ThirdParty TP with (nolock) on TP.Id = VTD.IdThirdParty
	UNION ALL
	SELECT 'Nota' as NameVoucher
		,CR.Id as CashRegisterId
		,TN.NoteDate as DocumentDate
		,CR.Code as CashRegisterCode
		,CR.Name as CashRegisterName
		,IIF(TN.Nature = 1, TN.Value, 0) as ValueDebit
		,IIF(TN.Nature = 2, TN.Value, 0) as ValueCredit
		,'' as ThirdPartyNit
		,'Documento De Nota' as ThirdPartyName
		,TN.Code as Code
		,TN.Description as Detail
		,4 as VoucherType
		,TN.Status as Status
		,1 as PaymentMethod
		,null as BillNumber
  FROM  Treasury.TreasuryNote TN with (nolock)
  inner join Treasury.CashRegisters CR with (nolock) on CR.Id = TN.CashRegisterId where TN.NoteType = 2
  UNION ALL
	SELECT 'Nota' as NameVoucher
		,CR.Id as CashRegisterId
		,TN.NoteDate as DocumentDate
		,CR.Code as CashRegisterCode
		,CR.Name as CashRegisterName
		,TN.Value as ValueDebit
		,0 as ValueCredit
		,TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,TN.Code as Code
		,TN.Description as Detail
		,4 as VoucherType
		,TN.Status as Status
		,1 as PaymentMethod
		,null as BillNumber
  FROM  Treasury.TreasuryNote TN with (nolock)
  inner join Treasury.VoucherTransaction VT with (nolock) on VT.Id = TN.VoucherTransactionId 
  inner join Treasury.CashRegisters CR with (nolock) on CR.Id = VT.IdCashRegister 
		left join Common.ThirdParty TP with (nolock) on TP.Id = VT.IdThirdParty	where TN.NoteType = 3
  UNION ALL
	SELECT 'Nota' as NameVoucher
		,CR.Id as CashRegisterId
		,TN.NoteDate as DocumentDate
		,CR.Code as CashRegisterCode
		,CR.Name as CashRegisterName
		,VTD.Value as ValueDebit
		,0 as ValueCredit
		,TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,TN.Code as Code
		,TN.Description as Detail
		,4 as VoucherType
		,TN.Status as Status
		,1 as PaymentMethod
		,null as BillNumber
  FROM  Treasury.TreasuryNote TN with (nolock)
  inner join Treasury.VoucherTransaction VT with (nolock) on VT.Id = TN.VoucherTransactionId 
  inner join Treasury.VoucherTransactionDetails VTD  with (nolock) on VT.Id = VTD.IdVoucherTransaction 
  inner join Treasury.CashRegisters CR with (nolock) on CR.Id = VT.IdCashRegister 
  left join Common.ThirdParty TP with (nolock) on TP.Id = VTD.IdThirdParty where TN.NoteType = 3) as Datos