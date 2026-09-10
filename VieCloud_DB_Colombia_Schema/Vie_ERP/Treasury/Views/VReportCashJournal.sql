

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Libro diario de caja (Cash Journal) que consolida en una sola consulta todos los movimientos de tesorería registrados en las cajas de la organización. Integra siete tipos de documentos financieros: comprobantes de egreso (pagos a terceros con su desglose por cuenta), recibos de caja (ingresos), consignaciones bancarias, notas de tesorería de ajuste y notas asociadas a comprobantes de egreso. Para cada movimiento expone la caja origen, la fecha del documento, el código y nombre del comprobante, el tercero beneficiario o pagador (NIT y nombre), el valor al débito, el valor al crédito, el tipo de documento, el estado, el método de pago y el número de factura o cuenta por pagar relacionada cuando aplica. Es la vista principal para reportería de libro diario de caja, conciliación de movimientos, auditoría de tesorería y cuadre de cajas por período.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'VReportCashJournal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'VReportCashJournal';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un libro/diario de caja todos los movimientos de tesorería (comprobantes de egreso, recibos de caja, consignaciones y notas) en un único listado numerado y ordenado por fecha del documento.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportCashJournal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada movimiento debe estar asociado a una caja registradora (Treasury.CashRegisters) vía sus respectivas claves (IdCashRegister/CashRegisterId).; Para incluirse como Recibo de Caja se requiere un método de pago registrado (INNER JOIN Treasury.PaymentMethods).; Para incluirse como Consignación deben existir tanto el detalle (ConsignmentDetail) como el encabezado (Consignment) y la caja asociada.; Las notas de tesorería sólo se incluyen si NoteType = 2 (nota directa sobre caja) o NoteType = 3 (nota asociada a una transacción de comprobante).', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportCashJournal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila pertenece exactamente a una caja registradora identificada por CashRegisterId/Code/Name.; ValueDebit y ValueCredit son mutuamente excluyentes por fila (siempre uno de los dos es 0), salvo el caso de notas tipo 2 donde se decide por TN.Nature.; VoucherType codifica el tipo de documento: 1=Recibo de Caja, 2=Comprobante de Egreso, 3=Consignación, 4=Nota.; PaymentMethod por defecto es 1 (efectivo/comprobante) excepto en recibos de caja (toma PM.PaymentMethodTypes) y consignaciones (=4).; Las consignaciones y notas de tipo 2 nunca exponen tercero real: usan literales fijos (''Documento De Consignacion'' / ''Documento De Nota'').; BillNumber sólo se resuelve para comprobantes de egreso (vía DischargeBill→AccountPayable) y recibos de caja (vía CashReceiptAccountReceivable); en los demás casos es NULL.; El campo Row es un consecutivo derivado del orden ascendente por DocumentDate del conjunto resultante.; Todas las lecturas se hacen WITH (NOLOCK), es decir sin bloqueos y aceptando lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportCashJournal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante de egreso; Recibo de caja; Consignación bancaria; Nota de tesorería (débito/crédito); Caja registradora; Tercero (NIT); Cuenta por pagar / factura; Método de pago; Naturaleza débito/crédito; Libro/diario de caja', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportCashJournal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Treasury.VReportCashJournal: Devuelve el conjunto unificado con un Row consecutivo asignado por ROW_NUMBER() OVER(ORDER BY DocumentDate ASC) sobre la unión de los seis sub-conjuntos.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportCashJournal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen = Treasury.VoucherTransaction (encabezado con IdCashRegister) → Se etiqueta NameVoucher=''Comprobante de Egreso'', VoucherType=2, PaymentMethod=1, ValueCredit=VT.Value y ValueDebit=0; se intenta resolver BillNumber vía DischargeBill→AccountPayable.; si Origen = Treasury.CashReceipts → Se etiqueta NameVoucher=''Recibo de Caja'', VoucherType=1, ValueDebit=CRC.Value y ValueCredit=0; PaymentMethod toma PM.PaymentMethodTypes y BillNumber se obtiene de CashReceiptAccountReceivable.InvoiceNumber.; si Origen = Treasury.ConsignmentDetail + Consignment → Se etiqueta NameVoucher=''Consignación'', VoucherType=3, PaymentMethod=4, ValueCredit=CD.Value, ThirdPartyNit='''' y ThirdPartyName=''Documento De Consignacion''; BillNumber=null.; si Origen = Treasury.VoucherTransactionDetails con CashRegisterId propio → Se etiqueta NameVoucher=''Comprobante de Egreso'', VoucherType=2, PaymentMethod=1, ValueDebit=VTD.Value y ValueCredit=0; tercero tomado de VTD.IdThirdParty.; si Origen = Treasury.TreasuryNote con NoteType = 2 → Se etiqueta NameVoucher=''Nota'', VoucherType=4; ValueDebit=TN.Value cuando Nature=1 y ValueCredit=TN.Value cuando Nature=2 (IIF), sin tercero (ThirdPartyName=''Documento De Nota'').; si Origen = Treasury.TreasuryNote con NoteType = 3 ligada a VoucherTransaction → Se etiqueta NameVoucher=''Nota'', VoucherType=4, PaymentMethod=1, ValueDebit=TN.Value (encabezado) o VTD.Value (detalle) y ValueCredit=0; tercero tomado de la transacción/detalle.; si TN.NoteType distinto de 2 o 3 → Se excluye de la vista (no aparece en ningún UNION).', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportCashJournal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.CashRegisters; Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashReceiptAccountReceivable; Treasury.PaymentMethods; Treasury.Consignment; Treasury.ConsignmentDetail; Treasury.TreasuryNote; Treasury.DischargeBill; Payments.AccountPayable; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportCashJournal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportCashJournal';
GO
