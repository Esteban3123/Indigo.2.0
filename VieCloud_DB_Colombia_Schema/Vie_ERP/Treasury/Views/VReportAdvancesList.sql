

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte consolidado de movimientos de anticipos a proveedores. Integra en una sola consulta cuatro tipos de transacciones asociadas a un anticipo: el registro inicial del anticipo, los reintegros mediante recibos de caja, los ajustes por notas de pago y los traslados de pago. Para cada movimiento expone el tercero (NIT y nombre del proveedor), la cuenta contable, el código del comprobante, la fecha, el valor en débito o crédito, el tipo de comprobante (Anticipo, Reintegro RC, Nota de Pago, Traslado de Pago), el saldo vigente del anticipo y la moneda. Sirve para reportería de tesorería y cuentas por pagar, permitiendo auditar el ciclo completo de un anticipo: su creación, aplicación, devolución o traslado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'VReportAdvancesList';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'VReportAdvancesList';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado los movimientos contables (débitos y créditos) que afectan los anticipos a proveedores: la creación del anticipo, los reintegros vía recibo de caja, las notas de pago y los traslados de pago, todos en estado activo.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportAdvancesList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los anticipos deben tener proveedor (Common.Supplier), tercero (Common.ThirdParty), cuenta contable (GeneralLedger.MainAccounts) y moneda (Common.Currency) válidos y relacionados.; Los recibos de caja considerados deben tener detalles ligados a un anticipo vía Treasury.CashReceiptAdvancePayment.; Las notas de pago consideradas deben tener registros en Payments.PaymentNotesAccountPayableAdvance vinculados al anticipo.; Los traslados de pago deben tener AdvancePaymentId, ThirdPartyId y MainAccountId válidos.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportAdvancesList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan documentos en estado 2 (activo/confirmado) en cada uno de los cuatro orígenes.; Un mismo anticipo (AdvanceId) puede aparecer en múltiples filas, una por cada movimiento que lo afecta.; Los importes monetarios MovesDebit y MovesCredit se devuelven siempre como DECIMAL(20,2).; En cada fila, MovesDebit y MovesCredit son mutuamente excluyentes: cuando uno tiene valor, el otro es 0.; El concepto NameVoucher identifica el origen del movimiento: ''Anticipo'', ''Reintegro RC'', ''Nota De Pago'' o ''Traslado De Pago''.; Todos los joins exigen que el anticipo tenga moneda asociada en Common.Currency.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportAdvancesList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Anticipo a proveedor; Reintegro por recibo de caja; Nota de pago (débito/crédito); Traslado de pago; Cuenta contable (PUC); Tercero/Proveedor (NIT); Saldo de anticipo; Naturaleza débito/crédito; Moneda/divisa', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportAdvancesList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Por cada anticipo con Status=2 se retorna una fila tipo ''Anticipo'' con MovesDebit = AP.Value y MovesCredit = 0.; [RETURN_RESULT] resultset: Por cada recibo de caja con CR.Status=2 vinculado a un anticipo se retorna una fila tipo ''Reintegro RC'' con MovesDebit = 0 y MovesCredit = CRAP.PaymentValue.; [RETURN_RESULT] resultset: Por cada nota de pago con PN.Status=2 sobre un anticipo se retorna fila ''Nota De Pago'' con MovesDebit = PNAPA.AdjusmentValue cuando PN.Nature=1, o MovesCredit = PNAPA.AdjusmentValue cuando PN.Nature=2.; [RETURN_RESULT] resultset: Por cada traslado de pago con PT.Status=2 se retorna fila ''Traslado De Pago'' con MovesCredit = (ValueDetails + ValueDebit) - ValueCredit y MovesDebit = 0; ValueDebit suma valores con Nature=1 y ValueCredit suma valores con Nature=2 de PaymentTransferOtherConcept.; [RETURN_RESULT] resultset: Se asigna Row con ROW_NUMBER() ordenado ASC por MovesDate sobre la unión de los cuatro orígenes.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportAdvancesList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AP.Status = 2 (anticipo activo) → Incluye el anticipo como movimiento débito ''Anticipo'' else Excluye el anticipo del reporte; si CR.Status = 2 (recibo de caja activo) → Incluye reintegro como movimiento crédito ''Reintegro RC'' else Excluye el recibo de caja; si PN.Status = 2 (nota de pago activa) → Incluye la nota de pago else Excluye la nota de pago; si PN.Nature = 1 → El valor PNAPA.AdjusmentValue se asigna a MovesDebit else Si PN.Nature=2 se asigna a MovesCredit; en cualquier otro caso ambos quedan en 0; si PT.Status = 2 (traslado de pago activo) → Incluye el traslado como ''Traslado De Pago'' else Excluye el traslado; si ptoc.Nature = 2 → Suma de Value se acumula como ValueCredit del traslado else Si Nature=1 se acumula como ValueDebit; otros se ignoran (0)', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportAdvancesList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AdvancePayments; Common.Supplier; Common.ThirdParty; GeneralLedger.MainAccounts; Common.Currency; Treasury.CashReceiptAdvancePayment; Treasury.CashReceiptDetails; Treasury.CashReceipts; Payments.PaymentNotesAccountPayableAdvance; Payments.PaymentNotes; Payments.PaymentTransfer; Payments.PaymentTransferDetail; Payments.PaymentTransferOtherConcept', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportAdvancesList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportAdvancesList';
GO
