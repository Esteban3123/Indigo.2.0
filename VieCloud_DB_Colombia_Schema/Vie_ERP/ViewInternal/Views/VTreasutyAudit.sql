

CREATE view [ViewInternal].[VTreasutyAudit]
as (
select 'Recibos de caja' as Entity,cr.Code,DocumentDate, cr.CreationUser,Detail, cr.Value as TreasuryValue, isnull(b.Value,0) as GeneralValue, c.Code + ' - ' + c.Name as Cash, eba.Code + ' - ' + eba.Number as EntityBank
from Treasury.CashReceipts cr
left join Treasury.CashRegisters c on c.Id = cr.IdCashRegister
left join Treasury.EntityBankAccounts eba on eba.Id = cr.IdBankAccount
left join (
select EntityCode, sum(DebitValue) as Value
from GeneralLedger.JournalVouchers jv
inner join GeneralLedger.JournalVoucherDetails jvd on jv.Id = jvd.IdAccounting
inner join GeneralLedger.MainAccounts ma on ma.Id = jvd.IdMainAccount and ma.LegalBookId = 1
where EntityName = 'CashReceipts'
group by EntityCode
) as b on b.EntityCode = cr.Code
where cr.Value <> isnull(b.Value,0) and cr.Status in (2,4)
union all
select 'Comprobante de Egreso' as Entity,vt.Code,DocumentDate,vt.CreationUser,Detail, vt.Value as TreasuryValue, isnull(b.Value,0) as GeneralValue, c.Code + ' - ' + c.Name as Cash, eba.Code + ' - ' + eba.Number as EntityBank
from Treasury.VoucherTransaction vt
left join Treasury.CashRegisters c on c.Id = vt.IdCashRegister
left join Treasury.EntityBankAccounts eba on eba.Id = vt.IdEntityBankAccount
left join (
select EntityCode, sum(DebitValue) as Value
from GeneralLedger.JournalVouchers jv
inner join GeneralLedger.JournalVoucherDetails jvd on jv.Id = jvd.IdAccounting
inner join GeneralLedger.MainAccounts ma on ma.Id = jvd.IdMainAccount and ma.LegalBookId = 1
where EntityName = 'VoucherTransaction'
group by EntityCode
) b on vt.Code = b.EntityCode
where vt.Value <> isnull(b.Value,0) and vt.Status in (2,4)
union all
select 'Notas DB/CR' as Entity,tn.Code,tn.NoteDate as DocumentDate,tn.CreationUser,tn.Description as Detail, tn.Value as TreasuryValue, isnull(b.Value,0) as GeneralValue, c.Code + ' - ' + c.Name as Cash, eba.Code + ' - ' + eba.Number as EntityBank
from Treasury.TreasuryNote tn
left join Treasury.CashRegisters c on c.Id = tn.CashRegisterId
left join Treasury.EntityBankAccounts eba on eba.Id = tn.EntityBankAccountId
left join (
select EntityCode, sum(DebitValue) as Value
from GeneralLedger.JournalVouchers jv
inner join GeneralLedger.JournalVoucherDetails jvd on jv.Id = jvd.IdAccounting
inner join GeneralLedger.MainAccounts ma on ma.Id = jvd.IdMainAccount and ma.LegalBookId = 1
where EntityName = 'TreasuryNote'
group by EntityCode
) b on tn.Code = b.EntityCode
where tn.Value <> isnull(b.Value,0) and tn.Status in (2,4) and NoteType in (1,2)
union all
select 'Notas Rev CE' as Entity,tn.Code,tn.NoteDate as DocumentDate,tn.CreationUser,tn.Description as Detail, vt.Value as TreasuryValue, isnull(b.Value,0) as GeneralValue, c.Code + ' - ' + c.Name as Cash, eba.Code + ' - ' + eba.Number as EntityBank
from Treasury.TreasuryNote tn
inner join Treasury.VoucherTransaction vt on vt.Id = tn.VoucherTransactionId
left join Treasury.CashRegisters c on c.Id = tn.CashRegisterId
left join Treasury.EntityBankAccounts eba on eba.Id = tn.EntityBankAccountId
left join (
select EntityCode, sum(DebitValue) as Value
from GeneralLedger.JournalVouchers jv
inner join GeneralLedger.JournalVoucherDetails jvd on jv.Id = jvd.IdAccounting
inner join GeneralLedger.MainAccounts ma on ma.Id = jvd.IdMainAccount and ma.LegalBookId = 1
where EntityName = 'TreasuryNote'
group by EntityCode
) b on tn.Code = b.EntityCode
where vt.Value <> isnull(b.Value,0) and tn.Status in (2,4) and NoteType = 3
union all
select 'Notas Rev RC' as Entity,tn.Code,tn.NoteDate as DocumentDate,tn.CreationUser,tn.Description as Detail, cr.Value as TreasuryValue, isnull(b.Value,0) as GeneralValue, c.Code + ' - ' + c.Name as Cash, eba.Code + ' - ' + eba.Number as EntityBank
from Treasury.TreasuryNote tn
inner join Treasury.CashReceipts cr on cr.Id = tn.CashReceiptId
left join Treasury.CashRegisters c on c.Id = tn.CashRegisterId
left join Treasury.EntityBankAccounts eba on eba.Id = tn.EntityBankAccountId
left join (
select EntityCode, sum(DebitValue) as Value
from GeneralLedger.JournalVouchers jv
inner join GeneralLedger.JournalVoucherDetails jvd on jv.Id = jvd.IdAccounting
inner join GeneralLedger.MainAccounts ma on ma.Id = jvd.IdMainAccount and ma.LegalBookId = 1
where EntityName = 'TreasuryNote'
group by EntityCode
) b on tn.Code = b.EntityCode
where cr.Value <> isnull(b.Value,0) and tn.Status in (2,4) and NoteType = 4
union all
select 'Consignaciones' as Entity,cs.Code,cs.DocumentDate,cs.CreationUser,cs.Description as Detail, cs.Value as TreasuryValue, isnull(b.Value,0) as GeneralValue, '' as Cash, eba.Code + ' - ' + eba.Number as EntityBank
from Treasury.Consignment cs
left join Treasury.EntityBankAccounts eba on eba.Id = cs.EntityBankAccountId
left join (
select EntityCode, sum(DebitValue) as Value
from GeneralLedger.JournalVouchers jv
inner join GeneralLedger.JournalVoucherDetails jvd on jv.Id = jvd.IdAccounting
inner join GeneralLedger.MainAccounts ma on ma.Id = jvd.IdMainAccount and ma.LegalBookId = 1
where EntityName = 'Consignment'
group by EntityCode
) b on cs.Code = b.EntityCode
where cs.Value <> isnull(b.Value,0) and cs.Status in (2,4)
union all
select 'Ajuste Contable' as Entity,
cast(jv.Consecutive as varchar(20)) as Code,
jv.VoucherDate as DocumentDate,
jv.CreationUser,
jv.Detail,
0 as TreasuryValue,
sum(jvd.DebitValue) as GeneralValue,
cr.Code + ' - ' + cr.Name as Cash,
eba.Code + ' - ' + eba.Number as EntityBank
from GeneralLedger.JournalVouchers jv
inner join GeneralLedger.JournalVoucherDetails jvd on jvd.IdAccounting = jv.Id
inner join GeneralLedger.MainAccounts ma on ma.Id = jvd.IdMainAccount and ma.LegalBookId = 1
left join Treasury.CashRegisters cr on cr.IdMainAccount = jvd.IdMainAccount
left join Treasury.EntityBankAccounts eba on eba.IdMainAccount = jvd.IdMainAccount
where jvd.IdMainAccount in (select IdMainAccount from Treasury.CashRegisters union all select IdMainAccount from Treasury.EntityBankAccounts)
and EntityName not in ('CashReceipts','TreasuryNote','Consignment', 'VoucherTransaction') and jv.Status = 2
group by jv.Consecutive, jv.VoucherDate, jv.CreationUser, jv.Detail, cr.Code, cr.Name, eba.Code, eba.Number
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de auditoría de tesorería orientada a control y conciliación contable. Detecta discrepancias entre el valor registrado en los documentos de tesorería (recibos de caja, comprobantes de egreso, notas DB/CR, consignaciones y ajustes contables) y el valor acumulado en el libro mayor, filtrando únicamente documentos en estados confirmado o revertido. Consolida en un único resultado aplanado todos los tipos de documento con su caja o cuenta bancaria asociada, para consumo en reportes de auditoría financiera.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VTreasutyAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VTreasutyAudit';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Auditoría que detecta diferencias entre los valores registrados en documentos de tesorería (recibos, egresos, notas, consignaciones) y los valores contabilizados en el libro mayor legal, además de listar ajustes contables hechos directamente sobre cuentas de tesorería.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VTreasutyAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los movimientos contables deben estar en el libro legal (MainAccounts.LegalBookId = 1) para ser comparados.; Los documentos de tesorería deben estar en estado 2 o 4 para ser auditados.; Los comprobantes contables del bloque de ajuste deben estar en estado 2.; La conciliación se basa en que el código del documento de tesorería coincida con EntityCode de los detalles contables y el EntityName respectivo.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VTreasutyAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se concilia contra movimientos del libro legal (LegalBookId = 1).; La comparación contable usa siempre la suma de DebitValue agrupada por EntityCode.; Sólo documentos en estados 2 o 4 son auditados (típicamente confirmados/aplicados).; Los ajustes contables sobre cuentas de tesorería sólo se listan si el comprobante está en estado 2.; La vista nunca retorna documentos cuyo valor coincida exactamente con el valor contabilizado (sólo descuadres).', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VTreasutyAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Recibos de caja; Comprobantes de egreso; Notas de tesorería (débito/crédito y reversiones); Consignaciones bancarias; Ajustes contables; Libro contable legal; Cuentas bancarias de la entidad; Cajas registradoras; Conciliación tesorería vs contabilidad', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VTreasutyAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewInternal.VTreasutyAudit: Cuando cr.Value <> isnull(b.Value,0) y cr.Status in (2,4), expone el recibo de caja como inconsistencia tipo ''Recibos de caja''.; [RETURN_RESULT] ViewInternal.VTreasutyAudit: Cuando vt.Value <> isnull(b.Value,0) y vt.Status in (2,4), expone el comprobante de egreso como inconsistencia tipo ''Comprobante de Egreso''.; [RETURN_RESULT] ViewInternal.VTreasutyAudit: Cuando tn.Value <> isnull(b.Value,0), tn.Status in (2,4) y NoteType in (1,2), expone la nota de tesorería como ''Notas DB/CR''.; [RETURN_RESULT] ViewInternal.VTreasutyAudit: Cuando NoteType = 3, tn.Status in (2,4) y vt.Value <> isnull(b.Value,0), expone la nota como ''Notas Rev CE'' (reversión de comprobante de egreso) comparando contra el VoucherTransaction asociado.; [RETURN_RESULT] ViewInternal.VTreasutyAudit: Cuando NoteType = 4, tn.Status in (2,4) y cr.Value <> isnull(b.Value,0), expone la nota como ''Notas Rev RC'' (reversión de recibo de caja) comparando contra el CashReceipts asociado.; [RETURN_RESULT] ViewInternal.VTreasutyAudit: Cuando cs.Value <> isnull(b.Value,0) y cs.Status in (2,4), expone la consignación como inconsistencia tipo ''Consignaciones''.; [RETURN_RESULT] ViewInternal.VTreasutyAudit: Cuando un detalle contable (jv.Status=2) afecta una cuenta principal asociada a una caja o cuenta bancaria y EntityName no es (''CashReceipts'',''TreasuryNote'',''Consignment'',''VoucherTransaction''), se reporta como ''Ajuste Contable''.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VTreasutyAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Status del documento de tesorería in (2,4) → Se incluye en la auditoría sólo si el valor del documento difiere del débito acumulado en el libro mayor legal. else Se excluye del resultado.; si TreasuryNote.NoteType → 1 ó 2 → categoría ''Notas DB/CR''; 3 → ''Notas Rev CE'' (compara contra VoucherTransaction); 4 → ''Notas Rev RC'' (compara contra CashReceipts).; si JournalVoucherDetails.IdMainAccount pertenece a CashRegisters o EntityBankAccounts y EntityName no es de tesorería conocida → Se reporta como ''Ajuste Contable'' con TreasuryValue = 0 y GeneralValue = sum(DebitValue). else No se reporta en el bloque de ajustes.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VTreasutyAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.CashReceipts; Treasury.CashRegisters; Treasury.EntityBankAccounts; Treasury.VoucherTransaction; Treasury.TreasuryNote; Treasury.Consignment; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VTreasutyAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VTreasutyAudit';
GO
