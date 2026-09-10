CREATE view [GeneralLedger].[VTreasutyAudit]
as
select ROW_NUMBER() OVER(ORDER BY DocumentDate ASC) as Row ,Entity,Code,DocumentDate, CreationUser,Detail, TreasuryValue, GeneralValue, Cash, EntityBank, Number, MainAccount, LegalBookId from (
select 'Recibos de caja' as Entity,cr.Code,DocumentDate, cr.CreationUser,Detail, cr.Value as TreasuryValue, isnull(b.Value,0) as GeneralValue, c.Code + ' - ' + c.Name as Cash, eba.Code + ' - ' + eba.Number as EntityBank
,ma.Number as Number, ma.Name as MainAccount, gl.Id as LegalBookId
from Treasury.CashReceipts cr
inner join GeneralLedger.MainAccounts as ma on ma.Id = cr.IdMainAccount
inner join GeneralLedger.LegalBook as gl on gl.Id = ma.LegalBookId
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
select 'Comprobante de Egreso' as Entity,vt.Code,DocumentDate,vt.CreationUser,vt.Detail, sum(vtd.[Value]) as TreasuryValue, isnull(b.Value,0) as GeneralValue, c.Code + ' - ' + c.Name as Cash, eba.Code + ' - ' + eba.Number as EntityBank
,ma.Number as Number, ma.Name as MainAccount, gl.Id as LegalBookId
from Treasury.VoucherTransaction vt
inner join Treasury.VoucherTransactionDetails vtd on vtd.IdVoucherTransaction = vt.Id and vtd.Nature = 1
inner join GeneralLedger.MainAccounts as ma on ma.Id = vt.IdMainAccount
inner join GeneralLedger.LegalBook as gl on gl.Id = ma.LegalBookId
left join Treasury.CashRegisters c on c.Id = vt.IdCashRegister
left join Treasury.EntityBankAccounts eba on eba.Id = vt.IdEntityBankAccount
left join (
select EntityCode, sum(DebitValue) as Value
from GeneralLedger.JournalVouchers jv
inner join GeneralLedger.JournalVoucherDetails jvd on jv.Id = jvd.IdAccounting
inner join GeneralLedger.MainAccounts ma on ma.Id = jvd.IdMainAccount and ma.LegalBookId = 1
where EntityName = 'VoucherTransaction' and jv.Status in (2,4)
group by EntityCode
) b on vt.Code = b.EntityCode
where vt.Status in (2,4)
group by vt.Code, vt.DocumentDate, vt.CreationUser, vt.Detail, b.[Value], c.Code,c.[Name], eba.Code, eba.[Number], ma.Number, ma.[Name], gl.Id
having sum(vtd.Value) <> isnull(b.Value,0) 
union all
select 'Notas DB/CR' as Entity,tn.Code,tn.NoteDate as DocumentDate,tn.CreationUser,tn.Description as Detail, tn.Value as TreasuryValue, isnull(b.Value,0) as GeneralValue, c.Code + ' - ' + c.Name as Cash, eba.Code + ' - ' + eba.Number as EntityBank
,ma.Number as Number, ma.Name as MainAccount, gl.Id as LegalBookId
from Treasury.TreasuryNote tn
left join GeneralLedger.MainAccounts as ma on ma.Id = tn.MainAccountId
left join GeneralLedger.LegalBook as gl on gl.Id = ma.LegalBookId
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
,ma.Number as Number, ma.Name as MainAccount, gl.Id as LegalBookId
from Treasury.TreasuryNote tn
inner join Treasury.VoucherTransaction vt on vt.Id = tn.VoucherTransactionId
left join GeneralLedger.MainAccounts as ma on ma.Id = tn.MainAccountId
left join GeneralLedger.LegalBook as gl on gl.Id = ma.LegalBookId
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
,ma.Number as Number, ma.Name as MainAccount, gl.Id as LegalBookId
from Treasury.TreasuryNote tn
inner join Treasury.CashReceipts cr on cr.Id = tn.CashReceiptId
left join GeneralLedger.MainAccounts as ma on ma.Id = tn.MainAccountId
left join GeneralLedger.LegalBook as gl on gl.Id = ma.LegalBookId
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
,ma.Number as Number, ma.Name as MainAccount, gl.Id as LegalBookId
from Treasury.Consignment cs
inner join GeneralLedger.MainAccounts as ma on ma.Id = cs.MainAccountId
inner join GeneralLedger.LegalBook as gl on gl.Id = ma.LegalBookId
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
,ma.Number as Number, ma.Name as MainAccount
, gl.Id as LegalBookId
from GeneralLedger.JournalVouchers jv
inner join GeneralLedger.JournalVoucherDetails jvd on jvd.IdAccounting = jv.Id
inner join GeneralLedger.MainAccounts ma on ma.Id = jvd.IdMainAccount and ma.LegalBookId = 1
inner join GeneralLedger.LegalBook as gl on gl.Id = ma.LegalBookId
left join Treasury.CashRegisters cr on cr.IdMainAccount = jvd.IdMainAccount
left join Treasury.EntityBankAccounts eba on eba.IdMainAccount = jvd.IdMainAccount
where jvd.IdMainAccount in (select IdMainAccount from Treasury.CashRegisters union all select IdMainAccount from Treasury.EntityBankAccounts)
and EntityName not in ('CashReceipts','TreasuryNote','Consignment', 'VoucherTransaction') and jv.Status = 2
group by jv.Consecutive, jv.VoucherDate, jv.CreationUser, jv.Detail, cr.Code, cr.Name, eba.Code, eba.Number, ma.Number, ma.Name, gl.Id
) as Datos
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de auditoría de tesorería que detecta inconsistencias entre los valores registrados en los módulos de tesorería y los comprobantes contables del libro mayor. Consolida en una sola consulta todos los documentos de tesorería activos (estados 2 y 4) —recibos de caja, comprobantes de egreso, notas débito/crédito, notas de reversión y consignaciones— comparando el valor registrado en tesorería contra la sumatoria de débitos contabilizados en el diario de comprobantes (JournalVouchers/JournalVoucherDetails). Solo muestra los registros donde existe diferencia entre ambos valores, es decir, documentos que no cuadran entre tesorería y contabilidad. Sirve para auditoría contable, conciliación de tesorería y detección de documentos sin contabilizar o con valores contabilizados incorrectamente; cada fila incluye el tipo de documento, código, fecha, usuario que lo creó, detalle, valor de tesorería, valor contabilizado, la caja o cuenta bancaria asociada, la cuenta contable principal y el libro legal al que pertenece.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'VIEW', @level1name = N'VTreasutyAudit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'VIEW', @level1name = N'VTreasutyAudit';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de auditoría que detecta descuadres entre los valores registrados en tesorería y los contabilizados en el libro mayor, listando los documentos cuyo valor difiere del débito contable correspondiente.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VTreasutyAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los documentos de tesorería deben tener una cuenta contable principal (MainAccount) asociada para cruzarse con el libro legal.; Los comprobantes contables (JournalVouchers) deben referenciar las entidades de tesorería mediante EntityName y EntityCode para poder consolidar el valor contabilizado.; El libro legal con Id = 1 se considera el libro contable principal sobre el cual se valida el cuadre.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VTreasutyAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran documentos de tesorería en estado 2 o 4 (excepto ajustes contables que requieren Status=2).; Únicamente se cruzan asientos contables sobre el libro legal con LegalBookId=1.; Para comprobantes de egreso solo se totalizan los detalles con Nature=1 (débitos).; Si no existe asiento contable asociado, GeneralValue se asume 0 vía isnull(b.Value,0) y se reporta el descuadre.; Los ajustes contables excluyen explícitamente las entidades ''CashReceipts'',''TreasuryNote'',''Consignment'',''VoucherTransaction'' para no duplicar partidas ya auditadas en sus secciones específicas.; El resultado se numera secuencialmente por DocumentDate ascendente.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VTreasutyAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Recibos de caja; Comprobantes de egreso; Notas débito/crédito de tesorería; Notas de reverso (CE y RC); Consignaciones bancarias; Ajustes contables; Cuentas bancarias de la entidad; Cajas registradoras; Comprobantes de diario / libro mayor; Libro legal contable; Auditoría de cuadre tesorería vs contabilidad', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VTreasutyAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] GeneralLedger.VTreasutyAudit: Devuelve recibos de caja con cr.Status in (2,4) cuando cr.Value <> isnull(b.Value,0) (valor de tesorería distinto al débito contabilizado).; [RETURN_RESULT] GeneralLedger.VTreasutyAudit: Devuelve comprobantes de egreso con vt.Status in (2,4) cuando sum(vtd.Value) con Nature=1 <> isnull(b.Value,0) del comprobante contable con Status in (2,4).; [RETURN_RESULT] GeneralLedger.VTreasutyAudit: Devuelve notas de tesorería con tn.Status in (2,4) y NoteType in (1,2) cuando tn.Value <> isnull(b.Value,0).; [RETURN_RESULT] GeneralLedger.VTreasutyAudit: Devuelve notas de reverso de comprobantes de egreso (NoteType=3) con tn.Status in (2,4) cuando vt.Value <> isnull(b.Value,0).; [RETURN_RESULT] GeneralLedger.VTreasutyAudit: Devuelve notas de reverso de recibos de caja (NoteType=4) con tn.Status in (2,4) cuando cr.Value <> isnull(b.Value,0).; [RETURN_RESULT] GeneralLedger.VTreasutyAudit: Devuelve consignaciones con cs.Status in (2,4) cuando cs.Value <> isnull(b.Value,0).; [RETURN_RESULT] GeneralLedger.VTreasutyAudit: Devuelve ajustes contables (JournalVouchers con jv.Status=2) que afectan cuentas asociadas a CashRegisters/EntityBankAccounts cuando EntityName no es de los módulos estándar de tesorería (''CashReceipts'',''TreasuryNote'',''Consignment'',''VoucherTransaction'').', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VTreasutyAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de documento = Recibo de caja → Compara cr.Value contra suma de DebitValue de JournalVouchers con EntityName=''CashReceipts''.; si Tipo de documento = Comprobante de Egreso → Compara suma de detalles con Nature=1 contra DebitValue de JournalVouchers con EntityName=''VoucherTransaction'' y Status in (2,4).; si NoteType in (1,2) → Se clasifica como ''Notas DB/CR'' y se compara tn.Value contra el contabilizado.; si NoteType = 3 → Se clasifica como ''Notas Rev CE'', usa el valor del VoucherTransaction asociado para comparar.; si NoteType = 4 → Se clasifica como ''Notas Rev RC'', usa el valor del CashReceipt asociado para comparar.; si Movimiento contable sobre cuenta de caja/banco con EntityName fuera de los módulos de tesorería y jv.Status=2 → Se clasifica como ''Ajuste Contable'' y se reporta con TreasuryValue=0.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VTreasutyAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.CashReceipts; Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.TreasuryNote; Treasury.Consignment; Treasury.CashRegisters; Treasury.EntityBankAccounts; GeneralLedger.MainAccounts; GeneralLedger.LegalBook; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VTreasutyAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VTreasutyAudit';
GO
